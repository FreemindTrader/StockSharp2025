// Decompiled with JetBrains decompiler
// Type: -.DefaultTickLabel
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

#nullable disable
namespace SciChart.Charting;

internal class DefaultTickLabel : 
  TemplatableControl,
  INotifyPropertyChanged
{
  
  public static readonly DependencyProperty \u0023\u003DzmTt\u0024SfmV25nz = DependencyProperty.Register(nameof (HorizontalAnchorPoint), typeof (HorizontalAnchorPoint), typeof (DefaultTickLabel), new PropertyMetadata((object) HorizontalAnchorPoint.Center, new PropertyChangedCallback(DefaultTickLabel.\u0023\u003Dzz78tLZucgyLCAdY2Vw\u003D\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzNSaqaTMhUL9e = DependencyProperty.Register(nameof (VerticalAnchorPoint), typeof (VerticalAnchorPoint), typeof (DefaultTickLabel), new PropertyMetadata((object) VerticalAnchorPoint.Top, new PropertyChangedCallback(DefaultTickLabel.\u0023\u003Dz_wsJfe2rBBLMHPozxQ\u003D\u003D)));
  
  public static readonly DependencyProperty \u0023\u003Dz1rwx8B8\u003D = DependencyProperty.Register(nameof (Position), typeof (Point), typeof (DefaultTickLabel), new PropertyMetadata((object) new Point(double.NaN, double.NaN), new PropertyChangedCallback(DefaultTickLabel.\u0023\u003DzlDkE84RlgFmy)));
  
  public static readonly DependencyProperty \u0023\u003DzQinmCMXtHUjS = DependencyProperty.Register(nameof (DefaultForeground), typeof (Brush), typeof (DefaultTickLabel), new PropertyMetadata((object) null));
  
  public static readonly DependencyProperty \u0023\u003DzLgY36yN4n7OLzzEgDw\u003D\u003D = DependencyProperty.Register(nameof (DefaultVerticalAnchorPoint), typeof (VerticalAnchorPoint), typeof (DefaultTickLabel), new PropertyMetadata((object) VerticalAnchorPoint.Top));
  
  public static readonly DependencyProperty \u0023\u003DzvGz4ypP\u00247z4yXGlavw\u003D\u003D = DependencyProperty.Register(nameof (DefaultHorizontalAnchorPoint), typeof (HorizontalAnchorPoint), typeof (DefaultTickLabel), new PropertyMetadata((object) HorizontalAnchorPoint.Left));
  
  public static readonly DependencyProperty \u0023\u003Dz32W8b1i6xoYS = DependencyProperty.Register("LayoutTransform", typeof (Transform), typeof (DefaultTickLabel), new PropertyMetadata((PropertyChangedCallback) null));
  
  private int \u0023\u003DzBNy9S01NVbGvmR7R9hW7nc86d\u0024wh;
  
  private Rect \u0023\u003Dz3HMfWnIqbAEXG_WP0A2BwLE9JmwR;

  public DefaultTickLabel()
  {
    this.DefaultStyleKey = (object) typeof (DefaultTickLabel);
  }

  public event PropertyChangedEventHandler PropertyChanged;

  public HorizontalAnchorPoint HorizontalAnchorPoint
  {
    get
    {
      return (HorizontalAnchorPoint) this.GetValue(DefaultTickLabel.\u0023\u003DzmTt\u0024SfmV25nz);
    }
    set
    {
      this.SetValue(DefaultTickLabel.\u0023\u003DzmTt\u0024SfmV25nz, (object) value);
    }
  }

  public VerticalAnchorPoint VerticalAnchorPoint
  {
    get
    {
      return (VerticalAnchorPoint) this.GetValue(DefaultTickLabel.\u0023\u003DzNSaqaTMhUL9e);
    }
    set
    {
      this.SetValue(DefaultTickLabel.\u0023\u003DzNSaqaTMhUL9e, (object) value);
    }
  }

  public Point Position
  {
    get
    {
      return (Point) this.GetValue(DefaultTickLabel.\u0023\u003Dz1rwx8B8\u003D);
    }
    set
    {
      this.SetValue(DefaultTickLabel.\u0023\u003Dz1rwx8B8\u003D, (object) value);
    }
  }

  public Brush DefaultForeground
  {
    get
    {
      return (Brush) this.GetValue(DefaultTickLabel.\u0023\u003DzQinmCMXtHUjS);
    }
  }

  public VerticalAnchorPoint DefaultVerticalAnchorPoint
  {
    get
    {
      return (VerticalAnchorPoint) this.GetValue(DefaultTickLabel.\u0023\u003DzLgY36yN4n7OLzzEgDw\u003D\u003D);
    }
  }

  public HorizontalAnchorPoint DefaultHorizontalAnchorPoint
  {
    get
    {
      return (HorizontalAnchorPoint) this.GetValue(DefaultTickLabel.\u0023\u003DzvGz4ypP\u00247z4yXGlavw\u003D\u003D);
    }
  }

  internal int \u0023\u003DzcWxBLkct8Sd5yemMlw\u003D\u003D()
  {
    return this.\u0023\u003DzBNy9S01NVbGvmR7R9hW7nc86d\u0024wh;
  }

  internal void \u0023\u003Dz9ak5jfg0CSsoUX9AxQ\u003D\u003D(int _param1)
  {
    this.\u0023\u003DzBNy9S01NVbGvmR7R9hW7nc86d\u0024wh = _param1;
  }

  internal Rect \u0023\u003DzLqLOI4\u0024kbsVKm2XaOGI9Rw8\u003D()
  {
    return this.\u0023\u003Dz3HMfWnIqbAEXG_WP0A2BwLE9JmwR;
  }

  internal void \u0023\u003DzT3xod7VLFT46Za\u0024kcJyM7Jw\u003D(Rect _param1)
  {
    this.\u0023\u003Dz3HMfWnIqbAEXG_WP0A2BwLE9JmwR = _param1;
  }

  protected virtual void OnPropertyChanged(string _param1)
  {
    PropertyChangedEventHandler zUapFgog = this.\u0023\u003DzUApFgog\u003D;
    if (zUapFgog == null)
      return;
    zUapFgog((object) this, new PropertyChangedEventArgs(_param1));
  }

  private static void \u0023\u003DzlDkE84RlgFmy(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    DefaultTickLabel.\u0023\u003Dzz78tLZucgyLCAdY2Vw\u003D\u003D(_param0, new DependencyPropertyChangedEventArgs());
    DefaultTickLabel.\u0023\u003Dz_wsJfe2rBBLMHPozxQ\u003D\u003D(_param0, new DependencyPropertyChangedEventArgs());
  }

  private static void \u0023\u003Dzz78tLZucgyLCAdY2Vw\u003D\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is DefaultTickLabel y9QajdhH6H6U9EEjd))
      return;
    y9QajdhH6H6U9EEjd.SetValue(AxisCanvas.\u0023\u003DztX3bWaM\u003D, (object) double.NaN);
    y9QajdhH6H6U9EEjd.SetValue(AxisCanvas.\u0023\u003DzQLHMxl4\u003D, (object) double.NaN);
    y9QajdhH6H6U9EEjd.SetValue(AxisCanvas.\u0023\u003DzHEgPKfijwe68, (object) double.NaN);
    y9QajdhH6H6U9EEjd.SetValue(AxisCanvas.\u0023\u003DzJFNIkcI_P8j8, (object) double.NaN);
    switch (y9QajdhH6H6U9EEjd.HorizontalAnchorPoint)
    {
      case HorizontalAnchorPoint.Left:
        y9QajdhH6H6U9EEjd.SetValue(AxisCanvas.\u0023\u003DztX3bWaM\u003D, (object) y9QajdhH6H6U9EEjd.Position.X);
        break;
      case HorizontalAnchorPoint.Center:
        y9QajdhH6H6U9EEjd.SetValue(AxisCanvas.\u0023\u003DzHEgPKfijwe68, (object) y9QajdhH6H6U9EEjd.Position.X);
        break;
      case HorizontalAnchorPoint.Right:
        y9QajdhH6H6U9EEjd.SetValue(AxisCanvas.\u0023\u003DzQLHMxl4\u003D, (object) y9QajdhH6H6U9EEjd.Position.X);
        break;
    }
  }

  private static void \u0023\u003Dz_wsJfe2rBBLMHPozxQ\u003D\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is DefaultTickLabel y9QajdhH6H6U9EEjd))
      return;
    y9QajdhH6H6U9EEjd.SetValue(AxisCanvas.\u0023\u003DzZpWLYz8\u003D, (object) double.NaN);
    y9QajdhH6H6U9EEjd.SetValue(AxisCanvas.\u0023\u003DzasJeVgQ\u003D, (object) double.NaN);
    y9QajdhH6H6U9EEjd.SetValue(AxisCanvas.\u0023\u003DzLd8ENL0vP3HT, (object) double.NaN);
    y9QajdhH6H6U9EEjd.SetValue(AxisCanvas.\u0023\u003DzIetIZ2A\u00246GdH, (object) double.NaN);
    switch (y9QajdhH6H6U9EEjd.VerticalAnchorPoint)
    {
      case VerticalAnchorPoint.Top:
        y9QajdhH6H6U9EEjd.SetValue(AxisCanvas.\u0023\u003DzZpWLYz8\u003D, (object) y9QajdhH6H6U9EEjd.Position.Y);
        break;
      case VerticalAnchorPoint.Center:
        y9QajdhH6H6U9EEjd.SetValue(AxisCanvas.\u0023\u003DzLd8ENL0vP3HT, (object) y9QajdhH6H6U9EEjd.Position.Y);
        break;
      case VerticalAnchorPoint.Bottom:
        y9QajdhH6H6U9EEjd.SetValue(AxisCanvas.\u0023\u003DzasJeVgQ\u003D, (object) y9QajdhH6H6U9EEjd.Position.Y);
        break;
    }
  }
}
