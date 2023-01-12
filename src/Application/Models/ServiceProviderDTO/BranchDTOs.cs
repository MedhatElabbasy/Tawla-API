using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.Mappings;
using Tawala.Application.Models.SettingDTO.AdminDTOS;
using Tawala.Domain.Entities.Settings.AdminSettings;
using Tawala.Domain.Entities.Settings.ServiceProvider;

namespace Tawala.Application.Models.ServiceProviderDTO
{
    public class BranchAddDTO : IMapFrom<Branch>
    {
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string DescriptionEn { get; set; }
        public string Address { get; set; }
        public string AddressDescription { get; set; }
        public Guid? CityId { get; set; }
        public Guid? DistrictId { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
        public Guid? RestaurantId { get; set; }
    }
    public class BranchUpdateDTO : IMapFrom<Branch>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string DescriptionEn { get; set; }
        public string Address { get; set; }
        public string AddressDescription { get; set; }
        public Guid? CityId { get; set; }
        public Guid? DistrictId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string Notes { get; set; }
        public Guid? RestaurantId { get; set; }
    }
    public class BranchResDTO : IMapFrom<Branch>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string DescriptionEn { get; set; }
        public string Address { get; set; }
        public string AddressDescription { get; set; }
        public Guid? CityId { get; set; }
        public CityResDTO City { get; set; }
        public Guid? DistrictId { get; set; }
        public DistrictResDTO District { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
        public Guid? RestaurantId { get; set; }
        public RestaurantResDTO Restaurant { get; set; }
    }
}
