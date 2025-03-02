using System;
using System.Collections.Generic;
using fx.Common;
using fx.Definitions;

using System.Linq;
using StockSharp.BusinessEntities;
using DevExpress.Mvvm;
using fx.Collections;

namespace fx.Algorithm
{
    public sealed partial class AdvancedAnalysisManager : BindableBase, ITechnicalAnalysisSignalProvider
    {
        private DictionarySlim< TimeSpan, PlatformIndicator > _freemindIndicators = new DictionarySlim< TimeSpan, PlatformIndicator >( );
        private DictionarySlim< TimeSpan, PlatformIndicator > _pivotIndicators    = new DictionarySlim< TimeSpan, PlatformIndicator >( );


        //public static void AddSupportedTimeSpan( TimeSpan support )
        //{
        //    _addedTimeSpan.Add( support );
        //}

        public PooledList<TimeSpan> SupportedTimeSpan
        {
            get
            {
                var list = new PooledList<TimeSpan>();
                var e = _freemindIndicators.GetEnumerator( );
                
                while ( e.MoveNext() )
                {
                    list.Add( e.Current.Key );                    
                }

                return list;                                
            }
        }

        public TimeSpan GetOneWaveTimeSpanHigher( TimeSpan period )
        {
            if ( period == TimeSpan.FromMinutes( 1 )  )
            {
                return TimeSpan.FromHours( 1 );
            }
            else if ( period == TimeSpan.FromHours( 1 ) )
            {
                return TimeSpan.FromDays( 1 );
            }

            return TimeSpan.Zero;
        }

        public TimeSpan GetOneWaveTimeSpanLower( TimeSpan period )
        {
            if ( period == TimeSpan.FromMinutes( 1 ) )
            {
                return TimeSpan.Zero;
            }
            else if ( period == TimeSpan.FromHours( 1 ) )
            {
                return TimeSpan.FromMinutes( 1 );
            }
            else if ( period == TimeSpan.FromDays( 1 ) )
            {
                return TimeSpan.FromHours( 1 );
            }

            return TimeSpan.Zero;
        }

        public TimeSpan GetOneTimeSpanHigher( TimeSpan responsibleForWhatTimeFrame )
        {
            int index = SupportedTimeSpan.IndexOf( responsibleForWhatTimeFrame );

            int next = index < _freemindIndicators.Count - 1 ? index + 1 : -1;

            TimeSpan output = next != -1 ? SupportedTimeSpan[ next ] : TimeSpan.Zero;

            return output;
        }

        public TimeSpan GetOneTimeSpanLower( TimeSpan responsibleForWhatTimeFrame )
        {
            int index = SupportedTimeSpan.IndexOf( responsibleForWhatTimeFrame );

            int prev = index > 0 ? index - 1 : -1;

            TimeSpan output = prev != -1 ? SupportedTimeSpan[ prev ] : TimeSpan.Zero;

            return output;            
        }

        private PlatformIndicator            _monthlyFmIndicator = null;
        private PlatformIndicator            _monthlyPivotPoints = null;
        private PlatformIndicator            _weeklyFmIndicator  = null;
        private PlatformIndicator            _weeklyPivotPoints  = null;
        private PlatformIndicator            _dailyFmIndicator   = null;
        private PlatformIndicator            _dailyPivotPoints   = null;

        private PlatformIndicator            _08hrFmIndicator    = null;
        private PlatformIndicator            _06hrFmIndicator    = null;
        private PlatformIndicator            _04hrFmIndicator    = null;
        private PlatformIndicator            _03hrFmIndicator    = null;
        private PlatformIndicator            _02hrFmIndicator    = null;
        private PlatformIndicator            _01hrFmIndicator    = null;
        private PlatformIndicator            _30MinsFmIndicator  = null;
        private PlatformIndicator            _15MinsFmIndicator  = null;
        private PlatformIndicator            _05MinsFmIndicator  = null;
        private PlatformIndicator            _04MinsFmIndicator  = null;
        private PlatformIndicator            _01MinsFmIndicator  = null;
        private PlatformIndicator            _01SecFmIndicator   = null;

        public PlatformIndicator GetFreemindIndicator( TimeSpan period )
        {
            PlatformIndicator output = null;
            if ( _freemindIndicators.TryGetValue( period, out output ) )
            {
                return output;
            }            

            var fm = IndicatorFactory.Instance.GetIndicatorCloneByName( "FreemindIndicator, Freemind Indicator" );

            _freemindIndicators.GetOrAddValueRef( period ) = fm;            

            fm.Symbol   = _symbol;
            fm.TimeSpan = period;

            SetFreemindIndicator( period, fm );

            return fm;
        }

