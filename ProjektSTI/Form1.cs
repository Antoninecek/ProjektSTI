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
            var souboryTask = File.VratSouboryCommituDoCasuAsync(DateTime.Now.AddYears(-5));
            //souboryTask.Start();
            var javaSouboryTask = RootObject.VratSouboryUrcitehoTypuRepozitareAsync("java");
            //javaSouboryTask.Start();

            
            //var javaSoubory = RootObject.VratSouboryUrcitehoTypuRepozitare("java");

            System.Diagnostics.Debug.WriteLine("cekame");

            var javaSoubory = await Task.Run(() => javaSouboryTask); ;

            System.Diagnostics.Debug.WriteLine(javaSoubory.Count);
            var pocetRadku = RootObject.SpocitejPocetRadkuSadySouboru(javaSoubory);
            System.Diagnostics.Debug.WriteLine(pocetRadku);

            var soubory = await Task.Run(() => souboryTask);
            System.Diagnostics.Debug.WriteLine(soubory.Count);
        }

        



        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
