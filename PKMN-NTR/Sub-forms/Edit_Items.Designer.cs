namespace pkmn_ntr.Sub_forms
{
    partial class Edit_Items
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Edit_Items));
            this.CHK_NEW = new System.Windows.Forms.CheckBox();
            this.NUD_Count = new System.Windows.Forms.NumericUpDown();
            this.L_Count = new System.Windows.Forms.Label();
            this.B_Sort = new System.Windows.Forms.Button();
            this.B_GiveAll = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.B_Save = new System.Windows.Forms.Button();
            this.B_Cancel = new System.Windows.Forms.Button();
            this.IL_Pouch = new System.Windows.Forms.ImageList(this.components);
            this.sortMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuSortName = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSortNameReverse = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSortCount = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSortCountReverse = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSortIndex = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSortIndexReverse = new System.Windows.Forms.ToolStripMenuItem();
            this.giveMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.giveAll = new System.Windows.Forms.ToolStripMenuItem();
            this.giveNone = new System.Windows.Forms.ToolStripMenuItem();
            this.giveModify = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Count)).BeginInit();
            this.sortMenu.SuspendLayout();
            this.giveMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // CHK_NEW
            // 
            this.CHK_NEW.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CHK_NEW.AutoSize = true;
            this.CHK_NEW.Checked = true;
            this.CHK_NEW.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHK_NEW.Location = new System.Drawing.Point(148, 383);
            this.CHK_NEW.Name = "CHK_NEW";
            this.CHK_NEW.Size = new System.Drawing.Size(75, 17);
            this.CHK_NEW.TabIndex = 30;
            this.CHK_NEW.Text = "Flag NEW";
            this.CHK_NEW.UseVisualStyleBackColor = true;
            // 
            // NUD_Count
            // 
            this.NUD_Count.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NUD_Count.Location = new System.Drawing.Point(93, 380);
            this.NUD_Count.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUD_Count.Name = "NUD_Count";
            this.NUD_Count.Size = new System.Drawing.Size(49, 20);
            this.NUD_Count.TabIndex = 29;
            this.NUD_Count.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // L_Count
            // 
            this.L_Count.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.L_Count.AutoSize = true;
            this.L_Count.Location = new System.Drawing.Point(92, 366);
            this.L_Count.Name = "L_Count";
            this.L_Count.Size = new System.Drawing.Size(38, 13);
            this.L_Count.TabIndex = 28;
            this.L_Count.Text = "Count:";
            // 
            // B_Sort
            // 
            this.B_Sort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.B_Sort.Location = new System.Drawing.Point(12, 353);
            this.B_Sort.Name = "B_Sort";
            this.B_Sort.Size = new System.Drawing.Size(75, 23);
            this.B_Sort.TabIndex = 27;
            this.B_Sort.Text = "Sort";
            this.B_Sort.UseVisualStyleBackColor = true;
            this.B_Sort.Click += new System.EventHandler(this.B_Sort_Click);
            // 
            // B_GiveAll
            // 
            this.B_GiveAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.B_GiveAll.Location = new System.Drawing.Point(12, 377);
            this.B_GiveAll.Name = "B_GiveAll";
            this.B_GiveAll.Size = new System.Drawing.Size(75, 23);
            this.B_GiveAll.TabIndex = 26;
            this.B_GiveAll.Text = "Give All";
            this.B_GiveAll.UseVisualStyleBackColor = true;
            this.B_GiveAll.Click += new System.EventHandler(this.B_GiveAll_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Location = new System.Drawing.Point(12, 11);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(291, 336);
            this.tabControl1.TabIndex = 25;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.SwitchBag);
            // 
            // B_Save
            // 
            this.B_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.B_Save.Location = new System.Drawing.Point(232, 353);
            this.B_Save.Name = "B_Save";
            this.B_Save.Size = new System.Drawing.Size(70, 23);
            this.B_Save.TabIndex = 24;
            this.B_Save.Text = "Save";
            this.B_Save.UseVisualStyleBackColor = true;
            this.B_Save.Click += new System.EventHandler(this.B_Save_Click);
            // 
            // B_Cancel
            // 
            this.B_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.B_Cancel.Location = new System.Drawing.Point(232, 377);
            this.B_Cancel.Name = "B_Cancel";
            this.B_Cancel.Size = new System.Drawing.Size(70, 23);
            this.B_Cancel.TabIndex = 23;
            this.B_Cancel.Text = "Cancel";
            this.B_Cancel.UseVisualStyleBackColor = true;
            this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
            // 
            // IL_Pouch
            // 
            this.IL_Pouch.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("IL_Pouch.ImageStream")));
            this.IL_Pouch.TransparentColor = System.Drawing.Color.Transparent;
            this.IL_Pouch.Images.SetKeyName(0, "Bag_Items.png");
            this.IL_Pouch.Images.SetKeyName(1, "Bag_Key.png");
            this.IL_Pouch.Images.SetKeyName(2, "Bag_TMHM.png");
            this.IL_Pouch.Images.SetKeyName(3, "Bag_Medicine.png");
            this.IL_Pouch.Images.SetKeyName(4, "Bag_Berries.png");
            this.IL_Pouch.Images.SetKeyName(5, "Bag_Balls.png");
            this.IL_Pouch.Images.SetKeyName(6, "Bag_Battle.png");
            this.IL_Pouch.Images.SetKeyName(7, "Bag_Mail.png");
            this.IL_Pouch.Images.SetKeyName(8, "Bag_PCItems.png");
            this.IL_Pouch.Images.SetKeyName(9, "Bag_Free.png");
            this.IL_Pouch.Images.SetKeyName(10, "Bag_Z.png");
            // 
            // sortMenu
            // 
            this.sortMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSortName,
            this.mnuSortNameReverse,
            this.mnuSortCount,
            this.mnuSortCountReverse,
            this.mnuSortIndex,
            this.mnuSortIndexReverse});
            this.sortMenu.Name = "modifyMenu";
            this.sortMenu.Size = new System.Drawing.Size(159, 136);
            // 
            // mnuSortName
            // 
            this.mnuSortName.Name = "mnuSortName";
            this.mnuSortName.Size = new System.Drawing.Size(158, 22);
            this.mnuSortName.Text = "Name";
            this.mnuSortName.Click += new System.EventHandler(this.SortByName);
            // 
            // mnuSortNameReverse
            // 
            this.mnuSortNameReverse.Name = "mnuSortNameReverse";
            this.mnuSortNameReverse.Size = new System.Drawing.Size(158, 22);
            this.mnuSortNameReverse.Text = "Name (Reverse)";
            this.mnuSortNameReverse.Click += new System.EventHandler(this.SortByName);
            // 
            // mnuSortCount
            // 
            this.mnuSortCount.Name = "mnuSortCount";
            this.mnuSortCount.Size = new System.Drawing.Size(158, 22);
            this.mnuSortCount.Text = "Count";
            this.mnuSortCount.Click += new System.EventHandler(this.SortByCount);
            // 
            // mnuSortCountReverse
            // 
            this.mnuSortCountReverse.Name = "mnuSortCountReverse";
            this.mnuSortCountReverse.Size = new System.Drawing.Size(158, 22);
            this.mnuSortCountReverse.Text = "Count (Reverse)";
            this.mnuSortCountReverse.Click += new System.EventHandler(this.SortByCount);
            // 
            // mnuSortIndex
            // 
            this.mnuSortIndex.Name = "mnuSortIndex";
            this.mnuSortIndex.Size = new System.Drawing.Size(158, 22);
            this.mnuSortIndex.Text = "Index";
            this.mnuSortIndex.Click += new System.EventHandler(this.SortByIndex);
            // 
            // mnuSortIndexReverse
            // 
            this.mnuSortIndexReverse.Name = "mnuSortIndexReverse";
            this.mnuSortIndexReverse.Size = new System.Drawing.Size(158, 22);
            this.mnuSortIndexReverse.Text = "Index (Reverse)";
            this.mnuSortIndexReverse.Click += new System.EventHandler(this.SortByIndex);
            // 
            // giveMenu
            // 
            this.giveMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.giveAll,
            this.giveNone,
            this.giveModify});
            this.giveMenu.Name = "modifyMenu";
            this.giveMenu.Size = new System.Drawing.Size(113, 70);
            // 
            // giveAll
            // 
            this.giveAll.Name = "giveAll";
            this.giveAll.Size = new System.Drawing.Size(112, 22);
            this.giveAll.Text = "All";
            this.giveAll.Click += new System.EventHandler(this.GiveAllItems);
            // 
            // giveNone
            // 
            this.giveNone.Name = "giveNone";
            this.giveNone.Size = new System.Drawing.Size(112, 22);
            this.giveNone.Text = "None";
            this.giveNone.Click += new System.EventHandler(this.RemoveAllItems);
            // 
            // giveModify
            // 
            this.giveModify.Name = "giveModify";
            this.giveModify.Size = new System.Drawing.Size(112, 22);
            this.giveModify.Text = "Modify";
            this.giveModify.Click += new System.EventHandler(this.ModifyAllItems);
            // 
            // Edit_Items
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 411);
            this.Controls.Add(this.CHK_NEW);
            this.Controls.Add(this.NUD_Count);
            this.Controls.Add(this.L_Count);
            this.Controls.Add(this.B_Sort);
            this.Controls.Add(this.B_GiveAll);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.B_Save);
            this.Controls.Add(this.B_Cancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Edit_Items";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "Item Editor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Edit_Items_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Count)).EndInit();
            this.sortMenu.ResumeLayout(false);
            this.giveMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox CHK_NEW;
        private System.Windows.Forms.NumericUpDown NUD_Count;
        private System.Windows.Forms.Label L_Count;
        private System.Windows.Forms.Button B_Sort;
        private System.Windows.Forms.Button B_GiveAll;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button B_Save;
        private System.Windows.Forms.Button B_Cancel;
        private System.Windows.Forms.ImageList IL_Pouch;
        private System.Windows.Forms.ContextMenuStrip sortMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuSortName;
        private System.Windows.Forms.ToolStripMenuItem mnuSortNameReverse;
        private System.Windows.Forms.ToolStripMenuItem mnuSortCount;
        private System.Windows.Forms.ToolStripMenuItem mnuSortCountReverse;
        private System.Windows.Forms.ToolStripMenuItem mnuSortIndex;
        private System.Windows.Forms.ToolStripMenuItem mnuSortIndexReverse;
        private System.Windows.Forms.ContextMenuStrip giveMenu;
        private System.Windows.Forms.ToolStripMenuItem giveAll;
        private System.Windows.Forms.ToolStripMenuItem giveNone;
        private System.Windows.Forms.ToolStripMenuItem giveModify;
    }
}