using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.Mappings;
using Tawala.Domain.Entities.Settings.AdminSettings;

namespace Tawala.Application.Models.SettingDTO.AdminDTOS
{
    public class DistrictAddDTO : IMapFrom<District>
    {
        public long DistrictId { get; set; }
        public long CityId { get; set; }
        public long RegionId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public bool IsActive { get; set; }

    }
    public class DistrictUpdateDTO : IMapFrom<District>
    {
        public long DistrictId { get; set; }
        public long CityId { get; set; }
        public long RegionId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public bool IsActive { get; set; }
        public Guid Id { get; set; }
    }
    public class DistrictResDTO : IMapFrom<District>
    {
        public long DistrictId { get; set; }
        public long CityId { get; set; }
        public long RegionId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public bool IsActive { get; set; }
        public Guid Id { get; set; }

    }
}
