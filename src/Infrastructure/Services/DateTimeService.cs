using Tawala.Infrastructure.Common.Interfaces;
using System;

namespace Tawala.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
