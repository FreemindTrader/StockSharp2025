// Decompiled with JetBrains decompiler
// Type: #=zV9O5tWduWosGLvu_87Zf5OXt7zllMlwUCoVEqrXWXWOo_9I8LKlxnD0wx5l0vOI7XMUaGCc=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
internal static class \u0023\u003DzV9O5tWduWosGLvu_87Zf5OXt7zllMlwUCoVEqrXWXWOo_9I8LKlxnD0wx5l0vOI7XMUaGCc\u003D
{
  public static void \u0023\u003DzQBDTcr7NcY0a(
    byte[] _param0,
    int _param1,
    byte[] _param2,
    int _param3,
    int _param4)
  {
    for (int index = 0; index < _param4; ++index)
      _param0[_param1 + index] = _param2[_param3 + index];
  }

  public static void \u0023\u003DzQBDTcr7NcY0a(
    int[] _param0,
    int _param1,
    int[] _param2,
    int _param3,
    int _param4)
  {
    for (int index = 0; index < _param4; ++index)
      _param0[_param1 + index] = _param2[_param3 + index];
  }

  public static void \u0023\u003DzQBDTcr7NcY0a(
    float[] _param0,
    int _param1,
    float[] _param2,
    int _param3,
    int _param4)
  {
    for (int index = 0; index < _param4; ++index)
      _param0[_param1++] = _param2[_param3++];
  }

  public static void \u0023\u003DzifsHAOd0B2qk(
    byte[] _param0,
    int _param1,
    byte[] _param2,
    int _param3,
    int _param4)
  {
    if (_param2 == _param0 && _param1 >= _param3)
      throw new Exception("this code needs to be tested");
    \u0023\u003DzV9O5tWduWosGLvu_87Zf5OXt7zllMlwUCoVEqrXWXWOo_9I8LKlxnD0wx5l0vOI7XMUaGCc\u003D.\u0023\u003DzQBDTcr7NcY0a(_param0, _param1, _param2, _param3, _param4);
  }

  public static void \u0023\u003DzifsHAOd0B2qk(
    int[] _param0,
    int _param1,
    int[] _param2,
    int _param3,
    int _param4)
  {
    if (_param2 == _param0 && _param1 >= _param3)
      throw new Exception("this code needs to be tested");
    \u0023\u003DzV9O5tWduWosGLvu_87Zf5OXt7zllMlwUCoVEqrXWXWOo_9I8LKlxnD0wx5l0vOI7XMUaGCc\u003D.\u0023\u003DzQBDTcr7NcY0a(_param0, _param1, _param2, _param3, _param4);
  }

  public static void \u0023\u003DzifsHAOd0B2qk(
    float[] _param0,
    int _param1,
    float[] _param2,
    int _param3,
    int _param4)
  {
    if (_param2 == _param0 && _param1 >= _param3)
      throw new Exception("this code needs to be tested");
    \u0023\u003DzV9O5tWduWosGLvu_87Zf5OXt7zllMlwUCoVEqrXWXWOo_9I8LKlxnD0wx5l0vOI7XMUaGCc\u003D.\u0023\u003DzQBDTcr7NcY0a(_param0, _param1, _param2, _param3, _param4);
  }

  public static void \u0023\u003Dz6\u0024HSYSmsjbW7(
    int[] _param0,
    int _param1,
    int _param2,
    int _param3)
  {
    for (int index = 0; index < _param3; ++index)
      _param0[_param1 + index] = _param2;
  }

  public static void \u0023\u003Dz6\u0024HSYSmsjbW7(
    byte[] _param0,
    int _param1,
    byte _param2,
    int _param3)
  {
    for (int index = 0; index < _param3; ++index)
      _param0[_param1 + index] = _param2;
  }

  public static void \u0023\u003DzDrRZp7mp0k89(int[] _param0, int _param1, int _param2)
  {
    for (int index = 0; index < _param2; ++index)
      _param0[_param1 + index] = 0;
  }

  public static void \u0023\u003DzDrRZp7mp0k89(byte[] _param0, int _param1, int _param2)
  {
    for (int index = 0; index < _param2; ++index)
      _param0[_param1 + index] = (byte) 0;
  }

  public static bool \u0023\u003DzIJMcsNznBVx5x4_1Eg\u003D\u003D(
    double _param0,
    double _param1,
    double _param2)
  {
    return Math.Abs(_param0 - _param1) <= _param2;
  }

  public static double \u0023\u003Dzrhv7\u0024gYfW15L(double _param0) => _param0 * Math.PI / 180.0;

  public static double \u0023\u003Dzcmn0QxhOl86n(double _param0) => _param0 * 180.0 / Math.PI;

  public static int \u0023\u003DzQ9DKAFLSaa9H(double _param0)
  {
    return _param0 < 0.0 ? (int) (_param0 - 0.5) : (int) (_param0 + 0.5);
  }

  public static int \u0023\u003DzQ9DKAFLSaa9H(double _param0, int _param1)
  {
    if (_param0 < (double) -_param1)
      return -_param1;
    return _param0 > (double) _param1 ? _param1 : \u0023\u003DzV9O5tWduWosGLvu_87Zf5OXt7zllMlwUCoVEqrXWXWOo_9I8LKlxnD0wx5l0vOI7XMUaGCc\u003D.\u0023\u003DzQ9DKAFLSaa9H(_param0);
  }

  public static int \u0023\u003DzROReRE0C5MV7(double _param0) => (int) (uint) (_param0 + 0.5);

  public static int \u0023\u003Dzj83nbdqVIraZ(double _param0) => (int) (uint) _param0;

  public static int \u0023\u003DzWpDhW47ZWd5f(double _param0) => (int) (uint) Math.Ceiling(_param0);

  public enum \u0023\u003Dz4wqgBpCiq\u00244vrUn_o2ab9Es\u003D
  {
  }

  public enum \u0023\u003Dzp8z7gdwdgeJ\u0024rJWaMDI8Grf4lqgM
  {
  }
}
