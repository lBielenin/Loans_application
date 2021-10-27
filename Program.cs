using Loans_application.Contants;
using Loans_application.CSVModels;
using Loans_application.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace Loans_application
{
    class Program
    {

        static void Main(string[] args)
        {
            WriteLine("Welcome to Loans Application!");
            WriteLine("Please, press button to select data set or q to quit");
            WriteLine("1: small");
            WriteLine("2. large");

            var dataSetValue = GetDataSetValueFromUserInput();  
            (List<Loan> loans, List<Facility> facilities, List<Covenant> covenants) = SetupData(new CsvService(dataSetValue));
            LoanManager manager = new(loans, facilities, covenants);
            List<LoanFacilityPair> result = manager.ManageLoans();

            result.OrderBy(res => res.Loan.Id).ToList().ForEach(res => WriteLine(res));

            WriteLine("Done. Press any key to terminate program");
            _ = ReadKey();
        }

        private static string GetDataSetValueFromUserInput()
        {
            var isPicked = false;
            string pickedDataSet = "";
            do
            {
                ConsoleKeyInfo pickedValue = ReadKey();

                switch ((char)pickedValue.Key)
                {
                    case '1':
                        pickedDataSet = DataSetNames.SMALL;
                        isPicked = true;
                        break;
                    case '2':
                        pickedDataSet = DataSetNames.LARGE;
                        isPicked = true;
                        break;
                    case 'Q':
                        Environment.Exit(0);
                        break;
                    default:
                        WriteLine("\nInvalid button, try again.");
                        break;
                }
            } while (!isPicked);

            WriteLine(string.Empty);
            return pickedDataSet;
        }

        private static (List<Loan> loans, List<Facility> facilities, List<Covenant> covenants)  SetupData(CsvService csvService)
        { 
            List<Loan> loans = csvService.GetLoans().Adapt<List<Loan>>();
            List<Facility> facilities = csvService.GetFacilities().Adapt<List<Facility>>();
            List<Covenant> covenants = csvService.GetCovenants().Adapt<List<Covenant>>();

            return (loans, facilities, covenants);
        }

    }
}