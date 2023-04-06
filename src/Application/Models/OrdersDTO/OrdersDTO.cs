using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.Mappings;
using Tawala.Domain.Entities.Identity;
using Tawala.Domain.Entities;
using Tawala.Domain.Entities.Orders;
using Tawala.Domain.Entities.Settings.OptionSetsEntities;
using Tawala.Domain.Entities.Settings.ServiceProvider;
using Tawala.Application.Models.OffersDTO;
using Tawala.Application.Models.ServiceProviderDTO;
using Tawala.Application.Models.SettingDTO.OptionSetDTOS;
using Tawala.Application.Models.MyAppUserDTO;

namespace Tawala.Application.Models.OrdersDTO
{
    public class OrdersAddDTO : IMapFrom<Orders>
    {
        public int OrderNumber { get; set; }
        public string AppUserId { get; set; }
        public Guid? OffersId { get; set; }
        public Guid? RestaurantId { get; set; }
        public decimal Totalprice { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalAfterDiscount { get; set; }
        public decimal TotalVat { get; set; }
        public decimal TotalAftetrVat { get; set; }
        public decimal FinalPrice { get; set; }
        public Guid? OrderStatusId { get; set; }
    }
    public class OrdersUpdateDTO : IMapFrom<Orders>
    {
        public Guid Id { get; set; }

        public int OrderNumber { get; set; }
        public string AppUserId { get; set; }

        public Guid? OffersId { get; set; }

        public Guid? RestaurantId { get; set; }
        public decimal Totalprice { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalAfterDiscount { get; set; }
        public decimal TotalVat { get; set; }
        public decimal TotalAftetrVat { get; set; }
        public decimal FinalPrice { get; set; }
        public Guid? OrderStatusId { get; set; }

    }
    public class OrdersResDTO : IMapFrom<Orders>
    {
        public Guid Id { get; set; } 
        public int OrderNumber { get; set; }
        public string AppUserId { get; set; }
        public AppUserResDTO AppUser { get; set; }
        public Guid? OffersId { get; set; }
        public OffersResDTO Offers { get; set; }
        public Guid? RestaurantId { get; set; }
        public RestaurantResDTO Restaurant { get; set; }
        public virtual IList<OrderItems> OrderItems { get; set; } = new List<OrderItems>();
        public decimal Totalprice { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalAfterDiscount { get; set; }
        public decimal TotalVat { get; set; }
        public decimal TotalAftetrVat { get; set; }
        public decimal FinalPrice { get; set; }
        public Guid? OrderStatusId { get; set; }
        public OptionSetItemResDTO OrderStatus { get; set; }
    }
}
