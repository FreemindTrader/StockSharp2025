using System;
using System.ComponentModel;
using fx.Definitions;
using fx.Database;
using DevExpress.Mvvm;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using fx.Collections;

namespace fx.Algorithm
{
    public class FxTradingEvents : BindableBase
    {
        TADivergence _lastDivergenceSignal;
        int _psarSignalBar;
        int _candleFormationBar;
        int _sma50XSignalBar;
        int _sma50ValueBar;
        int _emaTrendBar;
        int _aroonXSignalBar;
        CandleFormation _candleFormation;
        TACandle _candlePattern;

        int _aroonXSignal;
        int _macdCrossSignal;
        int _sma50xSignal;
        int _psarDirection;
        int _maXoverSignal;
        int _emaTrend;

        double _emaPrevious;
        double _emaCurrent;
        double _sma50Value;

        double _pipsFromTarget;
        double _mfiSignal;
        double _cciTrueSignal;
        double _hewRsiSignal;

        double _cciSignal;

        int _priceSignal;
        double _stochValue;

        int _lastMacdxBar;
        int _lastMovingAvgCrossBar;
        int _lastDivergenceBar;

        int _barsFromLastExtremum;
        int _barsFromLastCandlePattern;
        int _barsFromLastSignal;

        int _lastDivergence;
        int _lastStochSignal;

        string _speicalNumber;
        string _nextTarget;
        string _lastCandlePattern;
        string _periodString;
        string _lastMessage;

        TimeSpan _period;

        public FxTradingEvents( TimeSpan period )
        {
            _period = period;
            _periodString = FinancialHelper.GetPeriodString( period );
        }

        public List< TACandleViewModel >     CandlePatternList
        {
            get;
            set;
        }

        public List< TADivergenceViewModel > TADivergenceList
        {
            get;
            set;
        }

        public int AroonXSignal
        {
            get
            {
                return _aroonXSignal;
            }
            set
            {
                SetValue( ref _aroonXSignal, value );
            }
        }

        public int MaXoverSignal
        {
            get
            {
                return _maXoverSignal;
            }
            set
            {
                SetValue( ref _maXoverSignal, value );
            }
        }

        public int MacdCrossSignal
        {
            get
            {
                return _macdCrossSignal;
            }
            set
            {
                SetValue( ref _macdCrossSignal, value );
            }
        }

        public CandleFormation CandleFormation
        {
            get
            {
                return _candleFormation;
            }
            set
            {
                if( _candleFormation == value )
                {
                    return;
                }

                _candleFormation = value;
                CandlePattern = _candleFormation.CandleType;

                RaisePropertyChanged( nameof( CandleFormation ) );
            }
        }

        public int CandleFormationBar
        {
            get
            {
                return _candleFormationBar;
            }
            set
            {
                SetValue( ref _candleFormationBar, value );
            }
        }

        public TACandle CandlePattern
        {
            get
            {
                return _candlePattern;
            }
            set
            {
                SetValue( ref _candlePattern, value );
            }
        }

        public TADivergence LastDivergenceSignal
        {
            get
            {
                return _lastDivergenceSignal;
            }
            set
            {
                SetValue( ref _lastDivergenceSignal, value );
            }
        }

        public double Sma50Value
        {
            get
            {
                return _sma50Value;
            }
            set
            {
                SetValue( ref _sma50Value, value );
            }
        }

        public int EmaTrend
        {
            get
            {
                return _emaTrend;
            }
            set
            {
                SetValue( ref _emaTrend, value );
            }
        }

        public double EmaCurrent
        {
            get
            {
                return _emaCurrent;
            }
            set
            {
                SetValue( ref _emaCurrent, value );
            }
        }

        public double EmaPrevious
        {
            get
            {
                return _emaPrevious;
            }
            set
            {
                SetValue( ref _emaPrevious, value );
            }
        }

        public int Sma50xSignal
        {
            get
            {
                return _sma50xSignal;
            }
            set
            {
                SetValue( ref _sma50xSignal, value );
            }
        }

