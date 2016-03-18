﻿/**
 * WelcomeScreen.cs - A basic graphic element to inherit from
 * 
 * Changes:
 * 0.01, 24-jul-2013: Initial version, based on SdlMuncher 0.12 
 * 0.02, 18-feb-2016: Welcome screen defined
 */

using System;

namespace DamGame
{
    class WelcomeScreen
    {
        public enum options { Play, Credits, Quit };
        private options optionChosen;
        private Game g; 

        public void Run()
        {
            Random r = new Random();
            Font font18 = new Font("data/Joystix.ttf", 18);
            Image image = new Image("data/welcome3.png");
            Image player1 = new Image("data/personaje.png");
            Image player2 = new Image("data/personaje2.png");
            Balls[] balls = new Balls[2];
            balls[0] = new Balls(g, r.Next(0, 902), r.Next(0, 680), 0);
            balls[1] = new Balls(g, r.Next(0, 902), r.Next(0, 680), 1);

            bool validOptionChosen = false;
           
            do
            {
                Hardware.ClearScreen();     
                Hardware.DrawHiddenImage(image, 0, 0);
                balls[0].Move();
                balls[0].DrawOnHiddenScreen();
                balls[1].Move();
                balls[1].DrawOnHiddenScreen(); 
                Hardware.DrawHiddenImage(player1, 50, 530);
                Hardware.DrawHiddenImage(player2, 730, 530);
                Hardware.WriteHiddenText("Press P to Play",
                    410, 550,
                    0xCC, 0xCC, 0xCC,
                    font18);
                Hardware.WriteHiddenText("Press Q to Quit",
                    410, 600,
                    0xCC, 0xCC, 0xCC,
                    font18);
                Hardware.WriteHiddenText("Press C to Quit",
                    410, 650,
                    0xCC, 0xCC, 0xCC,
                    font18);
                Hardware.ShowHiddenScreen();
            
                if (Hardware.KeyPressed(Hardware.KEY_P) ||
                    Hardware.KeyPressed(Hardware.KEY_SPC))
                {
                    validOptionChosen = true;
                    optionChosen = options.Play;
                }
                if (Hardware.KeyPressed(Hardware.KEY_Q))
                {
                    validOptionChosen = true;
                    optionChosen = options.Quit;
                }

                if (Hardware.KeyPressed(Hardware.KEY_C))
                {
                    validOptionChosen = true;
                    optionChosen = options.Credits; 
                }
                Hardware.Pause(50);
            }
            while (!validOptionChosen);
        }

        public options GetOptionChosen()
        {
            return optionChosen;
        }


    }
}
