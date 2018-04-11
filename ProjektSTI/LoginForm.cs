using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektSTI
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.ActiveControl = label1;
            textBox1.GotFocus += new EventHandler(this.textBox1_GotFocus);
            textBox1.LostFocus += new EventHandler(this.textBox1_LostFocus);
            textBox2.GotFocus += new EventHandler(this.textBox2_GotFocus);
            textBox2.LostFocus += new EventHandler(this.textBox2_LostFocus);
            textBox3.GotFocus += new EventHandler(this.textBox3_GotFocus);
            textBox3.LostFocus += new EventHandler(this.textBox3_LostFocus);
        }

        private void textBox1_LostFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "")
            {
                tb.Text = "http://api.github.com";
                tb.ForeColor = Color.LightGray;
            }
        }

        private void textBox1_GotFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "http://api.github.com")
            {
                tb.Text = "";
                tb.ForeColor = Color.Black;
            }
        }
        private void textBox2_LostFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "")
            {
                tb.Text = "Antoninecek";
                tb.ForeColor = Color.LightGray;
            }
        }

        private void textBox2_GotFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "Antoninecek")
            {
                tb.Text = "";
                tb.ForeColor = Color.Black;
            }
        }
        private void textBox3_LostFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "")
            {
                tb.Text = "TEST";
                tb.ForeColor = Color.LightGray;
            }
        }

        private void textBox3_GotFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "TEST")
            {
                tb.Text = "";
                tb.ForeColor = Color.Black;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string adresa = textBox1.Text;
            string uzivatel = textBox2.Text;
            string repozitar = textBox3.Text;

            this.Hide();
            MainForm mf = new MainForm(adresa, uzivatel, repozitar);
            mf.Text = adresa + "/" + uzivatel + "/" + repozitar;
            mf.ShowDialog();
            this.Close();
        }
    }
}
