/**
 * Game.cs - A basic graphic element to inherit from
 * 
 * Changes:
 * 0.01, 24-jul-2013: Initial version, based on SdlMuncher 0.12 
 * 0.02, 11-feb-2016: Draw score, number of level, life and time on screen
 * 0.03, 11-feb-2016: Collision with Items
 * 0.04, 19-feb-2016: Shot activation and collision with enemy
 * 0.05, 24-feb-2016: Corrected player collision with shot, destroy shot after collision
 * 0.06, 13-may-2016: Place randomly an item
 * 0.07, 13-may-2016: Scroll screen when all enemies dead, cheat, if Z key pressed, change to next level
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
        private List<Enemy> enemies;
        private Item[] items;
        private Level currentLevel; 
        private int level;
        private int time;
        private int currentFrame;
        private bool finished;
        private DateTime startTime;
        private DateTime currentTime;
        private Image lifeImage;
        private ArrayList lifeX;
        private List<Shot> shots;
        private bool itemPlaced;
        private bool timeUp;
        private bool isScrolling;
        private short scrollSpeed;
        private short currentScroll;
        private int baronCount;

        public Game()
        {
            font18 = new Font("data/Joystix.ttf", 18);

            // Create items
            itemPlaced = false;
            items = new Item[1];
            items[0] = new Sweet(this);
            //items[0].SetX(650);
            //items[0].SetY(268);
            // items[1] = new LetterE(this);
            // items[2] = new LetterX(this);
            // items[3] = new LetterT(this);
            // items[4] = new LetterE(this);
            // items[5] = new LetterN(this);
            // items[6] = new LetterD(this);
             
            // Create player
            player = new Player(this);

            // Create shots
            shots = new List<Shot>();
            lifeImage = new Image("data/life2.png");
            
            time = 0;
            currentFrame = 0;

            // Load level
            level = 0;
            currentLevel = new Level();
            currentLevel.ChangeLevel(level);

            // Create enemies
            enemies = new List<Enemy>();
            for (int i = 0; i < currentLevel.GetEnemyCount(); i++)
                enemies.Add(new Bonzo(currentLevel.GetEnemyX()[i],
                    currentLevel.GetEnemyY()[i], this));
            baronCount = 0;

            // Calculate life image coords
            int width = 33;
            lifeX = new ArrayList();
            for (int i = 0; i < player.GetLife(); i++)
            {
                lifeX.Add(700 + width);
                width += 33;
            }

            // Starting time
            startTime = DateTime.Now;
            timeUp = false;

            // Scroll
            isScrolling = false;
            scrollSpeed = -5;
            currentScroll = currentLevel.GetLevelHeight();

            // Game status
            finished = false;
        }
              
        // Update screen
        public void DrawElements()
        {
            Hardware.ClearScreen();
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
                                 
            // Draw level on screen
            currentLevel.DrawOnHiddenScreen();

            // Draw player life on screen
            for (int i = 0; i < player.GetLife(); i++)
                Hardware.DrawHiddenImage(lifeImage, (int)lifeX[i], 10);

            // Draw shots on screen
            for (int i = 0; i < shots.Count; i++)
                shots[i].DrawOnHiddenScreen();

            // Draw items on screen
            PlaceItem();
            if (itemPlaced)
                for (int i = 0; i < items.Length; i++)
                    items[i].DrawOnHiddenScreen();

            // Draw player on screen
            if (player.Dead() || player.IsImmune())
                player.DisplaySpecialFrame();
            player.DrawOnHiddenScreen();
            
            // Draw enemies on screen
            for (int i = 0; i < enemies.Count; i++)
                enemies[i].DrawOnHiddenScreen();

            Hardware.ShowHiddenScreen();
        }


        // Check input by the user
        public void CheckKeys()
        {
            if (Hardware.KeyPressed(Hardware.KEY_UP) && !player.Dead())
            {
                if (Hardware.KeyPressed(Hardware.KEY_RIGHT))
                    player.JumpRight();
                else if (Hardware.KeyPressed(Hardware.KEY_LEFT))
                    player.JumpLeft();
                else
                    for (int i = 0; i < shots.Count; i++)
                        player.Jump(shots[i]);
            }

            else if (Hardware.KeyPressed(Hardware.KEY_RIGHT) && !player.Dead())
                player.MoveRight();

            else if (Hardware.KeyPressed(Hardware.KEY_LEFT) && !player.Dead())
                player.MoveLeft();
             
            if (Hardware.KeyPressed(Hardware.KEY_SPC) && !player.Dead())
            {
                player.Fire();
                currentFrame++;
                // Adding shots after 15 frames
                if (currentFrame == 15)
                {
                    shots.Add(new Shot(player, this));
                    shots[shots.Count - 1].Activate();
                    currentFrame = 0;
                }
            }

            if (Hardware.KeyPressed(Hardware.KEY_Z))
                enemies.Clear();
                              
            if (Hardware.KeyPressed(Hardware.KEY_ESC))
                finished = true;
        }


        // Move enemies, shots, player, animate background, etc 
        public void MoveElements()
        {             
            player.Move();       
            for (int i = 0; i < enemies.Count; i++)
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
        public void CheckPlayerStatus()
        {
            // Revive player when dead and
            if (player.Dead())
                player.Restart();

            if (player.ImmunityTimeUp())
                player.BackToNormal();

            if (player.GetLife() > lifeX.Count)
                lifeX.Add((int)lifeX[lifeX.Count - 1] + 33);

            if (player.GetLife() == 0)
                finished = true;
        }

        // Place randomly items on the map
        public void PlaceItem()
        {
            if (!itemPlaced && time == 3)
            {
                itemPlaced = true;
                Random r = new Random();
                ArrayList validPositions = currentLevel.GetValidItemPositions(items[0]);

                int randomPosition = r.Next(0, currentLevel.GetValidItemPositions(items[0]).Count);
                string positionsXAndY = (string)validPositions[randomPosition];
                short positionX = Convert.ToInt16(positionsXAndY.Split()[1]);
                short positionY = Convert.ToInt16(positionsXAndY.Split()[0]);
                items[0].SetX(positionX);
                items[0].SetY(positionY);
                items[0].Show();
            }    
        }

        // Check collisions and apply game logic
        public void CheckCollisions()
        {
            // Check collisions between player and items
            if (player.CollisionsWith(items[0]))
            {
                // player.SetSpeed(player.GetSpeedX() + items[0].GetSpeedX(), // Picking items
                //     player.GetSpeedY() + items[0].GetSpeedY());
                items[0].Hide();
                player.SetPoints(player.GetPoints() + items[0].GetItemPoints());
                player.LifeUp();
                Hardware.WriteHiddenText(items[0].GetItemPoints().ToString(),
                (short)items[0].GetX(), (short)(items[0].GetY() - 100),
                0xCC, 0xCC, 0xCC,
                font18);
            }
              
            for (int i = 0; i < enemies.Count; i++)
            {
                // Check collisions between player and enemy
                if (enemies[i].CollisionsWith(player) && !player.Dead() &&
                        !player.IsImmune())
                {
                    player.Die();
                    player.SubtLife();
                }

                // Enemy getting out of shot
                if (enemies[i].IsInBubble())
                    enemies[i].GetOutOfBubble(); 

                for (int j = 0; j < shots.Count; j++)
                {
                    // Check collisions between enemy and shot
                    if (shots[j].CollisionsWith(enemies[i]) && shots[j].GetSpeedX() != 0
                            && !shots[j].GotEnemy() && !enemies[i].IsBaron())
                    {
                        enemies[i].Hide();
                        enemies[i].InBubble(shots[j]);
                        shots[j].GotEnemyIn(enemies[i]); 
                    }
                    
                    // Check collisions between player and shot    
                    if (player.CollisionsWith(shots[j]) && !shots[j].IsExploding() &&
                            !player.Dead())
                    {
                        shots[j].Hide();
                        player.SetPoints(player.GetPoints() + 10);
                    }

                    // Delete shot if not visible
                    if (!shots[j].IsVisible())                                
                        shots.Remove(shots[j]);
                }  
            }
        }

        // Change level when all enemies not visible
        public void ChangeLevel()
        {
            // Check if there are enemies still visible
            int count = 0;
            for (int i = 0; i < enemies.Count; i++)
                if (enemies[i].IsVisible() && !enemies[i].IsBaron()) 
                    count++;
            // Check if there are enemies in bubble
            for (int i = 0; i < shots.Count; i++)  
                if (shots[i].GotEnemy()) 
                    count++;             

            // Reset game
            if (count == 0 && level < 2)
            {
                enemies.Clear();
                //Hardware.ClearScreen();
                while (currentScroll > 0)
                {
                    Hardware.ScrollVertically(scrollSpeed);
                    currentScroll += scrollSpeed;
                    currentLevel.DrawOnHiddenScreen();
                    Hardware.ShowHiddenScreen();
                    Hardware.Pause(20);
                }
                Hardware.ResetScroll();
                itemPlaced = false;
                shots.Clear();
                baronCount = 0;
                level++;
                currentLevel.ChangeLevel(level);
                player.Restart();
                startTime = DateTime.Now;
                for (int i = 0; i < currentLevel.GetEnemyCount(); i++)
                    enemies.Add(new Bonzo(currentLevel.GetEnemyX()[i], currentLevel.GetEnemyY()[i], this));
                currentScroll = currentLevel.GetLevelHeight();
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

        // Game timer
        public void Timer()
        {
            currentTime = DateTime.Now;
            TimeSpan duration = currentTime - startTime;
            time = duration.Seconds;

            // Check time for baron appearing
            if (time == 25)
                timeUp = true;
            if (timeUp && baronCount == 0)
            {
                baronCount++;
                enemies.Add(new BaronVonBlubba(150, 150, this));   
            }
             
        }

        public int GetCurrentTime()
        {
            return time;
        }
           
        public Player GetPlayer()
        {
            return player;
        }

        public Level GetLevel()
        {
            return currentLevel;
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
                CheckPlayerStatus();
                DrawElements(); 
                CheckKeys();
                MoveElements();
                CheckCollisions();
                ChangeLevel();
                PauseTillNextFrame();
            }
        }
    }
}
