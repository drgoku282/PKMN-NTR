/// I do not own the code used for pokémon editing in this class. 
/// All rights and credits for that code in this class belong to Kaphotics.
/// Code was taken from PKHeX https://github.com/kwsch/PKHeX with minor modifications
/// 

using pkmn_ntr.Bot;
using pkmn_ntr.Helpers;
using pkmn_ntr.Properties;
using pkmn_ntr.Sub_forms;
using Octokit;
using PKHeX.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using pkmn_ntr.Sub_forms.Scripting;

namespace pkmn_ntr
{
    public partial class MainForm : Form
    {
        #region Class variables

        //A "waiting room", where functions wait for data to be acquired. Entries are indexed by their sequence number. Once a request with a given sequence number is fulfilled, handleDataReady() uses information in DataReadyWaiting object to process the data.
        public static Dictionary<uint, DataReadyWaiting> waitingForData = new Dictionary<uint, DataReadyWaiting>();

        // Set this boolean to true to enable the write feature for the party pokémon.
        public readonly bool enablepartywrite = false;

        // Set this boolean to true to enable the write feature for illegal pokémon
        public readonly bool enableillegal = false;

        // Structure for box/slot last position
        struct LastBoxSlot
        {
            public decimal box { get; set; }
            public decimal slot { get; set; }
        }

        // New program-wide varialbes for PKHeX.Core
        public SaveFile SAV = SaveUtil.getBlankSAV(GameVersion.MN, "PKMN-NTR");
        public PKM pkm;
        private const string pkhexlang = "en";
        public static string[] gendersymbols = { "♂", "♀", "-" };
        public byte[] fileinfo;
        public byte[] iteminfo;
        private static GameVersion origintrack;
        private Action getFieldsfromPKM;
        private Func<PKM> getPKMfromFields;
        private LegalityAnalysis Legality;
        public static volatile bool formInitialized, fieldsInitialized, fieldsLoaded;
        private bool changingFields;
        public bool isConnected = false;
        private readonly PictureBox[] movePB, relearnPB;
        private readonly ToolTip Tip1 = new ToolTip(), Tip2 = new ToolTip(), Tip3 = new ToolTip(), NatureTip = new ToolTip(), EVTip = new ToolTip();
        private static readonly Image mixedHighlight = ImageUtil.ChangeOpacity(Resources.slotSet, 0.5);
        byte[] oppdata;
        public static string MGDatabasePath => Path.Combine(System.Windows.Forms.Application.StartupPath, "mgdb");

        // Program constants
        public uint BOXES;
        public int MAXSPECIES;
        public const int BOXSIZE = 30;
        public const int POKEBYTES = 232;
        public const string FOLDERPOKE = "Pokemon";
        public const string FOLDERDELETE = "Deleted";
        public const string FOLDERWT = "Wonder Trade";
        public string PKXEXT;
        public string BOXEXT;

        // Variables for update checking
        internal GitHubClient Github;
        private string updateURL = null;

        //Game information
        public int pid;
        public byte lang;
        public string pname;
        //Offsets for Pokemon sources
        public uint tradeOff;
        public uint opponentOff;
        public uint partyOff;
        public uint boxOff;
        public uint daycare1Off;
        public uint daycare2Off;
        public uint daycare3Off; // Battle Resort Daycare
        public uint daycare4Off; // Battle Resort Daycare
        public uint battleBoxOff;

        // Log handling
        public delegate void LogDelegate(string l);
        public LogDelegate delAddLog;

        // Bot variables
        public bool botWorking;
        public string lastlog;

        #endregion Class variables

        #region Main window

        public MainForm()
        {
            Program.ntrClient.DataReady += handleDataReady;
            Program.ntrClient.Connected += connectCheck;
            Program.ntrClient.InfoReady += getGame;
            delAddLog = new LogDelegate(Addlog);
            InitializeComponent();

            pkm = SAV.BlankPKM;
            setPKMFormatMode(SAV.Generation, SAV.Version);

            relearnPB = new[] { PB_WarnRelearn1, PB_WarnRelearn2, PB_WarnRelearn3, PB_WarnRelearn4 };
            movePB = new[] { PB_WarnMove1, PB_WarnMove2, PB_WarnMove3, PB_WarnMove4 };
            Label_Species.ResetForeColor();
            new ToolTip().SetToolTip(dragout, "PKM QuickSave");
            dragout.GiveFeedback += (sender, e) => { e.UseDefaultCursors = false; };
            GiveFeedback += (sender, e) => { e.UseDefaultCursors = false; };
            foreach (TabPage tab in tabMain.TabPages)
            {
                tab.AllowDrop = true;
                tab.DragDrop += tabMain_DragDrop;
                tab.DragEnter += tabMain_DragEnter;
            }

            GB_OT.Click += clickGT;
            GB_nOT.Click += clickGT;
            GB_CurrentMoves.Click += clickMoves;
            GB_RelearnMoves.Click += clickMoves;

            TB_Nickname.Font = FontUtil.getPKXFont(11);
            TB_OT.Font = (Font)TB_Nickname.Font.Clone();
            TB_OTt2.Font = (Font)TB_Nickname.Font.Clone();

            dragout.AllowDrop = true;

            // Load Event Databases
            refreshMGDB();

            PKM pk = SAV.getPKM((fieldsInitialized ? preparePKM() : pkm).Data);
            bool alreadyInit = fieldsInitialized;
            fieldsInitialized = false;
            InitializeStrings();
            InitializeLanguage();
            populateFields(pk); // put data back in form
            fieldsInitialized |= alreadyInit;
            InitializeFields();

            disableControls();
            formInitialized = true;
        }

        private void InitializeStrings()
        {
            GameInfo.Strings = GameInfo.getStrings("en");

            // Update Legality Strings
            // Clipboard.SetText(string.Join(Environment.NewLine, Util.getLocalization(typeof(LegalityCheckStrings))));
            Task.Run(() => Util.setLocalization(typeof(LegalityCheckStrings)));


            // Force an update to the met locations
            origintrack = GameVersion.Unknown;

            // Update Legality Analysis strings
            LegalityAnalysis.movelist = GameInfo.Strings.movelist;
            LegalityAnalysis.specieslist = GameInfo.Strings.specieslist;

            if (fieldsInitialized)
                updateIVs(null, null); // Prompt an update for the characteristics
        }

        private void InitializeLanguage()
        {
            ComboBox[] cbs =
            {
                CB_Country, CB_SubRegion, CB_3DSReg, CB_Language, CB_Ball, CB_HeldItem, CB_Species, DEV_Ability,
                CB_Nature, CB_EncounterType, CB_GameOrigin, CB_HPType
            };
            foreach (var cb in cbs) { cb.DisplayMember = "Text"; cb.ValueMember = "Value"; }

            // Set the various ComboBox DataSources up with their allowed entries
            setCountrySubRegion(CB_Country, "countries");
            CB_3DSReg.DataSource = Util.getUnsortedCBList("regions3ds");

            GameInfo.InitializeDataSources(GameInfo.Strings);

            CB_EncounterType.DataSource = Util.getCBList(GameInfo.Strings.encountertypelist, new[] { 0 }, Legal.Gen4EncounterTypes);
            CB_HPType.DataSource = Util.getCBList(GameInfo.Strings.types.Skip(1).Take(16).ToArray(), null);
            CB_Nature.DataSource = new BindingSource(GameInfo.NatureDataSource, null);

            populateFilteredDataSources();
        }

        private void populateFilteredDataSources()
        {
            GameInfo.setItemDataSource(false, SAV.MaxItemID, SAV.HeldItems, SAV.Generation, SAV.Version, GameInfo.Strings);
            if (SAV.Generation > 1)
                CB_HeldItem.DataSource = new BindingSource(GameInfo.ItemDataSource.Where(i => i.Value <= SAV.MaxItemID).ToList(), null);

            var languages = Util.getUnsortedCBList("languages");
            if (SAV.Generation < 7)
                languages = languages.Where(l => l.Value <= 8).ToList(); // Korean
            CB_Language.DataSource = languages;

            CB_Ball.DataSource = new BindingSource(GameInfo.BallDataSource.Where(b => b.Value <= SAV.MaxBallID).ToList(), null);
            CB_Species.DataSource = new BindingSource(GameInfo.SpeciesDataSource.Where(s => s.Value <= SAV.MaxSpeciesID).ToList(), null);
            DEV_Ability.DataSource = new BindingSource(GameInfo.AbilityDataSource.Where(a => a.Value <= SAV.MaxAbilityID).ToList(), null);
            CB_GameOrigin.DataSource = new BindingSource(GameInfo.VersionDataSource.Where(g => g.Value <= SAV.MaxGameID || SAV.Generation >= 3 && g.Value == 15).ToList(), null);

            // Set the Move ComboBoxes too..
            GameInfo.MoveDataSource = (GameInfo.LegalMoveDataSource).Where(m => m.Value <= SAV.MaxMoveID).ToList(); // Filter Z-Moves if appropriate
            foreach (ComboBox cb in new[] { CB_Move1, CB_Move2, CB_Move3, CB_Move4, CB_RelearnMove1, CB_RelearnMove2, CB_RelearnMove3, CB_RelearnMove4 })
            {
                cb.DisplayMember = "Text"; cb.ValueMember = "Value";
                cb.DataSource = new BindingSource(GameInfo.MoveDataSource, null);
            }
        }

        private static void refreshMGDB()
        {
            var g4 = getPCDDB(PKHeX.Core.Properties.Resources.pcd);
            var g5 = getPGFDB(PKHeX.Core.Properties.Resources.pgf);
            var g6 = getWC6DB(PKHeX.Core.Properties.Resources.wc6, PKHeX.Core.Properties.Resources.wc6full);
            var g7 = getWC7DB(PKHeX.Core.Properties.Resources.wc7, PKHeX.Core.Properties.Resources.wc7full);

            if (Directory.Exists(MGDatabasePath))
                foreach (var file in Directory.GetFiles(MGDatabasePath, "*", SearchOption.AllDirectories))
                {
                    var fi = new FileInfo(file);
                    if (!MysteryGift.getIsMysteryGift(fi.Length))
                        continue;

                    var gift = MysteryGift.getMysteryGift(File.ReadAllBytes(file), fi.Extension);
                    switch (gift?.Format)
                    {
                        case 4: g4.Add(gift); continue;
                        case 5: g5.Add(gift); continue;
                        case 6: g6.Add(gift); continue;
                        case 7: g7.Add(gift); continue;
                    }
                }

            Legal.MGDB_G4 = g4.ToArray();
            Legal.MGDB_G5 = g5.ToArray();
            Legal.MGDB_G6 = g6.ToArray();
            Legal.MGDB_G7 = g7.ToArray();
        }
        private static HashSet<MysteryGift> getPCDDB(byte[] bin)
        {
            var db = new HashSet<MysteryGift>();
            for (int i = 0; i < bin.Length; i += PCD.Size)
            {
                byte[] data = new byte[PCD.Size];
                Buffer.BlockCopy(bin, i, data, 0, PCD.Size);
                db.Add(new PCD(data));
            }
            return db;
        }
        private static HashSet<MysteryGift> getPGFDB(byte[] bin)
        {
            var db = new HashSet<MysteryGift>();
            for (int i = 0; i < bin.Length; i += PGF.Size)
            {
                byte[] data = new byte[PGF.Size];
                Buffer.BlockCopy(bin, i, data, 0, PGF.Size);
                db.Add(new PGF(data));
            }
            return db;
        }
        private static HashSet<MysteryGift> getWC6DB(byte[] wc6bin, byte[] wc6full)
        {
            var db = new HashSet<MysteryGift>();
            for (int i = 0; i < wc6bin.Length; i += WC6.Size)
            {
                byte[] data = new byte[WC6.Size];
                Buffer.BlockCopy(wc6bin, i, data, 0, WC6.Size);
                db.Add(new WC6(data));
            }
            for (int i = 0; i < wc6full.Length; i += WC6.SizeFull)
            {
                byte[] data = new byte[WC6.SizeFull];
                Buffer.BlockCopy(wc6full, i, data, 0, WC6.SizeFull);
                db.Add(new WC6(data));
            }
            return db;
        }
        private static HashSet<MysteryGift> getWC7DB(byte[] wc7bin, byte[] wc7full)
        {
            var db = new HashSet<MysteryGift>();
            for (int i = 0; i < wc7bin.Length; i += WC7.Size)
            {
                byte[] data = new byte[WC7.Size];
                Buffer.BlockCopy(wc7bin, i, data, 0, WC7.Size);
                db.Add(new WC7(data));
            }
            for (int i = 0; i < wc7full.Length; i += WC7.SizeFull)
            {
                byte[] data = new byte[WC7.SizeFull];
                Buffer.BlockCopy(wc7full, i, data, 0, WC7.SizeFull);
                db.Add(new WC7(data));
            }
            return db;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lb_pkmnntrver.Text = System.Windows.Forms.Application.ProductVersion;
            lb_pkhexcorever.Text = PKHeX.Core.Properties.Resources.ProgramVersion;

            checkUpdate();
            host.Text = Settings.Default.IP;
            callIP();
            host.Focus();
        }

        [Conditional("DEBUG")]
        private void callIP()
        {
            StreamReader sr = new StreamReader(@System.Windows.Forms.Application.StartupPath + "\\IP.txt");
            host.Text = sr.ReadLine();
            sr.Close();
        }

        [Conditional("DEBUG")]
        private void saveIP()
        {
            File.WriteAllText(@System.Windows.Forms.Application.StartupPath + "\\IP.txt", host.Text);
        }

