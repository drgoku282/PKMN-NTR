using PKHeX.Core;
using System.Windows.Forms;

namespace pkmn_ntr.Bot
{
    public static class Bot
    {
        public enum ErrorMessage { Finished, UserStop, ReadError, WriteError, ButtonError, TouchError, StickError, NotInPSS, FestivalPlaza, SVMatch, FilterMatch, NoMatch, SRMatch, BattleMatch, Disconnect, GeneralError };

        public static readonly string FOLDERBOT = "Bot";

        public static void Report(string message)
        {
            Program.gCmdWindow.addtoLog(message);
        }

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
                if (poke6.RibbonCountry || poke6.RibbonWorld ||  poke6.RibbonClassic || poke6.RibbonPremier || poke6.RibbonEvent || poke6.RibbonBirthday || poke6.RibbonSpecial || poke6.RibbonSouvenir || poke6.RibbonWishing || poke6.RibbonChampionBattle || poke6.RibbonChampionRegional || poke6.RibbonChampionNational || poke6.RibbonChampionWorld)
                { // Check for Special Ribbons
                    return false;
                }
            }
            if (poke.Format == 7)
            {
                var poke7 = new PK7(poke.Data);
                if (poke7.RibbonCountry || poke7.RibbonWorld || poke7.RibbonClassic || poke7.RibbonPremier || poke7.RibbonEvent || poke7.RibbonBirthday || poke7.RibbonSpecial || poke7.RibbonSouvenir || poke7.RibbonWishing || poke7.RibbonChampionBattle || poke7.RibbonChampionRegional || poke7.RibbonChampionNational || poke7.RibbonChampionWorld)
                { // Check for Special Ribbons
                    return false;
                }
            }
            return true;
        }

        public static uint GetBoxOff(uint startOff, NumericUpDown boxSource, NumericUpDown slotSource)
        {
            return startOff + (uint)(boxSource.Value - 1) * 30 * 232 + (uint)(slotSource.Value - 1) * 232;
        }

        public static int GetIndex(NumericUpDown ctrl)
        {
            return (int)ctrl.Value - 1;
        }

        public static int CheckFilters(PKM poke, DataGridView filters)
        {
            int currentfilter;
            int failedtests;
            int perfectIVs;
            if (filters.Rows.Count > 0)
            {
                currentfilter = 0;
                foreach (DataGridViewRow row in filters.Rows)
                {
                    currentfilter++;
                    Report("\r\nFilter: Analyze pokémon using filter # " + currentfilter);
                    failedtests = 0;
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
                            Report("Filter: Shiny - FAIL" );
                            failedtests++;
                        }
                    }
                    else
                    {
                        Report("Filter: Shiny - Don't care");
                    }

                    // Test nature
                    if ((int)row.Cells[1].Value < 0 || poke.Nature == (int)row.Cells[1].Value)
                    {
                        Report("Filter: Nature - PASS");
                    }
                    else
                    {
                        Report("Filter: Nature - FAIL");
                        failedtests++;
                    }

                    // Test Ability
                    if ((int)row.Cells[2].Value < 0 || (poke.Ability - 1) == (int)row.Cells[2].Value)
                    {
                        Report("Filter: Ability - PASS");
                    }
                    else
                    {
                        Report("Filter: Ability - FAIL");
                        failedtests++;
                    }

                    // Test Hidden Power
                    if ((int)row.Cells[3].Value < 0 || poke.HPType == (int)row.Cells[3].Value)
                    {
                        Report("Filter: Hidden Power - PASS");
                    }
                    else
                    {
                        Report("Filter: Hidden Power - FAIL");
                        failedtests++;
                    }

                    // Test Gender
                    if ((int)row.Cells[4].Value < 0 || (int)row.Cells[4].Value == poke.Gender)
                    {
                        Report("Filter: Gender - PASS");
                    }
                    else
                    {
                        Report("Filter: Gender - FAIL");
                        failedtests++;
                    }

                    // Test HP
                    if (IVCheck((int)row.Cells[5].Value, poke.IV_HP, (int)row.Cells[6].Value))
                    {
                        Report("Filter: Hit Points IV - PASS");
                    }
                    else
                    {
                        Report("Filter: Hit Points IV - FAIL");
                        failedtests++;
                    }
                    if (poke.IV_HP == 31)
                    {
                        perfectIVs++;
                    }

                    // Test Atk
                    if (IVCheck((int)row.Cells[7].Value, poke.IV_ATK, (int)row.Cells[8].Value))
                    {
                        Report("Filter: Attack IV - PASS");
                    }
                    else
                    {
                        Report("Filter: Attack IV - FAIL");
                        failedtests++;
                    }
                    if (poke.IV_ATK == 31)
                    {
                        perfectIVs++;
                    }

                    // Test Def
                    if (IVCheck((int)row.Cells[9].Value, poke.IV_DEF, (int)row.Cells[10].Value))
                    {
                        Report("Filter: Defense IV - PASS");
                    }
                    else
                    {
                        Report("Filter: Defense IV - FAIL");
                        failedtests++;
                    }
                    if (poke.IV_DEF == 31)
                    {
                        perfectIVs++;
                    }

                    // Test SpA
                    if (IVCheck((int)row.Cells[11].Value, poke.IV_SPA, (int)row.Cells[12].Value))
                    {
                        Report("Filter: Special Attack IV - PASS");
                    }
                    else
                    {
                        Report("Filter: Special Attack IV - FAIL");
                        failedtests++;
                    }
                    if (poke.IV_SPA == 31)
                    {
                        perfectIVs++;
                    }

                    // Test SpD
                    if (IVCheck((int)row.Cells[13].Value, poke.IV_SPD, (int)row.Cells[14].Value))
                    {
                        Report("Filter: Special Defense IV - PASS");
                    }
                    else
                    {
                        Report("Filter: Special Defense IV - FAIL");
                        failedtests++;
                    }
                    if (poke.IV_SPD == 31)
                    {
                        perfectIVs++;
                    }

                    // Test Spe
                    if (IVCheck((int)row.Cells[15].Value, poke.IV_SPE, (int)row.Cells[16].Value))
                    {
                        Report("Filter: Speed IV - PASS");
                    }
                    else
                    {
                        Report("Filter: Speed IV - FAIL");
                        failedtests++;
                    }
                    if (poke.IV_SPE == 31)
                    {
                        perfectIVs++;
                    }

                    // Test Perfect IVs
                    if (IVCheck((int)row.Cells[17].Value, perfectIVs, (int)row.Cells[18].Value))
                    {
                        Report("Filter: Perfect IVs - PASS");
                    }
                    else
                    {
                        Report("Filter: Perfect IVs - FAIL");
                        failedtests++;
                    }
                    if (failedtests == 0)
                    {
                        return currentfilter;
                    }
                }
                return -1;
            }
            else
            {
                return 1;
            }
        }

        private static bool IVCheck(int refiv, int actualiv, int logic)
        {
            switch (logic)
            {
                case 0: // Greater or equal
                    return actualiv >= refiv;
                case 1: // Greater
                    return actualiv > refiv;
                case 2: // Equal
                    return actualiv == refiv;
                case 3: // Less
                    return actualiv < refiv;
                case 4: // Less or equal
                    return actualiv <= refiv;
                case 5: // Different
                    return actualiv != refiv;
                case 6: // Even
                    return actualiv % 2 == 0;
                case 7: // Odd
                    return actualiv % 2 == 1;
                default:
                    return true;
            }
        }

        public static void ShowResult(string source, ErrorMessage message, int[] info = null)
        {
            switch (message)
            {
                case ErrorMessage.Finished:
                    MessageBox.Show("Bot finished sucessfully.", source, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case ErrorMessage.UserStop:
                    MessageBox.Show("Bot stopped by the user.", source, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case ErrorMessage.ReadError:
                    MessageBox.Show("A error ocurred while reading data from the 3DS RAM.", source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case ErrorMessage.WriteError:
                    MessageBox.Show("A error ocurred while writting data to the 3DS RAM.", source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case ErrorMessage.ButtonError:
                    MessageBox.Show("A error ocurred while sending Button commands to the 3DS.", source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case ErrorMessage.TouchError:
                    MessageBox.Show("A error ocurred while sending Touch Screen commands to the 3DS.", source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case ErrorMessage.StickError:
                    MessageBox.Show("A error ocurred while sending Control Stick commands to the 3DS.", source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case ErrorMessage.NotInPSS:
                    MessageBox.Show("Please go to the PSS menu and try again.", source, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case ErrorMessage.FestivalPlaza:
                    MessageBox.Show("Bot finished due level-up in Festival Plaza.", source, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case ErrorMessage.SVMatch:
                    MessageBox.Show($"Finished. A match was found at box {info[0]}, slot {info[1]} with the ESV/TSV value: {info[2]}.", source, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case ErrorMessage.FilterMatch:
                    MessageBox.Show($"Finished. A match was found at box {info[0]}, slot {info[1]} using filter #{info[2]}.", source, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case ErrorMessage.NoMatch:
                    MessageBox.Show("Bot finished sucessfuly without finding a match for the current settings.", source, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case ErrorMessage.SRMatch:
                    MessageBox.Show($"Finished. The current pokémon matched filter #{info[0]} after {info[1]} soft-resets.", source, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case ErrorMessage.BattleMatch:
                    MessageBox.Show($"Finished. The current pokémon matched filter #{info[0]} after {info[1]} battles.", source, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case ErrorMessage.Disconnect:
                    MessageBox.Show("Connection with the 3DS was lost.", source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case ErrorMessage.GeneralError:
                    MessageBox.Show("A error has ocurred, see log for detals.", source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                default:
                    MessageBox.Show("An unknown error has ocurred, please keep the log and report this error.", source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }
    }
}
