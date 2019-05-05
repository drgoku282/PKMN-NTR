using pkmn_ntr.Helpers;
using PKHeX.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using static pkmn_ntr.Bot.Bot;

namespace pkmn_ntr.Bot
{
    /// <summary>
    /// Generation 6 Wonder Trade bot.
    /// </summary>
    public partial class Bot_WonderTrade6 : Form
    {
        /// <summary>
        /// Secuency of steps done by the bot.
        /// </summary>
        private enum BotState { botstart, backup, testpssmenu, readpoke, readfolder, writefromfolder, pressWTbutton, testsavescrn, confirmsave, testwtscrn, confirmwt, testboxes, gotoboxchange, touchboxview, testboxview, touchnewbox, selectnewbox, testboxviewout, touchpoke, selectrade, confirmsend, testboxesout, waitfortrade, testbackpssmenu, notradepartner, dumpafter, actionafter, restorebackup, delete, endbot };

        // General bot variables
        private bool botworking;
        private bool userstop;
        private BotState botstate;
        private ErrorMessage botresult;
        private int attempts;
        private int maxreconnect;
        private Task<bool> waitTaskbool;
        private Task<PKM> waitTaskPKM;

        // Class bot variables
        private bool boxchange;
        private decimal startbox;
        private decimal startslot;
        private decimal starttrades;
        private int currentfile;
        private int tradewait;
        private List<PKM> pklist;
        private PKM WTpoke;
        private Random RNG;
        private string backuppath;
        private string[] pkfiles;
        private ushort currentCHK;

        // Class constants
        private readonly int commandtime = 250;
        private readonly int delaytime = 250;
        private string wtfolderpath = @Application.StartupPath + "\\Wonder Trade\\";

        // Data offsets
        private uint psssmenu1Off;
        private uint psssmenu1IN;
        //private uint psssmenu1OUT;
        private uint savescrnOff;
        private uint savescrnIN;
        //private uint savescrnOUT;
        private uint wtconfirmationOff;
        private uint wtconfirmationIN;
        //private uint wtconfirmationOUT;
        private uint wtboxesOff;
        private uint wtboxesIN;
        private uint wtboxesOUT;
        private uint wtboxviewOff;
        private uint wtboxviewIN;
        private uint wtboxviewOUT;
        private uint wtboxviewRange;
        private uint pcpkmOff;

        public Bot_WonderTrade6()
        {
            InitializeComponent();
            RNG = new Random();
        }

        private void Bot_WonderTrade6_Load(object sender, EventArgs e)
        {
            if (Program.gCmdWindow.IsXY)
            { // XY
                psssmenu1Off = 0x19ABC0;
                psssmenu1IN = 0x7E0000;
                //psssmenu1OUT = 0x4D0000;
                savescrnOff = 0x19AB78;
                savescrnIN = 0x7E0000;
                //savescrnOUT = 0x4D0000;
                wtconfirmationOff = 0x19A918;
                wtconfirmationIN = 0x4D0000;
                //wtconfirmationOUT = 0x780000;
                wtboxesOff = 0x19A988;
                wtboxesIN = 0x6C0000;
                wtboxesOUT = 0x4D0000;
                wtboxviewOff = 0x627437;
                wtboxviewIN = 0x00000000;
                wtboxviewOUT = 0x20000000;
                wtboxviewRange = 0x1000000;
                pcpkmOff = 0x8C861C8;
            }
            else if (Program.gCmdWindow.IsORAS)
            { // ORAS
                psssmenu1Off = 0x19C21C;
                psssmenu1IN = 0x830000;
                //psssmenu1OUT = 0x500000;
                savescrnOff = 0x19C1CC;
                savescrnIN = 0x830000;
                //savescrnOUT = 0x500000;
                wtconfirmationOff = 0x19C024;
                wtconfirmationIN = 0x500000;
                //wtconfirmationOUT = 0x700000;
                wtboxesOff = 0x19BFCC;
                wtboxesIN = 0x710000;
                wtboxesOUT = 0x500000;
                wtboxviewOff = 0x66F5F2;
                wtboxviewIN = 0xC000;
                wtboxviewOUT = 0x4000;
                wtboxviewRange = 0x1000;
                pcpkmOff = 0x8C9E134;
            }
        }

