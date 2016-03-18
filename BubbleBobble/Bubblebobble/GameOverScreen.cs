/**
 * GameOverScreen.cs - A basic graphic element to inherit from
 * 
 * Changes:
 * 0.01, 24-jul-2013: Initial version, based on SdlMuncher 0.12 
 * 0.02, 18-feb-2016: Game over screen defined
 */
namespace DamGame
{
    class GameOverScreen
    {
        public void Run()
        {
            Font font18 = new Font("data/Joystix.ttf", 18);
            Font font40 = new Font("data/Joystix.ttf", 40);
            do
            {
                Hardware.ClearScreen();
                Hardware.WriteHiddenText("GAME OVER",
                    380, 320,
                    0xFF, 0xAD, 0x8B,
                    font40);
                Hardware.WriteHiddenText("Press Q to continue...",
                    370, 450,
                    0xCC, 0xCC, 0xCC,
                    font18);                            
                Hardware.ShowHiddenScreen();

                Hardware.Pause(50);
            }
            while (!Hardware.KeyPressed(Hardware.KEY_Q) );
        }
    }
}
