// Decompiled with JetBrains decompiler
// Type: #=zPvCXK0Pc5P66nz_yS6WU8VnAZ1NnalzvTMgwqgXPDl29eBnig_Va4H5ByklMpTldBWsg9KM=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
public sealed class \u0023\u003DzPvCXK0Pc5P66nz_yS6WU8VnAZ1NnalzvTMgwqgXPDl29eBnig_Va4H5ByklMpTldBWsg9KM\u003D : 
  IImageFilterFunction
{
  private static double \u0023\u003DzXY7EHDGfNlcu(double _param0)
  {
    return _param0 > 0.0 ? _param0 * _param0 * _param0 : 0.0;
  }

  public double radius() => 2.0;

  public double calc_weight(double _param1)
  {
    return 1.0 / 6.0 * (\u0023\u003DzPvCXK0Pc5P66nz_yS6WU8VnAZ1NnalzvTMgwqgXPDl29eBnig_Va4H5ByklMpTldBWsg9KM\u003D.\u0023\u003DzXY7EHDGfNlcu(_param1 + 2.0) - 4.0 * \u0023\u003DzPvCXK0Pc5P66nz_yS6WU8VnAZ1NnalzvTMgwqgXPDl29eBnig_Va4H5ByklMpTldBWsg9KM\u003D.\u0023\u003DzXY7EHDGfNlcu(_param1 + 1.0) + 6.0 * \u0023\u003DzPvCXK0Pc5P66nz_yS6WU8VnAZ1NnalzvTMgwqgXPDl29eBnig_Va4H5ByklMpTldBWsg9KM\u003D.\u0023\u003DzXY7EHDGfNlcu(_param1) - 4.0 * \u0023\u003DzPvCXK0Pc5P66nz_yS6WU8VnAZ1NnalzvTMgwqgXPDl29eBnig_Va4H5ByklMpTldBWsg9KM\u003D.\u0023\u003DzXY7EHDGfNlcu(_param1 - 1.0));
  }
}
