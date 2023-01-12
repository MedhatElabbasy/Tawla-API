using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common;
using Tawala.Domain.Entities.Identity;
using Tawala.Domain.Entities.Settings.AdminSettings;
using Tawala.Domain.Entities.Settings.OptionSetsEntities;

namespace Tawala.Domain.Entities.Settings.ServiceProvider
{
    public class Restaurant : AuditableEntity, IEntityBase
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string OwnerUserId { get; set; }
        public AppUser OwnerUser { get; set; }
        public string ResturantName { get; set; }
        public string ResturantNameEn { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumber2 { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string AddressDescription { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string Description { get; set; }
        public int NumberOfBranches { get; set; }
        public Guid? RestaurantTypeId { get; set; }
        public OptionSetItem RestaurantType { get; set; }
        public Guid? LogoId { get; set; }
        public AppAttachment Logo { get; set; }
        public Guid? AppBanerId { get; set; }
        public AppAttachment AppBaner { get; set; }
        public Guid? CityId { get; set; }
        public City City { get; set; }
        public Guid? RegionsId { get; set; }
        public Regions Regions { get; set; }
        public Guid? DistrictId { get; set; }
        public District District { get; set; }
        public string TickTok { get; set; }
        public string Snap { get; set; }
        public string Twiter { get; set; }
        public string Instgram { get; set; }
        public string Facebook { get; set; }
        public bool IsActive { get; set; }
        public bool IsRejected { get; set; }
        public string RejectedReson { get; set; }
        public virtual IList<Branch> Branchs { get; set; } = new List<Branch>();
        public virtual IList<OpenDayes> OpenDayes { get; set; } = new List<OpenDayes>();
    }
}
