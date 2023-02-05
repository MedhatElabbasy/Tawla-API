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
        //Width && Hight
        public decimal Width { get; set; }
        public decimal Hight { get; set; }
        //Image
        public Guid? DefaultImageId { get; set; }
        public AppAttachment DefaultImage { get; set; }
        //Image list
        public virtual IList<RoomTableImages> RoomTableImages { get; set; } = new List<RoomTableImages>();
        public int TableNumber { get; set; }
        //--Table Type Id
        public Guid? TableTypeId { get; set; }
        public OptionSetItem TableType { get; set; }
        //-----------Days------
        public bool IsFriday { get; set; }
        public bool IsMonday { get; set; }
        public bool IsSaturday { get; set; }
        public bool IsSunday { get; set; }
        public bool IsThursday { get; set; }
        public bool IsTuesday { get; set; }
        public bool IsWednesday { get; set; }
        //---Table Avilabllity
        public TimeSpan StartAt { get; set; }
        public TimeSpan EndAt { get; set; }
        //For other custome model
        public string JsonModel { get; set; }
        //Table Setting 
        public int NumberOfSet { get; set; }
        public int MaxNumberOfSet { get; set; }
        public int DefaultNumberOfSet { get; set; }
        //-------------Price-----
        public decimal VipPricePerSet { get; set; }
        public decimal VipPricePerTable { get; set; }
        public decimal StandardPricePerSet { get; set; }
        public decimal StandardPricePerTable { get; set; }

    }
}
