// Decompiled with JetBrains decompiler
// Type: #=zAfUZ1hld3Aj4_oK9JVqPozXEeeEgBppgwHsFm3ggnXe6
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Runtime.CompilerServices;

#nullable disable
internal sealed class \u0023\u003DzAfUZ1hld3Aj4_oK9JVqPozXEeeEgBppgwHsFm3ggnXe6 : 
  \u0023\u003DztYZOHWyeiGLm7MH\u0024MqDS9fJgMIjfkOcK7kdYTA2avPAE<short>
{
  [SpecialName]
  public short \u0023\u003Dzn4BdrXIKaI7E() => short.MinValue;

  [SpecialName]
  public short \u0023\u003DzNLkUKUcMn0yy() => short.MaxValue;

  [SpecialName]
  public short \u0023\u003DzsQvnxYLDT3j3() => 0;

  public short \u0023\u003DzTOKoqZw\u003D(short _param1, short _param2)
  {
    return (int) _param1 <= (int) _param2 ? _param2 : _param1;
  }

  public short \u0023\u003DzRHWvkgM\u003D(short _param1, short _param2)
  {
    return (int) _param1 >= (int) _param2 ? _param2 : _param1;
  }

  public short \u0023\u003DzBz1ADeDbIZML(short _param1, short _param2, short _param3)
  {
    short num = this.\u0023\u003DzRHWvkgM\u003D(_param2, _param3);
    short koqZw = this.\u0023\u003DzTOKoqZw\u003D(_param2, _param3);
    return num.CompareTo(_param1) <= 0 ? koqZw : num;
  }

  public bool \u0023\u003DzeNpB9guo_tur(short _param1) => false;

  public short \u0023\u003DzFXH4KOE\u003D(short _param1, short _param2)
  {
    return (short) ((int) _param1 - (int) _param2);
  }

  public short \u0023\u003DzyIGkTCg\u003D(short _param1) => Math.Abs(_param1);

  public double \u0023\u003Dzb9UCYbo\u003D(short _param1) => (double) _param1;

  public short \u0023\u003Dz9_a9j8HqDt0f(short _param1, short _param2)
  {
    return (short) ((int) _param1 * (int) _param2);
  }

  public short \u0023\u003Dz9_a9j8HqDt0f(short _param1, double _param2)
  {
    return (short) ((double) _param1 * _param2);
  }

  public short \u0023\u003Dz6wT8xpE\u003D(short _param1, short _param2)
  {
    return (short) ((int) _param1 + (int) _param2);
  }

  public short \u0023\u003DzS\u0024BuL6M\u003D(ref short _param1) => ++_param1;

  public short \u0023\u003DzpTVOY3k\u003D(ref short _param1) => --_param1;
}
