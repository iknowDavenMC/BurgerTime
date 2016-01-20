using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burger_Time
{
    /// <summary>
    /// Class which handles drawing the images associated with a character or object
    /// 
    /// NOTE:
    /// Code is based off of code from my COMP 476 project,
    /// written by Alex Attar, myself, Jeff How and Addison Rodomista
    /// </summary>
    public class DrawingComponent
    {
        #region Attributes

        #region Animation Attributes

        /// <summary>
        /// Animation to be drawn
        /// </summary>
        private Animation animation;

        /// <summary>
        /// Is the animation paused
        /// </summary>
        private bool isPaused;

        /// <summary>
        /// Current frame of the animation
        /// </summary>
        private int currentFrame;

        /// <summary>
        /// Time for that particular frame
        /// </summary>
        private float frameTime;

        /// <summary>
        /// Time used so far for that particular frame
        /// </summary>
        private float frameTimer;

        /// <summary>
        /// Has the animation run from start to finish
        /// </summary>
        private bool isAnimationComplete;

        #endregion

        #region Drawing Attributes

        /// <summary>
        /// Center of the image (image's origin)
        /// </summary>
        private Vector2 center;

        /// <summary>
        /// Position at which to draw the image
        /// </summary>
        private Vector2 position;

        /// <summary>
        /// Scale of the image
        /// </summary>
        private Vector2 scale;

        /// <summary>
        /// Tint of the image
        /// </summary>
        private Color color;

        /// <summary>
        /// Image effects (rotation, flip)
        /// </summary>
        private SpriteEffects spriteEffects;

        /// <summary>
        /// Depth of the image (for drawing order)
        /// </summary>
        private float depth;

        /// <summary>
        /// Image transparency
        /// </summary>
        private float alpha;

        #endregion

        #endregion

        #region Properties

        public Animation Animation
        {
            get { return animation; }
            set { animation = value; }
        }

        public bool IsPaused
        {
            get { return isPaused; }
        }

        public int CurrentFrame
        {
            get { return currentFrame; }
        }

        public float FrameTime
        {
            get { return frameTime; }
        }

        public float FrameTimer
        {
            get { return frameTimer; }
        }

        public bool IsAnimationComplete
        {
            get { return isAnimationComplete; }
        }

        public Vector2 Center
        {
            get { return center; }
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public Vector2 Scale
        {
            get { return scale; }
        }

        public Color Color
        {
            get { return color; }
        }

        public SpriteEffects SpriteEffects
        {
            get { return spriteEffects; }
        }

        public float Depth
        {
            get { return depth; }
        }

        public float Alpha
        {
            get { return alpha; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Basic Constructor
        /// </summary>
        /// <param name="startingAnimation">Animation to start with</param>
        /// <param name="color">Tint of the sprite</param>
        /// <param name="center">Center of the image</param>
        /// <param name="scale">Scale of the image</param>
        /// <param name="depth">Depth of the image</param>
        public DrawingComponent(Animation startingAnimation, Color color, Vector2 scale, float depth)
        {
            // Specified
            animation = startingAnimation;
            this.color = color;
            this.scale = scale;
            this.depth = depth;

            // Not specified
            isPaused = false;
            currentFrame = 0;
            frameTime = 0;
            frameTimer = 0;
            isAnimationComplete = false;
            spriteEffects = SpriteEffects.None;
            alpha = 1;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Called to update what is being drawn
        /// </summary>
        /// <param name="gameTime">Time ellapsed since last call</param>
        /// <param name="spriteEffects">Effects to add to the sprite</param>
        public void Update(GameTime gameTime, SpriteEffects spriteEffects)
        {
            // Set effect
            this.spriteEffects = spriteEffects;

            Update(gameTime);
        }

        /// <summary>
        /// Called to update what is being drawn
        /// </summary>
        /// <param name="gameTime">Time ellapsed since last call</param>
        public void Update(GameTime gameTime)
        {
            // Set time
            frameTime = animation.FrameTimes[currentFrame];

            // If not paused
            if (!isPaused)
            {
                // Update frame
                frameTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (frameTimer > frameTime)
                {
                    frameTimer = 0;
                    ++currentFrame;
                }

                // Check if animation is complete
                if (currentFrame == animation.NumberOfFrames)
                {
                    isAnimationComplete = true;
                    frameTimer = 0;

                    if (animation.IsOneTimer)
                    {
                        currentFrame = animation.NumberOfFrames - 1;
                    }
                    else
                    {
                        currentFrame = 0;
                    }
                }
                else
                {
                    isAnimationComplete = false;
                }
            }
        }

        /// <summary>
        /// Drawing method
        /// </summary>
        /// <param name="position">Position at which to draw the image</param>
        public void Draw(Vector2 position)
        {
            // Set position
            this.position = position;

            // Set drawing information
            Rectangle imageSectionToDraw = new Rectangle(animation.FrameStartingXs[currentFrame], animation.FrameStartingYs[currentFrame],
                animation.FrameWidths[currentFrame], animation.FrameHeights[currentFrame]);

            center = new Vector2(animation.FrameWidths[currentFrame] / 2, animation.FrameHeights[currentFrame] / 2);

            Game1.spriteBatch.Draw(animation.Image, position, imageSectionToDraw, color * alpha, 0, center, scale, spriteEffects, depth);
        }

        /// <summary>
        /// Reset an animation
        /// </summary>
        public void Reset()
        {
            currentFrame = 0;
            frameTimer = 0;
            isAnimationComplete = false;
        }

        /// <summary>
        /// Stop an animation
        /// </summary>
        public void Stop()
        {
            Pause();
            Reset();
        }

        /// <summary>
        /// Play animation
        /// </summary>
        public void Play()
        {
            isPaused = false;
        }

        /// <summary>
        /// Pause animation
        /// </summary>
        public void Pause()
        {
            isPaused = true;
        }

        /// <summary>
        /// Go to previous frame
        /// </summary>
        public void GoToPreviousFrame()
        {
            currentFrame = (currentFrame - 1) % animation.NumberOfFrames;

            while (currentFrame < 0)
            {
                currentFrame += animation.NumberOfFrames;
            }
        }

        #endregion
    }
}
