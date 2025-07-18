// Decompiled with JetBrains decompiler
// Type: -.UltraStockChart
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Diagnostics;
using System.Windows;

#nullable disable
namespace StockSharp.Charting;

public sealed class UltraStockChart : 
  SciChartSurface
{
  
  public static readonly DependencyProperty \u0023\u003DzJ9vZ1SlBkImj = DependencyProperty.Register(nameof (XAxisStyle), typeof (Style), typeof (UltraStockChart), new PropertyMetadata((object) null));
  
  public static readonly DependencyProperty \u0023\u003DzSmF8nWOYvMQ_ = DependencyProperty.Register(nameof (YAxisStyle), typeof (Style), typeof (UltraStockChart), new PropertyMetadata((object) null));
  
  public static readonly DependencyProperty \u0023\u003DztbK2Bw0aHrIx = DependencyProperty.Register(nameof (IsCursorEnabled), typeof (bool), typeof (UltraStockChart), new PropertyMetadata((object) false, new PropertyChangedCallback(UltraStockChart.\u0023\u003DzbI1wn6DLqQEI)));
  
  public static readonly DependencyProperty \u0023\u003DzISO1gMx6b9vNqwnNKU5zU7w\u003D = DependencyProperty.Register(nameof (IsRolloverEnabled), typeof (bool), typeof (UltraStockChart), new PropertyMetadata((object) false, new PropertyChangedCallback(UltraStockChart.\u0023\u003DzbI1wn6DLqQEI)));
  
  public static readonly DependencyProperty \u0023\u003Dz_KdgKi_srL\u0024p = DependencyProperty.Register(nameof (IsPanEnabled), typeof (bool), typeof (UltraStockChart), new PropertyMetadata((object) false));
  
  public static readonly DependencyProperty \u0023\u003DzwaZI9uCUost6EXraqCs4gZY\u003D = DependencyProperty.Register(nameof (IsRubberBandZoomEnabled), typeof (bool), typeof (UltraStockChart), new PropertyMetadata((object) false));
  
  public static readonly DependencyProperty \u0023\u003Dzaf3Lae48WNlm = DependencyProperty.Register(nameof (BarTimeFrame), typeof (double), typeof (UltraStockChart), new PropertyMetadata((object) -1.0));
  
  public static readonly DependencyProperty \u0023\u003Dzn\u0024kyZmXv5BnB = DependencyProperty.Register(nameof (IsXAxisVisible), typeof (bool), typeof (UltraStockChart), new PropertyMetadata((object) true, new PropertyChangedCallback(UltraStockChart.\u0023\u003DzJEfp2ojSpmqcIp9R5Q\u003D\u003D)));
  
  public static readonly DependencyProperty \u0023\u003Dzu54g58TexZ1B = DependencyProperty.Register(nameof (VerticalChartGroupId), typeof (string), typeof (UltraStockChart), new PropertyMetadata((object) null, new PropertyChangedCallback(UltraStockChart.\u0023\u003Dz9QrnGw3vI4pcW7I4XA\u003D\u003D)));
  
  public static readonly DependencyProperty \u0023\u003Dzsou0kYCBYC1mAXWfsg\u003D\u003D = DependencyProperty.Register(nameof (IsAxisMarkersEnabled), typeof (bool), typeof (UltraStockChart), new PropertyMetadata((object) true, new PropertyChangedCallback(SciChartSurface.\u0023\u003Dz8nrTMdEZLwbciOTO7Vi3t9Y\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzY8n4mF7fBfJM = DependencyProperty.Register(nameof (LegendSource), typeof (\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSv1H1ORLBnhjlg\u003D\u003D), typeof (UltraStockChart), new PropertyMetadata((PropertyChangedCallback) null));
  
  public static readonly DependencyProperty \u0023\u003DzAFMql64D06Xd = DependencyProperty.Register(nameof (DefaultDataProvider), typeof (InspectSeriesModifierBase), typeof (UltraStockChart), new PropertyMetadata((PropertyChangedCallback) null));
  
  public static readonly DependencyProperty ShowLegendProperty = DependencyProperty.Register(nameof (ShowLegend), typeof (bool), typeof (UltraStockChart), new PropertyMetadata((object) true));
  
  public static readonly DependencyProperty \u0023\u003DzyODCTTGX9yAa = DependencyProperty.Register(nameof (LegendStyle), typeof (Style), typeof (UltraStockChart), new PropertyMetadata((object) null));

  public UltraStockChart()
  {
    this.DefaultStyleKey = (object) typeof (UltraStockChart);
  }

  public InspectSeriesModifierBase DefaultDataProvider
  {
    get
    {
      return (InspectSeriesModifierBase) this.GetValue(UltraStockChart.\u0023\u003DzAFMql64D06Xd);
    }
    set
    {
      this.SetValue(UltraStockChart.\u0023\u003DzAFMql64D06Xd, (object) value);
    }
  }

  public \u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSv1H1ORLBnhjlg\u003D\u003D LegendSource
  {
    get
    {
      return (\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSv1H1ORLBnhjlg\u003D\u003D) this.GetValue(UltraStockChart.\u0023\u003DzY8n4mF7fBfJM);
    }
    set
    {
      this.SetValue(UltraStockChart.\u0023\u003DzY8n4mF7fBfJM, (object) value);
    }
  }

  public bool ShowLegend
  {
    get
    {
      return (bool) this.GetValue(UltraStockChart.ShowLegendProperty);
    }
    set
    {
      this.SetValue(UltraStockChart.ShowLegendProperty, (object) value);
    }
  }

  public Style LegendStyle
  {
    get
    {
      return (Style) this.GetValue(UltraStockChart.\u0023\u003DzyODCTTGX9yAa);
    }
    set
    {
      this.SetValue(UltraStockChart.\u0023\u003DzyODCTTGX9yAa, (object) value);
    }
  }

  public bool IsAxisMarkersEnabled
  {
    get
    {
      return (bool) this.GetValue(UltraStockChart.\u0023\u003Dzsou0kYCBYC1mAXWfsg\u003D\u003D);
    }
    set
    {
      this.SetValue(UltraStockChart.\u0023\u003Dzsou0kYCBYC1mAXWfsg\u003D\u003D, (object) value);
    }
  }

  public string VerticalChartGroupId
  {
    get
    {
      return (string) this.GetValue(UltraStockChart.\u0023\u003Dzu54g58TexZ1B);
    }
    set
    {
      this.SetValue(UltraStockChart.\u0023\u003Dzu54g58TexZ1B, (object) value);
    }
  }

  public Style XAxisStyle
  {
    get
    {
      return (Style) this.GetValue(UltraStockChart.\u0023\u003DzJ9vZ1SlBkImj);
    }
    set
    {
      this.SetValue(UltraStockChart.\u0023\u003DzJ9vZ1SlBkImj, (object) value);
    }
  }

  public Style YAxisStyle
  {
    get
    {
      return (Style) this.GetValue(UltraStockChart.\u0023\u003DzSmF8nWOYvMQ_);
    }
    set
    {
      this.SetValue(UltraStockChart.\u0023\u003DzSmF8nWOYvMQ_, (object) value);
    }
  }

  public bool IsXAxisVisible
  {
    get
    {
      return (bool) this.GetValue(UltraStockChart.\u0023\u003Dzn\u0024kyZmXv5BnB);
    }
    set
    {
      this.SetValue(UltraStockChart.\u0023\u003Dzn\u0024kyZmXv5BnB, (object) value);
    }
  }

  public bool IsCursorEnabled
  {
    get
    {
      return (bool) this.GetValue(UltraStockChart.\u0023\u003DztbK2Bw0aHrIx);
    }
    set
    {
      this.SetValue(UltraStockChart.\u0023\u003DztbK2Bw0aHrIx, (object) value);
    }
  }

  public bool IsRolloverEnabled
  {
    get
    {
      return (bool) this.GetValue(UltraStockChart.\u0023\u003DzISO1gMx6b9vNqwnNKU5zU7w\u003D);
    }
    set
    {
      this.SetValue(UltraStockChart.\u0023\u003DzISO1gMx6b9vNqwnNKU5zU7w\u003D, (object) value);
    }
  }

  public bool IsPanEnabled
  {
    get
    {
      return (bool) this.GetValue(UltraStockChart.\u0023\u003Dz_KdgKi_srL\u0024p);
    }
    set
    {
      this.SetValue(UltraStockChart.\u0023\u003Dz_KdgKi_srL\u0024p, (object) value);
    }
  }

  public bool IsRubberBandZoomEnabled
  {
    get
    {
      return (bool) this.GetValue(UltraStockChart.\u0023\u003DzwaZI9uCUost6EXraqCs4gZY\u003D);
    }
    set
    {
      this.SetValue(UltraStockChart.\u0023\u003DzwaZI9uCUost6EXraqCs4gZY\u003D, (object) value);
    }
  }

  public double BarTimeFrame
  {
    get
    {
      return (double) this.GetValue(UltraStockChart.\u0023\u003Dzaf3Lae48WNlm);
    }
    set
    {
      this.SetValue(UltraStockChart.\u0023\u003Dzaf3Lae48WNlm, (object) value);
    }
  }

  public override void ZoomExtents()
  {
    if (this.YAxes.\u0023\u003DzCCMM80zDpO6N<IAxis>())
      return;
    using (this.SuspendUpdates())
    {
      this.YAxis.GrowBy=((IRange<double>) new DoubleRange(0.1, 0.1));
      base.ZoomExtents();
    }
  }

  private static void \u0023\u003DzJEfp2ojSpmqcIp9R5Q\u003D\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    UltraStockChart muzmbdfsghM62BaEjd = (UltraStockChart) _param0;
    bool newValue = (bool) _param1.NewValue;
    if (muzmbdfsghM62BaEjd.XAxis == null)
      return;
    muzmbdfsghM62BaEjd.XAxis.set_Visibility(newValue ? Visibility.Visible : Visibility.Collapsed);
  }

  private static void \u0023\u003Dz9QrnGw3vI4pcW7I4XA\u003D\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    UltraStockChart muzmbdfsghM62BaEjd = (UltraStockChart) _param0;
    if (muzmbdfsghM62BaEjd == null || !(muzmbdfsghM62BaEjd.ChartModifier is ModifierGroup chartModifier))
      return;
    MouseManager.SetMouseEventGroup((DependencyObject) chartModifier, _param1.NewValue as string);
  }

  private static void \u0023\u003DzbI1wn6DLqQEI(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is UltraStockChart muzmbdfsghM62BaEjd))
      return;
    ModifierGroup chartModifier = (ModifierGroup) muzmbdfsghM62BaEjd.ChartModifier;
    if (chartModifier == null)
      return;
    InspectSeriesModifierBase c8K7BtmrprxtexEjd = (InspectSeriesModifierBase) chartModifier["LegendModifier"];
    if (muzmbdfsghM62BaEjd.IsRolloverEnabled)
      c8K7BtmrprxtexEjd = (InspectSeriesModifierBase) chartModifier["RolloverModifier"];
    else if (muzmbdfsghM62BaEjd.IsCursorEnabled)
      c8K7BtmrprxtexEjd = (InspectSeriesModifierBase) chartModifier["CursorModifier"];
    muzmbdfsghM62BaEjd.DefaultDataProvider = c8K7BtmrprxtexEjd;
  }
}
