namespace DamGame
{
    class DamGame
    {
        static void Main(string[] args)
        {
            bool fullScreen = false;
            Hardware.Init(1024,768, 24, fullScreen);
            bool finished = false;

            while (!finished)
            {
                WelcomeScreen welcome = new WelcomeScreen();
                welcome.Run();

                if (welcome.GetOptionChosen() == WelcomeScreen.options.Credits)
                {
                    CreditsScreen credits = new CreditsScreen();
                    credits.Run();
                }

                if (welcome.GetOptionChosen() == WelcomeScreen.options.Play)
                {
                    Game myGame = new Game();
                    myGame.Run();

                    GameOverScreen gameEnd = new GameOverScreen();
                    gameEnd.Run();
                }

                if (welcome.GetOptionChosen() == WelcomeScreen.options.Quit)
                    finished = true;
            }
        }
    }
}
