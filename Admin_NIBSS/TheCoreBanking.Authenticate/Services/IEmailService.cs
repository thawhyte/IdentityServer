using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheCoreBanking.Authenticate.Services
{
    public interface IEmailService
    {
        Task SendEmail(string email, string subject, string message);
    }
}
