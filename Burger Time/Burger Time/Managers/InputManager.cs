using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Burger_Time
{
    /// <summary>
    /// Manages all input in the game
    /// 
    /// NOTE:
    /// Code is based off of code from my COMP 476 project,
    /// but the code was written entirely by me
    /// </summary>
    public class InputManager
    {
        #region Attributes

        /// <summary>
        /// Private instance
        /// </summary>
        private static volatile InputManager instance = null;
        
        /// <summary>
        /// States of the keyboard
        /// </summary>
        private KeyboardState keyboardState;

        /// <summary>
        /// Mapping of keyboard keys to actions
        /// </summary>
        private Dictionary<String, Keys[]> keyboardMapping;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        private InputManager()
        {
            keyboardMapping = new Dictionary<string, Keys[]>();

            keyboardMapping.Add("Play", new Keys[1] { Keys.Enter });
            keyboardMapping.Add("Pause", new Keys[2] { Keys.Pause, Keys.P });
            keyboardMapping.Add("Screen", new Keys[1] { Keys.F });
            keyboardMapping.Add("Sound", new Keys[1] { Keys.M });
            keyboardMapping.Add("Quit", new Keys[1] { Keys.Escape });

            keyboardMapping.Add("Down", new Keys[1] { Keys.Down });
            keyboardMapping.Add("Up", new Keys[1] { Keys.Up });
            keyboardMapping.Add("Right", new Keys[1] { Keys.Right });
            keyboardMapping.Add("Left", new Keys[1] { Keys.Left });
            keyboardMapping.Add("Pepper", new Keys[1] { Keys.Space });
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Allows the instance to be retrieved. This acts as the constructor
        /// </summary>
        /// <returns>The only instance of input manager</returns>
        public static InputManager GetInstance()
        {
            if (instance == null)
            {
                instance = new InputManager();
            }
            
            return instance;
        }

        /// <summary>
        /// Update method
        /// </summary>
        /// <param name="gameTime">Current game time</param>
        public void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
        }

        /// <summary>
        /// Returns whether or not the player is performing the given action
        /// </summary>
        /// <param name="key">Name of the action to check for</param>
        /// <returns>Whether or not the action exists</returns>
        public bool IsDoing(string key)
        {
            try
            {
                for (int i = 0; i != instance.keyboardMapping[key].GetLength(0); ++i)
                {
                    if (keyboardState.IsKeyDown(instance.keyboardMapping[key][i]))
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        #endregion
    }
}
