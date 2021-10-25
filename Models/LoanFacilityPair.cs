namespace Loans_application.Models
{
    public class LoanFacilityPair
    {
        public Loan Loan { get; }
        public Facility Facility { get; }
        public double Cost { get => Loan.Amount + (Loan.Amount + Loan.Amount * Loan.InterestRate) * Facility.InterestRate; }
        public LoanFacilityPair(Loan loan, Facility facility)
        {
            Loan = loan;
            Facility = facility;
        }

        public override string ToString() => $"Loan Id: {Loan.Id}, Facility Id: {Facility.Id}, Loan Cost: {Cost}";
    }
}
