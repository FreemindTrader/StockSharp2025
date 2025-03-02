using fx.Common;


using DevExpress.Mvvm;

using fx.Definitions;
using fx.Algorithm;
using System;
using fx.Collections;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using fx.TALib;
using System.Threading;

#pragma warning disable 414

namespace fx.Indicators
{
    public partial class FreemindIndicator : CustomPlatformIndicator
    {
        BBANDSStates _lastBollingerValue = BBANDSStates.Default;

        protected Task BasicBollingerTask( bool fullRecalculation, DataBarUpdateType? updateType, ref PooledList<Task> tasksList )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }


            //ThreadHelper.UpdateThreadName( "BasicBollingerTask" );
                        

            Task first = new Task(() => CalculateBollinger( fullRecalculation, updateType ), IndicatorExitToken);
            Task second = first.ContinueWith(
                                                antecedent =>
                                                {
                                                    if (antecedent.Status == TaskStatus.Faulted)
                                                    {
                                                        this.LogError("CalculateBollinger() caused Exception. Stack = " + antecedent.Exception.InnerException.StackTrace);
                                                    }
                                                    else
                                                    {
                                                        GetBollingerBandCrossOver( );
                                                    }

                                                }, IndicatorExitToken);

            tasksList.Add( first );
            tasksList.Add( second );

