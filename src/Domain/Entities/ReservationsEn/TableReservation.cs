using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common;
using Tawala.Domain.Entities.Identity;
using Tawala.Domain.Entities.Settings.ServiceProvider;

namespace Tawala.Domain.Entities.ReservationsEn
{
    public class TableReservation : AuditableEntity, IEntityBase
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid? RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        //Table
        public virtual IList<TableReservationTables> TableReservationTables { get; set; } = new List<TableReservationTables>();
    }
}
