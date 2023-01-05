using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tawala.Application.Common.NotificationService
{
    public interface INotificationService
    {
        Task<bool> NotifyAsync(string to, string title, string body);
    }
}
