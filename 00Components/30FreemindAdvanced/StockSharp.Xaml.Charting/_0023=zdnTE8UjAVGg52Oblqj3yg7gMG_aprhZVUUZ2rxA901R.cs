// Decompiled with JetBrains decompiler
// Type: #=zdnTE8UjAVGg52Oblqj3yg7gMG_aprhZVUUZ2rxA901RC
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;

#nullable disable
internal sealed class \u0023\u003DzdnTE8UjAVGg52Oblqj3yg7gMG_aprhZVUUZ2rxA901RC
{
  private static uint \u0023\u003DzmbQYIoVdQhkN;
  private readonly long dgr;
  private readonly long dgs;
  private readonly int \u0023\u003Dz5BKXmnjcYbh2SvP6Xw\u003D\u003D;
  private long \u0023\u003DzCdBe1JGiII2y;
  private Tuple<long, long> \u0023\u003Dzb9g4_DOjBJMx;

  internal \u0023\u003DzdnTE8UjAVGg52Oblqj3yg7gMG_aprhZVUUZ2rxA901RC(
    long _param1,
    long _param2,
    int _param3,
    uint _param4 = 10)
  {
    if (_param3 < 1)
      throw new ArgumentException("");
    this.dgr = _param1;
    this.dgs = _param2;
    this.\u0023\u003Dz5BKXmnjcYbh2SvP6Xw\u003D\u003D = _param3;
    \u0023\u003DzdnTE8UjAVGg52Oblqj3yg7gMG_aprhZVUUZ2rxA901RC.\u0023\u003DzmbQYIoVdQhkN = _param4;
    this.\u0023\u003Dz099hDwQ\u003D();
  }

  public Tuple<long, long> \u0023\u003DzAnYO0vLQEDGX()
  {
    return new Tuple<long, long>(this.\u0023\u003DzCdBe1JGiII2y / (long) this.\u0023\u003Dz5BKXmnjcYbh2SvP6Xw\u003D\u003D, this.\u0023\u003DzCdBe1JGiII2y);
  }

  internal Tuple<long, long> \u0023\u003DzPgZsWHrUSHPaSw8veQ\u003D\u003D()
  {
    return this.\u0023\u003Dzb9g4_DOjBJMx;
  }

  private void \u0023\u003Dz099hDwQ\u003D()
  {
    uint num1 = (uint) Math.Max((int) \u0023\u003DzdnTE8UjAVGg52Oblqj3yg7gMG_aprhZVUUZ2rxA901RC.\u0023\u003DzmbQYIoVdQhkN - 1, 1);
    long num2 = this.\u0023\u003Dz_RGjpdoFyFI_(this.dgs - this.dgr, false) / (long) num1;
    this.\u0023\u003DzCdBe1JGiII2y = num2 > 0L ? this.\u0023\u003Dz_RGjpdoFyFI_(num2, true) : 1L;
    this.\u0023\u003Dzb9g4_DOjBJMx = Tuple.Create<long, long>(this.dgr / this.\u0023\u003DzCdBe1JGiII2y * this.\u0023\u003DzCdBe1JGiII2y, this.dgs / this.\u0023\u003DzCdBe1JGiII2y * this.\u0023\u003DzCdBe1JGiII2y);
  }

  private long \u0023\u003Dz_RGjpdoFyFI_(long _param1, bool _param2)
  {
    double y = Math.Floor(Math.Log10((double) _param1));
    double num = ((double) _param1 / Math.Pow(10.0, y)).\u0023\u003DzZsq6ZfbZQvsf(1, MidpointRounding.AwayFromZero);
    return (!_param2 ? (num > 1.0 ? (num > 2.0 ? (num > 5.0 ? 10L : 5L) : 2L) : 1L) : (num >= 1.5 ? (num >= 3.0 ? (num >= 7.0 ? 10L : 5L) : 2L) : 1L)) * (long) Math.Pow(10.0, y);
  }
}
