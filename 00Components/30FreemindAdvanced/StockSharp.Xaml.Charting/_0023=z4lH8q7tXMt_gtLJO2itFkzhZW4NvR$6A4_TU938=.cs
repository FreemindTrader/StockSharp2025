// Decompiled with JetBrains decompiler
// Type: #=z4lH8q7tXMt_gtLJO2itFkzhZW4NvR$6A4_TU938=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

#nullable enable
internal static class \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D
{
  public static bool \u0023\u003Dz_Q3WCiJm2fzt(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzRKlLwRo\u003D _param0,
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzRKlLwRo\u003D _param1,
    out Point _param2)
  {
    return \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzlI3LM2ikHAvB(_param0, _param1, out _param2, false);
  }

  public static bool \u0023\u003DzlI3LM2ikHAvB(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzRKlLwRo\u003D _param0,
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzRKlLwRo\u003D _param1,
    out Point _param2)
  {
    return \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzlI3LM2ikHAvB(_param0, _param1, out _param2, true);
  }

  private static bool \u0023\u003DzlI3LM2ikHAvB(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzRKlLwRo\u003D _param0,
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzRKlLwRo\u003D _param1,
    out Point _param2,
    bool _param3)
  {
    bool flag1 = \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003Dz4MnLxdg\u003D(_param0);
    bool flag2 = \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003Dz4MnLxdg\u003D(_param1);
    if (flag1 & flag2)
      return \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzQC5xXonPvqnt(_param0, _param1, out _param2);
    if (flag1)
      return \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzU1zvmMf7cFe2(_param0, _param1, out _param2);
    if (flag2)
      return \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzU1zvmMf7cFe2(_param1, _param0, out _param2);
    double num1 = (_param1.\u0023\u003DzZ0gmED0\u003D - _param1.\u0023\u003DzAcftkI4\u003D) * (_param0.\u0023\u003Dz9xgeQi0\u003D - _param0.\u0023\u003DzunK60DE\u003D) - (_param1.\u0023\u003Dz9xgeQi0\u003D - _param1.\u0023\u003DzunK60DE\u003D) * (_param0.\u0023\u003DzZ0gmED0\u003D - _param0.\u0023\u003DzAcftkI4\u003D);
    double num2 = (_param1.\u0023\u003Dz9xgeQi0\u003D - _param1.\u0023\u003DzunK60DE\u003D) * (_param0.\u0023\u003DzAcftkI4\u003D - _param1.\u0023\u003DzAcftkI4\u003D) - (_param1.\u0023\u003DzZ0gmED0\u003D - _param1.\u0023\u003DzAcftkI4\u003D) * (_param0.\u0023\u003DzunK60DE\u003D - _param1.\u0023\u003DzunK60DE\u003D);
    double num3 = (_param0.\u0023\u003Dz9xgeQi0\u003D - _param0.\u0023\u003DzunK60DE\u003D) * (_param0.\u0023\u003DzAcftkI4\u003D - _param1.\u0023\u003DzAcftkI4\u003D) - (_param0.\u0023\u003DzZ0gmED0\u003D - _param0.\u0023\u003DzAcftkI4\u003D) * (_param0.\u0023\u003DzunK60DE\u003D - _param1.\u0023\u003DzunK60DE\u003D);
    if (num2 == 0.0 || num3 == 0.0)
    {
      Point[] pointArray = \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzzNKt\u0024EJ0BK\u0024x8jhH9Fhf8LU\u003D(new Point(_param0.\u0023\u003DzunK60DE\u003D, _param0.\u0023\u003DzAcftkI4\u003D), new Point(_param0.\u0023\u003Dz9xgeQi0\u003D, _param0.\u0023\u003DzZ0gmED0\u003D), new Point(_param1.\u0023\u003DzunK60DE\u003D, _param1.\u0023\u003DzAcftkI4\u003D), new Point(_param1.\u0023\u003Dz9xgeQi0\u003D, _param1.\u0023\u003DzZ0gmED0\u003D));
      if (pointArray.Length != 0)
      {
        _param2 = pointArray[0];
        return true;
      }
    }
    if (num1 == 0.0)
    {
      _param2 = new Point();
      return false;
    }
    double num4 = num2 / num1;
    double num5 = num3 / num1;
    if (_param3 || num4 >= 0.0 && num4 <= 1.0 && num5 >= 0.0 && num5 <= 1.0)
    {
      _param2 = new Point(_param0.\u0023\u003DzunK60DE\u003D + num4 * (_param0.\u0023\u003Dz9xgeQi0\u003D - _param0.\u0023\u003DzunK60DE\u003D), _param0.\u0023\u003DzAcftkI4\u003D + num4 * (_param0.\u0023\u003DzZ0gmED0\u003D - _param0.\u0023\u003DzAcftkI4\u003D));
      return true;
    }
    _param2 = new Point();
    return false;
  }

