/// PKMN-NTR - On-the-air memory editor for 3DS Pokémon games
/// Copyright(C) 2016-2017  PKMN-NTR Dev Team
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static pkmn_ntr.Bot.Bot;

namespace pkmn_ntr.Bot
{
    /// <summary>
    /// Generation 6 Breeding bot.
    /// </summary>
    public partial class Bot_Breeding6 : Form
    {
        /// <summary>
        /// Secuency of steps done by the bot.
        /// </summary>
        private enum BotState
        {
            StartBot, TurnToDayCareMan, GenerateEgg, Walk1, Walk2, CheckEggFlagSet, Walk3,
            CheckMap1, StartDialog, ContinueDialog, CheckEggFlagClear, ExitDialog,
            WalkToDayCare, CheckMap2, FixPosition1, EnterDayCare, CheckMap3, WalkToDesk,
            CheckMap4, WalkToPC, CheckMap5, FixPosition2, TurnToPC, StartPC, TestPC,
            PCDialog, SelectStorage, SelectOrganize, TestStorage, ReadSlot, TestBoxChange,
            SelectBoxView, TestBoxView, TouchNewBox, SelectNewBox, TestReturnStorage,
            SelectEgg, MoveEgg, ReleaseEgg, ExitPC, TestExitPC, ReadEgg, RetireFromPC,
            CheckMap6, FixPosition3, RetireFromDesk, CheckMap7, RetireFromDoor, CheckMap8,
            FixPosition5, WalkToDayCareMan, CheckMap9, FixPosition4, FilterTest,
            AllTestsPassed, ExitBot
        };

        // General bot variables
        private readonly string botName = "Breeding bot";
        private bool botWorking;
        private bool userStop;
        private BotState botState;
        private ErrorMessage botResult;
        private int attempts;
        private int maxReconnections;
        private Task<bool> waitForBool;
        private Task<PKM> waitForPKM;

        // Class variables
        private bool changeBox;
        private bool ORAS;
        private decimal[,] eggLocations = new decimal[5, 2];
        private int runningTime;
        private int eggsInParty;
        private int eggsInBatch;
        private int matchingFilter;
        private int[] finishInformation = new int[] { -1, -1, -1 };
        private PKM currentEgg;
        private uint lastPosition;

        // Class constants
        private readonly int walkingTime = 250;
        private readonly int shortDelay = 500;
        private readonly int longDelay = 1250;

        // Data offsets
        private uint storageViewOffset;
        private uint storageViewIn;
        private uint storageViewOut;
        private uint boxViewOffset;
        private uint boxViewIn;
        private uint boxViewOut;
        private uint boxViewRange;
        private uint pcMenuOffset;
        private uint pcMenuIn;
        //private uint pcMenuOut;
        private uint mapIdOffset;
        private uint mapXOffset;
        private uint mapYOffset;
        //private uint mapZOffset;
        private uint routeMapId;
        private uint daycareMapId;
        private uint daycareManXPosition;
        private uint daycareManYPosition;
        private uint daycareDoorXPosition;
        //uint daycareDoorYPosition;
        private uint daycareExitXPosition;
        //uint daycareExitYPosition;
        private uint pcXPosition;
        private uint pcYPosition;
        private uint eggFlagOffset;

        public Bot_Breeding6()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads XY data offsets.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadForm(object sender, EventArgs e)
        {
            if (Program.gCmdWindow.IsXY)
            { // XY
                ORAS = false;
                Delg.SetEnabled(grpOrganizeBoxes, false);
                Delg.SetEnabled(grpDaycare, false);
                pcMenuOffset = 0x19A918;
                pcMenuIn = 0x4D0000;
                storageViewOffset = 0x19A988;
                storageViewIn = 0x6C0000;
                storageViewOut = 0x4D0000;
                boxViewOffset = 0x627437;
                boxViewIn = 0x00000000;
                boxViewOut = 0x20000000;
                boxViewRange = 0x1000000;
                mapIdOffset = 0x81828EC;
                mapXOffset = 0x818290C;
                mapYOffset = 0x8182914;
                //mapZOffset = 0x8182910;
                routeMapId = 0x108;
                daycareMapId = 0x109;
                daycareManXPosition = 0x46219400;
                daycareManYPosition = 0x460F9400;
                daycareDoorXPosition = 0x4622FC00;
                //daycareDoorYPosition = 0x460F4C00;
                daycareExitXPosition = 0x43610000;
                //daycareExitYPosition = 0x43AF8000;
                pcXPosition = 0x43828000;
                pcYPosition = 0x43730000;
                eggFlagOffset = 0x8C80124;
                runningTime = 1000;
            }
            else if (Program.gCmdWindow.IsORAS)
            { // ORAS
                ORAS = true;
            }
        }

        /// <summary>
        /// Loads ORAS data offsets depending on which Daycare is selected.
        /// </summary>
        /// <param name="isRoute117">Use true if the Route 117 Day Care is used.</param>
        private void ChangeORASOffsets(bool isRoute117)
        {
            if (isRoute117)
            { // Route 117 daycare
                pcMenuOffset = 0x19BF5C;
                pcMenuIn = 0x500000;
                storageViewOffset = 0x19BFCC;
                storageViewIn = 0x710000;
                storageViewOut = 0x500000;
                boxViewOffset = 0x66F5F2;
                boxViewIn = 0xC000;
                boxViewOut = 0x4000;
                boxViewRange = 0x1000;
                mapIdOffset = 0x8187BD4;
                mapXOffset = 0x8187BF4;
                mapYOffset = 0x8187BFC;
                //mapzoff = 0x8187BF8;
                routeMapId = 0x2C;
                daycareMapId = 0x187;
                daycareManXPosition = 0x45553000;
                daycareManYPosition = 0x44D92000;
                daycareDoorXPosition = 0x455AD000;
                //daycaredoory = 0x44D6E000;
                daycareExitXPosition = 0x43610000;
                //daycareexity = 0x43A68000;
                pcXPosition = 0x43828000;
                pcYPosition = 0x43730000;
                eggFlagOffset = 0x8C88358;
                runningTime = 1000;
            }
            else
            { // Battle Resort Day Care
                pcMenuOffset = 0x19BF5C;
                pcMenuIn = 0x500000;
                storageViewOffset = 0x19BFCC;
                storageViewIn = 0x710000;
                storageViewOut = 0x500000;
                boxViewOffset = 0x66F5F2;
                boxViewIn = 0xC000;
                boxViewOut = 0x4000;
                boxViewRange = 0x1000;
                mapIdOffset = 0x8187BD4;
                mapXOffset = 0x8187BF4;
                mapYOffset = 0x8187BFC;
                //mapZOffset = 0x8187BF8;
                routeMapId = 0xD2;
                daycareMapId = 0x207;
                daycareManXPosition = 0x44A9E000;
                daycareManYPosition = 0x44D92000;
                daycareDoorXPosition = 0x449C6000;
                //daycareDoorYPosition = 0x44D4A000;
                daycareExitXPosition = 0x43610000;
                //daycareExitYPosition = 0x43A68000;
                pcXPosition = 0x43828000;
                pcYPosition = 0x43730000;
                eggFlagOffset = 0x8C88548;
                runningTime = 2000;
            }
        }

