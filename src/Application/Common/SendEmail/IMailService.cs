using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tawala.Application.Common.SendEmail
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
