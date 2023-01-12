using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common;
using Tawala.Domain.Entities.Identity;
using Tawala.Domain.Entities.Settings.OptionSetsEntities;
using Tawala.Domain.Entities.Settings.ServiceProvider;

namespace Tawala.Domain.Entities.Settings.Other
{
    public class Complain : AuditableEntity, IEntityBase
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string ComplainDescription { get; set; }
        public string UserPhoneNumber { get; set; }
        public string Email { get; set; }
        public Guid? ComplainTypeId { get; set; }
        public OptionSetItem ComplainType { get; set; }
        public string ComplainUserId { get; set; }
        public AppUser ComplainUser { get; set; }
        public Guid? ComplainStatusId { get; set; }
        public OptionSetItem ComplainStatus { get; set; }
        public Guid? RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        public Guid? BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}