        /// <summary>
        /// Start or stop the bot.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunStopBot(object sender, EventArgs e)
        {
            SetControls(false);
            if (botWorking)
            { // Stop the bot
                Delg.SetEnabled(btnRunStop, false);
                Delg.SetText(btnRunStop, "Start Bot");
                botWorking = false;
                userStop = true;
            }
            else
            { // Show information about the bot
                string modemessage;
                switch (cmbMode.SelectedIndex)
                {
                    case 0:
                        modemessage = $"Simple: This bot will produce {numEggs.Value} " +
                            $"eggs and deposit them in the PC, starting at box " +
                            $"{numBox.Value} slot {numSlot.Value}.\n\n";
                        break;
                    case 1:
                        modemessage = $"Filter: This bot will produce eggs and deposit" +
                            $" them in the pc, starting at box {numBox.Value} slot " +
                            $"{numSlot.Value}. Then it will check against the filter " +
                            $"list and if it finds a match the bot will stop. The bot " +
                            $"will also stop if it produces {numEggs.Value} eggs before" +
                            $"finding a match.\n\n";
                        break;
                    case 2:
                        modemessage = $"ESV/TSV: This bot will produce eggs and " +
                            $"deposit them in the pc, starting at box {numBox.Value} " +
                            $"slot {numSlot.Value}. Then it will check the egg's ESV " +
                            $"and if it finds a match with the values in the TSV list, " +
                            $"the bot will stop. The bot will also stop if it produces " +
                            $"{numEggs.Value} eggs before finding a match.\n\n";
                        break;
                    default:
                        modemessage = "No mode selected. Select one and try again.\n\n";
                        return;
                }
                DialogResult dialogResult;
                dialogResult = MessageBox.Show($"This bot will start producing eggs " +
                    $"from the Day Care using the following rules:\n\n {modemessage}" +
                    $"Make sure to have only one pokémon in your party. Please read " +
                    $"the Wiki at Github before starting. Do you want to continue?",
                    botName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes && numEggs.Value > 0)
                {
                    // Configure GUI
                    Delg.SetText(btnRunStop, "Stop Bot");
                    // Initialize variables
                    if (ORAS)
                    {
                        ChangeORASOffsets(radRoute117.Checked);
                    }
                    botWorking = true;
                    userStop = false;
                    botState = BotState.StartBot;
                    attempts = 0;
                    maxReconnections = 10;
                    changeBox = true;
                    eggsInParty = 0;
                    eggsInBatch = 0;
                    matchingFilter = -1;
                    // Run the bot
                    Program.gCmdWindow.SetBotMode(true);
                    RunBot();
                }
                else
                {
                    SetControls(true);
                }
            }
        }

        /// <summary>
        /// Enables or disables the controls in the form.
        /// </summary>
        /// <param name="state"></param>
        private void SetControls(bool state)
        {
            Delg.SetEnabled(grpOptions, state);
            Delg.SetEnabled(numTSV, state);
            Delg.SetEnabled(btnAddTSV, state);
            Delg.SetEnabled(btnRemoveTSV, state);
            Delg.SetEnabled(btnLoadTSV, state);
            Delg.SetEnabled(btnSaveTSV, state);
            Delg.SetEnabled(btnLoadFilters, state);
        }

