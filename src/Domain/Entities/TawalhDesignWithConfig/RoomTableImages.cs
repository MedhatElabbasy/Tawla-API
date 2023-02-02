using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common;
using Tawala.Domain.Entities.Settings;

namespace Tawala.Domain.Entities.TawalhDesignWithConfig
{
    public class RoomTableImages : IEntityBase
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? ImageId { get; set; }
        public AppAttachment Image { get; set; }
        public Guid? RoomId { get; set; }
        public RoomTable Room { get; set; }

    }
}