        private void RunStop_Click(object sender, EventArgs e)
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
            { // Run bot
                DialogResult dialogResult = MessageBox.Show("This scirpt will try to Wonder Trade " + Trades.Value + " pokémon, starting from the slot " + Slot.Value + " of box " + Box.Value + ". Remember to read the wiki for this bot in GitHub before starting.\r\n\r\nDo you want to continue?", "Wonder Trade Bot", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes && Trades.Value > 0)
                {
                    if (Program.gCmdWindow.HaX)
                    {
                        MessageBox.Show("Illegal mode enabled. If using a WT folder mode, it will write any pokémon to the game, regardless of legality. It will also attempt to Wonder Trade illegal pokémon it finds.", "Illegal mode", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    // Configure GUI
                    Delg.SetText(RunStop, "Stop Bot");
                    // Initialize variables
                    botworking = true;
                    userstop = false;
                    botstate = BotState.botstart;
                    attempts = 0;
                    maxreconnect = 10;
                    boxchange = true;
                    startbox = Box.Value;
                    startslot = Slot.Value;
                    starttrades = Trades.Value;
                    currentfile = 0;
                    tradewait = 0;
                    pklist = new List<PKM> { };
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
            Delg.SetEnabled(Box, false);
            Delg.SetEnabled(Slot, false);
            Delg.SetEnabled(Trades, false);
            Delg.SetEnabled(WTSource, false);
            Delg.SetEnabled(WTAfter, false);
            Delg.SetEnabled(runEndless, false);
        }

        private void EnableControls()
        {
            Delg.SetEnabled(Box, true);
            Delg.SetEnabled(Slot, true);
            Delg.SetEnabled(Trades, true);
            Delg.SetEnabled(WTSource, true);
            Delg.SetEnabled(WTAfter, true);
            Delg.SetEnabled(runEndless, true);
        }

        public async void RunBot()
        {
            try
            {
                Program.gCmdWindow.SetBotMode(true);
                while (botworking && Program.gCmdWindow.IsConnected)
                {
                    switch (botstate)
                    {
                        case BotState.botstart:
                            Report("Bot: START Gen 6 Wonder Trade bot");
                            botstate = BotState.backup;
                            break;

                        case BotState.backup:
                            Report("Bot: Backup boxes");
                            waitTaskbool = Program.helper.waitNTRmultiread(pcpkmOff, 232 * 30 * 31);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                string fileName = "WTBefore-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".ek6";
                                backuppath = wtfolderpath + fileName;
                                Program.gCmdWindow.WriteDataToFile(Program.helper.lastmultiread, backuppath);
                                botstate = BotState.testpssmenu;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botstate = BotState.backup;
                            }
                            break;

                        case BotState.testpssmenu:
                            Report("Bot: Test if the PSS menu is shown");
                            waitTaskbool = Program.helper.memoryinrange(psssmenu1Off, psssmenu1IN, 0x10000);
                            if (await waitTaskbool)
                            {
                                if (sourceBox.Checked)
                                {
                                    botstate = BotState.readpoke;
                                }
                                else
                                {
                                    botstate = BotState.readfolder;
                                }
                            }
                            else
                            {
                                botresult = ErrorMessage.NotInPSS;
                                botstate = BotState.endbot;
                            }
                            break;

                        case BotState.readpoke:
                            Report("Bot: Look for pokemon to trade");
                            waitTaskPKM = Program.helper.waitPokeRead(Box, Slot);
                            WTpoke = await waitTaskPKM;
                            if (WTpoke == null)
                            { // No data or invalid
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botstate = BotState.readpoke;
                            }
                            else if (WTpoke.Species == 0)
                            { // Empty space
                                Report("Bot: Empty slot");
                                attempts = 0;
                                getNextSlot();
                            }
                            else
                            { // Valid pkm, check legality
                                attempts = 0;
                                if (IsTradeable(WTpoke))
                                {
                                    currentCHK = WTpoke.Checksum;
                                    Report("Bot: Pokémon found - 0x" + currentCHK.ToString("X4"));
                                    botstate = BotState.pressWTbutton;
                                }
                                else
                                {
                                    if (Program.gCmdWindow.HaX)
                                    {
                                        Report("Bot: Pokémon cannot be traded, is an egg or have special ribbons.");
                                    }
                                    else
                                    {
                                        Report("Bot: Pokémon cannot be traded, is illegal or is an egg or have special ribbons.");
                                    }
                                    getNextSlot();
                                }
                            }
                            break;

                        case BotState.readfolder:
                            Report("Bot: Reading Wonder Trade folder");
                            pkfiles = Directory.GetFiles(wtfolderpath, "*.pk6");
                            if (pkfiles.Length > 0)
                            {
                                foreach (string pkf in pkfiles)
                                {
                                    byte[] temp = File.ReadAllBytes(pkf);
                                    if (temp.Length == 232)
                                    {
                                        PK6 pkmn = new PK6(temp);
                                        if (IsTradeable(pkmn))
                                        { // Legal pkm
                                            Report("Bot: Valid PK6 file");
                                            pklist.Add(pkmn);
                                        }
                                        else
                                        { // Illegal pkm
                                            Report("Bot: File " + pkf + " cannot be traded");
                                        }
                                    }
                                    else
                                    { // Not valid file
                                        Report("Bot: File " + pkf + " is not a valid pk6 file");
                                    }
                                }
                            }
                            if (pklist.Count > 0)
                            {
                                botstate = BotState.writefromfolder;
                            }
                            else
                            {
                                Report("Bot: No files detected");
                                botresult = ErrorMessage.Finished;
                                botstate = BotState.endbot;
                            }
                            break;

                        case BotState.writefromfolder:
                            Report("Bot: Write pkm file from list");
                            if (sourceRandom.Checked)
                            { // Select a random file
                                currentfile = RNG.Next() % pklist.Count;
                            }
                            waitTaskbool = Program.helper.waitNTRwrite(GetBoxOffset(pcpkmOff, Box, Slot), pklist[currentfile].EncryptedBoxData, Program.gCmdWindow.pid);
                            if (await waitTaskbool)
                            {
                                Program.gCmdWindow.UpdateDumpBoxes(Box, Slot);
                                Program.gCmdWindow.Pokemon = pklist[currentfile];
                                currentCHK = pklist[currentfile].Checksum;
                                if (sourceFolder.Checked)
                                {
                                    currentfile++;
                                    if (currentfile > pklist.Count - 1)
                                    {
                                        currentfile = 0;
                                    }
                                }
                                attempts = 0;
                                botstate = BotState.pressWTbutton;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.WriteError;
                                botstate = BotState.writefromfolder;
                            }
                            break;

                        case BotState.pressWTbutton:
                            Report("Bot: Touch Wonder Trade button");
                            waitTaskbool = Program.helper.waittouch(240, 120);
                            if (await waitTaskbool)
                            {
                                botstate = BotState.testsavescrn;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.TouchError;
                                botstate = BotState.pressWTbutton;
                            }
                            break;

                        case BotState.testsavescrn:
                            Report("Bot: Test if the save screen is shown");
                            await Task.Delay(delaytime);
                            waitTaskbool = Program.helper.timememoryinrange(savescrnOff, savescrnIN, 0x10000, 500, 5000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botstate = BotState.confirmsave;
                            }
                            else
                            { // If not in save screen, try again
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botstate = BotState.pressWTbutton;
                            }
                            break;

                        case BotState.confirmsave:
                            Report("Bot: Press Yes");
                            await Task.Delay(delaytime);
                            waitTaskbool = Program.helper.waitbutton(LookupTable.ButtonA);
                            if (await waitTaskbool)
                            {
                                botstate = BotState.testwtscrn;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botstate = BotState.confirmsave;
                            }
                            break;

                        case BotState.testwtscrn:
                            Report("Bot: Test if Wonder Trade screen is shown");
                            waitTaskbool = Program.helper.timememoryinrange(wtconfirmationOff, wtconfirmationIN, 0x10000, 500, 5000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botstate = BotState.confirmwt;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botstate = BotState.confirmsave;
                            }
                            break;

                        case BotState.confirmwt:
                            Report("Bot: Touch Yes");
                            await Task.Delay(delaytime);
                            waitTaskbool = Program.helper.waittouch(160, 100);
                            if (await waitTaskbool)
                            {
                                botstate = BotState.testboxes;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.TouchError;
                                botstate = BotState.confirmwt;
                            }
                            break;

                        case BotState.testboxes:
                            Report("Bot: Test if the boxes are shown");
                            waitTaskbool = Program.helper.timememoryinrange(wtboxesOff, wtboxesIN, 0x10000, 500, 5000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botstate = BotState.gotoboxchange;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botstate = BotState.confirmwt;
                            }
                            break;

                        case BotState.gotoboxchange:
                            await Task.Delay(8 * delaytime);
                            if (boxchange)
                            {
                                botstate = BotState.touchboxview;
                                boxchange = false;
                            }
                            else
                            {
                                botstate = BotState.touchpoke;
                            }
                            break;

                        case BotState.touchboxview:
                            Report("Bot: Touch Box View");
                            waitTaskbool = Program.helper.waittouch(30, 220);
                            if (await waitTaskbool)
                                botstate = BotState.testboxview;
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.TouchError;
                                botstate = BotState.touchboxview;
                            }
                            break;

                        case BotState.testboxview:
                            Report("Bot: Test if box view is shown");
                            waitTaskbool = Program.helper.timememoryinrange(wtboxviewOff, wtboxviewIN, wtboxviewRange, 500, 5000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botstate = BotState.touchnewbox;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botstate = BotState.touchboxview;
                            }
                            break;

                        case BotState.touchnewbox:
                            Report("Bot: Touch New Box");
                            await Task.Delay(delaytime);
                            waitTaskbool = Program.helper.waittouch(LookupTable.boxposX6[GetIndex(Box)], LookupTable.boxposY6[GetIndex(Box)]);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botstate = BotState.selectnewbox;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.TouchError;
                                botstate = BotState.touchboxview;
                            }
                            break;

                        case BotState.selectnewbox:
                            Report("Bot: Select New Box");
                            await Task.Delay(delaytime);
                            waitTaskbool = Program.helper.waitbutton(LookupTable.ButtonA);
                            if (await waitTaskbool)
                            {
                                botstate = BotState.testboxviewout;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botstate = BotState.confirmsave;
                            }
                            break;

                        case BotState.testboxviewout:
                            Report("Bot: Test if box view is not shown");
                            waitTaskbool = Program.helper.timememoryinrange(wtboxviewOff, wtboxviewOUT, wtboxviewRange, 500, 5000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botstate = BotState.touchpoke;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botstate = BotState.touchnewbox;
                            }
                            break;

                        case BotState.touchpoke:
                            Report("Bot: Touch Pokémon");
                            await Task.Delay(delaytime);
                            waitTaskbool = Program.helper.waittouch(LookupTable.pokeposX6[GetIndex(Slot)], LookupTable.pokeposY6[GetIndex(Slot)]);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botstate = BotState.selectrade;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botstate = BotState.touchpoke;
                            }
                            break;

                        case BotState.selectrade:
                            Report("Bot: Select Trade");
                            await Task.Delay(2 * delaytime);
                            waitTaskbool = Program.helper.waitbutton(LookupTable.ButtonA);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botstate = BotState.confirmsend;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botstate = BotState.selectrade;
                            }
                            break;

                        case BotState.confirmsend:
                            Report("Bot: Select Yes");
                            await Task.Delay(2 * delaytime);
                            waitTaskbool = Program.helper.waitbutton(LookupTable.ButtonA);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botstate = BotState.testboxesout;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botstate = BotState.confirmsend;
                            }
                            break;

                        case BotState.testboxesout:
                            Report("Bot: Test if the boxes are not shown");
                            waitTaskbool = Program.helper.timememoryinrange(wtboxesOff, wtboxesOUT, 0x10000, 500, 5000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                tradewait = 0;
                                botstate = BotState.waitfortrade;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botstate = BotState.touchpoke;
                            }
                            break;

                        case BotState.waitfortrade:
                            Report("Bot: Wait for trade");
                            ushort newCHK;
                            waitTaskPKM = Program.helper.waitPokeRead(Box, Slot);
                            WTpoke = await waitTaskPKM;
                            if (WTpoke == null)
                            {
                                newCHK = (ushort)(currentCHK + 1);
                            }
                            else if (WTpoke.Species == 0)
                            {
                                newCHK = (ushort)(currentCHK + 1);
                            }
                            else
                            {
                                newCHK = WTpoke.Checksum;
                            }
                            if (newCHK == currentCHK)
                            {
                                await Task.Delay(8 * delaytime);
                                tradewait++;
                                if (tradewait > 30) // Too much time passed
                                {
                                    attempts = 0;
                                    botstate = BotState.notradepartner;
                                }
                            }
                            else
                            {
                                Report("Bot: Wait 30 seconds");
                                await Task.Delay(30000);
                                botstate = BotState.testbackpssmenu;
                            }
                            break;

                        case BotState.testbackpssmenu:
                            Report("Bot: Test if back to the PSS menu");
                            waitTaskbool = Program.helper.timememoryinrange(psssmenu1Off, psssmenu1IN, 0x10000, 2000, 10000);
                            if (await waitTaskbool)
                            { // Trade sucessfull
                                attempts = 0;
                                getNextSlot();
                            }
                            else
                            { // Still waiting
                                attempts++;
                                botresult = ErrorMessage.GeneralError;
                                botstate = BotState.testbackpssmenu;
                            }
                            break;

                        case BotState.notradepartner:
                            Report("Bot: Test if back to the PSS menu");
                            waitTaskbool = Program.helper.timememoryinrange(psssmenu1Off, psssmenu1IN, 0x10000, 500, 3000);
                            if (await waitTaskbool)
                            { // Back in menu
                                attempts = 0;
                                botstate = BotState.pressWTbutton;
                            }
                            else
                            { // Still waiting
                                attempts++;
                                botresult = ErrorMessage.GeneralError;
                                Report("Bot: Select Yes");
                                Program.helper.quickbuton(LookupTable.ButtonA, commandtime);
                                await Task.Delay(commandtime + delaytime);
                                botstate = BotState.notradepartner;
                            }
                            break;

                        case BotState.dumpafter:
                            if (afterDump.Checked)
                            {
                                Report("Bot: Dump boxes");
                                waitTaskbool = Program.helper.waitNTRmultiread(pcpkmOff, 232 * 30 * 31);
                                if (await waitTaskbool)
                                {
                                    attempts = 0;
                                    string fileName = "WTAfter-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".ek6";
                                    Program.gCmdWindow.WriteDataToFile(Program.helper.lastmultiread, wtfolderpath + fileName);
                                    botstate = BotState.actionafter;
                                }
                                else
                                {
                                    attempts++;
                                    botresult = ErrorMessage.ReadError;
                                    botstate = BotState.dumpafter;
                                }
                            }
                            else
                            {
                                botstate = BotState.actionafter;
                            }
                            break;

                        case BotState.actionafter:
                            if (afterRestore.Checked)
                            {
                                botstate = BotState.restorebackup;
                            }
                            else if (afterDelete.Checked)
                            {
                                botstate = BotState.delete;
                            }
                            else
                            {
                                botresult = ErrorMessage.Finished;
                                botstate = BotState.endbot;
                            }
                            break;

                        case BotState.restorebackup:
                            Report("Bot: Restore boxes backup");
                            byte[] restore = File.ReadAllBytes(backuppath);
                            if (restore.Length == 232 * 30 * 31)
                            {
                                waitTaskbool = Program.helper.waitNTRwrite(pcpkmOff, restore, Program.gCmdWindow.pid);
                                if (await waitTaskbool)
                                {
                                    attempts = 0;
                                    botresult = ErrorMessage.Finished;
                                    botstate = BotState.endbot;
                                }
                                else
                                {
                                    attempts++;
                                    botresult = ErrorMessage.WriteError;
                                    botstate = BotState.restorebackup;
                                }
                            }
                            else
                            {
                                Report("Bot: Invalid boxes file");
                                botresult = ErrorMessage.GeneralError;
                                botstate = BotState.endbot;
                            }
                            break;

                        case BotState.delete:
                            Report("Bot: Delete traded pokémon");
                            byte[] deletearray = new byte[232 * (int)starttrades];
                            for (int i = 0; i < starttrades; i++)
                            {
                                Program.gCmdWindow.SAV.BlankPKM.EncryptedBoxData.CopyTo(deletearray, i * 232);
                            }
                            waitTaskbool = Program.helper.waitNTRwrite(GetBoxOffset(pcpkmOff, Box, Slot), deletearray, Program.gCmdWindow.pid);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botstate = BotState.endbot;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.WriteError;
                                botstate = BotState.delete;
                            }
                            break;

