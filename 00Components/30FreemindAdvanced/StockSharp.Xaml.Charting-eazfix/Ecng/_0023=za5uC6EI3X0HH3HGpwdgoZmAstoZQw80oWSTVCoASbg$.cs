// Decompiled with JetBrains decompiler
// Type: #=za5uC6EI3X0HH3HGpwdgoZmAstoZQw80oWSTVCoASbg$2NN_2m1bmqfPUTmapImzaG6dgeYHzVCAP533$$g==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
public sealed class \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZmAstoZQw80oWSTVCoASbg\u00242NN_2m1bmqfPUTmapImzaG6dgeYHzVCAP533\u0024\u0024g\u003D\u003D : 
  \u0023\u003DzyhbJ\u0024o3\u0024d46dmasRm5VGLHE4noQv5jvPOdidvRVonqXR1_40XGWjzqTPSwaJ
{
  private int \u0023\u003Dzay8Cw54\u003D;
  private int \u0023\u003DzWYndTuds7\u0024Yf;
  private int \u0023\u003DzTPVp5n0ryn49;
  private double \u0023\u003DzF0YeAutsQf6t;
  private double \u0023\u003DzW4cU_AtRwjMP;
  private double \u0023\u003DzE9ACHiZ9CemS;
  private double \u0023\u003Dz1xW1yskBxtqK;

  public \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZmAstoZQw80oWSTVCoASbg\u00242NN_2m1bmqfPUTmapImzaG6dgeYHzVCAP533\u0024\u0024g\u003D\u003D()
  {
    this.\u0023\u003Dzay8Cw54\u003D = 1600;
    this.\u0023\u003DzWYndTuds7\u0024Yf = 0;
    this.\u0023\u003DzTPVp5n0ryn49 = 0;
    this.\u0023\u003Dzs1JQ\u0024lajkyKy();
  }

  public \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZmAstoZQw80oWSTVCoASbg\u00242NN_2m1bmqfPUTmapImzaG6dgeYHzVCAP533\u0024\u0024g\u003D\u003D(
    double _param1,
    double _param2,
    double _param3)
  {
    this.\u0023\u003Dzay8Cw54\u003D = agg_basics.\u0023\u003DzQ9DKAFLSaa9H(_param1 * 16.0);
    this.\u0023\u003DzWYndTuds7\u0024Yf = agg_basics.\u0023\u003DzQ9DKAFLSaa9H(_param2 * 16.0);
    this.\u0023\u003DzTPVp5n0ryn49 = agg_basics.\u0023\u003DzQ9DKAFLSaa9H(_param3 * 16.0);
    this.\u0023\u003Dzs1JQ\u0024lajkyKy();
  }

  public void \u0023\u003Dz0gbwL\u00244\u003D(double _param1, double _param2, double _param3)
  {
    this.\u0023\u003Dzay8Cw54\u003D = agg_basics.\u0023\u003DzQ9DKAFLSaa9H(_param1 * 16.0);
    this.\u0023\u003DzWYndTuds7\u0024Yf = agg_basics.\u0023\u003DzQ9DKAFLSaa9H(_param2 * 16.0);
    this.\u0023\u003DzTPVp5n0ryn49 = agg_basics.\u0023\u003DzQ9DKAFLSaa9H(_param3 * 16.0);
    this.\u0023\u003Dzs1JQ\u0024lajkyKy();
  }

  public double radius() => (double) this.\u0023\u003Dzay8Cw54\u003D / 16.0;

  public double \u0023\u003Dz31cJuBzNTDHV() => (double) this.\u0023\u003DzWYndTuds7\u0024Yf / 16.0;

  public double \u0023\u003Dz7cQ9H6PnPkbj() => (double) this.\u0023\u003DzTPVp5n0ryn49 / 16.0;

  public int calculate(int _param1, int _param2, int _param3)
  {
    double num1 = (double) (_param1 - this.\u0023\u003DzWYndTuds7\u0024Yf);
    double num2 = (double) (_param2 - this.\u0023\u003DzTPVp5n0ryn49);
    double num3 = num1 * (double) this.\u0023\u003DzTPVp5n0ryn49 - num2 * (double) this.\u0023\u003DzWYndTuds7\u0024Yf;
    double num4 = this.\u0023\u003DzF0YeAutsQf6t * (num1 * num1 + num2 * num2) - num3 * num3;
    return agg_basics.\u0023\u003DzQ9DKAFLSaa9H((num1 * (double) this.\u0023\u003DzWYndTuds7\u0024Yf + num2 * (double) this.\u0023\u003DzTPVp5n0ryn49 + Math.Sqrt(Math.Abs(num4))) * this.\u0023\u003Dz1xW1yskBxtqK);
  }

  private void \u0023\u003Dzs1JQ\u0024lajkyKy()
  {
    this.\u0023\u003DzF0YeAutsQf6t = (double) this.\u0023\u003Dzay8Cw54\u003D * (double) this.\u0023\u003Dzay8Cw54\u003D;
    this.\u0023\u003DzW4cU_AtRwjMP = (double) this.\u0023\u003DzWYndTuds7\u0024Yf * (double) this.\u0023\u003DzWYndTuds7\u0024Yf;
    this.\u0023\u003DzE9ACHiZ9CemS = (double) this.\u0023\u003DzTPVp5n0ryn49 * (double) this.\u0023\u003DzTPVp5n0ryn49;
    double num = this.\u0023\u003DzF0YeAutsQf6t - (this.\u0023\u003DzW4cU_AtRwjMP + this.\u0023\u003DzE9ACHiZ9CemS);
    if (num == 0.0)
    {
      if (this.\u0023\u003DzWYndTuds7\u0024Yf != 0)
      {
        if (this.\u0023\u003DzWYndTuds7\u0024Yf < 0)
          ++this.\u0023\u003DzWYndTuds7\u0024Yf;
        else
          --this.\u0023\u003DzWYndTuds7\u0024Yf;
      }
      if (this.\u0023\u003DzTPVp5n0ryn49 != 0)
      {
        if (this.\u0023\u003DzTPVp5n0ryn49 < 0)
          ++this.\u0023\u003DzTPVp5n0ryn49;
        else
          --this.\u0023\u003DzTPVp5n0ryn49;
      }
      this.\u0023\u003DzW4cU_AtRwjMP = (double) this.\u0023\u003DzWYndTuds7\u0024Yf * (double) this.\u0023\u003DzWYndTuds7\u0024Yf;
      this.\u0023\u003DzE9ACHiZ9CemS = (double) this.\u0023\u003DzTPVp5n0ryn49 * (double) this.\u0023\u003DzTPVp5n0ryn49;
      num = this.\u0023\u003DzF0YeAutsQf6t - (this.\u0023\u003DzW4cU_AtRwjMP + this.\u0023\u003DzE9ACHiZ9CemS);
    }
    this.\u0023\u003Dz1xW1yskBxtqK = (double) this.\u0023\u003Dzay8Cw54\u003D / num;
  }
}