        /// <summary>
        /// Bot procedure.
        /// </summary>
        public async void RunBot()
        {
            try
            {
                Program.gCmdWindow.SetBotMode(true);
                while (botWorking && Program.gCmdWindow.IsConnected)
                {
                    switch (botState)
                    {
                        case (int)BotState.StartBot:
                            Report("Bot: START Gen 6 Breding bot");
                            if (chkQuickBreed.Checked)
                            {
                                botState = BotState.TurnToDayCareMan;
                            }
                            else if (cmbMode.SelectedIndex >= 0)
                            {
                                botState = BotState.Walk1;
                            }
                            else
                            {
                                botState = BotState.ExitBot;
                            }
                            break;

                        case BotState.TurnToDayCareMan:
                            Report("Bot: Turn towards Day Care Man");
                            waitForBool = Program.helper.waitbutton(LookupTable.DPadUp);
                            if (await waitForBool)
                            {
                                attempts = 0;
                                botState = BotState.GenerateEgg;
                            }
                            else
                            {
                                attempts++;
                                botResult = ErrorMessage.ButtonError;
                                botState = BotState.TurnToDayCareMan;
                            }
                            break;

                        case BotState.GenerateEgg:
                            Report("Bot: Generate Egg");
                            waitForBool = Program.helper.waitNTRwrite(eggFlagOffset,
                                0x01, Program.gCmdWindow.pid);
                            if (await waitForBool)
                            {
                                attempts = -10;
                                botState = BotState.StartDialog;
                            }
                            else
                            {
                                attempts++;
                                botResult = ErrorMessage.WriteError;
                                botState = BotState.GenerateEgg;
                            }
                            break;

                        case BotState.Walk1:
                            Report("Bot: Run south");
                            Program.helper.quickbuton(LookupTable.RunDown, runningTime);
                            await Task.Delay(runningTime + 250);
                            botState = BotState.Walk2;
                            break;

                        case BotState.Walk2:
                            Report("Bot: Run north");
                            Program.helper.quickbuton(LookupTable.RunUp, runningTime);
                            await Task.Delay(runningTime + 250);
                            botState = BotState.CheckEggFlagSet;
                            break;

                        case BotState.CheckEggFlagSet:
                            Report("Bot: Check if an egg is available");
                            waitForBool = Program.helper.memoryinrange(eggFlagOffset,
                                0x01, 0x01);
                            if (await waitForBool)
                            {
                                Report("Bot: Egg found");
                                botState = BotState.CheckMap1;
                            }
                            else
                            {
                                botState = BotState.Walk1;
                            }
                            break;

                        case BotState.CheckMap1:
                            waitForBool = Program.helper.timememoryinrange(mapYOffset,
                                daycareManYPosition, 0x01, 100, 5000);
                            if (await waitForBool)
                            {
                                attempts = -10;
                                Report("Bot: Egg found");
                                botState = BotState.StartDialog;
                            }
                            else
                            {
                                attempts++;
                                botResult = ErrorMessage.ReadError;
                                botState = BotState.Walk3;
                            }
                            break;

                        case BotState.Walk3:
                            Report("Bot: Return to Day Care Man");
                            Program.helper.quickbuton(LookupTable.RunUp, longDelay);
                            await Task.Delay(longDelay + shortDelay);
                            botState = BotState.CheckMap1;
                            break;

                        case BotState.StartDialog:
                            Report("Bot: Talk to Day Care Man");
                            int i;
                            for (i = 0; i < 7; i++)
                            {
                                await Task.Delay(shortDelay);
                                waitForBool = Program.helper.waitbutton(LookupTable.ButtonA);
                                if (!(await waitForBool))
                                    break;
                            }
                            if (i == 7)
                            {
                                botState = BotState.CheckEggFlagClear;
                            }
                            else
                            {
                                botState = BotState.ContinueDialog;
                            }
                            break;

                        case BotState.ContinueDialog:
                            Report("Bot: Continue dialog");
                            await Task.Delay(shortDelay);
                            waitForBool = Program.helper.waitbutton(LookupTable.ButtonA);
                            if (await waitForBool)
                            {
                                botState = BotState.CheckEggFlagClear;
                            }
                            else
                            {
                                attempts++;
                                botResult = ErrorMessage.ButtonError;
                                botState = BotState.ContinueDialog;
                            }
                            break;

                        case BotState.CheckEggFlagClear:
                            waitForBool = Program.helper.memoryinrange(eggFlagOffset,
                                0x00, 0x01);
                            if (await waitForBool)
                            {
                                attempts = 0;
                                Report("Bot: Egg received");
                                botState = BotState.ExitDialog;
                            }
                            else
                            {
                                attempts++;
                                botResult = ErrorMessage.ReadError;
                                botState = BotState.StartDialog;
                            }
                            break;

                        case BotState.ExitDialog:
                            Report("Bot: Exit dialog");
                            await Task.Delay(5 * shortDelay);
                            waitForBool = Program.helper.waitbutton(LookupTable.ButtonB);
                            if (await waitForBool)
                            {
                                waitForBool = Program.helper.waitbutton(LookupTable.ButtonB);
                                if (await waitForBool)
                                {
                                    AddEggtoParty();
                                    if (eggsInParty >= 5 || numEggs.Value == 0)
                                    {
                                        attempts = -15; // Allow more attempts
                                        botState = BotState.WalkToDayCare;
                                    }
                                    else if (chkQuickBreed.Checked)
                                    {
                                        botState = BotState.GenerateEgg;
                                    }
                                    else
                                    {
                                        botState = BotState.Walk1;
                                    }
                                }
                                else
                                {
                                    attempts++;
                                    botResult = ErrorMessage.ButtonError;
                                    botState = BotState.ExitDialog;
                                }
                            }
                            else
                            {
                                attempts++;
                                botResult = ErrorMessage.ButtonError;
                                botState = BotState.ExitDialog;
                            }
                            break;

                        case BotState.WalkToDayCare:
                            Report("Bot: Walk to Day Care");
                            await Task.Delay(shortDelay);
                            if (ORAS && radBattleResort.Checked)
                            {
                                Program.helper.quickbuton(LookupTable.DPadLeft,
                                    walkingTime);
                            }
                            else
                            {
                                Program.helper.quickbuton(LookupTable.DPadRight,
                                    walkingTime);
                            }
                            await Task.Delay(walkingTime + shortDelay);
                            botState = BotState.CheckMap2;
                            break;

                        case BotState.CheckMap2:
                            lastPosition = Program.helper.lastRead;
                            waitForBool = Program.helper.memoryinrange(mapXOffset,
                                daycareDoorXPosition, 0x01);
                            if (await waitForBool)
                            {
                                attempts = 0;
                                botState = BotState.EnterDayCare;
                            }
                            else if (lastPosition == Program.helper.lastRead)
                            {
                                Report("Bot: No movement detected, still on dialog?");
                                Program.helper.quickbuton(LookupTable.ButtonB, 250);
                                await Task.Delay(shortDelay);
                                attempts++;
                                botResult = ErrorMessage.ButtonError;
                                botState = BotState.WalkToDayCare;
                            }
                            else if (Program.helper.lastRead < daycareDoorXPosition &&
                                ORAS && radBattleResort.Checked)
                            {
                                attempts++;
                                botResult = ErrorMessage.GeneralError;
                                botState = BotState.FixPosition1;
                            }
                            else if (Program.helper.lastRead > daycareDoorXPosition &&
                                (!ORAS || radRoute117.Checked))
                            {
                                attempts++;
                                botResult = ErrorMessage.GeneralError;
                                botState = BotState.FixPosition1;
                            }
                            else
                            {
                                attempts++;
                                botResult = ErrorMessage.GeneralError;
                                botState = BotState.WalkToDayCare;
                            }
                            break;

                        case BotState.FixPosition1:
                            Report("Bot: Missed Day Care, return");
                            await Task.Delay(shortDelay);
                            if (ORAS && radBattleResort.Checked)
                            {
                                Program.helper.quickbuton(LookupTable.DPadRight,
                                    walkingTime);
                            }
                            else
                            {
                                Program.helper.quickbuton(LookupTable.DPadLeft,
                                    walkingTime);
                            }
                            await Task.Delay(walkingTime + shortDelay);
                            botState = BotState.CheckMap2;
                            break;

                        case BotState.EnterDayCare:
                            Report("Bot: Enter to Day Care");
                            await Task.Delay(shortDelay);
                            Program.helper.quickbuton(LookupTable.RunUp, longDelay);
                            await Task.Delay(longDelay + shortDelay);
                            botState = BotState.CheckMap3;
                            break;

                        case BotState.CheckMap3:
                            waitForBool = Program.helper.timememoryinrange(mapIdOffset,
                                daycareMapId, 0x01, 100, 5000);
                            if (await waitForBool)
                            {
                                attempts = 0;
                                botState = BotState.WalkToDesk;
                            }
                            else
                            {
                                attempts++;
                                botResult = ErrorMessage.ReadError;
                                botState = BotState.EnterDayCare;
                            }
                            break;

                        case BotState.WalkToDesk:
                            Report("Bot: Run to desk");
                            await Task.Delay(shortDelay);
                            Program.helper.quickbuton(LookupTable.RunUp, longDelay);
                            await Task.Delay(longDelay + shortDelay);
                            botState = BotState.CheckMap4;
                            break;

                        case BotState.CheckMap4:
                            waitForBool = Program.helper.timememoryinrange(mapYOffset,
                                pcYPosition, 0x01, 100, 5000);
                            if (await waitForBool)
                            {
                                attempts = 0;
                                botState = BotState.WalkToPC;
                            }
                            else
                            {
                                attempts++;
                                botResult = ErrorMessage.ReadError;
                                botState = BotState.WalkToDesk;
                            }
                            break;

                        case BotState.WalkToPC:
                            Report("Bot: Walk to the PC");
                            await Task.Delay(shortDelay);
                            Program.helper.quickbuton(LookupTable.DPadRight, walkingTime);
                            await Task.Delay(walkingTime + shortDelay);
                            botState = BotState.CheckMap5;
                            break;

                        case BotState.CheckMap5:
                            waitForBool = Program.helper.memoryinrange(mapXOffset,
                                pcXPosition, 0x01);
                            if (await waitForBool)
                            {
                                attempts = 0;
                                botState = BotState.TurnToPC;
                            }
                            else if (Program.helper.lastRead > pcXPosition)
                            {
                                attempts++;
                                botResult = ErrorMessage.GeneralError;
                                botState = BotState.FixPosition2;
                            }
                            else
                            { // Still far from computer
                                attempts++;
                                botResult = ErrorMessage.GeneralError;
                                botState = BotState.WalkToPC;
                            }
                            break;

                        case BotState.FixPosition2:
                            Report("Bot: Missed PC, return");
                            await Task.Delay(shortDelay);
                            Program.helper.quickbuton(LookupTable.DPadLeft, walkingTime);
                            await Task.Delay(walkingTime + shortDelay);
                            botState = BotState.CheckMap5;
                            break;

                        case BotState.TurnToPC:
                            Report("Bot: Turn towards the PC");
                            await Task.Delay(shortDelay);
                            waitForBool = Program.helper.waitbutton(LookupTable.DPadUp);
                            if (await waitForBool)
                            {
                                attempts = 0;
                                botState = BotState.StartPC;
                            }
                            else
                            {
                                attempts++;
                                botResult = ErrorMessage.ButtonError;
                                botState = BotState.TurnToPC;
                            }
                            break;

                        case BotState.StartPC:
                            Report("Bot: Turn on the PC");
                            waitForBool = Program.helper.waitbutton(LookupTable.ButtonA);
                            if (await waitForBool)
                            {
                                botState = BotState.TestPC;
                            }
                            else
                            {
                                attempts++;
                                botResult = ErrorMessage.ButtonError;
                                botState = BotState.StartPC;
                            }
                            break;

                        case BotState.TestPC:
                            Report("Bot: Test if the PC is on");
                            await Task.Delay(4 * shortDelay); // Wait for PC on
                            waitForBool = Program.helper.timememoryinrange(pcMenuOffset,
                                pcMenuIn, 0x10000, 100, 5000);
                            if (await waitForBool)
                            {
                                attempts = 0;
                                botState = BotState.PCDialog;
                            }
                            else
                            {
                                attempts++;
                                botResult = ErrorMessage.ReadError;
                                botState = BotState.TurnToPC;
                            }
                            break;

                        case BotState.PCDialog:
                            Report("Bot: Skip PC dialog");
                            await Task.Delay(shortDelay);
                            waitForBool = Program.helper.waitbutton(LookupTable.ButtonA);
                            if (await waitForBool)
                            {
                                attempts = 0;
                                botState = BotState.SelectStorage;
                            }
                            else
                            {
                                botResult = ErrorMessage.ButtonError;
                                Report("Bot: Error detected");
                                attempts = 11;
                            }
                            break;

                        case BotState.SelectStorage:
                            Report("Bot: Press Access PC storage");
                            await Task.Delay(shortDelay);
                            waitForBool = Program.helper.waitbutton(LookupTable.ButtonA);
                            if (await waitForBool)
                            {
                                attempts = 0;
                                botState = BotState.SelectOrganize;
                            }
                            else
                            {
                                botResult = ErrorMessage.ButtonError;
                                Report("Bot: Error detected");
                                attempts = 11;
                            }
                            break;

                        case BotState.SelectOrganize:
                            Report("Bot: Touch Organize boxes");
                            await Task.Delay(shortDelay);
                            if (ORAS && radOrganizeTop.Checked)
                            {
                                waitForBool = Program.helper.waittouch(160, 40);
                            }
                            else
                            {
                                waitForBool = Program.helper.waittouch(160, 120);
                            }
                            if (await waitForBool)
                            {
                                botState = BotState.TestStorage;
                            }
                            else
                            {
                                attempts++;
                                botResult = ErrorMessage.TouchError;
                                botState = BotState.SelectOrganize;
                            }
                            break;

                        case BotState.TestStorage:
                            Report("Test if the boxes are shown");
                            await Task.Delay(2 * shortDelay);
                            waitForBool = Program.helper.timememoryinrange
                                (storageViewOffset, storageViewIn, 0x10000, 100, 5000);
                            if (await waitForBool)
                            {
                                attempts = 0;
                                botState = BotState.ReadSlot;
                            }
                            else
                            {
                                attempts++;
                                botResult = ErrorMessage.ReadError;
                                botState = BotState.SelectOrganize;
                            }
                            break;

                        case BotState.ReadSlot:
                            Report("Bot: Search for empty slot");
                            waitForPKM = Program.helper.waitPokeRead(numBox, numSlot);
                            currentEgg = await waitForPKM;
                            if (currentEgg == null)
                            { // No data or invalid
                                attempts++;
                                botResult = ErrorMessage.ReadError;
                                botState = BotState.ReadSlot;
                            }
                            else if (currentEgg.Species == 0)
                            { // Empty space
                                Report("Bot: Empty slot found");
                                attempts = 0;
                                botState = BotState.TestBoxChange;
                            }
                            else
                            {
                                GetNextSlot();
                                botState = BotState.ReadSlot;
                            }
                            break;

                        case BotState.TestBoxChange:
                            if (changeBox)
                            {
                                botState = BotState.SelectBoxView;
                                changeBox = false;
                            }
                            else
                            {
                                botState = BotState.SelectEgg;
                            }
                            break;

                        case BotState.SelectBoxView:
                            Report("Bot: Touch Box View");
                            await Task.Delay(shortDelay);
                            waitForBool = Program.helper.waittouch(30, 220);
                            if (await waitForBool)
                            {
                                botState = BotState.TestBoxView;
                            }
                            else
                            {
                                attempts++;
                                botResult = ErrorMessage.TouchError;
                                botState = BotState.SelectBoxView;
                            }
                            break;

                        case BotState.TestBoxView:
                            Report("Bot: Test if box view is shown");
                            await Task.Delay(2 * shortDelay);
                            waitForBool = Program.helper.timememoryinrange(boxViewOffset,
                                boxViewIn, boxViewRange, 100, 5000);
                            if (await waitForBool)
                            {
                                attempts = 0;
                                botState = BotState.TouchNewBox;
                            }
                            else
                            {
                                attempts++;
                                botResult = ErrorMessage.ReadError;
                                botState = BotState.SelectBoxView;
                            }
                            break;

                        case BotState.TouchNewBox:
                            Report("Bot: Touch new box");
                            await Task.Delay(shortDelay);
                            waitForBool = Program.helper.waittouch(
                                LookupTable.boxposX6[GetIndex(numBox)],
                                LookupTable.boxposY6[GetIndex(numBox)]);
                            if (await waitForBool)
                            {
                                attempts = 0;
                                botState = BotState.SelectNewBox;
                            }
                            else
                            {
                                attempts++;
                                botResult = ErrorMessage.TouchError;
                                botState = BotState.TouchNewBox;
                            }
                            break;

                        case BotState.SelectNewBox:
                            Report("Bot: Select new box");
                            await Task.Delay(shortDelay);
                            waitForBool = Program.helper.waitbutton(LookupTable.ButtonA);
                            if (await waitForBool)
                            {
                                botState = BotState.TestReturnStorage;
                            }
                            else
                            {
                                attempts++;
                                botResult = ErrorMessage.ButtonError;
                                botState = BotState.SelectNewBox;
                            }
                            break;

                        case BotState.TestReturnStorage:
                            Report("Bot: Test if box view is not shown");
                            await Task.Delay(2 * shortDelay);
                            waitForBool = Program.helper.timememoryinrange(boxViewOffset,
                                boxViewOut, boxViewRange, 100, 5000);
                            if (await waitForBool)
                            {
                                attempts = 0;
                                botState = BotState.SelectEgg;
                            }
                            else
                            {
                                attempts++;
                                botResult = ErrorMessage.ReadError;
                                botState = BotState.SelectNewBox;
                            }
                            break;

                        case BotState.SelectEgg:
                            Report("Bot: Select Egg");
                            await Task.Delay(shortDelay);
                            waitForBool = Program.helper.waitholdtouch(300, 100);
                            if (await waitForBool)
                            {
                                botState = BotState.MoveEgg;
                            }
                            else
                            {
                                botResult = ErrorMessage.TouchError;
                                Report("Bot: Error detected");
                                attempts = 11;
                            }
                            break;

                        case BotState.MoveEgg:
                            Report("Move Egg");
                            await Task.Delay(shortDelay);
                            waitForBool = Program.helper.waitholdtouch(
                                LookupTable.pokeposX6[GetIndex(numSlot)],
                                LookupTable.pokeposY6[GetIndex(numSlot)]);
                            if (await waitForBool)
                            {
                                botState = BotState.ReleaseEgg;
                            }
                            else
                            {
                                botResult = ErrorMessage.TouchError;
                                Report("Bot: Error detected");
                                attempts = 11;
                            }
                            break;

                        case BotState.ReleaseEgg:
                            Report("Bot: Release Egg");
                            await Task.Delay(shortDelay);
                            waitForBool = Program.helper.waitfreetouch();
                            if (await waitForBool)
                            {
                                eggLocations[eggsInBatch, 0] = numBox.Value;
                                eggLocations[eggsInBatch, 1] = numSlot.Value;
                                eggsInBatch++;
                                GetNextSlot();
                                eggsInParty--;
                                if (eggsInParty > 0)
                                {
                                    botState = BotState.ReadSlot;
                                }
                                else
                                {
                                    botState = BotState.ExitPC;
                                }
                            }
                            else
                            {
                                botResult = ErrorMessage.TouchError;
                                Report("Bot: Error detected");
                                attempts = 11;
                            }
                            break;

                        case BotState.ExitPC:
                            Report("Bot: Exit from PC");
                            await Task.Delay(shortDelay);
                            waitForBool = Program.helper.waitbutton(LookupTable.ButtonX);
                            if (await waitForBool)
                            {
                                attempts = 0;
                                botState = BotState.TestExitPC;
                            }
                            else
                            {
                                attempts++;
                                botResult = ErrorMessage.ButtonError;
                                botState = BotState.ExitPC;
                            }
                            break;

                        case BotState.TestExitPC:
                            Report("Bot: Test if out from PC");
                            waitForBool = Program.helper.timememoryinrange(
                                storageViewOffset, storageViewOut, 0x10000, 100, 5000);
                            if (await waitForBool)
                            {
                                attempts = 0;
                                if (cmbMode.SelectedIndex == 1 ||
                                    cmbMode.SelectedIndex == 2 || chkReadESV.Checked)
                                {
                                    botState = BotState.FilterTest;
                                }
                                else if (numEggs.Value > 0)
                                {
                                    botState = BotState.RetireFromPC;
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
                                botResult = ErrorMessage.ReadError;
                                botState = BotState.ExitPC;
                            }
                            break;

                        case BotState.RetireFromPC:
                            Report("Bot: Retire from PC");
                            await Task.Delay(shortDelay);
                            eggsInBatch = 0;
                            Program.helper.quickbuton(LookupTable.DPadLeft, walkingTime);
                            await Task.Delay(walkingTime + shortDelay);
                            botState = BotState.CheckMap6;
                            break;

                        case BotState.CheckMap6:
                            waitForBool = Program.helper.memoryinrange(mapXOffset,
                                daycareExitXPosition, 0x01);
                            if (await waitForBool)
                            {
                                attempts = 0;
                                botState = BotState.RetireFromDesk;
                            }
                            else if (Program.helper.lastRead < daycareExitXPosition)
                            {
                                attempts++;
                                botResult = ErrorMessage.GeneralError;
                                botState = BotState.FixPosition3;
                            }
                            else
                            { // Still far from exit
                                attempts++;
                                botResult = ErrorMessage.GeneralError;
                                botState = BotState.RetireFromPC;
                            }
                            break;

                        case BotState.FixPosition3:
                            Report("Bot: Missed exit, return");
                            await Task.Delay(shortDelay);
                            Program.helper.quickbuton(LookupTable.DPadRight, walkingTime);
                            await Task.Delay(walkingTime + shortDelay);
                            botState = BotState.CheckMap6;
                            break;

                        case BotState.RetireFromDesk:
                            Report("Bot: Run to exit");
                            await Task.Delay(shortDelay);
                            Program.helper.quickbuton(LookupTable.RunDown, longDelay);
                            await Task.Delay(longDelay + shortDelay);
                            botState = BotState.CheckMap7;
                            break;

                        case BotState.CheckMap7:
                            waitForBool = Program.helper.timememoryinrange(mapIdOffset,
                                routeMapId, 0x01, 100, 5000);
                            if (await waitForBool)
                            {
                                attempts = 0;
                                botState = BotState.RetireFromDoor;
                            }
                            else
                            {
                                attempts++;
                                botResult = ErrorMessage.ReadError;
                                botState = BotState.RetireFromDesk;
                            }
                            break;

                        case BotState.RetireFromDoor:
                            Report("Bot: Retire from door");
                            await Task.Delay(shortDelay);
                            Program.helper.quickbuton(LookupTable.DPadDown, walkingTime);
                            await Task.Delay(walkingTime + shortDelay);
                            botState = BotState.CheckMap8;
                            break;

                        case BotState.CheckMap8:
                            waitForBool = Program.helper.memoryinrange(mapYOffset,
                                daycareManYPosition, 0x01);
                            if (await waitForBool)
                            {
                                attempts = -10; // Allow more attempts
                                botState = BotState.WalkToDayCareMan;
                            }
                            else if (Program.helper.lastRead > daycareManYPosition)
                            {
                                attempts++;
                                botResult = ErrorMessage.GeneralError;
                                botState = BotState.FixPosition5;
                            }
                            else
                            { // Still far from exit
                                attempts++;
                                botResult = ErrorMessage.GeneralError;
                                botState = BotState.RetireFromDoor;
                            }
                            break;

                        case BotState.FixPosition5:
                            Report("Bot: Missed Day Care Man, return");
                            await Task.Delay(shortDelay);
                            Program.helper.quickbuton(LookupTable.DPadUp, walkingTime);
                            await Task.Delay(walkingTime + shortDelay);
                            botState = BotState.CheckMap8;
                            break;

                        case BotState.WalkToDayCareMan:
                            Report("Bot: Walk to Day Care Man");
                            await Task.Delay(shortDelay);
                            if (ORAS && radBattleResort.Checked)
                            {
                                Program.helper.quickbuton(LookupTable.DPadRight,
                                    walkingTime);
                            }
                            else
                            {
                                Program.helper.quickbuton(LookupTable.DPadLeft,
                                    walkingTime);
                            }
                            await Task.Delay(walkingTime + shortDelay);
                            botState = BotState.CheckMap9;
                            break;

                        case BotState.CheckMap9:
                            waitForBool = Program.helper.memoryinrange(mapXOffset,
                                daycareManXPosition, 0x01);
                            if (await waitForBool)
                            {
                                if (chkQuickBreed.Checked)
                                {
                                    botState = BotState.TurnToDayCareMan;
                                }
                                else
                                {
                                    botState = BotState.Walk1;
                                }
                                attempts = 0;
                            }
                            else if (Program.helper.lastRead > daycareManXPosition &&
                                ORAS && radBattleResort.Checked)
                            {
                                attempts++;
                                botResult = ErrorMessage.GeneralError;
                                botState = BotState.FixPosition4;
                            }
                            else if (Program.helper.lastRead > daycareManXPosition &&
                                (!ORAS || radRoute117.Checked))
                            {
                                attempts++;
                                botResult = ErrorMessage.GeneralError;
                                botState = BotState.FixPosition4;
                            }
                            else
                            {
                                attempts++;
                                botResult = ErrorMessage.GeneralError;
                                botState = BotState.WalkToDayCareMan;
                            }
                            break;

                        case BotState.FixPosition4:
                            Report("Bot: Missed Day Care Man, return");
                            await Task.Delay(shortDelay);
                            if (ORAS && radBattleResort.Checked)
                            {
                                Program.helper.quickbuton(LookupTable.DPadRight,
                                    walkingTime);
                            }
                            else
                            {
                                Program.helper.quickbuton(LookupTable.DPadLeft,
                                    walkingTime);
                            }
                            await Task.Delay(walkingTime + shortDelay);
                            botState = BotState.CheckMap9;
                            break;

                        case BotState.FilterTest:
                            for (i = 0; i < eggsInBatch; i++)
                            {
                                if (attempts > 10)
                                {
                                    break;
                                }
                                Delg.SetValue(numBox, eggLocations[i, 0]);
                                Delg.SetValue(numSlot, eggLocations[i, 1]);
                                bool testsok = false;
                                Report("Bot: Check deposited egg");
                                waitForPKM = Program.helper.waitPokeRead(numBox, numSlot);
                                currentEgg = await waitForPKM;
                                if (currentEgg == null)
                                { // No data or invalid
                                    attempts++;
                                    botResult = ErrorMessage.ReadError;
                                    i--;
                                    attempts++;
                                    continue;
                                }
                                else if (currentEgg.Species == 0)
                                { // Empty space
                                    Report("Bot: Error detected - slot is empty");
                                    attempts = 11;
                                    botResult = ErrorMessage.GeneralError;
                                    botState = BotState.ExitBot;
                                    break;
                                }
                                else
                                {
                                    attempts = 0;
                                    if (chkReadESV.Checked || cmbMode.SelectedIndex == 2)
                                    {
                                        Delg.DataGridViewAddRow(dgvEggs, numBox.Value,
                                            numSlot.Value, currentEgg.PSV.ToString("D4"));
                                        if (cmbMode.SelectedIndex == 2)
                                        {
                                            testsok = CheckShinyMatch(currentEgg.PSV);
                                        }
                                    }
                                    if (cmbMode.SelectedIndex == 1)
                                    {
                                        matchingFilter = CheckFilters(currentEgg,
                                            dgvFilters);
                                        testsok = matchingFilter > 0;
                                    }
                                }
                                if (testsok)
                                {
                                    botState = BotState.AllTestsPassed;
                                    break;
                                }
                                else if (numEggs.Value > 0)
                                {
                                    botState = BotState.RetireFromPC;
                                }
                                else
                                {
                                    if (cmbMode.SelectedIndex == 1 ||
                                        cmbMode.SelectedIndex == 2)
                                    {
                                        Report("Bot: No match found");
                                        botResult = ErrorMessage.NoMatch;
                                    }
                                    else
                                    {
                                        botResult = ErrorMessage.Finished;
                                    }
                                    botState = BotState.ExitBot;
                                }
                            }
                            break;

                        case BotState.AllTestsPassed:
                            if (cmbMode.SelectedIndex == 1)
                            {
                                Report("Bot: All tests passed");
                                botResult = ErrorMessage.FilterMatch;
                                finishInformation[0] = (int)numBox.Value;
                                finishInformation[1] = (int)numSlot.Value;
                                finishInformation[2] = matchingFilter;
                            }
                            else if (cmbMode.SelectedIndex == 2)
                            {
                                Report("Bot: ESV/TSV match found");
                                botResult = ErrorMessage.SVMatch;
                                finishInformation[0] = (int)numBox.Value;
                                finishInformation[1] = (int)numSlot.Value;
                                finishInformation[2] = currentEgg.PSV;
                            }
                            botState = BotState.ExitBot;
                            break;

                        case BotState.ExitBot:
                            Report("Bot: STOP Gen 6 Breding bot");
                            botWorking = false;
                            break;

                        default:
                            Report("Bot: STOP Gen 6 Breding bot");
                            botResult = ErrorMessage.GeneralError;
                            botWorking = false;
                            break;
                    }
                    if (attempts > 10)
                    { // Too many attempts
                        if (maxReconnections > 0)
                        {
                            Report("Bot: Try reconnection to fix error");
                            waitForBool = Program.gCmdWindow.Reconnect();
                            maxReconnections--;
                            if (await waitForBool)
                            {
                                await Task.Delay(5 * shortDelay);
                                attempts = 0;
                            }
                            else
                            {
                                botResult = ErrorMessage.GeneralError;
                                botWorking = false;
                            }
                        }
                        else
                        {
                            Report("Bot: Maximum number of reconnection attempts " +
                                "reached");
                            Report("Bot: STOP Gen 6 Breeding bot");
                            botWorking = false;
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
                MessageBox.Show(ex.Message, botName);
                botWorking = false;
                botResult = ErrorMessage.GeneralError;
            }
            if (userStop)
            {
                botResult = ErrorMessage.UserStop;
            }
            else if (!Program.gCmdWindow.IsConnected)
            {
                botResult = ErrorMessage.Disconnect;
            }
            ShowResult("Breeding bot", botResult, finishInformation);
            Delg.SetText(btnRunStop, "Start Bot");
            Program.gCmdWindow.SetBotMode(false);
            SetControls(true);
            Delg.SetEnabled(btnRunStop, true);
        }

        /// <summary>
        /// Auxiliar method to increase the count of eggs in the party.
        /// </summary>
        private void AddEggtoParty()
        {
            eggsInParty++;
            Delg.SetValue(numEggs, numEggs.Value - 1);
        }

        /// <summary>
        /// Sets the reference to the next slot in the PC.
        /// </summary>
        private void GetNextSlot()
        {
            if (numSlot.Value == 30)
            {
                Delg.SetValue(numBox, numBox.Value + 1);
                Delg.SetValue(numSlot, 1);
                changeBox = true;
            }
            else
            {
                Delg.SetValue(numSlot, numSlot.Value + 1);
            }
        }

        /// <summary>
        /// Compares a given ESV to the TSV list.
        /// </summary>
        /// <param name="esv"></param>
        /// <returns>Returns true if the ESV matches any of the TSV values in the 
        /// list.</returns>
        public bool CheckShinyMatch(int esv)
        {
            if (lstTSV.Items.Count > 0)
            {
                Report("Filter: Checking egg with ESV: " + esv);
                foreach (var tsv in lstTSV.Items)
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

        /// <summary>
        /// Sets the maximum number of eggs possible based on the selected box and slot.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetMaximumEggs(object sender, EventArgs e)
        {
            Delg.SetMaximum(numEggs,
                LookupTable.GetRemainingSpaces((int)numBox.Value, (int)numSlot.Value));
        }

        /// <summary>
        /// Saves the Egg list in the bot folder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveESVList(object sender, EventArgs e)
        {
            try
            {
                if (dgvEggs.Rows.Count > 0)
                {
                    Directory.CreateDirectory(BotFolder);
                    string fileName = Path.Combine(BotFolder, "ESVlist6.csv");
                    var esvList = new StringBuilder();
                    var dgvHeaders = dgvEggs.Columns.Cast<DataGridViewColumn>();
                    esvList.AppendLine(string.Join(",",
                        dgvHeaders.Select(column => column.HeaderText).ToArray()));
                    foreach (DataGridViewRow row in dgvEggs.Rows)
                    {
                        var cells = row.Cells.Cast<DataGridViewCell>();
                        esvList.AppendLine(string.Join(",",
                            cells.Select(cell => cell.Value).ToArray()));
                    }
                    File.WriteAllText(fileName, esvList.ToString());
                    MessageBox.Show("Egg list saved.", botName);
                }
                else
                {
                    MessageBox.Show("There are no eggs on the ESV list", botName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, botName);
            }
        }

        /// <summary>
        /// Add the value selected in numTSV to the TSV list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddTSV(object sender, EventArgs e)
        {
            lstTSV.Items.Add(((int)numTSV.Value).ToString("D4"));
        }

        /// <summary>
        /// Removes the selected TSV from the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveTSV(object sender, EventArgs e)
        {
            if (lstTSV.SelectedIndices.Count > 0)
            {
                lstTSV.Items.RemoveAt(lstTSV.SelectedIndices[0]);
            }
            else
            {
                MessageBox.Show("No TSV selected for remove", botName);
            }
        }

        /// <summary>
        /// Saves the TSV list in the bot folder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveTSVList(object sender, EventArgs e)
        {
            try
            {
                if (lstTSV.Items.Count > 0)
                {
                    Directory.CreateDirectory(BotFolder);
                    string fileName = Path.Combine(BotFolder, "TSVlist6.csv");
                    var tsvlst = new StringBuilder();
                    foreach (var value in lstTSV.Items)
                    {
                        tsvlst.AppendLine(value.ToString());
                    }
                    File.WriteAllText(fileName, tsvlst.ToString());
                    MessageBox.Show("TSV list saved.", botName);
                }
                else
                {
                    MessageBox.Show("There are no TSV values in the list.", botName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, botName);
            }
        }

        /// <summary>
        /// Loads a TSV list from the bot folder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadTSVList(object sender, EventArgs e)
        {
            try
            {
                Directory.CreateDirectory(BotFolder);
                string fileName = Path.Combine(BotFolder, "TSVlist6.csv");
                if (File.Exists(fileName))
                {
                    string[] values = File.ReadAllLines(fileName);
                    lstTSV.Items.Clear();
                    lstTSV.Items.AddRange(values);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, botName);
            }
        }

        /// <summary>
        /// Clears all lists and puts settings at default.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetForm(object sender, EventArgs e)
        {
            Delg.SetSelectedIndex(cmbMode, -1);
            Delg.SetValue(numBox, 1);
            Delg.SetValue(numSlot, 1);
            Delg.SetValue(numEggs, 1);
            Delg.SetCheckedRadio(radOrganizeMiddle, true);
            Delg.SetCheckedRadio(radRoute117, true);
            Delg.SetChecked(chkReadESV, false);
            Delg.SetChecked(chkQuickBreed, false);
            dgvEggs.Rows.Clear();
            lstTSV.Items.Clear();
            dgvFilters.Rows.Clear();
        }

        /// <summary>
        /// Load a filter set.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadFilterList(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog()
                {
                    Filter = "PKMN-NTR Filter|*.pftr",
                    Title = "Select a filter set",
                    InitialDirectory = BotFolder
                };
                if (openFileDialog1.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                dgvFilters.Rows.Clear();
                List<int[]> rows = File.ReadAllLines(openFileDialog1.FileName)
                    .Select(s => s.Split(new[] { "," },
                    StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)
                    .ToArray()).ToList();
                foreach (int[] row in rows)
                {
                    dgvFilters.Rows.Add(row[0], row[1], row[2], row[3], row[4], row[5],
                        row[6], row[7], row[8], row[9], row[10], row[11], row[12],
                        row[13], row[14], row[15], row[16], row[17], row[18]);
                }
                MessageBox.Show("Filter Set loaded correctly.", botName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, botName);
            }
        }

        /// <summary>
        /// Prevents user from closing the form while a bot is running.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClosingForm(object sender, FormClosingEventArgs e)
        {
            if (botWorking)
            {
                MessageBox.Show("Stop the bot before closing this window.",
                    botName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Enables controls in the Main Form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseForm(object sender, FormClosedEventArgs e)
        {
            Program.gCmdWindow.Tool_Finish();
        }
    }
}
