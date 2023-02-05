using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.Mappings;
using Tawala.Domain.Entities.menu;
using Tawala.Domain.Entities.Settings.ServiceProvider;
using Tawala.Domain.Entities.Settings;
using Tawala.Application.Models.Common;
using Tawala.Application.Models.CategoryDTO;
using Tawala.Application.Models.ServiceProviderDTO;

namespace Tawala.Application.Models.ItemsDTOS
{
    public class ItemsAddDTO : IMapFrom<Items>
    {
        public string Namen { get; set; }
        public string Description { get; set; }
        public string DescriptionEn { get; set; }
        public bool IsActive { get; set; }
        public Guid? RestaurantId { get; set; }
        public decimal Price { get; set; }
        public Guid? PhotoId { get; set; }
        public Guid? CategoryId { get; set; }
    }
    public class ItemsUpdateDTO : IMapFrom<Items>
    {
        public Guid Id { get; set; }
        public string Namen { get; set; }
        public string Description { get; set; }
        public string DescriptionEn { get; set; }
        public bool IsActive { get; set; }
        public Guid? RestaurantId { get; set; }
        public decimal Price { get; set; }
        public Guid? PhotoId { get; set; }
        public Guid? CategoryId { get; set; }
    }
    public class ItemsResDTO : IMapFrom<Items>
    {
        public Guid Id { get; set; }
        public string Namen { get; set; }
        public string Description { get; set; }
        public string DescriptionEn { get; set; }
        public bool IsActive { get; set; }
        public Guid? RestaurantId { get; set; }
        public RestaurantResDTO Restaurant { get; set; }
        public decimal Price { get; set; }
        public Guid? PhotoId { get; set; }
        public AppAttachmentResDTO Photo { get; set; }
        public Guid? CategoryId { get; set; }
        public CategoryResDTO Category { get; set; }
    }
}
