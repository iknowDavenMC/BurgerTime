using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Burger_Time
{
    /// <summary>
    /// Class which handles all sound-related actions
    /// 
    /// NOTE:
    /// Code is based off of code from my COMP 476 project,
    /// written by Alex Attar, myself, Jeff How and Addison Rodomista
    /// </summary>
    public class SoundManager
    {
        #region Attributes

        /// <summary>
        /// Private instance
        /// </summary>
        private static volatile SoundManager instance = null;

        /// <summary>
        /// Mapping of names to sound effects
        /// </summary>
        private Dictionary<string, SoundEffect> soundEffects;

        /// <summary>
        /// Mapping of names to songs
        /// </summary>
        private Dictionary<string, Song> songs;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        private SoundManager()
        {
            soundEffects = new Dictionary<string, SoundEffect>();
            songs = new Dictionary<string, Song>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Allows the instance to be retrieved. This acts as the constructor
        /// </summary>
        /// <param name="type">Type of the controller to be used</param>
        /// <returns>The only instance of input manager</returns>
        public static SoundManager GetInstance()
        {
            if (instance == null)
            {
                instance = new SoundManager();
            }
            
            return instance;
        }

        /// <summary>
        /// Load the songs and sounds
        /// </summary>
        /// <param name="content">Content manager</param>
        public void LoadContent(ContentManager content)
        {
            // Load songs
            songs.Add("Level", content.Load<Song>("Songs/Level"));
            songs.Add("Menu", content.Load<Song>("Songs/Menu"));
            songs.Add("Dead", content.Load<Song>("Songs/Dead"));
            songs.Add("Win", content.Load<Song>("Songs/Win"));

            // Load sound effects
            soundEffects.Add("Bounce", content.Load<SoundEffect>("Sound Effects/Bounce"));
            soundEffects.Add("Dead", content.Load<SoundEffect>("Sound Effects/Dead"));
            soundEffects.Add("Start", content.Load<SoundEffect>("Sound Effects/Game Start"));
            soundEffects.Add("Pepper", content.Load<SoundEffect>("Sound Effects/Pepper"));
            soundEffects.Add("Peppered", content.Load<SoundEffect>("Sound Effects/Peppered"));
            soundEffects.Add("Appearance", content.Load<SoundEffect>("Sound Effects/Power Up Appears"));
            soundEffects.Add("Consumption", content.Load<SoundEffect>("Sound Effects/Power Up Consumed"));
            soundEffects.Add("Ride", content.Load<SoundEffect>("Sound Effects/Ride"));
            soundEffects.Add("Touch", content.Load<SoundEffect>("Sound Effects/Touch"));
        }

        /// <summary>
        /// Plays the sound effect corresponding to the event
        /// </summary>
        /// <param name="soundName">Name of the sound to play</param>
        public bool PlaySound(string soundName)
        {
            float volume;

            if (SettingsManager.GetInstance().IsMute)
            {
                volume = 0;
            }
            else
            {
                volume = SettingsManager.GetInstance().VolumeSoundEffects;
            }

            try
            {
                soundEffects[soundName].Play(volume, 0, 0);

                return true;
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine(soundName + " is not a sound effect.");
                Console.WriteLine(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Plays the sound effect corresponding to the event
        /// </summary>
        /// <param name="soundName">Name of the sound to play</param>
        /// <param name="positionX">X position of the sound</param>
        /// <returns>Whether or not the sound is played</returns>
        public bool PlaySound(string soundName, float positionX)
        {
            float volume;

            if (SettingsManager.GetInstance().IsMute)
            {
                volume = 0;
            }
            else
            {
                volume = SettingsManager.GetInstance().VolumeSoundEffects;
            }

            try
            {
                // Pan correctly
                float screenCenterX = SettingsManager.GetInstance().ScreenWidth / 2;
                float pan = (positionX - screenCenterX) / screenCenterX;

                soundEffects[soundName].Play(volume, 0, pan);

                return true;
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine(soundName + " is not a sound effect.");
                Console.WriteLine(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Plays a song
        /// </summary>
        /// <param name="songName">Name of the song</param>
        /// <param name="isLooped">Should the song be looped</param>
        /// <returns>Does the song exist</returns>
        public bool PlaySong(string songName, bool isLooped)
        {
            if (SettingsManager.GetInstance().IsMute)
            {
                MediaPlayer.IsMuted = true;
            }
            else
            {
                MediaPlayer.IsMuted = false;
            }

            try
            {
                // Stop the previous song
                MediaPlayer.Stop();

                // Play the new song
                MediaPlayer.Play(songs[songName]);

                MediaPlayer.IsRepeating = isLooped;

                return true;
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine(songName + " is not a song.");
                Console.WriteLine(e.Message);
                return false;
            }
        }

        #endregion
    }
}
