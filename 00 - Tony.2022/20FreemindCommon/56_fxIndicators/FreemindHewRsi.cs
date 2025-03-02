using System;
using fx.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using fx.TALib;
using fx.Definitions;
using fx.Algorithm;
using fx.Bars;

#pragma warning disable 414

/// <summary>
/// Tony Lam
/// My understanding of the code is like this
/// 
/// 1) For a period of 14 bars.
///     if the total UpSum ( if we have a higher high and the two bar low difference is less than the two bar high difference, we add to the Upsum
///     
///     On the other hand, if we have a Lower low, and the two bar high difference is less than the two bar low difference, we add to the DownSum
/// </summary>

/*
 * inputs : Length(NumericSimple);
Vars   : counter(0),UpSum(0),DownSum(0),RS(0);


UpSum = 0;
DownSum = 0; 
for counter = 0 to Length-1 	begin

    // [ ] Square Brackets: Used to reference previous values of data-related reserved words, calculations, and functions. 
    //                      (e.g. [0] = the current bar, [1] = of 1 bar ago, and so on; used to specify an array variable element.
    //    Usage Example:
    //    Plot1(High[1]); // High of one bar ago

	UpSum = UpSum + IFF(High[counter] - High[counter+1] > 0 and High[counter] - High[counter+1] > Low[counter+1] - Low[counter], High[counter] - High[counter+1], 0) ;
	DownSum = DownSum + IFF(Low[counter+1] - Low[counter] > 0 and Low[counter+1] - Low[counter] > High[counter] - High[counter+1], Low[counter+1] - Low[counter], 0 )  ;
end; 

if UpSum+DownSum <> 0 then
	RS = 100 * UpSum / (UpSum + DownSum)
else
	RS = 0;
HEW_RSI = RS ;


 * 
 * 
 * 
 * */

namespace fx.Indicators
{
    /// <summary>
    /// This indicator is a integrated for all the indicator that will be required for the major daily trend.	
    /// </summary>
    [Serializable]
    [UserFriendlyName( "HewRsi" )]
    public partial class FreemindHewRsi : CustomPlatformIndicator
    {
        private int _lookback = -1;
        
        public FreemindHewRsi( ) : base( typeof( FreemindHewRsi ).Name, true, true, false, new string[ ] { "FreemindRsi", "RsiOverBought", "RsiOverSold" } )
        {
            // The following will set how the Hew RSI lines look like
            //this.ChartSeries.OutputResultSetsPens[ "FreemindRsi" ]   = Pens.Red;
            //this.ChartSeries.OutputResultSetsPens[ "RsiOverSold" ]   = Pens.Green;
            //this.ChartSeries.OutputResultSetsPens[ "RsiOverBought" ] = Pens.MediumVioletRed;

            //this.ChartSeries.Visible                                 = true;
            //this.ChartSeries.InvolvedInDisplayAreaCalculation        = true;

            _lookback = Core.RsiLookback( 14 );
        }

        public override PlatformIndicator OnSimpleClone( )
        {
            var result          = new FreemindHewRsi();
            result._description = _description;
            result._name        = _name;

            return result;
        }

        protected override void OnCalculate( bool fullRecalculation, HistoricBarsUpdateEventArg e )
        {
            var barB4Calculation  = Bars.TotalBarCount;
            int resultSetLength   = IndicatorResult["FreemindRsi"].Count;

            if ( resultSetLength == 0 )
            {
                ProcessNewIndicatorBuffer( barB4Calculation );
            }
            else
            {
                ProcessExistingBuffer( e.UpdateType, barB4Calculation, resultSetLength );
            }
        }        

        private void ProcessNewIndicatorBuffer( int barB4Calculation )
        {
            int startIndex    = 0;
            int endIndex      = barB4Calculation - 1;
            int indexCount    = endIndex - startIndex + 1;

            var RS            = new double [ indexCount ];

            var fixedCount    = Math.Max( indexCount, IndicatorResult.SetLength );

            var overSold      = MathHelper.CreateFixedLineResultLength(30, fixedCount);
            var overBought    = MathHelper.CreateFixedLineResultLength(70, fixedCount);

            var outBeginIdx   = 0;
            var outNBElement = 0;

            HewRsi( startIndex, endIndex, Bars,  14, out outBeginIdx, out outNBElement, RS );

            lock( IndicatorResult )
            {
                IndicatorResult.AddSetValues( "FreemindRsi"  , outBeginIdx, outNBElement, true, RS );
                IndicatorResult.AddSetValues( "RsiOverBought", outBeginIdx, outNBElement, true, overBought );
                IndicatorResult.AddSetValues( "RsiOverSold"  , outBeginIdx, outNBElement, true, overSold );
            }
        }

