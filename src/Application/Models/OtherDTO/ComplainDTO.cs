using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.Mappings;
using Tawala.Application.Models.MyAppUserDTO;
using Tawala.Application.Models.ServiceProviderDTO;
using Tawala.Application.Models.SettingDTO.OptionSetDTOS;
using Tawala.Domain.Entities.Identity;
using Tawala.Domain.Entities.Settings.OptionSetsEntities;
using Tawala.Domain.Entities.Settings.Other;
using Tawala.Domain.Entities.Settings.ServiceProvider;

namespace Tawala.Application.Models.OtherDTO
{
    public class ComplainAddDTO : IMapFrom<Complain>
    {
        public string ComplainDescription { get; set; }
        public string UserPhoneNumber { get; set; }
        public string Email { get; set; }
        public Guid? ComplainTypeId { get; set; }
        public string ComplainUserId { get; set; }
        public Guid? ComplainStatusId { get; set; }
        public Guid? RestaurantId { get; set; }
        public Guid? BranchId { get; set; }
    }
    public class ComplainUpdateDTO : IMapFrom<Complain>
    {
        public Guid Id { get; set; }
        public string ComplainDescription { get; set; }
        public string UserPhoneNumber { get; set; }
        public string Email { get; set; }
        public Guid? ComplainTypeId { get; set; }
        public string ComplainUserId { get; set; }
        public Guid? ComplainStatusId { get; set; }
        public Guid? RestaurantId { get; set; }
        public Guid? BranchId { get; set; }
    }
    public class ComplainResDTO : IMapFrom<Complain>
    {
        public Guid Id { get; set; }
        public string ComplainDescription { get; set; }
        public string UserPhoneNumber { get; set; }
        public string Email { get; set; }
        public Guid? ComplainTypeId { get; set; }
        public OptionSetItemResDTO ComplainType { get; set; }
        public string ComplainUserId { get; set; }
        public AppUserResDTO ComplainUser { get; set; }
        public Guid? ComplainStatusId { get; set; }
        public OptionSetItemResDTO ComplainStatus { get; set; }
        public Guid? RestaurantId { get; set; }
        public RestaurantResDTO Restaurant { get; set; }
        public Guid? BranchId { get; set; }
        public BranchResDTO Branch { get; set; }
    }
}
