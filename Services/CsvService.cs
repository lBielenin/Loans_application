using CsvHelper;
using CsvHelper.Configuration;
using Loans_application.Contracts;
using Loans_application.CSVModels;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Loans_application.Services
{
    public class CsvService : ICSVService
    {
        private readonly string _filePath;

        public CsvService(string dataSetName)
        {
            _filePath = $"Static/{dataSetName}";
        }

        public IEnumerable<BankCSV> GetBanks() => 
            GetData<BankCSV>("banks.csv");

        public IEnumerable<CovenantCSV> GetCovenants() => 
            GetData<CovenantCSV>("covenants.csv");

        public IEnumerable<FacilityCSV> GetFacilities() => 
            GetData<FacilityCSV>("facilities.csv");

        public IEnumerable<LoanCSV> GetLoans() => 
            GetData<LoanCSV>("loans.csv");

        private List<T> GetData<T>(string fileName)
        {
            List<T> records;

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };

            using (TextReader reader = new StreamReader(GetFilePath(fileName)))
            using(var csvReader = new CsvReader(reader, config))
            {
                records = csvReader.GetRecords<T>().ToList();
            }

            return records;
        }
        private string GetFilePath(string end) => Path.Combine(_filePath, end);
    }
}
