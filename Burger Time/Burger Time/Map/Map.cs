using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Burger_Time
{
    /// <summary>
    /// Current level map with interaction methods
    /// </summary>
    public class Map
    {
        #region Attributes

        // Constant values
        private const byte EMPTY = 0;
        private const byte LIGHT_FLOOR = 1;
        private const byte DARK_FLOOR = 2;
        private const byte LIGHT_FLOOR_LADDER = 3;
        private const byte DARK_FLOOR_LADDER = 4;
        private const byte LADDER = 5;
        private const int TILE_WIDTH = 50;
        private const int TILE_HEIGHT = 78;
        private const float HORIZONTAL_TOLERANCE = 0.175f;
        private const float VERTICAL_TOLERANCE = 0.175f;

        // Image for each
        private static Texture2D imageEmpty;
        private static Texture2D imageLightFloor;
        private static Texture2D imageDarkFloor;
        private static Texture2D imageLightFloorLadder;
        private static Texture2D imageDarkFloorLadder;
        private static Texture2D imageLadder;

        /// <summary>
        /// Actual map
        /// </summary>
        private byte[,] map;

        /// <summary>
        /// Position of the top left corner of the map
        /// </summary>
        private Vector2 startingPosition;

        /// <summary>
        /// Scaling used
        /// </summary>
        private float scale;

        #endregion

        #region Properties

        public Vector2 StartingPosition
        {
            get { return startingPosition; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Parses the appropriate file to create the map
        /// </summary>
        /// <param name="fileName">Name of the file storing the map</param>
        public Map(string fileName)
        {
            SettingsManager manager = SettingsManager.GetInstance();

            scale = manager.Scale;

            // Set up for reading
            StreamReader reader = new StreamReader(fileName);

            try
            {
                // Get map dimensions
                string line = reader.ReadLine();

                if (line.StartsWith("Map Dimensions"))
                {
                    line = reader.ReadLine();
                }

                int width, height;

                width = int.Parse(line.Split(' ')[0]);
                height = int.Parse(line.Split(' ')[1]);

                map = new byte[height, width];

                // Fill the map
                line = reader.ReadLine();

                if (line.StartsWith("Map Content"))
                {
                    line = reader.ReadLine();
                }

                for (int i = 0; i != height; ++i)
                {
                    string[] content = line.Split(' ');

                    for (int j = 0; j != content.GetLength(0); ++j)
                    {
                        map[i, j] = getValue(content[j]);
                    }

                    line = reader.ReadLine();
                }

                reader.Close();

                startingPosition = new Vector2(
                    (manager.ScreenWidth / 2) - (map.GetLength(1) * (TILE_WIDTH * scale) / 2),
                    HUD.HudBorder.Y);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Returns the byte value associated with a string
        /// </summary>
        /// <param name="stored">String stored in the file</param>
        /// <returns>Byte value to be stored in the map</returns>
        private byte getValue(string stored)
        {
            switch (stored)
            {
                case "E":
                    return EMPTY;

                case "LF":
                    return LIGHT_FLOOR;

                case "DF":
                    return DARK_FLOOR;

                case "LFL":
                    return LIGHT_FLOOR_LADDER;

                case "DFL":
                    return DARK_FLOOR_LADDER;

                case "L":
                    return LADDER;

                default:
                    return EMPTY;
            }
        }

        /// <summary>
        /// Returns the texture to draw
        /// </summary>
        /// <param name="tileValue">Tile being drawn</param>
        /// <returns>Image associated with the tile</returns>
        private Texture2D textureOfTile(byte tileValue)
        {
            switch (tileValue)
            {
                case EMPTY:
                    return imageEmpty;

                case LIGHT_FLOOR:
                    return imageLightFloor;

                case DARK_FLOOR:
                    return imageDarkFloor;

                case LIGHT_FLOOR_LADDER:
                    return imageLightFloorLadder;

                case DARK_FLOOR_LADDER:
                    return imageDarkFloorLadder;

                case LADDER:
                    return imageLadder;

                default:
                    return null;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads the textures
        /// </summary>
        /// <param name="content">Game's content manager</param>
        public static void LoadContent(ContentManager content)
        {
            imageEmpty = content.Load<Texture2D>("Images/Empty");
            imageLightFloor = content.Load<Texture2D>("Images/Light Floor");
            imageDarkFloor = content.Load<Texture2D>("Images/Dark Floor");
            imageLightFloorLadder = content.Load<Texture2D>("Images/Light Floor With Ladder");
            imageDarkFloorLadder = content.Load<Texture2D>("Images/Dark Floor With Ladder");
            imageLadder = content.Load<Texture2D>("Images/Ladder");
        }

        /// <summary>
        /// Draw the map
        /// </summary>
        public void Draw()
        {
            for (int i = 0; i != map.GetLength(0); ++i)
            {
                for (int j = 0; j != map.GetLength(1); ++j)
                {
                    Game1.spriteBatch.Draw(textureOfTile(map[i, j]), startingPosition + new Vector2(j * TILE_WIDTH * scale, i * TILE_HEIGHT * scale),
                        null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 1);
                }
            }
        }

        /// <summary>
        /// Returns the center position of the corresponding tile
        /// </summary>
        /// <param name="position">Position of the character</param>
        /// <returns>Position of the center of the player's tile</returns>
        public Vector2 Center(Vector2 position)
        {
            position.X = (float)Math.Round((position.X - (TILE_WIDTH * scale) / 2f) / (TILE_WIDTH * scale));
            position.Y = (float)Math.Round((position.Y - (TILE_HEIGHT * scale) / 2f) / (TILE_HEIGHT * scale));
            position = new Vector2(
                (position.X * (TILE_WIDTH * scale)) + ((TILE_WIDTH * scale) / 2),
                (position.Y * TILE_HEIGHT * scale) + (TILE_HEIGHT * scale / 2));

            return position;
        }

        /// <summary>
        /// Maps position to the map array
        /// </summary>
        /// <param name="position">Position of the character</param>
        /// <returns>Indexes (in order) of the position on the map</returns>
        public int[] MapPosition(Vector2 position)
        {
            int x = (int)Math.Round((position.X - (TILE_WIDTH * scale) / 2f) / (TILE_WIDTH * scale));
            int y = (int)Math.Round((position.Y - (TILE_HEIGHT * scale) / 2f) / (TILE_HEIGHT * scale));

            return new int[2] { y, x };
        }

        /// <summary>
        /// Returns the top left-most tile in the map
        /// </summary>
        /// <returns>Position of the top left tile</returns>
        public Vector2 TopLeftCorner()
        {
            for (int i = 0; i != map.GetLength(0); ++i)
            {
                for (int j = 0; j != map.GetLength(1); ++j)
                {
                    if (map[i, j] == LIGHT_FLOOR || map[i, j] == DARK_FLOOR || map[i, j] == LIGHT_FLOOR_LADDER || map[i, j] == DARK_FLOOR_LADDER)
                    {
                        return MapIndex(i, j);
                    }
                }
            }

            return Vector2.Zero;
        }

        /// <summary>
        /// Returns the top right-most tile in the map
        /// </summary>
        /// <returns>Position of the top left tile</returns>
        public Vector2 TopRightCorner()
        {
            for (int i = 0; i != map.GetLength(0); ++i)
            {
                for (int j = map.GetLength(1) - 1; j != -1; --j)
                {
                    if (map[i, j] == LIGHT_FLOOR || map[i, j] == DARK_FLOOR || map[i, j] == LIGHT_FLOOR_LADDER || map[i, j] == DARK_FLOOR_LADDER)
                    {
                        return MapIndex(i, j);
                    }
                }
            }

            return Vector2.Zero;
        }

        /// <summary>
        /// Returns the bottom left-most tile in the map
        /// </summary>
        /// <returns>Position of the top left tile</returns>
        public Vector2 BottomLeftCorner()
        {
            for (int i = map.GetLength(0) - 1; i != -1; --i)
            {
                for (int j = 0; j != map.GetLength(1); ++j)
                {
                    if (map[i, j] == LIGHT_FLOOR || map[i, j] == DARK_FLOOR || map[i, j] == LIGHT_FLOOR_LADDER || map[i, j] == DARK_FLOOR_LADDER)
                    {
                        return MapIndex(i, j);
                    }
                }
            }

            return Vector2.Zero;
        }

        /// <summary>
        /// Returns the bottom right-most tile in the map
        /// </summary>
        /// <returns>Position of the top left tile</returns>
        public Vector2 BottomRightCorner()
        {
            for (int i = map.GetLength(0) - 1; i != -1; --i)
            {
                for (int j = map.GetLength(1) - 1; j != -1; --j)
                {
                    if (map[i, j] == LIGHT_FLOOR || map[i, j] == DARK_FLOOR || map[i, j] == LIGHT_FLOOR_LADDER || map[i, j] == DARK_FLOOR_LADDER)
                    {
                        return MapIndex(i, j);
                    }
                }
            }

            return Vector2.Zero;
        }

        /// <summary>
        /// Maps index in map to position relative to the top left corner of the map
        /// </summary>
        /// <param name="y">Index of first dimension (y value)</param>
        /// <param name="x">Index of second dimension (x value)</param>
        /// <returns>Position relative to the map</returns>
        public Vector2 MapIndex(int y, int x)
        {
            Vector2 position = new Vector2(
                (x * (TILE_WIDTH * scale)) + ((TILE_WIDTH * scale) / 2),
                (y * TILE_HEIGHT * scale) + (TILE_HEIGHT * scale / 2));

            return position;
        }

        /// <summary>
        /// Can the player move up
        /// </summary>
        /// <param name="position">Position of the player</param>
        /// <returns>Whether or not the player can move up</returns>
        public bool CanMoveUp(Vector2 position)
        {
            int x = (int)Math.Round((position.X - (TILE_WIDTH * scale) / 2f) / (TILE_WIDTH * scale));
            int y = (int)Math.Round((position.Y - (TILE_HEIGHT * scale) / 2f) / (TILE_HEIGHT * scale));
            int nextY = (int)Math.Floor((position.Y - (TILE_HEIGHT * scale) / 2f) / (TILE_HEIGHT * scale));

            // If exactly in the middle of a tile, must check next tile
            if (Math.Abs(y - ((position.Y - (TILE_HEIGHT * scale) / 2f) / (TILE_HEIGHT * scale))) < 0.001)
            {
                nextY = y - 1;
            }

            // If out of bounds, return false
            if (x < 0 || x >= map.GetLength(1) || y < 0 || y >= map.GetLength(0) || nextY < 0)
            {
                return false;
            }

            // If tile has ladder and next tile has ladder and centered, return true
            if ((map[y, x] == LADDER || map[y, x] == LIGHT_FLOOR_LADDER || map[y, x] == DARK_FLOOR_LADDER) &&
                (map[nextY, x] == LADDER || map[nextY, x] == LIGHT_FLOOR_LADDER || map[nextY, x] == DARK_FLOOR_LADDER) &&
                Math.Abs(x - ((position.X - (TILE_WIDTH * scale) / 2f) / (TILE_WIDTH * scale))) < HORIZONTAL_TOLERANCE)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Can the player move down
        /// </summary>
        /// <param name="position">Position of the player</param>
        /// <returns>Whether or not the player can move down</returns>
        public bool CanMoveDown(Vector2 position)
        {
            int x = (int)Math.Round((position.X - (TILE_WIDTH * scale) / 2f) / (TILE_WIDTH * scale));
            int y = (int)Math.Round((position.Y - (TILE_HEIGHT * scale) / 2f) / (TILE_HEIGHT * scale));
            int nextY = (int)Math.Ceiling((position.Y - (TILE_HEIGHT * scale) / 2f) / (TILE_HEIGHT * scale));

            // If exactly in the middle of a tile, must check next tile
            if (Math.Abs(y - ((position.Y - (TILE_HEIGHT * scale) / 2f) / (TILE_HEIGHT * scale))) < 0.001)
            {
                nextY = y + 1;
            }

            // If out of bounds, return false
            if (x < 0 || x >= map.GetLength(1) || y < 0 || y >= map.GetLength(0) || nextY >= map.GetLength(0))
            {
                return false;
            }

            // If tile has ladder and next tile has ladder, return true
            if ((map[y, x] == LADDER || map[y, x] == LIGHT_FLOOR_LADDER || map[y, x] == DARK_FLOOR_LADDER) &&
                (map[nextY, x] == LADDER || map[nextY, x] == LIGHT_FLOOR_LADDER || map[nextY, x] == DARK_FLOOR_LADDER) &&
                Math.Abs(x - ((position.X - (TILE_WIDTH * scale) / 2f) / (TILE_WIDTH * scale))) < HORIZONTAL_TOLERANCE)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Can the player move left
        /// </summary>
        /// <param name="position">Position of the player</param>
        /// <returns>Whether or not the player can move left</returns>
        public bool CanMoveLeft(Vector2 position)
        {
            int x = (int)Math.Round((position.X - (TILE_WIDTH * scale) / 2f) / (TILE_WIDTH * scale));
            int y = (int)Math.Round((position.Y - (TILE_HEIGHT * scale) / 2f) / (TILE_HEIGHT * scale));
            int nextX = (int)Math.Floor((position.X - (TILE_WIDTH * scale) / 2f) / (TILE_WIDTH * scale));

            // If exactly in the middle of a tile, must check next tile
            if (Math.Abs(x - ((position.X - (TILE_WIDTH * scale) / 2f) / (TILE_WIDTH * scale))) < 0.001)
            {
                nextX = x - 1;
            }

            // If out of bounds, return false
            if (x < 0 || x >= map.GetLength(1) || y < 0 || y >= map.GetLength(0) || nextX < 0)
            {
                return false;
            }

            // If tile has ladder and next tile has ladder, return true
            if ((map[y, x] == LIGHT_FLOOR || map[y, x] == DARK_FLOOR || map[y, x] == LIGHT_FLOOR_LADDER || map[y, x] == DARK_FLOOR_LADDER) &&
                (map[y, nextX] == LIGHT_FLOOR || map[y, nextX] == DARK_FLOOR || map[y, nextX] == LIGHT_FLOOR_LADDER || map[y, nextX] == DARK_FLOOR_LADDER) &&
                Math.Abs(y - ((position.Y - (TILE_HEIGHT * scale) / 2f) / (TILE_HEIGHT * scale))) < VERTICAL_TOLERANCE)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Can the player move right
        /// </summary>
        /// <param name="position">Position of the player</param>
        /// <returns>Whether or not the player can move right</returns>
        public bool CanMoveRight(Vector2 position)
        {
            int x = (int)Math.Round((position.X - (TILE_WIDTH * scale) / 2f) / (TILE_WIDTH * scale));
            int y = (int)Math.Round((position.Y - (TILE_HEIGHT * scale) / 2f) / (TILE_HEIGHT * scale));
            int nextX = (int)Math.Ceiling((position.X - (TILE_WIDTH * scale) / 2f) / (TILE_WIDTH * scale));

            // If exactly in the middle of a tile, must check next tile
            if (Math.Abs(x - ((position.X - (TILE_WIDTH * scale) / 2f) / (TILE_WIDTH * scale))) < 0.001)
            {
                nextX = x + 1;
            }

            // If out of bounds, return false
            if (x < 0 || x >= map.GetLength(1) || y < 0 || y >= map.GetLength(0) || nextX >= map.GetLength(1))
            {
                return false;
            }

            // If tile has ladder and next tile has ladder, return true
            if ((map[y, x] == LIGHT_FLOOR || map[y, x] == DARK_FLOOR || map[y, x] == LIGHT_FLOOR_LADDER || map[y, x] == DARK_FLOOR_LADDER) &&
                (map[y, nextX] == LIGHT_FLOOR || map[y, nextX] == DARK_FLOOR || map[y, nextX] == LIGHT_FLOOR_LADDER || map[y, nextX] == DARK_FLOOR_LADDER) &&
                Math.Abs(y - ((position.Y - (TILE_HEIGHT * scale) / 2f) / (TILE_HEIGHT * scale))) < VERTICAL_TOLERANCE)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Is the caller currently at a tile
        /// </summary>
        /// <param name="position">Position of the player</param>
        /// <returns>Whether or not the player can move up</returns>
        public bool IsAtTile(Vector2 position)
        {
            int x = (int)Math.Round((position.X - (TILE_WIDTH * scale) / 2f) / (TILE_WIDTH * scale));
            int y = (int)Math.Round((position.Y - (TILE_HEIGHT * scale) / 2f) / (TILE_HEIGHT * scale));

            // If out of bounds, return false
            if (x < 0 || x >= map.GetLength(1) || y < 0 || y >= map.GetLength(0))
            {
                return false;
            }

            // If tile, return true
            if ((map[y, x] == LIGHT_FLOOR || map[y, x] == DARK_FLOOR || map[y, x] == LIGHT_FLOOR_LADDER || map[y, x] == DARK_FLOOR_LADDER) &&
                Math.Abs(y - ((position.Y - (TILE_HEIGHT * scale) / 2f) / (TILE_HEIGHT * scale))) < VERTICAL_TOLERANCE / 10)
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}
