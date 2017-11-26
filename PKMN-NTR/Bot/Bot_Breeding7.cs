using pkmn_ntr.Helpers;
using PKHeX.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static pkmn_ntr.Bot.Bot;

namespace pkmn_ntr.Bot
{
    /// <summary>
    /// Generation 7 Breeding bot.
    /// </summary>
    public partial class Bot_Breeding7 : Form
    {
        /// <summary>
        /// Secuency of steps done by the bot.
        /// </summary>
        public enum BotState { botstart, selectbox, readslot, eggseed, quickegg, triggerdialog, testdialog1, continuedialog, fixdialog, checknoegg, exitdialog, testdialog2, filter, testspassed, botexit };

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
        private int filternum;
        private int[] finishmessage = new int[] { -1, -1, -1 };
        private PKM breedPoke;
        private uint key;

        // Data offsets
        private uint eggOff = 0x3313EDD8;
        private uint dialogOff = 0x6749A4; // 1.0: 0x63DD68; 1.1: 0x67499C
        private uint dialogIn = 0x80000000; // 1.0: 0x09;
        private uint dialogOut = 0x00000000; // 1.0: 0x08;
        private uint currentboxOff = 0x330D982F;
        private uint eggseedOff = 0x3313EDDC;

        //private uint boxesOff = 0x10F1A0;
        //private uint boxesIN = 0x6F0000;
        //private uint boxesOUT = 0x520000;
        //private uint boxesviewOff = 0x672D04;
        //private uint boxesviewIN = 0x00000000;
        //private uint boxesviewOUT = 0x40000000;
        //private uint posXOff = 0x33199260;
        //private uint posYOff = 0x3319E2C4;
        //private uint posZOff = 0x330D6744;

        public Bot_Breeding7()
        {
            InitializeComponent();
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
                string modemessage;
                switch (Mode.SelectedIndex)
                {
                    case 0:
                        modemessage = "Simple: This bot will produce " + Eggs.Value + " eggs and deposit them in the pc, starting at the first available slot in box " + Box.Value + ".\r\n\r\n";
                        break;
                    case 1:
                        modemessage = "Filter: This bot will start producing egss and deposit them in the pc, starting at the first available slot in box " + Box.Value + ". Then it will check against the selected filters and if it finds a match the bot will stop. The bot will also stop if it produces " + Eggs.Value + " eggs before finding a match.\r\n\r\n";
                        break;
                    case 2:
                        modemessage = "ESV/TSV: This bot will start producing egss and deposit them in the pc, starting at the first available slot in box " + Box.Value + ". Then it will check the egg's ESV and if it finds a match with the values in the TSV list, the bot will stop. The bot will also stop if it produces " + Eggs.Value + " eggs before finding a match.\r\n\r\n";
                        break;
                    case 3:
                        modemessage = "Accept/Reject: This bot will talk to the Nursery Lady and accept " + Accept.Value + " eggs, then it will reject " + Reject.Value + " eggs and stop.\r\n\r\n";
                        break;
                    default:
                        modemessage = "No mode selected. Select one and try again.\r\n\r\n";
                        break;
                }
                DialogResult dialogResult;
                dialogResult = MessageBox.Show("This bot will start producing eggs from the Nursery using the following rules:\r\n\r\n" + modemessage + "Make sure that your party is full and the Party/Box option is set to Automatic. Please read the Wiki at Github before starting. Do you want to continue?", "Breeding bot", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
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
            Delg.SetEnabled(Breed_options, false);
            Delg.SetEnabled(TSVlistNum, false);
            Delg.SetEnabled(tsvAdd, false);
            Delg.SetEnabled(tsvRemove, false);
            Delg.SetEnabled(tsvLoad, false);
            Delg.SetEnabled(tsvSave, false);
            Delg.SetEnabled(filterLoad, false);
        }

        private void EnableControls()
        {
            Delg.SetEnabled(Breed_options, true);
            Delg.SetEnabled(TSVlistNum, true);
            Delg.SetEnabled(tsvAdd, true);
            Delg.SetEnabled(tsvRemove, true);
            Delg.SetEnabled(tsvLoad, true);
            Delg.SetEnabled(tsvSave, true);
            Delg.SetEnabled(filterLoad, true);
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
                        case (int)BotState.botstart:
                            Report("Bot: START Gen 7 Breding bot");
                            if (Mode.SelectedIndex >= 0 && Mode.SelectedIndex != 3 && Eggs.Value > 0)
                            {
                                Delg.SetValue(Slot, 1);
                                botState = BotState.selectbox;
                            }
                            else if (Mode.SelectedIndex == 3 && (Accept.Value > 0 || Reject.Value > 0))
                            {
                                botState = BotState.eggseed;
                            }
                            else
                            {
                                botresult = ErrorMessage.Finished;
                                botState = BotState.botexit;
                            }
                            break;

                        case BotState.selectbox:
                            Report("Bot: Set start box");
                            waitTaskbool = Program.helper.waitNTRwrite(currentboxOff, (uint)GetIndex(Box), Program.gCmdWindow.pid);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = BotState.readslot;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.WriteError;
                                botState = BotState.selectbox;
                            }
                            break;

                        case BotState.readslot:
                            Report("Bot: Search for empty slot");
                            waitTaskPKM = Program.helper.waitPokeRead(Box, Slot);
                            breedPoke = await waitTaskPKM;
                            if (breedPoke == null)
                            { // No data or invalid
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.readslot;
                            }
                            else if (breedPoke.Species == 0)
                            { // Empty space
                                Report("Bot: Empty slot");
                                attempts = 0;
                                botState = BotState.eggseed;
                            }
                            else
                            {
                                getNextSlot();
                                botState = BotState.readslot;
                            }
                            break;

                        case BotState.eggseed:
                            Report("Bot: Update Egg seed");
                            waitTaskbool = Program.helper.waitNTRmultiread(eggseedOff, 0x10);
                            if (await waitTaskbool)
                            {
                                Report("Bot: Current seed - " + UpdateSeed(Program.helper.lastmultiread));
                                attempts = 0;
                                if (Mode.SelectedIndex != 3)
                                {
                                    botState = BotState.quickegg;
                                }
                                else
                                {
                                    if (Accept.Value == 0 && Reject.Value == 0)
                                    {
                                        botresult = ErrorMessage.Finished;
                                        botState = BotState.botexit;
                                    }
                                    else
                                    {
                                        botState = BotState.quickegg;
                                    }
                                }
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.eggseed;
                            }
                            break;

                        case BotState.quickegg:
                            Report("Bot: Produce Egg in Nursery");
                            waitTaskbool = Program.helper.waitNTRwrite(eggOff, 0x01, Program.gCmdWindow.pid);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = BotState.triggerdialog;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.WriteError;
                                botState = BotState.quickegg;
                            }
                            break;

