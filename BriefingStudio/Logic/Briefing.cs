﻿using BriefingStudio.Logic.Formats;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace BriefingStudio.Logic
{
    class Briefing
    {
        private PCXDecoder pcxdec = new PCXDecoder();
        private bool highRes;
        private Image backgroundCache;
        private readonly Bitmap screen;
        private readonly Bitmap flipped;
        private Graphics graphics;
        private Graphics flipper;
        private Rectangle frame;
        private readonly Object graphicsLock = new Object();
        private int charDelay;
        private bool useDelay;
        private FNTRenderer font;
        private Color[] palette;
        private int briefingColor;
        private Color clearColor;
        private Color[] briefingBGcols;
        private Color[] briefingFGcols;
        private int descentGame;
        private string backgroundName;
        private string fullText;
        private string text;
        private volatile bool playing;
        private volatile bool keyPress;
        private volatile bool rendering;
        private volatile bool interrupted;
        private List<BriefingScreen> screens;
        private int sequence;
        private int screenNum;
        private int tx;
        private int ty;
        private int tabStop = 0;
        private int textIndex = 0;
        private int lineAdjust = 0;
        private int dumbAdjust = 0;
        private int brightness = 255; // [0, 255]
        private string robotMoviePlaying;
        private int robotSpinning;
        private Bitmap bitmapDisplay;
        private string bitmapDisplayText;
        public event EventHandler BriefingEnded;
        private Brush placeholderBrush = new SolidBrush(Color.FromArgb(0, 0, 0));
        private Pen placeholderBorderPen = new Pen(Color.FromArgb(255, 0, 0));
        private Pen helperBorderPen = new Pen(Color.FromArgb(0, 255, 255));
        private Brush placeholderFontBrush = new SolidBrush(Color.FromArgb(255, 0, 0));
        private Font specialFont = new Font("Courier New", 10);
        private volatile Thread playThread;
        private bool hurryUp;
        private volatile Object playLock = new object();

        public delegate byte[] FindFile(string filename);
        private FindFile findFile;

        public Briefing(bool highRes, int descentGame, FNTRenderer font, FindFile findFile)
        {
            this.findFile = findFile;
            this.descentGame = descentGame;
            this.highRes = highRes;
            backgroundName = null;
            backgroundCache = null;
            fullText = text = null;
            if (highRes)
            {
                screen = new Bitmap(640, 480, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            }
            else
            {
                screen = new Bitmap(320, 200, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            }
            flipped = new Bitmap(screen);
            graphics = Graphics.FromImage(screen);
            flipper = Graphics.FromImage(flipped);
            frame = new Rectangle(new Point(0, 0), screen.Size);
            briefingColor = 0;
            this.font = font;
            playing = false;
            screens = new List<BriefingScreen>();
            screens.AddRange(GetBriefingScreens(descentGame, highRes));
            charDelay = descentGame == 2 ? 20 : 28;
        }

        private void UpdateBriefingColors()
        {
            if (palette == null)
            {
                playing = false;
                return;
            }

            clearColor = ClosestColor(palette, Color.Black);
            int i = 0;
            bool dxx = Properties.Settings.Default.allowD2XColors;
            if (descentGame == 1)
            {
                briefingBGcols = new System.Drawing.Color[dxx ? 7 : 2];
                briefingFGcols = new System.Drawing.Color[dxx ? 7 : 2];
                briefingBGcols[i] = ClosestColor(palette, Color.FromArgb(255, 0, 76, 0));
                briefingFGcols[i++] = ClosestColor(palette, Color.FromArgb(255, 0, 216, 0));
                briefingBGcols[i] = ClosestColor(palette, Color.FromArgb(255, 56, 56, 56));
                briefingFGcols[i++] = ClosestColor(palette, Color.FromArgb(255, 168, 152, 128));

                if (dxx)
                {
                    briefingBGcols[i] = ClosestColor(palette, Color.FromArgb(255, 84, 0, 0));
                    briefingFGcols[i++] = ClosestColor(palette, Color.FromArgb(255, 252, 0, 0));
                    briefingBGcols[i] = ClosestColor(palette, Color.FromArgb(255, 0, 0, 72));
                    briefingFGcols[i++] = ClosestColor(palette, Color.FromArgb(255, 0, 0, 216));
                    briefingBGcols[i] = ClosestColor(palette, Color.FromArgb(255, 19, 19, 19));
                    briefingFGcols[i++] = ClosestColor(palette, Color.FromArgb(255, 56, 56, 56));
                    briefingBGcols[i] = ClosestColor(palette, Color.FromArgb(255, 72, 72, 0));
                    briefingFGcols[i++] = ClosestColor(palette, Color.FromArgb(255, 216, 216, 0));
                    briefingBGcols[i] = ClosestColor(palette, Color.FromArgb(255, 0, 72, 72));
                    briefingFGcols[i++] = ClosestColor(palette, Color.FromArgb(255, 0, 216, 216));
                }
            }
            else if (descentGame == 2)
            {
                briefingBGcols = new System.Drawing.Color[dxx ? 7 : 3];
                briefingFGcols = new System.Drawing.Color[dxx ? 7 : 3];
                briefingBGcols[i] = ClosestColor(palette, Color.FromArgb(255, 0, 24, 0));
                briefingFGcols[i++] = ClosestColor(palette, Color.FromArgb(255, 0, 160, 0));
                briefingBGcols[i] = ClosestColor(palette, Color.FromArgb(255, 20, 20, 20));
                briefingFGcols[i++] = ClosestColor(palette, Color.FromArgb(255, 160, 132, 140));
                briefingBGcols[i] = ClosestColor(palette, Color.FromArgb(255, 4, 16, 28));
                briefingFGcols[i++] = ClosestColor(palette, Color.FromArgb(255, 32, 124, 216));

                if (dxx)
                {
                    briefingBGcols[i] = ClosestColor(palette, Color.FromArgb(255, 0, 0, 72));
                    briefingFGcols[i++] = ClosestColor(palette, Color.FromArgb(255, 0, 0, 216));
                    briefingBGcols[i] = ClosestColor(palette, Color.FromArgb(255, 19, 19, 19));
                    briefingFGcols[i++] = ClosestColor(palette, Color.FromArgb(255, 56, 56, 56));
                    briefingBGcols[i] = ClosestColor(palette, Color.FromArgb(255, 72, 72, 0));
                    briefingFGcols[i++] = ClosestColor(palette, Color.FromArgb(255, 216, 216, 0));
                    briefingBGcols[i] = ClosestColor(palette, Color.FromArgb(255, 0, 72, 72));
                    briefingFGcols[i++] = ClosestColor(palette, Color.FromArgb(255, 0, 216, 216));
                }
            }
        }

        private Color ClosestColor(Color[] palette, Color color)
        {
            return Utils.LDColorToGDIColor(LibDescent.Data.PCXImage.ClosestColor(Utils.GDIPaletteToLDPalette(palette), Utils.GDIColorToLDColor(color)));
        }

        private string RemoveComments(string text)
        {
            // remove lines starting with ;
            return Regex.Replace(text, "(?m)^;.*\n?", "");
        }

        public void Load(string text)
        {
            lock (playLock)
            {
                if (playing && playThread != null)
                {
                    interrupted = true;
                    Stop();
                    GotKey();
                    playThread?.Interrupt();
                    playThread?.Join(1000);
                    playThread?.Abort();
                }
            }
            backgroundCache = null;
            briefingColor = 0;
            fullText = RemoveComments(text).Replace("\r", "");
        }

        private int ReadNumber(string source, int index, out int result)
        {
            // read number from text at given index
            string cut = "";
            while (source[index] == ' ')
            {
                ++index;
            }
            while (cut.Length < 8)
            {
                char c = source[index];
                if (!Char.IsDigit(c))
                {
                    break;
                }
                else if (Char.IsDigit(c))
                {
                    cut += c;
                }
                ++index;
            }
            if (cut.Length > 0)
            {
                result = Int32.Parse(cut);
            }
            else
            {
                result = -1;
            }
            return index;
        }

        private int ReadNumberSpace(string source, int index, out int result)
        {
            // read number from text at given index
            // allow space as delimiter
            string cut = "";
            while (source[index] == ' ')
            {
                ++index;
            }
            while (cut.Length < 8)
            {
                char c = source[index];
                if (!Char.IsDigit(c))
                {
                    break;
                }
                else if (Char.IsDigit(c))
                {
                    cut += c;
                }
                ++index;
            }
            if (cut.Length > 0)
            {
                result = Int32.Parse(cut);
            }
            else
            {
                result = -1;
            }
            ++index;
            return index;
        }

        private int ReadStringSpace(string source, int index, out string result)
        {
            // read word from text at given index
            string cut = "";
            while (source[index] == ' ')
            {
                ++index;
            }
            while (cut.Length < 50)
            {
                char c = source[index];
                if (c == ' ' || c == '\n')
                {
                    break;
                }
                cut += c;
                ++index;
            }
            ++index;
            result = cut;
            return index;
        }

        private int ReadRestOfLine(string source, int index, out string result)
        {
            // read word from text at given index
            string cut = "";
            while (source[index] == ' ')
            {
                ++index;
            }
            while (cut.Length < 50)
            {
                char c = source[index];
                if (c == '\n')
                {
                    break;
                }
                cut += c;
                ++index;
            }
            ++index;
            result = cut;
            return index;
        }

        private int DefineBriefingBox()
        {
            int n;
            textIndex = ReadNumberSpace(text, textIndex, out n);
            if (n >= 60)
            {
                ShowText("Invalid screen #" + n + " > 59");
                playing = false;
                return 0;
            }

            // padding
            while (screens.Count < n)
            {
                screens.Add(null);
            }

            string name;
            int levelNum, messageNum, ulx, uly, w, h;
            textIndex = ReadStringSpace(text, textIndex, out name);
            textIndex = ReadNumberSpace(text, textIndex, out levelNum);
            textIndex = ReadNumberSpace(text, textIndex, out messageNum);
            textIndex = ReadNumberSpace(text, textIndex, out ulx);
            textIndex = ReadNumberSpace(text, textIndex, out uly);
            textIndex = ReadNumberSpace(text, textIndex, out w);
            textIndex = ReadNumberSpace(text, textIndex, out h);
            if (highRes)
            {
                ulx *= 2;
                uly = (int)(uly * 2.4);
                w *= 2;
                h = (int)(h * 2.4);
            }

            BriefingScreen bs = new BriefingScreen(name, (byte)levelNum, (byte)messageNum, ulx, uly, w, h);

            if (screens.Count > n)
            {
                screens[n] = bs;
            }
            else
            {
                screens.Insert(n, bs);
            }

            return n;
        }

        public static int FindSequenceIndex(string text, int sequence)
        {
            // $Snn
            int index = 0;
            int newIndex;
            while ((newIndex = text.IndexOf("$S", index)) >= 0)
            {
                int endIndex = text.IndexOf('\n', newIndex);
                if (endIndex < 0) endIndex = text.Length;
                int numIndex = newIndex + 2;
                if (numIndex == endIndex)
                {
                    break;
                }
                string number = text.Substring(numIndex, endIndex - numIndex).Trim();
                if (number.Length > 0)
                {
                    if (UInt32.TryParse(number, out uint n))
                    {
                        if (n == sequence)
                        {
                            return newIndex;
                        }
                    }
                }
                index = newIndex + 2;
            }
            return text.Length;
        }

        public int FindSequenceIndex(int sequence)
        {
            return FindSequenceIndex(fullText, sequence);
        }

        private string GetSequenceText(int sequence)
        {
            int startIndex = FindSequenceIndex(sequence);
            if (startIndex >= fullText.Length) return "";
            int endIndex = fullText.IndexOf("$S", startIndex + 2);
            if (endIndex < 0) endIndex = fullText.Length;
            return fullText.Substring(startIndex, endIndex - startIndex);
        }

        private string GetSequencesText(int start, int end)
        {
            int startIndex = FindSequenceIndex(start);
            if (startIndex >= fullText.Length) return "";
            int endIndex = FindSequenceIndex(end);
            return fullText.Substring(startIndex, endIndex - startIndex);
        }

        private void MoveText(int x, int y)
        {
            font.Reset();
            tx = x;
            ty = y;
        }

        public void Play(int sequence, bool instant)
        {
            lock (playLock)
            {
                interrupted = false;
                if (fullText == null)
                {
                    lock (graphicsLock)
                        graphics.Clear(Color.Black);
                    return;
                }

                if (playing && playThread != null)
                {
                    interrupted = true;
                    Stop();
                    GotKey();
                    playThread?.Interrupt();
                    playThread?.Join(1000);
                    playThread?.Abort();
                }
                interrupted = false;

                if (descentGame == 1)
                {
                    // get text for before level #N
                    int firstSequence = -1;
                    int firstNonSequence = screens.Count;

                    for (int i = 0; i < screens.Count; ++i)
                    {
                        if (firstSequence < 0 && screens[i].level == sequence)
                        {
                            firstSequence = i;
                        }
                        else if (firstSequence >= 0 && screens[i].level != sequence)
                        {
                            firstNonSequence = i;
                            break;
                        }
                    }

                    if (firstSequence > 40)
                    {
                        firstSequence -= 40;
                        if (firstNonSequence > 40) firstNonSequence -= 40;
                    }

                    if (firstSequence >= 0)
                    {
                        text = GetSequencesText(firstSequence + 1, firstNonSequence + 1);
                    }
                    else
                    {
                        text = "";
                    }
                    screenNum = firstSequence;
                    if (screenNum < 0 || screenNum >= screens.Count)
                    {
                        return;
                    }
                }
                else
                {
                    // get text for sequence number #N
                    text = GetSequenceText(sequence);
                    screenNum = 0;
                }
                textIndex = 0;
                playing = true;
                hurryUp = instant;
                this.sequence = sequence;
                backgroundName = screens[screenNum].bsName;

                briefingColor = 0;
                tabStop = 0;
                brightness = 255;

                rendering = true;

                (playThread = new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    Thread.CurrentThread.Name = "BriefingPlayThread" + (highRes ? "HighRes" : "LowRes");
                    NewPage(false);
                    try
                    {
                        MainLoop();
                    }
                    catch (Exception)
                    {
                        // ignore any and all errors here because they might be caused by another thread stopping
                    }
                    playThread = null;
                })).Start();
            }
        }

        public void Stop()
        {
            lock (playLock)
            {
                playing = false;
                text = null;
            }
        }

        private bool DrawBackground()
        {
            string pcx = backgroundName;
            if (highRes && descentGame == 2)
            {
                int index = pcx.LastIndexOf(".pcx", StringComparison.InvariantCultureIgnoreCase);
                if (index >= 0)
                {
                    // .B
                    pcx = pcx.Substring(0, index) + "B.pcx";
                }
            }
            byte[] pcxTexture = findFile(pcx);
            if (pcxTexture == null)
            {
                ShowText(pcx + " not found");
                playing = false;
                return false;
            }
            backgroundCache = pcxdec.LoadPCX(pcxTexture, out palette);
            UpdateBriefingColors();
            lock (graphicsLock)
            {
                graphics.Clear(clearColor);
                if (descentGame == 1 && highRes)
                {
                    graphics.DrawImage(backgroundCache, frame, 0, 0, 320, 200, GraphicsUnit.Pixel);
                }
                else
                {
                    graphics.DrawImage(backgroundCache, 0, 0);
                }
            }
            return true;
        }

        private int NextChar()
        {
            if (textIndex >= text.Length)
            {
                return -1;
            }
            return text[textIndex++];
        }

        private void WaitKey(bool cursorFlash, int timeout)
        {
            int dummy = tx + 1;
            Stopwatch sw = null;
            if (timeout >= 0)
            {
                sw = new Stopwatch();
                sw.Start();
            }

            while (playing && !keyPress && !interrupted)
            {
                // try cursor flash
                if (briefingFGcols != null && cursorFlash)
                {
                    Color color;
                    if (hurryUp || (DateTimeOffset.Now.ToUnixTimeMilliseconds() % 500 >= 250))
                    {
                        color = briefingFGcols[briefingColor];
                    }
                    else
                    {
                        color = clearColor;
                    }
                    lock (graphicsLock)
                        font.DrawCharacter(screen, '_', color, color, ref dummy, ty, false);
                    dummy = tx + 1;
                }

                try
                {
                    Thread.Sleep(hurryUp ? 1000 : charDelay / 2);
                }
                catch (ThreadInterruptedException)
                {
                    interrupted = true;
                    return;
                }

                if (sw != null && sw.ElapsedMilliseconds >= (timeout * 1000))
                {
                    break;
                }
            }

            sw?.Stop();
            keyPress = false;
        }

        private void WaitKey(bool cursorFlash)
        {
            WaitKey(cursorFlash, -1);
        }

        public void GotKey()
        {
            keyPress = true;
        }

        private void NewPage(bool background)
        {
            if (interrupted) return;
            if (background)
            {
                if (!DrawBackground())
                {
                    return;
                }
                UpdateBriefingColors();
            }
            if (descentGame == 1)
            {
                backgroundName = screens[screenNum].bsName;
                DrawBackground();
                UpdateBriefingColors();
            }
            MoveText(screens[screenNum].ulx, screens[screenNum].uly);
            useDelay = !hurryUp;
            keyPress = false;
            robotMoviePlaying = null;
            robotSpinning = -1;
            bitmapDisplay = null;
            bitmapDisplayText = null;
            font.Reset();
        }

        public void ShowText(string text)
        {
            if (interrupted)
                return;

            lock (graphicsLock)
            {
                NewPage(false);

                graphics.Clear(Color.Black);
                MoveText(0, 0);

                foreach (char c in text)
                {
                    font.DrawCharacter(screen, c, Color.White, Color.Black, ref tx, ty, false);
                }
                font.Reset();
                brightness = 255;
            }
        }

        public void EndOfBriefing()
        {
            if (interrupted)
                return;

            ShowText("END OF BRIEFING.");
            BriefingEnded?.Invoke(this, new EventArgs());
        }

        private string GetCurrentLine()
        {
            int previousLf = text.LastIndexOf('\n', textIndex);
            if (previousLf < 0) previousLf = 0;
            int nextLf = text.IndexOf('\n', textIndex);
            if (nextLf < 0) nextLf = text.Length;
            string anchor = new string(' ', textIndex - previousLf - 1) + "^ (" + textIndex + ")";
            return text.Substring(previousLf + 1, nextLf - previousLf) + "\n" + anchor;
        }

        private void SkipRestOfLine()
        {
            if (text == null) return;
            int newIndex = text.IndexOf('\n', textIndex);
            textIndex = newIndex >= textIndex ? newIndex + 1 : text.Length;
        }

        private int ReadMessageNumber()
        {
            int n;
            textIndex = ReadNumber(text, textIndex, out n);
            return n;
        }

        private void FadeOut()
        {
            if (hurryUp || !Properties.Settings.Default.fadeTransitions) return;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (brightness > 0)
            {
                Thread.Sleep(15);
                brightness -= (int)(sw.ElapsedMilliseconds / 2);
                sw.Restart();
            }
            sw.Stop();
            brightness = 0;
        }

        private void FadeIn()
        {
            if (hurryUp || !Properties.Settings.Default.fadeTransitions) return;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (brightness < 255)
            {
                Thread.Sleep(15);
                brightness += (int)(sw.ElapsedMilliseconds / 2);
                sw.Restart();
            }
            sw.Stop();
            brightness = 255;
        }

        private void MainLoop()
        {
            int pc = -1;
            bool gotZ = descentGame == 1;
            bool cursorFlash = false;

            while (playing)
            {
                int c = NextChar();
                bool print = true;
                if (c < 0)
                {
                    // end of screen...
                    if (gotZ)
                        WaitKey(cursorFlash);
                    FadeOut();
                    if (!interrupted)
                    {
                        EndOfBriefing();
                        Stop();
                    }
                    return;
                }

                BriefingScreen scr = screens[screenNum];

                if (c == '$') // special stuff
                {
                    print = false;
                    c = NextChar();
                    if (descentGame == 2 && c == '$')
                    {
                        print = true;
                    }
                    else if (descentGame == 2 && c == 'D')
                    {
                        int newNumber = DefineBriefingBox();
                        screenNum = newNumber;
                        lineAdjust = 0;
                        MoveText(screens[screenNum].ulx, screens[screenNum].uly);
                    }
                    else if (descentGame == 2 && c == 'U')
                    {
                        int newNumber = ReadMessageNumber();
                        if (newNumber >= screens.Count || screens[newNumber] == null)
                        {
                            ShowText("Invalid screen #" + newNumber);
                            playing = false;
                            return;
                        }
                        screenNum = newNumber;
                        MoveText(screens[screenNum].ulx, screens[screenNum].uly);
                        SkipRestOfLine();
                    }
                    else if (c == 'C')
                    {
                        if (!gotZ)
                        {
                            backgroundName = "END01.PCX";
                            DrawBackground();
                            UpdateBriefingColors();
                            gotZ = true;
                        }
                        int n = ReadMessageNumber() - 1;
                        if (n >= briefingFGcols.Length)
                        {
                            ShowText("Invalid color #" + n);
                            playing = false;
                            return;
                        }
                        briefingColor = n;
                        SkipRestOfLine();
                    }
                    else if (c == 'F')
                    {
                        cursorFlash = !cursorFlash;
                        SkipRestOfLine();
                    }
                    else if (c == 'T')
                    {
                        int n = ReadMessageNumber() * (highRes ? 2 : 1);
                        tabStop = n;
                        SkipRestOfLine();
                    }
                    else if (c == 'A')
                    {
                        lineAdjust = 1 - lineAdjust;
                    }
                    else if (descentGame == 2 && c == 'Z')
                    {
                        gotZ = true;
                        dumbAdjust = lineAdjust > 0 ? 1 : 2;
                        textIndex = ReadRestOfLine(text, textIndex, out string res);
                        --dumbAdjust;
                        backgroundName = res;
                        FadeOut();
                        DrawBackground();
                        FadeIn();
                        UpdateBriefingColors();
                    }
                    else if (descentGame == 2 && c == 'A')
                    {
                        // this controls dumbAdjust somehow on D2
                        // but that is not needed here
                        SkipRestOfLine();
                    }
                    else if (c == 'P')
                    {
                        // page change
                        WaitKey(cursorFlash);
                        if (interrupted) return;
                        NewPage(true);
                        SkipRestOfLine();
                    }
                    else if (c == 'R')
                    {
                        if (descentGame == 1)
                        {
                            robotSpinning = ReadMessageNumber();
                            bitmapDisplay = null;
                            SkipRestOfLine();
                        }
                        else if (descentGame == 2)
                        {
                            robotSpinning = -1;
                            bitmapDisplay = null;
                            robotMoviePlaying = "" + (char)NextChar();
                            SkipRestOfLine();
                        }
                    }
                    else if (c == 'N')
                    {
                        robotSpinning = -1;
                        textIndex = ReadRestOfLine(text, textIndex, out string res);
                        bitmapDisplay = null;
                        bitmapDisplayText = res + "#0";
                    }
                    else if (c == 'O')
                    {
                        robotSpinning = -1;
                        textIndex = ReadRestOfLine(text, textIndex, out string res);
                        bitmapDisplay = null;
                        bitmapDisplayText = res + "#0";
                    }
                    else if (c == 'B')
                    {
                        robotSpinning = -1;
                        textIndex = ReadRestOfLine(text, textIndex, out string res);
                        bitmapDisplay = null;
                        bitmapDisplayText = res + ".BBM";
                        byte[] bbmImage = findFile(bitmapDisplayText);
                        if (bbmImage != null)
                            TryLoadBBM(bbmImage);
                    }
                    else if (c == 'S')
                    {
                        if (descentGame == 1)
                        {
                            int newNumber = Math.Max(0, ReadMessageNumber() - 1);
                            if (newNumber != screenNum)
                            {
                                WaitKey(cursorFlash, 60);
                                if (interrupted) return;
                            }
                            if (newNumber >= screens.Count || screens[newNumber] == null)
                            {
                                ShowText("Invalid screen #" + newNumber);
                                playing = false;
                                return;
                            }
                            screenNum = newNumber;
                            FadeOut();
                            NewPage(true);
                            FadeIn();
                            SkipRestOfLine();
                        }
                        else if (descentGame == 2)
                        {
                            robotSpinning = -1;
                            bitmapDisplay = null;
                            // beginning of another section
                            // we can only reach this at the very beginning, so do nothing
                            // the beginning of the next section is never reached, instead it goes to if (c < 0) above
                            SkipRestOfLine();
                        }
                    }
                    DrawRegions();
                }
                else if (c == '\t')
                {
                    print = false;
                    if (tx - scr.ulx < tabStop)
                    {
                        tx = scr.ulx + tabStop;
                    }
                }
                else if (c == '\\')
                {
                    // do not write backslash
                    print = false;
                }
                else if (c == '\n')
                {
                    print = false;
                    if (pc != '\\')
                    {
                        // newline
                        tx = scr.ulx;
                        if (dumbAdjust > 0)
                            --dumbAdjust;
                        else
                            ty += highRes ? 16 : 8;

                        if (ty > scr.uly + scr.height)
                        {
                            DrawBackground();
                            MoveText(scr.ulx, scr.uly);
                        }
                    }
                }

                if (print)
                {
                    if (!gotZ)
                    {
                        backgroundName = "END01.PCX";
                        FadeOut();
                        DrawBackground();
                        FadeIn();
                        UpdateBriefingColors();
                        gotZ = true;
                    }

                    if (useDelay)
                    {
                        if (cursorFlash)
                        {
                            // draw cursor
                            int dummy = tx + 1;
                            lock (graphicsLock)
                                font.DrawCharacter(screen, '_', briefingFGcols[briefingColor], briefingBGcols[briefingColor], ref dummy, ty, false);
                        }
                        if (descentGame == 1)
                        {
                            Thread.Sleep(charDelay);
                        }
                        else if (descentGame == 2)
                        {
                            Thread.Sleep(1000 / 15);
                        }
                        if (cursorFlash)
                        {
                            // remove cursor
                            int dummy = tx + 1;
                            lock (graphicsLock)
                                font.DrawCharacter(screen, '_', clearColor, clearColor, ref dummy, ty, false);
                        }
                    }

                    tx += DrawCharacter(c);

                    if (keyPress)
                    {
                        useDelay = false;
                        keyPress = false;
                    }
                    DrawRegions();

                    if (tx > scr.ulx + scr.width)
                    {
                        tx = scr.ulx;
                        ty += scr.uly;
                    }

                    if (ty > scr.uly + scr.height)
                    {
                        WaitKey(cursorFlash);
                        if (interrupted) return;
                        NewPage(true);
                    }
                }
                pc = c;
            }
        }

        private void TryLoadBBM(byte[] bbmImage)
        {
            LibDescent.Data.BBMImage bbm = new LibDescent.Data.BBMImage();
            try
            {
                bbm.Read(bbmImage);
            }
            catch (Exception)
            {
                return;
            }

            Bitmap res = new Bitmap(bbm.Width, bbm.Height, PixelFormat.Format24bppRgb);
            BitmapData bmpData =
                res.LockBits(new Rectangle(0, 0, bbm.Width, bbm.Height),
                System.Drawing.Imaging.ImageLockMode.ReadWrite,
                res.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int stride = bbm.Width * 3;
            byte[] rgbData = bbm.GetRGBData();
            for (int y = 0; y < res.Height; ++y)
                System.Runtime.InteropServices.Marshal.Copy(rgbData, y * stride, ptr + y * bmpData.Stride, stride);
            res.UnlockBits(bmpData);
            bitmapDisplay = res;
            bitmapDisplayText = null;
        }

        private void DrawRegions()
        {
            lock (graphicsLock)
            {
                if (robotMoviePlaying != null)
                {
                    int bx = highRes ? 280 : 140;
                    int by = highRes ? 200 : 80;
                    int bw = highRes ? 320 : 160;
                    int bh = highRes ? 200 : 100;
                    graphics.FillRectangle(placeholderBrush, bx, by, bw, bh);
                    graphics.DrawRectangle(placeholderBorderPen, bx, by, bw, bh);
                    graphics.DrawString("RB" + robotMoviePlaying + ".MVE", specialFont, placeholderFontBrush, bx + 5, by + 5);
                }

                if (bitmapDisplay != null)
                {
                    int bx = 0, by = 0, bw = 0, bh = 0;
                    if (descentGame == 1)
                    {
                        bx = highRes ? 276 : 138;
                        by = highRes ? 132 : 55;
                        bw = highRes ? 332 : 166;
                        bh = highRes ? 331 : 138;
                    }
                    else if (descentGame == 2)
                    {
                        bx = highRes ? 440 : 220;
                        by = highRes ? 132 : 55;
                        bw = highRes ? 128 : 64;
                        bh = highRes ? 128 : 64;
                    }
                    graphics.DrawImageUnscaledAndClipped(bitmapDisplay, new Rectangle(bx, by, bw, bh));
                }

                if (robotSpinning >= 0)
                {
                    int bx = highRes ? 276 : 138;
                    int by = highRes ? 132 : 55;
                    int bw = highRes ? 332 : 166;
                    int bh = highRes ? 331 : 138;
                    graphics.FillRectangle(placeholderBrush, bx, by, bw, bh);
                    graphics.DrawRectangle(placeholderBorderPen, bx, by, bw, bh);
                    graphics.DrawString("Robot#" + robotSpinning, specialFont, placeholderFontBrush, bx + 5, by + 5);
                }

                if (bitmapDisplayText != null)
                {
                    int bx = 0, by = 0, bw = 0, bh = 0;
                    if (descentGame == 1)
                    {
                        bx = highRes ? 276 : 138;
                        by = highRes ? 132 : 55;
                        bw = highRes ? 332 : 166;
                        bh = highRes ? 331 : 138;
                    }
                    else if (descentGame == 2)
                    {
                        bx = highRes ? 440 : 220;
                        by = highRes ? 132 : 55;
                        bw = highRes ? 128 : 64;
                        bh = highRes ? 128 : 64;
                    }
                    graphics.FillRectangle(placeholderBrush, bx, by, bw, bh);
                    graphics.DrawRectangle(placeholderBorderPen, bx, by, bw, bh);
                    graphics.DrawString(bitmapDisplayText, specialFont, placeholderFontBrush, bx + 5, by + 5);
                }

                if (Properties.Settings.Default.showBriefingBox)
                {
                    BriefingScreen scr = screens[screenNum];
                    int bx = 0, by = 0, bw = 0, bh = 0;
                    bx = scr.ulx;
                    by = scr.uly;
                    bw = scr.width;
                    bh = scr.height;
                    graphics.DrawRectangle(helperBorderPen, bx, by, bw, bh);
                }
            }
        }

        private int DrawCharacter(int c)
        {
            int x = tx;
            lock (graphicsLock)
                font.DrawCharacter(screen, (char)c, briefingFGcols[briefingColor], briefingBGcols[briefingColor], ref x, ty, true);
            return x - tx;
        }

        public bool CanRender()
        {
            return rendering;
        }

        public void ApplyBrightnessToFlipped()
        {
            BitmapData data = flipped.LockBits(frame, System.Drawing.Imaging.ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            IntPtr ptr = data.Scan0;
            int bytes = data.Stride * data.Height;
            byte[] rgbValues = new byte[bytes];
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            for (int y = 0; y < frame.Height; ++y)
            {
                int p = y * data.Stride;
                for (int x = 0; x < frame.Width; ++x)
                {
                    rgbValues[p + x * 4 + 1] = (byte)Math.Max(0, rgbValues[p + x * 4 + 1] * brightness / 255);
                    rgbValues[p + x * 4 + 2] = (byte)Math.Max(0, rgbValues[p + x * 4 + 2] * brightness / 255);
                    rgbValues[p + x * 4 + 0] = (byte)Math.Max(0, rgbValues[p + x * 4 + 0] * brightness / 255);
                }
            }

            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            flipped.UnlockBits(data);
        }

        public Bitmap Render()
        {
            if (Monitor.TryEnter(graphicsLock, new TimeSpan(0, 0, 1)))
            {
                try
                {
                    flipper.DrawImageUnscaled(screen, 0, 0);
                    if (brightness < 255)
                    {
                        ApplyBrightnessToFlipped();
                    }
                    return flipped;
                }
                finally
                {
                    Monitor.Exit(graphicsLock);
                }
            }
            else
            {
                rendering = false;
                MessageBox.Show(null, "The lock is stuck", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        class BriefingScreen
        {
            public string bsName;
            public byte level;
            public byte message;
            public int ulx;
            public int uly;
            public int width;
            public int height;

            public BriefingScreen(string bsName, byte level, byte message, int ulx, int uly, int width, int height)
            {
                this.bsName = bsName;
                this.level = level;
                this.message = message;
                this.ulx = ulx;
                this.uly = uly;
                this.width = width;
                this.height = height;
            }
        }

        private static IEnumerable<BriefingScreen> GetBriefingScreens(int descentGame, bool highRes)
        {
            List<BriefingScreen> screens = new List<BriefingScreen>();

            BriefingScreen makeScaledBriefingScreen(string name, byte lvl, byte msg, int x, int y, int w, int h)
            {
                if (highRes)
                {
                    return new BriefingScreen(name, lvl, msg, x * 2, y * 12 / 5, w * 2, h * 12 / 5);
                }
                else
                {
                    return new BriefingScreen(name, lvl, msg, x, y, w, h);
                }
            }

            if (descentGame == 1)
            {
                // TITLES.C:309-370
                screens.Add(makeScaledBriefingScreen("brief01.pcx", 0, 1, 13, 140, 290, 59));
                screens.Add(makeScaledBriefingScreen("brief02.pcx", 0, 2, 27, 34, 257, 177));
                screens.Add(makeScaledBriefingScreen("brief03.pcx", 0, 3, 20, 22, 257, 177));
                screens.Add(makeScaledBriefingScreen("brief02.pcx", 0, 4, 27, 34, 257, 177));
                screens.Add(makeScaledBriefingScreen("moon01.pcx", 1, 5, 10, 10, 300, 170));
                screens.Add(makeScaledBriefingScreen("moon01.pcx", 2, 6, 10, 10, 300, 170));
                screens.Add(makeScaledBriefingScreen("moon01.pcx", 3, 7, 10, 10, 300, 170));
                screens.Add(makeScaledBriefingScreen("venus01.pcx", 4, 8, 15, 15, 300, 200));
                screens.Add(makeScaledBriefingScreen("venus01.pcx", 5, 9, 15, 15, 300, 200));
                screens.Add(makeScaledBriefingScreen("brief03.pcx", 6, 10, 20, 22, 257, 177));
                screens.Add(makeScaledBriefingScreen("merc01.pcx", 6, 11, 10, 15, 300, 200));
                screens.Add(makeScaledBriefingScreen("merc01.pcx", 7, 12, 10, 15, 300, 200));
                screens.Add(makeScaledBriefingScreen("brief03.pcx", 8, 13, 20, 22, 257, 177));
                screens.Add(makeScaledBriefingScreen("mars01.pcx", 8, 14, 10, 100, 300, 200));
                screens.Add(makeScaledBriefingScreen("mars01.pcx", 9, 15, 10, 100, 300, 200));
                screens.Add(makeScaledBriefingScreen("brief03.pcx", 10, 16, 20, 22, 257, 177));
                screens.Add(makeScaledBriefingScreen("mars01.pcx", 10, 17, 10, 100, 300, 200));
                screens.Add(makeScaledBriefingScreen("jup01.pcx", 11, 18, 10, 40, 300, 200));
                screens.Add(makeScaledBriefingScreen("jup01.pcx", 12, 19, 10, 40, 300, 200));
                screens.Add(makeScaledBriefingScreen("brief03.pcx", 13, 20, 20, 22, 257, 177));
                screens.Add(makeScaledBriefingScreen("jup01.pcx", 13, 21, 10, 40, 300, 200));
                screens.Add(makeScaledBriefingScreen("jup01.pcx", 14, 22, 10, 40, 300, 200));
                screens.Add(makeScaledBriefingScreen("saturn01.pcx", 15, 23, 10, 40, 300, 200));
                screens.Add(makeScaledBriefingScreen("brief03.pcx", 16, 24, 20, 22, 257, 177));
                screens.Add(makeScaledBriefingScreen("saturn01.pcx", 16, 25, 10, 40, 300, 200));
                screens.Add(makeScaledBriefingScreen("brief03.pcx", 17, 26, 20, 22, 257, 177));
                screens.Add(makeScaledBriefingScreen("saturn01.pcx", 17, 27, 10, 40, 300, 200));
                screens.Add(makeScaledBriefingScreen("uranus01.pcx", 18, 28, 100, 100, 300, 200));
                screens.Add(makeScaledBriefingScreen("uranus01.pcx", 19, 29, 100, 100, 300, 200));
                screens.Add(makeScaledBriefingScreen("uranus01.pcx", 20, 30, 100, 100, 300, 200));
                screens.Add(makeScaledBriefingScreen("uranus01.pcx", 21, 31, 100, 100, 300, 200));
                screens.Add(makeScaledBriefingScreen("neptun01.pcx", 22, 32, 10, 20, 300, 200));
                screens.Add(makeScaledBriefingScreen("neptun01.pcx", 23, 33, 10, 20, 300, 200));
                screens.Add(makeScaledBriefingScreen("neptun01.pcx", 24, 34, 10, 20, 300, 200));
                screens.Add(makeScaledBriefingScreen("pluto01.pcx", 25, 35, 10, 20, 300, 200));
                screens.Add(makeScaledBriefingScreen("pluto01.pcx", 26, 36, 10, 20, 300, 200));
                screens.Add(makeScaledBriefingScreen("pluto01.pcx", 27, 37, 10, 20, 300, 200));
                screens.Add(makeScaledBriefingScreen("aster01.pcx", 256 - 1, 38, 10, 90, 300, 200));
                screens.Add(makeScaledBriefingScreen("aster01.pcx", 256 - 2, 39, 10, 90, 300, 200));
                screens.Add(makeScaledBriefingScreen("aster01.pcx", 256 - 3, 40, 10, 90, 300, 200));
                screens.Add(makeScaledBriefingScreen("end01.pcx", 0x7f, 1, 23, 40, 320, 200));
                screens.Add(makeScaledBriefingScreen("end02.pcx", 0x7e, 1, 5, 5, 300, 200));
                screens.Add(makeScaledBriefingScreen("end01.pcx", 0x7e, 2, 23, 40, 320, 200));
                screens.Add(makeScaledBriefingScreen("end03.pcx", 0x7e, 3, 5, 5, 300, 200));
            }
            else if (descentGame == 2)
            {
                // TITLES.C:261
                screens.Add(makeScaledBriefingScreen("BRIEF03.PCX", 0, 3, 8, 8, 257, 177));
            }

            return screens;
        }
    }
}
