// Decompiled with JetBrains decompiler
// Type: #=zAJ2g5KE5bawCuhjG0TamYsPgfG5ccyJVug==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using StockSharp.Charting;
using System.Collections.Specialized;

#nullable disable
public sealed class YAxisesComboBoxEditSettings : 
  \u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IdiEBhTvW5hdKg\u003D\u003D
{
  protected override INotifyCollectionChanged \u0023\u003Dzzeb_TG_TF3qy(IChartElement _param1)
  {
    return _param1.ChartArea.YAxises as INotifyCollectionChanged;
  }
}
