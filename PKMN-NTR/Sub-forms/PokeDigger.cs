using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using PKHeX.Core;
using pkmn_ntr.Helpers;

namespace pkmn_ntr.Sub_forms
{
    public partial class PokeDigger : Form
    {
        const int POKEBYTES = 232;
        int pid;
        uint querySeq = 0;
        byte[] recvData;
        struct Result
        {
            public byte[] data { get; set; }
            public int offset { get; set; }
        }
        List<Result> allResults;

        public PokeDigger(int pid, bool connected)
        {
            this.pid = pid;
            allResults = new List<Result>();
            InitializeComponent();
            Program.ntrClient.DataReady += handleDataReady;
            if (!connected)
            {
                DoItBtn.Enabled = false;
            }
            GUISetReady();
        }

        private void PokeDigger_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.ntrClient.DataReady -= handleDataReady;
            Program.gCmdWindow.Tool_Finish();
        }

        private void GUISetReady()
        {
            StatusLabel.Text = "Status: Ready";
            progressBar.Value = 0;
            CancelBtn.Enabled = false;
            querySeq = 0;
        }

        private void StartWorker(ref byte[] data)
        {
            if (DigWorker.IsBusy)
            {
                MessageBox.Show("Another operation is in progress! Please wait.");
                return;
            }
            StatusLabel.Text = "Status: Processing";
            CancelBtn.Enabled = true;

            DigWorker.RunWorkerAsync(data);
        }

        private void DoItBtn_Click(object sender, EventArgs e)
        {
            if (querySeq != 0)
            {
                MessageBox.Show("Another operation is in progress! Please wait.");
                return;
            }
            
            uint start = Convert.ToUInt32(StartAddrText.Text, 16);
            uint size = Convert.ToUInt32(SizeText.Text, 16);
            querySeq = Program.scriptHelper.data(start, size, pid);
            StatusLabel.Text = "Status: Waiting for data";
            CancelBtn.Enabled = true;
        }

        private void FromFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog file_dialog = new OpenFileDialog();
            file_dialog.Title = "Choose a memory dump file";
            file_dialog.ShowDialog();

            if (file_dialog.FileName == "")
            {
                return;
            }

