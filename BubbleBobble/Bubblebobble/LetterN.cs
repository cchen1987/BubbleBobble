namespace DamGame
{
    class LetterN : Item
    {
        public LetterN(Game g) : base(g)
        {
            LoadImage("data/N.png");
            width = 53;
            height = 49;
            timeToSpawn = 25;
            points = 300;
        }
    }
}