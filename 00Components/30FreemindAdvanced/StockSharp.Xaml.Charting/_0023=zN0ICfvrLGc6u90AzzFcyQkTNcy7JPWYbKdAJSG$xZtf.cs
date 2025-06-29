// Decompiled with JetBrains decompiler
// Type: #=zN0ICfvrLGc6u90AzzFcyQkTNcy7JPWYbKdAJSG$xZtfF
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System.Runtime.CompilerServices;

#nullable disable
internal sealed class \u0023\u003DzN0ICfvrLGc6u90AzzFcyQkTNcy7JPWYbKdAJSG\u0024xZtfF : 
  \u0023\u003DztYZOHWyeiGLm7MH\u0024MqDS9fJgMIjfkOcK7kdYTA2avPAE<ulong>
{
  [SpecialName]
  public ulong \u0023\u003Dzn4BdrXIKaI7E() => 0;

  [SpecialName]
  public ulong \u0023\u003DzNLkUKUcMn0yy() => ulong.MaxValue;

  [SpecialName]
  public ulong \u0023\u003DzsQvnxYLDT3j3() => 0;

  public ulong \u0023\u003DzTOKoqZw\u003D(ulong _param1, ulong _param2)
  {
    return _param1 <= _param2 ? _param2 : _param1;
  }

  public ulong \u0023\u003DzRHWvkgM\u003D(ulong _param1, ulong _param2)
  {
    return _param1 >= _param2 ? _param2 : _param1;
  }

  public ulong \u0023\u003DzBz1ADeDbIZML(ulong _param1, ulong _param2, ulong _param3)
  {
    ulong num = this.\u0023\u003DzRHWvkgM\u003D(_param2, _param3);
    ulong koqZw = this.\u0023\u003DzTOKoqZw\u003D(_param2, _param3);
    return num.CompareTo(_param1) <= 0 ? koqZw : num;
  }

  public bool \u0023\u003DzeNpB9guo_tur(ulong _param1) => false;

  public ulong \u0023\u003DzFXH4KOE\u003D(ulong _param1, ulong _param2) => _param1 - _param2;

  public ulong \u0023\u003DzyIGkTCg\u003D(ulong _param1) => _param1;

  public double \u0023\u003Dzb9UCYbo\u003D(ulong _param1) => (double) _param1;

  public ulong \u0023\u003Dz9_a9j8HqDt0f(ulong _param1, ulong _param2) => _param1 * _param2;

  public ulong \u0023\u003Dz9_a9j8HqDt0f(ulong _param1, double _param2)
  {
    return (ulong) ((double) _param1 * _param2);
  }

  public ulong \u0023\u003Dz6wT8xpE\u003D(ulong _param1, ulong _param2) => _param1 + _param2;

  public ulong \u0023\u003DzS\u0024BuL6M\u003D(ref ulong _param1) => ++_param1;

  public ulong \u0023\u003DzpTVOY3k\u003D(ref ulong _param1) => --_param1;
}
