// Decompiled with JetBrains decompiler
// Type: #=zRYm3Fw8jwwRKksCg00$4P4FxDgUdmelprohP1tiH8Ftb
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Runtime.CompilerServices;

#nullable disable
internal sealed class \u0023\u003DzRYm3Fw8jwwRKksCg00\u00244P4FxDgUdmelprohP1tiH8Ftb : 
  \u0023\u003DztYZOHWyeiGLm7MH\u0024MqDS9fJgMIjfkOcK7kdYTA2avPAE<DateTime>
{
  [SpecialName]
  public DateTime \u0023\u003Dzn4BdrXIKaI7E() => DateTime.MinValue;

  [SpecialName]
  public DateTime \u0023\u003DzNLkUKUcMn0yy() => DateTime.MaxValue;

  [SpecialName]
  public DateTime \u0023\u003DzsQvnxYLDT3j3() => DateTime.MinValue;

  public DateTime \u0023\u003DzTOKoqZw\u003D(DateTime _param1, DateTime _param2)
  {
    return _param1.Ticks <= _param2.Ticks ? _param2 : _param1;
  }

  public DateTime \u0023\u003DzRHWvkgM\u003D(DateTime _param1, DateTime _param2)
  {
    return _param1.Ticks >= _param2.Ticks ? _param2 : _param1;
  }

  public DateTime \u0023\u003DzBz1ADeDbIZML(DateTime _param1, DateTime _param2, DateTime _param3)
  {
    DateTime dateTime = this.\u0023\u003DzRHWvkgM\u003D(_param2, _param3);
    DateTime koqZw = this.\u0023\u003DzTOKoqZw\u003D(_param2, _param3);
    return dateTime.CompareTo(_param1) <= 0 ? koqZw : dateTime;
  }

  public bool \u0023\u003DzeNpB9guo_tur(DateTime _param1) => false;

  public DateTime \u0023\u003DzFXH4KOE\u003D(DateTime _param1, DateTime _param2)
  {
    return new DateTime(_param1.Ticks - _param2.Ticks);
  }

  public DateTime \u0023\u003DzyIGkTCg\u003D(DateTime _param1) => _param1;

  public double \u0023\u003Dzb9UCYbo\u003D(DateTime _param1) => (double) _param1.Ticks;

  public DateTime \u0023\u003Dz9_a9j8HqDt0f(DateTime _param1, DateTime _param2)
  {
    return new DateTime(_param1.Ticks * _param2.Ticks);
  }

  public DateTime \u0023\u003Dz9_a9j8HqDt0f(DateTime _param1, double _param2)
  {
    return new DateTime((long) ((double) _param1.Ticks * _param2));
  }

  public DateTime \u0023\u003Dz6wT8xpE\u003D(DateTime _param1, DateTime _param2)
  {
    return new DateTime(_param1.Ticks + _param2.Ticks);
  }

  public DateTime \u0023\u003DzS\u0024BuL6M\u003D(ref DateTime _param1)
  {
    return new DateTime(_param1.Ticks + 1L);
  }

  public DateTime \u0023\u003DzpTVOY3k\u003D(ref DateTime _param1)
  {
    return new DateTime(_param1.Ticks - 1L);
  }
}
