namespace DamGame
{
    class LetterX : Item
    {
        public LetterX(Game g) : base(g)
        {
            LoadImage("data/X.png");
            width = 53;
            height = 49;
            timeToSpawn = 20;
            points = 300;
        }
    }
}