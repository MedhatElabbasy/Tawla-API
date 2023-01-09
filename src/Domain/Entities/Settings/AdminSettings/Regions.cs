using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common;

namespace Tawala.Domain.Entities.Settings.AdminSettings
{
    public class Regions : IEntityBase
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Code { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public bool IsActive { get; set; }
        public long RegionId { get; set; }
    }
}
