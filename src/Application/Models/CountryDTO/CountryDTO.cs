using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.Mappings;
using Tawala.Domain.Entities.Country;

namespace Tawala.Application.Models.CountryDTO
{
    public class CountryDTO : IMapFrom<Country>
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string PrefixCode { get; set; }
        public string NameEN { get; set; }
        public string Regex { get; set; }
        public string IOS2 { get; set; }

    }
}
