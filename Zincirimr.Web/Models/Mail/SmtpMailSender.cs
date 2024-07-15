using System.Net;
using System.Net.Mail;

namespace Zincirimr.Web.Models.Mail
{
    public class SmtpMailSender:IMailSender
    {

        private readonly string _server;
        private readonly int _port;
        private readonly string _login;
        private readonly string _password;
        private readonly bool _ssl;
        private readonly string _sender;

        public SmtpMailSender(string server, int port, string login, string password, bool ssl, string sender)
        {
            _server = server;
            _port = port;
            _login = login;
            _password = password;
            _ssl = ssl;
            _sender = sender;
        }


        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient(_server, _port)
            {
                Credentials = new NetworkCredential(_login, _password),
                EnableSsl = _ssl
            };
            return client.SendMailAsync(new MailMessage(_sender, email, subject, message) { IsBodyHtml = true });

        }
    }
}
