using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.Mappings;
using Tawala.Domain.Entities.Settings.AdminSettings;

namespace Tawala.Application.Models.SettingDTO.AdminDTOS
{
    public class RegionsAddDTO : IMapFrom<Regions>
    {
       
        public string Code { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public bool IsActive { get; set; }
        public long RegionId { get; set; }
    }
    public class RegionsUpdateDTO : IMapFrom<Regions>
    {
        public Guid Id { get; set; } 
        public string Code { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public bool IsActive { get; set; }
        public long RegionId { get; set; }
    }
    public class RegionsResDTO : IMapFrom<Regions>
    {
        public Guid Id { get; set; }
       
        public string Code { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public bool IsActive { get; set; }
        public long RegionId { get; set; }
    }
}
