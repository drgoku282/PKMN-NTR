using pkmn_ntr.Helpers;
using PKHeX.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static pkmn_ntr.Bot.Bot;

namespace pkmn_ntr.Bot
{
    /// <summary>
    /// Generation 6 Soft-reset bot.
    /// </summary>
    public partial class Bot_SoftReset6 : Form
    {
        /// <summary>
        /// Secuency of steps done by the bot.
        /// </summary>
        public enum BotState { botstart, pssmenush, fixwifi, touchpssset, testpssset, touchpssdis, testpssdis, touchpssconf, testpssout, returncontrol, touchsave, testsave, saveconf, saveout, typesr, trigger, miragespot, readopp, filter, testspassed, testshiny, testnature, testhp, testatk, testdef, testspa, testspd, testspe, testhdnpwr, testability, testgender, alltestsok, softreset, skipintro, skiptitle, startgame, reconnect, tev_start, tev_dialog, tev_cont1, tev_check, twk_start, soaring_start, soaring_cont, soaring_check, soaring_move, soaring_dialog, random_enc_check, flee1, flee2, flee3, botexit };

        // General bot variables
        private bool botworking;
        private bool userstop;
        private BotState botState;
        private ErrorMessage botresult;
        private int attempts;
        private int maxreconnect;
        private int[] finishmessage;
        private Task<bool> waitTaskbool;
        private Task<PKM> waitTaskPKM;

        // Class variables
        private bool walk;
        private bool ORAS;
        private int resetNo;
        private int steps;
        private int soaringX;
        private int soaringY;
        private int soaringTime;
        private int filternum;
        private PKM srpoke;

        // Class constants
        private const int commandtime = 250;
        private const int commanddelay = 250;

        // Data offsets
        private uint psssmenu1Off;
        private uint psssmenu1IN;
        //private uint psssmenu1OUT;
        public uint savescrnOff;
        public uint savescrnIN;
        public uint savescrnOUT;
        public uint pssettingsOff;
        public uint pssettingsIN;
        public uint pssettingsOUT;
        public uint pssdisableOff;
        public uint pssdisableY;
        public uint pssdisableIN;
        public uint pssdisableOUT;
        public uint dialogOff;
        public uint dialogIN;
        public uint dialogOUT;
        public uint soaringOff;
        public uint soaringIN;
        public uint soaringOUT;
        public uint soaringdialogOff;
        public uint soaringdialogIN;
        public uint soaringdialogOUT;

        public Bot_SoftReset6()
        {
            InitializeComponent();
        }

        private void Bot_SoftReset6_Load(object sender, System.EventArgs e)
        {
            Program.gCmdWindow.SetResetLabel("Number of resets:");
            if (Program.gCmdWindow.SAV.Version == GameVersion.X || Program.gCmdWindow.SAV.Version == GameVersion.Y)
            { // XY
                ORAS = false;
                psssmenu1Off = 0x19ABC0;
                psssmenu1IN = 0x7E0000;
                //psssmenu1OUT = 0x4D0000;
                savescrnOff = 0x19AB78;
                savescrnIN = 0x7E0000;
                savescrnOUT = 0x4D0000;
                pssettingsOff = 0x19ABF0;
                pssettingsIN = 0x7E0000;
                pssettingsOUT = 0x4D0000;
                pssdisableOff = 0x5EEEA4;
                pssdisableY = 100;
                pssdisableIN = 0x00000000;
                pssdisableOUT = 0x15000000;
                dialogOff = 0x5EA188;
                dialogIN = 0x0D;
                dialogOUT = 0x00;
            }
            else if (Program.gCmdWindow.SAV.Version == GameVersion.OR || Program.gCmdWindow.SAV.Version == GameVersion.AS)
            { // ORAS
                ORAS = true;
                psssmenu1Off = 0x19C21C;
                psssmenu1IN = 0x830000;
                //psssmenu1OUT = 0x500000;
                savescrnOff = 0x19C1CC;
                savescrnIN = 0x830000;
                savescrnOUT = 0x500000;
                pssettingsOff = 0x19C244;
                pssettingsIN = 0x830000;
                pssettingsOUT = 0x500000;
                pssdisableY = 120;
                pssdisableIN = 0x33000000;
                pssdisableOUT = 0x33100000;
                pssdisableOff = 0x630DA5;
                dialogOff = 0x62C2F4;
                dialogIN = 0x0D;
                dialogOUT = 0x0A;
                soaringOff = 0x62C2EC;
                soaringIN = 0x040DF1E0;
                soaringOUT = 0x00000000;
                soaringdialogOff = 0x62C2E4;
                soaringdialogIN = 0x200010;
                soaringdialogOUT = 0x200020;
            }
        }

