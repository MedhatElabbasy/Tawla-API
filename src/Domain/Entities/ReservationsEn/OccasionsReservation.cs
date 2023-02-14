using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common;
using Tawala.Domain.Entities.Identity;
using Tawala.Domain.Entities.Settings.OptionSetsEntities;
using Tawala.Domain.Entities.Settings.ServiceProvider;

namespace Tawala.Domain.Entities.Reservations
{
    public class OccasionsReservation : AuditableEntity, IEntityBase
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid RestOccasionsId { get; set; }
        public RestOccasions RestOccasions { get; set; }
        public Guid? BranchId { get; set; }
        public Branch Branch { get; set; }
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
        public DateTime OccasionDate { get; set; }
        public string Address { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string AddressDetails { get; set; } 
        public Guid? StatusId { get; set; }
        public OptionSetItem Status { get; set; }
    }
}
