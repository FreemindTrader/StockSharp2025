
using DevExpress.Mvvm.POCO;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using fx.Bars;
using SciChart.Charting.Model.DataSeries;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;

#nullable disable
namespace StockSharp.Xaml.Charting;

/// <summary>
/// The followings are the changed code that I have created to accompany my own logic
/// </summary>
public partial class ChartDrawData : IChartDrawData
{
    private Dictionary<IChartCandleElement, sCandleEx> _candleExMap;

    private Dictionary<IChartIndicatorElement, IndicatorValuesList> _indicatorExMap;

    private Dictionary<IChartIndicatorElement, IndicatorValuesList> GetMyIndicatorMap()
    {
        return _indicatorExMap ?? ( _indicatorExMap = new Dictionary<IChartIndicatorElement, IndicatorValuesList>() );
    }



    private Dictionary<IChartCandleElement, sCandleEx> GetMyCandleMap()
    {
        return _candleExMap ?? ( _candleExMap = new Dictionary<IChartCandleElement, sCandleEx>() );
    }

    public IndicatorValuesList SetIndicatorSource( IChartIndicatorElement indicatorUI, int capacity )
    {
        var indicatorMap = GetMyIndicatorMap();

        if ( indicatorMap.ContainsKey( indicatorUI ) )
        {
            return indicatorMap[ indicatorUI ];
        }
        else
        {
            var indicatorList = new IndicatorValuesList( capacity );

            indicatorMap.Add( indicatorUI, indicatorList );

            return indicatorList;
        }
    }

    public bool SetCandleSource( IChartCandleElement candleUI, fxHistoricBarsRepo bars, uint begin, uint end )
    {
        if ( end - begin + 1 > 0 )
        {
            _candleExMap = new Dictionary<IChartCandleElement, ChartDrawData.sCandleEx>();

            var candleEx = new sCandleEx( bars, begin, end );

            _candleExMap.Add( candleUI, candleEx );

            return true;
        }

        return false;
    }


    public bool SetCandleSource( IChartCandleElement candleUI, fxHistoricBarsRepo bars, (uint begin, uint end) range )
    {
        if ( range != default )
        {
            _candleExMap = new Dictionary<IChartCandleElement, ChartDrawData.sCandleEx>();

            var candleEx = new sCandleEx( bars, range.begin, range.end );

            _candleExMap.Add( candleUI, candleEx );

            return true;
        }

        return false;
    }

    public bool SetCandleSource( IChartCandleElement candleUI, fxHistoricBarsRepo bars, ref SBar singleBar )
    {
        _candleExMap = new Dictionary<IChartCandleElement, ChartDrawData.sCandleEx>();

        var candleEx = new sCandleEx( bars, singleBar.BarIndex, singleBar.BarIndex );

        _candleExMap.Add( candleUI, candleEx );

        return true;
    }

    public bool HasCandleData
    {
        get
        {
            return _candleExMap != null;
        }
    }

    public sCandleEx GetCandle( IChartCandleElement ui )
    {
        return _candleExMap.TryGetValue( ui );
    }

    public class sCandleEx : IDrawValue
    {
        public fxHistoricBarsRepo BarRepo
        {
            get; set;
        }
        public uint StartIndex { get; set; } = uint.MaxValue;
        public uint EndIndex { get; set; } = 0;

        public sCandleEx()
        {
            BarRepo = null;

            StartIndex = uint.MaxValue;

            EndIndex = 0;
        }

        public sCandleEx( fxHistoricBarsRepo bars, uint begin, uint end )
        {
            BarRepo = bars;

            StartIndex = begin;

            EndIndex = end;
        }

        public void Add( fxHistoricBarsRepo bars, uint index )
        {
            BarRepo = bars;

            if ( index < StartIndex ) StartIndex = index;

            if ( index > EndIndex ) EndIndex = index;

        }

        public bool IsSet
        {
            get
            {
                return BarRepo != null;
            }

        }

