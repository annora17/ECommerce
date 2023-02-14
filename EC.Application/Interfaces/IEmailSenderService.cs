using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EC.Application.Interfaces
{
    public interface IEmailSenderService
    {
        Task<bool> SendEmailAsync(string userEmail, string userMessage, bool isHtmlContent = false);
        Task<bool> SendPasswordRecoveryAsync(string userEmail, string callbackUrl);
    }
}
