using CsvHelper;
using Loans_application.models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loans_application
{
    class CsvService
    {
        private const string _filePath = "static/large";

        public IEnumerable<Bank> GetBanks()
        {
            return GetData<Bank>("banks.csv");
        }

        public IEnumerable<Covenant> GetCovenants()
        {
            return GetData<Covenant>("coventants.csv");
        }
        public IEnumerable<Facility> GetFacilities()
        {
            return GetData<Facility>("facilities.csv");
        }

        public List<Loan> GetLoans()
        {
            return GetData<Loan>("loans.csv");
        }

        private List<T> GetData<T>(string fileName)
        {
            List<T> records;

            using (TextReader reader = new StreamReader(GetFilePath(fileName)))
            using(var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                records = csvReader.GetRecords<T>().ToList();
            }

            return records;
        }
        private string GetFilePath(string end) => Path.Combine(_filePath, end);
    }
}
