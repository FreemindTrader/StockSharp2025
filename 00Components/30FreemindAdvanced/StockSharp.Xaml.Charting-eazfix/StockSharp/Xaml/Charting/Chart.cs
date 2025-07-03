// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Chart
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using DevExpress.Xpf.Grid;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Configuration;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Threading;

#nullable enable
namespace StockSharp.Xaml.Charting;

public class Chart : 
  UserControl,
  INotifyPropertyChanged,
  IChart,
  IPersistable,
  IComponentConnector,
  IChartBuilder,
  IThemeableChart,
  INotifyPropertyChangedEx,
  IWpfChart
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private static int \u0023\u003DzOuojlfHTX4K6;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly int \u0023\u003Dzt3ZlrZOO2ledE5D1Mw\u003D\u003D = ++Chart.\u0023\u003DzOuojlfHTX4K6;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly 
  #nullable disable
  ChartBuilder \u0023\u003DzQedEwiM\u003D = new ChartBuilder();
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Subscription \u0023\u003DzKCT2CVDV16nJ;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private ChartCandleDrawStyles \u0023\u003DzwXDAJOwaveOju\u0024lre38VQ5w\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private MarketDataMessage \u0023\u003DzbEOGQO_Gw5r5YQ6kr3Jalls\u003D = new MarketDataMessage()
  {
    DataType2 = Extensions.TimeFrame(TimeSpan.FromMinutes(5.0))
  };
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly \u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6 _scichartSurfaceMVVM;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly SynchronizedDictionary<IChartIndicatorElement, Chart.\u0023\u003DzZQ9Hpf12oRwg> \u0023\u003Dz\u0024IReF1JUpHYmGa845Q\u003D\u003D = new SynchronizedDictionary<IChartIndicatorElement, Chart.\u0023\u003DzZQ9Hpf12oRwg>();
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly SynchronizedDictionary<IChartElement, Subscription> \u0023\u003DzvWHSaOs\u003D = new SynchronizedDictionary<IChartElement, Subscription>();
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private ChartAnnotationTypes \u0023\u003DzdrtI10riHGVN;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzRQNCutnxzurB = true;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzKSdoqGGT2Q0s17vWLQ\u003D\u003D = true;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003Dzi\u0024DHUvHXaw26IYcmlA\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzwuVzD4GYBhfk = true;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DznNCcb03RWapN;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003Dz_hnUMt3_OcvB;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DznXh8y9EGbQ7R6xXCKw\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzL6RNvETlqFx4;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly Dictionary<(IChartArea, IndicatorMeasures), string> \u0023\u003DzDUXY6MNzkE1e = new Dictionary<(IChartArea, IndicatorMeasures), string>();
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private TimeSpan \u0023\u003Dz9Ee3umvrluGn = TimeSpan.FromMilliseconds(200.0);
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly List<IChartArea> \u0023\u003Dza1mnh6ythHbd = new List<IChartArea>();
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private ISecurityProvider \u0023\u003DzopaHcTq5\u0024VAJ1U\u0024FZA\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzBUE9gFoni2x4vnoaWAL_8Q8\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private TimeZoneInfo _timeZone = TimeZoneInfo.Local;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzEpShEnHWfpnt9EgGG53qksn4J8ln8AfdcBAf0gw\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Security \u0023\u003DzCeIks\u0024kpJtyCA0n_Hg\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Subscription \u0023\u003DzAFpdFZRw72NT1DPyxQ\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Action<IChartArea> \u0023\u003DzcHtgn6mNhxMM;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Action \u0023\u003DzKGKj0Lc\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private PropertyChangedEventHandler PropertyChangedEvent;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  internal dje_zCNSBRP7GPP9KUVX64FWNJCMERJEQNDDYNP68U92NYNLX7ULAMWXZ2_ejd \u0023\u003DzxYLKFqWiCEs\u0024;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzQGCmQMjHdLKS;

  public Chart()
  {
    Chart.\u0023\u003DzRgOqXl_UCNpCcsODKLfs7Ks\u003D ucNpCcsOdkLfs7Ks = new Chart.\u0023\u003DzRgOqXl_UCNpCcsODKLfs7Ks\u003D();
    ucNpCcsOdkLfs7Ks.\u0023\u003DzRRvwDu67s9Rm = this;
    this.InitializeComponent();
    this.DataContext = (object) (this._scichartSurfaceMVVM = new \u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6());
    this.ViewModel().\u0023\u003DzgNd4ReliYq4x(new Action<Order>(this.\u0023\u003DzrMNjBJFuBLP3));
    this.ViewModel().\u0023\u003DzqMcw8k8QHzu3(new Action<ChartArea>(this.\u0023\u003DzYhCvVp5ZsuEOxZPGgrZ6vLQ\u003D));
    this.AreaAdding += new Action(this.\u0023\u003DzA\u0024Wg8llSgKSoO3igMVHuEmE\u003D);
    this.AddCandles += new Action<ChartArea>(this.\u0023\u003DzlPurC69wnp3kb0Gv9WCPMD0\u003D);
    this.RebuildCandles += new Action<IChartElement, Subscription>(this.\u0023\u003DzBU1c7i4ydZiUB8ivBA\u003D\u003D);
    this.AddIndicator += new Action<ChartArea>(this.\u0023\u003DziEqlaww8py20vRuZvdjcMwM\u003D);
    this.AddOrders += new Action<ChartArea>(this.\u0023\u003DzUc2ksk6USZ7hJX6w86ULnVg\u003D);
    this.AddTrades += new Action<ChartArea>(this.\u0023\u003DzsXLt0R2oLUokWH956n5iSfc\u003D);
    this.RemoveElement += new Action<IChartElement>(this.\u0023\u003Dz\u0024_wDItQvnYOy);
    ucNpCcsOdkLfs7Ks.\u0023\u003DzgE4mCHaKpzj3mF7k57tWsbQ\u003D = new string[7]
    {
      nameof (IsInteracted),
      nameof (AllowAddArea),
      nameof (AllowAddAxis),
      nameof (AllowAddCandles),
      nameof (AllowAddIndicators),
      nameof (AllowAddOrders),
      nameof (AllowAddOwnTrades)
    };
    \u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DztwqF4KBjQLI4w4fkq\u0024UNEzaV82mj(new Action(ucNpCcsOdkLfs7Ks.\u0023\u003DzYqlPx6RFcqzqQlN8KA\u003D\u003D));
    if (IChartExtensions.TryIndicatorProvider == null)
    {
      IndicatorProvider indicatorProvider = new IndicatorProvider();
      indicatorProvider.Init();
      ConfigManager.RegisterService<IIndicatorProvider>((IIndicatorProvider) indicatorProvider);
    }
    if (IChartExtensions.TryIndicatorPainterProvider != null)
      return;
    ChartIndicatorPainterProvider indicatorPainterProvider = new ChartIndicatorPainterProvider();
    ((IChartIndicatorPainterProvider) indicatorPainterProvider).Init();
    ConfigManager.RegisterService<IChartIndicatorPainterProvider>((IChartIndicatorPainterProvider) indicatorPainterProvider);
  }

  internal int \u0023\u003DzSnq_a37UvF_W() => this.\u0023\u003Dzt3ZlrZOO2ledE5D1Mw\u003D\u003D;

  internal ChartCandleDrawStyles \u0023\u003Dzv9T9127oaRiK()
  {
    return this.\u0023\u003DzwXDAJOwaveOju\u0024lre38VQ5w\u003D;
  }

  internal void \u0023\u003DzvqeJB240RXk9(ChartCandleDrawStyles _param1)
  {
    this.\u0023\u003DzwXDAJOwaveOju\u0024lre38VQ5w\u003D = _param1;
  }

  public MarketDataMessage DefaultCandlesSettings
  {
    get => this.\u0023\u003DzbEOGQO_Gw5r5YQ6kr3Jalls\u003D;
    set
    {
      this.\u0023\u003DzbEOGQO_Gw5r5YQ6kr3Jalls\u003D = value ?? throw new ArgumentNullException(nameof (value));
    }
  }

  internal \u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6 ViewModel()
  {
    return this._scichartSurfaceMVVM;
  }

  public IEnumerable<IChartArea> Areas => (IEnumerable<IChartArea>) this.\u0023\u003Dza1mnh6ythHbd;

  public bool IsAutoScroll
  {
    get => this.\u0023\u003DzRQNCutnxzurB;
    set
    {
      this.\u0023\u003DzRQNCutnxzurB = value;
      NotifyPropertyChangedExHelper.Notify<Chart>(this, nameof (IsAutoScroll));
    }
  }

  public bool IsAutoRange
  {
    get => this.\u0023\u003Dz_hnUMt3_OcvB;
    set
    {
      this.\u0023\u003Dz_hnUMt3_OcvB = value;
      foreach (IChartArea area in this.Areas)
        area.SetAutoRange(value);
      NotifyPropertyChangedExHelper.Notify<Chart>(this, nameof (IsAutoRange));
    }
  }

  public TimeSpan AutoRangeInterval
  {
    get => this.\u0023\u003Dz9Ee3umvrluGn;
    set
    {
      this.\u0023\u003Dz9Ee3umvrluGn = !(value <= TimeSpan.Zero) ? value : throw new ArgumentOutOfRangeException(nameof (value), (object) value, LocalizedStrings.InvalidValue);
      NotifyPropertyChangedExHelper.Notify<Chart>(this, nameof (AutoRangeInterval));
    }
  }

  public ISecurityProvider SecurityProvider
  {
    get => this.\u0023\u003DzopaHcTq5\u0024VAJ1U\u0024FZA\u003D\u003D;
    set => this.\u0023\u003DzopaHcTq5\u0024VAJ1U\u0024FZA\u003D\u003D = value;
  }

  public bool DisableIndicatorReset
  {
    get => this.\u0023\u003DzBUE9gFoni2x4vnoaWAL_8Q8\u003D;
    set => this.\u0023\u003DzBUE9gFoni2x4vnoaWAL_8Q8\u003D = value;
  }

  public void AddArea(IChartArea area)
  {
    ((DispatcherObject) this).GuiSync(new Action(new Chart.\u0023\u003DzLeQepc5PMlUh44ZxsNBznec\u003D()
    {
      \u0023\u003Dzy_5REws\u003D = area,
      \u0023\u003DzRRvwDu67s9Rm = this
    }.\u0023\u003DzTUl6zvo_PmQZ4TJ2ew\u003D\u003D));
  }

  public void RemoveArea(IChartArea area)
  {
    ((DispatcherObject) this).GuiSync(new Action(new Chart.\u0023\u003DzwNBM8yU0t_ER1Neix6Hn12Q\u003D()
    {
      \u0023\u003DzRRvwDu67s9Rm = this,
      \u0023\u003Dzy_5REws\u003D = area
    }.\u0023\u003DzJiDIwzXuOcws4Nsgtg\u003D\u003D));
  }

  private void \u0023\u003DzqfFsxob3ngDX(object _param1, PropertyChangedEventArgs _param2)
  {
    ChartArea chartArea = (ChartArea) _param1;
    if (!(_param2.PropertyName == "Height"))
      return;
    chartArea.ViewModel().Height = chartArea.Height;
  }

  public event Action<IChartArea> AreaAdded;

  public event Action<IChartArea> AreaRemoved;

  public void AddElement(IChartArea area, IChartElement element)
  {
    Chart.\u0023\u003DzHeEbGncgo1okL0HgWiq5JrU\u003D gncgo1okL0HgWiq5JrU = new Chart.\u0023\u003DzHeEbGncgo1okL0HgWiq5JrU\u003D();
    gncgo1okL0HgWiq5JrU.\u0023\u003Dz_i6sZDg\u003D = element;
    gncgo1okL0HgWiq5JrU.\u0023\u003DzRRvwDu67s9Rm = this;
    gncgo1okL0HgWiq5JrU.\u0023\u003Dzy_5REws\u003D = area;
    if (gncgo1okL0HgWiq5JrU.\u0023\u003Dzy_5REws\u003D == null)
      throw new ArgumentNullException(nameof (area));
    if (gncgo1okL0HgWiq5JrU.\u0023\u003Dz_i6sZDg\u003D == null)
      throw new ArgumentNullException(nameof (element));
    ((DispatcherObject) this).GuiSync(new Action(gncgo1okL0HgWiq5JrU.\u0023\u003Dzo0PvKPRyMKTdyL4L\u0024A\u003D\u003D));
  }

  public void AddElement(IChartArea area, IChartCandleElement element, Subscription subscription)
  {
    if (area == null)
      throw new ArgumentNullException(nameof (area));
    if (element == null)
      throw new ArgumentNullException(nameof (element));
    if (subscription == null)
      throw new ArgumentNullException(nameof (subscription));
    this.\u0023\u003DzvWHSaOs\u003D.Add((IChartElement) element, subscription);
    this.AddElement(area, (IChartElement) element);
  }

  public void AddElement(
    IChartArea area,
    IChartIndicatorElement element,
    Subscription subscription,
    IIndicator indicator)
  {
    if (area == null)
      throw new ArgumentNullException(nameof (area));
    if (element == null)
      throw new ArgumentNullException(nameof (element));
    if (indicator == null)
      throw new ArgumentNullException(nameof (indicator));
    if (subscription != null)
      this.\u0023\u003DzvWHSaOs\u003D.Add((IChartElement) element, subscription);
    this.\u0023\u003Dz\u0024IReF1JUpHYmGa845Q\u003D\u003D.Add(element, new Chart.\u0023\u003DzZQ9Hpf12oRwg(this, element, indicator));
    ((ChartIndicatorElement) element).\u0023\u003Dz2Afk71t1OoFdU8tQ4Q\u003D\u003D(this.IndicatorTypes, indicator);
    this.AddElement(area, (IChartElement) element);
  }

  public void AddElement(IChartArea area, IChartOrderElement element, Subscription subscription)
  {
    if (area == null)
      throw new ArgumentNullException(nameof (area));
    if (element == null)
      throw new ArgumentNullException(nameof (element));
    if (subscription == null)
      throw new ArgumentNullException(nameof (subscription));
    this.\u0023\u003DzvWHSaOs\u003D.Add((IChartElement) element, subscription);
    this.AddElement(area, (IChartElement) element);
  }

  public void AddElement(IChartArea area, IChartTradeElement element, Subscription subscription)
  {
    if (area == null)
      throw new ArgumentNullException(nameof (area));
    if (element == null)
      throw new ArgumentNullException(nameof (element));
    if (subscription == null)
      throw new ArgumentNullException(nameof (subscription));
    this.\u0023\u003DzvWHSaOs\u003D.Add((IChartElement) element, subscription);
    this.AddElement(area, (IChartElement) element);
  }

  void IChart.RemoveElement(IChartArea area, IChartElement element)
  {
    Chart.\u0023\u003Dzm_oWpvJyDZiF55m7JqXuwJc\u003D jyDziF55m7JqXuwJc = new Chart.\u0023\u003Dzm_oWpvJyDZiF55m7JqXuwJc\u003D();
    jyDziF55m7JqXuwJc.\u0023\u003Dzy_5REws\u003D = area;
    jyDziF55m7JqXuwJc.\u0023\u003Dz_i6sZDg\u003D = element;
    if (jyDziF55m7JqXuwJc.\u0023\u003Dzy_5REws\u003D == null)
      throw new ArgumentNullException(nameof (area));
    if (jyDziF55m7JqXuwJc.\u0023\u003Dz_i6sZDg\u003D == null)
      throw new ArgumentNullException(nameof (element));
    Chart.\u0023\u003DzZQ9Hpf12oRwg zZq9Hpf12oRwg;
    if (jyDziF55m7JqXuwJc.\u0023\u003Dz_i6sZDg\u003D is IChartIndicatorElement zI6sZdg && this.\u0023\u003Dz\u0024IReF1JUpHYmGa845Q\u003D\u003D.TryGetValue(zI6sZdg, ref zZq9Hpf12oRwg))
    {
      zZq9Hpf12oRwg.Dispose();
      this.\u0023\u003Dz\u0024IReF1JUpHYmGa845Q\u003D\u003D.Remove(zI6sZdg);
    }
    ((DispatcherObject) this).GuiSync<bool>(new Func<bool>(jyDziF55m7JqXuwJc.\u0023\u003DzRy89s7w8wbPZi21A_M2JlLqW05mnmhs8PApgiRA\u003D));
    this.\u0023\u003DzvWHSaOs\u003D.Remove(jyDziF55m7JqXuwJc.\u0023\u003Dz_i6sZDg\u003D);
  }

  public IIndicator GetIndicator(IChartIndicatorElement element)
  {
    return CollectionHelper.TryGetValue<IChartIndicatorElement, Chart.\u0023\u003DzZQ9Hpf12oRwg>((IDictionary<IChartIndicatorElement, Chart.\u0023\u003DzZQ9Hpf12oRwg>) this.\u0023\u003Dz\u0024IReF1JUpHYmGa845Q\u003D\u003D, element)?.Indicator;
  }

  public Subscription TryGetSubscription(IChartElement element)
  {
    return CollectionHelper.TryGetValue<IChartElement, Subscription>((IDictionary<IChartElement, Subscription>) this.\u0023\u003DzvWHSaOs\u003D, element);
  }

  private (IChartCandleElement, Subscription) \u0023\u003DzmqXWWh6oQVIEJrU2Pw\u003D\u003D()
  {
    foreach (IChartElement chartElement in this.Areas.SelectMany<IChartArea, IChartElement>(Chart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzv\u002455fO77Mn0JronfvA\u003D\u003D ?? (Chart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzv\u002455fO77Mn0JronfvA\u003D\u003D = new Func<IChartArea, IEnumerable<IChartElement>>(Chart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzPJeLPTCgYwdmBIJASFbIA_hFSy_8))))
    {
      if (chartElement is IChartCandleElement element)
      {
        Subscription subscription = this.TryGetSubscription((IChartElement) element);
        if ((subscription != null ? (!subscription.SecurityId.HasValue ? 1 : 0) : 1) == 0)
          return (element, subscription);
      }
    }
    return ();
  }

  private void \u0023\u003Dz1jTUhLCjcbWI()
  {
    Subscription subscription1 = this.\u0023\u003DzmqXWWh6oQVIEJrU2Pw\u003D\u003D().Item2;
    if (this.\u0023\u003DzgDVjqFRN8sR7() == subscription1 && this.\u0023\u003Dz3OPaBTitYMD\u0024() == (subscription1 != null ? subscription1.TryGetSecurity() : (Security) null))
      return;
    this.\u0023\u003DzoVkyaxlnrQ61(subscription1);
    Subscription subscription2 = this.\u0023\u003DzgDVjqFRN8sR7();
    this.\u0023\u003DzJaQe\u0024mdWmfnr(subscription2 != null ? subscription2.TryGetSecurity() : (Security) null);
    Action zKgKj0Lc = this.\u0023\u003DzKGKj0Lc\u003D;
    if (zKgKj0Lc == null)
      return;
    zKgKj0Lc();
  }

  public void SetSubscription(IChartElement element, Subscription subscription)
  {
    SynchronizedDictionary<IChartElement, Subscription> zvWhSaOs = this.\u0023\u003DzvWHSaOs\u003D;
    IChartElement chartElement = element;
    zvWhSaOs[chartElement] = subscription ?? throw new ArgumentNullException(nameof (subscription));
    ((IfxChartElement) element).ResetUI();
  }

  public void CancelOrders(Func<Order, bool> predicate = null)
  {
    ((DispatcherObject) this).GuiSync(new Action(new Chart.\u0023\u003DzFDlMmaXY4v\u0024C2LpqBGOoouE\u003D()
    {
      \u0023\u003DzRRvwDu67s9Rm = this,
      \u0023\u003DzaPd0W_M\u003D = predicate
    }.\u0023\u003DzjckDB_EnwpKPCaWFi4pW638\u003D));
  }

  public bool AutoRangeByAnnotations
  {
    get => this.\u0023\u003DznXh8y9EGbQ7R6xXCKw\u003D\u003D;
    set
    {
      if (this.\u0023\u003DznXh8y9EGbQ7R6xXCKw\u003D\u003D == value)
        return;
      this.\u0023\u003DznXh8y9EGbQ7R6xXCKw\u003D\u003D = value;
      NotifyPropertyChangedExHelper.Notify<Chart>(this, nameof (AutoRangeByAnnotations));
    }
  }

  public int MinimumRange
  {
    get => this.ViewModel().MinimumRange;
    set => this.ViewModel().MinimumRange = value;
  }

  public string ChartTheme
  {
    get => this.ViewModel().SelectedTheme;
    set => this.ViewModel().SelectedTheme = value;
  }

  public bool ShowLegend
  {
    get => this.ViewModel().ShowLegend;
    set => this.ViewModel().ShowLegend = value;
  }

  public bool ShowOverview
  {
    get => this.ViewModel().ShowOverview;
    set => this.ViewModel().ShowOverview = value;
  }

  public bool ShowPerfStats
  {
    get => this.\u0023\u003DzL6RNvETlqFx4;
    set
    {
      if (this.\u0023\u003DzL6RNvETlqFx4 == value)
        return;
      this.\u0023\u003DzL6RNvETlqFx4 = value;
      NotifyPropertyChangedExHelper.Notify<Chart>(this, nameof (ShowPerfStats));
    }
  }

  public bool IsInteracted
  {
    get => this.ViewModel().IsInteracted;
    set => this.ViewModel().IsInteracted = value;
  }

  public bool AllowAddArea
  {
    get => this.ViewModel().AllowAddArea;
    set => this.ViewModel().AllowAddArea = value;
  }

  public bool AllowAddAxis
  {
    get => this.ViewModel().AllowAddAxis;
    set => this.ViewModel().AllowAddAxis = value;
  }

  public bool AllowAddCandles
  {
    get => this.ViewModel().AllowAddCandles;
    set => this.ViewModel().AllowAddCandles = value;
  }

  public bool AllowAddIndicators
  {
    get => this.ViewModel().AllowAddIndicators;
    set => this.ViewModel().AllowAddIndicators = value;
  }

  public bool AllowAddOrders
  {
    get => this.ViewModel().AllowAddOrders;
    set => this.ViewModel().AllowAddOrders = value;
  }

  public bool AllowAddOwnTrades
  {
    get => this.ViewModel().AllowAddOwnTrades;
    set => this.ViewModel().AllowAddOwnTrades = value;
  }

  public bool CrossHair
  {
    get => this.\u0023\u003DzwuVzD4GYBhfk;
    set
    {
      if (this.\u0023\u003DzwuVzD4GYBhfk == value)
        return;
      this.\u0023\u003DzwuVzD4GYBhfk = value;
      NotifyPropertyChangedExHelper.Notify<Chart>(this, nameof (CrossHair));
    }
  }

  public bool CrossHairTooltip
  {
    get => this.\u0023\u003Dzi\u0024DHUvHXaw26IYcmlA\u003D\u003D;
    set
    {
      if (this.\u0023\u003Dzi\u0024DHUvHXaw26IYcmlA\u003D\u003D == value)
        return;
      this.\u0023\u003Dzi\u0024DHUvHXaw26IYcmlA\u003D\u003D = value;
      NotifyPropertyChangedExHelper.Notify<Chart>(this, nameof (CrossHairTooltip));
    }
  }

  public bool CrossHairAxisLabels
  {
    get => this.\u0023\u003DzKSdoqGGT2Q0s17vWLQ\u003D\u003D;
    set
    {
      if (this.\u0023\u003DzKSdoqGGT2Q0s17vWLQ\u003D\u003D == value)
        return;
      this.\u0023\u003DzKSdoqGGT2Q0s17vWLQ\u003D\u003D = value;
      NotifyPropertyChangedExHelper.Notify<Chart>(this, nameof (CrossHairAxisLabels));
    }
  }

  public ChartAnnotationTypes AnnotationType
  {
    get => this.\u0023\u003DzdrtI10riHGVN;
    set
    {
      if (this.\u0023\u003DzdrtI10riHGVN == value)
        return;
      this.\u0023\u003DzdrtI10riHGVN = value;
      NotifyPropertyChangedExHelper.Notify<Chart>(this, nameof (AnnotationType));
    }
  }

  public bool OrderCreationMode
  {
    get => this.\u0023\u003DznNCcb03RWapN;
    set
    {
      if (this.\u0023\u003DznNCcb03RWapN == value)
        return;
      this.\u0023\u003DznNCcb03RWapN = value;
      NotifyPropertyChangedExHelper.Notify<Chart>(this, nameof (OrderCreationMode));
    }
  }

  public TimeZoneInfo TimeZone
  {
    get => this._timeZone;
    set
    {
      if (this._timeZone == value)
        return;
      this._timeZone = value ?? throw new ArgumentNullException(nameof (value));
      NotifyPropertyChangedExHelper.Notify<Chart>(this, nameof (TimeZone));
    }
  }

  public bool ShowNonFormedIndicators
  {
    get => this.\u0023\u003DzEpShEnHWfpnt9EgGG53qksn4J8ln8AfdcBAf0gw\u003D;
    set => this.\u0023\u003DzEpShEnHWfpnt9EgGG53qksn4J8ln8AfdcBAf0gw\u003D = value;
  }

  public IList<IndicatorType> IndicatorTypes
  {
    get => (IList<IndicatorType>) this.ViewModel().IndicatorTypes;
  }

  internal Security \u0023\u003Dz3OPaBTitYMD\u0024()
  {
    return this.\u0023\u003DzCeIks\u0024kpJtyCA0n_Hg\u003D\u003D;
  }

  private void \u0023\u003DzJaQe\u0024mdWmfnr(Security _param1)
  {
    this.\u0023\u003DzCeIks\u0024kpJtyCA0n_Hg\u003D\u003D = _param1;
  }

  internal Subscription \u0023\u003DzgDVjqFRN8sR7()
  {
    return this.\u0023\u003DzAFpdFZRw72NT1DPyxQ\u003D\u003D;
  }

  private void \u0023\u003DzoVkyaxlnrQ61(Subscription _param1)
  {
    this.\u0023\u003DzAFpdFZRw72NT1DPyxQ\u003D\u003D = _param1;
  }

  public IEnumerable<Subscription> Subscriptions
  {
    get => this.\u0023\u003DzvWHSaOs\u003D.Values.Distinct<Subscription>();
  }

  public event Action AreaAdding
  {
    add => this.ViewModel().\u0023\u003Dz1plbqKAfOGgT(value);
    remove => this.ViewModel().\u0023\u003DzBJGq\u0024rZnxcA9(value);
  }

  public event Action<ChartArea> AddCandles
  {
    add => this.ViewModel().\u0023\u003DzVpi0shdst7Eq3GZ4Qg\u003D\u003D(value);
    remove => this.ViewModel().\u0023\u003DzPasrMn3CLYCfaNBSBw\u003D\u003D(value);
  }

  public event Action<ChartArea> AddIndicator
  {
    add => this.ViewModel().\u0023\u003DzhyKCJLidU6\u0024W(value);
    remove => this.ViewModel().\u0023\u003DzXebVtC3nhhOK(value);
  }

  public event Action<ChartArea> AddOrders
  {
    add => this.ViewModel().\u0023\u003DzXNNsPD0jD0Pq1irRnA\u003D\u003D(value);
    remove => this.ViewModel().\u0023\u003DzDVmEWtkWkTPwviOp6w\u003D\u003D(value);
  }

  public event Action<ChartArea> AddTrades
  {
    add => this.ViewModel().\u0023\u003Dzs2NcPz2K5a8svDuiBw\u003D\u003D(value);
    remove
    {
      this.ViewModel().\u0023\u003DzjUn\u0024ppBdqy2Pyk\u002445A\u003D\u003D(value);
    }
  }

  public event Action<IChartElement> RemoveElement
  {
    add => this.ViewModel().\u0023\u003DzPvEital2M7gh(value);
    remove => this.ViewModel().\u0023\u003Dzfj2KEivrD_Sr(value);
  }

  public event Action<IChartElement, Subscription> RebuildCandles
  {
    add => this.ViewModel().\u0023\u003DzVZkENKDodIoaG_WJsg\u003D\u003D(value);
    remove => this.ViewModel().\u0023\u003Dztqxgxb_i07gVijmTJw\u003D\u003D(value);
  }

  public event Action<ChartArea, Order> CreateOrder;

  public event Action<Order, Decimal> MoveOrder;

  public event Action<Order> CancelOrder;

  public event Action<IChartAnnotationElement> AnnotationCreated;

  public event Action<IChartAnnotationElement, ChartDrawData.AnnotationData> AnnotationModified;

  public event Action<IChartAnnotationElement> AnnotationDeleted;

  public event Action<IChartAnnotationElement, ChartDrawData.AnnotationData> AnnotationSelected;

  public event Action<IChartCandleElement, Subscription> SubscribeCandleElement;

  public event Action<IChartIndicatorElement, Subscription, IIndicator> SubscribeIndicatorElement;

  public event Action<IChartOrderElement, Subscription> SubscribeOrderElement;

  public event Action<IChartTradeElement, Subscription> SubscribeTradeElement;

  public event Action<IChartElement> UnSubscribeElement;

  internal void \u0023\u003DzTWNYl4Ujpxot(Action<IChartArea> _param1)
  {
    Action<IChartArea> action = this.\u0023\u003DzcHtgn6mNhxMM;
    Action<IChartArea> comparand;
    do
    {
      comparand = action;
      action = Interlocked.CompareExchange<Action<IChartArea>>(ref this.\u0023\u003DzcHtgn6mNhxMM, comparand + _param1, comparand);
    }
    while (action != comparand);
  }

  internal void \u0023\u003DzLw8k9fjIsyhB(Action<IChartArea> _param1)
  {
    Action<IChartArea> action = this.\u0023\u003DzcHtgn6mNhxMM;
    Action<IChartArea> comparand;
    do
    {
      comparand = action;
      action = Interlocked.CompareExchange<Action<IChartArea>>(ref this.\u0023\u003DzcHtgn6mNhxMM, comparand - _param1, comparand);
    }
    while (action != comparand);
  }

  internal void \u0023\u003Dzz6Byf1ItRSMq(Action _param1)
  {
    Action action = this.\u0023\u003DzKGKj0Lc\u003D;
    Action comparand;
    do
    {
      comparand = action;
      action = Interlocked.CompareExchange<Action>(ref this.\u0023\u003DzKGKj0Lc\u003D, comparand + _param1, comparand);
    }
    while (action != comparand);
  }

  internal void \u0023\u003DzhaPIHSD4gMQ9(Action _param1)
  {
    Action action = this.\u0023\u003DzKGKj0Lc\u003D;
    Action comparand;
    do
    {
      comparand = action;
      action = Interlocked.CompareExchange<Action>(ref this.\u0023\u003DzKGKj0Lc\u003D, comparand - _param1, comparand);
    }
    while (action != comparand);
  }

  public void Reset(IEnumerable<IChartElement> elements)
  {
    Chart.\u0023\u003DzbPn0uB_1mAkEZqFF7kGyKzM\u003D obj1 = new Chart.\u0023\u003DzbPn0uB_1mAkEZqFF7kGyKzM\u003D();
    obj1.\u0023\u003DzRRvwDu67s9Rm = this;
    obj1.\u0023\u003DzDqgUu38\u003D = elements;
    Chart.\u0023\u003DzbPn0uB_1mAkEZqFF7kGyKzM\u003D obj2 = obj1;
    List<IChartElement> chartElementList = new List<IChartElement>();
    chartElementList.AddRange(obj1.\u0023\u003DzDqgUu38\u003D);
    \u0023\u003DzUqahKaP3EIK\u0024L1yMVA\u003D\u003D<IChartElement> uqahKaP3EikL1yMva = new \u0023\u003DzUqahKaP3EIK\u0024L1yMVA\u003D\u003D<IChartElement>(chartElementList);
    obj2.\u0023\u003DzDqgUu38\u003D = (IEnumerable<IChartElement>) uqahKaP3EikL1yMva;
    ((DispatcherObject) this).GuiSync(new Action(obj1.\u0023\u003DzD1ojw__0StW8WOcIEQ\u003D\u003D));
  }

  public IChartDrawData CreateData() => (IChartDrawData) new ChartDrawData();

  public IChartArea CreateArea()
  {
    return ((DispatcherObject) this).GuiSync<IChartArea>(new Func<IChartArea>(this.\u0023\u003DzQedEwiM\u003D.CreateArea));
  }

  public IChartAxis CreateAxis()
  {
    return ((DispatcherObject) this).GuiSync<IChartAxis>(new Func<IChartAxis>(this.\u0023\u003DzQedEwiM\u003D.CreateAxis));
  }

  public IChartCandleElement CreateCandleElement()
  {
    return ((DispatcherObject) this).GuiSync<IChartCandleElement>(new Func<IChartCandleElement>(this.\u0023\u003DzQedEwiM\u003D.CreateCandleElement));
  }

  public IChartIndicatorElement CreateIndicatorElement()
  {
    return ((DispatcherObject) this).GuiSync<IChartIndicatorElement>(new Func<IChartIndicatorElement>(this.\u0023\u003DzQedEwiM\u003D.CreateIndicatorElement));
  }

  public IChartActiveOrdersElement CreateActiveOrdersElement()
  {
    return ((DispatcherObject) this).GuiSync<IChartActiveOrdersElement>(new Func<IChartActiveOrdersElement>(this.\u0023\u003DzQedEwiM\u003D.CreateActiveOrdersElement));
  }

  public IChartAnnotationElement CreateAnnotation()
  {
    return ((DispatcherObject) this).GuiSync<IChartAnnotationElement>(new Func<IChartAnnotationElement>(this.\u0023\u003DzQedEwiM\u003D.CreateAnnotation));
  }

  public IChartBandElement CreateBandElement()
  {
    return ((DispatcherObject) this).GuiSync<IChartBandElement>(new Func<IChartBandElement>(this.\u0023\u003DzQedEwiM\u003D.CreateBandElement));
  }

  public IChartLineElement CreateLineElement()
  {
    return ((DispatcherObject) this).GuiSync<IChartLineElement>(new Func<IChartLineElement>(this.\u0023\u003DzQedEwiM\u003D.CreateLineElement));
  }

  public IChartLineElement CreateBubbleElement()
  {
    return ((DispatcherObject) this).GuiSync<IChartLineElement>(new Func<IChartLineElement>(this.\u0023\u003DzQedEwiM\u003D.CreateBubbleElement));
  }

  public IChartOrderElement CreateOrderElement()
  {
    return ((DispatcherObject) this).GuiSync<IChartOrderElement>(new Func<IChartOrderElement>(this.\u0023\u003DzQedEwiM\u003D.CreateOrderElement));
  }

  public IChartTradeElement CreateTradeElement()
  {
    return ((DispatcherObject) this).GuiSync<IChartTradeElement>(new Func<IChartTradeElement>(this.\u0023\u003DzQedEwiM\u003D.CreateTradeElement));
  }

  public void Draw(IChartDrawData data)
  {
    ChartDrawData chartDrawData = data != null ? (ChartDrawData) data : throw new ArgumentNullException(nameof (data));
    foreach (ChartArea chartArea in this.\u0023\u003Dza1mnh6ythHbd)
      chartArea.ViewModel().Draw(chartDrawData);
  }

  internal void \u0023\u003DzGJwj2DzYuV1h(ChartArea _param1, Order _param2)
  {
    Action<ChartArea, Order> zlaBqx5E = this.\u0023\u003DzlaBQx5E\u003D;
    if (zlaBqx5E == null)
      return;
    zlaBqx5E(_param1, _param2);
  }

  internal void \u0023\u003DzoSyIfjNKL9Ta(Order _param1, Decimal _param2)
  {
    Action<Order, Decimal> zJiM5nvc = this.\u0023\u003DzJIM5nvc\u003D;
    if (zJiM5nvc == null)
      return;
    zJiM5nvc(_param1, _param2);
  }

  internal void \u0023\u003DzrMNjBJFuBLP3(Order _param1)
  {
    Action<Order> zmMdfCucSnZwz = this.\u0023\u003DzmMdfCUCSnZWZ;
    if (zmMdfCucSnZwz == null)
      return;
    zmMdfCucSnZwz(_param1);
  }

  internal void \u0023\u003Dz49m\u0024QLWwKQs9(ChartAnnotation _param1)
  {
    Action<IChartAnnotationElement> z6KsbJRt22Hb = this.\u0023\u003Dz6KSbJ_RT22HB;
    if (z6KsbJRt22Hb == null)
      return;
    z6KsbJRt22Hb((IChartAnnotationElement) _param1);
  }

  internal void \u0023\u003Dz5mEkRaZSEt9m(
    ChartAnnotation _param1,
    ChartDrawData.AnnotationData _param2)
  {
    Action<IChartAnnotationElement, ChartDrawData.AnnotationData> zygdSp72uKvhL = this.\u0023\u003DzygdSp72uKVhL;
    if (zygdSp72uKvhL == null)
      return;
    zygdSp72uKvhL((IChartAnnotationElement) _param1, _param2);
  }

  internal void \u0023\u003DzXartur54T48t(ChartAnnotation _param1)
  {
    Action<IChartAnnotationElement> z53l3VmDrGxpJ = this.\u0023\u003Dz53l3VMDrGxpJ;
    if (z53l3VmDrGxpJ == null)
      return;
    z53l3VmDrGxpJ((IChartAnnotationElement) _param1);
  }

  internal void \u0023\u003DzSZqzgFQySfHr(
    ChartAnnotation _param1,
    ChartDrawData.AnnotationData _param2)
  {
    Action<IChartAnnotationElement, ChartDrawData.AnnotationData> zxVqsLo94Ea68 = this.\u0023\u003DzxVqsLO94Ea68;
    if (zxVqsLo94Ea68 == null)
      return;
    zxVqsLo94Ea68((IChartAnnotationElement) _param1, _param2);
  }

  internal TimeZoneInfo \u0023\u003DzJp_PZYEzsJcq()
  {
    return this.Areas.Select<IChartArea, IChartAxis>(Chart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzUgb1mArEMRFvFA4YrQ\u003D\u003D ?? (Chart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzUgb1mArEMRFvFA4YrQ\u003D\u003D = new Func<IChartArea, IChartAxis>(Chart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzqN6a1mvRk0j592xRhqiAxD4ES8KX))).LastOrDefault<IChartAxis>(Chart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzHbIKsCHhb08DRHHYMg\u003D\u003D ?? (Chart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzHbIKsCHhb08DRHHYMg\u003D\u003D = new Func<IChartAxis, bool>(Chart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzOCtOMr4FFxWKTTRmm0dMrehqCY9f)))?.TimeZone;
  }

  internal void \u0023\u003DzBF\u0024LAMIgiEWk(TimeSpan _param1)
  {
    (IChartCandleElement chartCandleElement, Subscription subscription) = this.\u0023\u003DzmqXWWh6oQVIEJrU2Pw\u003D\u003D();
    if (chartCandleElement == null)
      return;
    object obj = subscription.DataType.Arg;
    if ((obj != null ? (obj.Equals((object) _param1) ? 1 : 0) : 0) != 0)
      return;
    this.\u0023\u003DzBU1c7i4ydZiUB8ivBA\u003D\u003D((IChartElement) chartCandleElement, new Subscription(Extensions.TimeFrame(_param1), (SecurityMessage) subscription.MarketData));
  }

  private void \u0023\u003DzBU1c7i4ydZiUB8ivBA\u003D\u003D(
    IChartElement _param1,
    Subscription _param2)
  {
    Chart.\u0023\u003DzOuPSV9MAGW\u0024JG5Cu0tHrBA0\u003D magwJg5Cu0tHrBa0 = new Chart.\u0023\u003DzOuPSV9MAGW\u0024JG5Cu0tHrBA0\u003D();
    magwJg5Cu0tHrBa0.\u0023\u003DzRRvwDu67s9Rm = this;
    magwJg5Cu0tHrBa0.\u0023\u003Dz_i6sZDg\u003D = _param1 as IChartCandleElement;
    if (magwJg5Cu0tHrBa0.\u0023\u003Dz_i6sZDg\u003D == null)
      return;
    IChartArea chartArea = magwJg5Cu0tHrBa0.\u0023\u003Dz_i6sZDg\u003D.ChartArea;
    magwJg5Cu0tHrBa0.\u0023\u003DzlRZ9MD8\u003D = this.TryGetSubscription((IChartElement) magwJg5Cu0tHrBa0.\u0023\u003Dz_i6sZDg\u003D);
    Dictionary<IChartIndicatorElement, Tuple<IIndicator, IChartArea>> dictionary = ((IEnumerable<KeyValuePair<IChartElement, Subscription>>) this.\u0023\u003DzvWHSaOs\u003D).Where<KeyValuePair<IChartElement, Subscription>>(new Func<KeyValuePair<IChartElement, Subscription>, bool>(magwJg5Cu0tHrBa0.\u0023\u003Dz0h9UKK62LR3Vt49xiuMu8R9QgAUI)).Select<KeyValuePair<IChartElement, Subscription>, IChartIndicatorElement>(Chart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzW6pqQQ9lqKPZfyTXDw\u003D\u003D ?? (Chart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzW6pqQQ9lqKPZfyTXDw\u003D\u003D = new Func<KeyValuePair<IChartElement, Subscription>, IChartIndicatorElement>(Chart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzPMkLVMTIVacbdOnMgx5om9R8XfFp42JlLQ\u003D\u003D))).ToDictionary<IChartIndicatorElement, IChartIndicatorElement, Tuple<IIndicator, IChartArea>>(Chart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzBCXPyDZYNZgQeoOs4Q\u003D\u003D ?? (Chart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzBCXPyDZYNZgQeoOs4Q\u003D\u003D = new Func<IChartIndicatorElement, IChartIndicatorElement>(Chart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzuKv4rqmZgBRXOk9cL\u0024V\u0024mJZ_bYxaKSqnjQ\u003D\u003D)), new Func<IChartIndicatorElement, Tuple<IIndicator, IChartArea>>(magwJg5Cu0tHrBa0.\u0023\u003Dzw1Y8PN8Ihy08dhHsTYoW7TH7dWHX));
    this.\u0023\u003Dz\u0024_wDItQvnYOy((IChartElement) magwJg5Cu0tHrBa0.\u0023\u003Dz_i6sZDg\u003D);
    CollectionHelper.ForEach<IChartIndicatorElement>((IEnumerable<IChartIndicatorElement>) dictionary.Keys, new Action<IChartIndicatorElement>(this.\u0023\u003Dz\u0024_wDItQvnYOy));
    ((IfxChartElement) magwJg5Cu0tHrBa0.\u0023\u003Dz_i6sZDg\u003D).ResetUI();
    this.AddElement(chartArea, magwJg5Cu0tHrBa0.\u0023\u003Dz_i6sZDg\u003D, _param2);
    foreach (KeyValuePair<IChartIndicatorElement, Tuple<IIndicator, IChartArea>> keyValuePair in dictionary)
      this.AddElement(keyValuePair.Value.Item2, keyValuePair.Key, _param2, keyValuePair.Value.Item1);
    this.\u0023\u003Dz1jTUhLCjcbWI();
  }

  private void \u0023\u003Dz\u0024_wDItQvnYOy(IChartElement _param1)
  {
    if (_param1 is ChartIndicatorElement indicatorElement && indicatorElement.ParentElement != null)
      _param1 = indicatorElement.ParentElement;
    ((IChart) this).RemoveElement(_param1.ChartArea, _param1);
    this.\u0023\u003Dz1jTUhLCjcbWI();
  }

  public void Load(SettingsStorage storage)
  {
    this.IsAutoScroll = storage.GetValue<bool>("IsAutoScroll", this.IsAutoScroll);
    this.IsAutoRange = storage.GetValue<bool>("IsAutoRange", this.IsAutoRange);
    this.AutoRangeByAnnotations = storage.GetValue<bool>("AutoRangeByAnnotations", this.AutoRangeByAnnotations);
    this.ShowOverview = storage.GetValue<bool>("ShowOverview", this.ShowOverview);
    this.ShowLegend = storage.GetValue<bool>("ShowLegend", this.ShowLegend);
    this.CrossHair = storage.GetValue<bool>("CrossHair", this.CrossHair);
    this.CrossHairTooltip = storage.GetValue<bool>("CrossHairTooltip", this.CrossHairTooltip);
    this.CrossHairAxisLabels = storage.GetValue<bool>("CrossHairAxisLabels", this.CrossHairAxisLabels);
    this.OrderCreationMode = storage.GetValue<bool>("OrderCreationMode", this.OrderCreationMode);
    this.TimeZone = Converter.To<TimeZoneInfo>((object) storage.GetValue<string>("TimeZone", (string) null)) ?? this.TimeZone;
    this.ShowPerfStats = storage.GetValue<bool>("ShowPerfStats", this.ShowPerfStats);
    if (!this.IsInteracted)
      return;
    this.\u0023\u003DzvWHSaOs\u003D.Clear();
    CollectionHelper.ForEach<Chart.\u0023\u003DzZQ9Hpf12oRwg>((IEnumerable<Chart.\u0023\u003DzZQ9Hpf12oRwg>) this.\u0023\u003Dz\u0024IReF1JUpHYmGa845Q\u003D\u003D.Values, Chart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzosc1BoJdGH8Feib98w\u003D\u003D ?? (Chart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzosc1BoJdGH8Feib98w\u003D\u003D = new Action<Chart.\u0023\u003DzZQ9Hpf12oRwg>(Chart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dzd9aoFEQWycLNB5pmAWPP_1U\u003D)));
    this.\u0023\u003Dz\u0024IReF1JUpHYmGa845Q\u003D\u003D.Clear();
    object source = storage.GetValue<object>("Areas", (object) null);
    if (source == null)
      return;
    if (source is SettingsStorage settingsStorage)
      source = (object) settingsStorage.GetValue<IEnumerable<SettingsStorage>>("Areas", (IEnumerable<SettingsStorage>) null);
    this.\u0023\u003DzZTljjN6ww\u0024xh(((IEnumerable) source).Cast<SettingsStorage>());
  }

  public void Save(SettingsStorage storage)
  {
    storage.SetValue<bool>("IsAutoScroll", this.IsAutoScroll);
    storage.SetValue<bool>("IsAutoRange", this.IsAutoRange);
    storage.SetValue<bool>("AutoRangeByAnnotations", this.AutoRangeByAnnotations);
    storage.SetValue<bool>("ShowOverview", this.ShowOverview);
    storage.SetValue<bool>("ShowLegend", this.ShowLegend);
    storage.SetValue<bool>("CrossHair", this.CrossHair);
    storage.SetValue<bool>("CrossHairTooltip", this.CrossHairTooltip);
    storage.SetValue<bool>("CrossHairAxisLabels", this.CrossHairAxisLabels);
    storage.SetValue<bool>("OrderCreationMode", this.OrderCreationMode);
    storage.SetValue<string>("TimeZone", this.TimeZone?.Id);
    storage.SetValue<bool>("ShowPerfStats", this.ShowPerfStats);
    if (!this.IsInteracted)
      return;
    storage.SetValue<SettingsStorage[]>("Areas", this.\u0023\u003Dza1mnh6ythHbd.Select<IChartArea, SettingsStorage>(Chart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzIUod_Wcpx0QRK1tJ1g\u003D\u003D ?? (Chart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzIUod_Wcpx0QRK1tJ1g\u003D\u003D = new Func<IChartArea, SettingsStorage>(Chart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dzi3wYC00Rk5VQyB\u00247dTCZE2Q\u003D))).ToArray<SettingsStorage>());
  }

  public void ReSubscribeElements()
  {
    if (!this.IsInteracted)
      return;
    foreach (IChartElement element in this.GetElements())
    {
      this.\u0023\u003DzvFatuAKD1J5N(element);
      this.\u0023\u003DzHZTXI\u0024xQsuIc(element);
    }
  }

  private void \u0023\u003DzZTljjN6ww\u0024xh(IEnumerable<SettingsStorage> _param1)
  {
    this.\u0023\u003Dza1mnh6ythHbd.Clear();
    foreach (SettingsStorage storage in _param1)
    {
      ChartArea area = new ChartArea();
      area.Load(storage);
      this.AddArea((IChartArea) area);
      area.ViewModel().Height = storage.GetValue<double>("Height", double.NaN);
    }
  }

  private void \u0023\u003DzHZTXI\u0024xQsuIc(IChartElement _param1)
  {
    Subscription subscription = this.TryGetSubscription(_param1);
    if (subscription == null)
      return;
    this.\u0023\u003DzMG4Yhpw\u003D(_param1, subscription);
  }

  private void \u0023\u003DzMG4Yhpw\u003D(IChartElement _param1, Subscription _param2)
  {
    switch (_param1)
    {
      case IChartCandleElement chartCandleElement:
        Action<IChartCandleElement, Subscription> wdiUyNemvFqkVapq = this.\u0023\u003DzlWdi\u0024uyNemvFQkVAPQ\u003D\u003D;
        if (wdiUyNemvFqkVapq == null)
          break;
        wdiUyNemvFqkVapq(chartCandleElement, _param2);
        break;
      case IChartIndicatorElement element:
        Action<IChartIndicatorElement, Subscription, IIndicator> zLdfE1FxkiHdr = this.\u0023\u003DzLDFE1FXkiHDr;
        if (zLdfE1FxkiHdr == null)
          break;
        zLdfE1FxkiHdr(element, _param2, this.GetIndicator(element));
        break;
      case IChartOrderElement chartOrderElement:
        Action<IChartOrderElement, Subscription> zh7nXgYWoKl = this.\u0023\u003Dzh7nXgY\u0024WoKL\u0024;
        if (zh7nXgYWoKl == null)
          break;
        zh7nXgYWoKl(chartOrderElement, _param2);
        break;
      case IChartTradeElement chartTradeElement:
        Action<IChartTradeElement, Subscription> ssvKvae0LsR0LbUsEg = this.\u0023\u003DzSSvKVae0LsR0LbUSEg\u003D\u003D;
        if (ssvKvae0LsR0LbUsEg == null)
          break;
        ssvKvae0LsR0LbUsEg(chartTradeElement, _param2);
        break;
    }
  }

  private void \u0023\u003DzvFatuAKD1J5N(IChartElement _param1)
  {
    if (this.TryGetSubscription(_param1) == null)
      return;
    Action<IChartElement> z9PnYjM29SjfT = this.\u0023\u003Dz9PnYjM29SjfT;
    if (z9PnYjM29SjfT == null)
      return;
    z9PnYjM29SjfT(_param1);
  }

  private void \u0023\u003DzVpu_ST0\u003D(IChartElement _param1)
  {
    this.\u0023\u003Dz1jTUhLCjcbWI();
    if (!this.IsInteracted)
      return;
    this.\u0023\u003DzHZTXI\u0024xQsuIc(_param1);
  }

  private void \u0023\u003DzW\u0024jq94I\u003D(IChartElement _param1)
  {
    this.\u0023\u003Dz1jTUhLCjcbWI();
    this.\u0023\u003DzEm4mXfg\u003D(_param1, false);
  }

  private void \u0023\u003DzEm4mXfg\u003D(IChartElement _param1, bool _param2)
  {
    IChartElement[] elements;
    if (_param1 is IChartCandleElement)
    {
      Chart.\u0023\u003DzRhtUyozsiG5aICl2VmAKuos\u003D uyozsiG5aIcl2VmAkuos = new Chart.\u0023\u003DzRhtUyozsiG5aICl2VmAKuos\u003D();
      uyozsiG5aIcl2VmAkuos.\u0023\u003DzRRvwDu67s9Rm = this;
      uyozsiG5aIcl2VmAkuos.\u0023\u003DzgZ3Boxc\u003D = this.TryGetSubscription(_param1);
      List<IChartElement> chartElementList = new List<IChartElement>();
      chartElementList.AddRange(this.GetElements().Where<IChartElement>(new Func<IChartElement, bool>(uyozsiG5aIcl2VmAkuos.\u0023\u003Dzdtmu9eDzQDfVd61b8w\u003D\u003D)).Concat<IChartElement>((IEnumerable<IChartElement>) new \u0023\u003DzxOY_ppISsiadppaSwGkbOR8\u003D<IChartElement>(_param1)).Distinct<IChartElement>());
      elements = chartElementList.ToArray();
    }
    else
      elements = new IChartElement[1]{ _param1 };
    if (_param2)
    {
      if (this.IsInteracted)
        CollectionHelper.ForEach<IChartElement>((IEnumerable<IChartElement>) elements, new Action<IChartElement>(this.\u0023\u003DzxLh9Nbq2rmfxE6dfQbX9g5I\u003D));
      this.Reset((IEnumerable<IChartElement>) elements);
      if (!this.IsInteracted)
        return;
      CollectionHelper.ForEach<IChartElement>((IEnumerable<IChartElement>) elements, new Action<IChartElement>(this.\u0023\u003Dz8HYKybOvc5oIm3vAUluMQvw\u003D));
    }
    else
    {
      if (!this.IsInteracted)
        return;
      CollectionHelper.ForEach<IChartElement>((IEnumerable<IChartElement>) elements, new Action<IChartElement>(this.\u0023\u003DzvFatuAKD1J5N));
    }
  }

  internal static void \u0023\u003Dz370H8OFDsNyA(
    dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd _param0)
  {
    Chart.\u0023\u003DziYslZOQka25erb85NfEM3z4\u003D qka25erb85NfEm3z4 = new Chart.\u0023\u003DziYslZOQka25erb85NfEM3z4\u003D();
    qka25erb85NfEm3z4.\u0023\u003Dz6x1I8qQ\u003D = _param0;
    if (qka25erb85NfEm3z4.\u0023\u003Dz6x1I8qQ\u003D.DataContext != null)
      qka25erb85NfEm3z4.\u0023\u003Dqtx1KXraU1keT0uiySlEVOOB5PnDLulwyMJjyjX7rsVjruD1DZyrc16lnN0h2\u0024q6Q();
    else
      qka25erb85NfEm3z4.\u0023\u003Dz6x1I8qQ\u003D.DataContextChanged += new DependencyPropertyChangedEventHandler(qka25erb85NfEm3z4.\u0023\u003DzvrcTIvo4QYO6VIoIYgtMLK0\u003D);
  }

  private void dje_zZBGLMJSS5D7A5HB5JY6ZP4E8JMHA_ejd(object _param1, EventArgs _param2)
  {
    Chart.\u0023\u003Dz370H8OFDsNyA((dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd) _param1);
  }

  private void \u0023\u003Dzs2PvqlQSy\u002401UuUfTA\u003D\u003D(
    IChartIndicatorElement _param1,
    IIndicator _param2)
  {
    ((IfxChartElement) _param1).ResetUI();
    this.\u0023\u003DzEm4mXfg\u003D((IChartElement) _param1, true);
  }

  event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
  {
    add => this.PropertyChangedEvent += value;
    remove => this.PropertyChangedEvent -= value;
  }

  void INotifyPropertyChangedEx.NotifyPropertyChanged(string propertyName)
  {
    PropertyChangedEventHandler ziApqnpw = this.PropertyChangedEvent;
    if (ziApqnpw == null)
      return;
    DelegateHelper.Invoke(ziApqnpw, (object) this, propertyName);
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  public void InitializeComponent()
  {
    if (this.\u0023\u003DzQGCmQMjHdLKS)
      return;
    this.\u0023\u003DzQGCmQMjHdLKS = true;
    Application.LoadComponent((object) this, new Uri("/StockSharp.Xaml.Charting;V5.0.0;component/chart.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  internal Delegate \u0023\u003DzciIj4U627yBM(Type _param1, string _param2)
  {
    return Delegate.CreateDelegate(_param1, (object) this, _param2);
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    if (connectionId == 1)
      this.\u0023\u003DzxYLKFqWiCEs\u0024 = (dje_zCNSBRP7GPP9KUVX64FWNJCMERJEQNDDYNP68U92NYNLX7ULAMWXZ2_ejd) target;
    else
      this.\u0023\u003DzQGCmQMjHdLKS = true;
  }

  private Security \u0023\u003DqWFLBlYXsZ6MGzXnc3yqMfUBG1YSw2WmShVXKtnoKBjE\u003D(ChartArea _param1)
  {
    IChartCandleElement[] array = ((IEnumerable) _param1.Elements).OfType<IChartCandleElement>().ToArray<IChartCandleElement>();
    IChartCandleElement element = \u0023\u003DzsIIzg9COgILMyUKVNisy8sT1ePq3.\u0023\u003DzVqxLKNDqEV82<IChartCandleElement>(array);
    if (array.Length > 1)
    {
      ChartCandleElementPicker wnd = new ChartCandleElementPicker()
      {
        Elements = (IEnumerable<IChartCandleElement>) array,
        SelectedElement = element
      };
      if (!wnd.ShowModal((DependencyObject) this))
        return (Security) null;
      element = wnd.SelectedElement;
    }
    Security security1;
    if (element != null)
    {
      Subscription subscription = this.TryGetSubscription((IChartElement) element);
      security1 = subscription != null ? subscription.TryGetSecurity() : (Security) null;
    }
    else
      security1 = (Security) null;
    Security security2 = security1;
    if (security2 == null)
    {
      SecurityPickerWindow wnd = new SecurityPickerWindow()
      {
        SelectionMode = MultiSelectMode.Row
      };
      if (this.SecurityProvider != null)
        wnd.SecurityProvider = this.SecurityProvider;
      if (!wnd.ShowModal((DependencyObject) this))
        return (Security) null;
      security2 = wnd.SelectedSecurity;
    }
    return security2;
  }

  private void \u0023\u003DzYhCvVp5ZsuEOxZPGgrZ6vLQ\u003D(ChartArea _param1)
  {
    if (_param1 == null)
      return;
    _param1.GroupId = !StringHelper.IsEmpty(_param1.GroupId) ? string.Empty : Guid.NewGuid().ToString();
    Action<IChartArea> zcHtgn6mNhxMm = this.\u0023\u003DzcHtgn6mNhxMM;
    if (zcHtgn6mNhxMm == null)
      return;
    zcHtgn6mNhxMm((IChartArea) _param1);
  }

  private void \u0023\u003DzA\u0024Wg8llSgKSoO3igMVHuEmE\u003D()
  {
    Chart.\u0023\u003DzCMPTGwQ4dXwYZLzYWDdBciE\u003D q4dXwYzLzYwDdBciE = new Chart.\u0023\u003DzCMPTGwQ4dXwYZLzYWDdBciE\u003D();
    ChartArea area = new ChartArea()
    {
      Title = $"{LocalizedStrings.Panel} {(this.\u0023\u003Dza1mnh6ythHbd.Count + 1).ToString()}"
    };
    q4dXwYzLzYwDdBciE.\u0023\u003DzvZK8J1raIDr8 = this.\u0023\u003DzJp_PZYEzsJcq();
    CollectionHelper.ForEach<IChartAxis>((IEnumerable<IChartAxis>) area.XAxises, new Action<IChartAxis>(q4dXwYzLzYwDdBciE.\u0023\u003Dzn0BxqFlrvEnaJ4F1tg\u003D\u003D));
    this.AddArea((IChartArea) area);
  }

  private void \u0023\u003DzlPurC69wnp3kb0Gv9WCPMD0\u003D(ChartArea _param1)
  {
    if (this.\u0023\u003DzKCT2CVDV16nJ == null)
      this.\u0023\u003DzKCT2CVDV16nJ = new Subscription((ISubscriptionMessage) this.DefaultCandlesSettings, (SecurityMessage) null);
    CandleSettingsWindow wnd = new CandleSettingsWindow()
    {
      Subscription = ((Cloneable<Subscription>) this.\u0023\u003DzKCT2CVDV16nJ).Clone()
    };
    if (this.SecurityProvider != null)
      wnd.SecurityProvider = this.SecurityProvider;
    if (!wnd.ShowModal((DependencyObject) this))
      return;
    Subscription subscription = wnd.Subscription;
    this.\u0023\u003DzKCT2CVDV16nJ = subscription;
    ChartCandleElement element = new ChartCandleElement()
    {
      PriceStep = (Decimal?) ((SecurityMessage) subscription?.MarketData)?.PriceStep,
      DrawStyle = this.\u0023\u003Dzv9T9127oaRiK()
    };
    this.AddElement((IChartArea) _param1, (IChartCandleElement) element, subscription);
  }

  private void \u0023\u003DziEqlaww8py20vRuZvdjcMwM\u003D(ChartArea _param1)
  {
    IndicatorPickerWindow wnd1 = new IndicatorPickerWindow()
    {
      AutoSelectCandles = true,
      IndicatorTypes = (IEnumerable<IndicatorType>) this.IndicatorTypes
    };
    if (!wnd1.ShowModal((DependencyObject) this))
      return;
    IChartCandleElement[] array = this.GetElements<IChartCandleElement>().ToArray<IChartCandleElement>();
    IChartCandleElement element1 = ((IEnumerable) _param1.Elements).OfType<IChartCandleElement>().Concat<IChartCandleElement>((IEnumerable<IChartCandleElement>) array).FirstOrDefault<IChartCandleElement>();
    if (element1 == null)
    {
      int num = (int) new MessageBoxBuilder().Error().Text(LocalizedStrings.NoData2).Owner((DependencyObject) this).Show();
    }
    else
    {
      if (!wnd1.AutoSelectCandles)
      {
        ChartCandleElementPicker wnd2 = new ChartCandleElementPicker()
        {
          Elements = (IEnumerable<IChartCandleElement>) array,
          SelectedElement = element1
        };
        if (!wnd2.ShowModal((DependencyObject) this))
          return;
        element1 = wnd2.SelectedElement;
      }
      ChartIndicatorElement element2 = new ChartIndicatorElement()
      {
        IndicatorPainter = wnd1.SelectedIndicatorType.CreatePainter(),
        AutoAssignYAxis = true
      };
      this.AddElement((IChartArea) _param1, (IChartIndicatorElement) element2, this.TryGetSubscription((IChartElement) element1), wnd1.Indicator);
    }
  }

  private void \u0023\u003DzUc2ksk6USZ7hJX6w86ULnVg\u003D(ChartArea _param1)
  {
    Security security = this.\u0023\u003DqWFLBlYXsZ6MGzXnc3yqMfUBG1YSw2WmShVXKtnoKBjE\u003D(_param1);
    if (security == null)
      return;
    this.AddElement((IChartArea) _param1, (IChartOrderElement) new ChartOrderElement(), new Subscription(DataType.Transactions, security));
  }

  private void \u0023\u003DzsXLt0R2oLUokWH956n5iSfc\u003D(ChartArea _param1)
  {
    Security security = this.\u0023\u003DqWFLBlYXsZ6MGzXnc3yqMfUBG1YSw2WmShVXKtnoKBjE\u003D(_param1);
    if (security == null)
      return;
    this.AddElement((IChartArea) _param1, (IChartTradeElement) new ChartTradeElement(), new Subscription(DataType.Transactions, security));
  }

  private void \u0023\u003DzsmicQGCNPK5xce6UZ2zNBT0\u003D(string _param1)
  {
    ((INotifyPropertyChangedEx) this).NotifyPropertyChanged(_param1);
  }

  private void \u0023\u003DzxLh9Nbq2rmfxE6dfQbX9g5I\u003D(IChartElement _param1)
  {
    Action<IChartElement> z9PnYjM29SjfT = this.\u0023\u003Dz9PnYjM29SjfT;
    if (z9PnYjM29SjfT == null)
      return;
    z9PnYjM29SjfT(_param1);
  }

  private void \u0023\u003Dz8HYKybOvc5oIm3vAUluMQvw\u003D(IChartElement _param1)
  {
    this.\u0023\u003DzMG4Yhpw\u003D(_param1, this.TryGetSubscription(_param1));
  }

  [Serializable]
  private sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly Chart.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new Chart.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<IChartArea, 
    #nullable enable
    IEnumerable<
    #nullable disable
    IChartElement>> \u0023\u003Dzv\u002455fO77Mn0JronfvA\u003D\u003D;
    public static Func<IChartAxis, bool> \u0023\u003DzJWBwcSmmMYm95T_4EA\u003D\u003D;
    public static Func<IChartArea, IChartAxis> \u0023\u003DzUgb1mArEMRFvFA4YrQ\u003D\u003D;
    public static Func<IChartAxis, bool> \u0023\u003DzHbIKsCHhb08DRHHYMg\u003D\u003D;
    public static Func<KeyValuePair<IChartElement, Subscription>, IChartIndicatorElement> \u0023\u003DzW6pqQQ9lqKPZfyTXDw\u003D\u003D;
    public static Func<IChartIndicatorElement, 
    #nullable enable
    IChartIndicatorElement> \u0023\u003DzBCXPyDZYNZgQeoOs4Q\u003D\u003D;
    public static 
    #nullable disable
    Action<Chart.\u0023\u003DzZQ9Hpf12oRwg> \u0023\u003Dzosc1BoJdGH8Feib98w\u003D\u003D;
    public static Func<IChartArea, SettingsStorage> \u0023\u003DzIUod_Wcpx0QRK1tJ1g\u003D\u003D;

    internal 
    #nullable enable
    IEnumerable<
    #nullable disable
    IChartElement> \u0023\u003DzPJeLPTCgYwdmBIJASFbIA_hFSy_8(IChartArea _param1)
    {
      return (IEnumerable<IChartElement>) _param1.Elements;
    }

    internal IChartAxis \u0023\u003DzqN6a1mvRk0j592xRhqiAxD4ES8KX(IChartArea _param1)
    {
      return ((IEnumerable<IChartAxis>) _param1.XAxises).FirstOrDefault<IChartAxis>(Chart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzJWBwcSmmMYm95T_4EA\u003D\u003D ?? (Chart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzJWBwcSmmMYm95T_4EA\u003D\u003D = new Func<IChartAxis, bool>(Chart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzVVB4LH1KsMwAYfJoLw4TOG6pJ6Nk)));
    }

    internal bool \u0023\u003DzVVB4LH1KsMwAYfJoLw4TOG6pJ6Nk(IChartAxis _param1)
    {
      return _param1.TimeZone != null;
    }

    internal bool \u0023\u003DzOCtOMr4FFxWKTTRmm0dMrehqCY9f(IChartAxis _param1) => _param1 != null;

    internal IChartIndicatorElement \u0023\u003DzPMkLVMTIVacbdOnMgx5om9R8XfFp42JlLQ\u003D\u003D(
      KeyValuePair<IChartElement, Subscription> _param1)
    {
      return (IChartIndicatorElement) _param1.Key;
    }

    internal 
    #nullable enable
    IChartIndicatorElement \u0023\u003DzuKv4rqmZgBRXOk9cL\u0024V\u0024mJZ_bYxaKSqnjQ\u003D\u003D(
      #nullable disable
      IChartIndicatorElement _param1)
    {
      return _param1;
    }

    internal void \u0023\u003Dzd9aoFEQWycLNB5pmAWPP_1U\u003D(Chart.\u0023\u003DzZQ9Hpf12oRwg _param1)
    {
      _param1.Dispose();
    }

    internal SettingsStorage \u0023\u003Dzi3wYC00Rk5VQyB\u00247dTCZE2Q\u003D(IChartArea _param1)
    {
      SettingsStorage settingsStorage = PersistableHelper.Save((IPersistable) _param1);
      settingsStorage.SetValue<double>("Height", ((ChartArea) _param1).ViewModel().Height);
      return settingsStorage;
    }
  }

  private sealed class \u0023\u003DzCMPTGwQ4dXwYZLzYWDdBciE\u003D
  {
    public TimeZoneInfo \u0023\u003DzvZK8J1raIDr8;

    internal void \u0023\u003Dzn0BxqFlrvEnaJ4F1tg\u003D\u003D(IChartAxis _param1)
    {
      _param1.TimeZone = this.\u0023\u003DzvZK8J1raIDr8;
    }
  }

  private sealed class \u0023\u003DzFDlMmaXY4v\u0024C2LpqBGOoouE\u003D
  {
    public Chart \u0023\u003DzRRvwDu67s9Rm;
    public Func<Order, bool> \u0023\u003DzaPd0W_M\u003D;

    internal void \u0023\u003DzjckDB_EnwpKPCaWFi4pW638\u003D()
    {
      this.\u0023\u003DzRRvwDu67s9Rm.ViewModel().\u0023\u003Dz\u0024DK5seweHzSZIyjEhw\u003D\u003D(this.\u0023\u003DzaPd0W_M\u003D);
    }
  }

  private sealed class \u0023\u003DzHeEbGncgo1okL0HgWiq5JrU\u003D
  {
    public IChartElement \u0023\u003Dz_i6sZDg\u003D;
    public Chart \u0023\u003DzRRvwDu67s9Rm;
    public IChartArea \u0023\u003Dzy_5REws\u003D;

    internal void \u0023\u003Dzo0PvKPRyMKTdyL4L\u0024A\u003D\u003D()
    {
      Chart.\u0023\u003DzZQ9Hpf12oRwg zZq9Hpf12oRwg;
      if (this.\u0023\u003Dz_i6sZDg\u003D is IChartIndicatorElement zI6sZdg && zI6sZdg.AutoAssignYAxis && this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003Dz\u0024IReF1JUpHYmGa845Q\u003D\u003D.TryGetValue(zI6sZdg, ref zZq9Hpf12oRwg) && zZq9Hpf12oRwg.Indicator.Measure != IndicatorMeasures.Price)
      {
        (IChartArea, IndicatorMeasures) key = (this.\u0023\u003Dzy_5REws\u003D, zZq9Hpf12oRwg.Indicator.Measure);
        string str;
        if (!this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzDUXY6MNzkE1e.TryGetValue(key, out str))
        {
          str = $"{"Y"}({Guid.NewGuid()})";
          // ISSUE: explicit non-virtual call
          IChartAxis axis = __nonvirtual (this.\u0023\u003DzRRvwDu67s9Rm.CreateAxis());
          axis.Id = str;
          axis.AxisType = ChartAxisType.Numeric;
          ((ICollection<IChartAxis>) this.\u0023\u003Dzy_5REws\u003D.YAxises).Add(axis);
          this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzDUXY6MNzkE1e.Add(key, axis.Id);
        }
        this.\u0023\u003Dz_i6sZDg\u003D.YAxisId = str;
      }
      ((ICollection<IChartElement>) this.\u0023\u003Dzy_5REws\u003D.Elements).Add(this.\u0023\u003Dz_i6sZDg\u003D);
    }
  }

  private sealed class \u0023\u003DzLeQepc5PMlUh44ZxsNBznec\u003D
  {
    public IChartArea \u0023\u003Dzy_5REws\u003D;
    public Chart \u0023\u003DzRRvwDu67s9Rm;
    public Action<IChartAxis> \u0023\u003DzuAeZVTPDgzYE;

    internal void \u0023\u003DzTUl6zvo_PmQZ4TJ2ew\u003D\u003D()
    {
      if (this.\u0023\u003Dzy_5REws\u003D.Chart != null)
        throw new ArgumentException("area.Chart != null", "area");
      if (this.\u0023\u003Dzy_5REws\u003D == null || this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003Dza1mnh6ythHbd.Contains(this.\u0023\u003Dzy_5REws\u003D))
        throw new ArgumentException("area2");
      ChartAxisType? xaxisType = this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003Dza1mnh6ythHbd.FirstOrDefault<IChartArea>()?.XAxisType;
      if (xaxisType.HasValue)
      {
        if (CollectionHelper.IsEmpty<IChartElement>((ICollection<IChartElement>) this.\u0023\u003Dzy_5REws\u003D.Elements))
          this.\u0023\u003Dzy_5REws\u003D.XAxisType = xaxisType.Value;
        else if (this.\u0023\u003Dzy_5REws\u003D.XAxisType != xaxisType.Value)
          throw new InvalidOperationException(LocalizedStrings.InvalidAxisType);
      }
      CollectionHelper.ForEach<IChartAxis>((IEnumerable<IChartAxis>) this.\u0023\u003Dzy_5REws\u003D.XAxises, this.\u0023\u003DzuAeZVTPDgzYE ?? (this.\u0023\u003DzuAeZVTPDgzYE = new Action<IChartAxis>(this.\u0023\u003Dz_CddBmgHU5hqO5sw1g\u003D\u003D)));
      this.\u0023\u003Dzy_5REws\u003D.PropertyChanged += new PropertyChangedEventHandler(this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzqfFsxob3ngDX);
      ((INotifyCollection<IChartElement>) this.\u0023\u003Dzy_5REws\u003D.Elements).Added += new Action<IChartElement>(this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzVpu_ST0\u003D);
      ((INotifyCollection<IChartElement>) this.\u0023\u003Dzy_5REws\u003D.Elements).Removed += new Action<IChartElement>(this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzW\u0024jq94I\u003D);
      this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003Dza1mnh6ythHbd.Add(this.\u0023\u003Dzy_5REws\u003D);
      this.\u0023\u003Dzy_5REws\u003D.Chart = (IChart) this.\u0023\u003DzRRvwDu67s9Rm;
      this.\u0023\u003DzRRvwDu67s9Rm.ViewModel().ChartPaneViewModels.Add(((ChartArea) this.\u0023\u003Dzy_5REws\u003D).ViewModel());
      CollectionHelper.ForEach<IChartElement>((IEnumerable<IChartElement>) this.\u0023\u003Dzy_5REws\u003D.Elements, new Action<IChartElement>(this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzVpu_ST0\u003D));
      Action<IChartArea> zk8cjLwfRrDki = this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003Dzk8cjLWfRrDKI;
      if (zk8cjLwfRrDki == null)
        return;
      zk8cjLwfRrDki(this.\u0023\u003Dzy_5REws\u003D);
    }

    internal void \u0023\u003Dz_CddBmgHU5hqO5sw1g\u003D\u003D(IChartAxis _param1)
    {
      // ISSUE: explicit non-virtual call
      _param1.AutoRange = __nonvirtual (this.\u0023\u003DzRRvwDu67s9Rm.IsAutoRange);
    }
  }

  private sealed class \u0023\u003DzOuPSV9MAGW\u0024JG5Cu0tHrBA0\u003D
  {
    public Subscription \u0023\u003DzlRZ9MD8\u003D;
    public IChartCandleElement \u0023\u003Dz_i6sZDg\u003D;
    public Chart \u0023\u003DzRRvwDu67s9Rm;

    internal bool \u0023\u003Dz0h9UKK62LR3Vt49xiuMu8R9QgAUI(
      KeyValuePair<IChartElement, Subscription> _param1)
    {
      return _param1.Value == this.\u0023\u003DzlRZ9MD8\u003D && _param1.Key != this.\u0023\u003Dz_i6sZDg\u003D;
    }

    internal Tuple<IIndicator, IChartArea> \u0023\u003Dzw1Y8PN8Ihy08dhHsTYoW7TH7dWHX(
      IChartIndicatorElement _param1)
    {
      return Tuple.Create<IIndicator, IChartArea>(this.\u0023\u003DzRRvwDu67s9Rm.GetIndicator(_param1), _param1.ChartArea);
    }
  }

  private sealed class \u0023\u003DzRgOqXl_UCNpCcsODKLfs7Ks\u003D
  {
    public string[] \u0023\u003DzgE4mCHaKpzj3mF7k57tWsbQ\u003D;
    public Chart \u0023\u003DzRRvwDu67s9Rm;

    internal void \u0023\u003DzYqlPx6RFcqzqQlN8KA\u003D\u003D()
    {
      CollectionHelper.ForEach<string>((IEnumerable<string>) this.\u0023\u003DzgE4mCHaKpzj3mF7k57tWsbQ\u003D, new Action<string>(this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzsmicQGCNPK5xce6UZ2zNBT0\u003D));
    }
  }

  private sealed class \u0023\u003DzRhtUyozsiG5aICl2VmAKuos\u003D
  {
    public Subscription \u0023\u003DzgZ3Boxc\u003D;
    public Chart \u0023\u003DzRRvwDu67s9Rm;

    internal bool \u0023\u003Dzdtmu9eDzQDfVd61b8w\u003D\u003D(IChartElement _param1)
    {
      // ISSUE: explicit non-virtual call
      return __nonvirtual (this.\u0023\u003DzRRvwDu67s9Rm.TryGetSubscription(_param1)) == this.\u0023\u003DzgZ3Boxc\u003D;
    }
  }

  private sealed class \u0023\u003DzZQ9Hpf12oRwg : Disposable
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Chart _chart;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly IChartIndicatorElement \u0023\u003Dz2YSX_Z4\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly IIndicator \u0023\u003Dz5re6lC2j05\u0024MW0IM5w\u003D\u003D;

    public \u0023\u003DzZQ9Hpf12oRwg(
      Chart _param1,
      IChartIndicatorElement _param2,
      IIndicator _param3)
    {
      this._chart = _param1 ?? throw new ArgumentNullException("parent");
      this.\u0023\u003Dz2YSX_Z4\u003D = _param2 ?? throw new ArgumentNullException("element");
      this.\u0023\u003Dz5re6lC2j05\u0024MW0IM5w\u003D\u003D = _param3 ?? throw new ArgumentNullException("indicator");
      this.Indicator.Reseted += new Action(this.\u0023\u003Dze7c89\u0024wRkR45b80_dQ\u003D\u003D);
    }

    public IIndicator Indicator => this.\u0023\u003Dz5re6lC2j05\u0024MW0IM5w\u003D\u003D;

    protected virtual void DisposeManaged()
    {
      base.DisposeManaged();
      this.Indicator.Reseted -= new Action(this.\u0023\u003Dze7c89\u0024wRkR45b80_dQ\u003D\u003D);
    }

    private void \u0023\u003Dze7c89\u0024wRkR45b80_dQ\u003D\u003D()
    {
      if (this._chart.DisableIndicatorReset)
        return;
      this._chart.\u0023\u003Dzs2PvqlQSy\u002401UuUfTA\u003D\u003D(this.\u0023\u003Dz2YSX_Z4\u003D, this.Indicator);
    }
  }

  private sealed class \u0023\u003DzbPn0uB_1mAkEZqFF7kGyKzM\u003D
  {
    public Chart \u0023\u003DzRRvwDu67s9Rm;
    public IEnumerable<IChartElement> \u0023\u003DzDqgUu38\u003D;

    internal void \u0023\u003DzD1ojw__0StW8WOcIEQ\u003D\u003D()
    {
      foreach (ChartArea chartArea in this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003Dza1mnh6ythHbd)
        chartArea.ViewModel().Reset(this.\u0023\u003DzDqgUu38\u003D);
    }
  }

  private sealed class \u0023\u003DziYslZOQka25erb85NfEM3z4\u003D
  {
    public dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd \u0023\u003Dz6x1I8qQ\u003D;

    internal void \u0023\u003Dqtx1KXraU1keT0uiySlEVOOB5PnDLulwyMJjyjX7rsVjruD1DZyrc16lnN0h2\u0024q6Q()
    {
      ((\u0023\u003DzJ9vSi7sIwIEed80npzusCHkUgplLrVxmg1iWODdl3TDNKj06Uu87_wzk09Wj) this.\u0023\u003Dz6x1I8qQ\u003D.DataContext).\u0023\u003Dz3p2JBPVHDEUh(this.\u0023\u003Dz6x1I8qQ\u003D);
    }

    internal void \u0023\u003DzvrcTIvo4QYO6VIoIYgtMLK0\u003D(
      object _param1,
      DependencyPropertyChangedEventArgs _param2)
    {
      this.\u0023\u003Dqtx1KXraU1keT0uiySlEVOOB5PnDLulwyMJjyjX7rsVjruD1DZyrc16lnN0h2\u0024q6Q();
    }
  }

  private sealed class \u0023\u003Dzm_oWpvJyDZiF55m7JqXuwJc\u003D
  {
    public IChartArea \u0023\u003Dzy_5REws\u003D;
    public IChartElement \u0023\u003Dz_i6sZDg\u003D;

    internal bool \u0023\u003DzRy89s7w8wbPZi21A_M2JlLqW05mnmhs8PApgiRA\u003D()
    {
      return ((ICollection<IChartElement>) this.\u0023\u003Dzy_5REws\u003D.Elements).Remove(this.\u0023\u003Dz_i6sZDg\u003D);
    }
  }

  private sealed class \u0023\u003DzwNBM8yU0t_ER1Neix6Hn12Q\u003D
  {
    public Chart \u0023\u003DzRRvwDu67s9Rm;
    public IChartArea \u0023\u003Dzy_5REws\u003D;

    internal void \u0023\u003DzJiDIwzXuOcws4Nsgtg\u003D\u003D()
    {
      if (!this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003Dza1mnh6ythHbd.Remove(this.\u0023\u003Dzy_5REws\u003D))
        return;
      this.\u0023\u003Dzy_5REws\u003D.PropertyChanged -= new PropertyChangedEventHandler(this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzqfFsxob3ngDX);
      ((INotifyCollection<IChartElement>) this.\u0023\u003Dzy_5REws\u003D.Elements).Added -= new Action<IChartElement>(this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzVpu_ST0\u003D);
      ((INotifyCollection<IChartElement>) this.\u0023\u003Dzy_5REws\u003D.Elements).Removed -= new Action<IChartElement>(this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzW\u0024jq94I\u003D);
      this.\u0023\u003DzRRvwDu67s9Rm.ViewModel().ChartPaneViewModels.Remove(((ChartArea) this.\u0023\u003Dzy_5REws\u003D).ViewModel());
      CollectionHelper.ForEach<IChartElement>((IEnumerable<IChartElement>) this.\u0023\u003Dzy_5REws\u003D.Elements, new Action<IChartElement>(this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzW\u0024jq94I\u003D));
      this.\u0023\u003Dzy_5REws\u003D.Chart = (IChart) null;
      TypeHelper.DoDispose<IChartArea>(this.\u0023\u003Dzy_5REws\u003D);
      Action<IChartArea> z0aBkRs4Mkj0a = this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003Dz0aBkRs4Mkj0a;
      if (z0aBkRs4Mkj0a != null)
        z0aBkRs4Mkj0a(this.\u0023\u003Dzy_5REws\u003D);
      if (!CollectionHelper.IsEmpty<IChartArea>((ICollection<IChartArea>) this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003Dza1mnh6ythHbd))
        return;
      this.\u0023\u003DzRRvwDu67s9Rm.ViewModel().\u0023\u003DzoMQQ88MEiBDX();
    }
  }
}
