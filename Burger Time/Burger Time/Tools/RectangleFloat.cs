using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Burger_Time
{
    /// <summary>
    /// Rectangle which allows for floats
    /// 
    /// NOTE:
    /// Code is based off of code from my COMP 476 project,
    /// written by Alex Attar, myself, Jeff How and Addison Rodomista
    /// </summary>
    public class RectangleFloat
    {
        #region Attributes

        /// <summary>
        /// X value of top left corner
        /// </summary>
        public float X;

        /// <summary>
        /// Y value of top left corner
        /// </summary>
        public float Y;

        /// <summary>
        /// Width
        /// </summary>
        public float Width;

        /// <summary>
        /// Height
        /// </summary>
        public float Height;

        #endregion

        #region Properties

        public float Left
        {
            get { return (Width >= 0 ? X : X + Width); }
        }
            
        public float Right
        {
            get { return (Width >= 0 ? X + Width : X); }
        }

        public float Top
        {
            get { return (Height >= 0 ? Y : Y + Height); }
        }

        public float Bottom
        {
            get { return (Height >= 0 ? Y + Height : Y); }
        }
        
        public Vector2 Center
        {
            get { return new Vector2(X + Width / 2, Y + Height / 2); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Empty rectangle
        /// </summary>
        public RectangleFloat()
        {
            X = 0; Y = 0; Width = 0; Height = 0;
        }

        /// <summary>
        /// Standard constructor
        /// </summary>
        /// <param name="x">X value of top left corner</param>
        /// <param name="y">Y value of top left corner</param>
        /// <param name="width">Width of the rectangle</param>
        /// <param name="height">Height of the rectangle</param>
        public RectangleFloat(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="rect">Other rectangle</param>
        public RectangleFloat(RectangleFloat rect)
        {
            X = rect.X;
            Y = rect.Y;
            Width = rect.Width;
            Height = rect.Height;
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Retuns an empty rectangle
        /// </summary>
        public static RectangleFloat Empty()
        {
            return new RectangleFloat();
        }

        /// <summary>
        /// Checks rectangles for intersection
        /// </summary>
        /// <param name="rect1">First rectangle</param>
        /// <param name="rect2">Second rectangle</param>
        /// <returns>Area of intersection</returns>
        public static RectangleFloat Intersect(RectangleFloat rect1, RectangleFloat rect2)
        {
            RectangleFloat result;

            if (rect1.Contains(rect2))
            {
                result = new RectangleFloat(rect2);
            }
            else if (rect2.Contains(rect1))
            {
                result = new RectangleFloat(rect1);
            }
            else if (!rect1.Intersects(rect2))
            {
                result = new RectangleFloat();
            }
            else
            {
                result = new RectangleFloat(
                    Math.Max(rect1.X, rect2.X),
                    Math.Max(rect1.Y, rect2.Y),
                    Math.Min(rect1.Right, rect2.Right) - Math.Max(rect1.Left, rect2.Left),
                    Math.Min(rect1.Bottom, rect2.Bottom) - Math.Max(rect1.Top, rect2.Top));
            }
            return result;
        }

        /// <summary>
        /// Checks rectangles for intersection
        /// </summary>
        /// <param name="rect1">First rectangle</param>
        /// <param name="rect2">Second rectangle</param>
        /// <param name="result">Area of intersection</param>
        public static void Intersect(ref RectangleFloat rect1, ref RectangleFloat rect2, out RectangleFloat result)
        {
            if (rect1.Contains(rect2))
            {
                result = new RectangleFloat(rect2);
            }
            else if (rect2.Contains(rect1))
            {
                result = new RectangleFloat(rect1);
            }
            else if (!rect1.Intersects(rect2))
            {
                result = new RectangleFloat();
            }
            else
            {
                result = new RectangleFloat(
                    Math.Max(rect1.X, rect2.X),
                    Math.Max(rect1.Y, rect2.Y),
                    Math.Min(rect1.Right, rect2.Right) - Math.Max(rect1.Left, rect2.Left),
                    Math.Min(rect1.Bottom, rect2.Bottom) - Math.Max(rect1.Top, rect2.Top));
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Is the rectangle empty
        /// </summary>
        /// <returns>Is the rectangle empty</returns>
        public bool IsEmpty()
        {
            return (X == 0 && Y == 0 && Width == 0 && Height == 0);
        }

        /// <summary>
        /// If rectangle contains a point
        /// </summary>
        /// <param name="x">X value of the point</param>
        /// <param name="y">Y value of the point</param>
        /// <returns>If rectangle contains a point</returns>
        public bool Contains(float x, float y)
        {
            return (x >= Left && x <= Right && y >= Top && y <= Bottom);
        }

        /// <summary>
        /// If rectangle contains a point
        /// </summary>
        /// <param name="point">Point</param>
        /// <returns>If rectangle contains a point</returns>
        public bool Contains(Vector2 point)
        {
            return (point.X >= Left && point.X <= Right && point.Y >= Top && point.Y <= Bottom);
        }

        /// <summary>
        /// If rectangle contains a point
        /// </summary>
        /// <param name="point">Point</param>
        /// <param name="result">If rectangle contains a point</param>
        public void Contains(ref Vector2 point, out bool result)
        {
            result = (point.X >= Left && point.X <= Right && point.Y >= Top && point.Y <= Bottom);
        }

        /// <summary>
        /// If rectangle contains another rectangle
        /// </summary>
        /// <param name="rectangle">Other rectangle</param>
        /// <returns>If rectangle contains another rectangle</returns>
        public bool Contains(RectangleFloat rectangle)
        {
            return (rectangle.Left >= Left && rectangle.Right <= Right && rectangle.Top >= Top && rectangle.Bottom <= Bottom);
        }

        /// <summary>
        /// If rectangle contains another rectangle
        /// </summary>
        /// <param name="rectangle">Other rectangle</param>
        /// <param name="result">If rectangle contains another rectangle</result>
        public void Contains(ref RectangleFloat rectangle, out bool result)
        {
            result = rectangle.Left >= Left && rectangle.Right <= Right && rectangle.Top >= Top && rectangle.Bottom <= Bottom;
        }

        /// <summary>
        /// Compares rectangles for equality
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            RectangleFloat rect = obj as RectangleFloat;

            if (rect != null)
            {
                return (X == rect.X && Y == rect.Y && Width == rect.Width && Height == rect.Height);
            }

            return base.Equals(obj);
        }

        /// <summary>
        /// Checks for intersection between two rectangles
        /// </summary>
        /// <param name="rect">Other rectangle</param>
        /// <returns>Whether or not the two rectangles intersect</returns>
        public bool Intersects(RectangleFloat rect)
        {
            if (rect.Right < Left || rect.Left > Right || rect.Bottom < Top || rect.Top > Bottom)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
