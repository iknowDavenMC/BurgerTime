using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burger_Time
{
    /// <summary>
    /// 2D bounding box with debugging tools.
    /// 
    /// NOTE:
    /// Code is based off of code from my COMP 476 project,
    /// written by Alex Attar, myself, Jeff How and Addison Rodomista
    /// </summary>
    public class BoundingRectangle
    {
        #region Attributes

        /// <summary>
        /// Center of the rectangle
        /// </summary>
        private Vector2 center;

        /// <summary>
        /// Dimensions from the center
        /// </summary>
        private Vector2 dimensionsFromCenter;

        /// <summary>
        /// Actual rectangle
        /// </summary>
        private RectangleFloat boundingRectangle;

        /// <summary>
        /// Texture for debugging
        /// </summary>
        private Texture2D image;

        #endregion

        #region Properties

        public RectangleFloat Bounds
        {
            get { return boundingRectangle; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Rectangle style constructor
        /// </summary>
        /// <param name="x">Left most X value</param>
        /// <param name="y">Top most Y value</param>
        /// <param name="width">Width of the box</param>
        /// <param name="height">Height of the box</param>
        public BoundingRectangle(float x, float y, float width, float height)
        {
            center = new Vector2(x + width / 2, y + height / 2);

            boundingRectangle = new RectangleFloat(x, y, width, height);

            dimensionsFromCenter = new Vector2(width / 2, height / 2);

            image = SpriteDatabase.Blank;
        }

        /// <summary>
        /// Bounding sphere style constructor
        /// </summary>
        /// <param name="center">Center of the box</param>
        /// <param name="radius">Radius from center</param>
        public BoundingRectangle(Vector2 center, float radius)
        {
            dimensionsFromCenter = new Vector2(radius, radius);

            this.center = center;

            boundingRectangle = new RectangleFloat(center.X - radius, center.Y - radius, 2 * radius, 2 * radius);

            image = SpriteDatabase.Blank;
        }

        /// <summary>
        /// Bounding sphere style constructor 2
        /// </summary>
        /// <param name="center">Center of the box</param>
        /// <param name="x">X radius from center</param>
        /// <param name="y">Y radius from center</param>
        public BoundingRectangle(Vector2 center, float x, float y)
        {
            dimensionsFromCenter = new Vector2(x, y);

            this.center = center;

            boundingRectangle = new RectangleFloat(center.X - x, center.Y - y, 2 * x, 2 * y);

            image = SpriteDatabase.Blank;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Update the bounding rectangle position
        /// </summary>
        /// <param name="center"></param>
        public void Update(Vector2 center)
        {
            this.center = center;
            boundingRectangle.X = center.X - dimensionsFromCenter.X;
            boundingRectangle.Y = center.Y - dimensionsFromCenter.Y;
        }

        /// <summary>
        /// Checks for collision with other bounding rectangle
        /// </summary>
        /// <param name="rectangle">Bounding rectangle to check collision with</param>
        /// <returns>True if there is a collision</returns>
        public bool Collides(BoundingRectangle rectangle)
        {
            return boundingRectangle.Intersects(rectangle.boundingRectangle);
        }

        /// <summary>
        /// Checks for collision with a rectangle
        /// </summary>
        /// <param name="rectangle">Rectangle to check collision with</param>
        /// <returns>True if there is a collision</returns>
        public bool Collides(RectangleFloat rectangle)
        {
            return boundingRectangle.Intersects(rectangle);
        }

        /// <summary>
        /// Draw the bounding rectangle for debugging purposes
        /// </summary>
        public void Draw()
        {
            Game1.spriteBatch.Draw(image, center + World.map.StartingPosition, null,
                new Color(256, 0, 0, 0.1f), 0, new Vector2(0.5f, 0.5f),
                dimensionsFromCenter * 2,
                SpriteEffects.None, 0);
        }

        #endregion
    }
}