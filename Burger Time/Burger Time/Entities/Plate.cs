using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Burger_Time
{
    public class Plate : Entity
    {
        /// <summary>
        /// Basic constructor
        /// </summary>
        /// <param name="position">Position</param>
        /// <param name="x">Collision x radius</param>
        /// <param name="y">Collision y radius</param>
        public Plate(Vector2 position, float x, float y)
            : base(position, x, y)
        {
            artist = new DrawingComponent(SpriteDatabase.GetAnimation("Plate"), Color.White,
                new Vector2(SettingsManager.GetInstance().Scale, SettingsManager.GetInstance().Scale), 1);
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
