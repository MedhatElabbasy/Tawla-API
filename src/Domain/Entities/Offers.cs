using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common;
using Tawala.Domain.Entities.Settings;

namespace Tawala.Domain.Entities
{
    public class Offers : AuditableEntity, IEntityBase
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Code { get; set; }
        public string title { get; set; }
        public string Description { get; set; }
        public OfferType OfferType { get; set; }
        public decimal Amount { get; set; }
        public decimal Percent { get; set; }
        public decimal MustExceed { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid? PhotoId { get; set; }
        public AppAttachment Photo { get; set; }
    }

    public enum OfferType
    {
        Amount = 1,
        Percent = 2,
    }
}
