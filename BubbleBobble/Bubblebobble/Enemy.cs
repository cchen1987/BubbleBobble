/**
 * Enemy.cs - A basic graphic element to inherit from
 * 
 * Changes:
 * 0.01, 24-jul-2013: Initial version, based on SdlMuncher 0.12
 * 0.02, 22-jan-2016: Initial version of class Enemy
 * 0.03, 27-feb-2016: Initial version of AI
 */

namespace DamGame
{
    class Enemy : Sprite
    {
        protected bool isInBubble;
        protected Shot myShot;

        public Enemy(int newX, int newY, Game g) : base(g)
        {       
            ChangeDirection(RIGHT);
            x = newX;
            y = newY;
            xSpeed = 8;
            ySpeed = 8;
            width = 48;
            height = 44;
            stepsTillNextFrame = 5;
            currentStep = 0;
            isInBubble = false;
            myGame = g;
        }

        public override void Move()
        {
            base.Move();
            if (!myGame.IsValidMove(x, y + ySpeed, x + width, y + height + ySpeed))
            {     
                if (x < myGame.GetPlayer().GetX() || (y + height <= myGame.GetPlayer().GetY() && 
                        y - height >= myGame.GetPlayer().GetY()) || x == myGame.GetPlayer().GetX())
                    MoveRight();
                else if (x >= myGame.GetPlayer().GetX() || (y + height <= myGame.GetPlayer().GetY() &&
                        y - height >= myGame.GetPlayer().GetY()))
                    MoveLeft();
                if (myGame.GetPlayer().GetY() < y - height * 2.5)
                    Jump();                         
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
        
        public bool IsInBubble()
        {
            return isInBubble;
        } 
        
        public void InBubble(Shot shot)
        {
            isInBubble = true;
            myShot = shot;        
        }
        
        public void GetOutOfBubble()
        {
            if (myShot.IsExploding())
            {
                x = myShot.GetX();
                y = myShot.GetY();
                xSpeed += 6;
                visible = true;
                isInBubble = false;
            } 
        }   
    }
}
