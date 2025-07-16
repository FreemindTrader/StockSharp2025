// Decompiled with JetBrains decompiler
// Type: -.HeatmapColourMap
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace SciChart.Charting;

internal sealed class HeatmapColourMap : 
  Control,
  INotifyPropertyChanged
{
  
  public static readonly DependencyProperty \u0023\u003DzaKv9NrOuOcapF0LrKNnfOo9b1\u0024C2bSjheA\u003D\u003D = DependencyProperty.Register(nameof (FastHeatMapRenderableSeries), typeof (FastHeatMapRenderableSeries), typeof (HeatmapColourMap), new PropertyMetadata((object) null, new PropertyChangedCallback(HeatmapColourMap.\u0023\u003DzPWvFs4bMVSvB)));
  
  public static readonly DependencyProperty \u0023\u003DzXMV_skc\u003D = DependencyProperty.Register(nameof (Orientation), typeof (Orientation), typeof (HeatmapColourMap), new PropertyMetadata((object) Orientation.Horizontal, new PropertyChangedCallback(HeatmapColourMap.\u0023\u003DzcZqyvQl0j0My)));

  public HeatmapColourMap()
  {
    this.DefaultStyleKey = (object) typeof (HeatmapColourMap);
  }

  public FastHeatMapRenderableSeries FastHeatMapRenderableSeries
  {
    get
    {
      return (FastHeatMapRenderableSeries) this.GetValue(HeatmapColourMap.\u0023\u003DzaKv9NrOuOcapF0LrKNnfOo9b1\u0024C2bSjheA\u003D\u003D);
    }
    set
    {
      this.SetValue(HeatmapColourMap.\u0023\u003DzaKv9NrOuOcapF0LrKNnfOo9b1\u0024C2bSjheA\u003D\u003D, (object) value);
    }
  }

  public Orientation Orientation
  {
    get
    {
      return (Orientation) this.GetValue(HeatmapColourMap.\u0023\u003DzXMV_skc\u003D);
    }
    set
    {
      this.SetValue(HeatmapColourMap.\u0023\u003DzXMV_skc\u003D, (object) value);
    }
  }

  private static void \u0023\u003DzPWvFs4bMVSvB(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
  }

  private static void \u0023\u003DzcZqyvQl0j0My(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
  }

  public event PropertyChangedEventHandler PropertyChanged;
}
