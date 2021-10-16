using Loans_application.models;
using System;
using System.Collections.Generic;

namespace Loans_application
{
    class Program
    {
        private static List<Bank> _banks;
        private static List<Loan> _loans;
        private static List<Facility> facilities;
        private static List<Loan> _covenants;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            CsvService service = new();

            _loans = service.GetLoans();

            foreach (var loan in _loans)
            {

            }
        }
    }
}
