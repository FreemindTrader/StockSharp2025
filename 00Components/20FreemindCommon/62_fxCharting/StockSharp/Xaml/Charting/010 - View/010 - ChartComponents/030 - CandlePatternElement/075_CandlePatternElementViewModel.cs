// Decompiled with JetBrains decompiler
// Type: #=zGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

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
public sealed class CandlePatternElementViewModel( CandlePatternElement pattern) : ChartCompentWpfBaseViewModel<CandlePatternElement>(pattern)
{

	private readonly HashSet<DateTime> _dateTimeHashSet = new HashSet<DateTime>();

	private DateTime[] _dateTimeArray = Array.Empty<DateTime>();

	private ChartCandleElementViewModel _candlestickUI;

	protected override void Init()
	{
		base.Init();
		InternalInit(false);
	}


	/// <summary>
	/// 
	/// </summary>
	/// <param name="shouldThrow"></param>
	/// <exception cref="InvalidOperationException"></exception>
	private void InternalInit(bool shouldThrow)
	{
		ChartCandleElementViewModel candlesVM = DrawingSurface.CandlesCompositeElement?.CandlesViewModel;
		if ( candlesVM == null & shouldThrow )
			throw new InvalidOperationException("unable to draw candle patterns on a chart without candles");

		if ( _candlestickUI == candlesVM )
			return;
		_candlestickUI?.RemovePattern(this);
		_candlestickUI = candlesVM;
		_candlestickUI?.AddPattern(this);
	}

	protected override void Clear()
	{
		_candlestickUI?.RemovePattern(this);
		DrawingSurface.Refresh();
	}

	protected override void UpdateUi()
	{
		lock ( _dateTimeHashSet )
			_dateTimeHashSet.Clear();

		DrawingSurface.Refresh();
	}

	protected override void RootElementPropertyChanged( IChartComponent com, string propName)
	{
		base.RootElementPropertyChanged(com, propName);

		if ( !( propName == "UpColor" ) && !( propName == "DownColor" ) && !( propName == "IsVisible" ) )
			return;
		
		DrawingSurface.Refresh();
	}

	public override bool Draw(IEnumerableEx<ChartDrawData.IDrawValue> data)
	{		
		if ( data == null || CollectionHelper.IsEmpty( data ) )
			return false;
		InternalInit(true);		

        var shouldUpdate = false;

		lock ( _dateTimeHashSet )
		{
			foreach ( ChartDrawData.IndicatorData indicatorData in data )
			{
				CandlePatternIndicatorValue pattern = (CandlePatternIndicatorValue)indicatorData.Value;
				if ( pattern.Value )
				{
					if ( pattern.IsFinal )
					{
						CollectionHelper.ForEach( pattern.CandleOpenTimes,  p => shouldUpdate = shouldUpdate | this._dateTimeHashSet.Add(p.UtcDateTime) );
					}
					else
					{
						var dateTimeList = new List<DateTime>();
						dateTimeList.AddRange(pattern.CandleOpenTimes.Select( p => p.UtcDateTime ));
						_dateTimeArray = dateTimeList.ToArray();
                        shouldUpdate = true;
					}
				}
				else if ( _dateTimeArray.Length != 0 )
				{
                    shouldUpdate = true;
					_dateTimeArray = Array.Empty<DateTime>();
				}
			}
		}
		if ( shouldUpdate )
			DrawingSurface.Refresh();

		return shouldUpdate;
	}

	public Color? GetCandleColor(DateTime barTime, bool isUp)
	{
		if ( !RootElem.IsVisible || !ChartComponentView.IsVisible )
			return new Color?();
		
		lock ( _dateTimeHashSet )
		{
			if ( !_dateTimeHashSet.Contains(barTime) )
			{
				if ( !( (IEnumerable<DateTime>)_dateTimeArray ).Contains<DateTime>(barTime) )
					return new Color?();
			}
		}
		return new Color?(isUp ? ChartComponentView.UpColor : ChartComponentView.DownColor);
	}
}
