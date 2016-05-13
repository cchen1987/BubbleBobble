/**
 * Shot.cs - A basic graphic element to inherit from
 * 
 * Changes:                
 * 0.01, 17-feb-2016: Initial version of class Shot
 * 0.02, 21-feb-2016: Improved move for bubbles 
 * 0.03, 24-feb-2016: Corrections for bubble Move logic
 * 0.04, 26-feb-2016: Shot timer and explode animation
 * 0.05, 29-apr-2016: Shot disappear after 6 minutes if got enemy in
 * 0.06, 06-may-2016: Initial animation when fired
 */

using System;

namespace DamGame
{      
    class Shot : Sprite
    {
        enum directions { FIRE, MOVE, UP, GOTENEMY };
        private int xMax;
        private int yMax;    
        private bool right;
        private bool gotEnemy;
        private bool explode;
        private Sprite mySprite;
        private Sprite myEnemy; 
        private int timeToExplode;

        public Shot(Sprite sprite, Game g) : base(g)
        {
            numDirections = 4;
            LoadSequence((int)directions.FIRE, new string[] { "data/bubble11.png", "data/bubble12.png", "data/bubble13.png", "data/bubble14.png",
                "data/bubble15.png", "data/bubble16.png", "data/bubble17.png", "data/bubble18.png", "data/bubble19.png" });
            LoadSequence((int)directions.UP, new string[] { "data/bubble20.png", "data/bubble3.png", "data/bubble2.png", "data/explosion.png" });
            LoadSequence((int)directions.MOVE, new string[] { "data/bubble20.png" });
            LoadSequence((int)directions.GOTENEMY, new string[] { "data/enemyInBubble.png" });
            ChangeDirection((int)directions.FIRE);
            width = 5;
            height = 4;
            xSpeed = 20;
            ySpeed = 3;
            yMax = 104;
            timeToExplode = 7;
            startTime = DateTime.Now;   
            mySprite = sprite;
            myGame = g;
            right = true;
            gotEnemy = false;
            visible = false;
            explode = false;
            rangeX = 250;
            currentStep = 0;
        }       
        
        public override void Move()
        {         
            if (time != timeToExplode)
            {
                // Horizontal move
                if (visible && right)
                {
                    x += xSpeed;
                    if (x > xMax || !myGame.IsValidMove(x + xSpeed, y, x + width + xSpeed, y + height) ||
                            gotEnemy)
                    {
                        xSpeed = 0;
                        ChangeDirection(UP);
                    }
                }
                else if (visible && !right)
                {
                    x -= xSpeed;
                    if (x < xMax || !myGame.IsValidMove(x - xSpeed, y, x + width - xSpeed, y + height) ||
                            gotEnemy)
                    {
                        xSpeed = 0;
                        ChangeDirection(UP);
                    }
                }

                // Vertical move when xSpeed is 0
                if (xSpeed == 0)
                {
                    y -= ySpeed;
                    if (y < yMax)
                        ySpeed = 0;
                }

                // Change initial frames when fired
                ChangeSequence();

                // Change frames after 3 seconds
                if (time == 3 && !gotEnemy)
                {
                    NextFrame(0);
                }
                // Change frames after 4 seconds
                else if (time == 4 && !gotEnemy)
                {
                    NextFrame(1);
                }
                // Change frames after 5 seconds
                else if (time == 5 && !gotEnemy)
                {
                    NextFrame(2);
                }
                // Change frames after 6 seconds
                else if (time == 6 && !gotEnemy)
                {
                    NextFrame(3);
                    explode = true;
                    ySpeed = 0;
                }
                else if (time == 6 && gotEnemy)
                {
                    explode = true;
                    visible = false; 
                }
            }
            else
                visible = false;
        } 

