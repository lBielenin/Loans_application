using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loans_application.models
{
    class Facility
    {
        public int id { get; set; }
        public double amount { get; set; }
        public double interest_rate { get; set; }
        public int bank_id { get; set; }

    }
}
