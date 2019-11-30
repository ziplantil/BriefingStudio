namespace BriefingStudio.UI
{
    partial class InteractiveEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InteractiveEditorForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.importTXBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportTXBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemAutoPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editorPanel = new System.Windows.Forms.Panel();
            this.editorTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.briefingTextBox = new BriefingStudio.UI.RichTextBoxEx();
            this.editorToolStrip = new System.Windows.Forms.ToolStrip();
            this.cutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.copyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.pasteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonGreen = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonGray = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonBlue = new System.Windows.Forms.ToolStripButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.listPanel = new System.Windows.Forms.Panel();
            this.screenListBox = new System.Windows.Forms.ListBox();
            this.screensToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAddScreen = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRemoveScreen = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonMoveUp = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonMoveDown = new System.Windows.Forms.ToolStripButton();
            this.levelComboBox = new System.Windows.Forms.ComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.briefingToolStrip = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.levelToolStripNumericUpDown = new BriefingStudio.UI.ToolStripNumericUpDown();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.briefingOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.briefingSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.textEditDebounceTimer = new System.Windows.Forms.Timer(this.components);
            this.importTxbFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.exportTxbFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip.SuspendLayout();
            this.editorPanel.SuspendLayout();
            this.editorTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.editorToolStrip.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.listPanel.SuspendLayout();
            this.screensToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.briefingToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.compileToolStripMenuItem,
            this.previewToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.ShowItemToolTips = true;
            this.menuStrip.Size = new System.Drawing.Size(584, 24);
            this.menuStrip.TabIndex = 0;
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(12, 20);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveasToolStripMenuItem,
            this.toolStripSeparator3,
            this.importTXBToolStripMenuItem,
            this.exportTXBToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveasToolStripMenuItem
            // 
            this.saveasToolStripMenuItem.Name = "saveasToolStripMenuItem";
            this.saveasToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveasToolStripMenuItem.Text = "Save &As...";
            this.saveasToolStripMenuItem.Click += new System.EventHandler(this.saveasToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(143, 6);
            // 
            // importTXBToolStripMenuItem
            // 
            this.importTXBToolStripMenuItem.Name = "importTXBToolStripMenuItem";
            this.importTXBToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.importTXBToolStripMenuItem.Text = "&Import TXB...";
            this.importTXBToolStripMenuItem.Click += new System.EventHandler(this.importTXBToolStripMenuItem_Click);
            // 
            // exportTXBToolStripMenuItem
            // 
            this.exportTXBToolStripMenuItem.Name = "exportTXBToolStripMenuItem";
            this.exportTXBToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exportTXBToolStripMenuItem.Text = "&Export TXB...";
            this.exportTXBToolStripMenuItem.Click += new System.EventHandler(this.exportTXBToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(143, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newScreenToolStripMenuItem,
            this.deleteScreenToolStripMenuItem,
            this.toolStripSeparator4,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator5,
            this.toolStripMenuItemAutoPreview});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // newScreenToolStripMenuItem
            // 
            this.newScreenToolStripMenuItem.Name = "newScreenToolStripMenuItem";
            this.newScreenToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.newScreenToolStripMenuItem.Text = "&New Screen";
            this.newScreenToolStripMenuItem.Click += new System.EventHandler(this.newScreenToolStripMenuItem_Click);
            // 
            // deleteScreenToolStripMenuItem
            // 
            this.deleteScreenToolStripMenuItem.Name = "deleteScreenToolStripMenuItem";
            this.deleteScreenToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.deleteScreenToolStripMenuItem.Text = "&Delete Screen";
            this.deleteScreenToolStripMenuItem.Click += new System.EventHandler(this.deleteScreenToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(142, 6);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.cutToolStripMenuItem.Text = "Cu&t";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.pasteToolStripMenuItem.Text = "&Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(142, 6);
            // 
            // toolStripMenuItemAutoPreview
            // 
            this.toolStripMenuItemAutoPreview.CheckOnClick = true;
            this.toolStripMenuItemAutoPreview.Name = "toolStripMenuItemAutoPreview";
            this.toolStripMenuItemAutoPreview.Size = new System.Drawing.Size(145, 22);
            this.toolStripMenuItemAutoPreview.Text = "Auto Preview";
            this.toolStripMenuItemAutoPreview.CheckedChanged += new System.EventHandler(this.toolStripMenuItemAutoPreview_CheckedChanged);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // compileToolStripMenuItem
            // 
            this.compileToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.compileToolStripMenuItem.Name = "compileToolStripMenuItem";
            this.compileToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.compileToolStripMenuItem.Text = "&Compile";
            this.compileToolStripMenuItem.ToolTipText = "Compiles a .TXB file in order to allow a full preview. The compiled file is enter" +
    "ed into the TXB editor.";
            this.compileToolStripMenuItem.Click += new System.EventHandler(this.compileToolStripMenuItem_Click);
            // 
            // previewToolStripMenuItem
            // 
            this.previewToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.previewToolStripMenuItem.Name = "previewToolStripMenuItem";
            this.previewToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.previewToolStripMenuItem.Text = "&Preview";
            this.previewToolStripMenuItem.ToolTipText = "Renders the current screen onto the open low-res and high-res preview windows.";
            this.previewToolStripMenuItem.Click += new System.EventHandler(this.previewToolStripMenuItem_Click);
            // 
            // editorPanel
            // 
            this.editorPanel.Controls.Add(this.editorTabControl);
            this.editorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorPanel.Location = new System.Drawing.Point(0, 0);
            this.editorPanel.Name = "editorPanel";
            this.editorPanel.Size = new System.Drawing.Size(444, 311);
            this.editorPanel.TabIndex = 3;
            // 
            // editorTabControl
            // 
            this.editorTabControl.Controls.Add(this.tabPage1);
            this.editorTabControl.Controls.Add(this.tabPage2);
            this.editorTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorTabControl.Location = new System.Drawing.Point(0, 0);
            this.editorTabControl.Name = "editorTabControl";
            this.editorTabControl.SelectedIndex = 0;
            this.editorTabControl.Size = new System.Drawing.Size(444, 311);
            this.editorTabControl.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.briefingTextBox);
            this.tabPage1.Controls.Add(this.editorToolStrip);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(436, 285);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Text";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // briefingTextBox
            // 
            this.briefingTextBox.BackColor = System.Drawing.Color.Black;
            this.briefingTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.briefingTextBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.briefingTextBox.ForeColor = System.Drawing.Color.Lime;
            this.briefingTextBox.Location = new System.Drawing.Point(3, 28);
            this.briefingTextBox.MaxLength = 5000;
            this.briefingTextBox.Name = "briefingTextBox";
            this.briefingTextBox.Size = new System.Drawing.Size(430, 254);
            this.briefingTextBox.TabIndex = 1;
            this.briefingTextBox.Text = "";
            this.briefingTextBox.Pasted += new System.EventHandler(this.briefingTextBox_Pasted);
            this.briefingTextBox.SelectionChanged += new System.EventHandler(this.briefingTextBox_SelectionChanged);
            this.briefingTextBox.TextChanged += new System.EventHandler(this.briefingTextBox_TextChanged);
            // 
            // editorToolStrip
            // 
            this.editorToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripButton,
            this.copyToolStripButton,
            this.pasteToolStripButton,
            this.toolStripSeparator1,
            this.toolStripButtonGreen,
            this.toolStripButtonGray,
            this.toolStripButtonBlue});
            this.editorToolStrip.Location = new System.Drawing.Point(3, 3);
            this.editorToolStrip.Name = "editorToolStrip";
            this.editorToolStrip.Size = new System.Drawing.Size(430, 25);
            this.editorToolStrip.TabIndex = 0;
            this.editorToolStrip.Text = "toolStrip1";
            // 
            // cutToolStripButton
            // 
            this.cutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cutToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripButton.Image")));
            this.cutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutToolStripButton.Name = "cutToolStripButton";
            this.cutToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.cutToolStripButton.Text = "C&ut";
            this.cutToolStripButton.Click += new System.EventHandler(this.cutToolStripButton_Click);
            // 
            // copyToolStripButton
            // 
            this.copyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copyToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripButton.Image")));
            this.copyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolStripButton.Name = "copyToolStripButton";
            this.copyToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.copyToolStripButton.Text = "&Copy";
            this.copyToolStripButton.Click += new System.EventHandler(this.copyToolStripButton_Click);
            // 
            // pasteToolStripButton
            // 
            this.pasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pasteToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripButton.Image")));
            this.pasteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteToolStripButton.Name = "pasteToolStripButton";
            this.pasteToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.pasteToolStripButton.Text = "&Paste";
            this.pasteToolStripButton.Click += new System.EventHandler(this.pasteToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonGreen
            // 
            this.toolStripButtonGreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonGreen.Image = global::BriefingStudio.Properties.Resources.toolStripButtonGreen_Image;
            this.toolStripButtonGreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonGreen.Name = "toolStripButtonGreen";
            this.toolStripButtonGreen.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonGreen.Text = "Green Text";
            this.toolStripButtonGreen.Click += new System.EventHandler(this.toolStripButtonGreen_Click);
            // 
            // toolStripButtonGray
            // 
            this.toolStripButtonGray.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonGray.Image = global::BriefingStudio.Properties.Resources.toolStripButtonGray_Image;
            this.toolStripButtonGray.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonGray.Name = "toolStripButtonGray";
            this.toolStripButtonGray.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonGray.Text = "Brown Text";
            this.toolStripButtonGray.Click += new System.EventHandler(this.toolStripButtonGray_Click);
            // 
            // toolStripButtonBlue
            // 
            this.toolStripButtonBlue.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonBlue.Image = global::BriefingStudio.Properties.Resources.toolStripButtonBlue_Image;
            this.toolStripButtonBlue.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonBlue.Name = "toolStripButtonBlue";
            this.toolStripButtonBlue.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonBlue.Text = "Blue Text";
            this.toolStripButtonBlue.Click += new System.EventHandler(this.toolStripButtonBlue_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.propertyGrid);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(436, 285);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Properties";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.LineColor = System.Drawing.SystemColors.ControlDarkDark;
            this.propertyGrid.Location = new System.Drawing.Point(3, 3);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(430, 279);
            this.propertyGrid.TabIndex = 1;
            this.propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
            // 
            // listPanel
            // 
            this.listPanel.Controls.Add(this.screenListBox);
            this.listPanel.Controls.Add(this.screensToolStrip);
            this.listPanel.Controls.Add(this.levelComboBox);
            this.listPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listPanel.Location = new System.Drawing.Point(0, 0);
            this.listPanel.Name = "listPanel";
            this.listPanel.Size = new System.Drawing.Size(136, 311);
            this.listPanel.TabIndex = 5;
            // 
            // screenListBox
            // 
            this.screenListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.screenListBox.FormattingEnabled = true;
            this.screenListBox.Location = new System.Drawing.Point(0, 21);
            this.screenListBox.Name = "screenListBox";
            this.screenListBox.Size = new System.Drawing.Size(136, 265);
            this.screenListBox.TabIndex = 0;
            this.screenListBox.SelectedIndexChanged += new System.EventHandler(this.screenListBox_SelectedIndexChanged);
            // 
            // screensToolStrip
            // 
            this.screensToolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.screensToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAddScreen,
            this.toolStripButtonRemoveScreen,
            this.toolStripButtonMoveUp,
            this.toolStripButtonMoveDown});
            this.screensToolStrip.Location = new System.Drawing.Point(0, 286);
            this.screensToolStrip.Name = "screensToolStrip";
            this.screensToolStrip.Size = new System.Drawing.Size(136, 25);
            this.screensToolStrip.TabIndex = 2;
            this.screensToolStrip.Text = "toolStrip2";
            // 
            // toolStripButtonAddScreen
            // 
            this.toolStripButtonAddScreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonAddScreen.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddScreen.Image")));
            this.toolStripButtonAddScreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddScreen.Name = "toolStripButtonAddScreen";
            this.toolStripButtonAddScreen.Size = new System.Drawing.Size(33, 22);
            this.toolStripButtonAddScreen.Text = "Add";
            this.toolStripButtonAddScreen.Click += new System.EventHandler(this.toolStripButtonAddScreen_Click);
            // 
            // toolStripButtonRemoveScreen
            // 
            this.toolStripButtonRemoveScreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonRemoveScreen.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRemoveScreen.Image")));
            this.toolStripButtonRemoveScreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRemoveScreen.Name = "toolStripButtonRemoveScreen";
            this.toolStripButtonRemoveScreen.Size = new System.Drawing.Size(28, 22);
            this.toolStripButtonRemoveScreen.Text = "Del";
            this.toolStripButtonRemoveScreen.Click += new System.EventHandler(this.toolStripButtonRemoveScreen_Click);
            // 
            // toolStripButtonMoveUp
            // 
            this.toolStripButtonMoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonMoveUp.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonMoveUp.Image")));
            this.toolStripButtonMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMoveUp.Name = "toolStripButtonMoveUp";
            this.toolStripButtonMoveUp.Size = new System.Drawing.Size(26, 22);
            this.toolStripButtonMoveUp.Text = "Up";
            this.toolStripButtonMoveUp.Click += new System.EventHandler(this.toolStripButtonMoveUp_Click);
            // 
            // toolStripButtonMoveDown
            // 
            this.toolStripButtonMoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonMoveDown.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonMoveDown.Image")));
            this.toolStripButtonMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMoveDown.Name = "toolStripButtonMoveDown";
            this.toolStripButtonMoveDown.Size = new System.Drawing.Size(26, 22);
            this.toolStripButtonMoveDown.Text = "Dn";
            this.toolStripButtonMoveDown.Click += new System.EventHandler(this.toolStripButtonMoveDown_Click);
            // 
            // levelComboBox
            // 
            this.levelComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.levelComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.levelComboBox.FormattingEnabled = true;
            this.levelComboBox.Location = new System.Drawing.Point(0, 0);
            this.levelComboBox.Name = "levelComboBox";
            this.levelComboBox.Size = new System.Drawing.Size(136, 21);
            this.levelComboBox.TabIndex = 1;
            this.levelComboBox.SelectedIndexChanged += new System.EventHandler(this.levelComboBox_SelectedIndexChanged);
            this.levelComboBox.SelectedValueChanged += new System.EventHandler(this.levelComboBox_SelectedValueChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 50);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.editorPanel);
            this.splitContainer1.Size = new System.Drawing.Size(584, 311);
            this.splitContainer1.SplitterDistance = 136;
            this.splitContainer1.TabIndex = 6;
            // 
            // briefingToolStrip
            // 
            this.briefingToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.levelToolStripNumericUpDown,
            this.toolStripLabel1});
            this.briefingToolStrip.Location = new System.Drawing.Point(0, 24);
            this.briefingToolStrip.Name = "briefingToolStrip";
            this.briefingToolStrip.Size = new System.Drawing.Size(584, 26);
            this.briefingToolStrip.TabIndex = 7;
            this.briefingToolStrip.Text = "toolStrip3";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(23, 23);
            this.newToolStripButton.Text = "&New";
            this.newToolStripButton.Click += new System.EventHandler(this.newToolStripButton_Click);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 23);
            this.openToolStripButton.Text = "&Open";
            this.openToolStripButton.Click += new System.EventHandler(this.openToolStripButton_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 23);
            this.saveToolStripButton.Text = "&Save";
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // levelToolStripNumericUpDown
            // 
            this.levelToolStripNumericUpDown.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.levelToolStripNumericUpDown.Name = "levelToolStripNumericUpDown";
            this.levelToolStripNumericUpDown.Size = new System.Drawing.Size(41, 23);
            this.levelToolStripNumericUpDown.Text = "1";
            this.levelToolStripNumericUpDown.Click += new System.EventHandler(this.levelToolStripNumericUpDown_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(70, 23);
            this.toolStripLabel1.Text = "Level Count";
            // 
            // briefingOpenFileDialog
            // 
            this.briefingOpenFileDialog.Filter = "Briefing project files (*.dbp)|*.dbp|All files|*.*";
            this.briefingOpenFileDialog.RestoreDirectory = true;
            this.briefingOpenFileDialog.Title = "Open briefing project";
            this.briefingOpenFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.briefingOpenFileDialog_FileOk);
            // 
            // briefingSaveFileDialog
            // 
            this.briefingSaveFileDialog.Filter = "Briefing project files (*.dbp)|*.dbp|All files|*.*";
            this.briefingSaveFileDialog.RestoreDirectory = true;
            this.briefingSaveFileDialog.Title = "Save briefing project";
            this.briefingSaveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.briefingSaveFileDialog_FileOk);
            // 
            // textEditDebounceTimer
            // 
            this.textEditDebounceTimer.Interval = 500;
            this.textEditDebounceTimer.Tick += new System.EventHandler(this.textEditDebounceTimer_Tick);
            // 
            // importTxbFileDialog
            // 
            this.importTxbFileDialog.Filter = "TXB files (*.TXB)|*.TXB|All files|*.*";
            this.importTxbFileDialog.Title = "Import TXB...";
            this.importTxbFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.importTxbFileDialog_FileOk);
            // 
            // exportTxbFileDialog
            // 
            this.exportTxbFileDialog.Filter = "TXB files (*.TXB)|*.TXB|All files|*.*";
            this.exportTxbFileDialog.RestoreDirectory = true;
            this.exportTxbFileDialog.Title = "Export TXB...";
            this.exportTxbFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.exportTxbFileDialog_FileOk);
            // 
            // InteractiveEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.briefingToolStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "InteractiveEditorForm";
            this.ShowIcon = false;
            this.Text = "Interactive Briefing Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InteractiveEditorForm_FormClosing);
            this.Load += new System.EventHandler(this.InteractiveEditorForm_Load);
            this.Shown += new System.EventHandler(this.InteractiveEditorForm_Shown);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.editorPanel.ResumeLayout(false);
            this.editorTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.editorToolStrip.ResumeLayout(false);
            this.editorToolStrip.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.listPanel.ResumeLayout(false);
            this.listPanel.PerformLayout();
            this.screensToolStrip.ResumeLayout(false);
            this.screensToolStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.briefingToolStrip.ResumeLayout(false);
            this.briefingToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compileToolStripMenuItem;
        private System.Windows.Forms.Panel editorPanel;
        private System.Windows.Forms.ToolStrip editorToolStrip;
        private System.Windows.Forms.Panel listPanel;
        private System.Windows.Forms.ListBox screenListBox;
        private System.Windows.Forms.ComboBox levelComboBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripButton cutToolStripButton;
        private System.Windows.Forms.ToolStripButton copyToolStripButton;
        private System.Windows.Forms.ToolStripButton pasteToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonGreen;
        private System.Windows.Forms.ToolStripButton toolStripButtonGray;
        private System.Windows.Forms.ToolStripButton toolStripButtonBlue;
        private System.Windows.Forms.ToolStrip screensToolStrip;
        private BriefingStudio.UI.RichTextBoxEx briefingTextBox;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddScreen;
        private System.Windows.Forms.ToolStripButton toolStripButtonRemoveScreen;
        private System.Windows.Forms.ToolStrip briefingToolStrip;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private ToolStripNumericUpDown levelToolStripNumericUpDown;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveasToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newScreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteScreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog briefingOpenFileDialog;
        private System.Windows.Forms.SaveFileDialog briefingSaveFileDialog;
        private System.Windows.Forms.TabControl editorTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripMenuItem previewToolStripMenuItem;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.Timer textEditDebounceTimer;
        private System.Windows.Forms.ToolStripButton toolStripButtonMoveUp;
        private System.Windows.Forms.ToolStripButton toolStripButtonMoveDown;
        private System.Windows.Forms.ToolStripMenuItem importTXBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportTXBToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.OpenFileDialog importTxbFileDialog;
        private System.Windows.Forms.SaveFileDialog exportTxbFileDialog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAutoPreview;
    }
}