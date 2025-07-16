// Decompiled with JetBrains decompiler
// Type: -.StackedColumnRenderableSeries
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;

#nullable disable
namespace StockSharp.Charting;

public sealed class StackedColumnRenderableSeries : 
  BaseColumnRenderableSeries,
  IDrawable,
  IXmlSerializable,
  \u0023\u003Dz5B3gvTTfbmLYjDPhZPGfZJtupKNFWXP0_On1YUVI0hqJ\u0024lotV9V57okcKlXHXNUKOsbYO\u0024c\u003D,
  \u0023\u003Dz5VLaAZX2bctAcuSoajSAXvZYOg6JAbLCIgQvZp9odw6FSOKg1daH3vPLNHtT2ZG4iQ\u003D\u003D,
  IRenderableSeries,
  \u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D
{
  
  public static readonly DependencyProperty \u0023\u003Dz2Rta\u0024oTnlQkx = DependencyProperty.Register(nameof (StackedGroupId), typeof (string), typeof (StackedColumnRenderableSeries), new PropertyMetadata((object) "DefaultStackedGroupId", new PropertyChangedCallback(StackedColumnRenderableSeries.\u0023\u003DzyGQyqAS53k26)));
  
  public static readonly DependencyProperty \u0023\u003Dzj\u0024ZDuobq5\u0024GmruUxggpnzio\u003D = DependencyProperty.Register(nameof (IsOneHundredPercent), typeof (bool), typeof (StackedColumnRenderableSeries), new PropertyMetadata((object) false));
  
  public static readonly DependencyProperty \u0023\u003DzYrTUOus\u003D = DependencyProperty.Register(nameof (Spacing), typeof (double), typeof (StackedColumnRenderableSeries), new PropertyMetadata((object) 0.1, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzDmC1x3LgSH85 = DependencyProperty.Register(nameof (SpacingMode), typeof (\u0023\u003DzUJpBz2W8IzAtBIqVtQXHBxp6vbyZAM3jtrSiCk74BV066G8ZjcZkoR4\u003D), typeof (StackedColumnRenderableSeries), new PropertyMetadata((object) \u0023\u003DzUJpBz2W8IzAtBIqVtQXHBxp6vbyZAM3jtrSiCk74BV066G8ZjcZkoR4\u003D.Relative, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003Dzj9GlhPUqVKhn = DependencyProperty.Register(nameof (ShowLabel), typeof (bool), typeof (StackedColumnRenderableSeries), new PropertyMetadata((object) false, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzE0CH9XMtyZ7l = DependencyProperty.Register(nameof (LabelColor), typeof (Color), typeof (StackedColumnRenderableSeries), new PropertyMetadata((object) Colors.White));
  
  public static readonly DependencyProperty \u0023\u003DzDFBNGGYX51P8 = DependencyProperty.Register(nameof (LabelFontSize), typeof (float), typeof (StackedColumnRenderableSeries), new PropertyMetadata((object) 12f));
  
  public static readonly DependencyProperty \u0023\u003DzdeZXVnb310kr = DependencyProperty.Register(nameof (LabelTextFormatting), typeof (string), typeof (StackedColumnRenderableSeries), new PropertyMetadata((object) "0.00"));

  public StackedColumnRenderableSeries()
  {
    this.DefaultStyleKey = (object) typeof (StackedColumnRenderableSeries);
  }

  public string StackedGroupId
  {
    get
    {
      return (string) this.GetValue(StackedColumnRenderableSeries.\u0023\u003Dz2Rta\u0024oTnlQkx);
    }
    set
    {
      this.SetValue(StackedColumnRenderableSeries.\u0023\u003Dz2Rta\u0024oTnlQkx, (object) value);
    }
  }

  public bool IsOneHundredPercent
  {
    get
    {
      return (bool) this.GetValue(StackedColumnRenderableSeries.\u0023\u003Dzj\u0024ZDuobq5\u0024GmruUxggpnzio\u003D);
    }
    set
    {
      this.SetValue(StackedColumnRenderableSeries.\u0023\u003Dzj\u0024ZDuobq5\u0024GmruUxggpnzio\u003D, (object) value);
      this.\u0023\u003DzoXzc48\u0024TAMxP()?.InvalidateElement();
    }
  }

  public double Spacing
  {
    get
    {
      return (double) this.GetValue(StackedColumnRenderableSeries.\u0023\u003DzYrTUOus\u003D);
    }
    set
    {
      this.SetValue(StackedColumnRenderableSeries.\u0023\u003DzYrTUOus\u003D, (object) value);
    }
  }

  public \u0023\u003DzUJpBz2W8IzAtBIqVtQXHBxp6vbyZAM3jtrSiCk74BV066G8ZjcZkoR4\u003D SpacingMode
  {
    get
    {
      return (\u0023\u003DzUJpBz2W8IzAtBIqVtQXHBxp6vbyZAM3jtrSiCk74BV066G8ZjcZkoR4\u003D) this.GetValue(StackedColumnRenderableSeries.\u0023\u003DzDmC1x3LgSH85);
    }
    set
    {
      this.SetValue(StackedColumnRenderableSeries.\u0023\u003DzDmC1x3LgSH85, (object) value);
    }
  }

  public bool ShowLabel
  {
    get
    {
      return (bool) this.GetValue(StackedColumnRenderableSeries.\u0023\u003Dzj9GlhPUqVKhn);
    }
    set
    {
      this.SetValue(StackedColumnRenderableSeries.\u0023\u003Dzj9GlhPUqVKhn, (object) value);
    }
  }

  public Color LabelColor
  {
    get
    {
      return (Color) this.GetValue(StackedColumnRenderableSeries.\u0023\u003DzE0CH9XMtyZ7l);
    }
    set
    {
      this.SetValue(StackedColumnRenderableSeries.\u0023\u003DzE0CH9XMtyZ7l, (object) value);
    }
  }

  public float LabelFontSize
  {
    get
    {
      return (float) this.GetValue(StackedColumnRenderableSeries.\u0023\u003DzDFBNGGYX51P8);
    }
    set
    {
      this.SetValue(StackedColumnRenderableSeries.\u0023\u003DzDFBNGGYX51P8, (object) value);
    }
  }

  public string LabelTextFormatting
  {
    get
    {
      return (string) this.GetValue(StackedColumnRenderableSeries.\u0023\u003DzdeZXVnb310kr);
    }
    set
    {
      this.SetValue(StackedColumnRenderableSeries.\u0023\u003DzdeZXVnb310kr, (object) value);
    }
  }

  bool \u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D.\u0023\u003DzzSV9ePAnJ860K\u0024gH7z\u0024ORqYfI356QjmFD8xknWuHXzXo1YjYsnKgk49nen5O5ViB\u0024mBf8vWMfIFRnM2e\u0024g\u003D\u003D()
  {
    return this.\u0023\u003Dz_2ANtA3ZTojx\u00243R38A\u003D\u003D();
  }

  [SpecialName]
  public \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUtzzzd5rSNXl95sF5MghysRDMZyklVKg61SC2QL8 \u0023\u003Dz9NrWMa9\u00243uT8()
  {
    return ((SciChartSurface) this.\u0023\u003DzoXzc48\u0024TAMxP())?.\u0023\u003DzGqYqkF73Z9yr0zUWMg\u003D\u003D();
  }

  public override IRange \u0023\u003Dzq3MgExWxza1L()
  {
    bool flag = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D() != null && this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D().\u0023\u003Dzvv0hxm6toXaEx\u0024Ig9\u0024tNoOTFJF\u0024GzZcDvmTfkJY\u003D();
    return this.\u0023\u003Dz9NrWMa9\u00243uT8().\u0023\u003Dzq3MgExWxza1L(flag);
  }

  public override IRange \u0023\u003DzxNQHuqrEvxH2(
    IRange _param1,
    bool _param2)
  {
    IndexRange  g8Oq2rGx6KyfAreq = _param1 != null ? this.DataSeries.GetIndicesRange(_param1) : throw new ArgumentNullException("xRange");
    return (IRange) this.\u0023\u003Dz9NrWMa9\u00243uT8().\u0023\u003DzzMId\u0024f67Wftb((\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D) this, g8Oq2rGx6KyfAreq);
  }

  protected override void \u0023\u003Dz_mrkCOu7iZTY(
    IRenderContext2D _param1,
    IRenderPassData _param2)
  {
    this.\u0023\u003Dz9NrWMa9\u00243uT8().\u0023\u003DzNi0XCnZpx1ge(_param1);
  }

  public double \u0023\u003DzNfVFwxaLW3jC()
  {
    return this.\u0023\u003DzNfVFwxaLW3jC(this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D());
  }

  protected override double \u0023\u003DzPADldLd\u0024JydfjzvZWw\u003D\u003D(
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param1)
  {
    Tuple<double, double> tuple = this.\u0023\u003Dz9NrWMa9\u00243uT8().\u0023\u003DzEAAzcP_HlZ8b((\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D) this, _param1.\u0023\u003DzSkvCFWUKQ7Fw());
    return Math.Min(tuple.Item1, tuple.Item2);
  }

  protected override double \u0023\u003DzWRZyMoPrv0mW7TClKA\u003D\u003D(
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param1)
  {
    Tuple<double, double> tuple = this.\u0023\u003Dz9NrWMa9\u00243uT8().\u0023\u003DzEAAzcP_HlZ8b((\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D) this, _param1.\u0023\u003DzSkvCFWUKQ7Fw());
    return Math.Max(tuple.Item1, tuple.Item2);
  }

  protected override double \u0023\u003DzcaynwI5AMDdY(
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param1)
  {
    return this.\u0023\u003Dz9NrWMa9\u00243uT8().\u0023\u003DzcaynwI5AMDdY((\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D) this, _param1.\u0023\u003DzSkvCFWUKQ7Fw());
  }

  protected override \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D \u0023\u003Dz__R3\u0024ryThR5H(
    Point _param1,
    double _param2,
    bool _param3)
  {
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D zldchDrVsrVyHh6WyiGy1 = \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D.\u0023\u003Dzz_6Dy9M\u003D;
    if (this.IsVisible)
    {
      \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D zldchDrVsrVyHh6WyiGy2 = this.\u0023\u003Dzr7PRxQcLL3EF(_param1, this.\u0023\u003Dz1runmyhnjbZYf6YRbnCukUGsf9D0YvUs2A\u003D\u003D(_param2), (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 1, false);
      zldchDrVsrVyHh6WyiGy1 = this.\u0023\u003Dz9NrWMa9\u00243uT8().\u0023\u003Dznnv4eJBaYYey(_param1, zldchDrVsrVyHh6WyiGy2, _param2, (\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D) this);
    }
    return zldchDrVsrVyHh6WyiGy1;
  }

  private static void \u0023\u003DzyGQyqAS53k26(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is StackedColumnRenderableSeries d5Uc36Ku2HzS32Ejd) || d5Uc36Ku2HzS32Ejd.\u0023\u003Dz9NrWMa9\u00243uT8() == null)
      return;
    d5Uc36Ku2HzS32Ejd.\u0023\u003Dz9NrWMa9\u00243uT8().\u0023\u003DzZcYnC1z3z2MT((\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D) d5Uc36Ku2HzS32Ejd, (string) _param1.OldValue, (string) _param1.NewValue);
  }

  Style IRenderableSeries.\u0023\u003DzUJpBz2W8IzAtBIqVtQXHBxp6vbyZAM3jtrSiCk74BV1DuKLm1V1cPvuYOQQ3vfm3CE6f2xA\u003D()
  {
    return this.Style;
  }

  void IRenderableSeries.\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEewBN21r93mO2zsz2dRPeFYWJbEJRkUuJj8DgmKkp4Eq5v4nmubs\u003D(
    Style _param1)
  {
    this.Style = _param1;
  }

  object IRenderableSeries.\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEewBN21r93mO2zsz2dRPeFYUUfzfIMBKYp9sEsF_gP\u0024Pd_00evAM\u003D()
  {
    return this.DataContext;
  }

  void IRenderableSeries.\u0023\u003DzupHrUO0UFO07vWyNRguf_0VZ\u0024qSpKkKiNQBNd6W0uxIwj8NLZZkxSgEI5esBhFlrGGFudoE\u003D(
    object _param1)
  {
    this.DataContext = _param1;
  }

  double IDrawable.\u0023\u003DzEa5ACpOap4rFIaHj5p9yfH70ARbSZe0FxQ0q\u00240QfMpnPN_04zQ\u003D\u003D()
  {
    return this.Width;
  }

  void IDrawable.\u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARI4gQW2p5XnENj22E0ug7VJ0RyC3hMw\u003D\u003D(
    double _param1)
  {
    this.Width = _param1;
  }

  double IDrawable.\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ZE9YOd5sMhl\u0024Z\u0024xSADAZlqXzWzlvA\u003D\u003D()
  {
    return this.Height;
  }

  void IDrawable.\u0023\u003Dzi_t7eeX4F5JXHEvvNMYntaAE_X2h6PHbsMnRBK9cYE8yLrOBvg\u003D\u003D(
    double _param1)
  {
    this.Height = _param1;
  }
}
