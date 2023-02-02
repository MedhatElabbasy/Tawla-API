using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common;
using Tawala.Domain.Entities.Settings.OptionSetsEntities;

namespace Tawala.Domain.Entities.TawalhDesignWithConfig
{
    public class Floor : AuditableEntity, IEntityBase
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? FloorNumberId { get; set; }
        public OptionSetItem FloorNumber { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public Guid? BuildingId { get; set; }
        public Building Building { get; set; }
        public Guid? FloorTypeId { get; set; }
        public OptionSetItem FloorType { get; set; }
        public bool IsActive { get; set; }
    }
}
