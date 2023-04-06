using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.Mappings;
using Tawala.Application.Models.ItemsDTOS;
using Tawala.Application.Models.MyAppUserDTO;
using Tawala.Domain.Entities.Identity;
using Tawala.Domain.Entities.menu;
using Tawala.Domain.Entities.Orders;

namespace Tawala.Application.Models.OrdersDTO
{
    public class OrderItemsAddDTO : IMapFrom<OrderItems>
    {
        public Guid? ItemsId { get; set; } 
        public int Amount { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal TotalWithAmount { get; set; }
        public string AppUserId { get; set; }
    }
    public class OrderItemsUpdateDTO : IMapFrom<OrderItems>
    {
        public Guid Id { get; set; }
        public Guid? ItemsId { get; set; }
        public Guid? OrdersId { get; set; }
        public int Amount { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal TotalWithAmount { get; set; }
        public string AppUserId { get; set; }
    }
    public class OrderItemsResDTO : IMapFrom<OrderItems>
    {
        public Guid Id { get; set; }
        public Guid? ItemsId { get; set; }
        public ItemsResDTO Items { get; set; }
        public Guid? OrdersId { get; set; }
        public OrdersResDTO Orders { get; set; }
        public int Amount { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal TotalWithAmount { get; set; }
        public string AppUserId { get; set; }
        public AppUserResDTO AppUser { get; set; }
    }
}
