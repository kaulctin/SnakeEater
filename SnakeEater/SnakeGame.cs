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
    public partial class SnakeGame : Form
    {
        #region Consts
        /// <summary>
        /// The max x position of snake or food.
        /// </summary>
        public const int MAX_X = 64;

        /// <summary>
        /// The max y position of snake or food.
        /// </summary>
        public const int MAX_Y = 40;
        #endregion

        #region Properties
        /// <summary>
        /// The time that the game has being on.
        /// </summary>
        private TimeSpan ellaspedTime;

        /// <summary>
        /// The number of foods that are eaten.
        /// </summary>
        private int foodCount;

        /// <summary>
        /// Game is paused or not.
        /// </summary>
        private bool isGamePaused;

        /// <summary>
        /// Current hard level.
        /// </summary>
        private int level = 0;

        /// <summary>
        /// The array of the interval for each level.
        /// </summary>
        private int[] lvInterval = { 300, 250, 200, 150, 100 };

        /// <summary>
        /// Total scores.
        /// </summary>
        private int scoreTotal;

        /// <summary>
        /// The array of the scores for each level.
        /// </summary>
        private int[] scorePerFood = { 10, 12, 16, 25, 50 };

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

        private Random rand;
        #endregion

        #region Constructor
        /// <summary>
        /// 
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

        #region Event
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SnakeGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.snake == null)
            {
                return;
            }

            switch (e.KeyCode)
            {
                case Keys.W:
                case Keys.Up:
                    if (this.snake.Direction != Direction.Down)
                    {
                        this.snake.Direction = Direction.UP;
                    }
                    break;
                case Keys.A:
                case Keys.Left:
                    if (this.snake.Direction != Direction.Right)
                    {
                        this.snake.Direction = Direction.Left;
                    }
                    break;
                case Keys.S:
                case Keys.Down:
                    if (this.snake.Direction != Direction.UP)
                    {
                        this.snake.Direction = Direction.Down;
                    }
                    break;
                case Keys.D:
                case Keys.Right:
                    if (this.snake.Direction != Direction.Left)
                    {
                        this.snake.Direction = Direction.Right;
                    }
                    break;
                case Keys.Space:
                    this.Pause();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenu_NewGame_Click(object sender, EventArgs e)
        {

            this.g.Clear(this.pboxGameZone.BackColor);

            this.StartNewGame();
        }

        /// <summary>
        /// The event that increase the hard level of the game each time the Speed-Control-Timer is triggered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmrSpeedCtrl_Tick(object sender, EventArgs e)
        {
            if (this.level <= 4)
            {
                this.level++;
            }

            this.tmrForward.Interval = this.lvInterval[this.level];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmrForward_Tick(object sender, EventArgs e)
        {
            bool isAlive = this.Forward();
            if (!isAlive)
            {
                // game is over
                this.tmrForward.Stop();
                this.tmrSpeedCtrl.Stop();

                MessageBox.Show(this, "Game is over......", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrCostTime_Tick(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenu_Pause_Click(object sender, EventArgs e)
        {
            this.Pause();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenu_Save_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Not implemented yet!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenu_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Start a new game.
        /// </summary>
        private void StartNewGame()
        {
            this.toolStripMenu_Pause.Text = "开始";
            //this.button1.Text = "开始";
            this.tmrForward.Stop();
            this.tmrSpeedCtrl.Stop();
            this.isGamePaused = true;
            this.foodCount = 0;
            this.level = 0;
            this.scoreTotal = 0;
            this.tmrForward.Interval = this.lvInterval[this.level];

            // init the snake
            this.snake = new Snake();
            foreach (Dot dot in this.snake.Body.AsEnumerable())
            {
                this.ShowDot(dot, false);
            }

            // init the food
            this.food = this.GetNextFood();
            this.ShowDot(this.food, true);
        }

        /// <summary>
        /// 蛇动作的主要函数。
        /// 
        /// 控制蛇身前进一步，并判断是否吃到食物，或者撞到墙、自己的身体。
        /// 吃到食物则身体增长一点，游戏继续；
        /// 撞到墙、自己的身体则游戏结束。
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
                x = this.rand.Next(0, MAX_X);
                y = this.rand.Next(0, MAX_Y);
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
            if (newHead.Point.X < 0 || newHead.Point.X >= MAX_X ||
                newHead.Point.Y < 0 || newHead.Point.Y >= MAX_Y)
            {
                return true;
            }

            if (this.snake.Body.Contains(newHead))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        private void Pause()
        {
            if (this.isGamePaused)
            {
                this.isGamePaused = false;
                this.tmrSpeedCtrl.Start();
                this.tmrForward.Start();
                //this.button1.Text = "暂停";
                this.toolStripMenu_Pause.Text = "暂停";
            }
            else
            {
                this.isGamePaused = true;
                this.tmrSpeedCtrl.Stop();
                this.tmrForward.Stop();
                //this.button1.Text = "继续";
                this.toolStripMenu_Pause.Text = "继续";
            }
        }

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
    }
}
