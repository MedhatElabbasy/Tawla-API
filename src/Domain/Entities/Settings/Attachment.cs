using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common;
using Tawala.Domain.Entities.Identity;

namespace Tawala.Domain.Entities.Settings
{
    public class AppAttachment : AuditableEntity, IEntityBase
    {
        public Guid Id { get; set; }
        public Guid ImageId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public string AppUserId { get; set; }
    }
}