        private async void checkUpdate()
        {
            try
            {
                addtoLog("GUI: Look for updates");
                // Get current             
                addtoLog("GUI: Current version: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);

                // Get latest stable
                Github = new GitHubClient(new ProductHeaderValue("PKMN-NTR-UpdateCheck"));
                Release lateststable = await Github.Repository.Release.GetLatest("drgoku282", "PKMN-NTR");
                int[] verlatest = Array.ConvertAll(lateststable.TagName.Split('.'), int.Parse);
                addtoLog("GUI: Last stable: " + lateststable.TagName);

                // Look for latest stable
                if (checkversions(verlatest))
                {
                    addtoLog("GUI: Update found!");
                    Delg.SetText(lb_update, "Version " + lateststable.TagName + " is available.");
                    updateURL = lateststable.HtmlUrl;
                    DialogResult result = MessageBox.Show("Version " + lateststable.TagName + " is available.\r\nDo you want to go to GitHub and download it?", "Update Available", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        Process.Start(updateURL);
                    }
                }
                else
                { // Look for beta
                    IReadOnlyList<Release> releases = await Github.Repository.Release.GetAll("drgoku282", "PKMN-NTR");
                    Release latestbeta = releases.FirstOrDefault(rel => rel.Prerelease);
                    if (latestbeta != null)
                    {
                        addtoLog("GUI: Last preview: " + latestbeta.TagName);
                        int[] verbeta = Array.ConvertAll(latestbeta.TagName.Split('.'), int.Parse);
                        if (checkversions(verbeta))
                        {
                            addtoLog("GUI: New preview version found");
                            Delg.SetText(lb_update, "Preview version " + latestbeta.TagName + " is available.");
                            updateURL = latestbeta.HtmlUrl;
                        }
                        else
                        {
                            addtoLog("GUI: PKMN-NTR is up to date");
                            Delg.SetText(lb_update, "PKMN-NTR is up to date.");
                            updateURL = null;
                        }
                    }
                    else
                    {
                        addtoLog("GUI: PKMN-NTR is up to date");
                        Delg.SetText(lb_update, "PKMN-NTR is up to date.");
                        updateURL = null;
                    }
                }
            }
            catch (Exception ex)
            {
                updateURL = null;
                addtoLog("GUI: An error has ocurred while checking for updates:");
                addtoLog(ex.Message);
                Delg.SetText(lb_update, "Update not found.");
            }
        }

        private void updateLabel_Click(object sender, EventArgs e)
        {
            if (updateURL != null)
            {
                Process.Start(updateURL);
            }
        }

        private bool checkversions(int[] tag)
        {
            int major = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major;
            int minor = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor;
            int build = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build;

            if (tag[0] > major)
            {
                return true;
            }

            if (tag[0] == major && tag[1] > minor)
            {
                return true;
            }

            if (tag[0] == major && tag[1] == minor && tag[2] > build)
            {
                return true;
            }

            return false;
        }

        private void enableControls()
        {
            foreach (TabPage tab in Tabs_General.TabPages)
            {
                Delg.SetEnabled(tab, true);
            }
            Delg.SetEnabled(Tool_Trainer, true);
            Delg.SetEnabled(Tool_Items, true);
            Delg.SetEnabled(Tool_Controls, true);
            Delg.SetEnabled(Tools_Breeding, true);
            Delg.SetEnabled(Tools_SoftReset, true);
            Delg.SetEnabled(Tools_WonderTrade, true);
            Delg.SetEnabled(Tools_PokeDigger, true);
            Delg.SetEnabled(resetNoBox, true);
            foreach (TabPage tab in tabMain.TabPages)
            {
                Delg.SetEnabled(tab, true);
            }
        }

        private void disableControls()
        {
            foreach (TabPage tab in Tabs_General.TabPages)
            {
                if (!(tab.Name == "Tab_Log" || tab.Name == "Tab_About" || tab.Name == "Tab_Tools"))
                {
                    Delg.SetEnabled(tab, false);
                }
            }
            Delg.SetEnabled(Tool_Trainer, false);
            Delg.SetEnabled(Tool_Items, false);
            Delg.SetEnabled(Tool_Controls, false);
            Delg.SetEnabled(Tools_Breeding, false);
            Delg.SetEnabled(Tools_SoftReset, false);
            Delg.SetEnabled(Tools_WonderTrade, false);
            Delg.SetEnabled(Tools_PokeDigger, false);
            Delg.SetEnabled(resetNoBox, false);
            foreach (TabPage tab in tabMain.TabPages)
            {
                Delg.SetEnabled(tab, false);
            }
        }

        public void Addlog(string l)
        {
            lastlog = l;
            if (l.Contains("Server disconnected") && !botWorking)
            {
                PerformDisconnect();
            }
            if (l.Contains("finished") && botWorking) // Supress "finished" messages on bots
            {
                l = l.Replace("finished", null);
            }
            if (!l.Contains("\r\n") && l.Length > 2)
            {
                l = l.Replace("\n", "\r\n");
            }
            if (!l.EndsWith("\n") && l.Length > 2)
            {
                l += "\r\n";
            }
            txtLog.AppendText(l);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                Program.ntrClient.sendHeartbeatPacket();
            }
            catch (Exception)
            {

            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.ntrClient.disconnect();
        }

        public void startAutoDisconnect()
        {
            disconnectTimer.Enabled = true;
        }

        private void disconnectTimer_Tick(object sender, EventArgs e)
        {
            disconnectTimer.Enabled = false;
            Program.ntrClient.disconnect();
        }

        static void handleDataReady(object sender, DataReadyEventArgs e)
        { // We move data processing to a separate thread. This way even if processing takes a long time, the netcode doesn't hang.
            DataReadyWaiting args;
            if (waitingForData.TryGetValue(e.seq, out args))
            {
                Array.Copy(e.data, args.data, Math.Min(e.data.Length, args.data.Length));
                Thread t = new Thread(new ParameterizedThreadStart(args.handler));
                t.Start(args);
                waitingForData.Remove(e.seq);
            }
        }

        public void addtoLog(string msg)
        {
            Program.gCmdWindow.BeginInvoke(Program.gCmdWindow.delAddLog, msg);
        }

        public void addwaitingForData(uint newkey, DataReadyWaiting newvalue)
        {
            waitingForData.Add(newkey, newvalue);
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            //Some people leave the default IP address, hoping it would work...
            if (host.Text == "0.0.0.0")
            {
                MessageBox.Show("Please input your console's local IP address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            txtLog.Clear();
            Program.scriptHelper.connect(host.Text, 8000);
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            PerformDisconnect();
        }

        public void PerformDisconnect()
        {
            Program.scriptHelper.disconnect();
            buttonConnect.Text = "Connect";
            buttonConnect.Enabled = true;
            buttonDisconnect.Enabled = false;
            isConnected = false;
            disableControls();
        }

        private void connectCheck(object sender, EventArgs e)
        {
            Program.scriptHelper.listprocess();
            buttonConnect.Text = "Connected";
            buttonConnect.Enabled = false;
            buttonDisconnect.Enabled = true;
            isConnected = true;
            Settings.Default.IP = host.Text;
            Settings.Default.Save();
            saveIP();
        }

        //This functions handles additional information events from NTR netcode. We are only interested in them if they are a process list, containing our game's PID and game type.
        delegate void getGameDelegate(object sender, EventArgs e);

        public void getGame(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                BeginInvoke(new getGameDelegate(getGame), sender, e);
                return;
            }

            InfoReadyEventArgs args = (InfoReadyEventArgs)e;
            if (args.info.Contains("kujira-1")) // X
            {
                string log = args.info;
                pname = ", pname: kujira-1";
                string splitlog = log.Substring(log.IndexOf(pname) - 8, log.Length - log.IndexOf(pname));
                pid = Convert.ToInt32("0x" + splitlog.Substring(0, 8), 16);
                SAV = SaveUtil.getBlankSAV(GameVersion.X, "PKMN-NTR");
                boxOff = 0x8C861C8;
                daycare1Off = 0x8C7FF4C;
                daycare2Off = 0x8C8003C;
                tradeOff = 0x8500000;
                battleBoxOff = 0x8C6AC2C;
                partyOff = 0x8CE1CF8;
                opponentOff = 0x8800000;
            }
            else if (args.info.Contains("kujira-2")) // Y
            {
                string log = args.info;
                pname = ", pname: kujira-2";
                string splitlog = log.Substring(log.IndexOf(pname) - 8, log.Length - log.IndexOf(pname));
                pid = Convert.ToInt32("0x" + splitlog.Substring(0, 8), 16);
                SAV = SaveUtil.getBlankSAV(GameVersion.Y, "PKMN-NTR");
                boxOff = 0x8C861C8;
                daycare1Off = 0x8C7FF4C;
                daycare2Off = 0x8C8003C;
                tradeOff = 0x8500000;
                battleBoxOff = 0x8C6AC2C;
                partyOff = 0x8CE1CF8;
                opponentOff = 0x8800000;
            }
            else if (args.info.Contains("sango-1")) // Omega Ruby
            {
                string log = args.info;
                pname = ", pname:  sango-1";
                string splitlog = log.Substring(log.IndexOf(pname) - 8, log.Length - log.IndexOf(pname));
                pid = Convert.ToInt32("0x" + splitlog.Substring(0, 8), 16);
                SAV = SaveUtil.getBlankSAV(GameVersion.OR, "PKMN-NTR");
                boxOff = 0x8C9E134;
                daycare1Off = 0x8C88180;
                daycare2Off = 0x8C88270;
                daycare3Off = 0x8C88370;
                daycare4Off = 0x8C88460;
                tradeOff = 0x8520000;
                battleBoxOff = 0x8C72330;
                partyOff = 0x8CFB26C;
                opponentOff = 0x8800000;
            }
            else if (args.info.Contains("sango-2")) // Alpha Sapphire
            {
                string log = args.info;
                pname = ", pname:  sango-2";
                string splitlog = log.Substring(log.IndexOf(pname) - 8, log.Length - log.IndexOf(pname));
                pid = Convert.ToInt32("0x" + splitlog.Substring(0, 8), 16);
                SAV = SaveUtil.getBlankSAV(GameVersion.AS, "PKMN-NTR");
                boxOff = 0x8C9E134;
                daycare1Off = 0x8C88180;
                daycare2Off = 0x8C88270;
                daycare3Off = 0x8C88370;
                daycare4Off = 0x8C88460;
                tradeOff = 0x8520000;
                battleBoxOff = 0x8C72330;
                partyOff = 0x8CFB26C;
                opponentOff = 0x8800000;
            }
            else if (args.info.Contains("niji_loc")) // Sun/Moon
            {
                string log = args.info;
                pname = ", pname: niji_loc";
                string splitlog = log.Substring(log.IndexOf(pname) - 8, log.Length - log.IndexOf(pname));
                pid = Convert.ToInt32("0x" + splitlog.Substring(0, 8), 16);
                SAV = SaveUtil.getBlankSAV(GameVersion.SN, "PKMN-NTR");
                boxOff = 0x330D9838;
                daycare1Off = 0x3313EC01;
                daycare2Off = 0x3313ECEA;
                tradeOff = 0x32A870C8;
                opponentOff = 0x3254F4AC;
                partyOff = 0x34195E10;
            }
            else // not a process list or game not found - ignore packet
            {
                return;
            }

            // Clear tabs to avoid writting wrong data
            if (!botWorking)
            {
                setPKMFormatMode(SAV.Generation, SAV.Version);
                pkm = SAV.BlankPKM;
                bool init = fieldsInitialized;
                fieldsInitialized = fieldsLoaded = false;
                populateFilteredDataSources();
                InitializeFields();
                fieldsInitialized |= init;

                MAXSPECIES = SAV.MaxSpeciesID;

                if (SAV.Generation == 7)
                {
                    PKXEXT = ".pk7";
                    BOXEXT = ".ek7";
                    BOXES = 32;
                    fillGen7();
                    dumpAllData7();
                }
                else if (SAV.Generation == 6)
                {
                    PKXEXT = ".pk6";
                    BOXEXT = ".ek6";
                    BOXES = 31;
                    fillGen6();
                    dumpAllData6();
                }
                enableControls();
            }

            // Fill fields in the form according to gen
            Program.helper.pid = pid;
        }

        private void fillGen6()
        {
            Delg.SetEnabled(radioBattleBox, true);
            Delg.SetEnabled(Write_PKM, true);
            Delg.SetCheckedRadio(radioBoxes, true);
            Delg.SetText(radioDaycare, "Daycare");
            Delg.SetMaximum(boxDump, BOXES);
            Delg.SetMaximum(Num_CDBox, BOXES);
            Delg.SetMaximum(Num_CDAmount, LookupTable.getMaxSpace((int)Num_CDBox.Value, (int)Num_CDSlot.Value));
        }

        private async void fillGen7()
        {
            Delg.SetEnabled(radioBattleBox, false);
            Delg.SetEnabled(Write_PKM, true);
            Delg.SetCheckedRadio(radioBoxes, true);
            Delg.SetText(radioDaycare, "Nursery");
            Delg.SetMaximum(boxDump, BOXES);
            Delg.SetMaximum(Num_CDBox, BOXES);
            Delg.SetMaximum(Num_CDAmount, LookupTable.getMaxSpace((int)Num_CDBox.Value, (int)Num_CDSlot.Value));

            //Apply connection patch
            Task<bool> Patch = Program.helper.waitNTRwrite(LookupTable.nfcOff, LookupTable.nfcVal, pid);
            if (!(await Patch))
            {
                MessageBox.Show("An error has ocurred while applying the connection patch.", "PKMN-NTR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeTabs()
        {
            bool alreadyInit = fieldsInitialized;
            fieldsInitialized = false;

            GameInfo.Strings = GameInfo.getStrings(pkhexlang);

            // Force an update to the met locations
            origintrack = GameVersion.Unknown;

            // Update Legality Analysis strings
            LegalityAnalysis.movelist = GameInfo.Strings.movelist;

            if (fieldsInitialized)
                updateIVs(null, null); // Prompt an update for the characteristics

            ComboBox[] cbs =
            {
                CB_Country, CB_SubRegion, CB_3DSReg, CB_Language, CB_Ball, CB_HeldItem, CB_Species, DEV_Ability,
                CB_Nature, CB_EncounterType, CB_GameOrigin, CB_HPType
            };
            foreach (var cb in cbs)
            {
                cb.DisplayMember = "Text";
                cb.ValueMember = "Value";
            }

            // Set the various ComboBox DataSources up with their allowed entries
            setCountrySubRegion(CB_Country, "countries");
            CB_3DSReg.DataSource = Util.getUnsortedCBList("regions3ds");

            GameInfo.InitializeDataSources(GameInfo.Strings);

            CB_EncounterType.DataSource = Util.getCBList(GameInfo.Strings.encountertypelist, new[] { 0 }, Legal.Gen4EncounterTypes);
            CB_HPType.DataSource = Util.getCBList(GameInfo.Strings.types.Skip(1).Take(16).ToArray(), null);
            CB_Nature.DataSource = new BindingSource(GameInfo.NatureDataSource, null);

            GameInfo.setItemDataSource(false, SAV.MaxItemID, SAV.HeldItems, SAV.Generation, SAV.Version, GameInfo.Strings);
            if (SAV.Generation > 1)
                CB_HeldItem.DataSource = new BindingSource(GameInfo.ItemDataSource.Where(i => i.Value <= SAV.MaxItemID).ToList(), null);

            var languages = Util.getUnsortedCBList("languages");
            if (SAV.Generation < 7)
                languages = languages.Where(l => l.Value <= 8).ToList(); // Korean
            CB_Language.DataSource = languages;

            CB_Ball.DataSource = new BindingSource(GameInfo.BallDataSource.Where(b => b.Value <= SAV.MaxBallID).ToList(), null);
            CB_Species.DataSource = new BindingSource(GameInfo.SpeciesDataSource.Where(s => s.Value <= SAV.MaxSpeciesID).ToList(), null);
            DEV_Ability.DataSource = new BindingSource(GameInfo.AbilityDataSource.Where(a => a.Value <= SAV.MaxAbilityID).ToList(), null);
            CB_GameOrigin.DataSource = new BindingSource(GameInfo.VersionDataSource.Where(g => g.Value <= SAV.MaxGameID || SAV.Generation >= 3 && g.Value == 15).ToList(), null);

            // Set the Move ComboBoxes too..
            var moves = GameInfo.MoveDataSource.Where(m => m.Value <= SAV.MaxMoveID).ToList(); // Filter Z-Moves if appropriate
            foreach (ComboBox cb in new[] { CB_Move1, CB_Move2, CB_Move3, CB_Move4, CB_RelearnMove1, CB_RelearnMove2, CB_RelearnMove3, CB_RelearnMove4 })
            {
                cb.DisplayMember = "Text"; cb.ValueMember = "Value";
                cb.DataSource = new BindingSource(moves, null);
            }

            fieldsInitialized |= alreadyInit;
        }

        private void setPKMFormatMode(int Format, GameVersion version)
        {
            byte[] extraBytes = new byte[0];
            switch (Format)
            {
                case 6:
                    getFieldsfromPKM = populateFieldsPK6;
                    getPKMfromFields = preparePK6;
                    extraBytes = PK6.ExtraBytes;
                    break;
                case 7:
                    getFieldsfromPKM = populateFieldsPK7;
                    getPKMfromFields = preparePK7;
                    extraBytes = PK7.ExtraBytes;
                    break;
            }
            // Load Extra Byte List
            GB_ExtraBytes.Visible = GB_ExtraBytes.Enabled = extraBytes.Length != 0;
            if (GB_ExtraBytes.Enabled)
            {
                CB_ExtraBytes.Items.Clear();
                foreach (byte b in extraBytes)
                    CB_ExtraBytes.Items.Add("0x" + b.ToString("X2"));
                CB_ExtraBytes.SelectedIndex = 0;
            }
        }

        private void populateFieldsPK6()
        {
            PK6 pk6 = pkm as PK6;
            if (pk6 == null)
                return;

            // Do first
            pk6.Stat_Level = PKX.getLevel(pk6.Species, pk6.EXP);
            if (pk6.Stat_Level == 100)
                pk6.EXP = PKX.getEXP(pk6.Stat_Level, pk6.Species);

            CB_Species.SelectedValue = pk6.Species;
            TB_Level.Text = pk6.Stat_Level.ToString();
            TB_EXP.Text = pk6.EXP.ToString();

            // Load rest
            TB_EC.Text = pk6.EncryptionConstant.ToString("X8");
            CHK_Fateful.Checked = pk6.FatefulEncounter;
            CHK_IsEgg.Checked = pk6.IsEgg;
            CHK_Nicknamed.Checked = pk6.IsNicknamed;
            Label_OTGender.Text = gendersymbols[pk6.OT_Gender];
            Label_OTGender.ForeColor = pk6.OT_Gender == 1 ? Color.Red : Color.Blue;
            TB_PID.Text = pk6.PID.ToString("X8");
            CB_HeldItem.SelectedValue = pk6.HeldItem;
            TB_AbilityNumber.Text = pk6.AbilityNumber.ToString();
            CB_Ability.SelectedIndex = pk6.AbilityNumber < 6 ? pk6.AbilityNumber >> 1 : 0; // with some simple error handling
            CB_Nature.SelectedValue = pk6.Nature;
            TB_TID.Text = pk6.TID.ToString("00000");
            TB_SID.Text = pk6.SID.ToString("00000");
            TB_Nickname.Text = pk6.Nickname;
            TB_OT.Text = pk6.OT_Name;
            TB_OTt2.Text = pk6.HT_Name;
            TB_Friendship.Text = pk6.CurrentFriendship.ToString();
            if (pk6.CurrentHandler == 1)  // HT
            {
                GB_nOT.BackgroundImage = mixedHighlight;
                GB_OT.BackgroundImage = null;
            }
            else                  // = 0
            {
                GB_OT.BackgroundImage = mixedHighlight;
                GB_nOT.BackgroundImage = null;
            }
            CB_Language.SelectedValue = pk6.Language;
            CB_Country.SelectedValue = pk6.Country;
            CB_SubRegion.SelectedValue = pk6.Region;
            CB_3DSReg.SelectedValue = pk6.ConsoleRegion;
            CB_GameOrigin.SelectedValue = pk6.Version;
            CB_EncounterType.SelectedValue = pk6.Gen4 ? pk6.EncounterType : 0;
            CB_Ball.SelectedValue = pk6.Ball;

            CAL_MetDate.Value = pk6.MetDate ?? new DateTime(2000, 1, 1);

            if (pk6.Egg_Location != 0)
            {
                // Was obtained initially as an egg.
                CHK_AsEgg.Checked = true;
                GB_EggConditions.Enabled = true;

                CB_EggLocation.SelectedValue = pk6.Egg_Location;
                CAL_EggDate.Value = pk6.EggMetDate ?? new DateTime(2000, 1, 1);
            }
            else { CAL_EggDate.Value = new DateTime(2000, 01, 01); CHK_AsEgg.Checked = GB_EggConditions.Enabled = false; CB_EggLocation.SelectedValue = 0; }

            CB_MetLocation.SelectedValue = pk6.Met_Location;

            // Set CT Gender to None if no CT, else set to gender symbol.
            Label_CTGender.Text = pk6.HT_Name == "" ? "" : gendersymbols[pk6.HT_Gender % 2];
            Label_CTGender.ForeColor = pk6.HT_Gender == 1 ? Color.Red : Color.Blue;

            TB_MetLevel.Text = pk6.Met_Level.ToString();

            // Reset Label and ComboBox visibility, as well as non-data checked status.
            Label_PKRS.Visible = CB_PKRSStrain.Visible = CHK_Infected.Checked = pk6.PKRS_Strain != 0;
            Label_PKRSdays.Visible = CB_PKRSDays.Visible = pk6.PKRS_Days != 0;

            // Set SelectedIndexes for PKRS
            CB_PKRSStrain.SelectedIndex = pk6.PKRS_Strain;
            CHK_Cured.Checked = pk6.PKRS_Strain > 0 && pk6.PKRS_Days == 0;
            CB_PKRSDays.SelectedIndex = Math.Min(CB_PKRSDays.Items.Count - 1, pk6.PKRS_Days); // to strip out bad hacked 'rus

            TB_Cool.Text = pk6.CNT_Cool.ToString();
            TB_Beauty.Text = pk6.CNT_Beauty.ToString();
            TB_Cute.Text = pk6.CNT_Cute.ToString();
            TB_Smart.Text = pk6.CNT_Smart.ToString();
            TB_Tough.Text = pk6.CNT_Tough.ToString();
            TB_Sheen.Text = pk6.CNT_Sheen.ToString();

            TB_HPIV.Text = pk6.IV_HP.ToString();
            TB_ATKIV.Text = pk6.IV_ATK.ToString();
            TB_DEFIV.Text = pk6.IV_DEF.ToString();
            TB_SPEIV.Text = pk6.IV_SPE.ToString();
            TB_SPAIV.Text = pk6.IV_SPA.ToString();
            TB_SPDIV.Text = pk6.IV_SPD.ToString();
            CB_HPType.SelectedValue = pk6.HPType;

            TB_HPEV.Text = pk6.EV_HP.ToString();
            TB_ATKEV.Text = pk6.EV_ATK.ToString();
            TB_DEFEV.Text = pk6.EV_DEF.ToString();
            TB_SPEEV.Text = pk6.EV_SPE.ToString();
            TB_SPAEV.Text = pk6.EV_SPA.ToString();
            TB_SPDEV.Text = pk6.EV_SPD.ToString();

            CB_Move1.SelectedValue = pk6.Move1;
            CB_Move2.SelectedValue = pk6.Move2;
            CB_Move3.SelectedValue = pk6.Move3;
            CB_Move4.SelectedValue = pk6.Move4;
            CB_RelearnMove1.SelectedValue = pk6.RelearnMove1;
            CB_RelearnMove2.SelectedValue = pk6.RelearnMove2;
            CB_RelearnMove3.SelectedValue = pk6.RelearnMove3;
            CB_RelearnMove4.SelectedValue = pk6.RelearnMove4;
            CB_PPu1.SelectedIndex = pk6.Move1_PPUps;
            CB_PPu2.SelectedIndex = pk6.Move2_PPUps;
            CB_PPu3.SelectedIndex = pk6.Move3_PPUps;
            CB_PPu4.SelectedIndex = pk6.Move4_PPUps;
            TB_PP1.Text = pk6.Move1_PP.ToString();
            TB_PP2.Text = pk6.Move2_PP.ToString();
            TB_PP3.Text = pk6.Move3_PP.ToString();
            TB_PP4.Text = pk6.Move4_PP.ToString();

            // Set Form if count is enough, else cap.
            CB_Form.SelectedIndex = CB_Form.Items.Count > pk6.AltForm ? pk6.AltForm : CB_Form.Items.Count - 1;

            // Load Extrabyte Value
            TB_ExtraByte.Text = pk6.Data[Convert.ToInt32(CB_ExtraBytes.Text, 16)].ToString();

            updateStats();

            TB_EXP.Text = pk6.EXP.ToString();
            Label_Gender.Text = gendersymbols[pk6.Gender];
            Label_Gender.ForeColor = pk6.Gender == 2 ? Label_Species.ForeColor : (pk6.Gender == 1 ? Color.Red : Color.Blue);

            // Highlight the Current Handler
            clickGT(pk6.CurrentHandler == 1 ? GB_nOT : GB_OT, null);
        }

        private PKM preparePK6()
        {
            PK6 pk6 = pkm as PK6;
            if (pk6 == null)
                return null;

            // Repopulate PK6 with Edited Stuff
            if (WinFormsUtil.getIndex(CB_GameOrigin) < 24)
            {
                uint EC = Util.getHEXval(TB_EC.Text);
                uint PID = Util.getHEXval(TB_PID.Text);
                uint SID = Util.ToUInt32(TB_TID.Text);
                uint TID = Util.ToUInt32(TB_TID.Text);
                uint LID = PID & 0xFFFF;
                uint HID = PID >> 16;
                uint XOR = TID ^ LID ^ SID ^ HID;

                // Ensure we don't have a shiny.
                if (XOR >> 3 == 1) // Illegal, fix. (not 16<XOR>=8)
                {
                    // Keep as shiny, so we have to mod the PID
                    PID ^= XOR;
                    TB_PID.Text = PID.ToString("X8");
                    TB_EC.Text = PID.ToString("X8");
                }
                else if ((XOR ^ 0x8000) >> 3 == 1 && PID != EC)
                    TB_EC.Text = (PID ^ 0x80000000).ToString("X8");
                else // Not Illegal, no fix.
                    TB_EC.Text = PID.ToString("X8");
            }

            pk6.EncryptionConstant = Util.getHEXval(TB_EC.Text);
            pk6.Checksum = 0; // 0 CHK for now

            // Block A
            pk6.Species = WinFormsUtil.getIndex(CB_Species);
            pk6.HeldItem = WinFormsUtil.getIndex(CB_HeldItem);
            pk6.TID = Util.ToInt32(TB_TID.Text);
            pk6.SID = Util.ToInt32(TB_SID.Text);
            pk6.EXP = Util.ToUInt32(TB_EXP.Text);
            pk6.Ability = (byte)Array.IndexOf(GameInfo.Strings.abilitylist, CB_Ability.Text.Remove(CB_Ability.Text.Length - 4));
            pk6.AbilityNumber = Util.ToInt32(TB_AbilityNumber.Text);   // Number
            // pkx[0x16], pkx[0x17] are handled by the Medals UI (Hits & Training Bag)
            pk6.PID = Util.getHEXval(TB_PID.Text);
            pk6.Nature = (byte)WinFormsUtil.getIndex(CB_Nature);
            pk6.FatefulEncounter = CHK_Fateful.Checked;
            pk6.Gender = PKX.getGender(Label_Gender.Text);
            pk6.AltForm = (CB_Form.Enabled ? CB_Form.SelectedIndex : 0) & 0x1F;
            pk6.EV_HP = Util.ToInt32(TB_HPEV.Text);       // EVs
            pk6.EV_ATK = Util.ToInt32(TB_ATKEV.Text);
            pk6.EV_DEF = Util.ToInt32(TB_DEFEV.Text);
            pk6.EV_SPE = Util.ToInt32(TB_SPEEV.Text);
            pk6.EV_SPA = Util.ToInt32(TB_SPAEV.Text);
            pk6.EV_SPD = Util.ToInt32(TB_SPDEV.Text);

            pk6.CNT_Cool = Util.ToInt32(TB_Cool.Text);       // CNT
            pk6.CNT_Beauty = Util.ToInt32(TB_Beauty.Text);
            pk6.CNT_Cute = Util.ToInt32(TB_Cute.Text);
            pk6.CNT_Smart = Util.ToInt32(TB_Smart.Text);
            pk6.CNT_Tough = Util.ToInt32(TB_Tough.Text);
            pk6.CNT_Sheen = Util.ToInt32(TB_Sheen.Text);

            pk6.PKRS_Days = CB_PKRSDays.SelectedIndex;
            pk6.PKRS_Strain = CB_PKRSStrain.SelectedIndex;
            // Already in buff (then transferred to new pkx)
            // 0x2C, 0x2D, 0x2E, 0x2F
            // 0x30, 0x31, 0x32, 0x33
            // 0x34, 0x35, 0x36, 0x37
            // 0x38, 0x39

            // Unused
            // 0x3A, 0x3B
            // 0x3C, 0x3D, 0x3E, 0x3F

            // Block B
            // Convert Nickname field back to bytes
            pk6.Nickname = TB_Nickname.Text;
            pk6.Move1 = WinFormsUtil.getIndex(CB_Move1);
            pk6.Move2 = WinFormsUtil.getIndex(CB_Move2);
            pk6.Move3 = WinFormsUtil.getIndex(CB_Move3);
            pk6.Move4 = WinFormsUtil.getIndex(CB_Move4);
            pk6.Move1_PP = WinFormsUtil.getIndex(CB_Move1) > 0 ? Util.ToInt32(TB_PP1.Text) : 0;
            pk6.Move2_PP = WinFormsUtil.getIndex(CB_Move2) > 0 ? Util.ToInt32(TB_PP2.Text) : 0;
            pk6.Move3_PP = WinFormsUtil.getIndex(CB_Move3) > 0 ? Util.ToInt32(TB_PP3.Text) : 0;
            pk6.Move4_PP = WinFormsUtil.getIndex(CB_Move4) > 0 ? Util.ToInt32(TB_PP4.Text) : 0;
            pk6.Move1_PPUps = WinFormsUtil.getIndex(CB_Move1) > 0 ? CB_PPu1.SelectedIndex : 0;
            pk6.Move2_PPUps = WinFormsUtil.getIndex(CB_Move2) > 0 ? CB_PPu2.SelectedIndex : 0;
            pk6.Move3_PPUps = WinFormsUtil.getIndex(CB_Move3) > 0 ? CB_PPu3.SelectedIndex : 0;
            pk6.Move4_PPUps = WinFormsUtil.getIndex(CB_Move4) > 0 ? CB_PPu4.SelectedIndex : 0;
            pk6.RelearnMove1 = WinFormsUtil.getIndex(CB_RelearnMove1);
            pk6.RelearnMove2 = WinFormsUtil.getIndex(CB_RelearnMove2);
            pk6.RelearnMove3 = WinFormsUtil.getIndex(CB_RelearnMove3);
            pk6.RelearnMove4 = WinFormsUtil.getIndex(CB_RelearnMove4);
            // 0x72 - Ribbon editor sets this flag (Secret Super Training)
            // 0x73
            pk6.IV_HP = Util.ToInt32(TB_HPIV.Text);
            pk6.IV_ATK = Util.ToInt32(TB_ATKIV.Text);
            pk6.IV_DEF = Util.ToInt32(TB_DEFIV.Text);
            pk6.IV_SPE = Util.ToInt32(TB_SPEIV.Text);
            pk6.IV_SPA = Util.ToInt32(TB_SPAIV.Text);
            pk6.IV_SPD = Util.ToInt32(TB_SPDIV.Text);
            pk6.IsEgg = CHK_IsEgg.Checked;
            pk6.IsNicknamed = CHK_Nicknamed.Checked;

            // Block C
            pk6.HT_Name = TB_OTt2.Text;

            // 0x90-0xAF
            pk6.HT_Gender = PKX.getGender(Label_CTGender.Text) & 1;
            // Plus more, set by MemoryAmie (already in buff)

            // Block D
            pk6.OT_Name = TB_OT.Text;
            pk6.CurrentFriendship = Util.ToInt32(TB_Friendship.Text);

            DateTime? egg_date = null;
            int egg_location = 0;
            if (CHK_AsEgg.Checked)      // If encountered as an egg, load the Egg Met data from fields.
            {
                egg_date = CAL_EggDate.Value;
                egg_location = WinFormsUtil.getIndex(CB_EggLocation);
            }
            // Egg Met Data
            pk6.EggMetDate = egg_date;
            pk6.Egg_Location = egg_location;
            // Met Data
            pk6.MetDate = CAL_MetDate.Value;
            pk6.Met_Location = WinFormsUtil.getIndex(CB_MetLocation);

            if (pk6.IsEgg && pk6.Met_Location == 0)    // If still an egg, it has no hatch location/date. Zero it!
                pk6.MetDate = null;

            // 0xD7 Unknown

            pk6.Ball = WinFormsUtil.getIndex(CB_Ball);
            pk6.Met_Level = Util.ToInt32(TB_MetLevel.Text);
            pk6.OT_Gender = PKX.getGender(Label_OTGender.Text);
            pk6.EncounterType = WinFormsUtil.getIndex(CB_EncounterType);
            pk6.Version = WinFormsUtil.getIndex(CB_GameOrigin);
            pk6.Country = WinFormsUtil.getIndex(CB_Country);
            pk6.Region = WinFormsUtil.getIndex(CB_SubRegion);
            pk6.ConsoleRegion = WinFormsUtil.getIndex(CB_3DSReg);
            pk6.Language = WinFormsUtil.getIndex(CB_Language);
            // 0xE4-0xE7

            // Toss in Party Stats
            Array.Resize(ref pk6.Data, pk6.SIZE_PARTY);
            pk6.Stat_Level = Util.ToInt32(TB_Level.Text);
            pk6.Stat_HPCurrent = Util.ToInt32(Stat_HP.Text);
            pk6.Stat_HPMax = Util.ToInt32(Stat_HP.Text);
            pk6.Stat_ATK = Util.ToInt32(Stat_ATK.Text);
            pk6.Stat_DEF = Util.ToInt32(Stat_DEF.Text);
            pk6.Stat_SPE = Util.ToInt32(Stat_SPE.Text);
            pk6.Stat_SPA = Util.ToInt32(Stat_SPA.Text);
            pk6.Stat_SPD = Util.ToInt32(Stat_SPD.Text);

            // Unneeded Party Stats (Status, Flags, Unused)
            pk6.Data[0xE8] = pk6.Data[0xE9] = pk6.Data[0xEA] = pk6.Data[0xEB] =
                pk6.Data[0xED] = pk6.Data[0xEE] = pk6.Data[0xEF] =
                pk6.Data[0xFE] = pk6.Data[0xFF] = pk6.Data[0x100] =
                pk6.Data[0x101] = pk6.Data[0x102] = pk6.Data[0x103] = 0;

            // Fix Moves if a slot is empty 
            pk6.FixMoves();
            pk6.FixRelearn();
            pk6.FixMemories();

            // PKX is now filled
            pk6.RefreshChecksum();
            return pk6;
        }

        private void populateFieldsPK7()
        {
            PK7 pk7 = pkm as PK7;
            if (pk7 == null)
                return;

            // Do first
            pk7.Stat_Level = PKX.getLevel(pk7.Species, pk7.EXP);
            if (pk7.Stat_Level == 100)
                pk7.EXP = PKX.getEXP(pk7.Stat_Level, pk7.Species);

            CB_Species.SelectedValue = pk7.Species;
            TB_Level.Text = pk7.Stat_Level.ToString();
            TB_EXP.Text = pk7.EXP.ToString();

            // Load rest
            TB_EC.Text = pk7.EncryptionConstant.ToString("X8");
            CHK_Fateful.Checked = pk7.FatefulEncounter;
            CHK_IsEgg.Checked = pk7.IsEgg;
            CHK_Nicknamed.Checked = pk7.IsNicknamed;
            Label_OTGender.Text = gendersymbols[pk7.OT_Gender];
            Label_OTGender.ForeColor = pk7.OT_Gender == 1 ? Color.Red : Color.Blue;
            TB_PID.Text = pk7.PID.ToString("X8");
            CB_HeldItem.SelectedValue = pk7.HeldItem;
            TB_AbilityNumber.Text = pk7.AbilityNumber.ToString();
            CB_Ability.SelectedIndex = pk7.AbilityNumber < 6 ? pk7.AbilityNumber >> 1 : 0; // with some simple error handling
            CB_Nature.SelectedValue = pk7.Nature;
            TB_TID.Text = pk7.TID.ToString("00000");
            TB_SID.Text = pk7.SID.ToString("00000");
            TB_Nickname.Text = pk7.Nickname;
            TB_OT.Text = pk7.OT_Name;
            TB_OTt2.Text = pk7.HT_Name;
            TB_Friendship.Text = pk7.CurrentFriendship.ToString();
            if (pk7.CurrentHandler == 1)  // HT
            {
                GB_nOT.BackgroundImage = mixedHighlight;
                GB_OT.BackgroundImage = null;
            }
            else                  // = 0
            {
                GB_OT.BackgroundImage = mixedHighlight;
                GB_nOT.BackgroundImage = null;
            }
            CB_Language.SelectedValue = pk7.Language;
            CB_Country.SelectedValue = pk7.Country;
            CB_SubRegion.SelectedValue = pk7.Region;
            CB_3DSReg.SelectedValue = pk7.ConsoleRegion;
            CB_GameOrigin.SelectedValue = pk7.Version;
            CB_EncounterType.SelectedValue = pk7.Gen4 ? pk7.EncounterType : 0;
            CB_Ball.SelectedValue = pk7.Ball;

            CAL_MetDate.Value = pk7.MetDate ?? new DateTime(2000, 1, 1);

            if (pk7.Egg_Location != 0)
            {
                // Was obtained initially as an egg.
                CHK_AsEgg.Checked = true;
                GB_EggConditions.Enabled = true;

                CB_EggLocation.SelectedValue = pk7.Egg_Location;
                CAL_EggDate.Value = pk7.EggMetDate ?? new DateTime(2000, 1, 1);
            }
            else { CAL_EggDate.Value = new DateTime(2000, 01, 01); CHK_AsEgg.Checked = GB_EggConditions.Enabled = false; CB_EggLocation.SelectedValue = 0; }

            CB_MetLocation.SelectedValue = pk7.Met_Location;

            // Set CT Gender to None if no CT, else set to gender symbol.
            Label_CTGender.Text = pk7.HT_Name == "" ? "" : gendersymbols[pk7.HT_Gender % 2];
            Label_CTGender.ForeColor = pk7.HT_Gender == 1 ? Color.Red : Color.Blue;

            TB_MetLevel.Text = pk7.Met_Level.ToString();

            // Reset Label and ComboBox visibility, as well as non-data checked status.
            Label_PKRS.Visible = CB_PKRSStrain.Visible = CHK_Infected.Checked = pk7.PKRS_Strain != 0;
            Label_PKRSdays.Visible = CB_PKRSDays.Visible = pk7.PKRS_Days != 0;

            // Set SelectedIndexes for PKRS
            CB_PKRSStrain.SelectedIndex = pk7.PKRS_Strain;
            CHK_Cured.Checked = pk7.PKRS_Strain > 0 && pk7.PKRS_Days == 0;
            CB_PKRSDays.SelectedIndex = Math.Min(CB_PKRSDays.Items.Count - 1, pk7.PKRS_Days); // to strip out bad hacked 'rus

            TB_Cool.Text = pk7.CNT_Cool.ToString();
            TB_Beauty.Text = pk7.CNT_Beauty.ToString();
            TB_Cute.Text = pk7.CNT_Cute.ToString();
            TB_Smart.Text = pk7.CNT_Smart.ToString();
            TB_Tough.Text = pk7.CNT_Tough.ToString();
            TB_Sheen.Text = pk7.CNT_Sheen.ToString();

            TB_HPIV.Text = pk7.IV_HP.ToString();
            TB_ATKIV.Text = pk7.IV_ATK.ToString();
            TB_DEFIV.Text = pk7.IV_DEF.ToString();
            TB_SPEIV.Text = pk7.IV_SPE.ToString();
            TB_SPAIV.Text = pk7.IV_SPA.ToString();
            TB_SPDIV.Text = pk7.IV_SPD.ToString();
            CB_HPType.SelectedValue = pk7.HPType;

            TB_HPEV.Text = pk7.EV_HP.ToString();
            TB_ATKEV.Text = pk7.EV_ATK.ToString();
            TB_DEFEV.Text = pk7.EV_DEF.ToString();
            TB_SPEEV.Text = pk7.EV_SPE.ToString();
            TB_SPAEV.Text = pk7.EV_SPA.ToString();
            TB_SPDEV.Text = pk7.EV_SPD.ToString();

            CB_Move1.SelectedValue = pk7.Move1;
            CB_Move2.SelectedValue = pk7.Move2;
            CB_Move3.SelectedValue = pk7.Move3;
            CB_Move4.SelectedValue = pk7.Move4;
            CB_RelearnMove1.SelectedValue = pk7.RelearnMove1;
            CB_RelearnMove2.SelectedValue = pk7.RelearnMove2;
            CB_RelearnMove3.SelectedValue = pk7.RelearnMove3;
            CB_RelearnMove4.SelectedValue = pk7.RelearnMove4;
            CB_PPu1.SelectedIndex = pk7.Move1_PPUps;
            CB_PPu2.SelectedIndex = pk7.Move2_PPUps;
            CB_PPu3.SelectedIndex = pk7.Move3_PPUps;
            CB_PPu4.SelectedIndex = pk7.Move4_PPUps;
            TB_PP1.Text = pk7.Move1_PP.ToString();
            TB_PP2.Text = pk7.Move2_PP.ToString();
            TB_PP3.Text = pk7.Move3_PP.ToString();
            TB_PP4.Text = pk7.Move4_PP.ToString();

            // Set Form if count is enough, else cap.
            CB_Form.SelectedIndex = CB_Form.Items.Count > pk7.AltForm ? pk7.AltForm : CB_Form.Items.Count - 1;

            // Load Extrabyte Value
            TB_ExtraByte.Text = pk7.Data[Convert.ToInt32(CB_ExtraBytes.Text, 16)].ToString();

            updateStats();

            TB_EXP.Text = pk7.EXP.ToString();
            Label_Gender.Text = gendersymbols[pk7.Gender];
            Label_Gender.ForeColor = pk7.Gender == 2 ? Label_Species.ForeColor : (pk7.Gender == 1 ? Color.Red : Color.Blue);

            // Highlight the Current Handler
            clickGT(pk7.CurrentHandler == 1 ? GB_nOT : GB_OT, null);
        }

        private PKM preparePK7()
        {
            PK7 pk7 = pkm as PK7;
            if (pk7 == null)
                return null;

            // Repopulate PK6 with Edited Stuff
            if (WinFormsUtil.getIndex(CB_GameOrigin) < 24)
            {
                uint EC = Util.getHEXval(TB_EC.Text);
                uint PID = Util.getHEXval(TB_PID.Text);
                uint SID = Util.ToUInt32(TB_TID.Text);
                uint TID = Util.ToUInt32(TB_TID.Text);
                uint LID = PID & 0xFFFF;
                uint HID = PID >> 16;
                uint XOR = TID ^ LID ^ SID ^ HID;

                // Ensure we don't have a shiny.
                if (XOR >> 3 == 1) // Illegal, fix. (not 16<XOR>=8)
                {
                    // Keep as shiny, so we have to mod the PID
                    PID ^= XOR;
                    TB_PID.Text = PID.ToString("X8");
                    TB_EC.Text = PID.ToString("X8");
                }
                else if ((XOR ^ 0x8000) >> 3 == 1 && PID != EC)
                    TB_EC.Text = (PID ^ 0x80000000).ToString("X8");
                else // Not Illegal, no fix.
                    TB_EC.Text = PID.ToString("X8");
            }

            pk7.EncryptionConstant = Util.getHEXval(TB_EC.Text);
            pk7.Checksum = 0; // 0 CHK for now

            // Block A
            pk7.Species = WinFormsUtil.getIndex(CB_Species);
            pk7.HeldItem = WinFormsUtil.getIndex(CB_HeldItem);
            pk7.TID = Util.ToInt32(TB_TID.Text);
            pk7.SID = Util.ToInt32(TB_SID.Text);
            pk7.EXP = Util.ToUInt32(TB_EXP.Text);
            pk7.Ability = (byte)Array.IndexOf(GameInfo.Strings.abilitylist, CB_Ability.Text.Remove(CB_Ability.Text.Length - 4));
            pk7.AbilityNumber = Util.ToInt32(TB_AbilityNumber.Text);   // Number
            // pkx[0x16], pkx[0x17] are handled by the Medals UI (Hits & Training Bag)
            pk7.PID = Util.getHEXval(TB_PID.Text);
            pk7.Nature = (byte)WinFormsUtil.getIndex(CB_Nature);
            pk7.FatefulEncounter = CHK_Fateful.Checked;
            pk7.Gender = PKX.getGender(Label_Gender.Text);
            pk7.AltForm = (CB_Form.Enabled ? CB_Form.SelectedIndex : 0) & 0x1F;
            pk7.EV_HP = Util.ToInt32(TB_HPEV.Text);       // EVs
            pk7.EV_ATK = Util.ToInt32(TB_ATKEV.Text);
            pk7.EV_DEF = Util.ToInt32(TB_DEFEV.Text);
            pk7.EV_SPE = Util.ToInt32(TB_SPEEV.Text);
            pk7.EV_SPA = Util.ToInt32(TB_SPAEV.Text);
            pk7.EV_SPD = Util.ToInt32(TB_SPDEV.Text);

            pk7.CNT_Cool = Util.ToInt32(TB_Cool.Text);       // CNT
            pk7.CNT_Beauty = Util.ToInt32(TB_Beauty.Text);
            pk7.CNT_Cute = Util.ToInt32(TB_Cute.Text);
            pk7.CNT_Smart = Util.ToInt32(TB_Smart.Text);
            pk7.CNT_Tough = Util.ToInt32(TB_Tough.Text);
            pk7.CNT_Sheen = Util.ToInt32(TB_Sheen.Text);

            pk7.PKRS_Days = CB_PKRSDays.SelectedIndex;
            pk7.PKRS_Strain = CB_PKRSStrain.SelectedIndex;
            // Already in buff (then transferred to new pkx)
            // 0x2C, 0x2D, 0x2E, 0x2F
            // 0x30, 0x31, 0x32, 0x33
            // 0x34, 0x35, 0x36, 0x37
            // 0x38, 0x39

            // Unused
            // 0x3A, 0x3B
            // 0x3C, 0x3D, 0x3E, 0x3F

            // Block B
            // Convert Nickname field back to bytes
            pk7.Nickname = TB_Nickname.Text;
            pk7.Move1 = WinFormsUtil.getIndex(CB_Move1);
            pk7.Move2 = WinFormsUtil.getIndex(CB_Move2);
            pk7.Move3 = WinFormsUtil.getIndex(CB_Move3);
            pk7.Move4 = WinFormsUtil.getIndex(CB_Move4);
            pk7.Move1_PP = WinFormsUtil.getIndex(CB_Move1) > 0 ? Util.ToInt32(TB_PP1.Text) : 0;
            pk7.Move2_PP = WinFormsUtil.getIndex(CB_Move2) > 0 ? Util.ToInt32(TB_PP2.Text) : 0;
            pk7.Move3_PP = WinFormsUtil.getIndex(CB_Move3) > 0 ? Util.ToInt32(TB_PP3.Text) : 0;
            pk7.Move4_PP = WinFormsUtil.getIndex(CB_Move4) > 0 ? Util.ToInt32(TB_PP4.Text) : 0;
            pk7.Move1_PPUps = WinFormsUtil.getIndex(CB_Move1) > 0 ? CB_PPu1.SelectedIndex : 0;
            pk7.Move2_PPUps = WinFormsUtil.getIndex(CB_Move2) > 0 ? CB_PPu2.SelectedIndex : 0;
            pk7.Move3_PPUps = WinFormsUtil.getIndex(CB_Move3) > 0 ? CB_PPu3.SelectedIndex : 0;
            pk7.Move4_PPUps = WinFormsUtil.getIndex(CB_Move4) > 0 ? CB_PPu4.SelectedIndex : 0;
            pk7.RelearnMove1 = WinFormsUtil.getIndex(CB_RelearnMove1);
            pk7.RelearnMove2 = WinFormsUtil.getIndex(CB_RelearnMove2);
            pk7.RelearnMove3 = WinFormsUtil.getIndex(CB_RelearnMove3);
            pk7.RelearnMove4 = WinFormsUtil.getIndex(CB_RelearnMove4);
            // 0x72 - Ribbon editor sets this flag (Secret Super Training)
            // 0x73
            pk7.IV_HP = Util.ToInt32(TB_HPIV.Text);
            pk7.IV_ATK = Util.ToInt32(TB_ATKIV.Text);
            pk7.IV_DEF = Util.ToInt32(TB_DEFIV.Text);
            pk7.IV_SPE = Util.ToInt32(TB_SPEIV.Text);
            pk7.IV_SPA = Util.ToInt32(TB_SPAIV.Text);
            pk7.IV_SPD = Util.ToInt32(TB_SPDIV.Text);
            pk7.IsEgg = CHK_IsEgg.Checked;
            pk7.IsNicknamed = CHK_Nicknamed.Checked;

            // Block C
            pk7.HT_Name = TB_OTt2.Text;

            // 0x90-0xAF
            pk7.HT_Gender = PKX.getGender(Label_CTGender.Text) & 1;
            // Plus more, set by MemoryAmie (already in buff)

            // Block D
            pk7.OT_Name = TB_OT.Text;
            pk7.CurrentFriendship = Util.ToInt32(TB_Friendship.Text);

            DateTime? egg_date = null;
            int egg_location = 0;
            if (CHK_AsEgg.Checked)      // If encountered as an egg, load the Egg Met data from fields.
            {
                egg_date = CAL_EggDate.Value;
                egg_location = WinFormsUtil.getIndex(CB_EggLocation);
            }
            // Egg Met Data
            pk7.EggMetDate = egg_date;
            pk7.Egg_Location = egg_location;
            // Met Data
            pk7.MetDate = CAL_MetDate.Value;
            pk7.Met_Location = WinFormsUtil.getIndex(CB_MetLocation);

            if (pk7.IsEgg && pk7.Met_Location == 0)    // If still an egg, it has no hatch location/date. Zero it!
                pk7.MetDate = null;

            // 0xD7 Unknown

            pk7.Ball = WinFormsUtil.getIndex(CB_Ball);
            pk7.Met_Level = Util.ToInt32(TB_MetLevel.Text);
            pk7.OT_Gender = PKX.getGender(Label_OTGender.Text);
            pk7.EncounterType = WinFormsUtil.getIndex(CB_EncounterType);
            pk7.Version = WinFormsUtil.getIndex(CB_GameOrigin);
            pk7.Country = WinFormsUtil.getIndex(CB_Country);
            pk7.Region = WinFormsUtil.getIndex(CB_SubRegion);
            pk7.ConsoleRegion = WinFormsUtil.getIndex(CB_3DSReg);
            pk7.Language = WinFormsUtil.getIndex(CB_Language);
            // 0xE4-0xE7

            // Toss in Party Stats
            Array.Resize(ref pk7.Data, pk7.SIZE_PARTY);
            pk7.Stat_Level = Util.ToInt32(TB_Level.Text);
            pk7.Stat_HPCurrent = Util.ToInt32(Stat_HP.Text);
            pk7.Stat_HPMax = Util.ToInt32(Stat_HP.Text);
            pk7.Stat_ATK = Util.ToInt32(Stat_ATK.Text);
            pk7.Stat_DEF = Util.ToInt32(Stat_DEF.Text);
            pk7.Stat_SPE = Util.ToInt32(Stat_SPE.Text);
            pk7.Stat_SPA = Util.ToInt32(Stat_SPA.Text);
            pk7.Stat_SPD = Util.ToInt32(Stat_SPD.Text);

            // Unneeded Party Stats (Status, Flags, Unused)
            pk7.Data[0xE8] = pk7.Data[0xE9] = pk7.Data[0xEA] = pk7.Data[0xEB] =
                pk7.Data[0xED] = pk7.Data[0xEE] = pk7.Data[0xEF] =
                pk7.Data[0xFE] = pk7.Data[0xFF] = pk7.Data[0x100] =
                pk7.Data[0x101] = pk7.Data[0x102] = pk7.Data[0x103] = 0;

            // Fix Moves if a slot is empty 
            pk7.FixMoves();
            pk7.FixRelearn();
            pk7.FixMemories();

            // PKX is now filled
            pk7.RefreshChecksum();
            return pk7;
        }

        #endregion Main Window

        #region R/W trainer data

        // Dump data according to generation
        public void dumpAllData6()
        {
            dumpTrainerCard();
        }

        public void dumpAllData7()
        {
            dumpTrainerCard();
            dumpEggSeed();
            dumpLegendarySeed();
        }

        // Reload fields
        private void Btn_ReloadFields_Click(object sender, EventArgs e)
        {
            if (SAV.Generation == 6)
            {
                dumpAllData6();
            }
            else if (SAV.Generation == 7)
            {
                dumpAllData7();
            }
        }

        // Game save data handling
        public void dumpTrainerCard()
        {
            DataReadyWaiting myArgs = new DataReadyWaiting(new byte[LookupTable.trainercardSize], handleTrainerCard, null);
            waitingForData.Add(Program.scriptHelper.data(LookupTable.trainercardOff, LookupTable.trainercardSize, pid), myArgs);
        }

        public void handleTrainerCard(object args_obj)
        {
            DataReadyWaiting args = (DataReadyWaiting)args_obj;
            Array.Copy(args.data, 0, SAV.Data, LookupTable.trainercardLocation, LookupTable.trainercardSize);
            Delg.SetText(lb_name, SAV.OT);
            Delg.SetText(lb_tid, SAV.TID.ToString("D5"));
            Delg.SetText(lb_sid, SAV.SID.ToString("D5"));
            Delg.SetText(lb_tsv, LookupTable.getTSV(SAV.TID, SAV.SID).ToString("D4"));
            switch (SAV.Version)
            {
                case GameVersion.X:
                    Delg.SetText(lb_version, "X");
                    Delg.SetText(lb_g7id, null);
                    break;
                case GameVersion.Y:
                    Delg.SetText(lb_version, "Y");
                    Delg.SetText(lb_g7id, null);
                    break;
                case GameVersion.OR:
                    Delg.SetText(lb_version, "Omega Ruby");
                    Delg.SetText(lb_g7id, null);
                    break;
                case GameVersion.AS:
                    Delg.SetText(lb_version, "Alpha Sapphire");
                    Delg.SetText(lb_g7id, null);
                    break;
                case GameVersion.SN:
                    Delg.SetText(lb_version, "Sun");
                    Delg.SetText(lb_g7id, LookupTable.getG7ID(SAV.TID, SAV.SID).ToString("D6"));
                    break;
                case GameVersion.MN:
                    Delg.SetText(lb_version, "Moon");
                    Delg.SetText(lb_g7id, LookupTable.getG7ID(SAV.TID, SAV.SID).ToString("D6"));
                    break;
            }
        }

        // Egg Seed handling
        public void dumpEggSeed()
        {
            DataReadyWaiting myArgs = new DataReadyWaiting(new byte[0x10], handleEggSeed, null);
            waitingForData.Add(Program.scriptHelper.data(LookupTable.eggseedOff, 0x10, pid), myArgs);
        }

        public void handleEggSeed(object args_obj)
        {
            DataReadyWaiting args = (DataReadyWaiting)args_obj;
            Delg.SetText(Seed_Egg, BitConverter.ToString(args.data.Reverse().ToArray()).Replace("-", ""));
        }

        // RNG Seed
        public void dumpLegendarySeed()
        {
            DataReadyWaiting myArgs = new DataReadyWaiting(new byte[0x04], handleLegendarySeed, null);
            waitingForData.Add(Program.scriptHelper.data(LookupTable.legseedOff, 0x04, pid), myArgs);
        }

        public void handleLegendarySeed(object args_obj)
        {
            DataReadyWaiting args = (DataReadyWaiting)args_obj;
            Delg.SetText(Seed_Legendary, BitConverter.ToUInt32(args.data, 0).ToString("X8"));
        }

        #endregion R/W trainer data

        #region R/W pokémon data

        // Dump single pokémon
        private void dumpPokemon_Click(object sender, EventArgs e)
        {
            // Obtain offset
            uint dumpOff = 0;
            if (radioBoxes.Checked)
            {
                uint ssd = ((decimal.ToUInt32(boxDump.Value) - 1) * BOXSIZE) + decimal.ToUInt32(slotDump.Value) - 1;
                dumpOff = boxOff + (ssd * POKEBYTES);
            }
            else if (radioDaycare.Checked)
            {
                switch ((int)slotDump.Value)
                {
                    case 1: dumpOff = daycare1Off; break;
                    case 2: dumpOff = daycare2Off; break;
                    case 3: dumpOff = daycare3Off; break;
                    case 4: dumpOff = daycare4Off; break;
                    default: dumpOff = daycare1Off; break;
                }
            }
            else if (radioBattleBox.Checked)
            {
                dumpOff = battleBoxOff + ((decimal.ToUInt32(slotDump.Value) - 1) * POKEBYTES);
            }
            else if (radioTrade.Checked)
            {
                if (SAV.Generation == 6)
                {
                    DataReadyWaiting myArgs = new DataReadyWaiting(new byte[0x1FFFF], handleTradeData, null);
                    waitingForData.Add(Program.scriptHelper.data(tradeOff, 0x1FFFF, pid), myArgs);
                }
                else
                {
                    DataReadyWaiting myArgs = new DataReadyWaiting(new byte[POKEBYTES], handlePkmData, null);
                    uint mySeq = Program.scriptHelper.data(tradeOff, POKEBYTES, pid);
                    waitingForData.Add(mySeq, myArgs);
                }
            }
            else if (radioOpponent.Checked)
            {
                if (SAV.Generation == 6)
                {
                    DataReadyWaiting myArgs = new DataReadyWaiting(new byte[0x1FFFF], handleOpponentData, null);
                    waitingForData.Add(Program.scriptHelper.data(opponentOff, 0x1FFFF, pid), myArgs);
                }
                else
                {
                    DataReadyWaiting myArgs = new DataReadyWaiting(new byte[POKEBYTES], handlePkmData, null);
                    uint offset = 0;
                    switch ((int)boxDump.Value)
                    {
                        //Opponent 1 / wild Pokemon
                        case 1:
                            offset = opponentOff + (uint)(slotDump.Value - 1) * 260;
                            break;
                        //Opponent 2 (in dual battle)
                        case 2:
                            offset = opponentOff + 0xC98 + (uint)(slotDump.Value - 1) * 260;
                            break;
                        //Last called helper in SOS battle.
                        case 3:
                            offset = 0x3003969C;
                            break;
                        //Last 4 Pokemon in SOS battle
                        case 4:
                            offset = 0x3002F7B8 + (uint)(slotDump.Value - 1) * 0x1E4;
                            break;
                    }
                    uint mySeq = Program.scriptHelper.data(offset, POKEBYTES, pid);
                    waitingForData.Add(mySeq, myArgs);
                }
            }
            else if (radioParty.Checked)
            {
                dumpOff = partyOff + (decimal.ToUInt32(slotDump.Value) - 1) * 484;
            }

            // Read at offset
            if (radioParty.Checked)
            {
                DataReadyWaiting myArgs = new DataReadyWaiting(new byte[2602], handlePkmData, null);
                uint mySeq = Program.scriptHelper.data(dumpOff, 260, pid);
                waitingForData.Add(mySeq, myArgs);
            }
            else if (radioBoxes.Checked || radioDaycare.Checked || radioBattleBox.Checked)
            {
                DataReadyWaiting myArgs = new DataReadyWaiting(new byte[POKEBYTES], handlePkmData, null);
                uint mySeq = Program.scriptHelper.data(dumpOff, POKEBYTES, pid);
                waitingForData.Add(mySeq, myArgs);
            }
        }

        delegate void handlePkmDataDelegate(object args_obj);

        public void handlePkmData(object args_obj)
        {
            if (this.InvokeRequired)
            {
                BeginInvoke(new handlePkmDataDelegate(handlePkmData), args_obj);
                return;
            }
            try
            {
                DataReadyWaiting args = (DataReadyWaiting)args_obj;
                PKM validator = SAV.BlankPKM;

                validator.Data = PKX.decryptArray(args.data);
                bool dataCorrect = validator.ChecksumValid && validator.Species > 0 && validator.Species < SAV.MaxSpeciesID;

                if (dataCorrect)
                { // Valid pkx file
                    pkm = validator.Clone();
                    populateFields(pkm);
                    if (backupPKM.Checked)
                    {
                        savePKMtoFile();
                    }
                }
                else if (validator.ChecksumValid && validator.Species == 0)
                { // Empty data
                    MessageBox.Show("This pokémon data is empty.", "Empty data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                { // Invalid data
                    MessageBox.Show("This pokémon data is invalid, please try again.", "Invalid data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A error has ocurred:\r\n\r\n" + ex.Message);
            }
        }

        public void savePKMtoFile()
        {
            try
            {
                if (!verifiedPKM())
                    return;

                // Create Temp File to Drag
                PKM pkx = preparePKM();
                string fn = pkx.FileName; fn = fn.Substring(0, fn.LastIndexOf('.'));
                string filename = $"{fn}{"." + pkx.Extension}";
                byte[] data = pkx.DecryptedBoxData;

                // Make file
                string folderPath = System.Windows.Forms.@Application.StartupPath + "\\" + FOLDERPOKE + "\\";
                new FileInfo(folderPath).Directory.Create();
                string newfile = Path.Combine(folderPath, Util.CleanFileName(filename));
                File.WriteAllBytes(newfile, data);
            }
            catch (Exception ex)
            {
                MessageBox.Show("A error has ocurred:\r\n\r\n" + ex.Message);
            }
        }

        public void handleTradeData(object args_obj)
        {
            DataReadyWaiting args = (DataReadyWaiting)args_obj;

            byte[] relativePattern = null;
            uint offsetAfter = 0;

            if (SAV.Version == GameVersion.X || SAV.Version == GameVersion.Y)
            {
                relativePattern = new byte[] { 0x08, 0x1C, 0x01, 0x00, 0x00, 0x20, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xD8, 0xBE, 0x59 };
                offsetAfter += 98;
            }

            if (SAV.Version == GameVersion.OR || SAV.Version == GameVersion.AS)
            {
                relativePattern = new byte[] { 0x08, 0x1E, 0x01, 0x00, 0x00, 0x20, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x9C, 0xE8, 0x5D };
                offsetAfter += 98;
            }

            List<uint> occurences = findOccurences(args.data, relativePattern);
            int count = 0;
            foreach (uint occurence in occurences)
            {
                count++;
                if (count != 2)
                {
                    continue;
                }
                int dataOffset = (int)(occurence + offsetAfter);
                DataReadyWaiting args_pkm = new DataReadyWaiting(args.data.Skip(dataOffset).Take(POKEBYTES).ToArray(), handlePkmData, null);
                handlePkmData(args_pkm);
            }
        }

        public void handleOpponentData(object args_obj)
        {
            DataReadyWaiting args = (DataReadyWaiting)args_obj;            

            List<uint> occurences = findOccurences(args.data, LookupTable.oppPattern);
            int count = 0;
            foreach (uint occurence in occurences)
            {
                count++;
                int dataOffset = (int)(occurence + LookupTable.offsetOpp);
                DataReadyWaiting args_pkm = new DataReadyWaiting(args.data.Skip(dataOffset).Take(POKEBYTES).ToArray(), handlePkmData, null);
                handlePkmData(args_pkm);
            }
        }

        public void waitoppData(object args_obj)
        {
            DataReadyWaiting args = (DataReadyWaiting)args_obj;

            List<uint> occurences = findOccurences(args.data, LookupTable.oppPattern);
            foreach (uint occurence in occurences)
            {
                int dataOffset = (int)(occurence + LookupTable.offsetOpp);
                oppdata = args.data.Skip(dataOffset).Take(POKEBYTES).ToArray();
            }
        }

        static List<uint> findOccurences(byte[] haystack, byte[] needle)
        {
            List<uint> occurences = new List<uint>();

            for (uint i = 0; i < haystack.Length; i++)
            {
                if (needle[0] == haystack[i])
                {
                    bool found = true;
                    uint j, k;
                    for (j = 0, k = i; j < needle.Length; j++, k++)
                    {
                        if (k >= haystack.Length || needle[j] != haystack[k])
                        {
                            found = false;
                            break;
                        }
                    }
                    if (found)
                    {
                        occurences.Add(i - 1);
                        i = k;
                    }
                }
            }
            return occurences;
        }
        
        // Save all boxes
        private void dumpBoxes_Click(object sender, EventArgs e)
        {
            DataReadyWaiting myArgs = new DataReadyWaiting(new byte[BOXES * BOXSIZE * POKEBYTES], handleAllBoxesData, null);
            waitingForData.Add(Program.scriptHelper.data(boxOff, BOXES * BOXSIZE * POKEBYTES, pid), myArgs);
        }

        public void handleAllBoxesData(object args_obj)
        {
            try
            {
                DataReadyWaiting args = (DataReadyWaiting)args_obj;
                string folderPath = System.Windows.Forms.@Application.StartupPath + "\\" + FOLDERPOKE + "\\";
                (new FileInfo(folderPath)).Directory.Create();
                string fileName = SAV.OT + " (" + SAV.Version.ToString() + ") - " + DateTime.Now.ToString("yyyyMMddHHmmss") + BOXEXT;
                string newfile = Path.Combine(folderPath, Util.CleanFileName(fileName));
                File.WriteAllBytes(newfile, args.data);
            }
            catch (Exception ex)
            {
                MessageBox.Show("A error has ocurred:\r\n\r\n" + ex.Message);
            }

        }

        public void WriteDataToFile(byte[] data, string path)
        {
            try
            {
                File.WriteAllBytes(path, data);
            }
            catch (Exception ex)
            {
                MessageBox.Show("A error has ocurred:\r\n\r\n" + ex.Message);
            }
        }

        // Write single pokémon from tabs
        private void Write_PKM_Click(object sender, EventArgs e)
        {
            if (!verifiedPKM())
                return;

            pkm = preparePKM();
            updateLegality();

            if (Legality.Valid || PB_Legal.Visible == false || enableillegal)
            { // Write only legal and Gen 5 or eariler pokemon
                if (enableillegal)
                {
                    DialogResult dr = MessageBox.Show("This pokémon is illegal. Do you still want to inject it to the game?", "Illegal pokémon", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.No)
                    {
                        return;
                    }
                }
                byte[] pkmEdited = PKX.encryptArray(pkm.DecryptedBoxData);
                if (radioBoxes.Checked)
                {
                    uint index = ((uint)boxDump.Value - 1) * BOXSIZE + (uint)slotDump.Value - 1;
                    uint offset = boxOff + (index * POKEBYTES);
                    Program.scriptHelper.write(offset, pkmEdited, pid);
                }
                else
                {
                    MessageBox.Show("No support for this source, if you want to edit this pokémon, deposit it in the PC.", "No editing support", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("This pokémon is illegal, it won't be written to the file.", "Illegal pokémon", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Clone and delete
        private void Btn_CDstart_Click(object sender, EventArgs e)
        {
            if (!verifiedPKM())
                return;

            pkm = preparePKM();
            updateLegality();
            byte[] pkmsource;

            if (CloneMode.Checked)
            {
                if (Legality.Valid || PB_Legal.Visible == false)
                { // Write only legal and Gen 5 or eariler pokemon
                    pkmsource = PKX.encryptArray(pkm.DecryptedBoxData);
                }
                else
                {
                    MessageBox.Show("This pokémon is illegal, it won't be written to the file.", "Illegal pokémon", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else if (DeleteMode.Checked)
            {
                pkmsource = PKX.encryptArray(SAV.BlankPKM.DecryptedBoxData);
            }
            else
            {
                return;
            }

            uint index = ((uint)Num_CDBox.Value - 1) * BOXSIZE + (uint)Num_CDSlot.Value - 1;
            uint offset = boxOff + (index * POKEBYTES);
            uint size = (uint)Num_CDAmount.Value * POKEBYTES;

            if (CB_CDBackup.Checked)
            {
                DataReadyWaiting myArgs = new DataReadyWaiting(new byte[size], handleAllBoxesData, null);
                waitingForData.Add(Program.scriptHelper.data(offset, size, pid), myArgs);
            }

            byte[] data = new byte[size];
            for (int i = 0; i < Num_CDAmount.Value; i++)
            {
                Array.Copy(pkmsource, 0, data, i * POKEBYTES, POKEBYTES);
            }
            Program.scriptHelper.write(offset, data, pid);
        }

        #endregion R/W pokémon data

        #region GUI handling

        // Log export
        private void Log_Export_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText(System.Windows.Forms.@Application.StartupPath + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_pkmn-ntr.txt", txtLog.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("A error has ocurred:\r\n\r\n" + ex.Message);
            }
        }

        // Radio boxes for pokémon source
        private void radioBoxes_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBoxes.Tag == null)
            {
                radioBoxes.Tag = new LastBoxSlot { box = 1, slot = 1 };
            }

            if (radioBoxes.Checked)
            {
                boxDump.Minimum = 1;
                boxDump.Maximum = BOXES;
                slotDump.Minimum = 1;
                slotDump.Maximum = BOXSIZE;
                boxDump.Enabled = true;
                slotDump.Enabled = true;
                backupPKM.Enabled = true;
                Write_PKM.Enabled = true;
                boxDump.Value = ((LastBoxSlot)radioBoxes.Tag).box;
                slotDump.Value = ((LastBoxSlot)radioBoxes.Tag).slot;
            }
            else
            {
                radioBoxes.Tag = new LastBoxSlot { box = boxDump.Value, slot = slotDump.Value };
            }

        }

        private void radioDaycare_CheckedChanged(object sender, EventArgs e)
        {
            if (radioDaycare.Tag == null)
            {
                radioDaycare.Tag = new LastBoxSlot { box = 1, slot = 1 };
            }
            if (radioDaycare.Checked)
            {
                boxDump.Minimum = 1;
                boxDump.Maximum = 2;
                slotDump.Minimum = 1;
                if (SAV.Version == GameVersion.OR || SAV.Version == GameVersion.AS) // Handle ORAS Battle Resort Daycare
                {
                    slotDump.Maximum = 4;
                }
                else
                {
                    slotDump.Maximum = 2;
                }
                boxDump.Enabled = false;
                slotDump.Enabled = true;
                backupPKM.Enabled = true;
                Write_PKM.Enabled = false;
                boxDump.Value = ((LastBoxSlot)radioDaycare.Tag).box;
                slotDump.Value = ((LastBoxSlot)radioDaycare.Tag).slot;
            }
            else
            {
                radioDaycare.Tag = new LastBoxSlot { box = boxDump.Value, slot = slotDump.Value };
            }
        }

        private void radioBattleBox_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBattleBox.Tag == null)
            {
                radioBattleBox.Tag = new LastBoxSlot { box = 1, slot = 1 };
            }
            if (radioBattleBox.Checked)
            {
                boxDump.Minimum = 1;
                boxDump.Maximum = 1;
                slotDump.Minimum = 1;
                slotDump.Maximum = 6;
                boxDump.Enabled = false;
                slotDump.Enabled = true;
                backupPKM.Enabled = true;
                Write_PKM.Enabled = false;
                boxDump.Value = ((LastBoxSlot)radioBattleBox.Tag).box;
                slotDump.Value = ((LastBoxSlot)radioBattleBox.Tag).slot;
            }
            else
            {
                radioBattleBox.Tag = new LastBoxSlot { box = boxDump.Value, slot = slotDump.Value };
            }
        }

        private void radioTrade_CheckedChanged(object sender, EventArgs e)
        {
            if (radioTrade.Tag == null)
            {
                radioTrade.Tag = new LastBoxSlot { box = 1, slot = 1 };
            }
            if (radioTrade.Checked)
            {
                boxDump.Minimum = 1;
                boxDump.Maximum = 1;
                slotDump.Minimum = 1;
                slotDump.Maximum = 1;
                boxDump.Enabled = false;
                slotDump.Enabled = false;
                backupPKM.Checked = true;
                backupPKM.Enabled = false;
                Write_PKM.Enabled = false;
                boxDump.Value = ((LastBoxSlot)radioTrade.Tag).box;
                slotDump.Value = ((LastBoxSlot)radioTrade.Tag).slot;
            }
            else
            {
                radioTrade.Tag = new LastBoxSlot { box = boxDump.Value, slot = slotDump.Value };
            }
        }

        private void radioOpponent_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOpponent.Tag == null)
            {
                radioOpponent.Tag = new LastBoxSlot { box = 1, slot = 1 };
            }
            if (radioOpponent.Checked)
            {
                if (SAV.Generation == 6)
                {
                    boxDump.Minimum = 1;
                    boxDump.Maximum = 1;
                    slotDump.Minimum = 1;
                    slotDump.Maximum = 1;
                    boxDump.Enabled = false;
                    slotDump.Enabled = false;
                    DumpInstructionsBtn.Visible = false;

                }
                if (SAV.Generation == 7)
                {
                    boxDump.Minimum = 1;
                    boxDump.Maximum = 4;
                    slotDump.Minimum = 1;
                    slotDump.Maximum = 6;
                    boxDump.Enabled = true;
                    slotDump.Enabled = true;
                    DumpInstructionsBtn.Visible = true;
                }
                backupPKM.Enabled = false;
                BoxLabel.Text = "Opp.:";
                Write_PKM.Enabled = false;
                boxDump.Value = ((LastBoxSlot)radioOpponent.Tag).box;
                slotDump.Value = ((LastBoxSlot)radioOpponent.Tag).slot;
            }
            else
            {
                radioOpponent.Tag = new LastBoxSlot { box = boxDump.Value, slot = slotDump.Value };
                BoxLabel.Text = "Box:";
                if (SAV.Generation == 7)
                {
                    DumpInstructionsBtn.Visible = false;
                }
            }
        }

        private void radioParty_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioParty.Tag == null)
            {
                radioParty.Tag = new LastBoxSlot { box = 1, slot = 1 };
            }
            if (radioParty.Checked)
            {
                if (!enablepartywrite && Tabs_General.TabPages[0].Enabled)
                {
                    MessageBox.Show("Important:\r\n\r\nThis feature is experimental, the slots that is selected in this application might not be the same slots that are shown in your party. Due the unkonown mechanics of this, the write feature has been disabled.\r\n\r\nIf you wish to edit a pokémon in your party, deposit it the PC.", "PKMN-NTR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                boxDump.Minimum = 1;
                boxDump.Maximum = 1;
                slotDump.Minimum = 1;
                slotDump.Maximum = 6;
                boxDump.Enabled = false;
                slotDump.Enabled = true;
                backupPKM.Enabled = true;
                Write_PKM.Enabled = false;
                boxDump.Value = ((LastBoxSlot)radioParty.Tag).box;
                slotDump.Value = ((LastBoxSlot)radioParty.Tag).slot;
            }
            else
            {
                radioParty.Tag = new LastBoxSlot { box = boxDump.Value, slot = slotDump.Value };
            }
        }

        // Clone/Delete tab
        private void clonedelete_Changed(object sender, EventArgs e)
        {
            Delg.SetMaximum(Num_CDAmount, LookupTable.getMaxSpace((int)Num_CDBox.Value, (int)Num_CDSlot.Value));
        }

        // Bot functions
        public void HandleRAMread(uint value)
        {
            addtoLog("NTR: Read sucessful - 0x" + value.ToString("X8"));
            Delg.SetText(readResult, "0x" + value.ToString("X8"));
        }

        public void updateDumpBoxes(int box, int slot)
        {
            Delg.SetValue(boxDump, box + 1);
            Delg.SetValue(slotDump, slot + 1);
        }

        public void updateDumpBoxes(NumericUpDown box, NumericUpDown slot)
        {
            Delg.SetValue(boxDump, box.Value);
            Delg.SetValue(slotDump, slot.Value);
        }

        public void UpdateResetCounter(int resets)
        {
            Delg.SetText(resetNoBox, resets.ToString());
        }

        public void SetResetLabel(string lbl)
        {
            Delg.SetText(labelreset, lbl);
        }

        public int GetResetNumber()
        {
            if (int.TryParse(resetNoBox.Text, out int number))
            {
                return number;
            }
            else
            {
                return 0;
            }
        }

        #endregion GUI handling

        #region PKHeX Tabs

        // Open file
        private void tabMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.AllowedEffect == (DragDropEffects.Copy | DragDropEffects.Link)) // external file
                e.Effect = DragDropEffects.Copy;
            else if (e.Data != null) // within
                e.Effect = DragDropEffects.Move;
        }

        private void tabMain_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            openQuick(files[0]);
            e.Effect = DragDropEffects.Copy;

            Cursor = DefaultCursor;
        }

        private void openQuick(string path, bool force = false)
        {
            if (!(CanFocus || force))
            {
                SystemSounds.Asterisk.Play();
                return;
            }

            string ext = Path.GetExtension(path);
            FileInfo fi = new FileInfo(path);
            if (fi.Length > 0x10009C && fi.Length != 0x380000)
                WinFormsUtil.Error("Input file is too large." + Environment.NewLine + $"Size: {fi.Length} bytes", path);
            else if (fi.Length < 32)
                WinFormsUtil.Error("Input file is too small." + Environment.NewLine + $"Size: {fi.Length} bytes", path);
            else
            {
                byte[] input; try { input = File.ReadAllBytes(path); }
                catch (Exception e) { WinFormsUtil.Error("Unable to load file.  It could be in use by another program.\nPath: " + path, e); return; }

                try { openFile(input, path, ext); }
                catch (Exception e) { WinFormsUtil.Error("Unable to load file.\nPath: " + path, e); }
            }
        }

        private void openFile(byte[] input, string path, string ext)
        {
            PKM temp; MysteryGift tg; string c;
            if ((temp = PKMConverter.getPKMfromBytes(input, prefer: SAV.Generation)) != null)
            {
                PKM pk = PKMConverter.convertToFormat(temp, SAV.PKMType, out c);
                if (pk == null)
                    WinFormsUtil.Alert("Conversion failed.", c);
                else if (SAV.Generation < 3 && ((pk as PK1)?.Japanese ?? ((PK2)pk).Japanese) != SAV.Japanese)
                {
                    string a_lang = SAV.Japanese ? "an International" : "a Japanese";
                    string pk_type = pk.GetType().Name;
                    WinFormsUtil.Alert($"Cannot load {a_lang} {pk_type} in {a_lang} save file.");
                }
                else
                    populateFields(pk);
                Console.WriteLine(c);
            }
            else if ((tg = MysteryGift.getMysteryGift(input, ext)) != null)
            {
                if (!tg.IsPokémon)
                { WinFormsUtil.Alert("Mystery Gift is not a Pokémon.", path); return; }

                temp = tg.convertToPKM(SAV);
                PKM pk = PKMConverter.convertToFormat(temp, SAV.PKMType, out c);

                if (pk == null)
                    WinFormsUtil.Alert("Conversion failed.", c);
                else
                    populateFields(pk);
                Console.WriteLine(c);
            }
            else
                WinFormsUtil.Error("Attempted to load an unsupported file type/size.",
                    $"File Loaded:{Environment.NewLine}{path}",
                    $"File Size:{Environment.NewLine}{input.Length} bytes (0x{input.Length:X4})");
        }

        // All tabs
        private void InitializeFields()
        {
            // Now that the ComboBoxes are ready, load the data.
            fieldsInitialized = true;
            pkm.RefreshChecksum();

            // Load Data
            populateFields(pkm);
            TemplateFields();
        }

        delegate void populateDelegate(PKM pk, bool focus = true);

        public void populateFields(PKM pk, bool focus = true)
        {
            if (this.InvokeRequired)
            {
                BeginInvoke(new populateDelegate(populateFields), pk, focus);
                return;
            }
            if (pk == null) { WinFormsUtil.Error("Attempted to load a null file."); return; }

            if ((pk.Format >= 3 && pk.Format > SAV.Generation) // pk3-7, can't go backwards
                || (pk.Format <= 2 && SAV.Generation > 2 && SAV.Generation < 7)) // pk1-2, can't go 3-6
            { WinFormsUtil.Alert($"Can't load Gen{pk.Format} to Gen{SAV.Generation} games."); return; }

            bool oldInit = fieldsInitialized;
            fieldsInitialized = fieldsLoaded = false;
            if (focus)
                Tab_Main.Focus();

            pkm = pk.Clone();

            if (fieldsInitialized & !pkm.ChecksumValid)
                WinFormsUtil.Alert("PKX File has an invalid checksum.");

            if (pkm.Format != SAV.Generation) // past gen format
            {
                string c;
                pkm = PKMConverter.convertToFormat(pkm, SAV.PKMType, out c);
                if (pk.Format != pkm.Format && focus) // converted
                    WinFormsUtil.Alert("Converted File.");
            }

            try { getFieldsfromPKM(); }
            catch { fieldsInitialized = oldInit; throw; }

            CB_EncounterType.Visible = Label_EncounterType.Visible = pkm.Gen4 && SAV.Generation < 7;
            fieldsInitialized = oldInit;
            updateIVs(null, null);
            updatePKRSInfected(null, null);
            updatePKRSCured(null, null);
            fieldsLoaded = true;

            Label_HatchCounter.Visible = CHK_IsEgg.Checked && SAV.Generation > 1;
            Label_Friendship.Visible = !CHK_IsEgg.Checked && SAV.Generation > 1;

            // Set the Preview Box
            dragout.Image = pk.Sprite();
            setMarkings();
            updateLegality();
        }

        private void TemplateFields()
        {
            CB_GameOrigin.SelectedIndex = 0;
            CB_Move1.SelectedValue = 1;
            TB_OT.Text = "PKMN-NTR";
            TB_TID.Text = 12345.ToString();
            TB_SID.Text = 54321.ToString();
            int curlang = Array.IndexOf(GameInfo.lang_val, "en");
            CB_Language.SelectedIndex = curlang > CB_Language.Items.Count - 1 ? 1 : curlang;
            CB_Ball.SelectedIndex = Math.Min(0, CB_Ball.Items.Count - 1);
            CB_Country.SelectedIndex = Math.Min(0, CB_Country.Items.Count - 1);
            CAL_MetDate.Value = CAL_EggDate.Value = DateTime.Today;
            CB_Species.SelectedValue = SAV.MaxSpeciesID;
            CHK_Nicknamed.Checked = false;
        }

        private void clickLegality(object sender, EventArgs e)
        {
            PKM pk;
            if (verifiedPKM())
                pk = preparePKM();
            else
                return;

            if (pk.Species == 0 || !pk.ChecksumValid)
            { SystemSounds.Asterisk.Play(); return; }
            showLegality(pk, true, ModifierKeys == Keys.Control);
        }

        private void updateLegality(LegalityAnalysis la = null, bool skipMoveRepop = false)
        {
            if (!fieldsLoaded)
                return;

            Legality = la ?? new LegalityAnalysis(pkm);
            if (!Legality.Parsed)
            {
                PB_Legal.Visible =
                PB_WarnMove1.Visible = PB_WarnMove2.Visible = PB_WarnMove3.Visible = PB_WarnMove4.Visible =
                PB_WarnRelearn1.Visible = PB_WarnRelearn2.Visible = PB_WarnRelearn3.Visible = PB_WarnRelearn4.Visible = false;
                return;
            }

            PB_Legal.Visible = true;
            PB_Legal.Image = Legality.Valid ? Resources.valid : Resources.warn;

            // Refresh Move Legality
            for (int i = 0; i < 4; i++)
                movePB[i].Visible = !Legality.vMoves[i].Valid;

            if (pkm.Format >= 6)
                for (int i = 0; i < 4; i++)
                    relearnPB[i].Visible = !Legality.vRelearn[i].Valid;

            if (skipMoveRepop)
                return;
            // Resort moves
            bool tmp = fieldsLoaded;
            fieldsLoaded = false;
            var cb = new[] { CB_Move1, CB_Move2, CB_Move3, CB_Move4 };
            var moves = Legality.AllSuggestedMovesAndRelearn;
            var moveList = GameInfo.MoveDataSource.OrderByDescending(m => moves.Contains(m.Value)).ToArray();
            foreach (ComboBox c in cb)
            {
                var index = WinFormsUtil.getIndex(c);
                c.DataSource = new BindingSource(moveList, null);
                c.SelectedValue = index;
                if (c.Visible)
                    c.SelectionLength = 0; // flicker hack
            }
            fieldsLoaded |= tmp;
        }

        private void showLegality(PKM pk, bool tabs, bool verbose, bool skipMoveRepop = false)
        {
            LegalityAnalysis la = new LegalityAnalysis(pk);
            if (!la.Parsed)
            {
                WinFormsUtil.Alert(pk.Format < 3
                       ? $"Checking legality of PK{pk.Format} files is not supported."
                       : $"Checking legality of PK{pk.Format} files that originated from Gen{pk.GenNumber} is not supported.");
                return;
            }
            if (tabs)
                updateLegality(la, skipMoveRepop);
            WinFormsUtil.Alert(la.Report(verbose));
        }

        public PKM preparePKM(bool click = true)
        {
            if (click)
                ValidateChildren();

            PKM pk = getPKMfromFields();
            return pk?.Clone();
        }

        public bool verifiedPKM()
        {
            if (ModifierKeys == (Keys.Control | Keys.Shift | Keys.Alt))
                return true; // Override
            // Make sure the PKX Fields are filled out properly (color check)
            ComboBox[] cba = {
                                 CB_Species, CB_Nature, CB_HeldItem, CB_Ability, // Main Tab
                                 CB_MetLocation, CB_EggLocation, CB_Ball,   // Met Tab
                                 CB_Move1, CB_Move2, CB_Move3, CB_Move4,    // Moves
                                 CB_RelearnMove1, CB_RelearnMove2, CB_RelearnMove3, CB_RelearnMove4 // Moves
                             };

            ComboBox cb = cba.FirstOrDefault(c => c.BackColor == Color.DarkSalmon && c.Items.Count != 0);
            if (cb != null)
            {
                Control c = cb.Parent;
                while (!(c is TabPage))
                    c = c.Parent;
                tabMain.SelectedTab = c as TabPage;
            }
            else if (SAV.Generation >= 3 && Convert.ToUInt32(TB_EVTotal.Text) > 510)
                tabMain.SelectedTab = Tab_Stats;
            else if (WinFormsUtil.getIndex(CB_Species) == 0)
                tabMain.SelectedTab = Tab_Main;
            else
                return true;

            SystemSounds.Exclamation.Play();
            return false;
        }

        private void dragoutDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            openQuick(files[0]);
            e.Effect = DragDropEffects.Copy;

            Cursor = DefaultCursor;
        }

        private void dragout_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void dragout_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;
            if (!verifiedPKM())
                return;

            // Create Temp File to Drag
            PKM pkx = preparePKM();
            bool encrypt = ModifierKeys == Keys.Control;
            string fn = pkx.FileName; fn = fn.Substring(0, fn.LastIndexOf('.'));
            string filename = $"{fn}{(encrypt ? ".ek" + pkx.Format : "." + pkx.Extension)}";
            byte[] dragdata = encrypt ? pkx.EncryptedBoxData : pkx.DecryptedBoxData;
            // Make file
            string newfile = Path.Combine(Path.GetTempPath(), Util.CleanFileName(filename));
            try
            {
                File.WriteAllBytes(newfile, dragdata);
                PictureBox pb = (PictureBox)sender;
                Cursor = DragInfo.Cursor = new Cursor(((Bitmap)pb.Image).GetHicon());
                DoDragDrop(new DataObject(DataFormats.FileDrop, new[] { newfile }), DragDropEffects.Move);
            }
            catch (Exception x)
            { WinFormsUtil.Error("Drag & Drop Error", x); }
            Cursor = DragInfo.Cursor = DefaultCursor;
            File.Delete(newfile);
        }

        private void dragoutEnter(object sender, EventArgs e)
        {
            dragout.BackgroundImage = WinFormsUtil.getIndex(CB_Species) > 0 ? Resources.slotSet : Resources.slotDel;
            Cursor = Cursors.Hand;
        }

        private void dragoutLeave(object sender, EventArgs e)
        {
            dragout.BackgroundImage = Resources.slotTrans;
            if (Cursor == Cursors.Hand)
                Cursor = Cursors.Default;
        }

        private void setIsShiny(object sender)
        {
            if (sender == TB_PID)
                pkm.PID = Util.getHEXval(TB_PID.Text);
            else if (sender == TB_TID)
                pkm.TID = (int)Util.ToUInt32(TB_TID.Text);
            else if (sender == TB_SID)
                pkm.SID = (int)Util.ToUInt32(TB_SID.Text);

            bool isShiny = pkm.IsShiny;

            // Set the Controls
            BTN_Shinytize.Visible = BTN_Shinytize.Enabled = !isShiny;
            Label_IsShiny.Visible = isShiny;

            // Refresh Markings (for Shiny Star if applicable)
            setMarkings();
        }

        private void getQuickFiller(PictureBox pb, PKM pk = null)
        {
            if (!fieldsInitialized) return;
            pk = pk ?? preparePKM(false); // don't perform control loss click

            var sprite = pk.Species != 0 ? pk.Sprite() : null;
            pb.Image = sprite;
            if (pb.BackColor == Color.Red)
                pb.BackColor = Color.Transparent;
        }

        private void removedropCB(object sender, KeyEventArgs e)
        {
            ((ComboBox)sender).DroppedDown = false;
        }

        private void validateComboBox(object sender)
        {
            if (!formInitialized)
                return;
            ComboBox cb = sender as ComboBox;
            if (cb == null)
                return;

            if (cb.Text == "" && cb.Items.Count > 0)
            { cb.SelectedIndex = 0; return; }
            if (cb.SelectedValue == null)
                cb.BackColor = Color.DarkSalmon;
            else
                cb.ResetBackColor();
        }

        private void validateComboBox(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!(sender is ComboBox))
                return;

            validateComboBox(sender);

            if (fieldsLoaded)
                getQuickFiller(dragout);
        }

        private void validateComboBox2(object sender, EventArgs e)
        {
            if (!fieldsInitialized)
                return;
            validateComboBox(sender, null);
            if (fieldsLoaded)
            {
                if (sender == CB_Ability && SAV.Generation >= 6)
                    TB_AbilityNumber.Text = (1 << CB_Ability.SelectedIndex).ToString();
                if (sender == CB_Ability && SAV.Generation <= 5 && CB_Ability.SelectedIndex < 2) // not hidden
                    updateRandomPID(sender, e);
                if (sender == CB_Nature && SAV.Generation <= 4)
                {
                    pkm.Nature = CB_Nature.SelectedIndex;
                    updateRandomPID(sender, e);
                }
                if (sender == CB_HeldItem || sender == CB_Ability)
                    updateLegality();
            }
            updateNatureModification(sender, null);
            updateIVs(null, null); // updating Nature will trigger stats to update as well
        }

        // Main tab
        private void updateShinyPID(object sender, EventArgs e)
        {
            pkm.TID = Util.ToInt32(TB_TID.Text);
            pkm.SID = Util.ToInt32(TB_SID.Text);
            pkm.PID = Util.getHEXval(TB_PID.Text);
            pkm.Nature = WinFormsUtil.getIndex(CB_Nature);
            pkm.Gender = PKX.getGender(Label_Gender.Text);
            pkm.AltForm = CB_Form.SelectedIndex;
            pkm.Version = WinFormsUtil.getIndex(CB_GameOrigin);

            if (pkm.Format > 2)
                pkm.setShinyPID();
            else
            {
                // IVs determine shininess
                // All 10IV except for one where (IV & 2 == 2) [gen specific]
                int[] and2 = { 2, 3, 6, 7, 10, 11, 14, 15 };
                int randIV = and2[Util.rnd32() % and2.Length];
                if (pkm.Format == 1)
                {
                    TB_ATKIV.Text = "10"; // an attempt was made
                    TB_DEFIV.Text = randIV.ToString();
                }
                else // pkm.Format == 2
                {
                    TB_ATKIV.Text = randIV.ToString();
                    TB_DEFIV.Text = "10";
                }
                TB_SPEIV.Text = "10";
                TB_SPAIV.Text = "10";
                updateIVs(null, null);
            }
            TB_PID.Text = pkm.PID.ToString("X8");

            if (pkm.GenNumber < 6 && TB_EC.Visible)
                TB_EC.Text = TB_PID.Text;

            getQuickFiller(dragout);
            updateLegality();
        }

        private void clickGender(object sender, EventArgs e)
        {
            // Get Gender Threshold
            int gt = SAV.Personal.getFormeEntry(WinFormsUtil.getIndex(CB_Species), CB_Form.SelectedIndex).Gender;

            if (gt == 255 || gt == 0 || gt == 254) // Single gender/genderless
                return;

            if (gt >= 255) return;
            // If not a single gender(less) species: (should be <254 but whatever, 255 never happens)

            int newGender = PKX.getGender(Label_Gender.Text) ^ 1;
            if (SAV.Generation == 2)
                do { TB_ATKIV.Text = (Util.rnd32() & SAV.MaxIV).ToString(); } while (PKX.getGender(Label_Gender.Text) != newGender);
            else if (SAV.Generation <= 4)
            {
                pkm.Species = WinFormsUtil.getIndex(CB_Species);
                pkm.Version = WinFormsUtil.getIndex(CB_GameOrigin);
                pkm.Nature = WinFormsUtil.getIndex(CB_Nature);
                pkm.AltForm = CB_Form.SelectedIndex;

                pkm.setPIDGender(newGender);
                TB_PID.Text = pkm.PID.ToString("X8");
            }
            pkm.Gender = newGender;
            Label_Gender.Text = gendersymbols[pkm.Gender];
            Label_Gender.ForeColor = pkm.Gender == 2 ? Label_Species.ForeColor : (pkm.Gender == 1 ? Color.Red : Color.Blue);

            if (PKX.getGender(CB_Form.Text) < 2) // Gendered Forms
                CB_Form.SelectedIndex = PKX.getGender(Label_Gender.Text);

            getQuickFiller(dragout);
        }

        private void updateGender()
        {
            int cg = PKX.getGender(Label_Gender.Text);
            int gt = SAV.Personal.getFormeEntry(WinFormsUtil.getIndex(CB_Species), CB_Form.SelectedIndex).Gender;

            int Gender;
            if (gt == 255)      // Genderless
                Gender = 2;
            else if (gt == 254) // Female Only
                Gender = 1;
            else if (gt == 0)  // Male Only
                Gender = 0;
            else if (cg == 2 || WinFormsUtil.getIndex(CB_GameOrigin) < 24)
                Gender = (Util.getHEXval(TB_PID.Text) & 0xFF) <= gt ? 1 : 0;
            else
                Gender = cg;

            Label_Gender.Text = gendersymbols[Gender];
            Label_Gender.ForeColor = Gender == 2 ? Label_Species.ForeColor : (Gender == 1 ? Color.Red : Color.Blue);
        }

        private void updateRandomPID(object sender, EventArgs e)
        {
            if (pkm.Format < 3)
                return;
            if (fieldsLoaded)
                pkm.PID = Util.getHEXval(TB_PID.Text);

            if (sender == Label_Gender)
                pkm.setPIDGender(pkm.Gender);
            else if (sender == CB_Nature && pkm.Nature != WinFormsUtil.getIndex(CB_Nature))
                pkm.setPIDNature(WinFormsUtil.getIndex(CB_Nature));
            else if (sender == BTN_RerollPID)
                pkm.setPIDGender(pkm.Gender);
            else if (sender == CB_Ability && CB_Ability.SelectedIndex != pkm.PIDAbility && pkm.PIDAbility > -1)
                pkm.PID = PKX.getRandomPID(pkm.Species, pkm.Gender, pkm.Version, pkm.Nature, pkm.Format, (uint)(CB_Ability.SelectedIndex * 0x10001));

            TB_PID.Text = pkm.PID.ToString("X8");
            getQuickFiller(dragout);
            if (pkm.GenNumber < 6 && SAV.Generation >= 6)
                TB_EC.Text = TB_PID.Text;
        }

        private void updateSpecies(object sender, EventArgs e)
        {
            // Get Species dependent information
            setAbilityList();
            setForms();
            updateForm(null, null);

            if (!fieldsLoaded)
                return;

            pkm.Species = WinFormsUtil.getIndex(CB_Species);
            // Recalculate EXP for Given Level
            uint EXP = PKX.getEXP(pkm.CurrentLevel, pkm.Species);
            TB_EXP.Text = EXP.ToString();

            // Check for Gender Changes
            updateGender();

            // If species changes and no nickname, set the new name == speciesName.
            if (!CHK_Nicknamed.Checked)
                updateNickname(sender, e);

            updateLegality();
        }

        private void updateNickname(object sender, EventArgs e)
        {
            int lang = WinFormsUtil.getIndex(CB_Language);

            if (!fieldsInitialized || CHK_Nicknamed.Checked)
                return;

            // Fetch Current Species and set it as Nickname Text
            int species = WinFormsUtil.getIndex(CB_Species);
            if (species < 1 || species > SAV.MaxSpeciesID)
            { TB_Nickname.Text = ""; return; }

            if (CHK_IsEgg.Checked)
                species = 0; // get the egg name.

            // If name is that of another language, don't replace the nickname
            if (sender != CB_Language && species != 0 && !PKX.getIsNicknamedAnyLanguage(species, TB_Nickname.Text, SAV.Generation))
                return;

            TB_Nickname.Text = PKX.getSpeciesNameGeneration(species, lang, SAV.Generation);
            if (SAV.Generation == 1)
                ((PK1)pkm).setNotNicknamed();
            if (SAV.Generation == 2)
                ((PK2)pkm).setNotNicknamed();
        }

        private void updateNicknameClick(object sender, MouseEventArgs e)
        {
            TextBox tb = !(sender is TextBox) ? TB_Nickname : (TextBox)sender;
            // Special Character Form
            if (ModifierKeys != Keys.Control)
                return;

            if (tb == TB_Nickname)
            {
                pkm.Nickname = tb.Text;
                var d = new f2_Text(tb, pkm.Nickname_Trash);
                d.ShowDialog();
                tb.Text = d.FinalString;
                pkm.Nickname_Trash = d.FinalBytes;
            }
            else if (tb == TB_OT)
            {
                pkm.OT_Name = tb.Text;
                var d = new f2_Text(tb, pkm.OT_Trash);
                d.ShowDialog();
                tb.Text = d.FinalString;
                pkm.OT_Trash = d.FinalBytes;
            }
            else if (tb == TB_OTt2)
            {
                pkm.HT_Name = tb.Text;
                var d = new f2_Text(tb, pkm.HT_Trash);
                d.ShowDialog();
                tb.Text = d.FinalString;
                pkm.HT_Trash = d.FinalBytes;
            }
        }

        private void updateIsNicknamed(object sender, EventArgs e)
        {
            if (!fieldsLoaded)
                return;

            if (!CHK_Nicknamed.Checked)
            {
                int species = WinFormsUtil.getIndex(CB_Species);
                if (species < 1 || species > SAV.MaxSpeciesID)
                    return;
                int lang = WinFormsUtil.getIndex(CB_Language);
                if (CHK_IsEgg.Checked) species = 0; // Set species to 0 to get the egg name.
                string nick = PKX.getSpeciesName(CHK_IsEgg.Checked ? 0 : species, lang);

                if (SAV.Generation < 5) // All caps GenIV and previous
                    nick = nick.ToUpper();
                if (SAV.Generation < 3)
                    nick = nick.Replace(" ", "");
                if (TB_Nickname.Text != nick)
                {
                    CHK_Nicknamed.Checked = true;
                    pkm.Nickname = TB_Nickname.Text;
                }
            }
        }

        private void updateEXPLevel(object sender, EventArgs e)
        {
            if (changingFields || !fieldsInitialized) return;

            changingFields = true;
            if (sender == TB_EXP)
            {
                // Change the Level
                uint EXP = Util.ToUInt32(TB_EXP.Text);
                int Species = WinFormsUtil.getIndex(CB_Species);
                int Level = PKX.getLevel(Species, EXP);
                if (Level == 100)
                    EXP = PKX.getEXP(100, Species);

                TB_Level.Text = Level.ToString();
                TB_EXP.Text = EXP.ToString();
            }
            else
            {
                // Change the XP
                int Level = Util.ToInt32(TB_Level.Text);
                if (Level > 100) TB_Level.Text = "100";
                if (Level > byte.MaxValue) MT_Level.Text = "255";

                if (Level <= 100)
                    TB_EXP.Text = PKX.getEXP(Level, WinFormsUtil.getIndex(CB_Species)).ToString();
            }
            changingFields = false;
            if (fieldsLoaded) // store values back
            {
                pkm.EXP = Util.ToUInt32(TB_EXP.Text);
                pkm.Stat_Level = Util.ToInt32(TB_Level.Text);
            }
            updateStats();
            updateLegality();
        }

        private void clickLevel(object sender, EventArgs e)
        {
            if (ModifierKeys == Keys.Control)
            {
                ((MaskedTextBox)sender).Text = "100";
            }
        }

        private void updateNatureModification(object sender, EventArgs e)
        {
            if (sender != CB_Nature) return;
            int nature = WinFormsUtil.getIndex(CB_Nature);
            int incr = nature / 5;
            int decr = nature % 5;

            System.Windows.Forms.Label[] labarray = { Label_ATK, Label_DEF, Label_SPE, Label_SPA, Label_SPD };
            // Reset Label Colors
            foreach (System.Windows.Forms.Label label in labarray)
                label.ResetForeColor();

            // Set Colored StatLabels only if Nature isn't Neutral
            NatureTip.SetToolTip(CB_Nature,
                incr != decr
                    ? $"+{labarray[incr].Text} / -{labarray[decr].Text}".Replace(":", "")
                    : "-/-");
        }

        private void clickFriendship(object sender, EventArgs e)
        {
            if (ModifierKeys == Keys.Control) // prompt to reset
                TB_Friendship.Text = pkm.CurrentFriendship.ToString();
            else
                TB_Friendship.Text = TB_Friendship.Text == "255" ? SAV.Personal[pkm.Species].BaseFriendship.ToString() : "255";
        }

        private void update255_MTB(object sender, EventArgs e)
        {
            if (Util.ToInt32(((MaskedTextBox)sender).Text) > byte.MaxValue)
                ((MaskedTextBox)sender).Text = "255";
        }

        private void setForms()
        {
            int species = WinFormsUtil.getIndex(CB_Species);
            if (SAV.Generation < 4 && species != 201)
            {
                Label_Form.Visible = CB_Form.Visible = CB_Form.Enabled = false;
                return;
            }

            bool hasForms = SAV.Personal[species].HasFormes || new[] { 201, 664, 665, 414 }.Contains(species);
            CB_Form.Enabled = CB_Form.Visible = Label_Form.Visible = hasForms;

            if (!hasForms)
                return;

            var ds = PKX.getFormList(species, GameInfo.Strings.types, GameInfo.Strings.forms, gendersymbols, SAV.Generation).ToList();
            if (ds.Count == 1 && string.IsNullOrEmpty(ds[0])) // empty (Alolan Totems)
                CB_Form.Enabled = CB_Form.Visible = Label_Form.Visible = false;
            else CB_Form.DataSource = ds;
        }

        private void updateForm(object sender, EventArgs e)
        {
            if (CB_Form == sender && fieldsLoaded)
                pkm.AltForm = CB_Form.SelectedIndex;

            updateGender();
            updateStats();
            // Repopulate Abilities if Species Form has different abilities
            setAbilityList();

            // Gender Forms
            if (WinFormsUtil.getIndex(CB_Species) == 201 && fieldsLoaded)
            {
                if (SAV.Generation == 3)
                {
                    pkm.setPIDUnown3(CB_Form.SelectedIndex);
                    TB_PID.Text = pkm.PID.ToString("X8");
                }
                else if (SAV.Generation == 2)
                {
                    int desiredForm = CB_Form.SelectedIndex;
                    while (pkm.AltForm != desiredForm)
                        updateRandomIVs(null, null);
                }
            }
            else if (PKX.getGender(CB_Form.Text) < 2)
            {
                if (CB_Form.Items.Count == 2) // actually M/F; Pumpkaboo formes in German are S,M,L,XL
                    Label_Gender.Text = gendersymbols[PKX.getGender(CB_Form.Text)];
            }

            if (changingFields)
                return;

            if (fieldsLoaded)
                getQuickFiller(dragout);
        }

        private void setAbilityList()
        {
            if (SAV.Generation < 3) // no abilities
                return;

            int formnum = 0;
            int species = WinFormsUtil.getIndex(CB_Species);
            if (SAV.Generation > 3) // has forms
                formnum = CB_Form.SelectedIndex;

            int[] abils = SAV.Personal.getAbilities(species, formnum);
            if (abils[1] == 0 && SAV.Generation != 3)
                abils[1] = abils[0];
            string[] abilIdentifier = { " (1)", " (2)", " (H)" };
            List<string> ability_list = abils.Where(a => a != 0).Select((t, i) => GameInfo.Strings.abilitylist[t] + abilIdentifier[i]).ToList();
            if (!ability_list.Any())
                ability_list.Add(GameInfo.Strings.abilitylist[0] + abilIdentifier[0]);

            bool tmp = fieldsLoaded;
            fieldsLoaded = false;
            int abil = CB_Ability.SelectedIndex;
            CB_Ability.DataSource = ability_list;
            CB_Ability.SelectedIndex = abil < 0 || abil >= CB_Ability.Items.Count ? 0 : abil;
            fieldsLoaded = tmp;
        }

        private void updateIsEgg(object sender, EventArgs e)
        {
            // Display hatch counter if it is an egg, Display Friendship if it is not.
            Label_HatchCounter.Visible = CHK_IsEgg.Checked && SAV.Generation > 1;
            Label_Friendship.Visible = !CHK_IsEgg.Checked && SAV.Generation > 1;

            if (!fieldsLoaded)
                return;

            pkm.IsEgg = CHK_IsEgg.Checked;
            if (CHK_IsEgg.Checked)
            {
                TB_Friendship.Text = "1";

                // If we are an egg, it won't have a met location.
                CHK_AsEgg.Checked = true;
                GB_EggConditions.Enabled = true;

                CAL_MetDate.Value = new DateTime(2000, 01, 01);

                // if egg wasn't originally obtained by OT => Link Trade, else => None
                bool isTraded = SAV.OT != TB_OT.Text || SAV.TID != Util.ToInt32(TB_TID.Text) || SAV.SID != Util.ToInt32(TB_SID.Text);
                CB_MetLocation.SelectedIndex = isTraded ? 2 : 0;

                if (!CHK_Nicknamed.Checked)
                {
                    TB_Nickname.Text = PKX.getSpeciesName(0, WinFormsUtil.getIndex(CB_Language));
                    CHK_Nicknamed.Checked = true;
                }
            }
            else // Not Egg
            {
                if (!CHK_Nicknamed.Checked)
                    updateNickname(null, null);

                TB_Friendship.Text = SAV.Personal[WinFormsUtil.getIndex(CB_Species)].BaseFriendship.ToString();

                if (CB_EggLocation.SelectedIndex == 0)
                {
                    CAL_EggDate.Value = new DateTime(2000, 01, 01);
                    CHK_AsEgg.Checked = false;
                    GB_EggConditions.Enabled = false;
                }

                if (TB_Nickname.Text == PKX.getSpeciesName(0, WinFormsUtil.getIndex(CB_Language)))
                    CHK_Nicknamed.Checked = false;
            }

            updateNickname(null, null);
            getQuickFiller(dragout);
        }

        private void updatePKRSstrain(object sender, EventArgs e)
        {
            // Change the PKRS Days to the legal bounds.
            int currentDuration = CB_PKRSDays.SelectedIndex;
            CB_PKRSDays.Items.Clear();
            foreach (int day in Enumerable.Range(0, CB_PKRSStrain.SelectedIndex % 4 + 2)) CB_PKRSDays.Items.Add(day);

            // Set the days back if they're legal, else set it to 1. (0 always passes).
            CB_PKRSDays.SelectedIndex = currentDuration < CB_PKRSDays.Items.Count ? currentDuration : 1;

            if (CB_PKRSStrain.SelectedIndex != 0) return;

            // Never Infected
            CB_PKRSDays.SelectedIndex = 0;
            CHK_Cured.Checked = false;
            CHK_Infected.Checked = false;
        }

        private void updatePKRSInfected(object sender, EventArgs e)
        {
            if (!fieldsInitialized) return;
            if (CHK_Cured.Checked && !CHK_Infected.Checked) { CHK_Cured.Checked = false; return; }
            if (CHK_Cured.Checked) return;
            Label_PKRS.Visible = CB_PKRSStrain.Visible = CHK_Infected.Checked;
            if (!CHK_Infected.Checked) { CB_PKRSStrain.SelectedIndex = 0; CB_PKRSDays.SelectedIndex = 0; Label_PKRSdays.Visible = CB_PKRSDays.Visible = false; }
            else if (CB_PKRSStrain.SelectedIndex == 0) { CB_PKRSStrain.SelectedIndex = 1; Label_PKRSdays.Visible = CB_PKRSDays.Visible = true; updatePKRSCured(sender, e); }

            // if not cured yet, days > 0
            if (CHK_Infected.Checked && CB_PKRSDays.SelectedIndex == 0) CB_PKRSDays.SelectedIndex++;
        }

        private void updatePKRSCured(object sender, EventArgs e)
        {
            if (!fieldsInitialized) return;
            // Cured PokeRus is toggled
            if (CHK_Cured.Checked)
            {
                // Has Had PokeRus
                Label_PKRSdays.Visible = CB_PKRSDays.Visible = false;
                CB_PKRSDays.SelectedIndex = 0;

                Label_PKRS.Visible = CB_PKRSStrain.Visible = true;
                CHK_Infected.Checked = true;

                // If we're cured we have to have a strain infection.
                if (CB_PKRSStrain.SelectedIndex == 0)
                    CB_PKRSStrain.SelectedIndex = 1;
            }
            else if (!CHK_Infected.Checked)
            {
                // Not Infected, Disable the other
                Label_PKRS.Visible = CB_PKRSStrain.Visible = false;
                CB_PKRSStrain.SelectedIndex = 0;
            }
            else
            {
                // Still Infected for a duration
                Label_PKRSdays.Visible = CB_PKRSDays.Visible = true;
                CB_PKRSDays.SelectedValue = 1;
            }
            // if not cured yet, days > 0
            if (!CHK_Cured.Checked && CHK_Infected.Checked && CB_PKRSDays.SelectedIndex == 0)
                CB_PKRSDays.SelectedIndex++;

            setMarkings();
        }

        private void updatePKRSdays(object sender, EventArgs e)
        {
            if (CB_PKRSDays.SelectedIndex != 0) return;

            // If no days are selected
            if (CB_PKRSStrain.SelectedIndex == 0)
                CHK_Cured.Checked = CHK_Infected.Checked = false; // No Strain = Never Cured / Infected, triggers Strain update
            else CHK_Cured.Checked = true; // Any Strain = Cured
        }

        private void updateCountry(object sender, EventArgs e)
        {
            if (WinFormsUtil.getIndex(sender as ComboBox) > 0)
                setCountrySubRegion(CB_SubRegion, "sr_" + WinFormsUtil.getIndex(sender as ComboBox).ToString("000"));
        }

        internal static void setCountrySubRegion(ComboBox CB, string type)
        {
            int index = CB.SelectedIndex;
            // fix for Korean / Chinese being swapped
            string cl = pkhexlang + "";
            cl = cl == "zh" ? "ko" : cl == "ko" ? "zh" : cl;

            CB.DataSource = Util.getCBList(type, cl);

            if (index > 0 && index < CB.Items.Count && fieldsInitialized)
                CB.SelectedIndex = index;
        }

        // Met tab
        private void updateOriginGame(object sender, EventArgs e)
        {
            GameVersion Version = (GameVersion)WinFormsUtil.getIndex(CB_GameOrigin);

            // check if differs
            GameVersion newTrack = GameUtil.getMetLocationVersionGroup(Version);
            if (newTrack != origintrack)
            {
                var met_list = GameInfo.getLocationList(Version, SAV.Generation, egg: false);
                CB_MetLocation.DisplayMember = "Text";
                CB_MetLocation.ValueMember = "Value";
                CB_MetLocation.DataSource = new BindingSource(met_list, null);

                int metLoc = 0; // transporter or pal park for past gen pkm
                switch (newTrack)
                {
                    case GameVersion.GO: metLoc = 30012; break;
                    case GameVersion.RBY: metLoc = 30013; break;
                }
                if (metLoc != 0)
                    CB_MetLocation.SelectedValue = metLoc;
                else
                    CB_MetLocation.SelectedIndex = metLoc;

                var egg_list = GameInfo.getLocationList(Version, SAV.Generation, egg: true);
                CB_EggLocation.DisplayMember = "Text";
                CB_EggLocation.ValueMember = "Value";
                CB_EggLocation.DataSource = new BindingSource(egg_list, null);
                CB_EggLocation.SelectedIndex = CHK_AsEgg.Checked ? 1 : 0; // daycare : none

                origintrack = newTrack;

                // Stretch C/XD met location dropdowns
                int width = CB_EggLocation.DropDownWidth;
                if (Version == GameVersion.CXD && SAV.Generation == 3)
                    width = 2 * width;
                CB_MetLocation.DropDownWidth = width;
            }

            // Visibility logic for Gen 4 encounter type; only show for Gen 4 Pokemon.
            if (SAV.Generation >= 4)
            {
                bool g4 = Version >= GameVersion.HG && Version <= GameVersion.Pt;
                if ((int)Version == 9) // invalid
                    g4 = false;
                CB_EncounterType.Visible = Label_EncounterType.Visible = g4 && SAV.Generation < 7;
                if (!g4)
                    CB_EncounterType.SelectedValue = 0;
            }

            if (!fieldsLoaded)
                return;
            pkm.Version = (int)Version;
            setMarkings(); // Set/Remove KB marking
            updateLegality();
        }

        private void clickMetLocation(object sender, EventArgs e)
        {
            pkm = preparePKM();
            updateLegality(skipMoveRepop: true);
            if (Legality.Valid)
                return;

            var encounter = Legality.getSuggestedMetInfo();
            if (encounter == null || (pkm.Format >= 3 && encounter.Location < 0))
            {
                WinFormsUtil.Alert("Unable to provide a suggestion.");
                return;
            }

            int level = encounter.Level;
            int location = encounter.Location;
            int minlvl = Legal.getLowestLevel(pkm, encounter.Species);
            if (minlvl == 0)
                minlvl = level;

            if (pkm.CurrentLevel >= minlvl && pkm.Met_Level == level && pkm.Met_Location == location)
                return;
            if (minlvl < level)
                minlvl = level;

            var suggestion = new List<string> { "Suggested:" };
            if (pkm.Format >= 3)
            {
                var met_list = GameInfo.getLocationList((GameVersion)pkm.Version, SAV.Generation, egg: false);
                var locstr = met_list.FirstOrDefault(loc => loc.Value == location)?.Text;
                suggestion.Add($"Met Location: {locstr}");
                suggestion.Add($"Met Level: {level}");
            }
            if (pkm.CurrentLevel < minlvl)
                suggestion.Add($"Current Level: {minlvl}");

            if (suggestion.Count == 1) // no suggestion
                return;

            string suggest = string.Join(Environment.NewLine, suggestion);
            if (WinFormsUtil.Prompt(MessageBoxButtons.YesNo, suggest) != DialogResult.Yes)
                return;

            if (pkm.Format >= 3)
            {
                TB_MetLevel.Text = level.ToString();
                CB_MetLocation.SelectedValue = location;
            }

            if (pkm.CurrentLevel < minlvl)
                TB_Level.Text = minlvl.ToString();

            pkm = preparePKM();
            updateLegality(skipMoveRepop: true);
        }

        private void validateLocation(object sender, EventArgs e)
        {
            validateComboBox(sender);
            if (!fieldsLoaded)
                return;

            pkm.Met_Location = WinFormsUtil.getIndex(CB_MetLocation);
            pkm.Egg_Location = WinFormsUtil.getIndex(CB_EggLocation);
            updateLegality();
        }

        private void updateBall(object sender, EventArgs e)
        {
            PB_Ball.Image = PKMUtil.getBallSprite(WinFormsUtil.getIndex(CB_Ball));
        }

        private void updateMetAsEgg(object sender, EventArgs e)
        {
            GB_EggConditions.Enabled = CHK_AsEgg.Checked;
            if (CHK_AsEgg.Checked)
            {
                if (!fieldsLoaded)
                    return;

                CAL_EggDate.Value = DateTime.Now;
                CB_EggLocation.SelectedIndex = 1;
                return;
            }
            // Remove egg met data
            CHK_IsEgg.Checked = false;
            CAL_EggDate.Value = new DateTime(2000, 01, 01);
            CB_EggLocation.SelectedValue = 0;

            updateLegality();
        }

        // Stats tab
        private void clickStatLabel(object sender, MouseEventArgs e)
        {
            if (!(ModifierKeys == Keys.Control || ModifierKeys == Keys.Alt))
                return;

            if (sender == Label_SPC)
                sender = Label_SPA;
            int index = Array.IndexOf(new[] { Label_HP, Label_ATK, Label_DEF, Label_SPA, Label_SPD, Label_SPE }, sender);

            if (ModifierKeys == Keys.Alt) // EV
            {
                var mt = new[] { TB_HPEV, TB_ATKEV, TB_DEFEV, TB_SPAEV, TB_SPDEV, TB_SPEEV }[index];
                if (e.Button == MouseButtons.Left) // max
                    mt.Text = SAV.Generation >= 3
                        ? Math.Min(Math.Max(510 - Util.ToInt32(TB_EVTotal.Text) + Util.ToInt32(mt.Text), 0), 252).ToString()
                        : ushort.MaxValue.ToString();
                else // min
                    mt.Text = 0.ToString();
            }
            else
                new[] { TB_HPIV, TB_ATKIV, TB_DEFIV, TB_SPAIV, TB_SPDIV, TB_SPEIV }[index].Text =
                    (e.Button == MouseButtons.Left ? SAV.MaxIV : 0).ToString();
        }

        private void clickIV(object sender, EventArgs e)
        {
            if (ModifierKeys == Keys.Control)
                if (SAV.Generation < 7)
                    ((MaskedTextBox)sender).Text = SAV.MaxIV.ToString();
                else
                {
                    var index = Array.IndexOf(new[] { TB_HPIV, TB_ATKIV, TB_DEFIV, TB_SPAIV, TB_SPDIV, TB_SPEIV }, sender);
                    pkm.HyperTrainInvert(index);
                    updateIVs(sender, e);
                }
            else if (ModifierKeys == Keys.Alt)
                ((MaskedTextBox)sender).Text = 0.ToString();
        }

        private void updateIVs(object sender, EventArgs e)
        {
            if (changingFields || !fieldsInitialized) return;
            if (sender != null && Util.ToInt32(((MaskedTextBox)sender).Text) > SAV.MaxIV)
                ((MaskedTextBox)sender).Text = SAV.MaxIV.ToString("00");

            changingFields = true;

            // Update IVs
            pkm.IV_HP = Util.ToInt32(TB_HPIV.Text);
            pkm.IV_ATK = Util.ToInt32(TB_ATKIV.Text);
            pkm.IV_DEF = Util.ToInt32(TB_DEFIV.Text);
            pkm.IV_SPE = Util.ToInt32(TB_SPEIV.Text);
            pkm.IV_SPA = Util.ToInt32(TB_SPAIV.Text);
            pkm.IV_SPD = Util.ToInt32(TB_SPDIV.Text);

            var IV_Boxes = new[] { TB_HPIV, TB_ATKIV, TB_DEFIV, TB_SPAIV, TB_SPDIV, TB_SPEIV };
            var HT_Vals = new[] { pkm.HT_HP, pkm.HT_ATK, pkm.HT_DEF, pkm.HT_SPA, pkm.HT_SPD, pkm.HT_SPE };
            for (int i = 0; i < IV_Boxes.Length; i++)
                if (HT_Vals[i])
                    IV_Boxes[i].BackColor = Color.LightGreen;
                else
                    IV_Boxes[i].ResetBackColor();

            if (SAV.Generation < 3)
            {
                TB_HPIV.Text = pkm.IV_HP.ToString();
                TB_SPDIV.Text = TB_SPAIV.Text;
                if (SAV.Generation == 2)
                {
                    Label_Gender.Text = gendersymbols[pkm.Gender];
                    Label_Gender.ForeColor = pkm.Gender == 2
                        ? Label_Species.ForeColor
                        : (pkm.Gender == 1 ? Color.Red : Color.Blue);
                    if (pkm.Species == 201 && e != null) // Unown
                        CB_Form.SelectedIndex = pkm.AltForm;
                }
                setIsShiny(null);
                getQuickFiller(dragout);
            }

            CB_HPType.SelectedValue = pkm.HPType;
            changingFields = false;

            // Potential Reading
            L_Potential.Text = (new[] { "★☆☆☆", "★★☆☆", "★★★☆", "★★★★" })[pkm.PotentialRating];

            TB_IVTotal.Text = pkm.IVs.Sum().ToString();

            int characteristic = pkm.Characteristic;
            L_Characteristic.Visible = Label_CharacteristicPrefix.Visible = characteristic > -1;
            if (characteristic > -1)
                L_Characteristic.Text = GameInfo.Strings.characteristics[pkm.Characteristic];
            updateStats();
        }

        private void clickEV(object sender, EventArgs e)
        {
            MaskedTextBox mt = (MaskedTextBox)sender;
            if (ModifierKeys == Keys.Control) // EV
                mt.Text = SAV.Generation >= 3
                    ? Math.Min(Math.Max(510 - Util.ToInt32(TB_EVTotal.Text) + Util.ToInt32(mt.Text), 0), 252).ToString()
                    : ushort.MaxValue.ToString();
            else if (ModifierKeys == Keys.Alt)
                mt.Text = 0.ToString();
        }

        private void updateEVs(object sender, EventArgs e)
        {
            if (sender is MaskedTextBox)
            {
                MaskedTextBox m = (MaskedTextBox)sender;
                if (Util.ToInt32(m.Text) > SAV.MaxEV)
                { m.Text = SAV.MaxEV.ToString(); return; } // recursive on text set
            }

            changingFields = true;
            if (sender == TB_HPEV) pkm.EV_HP = Util.ToInt32(TB_HPEV.Text);
            else if (sender == TB_ATKEV) pkm.EV_ATK = Util.ToInt32(TB_ATKEV.Text);
            else if (sender == TB_DEFEV) pkm.EV_DEF = Util.ToInt32(TB_DEFEV.Text);
            else if (sender == TB_SPEEV) pkm.EV_SPE = Util.ToInt32(TB_SPEEV.Text);
            else if (sender == TB_SPAEV) pkm.EV_SPA = Util.ToInt32(TB_SPAEV.Text);
            else if (sender == TB_SPDEV) pkm.EV_SPD = Util.ToInt32(TB_SPDEV.Text);

            if (SAV.Generation < 3)
                TB_SPDEV.Text = TB_SPAEV.Text;

            int evtotal = pkm.EVs.Sum();

            if (evtotal > 510) // Background turns Red
                TB_EVTotal.BackColor = Color.Red;
            else if (evtotal == 510) // Maximum EVs
                TB_EVTotal.BackColor = Color.Honeydew;
            else if (evtotal == 508) // Fishy EVs
                TB_EVTotal.BackColor = Color.LightYellow;
            else TB_EVTotal.BackColor = TB_IVTotal.BackColor;

            TB_EVTotal.Text = evtotal.ToString();
            EVTip.SetToolTip(TB_EVTotal, $"Remaining: {510 - evtotal}");
            changingFields = false;
            updateStats();
        }

        private void updateHPType(object sender, EventArgs e)
        {
            if (changingFields || !fieldsInitialized) return;
            changingFields = true;
            int[] ivs =
            {
                Util.ToInt32(TB_HPIV.Text), Util.ToInt32(TB_ATKIV.Text), Util.ToInt32(TB_DEFIV.Text),
                Util.ToInt32(TB_SPAIV.Text), Util.ToInt32(TB_SPDIV.Text), Util.ToInt32(TB_SPEIV.Text)
            };

            // Change IVs to match the new Hidden Power
            int[] newIVs = PKX.setHPIVs(WinFormsUtil.getIndex(CB_HPType), ivs);
            TB_HPIV.Text = newIVs[0].ToString();
            TB_ATKIV.Text = newIVs[1].ToString();
            TB_DEFIV.Text = newIVs[2].ToString();
            TB_SPAIV.Text = newIVs[3].ToString();
            TB_SPDIV.Text = newIVs[4].ToString();
            TB_SPEIV.Text = newIVs[5].ToString();

            // Refresh View
            changingFields = false;
            updateIVs(null, null);
        }

        private void updateRandomIVs(object sender, EventArgs e)
        {
            changingFields = true;
            if (ModifierKeys == Keys.Control || ModifierKeys == Keys.Shift)
            {
                // Max IVs
                TB_HPIV.Text = SAV.MaxIV.ToString();
                TB_ATKIV.Text = SAV.MaxIV.ToString();
                TB_DEFIV.Text = SAV.MaxIV.ToString();
                TB_SPAIV.Text = SAV.MaxIV.ToString();
                TB_SPDIV.Text = SAV.MaxIV.ToString();
                TB_SPEIV.Text = SAV.MaxIV.ToString();
            }
            else
            {
                bool IV3 = Legal.Legends.Contains(pkm.Species) || Legal.SubLegends.Contains(pkm.Species);

                int[] IVs = new int[6];
                for (int i = 0; i < 6; i++)
                    IVs[i] = (int)(Util.rnd32() & SAV.MaxIV);
                if (IV3)
                    for (int i = 0; i < 3; i++)
                        IVs[i] = SAV.MaxIV;
                Util.Shuffle(IVs); // Randomize IV order

                var IVBoxes = new[] { TB_HPIV, TB_ATKIV, TB_DEFIV, TB_SPAIV, TB_SPDIV, TB_SPEIV };
                for (int i = 0; i < 6; i++)
                    IVBoxes[i].Text = IVs[i].ToString();
            }
            changingFields = false;
            updateIVs(null, e);
        }

        private void updateRandomEVs(object sender, EventArgs e)
        {
            changingFields = true;

            var tb = new[] { TB_HPEV, TB_ATKEV, TB_DEFEV, TB_SPAEV, TB_SPDEV, TB_SPEEV };
            bool zero = ModifierKeys == Keys.Control || ModifierKeys == Keys.Shift;
            var evs = zero ? new uint[6] : PKX.getRandomEVs(SAV.Generation);
            for (int i = 0; i < 6; i++)
                tb[i].Text = evs[i].ToString();

            changingFields = false;
            updateEVs(null, null);
        }

        private void updateStats()
        {
            // Generate the stats.
            pkm.setStats(pkm.getStats(SAV.Personal.getFormeEntry(pkm.Species, pkm.AltForm)));

            Stat_HP.Text = pkm.Stat_HPCurrent.ToString();
            Stat_ATK.Text = pkm.Stat_ATK.ToString();
            Stat_DEF.Text = pkm.Stat_DEF.ToString();
            Stat_SPA.Text = pkm.Stat_SPA.ToString();
            Stat_SPD.Text = pkm.Stat_SPD.ToString();
            Stat_SPE.Text = pkm.Stat_SPE.ToString();

            // Recolor the Stat Labels based on boosted stats.
            {
                int incr = pkm.Nature / 5;
                int decr = pkm.Nature % 5;

                System.Windows.Forms.Label[] labarray = { Label_ATK, Label_DEF, Label_SPE, Label_SPA, Label_SPD };
                // Reset Label Colors
                foreach (System.Windows.Forms.Label label in labarray)
                    label.ResetForeColor();

                // Set Colored StatLabels only if Nature isn't Neutral
                if (incr == decr) return;
                labarray[incr].ForeColor = Color.Red;
                labarray[decr].ForeColor = Color.Blue;
            }
        }

        // Moves tab
        private void validateMovePaint(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            var i = (ComboItem)(sender as ComboBox).Items[e.Index];
            var moves = Legality.AllSuggestedMovesAndRelearn;
            bool vm = moves != null && moves.Contains(i.Value);

            bool current = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            Brush tBrush = current ? SystemBrushes.HighlightText : new SolidBrush(e.ForeColor);
            Brush brush = current ? SystemBrushes.Highlight : vm ? Brushes.PaleGreen : new SolidBrush(e.BackColor);

            e.Graphics.FillRectangle(brush, e.Bounds);
            e.Graphics.DrawString(i.Text, e.Font, tBrush, e.Bounds, StringFormat.GenericDefault);
            if (current) return;
            tBrush.Dispose();
            if (!vm)
                brush.Dispose();
        }

        private void validateMove(object sender, EventArgs e)
        {
            if (!fieldsInitialized)
                return;
            validateComboBox(sender);
            if (!fieldsLoaded)
                return;

            if (new[] { CB_Move1, CB_Move2, CB_Move3, CB_Move4 }.Contains(sender)) // Move
                updatePP(sender, e);

            // Legality
            pkm.Moves = new[] { CB_Move1, CB_Move2, CB_Move3, CB_Move4 }.Select(WinFormsUtil.getIndex).ToArray();
            pkm.RelearnMoves = new[] { CB_RelearnMove1, CB_RelearnMove2, CB_RelearnMove3, CB_RelearnMove4 }.Select(WinFormsUtil.getIndex).ToArray();
            updateLegality(skipMoveRepop: true);
        }

        private void clickMoves(object sender, EventArgs e)
        {
            updateLegality(skipMoveRepop: true);
            if (sender == GB_CurrentMoves)
            {
                bool random = ModifierKeys == Keys.Control;
                int[] m = Legality.getSuggestedMoves(tm: random, tutor: random, reminder: random);
                if (m == null)
                { WinFormsUtil.Alert("Suggestions are not enabled for this PKM format."); return; }

                if (random)
                    Util.Shuffle(m);
                if (m.Length > 4)
                    m = m.Skip(m.Length - 4).ToArray();
                Array.Resize(ref m, 4);

                if (pkm.Moves.SequenceEqual(m))
                    return;

                string r = string.Join(Environment.NewLine, m.Select(v => v >= GameInfo.Strings.movelist.Length ? "ERROR" : GameInfo.Strings.movelist[v]));
                if (DialogResult.Yes != WinFormsUtil.Prompt(MessageBoxButtons.YesNo, "Apply suggested current moves?", r))
                    return;

                CB_Move1.SelectedValue = m[0];
                CB_Move2.SelectedValue = m[1];
                CB_Move3.SelectedValue = m[2];
                CB_Move4.SelectedValue = m[3];
            }
            else if (sender == GB_RelearnMoves)
            {
                int[] m = Legality.getSuggestedRelearn();
                if (!pkm.WasEgg && !pkm.WasEvent && !pkm.WasEventEgg && !pkm.WasLink)
                {
                    var encounter = Legality.getSuggestedMetInfo();
                    if (encounter != null)
                        m = encounter.Relearn;
                }

                if (pkm.RelearnMoves.SequenceEqual(m))
                    return;

                string r = string.Join(Environment.NewLine, m.Select(v => v >= GameInfo.Strings.movelist.Length ? "ERROR" : GameInfo.Strings.movelist[v]));
                if (DialogResult.Yes != WinFormsUtil.Prompt(MessageBoxButtons.YesNo, "Apply suggested relearn moves?", r))
                    return;

                CB_RelearnMove1.SelectedValue = m[0];
                CB_RelearnMove2.SelectedValue = m[1];
                CB_RelearnMove3.SelectedValue = m[2];
                CB_RelearnMove4.SelectedValue = m[3];
            }

            updateLegality();
        }

        private void updatePP(object sender, EventArgs e)
        {
            ComboBox[] cbs = { CB_Move1, CB_Move2, CB_Move3, CB_Move4 };
            ComboBox[] pps = { CB_PPu1, CB_PPu2, CB_PPu3, CB_PPu4 };
            MaskedTextBox[] tbs = { TB_PP1, TB_PP2, TB_PP3, TB_PP4 };
            int index = Array.IndexOf(cbs, sender);
            if (index < 0)
                index = Array.IndexOf(pps, sender);
            if (index < 0)
                return;

            int move = WinFormsUtil.getIndex(cbs[index]);
            int pp = pps[index].SelectedIndex;
            if (move == 0 && pp != 0)
            {
                pps[index].SelectedIndex = 0;
                return; // recursively triggers
            }
            tbs[index].Text = pkm.getMovePP(move, pp).ToString();
        }

        private void clickPPUps(object sender, EventArgs e)
        {
            CB_PPu1.SelectedIndex = ModifierKeys != Keys.Control && WinFormsUtil.getIndex(CB_Move1) > 0 ? 3 : 0;
            CB_PPu2.SelectedIndex = ModifierKeys != Keys.Control && WinFormsUtil.getIndex(CB_Move2) > 0 ? 3 : 0;
            CB_PPu3.SelectedIndex = ModifierKeys != Keys.Control && WinFormsUtil.getIndex(CB_Move3) > 0 ? 3 : 0;
            CB_PPu4.SelectedIndex = ModifierKeys != Keys.Control && WinFormsUtil.getIndex(CB_Move4) > 0 ? 3 : 0;
        }

        // OT/Misc tab
        private void clickGT(object sender, EventArgs e)
        {
            if (!GB_nOT.Visible)
                return;
            if (sender == GB_OT)
            {
                pkm.CurrentHandler = 0;
                GB_OT.BackgroundImage = mixedHighlight;
                GB_nOT.BackgroundImage = null;
            }
            else if (TB_OTt2.Text.Length > 0)
            {
                pkm.CurrentHandler = 1;
                GB_OT.BackgroundImage = null;
                GB_nOT.BackgroundImage = mixedHighlight;
            }
            TB_Friendship.Text = pkm.CurrentFriendship.ToString();
        }

        private void updateTSV(object sender, EventArgs e)
        {
            if (SAV.Generation < 6)
                return;

            var TSV = pkm.TSV.ToString("0000");
            string IDstr = "TSV: " + TSV;
            if (SAV.Generation > 6)
                IDstr += Environment.NewLine + "G7TID: " + pkm.TrainerID7.ToString("000000");

            Tip1.SetToolTip(TB_TID, IDstr);
            Tip2.SetToolTip(TB_SID, IDstr);

            pkm.PID = Util.getHEXval(TB_PID.Text);
            var PSV = pkm.PSV;
            Tip3.SetToolTip(TB_PID, "PSV: " + PSV.ToString("0000"));
        }

        private void update_ID(object sender, EventArgs e)
        {
            // Trim out nonhex characters
            TB_PID.Text = Util.getHEXval(TB_PID.Text).ToString("X8");
            TB_EC.Text = Util.getHEXval(TB_EC.Text).ToString("X8");

            // Max TID/SID is 65535
            if (Util.ToUInt32(TB_TID.Text) > ushort.MaxValue) TB_TID.Text = "65535";
            if (Util.ToUInt32(TB_SID.Text) > ushort.MaxValue) TB_SID.Text = "65535";

            setIsShiny(sender);
            getQuickFiller(dragout);
            updateIVs(null, null);   // If the EC is changed, EC%6 (Characteristic) might be changed. 
            TB_PID.Select(60, 0);   // position cursor at end of field
            if (SAV.Generation <= 4 && fieldsLoaded)
            {
                fieldsLoaded = false;
                pkm.PID = Util.getHEXval(TB_PID.Text);
                CB_Nature.SelectedValue = pkm.Nature;
                Label_Gender.Text = gendersymbols[pkm.Gender];
                Label_Gender.ForeColor = pkm.Gender == 2 ? Label_Species.ForeColor : (pkm.Gender == 1 ? Color.Red : Color.Blue);
                fieldsLoaded = true;
            }
        }

        private void clickOT(object sender, EventArgs e)
        {
            if (!SAV.Exportable)
                return;

            // Get Save Information
            TB_OT.Text = SAV.OT;
            Label_OTGender.Text = gendersymbols[SAV.Gender % 2];
            Label_OTGender.ForeColor = SAV.Gender == 1 ? Color.Red : Color.Blue;
            TB_TID.Text = SAV.TID.ToString("00000");
            TB_SID.Text = SAV.SID.ToString("00000");

            if (SAV.Game >= 0)
                CB_GameOrigin.SelectedValue = SAV.Game;
            if (SAV.Language >= 0)
                CB_Language.SelectedValue = SAV.Language;
            if (SAV.HasGeolocation)
            {
                CB_3DSReg.SelectedValue = SAV.ConsoleRegion;
                CB_Country.SelectedValue = SAV.Country;
                CB_SubRegion.SelectedValue = SAV.SubRegion;
            }
            updateNickname(null, null);
        }

        private void clickTRGender(object sender, EventArgs e)
        {
            System.Windows.Forms.Label lbl = sender as System.Windows.Forms.Label;
            if (!string.IsNullOrWhiteSpace(lbl?.Text)) // set gender label (toggle M/F)
            {
                int gender = PKX.getGender(lbl.Text) ^ 1;
                lbl.Text = gendersymbols[gender];
                lbl.ForeColor = gender == 1 ? Color.Red : Color.Blue;
            }
        }

        private void clickCT(object sender, EventArgs e)
        {
            if (TB_OTt2.Text.Length > 0)
                Label_CTGender.Text = gendersymbols[SAV.Gender % 2];
        }

        private void updateNotOT(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TB_OTt2.Text))
            {
                clickGT(GB_OT, null); // Switch CT over to OT.
                Label_CTGender.Text = "";
                TB_Friendship.Text = pkm.CurrentFriendship.ToString();
            }
            else if (string.IsNullOrWhiteSpace(Label_CTGender.Text))
                Label_CTGender.Text = gendersymbols[0];
        }

