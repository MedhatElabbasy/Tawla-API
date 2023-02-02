using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common;

namespace Tawala.Domain.Entities.menu
{
    public class MenuItems : AuditableEntity, IEntityBase
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? ItemsId { get; set; }
        public Items Items { get; set; }
        public Guid? MenuId { get; set; }
        public Menu Menu { get; set; }
        public bool IsActive { get; set; }
    }
}
