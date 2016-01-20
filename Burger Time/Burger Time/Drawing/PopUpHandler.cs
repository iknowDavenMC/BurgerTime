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
    /// Class which handles drawing points on the screen
    /// 
    /// NOTE:
    /// Code is based off of code from my COMP 476 project,
    /// written by Alex Attar, myself, Jeff How and Addison Rodomista
    /// </summary>
    public static class PopUpHandler
    {
        #region Attributes

        /// <summary>
        /// Position of the popup
        /// </summary>
        private static List<Vector2> positions;

        /// <summary>
        /// Point values
        /// </summary>
        private static List<int> values;

        /// <summary>
        /// Timers
        /// </summary>
        private static List<float> timers;

        /// <summary>
        /// Life span
        /// </summary>
        private static List<float> times;

        /// <summary>
        /// Font to use
        /// </summary>
        private static SpriteFont font;

        #endregion

        #region Methods

        /// <summary>
        /// Initialize the manager
        /// </summary>
        public static void Initialize()
        {
            positions = new List<Vector2>();
            values = new List<int>();
            timers = new List<float>();
            times = new List<float>();
        }

        /// <summary>
        /// Load content
        /// </summary>
        public static void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Fonts/Arcade");
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="gameTime">Game's time</param>
        public static void Update(GameTime gameTime)
        {
            for (int i = 0; i != positions.Count;)
            {
                timers[i] += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (timers[i] > times[i])
                {
                    positions.RemoveAt(i);
                    values.RemoveAt(i);
                    timers.RemoveAt(i);
                    times.RemoveAt(i);
                }
                else
                {
                    ++i;
                }
            }
        }

        /// <summary>
        /// Add a pop up
        /// </summary>
        public static void AddPopUp(Vector2 position, int value, float time)
        {
            positions.Add(position - font.MeasureString(value.ToString()) / 2);
            values.Add(value);
            timers.Add(0);
            times.Add(time);
        }

        /// <summary>
        /// Draw
        /// </summary>
        public static void Draw()
        {
            for (int i = 0; i != positions.Count; ++i)
            {
                Game1.spriteBatch.DrawString(font, values[i].ToString(), positions[i], Color.Black);
                Game1.spriteBatch.DrawString(font, values[i].ToString(), positions[i] + new Vector2(1, -1), Color.White);
            }
        }

        #endregion
    }
}
