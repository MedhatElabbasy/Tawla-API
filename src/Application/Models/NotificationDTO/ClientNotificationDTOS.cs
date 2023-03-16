using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.DateTimeCalculation;
using Tawala.Application.Common.Mappings;
using Tawala.Application.Models.Common;
using Tawala.Domain.Entities.Notifications;
using Tawala.Domain.Entities.Settings;

namespace Tawala.Application.Models.NotificationDTO
{
    public class ClientNotificationAddDTOS : IMapFrom<ClientNotification>
    {
        public bool IsApprovedByAdmin { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TitleEn { get; set; }
        public string DescriptionEn { get; set; }
        public Guid? NotificationPhotoId { get; set; }

    }
    public class ClientNotificationUpdateDTOS : IMapFrom<ClientNotification>
    {
        public bool IsApprovedByAdmin { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TitleEn { get; set; }
        public string DescriptionEn { get; set; }
        public Guid? NotificationPhotoId { get; set; }

    }

    public class ClientNotificationResDTOS : IMapFrom<ClientNotification>
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsApprovedByAdmin { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TitleEn { get; set; }
        public string DescriptionEn { get; set; }
        public Guid? NotificationPhotoId { get; set; }
        public AppAttachmentResDTO NotificationPhoto { get; set; }
        public DateTime Created { get; set; }
        public string CreatedDateTime { get { return Created.ToString("MM dddd yyyy"); } }
        public string CreatedSinceTime { get { return Created.ToRelativeDateString(true); } }
    }
}
