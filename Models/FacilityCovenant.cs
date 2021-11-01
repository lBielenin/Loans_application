using System.Collections.Generic;
using System.Linq;

namespace Loans_application.Models
{
    public class FacilityCovenant
    {
        public Facility Facility { get; set; }
        public IEnumerable<Covenant> Covenants { get; set; }

        public IEnumerable<string> GetConvenantStates() => Covenants?.Select(cov => cov.BannedState);
    }
}
