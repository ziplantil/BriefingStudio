namespace BriefingStudio.UI
{
    partial class SettingsForm
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
            this.allowD2XColorsCheckBox = new System.Windows.Forms.CheckBox();
            this.addEndSectionCheckBox = new System.Windows.Forms.CheckBox();
            this.resetButton = new System.Windows.Forms.Button();
            this.showBriefingBoxCheckBox = new System.Windows.Forms.CheckBox();
            this.fadeTransitionsCheckBox = new System.Windows.Forms.CheckBox();
            this.upscaleLowResCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // allowD2XColorsCheckBox
            // 
            this.allowD2XColorsCheckBox.AutoSize = true;
            this.allowD2XColorsCheckBox.Location = new System.Drawing.Point(3, 12);
            this.allowD2XColorsCheckBox.Name = "allowD2XColorsCheckBox";
            this.allowD2XColorsCheckBox.Size = new System.Drawing.Size(163, 17);
            this.allowD2XColorsCheckBox.TabIndex = 0;
            this.allowD2XColorsCheckBox.Text = "Enable DXX color extensions";
            this.allowD2XColorsCheckBox.UseVisualStyleBackColor = true;
            this.allowD2XColorsCheckBox.CheckedChanged += new System.EventHandler(this.allowD2XColorsCheckBox_CheckedChanged);
            // 
            // addEndSectionCheckBox
            // 
            this.addEndSectionCheckBox.AutoSize = true;
            this.addEndSectionCheckBox.Location = new System.Drawing.Point(3, 35);
            this.addEndSectionCheckBox.Name = "addEndSectionCheckBox";
            this.addEndSectionCheckBox.Size = new System.Drawing.Size(256, 17);
            this.addEndSectionCheckBox.TabIndex = 1;
            this.addEndSectionCheckBox.Text = "Automatically add dummy section to end on save";
            this.addEndSectionCheckBox.UseVisualStyleBackColor = true;
            this.addEndSectionCheckBox.CheckedChanged += new System.EventHandler(this.addEndSectionCheckBox_CheckedChanged);
            // 
            // resetButton
            // 
            this.resetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.resetButton.Location = new System.Drawing.Point(222, 295);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 2;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // showBriefingBoxCheckBox
            // 
            this.showBriefingBoxCheckBox.AutoSize = true;
            this.showBriefingBoxCheckBox.Location = new System.Drawing.Point(3, 58);
            this.showBriefingBoxCheckBox.Name = "showBriefingBoxCheckBox";
            this.showBriefingBoxCheckBox.Size = new System.Drawing.Size(142, 17);
            this.showBriefingBoxCheckBox.TabIndex = 3;
            this.showBriefingBoxCheckBox.Text = "Show briefing text region";
            this.showBriefingBoxCheckBox.UseVisualStyleBackColor = true;
            this.showBriefingBoxCheckBox.CheckedChanged += new System.EventHandler(this.showBriefingBoxCheckBox_CheckedChanged);
            // 
            // fadeTransitionsCheckBox
            // 
            this.fadeTransitionsCheckBox.AutoSize = true;
            this.fadeTransitionsCheckBox.Location = new System.Drawing.Point(3, 81);
            this.fadeTransitionsCheckBox.Name = "fadeTransitionsCheckBox";
            this.fadeTransitionsCheckBox.Size = new System.Drawing.Size(127, 17);
            this.fadeTransitionsCheckBox.TabIndex = 4;
            this.fadeTransitionsCheckBox.Text = "Show fade transitions";
            this.fadeTransitionsCheckBox.UseVisualStyleBackColor = true;
            this.fadeTransitionsCheckBox.CheckedChanged += new System.EventHandler(this.fadeTransitionsCheckBox_CheckedChanged);
            // 
            // upscaleLowResCheckBox
            // 
            this.upscaleLowResCheckBox.AutoSize = true;
            this.upscaleLowResCheckBox.Location = new System.Drawing.Point(3, 104);
            this.upscaleLowResCheckBox.Name = "upscaleLowResCheckBox";
            this.upscaleLowResCheckBox.Size = new System.Drawing.Size(144, 17);
            this.upscaleLowResCheckBox.TabIndex = 5;
            this.upscaleLowResCheckBox.Text = "Up-scale low-res preview";
            this.upscaleLowResCheckBox.UseVisualStyleBackColor = true;
            this.upscaleLowResCheckBox.CheckedChanged += new System.EventHandler(this.upscaleLowResCheckBox_CheckedChanged);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 319);
            this.Controls.Add(this.upscaleLowResCheckBox);
            this.Controls.Add(this.fadeTransitionsCheckBox);
            this.Controls.Add(this.showBriefingBoxCheckBox);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.addEndSectionCheckBox);
            this.Controls.Add(this.allowD2XColorsCheckBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.Text = "Descent Briefing Studio Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox allowD2XColorsCheckBox;
        private System.Windows.Forms.CheckBox addEndSectionCheckBox;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.CheckBox showBriefingBoxCheckBox;
        private System.Windows.Forms.CheckBox fadeTransitionsCheckBox;
        private System.Windows.Forms.CheckBox upscaleLowResCheckBox;
    }
}