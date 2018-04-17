using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace ProjektSTI
{
    class Logger
    {

        public void OdesliEmail()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("juklkokr@gmail.com");
                mail.To.Add("juklkokr@gmail.com");
                mail.Subject = "Error";
                mail.Body = "Error";

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("juklkokr", "juklkokr123");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                
                //throw;
            }
        }

    }
}