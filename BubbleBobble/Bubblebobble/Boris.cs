namespace DamGame
{
    class Boris : Enemy
    {
        public Boris(int newX, int newY, Game g) : base(newX, newY, g)
        {
            LoadSequence(LEFT, new string[] { "data/enemy2Left1.png", "data/enemy2Left2.png" });
            LoadSequence(RIGHT, new string[] { "data/enemy2Right1.png", "data/enemy2Right2.png" });
            ChangeDirection(RIGHT);
            x = newX;
            y = newY;
            xSpeed = 6;
            ySpeed = 6;
            width = 48;
            height = 44;
            stepsTillNextFrame = 5;
            currentStep = 0;
        }
    }
}