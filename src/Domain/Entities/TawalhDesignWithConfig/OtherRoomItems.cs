using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common;
using Tawala.Domain.Entities.Settings;

namespace Tawala.Domain.Entities.TawalhDesignWithConfig
{
    public class OtherRoomItems : AuditableEntity, IEntityBase
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public Room RoomId { get; set; }
        public Room Room { get; set; }
        //Position
        public decimal PositionLeft { get; set; }
        public decimal PositionRight { get; set; }
        public decimal PositionTop { get; set; }
        public decimal PositionBottom { get; set; }
        //Width && Hight
        public decimal Width { get; set; }
        public decimal Hight { get; set; }
        //Image
        public Guid? ImageId { get; set; }
        public AppAttachment Image { get; set; }
        //------------Json Data
        public string JsonData { get; set; }
    }
}