            return first;
        }
        public double CurrentMean
        {
            get
            {
                if ( Bars.TotalBarCount > 0 )
                {
                    return IndicatorResult [ "BBMean" ] [ IndicatorResult.SetLength - 1 ];
                }

                return 0;
            }
        }

        public double CurrentInnerUpper
        {
            get
            {
                if ( Bars.TotalBarCount > 0 )
                {
                    return IndicatorResult [ "InnerBBUpper" ] [ IndicatorResult.SetLength - 1 ];
                }

                return 0;
            }
        }

        public double CurrentInnerLower
        {
            get
            {
                if ( Bars.TotalBarCount > 0 )
                {
                    return IndicatorResult [ "InnerBBLower" ] [ IndicatorResult.SetLength - 1 ];
                }

                return 0;
            }
        }

        public double CurrentOuterUpper
        {
            get
            {
                if ( Bars.TotalBarCount > 0 )
                {
                    return IndicatorResult [ "OuterBBUpper" ] [ IndicatorResult.SetLength - 1 ];
                }

                return 0;
            }
        }

        public double CurrentOuterLower
        {
            get
            {
                if ( Bars.TotalBarCount > 0 )
                {
                    return IndicatorResult [ "OuterBBLower" ] [ IndicatorResult.SetLength - 1 ];
                }

                return 0;
            }
        }

        protected BBANDSStates FindPriceStrengthInsideBB( double close, double high, double low, double outerBBUpper, double InnerBBUpper, double mean, double InnerBBLower, double OuterBBLower )
        {
            // For some reason, where there is no price action, the bollinger bands are convergent together.
            if ( ( outerBBUpper - OuterBBLower ) < 0.0001 )
            {
                return BBANDSStates.Default;
            }

            if ( ( double ) close > outerBBUpper )
            {
                if ( ( ( double ) high - outerBBUpper ) >= ( outerBBUpper - ( double ) low ) )
                {
                    return BBANDSStates.SuperStrongBuy;
                }
                else
                {
                    return BBANDSStates.StrongBuy;
                }
            }

            if ( ( double ) close < OuterBBLower )
            {
                if ( ( OuterBBLower - ( double ) low ) >= ( ( double ) high - OuterBBLower ) )
                {
                    return BBANDSStates.SuperStrongSell;
                }
                else
                {
                    return BBANDSStates.StrongSell;
                }
            }

            if ( ( double ) close >= InnerBBUpper )
            {
                if ( ( ( double ) high - InnerBBUpper ) >= ( InnerBBUpper - ( double ) low ) )
                {
                    return BBANDSStates.StrongBuy;
                }
                else
                {
                    return BBANDSStates.Buy;
                }
            }

            if ( ( double ) close <= InnerBBLower )
            {
                if ( ( InnerBBLower - ( double ) low ) >= ( ( double ) high - InnerBBLower ) )
                {
                    return BBANDSStates.StrongSell;
                }
                else
                {
                    return BBANDSStates.Sell;
                }
            }

            if ( ( double ) close >= mean )
            {
                if ( ( ( double ) high - mean ) >= ( mean - ( double ) low ) )
                {
                    return BBANDSStates.Buy;
                }
                else
                {
                    return BBANDSStates.Sell;
                }
            }

            if ( ( double ) close < mean )
            {
                if ( ( mean - ( double ) low ) >= ( ( double ) high - mean ) )
                {
                    return BBANDSStates.Sell;
                }
                else
                {
                    return BBANDSStates.Buy;
                }
            }

            return BBANDSStates.Default;
        }



        private void FindCrossPointAndStrength( )
        {
            int lookbackCount = Core.BbandsLookback( Core.MAType.Ema, 20 );

            lock ( this )
            {
                var barB4Calculation = _barCountBeforeCalculation;

                if ( barB4Calculation >= lookbackCount )
                {
                    for ( int i = _crossBollinger.Count; i < _signalBollinger.Count; i++ )
                    {
                        if (
                                ( _signalBollinger [ i - 3 ].isBuy( ) &&
                                    _signalBollinger [ i - 2 ].isSell( ) &&
                                    _signalBollinger [ i - 1 ].isBuy( ) &&
                                    _signalBollinger [ i ].isBuy( )
                                )
                                ||
                                ( _signalBollinger [ i - 3 ].isSell( ) &&
                                    _signalBollinger [ i - 2 ].isBuy( ) &&
                                    _signalBollinger [ i - 1 ].isSell( ) &&
                                    _signalBollinger [ i ].isSell( )
                                )
                            )
                        {
                            _currentBBandCross = _previousBBandCross;
                            _crossBollinger [ i - 2 ] = _crossBollinger [ i - 3 ];
                            _crossBollinger [ i - 1 ] = _crossBollinger [ i - 2 ] + ( int ) _signalBollinger [ i - 1 ];

                            _crossBollinger.Add( _crossBollinger [ i - 1 ] + ( int ) _signalBollinger [ i ] );
                        }
                        else if ( ( _signalBollinger [ i - 1 ].isBuy( ) && _signalBollinger [ i ].isSell( ) ) ||
                                     _signalBollinger [ i - 1 ].isSell( ) && _signalBollinger [ i ].isBuy( ) )
                        {
                            _crossBollinger.Add( ( int ) _signalBollinger [ i ] - ( int ) _signalBollinger [ i - 1 ] );

                            _previousBBandCross = _currentBBandCross;
                            _currentBBandCross = i;
                        }
                        else
                        {
                            // Here I would love to recalculate the case where one bar temporarily change direction and come back again at the next bar.                            
                            _crossBollinger.Add( _crossBollinger [ i - 1 ] + ( int ) _signalBollinger [ i - 1 ] );
                        }
                    }
                }
            }
        }

        protected void CalculateBollinger( bool fullRecalculation, DataBarUpdateType? updateType )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return;
            }

            
            var barB4Calculation = _barCountBeforeCalculation;


            double[] innerbbmean,
                        innerbbupper,
                        innerbblower,
                        outerbbmean,
                        outerbbupper,
                        outerbblower;
                        

            int repoStartingIndex = 0;
            int resultSetLength   = IndicatorResult["BBMean"].Count;
            
            int outBeginIdx       = 0;
            int outNBElement     = 0;
            
            if ( fullRecalculation == false )
            {
                repoStartingIndex = Math.Max( 0, resultSetLength - Core.BbandsLookback( Core.MAType.Ema, 20 ) - 2 );
            }

            //!+ Get Bollinger Band
            var startIndex = Math.Max( 0, repoStartingIndex - 1 );
            var endIndex = barB4Calculation - 1;
            var indexCount = endIndex - startIndex + 1;

            if ( indexCount < 0 )
                return;

            innerbbmean  = new double [ indexCount ];
            innerbbupper = new double [ indexCount ];
            innerbblower = new double [ indexCount ];
            outerbbmean  = new double [ indexCount ];
            outerbbupper = new double [ indexCount ];
            outerbblower = new double [ indexCount ];


            Core.RetCode code = Core.Bbands(Bars, startIndex, endIndex, innerbbupper, innerbbmean, innerbblower, out outBeginIdx, out outNBElement, Core.MAType.Ema, 20, 1, 1  );


            IndicatorResult.AddSetValues( "BBMean",       outBeginIdx, outNBElement, true, innerbbmean );
            IndicatorResult.AddSetValues( "InnerBBUpper", outBeginIdx, outNBElement, true, innerbbupper );
            IndicatorResult.AddSetValues( "InnerBBLower", outBeginIdx, outNBElement, true, innerbblower );


            outBeginIdx = 0;
            outNBElement = 0;

            Core.Bbands( Bars, startIndex, endIndex, innerbbupper, innerbbmean, innerbblower, out outBeginIdx, out outNBElement, Core.MAType.Ema, 20, 2, 2  );
            
            IndicatorResult.AddSetValues( "OuterBBUpper", outBeginIdx, outNBElement, true, outerbbupper );
            IndicatorResult.AddSetValues( "OuterBBLower", outBeginIdx, outNBElement, true, outerbblower );

            if ( ( IndicatorResult [ "BBMean" ].Count != barB4Calculation ) || ( IndicatorResult [ "InnerBBUpper" ].Count != barB4Calculation ) || ( IndicatorResult [ "OuterBBUpper" ].Count != barB4Calculation ) )
            {

            }            
        }

        protected void GetBollingerBandCrossOver( )
        {
            var innerbbmean = GeneralHelper.EnumerableToArray( IndicatorResult["BBMean"] );

            if ( innerbbmean.Length == 0 )
            {
                return;
            }

            var innerbbupper     = GeneralHelper.EnumerableToArray( IndicatorResult["InnerBBUpper"] );
            var innerbblower     = GeneralHelper.EnumerableToArray( IndicatorResult["InnerBBLower"] );
            var outerbbupper     = GeneralHelper.EnumerableToArray( IndicatorResult["OuterBBUpper"] );
            var outerbblower     = GeneralHelper.EnumerableToArray( IndicatorResult["OuterBBLower"] );

            var barB4Calculation = _barCountBeforeCalculation;

            var calcCount        = Math.Min( barB4Calculation, innerbbmean.Length );

            if ( _indexOfLastBollingBandCross < 0 )
            {
                return;
            }

            int startIndex = Math.Max( Core.BbandsLookback(Core.MAType.Ema, 20 ), _indexOfLastBollingBandCross);
            int endIndex   = calcCount - 2;         // Tony: We need to ignore the last bar as it is still updating
            int indexCount = endIndex - startIndex + 1;

            if ( indexCount < 0 )
            {
                return;
            }


            for ( int i = startIndex; i < endIndex; i++ )
            {
                _lastBollingerValue = FindPriceStrengthInsideBB( Bars[ i ].Close, Bars[ i ].High, Bars[ i ].Low, outerbbupper[ i ], innerbbupper[ i ], innerbbmean[ i ], innerbblower[ i ], outerbblower[ i ] );
                _signalBollinger.TryAdd( i, _lastBollingerValue );
            }

            _fxBBstrengthList?.AddBBstrength( _currentPeriod, (int)_lastBollingerValue, _lastBollingerValue.ToColor( ) );            
        }



        protected void PartialBBandsCrossOver( )
        {
            //int lookbackCount = fx.TALib.Core.BbandsLookback( 20, 2.0, 2.0, Core.MAType.Ema );
            //   
            //   int barIndex = MutltiTimeFrameSessionDataRepo.TotalBarCount - 1;

            //   if ( MutltiTimeFrameSessionDataRepo.TotalBarCount >= lookbackCount )
            //   {
            //       DataBar currentBar = MutltiTimeFrameSessionDataRepo.ImmutableBarsView[ barIndex ];

            //       _signalBollinger.Add(
            //                               FindPriceStrengthInsideBB(
            //                                                           currentBar,
            //                                                           Results[ "OuterBBUpper" ][ barIndex ],
            //                                                           Results[ "InnerBBUpper" ][ barIndex ],
            //                                                           Results[ "BBMean" ][ barIndex ],
            //                                                           Results[ "InnerBBLower" ][ barIndex ],
            //                                                           Results[ "OuterBBLower" ][ barIndex ]
            //                                                           )
            //                           );
            //   }
            //   

            //   FindCrossPointAndStrength();
        }

    }
}
