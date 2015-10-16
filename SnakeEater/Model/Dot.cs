using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeEater.Model
{
    public class Dot
    {
        #region Const
        /// <summary>
        /// Side length.
        /// </summary>
        public const int SIDE_LENGTH = 10;
        #endregion

        #region Properties
        /// <summary>
        /// The size of this dot.
        /// </summary>
        public Size Size
        {
            get;
            private set;
        }

        /// <summary>
        /// Relative position of this dot.
        /// </summary>
        public Point Point
        {
            get;
            private set;
        }

        /// <summary>
        /// Pixel position of this dot.
        /// </summary>
        public Point PointReal
        {
            get;
            private set;
        }
        #endregion

        #region Constructor
        public Dot() : this(0, 0)
        {
        }

        public Dot(int x, int y)
        {
            this.Size = new Size(SIDE_LENGTH, SIDE_LENGTH);
            
            this.Point = new Point(x, y);
            this.PointReal = new Point(x * SIDE_LENGTH, y * SIDE_LENGTH);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Dot a, Dot b)
        {
            if (null == (object)a)
            {
                if (null == (object)b)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (null == (object)b)
                {
                    return false;
                }
                else
                {
                    return a.Point.X == b.Point.X &&
                        a.Point.Y == b.Point.Y;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Dot a, Dot b)
        {
            return !(a == b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is Dot)
            {
                return this.Point.X == ((Dot)obj).Point.X &&
                    this.Point.Y == ((Dot)obj).Point.Y;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return 100 * Point.X + Point.Y;
        }
        #endregion
    }
}
