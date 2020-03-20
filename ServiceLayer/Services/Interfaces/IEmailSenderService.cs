
using ServiceLayer.Models;
using System.Threading.Tasks;

namespace ServiceLayer.Common.Interfaces
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(SendEmailData sendEmailData);
    }
}
