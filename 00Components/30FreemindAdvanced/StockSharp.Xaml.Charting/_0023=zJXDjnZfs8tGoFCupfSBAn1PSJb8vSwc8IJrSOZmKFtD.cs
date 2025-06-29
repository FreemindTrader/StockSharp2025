// Decompiled with JetBrains decompiler
// Type: #=zJXDjnZfs8tGoFCupfSBAn1PSJb8vSwc8IJrSOZmKFtDT
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System.Runtime.CompilerServices;

#nullable disable
internal sealed class \u0023\u003DzJXDjnZfs8tGoFCupfSBAn1PSJb8vSwc8IJrSOZmKFtDT : 
  \u0023\u003DztYZOHWyeiGLm7MH\u0024MqDS9fJgMIjfkOcK7kdYTA2avPAE<uint>
{
  [SpecialName]
  public uint \u0023\u003Dzn4BdrXIKaI7E() => 0;

  [SpecialName]
  public uint \u0023\u003DzNLkUKUcMn0yy() => uint.MaxValue;

  [SpecialName]
  public uint \u0023\u003DzsQvnxYLDT3j3() => 0;

  public uint \u0023\u003DzTOKoqZw\u003D(uint _param1, uint _param2)
  {
    return _param1 <= _param2 ? _param2 : _param1;
  }

  public uint \u0023\u003DzRHWvkgM\u003D(uint _param1, uint _param2)
  {
    return _param1 >= _param2 ? _param2 : _param1;
  }

  public uint \u0023\u003DzBz1ADeDbIZML(uint _param1, uint _param2, uint _param3)
  {
    uint num = this.\u0023\u003DzRHWvkgM\u003D(_param2, _param3);
    uint koqZw = this.\u0023\u003DzTOKoqZw\u003D(_param2, _param3);
    return num.CompareTo(_param1) <= 0 ? koqZw : num;
  }

  public bool \u0023\u003DzeNpB9guo_tur(uint _param1) => false;

  public uint \u0023\u003DzFXH4KOE\u003D(uint _param1, uint _param2) => _param1 - _param2;

  public uint \u0023\u003DzyIGkTCg\u003D(uint _param1) => _param1;

  public double \u0023\u003Dzb9UCYbo\u003D(uint _param1) => (double) _param1;

  public uint \u0023\u003Dz9_a9j8HqDt0f(uint _param1, uint _param2) => _param1 * _param2;

  public uint \u0023\u003Dz9_a9j8HqDt0f(uint _param1, double _param2)
  {
    return (uint) ((double) _param1 * _param2);
  }

  public uint \u0023\u003Dz6wT8xpE\u003D(uint _param1, uint _param2) => _param1 + _param2;

  public uint \u0023\u003DzS\u0024BuL6M\u003D(ref uint _param1) => ++_param1;

  public uint \u0023\u003DzpTVOY3k\u003D(ref uint _param1) => --_param1;
}
