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
<<<<<<< HEAD
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
                
=======
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("mail_from");
                mail.To.Add("mail_to");
                mail.Subject = "Test Mail";
                mail.Body = "Ondrášek si poslal mail ze C#";

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("login", "password");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
>>>>>>> 2844fcd11fe8d5e10b21e26431200142bff7e84f
            }
            catch (Exception ex)
            {
                
                //throw;
            }
        }

    }
}
