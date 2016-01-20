using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Burger_Time
{
    /// <summary>
    /// Abstract parent class of all objects in the game (other than burger parts)
    /// </summary>
    public abstract class Entity
    {
        #region Attributes

        /// <summary>
        /// Position of the entity
        /// </summary>
        protected Vector2 position;

        /// <summary>
        /// Bounding box of the entity
        /// </summary>
        protected BoundingRectangle boundingBox;

        /// <summary>
        /// Handles drawing the entity
        /// </summary>
        protected DrawingComponent artist;

        #endregion

        #region Properties

        public Vector2 Position
        {
            get { return position; }
        }

        public BoundingRectangle BoundingBox
        {
            get { return boundingBox; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Basic Constructor
        /// </summary>
        /// <param name="position">Position of the entity</param>
        /// <param name="x">Radius of the bounding box width</param>
        /// <param name="y">Radius of the bounding box height</param>
        public Entity(Vector2 position, float x, float y)
        {
            this.position = position;
            boundingBox = new BoundingRectangle(position, x, y);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Update, to be defined
        /// </summary>
        /// <param name="gameTime">Game's game time</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Draws the sprite
        /// </summary>
        public virtual void Draw()
        {
            artist.Draw(World.map.StartingPosition + position);

#if (DEBUG)
            {
                boundingBox.Draw();
            }
#endif
        }

        #endregion
    }
}
