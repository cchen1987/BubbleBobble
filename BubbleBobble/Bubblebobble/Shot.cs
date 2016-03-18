/**
 * Shot.cs - A basic graphic element to inherit from
 * 
 * Changes:                
 * 0.01, 17-feb-2016: Initial version of class Shot
 * 0.02, 21-feb-2016: Improved move for bubbles 
 * 0.03, 24-feb-2016: Corrections for bubble Move logic
 * 0.04, 26-feb-2016: Shot timer and explode animation
 */

using System;

namespace DamGame
{
    class Shot : Sprite
    {
        private int xMax;
        private int yMax;    
        private bool right;
        private bool gotEnemy;
        private bool explode;
        private Sprite mySprite; 
        private int timeToExplode;
                                    
        public Shot(Sprite sprite, Game g) : base(g)
        {
            LoadImage("data/bubble.png");
            width = 48;
            height = 44;
            xSpeed = 12;
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
        }       
        
        public override void Move()
        {              
            if (time != timeToExplode)
            {
                if (visible && right)
                {
                    x += xSpeed;
                    if (x > xMax || !myGame.IsValidMove(x + xSpeed, y, x + width + xSpeed, y + height) ||
                            gotEnemy)
                        xSpeed = 0;
                }
                else if (visible && !right)
                {
                    x -= xSpeed;
                    if (x < xMax || !myGame.IsValidMove(x - xSpeed, y, x + width - xSpeed, y + height) ||
                            gotEnemy)
                        xSpeed = 0;
                }
                if (xSpeed == 0)
                {
                    y -= ySpeed;
                    if (y < yMax)
                        ySpeed = 0;
                }
                if (time == 3 && !gotEnemy)
                {
                    LoadImage("data/bubble3.png");
                }
                else if (time == 5 && !gotEnemy)
                {
                    LoadImage("data/bubble2.png");
                }
                else if (time == 6 && !gotEnemy)
                {                 
                    LoadImage("data/explosion.png");
                    explode = true;
                    ySpeed = 0;
                }
            }
            else
                visible = false;
        } 

        public void Activate()
        {    
            visible = true;
            y = mySprite.GetY();

            if (mySprite.IsMovingRight())
            {
                xMax = mySprite.GetX() + mySprite.GetWidth() + 250;
                right = true;
                x = mySprite.GetX() + mySprite.GetWidth() - 8; // Pixels adjusting 
                if (x + width > 920) 
                    x = 920 - 20;
            }
            else
            {
                xMax = mySprite.GetX() - 250;
                right = false;
                x = mySprite.GetX() - width + 8; // Pixels adjusting
                if (x < 136)
                    x = 136 - 20;
            }   
        }   
        
        public void GotEnemyIn(Sprite newSprite)
        {
            gotEnemy = true;
            x = newSprite.GetX();
            y = newSprite.GetY();
            LoadImage("data/enemyInBubble.png");
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
    }
}
