using BriefingStudio.Logic;
using BriefingStudio.Logic.Formats;
using LibDescent.Data;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace BriefingStudio.UI
{
    public partial class MainForm : Form
    {
        private HOGFile baseHog;
        private HOGFile workingHog;
        private string baseHogFile;
        private string workingHogFile;
        private LowResScreenForm lowres = new LowResScreenForm();
        private HighResScreenForm highres = new HighResScreenForm();
        private TXBEditorForm editor = new TXBEditorForm();
        private SettingsForm settingsForm = new SettingsForm();
        private BannerCreatorForm bannerCreatorForm = new BannerCreatorForm();
        private InteractiveEditorForm betterEditor = null;
        private Briefing bl = null;
        private Briefing bh = null;
        private int descentGame;
        private bool playingBriefingRaw;
        private EveryFrameEventHandler everyFrameHandler = new EveryFrameEventHandler();
        private string loadedBriefing = null;

        private bool PlayingBriefing
        {
            get
            {
                return playingBriefingRaw;
            }
            set
            {
                playingBriefingLabel.Invoke(new Action(() => playingBriefingLabel.Text = value ? "Playing briefing" : "Idle"));
                playingBriefingRaw = value;
            }
        }

        public MainForm()
        {
            InitializeComponent();
            descentGame = 0;
            playingBriefingRaw = false;
            everyFrameHandler.Bind();
            everyFrameHandler.IdleFrame += OnIdleFrame;
            lowres.KeyDown += OnScreenKeyEvent;
            highres.KeyDown += OnScreenKeyEvent;
        }

        private void OnScreenKeyEvent(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                bl.GotKey();
                bh.GotKey();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                StopBriefing();
                bl.EndOfBriefing();
                bh.EndOfBriefing();
                UpdateFrames();
            }
        }

        private void OnIdleFrame(object sender, EventArgs e)
        {
            UpdateFrames();
        }

        public void InitScreens(FNTRenderer fl, FNTRenderer fh)
        {
            bl = new Briefing(false, descentGame, fl, FindFile);
            bh = new Briefing(true, descentGame, fh, FindFile);
            bl.BriefingEnded += OnBriefingEnded;
            bh.BriefingEnded += OnBriefingEnded;
        }

        private void OnBriefingEnded(object sender, EventArgs e)
        {
            StopBriefing();
        }

        public void SubmitBriefing(string contents)
        {
            if (editor.Unsaved && !editor.fromCompile)
            {
                if (DialogResult.Yes != MessageBox.Show(this, "The TXB editor already has an unsaved TXB. Overwrite it?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    return;
                }
            }
            editor.ImportText(contents);
        }

        public void PlayBriefing(string text, int n, bool instant)
        {
            if (bl == null)
            {
                MessageBox.Show(this, "Open a base HOG first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PlayingBriefing = true;
            if (text != null)
            {
                bl.Load(text);
                bh.Load(text);
            }
            bl.Play(n, instant);
            bh.Play(n, instant);
        }

        private void PlayBriefing(int n)
        {
            if (baseHog == null)
            {
                MessageBox.Show(null, "Select a valid base HOG first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string briefing = null;
            string newLoadedBriefing = briefingNameTextBox.Text;
            if (newLoadedBriefing != loadedBriefing)
            {
                byte[] txb = FindFile(newLoadedBriefing);
                if (txb == null)
                {
                    MessageBox.Show(null, newLoadedBriefing + " not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                briefing = TXBConverter.DecodeTXB(txb);
                bl.Load(briefing);
                bh.Load(briefing);
                loadedBriefing = newLoadedBriefing;
            }
            PlayBriefing(briefing, n, false);
        }

        private void PlayBriefing()
        {
            PlayBriefing((int)levelNumericUpDown.Value);
        }

        private void StopBriefing()
        {
            PlayingBriefing = false;
            bl.Stop();
            bh.Stop();
        }

        public void UpdateFrames()
        {
            if (lowres.Visible && bl != null)
            {
                if (bl.CanRender())
                    lowres.SetImage(bl.Render());
            }
            if (highres.Visible && bh != null)
            {
                if (bh.CanRender())
                    highres.SetImage(bh.Render());
            }
        }

        private void SaveBriefing(string filename, string contents)
        {
            string text = contents.Replace("\r", "");
            if (Properties.Settings.Default.addEndSection)
            {
                string append = "";
                if (!text.Contains("$S999"))
                {
                    append = "$S999\n";
                    if (!text.EndsWith("\n"))
                    {
                        append = "\n" + text;
                    }
                }
                text += append;
            }
            editor.ModifyText(text.Replace("\n", "\r\n"));
            byte[] txb = TXBConverter.EncodeTXB(text);
            workingHog.ReplaceLump(new HOGLump(filename, txb));
            workingHog.Write(workingHogFile);
        }

        private void DescentHogOpenButton_click(object sender, EventArgs e)
        {
            descentHogOpenFileDialog.ShowDialog();
        }

        private void WorkingHogOpenButton_click(object sender, EventArgs e)
        {
            workingHogOpenFileDialog.ShowDialog();
        }

        private void LoadBaseHog(string filename)
        {
            editor.SetDescentGame(0);
            betterEditor?.SetDescentGame(0);

            baseHogLabel.Text = "No Descent HOG file specified";
            baseHog = null;
            try
            {
                baseHog = new HOGFile(baseHogFile = filename);
            }
            catch (ArgumentException aex)
            {
                MessageBox.Show(this, aex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            baseHogLabel.Text = "No Descent HOG file specified";
            if (!baseHog.ContainsFile("descent.txb") || !baseHog.ContainsFile("end01.pcx"))
            {
                MessageBox.Show(this, "This is not a valid registered Descent 1 or Descent 2 main HOG", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baseHog = null;
                return;
            }

            if (baseHog.ContainsFile("neptun01.pcx"))
            {
                descentGame = 1;
                baseHogLabel.Text = "Main HOG: Descent 1";
            }
            else if (baseHog.ContainsFile("d2levf-s.rl2"))
            {
                descentGame = 2;
                baseHogLabel.Text = "Main HOG: Descent 2";
            }
            else
            {
                MessageBox.Show(this, "This is not a valid registered Descent 1 or Descent 2 main HOG", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baseHog = null;
                return;
            }

            byte[] font3_1 = baseHog.GetFileData("FONT3-1.FNT");
            if (font3_1 == null)
            {
                MessageBox.Show(this, "FONT3-1.FNT not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baseHog = null;
                return;
            }

            FNTRenderer gamefontL, gamefontH = null;

            gamefontL = new FNTRenderer(LoadFont(new MemoryStream(font3_1)));

            if (descentGame == 1)
            {
                gamefontH = new FNTRendererUpScale(gamefontL.font);
            }
            else if (descentGame == 2)
            {
                font3_1 = baseHog.GetFileData("FONT3-1H.FNT");
                if (font3_1 == null)
                {
                    MessageBox.Show(this, "FONT3-1H.FNT not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baseHog = null;
                    return;
                }
                gamefontH = new FNTRenderer(LoadFont(new MemoryStream(font3_1)));
            }

            InitScreens(gamefontL, gamefontH);
            lowres.Refresh();
            highres.Refresh();
            editor.SetDescentGame(descentGame);
            betterEditor?.SetDescentGame(descentGame);
            Properties.Settings.Default.baseHogPath = filename;
            Properties.Settings.Default.Save();
        }

        internal static Font LoadFont(Stream stream)
        {
            Font font = new Font();
            font.LoadFont(stream);
            return font;
        }

        private void LoadWorkingHog(string filename)
        {
            editor.SetCanSave(false);
            workingHogLabel.Text = "No Working HOG opened";
            workingHog = null;
            loadedBriefing = null;
            try
            {
                workingHog = new HOGFile(workingHogFile = filename);
                workingHogLabel.Text = "Working HOG: " + Path.GetFileName(filename);
                if (briefingNameTextBox.Text.Length == 0)
                {
                    briefingNameTextBox.Text = Path.GetFileNameWithoutExtension(filename) + ".txb";
                }
                editor.SetCanSave(true);
            }
            catch (ArgumentException aex)
            {
                MessageBox.Show(this, aex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                workingHog = null;
                return;
            }
        }

        private void DescentHogOpenFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            LoadBaseHog(descentHogOpenFileDialog.FileName);
        }

        private void WorkingHogOpenFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            LoadWorkingHog(workingHogOpenFileDialog.FileName);
        }

        private byte[] FindFile(string fileName)
        {
            if (baseHog == null)
            {
                return null;
            }

            byte[] file = null;
            if (workingHog != null)
            {
                file = workingHog.GetFileData(fileName);
                if (file != null)
                {
                    return file;
                }
            }
            return baseHog.GetFileData(fileName);
        }

        private void lowResScreenButton_Click(object sender, EventArgs e)
        {
            lowres.Show();
            lowres.BringToFront();
        }

        private void highResScreenButton_Click(object sender, EventArgs e)
        {
            highres.Show();
            highres.BringToFront();
        }
        /*
        private void briefingTimer_Tick(object sender, EventArgs e)
        {
            if (!playingBriefing)
            {
                briefingTimer.Stop();
                return;
            }

            bl.Tick();
            lowres.SetImage(bl.Render());
            lowres.Refresh();

            bh.Tick();
            highres.SetImage(bh.Render());
            highres.Refresh();
        }
        */
        private void MainForm_Load(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();
            try
            {
                if (Properties.Settings.Default.baseHogPath.Length > 0)
                {
                    LoadBaseHog(Properties.Settings.Default.baseHogPath);
                }
            }
            catch (Exception)
            {
                Properties.Settings.Default.baseHogPath = "";
                Properties.Settings.Default.Save();
            }
        }

        private void stopBriefingButton_Click(object sender, EventArgs e)
        {
            StopBriefing();
        }

        private void playBriefingButton_Click(object sender, EventArgs e)
        {
            PlayBriefing();
        }

        private void frameTimer_Tick(object sender, EventArgs e)
        {
            UpdateFrames();
        }

        private void closeWorkingHogButton_Click(object sender, EventArgs e)
        {
            workingHog = null;
            workingHogLabel.Text = "No Working HOG opened";
            editor.SetCanSave(false);
        }

        private void txbEditorButton_Click(object sender, EventArgs e)
        {
            editor.Show();
            editor.BringToFront();
            editor.SetDelegates(FindFile, PlayBriefing, StopBriefing, SaveBriefing);
            if (workingHog != null && briefingNameTextBox.Text.Length > 0)
            {
                editor.LoadFile(briefingNameTextBox.Text);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            settingsForm.Show();
            settingsForm.BringToFront();
        }

        private void bannerGeneratorButton_Click(object sender, EventArgs e)
        {
            bannerCreatorForm.SetFindFile(FindFile);
            bannerCreatorForm.Show();
            bannerCreatorForm.BringToFront();
        }

        private void newEditorButton_Click(object sender, EventArgs e)
        {
            if ((Application.OpenForms["InteractiveEditorForm"] as InteractiveEditorForm) != null)
            {
                betterEditor?.BringToFront();
            }
            else
            {
                betterEditor?.Dispose();
                betterEditor = new InteractiveEditorForm();
                betterEditor.SetDelegates(FindFile, PlayBriefing, StopBriefing, SubmitBriefing);
                betterEditor.SetDescentGame(descentGame);
                betterEditor.Show();
            }
        }
    }
}
