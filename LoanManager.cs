using Loans_application.Contracts;
using Loans_application.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Loans_application
{
    public class LoanManager : ILoanManager
    {
        private List<Loan> _loans;
        private List<Facility> _facilities;
        private List<Covenant> _covenants;
        private List<FacilityCovenant> _facilityCovenants;
        private List<Loan> _unManagedLoans;
        private List<Loan> _managedLoans;
        private List<Loan> _unmanageableLoans = new();
        private double _sumOfFacilitiesCoverage = 0;
        private double _sumOfLoans = 0;

        public List<LoanFacilityPair> ManageLoans(
            List<Loan> loans,
            List<Facility> facilities,
            List<Covenant> covenants)
        {
            SetupData(loans, facilities, covenants);

            while (_unManagedLoans.Count > 0)
            {
                Loan currentLoan =
                    _unManagedLoans.First();

                if (_sumOfLoans + currentLoan.Amount > _sumOfFacilitiesCoverage)
                {
                    break;
                }

                List<Facility> possibleFacilites =
                    GetPossibleFacilites(currentLoan);

                if (!possibleFacilites.Any())
                {
                    DisposeLoan(currentLoan);
                }

                List<Facility> eligibleFacilites =
                    possibleFacilites.Where(fac => fac.CanLoanBeCovered(currentLoan.Amount)).ToList();

                if (eligibleFacilites.Count() > 0)
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

            return _managedLoans
                .Select(loan => new LoanFacilityPair(loan, _facilities.First(fac => fac.Id == loan.FacilityId))).ToList();
        }

        private void SetupData(
            List<Loan> loans,
            List<Facility> facilities,
            List<Covenant> covenants)
        {
            _loans = loans;
            _facilities = facilities;
            _covenants = covenants;

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

        private List<Facility> GetPossibleFacilites(Loan loan)
        {
            var facilities = new List<Facility>();

            foreach (FacilityCovenant facCovenant in _facilityCovenants)
            {
                if (facCovenant.Facility.Amount >= loan.Amount)
                {
                    if (!facCovenant.GetConvenantStates().Contains(loan.State))
                    {
                        facilities.Add(facCovenant.Facility);
                        continue;
                    }
                    Covenant applicableCovenant = 
                        facCovenant.Covenants.First(cov => cov.BannedState == loan.State);

                    if (applicableCovenant.MaxDefaultLikelihood <= loan.DefaultLikelihood)
                        facilities.Add(facCovenant.Facility);
                }
            }

            return facilities;
        }


        private void MoveLoanToManaged(Loan loan)
        {
            _unManagedLoans.Remove(loan);
            _managedLoans.Add(loan);
            _sumOfLoans += loan.Amount;
        }

        private void MoveLoanToUnmanaged(Loan loan)
        {
            _managedLoans.Remove(loan);
            _unManagedLoans.Add(loan);
        }

        private void DisposeLoan(Loan loan)
        {
            _managedLoans.Remove(loan);
            _unmanageableLoans.Add(loan);
            _sumOfLoans -= loan.Amount;
        }
    }
}
