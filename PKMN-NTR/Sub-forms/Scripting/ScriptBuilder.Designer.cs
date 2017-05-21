namespace pkmn_ntr.Sub_forms.Scripting
{
    partial class ScriptBuilder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptBuilder));
            this.Remote_Stick = new System.Windows.Forms.GroupBox();
            this.sdrX = new System.Windows.Forms.TrackBar();
            this.sdrY = new System.Windows.Forms.TrackBar();
            this.numStickY = new System.Windows.Forms.NumericUpDown();
            this.btnReleaseStick = new System.Windows.Forms.Button();
            this.btnStick = new System.Windows.Forms.Button();
            this.numStickX = new System.Windows.Forms.NumericUpDown();
            this.label62 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.Remote_touch = new System.Windows.Forms.GroupBox();
            this.label54 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.numTouchX = new System.Windows.Forms.NumericUpDown();
            this.numTouchY = new System.Windows.Forms.NumericUpDown();
            this.btnReleaseTouch = new System.Windows.Forms.Button();
            this.btnTouch = new System.Windows.Forms.Button();
            this.Remote_buttons = new System.Windows.Forms.GroupBox();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnA = new System.Windows.Forms.Button();
            this.btnR = new System.Windows.Forms.Button();
            this.btnB = new System.Windows.Forms.Button();
            this.btnL = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnY = new System.Windows.Forms.Button();
            this.btnReleaseButton = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnX = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.lstActions = new System.Windows.Forms.ListBox();
            this.btnActionRemove = new System.Windows.Forms.Button();
            this.btnActionUp = new System.Windows.Forms.Button();
            this.btnActionDown = new System.Windows.Forms.Button();
            this.bthActionClear = new System.Windows.Forms.Button();
            this.numTime = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnDelay = new System.Windows.Forms.Button();
            this.btnAddFor = new System.Windows.Forms.Button();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.numFor = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSaveScript = new System.Windows.Forms.Button();
            this.btnLoadScript = new System.Windows.Forms.Button();
            this.Remote_Stick.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sdrX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sdrY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStickY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStickX)).BeginInit();
            this.Remote_touch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTouchX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTouchY)).BeginInit();
            this.Remote_buttons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFor)).BeginInit();
            this.SuspendLayout();
            // 
            // Remote_Stick
            // 
            this.Remote_Stick.Controls.Add(this.sdrX);
            this.Remote_Stick.Controls.Add(this.sdrY);
            this.Remote_Stick.Controls.Add(this.numStickY);
            this.Remote_Stick.Controls.Add(this.btnReleaseStick);
            this.Remote_Stick.Controls.Add(this.btnStick);
            this.Remote_Stick.Controls.Add(this.numStickX);
            this.Remote_Stick.Controls.Add(this.label62);
            this.Remote_Stick.Controls.Add(this.label61);
            this.Remote_Stick.Location = new System.Drawing.Point(290, 12);
            this.Remote_Stick.Name = "Remote_Stick";
            this.Remote_Stick.Size = new System.Drawing.Size(268, 186);
            this.Remote_Stick.TabIndex = 6;
            this.Remote_Stick.TabStop = false;
            this.Remote_Stick.Text = "Control Stick";
            // 
            // sdrX
            // 
            this.sdrX.Location = new System.Drawing.Point(57, 103);
            this.sdrX.Maximum = 100;
            this.sdrX.Minimum = -100;
            this.sdrX.Name = "sdrX";
            this.sdrX.Size = new System.Drawing.Size(205, 45);
            this.sdrX.TabIndex = 1;
            this.sdrX.TickFrequency = 25;
            this.sdrX.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.sdrX.Scroll += new System.EventHandler(this.StickX_Scroll);
            // 
            // sdrY
            // 
            this.sdrY.Location = new System.Drawing.Point(6, 19);
            this.sdrY.Maximum = 100;
            this.sdrY.Minimum = -100;
            this.sdrY.Name = "sdrY";
            this.sdrY.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.sdrY.Size = new System.Drawing.Size(45, 129);
            this.sdrY.TabIndex = 0;
            this.sdrY.TickFrequency = 25;
            this.sdrY.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.sdrY.Scroll += new System.EventHandler(this.StickY_Scroll);
            // 
            // numStickY
            // 
            this.numStickY.Location = new System.Drawing.Point(57, 22);
            this.numStickY.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numStickY.Name = "numStickY";
            this.numStickY.Size = new System.Drawing.Size(100, 20);
            this.numStickY.TabIndex = 2;
            this.numStickY.ValueChanged += new System.EventHandler(this.StickNumY_ValueChanged);
            // 
            // btnReleaseStick
            // 
            this.btnReleaseStick.Location = new System.Drawing.Point(162, 63);
            this.btnReleaseStick.Name = "btnReleaseStick";
            this.btnReleaseStick.Size = new System.Drawing.Size(100, 23);
            this.btnReleaseStick.TabIndex = 4;
            this.btnReleaseStick.Text = "Release";
            this.btnReleaseStick.UseVisualStyleBackColor = true;
            this.btnReleaseStick.Click += new System.EventHandler(this.ClickReleaseStick);
            // 
            // btnStick
            // 
            this.btnStick.Location = new System.Drawing.Point(57, 63);
            this.btnStick.Name = "btnStick";
            this.btnStick.Size = new System.Drawing.Size(100, 23);
            this.btnStick.TabIndex = 4;
            this.btnStick.Text = "Control Stick";
            this.btnStick.UseVisualStyleBackColor = true;
            this.btnStick.Click += new System.EventHandler(this.ClickStickButton);
            // 
            // numStickX
            // 
            this.numStickX.Location = new System.Drawing.Point(163, 22);
            this.numStickX.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numStickX.Name = "numStickX";
            this.numStickX.Size = new System.Drawing.Size(99, 20);
            this.numStickX.TabIndex = 3;
            this.numStickX.ValueChanged += new System.EventHandler(this.StickNumX_ValueChanged);
            // 
            // label62
            // 
            this.label62.Location = new System.Drawing.Point(163, 45);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(99, 18);
            this.label62.TabIndex = 25;
            this.label62.Text = "Horizontal";
            this.label62.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label61
            // 
            this.label61.Location = new System.Drawing.Point(57, 45);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(100, 18);
            this.label61.TabIndex = 24;
            this.label61.Text = "Vertical";
            this.label61.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Remote_touch
            // 
            this.Remote_touch.AutoSize = true;
            this.Remote_touch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Remote_touch.Controls.Add(this.label54);
            this.Remote_touch.Controls.Add(this.label30);
            this.Remote_touch.Controls.Add(this.numTouchX);
            this.Remote_touch.Controls.Add(this.numTouchY);
            this.Remote_touch.Controls.Add(this.btnReleaseTouch);
            this.Remote_touch.Controls.Add(this.btnTouch);
            this.Remote_touch.Location = new System.Drawing.Point(12, 127);
            this.Remote_touch.Name = "Remote_touch";
            this.Remote_touch.Size = new System.Drawing.Size(274, 71);
            this.Remote_touch.TabIndex = 5;
            this.Remote_touch.TabStop = false;
            this.Remote_touch.Text = "Touchscreen";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(72, 42);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(61, 13);
            this.label54.TabIndex = 15;
            this.label54.Text = "Y (from top)";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(6, 42);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(60, 13);
            this.label30.TabIndex = 15;
            this.label30.Text = "X (from left)";
            // 
            // numTouchX
            // 
            this.numTouchX.Location = new System.Drawing.Point(6, 19);
            this.numTouchX.Maximum = new decimal(new int[] {
            319,
            0,
            0,
            0});
            this.numTouchX.Name = "numTouchX";
            this.numTouchX.Size = new System.Drawing.Size(60, 20);
            this.numTouchX.TabIndex = 0;
            // 
            // numTouchY
            // 
            this.numTouchY.Location = new System.Drawing.Point(72, 19);
            this.numTouchY.Maximum = new decimal(new int[] {
            239,
            0,
            0,
            0});
            this.numTouchY.Name = "numTouchY";
            this.numTouchY.Size = new System.Drawing.Size(60, 20);
            this.numTouchY.TabIndex = 1;
            // 
            // btnReleaseTouch
            // 
            this.btnReleaseTouch.Location = new System.Drawing.Point(206, 16);
            this.btnReleaseTouch.Name = "btnReleaseTouch";
            this.btnReleaseTouch.Size = new System.Drawing.Size(62, 23);
            this.btnReleaseTouch.TabIndex = 2;
            this.btnReleaseTouch.Text = "Release";
            this.btnReleaseTouch.UseVisualStyleBackColor = true;
            this.btnReleaseTouch.Click += new System.EventHandler(this.ClickReleaseTouch);
            // 
            // btnTouch
            // 
            this.btnTouch.Location = new System.Drawing.Point(138, 16);
            this.btnTouch.Name = "btnTouch";
            this.btnTouch.Size = new System.Drawing.Size(62, 23);
            this.btnTouch.TabIndex = 2;
            this.btnTouch.Text = "Touch";
            this.btnTouch.UseVisualStyleBackColor = true;
            this.btnTouch.Click += new System.EventHandler(this.ClickTouchButton);
            // 
            // Remote_buttons
            // 
            this.Remote_buttons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Remote_buttons.Controls.Add(this.btnUp);
            this.Remote_buttons.Controls.Add(this.btnA);
            this.Remote_buttons.Controls.Add(this.btnR);
            this.Remote_buttons.Controls.Add(this.btnB);
            this.Remote_buttons.Controls.Add(this.btnL);
            this.Remote_buttons.Controls.Add(this.btnSelect);
            this.Remote_buttons.Controls.Add(this.btnRight);
            this.Remote_buttons.Controls.Add(this.btnDown);
            this.Remote_buttons.Controls.Add(this.btnY);
            this.Remote_buttons.Controls.Add(this.btnReleaseButton);
            this.Remote_buttons.Controls.Add(this.btnStart);
            this.Remote_buttons.Controls.Add(this.btnX);
            this.Remote_buttons.Controls.Add(this.btnLeft);
            this.Remote_buttons.Location = new System.Drawing.Point(12, 12);
            this.Remote_buttons.Name = "Remote_buttons";
            this.Remote_buttons.Size = new System.Drawing.Size(272, 109);
            this.Remote_buttons.TabIndex = 4;
            this.Remote_buttons.TabStop = false;
            this.Remote_buttons.Text = "Buttons";
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(35, 19);
            this.btnUp.Name = "btnUp";
            this.btnUp.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnUp.Size = new System.Drawing.Size(23, 23);
            this.btnUp.TabIndex = 4;
            this.btnUp.Text = "↑";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.ClickConsoleButton);
            // 
            // btnA
            // 
            this.btnA.Location = new System.Drawing.Point(243, 46);
            this.btnA.Name = "btnA";
            this.btnA.Size = new System.Drawing.Size(23, 23);
            this.btnA.TabIndex = 0;
            this.btnA.Text = "A";
            this.btnA.UseVisualStyleBackColor = true;
            this.btnA.Click += new System.EventHandler(this.ClickConsoleButton);
            // 
            // btnR
            // 
            this.btnR.Location = new System.Drawing.Point(146, 19);
            this.btnR.Name = "btnR";
            this.btnR.Size = new System.Drawing.Size(23, 23);
            this.btnR.TabIndex = 9;
            this.btnR.Text = "R";
            this.btnR.UseVisualStyleBackColor = true;
            this.btnR.Click += new System.EventHandler(this.ClickConsoleButton);
            // 
            // btnB
            // 
            this.btnB.Location = new System.Drawing.Point(214, 74);
            this.btnB.Name = "btnB";
            this.btnB.Size = new System.Drawing.Size(23, 23);
            this.btnB.TabIndex = 1;
            this.btnB.Text = "B";
            this.btnB.UseVisualStyleBackColor = true;
            this.btnB.Click += new System.EventHandler(this.ClickConsoleButton);
            // 
            // btnL
            // 
            this.btnL.Location = new System.Drawing.Point(103, 19);
            this.btnL.Name = "btnL";
            this.btnL.Size = new System.Drawing.Size(23, 23);
            this.btnL.TabIndex = 8;
            this.btnL.Text = "L";
            this.btnL.UseVisualStyleBackColor = true;
            this.btnL.Click += new System.EventHandler(this.ClickConsoleButton);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(146, 74);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(62, 23);
            this.btnSelect.TabIndex = 11;
            this.btnSelect.Text = "SELECT";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.ClickConsoleButton);
            // 
            // btnRight
            // 
            this.btnRight.Location = new System.Drawing.Point(64, 46);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(23, 23);
            this.btnRight.TabIndex = 7;
            this.btnRight.Text = "→";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.ClickConsoleButton);
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(35, 74);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(23, 23);
            this.btnDown.TabIndex = 5;
            this.btnDown.Text = "↓";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.ClickConsoleButton);
            // 
            // btnY
            // 
            this.btnY.Location = new System.Drawing.Point(185, 46);
            this.btnY.Name = "btnY";
            this.btnY.Size = new System.Drawing.Size(23, 23);
            this.btnY.TabIndex = 3;
            this.btnY.Text = "Y";
            this.btnY.UseVisualStyleBackColor = true;
            this.btnY.Click += new System.EventHandler(this.ClickConsoleButton);
            // 
            // btnReleaseButton
            // 
            this.btnReleaseButton.Location = new System.Drawing.Point(103, 48);
            this.btnReleaseButton.Name = "btnReleaseButton";
            this.btnReleaseButton.Size = new System.Drawing.Size(66, 23);
            this.btnReleaseButton.TabIndex = 10;
            this.btnReleaseButton.Text = "Release";
            this.btnReleaseButton.UseVisualStyleBackColor = true;
            this.btnReleaseButton.Click += new System.EventHandler(this.ClickReleaseButtons);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(64, 74);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(62, 23);
            this.btnStart.TabIndex = 10;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.ClickConsoleButton);
            // 
            // btnX
            // 
            this.btnX.Location = new System.Drawing.Point(214, 19);
            this.btnX.Name = "btnX";
            this.btnX.Size = new System.Drawing.Size(23, 23);
            this.btnX.TabIndex = 2;
            this.btnX.Text = "X";
            this.btnX.UseVisualStyleBackColor = true;
            this.btnX.Click += new System.EventHandler(this.ClickConsoleButton);
            // 
            // btnLeft
            // 
            this.btnLeft.Location = new System.Drawing.Point(6, 46);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(23, 23);
            this.btnLeft.TabIndex = 6;
            this.btnLeft.Text = "←";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.ClickConsoleButton);
            // 
            // lstActions
            // 
            this.lstActions.FormattingEnabled = true;
            this.lstActions.Location = new System.Drawing.Point(12, 204);
            this.lstActions.Name = "lstActions";
            this.lstActions.Size = new System.Drawing.Size(272, 108);
            this.lstActions.TabIndex = 7;
            // 
            // btnActionRemove
            // 
            this.btnActionRemove.Location = new System.Drawing.Point(290, 262);
            this.btnActionRemove.Name = "btnActionRemove";
            this.btnActionRemove.Size = new System.Drawing.Size(75, 23);
            this.btnActionRemove.TabIndex = 8;
            this.btnActionRemove.Text = "Remove";
            this.btnActionRemove.UseVisualStyleBackColor = true;
            this.btnActionRemove.Click += new System.EventHandler(this.RemoveAction);
            // 
            // btnActionUp
            // 
            this.btnActionUp.Location = new System.Drawing.Point(290, 204);
            this.btnActionUp.Name = "btnActionUp";
            this.btnActionUp.Size = new System.Drawing.Size(75, 23);
            this.btnActionUp.TabIndex = 9;
            this.btnActionUp.Text = "Move Up";
            this.btnActionUp.UseVisualStyleBackColor = true;
            this.btnActionUp.Click += new System.EventHandler(this.BtnActionUp_Click);
            // 
            // btnActionDown
            // 
            this.btnActionDown.Location = new System.Drawing.Point(290, 233);
            this.btnActionDown.Name = "btnActionDown";
            this.btnActionDown.Size = new System.Drawing.Size(75, 23);
            this.btnActionDown.TabIndex = 9;
            this.btnActionDown.Text = "Move Down";
            this.btnActionDown.UseVisualStyleBackColor = true;
            this.btnActionDown.Click += new System.EventHandler(this.BtnActionDown_Click);
            // 
            // bthActionClear
            // 
            this.bthActionClear.Location = new System.Drawing.Point(290, 291);
            this.bthActionClear.Name = "bthActionClear";
            this.bthActionClear.Size = new System.Drawing.Size(75, 23);
            this.bthActionClear.TabIndex = 8;
            this.bthActionClear.Text = "Clear All";
            this.bthActionClear.UseVisualStyleBackColor = true;
            this.bthActionClear.Click += new System.EventHandler(this.ClearAllActions);
            // 
            // numTime
            // 
            this.numTime.Location = new System.Drawing.Point(418, 207);
            this.numTime.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTime.Name = "numTime";
            this.numTime.Size = new System.Drawing.Size(58, 20);
            this.numTime.TabIndex = 10;
            this.numTime.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(371, 209);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Option:";
            // 
            // btnDelay
            // 
            this.btnDelay.Location = new System.Drawing.Point(482, 204);
            this.btnDelay.Name = "btnDelay";
            this.btnDelay.Size = new System.Drawing.Size(76, 23);
            this.btnDelay.TabIndex = 9;
            this.btnDelay.Text = "Add Delay";
            this.toolTip1.SetToolTip(this.btnDelay, "Adds a Delay with a length in ms equal to the value selected in Option.");
            this.btnDelay.UseVisualStyleBackColor = true;
            this.btnDelay.Click += new System.EventHandler(this.ClickDelay);
            // 
            // btnAddFor
            // 
            this.btnAddFor.Location = new System.Drawing.Point(482, 233);
            this.btnAddFor.Name = "btnAddFor";
            this.btnAddFor.Size = new System.Drawing.Size(76, 23);
            this.btnAddFor.TabIndex = 9;
            this.btnAddFor.Text = "Add Loop";
            this.toolTip1.SetToolTip(this.btnAddFor, "Adds a loop block. The number of loops is selected in the Loops field,\r\nand the I" +
        "D of the loop is selected in the Option flield.");
            this.btnAddFor.UseVisualStyleBackColor = true;
            this.btnAddFor.Click += new System.EventHandler(this.AddLoop);
            // 
            // btnStartStop
            // 
            this.btnStartStop.Enabled = false;
            this.btnStartStop.Location = new System.Drawing.Point(374, 291);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(184, 23);
            this.btnStartStop.TabIndex = 13;
            this.btnStartStop.Text = "Not Connected";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.StartStopScript);
            // 
            // numFor
            // 
            this.numFor.Location = new System.Drawing.Point(418, 236);
            this.numFor.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numFor.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFor.Name = "numFor";
            this.numFor.Size = new System.Drawing.Size(58, 20);
            this.numFor.TabIndex = 10;
            this.numFor.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(371, 238);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Loops:";
            // 
            // btnSaveScript
            // 
            this.btnSaveScript.Location = new System.Drawing.Point(374, 262);
            this.btnSaveScript.Name = "btnSaveScript";
            this.btnSaveScript.Size = new System.Drawing.Size(89, 23);
            this.btnSaveScript.TabIndex = 9;
            this.btnSaveScript.Text = "Save Script...";
            this.btnSaveScript.UseVisualStyleBackColor = true;
            this.btnSaveScript.Click += new System.EventHandler(this.SaveScript);
            // 
            // btnLoadScript
            // 
            this.btnLoadScript.Location = new System.Drawing.Point(469, 262);
            this.btnLoadScript.Name = "btnLoadScript";
            this.btnLoadScript.Size = new System.Drawing.Size(89, 23);
            this.btnLoadScript.TabIndex = 9;
            this.btnLoadScript.Text = "Load Script...";
            this.btnLoadScript.UseVisualStyleBackColor = true;
            this.btnLoadScript.Click += new System.EventHandler(this.LoadScript);
            // 
            // ScriptBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(566, 325);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numFor);
            this.Controls.Add(this.numTime);
            this.Controls.Add(this.btnActionDown);
            this.Controls.Add(this.btnAddFor);
            this.Controls.Add(this.btnDelay);
            this.Controls.Add(this.btnLoadScript);
            this.Controls.Add(this.btnSaveScript);
            this.Controls.Add(this.btnActionUp);
            this.Controls.Add(this.bthActionClear);
            this.Controls.Add(this.btnActionRemove);
            this.Controls.Add(this.lstActions);
            this.Controls.Add(this.Remote_Stick);
            this.Controls.Add(this.Remote_touch);
            this.Controls.Add(this.Remote_buttons);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ScriptBuilder";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 6, 6);
            this.Text = "Script Builder";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CloseForm);
            this.Load += new System.EventHandler(this.ScriptBuilder_Load);
            this.Remote_Stick.ResumeLayout(false);
            this.Remote_Stick.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sdrX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sdrY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStickY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStickX)).EndInit();
            this.Remote_touch.ResumeLayout(false);
            this.Remote_touch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTouchX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTouchY)).EndInit();
            this.Remote_buttons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox Remote_Stick;
        private System.Windows.Forms.TrackBar sdrX;
        private System.Windows.Forms.TrackBar sdrY;
        private System.Windows.Forms.NumericUpDown numStickY;
        private System.Windows.Forms.Button btnStick;
        private System.Windows.Forms.NumericUpDown numStickX;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.GroupBox Remote_touch;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.NumericUpDown numTouchX;
        private System.Windows.Forms.NumericUpDown numTouchY;
        private System.Windows.Forms.Button btnTouch;
        private System.Windows.Forms.GroupBox Remote_buttons;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnA;
        private System.Windows.Forms.Button btnR;
        private System.Windows.Forms.Button btnB;
        private System.Windows.Forms.Button btnL;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnY;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnX;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnReleaseButton;
        private System.Windows.Forms.ListBox lstActions;
        private System.Windows.Forms.Button btnReleaseTouch;
        private System.Windows.Forms.Button btnReleaseStick;
        private System.Windows.Forms.Button btnActionRemove;
        private System.Windows.Forms.Button btnActionUp;
        private System.Windows.Forms.Button btnActionDown;
        private System.Windows.Forms.Button bthActionClear;
        private System.Windows.Forms.NumericUpDown numTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnDelay;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.NumericUpDown numFor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAddFor;
        private System.Windows.Forms.Button btnSaveScript;
        private System.Windows.Forms.Button btnLoadScript;
    }
}