using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common;

namespace Tawala.Domain.Entities.Notifications
{
    public class Device : AuditableEntity, IEntityBase
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string UserId { get; set; }
        public string DeviceToken { get; set; }
    }
}
