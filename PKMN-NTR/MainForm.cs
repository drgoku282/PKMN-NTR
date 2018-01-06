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
using PKHeX.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using pkmn_ntr.Sub_forms.Scripting;
using System.ComponentModel;
using System.Media;

namespace pkmn_ntr
{
    public partial class MainForm : Form
    {
        #region Class variables

        //A "waiting room", where functions wait for data to be acquired. Entries are indexed by their sequence number. Once a request with a given sequence number is fulfilled, handleDataReady() uses information in DataReadyWaiting object to process the data.
        public static Dictionary<uint, DataReadyWaiting> waitingForData = new Dictionary<uint, DataReadyWaiting>();

        // Program-wide properties
        public readonly bool EnablePartyWrite;
        public readonly bool HaX;

        public bool IsConnected { get; set; }
        public bool IsLegal { get; set; }
        public PKM Pokemon
        {
            get
            {
                return PreparePKM();
            }
            set
            {
                PKME_Tabs.PopulateFields(value.Clone());
            }
        }

        // Game checks
        public bool IsXY
        {
            get
            {
                return SAV.Version == GameVersion.X || SAV.Version == GameVersion.Y;
            }
        }
        public bool IsORAS
        {
            get
            {
                return SAV.Version == GameVersion.OR || SAV.Version == GameVersion.AS;
            }
        }
        public bool IsSM
        {
            get
            {
                return SAV.Version == GameVersion.SN || SAV.Version == GameVersion.MN;
            }
        }
        public bool IsUSUM
        {
            get
            {
                return SAV.Version == GameVersion.US || SAV.Version == GameVersion.UM;
            }
        }

        // Structure for box/slot last position
        struct LastBoxSlot
        {
            public decimal Box { get; set; }
            public decimal Slot { get; set; }
        }

        // New program-wide variables for PKHeX.Core
        public SaveFile SAV;
        public PKMEditor PKME_Tabs;
        public byte[] fileinfo;
        public byte[] iteminfo;
        byte[] oppdata;
        public static string MGDatabasePath => Path.Combine(System.Windows.Forms.Application.StartupPath, "mgdb");

        // Program constants
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

        // Log handling
        public delegate void LogDelegate(string l);
        public LogDelegate delAddLog;

        // Bot variables
        public bool botWorking;
        public string lastlog;

        // Event handler variables
        private AutoResetEvent pollingCancelledEvent = new AutoResetEvent(false);
        private bool polling = false;
        public string slotChangeCommand = "";
        public string hpZeroCommand = "";

        #endregion Class variables

        #region Main window

        public MainForm()
        {
            // Read command line arguments to enable illegal mode and party editing
            string[] cmdargs = Environment.GetCommandLineArgs();
            HaX = cmdargs.Any(x => x.Trim('-').ToLower() == "hax");
            EnablePartyWrite = cmdargs.Any(x => x.Trim('-').ToLower() == "party");

            // Add event handlers for NTR and log
            Program.ntrClient.DataReady += HandleDataReady;
            Program.ntrClient.Connected += CheckConnection;
            Program.ntrClient.InfoReady += GetGame;
            delAddLog = new LogDelegate(Addlog);

            // Draw the window
            InitializeComponent();
            PKME_Tabs = new PKMEditor()
            {
                Location = new Point(318, 33),
                Size = new Size(280, 325)
            };
            InitializePKMEditor();
            Controls.Add(PKME_Tabs);

            // Initialize other components
            PokemonEventHandler.RestoreCommands(this);
            dragout.AllowDrop = true;
            dragout.GiveFeedback += (sender, e) => { e.UseDefaultCursors = false; };
            GiveFeedback += (sender, e) => { e.UseDefaultCursors = false; };

            // Disable all controls
            DisableControls();
        }

