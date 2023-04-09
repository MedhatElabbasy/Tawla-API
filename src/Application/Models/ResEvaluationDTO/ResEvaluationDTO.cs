using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.Mappings;
using Tawala.Application.Models.MyAppUserDTO;
using Tawala.Application.Models.ServiceProviderDTO;
using Tawala.Domain.Entities.Identity;
using Tawala.Domain.Entities.Settings.EvaluationEN;
using Tawala.Domain.Entities.Settings.ResEvaluation;
using Tawala.Domain.Entities.Settings.ServiceProvider;

namespace Tawala.Application.Models.ResEvaluationDTO
{
    public class ResEvaluationAddDTO : IMapFrom<ResEvaluation>
    {
        public Guid? RestaurantId { get; set; }
        public int Rate { get; set; }
        public string Notes { get; set; }
        public string AppUserId { get; set; }
    }
    public class ResEvaluationUpdateDTO : IMapFrom<ResEvaluation>
    {
        public Guid Id { get; set; }
        public Guid? RestaurantId { get; set; }
        public int Rate { get; set; }
        public string Notes { get; set; }
        public string AppUserId { get; set; }
    }

    public class ResEvaluationResDTO : IMapFrom<ResEvaluation>
    {
        public Guid Id { get; set; }
        public Guid? RestaurantId { get; set; }
        public RestaurantResDTO Restaurant { get; set; }
        public int Rate { get; set; }
        public string Notes { get; set; }
        public string AppUserId { get; set; }
        public AppUserResDTO AppUser { get; set; }
    }
}
