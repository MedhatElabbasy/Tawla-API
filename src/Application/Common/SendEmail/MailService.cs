using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Security;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;

namespace Tawala.Application.Common.SendEmail
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task SendEmailAsync(MailRequest mailRequest)
        {


            //  MailMessage message = new MailMessage();
            //  message.From = new MailAddress(_mailSettings.Mail);
            //  message.To.Add(mailRequest.ToEmail);
            //  message.Subject = mailRequest.Subject;

            //  SmtpClient smtpClient = new SmtpClient();

            //  smtpClient.Host = _mailSettings.Host;
            //  smtpClient.Port = _mailSettings.Port;
            //  //smtpClient.EnableSsl = true;

            ////  smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            //  smtpClient.Credentials = new System.Net.NetworkCredential(_mailSettings.Mail, _mailSettings.Password);
            //  //ServicePointManager.ServerCertificateValidationCallback =
            //  //delegate (object s, X509Certificate certificate,
            //  //    X509Chain chain, SslPolicyErrors sslPolicyErrors)
            //  //{ return true; };


            //  smtpClient.Send(message);




            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
            message.To.Add(new MailboxAddress("Amr", mailRequest.ToEmail));
            message.Subject = mailRequest.Subject;

            message.Body = new TextPart("plain")
            {
                Text = mailRequest.Body
            };


            //var email = new MimeMessage();
            //email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            //email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            //email.Subject = mailRequest.Subject;

            //email.Headers.Add("Message-Id",
            //            String.Format("<{0}@{1}>",
            //            Guid.NewGuid().ToString(),
            //            mailRequest.ToEmail));
            //var builder = new BodyBuilder();
            //builder.HtmlBody = mailRequest.Body;
            // email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();

            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.SslOnConnect);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            var res = await smtp.SendAsync(message);
            smtp.Disconnect(true);

        }

    }
}
