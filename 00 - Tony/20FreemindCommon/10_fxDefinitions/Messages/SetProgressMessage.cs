using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public class SetProgessMessage
    {
        public double Progress
        {
            get;
            set;
        }

        public double MaxProgress
        {
            get;
            set;
        }

        public object State
        {
            get;
            set;
        }

        public SetProgessMessage( double progress, double maxProgress, object message )
        {
            Progress = progress;
            MaxProgress = maxProgress;
            State = message;
        }
    }
}
