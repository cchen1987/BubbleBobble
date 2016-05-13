namespace DamGame
{
    class Bonzo : Enemy
    {
        public Bonzo(int newX, int newY, Game g) : base(newX, newY, g)
        {
            LoadSequence(LEFT, new string[] { "data/enemyLeft1.png", "data/enemyLeft2.png" });
            LoadSequence(RIGHT, new string[] { "data/enemyRight1.png", "data/enemyRight2.png" });
            ChangeDirection(RIGHT);
            x = newX;
            y = newY;
            xSpeed = 4;
            ySpeed = 5;
            width = 48;
            height = 48;
            stepsTillNextFrame = 5;
            currentStep = 0;
            rangeX = 400;
            rangeY = 60;
        }             
    }
}