        private void ProcessExistingBuffer( DataBarUpdateType? updateType, int barB4Calculation, int resultSetLength )
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

            var RS            = new double [ indexCount ];

            var overSold      = MathHelper.CreateFixedLineResultLength(30, indexCount);
            var overBought    = MathHelper.CreateFixedLineResultLength(70, indexCount);

            HewRsi( startIndex, endIndex, Bars, 14, out outBeginIdx, out outNBElement, RS );

            lock ( IndicatorResult )
            {
                IndicatorResult.AddSetValues( "FreemindRsi"  , outBeginIdx, outNBElement, true, RS );
                IndicatorResult.AddSetValues( "RsiOverBought", outBeginIdx, outNBElement, true, overBought );
                IndicatorResult.AddSetValues( "RsiOverSold"  , outBeginIdx, outNBElement, true, overSold );
            }
        }

        //protected void OnCalculate2( bool fullRecalculation, DataBarUpdateType? updateType )
        //{
        //    EstablishLookback( );

        //    int repoStartingIndex = 0;
        //    
        //    //if ( fullRecalculation == false )
        //    //{
        //    //    repoStartingIndex = Math.Max( 0, IndicatorResult.SetLength - _lookback.Value - 2 );
        //    //}

        //    int outBeginIdx;
        //    int outNBElement;

        //    int startIndex = 0;
        //    int endIndex   = DatabarsRepo.TotalBarCount - repoStartingIndex - 1;
        //    int indexCount = endIndex - startIndex + 1;

        //    if( indexCount <= 0 )
        //        return;

        

        //    var RS         = new double [ indexCount ];

        //    var fixedCount = Math.Max( indexCount, IndicatorResult.SetLength );

        //    var overSold   = MathHelper.CreateFixedLineResultLength(30, fixedCount);
        //    var overBought = MathHelper.CreateFixedLineResultLength(70, fixedCount);

        //    outBeginIdx   = 0;
        //    outNBElement = 0;

        //    //HewRsi( startIndex, endIndex, 14, out outBeginIdx, out outNBElement, RS );

        //    IndicatorResult.AddSetValues( "FreemindRsi",   repoStartingIndex + outBeginIdx, outNBElement, true, RS );
        //    IndicatorResult.AddSetValues( "RsiOverBought", repoStartingIndex + outBeginIdx, outNBElement, true, overBought );
        //    IndicatorResult.AddSetValues( "RsiOverSold",   repoStartingIndex + outBeginIdx, outNBElement, true, overSold );
        //}