        private void RunStop_Click(object sender, System.EventArgs e)
        {
            DisableControls();
            if (botworking)
            { // Stop bot
                Delg.SetEnabled(RunStop, false);
                Delg.SetText(RunStop, "Start Bot");
                botworking = false;
                userstop = true;
            }
            else
            {
                string typemessage;
                string resumemessage;
                switch (Mode.SelectedIndex)
                {
                    case 0:
                        typemessage = "Regular - Make sure you are in front of the pokémon.";
                        resumemessage = "In front of pokémon, will press A to trigger start the battle";
                        Program.gCmdWindow.SetRadioOpponent();
                        break;
                    case 1:
                        typemessage = "Mirage Spot - Make sure you are in front of the hole.";
                        resumemessage = "In front of hole, will press A to trigger dialog";
                        Program.gCmdWindow.SetRadioOpponent();
                        break;
                    case 2:
                        typemessage = "Event - Make sure you are in front of the lady in the Pokémon Center. Also, you must only have one pokémon in your party.";
                        resumemessage = "In front of the lady, will press A to trigger dialog";
                        Program.gCmdWindow.SetRadioParty();
                        break;
                    case 3:
                        typemessage = "Groudon/Kyogre - You must disable the PSS communications manually due PokéNav malfunction. Go in front of Groudon/Kyogre and save game before starting the battle.";
                        resumemessage = "In front of Groudon/Kyogre, will press A to trigger dialog";
                        Program.gCmdWindow.SetRadioOpponent();
                        break;
                    case 4:
                        typemessage = "Walk - Make sure you are one step south of the pokémon.";
                        resumemessage = "One step south of the pokémon, will press up to trigger dialog";
                        Program.gCmdWindow.SetRadioOpponent();
                        break;
                    case 5:
                        typemessage = "Dialga/Palkia/Giratina - Make sure you are anywhere in Dewford Town and the Eon Flute is the only registered item.";
                        resumemessage = "In Dewford Town, will press Y to activate the Eon Flute";
                        Program.gCmdWindow.SetRadioOpponent();
                        break;
                    case 6:
                        typemessage = "Tornadus/Thundurus/Landorus - Make sure you are anywhere in Route 120 and the Eon Flute is the only registered item.";
                        resumemessage = "In Dewford Town, will press Y to activate the Eon Flute";
                        Program.gCmdWindow.SetRadioOpponent();
                        break;
                    default:
                        typemessage = "No type - Select one type of soft-reset and try again.";
                        resumemessage = "";
                        break;
                }
                DialogResult dialogResult = MessageBox.Show("This bot will trigger an encounter with a pokémon, and soft-reset if it doesn't match with the loaded filters.\r\n\r\nType: " + typemessage + "\r\nResume: " + resumemessage + "\r\n\r\nPlease read the wiki at GitHub before using this bot. Do you want to continue?", "Soft-reset bot", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.OK && Mode.SelectedIndex >= 0)
                {
                    // Configure GUI
                    Delg.SetText(RunStop, "Stop Bot");
                    // Initialize variables
                    botworking = true;
                    userstop = false;
                    botState = BotState.botstart;
                    attempts = 0;
                    maxreconnect = 10;
                    resetNo = resetNo = Program.gCmdWindow.GetResetNumber();
                    walk = false;
                    steps = 0;
                    finishmessage = null;
                    // Run the bot
                    Program.gCmdWindow.SetBotMode(true);
                    RunBot();
                }
                else
                {
                    EnableControls();
                }
            }
        }

