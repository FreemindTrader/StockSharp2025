// Decompiled with JetBrains decompiler
// Type: #=zuPRmIFUVJkGxyCE55JH19Y1Y7Qx8W3RPdw3pGhctFncRRN9qug==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Runtime.CompilerServices;

#nullable disable
internal sealed class \u0023\u003DzuPRmIFUVJkGxyCE55JH19Y1Y7Qx8W3RPdw3pGhctFncRRN9qug\u003D\u003D : 
  \u0023\u003DztYZOHWyeiGLm7MH\u0024MqDS9fJgMIjfkOcK7kdYTA2avPAE<sbyte>
{
  [SpecialName]
  public sbyte \u0023\u003Dzn4BdrXIKaI7E() => sbyte.MinValue;

  [SpecialName]
  public sbyte \u0023\u003DzNLkUKUcMn0yy() => sbyte.MaxValue;

  [SpecialName]
  public sbyte \u0023\u003DzsQvnxYLDT3j3() => 0;

  public sbyte \u0023\u003DzTOKoqZw\u003D(sbyte _param1, sbyte _param2)
  {
    return (int) _param1 <= (int) _param2 ? _param2 : _param1;
  }

  public sbyte \u0023\u003DzRHWvkgM\u003D(sbyte _param1, sbyte _param2)
  {
    return (int) _param1 >= (int) _param2 ? _param2 : _param1;
  }

  public sbyte \u0023\u003DzBz1ADeDbIZML(sbyte _param1, sbyte _param2, sbyte _param3)
  {
    sbyte num = this.\u0023\u003DzRHWvkgM\u003D(_param2, _param3);
    sbyte koqZw = this.\u0023\u003DzTOKoqZw\u003D(_param2, _param3);
    return num.CompareTo(_param1) <= 0 ? koqZw : num;
  }

  public bool \u0023\u003DzeNpB9guo_tur(sbyte _param1) => false;

  public sbyte \u0023\u003DzFXH4KOE\u003D(sbyte _param1, sbyte _param2)
  {
    return (sbyte) ((int) _param1 - (int) _param2);
  }

  public sbyte \u0023\u003DzyIGkTCg\u003D(sbyte _param1) => Math.Abs(_param1);

  public double \u0023\u003Dzb9UCYbo\u003D(sbyte _param1) => (double) _param1;

  public sbyte \u0023\u003Dz9_a9j8HqDt0f(sbyte _param1, sbyte _param2)
  {
    return (sbyte) ((int) _param1 * (int) _param2);
  }

  public sbyte \u0023\u003Dz9_a9j8HqDt0f(sbyte _param1, double _param2)
  {
    return (sbyte) ((double) _param1 * _param2);
  }

  public sbyte \u0023\u003Dz6wT8xpE\u003D(sbyte _param1, sbyte _param2)
  {
    return (sbyte) ((int) _param1 + (int) _param2);
  }

  public sbyte \u0023\u003DzS\u0024BuL6M\u003D(ref sbyte _param1) => ++_param1;

  public sbyte \u0023\u003DzpTVOY3k\u003D(ref sbyte _param1) => --_param1;
}
