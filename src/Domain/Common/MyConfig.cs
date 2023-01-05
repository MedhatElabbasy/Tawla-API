using Tawala.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tawala.Domain.Common
{
    public class MyConfig
    {
        public string DefaultIdentityPassword { get; set; }
        public TotpSettings TotpSettings { get; set; }
        public JWTSettings JWTSettings { get; set; }
        public string AttachmentPath { get; set; }
    }
}