  private static bool \u0023\u003DzQC5xXonPvqnt(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzRKlLwRo\u003D _param0,
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzRKlLwRo\u003D _param1,
    out Point _param2)
  {
    Point point1 = new Point(_param0.\u0023\u003DzunK60DE\u003D, _param0.\u0023\u003DzAcftkI4\u003D);
    Point point2 = new Point(_param1.\u0023\u003DzunK60DE\u003D, _param1.\u0023\u003DzAcftkI4\u003D);
    _param2 = new Point(point1.X, point1.Y);
    return \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzwAiTZQA\u003D(point1, point2).CompareTo(0.0) == 0;
  }

  public static double \u0023\u003DzwAiTZQA\u003D(Point _param0, Point _param1)
  {
    double num1 = _param0.X - _param1.X;
    double num2 = _param0.Y - _param1.Y;
    return Math.Sqrt(num1 * num1 + num2 * num2);
  }

  public static double \u0023\u003DzYJ5BoptbjSzj(Point _param0, Point _param1)
  {
    double d = Math.PI / 180.0 * (_param1.X - _param0.X);
    double num = 2.0 * _param0.Y * _param1.Y * Math.Cos(d);
    return Math.Sqrt(_param0.Y * _param0.Y + _param1.Y * _param1.Y - num);
  }

  private static bool \u0023\u003DzU1zvmMf7cFe2(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzRKlLwRo\u003D _param0,
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzRKlLwRo\u003D _param1,
    out Point _param2)
  {
    Point point1 = new Point(_param0.\u0023\u003DzunK60DE\u003D, _param0.\u0023\u003DzAcftkI4\u003D);
    Point point2 = new Point(_param1.\u0023\u003DzunK60DE\u003D, _param1.\u0023\u003DzAcftkI4\u003D);
    Point point3 = new Point(_param1.\u0023\u003Dz9xgeQi0\u003D, _param1.\u0023\u003DzZ0gmED0\u003D);
    _param2 = new Point(point1.X, point1.Y);
    return \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzAX\u0024lol1aDgYQ(point1, point2, point3, true).CompareTo(0.0) == 0;
  }

  internal static double \u0023\u003DzAX\u0024lol1aDgYQ(
    Point _param0,
    Point _param1,
    Point _param2,
    bool _param3)
  {
    double num = \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003Dzkgl9_iCbsQoM(_param1, _param2, _param0) / \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzwAiTZQA\u003D(_param1, _param2);
    if (_param3)
    {
      if (\u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzFQjJnKiAdNVJ(_param1, _param2, _param0) > 0.0)
        return \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzwAiTZQA\u003D(_param2, _param0);
      if (\u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzFQjJnKiAdNVJ(_param2, _param1, _param0) > 0.0)
        return \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzwAiTZQA\u003D(_param1, _param0);
    }
    return Math.Abs(num);
  }

  private static double \u0023\u003Dzkgl9_iCbsQoM(Point _param0, Point _param1, Point _param2)
  {
    return (_param1.X - _param0.X) * (_param2.Y - _param0.Y) - (_param1.Y - _param0.Y) * (_param2.X - _param0.X);
  }

  private static double \u0023\u003DzFQjJnKiAdNVJ(Point _param0, Point _param1, Point _param2)
  {
    Point point1 = new Point();
    Point point2 = new Point();
    point1.X = _param1.X - _param0.X;
    point1.Y = _param1.Y - _param0.Y;
    point2.X = _param2.X - _param1.X;
    point2.Y = _param2.Y - _param1.Y;
    return point1.X * point2.X + point1.Y * point2.Y;
  }

