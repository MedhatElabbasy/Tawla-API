using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.Mappings;
using Tawala.Application.Models.ServiceProviderDTO;
using Tawala.Application.Models.SettingDTO.OptionSetDTOS;
using Tawala.Domain.Entities.Settings.OptionSetsEntities;
using Tawala.Domain.Entities.Settings.ServiceProvider;
using Tawala.Domain.Entities.TawalhDesignWithConfig;

namespace Tawala.Application.Models.TawalhDesignWithConfigDTO
{
    public class BuildingAddDTO : IMapFrom<Building>
    {
        public string Name { get; set; }
        public string NameEn { get; set; }
        public Guid? RestaurantId { get; set; }
        public Guid? BuildingTypeId { get; set; }
        public string Address { get; set; }
        public string AddressDetails { get; set; }
        public string ResposableName { get; set; }
        public string ResposablePhone { get; set; }
        public Guid? BranchId { get; set; }
        public bool IsActive { get; set; }
    }
    public class BuildingUpdateDTO : IMapFrom<Building>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public Guid? RestaurantId { get; set; }
        public Guid? BuildingTypeId { get; set; }
        public string Address { get; set; }
        public string AddressDetails { get; set; }
        public string ResposableName { get; set; }
        public string ResposablePhone { get; set; }
        public Guid? BranchId { get; set; }
        public bool IsActive { get; set; }
    }
    public class BuildingResDTO : IMapFrom<Building>
    {
        public string Name { get; set; }
        public string NameEn { get; set; }
        public Guid Id { get; set; }
        public Guid? RestaurantId { get; set; }
        public RestaurantResDTO Restaurant { get; set; }
        public Guid? BuildingTypeId { get; set; }
        public OptionSetItemResDTO BuildingType { get; set; }
        public string Address { get; set; }
        public string AddressDetails { get; set; }
        public string ResposableName { get; set; }
        public string ResposablePhone { get; set; }
        public Guid? BranchId { get; set; }
        public BranchResDTO Branch { get; set; }
        public bool IsActive { get; set; }
    }
}
