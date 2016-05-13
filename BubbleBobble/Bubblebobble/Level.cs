/**
 * Level.cs - A basic graphic element to inherit from
 * 
 * Changes:
 * 0.01, 24-jul-2013: Initial version, based on SdlMuncher 0.12
 * 0.02, 22-jan-2016: Draw map
 * 0.03, 29-feb-2016: Collisions with level
 * 0.04, 11-feb-2016: Improved collisions with level
 * 0.05, 14-feb-2016: Method GetValidItemPositions to find valid positions for items on map
 * 0.06, 29-apr-2016: Check for enemy position and return an array of x and y
 * 0.07, 29-apr-2016: Change level method
 * 0.08, 13-may-2016: Array of tile images, to change image after passing each level
 */

using System;
using System.Collections;

namespace DamGame
{
    class Level
    {
        byte tileWidth, tileHeight;
        int levelWidth, levelHeight;
        int leftMargin, topMargin;
        LevelDescriptions levelDescription;
        int currentLevel;
        int numEnemy;

        int[] enemyX, enemyY;

        int currentBrick, nextBrick,
            currentShadow, nextShadow;

        Image[] brick, shadow1, shadow2, shadow3,
            shadow4, shadow5, shadow6, shadow7;
         
        public Level()
        {
            tileWidth = 28;
            tileHeight = 28;
            levelWidth = 32;
            levelHeight = 25;
            leftMargin = 80;   
            topMargin = 50;
            levelDescription = new LevelDescriptions();
            currentLevel = 0;
            numEnemy = levelDescription.GetNumEnemy(currentLevel);
            enemyX = new int[numEnemy];
            enemyY = new int[numEnemy];

            // Tile images
            brick = new Image[] {
                new Image("data\\L1.png"),
                new Image("data\\L2.png"),
                new Image("data\\L3.png"),
                new Image("data\\L4.png"),
                new Image("data\\L5.png"),
                new Image("data\\L6.png"),
                new Image("data\\L7.png"),
            };

            shadow1 = new Image[] {
                new Image("data\\S1A.png"),
                new Image("data\\S1O.png"),
                new Image("data\\S1R.png"),
                new Image("data\\S1V.png")
            };
            shadow2 = new Image[] {
                new Image("data\\S2A.png"),
                new Image("data\\S2O.png"),
                new Image("data\\S2R.png"),
                new Image("data\\S2V.png")
            };
            shadow3 = new Image[] {
                new Image("data\\S3A.png"),
                new Image("data\\S3O.png"),
                new Image("data\\S3R.png"),
                new Image("data\\S3V.png")
            };
            shadow4 = new Image[] {
                new Image("data\\S4A.png"),
                new Image("data\\S4O.png"),
                new Image("data\\S4R.png"),
                new Image("data\\S4V.png")
            };
            shadow5 = new Image[] {
                new Image("data\\S5A.png"),
                new Image("data\\S5O.png"),
                new Image("data\\S5R.png"),
                new Image("data\\S5V.png")
            };
            shadow6 = new Image[] {
                new Image("data\\S6A.png"),
                new Image("data\\S6O.png"),
                new Image("data\\S6R.png"),
                new Image("data\\S6V.png")
            };
            shadow7 = new Image[] {
                new Image("data\\S7A.png"),
                new Image("data\\S7O.png"),
                new Image("data\\S7R.png"),
                new Image("data\\S7V.png")
            };

            // Tile index
            currentBrick = 0;
            nextBrick = 1;
            currentShadow = 0;
            nextShadow = 1;
        }

        // Change to next level
        public void ChangeLevel(int level)
        {
            // Change current brick and shadow to the next image
            currentBrick = nextBrick;
            currentShadow = nextShadow;

            // Set current level to next level
            currentLevel = level;

            // Set new enemies coordinates
            numEnemy = levelDescription.GetNumEnemy(currentLevel);
            enemyX = new int[numEnemy];
            enemyY = new int[numEnemy];
            SetEnemyCoords();

            // Randomly choose next brick and shadow
            Random rand = new Random();
            nextBrick = rand.Next(0, 7);
            nextShadow = rand.Next(0, 4);
        }

        // Store valid positions for enemies
        public void SetEnemyCoords()
        {
            int count = 0;
            for (int row = 0; row < levelHeight; row++) 
                for (int col = 0; col < levelWidth; col++)
                {
                    int xPos = leftMargin + col * tileWidth;
                    int yPos = topMargin + row * tileHeight;

                    if (levelDescription.GetLevelAt(currentLevel)[row][col] == '*')
                    {      
                        enemyX[count] = xPos;
                        enemyY[count] = yPos;
                        count++; 
                    }
                }   
        }

        // Return an array of x for each enemy
        public int[] GetEnemyX()
        {
            return enemyX;
        }

        // Return an array of y for each enemy
        public int[] GetEnemyY()
        {
            return enemyY;
        }

        // Return number of enemies in each level
        public int GetEnemyCount()
        {
            return levelDescription.GetNumEnemy(currentLevel);
        }

