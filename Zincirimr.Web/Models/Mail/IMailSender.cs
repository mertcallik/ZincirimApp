using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zincirimr.Web.Models.Mail
{
    public interface IMailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
