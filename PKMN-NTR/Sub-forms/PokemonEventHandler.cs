using System;
using System.Windows.Forms;
using System.IO;

namespace pkmn_ntr.Sub_forms
{
    public partial class PokemonEventHandler : Form
    {
        MainForm mainForm;

        public PokemonEventHandler(MainForm main)
        {
            InitializeComponent();
            mainForm = main;
            if (mainForm.slotChangeCommand.Length > 0)
            {
                SlotChangeText.Text = mainForm.slotChangeCommand;
            }
            if (mainForm.hpZeroCommand.Length > 0)
            {
                HPZeroText.Text = mainForm.hpZeroCommand;
            }
        }

        private void PokemonEventHandler_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.gCmdWindow.Tool_Finish();
        }

        private void SlotChangeButton_Click(object sender, EventArgs e)
        {
            mainForm.slotChangeCommand = SlotChangeText.Text;
            SlotChangeButton.Enabled = false;
            SaveCommands();
        }

        private void HPZeroButton_Click(object sender, EventArgs e)
        {
            mainForm.hpZeroCommand = HPZeroText.Text;
            HPZeroButton.Enabled = false;
            SaveCommands();
        }

        private void SlotChangeText_TextChanged(object sender, EventArgs e)
        {
            SlotChangeButton.Enabled = true;
        }

        private void HPZeroText_TextChanged(object sender, EventArgs e)
        {
            HPZeroButton.Enabled = true;
        }

        private void SaveCommands()
        {
            try
            {
                string output = "SlotChange:";
                output += mainForm.slotChangeCommand + "\r\n";
                output += "HPZero:";
                output += mainForm.hpZeroCommand + "\r\n";
                File.WriteAllText(@System.Windows.Forms.Application.StartupPath + "\\EventCommands.txt", output);
            }
            catch (FileNotFoundException ex)
            {
            }
        }

        public static void RestoreCommands(MainForm main)
        {
            try
            {
                StreamReader sr = new StreamReader(@System.Windows.Forms.Application.StartupPath + "\\EventCommands.txt");
                string line;
                do
                {
                    line = sr.ReadLine();
                    if (null != line)
                    {
                        if (line.StartsWith("SlotChange:"))
                        {
                            main.slotChangeCommand = line.Split(new char[] { ':' }, 2)[1];
                        }
                        else if (line.StartsWith("HPZero:"))
                        {
                            main.hpZeroCommand = line.Split(new char[] { ':' }, 2)[1];
                        }
                    }
                }
                while (null != line);
                sr.Close();
            }
            catch (FileNotFoundException ex)
            {
            }
        }
    }
}
