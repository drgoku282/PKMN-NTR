/// PKMN-NTR - On-the-air memory editor for 3DS Pokémon games
/// Copyright(C) 2016-2018  PKMN-NTR Dev Team
///
/// This program is free software: you can redistribute it and/or modify
/// it under the terms of the GNU General Public License as published by
/// the Free Software Foundation, either version 3 of the License, or
/// (at your option) any later version.
///
/// This program is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
/// GNU General Public License for more details.
///
/// You should have received a copy of the GNU General Public License
/// along with this program.If not, see<http://www.gnu.org/licenses/>.
///

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
    /// Generation 7 Soft-reset bot.
    /// </summary>
    public partial class Bot_SoftReset7 : Form
    {
        /// <summary>
        /// Secuency of steps done by the bot.
        /// </summary>
        public enum BotState
        {
            BotStart, SelectMode, StartDialog, TestDialog1, ReadParty,
            ContinueDialog, TestDialog2, ExitDialog, Filter, TestsPassed, SoftReset,
            SkipTitle, Reconnect, NFCPatch, StartGame, NickScreen, TriggerBattle,
            TestDialog3, ContinueDialog2, ReadOpponent, Soluna1, Soluna2, Soluna3,
            Soluna4, Soluna5, RunBattle1, RunBattle2, RunBattle3, WriteHoney, OpenMenu,
            TestMenu, OpenBag, TestBag, SelectHoney, ActivateHoney, TestWildPoke,
            WaitWildPoke, ReadWildPoke, dismissmsg, botexit
        };

        // General bot variables
        private bool botworking;
        private bool userstop;
        private BotState botState;
        private ErrorMessage botresult;
        private int attempts;
        private int maxreconnect;
        private Task<bool> waitTaskbool;
        private Task<PKM> waitTaskPKM;

        // Class variables
        private bool isub;
        private int resetNo;
        private int honeynum;
        private int filternum;
        private int[] finishmessage;
        private PKM srPoke;

        // Data Offsets
        private uint DialogOffset
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0x6749A4; // 1.0: 0x63DD68; 1.1: 0x67499C;
                    case GameVersion.US:
                    case GameVersion.UM:
                        return 0x6A668C;
                    default:
                        return 0;
                }
            }
        }
        private uint DialogIN
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0x80000000; // 1.0: 0x09;
                    case GameVersion.US:
                    case GameVersion.UM:
                        return 0x80008;
                    default:
                        return 0x0;
                }
            }
        }
        private uint DialogOut
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0x80000000; // 1.0: 0x08;
                    case GameVersion.US:
                    case GameVersion.UM:
                        return 0x800080;
                    default:
                        return 0x0;
                }
            }
        }
        private uint DialogRange
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0x10000000; // 1.0: 0x08;
                    case GameVersion.US:
                    case GameVersion.UM:
                        return 0x100;
                    default:
                        return 0x0;
                }
            }
        }
        private uint BattleOffset
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0x6747E0;// 1.0: 0x6731A4: 1.1: 0x6747D8;
                    case GameVersion.US:
                    case GameVersion.UM:
                        return 0x67107C;
                    default:
                        return 0x0;
                }
            }
        }
        private uint BattleIN
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0x40400000; // 1.0: 0x00000000;;
                    case GameVersion.US:
                    case GameVersion.UM:
                        return 0x304B742C;
                    default:
                        return 0x0;
                }
            }
        }
        private uint BattleOUT
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0x00000000; // 1.0: 0x00FFFFFF;
                    case GameVersion.US:
                    case GameVersion.UM:
                        return 0x0000000;
                    default:
                        return 0x0;
                }
            }
        }
        private uint BattleRange
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0x10000;
                    case GameVersion.US:
                    case GameVersion.UM:
                        return 0x1;
                    default:
                        return 0x0;
                }
            }
        }
        private uint MenuOffset
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0x674974; // 1.0: 0x672920; 1.1: 0x67496C
                    case GameVersion.US:
                    case GameVersion.UM:
                        return 0x6A60EA;
                    default:
                        return 0x0;
                }
            }
        }
        private uint MenuIN
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0x80000000;
                    case GameVersion.US:
                    case GameVersion.UM:
                        return 0x0;
                    default:
                        return 0x0;
                }
            }
        }
        private uint BagOffset
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return  0x674800; // 1.0: 0x67DF74; 1.1: 0x6747F8;
                    case GameVersion.US:
                    case GameVersion.UM:
                        return 0x66EB54;
                    default:
                        return 0x0;
                }
            }
        }
        private uint BagIN
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0x41280000; // 1.0: 0x01;
                    case GameVersion.US:
                    case GameVersion.UM:
                        return 0x01;
                    default:
                        return 0x0;
                }
            }
        }
        private uint BagOut
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0x00000000; // 1.0: 0x03;
                    case GameVersion.US:
                    case GameVersion.UM:
                        return 0x2;
                    default:
                        return 0x0;
                }
            }
        }
        private uint BagRange
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0x10000;
                    case GameVersion.US:
                    case GameVersion.UM:
                        return 0x1;
                    default:
                        return 0x0;
                }
            }
        }
        private uint NicknameLow
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0x3D000000;
                    case GameVersion.US:
                    case GameVersion.UM:
                        return 0x00080020;
                    default:
                        return 0x0;
                }
            }
        }
        private uint NicknameHigh
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0x3E000000;
                    case GameVersion.US:
                    case GameVersion.UM:
                        return 0x0008000;
                    default:
                        return 0x0;
                }
            }
        }
        private uint PokedexLow
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0x3F000000;
                    case GameVersion.US:
                    case GameVersion.UM:
                        return 0x00100010;
                    default:
                        return 0x0;
                }
            }
        }
        private uint PokedexHigh
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0x40000000;
                    case GameVersion.US:
                    case GameVersion.UM:
                        return 0x00200021;
                    default:
                        return 0x0;
                }
            }
        }
        private uint honey = 0x000F9C5E;

        public Bot_SoftReset7()
        {
            InitializeComponent();
            Species.DisplayMember = "Text";
            Species.ValueMember = "Value";
            Species.DataSource = new BindingSource(GameInfo.SpeciesDataSource.Where(s =>
            s.Value <= Program.gCmdWindow.SAV.MaxSpeciesID).ToList(), null);
            Delg.SetSelectedValue(Species, 1);
            Program.gCmdWindow.SetResetLabel("Number of resets:");
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
                switch (Mode.SelectedIndex)
                {
                    case 0:
                        typemessage = "Event - Make sure you are in front of the man " +
                            "in the Pokémon Center. Also, you must only have one " +
                            "pokémon in your party.";
                        Program.gCmdWindow.SetRadioParty();
                        Program.gCmdWindow.SetResetLabel("Number of resets:");
                        break;
                    case 1:
                        typemessage = "Type: Null (SM) - Make sure you are in front of " +
                            "Gladion at the Aether Paradise. Also, you must only have " +
                            "one pokémon in your party.\r\n\r\nThis mode can also be " +
                            "used for event pokémon.";
                        Program.gCmdWindow.SetRadioParty();
                        Program.gCmdWindow.SetResetLabel("Number of resets:");
                        break;
                    case 2:
                        typemessage = "Tapus (SM) - Make sure you are in front of the " +
                            "statue at the ruins.";
                        Program.gCmdWindow.SetRadioOpponent();
                        Program.gCmdWindow.SetResetLabel("Number of resets:");
                        break;
                    case 3:
                        typemessage = "Solgaleo/Lunala (SM) - Make sure you are in " +
                            "front of Solgaleo/Lunala at the Altar of the Sunne/Moone.";
                        Program.gCmdWindow.SetRadioOpponent();
                        Program.gCmdWindow.SetResetLabel("Number of resets:");
                        break;
                    case 4:
                        typemessage = "Wild Pokémon - Make sure you are in the place " +
                            "where wild pokémon can appear. Also, check that Honey is " +
                            "the item at the top of your Item list and can be selected " +
                            "by just opening the menu and pressing A.";
                        Program.gCmdWindow.SetRadioOpponent();
                        Program.gCmdWindow.SetResetLabel("Total Encounters:");
                        break;
                    case 5:
                        typemessage = "Ultra Beast/Necrozma (SM) - Make sure you are " +
                            "in the place where the Ultra Beast / Necrozma appears. " +
                            "Also, check that Honey is the item at the top of your " +
                            "Item list and can be selected by just opening the menu " +
                            "and pressing A.";
                        Program.gCmdWindow.SetRadioOpponent();
                        Program.gCmdWindow.SetResetLabel("Total Encounters:");
                        break;
                    default:
                        typemessage = "No type - Select one type of soft-reset and " +
                            "try again.";
                        break;
                }

                DialogResult dialogResult = MessageBox.Show("This bot will trigger an " +
                    "encounter with a pokémon, and soft-reset if it doesn't match with " +
                    "the loaded filters.\r\n\r\nType: " + typemessage + "\r\n\r\n" +
                    "Please read the wiki at GitHub before using this bot. Do you want " +
                    "to continue?", "Soft-reset bot", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.OK && Mode.SelectedIndex >= 0)
                {
                    // Configure GUI
                    Delg.SetText(RunStop, "Stop Bot");
                    // Initialize variables
                    botworking = true;
                    userstop = false;
                    botState = BotState.BotStart;
                    attempts = 0;
                    maxreconnect = 10;
                    resetNo = Program.gCmdWindow.GetResetNumber();
                    isub = false;
                    honeynum = 0;
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
            Delg.SetEnabled(Species, false);
            Delg.SetEnabled(LoadFilters, false);
            Delg.SetEnabled(ClearAll, false);
        }

        private void EnableControls()
        {
            Delg.SetEnabled(Mode, true);
            Delg.SetEnabled(Species, true);
            Delg.SetEnabled(LoadFilters, true);
            Delg.SetEnabled(ClearAll, true);
        }

        public async void RunBot()
        {
            try
            {
                while (botworking && Program.gCmdWindow.IsConnected)
                {
                    switch (botState)
                    {
                        case BotState.BotStart:
                            Report("Bot: START Gen 7 Soft-reset bot");
                            botState = BotState.SelectMode;
                            break;

                        case BotState.SelectMode:
                            switch (Mode.SelectedIndex)
                            {
                                case 0:
                                case 1:
                                    botState = BotState.StartDialog;
                                    break;
                                case 2:
                                    botState = BotState.TriggerBattle;
                                    break;
                                case 3:
                                    resetNo = resetNo == 0 ? 1 : resetNo;
                                    Program.gCmdWindow.UpdateResetCounter(resetNo);
                                    botState = BotState.Soluna1;
                                    break;
                                case 4:
                                case 5:
                                    resetNo = resetNo == 0 ? 1 : resetNo;
                                    Program.gCmdWindow.UpdateResetCounter(resetNo);
                                    botState = BotState.WriteHoney;
                                    break;
                                default:
                                    botState = BotState.botexit;
                                    break;
                            }
                            break;

                        case BotState.StartDialog:
                            Report("Bot: Start dialog");
                            waitTaskbool = Program.helper.waitbutton(LookupTable.ButtonA);
                            if (await waitTaskbool)
                            {
                                botState = BotState.TestDialog1;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = BotState.StartDialog;
                            }
                            break;

                        case BotState.TestDialog1:
                            Report("Bot: Test if dialog has started");
                            waitTaskbool = Program.helper.memoryinrange(DialogOffset, 
                                DialogIN, DialogRange);
                            if (await waitTaskbool)
                            {
                                if (Mode.SelectedIndex == 1)
                                {
                                    attempts = -40; // Type:Null dialog is longer
                                }
                                else
                                {
                                    attempts = -15;
                                }
                                botState = BotState.ContinueDialog;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.StartDialog;
                            }
                            break;

                        case BotState.ContinueDialog:
                            Report("Bot: Continue dialog");
                            waitTaskbool = Program.helper.waitbutton(LookupTable.ButtonA);
                            if (await waitTaskbool)
                            {
                                botState = BotState.TestDialog2;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = BotState.ContinueDialog;
                            }
                            break;

                        case BotState.TestDialog2:
                            Report("Bot: Test if dialog has finished");
                            waitTaskbool = Program.helper.memoryinrange(DialogOffset, 
                                DialogOut, DialogRange);
                            if (await waitTaskbool)
                            {
                                attempts = -10;
                                botState = BotState.ReadParty;
                            }
                            else if (Program.helper.lastRead >= PokedexLow && 
                                Program.helper.lastRead < PokedexHigh)
                            {
                                Report("Bot: Pokedex screen detected");
                                attempts = -10;
                                botState = BotState.ExitDialog;
                            }
                            else if (Program.helper.lastRead >= NicknameLow && 
                                Program.helper.lastRead < NicknameHigh)
                            {
                                Report("Bot: Nickname screen detected");
                                attempts = 0;
                                botState = BotState.NickScreen;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.ContinueDialog;
                            }
                            break;

                        case BotState.ExitDialog:
                            Report("Bot: Exit dialog");
                            waitTaskbool = Program.helper.waitbutton(LookupTable.ButtonB);
                            if (await waitTaskbool)
                            {
                                botState = BotState.ReadParty;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = BotState.ExitDialog;
                            }
                            break;

                        case BotState.ReadParty:
                            Report("Bot: Try to read party");
                            waitTaskPKM = Program.helper.waitPartyRead(2);
                            srPoke = await waitTaskPKM;
                            if (srPoke == null)
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.ExitDialog;
                            }
                            else if (srPoke.Species == 0)
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.ExitDialog;
                            }
                            else
                            {
                                attempts = 0;
                                botState = BotState.Filter;
                            }
                            break;

                        case BotState.Filter:
                            filternum = CheckFilters(srPoke, filterList);
                            bool testsok = filternum > 0;
                            if (testsok)
                            {
                                botState = BotState.TestsPassed;
                            }
                            else if (Mode.SelectedIndex == 3 || Mode.SelectedIndex == 4 
                                || Mode.SelectedIndex == 5)
                            {
                                botState = BotState.RunBattle1;
                            }
                            else
                            {
                                botState = BotState.SoftReset;
                            }
                            break;

                        case BotState.TestsPassed:
                            Report("Bot: All tests passed!");
                            if (Mode.SelectedIndex == 3 || Mode.SelectedIndex == 4 
                                || Mode.SelectedIndex == 5)
                            {
                                botresult = ErrorMessage.BattleMatch;
                            }
                            else
                            {
                                botresult = ErrorMessage.SRMatch;
                            }
                            finishmessage = new int[] { filternum, resetNo };
                            botState = BotState.botexit;
                            break;

                        case BotState.SoftReset:
                            resetNo++;
                            Report("Bot: Sof-reset #" + resetNo.ToString());
                            Program.gCmdWindow.UpdateResetCounter(resetNo);
                            waitTaskbool = Program.helper.waitSoftReset();
                            if (await waitTaskbool)
                            {
                                botState = BotState.SkipTitle;
                            }
                            else
                            {
                                botresult = ErrorMessage.ButtonError;
                                botState = BotState.botexit;
                            }
                            break;

                        case BotState.SkipTitle:
                            await Task.Delay(7000);
                            Report("Bot: Open Menu");
                            waitTaskbool = Program.helper.waitbutton(LookupTable.ButtonA);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = BotState.Reconnect;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = BotState.SkipTitle;
                            }
                            break;

                        case BotState.Reconnect:
                            Report("Bot: Try reconnect");
                            waitTaskbool = Program.gCmdWindow.Reconnect();
                            if (await waitTaskbool)
                            {
                                await Task.Delay(1000);
                                botState = BotState.NFCPatch;
                            }
                            else
                            {
                                botresult = ErrorMessage.GeneralError;
                                botState = BotState.botexit;
                            }
                            break;

                        case BotState.NFCPatch:
                            Report("Bot: Apply NFC patch");
                            waitTaskbool = Program.helper.waitNTRwrite(
                                LookupTable.NFCOffset, LookupTable.NFCValue, 
                                Program.gCmdWindow.pid);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = BotState.StartGame;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.WriteError;
                                botState = BotState.NFCPatch;
                            }
                            break;

                        case BotState.StartGame:
                            Report("Bot: Start the game");
                            await Task.Delay(1000);
                            waitTaskbool = Program.helper.waitbutton(LookupTable.ButtonA);
                            if (await waitTaskbool)
                            {
                                await Task.Delay(3000);
                                attempts = 0;
                                botState = BotState.SelectMode;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = BotState.StartGame;
                            }
                            break;

                        case BotState.NickScreen:
                            Report("Bot: Nickname screen");
                            waitTaskbool = Program.helper.waitbutton(LookupTable.ButtonStart);
                            await Task.Delay(250);
                            waitTaskbool = Program.helper.waitbutton(LookupTable.ButtonA);
                            if (await waitTaskbool)

                            {
                                botState = BotState.TestDialog2;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = BotState.NickScreen;
                            }
                            break;

                        case BotState.TriggerBattle:
                            Report("Bot: Try to trigger battle");
                            waitTaskbool = Program.helper.waitbutton(LookupTable.ButtonA);
                            if (await waitTaskbool)
                            {
                                botState = BotState.TestDialog3;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = BotState.TriggerBattle;
                            }
                            break;

                        case BotState.TestDialog3:
                            Report("Bot: Test if dialog has started");
                            waitTaskbool = Program.helper.memoryinrange(DialogOffset, 
                                DialogIN, DialogRange);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = BotState.ContinueDialog2;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.TriggerBattle;
                            }
                            break;

                        case BotState.ContinueDialog2:
                            Report("Bot: Continue dialog");
                            waitTaskbool = Program.helper.waitbutton(LookupTable.ButtonA);
                            if (await waitTaskbool)
                            {
                                botState = BotState.ReadOpponent;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = BotState.ContinueDialog2;
                            }
                            break;

                        case BotState.ReadOpponent:
                            Report("Bot: Try to read opponent");
                            srPoke = null;
                            waitTaskPKM = Program.helper.waitPokeRead(LookupTable
                                .WildOffset1);
                            srPoke = await waitTaskPKM;
                            if (srPoke == null)
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.ReadOpponent;
                            }
                            else if (srPoke.Species == 0)
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.ContinueDialog2;
                            }
                            else
                            {
                                attempts = 0;
                                botState = BotState.Filter;
                            }
                            break;

                        case BotState.Soluna1:
                            Report("Bot: Walk to legendary pokemon");
                            waitTaskbool = Program.helper.waitsitck(0, 100);
                            if (await waitTaskbool)
                            {
                                botState = BotState.Soluna2;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.StickError;
                                botState = BotState.Soluna1;
                            }
                            break;

                        case BotState.Soluna2:
                            Report("Bot: Trigger battle #" + resetNo);
                            waitTaskbool = Program.helper.waitbutton(LookupTable.ButtonA);
                            if (await waitTaskbool)
                            {
                                botState = BotState.Soluna3;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = BotState.Soluna2;
                            }
                            break;

                        case BotState.Soluna3:
                            Report("Bot: Test if dialog has started");
                            waitTaskbool = Program.helper.memoryinrange(DialogOffset, 
                                DialogIN, DialogRange);
                            if (await waitTaskbool)
                            {
                                attempts = -0;
                                botState = BotState.Soluna4;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.Soluna2;
                            }
                            break;

                        case BotState.Soluna4:
                            Report("Bot: Test if data is available");
                            waitTaskbool = Program.helper.timememoryinrange(BattleOffset,
                                BattleIN, BattleRange, 1000, 20000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = BotState.Soluna5;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.Soluna1;
                            }
                            break;

                        case BotState.Soluna5:
                            Report("Bot: Try to read opponent");
                            srPoke = null;
                            waitTaskPKM = Program.helper.waitPokeRead(LookupTable
                                .WildOffset1);
                            srPoke = await waitTaskPKM;
                            if (srPoke == null)
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.Soluna5;
                            }
                            else if (srPoke.Species == 0)
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.Soluna2;
                            }
                            else
                            {
                                attempts = 0;
                                botState = BotState.Filter;
                            }
                            break;

                        case BotState.RunBattle1:
                            Report("Bot: Run from battle");
                            await Task.Delay(2000);
                            waitTaskbool = Program.helper.waitbutton(LookupTable
                                .DPadDown);
                            if (await waitTaskbool)
                            {
                                botState = BotState.RunBattle2;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = BotState.RunBattle1;
                            }
                            break;

                        case BotState.RunBattle2:
                            waitTaskbool = Program.helper.waitbutton(LookupTable.ButtonA);
                            if (await waitTaskbool)
                            {
                                botState = BotState.RunBattle3;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = BotState.RunBattle2;
                            }
                            break;

                        case BotState.RunBattle3:
                            Report("Bot: Test out from battle");
                            waitTaskbool = Program.helper.timememoryinrange(BattleOffset,
                                BattleOUT, BattleRange, 1000, 10000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                resetNo++;
                                Program.gCmdWindow.UpdateResetCounter(resetNo);
                                if (Mode.SelectedIndex == 3)
                                {
                                    botState = BotState.Soluna1;
                                    await Task.Delay(6000);
                                }
                                else if (isub)
                                {
                                    botState = BotState.dismissmsg;
                                    await Task.Delay(6000);
                                }
                                else
                                {
                                    botState = BotState.WriteHoney;
                                    await Task.Delay(5000);
                                }

                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.RunBattle1;
                            }
                            break;

                        case BotState.WriteHoney:
                            if (honeynum >= 10)
                            {
                                botState = BotState.OpenMenu;
                            }
                            else
                            {
                                Report("Bot: Give 999 honey");
                                waitTaskbool = Program.helper.waitNTRwrite(LookupTable
                                    .ItemsOffset, honey, Program.gCmdWindow.pid);
                                if (await waitTaskbool)
                                {
                                    attempts = 0;
                                    honeynum = 999;
                                    botState = BotState.OpenMenu;
                                }
                                else
                                {
                                    attempts++;
                                    botresult = ErrorMessage.WriteError;
                                    botState = BotState.WriteHoney;
                                }
                            }
                            break;

                        case BotState.OpenMenu:
                            Report("Bot: Open Menu");
                            waitTaskbool = Program.helper.waitbutton(LookupTable.ButtonX);
                            if (await waitTaskbool)
                            {
                                botState = BotState.TestMenu;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = BotState.OpenMenu;
                            }
                            break;

                        case BotState.TestMenu:
                            Report("Bot: Test if the menu is open");
                            waitTaskbool = Program.helper.timememoryinrange(MenuOffset, 
                                MenuIN, 0x10000000, 1000, 5000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = BotState.OpenBag;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.OpenMenu;
                            }
                            break;

                        case BotState.OpenBag:
                            Report("Bot: Open Bag");
                            waitTaskbool = Program.helper.waitbutton(LookupTable.ButtonA);
                            if (await waitTaskbool)
                            {
                                botState = BotState.TestBag;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = BotState.OpenBag;
                            }
                            break;

                        case BotState.TestBag:
                            Report("Bot: Test if the bag is open");
                            waitTaskbool = Program.helper.timememoryinrange(BagOffset, 
                                BagIN, 0x10000, 1000, 5000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = BotState.SelectHoney;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.OpenBag;
                            }
                            break;

                        case BotState.SelectHoney:
                            Report("Bot: Select Honey");
                            waitTaskbool = Program.helper.waitbutton(LookupTable.ButtonA);
                            if (await waitTaskbool)
                            {
                                botState = BotState.ActivateHoney;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = BotState.SelectHoney;
                            }
                            break;

                        case BotState.ActivateHoney:
                            Report("Bot: Trigger battle #" + resetNo);
                            waitTaskbool = Program.helper.waitbutton(LookupTable.ButtonA);
                            if (await waitTaskbool)
                            {
                                botState = BotState.TestWildPoke;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = BotState.OpenBag;
                            }
                            break;

                        case BotState.TestWildPoke:
                            Report("Bot: Test if battle is triggered");
                            waitTaskbool = Program.helper.timememoryinrange(BagOffset,
                                BagOut, 0x10000, 1000, 10000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                honeynum--;
                                botState = BotState.WaitWildPoke;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.ActivateHoney;
                            }
                            break;

                        case BotState.WaitWildPoke:
                            Report("Bot: Test if data is available");
                            waitTaskbool = Program.helper.timememoryinrange(BattleOffset,
                                BattleIN, BattleRange, 1000, 20000);
                            if (await waitTaskbool)
                            {
                                if (Program.gCmdWindow.IsUSUM)
                                {
                                    Report("Bot: Wait 10 seconds");
                                    await Task.Delay(10000);
                                }
                                attempts = 0;
                                botState = BotState.ReadWildPoke;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.ActivateHoney;
                            }
                            break;

                        case BotState.ReadWildPoke:
                            Report("Bot: Try to read opponent");
                            srPoke = null;
                            waitTaskPKM = Program.helper.waitPokeRead(LookupTable
                                .WildOffset1);
                            srPoke = await waitTaskPKM;
                            if (srPoke == null)
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.ReadWildPoke;
                            }
                            else if (srPoke.Species == 0)
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.ReadWildPoke;
                            }
                            else if (srPoke.Species == WinFormsUtil.getIndex(Species))
                            {
                                attempts = 0;
                                isub = Mode.SelectedIndex == 5;
                                botState = BotState.Filter;
                            }
                            else
                            {
                                attempts = 0;
                                isub = false;
                                botState = BotState.RunBattle1;
                            }
                            break;

                        case BotState.dismissmsg:
                            Report("Bot: Dismiss message");
                            waitTaskbool = Program.helper.waitbutton(LookupTable.ButtonA);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = BotState.WriteHoney;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = BotState.dismissmsg;
                            }
                            break;

                        case BotState.botexit:
                            Report("Bot: STOP Gen 7 Soft-reset bot");
                            botworking = false;
                            break;

                        default:
                            Report("Bot: STOP Gen 7 Soft-reset bot");
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
                                await Task.Delay(2500);
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
                            Report("Bot: STOP Gen 7 Soft-reset bot");
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
            filterList.Rows.Clear();
            Delg.SetSelectedValue(Species, 1);
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
                    List<int[]> rows = File.ReadAllLines(openFileDialog1.FileName).Select(
                        s => s.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse).ToArray()).ToList();
                    foreach (int[] row in rows)
                    {
                        filterList.Rows.Add(row[0], row[1], row[2], row[3], row[4],
                            row[5], row[6], row[7], row[8], row[9], row[10], row[11],
                            row[12], row[13], row[14], row[15], row[16], row[17], row[18]);
                    }
                    MessageBox.Show("Filter Set loaded correctly.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Bot_SoftReset7_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (botworking)
            {
                MessageBox.Show("Stop the bot before closing this window", 
                    "Soft-reset bot", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }

        private void Bot_SoftReset7_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.gCmdWindow.Tool_Finish();
        }
    }
}
