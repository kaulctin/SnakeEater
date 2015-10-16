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
        public bool IsStopped
        {
            get;
            private set;
        }

        public UTimer()
        {
            this.IsStopped = true;
        }

        public UTimer(IContainer container)
        {
            this.IsStopped = true;
            
            // TODO
        }


        new public void Start()
        {
            this.IsStopped = false;
            base.Start();
        }

        new public void Stop()
        {
            this.IsStopped = true;
            base.Stop();
        }
    }
}
