using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common;
using Tawala.Domain.Entities.Settings.OptionSetsEntities;

namespace Tawala.Domain.Entities.Settings.Other
{
    public class ContactUs : AuditableEntity, IEntityBase
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhoneNumner { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public Guid? StatusId { get; set; }
        public OptionSetItem Status { get; set; }
    }
}
