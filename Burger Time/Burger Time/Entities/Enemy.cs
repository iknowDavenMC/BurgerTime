using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Burger_Time
{
    /// <summary>
    /// Abstract parent class of all enemies in the game
    /// </summary>
    public abstract class Enemy : Entity
    {
        #region Enumerations

        public enum Behaviour
        { Start, SeekLadder, SeekTile }

        public enum State
        { Start, Pepper, Dying, Dead, Riding, Ascending, Descending, Walking }

        #endregion

        #region Attributes

        /// <summary>
        /// AI aspect of the character
        /// </summary>
        protected Behaviour behaviour;

        /// <summary>
        /// Update state of the character
        /// </summary>
        protected State state;

        /// <summary>
        /// Points gained by the player if killed
        /// </summary>
        protected int points;

        /// <summary>
        /// Direction moving
        /// </summary>
        protected Vector2 direction;

        /// <summary>
        /// Used speed
        /// </summary>
        protected float speed;

        /// <summary>
        /// Horizontal Speed
        /// </summary>
        protected float horizontalSpeed;

        /// <summary>
        /// Vertical Speed
        /// </summary>
        protected float verticalSpeed;

        /// <summary>
        /// Falling Speed
        /// </summary>
        protected float fallingSpeed;

        /// <summary>
        /// Last target point
        /// </summary>
        protected int[] lastTarget;

        /// <summary>
        /// Timer since pepper attack
        /// </summary>
        protected int pepperTimer;

        /// <summary>
        /// Time that must go by for pepper attack to end
        /// </summary>
        protected int pepperTime;

        /// <summary>
        /// Burger part riding on
        /// </summary>
        protected BurgerPart ride;

        #endregion

        #region Properties

        public int Points
        {
            get { return points; }
        }

        public State GetState
        {
            get { return state; }
        }

        #endregion

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
        public Enemy(Vector2 position, float x, float y, int points, float horizontalSpeed,
            float verticalSpeed, float fallingSpeed)
            : base(position, x, y)
        {
            this.points = points;
            this.horizontalSpeed = horizontalSpeed;
            this.verticalSpeed = verticalSpeed;
            this.fallingSpeed = fallingSpeed;

            state = State.Start;
            behaviour = Behaviour.Start;

            pepperTimer = 0;
            pepperTime = 2500;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Update the character
        /// </summary>
        /// <param name="gameTime">Game's time</param>
        public override void Update(GameTime gameTime)
        {
            // Update state based on state and behaviour
            switch (state)
            {
                case State.Dead:

                    break;

                case State.Dying:

                    if (artist.IsAnimationComplete)
                    {
                        TransitionToState(State.Dead);
                    }

                    break;

                case State.Ascending:

                    switch (behaviour)
                    {
                        case Behaviour.SeekTile:

                            bool canMoveUp = World.map.CanMoveUp(position);
                            bool canMoveDown = World.map.CanMoveDown(position);
                            bool canMoveLeft = World.map.CanMoveLeft(position);
                            bool canMoveRight = World.map.CanMoveRight(position);

                            // If at dead end, turn around
                            if (!canMoveUp && canMoveDown && !canMoveLeft && !canMoveRight)
                            {
                                direction = -direction;
                                TransitionToState(State.Descending);
                                lastTarget = new int[2] { -5, -5 };
                            }

                            // If found a tile, move towards the player
                            else if ((canMoveLeft || canMoveRight) &&
                                !(World.map.MapPosition(position)[0] == lastTarget[0]
                                && World.map.MapPosition(position)[1] == lastTarget[1]))
                            {
                                // If player is to the left and there is a platform, move left
                                if (World.player.Position.X < position.X && canMoveLeft)
                                {
                                    direction = -Vector2.UnitX;
                                }
                                // If player is to the right and there is a platform, move right
                                else if (World.player.Position.X > position.X && canMoveRight)
                                {
                                    direction = Vector2.UnitX;
                                }
                                // If there is a platform, move right
                                else if (canMoveRight)
                                {
                                    direction = Vector2.UnitX;
                                }
                                // If there is a platform, move left
                                else
                                {
                                    direction = -Vector2.UnitX;
                                }

                                speed = horizontalSpeed;
                                behaviour = Behaviour.SeekLadder;
                                lastTarget = World.map.MapPosition(position);
                                TransitionToState(State.Walking);
                            }

                            break;
                    }

                    break;

                case State.Descending:

                    switch(behaviour)
                    {
                        case Behaviour.SeekTile:

                            bool canMoveUp = World.map.CanMoveUp(position);
                            bool canMoveDown = World.map.CanMoveDown(position);
                            bool canMoveLeft = World.map.CanMoveLeft(position);
                            bool canMoveRight = World.map.CanMoveRight(position);

                            // If at dead end, turn around
                            if (canMoveUp && !canMoveDown && !canMoveLeft && !canMoveRight)
                            {
                                direction = -direction;
                                TransitionToState(State.Ascending);
                                lastTarget = new int[2] { -5, -5 };
                            }

                            // If found a tile, move towards the player
                            else if ((canMoveLeft || canMoveRight) &&
                                !(World.map.MapPosition(position)[0] == lastTarget[0]
                                && World.map.MapPosition(position)[1] == lastTarget[1]))
                            {
                                // If player is to the left and there is a platform, move left
                                if (World.player.Position.X < position.X && canMoveLeft)
                                {
                                    direction = -Vector2.UnitX;
                                }
                                // If player is to the right and there is a platform, move right
                                else if (World.player.Position.X > position.X && canMoveRight)
                                {
                                    direction = Vector2.UnitX;
                                }
                                // If there is a platform, move right
                                else if (canMoveRight)
                                {
                                    direction = Vector2.UnitX;
                                }
                                // If there is a platform, move left
                                else
                                {
                                    direction = -Vector2.UnitX;
                                }

                                speed = horizontalSpeed;
                                behaviour = Behaviour.SeekLadder;
                                lastTarget = World.map.MapPosition(position);
                                TransitionToState(State.Walking);
                            }

                            break;
                    }

                    break;

                case State.Walking:

                    switch (behaviour)
                    {
                        case Behaviour.SeekLadder:

                            bool canMoveUp = World.map.CanMoveUp(position);
                            bool canMoveDown = World.map.CanMoveDown(position);
                            bool canMoveLeft = World.map.CanMoveLeft(position);
                            bool canMoveRight = World.map.CanMoveRight(position);

                            // If at dead end, turn around
                            if ((direction == -Vector2.UnitX && !canMoveUp && !canMoveDown && !canMoveLeft && canMoveRight) ||
                                (direction == Vector2.UnitX && !canMoveUp && !canMoveDown && canMoveLeft && !canMoveRight))
                            {
                                direction = -direction;
                                lastTarget = new int[2] { -5, -5 };
                            }
                            // If at ladder, move towards the player
                            else if ((canMoveUp || canMoveDown) &&
                                !(World.map.MapPosition(position)[0] == lastTarget[0]
                                && World.map.MapPosition(position)[1] == lastTarget[1]))
                            {
                                // If player is above and there is a ladder going up, move up
                                if (World.player.Position.Y < position.Y && canMoveUp)
                                {
                                    direction = -Vector2.UnitY;
                                }
                                // If player is below and there is a ladder going down, move down
                                else if (World.player.Position.Y > position.Y && canMoveDown)
                                {
                                    direction = Vector2.UnitY;
                                }
                                // If there is a ladder going down, move down
                                else if (canMoveDown)
                                {
                                    direction = Vector2.UnitY;
                                }
                                // If there is a ladder going up, move up
                                else
                                {
                                    direction = -Vector2.UnitY;
                                }

                                speed = verticalSpeed;
                                behaviour = Behaviour.SeekTile;
                                lastTarget = World.map.MapPosition(position);

                                if (direction == Vector2.UnitY)
                                {
                                    TransitionToState(State.Descending);
                                }
                                else
                                {
                                    TransitionToState(State.Ascending);
                                }
                            }

                            break;
                    }

                    break;

                case State.Start:

                    switch (behaviour)
                    {
                        case Behaviour.Start:

                            lastTarget = new int[2] { -50, -50 };

                            // Move based on first available option

                            if (World.map.CanMoveUp(position))
                            {
                                direction = -Vector2.UnitY;
                                speed = verticalSpeed;
                                behaviour = Behaviour.SeekTile;
                                TransitionToState(State.Ascending);
                            }
                            else if (World.map.CanMoveDown(position))
                            {
                                direction = Vector2.UnitY;
                                speed = verticalSpeed;
                                behaviour = Behaviour.SeekTile;
                                TransitionToState(State.Descending);
                            }
                            else if (World.map.CanMoveLeft(position))
                            {
                                direction = -Vector2.UnitX;
                                speed = horizontalSpeed;
                                behaviour = Behaviour.SeekLadder;
                                TransitionToState(State.Walking);
                            }
                            else if (World.map.CanMoveRight(position))
                            {
                                direction = Vector2.UnitX;
                                speed = horizontalSpeed;
                                behaviour = Behaviour.SeekLadder;
                                TransitionToState(State.Walking);
                            }

                            break;
                    }

                    break;

                case State.Pepper:

                    pepperTimer += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

                    // If done being peppered
                    if (pepperTimer > pepperTime)
                    {
                        pepperTimer = 0;
                        TransitionToState(State.Start);
                    }

                    break;

                case State.Riding:

                    speed = fallingSpeed;
                    position.Y = ride.Position.Y;

                    // If burger part is done falling, die
                    if (ride.GetState == BurgerPart.State.Static ||
                        ride.GetState == BurgerPart.State.Done)
                    {
                        DataManager.GetInstance().IncreaseScore(500, position);
                        TransitionToState(State.Dying);
                    }

                    break;
            }

            // Update position
            position += speed * direction;
            boundingBox.Update(position);

            // Update orientation and drawing
            if (direction == -Vector2.UnitX)
            {
                artist.Update(gameTime, Microsoft.Xna.Framework.Graphics.SpriteEffects.FlipHorizontally);
            }
            else if (direction == Vector2.UnitX)
            {
                artist.Update(gameTime, Microsoft.Xna.Framework.Graphics.SpriteEffects.None);
            }
            else
            {
                artist.Update(gameTime);
            }
        }

        /// <summary>
        /// Transitions to a new state, setting everything correctly for it
        /// </summary>
        /// <param name="newState">State to switch to</param>
        public abstract void TransitionToState(State newState);

        /// <summary>
        /// Sets the enemy's burger part ride
        /// </summary>
        /// <param name="burger"></param>
        public void SetRider(BurgerPart burger)
        {
            ride = burger;
        }

        #endregion
    }
}
