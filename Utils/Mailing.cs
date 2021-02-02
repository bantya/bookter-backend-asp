using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class Mailing
    {
        private static string SMTPAddress = "smtp.mailtrap.io";
        private static int SMTPPort = 2525;
        private static string Username = "USERNAME";
        private static string Password = "PASSWORD";

        public static bool Send(string from, string to, string subject, string body)
        {
            bool status = false;

            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(from);
                    mail.To.Add(to);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;

                    var smtp = new SmtpClient(SMTPAddress, SMTPPort)
                    {
                        Credentials = new NetworkCredential(Username, Password),
                        EnableSsl = true
                    };

                    smtp.Send(mail);

                    status = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return status;
        }
    }
}