            byte[] fileBytes = File.ReadAllBytes(file_dialog.FileName);
            StartWorker(ref fileBytes);
        }

        private void handleDataReady(object sender, DataReadyEventArgs e)
        {
            if (e.seq != querySeq) //somebody else's data
            {
                return;
            }
            
            recvData = e.data;
            if (DumpSaveCB.Checked) { 
                string folderPath = @Application.StartupPath + "\\Digger\\";
                (new FileInfo(folderPath)).Directory.Create();
                string fileName = Path.Combine(folderPath, "dump " + StartAddrText.Text + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bin");
                File.WriteAllBytes(fileName, recvData);
            }
            //Thanks to this, processing will be done in GUI thread
            //so we don't need to use SetBlahblahblah functions everywhere
            Delg.SetText(UglyMultithreadingHack, "b");
        }

        private void UglyMultithreadingHack_TextChanged(object sender, EventArgs e)
        {
            if (UglyMultithreadingHack.Text == "b")
            {
                UglyMultithreadingHack.Text = "a";
                querySeq = 0; //reset to default
                StartWorker(ref recvData);
            }
        }

        private void DigWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            List<Result> results = new List<Result>();
            byte[] source = (byte[])e.Argument;
            int length = source.Length - POKEBYTES + 1;
            int nextProgress = 0;
            int progressStep = (int)((double)length / 100.0);
            bool fullMode = !FastModeCB.Checked;
            bool gen7 = Program.gCmdWindow.SAV.Generation == 7;
            int maxspecies = Program.gCmdWindow.SAV.MaxSpeciesID;

            for (int i = 0; i < length; i++)
            {
                //Check sanity. Rejects ~50% of positions
                if (source[i+4] == 0 && source[i+5] == 0)
                {
                    uint seed = BitConverter.ToUInt32(source, i + 0);
                    //In fast mode skip if EC == 0. Rejects ~90% of positions
                    if (fullMode || seed != 0) 
                    {
                        PKM pkFound = Program.gCmdWindow.SAV.BlankPKM;
                        byte[] dataFound = new byte[POKEBYTES];
                        Array.Copy(source, i, dataFound, 0, POKEBYTES);
                        dataFound = PKX.DecryptArray(dataFound);
                        pkFound.Data = dataFound;
                        if (pkFound.ChecksumValid && pkFound.Species >= 1 && pkFound.Species <= maxspecies)
                        {
                            results.Add(new Result { data = dataFound, offset = i });
                        }
                        ////Decrypt partially, calculate checksum
                        ////We don't need to deshuffle, it won't affect the checksum
                        //ushort checksum = 0;
                        //for (int j = 8; j < POKEBYTES; j += 2)
                        //{
                        //    seed = seed * 0x41C64E6D + 0x00006073;
                        //    checksum += (ushort)(BitConverter.ToUInt16(source, i + j) ^ (ushort)(seed >> 16));
                        //}

                        ////Allows very few positions, but there's still some garbage
                        //if (checksum == BitConverter.ToUInt16(source, i + 6))
                        //{
                        //    byte[] dataFound = new byte[POKEBYTES];
                        //    Array.Copy(source, i, dataFound, 0, POKEBYTES);
                        //    //Decrypt it properly this time
                        //    dataFound = PKX.decryptArray(dataFound);
                        //    PKM pkFound = Program.gCmdWindow.SAV.BlankPKM;
                        //    pkFound.Data = dataFound;

                        //    if (pkFound.Species >= 1 && pkFound.Species <= 802)
                        //    {
                        //        //Almost certainly valid
                        //        results.Add(new Result { data = dataFound, offset = i });
                        //    }
                        //}
                    }
                }
                
                if (i >= nextProgress) {
                    //The condition will be checked every 1% of progress to improve performance
                    if (worker.CancellationPending == true)
                    {
                        e.Cancel = true;
                        break;
                    }

                    int progress = (int)((100.0 * (double)i) / (double)length);
                    worker.ReportProgress(progress);
                    nextProgress = i + progressStep;
                }
            }

            allResults.AddRange(results);
        }

        private void DigWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void DigWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            GUISetReady();
            
            int resultsCount = allResults.Count;
            if (resultsCount == 0)
                return;

            ResultsGrid.Rows.Clear();
            ResultsGrid.Rows.Add(resultsCount);

            int i = 0;
            foreach (Result r in allResults)
            {
                PKM pkInfo = Program.gCmdWindow.SAV.BlankPKM;
                pkInfo.Data = r.data;

                ResultsGrid.Rows[i].Tag = r.data;
                Image sprite = pkInfo.Sprite();

                ResultsGrid.Rows[i].Cells[0].Value = sprite;
                ResultsGrid.Rows[i].Height = sprite.Height;
                ResultsGrid.Rows[i].Cells[1].Value = pkInfo.CurrentLevel;
                ResultsGrid.Rows[i].Cells[2].Value = pkInfo.Nickname;
                ResultsGrid.Rows[i].Cells[4].Value = pkInfo.PID.ToString("X8");
                ResultsGrid.Rows[i].Cells[5].Value = r.offset.ToString("X8");
                i++;
            }
        }

        private void DumpBtn_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in ResultsGrid.Rows)
            {
                DataGridViewCheckBoxCell checkboxCell = (DataGridViewCheckBoxCell)r.Cells[3];
                if (checkboxCell.Value == null || (bool)checkboxCell.Value == false)
                {
                    continue;
                }
                string folderPath = @Application.StartupPath + "\\Digger\\";
                (new FileInfo(folderPath)).Directory.Create();
                PKM pkx = Program.gCmdWindow.SAV.BlankPKM;
                pkx.Data = (byte[])r.Tag;
                string fn = pkx.FileName;
                fn = fn.Substring(0, fn.LastIndexOf('.'));
                string filename = $"{fn}.{pkx.Extension}";
                string newfile = Path.Combine(folderPath, Util.CleanFileName(filename));
                File.WriteAllBytes(newfile, pkx.DecryptedBoxData);
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ResultsGrid.Rows.Clear();
            allResults.Clear();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            if (DigWorker.IsBusy)
            {
                DigWorker.CancelAsync();
            }
            
            GUISetReady();
        }

        private void StepPlusBtn_Click(object sender, EventArgs e)
        {
            uint start = Convert.ToUInt32(StartAddrText.Text, 16);
            uint size = Convert.ToUInt32(SizeText.Text, 16);
            start += size;
            StartAddrText.Text = String.Format("0x{0:X8}", start);
        }

        private void StepMinusBtn_Click(object sender, EventArgs e)
        {
            uint start = Convert.ToUInt32(StartAddrText.Text, 16);
            uint size = Convert.ToUInt32(SizeText.Text, 16);
            start -= size;
            StartAddrText.Text = String.Format("0x{0:X8}", start);
        }

        private void FastModeCB_CheckedChanged(object sender, EventArgs e)
        {
            if (FastModeCB.Checked)
            {
                MessageBox.Show("In fast mode, Pokemon data with encryption constant equal to 0 will be skipped.\r\n" +
                    "This will exclude some valid Pokemon, but will massively speed up the search.");
            }
        }

        private void InvertBtn_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in ResultsGrid.Rows)
            {
                DataGridViewCheckBoxCell checkboxCell = (DataGridViewCheckBoxCell)r.Cells[3];
                if (checkboxCell.Value == null || (bool)checkboxCell.Value == false)
                {
                    checkboxCell.Value = true;
                }
                else
                {
                    checkboxCell.Value = null;
                }
            }
        }

        private void HelpBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hover your mouse cursor over all the controls to find out what they do.");
            toolTip1.SetToolTip(HelpBtn, "Congratulations! You have demonstrated your ability to follow orders.");
        }
    }
}
