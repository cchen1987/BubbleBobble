/**
 * Enemy.cs - A basic graphic element to inherit from
 * 
 * Changes:
 * 0.01, 24-jul-2013: Initial version, based on SdlMuncher 0.12
 * 0.02, 22-jan-2016: Initial version of class Enemy
 * 0.03, 27-feb-2016: Initial version of AI
 * 0.04, 29-apr-2016: Reappears if got caught by shot and not exploded by player
 * 0.05, 06-may-2016: AI, if player not in range, move randomly, else, chase player
 */

using System;

namespace DamGame
{
    class Enemy : Sprite
    {
        protected bool isInBubble;
        protected Shot myShot;
        protected bool jumped;
        protected int option;
        protected bool isBaron;
        protected int levelTopMargin;

        public Enemy(int newX, int newY, Game g) : base(g)
        {       
            ChangeDirection(RIGHT);
            x = newX;
            y = newY + 15;
            xSpeed = 8;
            ySpeed = 8;
            width = 48;
            height = 44;
            stepsTillNextFrame = 5;
            currentStep = 0;
            isInBubble = false;
            jumped = false;
            startTime = DateTime.Now;
            option = 0;
            myGame = g;
            isBaron = false;
            levelTopMargin = 50 + 3 * 28;
        }

        public override void Move()
        {
            base.Move();
            Timer();
            // Check is player is in range to chase him
            if (!myGame.IsValidMove(x, y + ySpeed, x + width, y + height + ySpeed))
            {
                if (((myGame.GetPlayer().GetX() >= x - rangeX && myGame.GetPlayer().GetX() <= x + width / 2) ||
                    (myGame.GetPlayer().GetX() + myGame.GetPlayer().GetWidth() >= x - rangeX &&
                    myGame.GetPlayer().GetX() + myGame.GetPlayer().GetWidth() <= x + width / 2)) &&
                    ((myGame.GetPlayer().GetY() + myGame.GetPlayer().GetHeight() >= y - rangeY &&
                    myGame.GetPlayer().GetY() + myGame.GetPlayer().GetHeight() <= y + height) ||
                    (myGame.GetPlayer().GetY() >= y - rangeY && myGame.GetPlayer().GetY() <= y + height)) &&
                    myGame.IsValidMove(x - xSpeed, y, x - width - xSpeed, y + height) && !myGame.GetPlayer().Dead() &&
                    !myGame.GetPlayer().IsImmune())
                {
                    MoveLeft();
                }
                else if (((myGame.GetPlayer().GetX() <= x + rangeX && myGame.GetPlayer().GetX() >= x + width / 2) ||
                        (myGame.GetPlayer().GetX() + myGame.GetPlayer().GetWidth() <= x + rangeX &&
                        myGame.GetPlayer().GetX() + myGame.GetPlayer().GetWidth() >= x + width / 2)) &&
                        ((myGame.GetPlayer().GetY() + myGame.GetPlayer().GetHeight() >= y - rangeY &&
                        myGame.GetPlayer().GetY() + myGame.GetPlayer().GetHeight() <= y + height) ||
                        (myGame.GetPlayer().GetY() >= y - rangeY && myGame.GetPlayer().GetY() <= y + height)) &&
                        myGame.IsValidMove(x + xSpeed, y, x + width + xSpeed, y + height) && !myGame.GetPlayer().Dead() &&
                        !myGame.GetPlayer().IsImmune())
                {
                    MoveRight();
                }
                else
                {
                    // Random move for enemy if player not in range
                    Random r = new Random(); 
                    if (time % 4 == 0 || jumped)
                    {
                        option = r.Next(0, 10);
                        if ((option >= 7 && jumped) || (y < levelTopMargin || y + height < levelTopMargin))
                            option = r.Next(0, 7);
                        jumped = false;    
                    }

                    if (option < 3 && myGame.IsValidMove(x + xSpeed, y, x + width + xSpeed, y + height))
                    {
                        MoveRight();
                        if (!myGame.IsValidMove(x + xSpeed, y, x + width + xSpeed, y + height))
                        {
                            option = r.Next(0, 10);
                            jumped = false;
                        }
                    }
                    else if (option >= 3 && option < 7 && 
                            myGame.IsValidMove(x - xSpeed, y, x + width - xSpeed, y + height))
                    {
                        MoveLeft();
                        if (!myGame.IsValidMove(x - xSpeed, y, x + width - xSpeed, y + height))
                        {
                            option = r.Next(0, 10);
                            jumped = false;
                        }
                    }
                    else if (y < levelTopMargin || y + height < levelTopMargin)
                        option = r.Next(0, 7);
                    else if (!jumped && (y > levelTopMargin && y + height > levelTopMargin))
                    {
                        Jump();
                        jumped = true;
                    }
                }
            }
            /*if (x < 134 || x > 917 - width)
                xSpeed = -xSpeed;
            if (y < 71 || y > 722 - height)
                ySpeed = -ySpeed;

            x += xSpeed;
            y += ySpeed;

            if (xSpeed < 0)  
                ChangeDirection(LEFT);   
            else          
                ChangeDirection(RIGHT);

            NextFrame();*/   
        }
        
        // Check if is in bubble
        public bool IsInBubble()
        {
            return isInBubble;
        } 
        
        // Change enemy status to in bubble
        public void InBubble(Shot shot)
        {
            isInBubble = true;
            myShot = shot;        
        }
        
        // Reapear if shot's not exploded
        public void GetOutOfBubble()
        {
            if (myShot.GetTime() == 6)
            {
                x = myShot.GetX();
                y = myShot.GetY();
                xSpeed += 6;
                visible = true;
                isInBubble = false;
            }
        }
        
        // Return if is baron
        public bool IsBaron()
        {
            return isBaron;
        }
    }
}
