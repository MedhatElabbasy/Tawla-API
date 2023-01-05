using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tawala.Domain.Models
{
    public class JWTSettings
    {
        public string securityKey { get; set; }
        public string validIssuer { get; set; }
        public string validAudience { get; set; }
        public int expiryInMinutes { get; set; }
    }
}