  private static 
  #nullable disable
  Point[] \u0023\u003DzzNKt\u0024EJ0BK\u0024x8jhH9Fhf8LU\u003D(
    Point _param0,
    Point _param1,
    Point _param2,
    Point _param3)
  {
    double num1 = _param1.X - _param0.X;
    double num2 = _param1.Y - _param0.Y;
    double num3;
    double num4;
    if (Math.Abs(num1) > Math.Abs(num2))
    {
      num3 = (_param2.X - _param0.X) / num1;
      num4 = (_param3.X - _param0.X) / num1;
    }
    else
    {
      num3 = (_param2.Y - _param0.Y) / num2;
      num4 = (_param3.Y - _param0.Y) / num2;
    }
    List<Point> pointList = new List<Point>();
    foreach (double num5 in \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003Dzbbip4gwVxT9PiTuR1Q\u003D\u003D(num3, num4))
    {
      Point point = new Point(_param1.X * num5 + _param0.X * (1.0 - num5), _param1.Y * num5 + _param0.Y * (1.0 - num5));
      pointList.Add(point);
    }
    return pointList.ToArray();
  }

  private static double[] \u0023\u003Dzbbip4gwVxT9PiTuR1Q\u003D\u003D(
    double _param0,
    double _param1)
  {
    double val2_1 = Math.Min(_param0, _param1);
    double val2_2 = Math.Max(_param0, _param1);
    double num1 = Math.Max(0.0, val2_1);
    double num2 = Math.Min(1.0, val2_2);
    if (num1 > num2)
      return Array.Empty<double>();
    return num1 == num2 ? new double[1]{ num1 } : new double[2]
    {
      num1,
      num2
    };
  }

  internal static bool \u0023\u003DzFP5XPYppz8kpYCPP8A\u003D\u003D(
    Point _param0,
    Point _param1,
    Point _param2)
  {
    return \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003Dzkgl9_iCbsQoM(_param1, _param2, _param0) > 0.0;
  }

  internal static bool \u0023\u003Dz_0KJG0n18I8E(
    Point _param0,
    Point _param1,
    Point _param2,
    Point _param3)
  {
    if (Point.op_Equality(_param1, _param2) && Point.op_Equality(_param2, _param3))
      return Point.op_Equality(_param0, _param1);
    if (Point.op_Equality(_param1, _param2) || Point.op_Equality(_param2, _param3))
      return \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzAX\u0024lol1aDgYQ(_param0, _param1, _param3, true) < double.Epsilon;
    if (Point.op_Equality(_param1, _param3))
      return \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzAX\u0024lol1aDgYQ(_param0, _param1, _param2, true) < double.Epsilon;
    int num1 = \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003Dzkgl9_iCbsQoM(_param0, _param1, _param2) < double.Epsilon ? 1 : 0;
    bool flag1 = \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003Dzkgl9_iCbsQoM(_param0, _param2, _param3) < double.Epsilon;
    bool flag2 = \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003Dzkgl9_iCbsQoM(_param0, _param3, _param1) < double.Epsilon;
    int num2 = flag1 ? 1 : 0;
    return num1 == num2 && flag1 == flag2;
  }

  private static bool \u0023\u003Dz4MnLxdg\u003D(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzRKlLwRo\u003D _param0)
  {
    return Math.Abs(_param0.\u0023\u003DzunK60DE\u003D - _param0.\u0023\u003Dz9xgeQi0\u003D) < double.Epsilon && Math.Abs(_param0.\u0023\u003DzAcftkI4\u003D - _param0.\u0023\u003DzZ0gmED0\u003D) < double.Epsilon;
  }

  internal static bool \u0023\u003DzxGhbraO0gg9\u0024(this Point _param0, Size _param1)
  {
    return _param0.X >= 0.0 && _param0.X <= _param1.Width && _param0.Y >= 0.0 && _param0.Y <= _param1.Height;
  }

  internal static bool \u0023\u003Dzv0egX3NE\u0024EiJR4qnZA\u003D\u003D(
    Point _param0,
    Point _param1,
    Point _param2)
  {
    return (_param0.X * (_param1.Y - _param2.Y) + _param1.X * (_param2.Y - _param0.Y) + _param2.X * (_param0.Y - _param1.Y)).Equals(0.0);
  }

