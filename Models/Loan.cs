namespace Loans_application.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public double InterestRate { get; set; }
        public double Amount { get; set; }
        public double DefaultLikelihood { get; set; }
        public string State { get; set; }

        public int? FacilityId { get; set; }
    }
}
