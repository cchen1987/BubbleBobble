namespace DamGame
{
    class BaronVonBlubba : Enemy
    {
        public BaronVonBlubba(int newX, int newY, Game g) : base(newX, newY, g)
        {
            LoadSequence(LEFT, new string[] { "data/ghost.png" });
            LoadSequence(RIGHT, new string[] { "data/ghostRight.png" });
            ChangeDirection(RIGHT);
            x = newX;
            y = newY;
            xSpeed = 6;
            ySpeed = 6;
            width = 54;
            height = 44; 
        }

        public override void Move()
        {
            if (x < 134 || x > 917 - width)
                xSpeed = -xSpeed;
            if (y < 71 || y > 722 - height)
                ySpeed = -ySpeed;

            x += xSpeed;
            y += ySpeed;

            if (xSpeed < 0)
                ChangeDirection(LEFT);
            else
                ChangeDirection(RIGHT);

            NextFrame();
        }
    }
}
