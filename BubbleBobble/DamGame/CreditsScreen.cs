namespace DamGame
{
    class CreditsScreen
    {
        public void Run()
        {
            Font font18 = new Font("data/Joystix.ttf", 18);
            Image player = new Image("data/player.png");
            Image floor = new Image("data/tileBrick01.png");
            int playerX = 500;
            int playerY = 250;

            do
            {
                Hardware.ClearScreen();
                Hardware.WriteHiddenText("Programmed by Nacho",
                    400, 10,
                    0xCC, 0xCC, 0xCC,
                    font18);
                Hardware.WriteHiddenText("Press R to return...",
                    394, 50,
                    0x80, 0x80, 0x80,
                    font18);
                Hardware.DrawHiddenImage(player, playerX, playerY);
                for (int i=0; i<10; i++)
                    Hardware.DrawHiddenImage(floor, 100+i*80, 357);
                Hardware.ShowHiddenScreen();

                if (Hardware.KeyPressed(Hardware.KEY_LEFT))
                {
                    Hardware.ScrollHorizontally(5);
                    playerX -= 5;
                }
                if (Hardware.KeyPressed(Hardware.KEY_RIGHT))
                {
                    Hardware.ScrollHorizontally(-5);
                    playerX += 5;
                }

                Hardware.Pause(20);
            }
            while (!Hardware.KeyPressed(Hardware.KEY_R));
            Hardware.ResetScroll();
        }
    }
}
