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
            public string Text;

            [DescriptionAttribute("The file name of the background used for this screen. Must be a PCX file.")]
            public string Background { get; set; }
            [DescriptionAttribute("The rectangle within which text will appear on the screen. All coordinates are relative to a 320x200 screen when scaled.")]
            public Rectangle TextRegion { get; set; }
            [DescriptionAttribute("Whether to show a cursor on this screen.")]
            public bool FlashCursor { get; set; }

            public BriefingScreen()
            {
                Text = "";
                Background = "END01.PCX";
                TextRegion = new Rectangle(20, 22, 257, 177);
                FlashCursor = true;
            }

            public BriefingScreen(BriefingScreen template)
            {
                Text = "";
                Background = template.Background;
                TextRegion = template.TextRegion;
                FlashCursor = template.FlashCursor;
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