        private void InitializePKMEditor()
        {
            SAV = new SAV7(Resources.SavUltraMoon);
            PKME_Tabs.LegalityChanged += new EventHandler(PKME_Tabs_LegalityChanged);
            PKME_Tabs.UpdatePreviewSprite += new EventHandler(PKME_Tabs_UpdatePreviewSprite);
            PKME_Tabs.SaveFileRequested += new PKMEditor.ReturnSAVEventHandler(PKME_Tabs_SaveFileRequested);
            GameInfo.CurrentLanguage = "en";
            GameInfo.Strings = GameInfo.GetStrings("en");
            PKM pk = SAV.GetPKM(PKME_Tabs.CurrentPKM.Data);
            PKME_Tabs.EnableDragDrop(EnterTabDrag, DropTabDrag);
            Task.Run(() =>
            {
                Util.SetLocalization(typeof(LegalityCheckStrings), "en");
                RibbonStrings.ResetDictionary(GameInfo.Strings.ribbons);
            });
            // Update Legality Analysis strings
            LegalityAnalysis.MoveStrings = GameInfo.Strings.movelist;
            LegalityAnalysis.SpeciesStrings = GameInfo.Strings.specieslist;
            PKME_Tabs.ChangeLanguage(SAV, pk);
            Pokemon = SAV.BlankPKM;
            PKME_Tabs.InitializeFields();
            PKME_Tabs.TemplateFields(null);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lb_pkmnntrver.Text = $"{System.Windows.Forms.Application.ProductVersion} ({System.Reflection.Assembly.GetExecutingAssembly().GetName().Version})";
            lb_pkhexcorever.Text = "17.12.05";

            CheckForUpdate();
            host.Text = Settings.Default.IP;
            LoadDebugIP();

            // Prompt warining for command line modes
            if (HaX)
            {
                MessageBox.Show("Illegal mode enabled, please be careful.", "PKMN-NTR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                chkHaXMessages.Visible = true;
                this.Text += " (HaX Mode)";
            }
            if (EnablePartyWrite)
            {
                MessageBox.Show("Party editing enabled, please be careful.", "PKMN-NTR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Text += " (Party Write Mode)";
            }

            host.Focus();
        }

        [Conditional("DEBUG")]
        private void LoadDebugIP()
        {
            StreamReader sr = new StreamReader(@System.Windows.Forms.Application.StartupPath + "\\IP.txt");
            host.Text = sr.ReadLine();
            sr.Close();
        }

        [Conditional("DEBUG")]
        private void SaveDebugIP()
        {
            File.WriteAllText(@System.Windows.Forms.Application.StartupPath + "\\IP.txt", host.Text);
        }

        private async void CheckForUpdate()
        {
            try
            {
                AddToLog("GUI: Look for updates");
                // Get current             
                AddToLog("GUI: Current version: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);

                // Get latest stable
                Github = new GitHubClient(new ProductHeaderValue("PKMN-NTR-UpdateCheck"));
                Release lateststable = await Github.Repository.Release.GetLatest("drgoku282", "PKMN-NTR");
                int[] verlatest = Array.ConvertAll(lateststable.TagName.Split('.'), int.Parse);
                AddToLog("GUI: Last stable: " + lateststable.TagName);

                // Look for latest stable
                if (CheckVersions(verlatest))
                {
                    AddToLog("GUI: Update found!");
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
                        AddToLog("GUI: Last preview: " + latestbeta.TagName);
                        int[] verbeta = Array.ConvertAll(latestbeta.TagName.Split('.'), int.Parse);
                        if (CheckVersions(verbeta))
                        {
                            AddToLog("GUI: New preview version found");
                            Delg.SetText(lb_update, "Preview version " + latestbeta.TagName + " is available.");
                            updateURL = latestbeta.HtmlUrl;
                        }
                        else
                        {
                            AddToLog("GUI: PKMN-NTR is up to date");
                            Delg.SetText(lb_update, "PKMN-NTR is up to date.");
                            updateURL = null;
                        }
                    }
                    else
                    {
                        AddToLog("GUI: PKMN-NTR is up to date");
                        Delg.SetText(lb_update, "PKMN-NTR is up to date.");
                        updateURL = null;
                    }
                }
            }
            catch (Exception ex)
            {
                updateURL = null;
                AddToLog("GUI: An error has ocurred while checking for updates:");
                AddToLog(ex.Message);
                Delg.SetText(lb_update, "Update not found.");
            }
        }

        private void ClickUpdateLabel(object sender, EventArgs e)
        {
            if (updateURL != null)
            {
                Process.Start(updateURL);
            }
        }

        private bool CheckVersions(int[] tag)
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

        private void EnableControls()
        {
            foreach (TabPage tab in Tabs_General.TabPages)
            {
                Delg.SetEnabled(tab, true);
            }
            if (!(IsUSUM))
            {
                Delg.SetEnabled(Tools_Breeding, true);
                Delg.SetEnabled(Tools_SoftReset, true);
                Delg.SetEnabled(Tools_WonderTrade, true);
            }
            Delg.SetEnabled(Tool_Trainer, true);
            Delg.SetEnabled(Tool_Items, true);
            Delg.SetEnabled(Tool_Controls, true);
            Delg.SetEnabled(Tools_PokeDigger, true);
            Delg.SetEnabled(resetNoBox, true);
            Delg.SetEnabled(Btn_ReloadFields, true);
        }

        private void DisableControls()
        {
            Delg.SetEnabled(Tab_Dump, false);
            Delg.SetEnabled(Tab_Clone, false);
            Delg.SetEnabled(Tool_Trainer, false);
            Delg.SetEnabled(Tool_Items, false);
            Delg.SetEnabled(Tool_Controls, false);
            Delg.SetEnabled(Tools_Breeding, false);
            Delg.SetEnabled(Tools_SoftReset, false);
            Delg.SetEnabled(Tools_WonderTrade, false);
            Delg.SetEnabled(Tools_PokeDigger, false);
            Delg.SetEnabled(resetNoBox, false);
            Delg.SetEnabled(Btn_ReloadFields, false);
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

        private void SendHeartbeat(object sender, EventArgs e)
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

        public void StartAutoDisconnect()
        {
            disconnectTimer.Enabled = true;
        }

        private void AutoDisconnect(object sender, EventArgs e)
        {
            disconnectTimer.Enabled = false;
            Program.ntrClient.disconnect();
        }

        static void HandleDataReady(object sender, DataReadyEventArgs e)
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

        public void AddToLog(string msg)
        {
            Program.gCmdWindow.BeginInvoke(Program.gCmdWindow.delAddLog, msg);
        }

        public void AddWaitingForData(uint newkey, DataReadyWaiting newvalue)
        {
            if (waitingForData.ContainsKey(newkey))
            {
                return;
            }

            waitingForData.Add(newkey, newvalue);
        }

        private void StartConnecting(object sender, EventArgs e)
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

        private void StartDisconnecting(object sender, EventArgs e)
        {
            PerformDisconnect();

            if (EventPollingWorker.IsBusy)
            {
                EventPollingWorker.CancelAsync();
            }
        }

        public void PerformDisconnect()
        {
            Program.scriptHelper.disconnect();
            buttonConnect.Text = "Connect";
            buttonConnect.Enabled = true;
            buttonDisconnect.Enabled = false;
            IsConnected = false;
            DisableControls();
        }

        private void CheckConnection(object sender, EventArgs e)
        {
            Program.scriptHelper.listprocess();
            buttonConnect.Text = "Connected";
            buttonConnect.Enabled = false;
            buttonDisconnect.Enabled = true;
            IsConnected = true;
            Settings.Default.IP = host.Text;
            Settings.Default.Save();
            SaveDebugIP();
        }

        //This functions handles additional information events from NTR netcode. We are only interested in them if they are a process list, containing our game's PID and game type.
        delegate void getGameDelegate(object sender, EventArgs e);

        public void GetGame(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                BeginInvoke(new getGameDelegate(GetGame), sender, e);
                return;
            }

            InfoReadyEventArgs args = (InfoReadyEventArgs)e;
            if (args.info.Contains("kujira-1")) // X
            {
                string log = args.info;
                pname = ", pname: kujira-1";
                string splitlog = log.Substring(log.IndexOf(pname) - 8, log.Length - log.IndexOf(pname));
                pid = Convert.ToInt32("0x" + splitlog.Substring(0, 8), 16);
                SAV = new SAV6(Resources.SavX);
            }
            else if (args.info.Contains("kujira-2")) // Y
            {
                string log = args.info;
                pname = ", pname: kujira-2";
                string splitlog = log.Substring(log.IndexOf(pname) - 8, log.Length - log.IndexOf(pname));
                pid = Convert.ToInt32("0x" + splitlog.Substring(0, 8), 16);
                SAV = new SAV6(Resources.SavY);
            }
            else if (args.info.Contains("sango-1")) // Omega Ruby
            {
                string log = args.info;
                pname = ", pname:  sango-1";
                string splitlog = log.Substring(log.IndexOf(pname) - 8, log.Length - log.IndexOf(pname));
                pid = Convert.ToInt32("0x" + splitlog.Substring(0, 8), 16);
                SAV = new SAV6(Resources.SavOmegaRuby);
            }
            else if (args.info.Contains("sango-2")) // Alpha Sapphire
            {
                string log = args.info;
                pname = ", pname:  sango-2";
                string splitlog = log.Substring(log.IndexOf(pname) - 8, log.Length - log.IndexOf(pname));
                pid = Convert.ToInt32("0x" + splitlog.Substring(0, 8), 16);
                SAV = new SAV6(Resources.SavAlphaSapphire);
            }
            else if (args.info.Contains("niji_loc") &&
                args.info.Contains("0004000000164800")) // Sun
            {
                string log = args.info;
                pname = ", pname: niji_loc";
                string splitlog = log.Substring(log.IndexOf(pname) - 8, log.Length - log.IndexOf(pname));
                pid = Convert.ToInt32("0x" + splitlog.Substring(0, 8), 16);
                SAV = new SAV7(Resources.SavSun);
            }
            else if (args.info.Contains("niji_loc") &&
                args.info.Contains("0004000000175e00")) // Moon
            {
                string log = args.info;
                pname = ", pname: niji_loc";
                string splitlog = log.Substring(log.IndexOf(pname) - 8, log.Length - log.IndexOf(pname));
                pid = Convert.ToInt32("0x" + splitlog.Substring(0, 8), 16);
                SAV = new SAV7(Resources.SavMoon);
            }
            else if (args.info.Contains("momiji") &&
                args.info.Contains("00040000001b5000")) // Ultra Sun
            {
                string log = args.info;
                pname = ", pname:   momiji";
                string splitlog = log.Substring(log.IndexOf(pname) - 8, log.Length - log.IndexOf(pname));
                pid = Convert.ToInt32("0x" + splitlog.Substring(0, 8), 16);
                SAV = new SAV7(Resources.SavUltraSun);
            }
            else if (args.info.Contains("momiji") &&
                args.info.Contains("00040000001b5100")) // Ultra Moon
            {
                string log = args.info;
                pname = ", pname:   momiji";
                string splitlog = log.Substring(log.IndexOf(pname) - 8, log.Length - log.IndexOf(pname));
                pid = Convert.ToInt32("0x" + splitlog.Substring(0, 8), 16);
                SAV = new SAV7(Resources.SavUltraMoon);
            }
            else // not a process list or game not found - ignore packet
            {
                return;
            }

            // Clear tabs to avoid writting wrong data
            if (!botWorking)
            {
                PKME_Tabs.CurrentPKM = SAV.BlankPKM;
                PKME_Tabs.SetPKMFormatMode(SAV.Generation);
                PKME_Tabs.PopulateFields(PKME_Tabs.CurrentPKM);
                PKME_Tabs.ToggleInterface(SAV, SAV.BlankPKM);
                PKME_Tabs.CurrentPKM = SAV.BlankPKM;
                PKME_Tabs.TemplateFields(null);
                if (SAV.Generation == 7)
                {
                    PKXEXT = ".pk7";
                    BOXEXT = ".ek7";
                    LoadGen7GameData();
                    DumpGen7Data();
                }
                else if (SAV.Generation == 6)
                {
                    PKXEXT = ".pk6";
                    BOXEXT = ".ek6";
                    LoadGen6GameData();
                    DumpGen6Data();
                }
                EnableControls();
            }

            // Fill fields in the form according to gen
            Program.helper.pid = pid;
        }

        private void LoadGen6GameData()
        {
            Delg.SetEnabled(radioBattleBox, true);
            Delg.SetEnabled(Write_PKM, true);
            Delg.SetCheckedRadio(radioBoxes, true);
            Delg.SetText(radioDaycare, "Daycare");
            Delg.SetMaximum(boxDump, SAV.BoxCount);
            Delg.SetMaximum(Num_CDBox, SAV.BoxCount);
            Delg.SetMaximum(Num_CDAmount, LookupTable.GetRemainingSpaces((int)Num_CDBox.Value, (int)Num_CDSlot.Value));
        }

        private async void LoadGen7GameData()
        {
            Delg.SetEnabled(radioBattleBox, false);
            Delg.SetEnabled(Write_PKM, true);
            Delg.SetCheckedRadio(radioBoxes, true);
            Delg.SetText(radioDaycare, "Nursery");
            Delg.SetMaximum(boxDump, SAV.BoxCount);
            Delg.SetMaximum(Num_CDBox, SAV.BoxCount);
            Delg.SetMaximum(Num_CDAmount, LookupTable.GetRemainingSpaces((int)Num_CDBox.Value, (int)Num_CDSlot.Value));

            //Apply connection patch
            Task<bool> Patch = Program.helper.waitNTRwrite(LookupTable.NFCOffset, LookupTable.NFCValue, pid);
            if (!(await Patch))
            {
                MessageBox.Show("An error has ocurred while applying the connection patch.", "PKMN-NTR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion Main Window

        #region R/W trainer data

        // Dump data according to generation
        public void DumpGen6Data()
        {
            DumpTrainerCard();
        }

        public void DumpGen7Data()
        {
            DumpTrainerCard();
            DumpEggSeed();
            DumpLegendarySeed();
        }

        // Reload fields
        private void Btn_ReloadFields_Click(object sender, EventArgs e)
        {
            if (SAV.Generation == 6)
            {
                DumpGen6Data();
            }
            else if (SAV.Generation == 7)
            {
                DumpGen7Data();
            }
        }

        // Game save data handling
        public void DumpTrainerCard()
        {
            DataReadyWaiting myArgs = new DataReadyWaiting(new byte[LookupTable.TrainerCardSize], HandleTrainerCard, null);
            AddWaitingForData(Program.scriptHelper.data(LookupTable.TrainerCardOffset, LookupTable.TrainerCardSize, pid), myArgs);
        }

        public void HandleTrainerCard(object args_obj)
        {
            DataReadyWaiting args = (DataReadyWaiting)args_obj;
            SAV.SetData(args.data, (int)LookupTable.TrainerCardLocation);
            Delg.SetText(lb_name, SAV.OT);
            Delg.SetText(lb_tid, SAV.TID.ToString("D5"));
            Delg.SetText(lb_sid, SAV.SID.ToString("D5"));
            Delg.SetText(lb_tsv, LookupTable.GetTSV(SAV.TID, SAV.SID).ToString("D4"));
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
                    Delg.SetText(lb_g7id, SAV.TrainerID7.ToString("D6"));
                    break;
                case GameVersion.MN:
                    Delg.SetText(lb_version, "Moon");
                    Delg.SetText(lb_g7id, SAV.TrainerID7.ToString("D6"));
                    break;
                case GameVersion.US:
                    Delg.SetText(lb_version, "Ultra Sun");
                    Delg.SetText(lb_g7id, SAV.TrainerID7.ToString("D6"));
                    break;
                case GameVersion.UM:
                    Delg.SetText(lb_version, "Ultra Moon");
                    Delg.SetText(lb_g7id, SAV.TrainerID7.ToString("D6"));
                    break;
            }
        }

        // Egg Seed handling
        public void DumpEggSeed()
        {
            DataReadyWaiting myArgs = new DataReadyWaiting(new byte[0x10], HandleEggSeed, null);
            AddWaitingForData(Program.scriptHelper.data(LookupTable.SeedEggOffset, 0x10, pid), myArgs);
        }

        public void HandleEggSeed(object args_obj)
        {
            DataReadyWaiting args = (DataReadyWaiting)args_obj;
            Delg.SetText(Seed_Egg, BitConverter.ToString(args.data.Reverse().ToArray()).Replace("-", ""));
        }

        // RNG Seed
        public void DumpLegendarySeed()
        {
            DataReadyWaiting myArgs = new DataReadyWaiting(new byte[0x04], HandleLegendarySeed, null);
            AddWaitingForData(Program.scriptHelper.data(LookupTable.SeedLegendaryOffset, 0x04, pid), myArgs);
        }

        public void HandleLegendarySeed(object args_obj)
        {
            DataReadyWaiting args = (DataReadyWaiting)args_obj;
            Delg.SetText(Seed_Legendary, BitConverter.ToUInt32(args.data, 0).ToString("X8"));
        }

        #endregion R/W trainer data

        #region R/W pokémon data

        // Dump single pokémon
        private void DumpPokemon(object sender, EventArgs e)
        {
            // Obtain offset
            uint dumpOff = 0;
            if (radioBoxes.Checked)
            {
                uint ssd = ((decimal.ToUInt32(boxDump.Value) - 1) * BOXSIZE) + decimal.ToUInt32(slotDump.Value) - 1;
                dumpOff = LookupTable.BoxOffset + (ssd * POKEBYTES);
            }
            else if (radioDaycare.Checked)
            {
                switch ((int)slotDump.Value)
                {
                    case 1: dumpOff = LookupTable.DayCare1Offset; break;
                    case 2: dumpOff = LookupTable.DayCare2Offset; break;
                    case 3: dumpOff = LookupTable.DayCare3Offset; break;
                    case 4: dumpOff = LookupTable.DayCare4Offset; break;
                    default: dumpOff = LookupTable.DayCare1Offset; break;
                }
            }
            else if (radioBattleBox.Checked)
            {
                dumpOff = LookupTable.BattleBoxOffset + ((decimal.ToUInt32(slotDump.Value) - 1) * POKEBYTES);
            }
            else if (radioTrade.Checked)
            {
                if (SAV.Generation == 6)
                {
                    DataReadyWaiting myArgs = new DataReadyWaiting(new byte[0x1FFFF], HandleTradeData, null);
                    AddWaitingForData(Program.scriptHelper.data(LookupTable.TradeOffset, 0x1FFFF, pid), myArgs);
                }
                else
                {
                    DataReadyWaiting myArgs = new DataReadyWaiting(new byte[POKEBYTES], HandlePokemon, null);
                    uint mySeq = Program.scriptHelper.data(LookupTable.TradeOffset, POKEBYTES, pid);
                    AddWaitingForData(mySeq, myArgs);
                }
            }
            else if (radioOpponent.Checked)
            {
                if (SAV.Generation == 6)
                {
                    DataReadyWaiting myArgs = new DataReadyWaiting(new byte[0x1FFFF], HandleOpponentData, null);
                    AddWaitingForData(Program.scriptHelper.data(LookupTable.WildOffset1, 0x1FFFF, pid), myArgs);
                }
                else
                {
                    DataReadyWaiting myArgs = new DataReadyWaiting(new byte[POKEBYTES], HandlePokemon, null);
                    uint offset = 0;
                    switch ((int)boxDump.Value)
                    {
                        case 1: offset = LookupTable.WildOffset1; break;
                        //Opponent 2 (in dual battle)
                        case 2: offset = LookupTable.WildOffset2; break;
                        //Last called helper in SOS battle.
                        case 3: offset = LookupTable.WildOffset3; break;
                        //Last 4 Pokemon in SOS battle
                        case 4:
                            offset = LookupTable.WildOffset4 + (uint)(slotDump.Value - 1) * 0x1E4;
                            break;
                    }
                    uint mySeq = Program.scriptHelper.data(offset, POKEBYTES, pid);
                    AddWaitingForData(mySeq, myArgs);
                }
            }
            else if (radioParty.Checked)
            {
                dumpOff = LookupTable.PartyOffset + (decimal.ToUInt32(slotDump.Value) - 1) * 484;
            }

            // Read at offset
            if (radioParty.Checked)
            {
                DataReadyWaiting myArgs = new DataReadyWaiting(new byte[2602], HandlePokemon, null);
                uint mySeq = Program.scriptHelper.data(dumpOff, 260, pid);
                AddWaitingForData(mySeq, myArgs);
            }
            else if (radioBoxes.Checked || radioDaycare.Checked || radioBattleBox.Checked)
            {
                DataReadyWaiting myArgs = new DataReadyWaiting(new byte[POKEBYTES], HandlePokemon, null);
                uint mySeq = Program.scriptHelper.data(dumpOff, POKEBYTES, pid);
                AddWaitingForData(mySeq, myArgs);
            }
        }

        delegate void handlePkmDataDelegate(object args_obj);

        public void HandlePokemon(object args_obj)
        {
            if (this.InvokeRequired)
            {
                BeginInvoke(new handlePkmDataDelegate(HandlePokemon), args_obj);
                return;
            }
            try
            {
                DataReadyWaiting args = (DataReadyWaiting)args_obj;
                PKM validator = SAV.BlankPKM;
                validator.Data = PKX.DecryptArray(args.data);
                bool dataCorrect = validator.ChecksumValid && validator.Species > 0 && validator.Species < SAV.MaxSpeciesID;

                if (dataCorrect)
                { // Valid pkx file
                    Pokemon = validator.Clone();
                    if (backupPKM.Checked)
                    {
                        SavePKMToFile();
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

        public void SavePKMToFile()
        {
            try
            {
                // Create Temp File to Drag
                PKM pkx = PreparePKM();
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

        public void HandleTradeData(object args_obj)
        {
            DataReadyWaiting args = (DataReadyWaiting)args_obj;

            byte[] relativePattern = null;
            uint offsetAfter = 0;

            if (IsXY)
            {
                relativePattern = new byte[] { 0x08, 0x1C, 0x01, 0x00, 0x00, 0x20, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xD8, 0xBE, 0x59 };
                offsetAfter += 98;
            }

            else if (IsORAS)
            {
                relativePattern = new byte[] { 0x08, 0x1E, 0x01, 0x00, 0x00, 0x20, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x9C, 0xE8, 0x5D };
                offsetAfter += 98;
            }

            List<uint> occurences = FindInRAMDump(args.data, relativePattern);
            int count = 0;
            foreach (uint occurence in occurences)
            {
                count++;
                if (count != 2)
                {
                    continue;
                }
                int dataOffset = (int)(occurence + offsetAfter);
                DataReadyWaiting args_pkm = new DataReadyWaiting(args.data.Skip(dataOffset).Take(POKEBYTES).ToArray(), HandlePokemon, null);
                HandlePokemon(args_pkm);
            }
        }

        public void HandleOpponentData(object args_obj)
        {
            DataReadyWaiting args = (DataReadyWaiting)args_obj;

            List<uint> occurences = FindInRAMDump(args.data, LookupTable.OpponentPatern);
            int count = 0;
            foreach (uint occurence in occurences)
            {
                count++;
                int dataOffset = (int)(occurence + LookupTable.OpponentOffset);
                DataReadyWaiting args_pkm = new DataReadyWaiting(args.data.Skip(dataOffset).Take(POKEBYTES).ToArray(), HandlePokemon, null);
                HandlePokemon(args_pkm);
            }
        }

        public void WaitForOpponentData(object args_obj)
        {
            DataReadyWaiting args = (DataReadyWaiting)args_obj;

            List<uint> occurences = FindInRAMDump(args.data, LookupTable.OpponentPatern);
            foreach (uint occurence in occurences)
            {
                int dataOffset = (int)(occurence + LookupTable.OpponentOffset);
                oppdata = args.data.Skip(dataOffset).Take(POKEBYTES).ToArray();
            }
        }

        static List<uint> FindInRAMDump(byte[] haystack, byte[] needle)
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
        private void DumpBoxes(object sender, EventArgs e)
        {
            DataReadyWaiting myArgs = new DataReadyWaiting(new byte[SAV.BoxCount * BOXSIZE * POKEBYTES], HandleBoxesData, null);
            AddWaitingForData(Program.scriptHelper.data(LookupTable.BoxOffset, (uint)SAV.BoxCount * BOXSIZE * POKEBYTES, pid), myArgs);
        }

        public void HandleBoxesData(object args_obj)
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
        private void InjectPokemon(object sender, EventArgs e)
        {
            Pokemon = PreparePKM();

            if (!IsLegal)
            {
                if (HaX)
                {
                    if (!chkHaXMessages.Checked)
                    {
                        DialogResult dr = MessageBox.Show("This pokémon is illegal. Do " +
                            "you still want to inject it to the game?", "Illegal pokémon",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.No)
                        {
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("This pokémon is illegal, it won't be written to " +
                        "the file", "Illegal pokémon", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
            }

            if (radioBoxes.Checked)
            {
                uint index = ((uint)boxDump.Value - 1) * BOXSIZE + (uint)slotDump.Value - 1;
                uint offset = LookupTable.BoxOffset + (index * POKEBYTES);
                Program.scriptHelper.write(offset, Pokemon.EncryptedBoxData, pid);
            }
            else if (radioParty.Checked && EnablePartyWrite)
            {
                uint offset =LookupTable.PartyOffset + ((uint)slotDump.Value - 1) * 484;
                Program.scriptHelper.write(offset, Pokemon.EncryptedPartyData, pid);
            }
            else
            {
                MessageBox.Show("No support for this source, if you want to edit this pokémon, deposit it in the PC.", "No editing support", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Clone and delete
        private void StartCloneDelete(object sender, EventArgs e)
        {
            Pokemon = PreparePKM();
            byte[] pkmsource = null;

            if (CloneMode.Checked)
            {
                if (!IsLegal)
                {
                    if (HaX)
                    {
                        if (!chkHaXMessages.Checked)
                        {
                            DialogResult dr = MessageBox.Show("This pokémon is illegal. Do " +
                                "you still want to inject it to the game?", "Illegal pokémon",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dr == DialogResult.No)
                            {
                                return;
                            }
                        }
                        pkmsource = Pokemon.EncryptedBoxData;
                    }
                    else
                    {
                        MessageBox.Show("This pokémon is illegal, it won't be written " +
                            "to the file", "Illegal pokémon", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    pkmsource = Pokemon.EncryptedBoxData;
                }
            }
            else if (DeleteMode.Checked)
            {
                pkmsource = SAV.BlankPKM.EncryptedBoxData;
            }
            else
            {
                return;
            }

            uint index = ((uint)Num_CDBox.Value - 1) * BOXSIZE + (uint)Num_CDSlot.Value - 1;
            uint offset = LookupTable.BoxOffset + (index * POKEBYTES);
            uint size = (uint)Num_CDAmount.Value * POKEBYTES;

            if (CB_CDBackup.Checked)
            {
                DataReadyWaiting myArgs = new DataReadyWaiting(new byte[size], HandleBoxesData, null);
                AddWaitingForData(Program.scriptHelper.data(offset, size, pid), myArgs);
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

        // PKHeX Tabs
        private void PKME_Tabs_LegalityChanged(object sender, EventArgs e)
        {
            if (sender == null)
            {
                PB_Legal.Visible = false;
                return;
            }

            PB_Legal.Visible = true;
            PB_Legal.Image = sender as bool? == false ? Resources.warn : Resources.valid;
            IsLegal = sender as bool? == false ? false : true;
        }

        private void PKME_Tabs_UpdatePreviewSprite(object sender, EventArgs e) => GetPreview(dragout);

        private SaveFile PKME_Tabs_SaveFileRequested(object sender, EventArgs e) => SAV;

        private void GetPreview(PictureBox pb, PKM pk = null)
        {
            pk = pk ?? PreparePKM(false); // don't perform control loss click

            pb.Image = PKMUtil.GetSprite(pk.Species, pk.AltForm, pk.Gender, pk.HeldItem, pk.IsEgg, pk.IsShiny);
            if (pb.BackColor == Color.Red)
                pb.BackColor = Color.Transparent;
        }

        public PKM PreparePKM(bool click = true) => PKME_Tabs.PreparePKM(click);

        private void EnterTabDrag(object sender, DragEventArgs e)
        {
            if (e.AllowedEffect == (DragDropEffects.Copy | DragDropEffects.Link)) // external file
                e.Effect = DragDropEffects.Copy;
            else if (e.Data != null) // within
                e.Effect = DragDropEffects.Move;
        }

        private void DropTabDrag(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files == null || files.Length == 0)
                return;
            OpenQuick(files[0]);
            e.Effect = DragDropEffects.Copy;

            Cursor = DefaultCursor;
        }

        private void OpenQuick(string path)
        {
            string ext = Path.GetExtension(path);
            byte[] input;
            try
            {
                input = File.ReadAllBytes(path);
            }
            catch (Exception e)
            {
                WinFormsUtil.Error("Unable to load file.  It could be in use by another program.\nPath: " + path, e);
                return;
            }

            var pk = PKMConverter.GetPKMfromBytes(input, prefer: ext.Length > 0 ? (ext.Last() - '0') & 0xF : SAV.Generation);
            if (pk == null)
            {
                WinFormsUtil.Error("Unable to load file.  This file is not compatible with this program.\nPath: " + path);
            }

            PKME_Tabs.PopulateFields(pk);
        }

        private void DragoutDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            OpenQuick(files[0]);
            e.Effect = DragDropEffects.Copy;

            Cursor = DefaultCursor;
        }

        private void Dragout_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void Dragout_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;
            if (!PKME_Tabs.VerifiedPKM())
                return;

            // Create Temp File to Drag
            PKM pkx = PreparePKM();
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
                //C_SAV.M.DragInfo.Source.PKM = pkx;
                Cursor = new Cursor(((Bitmap)pb.Image).GetHicon());
                DoDragDrop(new DataObject(DataFormats.FileDrop, new[] { newfile }), DragDropEffects.Move);
            }
            catch (Exception x)
            { WinFormsUtil.Error("Drag & Drop Error", x); }
            Cursor = Cursors.Default;
            File.Delete(newfile);
        }

        private void DragoutLeave(object sender, EventArgs e)
        {
            dragout.BackgroundImage = Resources.slotTrans;
            if (Cursor == Cursors.Hand)
                Cursor = Cursors.Default;
        }

        private void DragoutEnter(object sender, EventArgs e)
        {
            dragout.BackgroundImage = WinFormsUtil.getIndex(PKME_Tabs.CB_Species) > 0 ? Resources.slotSet : Resources.slotDel;
            Cursor = Cursors.Hand;
        }

        private void ClickLegality(object sender, EventArgs e)
        {
            if (!PKME_Tabs.VerifiedPKM())
            { SystemSounds.Asterisk.Play(); return; }

            var pk = PreparePKM();

            if (pk.Species == 0 || !pk.ChecksumValid)
            { SystemSounds.Asterisk.Play(); return; }

            ShowLegality(sender, e, pk);
        }

        private void ShowLegality(object sender, EventArgs e, PKM pk)
        {
            LegalityAnalysis la = new LegalityAnalysis(pk);
            if (pk.Slot < 0)
                PKME_Tabs.UpdateLegality(la);
            bool verbose = ModifierKeys == Keys.Control;
            var report = la.Report(verbose);
            if (verbose)
            {
                var dr = WinFormsUtil.Prompt(MessageBoxButtons.YesNo, report, "Copy report to Clipboard?");
                if (dr == DialogResult.Yes)
                    Clipboard.SetText(report);
            }
            else
                WinFormsUtil.Alert(report);
        }

        // Radio boxes for pokémon source
        private void ActiveBoxes(object sender, EventArgs e)
        {
            if (radioBoxes.Tag == null)
            {
                radioBoxes.Tag = new LastBoxSlot { Box = 1, Slot = 1 };
            }

            if (radioBoxes.Checked)
            {
                boxDump.Minimum = 1;
                boxDump.Maximum = SAV.BoxCount;
                slotDump.Minimum = 1;
                slotDump.Maximum = BOXSIZE;
                boxDump.Enabled = true;
                slotDump.Enabled = true;
                backupPKM.Enabled = true;
                Write_PKM.Enabled = true;
                boxDump.Value = ((LastBoxSlot)radioBoxes.Tag).Box;
                slotDump.Value = ((LastBoxSlot)radioBoxes.Tag).Slot;
            }
            else
            {
                radioBoxes.Tag = new LastBoxSlot { Box = boxDump.Value, Slot = slotDump.Value };
            }

        }

        private void ActiveDaycare(object sender, EventArgs e)
        {
            if (radioDaycare.Tag == null)
            {
                radioDaycare.Tag = new LastBoxSlot { Box = 1, Slot = 1 };
            }
            if (radioDaycare.Checked)
            {
                boxDump.Minimum = 1;
                boxDump.Maximum = 2;
                slotDump.Minimum = 1;
                if (IsORAS) // Handle ORAS Battle Resort Daycare
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
                boxDump.Value = ((LastBoxSlot)radioDaycare.Tag).Box;
                slotDump.Value = ((LastBoxSlot)radioDaycare.Tag).Slot;
            }
            else
            {
                radioDaycare.Tag = new LastBoxSlot { Box = boxDump.Value, Slot = slotDump.Value };
            }
        }

        private void ActiveBattleBox(object sender, EventArgs e)
        {
            if (radioBattleBox.Tag == null)
            {
                radioBattleBox.Tag = new LastBoxSlot { Box = 1, Slot = 1 };
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
                boxDump.Value = ((LastBoxSlot)radioBattleBox.Tag).Box;
                slotDump.Value = ((LastBoxSlot)radioBattleBox.Tag).Slot;
            }
            else
            {
                radioBattleBox.Tag = new LastBoxSlot { Box = boxDump.Value, Slot = slotDump.Value };
            }
        }

        private void ActiveTrade(object sender, EventArgs e)
        {
            if (radioTrade.Tag == null)
            {
                radioTrade.Tag = new LastBoxSlot { Box = 1, Slot = 1 };
            }
            if (radioTrade.Checked)
            {
                boxDump.Minimum = 1;
                boxDump.Maximum = 1;
                slotDump.Minimum = 1;
                slotDump.Maximum = 1;
                boxDump.Enabled = false;
                slotDump.Enabled = false;
                backupPKM.Enabled = false;
                Write_PKM.Enabled = false;
                boxDump.Value = ((LastBoxSlot)radioTrade.Tag).Box;
                slotDump.Value = ((LastBoxSlot)radioTrade.Tag).Slot;
            }
            else
            {
                radioTrade.Tag = new LastBoxSlot { Box = boxDump.Value, Slot = slotDump.Value };
            }
        }

        private void ActiveOpponent(object sender, EventArgs e)
        {
            if (radioOpponent.Tag == null)
            {
                radioOpponent.Tag = new LastBoxSlot { Box = 1, Slot = 1 };
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
                Write_PKM.Enabled = false;
                BoxLabel.Text = "Opp.:";
                boxDump.Value = ((LastBoxSlot)radioOpponent.Tag).Box;
                slotDump.Value = ((LastBoxSlot)radioOpponent.Tag).Slot;
            }
            else
            {
                radioOpponent.Tag = new LastBoxSlot { Box = boxDump.Value, Slot = slotDump.Value };
                BoxLabel.Text = "Box:";
                if (SAV.Generation == 7)
                {
                    DumpInstructionsBtn.Visible = false;
                }
            }
        }

        private void ActiveParty(object sender, EventArgs e)
        {
            if (radioParty.Tag == null)
            {
                radioParty.Tag = new LastBoxSlot { Box = 1, Slot = 1 };
            }
            if (radioParty.Checked)
            {
                boxDump.Minimum = 1;
                boxDump.Maximum = 1;
                slotDump.Minimum = 1;
                slotDump.Maximum = 6;
                boxDump.Enabled = false;
                slotDump.Enabled = true;
                backupPKM.Enabled = true;
                Write_PKM.Enabled = EnablePartyWrite;
                boxDump.Value = ((LastBoxSlot)radioParty.Tag).Box;
                slotDump.Value = ((LastBoxSlot)radioParty.Tag).Slot;
            }
            else
            {
                radioParty.Tag = new LastBoxSlot { Box = boxDump.Value, Slot = slotDump.Value };
            }
        }

        // Clone/Delete tab
        private void UpdateMaxCloneDelete(object sender, EventArgs e)
        {
            Delg.SetMaximum(Num_CDAmount, LookupTable.GetRemainingSpaces((int)Num_CDBox.Value, (int)Num_CDSlot.Value));
        }

        // Bot functions
        public void HandleRAMread(uint value)
        {
            AddToLog("NTR: Read sucessful - 0x" + value.ToString("X8"));
            Delg.SetText(readResult, "0x" + value.ToString("X8"));
        }

        public void UpdateDumpBoxes(int box, int slot)
        {
            Delg.SetValue(boxDump, box + 1);
            Delg.SetValue(slotDump, slot + 1);
        }

        public void UpdateDumpBoxes(NumericUpDown box, NumericUpDown slot)
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

        #region Sub-forms

        // Tool start/finish
        private void Tool_Start()
        {
            txtLog.Clear();
            DisableControls();
            Delg.SetEnabled(Tool_Script, false);
        }

        public void Tool_Finish()
        {
            if (IsConnected)
            {
                EnableControls();
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
            iteminfo = await DumpItems();
            if (iteminfo == null)
            {
                MessageBox.Show("A error ocurred while dumping items", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                SAV.SetData(iteminfo, (int)LookupTable.ItemsLocation);
                new Edit_Items().ShowDialog();
            }
        }

        public async Task<byte[]> DumpItems()
        {
            Task<bool> worker = Program.helper.waitNTRmultiread(LookupTable.ItemsOffset, LookupTable.ItemsSize);
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
            new PokeDigger(pid, IsConnected).ShowDialog();
        }

        // Script Builder
        private void Tool_Script_Click(object sender, EventArgs e)
        {
            Tool_Start();
            new ScriptBuilder().Show();
        }

        // Event Handler
        private void Tool_EventHandler_Click(object sender, EventArgs e)
        {
            Tool_Start();
            new PokemonEventHandler(this).Show();
        }

        private void DumpInstructionsBtn_Click(object sender, EventArgs e)
        {
            if (radioOpponent.Checked)
            {
                new DumpOpponentHelp().Show();
            }
        }

        private void PollingButton_Click(object sender, EventArgs e)
        {
            if (!polling)
            {
                PollingButton.Enabled = false;

                EventPollingWorker.RunWorkerAsync();
                PollingButton.Text = "Stop Polling";
                polling = true;

                PollingButton.Enabled = true;
            }
            else
            {
                PollingButton.Enabled = false;

                EventPollingWorker.CancelAsync();
                pollingCancelledEvent.WaitOne();
                PollingButton.Text = "Start Polling";
                polling = false;

                PollingButton.Enabled = true;
            }
        }

        delegate void handlePollingPkmDataDelegate(object args_obj);

        private void HandlePollingPkmData(object args_obj)
        {
            if (this.InvokeRequired)
            {
                BeginInvoke(new handlePollingPkmDataDelegate(HandlePollingPkmData), args_obj);
                return;
            }
            try
            {
                DataReadyWaiting args = (DataReadyWaiting)args_obj;
                List<PKM> party = (List<PKM>)args.arguments;
                PKM validator = SAV.BlankPKM;

                validator.Data = PKX.DecryptArray(args.data);
                bool dataCorrect = validator.ChecksumValid && validator.Species > 0 && validator.Species < SAV.MaxSpeciesID;

                if (dataCorrect)
                { // Valid pkx file
                    PKM new_pkm = validator.Clone();
                    party.Add(new_pkm);
                }
                else
                {
                    // Ignore invalid/empty data
                }
            }
            catch (Exception)
            {
            }
        }

        private void pollingLog(String msg)
        {
            try
            {
                Program.gCmdWindow.BeginInvoke(Program.gCmdWindow.delAddLog, msg);
            }
            catch (Exception)
            {
            }
        }

        private void RunCommand(string cmd, string pokemonName, int slotNum)
        {
            if (cmd.Length > 0)
            {
                cmd = cmd.Replace("###SLOT###", slotNum.ToString());
                cmd = cmd.Replace("###NAME###", pokemonName.ToLower());

                pollingLog("Running command: " + cmd);

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C \"" + cmd + "\"";
                process.StartInfo = startInfo;
                process.Start();
            }
        }

        private void EventPollingWorker_DoWork(object sender, CancelEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            uint[] sequenceNumbers = new uint[6];
            List<PKM> current_party = new List<PKM>();
            List<PKM> last_party = new List<PKM>();
            DataReadyWaiting args;

            pollingLog("Started polling loop");

            while (true)
            {
                // Read all 6 party slots
                for (uint i = 0; i < 6; i++)
                {
                    // Obtain offset
                    uint dumpOff = LookupTable.PartyOffset + i * 484;

                    // Read at offset
                    DataReadyWaiting myArgs = new DataReadyWaiting(new byte[2602], HandlePollingPkmData, current_party);
                    sequenceNumbers[i] = Program.scriptHelper.data(dumpOff, 260, pid);
                    AddWaitingForData(sequenceNumbers[i], myArgs);

                    // Don't query too frequently in quick succession
                    Thread.Sleep(100);
                }

                // TODO: Come up with a more thread-safe way to check for completion
                while (waitingForData.TryGetValue(sequenceNumbers[0], out args) &&
                       waitingForData.TryGetValue(sequenceNumbers[1], out args) &&
                       waitingForData.TryGetValue(sequenceNumbers[2], out args) &&
                       waitingForData.TryGetValue(sequenceNumbers[3], out args) &&
                       waitingForData.TryGetValue(sequenceNumbers[4], out args) &&
                       waitingForData.TryGetValue(sequenceNumbers[5], out args))
                {
                    Thread.Sleep(100);
                }

                // We have our party data!
                current_party = current_party.OrderBy(o => o.Slot).ToList();

                for (int j = 0; j < current_party.Count; j++)
                {
                    PKM oldPKM = null;
                    PKM newPKM = current_party[j];
                    bool runSlotChange = false;
                    string newPKM_Name = PKX.GetSpeciesName(newPKM.Species, 2).ToLower();

                    // TODO: Stat_HPCurrent values don't seem to be correct (at least on Omega Ruby)
                    //pollingLog(newPKM.Stat_HPCurrent + " HP in slot " + (j + 1) + " -> " + newPKM_Name);

                    if (last_party.Count > j)
                    {
                        oldPKM = last_party[j];
                    }

                    if (null != oldPKM)
                    {
                        if (oldPKM.Checksum != newPKM.Checksum)
                        {
                            pollingLog("Slot " + (j + 1) + " -> " + newPKM_Name);

                            // New Pokemon, do we put Pokemon event or HP zero event?
                            if (newPKM.Stat_HPCurrent == 0)
                            {
                                pollingLog("HP Zero in slot " + (j + 1) + " -> " + newPKM_Name);
                                RunCommand(hpZeroCommand, newPKM_Name, j + 1);
                            }
                            else
                            {
                                runSlotChange = true;
                            }
                        }
                        else if (oldPKM.Stat_HPCurrent != 0 && newPKM.Stat_HPCurrent == 0)
                        {
                            // Pokemon didn't change, but check for HP zero
                            pollingLog("HP Zero in slot " + (j + 1) + " -> " + newPKM_Name);
                            RunCommand(hpZeroCommand, newPKM_Name, j + 1);
                        }
                    }
                    else
                    {
                        pollingLog("Slot " + (j + 1) + " -> " + newPKM_Name);

                        if (newPKM.Stat_HPCurrent == 0)
                        {
                            pollingLog("HP Zero in slot " + (j + 1) + " -> " + newPKM_Name);
                            RunCommand(hpZeroCommand, newPKM_Name, j + 1);
                        }
                        else
                        {
                            runSlotChange = true;
                        }
                    }

                    if (runSlotChange)
                    {
                        // Execute the SlotChanged event command
                        RunCommand(slotChangeCommand, newPKM_Name, j + 1);
                    }
                }

                // Handle any disappearances
                if (current_party.Count() < last_party.Count())
                {
                    for (int j = current_party.Count(); j < last_party.Count(); j++)
                    {
                        pollingLog("Slot " + (j + 1) + " -> (empty)");

                        if (slotChangeCommand.Length > 0)
                        {
                            // Execute the SlotChanged event command
                            RunCommand(slotChangeCommand, "000", j + 1);
                        }
                    }
                }

                if (worker.CancellationPending == true)
                {
                    break;
                }

                last_party.Clear();
                current_party.ForEach((item) =>
                {
                    last_party.Add(item.Clone());
                });
                current_party.Clear();

                Thread.Sleep(5000);
            }

            pollingCancelledEvent.Set();
            pollingLog("Exited polling loop");
        }

        #endregion Sub-forms

        #region Bots

        public void SetBotMode(bool state)
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
                AddToLog("NTR: Read opponent pokémon data");
                oppdata = null;
                DataReadyWaiting myArgs = new DataReadyWaiting(new byte[0x1FFFF], WaitForOpponentData, null);
                AddWaitingForData(Program.scriptHelper.data(0x8800000, 0x1FFFF, pid), myArgs);
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
                    AddToLog("NTR: Read failed");
                    return null;
                }
                PKM validator = new PK6(PKX.DecryptArray(oppdata));
                if (validator.ChecksumValid && validator.Species > 0 && validator.Species <= Program.gCmdWindow.SAV.MaxSpeciesID)
                { // Valid pokemon
                    Program.helper.lastRead = validator.Checksum;
                    PKME_Tabs.PopulateFields(validator);
                    AddToLog("NTR: Read sucessful - PID 0x" + validator.PID.ToString("X8"));
                    return validator;
                }
                else if (validator.ChecksumValid && validator.Species == 0)
                { // Empty slot
                    AddToLog("NTR: Empty pokémon data");
                    return SAV.BlankPKM;
                }
                else
                { // Invalid pokémon
                    AddToLog("NTR: Invalid pokémon data");
                    return null;
                }
            }
            catch (Exception ex)
            {
                AddToLog("NTR: Read failed with exception:");
                AddToLog(ex.Message);
                return null; // No data received
            }
        }

        public async Task<bool> Reconnect()
        {
            AddToLog("NTR: Reconnect");
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
                AddToLog("NTR: Reconnect sucessful");
                return true;
            }
            else
            {
                AddToLog("NTR: Reconnect failed");
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
}
