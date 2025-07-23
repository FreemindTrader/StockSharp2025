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
    [UserFriendlyName("Sma55")]
    public partial class FreemindSma55 : CustomPlatformIndicator
    {
        public FreemindSma55() : base(typeof(FreemindSma55).Name, true, true, true, new string[] { "SMA55" })
        {

        }

        int _timePeriod = 14;
        public int TimePeriod
        {
            get { return _timePeriod; }
            set { _timePeriod = value; }
        }


        public override PlatformIndicator OnSimpleClone()
        {
            var result = new FreemindSma55();

            result._description = _description;
            result._name        = _name;

            return result;
        }

        protected override void OnCalculate(bool fullRecalculation, HistoricBarsUpdateEventArg e)
        {
            var barB4Calculation = Bars.TotalBarCount;
            int sma55Length = IndicatorResult["SMA55"].Count;            

            if ( sma55Length == 0 )
            {
                ProcessNewIndicatorBuffer(barB4Calculation, 55, "SMA55");
            }
            else
            {
                ProcessExistingBuffer(e.UpdateType, barB4Calculation, sma55Length, 8, "SMA55");
            }            
        }

        private void ProcessNewIndicatorBuffer(int barB4Calculation, int smaLength, string emaBuffer)
        {
            int startIndex = 0;
            int endIndex = barB4Calculation - 1;
            int indexCount = endIndex - startIndex + 1;

            var ema = new double[indexCount];

            var outBeginIdx = 0;
            var outNBElement = 0;

            TALib.Core.Sma(Bars, startIndex, endIndex, ema, out outBeginIdx, out outNBElement, smaLength);

            lock ( IndicatorResult )
            {
                IndicatorResult.AddSetValues(emaBuffer, outBeginIdx, outNBElement, true, ema);
            }
        }

        private void ProcessExistingBuffer(DataBarUpdateType? updateType, int barB4Calculation, int resultSetLength, int smaLength, string emaBuffer)
        {
            var startIndex = resultSetLength - 1;
            var endIndex = barB4Calculation - 1;
            var indexCount = endIndex - startIndex + 1;

            if ( endIndex < 0 || indexCount < 0 )
            {
                return;
            }

            int outBeginIdx = 0;
            int outNBElement = 0;

            var ema = new double[indexCount];

            TALib.Core.Sma(Bars, startIndex, endIndex, ema, out outBeginIdx, out outNBElement, smaLength);

            lock ( IndicatorResult )
            {
                IndicatorResult.AddSetValues(emaBuffer, outBeginIdx, outNBElement, true, ema);
            }
        }

        protected override void OnFinalCalculate(bool fullRecalculation, DataBarUpdateType? updateType)
        {
            //throw new NotImplementedException( );
        }


    }
}