                        case BotState.endbot:
                            Report("Bot: STOP Gen 6 Wonder Trade bot");
                            botworking = false;
                            break;

                        default:
                            Report("Bot: STOP Gen 6 Wonder Trade bot");
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
                                await Task.Delay(10 * delaytime);
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
                            Report("Bot: STOP Gen 6 Wonder Trade bot");
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
                Report("Bot: STOP Gen 6 Wonder Trade bot");
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
            ShowResult("Wonder Trade bot", botresult);
            Delg.SetText(RunStop, "Start Bot");
            Program.gCmdWindow.SetBotMode(false);
            EnableControls();
            Delg.SetEnabled(RunStop, true);
        }

        private void Box_ValueChanged(object sender, EventArgs e)
        {
            Delg.SetMaximum(Trades, LookupTable.GetRemainingSpaces((int)Box.Value, (int)Slot.Value));
        }

        private void getNextSlot()
        {
            if (Slot.Value == 30)
            {
                Delg.SetValue(Box, Box.Value + 1);
                Delg.SetValue(Slot, 1);
                boxchange = true;
            }
            else
            {
                Delg.SetValue(Slot, Slot.Value + 1);
            }
            Delg.SetValue(Trades, Trades.Value - 1);
            if (Trades.Value > 0)
            {
                if (sourceBox.Checked)
                {
                    botstate = BotState.readpoke;
                }
                else
                {
                    botstate = BotState.writefromfolder;
                }
                attempts = 0;
            }
            else if (runEndless.Checked)
            {
                Delg.SetValue(Box, startbox);
                Delg.SetValue(Slot, startslot);
                Delg.SetValue(Trades, starttrades);
                if (sourceBox.Checked)
                {
                    botstate = BotState.readpoke;
                }
                else
                {
                    botstate = BotState.writefromfolder;
                }
                attempts = 0;
            }
            else
            {
                botstate = BotState.dumpafter;
            }
        }

        private void Bot_WonderTrade6_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (botworking)
            {
                MessageBox.Show("Stop the bot before closing this window", "Wonder Trade bot", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }

        private void Bot_WonderTrade6_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.gCmdWindow.Tool_Finish();
        }
    }
}
