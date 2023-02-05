using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.Mappings;
using Tawala.Domain.Entities.menu;

namespace Tawala.Application.Models.ItemsDTOS
{
    public class MenuItemsAddDTO : IMapFrom<MenuItems>
    {

        public Guid? ItemsId { get; set; }
        public Guid? MenuId { get; set; }
        public bool IsActive { get; set; }
    }
    public class MenuItemsUpdateDTO : IMapFrom<MenuItems>
    {
        public Guid Id { get; set; }
        public Guid? ItemsId { get; set; }
        public Guid? MenuId { get; set; }
        public bool IsActive { get; set; }
    }
    public class MenuItemsResDTO : IMapFrom<MenuItems>
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? ItemsId { get; set; }
        public ItemsResDTO Items { get; set; }
        public Guid? MenuId { get; set; }
        public MenuResDTO Menu { get; set; }
        public bool IsActive { get; set; }
    }
}
