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
            this.ExportButton = new System.Windows.Forms.Button();
            this.TabulkaCommitu = new System.Windows.Forms.DataGridView();
            this.nazev = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UlozitButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Kontrolka)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TabulkaCommitu)).BeginInit();
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
            this.GrafButton.Location = new System.Drawing.Point(283, 448);
            this.GrafButton.Name = "GrafButton";
            this.GrafButton.Size = new System.Drawing.Size(75, 23);
            this.GrafButton.TabIndex = 8;
            this.GrafButton.Text = "Graf";
            this.GrafButton.UseVisualStyleBackColor = true;
            this.GrafButton.Click += new System.EventHandler(this.GrafButton_Click);
            // 
            // ExportButton
            // 
            this.ExportButton.Enabled = false;
            this.ExportButton.Location = new System.Drawing.Point(551, 448);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(75, 23);
            this.ExportButton.TabIndex = 12;
            this.ExportButton.Text = "Export";
            this.ExportButton.UseVisualStyleBackColor = true;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // TabulkaCommitu
            // 
            this.TabulkaCommitu.AllowUserToAddRows = false;
            this.TabulkaCommitu.AllowUserToDeleteRows = false;
            this.TabulkaCommitu.AllowUserToResizeRows = false;
            this.TabulkaCommitu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TabulkaCommitu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nazev,
            this.datum,
            this.sha});
            this.TabulkaCommitu.Location = new System.Drawing.Point(224, 57);
            this.TabulkaCommitu.MultiSelect = false;
            this.TabulkaCommitu.Name = "TabulkaCommitu";
            this.TabulkaCommitu.ReadOnly = true;
            this.TabulkaCommitu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TabulkaCommitu.Size = new System.Drawing.Size(449, 386);
            this.TabulkaCommitu.TabIndex = 13;
            this.TabulkaCommitu.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            this.TabulkaCommitu.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView1_RowsRemoved);
            this.TabulkaCommitu.SelectionChanged += new System.EventHandler(this.TabulkaCommitu_SelectionChanged);
            // 
            // nazev
            // 
            this.nazev.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nazev.HeaderText = "Název souboru";
            this.nazev.Name = "nazev";
            this.nazev.ReadOnly = true;
            this.nazev.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.nazev.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // datum
            // 
            this.datum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.datum.HeaderText = "Datum";
            this.datum.Name = "datum";
            this.datum.ReadOnly = true;
            this.datum.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.datum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // sha
            // 
            this.sha.HeaderText = "SHA";
            this.sha.Name = "sha";
            this.sha.ReadOnly = true;
            this.sha.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.sha.Visible = false;
            // 
            // UlozitButton
            // 
            this.UlozitButton.Enabled = false;
            this.UlozitButton.Location = new System.Drawing.Point(419, 448);
            this.UlozitButton.Name = "UlozitButton";
            this.UlozitButton.Size = new System.Drawing.Size(75, 23);
            this.UlozitButton.TabIndex = 14;
            this.UlozitButton.Text = "Uložit";
            this.UlozitButton.UseVisualStyleBackColor = true;
            this.UlozitButton.Click += new System.EventHandler(this.UlozitButon_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(683, 483);
            this.Controls.Add(this.UlozitButton);
            this.Controls.Add(this.TabulkaCommitu);
            this.Controls.Add(this.ExportButton);
            this.Controls.Add(this.GrafButton);
            this.Controls.Add(this.Kontrolka);
            this.Controls.Add(this.ClearLogBoxButton);
            this.Controls.Add(this.TimeShower);
            this.Controls.Add(this.LogBox);
            this.Controls.Add(this.RefreshButton);
            this.Name = "MainForm";
            this.Text = "Projekt STI";
            ((System.ComponentModel.ISupportInitialize)(this.Kontrolka)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TabulkaCommitu)).EndInit();
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
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.DataGridView TabulkaCommitu;
        private System.Windows.Forms.DataGridViewTextBoxColumn nazev;
        private System.Windows.Forms.DataGridViewTextBoxColumn datum;
        private System.Windows.Forms.DataGridViewTextBoxColumn sha;
        private System.Windows.Forms.Button UlozitButton;
    }
}

