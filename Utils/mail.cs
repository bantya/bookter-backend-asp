using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;



namespace Utils
{
    public class mail
    {
        public static bool sendmail(string sender, string receiver, string subject, string body)
        {
            bool status = false;
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("we@bookter.in");
                    mail.To.Add("user@example.in");
                    mail.Subject = "Namaste User";
                    mail.Body = "<!DOCTYPE html><html lang='en'><head><meta charset='UTF-8'><meta http-equiv='X-UA-Compatible' content='IE=100' ><meta name='viewport' content='width=device-width, initial-scale=1, shrink-to-fit=no'><title>Bookter</title></head><body><div class='container'><h1>Namaste 🙏🏼, we are Bookter.</h1><h3>We welcome you in our family!</h3><p></p></div></body></html>";
                    mail.IsBodyHtml = true;

                    var smtp = new SmtpClient("smtp.mailtrap.io", 2525)
                    {
                        Credentials = new NetworkCredential("3174eb73d061c3", "f2a3a3d267c284"),
                        EnableSsl = true
                    };

                    smtp.Send(mail);
                    status = true;
                }
            }
            catch (Exception e)
            {
                return status = false;
            }

            return status;
        }
    }
    
}
