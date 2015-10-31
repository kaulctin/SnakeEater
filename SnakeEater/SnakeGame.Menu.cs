using SnakeEater.Common;
using SnakeEater.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
            if (this.gameData.Snake == null)
            {
                return;
            }

            if (this.tmrForward.IsStopped)
            {
                return;
            }

            switch (e.KeyCode)
            {
                case Keys.W:
                case Keys.Up:
                    if (this.gameData.Snake.Direction != Direction.Down)
                    {
                        this.gameData.Snake.Direction = Direction.UP;
                    }
                    break;
                case Keys.A:
                case Keys.Left:
                    if (this.gameData.Snake.Direction != Direction.Right)
                    {
                        this.gameData.Snake.Direction = Direction.Left;
                    }
                    break;
                case Keys.S:
                case Keys.Down:
                    if (this.gameData.Snake.Direction != Direction.UP)
                    {
                        this.gameData.Snake.Direction = Direction.Down;
                    }
                    break;
                case Keys.D:
                case Keys.Right:
                    if (this.gameData.Snake.Direction != Direction.Left)
                    {
                        this.gameData.Snake.Direction = Direction.Right;
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
            if (this.tmrForward.IsStopped)
            {
                return;
            }

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

        /// <summary>
        /// Language changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Language_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string selectedLang = e.ClickedItem.Name.Substring(Consts.LanguageMenuNamePrifix.Length);

            // change the display language.
            bool rst = this.InitializeDisplayLanguage(selectedLang);

            if (rst)
            {
                // write default language into config file.
                Configuration conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                conf.AppSettings.Settings.Remove(Consts.DefaultLangConfigKey);
                conf.AppSettings.Settings.Add(Consts.DefaultLangConfigKey, selectedLang);
                conf.Save(ConfigurationSaveMode.Minimal);
            }
        }
        #endregion

        #region private method
        /// <summary>
        /// Start a new game.
        /// </summary>
        private void StartNewGame()
        {
            // init relevent variables
            this.toolStripMenu_Pause.Text = this.gameData.Lang.Pause;
            this.toolStripMenu_Pause.Enabled = true;
            this.gameData.ScoreTotal = 0;
            this.gameData.FoodCount = 0;
            this.gameData.Level = 0;
            this.tmrForward.Interval = Consts.LvInterval[this.gameData.Level];

            // init the snake
            this.gameData.Snake = new Snake();
            foreach (Dot dot in this.gameData.Snake.Body.AsEnumerable())
            {
                this.ShowDot(dot, false);
            }

            // init the food
            this.gameData.Food = this.GetNextFood();
            this.ShowDot(this.gameData.Food, true);

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
                this.toolStripMenu_Pause.Text = this.gameData.Lang.Pause;
            }
            else
            {
                this.tmrCostTime.Stop();
                this.tmrForward.Stop();
                this.tmrSpeedCtrl.Stop();
                this.toolStripMenu_Pause.Text = this.gameData.Lang.Play;
            }
        }
        #endregion
    }
}
