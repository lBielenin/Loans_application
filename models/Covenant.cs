using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loans_application.models
{
    public class Covenant
    {
        public int facility_id { get; set; }
        public double max_default_likelihood { get; set; }
        public int bank_id { get; set; }
        public string banned_state { get; set; }
    }
}
