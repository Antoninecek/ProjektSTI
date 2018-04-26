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
            this.urlLabel = new System.Windows.Forms.Label();
            this.tokenLabel = new System.Windows.Forms.Label();
            this.urlBox = new System.Windows.Forms.TextBox();
            this.tokenBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // urlLabel
            // 
            this.urlLabel.AutoSize = true;
            this.urlLabel.Location = new System.Drawing.Point(51, 35);
            this.urlLabel.Name = "urlLabel";
            this.urlLabel.Size = new System.Drawing.Size(29, 13);
            this.urlLabel.TabIndex = 0;
            this.urlLabel.Text = "URL";
            // 
            // tokenLabel
            // 
            this.tokenLabel.AutoSize = true;
            this.tokenLabel.Location = new System.Drawing.Point(23, 60);
            this.tokenLabel.Name = "tokenLabel";
            this.tokenLabel.Size = new System.Drawing.Size(57, 13);
            this.tokenLabel.TabIndex = 1;
            this.tokenLabel.Text = "GH Token";
            // 
            // urlBox
            // 
            this.urlBox.ForeColor = System.Drawing.Color.LightGray;
            this.urlBox.Location = new System.Drawing.Point(86, 32);
            this.urlBox.Name = "urlBox";
            this.urlBox.Size = new System.Drawing.Size(285, 20);
            this.urlBox.TabIndex = 4;
            this.urlBox.Text = "https://github.com/Uzivatel/Repozitar";
            // 
            // tokenBox
            // 
            this.tokenBox.ForeColor = System.Drawing.Color.LightGray;
            this.tokenBox.Location = new System.Drawing.Point(86, 57);
            this.tokenBox.Name = "tokenBox";
            this.tokenBox.Size = new System.Drawing.Size(285, 20);
            this.tokenBox.TabIndex = 5;
            this.tokenBox.Text = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(180, 83);
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
            this.ClientSize = new System.Drawing.Size(418, 123);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tokenBox);
            this.Controls.Add(this.urlBox);
            this.Controls.Add(this.tokenLabel);
            this.Controls.Add(this.urlLabel);
            this.Name = "LoginForm";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label urlLabel;
        private System.Windows.Forms.Label tokenLabel;
        private System.Windows.Forms.TextBox urlBox;
        private System.Windows.Forms.TextBox tokenBox;
        private System.Windows.Forms.Button button1;
    }
}