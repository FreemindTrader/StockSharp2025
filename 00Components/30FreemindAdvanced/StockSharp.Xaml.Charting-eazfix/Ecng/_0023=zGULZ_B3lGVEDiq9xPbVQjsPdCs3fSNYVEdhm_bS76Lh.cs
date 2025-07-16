// Decompiled with JetBrains decompiler
// Type: #=zGULZ_B3lGVEDiq9xPbVQjsPdCs3fSNYVEdhm_bS76Lhc
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using StockSharp.Messages;
using System.Diagnostics;

#nullable disable
internal sealed class \u0023\u003DzGULZ_B3lGVEDiq9xPbVQjsPdCs3fSNYVEdhm_bS76Lhc : 
  \u0023\u003Dz3HkNAtjftY7KLZeVO1e0c8c41pWQbDKntdB13Yg\u003D
{
  
  private CandlePriceLevel \u0023\u003DzsQrVoaE\u003D;

  public \u0023\u003DzGULZ_B3lGVEDiq9xPbVQjsPdCs3fSNYVEdhm_bS76Lhc(
    IRenderableSeries _param1,
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param2)
    : base(_param1, _param2)
  {
    this.Level = _param2.\u0023\u003DzsTIRngDzycAM();
  }

  public CandlePriceLevel Level
  {
    get => this.\u0023\u003DzsQrVoaE\u003D;
    set
    {
      this.\u0023\u003DzwGPLgl8\u003D<CandlePriceLevel>(ref this.\u0023\u003DzsQrVoaE\u003D, value, nameof (Level));
    }
  }

  public override void \u0023\u003DzCadMMgc\u003D(
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
  {
    base.\u0023\u003DzCadMMgc\u003D(_param1);
    this.Level = ((\u0023\u003DzGULZ_B3lGVEDiq9xPbVQjsPdCs3fSNYVEdhm_bS76Lhc) _param1).Level;
  }
}