        public int Count
        {
            get
            {
                if ( EndIndex == 0 ) return 0;
                return ( int ) ( EndIndex - StartIndex + 1 );
            }
        }
    }

    public struct sCandle : IDrawValue
    {
        private readonly DateTime _utcTime;
        private readonly object _candleArg;
        private readonly double _openPrice;
        private readonly double _highPrice;
        private readonly double _lowPrice;
        private readonly double _closePrice;
        private readonly CandlePriceLevel[ ] _candlePriceLevel;
        private readonly double? _priceStep;
        private readonly IPointMetadata  _advancedTAinfo;

        public sCandle(
                          DateTimeOffset barTime,
                          object arg,
                          Decimal open,
                          Decimal high,
                          Decimal low,
                          Decimal close,
                          IEnumerable<CandlePriceLevel> priceLvls,
                          Decimal? priceStep
                     )
        {
            _utcTime = barTime.UtcDateTime;
            _candleArg = arg;
            _openPrice = ( double ) open;
            _highPrice = ( double ) high;
            _lowPrice = ( double ) low;
            _closePrice = ( double ) close;
            _candlePriceLevel = priceLvls != null ? priceLvls.ToArray() : null;
            _priceStep = priceStep.HasValue ? new double?( ( double ) priceStep.GetValueOrDefault() ) : new double?();
            _advancedTAinfo = null;
        }

        public sCandle( DateTimeOffset _param1, DataType _param2, Decimal _param3, Decimal _param4, Decimal _param5, Decimal _param6, IEnumerable<CandlePriceLevel> _param7 )
        {
            _utcTime = _param1.UtcDateTime;
            _candleArg = _param2 ?? throw new ArgumentNullException( "dataType" );
            _openPrice = ( double ) _param3;
            _highPrice = ( double ) _param4;
            _lowPrice = ( double ) _param5;
            _closePrice = ( double ) _param6;
            _candlePriceLevel = _param7 != null ? _param7.ToArray<CandlePriceLevel>() : ( CandlePriceLevel[ ] ) null;
        }

        //_lastBarTime, bar.Candle.Arg, bar.Open, bar.High, bar.Low, bar.Close, bar.Candle.PriceLevels, ( double )bar.Candle.Security.PriceStep, ( IPointMetadata )bar
        public sCandle( ref SBar bar )
        {
            _utcTime = bar.BarTime;
            _candleArg = bar.SymbolEx.Period;
            _openPrice = bar.Open;
            _highPrice = bar.High;
            _lowPrice = bar.Low;
            _closePrice = bar.Close;
            _candlePriceLevel = null;
            _priceStep = bar.SymbolEx.PriceStep;
            _advancedTAinfo = bar;
        }

        public sCandle( DateTimeOffset utcTime, object candleArg, double openPrice, double highPrice, double lowPrice, double closePrice, IEnumerable<CandlePriceLevel> priceLvls, double? priceStep, IPointMetadata advancedTAInfo )
        {
            _utcTime = utcTime.UtcDateTime;
            _candleArg = candleArg;
            _openPrice = openPrice;
            _highPrice = highPrice;
            _lowPrice = lowPrice;
            _closePrice = closePrice;
            _candlePriceLevel = priceLvls != null ? priceLvls.ToArray() : null;
            _priceStep = priceStep;
            _advancedTAinfo = advancedTAInfo;
        }


        public DateTime UtcTime()
        {
            return _utcTime;
        }

        public IPointMetadata AdvancedTAInfo()
        {
            return _advancedTAinfo;
        }

        public object CandleArg()
        {
            return _candleArg;
        }


        public double OpenPrice()
        {
            return _openPrice;
        }


        public double HighPrice()
        {
            return _highPrice;
        }


        public double LowPrice()
        {
            return _lowPrice;
        }


        public double ClosePrice()
        {
            return _closePrice;
        }


        public CandlePriceLevel[ ] CandlePriceLevel()
        {
            return _candlePriceLevel;
        }


        public double? PriceStep()
        {
            return _priceStep;
        }
    }


    
}