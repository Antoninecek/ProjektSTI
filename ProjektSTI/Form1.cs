using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektSTI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //var souboryTask = File.VratSouboryCommituDoCasuAsync(DateTime.Now.AddYears(-5));

            //var javaSouboryTask = RootObject.VratSouboryUrcitehoTypuRepozitareAsync("java");

            //System.Diagnostics.Debug.WriteLine("cekame");

            //var javaSoubory = await javaSouboryTask;
            //System.Diagnostics.Debug.WriteLine("java rdy");

            //var soubory = await souboryTask;
            //System.Diagnostics.Debug.WriteLine("soubory rdy");


            ////var soubory = File.VratSouboryCommituDoCasu(DateTime.Now.AddYears(-5));
            ////var javaSoubory = RootObject.VratSouboryUrcitehoTypuRepozitare("java");


            //var pocetRadku = RootObject.SpocitejPocetRadkuSadySouboru(javaSoubory);

            //System.Diagnostics.Debug.WriteLine(soubory.Count);
            //System.Diagnostics.Debug.WriteLine(javaSoubory.Count);
            //System.Diagnostics.Debug.WriteLine(pocetRadku);

            DataMiner dm = new DataMiner();
            //var a = await dm.VratPrehledRadkuJazykuRepozitareAsync("PHP");
            var commity = dm.VratCommityJednohoSouboru("JAVASOUBOR.java");
            var detail = dm.VratDetailCommitu(commity.First().sha);
            foreach(var soubor in detail.files)
            {
                if(soubor.filename == "JAVASOUBOR.java")
                {
                    // tady vezmu cas, pridani, odebrani
                }
            }
            
        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
