using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Burger_Time
{
    /// <summary>
    /// Abstract parent class of all power ups in the game
    /// </summary>
    public abstract class PowerUp : Entity
    {
        #region Attributes

        /// <summary>
        /// Points gained by the player if consummed
        /// </summary>
        protected int points;

        /// <summary>
        /// Timer since appearance
        /// </summary>
        protected int lifeTimer;

        /// <summary>
        /// Time that must go by for item to dissapear
        /// </summary>
        protected int lifeTime;

        #endregion

        #region Properties

        public int Points
        {
            get { return points; }
        }

        public bool IsTimeUp
        {
            get { return (lifeTimer >= lifeTime); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Basic constructor
        /// </summary>
        /// <param name="position">Position</param>
        /// <param name="x">Radius of the bounding box width</param>
        /// <param name="y">Radius of the bounding box height</param>
        /// <param name="points">Points awarded to the player if consummed</param>
        /// <param name="lifeTime">Time before the item dissapears</param>
        public PowerUp(Vector2 position, float x, float y, int points, int lifeTime)
            : base (position, x, y)
        {
            this.points = points;

            lifeTimer = 0;
            this.lifeTime = lifeTime;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the power up
        /// </summary>
        /// <param name="gameTime">Game's time</param>
        public override void Update(GameTime gameTime)
        {
            lifeTimer += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            artist.Update(gameTime);
        }

        #endregion
    }
}
