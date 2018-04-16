namespace ProjektSTI
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tokenLabel = new System.Windows.Forms.Label();
            this.uzivatelLabel = new System.Windows.Forms.Label();
            this.repozitarLabel = new System.Windows.Forms.Label();
            this.tokenBox = new System.Windows.Forms.TextBox();
            this.uzivatelBox = new System.Windows.Forms.TextBox();
            this.repozitarBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tokenLabel
            // 
            this.tokenLabel.AutoSize = true;
            this.tokenLabel.Location = new System.Drawing.Point(22, 80);
            this.tokenLabel.Name = "tokenLabel";
            this.tokenLabel.Size = new System.Drawing.Size(57, 13);
            this.tokenLabel.TabIndex = 0;
            this.tokenLabel.Text = "GH Token";
            // 
            // uzivatelLabel
            // 
            this.uzivatelLabel.AutoSize = true;
            this.uzivatelLabel.Location = new System.Drawing.Point(34, 34);
            this.uzivatelLabel.Name = "uzivatelLabel";
            this.uzivatelLabel.Size = new System.Drawing.Size(45, 13);
            this.uzivatelLabel.TabIndex = 1;
            this.uzivatelLabel.Text = "Uživatel";
            // 
            // repozitarLabel
            // 
            this.repozitarLabel.AutoSize = true;
            this.repozitarLabel.Location = new System.Drawing.Point(26, 57);
            this.repozitarLabel.Name = "repozitarLabel";
            this.repozitarLabel.Size = new System.Drawing.Size(53, 13);
            this.repozitarLabel.TabIndex = 2;
            this.repozitarLabel.Text = "Repozitář";
            // 
            // tokenBox
            // 
            this.tokenBox.ForeColor = System.Drawing.Color.LightGray;
            this.tokenBox.Location = new System.Drawing.Point(85, 77);
            this.tokenBox.Name = "tokenBox";
            this.tokenBox.Size = new System.Drawing.Size(285, 20);
            this.tokenBox.TabIndex = 4;
            this.tokenBox.Text = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
            // 
            // uzivatelBox
            // 
            this.uzivatelBox.ForeColor = System.Drawing.Color.LightGray;
            this.uzivatelBox.Location = new System.Drawing.Point(85, 31);
            this.uzivatelBox.Name = "uzivatelBox";
            this.uzivatelBox.Size = new System.Drawing.Size(285, 20);
            this.uzivatelBox.TabIndex = 5;
            this.uzivatelBox.Text = "Antoninecek";
            // 
            // repozitarBox
            // 
            this.repozitarBox.ForeColor = System.Drawing.Color.LightGray;
            this.repozitarBox.Location = new System.Drawing.Point(85, 54);
            this.repozitarBox.Name = "repozitarBox";
            this.repozitarBox.Size = new System.Drawing.Size(285, 20);
            this.repozitarBox.TabIndex = 6;
            this.repozitarBox.Text = "TEST";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(188, 103);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Login";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 146);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.repozitarBox);
            this.Controls.Add(this.uzivatelBox);
            this.Controls.Add(this.tokenBox);
            this.Controls.Add(this.repozitarLabel);
            this.Controls.Add(this.uzivatelLabel);
            this.Controls.Add(this.tokenLabel);
            this.Name = "LoginForm";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tokenLabel;
        private System.Windows.Forms.Label uzivatelLabel;
        private System.Windows.Forms.Label repozitarLabel;
        private System.Windows.Forms.TextBox tokenBox;
        private System.Windows.Forms.TextBox uzivatelBox;
        private System.Windows.Forms.TextBox repozitarBox;
        private System.Windows.Forms.Button button1;
    }
}