        private void DisableControls()
        {
            Delg.SetEnabled(Mode, false);
            Delg.SetEnabled(Resume, false);
            Delg.SetEnabled(LoadFilters, false);
            Delg.SetEnabled(ClearAll, false);
        }

        private void EnableControls()
        {
            Delg.SetEnabled(Mode, true);
            Delg.SetEnabled(Resume, true);
            Delg.SetEnabled(LoadFilters, true);
            Delg.SetEnabled(ClearAll, true);
        }

        public async void RunBot()
        {
            try
            {
                Program.gCmdWindow.SetBotMode(true);
                while (botworking && Program.gCmdWindow.IsConnected)
                {
                    switch (botState)
                    {
                        case BotState.botstart:
                            Report("Bot: START Gen 6 Soft-reset bot");
                            switch (Mode.SelectedIndex)
                            {
                                case 0:
                                    botState = Resume.Checked ? BotState.trigger : BotState.pssmenush;
                                    break;
                                case 1:
                                    botState = Resume.Checked ? BotState.miragespot : BotState.pssmenush;
                                    break;
                                case 2:
                                    botState = Resume.Checked ? BotState.tev_start : BotState.pssmenush;
                                    break;
                                case 3:
                                    botState = Resume.Checked ? BotState.trigger : BotState.fixwifi;
                                    break;
                                case 4:
                                    if (Resume.Checked)
                                    {
                                        steps = 0;
                                        walk = true;
                                        botState = BotState.twk_start;

                                    }
                                    else
                                    {
                                        steps = 0;
                                        walk = true;
                                        botState = BotState.pssmenush;
                                    }
                                    break;
                                case 5:
                                    soaringX = 30;
                                    soaringY = -100;
                                    soaringTime = 4000;
                                    if (Resume.Checked)
                                    {
                                        botState = BotState.soaring_start;
                                    }
                                    else
                                    {
                                        botState = BotState.pssmenush;
                                    }
                                    break;
                                case 6:
                                    soaringX = 0;
                                    soaringY = 100;
                                    soaringTime = 5000;
                                    if (Resume.Checked)
                                    {
                                        botState = BotState.soaring_start;
                                    }
                                    else
                                    {
                                        botState = BotState.pssmenush;
                                    }
                                    break;
                                default:
                                    botState = BotState.botexit;
                                    break;
                            }
                            break;

                        case BotState.pssmenush:
                            Report("Bot: Test if the PSS menu is shown");
                            waitTaskbool = Program.helper.memoryinrange(psssmenu1Off, psssmenu1IN, 0x10000);
                            if (await waitTaskbool)
                            {
                                botState = BotState.fixwifi;
                            }
                            else
                            {
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.botexit;
                            }
                            break;

                        case BotState.fixwifi:
                            waitTaskbool = Program.helper.waitNTRwrite(0x0105AE4, 0x4770, 0x1A);
                            if (await waitTaskbool)
                            {
                                botState = Mode.SelectedIndex == 3 ? BotState.trigger : BotState.touchpssset;
                            }
                            else
                            {
                                botresult = ErrorMessage.WriteError;
                                Report("Bot: Error detected");
                                attempts = 11;
                            }
                            break;

                        case BotState.touchpssset:
                            Report("Bot: Touch Box View");
                            await Task.Delay(commanddelay);
                            waitTaskbool = Program.helper.waittouch(240, 180);
                            if (await waitTaskbool)
                            {
                                botState = BotState.testpssset;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.TouchError;
                                botState = BotState.touchpssset;
                            }
                            break;

                        case BotState.testpssset:
                            Report("Bot: Test if the PSS setings are shown");
                            waitTaskbool = Program.helper.timememoryinrange(pssettingsOff, pssettingsIN, 0x10000, 100, 5000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = BotState.touchpssdis;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.touchpssset;
                            }
                            break;

                        case BotState.touchpssdis:
                            Report("Bot: Touch Disable PSS communication");
                            await Task.Delay(commanddelay);
                            waitTaskbool = Program.helper.waittouch(160, pssdisableY);
                            if (await waitTaskbool)
                            {
                                botState = BotState.testpssdis;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.TouchError;
                                botState = BotState.touchpssdis;
                            }
                            break;

                        case BotState.testpssdis:
                            Report("Bot: Test if PSS disable confirmation appears");
                            waitTaskbool = Program.helper.timememoryinrange(pssdisableOff, pssdisableIN, 0x10000, 100, 5000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = BotState.touchpssconf;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.touchpssdis;
                            }
                            break;

                        case BotState.touchpssconf:
                            Report("Bot: Touch Yes");
                            await Task.Delay(commanddelay);
                            waitTaskbool = Program.helper.waittouch(160, 120);
                            if (await waitTaskbool)
                            {
                                botState = BotState.testpssout;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.TouchError;
                                botState = BotState.touchpssconf;
                            }
                            break;

                        case BotState.testpssout:
                            Report("Bot: Test if back to PSS screen");
                            waitTaskbool = Program.helper.timememoryinrange(pssettingsOff, pssettingsOUT, 0x10000, 100, 5000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = BotState.returncontrol;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.touchpssconf;
                            }
                            break;

                        case BotState.returncontrol:
                            Report("Bot: Return contol to character");
                            await Task.Delay(6 * commanddelay);
                            waitTaskbool = Program.helper.waitbutton(LookupTable.ButtonA);
                            if (await waitTaskbool)
                            {
                                botState = BotState.touchsave;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = BotState.returncontrol;
                            }
                            break;

                        case BotState.touchsave:
                            Report("Bot: Touch Save button");
                            await Task.Delay(4 * commanddelay);
                            waitTaskbool = Program.helper.waittouch(220, 220);
                            if (await waitTaskbool)
                            {
                                botState = BotState.testsave;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.TouchError;
                                botState = BotState.touchsave;
                            }
                            break;

                        case BotState.testsave:
                            Report("Bot: Test if the save screen is shown");
                            waitTaskbool = Program.helper.timememoryinrange(savescrnOff, savescrnIN, 0x10000, 100, 5000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = BotState.saveconf;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.touchsave;
                            }
                            break;

                        case BotState.saveconf:
                            Report("Bot: Press Yes");
                            await Task.Delay(8 * commanddelay);
                            waitTaskbool = Program.helper.waitbutton(LookupTable.ButtonA);
                            if (await waitTaskbool)
                            {
                                botState = BotState.saveout;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = BotState.saveconf;
                            }
                            break;

                        case BotState.saveout:
                            Report("Bot: Test if the save screen is not shown");
                            waitTaskbool = Program.helper.timememoryinrange(savescrnOff, savescrnOUT, 0x10000, 100, 5000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                if (Mode.SelectedIndex == 2)
                                {
                                    Report("Bot: Soft-reset for party data intialize");
                                    botState = BotState.softreset;
                                }
                                else
                                {
                                    botState = BotState.typesr;
                                }
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.saveconf;
                            }
                            break;

                        case BotState.typesr:
                            switch (Mode.SelectedIndex)
                            {
                                case 0:
                                    botState = BotState.trigger;
                                    break;
                                case 1:
                                    botState = BotState.miragespot;
                                    break;
                                case 2:
                                    botState = BotState.tev_start;
                                    break;
                                case 3:
                                    botState = BotState.trigger;
                                    break;
                                case 4:
                                    botState = BotState.twk_start;
                                    break;
                                case 5:
                                case 6:
                                    botState = BotState.soaring_start;
                                    break;
                                default:
                                    botState = BotState.trigger;
                                    break;
                            }
                            break;

                        case BotState.trigger:
                            Report("Bot: Try to trigger encounter");
                            await Task.Delay(commanddelay);
                            waitTaskbool = Program.helper.waitbutton(LookupTable.ButtonA);
                            if (await waitTaskbool)
                            {
                                botState = BotState.readopp;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = BotState.trigger;

                            }
                            break;

                        case BotState.miragespot:
                            Report("Bot: Try to trigger encounter in mirage spot");
                            int i;
                            for (i = 0; i < 4; i++)
                            {
                                await Task.Delay(2 * commanddelay);
                                waitTaskbool = Program.helper.waitbutton(LookupTable.ButtonA);
                                if (!(await waitTaskbool))
                                {
                                    break;
                                }
                            }
                            if (i == 4)
                            {
                                botState = BotState.readopp;
                            }
                            else
                            {
                                botState = BotState.trigger;
                            }
                            break;

                        case BotState.readopp:
                            Report("Bot: Try to read opponent");
                            srpoke = null;
                            await Task.Delay(2 * commanddelay); // Wait for pokémon data
                            waitTaskPKM = Program.gCmdWindow.ReadOpponent();
                            srpoke = await waitTaskPKM;
                            if (srpoke == null)
                            { // No data received
                                if (walk)
                                {
                                    steps++;
                                    Report("Steps: " + steps);
                                    if (steps >= 10)
                                    {
                                        steps = 0;
                                        attempts++;
                                        botresult = ErrorMessage.ButtonError;
                                        botState = BotState.twk_start;
                                    }
                                    else
                                    {
                                        attempts++;
                                        botresult = ErrorMessage.ReadError;
                                        botState = BotState.trigger;
                                    }

                                }
                                else
                                {
                                    attempts++;
                                    botresult = ErrorMessage.ReadError;
                                    botState = BotState.trigger;
                                }
                            }
                            else if (srpoke.Species > 0)
                            {
                                attempts = 0;
                                botState = BotState.filter;
                            }
                            break;

                        case BotState.filter:
                            if (walk)
                            {
                                steps = 0;
                            }
                            filternum = CheckFilters(srpoke, filterList);
                            bool testsok = filternum > 0;
                            if (testsok)
                            {
                                botState = BotState.testspassed;
                            }
                            else
                            {
                                botState = BotState.softreset;
                            }
                            break;

                        case BotState.testspassed:
                            Report("Bot: All tests passed!");
                            finishmessage = new int[] { filternum, resetNo };
                            botresult = ErrorMessage.SRMatch;
                            botState = BotState.botexit;
                            break;

                        case BotState.softreset:
                            resetNo++;
                            Report("Bot: Soft-reset #" + resetNo.ToString());
                            Program.gCmdWindow.UpdateResetCounter(resetNo);
                            waitTaskbool = Program.helper.waitSoftReset();
                            if (await waitTaskbool)
                            {
                                botState = BotState.skipintro;
                            }
                            else
                            {
                                botresult = ErrorMessage.ButtonError;
                                botState = BotState.botexit;
                            }
                            break;

                        case BotState.skipintro:
                            await Task.Delay(44 * commanddelay);
                            Report("Bot: Skip intro cutscene");
                            Program.helper.quickbuton(LookupTable.ButtonA, commandtime);
                            await Task.Delay(commandtime + commanddelay);
                            botState = ORAS ? BotState.skiptitle : BotState.startgame;
                            break;

                        case BotState.skiptitle:
                            await Task.Delay(20 * commanddelay);
                            Report("Bot: Skip title screen");
                            Program.helper.quickbuton(LookupTable.ButtonA, commandtime);
                            await Task.Delay(commandtime + commanddelay);
                            botState = BotState.startgame;
                            break;

                        case BotState.startgame:
                            await Task.Delay(24 * commanddelay);
                            Report("Bot: Start game");
                            Program.helper.quickbuton(LookupTable.ButtonA, commandtime);
                            await Task.Delay(commandtime + commanddelay);
                            botState = BotState.reconnect;
                            break;

                        case BotState.reconnect:
                            await Task.Delay(16 * commanddelay);
                            waitTaskbool = Program.gCmdWindow.Reconnect();
                            if (await waitTaskbool)
                            {
                                await Task.Delay(8 * commanddelay);
                                botState = BotState.typesr;
                            }
                            else
                            {
                                botresult = ErrorMessage.GeneralError;
                                botState = BotState.botexit;
                            }
                            break;

                        case BotState.tev_start:
                            Report("Bot: Trigger Dialog");
                            await Task.Delay(commanddelay);
                            waitTaskbool = Program.helper.waitbutton(LookupTable.ButtonA);
                            if (await waitTaskbool)
                                botState = BotState.tev_dialog;
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = BotState.tev_start;
                            }
                            break;

                        case BotState.tev_dialog:
                            Report("Bot: Test if dialog has started");
                            waitTaskbool = Program.helper.timememoryinrange(dialogOff, dialogIN, 0x01, 100, 5000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = BotState.tev_cont1;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.tev_start;
                            }
                            break;

                        case BotState.tev_cont1:
                            Report("Bot: Talk to lady");
                            await Task.Delay(2 * commanddelay);
                            waitTaskbool = Program.helper.waitbutton(LookupTable.ButtonB);
                            if (await waitTaskbool)
                                botState = BotState.tev_check;
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = BotState.tev_cont1;
                            }
                            break;

                        case BotState.tev_check:
                            Report("Bot: Try to read party");
                            waitTaskPKM = Program.helper.waitPartyRead(2);
                            srpoke = await waitTaskPKM;
                            if (srpoke == null)
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.tev_check;
                            }
                            else if (srpoke.Species == 0)
                            {  // No data or invalid
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.tev_cont1;
                            }
                            else
                            {
                                attempts = 0;
                                botState = BotState.filter;
                            }
                            break;

                        case BotState.twk_start:
                            Report("Bot: Walk one step");
                            await Task.Delay(4 * commanddelay);
                            waitTaskbool = Program.helper.waitbutton(LookupTable.RunUp);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = BotState.trigger;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = BotState.twk_start;
                            }
                            break;

