using Loans_application.Contants;
using Loans_application.Models;
using Loans_application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace Loans_application
{
    internal class LoansApplication
    {
        public void Run()
        {
            WriteLine(@"
            Welcome to Loans Application!
            Please, press button to select data set or q. to quit
            1: small
            2. large");

            string dataSetValue = GetDataSetValueFromUserInput();

            var facade = new LoanApplicationDataFacade(new CsvService(dataSetValue), new LoanManagerService());

            List<LoanFacilityPair> result = facade.CalculateLoansFromCSVData();

            result.OrderBy(res => res.Loan.Id).ToList().ForEach(res => WriteLine(res));

            WriteLine("Done. Press any key to terminate program");
            _ = ReadKey();
        }

        private static string GetDataSetValueFromUserInput()
        {
            var isPicked = false;
            string pickedDataSetName = "";
            do
            {
                ConsoleKeyInfo pickedValue = ReadKey();

                switch ((char)pickedValue.Key)
                {
                    case '1':
                        pickedDataSetName = DataSetNames.SMALL;
                        isPicked = true;
                        break;
                    case '2':
                        pickedDataSetName = DataSetNames.LARGE;
                        isPicked = true;
                        break;
                    case 'q':
                        Environment.Exit(0);
                        break;
                    default:
                        WriteLine("Invalid button, try again.");
                        break;
                }

            } while (!isPicked);

            WriteLine(string.Empty);
            return pickedDataSetName;
        }
    }
}