// Decompiled with JetBrains decompiler
// Type: #=zFXfXgyJ9DFiOo1IYbwdMA$cZLs_od3qVmWwgIlKdnZ_XFod55ir8$xo=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
public static class \u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA\u0024cZLs_od3qVmWwgIlKdnZ_XFod55ir8\u0024xo\u003D
{
  public static void \u0023\u003DzjBmQkSQ797ct(
    \u0023\u003DzpKvy0OA0_My0Sg27HiUJaX\u0024AyxSGkqEcPv0Ah3hMaVEX _param0,
    Func<double, double, IPathColor> _param1,
    IPointSeries _param2,
    ICoordinateCalculator<<double> _param3,
    ICoordinateCalculator<<double> _param4)
  {
    \u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA\u0024cZLs_od3qVmWwgIlKdnZ_XFod55ir8\u0024xo\u003D.\u0023\u003DzjBmQkSQ797ct(_param0, _param1, _param2.\u0023\u003DzwQnyySN6xaVC().\u0023\u003DzRr4AYdnHaTxa(), _param2.\u0023\u003DzPqsSI6C5MOOb().\u0023\u003DzRr4AYdnHaTxa(), _param2.\u0023\u003DzlpVGw6E\u003D(), _param3, _param4);
  }

  public static void \u0023\u003DzjBmQkSQ797ct(
    \u0023\u003DzpKvy0OA0_My0Sg27HiUJaX\u0024AyxSGkqEcPv0Ah3hMaVEX _param0,
    IPointSeries _param1,
    ICoordinateCalculator<<double> _param2,
    ICoordinateCalculator<<double> _param3)
  {
    \u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA\u0024cZLs_od3qVmWwgIlKdnZ_XFod55ir8\u0024xo\u003D.\u0023\u003DzjBmQkSQ797ct(_param0, \u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA\u0024cZLs_od3qVmWwgIlKdnZ_XFod55ir8\u0024xo\u003D.SomeClass34343383.\u0023\u003DzMJsSoL5rX24oRR6rOw\u003D\u003D ?? (\u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA\u0024cZLs_od3qVmWwgIlKdnZ_XFod55ir8\u0024xo\u003D.SomeClass34343383.\u0023\u003DzMJsSoL5rX24oRR6rOw\u003D\u003D = new Func<double, double, IPathColor>(\u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA\u0024cZLs_od3qVmWwgIlKdnZ_XFod55ir8\u0024xo\u003D.SomeClass34343383.SomeMethond0343.\u0023\u003DzJu67dK0kkhLjQsBBw\u00245SnFg\u003D)), _param1.\u0023\u003DzwQnyySN6xaVC().\u0023\u003DzRr4AYdnHaTxa(), _param1.\u0023\u003DzPqsSI6C5MOOb().\u0023\u003DzRr4AYdnHaTxa(), _param1.\u0023\u003DzlpVGw6E\u003D(), _param2, _param3);
  }

  private static void \u0023\u003DzjBmQkSQ797ct(
    \u0023\u003DzpKvy0OA0_My0Sg27HiUJaX\u0024AyxSGkqEcPv0Ah3hMaVEX _param0,
    Func<double, double, IPathColor> _param1,
    double[] _param2,
    double[] _param3,
    int _param4,
    ICoordinateCalculator<<double> _param5,
    ICoordinateCalculator<<double> _param6)
  {
    bool flag = !_param5.\u0023\u003Dz23Oi_5A6gjXaau8ZzBLLsFfzG2_K();
    \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 v1qkdyQymVhxLr4oDq = (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024) null;
    double num1 = _param2[0];
    double d1 = _param3[0];
    IPathColor bDPyrbD0P2noEi5c1 = _param1(num1, d1);
    double num2 = _param5.\u0023\u003DzhL6gsJw\u003D(num1);
    double num3 = _param6.\u0023\u003DzhL6gsJw\u003D(d1);
    double num4 = flag ? num3 : num2;
    double num5 = flag ? num2 : num3;
    double num6 = num4;
    if (!double.IsNaN(d1))
      v1qkdyQymVhxLr4oDq = _param0.\u0023\u003Dz7ZSU06M\u003D(bDPyrbD0P2noEi5c1, num6, num5);
    for (int index = 1; index < _param4; ++index)
    {
      double num7 = _param2[index];
      double d2 = _param3[index];
      if (!double.IsNaN(d2))
      {
        IPathColor bDPyrbD0P2noEi5c2 = _param1(num7, d2);
        if (bDPyrbD0P2noEi5c2 != bDPyrbD0P2noEi5c1)
        {
          bDPyrbD0P2noEi5c1.Dispose();
          v1qkdyQymVhxLr4oDq?.\u0023\u003DzBNsE20w\u003D();
          v1qkdyQymVhxLr4oDq = (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024) null;
          bDPyrbD0P2noEi5c1 = bDPyrbD0P2noEi5c2;
        }
        double num8 = _param5.\u0023\u003DzhL6gsJw\u003D(num7);
        double num9 = _param6.\u0023\u003DzhL6gsJw\u003D(d2);
        double num10 = flag ? num9 : num8;
        double num11 = flag ? num8 : num9;
        double num12 = num10;
        if (v1qkdyQymVhxLr4oDq == null)
          v1qkdyQymVhxLr4oDq = _param0.\u0023\u003Dz7ZSU06M\u003D(bDPyrbD0P2noEi5c1, num12, num11);
        else
          v1qkdyQymVhxLr4oDq.\u0023\u003DzfRDRUq8\u003D(num12, num11);
      }
    }
    v1qkdyQymVhxLr4oDq?.\u0023\u003DzBNsE20w\u003D();
  }

  [Serializable]
  private sealed class SomeClass34343383
  {
    public static readonly \u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA\u0024cZLs_od3qVmWwgIlKdnZ_XFod55ir8\u0024xo\u003D.SomeClass34343383 SomeMethond0343 = new \u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA\u0024cZLs_od3qVmWwgIlKdnZ_XFod55ir8\u0024xo\u003D.SomeClass34343383();
    public static Func<double, double, IPathColor> \u0023\u003DzMJsSoL5rX24oRR6rOw\u003D\u003D;

    public IPathColor \u0023\u003DzJu67dK0kkhLjQsBBw\u00245SnFg\u003D(
      double _param1,
      double _param2)
    {
      return (IPathColor) null;
    }
  }
}
