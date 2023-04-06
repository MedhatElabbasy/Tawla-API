using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common;
using Tawala.Domain.Entities.Identity;
using Tawala.Domain.Entities.ReservationsEn;
using Tawala.Domain.Entities.Settings.OptionSetsEntities;
using Tawala.Domain.Entities.Settings.ServiceProvider;

namespace Tawala.Domain.Entities.Orders
{
    public class Orders : AuditableEntity, IEntityBase
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public int OrderNumber { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid? OffersId { get; set; }
        public Offers Offers { get; set; }
        public Guid? RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        public virtual IList<OrderItems> OrderItems { get; set; } = new List<OrderItems>();
        public decimal Totalprice { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalAfterDiscount { get; set; }
        public decimal TotalVat { get; set; }
        public decimal TotalAftetrVat { get; set; }
        public decimal FinalPrice { get; set; }
        public Guid? OrderStatusId { get; set; }
        public OptionSetItem OrderStatus { get; set; }

        public string Address { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string AddressDetails { get; set; }


    }
}
