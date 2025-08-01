// Decompiled with JetBrains decompiler
// Type: #=zGULZ_B3lGVEDiq9xPbVQjsPdCs3fSNYVEdhm_bS76Lhc
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using StockSharp.Messages;
using System.Diagnostics;

#nullable disable
public sealed class \u0023\u003DzGULZ_B3lGVEDiq9xPbVQjsPdCs3fSNYVEdhm_bS76Lhc : 
  OhlcSeriesInfo
{
  
  private CandlePriceLevel \u0023\u003DzsQrVoaE\u003D;

  public \u0023\u003DzGULZ_B3lGVEDiq9xPbVQjsPdCs3fSNYVEdhm_bS76Lhc(
    IRenderableSeries _param1,
    HitTestInfo _param2)
    : base(_param1, _param2)
  {
    this.Level = _param2.\u0023\u003DzsTIRngDzycAM();
  }

  public CandlePriceLevel Level
  {
    get => this.\u0023\u003DzsQrVoaE\u003D;
    set
    {
      this.OnSetPropertyChanged<CandlePriceLevel>(ref this.\u0023\u003DzsQrVoaE\u003D, value, nameof (Level));
    }
  }

  public override void \u0023\u003DzCadMMgc\u003D(
    SeriesInfo _param1)
  {
    base.\u0023\u003DzCadMMgc\u003D(_param1);
    this.Level = ((\u0023\u003DzGULZ_B3lGVEDiq9xPbVQjsPdCs3fSNYVEdhm_bS76Lhc) _param1).Level;
  }
}
