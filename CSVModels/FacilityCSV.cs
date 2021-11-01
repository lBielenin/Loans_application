using CsvHelper.Configuration.Attributes;

namespace Loans_application.CSVModels
{
    public record FacilityCSV
    {
        [Name("id")]
        public int Id { get; set; }
        [Name("amount")]
        public double Amount { get; set; }
        [Name("interest_rate")]
        public double InterestRate { get; set; }
        [Name("bank_id")]
        public int BankId { get; set; }
    }
}
