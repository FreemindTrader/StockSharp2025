// Decompiled with JetBrains decompiler
// Type: #=zS5mFHV$eXnkCjzbt0Dx26h0hvlV4LfsI5sS6QMD39it_JzJq2Auui18=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System.Collections.Generic;
using System.Windows;

#nullable disable
internal static class \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26h0hvlV4LfsI5sS6QMD39it_JzJq2Auui18\u003D
{
  public static Point \u0023\u003Dzop6vn0GowyiR(float _param0, float _param1, bool _param2)
  {
    return _param2 ? new Point((double) _param1, (double) _param0) : new Point((double) _param0, (double) _param1);
  }

  public static Point \u0023\u003Dzop6vn0GowyiR(Point _param0, bool _param1)
  {
    if (_param1)
    {
      double x = _param0.X;
      _param0.X = _param0.Y;
      _param0.Y = x;
    }
    return _param0;
  }

  public static Point \u0023\u003Dzop6vn0GowyiR(double _param0, double _param1, bool _param2)
  {
    return _param2 ? new Point(_param1, _param0) : new Point(_param0, _param1);
  }

  public static void \u0023\u003DzzNCP093OQhtA(
    IEnumerable<Point> _param0,
    \u0023\u003DzpKvy0OA0_My0Sg27HiUJaX\u0024AyxSGkqEcPv0Ah3hMaVEX _param1,
    \u0023\u003Dzo13esGCwfQJn\u0024h2kOXY\u0024_bD_Pyrb\u0024d0P2noEI5c\u003D _param2)
  {
    IEnumerator<Point> enumerator = _param0.GetEnumerator();
    if (!enumerator.MoveNext())
      return;
    Point current1 = enumerator.Current;
    using (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 v1qkdyQymVhxLr4oDq1 = _param1.\u0023\u003Dz7ZSU06M\u003D(_param2, current1.X, current1.Y))
    {
      while (enumerator.MoveNext())
      {
        \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 v1qkdyQymVhxLr4oDq2 = v1qkdyQymVhxLr4oDq1;
        Point current2 = enumerator.Current;
        double x = current2.X;
        current2 = enumerator.Current;
        double y = current2.Y;
        v1qkdyQymVhxLr4oDq2.\u0023\u003DzfRDRUq8\u003D(x, y);
      }
    }
  }
}
