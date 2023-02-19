using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common;
using Tawala.Domain.Entities.Settings.ServiceProvider;

namespace Tawala.Domain.Entities.Settings
{
    public class RestCommissionConfig : AuditableEntity, IEntityBase
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        public bool IsAmount { get; set; }
        public bool IsPercent { get; set; }
        public decimal Percent { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
    }
}
