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
using System.IO;

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
 //       static int pocetVsechCommitu = 0;

        // hodnota, která oznamuje, zda je v průběhu vrácení commitů - pro správné nastavení tlačítek a zobrazení GUI
        static Boolean pracuji = false;

        // čas poslední kontroly - pro správné opětovné hledání commitů
        static DateTime posledniKontrola = new DateTime(2007, 10, 1, 0, 0, 0); // 01.10.2007 00:00:00 - První commit na GitHubu

        // počet nových commitů během jednoho cyklu hledání - pouze pro vypsání do logu
        static int pocetNovychCommitu = 0;

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
                    ZpracujAVypis(posledniKontrola);
                }
                if (cas.VratAktualniCasMs() == 0)
                {
                    DateTime datum = posledniKontrola;
                    //DateTime datum = DateTime.Now.AddDays(-8); // PRO TESTOVANI
                    ZpracujAVypis(datum);
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

        private static async void ZpracujAVypis(DateTime datum)
        {
            pracuji = true;
            posledniKontrola = DateTime.Now;
            NastavTlacitkaAKontrolku();
            Sluzba s = new Sluzba();

            Program.MainForm.LogBox.AppendText("-------- " + DateTime.Now.ToString() + " --------" + "\n");
            Program.MainForm.LogBox.AppendText("Zpracovavam commity..." + "\n");
            var commity = await s.VratSouboryCommituPoCaseAsync(datum);
            Console.WriteLine("Vrat commity od: " + datum.ToString());
            if (commity.Count > 0)
            {
                VypisCommityDoTabulky(commity);
            }
            Program.MainForm.LogBox.AppendText("Pocet commitu: " + pocetNovychCommitu + "\n");

            var jazyky = await s.SpocitejPocetRadkuVSouborechUrcitehoTypuAsync("java");
            Program.MainForm.LogBox.AppendText("Pocet radku jazyku Java: " + jazyky.ToString() + "\n\n");
            pocetNovychCommitu = 0;

            pracuji = false;
        }

        private static void NastavTlacitkaAKontrolku()
        {
            if (!pracuji && ZkouskaInternetovehoPripojeni())
            {
                Program.MainForm.RefreshButton.Enabled =true;
                Program.MainForm.ClearLogBoxButton.Enabled = true;
                Program.MainForm.Kontrolka.BackColor = Color.Green;
            }
            else if (pracuji && ZkouskaInternetovehoPripojeni())
            {
                Program.MainForm.RefreshButton.Enabled = false;
                Program.MainForm.ClearLogBoxButton.Enabled = false;
                Program.MainForm.Kontrolka.BackColor = Color.Green;
            }
            else
            {
                Program.MainForm.RefreshButton.Enabled = false;
                Program.MainForm.ClearLogBoxButton.Enabled = false;
                Program.MainForm.Kontrolka.BackColor = Color.Red;
            }
        }

        private static void VypisCommityDoTabulky(List<File> soubory)
        {
            soubory.Reverse();
            
            foreach (File soubor in soubory)
            {
                pocetNovychCommitu++;
                Program.MainForm.TabulkaCommitu.Rows.Insert(0, soubor.filename, soubor.datum_commitu.ToString(), soubor.sha.ToString());
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
            
            String selected_file = Program.MainForm.TabulkaCommitu.SelectedCells[0].Value.ToString();
            
            Form2 GraphForm = new Form2(selected_file);
            GraphForm.Text = "Graf " + selected_file;
            GraphForm.Show();
            Sluzba sluzba = new Sluzba();
            var stat = await sluzba.VratStatistikuZmenyRadkuSouboruAsync(selected_file);
            //GraphForm.chart1.Series["Počet přidaných řádků"].Points.Clear();
            stat.Reverse();
            foreach (var commit in stat)
            {
                GraphForm.chart1.Series["Počet přidaných řádků"].Points.AddY(commit.pridane_radky - commit.odebrane_radky);
            };
        }

        private async void ExportButton_Click(object sender, EventArgs e)
        {
            Sluzba s = new Sluzba();
            List<Tuple<string, DateTime>> list = new List<Tuple<string, DateTime>>();

            foreach (DataGridViewRow row in TabulkaCommitu.Rows)
            {
                list.Add(new Tuple<string, DateTime>(row.Cells[0].Value.ToString(), DateTime.Parse(row.Cells[1].Value.ToString())));
            }
            
            string cesta = VyberMistoUlozeni();

            if (!cesta.Equals("Fail"))
            {
                var excel = await s.VytvorExcelSeznamCommituAsync(list, cesta);
                if (excel)
                {
                    Program.MainForm.TabulkaCommitu.ClearSelection();
                    Program.MainForm.TabulkaCommitu.Rows.Clear();
                    Program.MainForm.TabulkaCommitu.Refresh();
                    Console.WriteLine("excel vytvoren");
                }
                else
                {
                    Console.WriteLine("excel nevytvoren");
                }
            }
            else
            {
                Console.WriteLine("cesta nevybrana");
            }
        }


        private void TabulkaCommitu_SelectionChanged(object sender, EventArgs e)
        {
           
            if (Program.MainForm.TabulkaCommitu.SelectedRows.Count != 0)
            {
                if (Program.MainForm.TabulkaCommitu.SelectedRows[0].Cells[0].Value.ToString().EndsWith(".java"))
                {
                    Program.MainForm.GrafButton.Enabled = true;
                }
                else
                {
                    Program.MainForm.GrafButton.Enabled = false;
                }
                Program.MainForm.UlozitButton.Enabled = true;
            }
            else
            {
                Program.MainForm.GrafButton.Enabled = false;
                Program.MainForm.UlozitButton.Enabled = false;
            }

        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            Program.MainForm.ExportButton.Enabled = true;
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            Program.MainForm.ExportButton.Enabled = false;
        }

        private async void UlozitButon_Click(object sender, EventArgs e)
        {
            Sluzba s = new Sluzba();
            String cesta = VyberMistoUlozeni();
            String nazev = Program.MainForm.TabulkaCommitu.SelectedRows[0].Cells[0].Value.ToString();
            String sha = Program.MainForm.TabulkaCommitu.SelectedRows[0].Cells[2].Value.ToString();

            if (!cesta.Equals("Fail"))
            {
                var uloz = await s.StahniSouborZGituAsync(cesta, nazev, sha);

                if (uloz)
                {
                    Console.WriteLine("ulozeno");
                }
                else
                {
                    Console.WriteLine("neulozeno");
                }
            }
            else
            {
                Console.WriteLine("cesta nevybrana");
            }

        }

        private string VyberMistoUlozeni()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    return fbd.SelectedPath;
                }
                return "Fail";
            }
        }
    }
}
