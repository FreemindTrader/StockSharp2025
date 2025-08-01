// Decompiled with JetBrains decompiler
// Type: #=zEJoJjwSelM_K3zbmiw1OA_rAdtq$7psDWA==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;
using System.Windows;

#nullable disable
public sealed class \u0023\u003DzEJoJjwSelM_K3zbmiw1OA_rAdtq\u00247psDWA\u003D\u003D : 
  SeriesInfo
{
  
  private IComparable \u0023\u003Dz3Y\u0024NY_pvq2mj;
  
  private Point \u0023\u003Dz1CwHCPFpNKq7;
  
  private bool \u0023\u003Dzu7RI_Cg\u0024p83F;

  public \u0023\u003DzEJoJjwSelM_K3zbmiw1OA_rAdtq\u00247psDWA\u003D\u003D(
    IRenderableSeries _param1,
    HitTestInfo _param2)
    : base(_param1, _param2)
  {
    this.Y1Value = _param2.\u0023\u003DzpV2MuX1Y\u0024EoN();
    this.Xy1Coordinate = _param2.\u0023\u003DzsTZhf2NJangnoun2zQ\u003D\u003D();
  }

  public override object SeriesInfoKey
  {
    get
    {
      return (object) Tuple.Create<IRenderableSeries, bool>(this.RenderableSeries, this.IsFirstSeries);
    }
  }

  public bool IsFirstSeries
  {
    get => this.\u0023\u003Dzu7RI_Cg\u0024p83F;
    set => this.\u0023\u003Dzu7RI_Cg\u0024p83F = value;
  }

  public IComparable Y1Value
  {
    get => this.\u0023\u003Dz3Y\u0024NY_pvq2mj;
    set
    {
      if (!this.OnSetPropertyChanged<IComparable>(ref this.\u0023\u003Dz3Y\u0024NY_pvq2mj, value, nameof (Y1Value)))
        return;
      this.OnPropertyChanged("FormattedY1Value");
    }
  }

  public string FormattedY1Value => this.\u0023\u003DzwMhD0eRlLP6L(this.Y1Value);

  public Point Xy1Coordinate
  {
    get => this.\u0023\u003Dz1CwHCPFpNKq7;
    set
    {
      this.OnSetPropertyChanged<Point>(ref this.\u0023\u003Dz1CwHCPFpNKq7, value, nameof (Xy1Coordinate));
    }
  }

  public override void \u0023\u003DzCadMMgc\u003D(
    SeriesInfo _param1)
  {
    base.\u0023\u003DzCadMMgc\u003D(_param1);
    \u0023\u003DzEJoJjwSelM_K3zbmiw1OA_rAdtq\u00247psDWA\u003D\u003D k3zbmiw1OaRAdtq7psDwa = (\u0023\u003DzEJoJjwSelM_K3zbmiw1OA_rAdtq\u00247psDWA\u003D\u003D) _param1;
    this.Y1Value = k3zbmiw1OaRAdtq7psDwa.Y1Value;
    this.Xy1Coordinate = k3zbmiw1OaRAdtq7psDwa.Xy1Coordinate;
    this.IsFirstSeries = k3zbmiw1OaRAdtq7psDwa.IsFirstSeries;
  }
}
