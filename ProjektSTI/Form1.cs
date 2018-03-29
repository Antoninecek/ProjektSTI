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
        //static int interval = 30000 - 1000; // PRO TESTOVANI
        static Cas cas = new Cas(interval);
        static Stopwatch sw = new Stopwatch();

        // vytvořeno speciálně pro nové spuštění aplikace, po prvním vrácení commitů se nastaví na false
        static Boolean noveSpusteni = true;

        // uchování hodnoty pro otevřený/zavřený strom souborů v GUI
        static Boolean openTree = false;

        // počet všech commitů - pro správné indexování
        static int pocetVsechCommitu = 0;

        // hodnota, která oznamuje, zda je v průběhu vrácení commitů - pro správné nastavení tlačítek a zobrazení GUI
        static Boolean pracuji = false;

        // čas poslední kontroly - pro správné opětovné hledání commitů
        static DateTime posledniKontrola = DateTime.Now.AddYears(-5);

        // počet nových commitů během jednoho cyklu hledání - pouze pro vypsání do logu
        static int pocetNovychCommitu = 0;

        // pocet všech nových commitu behem cele doby
        static int pocetVsechNovychCommitu = -1;

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

        private static void ZpracovaniCasovace(Object objekt, EventArgs eventargs)
        {
            if (ZkouskaInternetovehoPripojeni())
            {
                cas.OdectiSekunduAktualnihoCasu();
                Program.MainForm.TimeShower.Text = "Další kontrola za: " + cas.VratAktualniCasFormat();

                if (noveSpusteni == true)
                {
                    noveSpusteni = false;

                    ZpracujAVypis(posledniKontrola, Program.MainForm.VsechnyCommityTreeView);
                    Program.MainForm.OtevriZavriVseButton.Enabled = true;
                }
                if (cas.VratAktualniCasMs() == 0)
                {
                    DateTime datum = DateTime.Now.AddMilliseconds(posledniKontrola.Subtract(DateTime.Now).TotalMilliseconds);
                    //DateTime datum = DateTime.Now.AddDays(-4); // PRO TESTOVANI
                    ZpracujAVypis(datum, Program.MainForm.NoveCommityTreeView);
                    }
            }
            else
            {
                if (cas.VratAktualniCasMs() != 1000)
                {
                    cas.OdectiSekunduAktualnihoCasu();
                    Program.MainForm.TimeShower.Text = "Další kontrola za: " + cas.VratAktualniCasFormat();
                }
                else
                {
                    Program.MainForm.TimeShower.Text = "Další kontrola po připojení k internetu";
                }
            }
            NastavTlacitkaAKontrolku();

        }

        private static async void ZpracujAVypis(DateTime datum, System.Windows.Forms.TreeView tv)
        {
            pracuji = true;
            NastavTlacitkaAKontrolku();
            Sluzba s = new Sluzba();

            Program.MainForm.LogBox.AppendText("-------- " + DateTime.Now.ToString() + " --------" + "\n");
            Program.MainForm.LogBox.AppendText("Zpracovavam commity..." + "\n");
            var commity = await s.VratSouboryCommituPoCaseAsync(datum);
            posledniKontrola = DateTime.Now;
            if (commity.Count > 0)
            {
                VypisSeznamSouboru(tv, commity);
            }
            Program.MainForm.LogBox.AppendText("Pocet commitu: " + pocetNovychCommitu + "\n");

            var jazyky = await s.SpocitejPocetRadkuVSouborechUrcitehoTypuAsync("java");
            Program.MainForm.LogBox.AppendText("Pocet radku jazyku Java: " + jazyky.ToString() + "\n\n");

            pocetNovychCommitu = 0;
            pocetVsechNovychCommitu = (tv == Program.MainForm.VsechnyCommityTreeView) ? -1 : pocetVsechNovychCommitu;
            pracuji = false;
        }

        private static void NastavTlacitkaAKontrolku()
        {
            if (!pracuji && ZkouskaInternetovehoPripojeni())
            {
                Program.MainForm.RefreshButton.Enabled =true;
                Program.MainForm.ClearLogBoxButton.Enabled = true;
                Program.MainForm.pictureBox1.BackColor = Color.Green;
            }
            else if (pracuji && ZkouskaInternetovehoPripojeni())
            {
                Program.MainForm.RefreshButton.Enabled = false;
                Program.MainForm.ClearLogBoxButton.Enabled = false;
                Program.MainForm.pictureBox1.BackColor = Color.Green;
            }
            else
            {
                Program.MainForm.RefreshButton.Enabled = false;
                Program.MainForm.ClearLogBoxButton.Enabled = false;
                Program.MainForm.pictureBox1.BackColor = Color.Red;
            }
        }

        private static void VypisSeznamSouboru(System.Windows.Forms.TreeView tv, List<File> soubory)
        {
            soubory.Reverse();
            int i = 0;
            ArrayList dateList = new ArrayList();

            foreach (File soubor in soubory)
            {
                DateTime date = soubor.datum_commitu;

                if (!dateList.Contains(date))
                {
                    pocetVsechCommitu++;
                    pocetVsechNovychCommitu++;
                    pocetNovychCommitu++;
                    i = (tv == Program.MainForm.NoveCommityTreeView) ? pocetVsechNovychCommitu : 0;
                    dateList.Add(date);
                    tv.Nodes.Insert(i, "[" + pocetVsechCommitu + "] " + soubor.datum_commitu.ToString());
                    tv.Nodes[i].Nodes.Add(soubor.filename.ToString());
                }
                else
                {
                    tv.Nodes[i].Nodes.Add(soubor.filename.ToString());
                }
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
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
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
            String selected_file = Program.MainForm.VsechnyCommityTreeView.SelectedNode.Text;

            Form2 GraphForm = new Form2(selected_file);
            GraphForm.Text = "Graf " + selected_file;
            GraphForm.Show();
            Sluzba sluzba = new Sluzba();

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
                Program.MainForm.VsechnyCommityTreeView.CollapseAll();
                openTree = false;
            } else
            {
                Program.MainForm.VsechnyCommityTreeView.ExpandAll();
                openTree = true;
            }

            
        }

        private void VsechnyCommityTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (Program.MainForm.VsechnyCommityTreeView.SelectedNode.Text.EndsWith(".java"))
            {
                Program.MainForm.GrafButton.Enabled = true;
            } else
            {
                Program.MainForm.GrafButton.Enabled = false;
            }
            
        }

        private void NoveCommityTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (Program.MainForm.NoveCommityTreeView.SelectedNode.Parent == null)
            {
                Program.MainForm.PresunoutButton.Enabled = true;
            } else
            {
                Program.MainForm.PresunoutButton.Enabled = false;
            }
        }

        private void PresunoutButton_Click(object sender, EventArgs e)
        {
            var selectedNode = Program.MainForm.NoveCommityTreeView.SelectedNode;
            Program.MainForm.NoveCommityTreeView.SelectedNode.Remove();
            Program.MainForm.VsechnyCommityTreeView.Nodes.Insert(0, selectedNode);
            pocetVsechNovychCommitu--;
            if (Program.MainForm.NoveCommityTreeView.Nodes.Count == 0)
            {
                Program.MainForm.PresunoutButton.Enabled = false;
            }

        }
    }
}
