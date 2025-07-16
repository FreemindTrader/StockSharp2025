// Decompiled with JetBrains decompiler
// Type: #=z9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;

#nullable disable
public interface IChartModifier : 
  INotifyPropertyChanged,
  IChartModifierBase,
  IReceiveMouseEvents 
{
  ISciChartSurface ParentSurface { get; set; }

  IAxis XAxis { get; }

  IEnumerable<IAxis> YAxes { get; }

  IAxis YAxis { get; }

  IAxis \u0023\u003Dz4uoxB8oLWxeL(
    string _param1);

  bool \u0023\u003DzbOxVzAyGdX66(
    Point _param1,
    IHitTestable _param2);

  void \u0023\u003DzIc5ifvVNpnRzFbQNeA\u003D\u003D();

  void \u0023\u003DzMQsNWQqev3ol8vdG1w\u003D\u003D(
    object _param1,
    NotifyCollectionChangedEventArgs _param2);

  void \u0023\u003DzBwMZf3Z18v_xRgVWSw\u003D\u003D(
    object _param1,
    NotifyCollectionChangedEventArgs _param2);

  void \u0023\u003Dzok6jmLaiH5ai(object _param1, NotifyCollectionChangedEventArgs _param2);
}
