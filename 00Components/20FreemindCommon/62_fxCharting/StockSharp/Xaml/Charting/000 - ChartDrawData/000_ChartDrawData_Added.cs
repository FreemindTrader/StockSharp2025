
using DevExpress.Mvvm.POCO;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using fx.Bars;
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
public partial class ChartDrawDataEx : IChartDrawData
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
            _candleExMap = new Dictionary<IChartCandleElement, ChartDrawDataEx.sCandleEx>();

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
            _candleExMap = new Dictionary<IChartCandleElement, ChartDrawDataEx.sCandleEx>();

            var candleEx = new sCandleEx( bars, range.begin, range.end );

            _candleExMap.Add( candleUI, candleEx );

            return true;
        }

        return false;
    }

    public bool SetCandleSource( IChartCandleElement candleUI, fxHistoricBarsRepo bars, ref SBar singleBar )
    {
        _candleExMap = new Dictionary<IChartCandleElement, ChartDrawDataEx.sCandleEx>();

        var candleEx = new sCandleEx( bars, singleBar.BarIndex, singleBar.BarIndex );

        _candleExMap.Add( candleUI, candleEx );

        return true;
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
}