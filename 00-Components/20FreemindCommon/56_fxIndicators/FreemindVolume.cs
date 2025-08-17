using System;
using fx.Bars;
using fx.Algorithm;
using System.Drawing;
using fx.Definitions;
using System.Collections.Generic;


#pragma warning disable 414


namespace fx.Indicators
{
    /// <summary>
    /// This indicator is a integrated for all the indicator that will be required for the major daily trend.	
    /// </summary>
    [Serializable]
    [UserFriendlyName( "Chaikin" )]
    public partial class FreemindVolume : CustomPlatformIndicator
    {
        private int? _lookbackCount = null;
        IList<double> High = null;
        IList<double> Low = null;
        IList<double> Close = null;
        

        public FreemindVolume( ) : base( typeof( FreemindVolume ).Name, true, true, false, new string[ ] { "ChaikinVol", "Zero" } )
        {
            // The following will set how the Hew RSI lines look like
            //this.ChartSeries.OutputResultSetsPens[ "ChaikinVol" ] = Pens.Red;
            //this.ChartSeries.OutputResultSetsPens[ "Zero" ] = Pens.Black;            

            //this.ChartSeries.Visible = true;
            //this.ChartSeries.InvolvedInDisplayAreaCalculation = false;
        }

        public override PlatformIndicator OnSimpleClone( )
        {
            var result          = new FreemindVolume();
            result._description = _description;
            result._name        = _name;

            return result;
        }

        protected override void OnCalculate( bool fullRecalculation, HistoricBarsUpdateEventArg e )
        {
            EstablishLookback( );

            int repoStartingIndex = 0;

            if ( fullRecalculation == false )
            {
                repoStartingIndex = Math.Max( 0, IndicatorResult.SetLength - _lookbackCount.Value - 2 );
            }

            int outBeginIdx;
            int outNBElement;

            int startIndex = 0;
            int endIndex   = Bars.TotalBarCount - repoStartingIndex - 1;
            int indexCount = endIndex - startIndex + 1;

            if ( indexCount <= 0 )
            {
                return;
            }
        

            var chaikinFast   = new double [ indexCount ];
            var chaikinSlow   = new double [ indexCount ];
            var chaikinDiff   = new double [ indexCount ];
            var fixedCount    = Math.Max( indexCount, IndicatorResult.SetLength );
            var zero          = MathHelper.CreateFixedLineResultLength(0, fixedCount);  
            
            outBeginIdx       = 0;
            outNBElement     = 0;

            CalcChaikinVol( startIndex, endIndex,  3, out outBeginIdx, out outNBElement, chaikinFast );
            CalcChaikinVol( startIndex, endIndex, 10, out outBeginIdx, out outNBElement, chaikinSlow );


            IndicatorResult.AddSetValues( "ChaikinVol", repoStartingIndex + outBeginIdx, outNBElement, true, chaikinDiff );
            IndicatorResult.AddSetValues( "Zero"      , repoStartingIndex + outBeginIdx, outNBElement, true, zero );            
        }

        public IndicatorReturn CalcChaikinVol( int startIdx, int endIdx, int Length, out int outBegIdx, out int outNBElement, double[ ] outReal )
        {
            outNBElement = 0;
            outBegIdx = 0;

            int outIdx = 0;            

            if ( startIdx < 0 )
            {
                return IndicatorReturn.OutOfRangeStartIndex;
            }

            if ( ( endIdx < 0 ) || ( endIdx < startIdx ) )
            {
                return IndicatorReturn.OutOfRangeEndIndex;
            }
           

            if ( outReal == null )
            {
                return IndicatorReturn.BadParam;
            }

            

            outBegIdx = startIdx;
            outNBElement = outIdx;

            return IndicatorReturn.Success;
        }



        protected override void OnFinalCalculate( bool fullRecalculation, DataBarUpdateType? updateType )
        {
            //throw new NotImplementedException( );
        }

        protected void EstablishLookback( )
        {
            if ( _lookbackCount.HasValue == false )
            {
                lock ( this )
                {
                    _lookbackCount = TALib.Core.RsiLookback( 14 );
                }
            }
        }
    }
}


