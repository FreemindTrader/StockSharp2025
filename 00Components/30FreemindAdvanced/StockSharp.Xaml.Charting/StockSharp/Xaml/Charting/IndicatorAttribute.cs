// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorAttribute
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using Ecng.Common;
using StockSharp.Algo.Indicators;
using System;
using System.Diagnostics;

#nullable disable
namespace StockSharp.Xaml.Charting;

/// <summary>
/// Attribute, applied to indicator's painter, to provide information about type of <see cref="T:StockSharp.Algo.Indicators.IIndicator" />.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class IndicatorAttribute : Attribute
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly Type \u0023\u003DzjIbS_29QSa7sxFGAlg\u003D\u003D;

  /// <summary>
  /// Initializes a new instance of the <see cref="T:StockSharp.Xaml.Charting.IndicatorAttribute" />.
  /// </summary>
  /// <param name="type">Indicator type.</param>
  public IndicatorAttribute(Type type)
  {
    if (type == (Type) null)
      throw new ArgumentNullException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539442707));
    this.\u0023\u003DzjIbS_29QSa7sxFGAlg\u003D\u003D = TypeHelper.Is<IIndicator>(type, true) ? type : throw new ArgumentException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539428523));
  }

  /// <summary>Indicator type.</summary>
  public Type Type => this.\u0023\u003DzjIbS_29QSa7sxFGAlg\u003D\u003D;
}
