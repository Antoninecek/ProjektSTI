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

        private void button1_Click(object sender, EventArgs e)
        {
            var soubory = VratSouboryCommituDoCasu(DateTime.Now.AddHours(-1));
            System.Diagnostics.Debug.WriteLine(soubory.Count);
            var javaSoubory = RootObject.VratSouboryUrcitehoTypuRepozitare("java");
            var pocetRadku = RootObject.SpocitejPocetRadkuSadySouboru(javaSoubory);
        }

        /// <summary>
        /// main metoda pro ziskani souboru z commitu uskutecnenych po zadane dobe
        /// </summary>
        /// <param name="cas"></param>
        /// <returns></returns>
        public List<File> VratSouboryCommituDoCasu(DateTime cas)
        {
            DataMiner dm = new DataMiner();
            List<Zaznam> zaznamy = dm.VratCommity();
            List<Zaznam> zaznamyHodina = Zaznam.SelektujCasovouPeriodu(zaznamy, cas);
            List<File> soubory = new List<File>();
            foreach (var z in zaznamyHodina)
            {
                var detail = dm.VratDetailCommitu(z.sha);
                soubory.AddRange(detail.files);
            }
            return soubory;
        }



        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
