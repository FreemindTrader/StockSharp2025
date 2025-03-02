using System;
using fx.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using fx.Definitions;
using DevExpress.Mvvm;
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


namespace fx.Indicators
{
    /// <summary>
    /// This indicator is a integrated for all the indicator that will be required for the major daily trend.	
    /// </summary>
    [Serializable]
    [UserFriendlyName( "ATR" )]
    public partial class FreemindATR : CustomPlatformIndicator
    {
        
        
        public FreemindATR( ) : base( typeof( FreemindATR ).Name, true, true, false, new string[ ] { "ATR" } )
        {
                       
        }

        int _timePeriod = 14;
        public int TimePeriod
        {
            get { return _timePeriod; }
            set { _timePeriod = value; }
        }


        public override PlatformIndicator OnSimpleClone( )
        {
            var result          = new FreemindATR();
            
            result._description = _description;
            result._name        = _name;
            
            return result;
        }

        protected override void OnCalculate( bool fullRecalculation, HistoricBarsUpdateEventArg e )
        {
            var barB4Calculation  = Bars.TotalBarCount;
            int resultSetLength   = IndicatorResult["ATR"].Count;

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

            var atr           = new double [ indexCount ];
            
            var outBeginIdx   = 0;
            var outNBElement = 0;

            TALib.Core.RetCode code = TALib.Core.Atr(Bars, startIndex, endIndex, atr, out outBeginIdx, out outNBElement, TimePeriod );

            lock ( IndicatorResult )
            {
                IndicatorResult.AddSetValues( "ATR", outBeginIdx, outNBElement, true, atr );                
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

            var atr           = new double [ indexCount ];

            TALib.Core.RetCode code = TALib.Core.Atr( Bars, startIndex, endIndex, atr, out outBeginIdx, out outNBElement, TimePeriod );            

            lock ( IndicatorResult )
            {
                IndicatorResult.AddSetValues( "ATR", outBeginIdx, outNBElement, true, atr );
            }            
        }

        
     



        protected override void OnFinalCalculate( bool fullRecalculation, DataBarUpdateType? updateType )
        {
            //throw new NotImplementedException( );
        }


    }
}






