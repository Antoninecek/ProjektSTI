﻿using System;
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
    public partial class Form2 : Form
    {
        public Form2(String FileName)
        {
            InitializeComponent();
            Form2.ActiveForm.Text = "Graf: " + FileName;
            label1.Text = "asd: " + FileName;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}