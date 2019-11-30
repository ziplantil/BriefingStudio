using BriefingStudio.Logic.Formats;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BriefingStudio.UI
{
    public partial class InteractiveEditorForm : Form
    {
        public delegate byte[] FindFile(string filename);
        public delegate void PlayBriefing(string text, int n, bool instant);
        public delegate void StopBriefing();
        public delegate void SubmitBriefing(string contents);

        private string editingFilePath = null;
        private FindFile findFile;
        private PlayBriefing playBriefing;
        private StopBriefing stopBriefing;
        private SubmitBriefing submitBriefing;
        private bool _unsaved;
        private BinaryFormatter bfmt;
        private int descentGame;
        private bool loadingRtf;

        private BriefingProject _project;
        private BriefingProject.BriefingLevel _level;
        private BriefingProject.BriefingScreen _screen;

        private static Color[] textColors = { Color.FromArgb(0, 252, 0), Color.FromArgb(204, 176, 144), Color.FromArgb(38, 146, 255) };

        private BriefingProject Project
        {
            get => _project;
            set
            {
                if (_project != value)
                {
                    Level = null;
                    levelComboBox.SelectedIndex = -1;
                }
                _project = value;
                saveasToolStripMenuItem.Enabled =
                    saveToolStripMenuItem.Enabled =
                    saveToolStripButton.Enabled =
                    levelComboBox.Enabled =
                    exportTXBToolStripMenuItem.Enabled =
                    compileToolStripMenuItem.Enabled = _project != null;
                UpdateLevelList();
            }
        }

        private BriefingProject.BriefingLevel Level
        {
            get => _level;
            set
            {
                if (_level != value)
                {
                    Screen = null;
                    screenListBox.ClearSelected();
                }
                _level = value;
                screenListBox.Enabled =
                    toolStripButtonAddScreen.Enabled =
                    newScreenToolStripMenuItem.Enabled = _level != null;
            }
        }

        private BriefingProject.BriefingScreen Screen
        {
            get => _screen;
            set
            {
                if (_screen != value)
                {
                    propertyGrid.SelectedObject = value;
                    loadingRtf = true;
                    if (value == null)
                    {
                        briefingTextBox.Text = "";
                        briefingTextBox.Modified = false;
                    }
                    else
                    {
                        ConvertDescentToRtf(briefingTextBox, value.Text);
                        briefingTextBox.Modified = false;
                    }
                    loadingRtf = false;
                }
                _screen = value;
                toolStripButtonRemoveScreen.Enabled =
                    toolStripButtonMoveUp.Enabled =
                    toolStripButtonMoveDown.Enabled =
                    editorTabControl.Enabled =
                    previewToolStripMenuItem.Enabled =
                    deleteScreenToolStripMenuItem.Enabled =
                    briefingTextBox.Enabled =
                    cutToolStripMenuItem.Enabled =
                    copyToolStripMenuItem.Enabled =
                    pasteToolStripMenuItem.Enabled =
                    editorToolStrip.Enabled = _screen != null;
                UpdateSelectionButtonEnabled();
            }
        }

        public bool Unsaved
        {
            get
            {
                return _unsaved;
            }
            set
            {
                _unsaved = value;
                this.UpdateTitle();
            }
        }

        public InteractiveEditorForm()
        {
            InitializeComponent();
            bfmt = new BinaryFormatter();
            loadingRtf = false;
        }

        public void SetDescentGame(int game)
        {
            descentGame = game;
            if (this.Visible && descentGame == 1)
            {
                this.Enabled = false;
                MessageBox.Show(this, "Descent I is not supported for the Interactive Briefing Editor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void UpdateLevelList()
        {
            object oldSelection = levelComboBox.SelectedItem;
            levelComboBox.Items.Clear();

            if (Project != null)
            {
                //levelComboBox.Items.Add("Intro (D1 Only)");
                for (int i = 1; i <= levelToolStripNumericUpDown.NumericUpDownControl.Value; ++i)
                {
                    levelComboBox.Items.Add("Before Level " + i);
                }
                levelComboBox.Items.Add("Outro");
            }

            if (oldSelection != null && levelComboBox.Items.Contains(oldSelection))
            {
                levelComboBox.SelectedItem = oldSelection;
                UpdateSelectedLevel();
            }
            else
            {
                levelComboBox.SelectedIndex = -1;
                Level = null;
            }
        }

        private string CleanAndTruncate(string text, int length)
        {
            text = text.Replace("$C1", "").Replace("$C2", "").Replace("$C3", "").Replace("\r", "").Replace("\n", " ");
            if (text.Length > length)
            {
                return text.Substring(0, length) + "...";
            }
            else
            {
                return text;
            }
        }

        private void UpdateScreenList()
        {
            int oldIndex = screenListBox.SelectedIndex;
            screenListBox.Items.Clear();

            if (Level != null)
            {
                int index = 1;

                foreach (BriefingProject.BriefingScreen screen in Level.Screens)
                {
                    screenListBox.Items.Add("Screen #" + index++ + ": " + CleanAndTruncate(screen.Text, 40));
                }
            }

            if (screenListBox.Items.Count > 0)
            {
                screenListBox.SelectedIndex = Math.Min(oldIndex, screenListBox.Items.Count - 1);
                UpdateSelectedScreen();
            }
            else
            {
                screenListBox.ClearSelected();
                Screen = null;
            }
        }

        public void UpdateSelectedLevel()
        {
            int index = levelComboBox.SelectedIndex;
            if (index >= 0)
            {
                int outroIndex = (int)levelToolStripNumericUpDown.NumericUpDownControl.Value + 1;
                /* if (index == 0)
                {
                    Level = Project.Intro;
                }
                else */
                if (index == outroIndex)
                {
                    Level = Project.Outro;
                }
                else
                {
                    int levelIndex = index; // - 1;
                    while (Project.Levels.Count <= levelIndex)
                    {
                        Project.Levels.Add(new BriefingProject.BriefingLevel());
                    }
                    Level = Project.Levels[levelIndex];
                }
            }
            else
            {
                Level = null;
            }
            UpdateScreenList();
        }

        private void UpdateSelectedScreen()
        {
            if (Screen != null)
            {
                Screen.Text = ConvertRtfToDescent(briefingTextBox);
            }
            int index = screenListBox.SelectedIndex;
            if (index >= 0)
            {
                Screen = Level.Screens[index];
            }
            else
            {
                Screen = null;
            }
            MaybeUpdatePreview();
        }

        private void MaybeUpdatePreview()
        {
            if (Screen != null && toolStripMenuItemAutoPreview.Checked)
            {
                playBriefing("$S1\n" + Screen.ToBriefing(), 1, true);
            }
        }

        private void UpdateTitle()
        {
            this.Text = (Unsaved ? "*" : "") + "Interactive Briefing Editor";
            if (editingFilePath != null)
            {
                this.Text += " - " + editingFilePath;
            }
        }

        public void SetDelegates(FindFile findFile, PlayBriefing playBriefing, StopBriefing stopBriefing, SubmitBriefing submitBriefing)
        {
            this.findFile = findFile;
            this.playBriefing = playBriefing;
            this.stopBriefing = stopBriefing;
            this.submitBriefing = submitBriefing;
        }

        private void NewFile()
        {
            editingFilePath = null;
            Project = new BriefingProject();
            UpdateLevelList();
            levelComboBox.SelectedIndex = 0;
            UpdateSelectedLevel();
            levelToolStripNumericUpDown.NumericUpDownControl.Value = 1;
            Unsaved = false;
        }

        private void OpenFile()
        {
            Unsaved = false;
            try
            {
                using (FileStream fs = File.Open(editingFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    Project = (BriefingProject)bfmt.Deserialize(fs);
                }
                levelToolStripNumericUpDown.NumericUpDownControl.Value = Project.LevelCount;
                levelComboBox.SelectedIndex = 0;
                UpdateSelectedLevel();
                Unsaved = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Could not open briefing project file.\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Project = null;
            }
        }

        private void SaveFile()
        {
            Unsaved = false;
            if (Screen != null)
            {
                Screen.Text = ConvertRtfToDescent(briefingTextBox);
            }
            try
            {
                Project.LevelCount = (int)levelToolStripNumericUpDown.NumericUpDownControl.Value;
                using (FileStream fs = File.Open(editingFilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                {
                    bfmt.Serialize(fs, Project);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Could not save briefing project file.\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Project = null;
            }
        }

        private void GoSave()
        {
            // existing file? save, otherwise save as
            if (editingFilePath == null)
            {
                briefingSaveFileDialog.ShowDialog();
            }
            else
            {
                SaveFile();
            }
        }

        private bool ConfirmUnsaved()
        {
            if (!Unsaved)
            {
                return true;
            }

            DialogResult result = MessageBox.Show(this, "This briefing is unsaved. Do you want to save it before closing it?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (editingFilePath == null)
                {
                    briefingSaveFileDialog.ShowDialog();
                    return false;
                }
                else
                {
                    SaveFile();
                    return true;
                }
            }
            return result == DialogResult.No;
        }

        private void CloseFile()
        {
            editingFilePath = "";
            Project = null;
        }

        private void ConvertDescentToRtf(RichTextBox rtf, string desc)
        {
            rtf.Clear();

            // convert to RTF piece by piece
            rtf.SelectionColor = textColors[0];
            int previousIndex = 0, newIndex, lfIndex;
            desc += "$C0";
            while (previousIndex < desc.Length && (newIndex = desc.IndexOf("$C", previousIndex)) >= 0)
            {
                rtf.AppendText(desc.Substring(previousIndex, newIndex - previousIndex));
                // find number in text, check if range, otherwise skip and then to new line
                while (newIndex < desc.Length && (desc[newIndex] == ' ' || desc[newIndex] == '0')) newIndex++;
                if (newIndex >= desc.Length) break;
                if (Char.IsDigit(desc[newIndex]) && !Char.IsDigit(desc[newIndex + 1]))
                {
                    char c = desc[newIndex];
                    if (c == '1') rtf.SelectionColor = textColors[0];
                    if (c == '2') rtf.SelectionColor = textColors[1];
                    if (c == '3') rtf.SelectionColor = textColors[2];
                }
                // skip to new line
                lfIndex = desc.IndexOf("\n", newIndex);
                if (lfIndex < 0) lfIndex = desc.Length;
                previousIndex = lfIndex + 1;
            }
        }

        private string ConvertRtfToDescent(RichTextBoxEx rtf)
        {
            // go over colored sections
            // this is massively messy, but RichTextBox gives nothing else - apart from manually parsing .rtf
            // which nobody with even a shred of sanity left would attempt to do

            rtf.SuspendDrawing();

            string res = "$C1\n";
            int oldSelectionStart = rtf.SelectionStart;
            int oldSelectionLength = rtf.SelectionLength;

            int colorIndex = 0;
            Color c;
            int i = 0;
            while (i < rtf.Text.Length)
            {
                rtf.SelectionStart = i;
                rtf.SelectionLength = 1;
                while (rtf.SelectionColor != Color.Empty && i + rtf.SelectionLength < rtf.Text.Length)
                    ++rtf.SelectionLength;
                if (rtf.SelectionColor == Color.Empty)
                    --rtf.SelectionLength;

                c = rtf.SelectionColor;

                if (c.ToArgb() != textColors[colorIndex].ToArgb())
                {
                    if (c.ToArgb() == textColors[0].ToArgb())
                    {
                        res += "$C1\n";
                        colorIndex = 0;
                    }
                    if (c.ToArgb() == textColors[1].ToArgb())
                    {
                        res += "$C2\n";
                        colorIndex = 1;
                    }
                    if (c.ToArgb() == textColors[2].ToArgb())
                    {
                        if (descentGame == 1)
                            res += "$C2\n";
                        else
                            res += "$C3\n";
                        colorIndex = 2;
                    }
                }

                res += rtf.SelectedText;
                i += rtf.SelectionLength;
            }

            rtf.SelectionStart = oldSelectionStart;
            rtf.SelectionLength = oldSelectionLength;
            rtf.ResumeDrawing();
            return res;
        }

        private void ForceReformat()
        {
            int oldSelectionStart = briefingTextBox.SelectionStart;
            int oldSelectionLength = briefingTextBox.SelectionLength;
            loadingRtf = true;
            ConvertDescentToRtf(briefingTextBox, ConvertRtfToDescent(briefingTextBox));
            briefingTextBox.SelectionStart = oldSelectionStart;
            briefingTextBox.SelectionLength = oldSelectionLength;
            loadingRtf = false;
        }

        private void UpdateSelectionButtonEnabled()
        {
            bool selected = briefingTextBox.Enabled && briefingTextBox.SelectionLength > 0;
            cutToolStripMenuItem.Enabled =
                copyToolStripMenuItem.Enabled =
                cutToolStripButton.Enabled =
                copyToolStripButton.Enabled = selected;
        }

        private void AddNewScreen()
        {
            int newIndex = screenListBox.Items.Count;
            if (Screen != null)
                Level.Screens.Add(new BriefingProject.BriefingScreen(Screen));
            else
                Level.Screens.Add(new BriefingProject.BriefingScreen());
            UpdateScreenList();
            screenListBox.SelectedIndex = newIndex;
            UpdateSelectedScreen();
            Unsaved = true;
        }

        private void DeleteThisScreen()
        {
            if (DialogResult.Yes == MessageBox.Show(this, "Are you sure you want to delete this screen?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                Level.Screens.RemoveAt(screenListBox.SelectedIndex);
                UpdateScreenList();
                Unsaved = true;
            }
        }

        private void InteractiveEditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ConfirmUnsaved())
            {
                e.Cancel = true;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Briefing Editor by ziplantil 2019");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ConfirmUnsaved())
            {
                this.Close();
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ConfirmUnsaved())
            {
                CloseFile();
            }
        }

        private void briefingSaveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            editingFilePath = briefingSaveFileDialog.FileName;
            this.UpdateTitle();
            SaveFile();
        }

        private void briefingOpenFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            editingFilePath = briefingOpenFileDialog.FileName;
            this.UpdateTitle();
            OpenFile();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            if (ConfirmUnsaved())
            {
                briefingOpenFileDialog.ShowDialog();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ConfirmUnsaved())
            {
                briefingOpenFileDialog.ShowDialog();
            }
        }

        private void InteractiveEditorForm_Load(object sender, EventArgs e)
        {
            Project = null;
            Level = null;
            Screen = null;
            NewFile();
            briefingTextBox.ForeColor = textColors[0];
            levelToolStripNumericUpDown.NumericUpDownControl.ValueChanged += levelToolStripNumericUpDown_ValueChanged;
            toolStripMenuItemAutoPreview.Checked = BriefingStudio.Properties.Settings.Default.autoPreview;
        }

        private void levelToolStripNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Unsaved = true;
            UpdateLevelList();
        }

        private void saveasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            briefingSaveFileDialog.ShowDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoSave();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            GoSave();
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            if (ConfirmUnsaved())
            {
                NewFile();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ConfirmUnsaved())
            {
                NewFile();
            }
        }

        private void levelToolStripNumericUpDown_Click(object sender, EventArgs e)
        {
            UpdateLevelList();
        }

        private void levelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSelectedLevel();
        }

        private void screenListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSelectedScreen();
        }

        private void briefingTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!loadingRtf && Screen != null && briefingTextBox.Modified)
            {
                briefingTextBox.Modified = false;
                Unsaved = true;
            }
            textEditDebounceTimer.Stop();
            textEditDebounceTimer.Start();
        }

        private void textEditDebounceTimer_Tick(object sender, EventArgs e)
        {
            textEditDebounceTimer.Stop();
            UpdateScreenList();
            MaybeUpdatePreview();
        }

        private void toolStripButtonAddScreen_Click(object sender, EventArgs e)
        {
            AddNewScreen();
        }

        private void toolStripButtonRemoveScreen_Click(object sender, EventArgs e)
        {
            DeleteThisScreen();
        }

        private void newScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewScreen();
        }

        private void deleteScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteThisScreen();
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            briefingTextBox.Cut();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            briefingTextBox.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            briefingTextBox.Copy();
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            briefingTextBox.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            briefingTextBox.Paste(DataFormats.GetFormat(DataFormats.Text));
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            briefingTextBox.Paste(DataFormats.GetFormat(DataFormats.Text));
        }

        private void briefingTextBox_SelectionChanged(object sender, EventArgs e)
        {
            UpdateSelectionButtonEnabled();
        }

        private void toolStripButtonGreen_Click(object sender, EventArgs e)
        {
            briefingTextBox.SelectionColor = textColors[0];
        }

        private void toolStripButtonGray_Click(object sender, EventArgs e)
        {
            briefingTextBox.SelectionColor = textColors[1];
        }

        private void toolStripButtonBlue_Click(object sender, EventArgs e)
        {
            briefingTextBox.SelectionColor = textColors[2];
        }

        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            submitBriefing(Project.ToBriefing((int)levelToolStripNumericUpDown.NumericUpDownControl.Value));
        }

        private void previewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            playBriefing("$S1\n" + Screen.ToBriefing(), 1, true);
        }

        private void levelComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            UpdateSelectedLevel();
        }

        private void toolStripButtonMoveUp_Click(object sender, EventArgs e)
        {
            int index = screenListBox.SelectedIndex;
            if (index > 0)
            {
                Level.Screens.RemoveAt(index);
                Level.Screens.Insert(index - 1, Screen);
                UpdateScreenList();
                screenListBox.SelectedIndex = index - 1;
            }
        }

        private void toolStripButtonMoveDown_Click(object sender, EventArgs e)
        {
            int index = screenListBox.SelectedIndex;
            int max = screenListBox.Items.Count - 1;
            if (index < max)
            {
                Level.Screens.RemoveAt(index);
                Level.Screens.Insert(index + 1, Screen);
                UpdateScreenList();
                screenListBox.SelectedIndex = index + 1;
            }
        }

        private void importTXBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ConfirmUnsaved())
            {
                importTxbFileDialog.ShowDialog();
            }
        }

        private void exportTXBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exportTxbFileDialog.ShowDialog();
        }

        private void exportTxbFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                using (FileStream fs = File.Open(exportTxbFileDialog.FileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                {
                    byte[] txb = TXBConverter.EncodeTXB(Project.ToBriefing((int)levelToolStripNumericUpDown.NumericUpDownControl.Value));
                    fs.Write(txb, 0, txb.Length);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Could not export TXB.\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static int GetLeadingInt(string input)
        {
            string s = new string(input.Trim().TakeWhile(c => char.IsDigit(c)).ToArray());
            if (s.Length < 1) return -1;
            return Int32.Parse(s);
        }

        private void importTxbFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            string txt = null;
            try
            {
                using (FileStream fs = File.Open(importTxbFileDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (BinaryReader br = new BinaryReader(fs))
                {
                    txt = TXBConverter.DecodeTXB(br.ReadBytes((int)fs.Length));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Could not import TXB.\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            NewFile();
            Unsaved = true;
            string[] sections = txt.Split(new string[] { "$S" }, StringSplitOptions.None);
            int levelCount = 1;
            foreach (string section in sections)
            {
                // get section number
                int snum = GetLeadingInt(section);
                if (snum < 1)
                    continue;

                int newline = section.IndexOf("\n");
                if (newline < 0)
                    continue;

                string sectionText = section.Substring(newline + 1);
                string[] pages = ("\n" + sectionText).Split(new string[] { "$P" }, StringSplitOptions.None);
                BriefingProject.BriefingLevel level = new BriefingProject.BriefingLevel();
                bool created = false;
                bool cursorFlash = false;

                Project.Intro = new BriefingProject.BriefingLevel();
                foreach (string origPage in pages)
                {
                    newline = origPage.IndexOf("\n");
                    if (newline < 0) continue;
                    string page = origPage.Substring(newline + 1);
                    if (!created && page.Trim().Length > 0)
                    {
                        while (Project.Levels.Count <= snum)
                        {
                            Project.Levels.Add(new BriefingProject.BriefingLevel());
                        }
                        Project.Levels[snum - 1] = level;
                        levelCount = Math.Max(levelCount, snum);
                        created = true;
                    }

                    BriefingProject.BriefingScreen screen = new BriefingProject.BriefingScreen();
                    // try finding commands from beginning of text
                    bool definedScreen = false, definedBg = false;
                    while (page.StartsWith("$"))
                    {
                        if (page.StartsWith("$D") && !definedScreen)
                        {
                            definedScreen = true;

                            newline = page.IndexOf("\n");
                            string[] parameters = page.Substring(2, newline - 2).Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if (parameters.Length < 8)  // SCREEN BG LNUM MNUM ULX ULY W H
                                break;  // not well formed enough for us

                            try
                            {
                                screen.TextRegion = new Rectangle((int)UInt32.Parse(parameters[4]), (int)UInt32.Parse(parameters[5]), (int)UInt32.Parse(parameters[6]), (int)UInt32.Parse(parameters[7]));
                            }
                            catch (FormatException)
                            {
                                break;
                            }

                            if (newline < 0)
                                page = "";
                            else
                                page = page.Substring(newline + 1);
                        }
                        else if (page.StartsWith("$Z") && !definedBg)
                        {
                            definedBg = true;
                            newline = page.IndexOf("\n");
                            screen.Background = page.Substring(2, newline - 2).Trim();
                            if (newline < 0)
                                page = "";
                            else
                                page = page.Substring(newline + 1);
                        }
                        else if (page.StartsWith("$F"))
                        {
                            cursorFlash = !cursorFlash;
                            newline = page.IndexOf("\n");
                            if (newline < 0)
                                page = "";
                            else
                                page = page.Substring(newline + 1);
                        }
                        else
                        {
                            break;
                        }
                    }

                    screen.Text = page;

                    level.Screens.Add(screen);
                }
            }

            if (levelCount > 1 && DialogResult.Yes == MessageBox.Show(this, $"I detected {levelCount} levels. Should I consider the briefing for the last one an outro?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                BriefingProject.BriefingLevel finalLevel = Project.Levels.Last();
                levelCount -= 1;
                Project.Levels.RemoveAt(Project.Levels.Count - 1);
                Project.Outro = finalLevel;
            }
            else
            {
                Project.Outro = new BriefingProject.BriefingLevel();
            }

            Project.LevelCount = levelCount;
            levelToolStripNumericUpDown.NumericUpDownControl.Value = levelCount;
            UpdateLevelList();
            UpdateScreenList();
            levelComboBox.SelectedIndex = 0;
            UpdateSelectedLevel();
        }

        private void InteractiveEditorForm_Shown(object sender, EventArgs e)
        {
            if (descentGame == 1)
            {
                this.Enabled = false;
                MessageBox.Show(this, "Descent I is not supported for the Interactive Briefing Editor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void briefingTextBox_Pasted(object sender, EventArgs e)
        {
            ForceReformat();
        }

        private void toolStripMenuItemAutoPreview_CheckedChanged(object sender, EventArgs e)
        {
            BriefingStudio.Properties.Settings.Default.autoPreview = toolStripMenuItemAutoPreview.Checked;
            BriefingStudio.Properties.Settings.Default.Save();
        }

        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            MaybeUpdatePreview();
        }
    }
}
