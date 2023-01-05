using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tawala.Domain.Entities.Identity
{
    public class AppIdentityRole : IdentityRole
    {
        public bool IsDeleted { get; set; }
        public string ParentId { get; set; }
        public string NameAR { get; set; }
        public string NameEn { get; set; }
    }
}
