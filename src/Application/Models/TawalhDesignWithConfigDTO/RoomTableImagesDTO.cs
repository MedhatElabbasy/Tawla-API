using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.Mappings;
using Tawala.Domain.Entities.Settings;
using Tawala.Domain.Entities.TawalhDesignWithConfig;

namespace Tawala.Application.Models.TawalhDesignWithConfigDTO
{
    public class RoomTableImagesAddDTO : IMapFrom<RoomTableImages>
    {
        public Guid? ImageId { get; set; }
        public Guid? RoomId { get; set; }
    }
}
