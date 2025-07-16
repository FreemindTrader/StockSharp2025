// Decompiled with JetBrains decompiler
// Type: #=z7oKBks6ccXdMBOl$qXdcQAaMpPLBW$Zg11zdsYrjLM_XHqr5gklTxg8RrptyaKjnN$fnP2E4v4$F
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
public sealed class \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQAaMpPLBW\u0024Zg11zdsYrjLM_XHqr5gklTxg8RrptyaKjnN\u0024fnP2E4v4\u0024F
{
  private int \u0023\u003DzYTRoyKyN3fBR;
  private int \u0023\u003DzOxmXVfz021CI;
  private int \u0023\u003DzwmOOME4syT6R;

  public \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQAaMpPLBW\u0024Zg11zdsYrjLM_XHqr5gklTxg8RrptyaKjnN\u0024fnP2E4v4\u0024F()
  {
  }

  public \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQAaMpPLBW\u0024Zg11zdsYrjLM_XHqr5gklTxg8RrptyaKjnN\u0024fnP2E4v4\u0024F(
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5,
    int _param6)
  {
    this.\u0023\u003DzYTRoyKyN3fBR = _param3 - _param1;
    this.\u0023\u003DzOxmXVfz021CI = _param4 - _param2;
    this.\u0023\u003DzwmOOME4syT6R = \u0023\u003DzV9O5tWduWosGLvu_87Zf5OXt7zllMlwUCoVEqrXWXWOo_9I8LKlxnD0wx5l0vOI7XMUaGCc\u003D.\u0023\u003DzQ9DKAFLSaa9H((double) (_param5 + 128 /*0x80*/ - _param3) * (double) this.\u0023\u003DzOxmXVfz021CI - (double) (_param6 + 128 /*0x80*/ - _param4) * (double) this.\u0023\u003DzYTRoyKyN3fBR);
    this.\u0023\u003DzYTRoyKyN3fBR <<= 8;
    this.\u0023\u003DzOxmXVfz021CI <<= 8;
  }

  public void \u0023\u003DzgyMj__wzNkSr()
  {
    this.\u0023\u003DzwmOOME4syT6R += this.\u0023\u003DzOxmXVfz021CI;
  }

  public void \u0023\u003Dz_RpGZP9DPu_I()
  {
    this.\u0023\u003DzwmOOME4syT6R -= this.\u0023\u003DzOxmXVfz021CI;
  }

  public void \u0023\u003DzwefICal7Dcfa()
  {
    this.\u0023\u003DzwmOOME4syT6R -= this.\u0023\u003DzYTRoyKyN3fBR;
  }

  public void \u0023\u003Dzf8OSR0x8p5Nn()
  {
    this.\u0023\u003DzwmOOME4syT6R += this.\u0023\u003DzYTRoyKyN3fBR;
  }

  public void \u0023\u003DzgyMj__wzNkSr(int _param1)
  {
    this.\u0023\u003DzwmOOME4syT6R += this.\u0023\u003DzOxmXVfz021CI;
    if (_param1 > 0)
      this.\u0023\u003DzwmOOME4syT6R -= this.\u0023\u003DzYTRoyKyN3fBR;
    if (_param1 >= 0)
      return;
    this.\u0023\u003DzwmOOME4syT6R += this.\u0023\u003DzYTRoyKyN3fBR;
  }

  public void \u0023\u003Dz_RpGZP9DPu_I(int _param1)
  {
    this.\u0023\u003DzwmOOME4syT6R -= this.\u0023\u003DzOxmXVfz021CI;
    if (_param1 > 0)
      this.\u0023\u003DzwmOOME4syT6R -= this.\u0023\u003DzYTRoyKyN3fBR;
    if (_param1 >= 0)
      return;
    this.\u0023\u003DzwmOOME4syT6R += this.\u0023\u003DzYTRoyKyN3fBR;
  }

  public void \u0023\u003DzwefICal7Dcfa(int _param1)
  {
    this.\u0023\u003DzwmOOME4syT6R -= this.\u0023\u003DzYTRoyKyN3fBR;
    if (_param1 > 0)
      this.\u0023\u003DzwmOOME4syT6R += this.\u0023\u003DzOxmXVfz021CI;
    if (_param1 >= 0)
      return;
    this.\u0023\u003DzwmOOME4syT6R -= this.\u0023\u003DzOxmXVfz021CI;
  }

  public void \u0023\u003Dzf8OSR0x8p5Nn(int _param1)
  {
    this.\u0023\u003DzwmOOME4syT6R += this.\u0023\u003DzYTRoyKyN3fBR;
    if (_param1 > 0)
      this.\u0023\u003DzwmOOME4syT6R += this.\u0023\u003DzOxmXVfz021CI;
    if (_param1 >= 0)
      return;
    this.\u0023\u003DzwmOOME4syT6R -= this.\u0023\u003DzOxmXVfz021CI;
  }

  public int \u0023\u003DzgIdxb7A6Diac() => this.\u0023\u003DzwmOOME4syT6R;

  public int \u0023\u003DzOTSgnUI\u003D() => this.\u0023\u003DzYTRoyKyN3fBR;

  public int \u0023\u003Dz5QE1hBg\u003D() => this.\u0023\u003DzOxmXVfz021CI;
}
