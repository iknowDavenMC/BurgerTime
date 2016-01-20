using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Burger_Time
{
    /// <summary>
    /// Holds everything relevant to a game
    /// </summary>
    public static class World
    {
        #region Attributes

        /// <summary>
        /// Current map
        /// </summary>
        public static Map map;

        /// <summary>
        /// Player
        /// </summary>
        public static PeterPepper player;

        /// <summary>
        /// Enemies
        /// </summary>
        public static List<Enemy> enemies;

        /// <summary>
        /// Power ups
        /// </summary>
        public static List<PowerUp> powerUps;

        /// <summary>
        /// Burger parts
        /// </summary>
        public static List<BurgerPart> burgerParts;

        /// <summary>
        /// Plates
        /// </summary>
        public static List<Plate> plates;

        /// <summary>
        /// Holds all entities, for drawing purposes
        /// </summary>
        public static List<Entity> entities;

        /// <summary>
        /// Timer for power up spawning
        /// </summary>
        private static float powerUpTimer;

        /// <summary>
        /// Delay between spawns
        /// </summary>
        private static float powerUpTime;

        /// <summary>
        /// Timer to have gap between levels
        /// </summary>
        private static float finishTimer;

        /// <summary>
        /// Delay for gap between levels
        /// </summary>
        private static float finishTime;

        /// <summary>
        /// Is the player finished with the level
        /// </summary>
        private static bool isDoneLevel;

        /// <summary>
        /// Total number of enemies at the start of a level
        /// </summary>
        private static int numberOfEnemies;

        /// <summary>
        /// Last spawn point for enemies
        /// </summary>
        private static Vector2 lastSpawn = Vector2.Zero;

        /// <summary>
        /// Has the dead music been played
        /// </summary>
        private static bool playDead;

        /// <summary>
        /// Has the win music been played
        /// </summary>
        private static bool playWin;

        #endregion

        #region Private Methods

        /// <summary>
        /// Load entity starting positions from the map
        /// </summary>
        private static void loadEntities()
        {
            // Set up for reading from file
            DataManager manager = DataManager.GetInstance();

            int level = DataManager.GetInstance().Level % DataManager.GetInstance().NumberOfLevels;

            if (level == 0)
            {
                level = DataManager.GetInstance().NumberOfLevels;
            }

            StreamReader reader = new StreamReader("Files/Entities" + level + ".txt");
            float scale = SettingsManager.GetInstance().Scale;

            enemies = new List<Enemy>();
            powerUps = new List<PowerUp>();
            numberOfEnemies = 0;

            try
            {
                int x, y;

                // Get entity positions
                string line = reader.ReadLine();

                if (line.StartsWith("Player"))
                {
                    line = reader.ReadLine();

                    x = int.Parse(line.Split(' ')[0]);
                    y = int.Parse(line.Split(' ')[1]);
                    player = new PeterPepper(map.MapIndex(y, x), 12 * scale, 20 * scale, 1.3f * scale, 0.8f * scale);
                    line = reader.ReadLine();
                }

                if (line.StartsWith("Egg"))
                {
                    line = reader.ReadLine();

                    do
                    {
                        x = int.Parse(line.Split(' ')[0]);
                        y = int.Parse(line.Split(' ')[1]);
                        enemies.Add(new Egg(map.MapIndex(y, x), 12 * scale, 20 * scale, 300,
                            (0.8f + 0.02f * manager.Level) * scale,
                            (0.4f + 0.02f * manager.Level) * scale, 1));
                        ++numberOfEnemies;
                        line = reader.ReadLine();
                    }
                    while (reader.Peek() != -1
                    && !line.StartsWith("Hot Dog")
                    && !line.StartsWith("Pickle")
                    && !line.StartsWith("End"));
                }

                if (line.StartsWith("Hot Dog"))
                {
                    line = reader.ReadLine();

                    do
                    {
                        x = int.Parse(line.Split(' ')[0]);
                        y = int.Parse(line.Split(' ')[1]);
                        enemies.Add(new HotDog(map.MapIndex(y, x), 12 * scale, 20 * scale, 100,
                            (0.8f + 0.02f * manager.Level) * scale,
                            (0.4f + 0.02f * manager.Level) * scale, 1));
                        ++numberOfEnemies;
                        line = reader.ReadLine();
                    }
                    while (reader.Peek() != -1
                    && !line.StartsWith("Pickle")
                    && !line.StartsWith("End"));
                }

                if (line.StartsWith("Pickle"))
                {
                    line = reader.ReadLine();

                    do
                    {
                        x = int.Parse(line.Split(' ')[0]);
                        y = int.Parse(line.Split(' ')[1]);
                        enemies.Add(new Pickle(map.MapIndex(y, x), 12 * scale, 20 * scale, 200,
                            (0.8f + 0.02f * manager.Level) * scale,
                            (0.4f + 0.02f * manager.Level) * scale, 1));
                        ++numberOfEnemies;
                        line = reader.ReadLine();
                    }
                    while (reader.Peek() != -1
                    && !line.StartsWith("End"));
                }

                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        /// <summary>
        /// Spawns an enemy
        /// </summary>
        private static void spawnEnemy()
        {
            int random = Game1.random.Next(0, 3);

            float scale = SettingsManager.GetInstance().Scale;

            Vector2[] positions = new Vector2[5];
            positions[0] = map.TopLeftCorner();
            positions[1] = map.TopRightCorner();
            positions[2] = map.BottomLeftCorner();
            positions[3] = map.BottomRightCorner();
            positions[4] = positions[0];

            // Find spawn point farthest from player but not used last
            for (int i = 1; i != positions.GetLength(0) - 1; ++i)
            {
                if ((player.Position - positions[i]).LengthSquared() > (player.Position - positions[4]).LengthSquared() &&
                    lastSpawn != positions[i])
                {
                    positions[4] = positions[i];
                }
            }

            lastSpawn = positions[4];

            if (random == 0)
            {
                enemies.Add(new Egg(positions[4], 12 * scale, 20 * scale, 300,
                            (0.8f + 0.02f * DataManager.GetInstance().Level) * scale,
                            (0.4f + 0.02f * DataManager.GetInstance().Level) * scale, 1));
                entities.Add(enemies[enemies.Count - 1]);
            }
            else if (random == 1)
            {
                enemies.Add(new HotDog(positions[4], 12 * scale, 20 * scale, 100,
                            (0.8f + 0.02f * DataManager.GetInstance().Level) * scale,
                            (0.4f + 0.02f * DataManager.GetInstance().Level) * scale, 1));
                entities.Add(enemies[enemies.Count - 1]);
            }
            else if (random == 2)
            {
                enemies.Add(new Pickle(positions[4], 12 * scale, 20 * scale, 200,
                            (0.8f + 0.02f * DataManager.GetInstance().Level) * scale,
                            (0.4f + 0.02f * DataManager.GetInstance().Level) * scale, 1));
                entities.Add(enemies[enemies.Count - 1]);
            }
        }

        /// <summary>
        /// Is level done
        /// </summary>
        /// <returns></returns>
        private static bool isLevelComplete()
        {
            if (isDoneLevel)
            {
                return true;
            }

            isDoneLevel = true;

            // If all burgers are done, then yes level is done
            foreach (BurgerPart burger in burgerParts)
            {
                if (burger.GetState != BurgerPart.State.Done)
                {
                    isDoneLevel = false;
                    break;
                }
            }

            return isDoneLevel;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Set up the world
        /// </summary>
        public static void Initialize()
        {
            // Set level
            int level = DataManager.GetInstance().Level % DataManager.GetInstance().NumberOfLevels;

            if (level == 0)
            {
                level = DataManager.GetInstance().NumberOfLevels;
            }

            // Create map
            map = new Map("Files/Map" + level + ".txt");
            PopUpHandler.Initialize();
        }

        /// <summary>
        /// Reset when done level or dead
        /// </summary>
        public static void Reset()
        {
            if (Game1.gameState == Game1.GameState.Level)
            {
                SoundManager.GetInstance().PlaySong("Level", true);
            }

            loadEntities();

            powerUpTimer = 0;
            powerUpTime = 30;

            finishTimer = 0;
            finishTime = 5;

            isDoneLevel = false;
            playDead = false;
            playWin = false;

            // For drawing in order
            entities = new List<Entity>();
            entities.Add(player);

            foreach (Enemy enemy in enemies)
            {
                entities.Add(enemy);
            }

            foreach (PowerUp powerUp in powerUps)
            {
                entities.Add(powerUp);
            }
        }

        /// <summary>
        /// Load burger parts and plates from file, only if level complete
        /// </summary>
        public static void LoadBurgers()
        {
            // Set up for reading from file
            int level = DataManager.GetInstance().Level % DataManager.GetInstance().NumberOfLevels;

            if (level == 0)
            {
                level = DataManager.GetInstance().NumberOfLevels;
            }

            StreamReader reader = new StreamReader("Files/Burger" + level + ".txt");
            float scale = SettingsManager.GetInstance().Scale;

            burgerParts = new List<BurgerPart>();
            plates = new List<Plate>();

            try
            {
                int x, y;

                // Get burger positions
                string line = reader.ReadLine();

                if (line.StartsWith("Bottom Bun"))
                {
                    line = reader.ReadLine();

                    do
                    {
                        x = int.Parse(line.Split(' ')[0]);
                        y = int.Parse(line.Split(' ')[1]);
                        burgerParts.Add(new BottomBun(map.MapIndex(y, x), 75 * scale, 15 * scale));
                        line = reader.ReadLine();
                    }
                    while (reader.Peek() != -1
                    && !line.StartsWith("Cheese")
                    && !line.StartsWith("Lettuce")
                    && !line.StartsWith("Meat")
                    && !line.StartsWith("Tomato")
                    && !line.StartsWith("Top Bun")
                    && !line.StartsWith("Plate")
                    && !line.StartsWith("End"));
                }

                if (line.StartsWith("Cheese"))
                {
                    line = reader.ReadLine();

                    do
                    {
                        x = int.Parse(line.Split(' ')[0]);
                        y = int.Parse(line.Split(' ')[1]);
                        burgerParts.Add(new Cheese(map.MapIndex(y, x), 75 * scale, 15 * scale));
                        line = reader.ReadLine();
                    }
                    while (reader.Peek() != -1
                    && !line.StartsWith("Lettuce")
                    && !line.StartsWith("Meat")
                    && !line.StartsWith("Tomato")
                    && !line.StartsWith("Top Bun")
                    && !line.StartsWith("Plate")
                    && !line.StartsWith("End"));
                }

                if (line.StartsWith("Lettuce"))
                {
                    line = reader.ReadLine();

                    do
                    {
                        x = int.Parse(line.Split(' ')[0]);
                        y = int.Parse(line.Split(' ')[1]);
                        burgerParts.Add(new Lettuce(map.MapIndex(y, x), 75 * scale, 15 * scale));
                        line = reader.ReadLine();
                    }
                    while (reader.Peek() != -1
                    && !line.StartsWith("Meat")
                    && !line.StartsWith("Tomato")
                    && !line.StartsWith("Top Bun")
                    && !line.StartsWith("Plate")
                    && !line.StartsWith("End"));
                }

                if (line.StartsWith("Meat"))
                {
                    line = reader.ReadLine();

                    do
                    {
                        x = int.Parse(line.Split(' ')[0]);
                        y = int.Parse(line.Split(' ')[1]);
                        burgerParts.Add(new Meat(map.MapIndex(y, x), 75 * scale, 15 * scale));
                        line = reader.ReadLine();
                    }
                    while (reader.Peek() != -1
                    && !line.StartsWith("Tomato")
                    && !line.StartsWith("Top Bun")
                    && !line.StartsWith("Plate")
                    && !line.StartsWith("End"));
                }

                if (line.StartsWith("Tomato"))
                {
                    line = reader.ReadLine();

                    do
                    {
                        x = int.Parse(line.Split(' ')[0]);
                        y = int.Parse(line.Split(' ')[1]);
                        burgerParts.Add(new Tomato(map.MapIndex(y, x), 75 * scale, 15 * scale));
                        line = reader.ReadLine();
                    }
                    while (reader.Peek() != -1
                    && !line.StartsWith("Top Bun")
                    && !line.StartsWith("Plate")
                    && !line.StartsWith("End"));
                }

                if (line.StartsWith("Top Bun"))
                {
                    line = reader.ReadLine();

                    do
                    {
                        x = int.Parse(line.Split(' ')[0]);
                        y = int.Parse(line.Split(' ')[1]);
                        burgerParts.Add(new TopBun(map.MapIndex(y, x), 75 * scale, 15 * scale));
                        line = reader.ReadLine();
                    }
                    while (reader.Peek() != -1
                    && !line.StartsWith("Plate")
                    && !line.StartsWith("End"));
                }

                if (line.StartsWith("Plate"))
                {
                    line = reader.ReadLine();

                    do
                    {
                        x = int.Parse(line.Split(' ')[0]);
                        y = int.Parse(line.Split(' ')[1]);
                        plates.Add(new Plate(map.MapIndex(y, x), 75 * scale, 15 * scale));
                        line = reader.ReadLine();
                    }
                    while (reader.Peek() != -1
                    && !line.StartsWith("End"));
                }

                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        /// <summary>
        /// Load content at the start of the program launch
        /// </summary>
        /// <param name="content">Content manager</param>
        public static void LoadContent(ContentManager content)
        {
            PopUpHandler.LoadContent(content);
            SpriteDatabase.LoadContent(content);
            HUD.LoadContent(content);
            Map.LoadContent(content);

            LoadBurgers();

            Reset();        
        }

        /// <summary>
        /// Update the game world
        /// </summary>
        /// <param name="gameTime"></param>
        public static void Update(GameTime gameTime)
        {
            #region Game Logic

            // Play dead song if player is dead
            if (player.GetState == PeterPepper.State.Dying && !playDead)
            {
                playDead = true;
                SoundManager.GetInstance().PlaySong("Dead", false);
            }

            // If dead, reload level minus life
            if (player.GetState == PeterPepper.State.Dead)
            {
                finishTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Reload once enough time has passed
                if (finishTimer >= finishTime / 4)
                {
                    DataManager.GetInstance().LoseALife();

                    // Out of lives
                    if (DataManager.GetInstance().Lives == 0)
                    {
                        DataManager.GetInstance().SaveHighScore();
                        Game1.gameState = Game1.GameState.Menu;
                        Game1.Reset();
                        SoundManager.GetInstance().PlaySong("Menu", false);

                        return;
                    }
                    else
                    {
                        Reset();
                    }
                }
                else
                {
                    return;
                }
            }

            // If all burgers are done, go to next level
            if (isLevelComplete())
            {
                if (!playWin)
                {
                    playWin = true;
                    SoundManager.GetInstance().PlaySong("Win", false);
                }

                finishTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                // If delay has passed
                if (finishTimer >= finishTime)
                {
                    DataManager.GetInstance().IncreaseLevel();
                    Initialize();
                    Reset();
                    LoadBurgers();
                }
                else
                {
                    return;
                }
            }

            #endregion

            #region Spawns

            #region Spawn enemies

            // Spawn until there is the right number of enemies
            while (enemies.Count != numberOfEnemies)
            {
                spawnEnemy();
            }

            #endregion

            #region Spawn Power Ups

            powerUpTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (powerUpTimer > powerUpTime)
            {
                powerUpTimer = 0;

                int random = Game1.random.Next(0, 3);

                if (random == 0)
                {
                    powerUps.Add(new IceCream(map.MapIndex(3, 8), 25 * SettingsManager.GetInstance().Scale,
                        39 * SettingsManager.GetInstance().Scale, 500, 10000));
                    entities.Add(powerUps[powerUps.Count - 1]);
                }
                else if (random == 1)
                {
                    powerUps.Add(new FrenchFries(map.MapIndex(3, 8), 25 * SettingsManager.GetInstance().Scale,
                        39 * SettingsManager.GetInstance().Scale, 1500, 10000));
                    entities.Add(powerUps[powerUps.Count - 1]);
                }
                else
                {
                    powerUps.Add(new Coffee(map.MapIndex(3, 8), 25 * SettingsManager.GetInstance().Scale,
                        39 * SettingsManager.GetInstance().Scale, 1000, 10000));
                    entities.Add(powerUps[powerUps.Count - 1]);
                }

                SoundManager.GetInstance().PlaySound("Appearance", map.MapIndex(3, 8).X);
            }

            #endregion

            #endregion

            #region Collisions

            #region Check for pepper and enemy collision

            if (player.Pepper != null)
            {
                foreach (Enemy enemy in enemies)
                {
                    if (enemy.GetState != Enemy.State.Pepper &&
                        enemy.GetState != Enemy.State.Dead &&
                        enemy.GetState != Enemy.State.Dying &&
                        player.Pepper.BoundingBox.Collides(enemy.BoundingBox))
                    {
                        enemy.TransitionToState(Enemy.State.Pepper);
                        SoundManager.GetInstance().PlaySound("Peppered", enemy.Position.X);
                    }
                }
            }

            #endregion

            #region Player power-up collision

            for (int i = 0; i != powerUps.Count;)
            {
                if (player.BoundingBox.Collides(powerUps[i].BoundingBox))
                {
                    SoundManager.GetInstance().PlaySound("Consumption", powerUps[i].Position.X);
                    DataManager.GetInstance().IncreaseScore(powerUps[i].Points, powerUps[i].Position);
                    DataManager.GetInstance().IncreasePepper();
                    entities.Remove(powerUps[i]);
                    powerUps.RemoveAt(i);
                }
                else
                {
                    ++i;
                }
            }

            #endregion

            #region Burger enemy collision

            foreach (BurgerPart burger in burgerParts)
            {
                foreach (Enemy enemy in enemies)
                {
                    if (burger.GetState == BurgerPart.State.Falling &&
                        enemy.GetState != Enemy.State.Dying &&
                        enemy.GetState != Enemy.State.Dead &&
                        enemy.GetState != Enemy.State.Riding &&
                        enemy.BoundingBox.Collides(burger.BoundingBox))
                    {
                        enemy.TransitionToState(Enemy.State.Dying);
                    }
                }
            }

            #endregion

            #region Enemy player collision

            foreach (Enemy enemy in enemies)
            {
                if (enemy.GetState != Enemy.State.Pepper &&
                    enemy.GetState != Enemy.State.Dead &&
                    enemy.GetState != Enemy.State.Dying &&
                    enemy.GetState != Enemy.State.Riding &&
                    player.GetState != PeterPepper.State.Dying &&
                    player.GetState != PeterPepper.State.Dead &&
                    player.BoundingBox.Collides(enemy.BoundingBox))
                {
                    player.TransitionToState(PeterPepper.State.Dying);
                }
            }

            #endregion

            #endregion

            #region Updates

            #region Update the player

            player.Update(gameTime);

            #endregion

            #region Update the enemies

            foreach (Enemy enemy in enemies)
            {
                enemy.Update(gameTime);
            }

            // Remove dead enemies
            for (int i = 0; i != enemies.Count; )
            {
                if (enemies[i].GetState == Enemy.State.Dead)
                {
                    SoundManager.GetInstance().PlaySound("Dead", enemies[i].Position.X);
                    DataManager.GetInstance().IncreaseScore(enemies[i].Points, enemies[i].Position);
                    entities.Remove(enemies[i]);
                    enemies.RemoveAt(i);
                }
                else
                {
                    ++i;
                }
            }

            #endregion

            #region Update the World

            // Remove consumed power ups
            for (int i = 0; i != powerUps.Count; )
            {
                if (powerUps[i].IsTimeUp)
                {
                    entities.Remove(powerUps[i]);
                    powerUps.RemoveAt(i);
                }
                else
                {
                    ++i;
                }
            }

            // Update power ups
            foreach (PowerUp powerUp in powerUps)
            {
                powerUp.Update(gameTime);
            }

            // Update burgers
            foreach (BurgerPart burger in burgerParts)
            {
                burger.Update(gameTime);
            }

            #endregion

            #endregion

            // Scores
            PopUpHandler.Update(gameTime);

            // Sort all entities for drawing
            entities = entities.OrderBy(o => o.Position.Y).ToList();
        }

        /// <summary>
        /// Draw the world
        /// </summary>
        public static void Draw()
        {
            map.Draw();
            HUD.Draw();

            foreach (Plate plate in plates)
            {
                plate.Draw();
            }

            foreach (BurgerPart burgerPart in burgerParts)
            {
                burgerPart.Draw();
            }

            foreach (Entity entity in entities)
            {
                entity.Draw();
            }

            PopUpHandler.Draw();
        }

        #endregion
    }
}
