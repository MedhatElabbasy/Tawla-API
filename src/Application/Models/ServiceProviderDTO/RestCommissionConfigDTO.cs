using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.Mappings;
using Tawala.Domain.Entities.Settings;

namespace Tawala.Application.Models.ServiceProviderDTO
{
    public class RestCommissionConfigAddDTO : IMapFrom<RestCommissionConfig>
    {
        public Guid? RestaurantId { get; set; }
        public bool IsAmount { get; set; }
        public bool IsPercent { get; set; }
        public decimal Percent { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
    }

    public class RestCommissionConfigUpdateDTO : IMapFrom<RestCommissionConfig>
    {
        public Guid Id { get; set; }
        public Guid? RestaurantId { get; set; }
        public bool IsAmount { get; set; }
        public bool IsPercent { get; set; }
        public decimal Percent { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
    }

    public class RestCommissionConfigResDTO : IMapFrom<RestCommissionConfig>
    {
        public Guid Id { get; set; }
        public Guid? RestaurantId { get; set; }
        public RestaurantResDTO Restaurant { get; set; }
        public bool IsAmount { get; set; }
        public bool IsPercent { get; set; }
        public decimal Percent { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
    }

}
