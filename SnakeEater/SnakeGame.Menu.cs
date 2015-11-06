using SnakeEater.Common;
using SnakeEater.Model;
using SnakeEater.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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

            if (e.KeyCode == Keys.Space)
            {
                this.Pause();
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
            if (!this.tmrForward.IsStopped)
            {
                this.Pause();
            }

            MemoryStream stream = new MemoryStream();
            BinaryFormatter fmt = new BinaryFormatter();
            fmt.Serialize(stream, this.gameData);

            String filePath;
            if (FileUtil.ShowFileSaveDialog(FileType.Data, out filePath))
            {
                FileStream fs = null;
                try
                {
                    fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);

                    // copy the serialized game data file from MemoryStream to FileStream.
                    stream.WriteTo(fs);

                    // save to file.
                    fs.Flush();
                }
                catch (IOException)
                {
                    MessageBox.Show(this, "Archive save error!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Restore game status from file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenu_Restore_Click(object sender, EventArgs e)
        {
            if (!this.tmrForward.IsStopped)
            {
                this.Pause();
            }

            String filePath;
            if (FileUtil.ShowFileSelectDialog(FileType.Data, out filePath))
            {
                FileStream fs = null;
                try
                {
                    fs = new FileStream(filePath, FileMode.Open);
                    BinaryFormatter fmt = new BinaryFormatter();
                    this.gameData = (Game)fmt.Deserialize(fs);

                    // menu
                    this.SetMenuDisplay(this.gameData.Lang);
                    this.txtFoodCount.Text = this.gameData.FoodCount.ToString();
                    this.txtScore.Text = this.gameData.ScoreTotal.ToString();
                    this.txtTime.Text = string.Format(
                        Consts.FormatTime, this.gameData.TimeCountArray[0], this.gameData.TimeCountArray[1], this.gameData.TimeCountArray[2], this.gameData.TimeCountArray[3]);

                    // snake and food
                    this.g.Clear(this.pboxGameZone.BackColor);
                    this.ShowDot(this.gameData.Food, true);
                    foreach (var item in this.gameData.Snake.Body)
                    {
                        this.ShowDot(item, false);
                    }

                    // level
                    this.tmrForward.Interval = Consts.LvInterval[this.gameData.Level];
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "Archive read error!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Close();
                    }
                }
                
            }
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
