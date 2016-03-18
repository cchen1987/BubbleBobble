/**
 * Game.cs - A basic graphic element to inherit from
 * 
 * Changes:
 * 0.01, 24-jul-2013: Initial version, based on SdlMuncher 0.12 
 * 0.02, 11-feb-2016: Draw score, number of level, life and time on screen
 * 0.03, 11-feb-2016: Collision with Items
 * 0.04, 19-feb-2016: Shot activation and collision with enemy
 * 0.05, 24-feb-2016: Corrected player collision with shot, destroy shot after collision
 */

using System;
using System.Collections;
using System.Collections.Generic;

namespace DamGame
{
    class Game
    {
        private Font font18;
        private Player player;
        private Enemy[] enemies;
        private Item[] items;
        private Level currentLevel;
        private int numEnemies;
        private int level;
        private int time;
        private int currentFrame;
        private bool finished;
        private DateTime startTime;
        private DateTime currentTime;
        private Image lifeImage;
        private Image explosion;
        private ArrayList lifeX;
        private List<Shot> shots;

        public Game()
        {
            font18 = new Font("data/Joystix.ttf", 18);
            items = new Item[6];
            items[0] = new Sweet(this);
            items[0].SetX(650);
            items[0].SetY(268);
            // items[1] = new LetterE(this);
            // items[2] = new LetterX(this);
            // items[3] = new LetterT(this);
            // items[4] = new LetterE(this);
            // items[5] = new LetterN(this);
            // items[6] = new LetterD(this);
            
            lifeX = new ArrayList(); 
            player = new Player(this);
            shots = new List<Shot>();        
            lifeImage = new Image("data/life2.png");
            explosion = new Image("data/explosion.png");
            level = 0;
            time = 0;
            numEnemies = 5;
            currentFrame = 0;
            Random rnd = new Random(); 
            
            enemies = new Bonzo[numEnemies];
            for (int i = 0; i < numEnemies; i++)
            {
                enemies[i] = new Bonzo(rnd.Next(200, 800), rnd.Next(50, 600), this);
                enemies[i].SetSpeed(rnd.Next(1, 7), 2);
            }

            int width = 33; 
            for (int i = 0; i < player.GetLife(); i++)
            {                       
                lifeX.Add(700 + width);
                width += 33;
            }
                
            startTime = DateTime.Now;
            currentLevel = new Level();
            finished = false;
        }

        public Player GetPlayer()
        {
            return player;
        } 

        // Update screen
        public void DrawElements()
        {
            Hardware.ClearScreen();

            currentLevel.DrawOnHiddenScreen();
            Hardware.WriteHiddenText("Score: ",
                80, 10,
                0xCC, 0xCC, 0xCC,
                font18);
            Hardware.WriteHiddenText(player.GetPoints().ToString(),
                180, 10,
                0xCC, 0xCC, 0xCC,
                font18);
            Hardware.WriteHiddenText((level + 1).ToString("00"),
                90, 50,
                0xCC, 0xCC, 0xCC,
                font18);
            Hardware.WriteHiddenText(time.ToString("00"),
                950, 10,
                0xCC, 0xCC, 0xCC,
                font18);
            Hardware.WriteHiddenText("LIFE: ",
                650, 10,
                0xCC, 0xCC, 0xCC,
                font18);  
            for (int i = 0; i < player.GetLife(); i++)        
                Hardware.DrawHiddenImage(lifeImage, (int)lifeX[i], 10);
            for (int i = 0; i < shots.Count; i++)
            {
                shots[i].DrawOnHiddenScreen();
                Hardware.WriteHiddenText(shots.Count.ToString(),
                10, 10,
                0xCC, 0xCC, 0xCC,
                font18);
            }

            items[0].DrawOnHiddenScreen();
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
                else if (Hardware.KeyPressed(Hardware.KEY_LEFT))
                    player.JumpLeft();
                else
                    player.Jump();
                    /*for (int i = 0; i < shots.Count; i++)
                    {
                        player.JumpOnBubble(shots[i]);
                    }*/ 
            }

            else if (Hardware.KeyPressed(Hardware.KEY_RIGHT))
                player.MoveRight();

            else if (Hardware.KeyPressed(Hardware.KEY_LEFT))
                player.MoveLeft();
             