                        case BotState.soaring_start:
                            Report("Bot: Activate Eon Flute");
                            waitTaskbool = Program.helper.waitbutton(LookupTable.ButtonY);
                            if (await waitTaskbool)
                            {
                                botState = BotState.soaring_cont;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = BotState.soaring_start;
                            }
                            break;

                        case BotState.soaring_cont:
                            Report("Bot: Start cutscene");
                            await Task.Delay(commanddelay);
                            waitTaskbool = Program.helper.waitbutton(LookupTable.ButtonA);
                            if (await waitTaskbool)
                            {
                                botState = BotState.soaring_check;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = BotState.soaring_cont;
                            }
                            break;

                        case BotState.soaring_check:
                            await Task.Delay(8000);
                            Report("Bot: Test if ready to move");
                            waitTaskbool = Program.helper.timememoryinrange(soaringOff, soaringIN, 0x10000, 1000, 10000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = BotState.soaring_move;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.soaring_start;
                            }
                            break;

                        case BotState.soaring_move:
                            Report("Bot: Start movement");
                            Program.helper.quickstick(soaringX, soaringY, soaringTime);
                            await Task.Delay(soaringTime + 2 * commanddelay);
                            botState = BotState.soaring_dialog;
                            break;

                        case BotState.soaring_dialog:
                            Report("Bot: Test dialog has started");
                            waitTaskbool = Program.helper.timememoryinrange(soaringdialogOff, soaringdialogIN, 0x10, 500, 5000);
                            if (await waitTaskbool)
                            {
                                botState = BotState.miragespot;
                            }
                            else
                            {
                                Report("Bot: Dialog failed, imposible to check position to continue, will check random encounter");
                                botState = BotState.random_enc_check;
                            }
                            break;

