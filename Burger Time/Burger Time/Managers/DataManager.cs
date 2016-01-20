using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burger_Time
{
    /// <summary>
    /// Singleton class which stores all game information
    /// 
    /// NOTE:
    /// Code is based off of code from my COMP 476 project,
    /// written by Alex Attar, myself, Jeff How and Addison Rodomista
    /// </summary>
    public class DataManager
    {
        #region Attributes

        /// <summary>
        /// Private instance of the manager
        /// </summary>
        private static volatile DataManager instance = null;

        /// <summary>
        /// Number of lives the player has
        /// </summary>
        private int lives;

        /// <summary>
        /// Player score
        /// </summary>
        private int score;

        /// <summary>
        /// Player score since life gain
        /// </summary>
        private int scoreSinceLife;

        /// <summary>
        /// All time high score
        /// </summary>
        private int highScore;

        /// <summary>
        /// Number of pepper attacks
        /// </summary>
        private int pepper;

        /// <summary>
        /// Game level
        /// </summary>
        private int level;

        /// <summary>
        /// Number of levels
        /// </summary>
        private int numberOfLevels;

        #endregion

        #region Properties

        public int Lives
        {
            get { return lives; }
        }

        public int Score
        {
            get { return score; }
        }

        public int HighScore
        {
            get { return highScore; }
        }

        public int Pepper
        {
            get { return pepper; }
        }

        public int Level
        {
            get { return level; }
        }

        public int NumberOfLevels
        {
            get { return numberOfLevels; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        private DataManager()
        {
            Reset();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Allows the instance to be retrieved. This acts as the constructor
        /// </summary>
        /// <returns>The only instance of data manager</returns>
        public static DataManager GetInstance()
        {
            if (instance == null)
            {
                instance = new DataManager();
            }

            return instance;
        }

        /// <summary>
        /// Reset all values
        /// </summary>
        public void Reset()
        {
            lives = 3;

            highScore = int.Parse(System.IO.File.ReadAllText("Files/HighScore.txt"));

            score = 0;

            scoreSinceLife = 0;

            pepper = 5;

            level = 1;

            numberOfLevels = 6;
        }

        /// <summary>
        /// Player loses a life
        /// </summary>
        /// <param name="amount"></param>
        public void LoseALife()
        {
            if (lives > 0)
            {
                --lives;
            }
        }

        /// <summary>
        /// Gain a life
        /// </summary>
        public void GainALife()
        {
            ++lives;
        }

        /// <summary>
        /// Increase the score
        /// </summary>
        /// <param name="amount">Amount to increase by</param>
        public void IncreaseScore(int amount, Vector2 position)
        {
            score += amount;
            scoreSinceLife += amount;

            PopUpHandler.AddPopUp(position + World.map.StartingPosition, amount, 250);

            if (scoreSinceLife >= 20000)
            {
                scoreSinceLife -= 20000;
                GainALife();
            }
        }

        /// <summary>
        /// Use a pepper attack
        /// </summary>
        public void UsePepper()
        {
            if (pepper > 0)
            {
                --pepper;
            }
        }

        /// <summary>
        /// Gain a pepper attack
        /// </summary>
        public void IncreasePepper()
        {
            ++pepper;
        }

        /// <summary>
        /// Go up a level
        /// </summary>
        public void IncreaseLevel()
        {
            ++level;
        }

        /// <summary>
        /// Save high score to file
        /// </summary>
        public void SaveHighScore()
        {
            // Write the string to a file.
            if (score > highScore)
            {
                lock (this)
                {
                    highScore = score;
                    System.IO.StreamWriter file = new System.IO.StreamWriter("HighScore.txt");
                    file.WriteLine(highScore);
                    file.Close();
                }
            }
        }

        #endregion
    }
}