  internal static Point \u0023\u003DzrLjTTj5p96o9(
    this Point _param0,
    Size _param1,
    int _param2,
    int _param3)
  {
    double num1 = _param1.Width + (double) _param3;
    double num2 = _param1.Height + (double) _param2;
    if (_param0.X < (double) -_param3)
      _param0.X = (double) -_param3;
    else if (_param0.X > num1)
      _param0.X = num1;
    if (_param0.Y < (double) -_param2)
      _param0.Y = (double) -_param2;
    else if (_param0.Y > num2)
      _param0.Y = num2;
    return _param0;
  }

  internal static IEnumerable<Point> \u0023\u003DzUIfyPrQYBsuR(
    IEnumerable<Point> _param0,
    Size _param1,
    int _param2,
    int _param3)
  {
    return (IEnumerable<Point>) new \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzgzwoKjR91nxQh5ABtw\u003D\u003D(-2)
    {
      \u0023\u003DziwMdY_txwoDC = _param0,
      \u0023\u003DzvUG33rdW_tUAXVdY9g\u003D\u003D = _param1,
      \u0023\u003DzW8BpjBnBwKqAf89dDA\u003D\u003D = _param2,
      \u0023\u003Dz_yK7M3gPxTsI_6thRw\u003D\u003D = _param3
    };
  }

  public struct \u0023\u003DzRKlLwRo\u003D
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public double \u0023\u003DzunK60DE\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public double \u0023\u003DzAcftkI4\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public double \u0023\u003Dz9xgeQi0\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public double \u0023\u003DzZ0gmED0\u003D;

    public \u0023\u003DzRKlLwRo\u003D(Point _param1, Point _param2)
      : this(_param1.X, _param1.Y, _param2.X, _param2.Y)
    {
    }