                        case BotState.random_enc_check:
                            Report("Bot: Test if random sky encounter");
                            srpoke = null;
                            await Task.Delay(2 * commanddelay); // Wait for pokémon data
                            waitTaskPKM = Program.gCmdWindow.ReadOpponent();
                            srpoke = await waitTaskPKM;
                            if (srpoke == null)
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.random_enc_check;
                            }
                            if (srpoke.Species > 0)
                            {
                                botState = BotState.flee1;
                            }
                            else
                            {
                                Report("Bot: No random encounter, bot cannot continue");
                                botresult = ErrorMessage.GeneralError;
                                botState = BotState.botexit;
                            }

                            break;

                        case BotState.flee1:
                            Report("Bot: Fleeing from random encounter, pressing down");
                            await Task.Delay(20 * commanddelay);    //30?
                            waitTaskbool = Program.helper.waitbutton(LookupTable.DPadDown);
                            if (await waitTaskbool)
                            {
                                botState = BotState.flee2;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = BotState.flee1;
                            }
                            break;

                        case BotState.flee2:
                            Report("Bot: Fleeing from random encounter, pressing right");
                            await Task.Delay(10 * commanddelay);
                            waitTaskbool = Program.helper.waitbutton(LookupTable.DPadRight);
                            if (await waitTaskbool)
                            {
                                botState = BotState.flee3;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = BotState.flee2;
                            }
                            break;

