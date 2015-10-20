using SnakeEater.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeEater
{
    /// <summary>
    /// My Snake Eater Game.
    /// </summary>
    public partial class SnakeGame
    {
        #region event handler
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
            this.tmrForward.Stop();
            this.tmrSpeedCtrl.Stop();
            this.tmrCostTime.Stop();

            this.g.Clear(this.pboxGameZone.BackColor);

            this.StartNewGame();
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

        /// <summary>
        /// About Dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenu_About_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Not implemented yet!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region private method
        /// <summary>
        /// Start a new game.
        /// </summary>
        private void StartNewGame()
        {
            // init relevent variables
            this.toolStripMenu_Pause.Text = "Pause";
            this.toolStripMenu_Pause.Enabled = true;
            this.scoreTotal = 0;
            this.foodCount = 0;
            this.level = 0;
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

            this.tmrCostTime.Start();
            this.tmrForward.Start();
            this.tmrSpeedCtrl.Start();
        }

        /// <summary>
        /// Pause or continue the game.
        /// </summary>
        private void Pause()
        {
            if (this.tmrForward.IsStopped)
            {
                this.tmrCostTime.Start();
                this.tmrForward.Start();
                this.tmrSpeedCtrl.Start();
                this.toolStripMenu_Pause.Text = "Pause";
            }
            else
            {
                this.tmrCostTime.Stop();
                this.tmrForward.Stop();
                this.tmrSpeedCtrl.Stop();
                this.toolStripMenu_Pause.Text = "Play";
            }
        }
        #endregion
    }
}
