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
            this.tabMain = new System.Windows.Forms.TabControl();
            this.Tab_Main = new System.Windows.Forms.TabPage();
            this.FLP_Main = new System.Windows.Forms.FlowLayoutPanel();
            this.FLP_PID = new System.Windows.Forms.FlowLayoutPanel();
            this.FLP_PIDLeft = new System.Windows.Forms.FlowLayoutPanel();
            this.Label_PID = new System.Windows.Forms.Label();
            this.BTN_Shinytize = new System.Windows.Forms.Button();
            this.Label_IsShiny = new System.Windows.Forms.PictureBox();
            this.FLP_PIDRight = new System.Windows.Forms.FlowLayoutPanel();
            this.TB_PID = new System.Windows.Forms.TextBox();
            this.Label_Gender = new System.Windows.Forms.Label();
            this.BTN_RerollPID = new System.Windows.Forms.Button();
            this.FLP_Species = new System.Windows.Forms.FlowLayoutPanel();
            this.Label_Species = new System.Windows.Forms.Label();
            this.CB_Species = new System.Windows.Forms.ComboBox();
            this.FLP_Nickname = new System.Windows.Forms.FlowLayoutPanel();
            this.FLP_NicknameLeft = new System.Windows.Forms.FlowLayoutPanel();
            this.CHK_Nicknamed = new System.Windows.Forms.CheckBox();
            this.TB_Nickname = new System.Windows.Forms.TextBox();
            this.FLP_EXPLevel = new System.Windows.Forms.FlowLayoutPanel();
            this.Label_EXP = new System.Windows.Forms.Label();
            this.FLP_EXPLevelRight = new System.Windows.Forms.FlowLayoutPanel();
            this.TB_EXP = new System.Windows.Forms.MaskedTextBox();
            this.Label_CurLevel = new System.Windows.Forms.Label();
            this.TB_Level = new System.Windows.Forms.MaskedTextBox();
            this.MT_Level = new System.Windows.Forms.MaskedTextBox();
            this.FLP_Nature = new System.Windows.Forms.FlowLayoutPanel();
            this.Label_Nature = new System.Windows.Forms.Label();
            this.CB_Nature = new System.Windows.Forms.ComboBox();
            this.FLP_HeldItem = new System.Windows.Forms.FlowLayoutPanel();
            this.Label_HeldItem = new System.Windows.Forms.Label();
            this.CB_HeldItem = new System.Windows.Forms.ComboBox();
            this.FLP_FriendshipForm = new System.Windows.Forms.FlowLayoutPanel();
            this.FLP_FriendshipFormLeft = new System.Windows.Forms.FlowLayoutPanel();
            this.Label_Friendship = new System.Windows.Forms.Label();
            this.Label_HatchCounter = new System.Windows.Forms.Label();
            this.FLP_FriendshipFormRight = new System.Windows.Forms.FlowLayoutPanel();
            this.TB_Friendship = new System.Windows.Forms.MaskedTextBox();
            this.Label_Form = new System.Windows.Forms.Label();
            this.CB_Form = new System.Windows.Forms.ComboBox();
            this.FLP_Ability = new System.Windows.Forms.FlowLayoutPanel();
            this.Label_Ability = new System.Windows.Forms.Label();
            this.FLP_AbilityRight = new System.Windows.Forms.FlowLayoutPanel();
            this.CB_Ability = new System.Windows.Forms.ComboBox();
            this.DEV_Ability = new System.Windows.Forms.ComboBox();
            this.TB_AbilityNumber = new System.Windows.Forms.MaskedTextBox();
            this.FLP_Language = new System.Windows.Forms.FlowLayoutPanel();
            this.Label_Language = new System.Windows.Forms.Label();
            this.CB_Language = new System.Windows.Forms.ComboBox();
            this.FLP_EggPKRS = new System.Windows.Forms.FlowLayoutPanel();
            this.FLP_EggPKRSLeft = new System.Windows.Forms.FlowLayoutPanel();
            this.CHK_IsEgg = new System.Windows.Forms.CheckBox();
            this.FLP_EggPKRSRight = new System.Windows.Forms.FlowLayoutPanel();
            this.CHK_Infected = new System.Windows.Forms.CheckBox();
            this.CHK_Cured = new System.Windows.Forms.CheckBox();
            this.FLP_PKRS = new System.Windows.Forms.FlowLayoutPanel();
            this.Label_PKRS = new System.Windows.Forms.Label();
            this.FLP_PKRSRight = new System.Windows.Forms.FlowLayoutPanel();
            this.CB_PKRSStrain = new System.Windows.Forms.ComboBox();
            this.Label_PKRSdays = new System.Windows.Forms.Label();
            this.CB_PKRSDays = new System.Windows.Forms.ComboBox();
            this.FLP_Country = new System.Windows.Forms.FlowLayoutPanel();
            this.Label_Country = new System.Windows.Forms.Label();
            this.CB_Country = new System.Windows.Forms.ComboBox();
            this.FLP_SubRegion = new System.Windows.Forms.FlowLayoutPanel();
            this.Label_SubRegion = new System.Windows.Forms.Label();
            this.CB_SubRegion = new System.Windows.Forms.ComboBox();
            this.FLP_3DSRegion = new System.Windows.Forms.FlowLayoutPanel();
            this.Label_3DSRegion = new System.Windows.Forms.Label();
            this.CB_3DSReg = new System.Windows.Forms.ComboBox();
            this.Tab_Met = new System.Windows.Forms.TabPage();
            this.CHK_AsEgg = new System.Windows.Forms.CheckBox();
            this.GB_EggConditions = new System.Windows.Forms.GroupBox();
            this.CB_EggLocation = new System.Windows.Forms.ComboBox();
            this.CAL_EggDate = new System.Windows.Forms.DateTimePicker();
            this.Label_EggDate = new System.Windows.Forms.Label();
            this.Label_EggLocation = new System.Windows.Forms.Label();
            this.FLP_Met = new System.Windows.Forms.FlowLayoutPanel();
            this.FLP_OriginGame = new System.Windows.Forms.FlowLayoutPanel();
            this.Label_OriginGame = new System.Windows.Forms.Label();
            this.CB_GameOrigin = new System.Windows.Forms.ComboBox();
            this.FLP_MetLocation = new System.Windows.Forms.FlowLayoutPanel();
            this.Label_MetLocation = new System.Windows.Forms.Label();
            this.CB_MetLocation = new System.Windows.Forms.ComboBox();
            this.FLP_Ball = new System.Windows.Forms.FlowLayoutPanel();
            this.FLP_BallLeft = new System.Windows.Forms.FlowLayoutPanel();
            this.Label_Ball = new System.Windows.Forms.Label();
            this.PB_Ball = new System.Windows.Forms.PictureBox();
            this.CB_Ball = new System.Windows.Forms.ComboBox();
            this.FLP_MetLevel = new System.Windows.Forms.FlowLayoutPanel();
            this.Label_MetLevel = new System.Windows.Forms.Label();
            this.TB_MetLevel = new System.Windows.Forms.MaskedTextBox();
            this.FLP_MetDate = new System.Windows.Forms.FlowLayoutPanel();
            this.Label_MetDate = new System.Windows.Forms.Label();
            this.CAL_MetDate = new System.Windows.Forms.DateTimePicker();
            this.FLP_Fateful = new System.Windows.Forms.FlowLayoutPanel();
            this.PAN_Fateful = new System.Windows.Forms.Panel();
            this.CHK_Fateful = new System.Windows.Forms.CheckBox();
            this.FLP_EncounterType = new System.Windows.Forms.FlowLayoutPanel();
            this.Label_EncounterType = new System.Windows.Forms.Label();
            this.CB_EncounterType = new System.Windows.Forms.ComboBox();
            this.FLP_TimeOfDay = new System.Windows.Forms.FlowLayoutPanel();
            this.L_MetTimeOfDay = new System.Windows.Forms.Label();
            this.CB_MetTimeOfDay = new System.Windows.Forms.ComboBox();
            this.Tab_Stats = new System.Windows.Forms.TabPage();
            this.PAN_Contest = new System.Windows.Forms.Panel();
            this.TB_Sheen = new System.Windows.Forms.MaskedTextBox();
            this.TB_Tough = new System.Windows.Forms.MaskedTextBox();
            this.TB_Smart = new System.Windows.Forms.MaskedTextBox();
            this.TB_Cute = new System.Windows.Forms.MaskedTextBox();
            this.TB_Beauty = new System.Windows.Forms.MaskedTextBox();
            this.TB_Cool = new System.Windows.Forms.MaskedTextBox();
            this.Label_Sheen = new System.Windows.Forms.Label();
            this.Label_Tough = new System.Windows.Forms.Label();
            this.Label_Smart = new System.Windows.Forms.Label();
            this.Label_Cute = new System.Windows.Forms.Label();
            this.Label_Beauty = new System.Windows.Forms.Label();
            this.Label_Cool = new System.Windows.Forms.Label();
            this.Label_ContestStats = new System.Windows.Forms.Label();
            this.FLP_Stats = new System.Windows.Forms.FlowLayoutPanel();
            this.FLP_StatHeader = new System.Windows.Forms.FlowLayoutPanel();
            this.FLP_HackedStats = new System.Windows.Forms.FlowLayoutPanel();
            this.FLP_StatsHeaderRight = new System.Windows.Forms.FlowLayoutPanel();
            this.Label_IVs = new System.Windows.Forms.Label();
            this.Label_EVs = new System.Windows.Forms.Label();
            this.Label_Stats = new System.Windows.Forms.Label();
            this.FLP_HP = new System.Windows.Forms.FlowLayoutPanel();
            this.Label_HP = new System.Windows.Forms.Label();
            this.FLP_HPRight = new System.Windows.Forms.FlowLayoutPanel();
            this.TB_HPIV = new System.Windows.Forms.MaskedTextBox();
            this.TB_HPEV = new System.Windows.Forms.MaskedTextBox();
            this.Stat_HP = new System.Windows.Forms.MaskedTextBox();
            this.FLP_Atk = new System.Windows.Forms.FlowLayoutPanel();
            this.Label_ATK = new System.Windows.Forms.Label();
            this.FLP_AtkRight = new System.Windows.Forms.FlowLayoutPanel();
            this.TB_ATKIV = new System.Windows.Forms.MaskedTextBox();
            this.TB_ATKEV = new System.Windows.Forms.MaskedTextBox();
            this.Stat_ATK = new System.Windows.Forms.MaskedTextBox();
            this.FLP_Def = new System.Windows.Forms.FlowLayoutPanel();
            this.Label_DEF = new System.Windows.Forms.Label();
            this.FLP_DefRight = new System.Windows.Forms.FlowLayoutPanel();
            this.TB_DEFIV = new System.Windows.Forms.MaskedTextBox();
            this.TB_DEFEV = new System.Windows.Forms.MaskedTextBox();
            this.Stat_DEF = new System.Windows.Forms.MaskedTextBox();
            this.FLP_SpA = new System.Windows.Forms.FlowLayoutPanel();
            this.FLP_SpALeft = new System.Windows.Forms.FlowLayoutPanel();
            this.Label_SPA = new System.Windows.Forms.Label();
            this.Label_SPC = new System.Windows.Forms.Label();
            this.FLP_SpARight = new System.Windows.Forms.FlowLayoutPanel();
            this.TB_SPAIV = new System.Windows.Forms.MaskedTextBox();
            this.TB_SPAEV = new System.Windows.Forms.MaskedTextBox();
            this.Stat_SPA = new System.Windows.Forms.MaskedTextBox();
            this.FLP_SpD = new System.Windows.Forms.FlowLayoutPanel();
            this.Label_SPD = new System.Windows.Forms.Label();
            this.FLP_SpDRight = new System.Windows.Forms.FlowLayoutPanel();
            this.TB_SPDIV = new System.Windows.Forms.MaskedTextBox();
            this.TB_SPDEV = new System.Windows.Forms.MaskedTextBox();
            this.Stat_SPD = new System.Windows.Forms.MaskedTextBox();
            this.FLP_Spe = new System.Windows.Forms.FlowLayoutPanel();
            this.Label_SPE = new System.Windows.Forms.Label();
            this.FLP_SpeRight = new System.Windows.Forms.FlowLayoutPanel();
            this.TB_SPEIV = new System.Windows.Forms.MaskedTextBox();
            this.TB_SPEEV = new System.Windows.Forms.MaskedTextBox();
            this.Stat_SPE = new System.Windows.Forms.MaskedTextBox();
            this.FLP_StatsTotal = new System.Windows.Forms.FlowLayoutPanel();
            this.Label_Total = new System.Windows.Forms.Label();
            this.FLP_StatsTotalRight = new System.Windows.Forms.FlowLayoutPanel();
            this.TB_IVTotal = new System.Windows.Forms.TextBox();
            this.TB_EVTotal = new System.Windows.Forms.TextBox();
            this.L_Potential = new System.Windows.Forms.Label();
            this.FLP_HPType = new System.Windows.Forms.FlowLayoutPanel();
            this.Label_HiddenPowerPrefix = new System.Windows.Forms.Label();
            this.CB_HPType = new System.Windows.Forms.ComboBox();
            this.FLP_Characteristic = new System.Windows.Forms.FlowLayoutPanel();
            this.Label_CharacteristicPrefix = new System.Windows.Forms.Label();
            this.L_Characteristic = new System.Windows.Forms.Label();
            this.BTN_RandomEVs = new System.Windows.Forms.Button();
            this.BTN_RandomIVs = new System.Windows.Forms.Button();
            this.Tab_Attacks = new System.Windows.Forms.TabPage();
            this.PB_WarnMove4 = new System.Windows.Forms.PictureBox();
            this.PB_WarnMove3 = new System.Windows.Forms.PictureBox();
            this.PB_WarnMove2 = new System.Windows.Forms.PictureBox();
            this.PB_WarnMove1 = new System.Windows.Forms.PictureBox();
            this.GB_RelearnMoves = new System.Windows.Forms.GroupBox();
            this.PB_WarnRelearn4 = new System.Windows.Forms.PictureBox();
            this.PB_WarnRelearn3 = new System.Windows.Forms.PictureBox();
            this.PB_WarnRelearn2 = new System.Windows.Forms.PictureBox();
            this.PB_WarnRelearn1 = new System.Windows.Forms.PictureBox();
            this.CB_RelearnMove4 = new System.Windows.Forms.ComboBox();
            this.CB_RelearnMove3 = new System.Windows.Forms.ComboBox();
            this.CB_RelearnMove2 = new System.Windows.Forms.ComboBox();
            this.CB_RelearnMove1 = new System.Windows.Forms.ComboBox();
            this.GB_CurrentMoves = new System.Windows.Forms.GroupBox();
            this.TB_PP4 = new System.Windows.Forms.MaskedTextBox();
            this.TB_PP3 = new System.Windows.Forms.MaskedTextBox();
            this.TB_PP2 = new System.Windows.Forms.MaskedTextBox();
            this.TB_PP1 = new System.Windows.Forms.MaskedTextBox();
            this.Label_CurPP = new System.Windows.Forms.Label();
            this.Label_PPups = new System.Windows.Forms.Label();
            this.CB_PPu4 = new System.Windows.Forms.ComboBox();
            this.CB_PPu3 = new System.Windows.Forms.ComboBox();
            this.CB_PPu2 = new System.Windows.Forms.ComboBox();
            this.CB_Move4 = new System.Windows.Forms.ComboBox();
            this.CB_PPu1 = new System.Windows.Forms.ComboBox();
            this.CB_Move3 = new System.Windows.Forms.ComboBox();
            this.CB_Move2 = new System.Windows.Forms.ComboBox();
            this.CB_Move1 = new System.Windows.Forms.ComboBox();
            this.Tab_OTMisc = new System.Windows.Forms.TabPage();
            this.GB_Markings = new System.Windows.Forms.GroupBox();
            this.PB_MarkHorohoro = new System.Windows.Forms.PictureBox();
            this.PB_MarkVC = new System.Windows.Forms.PictureBox();
            this.PB_MarkAlola = new System.Windows.Forms.PictureBox();
            this.PB_Mark6 = new System.Windows.Forms.PictureBox();
            this.PB_MarkPentagon = new System.Windows.Forms.PictureBox();
            this.PB_Mark3 = new System.Windows.Forms.PictureBox();
            this.PB_Mark5 = new System.Windows.Forms.PictureBox();
            this.PB_MarkCured = new System.Windows.Forms.PictureBox();
            this.PB_Mark2 = new System.Windows.Forms.PictureBox();
            this.PB_MarkShiny = new System.Windows.Forms.PictureBox();
            this.PB_Mark1 = new System.Windows.Forms.PictureBox();
            this.PB_Mark4 = new System.Windows.Forms.PictureBox();
            this.FLP_PKMEditors = new System.Windows.Forms.FlowLayoutPanel();
            this.BTN_Ribbons = new System.Windows.Forms.Button();
            this.BTN_Medals = new System.Windows.Forms.Button();
            this.BTN_History = new System.Windows.Forms.Button();
            this.TB_EC = new System.Windows.Forms.TextBox();
            this.GB_nOT = new System.Windows.Forms.GroupBox();
            this.Label_CTGender = new System.Windows.Forms.Label();
            this.TB_OTt2 = new System.Windows.Forms.TextBox();
            this.Label_PrevOT = new System.Windows.Forms.Label();
            this.BTN_RerollEC = new System.Windows.Forms.Button();
            this.GB_ExtraBytes = new System.Windows.Forms.GroupBox();
            this.TB_ExtraByte = new System.Windows.Forms.MaskedTextBox();
            this.CB_ExtraBytes = new System.Windows.Forms.ComboBox();
            this.GB_OT = new System.Windows.Forms.GroupBox();
            this.Label_OTGender = new System.Windows.Forms.Label();
            this.TB_OT = new System.Windows.Forms.TextBox();
            this.TB_SID = new System.Windows.Forms.MaskedTextBox();
            this.TB_TID = new System.Windows.Forms.MaskedTextBox();
            this.Label_OT = new System.Windows.Forms.Label();
            this.Label_SID = new System.Windows.Forms.Label();
            this.Label_TID = new System.Windows.Forms.Label();
            this.Label_EncryptionConstant = new System.Windows.Forms.Label();
            this.Tool_Trainer = new System.Windows.Forms.Button();
            this.Tabs_General = new System.Windows.Forms.TabControl();
            this.Tab_Dump = new System.Windows.Forms.TabPage();
            this.StartPollingButton = new System.Windows.Forms.Button();
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
            this.PB_Legal = new System.Windows.Forms.PictureBox();
            this.dragout = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.StopPollingButton = new System.Windows.Forms.Button();
            this.EventPollingWorker = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slotDump)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.boxDump)).BeginInit();
            this.tabMain.SuspendLayout();
            this.Tab_Main.SuspendLayout();
            this.FLP_Main.SuspendLayout();
            this.FLP_PID.SuspendLayout();
            this.FLP_PIDLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Label_IsShiny)).BeginInit();
            this.FLP_PIDRight.SuspendLayout();
            this.FLP_Species.SuspendLayout();
            this.FLP_Nickname.SuspendLayout();
            this.FLP_NicknameLeft.SuspendLayout();
            this.FLP_EXPLevel.SuspendLayout();
            this.FLP_EXPLevelRight.SuspendLayout();
            this.FLP_Nature.SuspendLayout();
            this.FLP_HeldItem.SuspendLayout();
            this.FLP_FriendshipForm.SuspendLayout();
            this.FLP_FriendshipFormLeft.SuspendLayout();
            this.FLP_FriendshipFormRight.SuspendLayout();
            this.FLP_Ability.SuspendLayout();
            this.FLP_AbilityRight.SuspendLayout();
            this.FLP_Language.SuspendLayout();
            this.FLP_EggPKRS.SuspendLayout();
            this.FLP_EggPKRSLeft.SuspendLayout();
            this.FLP_EggPKRSRight.SuspendLayout();
            this.FLP_PKRS.SuspendLayout();
            this.FLP_PKRSRight.SuspendLayout();
            this.FLP_Country.SuspendLayout();
            this.FLP_SubRegion.SuspendLayout();
            this.FLP_3DSRegion.SuspendLayout();
            this.Tab_Met.SuspendLayout();
            this.GB_EggConditions.SuspendLayout();
            this.FLP_Met.SuspendLayout();
            this.FLP_OriginGame.SuspendLayout();
            this.FLP_MetLocation.SuspendLayout();
            this.FLP_Ball.SuspendLayout();
            this.FLP_BallLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Ball)).BeginInit();
            this.FLP_MetLevel.SuspendLayout();
            this.FLP_MetDate.SuspendLayout();
            this.FLP_Fateful.SuspendLayout();
            this.FLP_EncounterType.SuspendLayout();
            this.FLP_TimeOfDay.SuspendLayout();
            this.Tab_Stats.SuspendLayout();
            this.PAN_Contest.SuspendLayout();
            this.FLP_Stats.SuspendLayout();
            this.FLP_StatHeader.SuspendLayout();
            this.FLP_StatsHeaderRight.SuspendLayout();
            this.FLP_HP.SuspendLayout();
            this.FLP_HPRight.SuspendLayout();
            this.FLP_Atk.SuspendLayout();
            this.FLP_AtkRight.SuspendLayout();
            this.FLP_Def.SuspendLayout();
            this.FLP_DefRight.SuspendLayout();
            this.FLP_SpA.SuspendLayout();
            this.FLP_SpALeft.SuspendLayout();
            this.FLP_SpARight.SuspendLayout();
            this.FLP_SpD.SuspendLayout();
            this.FLP_SpDRight.SuspendLayout();
            this.FLP_Spe.SuspendLayout();
            this.FLP_SpeRight.SuspendLayout();
            this.FLP_StatsTotal.SuspendLayout();
            this.FLP_StatsTotalRight.SuspendLayout();
            this.FLP_HPType.SuspendLayout();
            this.FLP_Characteristic.SuspendLayout();
            this.Tab_Attacks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PB_WarnMove4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_WarnMove3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_WarnMove2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_WarnMove1)).BeginInit();
            this.GB_RelearnMoves.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PB_WarnRelearn4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_WarnRelearn3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_WarnRelearn2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_WarnRelearn1)).BeginInit();
            this.GB_CurrentMoves.SuspendLayout();
            this.Tab_OTMisc.SuspendLayout();
            this.GB_Markings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PB_MarkHorohoro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_MarkVC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_MarkAlola)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Mark6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_MarkPentagon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Mark3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Mark5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_MarkCured)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Mark2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_MarkShiny)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Mark1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Mark4)).BeginInit();
            this.FLP_PKMEditors.SuspendLayout();
            this.GB_nOT.SuspendLayout();
            this.GB_ExtraBytes.SuspendLayout();
            this.GB_OT.SuspendLayout();
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
            ((System.ComponentModel.ISupportInitialize)(this.PB_Legal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dragout)).BeginInit();
            this.SuspendLayout();
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.Color.Black;
            this.txtLog.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtLog.ForeColor = System.Drawing.Color.LawnGreen;
            this.txtLog.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtLog.Location = new System.Drawing.Point(3, 0);
            this.txtLog.MaxLength = 32767000;
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(286, 232);
            this.txtLog.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // disconnectTimer
            // 
            this.disconnectTimer.Interval = 10000;
            this.disconnectTimer.Tick += new System.EventHandler(this.disconnectTimer_Tick);
            // 
            // buttonConnect
            // 
            this.buttonConnect.AutoSize = true;
            this.buttonConnect.Location = new System.Drawing.Point(118, 3);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(83, 23);
            this.buttonConnect.TabIndex = 1;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.AutoSize = true;
            this.buttonDisconnect.Enabled = false;
            this.buttonDisconnect.Location = new System.Drawing.Point(207, 3);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(83, 23);
            this.buttonDisconnect.TabIndex = 2;
            this.buttonDisconnect.Text = "Disconnect";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // host
            // 
            this.host.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.host.Location = new System.Drawing.Point(29, 4);
            this.host.Name = "host";
            this.host.Size = new System.Drawing.Size(83, 20);
            this.host.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 308);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 50);
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
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(294, 29);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "IP:";
            // 
            // DumpInstructionsBtn
            // 
            this.DumpInstructionsBtn.Location = new System.Drawing.Point(211, 235);
            this.DumpInstructionsBtn.Name = "DumpInstructionsBtn";
            this.DumpInstructionsBtn.Size = new System.Drawing.Size(75, 23);
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
            this.lb_update.Location = new System.Drawing.Point(79, 213);
            this.lb_update.Name = "lb_update";
            this.lb_update.Size = new System.Drawing.Size(204, 13);
            this.lb_update.TabIndex = 4;
            this.lb_update.Text = "Looking for updates...";
            this.lb_update.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lb_update.Click += new System.EventHandler(this.updateLabel_Click);
            // 
            // backupPKM
            // 
            this.backupPKM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.backupPKM.AutoSize = true;
            this.backupPKM.Location = new System.Drawing.Point(219, 27);
            this.backupPKM.Name = "backupPKM";
            this.backupPKM.Size = new System.Drawing.Size(63, 17);
            this.backupPKM.TabIndex = 3;
            this.backupPKM.Text = "Backup";
            this.toolTip1.SetToolTip(this.backupPKM, "When activated, it saves a copy of the dumped data in the \"Pokemon\" folder.");
            this.backupPKM.UseVisualStyleBackColor = true;
            // 
            // readResult
            // 
            this.readResult.Location = new System.Drawing.Point(98, 238);
            this.readResult.Name = "readResult";
            this.readResult.ReadOnly = true;
            this.readResult.Size = new System.Drawing.Size(90, 20);
            this.readResult.TabIndex = 1;
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(6, 241);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(86, 13);
            this.label71.TabIndex = 2;
            this.label71.Text = "Last RAM Read:";
            // 
            // radioBoxes
            // 
            this.radioBoxes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.radioBoxes.AutoSize = true;
            this.radioBoxes.Checked = true;
            this.radioBoxes.Location = new System.Drawing.Point(3, 5);
            this.radioBoxes.Name = "radioBoxes";
            this.radioBoxes.Size = new System.Drawing.Size(87, 17);
            this.radioBoxes.TabIndex = 0;
            this.radioBoxes.TabStop = true;
            this.radioBoxes.Text = "Boxes";
            this.radioBoxes.UseVisualStyleBackColor = true;
            this.radioBoxes.CheckedChanged += new System.EventHandler(this.radioBoxes_CheckedChanged);
            // 
            // radioDaycare
            // 
            this.radioDaycare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.radioDaycare.AutoSize = true;
            this.radioDaycare.Location = new System.Drawing.Point(96, 5);
            this.radioDaycare.Name = "radioDaycare";
            this.radioDaycare.Size = new System.Drawing.Size(87, 17);
            this.radioDaycare.TabIndex = 1;
            this.radioDaycare.Text = "Daycare";
            this.radioDaycare.UseVisualStyleBackColor = true;
            this.radioDaycare.CheckedChanged += new System.EventHandler(this.radioDaycare_CheckedChanged);
            // 
            // dumpBoxes
            // 
            this.dumpBoxes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dumpBoxes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dumpBoxes.Location = new System.Drawing.Point(195, 140);
            this.dumpBoxes.Name = "dumpBoxes";
            this.dumpBoxes.Size = new System.Drawing.Size(91, 23);
            this.dumpBoxes.TabIndex = 6;
            this.dumpBoxes.Text = "Dump All Boxes";
            this.dumpBoxes.UseVisualStyleBackColor = true;
            this.dumpBoxes.Click += new System.EventHandler(this.dumpBoxes_Click);
            // 
            // slotDump
            // 
            this.slotDump.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.slotDump.AutoSize = true;
            this.slotDump.Location = new System.Drawing.Point(52, 24);
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
            this.slotDump.Size = new System.Drawing.Size(40, 20);
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
            this.boxDump.Location = new System.Drawing.Point(6, 24);
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
            this.boxDump.Size = new System.Drawing.Size(40, 20);
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
            this.radioOpponent.Location = new System.Drawing.Point(96, 32);
            this.radioOpponent.Name = "radioOpponent";
            this.radioOpponent.Size = new System.Drawing.Size(87, 17);
            this.radioOpponent.TabIndex = 4;
            this.radioOpponent.TabStop = true;
            this.radioOpponent.Text = "Opponent";
            this.radioOpponent.UseVisualStyleBackColor = true;
            this.radioOpponent.CheckedChanged += new System.EventHandler(this.radioOpponent_CheckedChanged);
            // 
            // radioTrade
            // 
            this.radioTrade.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.radioTrade.AutoSize = true;
            this.radioTrade.Location = new System.Drawing.Point(3, 32);
            this.radioTrade.Name = "radioTrade";
            this.radioTrade.Size = new System.Drawing.Size(87, 17);
            this.radioTrade.TabIndex = 3;
            this.radioTrade.TabStop = true;
            this.radioTrade.Text = "Trade";
            this.radioTrade.UseVisualStyleBackColor = true;
            this.radioTrade.CheckedChanged += new System.EventHandler(this.radioTrade_CheckedChanged);
            // 
            // SlotLabel
            // 
            this.SlotLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.SlotLabel.AutoSize = true;
            this.SlotLabel.Location = new System.Drawing.Point(49, 8);
            this.SlotLabel.Name = "SlotLabel";
            this.SlotLabel.Size = new System.Drawing.Size(28, 13);
            this.SlotLabel.TabIndex = 13;
            this.SlotLabel.Text = "Slot:";
            // 
            // radioParty
            // 
            this.radioParty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.radioParty.AutoSize = true;
            this.radioParty.Location = new System.Drawing.Point(189, 32);
            this.radioParty.Name = "radioParty";
            this.radioParty.Size = new System.Drawing.Size(88, 17);
            this.radioParty.TabIndex = 5;
            this.radioParty.Text = "Party";
            this.radioParty.UseVisualStyleBackColor = true;
            this.radioParty.CheckedChanged += new System.EventHandler(this.radioParty_CheckedChanged_1);
            // 
            // dumpPokemon
            // 
            this.dumpPokemon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dumpPokemon.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dumpPokemon.Location = new System.Drawing.Point(98, 22);
            this.dumpPokemon.Name = "dumpPokemon";
            this.dumpPokemon.Size = new System.Drawing.Size(115, 23);
            this.dumpPokemon.TabIndex = 2;
            this.dumpPokemon.Text = "Read Pokémon";
            this.dumpPokemon.UseVisualStyleBackColor = true;
            this.dumpPokemon.Click += new System.EventHandler(this.dumpPokemon_Click);
            // 
            // radioBattleBox
            // 
            this.radioBattleBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.radioBattleBox.AutoSize = true;
            this.radioBattleBox.Location = new System.Drawing.Point(189, 5);
            this.radioBattleBox.Name = "radioBattleBox";
            this.radioBattleBox.Size = new System.Drawing.Size(88, 17);
            this.radioBattleBox.TabIndex = 2;
            this.radioBattleBox.Text = "Battle Box";
            this.radioBattleBox.UseVisualStyleBackColor = true;
            this.radioBattleBox.CheckedChanged += new System.EventHandler(this.radioBattleBox_CheckedChanged);
            // 
            // BoxLabel
            // 
            this.BoxLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.BoxLabel.AutoSize = true;
            this.BoxLabel.Location = new System.Drawing.Point(6, 8);
            this.BoxLabel.Name = "BoxLabel";
            this.BoxLabel.Size = new System.Drawing.Size(28, 13);
            this.BoxLabel.TabIndex = 12;
            this.BoxLabel.Text = "Box:";
            // 
            // tabMain
            // 
            this.tabMain.AllowDrop = true;
            this.tabMain.Controls.Add(this.Tab_Main);
            this.tabMain.Controls.Add(this.Tab_Met);
            this.tabMain.Controls.Add(this.Tab_Stats);
            this.tabMain.Controls.Add(this.Tab_Attacks);
            this.tabMain.Controls.Add(this.Tab_OTMisc);
            this.tabMain.Location = new System.Drawing.Point(318, 30);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(280, 328);
            this.tabMain.TabIndex = 2;
            // 
            // Tab_Main
            // 
            this.Tab_Main.AllowDrop = true;
            this.Tab_Main.Controls.Add(this.FLP_Main);
            this.Tab_Main.Location = new System.Drawing.Point(4, 22);
            this.Tab_Main.Name = "Tab_Main";
            this.Tab_Main.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_Main.Size = new System.Drawing.Size(272, 302);
            this.Tab_Main.TabIndex = 0;
            this.Tab_Main.Text = "Main";
            this.Tab_Main.UseVisualStyleBackColor = true;
            // 
            // FLP_Main
            // 
            this.FLP_Main.Controls.Add(this.FLP_PID);
            this.FLP_Main.Controls.Add(this.FLP_Species);
            this.FLP_Main.Controls.Add(this.FLP_Nickname);
            this.FLP_Main.Controls.Add(this.FLP_EXPLevel);
            this.FLP_Main.Controls.Add(this.FLP_Nature);
            this.FLP_Main.Controls.Add(this.FLP_HeldItem);
            this.FLP_Main.Controls.Add(this.FLP_FriendshipForm);
            this.FLP_Main.Controls.Add(this.FLP_Ability);
            this.FLP_Main.Controls.Add(this.FLP_Language);
            this.FLP_Main.Controls.Add(this.FLP_EggPKRS);
            this.FLP_Main.Controls.Add(this.FLP_PKRS);
            this.FLP_Main.Controls.Add(this.FLP_Country);
            this.FLP_Main.Controls.Add(this.FLP_SubRegion);
            this.FLP_Main.Controls.Add(this.FLP_3DSRegion);
            this.FLP_Main.Location = new System.Drawing.Point(0, 2);
            this.FLP_Main.Name = "FLP_Main";
            this.FLP_Main.Size = new System.Drawing.Size(272, 301);
            this.FLP_Main.TabIndex = 103;
            // 
            // FLP_PID
            // 
            this.FLP_PID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_PID.Controls.Add(this.FLP_PIDLeft);
            this.FLP_PID.Controls.Add(this.FLP_PIDRight);
            this.FLP_PID.Location = new System.Drawing.Point(0, 0);
            this.FLP_PID.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_PID.Name = "FLP_PID";
            this.FLP_PID.Size = new System.Drawing.Size(272, 22);
            this.FLP_PID.TabIndex = 0;
            // 
            // FLP_PIDLeft
            // 
            this.FLP_PIDLeft.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.FLP_PIDLeft.Controls.Add(this.Label_PID);
            this.FLP_PIDLeft.Controls.Add(this.BTN_Shinytize);
            this.FLP_PIDLeft.Controls.Add(this.Label_IsShiny);
            this.FLP_PIDLeft.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.FLP_PIDLeft.Location = new System.Drawing.Point(0, 0);
            this.FLP_PIDLeft.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_PIDLeft.Name = "FLP_PIDLeft";
            this.FLP_PIDLeft.Size = new System.Drawing.Size(110, 22);
            this.FLP_PIDLeft.TabIndex = 0;
            // 
            // Label_PID
            // 
            this.Label_PID.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Label_PID.AutoSize = true;
            this.Label_PID.Location = new System.Drawing.Point(82, 5);
            this.Label_PID.Margin = new System.Windows.Forms.Padding(0, 5, 0, 4);
            this.Label_PID.Name = "Label_PID";
            this.Label_PID.Size = new System.Drawing.Size(28, 13);
            this.Label_PID.TabIndex = 0;
            this.Label_PID.Text = "PID:";
            this.Label_PID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BTN_Shinytize
            // 
            this.BTN_Shinytize.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BTN_Shinytize.Location = new System.Drawing.Point(58, 0);
            this.BTN_Shinytize.Margin = new System.Windows.Forms.Padding(0);
            this.BTN_Shinytize.Name = "BTN_Shinytize";
            this.BTN_Shinytize.Size = new System.Drawing.Size(24, 22);
            this.BTN_Shinytize.TabIndex = 1;
            this.BTN_Shinytize.Text = "☆";
            this.BTN_Shinytize.UseVisualStyleBackColor = true;
            this.BTN_Shinytize.Click += new System.EventHandler(this.updateShinyPID);
            // 
            // Label_IsShiny
            // 
            this.Label_IsShiny.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Label_IsShiny.Image = ((System.Drawing.Image)(resources.GetObject("Label_IsShiny.Image")));
            this.Label_IsShiny.InitialImage = ((System.Drawing.Image)(resources.GetObject("Label_IsShiny.InitialImage")));
            this.Label_IsShiny.Location = new System.Drawing.Point(36, 2);
            this.Label_IsShiny.Margin = new System.Windows.Forms.Padding(0, 2, 2, 0);
            this.Label_IsShiny.Name = "Label_IsShiny";
            this.Label_IsShiny.Size = new System.Drawing.Size(20, 20);
            this.Label_IsShiny.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Label_IsShiny.TabIndex = 62;
            this.Label_IsShiny.TabStop = false;
            this.Label_IsShiny.Visible = false;
            // 
            // FLP_PIDRight
            // 
            this.FLP_PIDRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_PIDRight.Controls.Add(this.TB_PID);
            this.FLP_PIDRight.Controls.Add(this.Label_Gender);
            this.FLP_PIDRight.Controls.Add(this.BTN_RerollPID);
            this.FLP_PIDRight.Location = new System.Drawing.Point(110, 0);
            this.FLP_PIDRight.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_PIDRight.Name = "FLP_PIDRight";
            this.FLP_PIDRight.Size = new System.Drawing.Size(162, 22);
            this.FLP_PIDRight.TabIndex = 104;
            // 
            // TB_PID
            // 
            this.TB_PID.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.TB_PID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_PID.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB_PID.Location = new System.Drawing.Point(0, 1);
            this.TB_PID.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.TB_PID.MaxLength = 8;
            this.TB_PID.Name = "TB_PID";
            this.TB_PID.Size = new System.Drawing.Size(60, 20);
            this.TB_PID.TabIndex = 1;
            this.TB_PID.Text = "12345678";
            this.TB_PID.TextChanged += new System.EventHandler(this.update_ID);
            this.TB_PID.MouseHover += new System.EventHandler(this.updateTSV);
            // 
            // Label_Gender
            // 
            this.Label_Gender.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Label_Gender.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Gender.Location = new System.Drawing.Point(60, 0);
            this.Label_Gender.Margin = new System.Windows.Forms.Padding(0);
            this.Label_Gender.Name = "Label_Gender";
            this.Label_Gender.Size = new System.Drawing.Size(19, 21);
            this.Label_Gender.TabIndex = 55;
            this.Label_Gender.Text = "-";
            this.Label_Gender.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_Gender.Click += new System.EventHandler(this.clickGender);
            // 
            // BTN_RerollPID
            // 
            this.BTN_RerollPID.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.BTN_RerollPID.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_RerollPID.Location = new System.Drawing.Point(79, 1);
            this.BTN_RerollPID.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.BTN_RerollPID.Name = "BTN_RerollPID";
            this.BTN_RerollPID.Size = new System.Drawing.Size(47, 20);
            this.BTN_RerollPID.TabIndex = 1;
            this.BTN_RerollPID.Text = "Reroll";
            this.BTN_RerollPID.UseVisualStyleBackColor = true;
            this.BTN_RerollPID.Click += new System.EventHandler(this.updateRandomPID);
            // 
            // FLP_Species
            // 
            this.FLP_Species.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_Species.Controls.Add(this.Label_Species);
            this.FLP_Species.Controls.Add(this.CB_Species);
            this.FLP_Species.Location = new System.Drawing.Point(0, 22);
            this.FLP_Species.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_Species.Name = "FLP_Species";
            this.FLP_Species.Size = new System.Drawing.Size(272, 21);
            this.FLP_Species.TabIndex = 1;
            // 
            // Label_Species
            // 
            this.Label_Species.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Label_Species.Location = new System.Drawing.Point(0, 0);
            this.Label_Species.Margin = new System.Windows.Forms.Padding(0);
            this.Label_Species.Name = "Label_Species";
            this.Label_Species.Size = new System.Drawing.Size(110, 21);
            this.Label_Species.TabIndex = 1;
            this.Label_Species.Text = "Species:";
            this.Label_Species.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Label_Species.Click += new System.EventHandler(this.updateNickname);
            // 
            // CB_Species
            // 
            this.CB_Species.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CB_Species.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CB_Species.FormattingEnabled = true;
            this.CB_Species.Location = new System.Drawing.Point(110, 0);
            this.CB_Species.Margin = new System.Windows.Forms.Padding(0);
            this.CB_Species.Name = "CB_Species";
            this.CB_Species.Size = new System.Drawing.Size(126, 21);
            this.CB_Species.TabIndex = 3;
            this.CB_Species.SelectedIndexChanged += new System.EventHandler(this.validateComboBox2);
            this.CB_Species.SelectedValueChanged += new System.EventHandler(this.updateSpecies);
            this.CB_Species.KeyDown += new System.Windows.Forms.KeyEventHandler(this.removedropCB);
            this.CB_Species.Validating += new System.ComponentModel.CancelEventHandler(this.validateComboBox);
            // 
            // FLP_Nickname
            // 
            this.FLP_Nickname.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_Nickname.Controls.Add(this.FLP_NicknameLeft);
            this.FLP_Nickname.Controls.Add(this.TB_Nickname);
            this.FLP_Nickname.Location = new System.Drawing.Point(0, 43);
            this.FLP_Nickname.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_Nickname.Name = "FLP_Nickname";
            this.FLP_Nickname.Size = new System.Drawing.Size(272, 22);
            this.FLP_Nickname.TabIndex = 2;
            // 
            // FLP_NicknameLeft
            // 
            this.FLP_NicknameLeft.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.FLP_NicknameLeft.Controls.Add(this.CHK_Nicknamed);
            this.FLP_NicknameLeft.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.FLP_NicknameLeft.Location = new System.Drawing.Point(0, 0);
            this.FLP_NicknameLeft.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_NicknameLeft.Name = "FLP_NicknameLeft";
            this.FLP_NicknameLeft.Size = new System.Drawing.Size(110, 21);
            this.FLP_NicknameLeft.TabIndex = 109;
            // 
            // CHK_Nicknamed
            // 
            this.CHK_Nicknamed.AutoSize = true;
            this.CHK_Nicknamed.Location = new System.Drawing.Point(36, 3);
            this.CHK_Nicknamed.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.CHK_Nicknamed.Name = "CHK_Nicknamed";
            this.CHK_Nicknamed.Size = new System.Drawing.Size(74, 17);
            this.CHK_Nicknamed.TabIndex = 4;
            this.CHK_Nicknamed.Text = "Nickname";
            this.CHK_Nicknamed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CHK_Nicknamed.UseVisualStyleBackColor = true;
            this.CHK_Nicknamed.CheckedChanged += new System.EventHandler(this.updateNickname);
            // 
            // TB_Nickname
            // 
            this.TB_Nickname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_Nickname.Location = new System.Drawing.Point(110, 0);
            this.TB_Nickname.Margin = new System.Windows.Forms.Padding(0);
            this.TB_Nickname.MaxLength = 12;
            this.TB_Nickname.Name = "TB_Nickname";
            this.TB_Nickname.Size = new System.Drawing.Size(126, 20);
            this.TB_Nickname.TabIndex = 5;
            this.TB_Nickname.TextChanged += new System.EventHandler(this.updateIsNicknamed);
            this.TB_Nickname.MouseDown += new System.Windows.Forms.MouseEventHandler(this.updateNicknameClick);
            // 
            // FLP_EXPLevel
            // 
            this.FLP_EXPLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_EXPLevel.Controls.Add(this.Label_EXP);
            this.FLP_EXPLevel.Controls.Add(this.FLP_EXPLevelRight);
            this.FLP_EXPLevel.Location = new System.Drawing.Point(0, 65);
            this.FLP_EXPLevel.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_EXPLevel.Name = "FLP_EXPLevel";
            this.FLP_EXPLevel.Size = new System.Drawing.Size(272, 21);
            this.FLP_EXPLevel.TabIndex = 3;
            // 
            // Label_EXP
            // 
            this.Label_EXP.Location = new System.Drawing.Point(0, 0);
            this.Label_EXP.Margin = new System.Windows.Forms.Padding(0);
            this.Label_EXP.Name = "Label_EXP";
            this.Label_EXP.Size = new System.Drawing.Size(110, 21);
            this.Label_EXP.TabIndex = 3;
            this.Label_EXP.Text = "EXP:";
            this.Label_EXP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FLP_EXPLevelRight
            // 
            this.FLP_EXPLevelRight.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.FLP_EXPLevelRight.Controls.Add(this.TB_EXP);
            this.FLP_EXPLevelRight.Controls.Add(this.Label_CurLevel);
            this.FLP_EXPLevelRight.Controls.Add(this.TB_Level);
            this.FLP_EXPLevelRight.Controls.Add(this.MT_Level);
            this.FLP_EXPLevelRight.Location = new System.Drawing.Point(110, 0);
            this.FLP_EXPLevelRight.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_EXPLevelRight.Name = "FLP_EXPLevelRight";
            this.FLP_EXPLevelRight.Size = new System.Drawing.Size(162, 21);
            this.FLP_EXPLevelRight.TabIndex = 0;
            // 
            // TB_EXP
            // 
            this.TB_EXP.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.TB_EXP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_EXP.Location = new System.Drawing.Point(0, 0);
            this.TB_EXP.Margin = new System.Windows.Forms.Padding(0);
            this.TB_EXP.Mask = "0000000";
            this.TB_EXP.Name = "TB_EXP";
            this.TB_EXP.Size = new System.Drawing.Size(46, 20);
            this.TB_EXP.TabIndex = 7;
            this.TB_EXP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Label_CurLevel
            // 
            this.Label_CurLevel.Location = new System.Drawing.Point(46, 0);
            this.Label_CurLevel.Margin = new System.Windows.Forms.Padding(0);
            this.Label_CurLevel.Name = "Label_CurLevel";
            this.Label_CurLevel.Size = new System.Drawing.Size(58, 21);
            this.Label_CurLevel.TabIndex = 7;
            this.Label_CurLevel.Text = "Level:";
            this.Label_CurLevel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Label_CurLevel.Click += new System.EventHandler(this.clickMetLocation);
            // 
            // TB_Level
            // 
            this.TB_Level.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_Level.Location = new System.Drawing.Point(104, 0);
            this.TB_Level.Margin = new System.Windows.Forms.Padding(0);
            this.TB_Level.Mask = "000";
            this.TB_Level.Name = "TB_Level";
            this.TB_Level.Size = new System.Drawing.Size(22, 20);
            this.TB_Level.TabIndex = 8;
            this.TB_Level.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_Level.Click += new System.EventHandler(this.clickLevel);
            this.TB_Level.TextChanged += new System.EventHandler(this.updateEXPLevel);
            // 
            // MT_Level
            // 
            this.MT_Level.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MT_Level.Enabled = false;
            this.MT_Level.Location = new System.Drawing.Point(126, 0);
            this.MT_Level.Margin = new System.Windows.Forms.Padding(0);
            this.MT_Level.Mask = "000";
            this.MT_Level.Name = "MT_Level";
            this.MT_Level.Size = new System.Drawing.Size(22, 20);
            this.MT_Level.TabIndex = 17;
            this.MT_Level.Visible = false;
            this.MT_Level.TextChanged += new System.EventHandler(this.updateEXPLevel);
            // 
            // FLP_Nature
            // 
            this.FLP_Nature.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_Nature.Controls.Add(this.Label_Nature);
            this.FLP_Nature.Controls.Add(this.CB_Nature);
            this.FLP_Nature.Location = new System.Drawing.Point(0, 86);
            this.FLP_Nature.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_Nature.Name = "FLP_Nature";
            this.FLP_Nature.Size = new System.Drawing.Size(272, 21);
            this.FLP_Nature.TabIndex = 4;
            // 
            // Label_Nature
            // 
            this.Label_Nature.Location = new System.Drawing.Point(0, 0);
            this.Label_Nature.Margin = new System.Windows.Forms.Padding(0);
            this.Label_Nature.Name = "Label_Nature";
            this.Label_Nature.Size = new System.Drawing.Size(110, 21);
            this.Label_Nature.TabIndex = 8;
            this.Label_Nature.Text = "Nature:";
            this.Label_Nature.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CB_Nature
            // 
            this.CB_Nature.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CB_Nature.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CB_Nature.FormattingEnabled = true;
            this.CB_Nature.Location = new System.Drawing.Point(110, 0);
            this.CB_Nature.Margin = new System.Windows.Forms.Padding(0);
            this.CB_Nature.Name = "CB_Nature";
            this.CB_Nature.Size = new System.Drawing.Size(126, 21);
            this.CB_Nature.TabIndex = 9;
            this.CB_Nature.SelectedIndexChanged += new System.EventHandler(this.validateComboBox2);
            this.CB_Nature.KeyDown += new System.Windows.Forms.KeyEventHandler(this.removedropCB);
            this.CB_Nature.MouseHover += new System.EventHandler(this.updateNatureModification);
            this.CB_Nature.Validating += new System.ComponentModel.CancelEventHandler(this.validateComboBox);
            // 
            // FLP_HeldItem
            // 
            this.FLP_HeldItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_HeldItem.Controls.Add(this.Label_HeldItem);
            this.FLP_HeldItem.Controls.Add(this.CB_HeldItem);
            this.FLP_HeldItem.Location = new System.Drawing.Point(0, 107);
            this.FLP_HeldItem.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_HeldItem.Name = "FLP_HeldItem";
            this.FLP_HeldItem.Size = new System.Drawing.Size(272, 21);
            this.FLP_HeldItem.TabIndex = 5;
            // 
            // Label_HeldItem
            // 
            this.Label_HeldItem.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Label_HeldItem.Location = new System.Drawing.Point(0, 0);
            this.Label_HeldItem.Margin = new System.Windows.Forms.Padding(0);
            this.Label_HeldItem.Name = "Label_HeldItem";
            this.Label_HeldItem.Size = new System.Drawing.Size(110, 21);
            this.Label_HeldItem.TabIndex = 51;
            this.Label_HeldItem.Text = "Held Item:";
            this.Label_HeldItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CB_HeldItem
            // 
            this.CB_HeldItem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CB_HeldItem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CB_HeldItem.FormattingEnabled = true;
            this.CB_HeldItem.Location = new System.Drawing.Point(110, 0);
            this.CB_HeldItem.Margin = new System.Windows.Forms.Padding(0);
            this.CB_HeldItem.Name = "CB_HeldItem";
            this.CB_HeldItem.Size = new System.Drawing.Size(126, 21);
            this.CB_HeldItem.TabIndex = 10;
            this.CB_HeldItem.SelectedIndexChanged += new System.EventHandler(this.validateComboBox2);
            this.CB_HeldItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.removedropCB);
            this.CB_HeldItem.Validating += new System.ComponentModel.CancelEventHandler(this.validateComboBox);
            // 
            // FLP_FriendshipForm
            // 
            this.FLP_FriendshipForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_FriendshipForm.Controls.Add(this.FLP_FriendshipFormLeft);
            this.FLP_FriendshipForm.Controls.Add(this.FLP_FriendshipFormRight);
            this.FLP_FriendshipForm.Location = new System.Drawing.Point(0, 128);
            this.FLP_FriendshipForm.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_FriendshipForm.Name = "FLP_FriendshipForm";
            this.FLP_FriendshipForm.Size = new System.Drawing.Size(272, 21);
            this.FLP_FriendshipForm.TabIndex = 6;
            // 
            // FLP_FriendshipFormLeft
            // 
            this.FLP_FriendshipFormLeft.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.FLP_FriendshipFormLeft.Controls.Add(this.Label_Friendship);
            this.FLP_FriendshipFormLeft.Controls.Add(this.Label_HatchCounter);
            this.FLP_FriendshipFormLeft.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.FLP_FriendshipFormLeft.Location = new System.Drawing.Point(0, 0);
            this.FLP_FriendshipFormLeft.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_FriendshipFormLeft.Name = "FLP_FriendshipFormLeft";
            this.FLP_FriendshipFormLeft.Size = new System.Drawing.Size(110, 21);
            this.FLP_FriendshipFormLeft.TabIndex = 0;
            // 
            // Label_Friendship
            // 
            this.Label_Friendship.Location = new System.Drawing.Point(0, 0);
            this.Label_Friendship.Margin = new System.Windows.Forms.Padding(0);
            this.Label_Friendship.Name = "Label_Friendship";
            this.Label_Friendship.Size = new System.Drawing.Size(110, 21);
            this.Label_Friendship.TabIndex = 9;
            this.Label_Friendship.Text = "Friendship:";
            this.Label_Friendship.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Label_Friendship.Click += new System.EventHandler(this.clickFriendship);
            // 
            // Label_HatchCounter
            // 
            this.Label_HatchCounter.Location = new System.Drawing.Point(0, 21);
            this.Label_HatchCounter.Margin = new System.Windows.Forms.Padding(0);
            this.Label_HatchCounter.Name = "Label_HatchCounter";
            this.Label_HatchCounter.Size = new System.Drawing.Size(110, 21);
            this.Label_HatchCounter.TabIndex = 61;
            this.Label_HatchCounter.Text = "Hatch Counter:";
            this.Label_HatchCounter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FLP_FriendshipFormRight
            // 
            this.FLP_FriendshipFormRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_FriendshipFormRight.Controls.Add(this.TB_Friendship);
            this.FLP_FriendshipFormRight.Controls.Add(this.Label_Form);
            this.FLP_FriendshipFormRight.Controls.Add(this.CB_Form);
            this.FLP_FriendshipFormRight.Location = new System.Drawing.Point(110, 0);
            this.FLP_FriendshipFormRight.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_FriendshipFormRight.Name = "FLP_FriendshipFormRight";
            this.FLP_FriendshipFormRight.Size = new System.Drawing.Size(162, 21);
            this.FLP_FriendshipFormRight.TabIndex = 104;
            // 
            // TB_Friendship
            // 
            this.TB_Friendship.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_Friendship.Location = new System.Drawing.Point(0, 0);
            this.TB_Friendship.Margin = new System.Windows.Forms.Padding(0);
            this.TB_Friendship.Mask = "000";
            this.TB_Friendship.Name = "TB_Friendship";
            this.TB_Friendship.Size = new System.Drawing.Size(22, 20);
            this.TB_Friendship.TabIndex = 11;
            this.TB_Friendship.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_Friendship.TextChanged += new System.EventHandler(this.update255_MTB);
            // 
            // Label_Form
            // 
            this.Label_Form.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Label_Form.AutoSize = true;
            this.Label_Form.Location = new System.Drawing.Point(22, 4);
            this.Label_Form.Margin = new System.Windows.Forms.Padding(0);
            this.Label_Form.Name = "Label_Form";
            this.Label_Form.Size = new System.Drawing.Size(33, 13);
            this.Label_Form.TabIndex = 11;
            this.Label_Form.Text = "Form:";
            this.Label_Form.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CB_Form
            // 
            this.CB_Form.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Form.DropDownWidth = 85;
            this.CB_Form.Enabled = false;
            this.CB_Form.FormattingEnabled = true;
            this.CB_Form.Location = new System.Drawing.Point(55, 0);
            this.CB_Form.Margin = new System.Windows.Forms.Padding(0);
            this.CB_Form.Name = "CB_Form";
            this.CB_Form.Size = new System.Drawing.Size(71, 21);
            this.CB_Form.TabIndex = 12;
            this.CB_Form.SelectedIndexChanged += new System.EventHandler(this.updateForm);
            // 
            // FLP_Ability
            // 
            this.FLP_Ability.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_Ability.Controls.Add(this.Label_Ability);
            this.FLP_Ability.Controls.Add(this.FLP_AbilityRight);
            this.FLP_Ability.Location = new System.Drawing.Point(0, 149);
            this.FLP_Ability.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_Ability.Name = "FLP_Ability";
            this.FLP_Ability.Size = new System.Drawing.Size(272, 21);
            this.FLP_Ability.TabIndex = 7;
            // 
            // Label_Ability
            // 
            this.Label_Ability.Location = new System.Drawing.Point(0, 0);
            this.Label_Ability.Margin = new System.Windows.Forms.Padding(0);
            this.Label_Ability.Name = "Label_Ability";
            this.Label_Ability.Size = new System.Drawing.Size(110, 21);
            this.Label_Ability.TabIndex = 10;
            this.Label_Ability.Text = "Ability:";
            this.Label_Ability.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FLP_AbilityRight
            // 
            this.FLP_AbilityRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_AbilityRight.Controls.Add(this.CB_Ability);
            this.FLP_AbilityRight.Controls.Add(this.DEV_Ability);
            this.FLP_AbilityRight.Controls.Add(this.TB_AbilityNumber);
            this.FLP_AbilityRight.Location = new System.Drawing.Point(110, 0);
            this.FLP_AbilityRight.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_AbilityRight.Name = "FLP_AbilityRight";
            this.FLP_AbilityRight.Size = new System.Drawing.Size(162, 21);
            this.FLP_AbilityRight.TabIndex = 109;
            // 
            // CB_Ability
            // 
            this.CB_Ability.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CB_Ability.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CB_Ability.FormattingEnabled = true;
            this.CB_Ability.Items.AddRange(new object[] {
            "Item"});
            this.CB_Ability.Location = new System.Drawing.Point(0, 0);
            this.CB_Ability.Margin = new System.Windows.Forms.Padding(0);
            this.CB_Ability.Name = "CB_Ability";
            this.CB_Ability.Size = new System.Drawing.Size(126, 21);
            this.CB_Ability.TabIndex = 13;
            this.CB_Ability.SelectedIndexChanged += new System.EventHandler(this.validateComboBox2);
            this.CB_Ability.KeyDown += new System.Windows.Forms.KeyEventHandler(this.removedropCB);
            this.CB_Ability.Validating += new System.ComponentModel.CancelEventHandler(this.validateComboBox);
            // 
            // DEV_Ability
            // 
            this.DEV_Ability.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.DEV_Ability.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.DEV_Ability.Enabled = false;
            this.DEV_Ability.FormattingEnabled = true;
            this.DEV_Ability.Items.AddRange(new object[] {
            "Item"});
            this.DEV_Ability.Location = new System.Drawing.Point(0, 21);
            this.DEV_Ability.Margin = new System.Windows.Forms.Padding(0);
            this.DEV_Ability.Name = "DEV_Ability";
            this.DEV_Ability.Size = new System.Drawing.Size(126, 21);
            this.DEV_Ability.TabIndex = 14;
            this.DEV_Ability.Visible = false;
            // 
            // TB_AbilityNumber
            // 
            this.TB_AbilityNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_AbilityNumber.Location = new System.Drawing.Point(126, 21);
            this.TB_AbilityNumber.Margin = new System.Windows.Forms.Padding(0);
            this.TB_AbilityNumber.Mask = "0";
            this.TB_AbilityNumber.Name = "TB_AbilityNumber";
            this.TB_AbilityNumber.Size = new System.Drawing.Size(19, 20);
            this.TB_AbilityNumber.TabIndex = 14;
            this.TB_AbilityNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_AbilityNumber.Visible = false;
            // 
            // FLP_Language
            // 
            this.FLP_Language.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_Language.Controls.Add(this.Label_Language);
            this.FLP_Language.Controls.Add(this.CB_Language);
            this.FLP_Language.Location = new System.Drawing.Point(0, 170);
            this.FLP_Language.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_Language.Name = "FLP_Language";
            this.FLP_Language.Size = new System.Drawing.Size(272, 21);
            this.FLP_Language.TabIndex = 8;
            // 
            // Label_Language
            // 
            this.Label_Language.Location = new System.Drawing.Point(0, 0);
            this.Label_Language.Margin = new System.Windows.Forms.Padding(0);
            this.Label_Language.Name = "Label_Language";
            this.Label_Language.Size = new System.Drawing.Size(110, 21);
            this.Label_Language.TabIndex = 12;
            this.Label_Language.Text = "Language:";
            this.Label_Language.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CB_Language
            // 
            this.CB_Language.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.CB_Language.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Language.FormattingEnabled = true;
            this.CB_Language.Location = new System.Drawing.Point(110, 0);
            this.CB_Language.Margin = new System.Windows.Forms.Padding(0);
            this.CB_Language.Name = "CB_Language";
            this.CB_Language.Size = new System.Drawing.Size(126, 21);
            this.CB_Language.TabIndex = 15;
            this.CB_Language.SelectedIndexChanged += new System.EventHandler(this.updateNickname);
            // 
            // FLP_EggPKRS
            // 
            this.FLP_EggPKRS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_EggPKRS.Controls.Add(this.FLP_EggPKRSLeft);
            this.FLP_EggPKRS.Controls.Add(this.FLP_EggPKRSRight);
            this.FLP_EggPKRS.Location = new System.Drawing.Point(0, 191);
            this.FLP_EggPKRS.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_EggPKRS.Name = "FLP_EggPKRS";
            this.FLP_EggPKRS.Size = new System.Drawing.Size(272, 21);
            this.FLP_EggPKRS.TabIndex = 9;
            // 
            // FLP_EggPKRSLeft
            // 
            this.FLP_EggPKRSLeft.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.FLP_EggPKRSLeft.Controls.Add(this.CHK_IsEgg);
            this.FLP_EggPKRSLeft.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.FLP_EggPKRSLeft.Location = new System.Drawing.Point(0, 0);
            this.FLP_EggPKRSLeft.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_EggPKRSLeft.Name = "FLP_EggPKRSLeft";
            this.FLP_EggPKRSLeft.Size = new System.Drawing.Size(110, 21);
            this.FLP_EggPKRSLeft.TabIndex = 0;
            // 
            // CHK_IsEgg
            // 
            this.CHK_IsEgg.AutoSize = true;
            this.CHK_IsEgg.Location = new System.Drawing.Point(54, 3);
            this.CHK_IsEgg.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.CHK_IsEgg.Name = "CHK_IsEgg";
            this.CHK_IsEgg.Size = new System.Drawing.Size(56, 17);
            this.CHK_IsEgg.TabIndex = 16;
            this.CHK_IsEgg.Text = "Is Egg";
            this.CHK_IsEgg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CHK_IsEgg.UseVisualStyleBackColor = true;
            // 
            // FLP_EggPKRSRight
            // 
            this.FLP_EggPKRSRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_EggPKRSRight.Controls.Add(this.CHK_Infected);
            this.FLP_EggPKRSRight.Controls.Add(this.CHK_Cured);
            this.FLP_EggPKRSRight.Location = new System.Drawing.Point(110, 0);
            this.FLP_EggPKRSRight.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_EggPKRSRight.Name = "FLP_EggPKRSRight";
            this.FLP_EggPKRSRight.Size = new System.Drawing.Size(162, 21);
            this.FLP_EggPKRSRight.TabIndex = 104;
            // 
            // CHK_Infected
            // 
            this.CHK_Infected.AutoSize = true;
            this.CHK_Infected.Location = new System.Drawing.Point(0, 3);
            this.CHK_Infected.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.CHK_Infected.Name = "CHK_Infected";
            this.CHK_Infected.Size = new System.Drawing.Size(65, 17);
            this.CHK_Infected.TabIndex = 17;
            this.CHK_Infected.Text = "Infected";
            this.CHK_Infected.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CHK_Infected.UseVisualStyleBackColor = true;
            this.CHK_Infected.CheckedChanged += new System.EventHandler(this.updatePKRSInfected);
            // 
            // CHK_Cured
            // 
            this.CHK_Cured.AutoSize = true;
            this.CHK_Cured.Location = new System.Drawing.Point(65, 3);
            this.CHK_Cured.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.CHK_Cured.Name = "CHK_Cured";
            this.CHK_Cured.Size = new System.Drawing.Size(54, 17);
            this.CHK_Cured.TabIndex = 18;
            this.CHK_Cured.Text = "Cured";
            this.CHK_Cured.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CHK_Cured.UseVisualStyleBackColor = true;
            this.CHK_Cured.CheckedChanged += new System.EventHandler(this.updatePKRSCured);
            // 
            // FLP_PKRS
            // 
            this.FLP_PKRS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_PKRS.Controls.Add(this.Label_PKRS);
            this.FLP_PKRS.Controls.Add(this.FLP_PKRSRight);
            this.FLP_PKRS.Location = new System.Drawing.Point(0, 212);
            this.FLP_PKRS.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_PKRS.Name = "FLP_PKRS";
            this.FLP_PKRS.Size = new System.Drawing.Size(272, 21);
            this.FLP_PKRS.TabIndex = 10;
            // 
            // Label_PKRS
            // 
            this.Label_PKRS.Location = new System.Drawing.Point(0, 0);
            this.Label_PKRS.Margin = new System.Windows.Forms.Padding(0);
            this.Label_PKRS.Name = "Label_PKRS";
            this.Label_PKRS.Size = new System.Drawing.Size(110, 21);
            this.Label_PKRS.TabIndex = 14;
            this.Label_PKRS.Text = "PkRs:";
            this.Label_PKRS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Label_PKRS.Visible = false;
            // 
            // FLP_PKRSRight
            // 
            this.FLP_PKRSRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_PKRSRight.Controls.Add(this.CB_PKRSStrain);
            this.FLP_PKRSRight.Controls.Add(this.Label_PKRSdays);
            this.FLP_PKRSRight.Controls.Add(this.CB_PKRSDays);
            this.FLP_PKRSRight.Location = new System.Drawing.Point(110, 0);
            this.FLP_PKRSRight.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_PKRSRight.Name = "FLP_PKRSRight";
            this.FLP_PKRSRight.Size = new System.Drawing.Size(162, 21);
            this.FLP_PKRSRight.TabIndex = 105;
            // 
            // CB_PKRSStrain
            // 
            this.CB_PKRSStrain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_PKRSStrain.FormattingEnabled = true;
            this.CB_PKRSStrain.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15"});
            this.CB_PKRSStrain.Location = new System.Drawing.Point(0, 0);
            this.CB_PKRSStrain.Margin = new System.Windows.Forms.Padding(0);
            this.CB_PKRSStrain.Name = "CB_PKRSStrain";
            this.CB_PKRSStrain.Size = new System.Drawing.Size(43, 21);
            this.CB_PKRSStrain.TabIndex = 19;
            this.CB_PKRSStrain.Visible = false;
            this.CB_PKRSStrain.SelectedIndexChanged += new System.EventHandler(this.updatePKRSstrain);
            // 
            // Label_PKRSdays
            // 
            this.Label_PKRSdays.Location = new System.Drawing.Point(43, 0);
            this.Label_PKRSdays.Margin = new System.Windows.Forms.Padding(0);
            this.Label_PKRSdays.Name = "Label_PKRSdays";
            this.Label_PKRSdays.Size = new System.Drawing.Size(25, 21);
            this.Label_PKRSdays.TabIndex = 15;
            this.Label_PKRSdays.Text = "d:";
            this.Label_PKRSdays.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Label_PKRSdays.Visible = false;
            // 
            // CB_PKRSDays
            // 
            this.CB_PKRSDays.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_PKRSDays.FormattingEnabled = true;
            this.CB_PKRSDays.Location = new System.Drawing.Point(68, 0);
            this.CB_PKRSDays.Margin = new System.Windows.Forms.Padding(0);
            this.CB_PKRSDays.Name = "CB_PKRSDays";
            this.CB_PKRSDays.Size = new System.Drawing.Size(30, 21);
            this.CB_PKRSDays.TabIndex = 20;
            this.CB_PKRSDays.Visible = false;
            this.CB_PKRSDays.SelectedIndexChanged += new System.EventHandler(this.updatePKRSdays);
            // 
            // FLP_Country
            // 
            this.FLP_Country.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_Country.Controls.Add(this.Label_Country);
            this.FLP_Country.Controls.Add(this.CB_Country);
            this.FLP_Country.Location = new System.Drawing.Point(0, 233);
            this.FLP_Country.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_Country.Name = "FLP_Country";
            this.FLP_Country.Size = new System.Drawing.Size(272, 21);
            this.FLP_Country.TabIndex = 107;
            // 
            // Label_Country
            // 
            this.Label_Country.Location = new System.Drawing.Point(0, 0);
            this.Label_Country.Margin = new System.Windows.Forms.Padding(0);
            this.Label_Country.Name = "Label_Country";
            this.Label_Country.Size = new System.Drawing.Size(110, 21);
            this.Label_Country.TabIndex = 16;
            this.Label_Country.Text = "Country:";
            this.Label_Country.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CB_Country
            // 
            this.CB_Country.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CB_Country.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CB_Country.DropDownWidth = 180;
            this.CB_Country.FormattingEnabled = true;
            this.CB_Country.Location = new System.Drawing.Point(110, 0);
            this.CB_Country.Margin = new System.Windows.Forms.Padding(0);
            this.CB_Country.Name = "CB_Country";
            this.CB_Country.Size = new System.Drawing.Size(126, 21);
            this.CB_Country.TabIndex = 21;
            this.CB_Country.SelectedIndexChanged += new System.EventHandler(this.updateCountry);
            this.CB_Country.KeyDown += new System.Windows.Forms.KeyEventHandler(this.removedropCB);
            // 
            // FLP_SubRegion
            // 
            this.FLP_SubRegion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_SubRegion.Controls.Add(this.Label_SubRegion);
            this.FLP_SubRegion.Controls.Add(this.CB_SubRegion);
            this.FLP_SubRegion.Location = new System.Drawing.Point(0, 254);
            this.FLP_SubRegion.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_SubRegion.Name = "FLP_SubRegion";
            this.FLP_SubRegion.Size = new System.Drawing.Size(272, 21);
            this.FLP_SubRegion.TabIndex = 110;
            // 
            // Label_SubRegion
            // 
            this.Label_SubRegion.Location = new System.Drawing.Point(0, 0);
            this.Label_SubRegion.Margin = new System.Windows.Forms.Padding(0);
            this.Label_SubRegion.Name = "Label_SubRegion";
            this.Label_SubRegion.Size = new System.Drawing.Size(110, 21);
            this.Label_SubRegion.TabIndex = 17;
            this.Label_SubRegion.Text = "Sub Region:";
            this.Label_SubRegion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CB_SubRegion
            // 
            this.CB_SubRegion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CB_SubRegion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CB_SubRegion.DropDownWidth = 180;
            this.CB_SubRegion.FormattingEnabled = true;
            this.CB_SubRegion.Location = new System.Drawing.Point(110, 0);
            this.CB_SubRegion.Margin = new System.Windows.Forms.Padding(0);
            this.CB_SubRegion.Name = "CB_SubRegion";
            this.CB_SubRegion.Size = new System.Drawing.Size(126, 21);
            this.CB_SubRegion.TabIndex = 22;
            // 
            // FLP_3DSRegion
            // 
            this.FLP_3DSRegion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_3DSRegion.Controls.Add(this.Label_3DSRegion);
            this.FLP_3DSRegion.Controls.Add(this.CB_3DSReg);
            this.FLP_3DSRegion.Location = new System.Drawing.Point(0, 275);
            this.FLP_3DSRegion.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_3DSRegion.Name = "FLP_3DSRegion";
            this.FLP_3DSRegion.Size = new System.Drawing.Size(272, 21);
            this.FLP_3DSRegion.TabIndex = 111;
            // 
            // Label_3DSRegion
            // 
            this.Label_3DSRegion.Location = new System.Drawing.Point(0, 0);
            this.Label_3DSRegion.Margin = new System.Windows.Forms.Padding(0);
            this.Label_3DSRegion.Name = "Label_3DSRegion";
            this.Label_3DSRegion.Size = new System.Drawing.Size(110, 21);
            this.Label_3DSRegion.TabIndex = 18;
            this.Label_3DSRegion.Text = "3DS Region:";
            this.Label_3DSRegion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CB_3DSReg
            // 
            this.CB_3DSReg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_3DSReg.FormattingEnabled = true;
            this.CB_3DSReg.Location = new System.Drawing.Point(110, 0);
            this.CB_3DSReg.Margin = new System.Windows.Forms.Padding(0);
            this.CB_3DSReg.Name = "CB_3DSReg";
            this.CB_3DSReg.Size = new System.Drawing.Size(126, 21);
            this.CB_3DSReg.TabIndex = 23;
            // 
            // Tab_Met
            // 
            this.Tab_Met.AllowDrop = true;
            this.Tab_Met.Controls.Add(this.CHK_AsEgg);
            this.Tab_Met.Controls.Add(this.GB_EggConditions);
            this.Tab_Met.Controls.Add(this.FLP_Met);
            this.Tab_Met.Location = new System.Drawing.Point(4, 22);
            this.Tab_Met.Name = "Tab_Met";
            this.Tab_Met.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_Met.Size = new System.Drawing.Size(272, 302);
            this.Tab_Met.TabIndex = 1;
            this.Tab_Met.Text = "Met";
            this.Tab_Met.UseVisualStyleBackColor = true;
            // 
            // CHK_AsEgg
            // 
            this.CHK_AsEgg.AutoSize = true;
            this.CHK_AsEgg.Location = new System.Drawing.Point(110, 204);
            this.CHK_AsEgg.Name = "CHK_AsEgg";
            this.CHK_AsEgg.Size = new System.Drawing.Size(60, 17);
            this.CHK_AsEgg.TabIndex = 8;
            this.CHK_AsEgg.Text = "As Egg";
            this.CHK_AsEgg.UseVisualStyleBackColor = true;
            this.CHK_AsEgg.Click += new System.EventHandler(this.updateMetAsEgg);
            // 
            // GB_EggConditions
            // 
            this.GB_EggConditions.Controls.Add(this.CB_EggLocation);
            this.GB_EggConditions.Controls.Add(this.CAL_EggDate);
            this.GB_EggConditions.Controls.Add(this.Label_EggDate);
            this.GB_EggConditions.Controls.Add(this.Label_EggLocation);
            this.GB_EggConditions.Enabled = false;
            this.GB_EggConditions.Location = new System.Drawing.Point(39, 226);
            this.GB_EggConditions.Name = "GB_EggConditions";
            this.GB_EggConditions.Size = new System.Drawing.Size(200, 67);
            this.GB_EggConditions.TabIndex = 9;
            this.GB_EggConditions.TabStop = false;
            this.GB_EggConditions.Text = "Egg Met Conditions";
            // 
            // CB_EggLocation
            // 
            this.CB_EggLocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CB_EggLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CB_EggLocation.DropDownWidth = 150;
            this.CB_EggLocation.FormattingEnabled = true;
            this.CB_EggLocation.Location = new System.Drawing.Point(71, 19);
            this.CB_EggLocation.Name = "CB_EggLocation";
            this.CB_EggLocation.Size = new System.Drawing.Size(122, 21);
            this.CB_EggLocation.TabIndex = 10;
            this.CB_EggLocation.SelectedIndexChanged += new System.EventHandler(this.validateLocation);
            this.CB_EggLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.removedropCB);
            this.CB_EggLocation.Validating += new System.ComponentModel.CancelEventHandler(this.validateComboBox);
            // 
            // CAL_EggDate
            // 
            this.CAL_EggDate.CustomFormat = "MM/dd/yyyy";
            this.CAL_EggDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.CAL_EggDate.Location = new System.Drawing.Point(71, 40);
            this.CAL_EggDate.MaxDate = new System.DateTime(2099, 12, 31, 0, 0, 0, 0);
            this.CAL_EggDate.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.CAL_EggDate.Name = "CAL_EggDate";
            this.CAL_EggDate.Size = new System.Drawing.Size(122, 20);
            this.CAL_EggDate.TabIndex = 11;
            this.CAL_EggDate.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            // 
            // Label_EggDate
            // 
            this.Label_EggDate.Location = new System.Drawing.Point(5, 44);
            this.Label_EggDate.Name = "Label_EggDate";
            this.Label_EggDate.Size = new System.Drawing.Size(63, 13);
            this.Label_EggDate.TabIndex = 8;
            this.Label_EggDate.Text = "Date:";
            this.Label_EggDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label_EggLocation
            // 
            this.Label_EggLocation.Location = new System.Drawing.Point(5, 24);
            this.Label_EggLocation.Name = "Label_EggLocation";
            this.Label_EggLocation.Size = new System.Drawing.Size(63, 13);
            this.Label_EggLocation.TabIndex = 6;
            this.Label_EggLocation.Text = "Location:";
            this.Label_EggLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FLP_Met
            // 
            this.FLP_Met.Controls.Add(this.FLP_OriginGame);
            this.FLP_Met.Controls.Add(this.FLP_MetLocation);
            this.FLP_Met.Controls.Add(this.FLP_Ball);
            this.FLP_Met.Controls.Add(this.FLP_MetLevel);
            this.FLP_Met.Controls.Add(this.FLP_MetDate);
            this.FLP_Met.Controls.Add(this.FLP_Fateful);
            this.FLP_Met.Controls.Add(this.FLP_EncounterType);
            this.FLP_Met.Controls.Add(this.FLP_TimeOfDay);
            this.FLP_Met.Location = new System.Drawing.Point(0, 24);
            this.FLP_Met.Name = "FLP_Met";
            this.FLP_Met.Size = new System.Drawing.Size(272, 175);
            this.FLP_Met.TabIndex = 103;
            // 
            // FLP_OriginGame
            // 
            this.FLP_OriginGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_OriginGame.Controls.Add(this.Label_OriginGame);
            this.FLP_OriginGame.Controls.Add(this.CB_GameOrigin);
            this.FLP_OriginGame.Location = new System.Drawing.Point(0, 0);
            this.FLP_OriginGame.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_OriginGame.Name = "FLP_OriginGame";
            this.FLP_OriginGame.Size = new System.Drawing.Size(272, 21);
            this.FLP_OriginGame.TabIndex = 112;
            // 
            // Label_OriginGame
            // 
            this.Label_OriginGame.Location = new System.Drawing.Point(0, 0);
            this.Label_OriginGame.Margin = new System.Windows.Forms.Padding(0);
            this.Label_OriginGame.Name = "Label_OriginGame";
            this.Label_OriginGame.Size = new System.Drawing.Size(110, 21);
            this.Label_OriginGame.TabIndex = 0;
            this.Label_OriginGame.Text = "Origin Game:";
            this.Label_OriginGame.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CB_GameOrigin
            // 
            this.CB_GameOrigin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_GameOrigin.FormattingEnabled = true;
            this.CB_GameOrigin.Location = new System.Drawing.Point(110, 0);
            this.CB_GameOrigin.Margin = new System.Windows.Forms.Padding(0);
            this.CB_GameOrigin.Name = "CB_GameOrigin";
            this.CB_GameOrigin.Size = new System.Drawing.Size(126, 21);
            this.CB_GameOrigin.TabIndex = 1;
            this.CB_GameOrigin.SelectedIndexChanged += new System.EventHandler(this.updateOriginGame);
            // 
            // FLP_MetLocation
            // 
            this.FLP_MetLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_MetLocation.Controls.Add(this.Label_MetLocation);
            this.FLP_MetLocation.Controls.Add(this.CB_MetLocation);
            this.FLP_MetLocation.Location = new System.Drawing.Point(0, 21);
            this.FLP_MetLocation.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_MetLocation.Name = "FLP_MetLocation";
            this.FLP_MetLocation.Size = new System.Drawing.Size(272, 21);
            this.FLP_MetLocation.TabIndex = 113;
            // 
            // Label_MetLocation
            // 
            this.Label_MetLocation.Location = new System.Drawing.Point(0, 0);
            this.Label_MetLocation.Margin = new System.Windows.Forms.Padding(0);
            this.Label_MetLocation.Name = "Label_MetLocation";
            this.Label_MetLocation.Size = new System.Drawing.Size(110, 21);
            this.Label_MetLocation.TabIndex = 1;
            this.Label_MetLocation.Text = "Met Location:";
            this.Label_MetLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Label_MetLocation.Click += new System.EventHandler(this.clickMetLocation);
            // 
            // CB_MetLocation
            // 
            this.CB_MetLocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CB_MetLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CB_MetLocation.DropDownWidth = 150;
            this.CB_MetLocation.FormattingEnabled = true;
            this.CB_MetLocation.Location = new System.Drawing.Point(110, 0);
            this.CB_MetLocation.Margin = new System.Windows.Forms.Padding(0);
            this.CB_MetLocation.Name = "CB_MetLocation";
            this.CB_MetLocation.Size = new System.Drawing.Size(126, 21);
            this.CB_MetLocation.TabIndex = 2;
            this.CB_MetLocation.SelectedIndexChanged += new System.EventHandler(this.validateLocation);
            this.CB_MetLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.removedropCB);
            this.CB_MetLocation.Validating += new System.ComponentModel.CancelEventHandler(this.validateComboBox);
            // 
            // FLP_Ball
            // 
            this.FLP_Ball.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_Ball.Controls.Add(this.FLP_BallLeft);
            this.FLP_Ball.Controls.Add(this.CB_Ball);
            this.FLP_Ball.Location = new System.Drawing.Point(0, 42);
            this.FLP_Ball.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_Ball.Name = "FLP_Ball";
            this.FLP_Ball.Size = new System.Drawing.Size(272, 21);
            this.FLP_Ball.TabIndex = 114;
            // 
            // FLP_BallLeft
            // 
            this.FLP_BallLeft.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.FLP_BallLeft.Controls.Add(this.Label_Ball);
            this.FLP_BallLeft.Controls.Add(this.PB_Ball);
            this.FLP_BallLeft.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.FLP_BallLeft.Location = new System.Drawing.Point(0, 0);
            this.FLP_BallLeft.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_BallLeft.Name = "FLP_BallLeft";
            this.FLP_BallLeft.Size = new System.Drawing.Size(110, 21);
            this.FLP_BallLeft.TabIndex = 4;
            // 
            // Label_Ball
            // 
            this.Label_Ball.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Label_Ball.AutoSize = true;
            this.Label_Ball.Location = new System.Drawing.Point(83, 0);
            this.Label_Ball.Margin = new System.Windows.Forms.Padding(0);
            this.Label_Ball.Name = "Label_Ball";
            this.Label_Ball.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.Label_Ball.Size = new System.Drawing.Size(27, 19);
            this.Label_Ball.TabIndex = 2;
            this.Label_Ball.Text = "Ball:";
            this.Label_Ball.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PB_Ball
            // 
            this.PB_Ball.Location = new System.Drawing.Point(60, 0);
            this.PB_Ball.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.PB_Ball.Name = "PB_Ball";
            this.PB_Ball.Size = new System.Drawing.Size(20, 20);
            this.PB_Ball.TabIndex = 3;
            this.PB_Ball.TabStop = false;
            // 
            // CB_Ball
            // 
            this.CB_Ball.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CB_Ball.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CB_Ball.FormattingEnabled = true;
            this.CB_Ball.Location = new System.Drawing.Point(110, 0);
            this.CB_Ball.Margin = new System.Windows.Forms.Padding(0);
            this.CB_Ball.Name = "CB_Ball";
            this.CB_Ball.Size = new System.Drawing.Size(126, 21);
            this.CB_Ball.TabIndex = 3;
            this.CB_Ball.SelectedIndexChanged += new System.EventHandler(this.validateComboBox2);
            this.CB_Ball.KeyDown += new System.Windows.Forms.KeyEventHandler(this.removedropCB);
            // 
            // FLP_MetLevel
            // 
            this.FLP_MetLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_MetLevel.Controls.Add(this.Label_MetLevel);
            this.FLP_MetLevel.Controls.Add(this.TB_MetLevel);
            this.FLP_MetLevel.Location = new System.Drawing.Point(0, 63);
            this.FLP_MetLevel.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_MetLevel.Name = "FLP_MetLevel";
            this.FLP_MetLevel.Size = new System.Drawing.Size(272, 21);
            this.FLP_MetLevel.TabIndex = 115;
            // 
            // Label_MetLevel
            // 
            this.Label_MetLevel.Location = new System.Drawing.Point(0, 0);
            this.Label_MetLevel.Margin = new System.Windows.Forms.Padding(0);
            this.Label_MetLevel.Name = "Label_MetLevel";
            this.Label_MetLevel.Size = new System.Drawing.Size(110, 21);
            this.Label_MetLevel.TabIndex = 3;
            this.Label_MetLevel.Text = "Met Level:";
            this.Label_MetLevel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TB_MetLevel
            // 
            this.TB_MetLevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_MetLevel.Location = new System.Drawing.Point(110, 0);
            this.TB_MetLevel.Margin = new System.Windows.Forms.Padding(0);
            this.TB_MetLevel.Mask = "000";
            this.TB_MetLevel.Name = "TB_MetLevel";
            this.TB_MetLevel.Size = new System.Drawing.Size(126, 20);
            this.TB_MetLevel.TabIndex = 4;
            this.TB_MetLevel.TextChanged += new System.EventHandler(this.update255_MTB);
            // 
            // FLP_MetDate
            // 
            this.FLP_MetDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_MetDate.Controls.Add(this.Label_MetDate);
            this.FLP_MetDate.Controls.Add(this.CAL_MetDate);
            this.FLP_MetDate.Location = new System.Drawing.Point(0, 84);
            this.FLP_MetDate.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_MetDate.Name = "FLP_MetDate";
            this.FLP_MetDate.Size = new System.Drawing.Size(272, 21);
            this.FLP_MetDate.TabIndex = 116;
            // 
            // Label_MetDate
            // 
            this.Label_MetDate.Location = new System.Drawing.Point(0, 0);
            this.Label_MetDate.Margin = new System.Windows.Forms.Padding(0);
            this.Label_MetDate.Name = "Label_MetDate";
            this.Label_MetDate.Size = new System.Drawing.Size(110, 21);
            this.Label_MetDate.TabIndex = 4;
            this.Label_MetDate.Text = "Met Date:";
            this.Label_MetDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CAL_MetDate
            // 
            this.CAL_MetDate.CustomFormat = "MM/dd/yyyy";
            this.CAL_MetDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.CAL_MetDate.Location = new System.Drawing.Point(110, 0);
            this.CAL_MetDate.Margin = new System.Windows.Forms.Padding(0);
            this.CAL_MetDate.MaxDate = new System.DateTime(2099, 12, 31, 0, 0, 0, 0);
            this.CAL_MetDate.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.CAL_MetDate.Name = "CAL_MetDate";
            this.CAL_MetDate.Size = new System.Drawing.Size(126, 20);
            this.CAL_MetDate.TabIndex = 5;
            this.CAL_MetDate.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            // 
            // FLP_Fateful
            // 
            this.FLP_Fateful.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_Fateful.Controls.Add(this.PAN_Fateful);
            this.FLP_Fateful.Controls.Add(this.CHK_Fateful);
            this.FLP_Fateful.Location = new System.Drawing.Point(0, 105);
            this.FLP_Fateful.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_Fateful.Name = "FLP_Fateful";
            this.FLP_Fateful.Size = new System.Drawing.Size(272, 21);
            this.FLP_Fateful.TabIndex = 117;
            // 
            // PAN_Fateful
            // 
            this.PAN_Fateful.Location = new System.Drawing.Point(0, 0);
            this.PAN_Fateful.Margin = new System.Windows.Forms.Padding(0);
            this.PAN_Fateful.Name = "PAN_Fateful";
            this.PAN_Fateful.Size = new System.Drawing.Size(110, 21);
            this.PAN_Fateful.TabIndex = 104;
            // 
            // CHK_Fateful
            // 
            this.CHK_Fateful.AutoSize = true;
            this.CHK_Fateful.Location = new System.Drawing.Point(110, 3);
            this.CHK_Fateful.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.CHK_Fateful.Name = "CHK_Fateful";
            this.CHK_Fateful.Size = new System.Drawing.Size(110, 17);
            this.CHK_Fateful.TabIndex = 6;
            this.CHK_Fateful.Text = "Fateful Encounter";
            this.CHK_Fateful.UseVisualStyleBackColor = true;
            // 
            // FLP_EncounterType
            // 
            this.FLP_EncounterType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_EncounterType.Controls.Add(this.Label_EncounterType);
            this.FLP_EncounterType.Controls.Add(this.CB_EncounterType);
            this.FLP_EncounterType.Location = new System.Drawing.Point(0, 126);
            this.FLP_EncounterType.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_EncounterType.Name = "FLP_EncounterType";
            this.FLP_EncounterType.Size = new System.Drawing.Size(272, 21);
            this.FLP_EncounterType.TabIndex = 118;
            // 
            // Label_EncounterType
            // 
            this.Label_EncounterType.Location = new System.Drawing.Point(0, 0);
            this.Label_EncounterType.Margin = new System.Windows.Forms.Padding(0);
            this.Label_EncounterType.Name = "Label_EncounterType";
            this.Label_EncounterType.Size = new System.Drawing.Size(110, 21);
            this.Label_EncounterType.TabIndex = 5;
            this.Label_EncounterType.Text = "Encounter:";
            this.Label_EncounterType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CB_EncounterType
            // 
            this.CB_EncounterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_EncounterType.DropDownWidth = 160;
            this.CB_EncounterType.FormattingEnabled = true;
            this.CB_EncounterType.Location = new System.Drawing.Point(110, 0);
            this.CB_EncounterType.Margin = new System.Windows.Forms.Padding(0);
            this.CB_EncounterType.Name = "CB_EncounterType";
            this.CB_EncounterType.Size = new System.Drawing.Size(126, 21);
            this.CB_EncounterType.TabIndex = 7;
            // 
            // FLP_TimeOfDay
            // 
            this.FLP_TimeOfDay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_TimeOfDay.Controls.Add(this.L_MetTimeOfDay);
            this.FLP_TimeOfDay.Controls.Add(this.CB_MetTimeOfDay);
            this.FLP_TimeOfDay.Location = new System.Drawing.Point(0, 147);
            this.FLP_TimeOfDay.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_TimeOfDay.Name = "FLP_TimeOfDay";
            this.FLP_TimeOfDay.Size = new System.Drawing.Size(272, 21);
            this.FLP_TimeOfDay.TabIndex = 119;
            // 
            // L_MetTimeOfDay
            // 
            this.L_MetTimeOfDay.Location = new System.Drawing.Point(0, 0);
            this.L_MetTimeOfDay.Margin = new System.Windows.Forms.Padding(0);
            this.L_MetTimeOfDay.Name = "L_MetTimeOfDay";
            this.L_MetTimeOfDay.Size = new System.Drawing.Size(110, 21);
            this.L_MetTimeOfDay.TabIndex = 10;
            this.L_MetTimeOfDay.Text = "Time of Day:";
            this.L_MetTimeOfDay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.L_MetTimeOfDay.Visible = false;
            // 
            // CB_MetTimeOfDay
            // 
            this.CB_MetTimeOfDay.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CB_MetTimeOfDay.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CB_MetTimeOfDay.DropDownWidth = 150;
            this.CB_MetTimeOfDay.FormattingEnabled = true;
            this.CB_MetTimeOfDay.Items.AddRange(new object[] {
            "(None)",
            "Morning",
            "Day",
            "Night"});
            this.CB_MetTimeOfDay.Location = new System.Drawing.Point(110, 0);
            this.CB_MetTimeOfDay.Margin = new System.Windows.Forms.Padding(0);
            this.CB_MetTimeOfDay.Name = "CB_MetTimeOfDay";
            this.CB_MetTimeOfDay.Size = new System.Drawing.Size(126, 21);
            this.CB_MetTimeOfDay.TabIndex = 11;
            this.CB_MetTimeOfDay.Visible = false;
            // 
            // Tab_Stats
            // 
            this.Tab_Stats.AllowDrop = true;
            this.Tab_Stats.Controls.Add(this.PAN_Contest);
            this.Tab_Stats.Controls.Add(this.FLP_Stats);
            this.Tab_Stats.Controls.Add(this.BTN_RandomEVs);
            this.Tab_Stats.Controls.Add(this.BTN_RandomIVs);
            this.Tab_Stats.Location = new System.Drawing.Point(4, 22);
            this.Tab_Stats.Name = "Tab_Stats";
            this.Tab_Stats.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_Stats.Size = new System.Drawing.Size(272, 302);
            this.Tab_Stats.TabIndex = 2;
            this.Tab_Stats.Text = "Stats";
            this.Tab_Stats.UseVisualStyleBackColor = true;
            // 
            // PAN_Contest
            // 
            this.PAN_Contest.Controls.Add(this.TB_Sheen);
            this.PAN_Contest.Controls.Add(this.TB_Tough);
            this.PAN_Contest.Controls.Add(this.TB_Smart);
            this.PAN_Contest.Controls.Add(this.TB_Cute);
            this.PAN_Contest.Controls.Add(this.TB_Beauty);
            this.PAN_Contest.Controls.Add(this.TB_Cool);
            this.PAN_Contest.Controls.Add(this.Label_Sheen);
            this.PAN_Contest.Controls.Add(this.Label_Tough);
            this.PAN_Contest.Controls.Add(this.Label_Smart);
            this.PAN_Contest.Controls.Add(this.Label_Cute);
            this.PAN_Contest.Controls.Add(this.Label_Beauty);
            this.PAN_Contest.Controls.Add(this.Label_Cool);
            this.PAN_Contest.Controls.Add(this.Label_ContestStats);
            this.PAN_Contest.Location = new System.Drawing.Point(21, 247);
            this.PAN_Contest.Name = "PAN_Contest";
            this.PAN_Contest.Size = new System.Drawing.Size(230, 50);
            this.PAN_Contest.TabIndex = 104;
            // 
            // TB_Sheen
            // 
            this.TB_Sheen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_Sheen.Location = new System.Drawing.Point(192, 30);
            this.TB_Sheen.Mask = "000";
            this.TB_Sheen.Name = "TB_Sheen";
            this.TB_Sheen.Size = new System.Drawing.Size(31, 20);
            this.TB_Sheen.TabIndex = 45;
            this.TB_Sheen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TB_Tough
            // 
            this.TB_Tough.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_Tough.Location = new System.Drawing.Point(155, 30);
            this.TB_Tough.Mask = "000";
            this.TB_Tough.Name = "TB_Tough";
            this.TB_Tough.Size = new System.Drawing.Size(31, 20);
            this.TB_Tough.TabIndex = 44;
            this.TB_Tough.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TB_Smart
            // 
            this.TB_Smart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_Smart.Location = new System.Drawing.Point(118, 30);
            this.TB_Smart.Mask = "000";
            this.TB_Smart.Name = "TB_Smart";
            this.TB_Smart.Size = new System.Drawing.Size(31, 20);
            this.TB_Smart.TabIndex = 43;
            this.TB_Smart.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TB_Cute
            // 
            this.TB_Cute.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_Cute.Location = new System.Drawing.Point(81, 30);
            this.TB_Cute.Mask = "000";
            this.TB_Cute.Name = "TB_Cute";
            this.TB_Cute.Size = new System.Drawing.Size(31, 20);
            this.TB_Cute.TabIndex = 42;
            this.TB_Cute.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TB_Beauty
            // 
            this.TB_Beauty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_Beauty.Location = new System.Drawing.Point(44, 30);
            this.TB_Beauty.Mask = "000";
            this.TB_Beauty.Name = "TB_Beauty";
            this.TB_Beauty.Size = new System.Drawing.Size(31, 20);
            this.TB_Beauty.TabIndex = 41;
            this.TB_Beauty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TB_Cool
            // 
            this.TB_Cool.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_Cool.Location = new System.Drawing.Point(7, 30);
            this.TB_Cool.Mask = "000";
            this.TB_Cool.Name = "TB_Cool";
            this.TB_Cool.Size = new System.Drawing.Size(31, 20);
            this.TB_Cool.TabIndex = 40;
            this.TB_Cool.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Label_Sheen
            // 
            this.Label_Sheen.Location = new System.Drawing.Point(186, 17);
            this.Label_Sheen.Name = "Label_Sheen";
            this.Label_Sheen.Size = new System.Drawing.Size(43, 13);
            this.Label_Sheen.TabIndex = 52;
            this.Label_Sheen.Text = "Sheen";
            this.Label_Sheen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_Tough
            // 
            this.Label_Tough.Location = new System.Drawing.Point(149, 17);
            this.Label_Tough.Name = "Label_Tough";
            this.Label_Tough.Size = new System.Drawing.Size(43, 13);
            this.Label_Tough.TabIndex = 51;
            this.Label_Tough.Text = "Tough";
            this.Label_Tough.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_Smart
            // 
            this.Label_Smart.Location = new System.Drawing.Point(112, 17);
            this.Label_Smart.Name = "Label_Smart";
            this.Label_Smart.Size = new System.Drawing.Size(43, 13);
            this.Label_Smart.TabIndex = 50;
            this.Label_Smart.Text = "Clever";
            this.Label_Smart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_Cute
            // 
            this.Label_Cute.Location = new System.Drawing.Point(75, 17);
            this.Label_Cute.Name = "Label_Cute";
            this.Label_Cute.Size = new System.Drawing.Size(43, 13);
            this.Label_Cute.TabIndex = 49;
            this.Label_Cute.Text = "Cute";
            this.Label_Cute.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_Beauty
            // 
            this.Label_Beauty.Location = new System.Drawing.Point(38, 17);
            this.Label_Beauty.Name = "Label_Beauty";
            this.Label_Beauty.Size = new System.Drawing.Size(43, 13);
            this.Label_Beauty.TabIndex = 48;
            this.Label_Beauty.Text = "Beauty";
            this.Label_Beauty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_Cool
            // 
            this.Label_Cool.Location = new System.Drawing.Point(1, 17);
            this.Label_Cool.Name = "Label_Cool";
            this.Label_Cool.Size = new System.Drawing.Size(43, 13);
            this.Label_Cool.TabIndex = 47;
            this.Label_Cool.Text = "Cool";
            this.Label_Cool.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_ContestStats
            // 
            this.Label_ContestStats.Location = new System.Drawing.Point(46, 1);
            this.Label_ContestStats.Name = "Label_ContestStats";
            this.Label_ContestStats.Size = new System.Drawing.Size(140, 13);
            this.Label_ContestStats.TabIndex = 46;
            this.Label_ContestStats.Text = "Contest Stats";
            this.Label_ContestStats.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FLP_Stats
            // 
            this.FLP_Stats.Controls.Add(this.FLP_StatHeader);
            this.FLP_Stats.Controls.Add(this.FLP_HP);
            this.FLP_Stats.Controls.Add(this.FLP_Atk);
            this.FLP_Stats.Controls.Add(this.FLP_Def);
            this.FLP_Stats.Controls.Add(this.FLP_SpA);
            this.FLP_Stats.Controls.Add(this.FLP_SpD);
            this.FLP_Stats.Controls.Add(this.FLP_Spe);
            this.FLP_Stats.Controls.Add(this.FLP_StatsTotal);
            this.FLP_Stats.Controls.Add(this.FLP_HPType);
            this.FLP_Stats.Controls.Add(this.FLP_Characteristic);
            this.FLP_Stats.Location = new System.Drawing.Point(0, 2);
            this.FLP_Stats.Name = "FLP_Stats";
            this.FLP_Stats.Size = new System.Drawing.Size(272, 206);
            this.FLP_Stats.TabIndex = 103;
            // 
            // FLP_StatHeader
            // 
            this.FLP_StatHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_StatHeader.Controls.Add(this.FLP_HackedStats);
            this.FLP_StatHeader.Controls.Add(this.FLP_StatsHeaderRight);
            this.FLP_StatHeader.Location = new System.Drawing.Point(0, 0);
            this.FLP_StatHeader.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_StatHeader.Name = "FLP_StatHeader";
            this.FLP_StatHeader.Size = new System.Drawing.Size(272, 22);
            this.FLP_StatHeader.TabIndex = 122;
            // 
            // FLP_HackedStats
            // 
            this.FLP_HackedStats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_HackedStats.Location = new System.Drawing.Point(0, 0);
            this.FLP_HackedStats.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_HackedStats.Name = "FLP_HackedStats";
            this.FLP_HackedStats.Size = new System.Drawing.Size(107, 21);
            this.FLP_HackedStats.TabIndex = 122;
            // 
            // FLP_StatsHeaderRight
            // 
            this.FLP_StatsHeaderRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_StatsHeaderRight.Controls.Add(this.Label_IVs);
            this.FLP_StatsHeaderRight.Controls.Add(this.Label_EVs);
            this.FLP_StatsHeaderRight.Controls.Add(this.Label_Stats);
            this.FLP_StatsHeaderRight.Location = new System.Drawing.Point(107, 0);
            this.FLP_StatsHeaderRight.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_StatsHeaderRight.Name = "FLP_StatsHeaderRight";
            this.FLP_StatsHeaderRight.Size = new System.Drawing.Size(162, 21);
            this.FLP_StatsHeaderRight.TabIndex = 123;
            // 
            // Label_IVs
            // 
            this.Label_IVs.Location = new System.Drawing.Point(0, 0);
            this.Label_IVs.Margin = new System.Windows.Forms.Padding(0);
            this.Label_IVs.Name = "Label_IVs";
            this.Label_IVs.Size = new System.Drawing.Size(30, 21);
            this.Label_IVs.TabIndex = 29;
            this.Label_IVs.Text = "IVs";
            this.Label_IVs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_EVs
            // 
            this.Label_EVs.Location = new System.Drawing.Point(30, 0);
            this.Label_EVs.Margin = new System.Windows.Forms.Padding(0);
            this.Label_EVs.Name = "Label_EVs";
            this.Label_EVs.Size = new System.Drawing.Size(35, 21);
            this.Label_EVs.TabIndex = 27;
            this.Label_EVs.Text = "EVs";
            this.Label_EVs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_Stats
            // 
            this.Label_Stats.Location = new System.Drawing.Point(65, 0);
            this.Label_Stats.Margin = new System.Windows.Forms.Padding(0);
            this.Label_Stats.Name = "Label_Stats";
            this.Label_Stats.Size = new System.Drawing.Size(35, 21);
            this.Label_Stats.TabIndex = 28;
            this.Label_Stats.Text = "Stats";
            this.Label_Stats.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FLP_HP
            // 
            this.FLP_HP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_HP.Controls.Add(this.Label_HP);
            this.FLP_HP.Controls.Add(this.FLP_HPRight);
            this.FLP_HP.Location = new System.Drawing.Point(0, 22);
            this.FLP_HP.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_HP.Name = "FLP_HP";
            this.FLP_HP.Size = new System.Drawing.Size(272, 21);
            this.FLP_HP.TabIndex = 123;
            // 
            // Label_HP
            // 
            this.Label_HP.Location = new System.Drawing.Point(0, 0);
            this.Label_HP.Margin = new System.Windows.Forms.Padding(0);
            this.Label_HP.Name = "Label_HP";
            this.Label_HP.Size = new System.Drawing.Size(110, 21);
            this.Label_HP.TabIndex = 19;
            this.Label_HP.Text = "HP:";
            this.Label_HP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Label_HP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.clickStatLabel);
            // 
            // FLP_HPRight
            // 
            this.FLP_HPRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_HPRight.Controls.Add(this.TB_HPIV);
            this.FLP_HPRight.Controls.Add(this.TB_HPEV);
            this.FLP_HPRight.Controls.Add(this.Stat_HP);
            this.FLP_HPRight.Location = new System.Drawing.Point(110, 0);
            this.FLP_HPRight.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_HPRight.Name = "FLP_HPRight";
            this.FLP_HPRight.Size = new System.Drawing.Size(162, 21);
            this.FLP_HPRight.TabIndex = 121;
            // 
            // TB_HPIV
            // 
            this.TB_HPIV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_HPIV.Location = new System.Drawing.Point(0, 0);
            this.TB_HPIV.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.TB_HPIV.Mask = "00";
            this.TB_HPIV.Name = "TB_HPIV";
            this.TB_HPIV.Size = new System.Drawing.Size(22, 20);
            this.TB_HPIV.TabIndex = 1;
            this.TB_HPIV.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_HPIV.Click += new System.EventHandler(this.clickIV);
            this.TB_HPIV.TextChanged += new System.EventHandler(this.updateIVs);
            // 
            // TB_HPEV
            // 
            this.TB_HPEV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_HPEV.Location = new System.Drawing.Point(28, 0);
            this.TB_HPEV.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.TB_HPEV.Mask = "000";
            this.TB_HPEV.Name = "TB_HPEV";
            this.TB_HPEV.Size = new System.Drawing.Size(28, 20);
            this.TB_HPEV.TabIndex = 7;
            this.TB_HPEV.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_HPEV.Click += new System.EventHandler(this.clickEV);
            this.TB_HPEV.TextChanged += new System.EventHandler(this.updateEVs);
            // 
            // Stat_HP
            // 
            this.Stat_HP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Stat_HP.Enabled = false;
            this.Stat_HP.Location = new System.Drawing.Point(62, 0);
            this.Stat_HP.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.Stat_HP.Mask = "00000";
            this.Stat_HP.Name = "Stat_HP";
            this.Stat_HP.PromptChar = ' ';
            this.Stat_HP.Size = new System.Drawing.Size(37, 20);
            this.Stat_HP.TabIndex = 45;
            this.Stat_HP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FLP_Atk
            // 
            this.FLP_Atk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_Atk.Controls.Add(this.Label_ATK);
            this.FLP_Atk.Controls.Add(this.FLP_AtkRight);
            this.FLP_Atk.Location = new System.Drawing.Point(0, 43);
            this.FLP_Atk.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_Atk.Name = "FLP_Atk";
            this.FLP_Atk.Size = new System.Drawing.Size(272, 21);
            this.FLP_Atk.TabIndex = 124;
            // 
            // Label_ATK
            // 
            this.Label_ATK.Location = new System.Drawing.Point(0, 0);
            this.Label_ATK.Margin = new System.Windows.Forms.Padding(0);
            this.Label_ATK.Name = "Label_ATK";
            this.Label_ATK.Size = new System.Drawing.Size(110, 21);
            this.Label_ATK.TabIndex = 20;
            this.Label_ATK.Text = "Atk:";
            this.Label_ATK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Label_ATK.MouseDown += new System.Windows.Forms.MouseEventHandler(this.clickStatLabel);
            // 
            // FLP_AtkRight
            // 
            this.FLP_AtkRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_AtkRight.Controls.Add(this.TB_ATKIV);
            this.FLP_AtkRight.Controls.Add(this.TB_ATKEV);
            this.FLP_AtkRight.Controls.Add(this.Stat_ATK);
            this.FLP_AtkRight.Location = new System.Drawing.Point(110, 0);
            this.FLP_AtkRight.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_AtkRight.Name = "FLP_AtkRight";
            this.FLP_AtkRight.Size = new System.Drawing.Size(162, 21);
            this.FLP_AtkRight.TabIndex = 123;
            // 
            // TB_ATKIV
            // 
            this.TB_ATKIV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_ATKIV.Location = new System.Drawing.Point(0, 0);
            this.TB_ATKIV.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.TB_ATKIV.Mask = "00";
            this.TB_ATKIV.Name = "TB_ATKIV";
            this.TB_ATKIV.Size = new System.Drawing.Size(22, 20);
            this.TB_ATKIV.TabIndex = 2;
            this.TB_ATKIV.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_ATKIV.Click += new System.EventHandler(this.clickIV);
            this.TB_ATKIV.TextChanged += new System.EventHandler(this.updateIVs);
            // 
            // TB_ATKEV
            // 
            this.TB_ATKEV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_ATKEV.Location = new System.Drawing.Point(28, 0);
            this.TB_ATKEV.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.TB_ATKEV.Mask = "000";
            this.TB_ATKEV.Name = "TB_ATKEV";
            this.TB_ATKEV.Size = new System.Drawing.Size(28, 20);
            this.TB_ATKEV.TabIndex = 8;
            this.TB_ATKEV.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_ATKEV.Click += new System.EventHandler(this.clickEV);
            this.TB_ATKEV.TextChanged += new System.EventHandler(this.updateEVs);
            // 
            // Stat_ATK
            // 
            this.Stat_ATK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Stat_ATK.Enabled = false;
            this.Stat_ATK.Location = new System.Drawing.Point(62, 0);
            this.Stat_ATK.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.Stat_ATK.Mask = "00000";
            this.Stat_ATK.Name = "Stat_ATK";
            this.Stat_ATK.PromptChar = ' ';
            this.Stat_ATK.Size = new System.Drawing.Size(37, 20);
            this.Stat_ATK.TabIndex = 46;
            this.Stat_ATK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FLP_Def
            // 
            this.FLP_Def.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_Def.Controls.Add(this.Label_DEF);
            this.FLP_Def.Controls.Add(this.FLP_DefRight);
            this.FLP_Def.Location = new System.Drawing.Point(0, 64);
            this.FLP_Def.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_Def.Name = "FLP_Def";
            this.FLP_Def.Size = new System.Drawing.Size(272, 21);
            this.FLP_Def.TabIndex = 125;
            // 
            // Label_DEF
            // 
            this.Label_DEF.Location = new System.Drawing.Point(0, 0);
            this.Label_DEF.Margin = new System.Windows.Forms.Padding(0);
            this.Label_DEF.Name = "Label_DEF";
            this.Label_DEF.Size = new System.Drawing.Size(110, 21);
            this.Label_DEF.TabIndex = 21;
            this.Label_DEF.Text = "Def:";
            this.Label_DEF.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Label_DEF.MouseDown += new System.Windows.Forms.MouseEventHandler(this.clickStatLabel);
            // 
            // FLP_DefRight
            // 
            this.FLP_DefRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_DefRight.Controls.Add(this.TB_DEFIV);
            this.FLP_DefRight.Controls.Add(this.TB_DEFEV);
            this.FLP_DefRight.Controls.Add(this.Stat_DEF);
            this.FLP_DefRight.Location = new System.Drawing.Point(110, 0);
            this.FLP_DefRight.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_DefRight.Name = "FLP_DefRight";
            this.FLP_DefRight.Size = new System.Drawing.Size(162, 21);
            this.FLP_DefRight.TabIndex = 123;
            // 
            // TB_DEFIV
            // 
            this.TB_DEFIV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_DEFIV.Location = new System.Drawing.Point(0, 0);
            this.TB_DEFIV.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.TB_DEFIV.Mask = "00";
            this.TB_DEFIV.Name = "TB_DEFIV";
            this.TB_DEFIV.Size = new System.Drawing.Size(22, 20);
            this.TB_DEFIV.TabIndex = 3;
            this.TB_DEFIV.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_DEFIV.Click += new System.EventHandler(this.clickIV);
            this.TB_DEFIV.TextChanged += new System.EventHandler(this.updateIVs);
            // 
            // TB_DEFEV
            // 
            this.TB_DEFEV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_DEFEV.Location = new System.Drawing.Point(28, 0);
            this.TB_DEFEV.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.TB_DEFEV.Mask = "000";
            this.TB_DEFEV.Name = "TB_DEFEV";
            this.TB_DEFEV.Size = new System.Drawing.Size(28, 20);
            this.TB_DEFEV.TabIndex = 9;
            this.TB_DEFEV.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_DEFEV.Click += new System.EventHandler(this.clickEV);
            this.TB_DEFEV.TextChanged += new System.EventHandler(this.updateEVs);
            // 
            // Stat_DEF
            // 
            this.Stat_DEF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Stat_DEF.Enabled = false;
            this.Stat_DEF.Location = new System.Drawing.Point(62, 0);
            this.Stat_DEF.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.Stat_DEF.Mask = "00000";
            this.Stat_DEF.Name = "Stat_DEF";
            this.Stat_DEF.PromptChar = ' ';
            this.Stat_DEF.Size = new System.Drawing.Size(37, 20);
            this.Stat_DEF.TabIndex = 47;
            this.Stat_DEF.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FLP_SpA
            // 
            this.FLP_SpA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_SpA.Controls.Add(this.FLP_SpALeft);
            this.FLP_SpA.Controls.Add(this.FLP_SpARight);
            this.FLP_SpA.Location = new System.Drawing.Point(0, 85);
            this.FLP_SpA.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_SpA.Name = "FLP_SpA";
            this.FLP_SpA.Size = new System.Drawing.Size(272, 21);
            this.FLP_SpA.TabIndex = 126;
            // 
            // FLP_SpALeft
            // 
            this.FLP_SpALeft.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.FLP_SpALeft.Controls.Add(this.Label_SPA);
            this.FLP_SpALeft.Controls.Add(this.Label_SPC);
            this.FLP_SpALeft.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.FLP_SpALeft.Location = new System.Drawing.Point(0, 0);
            this.FLP_SpALeft.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_SpALeft.Name = "FLP_SpALeft";
            this.FLP_SpALeft.Size = new System.Drawing.Size(110, 21);
            this.FLP_SpALeft.TabIndex = 124;
            // 
            // Label_SPA
            // 
            this.Label_SPA.Location = new System.Drawing.Point(0, 0);
            this.Label_SPA.Margin = new System.Windows.Forms.Padding(0);
            this.Label_SPA.Name = "Label_SPA";
            this.Label_SPA.Size = new System.Drawing.Size(110, 21);
            this.Label_SPA.TabIndex = 22;
            this.Label_SPA.Text = "SpA:";
            this.Label_SPA.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Label_SPA.MouseDown += new System.Windows.Forms.MouseEventHandler(this.clickStatLabel);
            // 
            // Label_SPC
            // 
            this.Label_SPC.Location = new System.Drawing.Point(0, 21);
            this.Label_SPC.Margin = new System.Windows.Forms.Padding(0);
            this.Label_SPC.Name = "Label_SPC";
            this.Label_SPC.Size = new System.Drawing.Size(110, 21);
            this.Label_SPC.TabIndex = 125;
            this.Label_SPC.Text = "SpC:";
            this.Label_SPC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FLP_SpARight
            // 
            this.FLP_SpARight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_SpARight.Controls.Add(this.TB_SPAIV);
            this.FLP_SpARight.Controls.Add(this.TB_SPAEV);
            this.FLP_SpARight.Controls.Add(this.Stat_SPA);
            this.FLP_SpARight.Location = new System.Drawing.Point(110, 0);
            this.FLP_SpARight.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_SpARight.Name = "FLP_SpARight";
            this.FLP_SpARight.Size = new System.Drawing.Size(162, 21);
            this.FLP_SpARight.TabIndex = 123;
            // 
            // TB_SPAIV
            // 
            this.TB_SPAIV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_SPAIV.Location = new System.Drawing.Point(0, 0);
            this.TB_SPAIV.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.TB_SPAIV.Mask = "00";
            this.TB_SPAIV.Name = "TB_SPAIV";
            this.TB_SPAIV.Size = new System.Drawing.Size(22, 20);
            this.TB_SPAIV.TabIndex = 4;
            this.TB_SPAIV.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_SPAIV.Click += new System.EventHandler(this.clickIV);
            this.TB_SPAIV.TextChanged += new System.EventHandler(this.updateIVs);
            // 
            // TB_SPAEV
            // 
            this.TB_SPAEV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_SPAEV.Location = new System.Drawing.Point(28, 0);
            this.TB_SPAEV.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.TB_SPAEV.Mask = "000";
            this.TB_SPAEV.Name = "TB_SPAEV";
            this.TB_SPAEV.Size = new System.Drawing.Size(28, 20);
            this.TB_SPAEV.TabIndex = 10;
            this.TB_SPAEV.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_SPAEV.Click += new System.EventHandler(this.clickEV);
            this.TB_SPAEV.TextChanged += new System.EventHandler(this.updateEVs);
            // 
            // Stat_SPA
            // 
            this.Stat_SPA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Stat_SPA.Enabled = false;
            this.Stat_SPA.Location = new System.Drawing.Point(62, 0);
            this.Stat_SPA.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.Stat_SPA.Mask = "00000";
            this.Stat_SPA.Name = "Stat_SPA";
            this.Stat_SPA.PromptChar = ' ';
            this.Stat_SPA.Size = new System.Drawing.Size(37, 20);
            this.Stat_SPA.TabIndex = 48;
            this.Stat_SPA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FLP_SpD
            // 
            this.FLP_SpD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_SpD.Controls.Add(this.Label_SPD);
            this.FLP_SpD.Controls.Add(this.FLP_SpDRight);
            this.FLP_SpD.Location = new System.Drawing.Point(0, 106);
            this.FLP_SpD.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_SpD.Name = "FLP_SpD";
            this.FLP_SpD.Size = new System.Drawing.Size(272, 21);
            this.FLP_SpD.TabIndex = 127;
            // 
            // Label_SPD
            // 
            this.Label_SPD.Location = new System.Drawing.Point(0, 0);
            this.Label_SPD.Margin = new System.Windows.Forms.Padding(0);
            this.Label_SPD.Name = "Label_SPD";
            this.Label_SPD.Size = new System.Drawing.Size(110, 21);
            this.Label_SPD.TabIndex = 23;
            this.Label_SPD.Text = "SpD:";
            this.Label_SPD.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Label_SPD.MouseDown += new System.Windows.Forms.MouseEventHandler(this.clickStatLabel);
            // 
            // FLP_SpDRight
            // 
            this.FLP_SpDRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_SpDRight.Controls.Add(this.TB_SPDIV);
            this.FLP_SpDRight.Controls.Add(this.TB_SPDEV);
            this.FLP_SpDRight.Controls.Add(this.Stat_SPD);
            this.FLP_SpDRight.Location = new System.Drawing.Point(110, 0);
            this.FLP_SpDRight.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_SpDRight.Name = "FLP_SpDRight";
            this.FLP_SpDRight.Size = new System.Drawing.Size(162, 21);
            this.FLP_SpDRight.TabIndex = 123;
            // 
            // TB_SPDIV
            // 
            this.TB_SPDIV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_SPDIV.Location = new System.Drawing.Point(0, 0);
            this.TB_SPDIV.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.TB_SPDIV.Mask = "00";
            this.TB_SPDIV.Name = "TB_SPDIV";
            this.TB_SPDIV.Size = new System.Drawing.Size(22, 20);
            this.TB_SPDIV.TabIndex = 5;
            this.TB_SPDIV.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_SPDIV.Click += new System.EventHandler(this.clickIV);
            this.TB_SPDIV.TextChanged += new System.EventHandler(this.updateIVs);
            // 
            // TB_SPDEV
            // 
            this.TB_SPDEV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_SPDEV.Location = new System.Drawing.Point(28, 0);
            this.TB_SPDEV.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.TB_SPDEV.Mask = "000";
            this.TB_SPDEV.Name = "TB_SPDEV";
            this.TB_SPDEV.Size = new System.Drawing.Size(28, 20);
            this.TB_SPDEV.TabIndex = 11;
            this.TB_SPDEV.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_SPDEV.Click += new System.EventHandler(this.clickEV);
            this.TB_SPDEV.TextChanged += new System.EventHandler(this.updateEVs);
            // 
            // Stat_SPD
            // 
            this.Stat_SPD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Stat_SPD.Enabled = false;
            this.Stat_SPD.Location = new System.Drawing.Point(62, 0);
            this.Stat_SPD.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.Stat_SPD.Mask = "00000";
            this.Stat_SPD.Name = "Stat_SPD";
            this.Stat_SPD.PromptChar = ' ';
            this.Stat_SPD.Size = new System.Drawing.Size(37, 20);
            this.Stat_SPD.TabIndex = 49;
            this.Stat_SPD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FLP_Spe
            // 
            this.FLP_Spe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_Spe.Controls.Add(this.Label_SPE);
            this.FLP_Spe.Controls.Add(this.FLP_SpeRight);
            this.FLP_Spe.Location = new System.Drawing.Point(0, 127);
            this.FLP_Spe.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_Spe.Name = "FLP_Spe";
            this.FLP_Spe.Size = new System.Drawing.Size(272, 21);
            this.FLP_Spe.TabIndex = 128;
            // 
            // Label_SPE
            // 
            this.Label_SPE.Location = new System.Drawing.Point(0, 0);
            this.Label_SPE.Margin = new System.Windows.Forms.Padding(0);
            this.Label_SPE.Name = "Label_SPE";
            this.Label_SPE.Size = new System.Drawing.Size(110, 21);
            this.Label_SPE.TabIndex = 24;
            this.Label_SPE.Text = "Spe:";
            this.Label_SPE.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Label_SPE.MouseDown += new System.Windows.Forms.MouseEventHandler(this.clickStatLabel);
            // 
            // FLP_SpeRight
            // 
            this.FLP_SpeRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_SpeRight.Controls.Add(this.TB_SPEIV);
            this.FLP_SpeRight.Controls.Add(this.TB_SPEEV);
            this.FLP_SpeRight.Controls.Add(this.Stat_SPE);
            this.FLP_SpeRight.Location = new System.Drawing.Point(110, 0);
            this.FLP_SpeRight.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_SpeRight.Name = "FLP_SpeRight";
            this.FLP_SpeRight.Size = new System.Drawing.Size(162, 21);
            this.FLP_SpeRight.TabIndex = 123;
            // 
            // TB_SPEIV
            // 
            this.TB_SPEIV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_SPEIV.Location = new System.Drawing.Point(0, 0);
            this.TB_SPEIV.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.TB_SPEIV.Mask = "00";
            this.TB_SPEIV.Name = "TB_SPEIV";
            this.TB_SPEIV.Size = new System.Drawing.Size(22, 20);
            this.TB_SPEIV.TabIndex = 6;
            this.TB_SPEIV.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_SPEIV.Click += new System.EventHandler(this.clickIV);
            this.TB_SPEIV.TextChanged += new System.EventHandler(this.updateIVs);
            // 
            // TB_SPEEV
            // 
            this.TB_SPEEV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_SPEEV.Location = new System.Drawing.Point(28, 0);
            this.TB_SPEEV.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.TB_SPEEV.Mask = "000";
            this.TB_SPEEV.Name = "TB_SPEEV";
            this.TB_SPEEV.Size = new System.Drawing.Size(28, 20);
            this.TB_SPEEV.TabIndex = 12;
            this.TB_SPEEV.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_SPEEV.Click += new System.EventHandler(this.clickEV);
            this.TB_SPEEV.TextChanged += new System.EventHandler(this.updateEVs);
            // 
            // Stat_SPE
            // 
            this.Stat_SPE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Stat_SPE.Enabled = false;
            this.Stat_SPE.Location = new System.Drawing.Point(62, 0);
            this.Stat_SPE.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.Stat_SPE.Mask = "00000";
            this.Stat_SPE.Name = "Stat_SPE";
            this.Stat_SPE.PromptChar = ' ';
            this.Stat_SPE.Size = new System.Drawing.Size(37, 20);
            this.Stat_SPE.TabIndex = 50;
            this.Stat_SPE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FLP_StatsTotal
            // 
            this.FLP_StatsTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_StatsTotal.Controls.Add(this.Label_Total);
            this.FLP_StatsTotal.Controls.Add(this.FLP_StatsTotalRight);
            this.FLP_StatsTotal.Location = new System.Drawing.Point(0, 148);
            this.FLP_StatsTotal.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_StatsTotal.Name = "FLP_StatsTotal";
            this.FLP_StatsTotal.Size = new System.Drawing.Size(272, 21);
            this.FLP_StatsTotal.TabIndex = 129;
            // 
            // Label_Total
            // 
            this.Label_Total.Location = new System.Drawing.Point(0, 0);
            this.Label_Total.Margin = new System.Windows.Forms.Padding(0);
            this.Label_Total.Name = "Label_Total";
            this.Label_Total.Size = new System.Drawing.Size(110, 21);
            this.Label_Total.TabIndex = 25;
            this.Label_Total.Text = "Total:";
            this.Label_Total.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FLP_StatsTotalRight
            // 
            this.FLP_StatsTotalRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_StatsTotalRight.Controls.Add(this.TB_IVTotal);
            this.FLP_StatsTotalRight.Controls.Add(this.TB_EVTotal);
            this.FLP_StatsTotalRight.Controls.Add(this.L_Potential);
            this.FLP_StatsTotalRight.Location = new System.Drawing.Point(110, 0);
            this.FLP_StatsTotalRight.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_StatsTotalRight.Name = "FLP_StatsTotalRight";
            this.FLP_StatsTotalRight.Size = new System.Drawing.Size(162, 21);
            this.FLP_StatsTotalRight.TabIndex = 123;
            // 
            // TB_IVTotal
            // 
            this.TB_IVTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_IVTotal.Location = new System.Drawing.Point(0, 0);
            this.TB_IVTotal.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.TB_IVTotal.MaxLength = 3;
            this.TB_IVTotal.Name = "TB_IVTotal";
            this.TB_IVTotal.ReadOnly = true;
            this.TB_IVTotal.Size = new System.Drawing.Size(22, 20);
            this.TB_IVTotal.TabIndex = 41;
            this.TB_IVTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TB_EVTotal
            // 
            this.TB_EVTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_EVTotal.Location = new System.Drawing.Point(28, 0);
            this.TB_EVTotal.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.TB_EVTotal.MaxLength = 3;
            this.TB_EVTotal.Name = "TB_EVTotal";
            this.TB_EVTotal.ReadOnly = true;
            this.TB_EVTotal.Size = new System.Drawing.Size(28, 20);
            this.TB_EVTotal.TabIndex = 18;
            this.TB_EVTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // L_Potential
            // 
            this.L_Potential.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.L_Potential.Location = new System.Drawing.Point(59, 0);
            this.L_Potential.Margin = new System.Windows.Forms.Padding(0);
            this.L_Potential.Name = "L_Potential";
            this.L_Potential.Size = new System.Drawing.Size(67, 21);
            this.L_Potential.TabIndex = 42;
            this.L_Potential.Text = "(potential)";
            this.L_Potential.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FLP_HPType
            // 
            this.FLP_HPType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_HPType.Controls.Add(this.Label_HiddenPowerPrefix);
            this.FLP_HPType.Controls.Add(this.CB_HPType);
            this.FLP_HPType.Location = new System.Drawing.Point(0, 169);
            this.FLP_HPType.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_HPType.Name = "FLP_HPType";
            this.FLP_HPType.Size = new System.Drawing.Size(272, 21);
            this.FLP_HPType.TabIndex = 130;
            // 
            // Label_HiddenPowerPrefix
            // 
            this.Label_HiddenPowerPrefix.Location = new System.Drawing.Point(0, 0);
            this.Label_HiddenPowerPrefix.Margin = new System.Windows.Forms.Padding(0);
            this.Label_HiddenPowerPrefix.Name = "Label_HiddenPowerPrefix";
            this.Label_HiddenPowerPrefix.Size = new System.Drawing.Size(172, 21);
            this.Label_HiddenPowerPrefix.TabIndex = 29;
            this.Label_HiddenPowerPrefix.Text = "Hidden Power Type:";
            this.Label_HiddenPowerPrefix.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CB_HPType
            // 
            this.CB_HPType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CB_HPType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CB_HPType.DropDownWidth = 80;
            this.CB_HPType.FormattingEnabled = true;
            this.CB_HPType.Location = new System.Drawing.Point(172, 0);
            this.CB_HPType.Margin = new System.Windows.Forms.Padding(0);
            this.CB_HPType.Name = "CB_HPType";
            this.CB_HPType.Size = new System.Drawing.Size(70, 21);
            this.CB_HPType.TabIndex = 44;
            this.CB_HPType.SelectedIndexChanged += new System.EventHandler(this.updateHPType);
            // 
            // FLP_Characteristic
            // 
            this.FLP_Characteristic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_Characteristic.Controls.Add(this.Label_CharacteristicPrefix);
            this.FLP_Characteristic.Controls.Add(this.L_Characteristic);
            this.FLP_Characteristic.Location = new System.Drawing.Point(0, 190);
            this.FLP_Characteristic.Margin = new System.Windows.Forms.Padding(0);
            this.FLP_Characteristic.Name = "FLP_Characteristic";
            this.FLP_Characteristic.Size = new System.Drawing.Size(272, 21);
            this.FLP_Characteristic.TabIndex = 131;
            // 
            // Label_CharacteristicPrefix
            // 
            this.Label_CharacteristicPrefix.Location = new System.Drawing.Point(0, 0);
            this.Label_CharacteristicPrefix.Margin = new System.Windows.Forms.Padding(0);
            this.Label_CharacteristicPrefix.Name = "Label_CharacteristicPrefix";
            this.Label_CharacteristicPrefix.Size = new System.Drawing.Size(110, 21);
            this.Label_CharacteristicPrefix.TabIndex = 43;
            this.Label_CharacteristicPrefix.Text = "Characteristic:";
            this.Label_CharacteristicPrefix.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // L_Characteristic
            // 
            this.L_Characteristic.Location = new System.Drawing.Point(110, 0);
            this.L_Characteristic.Margin = new System.Windows.Forms.Padding(0);
            this.L_Characteristic.Name = "L_Characteristic";
            this.L_Characteristic.Size = new System.Drawing.Size(150, 21);
            this.L_Characteristic.TabIndex = 40;
            this.L_Characteristic.Text = "(char)";
            this.L_Characteristic.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BTN_RandomEVs
            // 
            this.BTN_RandomEVs.Location = new System.Drawing.Point(140, 218);
            this.BTN_RandomEVs.Name = "BTN_RandomEVs";
            this.BTN_RandomEVs.Size = new System.Drawing.Size(92, 23);
            this.BTN_RandomEVs.TabIndex = 14;
            this.BTN_RandomEVs.Text = "Randomize EVs";
            this.BTN_RandomEVs.UseVisualStyleBackColor = true;
            this.BTN_RandomEVs.Click += new System.EventHandler(this.updateRandomEVs);
            // 
            // BTN_RandomIVs
            // 
            this.BTN_RandomIVs.Location = new System.Drawing.Point(41, 218);
            this.BTN_RandomIVs.Name = "BTN_RandomIVs";
            this.BTN_RandomIVs.Size = new System.Drawing.Size(92, 23);
            this.BTN_RandomIVs.TabIndex = 13;
            this.BTN_RandomIVs.Text = "Randomize IVs";
            this.BTN_RandomIVs.UseVisualStyleBackColor = true;
            this.BTN_RandomIVs.Click += new System.EventHandler(this.updateRandomIVs);
            // 
            // Tab_Attacks
            // 
            this.Tab_Attacks.AllowDrop = true;
            this.Tab_Attacks.Controls.Add(this.PB_WarnMove4);
            this.Tab_Attacks.Controls.Add(this.PB_WarnMove3);
            this.Tab_Attacks.Controls.Add(this.PB_WarnMove2);
            this.Tab_Attacks.Controls.Add(this.PB_WarnMove1);
            this.Tab_Attacks.Controls.Add(this.GB_RelearnMoves);
            this.Tab_Attacks.Controls.Add(this.GB_CurrentMoves);
            this.Tab_Attacks.Location = new System.Drawing.Point(4, 22);
            this.Tab_Attacks.Name = "Tab_Attacks";
            this.Tab_Attacks.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_Attacks.Size = new System.Drawing.Size(272, 302);
            this.Tab_Attacks.TabIndex = 3;
            this.Tab_Attacks.Text = "Attacks";
            this.Tab_Attacks.UseVisualStyleBackColor = true;
            // 
            // PB_WarnMove4
            // 
            this.PB_WarnMove4.Image = ((System.Drawing.Image)(resources.GetObject("PB_WarnMove4.Image")));
            this.PB_WarnMove4.Location = new System.Drawing.Point(8, 113);
            this.PB_WarnMove4.Name = "PB_WarnMove4";
            this.PB_WarnMove4.Size = new System.Drawing.Size(16, 16);
            this.PB_WarnMove4.TabIndex = 5;
            this.PB_WarnMove4.TabStop = false;
            this.PB_WarnMove4.Visible = false;
            // 
            // PB_WarnMove3
            // 
            this.PB_WarnMove3.Image = ((System.Drawing.Image)(resources.GetObject("PB_WarnMove3.Image")));
            this.PB_WarnMove3.Location = new System.Drawing.Point(8, 91);
            this.PB_WarnMove3.Name = "PB_WarnMove3";
            this.PB_WarnMove3.Size = new System.Drawing.Size(16, 16);
            this.PB_WarnMove3.TabIndex = 4;
            this.PB_WarnMove3.TabStop = false;
            this.PB_WarnMove3.Visible = false;
            // 
            // PB_WarnMove2
            // 
            this.PB_WarnMove2.Image = ((System.Drawing.Image)(resources.GetObject("PB_WarnMove2.Image")));
            this.PB_WarnMove2.Location = new System.Drawing.Point(8, 69);
            this.PB_WarnMove2.Name = "PB_WarnMove2";
            this.PB_WarnMove2.Size = new System.Drawing.Size(16, 16);
            this.PB_WarnMove2.TabIndex = 3;
            this.PB_WarnMove2.TabStop = false;
            this.PB_WarnMove2.Visible = false;
            // 
            // PB_WarnMove1
            // 
            this.PB_WarnMove1.Image = ((System.Drawing.Image)(resources.GetObject("PB_WarnMove1.Image")));
            this.PB_WarnMove1.Location = new System.Drawing.Point(8, 47);
            this.PB_WarnMove1.Name = "PB_WarnMove1";
            this.PB_WarnMove1.Size = new System.Drawing.Size(16, 16);
            this.PB_WarnMove1.TabIndex = 2;
            this.PB_WarnMove1.TabStop = false;
            this.PB_WarnMove1.Visible = false;
            // 
            // GB_RelearnMoves
            // 
            this.GB_RelearnMoves.Controls.Add(this.PB_WarnRelearn4);
            this.GB_RelearnMoves.Controls.Add(this.PB_WarnRelearn3);
            this.GB_RelearnMoves.Controls.Add(this.PB_WarnRelearn2);
            this.GB_RelearnMoves.Controls.Add(this.PB_WarnRelearn1);
            this.GB_RelearnMoves.Controls.Add(this.CB_RelearnMove4);
            this.GB_RelearnMoves.Controls.Add(this.CB_RelearnMove3);
            this.GB_RelearnMoves.Controls.Add(this.CB_RelearnMove2);
            this.GB_RelearnMoves.Controls.Add(this.CB_RelearnMove1);
            this.GB_RelearnMoves.Location = new System.Drawing.Point(25, 160);
            this.GB_RelearnMoves.Name = "GB_RelearnMoves";
            this.GB_RelearnMoves.Size = new System.Drawing.Size(220, 120);
            this.GB_RelearnMoves.TabIndex = 1;
            this.GB_RelearnMoves.TabStop = false;
            this.GB_RelearnMoves.Text = "Relearn Moves";
            // 
            // PB_WarnRelearn4
            // 
            this.PB_WarnRelearn4.Image = ((System.Drawing.Image)(resources.GetObject("PB_WarnRelearn4.Image")));
            this.PB_WarnRelearn4.Location = new System.Drawing.Point(22, 93);
            this.PB_WarnRelearn4.Name = "PB_WarnRelearn4";
            this.PB_WarnRelearn4.Size = new System.Drawing.Size(16, 16);
            this.PB_WarnRelearn4.TabIndex = 19;
            this.PB_WarnRelearn4.TabStop = false;
            this.PB_WarnRelearn4.Visible = false;
            // 
            // PB_WarnRelearn3
            // 
            this.PB_WarnRelearn3.Image = ((System.Drawing.Image)(resources.GetObject("PB_WarnRelearn3.Image")));
            this.PB_WarnRelearn3.Location = new System.Drawing.Point(22, 71);
            this.PB_WarnRelearn3.Name = "PB_WarnRelearn3";
            this.PB_WarnRelearn3.Size = new System.Drawing.Size(16, 16);
            this.PB_WarnRelearn3.TabIndex = 18;
            this.PB_WarnRelearn3.TabStop = false;
            this.PB_WarnRelearn3.Visible = false;
            // 
            // PB_WarnRelearn2
            // 
            this.PB_WarnRelearn2.Image = ((System.Drawing.Image)(resources.GetObject("PB_WarnRelearn2.Image")));
            this.PB_WarnRelearn2.Location = new System.Drawing.Point(22, 49);
            this.PB_WarnRelearn2.Name = "PB_WarnRelearn2";
            this.PB_WarnRelearn2.Size = new System.Drawing.Size(16, 16);
            this.PB_WarnRelearn2.TabIndex = 17;
            this.PB_WarnRelearn2.TabStop = false;
            this.PB_WarnRelearn2.Visible = false;
            // 
            // PB_WarnRelearn1
            // 
            this.PB_WarnRelearn1.Image = ((System.Drawing.Image)(resources.GetObject("PB_WarnRelearn1.Image")));
            this.PB_WarnRelearn1.Location = new System.Drawing.Point(22, 27);
            this.PB_WarnRelearn1.Name = "PB_WarnRelearn1";
            this.PB_WarnRelearn1.Size = new System.Drawing.Size(16, 16);
            this.PB_WarnRelearn1.TabIndex = 6;
            this.PB_WarnRelearn1.TabStop = false;
            this.PB_WarnRelearn1.Visible = false;
            // 
            // CB_RelearnMove4
            // 
            this.CB_RelearnMove4.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CB_RelearnMove4.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CB_RelearnMove4.FormattingEnabled = true;
            this.CB_RelearnMove4.Location = new System.Drawing.Point(48, 91);
            this.CB_RelearnMove4.Name = "CB_RelearnMove4";
            this.CB_RelearnMove4.Size = new System.Drawing.Size(124, 21);
            this.CB_RelearnMove4.TabIndex = 16;
            this.CB_RelearnMove4.SelectedIndexChanged += new System.EventHandler(this.validateMove);
            this.CB_RelearnMove4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.removedropCB);
            this.CB_RelearnMove4.Leave += new System.EventHandler(this.validateComboBox2);
            this.CB_RelearnMove4.Validating += new System.ComponentModel.CancelEventHandler(this.validateComboBox);
            // 
            // CB_RelearnMove3
            // 
            this.CB_RelearnMove3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CB_RelearnMove3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CB_RelearnMove3.FormattingEnabled = true;
            this.CB_RelearnMove3.Location = new System.Drawing.Point(48, 69);
            this.CB_RelearnMove3.Name = "CB_RelearnMove3";
            this.CB_RelearnMove3.Size = new System.Drawing.Size(124, 21);
            this.CB_RelearnMove3.TabIndex = 15;
            this.CB_RelearnMove3.SelectedIndexChanged += new System.EventHandler(this.validateMove);
            this.CB_RelearnMove3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.removedropCB);
            this.CB_RelearnMove3.Leave += new System.EventHandler(this.validateComboBox2);
            this.CB_RelearnMove3.Validating += new System.ComponentModel.CancelEventHandler(this.validateComboBox);
            // 
            // CB_RelearnMove2
            // 
            this.CB_RelearnMove2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CB_RelearnMove2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CB_RelearnMove2.FormattingEnabled = true;
            this.CB_RelearnMove2.Location = new System.Drawing.Point(48, 47);
            this.CB_RelearnMove2.Name = "CB_RelearnMove2";
            this.CB_RelearnMove2.Size = new System.Drawing.Size(124, 21);
            this.CB_RelearnMove2.TabIndex = 14;
            this.CB_RelearnMove2.SelectedIndexChanged += new System.EventHandler(this.validateMove);
            this.CB_RelearnMove2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.removedropCB);
            this.CB_RelearnMove2.Leave += new System.EventHandler(this.validateComboBox2);
            this.CB_RelearnMove2.Validating += new System.ComponentModel.CancelEventHandler(this.validateComboBox);
            // 
            // CB_RelearnMove1
            // 
            this.CB_RelearnMove1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CB_RelearnMove1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CB_RelearnMove1.FormattingEnabled = true;
            this.CB_RelearnMove1.Location = new System.Drawing.Point(48, 25);
            this.CB_RelearnMove1.Name = "CB_RelearnMove1";
            this.CB_RelearnMove1.Size = new System.Drawing.Size(124, 21);
            this.CB_RelearnMove1.TabIndex = 13;
            this.CB_RelearnMove1.SelectedIndexChanged += new System.EventHandler(this.validateMove);
            this.CB_RelearnMove1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.removedropCB);
            this.CB_RelearnMove1.Leave += new System.EventHandler(this.validateComboBox2);
            this.CB_RelearnMove1.Validating += new System.ComponentModel.CancelEventHandler(this.validateComboBox);
            // 
            // GB_CurrentMoves
            // 
            this.GB_CurrentMoves.Controls.Add(this.TB_PP4);
            this.GB_CurrentMoves.Controls.Add(this.TB_PP3);
            this.GB_CurrentMoves.Controls.Add(this.TB_PP2);
            this.GB_CurrentMoves.Controls.Add(this.TB_PP1);
            this.GB_CurrentMoves.Controls.Add(this.Label_CurPP);
            this.GB_CurrentMoves.Controls.Add(this.Label_PPups);
            this.GB_CurrentMoves.Controls.Add(this.CB_PPu4);
            this.GB_CurrentMoves.Controls.Add(this.CB_PPu3);
            this.GB_CurrentMoves.Controls.Add(this.CB_PPu2);
            this.GB_CurrentMoves.Controls.Add(this.CB_Move4);
            this.GB_CurrentMoves.Controls.Add(this.CB_PPu1);
            this.GB_CurrentMoves.Controls.Add(this.CB_Move3);
            this.GB_CurrentMoves.Controls.Add(this.CB_Move2);
            this.GB_CurrentMoves.Controls.Add(this.CB_Move1);
            this.GB_CurrentMoves.Location = new System.Drawing.Point(27, 19);
            this.GB_CurrentMoves.Name = "GB_CurrentMoves";
            this.GB_CurrentMoves.Size = new System.Drawing.Size(220, 120);
            this.GB_CurrentMoves.TabIndex = 0;
            this.GB_CurrentMoves.TabStop = false;
            this.GB_CurrentMoves.Text = "Current Moves";
            // 
            // TB_PP4
            // 
            this.TB_PP4.Location = new System.Drawing.Point(135, 93);
            this.TB_PP4.Mask = "000";
            this.TB_PP4.Name = "TB_PP4";
            this.TB_PP4.PromptChar = ' ';
            this.TB_PP4.Size = new System.Drawing.Size(31, 20);
            this.TB_PP4.TabIndex = 16;
            this.TB_PP4.TextChanged += new System.EventHandler(this.update255_MTB);
            // 
            // TB_PP3
            // 
            this.TB_PP3.Location = new System.Drawing.Point(135, 71);
            this.TB_PP3.Mask = "000";
            this.TB_PP3.Name = "TB_PP3";
            this.TB_PP3.PromptChar = ' ';
            this.TB_PP3.Size = new System.Drawing.Size(31, 20);
            this.TB_PP3.TabIndex = 15;
            this.TB_PP3.TextChanged += new System.EventHandler(this.update255_MTB);
            // 
            // TB_PP2
            // 
            this.TB_PP2.Location = new System.Drawing.Point(135, 49);
            this.TB_PP2.Mask = "000";
            this.TB_PP2.Name = "TB_PP2";
            this.TB_PP2.PromptChar = ' ';
            this.TB_PP2.Size = new System.Drawing.Size(31, 20);
            this.TB_PP2.TabIndex = 14;
            this.TB_PP2.TextChanged += new System.EventHandler(this.update255_MTB);
            // 
            // TB_PP1
            // 
            this.TB_PP1.Location = new System.Drawing.Point(135, 27);
            this.TB_PP1.Mask = "000";
            this.TB_PP1.Name = "TB_PP1";
            this.TB_PP1.PromptChar = ' ';
            this.TB_PP1.Size = new System.Drawing.Size(31, 20);
            this.TB_PP1.TabIndex = 13;
            this.TB_PP1.TextChanged += new System.EventHandler(this.update255_MTB);
            // 
            // Label_CurPP
            // 
            this.Label_CurPP.Location = new System.Drawing.Point(133, 12);
            this.Label_CurPP.Name = "Label_CurPP";
            this.Label_CurPP.Size = new System.Drawing.Size(35, 13);
            this.Label_CurPP.TabIndex = 2;
            this.Label_CurPP.Text = "PP";
            this.Label_CurPP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_PPups
            // 
            this.Label_PPups.Location = new System.Drawing.Point(169, 12);
            this.Label_PPups.Name = "Label_PPups";
            this.Label_PPups.Size = new System.Drawing.Size(45, 13);
            this.Label_PPups.TabIndex = 12;
            this.Label_PPups.Text = "PP Ups";
            this.Label_PPups.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CB_PPu4
            // 
            this.CB_PPu4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_PPu4.FormattingEnabled = true;
            this.CB_PPu4.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.CB_PPu4.Location = new System.Drawing.Point(172, 92);
            this.CB_PPu4.Name = "CB_PPu4";
            this.CB_PPu4.Size = new System.Drawing.Size(38, 21);
            this.CB_PPu4.TabIndex = 12;
            this.CB_PPu4.SelectedIndexChanged += new System.EventHandler(this.updatePP);
            // 
            // CB_PPu3
            // 
            this.CB_PPu3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_PPu3.FormattingEnabled = true;
            this.CB_PPu3.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.CB_PPu3.Location = new System.Drawing.Point(172, 70);
            this.CB_PPu3.Name = "CB_PPu3";
            this.CB_PPu3.Size = new System.Drawing.Size(38, 21);
            this.CB_PPu3.TabIndex = 9;
            this.CB_PPu3.SelectedIndexChanged += new System.EventHandler(this.updatePP);
            // 
            // CB_PPu2
            // 
            this.CB_PPu2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_PPu2.FormattingEnabled = true;
            this.CB_PPu2.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.CB_PPu2.Location = new System.Drawing.Point(172, 48);
            this.CB_PPu2.Name = "CB_PPu2";
            this.CB_PPu2.Size = new System.Drawing.Size(38, 21);
            this.CB_PPu2.TabIndex = 6;
            this.CB_PPu2.SelectedIndexChanged += new System.EventHandler(this.updatePP);
            // 
            // CB_Move4
            // 
            this.CB_Move4.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CB_Move4.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CB_Move4.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CB_Move4.FormattingEnabled = true;
            this.CB_Move4.Location = new System.Drawing.Point(9, 92);
            this.CB_Move4.Name = "CB_Move4";
            this.CB_Move4.Size = new System.Drawing.Size(121, 21);
            this.CB_Move4.TabIndex = 10;
            this.CB_Move4.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.validateMovePaint);
            this.CB_Move4.SelectedIndexChanged += new System.EventHandler(this.validateMove);
            this.CB_Move4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.removedropCB);
            this.CB_Move4.Leave += new System.EventHandler(this.validateComboBox2);
            this.CB_Move4.Validating += new System.ComponentModel.CancelEventHandler(this.validateComboBox);
            // 
            // CB_PPu1
            // 
            this.CB_PPu1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_PPu1.FormattingEnabled = true;
            this.CB_PPu1.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.CB_PPu1.Location = new System.Drawing.Point(172, 26);
            this.CB_PPu1.Name = "CB_PPu1";
            this.CB_PPu1.Size = new System.Drawing.Size(38, 21);
            this.CB_PPu1.TabIndex = 3;
            this.CB_PPu1.SelectedIndexChanged += new System.EventHandler(this.updatePP);
            // 
            // CB_Move3
            // 
            this.CB_Move3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CB_Move3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CB_Move3.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CB_Move3.FormattingEnabled = true;
            this.CB_Move3.Location = new System.Drawing.Point(9, 70);
            this.CB_Move3.Name = "CB_Move3";
            this.CB_Move3.Size = new System.Drawing.Size(121, 21);
            this.CB_Move3.TabIndex = 7;
            this.CB_Move3.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.validateMovePaint);
            this.CB_Move3.SelectedIndexChanged += new System.EventHandler(this.validateMove);
            this.CB_Move3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.removedropCB);
            this.CB_Move3.Leave += new System.EventHandler(this.validateComboBox2);
            this.CB_Move3.Validating += new System.ComponentModel.CancelEventHandler(this.validateComboBox);
            // 
            // CB_Move2
            // 
            this.CB_Move2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CB_Move2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CB_Move2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CB_Move2.FormattingEnabled = true;
            this.CB_Move2.Location = new System.Drawing.Point(9, 48);
            this.CB_Move2.Name = "CB_Move2";
            this.CB_Move2.Size = new System.Drawing.Size(121, 21);
            this.CB_Move2.TabIndex = 4;
            this.CB_Move2.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.validateMovePaint);
            this.CB_Move2.SelectedIndexChanged += new System.EventHandler(this.validateMove);
            this.CB_Move2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.removedropCB);
            this.CB_Move2.Leave += new System.EventHandler(this.validateComboBox2);
            this.CB_Move2.Validating += new System.ComponentModel.CancelEventHandler(this.validateComboBox);
            // 
            // CB_Move1
            // 
            this.CB_Move1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CB_Move1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CB_Move1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CB_Move1.FormattingEnabled = true;
            this.CB_Move1.Location = new System.Drawing.Point(9, 26);
            this.CB_Move1.Name = "CB_Move1";
            this.CB_Move1.Size = new System.Drawing.Size(121, 21);
            this.CB_Move1.TabIndex = 1;
            this.CB_Move1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.validateMovePaint);
            this.CB_Move1.SelectedIndexChanged += new System.EventHandler(this.validateMove);
            this.CB_Move1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.removedropCB);
            this.CB_Move1.Leave += new System.EventHandler(this.validateComboBox2);
            this.CB_Move1.Validating += new System.ComponentModel.CancelEventHandler(this.validateComboBox);
            // 
            // Tab_OTMisc
            // 
            this.Tab_OTMisc.AllowDrop = true;
            this.Tab_OTMisc.Controls.Add(this.GB_Markings);
            this.Tab_OTMisc.Controls.Add(this.FLP_PKMEditors);
            this.Tab_OTMisc.Controls.Add(this.TB_EC);
            this.Tab_OTMisc.Controls.Add(this.GB_nOT);
            this.Tab_OTMisc.Controls.Add(this.BTN_RerollEC);
            this.Tab_OTMisc.Controls.Add(this.GB_ExtraBytes);
            this.Tab_OTMisc.Controls.Add(this.GB_OT);
            this.Tab_OTMisc.Controls.Add(this.Label_EncryptionConstant);
            this.Tab_OTMisc.Location = new System.Drawing.Point(4, 22);
            this.Tab_OTMisc.Name = "Tab_OTMisc";
            this.Tab_OTMisc.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_OTMisc.Size = new System.Drawing.Size(272, 302);
            this.Tab_OTMisc.TabIndex = 4;
            this.Tab_OTMisc.Text = "OT/Misc";
            this.Tab_OTMisc.UseVisualStyleBackColor = true;
            // 
            // GB_Markings
            // 
            this.GB_Markings.Controls.Add(this.PB_MarkHorohoro);
            this.GB_Markings.Controls.Add(this.PB_MarkVC);
            this.GB_Markings.Controls.Add(this.PB_MarkAlola);
            this.GB_Markings.Controls.Add(this.PB_Mark6);
            this.GB_Markings.Controls.Add(this.PB_MarkPentagon);
            this.GB_Markings.Controls.Add(this.PB_Mark3);
            this.GB_Markings.Controls.Add(this.PB_Mark5);
            this.GB_Markings.Controls.Add(this.PB_MarkCured);
            this.GB_Markings.Controls.Add(this.PB_Mark2);
            this.GB_Markings.Controls.Add(this.PB_MarkShiny);
            this.GB_Markings.Controls.Add(this.PB_Mark1);
            this.GB_Markings.Controls.Add(this.PB_Mark4);
            this.GB_Markings.Location = new System.Drawing.Point(68, 184);
            this.GB_Markings.Name = "GB_Markings";
            this.GB_Markings.Size = new System.Drawing.Size(135, 58);
            this.GB_Markings.TabIndex = 10;
            this.GB_Markings.TabStop = false;
            this.GB_Markings.Text = "Markings";
            // 
            // PB_MarkHorohoro
            // 
            this.PB_MarkHorohoro.Image = ((System.Drawing.Image)(resources.GetObject("PB_MarkHorohoro.Image")));
            this.PB_MarkHorohoro.InitialImage = ((System.Drawing.Image)(resources.GetObject("PB_MarkHorohoro.InitialImage")));
            this.PB_MarkHorohoro.Location = new System.Drawing.Point(110, 15);
            this.PB_MarkHorohoro.Name = "PB_MarkHorohoro";
            this.PB_MarkHorohoro.Size = new System.Drawing.Size(20, 20);
            this.PB_MarkHorohoro.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PB_MarkHorohoro.TabIndex = 11;
            this.PB_MarkHorohoro.TabStop = false;
            // 
            // PB_MarkVC
            // 
            this.PB_MarkVC.Image = ((System.Drawing.Image)(resources.GetObject("PB_MarkVC.Image")));
            this.PB_MarkVC.InitialImage = ((System.Drawing.Image)(resources.GetObject("PB_MarkVC.InitialImage")));
            this.PB_MarkVC.Location = new System.Drawing.Point(89, 15);
            this.PB_MarkVC.Name = "PB_MarkVC";
            this.PB_MarkVC.Size = new System.Drawing.Size(20, 20);
            this.PB_MarkVC.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PB_MarkVC.TabIndex = 10;
            this.PB_MarkVC.TabStop = false;
            // 
            // PB_MarkAlola
            // 
            this.PB_MarkAlola.Image = ((System.Drawing.Image)(resources.GetObject("PB_MarkAlola.Image")));
            this.PB_MarkAlola.InitialImage = ((System.Drawing.Image)(resources.GetObject("PB_MarkAlola.InitialImage")));
            this.PB_MarkAlola.Location = new System.Drawing.Point(68, 15);
            this.PB_MarkAlola.Name = "PB_MarkAlola";
            this.PB_MarkAlola.Size = new System.Drawing.Size(20, 20);
            this.PB_MarkAlola.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PB_MarkAlola.TabIndex = 9;
            this.PB_MarkAlola.TabStop = false;
            // 
            // PB_Mark6
            // 
            this.PB_Mark6.Image = ((System.Drawing.Image)(resources.GetObject("PB_Mark6.Image")));
            this.PB_Mark6.InitialImage = ((System.Drawing.Image)(resources.GetObject("PB_Mark6.InitialImage")));
            this.PB_Mark6.Location = new System.Drawing.Point(110, 36);
            this.PB_Mark6.Margin = new System.Windows.Forms.Padding(1);
            this.PB_Mark6.Name = "PB_Mark6";
            this.PB_Mark6.Size = new System.Drawing.Size(20, 20);
            this.PB_Mark6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PB_Mark6.TabIndex = 5;
            this.PB_Mark6.TabStop = false;
            this.PB_Mark6.Click += new System.EventHandler(this.clickMarking);
            // 
            // PB_MarkPentagon
            // 
            this.PB_MarkPentagon.Image = ((System.Drawing.Image)(resources.GetObject("PB_MarkPentagon.Image")));
            this.PB_MarkPentagon.InitialImage = ((System.Drawing.Image)(resources.GetObject("PB_MarkPentagon.InitialImage")));
            this.PB_MarkPentagon.Location = new System.Drawing.Point(47, 15);
            this.PB_MarkPentagon.Name = "PB_MarkPentagon";
            this.PB_MarkPentagon.Size = new System.Drawing.Size(20, 20);
            this.PB_MarkPentagon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PB_MarkPentagon.TabIndex = 8;
            this.PB_MarkPentagon.TabStop = false;
            // 
            // PB_Mark3
            // 
            this.PB_Mark3.Image = ((System.Drawing.Image)(resources.GetObject("PB_Mark3.Image")));
            this.PB_Mark3.InitialImage = ((System.Drawing.Image)(resources.GetObject("PB_Mark3.InitialImage")));
            this.PB_Mark3.Location = new System.Drawing.Point(47, 36);
            this.PB_Mark3.Margin = new System.Windows.Forms.Padding(1);
            this.PB_Mark3.Name = "PB_Mark3";
            this.PB_Mark3.Size = new System.Drawing.Size(20, 20);
            this.PB_Mark3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PB_Mark3.TabIndex = 2;
            this.PB_Mark3.TabStop = false;
            this.PB_Mark3.Click += new System.EventHandler(this.clickMarking);
            // 
            // PB_Mark5
            // 
            this.PB_Mark5.Image = ((System.Drawing.Image)(resources.GetObject("PB_Mark5.Image")));
            this.PB_Mark5.InitialImage = ((System.Drawing.Image)(resources.GetObject("PB_Mark5.InitialImage")));
            this.PB_Mark5.Location = new System.Drawing.Point(89, 36);
            this.PB_Mark5.Margin = new System.Windows.Forms.Padding(1);
            this.PB_Mark5.Name = "PB_Mark5";
            this.PB_Mark5.Size = new System.Drawing.Size(20, 20);
            this.PB_Mark5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PB_Mark5.TabIndex = 4;
            this.PB_Mark5.TabStop = false;
            this.PB_Mark5.Click += new System.EventHandler(this.clickMarking);
            // 
            // PB_MarkCured
            // 
            this.PB_MarkCured.Image = ((System.Drawing.Image)(resources.GetObject("PB_MarkCured.Image")));
            this.PB_MarkCured.InitialImage = ((System.Drawing.Image)(resources.GetObject("PB_MarkCured.InitialImage")));
            this.PB_MarkCured.Location = new System.Drawing.Point(26, 15);
            this.PB_MarkCured.Name = "PB_MarkCured";
            this.PB_MarkCured.Size = new System.Drawing.Size(20, 20);
            this.PB_MarkCured.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PB_MarkCured.TabIndex = 7;
            this.PB_MarkCured.TabStop = false;
            // 
            // PB_Mark2
            // 
            this.PB_Mark2.Image = ((System.Drawing.Image)(resources.GetObject("PB_Mark2.Image")));
            this.PB_Mark2.InitialImage = ((System.Drawing.Image)(resources.GetObject("PB_Mark2.InitialImage")));
            this.PB_Mark2.Location = new System.Drawing.Point(26, 36);
            this.PB_Mark2.Margin = new System.Windows.Forms.Padding(1);
            this.PB_Mark2.Name = "PB_Mark2";
            this.PB_Mark2.Size = new System.Drawing.Size(20, 20);
            this.PB_Mark2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PB_Mark2.TabIndex = 1;
            this.PB_Mark2.TabStop = false;
            this.PB_Mark2.Click += new System.EventHandler(this.clickMarking);
            // 
            // PB_MarkShiny
            // 
            this.PB_MarkShiny.Image = ((System.Drawing.Image)(resources.GetObject("PB_MarkShiny.Image")));
            this.PB_MarkShiny.InitialImage = ((System.Drawing.Image)(resources.GetObject("PB_MarkShiny.InitialImage")));
            this.PB_MarkShiny.Location = new System.Drawing.Point(5, 15);
            this.PB_MarkShiny.Name = "PB_MarkShiny";
            this.PB_MarkShiny.Size = new System.Drawing.Size(20, 20);
            this.PB_MarkShiny.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PB_MarkShiny.TabIndex = 6;
            this.PB_MarkShiny.TabStop = false;
            // 
            // PB_Mark1
            // 
            this.PB_Mark1.Image = ((System.Drawing.Image)(resources.GetObject("PB_Mark1.Image")));
            this.PB_Mark1.InitialImage = ((System.Drawing.Image)(resources.GetObject("PB_Mark1.InitialImage")));
            this.PB_Mark1.Location = new System.Drawing.Point(5, 36);
            this.PB_Mark1.Margin = new System.Windows.Forms.Padding(1);
            this.PB_Mark1.Name = "PB_Mark1";
            this.PB_Mark1.Size = new System.Drawing.Size(20, 20);
            this.PB_Mark1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PB_Mark1.TabIndex = 0;
            this.PB_Mark1.TabStop = false;
            this.PB_Mark1.Click += new System.EventHandler(this.clickMarking);
            // 
            // PB_Mark4
            // 
            this.PB_Mark4.Image = ((System.Drawing.Image)(resources.GetObject("PB_Mark4.Image")));
            this.PB_Mark4.InitialImage = ((System.Drawing.Image)(resources.GetObject("PB_Mark4.InitialImage")));
            this.PB_Mark4.Location = new System.Drawing.Point(68, 36);
            this.PB_Mark4.Margin = new System.Windows.Forms.Padding(1);
            this.PB_Mark4.Name = "PB_Mark4";
            this.PB_Mark4.Size = new System.Drawing.Size(20, 20);
            this.PB_Mark4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PB_Mark4.TabIndex = 3;
            this.PB_Mark4.TabStop = false;
            this.PB_Mark4.Click += new System.EventHandler(this.clickMarking);
            // 
            // FLP_PKMEditors
            // 
            this.FLP_PKMEditors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FLP_PKMEditors.AutoSize = true;
            this.FLP_PKMEditors.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FLP_PKMEditors.Controls.Add(this.BTN_Ribbons);
            this.FLP_PKMEditors.Controls.Add(this.BTN_Medals);
            this.FLP_PKMEditors.Controls.Add(this.BTN_History);
            this.FLP_PKMEditors.Location = new System.Drawing.Point(49, 245);
            this.FLP_PKMEditors.Name = "FLP_PKMEditors";
            this.FLP_PKMEditors.Size = new System.Drawing.Size(175, 25);
            this.FLP_PKMEditors.TabIndex = 9;
            this.FLP_PKMEditors.WrapContents = false;
            // 
            // BTN_Ribbons
            // 
            this.BTN_Ribbons.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_Ribbons.AutoSize = true;
            this.BTN_Ribbons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BTN_Ribbons.Location = new System.Drawing.Point(1, 1);
            this.BTN_Ribbons.Margin = new System.Windows.Forms.Padding(1);
            this.BTN_Ribbons.Name = "BTN_Ribbons";
            this.BTN_Ribbons.Size = new System.Drawing.Size(56, 23);
            this.BTN_Ribbons.TabIndex = 5;
            this.BTN_Ribbons.Text = "Ribbons";
            this.BTN_Ribbons.UseVisualStyleBackColor = true;
            this.BTN_Ribbons.Click += new System.EventHandler(this.openRibbons);
            // 
            // BTN_Medals
            // 
            this.BTN_Medals.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_Medals.AutoSize = true;
            this.BTN_Medals.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BTN_Medals.Location = new System.Drawing.Point(59, 1);
            this.BTN_Medals.Margin = new System.Windows.Forms.Padding(1);
            this.BTN_Medals.Name = "BTN_Medals";
            this.BTN_Medals.Size = new System.Drawing.Size(51, 23);
            this.BTN_Medals.TabIndex = 7;
            this.BTN_Medals.Text = "Medals";
            this.BTN_Medals.UseVisualStyleBackColor = true;
            this.BTN_Medals.Click += new System.EventHandler(this.openMedals);
            // 
            // BTN_History
            // 
            this.BTN_History.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_History.AutoSize = true;
            this.BTN_History.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BTN_History.Location = new System.Drawing.Point(112, 1);
            this.BTN_History.Margin = new System.Windows.Forms.Padding(1);
            this.BTN_History.Name = "BTN_History";
            this.BTN_History.Size = new System.Drawing.Size(62, 23);
            this.BTN_History.TabIndex = 6;
            this.BTN_History.Text = "Memories";
            this.BTN_History.UseVisualStyleBackColor = true;
            this.BTN_History.Click += new System.EventHandler(this.openHistory);
            // 
            // TB_EC
            // 
            this.TB_EC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_EC.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB_EC.Location = new System.Drawing.Point(176, 276);
            this.TB_EC.MaxLength = 8;
            this.TB_EC.Name = "TB_EC";
            this.TB_EC.Size = new System.Drawing.Size(60, 20);
            this.TB_EC.TabIndex = 8;
            this.TB_EC.Text = "12345678";
            this.TB_EC.TextChanged += new System.EventHandler(this.update_ID);
            // 
            // GB_nOT
            // 
            this.GB_nOT.Controls.Add(this.Label_CTGender);
            this.GB_nOT.Controls.Add(this.TB_OTt2);
            this.GB_nOT.Controls.Add(this.Label_PrevOT);
            this.GB_nOT.Location = new System.Drawing.Point(40, 85);
            this.GB_nOT.Name = "GB_nOT";
            this.GB_nOT.Size = new System.Drawing.Size(190, 50);
            this.GB_nOT.TabIndex = 2;
            this.GB_nOT.TabStop = false;
            this.GB_nOT.Text = "Latest (not OT) Handler";
            // 
            // Label_CTGender
            // 
            this.Label_CTGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_CTGender.Location = new System.Drawing.Point(144, 23);
            this.Label_CTGender.Name = "Label_CTGender";
            this.Label_CTGender.Size = new System.Drawing.Size(16, 13);
            this.Label_CTGender.TabIndex = 57;
            this.Label_CTGender.Text = "G";
            this.Label_CTGender.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_CTGender.Click += new System.EventHandler(this.clickTRGender);
            // 
            // TB_OTt2
            // 
            this.TB_OTt2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TB_OTt2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_OTt2.Location = new System.Drawing.Point(46, 20);
            this.TB_OTt2.MaxLength = 12;
            this.TB_OTt2.Name = "TB_OTt2";
            this.TB_OTt2.Size = new System.Drawing.Size(94, 20);
            this.TB_OTt2.TabIndex = 1;
            this.TB_OTt2.WordWrap = false;
            this.TB_OTt2.TextChanged += new System.EventHandler(this.updateNotOT);
            this.TB_OTt2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.updateNicknameClick);
            // 
            // Label_PrevOT
            // 
            this.Label_PrevOT.Location = new System.Drawing.Point(4, 23);
            this.Label_PrevOT.Name = "Label_PrevOT";
            this.Label_PrevOT.Size = new System.Drawing.Size(40, 13);
            this.Label_PrevOT.TabIndex = 42;
            this.Label_PrevOT.Text = "OT:";
            this.Label_PrevOT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Label_PrevOT.Click += new System.EventHandler(this.clickCT);
            // 
            // BTN_RerollEC
            // 
            this.BTN_RerollEC.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.BTN_RerollEC.Location = new System.Drawing.Point(138, 276);
            this.BTN_RerollEC.Name = "BTN_RerollEC";
            this.BTN_RerollEC.Size = new System.Drawing.Size(38, 20);
            this.BTN_RerollEC.TabIndex = 7;
            this.BTN_RerollEC.Text = "Reroll";
            this.BTN_RerollEC.UseVisualStyleBackColor = true;
            this.BTN_RerollEC.Click += new System.EventHandler(this.updateRandomEC);
            // 
            // GB_ExtraBytes
            // 
            this.GB_ExtraBytes.Controls.Add(this.TB_ExtraByte);
            this.GB_ExtraBytes.Controls.Add(this.CB_ExtraBytes);
            this.GB_ExtraBytes.Location = new System.Drawing.Point(68, 135);
            this.GB_ExtraBytes.Name = "GB_ExtraBytes";
            this.GB_ExtraBytes.Size = new System.Drawing.Size(135, 48);
            this.GB_ExtraBytes.TabIndex = 3;
            this.GB_ExtraBytes.TabStop = false;
            this.GB_ExtraBytes.Text = "Extra Bytes";
            // 
            // TB_ExtraByte
            // 
            this.TB_ExtraByte.Location = new System.Drawing.Point(87, 19);
            this.TB_ExtraByte.Mask = "000";
            this.TB_ExtraByte.Name = "TB_ExtraByte";
            this.TB_ExtraByte.Size = new System.Drawing.Size(28, 20);
            this.TB_ExtraByte.TabIndex = 2;
            this.TB_ExtraByte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_ExtraByte.TextChanged += new System.EventHandler(this.updateExtraByteValue);
            // 
            // CB_ExtraBytes
            // 
            this.CB_ExtraBytes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_ExtraBytes.FormattingEnabled = true;
            this.CB_ExtraBytes.Location = new System.Drawing.Point(20, 18);
            this.CB_ExtraBytes.Name = "CB_ExtraBytes";
            this.CB_ExtraBytes.Size = new System.Drawing.Size(57, 21);
            this.CB_ExtraBytes.TabIndex = 1;
            this.CB_ExtraBytes.SelectedIndexChanged += new System.EventHandler(this.updateExtraByteIndex);
            // 
            // GB_OT
            // 
            this.GB_OT.Controls.Add(this.Label_OTGender);
            this.GB_OT.Controls.Add(this.TB_OT);
            this.GB_OT.Controls.Add(this.TB_SID);
            this.GB_OT.Controls.Add(this.TB_TID);
            this.GB_OT.Controls.Add(this.Label_OT);
            this.GB_OT.Controls.Add(this.Label_SID);
            this.GB_OT.Controls.Add(this.Label_TID);
            this.GB_OT.Location = new System.Drawing.Point(40, 8);
            this.GB_OT.Name = "GB_OT";
            this.GB_OT.Size = new System.Drawing.Size(190, 75);
            this.GB_OT.TabIndex = 1;
            this.GB_OT.TabStop = false;
            this.GB_OT.Text = "Trainer Information";
            // 
            // Label_OTGender
            // 
            this.Label_OTGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_OTGender.Location = new System.Drawing.Point(144, 48);
            this.Label_OTGender.Name = "Label_OTGender";
            this.Label_OTGender.Size = new System.Drawing.Size(16, 13);
            this.Label_OTGender.TabIndex = 56;
            this.Label_OTGender.Text = "G";
            this.Label_OTGender.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_OTGender.Click += new System.EventHandler(this.clickTRGender);
            // 
            // TB_OT
            // 
            this.TB_OT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_OT.Location = new System.Drawing.Point(46, 46);
            this.TB_OT.MaxLength = 12;
            this.TB_OT.Name = "TB_OT";
            this.TB_OT.Size = new System.Drawing.Size(94, 20);
            this.TB_OT.TabIndex = 3;
            this.TB_OT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.updateNicknameClick);
            // 
            // TB_SID
            // 
            this.TB_SID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_SID.Location = new System.Drawing.Point(132, 20);
            this.TB_SID.Mask = "00000";
            this.TB_SID.Name = "TB_SID";
            this.TB_SID.Size = new System.Drawing.Size(40, 20);
            this.TB_SID.TabIndex = 2;
            this.TB_SID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_SID.TextChanged += new System.EventHandler(this.update_ID);
            this.TB_SID.MouseHover += new System.EventHandler(this.updateTSV);
            // 
            // TB_TID
            // 
            this.TB_TID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_TID.Location = new System.Drawing.Point(46, 20);
            this.TB_TID.Mask = "00000";
            this.TB_TID.Name = "TB_TID";
            this.TB_TID.Size = new System.Drawing.Size(40, 20);
            this.TB_TID.TabIndex = 1;
            this.TB_TID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_TID.TextChanged += new System.EventHandler(this.update_ID);
            this.TB_TID.MouseHover += new System.EventHandler(this.updateTSV);
            // 
            // Label_OT
            // 
            this.Label_OT.Location = new System.Drawing.Point(4, 48);
            this.Label_OT.Name = "Label_OT";
            this.Label_OT.Size = new System.Drawing.Size(40, 13);
            this.Label_OT.TabIndex = 5;
            this.Label_OT.Text = "OT:";
            this.Label_OT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Label_OT.Click += new System.EventHandler(this.clickOT);
            // 
            // Label_SID
            // 
            this.Label_SID.Location = new System.Drawing.Point(86, 22);
            this.Label_SID.Name = "Label_SID";
            this.Label_SID.Size = new System.Drawing.Size(45, 13);
            this.Label_SID.TabIndex = 4;
            this.Label_SID.Text = "SID:";
            this.Label_SID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label_TID
            // 
            this.Label_TID.Location = new System.Drawing.Point(4, 22);
            this.Label_TID.Name = "Label_TID";
            this.Label_TID.Size = new System.Drawing.Size(40, 13);
            this.Label_TID.TabIndex = 3;
            this.Label_TID.Text = "TID:";
            this.Label_TID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label_EncryptionConstant
            // 
            this.Label_EncryptionConstant.Location = new System.Drawing.Point(20, 279);
            this.Label_EncryptionConstant.Name = "Label_EncryptionConstant";
            this.Label_EncryptionConstant.Size = new System.Drawing.Size(120, 13);
            this.Label_EncryptionConstant.TabIndex = 1;
            this.Label_EncryptionConstant.Text = "Encryption Constant:";
            this.Label_EncryptionConstant.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Tool_Trainer
            // 
            this.Tool_Trainer.Location = new System.Drawing.Point(3, 3);
            this.Tool_Trainer.Name = "Tool_Trainer";
            this.Tool_Trainer.Size = new System.Drawing.Size(89, 23);
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
            this.Tabs_General.Location = new System.Drawing.Point(12, 12);
            this.Tabs_General.Name = "Tabs_General";
            this.Tabs_General.SelectedIndex = 0;
            this.Tabs_General.Size = new System.Drawing.Size(300, 290);
            this.Tabs_General.TabIndex = 1;
            // 
            // Tab_Dump
            // 
            this.Tab_Dump.BackColor = System.Drawing.SystemColors.Control;
            this.Tab_Dump.Controls.Add(this.StopPollingButton);
            this.Tab_Dump.Controls.Add(this.StartPollingButton);
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
            this.Tab_Dump.Location = new System.Drawing.Point(4, 22);
            this.Tab_Dump.Name = "Tab_Dump";
            this.Tab_Dump.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_Dump.Size = new System.Drawing.Size(292, 264);
            this.Tab_Dump.TabIndex = 4;
            this.Tab_Dump.Text = "Read/Write";
            // 
            // StartPollingButton
            // 
            this.StartPollingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.StartPollingButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.StartPollingButton.Enabled = true;
            this.StartPollingButton.Location = new System.Drawing.Point(9, 210);
            this.StartPollingButton.Name = "StartPollingButton";
            this.StartPollingButton.Size = new System.Drawing.Size(91, 23);
            this.StartPollingButton.TabIndex = 14;
            this.StartPollingButton.Text = "Start Polling";
            this.StartPollingButton.UseVisualStyleBackColor = true;
            this.StartPollingButton.Click += new System.EventHandler(this.StartPollingButton_Click);
            // 
            // Write_PKM
            // 
            this.Write_PKM.Enabled = false;
            this.Write_PKM.Location = new System.Drawing.Point(6, 111);
            this.Write_PKM.Name = "Write_PKM";
            this.Write_PKM.Size = new System.Drawing.Size(280, 23);
            this.Write_PKM.TabIndex = 5;
            this.Write_PKM.Text = "Write Pokémon";
            this.Write_PKM.UseVisualStyleBackColor = true;
            this.Write_PKM.Click += new System.EventHandler(this.Write_PKM_Click);
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 51);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(280, 54);
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
            this.Tab_Clone.Location = new System.Drawing.Point(4, 22);
            this.Tab_Clone.Name = "Tab_Clone";
            this.Tab_Clone.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_Clone.Size = new System.Drawing.Size(292, 264);
            this.Tab_Clone.TabIndex = 1;
            this.Tab_Clone.Text = "Clone/Delete";
            // 
            // Btn_CDstart
            // 
            this.Btn_CDstart.Location = new System.Drawing.Point(157, 71);
            this.Btn_CDstart.Name = "Btn_CDstart";
            this.Btn_CDstart.Size = new System.Drawing.Size(129, 23);
            this.Btn_CDstart.TabIndex = 4;
            this.Btn_CDstart.Text = "Go!";
            this.toolTip1.SetToolTip(this.Btn_CDstart, "Pokémon will be cloned or deleted starting at the specified position.");
            this.Btn_CDstart.UseVisualStyleBackColor = true;
            this.Btn_CDstart.Click += new System.EventHandler(this.Btn_CDstart_Click);
            // 
            // CB_CDBackup
            // 
            this.CB_CDBackup.AutoSize = true;
            this.CB_CDBackup.Location = new System.Drawing.Point(157, 26);
            this.CB_CDBackup.Name = "CB_CDBackup";
            this.CB_CDBackup.Size = new System.Drawing.Size(90, 17);
            this.CB_CDBackup.TabIndex = 0;
            this.CB_CDBackup.Text = "Keep backup";
            this.CB_CDBackup.UseVisualStyleBackColor = true;
            // 
            // GB_CDmode
            // 
            this.GB_CDmode.Controls.Add(this.DeleteMode);
            this.GB_CDmode.Controls.Add(this.CloneMode);
            this.GB_CDmode.Location = new System.Drawing.Point(6, 6);
            this.GB_CDmode.Name = "GB_CDmode";
            this.GB_CDmode.Size = new System.Drawing.Size(145, 49);
            this.GB_CDmode.TabIndex = 0;
            this.GB_CDmode.TabStop = false;
            this.GB_CDmode.Text = "Mode";
            // 
            // DeleteMode
            // 
            this.DeleteMode.AutoSize = true;
            this.DeleteMode.Location = new System.Drawing.Point(83, 19);
            this.DeleteMode.Name = "DeleteMode";
            this.DeleteMode.Size = new System.Drawing.Size(56, 17);
            this.DeleteMode.TabIndex = 1;
            this.DeleteMode.Text = "Delete";
            this.DeleteMode.UseVisualStyleBackColor = true;
            // 
            // CloneMode
            // 
            this.CloneMode.AutoSize = true;
            this.CloneMode.Checked = true;
            this.CloneMode.Location = new System.Drawing.Point(6, 19);
            this.CloneMode.Name = "CloneMode";
            this.CloneMode.Size = new System.Drawing.Size(52, 17);
            this.CloneMode.TabIndex = 0;
            this.CloneMode.TabStop = true;
            this.CloneMode.Text = "Clone";
            this.toolTip1.SetToolTip(this.CloneMode, "Source is the pokémon shown in the tabs.");
            this.CloneMode.UseVisualStyleBackColor = true;
            // 
            // Num_CDBox
            // 
            this.Num_CDBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Num_CDBox.Location = new System.Drawing.Point(9, 74);
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
            this.Num_CDBox.Size = new System.Drawing.Size(40, 20);
            this.Num_CDBox.TabIndex = 1;
            this.Num_CDBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_CDBox.ValueChanged += new System.EventHandler(this.clonedelete_Changed);
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(98, 58);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(46, 13);
            this.label16.TabIndex = 13;
            this.label16.Text = "Amount:";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(52, 58);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(28, 13);
            this.label14.TabIndex = 13;
            this.label14.Text = "Slot:";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(9, 58);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(28, 13);
            this.label15.TabIndex = 12;
            this.label15.Text = "Box:";
            // 
            // Num_CDAmount
            // 
            this.Num_CDAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Num_CDAmount.AutoSize = true;
            this.Num_CDAmount.Location = new System.Drawing.Point(101, 74);
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
            this.Num_CDAmount.Size = new System.Drawing.Size(50, 20);
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
            this.Num_CDSlot.Location = new System.Drawing.Point(55, 74);
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
            this.Num_CDSlot.Size = new System.Drawing.Size(40, 20);
            this.Num_CDSlot.TabIndex = 2;
            this.Num_CDSlot.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_CDSlot.ValueChanged += new System.EventHandler(this.clonedelete_Changed);
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
            this.Tab_Tools.Location = new System.Drawing.Point(4, 22);
            this.Tab_Tools.Name = "Tab_Tools";
            this.Tab_Tools.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_Tools.Size = new System.Drawing.Size(292, 264);
            this.Tab_Tools.TabIndex = 0;
            this.Tab_Tools.Text = "Tools";
            // 
            // resetNoBox
            // 
            this.resetNoBox.Location = new System.Drawing.Point(102, 205);
            this.resetNoBox.Name = "resetNoBox";
            this.resetNoBox.Size = new System.Drawing.Size(88, 20);
            this.resetNoBox.TabIndex = 17;
            // 
            // labelreset
            // 
            this.labelreset.Location = new System.Drawing.Point(6, 208);
            this.labelreset.Name = "labelreset";
            this.labelreset.Size = new System.Drawing.Size(90, 13);
            this.labelreset.TabIndex = 16;
            this.labelreset.Text = "Starting number:";
            this.labelreset.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Btn_ReloadFields
            // 
            this.Btn_ReloadFields.Location = new System.Drawing.Point(196, 205);
            this.Btn_ReloadFields.Name = "Btn_ReloadFields";
            this.Btn_ReloadFields.Size = new System.Drawing.Size(89, 23);
            this.Btn_ReloadFields.TabIndex = 3;
            this.Btn_ReloadFields.Text = "Reload Fields";
            this.toolTip1.SetToolTip(this.Btn_ReloadFields, "Also reloads the \"About\" tab.");
            this.Btn_ReloadFields.UseVisualStyleBackColor = true;
            this.Btn_ReloadFields.Click += new System.EventHandler(this.Btn_ReloadFields_Click);
            // 
            // Seed_Legendary
            // 
            this.Seed_Legendary.Location = new System.Drawing.Point(6, 179);
            this.Seed_Legendary.Name = "Seed_Legendary";
            this.Seed_Legendary.ReadOnly = true;
            this.Seed_Legendary.Size = new System.Drawing.Size(279, 20);
            this.Seed_Legendary.TabIndex = 2;
            // 
            // Seed_Egg
            // 
            this.Seed_Egg.Location = new System.Drawing.Point(6, 140);
            this.Seed_Egg.Name = "Seed_Egg";
            this.Seed_Egg.ReadOnly = true;
            this.Seed_Egg.Size = new System.Drawing.Size(279, 20);
            this.Seed_Egg.TabIndex = 1;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 163);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(88, 13);
            this.label19.TabIndex = 1;
            this.label19.Text = "Legendary Seed:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 124);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(57, 13);
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
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(286, 116);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // Tool_Items
            // 
            this.Tool_Items.Location = new System.Drawing.Point(98, 3);
            this.Tool_Items.Name = "Tool_Items";
            this.Tool_Items.Size = new System.Drawing.Size(89, 23);
            this.Tool_Items.TabIndex = 1;
            this.Tool_Items.Text = "Edit Items";
            this.Tool_Items.UseVisualStyleBackColor = true;
            this.Tool_Items.Click += new System.EventHandler(this.Tool_Items_Click);
            // 
            // Tool_Controls
            // 
            this.Tool_Controls.Location = new System.Drawing.Point(193, 3);
            this.Tool_Controls.Name = "Tool_Controls";
            this.Tool_Controls.Size = new System.Drawing.Size(89, 23);
            this.Tool_Controls.TabIndex = 2;
            this.Tool_Controls.Text = "Remote Control";
            this.Tool_Controls.UseVisualStyleBackColor = true;
            this.Tool_Controls.Click += new System.EventHandler(this.Tool_Controls_Click);
            // 
            // Tools_Breeding
            // 
            this.Tools_Breeding.Location = new System.Drawing.Point(3, 32);
            this.Tools_Breeding.Name = "Tools_Breeding";
            this.Tools_Breeding.Size = new System.Drawing.Size(89, 23);
            this.Tools_Breeding.TabIndex = 3;
            this.Tools_Breeding.Text = "Breeding Bot";
            this.Tools_Breeding.UseVisualStyleBackColor = true;
            this.Tools_Breeding.Click += new System.EventHandler(this.Tools_Breeding_Click);
            // 
            // Tools_SoftReset
            // 
            this.Tools_SoftReset.Location = new System.Drawing.Point(98, 32);
            this.Tools_SoftReset.Name = "Tools_SoftReset";
            this.Tools_SoftReset.Size = new System.Drawing.Size(89, 23);
            this.Tools_SoftReset.TabIndex = 4;
            this.Tools_SoftReset.Text = "Soft-reset Bot";
            this.Tools_SoftReset.UseVisualStyleBackColor = true;
            this.Tools_SoftReset.Click += new System.EventHandler(this.Tools_SoftReset_Click);
            // 
            // Tools_WonderTrade
            // 
            this.Tools_WonderTrade.Location = new System.Drawing.Point(193, 32);
            this.Tools_WonderTrade.Name = "Tools_WonderTrade";
            this.Tools_WonderTrade.Size = new System.Drawing.Size(89, 23);
            this.Tools_WonderTrade.TabIndex = 5;
            this.Tools_WonderTrade.Text = "WT Bot";
            this.Tools_WonderTrade.UseVisualStyleBackColor = true;
            this.Tools_WonderTrade.Click += new System.EventHandler(this.Tools_WonderTrade_Click);
            // 
            // Tool_Script
            // 
            this.Tool_Script.Location = new System.Drawing.Point(3, 61);
            this.Tool_Script.Name = "Tool_Script";
            this.Tool_Script.Size = new System.Drawing.Size(89, 23);
            this.Tool_Script.TabIndex = 7;
            this.Tool_Script.Text = "Script Builder";
            this.Tool_Script.UseVisualStyleBackColor = true;
            this.Tool_Script.Click += new System.EventHandler(this.Tool_Script_Click);
            // 
            // Tools_PokeDigger
            // 
            this.Tools_PokeDigger.Location = new System.Drawing.Point(98, 61);
            this.Tools_PokeDigger.Name = "Tools_PokeDigger";
            this.Tools_PokeDigger.Size = new System.Drawing.Size(89, 23);
            this.Tools_PokeDigger.TabIndex = 7;
            this.Tools_PokeDigger.Text = "PokéDigger";
            this.Tools_PokeDigger.UseVisualStyleBackColor = true;
            this.Tools_PokeDigger.Click += new System.EventHandler(this.Tools_PokeDigger_Click);
            // 
            // Tools_Filter
            // 
            this.Tools_Filter.Location = new System.Drawing.Point(193, 61);
            this.Tools_Filter.Name = "Tools_Filter";
            this.Tools_Filter.Size = new System.Drawing.Size(89, 23);
            this.Tools_Filter.TabIndex = 6;
            this.Tools_Filter.Text = "Filters";
            this.Tools_Filter.UseVisualStyleBackColor = true;
            this.Tools_Filter.Click += new System.EventHandler(this.Tools_Filter_Click);
            // 
            // EventHandlerButton
            // 
            this.EventHandlerButton.Location = new System.Drawing.Point(3, 90);
            this.EventHandlerButton.Name = "EventHandlerButton";
            this.EventHandlerButton.Size = new System.Drawing.Size(89, 23);
            this.EventHandlerButton.TabIndex = 8;
            this.EventHandlerButton.Text = "Event Handler";
            this.EventHandlerButton.UseVisualStyleBackColor = true;
            this.EventHandlerButton.Click += new System.EventHandler(this.Tool_EventHandler_Click);
            // 
            // Tab_Log
            // 
            this.Tab_Log.BackColor = System.Drawing.SystemColors.Control;
            this.Tab_Log.Controls.Add(this.Log_Export);
            this.Tab_Log.Controls.Add(this.txtLog);
            this.Tab_Log.Controls.Add(this.readResult);
            this.Tab_Log.Controls.Add(this.label71);
            this.Tab_Log.Location = new System.Drawing.Point(4, 22);
            this.Tab_Log.Name = "Tab_Log";
            this.Tab_Log.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_Log.Size = new System.Drawing.Size(292, 264);
            this.Tab_Log.TabIndex = 2;
            this.Tab_Log.Text = "Log";
            // 
            // Log_Export
            // 
            this.Log_Export.Location = new System.Drawing.Point(194, 236);
            this.Log_Export.Name = "Log_Export";
            this.Log_Export.Size = new System.Drawing.Size(92, 23);
            this.Log_Export.TabIndex = 2;
            this.Log_Export.Text = "Export Log";
            this.Log_Export.UseVisualStyleBackColor = true;
            this.Log_Export.Click += new System.EventHandler(this.Log_Export_Click);
            // 
            // Tab_About
            // 
            this.Tab_About.BackColor = System.Drawing.SystemColors.Control;
            this.Tab_About.Controls.Add(this.tableLayoutPanel2);
            this.Tab_About.Location = new System.Drawing.Point(4, 22);
            this.Tab_About.Margin = new System.Windows.Forms.Padding(6);
            this.Tab_About.Name = "Tab_About";
            this.Tab_About.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_About.Size = new System.Drawing.Size(292, 264);
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 11;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(286, 230);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Game Data";
            // 
            // lb_name
            // 
            this.lb_name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_name.AutoSize = true;
            this.lb_name.Location = new System.Drawing.Point(79, 23);
            this.lb_name.Name = "lb_name";
            this.lb_name.Size = new System.Drawing.Size(204, 13);
            this.lb_name.TabIndex = 0;
            this.lb_name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Name:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Version:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "TID:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "SID:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "G7ID:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 123);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "TSV:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_version
            // 
            this.lb_version.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_version.AutoSize = true;
            this.lb_version.Location = new System.Drawing.Point(79, 43);
            this.lb_version.Name = "lb_version";
            this.lb_version.Size = new System.Drawing.Size(204, 13);
            this.lb_version.TabIndex = 0;
            this.lb_version.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_tid
            // 
            this.lb_tid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_tid.AutoSize = true;
            this.lb_tid.Location = new System.Drawing.Point(79, 63);
            this.lb_tid.Name = "lb_tid";
            this.lb_tid.Size = new System.Drawing.Size(204, 13);
            this.lb_tid.TabIndex = 0;
            this.lb_tid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_sid
            // 
            this.lb_sid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_sid.AutoSize = true;
            this.lb_sid.Location = new System.Drawing.Point(79, 83);
            this.lb_sid.Name = "lb_sid";
            this.lb_sid.Size = new System.Drawing.Size(204, 13);
            this.lb_sid.TabIndex = 0;
            this.lb_sid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_g7id
            // 
            this.lb_g7id.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_g7id.AutoSize = true;
            this.lb_g7id.Location = new System.Drawing.Point(79, 103);
            this.lb_g7id.Name = "lb_g7id";
            this.lb_g7id.Size = new System.Drawing.Size(204, 13);
            this.lb_g7id.TabIndex = 0;
            this.lb_g7id.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_tsv
            // 
            this.lb_tsv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_tsv.AutoSize = true;
            this.lb_tsv.Location = new System.Drawing.Point(79, 123);
            this.lb_tsv.Name = "lb_tsv";
            this.lb_tsv.Size = new System.Drawing.Size(204, 13);
            this.lb_tsv.TabIndex = 0;
            this.lb_tsv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 173);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Version:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label13.Location = new System.Drawing.Point(3, 157);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "PKMN-NTR";
            // 
            // lb_pkmnntrver
            // 
            this.lb_pkmnntrver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_pkmnntrver.AutoSize = true;
            this.lb_pkmnntrver.Location = new System.Drawing.Point(79, 173);
            this.lb_pkmnntrver.Name = "lb_pkmnntrver";
            this.lb_pkmnntrver.Size = new System.Drawing.Size(204, 13);
            this.lb_pkmnntrver.TabIndex = 4;
            this.lb_pkmnntrver.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lb_pkmnntrver.Click += new System.EventHandler(this.updateLabel_Click);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 193);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "PKHeX.Core:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 213);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Updates:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_pkhexcorever
            // 
            this.lb_pkhexcorever.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_pkhexcorever.AutoSize = true;
            this.lb_pkhexcorever.Location = new System.Drawing.Point(79, 193);
            this.lb_pkhexcorever.Name = "lb_pkhexcorever";
            this.lb_pkhexcorever.Size = new System.Drawing.Size(204, 13);
            this.lb_pkhexcorever.TabIndex = 4;
            this.lb_pkhexcorever.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lb_pkhexcorever.Click += new System.EventHandler(this.updateLabel_Click);
            // 
            // PB_Legal
            // 
            this.PB_Legal.Image = ((System.Drawing.Image)(resources.GetObject("PB_Legal.Image")));
            this.PB_Legal.Location = new System.Drawing.Point(529, 12);
            this.PB_Legal.Name = "PB_Legal";
            this.PB_Legal.Size = new System.Drawing.Size(16, 16);
            this.PB_Legal.TabIndex = 102;
            this.PB_Legal.TabStop = false;
            this.PB_Legal.Click += new System.EventHandler(this.clickLegality);
            // 
            // dragout
            // 
            this.dragout.Location = new System.Drawing.Point(551, 12);
            this.dragout.Name = "dragout";
            this.dragout.Size = new System.Drawing.Size(40, 30);
            this.dragout.TabIndex = 18;
            this.dragout.TabStop = false;
            this.dragout.DragDrop += new System.Windows.Forms.DragEventHandler(this.dragoutDrop);
            this.dragout.DragOver += new System.Windows.Forms.DragEventHandler(this.dragout_DragOver);
            this.dragout.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dragout_MouseDown);
            this.dragout.MouseEnter += new System.EventHandler(this.dragoutEnter);
            this.dragout.MouseLeave += new System.EventHandler(this.dragoutLeave);
            // 
            // StopPollingButton
            // 
            this.StopPollingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.StopPollingButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.StopPollingButton.Enabled = false;
            this.StopPollingButton.Location = new System.Drawing.Point(9, 235);
            this.StopPollingButton.Name = "StopPollingButton";
            this.StopPollingButton.Size = new System.Drawing.Size(91, 23);
            this.StopPollingButton.TabIndex = 15;
            this.StopPollingButton.Text = "Stop Polling";
            this.StopPollingButton.UseVisualStyleBackColor = true;
            this.StopPollingButton.Click += new System.EventHandler(this.StopPollingButton_Click);
            // 
            // EventPollingWorker
            // 
            this.EventPollingWorker.WorkerReportsProgress = true;
            this.EventPollingWorker.WorkerSupportsCancellation = true;
            this.EventPollingWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.EventPollingWorker_DoWork);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(613, 368);
            this.Controls.Add(this.Tabs_General);
            this.Controls.Add(this.PB_Legal);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dragout);
            this.Controls.Add(this.tabMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 6, 6);
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
            this.tabMain.ResumeLayout(false);
            this.Tab_Main.ResumeLayout(false);
            this.FLP_Main.ResumeLayout(false);
            this.FLP_PID.ResumeLayout(false);
            this.FLP_PIDLeft.ResumeLayout(false);
            this.FLP_PIDLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Label_IsShiny)).EndInit();
            this.FLP_PIDRight.ResumeLayout(false);
            this.FLP_PIDRight.PerformLayout();
            this.FLP_Species.ResumeLayout(false);
            this.FLP_Nickname.ResumeLayout(false);
            this.FLP_Nickname.PerformLayout();
            this.FLP_NicknameLeft.ResumeLayout(false);
            this.FLP_NicknameLeft.PerformLayout();
            this.FLP_EXPLevel.ResumeLayout(false);
            this.FLP_EXPLevelRight.ResumeLayout(false);
            this.FLP_EXPLevelRight.PerformLayout();
            this.FLP_Nature.ResumeLayout(false);
            this.FLP_HeldItem.ResumeLayout(false);
            this.FLP_FriendshipForm.ResumeLayout(false);
            this.FLP_FriendshipFormLeft.ResumeLayout(false);
            this.FLP_FriendshipFormRight.ResumeLayout(false);
            this.FLP_FriendshipFormRight.PerformLayout();
            this.FLP_Ability.ResumeLayout(false);
            this.FLP_AbilityRight.ResumeLayout(false);
            this.FLP_AbilityRight.PerformLayout();
            this.FLP_Language.ResumeLayout(false);
            this.FLP_EggPKRS.ResumeLayout(false);
            this.FLP_EggPKRSLeft.ResumeLayout(false);
            this.FLP_EggPKRSLeft.PerformLayout();
            this.FLP_EggPKRSRight.ResumeLayout(false);
            this.FLP_EggPKRSRight.PerformLayout();
            this.FLP_PKRS.ResumeLayout(false);
            this.FLP_PKRSRight.ResumeLayout(false);
            this.FLP_Country.ResumeLayout(false);
            this.FLP_SubRegion.ResumeLayout(false);
            this.FLP_3DSRegion.ResumeLayout(false);
            this.Tab_Met.ResumeLayout(false);
            this.Tab_Met.PerformLayout();
            this.GB_EggConditions.ResumeLayout(false);
            this.FLP_Met.ResumeLayout(false);
            this.FLP_OriginGame.ResumeLayout(false);
            this.FLP_MetLocation.ResumeLayout(false);
            this.FLP_Ball.ResumeLayout(false);
            this.FLP_BallLeft.ResumeLayout(false);
            this.FLP_BallLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Ball)).EndInit();
            this.FLP_MetLevel.ResumeLayout(false);
            this.FLP_MetLevel.PerformLayout();
            this.FLP_MetDate.ResumeLayout(false);
            this.FLP_Fateful.ResumeLayout(false);
            this.FLP_Fateful.PerformLayout();
            this.FLP_EncounterType.ResumeLayout(false);
            this.FLP_TimeOfDay.ResumeLayout(false);
            this.Tab_Stats.ResumeLayout(false);
            this.PAN_Contest.ResumeLayout(false);
            this.PAN_Contest.PerformLayout();
            this.FLP_Stats.ResumeLayout(false);
            this.FLP_StatHeader.ResumeLayout(false);
            this.FLP_StatsHeaderRight.ResumeLayout(false);
            this.FLP_HP.ResumeLayout(false);
            this.FLP_HPRight.ResumeLayout(false);
            this.FLP_HPRight.PerformLayout();
            this.FLP_Atk.ResumeLayout(false);
            this.FLP_AtkRight.ResumeLayout(false);
            this.FLP_AtkRight.PerformLayout();
            this.FLP_Def.ResumeLayout(false);
            this.FLP_DefRight.ResumeLayout(false);
            this.FLP_DefRight.PerformLayout();
            this.FLP_SpA.ResumeLayout(false);
            this.FLP_SpALeft.ResumeLayout(false);
            this.FLP_SpARight.ResumeLayout(false);
            this.FLP_SpARight.PerformLayout();
            this.FLP_SpD.ResumeLayout(false);
            this.FLP_SpDRight.ResumeLayout(false);
            this.FLP_SpDRight.PerformLayout();
            this.FLP_Spe.ResumeLayout(false);
            this.FLP_SpeRight.ResumeLayout(false);
            this.FLP_SpeRight.PerformLayout();
            this.FLP_StatsTotal.ResumeLayout(false);
            this.FLP_StatsTotalRight.ResumeLayout(false);
            this.FLP_StatsTotalRight.PerformLayout();
            this.FLP_HPType.ResumeLayout(false);
            this.FLP_Characteristic.ResumeLayout(false);
            this.Tab_Attacks.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PB_WarnMove4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_WarnMove3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_WarnMove2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_WarnMove1)).EndInit();
            this.GB_RelearnMoves.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PB_WarnRelearn4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_WarnRelearn3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_WarnRelearn2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_WarnRelearn1)).EndInit();
            this.GB_CurrentMoves.ResumeLayout(false);
            this.GB_CurrentMoves.PerformLayout();
            this.Tab_OTMisc.ResumeLayout(false);
            this.Tab_OTMisc.PerformLayout();
            this.GB_Markings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PB_MarkHorohoro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_MarkVC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_MarkAlola)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Mark6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_MarkPentagon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Mark3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Mark5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_MarkCured)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Mark2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_MarkShiny)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Mark1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Mark4)).EndInit();
            this.FLP_PKMEditors.ResumeLayout(false);
            this.FLP_PKMEditors.PerformLayout();
            this.GB_nOT.ResumeLayout(false);
            this.GB_nOT.PerformLayout();
            this.GB_ExtraBytes.ResumeLayout(false);
            this.GB_ExtraBytes.PerformLayout();
            this.GB_OT.ResumeLayout(false);
            this.GB_OT.PerformLayout();
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
            ((System.ComponentModel.ISupportInitialize)(this.PB_Legal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dragout)).EndInit();
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
        private System.Windows.Forms.PictureBox dragout;
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
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage Tab_Main;
        private System.Windows.Forms.FlowLayoutPanel FLP_Main;
        private System.Windows.Forms.FlowLayoutPanel FLP_PID;
        private System.Windows.Forms.FlowLayoutPanel FLP_PIDLeft;
        private System.Windows.Forms.Label Label_PID;
        private System.Windows.Forms.Button BTN_Shinytize;
        private System.Windows.Forms.PictureBox Label_IsShiny;
        private System.Windows.Forms.FlowLayoutPanel FLP_PIDRight;
        private System.Windows.Forms.TextBox TB_PID;
        private System.Windows.Forms.Label Label_Gender;
        private System.Windows.Forms.Button BTN_RerollPID;
        private System.Windows.Forms.FlowLayoutPanel FLP_Species;
        private System.Windows.Forms.Label Label_Species;
        public System.Windows.Forms.ComboBox CB_Species;
        private System.Windows.Forms.FlowLayoutPanel FLP_Nickname;
        private System.Windows.Forms.FlowLayoutPanel FLP_NicknameLeft;
        private System.Windows.Forms.CheckBox CHK_Nicknamed;
        public System.Windows.Forms.TextBox TB_Nickname;
        private System.Windows.Forms.FlowLayoutPanel FLP_EXPLevel;
        private System.Windows.Forms.Label Label_EXP;
        private System.Windows.Forms.FlowLayoutPanel FLP_EXPLevelRight;
        private System.Windows.Forms.MaskedTextBox TB_EXP;
        private System.Windows.Forms.Label Label_CurLevel;
        private System.Windows.Forms.MaskedTextBox TB_Level;
        private System.Windows.Forms.MaskedTextBox MT_Level;
        private System.Windows.Forms.FlowLayoutPanel FLP_Nature;
        private System.Windows.Forms.Label Label_Nature;
        private System.Windows.Forms.ComboBox CB_Nature;
        private System.Windows.Forms.FlowLayoutPanel FLP_HeldItem;
        private System.Windows.Forms.Label Label_HeldItem;
        private System.Windows.Forms.ComboBox CB_HeldItem;
        private System.Windows.Forms.FlowLayoutPanel FLP_FriendshipForm;
        private System.Windows.Forms.FlowLayoutPanel FLP_FriendshipFormLeft;
        public System.Windows.Forms.Label Label_Friendship;
        public System.Windows.Forms.Label Label_HatchCounter;
        private System.Windows.Forms.FlowLayoutPanel FLP_FriendshipFormRight;
        private System.Windows.Forms.MaskedTextBox TB_Friendship;
        private System.Windows.Forms.Label Label_Form;
        private System.Windows.Forms.ComboBox CB_Form;
        private System.Windows.Forms.FlowLayoutPanel FLP_Ability;
        private System.Windows.Forms.Label Label_Ability;
        private System.Windows.Forms.FlowLayoutPanel FLP_AbilityRight;
        private System.Windows.Forms.ComboBox CB_Ability;
        private System.Windows.Forms.ComboBox DEV_Ability;
        private System.Windows.Forms.MaskedTextBox TB_AbilityNumber;
        private System.Windows.Forms.FlowLayoutPanel FLP_Language;
        private System.Windows.Forms.Label Label_Language;
        private System.Windows.Forms.ComboBox CB_Language;
        private System.Windows.Forms.FlowLayoutPanel FLP_EggPKRS;
        private System.Windows.Forms.FlowLayoutPanel FLP_EggPKRSLeft;
        public System.Windows.Forms.CheckBox CHK_IsEgg;
        private System.Windows.Forms.FlowLayoutPanel FLP_EggPKRSRight;
        private System.Windows.Forms.CheckBox CHK_Infected;
        private System.Windows.Forms.CheckBox CHK_Cured;
        private System.Windows.Forms.FlowLayoutPanel FLP_PKRS;
        private System.Windows.Forms.Label Label_PKRS;
        private System.Windows.Forms.FlowLayoutPanel FLP_PKRSRight;
        private System.Windows.Forms.ComboBox CB_PKRSStrain;
        private System.Windows.Forms.Label Label_PKRSdays;
        private System.Windows.Forms.ComboBox CB_PKRSDays;
        private System.Windows.Forms.FlowLayoutPanel FLP_Country;
        private System.Windows.Forms.Label Label_Country;
        private System.Windows.Forms.ComboBox CB_Country;
        private System.Windows.Forms.FlowLayoutPanel FLP_SubRegion;
        private System.Windows.Forms.Label Label_SubRegion;
        private System.Windows.Forms.ComboBox CB_SubRegion;
        private System.Windows.Forms.FlowLayoutPanel FLP_3DSRegion;
        private System.Windows.Forms.Label Label_3DSRegion;
        private System.Windows.Forms.ComboBox CB_3DSReg;
        private System.Windows.Forms.TabPage Tab_Met;
        private System.Windows.Forms.CheckBox CHK_AsEgg;
        private System.Windows.Forms.GroupBox GB_EggConditions;
        private System.Windows.Forms.ComboBox CB_EggLocation;
        private System.Windows.Forms.DateTimePicker CAL_EggDate;
        private System.Windows.Forms.Label Label_EggDate;
        private System.Windows.Forms.Label Label_EggLocation;
        private System.Windows.Forms.FlowLayoutPanel FLP_Met;
        private System.Windows.Forms.FlowLayoutPanel FLP_OriginGame;
        private System.Windows.Forms.Label Label_OriginGame;
        private System.Windows.Forms.ComboBox CB_GameOrigin;
        private System.Windows.Forms.FlowLayoutPanel FLP_MetLocation;
        private System.Windows.Forms.Label Label_MetLocation;
        public System.Windows.Forms.ComboBox CB_MetLocation;
        private System.Windows.Forms.FlowLayoutPanel FLP_Ball;
        private System.Windows.Forms.FlowLayoutPanel FLP_BallLeft;
        private System.Windows.Forms.Label Label_Ball;
        private System.Windows.Forms.PictureBox PB_Ball;
        public System.Windows.Forms.ComboBox CB_Ball;
        private System.Windows.Forms.FlowLayoutPanel FLP_MetLevel;
        private System.Windows.Forms.Label Label_MetLevel;
        private System.Windows.Forms.MaskedTextBox TB_MetLevel;
        private System.Windows.Forms.FlowLayoutPanel FLP_MetDate;
        private System.Windows.Forms.Label Label_MetDate;
        private System.Windows.Forms.DateTimePicker CAL_MetDate;
        private System.Windows.Forms.FlowLayoutPanel FLP_Fateful;
        private System.Windows.Forms.Panel PAN_Fateful;
        private System.Windows.Forms.CheckBox CHK_Fateful;
        private System.Windows.Forms.FlowLayoutPanel FLP_EncounterType;
        private System.Windows.Forms.Label Label_EncounterType;
        private System.Windows.Forms.ComboBox CB_EncounterType;
        private System.Windows.Forms.FlowLayoutPanel FLP_TimeOfDay;
        private System.Windows.Forms.Label L_MetTimeOfDay;
        public System.Windows.Forms.ComboBox CB_MetTimeOfDay;
        private System.Windows.Forms.TabPage Tab_Stats;
        private System.Windows.Forms.Panel PAN_Contest;
        private System.Windows.Forms.MaskedTextBox TB_Sheen;
        private System.Windows.Forms.MaskedTextBox TB_Tough;
        private System.Windows.Forms.MaskedTextBox TB_Smart;
        private System.Windows.Forms.MaskedTextBox TB_Cute;
        private System.Windows.Forms.MaskedTextBox TB_Beauty;
        private System.Windows.Forms.MaskedTextBox TB_Cool;
        private System.Windows.Forms.Label Label_Sheen;
        private System.Windows.Forms.Label Label_Tough;
        private System.Windows.Forms.Label Label_Smart;
        private System.Windows.Forms.Label Label_Cute;
        private System.Windows.Forms.Label Label_Beauty;
        private System.Windows.Forms.Label Label_Cool;
        private System.Windows.Forms.Label Label_ContestStats;
        private System.Windows.Forms.FlowLayoutPanel FLP_Stats;
        private System.Windows.Forms.FlowLayoutPanel FLP_StatHeader;
        private System.Windows.Forms.FlowLayoutPanel FLP_HackedStats;
        private System.Windows.Forms.FlowLayoutPanel FLP_StatsHeaderRight;
        private System.Windows.Forms.Label Label_IVs;
        private System.Windows.Forms.Label Label_EVs;
        private System.Windows.Forms.Label Label_Stats;
        private System.Windows.Forms.FlowLayoutPanel FLP_HP;
        private System.Windows.Forms.Label Label_HP;
        private System.Windows.Forms.FlowLayoutPanel FLP_HPRight;
        private System.Windows.Forms.MaskedTextBox TB_HPIV;
        private System.Windows.Forms.MaskedTextBox TB_HPEV;
        private System.Windows.Forms.MaskedTextBox Stat_HP;
        private System.Windows.Forms.FlowLayoutPanel FLP_Atk;
        private System.Windows.Forms.Label Label_ATK;
        private System.Windows.Forms.FlowLayoutPanel FLP_AtkRight;
        private System.Windows.Forms.MaskedTextBox TB_ATKIV;
        private System.Windows.Forms.MaskedTextBox TB_ATKEV;
        private System.Windows.Forms.MaskedTextBox Stat_ATK;
        private System.Windows.Forms.FlowLayoutPanel FLP_Def;
        private System.Windows.Forms.Label Label_DEF;
        private System.Windows.Forms.FlowLayoutPanel FLP_DefRight;
        private System.Windows.Forms.MaskedTextBox TB_DEFIV;
        private System.Windows.Forms.MaskedTextBox TB_DEFEV;
        private System.Windows.Forms.MaskedTextBox Stat_DEF;
        private System.Windows.Forms.FlowLayoutPanel FLP_SpA;
        private System.Windows.Forms.FlowLayoutPanel FLP_SpALeft;
        private System.Windows.Forms.Label Label_SPA;
        private System.Windows.Forms.Label Label_SPC;
        private System.Windows.Forms.FlowLayoutPanel FLP_SpARight;
        private System.Windows.Forms.MaskedTextBox TB_SPAIV;
        private System.Windows.Forms.MaskedTextBox TB_SPAEV;
        private System.Windows.Forms.MaskedTextBox Stat_SPA;
        private System.Windows.Forms.FlowLayoutPanel FLP_SpD;
        private System.Windows.Forms.Label Label_SPD;
        private System.Windows.Forms.FlowLayoutPanel FLP_SpDRight;
        private System.Windows.Forms.MaskedTextBox TB_SPDIV;
        private System.Windows.Forms.MaskedTextBox TB_SPDEV;
        private System.Windows.Forms.MaskedTextBox Stat_SPD;
        private System.Windows.Forms.FlowLayoutPanel FLP_Spe;
        private System.Windows.Forms.Label Label_SPE;
        private System.Windows.Forms.FlowLayoutPanel FLP_SpeRight;
        private System.Windows.Forms.MaskedTextBox TB_SPEIV;
        private System.Windows.Forms.MaskedTextBox TB_SPEEV;
        private System.Windows.Forms.MaskedTextBox Stat_SPE;
        private System.Windows.Forms.FlowLayoutPanel FLP_StatsTotal;
        private System.Windows.Forms.Label Label_Total;
        private System.Windows.Forms.FlowLayoutPanel FLP_StatsTotalRight;
        private System.Windows.Forms.TextBox TB_IVTotal;
        private System.Windows.Forms.TextBox TB_EVTotal;
        private System.Windows.Forms.Label L_Potential;
        private System.Windows.Forms.FlowLayoutPanel FLP_HPType;
        private System.Windows.Forms.Label Label_HiddenPowerPrefix;
        private System.Windows.Forms.ComboBox CB_HPType;
        private System.Windows.Forms.FlowLayoutPanel FLP_Characteristic;
        private System.Windows.Forms.Label Label_CharacteristicPrefix;
        private System.Windows.Forms.Label L_Characteristic;
        private System.Windows.Forms.Button BTN_RandomEVs;
        private System.Windows.Forms.Button BTN_RandomIVs;
        private System.Windows.Forms.TabPage Tab_Attacks;
        private System.Windows.Forms.PictureBox PB_WarnMove4;
        private System.Windows.Forms.PictureBox PB_WarnMove3;
        private System.Windows.Forms.PictureBox PB_WarnMove2;
        private System.Windows.Forms.PictureBox PB_WarnMove1;
        private System.Windows.Forms.GroupBox GB_RelearnMoves;
        private System.Windows.Forms.PictureBox PB_WarnRelearn4;
        private System.Windows.Forms.PictureBox PB_WarnRelearn3;
        private System.Windows.Forms.PictureBox PB_WarnRelearn2;
        private System.Windows.Forms.PictureBox PB_WarnRelearn1;
        private System.Windows.Forms.ComboBox CB_RelearnMove4;
        private System.Windows.Forms.ComboBox CB_RelearnMove3;
        private System.Windows.Forms.ComboBox CB_RelearnMove2;
        private System.Windows.Forms.ComboBox CB_RelearnMove1;
        private System.Windows.Forms.GroupBox GB_CurrentMoves;
        private System.Windows.Forms.MaskedTextBox TB_PP4;
        private System.Windows.Forms.MaskedTextBox TB_PP3;
        private System.Windows.Forms.MaskedTextBox TB_PP2;
        private System.Windows.Forms.MaskedTextBox TB_PP1;
        private System.Windows.Forms.Label Label_CurPP;
        private System.Windows.Forms.Label Label_PPups;
        private System.Windows.Forms.ComboBox CB_PPu4;
        private System.Windows.Forms.ComboBox CB_PPu3;
        private System.Windows.Forms.ComboBox CB_PPu2;
        private System.Windows.Forms.ComboBox CB_Move4;
        private System.Windows.Forms.ComboBox CB_PPu1;
        private System.Windows.Forms.ComboBox CB_Move3;
        private System.Windows.Forms.ComboBox CB_Move2;
        private System.Windows.Forms.ComboBox CB_Move1;
        private System.Windows.Forms.TabPage Tab_OTMisc;
        private System.Windows.Forms.FlowLayoutPanel FLP_PKMEditors;
        private System.Windows.Forms.Button BTN_Ribbons;
        private System.Windows.Forms.Button BTN_Medals;
        private System.Windows.Forms.Button BTN_History;
        private System.Windows.Forms.TextBox TB_EC;
        public System.Windows.Forms.GroupBox GB_nOT;
        private System.Windows.Forms.Label Label_CTGender;
        private System.Windows.Forms.TextBox TB_OTt2;
        private System.Windows.Forms.Label Label_PrevOT;
        private System.Windows.Forms.Button BTN_RerollEC;
        private System.Windows.Forms.GroupBox GB_ExtraBytes;
        private System.Windows.Forms.MaskedTextBox TB_ExtraByte;
        private System.Windows.Forms.ComboBox CB_ExtraBytes;
        public System.Windows.Forms.GroupBox GB_OT;
        private System.Windows.Forms.Label Label_OTGender;
        private System.Windows.Forms.TextBox TB_OT;
        private System.Windows.Forms.MaskedTextBox TB_SID;
        private System.Windows.Forms.MaskedTextBox TB_TID;
        private System.Windows.Forms.Label Label_OT;
        private System.Windows.Forms.Label Label_SID;
        private System.Windows.Forms.Label Label_TID;
        private System.Windows.Forms.Label Label_EncryptionConstant;
        private System.Windows.Forms.PictureBox PB_Legal;
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
        private System.Windows.Forms.GroupBox GB_Markings;
        private System.Windows.Forms.PictureBox PB_MarkHorohoro;
        private System.Windows.Forms.PictureBox PB_MarkVC;
        private System.Windows.Forms.PictureBox PB_MarkAlola;
        private System.Windows.Forms.PictureBox PB_Mark6;
        private System.Windows.Forms.PictureBox PB_MarkPentagon;
        private System.Windows.Forms.PictureBox PB_Mark3;
        private System.Windows.Forms.PictureBox PB_Mark5;
        private System.Windows.Forms.PictureBox PB_MarkCured;
        private System.Windows.Forms.PictureBox PB_Mark2;
        private System.Windows.Forms.PictureBox PB_MarkShiny;
        private System.Windows.Forms.PictureBox PB_Mark1;
        private System.Windows.Forms.PictureBox PB_Mark4;
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
        private System.Windows.Forms.Button StartPollingButton;
        private System.Windows.Forms.Button StopPollingButton;
    }
}

