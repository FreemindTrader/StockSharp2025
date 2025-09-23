using Ecng.Collections;
using StockSharp.Algo.Indicators;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Media;

/// <summary>
/// Pattern (from English: pattern — model, sample) — in technical analysis, refers to stable recurring combinations of price data, volume, or indicators. 
/// Pattern analysis is based on one of the axioms of technical analysis: "history repeats itself" — it is believed that recurring data combinations lead to similar results.
/// 
/// Patterns are also called "templates" or "figures" of technical analysis.
/// Patterns are conventionally divided into:
///		Indeterminate(can lead to both continuation and change of the current trend).
///		Continuation patterns of the current trend.
///		Patterns of existing trend reversal.
///		
/// When editing a pattern, each line represents a separate candle. The topmost line is the current candle, accordingly, 
/// the second line is one candle back, the third and subsequent lines are minus 2 and more candles.
/// 
/// The editor uses the following parameters:
///		O - opening price,
///		H - high,
///		L - low,
///		C - closing price,
///		V - volume,
///		OI - open interest,
///		B - candle body,
///		LEN - length of the candle (from high to low),
///		BS - lower shadow of the candle,
///		TS - upper shadow of the candle.
///		With parameters, it is possible to use the following indices (references) to the desired values. For example, for the closing price:
///		
///		C: closing price of the current candle,
///		C1: closing price of the 1st candle after the current one,
///		C2: closing price of the 2nd candle after the current one,
///		pC: closing price of the previous candle,
///		pC1: closing price of the candle before the previous one, All references must be within the range of the current pattern. For example, the range of the 3 Black Crows pattern consists of the current and two previous candles, so referring to the third previous candle is not allowed.
/// </summary>
/// <param name="pattern"></param>
public sealed class CandlePatternElementUiDomain( CandlePatternElement pattern ) : ChartCompentWpfUiDomain<CandlePatternElement>( pattern )
{

    private readonly HashSet<DateTime> _candlePatternsTime = new HashSet<DateTime>();

    private DateTime[] _candlePatternsArray = Array.Empty<DateTime>();

    private ChartCandleElementUiDomain _candlestickUI;

    protected override void Init()
    {
        base.Init();
        InternalInit( false );
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="shouldThrow"></param>
    /// <exception cref="InvalidOperationException"></exception>
    private void InternalInit( bool shouldThrow )
    {
        var candlesUiDomain = DrawingSurface.CandlesCompositeElement?.CandlesViewModel;
        if ( candlesUiDomain == null & shouldThrow )
            throw new InvalidOperationException( "unable to draw candle patterns on a chart without candles" );

        if ( _candlestickUI == candlesUiDomain )
            return;

        _candlestickUI?.RemovePattern( this );
        _candlestickUI = candlesUiDomain;
        _candlestickUI?.AddPattern( this );
    }

    protected override void Clear()
    {
        _candlestickUI?.RemovePattern( this );
        DrawingSurface.Refresh();
    }

    protected override void UpdateUi()
    {
        lock ( _candlePatternsTime )
            _candlePatternsTime.Clear();

        DrawingSurface.Refresh();
    }

    protected override void RootElementPropertyChanged( IChartComponent com, string propName )
    {
        base.RootElementPropertyChanged( com, propName );

        if ( ( propName != "UpColor" ) && ( propName != "DownColor" ) && ( propName != "IsVisible" ) )
            return;

        DrawingSurface.Refresh();
    }

    public override bool Draw( IEnumerableEx<ChartDrawData.IDrawValue> drawData )
    {
        if ( drawData == null || CollectionHelper.IsEmpty( drawData ) )
            return false;

        InternalInit( true );

        var shouldUpdate = false;

        lock ( _candlePatternsTime )
        {
            foreach ( ChartDrawData.IndicatorData indicatorData in drawData )
            {
                CandlePatternIndicatorValue pattern = (CandlePatternIndicatorValue)indicatorData.Value;
                if ( pattern.Value )
                {
                    if ( pattern.IsFinal )
                    {
                        CollectionHelper.ForEach( pattern.CandleOpenTimes, p => shouldUpdate = shouldUpdate | _candlePatternsTime.Add( p.UtcDateTime ) );
                    }
                    else
                    {
                        var patternTimeList = new List<DateTime>();
                        patternTimeList.AddRange( pattern.CandleOpenTimes.Select( p => p.UtcDateTime ) );
                        _candlePatternsArray = patternTimeList.ToArray();
                        shouldUpdate = true;
                    }
                }
                else if ( _candlePatternsArray.Length != 0 )
                {
                    shouldUpdate = true;
                    _candlePatternsArray = Array.Empty<DateTime>();
                }
            }
        }
        if ( shouldUpdate )
            DrawingSurface.Refresh();

        return shouldUpdate;
    }

    public Color? GetCandleColor( DateTime barTime, bool isUp )
    {
        if ( !RootElem.IsVisible || !ChartComponentView.IsVisible )
            return new Color?();

        lock ( _candlePatternsTime )
        {
            if ( !_candlePatternsTime.Contains( barTime ) )
            {
                if ( !( ( IEnumerable<DateTime> ) _candlePatternsArray ).Contains<DateTime>( barTime ) )
                    return new Color?();
            }
        }
        return new Color?( isUp ? ChartComponentView.UpColor : ChartComponentView.DownColor );
    }
}
