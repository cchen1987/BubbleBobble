namespace DamGame
{
    class RedUmbrella : Item
    {
        public RedUmbrella(Game g) : base(g)
        {
            LoadImage("data/umbrella.png");
            width = 50;
            height = 41;
            points = 400;
        }
    }
}
