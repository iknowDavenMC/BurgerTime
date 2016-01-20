#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
#endregion

namespace Burger_Time
{
    /// <summary>
    /// Class which stores all animations in the game
    /// 
    /// NOTE:
    /// Code is based off of code from my COMP 476 project,
    /// written by Alex Attar, myself, Jeff How and Addison Rodomista
    /// </summary>
    public static class SpriteDatabase
    {
        #region Attributes 
        
        /// <summary>
        /// Dictionary of all animations in the game
        /// </summary>
        private static SortedDictionary<string, Animation> animations = new SortedDictionary<string, Animation>();

        /// <summary>
        /// Blank texture
        /// </summary>
        private static Texture2D blank;

        #endregion

        #region Properties

        public static Texture2D Blank
        {
            get { return blank; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads the animations
        /// </summary>
        /// <param name="Content">Content manager</param>
        public static void LoadContent(ContentManager Content)
        {
            // Load animation images
            blank = Content.Load<Texture2D>("Images/Blank");

            #region Peter Pepper

            // Peter Pepper
            Texture2D peterPepper = Content.Load<Texture2D>("Images/Peter Pepper");
            SpriteDatabase.AddAnimation(new Animation("Peter Pepper Static Walk", peterPepper, 1,
                new int[1] { 50 },
                new int[1] { 78 },
                new int[1] { 0 },
                new int[1] { 0 },
                new int[1] { 150 }));

            SpriteDatabase.AddAnimation(new Animation("Peter Pepper Walk", peterPepper, 4,
                new int[4] { 50, 50, 50, 50 },
                new int[4] { 78, 78, 78, 78 },
                new int[4] { 0, 50, 100, 150 },
                new int[4] { 0, 0, 0, 0 },
                new int[4] { 110, 110, 110, 110 }));

            SpriteDatabase.AddAnimation(new Animation("Peter Pepper Climb", peterPepper, 4,
                new int[4] { 50, 50, 50, 50 },
                new int[4] { 78, 78, 78, 78 },
                new int[4] { 0, 50, 100, 150 },
                new int[4] { 78, 78, 78, 78 },
                new int[4] { 100, 100, 100, 100 }));

            SpriteDatabase.AddAnimation(new Animation("Peter Pepper Dead", peterPepper, 1,
                new int[1] { 50 },
                new int[1] { 78 },
                new int[1] { 250 },
                new int[1] { 156 },
                new int[1] { 10000 }));

            SpriteDatabase.AddAnimation(new Animation("Peter Pepper Dying", peterPepper, 14,
                new int[14] { 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50 },
                new int[14] { 78, 78, 78, 78, 78, 78, 78, 78, 78, 78, 78, 78, 78, 78 },
                new int[14] { 0, 50, 100, 150, 200, 250, 200, 250, 200, 250, 200, 250, 200, 250 },
                new int[14] { 156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 156 },
                new int[14] { 150, 1000, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100 },
                true));

            SpriteDatabase.AddAnimation(new Animation("Peter Pepper Pepper", peterPepper, 4,
                new int[4] { 50, 50, 54, 55 },
                new int[4] { 78, 78, 78, 78 },
                new int[4] { 0, 50, 100, 154 },
                new int[4] { 234, 234, 234, 234 },
                new int[4] { 75, 75, 75, 75 },
                true));

            #endregion

            #region Egg

            // Egg
            Texture2D egg = Content.Load<Texture2D>("Images/Egg");
            SpriteDatabase.AddAnimation(new Animation("Egg Walk", egg, 2,
                new int[2] { 50, 50 },
                new int[2] { 78, 78 },
                new int[2] { 204, 255 },
                new int[2] { 0, 0 },
                new int[2] { 110, 110 }));

            SpriteDatabase.AddAnimation(new Animation("Egg Ascension", egg, 2,
                new int[2] { 50, 50 },
                new int[2] { 78, 78 },
                new int[2] { 102, 153 },
                new int[2] { 0, 0 },
                new int[2] { 100, 100 }));

            SpriteDatabase.AddAnimation(new Animation("Egg Descension", egg, 2,
                new int[2] { 50, 50 },
                new int[2] { 78, 78 },
                new int[2] { 0, 51 },
                new int[2] { 0, 0 },
                new int[2] { 100, 100 }));

            SpriteDatabase.AddAnimation(new Animation("Egg Dying", egg, 4,
                new int[4] { 50, 50, 50, 50 },
                new int[4] { 78, 78, 78, 78 },
                new int[4] { 0, 51, 102, 153 },
                new int[4] { 79, 79, 79, 79 },
                new int[4] { 100, 100, 100, 100 },
                true));

            SpriteDatabase.AddAnimation(new Animation("Egg Pepper", egg, 2,
                new int[2] { 50, 50 },
                new int[2] { 78, 78 },
                new int[2] { 204, 255, },
                new int[2] { 79, 79 },
                new int[2] { 250, 250 }));

            SpriteDatabase.AddAnimation(new Animation("Egg Dead", egg, 1,
                new int[1] { 50 },
                new int[1] { 78 },
                new int[1] { 153 },
                new int[1] { 79 },
                new int[1] { 10000 }));

            #endregion

            #region Hot Dog

            // Hot Dog
            Texture2D hotDog = Content.Load<Texture2D>("Images/Hot Dog");
            SpriteDatabase.AddAnimation(new Animation("Hot Dog Walk", hotDog, 2,
                new int[2] { 50, 50 },
                new int[2] { 78, 78 },
                new int[2] { 204, 255 },
                new int[2] { 0, 0 },
                new int[2] { 110, 110 }));

            SpriteDatabase.AddAnimation(new Animation("Hot Dog Ascension", hotDog, 2,
                new int[2] { 50, 50 },
                new int[2] { 78, 78 },
                new int[2] { 102, 153 },
                new int[2] { 0, 0 },
                new int[2] { 100, 100 }));

            SpriteDatabase.AddAnimation(new Animation("Hot Dog Descension", hotDog, 2,
                new int[2] { 50, 50 },
                new int[2] { 78, 78 },
                new int[2] { 0, 51 },
                new int[2] { 0, 0 },
                new int[2] { 100, 100 }));

            SpriteDatabase.AddAnimation(new Animation("Hot Dog Dying", hotDog, 4,
                new int[4] { 50, 50, 50, 50 },
                new int[4] { 78, 78, 78, 78 },
                new int[4] { 0, 51, 102, 153 },
                new int[4] { 79, 79, 79, 79 },
                new int[4] { 100, 100, 100, 100 },
                true));

            SpriteDatabase.AddAnimation(new Animation("Hot Dog Pepper", hotDog, 2,
                new int[2] { 50, 50 },
                new int[2] { 78, 78 },
                new int[2] { 204, 255, },
                new int[2] { 79, 79 },
                new int[2] { 250, 250 }));

            SpriteDatabase.AddAnimation(new Animation("Hot Dog Dead", hotDog, 1,
                new int[1] { 50 },
                new int[1] { 78 },
                new int[1] { 153 },
                new int[1] { 79 },
                new int[1] { 10000 }));

            #endregion

            #region Pickle

            // Pickle
            Texture2D pickle = Content.Load<Texture2D>("Images/Pickle");
            SpriteDatabase.AddAnimation(new Animation("Pickle Walk", pickle, 2,
                new int[2] { 50, 50 },
                new int[2] { 78, 78 },
                new int[2] { 204, 255 },
                new int[2] { 0, 0 },
                new int[2] { 110, 110 }));

            SpriteDatabase.AddAnimation(new Animation("Pickle Ascension", pickle, 2,
                new int[2] { 50, 50 },
                new int[2] { 78, 78 },
                new int[2] { 102, 153 },
                new int[2] { 0, 0 },
                new int[2] { 100, 100 }));

            SpriteDatabase.AddAnimation(new Animation("Pickle Descension", pickle, 2,
                new int[2] { 50, 50 },
                new int[2] { 78, 78 },
                new int[2] { 0, 51 },
                new int[2] { 0, 0 },
                new int[2] { 100, 100 }));

            SpriteDatabase.AddAnimation(new Animation("Pickle Dying", pickle, 4,
                new int[4] { 50, 50, 50, 50 },
                new int[4] { 78, 78, 78, 78 },
                new int[4] { 0, 51, 102, 153 },
                new int[4] { 79, 79, 79, 79 },
                new int[4] { 100, 100, 100, 100 },
                true));

            SpriteDatabase.AddAnimation(new Animation("Pickle Pepper", pickle, 2,
                new int[2] { 50, 50 },
                new int[2] { 78, 78 },
                new int[2] { 204, 255, },
                new int[2] { 79, 79 },
                new int[2] { 250, 250 }));

            SpriteDatabase.AddAnimation(new Animation("Pickle Dead", pickle, 1,
                new int[1] { 50 },
                new int[1] { 78 },
                new int[1] { 153 },
                new int[1] { 79 },
                new int[1] { 10000 }));

            #endregion

            #region Power Ups

            // Peter Pepper
            Texture2D powerUps = Content.Load<Texture2D>("Images/Power Ups");
            SpriteDatabase.AddAnimation(new Animation("Ice Cream", powerUps, 1,
                new int[1] { 50 },
                new int[1] { 78 },
                new int[1] { 0 },
                new int[1] { 0 },
                new int[1] { 1000 }));

            SpriteDatabase.AddAnimation(new Animation("Coffee", powerUps, 1,
                new int[1] { 50 },
                new int[1] { 78 },
                new int[1] { 50 },
                new int[1] { 0 },
                new int[1] { 1000 }));

            SpriteDatabase.AddAnimation(new Animation("French Fries", powerUps, 1,
                new int[1] { 50 },
                new int[1] { 78 },
                new int[1] { 100 },
                new int[1] { 0 },
                new int[1] { 1000 }));

            #endregion

            #region Pepper

            // Peter Pepper
            Texture2D pepper = Content.Load<Texture2D>("Images/Pepper");
            SpriteDatabase.AddAnimation(new Animation("Pepper", pepper, 4,
                new int[4] { 50, 50, 50, 50 },
                new int[4] { 78, 78, 78, 78 },
                new int[4] { 0, 51, 102, 153 },
                new int[4] { 0, 0, 0, 0 },
                new int[4] { 75, 65, 55, 45 },
                true));

            #endregion

            #region Bottom Bun

            // Bottom Bun
            Texture2D bottomBun = Content.Load<Texture2D>("Images/Bottom Bun");
            SpriteDatabase.AddAnimation(new Animation("Bottom Bun Left", bottomBun, 1,
                new int[1] { 38 },
                new int[1] { 78 },
                new int[1] { 0 },
                new int[1] { 0 },
                new int[1] { 10000 }));

            SpriteDatabase.AddAnimation(new Animation("Bottom Bun Middle Left", bottomBun, 1,
                new int[1] { 38 },
                new int[1] { 78 },
                new int[1] { 37 },
                new int[1] { 0 },
                new int[1] { 10000 }));

            SpriteDatabase.AddAnimation(new Animation("Bottom Bun Middle Right", bottomBun, 1,
                new int[1] { 38 },
                new int[1] { 78 },
                new int[1] { 76 },
                new int[1] { 0 },
                new int[1] { 10000 }));

            SpriteDatabase.AddAnimation(new Animation("Bottom Bun Right", bottomBun, 1,
                new int[1] { 38 },
                new int[1] { 78 },
                new int[1] { 113 },
                new int[1] { 0 },
                new int[1] { 10000 }));

            SpriteDatabase.AddAnimation(new Animation("Bottom Bun Bounce", bottomBun, 4,
                new int[4] { 150, 150, 150, 150 },
                new int[4] { 78, 78, 78, 78 },
                new int[4] { 0, 150, 0, 150 },
                new int[4] { 0, 0, 78, 78 },
                new int[4] { 75, 75, 75, 75 },
                true));

            #endregion

            #region Cheese

            Texture2D cheese = Content.Load<Texture2D>("Images/Cheese");
            SpriteDatabase.AddAnimation(new Animation("Cheese Left", cheese, 1,
                new int[1] { 38 },
                new int[1] { 78 },
                new int[1] { 0 },
                new int[1] { 0 },
                new int[1] { 10000 }));

            SpriteDatabase.AddAnimation(new Animation("Cheese Middle Left", cheese, 1,
                new int[1] { 38 },
                new int[1] { 78 },
                new int[1] { 37 },
                new int[1] { 0 },
                new int[1] { 10000 }));

            SpriteDatabase.AddAnimation(new Animation("Cheese Middle Right", cheese, 1,
                new int[1] { 38 },
                new int[1] { 78 },
                new int[1] { 75 },
                new int[1] { 0 },
                new int[1] { 10000 }));

            SpriteDatabase.AddAnimation(new Animation("Cheese Right", cheese, 1,
                new int[1] { 38 },
                new int[1] { 78 },
                new int[1] { 113 },
                new int[1] { 0 },
                new int[1] { 10000 }));

            SpriteDatabase.AddAnimation(new Animation("Cheese Bounce", cheese, 4,
                new int[4] { 150, 150, 150, 150 },
                new int[4] { 78, 78, 78, 78 },
                new int[4] { 0, 150, 0, 150 },
                new int[4] { 0, 0, 78, 78 },
                new int[4] { 75, 75, 75, 75 },
                true));

            #endregion

            #region Lettuce

            Texture2D lettuce = Content.Load<Texture2D>("Images/Lettuce");
            SpriteDatabase.AddAnimation(new Animation("Lettuce Left", lettuce, 1,
                new int[1] { 38 },
                new int[1] { 78 },
                new int[1] { 0 },
                new int[1] { 0 },
                new int[1] { 10000 }));

            SpriteDatabase.AddAnimation(new Animation("Lettuce Middle Left", lettuce, 1,
                new int[1] { 38 },
                new int[1] { 78 },
                new int[1] { 37 },
                new int[1] { 0 },
                new int[1] { 10000 }));

            SpriteDatabase.AddAnimation(new Animation("Lettuce Middle Right", lettuce, 1,
                new int[1] { 38 },
                new int[1] { 78 },
                new int[1] { 75 },
                new int[1] { 0 },
                new int[1] { 10000 }));

            SpriteDatabase.AddAnimation(new Animation("Lettuce Right", lettuce, 1,
                new int[1] { 38 },
                new int[1] { 78 },
                new int[1] { 113 },
                new int[1] { 0 },
                new int[1] { 10000 }));

            SpriteDatabase.AddAnimation(new Animation("Lettuce Bounce", lettuce, 4,
                new int[4] { 150, 150, 150, 150 },
                new int[4] { 78, 78, 78, 78 },
                new int[4] { 0, 150, 0, 150 },
                new int[4] { 0, 0, 78, 78 },
                new int[4] { 75, 75, 75, 75 },
                true));

            #endregion

            #region Meat

            Texture2D meat = Content.Load<Texture2D>("Images/Meat");
            SpriteDatabase.AddAnimation(new Animation("Meat Left", meat, 1,
                new int[1] { 38 },
                new int[1] { 78 },
                new int[1] { 0 },
                new int[1] { 0 },
                new int[1] { 10000 }));

            SpriteDatabase.AddAnimation(new Animation("Meat Middle Left", meat, 1,
                new int[1] { 38 },
                new int[1] { 78 },
                new int[1] { 37 },
                new int[1] { 0 },
                new int[1] { 10000 }));

            SpriteDatabase.AddAnimation(new Animation("Meat Middle Right", meat, 1,
                new int[1] { 38 },
                new int[1] { 78 },
                new int[1] { 75 },
                new int[1] { 0 },
                new int[1] { 10000 }));

            SpriteDatabase.AddAnimation(new Animation("Meat Right", meat, 1,
                new int[1] { 38 },
                new int[1] { 78 },
                new int[1] { 113 },
                new int[1] { 0 },
                new int[1] { 10000 }));

            SpriteDatabase.AddAnimation(new Animation("Meat Bounce", meat, 4,
                new int[4] { 150, 150, 150, 150 },
                new int[4] { 78, 78, 78, 78 },
                new int[4] { 0, 150, 0, 150 },
                new int[4] { 0, 0, 78, 78 },
                new int[4] { 75, 75, 75, 75 },
                true));

            #endregion

            #region Tomato

            Texture2D tomato = Content.Load<Texture2D>("Images/Tomato");
            SpriteDatabase.AddAnimation(new Animation("Tomato Left", tomato, 1,
                new int[1] { 38 },
                new int[1] { 78 },
                new int[1] { 0 },
                new int[1] { 0 },
                new int[1] { 10000 }));

            SpriteDatabase.AddAnimation(new Animation("Tomato Middle Left", tomato, 1,
                new int[1] { 38 },
                new int[1] { 78 },
                new int[1] { 37 },
                new int[1] { 0 },
                new int[1] { 10000 }));

            SpriteDatabase.AddAnimation(new Animation("Tomato Middle Right", tomato, 1,
                new int[1] { 38 },
                new int[1] { 78 },
                new int[1] { 75 },
                new int[1] { 0 },
                new int[1] { 10000 }));

            SpriteDatabase.AddAnimation(new Animation("Tomato Right", tomato, 1,
                new int[1] { 38 },
                new int[1] { 78 },
                new int[1] { 113 },
                new int[1] { 0 },
                new int[1] { 10000 }));

            SpriteDatabase.AddAnimation(new Animation("Tomato Bounce", tomato, 4,
                new int[4] { 150, 150, 150, 150 },
                new int[4] { 78, 78, 78, 78 },
                new int[4] { 0, 150, 0, 150 },
                new int[4] { 0, 0, 78, 78 },
                new int[4] { 75, 75, 75, 75 },
                true));

            #endregion

            #region Top Bun

            // Top Bun
            Texture2D topBun = Content.Load<Texture2D>("Images/Top Bun");
            SpriteDatabase.AddAnimation(new Animation("Top Bun Left", topBun, 1,
                new int[1] { 38 },
                new int[1] { 78 },
                new int[1] { 0 },
                new int[1] { 0 },
                new int[1] { 10000 }));

            SpriteDatabase.AddAnimation(new Animation("Top Bun Middle Left", topBun, 1,
                new int[1] { 38 },
                new int[1] { 78 },
                new int[1] { 37 },
                new int[1] { 0 },
                new int[1] { 10000 }));

            SpriteDatabase.AddAnimation(new Animation("Top Bun Middle Right", topBun, 1,
                new int[1] { 38 },
                new int[1] { 78 },
                new int[1] { 75 },
                new int[1] { 0 },
                new int[1] { 10000 }));

            SpriteDatabase.AddAnimation(new Animation("Top Bun Right", topBun, 1,
                new int[1] { 38 },
                new int[1] { 78 },
                new int[1] { 113 },
                new int[1] { 0 },
                new int[1] { 10000 }));

            SpriteDatabase.AddAnimation(new Animation("Top Bun Bounce", topBun, 4,
                new int[4] { 150, 150, 150, 150 },
                new int[4] { 78, 78, 78, 78 },
                new int[4] { 0, 150, 0, 150 },
                new int[4] { 0, 0, 78, 78 },
                new int[4] { 75, 75, 75, 75 },
                true));

            #endregion

            #region Plate

            Texture2D plate = Content.Load<Texture2D>("Images/Plate");
            SpriteDatabase.AddAnimation(new Animation("Plate", plate, 1,
                new int[1] { 150 },
                new int[1] { 78 },
                new int[1] { 0 },
                new int[1] { 0 },
                new int[1] { 10000 }));

            #endregion
        }

        /// <summary>
        /// Adds an animation to the database
        /// </summary>
        /// <param name="animation">Animation to add</param>
        /// <returns>Whether or not addition was successful</returns>
        public static bool AddAnimation(Animation animation)
        {
            // If contained already, do not add
            if (animations.ContainsKey(animation.AnimationName))
            {
                return false;
            }

            animations.Add(animation.AnimationName, animation);

            return true;
        }

        /// <summary>
        /// Remove animation from database
        /// </summary>
        /// <param name="animation">Animation to remove</param>
        /// <returns>Whether or not the animation was removed</returns>
        public static bool RemoveAnimation(Animation animation)
        {
            // If not contained, do not remove
            if (!animations.ContainsKey(animation.AnimationName))
            {
                return false;
            }

            animations.Remove(animation.AnimationName);

            return true;
        }

        /// <summary>
        /// Return an animation
        /// </summary>
        /// <param name="animationName">Name of the desired animation</param>
        /// <returns>The animation asked for</returns>
        public static Animation GetAnimation(string animationName)
        {
            // If not contained, return null
            if (!animations.ContainsKey(animationName))
            {
                return null;
            }

            return animations[animationName];
        }

        #endregion
    }
}
