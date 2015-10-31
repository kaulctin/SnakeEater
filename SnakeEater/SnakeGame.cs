using SnakeEater.Common;
using SnakeEater.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SnakeEater
{
    /// <summary>
    /// My Snake Eater Game.
    /// </summary>
    public partial class SnakeGame : Form
    {
        #region Variables
        /// <summary>
        /// Game's data class.
        /// </summary>
        private Game gameData;

        /// <summary>
        /// Random number generator.
        /// </summary>
        private Random rand;

        #region GDI
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
        #endregion
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

            this.gameData = new Game();
        }
        #endregion

        /// <summary>
        /// Load event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SnakeGame_Load(object sender, EventArgs e)
        {
            // Initialize the language drop down menus.
            this.InitializeLanguageMenu();

            // Initialize the facade's display language.
            this.InitializeDisplayLanguage(ConfigurationManager.AppSettings.Get(Consts.DefaultLangConfigKey));
        }

        #region Timer ticks
        /// <summary>
        /// The event that increase the hard level of the game each time the Speed-Control-Timer is triggered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmrSpeedCtrl_Tick(object sender, EventArgs e)
        {
            if (this.gameData.Level == Consts.LvInterval.Length - 1)
            {
                // the most hard level reached.
                return;
            }

            this.tmrForward.Interval = Consts.LvInterval[this.gameData.Level];
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
                this.toolStripMenu_Pause.Enabled = false;

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
            this.gameData.TimeCountArray[3]++;
            if (this.gameData.TimeCountArray[3] == 10)
            {
                this.gameData.TimeCountArray[3] = 0;
                this.AddOneSecond();
            }

            this.txtTime.Text = string.Format(Consts.FormatTime, this.gameData.TimeCountArray[0], this.gameData.TimeCountArray[1], this.gameData.TimeCountArray[2], this.gameData.TimeCountArray[3]);
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
                this.gameData.FoodCount++;
                this.txtFoodCount.Text = this.gameData.FoodCount.ToString();
                this.gameData.Snake.Body.AddFirst(nextHead);
                this.ShowDot(nextHead, false);

                // add score
                this.gameData.ScoreTotal += Consts.ScorePerFood[this.gameData.Level];
                this.txtScore.Text = this.gameData.ScoreTotal.ToString();

                // get next food
                this.gameData.Food = this.GetNextFood();
                this.ShowDot(this.gameData.Food, true);
            }
            else
            {
                // body length remains unchanged
                this.ShowDot(nextHead, false);
                this.FadeDot(this.gameData.Snake.Body.Last.Value);
                this.gameData.Snake.Body.AddFirst(nextHead);
                this.gameData.Snake.Body.RemoveLast();
            }

            return true;
        }

        /// <summary>
        /// Gets the position of next head Dot.
        /// </summary>
        /// <returns>Next head Dot</returns>
        private Dot GetNextHead()
        {
            Dot h = this.gameData.Snake.Body.First.Value;
            int x, y;
            switch (this.gameData.Snake.Direction)
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
            } while (this.gameData.Snake.Body.Contains(d));

            return d;
        }

        /// <summary>
        /// Test if the snake hits the food.
        /// </summary>
        /// <param name="newHead">newHead</param>
        /// <returns>True if the snake head is at the same position with the food.</returns>
        private bool IsFoodEaten(Dot newHead)
        {
            return this.gameData.Food.Equals(newHead);
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

            if (this.gameData.Snake.Body.Contains(newHead))
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
            this.gameData.TimeCountArray[2]++;
            if (this.gameData.TimeCountArray[2] == 60)
            {
                this.gameData.TimeCountArray[2] = 0;
                this.AddOneMinute();
            }
        }

        /// <summary>
        /// Add one minute to the total gaming time.
        /// </summary>
        private void AddOneMinute()
        {
            this.gameData.TimeCountArray[1]++;
            if (this.gameData.TimeCountArray[1] == 60)
            {
                this.gameData.TimeCountArray[1] = 0;
                this.AddOneHour();
            }
        }

        /// <summary>
        /// Add one hour to the total gaming time.
        /// </summary>
        private void AddOneHour()
        {
            this.gameData.TimeCountArray[0]++;
        }
        #endregion

        #region Language Control
        /// <summary>
        /// Initialize the language drop down menus.
        /// </summary>
        private void InitializeLanguageMenu()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Directory.GetCurrentDirectory());
            sb.Append(Consts.LanguageFolder);
            sb.Append(Consts.LanguageMenuListFile);
            sb.Append(Consts.LanguageFileExt);

            XmlReaderSettings set = new XmlReaderSettings();
            set.IgnoreComments = true;
            XmlReader reader = XmlReader.Create(sb.ToString(), set);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);

            var nod = doc.SelectSingleNode(Consts.LanguageMenuListFile);
            var childNodes = nod.ChildNodes;
            Dictionary<string, string> menuItemsDict = new Dictionary<string, string>();
            foreach (XmlNode item in childNodes)
            {
                menuItemsDict.Add(item.Name, item.InnerText);
            }

            // add menu items
            this.AddLanguageMenuItems(menuItemsDict);
        }

        /// <summary>
        /// Add language candidate list to the language menu's DropDownItems.
        /// </summary>
        /// <param name="menuItemsDict">the language candidates list</param>
        private void AddLanguageMenuItems(Dictionary<string, string> menuItemsDict)
        {
            List<ToolStripMenuItem> menuList = new List<ToolStripMenuItem>();

            foreach (KeyValuePair<string, string> kvp in menuItemsDict)
            {
                ToolStripMenuItem oneMenu = new ToolStripMenuItem();
                oneMenu.Name = Consts.LanguageMenuNamePrifix + kvp.Key;
                oneMenu.Text = kvp.Value;
                oneMenu.Size = Consts.LanguageMenuSize;
                menuList.Add(oneMenu);
            }

            this.toolStripMenu_Language.DropDownItems.AddRange(menuList.ToArray());
        }

        /// <summary>
        /// Initialize the facade's display language.
        /// </summary>
        /// <param name="culInfoName">The name of CultureInfo</param>
        private bool InitializeDisplayLanguage(string culInfoName)
        {
            try
            {
                CultureInfo ci = new CultureInfo(culInfoName);

                StringBuilder sb = new StringBuilder();
                sb.Append(Directory.GetCurrentDirectory());
                sb.Append(Consts.LanguageFolder);
                sb.Append(culInfoName);
                sb.Append(Consts.LanguageFileExt);

                // read in
                this.gameData.Lang.CultureInfoName = culInfoName;
                this.ReadInTheLanguageInfo(this.gameData.Lang, sb.ToString());

                // set
                this.SetMenuDisplay(this.gameData.Lang);
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show(this, "An error occured when setting display language!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }
        }

        /// <summary>
        /// Read language config file into a LanguageClass instance.
        /// </summary>
        /// <param name="lang">The LanguageClass to read into.</param>
        /// <param name="filePath">language config file's full path</param>
        private void ReadInTheLanguageInfo(LanguageClass lang, string filePath)
        {
            if (lang == null || string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Language File read error!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNode langNode = doc.SelectSingleNode("language");
            lang.CultureInfoName = ((XmlElement)langNode).GetAttribute("CultureInfo");
            XmlNodeList nodes = langNode.ChildNodes;

            Type t = lang.GetType();
            foreach (XmlNode item in nodes)
            {
                t.GetProperty(item.Name).SetValue(lang, item.InnerText);
            }
        }

        /// <summary>
        /// Set the display words.
        /// </summary>
        /// <param name="lang">The language instance.</param>
        private void SetMenuDisplay(LanguageClass lang)
        {
            if (lang == null)
            {
                return;
            }

            this.Text = lang.Title;
            this.lblTime.Text = lang.GameTime;
            this.txtTime.Left = this.lblTime.Left + this.lblTime.PreferredWidth;
            this.lblFoodCount.Text = lang.FoodEaten;
            this.lblFoodCount.Left = this.txtTime.Left + this.txtTime.Width + 6;
            this.txtFoodCount.Left = this.lblFoodCount.Left + this.lblFoodCount.PreferredWidth;
            this.lblScore.Text = lang.Score;
            this.lblScore.Left = this.txtFoodCount.Left + this.txtFoodCount.Width + 6;
            this.txtScore.Left = this.lblScore.Left + this.lblScore.PreferredWidth;

            this.toolStripMenu_Game.Text = lang.Game;
            this.toolStripMenu_NewGame.Text = lang.NewGame;
            this.toolStripMenu_Pause.Text = this.tmrForward.IsStopped ? lang.Play : lang.Pause;
            this.toolStripMenu_Save.Text = lang.Save;
            this.toolStripMenu_Exit.Text = lang.Exit;
            this.toolStripMenu_Set.Text = lang.Settings;
            this.toolStripMenu_Language.Text = lang.Language;
            foreach (ToolStripMenuItem item in this.toolStripMenu_Language.DropDownItems)
            {
                if (item.Name.IndexOf(lang.CultureInfoName) >= 0)
                {
                    item.Checked = true;
                    //item.CheckState = CheckState.Checked;
                }
                else
                {
                    item.Checked = false;
                    //item.CheckState = CheckState.Unchecked;
                }
            }
            
            this.ToolStripMenu_Help.Text = lang.Help;
            this.toolStripMenu_UpdLog.Text = lang.Log;
            this.toolStripMenu_About.Text = lang.About;
        }
        #endregion
    }
}