                        case BotState.triggerdialog:
                            Report("Bot: Start dialog");
                            waitTaskbool = Program.helper.waitbutton(LookupTable.ButtonA);
                            if (await waitTaskbool)
                            {
                                botState = BotState.testdialog1;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = BotState.triggerdialog;
                            }
                            break;

                        case BotState.testdialog1:
                            Report("Bot: Test if dialog has started");
                            waitTaskbool = Program.helper.memoryinrange(dialogOff, dialogIn, 0x10000000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = BotState.continuedialog;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.triggerdialog;
                            }
                            break;

                        case BotState.continuedialog:
                            Report("Bot: Continue dialog");
                            int maxi;
                            if (Mode.SelectedIndex == 3 && Accept.Value == 0)
                            {
                                key = LookupTable.ButtonB;
                                maxi = 9;
                            }
                            else
                            {
                                key = LookupTable.ButtonA;
                                maxi = 6;
                            }
                            int i;
                            for (i = 0; i < maxi; i++)
                            {
                                waitTaskbool = Program.helper.waitbutton(key);
                                if (!(await waitTaskbool))
                                {
                                    break;
                                }
                            }
                            if (i == 6)
                            {
                                botState = BotState.checknoegg;
                            }
                            if (i == 9)
                            {
                                botState = BotState.testdialog2;
                            }
                            else
                            {
                                botState = BotState.fixdialog;
                            }
                            break;

                        case BotState.fixdialog:
                            waitTaskbool = Program.helper.waitbutton(key);
                            if (await waitTaskbool)
                            {
                                botState = BotState.checknoegg;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = BotState.fixdialog;
                            }
                            break;

                        case BotState.checknoegg:
                            waitTaskbool = Program.helper.memoryinrange(eggOff, 0x00, 0x01);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                Report("Bot: Egg received");
                                botState = BotState.exitdialog;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.fixdialog;
                            }
                            break;

