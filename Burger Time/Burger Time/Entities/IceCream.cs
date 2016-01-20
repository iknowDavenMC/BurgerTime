using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Burger_Time
{
    public class IceCream : PowerUp
    {
        #region Constructors

        /// <summary>
        /// Basic Constructor
        /// </summary>
        /// <param name="position">Position</param>
        /// <param name="x">Radius of the bounding box width</param>
        /// <param name="y">Radius of the bounding box height</param>
        /// <param name="points">Points obtained if consumed</param>
        /// <param name="lifeTime">Time until it dissapears</param>
        public IceCream(Vector2 position, float x, float y, int points, int lifeTime)
            : base(position, x, y, points, lifeTime)
        {
            artist = new DrawingComponent(SpriteDatabase.GetAnimation("Ice Cream"), Color.White,
                new Vector2(SettingsManager.GetInstance().Scale, SettingsManager.GetInstance().Scale), 0);
        }

        #endregion
    }
}
