namespace DamGame
{
    class Cake : Item
    {
        public Cake(Game g) : base (g)
        {
            LoadImage("data/cake.png");
            width = 50;
            height = 41;
            points = 500;
        }
    }
}
