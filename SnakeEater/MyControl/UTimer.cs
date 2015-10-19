using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeEater.MyControl
{
    public class UTimer : Timer
    {
        #region Properties
        /// <summary>
        /// Gets a value that indicates whether the timer is stopped or not.
        /// </summary>
        [Browsable(true)]
        [Category("CustomizedProperties")]
        [DefaultValue(true)]
        [Description("Gets a value that indicates whether the timer is stopped or not.")]
        public bool IsStopped
        {
            get;
            protected set;
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialize a new instance of Kaulctin.FormUI.View.Timer class.
        /// </summary>
        public UTimer() : base()
        {
            this.IsStopped = true;
        }

        /// <summary>
        /// Initialize a new instance of Kaulctin.FormUI.View.Timer class with its container.
        /// </summary>
        /// <param name="container">a System.ComponentModel.IContainer that contains this new Kaulctin.FormUI.View.Timer instance.</param>
        public UTimer(IContainer container)
            : base(container)
        {
            this.IsStopped = true;
        }
        #endregion

        /// <summary>
        /// Start the timer instance.
        /// </summary>
        new public void Start()
        {
            this.IsStopped = false;
            base.Start();
        }

        /// <summary>
        /// Stop the timer instance.
        /// </summary>
        new public void Stop()
        {
            this.IsStopped = true;
            base.Stop();
        }
    }
}
