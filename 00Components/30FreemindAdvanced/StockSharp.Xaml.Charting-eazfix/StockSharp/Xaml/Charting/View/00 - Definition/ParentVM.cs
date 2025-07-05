// Decompiled with JetBrains decompiler
// Type: #=zfuNSIBalvsZFtWGR3evczlu8c0hHILDz7oIFnPPdzY2A4VgOP$CeDIqsTdzB
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Collections;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Media;

namespace StockSharp.Xaml.Charting;
#nullable enable
public sealed class ParentVM :  ChartBaseViewModel, IDisposable
{
  
  private readonly 
  #nullable disable
  ObservableCollection<ChildVM> _childViewModels = new ObservableCollection<ChildVM>();
  
  private readonly List<UIChartBaseViewModel> _childElements = new List<UIChartBaseViewModel>();
  
  private readonly ScichartSurfaceMVVM _scichartSurfaceVM;
  
  private readonly IChartComponent _chartElement;
  
  private readonly bool \u0023\u003DzltAOnz7pENYNz4RZiZiGFJ7j3MDa;
  
  private CandlestickUI \u0023\u003Dz\u0024j34drxVvrdumV_HyGybhvc\u003D;
  
  private bool \u0023\u003DzeOVf5rToxe0_\u0024sT_tw\u003D\u003D;

  public ParentVM( ScichartSurfaceMVVM _param1, IChartComponent _param2 )
    {
    this._scichartSurfaceVM; = _param1 ?? throw new ArgumentNullException("pane");
    this._chartElement = _param2 ?? throw new ArgumentNullException("element");
    this.\u0023\u003DzltAOnz7pENYNz4RZiZiGFJ7j3MDa = _param2 is IChartCandleElement;
    \u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DztwqF4KBjQLI4w4fkq\u0024UNEzaV82mj(new Action(this.\u0023\u003DzJRKin3dIscU4TEH\u0024FkxyZyjDceEY));
    this.ChartElement.PropertyChanged += new PropertyChangedEventHandler(this.\u0023\u003DzqKjZcEftBQYu8FctY__O05c\u003D);
  }

  public ScichartSurfaceMVVM Pane
  {
    get => this._scichartSurfaceVM;
  }

  public bool AllowToRemove => this.Pane.\u0023\u003Dzv_LIRKQ\u003D(this);

  public IChartComponent ChartElement
  {
    get => this._chartElement;
  }

  public string Title => this.ChartElement.GetGeneratedTitle();

  public bool IsCandleElement => this.\u0023\u003DzltAOnz7pENYNz4RZiZiGFJ7j3MDa;

  public CandlestickUI Candles
  {
    get => this.\u0023\u003Dz\u0024j34drxVvrdumV_HyGybhvc\u003D;
    private set => this.\u0023\u003Dz\u0024j34drxVvrdumV_HyGybhvc\u003D = value;
  }

  public IEnumerable<UIChartBaseViewModel> Elements
  {
    get
    {
      return (IEnumerable<UIChartBaseViewModel>) this._childElements;
    }
  }

  public IEnumerable<ChildVM> Values
  {
    get
    {
      return (IEnumerable<ChildVM>) this._childViewModels;
    }
  }

  public Color Color
  {
    get
    {
      return this._childElements.Count == 1 ? this._childElements[0].Element.Color : Colors.Transparent;
    }
  }

  public void \u0023\u003DzkFJdjYoyxP8n(
    IEnumerable<UIChartBaseViewModel> _param1)
  {
    UIChartBaseViewModel[] array = _param1.ToArray<UIChartBaseViewModel>();
    if (CollectionHelper.IsEmpty<UIChartBaseViewModel>(array))
      throw new ArgumentException("zero child elements");
    this._childElements.AddRange((IEnumerable<UIChartBaseViewModel>) array);
    if (this.IsCandleElement && this.Candles == null)
      this.Candles = array.OfType<CandlestickUI>().First<CandlestickUI>();
    if (this._childElements.Count == 1)
      this.MapPropertyChangeNotification((INotifyPropertyChanged) this._childElements[0].Element, "Color", "Color");
    CollectionHelper.ForEach<UIChartBaseViewModel>((IEnumerable<UIChartBaseViewModel>) array, new Action<UIChartBaseViewModel>(this.\u0023\u003Dz7x6SlCXIp8hAJNVeAefGj\u0024I\u003D));
  }

  private void \u0023\u003DzJRKin3dIscU4TEH\u0024FkxyZyjDceEY()
  {
    this.NotifyChanged("AllowToRemove");
  }

  public void \u0023\u003Dzfc4TzKM\u003D(
    ChildVM _param1)
  {
    if (_param1 == null)
      throw new ArgumentNullException("val");
    if (this._childViewModels.Contains(_param1))
      throw new ArgumentException("val");
    _param1.Parent = this;
    this._childViewModels.Add(_param1);
  }

  public void \u0023\u003DzMNK339lzrtSc()
  {
    CollectionHelper.ForEach<ChildVM>((IEnumerable<ChildVM>) this._childViewModels, ParentVM.SomeClass34343383.\u0023\u003Dzxza6M2rQ\u0024\u0024Ygg7l8sg\u003D\u003D ?? (ParentVM.SomeClass34343383.\u0023\u003Dzxza6M2rQ\u0024\u0024Ygg7l8sg\u003D\u003D = new Action<ChildVM>(ParentVM.SomeClass34343383.SomeMethond0343.\u0023\u003Dz\u0024Y0CRLvNGz\u0024QVOp_KD9Goz0\u003D)));
  }

