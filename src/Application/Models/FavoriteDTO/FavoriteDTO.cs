using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.Mappings;
using Tawala.Application.Models.MyAppUserDTO;
using Tawala.Application.Models.ServiceProviderDTO;
using Tawala.Domain.Entities;
using Tawala.Domain.Entities.Identity;
using Tawala.Domain.Entities.Settings.ServiceProvider;

namespace Tawala.Application.Models.FavoriteDTO
{
    public class FavoriteAddDTO : IMapFrom<Favorite>
    {
        public string AppUserId { get; set; }
        public Guid? RestaurantId { get; set; }
    }
    public class FavoriteUpdateDTO : IMapFrom<Favorite>
    {
        public Guid Id { get; set; }
        public string AppUserId { get; set; }
        public Guid? RestaurantId { get; set; }
    }
    public class FavoriteResDTO : IMapFrom<Favorite>
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string AppUserId { get; set; }
        public AppUserResDTO AppUser { get; set; }
        public Guid? RestaurantId { get; set; }
        public RestaurantResDTO Restaurant { get; set; }
    }
}
