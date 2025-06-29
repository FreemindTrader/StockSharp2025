// Decompiled with JetBrains decompiler
// Type: #=z1EupPkIlS$DjzDzGIXoOwvqJYhGeF6ry$1tKNzE=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using \u002D;
using System;
using System.Collections.Generic;

#nullable disable
internal static class \u0023\u003Dz1EupPkIlS\u0024DjzDzGIXoOwvqJYhGeF6ry\u00241tKNzE\u003D
{
  public static \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D \u0023\u003DzzXTqVFg\u003D(
    this \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D _param0,
    double _param1,
    double _param2,
    bool _param3,
    double _param4)
  {
    if (!_param3)
      return _param0.\u0023\u003DzzXTqVFg\u003D(_param1, _param2);
    dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd klqcJ87Zm8UwE3WEjd1 = _param0.\u0023\u003DzfODy_Nxn8OGy();
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
      \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003DzMv8ALVs\u003D(ref num6, ref num7);
    dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd klqcJ87Zm8UwE3WEjd2 = new dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd(num6, num7);
    return \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003Dzo7udA0u6sNJJ(_param0, (IComparable) klqcJ87Zm8UwE3WEjd2.Min, (IComparable) klqcJ87Zm8UwE3WEjd2.Max);
  }

  public static \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D \u0023\u003DzeiifnZI\u003D(
    this IEnumerable<\u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D> _param0)
  {
    \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D abyLt9clZggmJsWhw1 = (\u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D) null;
    foreach (\u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D abyLt9clZggmJsWhw2 in _param0)
      abyLt9clZggmJsWhw1 = abyLt9clZggmJsWhw1 != null ? abyLt9clZggmJsWhw1.\u0023\u003DzeiifnZI\u003D(abyLt9clZggmJsWhw2) : abyLt9clZggmJsWhw2;
    return abyLt9clZggmJsWhw1;
  }
}
