using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeEater.Model
{
    /// <summary>
    /// A class that contains all the menu display strings.
    /// </summary>
    [Serializable]
    public class LanguageClass
    {
        /// <summary>
        /// CultureInfo of this language.
        /// </summary>
        public string CultureInfoName
        {
            get;
            set;
        }

        /// <summary>
        /// Form's Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Total game time.
        /// </summary>
        public string GameTime { get; set; }

        /// <summary>
        /// String refers to Foods Eaten.
        /// </summary>
        public string FoodEaten { get; set; }

        /// <summary>
        /// String refers to the score.
        /// </summary>
        public string Score { get; set; }

        /// <summary>
        /// String refers to the "Game" menu.
        /// </summary>
        public string Game { get; set; }

        /// <summary>
        /// String refers to the "Start a new game" menu.
        /// </summary>
        public string NewGame { get; set; }

        /// <summary>
        /// String refers to the "Pause" menu.
        /// </summary>
        public string Pause { get; set; }

        /// <summary>
        /// String refers to the "Play" menu.
        /// </summary>
        public string Play { get; set; }

        /// <summary>
        /// String refers to the "Save" menu.
        /// </summary>
        public string Save { get; set; }

        /// <summary>
        /// String refers to the "Restore" menu.
        /// </summary>
        public string Restore { get; set; }

        /// <summary>
        /// String refers to the "Exit" menu.
        /// </summary>
        public string Exit { get; set; }

        /// <summary>
        /// String refers to the "Settings" menu.
        /// </summary>
        public string Settings { get; set; }

        /// <summary>
        /// String refers to the "Language" menu.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// String refers to the "Help" menu.
        /// </summary>
        public string Help { get; set; }

        /// <summary>
        /// String refers to the "Log" menu.
        /// </summary>
        public string Log { get; set; }

        /// <summary>
        /// String refers to the "About" menu.
        /// </summary>
        public string About { get; set; }
    }
}
