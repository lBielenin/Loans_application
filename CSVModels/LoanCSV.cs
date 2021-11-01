using CsvHelper.Configuration.Attributes;

namespace Loans_application.CSVModels
{
    public record LoanCSV
    {
        [Name("id")]
        public int Id { get; set; }
        [Name("interest_rate")]
        public double InterestRate { get; set; }
        [Name("amount")]
        public double Amount { get; set; }
        [Name("default_likelihood")]
        public double DefaultLikelihood { get; set; }
        [Name("state")]
        public string State { get; set; }

    }
}