    public \u0023\u003DzRKlLwRo\u003D(
      double _param1,
      double _param2,
      double _param3,
      double _param4)
      : this()
    {
      this.\u0023\u003DzunK60DE\u003D = _param1;
      this.\u0023\u003DzAcftkI4\u003D = _param2;
      this.\u0023\u003Dz9xgeQi0\u003D = _param3;
      this.\u0023\u003DzZ0gmED0\u003D = _param4;
    }
  }

  private sealed class \u0023\u003DzgzwoKjR91nxQh5ABtw\u003D\u003D : 
    IEnumerable<Point>,
    IEnumerable,
    IEnumerator<Point>,
    IEnumerator,
    IDisposable
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int \u0023\u003Dz4fzyEZ1SsHYa;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private Point \u0023\u003Dzaev1bhaFFIDX;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int \u0023\u003DzFd7NMxipJEwB36N0OA\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int \u0023\u003DzGwDDlVg\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public int \u0023\u003DzW8BpjBnBwKqAf89dDA\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int \u0023\u003DzM70dnFE\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public int \u0023\u003Dz_yK7M3gPxTsI_6thRw\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private Size \u0023\u003Dz9PpvceM\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public Size \u0023\u003DzvUG33rdW_tUAXVdY9g\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private IEnumerable<Point> \u0023\u003DzGB9Q4U0\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public IEnumerable<Point> \u0023\u003DziwMdY_txwoDC;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private Rect \u0023\u003DzUvAPVIkSl5uA0TQbi0Nq2_s\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private IEnumerator<Point> \u0023\u003Dzc3rfTwtoPKq7U6lxKg\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private Point \u0023\u003Dz2MSoD0cy_vDQWs_rCw\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private double \u0023\u003DzS9Wz8AG_ShZCYq5dHw\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private double \u0023\u003Dz\u0024gm4rwL4NEXcdF40Aw\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private double \u0023\u003DzFKtynyvcTWguKWpZDQ\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private double \u0023\u003DzRdi1l9NBj4s\u00241xsg\u0024w\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private Point \u0023\u003DzQ_VO07S2nHapD4taMQ\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private Point \u0023\u003Dzi90jTq2_xOuxcuse4232_M2nT_gE;

    [DebuggerHidden]
    public \u0023\u003DzgzwoKjR91nxQh5ABtw\u003D\u003D(int _param1)
    {
      this.\u0023\u003Dz4fzyEZ1SsHYa = _param1;
      this.\u0023\u003DzFd7NMxipJEwB36N0OA\u003D\u003D = Environment.CurrentManagedThreadId;
    }

    [DebuggerHidden]
    void IDisposable.\u0023\u003DzyDgD8d_Zy8d21234Xw\u003D\u003D()
    {
      switch (this.\u0023\u003Dz4fzyEZ1SsHYa)
      {
        case -3:
        case 1:
        case 2:
        case 3:
        case 4:
        case 5:
        case 6:
        case 7:
          try
          {
          }
          finally
          {
            this.\u0023\u003Dzs9oL4F7laYDny0tTHw\u003D\u003D();
          }
          break;
      }
    }

    bool IEnumerator.MoveNext()
    {
      // ISSUE: fault handler
      try
      {
        Point? nullable;
        switch (this.\u0023\u003Dz4fzyEZ1SsHYa)
        {
          case 0:
            this.\u0023\u003Dz4fzyEZ1SsHYa = -1;
            nullable = new Point?();
            this.\u0023\u003DzUvAPVIkSl5uA0TQbi0Nq2_s\u003D = new Rect((double) -this.\u0023\u003DzGwDDlVg\u003D, (double) -this.\u0023\u003DzM70dnFE\u003D, this.\u0023\u003Dz9PpvceM\u003D.Width + (double) this.\u0023\u003DzGwDDlVg\u003D, this.\u0023\u003Dz9PpvceM\u003D.Height + (double) this.\u0023\u003DzM70dnFE\u003D);
            this.\u0023\u003Dzc3rfTwtoPKq7U6lxKg\u003D\u003D = this.\u0023\u003DzGB9Q4U0\u003D.GetEnumerator();
            this.\u0023\u003Dz4fzyEZ1SsHYa = -3;
            goto label_17;
          case 1:
            this.\u0023\u003Dz4fzyEZ1SsHYa = -3;
            this.\u0023\u003Dzaev1bhaFFIDX = new Point(this.\u0023\u003DzS9Wz8AG_ShZCYq5dHw\u003D\u003D, this.\u0023\u003Dz\u0024gm4rwL4NEXcdF40Aw\u003D\u003D);
            this.\u0023\u003Dz4fzyEZ1SsHYa = 2;
            return true;
          case 2:
            this.\u0023\u003Dz4fzyEZ1SsHYa = -3;
            this.\u0023\u003Dzaev1bhaFFIDX = new Point(this.\u0023\u003DzFKtynyvcTWguKWpZDQ\u003D\u003D, this.\u0023\u003DzRdi1l9NBj4s\u00241xsg\u0024w\u003D\u003D);
            this.\u0023\u003Dz4fzyEZ1SsHYa = 3;
            return true;
          case 3:
            this.\u0023\u003Dz4fzyEZ1SsHYa = -3;
            break;
          case 4:
            this.\u0023\u003Dz4fzyEZ1SsHYa = -3;
            this.\u0023\u003Dzaev1bhaFFIDX = this.\u0023\u003Dzi90jTq2_xOuxcuse4232_M2nT_gE;
            this.\u0023\u003Dz4fzyEZ1SsHYa = 5;
            return true;
          case 5:
            this.\u0023\u003Dz4fzyEZ1SsHYa = -3;
            this.\u0023\u003Dzaev1bhaFFIDX = this.\u0023\u003DzQ_VO07S2nHapD4taMQ\u003D\u003D;
            this.\u0023\u003Dz4fzyEZ1SsHYa = 6;
            return true;
          case 6:
            this.\u0023\u003Dz4fzyEZ1SsHYa = -3;
            this.\u0023\u003DzQ_VO07S2nHapD4taMQ\u003D\u003D = new Point();
            this.\u0023\u003Dzi90jTq2_xOuxcuse4232_M2nT_gE = new Point();
            break;
          case 7:
            this.\u0023\u003Dz4fzyEZ1SsHYa = -3;
            break;
          case 8:
            this.\u0023\u003Dz4fzyEZ1SsHYa = -1;
            goto label_21;
          default:
            return false;
        }
label_16:
        nullable = new Point?(this.\u0023\u003Dz2MSoD0cy_vDQWs_rCw\u003D\u003D);
        this.\u0023\u003Dz2MSoD0cy_vDQWs_rCw\u003D\u003D = new Point();
label_17:
        if (this.\u0023\u003Dzc3rfTwtoPKq7U6lxKg\u003D\u003D.MoveNext())
        {
          this.\u0023\u003Dz2MSoD0cy_vDQWs_rCw\u003D\u003D = this.\u0023\u003Dzc3rfTwtoPKq7U6lxKg\u003D\u003D.Current;
          if (nullable.HasValue)
          {
            Point point1 = nullable.Value;
            if (!point1.\u0023\u003DzxGhbraO0gg9\u0024(this.\u0023\u003Dz9PpvceM\u003D) || !this.\u0023\u003Dz2MSoD0cy_vDQWs_rCw\u003D\u003D.\u0023\u003DzxGhbraO0gg9\u0024(this.\u0023\u003Dz9PpvceM\u003D))
            {
              this.\u0023\u003DzS9Wz8AG_ShZCYq5dHw\u003D\u003D = point1.X;
              this.\u0023\u003Dz\u0024gm4rwL4NEXcdF40Aw\u003D\u003D = point1.Y;
              this.\u0023\u003DzFKtynyvcTWguKWpZDQ\u003D\u003D = this.\u0023\u003Dz2MSoD0cy_vDQWs_rCw\u003D\u003D.X;
              this.\u0023\u003DzRdi1l9NBj4s\u00241xsg\u0024w\u003D\u003D = this.\u0023\u003Dz2MSoD0cy_vDQWs_rCw\u003D\u003D.Y;
              if (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzNk8cBLuXAKp15HT5R2aU3iOBWzr8(this.\u0023\u003DzUvAPVIkSl5uA0TQbi0Nq2_s\u003D, ref this.\u0023\u003DzS9Wz8AG_ShZCYq5dHw\u003D\u003D, ref this.\u0023\u003Dz\u0024gm4rwL4NEXcdF40Aw\u003D\u003D, ref this.\u0023\u003DzFKtynyvcTWguKWpZDQ\u003D\u003D, ref this.\u0023\u003DzRdi1l9NBj4s\u00241xsg\u0024w\u003D\u003D))
              {
                this.\u0023\u003Dzaev1bhaFFIDX = point1.\u0023\u003DzrLjTTj5p96o9(this.\u0023\u003Dz9PpvceM\u003D, this.\u0023\u003DzM70dnFE\u003D, this.\u0023\u003DzGwDDlVg\u003D);
                this.\u0023\u003Dz4fzyEZ1SsHYa = 1;
                return true;
              }
              Point point2 = point1.\u0023\u003DzrLjTTj5p96o9(this.\u0023\u003Dz9PpvceM\u003D, this.\u0023\u003DzM70dnFE\u003D, this.\u0023\u003DzGwDDlVg\u003D);
              this.\u0023\u003DzQ_VO07S2nHapD4taMQ\u003D\u003D = this.\u0023\u003Dz2MSoD0cy_vDQWs_rCw\u003D\u003D.\u0023\u003DzrLjTTj5p96o9(this.\u0023\u003Dz9PpvceM\u003D, this.\u0023\u003DzM70dnFE\u003D, this.\u0023\u003DzGwDDlVg\u003D);
              bool flag = \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzFP5XPYppz8kpYCPP8A\u003D\u003D(new Point(0.0, 0.0), point1, this.\u0023\u003Dz2MSoD0cy_vDQWs_rCw\u003D\u003D);
              this.\u0023\u003Dzi90jTq2_xOuxcuse4232_M2nT_gE = flag && this.\u0023\u003Dz2MSoD0cy_vDQWs_rCw\u003D\u003D.Y < point1.Y || !flag && this.\u0023\u003Dz2MSoD0cy_vDQWs_rCw\u003D\u003D.Y > point1.Y ? new Point(point2.X, this.\u0023\u003DzQ_VO07S2nHapD4taMQ\u003D\u003D.Y) : new Point(this.\u0023\u003DzQ_VO07S2nHapD4taMQ\u003D\u003D.X, point2.Y);
              this.\u0023\u003Dzaev1bhaFFIDX = point2;
              this.\u0023\u003Dz4fzyEZ1SsHYa = 4;
              return true;
            }
            this.\u0023\u003Dzaev1bhaFFIDX = point1;
            this.\u0023\u003Dz4fzyEZ1SsHYa = 7;
            return true;
          }
          goto label_16;
        }
        this.\u0023\u003Dzs9oL4F7laYDny0tTHw\u003D\u003D();
        this.\u0023\u003Dzc3rfTwtoPKq7U6lxKg\u003D\u003D = (IEnumerator<Point>) null;
        if (nullable.HasValue)
        {
          this.\u0023\u003Dzaev1bhaFFIDX = nullable.Value.\u0023\u003DzrLjTTj5p96o9(this.\u0023\u003Dz9PpvceM\u003D, this.\u0023\u003DzM70dnFE\u003D, this.\u0023\u003DzGwDDlVg\u003D);
          this.\u0023\u003Dz4fzyEZ1SsHYa = 8;
          return true;
        }
label_21:
        return false;
      }
      __fault
      {
        this.\u0023\u003DzyDgD8d_Zy8d21234Xw\u003D\u003D();
      }
    }

    private void \u0023\u003Dzs9oL4F7laYDny0tTHw\u003D\u003D()
    {
      this.\u0023\u003Dz4fzyEZ1SsHYa = -1;
      if (this.\u0023\u003Dzc3rfTwtoPKq7U6lxKg\u003D\u003D == null)
        return;
      this.\u0023\u003Dzc3rfTwtoPKq7U6lxKg\u003D\u003D.Dispose();
    }

    [DebuggerHidden]
    Point IEnumerator<Point>.\u0023\u003DzdQvlJwBrQOkhF8rejS5KemZ9qdsUIguNvslfeT0\u003D()
    {
      return this.\u0023\u003Dzaev1bhaFFIDX;
    }

    [DebuggerHidden]
    void IEnumerator.\u0023\u003Dz__yDkd4DQlAhNe9vxQ\u003D\u003D()
    {
      throw new NotSupportedException();
    }

    [DebuggerHidden]
    object IEnumerator.\u0023\u003DzmTTmbxIPszmU9qSLsSEbSqM\u003D()
    {
      return (object) this.\u0023\u003Dzaev1bhaFFIDX;
    }

    [DebuggerHidden]
    #nullable enable
    IEnumerator<Point> IEnumerable<Point>.\u0023\u003Dzz0QiOq5G7BemC3GqHNUcyAqBFdcssAbLh0fYuZs\u003D()
    {
      \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzgzwoKjR91nxQh5ABtw\u003D\u003D zgzwoKjR91nxQh5Abtw;
      if (this.\u0023\u003Dz4fzyEZ1SsHYa == -2 && this.\u0023\u003DzFd7NMxipJEwB36N0OA\u003D\u003D == Environment.CurrentManagedThreadId)
      {
        this.\u0023\u003Dz4fzyEZ1SsHYa = 0;
        zgzwoKjR91nxQh5Abtw = this;
      }
      else
        zgzwoKjR91nxQh5Abtw = new \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzgzwoKjR91nxQh5ABtw\u003D\u003D(0);
      zgzwoKjR91nxQh5Abtw.\u0023\u003DzGB9Q4U0\u003D = this.\u0023\u003DziwMdY_txwoDC;
      zgzwoKjR91nxQh5Abtw.\u0023\u003Dz9PpvceM\u003D = this.\u0023\u003DzvUG33rdW_tUAXVdY9g\u003D\u003D;
      zgzwoKjR91nxQh5Abtw.\u0023\u003DzGwDDlVg\u003D = this.\u0023\u003DzW8BpjBnBwKqAf89dDA\u003D\u003D;
      zgzwoKjR91nxQh5Abtw.\u0023\u003DzM70dnFE\u003D = this.\u0023\u003Dz_yK7M3gPxTsI_6thRw\u003D\u003D;
      return (IEnumerator<Point>) zgzwoKjR91nxQh5Abtw;
    }

    [DebuggerHidden]
    IEnumerator IEnumerable.\u0023\u003DzSV_TZe7ftMh2SQ86i417Nok\u003D()
    {
      return (IEnumerator) this.\u0023\u003Dzz0QiOq5G7BemC3GqHNUcyAqBFdcssAbLh0fYuZs\u003D();
    }
  }
}
