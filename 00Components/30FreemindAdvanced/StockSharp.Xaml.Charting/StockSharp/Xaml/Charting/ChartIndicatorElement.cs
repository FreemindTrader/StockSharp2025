// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartIndicatorElement
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

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

/// <summary>The chart element representing the indicator.</summary>
[Display(ResourceType = typeof (LocalizedStrings), Name = "IndicatorSettings")]
public sealed class ChartIndicatorElement : 
  ChartElement<ChartIndicatorElement>,
  IChartIndicatorElement,
  IChartElement,
  IChartPart<IChartElement>,
  INotifyPropertyChanging,
  INotifyPropertyChanged,
  IPersistable
{
  
  private DefaultPainter \u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D;
  
  private IChartIndicatorPainter \u0023\u003DzggYpSp2p8YZic9F7tQ\u003D\u003D;
  
  private bool \u0023\u003Dzi6QomveZ37NV\u0024lKbXjNgtwc\u003D;

  /// <summary>
  /// Initializes a new instance of the <see cref="T:StockSharp.Xaml.Charting.ChartIndicatorElement" />.
  /// </summary>
  public ChartIndicatorElement()
  {
    this.\u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D = new DefaultPainter();
    this.\u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D.OnAttached((IChartIndicatorElement) this);
  }

  /// <inheritdoc />
  public virtual string ToString() => this.FullTitle;

  /// <inheritdoc />
  public bool AutoAssignYAxis
  {
    get => this.\u0023\u003Dzi6QomveZ37NV\u0024lKbXjNgtwc\u003D;
    set => this.\u0023\u003Dzi6QomveZ37NV\u0024lKbXjNgtwc\u003D = value;
  }

  /// <inheritdoc />
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
      chartArea?.\u0023\u003Dz3ThQNm3rQ1fp().\u0023\u003DzmxDTmQc\u003D((IChartElement) this.IndicatorPainter.Element);
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
      chartArea?.\u0023\u003Dz3ThQNm3rQ1fp().\u0023\u003Dz4M_pW8k\u003D((IChartElement) this);
      this.RaisePropertyChanged(XXX.SSS(-539428532));
    }
  }

  /// <summary>Compatibility property for <see cref="P:StockSharp.Xaml.Charting.ChartLineElement.Color" /> for <see cref="T:StockSharp.Xaml.Charting.IndicatorPainters.DefaultPainter" />.</summary>
  [Browsable(false)]
  public System.Windows.Media.Color Color
  {
    get => this.\u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D.Line.Color.ToWpf();
    set => this.\u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D.Line.Color = value.FromWpf();
  }

  /// <summary>Compatibility property for <see cref="P:StockSharp.Xaml.Charting.ChartLineElement.AdditionalColor" /> for <see cref="T:StockSharp.Xaml.Charting.IndicatorPainters.DefaultPainter" />.</summary>
  [Browsable(false)]
  public System.Windows.Media.Color AdditionalColor
  {
    get => this.\u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D.Line.AdditionalColor.ToWpf();
    set => this.\u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D.Line.AdditionalColor = value.FromWpf();
  }

  /// <inheritdoc />
  [Browsable(false)]
  public int StrokeThickness
  {
    get => this.\u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D.Line.StrokeThickness;
    set => this.\u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D.Line.StrokeThickness = value;
  }

  /// <inheritdoc />
  [Browsable(false)]
  public bool AntiAliasing
  {
    get => this.\u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D.Line.AntiAliasing;
    set => this.\u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D.Line.AntiAliasing = value;
  }

  /// <inheritdoc />
  [Browsable(false)]
  public DrawStyles DrawStyle
  {
    get => this.\u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D.Line.Style;
    set => this.\u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D.Line.Style = value;
  }

  /// <inheritdoc />
  [Browsable(false)]
  public bool ShowAxisMarker
  {
    get => this.\u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D.Line.ShowAxisMarker;
    set => this.\u0023\u003DzwZUN8k4QxNb8DrQ1IQ\u003D\u003D.Line.ShowAxisMarker = value;
  }

  /// <summary>Compatibility property for <see cref="P:StockSharp.Xaml.Charting.ChartLineElement.DrawTemplate" /> for <see cref="T:StockSharp.Xaml.Charting.IndicatorPainters.DefaultPainter" />.</summary>
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

  /// <inheritdoc />
  protected override bool OnDraw(ChartDrawData data)
  {
    return this.IndicatorPainter.Draw((IChartDrawData) data);
  }

  /// <inheritdoc />
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

  /// <inheritdoc />
  protected override ChartIndicatorElement CreateClone()
  {
    ChartIndicatorElement clone = base.CreateClone();
    clone.IndicatorPainter = PersistableHelper.Clone<IChartIndicatorPainter>(this.IndicatorPainter);
    return clone;
  }

  /// <inheritdoc />
  protected override string GetGeneratedTitle() => this.TryGetIndicator()?.ToString();

  /// <inheritdoc />
  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    SettingsStorage settingsStorage1 = storage.GetValue<SettingsStorage>(XXX.SSS(-539428532), (SettingsStorage) null);
    if (settingsStorage1 == null)
      return;
    Type type = Type.GetType(settingsStorage1.GetValue<string>(XXX.SSS(-539428523), (string) null), false);
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
    SettingsStorage settingsStorage2 = settingsStorage1.GetValue<SettingsStorage>(XXX.SSS(-539428562), (SettingsStorage) null);
    try
    {
      ((IPersistable) instance).Load(settingsStorage2);
    }
    catch
    {
    }
    this.IndicatorPainter.OnAttached((IChartIndicatorElement) this);
  }

  /// <inheritdoc />
  public override void Save(SettingsStorage storage)
  {
    storage.SetValue<SettingsStorage>(XXX.SSS(-539428532), PersistableHelper.SaveEntire((IPersistable) this.IndicatorPainter, false));
    base.Save(storage);
  }

  /// <inheritdoc />
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