        // Draw the level on screen
        public void DrawOnHiddenScreen()
        {
            for (int row = 0; row < levelHeight; row++)
            {
                for (int col = 0; col < levelWidth; col++)
                {
                    int xPos = leftMargin + col * tileWidth;
                    int yPos = topMargin + row * tileHeight;

                    switch (levelDescription.GetLevelAt(currentLevel)[row][col])
                    {
                        case 'L': Hardware.DrawHiddenImage(brick[currentBrick], xPos, yPos); break;
                        case '<': Hardware.DrawHiddenImage(shadow1[currentShadow], xPos, yPos); break;
                        case '>': Hardware.DrawHiddenImage(shadow2[currentShadow], xPos, yPos); break;
                        case '#': Hardware.DrawHiddenImage(shadow3[currentShadow], xPos, yPos); break;
                        case '&': Hardware.DrawHiddenImage(shadow4[currentShadow], xPos, yPos); break;
                        case '%': Hardware.DrawHiddenImage(shadow5[currentShadow], xPos, yPos); break;
                        case '@': Hardware.DrawHiddenImage(shadow6[currentShadow], xPos, yPos); break;
                        case '.': Hardware.DrawHiddenImage(shadow7[currentShadow], xPos, yPos); break;
                    }
                    // Charge another level for scroll
                    if (currentLevel + 1 < 3)
                    {
                        xPos = leftMargin + col * tileWidth;
                        yPos = (topMargin + (row + levelHeight) * tileHeight);

                        switch (levelDescription.GetLevelAt(currentLevel + 1)[row][col])
                        {
                            case 'L': Hardware.DrawHiddenImage(brick[nextBrick], xPos, yPos); break;
                            case '<': Hardware.DrawHiddenImage(shadow1[nextShadow], xPos, yPos); break;
                            case '>': Hardware.DrawHiddenImage(shadow2[nextShadow], xPos, yPos); break;
                            case '#': Hardware.DrawHiddenImage(shadow3[nextShadow], xPos, yPos); break;
                            case '&': Hardware.DrawHiddenImage(shadow4[nextShadow], xPos, yPos); break;
                            case '%': Hardware.DrawHiddenImage(shadow5[nextShadow], xPos, yPos); break;
                            case '@': Hardware.DrawHiddenImage(shadow6[nextShadow], xPos, yPos); break;
                            case '.': Hardware.DrawHiddenImage(shadow7[nextShadow], xPos, yPos); break;
                        }
                    }
                }
            }
        }

        // Check if is valid move when player or enemy's jumping
        public bool IsValidMove(int xMin, int yMin, int xMax, int yMax, bool jump)
        {   
            for (int row = 0; row < levelHeight; row++)
                for (int col = 0; col < levelWidth; col++)
                {
                    char tileType = levelDescription.GetLevelAt(currentLevel)[row][col];                                      
                    // If we don't need to check collisions with this tile, we skip it               
                    if ((jump && col > 1 && col < 30) || (tileType == ' ')  // Empty space                                            
                            || (tileType == '<') || (tileType == '>')        
                            || (tileType == '#') || (tileType == '&')
                            || (tileType == '%') || (tileType == '@')
                            || (tileType == '*'))               
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
                    char tileType = levelDescription.GetLevelAt(currentLevel)[row][col];
                    // If we don't need to check collisions with this tile, we skip it               
                    if ((col > 1 && col < 30) && ((tileType == ' ')  // Empty space                                            
                            || (tileType == '<') || (tileType == '>')
                            || (tileType == '#') || (tileType == '&')
                            || (tileType == '%') || (tileType == '@')
                            || (tileType == '*')))
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

        // Check next floor tile for enemies 
        public bool NeedJump(int x, int y)
        {
            int col = (x - leftMargin) / tileWidth;
            int row = (y - topMargin) / tileHeight;

            char tileType = levelDescription.GetLevelAt(currentLevel)[row][col];
            // If we don't need to check collisions with this tile, we skip it               
            if (tileType == ' ')
                return true;  
            return false;
        }

        // This method check valid positions for items
        public ArrayList GetValidItemPositions(Item item)
        {
            ArrayList positions = new ArrayList();

            for (int row = 2; row < levelHeight; row++)   
                for (int col = 2; col < levelWidth - 4; col++)  
                    if (levelDescription.GetLevelAt(currentLevel)[row][col] == 'L' && 
                            levelDescription.GetLevelAt(currentLevel)[row][col+1] == 'L' &&
                            levelDescription.GetLevelAt(currentLevel)[row-1][col] != 'L' &&
                            levelDescription.GetLevelAt(currentLevel)[row-1][col+1] != 'L' &&
                            levelDescription.GetLevelAt(currentLevel)[row-2][col] != 'L' &&
                            levelDescription.GetLevelAt(currentLevel)[row-2][col+1] != 'L')  
                        positions.Add("" + ((row - 2) * tileHeight + topMargin + 
                            (2 * tileHeight - item.GetHeight())) + " " + 
                            (col * tileWidth + leftMargin));

            return positions;
        }

        // Return the total height of the level
        public short GetLevelHeight()
        {
            // Total height 700
            return (short)(levelHeight * tileHeight);
        }
    }
}
