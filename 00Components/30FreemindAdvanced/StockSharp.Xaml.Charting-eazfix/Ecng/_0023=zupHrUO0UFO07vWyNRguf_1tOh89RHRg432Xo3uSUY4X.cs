// Decompiled with JetBrains decompiler
// Type: #=zupHrUO0UFO07vWyNRguf_1tOh89RHRg432Xo3uSUY4XoCu07N4DQfXXxG3GgLjwrP3Obcvw=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
internal sealed class \u0023\u003DzupHrUO0UFO07vWyNRguf_1tOh89RHRg432Xo3uSUY4XoCu07N4DQfXXxG3GgLjwrP3Obcvw\u003D : 
  \u0023\u003DzRqOhEDBos0N6g4v4zYOaM9GR9bn8mvj2yw4D0iSSz7\u00247IVjNcUWYRVrjRbV\u0024QDTRFg\u003D\u003D
{
  private double \u0023\u003Dzgzm8ZP4\u003D;
  private double \u0023\u003DzSWv5SJI\u003D;
  private double \u0023\u003DzUnjSnH\u0024dFm9e;

  public \u0023\u003DzupHrUO0UFO07vWyNRguf_1tOh89RHRg432Xo3uSUY4XoCu07N4DQfXXxG3GgLjwrP3Obcvw\u003D()
    : this(6.33)
  {
  }

  public \u0023\u003DzupHrUO0UFO07vWyNRguf_1tOh89RHRg432Xo3uSUY4XoCu07N4DQfXXxG3GgLjwrP3Obcvw\u003D(
    double _param1)
  {
    this.\u0023\u003Dzgzm8ZP4\u003D = _param1;
    this.\u0023\u003DzUnjSnH\u0024dFm9e = 1E-12;
    this.\u0023\u003DzSWv5SJI\u003D = 1.0 / this.\u0023\u003Dzk6_X7W2nSWVcrTmzXA\u003D\u003D(_param1);
  }

  public double \u0023\u003Dzh1hhOkJ3kH4Y() => 1.0;

  public double \u0023\u003DzG17fc7\u0024pCNOA(double _param1)
  {
    return this.\u0023\u003Dzk6_X7W2nSWVcrTmzXA\u003D\u003D(this.\u0023\u003Dzgzm8ZP4\u003D * Math.Sqrt(1.0 - _param1 * _param1)) * this.\u0023\u003DzSWv5SJI\u003D;
  }

  private double \u0023\u003Dzk6_X7W2nSWVcrTmzXA\u003D\u003D(double _param1)
  {
    double num1 = 1.0;
    double num2 = _param1 * _param1 / 4.0;
    double num3 = num2;
    int num4 = 2;
    while (num3 > this.\u0023\u003DzUnjSnH\u0024dFm9e)
    {
      num1 += num3;
      num3 *= num2 / (double) (num4 * num4);
      ++num4;
    }
    return num1;
  }
}
