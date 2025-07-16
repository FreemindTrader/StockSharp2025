// Decompiled with JetBrains decompiler
// Type: #=zWwDYSe$z_nPO4KEsBbkyAUTgsMETmsdNgd8UYUJWrW04
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
internal sealed class \u0023\u003DzWwDYSe\u0024z_nPO4KEsBbkyAUTgsMETmsdNgd8UYUJWrW04
{
  private \u0023\u003Dz3arZou\u0024KE51WuqbncgcGPkZqxYZZKmiH00SK0z8\u003D \u0023\u003Dz\u0024wP7eNcNWEpx;
  private volatile bool \u0023\u003Dzqa0jMCmen76t;
  private readonly Action dpoChangedEventArgs;
  private readonly \u0023\u003DzK74oGPE3yyB7zop8uDdzn_\u0024VotJ4cPSCfA7R2Hc\u003D \u0023\u003DzVBVazrI\u003D;

  public \u0023\u003DzWwDYSe\u0024z_nPO4KEsBbkyAUTgsMETmsdNgd8UYUJWrW04(
    Action _param1,
    \u0023\u003DzK74oGPE3yyB7zop8uDdzn_\u0024VotJ4cPSCfA7R2Hc\u003D _param2)
  {
    this.dpoChangedEventArgs = _param1;
    this.\u0023\u003DzVBVazrI\u003D = _param2;
  }

  public void \u0023\u003DzrwMjAU8\u003D()
  {
    this.\u0023\u003DzedzqGkG8VlN1();
    this.\u0023\u003DzBH2FQrQ\u003D();
    this.\u0023\u003DzWJbfQAfmCvtw();
  }

  private void \u0023\u003DzedzqGkG8VlN1()
  {
    if (this.\u0023\u003Dz\u0024wP7eNcNWEpx == null)
      return;
    this.\u0023\u003Dz\u0024wP7eNcNWEpx.Dispose();
    this.\u0023\u003Dz\u0024wP7eNcNWEpx = (\u0023\u003Dz3arZou\u0024KE51WuqbncgcGPkZqxYZZKmiH00SK0z8\u003D) null;
  }

  private void \u0023\u003DzBH2FQrQ\u003D()
  {
    this.\u0023\u003Dz\u0024wP7eNcNWEpx = new \u0023\u003Dz3arZou\u0024KE51WuqbncgcGPkZqxYZZKmiH00SK0z8\u003D(new double?(), this.\u0023\u003DzVBVazrI\u003D, new Action(this.\u0023\u003DzWJbfQAfmCvtw));
  }

  public void \u0023\u003DzQkZEqBc0xlZziN1nrQ\u003D\u003D() => this.\u0023\u003DzedzqGkG8VlN1();

  private void \u0023\u003DzWJbfQAfmCvtw()
  {
    if (!this.\u0023\u003Dzqa0jMCmen76t)
      return;
    try
    {
      this.dpoChangedEventArgs();
    }
    finally
    {
      this.\u0023\u003Dzqa0jMCmen76t = false;
    }
  }

  public void Invalidate() => this.\u0023\u003Dzqa0jMCmen76t = true;
}
