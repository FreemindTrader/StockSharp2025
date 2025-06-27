using DevExpress.Mvvm;
using fx.Collections;
using fx.Common;
using fx.Definitions;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace fx.Algorithm
{
    public partial class PeriodXTaManager
    {
        private ThreadSafeDictionary< long, FreemindAdvAnalysisInfo > _technicalAnalysisInfo = new ThreadSafeDictionary< long, FreemindAdvAnalysisInfo >( );
        private ThreadSafeDictionary< long, TASignal > _zigZagInfo = new ThreadSafeDictionary< long, TASignal >( );

        private double _todayRange                = -1;
        private int    _todayRangeToAvgPercentage = -1;
        private double _averageDailyRange         = -1;
        private long   _latestDivergenceIndex     = -1;

        private long   _latestWaveRotationIndex   = -1;

        private long _lastestGannPriceTimeIndex = -1;

        private long   _latestBottomSignalIndex   = -1;
        private long   _latestTopSignalIndex      = -1;

        public long LatestBottomSignalIndex
        {
            get
            {
                return _latestBottomSignalIndex;
            }
        }
        public long LatestTopSignalIndex
        {
            get
            {
                return _latestTopSignalIndex;
            }
        }

        public long LatestDivergenceIndex
        {
            get
            {
                return _latestDivergenceIndex;
            }
        }

        public long LatestWaveRotationIndex
        {
            get
            {
                return _latestWaveRotationIndex;
            }
        }

        public long LastestGannPriceTimeIndex
        {
            get
            {
                return _lastestGannPriceTimeIndex;
            }
        }
    }
}
