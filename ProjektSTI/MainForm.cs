﻿using Newtonsoft.Json;
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
        //static int interval = 60000 - 1000; // PRO TESTOVANI
        static Cas cas = new Cas(interval);
        static Stopwatch sw = new Stopwatch();

        // vytvořeno speciálně pro nové spuštění aplikace, po prvním vrácení commitů se nastaví na false
        static Boolean noveSpusteni = true;
        static Boolean prvniCyklus = true;

        // počet všech commitů
        //       static int pocetVsechCommitu = 0;

        // hodnota, která oznamuje, zda je v průběhu nějaká činnost aplikace - pro správné nastavení tlačítek a zobrazení GUI
        static Boolean pracuji = false;

        // čas poslední kontroly - pro správné opětovné hledání commitů
        static DateTime posledniKontrola = new DateTime(2007, 10, 1, 0, 0, 0); // 01.10.2007 00:00:00 - První commit na GitHubu

        // počet nových commitů během jednoho cyklu hledání - pouze pro vypsání do logu
        static int pocetNovychSouboru = 0;

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

        private void ZpracovaniCasovace(Object objekt, EventArgs eventargs)
        {
            if (ZkouskaInternetovehoPripojeni())
            {
                if (!pracuji)
                {
                    cas.OdectiSekunduAktualnihoCasu();
                    
                    UkazatelCasu.Text = "Další kontrola za: " + cas.VratAktualniCasFormat();

                    if (noveSpusteni == true)
                    {
                        ZpracujAVypis(posledniKontrola);
                        noveSpusteni = false;
                    }
                    if (cas.VratAktualniCasMs() == 0)
                    {
                        DateTime datum = posledniKontrola;
                        //DateTime datum = DateTime.Now.AddYears(-8); // PRO TESTOVANI
                        ZpracujAVypis(datum);
                    }
                }
                else
                {
                    if (cas.VratAktualniCasMs() != 1000)
                    {
                        cas.OdectiSekunduAktualnihoCasu();
                        UkazatelCasu.Text = "Další kontrola za: " + cas.VratAktualniCasFormat();
                    }
                    else
                    {
                        UkazatelCasu.Text = "Další kontrola po dokončení probíhající práce";
                    }
                }

            }
            else
            {
                if (cas.VratAktualniCasMs() != 1000)
                {
                    cas.OdectiSekunduAktualnihoCasu();
                    UkazatelCasu.Text = "Další kontrola za: " + cas.VratAktualniCasFormat();
                }
                else
                {
                    UkazatelCasu.Text = "Další kontrola po připojení k internetu";
                }
            }
            NastavTlacitkaAKontrolku();

        }

        private async void ZpracujAVypis(DateTime datum)
        {
            while (pracuji)
            {
                await Task.Delay(100);
            }
            pracuji = true;
            posledniKontrola = DateTime.Now;
            NastavTlacitkaAKontrolku();
            Sluzba s = new Sluzba();

            DateTime start;

            LogniCas();
            LogBox.AppendText("Zpracovávám commity..." + "\n");
            try
            {
                start = DateTime.Now;
                System.Diagnostics.Debug.WriteLine("start: " + DateTime.Now);
                var commity = await s.VratSouboryCommituPoCaseAsync(datum);
                System.Diagnostics.Debug.WriteLine("konec: " + DateTime.Now);
                System.Diagnostics.Debug.WriteLine("doba vraceni souboru commitu: " + (start - DateTime.Now));
                if (commity.Count > 0)
                {
                    start = DateTime.Now;
                    System.Diagnostics.Debug.WriteLine("start: " + DateTime.Now);
                    VypisCommityDoTabulky(commity);
                    System.Diagnostics.Debug.WriteLine("konec: " + DateTime.Now);
                    System.Diagnostics.Debug.WriteLine("doba vypsani souboru do tabulky: " + (start - DateTime.Now));
                }
            }
            catch (Exception ex)
            {
                if (prvniCyklus == true)
                {
                    MessageBox.Show(ex.Message);
                    Application.Restart();
                }
            }
            prvniCyklus = false;
            LogBox.AppendText("Počet nových souborů: " + pocetNovychSouboru + "\n");

            start = DateTime.Now;
            System.Diagnostics.Debug.WriteLine("start: " + DateTime.Now);
            try
            {
                var jazyky = await s.SpocitejPocetRadkuVSouborechUrcitehoTypuAsync("java");
                System.Diagnostics.Debug.WriteLine("konec: " + DateTime.Now);
                System.Diagnostics.Debug.WriteLine("doba pocitani radku: " + (start - DateTime.Now));
                LogBox.AppendText("Počet řádků jazyku Java: " + jazyky.ToString() + "\n\n");
                pocetNovychSouboru = 0;
            }
            catch(Exception ex)
            {
                String chyba = "Nepodařilo se spočítat řádky java souborů. ";
                if (!ZkouskaInternetovehoPripojeni())
                { 
                    chyba += ("Chyba internetového připojení.");
                }
                LogBox.AppendText("Počet řádků jazyku Java: " + chyba + "\n\n");
                //MessageBox.Show(chyba);
            }
            pracuji = false;
        }

        private void NastavTlacitkaAKontrolku()
        {
            if (!pracuji && ZkouskaInternetovehoPripojeni())
            {
                this.Cursor = Cursors.Default;
                RefreshButton.Enabled = true;
                ClearLogBoxButton.Enabled = true;
                Kontrolka.BackColor = Color.Green;
                if (TabulkaCommitu.Rows.Count != 0)
                {
                    if (TabulkaCommitu.SelectedRows.Count != 0)
                    {
                        if (TabulkaCommitu.SelectedRows[0].Cells[0].Value.ToString().EndsWith(".java"))
                        {
                            GrafButton.Enabled = true;
                        }
                        else
                        {
                            GrafButton.Enabled = false;
                        }
                        UlozitButton.Enabled = true;
                    }
                    else
                    {
                        GrafButton.Enabled = false;
                        UlozitButton.Enabled = false;
                    }
                    ExportButton.Enabled = true;
                }
                else
                {
                    GrafButton.Enabled = false;
                    UlozitButton.Enabled = false;
                    ExportButton.Enabled = false;
                }

            }
            else if (pracuji && ZkouskaInternetovehoPripojeni())
            {
                this.Cursor = Cursors.WaitCursor;
                RefreshButton.Enabled = false;
                ClearLogBoxButton.Enabled = false;
                Kontrolka.BackColor = Color.Green;
                UlozitButton.Enabled = false;
                GrafButton.Enabled = false;
                UlozitButton.Enabled = false;
                ExportButton.Enabled = false;
            }
            else
            {
                this.Cursor = Cursors.Default;
                RefreshButton.Enabled = false;
                ClearLogBoxButton.Enabled = false;
                Kontrolka.BackColor = Color.Red;
                UlozitButton.Enabled = false;
                GrafButton.Enabled = false;
                UlozitButton.Enabled = false;
                ExportButton.Enabled = false;
            }
        }

        private void VypisCommityDoTabulky(List<File> soubory)
        {
            soubory.Reverse();

            foreach (File soubor in soubory)
            {
                pocetNovychSouboru++;
                TabulkaCommitu.Rows.Insert(0, soubor.filename, soubor.datum_commitu.ToString(), soubor.sha.ToString());
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
            LogBox.Clear();
        }

        private async void GrafButton_Click(object sender, EventArgs e)
        {

            String selected_file = TabulkaCommitu.SelectedCells[0].Value.ToString();

            GrafForm GraphForm = new GrafForm(selected_file);
            GraphForm.Text = "Graf " + selected_file;
            GraphForm.Show();
            GraphForm.chart1.Legends.Clear();
            Sluzba sluzba = new Sluzba();
            try
            {
                var stat = await sluzba.VratStatistikuZmenyRadkuSouboruAsync(selected_file);
                //GraphForm.chart1.Series["Počet přidaných řádků"].Points.Clear();
                stat.Reverse();
                foreach (var commit in stat)
                {
                    GraphForm.chart1.Series["Počet přidaných řádků"].Points.AddY(commit.pridane_radky - commit.odebrane_radky);
                }
                GraphForm.chart1.Cursor = Cursors.Default;
            }
            catch (System.NullReferenceException)
            {};
        }

        private async void ExportButton_Click(object sender, EventArgs e)
        {
            while (pracuji)
            {
                await Task.Delay(100);
            }
            pracuji = true;

            Sluzba s = new Sluzba();
            List<Tuple<string, DateTime>> list = new List<Tuple<string, DateTime>>();

            string cesta = VyberMistoUlozeni("export.xlsx");

            try
            {
                if (cesta != null)
                {
                    foreach (DataGridViewRow row in TabulkaCommitu.Rows)
                    {
                        list.Add(new Tuple<string, DateTime>(row.Cells[0].Value.ToString(), DateTime.Parse(row.Cells[1].Value.ToString())));
                    }
                    LogniCas();
                    LogBox.AppendText("Exportuji... \n");
                    var excel = await s.VytvorExcelSeznamCommituAsync(list, cesta);

                    if (excel)
                    {
                        LogBox.AppendText("Soubor exportován do: " + cesta + "\n");
                        TabulkaCommitu.ClearSelection();
                        TabulkaCommitu.Rows.Clear();
                        TabulkaCommitu.Refresh();
                        Console.WriteLine("excel vytvoren v: " + cesta);
                    }
                    else
                    {
                        LogBox.AppendText("Soubor se nepodařilo exportovat \n");
                        Console.WriteLine("excel nevytvoren");
                    }
                    LogBox.AppendText("\n");
                }
                else
                {
                    Console.WriteLine("cesta nevybrana");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                pracuji = false;
        }

        private async void UlozitButon_Click(object sender, EventArgs e)
        {
            while (pracuji)
            {
                await Task.Delay(100);
            }
            pracuji = true;

            Sluzba s = new Sluzba();
            String nazev = TabulkaCommitu.SelectedRows[0].Cells[0].Value.ToString();
            String cesta = VyberMistoUlozeni(nazev);
            String sha = TabulkaCommitu.SelectedRows[0].Cells[2].Value.ToString();
            try
            {
                if (cesta != null)
                {
                    LogniCas();
                    LogBox.AppendText("Ukládám soubor...\n");
                    var uloz = await s.StahniSouborZGituAsync(cesta, nazev, sha);

                    if (uloz)
                    {
                        LogBox.AppendText("Soubor uložen do: " + cesta + "\n");
                        Console.WriteLine("ulozeno do: " + cesta);
                    }
                    else
                    {
                        LogBox.AppendText("Soubor se nepodařilo uložit \n");
                        Console.WriteLine("neulozeno: " + cesta + " " + nazev + " " + sha);
                    }
                    LogBox.AppendText("\n");
                }
                else
                {
                    Console.WriteLine("cesta nevybrana");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                pracuji = false;

        }
        private void TabulkaCommitu_SelectionChanged(object sender, EventArgs e)
        {
            NastavTlacitkaAKontrolku();
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            NastavTlacitkaAKontrolku();
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            NastavTlacitkaAKontrolku();
        }

        private void LogniCas()
        {
            LogBox.AppendText("-------- " + DateTime.Now.ToString() + " --------" + "\n");
        }

        private string VyberMistoUlozeni(string nazev)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            string format = Path.GetExtension(nazev);
            char[] splitchar = { '/' };
            String[] split_nazev = nazev.Split(splitchar);
            String save_nazev = split_nazev[split_nazev.Length - 1];
            savefile.FileName = save_nazev;
            savefile.Filter = format.Substring(1, format.Length - 1).ToUpper() + " soubory (*" + format + ")|*" + format + "|Všechny soubory (*.*)|*.*";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                return savefile.FileName;
            }
            else
            {
                return null;
            }
        }

        private bool ZkouskaInternetovehoPripojeni()
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

        private void LogBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
