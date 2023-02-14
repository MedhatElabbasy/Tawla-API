using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.Mappings;
using Tawala.Application.Models.ServiceProviderDTO;
using Tawala.Domain.Entities.Reservations;
using Tawala.Domain.Entities.Settings.ServiceProvider;

namespace Tawala.Application.Models.ReservationsDTOS
{
    public class RestOccasionsAddDTO : IMapFrom<RestOccasions>
    {
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string DescriptionEn { get; set; }
        public Guid? RestaurantId { get; set; }
        public bool IsActive { get; set; }
    }
    public class RestOccasionsUpdateDTO : IMapFrom<RestOccasions>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string DescriptionEn { get; set; }
        public Guid? RestaurantId { get; set; }
        public bool IsActive { get; set; }
    }
    public class RestOccasionsResDTO : IMapFrom<RestOccasions>
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string DescriptionEn { get; set; }
        public Guid? RestaurantId { get; set; }
        public RestaurantResDTO Restaurant { get; set; }
        public bool IsActive { get; set; }
    }
}
