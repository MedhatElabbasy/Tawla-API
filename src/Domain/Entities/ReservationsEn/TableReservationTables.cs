using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common;
using Tawala.Domain.Entities.TawalhDesignWithConfig;

namespace Tawala.Domain.Entities.ReservationsEn
{
    public class TableReservationTables : AuditableEntity, IEntityBase
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? TableReservationId { get; set; }
        public TableReservation TableReservation { get; set; }
        public Guid? RoomTableId { get; set; }
        public RoomTable RoomTable { get; set; }
    }
}
