using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common;
using Tawala.Domain.Entities.Settings;
using Tawala.Domain.Entities.Settings.ServiceProvider;
using Tawala.Domain.Entities.CategoryEni;
namespace Tawala.Domain.Entities.menu
{
    public class Items : AuditableEntity, IEntityBase
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Namen { get; set; }
        public string Description { get; set; }
        public string DescriptionEn { get; set; }
        public bool IsActive { get; set; }
        public Guid? RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        public decimal Price { get; set; }
        public Guid? PhotoId { get; set; }
        public AppAttachment Photo { get; set; }
        public Guid? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
