namespace pkmn_ntr
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.txtLog = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.disconnectTimer = new System.Windows.Forms.Timer(this.components);
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.host = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.DumpInstructionsBtn = new System.Windows.Forms.Button();
            this.Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lb_update = new System.Windows.Forms.Label();
            this.backupPKM = new System.Windows.Forms.CheckBox();
            this.readResult = new System.Windows.Forms.TextBox();
            this.label71 = new System.Windows.Forms.Label();
            this.radioBoxes = new System.Windows.Forms.RadioButton();
            this.radioDaycare = new System.Windows.Forms.RadioButton();
            this.dumpBoxes = new System.Windows.Forms.Button();
            this.slotDump = new System.Windows.Forms.NumericUpDown();
            this.boxDump = new System.Windows.Forms.NumericUpDown();
            this.radioOpponent = new System.Windows.Forms.RadioButton();
            this.radioTrade = new System.Windows.Forms.RadioButton();
            this.SlotLabel = new System.Windows.Forms.Label();
            this.radioParty = new System.Windows.Forms.RadioButton();
            this.dumpPokemon = new System.Windows.Forms.Button();
            this.radioBattleBox = new System.Windows.Forms.RadioButton();
            this.BoxLabel = new System.Windows.Forms.Label();
            this.Tool_Trainer = new System.Windows.Forms.Button();
            this.Tabs_General = new System.Windows.Forms.TabControl();
            this.Tab_Dump = new System.Windows.Forms.TabPage();
            this.Write_PKM = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Tab_Clone = new System.Windows.Forms.TabPage();
            this.Btn_CDstart = new System.Windows.Forms.Button();
            this.CB_CDBackup = new System.Windows.Forms.CheckBox();
            this.GB_CDmode = new System.Windows.Forms.GroupBox();
            this.DeleteMode = new System.Windows.Forms.RadioButton();
            this.CloneMode = new System.Windows.Forms.RadioButton();
            this.Num_CDBox = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.Num_CDAmount = new System.Windows.Forms.NumericUpDown();
            this.Num_CDSlot = new System.Windows.Forms.NumericUpDown();
            this.Tab_Tools = new System.Windows.Forms.TabPage();
            this.resetNoBox = new System.Windows.Forms.TextBox();
            this.labelreset = new System.Windows.Forms.Label();
            this.Btn_ReloadFields = new System.Windows.Forms.Button();
            this.Seed_Legendary = new System.Windows.Forms.TextBox();
            this.Seed_Egg = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.Tool_Items = new System.Windows.Forms.Button();
            this.Tool_Controls = new System.Windows.Forms.Button();
            this.Tools_Breeding = new System.Windows.Forms.Button();
            this.Tools_SoftReset = new System.Windows.Forms.Button();
            this.Tools_WonderTrade = new System.Windows.Forms.Button();
            this.Tool_Script = new System.Windows.Forms.Button();
            this.Tools_PokeDigger = new System.Windows.Forms.Button();
            this.Tools_Filter = new System.Windows.Forms.Button();
            this.EventHandlerButton = new System.Windows.Forms.Button();
            this.PollingButton = new System.Windows.Forms.Button();
            this.Tab_Log = new System.Windows.Forms.TabPage();
            this.Log_Export = new System.Windows.Forms.Button();
            this.Tab_About = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_name = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lb_version = new System.Windows.Forms.Label();
            this.lb_tid = new System.Windows.Forms.Label();
            this.lb_sid = new System.Windows.Forms.Label();
            this.lb_g7id = new System.Windows.Forms.Label();
            this.lb_tsv = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lb_pkmnntrver = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lb_pkhexcorever = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.dragout = new System.Windows.Forms.PictureBox();
            this.EventPollingWorker = new System.ComponentModel.BackgroundWorker();
            this.PB_Legal = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slotDump)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.boxDump)).BeginInit();
            this.Tabs_General.SuspendLayout();
            this.Tab_Dump.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.Tab_Clone.SuspendLayout();
            this.GB_CDmode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_CDBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_CDAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_CDSlot)).BeginInit();
            this.Tab_Tools.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.Tab_Log.SuspendLayout();
            this.Tab_About.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dragout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Legal)).BeginInit();
            this.SuspendLayout();
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.Color.Black;
            this.txtLog.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtLog.ForeColor = System.Drawing.Color.LawnGreen;
            this.txtLog.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtLog.Location = new System.Drawing.Point(4, 0);
            this.txtLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtLog.MaxLength = 32767000;
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(380, 285);
            this.txtLog.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.SendHeartbeat);
            // 
            // disconnectTimer
            // 
            this.disconnectTimer.Interval = 10000;
            this.disconnectTimer.Tick += new System.EventHandler(this.AutoDisconnect);
            // 
            // buttonConnect
            // 
            this.buttonConnect.AutoSize = true;
            this.buttonConnect.Location = new System.Drawing.Point(153, 4);
            this.buttonConnect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(111, 33);
            this.buttonConnect.TabIndex = 1;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.StartConnecting);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.AutoSize = true;
            this.buttonDisconnect.Enabled = false;
            this.buttonDisconnect.Location = new System.Drawing.Point(4, 45);
            this.buttonDisconnect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(117, 33);
            this.buttonDisconnect.TabIndex = 2;
            this.buttonDisconnect.Text = "Disconnect";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.StartDisconnecting);
            // 
            // host
            // 
            this.host.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.host.Location = new System.Drawing.Point(36, 9);
            this.host.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.host.Name = "host";
            this.host.Size = new System.Drawing.Size(109, 22);
            this.host.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(16, 379);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(400, 62);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.host);
            this.flowLayoutPanel1.Controls.Add(this.buttonConnect);
            this.flowLayoutPanel1.Controls.Add(this.buttonDisconnect);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(4, 19);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(392, 82);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "IP:";
            // 
            // DumpInstructionsBtn
            // 
            this.DumpInstructionsBtn.Location = new System.Drawing.Point(281, 289);
            this.DumpInstructionsBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DumpInstructionsBtn.Name = "DumpInstructionsBtn";
            this.DumpInstructionsBtn.Size = new System.Drawing.Size(100, 28);
            this.DumpInstructionsBtn.TabIndex = 7;
            this.DumpInstructionsBtn.Text = "How to use";
            this.DumpInstructionsBtn.UseVisualStyleBackColor = true;
            this.DumpInstructionsBtn.Visible = false;
            this.DumpInstructionsBtn.Click += new System.EventHandler(this.DumpInstructionsBtn_Click);
            // 
            // Item
            // 
            this.Item.Name = "Item";
            // 
            // Amount
            // 
            this.Amount.Name = "Amount";
            // 
            // lb_update
            // 
            this.lb_update.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_update.AutoSize = true;
            this.lb_update.Location = new System.Drawing.Point(103, 266);
            this.lb_update.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_update.Name = "lb_update";
            this.lb_update.Size = new System.Drawing.Size(277, 17);
            this.lb_update.TabIndex = 4;
            this.lb_update.Text = "Looking for updates...";
            this.lb_update.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lb_update.Click += new System.EventHandler(this.ClickUpdateLabel);
            // 
            // backupPKM
            // 
            this.backupPKM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.backupPKM.AutoSize = true;
            this.backupPKM.Location = new System.Drawing.Point(292, 33);
            this.backupPKM.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.backupPKM.Name = "backupPKM";
            this.backupPKM.Size = new System.Drawing.Size(77, 21);
            this.backupPKM.TabIndex = 3;
            this.backupPKM.Text = "Backup";
            this.toolTip1.SetToolTip(this.backupPKM, "When activated, it saves a copy of the dumped data in the \"Pokemon\" folder.");
            this.backupPKM.UseVisualStyleBackColor = true;
            // 
            // readResult
            // 
            this.readResult.Location = new System.Drawing.Point(131, 293);
            this.readResult.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.readResult.Name = "readResult";
            this.readResult.ReadOnly = true;
            this.readResult.Size = new System.Drawing.Size(119, 22);
            this.readResult.TabIndex = 1;
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(8, 297);
            this.label71.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(111, 17);
            this.label71.TabIndex = 2;
            this.label71.Text = "Last RAM Read:";
            // 
            // radioBoxes
            // 
            this.radioBoxes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.radioBoxes.AutoSize = true;
            this.radioBoxes.Checked = true;
            this.radioBoxes.Location = new System.Drawing.Point(4, 6);
            this.radioBoxes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioBoxes.Name = "radioBoxes";
            this.radioBoxes.Size = new System.Drawing.Size(116, 21);
            this.radioBoxes.TabIndex = 0;
            this.radioBoxes.TabStop = true;
            this.radioBoxes.Text = "Boxes";
            this.radioBoxes.UseVisualStyleBackColor = true;
            this.radioBoxes.CheckedChanged += new System.EventHandler(this.ActiveBoxes);
            // 
            // radioDaycare
            // 
            this.radioDaycare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.radioDaycare.AutoSize = true;
            this.radioDaycare.Location = new System.Drawing.Point(128, 6);
            this.radioDaycare.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioDaycare.Name = "radioDaycare";
            this.radioDaycare.Size = new System.Drawing.Size(116, 21);
            this.radioDaycare.TabIndex = 1;
            this.radioDaycare.Text = "Daycare";
            this.radioDaycare.UseVisualStyleBackColor = true;
            this.radioDaycare.CheckedChanged += new System.EventHandler(this.ActiveDaycare);
            // 
            // dumpBoxes
            // 
            this.dumpBoxes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dumpBoxes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dumpBoxes.Location = new System.Drawing.Point(260, 172);
            this.dumpBoxes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dumpBoxes.Name = "dumpBoxes";
            this.dumpBoxes.Size = new System.Drawing.Size(121, 28);
            this.dumpBoxes.TabIndex = 6;
            this.dumpBoxes.Text = "Dump All Boxes";
            this.dumpBoxes.UseVisualStyleBackColor = true;
            this.dumpBoxes.Click += new System.EventHandler(this.DumpBoxes);
            // 
            // slotDump
            // 
            this.slotDump.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.slotDump.AutoSize = true;
            this.slotDump.Location = new System.Drawing.Point(69, 30);
            this.slotDump.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.slotDump.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.slotDump.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.slotDump.Name = "slotDump";
            this.slotDump.Size = new System.Drawing.Size(59, 22);
            this.slotDump.TabIndex = 1;
            this.slotDump.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // boxDump
            // 
            this.boxDump.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.boxDump.Location = new System.Drawing.Point(8, 30);
            this.boxDump.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.boxDump.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.boxDump.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.boxDump.Name = "boxDump";
            this.boxDump.Size = new System.Drawing.Size(53, 22);
            this.boxDump.TabIndex = 0;
            this.boxDump.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // radioOpponent
            // 
            this.radioOpponent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.radioOpponent.AutoSize = true;
            this.radioOpponent.Location = new System.Drawing.Point(128, 39);
            this.radioOpponent.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioOpponent.Name = "radioOpponent";
            this.radioOpponent.Size = new System.Drawing.Size(116, 21);
            this.radioOpponent.TabIndex = 4;
            this.radioOpponent.TabStop = true;
            this.radioOpponent.Text = "Opponent";
            this.radioOpponent.UseVisualStyleBackColor = true;
            this.radioOpponent.CheckedChanged += new System.EventHandler(this.ActiveOpponent);
            // 
            // radioTrade
            // 
            this.radioTrade.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.radioTrade.AutoSize = true;
            this.radioTrade.Location = new System.Drawing.Point(4, 39);
            this.radioTrade.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioTrade.Name = "radioTrade";
            this.radioTrade.Size = new System.Drawing.Size(116, 21);
            this.radioTrade.TabIndex = 3;
            this.radioTrade.TabStop = true;
            this.radioTrade.Text = "Trade";
            this.radioTrade.UseVisualStyleBackColor = true;
            this.radioTrade.CheckedChanged += new System.EventHandler(this.ActiveTrade);
            // 
            // SlotLabel
            // 
            this.SlotLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.SlotLabel.AutoSize = true;
            this.SlotLabel.Location = new System.Drawing.Point(65, 10);
            this.SlotLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SlotLabel.Name = "SlotLabel";
            this.SlotLabel.Size = new System.Drawing.Size(36, 17);
            this.SlotLabel.TabIndex = 13;
            this.SlotLabel.Text = "Slot:";
            // 
            // radioParty
            // 
            this.radioParty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.radioParty.AutoSize = true;
            this.radioParty.Location = new System.Drawing.Point(252, 39);
            this.radioParty.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioParty.Name = "radioParty";
            this.radioParty.Size = new System.Drawing.Size(117, 21);
            this.radioParty.TabIndex = 5;
            this.radioParty.Text = "Party";
            this.radioParty.UseVisualStyleBackColor = true;
            this.radioParty.CheckedChanged += new System.EventHandler(this.ActiveParty);
            // 
            // dumpPokemon
            // 
            this.dumpPokemon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dumpPokemon.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dumpPokemon.Location = new System.Drawing.Point(131, 27);
            this.dumpPokemon.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dumpPokemon.Name = "dumpPokemon";
            this.dumpPokemon.Size = new System.Drawing.Size(153, 28);
            this.dumpPokemon.TabIndex = 2;
            this.dumpPokemon.Text = "Read Pokémon";
            this.dumpPokemon.UseVisualStyleBackColor = true;
            this.dumpPokemon.Click += new System.EventHandler(this.DumpPokemon);
            // 
            // radioBattleBox
            // 
            this.radioBattleBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.radioBattleBox.AutoSize = true;
            this.radioBattleBox.Location = new System.Drawing.Point(252, 6);
            this.radioBattleBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioBattleBox.Name = "radioBattleBox";
            this.radioBattleBox.Size = new System.Drawing.Size(117, 21);
            this.radioBattleBox.TabIndex = 2;
            this.radioBattleBox.Text = "Battle Box";
            this.radioBattleBox.UseVisualStyleBackColor = true;
            this.radioBattleBox.CheckedChanged += new System.EventHandler(this.ActiveBattleBox);
            // 
            // BoxLabel
            // 
            this.BoxLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.BoxLabel.AutoSize = true;
            this.BoxLabel.Location = new System.Drawing.Point(8, 10);
            this.BoxLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.BoxLabel.Name = "BoxLabel";
            this.BoxLabel.Size = new System.Drawing.Size(35, 17);
            this.BoxLabel.TabIndex = 12;
            this.BoxLabel.Text = "Box:";
            // 
            // Tool_Trainer
            // 
            this.Tool_Trainer.Location = new System.Drawing.Point(4, 4);
            this.Tool_Trainer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Tool_Trainer.Name = "Tool_Trainer";
            this.Tool_Trainer.Size = new System.Drawing.Size(119, 28);
            this.Tool_Trainer.TabIndex = 0;
            this.Tool_Trainer.Text = "Edit Trainer";
            this.Tool_Trainer.UseVisualStyleBackColor = true;
            this.Tool_Trainer.Click += new System.EventHandler(this.Tool_Trainer_Click);
            // 
            // Tabs_General
            // 
            this.Tabs_General.Controls.Add(this.Tab_Dump);
            this.Tabs_General.Controls.Add(this.Tab_Clone);
            this.Tabs_General.Controls.Add(this.Tab_Tools);
            this.Tabs_General.Controls.Add(this.Tab_Log);
            this.Tabs_General.Controls.Add(this.Tab_About);
            this.Tabs_General.Location = new System.Drawing.Point(16, 15);
            this.Tabs_General.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Tabs_General.Name = "Tabs_General";
            this.Tabs_General.SelectedIndex = 0;
            this.Tabs_General.Size = new System.Drawing.Size(400, 357);
            this.Tabs_General.TabIndex = 1;
            // 
            // Tab_Dump
            // 
            this.Tab_Dump.BackColor = System.Drawing.SystemColors.Control;
            this.Tab_Dump.Controls.Add(this.Write_PKM);
            this.Tab_Dump.Controls.Add(this.dumpBoxes);
            this.Tab_Dump.Controls.Add(this.dumpPokemon);
            this.Tab_Dump.Controls.Add(this.DumpInstructionsBtn);
            this.Tab_Dump.Controls.Add(this.backupPKM);
            this.Tab_Dump.Controls.Add(this.slotDump);
            this.Tab_Dump.Controls.Add(this.BoxLabel);
            this.Tab_Dump.Controls.Add(this.SlotLabel);
            this.Tab_Dump.Controls.Add(this.boxDump);
            this.Tab_Dump.Controls.Add(this.tableLayoutPanel1);
            this.Tab_Dump.Location = new System.Drawing.Point(4, 25);
            this.Tab_Dump.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Tab_Dump.Name = "Tab_Dump";
            this.Tab_Dump.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Tab_Dump.Size = new System.Drawing.Size(392, 328);
            this.Tab_Dump.TabIndex = 4;
            this.Tab_Dump.Text = "Read/Write";
            // 
            // Write_PKM
            // 
            this.Write_PKM.Enabled = false;
            this.Write_PKM.Location = new System.Drawing.Point(8, 137);
            this.Write_PKM.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Write_PKM.Name = "Write_PKM";
            this.Write_PKM.Size = new System.Drawing.Size(373, 28);
            this.Write_PKM.TabIndex = 5;
            this.Write_PKM.Text = "Write Pokémon";
            this.Write_PKM.UseVisualStyleBackColor = true;
            this.Write_PKM.Click += new System.EventHandler(this.InjectPokemon);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.radioParty, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.radioBoxes, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.radioDaycare, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.radioOpponent, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.radioBattleBox, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.radioTrade, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 63);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(373, 66);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // Tab_Clone
            // 
            this.Tab_Clone.BackColor = System.Drawing.SystemColors.Control;
            this.Tab_Clone.Controls.Add(this.Btn_CDstart);
            this.Tab_Clone.Controls.Add(this.CB_CDBackup);
            this.Tab_Clone.Controls.Add(this.GB_CDmode);
            this.Tab_Clone.Controls.Add(this.Num_CDBox);
            this.Tab_Clone.Controls.Add(this.label16);
            this.Tab_Clone.Controls.Add(this.label14);
            this.Tab_Clone.Controls.Add(this.label15);
            this.Tab_Clone.Controls.Add(this.Num_CDAmount);
            this.Tab_Clone.Controls.Add(this.Num_CDSlot);
            this.Tab_Clone.Location = new System.Drawing.Point(4, 25);
            this.Tab_Clone.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Tab_Clone.Name = "Tab_Clone";
            this.Tab_Clone.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Tab_Clone.Size = new System.Drawing.Size(392, 328);
            this.Tab_Clone.TabIndex = 1;
            this.Tab_Clone.Text = "Clone/Delete";
            // 
            // Btn_CDstart
            // 
            this.Btn_CDstart.Location = new System.Drawing.Point(209, 87);
            this.Btn_CDstart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Btn_CDstart.Name = "Btn_CDstart";
            this.Btn_CDstart.Size = new System.Drawing.Size(172, 28);
            this.Btn_CDstart.TabIndex = 4;
            this.Btn_CDstart.Text = "Go!";
            this.toolTip1.SetToolTip(this.Btn_CDstart, "Pokémon will be cloned or deleted starting at the specified position.");
            this.Btn_CDstart.UseVisualStyleBackColor = true;
            this.Btn_CDstart.Click += new System.EventHandler(this.StartCloneDelete);
            // 
            // CB_CDBackup
            // 
            this.CB_CDBackup.AutoSize = true;
            this.CB_CDBackup.Location = new System.Drawing.Point(209, 32);
            this.CB_CDBackup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CB_CDBackup.Name = "CB_CDBackup";
            this.CB_CDBackup.Size = new System.Drawing.Size(113, 21);
            this.CB_CDBackup.TabIndex = 0;
            this.CB_CDBackup.Text = "Keep backup";
            this.CB_CDBackup.UseVisualStyleBackColor = true;
            // 
            // GB_CDmode
            // 
            this.GB_CDmode.Controls.Add(this.DeleteMode);
            this.GB_CDmode.Controls.Add(this.CloneMode);
            this.GB_CDmode.Location = new System.Drawing.Point(8, 7);
            this.GB_CDmode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GB_CDmode.Name = "GB_CDmode";
            this.GB_CDmode.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GB_CDmode.Size = new System.Drawing.Size(193, 60);
            this.GB_CDmode.TabIndex = 0;
            this.GB_CDmode.TabStop = false;
            this.GB_CDmode.Text = "Mode";
            // 
            // DeleteMode
            // 
            this.DeleteMode.AutoSize = true;
            this.DeleteMode.Location = new System.Drawing.Point(111, 23);
            this.DeleteMode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DeleteMode.Name = "DeleteMode";
            this.DeleteMode.Size = new System.Drawing.Size(70, 21);
            this.DeleteMode.TabIndex = 1;
            this.DeleteMode.Text = "Delete";
            this.DeleteMode.UseVisualStyleBackColor = true;
            // 
            // CloneMode
            // 
            this.CloneMode.AutoSize = true;
            this.CloneMode.Checked = true;
            this.CloneMode.Location = new System.Drawing.Point(8, 23);
            this.CloneMode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CloneMode.Name = "CloneMode";
            this.CloneMode.Size = new System.Drawing.Size(65, 21);
            this.CloneMode.TabIndex = 0;
            this.CloneMode.TabStop = true;
            this.CloneMode.Text = "Clone";
            this.toolTip1.SetToolTip(this.CloneMode, "Source is the pokémon shown in the tabs.");
            this.CloneMode.UseVisualStyleBackColor = true;
            // 
            // Num_CDBox
            // 
            this.Num_CDBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Num_CDBox.Location = new System.Drawing.Point(12, 91);
            this.Num_CDBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Num_CDBox.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.Num_CDBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_CDBox.Name = "Num_CDBox";
            this.Num_CDBox.Size = new System.Drawing.Size(53, 22);
            this.Num_CDBox.TabIndex = 1;
            this.Num_CDBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_CDBox.ValueChanged += new System.EventHandler(this.UpdateMaxCloneDelete);
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(131, 71);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(60, 17);
            this.label16.TabIndex = 13;
            this.label16.Text = "Amount:";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(69, 71);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(36, 17);
            this.label14.TabIndex = 13;
            this.label14.Text = "Slot:";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 71);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(35, 17);
            this.label15.TabIndex = 12;
            this.label15.Text = "Box:";
            // 
            // Num_CDAmount
            // 
            this.Num_CDAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Num_CDAmount.AutoSize = true;
            this.Num_CDAmount.Location = new System.Drawing.Point(135, 91);
            this.Num_CDAmount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Num_CDAmount.Maximum = new decimal(new int[] {
            930,
            0,
            0,
            0});
            this.Num_CDAmount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_CDAmount.Name = "Num_CDAmount";
            this.Num_CDAmount.Size = new System.Drawing.Size(69, 22);
            this.Num_CDAmount.TabIndex = 3;
            this.Num_CDAmount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Num_CDSlot
            // 
            this.Num_CDSlot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Num_CDSlot.AutoSize = true;
            this.Num_CDSlot.Location = new System.Drawing.Point(73, 91);
            this.Num_CDSlot.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Num_CDSlot.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.Num_CDSlot.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_CDSlot.Name = "Num_CDSlot";
            this.Num_CDSlot.Size = new System.Drawing.Size(59, 22);
            this.Num_CDSlot.TabIndex = 2;
            this.Num_CDSlot.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_CDSlot.ValueChanged += new System.EventHandler(this.UpdateMaxCloneDelete);
            // 
            // Tab_Tools
            // 
            this.Tab_Tools.BackColor = System.Drawing.SystemColors.Control;
            this.Tab_Tools.Controls.Add(this.resetNoBox);
            this.Tab_Tools.Controls.Add(this.labelreset);
            this.Tab_Tools.Controls.Add(this.Btn_ReloadFields);
            this.Tab_Tools.Controls.Add(this.Seed_Legendary);
            this.Tab_Tools.Controls.Add(this.Seed_Egg);
            this.Tab_Tools.Controls.Add(this.label19);
            this.Tab_Tools.Controls.Add(this.label18);
            this.Tab_Tools.Controls.Add(this.flowLayoutPanel2);
            this.Tab_Tools.Location = new System.Drawing.Point(4, 25);
            this.Tab_Tools.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Tab_Tools.Name = "Tab_Tools";
            this.Tab_Tools.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Tab_Tools.Size = new System.Drawing.Size(392, 328);
            this.Tab_Tools.TabIndex = 0;
            this.Tab_Tools.Text = "Tools";
            // 
            // resetNoBox
            // 
            this.resetNoBox.Location = new System.Drawing.Point(136, 252);
            this.resetNoBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.resetNoBox.Name = "resetNoBox";
            this.resetNoBox.Size = new System.Drawing.Size(116, 22);
            this.resetNoBox.TabIndex = 17;
            // 
            // labelreset
            // 
            this.labelreset.Location = new System.Drawing.Point(8, 256);
            this.labelreset.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelreset.Name = "labelreset";
            this.labelreset.Size = new System.Drawing.Size(120, 16);
            this.labelreset.TabIndex = 16;
            this.labelreset.Text = "Starting number:";
            this.labelreset.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Btn_ReloadFields
            // 
            this.Btn_ReloadFields.Location = new System.Drawing.Point(261, 252);
            this.Btn_ReloadFields.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Btn_ReloadFields.Name = "Btn_ReloadFields";
            this.Btn_ReloadFields.Size = new System.Drawing.Size(119, 28);
            this.Btn_ReloadFields.TabIndex = 3;
            this.Btn_ReloadFields.Text = "Reload Fields";
            this.toolTip1.SetToolTip(this.Btn_ReloadFields, "Also reloads the \"About\" tab.");
            this.Btn_ReloadFields.UseVisualStyleBackColor = true;
            this.Btn_ReloadFields.Click += new System.EventHandler(this.Btn_ReloadFields_Click);
            // 
            // Seed_Legendary
            // 
            this.Seed_Legendary.Location = new System.Drawing.Point(8, 220);
            this.Seed_Legendary.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Seed_Legendary.Name = "Seed_Legendary";
            this.Seed_Legendary.ReadOnly = true;
            this.Seed_Legendary.Size = new System.Drawing.Size(371, 22);
            this.Seed_Legendary.TabIndex = 2;
            // 
            // Seed_Egg
            // 
            this.Seed_Egg.Location = new System.Drawing.Point(8, 172);
            this.Seed_Egg.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Seed_Egg.Name = "Seed_Egg";
            this.Seed_Egg.ReadOnly = true;
            this.Seed_Egg.Size = new System.Drawing.Size(371, 22);
            this.Seed_Egg.TabIndex = 1;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(8, 201);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(117, 17);
            this.label19.TabIndex = 1;
            this.label19.Text = "Legendary Seed:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(8, 153);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(74, 17);
            this.label18.TabIndex = 1;
            this.label18.Text = "Egg Seed:";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.Tool_Trainer);
            this.flowLayoutPanel2.Controls.Add(this.Tool_Items);
            this.flowLayoutPanel2.Controls.Add(this.Tool_Controls);
            this.flowLayoutPanel2.Controls.Add(this.Tools_Breeding);
            this.flowLayoutPanel2.Controls.Add(this.Tools_SoftReset);
            this.flowLayoutPanel2.Controls.Add(this.Tools_WonderTrade);
            this.flowLayoutPanel2.Controls.Add(this.Tool_Script);
            this.flowLayoutPanel2.Controls.Add(this.Tools_PokeDigger);
            this.flowLayoutPanel2.Controls.Add(this.Tools_Filter);
            this.flowLayoutPanel2.Controls.Add(this.EventHandlerButton);
            this.flowLayoutPanel2.Controls.Add(this.PollingButton);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(4, 4);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(384, 144);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // Tool_Items
            // 
            this.Tool_Items.Location = new System.Drawing.Point(131, 4);
            this.Tool_Items.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Tool_Items.Name = "Tool_Items";
            this.Tool_Items.Size = new System.Drawing.Size(119, 28);
            this.Tool_Items.TabIndex = 1;
            this.Tool_Items.Text = "Edit Items";
            this.Tool_Items.UseVisualStyleBackColor = true;
            this.Tool_Items.Click += new System.EventHandler(this.Tool_Items_Click);
            // 
            // Tool_Controls
            // 
            this.Tool_Controls.Location = new System.Drawing.Point(258, 4);
            this.Tool_Controls.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Tool_Controls.Name = "Tool_Controls";
            this.Tool_Controls.Size = new System.Drawing.Size(119, 28);
            this.Tool_Controls.TabIndex = 2;
            this.Tool_Controls.Text = "Remote Control";
            this.Tool_Controls.UseVisualStyleBackColor = true;
            this.Tool_Controls.Click += new System.EventHandler(this.Tool_Controls_Click);
            // 
            // Tools_Breeding
            // 
            this.Tools_Breeding.Location = new System.Drawing.Point(4, 40);
            this.Tools_Breeding.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Tools_Breeding.Name = "Tools_Breeding";
            this.Tools_Breeding.Size = new System.Drawing.Size(119, 28);
            this.Tools_Breeding.TabIndex = 3;
            this.Tools_Breeding.Text = "Breeding Bot";
            this.Tools_Breeding.UseVisualStyleBackColor = true;
            this.Tools_Breeding.Click += new System.EventHandler(this.Tools_Breeding_Click);
            // 
            // Tools_SoftReset
            // 
            this.Tools_SoftReset.Location = new System.Drawing.Point(131, 40);
            this.Tools_SoftReset.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Tools_SoftReset.Name = "Tools_SoftReset";
            this.Tools_SoftReset.Size = new System.Drawing.Size(119, 28);
            this.Tools_SoftReset.TabIndex = 4;
            this.Tools_SoftReset.Text = "Soft-reset Bot";
            this.Tools_SoftReset.UseVisualStyleBackColor = true;
            this.Tools_SoftReset.Click += new System.EventHandler(this.Tools_SoftReset_Click);
            // 
            // Tools_WonderTrade
            // 
            this.Tools_WonderTrade.Location = new System.Drawing.Point(258, 40);
            this.Tools_WonderTrade.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Tools_WonderTrade.Name = "Tools_WonderTrade";
            this.Tools_WonderTrade.Size = new System.Drawing.Size(119, 28);
            this.Tools_WonderTrade.TabIndex = 5;
            this.Tools_WonderTrade.Text = "WT Bot";
            this.Tools_WonderTrade.UseVisualStyleBackColor = true;
            this.Tools_WonderTrade.Click += new System.EventHandler(this.Tools_WonderTrade_Click);
            // 
            // Tool_Script
            // 
            this.Tool_Script.Location = new System.Drawing.Point(4, 76);
            this.Tool_Script.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Tool_Script.Name = "Tool_Script";
            this.Tool_Script.Size = new System.Drawing.Size(119, 28);
            this.Tool_Script.TabIndex = 7;
            this.Tool_Script.Text = "Script Builder";
            this.Tool_Script.UseVisualStyleBackColor = true;
            this.Tool_Script.Click += new System.EventHandler(this.Tool_Script_Click);
            // 
            // Tools_PokeDigger
            // 
            this.Tools_PokeDigger.Location = new System.Drawing.Point(131, 76);
            this.Tools_PokeDigger.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Tools_PokeDigger.Name = "Tools_PokeDigger";
            this.Tools_PokeDigger.Size = new System.Drawing.Size(119, 28);
            this.Tools_PokeDigger.TabIndex = 7;
            this.Tools_PokeDigger.Text = "PokéDigger";
            this.Tools_PokeDigger.UseVisualStyleBackColor = true;
            this.Tools_PokeDigger.Click += new System.EventHandler(this.Tools_PokeDigger_Click);
            // 
            // Tools_Filter
            // 
            this.Tools_Filter.Location = new System.Drawing.Point(258, 76);
            this.Tools_Filter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Tools_Filter.Name = "Tools_Filter";
            this.Tools_Filter.Size = new System.Drawing.Size(119, 28);
            this.Tools_Filter.TabIndex = 6;
            this.Tools_Filter.Text = "Filters";
            this.Tools_Filter.UseVisualStyleBackColor = true;
            this.Tools_Filter.Click += new System.EventHandler(this.Tools_Filter_Click);
            // 
            // EventHandlerButton
            // 
            this.EventHandlerButton.Location = new System.Drawing.Point(4, 112);
            this.EventHandlerButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.EventHandlerButton.Name = "EventHandlerButton";
            this.EventHandlerButton.Size = new System.Drawing.Size(119, 28);
            this.EventHandlerButton.TabIndex = 8;
            this.EventHandlerButton.Text = "Event Handler";
            this.EventHandlerButton.UseVisualStyleBackColor = true;
            this.EventHandlerButton.Click += new System.EventHandler(this.Tool_EventHandler_Click);
            // 
            // PollingButton
            // 
            this.PollingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PollingButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PollingButton.Location = new System.Drawing.Point(131, 112);
            this.PollingButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PollingButton.Name = "PollingButton";
            this.PollingButton.Size = new System.Drawing.Size(119, 28);
            this.PollingButton.TabIndex = 15;
            this.PollingButton.Text = "Start Polling";
            this.PollingButton.UseVisualStyleBackColor = true;
            this.PollingButton.Click += new System.EventHandler(this.PollingButton_Click);
            // 
            // Tab_Log
            // 
            this.Tab_Log.BackColor = System.Drawing.SystemColors.Control;
            this.Tab_Log.Controls.Add(this.Log_Export);
            this.Tab_Log.Controls.Add(this.txtLog);
            this.Tab_Log.Controls.Add(this.readResult);
            this.Tab_Log.Controls.Add(this.label71);
            this.Tab_Log.Location = new System.Drawing.Point(4, 25);
            this.Tab_Log.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Tab_Log.Name = "Tab_Log";
            this.Tab_Log.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Tab_Log.Size = new System.Drawing.Size(392, 328);
            this.Tab_Log.TabIndex = 2;
            this.Tab_Log.Text = "Log";
            // 
            // Log_Export
            // 
            this.Log_Export.Location = new System.Drawing.Point(259, 290);
            this.Log_Export.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Log_Export.Name = "Log_Export";
            this.Log_Export.Size = new System.Drawing.Size(123, 28);
            this.Log_Export.TabIndex = 2;
            this.Log_Export.Text = "Export Log";
            this.Log_Export.UseVisualStyleBackColor = true;
            this.Log_Export.Click += new System.EventHandler(this.Log_Export_Click);
            // 
            // Tab_About
            // 
            this.Tab_About.BackColor = System.Drawing.SystemColors.Control;
            this.Tab_About.Controls.Add(this.tableLayoutPanel2);
            this.Tab_About.Location = new System.Drawing.Point(4, 25);
            this.Tab_About.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Tab_About.Name = "Tab_About";
            this.Tab_About.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Tab_About.Size = new System.Drawing.Size(392, 328);
            this.Tab_About.TabIndex = 3;
            this.Tab_About.Text = "About";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lb_name, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.label10, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.lb_version, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.lb_tid, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.lb_sid, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.lb_g7id, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.lb_tsv, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.label12, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.label13, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.lb_pkmnntrver, 1, 8);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 9);
            this.tableLayoutPanel2.Controls.Add(this.label11, 0, 10);
            this.tableLayoutPanel2.Controls.Add(this.lb_pkhexcorever, 1, 9);
            this.tableLayoutPanel2.Controls.Add(this.lb_update, 1, 10);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 11;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(384, 287);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 4);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Game Data";
            // 
            // lb_name
            // 
            this.lb_name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_name.AutoSize = true;
            this.lb_name.Location = new System.Drawing.Point(103, 29);
            this.lb_name.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_name.Name = "lb_name";
            this.lb_name.Size = new System.Drawing.Size(277, 17);
            this.lb_name.TabIndex = 0;
            this.lb_name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 29);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Name:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 54);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Version:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 79);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "TID:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 104);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "SID:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 129);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "G7ID:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 154);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 17);
            this.label10.TabIndex = 0;
            this.label10.Text = "TSV:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_version
            // 
            this.lb_version.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_version.AutoSize = true;
            this.lb_version.Location = new System.Drawing.Point(103, 54);
            this.lb_version.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_version.Name = "lb_version";
            this.lb_version.Size = new System.Drawing.Size(277, 17);
            this.lb_version.TabIndex = 0;
            this.lb_version.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_tid
            // 
            this.lb_tid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_tid.AutoSize = true;
            this.lb_tid.Location = new System.Drawing.Point(103, 79);
            this.lb_tid.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_tid.Name = "lb_tid";
            this.lb_tid.Size = new System.Drawing.Size(277, 17);
            this.lb_tid.TabIndex = 0;
            this.lb_tid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_sid
            // 
            this.lb_sid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_sid.AutoSize = true;
            this.lb_sid.Location = new System.Drawing.Point(103, 104);
            this.lb_sid.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_sid.Name = "lb_sid";
            this.lb_sid.Size = new System.Drawing.Size(277, 17);
            this.lb_sid.TabIndex = 0;
            this.lb_sid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_g7id
            // 
            this.lb_g7id.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_g7id.AutoSize = true;
            this.lb_g7id.Location = new System.Drawing.Point(103, 129);
            this.lb_g7id.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_g7id.Name = "lb_g7id";
            this.lb_g7id.Size = new System.Drawing.Size(277, 17);
            this.lb_g7id.TabIndex = 0;
            this.lb_g7id.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_tsv
            // 
            this.lb_tsv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_tsv.AutoSize = true;
            this.lb_tsv.Location = new System.Drawing.Point(103, 154);
            this.lb_tsv.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_tsv.Name = "lb_tsv";
            this.lb_tsv.Size = new System.Drawing.Size(277, 17);
            this.lb_tsv.TabIndex = 0;
            this.lb_tsv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(4, 216);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(91, 17);
            this.label12.TabIndex = 0;
            this.label12.Text = "Version:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label13.Location = new System.Drawing.Point(4, 195);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(91, 17);
            this.label13.TabIndex = 0;
            this.label13.Text = "PKMN-NTR";
            // 
            // lb_pkmnntrver
            // 
            this.lb_pkmnntrver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_pkmnntrver.AutoSize = true;
            this.lb_pkmnntrver.Location = new System.Drawing.Point(103, 216);
            this.lb_pkmnntrver.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_pkmnntrver.Name = "lb_pkmnntrver";
            this.lb_pkmnntrver.Size = new System.Drawing.Size(277, 17);
            this.lb_pkmnntrver.TabIndex = 4;
            this.lb_pkmnntrver.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lb_pkmnntrver.Click += new System.EventHandler(this.ClickUpdateLabel);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 241);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 17);
            this.label8.TabIndex = 0;
            this.label8.Text = "PKHeX.Core:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(4, 266);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(91, 17);
            this.label11.TabIndex = 0;
            this.label11.Text = "Updates:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_pkhexcorever
            // 
            this.lb_pkhexcorever.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_pkhexcorever.AutoSize = true;
            this.lb_pkhexcorever.Location = new System.Drawing.Point(103, 241);
            this.lb_pkhexcorever.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_pkhexcorever.Name = "lb_pkhexcorever";
            this.lb_pkhexcorever.Size = new System.Drawing.Size(277, 17);
            this.lb_pkhexcorever.TabIndex = 4;
            this.lb_pkhexcorever.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lb_pkhexcorever.Click += new System.EventHandler(this.ClickUpdateLabel);
            // 
            // dragout
            // 
            this.dragout.BackColor = System.Drawing.Color.Transparent;
            this.dragout.Location = new System.Drawing.Point(735, 23);
            this.dragout.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dragout.Name = "dragout";
            this.dragout.Size = new System.Drawing.Size(53, 37);
            this.dragout.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.dragout.TabIndex = 61;
            this.dragout.TabStop = false;
            this.toolTip1.SetToolTip(this.dragout, "PKM QuickSave");
            this.dragout.DragDrop += new System.Windows.Forms.DragEventHandler(this.DragoutDrop);
            this.dragout.DragOver += new System.Windows.Forms.DragEventHandler(this.Dragout_DragOver);
            this.dragout.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Dragout_MouseDown);
            this.dragout.MouseEnter += new System.EventHandler(this.DragoutEnter);
            this.dragout.MouseLeave += new System.EventHandler(this.DragoutLeave);
            // 
            // EventPollingWorker
            // 
            this.EventPollingWorker.WorkerReportsProgress = true;
            this.EventPollingWorker.WorkerSupportsCancellation = true;
            this.EventPollingWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.EventPollingWorker_DoWork);
            // 
            // PB_Legal
            // 
            this.PB_Legal.Image = ((System.Drawing.Image)(resources.GetObject("PB_Legal.Image")));
            this.PB_Legal.Location = new System.Drawing.Point(709, 20);
            this.PB_Legal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PB_Legal.Name = "PB_Legal";
            this.PB_Legal.Size = new System.Drawing.Size(21, 20);
            this.PB_Legal.TabIndex = 102;
            this.PB_Legal.TabStop = false;
            this.PB_Legal.Click += new System.EventHandler(this.ClickLegality);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(817, 453);
            this.Controls.Add(this.PB_Legal);
            this.Controls.Add(this.dragout);
            this.Controls.Add(this.Tabs_General);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 8, 7);
            this.Text = "PKMN-NTR";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slotDump)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.boxDump)).EndInit();
            this.Tabs_General.ResumeLayout(false);
            this.Tab_Dump.ResumeLayout(false);
            this.Tab_Dump.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.Tab_Clone.ResumeLayout(false);
            this.Tab_Clone.PerformLayout();
            this.GB_CDmode.ResumeLayout(false);
            this.GB_CDmode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_CDBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_CDAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_CDSlot)).EndInit();
            this.Tab_Tools.ResumeLayout(false);
            this.Tab_Tools.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.Tab_Log.ResumeLayout(false);
            this.Tab_Log.PerformLayout();
            this.Tab_About.ResumeLayout(false);
            this.Tab_About.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dragout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Legal)).EndInit();
            this.ResumeLayout(false);

        }

        public System.Windows.Forms.TextBox txtLog;
		private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer disconnectTimer;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.TextBox host;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.TextBox readResult;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.Label lb_update;
        private System.Windows.Forms.Button DumpInstructionsBtn;
        private System.Windows.Forms.Label BoxLabel;
        private System.Windows.Forms.Button dumpPokemon;
        private System.Windows.Forms.Label SlotLabel;
        private System.Windows.Forms.CheckBox backupPKM;
        private System.Windows.Forms.NumericUpDown boxDump;
        private System.Windows.Forms.NumericUpDown slotDump;
        private System.Windows.Forms.Button dumpBoxes;
        private System.Windows.Forms.RadioButton radioBoxes;
        private System.Windows.Forms.RadioButton radioOpponent;
        private System.Windows.Forms.RadioButton radioTrade;
        private System.Windows.Forms.RadioButton radioParty;
        private System.Windows.Forms.RadioButton radioBattleBox;
        private System.Windows.Forms.Button Tool_Trainer;
        private System.Windows.Forms.TabControl Tabs_General;
        private System.Windows.Forms.TabPage Tab_Tools;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button Tool_Items;
        private System.Windows.Forms.Button Tool_Controls;
        private System.Windows.Forms.Button Tools_Breeding;
        private System.Windows.Forms.Button Tools_SoftReset;
        private System.Windows.Forms.Button Tools_WonderTrade;
        private System.Windows.Forms.Button Tools_Filter;
        private System.Windows.Forms.TabPage Tab_Clone;
        private System.Windows.Forms.TabPage Tab_Log;
        private System.Windows.Forms.Button Log_Export;
        private System.Windows.Forms.TabPage Tab_About;
        private System.Windows.Forms.TabPage Tab_Dump;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button Tools_PokeDigger;
        private System.Windows.Forms.Button Write_PKM;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lb_name;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lb_version;
        private System.Windows.Forms.Label lb_tid;
        private System.Windows.Forms.Label lb_sid;
        private System.Windows.Forms.Label lb_g7id;
        private System.Windows.Forms.Label lb_tsv;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lb_pkmnntrver;
        private System.Windows.Forms.Button Btn_CDstart;
        private System.Windows.Forms.CheckBox CB_CDBackup;
        private System.Windows.Forms.GroupBox GB_CDmode;
        private System.Windows.Forms.RadioButton DeleteMode;
        private System.Windows.Forms.RadioButton CloneMode;
        private System.Windows.Forms.NumericUpDown Num_CDBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown Num_CDAmount;
        private System.Windows.Forms.NumericUpDown Num_CDSlot;
        private System.Windows.Forms.RadioButton radioDaycare;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lb_pkhexcorever;
        private System.Windows.Forms.TextBox Seed_Legendary;
        private System.Windows.Forms.TextBox Seed_Egg;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button Btn_ReloadFields;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button Tool_Script;
        private System.Windows.Forms.TextBox resetNoBox;
        private System.Windows.Forms.Label labelreset;
        private System.Windows.Forms.Button EventHandlerButton;
        private System.ComponentModel.BackgroundWorker EventPollingWorker;
        private System.Windows.Forms.PictureBox dragout;
        private System.Windows.Forms.PictureBox PB_Legal;
        private System.Windows.Forms.Button PollingButton;
    }
}

