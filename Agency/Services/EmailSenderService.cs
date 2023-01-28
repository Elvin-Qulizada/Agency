using System.Net.Mail;

namespace Agency.Services
{
    public class EmailSenderService
    {
        private readonly IConfiguration _configuration;

        public EmailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Send(string to,string subject,string html)
        {
            MailMessage newMail = new MailMessage();
            SmtpClient client = new SmtpClient(_configuration.GetValue<string>("Mail:Smtp"));
            newMail.From = new MailAddress(_configuration.GetValue<string>("Mail:UserMail"), _configuration.GetValue<string>("Mail:UserName"));
            newMail.To.Add(to);
            newMail.Subject = subject; 
            newMail.IsBodyHtml = true; newMail.Body = html;
            client.EnableSsl = true;
            client.Port = _configuration.GetValue<int>("Mail:Port");
            client.Credentials = new System.Net.NetworkCredential(_configuration.GetValue<string>("Mail:UserMail"), _configuration.GetValue<string>("Mail:UserPassword"));
            client.Send(newMail);
        }
    }
}
