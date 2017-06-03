namespace pkmn_ntr.Sub_forms
{
    partial class PokeDigger
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PokeDigger));
            this.DoItBtn = new System.Windows.Forms.Button();
            this.InfoLabel = new System.Windows.Forms.Label();
            this.ResultsGrid = new System.Windows.Forms.DataGridView();
            this.ColIcon = new System.Windows.Forms.DataGridViewImageColumn();
            this.ColLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDump = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColPID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColOffset = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DumpBtn = new System.Windows.Forms.Button();
            this.StartAddrText = new System.Windows.Forms.TextBox();
            this.StartLabel = new System.Windows.Forms.Label();
            this.SizeLabel = new System.Windows.Forms.Label();
            this.SizeText = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.DigWorker = new System.ComponentModel.BackgroundWorker();
            this.FromFileBtn = new System.Windows.Forms.Button();
            this.ClearBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.UglyMultithreadingHack = new System.Windows.Forms.Label();
            this.StepPlusBtn = new System.Windows.Forms.Button();
            this.StepMinusBtn = new System.Windows.Forms.Button();
            this.FastModeCB = new System.Windows.Forms.CheckBox();
            this.InvertBtn = new System.Windows.Forms.Button();
            this.DumpSaveCB = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.HelpBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ResultsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // DoItBtn
            // 
            this.DoItBtn.Location = new System.Drawing.Point(123, 59);
            this.DoItBtn.Name = "DoItBtn";
            this.DoItBtn.Size = new System.Drawing.Size(92, 23);
            this.DoItBtn.TabIndex = 4;
            this.DoItBtn.Text = "Can you dig it?";
            this.toolTip1.SetToolTip(this.DoItBtn, "Dump memory from console and process it");
            this.DoItBtn.UseVisualStyleBackColor = true;
            this.DoItBtn.Click += new System.EventHandler(this.DoItBtn_Click);
            // 
            // InfoLabel
            // 
            this.InfoLabel.AutoSize = true;
            this.InfoLabel.Location = new System.Drawing.Point(12, 9);
            this.InfoLabel.Name = "InfoLabel";
            this.InfoLabel.Size = new System.Drawing.Size(370, 13);
            this.InfoLabel.TabIndex = 14;
            this.InfoLabel.Text = "Experimental tool for extracting any valid-looking Pokemon data from memory.";
            // 
            // ResultsGrid
            // 
            this.ResultsGrid.AllowUserToAddRows = false;
            this.ResultsGrid.AllowUserToDeleteRows = false;
            this.ResultsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ResultsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColIcon,
            this.ColLevel,
            this.ColName,
            this.ColDump,
            this.ColPID,
            this.ColOffset});
            this.ResultsGrid.Location = new System.Drawing.Point(15, 118);
            this.ResultsGrid.Name = "ResultsGrid";
            this.ResultsGrid.Size = new System.Drawing.Size(457, 162);
            this.ResultsGrid.TabIndex = 8;
            // 
            // ColIcon
            // 
            this.ColIcon.HeaderText = "Species";
            this.ColIcon.Name = "ColIcon";
            this.ColIcon.ReadOnly = true;
            this.ColIcon.Width = 50;
            // 
            // ColLevel
            // 
            this.ColLevel.HeaderText = "Level";
            this.ColLevel.Name = "ColLevel";
            this.ColLevel.ReadOnly = true;
            this.ColLevel.Width = 50;
            // 
            // ColName
            // 
            this.ColName.HeaderText = "Name";
            this.ColName.Name = "ColName";
            this.ColName.ReadOnly = true;
            this.ColName.Width = 120;
            // 
            // ColDump
            // 
            this.ColDump.HeaderText = "Dump it?";
            this.ColDump.Name = "ColDump";
            this.ColDump.ToolTipText = "Check this box and click \"Dump to file(s)\" to save this Pokemon\'s data to file.";
            this.ColDump.Width = 60;
            // 
            // ColPID
            // 
            this.ColPID.HeaderText = "PID";
            this.ColPID.Name = "ColPID";
            this.ColPID.ReadOnly = true;
            this.ColPID.Width = 80;
            // 
            // ColOffset
            // 
            this.ColOffset.HeaderText = "Rel. offset";
            this.ColOffset.Name = "ColOffset";
            this.ColOffset.ReadOnly = true;
            this.ColOffset.ToolTipText = "Offset of this Pokemon\'s data, relative to the start of the dump.";
            this.ColOffset.Width = 80;
            // 
            // DumpBtn
            // 
            this.DumpBtn.Location = new System.Drawing.Point(12, 286);
            this.DumpBtn.Name = "DumpBtn";
            this.DumpBtn.Size = new System.Drawing.Size(96, 23);
            this.DumpBtn.TabIndex = 9;
            this.DumpBtn.Text = "Dump to file(s)";
            this.toolTip1.SetToolTip(this.DumpBtn, "Save selected Pokemon to .pk7 files");
            this.DumpBtn.UseVisualStyleBackColor = true;
            this.DumpBtn.Click += new System.EventHandler(this.DumpBtn_Click);
            // 
            // StartAddrText
            // 
            this.StartAddrText.Location = new System.Drawing.Point(77, 33);
            this.StartAddrText.Name = "StartAddrText";
            this.StartAddrText.Size = new System.Drawing.Size(100, 20);
            this.StartAddrText.TabIndex = 0;
            this.StartAddrText.Text = "0x32500000";
            this.toolTip1.SetToolTip(this.StartAddrText, "Memory address (in hexadecimal, in bytes) to start dumping from.\r\nDefault is 0x32" +
        "500000, just because I like it.");
            // 
            // StartLabel
            // 
            this.StartLabel.AutoSize = true;
            this.StartLabel.Location = new System.Drawing.Point(12, 36);
            this.StartLabel.Name = "StartLabel";
            this.StartLabel.Size = new System.Drawing.Size(59, 13);
            this.StartLabel.TabIndex = 16;
            this.StartLabel.Text = "Start addr.:";
            // 
            // SizeLabel
            // 
            this.SizeLabel.AutoSize = true;
            this.SizeLabel.Location = new System.Drawing.Point(183, 36);
            this.SizeLabel.Name = "SizeLabel";
            this.SizeLabel.Size = new System.Drawing.Size(30, 13);
            this.SizeLabel.TabIndex = 6;
            this.SizeLabel.Text = "Size:";
            // 
            // SizeText
            // 
            this.SizeText.Location = new System.Drawing.Point(219, 33);
            this.SizeText.Name = "SizeText";
            this.SizeText.Size = new System.Drawing.Size(100, 20);
            this.SizeText.TabIndex = 1;
            this.SizeText.Text = "0x00100000";
            this.toolTip1.SetToolTip(this.SizeText, "Amount of memory (in hexadecimal, in bytes) to dump.\r\nDefault is 0x00100000 (1 MB" +
        ").\r\nLarge amounts of memory may take a lot of time to transfer.");
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(15, 89);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(457, 23);
            this.progressBar.TabIndex = 18;
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(325, 36);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(43, 13);
            this.StatusLabel.TabIndex = 17;
            this.StatusLabel.Text = "Status: ";
            // 
            // DigWorker
            // 
            this.DigWorker.WorkerReportsProgress = true;
            this.DigWorker.WorkerSupportsCancellation = true;
            this.DigWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.DigWorker_DoWork);
            this.DigWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.DigWorker_ProgressChanged);
            this.DigWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.DigWorker_RunWorkerCompleted);
            // 
            // FromFileBtn
            // 
            this.FromFileBtn.Location = new System.Drawing.Point(397, 59);
            this.FromFileBtn.Name = "FromFileBtn";
            this.FromFileBtn.Size = new System.Drawing.Size(75, 23);
            this.FromFileBtn.TabIndex = 7;
            this.FromFileBtn.Text = "From file...";
            this.toolTip1.SetToolTip(this.FromFileBtn, "Read memory dump from file");
            this.FromFileBtn.UseVisualStyleBackColor = true;
            this.FromFileBtn.Click += new System.EventHandler(this.FromFileBtn_Click);
            // 
            // ClearBtn
            // 
            this.ClearBtn.Location = new System.Drawing.Point(210, 286);
            this.ClearBtn.Name = "ClearBtn";
            this.ClearBtn.Size = new System.Drawing.Size(75, 23);
            this.ClearBtn.TabIndex = 11;
            this.ClearBtn.Text = "Clear list";
            this.toolTip1.SetToolTip(this.ClearBtn, "Clear list of found Pokemon.\r\nThe list will not be cleared after a dump is analyz" +
        "ed\r\n(each new dump is appended to the end of the list).");
            this.ClearBtn.UseVisualStyleBackColor = true;
            this.ClearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Enabled = false;
            this.CancelBtn.Location = new System.Drawing.Point(291, 286);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 12;
            this.CancelBtn.Text = "Cancel";
            this.toolTip1.SetToolTip(this.CancelBtn, "Cancel whatever operation is in progress");
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // UglyMultithreadingHack
            // 
            this.UglyMultithreadingHack.AutoSize = true;
            this.UglyMultithreadingHack.Location = new System.Drawing.Point(411, 298);
            this.UglyMultithreadingHack.Name = "UglyMultithreadingHack";
            this.UglyMultithreadingHack.Size = new System.Drawing.Size(61, 13);
            this.UglyMultithreadingHack.TabIndex = 13;
            this.UglyMultithreadingHack.Text = "but it works";
            this.UglyMultithreadingHack.Visible = false;
            this.UglyMultithreadingHack.TextChanged += new System.EventHandler(this.UglyMultithreadingHack_TextChanged);
            // 
            // StepPlusBtn
            // 
            this.StepPlusBtn.Location = new System.Drawing.Point(15, 59);
            this.StepPlusBtn.Name = "StepPlusBtn";
            this.StepPlusBtn.Size = new System.Drawing.Size(48, 23);
            this.StepPlusBtn.TabIndex = 2;
            this.StepPlusBtn.Text = "Step +";
            this.toolTip1.SetToolTip(this.StepPlusBtn, "Increase start address by size value");
            this.StepPlusBtn.UseVisualStyleBackColor = true;
            this.StepPlusBtn.Click += new System.EventHandler(this.StepPlusBtn_Click);
            // 
            // StepMinusBtn
            // 
            this.StepMinusBtn.Location = new System.Drawing.Point(69, 59);
            this.StepMinusBtn.Name = "StepMinusBtn";
            this.StepMinusBtn.Size = new System.Drawing.Size(48, 23);
            this.StepMinusBtn.TabIndex = 3;
            this.StepMinusBtn.Text = "Step -";
            this.toolTip1.SetToolTip(this.StepMinusBtn, "Decrease start address by size value");
            this.StepMinusBtn.UseVisualStyleBackColor = true;
            this.StepMinusBtn.Click += new System.EventHandler(this.StepMinusBtn_Click);
            // 
            // FastModeCB
            // 
            this.FastModeCB.AutoSize = true;
            this.FastModeCB.Location = new System.Drawing.Point(221, 63);
            this.FastModeCB.Name = "FastModeCB";
            this.FastModeCB.Size = new System.Drawing.Size(75, 17);
            this.FastModeCB.TabIndex = 5;
            this.FastModeCB.Text = "Fast mode";
            this.toolTip1.SetToolTip(this.FastModeCB, "Click it to learn more");
            this.FastModeCB.UseVisualStyleBackColor = true;
            this.FastModeCB.CheckedChanged += new System.EventHandler(this.FastModeCB_CheckedChanged);
            // 
            // InvertBtn
            // 
            this.InvertBtn.Location = new System.Drawing.Point(114, 286);
            this.InvertBtn.Name = "InvertBtn";
            this.InvertBtn.Size = new System.Drawing.Size(90, 23);
            this.InvertBtn.TabIndex = 10;
            this.InvertBtn.Text = "Invert checked";
            this.toolTip1.SetToolTip(this.InvertBtn, "Pokemon checked in \"Dump it?\" column will become unchecked\r\nand vice versa");
            this.InvertBtn.UseVisualStyleBackColor = true;
            this.InvertBtn.Click += new System.EventHandler(this.InvertBtn_Click);
            // 
            // DumpSaveCB
            // 
            this.DumpSaveCB.AutoSize = true;
            this.DumpSaveCB.Location = new System.Drawing.Point(302, 63);
            this.DumpSaveCB.Name = "DumpSaveCB";
            this.DumpSaveCB.Size = new System.Drawing.Size(96, 17);
            this.DumpSaveCB.TabIndex = 6;
            this.DumpSaveCB.Text = "Save dump file";
            this.toolTip1.SetToolTip(this.DumpSaveCB, "If checked, the entire memory dump will be saved to .bin file");
            this.DumpSaveCB.UseVisualStyleBackColor = true;
            // 
            // HelpBtn
            // 
            this.HelpBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.HelpBtn.Location = new System.Drawing.Point(449, 12);
            this.HelpBtn.Name = "HelpBtn";
            this.HelpBtn.Size = new System.Drawing.Size(23, 23);
            this.HelpBtn.TabIndex = 15;
            this.HelpBtn.Text = "?";
            this.toolTip1.SetToolTip(this.HelpBtn, "What do I do now???");
            this.HelpBtn.UseVisualStyleBackColor = true;
            this.HelpBtn.Click += new System.EventHandler(this.HelpBtn_Click);
            // 
            // PokeDigger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(484, 320);
            this.Controls.Add(this.HelpBtn);
            this.Controls.Add(this.DumpSaveCB);
            this.Controls.Add(this.InvertBtn);
            this.Controls.Add(this.FastModeCB);
            this.Controls.Add(this.StepMinusBtn);
            this.Controls.Add(this.StepPlusBtn);
            this.Controls.Add(this.UglyMultithreadingHack);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.ClearBtn);
            this.Controls.Add(this.FromFileBtn);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.SizeText);
            this.Controls.Add(this.SizeLabel);
            this.Controls.Add(this.StartLabel);
            this.Controls.Add(this.StartAddrText);
            this.Controls.Add(this.DumpBtn);
            this.Controls.Add(this.ResultsGrid);
            this.Controls.Add(this.InfoLabel);
            this.Controls.Add(this.DoItBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PokeDigger";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 6, 6);
            this.Text = "PokeDigger";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PokeDigger_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.ResultsGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DoItBtn;
        private System.Windows.Forms.Label InfoLabel;
        private System.Windows.Forms.DataGridView ResultsGrid;
        private System.Windows.Forms.Button DumpBtn;
        private System.Windows.Forms.TextBox StartAddrText;
        private System.Windows.Forms.Label StartLabel;
        private System.Windows.Forms.Label SizeLabel;
        private System.Windows.Forms.TextBox SizeText;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label StatusLabel;
        private System.ComponentModel.BackgroundWorker DigWorker;
        private System.Windows.Forms.Button FromFileBtn;
        private System.Windows.Forms.Button ClearBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Label UglyMultithreadingHack;
        private System.Windows.Forms.Button StepPlusBtn;
        private System.Windows.Forms.Button StepMinusBtn;
        private System.Windows.Forms.CheckBox FastModeCB;
        private System.Windows.Forms.Button InvertBtn;
        private System.Windows.Forms.DataGridViewImageColumn ColIcon;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColDump;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColOffset;
        private System.Windows.Forms.CheckBox DumpSaveCB;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button HelpBtn;
    }
}