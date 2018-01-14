using System;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using PKHeX.Core;
using pkmn_ntr.Helpers;
using System.Linq;

namespace pkmn_ntr.Sub_forms
{
    public partial class Edit_Trainer : Form
    {
        private Task<bool> RAMreader;

        private Control[] Ctrls = new Control[] { };

        public Edit_Trainer()
        {
            InitializeComponent();
            Ctrls = new Control[] { TB_Name, Write_Name, Num_TID, Write_TID, Num_SID, Write_SID, Num_Money, Write_Money, Num_Miles, Write_Miles, Num_FestivalCoins, Num_FestivalCoins, Num_TotalFC, Write_TotalFC, Num_Miles, Write_Miles, Num_BP, Write_BP, CB_Language, Write_Lang, Num_Hour, Num_Min, Num_Sec, Write_Time, ReloadFields };
            CB_Language.DisplayMember = "Text";
            CB_Language.ValueMember = "Value";
            var languages = Util.GetUnsortedCBList("languages");
            if (Program.gCmdWindow.SAV.Generation < 7)
                languages = languages.Where(l => l.Value <= 8).ToList(); // Korean
            CB_Language.DataSource = languages;
        }

        private void Edit_Trainer_Load(object sender, EventArgs e)
        {
            // Show earning
            DialogResult dr = MessageBox.Show("Editing any value in this window may result in a temporal BAN from online services or a PERMANENT BAN from all features that uses Pokémon Global Link (PGL) like Game Sync, Rated Battles and Global Missions. Do you want to continue?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            { // Download data from game
                if (Program.gCmdWindow.SAV.Generation == 6)
                {
                    Fill6();
                }
                else
                {
                    Fill7();
                }
            }
            else
            { // Close the window
                Close();
            }
        }

        private void SetControls(bool state)
        {
            foreach (Control c in Ctrls)
            {
                Delg.SetEnabled(c, state);
            }
        }

        private async void Fill6()
        {
            SetControls(false);
            Delg.SetText(TB_Name, await waitName());
            Delg.SetValue(Num_TID, await waitTID());
            Delg.SetValue(Num_SID, await waitSID());
            Delg.SetValue(Num_Money, await waitMoney());
            Delg.SetValue(Num_Miles, await waitMiles());
            Delg.SetValue(Num_BP, await waitBP());
            Delg.SetSelectedValue(CB_Language, await waitLang());
            byte[] time = await waitTime();
            Delg.SetValue(Num_Hour, BitConverter.ToUInt16(time, 0));
            Delg.SetValue(Num_Min, time[2]);
            Delg.SetValue(Num_Sec, time[3]);
            Delg.SetEnabled(ReloadFields, true);
        }

        private async void Fill7()
        {
            SetControls(false);
            Delg.SetText(TB_Name, await waitName());
            Delg.SetValue(Num_TID, await waitTID());
            Delg.SetValue(Num_SID, await waitSID());
            Delg.SetValue(Num_Money, await waitMoney());
            Delg.SetValue(Num_FestivalCoins, await waitFestivalCoins());
            Delg.SetValue(Num_TotalFC, await waitTotalFC());
            Delg.SetValue(Num_BP, await waitBP());
            Delg.SetSelectedValue(CB_Language, await waitLang());
            byte[] time = await waitTime();
            Delg.SetValue(Num_Hour, BitConverter.ToUInt16(time, 0));
            Delg.SetValue(Num_Min, time[2]);
            Delg.SetValue(Num_Sec, time[3]);
            Delg.SetEnabled(ReloadFields, true);
        }

        private async Task<string> waitName()
        {
            RAMreader = Program.helper.waitNTRmultiread(LookupTable.TrainerNameOffset, 0x1A);
            if (await RAMreader)
            {
                Delg.SetEnabled(TB_Name, true);
                Delg.SetEnabled(Write_Name, true);
                return Util.TrimFromZero(Encoding.Unicode.GetString(Program.helper.lastmultiread));
            }
            else
            {
                return null;
            }
        }

        private async Task<ushort> waitTID()
        {
            RAMreader = Program.helper.waitNTRmultiread(LookupTable.TrainerTIDOffset, 0x02);
            if (await RAMreader)
            {
                Delg.SetEnabled(Num_TID, true);
                Delg.SetEnabled(Write_TID, true);
                return BitConverter.ToUInt16(Program.helper.lastmultiread, 0);
            }
            else
            {
                return 0;
            }
        }

