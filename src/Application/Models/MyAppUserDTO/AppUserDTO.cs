using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.Mappings;
using Tawala.Domain.Entities.Identity;
using Tawala.Domain.Entities.Settings;

namespace Tawala.Application.Models.MyAppUserDTO
{
    public class AppUserDTO : IMapFrom<AppUser>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public string Address { get; set; }
        public string OtherPhoneNumber { get; set; }
        public bool IsRestUser { get; set; }
        public bool IsRestAdmin { get; set; }
        public bool IsAdminUser { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsUser { get; set; }
        public string Email { get; set; }
       // public string UserName { get; set; }
        public string PhoneNumer { get; set; }
        public string Password { get; set; }
        public Guid? PhotoId { get; set; }

    }
    public class AppUserResDTO : IMapFrom<AppUser>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public string Address { get; set; }
        public string OtherPhoneNumber { get; set; }
        public bool IsRestUser { get; set; }
        public bool IsRestAdmin { get; set; }
        public bool IsAdminUser { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsUser { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumer { get; set; }
        public string Password { get; set; }
        public Guid? PhotoId { get; set; }
        public AppAttachment Photo { get; set; }

    }
}
