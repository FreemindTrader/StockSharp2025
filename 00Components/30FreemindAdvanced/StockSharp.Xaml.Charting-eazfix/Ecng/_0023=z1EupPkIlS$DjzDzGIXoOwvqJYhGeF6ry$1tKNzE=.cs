// Decompiled with JetBrains decompiler
// Type: #=z1EupPkIlS$DjzDzGIXoOwvqJYhGeF6ry$1tKNzE=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;
using System.Collections.Generic;

#nullable disable
public static class \u0023\u003Dz1EupPkIlS\u0024DjzDzGIXoOwvqJYhGeF6ry\u00241tKNzE\u003D
{
  public static IRange GrowBy(
    this IRange _param0,
    double _param1,
    double _param2,
    bool _param3,
    double _param4)
  {
    if (!_param3)
      return _param0.GrowBy(_param1, _param2);
    DoubleRange klqcJ87Zm8UwE3WEjd1 = _param0.AsDoubleRange();
    double a1 = klqcJ87Zm8UwE3WEjd1.Min <= 0.0 ? double.Epsilon : klqcJ87Zm8UwE3WEjd1.Min;
    double a2 = klqcJ87Zm8UwE3WEjd1.Max <= 0.0 ? double.Epsilon : klqcJ87Zm8UwE3WEjd1.Max;
    double num1 = Math.Log(a1, _param4);
    double newBase = _param4;
    double num2 = Math.Log(a2, newBase);
    double num3 = num2 - num1;
    double num4 = num3 * _param1;
    double num5 = num3 * _param2;
    double num6 = Math.Pow(_param4, num1 - num4);
    double num7 = Math.Pow(_param4, num2 + num5);
    if (num6 > num7)
      NumberUtil.Swap(ref num6, ref num7);
    DoubleRange klqcJ87Zm8UwE3WEjd2 = new DoubleRange(num6, num7);
    return \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003Dzo7udA0u6sNJJ(_param0, (IComparable) klqcJ87Zm8UwE3WEjd2.Min, (IComparable) klqcJ87Zm8UwE3WEjd2.Max);
  }

  public static IRange \u0023\u003DzeiifnZI\u003D(
    this IEnumerable<IRange> _param0)
  {
    IRange abyLt9clZggmJsWhw1 = (IRange) null;
    foreach (IRange abyLt9clZggmJsWhw2 in _param0)
      abyLt9clZggmJsWhw1 = abyLt9clZggmJsWhw1 != null ? abyLt9clZggmJsWhw1.\u0023\u003DzeiifnZI\u003D(abyLt9clZggmJsWhw2) : abyLt9clZggmJsWhw2;
    return abyLt9clZggmJsWhw1;
  }
}