        private void updateExtraByteIndex(object sender, EventArgs e)
        {
            if (CB_ExtraBytes.Items.Count == 0)
                return;
            // Byte changed, need to refresh the Text box for the byte's value.
            TB_ExtraByte.Text = pkm.Data[Convert.ToInt32(CB_ExtraBytes.Text, 16)].ToString();
        }

        private void updateExtraByteValue(object sender, EventArgs e)
        {
            if (CB_ExtraBytes.Items.Count == 0)
                return;
            // Changed Extra Byte's Value
            if (Util.ToInt32(((MaskedTextBox)sender).Text) > byte.MaxValue)
                ((MaskedTextBox)sender).Text = "255";

            int value = Util.ToInt32(TB_ExtraByte.Text);
            int offset = Convert.ToInt32(CB_ExtraBytes.Text, 16);
            pkm.Data[offset] = (byte)value;
        }

        private void clickMarking(object sender, EventArgs e)
        {
            PictureBox[] pba = { PB_Mark1, PB_Mark2, PB_Mark3, PB_Mark4, PB_Mark5, PB_Mark6 };
            int index = Array.IndexOf(pba, sender);

            // Handling Gens 3-6
            int[] markings = pkm.Markings;
            switch (pkm.Format)
            {
                case 3:
                case 4:
                case 5:
                case 6: // on/off
                    markings[index] ^= 1; // toggle
                    pkm.Markings = markings;
                    break;
                case 7: // 0 (none) | 1 (blue) | 2 (pink)
                    markings[index] = (markings[index] + 1) % 3; // cycle
                    pkm.Markings = markings;
                    break;
                default:
                    return;
            }
            setMarkings();
        }

