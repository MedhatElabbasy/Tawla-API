using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.Mappings;
using Tawala.Application.Models.Common;
using Tawala.Domain.Entities.Settings;
using Tawala.Domain.Entities.TawalhDesignWithConfig;

namespace Tawala.Application.Models.TawalhDesignWithConfigDTO
{
    public class RoomImagesAddDTO : IMapFrom<RoomImages>
    { 
        public Guid? ImageId { get; set; } 
        public Guid? RoomId { get; set; } 
    }
    public class RoomImagesResDTO : IMapFrom<RoomImages>
    {
        public Guid Id { get; set; } 
        public Guid? ImageId { get; set; }
        public AppAttachmentResDTO Image { get; set; }
        public Guid? RoomId { get; set; }
        public RoomResDTO Room { get; set; }
    }
}