                        case BotState.exitdialog:
                            Report("Bot: Exit dialog");
                            await Task.Delay(1500);
                            waitTaskbool = Program.helper.waitbutton(LookupTable.ButtonB);
                            if (await waitTaskbool)
                            {
                                waitTaskbool = Program.helper.waitbutton(LookupTable.ButtonB);
                                if (await waitTaskbool)
                                {
                                    botState = BotState.testdialog2;
                                }
                                else
                                {
                                    attempts++;
                                    botresult = ErrorMessage.ButtonError;
                                    botState = BotState.exitdialog;
                                }
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = BotState.exitdialog;
                            }
                            break;

                        case BotState.testdialog2:
                            waitTaskbool = Program.helper.memoryinrange(dialogOff, dialogOut, 0x10000000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                Report("Bot: Dialog finished");
                                if (Mode.SelectedIndex != 3)
                                {
                                    botState = BotState.filter;
                                }
                                else
                                {
                                    if (Accept.Value > 0)
                                    {
                                        Delg.SetValue(Accept, Accept.Value - 1);
                                    }
                                    else
                                    {
                                        Delg.SetValue(Reject, Reject.Value - 1); ;
                                    }
                                    botState = BotState.eggseed;
                                }
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.exitdialog;
                            }
                            break;

                        case BotState.filter:
                            bool testsok = false;
                            Report("Bot: Read recevied egg");
                            waitTaskPKM = Program.helper.waitPokeRead(Box, Slot);
                            breedPoke = await waitTaskPKM;
                            if (breedPoke == null)
                            { // No data or invalid
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = BotState.filter;
                            }
                            else if (breedPoke.Species == 0)
                            { // Empty space
                                Report("Bot: Error detected - slot is empty");
                                attempts = 11;
                                botresult = ErrorMessage.GeneralError;
                                botState = BotState.botexit;
                            }
                            else
                            {
                                attempts = 0;
                                Delg.SetValue(Eggs, Eggs.Value - 1);
                                if (ReadESV.Checked || Mode.SelectedIndex == 2)
                                {
                                    Delg.DataGridViewAddRow(esvList, Box.Value, Slot.Value, breedPoke.PSV.ToString("D4"));
                                    if (Mode.SelectedIndex == 2)
                                    {
                                        testsok = ESV_TSV_check(breedPoke.PSV);
                                    }
                                }
                                if (Mode.SelectedIndex == 1)
                                {
                                    filternum = CheckFilters(breedPoke, filterList);
                                    testsok = filternum > 0;
                                }
                            }
                            if (testsok)
                            {
                                botState = BotState.testspassed;
                                break;
                            }
                            else if (Eggs.Value > 0)
                            {
                                getNextSlot();
                                botState = BotState.readslot;
                            }
                            else
                            {
                                if (Mode.SelectedIndex == 1 || Mode.SelectedIndex == 2)
                                {
                                    Report("Bot: No match found");
                                    botresult = ErrorMessage.NoMatch;
                                }
                                else
                                {
                                    botresult = ErrorMessage.Finished;
                                }
                                botState = BotState.botexit;
                            }
                            break;

                        case BotState.testspassed:
                            if (Mode.SelectedIndex == 1)
                            {
                                Report("Bot: All tests passed");
                                botresult = ErrorMessage.FilterMatch;
                                finishmessage[0] = (int)Box.Value;
                                finishmessage[1] = (int)Slot.Value;
                                finishmessage[2] = filternum;
                            }
                            else if (Mode.SelectedIndex == 2)
                            {
                                Report("Bot: ESV/TSV match found");
                                botresult = ErrorMessage.SVMatch;
                                finishmessage[0] = (int)Box.Value;
                                finishmessage[1] = (int)Slot.Value;
                                finishmessage[2] = breedPoke.PSV;
                            }
                            botState = BotState.botexit;
                            break;

                        case BotState.botexit:
                            Report("Bot: STOP Gen 7 Breding bot");
                            botworking = false;
                            break;

