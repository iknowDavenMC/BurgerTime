using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Burger_Time
{
    /// <summary>
    /// Stores all relevant game settings
    /// </summary>
    public class SettingsManager
    {
        #region Attributes

        /// <summary>
        /// Private instance of the manager
        /// </summary>
        private static volatile SettingsManager instance = null;

        /// <summary>
        /// Volume of sound effects
        /// </summary>
        private float volumeSoundEffects;

        /// <summary>
        /// Volume of songs
        /// </summary>
        private float volumeSongs;

        /// <summary>
        /// Game scale
        /// </summary>
        private float scale;

        /// <summary>
        /// Screen Width
        /// </summary>
        private int screenWidthNormal;

        /// <summary>
        /// Screen Height
        /// </summary>
        private int screenHeightNormal;

        /// <summary>
        /// Screen Width Full Screen
        /// </summary>
        private int screenWidthFull;

        /// <summary>
        /// Screen Height Full Screen
        /// </summary>
        private int screenHeightFull;

        /// <summary>
        /// Is the game in full screen
        /// </summary>
        private bool isFullScreen;

        /// <summary>
        /// Is the sound muted
        /// </summary>
        private bool isMute;

        #endregion

        #region Properties

        public float VolumeSoundEffects
        {
            get { return volumeSoundEffects; }
            set { volumeSoundEffects = value; }
        }

        public float VolumeSongs
        {
            get { return volumeSongs; }
            set { volumeSongs = value; }
        }

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public int ScreenWidth
        {
            get
            {
                if (isFullScreen)
                {
                    return screenWidthFull;
                }
                else
                {
                    return screenWidthNormal;
                }
            }
        }

        public int ScreenHeight
        {
            get
            {
                if (isFullScreen)
                {
                    return screenHeightFull;
                }
                else
                {
                    return screenHeightNormal;
                }
            }
        }

        public int ScreenWidthNormal
        {
            get { return screenWidthNormal; }
            set { screenWidthNormal = value; }
        }

        public int ScreenHeightNormal
        {
            get { return screenHeightNormal; }
            set { screenHeightNormal = value; }
        }

        public int ScreenWidthFull
        {
            get { return screenWidthFull; }
            set { screenWidthFull = value; }
        }

        public int ScreenHeightFull
        {
            get { return screenHeightFull; }
            set { screenHeightFull = value; }
        }

        public bool IsFullScreen
        {
            get { return isFullScreen; }
            set { isFullScreen = value; }
        }

        public bool IsMute
        {
            get { return isMute; }
            set { isMute = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Basic constructor
        /// </summary>
        private SettingsManager()
        {
            volumeSoundEffects = 0.25f;
            volumeSongs = 1;

            scale = 0.6f;

            screenWidthNormal = 1024;
            screenHeightNormal = 768;

            screenWidthFull = 1680;
            screenHeightFull = 1050;

            isFullScreen = false;
            isMute = false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Allows the instance to be retrieved. This acts as the constructor
        /// </summary>
        /// <returns>The only instance of settings manager</returns>
        public static SettingsManager GetInstance()
        {
            if (instance == null)
            {
                instance = new SettingsManager();
            }

            return instance;
        }

        /// <summary>
        /// Toggle between window and full screen
        /// </summary>
        /// <returns>The only instance of settings manager</returns>
        public void ToggleFullScreen()
        {
            isFullScreen = !isFullScreen;

            if (isFullScreen)
            {
                scale = 0.8f;
            }
            else
            {
                scale = 0.6f;
            }

            Game1.graphics.PreferredBackBufferWidth = SettingsManager.GetInstance().ScreenWidth;
            Game1.graphics.PreferredBackBufferHeight = SettingsManager.GetInstance().ScreenHeight;
            Game1.graphics.IsFullScreen = SettingsManager.GetInstance().IsFullScreen;
            Game1.graphics.ApplyChanges();

            Game1.Reset();
        }

        /// <summary>
        /// Allows the instance to be retrieved. This acts as the constructor
        /// </summary>
        /// <returns>The only instance of settings manager</returns>
        public void ToggleMute()
        {
            isMute = !isMute;
        }

        #endregion
    }
}
