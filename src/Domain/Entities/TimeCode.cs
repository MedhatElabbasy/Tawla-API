using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common; 
using Tawala.Domain.Entities.Identity;

namespace Tawala.Domain.Entities.Identity
{
    public class TempCode : IEntityBase
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiredAt { get; set; }
        public string TempCodeHash { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

    }
}
