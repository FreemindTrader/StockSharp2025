using fx.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Common
{
    public class PivotPointMessage
    {

        DateTime _lastPivotTime;
        TimeSpan _timeFrame;
        PooledList< SRlevel > _srLevels = null;

        public TimeSpan TimeFrame
        {
            get { return _timeFrame; }
            set
            {
                _timeFrame = value;
            }
        }

        
        public DateTime LastPivotTime
        {
            get { return _lastPivotTime; }
            set
            {
                _lastPivotTime = value;
            }
        }

        public PooledList< SRlevel > SrLevels
        {
            get { return _srLevels; }
            set
            {
                _srLevels = value;
            }
        }



        public PivotPointMessage( TimeSpan tf, DateTime lastPivotTime, PooledList<SRlevel> srLevels )
        {
            TimeFrame     = tf;
            LastPivotTime = lastPivotTime;
            SrLevels      = srLevels;
        }
    }
}
