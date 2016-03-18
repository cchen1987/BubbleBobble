namespace DamGame
{
    class LetterD : Item
    {
        public LetterD(Game g) : base(g)
        {
            LoadImage("data/D.png");
            width = 53;
            height = 49;
            timeToSpawn = 15;
            points = 300; 
        }
    }
}
