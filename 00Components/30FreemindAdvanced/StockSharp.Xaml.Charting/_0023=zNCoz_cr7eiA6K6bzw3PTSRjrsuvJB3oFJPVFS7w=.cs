// Decompiled with JetBrains decompiler
// Type: #=zNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using Ecng.Common;
using System;

#nullable disable
internal static class \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D
{
  private static readonly Decimal \u0023\u003Dz8Rqpq2AmP\u0024GfIHgARw\u003D\u003D = 0.0000000000000000000000000001M;

  public static int \u0023\u003DzfAOpzpzamEKk(this Decimal _param0)
  {
    MathHelper.DecimalInfo decimalInfo = MathHelper.GetDecimalInfo(_param0);
    int effectiveScale = ((MathHelper.DecimalInfo) ref decimalInfo).EffectiveScale;
    if (effectiveScale > 0)
      ++effectiveScale;
    return MathHelper.GetDigitCount((long) _param0) + effectiveScale;
  }

  public static bool \u0023\u003Dz0AeaaBjWIeEn(this double _param0, double _param1)
  {
    return Math.Abs(_param0 - _param1) < 1E-15;
  }

  public static double \u0023\u003DzhjMeblo\u003D(this double _param0, double _param1)
  {
    return Math.Round(_param0 / _param1) * _param1;
  }

  public static double \u0023\u003Dzezm_VedE86O1(this double _param0, double _param1)
  {
    return Math.Round(_param0 / _param1) * _param1;
  }

  public static float \u0023\u003DzhjMeblo\u003D(this float _param0, float _param1)
  {
    return (float) Math.Round((double) _param0 / (double) _param1) * _param1;
  }

  public static double \u0023\u003Dz1bwNIVeIfnJ7(double _param0, double _param1)
  {
    return Math.Ceiling(_param0 / _param1) * _param1;
  }

  public static double \u0023\u003DzhMBCN0mwgFw7(double _param0, double _param1)
  {
    return Math.Floor(_param0 / _param1) * _param1;
  }

  internal static bool \u0023\u003Dzpz5fd0zg24Kt(double _param0, double _param1)
  {
    _param0 = Math.Round(_param0, 15);
    if (Math.Abs(_param1) < 1E-15)
      return false;
    double a = Math.Abs(_param0 / _param1);
    double num = 1E-15 * a;
    return Math.Abs(a - Math.Round(a)) <= num;
  }

  internal static bool \u0023\u003Dzpz5fd0zg24Kt(Decimal _param0, Decimal _param1)
  {
    if (Math.Abs(_param1 - 0M) < \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003Dz8Rqpq2AmP\u0024GfIHgARw\u003D\u003D)
      return false;
    Decimal d = Math.Abs(_param0 / _param1);
    Decimal num = \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003Dz8Rqpq2AmP\u0024GfIHgARw\u003D\u003D * d;
    return Math.Abs(d - Math.Round(d)) <= num;
  }

  internal static Decimal \u0023\u003Dz1bwNIVeIfnJ7(Decimal _param0, Decimal _param1)
  {
    return Decimal.Ceiling(_param0 / _param1) * _param1;
  }

  public static void \u0023\u003DzMv8ALVs\u003D(ref int _param0, ref int _param1)
  {
    int num = _param1;
    _param1 = _param0;
    _param0 = num;
  }

  public static void \u0023\u003DzMv8ALVs\u003D(ref long _param0, ref long _param1)
  {
    long num = _param1;
    _param1 = _param0;
    _param0 = num;
  }

  public static void \u0023\u003DzMv8ALVs\u003D(ref double _param0, ref double _param1)
  {
    double num = _param1;
    _param1 = _param0;
    _param0 = num;
  }

  public static void \u0023\u003DzMv8ALVs\u003D(ref float _param0, ref float _param1)
  {
    float num = _param1;
    _param1 = _param0;
    _param0 = num;
  }

  internal static void \u0023\u003Dz9tfGzYXjAfsx(
    ref double _param0,
    ref double _param1,
    ref double _param2,
    ref double _param3)
  {
    if (_param0 <= _param1)
      return;
    double num1 = _param0;
    _param0 = _param1;
    _param1 = num1;
    double num2 = _param2;
    _param2 = _param3;
    _param3 = num2;
  }

  public static int \u0023\u003DzOS5Il8E\u003D(int _param0, int _param1, int _param2)
  {
    if (_param0 < _param1)
      return _param1;
    return _param0 <= _param2 ? _param0 : _param2;
  }

  public static long \u0023\u003DzOS5Il8E\u003D(long _param0, long _param1, long _param2)
  {
    if (_param0 < _param1)
      return _param1;
    return _param0 <= _param2 ? _param0 : _param2;
  }

  public static double \u0023\u003DzOS5Il8E\u003D(double _param0, double _param1, double _param2)
  {
    if (_param0 < _param1)
      return _param1;
    return _param0 <= _param2 ? _param0 : _param2;
  }

  public static bool \u0023\u003Dzsywd1DCE\u0024jHB(double _param0, double _param1, double _param2)
  {
    return Math.Abs(\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003DzIj_bM_9LFqLd(_param0, _param1, _param2) - _param0) <= double.Epsilon;
  }

  internal static double \u0023\u003DzIj_bM_9LFqLd(double _param0, double _param1, double _param2)
  {
    bool flag = Math.Sign(_param0) == -1;
    double a = Math.Round(Math.Log(Math.Abs(_param0), _param2) / Math.Log(Math.Abs(_param1), _param2), 5);
    double num1 = Math.Ceiling(a);
    if (Math.Abs(num1 - a) < double.Epsilon)
      return _param0;
    double y = flag ? num1 - 1.0 : num1;
    double num2 = Math.Pow(_param1, y);
    return !flag ? num2 : -num2;
  }

  internal static double \u0023\u003Dzcszu4jA0eOO6(double _param0, double _param1, double _param2)
  {
    bool flag = Math.Sign(_param0) == -1;
    double d = Math.Round(Math.Log(Math.Abs(_param0), _param2) / Math.Log(Math.Abs(_param1), _param2), 5);
    double num1 = Math.Floor(d);
    if (Math.Abs(num1 - d) < double.Epsilon)
      return _param0;
    double y = flag ? num1 - 1.0 : num1;
    double num2 = Math.Pow(_param1, y);
    return !flag ? num2 : -num2;
  }

  internal static bool \u0023\u003Dzl7l7j94ADTiB(Type _param0)
  {
    bool flag = false;
    switch (Type.GetTypeCode(_param0))
    {
      case TypeCode.SByte:
      case TypeCode.Byte:
      case TypeCode.Int16:
      case TypeCode.UInt16:
      case TypeCode.Int32:
      case TypeCode.UInt32:
      case TypeCode.Int64:
      case TypeCode.UInt64:
        flag = true;
        break;
    }
    return flag;
  }

  public static bool \u0023\u003DzeNpB9guo_tur(double _param0) => _param0 != _param0;
}
