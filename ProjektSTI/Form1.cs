using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektSTI
{
    public partial class Form1 : Form
    {
        static System.Windows.Forms.Timer casovac = new System.Windows.Forms.Timer();
        static Cas cas = new Cas(5000);

        public Form1()
        {
            InitializeComponent();
            casovac.Tick += new EventHandler(ZpracovaniCasovace);
            //casovac.Interval = 3600000;
            casovac.Interval = 1000;
            casovac.Start();
        }

        private static void ZpracovaniCasovace(Object objekt, EventArgs eventargs)
        {
            cas.OdectiSekunduAktualnihoCasu();
            System.Diagnostics.Debug.WriteLine(cas.VratAktualniCasFormat());
            if (cas.VratAktualniCasMs() == 0)
            {
                new Thread(async () =>
                {
                    Thread.CurrentThread.IsBackground = true;

                    Sluzba s = new Sluzba();
                    var a = await s.VratSouboryCommituPoCaseAsync(DateTime.Now.AddYears(-5));
                //var a = await s.VratPrehledRadkuJazykuRepozitareAsync("java");
                System.Diagnostics.Debug.WriteLine(a.Count);

                }).Start();
            }
            
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Sluzba s = new Sluzba();
            var souboryCommitu = await s.VratSouboryCommituPoCaseAsync(DateTime.Now.AddYears(-5));




        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
