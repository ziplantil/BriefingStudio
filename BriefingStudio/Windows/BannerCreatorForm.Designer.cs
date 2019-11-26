namespace BriefingStudio
{
    partial class BannerCreatorForm
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
            this.textTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.makeButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.fontTextBox = new System.Windows.Forms.TextBox();
            this.bannerSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // textTextBox
            // 
            this.textTextBox.Location = new System.Drawing.Point(85, 6);
            this.textTextBox.Name = "textTextBox";
            this.textTextBox.Size = new System.Drawing.Size(298, 20);
            this.textTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-2, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Text to render";
            // 
            // makeButton
            // 
            this.makeButton.Location = new System.Drawing.Point(157, 73);
            this.makeButton.Name = "makeButton";
            this.makeButton.Size = new System.Drawing.Size(75, 23);
            this.makeButton.TabIndex = 2;
            this.makeButton.Text = "Make";
            this.makeButton.UseVisualStyleBackColor = true;
            this.makeButton.Click += new System.EventHandler(this.makeButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-2, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Font";
            // 
            // fontTextBox
            // 
            this.fontTextBox.Location = new System.Drawing.Point(85, 36);
            this.fontTextBox.Name = "fontTextBox";
            this.fontTextBox.Size = new System.Drawing.Size(298, 20);
            this.fontTextBox.TabIndex = 4;
            this.fontTextBox.Text = "FONT1-1.FNT";
            // 
            // bannerSaveFileDialog
            // 
            this.bannerSaveFileDialog.Filter = "Bitmap files (*.BMP)|*.BMP";
            this.bannerSaveFileDialog.RestoreDirectory = true;
            this.bannerSaveFileDialog.Title = "Save banner to";
            this.bannerSaveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.bannerSaveFileDialog_FileOk);
            // 
            // BannerCreatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 104);
            this.Controls.Add(this.fontTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.makeButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "BannerCreatorForm";
            this.Text = "Descent font banner creator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BannerCreatorForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button makeButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox fontTextBox;
        private System.Windows.Forms.SaveFileDialog bannerSaveFileDialog;
    }
}