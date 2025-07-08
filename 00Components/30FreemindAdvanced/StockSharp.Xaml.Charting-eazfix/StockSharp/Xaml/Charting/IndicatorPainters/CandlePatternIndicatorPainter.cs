// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorPainters.CandlePatternIndicatorPainter
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Collections;
using Ecng.Serialization;
using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using StockSharp.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting.IndicatorPainters;

[Indicator(typeof (CandlePatternIndicator))]
public class CandlePatternIndicatorPainter : BaseChartIndicatorPainter<CandlePatternIndicator>
{
  
  private readonly CandlePatternElement \u0023\u003DzAiwJX\u0024zIPQsrx2qUGA\u003D\u003D;

  public CandlePatternIndicatorPainter()
  {
    this.\u0023\u003DzAiwJX\u0024zIPQsrx2qUGA\u003D\u003D = new CandlePatternElement()
    {
      DownColor = Colors.Black,
      UpColor = Colors.White
    };
    this.AddChildElement((IChartElement) this.PatternElement);
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "Pattern", Description = "Pattern")]
  public CandlePatternElement PatternElement
  {
    get => this.\u0023\u003DzAiwJX\u0024zIPQsrx2qUGA\u003D\u003D;
  }

  protected override bool OnDraw(
    CandlePatternIndicator indicator,
    IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data)
  {
    return ((IDrawableChartElement) this.PatternElement).StartDrawing(CollectionHelper.ToEx<ChartDrawData.IDrawValue>(data[(IIndicator) indicator].Cast<ChartDrawData.IDrawValue>(), data.Count));
  }

  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    PersistableHelper.Load((IPersistable) this.PatternElement, storage, "PatternElement");
  }

  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<SettingsStorage>("PatternElement", PersistableHelper.Save((IPersistable) this.PatternElement));
  }
}
