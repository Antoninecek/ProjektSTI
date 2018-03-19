using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        static Cas cas = new Cas(10000);
        static Stopwatch sw = new Stopwatch();

        public Form1()
        {
            InitializeComponent();
            casovac.Tick += new EventHandler(ZpracovaniCasovace);
            //casovac.Interval = 3600000;
            casovac.Interval = 1000;
            casovac.Start();
            sw.Restart();
        }

        private static async void ZpracovaniCasovace(Object objekt, EventArgs eventargs)
        {
            Program.form1.richTextBox1.AppendText((Math.Round((Decimal)sw.ElapsedMilliseconds/1000)).ToString() + " sekund \n");
            cas.OdectiSekunduAktualnihoCasu();
            Program.form1.richTextBox1.AppendText(cas.VratAktualniCasFormat() + "\n");
            if (cas.VratAktualniCasMs() == 0)
            {
                Sluzba s = new Sluzba();
                Program.form1.richTextBox2.AppendText("Zpracovavam commity" + "\n");
                var commity = await s.VratSouboryCommituPoCaseAsync(DateTime.Now.AddYears(-5));
                Program.form1.richTextBox2.AppendText("Pocet commitu: " + commity.Count + "\n");
                var jazyky = await s.VratPrehledRadkuJazykuRepozitareAsync("java");
                Program.form1.richTextBox2.AppendText("Pocet radku jazyku: " + jazyky.ToString() + "\n");
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

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
