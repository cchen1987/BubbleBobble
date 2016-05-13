/**
 * Player.cs - A basic graphic element to inherit from
 * 
 * Changes:
 * 0.01, 24-jul-2013: Initial version, based on SdlMuncher 0.12 
 * 0.02, 05-feb-2016: Image sequence
 * 0.03, 11-feb-2016: Included jump methods 
 * 0.04, 11-feb-2016: Move methods: MoveLeft, MoveRight, MoveDown and Jump
 * 0.05, 12-feb-2016: Jump and gravity
 * 0.06, 29-apr-2016: Jump on bubble
 * 0.07, 12-may-2016: Display dead animation when killed by enemy and revive time
 *                    to reappear, immunity when reappeared
 */
using System;

namespace DamGame
{
    class Player : Sprite
    {
        private int life;
        private int points;
        private int reviveTime;
        private int immuneTime;
        private bool gotX;
        private bool gotT;
        private bool gotN;
        private bool gotD;
        private int countE;

        public Player(Game g) : base(g)
        {
            LoadSequence(RIGHT ,new string[] { "data/playerRight1.png", "data/playerRight2.png" });
            LoadSequence(LEFT, new string[] { "data/playerLeft1.png", "data/playerLeft2.png" });
            LoadSequence(FIRELEFT, new string[] { "data/playerFireLeft.png", "data/playerLeft1.png" });
            LoadSequence(FIRERIGHT, new string[] { "data/playerFireRight.png", "data/playerRight1.png" });
            LoadSequence(DEAD, new string[] { "data/playerDead.png", "data/playerDead.png", "data/playerDead.png",
                "data/playerDead2.png", "data/playerDead2.png" , "data/playerDead2.png"});
            LoadSequence(APPEARINGRIGHT, new string[] { "data/transparent.png", "data/playerRight1.png", "data/playerRight2.png" });
            LoadSequence(APPEARINGLEFT, new string[] { "data/transparent.png", "data/playerLeft1.png", "data/playerLeft2.png" });

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
            stepsTillNextFrame = 6;
            reviveTime = 3;
            immuneTime = 3;
            isRight = true;
            isDead = false;
            immune = false;
            gotX = false;
            gotT = false;
            gotN = false;
            gotD = false;
            countE = 0;
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
            if (gotX && gotT && gotN && gotD && countE == 2)
            {
                gotX = false;
                gotT = false;
                gotN = false;
                gotD = false;
                countE = 0;
                LifeUp();
            }
                
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

        public void Jump(Shot shot)
        {
            if (y + height + ySpeed >= shot.GetY() && ((x >= shot.GetX() && x <= shot.GetX() + shot.GetWidth()) ||
                    (x + width >= shot.GetX() && x + width <= shot.GetX() + shot.GetWidth())))
                jumping = true;
            else if (jumping || myGame.IsValidMove(
                    x, y + height + ySpeed, x + width, y + height + ySpeed))
                return;
            jumping = true;
            jumpXspeed = 0;
        }

        public void Die()
        {
            startTime = DateTime.Now;
            isDead = true;
            ChangeDirection(DEAD);
        }

        public void DisplaySpecialFrame()
        {
            currentFrame++;
            if (currentFrame >= sequence[currentDirection].Length)
                currentFrame = 0;
        }

        public override void Restart()
        {
            if (isDead)
            {
                Timer();
                if (time == reviveTime)
                {
                    ChangeDirection(APPEARINGRIGHT);
                    immune = true;
                    startTime = DateTime.Now;
                    isDead = false;
                    x = startX;
                    y = startY;
                }
            }
            else
            {
                x = startX;
                y = startY;
            }
        }

        public bool ImmunityTimeUp()
        {
            Timer();
            if (time == immuneTime)
                return true;
            return false;
        }

        public void BackToNormal()
        {
            immune = false;
            if (currentDirection == APPEARINGLEFT
                    || currentDirection == FIRELEFT)
                ChangeDirection(LEFT);
            else
                ChangeDirection(RIGHT);
        }

        public bool IsImmune()
        {
            return immune;
        }

        public void GotE()
        {
            countE++;
        }

        public void GotX()
        {
            gotX = true;
        }

        public void GotT()
        {
            gotT = true;
        }

        public void GotN()
        {
            gotN = true;
        }

        public void GotD()
        {
            gotD = true;
        }
    }
}
