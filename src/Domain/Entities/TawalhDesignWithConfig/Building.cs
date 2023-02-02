using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common;
using Tawala.Domain.Entities.Settings.OptionSetsEntities;
using Tawala.Domain.Entities.Settings.ServiceProvider;

namespace Tawala.Domain.Entities.TawalhDesignWithConfig
{
    public class Building : AuditableEntity, IEntityBase
    {
        public string Name { get; set; }
        public string NameEn { get; set; }
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        public Guid? BuildingTypeId { get; set; }
        public OptionSetItem BuildingType { get; set; }
        public string Address { get; set; }
        public string AddressDetails { get; set; }
        public string ResposableName { get; set; }
        public string ResposablePhone { get; set; }
        public Guid? BranchId { get; set; }
        public Branch Branch { get; set; }
        public bool IsActive { get; set; } 
    }
}