        private async Task<ushort> waitSID()
        {
            RAMreader = Program.helper.waitNTRmultiread(LookupTable.TrainerSIDOffset, 0x02);
            if (await RAMreader)
            {
                Delg.SetEnabled(Num_SID, true);
                Delg.SetEnabled(Write_SID, true);
                return BitConverter.ToUInt16(Program.helper.lastmultiread, 0);
            }
            else
            {
                return 0;
            }
        }

        private async Task<uint> waitMoney()
        {
            RAMreader = Program.helper.waitNTRmultiread(LookupTable.TrainerMoneyOffset, 0x04);
            if (await RAMreader)
            {
                Delg.SetEnabled(Num_Money, true);
                Delg.SetEnabled(Write_Money, true);
                return BitConverter.ToUInt32(Program.helper.lastmultiread, 0);
            }
            else
            {
                return 0;
            }
        }

        private async Task<uint> waitMiles()
        {
            RAMreader = Program.helper.waitNTRmultiread(LookupTable.TrainerPokeMilesOffset, 0x04);
            if (await RAMreader)
            {
                Delg.SetEnabled(Num_Miles, true);
                Delg.SetEnabled(Write_Miles, true);
                return BitConverter.ToUInt32(Program.helper.lastmultiread, 0);
            }
            else
            {
                return 0;
            }
        }

        private async Task<uint> waitFestivalCoins()
        {
            RAMreader = Program.helper.waitNTRmultiread(LookupTable.TrainerCurrentFCOffset, 0x04);
            if (await RAMreader)
            {
                Delg.SetEnabled(Num_FestivalCoins, true);
                Delg.SetEnabled(Write_FestivalCoins, true);
                return BitConverter.ToUInt32(Program.helper.lastmultiread, 0);
            }
            else
            {
                return 0;
            }
        }

        private async Task<uint> waitTotalFC()
        {
            RAMreader = Program.helper.waitNTRmultiread(LookupTable.TrainerTotalFCOffset, 0x04);
            if (await RAMreader)
            {
                Delg.SetEnabled(Num_TotalFC, true);
                Delg.SetEnabled(Write_TotalFC, true);
                return BitConverter.ToUInt32(Program.helper.lastmultiread, 0);
            }
            else
            {
                return 0;
            }
        }

        private async Task<uint> waitBP()
        {
            RAMreader = Program.helper.waitNTRmultiread(LookupTable.TrainerBPOffset, 0x04);
            if (await RAMreader)
            {
                Delg.SetEnabled(Num_BP, true);
                Delg.SetEnabled(Write_BP, true);
                return BitConverter.ToUInt32(Program.helper.lastmultiread, 0);
            }
            else
            {
                return 0;
            }
        }

        private async Task<int> waitLang()
        {
            RAMreader = Program.helper.waitNTRmultiread(LookupTable.GameLanguageOffset, 0x01);
            if (await RAMreader)
            {
                Delg.SetEnabled(CB_Language, true);
                Delg.SetEnabled(Write_Lang, true);
                return Convert.ToInt32(Program.helper.lastmultiread[0]);
            }
            else
            {
                return 0;
            }
        }

        private async Task<byte[]> waitTime()
        {
            RAMreader = Program.helper.waitNTRmultiread(LookupTable.GamePlayTimeOffset, 0x04);
            if (await RAMreader)
            {
                Delg.SetEnabled(Num_Hour, true);
                Delg.SetEnabled(Num_Min, true);
                Delg.SetEnabled(Num_Sec, true);
                Delg.SetEnabled(Write_Time, true);
                return Program.helper.lastmultiread;
            }
            else
            {
                return new byte[] { 0, 0, 0, 0 };
            }
        }

        private void ReloadFields_Click(object sender, EventArgs e)
        {
            if (Program.gCmdWindow.SAV.Generation == 6)
            {
                Fill6();
            }
            else
            {
                Fill7();
            }
        }