        private void setMarkings()
        {
            Func<bool, double> getOpacity = b => b ? 1 : 0.175;
            PictureBox[] pba = { PB_Mark1, PB_Mark2, PB_Mark3, PB_Mark4, PB_Mark5, PB_Mark6 };
            for (int i = 0; i < pba.Length; i++)
                pba[i].Image = ImageUtil.ChangeOpacity(pba[i].InitialImage, getOpacity(pkm.Markings[i] != 0));

            PB_MarkShiny.Image = ImageUtil.ChangeOpacity(PB_MarkShiny.InitialImage, getOpacity(!BTN_Shinytize.Enabled));
            PB_MarkCured.Image = ImageUtil.ChangeOpacity(PB_MarkCured.InitialImage, getOpacity(CHK_Cured.Checked));

            PB_MarkPentagon.Image = ImageUtil.ChangeOpacity(PB_MarkPentagon.InitialImage, getOpacity(pkm.Gen6));

            // Gen7 Markings
            if (pkm.Format != 7)
                return;

            PB_MarkAlola.Image = ImageUtil.ChangeOpacity(PB_MarkAlola.InitialImage, getOpacity(pkm.Gen7));
            PB_MarkVC.Image = ImageUtil.ChangeOpacity(PB_MarkVC.InitialImage, getOpacity(pkm.VC));
            PB_MarkHorohoro.Image = ImageUtil.ChangeOpacity(PB_MarkHorohoro.InitialImage, getOpacity(pkm.Horohoro));

            for (int i = 0; i < pba.Length; i++)
            {
                switch (pkm.Markings[i])
                {
                    case 1:
                        pba[i].Image = ImageUtil.ChangeAllColorTo(pba[i].Image, Color.FromArgb(000, 191, 255));
                        break;
                    case 2:
                        pba[i].Image = ImageUtil.ChangeAllColorTo(pba[i].Image, Color.FromArgb(255, 117, 179));
                        break;
                }
            }
        }

