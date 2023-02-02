using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common;
using Tawala.Domain.Entities.Settings;
using Tawala.Domain.Entities.Settings.ServiceProvider;

namespace Tawala.Domain.Entities.TawalhDesignWithConfig
{
    public class Room : AuditableEntity, IEntityBase
    {
        public string Name { get; set; }
        public string NameEn { get; set; }
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? FloorId { get; set; }
        public Floor Floor { get; set; }
        public string BackGroundColor { get; set; }
        public string SeatBackGroundColor { get; set; }
        public string TableBackGroundColor { get; set; }
        public Guid? DefaultImageId { get; set; }
        public AppAttachment DefaultImage { get; set; }
        public virtual IList<RoomImages> RoomImages { get; set; } = new List<RoomImages>();
        public decimal width { get; set; }
        public decimal hight { get; set; }
    }
}
