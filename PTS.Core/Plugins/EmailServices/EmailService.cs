using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using PTS.Core.Messages;
using PTS.Core.Utilities.Results.Result;

namespace PTS.Core.Plugins.EmailServices
{
    public class EmailService : IEmailService
    {
        public async Task<IResponse> SendMailAsync(string email, EmailSetting template, List<string> cc = null)
        {

            var message = new MailMessage(template.EmailOptions.UserName, email)
            {
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = true,
                Subject = template.Subject,
                Body = template.Body
            };
            if (cc != null)
            {
                foreach (var ccMail in cc)
                {
                    message.CC.Add(ccMail);
                }
            }
            var info = new NetworkCredential(template.EmailOptions.UserName, template.EmailOptions.Password);
            var mailClient = new SmtpClient(template.EmailOptions.SmtpServer, template.EmailOptions.Port)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = info,
                EnableSsl = template.EmailOptions.EnableSsl
            };
            mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                mailClient.SendCompleted += (s, e) =>
                {
                    mailClient.Dispose();
                    message.Dispose();
                };
                await mailClient.SendMailAsync(message);
                return new SuccessResponse(EmailMessage.SentSuccessfully);
            }
            catch (Exception e)
            {
                return new ErrorResponse(e.Message);
            }
        }
    }
}
