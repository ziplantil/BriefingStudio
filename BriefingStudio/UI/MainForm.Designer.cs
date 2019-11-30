namespace BriefingStudio.UI
{
    partial class MainForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.descentHogOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.workingHogOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.lowResScreenButton = new System.Windows.Forms.Button();
            this.highResScreenButton = new System.Windows.Forms.Button();
            this.baseHogLabel = new System.Windows.Forms.Label();
            this.txbEditorButton = new System.Windows.Forms.Button();
            this.closeWorkingHogButton = new System.Windows.Forms.Button();
            this.playBriefingButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.levelNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.stopBriefingButton = new System.Windows.Forms.Button();
            this.frameTimer = new System.Windows.Forms.Timer(this.components);
            this.briefingNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.workingHogLabel = new System.Windows.Forms.Label();
            this.playingBriefingLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.newEditorButton = new System.Windows.Forms.Button();
            this.bannerGeneratorButton = new System.Windows.Forms.Button();
            this.infoLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.levelNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Open D1/D2 HOG";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.DescentHogOpenButton_click);
            // 
            // descentHogOpenFileDialog
            // 
            this.descentHogOpenFileDialog.DefaultExt = "hog";
            this.descentHogOpenFileDialog.Filter = "DESCENT.HOG, DESCENT2.HOG|*.hog|All files|*.*";
            this.descentHogOpenFileDialog.Title = "Open registered Descent or Descent 2 HOG";
            this.descentHogOpenFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.DescentHogOpenFileDialog_FileOk);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(148, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(142, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Open Working HOG";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.WorkingHogOpenButton_click);
            // 
            // workingHogOpenFileDialog
            // 
            this.workingHogOpenFileDialog.DefaultExt = "hog";
            this.workingHogOpenFileDialog.Filter = ".HOG file|*.hog|All files|*.*";
            this.workingHogOpenFileDialog.Title = "Open HOG you\'re working on";
            this.workingHogOpenFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.WorkingHogOpenFileDialog_FileOk);
            // 
            // lowResScreenButton
            // 
            this.lowResScreenButton.Location = new System.Drawing.Point(0, 120);
            this.lowResScreenButton.Name = "lowResScreenButton";
            this.lowResScreenButton.Size = new System.Drawing.Size(142, 23);
            this.lowResScreenButton.TabIndex = 3;
            this.lowResScreenButton.Text = "Low Res (320x200)";
            this.lowResScreenButton.UseVisualStyleBackColor = true;
            this.lowResScreenButton.Click += new System.EventHandler(this.lowResScreenButton_Click);
            // 
            // highResScreenButton
            // 
            this.highResScreenButton.Location = new System.Drawing.Point(148, 120);
            this.highResScreenButton.Name = "highResScreenButton";
            this.highResScreenButton.Size = new System.Drawing.Size(142, 23);
            this.highResScreenButton.TabIndex = 4;
            this.highResScreenButton.Text = "High Res (640x480)";
            this.highResScreenButton.UseVisualStyleBackColor = true;
            this.highResScreenButton.Click += new System.EventHandler(this.highResScreenButton_Click);
            // 
            // baseHogLabel
            // 
            this.baseHogLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.baseHogLabel.AutoSize = true;
            this.baseHogLabel.Location = new System.Drawing.Point(-3, 231);
            this.baseHogLabel.Name = "baseHogLabel";
            this.baseHogLabel.Size = new System.Drawing.Size(152, 13);
            this.baseHogLabel.TabIndex = 5;
            this.baseHogLabel.Text = "No Descent HOG file specified";
            // 
            // txbEditorButton
            // 
            this.txbEditorButton.Location = new System.Drawing.Point(0, 91);
            this.txbEditorButton.Name = "txbEditorButton";
            this.txbEditorButton.Size = new System.Drawing.Size(142, 23);
            this.txbEditorButton.TabIndex = 8;
            this.txbEditorButton.Text = "TXB Editor";
            this.txbEditorButton.UseVisualStyleBackColor = true;
            this.txbEditorButton.Click += new System.EventHandler(this.txbEditorButton_Click);
            // 
            // closeWorkingHogButton
            // 
            this.closeWorkingHogButton.Location = new System.Drawing.Point(148, 32);
            this.closeWorkingHogButton.Name = "closeWorkingHogButton";
            this.closeWorkingHogButton.Size = new System.Drawing.Size(142, 23);
            this.closeWorkingHogButton.TabIndex = 9;
            this.closeWorkingHogButton.Text = "Close Working HOG";
            this.closeWorkingHogButton.UseVisualStyleBackColor = true;
            this.closeWorkingHogButton.Click += new System.EventHandler(this.closeWorkingHogButton_Click);
            // 
            // playBriefingButton
            // 
            this.playBriefingButton.Location = new System.Drawing.Point(0, 149);
            this.playBriefingButton.Name = "playBriefingButton";
            this.playBriefingButton.Size = new System.Drawing.Size(142, 23);
            this.playBriefingButton.TabIndex = 12;
            this.playBriefingButton.Text = "Play Briefing:";
            this.playBriefingButton.UseVisualStyleBackColor = true;
            this.playBriefingButton.Click += new System.EventHandler(this.playBriefingButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-3, 184);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Sequence";
            // 
            // levelNumericUpDown
            // 
            this.levelNumericUpDown.Location = new System.Drawing.Point(66, 182);
            this.levelNumericUpDown.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.levelNumericUpDown.Name = "levelNumericUpDown";
            this.levelNumericUpDown.Size = new System.Drawing.Size(76, 20);
            this.levelNumericUpDown.TabIndex = 15;
            this.levelNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // stopBriefingButton
            // 
            this.stopBriefingButton.Location = new System.Drawing.Point(148, 149);
            this.stopBriefingButton.Name = "stopBriefingButton";
            this.stopBriefingButton.Size = new System.Drawing.Size(142, 23);
            this.stopBriefingButton.TabIndex = 16;
            this.stopBriefingButton.Text = "Stop Briefing";
            this.stopBriefingButton.UseVisualStyleBackColor = true;
            this.stopBriefingButton.Click += new System.EventHandler(this.stopBriefingButton_Click);
            // 
            // frameTimer
            // 
            this.frameTimer.Interval = 20;
            this.frameTimer.Tick += new System.EventHandler(this.frameTimer_Tick);
            // 
            // briefingNameTextBox
            // 
            this.briefingNameTextBox.Location = new System.Drawing.Point(196, 65);
            this.briefingNameTextBox.Name = "briefingNameTextBox";
            this.briefingNameTextBox.Size = new System.Drawing.Size(94, 20);
            this.briefingNameTextBox.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(145, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Briefing:";
            // 
            // workingHogLabel
            // 
            this.workingHogLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.workingHogLabel.AutoSize = true;
            this.workingHogLabel.Location = new System.Drawing.Point(-3, 255);
            this.workingHogLabel.Name = "workingHogLabel";
            this.workingHogLabel.Size = new System.Drawing.Size(130, 13);
            this.workingHogLabel.TabIndex = 19;
            this.workingHogLabel.Text = "No Working HOG opened";
            // 
            // playingBriefingLabel
            // 
            this.playingBriefingLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.playingBriefingLabel.AutoSize = true;
            this.playingBriefingLabel.Location = new System.Drawing.Point(-3, 277);
            this.playingBriefingLabel.Name = "playingBriefingLabel";
            this.playingBriefingLabel.Size = new System.Drawing.Size(24, 13);
            this.playingBriefingLabel.TabIndex = 20;
            this.playingBriefingLabel.Text = "Idle";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(145, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 39);
            this.label3.TabIndex = 21;
            this.label3.Text = "before level N\r\nfor D1, intro=0\r\nfor D2, intro=1";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(0, 32);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(142, 23);
            this.button3.TabIndex = 22;
            this.button3.Text = "Settings";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // newEditorButton
            // 
            this.newEditorButton.Location = new System.Drawing.Point(148, 91);
            this.newEditorButton.Name = "newEditorButton";
            this.newEditorButton.Size = new System.Drawing.Size(142, 23);
            this.newEditorButton.TabIndex = 23;
            this.newEditorButton.Text = "Briefing Editor";
            this.newEditorButton.UseVisualStyleBackColor = true;
            this.newEditorButton.Click += new System.EventHandler(this.newEditorButton_Click);
            // 
            // bannerGeneratorButton
            // 
            this.bannerGeneratorButton.Location = new System.Drawing.Point(0, 63);
            this.bannerGeneratorButton.Name = "bannerGeneratorButton";
            this.bannerGeneratorButton.Size = new System.Drawing.Size(142, 23);
            this.bannerGeneratorButton.TabIndex = 24;
            this.bannerGeneratorButton.Text = "Banner Generator";
            this.bannerGeneratorButton.UseVisualStyleBackColor = true;
            this.bannerGeneratorButton.Click += new System.EventHandler(this.bannerGeneratorButton_Click);
            // 
            // infoLabel
            // 
            this.infoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.infoLabel.Location = new System.Drawing.Point(205, 277);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(85, 19);
            this.infoLabel.TabIndex = 25;
            this.infoLabel.Text = "ziplantil 2019";
            this.infoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 294);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.bannerGeneratorButton);
            this.Controls.Add(this.newEditorButton);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.playingBriefingLabel);
            this.Controls.Add(this.workingHogLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.briefingNameTextBox);
            this.Controls.Add(this.stopBriefingButton);
            this.Controls.Add(this.levelNumericUpDown);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.playBriefingButton);
            this.Controls.Add(this.closeWorkingHogButton);
            this.Controls.Add(this.txbEditorButton);
            this.Controls.Add(this.baseHogLabel);
            this.Controls.Add(this.highResScreenButton);
            this.Controls.Add(this.lowResScreenButton);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Descent Briefing Studio";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.levelNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog descentHogOpenFileDialog;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.OpenFileDialog workingHogOpenFileDialog;
        private System.Windows.Forms.Button lowResScreenButton;
        private System.Windows.Forms.Button highResScreenButton;
        private System.Windows.Forms.Label baseHogLabel;
        private System.Windows.Forms.Button txbEditorButton;
        private System.Windows.Forms.Button closeWorkingHogButton;
        private System.Windows.Forms.Button playBriefingButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown levelNumericUpDown;
        private System.Windows.Forms.Button stopBriefingButton;
        private System.Windows.Forms.Timer frameTimer;
        private System.Windows.Forms.TextBox briefingNameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label workingHogLabel;
        private System.Windows.Forms.Label playingBriefingLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button newEditorButton;
        private System.Windows.Forms.Button bannerGeneratorButton;
        private System.Windows.Forms.Label infoLabel;
    }
}