                        case BotState.flee3:
                            Report("Bot: Fleeing from random encounter, pressing A");
                            await Task.Delay(commanddelay);
                            waitTaskbool = Program.helper.waitbutton(LookupTable.ButtonA);
                            if (await waitTaskbool)
                            {
                                await Task.Delay(8 * commanddelay);
                                botState = BotState.soaring_move;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = BotState.flee3;
                            }
                            break;

                        case BotState.botexit:
                            Report("Bot: STOP Gen 6 Soft-reset bot");
                            botworking = false;
                            break;

                        default:
                            Report("Bot: STOP Gen 6 Soft-reset bot");
                            botresult = ErrorMessage.GeneralError;
                            botworking = false;
                            break;
                    }
                    if (attempts > 10)
                    { // Too many attempts
                        if (maxreconnect > 0)
                        {
                            Report("Bot: Try reconnection to fix error");
                            waitTaskbool = Program.gCmdWindow.Reconnect();
                            maxreconnect--;
                            if (await waitTaskbool)
                            {
                                await Task.Delay(10 * commanddelay);
                                attempts = 0;
                            }
                            else
                            {
                                botresult = ErrorMessage.GeneralError;
                                botworking = false;
                            }
                        }
                        else
                        {
                            Report("Bot: Maximum number of reconnection attempts reached");
                            Report("Bot: STOP Gen 6 Soft-reset bot");
                            botworking = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Report("Bot: Exception detected:");
                Report(ex.Source);
                Report(ex.Message);
                Report(ex.StackTrace);
                Report("Bot: STOP Gen 6 Soft-reset bot");
                MessageBox.Show(ex.Message);
                botworking = false;
                botresult = ErrorMessage.GeneralError;
            }
            if (userstop)
            {
                botresult = ErrorMessage.UserStop;
            }
            else if (!Program.gCmdWindow.IsConnected)
            {
                botresult = ErrorMessage.Disconnect;
            }
            ShowResult("Soft-reset bot", botresult, finishmessage);
            Delg.SetText(RunStop, "Start Bot");
            Program.gCmdWindow.SetBotMode(false);
            EnableControls();
            Delg.SetEnabled(RunStop, true);
        }

        private void ClearAll_Click(object sender, EventArgs e)
        {
            Delg.SetChecked(Resume, false);
            filterList.Rows.Clear();
        }

        private void LoadFilters_Click(object sender, EventArgs e)
        {
            try
            {
                (new FileInfo(BotFolder)).Directory.Create();
                OpenFileDialog openFileDialog1 = new OpenFileDialog()
                {
                    Filter = "PKMN-NTR Filter|*.pftr",
                    Title = "Select a filter set",
                    InitialDirectory = BotFolder
                };
                openFileDialog1.ShowDialog();
                if (openFileDialog1.FileName != "")
                {
                    filterList.Rows.Clear();
                    List<int[]> rows = File.ReadAllLines(openFileDialog1.FileName).Select(s => s.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()).ToList();
                    foreach (int[] row in rows)
                    {
                        filterList.Rows.Add(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7], row[8], row[9], row[10], row[11], row[12], row[13], row[14], row[15], row[16], row[17], row[18]);
                    }
                    MessageBox.Show("Filter Set loaded correctly.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Bot_SoftReset6_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (botworking)
            {
                MessageBox.Show("Stop the bot before closing this window", "Soft-reset bot", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }

        private void Bot_SoftReset6_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.gCmdWindow.Tool_Finish();
        }
    }
}
