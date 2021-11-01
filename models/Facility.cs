using System.Collections.Generic;
using System.Linq;

namespace Loans_application.Models
{
    public class Facility : BaseIdModel
    {
        public double Amount { get; set; }
        public double InterestRate { get; set; }
        public int BankId { get; set; }
        public double RealCovering { get => AssignedLoans.Select(l => l.Amount).Sum(); }
        public double RealAmount { get => Amount - RealCovering; }

        public bool CanLoanBeCovered(double amount) => RealAmount >= amount;

        private List<Loan> _assignedLoans = new ();
        public IReadOnlyList<Loan> AssignedLoans
        {
            get { return _assignedLoans.AsReadOnly(); }
        }


        public void AddLoan(Loan loan)
        {
            loan.FacilityId = Id;
            _assignedLoans.Add(loan);
        }

        public void RemoveLoan(Loan loan)
        {
            loan.FacilityId = null;
            _assignedLoans.Remove(loan);
        }
    }
}
