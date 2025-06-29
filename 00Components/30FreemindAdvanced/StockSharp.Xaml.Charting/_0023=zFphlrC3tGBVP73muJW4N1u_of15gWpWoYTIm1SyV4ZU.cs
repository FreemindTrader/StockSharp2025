// Decompiled with JetBrains decompiler
// Type: #=zFphlrC3tGBVP73muJW4N1u_of15gWpWoYTIm1SyV4ZUF
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System.Runtime.CompilerServices;

#nullable disable
internal sealed class \u0023\u003DzFphlrC3tGBVP73muJW4N1u_of15gWpWoYTIm1SyV4ZUF : 
  \u0023\u003DztYZOHWyeiGLm7MH\u0024MqDS9fJgMIjfkOcK7kdYTA2avPAE<byte>
{
  [SpecialName]
  public byte \u0023\u003Dzn4BdrXIKaI7E() => 0;

  [SpecialName]
  public byte \u0023\u003DzNLkUKUcMn0yy() => byte.MaxValue;

  [SpecialName]
  public byte \u0023\u003DzsQvnxYLDT3j3() => 0;

  public byte \u0023\u003DzTOKoqZw\u003D(byte _param1, byte _param2)
  {
    return (int) _param1 <= (int) _param2 ? _param2 : _param1;
  }

  public byte \u0023\u003DzRHWvkgM\u003D(byte _param1, byte _param2)
  {
    return (int) _param1 >= (int) _param2 ? _param2 : _param1;
  }

  public byte \u0023\u003DzBz1ADeDbIZML(byte _param1, byte _param2, byte _param3)
  {
    byte num = this.\u0023\u003DzRHWvkgM\u003D(_param2, _param3);
    byte koqZw = this.\u0023\u003DzTOKoqZw\u003D(_param2, _param3);
    return num.CompareTo(_param1) <= 0 ? koqZw : num;
  }

  public bool \u0023\u003DzeNpB9guo_tur(byte _param1) => false;

  public byte \u0023\u003DzFXH4KOE\u003D(byte _param1, byte _param2)
  {
    return (byte) ((uint) _param1 - (uint) _param2);
  }

  public byte \u0023\u003DzyIGkTCg\u003D(byte _param1) => _param1;

  public double \u0023\u003Dzb9UCYbo\u003D(byte _param1) => (double) _param1;

  public byte \u0023\u003Dz9_a9j8HqDt0f(byte _param1, byte _param2)
  {
    return (byte) ((uint) _param1 * (uint) _param2);
  }

  public byte \u0023\u003Dz9_a9j8HqDt0f(byte _param1, double _param2)
  {
    return (byte) ((double) _param1 * _param2);
  }

  public byte \u0023\u003Dz6wT8xpE\u003D(byte _param1, byte _param2)
  {
    return (byte) ((uint) _param1 + (uint) _param2);
  }

  public byte \u0023\u003DzS\u0024BuL6M\u003D(ref byte _param1) => ++_param1;

  public byte \u0023\u003DzpTVOY3k\u003D(ref byte _param1) => --_param1;
}