        public int PsarSignal
        {
            get
            {
                return _psarDirection;
            }
            set
            {
                SetValue( ref _psarDirection, value );
            }
        }

        public double StochValue
        {
            get
            {
                return _stochValue;
            }
            set
            {
                SetValue( ref _stochValue, value );
            }
        }

        public double HewRsiValue
        {
            get
            {
                return _hewRsiSignal;
            }
            set
            {
                SetValue( ref _hewRsiSignal, value );
            }
        }

        public int PriceSignal
        {
            get
            {
                return _priceSignal;
            }
            set
            {
                SetValue( ref _priceSignal, value );
            }
        }

        public double CciValue
        {
            get
            {
                return _cciSignal;
            }
            set
            {
                SetValue( ref _cciSignal, value );
            }
        }

        public double CciTrueValue
        {
            get
            {
                return _cciTrueSignal;
            }
            set
            {
                SetValue( ref _cciTrueSignal, value );
            }
        }

        public double MfiValue
        {
            get
            {
                return _mfiSignal;
            }
            set
            {
                SetValue( ref _mfiSignal, value );
            }
        }

        public string CandleSignal
        {
            get
            {
                return _lastCandlePattern;
            }
            set
            {
                SetValue( ref _lastCandlePattern, value );
            }
        }

        public string SpecialNumber
        {
            get
            {
                return _speicalNumber;
            }
            set
            {
                SetValue( ref _speicalNumber, value );
            }
        }

        public string LastMessage
        {
            get
            {
                return _lastMessage;
            }
            set
            {
                SetValue( ref _lastMessage, value );
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
                SetValue( ref _period, value );
            }
        }

        public string PeriodString
        {
            get
            {
                if( _period == TimeSpan.FromSeconds( 1 ) )
                {
                    return "01 - 1 Sec";
                }
                else if( _period == TimeSpan.FromMinutes( 1 ) )
                {
                    return "02 - 1 min";
                }
                else if( _period == TimeSpan.FromMinutes( 4 ) )
                {
                    return "03 - 04 mins";
                }
                else if( _period == TimeSpan.FromMinutes( 5 ) )
                {
                    return "04 - 05 mins";
                }
                else if( _period == TimeSpan.FromMinutes( 15 ) )
                {
                    return "05 - 15 mins";
                }
                else if( _period == TimeSpan.FromMinutes( 30 ) )
                {
                    return "06 - 30 mins";
                }
                else if( _period == TimeSpan.FromHours( 1 ) )
                {
                    return "07 - 01 Hour";
                }
                else if( _period == TimeSpan.FromHours( 2 ) )
                {
                    return "08 - 02 Hour";
                }
                else if ( _period == TimeSpan.FromHours( 3 ) )
                {
                    return "09 - 03 Hour";
                }
                else if( _period == TimeSpan.FromHours( 4 ) )
                {
                    return "10 - 04 Hour";
                }
                else if ( _period == TimeSpan.FromHours( 6 ) )
                {
                    return "11 - 06 Hour";
                }
                else if ( _period == TimeSpan.FromHours( 8 ) )
                {
                    return "12 - 08 Hour";
                }
                else if( _period == TimeSpan.FromDays( 1 ) )
                {
                    return "13 - Daily";
                }
                else if( _period == TimeSpan.FromDays( 7 ) )
                {
                    return "14 - Weekly";
                }
                else if( _period == TimeSpan.FromDays( 30 ) )
                {
                    return "15 - Monthly";
                }

                return "Unknown";
            }
        }

