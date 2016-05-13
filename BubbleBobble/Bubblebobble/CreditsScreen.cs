namespace DamGame
{
    class CreditsScreen
    {
        public void Run()
        {
            Font font18 = new Font("data/Joystix.ttf", 18);
            Image player = new Image("data/playerRight1.png");
            Image floor = new Image("data/Brick2.png");
            int playerX = 500;
            int playerY = 250;
            Image text = Hardware.CreateImageFromText("Press R to return...",
                    0x80, 0x80, 0x80,
                    font18);
            Image programmer = Hardware.CreateImageFromText("Programmed by Chen",
                    0xCC, 0xCC, 0xCC,
                    font18);
            do
            {
                Hardware.ClearScreen();
                Hardware.DrawHiddenImage(programmer, 400, 10);
                Hardware.DrawHiddenImage(text, 394, 50);
                Hardware.DrawHiddenImage(player, playerX, playerY);
                for (int i = 0; i < 30; i++)
                     Hardware.DrawHiddenImage(floor, 100 + i * 28, 357);
                Hardware.ShowHiddenScreen();
               
                if (Hardware.KeyPressed(Hardware.KEY_DOWN))
                {
                     Hardware.ScrollVertically(5);
                     playerY -= 5;
                }
                if (Hardware.KeyPressed(Hardware.KEY_UP))
                {
                     Hardware.ScrollVertically(-5);
                     playerY += 5;
                }
               
                Hardware.Pause(20);
            }
            while (!Hardware.KeyPressed(Hardware.KEY_R)) ;
            Hardware.ResetScroll();
        }
    }
}
