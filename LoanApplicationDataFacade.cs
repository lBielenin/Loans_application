using Loans_application.Contracts;
using Loans_application.Models;
using Mapster;
using System.Collections.Generic;

namespace Loans_application
{
    internal class LoanApplicationDataFacade
    {
        private readonly ICSVService _csvService;
        private readonly ILoanManager _loanManager;

        public LoanApplicationDataFacade(ICSVService csvService, ILoanManager loanManager)
        {
            _csvService = csvService;
            _loanManager = loanManager;
        }
        
        public List<LoanFacilityPair> CalculateLoansFromCSVData()
        {
            (List<Loan> loans, List<Facility> facilities, List<Covenant> covenants) = SetupData();
            
            return _loanManager.ManageLoans(loans, facilities, covenants);
        }

        private (List<Loan> loans, List<Facility> facilities, List<Covenant> covenants) SetupData()
        {
            List<Loan> loans = _csvService.GetLoans().Adapt<List<Loan>>();
            List<Facility> facilities = _csvService.GetFacilities().Adapt<List<Facility>>();
            List<Covenant> covenants = _csvService.GetCovenants().Adapt<List<Covenant>>();

            return (loans, facilities, covenants);
        }
    }
}
