using CsvHelper.Configuration.Attributes;

namespace Loans_application.CSVModels
{
    public record BankCSV
    {
        [Name("id")]
        public int Id { get; set; }
        [Name("name")]
        public string Name { get; set; }
    }
}
