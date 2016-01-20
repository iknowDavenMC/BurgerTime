using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Burger_Time
{
    /// <summary>
    /// Sprite sheet manager for one particular animation
    /// 
    /// NOTE:
    /// Code is based off of code from my COMP 476 project,
    /// written by Alex Attar, myself, Jeff How and Addison Rodomista
    /// </summary>
    public class Animation
    {
        #region Attributes

        /// <summary>
        /// Animation name
        /// </summary>
        private string animationName;

        /// <summary>
        /// Image associated with the animation
        /// </summary>
        private Texture2D image;

        /// <summary>
        /// Number of frames in the animation
        /// </summary>
        private int numberOfFrames;

        /// <summary>
        /// Widths of the frames, in pixels
        /// </summary>
        private int[] frameWidths;

        /// <summary>
        /// Heights of the frames, in pixels
        /// </summary>
        private int[] frameHeights;

        /// <summary>
        /// Starting X values of the frames in the spritesheet
        /// </summary>
        private int[] frameStartingXs;

        /// <summary>
        /// Starting Y values of the frames in the spritesheet
        /// </summary>
        private int[] frameStartingYs;

        /// <summary>
        /// Time per frame, in milliseconds
        /// </summary>
        private int[] frameTimes;

        /// <summary>
        /// If the animation should only play once
        /// </summary>
        private bool isOneTimer;

        #endregion

        #region Properties

        public string AnimationName
        {
            get { return animationName; }
        }

        public Texture2D Image
        {
            get { return image; }
        }

        public int NumberOfFrames
        {
            get { return numberOfFrames; }
        }

        public int[] FrameWidths
        {
            get { return frameWidths; }
        }

        public int[] FrameHeights
        {
            get { return frameHeights; }
        }

        public int[] FrameStartingXs
        {
            get { return frameStartingXs; }
        }

        public int[] FrameStartingYs
        {
            get { return frameStartingYs; }
        }

        public int[] FrameTimes
        {
            get { return frameTimes; }
        }

        public bool IsOneTimer
        {
            get { return isOneTimer; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Basic constructor
        /// </summary>
        /// <param name="animationName">Name of animation</param>
        /// <param name="image">Image associated</param>
        /// <param name="numberOfFrames">Number of frames</param>
        /// <param name="frameWidths">Width of each frame</param>
        /// <param name="frameHeights">Height of each frame</param>
        /// <param name="frameStartingXs">X values of top left pixel</param>
        /// <param name="frameStartingYs">Y values of top left pixels</param>
        /// <param name="frameTimes">Time for each frame</param>
        /// <param name="isOneTimer">Does animation play once</param>
        public Animation(string animationName, Texture2D image, int numberOfFrames, int[] frameWidths,
            int[] frameHeights, int[] frameStartingXs, int[] frameStartingYs, int[] frameTimes, bool isOneTimer = false)
        {
            this.animationName = animationName;
            this.image = image;
            this.numberOfFrames = numberOfFrames;
            this.frameWidths = frameWidths;
            this.frameHeights = frameHeights;
            this.frameStartingXs = frameStartingXs;
            this.frameStartingYs = frameStartingYs;
            this.frameTimes = frameTimes;
            this.isOneTimer = isOneTimer;
        }

        #endregion
    }
}
