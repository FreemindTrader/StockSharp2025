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
using System.Windows.Media;

#nullable disable
public sealed class CandlePatternElementViewModel(
  CandlePatternElement _param1) :
  ChartCompentWpfBaseViewModel<CandlePatternElement>(_param1)
{

	private readonly HashSet<DateTime> _dateTimeHashSet = new HashSet<DateTime>();

	private DateTime[] _dateTimeArray = Array.Empty<DateTime>();

	private ChartCandleElementViewModel _candlestickUI;

	protected override void Init()
	{
		base.Init();
		this.InternalInit(false);
	}

	private void InternalInit(bool _param1)
	{
		ChartCandleElementViewModel candles = this.DrawingSurface.CandlesCompositeElement?.Candles;
		if ( candles == null & _param1 )
			throw new InvalidOperationException("unable to draw candle patterns on a chart without candles");
		if ( this._candlestickUI == candles )
			return;
		this._candlestickUI?.RemovePattern(this);
		this._candlestickUI = candles;
		this._candlestickUI?.AddPattern(this);
	}

	protected override void Clear()
	{
		this._candlestickUI?.RemovePattern(this);
		this.DrawingSurface.Refresh();
	}

	protected override void UpdateUi()
	{
		lock ( this._dateTimeHashSet )
			this._dateTimeHashSet.Clear();
		this.DrawingSurface.Refresh();
	}

	protected override void RootElementPropertyChanged(
	  IChartComponent _param1,
	  string _param2)
	{
		base.RootElementPropertyChanged(_param1, _param2);
		if ( !( _param2 == "UpColor" ) && !( _param2 == "DownColor" ) && !( _param2 == "IsVisible" ) )
			return;
		this.DrawingSurface.Refresh();
	}

	public override bool Draw(IEnumerableEx<ChartDrawData.IDrawValue> _param1)
	{
		CandlePatternElementViewModel.SomeClass34343333 zeaY3Uu1m4CyxerxRw = new CandlePatternElementViewModel.SomeClass34343333();
		zeaY3Uu1m4CyxerxRw._variableSome3535 = this;
		if ( _param1 == null || CollectionHelper.IsEmpty<ChartDrawData.IDrawValue>((IEnumerable<ChartDrawData.IDrawValue>)_param1) )
			return false;
		this.InternalInit(true);
		zeaY3Uu1m4CyxerxRw._memeber01 = false;
		lock ( this._dateTimeHashSet )
		{
			foreach ( ChartDrawData.IndicatorData indicatorData in (IEnumerable<ChartDrawData.IDrawValue>)_param1 )
			{
				CandlePatternIndicatorValue patternIndicatorValue = (CandlePatternIndicatorValue)indicatorData.Value;
				if ( patternIndicatorValue.Value )
				{
					if ( patternIndicatorValue.IsFinal )
					{
						CollectionHelper.ForEach<DateTimeOffset>((IEnumerable<DateTimeOffset>)patternIndicatorValue.CandleOpenTimes, zeaY3Uu1m4CyxerxRw._memeber02 ?? ( zeaY3Uu1m4CyxerxRw._memeber02 = new Action<DateTimeOffset>(zeaY3Uu1m4CyxerxRw.Method00222) ));
					}
					else
					{
						List<DateTime> dateTimeList = new List<DateTime>();
						dateTimeList.AddRange(( (IEnumerable<DateTimeOffset>)patternIndicatorValue.CandleOpenTimes ).Select<DateTimeOffset, DateTime>(CandlePatternElementViewModel.SomeClass34343383._memeber01 ?? ( CandlePatternElementViewModel.SomeClass34343383._memeber01 = new Func<DateTimeOffset, DateTime>(CandlePatternElementViewModel.SomeClass34343383.SomeMethond0343.Method00221) )));
						this._dateTimeArray = dateTimeList.ToArray();
						zeaY3Uu1m4CyxerxRw._memeber01 = true;
					}
				}
				else if ( this._dateTimeArray.Length != 0 )
				{
					zeaY3Uu1m4CyxerxRw._memeber01 = true;
					this._dateTimeArray = Array.Empty<DateTime>();
				}
			}
		}
		if ( zeaY3Uu1m4CyxerxRw._memeber01 )
			this.DrawingSurface.Refresh();
		return zeaY3Uu1m4CyxerxRw._memeber01;
	}

	public Color? GetCandleColor(DateTime barTime, bool isUp)
	{
		if ( !this.RootElem.IsVisible || !this.ChartComponentView.IsVisible )
			return new Color?();
		
		lock ( this._dateTimeHashSet )
		{
			if ( !this._dateTimeHashSet.Contains(barTime) )
			{
				if ( !( (IEnumerable<DateTime>)this._dateTimeArray ).Contains<DateTime>(barTime) )
					return new Color?();
			}
		}
		return new Color?(isUp ? this.ChartComponentView.UpColor : this.ChartComponentView.DownColor);
	}

	[Serializable]
	private new sealed class SomeClass34343383
	{
		public static readonly CandlePatternElementViewModel.SomeClass34343383 SomeMethond0343 = new CandlePatternElementViewModel.SomeClass34343383();
		public static Func<DateTimeOffset, DateTime> _memeber01;

		public DateTime Method00221(DateTimeOffset _param1)
		{
			return _param1.UtcDateTime;
		}
	}

	private sealed class SomeClass34343333
	{
		public bool _memeber01;
		public CandlePatternElementViewModel _variableSome3535;
		public Action<DateTimeOffset> _memeber02;

		public void Method00222(DateTimeOffset _param1)
		{
			this._memeber01 |= this._variableSome3535._dateTimeHashSet.Add(_param1.UtcDateTime);
		}
	}
}
