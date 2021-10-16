using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loans_application.models
{
    class Loan
    {
        public int id { get; set; } 
        public double interest_rate { get; set; }
        public double amount { get; set; }
        public double default_likelihood { get; set; }
        public string state { get; set; }

    }
}
