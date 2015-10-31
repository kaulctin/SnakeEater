using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeEater.Common
{
    /// <summary>
    /// Constants class
    /// </summary>
    public class Consts
    {
        /// <summary>
        /// The max x position of snake or food.
        /// </summary>
        public const int MAX_X = 64;

        /// <summary>
        /// The max y position of snake or food.
        /// </summary>
        public const int MAX_Y = 40;

        /// <summary>
        /// Time format for gaming time. "{0}:{1:D2}:{2:D2}.{3}"
        /// </summary>
        public const string FormatTime = "{0}:{1:D2}:{2:D2}.{3}";

        /// <summary>
        /// The key of default language setting in config file.
        /// </summary>
        public const string DefaultLangConfigKey = "DefaultLanguage";

        /// <summary>
        /// Default culture info, which is en-US.
        /// </summary>
        public const string DefaultCultureInfo = "en-US";

        /// <summary>
        /// Language file folder.
        /// </summary>
        public const string LanguageFolder = "/lang/";

        /// <summary>
        /// Language menu file's name.
        /// </summary>
        public const string LanguageMenuListFile = "list";

        /// <summary>
        /// Language file extension ".xml"
        /// </summary>
        public const string LanguageFileExt = ".xml";

        /// <summary>
        /// Language file default section.
        /// </summary>
        public const string LanguageSection = "Main";

        /// <summary>
        /// Language menu's name prifix.
        /// </summary>
        public const string LanguageMenuNamePrifix = "toolStripMenu_Lang_";

        /// <summary>
        /// The array of the interval for each level.
        /// </summary>
        public static readonly int[] LvInterval = { 300, 250, 200, 150, 100, 80, 50 };

        /// <summary>
        /// The array of the scores for each level.
        /// </summary>
        public static readonly int[] ScorePerFood = { 10, 12, 16, 25, 50, 80, 100 };

        /// <summary>
        /// Language selection list menu's default size.
        /// </summary>
        public static readonly Size LanguageMenuSize = new Size(152, 22);
    }
}
