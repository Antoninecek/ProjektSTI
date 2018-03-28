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
            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential("frantisek.jukl@gmail.com", "pass");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;

            try
            {
                MailAddress
                    maFrom = new MailAddress("sender_email@domain.tld", "Sender's Name", Encoding.UTF8),
                    maTo = new MailAddress("frantisek.jukl@gmail.com", "Recipient's Name", Encoding.UTF8);
                MailMessage mmsg = new MailMessage(maFrom.Address, maTo.Address);
                mmsg.Body = "<html><body><h1>Some HTML Text for Test as BODY</h1></body></html>";
                mmsg.BodyEncoding = Encoding.UTF8;
                mmsg.IsBodyHtml = true;
                mmsg.Subject = "Some Other Text as Subject";
                mmsg.SubjectEncoding = Encoding.UTF8;

                client.Send(mmsg);
                
            }
            catch (Exception ex)
            {
                
                //throw;
            }
        }

    }
}
