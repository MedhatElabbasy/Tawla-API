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
    public class RoomAddDTO : IMapFrom<Room>
    { 
        public string Name { get; set; }
        public string NameEn { get; set; }
        public Guid? FloorId { get; set; } 
        public string BackGroundColor { get; set; }
        public string SeatBackGroundColor { get; set; }
        public string TableBackGroundColor { get; set; }
        public Guid? DefaultImageId { get; set; } 
        public virtual IList<RoomImagesAddDTO> RoomImages { get; set; } = new List<RoomImagesAddDTO>();
        public decimal width { get; set; }
        public decimal hight { get; set; }
    }
    public class RoomUpdateDTO : IMapFrom<Room>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public Guid? FloorId { get; set; }
        public string BackGroundColor { get; set; }
        public string SeatBackGroundColor { get; set; }
        public string TableBackGroundColor { get; set; }
        public Guid? DefaultImageId { get; set; }
       // public virtual IList<RoomImagesAddDTO> RoomImages { get; set; } = new List<RoomImagesAddDTO>();
        public decimal width { get; set; }
        public decimal hight { get; set; }
    }
    public class RoomResDTO : IMapFrom<Room>
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public string NameEn { get; set; }
        public Guid? FloorId { get; set; }
        public Floor Floor { get; set; }
        public string BackGroundColor { get; set; }
        public string SeatBackGroundColor { get; set; }
        public string TableBackGroundColor { get; set; }
        public Guid? DefaultImageId { get; set; }
        public AppAttachmentResDTO DefaultImage { get; set; }
        public virtual IList<RoomImagesResDTO> RoomImages { get; set; } = new List<RoomImagesResDTO>();
        public decimal width { get; set; }
        public decimal hight { get; set; }
    }
}
