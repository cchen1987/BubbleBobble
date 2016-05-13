/**
 * Sprite.cs - A basic graphic element to inherit from
 * 
 * Changes:
 * 0.01, 24-jul-2013: Initial version, based on SdlMuncher 0.12 
 * 0.02, 05-feb-2016: Image sequence 
 * 0.03, 18-feb-2016: Jump methods and gravity
 */

using System;
namespace DamGame
{
    class Sprite
    {
        protected int x, y;
        protected int startX, startY;
        protected int width, height;
        protected int xSpeed, ySpeed;
        protected bool visible;
        protected Image image;
        protected Image[][] sequence;
        protected bool containsSequence;
        protected int currentFrame;
        protected Game myGame;
        protected bool jumping;
        protected bool falling; 
        protected bool isDead;
        protected bool isRight;
        protected bool immune;
        protected int jumpXspeed;
        protected int jumpFrame;
        protected int[] jumpSteps =
        {
            -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7,
            7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7
        };
        protected int time;
        protected DateTime startTime;
        protected DateTime currentTime;
        protected int stepsTillNextFrame;
        protected int currentStep;  
        protected int rangeX;
        protected int rangeY;
        
        protected byte numDirections = 15;
        protected byte currentDirection;
        public const byte RIGHT = 0;
        public const byte LEFT = 1;
        public const byte DOWN = 2;
        public const byte UP = 3;
        public const byte DOWNRIGHT = 4;
        public const byte DOWNLEFT = 5;
        public const byte UPRIGHT = 6;
        public const byte UPLEFT = 7;
        public const byte APPEARINGRIGHT = 8;
        public const byte APPEARINGLEFT = 9;
        public const byte JUMPING = 10;
        public const byte FIRELEFT = 11;
        public const byte FIRERIGHT = 12;
        public const byte DEAD = 13;
        public const byte MOVING = 14;
                                       
        public Sprite(Game g)
        {
            startX = -1;
            startY = -1;
            width = 32;
            height = 32;
            visible = true;
            sequence = new Image[numDirections][];
            currentDirection = RIGHT;

            stepsTillNextFrame = 1;
            currentStep = 0;
            jumpXspeed = 0;
            jumping = false;
            jumpFrame = 0;
            falling = false;
            myGame = g;
            isRight = true;
            immune = false;
        }  

        public Sprite(string imageName, Game g)
            : this(g)
        {
            LoadImage(imageName);
        }

        public Sprite(string[] imageNames, Game g)
            : this(g)
        {
            LoadSequence(imageNames);
        }

        public void LoadImage(string name)
        {
            image = new Image(name);
            containsSequence = false;
        }

        public virtual void LoadSequence(byte direction, string[] names)
        {
            int amountOfFrames = names.Length;
            sequence[direction] = new Image[amountOfFrames];
            for (int i = 0; i < amountOfFrames; i++)
                sequence[direction][i] = new Image(names[i]);
            containsSequence = true;
            currentFrame = 0;
        }

        public void LoadSequence(string[] names)
        {
            LoadSequence(RIGHT, names);
        }

        public int GetX()
        {
            return x;
        }

        public void SetX(int x)
        {
            this.x = x;
        }

        public int GetY()
        {
            return y;
        }

        public void SetY(int y)
        {
            this.y = y;
        }

        public int GetWidth()
        {
            return width;
        }

        public int GetHeight()
        {
            return height;
        }

        public int GetSpeedX()
        {
            return xSpeed;
        }

        public int GetSpeedY()
        {
            return ySpeed;
        }          
              
        public bool IsVisible()
        {
            return visible;
        }

        public void MoveTo(int newX, int newY)
        {
            x = newX;
            y = newY;
            if (startX == -1)
            {
                startX = x;
                startY = y;
            }
        }

        public bool IsMovingRight()
        {
            return isRight;
        }

        public void MoveRight()
        {
            isRight = true;

            if (jumping)
                JumpRight();

            else if (myGame.IsValidMove(x + xSpeed, y + height, x + width + xSpeed, y + height))
            {
                x += xSpeed;
                if (!immune)
                    ChangeDirection(RIGHT);
                else
                    ChangeDirection(APPEARINGRIGHT);
                NextFrame();
            }
        }

        public void MoveLeft()
        {
            isRight = false;

            if (jumping)
                JumpLeft();

            else if (myGame.IsValidMove(x - xSpeed, y + height, x + width - xSpeed, y + height))
            {
                x -= xSpeed;
                if (!immune)
                    ChangeDirection(LEFT);
                else
                    ChangeDirection(APPEARINGLEFT);
                NextFrame();
            }
        }

