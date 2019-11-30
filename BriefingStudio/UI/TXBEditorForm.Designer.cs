namespace BriefingStudio.UI
{
    partial class TXBEditorForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.briefingNameTextBox = new System.Windows.Forms.TextBox();
            this.txbBox = new System.Windows.Forms.TextBox();
            this.reloadButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.playButton = new System.Windows.Forms.Button();
            this.sequenceNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.jumpButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.syntaxHelpButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.sequenceNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "File name: ";
            // 
            // briefingNameTextBox
            // 
            this.briefingNameTextBox.Location = new System.Drawing.Point(61, 6);
            this.briefingNameTextBox.Name = "briefingNameTextBox";
            this.briefingNameTextBox.Size = new System.Drawing.Size(130, 20);
            this.briefingNameTextBox.TabIndex = 1;
            // 
            // txbBox
            // 
            this.txbBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbBox.Location = new System.Drawing.Point(6, 32);
            this.txbBox.Multiline = true;
            this.txbBox.Name = "txbBox";
            this.txbBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txbBox.Size = new System.Drawing.Size(573, 397);
            this.txbBox.TabIndex = 2;
            this.txbBox.WordWrap = false;
            this.txbBox.TextChanged += new System.EventHandler(this.txbBox_TextChanged);
            // 
            // reloadButton
            // 
            this.reloadButton.Location = new System.Drawing.Point(197, 3);
            this.reloadButton.Name = "reloadButton";
            this.reloadButton.Size = new System.Drawing.Size(59, 23);
            this.reloadButton.TabIndex = 3;
            this.reloadButton.Text = "Reload";
            this.reloadButton.UseVisualStyleBackColor = true;
            this.reloadButton.Click += new System.EventHandler(this.reloadButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point(262, 3);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(59, 23);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.stopButton.Location = new System.Drawing.Point(540, 4);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(39, 23);
            this.stopButton.TabIndex = 5;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // playButton
            // 
            this.playButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.playButton.Location = new System.Drawing.Point(495, 4);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(39, 23);
            this.playButton.TabIndex = 6;
            this.playButton.Text = "Play";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // sequenceNumericUpDown
            // 
            this.sequenceNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sequenceNumericUpDown.Location = new System.Drawing.Point(383, 6);
            this.sequenceNumericUpDown.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.sequenceNumericUpDown.Name = "sequenceNumericUpDown";
            this.sequenceNumericUpDown.Size = new System.Drawing.Size(62, 20);
            this.sequenceNumericUpDown.TabIndex = 7;
            this.sequenceNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // jumpButton
            // 
            this.jumpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.jumpButton.Location = new System.Drawing.Point(451, 3);
            this.jumpButton.Name = "jumpButton";
            this.jumpButton.Size = new System.Drawing.Size(39, 23);
            this.jumpButton.TabIndex = 8;
            this.jumpButton.Text = "Go";
            this.jumpButton.UseVisualStyleBackColor = true;
            this.jumpButton.Click += new System.EventHandler(this.jumpButton_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(351, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Seq";
            // 
            // syntaxHelpButton
            // 
            this.syntaxHelpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.syntaxHelpButton.Location = new System.Drawing.Point(354, 435);
            this.syntaxHelpButton.Name = "syntaxHelpButton";
            this.syntaxHelpButton.Size = new System.Drawing.Size(225, 23);
            this.syntaxHelpButton.TabIndex = 11;
            this.syntaxHelpButton.Text = "Syntax help";
            this.syntaxHelpButton.UseVisualStyleBackColor = true;
            this.syntaxHelpButton.Click += new System.EventHandler(this.syntaxHelpButton_Click);
            // 
            // TXBEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 461);
            this.Controls.Add(this.syntaxHelpButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.jumpButton);
            this.Controls.Add(this.sequenceNumericUpDown);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.reloadButton);
            this.Controls.Add(this.txbBox);
            this.Controls.Add(this.briefingNameTextBox);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(600, 300);
            this.Name = "TXBEditorForm";
            this.ShowIcon = false;
            this.Text = "TXB Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TXBEditorForm_FormClosing);
            this.Load += new System.EventHandler(this.TXBEditorForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sequenceNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox briefingNameTextBox;
        private System.Windows.Forms.TextBox txbBox;
        private System.Windows.Forms.Button reloadButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.NumericUpDown sequenceNumericUpDown;
        private System.Windows.Forms.Button jumpButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button syntaxHelpButton;
    }
}