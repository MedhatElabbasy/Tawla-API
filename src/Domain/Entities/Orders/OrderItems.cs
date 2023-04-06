using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common;
using Tawala.Domain.Entities.Identity;
using Tawala.Domain.Entities.menu;

namespace Tawala.Domain.Entities.Orders
{
    public class OrderItems : AuditableEntity, IEntityBase
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? ItemsId { get; set; }
        public Items Items { get; set; }
        public Guid? OrdersId { get; set; }
        public Orders Orders { get; set; }
        public int Amount { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal TotalWithAmount { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
