// Decompiled with JetBrains decompiler
// Type: #=zupHrUO0UFO07vWyNRguf_6KxLa4699odrw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;

#nullable disable
internal class \u0023\u003DzupHrUO0UFO07vWyNRguf_6KxLa4699odrw\u003D\u003D : 
  \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private IComparable \u0023\u003Dzk34bhgA\u003D;

  public \u0023\u003DzupHrUO0UFO07vWyNRguf_6KxLa4699odrw\u003D\u003D(
    IRenderableSeries _param1,
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param2)
    : base(_param1, _param2)
  {
    this.ZValue = _param2.ZValue;
  }

  public IComparable ZValue
  {
    get => this.\u0023\u003Dzk34bhgA\u003D;
    set
    {
      this.\u0023\u003DzwGPLgl8\u003D<IComparable>(ref this.\u0023\u003Dzk34bhgA\u003D, value, nameof (ZValue));
    }
  }

  public override void \u0023\u003DzCadMMgc\u003D(
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
  {
    base.\u0023\u003DzCadMMgc\u003D(_param1);
    this.ZValue = ((\u0023\u003DzupHrUO0UFO07vWyNRguf_6KxLa4699odrw\u003D\u003D) _param1).ZValue;
  }
}
