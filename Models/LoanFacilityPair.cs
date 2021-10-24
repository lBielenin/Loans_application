namespace Loans_application.Models
{
    public class LoanFacilityPair
    {
        public Loan Loan { get; }
        public Facility Facility { get; }

        public LoanFacilityPair(Loan loan, Facility facility)
        {
            Loan = loan;
            Facility = facility;
        }

        public override string ToString() => $"{Loan.Id}, {Facility.Id}, {Facility.Amount}, {Facility.RealCovering}";
    }
}
