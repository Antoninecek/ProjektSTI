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
            this.Kontrolka = new System.Windows.Forms.PictureBox();
            this.GrafButton = new System.Windows.Forms.Button();
            this.VsechnyCommityTreeView = new System.Windows.Forms.TreeView();
            this.OtevriZavriVseButton = new System.Windows.Forms.Button();
            this.NoveCommityTreeView = new System.Windows.Forms.TreeView();
            this.PresunoutButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Kontrolka)).BeginInit();
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
            // Kontrolka
            // 
            this.Kontrolka.BackColor = System.Drawing.Color.Red;
            this.Kontrolka.Location = new System.Drawing.Point(648, 12);
            this.Kontrolka.Name = "Kontrolka";
            this.Kontrolka.Size = new System.Drawing.Size(25, 26);
            this.Kontrolka.TabIndex = 7;
            this.Kontrolka.TabStop = false;
            // 
            // GrafButton
            // 
            this.GrafButton.Enabled = false;
            this.GrafButton.Location = new System.Drawing.Point(305, 449);
            this.GrafButton.Name = "GrafButton";
            this.GrafButton.Size = new System.Drawing.Size(75, 23);
            this.GrafButton.TabIndex = 8;
            this.GrafButton.Text = "Graf";
            this.GrafButton.UseVisualStyleBackColor = true;
            this.GrafButton.Click += new System.EventHandler(this.GrafButton_Click);
            // 
            // VsechnyCommityTreeView
            // 
            this.VsechnyCommityTreeView.Location = new System.Drawing.Point(224, 57);
            this.VsechnyCommityTreeView.Name = "VsechnyCommityTreeView";
            this.VsechnyCommityTreeView.PathSeparator = "/";
            this.VsechnyCommityTreeView.Size = new System.Drawing.Size(222, 386);
            this.VsechnyCommityTreeView.TabIndex = 9;
            this.VsechnyCommityTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.VsechnyCommityTreeView_AfterSelect);
            // 
            // OtevriZavriVseButton
            // 
            this.OtevriZavriVseButton.Enabled = false;
            this.OtevriZavriVseButton.Location = new System.Drawing.Point(281, 28);
            this.OtevriZavriVseButton.Name = "OtevriZavriVseButton";
            this.OtevriZavriVseButton.Size = new System.Drawing.Size(99, 23);
            this.OtevriZavriVseButton.TabIndex = 10;
            this.OtevriZavriVseButton.Text = "Otevři/Zavři vše";
            this.OtevriZavriVseButton.UseVisualStyleBackColor = true;
            this.OtevriZavriVseButton.Click += new System.EventHandler(this.OtevriZavriVseButton_Click);
            // 
            // NoveCommityTreeView
            // 
            this.NoveCommityTreeView.Location = new System.Drawing.Point(452, 57);
            this.NoveCommityTreeView.Name = "NoveCommityTreeView";
            this.NoveCommityTreeView.PathSeparator = "/";
            this.NoveCommityTreeView.Size = new System.Drawing.Size(222, 386);
            this.NoveCommityTreeView.TabIndex = 11;
            this.NoveCommityTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.NoveCommityTreeView_AfterSelect);
            // 
            // PresunoutButton
            // 
            this.PresunoutButton.Enabled = false;
            this.PresunoutButton.Location = new System.Drawing.Point(532, 449);
            this.PresunoutButton.Name = "PresunoutButton";
            this.PresunoutButton.Size = new System.Drawing.Size(75, 23);
            this.PresunoutButton.TabIndex = 12;
            this.PresunoutButton.Text = "Přesunout";
            this.PresunoutButton.UseVisualStyleBackColor = true;
            this.PresunoutButton.Click += new System.EventHandler(this.PresunoutButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(687, 487);
            this.Controls.Add(this.PresunoutButton);
            this.Controls.Add(this.NoveCommityTreeView);
            this.Controls.Add(this.OtevriZavriVseButton);
            this.Controls.Add(this.VsechnyCommityTreeView);
            this.Controls.Add(this.GrafButton);
            this.Controls.Add(this.Kontrolka);
            this.Controls.Add(this.ClearLogBoxButton);
            this.Controls.Add(this.TimeShower);
            this.Controls.Add(this.LogBox);
            this.Controls.Add(this.RefreshButton);
            this.Name = "MainForm";
            this.Text = "Projekt STI";
            ((System.ComponentModel.ISupportInitialize)(this.Kontrolka)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.RichTextBox LogBox;
        private System.Windows.Forms.Label TimeShower;
        private System.Windows.Forms.Button ClearLogBoxButton;
        private System.Windows.Forms.PictureBox Kontrolka;
        private System.Windows.Forms.Button GrafButton;
        private System.Windows.Forms.TreeView VsechnyCommityTreeView;
        private System.Windows.Forms.Button OtevriZavriVseButton;
        private System.Windows.Forms.TreeView NoveCommityTreeView;
        private System.Windows.Forms.Button PresunoutButton;
    }
}

