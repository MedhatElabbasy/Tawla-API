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
        public string ClientId { get; set; }
        public AppUser Client { get; set; }
        public Guid? RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        //Table
        public virtual IList<TableReservationTables> TableReservationTables { get; set; } = new List<TableReservationTables>();
        //selected Itrems
        public virtual IList<ReservationItems> ReservationItems { get; set; } = new List<ReservationItems>();
        //----------date
        public DateTime ReservationDate { get; set; }

        public DateTime ReservationStartAt { get; set; }
        public DateTime ReservationStartEndAt { get; set; }
        //--notes
        public string Notes { get; set; }
        //----person number
        public int NumberOfperson { get; set; }
        //price List
        public decimal Totalprice { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalAfterDiscount { get; set; }
        public decimal TotalVat { get; set; }
        public decimal TotalAftetrVat { get; set; }
        public decimal FinalPrice { get; set; }

    }
}
