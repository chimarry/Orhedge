
using ServiceLayer.Models;
using System.Threading.Tasks;

namespace ServiceLayer.Common.Interfaces
{
    public interface IEmailSenderService
    {
        /// <summary>
        /// Sends email on specified address with proper design.
        /// </summary>
        /// <param name="sendEmailData">Details about email, such as sender's email, receiver's email...</param>
        /// <returns></returns>
        Task SendTemplateEmailAsync(TemplateEmail sendEmailData);
    }
}
