namespace DamGame
{
    class Clock : Item
    {
        public Clock(Game g) : base(g)
        {
            LoadImage("data/clock.png");
            width = 54;
            height = 54;
            timeToSpawn = 20;
        }
    }
}
