using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.Mappings;
using Tawala.Domain.Entities.menu;
using Tawala.Domain.Entities.ReservationsEn;

namespace Tawala.Application.Models.ReservationsDTO
{
    public class ReservationItemsAddDTO : IMapFrom<ReservationItems> 
    { 
        public Guid? TableReservationId { get; set; } 
        public Guid? ItemsId { get; set; } 
        public string Notes { get; set; }
    }
    public class ReservationItemsUpdateDTO : IMapFrom<ReservationItems>
    {
        public Guid Id { get; set; } 
        public Guid? TableReservationId { get; set; } 
        public Guid? ItemsId { get; set; } 
        public string Notes { get; set; }
    }
    public class ReservationItemsResDTO : IMapFrom<ReservationItems>
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? TableReservationId { get; set; }
        public TableReservation TableReservation { get; set; }
        public Guid? ItemsId { get; set; }
        public Items Items { get; set; }
        public string Notes { get; set; }
    }
}
