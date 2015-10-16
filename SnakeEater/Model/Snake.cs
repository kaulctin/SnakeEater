using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Marching direction of the snake.
/// </summary>
public enum Direction
{
    UP = 0,
    Left,
    Down,
    Right
};

namespace SnakeEater.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Snake
    {
        #region Properties
        /// <summary>
        /// The direction which the snake is heading to.
        /// </summary>
        public Direction Direction;

        /// <summary>
        /// The snake's body.
        /// A linkedlist of the Dot that makes up the snake's body.
        /// </summary>
        public LinkedList<Dot> Body;
        #endregion

        #region Constructor
        /// <summary>
        /// default initializer
        /// </summary>
        public Snake() : this(Direction.Right)
        {
        }

        /// <summary>
        /// a snake with a direction.
        /// </summary>
        /// <param name="d">initial direction</param>
        public Snake(Direction d)
        {
            this.Direction = d;

            this.Body = new LinkedList<Dot>();
            for (int i = 3; i >= 0; i--)
            {
                Dot newD = new Dot(i, 0);
                this.Body.AddLast(newD);
            }
        }
        #endregion
    }
}
