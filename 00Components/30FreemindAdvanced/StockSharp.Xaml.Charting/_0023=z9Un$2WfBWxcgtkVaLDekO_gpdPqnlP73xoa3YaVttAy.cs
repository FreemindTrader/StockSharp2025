// Decompiled with JetBrains decompiler
// Type: #=z9Un$2WfBWxcgtkVaLDekO_gpdPqnlP73xoa3YaVttAyX
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Runtime.CompilerServices;

#nullable disable
internal sealed class \u0023\u003Dz9Un\u00242WfBWxcgtkVaLDekO_gpdPqnlP73xoa3YaVttAyX : 
  \u0023\u003DztYZOHWyeiGLm7MH\u0024MqDS9fJgMIjfkOcK7kdYTA2avPAE<TimeSpan>
{
  [SpecialName]
  public TimeSpan \u0023\u003Dzn4BdrXIKaI7E() => TimeSpan.MinValue;

  [SpecialName]
  public TimeSpan \u0023\u003DzNLkUKUcMn0yy() => TimeSpan.MaxValue;

  [SpecialName]
  public TimeSpan \u0023\u003DzsQvnxYLDT3j3() => TimeSpan.Zero;

  public TimeSpan \u0023\u003DzTOKoqZw\u003D(TimeSpan _param1, TimeSpan _param2)
  {
    return _param1.Ticks <= _param2.Ticks ? _param2 : _param1;
  }

  public TimeSpan \u0023\u003DzRHWvkgM\u003D(TimeSpan _param1, TimeSpan _param2)
  {
    return _param1.Ticks >= _param2.Ticks ? _param2 : _param1;
  }

  public TimeSpan \u0023\u003DzBz1ADeDbIZML(TimeSpan _param1, TimeSpan _param2, TimeSpan _param3)
  {
    TimeSpan timeSpan = this.\u0023\u003DzRHWvkgM\u003D(_param2, _param3);
    TimeSpan koqZw = this.\u0023\u003DzTOKoqZw\u003D(_param2, _param3);
    return timeSpan.CompareTo(_param1) <= 0 ? koqZw : timeSpan;
  }

  public bool \u0023\u003DzeNpB9guo_tur(TimeSpan _param1) => false;

  public TimeSpan \u0023\u003DzFXH4KOE\u003D(TimeSpan _param1, TimeSpan _param2)
  {
    return _param1 - _param2;
  }

  public TimeSpan \u0023\u003DzyIGkTCg\u003D(TimeSpan _param1) => _param1;

  public double \u0023\u003Dzb9UCYbo\u003D(TimeSpan _param1) => (double) _param1.Ticks;

  public TimeSpan \u0023\u003Dz9_a9j8HqDt0f(TimeSpan _param1, TimeSpan _param2)
  {
    return new TimeSpan(_param1.Ticks * _param2.Ticks);
  }

  public TimeSpan \u0023\u003Dz9_a9j8HqDt0f(TimeSpan _param1, double _param2)
  {
    return new TimeSpan((long) ((double) _param1.Ticks * _param2));
  }

  public TimeSpan \u0023\u003Dz6wT8xpE\u003D(TimeSpan _param1, TimeSpan _param2)
  {
    return new TimeSpan(_param1.Ticks + _param2.Ticks);
  }

  public TimeSpan \u0023\u003DzS\u0024BuL6M\u003D(ref TimeSpan _param1)
  {
    return new TimeSpan(_param1.Ticks + 1L);
  }

  public TimeSpan \u0023\u003DzpTVOY3k\u003D(ref TimeSpan _param1)
  {
    return new TimeSpan(_param1.Ticks - 1L);
  }
}
