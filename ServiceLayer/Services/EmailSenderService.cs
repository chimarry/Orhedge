using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using ServiceLayer.Common.Interfaces;
using ServiceLayer.ErrorHandling.Exceptions;
using ServiceLayer.Models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ServiceLayer.Common.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private SendGridClient _client;

        public EmailSenderService(IConfiguration config)
            => _client = new SendGridClient(config["SendGridApiKey"]);

        public async Task SendEmailAsync(SendEmailData sendEmailData)
        {
            EmailAddress fromAddress = new EmailAddress(sendEmailData.From);
            EmailAddress toAddress = new EmailAddress(sendEmailData.To);
            SendGridMessage msg = new SendGridMessage
            {
                From = fromAddress,
                Subject = sendEmailData.Subject
            };
            msg.AddTo(toAddress);
            msg.AddContent(sendEmailData.ContentType, sendEmailData.Message);
            try
            {
                Response response = await _client.SendEmailAsync(msg);
                if (response.StatusCode != HttpStatusCode.Accepted)
                    throw new EmailSenderException(await response.Body.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                // SendGrid did not document what exception is thrown in case of an error
                throw new EmailSenderException("Unable to send email via SendGrid", ex);
            }
        }
    }
}
