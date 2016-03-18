namespace DamGame
{
    class GameOverScreen
    {
        public void Run()
        {
            Font font18 = new Font("data/Joystix.ttf", 18);
            Image player = new Image("data/player.png");

            do
            {
                Hardware.ClearScreen();
                Hardware.WriteHiddenText("Bye! Press Q to return to Operating System...",
                    40, 10,
                    0xCC, 0xCC, 0xCC,
                    font18);
                Hardware.DrawHiddenImage(player, 400, 300);
                Hardware.ShowHiddenScreen();

                Hardware.Pause(50);
            }
            while (!Hardware.KeyPressed(Hardware.KEY_Q) );
        }
    }
}
