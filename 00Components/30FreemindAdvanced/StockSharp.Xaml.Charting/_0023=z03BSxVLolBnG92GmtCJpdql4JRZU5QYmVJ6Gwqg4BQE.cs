// Decompiled with JetBrains decompiler
// Type: #=z03BSxVLolBnG92GmtCJpdql4JRZU5QYmVJ6Gwqg4BQEo
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System.Runtime.CompilerServices;

#nullable disable
internal sealed class \u0023\u003Dz03BSxVLolBnG92GmtCJpdql4JRZU5QYmVJ6Gwqg4BQEo : 
  \u0023\u003DztYZOHWyeiGLm7MH\u0024MqDS9fJgMIjfkOcK7kdYTA2avPAE<ushort>
{
  [SpecialName]
  public ushort \u0023\u003Dzn4BdrXIKaI7E() => 0;

  [SpecialName]
  public ushort \u0023\u003DzNLkUKUcMn0yy() => ushort.MaxValue;

  [SpecialName]
  public ushort \u0023\u003DzsQvnxYLDT3j3() => 0;

  public ushort \u0023\u003DzTOKoqZw\u003D(ushort _param1, ushort _param2)
  {
    return (int) _param1 <= (int) _param2 ? _param2 : _param1;
  }

  public ushort \u0023\u003DzRHWvkgM\u003D(ushort _param1, ushort _param2)
  {
    return (int) _param1 >= (int) _param2 ? _param2 : _param1;
  }

  public ushort \u0023\u003DzBz1ADeDbIZML(ushort _param1, ushort _param2, ushort _param3)
  {
    ushort num = this.\u0023\u003DzRHWvkgM\u003D(_param2, _param3);
    ushort koqZw = this.\u0023\u003DzTOKoqZw\u003D(_param2, _param3);
    return num.CompareTo(_param1) <= 0 ? koqZw : num;
  }

  public bool \u0023\u003DzeNpB9guo_tur(ushort _param1) => false;

  public ushort \u0023\u003DzFXH4KOE\u003D(ushort _param1, ushort _param2)
  {
    return (ushort) ((uint) _param1 - (uint) _param2);
  }

  public ushort \u0023\u003DzyIGkTCg\u003D(ushort _param1) => _param1;

  public double \u0023\u003Dzb9UCYbo\u003D(ushort _param1) => (double) _param1;

  public ushort \u0023\u003Dz9_a9j8HqDt0f(ushort _param1, ushort _param2)
  {
    return (ushort) ((uint) _param1 * (uint) _param2);
  }

  public ushort \u0023\u003Dz9_a9j8HqDt0f(ushort _param1, double _param2)
  {
    return (ushort) ((double) _param1 * _param2);
  }

  public ushort \u0023\u003Dz6wT8xpE\u003D(ushort _param1, ushort _param2)
  {
    return (ushort) ((uint) _param1 + (uint) _param2);
  }

  public ushort \u0023\u003DzS\u0024BuL6M\u003D(ref ushort _param1) => ++_param1;

  public ushort \u0023\u003DzpTVOY3k\u003D(ref ushort _param1) => --_param1;
}
