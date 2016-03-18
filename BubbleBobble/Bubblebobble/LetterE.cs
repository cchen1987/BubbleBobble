namespace DamGame
{
    class LetterE : Item
    {
        public LetterE(Game g) : base(g)
        {
            LoadImage("data/E.png");
            width = 53;
            height = 49;
            timeToSpawn = 20;
            points = 300;
        }
    }
}