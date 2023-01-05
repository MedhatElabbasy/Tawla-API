using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tawala.Domain.Models
{
    public class TotpSettings
    {
        public string TotpSecret { get; set; }
        public int? TotpLength { get; set; }
        public int? TotpStep { get; set; }
    }
}
