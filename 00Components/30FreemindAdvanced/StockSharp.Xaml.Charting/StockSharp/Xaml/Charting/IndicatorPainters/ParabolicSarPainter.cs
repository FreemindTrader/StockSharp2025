// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorPainters.ParabolicSarPainter
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using Ecng.Drawing;
using StockSharp.Algo.Indicators;

#nullable disable
namespace StockSharp.Xaml.Charting.IndicatorPainters;

/// <summary>
/// The chart element for <see cref="T:StockSharp.Algo.Indicators.ParabolicSar" />.
/// </summary>
[Indicator(typeof (ParabolicSar))]
public class ParabolicSarPainter : DefaultPainter
{
  /// <summary>Create instance.</summary>
  public ParabolicSarPainter()
  {
    this.Line.Style = DrawStyles.Dot;
    this.Line.StrokeThickness = 4;
  }
}
