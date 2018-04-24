using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektSTI
{
    public partial class LoginForm : Form
    {
        string Repozitar;
        string Uzivatel;
        string Token;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            NactiConfig();

            repozitarBox.Text = Repozitar;
            uzivatelBox.Text = Uzivatel;
            tokenBox.Text = Token;
            
            this.ActiveControl = tokenLabel;
            tokenBox.GotFocus += new EventHandler(this.tokenBox_GotFocus);
            tokenBox.LostFocus += new EventHandler(this.tokenBox_LostFocus);
            uzivatelBox.GotFocus += new EventHandler(this.uzivatelBox_GotFocus);
            uzivatelBox.LostFocus += new EventHandler(this.uzivatelBox_LostFocus);
            repozitarBox.GotFocus += new EventHandler(this.repozitarBox_GotFocus);
            repozitarBox.LostFocus += new EventHandler(this.repozitarBox_LostFocus);
        }

        private void NactiConfig()
        {
            string config_path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + "\\config.json";
            if (System.IO.File.Exists(config_path))
            {
                var txt = System.IO.File.ReadAllText(config_path);
                Nastaveni n = JsonConvert.DeserializeObject<Nastaveni>(txt);
                Repozitar = n.Repozitar;
                Uzivatel = n.Uzivatel;
                Token = n.githubToken;
            } else
            {
                Repozitar = "Název repozitáře";
                Uzivatel = "Uživatelské jméno";
                Token = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
            }
            
        }

        private void tokenBox_LostFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "")
            {
                tb.Text = Token;
                tb.ForeColor = Color.LightGray;
            }
        }

        private void tokenBox_GotFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == Token)
            {
                tb.Text = "";
                tb.ForeColor = Color.Black;
            }
        }
        private void uzivatelBox_LostFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "")
            {
                tb.Text = Uzivatel;
                tb.ForeColor = Color.LightGray;
            }
        }

        private void uzivatelBox_GotFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == Uzivatel)
            {
                tb.Text = "";
                tb.ForeColor = Color.Black;
            }
        }
        private void repozitarBox_LostFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "")
            {
                tb.Text = Repozitar;
                tb.ForeColor = Color.LightGray;
            }
        }

        private void repozitarBox_GotFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == Repozitar)
            {
                tb.Text = "";
                tb.ForeColor = Color.Black;
            }
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string token = tokenBox.Text;
            string uzivatel = uzivatelBox.Text;
            string repozitar = repozitarBox.Text;

            if (token == "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx")
            {
                MessageBox.Show("Zadejte GH token", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.Hide();
                Sluzba s = new Sluzba();
                s.NastavDataMiner(repozitar, uzivatel, token);
                MainForm mf = new MainForm();
                mf.Text = uzivatel + "/" + repozitar;
                mf.StartPosition = FormStartPosition.CenterParent;
                mf.ShowDialog();
                this.Close();
            }
        }

        private void uzivatelBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
