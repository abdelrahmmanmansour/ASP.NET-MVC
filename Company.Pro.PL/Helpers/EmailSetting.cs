using Microsoft.Identity.Client;
using System.Net;
using System.Net.Mail;

namespace Company.Pro.PL.Helpers
{
    public static class EmailSetting
    {
        public static bool SendEmail(Email email)
        {
            // Send Email Logic Here:
            // Mail Server : Gmail
            // Protocol : SMTP => Simple Mail Transfer Protocol
            // Port : 587 TLS
            // Password : txqrvgfepoajjbii

            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587); // Gmail SMTP Server
                client.EnableSsl = true; // To Encrypt Data
                client.Credentials = new NetworkCredential("abdo.mansour741@gmail.com", "txqrvgfepoajjbii"); // Sender Email + Password
                client.Send("abdo.mansour741@gmail.com", email.TO, email.Subject, email.Body);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
