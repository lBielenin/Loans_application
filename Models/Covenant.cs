namespace Loans_application.Models
{
    public class Covenant
    {
        public int FacilityId { get; set; }
        public double MaxDefaultLikelihood { get; set; }
        public int BankId { get; set; }
        public string BannedState { get; set; }
    }
}
