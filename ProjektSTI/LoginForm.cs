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
        string Url;
        string Token;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            NactiConfig();

            urlBox.Text = Url;
            tokenBox.Text = Token;
            
            this.ActiveControl = urlLabel;
            urlBox.GotFocus += new EventHandler(this.urlBox_GotFocus);
            urlBox.LostFocus += new EventHandler(this.urlBox_LostFocus);
            tokenBox.GotFocus += new EventHandler(this.tokenBox_GotFocus);
            tokenBox.LostFocus += new EventHandler(this.tokenBox_LostFocus);
        }

        private void NactiConfig()
        {
            string config_path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + "\\config.json";
            if (System.IO.File.Exists(config_path))
            {
                var txt = System.IO.File.ReadAllText(config_path);
                Nastaveni n = JsonConvert.DeserializeObject<Nastaveni>(txt);
                Url = "https://github.com/" + n.Uzivatel + "/" + n.Repozitar;
                Token = n.githubToken;
            } else
            {
                Url = "https://github.com/Uzivatel/Repozitar";
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
        
        private void urlBox_LostFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "")
            {
                tb.Text = Url;
                tb.ForeColor = Color.LightGray;
            }
        }

        private void urlBox_GotFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == Url)
            {
                tb.Text = "";
                tb.ForeColor = Color.Black;
            }
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string token = tokenBox.Text;
            Uri url;
            bool validUrl = Uri.TryCreate(urlBox.Text, UriKind.Absolute, out url) && (url.Scheme == Uri.UriSchemeHttp || url.Scheme == Uri.UriSchemeHttps) && (url.PathAndQuery.Split('/').Length > 2);

            if (token == "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx")
            {
                MessageBox.Show("Zadejte GH token", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else if (!validUrl)
            {
                MessageBox.Show("Zadejte URL ve správném tvaru", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.Hide();
                string[] path = url.PathAndQuery.Split('/');
                string uzivatel = path[1];
                string repozitar = path[2];

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
