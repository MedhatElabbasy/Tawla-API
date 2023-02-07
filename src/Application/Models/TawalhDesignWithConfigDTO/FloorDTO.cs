using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.Mappings;
using Tawala.Application.Models.SettingDTO.OptionSetDTOS;
using Tawala.Domain.Entities.Settings.OptionSetsEntities;
using Tawala.Domain.Entities.TawalhDesignWithConfig;

namespace Tawala.Application.Models.TawalhDesignWithConfigDTO
{
    public class FloorAddDTO : IMapFrom<Floor>
    {
        public Guid? FloorNumberId { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public Guid? BuildingId { get; set; }
        public Guid? FloorTypeId { get; set; }
        public bool IsActive { get; set; }
    }
    public class FloorUpdateDTO : IMapFrom<Floor>
    {
        public Guid Id { get; set; }
        public Guid? FloorNumberId { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public Guid? BuildingId { get; set; }
        public Guid? FloorTypeId { get; set; }
        public bool IsActive { get; set; }
    }
    public class FloorResDTO : IMapFrom<Floor>
    {
        public Guid Id { get; set; }
        public Guid? FloorNumberId { get; set; }
        public OptionSetItemResDTO FloorNumber { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public Guid? BuildingId { get; set; }
        public BuildingResDTO Building { get; set; }
        public Guid? FloorTypeId { get; set; }
        public OptionSetItemResDTO FloorType { get; set; }
        public bool IsActive { get; set; }
    }
}
