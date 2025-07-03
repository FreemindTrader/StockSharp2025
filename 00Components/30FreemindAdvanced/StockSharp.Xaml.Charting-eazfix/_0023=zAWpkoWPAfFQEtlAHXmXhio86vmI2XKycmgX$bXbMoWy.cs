// Decompiled with JetBrains decompiler
// Type: #=zAWpkoWPAfFQEtlAHXmXhio86vmI2XKycmgX$bXbMoWy0WSCN5qTb7KX3DSGIbEK1Aw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
internal static class \u0023\u003DzAWpkoWPAfFQEtlAHXmXhio86vmI2XKycmgX\u0024bXbMoWy0WSCN5qTb7KX3DSGIbEK1Aw\u003D\u003D
{
  public static long \u0023\u003DzYDSd8XwnDBcn(long _param0)
  {
    return _param0 >= 0L ? (long) Math.Pow(2.0, Math.Ceiling(Math.Log((double) _param0, 2.0))) : throw new ArgumentOutOfRangeException("n", "Must be positive.");
  }

  public static int \u0023\u003DzYDSd8XwnDBcn(int _param0)
  {
    return _param0 >= 0 ? (int) Math.Pow(2.0, Math.Ceiling(Math.Log((double) _param0, 2.0))) : throw new ArgumentOutOfRangeException("n", "Must be positive.");
  }

  public static float \u0023\u003DzYDSd8XwnDBcn(float _param0)
  {
    return (double) _param0 >= 0.0 ? (float) Math.Pow(2.0, Math.Ceiling(Math.Log((double) _param0, 2.0))) : throw new ArgumentOutOfRangeException("n", "Must be positive.");
  }

  public static double \u0023\u003DzYDSd8XwnDBcn(double _param0)
  {
    return _param0 >= 0.0 ? Math.Pow(2.0, Math.Ceiling(Math.Log(_param0, 2.0))) : throw new ArgumentOutOfRangeException("n", "Must be positive.");
  }

  public static long \u0023\u003Dzp\u0024z16uPz13h6luApgA\u003D\u003D(int _param0)
  {
    long num = 1;
    for (; _param0 > 1; --_param0)
      num *= (long) _param0;
    return num;
  }

  public static long \u0023\u003DzMg_Onud6d9iicJ4s5xdoftCggO1e(int _param0, int _param1)
  {
    return \u0023\u003DzAWpkoWPAfFQEtlAHXmXhio86vmI2XKycmgX\u0024bXbMoWy0WSCN5qTb7KX3DSGIbEK1Aw\u003D\u003D.\u0023\u003Dzp\u0024z16uPz13h6luApgA\u003D\u003D(_param0) / (\u0023\u003DzAWpkoWPAfFQEtlAHXmXhio86vmI2XKycmgX\u0024bXbMoWy0WSCN5qTb7KX3DSGIbEK1Aw\u003D\u003D.\u0023\u003Dzp\u0024z16uPz13h6luApgA\u003D\u003D(_param1) * \u0023\u003DzAWpkoWPAfFQEtlAHXmXhio86vmI2XKycmgX\u0024bXbMoWy0WSCN5qTb7KX3DSGIbEK1Aw\u003D\u003D.\u0023\u003Dzp\u0024z16uPz13h6luApgA\u003D\u003D(_param0 - _param1));
  }

  public static unsafe float \u0023\u003DzBFOpBnXKp_m76HjXMQ\u003D\u003D(float _param0)
  {
    float num = 0.5f * _param0;
    _param0 = *(float*) &(1597463174 - (*(int*) &_param0 >> 1));
    _param0 *= (float) (1.5 - (double) num * (double) _param0 * (double) _param0);
    return _param0;
  }

  public static double \u0023\u003DzBFOpBnXKp_m76HjXMQ\u003D\u003D(double _param0)
  {
    return (double) \u0023\u003DzAWpkoWPAfFQEtlAHXmXhio86vmI2XKycmgX\u0024bXbMoWy0WSCN5qTb7KX3DSGIbEK1Aw\u003D\u003D.\u0023\u003DzBFOpBnXKp_m76HjXMQ\u003D\u003D((float) _param0);
  }

  public static double \u0023\u003DzE1pg1ELlS2z_(double _param0)
  {
    if (_param0 < 0.0)
      _param0 += 6.2831854820251465;
    if (_param0 >= 6.2831854820251465)
      _param0 -= 6.2831854820251465;
    return _param0 >= 0.0 && _param0 <= 6.2831854820251465 ? _param0 : throw new Exception("Value >= 0 && Value <= Tau");
  }

  public static double \u0023\u003DzAWhsryhXz5Xs(double _param0, double _param1)
  {
    if (_param0 != \u0023\u003DzAWpkoWPAfFQEtlAHXmXhio86vmI2XKycmgX\u0024bXbMoWy0WSCN5qTb7KX3DSGIbEK1Aw\u003D\u003D.\u0023\u003DzE1pg1ELlS2z_(_param0))
      throw new Exception("StartAngle != Range0ToTau(StartAngle)");
    if (_param1 != \u0023\u003DzAWpkoWPAfFQEtlAHXmXhio86vmI2XKycmgX\u0024bXbMoWy0WSCN5qTb7KX3DSGIbEK1Aw\u003D\u003D.\u0023\u003DzE1pg1ELlS2z_(_param1))
      throw new Exception("EndAngle != Range0ToTau(EndAngle)");
    double num = _param1 - _param0;
    if (num > 3.1415927410125732)
      num -= 6.2831854820251465;
    if (num < -3.1415927410125732)
      num += 6.2831854820251465;
    return num;
  }

  public static double \u0023\u003Dzr5Wc65_G0uGy4mMO_\u0024kM2TE\u003D(double _param0)
  {
    return _param0 * (Math.PI / 180.0);
  }

  public static double \u0023\u003Dzg7YMXd0vNonwpIK4T3ZnQos\u003D(double _param0)
  {
    return _param0 * (180.0 / Math.PI);
  }

  public static void \u0023\u003DzMv8ALVs\u003D(ref double _param0, ref double _param1)
  {
    double num = _param0;
    _param0 = _param1;
    _param1 = num;
  }

  public static void \u0023\u003DzMv8ALVs\u003D(ref float _param0, ref float _param1)
  {
    float num = _param0;
    _param0 = _param1;
    _param1 = num;
  }

  public static bool \u0023\u003DzQ5TIsvyIx_oUWlkBHw\u003D\u003D(
    double _param0,
    double _param1,
    double _param2)
  {
    return _param0 > _param1 - _param2 && _param0 < _param1 + _param2;
  }
}
