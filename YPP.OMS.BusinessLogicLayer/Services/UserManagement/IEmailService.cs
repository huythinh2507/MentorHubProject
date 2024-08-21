using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPP.MH.BusinessLogicLayer.Services.UserManagement
{
    public interface IEmailService
    {
        Task SendConfirmationEmailAsync(string email, string code);
        Task SendPasswordResetEmailAsync(string email, string resetLink);
    }
}