        private void setTSV(object sender, EventArgs e)
        {
            int TSV = LookupTable.GetTSV((ushort)Num_TID.Value, (ushort)Num_SID.Value);
            int G7ID = LookupTable.GetG7ID((ushort)Num_TID.Value, (ushort)Num_SID.Value);
            if (Num_TID.Value > ushort.MaxValue)
            {
                Num_TID.Value = ushort.MaxValue;
            }
            if (Num_SID.Value > ushort.MaxValue)
            {
                Num_SID.Value = ushort.MaxValue;
            }
            if (Program.gCmdWindow.SAV.Generation == 6)
            {
                Delg.SetTooltip(toolTip1, Num_TID, "TSV: " + TSV.ToString("D4"));
                Delg.SetTooltip(toolTip1, Num_SID, "TSV: " + TSV.ToString("D4"));
            }
            else if (Program.gCmdWindow.SAV.Generation == 7)
            {
                Delg.SetTooltip(toolTip1, Num_TID, "TSV: " + TSV.ToString("D4") + "\r\nG7ID: " + G7ID.ToString("D6"));
                Delg.SetTooltip(toolTip1, Num_SID, "TSV: " + TSV.ToString("D4") + "\r\nG7ID: " + G7ID.ToString("D6"));
            }
        }

        private async void Write_Name_Click(object sender, EventArgs e)
        {
            Delg.SetEnabled(TB_Name, false);
            Delg.SetEnabled(Write_Name, false);
            byte[] data = Encoding.Unicode.GetBytes(TB_Name.Text.PadRight(13, '\0'));
            RAMreader = Program.helper.waitNTRwrite(LookupTable.TrainerNameOffset, data, Program.gCmdWindow.pid);
            if (!(await RAMreader))
            {
                MessageBox.Show("A error ocurred while writting data to RAM.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Delg.SetEnabled(TB_Name, true);
            Delg.SetEnabled(Write_Name, true);
        }

        private async void Write_TID_Click(object sender, EventArgs e)
        {
            Delg.SetEnabled(Num_TID, false);
            Delg.SetEnabled(Write_TID, false);
            if (Num_TID.Value > ushort.MaxValue)
            {
                Num_TID.Value = ushort.MaxValue;
            }
            byte[] data = BitConverter.GetBytes(Convert.ToUInt16(Num_TID.Value));
            RAMreader = Program.helper.waitNTRwrite(LookupTable.TrainerTIDOffset, data, Program.gCmdWindow.pid);
            if (!(await RAMreader))
            {
                MessageBox.Show("A error ocurred while writting data to RAM.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Delg.SetEnabled(Num_TID, true);
            Delg.SetEnabled(Write_TID, true);
        }

        private async void Write_SID_Click(object sender, EventArgs e)
        {
            Delg.SetEnabled(Num_SID, false);
            Delg.SetEnabled(Write_SID, false);
            if (Num_SID.Value > ushort.MaxValue)
            {
                Num_SID.Value = ushort.MaxValue;
            }
            byte[] data = BitConverter.GetBytes(Convert.ToUInt16(Num_SID.Value));
            RAMreader = Program.helper.waitNTRwrite(LookupTable.TrainerSIDOffset, data, Program.gCmdWindow.pid);
            if (!(await RAMreader))
            {
                MessageBox.Show("A error ocurred while writting data to RAM.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Delg.SetEnabled(Num_SID, true);
            Delg.SetEnabled(Write_SID, true);
        }

        private async void Write_Money_Click(object sender, EventArgs e)
        {
            Delg.SetEnabled(Num_Money, false);
            Delg.SetEnabled(Write_Money, false);
            if (Num_Money.Value > 9999999)
            {
                Num_Money.Value = 9999999;
            }
            byte[] data = BitConverter.GetBytes(Convert.ToUInt32(Num_Money.Value));
            RAMreader = Program.helper.waitNTRwrite(LookupTable.TrainerMoneyOffset, data, Program.gCmdWindow.pid);
            if (!(await RAMreader))
            {
                MessageBox.Show("A error ocurred while writting data to RAM.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Delg.SetEnabled(Num_Money, true);
            Delg.SetEnabled(Write_Money, true);
        }

        private async void Write_Miles_Click(object sender, EventArgs e)
        {
            Delg.SetEnabled(Num_Miles, false);
            Delg.SetEnabled(Write_Miles, false);
            if (Num_Miles.Value > 9999999)
            {
                Num_Miles.Value = 9999999;
            }
            byte[] data = BitConverter.GetBytes(Convert.ToUInt32(Num_Miles.Value));
            RAMreader = Program.helper.waitNTRwrite(LookupTable.TrainerPokeMilesOffset, data, Program.gCmdWindow.pid);
            if (!(await RAMreader))
            {
                MessageBox.Show("A error ocurred while writting data to RAM.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Delg.SetEnabled(Num_Miles, true);
            Delg.SetEnabled(Write_Miles, true);
        }

        private async void Write_FestivalCoins_Click(object sender, EventArgs e)
        {
            Delg.SetEnabled(Num_FestivalCoins, false);
            Delg.SetEnabled(Write_FestivalCoins, false);
            if (Num_FestivalCoins.Value > 9999999)
            {
                Num_FestivalCoins.Value = 9999999;
            }
            byte[] data = BitConverter.GetBytes(Convert.ToUInt32(Num_FestivalCoins.Value));
            RAMreader = Program.helper.waitNTRwrite(LookupTable.TrainerCurrentFCOffset, data, Program.gCmdWindow.pid);
            if (!(await RAMreader))
            {
                MessageBox.Show("A error ocurred while writting data to RAM.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Delg.SetEnabled(Num_FestivalCoins, true);
            Delg.SetEnabled(Write_FestivalCoins, true);
        }

        private async void Write_TotalFC_Click(object sender, EventArgs e)
        {
            Delg.SetEnabled(Num_TotalFC, false);
            Delg.SetEnabled(Write_TotalFC, false);
            if (Num_TotalFC.Value > 9999999)
            {
                Num_TotalFC.Value = 9999999;
            }
            byte[] data = BitConverter.GetBytes(Convert.ToUInt32(Num_TotalFC.Value));
            RAMreader = Program.helper.waitNTRwrite(LookupTable.TrainerTotalFCOffset, data, Program.gCmdWindow.pid);
            if (!(await RAMreader))
            {
                MessageBox.Show("A error ocurred while writting data to RAM.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Delg.SetEnabled(Num_TotalFC, true);
            Delg.SetEnabled(Write_TotalFC, true);
        }

        private async void Write_BP_Click(object sender, EventArgs e)
        {
            Delg.SetEnabled(Num_BP, false);
            Delg.SetEnabled(Write_BP, false);
            if (Num_BP.Value > 9999)
            {
                Num_BP.Value = 9999;
            }
            byte[] data = BitConverter.GetBytes(Convert.ToUInt32(Num_BP.Value));
            RAMreader = Program.helper.waitNTRwrite(LookupTable.TrainerBPOffset, data, Program.gCmdWindow.pid);
            if (!(await RAMreader))
            {
                MessageBox.Show("A error ocurred while writting data to RAM.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Delg.SetEnabled(Num_BP, true);
            Delg.SetEnabled(Write_BP, true);
        }

        private async void Write_Lang_Click(object sender, EventArgs e)
        {
            Delg.SetEnabled(CB_Language, false);
            Delg.SetEnabled(Write_Lang, false);
            byte[] data = BitConverter.GetBytes(WinFormsUtil.getIndex(CB_Language));
            RAMreader = Program.helper.waitNTRwrite(LookupTable.GameLanguageOffset, data, Program.gCmdWindow.pid);
            if (!(await RAMreader))
            {
                MessageBox.Show("A error ocurred while writting data to RAM.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Delg.SetEnabled(CB_Language, true);
            Delg.SetEnabled(Write_Lang, true);
        }

        private async void Write_Time_Click(object sender, EventArgs e)
        {
            Delg.SetEnabled(Num_Hour, false);
            Delg.SetEnabled(Num_Min, false);
            Delg.SetEnabled(Num_Sec, false);
            Delg.SetEnabled(Write_Time, false);
            if (Num_Hour.Value > 999)
            {
                Num_BP.Value = 999;
            }
            if (Num_Min.Value > 59)
            {
                Num_Min.Value = 59;
            }
            if (Num_Sec.Value > 59)
            {
                Num_Sec.Value = 59;
            }
            byte[] data = new byte[4];
            BitConverter.GetBytes(Convert.ToUInt16(Num_Hour.Value)).CopyTo(data, 0);
            data[2] = Convert.ToByte(Num_Min.Value);
            data[3] = Convert.ToByte(Num_Sec.Value);
            RAMreader = Program.helper.waitNTRwrite(LookupTable.GamePlayTimeOffset, data, Program.gCmdWindow.pid);
            if (!(await RAMreader))
            {
                MessageBox.Show("A error ocurred while writting data to RAM.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Delg.SetEnabled(Num_Hour, true);
            Delg.SetEnabled(Num_Min, true);
            Delg.SetEnabled(Num_Sec, true);
            Delg.SetEnabled(Write_Time, true);
        }

        private void Edit_Trainer_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.gCmdWindow.Tool_Finish();
        }
    }
}
