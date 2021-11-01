namespace Loans_application.Models
{
    public class Loan : BaseIdModel
    {
        public double InterestRate { get; set; }
        public double Amount { get; set; }
        public double DefaultLikelihood { get; set; }
        public string State { get; set; }

        public int? FacilityId { get; set; }
    }
}
