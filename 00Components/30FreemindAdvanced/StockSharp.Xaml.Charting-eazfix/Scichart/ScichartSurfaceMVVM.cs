// Decompiled with JetBrains decompiler
// Type: #=zJ9vSi7sIwIEed80npzusCHkUgplLrVxmg1iWODdl3TDNKj06Uu87_wzk09Wj
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using DevExpress.Xpf.Core;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Xaml;
using Ecng.Xaml.Converters;
using SciChart.Charting.ChartModifiers;
using SciChart.Charting.Visuals;
using SciChart.Charting.Visuals.Annotations;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

#nullable enable
public sealed class ScichartSurfaceMVVM : 
  ChartBaseViewModel,
  IDisposable,
  IScichartSurfaceVM
{
  
  private readonly ScichartSurfaceMVVM.ChartComponentParentVM _childElements;
  
  private readonly Dictionary<IChartComponent, List<IRenderableSeries>> _chartUIRSeries;
  
  private readonly Dictionary<IChartComponent, Dictionary<object, IAnnotation>> _topChartElmentAnnotationMap;
  
  private SciChartSurface _sciChartSurface;
  
  private bool _doneInitialization;
  
  private readonly List<ChartModifierBase> _chartModifiers;
  
  private readonly RubberBandXyZoomModifier _rubberBandXyZoomModifier;
  
  private readonly DispatcherTimer _dispatcherTimer;
  
  private readonly SynchronizedSet<ChartCompentViewModel> _parentChartViewModelCache;
  
  private readonly Queue<double> _queue;
  
  private double _fpsTotal;
  
  private readonly ObservableCollection<ChartCompentViewModel> _legendElements;
  
  private Action<IChartElement> RemoveElementEvent;
  
  private string _paneGroupSuffix;
  
  private readonly ChartArea _chartArea;
  
  private LegendModifierVM \u0023\u003DzrqALM6ELXjuaZXtSJw\u003D\u003D;
  
  private dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd \u0023\u003DzIfS1UPijEYCX;
  
  private readonly ObservableCollection<IRenderableSeries> \u0023\u003DzydxgB3sqmEwSB0XxJw\u003D\u003D;
  
  private readonly \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUrpBxQ9IN4CBXg\u003D\u003D _xAxisNotifyList;
  
  private readonly \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUrpBxQ9IN4CBXg\u003D\u003D _yAxisNotifyList;
  
  private bool \u0023\u003DziDb\u0024Ig3dGddOMOYevA\u003D\u003D;
  
  private ChartCompentViewModel \u0023\u003Dz9wKViCODW1Ii2tIsob2\u0024JVIp5Szh;
  
  private readonly \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D \u0023\u003DzFDPZeiB0vr7jQ9JhGA\u003D\u003D;
  
  private dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd \u0023\u003DzxkuwMuJ5gtvs;
  
  private readonly \u0023\u003DzsDU9XQyTsl2DjEg2HhKpBKWzr\u0024H44WFj\u0024d8XySFFwpGZ8_Hs5A\u003D\u003D \u0023\u003Dz2wrgvLmSMxAy\u0024vli6w\u003D\u003D;
  
  private string \u0023\u003DzZ5hHunO\u00246RUt;
  
  private double _height;
  
  private bool _showPerfStats;
  
  private TimeSpan _autoRangeIntervalNoGroup;
  
  private bool \u0023\u003Dz_BSxov_qh\u0024r8;
  
  private bool \u0023\u003Dzp9yX6hrHRIBY;
  
  private int _minimumRange;
  
  private string \u0023\u003Dz0M9ae_JEb7EN;
  
  private double _dataPointWidth;
  
  private ICommand _resetAxisTimeZoneCommand;
  
  private ICommand _closePaneCommand;
  
  private readonly ICommand _showHiddenAxesCommand;

  public ScichartSurfaceMVVM(ChartArea _param1) : base()
    {
    ScichartSurfaceMVVM.SomeClass6409 v4vdZv8GtEzAmB0rzFq = new ScichartSurfaceMVVM.SomeClass6409();
    v4vdZv8GtEzAmB0rzFq.\u0023\u003Dzy_5REws\u003D = _param1;
    
    v4vdZv8GtEzAmB0rzFq._variableSome3535 = this;
    this._chartArea = v4vdZv8GtEzAmB0rzFq.\u0023\u003Dzy_5REws\u003D ?? throw new ArgumentNullException("area");
    this._dispatcherTimer = new DispatcherTimer((DispatcherPriority) 7, Application.Current.Dispatcher);
    this._dispatcherTimer.Tick += new EventHandler(this.\u0023\u003DzMXbJl7nt_k3k);
    this._showHiddenAxesCommand = (ICommand) new DelegateCommand(new Action(this.\u0023\u003Dzf5vVcayMq7_zxBOILQ\u003D\u003D));
    this.ResetAxisTimeZoneCommand = (ICommand) new DelegateCommand<ChartAxis>(ScichartSurfaceMVVM.SomeClass34343383.\u0023\u003Dzfa4InO8BG_qfj7v\u0024gA\u003D\u003D ?? (ScichartSurfaceMVVM.SomeClass34343383.\u0023\u003Dzfa4InO8BG_qfj7v\u0024gA\u003D\u003D = new Action<ChartAxis>(ScichartSurfaceMVVM.SomeClass34343383.SomeMethond0343.\u0023\u003DzB6IuSPwhBuqMEjjiXTaDcdI\u003D)));
    this.Height = v4vdZv8GtEzAmB0rzFq.\u0023\u003Dzy_5REws\u003D.Height;
    ((INotifyCollection<IChartElement>) v4vdZv8GtEzAmB0rzFq.\u0023\u003Dzy_5REws\u003D.Elements).Added += new Action<IChartElement>(this.\u0023\u003Dz4M_pW8k\u003D);
    ((INotifyCollection<IChartElement>) v4vdZv8GtEzAmB0rzFq.\u0023\u003Dzy_5REws\u003D.Elements).Removing += new Func<IChartElement, bool>(this.\u0023\u003DzmxDTmQc\u003D);
    ((INotifyCollection<IChartElement>) v4vdZv8GtEzAmB0rzFq.\u0023\u003Dzy_5REws\u003D.Elements).RemovingAt += new Func<int, bool>(v4vdZv8GtEzAmB0rzFq.\u0023\u003Dzk0q47haYd4sk7HJcWw\u003D\u003D);
    ((INotifyCollection<IChartElement>) v4vdZv8GtEzAmB0rzFq.\u0023\u003Dzy_5REws\u003D.Elements).Clearing += new Func<bool>(v4vdZv8GtEzAmB0rzFq.\u0023\u003DzvwGmGW0txy637Xwc2g\u003D\u003D);
    ((INotifyCollection<IChartAxis>) v4vdZv8GtEzAmB0rzFq.\u0023\u003Dzy_5REws\u003D.XAxises).Added += new Action<IChartAxis>(v4vdZv8GtEzAmB0rzFq.\u0023\u003DzzTy9O8Bfu2Mtf5LgWw\u003D\u003D);
    ((INotifyCollection<IChartAxis>) v4vdZv8GtEzAmB0rzFq.\u0023\u003Dzy_5REws\u003D.XAxises).Removing += new Func<IChartAxis, bool>(v4vdZv8GtEzAmB0rzFq.\u0023\u003DzNMuSlzFThmIy12t4iw\u003D\u003D);
    ((INotifyCollection<IChartAxis>) v4vdZv8GtEzAmB0rzFq.\u0023\u003Dzy_5REws\u003D.XAxises).RemovingAt += new Func<int, bool>(v4vdZv8GtEzAmB0rzFq.\u0023\u003DzFhH1jy_AOH_jnpgLnA\u003D\u003D);
    ((INotifyCollection<IChartAxis>) v4vdZv8GtEzAmB0rzFq.\u0023\u003Dzy_5REws\u003D.YAxises).Added += new Action<IChartAxis>(v4vdZv8GtEzAmB0rzFq.\u0023\u003DzdMV7Kr3bkeboUtRdSw\u003D\u003D);
    ((INotifyCollection<IChartAxis>) v4vdZv8GtEzAmB0rzFq.\u0023\u003Dzy_5REws\u003D.YAxises).Removing += new Func<IChartAxis, bool>(v4vdZv8GtEzAmB0rzFq.SomeInternalVoidFunction30439);
    ((INotifyCollection<IChartAxis>) v4vdZv8GtEzAmB0rzFq.\u0023\u003Dzy_5REws\u003D.YAxises).RemovingAt += new Func<int, bool>(v4vdZv8GtEzAmB0rzFq.\u0023\u003Dzn0BxqFlrvEnaJ4F1tg\u003D\u003D);
    ThemeManager.ApplicationThemeChanged += new ThemeChangedRoutedEventHandler(v4vdZv8GtEzAmB0rzFq.\u0023\u003DzSf0sPwNIL9V4tJHvwQ\u003D\u003D);
    this.ChangeApplicationTheme();
    CollectionHelper.ForEach<IChartElement>((IEnumerable<IChartElement>) this.Area.Elements, new Action<IChartElement>(this.\u0023\u003Dz4M_pW8k\u003D));
    this.Area.PropertyChanged += new PropertyChangedEventHandler(this.\u0023\u003DzCGMYersHFBBf);
    RubberBandXyZoomModifier lk95Z3MuF7S7B6NwEjd = new RubberBandXyZoomModifier();
    lk95Z3MuF7S7B6NwEjd.IsXAxisOnly = true;
    lk95Z3MuF7S7B6NwEjd.ExecuteOn = dje_zKVLMQAQ8PVF9ES7S7RS764GN625CPCY4KFZRJDNDGVHGXXQ_ejd.MouseRightButton;
    lk95Z3MuF7S7B6NwEjd.ReceiveHandledEvents = true;
    this._rubberBandXyZoomModifier = lk95Z3MuF7S7B6NwEjd;
  }

  private object \u0023\u003DzVNHlJWAgfVxe() => (object) this.ParentViewModel ?? (object) this;

  public ObservableCollection<ChartCompentViewModel> LegendElements
  {
    get => this._legendElements;
  }

  public void RemoveElementEvent(Action<IChartElement> _param1)
  {
    Action<IChartElement> action = this.RemoveElementEvent;
    Action<IChartElement> comparand;
    do
    {
      comparand = action;
      action = Interlocked.CompareExchange<Action<IChartElement>>(ref this.RemoveElementEvent, comparand + _param1, comparand);
    }
    while (action != comparand);
  }

  public void RemoveElementEvent(Action<IChartElement> _param1)
  {
    Action<IChartElement> action = this.RemoveElementEvent;
    Action<IChartElement> comparand;
    do
    {
      comparand = action;
      action = Interlocked.CompareExchange<Action<IChartElement>>(ref this.RemoveElementEvent, comparand - _param1, comparand);
    }
    while (action != comparand);
  }

  private void \u0023\u003DzCGMYersHFBBf(object _param1, PropertyChangedEventArgs _param2)
  {
    if (_param2.PropertyName == "Height")
      this.Height = this.Area.Height;
    if (!(_param2.PropertyName == "Title"))
      return;
    this.NotifyChanged("Title");
  }

  private void \u0023\u003DzrvSxJoHvqJDd()
  {
    if (this._chartModifiers.Count > 0)
      return;
    dje_z3Z8T7U2SE2UJT59CTNY3TET7JDFJC5MSG29WF6RDGAYU7XFY5TNXTAC5YX2796KWCRF36XMM_ejd yx2796KwcrF36XmmEjd1 = new dje_z3Z8T7U2SE2UJT59CTNY3TET7JDFJC5MSG29WF6RDGAYU7XFY5TNXTAC5YX2796KWCRF36XMM_ejd();
    yx2796KwcrF36XmmEjd1.ShowTooltip = true;
    yx2796KwcrF36XmmEjd1.ReceiveHandledEvents = true;
    dje_z3Z8T7U2SE2UJT59CTNY3TET7JDFJC5MSG29WF6RDGAYU7XFY5TNXTAC5YX2796KWCRF36XMM_ejd yx2796KwcrF36XmmEjd2 = yx2796KwcrF36XmmEjd1;
    \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIwTJDRaTmD7p4rLOD8XENmXciu1eU_Ge7r60S1WUU1r\u0024gA\u003D\u003D uGe7r60S1WuU1rGA1 = new \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIwTJDRaTmD7p4rLOD8XENmXciu1eU_Ge7r60S1WUU1r\u0024gA\u003D\u003D(this.Area);
    uGe7r60S1WuU1rGA1.IsEnabled = false;
    \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIwTJDRaTmD7p4rLOD8XENmXciu1eU_Ge7r60S1WUU1r\u0024gA\u003D\u003D uGe7r60S1WuU1rGA2 = uGe7r60S1WuU1rGA1;
    dje_zMFW7VEH9YQSML9Y7R42FYSK6877R58D8BSZ6YPBYDEBG6VFFGCPZE_ejd ypbydebG6VffgcpzeEjd1 = new dje_zMFW7VEH9YQSML9Y7R42FYSK6877R58D8BSZ6YPBYDEBG6VFFGCPZE_ejd();
    ypbydebG6VffgcpzeEjd1.ExecuteOn = dje_zKVLMQAQ8PVF9ES7S7RS764GN625CPCY4KFZRJDNDGVHGXXQ_ejd.MouseLeftButton;
    ypbydebG6VffgcpzeEjd1.ReceiveHandledEvents = true;
    ypbydebG6VffgcpzeEjd1.ClipModeX = dje_z2ZZ2J3MC6TQVDLKAL45CSJLJJGW9K8Z7DFRDFNMP_ejd.None;
    dje_zMFW7VEH9YQSML9Y7R42FYSK6877R58D8BSZ6YPBYDEBG6VFFGCPZE_ejd ypbydebG6VffgcpzeEjd2 = ypbydebG6VffgcpzeEjd1;
    \u0023\u003Dz_QZ2gpRafNgOtcPtA9qy6iQnmqCFD8UsSE9i_38\u003D qnmqCfD8UsSe9i38 = new \u0023\u003Dz_QZ2gpRafNgOtcPtA9qy6iQnmqCFD8UsSE9i_38\u003D();
    qnmqCfD8UsSe9i38.\u0023\u003DzQNMSGlzReVSKbDSdEA\u003D\u003D(new Action<\u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY>(this.\u0023\u003Dz2baZtMhFLPZu0vCzoJUPQZE\u003D));
    qnmqCfD8UsSe9i38.\u0023\u003DzHs7QOJE3efiH3JF5Bw\u003D\u003D(new Action<\u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY>(this.\u0023\u003DzSH7LR2Lse3B4yzL0g_1pqV8\u003D));
    List<ChartModifierBase> zRhhpqbmmRle6 = this._chartModifiers;
    ChartModifierBase[] v7UvhxrxhaatqEjdArray = new ChartModifierBase[9];
    v7UvhxrxhaatqEjdArray[0] = (ChartModifierBase) qnmqCfD8UsSe9i38;
    v7UvhxrxhaatqEjdArray[1] = (ChartModifierBase) uGe7r60S1WuU1rGA2;
    v7UvhxrxhaatqEjdArray[2] = (ChartModifierBase) yx2796KwcrF36XmmEjd2;
    dje_zDBVG6SK23T4RT3VZFJ9FTNBW2KXD43HARN8WCSFFBEMYBNM9U2CJD_ejd wcsffbemybnM9U2CjdEjd = new dje_zDBVG6SK23T4RT3VZFJ9FTNBW2KXD43HARN8WCSFFBEMYBNM9U2CJD_ejd();
    wcsffbemybnM9U2CjdEjd.AxisId = "X";
    wcsffbemybnM9U2CjdEjd.ClipModeX = dje_z2ZZ2J3MC6TQVDLKAL45CSJLJJGW9K8Z7DFRDFNMP_ejd.None;
    v7UvhxrxhaatqEjdArray[3] = (ChartModifierBase) wcsffbemybnM9U2CjdEjd;
    dje_zHZJUNELMY3BAWUYNNRAVXVEJSS7HS9SSZHRJV76DGE2H48XYYA87S_ejd dgE2H48XyyA87SEjd = new dje_zHZJUNELMY3BAWUYNNRAVXVEJSS7HS9SSZHRJV76DGE2H48XYYA87S_ejd();
    dgE2H48XyyA87SEjd.AxisId = "Y";
    v7UvhxrxhaatqEjdArray[4] = (ChartModifierBase) dgE2H48XyyA87SEjd;
    v7UvhxrxhaatqEjdArray[5] = (ChartModifierBase) new dje_z48XSEY4E7J7ZY268G4C2RR2SX8TP9XUT5MGB3Z3KUFJWUUVR4YBN3_ejd();
    v7UvhxrxhaatqEjdArray[6] = (ChartModifierBase) this._rubberBandXyZoomModifier;
    dje_zNHZFRV6VYN2XDNU56GMDGQJ2YP79UFMBF66RXN4FK4QGAPHFMMUJD_ejd fk4QgaphfmmujdEjd = new dje_zNHZFRV6VYN2XDNU56GMDGQJ2YP79UFMBF66RXN4FK4QGAPHFMMUJD_ejd();
    fk4QgaphfmmujdEjd.ExecuteOn = dje_zKVLMQAQ8PVF9ES7S7RS764GN625CPCY4KFZRJDNDGVHGXXQ_ejd.MouseDoubleClick;
    v7UvhxrxhaatqEjdArray[7] = (ChartModifierBase) fk4QgaphfmmujdEjd;
    v7UvhxrxhaatqEjdArray[8] = (ChartModifierBase) ypbydebG6VffgcpzeEjd2;
    \u0023\u003DzFxYNKQ1M2eiqODEcXA\u003D\u003D<ChartModifierBase> collection = new \u0023\u003DzFxYNKQ1M2eiqODEcXA\u003D\u003D<ChartModifierBase>(v7UvhxrxhaatqEjdArray);
    zRhhpqbmmRle6.AddRange((IEnumerable<ChartModifierBase>) collection);
  }

  private void \u0023\u003DzPHnXepkJBjde(object _param1, PropertyChangedEventArgs _param2)
  {
    switch (_param2.PropertyName)
    {
      case "AutoRangeInterval":
        this.\u0023\u003Dzdp1hbkXJ1MkF();
        this.NotifyChanged(_param2.PropertyName);
        break;
      case "ShowPerfStats":
        this.NotifyChanged(_param2.PropertyName);
        break;
    }
  }

  public IEnumerable<Order> \u0023\u003DzQ\u0024gUWeEbsN2c(Func<Order, bool> _param1)
  {
    return ((IEnumerable<ParentVM>) this._childElements.CachedValues).Where<ParentVM>(ScichartSurfaceMVVM.SomeClass34343383.\u0023\u003DzZ1TIJti3dLv69swWwQ\u003D\u003D ?? (ScichartSurfaceMVVM.SomeClass34343383.\u0023\u003DzZ1TIJti3dLv69swWwQ\u003D\u003D = new Func<ParentVM, bool>(ScichartSurfaceMVVM.SomeClass34343383.SomeMethond0343.\u0023\u003DzrQJFhnLMBw4KLdFbErEk\u0024Eju6p\u00242))).SelectMany<ParentVM, Order>(new Func<ParentVM, IEnumerable<Order>>(new ScichartSurfaceMVVM.\u0023\u003Dz0EETrg8PejletCT5YuMr1Rw\u003D()
    {
      \u0023\u003DzaPd0W_M\u003D = _param1
    }.\u0023\u003Dz3ppSxBWUBHoE_OmJHdF4GT0\u003D));
  }

  public void InitPropertiesEventHandlers()
  {
    ((INotifyPropertyChanged) this.Chart).PropertyChanged += new PropertyChangedEventHandler(this.\u0023\u003DzPHnXepkJBjde);
    if (!this._doneInitialization)
    {
      this._doneInitialization = true;
      this.ChartModifier.ChildModifiers.Add((IChartModifier) this.LegendModifier);
      this.ChartModifier.ChildModifiers.Add((IChartModifier) this.AnnotationModifier);
      IChartCandleElement[] array = ((SynchronizedDictionary<IChartComponent, ParentVM>) this._childElements).Keys.OfType<IChartCandleElement>().ToArray<IChartCandleElement>();
      if (array.Length != 0)
        CollectionHelper.ForEach<IChartCandleElement>((IEnumerable<IChartCandleElement>) array, new Action<IChartCandleElement>(this.\u0023\u003Dz5QuVGogKpyDd_u\u0024czKaNHcU\u003D));
    }
    this.\u0023\u003Dz9VF9FI8x1QxU();
    if (this.ParentViewModel == null)
    {
      this.ClosePaneCommand = (ICommand) null;
    }
    else
    {
      this.ClosePaneCommand = (ICommand) this.ParentViewModel.ClosePaneCommand;
      this.ParentViewModel.AddPropertyListener(ChartViewModel.ShowLegendProperty, new Action<DependencyPropertyChangedEventArgs>(this.\u0023\u003DzlzIC4G73INUgxBGJvcu\u0024bn8\u003D));
      this.ParentViewModel.AddPropertyListener(ChartViewModel.ShowOverviewProperty, new Action<DependencyPropertyChangedEventArgs>(this.\u0023\u003DzQdV5fzQnxZUH8cqQCCGjyic\u003D));
      this.ParentViewModel.AddPropertyListener(ChartViewModel.MinimumRangeProperty, new Action<DependencyPropertyChangedEventArgs>(this.\u0023\u003DzaPzrvWfKjELcmaG2lpOTZss\u003D));
      this.ParentViewModel.AddPropertyListener(ChartViewModel.SelectedThemeProperty, new Action<DependencyPropertyChangedEventArgs>(this.\u0023\u003Dz\u0024BXtYBf7QC49I9Cu8SMYkgA\u003D));
    }
    StockSharp.Xaml.Charting.Chart groupChart = this.GroupChart;
    if (groupChart != null)
    {
      this.\u0023\u003DzrvSxJoHvqJDd();
      CollectionHelper.AddRange<IChartModifier>((ICollection<IChartModifier>) this.ChartModifier.ChildModifiers, (IEnumerable<IChartModifier>) this._chartModifiers);
      this.AnnotationModifier.SetBindings(dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd.\u0023\u003DzEaRzkw2Bl16I, (object) groupChart, "AnnotationType");
      dje_z3Z8T7U2SE2UJT59CTNY3TET7JDFJC5MSG29WF6RDGAYU7XFY5TNXTAC5YX2796KWCRF36XMM_ejd yx2796KwcrF36XmmEjd = this.ChartModifier.ChildModifiers.OfType<dje_z3Z8T7U2SE2UJT59CTNY3TET7JDFJC5MSG29WF6RDGAYU7XFY5TNXTAC5YX2796KWCRF36XMM_ejd>().Single<dje_z3Z8T7U2SE2UJT59CTNY3TET7JDFJC5MSG29WF6RDGAYU7XFY5TNXTAC5YX2796KWCRF36XMM_ejd>();
      yx2796KwcrF36XmmEjd.SetBindings(ChartModifierBase.\u0023\u003DzSLZmDSF5TsAu, (object) this.Chart, "CrossHair");
      yx2796KwcrF36XmmEjd.SetBindings(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003DzCuzcJq\u0024VLiWR, (object) this.Chart, "CrossHairAxisLabels");
      yx2796KwcrF36XmmEjd.SetBindings(dje_z3Z8T7U2SE2UJT59CTNY3TET7JDFJC5MSG29WF6RDGAYU7XFY5TNXTAC5YX2796KWCRF36XMM_ejd.\u0023\u003DztwQ4ieQ9dTof, (object) this.Chart, "CrossHairTooltip");
      \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIwTJDRaTmD7p4rLOD8XENmXciu1eU_Ge7r60S1WUU1r\u0024gA\u003D\u003D uGe7r60S1WuU1rGA1 = this.ChartModifier.ChildModifiers.OfType<\u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIwTJDRaTmD7p4rLOD8XENmXciu1eU_Ge7r60S1WUU1r\u0024gA\u003D\u003D>().Single<\u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIwTJDRaTmD7p4rLOD8XENmXciu1eU_Ge7r60S1WUU1r\u0024gA\u003D\u003D>();
      \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIwTJDRaTmD7p4rLOD8XENmXciu1eU_Ge7r60S1WUU1r\u0024gA\u003D\u003D uGe7r60S1WuU1rGA2 = uGe7r60S1WuU1rGA1;
      DependencyProperty zSlZmDsF5TsAu = ChartModifierBase.\u0023\u003DzSLZmDSF5TsAu;
      BoolAllConverter conv = new BoolAllConverter();
      conv.Value = true;
      Binding[] bindingArray = new Binding[2]
      {
        new Binding("OrderCreationMode")
        {
          Source = (object) this.Chart
        },
        new Binding("PaneHasCandles") { Source = (object) this }
      };
      uGe7r60S1WuU1rGA2.SetMultiBinding(zSlZmDsF5TsAu, (IMultiValueConverter) conv, bindingArray);
      uGe7r60S1WuU1rGA1.SetBindings(\u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIwTJDRaTmD7p4rLOD8XENmXciu1eU_Ge7r60S1WUU1r\u0024gA\u003D\u003D.\u0023\u003DzISs1rY1PUWf_, (object) this.Chart, "CrossHair", BindingMode.OneWay, (IValueConverter) new InverseBooleanConverter());
      this.ChartModifier.ChildModifiers.OfType<dje_zMFW7VEH9YQSML9Y7R42FYSK6877R58D8BSZ6YPBYDEBG6VFFGCPZE_ejd>().Single<dje_zMFW7VEH9YQSML9Y7R42FYSK6877R58D8BSZ6YPBYDEBG6VFFGCPZE_ejd>().SetBindings(ChartModifierBase.\u0023\u003DzSLZmDSF5TsAu, (object) this.Chart, "AnnotationType", BindingMode.OneWay, (IValueConverter) new EnumBooleanConverter(), (object) ChartAnnotationTypes.None.ToString());
      \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vgE1y_16\u0024Ql8QLBV6E\u003D.SetMouseEventGroup((DependencyObject) this.ChartModifier, this.PaneGroup);
    }
    this.\u0023\u003Dzdp1hbkXJ1MkF();
    CollectionHelper.ForEach<IChartAxis>((IEnumerable<IChartAxis>) this.Area.XAxises, new Action<IChartAxis>(this.\u0023\u003DzQVqx9924gaWX1\u0024r5THddBi0\u003D));
    CollectionHelper.ForEach<IChartAxis>((IEnumerable<IChartAxis>) this.Area.YAxises, new Action<IChartAxis>(this.\u0023\u003DzhueGWJf3Qdd7bLHGZUE_sNY\u003D));
  }

  public void Release()
  {
    ((INotifyPropertyChanged) this.Chart).PropertyChanged -= new PropertyChangedEventHandler(this.\u0023\u003DzPHnXepkJBjde);
    if (this.GroupChart != null)
      this._chartModifiers.ForEach(new Action<ChartModifierBase>(this.\u0023\u003DzTdIpNSu812itYYHN7lIakb6BiejkaXWFgQ\u003D\u003D));
    CollectionHelper.ForEach<IChartAxis>((IEnumerable<IChartAxis>) this.Area.XAxises, new Action<IChartAxis>(this.\u0023\u003DzrFpAakDNl3igj4xxMoWpQFW0GE4J_yze\u0024g\u003D\u003D));
    CollectionHelper.ForEach<IChartAxis>((IEnumerable<IChartAxis>) this.Area.YAxises, new Action<IChartAxis>(this.\u0023\u003DzGN9dmAg9QrR2Z7eHl8GIJrnf1RvCfrWhVA\u003D\u003D));
    this._dispatcherTimer.Stop();
  }

  private void \u0023\u003Dzdp1hbkXJ1MkF()
  {
    this._dispatcherTimer.Stop();
    this._dispatcherTimer.Interval = this.AutoRangeInterval;
    this._dispatcherTimer.Start();
  }

  private void \u0023\u003DzBUrMEb8\u003D(
    IChartAxis _param1,
    ICollection<IAxis> _param2)
  {
    ScichartSurfaceMVVM.\u0023\u003Dz5NCozMpa4F4mcyQlZrFJfRI\u003D mpa4F4mcyQlZrFjfRi = new ScichartSurfaceMVVM.\u0023\u003Dz5NCozMpa4F4mcyQlZrFJfRI\u003D();
    mpa4F4mcyQlZrFjfRi._variableSome3535 = this;
    mpa4F4mcyQlZrFjfRi.\u0023\u003Dzfl\u0024A1s0\u003D = _param1;
    mpa4F4mcyQlZrFjfRi.\u0023\u003Dz_liTKnA\u003D = _param2;
    FrameworkElement chart = (FrameworkElement) this.Chart;
    if (chart == null)
      return;
    ((DispatcherObject) chart).GuiAsync(new Action(mpa4F4mcyQlZrFjfRi.\u0023\u003DzFX_lHxlPjn56eMHQ\u0024g\u003D\u003D));
  }

  private void \u0023\u003Dz0FfA4J7ON5133\u00246jKg\u003D\u003D()
  {
    foreach (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd axF9ZgQ7NbH9KsEjd1 in this.XAxises.OfType<dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd>())
    {
      if (axF9ZgQ7NbH9KsEjd1.Tag is IChartAxis tag)
      {
        \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR g72ZksY7iW1Jk3iR = \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR.\u0023\u003DzYMTYgq1xYsSy(this.\u0023\u003DzVNHlJWAgfVxe(), tag.Group, this.PaneGroupSuffix, tag.AxisType);
        dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd axF9ZgQ7NbH9KsEjd2 = axF9ZgQ7NbH9KsEjd1;
        DependencyProperty zWl3LbWhL1z0D = dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzWl3LbWhL1z0D;
        \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR dataObject = g72ZksY7iW1Jk3iR;
        string path;
        if (g72ZksY7iW1Jk3iR.\u0023\u003DzOdcnf0c\u003D() != ChartAxisType.CategoryDateTime)
        {
          if (g72ZksY7iW1Jk3iR.\u0023\u003DzOdcnf0c\u003D() != ChartAxisType.Numeric)
          {
            if (g72ZksY7iW1Jk3iR.\u0023\u003DzOdcnf0c\u003D() != ChartAxisType.DateTime)
              throw new NotSupportedException("unsupported range type");
            path = "DateTimeRange";
          }
          else
            path = "NumericRange";
        }
        else
          path = "CategoryDateTimeRange";
        axF9ZgQ7NbH9KsEjd2.SetBindings(zWl3LbWhL1z0D, (object) dataObject, path);
      }
    }
  }

  private bool \u0023\u003Dz_FxkB6U\u003D(
    IChartAxis _param1,
    ICollection<IAxis> _param2)
  {
    ScichartSurfaceMVVM.\u0023\u003DzM\u0024TlJy0mx0yCUWlqEsh0ZdY\u003D jy0mx0yCuWlqEsh0ZdY = new ScichartSurfaceMVVM.\u0023\u003DzM\u0024TlJy0mx0yCUWlqEsh0ZdY\u003D();
    jy0mx0yCuWlqEsh0ZdY.\u0023\u003Dz_liTKnA\u003D = _param2;
    jy0mx0yCuWlqEsh0ZdY.\u0023\u003Dzfl\u0024A1s0\u003D = _param1;
    jy0mx0yCuWlqEsh0ZdY._variableSome3535 = this;
    FrameworkElement chart = (FrameworkElement) this.Chart;
    if (chart != null)
      ((DispatcherObject) chart).GuiAsync(new Action(jy0mx0yCuWlqEsh0ZdY.\u0023\u003DzgPfL2cm03IfPSYpk8w\u003D\u003D));
    return true;
  }

  private void \u0023\u003Dzfi9Y8f8VaR3y(object _param1, PropertyChangedEventArgs _param2)
  {
    if (!(_param1 is dje_zP5SLCZMPLKRDSVWETEPWLMZPT4N45VSYZ76M5M7C6J68NU9622VFYDAYPDEQ_ejd nu9622VfydaypdeqEjd) || _param2.PropertyName != "CurrentDatapointPixelSize")
      return;
    IChartCandleElement chartCandleElement = ((SynchronizedDictionary<IChartComponent, ParentVM>) this._childElements).Keys.OfType<IChartCandleElement>().FirstOrDefault<IChartCandleElement>();
    if (chartCandleElement != null && chartCandleElement.XAxisId != nu9622VfydaypdeqEjd.Id)
      return;
    this.DataPointWidth = Math.Round(MathHelper.IsNaN(nu9622VfydaypdeqEjd.CurrentDatapointPixelSize) ? 0.0 : nu9622VfydaypdeqEjd.CurrentDatapointPixelSize, 1, MidpointRounding.AwayFromZero);
  }

  public void \u0023\u003Dz3p2JBPVHDEUh(
    SciChartSurface _param1)
  {
    if (this._sciChartSurface == _param1)
      return;
    this._sciChartSurface = this._sciChartSurface == null || _param1 == null ? _param1 : throw new InvalidOperationException("got unexpected chart surface");
    _param1?.RenderSurface.\u0023\u003DzKPHSi1vgK\u0024Fx(new EventHandler<\u0023\u003DzuPRmIFUVJkGxyCE55JH19euDoShPRPT3Wvs_KD4jptvr>(this.\u0023\u003Dz0dAR\u00244WTXjQ\u0024));
  }

  public void Draw(ChartDrawData _param1)
  {
    if (this._sciChartSurface != null)
    {
      using (this._sciChartSurface.SuspendUpdates())
        this.\u0023\u003DzKrKnCxVCz6us(_param1);
    }
    else
      this.\u0023\u003DzKrKnCxVCz6us(_param1);
  }

  private void \u0023\u003Dz0dAR\u00244WTXjQ\u0024(
    object _param1,
    \u0023\u003DzuPRmIFUVJkGxyCE55JH19euDoShPRPT3Wvs_KD4jptvr _param2)
  {
    if (this._sciChartSurface == null)
      return;
    CollectionHelper.ForEach<dje_zP5SLCZMPLKRDSVWETEPWLMZPT4N45VSYZ76M5M7C6J68NU9622VFYDAYPDEQ_ejd>(this.XAxises.OfType<dje_zP5SLCZMPLKRDSVWETEPWLMZPT4N45VSYZ76M5M7C6J68NU9622VFYDAYPDEQ_ejd>(), ScichartSurfaceMVVM.SomeClass34343383.\u0023\u003DzrDW3kcvb8jweTamfSw\u003D\u003D ?? (ScichartSurfaceMVVM.SomeClass34343383.\u0023\u003DzrDW3kcvb8jweTamfSw\u003D\u003D = new Action<dje_zP5SLCZMPLKRDSVWETEPWLMZPT4N45VSYZ76M5M7C6J68NU9622VFYDAYPDEQ_ejd>(ScichartSurfaceMVVM.SomeClass34343383.SomeMethond0343.\u0023\u003DzJR2NRNOjWD80Ydt_1RhppIXJFkoJ)));
    CollectionHelper.ForEach<ParentVM>((IEnumerable<ParentVM>) ((SynchronizedDictionary<IChartComponent, ParentVM>) this._childElements).Values, ScichartSurfaceMVVM.SomeClass34343383.\u0023\u003DzHf64i7sS_m0gAJCSlg\u003D\u003D ?? (ScichartSurfaceMVVM.SomeClass34343383.\u0023\u003DzHf64i7sS_m0gAJCSlg\u003D\u003D = new Action<ParentVM>(ScichartSurfaceMVVM.SomeClass34343383.SomeMethond0343.\u0023\u003DzpfilwLTVeZDaJUeDuSBVjV3PZ4yR)));
    if (!this.ShowPerfStats || _param2.\u0023\u003DzguiAuOeZYTXy() <= 0.0)
      return;
    double num = 1000.0 / _param2.\u0023\u003DzguiAuOeZYTXy();
    while (this._queue.Count >= 10)
      this._fpsTotal -= this._queue.Dequeue();
    this._queue.Enqueue(num);
    this._fpsTotal += num;
    this.PerfStats = $"FPS: {this._fpsTotal / (double) this._queue.Count:0}   Count: {this._sciChartSurface.RenderableSeries.Where<IRenderableSeries>(ScichartSurfaceMVVM.SomeClass34343383.\u0023\u003DzOkZpX_jNLPK4\u0024VqKDw\u003D\u003D ?? (ScichartSurfaceMVVM.SomeClass34343383.\u0023\u003DzOkZpX_jNLPK4\u0024VqKDw\u003D\u003D = new Func<IRenderableSeries, bool>(ScichartSurfaceMVVM.SomeClass34343383.SomeMethond0343.\u0023\u003DzxrTS_1cNsfx\u0024XLYpPvCkhwAicVIY))).Sum<IRenderableSeries>(ScichartSurfaceMVVM.SomeClass34343383.\u0023\u003DzAMUmAPxCMMfLz0aKiA\u003D\u003D ?? (ScichartSurfaceMVVM.SomeClass34343383.\u0023\u003DzAMUmAPxCMMfLz0aKiA\u003D\u003D = new Func<IRenderableSeries, int>(ScichartSurfaceMVVM.SomeClass34343383.SomeMethond0343.\u0023\u003DzXPeSs80bTGyLZbXyn71KBILsfXa3))):n0}";
  }

  private void \u0023\u003DzKrKnCxVCz6us(ChartDrawData _param1)
  {
    foreach (ParentVM a4VgOpCeDiqsTdzB in this._childElements.\u0023\u003DzcMDdc03rjKXr())
    {
      if (a4VgOpCeDiqsTdzB.Draw(_param1))
        ((BaseCollection<ParentVM, ISet<ParentVM>>) this._parentChartViewModelCache).Add(a4VgOpCeDiqsTdzB);
    }
  }

  private void \u0023\u003DzMXbJl7nt_k3k(object _param1, EventArgs _param2)
  {
    ParentVM[] a4VgOpCeDiqsTdzBArray;
    lock (((SynchronizedCollection<ParentVM, ISet<ParentVM>>) this._parentChartViewModelCache).SyncRoot)
      a4VgOpCeDiqsTdzBArray = CollectionHelper.CopyAndClear<ParentVM>((ICollection<ParentVM>) this._parentChartViewModelCache);
    foreach (ParentVM a4VgOpCeDiqsTdzB in a4VgOpCeDiqsTdzBArray)
      a4VgOpCeDiqsTdzB.PerformPeriodicalAction();
  }

  public void Reset(IEnumerable<IChartElement> _param1)
  {
    foreach (IChartComponent ddznyiGmdRlAevOq in _param1)
    {
      ParentVM a4VgOpCeDiqsTdzB;
      if (this.\u0023\u003DzKDbpj6zM462r(ddznyiGmdRlAevOq, out a4VgOpCeDiqsTdzB))
      {
        ((BaseCollection<ParentVM, ISet<ParentVM>>) this._parentChartViewModelCache).Add(a4VgOpCeDiqsTdzB);
        a4VgOpCeDiqsTdzB.Reset();
      }
    }
  }

  private void \u0023\u003Dz9VF9FI8x1QxU()
  {
    foreach (IChartComponent ddznyiGmdRlAevOq in ((IEnumerable<KeyValuePair<IChartComponent, ParentVM>>) this._childElements).Where<KeyValuePair<IChartComponent, ParentVM>>(ScichartSurfaceMVVM.SomeClass34343383.\u0023\u003Dz8slTl9RRXzpBYOxh4Q\u003D\u003D ?? (ScichartSurfaceMVVM.SomeClass34343383.\u0023\u003Dz8slTl9RRXzpBYOxh4Q\u003D\u003D = new Func<KeyValuePair<IChartComponent, ParentVM>, bool>(ScichartSurfaceMVVM.SomeClass34343383.SomeMethond0343.\u0023\u003DzFUwEsnQgb3v8tcGvhyZ1DIc\u003D))).Select<KeyValuePair<IChartComponent, ParentVM>, IChartComponent>(ScichartSurfaceMVVM.SomeClass34343383.\u0023\u003DzG9p0UKsG3FcNaICZMQ\u003D\u003D ?? (ScichartSurfaceMVVM.SomeClass34343383.\u0023\u003DzG9p0UKsG3FcNaICZMQ\u003D\u003D = new Func<KeyValuePair<IChartComponent, ParentVM>, IChartComponent>(ScichartSurfaceMVVM.SomeClass34343383.SomeMethond0343.\u0023\u003DzkaiqO59tHfX5w4slAysHOio\u003D))).ToArray<IChartComponent>())
    {
      ParentVM a4VgOpCeDiqsTdzB = new ParentVM(this, ddznyiGmdRlAevOq);
      a4VgOpCeDiqsTdzB.InitializeChildElements(CollectionHelper.Append2<IChartElement>(ddznyiGmdRlAevOq.ChildElements, (IChartElement) ddznyiGmdRlAevOq).OfType<IDrawableChartElement>().Where<IDrawableChartElement>(ScichartSurfaceMVVM.SomeClass34343383.\u0023\u003DzrUb4sQiSyo1cFneMgA\u003D\u003D ?? (ScichartSurfaceMVVM.SomeClass34343383.\u0023\u003DzrUb4sQiSyo1cFneMgA\u003D\u003D = new Func<IDrawableChartElement, bool>(ScichartSurfaceMVVM.SomeClass34343383.SomeMethond0343.\u0023\u003DzihU1OXxWIdi3ASgjwqQi3aM\u003D))).Select<IDrawableChartElement, UIChartBaseViewModel>(new Func<IDrawableChartElement, UIChartBaseViewModel>(this.\u0023\u003DzMp9uGWCNEHILZjWmNYJHfvM\u003D)).Where<UIChartBaseViewModel>(ScichartSurfaceMVVM.SomeClass34343383.\u0023\u003DzKOhc8XoZrBjayO3KMw\u003D\u003D ?? (ScichartSurfaceMVVM.SomeClass34343383.\u0023\u003DzKOhc8XoZrBjayO3KMw\u003D\u003D = new Func<UIChartBaseViewModel, bool>(ScichartSurfaceMVVM.SomeClass34343383.SomeMethond0343.\u0023\u003DzDP46TP6OWYLlok7K1AlOaIg\u003D))));
      ((SynchronizedDictionary<IChartComponent, ParentVM>) this._childElements)[ddznyiGmdRlAevOq] = a4VgOpCeDiqsTdzB;
      if (ddznyiGmdRlAevOq.IsLegend)
        this.LegendElements.Add(a4VgOpCeDiqsTdzB);
    }
    this.\u0023\u003DzbLGEkvI0sqrejb5agA\u003D\u003D();
  }

  private void \u0023\u003DzbLGEkvI0sqrejb5agA\u003D\u003D()
  {
    this.CandlesCompositeElement = ((IEnumerable<KeyValuePair<IChartComponent, ParentVM>>) this._childElements).FirstOrDefault<KeyValuePair<IChartComponent, ParentVM>>(ScichartSurfaceMVVM.SomeClass34343383.\u0023\u003DzoANln2qVkbS_vrdROw\u003D\u003D ?? (ScichartSurfaceMVVM.SomeClass34343383.\u0023\u003DzoANln2qVkbS_vrdROw\u003D\u003D = new Func<KeyValuePair<IChartComponent, ParentVM>, bool>(ScichartSurfaceMVVM.SomeClass34343383.SomeMethond0343.\u0023\u003DzOxutfFQq8iOlG6uGIIvjfQjn_lrYDZf0Gw\u003D\u003D))).Value;
    this.PaneHasCandles = this.CandlesCompositeElement != null;
  }

  public bool \u0023\u003DzKDbpj6zM462r(
    IChartComponent _param1,
    out ParentVM _param2)
  {
    return ((SynchronizedDictionary<IChartComponent, ParentVM>) this._childElements).TryGetValue(_param1, ref _param2);
  }

  public void \u0023\u003Dz4M_pW8k\u003D(IChartElement _param1)
  {
    IChart chart = this.Chart;
    if (chart != null)
      chart.EnsureUIThread();
    IChartComponent ddznyiGmdRlAevOq = (IChartComponent) _param1;
    ddznyiGmdRlAevOq.AddAxisesAndEventHandler(this.Area);
    if (((SynchronizedDictionary<IChartComponent, ParentVM>) this._childElements).ContainsKey(ddznyiGmdRlAevOq))
      throw new ArgumentException("duplicate chart element", "element");
    if (this.Chart != null)
    {
      ParentVM a4VgOpCeDiqsTdzB = new ParentVM(this, ddznyiGmdRlAevOq);
      a4VgOpCeDiqsTdzB.InitializeChildElements(CollectionHelper.Append2<IChartElement>(ddznyiGmdRlAevOq.ChildElements, (IChartElement) ddznyiGmdRlAevOq).OfType<IDrawableChartElement>().Where<IDrawableChartElement>(ScichartSurfaceMVVM.SomeClass34343383.\u0023\u003Dz9cuH_vUOZn8pPBtNwQ\u003D\u003D ?? (ScichartSurfaceMVVM.SomeClass34343383.\u0023\u003Dz9cuH_vUOZn8pPBtNwQ\u003D\u003D = new Func<IDrawableChartElement, bool>(ScichartSurfaceMVVM.SomeClass34343383.SomeMethond0343.\u0023\u003DzZItGYEiEFtH3fOWMCA\u003D\u003D))).Select<IDrawableChartElement, UIChartBaseViewModel>(new Func<IDrawableChartElement, UIChartBaseViewModel>(this.\u0023\u003DzufB92GEzbUD3B9FSng\u003D\u003D)).Where<UIChartBaseViewModel>(ScichartSurfaceMVVM.SomeClass34343383.\u0023\u003DzesHJJr9lkjwYzxQD2g\u003D\u003D ?? (ScichartSurfaceMVVM.SomeClass34343383.\u0023\u003DzesHJJr9lkjwYzxQD2g\u003D\u003D = new Func<UIChartBaseViewModel, bool>(ScichartSurfaceMVVM.SomeClass34343383.SomeMethond0343.\u0023\u003DzmUdptvPaqZAvZEWcjQ\u003D\u003D))));
      ((SynchronizedDictionary<IChartComponent, ParentVM>) this._childElements)[ddznyiGmdRlAevOq] = a4VgOpCeDiqsTdzB;
      if (ddznyiGmdRlAevOq.IsLegend)
        this.LegendElements.Add(a4VgOpCeDiqsTdzB);
    }
    else
      ((SynchronizedDictionary<IChartComponent, ParentVM>) this._childElements)[ddznyiGmdRlAevOq] = (ParentVM) null;
    if (this.\u0023\u003DzxkuwMuJ5gtvs != null && ddznyiGmdRlAevOq is IChartCandleElement chartCandleElement)
    {
      this.\u0023\u003Dzs0UfYK\u0024prvoZsynK2TcvH5w\u003D(chartCandleElement);
      chartCandleElement.PropertyChanged += new PropertyChangedEventHandler(this.\u0023\u003DzzBrCMQ6JtN57pKl5fw\u003D\u003D);
    }
    this.\u0023\u003DzbLGEkvI0sqrejb5agA\u003D\u003D();
    ddznyiGmdRlAevOq.PropertyChanged += new PropertyChangedEventHandler(this.OnXYAxisPropertyChanged);
  }

  public bool \u0023\u003DzmxDTmQc\u003D(IChartElement _param1)
  {
    IChart chart = this.Chart;
    if (chart != null)
      chart.EnsureUIThread();
    IChartComponent ddznyiGmdRlAevOq = (IChartComponent) _param1;
    ParentVM a4VgOpCeDiqsTdzB;
    if (!this.\u0023\u003DzKDbpj6zM462r(ddznyiGmdRlAevOq, out a4VgOpCeDiqsTdzB))
      return false;
    ddznyiGmdRlAevOq.RemoveAxisesEventHandler();
    if (ddznyiGmdRlAevOq is IChartCandleElement chartCandleElement)
      chartCandleElement.PropertyChanged -= new PropertyChangedEventHandler(this.\u0023\u003DzzBrCMQ6JtN57pKl5fw\u003D\u003D);
    ddznyiGmdRlAevOq.PropertyChanged -= new PropertyChangedEventHandler(this.OnXYAxisPropertyChanged);
    ((SynchronizedDictionary<IChartComponent, ParentVM>) this._childElements).Remove(ddznyiGmdRlAevOq);
    if (a4VgOpCeDiqsTdzB != null)
    {
      a4VgOpCeDiqsTdzB.GuiUpdateAndClear();
      a4VgOpCeDiqsTdzB.Dispose();
      this.LegendElements.Remove(a4VgOpCeDiqsTdzB);
    }
    this.\u0023\u003DzbLGEkvI0sqrejb5agA\u003D\u003D();
    return true;
  }

  private void OnXYAxisPropertyChanged(object _param1, PropertyChangedEventArgs _param2)
  {
    IChartComponent ddznyiGmdRlAevOq = (IChartComponent) _param1;
    if (_param2.PropertyName != "XAxisId" && _param2.PropertyName != "YAxisId")
      return;
    IChart chart = this.Chart;
    if (chart != null)
      chart.EnsureUIThread();
    List<IRenderableSeries> koh9jO5RuUcFiAqLcList;
    if (this._chartUIRSeries.TryGetValue(ddznyiGmdRlAevOq, out koh9jO5RuUcFiAqLcList))
    {
      if (ddznyiGmdRlAevOq.TryGetXAxis() != null && ddznyiGmdRlAevOq.TryGetYAxis() != null)
      {
        foreach (IRenderableSeries koh9jO5RuUcFiAqLc in koh9jO5RuUcFiAqLcList)
        {
          if (!this.\u0023\u003DzydxgB3sqmEwSB0XxJw\u003D\u003D.Contains(koh9jO5RuUcFiAqLc))
            this.\u0023\u003DzydxgB3sqmEwSB0XxJw\u003D\u003D.Add(koh9jO5RuUcFiAqLc);
        }
      }
      else
      {
        foreach (IRenderableSeries koh9jO5RuUcFiAqLc in koh9jO5RuUcFiAqLcList)
          this.\u0023\u003DzydxgB3sqmEwSB0XxJw\u003D\u003D.Remove(koh9jO5RuUcFiAqLc);
      }
    }
    Dictionary<object, IAnnotation> dictionary;
    if (!this._topChartElmentAnnotationMap.TryGetValue(ddznyiGmdRlAevOq, out dictionary))
      return;
    if (ddznyiGmdRlAevOq.TryGetXAxis() != null && ddznyiGmdRlAevOq.TryGetYAxis() != null)
    {
      foreach (KeyValuePair<object, IAnnotation> keyValuePair in dictionary)
      {
        if (!this.Annotations.Contains(keyValuePair.Value))
          this.Annotations.Add(keyValuePair.Value);
      }
    }
    else
    {
      foreach (KeyValuePair<object, IAnnotation> keyValuePair in dictionary)
        this.Annotations.Remove(keyValuePair.Value);
    }
  }

  private void \u0023\u003DzzBrCMQ6JtN57pKl5fw\u003D\u003D(
    object _param1,
    PropertyChangedEventArgs _param2)
  {
    if (!(_param2.PropertyName == "DrawStyle"))
      return;
    this.\u0023\u003Dzs0UfYK\u0024prvoZsynK2TcvH5w\u003D((IChartCandleElement) _param1);
  }

  private void \u0023\u003Dzs0UfYK\u0024prvoZsynK2TcvH5w\u003D(IChartCandleElement _param1)
  {
    this._rubberBandXyZoomModifier.IsXAxisOnly = !_param1.DrawStyle.IsVolumeProfileChart();
  }

  internal \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR \u0023\u003DzOALCA8UxYpqEXXXKxQ\u003D\u003D(
    string _param1)
  {
    IChartAxis chartAxis = ((IEnumerable<IChartAxis>) this.Area.XAxises).FirstOrDefault<IChartAxis>(new Func<IChartAxis, bool>(new ScichartSurfaceMVVM.\u0023\u003DzHtYFIEI9CJAk\u0024GXG5dtCtJE\u003D()
    {
      \u0023\u003DzABBG58cxsbcx = _param1
    }.\u0023\u003DzbSjDouuxMOmtENb1CI\u0024NfEXlBX\u0024_));
    return chartAxis == null ? (\u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR) null : \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR.\u0023\u003DzYMTYgq1xYsSy(this.\u0023\u003DzVNHlJWAgfVxe(), chartAxis.Group, this.PaneGroupSuffix, chartAxis.AxisType);
  }

  private void \u0023\u003Dzf5vVcayMq7_zxBOILQ\u003D\u003D()
  {
    CollectionHelper.ForEach<IChartAxis>((IEnumerable<IChartAxis>) this.Area.XAxises, ScichartSurfaceMVVM.SomeClass34343383.\u0023\u003DzI2W424KLtiP8cORvLQ\u003D\u003D ?? (ScichartSurfaceMVVM.SomeClass34343383.\u0023\u003DzI2W424KLtiP8cORvLQ\u003D\u003D = new Action<IChartAxis>(ScichartSurfaceMVVM.SomeClass34343383.SomeMethond0343.\u0023\u003DzN_\u0024B1DcFjYnDp0by4f0KlE4uU6RB)));
    CollectionHelper.ForEach<IChartAxis>((IEnumerable<IChartAxis>) this.Area.YAxises, ScichartSurfaceMVVM.SomeClass34343383.\u0023\u003Dz9Q2dCF9HzV_29jrgkQ\u003D\u003D ?? (ScichartSurfaceMVVM.SomeClass34343383.\u0023\u003Dz9Q2dCF9HzV_29jrgkQ\u003D\u003D = new Action<IChartAxis>(ScichartSurfaceMVVM.SomeClass34343383.SomeMethond0343.\u0023\u003DzWE\u0024cRelRNS67FHvvu3n4cW42dbOy)));
  }

  public string PaneGroupSuffix
  {
    get => this._paneGroupSuffix;
    set
    {
      value = value?.Trim() ?? string.Empty;
      if (!this.SetField<string>(ref this._paneGroupSuffix, value, nameof (PaneGroupSuffix)))
        return;
      this.NotifyChanged("PaneGroup");
      if (this.GroupChart == null)
        return;
      this.\u0023\u003Dz0FfA4J7ON5133\u00246jKg\u003D\u003D();
      \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vgE1y_16\u0024Ql8QLBV6E\u003D.SetMouseEventGroup((DependencyObject) this.ChartModifier, this.PaneGroup);
    }
  }

  public string PaneGroup
  {
    get
    {
      object[] objArray = new object[2];
      objArray[0] = (object) (this.GroupChart ?? throw new NotSupportedException()).GetInstanceCount();
      objArray[1] = (object) this._paneGroupSuffix;
      return StringHelper.Put("ssharpultrachart{0}_{1}", objArray);
    }
  }

  public IChart Chart => this.Area.Chart;

  public StockSharp.Xaml.Charting.Chart GroupChart => this.Chart as StockSharp.Xaml.Charting.Chart;

  public ChartArea Area => this._chartArea;

  public ChartViewModel ParentViewModel
  {
    get => this.GroupChart?.ViewModel;
  }

  public LegendModifierVM LegendViewModel
  {
    get
    {
      if (this.\u0023\u003DzrqALM6ELXjuaZXtSJw\u003D\u003D != null)
        return this.\u0023\u003DzrqALM6ELXjuaZXtSJw\u003D\u003D;
      this.\u0023\u003DzrqALM6ELXjuaZXtSJw\u003D\u003D = new LegendModifierVM(this)
      {
        LegendModifier = new LegendModifier()
      };
      this.\u0023\u003DzrqALM6ELXjuaZXtSJw\u003D\u003D.RemoveElementEvent(new Action<IChartElement>(this.\u0023\u003DzPtxNDyntmL6NTRG9Qk\u00249wEg\u003D));
      return this.\u0023\u003DzrqALM6ELXjuaZXtSJw\u003D\u003D;
    }
  }

  public dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd AnnotationModifier
  {
    get
    {
      dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd zIfS1UpijEycx = this.\u0023\u003DzIfS1UPijEYCX;
      if (zIfS1UpijEycx != null)
        return zIfS1UpijEycx;
      dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd g7AaupM52GhwgqEjd = new dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd(this.Area, this.Annotations);
      g7AaupM52GhwgqEjd.IsEnabled = false;
      dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd annotationModifier = g7AaupM52GhwgqEjd;
      this.\u0023\u003DzIfS1UPijEYCX = g7AaupM52GhwgqEjd;
      return annotationModifier;
    }
  }

  public IEnumerable<IRenderableSeries> ChartSeriesViewModels
  {
    get
    {
      return (IEnumerable<IRenderableSeries>) this.\u0023\u003DzydxgB3sqmEwSB0XxJw\u003D\u003D;
    }
  }

  public void AddAxisMakerAnnotation(
    IChartComponent _param1,
    IAnnotation _param2,
    object _param3)
  {
    if (_param3 == null)
      throw new ArgumentNullException("key");
    Dictionary<object, IAnnotation> dictionary;
    if (!this._topChartElmentAnnotationMap.TryGetValue(_param1, out dictionary))
      this._topChartElmentAnnotationMap[_param1] = dictionary = new Dictionary<object, IAnnotation>();
    dictionary.Add(_param3, _param2);
    if (_param1.TryGetXAxis() == null || _param1.TryGetYAxis() == null)
      return;
    this.Annotations.Add(_param2);
  }

  public IAnnotation \u0023\u003Dz70qTJ3tMR8Q7(
    IChartComponent _param1,
    object _param2)
  {
    Dictionary<object, IAnnotation> dictionary;
    return !this._topChartElmentAnnotationMap.TryGetValue(_param1, out dictionary) ? (IAnnotation) null : CollectionHelper.TryGetValue<object, IAnnotation>((IDictionary<object, IAnnotation>) dictionary, _param2);
  }

  public void RemoveAnnotation(
    IChartComponent _param1,
    object _param2)
  {
    Dictionary<object, IAnnotation> dictionary;
    if (!this._topChartElmentAnnotationMap.TryGetValue(_param1, out dictionary))
      return;
    if (_param2 == null)
    {
      foreach (KeyValuePair<object, IAnnotation> keyValuePair in dictionary)
        this.Annotations.Remove(keyValuePair.Value);
      this._topChartElmentAnnotationMap.Remove(_param1);
    }
    else
    {
      IAnnotation hhh93Q0DqkV5Sv90k;
      if (!dictionary.TryGetValue(_param2, out hhh93Q0DqkV5Sv90k))
        return;
      this.Annotations.Remove(hhh93Q0DqkV5Sv90k);
      dictionary.Remove(_param2);
    }
  }

  public void \u0023\u003DzBE5I4io\u003D(
    IChartComponent _param1,
    IRenderableSeries _param2)
  {
    List<IRenderableSeries> koh9jO5RuUcFiAqLcList;
    if (!this._chartUIRSeries.TryGetValue(_param1, out koh9jO5RuUcFiAqLcList))
      this._chartUIRSeries[_param1] = koh9jO5RuUcFiAqLcList = new List<IRenderableSeries>();
    if (!koh9jO5RuUcFiAqLcList.Contains(_param2))
      koh9jO5RuUcFiAqLcList.Add(_param2);
    this.OnXYAxisPropertyChanged((object) _param1, new PropertyChangedEventArgs("XAxisId"));
  }

  public void \u0023\u003Dzwh_e_TheVZKh(
    IChartComponent _param1)
  {
    List<IRenderableSeries> koh9jO5RuUcFiAqLcList;
    if (!this._chartUIRSeries.TryGetValue(_param1, out koh9jO5RuUcFiAqLcList))
      return;
    foreach (IRenderableSeries koh9jO5RuUcFiAqLc in koh9jO5RuUcFiAqLcList)
      this.\u0023\u003DzydxgB3sqmEwSB0XxJw\u003D\u003D.Remove(koh9jO5RuUcFiAqLc);
    this._chartUIRSeries.Remove(_param1);
  }

  public void \u0023\u003DzBCuJKIIaVAUt()
  {
    this._sciChartSurface?.InvalidateElement();
  }

  public \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUrpBxQ9IN4CBXg\u003D\u003D XAxises
  {
    get => this._xAxisNotifyList;
  }

  public \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUrpBxQ9IN4CBXg\u003D\u003D YAxises
  {
    get => this._yAxisNotifyList;
  }

  public ParentVM CandlesCompositeElement
  {
    get => this.\u0023\u003Dz9wKViCODW1Ii2tIsob2\u0024JVIp5Szh;
    private set => this.\u0023\u003Dz9wKViCODW1Ii2tIsob2\u0024JVIp5Szh = value;
  }

  public bool PaneHasCandles
  {
    get => this.\u0023\u003DziDb\u0024Ig3dGddOMOYevA\u003D\u003D;
    set
    {
      this.SetField<bool>(ref this.\u0023\u003DziDb\u0024Ig3dGddOMOYevA\u003D\u003D, value, nameof (PaneHasCandles));
    }
  }

  public \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D Annotations
  {
    get => this.\u0023\u003DzFDPZeiB0vr7jQ9JhGA\u003D\u003D;
  }

  public dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd ChartModifier
  {
    get
    {
      return this.\u0023\u003DzxkuwMuJ5gtvs ?? (this.\u0023\u003DzxkuwMuJ5gtvs = new dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd());
    }
    set
    {
      this.SetField<dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd>(ref this.\u0023\u003DzxkuwMuJ5gtvs, value, nameof (ChartModifier));
    }
  }

  public \u0023\u003DzsDU9XQyTsl2DjEg2HhKpBKWzr\u0024H44WFj\u0024d8XySFFwpGZ8_Hs5A\u003D\u003D ViewportManager
  {
    get => this.\u0023\u003Dz2wrgvLmSMxAy\u0024vli6w\u003D\u003D;
  }

  public LegendModifier LegendModifier
  {
    get => this.LegendViewModel.LegendModifier;
  }

  public string Title
  {
    get => this.Area.Title;
    set => this.Area.Title = value;
  }

  public string PerfStats
  {
    get => this.\u0023\u003DzZ5hHunO\u00246RUt;
    set
    {
      this.SetField<string>(ref this.\u0023\u003DzZ5hHunO\u00246RUt, value, nameof (PerfStats));
    }
  }

  public double Height
  {
    get => this._height;
    set
    {
      this.SetField<double>(ref this._height, value, nameof (Height));
      this.Area.Height = value;
    }
  }

  public bool ShowPerfStats
  {
    get
    {
      StockSharp.Xaml.Charting.Chart groupChart = this.GroupChart;
      return groupChart == null ? this._showPerfStats : __nonvirtual (groupChart.ShowPerfStats);
    }
    set
    {
      if (this.Chart is StockSharp.Xaml.Charting.Chart chart)
      {
        chart.ShowPerfStats = value;
        this.NotifyChanged(nameof (ShowPerfStats));
      }
      else
        this.SetField<bool>(ref this._showPerfStats, value, nameof (ShowPerfStats));
    }
  }

  public TimeSpan AutoRangeInterval
  {
    get
    {
      StockSharp.Xaml.Charting.Chart groupChart = this.GroupChart;
      return groupChart == null ? this._autoRangeIntervalNoGroup : groupChart.AutoRangeInterval;
    }
    set
    {
      if (this.Chart is StockSharp.Xaml.Charting.Chart chart)
      {
        chart.AutoRangeInterval = value;
        this.NotifyChanged(nameof (AutoRangeInterval));
      }
      else
        this.SetField<TimeSpan>(ref this._autoRangeIntervalNoGroup, value, nameof (AutoRangeInterval));
    }
  }

  public bool ShowLegend
  {
    get
    {
      ChartViewModel parentViewModel = this.ParentViewModel;
      return parentViewModel == null ? this.\u0023\u003Dz_BSxov_qh\u0024r8 : parentViewModel.ShowLegend;
    }
    set
    {
      if (this.ParentViewModel != null)
      {
        this.ParentViewModel.ShowLegend = value;
        this.NotifyChanged(nameof (ShowLegend));
      }
      else
        this.SetField<bool>(ref this.\u0023\u003Dz_BSxov_qh\u0024r8, value, nameof (ShowLegend));
    }
  }

  public bool ShowOverview
  {
    get
    {
      ChartViewModel parentViewModel = this.ParentViewModel;
      return parentViewModel == null ? this.\u0023\u003Dzp9yX6hrHRIBY : parentViewModel.ShowOverview;
    }
    set
    {
      if (this.ParentViewModel != null)
      {
        this.ParentViewModel.ShowOverview = value;
        this.NotifyChanged(nameof (ShowOverview));
      }
      else
        this.SetField<bool>(ref this.\u0023\u003Dzp9yX6hrHRIBY, value, nameof (ShowOverview));
    }
  }

  public int MinimumRange
  {
    get
    {
      ChartViewModel parentViewModel = this.ParentViewModel;
      return parentViewModel == null ? this._minimumRange : parentViewModel.MinimumRange;
    }
    set
    {
      if (this.ParentViewModel != null)
      {
        this.ParentViewModel.MinimumRange = value;
        this.NotifyChanged(nameof (MinimumRange));
      }
      else
        this.SetField<int>(ref this._minimumRange, value, nameof (MinimumRange));
    }
  }

  public string SelectedTheme
  {
    get => this.ParentViewModel?.SelectedTheme ?? this.\u0023\u003Dz0M9ae_JEb7EN;
    set
    {
      if (this.ParentViewModel != null)
      {
        this.ParentViewModel.SelectedTheme = value;
        this.NotifyChanged(nameof (SelectedTheme));
      }
      else
        this.SetField<string>(ref this.\u0023\u003Dz0M9ae_JEb7EN, value, nameof (SelectedTheme));
    }
  }

  public double DataPointWidth
  {
    get => this._dataPointWidth;
    set
    {
      this.SetField<double>(ref this._dataPointWidth, value, nameof (DataPointWidth));
    }
  }

  public ICommand ResetAxisTimeZoneCommand
  {
    get => this._resetAxisTimeZoneCommand;
    set
    {
      this.SetField<ICommand>(ref this._resetAxisTimeZoneCommand, value, nameof (ResetAxisTimeZoneCommand));
    }
  }

  public ICommand ClosePaneCommand
  {
    get => this._closePaneCommand;
    set => this._closePaneCommand = value;
  }

  public ICommand ShowHiddenAxesCommand => this._showHiddenAxesCommand;

  void IScichartSurfaceVM.\u0023\u003Dz5lCsXFbOXx7JLjkULVu7cxAjsq9e8Fm3w6E7VVQ\u003D()
  {
    throw new NotSupportedException();
  }

  private void ChangeApplicationTheme() => this.SelectedTheme = ChartHelper.CurrChartTheme();

  public void Dispose()
  {
    this._sciChartSurface?.Dispose();
    this._sciChartSurface = (SciChartSurface) null;
  }

  public bool AllowElementToBeRemoved( ParentVM _param1)
  {
    if (this.ParentViewModel == null || !this.ParentViewModel.IsInteracted)
      return false;
    bool flag;
    switch (_param1.ChartElement)
    {
      case IChartCandleElement _:
        flag = this.ParentViewModel.AllowAddCandles;
        break;
      case IChartIndicatorElement _:
        flag = this.ParentViewModel.AllowAddIndicators;
        break;
      case IChartOrderElement _:
        flag = this.ParentViewModel.AllowAddOrders;
        break;
      case IChartTradeElement _:
        flag = this.ParentViewModel.AllowAddOwnTrades;
        break;
      default:
        flag = false;
        break;
    }
    return flag;
  }

  private void \u0023\u003Dz2baZtMhFLPZu0vCzoJUPQZE\u003D(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    IChart chart = this.Chart;
    if (chart == null)
      return;
    chart.IsAutoRange = false;
  }

  private void \u0023\u003DzSH7LR2Lse3B4yzL0g_1pqV8\u003D(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    IChart chart = this.Chart;
    if (chart == null)
      return;
    chart.IsAutoRange = false;
  }

  private void \u0023\u003Dz5QuVGogKpyDd_u\u0024czKaNHcU\u003D(IChartCandleElement _param1)
  {
    this.\u0023\u003Dzs0UfYK\u0024prvoZsynK2TcvH5w\u003D(_param1);
    _param1.PropertyChanged += new PropertyChangedEventHandler(this.\u0023\u003DzzBrCMQ6JtN57pKl5fw\u003D\u003D);
  }

  private void \u0023\u003DzlzIC4G73INUgxBGJvcu\u0024bn8\u003D(
    DependencyPropertyChangedEventArgs _param1)
  {
    this.NotifyChanged("ShowLegend");
  }

  private void \u0023\u003DzQdV5fzQnxZUH8cqQCCGjyic\u003D(DependencyPropertyChangedEventArgs _param1)
  {
    this.NotifyChanged("ShowOverview");
  }

  private void \u0023\u003DzaPzrvWfKjELcmaG2lpOTZss\u003D(DependencyPropertyChangedEventArgs _param1)
  {
    this.NotifyChanged("MinimumRange");
  }

  private void \u0023\u003Dz\u0024BXtYBf7QC49I9Cu8SMYkgA\u003D(
    DependencyPropertyChangedEventArgs _param1)
  {
    this.NotifyChanged("SelectedTheme");
  }

  private void \u0023\u003DzQVqx9924gaWX1\u0024r5THddBi0\u003D(IChartAxis _param1)
  {
    this.\u0023\u003DzBUrMEb8\u003D(_param1, (ICollection<IAxis>) this.XAxises);
  }

  private void \u0023\u003DzhueGWJf3Qdd7bLHGZUE_sNY\u003D(IChartAxis _param1)
  {
    this.\u0023\u003DzBUrMEb8\u003D(_param1, (ICollection<IAxis>) this.YAxises);
  }

  private void \u0023\u003DzTdIpNSu812itYYHN7lIakb6BiejkaXWFgQ\u003D\u003D(
    ChartModifierBase _param1)
  {
    this.ChartModifier.ChildModifiers.Remove((IChartModifier) _param1);
  }

  private void \u0023\u003DzrFpAakDNl3igj4xxMoWpQFW0GE4J_yze\u0024g\u003D\u003D(IChartAxis _param1)
  {
    this.\u0023\u003Dz_FxkB6U\u003D(_param1, (ICollection<IAxis>) this.XAxises);
  }

  private void \u0023\u003DzGN9dmAg9QrR2Z7eHl8GIJrnf1RvCfrWhVA\u003D\u003D(IChartAxis _param1)
  {
    this.\u0023\u003Dz_FxkB6U\u003D(_param1, (ICollection<IAxis>) this.YAxises);
  }

  private UIChartBaseViewModel \u0023\u003DzMp9uGWCNEHILZjWmNYJHfvM\u003D(
    IDrawableChartElement _param1)
  {
    return _param1.CreateViewModel(this);
  }

  private UIChartBaseViewModel \u0023\u003DzufB92GEzbUD3B9FSng\u003D\u003D(
    IDrawableChartElement _param1)
  {
    return _param1.CreateViewModel(this);
  }

  private void \u0023\u003DzPtxNDyntmL6NTRG9Qk\u00249wEg\u003D(IChartElement _param1)
  {
    this.ParentViewModel?.\u0023\u003DzzXq5ccDMuPZc(_param1);
    Action<IChartElement> zeBeQvx4 = this.RemoveElementEvent;
    if (zeBeQvx4 == null)
      return;
    zeBeQvx4(_param1);
  }

  private sealed class \u0023\u003Dz0EETrg8PejletCT5YuMr1Rw\u003D
  {
    public Func<Order, bool> \u0023\u003DzaPd0W_M\u003D;
    public Func<\u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D, 
    #nullable enable
    IEnumerable<
    #nullable disable
    Order>> \u0023\u003DzoD2HtVGZvKav;

    internal 
    #nullable enable
    IEnumerable<
    #nullable disable
    Order> \u0023\u003Dz3ppSxBWUBHoE_OmJHdF4GT0\u003D(
      ParentVM _param1)
    {
      return _param1.Elements.OfType<\u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D>().SelectMany<\u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D, Order>(this.\u0023\u003DzoD2HtVGZvKav ?? (this.\u0023\u003DzoD2HtVGZvKav = new Func<\u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D, IEnumerable<Order>>(this.\u0023\u003Dz2Yv1Pu_wdWkdbu34jskJTTE\u003D)));
    }

    internal 
    #nullable enable
    IEnumerable<
    #nullable disable
    Order> \u0023\u003Dz2Yv1Pu_wdWkdbu34jskJTTE\u003D(
      \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D _param1)
    {
      return _param1.\u0023\u003DzQ\u0024gUWeEbsN2c(this.\u0023\u003DzaPd0W_M\u003D);
    }
  }

  private sealed class \u0023\u003Dz5NCozMpa4F4mcyQlZrFJfRI\u003D
  {
    public ScichartSurfaceMVVM _variableSome3535;
    public IChartAxis \u0023\u003Dzfl\u0024A1s0\u003D;
    public ICollection<IAxis> \u0023\u003Dz_liTKnA\u003D;

    internal void \u0023\u003DzFX_lHxlPjn56eMHQ\u0024g\u003D\u003D()
    {
      if (this._variableSome3535.Chart == null)
        return;
      dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd axF9ZgQ7NbH9KsEjd = this.\u0023\u003Dzfl\u0024A1s0\u003D.\u0023\u003Dz68iph80\u003D(this._variableSome3535.ParentViewModel?.RemoveAxisCommand, this._variableSome3535.ResetAxisTimeZoneCommand, this._variableSome3535.Chart);
      axF9ZgQ7NbH9KsEjd.PropertyChanged += new PropertyChangedEventHandler(this._variableSome3535.\u0023\u003Dzfi9Y8f8VaR3y);
      this.\u0023\u003Dz_liTKnA\u003D.Add((IAxis) axF9ZgQ7NbH9KsEjd);
      if (this.\u0023\u003Dz_liTKnA\u003D != this._variableSome3535.XAxises)
        return;
      this._variableSome3535.\u0023\u003Dz0FfA4J7ON5133\u00246jKg\u003D\u003D();
    }
  }

  [Serializable]
  private new sealed class SomeClass34343383
  {
    public static readonly ScichartSurfaceMVVM.SomeClass34343383 SomeMethond0343 = new ScichartSurfaceMVVM.SomeClass34343383();
    public static Action<ChartAxis> \u0023\u003Dzfa4InO8BG_qfj7v\u0024gA\u003D\u003D;
    public static Func<ParentVM, bool> \u0023\u003DzZ1TIJti3dLv69swWwQ\u003D\u003D;
    public static Action<dje_zP5SLCZMPLKRDSVWETEPWLMZPT4N45VSYZ76M5M7C6J68NU9622VFYDAYPDEQ_ejd> \u0023\u003DzrDW3kcvb8jweTamfSw\u003D\u003D;
    public static Action<ParentVM> \u0023\u003DzHf64i7sS_m0gAJCSlg\u003D\u003D;
    public static Func<IRenderableSeries, bool> \u0023\u003DzOkZpX_jNLPK4\u0024VqKDw\u003D\u003D;
    public static Func<IRenderableSeries, int> \u0023\u003DzAMUmAPxCMMfLz0aKiA\u003D\u003D;
    public static Func<KeyValuePair<IChartComponent, ParentVM>, bool> \u0023\u003Dz8slTl9RRXzpBYOxh4Q\u003D\u003D;
    public static Func<KeyValuePair<IChartComponent, ParentVM>, IChartComponent> \u0023\u003DzG9p0UKsG3FcNaICZMQ\u003D\u003D;
    public static Func<IDrawableChartElement, bool> \u0023\u003DzrUb4sQiSyo1cFneMgA\u003D\u003D;
    public static Func<UIChartBaseViewModel, bool> \u0023\u003DzKOhc8XoZrBjayO3KMw\u003D\u003D;
    public static Func<KeyValuePair<IChartComponent, ParentVM>, bool> \u0023\u003DzoANln2qVkbS_vrdROw\u003D\u003D;
    public static Func<IDrawableChartElement, bool> \u0023\u003Dz9cuH_vUOZn8pPBtNwQ\u003D\u003D;
    public static Func<UIChartBaseViewModel, bool> \u0023\u003DzesHJJr9lkjwYzxQD2g\u003D\u003D;
    public static Action<IChartAxis> \u0023\u003DzI2W424KLtiP8cORvLQ\u003D\u003D;
    public static Action<IChartAxis> \u0023\u003Dz9Q2dCF9HzV_29jrgkQ\u003D\u003D;

    internal void \u0023\u003DzB6IuSPwhBuqMEjjiXTaDcdI\u003D(ChartAxis _param1)
    {
      _param1.TimeZone = (TimeZoneInfo) null;
    }

    internal bool \u0023\u003DzrQJFhnLMBw4KLdFbErEk\u0024Eju6p\u00242(
      ParentVM _param1)
    {
      return _param1 != null;
    }

    internal void \u0023\u003DzJR2NRNOjWD80Ydt_1RhppIXJFkoJ(
      dje_zP5SLCZMPLKRDSVWETEPWLMZPT4N45VSYZ76M5M7C6J68NU9622VFYDAYPDEQ_ejd _param1)
    {
      if (!(_param1.Tag is ChartAxis tag))
        return;
      tag.DataPointWidth = _param1.CurrentDatapointPixelSize;
    }

    internal void \u0023\u003DzpfilwLTVeZDaJUeDuSBVjV3PZ4yR(
      ParentVM _param1)
    {
      _param1.UpdateYAxisMarker();
    }

    internal bool \u0023\u003DzxrTS_1cNsfx\u0024XLYpPvCkhwAicVIY(
      IRenderableSeries _param1)
    {
      return _param1.IsVisible;
    }

    internal int \u0023\u003DzXPeSs80bTGyLZbXyn71KBILsfXa3(
      IRenderableSeries _param1)
    {
      \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D dataSeries = _param1.get_DataSeries();
      return dataSeries == null ? 0 : dataSeries.get_Count();
    }

    internal bool \u0023\u003DzFUwEsnQgb3v8tcGvhyZ1DIc\u003D(
      KeyValuePair<IChartComponent, ParentVM> _param1)
    {
      return _param1.Value == null;
    }

    internal IChartComponent \u0023\u003DzkaiqO59tHfX5w4slAysHOio\u003D(
      KeyValuePair<IChartComponent, ParentVM> _param1)
    {
      return _param1.Key;
    }

    internal bool \u0023\u003DzihU1OXxWIdi3ASgjwqQi3aM\u003D(
      IDrawableChartElement _param1)
    {
      return !_param1.DontDraw;
    }

    internal bool \u0023\u003DzDP46TP6OWYLlok7K1AlOaIg\u003D(
      UIChartBaseViewModel _param1)
    {
      return _param1 != null;
    }

    internal bool \u0023\u003DzOxutfFQq8iOlG6uGIIvjfQjn_lrYDZf0Gw\u003D\u003D(
      KeyValuePair<IChartComponent, ParentVM> _param1)
    {
      return _param1.Key is IChartCandleElement;
    }

    internal bool \u0023\u003DzZItGYEiEFtH3fOWMCA\u003D\u003D(
      IDrawableChartElement _param1)
    {
      return !_param1.DontDraw;
    }

    internal bool \u0023\u003DzmUdptvPaqZAvZEWcjQ\u003D\u003D(
      UIChartBaseViewModel _param1)
    {
      return _param1 != null;
    }

    internal void \u0023\u003DzN_\u0024B1DcFjYnDp0by4f0KlE4uU6RB(IChartAxis _param1)
    {
      _param1.IsVisible = true;
    }

    internal void \u0023\u003DzWE\u0024cRelRNS67FHvvu3n4cW42dbOy(IChartAxis _param1)
    {
      _param1.IsVisible = true;
    }
  }

  private sealed class \u0023\u003DzHtYFIEI9CJAk\u0024GXG5dtCtJE\u003D
  {
    public string \u0023\u003DzABBG58cxsbcx;

    internal bool \u0023\u003DzbSjDouuxMOmtENb1CI\u0024NfEXlBX\u0024_(IChartAxis _param1)
    {
      return _param1.Id == this.\u0023\u003DzABBG58cxsbcx;
    }
  }

  private sealed class ChartComponentParentVM :  CachedSynchronizedDictionary<IChartComponent, ParentVM>
  {
    
    private ParentVM[] \u0023\u003DzlS7dPxLH4yBm;

    public ParentVM[] \u0023\u003DzcMDdc03rjKXr()
    {
      lock (((SynchronizedDictionary<IChartComponent, ParentVM>) this).SyncRoot)
      {
        ParentVM[] a4VgOpCeDiqsTdzBArray = this.\u0023\u003DzlS7dPxLH4yBm;
        if (a4VgOpCeDiqsTdzBArray == null)
        {
          List<ParentVM> a4VgOpCeDiqsTdzBList = new List<ParentVM>();
          a4VgOpCeDiqsTdzBList.AddRange(((IEnumerable<KeyValuePair<IChartComponent, ParentVM>>) this).OrderBy<KeyValuePair<IChartComponent, ParentVM>, int>(ScichartSurfaceMVVM.ChartComponentParentVM.SomeClass34343383.\u0023\u003DzxLUmJTOQFZstXBxKkg\u003D\u003D ?? (ScichartSurfaceMVVM.ChartComponentParentVM.SomeClass34343383.\u0023\u003DzxLUmJTOQFZstXBxKkg\u003D\u003D = new Func<KeyValuePair<IChartComponent, ParentVM>, int>(ScichartSurfaceMVVM.ChartComponentParentVM.SomeClass34343383.SomeMethond0343.\u0023\u003DzfUnDYqsr\u0024Ej5bAJruqHN2sI\u003D))).Select<KeyValuePair<IChartComponent, ParentVM>, ParentVM>(ScichartSurfaceMVVM.ChartComponentParentVM.SomeClass34343383.\u0023\u003Dz4cfR9WtX1fuK0AziEg\u003D\u003D ?? (ScichartSurfaceMVVM.ChartComponentParentVM.SomeClass34343383.\u0023\u003Dz4cfR9WtX1fuK0AziEg\u003D\u003D = new Func<KeyValuePair<IChartComponent, ParentVM>, ParentVM>(ScichartSurfaceMVVM.ChartComponentParentVM.SomeClass34343383.SomeMethond0343.\u0023\u003DzbT5EZUEYMyUAzx5gAaXZNrw\u003D))));
          a4VgOpCeDiqsTdzBArray = this.\u0023\u003DzlS7dPxLH4yBm = a4VgOpCeDiqsTdzBList.ToArray();
        }
        return a4VgOpCeDiqsTdzBArray;
      }
    }

    protected virtual void OnResetCache(bool _param1)
    {
      base.OnResetCache(_param1);
      this.\u0023\u003DzlS7dPxLH4yBm = (ParentVM[]) null;
    }

    [Serializable]
    private sealed class SomeClass34343383
    {
      public static readonly ScichartSurfaceMVVM.ChartComponentParentVM.SomeClass34343383 SomeMethond0343 = new ScichartSurfaceMVVM.ChartComponentParentVM.SomeClass34343383();
      public static Func<KeyValuePair<IChartComponent, ParentVM>, int> \u0023\u003DzxLUmJTOQFZstXBxKkg\u003D\u003D;
      public static Func<KeyValuePair<IChartComponent, ParentVM>, ParentVM> \u0023\u003Dz4cfR9WtX1fuK0AziEg\u003D\u003D;

      internal int \u0023\u003DzfUnDYqsr\u0024Ej5bAJruqHN2sI\u003D(
        KeyValuePair<IChartComponent, ParentVM> _param1)
      {
        return _param1.Key.Priority;
      }

      internal ParentVM \u0023\u003DzbT5EZUEYMyUAzx5gAaXZNrw\u003D(
        KeyValuePair<IChartComponent, ParentVM> _param1)
      {
        return _param1.Value;
      }
    }
  }

  private sealed class \u0023\u003DzM\u0024TlJy0mx0yCUWlqEsh0ZdY\u003D
  {
    public ICollection<IAxis> \u0023\u003Dz_liTKnA\u003D;
    public IChartAxis \u0023\u003Dzfl\u0024A1s0\u003D;
    public ScichartSurfaceMVVM _variableSome3535;
    public Func<IAxis, bool> \u0023\u003DzuAeZVTPDgzYE;

    internal void \u0023\u003DzgPfL2cm03IfPSYpk8w\u003D\u003D()
    {
      dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd target = (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd) this.\u0023\u003Dz_liTKnA\u003D.FirstOrDefault<IAxis>(this.\u0023\u003DzuAeZVTPDgzYE ?? (this.\u0023\u003DzuAeZVTPDgzYE = new Func<IAxis, bool>(this.\u0023\u003DzZ4z4dhLl82s4_UYFOQ\u003D\u003D)));
      if (target == null)
        return;
      target.PropertyChanged -= new PropertyChangedEventHandler(this._variableSome3535.\u0023\u003Dzfi9Y8f8VaR3y);
      BindingOperations.ClearAllBindings((DependencyObject) target);
      this.\u0023\u003Dz_liTKnA\u003D.Remove((IAxis) target);
    }

    internal bool \u0023\u003DzZ4z4dhLl82s4_UYFOQ\u003D\u003D(
      IAxis _param1)
    {
      return _param1.Id == this.\u0023\u003Dzfl\u0024A1s0\u003D.Id;
    }
  }

  private sealed class SomeClass6409
  {
    public ScichartSurfaceMVVM _variableSome3535;
    public ChartArea \u0023\u003Dzy_5REws\u003D;
    public Action<IChartElement> \u0023\u003DzmkzpZPDRopSK;

    internal bool \u0023\u003Dzk0q47haYd4sk7HJcWw\u003D\u003D(int _param1)
    {
      return this._variableSome3535.\u0023\u003DzmxDTmQc\u003D(((IList<IChartElement>) this.\u0023\u003Dzy_5REws\u003D.Elements)[_param1]);
    }

    internal bool \u0023\u003DzvwGmGW0txy637Xwc2g\u003D\u003D()
    {
      CollectionHelper.ForEach<IChartElement>((IEnumerable<IChartElement>) this.\u0023\u003Dzy_5REws\u003D.Elements, this.\u0023\u003DzmkzpZPDRopSK ?? (this.\u0023\u003DzmkzpZPDRopSK = new Action<IChartElement>(this.\u0023\u003Dzr5cnmYeOtMU0QUibOQ\u003D\u003D)));
      return true;
    }

    internal void \u0023\u003Dzr5cnmYeOtMU0QUibOQ\u003D\u003D(IChartElement _param1)
    {
      this._variableSome3535.\u0023\u003DzmxDTmQc\u003D(_param1);
    }

    internal void \u0023\u003DzzTy9O8Bfu2Mtf5LgWw\u003D\u003D(IChartAxis _param1)
    {
      this._variableSome3535.\u0023\u003DzBUrMEb8\u003D(_param1, (ICollection<IAxis>) this._variableSome3535.XAxises);
    }

    internal bool \u0023\u003DzNMuSlzFThmIy12t4iw\u003D\u003D(IChartAxis _param1)
    {
      return this._variableSome3535.\u0023\u003Dz_FxkB6U\u003D(_param1, (ICollection<IAxis>) this._variableSome3535.XAxises);
    }

    internal bool \u0023\u003DzFhH1jy_AOH_jnpgLnA\u003D\u003D(int _param1)
    {
      return this._variableSome3535.\u0023\u003Dz_FxkB6U\u003D(((IList<IChartAxis>) this.\u0023\u003Dzy_5REws\u003D.XAxises)[_param1], (ICollection<IAxis>) this._variableSome3535.XAxises);
    }

    internal void \u0023\u003DzdMV7Kr3bkeboUtRdSw\u003D\u003D(IChartAxis _param1)
    {
      this._variableSome3535.\u0023\u003DzBUrMEb8\u003D(_param1, (ICollection<IAxis>) this._variableSome3535.YAxises);
    }

    internal bool SomeInternalVoidFunction30439(IChartAxis _param1)
    {
      return this._variableSome3535.\u0023\u003Dz_FxkB6U\u003D(_param1, (ICollection<IAxis>) this._variableSome3535.YAxises);
    }

    internal bool \u0023\u003Dzn0BxqFlrvEnaJ4F1tg\u003D\u003D(int _param1)
    {
      return this._variableSome3535.\u0023\u003Dz_FxkB6U\u003D(((IList<IChartAxis>) this.\u0023\u003Dzy_5REws\u003D.YAxises)[_param1], (ICollection<IAxis>) this._variableSome3535.YAxises);
    }

    internal void \u0023\u003DzSf0sPwNIL9V4tJHvwQ\u003D\u003D(
      DependencyObject _param1,
      ThemeChangedRoutedEventArgs _param2)
    {
      this._variableSome3535.ChangeApplicationTheme();
    }
  }
}
