using Loans_application.CSVModels;
using Loans_application.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace Loans_application
{
    class Program
    {
        private static CsvService csvService = new ();
        private static List<Bank> _banks;
        private static List<Loan> _loans;
        private static List<Facility> _facilities;
        private static List<Covenant> _covenants;
        private static List<FacilityCovenant> _facilityCovenants;
        private static List<Loan> _unManagedLoans;
        private static List<Loan> _managedLoans;
        private static List<Loan> _unmanageableLoans;
        private static double _sumOfFacilitiesCoverage = 0;
        private static double _sumOfLoans = 0;
        static void Main(string[] args)
        {
            SetupData();
            
            while(_unManagedLoans.Count > 0)
            {

                Loan currentLoan = 
                    _unManagedLoans.First();

                if(_sumOfLoans + currentLoan.Amount > _sumOfFacilitiesCoverage)
                {
                    break;
                }

                List<Facility> possibleFacilites = 
                    GetPossibleFacilites(currentLoan);
                if(!possibleFacilites.Any())
                {
                    DisposeLoan(currentLoan);
                }
                List<Facility> eligibleFacilites = 
                    possibleFacilites.Where(fac => fac.CanLoanBeCovered(currentLoan.Amount)).ToList();

                if(eligibleFacilites.Count() > 0)
                {
                    var facility = eligibleFacilites.First();

                    facility.AddLoan(currentLoan);
                    MoveLoanToManaged(currentLoan);

                    continue;
                }

                Facility facilityWithLowestMargin = possibleFacilites.OrderByDescending(fac => fac.RealCovering).First();

                Loan previouslyAssignedLoan =
                    _managedLoans
                    .Where(l => l.FacilityId == facilityWithLowestMargin.Id)
                    .Where(l => l.Amount + facilityWithLowestMargin.RealAmount >= currentLoan.Amount)
                    .OrderBy(l => l.Amount).First();
                
                facilityWithLowestMargin.RemoveLoan(previouslyAssignedLoan);
                MoveLoanToUnmanaged(previouslyAssignedLoan);

                facilityWithLowestMargin.AddLoan(currentLoan);
                MoveLoanToManaged(currentLoan);
            }

            List<LoanFacilityPair> result = _managedLoans.Select(loan => new LoanFacilityPair(loan, _facilities.First(fac => fac.Id == loan.FacilityId))).ToList();

            result.OrderBy(res => res.Loan.Id).ToList().ForEach(res => WriteLine(res));

            _ = ReadKey();
        } 
        
        private static List<Facility> GetPossibleFacilites(Loan loan)
        {
            var facilities = new List<Facility>();

            foreach(var facCovenant in _facilityCovenants)
            {
                if(facCovenant.Facility.Amount >= loan.Amount)
                {
                    if (!facCovenant.GetConvenantStates().Contains(loan.State))
                    {
                        facilities.Add(facCovenant.Facility);
                        continue;
                    }
                    var applicableCovenant = facCovenant.Covenants.First(cov => cov.BannedState == loan.State);

                    if (applicableCovenant.MaxDefaultLikelihood <= loan.DefaultLikelihood)
                        facilities.Add(facCovenant.Facility);
                }
            }

            return facilities;
        }

        private  static void MoveLoanToManaged(Loan loan)
        {
            _unManagedLoans.Remove(loan);
            _managedLoans.Add(loan);
            _sumOfLoans += loan.Amount;
        }

        private static void MoveLoanToUnmanaged(Loan loan)
        {
            _managedLoans.Remove(loan);
            _unManagedLoans.Add(loan);
        }

        private static void DisposeLoan(Loan loan)
        {
            _managedLoans.Remove(loan);
            _unmanageableLoans.Add(loan);
            _sumOfLoans -= loan.Amount;
        }

        private static void SetupData()
        {
            _loans = csvService.GetLoans().Adapt<List<Loan>>();
            _banks = csvService.GetBanks().Adapt<List<Bank>>();
            _facilities = csvService.GetFacilities().Adapt<List<Facility>>();
            _covenants = csvService.GetCovenants().Adapt<List<Covenant>>();
            _unManagedLoans = _loans;
            _managedLoans = new();

            _facilityCovenants =
                _facilities.Select(facility =>
                    new FacilityCovenant
                    {
                        Facility = facility,
                        Covenants = _covenants.Where(cov => cov.FacilityId == facility.Id)
                    }).ToList();

            _sumOfFacilitiesCoverage = _facilities.Select(fac => fac.Amount).Sum();
        }
    }
}
