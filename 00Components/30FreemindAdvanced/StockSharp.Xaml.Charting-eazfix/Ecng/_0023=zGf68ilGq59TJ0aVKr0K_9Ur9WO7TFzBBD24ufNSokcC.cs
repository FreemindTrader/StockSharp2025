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
public sealed class \u0023\u003DzGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg\u003D\u003D(
  CandlePatternElement _param1) : 
  ChartCompentWpfBaseViewModel<CandlePatternElement>(_param1)
{
  
  private readonly HashSet<DateTime> \u0023\u003DzcU\u0024Li0iEvziI = new HashSet<DateTime>();
  
  private DateTime[] \u0023\u003Dzn6iWmQ_uwD0z = Array.Empty<DateTime>();
  
  private CandlestickUI \u0023\u003DzA4B\u0024PS40sQTxmCO6Ew\u003D\u003D;

  protected override void Init()
  {
    base.Init();
    this.\u0023\u003Dzsinxw0lApF067xCcMw\u003D\u003D(false);
  }

  private void \u0023\u003Dzsinxw0lApF067xCcMw\u003D\u003D(bool _param1)
  {
    CandlestickUI candles = this.ScichartSurfaceMVVM.CandlesCompositeElement?.Candles;
    if (candles == null & _param1)
      throw new InvalidOperationException("unable to draw candle patterns on a chart without candles");
    if (this.\u0023\u003DzA4B\u0024PS40sQTxmCO6Ew\u003D\u003D == candles)
      return;
    this.\u0023\u003DzA4B\u0024PS40sQTxmCO6Ew\u003D\u003D?.\u0023\u003DziWBXfUI\u003D(this);
    this.\u0023\u003DzA4B\u0024PS40sQTxmCO6Ew\u003D\u003D = candles;
    this.\u0023\u003DzA4B\u0024PS40sQTxmCO6Ew\u003D\u003D?.\u0023\u003DznX2LIYg\u003D(this);
  }

  protected override void Clear()
  {
    this.\u0023\u003DzA4B\u0024PS40sQTxmCO6Ew\u003D\u003D?.\u0023\u003DziWBXfUI\u003D(this);
    this.ScichartSurfaceMVVM.\u0023\u003DzBCuJKIIaVAUt();
  }

  protected override void UpdateUi()
  {
    lock (this.\u0023\u003DzcU\u0024Li0iEvziI)
      this.\u0023\u003DzcU\u0024Li0iEvziI.Clear();
    this.ScichartSurfaceMVVM.\u0023\u003DzBCuJKIIaVAUt();
  }

  protected override void RootElementPropertyChanged(
    IChartComponent _param1,
    string _param2)
  {
    base.RootElementPropertyChanged(_param1, _param2);
    if (!(_param2 == "UpColor") && !(_param2 == "DownColor") && !(_param2 == "IsVisible"))
      return;
    this.ScichartSurfaceMVVM.\u0023\u003DzBCuJKIIaVAUt();
  }

  public override bool Draw(IEnumerableEx<ChartDrawData.IDrawValue> _param1)
  {
    \u0023\u003DzGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg\u003D\u003D.\u0023\u003Dzea\u0024y3Uu1m4CYxerxRw\u003D\u003D zeaY3Uu1m4CyxerxRw = new \u0023\u003DzGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg\u003D\u003D.\u0023\u003Dzea\u0024y3Uu1m4CYxerxRw\u003D\u003D();
    zeaY3Uu1m4CyxerxRw._variableSome3535 = this;
    if (_param1 == null || CollectionHelper.IsEmpty<ChartDrawData.IDrawValue>((IEnumerable<ChartDrawData.IDrawValue>) _param1))
      return false;
    this.\u0023\u003Dzsinxw0lApF067xCcMw\u003D\u003D(true);
    zeaY3Uu1m4CyxerxRw.\u0023\u003Dz1ZKXBqs\u003D = false;
    lock (this.\u0023\u003DzcU\u0024Li0iEvziI)
    {
      foreach (ChartDrawData.IndicatorData indicatorData in (IEnumerable<ChartDrawData.IDrawValue>) _param1)
      {
        CandlePatternIndicatorValue patternIndicatorValue = (CandlePatternIndicatorValue) indicatorData.Value;
        if (patternIndicatorValue.Value)
        {
          if (patternIndicatorValue.IsFinal)
          {
            CollectionHelper.ForEach<DateTimeOffset>((IEnumerable<DateTimeOffset>) patternIndicatorValue.CandleOpenTimes, zeaY3Uu1m4CyxerxRw.\u0023\u003Dzon\u0024_RZacJIPJ ?? (zeaY3Uu1m4CyxerxRw.\u0023\u003Dzon\u0024_RZacJIPJ = new Action<DateTimeOffset>(zeaY3Uu1m4CyxerxRw.\u0023\u003DzgXmWJEIKFwdJ3WKiTA\u003D\u003D)));
          }
          else
          {
            List<DateTime> dateTimeList = new List<DateTime>();
            dateTimeList.AddRange(((IEnumerable<DateTimeOffset>) patternIndicatorValue.CandleOpenTimes).Select<DateTimeOffset, DateTime>(\u0023\u003DzGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg\u003D\u003D.SomeClass34343383.\u0023\u003DzDGaDWsF\u00243rnprEPEXA\u003D\u003D ?? (\u0023\u003DzGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg\u003D\u003D.SomeClass34343383.\u0023\u003DzDGaDWsF\u00243rnprEPEXA\u003D\u003D = new Func<DateTimeOffset, DateTime>(\u0023\u003DzGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg\u003D\u003D.SomeClass34343383.SomeMethond0343.\u0023\u003Dzp5FLMdUoOF2aJ6NLRw\u003D\u003D))));
            this.\u0023\u003Dzn6iWmQ_uwD0z = dateTimeList.ToArray();
            zeaY3Uu1m4CyxerxRw.\u0023\u003Dz1ZKXBqs\u003D = true;
          }
        }
        else if (this.\u0023\u003Dzn6iWmQ_uwD0z.Length != 0)
        {
          zeaY3Uu1m4CyxerxRw.\u0023\u003Dz1ZKXBqs\u003D = true;
          this.\u0023\u003Dzn6iWmQ_uwD0z = Array.Empty<DateTime>();
        }
      }
    }
    if (zeaY3Uu1m4CyxerxRw.\u0023\u003Dz1ZKXBqs\u003D)
      this.ScichartSurfaceMVVM.\u0023\u003DzBCuJKIIaVAUt();
    return zeaY3Uu1m4CyxerxRw.\u0023\u003Dz1ZKXBqs\u003D;
  }

  public Color? \u0023\u003Dzj4w_lAs\u003D(DateTime _param1, bool _param2)
  {
    if (!this.RootElem.IsVisible || !this.ChartComponentView.IsVisible)
      return new Color?();
    lock (this.\u0023\u003DzcU\u0024Li0iEvziI)
    {
      if (!this.\u0023\u003DzcU\u0024Li0iEvziI.Contains(_param1))
      {
        if (!((IEnumerable<DateTime>) this.\u0023\u003Dzn6iWmQ_uwD0z).Contains<DateTime>(_param1))
          return new Color?();
      }
    }
    return new Color?(_param2 ? this.ChartComponentView.UpColor : this.ChartComponentView.DownColor);
  }

  [Serializable]
  private new sealed class SomeClass34343383
  {
    public static readonly \u0023\u003DzGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg\u003D\u003D.SomeClass34343383 SomeMethond0343 = new \u0023\u003DzGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg\u003D\u003D.SomeClass34343383();
    public static Func<DateTimeOffset, DateTime> \u0023\u003DzDGaDWsF\u00243rnprEPEXA\u003D\u003D;

    public DateTime \u0023\u003Dzp5FLMdUoOF2aJ6NLRw\u003D\u003D(DateTimeOffset _param1)
    {
      return _param1.UtcDateTime;
    }
  }

  private sealed class \u0023\u003Dzea\u0024y3Uu1m4CYxerxRw\u003D\u003D
  {
    public bool \u0023\u003Dz1ZKXBqs\u003D;
    public \u0023\u003DzGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg\u003D\u003D _variableSome3535;
    public Action<DateTimeOffset> \u0023\u003Dzon\u0024_RZacJIPJ;

    public void \u0023\u003DzgXmWJEIKFwdJ3WKiTA\u003D\u003D(DateTimeOffset _param1)
    {
      this.\u0023\u003Dz1ZKXBqs\u003D |= this._variableSome3535.\u0023\u003DzcU\u0024Li0iEvziI.Add(_param1.UtcDateTime);
    }
  }
}
