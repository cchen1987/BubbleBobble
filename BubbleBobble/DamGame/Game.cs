using System;

namespace DamGame
{
    class Game
    {
        private Font font18;
        private Player player;
        private Enemy[] enemies;
        private Level currentLevel;
        private bool finished;
        private int numEnemies;

        public Game()
        {
            font18 = new Font("data/Joystix.ttf", 18);
            player = new Player(this);

            Random rnd = new Random();
            numEnemies = 2;
            enemies = new Enemy[numEnemies];
            for (int i = 0; i < numEnemies; i++)
            {
                enemies[i] = new Enemy(rnd.Next(200, 800), rnd.Next(50, 600));
                enemies[i].SetSpeed(rnd.Next(1, 5), 0);
            }

            currentLevel = new Level();
            finished = false;
        }


        // Update screen
        public void DrawElements()
        {
            Hardware.ClearScreen();

            currentLevel.DrawOnHiddenScreen();
            Hardware.WriteHiddenText("Score: ",
                40, 10,
                0xCC, 0xCC, 0xCC,
                font18);
            player.DrawOnHiddenScreen();
            for (int i = 0; i < numEnemies; i++)
                enemies[i].DrawOnHiddenScreen();
            Hardware.ShowHiddenScreen();
        }


        // Check input by the user
        public void CheckKeys()
        {
            if (Hardware.KeyPressed(Hardware.KEY_UP))
            {
                if (Hardware.KeyPressed(Hardware.KEY_RIGHT))
                    player.JumpRight();
                else
                if (Hardware.KeyPressed(Hardware.KEY_LEFT))
                    player.JumpLeft();
                else
                    player.Jump();
            }

            else if (Hardware.KeyPressed(Hardware.KEY_RIGHT))
                player.MoveRight();

            else if (Hardware.KeyPressed(Hardware.KEY_LEFT))
                player.MoveLeft();

            //if (Hardware.KeyPressed(Hardware.KEY_DOWN))
            //    player.MoveDown();

            if (Hardware.KeyPressed(Hardware.KEY_ESC))
                finished = true;
        }


        // Move enemies, animate background, etc 
        public void MoveElements()
        {
            player.Move();
            for (int i = 0; i < numEnemies; i++)
                enemies[i].Move();
        }


        // Check collisions and apply game logic
        public void CheckCollisions()
        {
            for (int i = 0; i < numEnemies; i++)
                if (enemies[i].CollisionsWith(player))
                    finished = true;
        }

        public void PauseTillNextFrame()
        {
            // Pause till next frame (20 ms = 50 fps)
            Hardware.Pause(20);
        }

        public void Run()
        {
            // Game Loop
            while (!finished)
            {
                DrawElements();
                CheckKeys();
                MoveElements();
                CheckCollisions();
                PauseTillNextFrame();
            }
        }

        public bool IsValidMove(int xMin, int yMin, int xMax, int yMax)
        {
            return currentLevel.IsValidMove(xMin, yMin, xMax, yMax);
        }
    }
}
