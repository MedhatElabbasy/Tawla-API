using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common;
using Tawala.Domain.Entities.Settings;

namespace Tawala.Domain.Entities.Notifications
{
    public class ClientNotification : AuditableEntity, IEntityBase
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsApprovedByAdmin { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TitleEn { get; set; }
        public string DescriptionEn { get; set; }
        public Guid? NotificationPhotoId { get; set; }
        public AppAttachment NotificationPhoto { get; set; }
    }
}
