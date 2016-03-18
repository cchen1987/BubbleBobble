/**
 * WaterBall.cs - A basic graphic element to inherit from
 * 
 * Changes:
 * 0.01, 24-feb-2016: Initial version of Water Ball 
 */

namespace DamGame
{
    class WaterBall : Sprite
    {
        private bool activated;

        public WaterBall(Game g) : base(g)
        {
            LoadImage("data/waterBubble.png");
            width = 56;
            height = 52;
            xSpeed = 4;
            ySpeed = 4;
            activated = false;
        }

        public override void Move()
        {
            if (!activated)
            {
                // TO DO
            }
            else
            {
                // TO DO
            }
        }

        public void Activate()
        {
            activated = true;
        }
    }
}
