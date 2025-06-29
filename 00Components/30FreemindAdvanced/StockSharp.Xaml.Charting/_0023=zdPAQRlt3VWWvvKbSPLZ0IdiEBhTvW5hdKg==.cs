// Decompiled with JetBrains decompiler
// Type: #=zdPAQRlt3VWWvvKbSPLZ0IdiEBhTvW5hdKg==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using StockSharp.Charting;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Specialized;

#nullable disable
internal abstract class \u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IdiEBhTvW5hdKg\u003D\u003D : 
  \u0023\u003DzawTMm83sNsuVHdgLsihy4doIUbD\u0024mKpPwQ\u003D\u003D
{
  protected override void \u0023\u003DzC4EgRM0UvJOX(IChartElement _param1)
  {
    this.DisplayMember = "";
    this.ValueMember = "";
    this.ItemsSource = _param1 == null || _param1.ChartArea == null ? (object) Array.Empty<ChartAxis>() : (object) this.\u0023\u003Dzzeb_TG_TF3qy(_param1);
  }

  protected abstract INotifyCollectionChanged \u0023\u003Dzzeb_TG_TF3qy(IChartElement _param1);
}
