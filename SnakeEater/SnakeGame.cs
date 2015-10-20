﻿using SnakeEater.Common;
using SnakeEater.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeEater
{
    /// <summary>
    /// My Snake Eater Game.
    /// </summary>
    public partial class SnakeGame : Form
    {
        #region Variables
        /// <summary>
        /// The number of foods that are eaten.
        /// </summary>
        private int foodCount;

        /// <summary>
        /// Current hard level.
        /// </summary>
        private int level = 0;

        /// <summary>
        /// The array of the interval for each level.
        /// </summary>
        private int[] lvInterval = { 300, 250, 200, 150, 100, 80, 50 };

        /// <summary>
        /// Total scores.
        /// </summary>
        private int scoreTotal;

        /// <summary>
        /// The array of the scores for each level.
        /// </summary>
        private int[] scorePerFood = { 10, 12, 16, 25, 50, 80, 100 };

        /// <summary>
        /// The food.
        /// </summary>
        private Dot food;

        /// <summary>
        /// The snake.
        /// </summary>
        private Snake snake;

        /// <summary>
        /// 
        /// </summary>
        private Graphics g;

        /// <summary>
        /// Pen for drawing.
        /// </summary>
        private Brush pen;

        /// <summary>
        /// Pen for erasing.
        /// </summary>
        private Brush penErase;

        /// <summary>
        /// Random number generator.
        /// </summary>
        private Random rand;

        /// <summary>
        /// Array that stores total gaming time. Each stands for: hours, minutes, seconds, 0.1 seconds.
        /// </summary>
        private int[] timeCountArray = { 0, 0, 0, 0 };
        #endregion

        #region Constructor
        /// <summary>
        /// Initialize a SnakeGame instance.
        /// </summary>
        public SnakeGame()
        {
            InitializeComponent();

            // initialize the GDI+ controls
            this.g = this.pboxGameZone.CreateGraphics();
            this.pen = new SolidBrush(Color.Black);
            this.penErase = new SolidBrush(this.pboxGameZone.BackColor);
            this.rand = new Random();
        }
        #endregion

        #region Timer ticks
        /// <summary>
        /// The event that increase the hard level of the game each time the Speed-Control-Timer is triggered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmrSpeedCtrl_Tick(object sender, EventArgs e)
        {
            if (this.level == this.lvInterval.Length - 1)
            {
                // the most hard level reached.
                return;
            }

            this.tmrForward.Interval = this.lvInterval[this.level];
        }

        /// <summary>
        /// Make the snake move a step further.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmrForward_Tick(object sender, EventArgs e)
        {
            bool isAlive = this.Forward();
            if (!isAlive)
            {
                // game is over
                this.tmrCostTime.Stop();
                this.tmrForward.Stop();
                this.tmrSpeedCtrl.Stop();

                MessageBox.Show(this, "Game is over......", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        /// <summary>
        /// Updates the total gaming time.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmrCostTime_Tick(object sender, EventArgs e)
        {
            this.timeCountArray[3]++;
            if (this.timeCountArray[3] == 10)
            {
                this.timeCountArray[3] = 0;
                this.AddOneSecond();
            }

            this.txtTime.Text = string.Format(Consts.FormatTime, this.timeCountArray[0], this.timeCountArray[1], this.timeCountArray[2], this.timeCountArray[3]);
        }
        #endregion

        #region Movement Control
        /// <summary>
        /// Controls the snake to move a step forward. 
        /// <para>>The game is over If the wall or its own body is hit.</para>
        /// <para>>The snake grows a 'dot' if a food is eaten.</para>
        /// </summary>
        /// <returns>False if hit the wall or self's body, otherwise true is returned.</returns>
        private bool Forward()
        {
            Dot nextHead = this.GetNextHead();
            if (this.IsWallOrBodyHit(nextHead))
            {
                return false;
            }

            if (this.IsFoodEaten(nextHead))
            {
                // food eaten, body length +1
                this.foodCount++;
                this.txtFoodCount.Text = this.foodCount.ToString();
                this.snake.Body.AddFirst(nextHead);
                this.ShowDot(nextHead, false);

                // add score
                this.scoreTotal += this.scorePerFood[this.level];
                this.txtScore.Text = this.scoreTotal.ToString();

                // get next food
                this.food = this.GetNextFood();
                this.ShowDot(this.food, true);
            }
            else
            {
                // body length remains unchanged
                this.ShowDot(nextHead, false);
                this.FadeDot(this.snake.Body.Last.Value);
                this.snake.Body.AddFirst(nextHead);
                this.snake.Body.RemoveLast();
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Dot GetNextHead()
        {
            Dot h = this.snake.Body.First.Value;
            int x, y;
            switch (this.snake.Direction)
            {
                case Direction.Right:
                    x = h.Point.X + 1;
                    y = h.Point.Y;
                    break;
                case Direction.Down:
                    x = h.Point.X;
                    y = h.Point.Y + 1;
                    break;
                case Direction.Left:
                    x = h.Point.X - 1;
                    y = h.Point.Y;
                    break;
                case Direction.UP:
                    x = h.Point.X;
                    y = h.Point.Y - 1;
                    break;
                default:
                    x = h.Point.X + 1;
                    y = h.Point.Y;
                    break;
            }

            return new Dot(x, y);
        }

        /// <summary>
        /// Generates the next food instance.
        /// </summary>
        /// <returns>The instance of next food</returns>
        private Dot GetNextFood()
        {
            int x, y;
            Dot d;
            do
            {
                x = this.rand.Next(0, Consts.MAX_X);
                y = this.rand.Next(0, Consts.MAX_Y);
                d = new Dot(x, y);
            } while (this.snake.Body.Contains(d));

            return d;
        }

        /// <summary>
        /// Test if the snake hits the food.
        /// </summary>
        /// <param name="newHead">newHead</param>
        /// <returns>True if the snake head is at the same position with the food.</returns>
        private bool IsFoodEaten(Dot newHead)
        {
            return this.food.Equals(newHead);
        }

        /// <summary>
        /// Test if the snake hits the wall.
        /// </summary>
        /// <param name="newHead"></param>
        /// <returns>True if it hits the wall.</returns>
        private bool IsWallOrBodyHit(Dot newHead)
        {
            if (newHead.Point.X < 0 || newHead.Point.X >= Consts.MAX_X ||
                newHead.Point.Y < 0 || newHead.Point.Y >= Consts.MAX_Y)
            {
                return true;
            }

            if (this.snake.Body.Contains(newHead))
            {
                return true;
            }

            return false;
        }
        #endregion

        #region Display Control
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dot"></param>
        /// <param name="isFood"></param>
        private void ShowDot(Dot dot, bool isFood)
        {
            if (isFood)
            {
                g.FillEllipse(this.pen, dot.PointReal.X, dot.PointReal.Y, dot.Size.Width, dot.Size.Height);                
            }
            else
            {
                g.FillRectangle(this.pen, dot.PointReal.X, dot.PointReal.Y, dot.Size.Width, dot.Size.Height);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dot"></param>
        private void FadeDot(Dot dot)
        {
            g.FillRectangle(this.penErase, dot.PointReal.X, dot.PointReal.Y, dot.Size.Width, dot.Size.Height);
        }
        #endregion

        #region Gaming time Control
        /// <summary>
        /// Add one second to the total gaming time.
        /// </summary>
        private void AddOneSecond()
        {
            this.timeCountArray[2]++;
            if (this.timeCountArray[2] == 60)
            {
                this.timeCountArray[2] = 0;
                this.AddOneMinute();
            }
        }

        /// <summary>
        /// Add one minute to the total gaming time.
        /// </summary>
        private void AddOneMinute()
        {
            this.timeCountArray[1]++;
            if (this.timeCountArray[1] == 60)
            {
                this.timeCountArray[1] = 0;
                this.AddOneHour();
            }
        }

        /// <summary>
        /// Add one hour to the total gaming time.
        /// </summary>
        private void AddOneHour()
        {
            this.timeCountArray[0]++;
        }
        #endregion
    }
}
