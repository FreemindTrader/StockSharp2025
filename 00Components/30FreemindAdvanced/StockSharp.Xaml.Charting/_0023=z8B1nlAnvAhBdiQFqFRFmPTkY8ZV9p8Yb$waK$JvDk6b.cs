// Decompiled with JetBrains decompiler
// Type: #=z8B1nlAnvAhBdiQFqFRFmPTkY8ZV9p8Yb$waK$JvDk6br
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Runtime.CompilerServices;

#nullable disable
internal sealed class \u0023\u003Dz8B1nlAnvAhBdiQFqFRFmPTkY8ZV9p8Yb\u0024waK\u0024JvDk6br : 
  \u0023\u003DztYZOHWyeiGLm7MH\u0024MqDS9fJgMIjfkOcK7kdYTA2avPAE<double>
{
  [SpecialName]
  public double \u0023\u003DzNLkUKUcMn0yy() => double.MaxValue;

  [SpecialName]
  public double \u0023\u003Dzn4BdrXIKaI7E() => double.MinValue;

  [SpecialName]
  public double \u0023\u003DzsQvnxYLDT3j3() => 0.0;

  public double \u0023\u003DzTOKoqZw\u003D(double _param1, double _param2)
  {
    return _param1.\u0023\u003DzeNpB9guo_tur() || !_param2.\u0023\u003DzeNpB9guo_tur() && _param1 <= _param2 ? _param2 : _param1;
  }

  public double \u0023\u003DzRHWvkgM\u003D(double _param1, double _param2)
  {
    return _param1.\u0023\u003DzeNpB9guo_tur() || !_param2.\u0023\u003DzeNpB9guo_tur() && _param1 >= _param2 ? _param2 : _param1;
  }

  public double \u0023\u003DzBz1ADeDbIZML(double _param1, double _param2, double _param3)
  {
    double num = this.\u0023\u003DzRHWvkgM\u003D(_param2, _param3);
    double koqZw = this.\u0023\u003DzTOKoqZw\u003D(_param2, _param3);
    return num.CompareTo(_param1) <= 0 ? koqZw : num;
  }

  public bool \u0023\u003DzeNpB9guo_tur(double _param1) => _param1.\u0023\u003DzeNpB9guo_tur();

  public double \u0023\u003DzFXH4KOE\u003D(double _param1, double _param2) => _param1 - _param2;

  public double \u0023\u003DzyIGkTCg\u003D(double _param1) => Math.Abs(_param1);

  public double \u0023\u003Dzb9UCYbo\u003D(double _param1) => _param1;

  public double \u0023\u003Dz9_a9j8HqDt0f(double _param1, double _param2) => _param1 * _param2;

  public double \u0023\u003Dz6wT8xpE\u003D(double _param1, double _param2) => _param1 + _param2;

  public double \u0023\u003DzS\u0024BuL6M\u003D(ref double _param1) => ++_param1;

  public double \u0023\u003DzpTVOY3k\u003D(ref double _param1) => --_param1;
}
