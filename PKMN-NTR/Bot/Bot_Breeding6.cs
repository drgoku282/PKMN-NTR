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
    public partial class Bot_Breeding6 : Form
    {
        public enum breedbotstates { botstart, facedaycareman, quickegg, walk1, walk2, checkegg, walk3, checkmap1, triggerdialog, continuedialog, checknoegg, exitdialog, testparty, walktodaycare, checkmap2, fix1, entertodaycare, checkmap3, walktodesk, checkmap4, walktocomputer, checkmap5, fix2, facecomputer, startcomputer, testcomputer, computerdialog, pressPCstorage, touchOrganize, testboxes, readslot, testboxchange, touchboxview, testboxview, touchnewbox, selectnewbox, testviewout, touchegg, moveegg, releaseegg, exitcomputer, testexit, readegg, retirefromcomputer, checkmap6, fix3, retirefromdesk, checkmap7, retirefromdoor, checkmap8, fix5, walktodaycareman, checkmap9, fix4, filter, testspassed, botexit };

        // General bot variables
        private bool botworking;
        private bool userstop;
        private breedbotstates botState;
        private ErrorMessage botresult;
        private int attempts;
        private int maxreconnect;
        private Task<bool> waitTaskbool;
        private Task<PKM> waitTaskPKM;

        // Class variables
        private bool boxchange;
        private bool ORAS;
        private decimal[,] egglocations = new decimal[5, 2];
        private int runtime;
        private int eggsinparty;
        private int eggsinbatch;
        private int filternum;
        private int[] finishmessage = new int[] { -1, -1, -1 };
        private PKM breedPoke;
        private uint lastposition;

        // Class constants
        private readonly int walktime = 250;
        private readonly int commanddelay = 500;
        private readonly int longcommandtime = 1250;

        // Data offsets
        private uint wtboxesOff;
        private uint organizeBoxIN;
        private uint organizeBoxOUT;
        private uint wtboxviewOff;
        private uint wtboxviewIN;
        private uint wtboxviewOUT;
        private uint wtboxviewRange;
        private uint computerOff;
        private uint computerIN;
        //private uint computerOUT;
        private uint mapidoff;
        private uint mapxoff;
        private uint mapyoff;
        //private uint mapzoff;
        private uint routemapid;
        private uint daycaremapid;
        private uint daycaremanx;
        private uint daycaremany;
        private uint daycaredoorx;
        //uint daycaredoory;
        private uint daycareexitx;
        //uint daycareexity;
        private uint computerx;
        private uint computery;
        private uint eggoff;

        public Bot_Breeding6()
        {
            InitializeComponent();
        }

        private void Bot_Breeding6_Load(object sender, System.EventArgs e)
        {
            if (Program.gCmdWindow.SAV.Version == GameVersion.X || Program.gCmdWindow.SAV.Version == GameVersion.Y)
            { // XY
                ORAS = false;
                Delg.SetEnabled(orgbox_pos, false);
                Delg.SetEnabled(daycare_select, false);
                computerOff = 0x19A918;
                computerIN = 0x4D0000;
                wtboxesOff = 0x19A988;
                organizeBoxIN = 0x6C0000;
                organizeBoxOUT = 0x4D0000;
                wtboxviewOff = 0x627437;
                wtboxviewIN = 0x00000000;
                wtboxviewOUT = 0x20000000;
                wtboxviewRange = 0x1000000;
                mapidoff = 0x81828EC;
                mapxoff = 0x818290C;
                mapyoff = 0x8182914;
                //mapzoff = 0x8182910;
                routemapid = 0x108;
                daycaremapid = 0x109;
                daycaremanx = 0x46219400;
                daycaremany = 0x460F9400;
                daycaredoorx = 0x4622FC00;
                //daycaredoory = 0x460F4C00;
                daycareexitx = 0x43610000;
                //daycareexity = 0x43AF8000;
                computerx = 0x43828000;
                computery = 0x43730000;
                eggoff = 0x8C80124;
                runtime = 1000;
            }
            else if (Program.gCmdWindow.SAV.Version == GameVersion.OR || Program.gCmdWindow.SAV.Version == GameVersion.AS)
            { // ORAS
                ORAS = true;
            }
        }

        private void ORASdaycare(bool route117daycare)
        {
            if (route117daycare)
            { // Route 117 daycare
                computerOff = 0x19BF5C;
                computerIN = 0x500000;
                wtboxesOff = 0x19BFCC;
                organizeBoxIN = 0x710000;
                organizeBoxOUT = 0x500000;
                wtboxviewOff = 0x66F5F2;
                wtboxviewIN = 0xC000;
                wtboxviewOUT = 0x4000;
                wtboxviewRange = 0x1000;
                mapidoff = 0x8187BD4;
                mapxoff = 0x8187BF4;
                mapyoff = 0x8187BFC;
                //mapzoff = 0x8187BF8;
                routemapid = 0x2C;
                daycaremapid = 0x187;
                daycaremanx = 0x45553000;
                daycaremany = 0x44D92000;
                daycaredoorx = 0x455AD000;
                //daycaredoory = 0x44D6E000;
                daycareexitx = 0x43610000;
                //daycareexity = 0x43A68000;
                computerx = 0x43828000;
                computery = 0x43730000;
                eggoff = 0x8C88358;
                runtime = 1000;
            }
            else
            { // Battle Resort Day Care
                computerOff = 0x19BF5C;
                computerIN = 0x500000;
                wtboxesOff = 0x19BFCC;
                organizeBoxIN = 0x710000;
                organizeBoxOUT = 0x500000;
                wtboxviewOff = 0x66F5F2;
                wtboxviewIN = 0xC000;
                wtboxviewOUT = 0x4000;
                wtboxviewRange = 0x1000;
                mapidoff = 0x8187BD4;
                mapxoff = 0x8187BF4;
                mapyoff = 0x8187BFC;
                //mapzoff = 0x8187BF8;
                routemapid = 0xD2;
                daycaremapid = 0x207;
                daycaremanx = 0x44A9E000;
                daycaremany = 0x44D92000;
                daycaredoorx = 0x449C6000;
                //daycaredoory = 0x44D4A000;
                daycareexitx = 0x43610000;
                //daycareexity = 0x43A68000;
                computerx = 0x43828000;
                computery = 0x43730000;
                eggoff = 0x8C88548;
                runtime = 2000;
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
                string modemessage;
                switch (Mode.SelectedIndex)
                {
                    case 0:
                        modemessage = "Simple: This bot will produce " + Eggs.Value.ToString() + " eggs and deposit them in the pc, starting at box " + Box.Value.ToString() + " slot " + Slot.Value.ToString() + ".\r\n\r\n";
                        break;
                    case 1:
                        modemessage = "Filter: This bot will produce eggs and deposit them in the pc, starting at box " + Box.Value.ToString() + " slot " + Slot.Value.ToString() + ". Then it will check against the selected filters and if it finds a match the bot will stop. The bot will also stop if it produces " + Eggs.Value.ToString() + " eggs before finding a match.\r\n\r\n";
                        break;
                    case 2:
                        modemessage = "ESV/TSV: This bot will produce eggs and deposit them in the pc, starting at box " + Box.Value.ToString() + " slot " + Slot.Value.ToString() + ". Then it will check the egg's ESV and if it finds a match with the values in the TSV list, the bot will stop. The bot will also stop if it produces " + Eggs.Value.ToString() + " eggs before finding a match.\r\n\r\n";
                        break;
                    default:
                        modemessage = "No mode selected. Select one and try again.\r\n\r\n";
                        break;
                }
                DialogResult dialogResult;
                dialogResult = MessageBox.Show("This bot will start producing eggs from the day care using the following rules:\r\n\r\n" + modemessage + "Make sure that you only have one pokémon in your party. Please read the Wiki at Github before starting. Do you want to continue?", "Breeding bot", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.OK && Eggs.Value > 0 && Mode.SelectedIndex >= 0)
                {
                    // Configure GUI
                    Delg.SetText(RunStop, "Stop Bot");
                    // Initialize variables
                    if (ORAS)
                    {
                        ORASdaycare(daycareRoute117.Checked);
                    }
                    botworking = true;
                    userstop = false;
                    botState = breedbotstates.botstart;
                    attempts = 0;
                    maxreconnect = 10;
                    boxchange = true;
                    eggsinparty = 0;
                    eggsinbatch = 0;
                    filternum = -1;
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
                Program.gCmdWindow.botMode(true);
                while (botworking && Program.gCmdWindow.IsConnected)
                {
                    switch (botState)
                    {
                        case (int)breedbotstates.botstart:
                            Report("Bot: START Gen 6 Breding bot");
                            if (QuickBreed.Checked)
                            {
                                botState = breedbotstates.facedaycareman;
                            }
                            else if (Mode.SelectedIndex >= 0)
                            {
                                botState = breedbotstates.walk1;
                            }
                            else
                            {
                                botState = breedbotstates.botexit;
                            }
                            break;

                        case breedbotstates.facedaycareman:
                            Report("Bot: Turn to Day Care Man");
                            waitTaskbool = Program.helper.waitbutton(LookupTable.DpadUP);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = breedbotstates.quickegg;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = breedbotstates.facedaycareman;
                            }
                            break;

                        case breedbotstates.quickegg:
                            waitTaskbool = Program.helper.waitNTRwrite(eggoff, 0x01, Program.gCmdWindow.pid);
                            if (await waitTaskbool)
                            {
                                attempts = -10;
                                botState = breedbotstates.triggerdialog;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.WriteError;
                                botState = breedbotstates.quickegg;
                            }
                            break;

                        case breedbotstates.walk1:
                            Report("Bot: Run south");
                            Program.helper.quickbuton(LookupTable.runDOWN, runtime);
                            await Task.Delay(runtime + 250);
                            botState = breedbotstates.walk2;
                            break;

                        case breedbotstates.walk2:
                            Report("Bot: Run north");
                            Program.helper.quickbuton(LookupTable.runUP, runtime);
                            await Task.Delay(runtime + 250);
                            botState = breedbotstates.checkegg;
                            break;

                        case breedbotstates.checkegg:
                            Report("Bot: Check if an egg is available");
                            waitTaskbool = Program.helper.memoryinrange(eggoff, 0x01, 0x01);
                            if (await waitTaskbool)
                            {
                                Report("Bot: Egg found");
                                botState = breedbotstates.checkmap1;
                            }
                            else
                            {
                                botState = breedbotstates.walk1;
                            }
                            break;

                        case breedbotstates.checkmap1:
                            waitTaskbool = Program.helper.timememoryinrange(mapyoff, daycaremany, 0x01, 100, 5000);
                            if (await waitTaskbool)
                            {
                                attempts = -10;
                                Report("Bot: Egg found");
                                botState = breedbotstates.triggerdialog;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = breedbotstates.walk3;
                            }
                            break;

                        case breedbotstates.walk3:
                            Report("Bot: Return to day care man");
                            Program.helper.quickbuton(LookupTable.runUP, longcommandtime);
                            await Task.Delay(longcommandtime + commanddelay);
                            botState = breedbotstates.checkmap1;
                            break;

                        case breedbotstates.triggerdialog:
                            Report("Bot: Talk to Day Care Man");
                            int i;
                            for (i = 0; i < 7; i++)
                            {
                                await Task.Delay(commanddelay);
                                waitTaskbool = Program.helper.waitbutton(LookupTable.keyA);
                                if (!(await waitTaskbool))
                                    break;
                            }
                            if (i == 7)
                            {
                                botState = breedbotstates.checknoegg;
                            }
                            else
                            {
                                botState = breedbotstates.continuedialog;
                            }
                            break;

                        case breedbotstates.continuedialog:
                            Report("Bot: Continue dialog");
                            await Task.Delay(commanddelay);
                            waitTaskbool = Program.helper.waitbutton(LookupTable.keyA);
                            if (await waitTaskbool)
                            {
                                botState = breedbotstates.checknoegg;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = breedbotstates.continuedialog;
                            }
                            break;

                        case breedbotstates.checknoegg:
                            waitTaskbool = Program.helper.memoryinrange(eggoff, 0x00, 0x01);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                Report("Bot: Egg received");
                                botState = breedbotstates.exitdialog;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = breedbotstates.triggerdialog;
                            }
                            break;

                        case breedbotstates.exitdialog:
                            Report("Bot: Exit dialog");
                            await Task.Delay(5 * commanddelay);
                            waitTaskbool = Program.helper.waitbutton(LookupTable.keyB);
                            if (await waitTaskbool)
                            {
                                waitTaskbool = Program.helper.waitbutton(LookupTable.keyB);
                                if (await waitTaskbool)
                                {
                                    addEggtoParty();
                                    if (eggsinparty >= 5 || Eggs.Value == 0)
                                    {
                                        attempts = -15; // Allow more attempts
                                        botState = breedbotstates.walktodaycare;
                                    }
                                    else if (QuickBreed.Checked)
                                    {
                                        botState = breedbotstates.quickegg;
                                    }
                                    else
                                    {
                                        botState = breedbotstates.walk1;
                                    }
                                }
                                else
                                {
                                    attempts++;
                                    botresult = ErrorMessage.ButtonError;
                                    botState = breedbotstates.exitdialog;
                                }
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = breedbotstates.exitdialog;
                            }
                            break;

                        case breedbotstates.walktodaycare:
                            Report("Bot: Walk to Day Care");
                            await Task.Delay(commanddelay);
                            if (ORAS && daycareBattleResort.Checked)
                            {
                                Program.helper.quickbuton(LookupTable.DpadLEFT, walktime);
                            }
                            else
                            {
                                Program.helper.quickbuton(LookupTable.DpadRIGHT, walktime);
                            }
                            await Task.Delay(walktime + commanddelay);
                            botState = breedbotstates.checkmap2;
                            break;

                        case breedbotstates.checkmap2:
                            lastposition = Program.helper.lastRead;
                            waitTaskbool = Program.helper.memoryinrange(mapxoff, daycaredoorx, 0x01);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = breedbotstates.entertodaycare;
                            }
                            else if (lastposition == Program.helper.lastRead)
                            {
                                Report("Bot: No movement detected, still on dialog?");
                                Program.helper.quickbuton(LookupTable.keyB, 250);
                                await Task.Delay(commanddelay);
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = breedbotstates.walktodaycare;
                            }
                            else if (Program.helper.lastRead < daycaredoorx && ORAS && daycareBattleResort.Checked)
                            {
                                attempts++;
                                botresult = ErrorMessage.GeneralError;
                                botState = breedbotstates.fix1;
                            }
                            else if (Program.helper.lastRead > daycaredoorx && (!ORAS || daycareRoute117.Checked))
                            {
                                attempts++;
                                botresult = ErrorMessage.GeneralError;
                                botState = breedbotstates.fix1;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.GeneralError;
                                botState = breedbotstates.walktodaycare;
                            }
                            break;

                        case breedbotstates.fix1:
                            Report("Bot: Missed day care, return");
                            await Task.Delay(commanddelay);
                            if (ORAS && daycareBattleResort.Checked)
                            {
                                Program.helper.quickbuton(LookupTable.DpadRIGHT, walktime);
                            }
                            else
                            {
                                Program.helper.quickbuton(LookupTable.DpadLEFT, walktime);
                            }
                            await Task.Delay(walktime + commanddelay);
                            botState = breedbotstates.checkmap2;
                            break;

                        case breedbotstates.entertodaycare:
                            Report("Bot: Enter to Day Care");
                            await Task.Delay(commanddelay);
                            Program.helper.quickbuton(LookupTable.runUP, longcommandtime);
                            await Task.Delay(longcommandtime + commanddelay);
                            botState = breedbotstates.checkmap3;
                            break;

                        case breedbotstates.checkmap3:
                            waitTaskbool = Program.helper.timememoryinrange(mapidoff, daycaremapid, 0x01, 100, 5000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = breedbotstates.walktodesk;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = breedbotstates.entertodaycare;
                            }
                            break;

                        case breedbotstates.walktodesk:
                            Report("Bot: Run to desk");
                            await Task.Delay(commanddelay);
                            Program.helper.quickbuton(LookupTable.runUP, longcommandtime);
                            await Task.Delay(longcommandtime + commanddelay);
                            botState = breedbotstates.checkmap4;
                            break;

                        case breedbotstates.checkmap4:
                            waitTaskbool = Program.helper.timememoryinrange(mapyoff, computery, 0x01, 100, 5000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = breedbotstates.walktocomputer;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = breedbotstates.walktodesk;
                            }
                            break;

                        case breedbotstates.walktocomputer:
                            Report("Bot: Walk to the PC");
                            await Task.Delay(commanddelay);
                            Program.helper.quickbuton(LookupTable.DpadRIGHT, walktime);
                            await Task.Delay(walktime + commanddelay);
                            botState = breedbotstates.checkmap5;
                            break;

                        case breedbotstates.checkmap5:
                            waitTaskbool = Program.helper.memoryinrange(mapxoff, computerx, 0x01);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = breedbotstates.facecomputer;
                            }
                            else if (Program.helper.lastRead > computerx)
                            {
                                attempts++;
                                botresult = ErrorMessage.GeneralError;
                                botState = breedbotstates.fix2;
                            }
                            else
                            { // Still far from computer
                                attempts++;
                                botresult = ErrorMessage.GeneralError;
                                botState = breedbotstates.walktocomputer;
                            }
                            break;

                        case breedbotstates.fix2:
                            Report("Bot: Missed PC, return");
                            await Task.Delay(commanddelay);
                            Program.helper.quickbuton(LookupTable.DpadLEFT, walktime);
                            await Task.Delay(walktime + commanddelay);
                            botState = breedbotstates.checkmap5;
                            break;

                        case breedbotstates.facecomputer:
                            Report("Bot: Turn on the PC");
                            await Task.Delay(commanddelay);
                            waitTaskbool = Program.helper.waitbutton(LookupTable.DpadUP);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = breedbotstates.startcomputer;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = breedbotstates.facecomputer;
                            }
                            break;

                        case breedbotstates.startcomputer:
                            waitTaskbool = Program.helper.waitbutton(LookupTable.keyA);
                            if (await waitTaskbool)
                            {
                                botState = breedbotstates.testcomputer;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = breedbotstates.startcomputer;
                            }
                            break;

                        case breedbotstates.testcomputer:
                            Report("Bot: Test if the PC is on");
                            await Task.Delay(4 * commanddelay); // Wait for PC on
                            waitTaskbool = Program.helper.timememoryinrange(computerOff, computerIN, 0x10000, 100, 5000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = breedbotstates.computerdialog;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = breedbotstates.facecomputer;
                            }
                            break;

                        case breedbotstates.computerdialog:
                            Report("Bot: Skip PC dialog");
                            await Task.Delay(commanddelay);
                            waitTaskbool = Program.helper.waitbutton(LookupTable.keyA);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = breedbotstates.pressPCstorage;
                            }
                            else
                            {
                                botresult = ErrorMessage.ButtonError;
                                Report("Bot: Error detected");
                                attempts = 11;
                            }
                            break;

                        case breedbotstates.pressPCstorage:
                            Report("Bot: Press Access PC storage");
                            await Task.Delay(commanddelay);
                            waitTaskbool = Program.helper.waitbutton(LookupTable.keyA);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = breedbotstates.touchOrganize;
                            }
                            else
                            {
                                botresult = ErrorMessage.ButtonError;
                                Report("Bot: Error detected");
                                attempts = 11;
                            }
                            break;

                        case breedbotstates.touchOrganize:
                            Report("Bot: Touch Organize boxes");
                            await Task.Delay(commanddelay);
                            if (ORAS && orgboxTop.Checked)
                            {
                                waitTaskbool = Program.helper.waittouch(160, 40);
                            }
                            else
                            {
                                waitTaskbool = Program.helper.waittouch(160, 120);
                            }
                            if (await waitTaskbool)
                            {
                                botState = breedbotstates.testboxes;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.TouchError;
                                botState = breedbotstates.touchOrganize;
                            }
                            break;

                        case breedbotstates.testboxes:
                            Report("Test if the boxes are shown");
                            await Task.Delay(2 * commanddelay);
                            waitTaskbool = Program.helper.timememoryinrange(wtboxesOff, organizeBoxIN, 0x10000, 100, 5000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = breedbotstates.readslot;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = breedbotstates.touchOrganize;
                            }
                            break;

                        case breedbotstates.readslot:
                            Report("Bot: Search for empty slot");
                            waitTaskPKM = Program.helper.waitPokeRead(Box, Slot);
                            breedPoke = await waitTaskPKM;
                            if (breedPoke == null)
                            { // No data or invalid
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = breedbotstates.readslot;
                            }
                            else if (breedPoke.Species == 0)
                            { // Empty space
                                Report("Bot: Empty slot");
                                attempts = 0;
                                botState = breedbotstates.testboxchange;
                            }
                            else
                            {
                                getNextSlot();
                                botState = breedbotstates.readslot;
                            }
                            break;

                        case breedbotstates.testboxchange:
                            if (boxchange)
                            {
                                botState = breedbotstates.touchboxview;
                                boxchange = false;
                            }
                            else
                            {
                                botState = breedbotstates.touchegg;
                            }
                            break;

                        case breedbotstates.touchboxview:
                            Report("Bot: Touch Box View");
                            await Task.Delay(commanddelay);
                            waitTaskbool = Program.helper.waittouch(30, 220);
                            if (await waitTaskbool)
                            {
                                botState = breedbotstates.testboxview;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.TouchError;
                                botState = breedbotstates.touchboxview;
                            }
                            break;

                        case breedbotstates.testboxview:
                            Report("Bot: Test if box view is shown");
                            await Task.Delay(2 * commanddelay);
                            waitTaskbool = Program.helper.timememoryinrange(wtboxviewOff, wtboxviewIN, wtboxviewRange, 100, 5000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = breedbotstates.touchnewbox;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = breedbotstates.touchboxview;
                            }
                            break;

                        case breedbotstates.touchnewbox:
                            Report("Bot: Touch New Box");
                            await Task.Delay(commanddelay);
                            waitTaskbool = Program.helper.waittouch(LookupTable.boxposX6[GetIndex(Box)], LookupTable.boxposY6[GetIndex(Box)]);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = breedbotstates.selectnewbox;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.TouchError;
                                botState = breedbotstates.touchnewbox;
                            }
                            break;

                        case breedbotstates.selectnewbox:
                            Report("Bot: Select New Box");
                            await Task.Delay(commanddelay);
                            waitTaskbool = Program.helper.waitbutton(LookupTable.keyA);
                            if (await waitTaskbool)
                            {
                                botState = breedbotstates.testviewout;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = breedbotstates.selectnewbox;
                            }
                            break;

                        case breedbotstates.testviewout:
                            Report("Bot: Test if box view is not shown");
                            await Task.Delay(2 * commanddelay);
                            waitTaskbool = Program.helper.timememoryinrange(wtboxviewOff, wtboxviewOUT, wtboxviewRange, 100, 5000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = breedbotstates.touchegg;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = breedbotstates.selectnewbox;
                            }
                            break;

                        case breedbotstates.touchegg:
                            Report("Bot: Select Egg");
                            await Task.Delay(commanddelay);
                            waitTaskbool = Program.helper.waitholdtouch(300, 100);
                            if (await waitTaskbool)
                            {
                                botState = breedbotstates.moveegg;
                            }
                            else
                            {
                                botresult = ErrorMessage.TouchError;
                                Report("Bot: Error detected");
                                attempts = 11;
                            }
                            break;

                        case breedbotstates.moveegg:
                            Report("Move Egg");
                            await Task.Delay(commanddelay);
                            waitTaskbool = Program.helper.waitholdtouch(LookupTable.pokeposX6[GetIndex(Slot)], LookupTable.pokeposY6[GetIndex(Slot)]);
                            if (await waitTaskbool)
                            {
                                botState = breedbotstates.releaseegg;
                            }
                            else
                            {
                                botresult = ErrorMessage.TouchError;
                                Report("Bot: Error detected");
                                attempts = 11;
                            }
                            break;

                        case breedbotstates.releaseegg:
                            Report("Bot: Release Egg");
                            await Task.Delay(commanddelay);
                            waitTaskbool = Program.helper.waitfreetouch();
                            if (await waitTaskbool)
                            {
                                egglocations[eggsinbatch, 0] = Box.Value;
                                egglocations[eggsinbatch, 1] = Slot.Value;
                                eggsinbatch++;
                                getNextSlot();
                                eggsinparty--;
                                if (eggsinparty > 0)
                                {
                                    botState = breedbotstates.readslot;
                                }
                                else
                                {
                                    botState = breedbotstates.exitcomputer;
                                }
                            }
                            else
                            {
                                botresult = ErrorMessage.TouchError;
                                Report("Bot: Error detected");
                                attempts = 11;
                            }
                            break;

                        case breedbotstates.exitcomputer:
                            Report("Bot: Exit from PC");
                            await Task.Delay(commanddelay);
                            waitTaskbool = Program.helper.waitbutton(LookupTable.keyX);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = breedbotstates.testexit;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ButtonError;
                                botState = breedbotstates.exitcomputer;
                            }
                            break;

                        case breedbotstates.testexit:
                            Report("Bot: Test if out from PC");
                            waitTaskbool = Program.helper.timememoryinrange(wtboxesOff, organizeBoxOUT, 0x10000, 100, 5000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                if (Mode.SelectedIndex == 1 || Mode.SelectedIndex == 2 || ReadESV.Checked)
                                {
                                    botState = breedbotstates.filter;
                                }
                                else if (Eggs.Value > 0)
                                {
                                    botState = breedbotstates.retirefromcomputer;
                                }
                                else
                                {
                                    Report("Bot: Error detected");
                                    attempts = 11;
                                }
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = breedbotstates.exitcomputer;
                            }
                            break;

                        case breedbotstates.retirefromcomputer:
                            Report("Bot: Retire from PC");
                            await Task.Delay(commanddelay);
                            eggsinbatch = 0;
                            Program.helper.quickbuton(LookupTable.DpadLEFT, walktime);
                            await Task.Delay(walktime + commanddelay);
                            botState = breedbotstates.checkmap6;
                            break;

                        case breedbotstates.checkmap6:
                            waitTaskbool = Program.helper.memoryinrange(mapxoff, daycareexitx, 0x01);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = breedbotstates.retirefromdesk;
                            }
                            else if (Program.helper.lastRead < daycareexitx)
                            {
                                attempts++;
                                botresult = ErrorMessage.GeneralError;
                                botState = breedbotstates.fix3;
                            }
                            else
                            { // Still far from exit
                                attempts++;
                                botresult = ErrorMessage.GeneralError;
                                botState = breedbotstates.retirefromcomputer;
                            }
                            break;

                        case breedbotstates.fix3:
                            Report("Bot: Missed exit, return");
                            await Task.Delay(commanddelay);
                            Program.helper.quickbuton(LookupTable.DpadRIGHT, walktime);
                            await Task.Delay(walktime + commanddelay);
                            botState = breedbotstates.checkmap6;
                            break;

                        case breedbotstates.retirefromdesk:
                            Report("Bot: Run to exit");
                            await Task.Delay(commanddelay);
                            Program.helper.quickbuton(LookupTable.runDOWN, longcommandtime);
                            await Task.Delay(longcommandtime + commanddelay);
                            botState = breedbotstates.checkmap7;
                            break;

                        case breedbotstates.checkmap7:
                            waitTaskbool = Program.helper.timememoryinrange(mapidoff, routemapid, 0x01, 100, 5000);
                            if (await waitTaskbool)
                            {
                                attempts = 0;
                                botState = breedbotstates.retirefromdoor;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.ReadError;
                                botState = breedbotstates.retirefromdesk;
                            }
                            break;

                        case breedbotstates.retirefromdoor:
                            Report("Bot: Retire from door");
                            await Task.Delay(commanddelay);
                            Program.helper.quickbuton(LookupTable.DpadDOWN, walktime);
                            await Task.Delay(walktime + commanddelay);
                            botState = breedbotstates.checkmap8;
                            break;

                        case breedbotstates.checkmap8:
                            waitTaskbool = Program.helper.memoryinrange(mapyoff, daycaremany, 0x01);
                            if (await waitTaskbool)
                            {
                                attempts = -10; // Allow more attempts
                                botState = breedbotstates.walktodaycareman;
                            }
                            else if (Program.helper.lastRead > daycaremany)
                            {
                                attempts++;
                                botresult = ErrorMessage.GeneralError;
                                botState = breedbotstates.fix5;
                            }
                            else
                            { // Still far from exit
                                attempts++;
                                botresult = ErrorMessage.GeneralError;
                                botState = breedbotstates.retirefromdoor;
                            }
                            break;

                        case breedbotstates.fix5:
                            Report("Bot: Missed Day Care Man, return");
                            await Task.Delay(commanddelay);
                            Program.helper.quickbuton(LookupTable.DpadUP, walktime);
                            await Task.Delay(walktime + commanddelay);
                            botState = breedbotstates.checkmap8;
                            break;

                        case breedbotstates.walktodaycareman:
                            Report("Bot: Walk to Day Care Man");
                            await Task.Delay(commanddelay);
                            if (ORAS && daycareBattleResort.Checked)
                            {
                                Program.helper.quickbuton(LookupTable.DpadRIGHT, walktime);
                            }
                            else
                            {
                                Program.helper.quickbuton(LookupTable.DpadLEFT, walktime);
                            }
                            await Task.Delay(walktime + commanddelay);
                            botState = breedbotstates.checkmap9;
                            break;

                        case breedbotstates.checkmap9:
                            waitTaskbool = Program.helper.memoryinrange(mapxoff, daycaremanx, 0x01);
                            if (await waitTaskbool)
                            {
                                if (QuickBreed.Checked)
                                {
                                    botState = breedbotstates.facedaycareman;
                                }
                                else
                                {
                                    botState = breedbotstates.walk1;
                                }
                                attempts = 0;
                            }
                            else if (Program.helper.lastRead > daycaremanx && ORAS && daycareBattleResort.Checked)
                            {
                                attempts++;
                                botresult = ErrorMessage.GeneralError;
                                botState = breedbotstates.fix4;
                            }
                            else if (Program.helper.lastRead > daycaremanx && (!ORAS || daycareRoute117.Checked))
                            {
                                attempts++;
                                botresult = ErrorMessage.GeneralError;
                                botState = breedbotstates.fix4;
                            }
                            else
                            {
                                attempts++;
                                botresult = ErrorMessage.GeneralError;
                                botState = breedbotstates.walktodaycareman;
                            }
                            break;

                        case breedbotstates.fix4:
                            Report("Bot: Missed Day Care Man, return");
                            await Task.Delay(commanddelay);
                            if (ORAS && daycareBattleResort.Checked)
                            {
                                Program.helper.quickbuton(LookupTable.DpadRIGHT, walktime);
                            }
                            else
                            {
                                Program.helper.quickbuton(LookupTable.DpadLEFT, walktime);
                            }
                            await Task.Delay(walktime + commanddelay);
                            botState = breedbotstates.checkmap9;
                            break;

                        case breedbotstates.filter:
                            for (i = 0; i < eggsinbatch; i++)
                            {
                                if (attempts > 10)
                                {
                                    break;
                                }
                                Delg.SetValue(Box, egglocations[i, 0]);
                                Delg.SetValue(Slot, egglocations[i, 1]);
                                bool testsok = false;
                                Report("Bot: Check deposited egg");
                                waitTaskPKM = Program.helper.waitPokeRead(Box, Slot);
                                breedPoke = await waitTaskPKM;
                                if (breedPoke == null)
                                { // No data or invalid
                                    attempts++;
                                    botresult = ErrorMessage.ReadError;
                                    i--;
                                    attempts++;
                                    continue;
                                }
                                else if (breedPoke.Species == 0)
                                { // Empty space
                                    Report("Bot: Error detected - slot is empty");
                                    attempts = 11;
                                    botresult = ErrorMessage.GeneralError;
                                    botState = breedbotstates.botexit;
                                    break;
                                }
                                else
                                {
                                    attempts = 0;
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
                                    botState = breedbotstates.testspassed;
                                    break;
                                }
                                else if (Eggs.Value > 0)
                                {
                                    botState = breedbotstates.retirefromcomputer;
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
                                    botState = breedbotstates.botexit;
                                }
                            }
                            break;

                        case breedbotstates.testspassed:
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
                            botState = breedbotstates.botexit;
                            break;

                        case breedbotstates.botexit:
                            Report("Bot: STOP Gen 6 Breding bot");
                            botworking = false;
                            break;

                        default:
                            Report("Bot: STOP Gen 6 Breding bot");
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
                                await Task.Delay(5 * commanddelay);
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
                            Report("Bot: STOP Gen 6 Breeding bot");
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
                Report("Bot: STOP Gen 6 Breeding bot");
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
            Program.gCmdWindow.botMode(false);
            EnableControls();
            Delg.SetEnabled(RunStop, true);
        }

        private void addEggtoParty()
        {
            eggsinparty++;
            Delg.SetValue(Eggs, Eggs.Value - 1);
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
            Delg.SetMaximum(Eggs, LookupTable.getMaxSpace((int)Box.Value, (int)Slot.Value));
        }

        private void esvSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (esvList.Rows.Count > 0)
                {
                    string folderPath = System.Windows.Forms.@Application.StartupPath + "\\" + FOLDERBOT + "\\";
                    (new FileInfo(folderPath)).Directory.Create();
                    string fileName = "ESVlist6.csv";
                    var esvlst = new StringBuilder();
                    var headers = esvList.Columns.Cast<DataGridViewColumn>();
                    esvlst.AppendLine(string.Join(",", headers.Select(column => column.HeaderText).ToArray()));
                    foreach (DataGridViewRow row in esvList.Rows)
                    {
                        var cells = row.Cells.Cast<DataGridViewCell>();
                        esvlst.AppendLine(string.Join(",", cells.Select(cell => cell.Value).ToArray()));
                    }
                    File.WriteAllText(folderPath + fileName, esvlst.ToString());
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
                    string folderPath = System.Windows.Forms.@Application.StartupPath + "\\" + FOLDERBOT + "\\";
                    (new FileInfo(folderPath)).Directory.Create();
                    string fileName = "TSVlist6.csv";
                    var tsvlst = new StringBuilder();
                    foreach (var value in TSVlist.Items)
                    {
                        tsvlst.AppendLine(value.ToString());
                    }
                    File.WriteAllText(folderPath + fileName, tsvlst.ToString());
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
                string folderPath = @Application.StartupPath + "\\" + FOLDERBOT + "\\";
                (new FileInfo(folderPath)).Directory.Create();
                string fileName = "TSVlist6.csv";
                if (File.Exists(folderPath + fileName))
                {
                    string[] values = File.ReadAllLines(folderPath + fileName);
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
            Delg.SetCheckedRadio(orgboxMiddle, true);
            Delg.SetCheckedRadio(daycareRoute117, true);
            Delg.SetChecked(ReadESV, false);
            Delg.SetChecked(QuickBreed, false);
            esvList.Rows.Clear();
            TSVlist.Items.Clear();
            filterList.Rows.Clear();
        }

        private void filterLoad_Click(object sender, EventArgs e)
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

        private void Bot_Breeding6_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (botworking)
            {
                MessageBox.Show("Stop the bot before closing this window", "Breeding bot", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }

        private void Bot_Breeding6_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.gCmdWindow.Tool_Finish();
        }
    }
}
