using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common;
using Tawala.Domain.Entities.Settings;
using Tawala.Domain.Entities.Settings.OptionSetsEntities;

namespace Tawala.Domain.Entities.TawalhDesignWithConfig
{
    public class RoomTable : AuditableEntity, IEntityBase
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
        //width
        public decimal Width { get; set; }
        public decimal Hight { get; set; }
        //image
        public Guid? DefaultImageId { get; set; }
        public AppAttachment DefaultImage { get; set; }
        //
        public virtual IList<RoomTableImages> RoomTableImages { get; set; } = new List<RoomTableImages>();
        public int TableNumber { get; set; }
        //--Table Type Id
        public Guid? TableTypeId { get; set; }
        public OptionSetItem TableType { get; set; }
    }
}
