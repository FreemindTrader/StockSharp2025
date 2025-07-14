// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartLineElement
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

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

public class ChartLineElement : 
  ChartComponent<ChartLineElement>,
  IChartElement,
  IChartPart<IChartElement>,
  INotifyPropertyChanged,
  INotifyPropertyChanging,
  IPersistable,
  IChartLineElement,
  IChartComponent,
  IDrawableChartElement
{
  
  private System.Windows.Media.Color \u0023\u003Dzfzo3Zt0\u003D = Colors.DarkBlue;
  
  private System.Windows.Media.Color \u0023\u003DzqIZHmBRa0Zhe;
  
  private int \u0023\u003Dz9g4LKqGb_N_KCf\u0024R6Q\u003D\u003D = 1;
  
  private bool \u0023\u003DzCGVfeT7yJc5e = true;
  
  private DrawStyles _drawStyle;
  
  private bool \u0023\u003Dzvu7bxO54zKRR;
  
  private ControlTemplate \u0023\u003DzR0PVnrfRD9\u0024B;
  
  private DrawableChartElementBaseViewModel _baseViewModel;

  public ChartLineElement() => this.AdditionalColor = this.Color.ToTransparent((byte) 50);

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
      this.RaisePropertyChanged(nameof (Color));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "AdditionalColor", Description = "AdditionalColorDesc", Order = 40)]
  public System.Windows.Media.Color AdditionalColor
  {
    get => this.\u0023\u003DzqIZHmBRa0Zhe;
    set
    {
      this.\u0023\u003DzqIZHmBRa0Zhe = value;
      this.DrawTemplate = (ControlTemplate) null;
      this.RaisePropertyChanged(nameof (AdditionalColor));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "LineWidth", Description = "LineWidthDesc", Order = 50)]
  public int StrokeThickness
  {
    get => this.\u0023\u003Dz9g4LKqGb_N_KCf\u0024R6Q\u003D\u003D;
    set
    {
      if (this.\u0023\u003Dz9g4LKqGb_N_KCf\u0024R6Q\u003D\u003D == value)
        return;
      this.\u0023\u003Dz9g4LKqGb_N_KCf\u0024R6Q\u003D\u003D = value >= 1 ? value : throw new ArgumentOutOfRangeException(nameof (value), (object) value, LocalizedStrings.InvalidValue);
      this.DrawTemplate = (ControlTemplate) null;
      this.RaisePropertyChanged(nameof (StrokeThickness));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "AntiAliasing", Description = "LineAntiAliasing", Order = 60)]
  public bool AntiAliasing
  {
    get => this.\u0023\u003DzCGVfeT7yJc5e;
    set
    {
      this.\u0023\u003DzCGVfeT7yJc5e = value;
      this.RaisePropertyChanged(nameof (AntiAliasing));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "Style", Description = "StyleRender", Order = 70)]
  public DrawStyles Style
  {
    get => this._drawStyle;
    set
    {
      if (this._drawStyle == value)
        return;
      this.RaisePropertyValueChanging(nameof (Style), (object) value);
      this._drawStyle = value;
      this.DrawTemplate = (ControlTemplate) null;
      this.RaisePropertyChanged(nameof (Style));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "Marker", Description = "ShowAxisMarker", Order = 80 /*0x50*/)]
  public bool ShowAxisMarker
  {
    get => this.\u0023\u003Dzvu7bxO54zKRR;
    set
    {
      this.\u0023\u003Dzvu7bxO54zKRR = value;
      this.RaisePropertyChanged(nameof (ShowAxisMarker));
    }
  }

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
      this.RaisePropertyChanged(nameof (DrawTemplate));
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

  DrawableChartElementBaseViewModel IDrawableChartElement.CreateViewModel(
    ScichartSurfaceMVVM _param1)
  {
    this._baseViewModel = _param1.Area.XAxisType == ChartAxisType.Numeric ? (DrawableChartElementBaseViewModel) new \u0023\u003Dz5CbAZMXp7dgzzBe\u0024G3xhis\u0024LTAR\u0024BJC8orkn13FnKLIfi3A9i\u00246SqEijqyQF<double>(this) : (DrawableChartElementBaseViewModel) new \u0023\u003Dz5CbAZMXp7dgzzBe\u0024G3xhis\u0024LTAR\u0024BJC8orkn13FnKLIfi3A9i\u00246SqEijqyQF<DateTime>(this);
    return this._baseViewModel;
  }

  bool IDrawableChartElement.StartDrawing(
    IEnumerableEx<ChartDrawData.IDrawValue> _param1)
  {
    return this._baseViewModel.Draw(_param1);
  }

  void IDrawableChartElement.StartDrawing()
  {
    this._baseViewModel.Draw(CollectionHelper.ToEx<ChartDrawData.IDrawValue>(Enumerable.Empty<ChartDrawData.IDrawValue>(), 0));
  }

  protected override bool OnDraw(ChartDrawData data)
  {
    IEnumerableEx<ChartDrawData.IDrawValue> ienumerableEx = data.GetActiveOrders((IChartLineElement) this);
    return ienumerableEx != null && !CollectionHelper.IsEmpty<ChartDrawData.IDrawValue>((IEnumerable<ChartDrawData.IDrawValue>) ienumerableEx) && ((IDrawableChartElement) this).StartDrawing(ienumerableEx);
  }

  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    this.Color = XamlHelper.ToColor(storage.GetValue<int>("Color", this.Color.ToInt()));
    this.AdditionalColor = XamlHelper.ToColor(storage.GetValue<int>("AdditionalColor", this.AdditionalColor.ToInt()));
    this.StrokeThickness = storage.GetValue<int>("StrokeThickness", this.StrokeThickness);
    this.AntiAliasing = storage.GetValue<bool>("AntiAliasing", this.AntiAliasing);
    this.Style = storage.GetValue<DrawStyles>("Style", this.Style);
    this.ShowAxisMarker = storage.GetValue<bool>("ShowAxisMarker", this.ShowAxisMarker);
  }

  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<int>("Color", this.Color.ToInt());
    storage.SetValue<int>("AdditionalColor", this.AdditionalColor.ToInt());
    storage.SetValue<int>("StrokeThickness", this.StrokeThickness);
    storage.SetValue<bool>("AntiAliasing", this.AntiAliasing);
    storage.SetValue<string>("Style", Converter.To<string>((object) this.Style));
    storage.SetValue<bool>("ShowAxisMarker", this.ShowAxisMarker);
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

  public override bool CheckAxesCompatible(ChartAxisType? xType, ChartAxisType? yType)
  {
    return !yType.HasValue || yType.GetValueOrDefault() == ChartAxisType.Numeric;
  }
}
