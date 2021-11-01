using CsvHelper.Configuration.Attributes;

namespace Loans_application.CSVModels
{
    public record CovenantCSV
    {
        [Name("facility_id")]
        public int FacilityId { get; set; }
        [Name("max_default_likelihood")]
        public double? MaxDefaultLikelihood { get; set; }
        [Name("bank_id")]
        public int BankId { get; set; }
        [Name("banned_state")]
        public string BannedState { get; set; }
    }
}
