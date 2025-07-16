// Decompiled with JetBrains decompiler
// Type: #=zd9zvJtNUzU9n1Kn1rKAcAUGkbrTpo0RasN4yVXj5VhTA0420sE3wqYZZ8SLRoBt_43I8mE41BKsQ
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
public sealed class \u0023\u003Dzd9zvJtNUzU9n1Kn1rKAcAUGkbrTpo0RasN4yVXj5VhTA0420sE3wqYZZ8SLRoBt_43I8mE41BKsQ : 
  \u0023\u003Dzq8s_Zceh9qBcjYPACJ3nRNsBClCqagy0OvjeEBllPZvRaPkvRhhJrlp42uAvcBcBfw\u003D\u003D<\u0023\u003DzmAi_JN5raoSBYo9w2IEI_xf3Snq_5g10Q3H7nB4_DWxk20wjU4Dw\u0024eNHICc2N3v3PZtDYjg\u003D>
{
  public override void \u0023\u003DzObQSsmE\u003D(
    \u0023\u003DzmAi_JN5raoSBYo9w2IEI_xf3Snq_5g10Q3H7nB4_DWxk20wjU4Dw\u0024eNHICc2N3v3PZtDYjg\u003D _param1)
  {
    if (this.\u0023\u003DzG2qqjnQ\u003D() > 1 && !this.\u0023\u003DzvsnCYl4\u003D()[this.\u0023\u003DzG2qqjnQ\u003D() - 2].\u0023\u003DzGoZaFyE\u003D(this.\u0023\u003DzvsnCYl4\u003D()[this.\u0023\u003DzG2qqjnQ\u003D() - 1]))
      this.\u0023\u003Dz8ClqfHs\u003D();
    base.\u0023\u003DzObQSsmE\u003D(_param1);
  }

  public void \u0023\u003DzTI_2C2gAGMW03CHW9w\u003D\u003D(
    \u0023\u003DzmAi_JN5raoSBYo9w2IEI_xf3Snq_5g10Q3H7nB4_DWxk20wjU4Dw\u0024eNHICc2N3v3PZtDYjg\u003D _param1)
  {
    this.\u0023\u003Dz8ClqfHs\u003D();
    this.\u0023\u003DzObQSsmE\u003D(_param1);
  }

  public void \u0023\u003DzEzdbhJc\u003D(bool _param1)
  {
    while (this.\u0023\u003DzG2qqjnQ\u003D() > 1 && !this.\u0023\u003DzvsnCYl4\u003D()[this.\u0023\u003DzG2qqjnQ\u003D() - 2].\u0023\u003DzGoZaFyE\u003D(this.\u0023\u003DzvsnCYl4\u003D()[this.\u0023\u003DzG2qqjnQ\u003D() - 1]))
    {
      \u0023\u003DzmAi_JN5raoSBYo9w2IEI_xf3Snq_5g10Q3H7nB4_DWxk20wjU4Dw\u0024eNHICc2N3v3PZtDYjg\u003D nhiCc2N3v3PztDyjg = this[this.\u0023\u003DzG2qqjnQ\u003D() - 1];
      this.\u0023\u003Dz8ClqfHs\u003D();
      this.\u0023\u003DzTI_2C2gAGMW03CHW9w\u003D\u003D(nhiCc2N3v3PztDyjg);
    }
    if (!_param1)
      return;
    while (this.\u0023\u003DzG2qqjnQ\u003D() > 1 && !this.\u0023\u003DzvsnCYl4\u003D()[this.\u0023\u003DzG2qqjnQ\u003D() - 1].\u0023\u003DzGoZaFyE\u003D(this.\u0023\u003DzvsnCYl4\u003D()[0]))
      this.\u0023\u003Dz8ClqfHs\u003D();
  }

  public \u0023\u003DzmAi_JN5raoSBYo9w2IEI_xf3Snq_5g10Q3H7nB4_DWxk20wjU4Dw\u0024eNHICc2N3v3PZtDYjg\u003D \u0023\u003Dz9JbQ4jY\u003D(
    int _param1)
  {
    return this[(_param1 + this.\u0023\u003DzGupBQuw\u003D - 1) % this.\u0023\u003DzGupBQuw\u003D];
  }

  public \u0023\u003DzmAi_JN5raoSBYo9w2IEI_xf3Snq_5g10Q3H7nB4_DWxk20wjU4Dw\u0024eNHICc2N3v3PZtDYjg\u003D \u0023\u003DzLCJuIho\u003D(
    int _param1)
  {
    return this[_param1];
  }

  public \u0023\u003DzmAi_JN5raoSBYo9w2IEI_xf3Snq_5g10Q3H7nB4_DWxk20wjU4Dw\u0024eNHICc2N3v3PZtDYjg\u003D \u0023\u003Dz2zQMp9I\u003D(
    int _param1)
  {
    return this[(_param1 + 1) % this.\u0023\u003DzGupBQuw\u003D];
  }
}
