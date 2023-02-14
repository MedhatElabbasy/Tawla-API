using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common;
using Tawala.Domain.Entities.Settings.ServiceProvider;

namespace Tawala.Domain.Entities.Reservations
{
    public class RestOccasions : AuditableEntity, IEntityBase
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string DescriptionEn { get; set; }
        public Guid? RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        public bool IsActive { get; set; }
    }
}
