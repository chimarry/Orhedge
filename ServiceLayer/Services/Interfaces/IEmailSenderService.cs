
using ServiceLayer.Models;
using System.Threading.Tasks;

namespace ServiceLayer.Common.Interfaces
{
    public interface IEmailSenderService
    {
        Task SendTemplateEmailAsync(TemplateEmail sendEmailData);
    }
}
