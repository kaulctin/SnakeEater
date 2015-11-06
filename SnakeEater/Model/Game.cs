using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace SnakeEater.Model
{
    /// <summary>
    /// Game data class.
    /// </summary>
    [Serializable]
    public class Game
    {
        /// <summary>
        /// The number of foods that are eaten.
        /// </summary>
        public int FoodCount;

        /// <summary>
        /// Current hard level.
        /// </summary>
        public int Level = 0;

        /// <summary>
        /// Total scores.
        /// </summary>
        public int ScoreTotal = 0;

        /// <summary>
        /// The food.
        /// </summary>
        public Dot Food;

        /// <summary>
        /// The snake.
        /// </summary>
        public Snake Snake;

        /// <summary>
        /// Array that stores total gaming time. Each stands for: hours, minutes, seconds, 0.1 seconds.
        /// </summary>
        public int[] TimeCountArray = { 0, 0, 0, 0 };

        /// <summary>
        /// The current language info.
        /// </summary>
        public LanguageClass Lang = new LanguageClass();

        /// <summary>
        /// Do something before serializing.
        /// </summary>
        /// <param name="context">contex</param>
        [OnSerializing]
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
        private void OnSerializing(StreamingContext context)
        {
        }
    }
}
