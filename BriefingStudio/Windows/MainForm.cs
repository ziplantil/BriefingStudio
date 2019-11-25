using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace BriefingStudio
{
    public partial class MainForm : Form
    {
        private HOGFile baseHog;
        private HOGFile workingHog;
        private LowResScreenForm lowres = new LowResScreenForm();
        private HighResScreenForm highres = new HighResScreenForm();
        private TXBEditorForm editor = new TXBEditorForm();
        private SettingsForm settingsForm = new SettingsForm();
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

        public void InitScreens(FNTFont fl, FNTFont fh)
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

        public void PlayBriefing(string text, int n)
        {
            PlayingBriefing = true;
            if (text != null)
            {
                bl.Load(text);
                bh.Load(text);
            }
            bl.Play(n);
            bh.Play(n);
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
            PlayBriefing(briefing, n);
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
            byte[] txb = TXBConverter.EncodeTXB(text);
            workingHog.PutFile(filename, txb);
        }

        private void DescentHogOpenButton_click(object sender, EventArgs e)
        {
            descentHogOpenFileDialog.ShowDialog();
        }

        private void WorkingHogOpenButton_click(object sender, EventArgs e)
        {
            workingHogOpenFileDialog.ShowDialog();
        }

        private void DescentHogOpenFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            baseHog?.Dispose();
            baseHogLabel.Text = "No Descent HOG file specified";
            baseHog = null;
            try
            {
                baseHog = new HOGFile(descentHogOpenFileDialog.FileName);
            }
            catch (ArgumentException aex)
            {
                MessageBox.Show(this, aex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            baseHogLabel.Text = "No Descent HOG file specified";
            if (!baseHog.HasFile("descent.txb") || !baseHog.HasFile("end01.pcx"))
            {
                MessageBox.Show(this, "This is not a valid registered Descent 1 or Descent 2 main HOG", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baseHog = null;
                return;
            }

            if (baseHog.HasFile("neptun01.pcx"))
            {
                descentGame = 1;
                baseHogLabel.Text = "Main HOG: Descent 1";
            }
            else if (baseHog.HasFile("d2levf-s.rl2"))
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

            byte[] font3_1 = baseHog.GetFile("FONT3-1.FNT");
            if (font3_1 == null)
            {
                MessageBox.Show(this, "FONT3-1.FNT not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baseHog = null;
                return;
            }

            FNTFont gamefontL, gamefontH = null;

            gamefontL = new FNTFont(new MemoryStream(font3_1));

            if (descentGame == 1)
            {
                gamefontH = new FNTFontUpScale(gamefontL);
            }
            else if (descentGame == 2)
            {
                font3_1 = baseHog.GetFile("FONT3-1H.FNT");
                if (font3_1 == null)
                {
                    MessageBox.Show(this, "FONT3-1H.FNT not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baseHog = null;
                    return;
                }
                gamefontH = new FNTFont(new MemoryStream(font3_1));
            }

            InitScreens(gamefontL, gamefontH);
            lowres.Refresh();
            highres.Refresh();
        }

        private void WorkingHogOpenFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            workingHog?.Dispose();
            editor.SetCanSave(false);
            workingHogLabel.Text = "No Working HOG opened";
            workingHog = null;
            loadedBriefing = null;
            try
            {
                workingHog = new HOGFile(workingHogOpenFileDialog.FileName);
                workingHogLabel.Text = "Working HOG: " + Path.GetFileName(workingHogOpenFileDialog.FileName);
                if (briefingNameTextBox.Text.Length == 0)
                {
                    briefingNameTextBox.Text = Path.GetFileNameWithoutExtension(workingHogOpenFileDialog.FileName) + ".txb";
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

        private byte[] FindFile(string fileName)
        {
            byte[] file = null;
            if (workingHog != null)
            {
                file = workingHog.GetFile(fileName);
                if (file != null)
                {
                    return file;
                }
            }
            return baseHog.GetFile(fileName);
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
            workingHog.Dispose();
            workingHog = null;
            workingHogLabel.Text = "No Working HOG opened";
            editor.SetCanSave(false);
        }

        private void txbEditorButton_Click(object sender, EventArgs e)
        {
            editor.Show();
            editor.SetDelegates(FindFile, PlayBriefing, StopBriefing, SaveBriefing);
            if (workingHog != null && briefingNameTextBox.Text.Length > 0)
            {
                editor.LoadFile(briefingNameTextBox.Text);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            settingsForm.Show();
        }
    }
}
