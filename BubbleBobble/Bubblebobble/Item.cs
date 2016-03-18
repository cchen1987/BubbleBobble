/**
 * Item.cs - A basic graphic element to inherit from
 * 
 * Changes:
 * 0.01, 24-jul-2013: Initial version, based on SdlMuncher 0.12
 * 0.02, 11-feb-2016: Included jump methods, and define class item
 * 0.03, 24-feb-2016: Defined points for each item
 */

namespace DamGame
{
    class Item : Sprite
    {
        protected int timeToSpawn;
        protected int points;

        public Item(Game g) : base(g)
        {
            myGame = g;
            points = 0;   
        }

        public int GetTimeSpawn()
        {
            return timeToSpawn;
        } 
        
        public int GetItemPoints()
        {
            return points;
        }    
    }
}
