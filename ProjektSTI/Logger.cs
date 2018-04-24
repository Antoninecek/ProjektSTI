using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.IO;
using Newtonsoft.Json;

namespace ProjektSTI
{
    class Logger
    {
        public string zprava { get; set; }

        public Logger(string zprava)
        {
            this.zprava = zprava;
        }

        public void Loguj()
        {
            string zaznam = VytvorZaznam();
            OdesliEmail(zaznam);
            ZapisLog(zaznam);
        }

        public string VytvorZaznam()
        {
            return DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " " + this.zprava;
        }

        public void OdesliEmail(string zprava)
        {
            

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

<<<<<<< HEAD
                mail.From = new MailAddress("mail_from");
                mail.To.Add("mail_to");
                mail.Subject = "BUG/CHYBA/FAIL";
                mail.Body = "Moji mili developeri, to jsem ja, vas softicek a prave jsem se posral. Potrebuju prebalit. Tady je zaznam - " + zprava;

                SmtpServer.Port = 587;

                Nastaveni n = null;
                try
                {
                    var txt = System.IO.File.ReadAllText(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + "\\configa.json");
                    n = JsonConvert.DeserializeObject<Nastaveni>(txt);
                }
                catch (Exception ex)
                {
                    Logger lg = new Logger(ex.Message);
                    lg.ZapisLog(lg.VytvorZaznam());
                }
                SmtpServer.Credentials = new System.Net.NetworkCredential(n.Email_Jmeno, n.Email_Heslo);
=======
                mail.From = new MailAddress("juklkokr@gmail.com");
                mail.To.Add("juklkokr@gmail.com");
                mail.Subject = "Error";
                mail.Body = "Error";

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("juklkokr", "juklkokr123");
>>>>>>> 116a916500dbf2ff400bf43d34379a25a6ca0e42
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception)
            {
                
                //throw;
            }
        }

        public void ZapisLog(string zprava)
        {
            try
            {
                using (StreamWriter w = System.IO.File.AppendText(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + "\\log.txt"))
                {
                    w.Write(zprava + "\r\n");
                }
            }
            catch (Exception)
            {

            }
        }

    }
}