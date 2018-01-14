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

using PKHeX.Core;
using System.IO;
using System.Windows.Forms;

namespace pkmn_ntr.Bot
{
    /// <summary>
    /// Includes common methods for all bots.
    /// </summary>
    public static class Bot
    {
        /// <summary>
        /// All different error or finisish messages the bot can return.
        /// </summary>
        public enum ErrorMessage
        {
            Finished, UserStop, ReadError, WriteError, ButtonError, TouchError,
            StickError, NotInPSS, FestivalPlaza, SVMatch, FilterMatch, NoMatch, SRMatch,
            BattleMatch, Disconnect, NotWTMenu, GeneralError
        };

        /// <summary>
        /// Reference for storing bot-related files.
        /// </summary>
        public static string BotFolder
        {
            get
            {
                return Path.Combine(Application.StartupPath, "Bot");
            }
        }

        /// <summary>
        /// Write data to the log.
        /// </summary>
        /// <param name="message">String which will be added to the log</param>
        public static void Report(string message)
        {
            Program.gCmdWindow.AddToLog(message);
        }

        /// <summary>
        /// Check if a pokémon is legal,
        /// </summary>
        /// <param name="poke">PKM data to check.</param>
        /// <returns>Returns true if the pokémon is legal or if the program is in illegal
        /// mode. Returns false otherwise.</returns>
        public static bool IsLegal(PKM poke)
        {
            if (Program.gCmdWindow.HaX)
            {
                Report("Bot: Illegal mode enabled, skip check");
                return true;
            }

            LegalityAnalysis Legal = new LegalityAnalysis(poke);
            if (Legal.Parsed)
            {
                return Legal.Valid;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Check if a pokémon can be traded via Wonder Trade, checks legality, 
        /// egg and ribbons.
        /// </summary>
        /// <param name="poke">PKM data to check.</param>
        /// <returns>Returns false if the pokémon is an egg or has special ribbons. It 
        /// also returns false if the pokémon is illegal, except when the program is in
        /// illegal mode.</returns>
        public static bool IsTradeable(PKM poke)
        {
            if (!IsLegal(poke))
            { // Don't trade illegal pokemon
                return false;
            }

            if (poke.IsEgg)
            { // Don't trade eggs
                return false;
            }

            if (poke.Format == 6)
            {
                var poke6 = new PK6(poke.Data);
                if (poke6.RibbonCountry || poke6.RibbonWorld || poke6.RibbonClassic ||
                    poke6.RibbonPremier || poke6.RibbonEvent || poke6.RibbonBirthday ||
                    poke6.RibbonSpecial || poke6.RibbonSouvenir || poke6.RibbonWishing ||
                    poke6.RibbonChampionBattle || poke6.RibbonChampionRegional ||
                    poke6.RibbonChampionNational || poke6.RibbonChampionWorld)
                { // Check for Special Ribbons
                    return false;
                }
            }
            if (poke.Format == 7)
            {
                var poke7 = new PK7(poke.Data);
                if (poke7.RibbonCountry || poke7.RibbonWorld || poke7.RibbonClassic ||
                    poke7.RibbonPremier || poke7.RibbonEvent || poke7.RibbonBirthday ||
                    poke7.RibbonSpecial || poke7.RibbonSouvenir || poke7.RibbonWishing ||
                    poke7.RibbonChampionBattle || poke7.RibbonChampionRegional ||
                    poke7.RibbonChampionNational || poke7.RibbonChampionWorld)
                { // Check for Special Ribbons
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Gets the RAM offset of a pokémon in the PC.
        /// </summary>
        /// <param name="startOffset">Offset of the first pokémon in the PC.</param>
        /// <param name="boxSource">Box reference.</param>
        /// <param name="slotSource">Slot reference.</param>
        /// <returns>Returns an unsigned integer with the RAM address of the selected PC 
        /// slot</returns>
        public static uint GetBoxOffset(uint startOffset, NumericUpDown boxSource,
            NumericUpDown slotSource)
        {
            return startOffset + (uint)(boxSource.Value - 1) * 30 * 232 +
                (uint)(slotSource.Value - 1) * 232;
        }

        /// <summary>
        /// Gets the zero-indexed value of a NumpericUpDown field.
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns>Returns the value of the NumericUpDown minus one.</returns>
        public static int GetIndex(NumericUpDown ctrl)
        {
            return (int)ctrl.Value - 1;
        }

        /// <summary>
        /// Compare a pokémon against a list of filters.</summary>
        /// <param name="poke">PKM data to compare.</param>
        /// <param name="filters">Filter list.</param>
        /// <returns>If the pokémon passes all the tests of one of the filters this
        /// method returns the filter position in the list. If no match is found it
        /// returns -1.</returns>
        public static int CheckFilters(PKM poke, DataGridView filters)
        {
            int currentFilter;
            int failedTests;
            int perfectIVs;
            if (filters.Rows.Count > 0)
            {
                currentFilter = 0;
                foreach (DataGridViewRow row in filters.Rows)
                {
                    currentFilter++;
                    Report("\r\nFilter: Analyze pokémon using filter # " + currentFilter);
                    failedTests = 0;
                    perfectIVs = 0;
                    // Test shiny
                    if ((int)row.Cells[0].Value == 1)
                    {
                        if (poke.IsShiny)
                        {
                            Report("Filter: Shiny - PASS");
                        }
                        else
                        {
                            Report("Filter: Shiny - FAIL");
                            failedTests++;
                        }
                    }
                    else
                    {
                        Report("Filter: Shiny - Don't care");
                    }

                    // Test nature
                    if ((int)row.Cells[1].Value < 0 || poke.Nature ==
                        (int)row.Cells[1].Value)
                    {
                        Report("Filter: Nature - PASS");
                    }
                    else
                    {
                        Report("Filter: Nature - FAIL");
                        failedTests++;
                    }

                    // Test Ability
                    if ((int)row.Cells[2].Value < 0 || (poke.Ability - 1) ==
                        (int)row.Cells[2].Value)
                    {
                        Report("Filter: Ability - PASS");
                    }
                    else
                    {
                        Report("Filter: Ability - FAIL");
                        failedTests++;
                    }

                    // Test Hidden Power
                    if ((int)row.Cells[3].Value < 0 || poke.HPType ==
                        (int)row.Cells[3].Value)
                    {
                        Report("Filter: Hidden Power - PASS");
                    }
                    else
                    {
                        Report("Filter: Hidden Power - FAIL");
                        failedTests++;
                    }

                    // Test Gender
                    if ((int)row.Cells[4].Value < 0 || (int)row.Cells[4].Value ==
                        poke.Gender)
                    {
                        Report("Filter: Gender - PASS");
                    }
                    else
                    {
                        Report("Filter: Gender - FAIL");
                        failedTests++;
                    }

                    // Test HP
                    if (IVCheck((int)row.Cells[5].Value, poke.IV_HP,
                        (int)row.Cells[6].Value))
                    {
                        Report("Filter: Hit Points IV - PASS");
                    }
                    else
                    {
                        Report("Filter: Hit Points IV - FAIL");
                        failedTests++;
                    }
                    if (poke.IV_HP == 31)
                    {
                        perfectIVs++;
                    }

                    // Test Atk
                    if (IVCheck((int)row.Cells[7].Value, poke.IV_ATK,
                        (int)row.Cells[8].Value))
                    {
                        Report("Filter: Attack IV - PASS");
                    }
                    else
                    {
                        Report("Filter: Attack IV - FAIL");
                        failedTests++;
                    }
                    if (poke.IV_ATK == 31)
                    {
                        perfectIVs++;
                    }

                    // Test Def
                    if (IVCheck((int)row.Cells[9].Value, poke.IV_DEF,
                        (int)row.Cells[10].Value))
                    {
                        Report("Filter: Defense IV - PASS");
                    }
                    else
                    {
                        Report("Filter: Defense IV - FAIL");
                        failedTests++;
                    }
                    if (poke.IV_DEF == 31)
                    {
                        perfectIVs++;
                    }

                    // Test SpA
                    if (IVCheck((int)row.Cells[11].Value, poke.IV_SPA,
                        (int)row.Cells[12].Value))
                    {
                        Report("Filter: Special Attack IV - PASS");
                    }
                    else
                    {
                        Report("Filter: Special Attack IV - FAIL");
                        failedTests++;
                    }
                    if (poke.IV_SPA == 31)
                    {
                        perfectIVs++;
                    }

                    // Test SpD
                    if (IVCheck((int)row.Cells[13].Value, poke.IV_SPD,
                        (int)row.Cells[14].Value))
                    {
                        Report("Filter: Special Defense IV - PASS");
                    }
                    else
                    {
                        Report("Filter: Special Defense IV - FAIL");
                        failedTests++;
                    }
                    if (poke.IV_SPD == 31)
                    {
                        perfectIVs++;
                    }

                    // Test Spe
                    if (IVCheck((int)row.Cells[15].Value, poke.IV_SPE,
                        (int)row.Cells[16].Value))
                    {
                        Report("Filter: Speed IV - PASS");
                    }
                    else
                    {
                        Report("Filter: Speed IV - FAIL");
                        failedTests++;
                    }
                    if (poke.IV_SPE == 31)
                    {
                        perfectIVs++;
                    }

                    // Test Perfect IVs
                    if (IVCheck((int)row.Cells[17].Value, perfectIVs,
                        (int)row.Cells[18].Value))
                    {
                        Report("Filter: Perfect IVs - PASS");
                    }
                    else
                    {
                        Report("Filter: Perfect IVs - FAIL");
                        failedTests++;
                    }
                    if (failedTests == 0)
                    {
                        return currentFilter;
                    }
                }
                return -1;
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// Compares a numeric value againt other using a determined logic.
        /// </summary>
        /// <param name="targetNumber">Number reference.</param>
        /// <param name="sourceNumber">Number to compare.</param>
        /// <param name="logic">Logic to be applied.</param>
        /// <returns>Returns true if the target number passes the test.</returns>
        private static bool IVCheck(int targetNumber, int sourceNumber, int logic)
        {
            switch (logic)
            {
                case 0: // Greater or equal
                    return sourceNumber >= targetNumber;
                case 1: // Greater
                    return sourceNumber > targetNumber;
                case 2: // Equal
                    return sourceNumber == targetNumber;
                case 3: // Less
                    return sourceNumber < targetNumber;
                case 4: // Less or equal
                    return sourceNumber <= targetNumber;
                case 5: // Different
                    return sourceNumber != targetNumber;
                case 6: // Even
                    return sourceNumber % 2 == 0;
                case 7: // Odd
                    return sourceNumber % 2 == 1;
                default:
                    return true;
            }
        }

        /// <summary>
        /// Shows a message box with the result of a bot execution.
        /// </summary>
        /// <param name="source">Bot name.</param>
        /// <param name="message">Message to be displayed.</param>
        /// <param name="info">Additional informaiton.</param>
        public static void ShowResult(string source, ErrorMessage message,
            int[] info = null)
        {
            switch (message)
            {
                case ErrorMessage.Finished:
                    MessageBox.Show("Bot finished sucessfully.", source,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case ErrorMessage.UserStop:
                    MessageBox.Show("Bot stopped by the user.", source,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case ErrorMessage.ReadError:
                    MessageBox.Show("A error ocurred while reading data from the " +
                        "3DS RAM.", source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case ErrorMessage.WriteError:
                    MessageBox.Show("A error ocurred while writting data to the 3DS RAM.",
                        source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case ErrorMessage.ButtonError:
                    MessageBox.Show("A error ocurred while sending Button commands to " +
                        "the 3DS.", source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case ErrorMessage.TouchError:
                    MessageBox.Show("A error ocurred while sending Touch Screen " +
                        "commands to the 3DS.", source, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    break;
                case ErrorMessage.StickError:
                    MessageBox.Show("A error ocurred while sending Control Stick " +
                        "commands to the 3DS.", source, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    break;
                case ErrorMessage.NotInPSS:
                    MessageBox.Show("Please go to the PSS menu and try again.", source,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case ErrorMessage.FestivalPlaza:
                    MessageBox.Show("Bot finished due level-up in Festival Plaza.",
                        source, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case ErrorMessage.SVMatch:
                    MessageBox.Show($"Finished. A match was found at box {info[0]}, " +
                        $"slot{info[1]} with the ESV/TSV value: {info[2]}.", source,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case ErrorMessage.FilterMatch:
                    MessageBox.Show($"Finished. A match was found at box {info[0]}, " +
                        $"slot {info[1]} using filter #{info[2]}.", source,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case ErrorMessage.NoMatch:
                    MessageBox.Show("Bot finished sucessfuly without finding a match " +
                        "for the current settings.", source, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    break;
                case ErrorMessage.SRMatch:
                    MessageBox.Show($"Finished. The current pokémon matched filter " +
                        $"#{info[0]} after {info[1]} soft-resets.", source,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case ErrorMessage.BattleMatch:
                    MessageBox.Show($"Finished. The current pokémon matched filter " +
                        $"#{info[0]} after {info[1]} battles.", source,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case ErrorMessage.Disconnect:
                    MessageBox.Show("Connection with the 3DS was lost.", source,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case ErrorMessage.NotWTMenu:
                    MessageBox.Show("Please, go to the Wonder trade screen and try again.",
                        source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case ErrorMessage.GeneralError:
                    MessageBox.Show("A error has ocurred, see log for detals.", source,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                default:
                    MessageBox.Show("An unknown error has ocurred, please keep the " +
                        "log and report this error.", source, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    break;
            }
        }
    }
}
