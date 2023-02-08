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
    public class RoomTableImagesAddDTO : IMapFrom<RoomTableImages>
    {
        public Guid? ImageId { get; set; }
        public Guid? RoomId { get; set; }
    }
    public class RoomTableImagesResDTO : IMapFrom<RoomTableImages>
    {
        public Guid Id { get; set; } 
        public Guid? ImageId { get; set; }
        public AppAttachmentResDTO Image { get; set; }
        public Guid? RoomId { get; set; }
        public RoomTableResDTO Room { get; set; }
    }
}
