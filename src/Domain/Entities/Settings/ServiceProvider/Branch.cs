using System;
using Tawala.Domain.Common;
using Tawala.Domain.Entities.Settings.AdminSettings;

namespace Tawala.Domain.Entities.Settings.ServiceProvider
{
    public class Branch : AuditableEntity, IEntityBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string DescriptionEn { get; set; }
        public string Address { get; set; }
        public string AddressDescription { get; set; }
        public Guid? CityId { get; set; }
        public City City { get; set; }
        public Guid? DistrictId { get; set; }
        public District District { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string Notes { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumber2 { get; set; } 
        public Guid? RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
