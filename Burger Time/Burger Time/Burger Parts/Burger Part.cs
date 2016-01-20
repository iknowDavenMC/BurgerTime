using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Burger_Time
{
    /// <summary>
    /// Abstract parent class of all burger parts in the game
    /// </summary>
    public abstract class BurgerPart
    {
        #region Enumerations

        public enum State
        { Static, Falling, Bouncing, Done }

        #endregion

        #region Attributes

        /// <summary>
        /// State of the burger part
        /// </summary>
        protected State state;

        /// <summary>
        /// Falling speed
        /// </summary>
        protected float speed;

        /// <summary>
        /// Position of the whole
        /// </summary>
        protected Vector2 position;

        /// <summary>
        /// Position of the parts
        /// </summary>
        protected Vector2[] positions;

        /// <summary>
        /// Bounding box of the whole
        /// </summary>
        protected BoundingRectangle boundingBox;

        /// <summary>
        /// Bounding boxes of the parts
        /// </summary>
        protected BoundingRectangle[] boundingBoxes;

        /// <summary>
        /// Handles drawing the whole
        /// </summary>
        protected DrawingComponent artist;

        /// <summary>
        /// Handles drawing the parts
        /// </summary>
        protected DrawingComponent[] artists;

        /// <summary>
        /// Has each section been touched
        /// </summary>
        protected bool[] touched;

        /// <summary>
        /// Number of enemies riding with burger
        /// </summary>
        protected int numberOfRiders;

        /// <summary>
        /// Number of levels to fall down
        /// </summary>
        protected int numberOfLevels;

        /// <summary>
        /// Last tile that was collided with
        /// </summary>
        protected int[] lastTile;

        #endregion

        #region Properties

        public BoundingRectangle BoundingBox
        {
            get { return boundingBox; }
        }

        public Vector2 Position
        {
            get { return position; }
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
        /// <param name="position">Position of the burger</param>
        /// <param name="x">Radius in x for collision</param>
        /// <param name="y">Radius in y for collision</param>
        public BurgerPart(Vector2 position, float x, float y)
        {
            state = State.Static;

            float scale = SettingsManager.GetInstance().Scale;

            this.position = position;
            boundingBox = new BoundingRectangle(position, x, y);

            speed = 1.65f * scale;

            positions = new Vector2[4];
            positions[0] = this.position - new Vector2(56.5f * scale, 0);
            positions[1] = this.position - new Vector2(19f * scale, 0);
            positions[2] = this.position + new Vector2(19f * scale, 0);
            positions[3] = this.position + new Vector2(56.5f * scale, 0);

            boundingBoxes = new BoundingRectangle[4];
            boundingBoxes[0] = new BoundingRectangle(positions[0], 19 * scale, 15 * scale);
            boundingBoxes[1] = new BoundingRectangle(positions[1], 19 * scale, 15 * scale);
            boundingBoxes[2] = new BoundingRectangle(positions[2], 19 * scale, 15 * scale);
            boundingBoxes[3] = new BoundingRectangle(positions[3], 19 * scale, 15 * scale);

            touched = new bool[4] { false, false, false, false };

            numberOfRiders = 0;
            numberOfLevels = 0;

            lastTile = new int[2] { -50, -50 };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Update the burger
        /// </summary>
        /// <param name="gameTime">Game's time</param>
        public void Update(GameTime gameTime)
        {
            // Update state based on state and behaviour
            switch (state)
            {
                case State.Static:

                    // Peter Pepper touch
                    for (int i = 0; i != touched.GetLength(0); ++i)
                    {
                        if (!touched[i] && World.player.BoundingBox.Collides(boundingBoxes[i]))
                        {
                            touched[i] = true;

                            SoundManager.GetInstance().PlaySound("Touch", positions[i].X);

                            // Lower touched ones
                            for (int j = 0; j != touched.GetLength(0); ++j)
                            {
                                if (touched[j])
                                {
                                    positions[j].Y += 2;
                                    boundingBoxes[j].Update(positions[j]);
                                }
                            }
                        }
                    }

                    // Peter Pepper knock down
                    if (touched[0] && touched[1] && touched[2] && touched[3])
                    {
                        TransitionToState(State.Falling);
                        DataManager.GetInstance().IncreaseScore(50, position);
                        break;
                    }

                    // Burger waterfall
                    foreach (BurgerPart burgerPart in World.burgerParts)
                    {
                        if (burgerPart == this)
                        {
                            continue;
                        }

                        if (boundingBox.Collides(burgerPart.boundingBox))
                        {
                            TransitionToState(State.Falling);
                            DataManager.GetInstance().IncreaseScore(50, position);
                            break;
                        }
                    }

                    break;

                case State.Falling:

                    // Tile hit
                    if (World.map.IsAtTile(position) &&
                        (World.map.MapPosition(position)[0] != lastTile[0] ||
                        World.map.MapPosition(position)[1] != lastTile[1]))
                    {
                        position = World.map.Center(position);
                        boundingBox.Update(position);

                        for (int i = 0; i != boundingBoxes.GetLength(0); ++i)
                        {
                            positions[i].Y = position.Y;
                            boundingBoxes[i].Update(positions[i]);
                        }

                        TransitionToState(State.Bouncing);
                        SoundManager.GetInstance().PlaySound("Bounce", position.X);

                        break;
                    }

                    // Plate hit
                    foreach (Plate plate in World.plates)
                    {
                        if (boundingBox.Collides(plate.BoundingBox))
                        {
                            TransitionToState(State.Bouncing);
                            SoundManager.GetInstance().PlaySound("Bounce", position.X);
                        }
                    }

                    if (state == State.Bouncing)
                    {
                        break;
                    }

                    // Burger part hit
                    foreach (BurgerPart burgerPart in World.burgerParts)
                    {
                        if (burgerPart == this)
                        {
                            continue;
                        }

                        if (boundingBox.Collides(burgerPart.boundingBox))
                        {
                            TransitionToState(State.Bouncing);
                            SoundManager.GetInstance().PlaySound("Bounce", position.X);
                            break;
                        }
                    }

                    break;

                case State.Bouncing:

                    // Bounce is finished
                    if (artist.IsAnimationComplete)
                    {
                        // Plate hit
                        foreach (Plate plate in World.plates)
                        {
                            if (boundingBox.Collides(plate.BoundingBox))
                            {
                                TransitionToState(State.Done);
                                DataManager.GetInstance().IncreaseScore(50, position);
                                break;
                            }
                        }

                        if (state == State.Done)
                        {
                            break;
                        }

                        // Burger on plate hit
                        foreach (BurgerPart burgerPart in World.burgerParts)
                        {
                            if (burgerPart == this)
                            {
                                continue;
                            }

                            if (boundingBox.Collides(burgerPart.boundingBox) && burgerPart.state == State.Done)
                            {
                                TransitionToState(State.Done);
                                DataManager.GetInstance().IncreaseScore(50, position);
                                break;
                            }
                        }

                        if (state == State.Done)
                        {
                            break;
                        }

                        // Free fall
                        if (!World.map.IsAtTile(position))
                        {
                            if (numberOfRiders != 0)
                            {
                                ++numberOfLevels;
                            }

                            TransitionToState(State.Falling);
                            DataManager.GetInstance().IncreaseScore(50, position);
                        }
                        // At tile but riders
                        else if (World.map.IsAtTile(position) && numberOfRiders != 0 && numberOfLevels < numberOfRiders + 2)
                        {
                            ++numberOfLevels;
                            TransitionToState(State.Falling);
                            DataManager.GetInstance().IncreaseScore(50, position);
                        }
                        // At tile and done falling
                        else if (World.map.IsAtTile(position) && (numberOfRiders == 0 || numberOfLevels >= numberOfRiders + 2))
                        {
                            TransitionToState(State.Static);
                        }
                    }
                    else
                    {
                        artist.Update(gameTime);
                    }

                    break;
            }

            // Update position
            if (state == State.Falling)
            {
                position += speed * Vector2.UnitY;
                boundingBox.Update(position);

                for (int i = 0; i != boundingBoxes.GetLength(0); ++i)
                {
                    positions[i].Y = position.Y;
                    boundingBoxes[i].Update(positions[i]);
                }
            }
        }

        /// <summary>
        /// Draw the burger
        /// </summary>
        public void Draw()
        {
            // Draw based on state
            switch (state)
            {
                case State.Static:
                case State.Done:
                case State.Falling:

                    for (int i = 0; i != artists.GetLength(0); ++i)
                    {
                        artists[i].Draw(World.map.StartingPosition + positions[i]);
#if (DEBUG)
                        {
                            boundingBoxes[i].Draw();
                        }
#endif
                    }

                    break;

                case State.Bouncing:

                    artist.Draw(World.map.StartingPosition + position);

#if (DEBUG)
                    {
                        boundingBox.Draw();
                    }
#endif

                    break;
            }
        }

        /// <summary>
        /// Transition from one state to another
        /// </summary>
        /// <param name="newState">State to transition into</param>
        public abstract void TransitionToState(State newState);

        #endregion
    }
}