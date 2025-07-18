// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorAttribute
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Common;
using StockSharp.Algo.Indicators;
using System;
using System.Diagnostics;

#nullable disable
namespace StockSharp.Xaml.Charting;

[AttributeUsage(AttributeTargets.Class)]
public class IndicatorAttribute : Attribute
{
  
  private readonly Type _chartAxisType;

  public IndicatorAttribute(Type type)
  {
    if (type == (Type) null)
      throw new ArgumentNullException(nameof (type));
    this._chartAxisType = TypeHelper.Is<IIndicator>(type, true) ? type : throw new ArgumentException(nameof (type));
  }

  public Type Type => this._chartAxisType;
}
