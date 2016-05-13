/**
 * LevelDescriptions.cs - A basic graphic element to inherit from
 * 
 * Changes:
 * 0.01, 24-jul-2013: Initial version with 2 levels
 * 0.02, 29-apr-2016: Get number of enemies in each map
 * 0.03, 29-apr-2016: Return level at index method
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
                "LL>        *                  LL",
                "LL>                           LL",
                "LLLL%  LLLLLLLLLLLLLLLLLL%  LLLL",
                "LL#<&  @<<<<<<<<<<<<<<<<<&  @<LL",
                "LL>                           LL",
                "LL>             *             LL",
                "LL>                           LL",
                "LLLL%  LLLLLLLLLLLLLLLLLL%  LLLL",
                "LL#<&  @<<<<<<<<<<<<<<<<<&  @<LL",
                "LL>                           LL",
                "LL>                 *         LL",
                "LL>                           LL",
                "LLLL%  LLLLLLLLLLLLLLLLLL%  LLLL",
                "LL#<&  @<<<<<<<<<<<<<<<<<&  @<LL",
                "LL>                           LL",
                "LL>                           LL",
                "LL>                           LL",
                "..LLLLLLLLLLLLLLLLLLLLLLLLLLLLLL"    
            };
            descriptions[1] = new string[25]
            {
                "  LLLLLLLLLLLLLLLLLLLLLLLLLLLLLL",
                "LL#<<<<<<<<<<<<<<<<<<<<<<<<<<<LL",
                "LL>             *             LL",
                "LL>                           LL",
                "LL>          LLLLLL%          LL",
                "LL>          @<<<<<&          LL",
                "LL>         *       *         LL",
                "LL>                           LL",
                "LL>                           LL",
                "LL>       LLLLL% LLLLL%       LL",
                "LL>       @<<<<& @<<<<&       LL",
                "LL>                           LL",
                "LL>             *             LL",
                "LL>                           LL",
                "LL>    LLLLLLLLLLLLLLLLLL%    LL",
                "LL>    @<<<<<<<<<<<<<<<<<&    LL",
                "LL>                           LL",
                "LL>      *              *     LL",
                "LL>                           LL",
                "LL> LLLLLLL% LLLLLL% LLLLLLL% LL",
                "LL> @<<<<<<& @<<<<<& @<<<<<<& LL",
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
            descriptions[2] = new string[25]
            {
                "  LLLLLLL%   LLLLLL%   LLLLLLLLL",
                "LL#<<<<<<&   <<<<<<&   <<<<<<<LL",
                "LL>      *            *       LL",
                "LL>                           LL",
                "LL>  LLLLLLLL%     LLLLLLLL%  LL",
                "LL>  L#<<<<<<&     @<<<<<<L>  LL",
                "LL>  L>                   L>  LL",
                "LL>  L>*                * L>  LL",
                "LL>  L>                   L>  LL",
                "LL>  LLLLLLLLL%   LLLLLLLLL>  LL",
                "LL>  L#<<<<<<<&   @<<<<<<<L>  LL",
                "LL>  L>                   L>  LL",
                "LL>  L>   *          *    L>  LL",
                "LL>  L>                   L>  LL",
                "LL>  LLLLLLLLLL% LLLLLLLLLL>  LL",
                "LL>  @<<<<<<<<<& @<<<<<<<<<&  LL",
                "LL>                           LL",
                "LL>                           LL",
                "LL>                           LL",
                "LLLLLLLL% LLL%     LLL% LLLLLLLL",
                "LL#<<<<<& @<<&     <<<& @<<<<<LL",
                "LL>                           LL",
                "LL>                           LL",
                "LL>                           LL",
                "..LLLLLLL%   LLLLLL%   LLLLLLLLL"
            };
        }

        // Return a level
        public string[] GetLevelAt(int index)
        {
            return descriptions[index];
        }

        // Return the number of enemies in each level
        public int GetNumEnemy(int index)
        {
            int numEnemy = 0;
            for (int i = 0; i < descriptions[index].Length; i++)
                for (int j = 0; j < descriptions[index][i].Length; j++) 
                    if (descriptions[index][i][j] == '*') 
                        numEnemy++; 
            return numEnemy;
        }
    }
}

