using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public class DivergenceEventArgs : EventArgs
    {
        public TimeSpan Period
        {
            get;
            set;
        }

        public TADivergence Divergence
        {
            get;
            set;
        }

        public long     StartIndex { get; set; }
        public double   StartValue { get; set; }
        public DateTime StartTime  { get; set; }

        public long     EndIndex { get; set; }
        public double   EndValue { get; set; }
        public DateTime EndTime { get; set; }



        public string Security { get; set; }

        public DivergenceEventArgs( string security, TimeSpan timePeriod, TADivergence divergence, long startIndex, double startValue, DateTime startTime, long endIndex, double endValue, DateTime endTime )
        {
            Security   = security;
            Period     = timePeriod;
            Divergence = divergence;
            StartIndex = startIndex;
            StartValue = startValue;
            StartTime  = startTime;
            EndIndex   = endIndex;
            EndValue   = endValue;
            EndTime    = endTime;
        }
    }
}
