/**
 * Player.cs - A basic graphic element to inherit from
 * 
 * Changes:
 * 0.01, 24-jul-2013: Initial version, based on SdlMuncher 0.12 
 * 0.02, 05-feb-2016: Image sequence
 * 0.03, 11-feb-2016: Included jump methods 
 * 0.04, 11-feb-2016: Move methods: MoveLeft, MoveRight, MoveDown and Jump
 * 0.05, 12-feb-2016: Jump and gravity
 */

namespace DamGame
{
    class Player : Sprite
    {
        private int life;
        private int points;
        private int reviveTime;
                          
        public Player(Game g) : base(g)
        {
            LoadSequence(RIGHT ,new string[] { "data/playerRight1.png", "data/playerRight2.png" });
            LoadSequence(LEFT, new string[] { "data/playerLeft1.png", "data/playerLeft2.png" });
            LoadSequence(FIRELEFT, new string[] { "data/playerFireLeft.png", "data/playerLeft1.png" });
            LoadSequence(FIRERIGHT, new string[] { "data/playerFireRight.png", "data/playerRight1.png" });
            LoadSequence(DEAD, new string[] { "data/playerDead.png", "data/playerDead.png", "data/playerDead.png",
                "data/playerDead2.png", "data/playerDead2.png" , "data/playerDead2.png"});
            LoadSequence(DISAPPEARING, new string[] { "data/transparent.png", "data/playerRight1.png", "data/playerRight2.png" });
            
            ChangeDirection(RIGHT);
            startX = 175;
            startY = 673;
            x = startX;
            y = startY;
            xSpeed = 6;
            ySpeed = 7;
            width = 48;
            height = 48;
            life = 4;
            points = 0;
            reviveTime = 3;     
            stepsTillNextFrame = 6;
            isRight = true;
            isDead = false;                
            myGame = g;   
        }  

        public void SubtLife()
        {
            life--;
        }

        public void LifeUp()
        {
            life++;
        }

        public int GetLife()
        {        
            return life;
        }     

        public void SetPoints(int points)
        {
            this.points = points;
        }

        public int GetPoints()
        {
            return points;
        }

        // Jump on bubble
        /*public void JumpOnBubble(Shot shot)
        {           
            if (y + height + ySpeed >= shot.GetY() && ((x >= shot.GetX() && x <= shot.GetX() + shot.GetWidth()) ||
                    (x + width >= shot.GetX() && x + width <= shot.GetX() + shot.GetWidth())))
            {
                jumping = true;
                jumpXspeed = 0;
                base.Move();
            }
            else
            {
                
            }
        } */  
    }
}
