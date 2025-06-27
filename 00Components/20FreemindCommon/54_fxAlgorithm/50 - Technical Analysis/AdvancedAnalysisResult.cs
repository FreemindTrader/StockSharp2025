using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using fx.Definitions;
using System.ComponentModel;
using DevExpress.Mvvm;

using fx.Common;

namespace fx.Algorithm
{
    public class AdvancedAnalysisResult : BindableBase
    {
        AdvancedAnalysisManager _parent;
        TrendDirection _mainTrend;
        double _trendProbability;
        double _totalOBOSvalue;
        int _maximumOBOS = 1910; // 1440 + 240 + 120 + 60 + 30 + 15 + 5 ;

        public AdvancedAnalysisResult( AdvancedAnalysisManager parent )
        {
            _parent = parent;
        }

        public double TrendProbability
        {
            get
            {
                return _trendProbability;
            }
            set
            {
                SetValue( ref _trendProbability, value );
            }
        }

        public TrendDirection MainTrend
        {
            get
            {
                return _mainTrend;
            }
            set
            {
                SetValue( ref _mainTrend, value );
            }
        }

        public int MaximumOBOS
        {
            get
            {
                return _maximumOBOS;
            }

            set
            {
                if( value > 0 )
                {
                    SetValue( ref _maximumOBOS, value );
                }
            }
        }

        public double TotalOBOSvalue
        {
            get
            {
                return _totalOBOSvalue;
            }
            set
            {
                SetValue( ref _totalOBOSvalue, value );
            }
        }
    }
}
