using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common;

namespace Tawala.Domain.Entities.Country
{
    public class Country : IEntityBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PrefixCode { get; set; }
        public string NameEN { get; set; }
        public string Regex { get; set; }
        public bool IsDeleted { get; set; }
        public bool Status { get; set; }
        public string IOS2 { get; set; }
    }
}
