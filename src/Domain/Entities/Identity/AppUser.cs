using Tawala.Domain.Common;
using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;
using Tawala.Domain.Entities.Settings;
using System;
using System.Net.Mail;
using Tawala.Domain.Entities.Settings.ServiceProvider;

namespace Tawala.Domain.Entities.Identity
{
    public class AppUser : IdentityUser
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
        public string PhoneNumer { get; set; }
        public Guid? PhotoId { get; set; }
        public AppAttachment Photo { get; set; }
        //---
        public Guid? UserRestaurantId { get; set; }
         

    }
}
