namespace DamGame
{
    class Sweet : Item
    {
        public Sweet(Game g) : base(g)
        {
            LoadImage("data/sweet.png");
            width = 50;
            height = 34;
            xSpeed = 7;
            ySpeed = 7;
            timeToSpawn = 5;
            points = 1000;
        }    
    }
}