                        default:
                            Report("Bot: STOP Gen 7 Breding bot");
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
                            Report("Bot: STOP Gen 7 Breeding bot");
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
                Report("Bot: STOP Gen 7 Breeding bot");
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
            ShowResult("Breeding bot", botresult, finishmessage);
            Delg.SetText(RunStop, "Start Bot");
            Program.gCmdWindow.SetBotMode(false);
            EnableControls();
            Delg.SetEnabled(RunStop, true);
        }

        private void getNextSlot()
        {
            if (Slot.Value == 30)
            {
                Delg.SetValue(Box, Box.Value + 1);
                Delg.SetValue(Slot, 1);
            }
            else
            {
                Delg.SetValue(Slot, Slot.Value + 1);
            }
        }

        private string UpdateSeed(byte[] data)
        {
            string seed = BitConverter.ToString(data.Reverse().ToArray()).Replace("-", "");
            Delg.SetText(EggSeed, seed);
            return seed;
        }

        public bool ESV_TSV_check(int esv)
        {
            if (TSVlist.Items.Count > 0)
            {
                Report("Filter: Checking egg with ESV: " + esv);
                foreach (var tsv in TSVlist.Items)
                {
                    if (Convert.ToInt32(tsv) == esv)
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        private void Box_ValueChanged(object sender, EventArgs e)
        {
            Delg.SetMaximum(Eggs, LookupTable.GetRemainingSpaces((int)Box.Value, (int)Slot.Value));
        }

        private void esvSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (esvList.Rows.Count > 0)
                {
                    (new FileInfo(BotFolder)).Directory.Create();
                    string fileName = "ESVlist7.csv";
                    var esvlst = new StringBuilder();
                    var headers = esvList.Columns.Cast<DataGridViewColumn>();
                    esvlst.AppendLine(string.Join(",", headers.Select(column => column.HeaderText).ToArray()));
                    foreach (DataGridViewRow row in esvList.Rows)
                    {
                        var cells = row.Cells.Cast<DataGridViewCell>();
                        esvlst.AppendLine(string.Join(",", cells.Select(cell => cell.Value).ToArray()));
                    }
                    File.WriteAllText(BotFolder + fileName, esvlst.ToString());
                    MessageBox.Show("ESV list saved");
                }
                else
                {
                    MessageBox.Show("There are no eggs on the ESV list");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsvAdd_Click(object sender, EventArgs e)
        {
            TSVlist.Items.Add(((int)TSVlistNum.Value).ToString("D4"));
        }

        private void tsvRemove_Click(object sender, EventArgs e)
        {
            if (TSVlist.SelectedIndices.Count > 0)
            {
                TSVlist.Items.RemoveAt(TSVlist.SelectedIndices[0]);
            }
            else
            {
                MessageBox.Show("No TSV selected for remove");
            }
        }

        private void tsvSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (TSVlist.Items.Count > 0)
                {
                    (new FileInfo(BotFolder)).Directory.Create();
                    string fileName = "TSVlist7.csv";
                    var tsvlst = new StringBuilder();
                    foreach (var value in TSVlist.Items)
                    {
                        tsvlst.AppendLine(value.ToString());
                    }
                    File.WriteAllText(BotFolder + fileName, tsvlst.ToString());
                    MessageBox.Show("TSV list saved");
                }
                else
                {
                    MessageBox.Show("There are no numbers on the TSV list");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsvLoad_Click(object sender, EventArgs e)
        {
            try
            {
                (new FileInfo(BotFolder)).Directory.Create();
                string fileName = "TSVlist6.csv";
                if (File.Exists(BotFolder + fileName))
                {
                    string[] values = File.ReadAllLines(BotFolder + fileName);
                    TSVlist.Items.Clear();
                    TSVlist.Items.AddRange(values);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clearAll_Click(object sender, EventArgs e)
        {
            Delg.SetSelectedIndex(Mode, -1);
            Delg.SetValue(Box, 1);
            Delg.SetValue(Slot, 1);
            Delg.SetValue(Eggs, 1);
            Delg.SetValue(Accept, 0);
            Delg.SetValue(Reject, 0);
            Delg.SetChecked(ReadESV, false);
            esvList.Rows.Clear();
            TSVlist.Items.Clear();
            filterList.Rows.Clear();
        }

        private void filterLoad_Click(object sender, EventArgs e)
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

        private void Bot_Breeding7_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (botworking)
            {
                MessageBox.Show("Stop the bot before closing this window", "Breeding bot", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }

        private void Bot_Breeding7_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.gCmdWindow.DumpEggSeed();
            Program.gCmdWindow.Tool_Finish();
        }
    }
}
