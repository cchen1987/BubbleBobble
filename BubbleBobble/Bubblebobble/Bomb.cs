namespace DamGame
{
    class Bomb : Item
    {
        public Bomb(Game g) : base(g)
        {
            LoadImage("data/dynamite.png");
            width = 53;
            height = 49;
            points = 200;
        }
    }
}