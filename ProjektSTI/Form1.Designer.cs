namespace ProjektSTI
{
    partial class MainForm
    {
        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            this.RefreshButton = new System.Windows.Forms.Button();
            this.LogBox = new System.Windows.Forms.RichTextBox();
            this.TimeShower = new System.Windows.Forms.Label();
            this.ClearLogBoxButton = new System.Windows.Forms.Button();
            this.ChangedFilesBox = new System.Windows.Forms.RichTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.AllFilesTreeView = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // RefreshButton
            // 
            this.RefreshButton.Enabled = false;
            this.RefreshButton.Location = new System.Drawing.Point(12, 12);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(52, 39);
            this.RefreshButton.TabIndex = 0;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // LogBox
            // 
            this.LogBox.Location = new System.Drawing.Point(12, 57);
            this.LogBox.Name = "LogBox";
            this.LogBox.ReadOnly = true;
            this.LogBox.Size = new System.Drawing.Size(206, 386);
            this.LogBox.TabIndex = 3;
            this.LogBox.Text = "";
            // 
            // TimeShower
            // 
            this.TimeShower.AutoSize = true;
            this.TimeShower.Location = new System.Drawing.Point(70, 25);
            this.TimeShower.Name = "TimeShower";
            this.TimeShower.Size = new System.Drawing.Size(148, 13);
            this.TimeShower.TabIndex = 4;
            this.TimeShower.Text = "Další kontrola za: 0h 00m 00s";
            this.TimeShower.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ClearLogBoxButton
            // 
            this.ClearLogBoxButton.Enabled = false;
            this.ClearLogBoxButton.Location = new System.Drawing.Point(73, 450);
            this.ClearLogBoxButton.Name = "ClearLogBoxButton";
            this.ClearLogBoxButton.Size = new System.Drawing.Size(75, 23);
            this.ClearLogBoxButton.TabIndex = 5;
            this.ClearLogBoxButton.Text = "Clear";
            this.ClearLogBoxButton.UseVisualStyleBackColor = true;
            this.ClearLogBoxButton.Click += new System.EventHandler(this.ClearLogBoxButton_Click);
            // 
            // ChangedFilesBox
            // 
            this.ChangedFilesBox.Location = new System.Drawing.Point(452, 57);
            this.ChangedFilesBox.Name = "ChangedFilesBox";
            this.ChangedFilesBox.ReadOnly = true;
            this.ChangedFilesBox.Size = new System.Drawing.Size(222, 386);
            this.ChangedFilesBox.TabIndex = 6;
            this.ChangedFilesBox.Text = "";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Red;
            this.pictureBox1.Location = new System.Drawing.Point(648, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(25, 26);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(295, 449);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Graf";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AllFilesTreeView
            // 
            this.AllFilesTreeView.Location = new System.Drawing.Point(224, 57);
            this.AllFilesTreeView.Name = "AllFilesTreeView";
            this.AllFilesTreeView.PathSeparator = "/";
            this.AllFilesTreeView.Size = new System.Drawing.Size(222, 386);
            this.AllFilesTreeView.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(688, 487);
            this.Controls.Add(this.AllFilesTreeView);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ChangedFilesBox);
            this.Controls.Add(this.ClearLogBoxButton);
            this.Controls.Add(this.TimeShower);
            this.Controls.Add(this.LogBox);
            this.Controls.Add(this.RefreshButton);
            this.Name = "MainForm";
            this.Text = "Projekt STI";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.RichTextBox LogBox;
        private System.Windows.Forms.Label TimeShower;
        private System.Windows.Forms.Button ClearLogBoxButton;
        private System.Windows.Forms.RichTextBox ChangedFilesBox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TreeView AllFilesTreeView;
    }
}