        // Activate the shot, get sprite's x and y
        public void Activate()
        {    
            visible = true;
            y = mySprite.GetY();
            startY = mySprite.GetY();

            // Check if sprite is moving right or left
            if (mySprite.IsMovingRight())
            {
                xMax = mySprite.GetX() + mySprite.GetWidth() + rangeX;
                right = true;
                startX = mySprite.GetX() + mySprite.GetWidth() - 8;
                x = mySprite.GetX() + mySprite.GetWidth() - 8; // Pixels adjusting 
                if (x + width > 920) 
                    x = 920 - 20;
            }
            else
            {
                xMax = mySprite.GetX() - rangeX;
                right = false;
                startX = mySprite.GetX() - width + 8;
                x = mySprite.GetX() - width + 8; // Pixels adjusting
                if (x < 136)
                    x = 136 - 20;
            }   
        }   
        
        // Update shot info when got enemy
        public void GotEnemyIn(Sprite enemy)
        {
            myEnemy = enemy;
            gotEnemy = true;
            x = enemy.GetX();
            y = enemy.GetY();
            height = 44;
            width = 48;
            ChangeDirection((int)directions.GOTENEMY);
            NextFrame(0);
        } 

        public bool GotEnemy()
        {       
            return gotEnemy;
        }
        
        public int GetTime()
        {
            return time;
        } 

        public bool IsExploding()
        {
            return explode;
        }

        public Sprite GetEnemy()
        {
            return myEnemy;
        }

        // Changing initial frames when fired
        private void ChangeSequence()
        {
            if (!gotEnemy && xSpeed != 0)
            {
                // Adjust width, height, and y for each frame
                if (x - startX == xSpeed || x - startX == -xSpeed)
                {
                    height = 4;
                    width = 5;
                    y = startY + mySprite.GetHeight() / 2 - height / 2;
                    NextFrame(0);
                }
                if (x - startX == xSpeed * 2 || x - startX == -xSpeed * 2)
                {
                    height = 9;
                    width = 10;
                    y = startY + mySprite.GetHeight() / 2 - height / 2;
                    NextFrame(1);
                }
                if (x - startX == xSpeed * 3 || x - startX == -xSpeed * 3)
                {
                    height = 13;
                    width = 14;
                    y = startY + mySprite.GetHeight() / 2 - height / 2;
                    NextFrame(2);
                }
                if (x - startX == xSpeed * 4 || x - startX == -xSpeed * 4)
                {
                    height = 18;
                    width = 19;
                    y = startY + mySprite.GetHeight() / 2 - height / 2;
                    NextFrame(3);
                }
                if (x - startX == xSpeed * 5 || x - startX == -xSpeed * 5)
                {
                    height = 22;
                    width = 24;
                    y = startY + mySprite.GetHeight() / 2 - height / 2;
                    NextFrame(4);
                }
                if (x - startX == xSpeed * 6 || x - startX == -xSpeed * 6)
                {
                    height = 26;
                    width = 29;
                    y = startY + mySprite.GetHeight() / 2 - height / 2;
                    NextFrame(5);
                }
                if (x - startX == xSpeed * 7 || x - startX == -xSpeed * 7)
                {
                    height = 31;
                    width = 34;
                    y = startY + mySprite.GetHeight() / 2 - height / 2;
                    NextFrame(6);
                }
                if (x - startX == xSpeed * 8 || x - startX == -xSpeed * 8)
                {
                    height = 35;
                    width = 38;
                    y = startY + mySprite.GetHeight() / 2 - height / 2;
                    NextFrame(7);
                }
                if (x - startX == xSpeed * 10 || x - startX == -xSpeed * 10)
                {
                    height = 40;
                    width = 43;
                    y = startY + mySprite.GetHeight() / 2 - height / 2;
                    NextFrame(8);
                }
                if (x - startX >= xSpeed * 11 || x - startX <= -xSpeed * 11)
                {
                    height = 44;
                    width = 48;
                    y = startY;
                    ChangeDirection((int)directions.MOVE);
                    NextFrame(0);
                }
            }
            else if (!gotEnemy && xSpeed == 0)
                ChangeDirection((int)directions.UP);  
        }   

        public void NextFrame(int nextFrame)
        {
            currentFrame = nextFrame;
        }

        public override void DrawOnHiddenScreen()
        {
            if (!visible)
                return;
            Hardware.DrawHiddenImage(
                sequence[currentDirection][currentFrame], x, y); 
        }
    }
}
