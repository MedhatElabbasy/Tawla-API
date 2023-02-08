using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.Mappings;
using Tawala.Domain.Entities.Settings.OptionSetsEntities;
using Tawala.Domain.Entities.Settings;
using Tawala.Domain.Entities.TawalhDesignWithConfig;

namespace Tawala.Application.Models.TawalhDesignWithConfigDTO
{
    public class RoomTableAddDTO : IMapFrom<RoomTable>
    {
       
        public string Name { get; set; }
        public string NameEn { get; set; }
        public Guid? RoomId { get; set; } 
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
        //Image list
        public virtual IList<RoomTableImagesAddDTO> RoomTableImages { get; set; } = new List<RoomTableImagesAddDTO>();
        public int TableNumber { get; set; }
        //--Table Type Id
        public Guid? TableTypeId { get; set; }
    
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
