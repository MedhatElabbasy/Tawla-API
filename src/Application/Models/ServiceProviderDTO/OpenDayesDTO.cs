using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.Mappings;
using Tawala.Domain.Entities.Settings.ServiceProvider;

namespace Tawala.Application.Models.ServiceProviderDTO
{
    public class OpenDayesAddDTO : IMapFrom<OpenDayes>
    {
        public int DayNumber { get; set; }
        public TimeSpan OpenAt { get; set; }
        public TimeSpan ClosedAt { get; set; }
        public bool Status { get; set; }
        public string Notes { get; set; }
        public Guid? RestaurantId { get; set; }
    }
    public class OpenDayesUpdateDTO : IMapFrom<OpenDayes>
    {
        public Guid Id { get; set; }
        public int DayNumber { get; set; }
        public TimeSpan OpenAt { get; set; }
        public TimeSpan ClosedAt { get; set; }
        public bool Status { get; set; }
        public string Notes { get; set; }
        public Guid? RestaurantId { get; set; }
    }
    public class OpenDayesResDTO : IMapFrom<OpenDayes>
    {
        public Guid Id { get; set; }
        public int DayNumber { get; set; }
        public TimeSpan OpenAt { get; set; }
        public TimeSpan ClosedAt { get; set; }
        public bool Status { get; set; }
        public string Notes { get; set; }
        public Guid? RestaurantId { get; set; }
        public RestaurantResDTO Restaurant { get; set; }
    }
}
