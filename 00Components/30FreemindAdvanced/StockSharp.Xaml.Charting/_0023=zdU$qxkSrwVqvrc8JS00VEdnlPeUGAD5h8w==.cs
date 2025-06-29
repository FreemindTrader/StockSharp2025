// Decompiled with JetBrains decompiler
// Type: #=zdU$qxkSrwVqvrc8JS00VEdnlPeUGAD5h8w==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Diagnostics;

#nullable disable
internal class \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEdnlPeUGAD5h8w\u003D\u003D : 
  \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private double \u0023\u003DzLVApXYh3wio9;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private double \u0023\u003DzgW0Bx\u0024nmVR1e;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private double \u0023\u003DzGnjjvSC2pUUy;

  public \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEdnlPeUGAD5h8w\u003D\u003D(
    \u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D _param1,
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param2)
    : base(_param1, _param2)
  {
    this.HighValue = Convert.ToDouble((object) _param2.\u0023\u003Dz1D\u00248\u0024t39Cb2c());
    this.LowValue = Convert.ToDouble((object) _param2.\u0023\u003DzCH7BygPgTyIy());
    this.CloseValue = Convert.ToDouble((object) _param2.\u0023\u003Dzd9IAScWutAfJ());
  }

  public double HighValue
  {
    get => this.\u0023\u003DzLVApXYh3wio9;
    set
    {
      if (!this.\u0023\u003DzwGPLgl8\u003D<double>(ref this.\u0023\u003DzLVApXYh3wio9, value, XXX.SSS(-539441624)))
        return;
      this.\u0023\u003Dz15moWio\u003D(XXX.SSS(-539441608));
    }
  }

  public string FormattedHighValue => this.\u0023\u003DzwMhD0eRlLP6L((IComparable) this.HighValue);

  public double LowValue
  {
    get => this.\u0023\u003DzgW0Bx\u0024nmVR1e;
    set
    {
      if (!this.\u0023\u003DzwGPLgl8\u003D<double>(ref this.\u0023\u003DzgW0Bx\u0024nmVR1e, value, XXX.SSS(-539441661)))
        return;
      this.\u0023\u003Dz15moWio\u003D(XXX.SSS(-539441648));
    }
  }

  public string FormattedLowValue => this.\u0023\u003DzwMhD0eRlLP6L((IComparable) this.LowValue);

  public double CloseValue
  {
    get => this.\u0023\u003DzGnjjvSC2pUUy;
    set
    {
      if (!this.\u0023\u003DzwGPLgl8\u003D<double>(ref this.\u0023\u003DzGnjjvSC2pUUy, value, XXX.SSS(-539441416)))
        return;
      this.\u0023\u003Dz15moWio\u003D(XXX.SSS(-539441461));
    }
  }

  public string FormattedCloseValue
  {
    get => this.\u0023\u003DzwMhD0eRlLP6L((IComparable) this.CloseValue);
  }

  public override void \u0023\u003DzCadMMgc\u003D(
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
  {
    base.\u0023\u003DzCadMMgc\u003D(_param1);
    \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEdnlPeUGAD5h8w\u003D\u003D js00VednlPeUgaD5h8w = (\u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEdnlPeUGAD5h8w\u003D\u003D) _param1;
    this.HighValue = js00VednlPeUgaD5h8w.HighValue;
    this.LowValue = js00VednlPeUgaD5h8w.LowValue;
    this.CloseValue = js00VednlPeUgaD5h8w.CloseValue;
  }
}
