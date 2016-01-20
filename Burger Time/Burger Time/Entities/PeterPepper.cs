using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Burger_Time
{
    /// <summary>
    /// Player's character
    /// </summary>
    public class PeterPepper : Entity
    {
        #region Enumerations

        public enum State
        { StaticWalk, StaticClimb, Walking, Climbing, Pepper, Dying, Dead }

        #endregion

        #region Attributes

        /// <summary>
        /// Update state of the character
        /// </summary>
        protected State state;

        /// <summary>
        /// Direction moving
        /// </summary>
        protected Vector2 movementDirection;

        /// <summary>
        /// Direction facing
        /// </summary>
        protected Vector2 faceDirection;

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
        /// Input timer
        /// </summary>
        protected float inputTimer = 0;

        /// <summary>
        /// Time required between input reads
        /// </summary>
        protected float inputTime = 100;

        /// <summary>
        /// Bounding rectangle for pepper attack
        /// </summary>
        protected Pepper pepper;

        #endregion

        #region Properties

        public Pepper Pepper
        {
            get { return pepper; }
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
        /// <param name="x">Radius X of bounding box</param>
        /// <param name="y">Radius Y of bounding box</param>
        /// <param name="horizontalSpeed">Horizontal movement speed</param>
        /// <param name="verticalSpeed">Vertical movement speed</param>
        public PeterPepper(Vector2 position, float x, float y, float horizontalSpeed, float verticalSpeed)
            : base(position, x, y)
        {
            this.horizontalSpeed = horizontalSpeed;
            this.verticalSpeed = verticalSpeed;

            state = State.Walking;
            movementDirection = Vector2.Zero;
            faceDirection = Vector2.UnitX;
            speed = 0;

            artist = new DrawingComponent(SpriteDatabase.GetAnimation("Peter Pepper Walk"), Color.White,
                new Vector2(SettingsManager.GetInstance().Scale, SettingsManager.GetInstance().Scale), 0);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handle user input
        /// </summary>
        private void handleInput()
        {
            // Get input manager instance
            InputManager input = InputManager.GetInstance();

            // If doing pepper attack
            if (input.IsDoing("Pepper") && state != State.Pepper && DataManager.GetInstance().Pepper != 0)
            {
                TransitionToState(State.Pepper);
                SoundManager.GetInstance().PlaySound("Pepper", position.X);
            }
            // Else check movement
            else
            {
                if (input.IsDoing("Up") && World.map.CanMoveUp(position))
                {
                    movementDirection = -Vector2.UnitY;
                    faceDirection = movementDirection;
                    speed = verticalSpeed;
                    TransitionToState(State.Climbing);
                }
                if (input.IsDoing("Down") && World.map.CanMoveDown(position))
                {
                    movementDirection = Vector2.UnitY;
                    faceDirection = movementDirection;
                    speed = verticalSpeed;
                    TransitionToState(State.Climbing);
                }
                if (input.IsDoing("Left") && World.map.CanMoveLeft(position))
                {
                    movementDirection = -Vector2.UnitX;
                    faceDirection = movementDirection;
                    speed = horizontalSpeed;
                    TransitionToState(State.Walking);
                }
                if (input.IsDoing("Right") && World.map.CanMoveRight(position))
                {
                    movementDirection = Vector2.UnitX;
                    faceDirection = movementDirection;
                    speed = horizontalSpeed;
                    TransitionToState(State.Walking);
                }
            }
        }

        /// <summary>
        /// Create a pepper attack
        /// </summary>
        private void createPepper()
        {
            float scale = SettingsManager.GetInstance().Scale;

            if (faceDirection == Vector2.UnitX)
            {
                pepper = new Pepper(position + new Vector2(boundingBox.Bounds.Width * 2, 0), 25 * scale, 39 * scale, 0, 300);
            }
            else if (faceDirection == -Vector2.UnitX)
            {
                pepper = new Pepper(position - new Vector2(boundingBox.Bounds.Width * 2, 0), 25 * scale, 39 * scale, 0, 300);
            }
            else if (faceDirection == Vector2.UnitY)
            {
                pepper = new Pepper(position + new Vector2(0, boundingBox.Bounds.Height * 2), 25 * scale, 39 * scale, 0, 300);
            }
            else
            {
                pepper = new Pepper(position - new Vector2(0, boundingBox.Bounds.Height * 2), 25 * scale, 39 * scale, 0, 300);
            }
        }

        /// <summary>
        /// Update based on user input and state
        /// </summary>
        /// <param name="gameTime">Game's time</param>
        private void updateState(GameTime gameTime)
        {
            // Reset direction
            movementDirection = Vector2.Zero;

            // Update existing pepper
            if (pepper != null)
            {
                pepper.Update(gameTime);

                if (pepper.IsTimeUp)
                {
                    pepper = null;
                }
            }

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

                case State.Walking:

                    // Check input
                    handleInput();

                    // If no movement, static
                    if (movementDirection == Vector2.Zero && inputTimer > inputTime)
                    {
                        TransitionToState(State.StaticWalk);
                    }

                    break;

                case State.Climbing:

                    // Check input
                    handleInput();

                    // If no movement, static
                    if (movementDirection == Vector2.Zero && inputTimer > inputTime)
                    {
                        TransitionToState(State.StaticClimb);
                    }

                    break;

                case State.StaticWalk:

                    // Check input
                    handleInput();

                    // If no movement, static
                    if (movementDirection != Vector2.Zero)
                    {
                        if ((int)Math.Round(Math.Abs(movementDirection.X)) == 1)
                        {
                            TransitionToState(State.Walking);
                        }
                        else
                        {
                            TransitionToState(State.Climbing);
                        }
                    }

                    break;

                case State.StaticClimb:

                    // Check input
                    handleInput();

                    // If no movement, static
                    if (movementDirection != Vector2.Zero)
                    {
                        if ((int)Math.Round(Math.Abs(movementDirection.X)) == 1)
                        {
                            TransitionToState(State.Walking);
                        }
                        else
                        {
                            TransitionToState(State.Climbing);
                        }
                    }

                    break;

                case State.Pepper:

                    // Done with pepper animation
                    if (artist.IsAnimationComplete)
                    {
                        createPepper();

                        DataManager.GetInstance().UsePepper();

                        if (faceDirection == Vector2.UnitY || faceDirection == -Vector2.UnitY)
                        {
                            TransitionToState(State.Climbing);
                        }
                        else
                        {
                            TransitionToState(State.Walking);
                        }
                    }

                    break;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Update the player
        /// </summary>
        /// <param name="gameTime">Game's time</param>
        public override void Update(GameTime gameTime)
        {
            inputTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            // Update state
            updateState(gameTime);

            // Update position
            position += speed * movementDirection;
            boundingBox.Update(position);

            // Update orientation and drawing
            if (faceDirection == -Vector2.UnitX)
            {
                artist.Update(gameTime, Microsoft.Xna.Framework.Graphics.SpriteEffects.FlipHorizontally);
            }
            else if (faceDirection == Vector2.UnitX)
            {
                artist.Update(gameTime, Microsoft.Xna.Framework.Graphics.SpriteEffects.None);
            }
            else
            {
                artist.Update(gameTime);
            }

            if (inputTimer > inputTime)
            {
                inputTimer = 0;
            }
        }

        /// <summary>
        /// Draws the sprite
        /// </summary>
        public override void Draw()
        {
            if (pepper != null)
            {
                pepper.Draw();
            }

            base.Draw();
        }

        /// <summary>
        /// Transition from one state to another
        /// </summary>
        /// <param name="newState">State to transition into</param>
        public void TransitionToState(State newState)
        {
            if (state == newState)
            {
                return;
            }

            state = newState;

            switch (newState)
            {
                case State.Climbing:

                    artist.Animation = SpriteDatabase.GetAnimation("Peter Pepper Climb");
                    position.X = World.map.Center(position).X;
                    artist.Play();

                    break;

                case State.Dead:

                    artist.Animation = SpriteDatabase.GetAnimation("Peter Pepper Dead");
                    artist.Play();

                    break;

                case State.Dying:

                    artist.Animation = SpriteDatabase.GetAnimation("Peter Pepper Dying");
                    artist.Play();

                    break;

                case State.Pepper:

                    artist.Animation = SpriteDatabase.GetAnimation("Peter Pepper Pepper");
                    artist.Play();

                    break;

                case State.StaticClimb:

                    artist.Pause();

                    break;

                case State.StaticWalk:

                    artist.Animation = SpriteDatabase.GetAnimation("Peter Pepper Static Walk");
                    artist.Play();

                    break;

                case State.Walking:

                    artist.Animation = SpriteDatabase.GetAnimation("Peter Pepper Walk");
                    position.Y = World.map.Center(position).Y;
                    artist.Play();

                    break;
            }

            artist.Reset();
        }

        #endregion
    }
}