  public bool Draw(ChartDrawData _param1)
  {
    return this.ChartElement.Draw(_param1);
  }

  public void Reset()
  {
    this.ChartElement.Reset();
    this._childElements.ForEach(ParentVM.SomeClass34343383.\u0023\u003DzujJHoXZ3YoJgJW6Elg\u003D\u003D ?? (ParentVM.SomeClass34343383.\u0023\u003DzujJHoXZ3YoJgJW6Elg\u003D\u003D = new Action<UIChartBaseViewModel>(ParentVM.SomeClass34343383.SomeMethond0343.\u0023\u003Dz78guPox9d5EyKS2_DA\u003D\u003D)));
  }

  public void UpdateYAxisMarker()
  {
    this._childElements.ForEach(ParentVM.SomeClass34343383.\u0023\u003Dza6ES3WKQ8eKYsVbcqw\u003D\u003D ?? (ParentVM.SomeClass34343383.\u0023\u003Dza6ES3WKQ8eKYsVbcqw\u003D\u003D = new Action<UIChartBaseViewModel>(ParentVM.SomeClass34343383.SomeMethond0343.\u0023\u003Dzy8xbdOhlUdYc9J3lehtuYeU\u003D)));
  }

  public void PerformPeriodicalAction()
  {
    this._childElements.ForEach(ParentVM.SomeClass34343383.\u0023\u003Dzh2rq1rNuTua1OY6e3Q\u003D\u003D ?? (ParentVM.SomeClass34343383.\u0023\u003Dzh2rq1rNuTua1OY6e3Q\u003D\u003D = new Action<UIChartBaseViewModel>(ParentVM.SomeClass34343383.SomeMethond0343.\u0023\u003Dz8jVsNNt6XUrYulKMVzGRKbI\u003D)));
  }

  public void GuiUpdateAndClear()
  {
    this._childElements.ForEach(ParentVM.SomeClass34343383.\u0023\u003Dzc_JZPYTnozknjOo_1Q\u003D\u003D ?? (ParentVM.SomeClass34343383.\u0023\u003Dzc_JZPYTnozknjOo_1Q\u003D\u003D = new Action<UIChartBaseViewModel>(ParentVM.SomeClass34343383.SomeMethond0343.\u0023\u003DzIFx97lVTaAg3xg6hNg\u003D\u003D)));
  }

  internal Subscription \u0023\u003DzZ0VU1NABDfD8(
    IChartComponent _param1)
  {
    return ((Chart) this.Pane.Chart).TryGetSubscription((IChartElement) _param1);
  }

  public bool IsDisposed
  {
    get => this.\u0023\u003DzeOVf5rToxe0_\u0024sT_tw\u003D\u003D;
    private set => this.\u0023\u003DzeOVf5rToxe0_\u0024sT_tw\u003D\u003D = value;
  }

  public void Dispose()
  {
    \u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzFKIjoTgcr_ZQ\u0024BFi5BCDqDF5zYZT(new Action(this.\u0023\u003DzJRKin3dIscU4TEH\u0024FkxyZyjDceEY));
    this.IsDisposed = true;
  }

  private void \u0023\u003DzqKjZcEftBQYu8FctY__O05c\u003D(
    #nullable enable
    object? _param1,
    PropertyChangedEventArgs _param2)
  {
    if (!(_param2.PropertyName == "FullTitle"))
      return;
    this.NotifyChanged("Title");
  }

  private void \u0023\u003Dz7x6SlCXIp8hAJNVeAefGj\u0024I\u003D(
    #nullable disable
    UIChartBaseViewModel _param1)
  {
    _param1.Init(this);
  }

  [Serializable]
  private new sealed class SomeClass34343383
  {
    public static readonly ParentVM.SomeClass34343383 SomeMethond0343 = new ParentVM.SomeClass34343383();
    public static Action<ChildVM> \u0023\u003Dzxza6M2rQ\u0024\u0024Ygg7l8sg\u003D\u003D;
    public static Action<UIChartBaseViewModel> \u0023\u003DzujJHoXZ3YoJgJW6Elg\u003D\u003D;
    public static Action<UIChartBaseViewModel> \u0023\u003Dza6ES3WKQ8eKYsVbcqw\u003D\u003D;
    public static Action<UIChartBaseViewModel> \u0023\u003Dzh2rq1rNuTua1OY6e3Q\u003D\u003D;
    public static Action<UIChartBaseViewModel> \u0023\u003Dzc_JZPYTnozknjOo_1Q\u003D\u003D;

    internal void \u0023\u003Dz\u0024Y0CRLvNGz\u0024QVOp_KD9Goz0\u003D(
      ChildVM _param1)
    {
      _param1.Value = (string) null;
    }

    internal void \u0023\u003Dz78guPox9d5EyKS2_DA\u003D\u003D(
      UIChartBaseViewModel _param1)
    {
      _param1.Reset();
    }

    internal void \u0023\u003Dzy8xbdOhlUdYc9J3lehtuYeU\u003D(
      UIChartBaseViewModel _param1)
    {
      _param1.UpdateYAxisMarker();
    }

    internal void \u0023\u003Dz8jVsNNt6XUrYulKMVzGRKbI\u003D(
      UIChartBaseViewModel _param1)
    {
      _param1.PerformPeriodicalAction();
    }

    internal void \u0023\u003DzIFx97lVTaAg3xg6hNg\u003D\u003D(
      UIChartBaseViewModel _param1)
    {
      _param1.GuiUpdateAndClear();
    }
  }
}
