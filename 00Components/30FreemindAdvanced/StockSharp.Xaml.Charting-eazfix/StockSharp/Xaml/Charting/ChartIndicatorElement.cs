// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartIndicatorElement
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Common;
using Ecng.Drawing;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Xaml.Charting.IndicatorPainters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;

#nullable disable
namespace StockSharp.Xaml.Charting;

[Display(ResourceType = typeof (LocalizedStrings), Name = "IndicatorSettings")]
public sealed class ChartIndicatorElement : 
  ChartElement<ChartIndicatorElement>,
  IChartElement,
  IChartPart<IChartElement>,
  INotifyPropertyChanged,
  INotifyPropertyChanging,
  IPersistable,
  IChartIndicatorElement
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private DefaultPainter \u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private IChartIndicatorPainter \u0023\u003DzggYpSp2p8YZic9F7tQ\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003Dzi6QomveZ37NV\u0024lKbXjNgtwc\u003D;

  public ChartIndicatorElement()
  {
    this.\u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D = new DefaultPainter();
    this.\u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D.OnAttached((IChartIndicatorElement) this);
  }

  public virtual string ToString() => this.FullTitle;

  public bool AutoAssignYAxis
  {
    get => this.\u0023\u003Dzi6QomveZ37NV\u0024lKbXjNgtwc\u003D;
    set => this.\u0023\u003Dzi6QomveZ37NV\u0024lKbXjNgtwc\u003D = value;
  }

  public IChartIndicatorPainter IndicatorPainter
  {
    get
    {
      return this.\u0023\u003DzggYpSp2p8YZic9F7tQ\u003D\u003D ?? (IChartIndicatorPainter) this.\u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D;
    }
    set
    {
      if (this.\u0023\u003DzggYpSp2p8YZic9F7tQ\u003D\u003D == value)
        return;
      StockSharp.Xaml.Charting.ChartArea chartArea = (StockSharp.Xaml.Charting.ChartArea) this.ChartArea;
      chartArea?.ChartSurfaceViewModel.\u0023\u003DzmxDTmQc\u003D((IChartElement) this.IndicatorPainter.Element);
      this.IndicatorPainter.OnDetached();
      if (value?.GetType() != typeof (DefaultPainter))
      {
        this.\u0023\u003DzggYpSp2p8YZic9F7tQ\u003D\u003D = value;
      }
      else
      {
        this.\u0023\u003DzggYpSp2p8YZic9F7tQ\u003D\u003D = (IChartIndicatorPainter) null;
        this.\u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D = (DefaultPainter) value;
      }
      this.IndicatorPainter.OnAttached((IChartIndicatorElement) this);
      chartArea?.ChartSurfaceViewModel.\u0023\u003Dz4M_pW8k\u003D((IChartElement) this);
      this.RaisePropertyChanged(nameof (IndicatorPainter));
    }
  }

  [Browsable(false)]
  public System.Windows.Media.Color Color
  {
    get => this.\u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D.Line.Color.ToWpf();
    set => this.\u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D.Line.Color = value.FromWpf();
  }

  [Browsable(false)]
  public System.Windows.Media.Color AdditionalColor
  {
    get => this.\u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D.Line.AdditionalColor.ToWpf();
    set => this.\u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D.Line.AdditionalColor = value.FromWpf();
  }

  [Browsable(false)]
  public int StrokeThickness
  {
    get => this.\u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D.Line.StrokeThickness;
    set => this.\u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D.Line.StrokeThickness = value;
  }

  [Browsable(false)]
  public bool AntiAliasing
  {
    get => this.\u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D.Line.AntiAliasing;
    set => this.\u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D.Line.AntiAliasing = value;
  }

  [Browsable(false)]
  public DrawStyles DrawStyle
  {
    get => this.\u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D.Line.Style;
    set => this.\u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D.Line.Style = value;
  }

  [Browsable(false)]
  public bool ShowAxisMarker
  {
    get => this.\u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D.Line.ShowAxisMarker;
    set => this.\u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D.Line.ShowAxisMarker = value;
  }

  [Browsable(false)]
  public ControlTemplate DrawTemplate
  {
    get => ((ChartLineElement) this.\u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D.Line).DrawTemplate;
    set
    {
      ((ChartLineElement) this.\u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D.Line).DrawTemplate = value;
    }
  }

  System.Drawing.Color IChartIndicatorElement.Color
  {
    get => this.Color.FromWpf();
    set => this.Color = value.ToWpf();
  }

  System.Drawing.Color IChartIndicatorElement.AdditionalColor
  {
    get => this.AdditionalColor.FromWpf();
    set => this.AdditionalColor = value.ToWpf();
  }

  protected override bool OnDraw(ChartDrawData data)
  {
    return this.IndicatorPainter.Draw((IChartDrawData) data);
  }

  protected override void OnReset()
  {
    base.OnReset();
    this.IndicatorPainter?.Reset();
  }

  internal void \u0023\u003Dz2Afk71t1OoFdU8tQ4Q\u003D\u003D(
    IList<IndicatorType> _param1,
    IIndicator _param2)
  {
    ChartIndicatorElement.\u0023\u003DzcRH2j3Fkx\u0024jbaxmeEsfjsbo\u003D fkxJbaxmeEsfjsbo = new ChartIndicatorElement.\u0023\u003DzcRH2j3Fkx\u0024jbaxmeEsfjsbo\u003D();
    fkxJbaxmeEsfjsbo.\u0023\u003DzbpCudS_inBmWSVepSA\u003D\u003D = _param2;
    if (this.\u0023\u003DzggYpSp2p8YZic9F7tQ\u003D\u003D != null || _param1 == null || _param1.Count <= 0)
      return;
    IndicatorType type = _param1.FirstOrDefault<IndicatorType>(new Func<IndicatorType, bool>(fkxJbaxmeEsfjsbo.\u0023\u003DzcEPYpK4IBKRf\u0024fIcZDMjj5fzLW8H));
    IChartIndicatorPainter painter = type != null ? type.CreatePainter() : (IChartIndicatorPainter) null;
    if (!(painter?.GetType() != typeof (DefaultPainter)))
      return;
    this.IndicatorPainter = painter;
  }

  protected override ChartIndicatorElement CreateClone()
  {
    ChartIndicatorElement clone = base.CreateClone();
    clone.IndicatorPainter = PersistableHelper.Clone<IChartIndicatorPainter>(this.IndicatorPainter);
    return clone;
  }

  protected override string GetGeneratedTitle() => this.TryGetIndicator()?.ToString();

  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    SettingsStorage settingsStorage1 = storage.GetValue<SettingsStorage>("IndicatorPainter", (SettingsStorage) null);
    if (settingsStorage1 == null)
      return;
    Type type = Type.GetType(settingsStorage1.GetValue<string>("type", (string) null), false);
    if (type == (Type) null)
      return;
    this.IndicatorPainter.OnDetached();
    IChartIndicatorPainter instance = TypeHelper.CreateInstance<IChartIndicatorPainter>(type, Array.Empty<object>());
    if (instance.GetType() == typeof (DefaultPainter))
    {
      this.\u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D = (DefaultPainter) instance;
      this.\u0023\u003DzggYpSp2p8YZic9F7tQ\u003D\u003D = (IChartIndicatorPainter) null;
    }
    else
      this.\u0023\u003DzggYpSp2p8YZic9F7tQ\u003D\u003D = instance;
    SettingsStorage settingsStorage2 = settingsStorage1.GetValue<SettingsStorage>("settings", (SettingsStorage) null);
    try
    {
      ((IPersistable) instance).Load(settingsStorage2);
    }
    catch
    {
    }
    this.IndicatorPainter.OnAttached((IChartIndicatorElement) this);
  }

  public override void Save(SettingsStorage storage)
  {
    storage.SetValue<SettingsStorage>("IndicatorPainter", PersistableHelper.SaveEntire((IPersistable) this.IndicatorPainter, false));
    base.Save(storage);
  }

  protected override int Priority => 1;

  private sealed class \u0023\u003DzcRH2j3Fkx\u0024jbaxmeEsfjsbo\u003D
  {
    public IIndicator \u0023\u003DzbpCudS_inBmWSVepSA\u003D\u003D;

    internal bool \u0023\u003DzcEPYpK4IBKRf\u0024fIcZDMjj5fzLW8H(IndicatorType _param1)
    {
      return _param1.Indicator == this.\u0023\u003DzbpCudS_inBmWSVepSA\u003D\u003D.GetType();
    }
  }
}
