// Decompiled with JetBrains decompiler
// Type: #=zGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using Ecng.Collections;
using StockSharp.Algo.Indicators;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Media;

#nullable disable
internal sealed class \u0023\u003DzGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg\u003D\u003D(
  CandlePatternElement _param1) : 
  \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xAm_Ca5jQWV7qTLlcbbCsg9V98qcX3BV8d1LwxB\u0024<CandlePatternElement>(_param1)
{
  
  private readonly HashSet<DateTime> \u0023\u003DzcU\u0024Li0iEvziI = new HashSet<DateTime>();
  
  private DateTime[] \u0023\u003Dzn6iWmQ_uwD0z = Array.Empty<DateTime>();
  
  private \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ\u003D\u003D _candleMap;

  protected override void \u0023\u003DzY0x9JtY\u003D()
  {
    base.\u0023\u003DzY0x9JtY\u003D();
    this.\u0023\u003Dzsinxw0lApF067xCcMw\u003D\u003D(false);
  }

  private void \u0023\u003Dzsinxw0lApF067xCcMw\u003D\u003D(bool _param1)
  {
    \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ\u003D\u003D candles = this.\u0023\u003Dz\u00246aIVrHDxlRJ().CandlesCompositeElement?.Candles;
    if (candles == null & _param1)
      throw new InvalidOperationException("");
    if (this._candleMap == candles)
      return;
    this._candleMap?.\u0023\u003DziWBXfUI\u003D(this);
    this._candleMap = candles;
    this._candleMap?.\u0023\u003DznX2LIYg\u003D(this);
  }

  protected override void \u0023\u003DzXfak0jM\u003D()
  {
    this._candleMap?.\u0023\u003DziWBXfUI\u003D(this);
    this.\u0023\u003Dz\u00246aIVrHDxlRJ().\u0023\u003DzBCuJKIIaVAUt();
  }

  protected override void \u0023\u003DzowR7R4A\u003D()
  {
    lock (this.\u0023\u003DzcU\u0024Li0iEvziI)
      this.\u0023\u003DzcU\u0024Li0iEvziI.Clear();
    this.\u0023\u003Dz\u00246aIVrHDxlRJ().\u0023\u003DzBCuJKIIaVAUt();
  }

  protected override void \u0023\u003Dz3u1qwgvgJlZC(
    IfxChartElement _param1,
    string _param2)
  {
    base.\u0023\u003Dz3u1qwgvgJlZC(_param1, _param2);
    if (!(_param2 == "") && !(_param2 == "") && !(_param2 == ""))
      return;
    this.\u0023\u003Dz\u00246aIVrHDxlRJ().\u0023\u003DzBCuJKIIaVAUt();
  }

  public override bool Draw(IEnumerableEx<ChartDrawData.IDrawValue> _param1)
  {
    \u0023\u003DzGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg\u003D\u003D.\u0023\u003Dzea\u0024y3Uu1m4CYxerxRw\u003D\u003D zeaY3Uu1m4CyxerxRw = new \u0023\u003DzGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg\u003D\u003D.\u0023\u003Dzea\u0024y3Uu1m4CYxerxRw\u003D\u003D();
    zeaY3Uu1m4CyxerxRw.\u0023\u003DzRRvwDu67s9Rm = this;
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
            dateTimeList.AddRange(((IEnumerable<DateTimeOffset>) patternIndicatorValue.CandleOpenTimes).Select<DateTimeOffset, DateTime>(\u0023\u003DzGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzDGaDWsF\u00243rnprEPEXA\u003D\u003D ?? (\u0023\u003DzGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzDGaDWsF\u00243rnprEPEXA\u003D\u003D = new Func<DateTimeOffset, DateTime>(\u0023\u003DzGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dzp5FLMdUoOF2aJ6NLRw\u003D\u003D))));
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
      this.\u0023\u003Dz\u00246aIVrHDxlRJ().\u0023\u003DzBCuJKIIaVAUt();
    return zeaY3Uu1m4CyxerxRw.\u0023\u003Dz1ZKXBqs\u003D;
  }

  public Color? \u0023\u003Dzj4w_lAs\u003D(DateTime _param1, bool _param2)
  {
    if (!this.RootElem.IsVisible || !this.\u0023\u003DzeaszzAAoBOY9().IsVisible)
      return new Color?();
    lock (this.\u0023\u003DzcU\u0024Li0iEvziI)
    {
      if (!this.\u0023\u003DzcU\u0024Li0iEvziI.Contains(_param1))
      {
        if (!((IEnumerable<DateTime>) this.\u0023\u003Dzn6iWmQ_uwD0z).Contains<DateTime>(_param1))
          return new Color?();
      }
    }
    return new Color?(_param2 ? this.\u0023\u003DzeaszzAAoBOY9().UpColor : this.\u0023\u003DzeaszzAAoBOY9().DownColor);
  }

  [Serializable]
  private new sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly \u0023\u003DzGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new \u0023\u003DzGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<DateTimeOffset, DateTime> \u0023\u003DzDGaDWsF\u00243rnprEPEXA\u003D\u003D;

    internal DateTime \u0023\u003Dzp5FLMdUoOF2aJ6NLRw\u003D\u003D(DateTimeOffset _param1)
    {
      return _param1.UtcDateTime;
    }
  }

  private sealed class \u0023\u003Dzea\u0024y3Uu1m4CYxerxRw\u003D\u003D
  {
    public bool \u0023\u003Dz1ZKXBqs\u003D;
    public \u0023\u003DzGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg\u003D\u003D \u0023\u003DzRRvwDu67s9Rm;
    public Action<DateTimeOffset> \u0023\u003Dzon\u0024_RZacJIPJ;

    internal void \u0023\u003DzgXmWJEIKFwdJ3WKiTA\u003D\u003D(DateTimeOffset _param1)
    {
      this.\u0023\u003Dz1ZKXBqs\u003D |= this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzcU\u0024Li0iEvziI.Add(_param1.UtcDateTime);
    }
  }
}
