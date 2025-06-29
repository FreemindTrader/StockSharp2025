// Decompiled with JetBrains decompiler
// Type: #=z$ZziqW8v$JjjV5dA4z4_CaT59YnEciXH9I0nFFXWH2DvMJoLMw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using \u002D;
using System;
using System.Runtime.CompilerServices;

#nullable disable
internal sealed class \u0023\u003Dz\u0024ZziqW8v\u0024JjjV5dA4z4_CaT59YnEciXH9I0nFFXWH2DvMJoLMw\u003D\u003D<T, \u0023\u003DzE8zkRfY\u003D> : 
  \u0023\u003DzmAi_JN5raoSBYo9w2IEI_5Au6CspSbO6KAfA\u00249tysDE2RAfvXg\u003D\u003D<\u0023\u003DzJ9vSi7sIwIEed80npzusCBIsk9iDYaB43AY2Ep7_kjoD>
  where T : IComparable
  where \u0023\u003DzE8zkRfY\u003D : IComparable
{
  private readonly \u0023\u003DzKasBY8yFp0kHGchcdspopBzm5WEkx4_svXlI48ABMxC7sN4E32vyGbw\u003D \u0023\u003DzXfO9DgaVRj7B;
  private readonly Func<int, T> \u0023\u003Dzkv0SgdOPhi_0;
  private readonly Func<int, \u0023\u003DzE8zkRfY\u003D> \u0023\u003DzyoB24lWE6a_0;

  public \u0023\u003Dz\u0024ZziqW8v\u0024JjjV5dA4z4_CaT59YnEciXH9I0nFFXWH2DvMJoLMw\u003D\u003D(
    \u0023\u003DzKasBY8yFp0kHGchcdspopBzm5WEkx4_svXlI48ABMxC7sN4E32vyGbw\u003D _param1,
    Func<int, T> _param2,
    Func<int, \u0023\u003DzE8zkRfY\u003D> _param3)
    : base((\u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ) null)
  {
    this.\u0023\u003Dzkv0SgdOPhi_0 = _param2;
    this.\u0023\u003DzyoB24lWE6a_0 = _param3;
    this.\u0023\u003DzXfO9DgaVRj7B = _param1;
  }

  [IndexerName("#=zMRIb09I=")]
  public override \u0023\u003DzVsUQ9A_2kGjOa2mh\u00241UNKld48pAvULrTzJ1tmfY\u003D this[int _param1]
  {
    get
    {
      return (\u0023\u003DzVsUQ9A_2kGjOa2mh\u00241UNKld48pAvULrTzJ1tmfY\u003D) new \u0023\u003DzN0ICfvrLGc6u90AzzFcyQrSUX0fLOsOZdqgBUYOpk6V\u0024<T, \u0023\u003DzE8zkRfY\u003D>(this.\u0023\u003DzXfO9DgaVRj7B, this.\u0023\u003Dzkv0SgdOPhi_0, this.\u0023\u003DzyoB24lWE6a_0, _param1);
    }
  }

  [SpecialName]
  public override int \u0023\u003DzlpVGw6E\u003D()
  {
    return this.\u0023\u003DzXfO9DgaVRj7B.\u0023\u003DzUbuaVJkUy0ct();
  }

  public override dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd \u0023\u003DzxNQHuqrEvxH2()
  {
    return new dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd(this.\u0023\u003DzyoB24lWE6a_0(0).\u0023\u003Dzb9UCYbo\u003D(), this.\u0023\u003DzyoB24lWE6a_0(this.\u0023\u003DzXfO9DgaVRj7B.\u0023\u003DzUOzVYhDwNbf3()).\u0023\u003Dzb9UCYbo\u003D());
  }
}
