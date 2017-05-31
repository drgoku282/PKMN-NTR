namespace pkmn_ntr.Sub_forms
{
    partial class PokemonEventHandler
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PokemonEventHandler));
            this.InfoLabel = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SlotChangeText = new System.Windows.Forms.TextBox();
            this.SlotChangeButton = new System.Windows.Forms.Button();
            this.HPZeroText = new System.Windows.Forms.TextBox();
            this.HPZeroButton = new System.Windows.Forms.Button();
            this.SlotChangeLabel = new System.Windows.Forms.Label();
            this.HPZeroLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // InfoLabel
            // 
            this.InfoLabel.AutoSize = true;
            this.InfoLabel.Location = new System.Drawing.Point(12, 9);
            this.InfoLabel.Name = "InfoLabel";
            this.InfoLabel.Size = new System.Drawing.Size(338, 13);
            this.InfoLabel.TabIndex = 14;
            this.InfoLabel.Text = "Experimental tool for executing commands when specific events occur";
            // 
            // SlotChangeText
            // 
            this.SlotChangeText.Location = new System.Drawing.Point(123, 33);
            this.SlotChangeText.Name = "SlotChangeText";
            this.SlotChangeText.Size = new System.Drawing.Size(271, 20);
            this.SlotChangeText.TabIndex = 0;
            this.SlotChangeText.Text = "del C:\\Users\\username\\sprites\\p###SLOT###.png & copy /Y C:\\Users\\username\\sprites\\###NAME###.png C:\\Users\\username\\sprites\\p###SLOT###.png & copy /b C:\\Users\\username\\sprites\\p###SLOT###.png+,, C:\\Users\\username\\sprites\\p###SLOT###.png";
            this.toolTip1.SetToolTip(this.SlotChangeText, "Command to execute when any party slot changes");
            this.SlotChangeText.TextChanged += new System.EventHandler(this.SlotChangeText_TextChanged);
            // 
            // SlotChangeButton
            // 
            this.SlotChangeButton.Location = new System.Drawing.Point(400, 30);
            this.SlotChangeButton.Name = "SlotChangeButton";
            this.SlotChangeButton.Size = new System.Drawing.Size(75, 23);
            this.SlotChangeButton.TabIndex = 12;
            this.SlotChangeButton.Text = "Apply";
            this.toolTip1.SetToolTip(this.SlotChangeButton, "Apply this event handler");
            this.SlotChangeButton.UseVisualStyleBackColor = true;
            this.SlotChangeButton.Click += new System.EventHandler(this.SlotChangeButton_Click);
            // 
            // HPZeroText
            // 
            this.HPZeroText.Location = new System.Drawing.Point(123, 63);
            this.HPZeroText.Name = "HPZeroText";
            this.HPZeroText.Size = new System.Drawing.Size(271, 20);
            this.HPZeroText.TabIndex = 18;
            this.HPZeroText.Text = "del C:\\Users\\username\\sprites\\p###SLOT##.png & copy /Y C:\\Users\\username\\sprites\\pokeball.png C:\\Users\\username\\sprites\\p###SLOT###.png & copy /b C:\\Users\\username\\sprites\\p###SLOT###.png+,, C:\\Users\\username\\sprites\\p###SLOT###.png";
            this.toolTip1.SetToolTip(this.HPZeroText, "Command to execute when any Pokemon in your party reaches 0 HP");
            this.HPZeroText.TextChanged += new System.EventHandler(this.HPZeroText_TextChanged);
            // 
            // HPZeroButton
            // 
            this.HPZeroButton.Location = new System.Drawing.Point(400, 61);
            this.HPZeroButton.Name = "HPZeroButton";
            this.HPZeroButton.Size = new System.Drawing.Size(75, 23);
            this.HPZeroButton.TabIndex = 19;
            this.HPZeroButton.Text = "Apply";
            this.toolTip1.SetToolTip(this.HPZeroButton, "Apply this event handler");
            this.HPZeroButton.UseVisualStyleBackColor = true;
            this.HPZeroButton.Click += new System.EventHandler(this.HPZeroButton_Click);
            // 
            // SlotChangeLabel
            // 
            this.SlotChangeLabel.AutoSize = true;
            this.SlotChangeLabel.Location = new System.Drawing.Point(12, 36);
            this.SlotChangeLabel.Name = "SlotChangeLabel";
            this.SlotChangeLabel.Size = new System.Drawing.Size(108, 13);
            this.SlotChangeLabel.TabIndex = 16;
            this.SlotChangeLabel.Text = "On party slot change:";
            // 
            // HPZeroLabel
            // 
            this.HPZeroLabel.AutoSize = true;
            this.HPZeroLabel.Location = new System.Drawing.Point(12, 66);
            this.HPZeroLabel.Name = "HPZeroLabel";
            this.HPZeroLabel.Size = new System.Drawing.Size(109, 13);
            this.HPZeroLabel.TabIndex = 17;
            this.HPZeroLabel.Text = "On HP reaching zero:";
            // 
            // PokemonEventHandler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(484, 320);
            this.Controls.Add(this.HPZeroButton);
            this.Controls.Add(this.HPZeroText);
            this.Controls.Add(this.HPZeroLabel);
            this.Controls.Add(this.SlotChangeButton);
            this.Controls.Add(this.SlotChangeLabel);
            this.Controls.Add(this.SlotChangeText);
            this.Controls.Add(this.InfoLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PokemonEventHandler";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 6, 6);
            this.Text = "Event Handler";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PokemonEventHandler_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label InfoLabel;
        private System.Windows.Forms.TextBox SlotChangeText;
        private System.Windows.Forms.Label SlotChangeLabel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button SlotChangeButton;
        private System.Windows.Forms.Label HPZeroLabel;
        private System.Windows.Forms.TextBox HPZeroText;
        private System.Windows.Forms.Button HPZeroButton;
    }
}