        public void Jump()
        {
            if (jumping || falling || myGame.IsValidMove(
                    x, y + height + ySpeed, x + width, y + height + ySpeed))
                return;
            jumping = true;
            jumpXspeed = 0;
        }

        public void JumpRight()
        {
            isRight = true;
            jumpXspeed = xSpeed;
            if (!immune)
                ChangeDirection(RIGHT);
            else
                ChangeDirection(APPEARINGRIGHT);
            Jump(); 
        }

        public void JumpLeft()
        {
            isRight = false;  
            jumpXspeed = -xSpeed;
            if (!immune)
                ChangeDirection(LEFT);
            else
                ChangeDirection(APPEARINGLEFT);
            Jump();
        }

        public virtual void Move()
        {
            if (!jumping)
            {
                if (myGame.IsValidMove(
                        x, y + height + ySpeed, x + width, y + height + ySpeed))
                {
                    y += ySpeed;
                }
            }
            else
            {
                currentFrame = 0;

                short nextX = (short)(x + jumpXspeed);
                short nextY = (short)(y + jumpSteps[jumpFrame]);

                if (myGame.IsValidMove(
                        nextX, nextY + height - ySpeed,
                        nextX + width, nextY + height, jumping))
                {
                    x = nextX;
                    y = nextY;
                }
                else
                {
                    jumping = false;
                    jumpFrame = 0;
                }

                jumpFrame++;
                if (jumpSteps[jumpFrame] > 0 && myGame.IsValidMove(
                        nextX, nextY + height - ySpeed,
                        nextX + width, nextY + height))
                {
                    jumping = false;
                    jumpFrame = 0;
                }
            }
        }
                
        public void Fire()
        {     
            if (currentDirection == LEFT ||
                    currentDirection == APPEARINGLEFT) 
                ChangeDirection(FIRELEFT); 
            else if (currentDirection == RIGHT ||
                    currentDirection == APPEARINGRIGHT) 
                ChangeDirection(FIRERIGHT);
            NextFrame();
        } 

        public void SetSpeed(int newXSpeed, int newYSpeed)
        {
            xSpeed = newXSpeed;
            ySpeed = newYSpeed;
        }

        public void Show()
        {
            visible = true;
        }

        public void Hide()
        {
            visible = false;
        } 

        public virtual void DrawOnHiddenScreen()
        {
            if (!visible)
                return;

            if (containsSequence)
                Hardware.DrawHiddenImage(
                    sequence[currentDirection][currentFrame], x, y);
            else
                Hardware.DrawHiddenImage(image, x, y);
        }

        public virtual void NextFrame()
        {
            currentStep++;
            if (currentStep >= stepsTillNextFrame)
            {
                currentStep = 0;
                currentFrame++;
                if (currentFrame >= sequence[currentDirection].Length)
                    currentFrame = 0; 
            }
        }

        public bool Dead()
        {
            return isDead;
        }

        public void ChangeDirection(byte newDirection)
        {
            if (!containsSequence) return;
            if (currentDirection != newDirection)
            {
                currentDirection = newDirection;
                currentFrame = 0;
            }

        }

        public bool CollisionsWith(Sprite otherSprite)
        {
            return (visible && otherSprite.IsVisible() &&
                CollisionsWith(otherSprite.GetX(),
                    otherSprite.GetY(),
                    otherSprite.GetX() + otherSprite.GetWidth(),
                    otherSprite.GetY() + otherSprite.GetHeight()));
        }

        public bool CollisionsWith(int xStart, int yStart, int xEnd, int yEnd)
        {
            if (visible &&
                    (x < xEnd) &&
                    (x + width > xStart) &&
                    (y < yEnd) &&
                    (y + height > yStart)
                    )
                return true;
            return false;
        }

        public static bool CheckCollisions(
            int r1xStart, int r1yStart, int r1xEnd, int r1yEnd,
            int r2xStart, int r2yStart, int r2xEnd, int r2yEnd)
        {
            if ((r2xStart < r1xEnd) &&
                    (r2xEnd > r1xStart) &&
                    (r2yStart < r1yEnd) &&
                    (r2yEnd > r1yStart)
                    )
                return true;
            else
                return false;
        }

        public void Timer()
        {
            currentTime = DateTime.Now;
            TimeSpan duration = currentTime - startTime;
            time = duration.Seconds;
        }

        public virtual void Restart()
        {
            isDead = false;
            x = startX;
            y = startY;
        }    
    }
}