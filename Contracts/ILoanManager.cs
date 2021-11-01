using Loans_application.Models;
using System.Collections.Generic;

namespace Loans_application.Contracts
{
    interface ILoanManager
    {
        public List<LoanFacilityPair> ManageLoans(
            List<Loan> loans,
            List<Facility> facilities,
            List<Covenant> covenants);
    }
}
