// Decompiled with JetBrains decompiler
// Type: #=z59_koqr2EQdapDcFKycZuMFujzBx_Vn_sKSeFk9GdLpI
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using \u002D;
using System;
using System.Runtime.CompilerServices;

#nullable disable
internal sealed class \u0023\u003Dz59_koqr2EQdapDcFKycZuMFujzBx_Vn_sKSeFk9GdLpI : 
  \u0023\u003DzmAi_JN5raoSBYo9w2IEI_5Au6CspSbO6KAfA\u00249tysDE2RAfvXg\u003D\u003D<\u0023\u003DzJ9vSi7sIwIEed80npzusCBIsk9iDYaB43AY2Ep7_kjoD>
{
  private readonly \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ \u0023\u003DzA2V_HFN716My;
  private readonly \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ \u0023\u003DzakA_8Bh5_9jJ;

  public \u0023\u003Dz59_koqr2EQdapDcFKycZuMFujzBx_Vn_sKSeFk9GdLpI(
    \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ _param1,
    \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ _param2)
    : base(_param1)
  {
    this.\u0023\u003DzA2V_HFN716My = _param1;
    this.\u0023\u003DzakA_8Bh5_9jJ = _param2;
  }

  [SpecialName]
  public override int \u0023\u003DzlpVGw6E\u003D()
  {
    return this.\u0023\u003DzA2V_HFN716My.\u0023\u003DzlpVGw6E\u003D();
  }

  public new \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ \u0023\u003Dz_\u0024BXHQKXpGkf()
  {
    return this.\u0023\u003DzA2V_HFN716My;
  }

  public \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ \u0023\u003DzPL7HPIragYCv()
  {
    return this.\u0023\u003DzakA_8Bh5_9jJ;
  }

  [IndexerName("#=zMRIb09I=")]
  public override \u0023\u003DzVsUQ9A_2kGjOa2mh\u00241UNKld48pAvULrTzJ1tmfY\u003D this[int _param1]
  {
    get
    {
      return (\u0023\u003DzVsUQ9A_2kGjOa2mh\u00241UNKld48pAvULrTzJ1tmfY\u003D) new \u0023\u003DzzSC94lsu\u00242WfTPlDSLyhlFgNgyQmIWzGYVO4YAqDKpiI<\u0023\u003DzJ9vSi7sIwIEed80npzusCBIsk9iDYaB43AY2Ep7_kjoD>(this.\u0023\u003DzA2V_HFN716My.\u0023\u003Dz\u0024CeUvME\u003D(_param1).Property(), new \u0023\u003DzJ9vSi7sIwIEed80npzusCBIsk9iDYaB43AY2Ep7_kjoD(this.\u0023\u003DzA2V_HFN716My != null ? this.\u0023\u003DzA2V_HFN716My.\u0023\u003Dz\u0024CeUvME\u003D(_param1).\u0023\u003Dzu7q98_E\u003D() : 0.0, this.\u0023\u003DzakA_8Bh5_9jJ.\u0023\u003Dz\u0024CeUvME\u003D(_param1).\u0023\u003Dzu7q98_E\u003D()));
    }
  }

  public override dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd \u0023\u003DzxNQHuqrEvxH2()
  {
    int num1 = this.\u0023\u003DzlpVGw6E\u003D();
    double num2 = double.MaxValue;
    double num3 = double.MinValue;
    for (int index = 0; index < num1; ++index)
    {
      double num4 = this.\u0023\u003DzakA_8Bh5_9jJ.\u0023\u003Dz\u0024CeUvME\u003D(index).\u0023\u003Dzu7q98_E\u003D();
      double num5 = this.\u0023\u003DzA2V_HFN716My.\u0023\u003Dz\u0024CeUvME\u003D(index).\u0023\u003Dzu7q98_E\u003D();
      if (!double.IsNaN(num5) && !double.IsNaN(num4))
      {
        double num6 = Math.Min(num4, num5);
        double num7 = Math.Max(num4, num5);
        num2 = num2 < num6 ? num2 : num6;
        num3 = num3 > num7 ? num3 : num7;
      }
    }
    return new dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd(num2, num3);
  }
}
