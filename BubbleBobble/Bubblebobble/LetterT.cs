namespace DamGame
{
    class LetterT : Item
    {
        public LetterT(Game g) : base(g)
        {
            LoadImage("data/T.png");
            width = 53;
            height = 49;
            timeToSpawn = 10;
            points = 300;
        }
    }
}