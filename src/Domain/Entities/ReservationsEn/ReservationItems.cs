using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common;
using Tawala.Domain.Entities.menu;

namespace Tawala.Domain.Entities.ReservationsEn
{
    public class ReservationItems : AuditableEntity, IEntityBase
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