        public string PeriodSmallString
        {
            get
            {
                if ( _period == TimeSpan.FromSeconds( 1 ) )
                {
                    return "1s";
                }
                else if ( _period == TimeSpan.FromMinutes( 1 ) )
                {
                    return "1m";
                }
                else if ( _period == TimeSpan.FromMinutes( 4 ) )
                {
                    return "4m";
                }
                else if ( _period == TimeSpan.FromMinutes( 5 ) )
                {
                    return "5m";
                }
                else if ( _period == TimeSpan.FromMinutes( 15 ) )
                {
                    return "15m";
                }
                else if ( _period == TimeSpan.FromMinutes( 30 ) )
                {
                    return "30m";
                }
                else if ( _period == TimeSpan.FromHours( 1 ) )
                {
                    return "1hr";
                }
                else if ( _period == TimeSpan.FromHours( 2 ) )
                {
                    return "2hr";
                }
                else if ( _period == TimeSpan.FromHours( 3 ) )
                {
                    return "3hr";
                }
                else if ( _period == TimeSpan.FromHours( 4 ) )
                {
                    return "4hr";
                }
                else if ( _period == TimeSpan.FromHours( 6 ) )
                {
                    return "6hr";
                }
                else if ( _period == TimeSpan.FromHours( 8 ) )
                {
                    return "8hr";
                }
                else if ( _period == TimeSpan.FromDays( 1 ) )
                {
                    return "1D";
                }
                else if ( _period == TimeSpan.FromDays( 7 ) )
                {
                    return "1W";
                }
                else if ( _period == TimeSpan.FromDays( 30 ) )
                {
                    return "1M";
                }

                return "Unknown";
            }
        }

        public int MaXoverSignalBar
        {
            get
            {
                return _lastMovingAvgCrossBar;
            }
            set
            {
                SetValue( ref _lastMovingAvgCrossBar, value );
            }
        }

        public int PsarSignalBar
        {
            get
            {
                return _psarSignalBar;
            }
            set
            {
                SetValue( ref _psarSignalBar, value );
            }
        }

        public int AroonXSignalBar
        {
            get
            {
                return _aroonXSignalBar;
            }
            set
            {
                SetValue( ref _aroonXSignalBar, value );
            }
        }

        public int EmaTrendBar
        {
            get
            {
                return _emaTrendBar;
            }
            set
            {
                SetValue( ref _emaTrendBar, value );
            }
        }

        public int Sma50ValueBar
        {
            get
            {
                return _sma50ValueBar;
            }
            set
            {
                SetValue( ref _sma50ValueBar, value );
            }
        }

        public int Sma50xSignalBar
        {
            get
            {
                return _sma50XSignalBar;
            }
            set
            {
                SetValue( ref _sma50XSignalBar, value );
            }
        }

        public int MacdCrossSignalBar
        {
            get
            {
                return _lastMacdxBar;
            }
            set
            {
                SetValue( ref _lastMacdxBar, value );
            }
        }

        public int LastDivergenceBar
        {
            get
            {
                return _lastDivergenceBar;
            }
            set
            {
                SetValue( ref _lastDivergenceBar, value );
            }
        }

        public int LastDivergence
        {
            get
            {
                return _lastDivergence;
            }
            set
            {
                SetValue( ref _lastDivergence, value );
            }
        }

        public int LastStochSignal
        {
            get
            {
                return _lastStochSignal;
            }
            set
            {
                SetValue( ref _lastStochSignal, value );
            }
        }

        public int BarsFromLastSignal
        {
            get
            {
                return _barsFromLastSignal;
            }
            set
            {
                SetValue( ref _barsFromLastSignal, value );
            }
        }

        public int BarsFromLastCandlePattern
        {
            get
            {
                return _barsFromLastCandlePattern;
            }
            set
            {
                SetValue( ref _barsFromLastCandlePattern, value );
            }
        }

        public int BarsFromLastExtremum
        {
            get
            {
                return _barsFromLastExtremum;
            }
            set
            {
                SetValue( ref _barsFromLastExtremum, value );
            }
        }

        public string NextTarget
        {
            get
            {
                return _nextTarget;
            }
            set
            {
                SetValue( ref _nextTarget, value );
            }
        }

        public double PipsFromTarget
        {
            get
            {
                return _pipsFromTarget;
            }
            set
            {
                SetValue( ref _pipsFromTarget, value );
            }
        }
    }
}
