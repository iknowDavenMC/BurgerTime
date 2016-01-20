using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Burger_Time
{
    public class Pickle : Enemy
    {
        #region Constructors

        /// <summary>
        /// Basic constructor
        /// </summary>
        /// <param name="position">Position</param>
        /// <param name="x">Radius of the bounding box width</param>
        /// <param name="y">Radius of the bounding box height</param>
        /// <param name="points">Points obtained for killing the enemy</param>
        /// <param name="horizontalSpeed">Horizontal movement speed</param>
        /// <param name="verticalSpeed">Vertical movement speed</param>
        /// <param name="fallingSpeed">Falling speed</param>
        public Pickle(Vector2 position, float x, float y, int points, float horizontalSpeed,
            float verticalSpeed, float fallingSpeed)
            : base(position, x, y, points, horizontalSpeed, verticalSpeed, fallingSpeed)
        {
            artist = new DrawingComponent(SpriteDatabase.GetAnimation("Pickle Walk"), Color.White,
                new Vector2(SettingsManager.GetInstance().Scale, SettingsManager.GetInstance().Scale), 0.75f);
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
                case State.Dead:

                    artist.Animation = SpriteDatabase.GetAnimation("Pickle Dead");

                    break;

                case State.Dying:

                    speed = 0;
                    artist.Animation = SpriteDatabase.GetAnimation("Pickle Dying");

                    break;

                case State.Walking:

                    speed = horizontalSpeed;
                    artist.Animation = SpriteDatabase.GetAnimation("Pickle Walk");
                    position.Y = World.map.Center(position).Y;

                    break;

                case State.Ascending:

                    speed = verticalSpeed;
                    artist.Animation = SpriteDatabase.GetAnimation("Pickle Ascension");
                    position.X = World.map.Center(position).X;

                    break;

                case State.Descending:

                    speed = verticalSpeed;
                    artist.Animation = SpriteDatabase.GetAnimation("Pickle Descension");
                    position.X = World.map.Center(position).X;

                    break;

                case State.Pepper:

                    speed = 0;
                    artist.Animation = SpriteDatabase.GetAnimation("Pickle Pepper");

                    break;

                case State.Riding:

                    direction = Vector2.UnitY;
                    artist.Animation = SpriteDatabase.GetAnimation("Pickle Walk");

                    break;

                case State.Start:

                    behaviour = Behaviour.Start;
                    artist.Animation = SpriteDatabase.GetAnimation("Pickle Walk");

                    break;
            }

            artist.Reset();
        }

        #endregion
    }
}
