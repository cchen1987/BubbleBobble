/**
 * Boots.cs - A basic graphic element to inherit from
 * 
 * Changes: 
 * 0.01, 18-feb-2016: Class boots
 */

namespace DamGame
{
    class Boots : Item
    {
        public Boots(Game g) : base(g)
        {
            LoadImage("data/boots.png");
            width = 54;
            height = 27;
            points = 500;
        }
    }
}
