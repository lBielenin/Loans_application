using CsvHelper.Configuration.Attributes;

namespace Loans_application.CSVModels
{
    public class BankCSV
    {
        [Name("id")]
        public int Id { get; set; }
        [Name("name")]
        public string Name { get; set; }
    }
}