            if (Hardware.KeyPressed(Hardware.KEY_SPC))
            {
                player.Fire();
                currentFrame++;
                if (currentFrame == 15)  // In Spanish: contador de disparos, tiempo par se obtiene 1 disparo, no acumulable.
                {
                    shots.Add(new Shot(player, this));
                    shots[shots.Count - 1].Activate();  
                    currentFrame = 0;
                }
            }   
                  
            if (Hardware.KeyPressed(Hardware.KEY_ESC))
                finished = true;
        }


        // Move enemies, animate background, etc 
        public void MoveElements()
        {             
            player.Move();       
            for (int i = 0; i < numEnemies; i++)
                enemies[i].Move();
            for (int i = 0; i < shots.Count; i++)
            {                          
                if (shots[i].IsVisible())
                {                 
                    shots[i].Move();
                    shots[i].Timer();
                }
            }
        }

        // Check if player is still has life points
        public void CheckLife()
        {
            if (player.GetLife() > lifeX.Count) 
                lifeX.Add((int)lifeX[lifeX.Count - 1] + 33);

            if (player.GetLife() == 0)   
                finished = true;               
        }  

        // Place randomly items on the map
        /*public void PlaceItem()
        {
            Random r = new Random();
            ArrayList validPositions = currentLevel.GetValidPositions(items[0]);

            int randomPosition = r.Next(0, currentLevel.GetValidPositions(items[0]).Count);
            string positionsXAndY = (string) validPositions[randomPosition];
            string[] positions = positionsXAndY.Split(' ');
            short positionX = Convert.ToInt16(positions[1]);
            short positionY = Convert.ToInt16(positions[0]);
            if (time == 20)
            {
                items[0].SetX(positionX);
                items[0].SetY(positionY);
                items[0].DrawOnHiddenScreen();
                Hardware.ShowHiddenScreen();
            }   
        }    */

        // Check collisions and apply game logic
        public void CheckCollisions()
        {
            if (player.CollisionsWith(items[0]))
            {
                // player.SetSpeed(player.GetSpeedX() + items[0].GetSpeedX(), // Picking items
                //     player.GetSpeedY() + items[0].GetSpeedY());
                items[0].Hide();
                player.SetPoints(player.GetPoints() + items[0].GetItemPoints());
                /*Hardware.WriteHiddenText(items[0].GetItemPoints().ToString(),
                (short)items[0].GetX(), (short)(items[0].GetY() - 100),
                0xCC, 0xCC, 0xCC,
                font18);*/
            }
              
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].CollisionsWith(player) && ! player.Dead())
                {
                    player.Die();
                    player.SubtLife();
                    player.Restart();
                }

                if (enemies[i].IsInBubble())
                {
                    enemies[i].GetOutOfBubble();
                }

                for (int j = 0; j < shots.Count; j++)
                {
                    if (shots[j].CollisionsWith(enemies[i]) && shots[j].GetSpeedX() != 0 && !shots[j].GotEnemy()) // Need to be improved TO DO
                    {
                        enemies[i].Hide();
                        enemies[i].InBubble(shots[j]);
                        shots[j].GotEnemyIn(enemies[i]); 
                    }
                        
                    if (player.CollisionsWith(shots[j]) && !shots[j].IsExploding())
                    {
                        shots[j].Hide();
                        player.SetPoints(player.GetPoints() + 10);
                    }

                    if (!shots[j].IsVisible())                                
                        shots.Remove(shots[j]);
                }  
            }
        }
        
        public bool IsValidMove(int xMin, int yMin, int xMax, int yMax, bool jump)
        {
            return currentLevel.IsValidMove(xMin, yMin, xMax, yMax, jump);
        }

        public bool IsValidMove(int xMin, int yMin, int xMax, int yMax)
        {
            return currentLevel.IsValidMove(xMin, yMin, xMax, yMax);
        }

        public void Timer()
        {
            currentTime = DateTime.Now;
            TimeSpan duration = currentTime - startTime;
            time = duration.Seconds;   
        }

        public int GetCurrentTime()
        {
            return time;
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
                Timer();
                CheckLife();
                DrawElements();
                CheckKeys();
                MoveElements();
                CheckCollisions();
                PauseTillNextFrame();
            }
        }
    }
}
