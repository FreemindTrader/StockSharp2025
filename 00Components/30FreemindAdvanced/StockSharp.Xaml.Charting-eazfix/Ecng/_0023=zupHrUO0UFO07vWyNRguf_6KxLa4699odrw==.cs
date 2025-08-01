// Decompiled with JetBrains decompiler
// Type: #=zupHrUO0UFO07vWyNRguf_6KxLa4699odrw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;

#nullable disable
public class \u0023\u003DzupHrUO0UFO07vWyNRguf_6KxLa4699odrw\u003D\u003D : 
  SeriesInfo
{
  
  private IComparable \u0023\u003Dzk34bhgA\u003D;

  public \u0023\u003DzupHrUO0UFO07vWyNRguf_6KxLa4699odrw\u003D\u003D(
    IRenderableSeries _param1,
    HitTestInfo _param2)
    : base(_param1, _param2)
  {
    this.ZValue = _param2.ZValue;
  }

  public IComparable ZValue
  {
    get => this.\u0023\u003Dzk34bhgA\u003D;
    set
    {
      this.OnSetPropertyChanged<IComparable>(ref this.\u0023\u003Dzk34bhgA\u003D, value, nameof (ZValue));
    }
  }

  public override void \u0023\u003DzCadMMgc\u003D(
    SeriesInfo _param1)
  {
    base.\u0023\u003DzCadMMgc\u003D(_param1);
    this.ZValue = ((\u0023\u003DzupHrUO0UFO07vWyNRguf_6KxLa4699odrw\u003D\u003D) _param1).ZValue;
  }
}
