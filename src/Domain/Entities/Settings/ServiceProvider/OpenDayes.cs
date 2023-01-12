using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common;

namespace Tawala.Domain.Entities.Settings.ServiceProvider
{
    public class OpenDayes : IEntityBase
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public int DayNumber { get; set; }
        public TimeSpan OpenAt { get; set; }
        public TimeSpan ClosedAt { get; set; }
        public bool Status { get; set; }
        public string Notes { get; set; }
        public Guid? RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
