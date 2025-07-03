// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorPainters.ParabolicSarPainter
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Drawing;
using StockSharp.Algo.Indicators;

#nullable disable
namespace StockSharp.Xaml.Charting.IndicatorPainters;

[Indicator(typeof (ParabolicSar))]
public class ParabolicSarPainter : DefaultPainter
{
  public ParabolicSarPainter()
  {
    this.Line.Style = DrawStyles.Dot;
    this.Line.StrokeThickness = 4;
  }
}
