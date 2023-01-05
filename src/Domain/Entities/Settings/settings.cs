using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common;

namespace Tawala.Domain.Entities.Settings
{
    public class Settings :AuditableEntity, IEntityBase
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Key { get; set; }
        public string KeyAr { get; set; }
        public string Value { get; set; }
        public bool IsLocked { get; set; }
    }
}