        private void updateRandomEC(object sender, EventArgs e)
        {
            if (pkm.Format < 6)
                return;

            int wIndex = Array.IndexOf(Legal.WurmpleEvolutions, WinFormsUtil.getIndex(CB_Species));
            if (wIndex < 0)
            {
                TB_EC.Text = Util.rnd32().ToString("X8");
            }
            else
            {
                uint EC;
                bool valid;
                do
                {
                    EC = Util.rnd32();
                    uint evoVal;
                    switch (pkm.GenNumber)
                    {
                        case 3: evoVal = pkm.PID & 0xFFFF; break;
                        case 4:
                        case 5: evoVal = pkm.PID >> 16; break;
                        default: evoVal = pkm.EncryptionConstant >> 16; break;
                    }
                    evoVal = evoVal % 10 / 5;
                    valid = evoVal == wIndex / 2;
                } while (!valid);
                TB_EC.Text = EC.ToString("X8");
            }
            updateLegality();
        }

        private void openRibbons(object sender, EventArgs e)
        {
            new RibbonEditor().ShowDialog();
        }

        private void openMedals(object sender, EventArgs e)
        {
            new SuperTrainingEditor().ShowDialog();
        }

        private void openHistory(object sender, EventArgs e)
        {
            // Write back current values
            pkm.HT_Name = TB_OTt2.Text;
            pkm.OT_Name = TB_OT.Text;
            pkm.IsEgg = CHK_IsEgg.Checked;
            pkm.CurrentFriendship = Util.ToInt32(TB_Friendship.Text);
            new MemoryAmie().ShowDialog();
            TB_Friendship.Text = pkm.CurrentFriendship.ToString();
        }

