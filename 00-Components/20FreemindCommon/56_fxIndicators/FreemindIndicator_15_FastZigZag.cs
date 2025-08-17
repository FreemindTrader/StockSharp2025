using System;
using fx.Collections;
using fx.Bars;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

using fx.Common;

using fx.Algorithm;
using fx.TALib;
using fx.Definitions;
using System.Data;
using fx.Database;


#pragma warning disable 414

namespace fx.Indicators
{
    public partial class FreemindIndicator : CustomPlatformIndicator
    {        
        protected Task BasicFastZigZagTask( bool fullRecalculation, DataBarUpdateType? updateType, double percentage, int pips, FastZigZagVariables f, ref PooledList<Task> tasksList )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }

            //ThreadHelper.UpdateThreadName( "BasicFastZigZagTask" );

            

            Task first = new Task(() => FastZigZag( fullRecalculation, updateType, percentage, pips, f ), IndicatorExitToken);

            tasksList.Add( first );

            return first;
        }

        public void FastZigZag( bool fullRecalculation, DataBarUpdateType? updateType, double percentage, int pips, FastZigZagVariables zz )
        {
            var depth = ( double) ( _indicatorSecurity.PriceStep * pips );            

            if ( zz.calculated == 0 ) zz.LastBarIndex = 0;

            var total = _barCountBeforeCalculation;

            if ( ( updateType == DataBarUpdateType.Initial ) || ( updateType == DataBarUpdateType.HistoryUpdate ) || ( updateType == DataBarUpdateType.Reloaded ) || updateType == DataBarUpdateType.NewPeriod )
            {
                zz.ResetStaringChangeIndex();

                var startIndex = zz.calculated > 0 ? zz.calculated - 1 : 0;

                for ( int i = startIndex; i < _barCountBeforeCalculation - 1; i++ )
                {
                    bool higherHigh=false;

                    zz.zzL[ i ] = 0;
                    zz.zzH[ i ] = 0;

                    if ( zz.isUpTrend )
                    {
                        /* -------------------------------------------------------------------------------------------------------------------------------------------
                         * 
                         *  Here we have a higher high, so we clear out last high
                         * 
                         * ------------------------------------------------------------------------------------------------------------------------------------------- */

                        if ( zz.IsHigherHigh( i ) )//-deviation)
                        {
                            zz.RemoveLastHighAndSetCurrentHigh( i );

                            /* -------------------------------------------------------------------------------------------------------------------------------------------
                             * 
                             *  Even though we have a higher high, if our low somehow is lower than last highest Bar's low by x pips, 
                             *  And this current bar is a falling bar, the direction is changed.
                             *  
                             *  If somehow, the closing bar recovered, we have to reinstate last high
                             * 
                             * 
                             * ------------------------------------------------------------------------------------------------------------------------------------------- */

                            if ( zz.CurrentLowBreakPreBarsHighByDepth( i, depth ) )
                            {
                                if ( zz.IsRaisingBar( i )  )
                                {
                                    zz.RestoreLastHigh( i );                                    
                                }
                                else
                                {
                                    zz.DownTrendNow( );
                                }

                                zz.MarkCurrentBarAsWaveTrough( i );                                
                            }

                            zz.LastBarIndex = i;
                            higherHigh = true;
                        }

                        /* -------------------------------------------------------------------------------------------------------------------------------------------
                        * 
                        *  If current bar's low is depth pips below the last bar's high without making a new higher high
                        *  And this current bar is a falling bar, the direction is changed.
                        *  
                        *  If somehow, the closing bar recovered, we have to reinstate last high
                        * 
                        * 
                        * ------------------------------------------------------------------------------------------------------------------------------------------- */
                        if ( zz.CurrentLowBreakPreZigZagHighByDepth( i, depth )  && ( !higherHigh || zz.IsFallingBar( i )  ) )
                        {
                            zz.MarkCurrentBarAsWaveTrough( i );
                            //![](0C1989CD2DD29418D85D241CE89C5470.png;;;0.00935,0.01066)
                            // The following code means that the fall is great and we have recovered a lot to close the bar as positive  and the recover is larger than the depth PIps
                            // So we will record both the low and the high
                            if ( zz.FallBigThenRecoverMoreThanDepth( i, depth ) ) 
                            {
                                zz.AddExtraWavePeak( i );                                
                            }
                            else
                            {
                                zz.DownTrendNow();
                            }

                            zz.LastBarIndex = i;
                        }
                    }
                    else
                    {
                        bool lowerLow=false;

                        if ( zz.IsLowerLow( i )  )
                        {
                            zz.RemoveLastLowAndSetCurrentLow( i );
                           
                            if ( zz.CurrentHighBreakPreBarsLowByDepth( i, depth) )
                            {
                                if ( zz.IsFallingBar( i ) )
                                {
                                    zz.RestoreLastLow( i );
                                }
                                else
                                {
                                    zz.UpTrendNow();
                                }

                                zz.MarkCurrentBarAsWavePeak( i );                                
                            }
                            zz.LastBarIndex = i;
                            lowerLow = true;
                        }

                        if ( zz.CurrentHighBreakPreZigZagLowByDepth( i, depth ) && ( !lowerLow || zz.IsRaisingBar( i ) ) )                            
                        {
                            zz.MarkCurrentBarAsWavePeak( i );

                            if ( zz.RiseBigThenRetraceMoreThanDepth( i, depth ) )                            
                            {
                                zz.AddExtraWaveTrough( i );
                                
                            }
                            else zz.direction = 1;

                            zz.LastBarIndex = i;
                        }
                    }
                }

                zz.AddZigZagInfo( _hews, startIndex, pips );

                

                zz.zzH[ total - 1 ] = 0;
                zz.zzL[ total - 1 ]  = 0;

                zz.calculated        = _barCountBeforeCalculation;
            }
        }

        public class FastZigZagVariables
        {
            public int LastBarIndex = 0;

            public int direction = 1;

            public int calculated = 0;

            public ThreadSafeDictionary< int, double > zzL = new ThreadSafeDictionary< int, double >( );
            public ThreadSafeDictionary< int, double > zzH = new ThreadSafeDictionary< int, double >();
            public ThreadSafeDictionary<long, TASignal> zzSignals = new ThreadSafeDictionary<long, TASignal>( );
            public fxHistoricBarsRepo Bars { get; set; }


            public bool isUpTrend
            {
                get
                {
                    return direction == 1;
                }

            }

            public bool isDownTrend
            {
                get
                {
                    return direction == -1;
                }
            }

            public bool IsHigherHigh( int i )
            {
                return Bars[ i ].High > zzH[ LastBarIndex ];
            }

            public bool IsLowerLow( int i )
            {
                return Bars[ i ].Low < zzL[ LastBarIndex ];
            }

            public void RemoveLastHighAndSetCurrentHigh( int i )
            {
                zzH[ LastBarIndex ] = 0;
                zzSignals.Remove( Bars[ LastBarIndex ].LinuxTime );

                zzH[ i ] = Bars[ i ].High;

                zzSignals.TryGetValueAndAddOrReplace( Bars[ i ].LinuxTime, TASignal.WAVE_PEAK, out var old );

                _startingChangesIndex = LastBarIndex;
            }

            public void RemoveLastLowAndSetCurrentLow( int i )
            {
                zzL[ LastBarIndex ] = 0;
                zzSignals.Remove( Bars[ LastBarIndex ].LinuxTime );

                zzL[ i ] = Bars[ i ].Low;
                zzSignals.TryAdd( Bars[ i ].LinuxTime, TASignal.WAVE_TROUGH );

                if ( LastBarIndex < _startingChangesIndex )
                {
                    _startingChangesIndex = LastBarIndex;
                }
            }



            public bool IsRaisingBar( int i )
            {
                return ( Bars[ i ].Open < Bars[ i ].Close );
            }

            public bool IsFallingBar( int i )
            {
                return ( Bars[ i ].Open > Bars[ i ].Close );
            }

            internal void RestoreLastHigh( int i )
            {
                zzH[ LastBarIndex ] = Bars[ LastBarIndex ].High;
                zzSignals.TryAdd( Bars[ LastBarIndex ].LinuxTime, TASignal.WAVE_PEAK );

                if ( LastBarIndex < _startingChangesIndex )
                {
                    _startingChangesIndex = LastBarIndex;
                }
            }

            internal void RestoreLastLow( int i )
            {
                zzL[ LastBarIndex ] = Bars[ LastBarIndex ].Low;
                zzSignals.TryAdd( Bars[ LastBarIndex ].LinuxTime, TASignal.WAVE_TROUGH );

                if ( LastBarIndex < _startingChangesIndex )
                {
                    _startingChangesIndex = LastBarIndex;
                }
            }

            public void DownTrendNow()
            {
                direction = -1;
            }

            public void UpTrendNow()
            {
                direction = 1;
            }

            internal void MarkCurrentBarAsWaveTrough( int i )
            {
                zzL[ i ] = Bars[ i ].Low;
                zzSignals.TryAdd( Bars[ i ].LinuxTime, TASignal.WAVE_TROUGH );

                if ( i < _startingChangesIndex )
                {
                    _startingChangesIndex = i;
                }
            }


            internal void MarkCurrentBarAsWavePeak( int i )
            {
                zzH[ i ] = Bars[ i ].High;
                zzSignals.TryAdd( Bars[ i ].LinuxTime, TASignal.WAVE_PEAK );

                if ( i < _startingChangesIndex )
                {
                    _startingChangesIndex = i;
                }
            }


            internal bool FallBigThenRecoverMoreThanDepth( int i, double depth )
            {
                return Bars[ i ].High > zzL[ i ] + depth && Bars[ i ].Open < Bars[ i ].Close;
            }

            internal bool RiseBigThenRetraceMoreThanDepth( int i, double depth )
            {
                return Bars[ i ].Low < zzH[ i ] - depth && Bars[ i ].Open > Bars[ i ].Close;
            }

            internal void AddExtraWavePeak( int i )
            {
                zzH[ i ] = Bars[ i ].High;
                zzSignals.TryAdd( Bars[ i ].LinuxTime, TASignal.WAVE_PEAK );

                if ( i < _startingChangesIndex )
                {
                    _startingChangesIndex = i;
                }
            }

            internal void AddExtraWaveTrough( int i )
            {
                zzL[ i ] = Bars[ i ].Low;
                zzSignals.TryAdd( Bars[ i ].LinuxTime, TASignal.WAVE_TROUGH );

                if ( i < _startingChangesIndex )
                {
                    _startingChangesIndex = i;
                }
            }

            internal bool CurrentLowBreakPreBarsHighByDepth( int i, double depth )
            {
                return ( Bars[ i ].Low < Bars[ LastBarIndex ].High - depth );
            }

            internal bool CurrentHighBreakPreBarsLowByDepth( int i, double depth )
            {
                return Bars[ i ].High > Bars[ LastBarIndex ].Low + depth;
            }

            internal bool CurrentLowBreakPreZigZagHighByDepth( int i, double depth )
            {
                return Bars[ i ].Low < zzH[ LastBarIndex ] - depth;
            }

            internal bool CurrentHighBreakPreZigZagLowByDepth( int i, double depth )
            {
                return Bars[ i ].High > zzL[ LastBarIndex ] + depth;
            }

            internal void AddZigZagInfo( HewManager hew, int startIndex, int pips )
            {
                if ( _startingChangesIndex != Int32.MaxValue )
                {
                    var startingTime = Bars[ _startingChangesIndex ].LinuxTime;
                    var changes = zzSignals.Where( x => x.Key >= startingTime );

                    Bars.RemoveSignalsStartingFromIndex( _startingChangesIndex, TASignal.WAVE_PEAK, TASignal.WAVE_TROUGH );
                    Bars.AddSignalsToDataBar( changes );

                    if ( hew != null && Bars.Period.HasValue )
                    {
                        hew.BuildWavesImportance( Bars, Bars.Period.Value, pips, changes );
                    }

                    var aa = SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( Bars.Security );

                    if ( aa == null )
                    {
                        return;
                    }

                    var taManager     = ( PeriodXTaManager ) aa.GetPeriodXTa( Bars.Period.Value );                    
                    taManager.AddZigZagInfo( Bars[ startIndex ].LinuxTime, changes );
                }


            }

            public void ResetStaringChangeIndex()
            {
                _startingChangesIndex = Int32.MaxValue;
            }

            int _startingChangesIndex = Int32.MaxValue;
        }
    }
}