        public PlatformIndicator GetPivotPoint( TimeSpan period )
        {
            PlatformIndicator output = null;
            if ( _pivotIndicators.TryGetValue( period, out output ) )
            {
                return output;
            }                      

            var pp = IndicatorFactory.Instance.GetIndicatorCloneByName( "PivotPointsCustom, fxtrader.hk Pivot Points" );

            _pivotIndicators.GetOrAddValueRef( period ) = pp;

            pp.TimeSpan = period;

            SetPivotIndicator( period, pp );

            return pp;
        }

        private void SetPivotIndicator( TimeSpan period, PlatformIndicator indicator )
        {
            if ( period == TimeSpan.FromDays( 1 ) )
            {
                _dailyPivotPoints =  indicator;
            }
            else if ( period == TimeSpan.FromDays( 7 ) )
            {
                _weeklyPivotPoints = indicator;
            }
            else if ( period == TimeSpan.FromDays( 30 ) )
            {
                _monthlyPivotPoints = indicator;
            }
            else
            {
                throw new NotSupportedException( );
            }            
        }

        private void SetFreemindIndicator( TimeSpan period, PlatformIndicator indicator  )
        {
            if ( period == TimeSpan.FromSeconds( 1 ) )
            {
                _01SecFmIndicator= indicator;
            }
            else if ( period == TimeSpan.FromMinutes( 1 ) )
            {
                _01MinsFmIndicator= indicator;
            }
            else if ( period == TimeSpan.FromMinutes( 4 ) )
            {
                _04MinsFmIndicator= indicator;
            }
            else if ( period == TimeSpan.FromMinutes( 5 ) )
            {
                _05MinsFmIndicator= indicator;
            }
            else if ( period == TimeSpan.FromMinutes( 15 ) )
            {
                _15MinsFmIndicator= indicator;
            }
            else if ( period == TimeSpan.FromMinutes( 30 ) )
            {
                _30MinsFmIndicator= indicator;
            }
            else if ( period == TimeSpan.FromHours( 1 ) )
            {
                _01hrFmIndicator= indicator;
            }
            else if ( period == TimeSpan.FromHours( 2 ) )
            {
                _02hrFmIndicator= indicator;
            }
            else if ( period == TimeSpan.FromHours( 3 ) )
            {
                _03hrFmIndicator= indicator;
            }
            else if ( period == TimeSpan.FromHours( 4 ) )
            {
                _04hrFmIndicator= indicator;
            }
            else if ( period == TimeSpan.FromHours( 6 ) )
            {
                _06hrFmIndicator= indicator;
            }
            else if ( period == TimeSpan.FromHours( 8 ) )
            {
                _08hrFmIndicator= indicator;
            }
            else if ( period == TimeSpan.FromDays( 1 ) )
            {
                _dailyFmIndicator= indicator;
            }
            else if ( period == TimeSpan.FromDays( 7 ) )
            {
                _weeklyFmIndicator= indicator;
            }
            else if ( period == TimeSpan.FromDays( 30 ) )
            {
                _monthlyFmIndicator= indicator;
            }
            else
            {
                throw new NotSupportedException( );
            }            
        }

        public double GetCurrentSmaValue( TimeSpan period )
        {
            var indicator = GetFreemindIndicator( period );

            var sma = indicator.IndicatorResult[ "SMA" ];
            var last = sma.Count - 1;            

            return ( last >= 0 ) ? sma[ last ] : -1;
        }

        public ( double macd, double macdSig ) GetCurrentMacdValue( TimeSpan period )
        {
            (double macd, double macdSig) output = default;

            var indicator  = GetFreemindIndicator( period );            
            var MACD       = indicator.IndicatorResult["MACD"];
            var MACDSignal = indicator.IndicatorResult["MACDSignal"];

            var last = MACD.Count - 1;

            if ( last >= 0 )
            {
                output.macd     = MACD[ last ];
                output.macdSig  = MACDSignal[ last ];                
            }

            return output;
        }

        public void RaiseSmaValueChange( Security security, TimeSpan period, double sma50, DateTime valueTime )
        {
            if ( SmaValueChangedEvent != null )
            {
                var para = new TaValueEventArgs( security.Code, period, sma50, valueTime );

                SmaValueChangedEvent.Invoke( this, para );
            }
            
        }

        public void RaiseMacdValueChange( Security security, TimeSpan period, (double macd, double macdSig) value, DateTime valueTime )
        {
            if ( MacdValueChangedEvent != null )
            {
                var para = new MacdValueEventArgs( security.Code, period, value, valueTime );

                MacdValueChangedEvent?.Invoke( this, para );
            }            
        }

        public void RaisePivotPointsChanged( Security security, PPChangedEventArgs evt )
        {
            PivotPointChangedEvent?.Invoke( this, evt );
        }
    }
}
