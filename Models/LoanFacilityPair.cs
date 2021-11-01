namespace Loans_application.Models
{
    public class LoanFacilityPair
    {
        public Loan Loan { get; }
        public Facility Facility { get; }

        public double Cost 
        {
            get
            {
                double amountInterest = Loan.Amount + Loan.Amount * Loan.InterestRate;
                double allInterests = amountInterest + amountInterest * Facility.InterestRate;

                return allInterests;
            }
        }

        public LoanFacilityPair(Loan loan, Facility facility)
        {
            Loan = loan;
            Facility = facility;
        }

        public override string ToString() => $"Loan Id: {Loan.Id}, Facility Id: {Facility.Id}, Loan Cost: {Cost}";
    }
}
