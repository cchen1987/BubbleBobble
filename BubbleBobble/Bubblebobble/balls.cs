using System;

namespace DamGame
{
    class Balls : Sprite
    {
        public Balls(Game g, int x, int y, byte option) : base (g)
        {                          
            this.x = x;
            this.y = y;
            xSpeed = 10;
            ySpeed = 10;
            width = 122;
            height = 88;
            if (option == 0)
                LoadImage("data/bolas.png");
            if (option == 1)
                LoadImage("data/bolas2.png");
        }                

        public override void Move()
        {
            if (x < 0 || x > 1024 - width)
                xSpeed = -xSpeed;
            if (y < 0 || y > 768 - height)
                ySpeed = -ySpeed;

            x += xSpeed;
            y += ySpeed;
        }
    }
}
