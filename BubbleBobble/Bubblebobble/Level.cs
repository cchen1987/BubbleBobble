/**
 * Level.cs - A basic graphic element to inherit from
 * 
 * Changes:
 * 0.01, 24-jul-2013: Initial version, based on SdlMuncher 0.12
 * 0.02, 22-jan-2016: Draw map
 * 0.03, 29-feb-2016: Collisions with level
 * 0.04, 11-feb-2016: Improved collisions with level
 * 0.05, 14-feb-2016: Method GetValidPositions to find valid positions for items on map
 */

using System.Collections;

namespace DamGame
{
    class Level
    {
        byte tileWidth, tileHeight;
        int levelWidth, levelHeight;
        int leftMargin, topMargin;
        /* In spanish: para hacer el scroll vertical de niveles
         * moverlo con posicion hasta modulo de 768
         */
        LevelDescriptions levelsDescription; 
        string[] levelDescription;

        Image brick, shadow1, shadow2, shadow3,
            shadow4, shadow5, shadow6, shadow7;
         
        public Level()
        {
            tileWidth = 28;
            tileHeight = 28;
            levelWidth = 32;
            levelHeight = 25;
            leftMargin = 80;   
            topMargin = 50;
            levelsDescription = new LevelDescriptions();

            levelDescription = new string[25]
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

            brick = new Image("data\\Brick1.png");
            shadow1 = new Image("data\\Shadow1R.png");
            shadow2 = new Image("data\\Shadow2R.png");
            shadow3 = new Image("data\\Shadow3R.png");
            shadow4 = new Image("data\\Shadow4R.png");
            shadow5 = new Image("data\\Shadow5R.png");
            shadow6 = new Image("data\\Shadow6R.png");
            shadow7 = new Image("data\\Shadow7R.png");

        }
        public void DrawOnHiddenScreen()
        {
            for (int row = 0; row < levelHeight; row++)
            {
                for (int col = 0; col < levelWidth; col++)
                {
                    int xPos = leftMargin + col * tileWidth;
                    int yPos = topMargin + row * tileHeight;

                    switch (levelDescription[row][col])
                    {
                        case 'L': Hardware.DrawHiddenImage(brick, xPos, yPos); break;
                        case '<': Hardware.DrawHiddenImage(shadow1, xPos, yPos); break;
                        case '>': Hardware.DrawHiddenImage(shadow2, xPos, yPos); break;
                        case '#': Hardware.DrawHiddenImage(shadow3, xPos, yPos); break;
                        case '&': Hardware.DrawHiddenImage(shadow4, xPos, yPos); break;
                        case '%': Hardware.DrawHiddenImage(shadow5, xPos, yPos); break;
                        case '@': Hardware.DrawHiddenImage(shadow6, xPos, yPos); break;
                        case '.': Hardware.DrawHiddenImage(shadow7, xPos, yPos); break;
                    }                                                   
                }                
            }
        }  

        public bool IsValidMove(int xMin, int yMin, int xMax, int yMax, bool jump)
        {   
            for (int row = 0; row < levelHeight; row++)
                for (int col = 0; col < levelWidth; col++)
                {
                    char tileType = levelDescription[row][col];                                      
                    // If we don't need to check collisions with this tile, we skip it               
                    if ((jump && col > 1 && col < 30) || (tileType == ' ')  // Empty space                                            
                            || (tileType == '<') || (tileType == '>')        
                            || (tileType == '#') || (tileType == '&')
                            || (tileType == '%') || (tileType == '@'))               
                        continue;                                                                    
                    // Otherwise, lets calculate its corners and check rectangular collisions
                    int xPos = leftMargin + col * tileWidth;
                    int yPos = topMargin + row * tileHeight;
                    int xLimit = leftMargin + (col + 1) * tileWidth;
                    int yLimit = topMargin + (row + 1) * tileHeight;

                    if (Sprite.CheckCollisions(
                            xMin, yMin, xMax, yMax,  // Coords of the sprite
                            xPos, yPos, xLimit, yLimit)) // Coords of current tile
                        return false;
                }
            // If we have not collided with anything... then we can move
            return true;
        }
        
        public bool IsValidMove(int xMin, int yMin, int xMax, int yMax)
        {
            for (int row = 0; row < levelHeight; row++)
                for (int col = 0; col < levelWidth; col++)
                {
                    char tileType = levelDescription[row][col];
                    // If we don't need to check collisions with this tile, we skip it               
                    if ((col > 1 && col < 30) && ((tileType == ' ')  // Empty space                                            
                            || (tileType == '<') || (tileType == '>')
                            || (tileType == '#') || (tileType == '&')
                            || (tileType == '%') || (tileType == '@')))
                        continue;
                    // Otherwise, lets calculate its corners and check rectangular collisions
                    int xPos = leftMargin + col * tileWidth;
                    int yPos = topMargin + row * tileHeight;
                    int xLimit = leftMargin + (col + 1) * tileWidth;
                    int yLimit = topMargin + (row + 1) * tileHeight;

                    if (Sprite.CheckCollisions(
                            xMin, yMin, xMax, yMax,  // Coords of the sprite
                            xPos, yPos, xLimit, yLimit)) // Coords of current tile
                        return false;
                }
            // If we have not collided with anything... then we can move
            return true;
        }
        
        // This method check valid positions for items
        public ArrayList GetValidPositions(Item item)
        {
            ArrayList positions = new ArrayList();

            for (int row = 2; row < levelHeight; row++)   
                for (int col = 2; col < levelWidth - 4; col++)  
                    if (levelDescription[row][col] == 'L' && 
                            levelDescription[row][col+1] == 'L' &&
                            levelDescription[row-1][col] != 'L' &&
                            levelDescription[row-1][col+1] != 'L' &&
                            levelDescription[row-2][col] != 'L' &&
                            levelDescription[row-2][col+1] != 'L')  
                        positions.Add("" + ((row - 2) * tileHeight + topMargin + 
                            (2 * tileHeight - item.GetHeight())) + " " + 
                            (col * tileWidth + leftMargin));

            return positions;
        } 
    }
}
