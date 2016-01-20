using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Burger_Time
{
    public class TopBun : BurgerPart
    {
        #region Constrcutors

        /// <summary>
        /// Basic constructor
        /// </summary>
        /// <param name="position">Position</param>
        /// <param name="x">Radius x</param>
        /// <param name="y">Radius y</param>
        public TopBun(Vector2 position, float x, float y)
            : base(position, x, y)
        {
            artist = new DrawingComponent(SpriteDatabase.GetAnimation("Top Bun Bounce"), Color.White,
                new Vector2(SettingsManager.GetInstance().Scale, SettingsManager.GetInstance().Scale), 1);

            artists = new DrawingComponent[4];
            artists[0] = new DrawingComponent(SpriteDatabase.GetAnimation("Top Bun Left"), Color.White,
                new Vector2(SettingsManager.GetInstance().Scale, SettingsManager.GetInstance().Scale), 1);
            artists[1] = new DrawingComponent(SpriteDatabase.GetAnimation("Top Bun Middle Left"), Color.White,
                new Vector2(SettingsManager.GetInstance().Scale, SettingsManager.GetInstance().Scale), 1);
            artists[2] = new DrawingComponent(SpriteDatabase.GetAnimation("Top Bun Middle Right"), Color.White,
                new Vector2(SettingsManager.GetInstance().Scale, SettingsManager.GetInstance().Scale), 1);
            artists[3] = new DrawingComponent(SpriteDatabase.GetAnimation("Top Bun Right"), Color.White,
                new Vector2(SettingsManager.GetInstance().Scale, SettingsManager.GetInstance().Scale), 1);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Transition from one state to another
        /// </summary>
        /// <param name="newState">State to transition into</param>
        public override void TransitionToState(State newState)
        {
            if (state == newState)
            {
                return;
            }

            state = newState;

            switch (newState)
            {
                case State.Bouncing:

                    artist = new DrawingComponent(SpriteDatabase.GetAnimation("Top Bun Bounce"), Color.White,
                        new Vector2(SettingsManager.GetInstance().Scale, SettingsManager.GetInstance().Scale), 1);

                    break;

                case State.Done:

                    numberOfRiders = 0;
                    numberOfLevels = 0;

                    artist = null;

                    for (int i = 0; i != touched.GetLength(0); ++i)
                    {
                        touched[i] = false;
                    }

                    break;

                case State.Falling:

                    artist = null;

                    lastTile = World.map.MapPosition(position);

                    // Nudge as to avoid another collision
                    position += speed * Vector2.UnitY;

                    for (int i = 0; i != positions.GetLength(0); ++i)
                    {
                        positions[i].Y = position.Y;
                    }

                    // Rider
                    foreach (Enemy enemy in World.enemies)
                    {
                        if (enemy.BoundingBox.Collides(boundingBox) &&
                            enemy.GetState != Enemy.State.Riding &&
                            enemy.GetState != Enemy.State.Dying &&
                            enemy.GetState != Enemy.State.Dead)
                        {
                            enemy.TransitionToState(Enemy.State.Riding);
                            enemy.SetRider(this);
                            ++numberOfRiders;
                            SoundManager.GetInstance().PlaySound("Ride", position.X);
                        }
                    }

                    if (numberOfRiders == 0)
                    {
                        numberOfLevels = 0;
                    }

                    break;

                case State.Static:

                    numberOfRiders = 0;
                    numberOfLevels = 0;

                    artist = null;

                    for (int i = 0; i != touched.GetLength(0); ++i)
                    {
                        touched[i] = false;
                    }

                    break;
            }

            if (artist != null)
            {
                artist.Reset();
            }

            foreach (DrawingComponent draw in artists)
            {
                draw.Reset();
            }
        }

        #endregion
    }
}
