// Decompiled with JetBrains decompiler
// Type: #=zsDU9XQyTsl2DjEg2HhKpBHrPPgTSsTv2U_6gZ4ERd6yzf$lpnbFo5CpvYH_k
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System.Diagnostics;

#nullable disable
public sealed class \u0023\u003DzsDU9XQyTsl2DjEg2HhKpBHrPPgTSsTv2U_6gZ4ERd6yzf\u0024lpnbFo5CpvYH_k : 
  RenderSurfaceBase
{
  
  private \u0023\u003DzwAFz4a79BKMMb8uGV0S8\u0024aurvjmQtbZLb71GKZHjczV_N6A4_lPpsbKthY7Nkewpng\u003D\u003D \u0023\u003DzK3L_6jB1hKFR;
  
  private \u0023\u003DzYvV7blprrv\u0024kuBcS9cPJhMudAMKdvrMNDDglbcZ91kqYRNYmqhSBkIjQ8lsq \u0023\u003DzeUisW3maAY9U;
  
  protected public uint[] \u0023\u003DzcNkyaHI7GTSW;

  public \u0023\u003DzsDU9XQyTsl2DjEg2HhKpBHrPPgTSsTv2U_6gZ4ERd6yzf\u0024lpnbFo5CpvYH_k()
  {
    this.\u0023\u003DzSgMgi9QlqY9x();
  }

  public override void \u0023\u003DzSgMgi9QlqY9x()
  {
    base.\u0023\u003DzSgMgi9QlqY9x();
    this.\u0023\u003DzcNkyaHI7GTSW = new uint[this.\u0023\u003DzRIsZuY3LT4U\u0024.PixelWidth];
    this.\u0023\u003DzK3L_6jB1hKFR = new \u0023\u003DzwAFz4a79BKMMb8uGV0S8\u0024aurvjmQtbZLb71GKZHjczV_N6A4_lPpsbKthY7Nkewpng\u003D\u003D(this.\u0023\u003DzRIsZuY3LT4U\u0024.PixelWidth, this.\u0023\u003DzRIsZuY3LT4U\u0024.PixelHeight, 32 /*0x20*/, (\u0023\u003DzzSC94lsu\u00242WfTPlDSLyhlOESA9yl1dafdSqQDeAMKXaBPbCxPlPQgez5bfFbgS\u0024CknPn64g\u003D) new \u0023\u003DzVZAnYWMfoaQCzNrFMqw3u3L4wr8nsid\u00247DvZjvNcM0IIMw_vgUHCtHi5cN88nyND2bXVqyA2T5fC());
    this.\u0023\u003DzeUisW3maAY9U = this.\u0023\u003DzK3L_6jB1hKFR.\u0023\u003Dz9Yt\u0024vKcgxNiu();
  }

  protected override \u0023\u003DzZScQl1C_L0f_XQiTX6oTcyrI5xM77ZuKeI88UaM\u003D \u0023\u003Dzrl_5XeVvahdb()
  {
    return (\u0023\u003DzZScQl1C_L0f_XQiTX6oTcyrI5xM77ZuKeI88UaM\u003D) new \u0023\u003DzQ4iRj1YTApc8D349VbLPOV89ONPzc9V3zddUgPY\u003D();
  }

  public override IRenderContext2D \u0023\u003Dz1cRMfLZU4Eo2()
  {
    return (IRenderContext2D) new \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xCW0709RsZ9Zq1vDAFmVgiaSQqWCDAaSbFLFPC8x(this.\u0023\u003DzZAw3cTjlwxct(), this.\u0023\u003DzRIsZuY3LT4U\u0024, this.\u0023\u003DzcNkyaHI7GTSW, this.\u0023\u003DzK3L_6jB1hKFR, this.\u0023\u003DzeUisW3maAY9U, this.\u0023\u003DzNFOu7BeFZYda());
  }
}
