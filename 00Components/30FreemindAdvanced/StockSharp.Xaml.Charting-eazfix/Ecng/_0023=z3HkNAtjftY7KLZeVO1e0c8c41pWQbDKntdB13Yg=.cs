// Decompiled with JetBrains decompiler
// Type: #=z3HkNAtjftY7KLZeVO1e0c8c41pWQbDKntdB13Yg=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;

#nullable disable
public class \u0023\u003Dz3HkNAtjftY7KLZeVO1e0c8c41pWQbDKntdB13Yg\u003D : 
  \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEdnlPeUGAD5h8w\u003D\u003D
{
  
  private double \u0023\u003DzJirJ5UAoQttm;

  public \u0023\u003Dz3HkNAtjftY7KLZeVO1e0c8c41pWQbDKntdB13Yg\u003D(
    IRenderableSeries _param1,
    HitTestInfo _param2)
    : base(_param1, _param2)
  {
    this.OpenValue = Convert.ToDouble((object) _param2.\u0023\u003DzlVz0JivzQhAY());
    this.HighValue = Convert.ToDouble((object) _param2.\u0023\u003Dzk8BrWRwbV\u0024Y\u0024());
    this.LowValue = Convert.ToDouble((object) _param2.\u0023\u003Dz89dSIjCLFKC0());
    this.CloseValue = Convert.ToDouble((object) _param2.\u0023\u003DzrRG8qdg_pzoL());
  }

  public double OpenValue
  {
    get => this.\u0023\u003DzJirJ5UAoQttm;
    set
    {
      if (!this.\u0023\u003DzwGPLgl8\u003D<double>(ref this.\u0023\u003DzJirJ5UAoQttm, value, nameof (OpenValue)))
        return;
      this.OnPropertyChanged("FormattedOpenValue");
    }
  }

  public string FormattedOpenValue => this.\u0023\u003DzwMhD0eRlLP6L((IComparable) this.OpenValue);

  public override void \u0023\u003DzCadMMgc\u003D(
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
  {
    base.\u0023\u003DzCadMMgc\u003D(_param1);
    this.OpenValue = ((\u0023\u003Dz3HkNAtjftY7KLZeVO1e0c8c41pWQbDKntdB13Yg\u003D) _param1).OpenValue;
  }
}
