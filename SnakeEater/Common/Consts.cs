using System;
using System.Collections.Generic;
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
        /// Default culture info, which is en-US.
        /// </summary>
        public const string DefaultCultureInfo = "en-US";

        /// <summary>
        /// Language file folder.
        /// </summary>
        public const string LanguageFolder = "/lang/";

        /// <summary>
        /// Language file default section.
        /// </summary>
        public const string LanguageSection = "Main";
    }
}
