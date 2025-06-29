// Decompiled with JetBrains decompiler
// Type: #=zm9W_6u1Hb$Y4gq7yl8Gm$07I0wJVDIX8uAYHkX8=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Diagnostics;

#nullable disable
internal sealed class \u0023\u003Dzm9W_6u1Hb\u0024Y4gq7yl8Gm\u002407I0wJVDIX8uAYHkX8\u003D : 
  \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private IComparable \u0023\u003DzL8mySXs\u003D;

  public \u0023\u003Dzm9W_6u1Hb\u0024Y4gq7yl8Gm\u002407I0wJVDIX8uAYHkX8\u003D(
    \u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D _param1,
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param2)
    : base(_param1, _param2)
  {
    this.AccumulatedValue = _param2.\u0023\u003Dzd9IAScWutAfJ();
    this.YValue = _param2.\u0023\u003DzpV2MuX1Y\u0024EoN();
    this.Value = this.YValue.\u0023\u003Dzb9UCYbo\u003D();
  }

  public IComparable AccumulatedValue
  {
    get => this.\u0023\u003DzL8mySXs\u003D;
    set
    {
      this.\u0023\u003DzwGPLgl8\u003D<IComparable>(ref this.\u0023\u003DzL8mySXs\u003D, value, \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539351209));
    }
  }

  public override void \u0023\u003DzCadMMgc\u003D(
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
  {
    base.\u0023\u003DzCadMMgc\u003D(_param1);
    this.AccumulatedValue = ((\u0023\u003Dzm9W_6u1Hb\u0024Y4gq7yl8Gm\u002407I0wJVDIX8uAYHkX8\u003D) _param1).AccumulatedValue;
  }
}
