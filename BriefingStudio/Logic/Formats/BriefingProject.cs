using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BriefingStudio.Logic.Formats
{
    [Serializable()]
    class BriefingProject
    {
        public BriefingLevel Intro { get; set; }
        public List<BriefingLevel> Levels { get; set; }
        public BriefingLevel Outro { get; set; }
        public int LevelCount { get; set; }

        public BriefingProject()
        {
            Intro = new BriefingLevel();
            Levels = new List<BriefingLevel>();
            Outro = new BriefingLevel();
            LevelCount = 1;
        }

        public string ToBriefing(int levelCount)
        {
            string res = "";
            int i = 0;
            res += Intro.ToBriefing(i++);
            foreach (BriefingLevel level in Levels.Take(levelCount))
                res += level.ToBriefing(i++);
            res += Outro.ToBriefing(i++);
            res += "$S999\n";
            return res.Replace("\n", "\r\n");
        }

        public string ToBriefing()
        {
            return ToBriefing(Levels.Count);
        }

        [Serializable()]
        public class BriefingLevel
        {
            public List<BriefingScreen> Screens { get; set; }

            public BriefingLevel()
            {
                Screens = new List<BriefingScreen>();
            }

            public string ToBriefing(int index)
            {
                if (Screens.Count < 1) return "";
                bool cursor = false;
                bool first = true;
                string res = "";
                int message = 1;
                foreach (BriefingScreen screen in Screens)
                {
                    if (first)
                        first = false;
                    else
                        res += "$P\n";
                    res += screen.ToBriefing(index, ref message, ref cursor);
                }

                return "$S" + index + "\n" + res;
            }
        }

        [Serializable()]
        public class BriefingScreen
        {
            public string Text = "";
            private Rectangle _region = new Rectangle(20, 22, 257, 177);
            private int _tabStop = 0;

            [DescriptionAttribute("The file name of the background used for this screen. Must be a PCX file.")]
            public string Background { get; set; }
            [DescriptionAttribute("The rectangle within which text will appear on the screen. All coordinates are relative to a 320x200 screen for scaling.")]
            public Rectangle TextRegion { get { return _region; } set { _region = ClampRectangle(value); } }
            [DescriptionAttribute("Whether to show a cursor on this screen.")]
            public bool FlashCursor { get; set; }
            [DescriptionAttribute("The pixel from the left of the text region at which tabs should stop, relative to a 320x200 screen for scaling.")]
            public int TabStop { get { return _tabStop; } set { _tabStop = Clamp(value, 0, 319); } }

            public BriefingScreen()
            {
                Text = "";
                Background = "END01.PCX";
                TabStop = 0;
                TextRegion = new Rectangle(20, 22, 257, 177);
                FlashCursor = true;
            }

            public BriefingScreen(BriefingScreen template)
            {
                Text = "";
                Background = template.Background;
                TabStop = template.TabStop;
                TextRegion = template.TextRegion;
                FlashCursor = template.FlashCursor;
            }

            private static int Clamp(int value, int min, int max)
            {
                return Math.Max(min, Math.Min(max, value));
            }

            private static Rectangle ClampRectangle(Rectangle orig)
            {
                int x = Clamp(orig.Left, 0, 319);
                int y = Clamp(orig.Top, 0, 199);
                return new Rectangle(x, y, Clamp(orig.Width, 1, 320 - x), Clamp(orig.Height, 1, 200 - y));
            }

            public string ToBriefing(int level, ref int message, ref bool cursor)
            {
                string header = "";
                header += $"$D1 {Background} {level} {message} {TextRegion.Left} {TextRegion.Top} {TextRegion.Width} {TextRegion.Height}\n";
                header += $"$Z{Background}\n";
                if (cursor != FlashCursor)
                {
                    header += "$F\n";
                }
                header += $"$T{TabStop}\n";
                ++message;
                cursor = FlashCursor;
                return header + Text;
            }

            public string ToBriefing()
            {
                int message = 1;
                bool flash = false;
                return ToBriefing(1, ref message, ref flash);
            }
        }
    }
}
