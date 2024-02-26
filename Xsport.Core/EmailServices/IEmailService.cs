using Xsport.Core.EmailServices.Models;

namespace Xsport.Core.EmailServices
{
    public interface IEmailService
    {
        void SendEmail(Message message);
        Task SendEmailAsync(Message message);
    }
}