        public IndicatorReturn HewRsi( int startIdx, int endIdx, IHistoricBarsRepo bars, int optInTimePeriod, out int outBegIdx, out int outNBElement, double [] outReal )
        {            
            outNBElement = 0;
            outBegIdx    = 0;

            int outIdx;

            int today, lookbackTotal, unstablePeriod, i;
            double prevGain, prevLoss;
            double previousHigh, previousLow, currentHigh, currentLow;
            
            double lowDiff, highDiff;

            if ( startIdx < 0 )
            {
                return IndicatorReturn.OutOfRangeStartIndex;
            }

            if ( ( endIdx < 0 ) || ( endIdx < startIdx ) )
            {
                return IndicatorReturn.OutOfRangeEndIndex;
            }
            

            if ( optInTimePeriod <= 0 )
            {
                optInTimePeriod = 14;
            }
            else if ( ( optInTimePeriod < 2 ) || ( optInTimePeriod > 100000 ) )
            {
                return IndicatorReturn.BadParam;
            }

            if ( outReal == null )
            {
                return IndicatorReturn.BadParam;
            }

            lookbackTotal = Core.RsiLookback( 14 );

            if ( startIdx < lookbackTotal )
            {
                startIdx = lookbackTotal;
            }

            if ( startIdx > endIdx )
            {
                return IndicatorReturn.Success;
            }

            unstablePeriod = ( int )Core.GetUnstablePeriod( Core.FuncUnstId.Rsi );

            outIdx         = 0;            

            today          = startIdx - lookbackTotal;
            previousHigh   = bars[ today ].High;
            previousLow    = bars[ today ].Low;
            
            prevGain       = 0.0;
            prevLoss       = 0.0;

            today++;

            for ( i = optInTimePeriod; i > 0; i-- )
            {
                currentHigh  = bars[ today ].High;
                currentLow   = bars[ today ].Low;
                highDiff     = currentHigh - previousHigh;
                lowDiff      = previousLow - currentLow;
                previousHigh = currentHigh;
                previousLow  = currentLow;
                today++;

                if ( ( highDiff > 0 ) && ( highDiff > lowDiff ) )
                {
                    prevGain = prevGain + highDiff;
                }

                if ( ( lowDiff > 0 ) && ( lowDiff > highDiff ) )
                {
                    prevLoss = prevLoss + lowDiff;
                }                               
            }

            /* Subsequent DownSum and UpSum are smoothed
                * using the previous values (Wilder's approach).
                *  1) Multiply the previous by 'period-1'. 
                *  2) Add barIndex value.
                *  3) Divide by 'period'.
                */
            prevLoss /= optInTimePeriod;
            prevGain /= optInTimePeriod;

            /* Often documentation present the RSI calculation as follow:
             *    RSI = 100 - (100 / 1 + (UpSum/DownSum))
             *
             * The following is equivalent:
             *    RSI = 100 * (UpSum/(UpSum+DownSum))
             *
             * The second equation is used here for speed optimization.
             */
            if ( today > startIdx )
            {                
                if ( prevGain + prevLoss != 0 )
                {
                    outReal[ outIdx++ ] = 100.0 * ( prevGain / ( prevGain + prevLoss ) );
                }                    
                else
                {
                    outReal[ outIdx++ ] = 0.0;
                }                    
            }
            else
            {
                /* Skip the unstable period. Do the processing 
                 * but do not write it in the output.
                 */
                while ( today < startIdx )
                {
                    currentHigh  = bars[ today ].High;
                    currentLow   = bars[ today ].Low;
                    highDiff     = currentHigh - previousHigh;
                    lowDiff      = previousLow - currentLow;
                    previousHigh = currentHigh;
                    previousLow  = currentLow;
                    

                    // This is very important. This is equivalent to dropping the first element of past optInTimePeriod for averaging
                    prevLoss    *= ( optInTimePeriod - 1 );
                    prevGain    *= ( optInTimePeriod - 1 );


                    if ( ( highDiff > 0 ) && ( highDiff > lowDiff ) )
                    {
                        prevGain = prevGain + highDiff;
                    }

                    if ( ( lowDiff > 0 ) && ( lowDiff > highDiff ) )
                    {
                        prevLoss = prevLoss + lowDiff;
                    }

                    prevLoss /= optInTimePeriod;
                    prevGain   /= optInTimePeriod;

                    today++;
                }
            }

            /* Unstable period skipped... now continue
             * processing if needed.
             */
            while ( today <= endIdx )
            {
                currentHigh  = bars[ today ].High;
                currentLow   = bars[ today ].Low;
                highDiff     = currentHigh - previousHigh;
                lowDiff      = previousLow - currentLow;
                previousHigh = currentHigh;
                previousLow  = currentLow;
                today++;

                prevLoss     *= ( optInTimePeriod - 1 );
                prevGain     *= ( optInTimePeriod - 1 );

                if ( ( highDiff > 0 ) && ( highDiff > lowDiff ) )
                {
                    prevGain = prevGain + highDiff;
                }

                if ( ( lowDiff > 0 ) && ( lowDiff > highDiff ) )
                {
                    prevLoss = prevLoss + lowDiff;
                }

                prevLoss /= optInTimePeriod;
                prevGain   /= optInTimePeriod;


                if ( prevGain + prevLoss != 0 )
                {
                    outReal[ outIdx++ ] = 100.0 * ( prevGain / ( prevGain + prevLoss ) );
                }
                else
                {
                    outReal[ outIdx++ ] = 0.0;
                }
            }

            outBegIdx    = startIdx;
            outNBElement = outIdx;

            return IndicatorReturn.Success;
        }

        

        protected override void OnFinalCalculate( bool fullRecalculation, DataBarUpdateType? updateType )
        {
            //throw new NotImplementedException( );
        }

        
    }
}