        #endregion PKHeX Tabs

        #region Sub-forms

        // Tool start/finish
        private void Tool_Start()
        {
            txtLog.Clear();
            disableControls();
            Delg.SetEnabled(Tool_Script, false);
        }

        public void Tool_Finish()
        {
            if (isConnected)
            {
                enableControls();
            }
            Delg.SetEnabled(Tool_Script, true);
        }

        // Trainer Editor
        private void Tool_Trainer_Click(object sender, EventArgs e)
        {
            Tool_Start();
            new Edit_Trainer().ShowDialog();
        }

        // Item Editor
        private async void Tool_Items_Click(object sender, EventArgs e)
        {
            Tool_Start();
            iteminfo = await dumpItems();
            if (iteminfo == null)
            {
                MessageBox.Show("A error ocurred while dumping items", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Array.Copy(iteminfo, 0, SAV.Data, LookupTable.itemsLocation, LookupTable.itemsSize);
                new Edit_Items().ShowDialog();
            }
        }

        public async Task<byte[]> dumpItems()
        {
            Task<bool> worker = Program.helper.waitNTRmultiread(LookupTable.itemsOff, LookupTable.itemsSize);
            if (await worker)
            {
                return Program.helper.lastmultiread;
            }
            else
            {
                return null;
            }
        }

        // Remote Control
        private void Tool_Controls_Click(object sender, EventArgs e)
        {
            Tool_Start();
            new Remote_Control().Show();
        }

        // Filter Constructor
        private void Tools_Filter_Click(object sender, EventArgs e)
        {
            new Filter_Constructor().Show();
        }

        // Wonder Trade
        private void Tools_WonderTrade_Click(object sender, EventArgs e)
        {
            Tool_Start();
            Delg.SetCheckedRadio(radioBoxes, true);
            string folderPath = System.Windows.Forms.@Application.StartupPath + "\\" + FOLDERWT + "\\";
            (new FileInfo(folderPath)).Directory.Create();
            if (SAV.Generation == 6)
            {
                new Bot_WonderTrade6().Show();
            }
            else if (SAV.Generation == 7)
            {
                new Bot_WonderTrade7().Show();
            }
            else
            {
                Tool_Finish();
            }
        }

        // Breeding
        private void Tools_Breeding_Click(object sender, EventArgs e)
        {
            Tool_Start();
            Delg.SetCheckedRadio(radioBoxes, true);
            if (SAV.Generation == 6)
            {
                new Bot_Breeding6().Show();
            }
            else if (SAV.Generation == 7)
            {
                new Bot_Breeding7().Show();
            }
            else
            {
                Tool_Finish();
            }
        }

        // Soft-reset
        private void Tools_SoftReset_Click(object sender, EventArgs e)
        {
            Tool_Start();
            if (SAV.Generation == 6)
            {
                new Bot_SoftReset6().Show();
            }
            else if (SAV.Generation == 7)
            {
                new Bot_SoftReset7().Show();
            }
            else
            {
                Tool_Finish();
            }
        }

        // PokeDigger
        private void Tools_PokeDigger_Click(object sender, EventArgs e)
        {
            Tool_Start();
            new PokeDigger(pid, isConnected).ShowDialog();
        }

        // Script Builder
        private void Tool_Script_Click(object sender, EventArgs e)
        {
            Tool_Start();
            new ScriptBuilder().Show();
        }

        private void DumpInstructionsBtn_Click(object sender, EventArgs e)
        {
            if (radioOpponent.Checked)
            {
                new DumpOpponentHelp().Show();
            }
        }

        #endregion Sub-forms

        #region Bots

        public void botMode(bool state)
        {
            botWorking = state;
            if (state)
            {
                timer1.Interval = 500;
            }
            else
            {
                timer1.Interval = 1000;
                Delg.SetText(labelreset, "Starting number:");
            }
        }

        public void ScriptMode(bool state)
        {
            botWorking = state;
            if (state)
            {
                timer1.Interval = 250;
            }
            else
            {
                timer1.Interval = 1000;
            }
        }

        public async Task<PKM> ReadOpponent()
        {
            try
            {
                addtoLog("NTR: Read opponent pokémon data");
                oppdata = null;
                DataReadyWaiting myArgs = new DataReadyWaiting(new byte[0x1FFFF], waitoppData, null);
                waitingForData.Add(Program.scriptHelper.data(0x8800000, 0x1FFFF, pid), myArgs);
                int readcount = 0;
                for (readcount = 0; readcount < 100; readcount++)
                {
                    await Task.Delay(100);
                    if (lastlog.Contains("finished"))
                    {
                        break;
                    }
                }
                await Task.Delay(100);
                if (readcount >= 100 || oppdata == null)
                { // No read
                    addtoLog("NTR: Read failed");
                    return null;
                }
                PKM validator = new PK6(PKX.decryptArray(oppdata));
                if (validator.ChecksumValid && validator.Species > 0 && validator.Species <= MAXSPECIES)
                { // Valid pokemon
                    Program.helper.lastRead = validator.Checksum;
                    populateFields(validator);
                    addtoLog("NTR: Read sucessful - PID 0x" + validator.PID.ToString("X8"));
                    return validator;
                }
                else if (validator.ChecksumValid && validator.Species == 0)
                { // Empty slot
                    addtoLog("NTR: Empty pokémon data");
                    return SAV.BlankPKM;
                }
                else
                { // Invalid pokémon
                    addtoLog("NTR: Invalid pokémon data");
                    return null;
                }
            }
            catch (Exception ex)
            {
                addtoLog("NTR: Read failed with exception:");
                addtoLog(ex.Message);
                return null; // No data received
            }
        }

        public async Task<bool> Reconnect()
        {
            addtoLog("NTR: Reconnect");
            Program.scriptHelper.connect(host.Text, 8000);
            int waittimeout;
            for (waittimeout = 0; waittimeout < 20; waittimeout++)
            {
                await Task.Delay(500);
                if (lastlog.Contains("end of process list"))
                {
                    break;
                }
            }
            if (waittimeout < 20)
            {
                addtoLog("NTR: Reconnect sucessful");
                return true;
            }
            else
            {
                addtoLog("NTR: Reconnect failed");
                return false;
            }
        }

        public void SetRadioOpponent()
        {
            Delg.SetCheckedRadio(radioOpponent, true);
            if (SAV.Generation == 7)
            {
                Delg.SetValue(boxDump, 1);
                Delg.SetValue(slotDump, 1);
            }
        }

        public void SetRadioParty()
        {
            Delg.SetCheckedRadio(radioParty, true);
            Delg.SetValue(boxDump, 1);
            Delg.SetValue(slotDump, 2);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (botWorking)
            {
                DialogResult closewindows;
                closewindows = MessageBox.Show("A bot is currently wokring, do you still want to close the application?", "Bot in progress", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (closewindows == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        #endregion Bots
    }

    //Objects of this class contains an array for data that have been acquired, a delegate function 
    //to handle them and any additional arguments it might require.
    public class DataReadyWaiting
    {
        public byte[] data;
        public object arguments;
        public delegate void DataHandler(object data_arguments);
        public DataHandler handler;

        public DataReadyWaiting(byte[] data_, DataHandler handler_, object arguments_)
        {
            this.data = data_;
            this.handler = handler_;
            this.arguments = arguments_;
        }
    }

    public static class DragInfo
    {
        public static bool slotLeftMouseIsDown;
        public static bool slotRightMouseIsDown;
        public static bool slotDragDropInProgress;

        public static byte[] slotPkmSource;
        public static byte[] slotPkmDestination;

        public static object slotSource;
        public static int slotSourceOffset = -1;
        public static int slotSourceSlotNumber = -1;
        public static int slotSourceBoxNumber = -1;

        public static object slotDestination;
        public static int slotDestinationOffset = -1;
        public static int slotDestinationSlotNumber = -1;
        public static int slotDestinationBoxNumber = -1;

        public static Cursor Cursor;
        public static string CurrentPath;

        public static bool SameBox => slotSourceBoxNumber > -1 && slotSourceBoxNumber == slotDestinationBoxNumber;
        public static bool SameSlot => slotSourceSlotNumber == slotDestinationSlotNumber && slotSourceBoxNumber == slotDestinationBoxNumber;
        public static bool SourceValid => slotSourceSlotNumber > -1 && (slotSourceBoxNumber > -1 || SourceParty);
        public static bool DestinationValid => slotDestinationSlotNumber > -1 && (slotDestinationBoxNumber > -1 || DestinationParty);
        public static bool SourceParty => 30 <= slotSourceSlotNumber && slotSourceSlotNumber < 36;
        public static bool DestinationParty => 30 <= slotDestinationSlotNumber && slotDestinationSlotNumber < 36;

        // PKM Get Set
        public static PKM getPKMfromSource(SaveFile SAV)
        {
            int o = slotSourceOffset;
            return SourceParty ? SAV.getPartySlot(o) : SAV.getStoredSlot(o);
        }
        public static PKM getPKMfromDestination(SaveFile SAV)
        {
            int o = slotDestinationOffset;
            return DestinationParty ? SAV.getPartySlot(o) : SAV.getStoredSlot(o);
        }
        public static void setPKMtoSource(SaveFile SAV, PKM pk)
        {
            int o = slotSourceOffset;
            if (!SourceParty)
            { SAV.setStoredSlot(pk, o); return; }

            if (pk.Species == 0) // Empty Slot
            { SAV.deletePartySlot(slotSourceSlotNumber - 30); return; }

            if (pk.Stat_HPMax == 0) // Without Stats (Box)
            {
                pk.setStats(pk.getStats(SAV.Personal.getFormeEntry(pk.Species, pk.AltForm)));
                pk.Stat_Level = pk.CurrentLevel;
            }
            SAV.setPartySlot(pk, o);
        }
        public static void setPKMtoDestination(SaveFile SAV, PKM pk)
        {
            int o = slotDestinationOffset;
            if (!DestinationParty)
            { SAV.setStoredSlot(pk, o); return; }

            if (30 + SAV.PartyCount < slotDestinationSlotNumber)
            {
                o = SAV.getPartyOffset(SAV.PartyCount);
                slotDestinationSlotNumber = 30 + SAV.PartyCount;
            }
            if (pk.Stat_HPMax == 0) // Without Stats (Box/File)
            {
                pk.setStats(pk.getStats(SAV.Personal.getFormeEntry(pk.Species, pk.AltForm)));
                pk.Stat_Level = pk.CurrentLevel;
            }
            SAV.setPartySlot(pk, o);
        }

        public static void Reset()
        {
            slotLeftMouseIsDown = false;
            slotRightMouseIsDown = false;
            slotDragDropInProgress = false;

            slotPkmSource = null;
            slotSourceOffset = slotSourceSlotNumber = slotSourceBoxNumber = -1;
            slotPkmDestination = null;
            slotDestinationOffset = slotSourceBoxNumber = slotDestinationBoxNumber = -1;

            Cursor = null;
            CurrentPath = null;

            slotSource = null;
            slotDestination = null;
        }

        public static bool? WasDragParticipant(object form, int index)
        {
            if (slotDestinationBoxNumber != index && slotSourceBoxNumber != index)
                return null; // form was not watching box
            return slotSource == form || slotDestination == form; // form already updated?
        }
    }
}