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
    public partial class Bot_SoftReset7 : Form
    {
        public enum srbotStates { botstart, selectmode, startdialog, testdialog1, readparty, continuedialog, testdialog2, exitdialog, filter, testspassed, softreset, skiptitle, reconnect, connpatch, startgame, nickname, triggerbattle, testdialog3, continuedialog2, readopp, soluna1, soluna2, soluna3, soluna4, soluna5, runbattle1, runbattle2, runbattle3, writehoney, openmenu, testmenu, openbag, testbag, selecthoney, activatehoney, testwild, waitwild, readwild, dismissmsg, botexit };

        // General bot variables
        private bool botworking;
        private bool userstop;
        private srbotStates botState;
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
        private uint dialogOff = 0x6749A4; // 1.0: 0x63DD68; 1.1: 0x67499C
        private uint dialogIn = 0x80000000; // 1.0: 0x0C;
        private uint dialogOut = 0x00000000; // 1.0: 0x0B;
        private uint battleOff = 0x6747E0;// 1.0: 0x6731A4: 1.1: 0x6747D8;
        private uint battleIn = 0x40400000; // 1.0: 0x00000000;
        private uint battleOut = 0x00000000; // 1.0: 0x00FFFFFF;
        private uint partyOff = 0x34195E10;
        private uint opponentOff = 0x3254F4AC;
        private uint itemOff = 0x330D5934;
        private uint honey = 0x000F9C5E;
        private uint menuOff = 0x674974; // 1.0: 0x672920; 1.1: 0x67496C
        private uint menuIn = 0x80000000;
        //private uint menuOut = 0x00000000;
        private uint bagOff = 0x674800; // 1.0: 0x67DF74; 1.1: 0x6747F8;
        private uint bagIn = 0x41280000; // 1.0: 0x01;
        private uint bagOut = 0x00000000; // 1.0: 0x03;

        public Bot_SoftReset7()
        {
            InitializeComponent();
            Species.DisplayMember = "Text";
            Species.ValueMember = "Value";
            Species.DataSource = new BindingSource(GameInfo.SpeciesDataSource.Where(s => s.Value <= Program.gCmdWindow.SAV.MaxSpeciesID).ToList(), null);
            Delg.SetSelectedValue(Species, 1);
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
                        typemessage = "Event - Make sure you are in front of the man in the Pokémon Center. Also, you must only have one pokémon in your party.";
                        Program.gCmdWindow.SetRadioParty();
                        Program.gCmdWindow.labelreset.Visible = true;
                        Program.gCmdWindow.resetNoBox.Visible = true;
                        break;
                    case 1:
                        typemessage = "Type: Null - Make sure you are in front of Gladion at the Aether Paradise. Also, you must only have one pokémon in your party.\r\n\r\nThis mode can also be used for event pokémon.";
                        Program.gCmdWindow.SetRadioParty();
                        Program.gCmdWindow.labelreset.Visible = true;
                        Program.gCmdWindow.resetNoBox.Visible = true;
                        break;
                    case 2:
                        typemessage = "Tapus - Make sure you are in front of the statue at the ruins.";
                        Program.gCmdWindow.SetRadioOpponent();
                        Program.gCmdWindow.labelreset.Visible = true;
                        Program.gCmdWindow.resetNoBox.Visible = true;
                        break;
                    case 3:
                        typemessage = "Solgaleo/Lunala - Make sure you are in front of Solgaleo/Lunala at the Altar of the Sunne/Moone.";
                        Program.gCmdWindow.SetRadioOpponent();
                        Program.gCmdWindow.labelreset.Visible = true;
                        Program.gCmdWindow.resetNoBox.Visible = true;
                        break;
                    case 4:
                        typemessage = "Wild Pokémon - Make sure you are in the place where wild pokémon can appear. Also, check that Honey is the item at the top of your Item list and can be selected by just opening the menu and pressing A.";
                        Program.gCmdWindow.SetRadioOpponent();
                        Program.gCmdWindow.Text = "Total Encounters:";
                        Program.gCmdWindow.labelreset.Visible = true;
                        Program.gCmdWindow.resetNoBox.Visible = true;
                        break;
                    case 5:
                        typemessage = "Ultra Beast/Necrozma - Make sure you are in the place where the Ultra Beast / Necrozma appears. Also, check that Honey is the item at the top of your Item list and can be selected by just opening the menu and pressing A.";
                        Program.gCmdWindow.SetRadioOpponent();
                        Program.gCmdWindow.Text = "Total Encounters:";
                        Program.gCmdWindow.labelreset.Visible = true;
                        Program.gCmdWindow.resetNoBox.Visible = true;
                        break;
                    default:
                        typemessage = "No type - Select one type of soft-reset and try again.";
                        break;
                }
                if(Program.gCmdWindow.resetNoBox.Text != "")
                {
                    resetNo = Convert.ToInt32(Program.gCmdWindow.resetNoBox.Text);
                }
                else
                {
                    resetNo = 0;
                }
                DialogResult dialogResult = MessageBox.Show("This bot will trigger an encounter with a pokémon, and soft-reset if it doesn't match with the loaded filters.\r\n\r\nType: " + typemessage + "\r\n\r\nPlease read the wiki at GitHub before using this bot. Do you want to continue?", "Soft-reset bot", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.OK && Mode.SelectedIndex >= 0)
                {
                    // Configure GUI
                    Delg.SetText(RunStop, "Stop Bot");
                    // Initialize variables
                    botworking = true;
                    userstop = false;
                    botState = srbotStates.botstart;
                    attempts = 0;
                    maxreconnect = 10;
                    resetNo = 0;
                    isub = false;
                    honeynum = 0;
                    finishmessage = null;
                    // Run the bot
                    Program.gCmdWindow.botMode(true);
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
                while (botworking && Program.gCmdWindow.isConnected)
                {
                    switch (botState)
                    {
                        case srbotStates.botstart:
                            Report("Bot: START Gen 7 Soft-reset bot");
                            botState = srbotStates.selectmode;
                            break;

                        case srbotStates.selectmode:
                            switch (Mode.SelectedIndex)
                            {
                                case 0:
                                case 1:
                                    botState = srbotStates.startdialog;
                                    break;
                                case 2:
                                    botState = srbotStates.triggerbattle;
                                    break;
                                case 3:
                                    resetNo = 1;
                                    botState = srbotStates.soluna1;
                                    break;
                                case 4:
                                case 5:
                                    resetNo = 1;
                                    botState = srbotStates.writehoney;
                                    break;
                                default:
                                    botState = srbotStates.botexit;
                                    break;
                            }
                            break;

                        case srbotStates.startdialog:
                            Report("Bot: Start dialog");
                            waitTaskbool = Program.helper.waitbutton(LookupTable.keyA);
                            if (await waitTaskbool)
                            {
                                botState = srbotStates.testdialog1;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = srbotStates.startdialog;
                            }
                            break;

                        case srbotStates.testdialog1:
                            Report("Bot: Test if dialog has started");
                            waitTaskbool = Program.helper.memoryinrange(dialogOff, dialogIn, 0x10000000);
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
                                botState = srbotStates.continuedialog;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = srbotStates.startdialog;
                            }
                            break;

                        case srbotStates.continuedialog:
                            Report("Bot: Continue dialog");
                            waitTaskbool = Program.helper.waitbutton(LookupTable.keyA);
                            if (await waitTaskbool)
                            {
                                botState = srbotStates.testdialog2;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = srbotStates.continuedialog;
                            }
                            break;

                        case srbotStates.testdialog2:
                            Report("Bot: Test if dialog has finished");
                            waitTaskbool = Program.helper.memoryinrange(dialogOff, dialogOut, 0x10000000);
                            if (await waitTaskbool)
                            {
                                attempts = -10;
                                botState = srbotStates.readparty;
                            }
                            else if (Program.helper.lastRead >= 0x3F000000 && Program.helper.lastRead < 0x40000000)
                            {
                                attempts = -10;
                                botState = srbotStates.exitdialog;
                            }
                            else if (Program.helper.lastRead >= 0x3D000000 && Program.helper.lastRead < 0x3E000000)
                            {
                                attempts = 0;
                                botState = srbotStates.nickname;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = srbotStates.continuedialog;
                            }
                            break;

                        case srbotStates.exitdialog:
                            Report("Bot: Exit dialog");
                            waitTaskbool = Program.helper.waitbutton(LookupTable.keyB);
                            if (await waitTaskbool)
                            {
                                botState = srbotStates.readparty;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = srbotStates.exitdialog;
                            }
                            break;

                        case srbotStates.readparty:
                            Report("Bot: Try to read party");
                            waitTaskPKM = Program.helper.waitPartyRead(2);
                            srPoke = await waitTaskPKM;
                            if (srPoke == null)
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = srbotStates.exitdialog;
                            }
                            else if (srPoke.Species == 0)
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = srbotStates.exitdialog;
                            }
                            else
                            {
                                attempts = 0;
                                botState = srbotStates.filter;
                            }
                            break;

                        case srbotStates.filter:
                            filternum = CheckFilters(srPoke, filterList);
                            bool testsok = filternum > 0;
                            if (testsok)
                            {
                                botState = srbotStates.testspassed;
                            }
                            else if (Mode.SelectedIndex == 3 || Mode.SelectedIndex == 4 || Mode.SelectedIndex == 5)
                            {
                                botState = srbotStates.runbattle1;
                            }
                            else
                            {
                                botState = srbotStates.softreset;
                            }
                            break;

                        case srbotStates.testspassed:
                            Report("Bot: All tests passed!");
                            if (Mode.SelectedIndex == 3 || Mode.SelectedIndex == 4 || Mode.SelectedIndex == 5)
                            {
                                botresult  = ErrorMessage.BattleMatch;
                            }
                            else
                            {
                                botresult = ErrorMessage.SRMatch;
                            }
                            finishmessage = new int[] { filternum, resetNo };
                            botState = srbotStates.botexit;
                            break;

                        case srbotStates.softreset:
                            resetNo++;
                            Report("Bot: Sof-reset #" + resetNo.ToString());
                            Program.gCmdWindow.resetNoBox.Text = resetNo.ToString();
                            waitTaskbool = Program.helper.waitSoftReset();
                            if (await waitTaskbool)
                            {
                                botState = srbotStates.skiptitle;
                            }
                            else
                            {
                                botresult = ErrorMessage.ButtonError;
                                botState = srbotStates.botexit;
                            }
                            break;

                        case srbotStates.skiptitle:
                            await Task.Delay(7000);
                            Report("Bot: Open Menu");
                            waitTaskbool = Program.helper.waitbutton(LookupTable.keyA);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = srbotStates.reconnect;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = srbotStates.skiptitle;
                            }
                            break;

                        case srbotStates.reconnect:
                            Report("Bot: Try reconnect");
                            waitTaskbool = Program.gCmdWindow.Reconnect();
                            if (await waitTaskbool)
                            {
                                await Task.Delay(1000);
                                botState = srbotStates.connpatch;
                            }
                            else
                            {
                                botresult = ErrorMessage.GeneralError;
                                botState = srbotStates.botexit;
                            }
                            break;

                        case srbotStates.connpatch:
                            Report("Bot: Apply NFC patch");
                            waitTaskbool = Program.helper.waitNTRwrite(LookupTable.nfcOff, LookupTable.nfcVal, Program.gCmdWindow.pid);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = srbotStates.startgame;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.WriteError;
                                botState = srbotStates.connpatch;
                            }
                            break;

                        case srbotStates.startgame:
                            Report("Bot: Start the game");
                            await Task.Delay(1000);
                            waitTaskbool = Program.helper.waitbutton(LookupTable.keyA);
                            if (await waitTaskbool)
                            {
                                await Task.Delay(3000);
                                attempts = 0;
                                botState = srbotStates.selectmode;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = srbotStates.startgame;
                            }
                            break;

                        case srbotStates.nickname:
                            Report("Bot: Nickname screen");
                            waitTaskbool = Program.helper.waitbutton(LookupTable.keySTART);
                            await Task.Delay(250);
                            waitTaskbool = Program.helper.waitbutton(LookupTable.keyA);
                            if (await waitTaskbool)

                            {
                                botState = srbotStates.testdialog2;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = srbotStates.nickname;
                            }
                            break;

                        case srbotStates.triggerbattle:
                            Report("Bot: Try to trigger battle");
                            waitTaskbool = Program.helper.waitbutton(LookupTable.keyA);
                            if (await waitTaskbool)
                            {
                                botState = srbotStates.testdialog3;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = srbotStates.triggerbattle;
                            }
                            break;

                        case srbotStates.testdialog3:
                            Report("Bot: Test if dialog has started");
                            waitTaskbool = Program.helper.memoryinrange(dialogOff, dialogIn, 0x10000000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = srbotStates.continuedialog2;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = srbotStates.triggerbattle;
                            }
                            break;

                        case srbotStates.continuedialog2:
                            Report("Bot: Continue dialog");
                            waitTaskbool = Program.helper.waitbutton(LookupTable.keyA);
                            if (await waitTaskbool)
                            {
                                botState = srbotStates.readopp;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = srbotStates.continuedialog2;
                            }
                            break;

                        case srbotStates.readopp:
                            Report("Bot: Try to read opponent");
                            srPoke = null;
                            waitTaskPKM = Program.helper.waitPokeRead(opponentOff);
                            srPoke = await waitTaskPKM;
                            if (srPoke == null)
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = srbotStates.readopp;
                            }
                            else if (srPoke.Species == 0)
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = srbotStates.continuedialog2;
                            }
                            else
                            {
                                attempts = 0;
                                botState = srbotStates.filter;
                            }
                            break;

                        case srbotStates.soluna1:
                            Report("Bot: Walk to legendary pokemon");
                            waitTaskbool = Program.helper.waitsitck(0, 100);
                            if (await waitTaskbool)
                            {
                                botState = srbotStates.soluna2;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.StickError;
                                botState = srbotStates.soluna1;
                            }
                            break;

                        case srbotStates.soluna2:
                            Report("Bot: Trigger battle #" + resetNo);
                            waitTaskbool = Program.helper.waitbutton(LookupTable.keyA);
                            if (await waitTaskbool)
                            {
                                botState = srbotStates.soluna3;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = srbotStates.soluna2;
                            }
                            break;

                        case srbotStates.soluna3:
                            Report("Bot: Test if dialog has started");
                            waitTaskbool = Program.helper.memoryinrange(dialogOff, dialogIn, 0x10000000);
                            if (await waitTaskbool)
                            {
                                attempts = -0;
                                botState = srbotStates.soluna4;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = srbotStates.soluna2;
                            }
                            break;

                        case srbotStates.soluna4:
                            Report("Bot: Test if data is available");
                            waitTaskbool = Program.helper.timememoryinrange(battleOff, battleIn, 0x10000, 1000, 20000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = srbotStates.soluna5;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = srbotStates.soluna1;
                            }
                            break;

                        case srbotStates.soluna5:
                            Report("Bot: Try to read opponent");
                            srPoke = null;
                            waitTaskPKM = Program.helper.waitPokeRead(opponentOff);
                            srPoke = await waitTaskPKM;
                            if (srPoke == null)
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = srbotStates.soluna5;
                            }
                            else if (srPoke.Species == 0)
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = srbotStates.soluna2;
                            }
                            else
                            {
                                attempts = 0;
                                botState = srbotStates.filter;
                            }
                            break;

                        case srbotStates.runbattle1:
                            Report("Bot: Run from battle");
                            await Task.Delay(2000);
                            waitTaskbool = Program.helper.waitbutton(LookupTable.DpadDOWN);
                            if (await waitTaskbool)
                            {
                                botState = srbotStates.runbattle2;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = srbotStates.runbattle1;
                            }
                            break;

                        case srbotStates.runbattle2:
                            waitTaskbool = Program.helper.waitbutton(LookupTable.keyA);
                            if (await waitTaskbool)
                            {
                                botState = srbotStates.runbattle3;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = srbotStates.runbattle2;
                            }
                            break;

                        case srbotStates.runbattle3:
                            Report("Bot: Test out from battle");
                            waitTaskbool = Program.helper.timememoryinrange(battleOff, battleOut, 0x10000, 1000, 10000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                resetNo++;
                                if (Mode.SelectedIndex == 3)
                                {
                                    botState = srbotStates.soluna1;
                                    await Task.Delay(6000);
                                }
                                else if (isub)
                                {
                                    botState = srbotStates.dismissmsg;
                                    await Task.Delay(6000);
                                }
                                else
                                {
                                    botState = srbotStates.writehoney;
                                    await Task.Delay(5000);
                                }

                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = srbotStates.runbattle1;
                            }
                            break;

                        case srbotStates.writehoney:
                            if (honeynum >= 10)
                            {
                                botState = srbotStates.openmenu;
                            }
                            else
                            {
                                Report("Bot: Give 999 honey");
                                waitTaskbool = Program.helper.waitNTRwrite(itemOff, honey, Program.gCmdWindow.pid);
                                if (await waitTaskbool)
                                {
                                    attempts = 0;
                                    honeynum = 999;
                                    botState = srbotStates.openmenu;
                                }
                                else
                                {
                                    attempts++;
                                    botresult = ErrorMessage.WriteError;
                                    botState = srbotStates.writehoney;
                                }
                            }
                            break;

                        case srbotStates.openmenu:
                            Report("Bot: Open Menu");
                            waitTaskbool = Program.helper.waitbutton(LookupTable.keyX);
                            if (await waitTaskbool)
                            {
                                botState = srbotStates.testmenu;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = srbotStates.openmenu;
                            }
                            break;

                        case srbotStates.testmenu:
                            Report("Bot: Test if the menu is open");
                            waitTaskbool = Program.helper.timememoryinrange(menuOff, menuIn, 0x10000000, 1000, 5000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = srbotStates.openbag;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = srbotStates.openmenu;
                            }
                            break;

                        case srbotStates.openbag:
                            Report("Bot: Open Bag");
                            waitTaskbool = Program.helper.waitbutton(LookupTable.keyA);
                            if (await waitTaskbool)
                            {
                                botState = srbotStates.testbag;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = srbotStates.openbag;
                            }
                            break;

                        case srbotStates.testbag:
                            Report("Bot: Test if the bag is open");
                            waitTaskbool = Program.helper.timememoryinrange(bagOff, bagIn, 0x10000, 1000, 5000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = srbotStates.selecthoney;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = srbotStates.openbag;
                            }
                            break;

                        case srbotStates.selecthoney:
                            Report("Bot: Select Honey");
                            waitTaskbool = Program.helper.waitbutton(LookupTable.keyA);
                            if (await waitTaskbool)
                            {
                                botState = srbotStates.activatehoney;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = srbotStates.selecthoney;
                            }
                            break;

                        case srbotStates.activatehoney:
                            Report("Bot: Trigger battle #" + resetNo);
                            waitTaskbool = Program.helper.waitbutton(LookupTable.keyA);
                            if (await waitTaskbool)
                            {
                                botState = srbotStates.testwild;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = srbotStates.openbag;
                            }
                            break;

                        case srbotStates.testwild:
                            Report("Bot: Test if battle is triggered");
                            waitTaskbool = Program.helper.timememoryinrange(bagOff, bagOut, 0x10000, 1000, 10000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                honeynum--;
                                botState = srbotStates.waitwild;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = srbotStates.activatehoney;
                            }
                            break;

                        case srbotStates.waitwild:
                            Report("Bot: Test if data is available");
                            waitTaskbool = Program.helper.timememoryinrange(battleOff, battleIn, 0x10000, 1000, 20000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = srbotStates.readwild;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = srbotStates.activatehoney;
                            }
                            break;

                        case srbotStates.readwild:
                            Report("Bot: Try to read opponent");
                            srPoke = null;
                            waitTaskPKM = Program.helper.waitPokeRead(opponentOff);
                            srPoke = await waitTaskPKM;
                            if (srPoke == null)
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = srbotStates.readwild;
                            }
                            else if (srPoke.Species == 0)
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = srbotStates.readwild;
                            }
                            else if (srPoke.Species == WinFormsUtil.getIndex(Species))
                            {
                                attempts = 0;
                                isub = Mode.SelectedIndex == 5;
                                botState = srbotStates.filter;
                            }
                            else
                            {
                                attempts = 0;
                                isub = false;
                                botState = srbotStates.runbattle1;
                            }
                            break;

                        case srbotStates.dismissmsg:
                            Report("Bot: Dismiss message");
                            waitTaskbool = Program.helper.waitbutton(LookupTable.keyA);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = srbotStates.writehoney;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = srbotStates.dismissmsg;
                            }
                            break;

                        case srbotStates.botexit:
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
            else if (!Program.gCmdWindow.isConnected)
            {
                botresult = ErrorMessage.Disconnect;
            }
            ShowResult("Soft-reset bot", botresult, finishmessage);
            Delg.SetText(RunStop, "Start Bot");
            Program.gCmdWindow.botMode(false);
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
                string folderPath = @Application.StartupPath + "\\" + FOLDERBOT + "\\";
                (new FileInfo(folderPath)).Directory.Create();
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "PKMN-NTR Filter|*.pftr";
                openFileDialog1.Title = "Select a filter set";
                openFileDialog1.InitialDirectory = folderPath;
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

        private void Bot_SoftReset7_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (botworking)
            {
                MessageBox.Show("Stop the bot before closing this window", "Soft-reset bot", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }

        private void Bot_SoftReset7_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.gCmdWindow.Tool_Finish();
        }
    }
}
