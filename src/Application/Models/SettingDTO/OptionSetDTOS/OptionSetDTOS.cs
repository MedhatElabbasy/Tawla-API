using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.Mappings;
using Tawala.Domain.Entities.Settings.OptionSetsEntities;

namespace Tawala.Application.Models.SettingDTO.OptionSetDTOS
{
    public class OptionSetAddDTO : IMapFrom<OptionSet>
    {
        public string Name { get; set; }
        public string DisplayNameAr { get; set; }
        public string DisplayNameEN { get; set; }

    }
    public class OptionSetUpdateDTO : IMapFrom<OptionSet>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayNameAr { get; set; }
        public string DisplayNameEN { get; set; }

    }
    public class OptionSetResDTO : IMapFrom<OptionSet>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayNameAr { get; set; }
        public string DisplayNameEN { get; set; }
        public virtual IList<OptionSetItemResDTO> OptionSetItems { get; set; } = new List<OptionSetItemResDTO>();

    }
}
