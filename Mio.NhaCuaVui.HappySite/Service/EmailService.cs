using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Mio.NhaCuaVui.HappySite.Service
{
    public class EmailService
    {
        
        public async Task<bool> SendEmailAsync(string subject, string body, params string[] receiverAddress)
        {
            try
            {
                string senderAddress = "goidooi.info@gmail.com";
                string password = "goidooi@123";
                MailMessage mail = new MailMessage();
                var displayName = "Gọi đò ơi";
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                //var loginInfo = new NetworkCredential("donotreply.linvn5@gmail.com", "dev@1234");
                var loginInfo = new NetworkCredential(senderAddress, password);
                mail.From = new MailAddress(senderAddress, displayName);
                
                foreach(var ed in receiverAddress)
                {
                    mail.To.Add(ed);
                }    

                mail.Subject = subject;


                mail.IsBodyHtml = true;
                mail.Body = body;

                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = loginInfo;



                smtpClient.Send(mail);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
