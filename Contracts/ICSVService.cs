using Loans_application.CSVModels;
using System.Collections.Generic;

namespace Loans_application.Contracts
{
    interface ICSVService
    {
        public IEnumerable<BankCSV> GetBanks();
        public IEnumerable<CovenantCSV> GetCovenants();
        public IEnumerable<FacilityCSV> GetFacilities();

        public IEnumerable<LoanCSV> GetLoans();
    }
}
