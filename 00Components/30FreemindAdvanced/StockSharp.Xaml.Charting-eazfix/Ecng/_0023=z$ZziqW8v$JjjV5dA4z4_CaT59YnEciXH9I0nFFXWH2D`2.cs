// Decompiled with JetBrains decompiler
// Type: #=z$ZziqW8v$JjjV5dA4z4_CaT59YnEciXH9I0nFFXWH2DvMJoLMw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;
using System.Runtime.CompilerServices;

#nullable disable
public sealed class \u0023\u003Dz\u0024ZziqW8v\u0024JjjV5dA4z4_CaT59YnEciXH9I0nFFXWH2DvMJoLMw\u003D\u003D<TX, TY> : 
  \u0023\u003DzmAi_JN5raoSBYo9w2IEI_5Au6CspSbO6KAfA\u00249tysDE2RAfvXg\u003D\u003D<\u0023\u003DzJ9vSi7sIwIEed80npzusCBIsk9iDYaB43AY2Ep7_kjoD>
  where TX : IComparable
  where TY : IComparable
{
  private readonly \u0023\u003DzKasBY8yFp0kHGchcdspopBzm5WEkx4_svXlI48ABMxC7sN4E32vyGbw\u003D \u0023\u003DzXfO9DgaVRj7B;
  private readonly Func<int, TX> \u0023\u003Dzkv0SgdOPhi_0;
  private readonly Func<int, TY> \u0023\u003DzyoB24lWE6a_0;

  public \u0023\u003Dz\u0024ZziqW8v\u0024JjjV5dA4z4_CaT59YnEciXH9I0nFFXWH2DvMJoLMw\u003D\u003D(
    \u0023\u003DzKasBY8yFp0kHGchcdspopBzm5WEkx4_svXlI48ABMxC7sN4E32vyGbw\u003D _param1,
    Func<int, TX> _param2,
    Func<int, TY> _param3)
    : base((IPointSeries) null)
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
      return (\u0023\u003DzVsUQ9A_2kGjOa2mh\u00241UNKld48pAvULrTzJ1tmfY\u003D) new \u0023\u003DzN0ICfvrLGc6u90AzzFcyQrSUX0fLOsOZdqgBUYOpk6V\u0024<TX, TY>(this.\u0023\u003DzXfO9DgaVRj7B, this.\u0023\u003Dzkv0SgdOPhi_0, this.\u0023\u003DzyoB24lWE6a_0, _param1);
    }
  }

  [SpecialName]
  public override int Count()
  {
    return this.\u0023\u003DzXfO9DgaVRj7B.\u0023\u003DzUbuaVJkUy0ct();
  }

  public override DoubleRange \u0023\u003DzxNQHuqrEvxH2()
  {
    return new DoubleRange(this.\u0023\u003DzyoB24lWE6a_0(0).ToDouble(), this.\u0023\u003DzyoB24lWE6a_0(this.\u0023\u003DzXfO9DgaVRj7B.\u0023\u003DzUOzVYhDwNbf3()).ToDouble());
  }
}
