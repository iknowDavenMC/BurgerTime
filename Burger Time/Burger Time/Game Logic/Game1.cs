using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Burger_Time
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region Enumerations

        public enum GameState
        { Menu, Pause, Level }

        #endregion

        #region Attributes

        /// <summary>
        /// For anything that needs a random number
        /// </summary>
        public static Random random = new Random();

        /// <summary>
        /// So that everyone has access to the graphics manager
        /// </summary>
        public static GraphicsDeviceManager graphics;

        /// <summary>
        /// So that a draw can be done without passing anything
        /// </summary>
        public static SpriteBatch spriteBatch;

        /// <summary>
        /// State of the game
        /// </summary>
        public static GameState gameState = GameState.Menu;

        /// <summary>
        /// Input timer
        /// </summary>
        public static float inputTimer = 0;

        /// <summary>
        /// Input delay to prevent double-clicking
        /// </summary>
        public float inputTime = 150;

        /// <summary>
        /// Sets and monitors frame rate
        /// </summary>
        private FrameRate frameRate;

        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.Window.Title = "Burger Time";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            frameRate = new FrameRate(this, 1);
            World.Initialize();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Set screen information
            graphics.PreferredBackBufferWidth = SettingsManager.GetInstance().ScreenWidth;
            graphics.PreferredBackBufferHeight = SettingsManager.GetInstance().ScreenHeight;
            graphics.IsFullScreen = SettingsManager.GetInstance().IsFullScreen;
            graphics.ApplyChanges();

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load content
            SoundManager.GetInstance().LoadContent(Content);
            SoundManager.GetInstance().PlaySong("Menu", false);
            World.LoadContent(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Handle input
            inputTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            InputManager inputManager = InputManager.GetInstance();
            inputManager.Update(gameTime);

            // Update based on game state
            switch (gameState)
            {
                case GameState.Menu:

                    if (inputManager.IsDoing("Quit") && inputTimer > inputTime)
                    {
                        DataManager.GetInstance().SaveHighScore();
                        this.Exit();
                    }
                    else if (inputManager.IsDoing("Screen") && inputTimer > inputTime)
                    {
                        SettingsManager.GetInstance().ToggleFullScreen();
                        inputTimer = 0;
                    }
                    else if (inputManager.IsDoing("Sound") && inputTimer > inputTime)
                    {
                        SettingsManager.GetInstance().ToggleMute();
                        inputTimer = 0;
                    }
                    else if (inputManager.IsDoing("Play") && inputTimer > inputTime)
                    {
                        gameState = GameState.Level;
                        inputTimer = 0;
                        SoundManager.GetInstance().PlaySound("Start");
                        SoundManager.GetInstance().PlaySong("Level", true);
                    }

                    break;

                case GameState.Pause:

                    if (inputManager.IsDoing("Quit") && inputTimer > inputTime)
                    {
                        gameState = GameState.Menu;
                        inputTimer = 0;
                        Reset();
                        SoundManager.GetInstance().PlaySong("Menu", false);
                    }
                    else if (inputManager.IsDoing("Sound") && inputTimer > inputTime)
                    {
                        SettingsManager.GetInstance().ToggleMute();
                        inputTimer = 0;
                    }
                    else if (inputManager.IsDoing("Pause") && inputTimer > inputTime)
                    {
                        gameState = GameState.Level;
                        inputTimer = 0;
                    }

                    break;

                case GameState.Level:

                    if (inputManager.IsDoing("Pause") && inputTimer > inputTime)
                    {
                        gameState = GameState.Pause;
                        inputTimer = 0;
                    }
                    else
                    {
                        World.Update(gameTime);
                    }

                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            switch (gameState)
            {
                case GameState.Menu:

                    HUD.DrawMenu();

                    break;

                case GameState.Pause:

                    World.Draw();
                    HUD.DrawPause();

                    break;

                case GameState.Level:

                    World.Draw();

                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Reset everything
        /// </summary>
        public static void Reset()
        {
            DataManager.GetInstance().Reset();
            HUD.Initialize();
            World.Initialize();
            World.Reset();
            World.LoadBurgers();
        }
    }
}
