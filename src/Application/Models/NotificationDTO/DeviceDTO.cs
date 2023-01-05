using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.Mappings;
using Tawala.Domain.Common;
using Tawala.Domain.Entities.Notifications;

namespace Tawala.Application.Models.NotificationDTO
{
    public class DeviceAddDTO : IMapFrom<Device>
    {
        public string UserId { get; set; }
        public string DeviceToken { get; set; }
    }
    public class DeviceUpdateDTO : IMapFrom<Device>
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string DeviceToken { get; set; }
    }
    public class DeviceResDTO : IMapFrom<Device>
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string DeviceToken { get; set; }
    }
}
