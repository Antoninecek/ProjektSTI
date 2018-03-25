using Newtonsoft.Json;
using System;
using System.Collections;
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
    public partial class MainForm : Form
    {
        static System.Windows.Forms.Timer casovac = new System.Windows.Forms.Timer();
        static int interval = 3600000 - 1000; // -1 sekunda kvůli správné časové synchronizaci
        static Cas cas = new Cas(interval);
        static Stopwatch sw = new Stopwatch();

        static Boolean noveSpusteni = true;

        public MainForm()
        {
            InitializeComponent();
            casovac.Tick += new EventHandler(ZpracovaniCasovace);
            casovac.Interval = 1000;
            casovac.Start();

            LogBox.SelectAll();
            LogBox.SelectionAlignment = HorizontalAlignment.Center;

            sw.Restart();
        }

        private static async void ZpracovaniCasovace(Object objekt, EventArgs eventargs)
        {
            cas.OdectiSekunduAktualnihoCasu();
            Program.MainForm.TimeShower.Text = "Další kontrola za: " + cas.VratAktualniCasFormat();
            ArrayList vsechnySoubory = new ArrayList();
            ArrayList zmeneneSoubory = new ArrayList();
            NastavKontrolkuInternetu();

            if (noveSpusteni == true)
            {
                noveSpusteni = false;
                Sluzba s = new Sluzba();
                
                Program.MainForm.LogBox.AppendText("---------------- " + DateTime.Now.ToString("hh:mm:ss tt") + " ----------------" + "\n");
                Program.MainForm.LogBox.AppendText("Zpracovavam commity..." + "\n");
                var commity = await s.VratSouboryCommituPoCaseAsync(DateTime.Now.AddYears(-5));
                Program.MainForm.LogBox.AppendText("Pocet commitu: " + commity.Count + "\n");

                foreach (File commit in commity)
                {
                    if (!vsechnySoubory.Contains(commit.filename.ToString()))
                    {
                        vsechnySoubory.Add(commit.filename.ToString());
                    }
                }
                vsechnySoubory.Sort();
                VypisSeznamVsechSouboru(vsechnySoubory);

                var jazyky = await s.SpocitejPocetRadkuVSouborechUrcitehoTypuAsync("java");
                Program.MainForm.LogBox.AppendText("Pocet radku jazyku Java: " + jazyky.ToString() + "\n\n");
                AktivovatTlacitka(true);
                Program.MainForm.button1.Enabled = true;
            }
            if (cas.VratAktualniCasMs() == 0)
            {
                AktivovatTlacitka(false);
                Sluzba s = new Sluzba();
                Program.MainForm.LogBox.AppendText("---------------- " + DateTime.Now.ToString("hh:mm:ss tt") + " ----------------" + "\n");
                Program.MainForm.LogBox.AppendText("Zpracovavam commity..." + "\n");
                var commity = await s.VratSouboryCommituPoCaseAsync(DateTime.Now.AddMilliseconds(-1*(interval-(int)cas.VratAktualniCasMs())));
                //var commity = await s.VratSouboryCommituPoCaseAsync(DateTime.Now.AddDays(-7)); // Pro testovací účely
                Program.MainForm.LogBox.AppendText("Pocet commitu: " + commity.Count + "\n");
                var jazyky = await s.SpocitejPocetRadkuVSouborechUrcitehoTypuAsync("java");
                Program.MainForm.LogBox.AppendText("Pocet radku jazyku Java: " + jazyky.ToString() + "\n\n");
                if (commity.Count > 0){
                    foreach (File commit in commity)
                    {
                        if (!zmeneneSoubory.Contains(commit.filename.ToString()))
                        {
                            zmeneneSoubory.Add(commit.filename.ToString());
                        }
                    }
                    VypisSeznamZmenenychSouboru(zmeneneSoubory);
                    zmeneneSoubory.Clear();
                }
                

                AktivovatTlacitka(true);
            }
            

        }

        private static void NastavKontrolkuInternetu()
        {
            if (ZkouskaInternetovehoPripojeni())
            {
                Program.MainForm.pictureBox1.BackColor = Color.Green;
            } else
            {
                Program.MainForm.pictureBox1.BackColor = Color.Red;
            }
        }

        private static void AktivovatTlacitka(Boolean b)
        {
            Program.MainForm.RefreshButton.Enabled = b;
            Program.MainForm.ClearLogBoxButton.Enabled = b;
        }

        private static void VypisSeznamVsechSouboru(ArrayList soubory)
        {
            //Program.MainForm.AllFilesBox.Clear();
            
            foreach (String soubor in soubory)
            {
                //Program.MainForm.AllFilesBox.AppendText(soubor + "\n");
                Program.MainForm.AllFilesTreeView.Nodes.Add(soubor);
            }
        }

        private static void VypisSeznamZmenenychSouboru(ArrayList soubory)
        {
            foreach (String soubor in soubory)
            {
                Program.MainForm.ChangedFilesBox.AppendText(soubor + "\n");
            }
        }

        private static bool ZkouskaInternetovehoPripojeni()
        {
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    using (client.OpenRead("http://clients3.google.com/generate_204"))
                    {
                        Console.WriteLine("Internet pripojen");
                        return true;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Internet nepripojen");
                return false;
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            AktivovatTlacitka(false);
            while (cas.VratAktualniCasMs() != 1000)
            {
                cas.OdectiSekunduAktualnihoCasu();
            }

        }

        private void ClearLogBoxButton_Click(object sender, EventArgs e)
        {
            Program.MainForm.LogBox.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 GraphForm = new Form2(Program.MainForm.AllFilesTreeView.SelectedNode.Text);
            GraphForm.Show();
        }

    }
}
