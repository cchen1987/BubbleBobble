namespace DamGame
{
    class Player : Sprite
    {
        protected Game myGame;
        protected bool jumping, falling;
        protected int jumpXspeed;
        protected int jumpFrame;
        protected int[] jumpSteps = 
        {
            -10, -10, -8, -8, -6, -6, -4, -2, -1, -1, 0,
            0, 1, 1, 2, 4, 6, 6, 8, 8, 10, 10
        };

        public Player(Game g)
        {
            LoadSequence(LEFT,
                new string[] { "data/player.png", "data/player2.png" });
            LoadSequence(RIGHT,
                new string[] { "data/player.png", "data/player2.png" });
            ChangeDirection(LEFT);

            x = 50;
            y = 120;
            xSpeed = 4;
            ySpeed = 4;
            width = 50;
            height = 107;

            stepsTillNextFrame = 6;

            jumpXspeed = 0;
            jumping = false;
            jumpFrame = 0;
            falling = false;
            myGame = g;
        }

        public void MoveRight()
        {
            if (myGame.IsValidMove(x + xSpeed, y, x + width + xSpeed, y + height))
            {
                x += xSpeed;
                ChangeDirection(RIGHT);
                NextFrame();
            }
        }

        public void MoveLeft()
        {
            if (myGame.IsValidMove(x - xSpeed, y, x + width - xSpeed, y + height))
            {
                x -= xSpeed;
                ChangeDirection(LEFT);
                NextFrame();
            }
        }

        public void MoveUp()
        {
            // The player will not move up freely any longer, but jump
            // y -= ySpeed;
            Jump();
            // TO DO: Check if there is a stair or up down
        }

        public void MoveDown()
        {
            // The player will not move down freely any longer
            // y += ySpeed;
            // TO DO: Check if there is a stair or way down
        }


        // Starts the jump sequence
        public void Jump()
        {
            if (jumping || falling)
                return;
            jumping = true;
            jumpXspeed = 0;
        }


        // Starts the jump sequence to the right
        public void JumpRight()
        {
            Jump();
            jumpXspeed = xSpeed;
        }


        // Starts the jump sequence to the left
        public void JumpLeft()
        {
            Jump();
            jumpXspeed = -xSpeed;
        }


        // Sometimes the player must be animated, eg. jumping
        public override void Move()
        {
            // If the player is not jumping, it might need to fall down
            if (!jumping)
            {
                if (myGame.IsValidMove(
                    x, y + ySpeed, x + width, y + height + ySpeed))
                {
                    y += ySpeed;
                }
            }
            else
            // If jumping, it must go on with the sequence
            {
                currentFrame = 0; // static frame for the jump at this moment

                // Let's calculate the next positions
                short nextX = (short)(x + jumpXspeed);
                short nextY = (short)(y + jumpSteps[jumpFrame]);

                // If the player can still move, let's do it
                if (myGame.IsValidMove(
                    nextX, nextY + height - ySpeed,
                    nextX + width, nextY + height))
                {
                    x = nextX;
                    y = nextY;
                    // NextFrame();
                }
                // If it cannot move, then it must fall
                else
                {
                    jumping = false;
                    jumpFrame = 0;
                }

                // And let's prepare the next frame, maybe with a different speed
                jumpFrame++;
                if (jumpFrame >= jumpSteps.Length)
                {
                    jumping = false;
                    jumpFrame = 0;
                }
            }
        }
    }
}
