using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Algorithm
{
    [Serializable]
    public class DoneReloadDatabars
    {
        // Fields...
        private string _Symbol;
        private TimeSpan _period;
        private DateTime _StartDate;
        private long _TotalBarCount;

        public string Symbol
        {
            get
            {
                return _Symbol;
            }
            set
            {
                _Symbol = value;
            }
        }

        public DateTime StartDate
        {
            get
            {
                return _StartDate;
            }
            set
            {
                _StartDate = value;
            }
        }

        public TimeSpan Period
        {
            get
            {
                return _period;
            }
            set
            {
                _period = value;
            }
        }

        public long TotalBarCount
        {
            get
            {
                return _TotalBarCount;
            }
            set
            {
                _TotalBarCount = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoneReloadDataResponseMessage"/> class.
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="period"></param>
        /// <param name="startDate"></param>
        /// <param name="totalBarCount"></param>
        public DoneReloadDatabars(
                                    string symbol,
                                    TimeSpan period,
                                    DateTime startDate,
                                    long totalBarCount )
        {
            _Symbol = symbol;
            _period = period;
            _StartDate = startDate;
            _TotalBarCount = totalBarCount;
        }
    }
}
