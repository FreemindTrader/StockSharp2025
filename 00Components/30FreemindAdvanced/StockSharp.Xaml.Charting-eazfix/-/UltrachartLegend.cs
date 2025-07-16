// Decompiled with JetBrains decompiler
// Type: -.UltrachartLegend
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace SciChart.Charting;

internal sealed class UltrachartLegend : 
  ItemsControl
{
  
  public static readonly DependencyProperty \u0023\u003DzfMw6oHlwmSrk = DependencyProperty.Register(nameof (LegendData), typeof (\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSv1H1ORLBnhjlg\u003D\u003D), typeof (UltrachartLegend), new PropertyMetadata((object) null, new PropertyChangedCallback(UltrachartLegend.\u0023\u003DzlsjS\u0024q6k1db3)));
  
  public static readonly DependencyProperty \u0023\u003DzR_CKkzERxHn9SGnaiYrSxEE\u003D = DependencyProperty.Register(nameof (ShowVisibilityCheckboxes), typeof (bool), typeof (UltrachartLegend), new PropertyMetadata((object) false, new PropertyChangedCallback(UltrachartLegend.\u0023\u003Dz4yJK3ICfHaOEZ3fKD9DbViIosNdN)));
  
  public static readonly DependencyProperty \u0023\u003DzGEpRUSytcG_B = DependencyProperty.Register(nameof (ShowSeriesMarkers), typeof (bool), typeof (UltrachartLegend), new PropertyMetadata((object) true));
  
  public static readonly DependencyProperty \u0023\u003DzXMV_skc\u003D = DependencyProperty.Register(nameof (Orientation), typeof (Orientation), typeof (UltrachartLegend), new PropertyMetadata((object) Orientation.Vertical));

  public UltrachartLegend()
  {
    this.DefaultStyleKey = (object) typeof (UltrachartLegend);
  }

  public \u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSv1H1ORLBnhjlg\u003D\u003D LegendData
  {
    get
    {
      return (\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSv1H1ORLBnhjlg\u003D\u003D) this.GetValue(UltrachartLegend.\u0023\u003DzfMw6oHlwmSrk);
    }
    set
    {
      this.SetValue(UltrachartLegend.\u0023\u003DzfMw6oHlwmSrk, (object) value);
    }
  }

  public bool ShowVisibilityCheckboxes
  {
    get
    {
      return (bool) this.GetValue(UltrachartLegend.\u0023\u003DzR_CKkzERxHn9SGnaiYrSxEE\u003D);
    }
    set
    {
      this.SetValue(UltrachartLegend.\u0023\u003DzR_CKkzERxHn9SGnaiYrSxEE\u003D, (object) value);
    }
  }

  public bool ShowSeriesMarkers
  {
    get
    {
      return (bool) this.GetValue(UltrachartLegend.\u0023\u003DzGEpRUSytcG_B);
    }
    set
    {
      this.SetValue(UltrachartLegend.\u0023\u003DzGEpRUSytcG_B, (object) value);
    }
  }

  public Orientation Orientation
  {
    get
    {
      return (Orientation) this.GetValue(UltrachartLegend.\u0023\u003DzXMV_skc\u003D);
    }
    set
    {
      this.SetValue(UltrachartLegend.\u0023\u003DzXMV_skc\u003D, (object) value);
    }
  }

  private static void \u0023\u003Dz4yJK3ICfHaOEZ3fKD9DbViIosNdN(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    UltrachartLegend r886FtpjvsuvaEjd = (UltrachartLegend) _param0;
    if (r886FtpjvsuvaEjd.LegendData == null)
      return;
    r886FtpjvsuvaEjd.LegendData.ShowVisibilityCheckboxes = (bool) _param1.NewValue;
  }

  private static void \u0023\u003DzlsjS\u0024q6k1db3(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    UltrachartLegend r886FtpjvsuvaEjd = (UltrachartLegend) _param0;
    if (!(_param1.NewValue is \u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSv1H1ORLBnhjlg\u003D\u003D newValue))
      return;
    newValue.ShowVisibilityCheckboxes = r886FtpjvsuvaEjd.ShowVisibilityCheckboxes;
  }
}
