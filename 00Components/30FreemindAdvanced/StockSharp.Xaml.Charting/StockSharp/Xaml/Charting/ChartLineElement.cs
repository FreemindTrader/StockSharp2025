// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartLineElement
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using Ecng.Collections;
using Ecng.Common;
using Ecng.Drawing;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Charting;
using StockSharp.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

#nullable disable
namespace StockSharp.Xaml.Charting;

/// <summary>The chart element representing a line.</summary>
public class ChartLineElement : 
  ChartElement<ChartLineElement>,
  IDrawableChartElement,
  IfxChartElement,
  IChartElement,
  IChartPart<IChartElement>,
  INotifyPropertyChanging,
  INotifyPropertyChanged,
  IPersistable,
  IChartLineElement
{
  
  private System.Windows.Media.Color \u0023\u003Dzfzo3Zt0\u003D = Colors.DarkBlue;
  
  private System.Windows.Media.Color \u0023\u003DzqIZHmBRa0Zhe;
  
  private int \u0023\u003Dz9g4LKqGb_N_KCf\u0024R6Q\u003D\u003D = 1;
  
  private bool \u0023\u003DzCGVfeT7yJc5e = true;
  
  private DrawStyles \u0023\u003DzOoq7N0E\u003D;
  
  private bool \u0023\u003Dzvu7bxO54zKRR;
  
  private ControlTemplate \u0023\u003DzR0PVnrfRD9\u0024B;
  
  private UIBaseVM \u0023\u003Dz2YSX_Z4\u003D;

  /// <summary>
  /// Initializes a new instance of the <see cref="T:StockSharp.Xaml.Charting.ChartLineElement" />.
  /// </summary>
  public ChartLineElement() => this.AdditionalColor = this.Color.ToTransparent((byte) 50);

  /// <summary>
  /// Line color (candles, etc.), with which it will be drawn on chart.
  /// </summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "Color", Description = "ColorDesc", Order = 30)]
  public System.Windows.Media.Color Color
  {
    get => this.\u0023\u003Dzfzo3Zt0\u003D;
    set
    {
      if (this.\u0023\u003Dzfzo3Zt0\u003D == value)
        return;
      this.\u0023\u003Dzfzo3Zt0\u003D = value;
      this.DrawTemplate = (ControlTemplate) null;
      this.RaisePropertyChanged("");
    }
  }

  /// <summary>
  /// Additional line color (candles, etc.), with which it will be drawn on the chart.
  /// </summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "AdditionalColor", Description = "AdditionalColorDesc", Order = 40)]
  public System.Windows.Media.Color AdditionalColor
  {
    get => this.\u0023\u003DzqIZHmBRa0Zhe;
    set
    {
      this.\u0023\u003DzqIZHmBRa0Zhe = value;
      this.DrawTemplate = (ControlTemplate) null;
      this.RaisePropertyChanged("");
    }
  }

  /// <summary>
  /// The thickness of the line (bar, etc.) with which it will be drawn on the chart. The default is 1.
  /// </summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "LineWidth", Description = "LineWidthDesc", Order = 50)]
  public int StrokeThickness
  {
    get => this.\u0023\u003Dz9g4LKqGb_N_KCf\u0024R6Q\u003D\u003D;
    set
    {
      if (this.\u0023\u003Dz9g4LKqGb_N_KCf\u0024R6Q\u003D\u003D == value)
        return;
      this.\u0023\u003Dz9g4LKqGb_N_KCf\u0024R6Q\u003D\u003D = value >= 1 ? value : throw new ArgumentOutOfRangeException("", (object) value, LocalizedStrings.InvalidValue);
      this.DrawTemplate = (ControlTemplate) null;
      this.RaisePropertyChanged("");
    }
  }

  /// <summary>
  /// The anti aliasing of the line drawing. The default is enabled.
  /// </summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "AntiAliasing", Description = "LineAntiAliasing", Order = 60)]
  public bool AntiAliasing
  {
    get => this.\u0023\u003DzCGVfeT7yJc5e;
    set
    {
      this.\u0023\u003DzCGVfeT7yJc5e = value;
      this.RaisePropertyChanged("");
    }
  }

  /// <summary>
  /// The line drawing style. The default is <see cref="F:Ecng.Drawing.DrawStyles.Line" />.
  /// </summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "Style", Description = "StyleRender", Order = 70)]
  public DrawStyles Style
  {
    get => this.\u0023\u003DzOoq7N0E\u003D;
    set
    {
      if (this.\u0023\u003DzOoq7N0E\u003D == value)
        return;
      this.RaisePropertyValueChanging("", (object) value);
      this.\u0023\u003DzOoq7N0E\u003D = value;
      this.DrawTemplate = (ControlTemplate) null;
      this.RaisePropertyChanged("");
    }
  }

  /// <summary>Show Y-axis marker.</summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "Marker", Description = "ShowAxisMarker", Order = 80 /*0x50*/)]
  public bool ShowAxisMarker
  {
    get => this.\u0023\u003Dzvu7bxO54zKRR;
    set
    {
      this.\u0023\u003Dzvu7bxO54zKRR = value;
      this.RaisePropertyChanged("");
    }
  }

  /// <summary>
  /// The chart template for the Dot style <see cref="T:Ecng.Drawing.DrawStyles" />.
  /// </summary>
  [Browsable(false)]
  public ControlTemplate DrawTemplate
  {
    get
    {
      return this.\u0023\u003DzR0PVnrfRD9\u0024B ?? (this.\u0023\u003DzR0PVnrfRD9\u0024B = this.\u0023\u003DzC5cjcZf7bzaj());
    }
    set
    {
      this.\u0023\u003DzR0PVnrfRD9\u0024B = value;
      this.RaisePropertyChanged("");
    }
  }

  System.Drawing.Color IChartLineElement.Color
  {
    get => this.Color.FromWpf();
    set => this.Color = value.ToWpf();
  }

  System.Drawing.Color IChartLineElement.AdditionalColor
  {
    get => this.AdditionalColor.FromWpf();
    set => this.AdditionalColor = value.ToWpf();
  }

  private ControlTemplate \u0023\u003DzC5cjcZf7bzaj()
  {
    ControlTemplate controlTemplate = new ControlTemplate();
    controlTemplate.VisualTree = new FrameworkElementFactory(typeof (Ellipse));
    double num = this.Style == DrawStyles.Dot ? (double) this.StrokeThickness : Math.Max(8.0, 2.0 * (double) this.StrokeThickness);
    controlTemplate.VisualTree.SetValue(Shape.FillProperty, (object) new SolidColorBrush(this.Color.ToTransparent((byte) 150)));
    controlTemplate.VisualTree.SetValue(FrameworkElement.WidthProperty, (object) num);
    controlTemplate.VisualTree.SetValue(FrameworkElement.HeightProperty, (object) num);
    controlTemplate.VisualTree.SetValue(Shape.StrokeProperty, (object) new SolidColorBrush(this.AdditionalColor));
    controlTemplate.VisualTree.SetValue(Shape.StrokeThicknessProperty, (object) 2.0);
    return controlTemplate;
  }

  UIBaseVM IDrawableChartElement.\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy2NIVeJ\u0024WEKCPOgxige9iqo_yKcrMQ\u003D\u003D(
    ScichartSurfaceMVVM _param1)
  {
    this.\u0023\u003Dz2YSX_Z4\u003D = _param1.Area.XAxisType == ChartAxisType.Numeric ? (UIBaseVM) new \u0023\u003Dz5CbAZMXp7dgzzBe\u0024G3xhis\u0024LTAR\u0024BJC8orkn13FnKLIfi3A9i\u00246SqEijqyQF<double>(this) : (UIBaseVM) new \u0023\u003Dz5CbAZMXp7dgzzBe\u0024G3xhis\u0024LTAR\u0024BJC8orkn13FnKLIfi3A9i\u00246SqEijqyQF<DateTime>(this);
    return this.\u0023\u003Dz2YSX_Z4\u003D;
  }

  bool IDrawableChartElement.\u0023\u003DzJXDjnZfs8tGoFCupfSBAn4fwfCXfeCPpi\u0024rZmqxbRCtxRCyVSA\u003D\u003D(
    IEnumerableEx<ChartDrawData.IDrawValue> _param1)
  {
    return this.\u0023\u003Dz2YSX_Z4\u003D.Draw(_param1);
  }

  void IDrawableChartElement.\u0023\u003DzolvWmzKCnovSLB\u0024fEd65U8XPmuyOBlZpMiNagFIxa3issk4ACmj9rvI\u003D()
  {
    this.\u0023\u003Dz2YSX_Z4\u003D.Draw(CollectionHelper.ToEx<ChartDrawData.IDrawValue>(Enumerable.Empty<ChartDrawData.IDrawValue>(), 0));
  }

  /// <inheritdoc />
  protected override bool OnDraw(ChartDrawData data)
  {
    IEnumerableEx<ChartDrawData.IDrawValue> ienumerableEx = data.GetCandleRelatedData((IChartLineElement) this);
    return ienumerableEx != null && !CollectionHelper.IsEmpty<ChartDrawData.IDrawValue>((IEnumerable<ChartDrawData.IDrawValue>) ienumerableEx) && ((IDrawableChartElement) this).StartDrawing(ienumerableEx);
  }

  /// <summary>Load settings.</summary>
  /// <param name="storage">Settings storage.</param>
  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    this.Color = XamlHelper.ToColor(storage.GetValue<int>("", this.Color.ToInt()));
    this.AdditionalColor = XamlHelper.ToColor(storage.GetValue<int>("", this.AdditionalColor.ToInt()));
    this.StrokeThickness = storage.GetValue<int>("", this.StrokeThickness);
    this.AntiAliasing = storage.GetValue<bool>("", this.AntiAliasing);
    this.Style = storage.GetValue<DrawStyles>("", this.Style);
    this.ShowAxisMarker = storage.GetValue<bool>("", this.ShowAxisMarker);
  }

  /// <summary>Save settings.</summary>
  /// <param name="storage">Settings storage.</param>
  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<int>("", this.Color.ToInt());
    storage.SetValue<int>("", this.AdditionalColor.ToInt());
    storage.SetValue<int>("", this.StrokeThickness);
    storage.SetValue<bool>("", this.AntiAliasing);
    storage.SetValue<string>("", Converter.To<string>((object) this.Style));
    storage.SetValue<bool>("", this.ShowAxisMarker);
  }

  internal override ChartLineElement Clone(ChartLineElement _param1)
  {
    _param1 = base.Clone(_param1);
    _param1.Color = this.Color;
    _param1.AdditionalColor = this.AdditionalColor;
    _param1.StrokeThickness = this.StrokeThickness;
    _param1.AntiAliasing = this.AntiAliasing;
    _param1.Style = this.Style;
    _param1.ShowAxisMarker = this.ShowAxisMarker;
    return _param1;
  }

  /// <inheritdoc />
  public override bool CheckAxesCompatible(ChartAxisType? xType, ChartAxisType? yType)
  {
    return !yType.HasValue || yType.GetValueOrDefault() == ChartAxisType.Numeric;
  }
}
