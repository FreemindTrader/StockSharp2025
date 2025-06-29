// Decompiled with JetBrains decompiler
// Type: #=zlV2bk__vwLQKcXFMwtbJEU2xBpcNhs6SzKroD6K$yiEmA1ED1g==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Collections;

#nullable disable
internal sealed class \u0023\u003DzlV2bk__vwLQKcXFMwtbJEU2xBpcNhs6SzKroD6K\u0024yiEmA1ED1g\u003D\u003D
{
  private readonly double \u0023\u003Dz9eBGgoElCf5Wp_pFvA\u003D\u003D;
  private readonly double \u0023\u003DztoR9ymdsO7mZWEiavA\u003D\u003D;

  public \u0023\u003DzlV2bk__vwLQKcXFMwtbJEU2xBpcNhs6SzKroD6K\u0024yiEmA1ED1g\u003D\u003D(
    IList _param1)
  {
    double num1 = 0.0;
    double num2 = 0.0;
    int count = _param1.Count;
    for (int index = 0; index < count; ++index)
    {
      num1 += (double) index;
      num2 += ((IComparable) _param1[index]).\u0023\u003Dzb9UCYbo\u003D();
    }
    double num3 = num1 / (double) _param1.Count;
    double num4 = num2 / (double) _param1.Count;
    double num5 = 0.0;
    double num6 = 0.0;
    for (int index = 0; index < count; ++index)
    {
      num5 += ((double) index - num3) * (((IComparable) _param1[index]).\u0023\u003Dzb9UCYbo\u003D() - num4);
      num6 += Math.Pow((double) index - num3, 2.0);
    }
    this.\u0023\u003Dz9eBGgoElCf5Wp_pFvA\u003D\u003D = num5 / num6;
    this.\u0023\u003DztoR9ymdsO7mZWEiavA\u003D\u003D = num4 - this.\u0023\u003DzE42AUhfk7ZpN() * num3;
  }

  public double \u0023\u003DzCTMLEi0\u003D(double _param1)
  {
    return this.\u0023\u003Dz9eBGgoElCf5Wp_pFvA\u003D\u003D * _param1 + this.\u0023\u003DztoR9ymdsO7mZWEiavA\u003D\u003D;
  }

  public double \u0023\u003Dzepw2dxx7l1yc() => this.\u0023\u003DztoR9ymdsO7mZWEiavA\u003D\u003D;

  public double \u0023\u003DzE42AUhfk7ZpN() => this.\u0023\u003Dz9eBGgoElCf5Wp_pFvA\u003D\u003D;
}
