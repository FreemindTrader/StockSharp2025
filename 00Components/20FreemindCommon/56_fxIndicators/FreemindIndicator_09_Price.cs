using DevExpress.Mvvm;

using fx.Common;
using fx.Definitions;
using fx.Algorithm;

using System;
using fx.Collections;
using System.Linq;
using System.Threading.Tasks;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using System.Threading;

#pragma warning disable 414

namespace fx.Indicators
{
    public partial class FreemindIndicator : CustomPlatformIndicator
    {
        int _timePeriod = 14;

        double _latestAvgRange = 0.0;

        public int ATRtimePeriod
        {
            get { return _timePeriod; }
            set { _timePeriod = value; }
        }

        int _atrlookback = 0;

        private BarSpeed _barSpeedReading = BarSpeed.Invalid;
        protected Task BasicPriceTask( bool fullRecalculation, DataBarUpdateType? updateType, int curIterationBarcount, ref PooledList<Task> tasksList )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }

            Task first = new Task(() => ProcessPriceStuff( fullRecalculation, updateType, curIterationBarcount, IndicatorExitToken ), IndicatorExitToken);
            //Task second = first.ContinueWith(
            //                                    antecedent =>
            //                                    {
            //                                        if (antecedent.Status == TaskStatus.Faulted)
            //                                        {
            //                                            this.LogError("ProcessPriceStuff() caused Exception. Stack = " + antecedent.Exception.InnerException.StackTrace);
            //                                        }
            //                                        else
            //                                        {
            //                                            var pips = (int)( _latestAvgRange/(double) _indicatorSecurity.PriceStep.Value );

            //                                            FastZigZag( fullRecalculation, updateType, 0.1, pips, _fastZZ5 );
            //                                        }

            //                                    }, IndicatorExitToken);
            tasksList.Add( first );            

