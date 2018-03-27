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
using System.Web;

namespace ProjektSTI
{
    public partial class MainForm : Form
    {
        static System.Windows.Forms.Timer casovac = new System.Windows.Forms.Timer();
        static int interval = 3600000 - 1000; // -1 sekunda kvůli správné časové synchronizaci
        static Cas cas = new Cas(interval);
        static Stopwatch sw = new Stopwatch();

        static Boolean noveSpusteni = true;
        Boolean openTree = false;
        static int pocetVsechCommitu = -1;
        static ArrayList refList = new ArrayList();

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
                VypisSeznamVsechSouboru(commity);
                Program.MainForm.LogBox.AppendText("Pocet vsech commitu: " + (pocetVsechCommitu+1) + "\n");

                var jazyky = await s.SpocitejPocetRadkuVSouborechUrcitehoTypuAsync("java");
                Program.MainForm.LogBox.AppendText("Pocet radku jazyku Java: " + jazyky.ToString() + "\n\n");
                AktivovatTlacitka(true);
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

        private static void VypisSeznamVsechSouboru(List<File> soubory)
        {
            foreach (File soubor in soubory)
            {
                // Vezme URL a vyjme hodnotu "ref"
                // např.: https://api.github.com/repos/Antoninecek/TEST/contents/JAVASOUBOR.java?ref=4e18d7fe5d58c07b65465002e2ab52869ab3a6c9
                // rf =  "4e18d7fe5d58c07b65465002e2ab52869ab3a6c9";
                String rf = HttpUtility.ParseQueryString(new Uri(soubor.contents_url).Query).Get("ref");
                if (!refList.Contains(rf))
                {
                    pocetVsechCommitu++;
                    refList.Add(rf);
                    Program.MainForm.AllFilesTreeView.Nodes.Add("commit " + pocetVsechCommitu);
                    Program.MainForm.AllFilesTreeView.Nodes[pocetVsechCommitu].Nodes.Add(soubor.filename.ToString());
                } else
                {
                    Program.MainForm.AllFilesTreeView.Nodes[pocetVsechCommitu].Nodes.Add(soubor.filename.ToString());
                }
            }
            Program.MainForm.OtevriZavriVseButton.Enabled = true;
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

        private async void GrafButton_Click(object sender, EventArgs e)
        {
            Form2 GraphForm = new Form2(Program.MainForm.AllFilesTreeView.SelectedNode.Text);
            GraphForm.Text = "Graf " + Program.MainForm.AllFilesTreeView.SelectedNode.Text;
            GraphForm.Show();
            Sluzba sluzba = new Sluzba();

            String selected_file = Program.MainForm.AllFilesTreeView.SelectedNode.Text;
            
            var stat = await sluzba.VratStatistikuZmenyRadkuSouboruAsync(selected_file);
            GraphForm.chart1.Series["Počet přidaných řádků"].Points.Clear();
            stat.Reverse();
            foreach (var commit in stat)
            {
                GraphForm.chart1.Series["Počet přidaných řádků"].Points.AddY(commit.pridane_radky - commit.odebrane_radky);
            };
        }

        private void OtevriZavriVseButton_Click(object sender, EventArgs e)
        {
            if (openTree)
            {
                Program.MainForm.AllFilesTreeView.CollapseAll();
                openTree = false;
            } else
            {
                Program.MainForm.AllFilesTreeView.ExpandAll();
                openTree = true;
            }

            
        }

        private void AllFilesTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (Program.MainForm.AllFilesTreeView.SelectedNode.Text.EndsWith(".java"))
            {
                Program.MainForm.GrafButton.Enabled = true;
            } else
            {
                Program.MainForm.GrafButton.Enabled = false;
            }
            
        }
    }
}
