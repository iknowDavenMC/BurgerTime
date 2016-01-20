using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Burger_Time
{
    /// <summary>
    /// Handles drawing the heads up display and user interface
    /// </summary>
    public static class HUD
    {
        #region Attributes

        /// <summary>
        /// Font for writing to the HUD
        /// </summary>
        private static SpriteFont font;

        /// <summary>
        /// Image for the level
        /// </summary>
        private static Texture2D imageLevel;

        /// <summary>
        /// Image for the life
        /// </summary>
        private static Texture2D imageLife;

        /// <summary>
        /// Image for the menu
        /// </summary>
        private static Texture2D imageMenu;

        /// <summary>
        /// Space between screen edge and an image
        /// </summary>
        private static int borderSize = 25;

        /// <summary>
        /// Space between images
        /// </summary>
        private static int spacing = 5;

        /// <summary>
        /// High score message
        /// </summary>
        private static string highScore = "HI-SCORE";

        /// <summary>
        /// Score message
        /// </summary>
        private static string score = "SCORE";

        /// <summary>
        /// Pepper message
        /// </summary>
        private static string pepper = "PEPPER";

        /// <summary>
        /// Play
        /// </summary>
        private static string play = "Press enter to play";

        /// <summary>
        /// Pause
        /// </summary>
        private static string pause = "Press P or Pause to resume playing";

        /// <summary>
        /// Quit
        /// </summary>
        private static string quit = "Press escape to quit";

        /// <summary>
        /// Menu
        /// </summary>
        private static string menu = "Press escape to go back to the menu";

        /// <summary>
        /// Sound
        /// </summary>
        private static string sound = "Press M to toggle sound";

        /// <summary>
        /// Screen
        /// </summary>
        private static string screen = "Press F to toggle full screen";

        /// <summary>
        /// Score message position
        /// </summary>
        private static Vector2 scoreMessagePosition;

        /// <summary>
        /// High score message position
        /// </summary>
        private static Vector2 highScoreMessagePosition;

        /// <summary>
        /// Pepper message position
        /// </summary>
        private static Vector2 pepperMessagePosition;

        /// <summary>
        /// Life position
        /// </summary>
        private static Vector2 lifePosition;

        /// <summary>
        /// Level position
        /// </summary>
        private static Vector2 levelPosition;

        #endregion

        #region Properties

        public static Vector2 HudBorder
        {
            get { return new Vector2(borderSize * 4, borderSize * 4); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create the HUD
        /// </summary>
        public static void Initialize()
        {
            scoreMessagePosition = new Vector2(borderSize, borderSize);

            highScoreMessagePosition = new Vector2(SettingsManager.GetInstance().ScreenWidth / 2 - (font.MeasureString(highScore).X / 2), borderSize);

            pepperMessagePosition = new Vector2(SettingsManager.GetInstance().ScreenWidth - borderSize - font.MeasureString(pepper).X, borderSize);

            lifePosition = new Vector2(borderSize, SettingsManager.GetInstance().ScreenHeight - borderSize - imageLife.Height);

            levelPosition = new Vector2(SettingsManager.GetInstance().ScreenWidth - borderSize - imageLevel.Width, SettingsManager.GetInstance().ScreenHeight - borderSize - imageLevel.Height);
        }

        /// <summary>
        /// Load content
        /// </summary>
        /// <param name="content">Game content manager</param>
        public static void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Fonts/Arcade");
            imageLevel = content.Load<Texture2D>("Images/Level");
            imageLife = content.Load<Texture2D>("Images/Life");
            imageMenu = content.Load<Texture2D>("Images/Menu");

            Initialize();
        }

        /// <summary>
        /// Update to screen size
        /// </summary>
        /// <param name="content">Game content manager</param>
        public static void Update()
        {
            Initialize();
        }

        /// <summary>
        /// Draw the HUD
        /// </summary>
        public static void Draw()
        {
            int life = DataManager.GetInstance().Lives;
            int level = DataManager.GetInstance().Level;

            Vector2 scorePosition = scoreMessagePosition + new Vector2(0,
                font.MeasureString(score).Y);

            Vector2 highScorePosition = new Vector2(SettingsManager.GetInstance().ScreenWidth / 2 -
                (font.MeasureString(DataManager.GetInstance().HighScore.ToString()).X / 2),
                highScoreMessagePosition.Y + font.MeasureString(highScore).Y);

            Vector2 pepperPosition = new Vector2(SettingsManager.GetInstance().ScreenWidth - borderSize -
                font.MeasureString(DataManager.GetInstance().Pepper.ToString()).X,
                pepperMessagePosition.Y + font.MeasureString(pepper).Y);

            Game1.spriteBatch.DrawString(font, score, scoreMessagePosition, Color.Red);
            Game1.spriteBatch.DrawString(font, highScore, highScoreMessagePosition, Color.Red);
            Game1.spriteBatch.DrawString(font, pepper, pepperMessagePosition, Color.Green);
            Game1.spriteBatch.DrawString(font, DataManager.GetInstance().Score.ToString(), scorePosition, Color.White);
            Game1.spriteBatch.DrawString(font, DataManager.GetInstance().HighScore.ToString(), highScorePosition, Color.White);
            Game1.spriteBatch.DrawString(font, DataManager.GetInstance().Pepper.ToString(), pepperPosition, Color.White);

            if (life > 0)
            {
                for (int i = 0; i != life - 1; ++i)
                {
                    Game1.spriteBatch.Draw(imageLife, new Vector2(lifePosition.X, lifePosition.Y - (i * (imageLife.Height + spacing))), Color.White);
                }
            }

            for (int i = 0; i != level; ++i)
            {
                Game1.spriteBatch.Draw(imageLevel, new Vector2(levelPosition.X, levelPosition.Y - (i * (imageLevel.Height + spacing))), Color.White);
            }
        }

        /// <summary>
        /// Draw the menu
        /// </summary>
        public static void DrawMenu()
        {
            Vector2 highScorePosition = new Vector2(SettingsManager.GetInstance().ScreenWidth / 2 -
                (font.MeasureString(DataManager.GetInstance().HighScore.ToString()).X / 2),
                highScoreMessagePosition.Y + font.MeasureString(highScore).Y);

            Game1.spriteBatch.DrawString(font, highScore, highScoreMessagePosition, Color.Red);
            Game1.spriteBatch.DrawString(font, DataManager.GetInstance().HighScore.ToString(), highScorePosition, Color.White);

            Game1.spriteBatch.Draw(imageMenu,
                new Vector2(SettingsManager.GetInstance().ScreenWidth / 2 - 150,
                SettingsManager.GetInstance().ScreenHeight / 2 - 200),
                Color.White);

            Game1.spriteBatch.DrawString(font, play,
                new Vector2(SettingsManager.GetInstance().ScreenWidth / 2 - font.MeasureString(play).X / 2,
                SettingsManager.GetInstance().ScreenHeight / 2 - 2 * font.MeasureString(play).Y),
                Color.White);

            Game1.spriteBatch.DrawString(font, quit,
                new Vector2(SettingsManager.GetInstance().ScreenWidth / 2 - font.MeasureString(quit).X / 2,
                SettingsManager.GetInstance().ScreenHeight / 2 - 1 * font.MeasureString(quit).Y),
                Color.White);

            Game1.spriteBatch.DrawString(font, sound,
                new Vector2(SettingsManager.GetInstance().ScreenWidth / 2 - font.MeasureString(sound).X / 2,
                SettingsManager.GetInstance().ScreenHeight / 2 + 1 * font.MeasureString(sound).Y),
                Color.White);

            Game1.spriteBatch.DrawString(font, screen,
                new Vector2(SettingsManager.GetInstance().ScreenWidth / 2 - font.MeasureString(screen).X / 2,
                SettingsManager.GetInstance().ScreenHeight / 2 + 2 * font.MeasureString(screen).Y),
                Color.White);

        }

        /// <summary>
        /// Draw the pause screen
        /// </summary>
        public static void DrawPause()
        {
            Game1.spriteBatch.Draw(SpriteDatabase.Blank, Vector2.Zero, null, new Color(0, 0, 0, 0.6f), 0,
                Vector2.Zero, new Vector2(SettingsManager.GetInstance().ScreenWidth, SettingsManager.GetInstance().ScreenHeight),
                SpriteEffects.None, 0);

            Game1.spriteBatch.DrawString(font, pause,
                new Vector2(SettingsManager.GetInstance().ScreenWidth / 2 - font.MeasureString(pause).X / 2,
                SettingsManager.GetInstance().ScreenHeight / 2 - 2 * font.MeasureString(pause).Y),
                Color.White);

            Game1.spriteBatch.DrawString(font, menu,
                new Vector2(SettingsManager.GetInstance().ScreenWidth / 2 - font.MeasureString(menu).X / 2,
                SettingsManager.GetInstance().ScreenHeight / 2 - 1 * font.MeasureString(menu).Y),
                Color.White);

            Game1.spriteBatch.DrawString(font, sound,
                new Vector2(SettingsManager.GetInstance().ScreenWidth / 2 - font.MeasureString(sound).X / 2,
                SettingsManager.GetInstance().ScreenHeight / 2 + 1 * font.MeasureString(sound).Y),
                Color.White);
        }

        #endregion
    }
}