            return first;
        }

        protected void ProcessPriceStuff( bool fullRecalculation, DataBarUpdateType? updateType, int curIterationBarcount, CancellationToken token )
        {
            //ThreadHelper.UpdateThreadName( "ProcessPriceStuff" );

            if ( _currentPeriod == TimeSpan.FromMinutes( 1 ) )
            {
                CalculatePricePercentage( fullRecalculation, updateType, curIterationBarcount, token );
            }

            CalculatePriceRange( fullRecalculation, updateType, curIterationBarcount, token );

            return;
        }

        private void CalculatePriceRange( bool fullRecalculation, DataBarUpdateType? updateType, int curIterationBarcount, CancellationToken token )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return;
            }

            int resultSetLength   = IndicatorResult["FreemindATR"].Count;

            if ( resultSetLength == 0 )
            {
                ProcessATRNewIndicatorBuffer( curIterationBarcount, token );
            }
            else
            {
                ProcessATRExistingBuffer( updateType, curIterationBarcount, resultSetLength, token );
            }

            if ( _currentPeriod == TimeSpan.FromDays( 1 ) )
            {
                var aa = SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );

                if ( aa == null )
                    return;

                var taManager                       = ( PeriodXTaManager ) aa.GetPeriodXTa( TimeSpan.FromDays( 1 ) );

                taManager.TodayRange                = Math.Round( Bars.Current.CandleLengthAsPip, _instrumentDigits ); 
                taManager.TodayRangeToAvgPercentage = ( int ) Bars.Current.CandleLengthAsPip;
            }
        }

        private void ProcessATRNewIndicatorBuffer( int barB4Calculation, CancellationToken token )
        {
            int startIndex    = 0;
            int endIndex      = barB4Calculation - 1;
            int indexCount    = endIndex - startIndex + 1;

            var atr           = new double [ indexCount ];

            var outBeginIdx   = 0;
            var outNBElement  = 0;

            if ( token.IsCancellationRequested )
            {                
                token.ThrowIfCancellationRequested();
            }

            TALib.Core.RetCode code = TALib.Core.Atr(Bars, startIndex, endIndex, atr, out outBeginIdx, out outNBElement, ATRtimePeriod );

            if ( outNBElement > 0 )
            {
                lock ( IndicatorResult )
                {
                    IndicatorResult.AddSetValues( "FreemindATR", outBeginIdx, outNBElement, true, atr );
                }

                int atrIndex = outNBElement - 2;

                if ( atrIndex > 0 )
                {
                    _latestAvgRange = atr[ atrIndex ];
                }
                else
                {
                    _latestAvgRange = atr[ outNBElement - 1 ];
                }

                if ( _currentPeriod == TimeSpan.FromDays( 1 ) )
                {
                    var aa = SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );

                    if ( aa == null )
                        return;

                    var taManager = ( PeriodXTaManager ) aa.GetPeriodXTa( TimeSpan.FromDays( 1 ) );

                    if ( Bars.Current.SymbolEx.IsForexPair() )
                    {
                        taManager.AverageDailyRange = Math.Round( _latestAvgRange / _instrumentPointSize, 1 );
                    }
                    else
                    {
                        taManager.AverageDailyRange = Math.Round( _latestAvgRange, _instrumentDigits );
                    }

                    taManager.TodayRangeToAvgPercentage = ( int ) Bars.Current.CandleLengthAsPip;
                }
            }
        }


        private void ProcessATRExistingBuffer( DataBarUpdateType? updateType, int barB4Calculation, int resultSetLength, CancellationToken token )
        {
            var startIndex = resultSetLength - 1;
            var endIndex   = barB4Calculation - 1;
            var indexCount = endIndex - startIndex + 1;

            if ( endIndex < 0 || indexCount < 0 )
            {
                return;
            }

            int outBeginIdx       = 0;
            int outNBElement      = 0;

            var atr           = new double [ indexCount ];

            if ( token.IsCancellationRequested )
            {
                token.ThrowIfCancellationRequested();
            }

            TALib.Core.RetCode code = TALib.Core.Atr( Bars, startIndex, endIndex, atr, out outBeginIdx, out outNBElement, ATRtimePeriod );

            if ( outNBElement > 0 )
            {
                lock ( IndicatorResult )
                {
                    IndicatorResult.AddSetValues( "FreemindATR", outBeginIdx, outNBElement, true, atr );
                }

                int atrIndex = outNBElement - 2;


                if ( atrIndex > 0 )
                {
                    _latestAvgRange = atr[ atrIndex ];
                }
                else
                {
                    _latestAvgRange = atr[ outNBElement - 1 ];
                }



                if ( _currentPeriod == TimeSpan.FromDays( 1 ) )
                {
                    var aa = SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );

                    if ( aa == null )
                        return;

                    var taManager = ( PeriodXTaManager ) aa.GetPeriodXTa( TimeSpan.FromDays( 1 ) );

                    if ( Bars.Current.SymbolEx.IsForexPair() )
                    {
                        taManager.AverageDailyRange = Math.Round( _latestAvgRange / _instrumentPointSize, 1 );
                    }
                    else
                    {
                        taManager.AverageDailyRange = Math.Round( _latestAvgRange / _instrumentPointSize,  _instrumentDigits );
                    }

                    taManager.TodayRangeToAvgPercentage = ( int ) Bars.Current.CandleLengthAsPip;
                }

            }
        }

        protected void CalculatePricePercentage( bool fullRecalculation, DataBarUpdateType? updateType, int endBarCount, CancellationToken token )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return;
            }

            if ( _currentPeriod != TimeSpan.FromMinutes( 5 ) )
                return;

            double Bar1           = 0;
            double Bar2           = 0;
            double Bar3           = 0;
            double Bar4           = 0;
            double Bar5           = 0;
            double Bar6           = 0;
            double Bar7           = 0;
            double Bar8           = 0;
            double Bar9           = 0;
            double Bar10          = 0;

            double Bar1percent    = 0;
            double Bar2percent    = 0;
            double Bar3percent    = 0;
            double Bar4percent    = 0;
            double Bar5percent    = 0;

            double BarsAverage1   = 0;
            double BarsAverage2   = 0;
            double BarsAverage3   = 0;
            double BarsAverage4   = 0;
            double BarsAverage5   = 0;
            double BarsAllpercent = 0;

            int Bar1Col = 0;
            int Bar2Col = 0;
            int Bar3Col = 0;
            int Bar4Col = 0;
            int Bar5Col = 0;

            int startIndex     = 0;
            int endIndex       = endBarCount - 1;
            int indexCount     = endIndex - startIndex + 1;

            if ( endIndex > 10 )
            {
                if ( Bars[ endIndex ].Close > Bars[ endIndex - 1 ].Close )
                {
                    Bar1 = ( Bars[ endIndex ].Close - Bars[ endIndex - 1 ].Close ) * 100000;
                }
                if ( Bars[ endIndex ].Close < Bars[ endIndex - 1 ].Close )
                {
                    Bar1 = ( Bars[ endIndex - 1 ].Close - Bars[ endIndex ].Close ) * 100000;
                }


                if ( Bars[ endIndex - 1 ].Close > Bars[ endIndex - 2 ].Close )
                {
                    Bar2 = ( Bars[ endIndex - 1 ].Close - Bars[ endIndex - 2 ].Close ) * 100000;
                }
                if ( Bars[ endIndex - 1 ].Close < Bars[ endIndex - 2 ].Close )
                {
                    Bar2 = ( Bars[ endIndex - 2 ].Close - Bars[ endIndex - 1 ].Close ) * 100000;
                }


                if ( Bars[ endIndex - 2 ].Close > Bars[ endIndex - 3 ].Close )
                {
                    Bar3 = ( Bars[ endIndex - 2 ].Close - Bars[ endIndex - 3 ].Close ) * 100000;
                }
                if ( Bars[ endIndex - 2 ].Close < Bars[ endIndex - 3 ].Close )
                {
                    Bar3 = ( Bars[ endIndex - 3 ].Close - Bars[ endIndex - 2 ].Close ) * 100000;
                }



                if ( Bars[ endIndex - 3 ].Close > Bars[ endIndex - 4 ].Close )
                {
                    Bar4 = ( Bars[ endIndex - 3 ].Close - Bars[ endIndex - 4 ].Close ) * 100000;
                }
                if ( Bars[ endIndex - 3 ].Close < Bars[ endIndex - 4 ].Close )
                {
                    Bar4 = ( Bars[ endIndex - 4 ].Close - Bars[ endIndex - 3 ].Close ) * 100000;
                }




                if ( Bars[ endIndex - 4 ].Close > Bars[ endIndex - 5 ].Close )
                {
                    Bar5 = ( Bars[ endIndex - 4 ].Close - Bars[ endIndex - 5 ].Close ) * 100000;
                }
                if ( Bars[ endIndex - 4 ].Close < Bars[ endIndex - 5 ].Close )
                {
                    Bar5 = ( Bars[ endIndex - 5 ].Close - Bars[ endIndex - 4 ].Close ) * 100000;
                }




                if ( Bars[ endIndex - 5 ].Close > Bars[ endIndex - 6 ].Close )
                {
                    Bar6 = ( Bars[ endIndex - 5 ].Close - Bars[ endIndex - 6 ].Close ) * 100000;
                }
                if ( Bars[ endIndex - 5 ].Close < Bars[ endIndex - 6 ].Close )
                {
                    Bar6 = ( Bars[ endIndex - 6 ].Close - Bars[ endIndex - 5 ].Close ) * 100000;
                }



                if ( Bars[ endIndex - 6 ].Close > Bars[ endIndex - 7 ].Close )
                {
                    Bar7 = ( Bars[ endIndex - 6 ].Close - Bars[ endIndex - 7 ].Close ) * 100000;
                }
                if ( Bars[ endIndex - 6 ].Close < Bars[ endIndex - 7 ].Close )
                {
                    Bar7 = ( Bars[ endIndex - 7 ].Close - Bars[ endIndex - 6 ].Close ) * 100000;
                }




                if ( Bars[ endIndex - 7 ].Close > Bars[ endIndex - 8 ].Close )
                {
                    Bar8 = ( Bars[ endIndex - 7 ].Close - Bars[ endIndex - 8 ].Close ) * 100000;
                }
                if ( Bars[ endIndex - 7 ].Close < Bars[ endIndex - 8 ].Close )
                {
                    Bar8 = ( Bars[ endIndex - 8 ].Close - Bars[ endIndex - 7 ].Close ) * 100000;
                }



                if ( Bars[ endIndex - 8 ].Close > Bars[ endIndex - 9 ].Close )
                {
                    Bar9 = ( Bars[ endIndex - 8 ].Close - Bars[ endIndex - 9 ].Close ) * 100000;
                }
                if ( Bars[ endIndex - 8 ].Close < Bars[ endIndex - 9 ].Close )
                {
                    Bar9 = ( Bars[ endIndex - 9 ].Close - Bars[ endIndex - 8 ].Close ) * 100000;
                }


                if ( Bars[ endIndex - 9 ].Close > Bars[ endIndex - 10 ].Close )
                {
                    Bar10 = ( Bars[ endIndex - 9 ].Close - Bars[ endIndex - 10 ].Close ) * 100000;
                }
                if ( Bars[ endIndex - 9 ].Close < Bars[ endIndex - 10 ].Close )
                {
                    Bar10 = ( Bars[ endIndex - 10 ].Close - Bars[ endIndex - 9 ].Close ) * 100000;
                }


                BarsAverage1 = ( Bar2 + Bar3 + Bar4 + Bar5 ) / 4;
                BarsAverage2 = ( Bar3 + Bar4 + Bar5 + Bar6 ) / 4;
                BarsAverage3 = ( Bar4 + Bar5 + Bar6 + Bar7 ) / 4;
                BarsAverage4 = ( Bar5 + Bar6 + Bar7 + Bar8 ) / 4;
                BarsAverage5 = ( Bar6 + Bar7 + Bar8 + Bar9 ) / 4;

                Bar1percent = Math.Round( ( Bar1 / BarsAverage1 ) * 100, 0, MidpointRounding.AwayFromZero );
                Bar2percent = Math.Round( ( Bar2 / BarsAverage2 ) * 100, 0, MidpointRounding.AwayFromZero );
                Bar3percent = Math.Round( ( Bar3 / BarsAverage3 ) * 100, 0, MidpointRounding.AwayFromZero );
                Bar4percent = Math.Round( ( Bar4 / BarsAverage4 ) * 100, 0, MidpointRounding.AwayFromZero );
                Bar5percent = Math.Round( ( Bar5 / BarsAverage5 ) * 100, 0, MidpointRounding.AwayFromZero );

                BarsAllpercent = Math.Round( ( ( Bar2 + Bar3 + Bar4 + Bar5 ) / 4 ) * 100, 0 );

                if ( Bar1percent == 0 ) //Stopped
                {
                    _barSpeedReading = BarSpeed.Stopped;
                }

                if ( Bar1percent > Bar2percent && Bar1percent > 100 && Bar1percent != 0 ) //Speeding Up
                {
                    _barSpeedReading = BarSpeed.SpeedingUp;
                }

                if ( Bar1percent <= 100 && Bar1percent != 0 ) //Steady
                {
                    _barSpeedReading = BarSpeed.Steady;
                }

                if ( Bar1percent < Bar2percent && Bar2percent > 100 && Bar1percent > 100 && Bar1percent != 0 ) //Slowing Down
                {
                    _barSpeedReading = BarSpeed.SlowingDown;
                }


                if ( Bar1percent == 0 || Bars[ endIndex ].Close == Bars[ endIndex - 1 ].Close )
                {
                    Bar1Col = 12;
                }

                if ( Bar1percent < 25 && Bar1percent > 0 )
                {
                    Bar1Col = 11;
                }

                if ( Bar1percent >= 400 )
                {
                    if ( Bars[ endIndex ].Close > Bars[ endIndex - 1 ].Close )
                    {
                        Bar1Col = 1;
                    }
                    else if ( Bars[ endIndex ].Close < Bars[ endIndex - 1 ].Close ) Bar1Col = 10;
                }

                if ( Bar1percent >= 200 && Bar1percent < 400 )
                {
                    if ( Bars[ endIndex ].Close > Bars[ endIndex - 1 ].Close )
                    {
                        Bar1Col = 2;
                    }
                    else if ( Bars[ endIndex ].Close < Bars[ endIndex - 1 ].Close ) Bar1Col = 9;
                }

                if ( Bar1percent >= 100 && Bar1percent < 200 )
                {
                    if ( Bars[ endIndex ].Close > Bars[ endIndex - 1 ].Close )
                    {
                        Bar1Col = 3;
                    }
                    else if ( Bars[ endIndex ].Close < Bars[ endIndex - 1 ].Close ) Bar1Col = 8;
                }

                if ( Bar1percent >= 50 && Bar1percent < 100 )
                {
                    if ( Bars[ endIndex ].Close > Bars[ endIndex - 1 ].Close )
                    {
                        Bar1Col = 4;
                    }
                    else if ( Bars[ endIndex ].Close < Bars[ endIndex - 1 ].Close ) Bar1Col = 7;
                }

                if ( Bar1percent >= 25 && Bar1percent < 50 )
                {
                    if ( Bars[ endIndex ].Close > Bars[ endIndex - 1 ].Close )
                    {
                        Bar1Col = 5;
                    }
                    else if ( Bars[ endIndex ].Close < Bars[ endIndex - 1 ].Close ) Bar1Col = 6;
                }

                //--------------------------------------
                if ( Bar2percent == 0 || Bars[ endIndex - 1 ].Close == Bars[ endIndex - 2 ].Close )
                {
                    Bar2Col = 12;
                }

                if ( Bar2percent < 25 && Bar2percent > 0 )
                {
                    Bar2Col = 11;
                }

                if ( Bar2percent >= 400 )
                {
                    if ( Bars[ endIndex - 1 ].Close > Bars[ endIndex - 2 ].Close )
                    {
                        Bar2Col = 1;
                    }
                    else if ( Bars[ endIndex - 1 ].Close < Bars[ endIndex - 2 ].Close ) Bar2Col = 10;
                }

                if ( Bar2percent >= 200 && Bar2percent < 400 )
                {
                    if ( Bars[ endIndex - 1 ].Close > Bars[ endIndex - 2 ].Close )
                    {
                        Bar2Col = 2;
                    }
                    else if ( Bars[ endIndex - 1 ].Close < Bars[ endIndex - 2 ].Close ) Bar2Col = 9;
                }

                if ( Bar2percent >= 100 && Bar2percent < 200 )
                {
                    if ( Bars[ endIndex - 1 ].Close > Bars[ endIndex - 2 ].Close )
                    {
                        Bar2Col = 3;
                    }
                    else if ( Bars[ endIndex - 1 ].Close < Bars[ endIndex - 2 ].Close ) Bar2Col = 8;
                }

                if ( Bar2percent >= 50 && Bar2percent < 100 )
                {
                    if ( Bars[ endIndex - 1 ].Close > Bars[ endIndex - 2 ].Close )
                    {
                        Bar2Col = 4;
                    }
                    else if ( Bars[ endIndex - 1 ].Close < Bars[ endIndex - 2 ].Close ) Bar2Col = 7;
                }

                if ( Bar2percent >= 25 && Bar2percent < 50 )
                {
                    if ( Bars[ endIndex - 1 ].Close > Bars[ endIndex - 2 ].Close )
                    {
                        Bar2Col = 5;
                    }
                    else if ( Bars[ endIndex - 1 ].Close < Bars[ endIndex - 2 ].Close ) Bar2Col = 6;
                }

                //--------------------------------------
                if ( Bar3percent == 0 || Bars[ endIndex - 2 ].Close == Bars[ endIndex - 3 ].Close )
                {
                    Bar3Col = 12;
                }

                if ( Bar3percent < 25 && Bar3percent > 0 )
                {
                    Bar3Col = 11;
                }

                if ( Bar3percent >= 400 )
                {
                    if ( Bars[ endIndex - 2 ].Close > Bars[ endIndex - 3 ].Close )
                    {
                        Bar3Col = 1;
                    }
                    else if ( Bars[ endIndex - 2 ].Close < Bars[ endIndex - 3 ].Close ) Bar3Col = 10;
                }

                if ( Bar3percent >= 200 && Bar3percent < 400 )
                {
                    if ( Bars[ endIndex - 2 ].Close > Bars[ endIndex - 3 ].Close )
                    {
                        Bar3Col = 2;
                    }
                    else if ( Bars[ endIndex - 2 ].Close < Bars[ endIndex - 3 ].Close ) Bar3Col = 9;
                }

                if ( Bar3percent >= 100 && Bar3percent < 200 )
                {
                    if ( Bars[ endIndex - 2 ].Close > Bars[ endIndex - 3 ].Close )
                    {
                        Bar3Col = 3;
                    }
                    else if ( Bars[ endIndex - 2 ].Close < Bars[ endIndex - 3 ].Close ) Bar3Col = 8;
                }

                if ( Bar3percent >= 50 && Bar3percent < 100 )
                {
                    if ( Bars[ endIndex - 2 ].Close > Bars[ endIndex - 3 ].Close )
                    {
                        Bar3Col = 4;
                    }
                    else if ( Bars[ endIndex - 2 ].Close < Bars[ endIndex - 3 ].Close ) Bar3Col = 7;
                }

                if ( Bar3percent >= 25 && Bar3percent < 50 )
                {
                    if ( Bars[ endIndex - 2 ].Close > Bars[ endIndex - 3 ].Close )
                    {
                        Bar3Col = 5;
                    }
                    else if ( Bars[ endIndex - 2 ].Close < Bars[ endIndex - 3 ].Close ) Bar3Col = 6;
                }

                //--------------------------------------
                if ( Bar4percent == 0 || Bars[ endIndex - 3 ].Close == Bars[ endIndex - 4 ].Close )
                {
                    Bar4Col = 12;
                }

                if ( Bar4percent < 25 && Bar4percent > 0 )
                {
                    Bar4Col = 11;
                }

                if ( Bar4percent >= 400 )
                {
                    if ( Bars[ endIndex - 3 ].Close > Bars[ endIndex - 4 ].Close )
                    {
                        Bar4Col = 1;
                    }
                    else if ( Bars[ endIndex - 3 ].Close < Bars[ endIndex - 4 ].Close ) Bar4Col = 10;
                }

                if ( Bar4percent >= 200 && Bar4percent < 400 )
                {
                    if ( Bars[ endIndex - 3 ].Close > Bars[ endIndex - 4 ].Close )
                    {
                        Bar4Col = 2;
                    }
                    else if ( Bars[ endIndex - 3 ].Close < Bars[ endIndex - 4 ].Close ) Bar4Col = 9;
                }

                if ( Bar4percent >= 100 && Bar4percent < 200 )
                {
                    if ( Bars[ endIndex - 3 ].Close > Bars[ endIndex - 4 ].Close )
                    {
                        Bar4Col = 3;
                    }
                    else if ( Bars[ endIndex - 3 ].Close < Bars[ endIndex - 4 ].Close ) Bar4Col = 8;
                }

                if ( Bar4percent >= 50 && Bar4percent < 100 )
                {
                    if ( Bars[ endIndex - 3 ].Close > Bars[ endIndex - 4 ].Close )
                    {
                        Bar4Col = 4;
                    }
                    else if ( Bars[ endIndex - 3 ].Close < Bars[ endIndex - 4 ].Close ) Bar4Col = 7;
                }

                if ( Bar4percent >= 25 && Bar4percent < 50 )
                {
                    if ( Bars[ endIndex - 3 ].Close > Bars[ endIndex - 4 ].Close )
                    {
                        Bar4Col = 5;
                    }
                    else if ( Bars[ endIndex - 3 ].Close < Bars[ endIndex - 4 ].Close ) Bar4Col = 6;
                }

                //--------------------------------------
                if ( Bar5percent == 0 || Bars[ endIndex - 4 ].Close == Bars[ endIndex - 5 ].Close )
                {
                    Bar5Col = 12;
                }

                if ( Bar5percent < 25 && Bar5percent > 0 )
                {
                    Bar5Col = 11;
                }

                if ( Bar5percent >= 400 )
                {
                    if ( Bars[ endIndex - 4 ].Close > Bars[ endIndex - 5 ].Close )
                    {
                        Bar5Col = 1;
                    }
                    else if ( Bars[ endIndex - 4 ].Close < Bars[ endIndex - 5 ].Close ) Bar5Col = 10;
                }

                if ( Bar5percent >= 200 && Bar5percent < 400 )
                {
                    if ( Bars[ endIndex - 4 ].Close > Bars[ endIndex - 5 ].Close )
                    {
                        Bar5Col = 2;
                    }
                    else if ( Bars[ endIndex - 4 ].Close < Bars[ endIndex - 5 ].Close ) Bar5Col = 9;
                }

                if ( Bar5percent >= 100 && Bar5percent < 200 )
                {
                    if ( Bars[ endIndex - 4 ].Close > Bars[ endIndex - 5 ].Close )
                    {
                        Bar5Col = 3;
                    }
                    else if ( Bars[ endIndex - 4 ].Close < Bars[ endIndex - 5 ].Close ) Bar5Col = 8;
                }

                if ( Bar5percent >= 50 && Bar5percent < 100 )
                {
                    if ( Bars[ endIndex - 4 ].Close > Bars[ endIndex - 5 ].Close )
                    {
                        Bar5Col = 4;
                    }
                    else if ( Bars[ endIndex - 4 ].Close < Bars[ endIndex - 5 ].Close ) Bar5Col = 7;
                }

                if ( Bar5percent >= 25 && Bar5percent < 50 )
                {
                    if ( Bars[ endIndex - 4 ].Close > Bars[ endIndex - 5 ].Close )
                    {
                        Bar5Col = 5;
                    }
                    else if ( Bars[ endIndex - 4 ].Close < Bars[ endIndex - 5 ].Close ) Bar5Col = 6;
                }

                if ( _fxBarPercentBindingList != null && _currentPeriod == TimeSpan.FromMinutes( 5 ) )
                {
                    if ( Bar1Col > 0 ) _fxBarPercentBindingList.AddBarPercentage( 5, Bar1percent, Bar1Col );
                    if ( Bar2Col > 0 ) _fxBarPercentBindingList.AddBarPercentage( 4, Bar2percent, Bar2Col );
                    if ( Bar3Col > 0 ) _fxBarPercentBindingList.AddBarPercentage( 3, Bar3percent, Bar3Col );
                    if ( Bar4Col > 0 ) _fxBarPercentBindingList.AddBarPercentage( 2, Bar4percent, Bar4Col );
                    if ( Bar5Col > 0 ) _fxBarPercentBindingList.AddBarPercentage( 1, Bar5percent, Bar5Col );
                }

                if ( _periodXTaManager != null )
                {
                    _periodXTaManager.BarSpeeding = _barSpeedReading;
                }

            }
        }

        private ThreadSafeDictionary<int, FxBarPercentage> _barNumberToItem = new ThreadSafeDictionary<int, FxBarPercentage>( );

        private FxBarPercentage AddBarPercentage( int barNumber, double percentage, int barColor )
        {
            var barPercentage = _barNumberToItem[ barNumber ];

            barPercentage.BarNumber = barNumber;
            barPercentage.BarPercent = percentage;
            barPercentage.BarColor = barColor;

            return barPercentage;
        }
    }
}