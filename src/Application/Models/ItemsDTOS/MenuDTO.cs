using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.Mappings;
using Tawala.Domain.Entities.menu;
using Tawala.Domain.Entities.Settings.ServiceProvider;
using Tawala.Domain.Entities.Settings;
using Tawala.Application.Models.ServiceProviderDTO;
using Tawala.Application.Models.Common;

namespace Tawala.Application.Models.ItemsDTOS
{
    public class MenuAddDTO : IMapFrom<Menu>
    {
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string DescriptionEn { get; set; }
        public Guid? RestaurantId { get; set; }
        public bool IsActive { get; set; }
        public Guid? AppAttachmentId { get; set; }
        public List<MenuItemsAddDTO> MenuItems { get; set; } = new List<MenuItemsAddDTO>();
    }
    public class MenuUpdateDTO : IMapFrom<Menu>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string DescriptionEn { get; set; }
        public Guid? RestaurantId { get; set; }
        public bool IsActive { get; set; }
        public Guid? AppAttachmentId { get; set; }
        // public virtual IList<MenuItemsUpdateDTO> MenuItems { get; set; } = new List<MenuItemsUpdateDTO>();
    }
    public class MenuResDTO : IMapFrom<Menu>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string DescriptionEn { get; set; }
        public Guid? RestaurantId { get; set; }
        public RestaurantResDTO Restaurant { get; set; }
        public bool IsActive { get; set; }
        public Guid? AppAttachmentId { get; set; }
        public AppAttachmentResDTO AppAttachment { get; set; }
        public virtual IList<MenuItemsResDTO> MenuItems { get; set; } = new List<MenuItemsResDTO>();
    }
}
