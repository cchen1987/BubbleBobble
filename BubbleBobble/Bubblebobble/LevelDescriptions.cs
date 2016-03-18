/**
 * LevelDescriptions.cs - A basic graphic element to inherit from
 * 
 * Changes:
 * 0.01, 24-jul-2013: Initial version with 2 levels
 */

namespace DamGame
{
    class LevelDescriptions
    {
        const int totalLevels = 100;
        private string[][] descriptions = new string[100][];

        public LevelDescriptions()
        {
            descriptions[0] = new string[25] 
            {
                "  LLLLLLLLLLLLLLLLLLLLLLLLLLLLLL",
                "LL#<<<<<<<<<<<<<<<<<<<<<<<<<<<LL",
                "LL>                           LL",
                "LL>                           LL",
                "LL>                           LL",
                "LL>                           LL",
                "LL>                           LL",
                "LL>                           LL",
                "LL>                           LL",
                "LLLL%  LLLLLLLLLLLLLLLLLL%  LLLL",
                "LL#<&  @<<<<<<<<<<<<<<<<<&  @<LL",
                "LL>                           LL",
                "LL>                           LL",
                "LL>                           LL",
                "LLLL%  LLLLLLLLLLLLLLLLLL%  LLLL",
                "LL#<&  @<<<<<<<<<<<<<<<<<&  @<LL",
                "LL>                           LL",
                "LL>                           LL",
                "LL>                           LL",
                "LLLL%  LLLLLLLLLLLLLLLLLL%  LLLL",
                "LL#<&  @<<<<<<<<<<<<<<<<<&  @<LL",
                "LL>                           LL",
                "LL>                           LL",
                "LL>                           LL",
                "..LLLLLLLLLLLLLLLLLLLLLLLLLLLLLL"    
            };
            /* L2 = L (Brick#)
             * S1 = Shadow1-> <
             * S2 = Shadow2-> >
             * S3 = Shadow3-> #
             * S4 = Shadow4-> &
             * S5 = Shadow5-> %
             * S6 = Shadow6-> @
             * S7 = Shadow7-> . 
             */ 
            descriptions[1] = new string[25]
            {
                "  LLLLLLLLLLLLLLLLLLLLLLLLLLLLLL",
                "LL#<<<<<<<<<<<<<<<<<<<<<<<<<<<LL",
                "LL>                           LL",
                "LL>                           LL",
                "LL>          LLLLLL%          LL",
                "LL>          @<<<<<&          LL",
                "LL>                           LL",
                "LL>                           LL",
                "LL>                           LL",
                "LL>       LLLLL% LLLLL%       LL",
                "LL>       @<<<<% @<<<<&       LL",
                "LL>                           LL",
                "LL>                           LL",
                "LL>                           LL",
                "LL>    LLLLLLLLLLLLLLLLLL%    LL",
                "LL>    @<<<<<<<<<<<<<<<<<&    LL",
                "LL>                           LL",
                "LL>                           LL",
                "LL>                           LL",
                "LL> LLLLLLL% LLLLLL% LLLLLLL% LL",
                "LL> @<<<<<<& @<<<<<& @<<<<<<& LL",
                "LL>                           LL",
                "LL>                           LL",
                "LL>                           LL",
                "..LLLLLLLLLLLLLLLLLLLLLLLLLLLLLL"
            };
        }

        public string[][] GetLevels()
        {
            return descriptions;
        }

        public string[] GetLevelsAt(int index)
        {
            return descriptions[index];
        }
    }
}

