// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartPanel
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Dialogs;
using DevExpress.Xpf.Editors.Settings;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Logging;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Xaml.Charting.IndicatorPainters;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;

#nullable enable
namespace StockSharp.Xaml.Charting;

/// <summary>
/// The extended graphical component for candle charts displaying.
/// </summary>
/// <summary>ChartPanel</summary>
public class ChartPanel : 
  UserControl,
  IWpfChart,
  IChart,
  IChartBuilder,
  IThemeableChart,
  IPersistable,
  INotifyPropertyChangedEx,
  INotifyPropertyChanged,
  IComponentConnector,
  IStyleConnector
{
  
  private 
  #nullable disable
  CancellationTokenSource \u0023\u003DzjOHdKU9O7LKI;
  
  private readonly ObservableCollection<TimeSpan> \u0023\u003DzMtehrzRObGK8;
  /// <summary>The command for the order registration.</summary>
  public static readonly RoutedCommand RegisterOrderCommand = new RoutedCommand();
  /// <summary>The command to add the area to the chart.</summary>
  public static readonly RoutedCommand AddAreaCommand = new RoutedCommand();
  /// <summary>The command to add candles to the chart.</summary>
  public static readonly RoutedCommand AddCandlesCommand = new RoutedCommand();
  /// <summary>The command to add the indicator to the chart.</summary>
  public static readonly RoutedCommand AddIndicatorCommand = new RoutedCommand();
  /// <summary>The command for share chart as an image.</summary>
  public static readonly RoutedCommand ShareCommand = new RoutedCommand();
  /// <summary>The command for the pattern creation.</summary>
  public static readonly RoutedCommand PatternCommand = new RoutedCommand();
  
  private readonly ChartPanelOrderSettings \u0023\u003Dzu4SEH0UpBFYOJH\u0024CgC7r4Rg\u003D;
  
  private readonly ChartPanelShareSettings \u0023\u003DzxpZbe5MLm\u0024yDXxXOLgO2QY0\u003D;
  
  private static readonly List<ChartPanel.\u0023\u003DzDvqdDoIQ0lWXMU6f7Q\u003D\u003D> \u0023\u003DzoQNjsvS8x7txZ62LTQ\u003D\u003D = new List<ChartPanel.\u0023\u003DzDvqdDoIQ0lWXMU6f7Q\u003D\u003D>();
  
  private PropertyChangedEventHandler PropertyChangedEvent;
  
  private bool \u0023\u003DzMJITrmQ\u003D;
  
  private bool \u0023\u003DzCm\u0024a_RJSIzjb8Je3X21wN90\u003D;
  
  internal ChartPanel \u0023\u003Dzv4BS1WQ\u003D;
  
  internal QuickOrderPanel \u0023\u003DzW15YPW8yFbIF;
  
  internal BarManager \u0023\u003DzK0Y33rI\u003D;
  
  internal BarEditItem \u0023\u003DzAWra9\u0024_YgvQ6QTggug\u003D\u003D;
  
  internal ComboBoxEditSettings \u0023\u003Dz5wj51j\u0024m7KHERsE\u0024DA\u003D\u003D;
  
  internal BarEditItem \u0023\u003Dzb98_9e4umo9R;
  
  internal ComboBoxEditSettings \u0023\u003Dz1jt7bIJr0f0_;
  
  internal BarCheckItem \u0023\u003DzVzfMDsVHAcU9;
  
  internal Chart \u0023\u003DzO72kpz0\u003D;
  
  private bool \u0023\u003DzQGCmQMjHdLKS;

  static ChartPanel()
  {
    ChartPanel.RegisterCandleStyle("", new Func<string>(ChartPanel.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dz9FyKEnqWZMl35lW1Mh8Pfl4\u003D), ChartCandleDrawStyles.Ohlc);
    ChartPanel.RegisterCandleStyle("", new Func<string>(ChartPanel.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzlXKB39\u0024_RGvuXSNaoFYbUfY\u003D), ChartCandleDrawStyles.CandleStick);
    ChartPanel.RegisterCandleStyle("", new Func<string>(ChartPanel.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dz3VNaKjF0Px9ZE74bMBlf8Jc\u003D), ChartCandleDrawStyles.LineOpen);
    ChartPanel.RegisterCandleStyle("", new Func<string>(ChartPanel.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzPwX1fdUl8qrydTYdTH_dK8E\u003D), ChartCandleDrawStyles.LineClose);
    ChartPanel.RegisterCandleStyle("", new Func<string>(ChartPanel.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzKNZn5frBPZ2lkm_QkzXTr78\u003D), ChartCandleDrawStyles.LineHigh);
    ChartPanel.RegisterCandleStyle("", new Func<string>(ChartPanel.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzatM8lF9_VANXs9rCA\u0024nAIFo\u003D), ChartCandleDrawStyles.LineLow);
    ChartPanel.RegisterCandleStyle("", new Func<string>(ChartPanel.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dz6DsZRppqVaGqhjmGQ982Snc\u003D), ChartCandleDrawStyles.Area);
    ChartPanel.RegisterCandleStyle("", new Func<string>(ChartPanel.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzHdtpgWUYDJsY5xnfHe5\u0024iZQ\u003D), ChartCandleDrawStyles.BoxVolume);
    ChartPanel.RegisterCandleStyle("", new Func<string>(ChartPanel.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzgUhRURp7DzSchtEQx_odDF4\u003D), ChartCandleDrawStyles.ClusterProfile);
    ChartPanel.RegisterCandleStyle("", new Func<string>(ChartPanel.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzBs0U8AxGBth7PQG2Cppyd9U\u003D), ChartCandleDrawStyles.PnF);
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="T:StockSharp.Xaml.Charting.ChartPanel" />.
  /// </summary>
  public ChartPanel()
  {
    ChartPanel.\u0023\u003DzMwuV1aTxCngvmYEggNUKzD8\u003D txCngvmYeggNuKzD8 = new ChartPanel.\u0023\u003DzMwuV1aTxCngvmYEggNUKzD8\u003D();
    txCngvmYeggNuKzD8.\u0023\u003DzRRvwDu67s9Rm = this;
    this.InitializeComponent();
    if (this.IsDesignMode())
      return;
    this.\u0023\u003Dzu4SEH0UpBFYOJH\u0024CgC7r4Rg\u003D = new ChartPanelOrderSettings();
    this.OrderSettings.PropertyChanged += new PropertyChangedEventHandler(this.\u0023\u003DzwOtSTPwtINP9b1tYlTf9WvY\u003D);
    this.\u0023\u003DzxpZbe5MLm\u0024yDXxXOLgO2QY0\u003D = new ChartPanelShareSettings();
    this.ShareSettings.PropertyChanged += new PropertyChangedEventHandler(this.\u0023\u003DzWUtxOUt4gZ3\u0024);
    this.\u0023\u003DzO72kpz0\u003D.CreateOrder += new Action<ChartArea, Order>(this.\u0023\u003DzmbbCBPeSAsRFiiWslELKWfY\u003D);
    this.\u0023\u003DzO72kpz0\u003D.MoveOrder += new Action<Order, Decimal>(this.\u0023\u003DzsIUgZC\u0024bsMzwntYtdPR0Dww\u003D);
    this.\u0023\u003DzO72kpz0\u003D.CancelOrder += new Action<Order>(this.\u0023\u003Dzg1OGYvQtC93XdkFX1\u002444hrw\u003D);
    this.\u0023\u003DzO72kpz0\u003D.AnnotationCreated += new Action<IChartAnnotationElement>(this.\u0023\u003Dzam5mZvc5ePT\u0024xXJJL7L3riM\u003D);
    this.\u0023\u003DzO72kpz0\u003D.AnnotationModified += new Action<IChartAnnotationElement, ChartDrawData.AnnotationData>(this.\u0023\u003DzJovCO\u0024\u0024Un0QKO1u02HWxPEc\u003D);
    this.\u0023\u003DzO72kpz0\u003D.AnnotationDeleted += new Action<IChartAnnotationElement>(this.\u0023\u003DzxEMEGl05Lk7X5Tl\u0024DugWerI\u003D);
    this.\u0023\u003DzO72kpz0\u003D.AnnotationSelected += new Action<IChartAnnotationElement, ChartDrawData.AnnotationData>(this.\u0023\u003Dz8k9gDPsUiUIXL\u00242BjkEu8Hc\u003D);
    this.\u0023\u003DzO72kpz0\u003D.SubscribeCandleElement += new Action<IChartCandleElement, Subscription>(this.\u0023\u003Dz4wQzbf0WsLc5YzDDh44eqC4\u003D);
    this.\u0023\u003DzO72kpz0\u003D.SubscribeIndicatorElement += new Action<IChartIndicatorElement, Subscription, IIndicator>(this.\u0023\u003DzcNKrDKPhRiLWRqFNhpXIukg\u003D);
    this.\u0023\u003DzO72kpz0\u003D.SubscribeOrderElement += new Action<IChartOrderElement, Subscription>(this.\u0023\u003Dz1aDyDm3zg_Rs2wBbVgacVkc\u003D);
    this.\u0023\u003DzO72kpz0\u003D.SubscribeTradeElement += new Action<IChartTradeElement, Subscription>(this.\u0023\u003DzKAwimyvAzaY\u0024AMPMBCjXySc\u003D);
    this.\u0023\u003DzO72kpz0\u003D.UnSubscribeElement += new Action<IChartElement>(this.\u0023\u003Dzk9_ZG7Umjd_3nlcCpYJ42Hc\u003D);
    this.\u0023\u003DzO72kpz0\u003D.\u0023\u003DzTWNYl4Ujpxot(new Action<IChartArea>(this.\u0023\u003DzP98lXRlARNrXZxrvU5arwx8\u003D));
    this.\u0023\u003DzO72kpz0\u003D.AreaAdded += new Action<IChartArea>(this.\u0023\u003Dzwg4hEbZtsdrK12xCFTj_Fyg\u003D);
    this.\u0023\u003DzO72kpz0\u003D.AreaRemoved += new Action<IChartArea>(this.\u0023\u003Dz_zYN8JxCohXIKNL2XV\u0024B6P4\u003D);
    this.\u0023\u003Dz5wj51j\u0024m7KHERsE\u0024DA\u003D\u003D.ItemsSource = (object) ChartPanel.\u0023\u003DzoQNjsvS8x7txZ62LTQ\u003D\u003D;
    this.\u0023\u003DzAWra9\u0024_YgvQ6QTggug\u003D\u003D.EditValue = (object) ChartPanel.\u0023\u003DzoQNjsvS8x7txZ62LTQ\u003D\u003D[1];
    ObservableCollection<TimeSpan> observableCollection = new ObservableCollection<TimeSpan>();
    TimeSpan[] timeSpanArray = new TimeSpan[12]
    {
      TimeSpan.Zero,
      TimeSpan.FromMinutes(1.0),
      TimeSpan.FromMinutes(2.0),
      TimeSpan.FromMinutes(3.0),
      TimeSpan.FromMinutes(5.0),
      TimeSpan.FromMinutes(10.0),
      TimeSpan.FromMinutes(15.0),
      TimeSpan.FromMinutes(30.0),
      TimeSpan.FromHours(1.0),
      TimeSpan.FromHours(2.0),
      TimeSpan.FromHours(4.0),
      TimeSpan.FromDays(1.0)
    };
    foreach (TimeSpan timeSpan in timeSpanArray)
      observableCollection.Add(timeSpan);
    this.\u0023\u003DzMtehrzRObGK8 = observableCollection;
    this.\u0023\u003Dz1jt7bIJr0f0_.ItemsSource = (object) this.\u0023\u003DzMtehrzRObGK8;
    this.\u0023\u003Dzb98_9e4umo9R.EditValue = (object) TimeSpan.Zero;
    this.\u0023\u003DzO72kpz0\u003D.\u0023\u003Dzz6Byf1ItRSMq(new Action(this.\u0023\u003DzjfWxqcPIhQTyy4aKxIZ6mn8\u003D));
    this.\u0023\u003DzW15YPW8yFbIF.SettingsChanged += new Action(this.\u0023\u003DzJ2e2vCmK2SDuOdabPwfX1Ms\u003D);
    txCngvmYeggNuKzD8.\u0023\u003DzwqcAXcxvUggD = new string[1]
    {
      ""
    };
    ((INotifyPropertyChanged) this.\u0023\u003DzO72kpz0\u003D).PropertyChanged += new PropertyChangedEventHandler(txCngvmYeggNuKzD8.\u0023\u003DzpgDygB5ErIl1Nu7QHw\u003D\u003D);
  }

  /// <summary>The provider of information about instruments.</summary>
  public ISecurityProvider SecurityProvider
  {
    get => this.\u0023\u003DzO72kpz0\u003D.SecurityProvider;
    set => this.\u0023\u003DzO72kpz0\u003D.SecurityProvider = value;
  }

  /// <summary>The chart settings change event.</summary>
  public event Action SettingsChanged;

  /// <summary>The event of the order registration.</summary>
  public event Action<IChartArea, Order> RegisterOrder;

  /// <summary>The event of the order re-registration.</summary>
  public event Action<Order, Decimal> MoveOrder;

  /// <summary>The event of the order cancellation.</summary>
  public event Action<Order> CancelOrder;

  /// <summary>Annotation created event.</summary>
  public event Action<IChartAnnotationElement> AnnotationCreated;

  /// <summary>Annotation modified event.</summary>
  public event Action<IChartAnnotationElement, ChartDrawData.AnnotationData> AnnotationModified;

  /// <summary>Annotation deleted event.</summary>
  public event Action<IChartAnnotationElement> AnnotationDeleted;

  /// <summary>Annotation selection event.</summary>
  public event Action<IChartAnnotationElement, ChartDrawData.AnnotationData> AnnotationSelected;

  /// <summary>
  /// The event of the subscription to getting data for the element.
  /// </summary>
  public event Action<IChartCandleElement, Subscription> SubscribeCandleElement;

  /// <summary>
  /// The event of the subscription to getting data for the element.
  /// </summary>
  public event Action<IChartIndicatorElement, Subscription, IIndicator> SubscribeIndicatorElement;

  /// <summary>
  /// The event of the subscription to getting data for the element.
  /// </summary>
  public event Action<IChartOrderElement, Subscription> SubscribeOrderElement;

  /// <summary>
  /// The event of the subscription to getting data for the element.
  /// </summary>
  public event Action<IChartTradeElement, Subscription> SubscribeTradeElement;

  /// <summary>
  /// The event of the unsubscribe from getting data for the element.
  /// </summary>
  public event Action<IChartElement> UnSubscribeElement;

  /// <inheritdoc />
  public event Action<IChartArea> AreaAdded;

  /// <inheritdoc />
  public event Action<IChartArea> AreaRemoved;

  /// <inheritdoc />
  public bool IsAutoRange
  {
    get => this.\u0023\u003DzO72kpz0\u003D.IsAutoRange;
    set => this.\u0023\u003DzO72kpz0\u003D.IsAutoRange = value;
  }

  /// <inheritdoc />
  public string ChartTheme
  {
    get => this.\u0023\u003DzO72kpz0\u003D.ChartTheme;
    set => this.\u0023\u003DzO72kpz0\u003D.ChartTheme = value;
  }

  /// <inheritdoc />
  public bool ShowPerfStats
  {
    get => this.\u0023\u003DzO72kpz0\u003D.ShowPerfStats;
    set => this.\u0023\u003DzO72kpz0\u003D.ShowPerfStats = value;
  }

  /// <summary>Underlying chart.</summary>
  public IChart UnderlyingChart => (IChart) this.\u0023\u003DzO72kpz0\u003D;

  /// <summary>Chart order registering settings.</summary>
  public ChartPanelOrderSettings OrderSettings
  {
    get => this.\u0023\u003Dzu4SEH0UpBFYOJH\u0024CgC7r4Rg\u003D;
  }

  /// <summary>Chart share (upload image to web) settings.</summary>
  public ChartPanelShareSettings ShareSettings
  {
    get => this.\u0023\u003DzxpZbe5MLm\u0024yDXxXOLgO2QY0\u003D;
  }

  /// <summary>Disable tracking indicator reset.</summary>
  public bool DisableIndicatorReset
  {
    get => this.\u0023\u003DzO72kpz0\u003D.DisableIndicatorReset;
    set => this.\u0023\u003DzO72kpz0\u003D.DisableIndicatorReset = value;
  }

  /// <inheritdoc />
  public void AddArea(IChartArea area) => this.\u0023\u003DzO72kpz0\u003D.AddArea(area);

  /// <inheritdoc />
  public void RemoveArea(IChartArea area) => this.\u0023\u003DzO72kpz0\u003D.RemoveArea(area);

  /// <inheritdoc />
  public void AddElement(IChartArea area, IChartElement element)
  {
    this.\u0023\u003DzO72kpz0\u003D.AddElement(area, element);
  }

  /// <inheritdoc />
  public void AddElement(IChartArea area, IChartCandleElement element, Subscription subscription)
  {
    this.\u0023\u003DzO72kpz0\u003D.AddElement(area, element, subscription);
  }

  /// <inheritdoc />
  public void AddElement(
    IChartArea area,
    IChartIndicatorElement element,
    Subscription subscription,
    IIndicator indicator)
  {
    this.\u0023\u003DzO72kpz0\u003D.AddElement(area, element, subscription, indicator);
  }

  /// <inheritdoc />
  public void AddElement(IChartArea area, IChartOrderElement element, Subscription subscription)
  {
    this.\u0023\u003DzO72kpz0\u003D.AddElement(area, element, subscription);
  }

  /// <inheritdoc />
  public void AddElement(IChartArea area, IChartTradeElement element, Subscription subscription)
  {
    this.\u0023\u003DzO72kpz0\u003D.AddElement(area, element, subscription);
  }

  /// <inheritdoc />
  public void RemoveElement(IChartArea area, IChartElement element)
  {
    ((IChart) this.\u0023\u003DzO72kpz0\u003D).RemoveElement(area, element);
  }

  /// <inheritdoc />
  public IEnumerable<Subscription> Subscriptions => this.\u0023\u003DzO72kpz0\u003D.Subscriptions;

  /// <inheritdoc />
  public Subscription TryGetSubscription(IChartElement element)
  {
    return this.\u0023\u003DzO72kpz0\u003D.TryGetSubscription(element);
  }

  /// <inheritdoc />
  public void SetSubscription(IChartElement element, Subscription subscription)
  {
    this.\u0023\u003DzO72kpz0\u003D.SetSubscription(element, subscription);
  }

  /// <summary>
  /// Cancel orders that were added to this instance of <see cref="T:StockSharp.Xaml.Charting.ChartPanel" />.
  /// This method does not cancel orders by itself. It just notifies subscribers with <see cref="E:StockSharp.Xaml.Charting.ChartPanel.CancelOrder" /> event.
  /// </summary>
  public void CancelOrders(Func<Order, bool> predicate = null)
  {
    ((DispatcherObject) this).GuiAsync(new Action(new ChartPanel.\u0023\u003Dzfi7\u0024520U3d1pGN5XKVeQxF4\u003D()
    {
      \u0023\u003DzRRvwDu67s9Rm = this,
      \u0023\u003DzaPd0W_M\u003D = predicate
    }.\u0023\u003DzjckDB_EnwpKPCaWFi4pW638\u003D));
  }

  /// <summary>The interactive mode. The default is off.</summary>
  public bool IsInteracted
  {
    get => this.\u0023\u003DzO72kpz0\u003D.IsInteracted;
    set => this.\u0023\u003DzO72kpz0\u003D.IsInteracted = value;
  }

  /// <summary>Allow user to add new chart area.</summary>
  public bool AllowAddArea
  {
    get => this.\u0023\u003DzO72kpz0\u003D.AllowAddArea;
    set => this.\u0023\u003DzO72kpz0\u003D.AllowAddArea = value;
  }

  /// <summary>Allow user to add new chart axis.</summary>
  public bool AllowAddAxis
  {
    get => this.\u0023\u003DzO72kpz0\u003D.AllowAddAxis;
    set => this.\u0023\u003DzO72kpz0\u003D.AllowAddAxis = value;
  }

  /// <summary>Allow user to add new candles elements.</summary>
  public bool AllowAddCandles
  {
    get => this.\u0023\u003DzO72kpz0\u003D.AllowAddCandles;
    set => this.\u0023\u003DzO72kpz0\u003D.AllowAddCandles = value;
  }

  /// <summary>Allow user to add new indicator elements.</summary>
  public bool AllowAddIndicators
  {
    get => this.\u0023\u003DzO72kpz0\u003D.AllowAddIndicators;
    set => this.\u0023\u003DzO72kpz0\u003D.AllowAddIndicators = value;
  }

  /// <summary>Allow user to add new orders elements.</summary>
  public bool AllowAddOrders
  {
    get => this.\u0023\u003DzO72kpz0\u003D.AllowAddOrders;
    set => this.\u0023\u003DzO72kpz0\u003D.AllowAddOrders = value;
  }

  /// <summary>Allow user to add new own trades elements.</summary>
  public bool AllowAddOwnTrades
  {
    get => this.\u0023\u003DzO72kpz0\u003D.AllowAddOwnTrades;
    set => this.\u0023\u003DzO72kpz0\u003D.AllowAddOwnTrades = value;
  }

  /// <summary>The order creation mode. The default is off.</summary>
  public bool OrderCreationMode
  {
    get => this.\u0023\u003DzO72kpz0\u003D.OrderCreationMode;
    set => this.\u0023\u003DzO72kpz0\u003D.OrderCreationMode = value;
  }

  /// <summary>The minimum number of visible candles.</summary>
  public int MinimumRange
  {
    get => this.\u0023\u003DzO72kpz0\u003D.MinimumRange;
    set => this.\u0023\u003DzO72kpz0\u003D.MinimumRange = value;
  }

  /// <summary>To show the preview area.</summary>
  public bool ShowOverview
  {
    get => this.\u0023\u003DzO72kpz0\u003D.ShowOverview;
    set => this.\u0023\u003DzO72kpz0\u003D.ShowOverview = value;
  }

  /// <summary>
  /// Local time zone for all <see cref="T:System.DateTimeOffset" /> values conversion.
  /// </summary>
  public TimeZoneInfo TimeZone
  {
    get => this.\u0023\u003DzO72kpz0\u003D.TimeZone;
    set => this.\u0023\u003DzO72kpz0\u003D.TimeZone = value;
  }

  /// <inheritdoc />
  public IList<IndicatorType> IndicatorTypes => this.\u0023\u003DzO72kpz0\u003D.IndicatorTypes;

  /// <inheritdoc />
  public bool ShowNonFormedIndicators
  {
    get => this.\u0023\u003DzO72kpz0\u003D.ShowNonFormedIndicators;
    set => this.\u0023\u003DzO72kpz0\u003D.ShowNonFormedIndicators = value;
  }

  /// <summary>Register candle style.</summary>
  /// <param name="image">Image.</param>
  /// <param name="getName">Name.</param>
  /// <param name="style">Style of candles rendering.</param>
  public static void RegisterCandleStyle(
    string image,
    Func<string> getName,
    ChartCandleDrawStyles style)
  {
    ChartPanel.\u0023\u003DzoQNjsvS8x7txZ62LTQ\u003D\u003D.Add(new ChartPanel.\u0023\u003DzDvqdDoIQ0lWXMU6f7Q\u003D\u003D(ThemedIconsExtension.GetImage(image), getName, style));
  }

  /// <summary>To re-subscribe to getting data for all elements.</summary>
  public void ReSubscribeElements() => this.\u0023\u003DzO72kpz0\u003D.ReSubscribeElements();

  /// <summary>Available time-frames.</summary>
  public ICollection<TimeSpan> TimeFrames => (ICollection<TimeSpan>) this.\u0023\u003DzMtehrzRObGK8;

  private async Task \u0023\u003DzziS13AZjpXXq(CancellationToken _param1)
  {
    ChartPanel chartPanel = this;
    try
    {
      LogManager logManager = ServicesRegistry.LogManager;
      if (logManager != null)
        LoggingHelper.AddInfoLog(logManager.Application, "", new object[1]
        {
          (object) chartPanel.ShareSettings.Period
        });
      await chartPanel.\u0023\u003DzbF_eohY\u003D(chartPanel.ShareSettings.FileName, !chartPanel.ShareSettings.Published, _param1);
      chartPanel.ShareSettings.Published = true;
      while (true)
      {
        await AsyncHelper.Delay(chartPanel.ShareSettings.Period, _param1);
        await chartPanel.\u0023\u003DzbF_eohY\u003D(chartPanel.ShareSettings.FileName, !chartPanel.ShareSettings.Published, _param1);
      }
    }
    catch (Exception ex)
    {
      if (!_param1.IsCancellationRequested)
        LoggingHelper.LogError(ex, (string) null);
      ((DispatcherObject) chartPanel).GuiAsync(new Action(chartPanel.\u0023\u003DzlnGV\u002439XCDOa6zsez698EeE\u003D));
    }
  }

  private void \u0023\u003DzWUtxOUt4gZ3\u0024(object _param1, PropertyChangedEventArgs _param2)
  {
    if (_param2.PropertyName == "")
    {
      this.\u0023\u003DzjOHdKU9O7LKI?.Cancel();
      this.\u0023\u003DzjOHdKU9O7LKI = (CancellationTokenSource) null;
      if (this.ShareSettings.IsEnabled)
      {
        this.\u0023\u003DzjOHdKU9O7LKI = new CancellationTokenSource();
        this.\u0023\u003DzziS13AZjpXXq(this.\u0023\u003DzjOHdKU9O7LKI.Token);
      }
    }
    this.\u0023\u003DzTV_qYxt_pth\u0024();
  }

  private void \u0023\u003Dz364KuOoNJb9k(object _param1, RoutedEventArgs _param2)
  {
    this.\u0023\u003DzTV_qYxt_pth\u0024();
  }

  private void \u0023\u003DzTV_qYxt_pth\u0024()
  {
    Action zm9MpWuc = this.\u0023\u003Dzm9MpWUc\u003D;
    if (zm9MpWuc == null)
      return;
    zm9MpWuc();
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

  /// <summary>Save settings.</summary>
  /// <param name="storage">Settings storage.</param>
  public void Save(SettingsStorage storage)
  {
    storage.SetValue<SettingsStorage>("", PersistableHelper.Save((IPersistable) this.OrderSettings));
    storage.SetValue<SettingsStorage>("", PersistableHelper.Save((IPersistable) this.ShareSettings));
    storage.SetValue<string>("", this.\u0023\u003DzK0Y33rI\u003D.SaveDevExpressControl());
    storage.SetValue<SettingsStorage>("", PersistableHelper.Save((IPersistable) this.\u0023\u003DzO72kpz0\u003D));
    storage.SetValue<SettingsStorage>("", PersistableHelper.Save((IPersistable) this.\u0023\u003DzW15YPW8yFbIF));
  }

  /// <summary>Load settings.</summary>
  /// <param name="storage">Settings storage.</param>
  public void Load(SettingsStorage storage)
  {
    this.\u0023\u003DzMJITrmQ\u003D = true;
    try
    {
      PersistableHelper.Load((IPersistable) this.OrderSettings, storage, "");
      PersistableHelper.Load((IPersistable) this.ShareSettings, storage, "");
      string settings = storage.GetValue<string>("", (string) null);
      if (settings != null)
        this.\u0023\u003DzK0Y33rI\u003D.LoadDevExpressControl(settings);
      PersistableHelper.LoadIfNotNull((IPersistable) this.\u0023\u003DzO72kpz0\u003D, storage, "");
      PersistableHelper.LoadIfNotNull((IPersistable) this.\u0023\u003DzW15YPW8yFbIF, storage, "");
      if (this.\u0023\u003DzO72kpz0\u003D.Areas.Count<IChartArea>() != 1)
        return;
      IChartCandleElement[] array = ((IEnumerable) this.\u0023\u003DzO72kpz0\u003D.Areas.First<IChartArea>().Elements).OfType<IChartCandleElement>().ToArray<IChartCandleElement>();
      if (array.Length != 1)
        return;
      ChartPanel.\u0023\u003DzDvqdDoIQ0lWXMU6f7Q\u003D\u003D dvqdDoIq0lWxmU6f7Q = ChartPanel.\u0023\u003DzoQNjsvS8x7txZ62LTQ\u003D\u003D.FirstOrDefault<ChartPanel.\u0023\u003DzDvqdDoIQ0lWXMU6f7Q\u003D\u003D>(new Func<ChartPanel.\u0023\u003DzDvqdDoIQ0lWXMU6f7Q\u003D\u003D, bool>(new ChartPanel.\u0023\u003DzMEy4LQ0NbEhx_JRMRyeGaCc\u003D()
      {
        \u0023\u003DzgQgmhv4\u003D = array[0].DrawStyle
      }.\u0023\u003DzyBox3lu1IEaSzP_YYQ\u003D\u003D));
      if (dvqdDoIq0lWxmU6f7Q == null)
        return;
      this.\u0023\u003DzAWra9\u0024_YgvQ6QTggug\u003D\u003D.EditValue = (object) dvqdDoIq0lWxmU6f7Q;
    }
    finally
    {
      this.\u0023\u003DzMJITrmQ\u003D = false;
    }
  }

  /// <inheritdoc />
  public IEnumerable<IChartArea> Areas => this.\u0023\u003DzO72kpz0\u003D.Areas;

  /// <inheritdoc />
  public bool IsAutoScroll
  {
    get => this.\u0023\u003DzO72kpz0\u003D.IsAutoScroll;
    set => this.\u0023\u003DzO72kpz0\u003D.IsAutoScroll = value;
  }

  /// <inheritdoc />
  public bool ShowLegend
  {
    get => this.\u0023\u003DzO72kpz0\u003D.ShowLegend;
    set => this.\u0023\u003DzO72kpz0\u003D.ShowLegend = value;
  }

  /// <inheritdoc />
  public bool CrossHair
  {
    get => this.\u0023\u003DzO72kpz0\u003D.CrossHair;
    set => this.\u0023\u003DzO72kpz0\u003D.CrossHair = value;
  }

  /// <inheritdoc />
  public bool CrossHairTooltip
  {
    get => this.\u0023\u003DzO72kpz0\u003D.CrossHairTooltip;
    set => this.\u0023\u003DzO72kpz0\u003D.CrossHairTooltip = value;
  }

  /// <inheritdoc />
  public bool CrossHairAxisLabels
  {
    get => this.\u0023\u003DzO72kpz0\u003D.CrossHairAxisLabels;
    set => this.\u0023\u003DzO72kpz0\u003D.CrossHairAxisLabels = value;
  }

  /// <inheritdoc />
  public void Reset(IEnumerable<IChartElement> elements)
  {
    this.\u0023\u003DzO72kpz0\u003D.Reset(elements);
  }

  /// <inheritdoc />
  public void Draw(IChartDrawData data) => this.\u0023\u003DzO72kpz0\u003D.Draw(data);

  /// <inheritdoc />
  public IChartDrawData CreateData() => this.\u0023\u003DzO72kpz0\u003D.CreateData();

  /// <inheritdoc />
  public IChartArea CreateArea() => this.\u0023\u003DzO72kpz0\u003D.CreateArea();

  /// <inheritdoc />
  public IChartAxis CreateAxis() => this.\u0023\u003DzO72kpz0\u003D.CreateAxis();

  /// <inheritdoc />
  public IChartCandleElement CreateCandleElement()
  {
    return this.\u0023\u003DzO72kpz0\u003D.CreateCandleElement();
  }

  /// <inheritdoc />
  public IChartIndicatorElement CreateIndicatorElement()
  {
    return this.\u0023\u003DzO72kpz0\u003D.CreateIndicatorElement();
  }

  /// <inheritdoc />
  public IChartActiveOrdersElement CreateActiveOrdersElement()
  {
    return this.\u0023\u003DzO72kpz0\u003D.CreateActiveOrdersElement();
  }

  /// <inheritdoc />
  public IChartAnnotationElement CreateAnnotation()
  {
    return this.\u0023\u003DzO72kpz0\u003D.CreateAnnotation();
  }

  /// <inheritdoc />
  public IChartBandElement CreateBandElement()
  {
    return this.\u0023\u003DzO72kpz0\u003D.CreateBandElement();
  }

  /// <inheritdoc />
  public IChartLineElement CreateLineElement()
  {
    return this.\u0023\u003DzO72kpz0\u003D.CreateLineElement();
  }

  /// <inheritdoc />
  public IChartLineElement CreateBubbleElement()
  {
    return this.\u0023\u003DzO72kpz0\u003D.CreateBubbleElement();
  }

  /// <inheritdoc />
  public IChartOrderElement CreateOrderElement()
  {
    return this.\u0023\u003DzO72kpz0\u003D.CreateOrderElement();
  }

  /// <inheritdoc />
  public IChartTradeElement CreateTradeElement()
  {
    return this.\u0023\u003DzO72kpz0\u003D.CreateTradeElement();
  }

  private void \u0023\u003DzJeZjj6FzeNO\u0024l09CVA\u003D\u003D(
    object _param1,
    ExecutedRoutedEventArgs _param2)
  {
  }

  private void \u0023\u003DzBcx3YH_M47a1yHOJnA\u003D\u003D(
    object _param1,
    CanExecuteRoutedEventArgs _param2)
  {
    bool flag = this.\u0023\u003DzXp03Nmz0CLV\u0024 != null && this.IsInteracted;
    _param2.CanExecute = flag;
    this.\u0023\u003DzVzfMDsVHAcU9.IsEnabled = flag;
    if (flag)
      return;
    this.\u0023\u003DzW15YPW8yFbIF.Visibility = Visibility.Collapsed;
  }

  private void \u0023\u003DzjmH6itbX\u0024nl0cmhkRco8Qf4\u003D(
    object _param1,
    ExecutedRoutedEventArgs _param2)
  {
    this.\u0023\u003DzO72kpz0\u003D.CancelOrders();
  }

  private void \u0023\u003DztVEm0I5eM07Umsor3nyxAfc\u003D(
    object _param1,
    CanExecuteRoutedEventArgs _param2)
  {
    bool flag = this.\u0023\u003DzmMdfCUCSnZWZ != null && this.IsInteracted;
    _param2.CanExecute = flag;
  }

  private async Task \u0023\u003DzbF_eohY\u003D(
    string _param1,
    bool _param2,
    CancellationToken _param3)
  {
    ChartPanel owner = this;
    await owner.ShareAsync(_param2, _param1, new Func<Stream>(((ChartHelper) owner.\u0023\u003DzO72kpz0\u003D).SaveToImage), _param3);
  }

  private void \u0023\u003Dz8D3q9zA9CpOr(object _param1, RoutedEventArgs _param2)
  {
    DXSaveFileDialog dxSaveFileDialog = new DXSaveFileDialog();
    dxSaveFileDialog.RestoreDirectory = true;
    dxSaveFileDialog.Filter = "";
    dxSaveFileDialog.DefaultExt = "";
    DXSaveFileDialog dlg = dxSaveFileDialog;
    if (!dlg.TryOpenWithInitialDir((DependencyObject) this, ""))
      return;
    string fileName = dlg.FileName;
    IOHelper.Save(this.\u0023\u003DzO72kpz0\u003D.SaveToImage(), fileName);
    if (new MessageBoxBuilder().Owner((DependencyObject) this).Caption(LocalizedStrings.Export).Text(StringHelper.Put(LocalizedStrings.ExportDoneOpenFile, new object[1]
    {
      (object) Path.GetFileName(fileName)
    })).YesNo().Show() != MessageBoxResult.Yes)
      return;
    fileName.TryOpenLink((DependencyObject) this);
  }

  private void \u0023\u003Dz5LU4_tehysCK8xcidYi2YPw\u003D(object _param1, RoutedEventArgs _param2)
  {
    ChartPanel.\u0023\u003Dz9avG6juy8sz5xvXs9gnmpIA\u003D g6juy8sz5xvXs9gnmpIa = new ChartPanel.\u0023\u003Dz9avG6juy8sz5xvXs9gnmpIA\u003D();
    g6juy8sz5xvXs9gnmpIa.\u0023\u003DzgQgmhv4\u003D = (ChartPanel.\u0023\u003DzDvqdDoIQ0lWXMU6f7Q\u003D\u003D) this.\u0023\u003DzAWra9\u0024_YgvQ6QTggug\u003D\u003D.EditValue;
    if (g6juy8sz5xvXs9gnmpIa.\u0023\u003DzgQgmhv4\u003D != null)
      this.\u0023\u003DzO72kpz0\u003D.\u0023\u003DzvqeJB240RXk9(g6juy8sz5xvXs9gnmpIa.\u0023\u003DzgQgmhv4\u003D.DrawStyle);
    if (this.\u0023\u003DzMJITrmQ\u003D)
      return;
    CollectionHelper.ForEach<IChartCandleElement>(this.\u0023\u003DzO72kpz0\u003D.Areas.SelectMany<IChartArea, IChartElement>(ChartPanel.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzA23GjvIGUxigab9gDw\u003D\u003D ?? (ChartPanel.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzA23GjvIGUxigab9gDw\u003D\u003D = new Func<IChartArea, IEnumerable<IChartElement>>(ChartPanel.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzYvkVKOh6kY\u0024W7ARAS\u0024RZ2ZLc6Hzq3nufL5x4rb8\u003D))).OfType<IChartCandleElement>(), new Action<IChartCandleElement>(g6juy8sz5xvXs9gnmpIa.\u0023\u003DzhCrYxgzXy5_i82jHuEXWBKhZOVBJFTHN\u0024A\u003D\u003D));
  }

  private async void \u0023\u003DzQRGEhax_Ac2y(object _param1, ExecutedRoutedEventArgs _param2)
  {
    ChartPanel owner = this;
    try
    {
      string str1;
      if (!owner.ShareSettings.IsEnabled)
      {
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 1);
        interpolatedStringHandler.AppendLiteral("");
        interpolatedStringHandler.AppendFormatted<DateTime>(DateTime.Now, "");
        interpolatedStringHandler.AppendLiteral("");
        str1 = interpolatedStringHandler.ToStringAndClear();
      }
      else
        str1 = owner.ShareSettings.FileName;
      string str2 = str1;
      await owner.\u0023\u003DzbF_eohY\u003D(str2, true, CancellationToken.None);
    }
    catch (Exception ex)
    {
      int num = (int) new MessageBoxBuilder().Text(ex.ToString()).Caption(LocalizedStrings.ShareFile).Owner((DependencyObject) owner).Error().Show();
    }
  }

  private void \u0023\u003DzteilzWSl4NiW\u0024LAMOA\u003D\u003D(
    object _param1,
    CanExecuteRoutedEventArgs _param2)
  {
    _param2.CanExecute = true;
  }

  private void \u0023\u003Dz7ofo7nE6G\u0024U\u00245Hbg5sj83No\u003D(Order _param1)
  {
    _param1.Portfolio = this.OrderSettings.Portfolio;
    Action<IChartArea, Order> zXp03Nmz0Clv = this.\u0023\u003DzXp03Nmz0CLV\u0024;
    if (zXp03Nmz0Clv == null)
      return;
    zXp03Nmz0Clv(this.Areas.FirstOrDefault<IChartArea>(), _param1);
  }

  private void \u0023\u003Dzn5sQPBgCyW1A(object _param1, RoutedEventArgs _param2)
  {
    if (this.\u0023\u003DzCm\u0024a_RJSIzjb8Je3X21wN90\u003D)
      return;
    this.\u0023\u003DzCm\u0024a_RJSIzjb8Je3X21wN90\u003D = true;
    try
    {
      if (!(this.\u0023\u003Dzb98_9e4umo9R.EditValue is TimeSpan editValue) || !(editValue > TimeSpan.Zero))
        return;
      this.\u0023\u003DzO72kpz0\u003D.\u0023\u003DzBF\u0024LAMIgiEWk(editValue);
    }
    finally
    {
      this.\u0023\u003DzCm\u0024a_RJSIzjb8Je3X21wN90\u003D = false;
    }
  }

  private void \u0023\u003DzfU3Fd_StOx0y3VLx1m\u0024UMKE\u003D()
  {
    if (this.\u0023\u003DzCm\u0024a_RJSIzjb8Je3X21wN90\u003D)
      return;
    this.\u0023\u003DzCm\u0024a_RJSIzjb8Je3X21wN90\u003D = true;
    try
    {
      Subscription subscription = this.\u0023\u003DzO72kpz0\u003D.\u0023\u003DzgDVjqFRN8sR7();
      if (subscription != null && subscription.DataType.Arg is TimeSpan timeSpan)
      {
        if (this.\u0023\u003DzMtehrzRObGK8.Contains(timeSpan))
          this.\u0023\u003Dzb98_9e4umo9R.EditValue = (object) timeSpan;
        else
          this.\u0023\u003Dzb98_9e4umo9R.EditValue = (object) this.\u0023\u003DzMtehrzRObGK8[0];
      }
      else
        this.\u0023\u003Dzb98_9e4umo9R.EditValue = (object) this.\u0023\u003DzMtehrzRObGK8[0];
    }
    finally
    {
      this.\u0023\u003DzCm\u0024a_RJSIzjb8Je3X21wN90\u003D = false;
    }
  }

  private void \u0023\u003DzXmrWPrJZ7kp0ivUhHw\u003D\u003D(
    object _param1,
    CanExecuteRoutedEventArgs _param2)
  {
    _param2.CanExecute = this.IsInteracted;
  }

  private void \u0023\u003DzCXbrndwu4ONN(object _param1, ExecutedRoutedEventArgs _param2)
  {
    IChartArea area = this.Areas.FirstOrDefault<IChartArea>();
    if (area == null)
      return;
    ChartCandleElement element1 = ((IEnumerable) area.Elements).OfType<ChartCandleElement>().FirstOrDefault<ChartCandleElement>();
    if (element1 == null)
      return;
    CandlePatternWindow wnd = new CandlePatternWindow();
    if (!wnd.ShowModal((DependencyObject) this))
      return;
    CandlePatternIndicator patternIndicator = new CandlePatternIndicator()
    {
      Pattern = wnd.Pattern
    };
    ChartIndicatorElement element2 = new ChartIndicatorElement()
    {
      IndicatorPainter = (IChartIndicatorPainter) new CandlePatternIndicatorPainter(),
      AutoAssignYAxis = true
    };
    this.\u0023\u003DzO72kpz0\u003D.AddElement(area, (IChartIndicatorElement) element2, this.\u0023\u003DzO72kpz0\u003D.TryGetSubscription((IChartElement) element1), (IIndicator) patternIndicator);
  }

  private void \u0023\u003DzjnT64LWuMjR0t1\u002435A\u003D\u003D(
    object _param1,
    ItemClickEventArgs _param2)
  {
    this.ShareSettings.ShowSettingsWindow<ChartPanelShareSettings>((DependencyObject) this);
  }

  private void \u0023\u003DzGLJoGYRM3luJyepj1Q\u003D\u003D(
    object _param1,
    ItemClickEventArgs _param2)
  {
    this.OrderSettings.ShowSettingsWindow<ChartPanelOrderSettings>((DependencyObject) this);
  }

  /// <summary>InitializeComponent</summary>
  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  public void InitializeComponent()
  {
    if (this.\u0023\u003DzQGCmQMjHdLKS)
      return;
    this.\u0023\u003DzQGCmQMjHdLKS = true;
    Application.LoadComponent((object) this, new Uri("", UriKind.Relative));
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
    switch (connectionId)
    {
      case 1:
        this.\u0023\u003Dzv4BS1WQ\u003D = (ChartPanel) target;
        break;
      case 2:
        ((CommandBinding) target).Executed += new ExecutedRoutedEventHandler(this.\u0023\u003DzJeZjj6FzeNO\u0024l09CVA\u003D\u003D);
        ((CommandBinding) target).CanExecute += new CanExecuteRoutedEventHandler(this.\u0023\u003DzBcx3YH_M47a1yHOJnA\u003D\u003D);
        break;
      case 3:
        ((CommandBinding) target).Executed += new ExecutedRoutedEventHandler(this.\u0023\u003DzQRGEhax_Ac2y);
        ((CommandBinding) target).CanExecute += new CanExecuteRoutedEventHandler(this.\u0023\u003DzteilzWSl4NiW\u0024LAMOA\u003D\u003D);
        break;
      case 4:
        ((CommandBinding) target).Executed += new ExecutedRoutedEventHandler(this.\u0023\u003DzjmH6itbX\u0024nl0cmhkRco8Qf4\u003D);
        ((CommandBinding) target).CanExecute += new CanExecuteRoutedEventHandler(this.\u0023\u003DztVEm0I5eM07Umsor3nyxAfc\u003D);
        break;
      case 5:
        ((CommandBinding) target).Executed += new ExecutedRoutedEventHandler(this.\u0023\u003DzCXbrndwu4ONN);
        ((CommandBinding) target).CanExecute += new CanExecuteRoutedEventHandler(this.\u0023\u003DzXmrWPrJZ7kp0ivUhHw\u003D\u003D);
        break;
      case 6:
        this.\u0023\u003DzW15YPW8yFbIF = (QuickOrderPanel) target;
        this.\u0023\u003DzW15YPW8yFbIF.RegisterOrder += new Action<Order>(this.\u0023\u003Dz7ofo7nE6G\u0024U\u00245Hbg5sj83No\u003D);
        break;
      case 7:
        this.\u0023\u003DzK0Y33rI\u003D = (BarManager) target;
        break;
      case 8:
        this.\u0023\u003DzAWra9\u0024_YgvQ6QTggug\u003D\u003D = (BarEditItem) target;
        this.\u0023\u003DzAWra9\u0024_YgvQ6QTggug\u003D\u003D.EditValueChanged += new RoutedEventHandler(this.\u0023\u003Dz5LU4_tehysCK8xcidYi2YPw\u003D);
        break;
      case 9:
        this.\u0023\u003Dz5wj51j\u0024m7KHERsE\u0024DA\u003D\u003D = (ComboBoxEditSettings) target;
        break;
      case 10:
        this.\u0023\u003Dzb98_9e4umo9R = (BarEditItem) target;
        this.\u0023\u003Dzb98_9e4umo9R.EditValueChanged += new RoutedEventHandler(this.\u0023\u003Dzn5sQPBgCyW1A);
        break;
      case 11:
        this.\u0023\u003Dz1jt7bIJr0f0_ = (ComboBoxEditSettings) target;
        break;
      case 12:
        ((BarItem) target).ItemClick += new ItemClickEventHandler(this.\u0023\u003Dz364KuOoNJb9k);
        break;
      case 13:
        ((BarItem) target).ItemClick += new ItemClickEventHandler(this.\u0023\u003Dz364KuOoNJb9k);
        break;
      case 14:
        ((BarItem) target).ItemClick += new ItemClickEventHandler(this.\u0023\u003Dz364KuOoNJb9k);
        break;
      case 15:
        ((BarItem) target).ItemClick += new ItemClickEventHandler(this.\u0023\u003Dz364KuOoNJb9k);
        break;
      case 16 /*0x10*/:
        ((BarItem) target).ItemClick += new ItemClickEventHandler(this.\u0023\u003Dz364KuOoNJb9k);
        break;
      case 17:
        ((BarItem) target).ItemClick += new ItemClickEventHandler(this.\u0023\u003Dz364KuOoNJb9k);
        break;
      case 18:
        ((BarItem) target).ItemClick += new ItemClickEventHandler(this.\u0023\u003Dz364KuOoNJb9k);
        break;
      case 19:
        ((BarItem) target).ItemClick += new ItemClickEventHandler(this.\u0023\u003Dz364KuOoNJb9k);
        break;
      case 20:
        ((BarItem) target).ItemClick += new ItemClickEventHandler(this.\u0023\u003Dz8D3q9zA9CpOr);
        break;
      case 21:
        ((BarItem) target).ItemClick += new ItemClickEventHandler(this.\u0023\u003DzjnT64LWuMjR0t1\u002435A\u003D\u003D);
        break;
      case 23:
        ((BarItem) target).ItemClick += new ItemClickEventHandler(this.\u0023\u003DzGLJoGYRM3luJyepj1Q\u003D\u003D);
        break;
      case 24:
        this.\u0023\u003DzVzfMDsVHAcU9 = (BarCheckItem) target;
        break;
      case 25:
        this.\u0023\u003DzO72kpz0\u003D = (Chart) target;
        break;
      default:
        this.\u0023\u003DzQGCmQMjHdLKS = true;
        break;
    }
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IStyleConnector.Connect(int connectionId, object target)
  {
    if (connectionId != 22)
      return;
    ((ButtonBase) target).Click += new RoutedEventHandler(this.\u0023\u003Dz364KuOoNJb9k);
  }

  private void \u0023\u003DzwOtSTPwtINP9b1tYlTf9WvY\u003D(
    #nullable enable
    object? _param1,
    PropertyChangedEventArgs _param2)
  {
    this.\u0023\u003DzTV_qYxt_pth\u0024();
  }

  private void \u0023\u003DzmbbCBPeSAsRFiiWslELKWfY\u003D(
  #nullable disable
  ChartArea _param1, Order _param2)
  {
    if (this.OrderSettings.Portfolio == null)
    {
      PortfolioPickerWindow wnd = new PortfolioPickerWindow();
      PortfolioDataSource tryInstance = PortfolioDataSource.TryInstance;
      if (tryInstance != null)
        wnd.Portfolios = tryInstance;
      if (wnd.ShowModal((DependencyObject) this))
        this.OrderSettings.Portfolio = wnd.SelectedPortfolio;
    }
    IChartCandleElement[] array = ((IEnumerable) _param1.Elements).OfType<IChartCandleElement>().ToArray<IChartCandleElement>();
    Order order = _param2;
    Security security;
    if (array.Length != 1)
    {
      security = this.OrderSettings.Security;
    }
    else
    {
      Subscription subscription = this.\u0023\u003DzO72kpz0\u003D.TryGetSubscription((IChartElement) array[0]);
      security = (subscription != null ? subscription.TryGetSecurity(this.SecurityProvider) : (Security) null) ?? this.OrderSettings.Security;
    }
    order.Security = security;
    _param2.Portfolio = this.OrderSettings.Portfolio;
    _param2.Volume = this.OrderSettings.Volume;
    if (((Decimal?) _param2.Security?.PriceStep).HasValue)
      _param2.Price = _param2.Security.ShrinkPrice(_param2.Price);
    Action<IChartArea, Order> zXp03Nmz0Clv = this.\u0023\u003DzXp03Nmz0CLV\u0024;
    if (zXp03Nmz0Clv == null)
      return;
    zXp03Nmz0Clv((IChartArea) _param1, _param2);
  }

  private void \u0023\u003DzsIUgZC\u0024bsMzwntYtdPR0Dww\u003D(Order _param1, Decimal _param2)
  {
    Action<Order, Decimal> zJiM5nvc = this.\u0023\u003DzJIM5nvc\u003D;
    if (zJiM5nvc == null)
      return;
    zJiM5nvc(_param1, _param2);
  }

  private void \u0023\u003Dzg1OGYvQtC93XdkFX1\u002444hrw\u003D(Order _param1)
  {
    Action<Order> zmMdfCucSnZwz = this.\u0023\u003DzmMdfCUCSnZWZ;
    if (zmMdfCucSnZwz == null)
      return;
    zmMdfCucSnZwz(_param1);
  }

  private void \u0023\u003Dzam5mZvc5ePT\u0024xXJJL7L3riM\u003D(IChartAnnotationElement _param1)
  {
    Action<IChartAnnotationElement> z6KsbJRt22Hb = this.\u0023\u003Dz6KSbJ_RT22HB;
    if (z6KsbJRt22Hb == null)
      return;
    z6KsbJRt22Hb(_param1);
  }

  private void \u0023\u003DzJovCO\u0024\u0024Un0QKO1u02HWxPEc\u003D(
    IChartAnnotationElement _param1,
    ChartDrawData.AnnotationData _param2)
  {
    Action<IChartAnnotationElement, ChartDrawData.AnnotationData> zygdSp72uKvhL = this.\u0023\u003DzygdSp72uKVhL;
    if (zygdSp72uKvhL == null)
      return;
    zygdSp72uKvhL(_param1, _param2);
  }

  private void \u0023\u003DzxEMEGl05Lk7X5Tl\u0024DugWerI\u003D(IChartAnnotationElement _param1)
  {
    Action<IChartAnnotationElement> z53l3VmDrGxpJ = this.\u0023\u003Dz53l3VMDrGxpJ;
    if (z53l3VmDrGxpJ == null)
      return;
    z53l3VmDrGxpJ(_param1);
  }

  private void \u0023\u003Dz8k9gDPsUiUIXL\u00242BjkEu8Hc\u003D(
    IChartAnnotationElement _param1,
    ChartDrawData.AnnotationData _param2)
  {
    Action<IChartAnnotationElement, ChartDrawData.AnnotationData> zxVqsLo94Ea68 = this.\u0023\u003DzxVqsLO94Ea68;
    if (zxVqsLo94Ea68 == null)
      return;
    zxVqsLo94Ea68(_param1, _param2);
  }

  private void \u0023\u003Dz4wQzbf0WsLc5YzDDh44eqC4\u003D(
    IChartCandleElement _param1,
    Subscription _param2)
  {
    Action<IChartCandleElement, Subscription> wdiUyNemvFqkVapq = this.\u0023\u003DzlWdi\u0024uyNemvFQkVAPQ\u003D\u003D;
    if (wdiUyNemvFqkVapq == null)
      return;
    wdiUyNemvFqkVapq(_param1, _param2);
  }

  private void \u0023\u003DzcNKrDKPhRiLWRqFNhpXIukg\u003D(
    IChartIndicatorElement _param1,
    Subscription _param2,
    IIndicator _param3)
  {
    Action<IChartIndicatorElement, Subscription, IIndicator> zLdfE1FxkiHdr = this.\u0023\u003DzLDFE1FXkiHDr;
    if (zLdfE1FxkiHdr == null)
      return;
    zLdfE1FxkiHdr(_param1, _param2, _param3);
  }

  private void \u0023\u003Dz1aDyDm3zg_Rs2wBbVgacVkc\u003D(
    IChartOrderElement _param1,
    Subscription _param2)
  {
    Action<IChartOrderElement, Subscription> zh7nXgYWoKl = this.\u0023\u003Dzh7nXgY\u0024WoKL\u0024;
    if (zh7nXgYWoKl == null)
      return;
    zh7nXgYWoKl(_param1, _param2);
  }

  private void \u0023\u003DzKAwimyvAzaY\u0024AMPMBCjXySc\u003D(
    IChartTradeElement _param1,
    Subscription _param2)
  {
    Action<IChartTradeElement, Subscription> ssvKvae0LsR0LbUsEg = this.\u0023\u003DzSSvKVae0LsR0LbUSEg\u003D\u003D;
    if (ssvKvae0LsR0LbUsEg == null)
      return;
    ssvKvae0LsR0LbUsEg(_param1, _param2);
  }

  private void \u0023\u003Dzk9_ZG7Umjd_3nlcCpYJ42Hc\u003D(IChartElement _param1)
  {
    Action<IChartElement> z9PnYjM29SjfT = this.\u0023\u003Dz9PnYjM29SjfT;
    if (z9PnYjM29SjfT == null)
      return;
    z9PnYjM29SjfT(_param1);
  }

  private void \u0023\u003DzP98lXRlARNrXZxrvU5arwx8\u003D(IChartArea _param1)
  {
    Action zm9MpWuc = this.\u0023\u003Dzm9MpWUc\u003D;
    if (zm9MpWuc == null)
      return;
    zm9MpWuc();
  }

  private void \u0023\u003Dzwg4hEbZtsdrK12xCFTj_Fyg\u003D(IChartArea _param1)
  {
    Action<IChartArea> zk8cjLwfRrDki = this.\u0023\u003Dzk8cjLWfRrDKI;
    if (zk8cjLwfRrDki == null)
      return;
    zk8cjLwfRrDki(_param1);
  }

  private void \u0023\u003Dz_zYN8JxCohXIKNL2XV\u0024B6P4\u003D(IChartArea _param1)
  {
    Action<IChartArea> z0aBkRs4Mkj0a = this.\u0023\u003Dz0aBkRs4Mkj0a;
    if (z0aBkRs4Mkj0a == null)
      return;
    z0aBkRs4Mkj0a(_param1);
  }

  private void \u0023\u003DzjfWxqcPIhQTyy4aKxIZ6mn8\u003D()
  {
    this.\u0023\u003DzW15YPW8yFbIF.Security = this.\u0023\u003DzO72kpz0\u003D.\u0023\u003Dz3OPaBTitYMD\u0024();
    this.\u0023\u003DzfU3Fd_StOx0y3VLx1m\u0024UMKE\u003D();
  }

  private void \u0023\u003DzJ2e2vCmK2SDuOdabPwfX1Ms\u003D()
  {
    Action zm9MpWuc = this.\u0023\u003Dzm9MpWUc\u003D;
    if (zm9MpWuc == null)
      return;
    zm9MpWuc();
  }

  private void \u0023\u003DzlnGV\u002439XCDOa6zsez698EeE\u003D()
  {
    this.ShareSettings.IsEnabled = false;
  }

  [Serializable]
  private sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly ChartPanel.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new ChartPanel.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<IChartArea, 
    #nullable enable
    IEnumerable<
    #nullable disable
    IChartElement>> \u0023\u003DzA23GjvIGUxigab9gDw\u003D\u003D;

    internal string \u0023\u003Dz9FyKEnqWZMl35lW1Mh8Pfl4\u003D() => LocalizedStrings.Bars;

    internal string \u0023\u003DzlXKB39\u0024_RGvuXSNaoFYbUfY\u003D()
    {
      return LocalizedStrings.CandleStick;
    }

    internal string \u0023\u003Dz3VNaKjF0Px9ZE74bMBlf8Jc\u003D() => LocalizedStrings.LineOpen;

    internal string \u0023\u003DzPwX1fdUl8qrydTYdTH_dK8E\u003D() => LocalizedStrings.LineClose;

    internal string \u0023\u003DzKNZn5frBPZ2lkm_QkzXTr78\u003D() => LocalizedStrings.LineHigh;

    internal string \u0023\u003DzatM8lF9_VANXs9rCA\u0024nAIFo\u003D() => LocalizedStrings.LineLow;

    internal string \u0023\u003Dz6DsZRppqVaGqhjmGQ982Snc\u003D() => LocalizedStrings.Area;

    internal string \u0023\u003DzHdtpgWUYDJsY5xnfHe5\u0024iZQ\u003D() => LocalizedStrings.BoxChart;

    internal string \u0023\u003DzgUhRURp7DzSchtEQx_odDF4\u003D() => LocalizedStrings.ClusterProfile;

    internal string \u0023\u003DzBs0U8AxGBth7PQG2Cppyd9U\u003D() => LocalizedStrings.PnFCandle;

    internal 
    #nullable enable
    IEnumerable<
    #nullable disable
    IChartElement> \u0023\u003DzYvkVKOh6kY\u0024W7ARAS\u0024RZ2ZLc6Hzq3nufL5x4rb8\u003D(
      IChartArea _param1)
    {
      return (IEnumerable<IChartElement>) _param1.Elements;
    }
  }

  private sealed class \u0023\u003Dz9avG6juy8sz5xvXs9gnmpIA\u003D
  {
    public ChartPanel.\u0023\u003DzDvqdDoIQ0lWXMU6f7Q\u003D\u003D \u0023\u003DzgQgmhv4\u003D;

    internal void \u0023\u003DzhCrYxgzXy5_i82jHuEXWBKhZOVBJFTHN\u0024A\u003D\u003D(
      IChartCandleElement _param1)
    {
      _param1.DrawStyle = this.\u0023\u003DzgQgmhv4\u003D.DrawStyle;
    }
  }

  private sealed class \u0023\u003DzDvqdDoIQ0lWXMU6f7Q\u003D\u003D : NotifiableObject
  {
    
    private readonly Func<string> \u0023\u003DzmTOWZCo\u003D;
    
    private readonly ImageSource \u0023\u003DzPbY9ar5NYhJiWDJXAA\u003D\u003D;
    
    private readonly ChartCandleDrawStyles \u0023\u003DzzSzfw_S6SqVya\u0024wNvx65bYg\u003D;

    public \u0023\u003DzDvqdDoIQ0lWXMU6f7Q\u003D\u003D(
      ImageSource _param1,
      Func<string> _param2,
      ChartCandleDrawStyles _param3)
    {
      this.\u0023\u003DzPbY9ar5NYhJiWDJXAA\u003D\u003D = _param1 ?? throw new ArgumentNullException("");
      this.\u0023\u003DzmTOWZCo\u003D = _param2 ?? throw new ArgumentNullException("");
      this.\u0023\u003DzzSzfw_S6SqVya\u0024wNvx65bYg\u003D = _param3;
      LocalizedStrings.ActiveLanguageChanged += new Action(this.\u0023\u003DzotqVzZA_MaXV);
    }

    private void \u0023\u003DzotqVzZA_MaXV()
    {
      this.NotifyChanged("");
    }

    public ImageSource Image => this.\u0023\u003DzPbY9ar5NYhJiWDJXAA\u003D\u003D;

    public string Name => this.\u0023\u003DzmTOWZCo\u003D();

    public ChartCandleDrawStyles DrawStyle => this.\u0023\u003DzzSzfw_S6SqVya\u0024wNvx65bYg\u003D;
  }

  [StructLayout(LayoutKind.Auto)]
  private struct \u0023\u003DzI7z_8L99LsAlFQfWVphsFdc\u003D : IAsyncStateMachine
  {
    
    public int \u0023\u003Dz4fzyEZ1SsHYa;
    
    public AsyncTaskMethodBuilder \u0023\u003DzvW8JtmzNnvTa;
    
    public ChartPanel \u0023\u003DzRRvwDu67s9Rm;
    
    public CancellationToken \u0023\u003Dzf32vx1Q\u003D;
    
    private TaskAwaiter \u0023\u003DzS68_TbFJ1FlN;

    void IAsyncStateMachine.MoveNext()
    {
      int z4fzyEz1SsHya = this.\u0023\u003Dz4fzyEZ1SsHYa;
      ChartPanel zRrvwDu67s9Rm = this.\u0023\u003DzRRvwDu67s9Rm;
      try
      {
        try
        {
          TaskAwaiter awaiter;
          int num;
          switch (z4fzyEz1SsHya)
          {
            case 0:
              awaiter = this.\u0023\u003DzS68_TbFJ1FlN;
              this.\u0023\u003DzS68_TbFJ1FlN = new TaskAwaiter();
              this.\u0023\u003Dz4fzyEZ1SsHYa = num = -1;
              break;
            case 1:
              awaiter = this.\u0023\u003DzS68_TbFJ1FlN;
              this.\u0023\u003DzS68_TbFJ1FlN = new TaskAwaiter();
              this.\u0023\u003Dz4fzyEZ1SsHYa = num = -1;
              goto label_11;
            case 2:
              awaiter = this.\u0023\u003DzS68_TbFJ1FlN;
              this.\u0023\u003DzS68_TbFJ1FlN = new TaskAwaiter();
              this.\u0023\u003Dz4fzyEZ1SsHYa = num = -1;
              goto label_14;
            default:
              LogManager logManager = ServicesRegistry.LogManager;
              if (logManager != null)
                LoggingHelper.AddInfoLog(logManager.Application, "", new object[1]
                {
                  (object) zRrvwDu67s9Rm.ShareSettings.Period
                });
              awaiter = zRrvwDu67s9Rm.\u0023\u003DzbF_eohY\u003D(zRrvwDu67s9Rm.ShareSettings.FileName, !zRrvwDu67s9Rm.ShareSettings.Published, this.\u0023\u003Dzf32vx1Q\u003D).GetAwaiter();
              if (!awaiter.IsCompleted)
              {
                this.\u0023\u003Dz4fzyEZ1SsHYa = num = 0;
                this.\u0023\u003DzS68_TbFJ1FlN = awaiter;
                this.\u0023\u003DzvW8JtmzNnvTa.AwaitUnsafeOnCompleted<TaskAwaiter, ChartPanel.\u0023\u003DzI7z_8L99LsAlFQfWVphsFdc\u003D>(ref awaiter, ref this);
                return;
              }
              break;
          }
          awaiter.GetResult();
          zRrvwDu67s9Rm.ShareSettings.Published = true;
label_8:
          awaiter = AsyncHelper.Delay(zRrvwDu67s9Rm.ShareSettings.Period, this.\u0023\u003Dzf32vx1Q\u003D).GetAwaiter();
          if (!awaiter.IsCompleted)
          {
            this.\u0023\u003Dz4fzyEZ1SsHYa = num = 1;
            this.\u0023\u003DzS68_TbFJ1FlN = awaiter;
            this.\u0023\u003DzvW8JtmzNnvTa.AwaitUnsafeOnCompleted<TaskAwaiter, ChartPanel.\u0023\u003DzI7z_8L99LsAlFQfWVphsFdc\u003D>(ref awaiter, ref this);
            return;
          }
label_11:
          awaiter.GetResult();
          awaiter = zRrvwDu67s9Rm.\u0023\u003DzbF_eohY\u003D(zRrvwDu67s9Rm.ShareSettings.FileName, !zRrvwDu67s9Rm.ShareSettings.Published, this.\u0023\u003Dzf32vx1Q\u003D).GetAwaiter();
          if (!awaiter.IsCompleted)
          {
            this.\u0023\u003Dz4fzyEZ1SsHYa = num = 2;
            this.\u0023\u003DzS68_TbFJ1FlN = awaiter;
            this.\u0023\u003DzvW8JtmzNnvTa.AwaitUnsafeOnCompleted<TaskAwaiter, ChartPanel.\u0023\u003DzI7z_8L99LsAlFQfWVphsFdc\u003D>(ref awaiter, ref this);
            return;
          }
label_14:
          awaiter.GetResult();
          goto label_8;
        }
        catch (Exception ex)
        {
          if (!this.\u0023\u003Dzf32vx1Q\u003D.IsCancellationRequested)
            LoggingHelper.LogError(ex, (string) null);
          ((DispatcherObject) zRrvwDu67s9Rm).GuiAsync(new Action(zRrvwDu67s9Rm.\u0023\u003DzlnGV\u002439XCDOa6zsez698EeE\u003D));
        }
      }
      catch (Exception ex)
      {
        this.\u0023\u003Dz4fzyEZ1SsHYa = -2;
        this.\u0023\u003DzvW8JtmzNnvTa.SetException(ex);
        return;
      }
      this.\u0023\u003Dz4fzyEZ1SsHYa = -2;
      this.\u0023\u003DzvW8JtmzNnvTa.SetResult();
    }

    [DebuggerHidden]
    void IAsyncStateMachine.SetStateMachine(
    #nullable enable
    IAsyncStateMachine _param1) => this.\u0023\u003DzvW8JtmzNnvTa.SetStateMachine(_param1);
  }

  private sealed class \u0023\u003DzMEy4LQ0NbEhx_JRMRyeGaCc\u003D
  {
    public ChartCandleDrawStyles \u0023\u003DzgQgmhv4\u003D;

    internal bool \u0023\u003DzyBox3lu1IEaSzP_YYQ\u003D\u003D(
      #nullable disable
      ChartPanel.\u0023\u003DzDvqdDoIQ0lWXMU6f7Q\u003D\u003D _param1)
    {
      return _param1.DrawStyle == this.\u0023\u003DzgQgmhv4\u003D;
    }
  }

  private sealed class \u0023\u003DzMwuV1aTxCngvmYEggNUKzD8\u003D
  {
    public string[] \u0023\u003DzwqcAXcxvUggD;
    public ChartPanel \u0023\u003DzRRvwDu67s9Rm;

    internal void \u0023\u003DzpgDygB5ErIl1Nu7QHw\u003D\u003D(
      #nullable enable
      object? _param1,
      PropertyChangedEventArgs _param2)
    {
      if (!((IEnumerable<string>) this.\u0023\u003DzwqcAXcxvUggD).Contains<string>(_param2.PropertyName))
        return;
      ((INotifyPropertyChangedEx) this.\u0023\u003DzRRvwDu67s9Rm).NotifyPropertyChanged(_param2.PropertyName);
    }
  }

  [StructLayout(LayoutKind.Auto)]
  private struct \u0023\u003DzTcq_yS5TSFVn96767oyYZ8s\u003D : IAsyncStateMachine
  {
    
    public int \u0023\u003Dz4fzyEZ1SsHYa;
    
    public AsyncVoidMethodBuilder \u0023\u003DzvW8JtmzNnvTa;
    
    public 
    #nullable disable
    ChartPanel \u0023\u003DzRRvwDu67s9Rm;
    
    private TaskAwaiter \u0023\u003DzS68_TbFJ1FlN;

    void IAsyncStateMachine.MoveNext()
    {
      int z4fzyEz1SsHya = this.\u0023\u003Dz4fzyEZ1SsHYa;
      ChartPanel zRrvwDu67s9Rm = this.\u0023\u003DzRRvwDu67s9Rm;
      try
      {
        try
        {
          TaskAwaiter awaiter;
          int num;
          if (z4fzyEz1SsHya != 0)
          {
            string str1;
            if (!zRrvwDu67s9Rm.ShareSettings.IsEnabled)
            {
              DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 1);
              interpolatedStringHandler.AppendLiteral("");
              interpolatedStringHandler.AppendFormatted<DateTime>(DateTime.Now, "");
              interpolatedStringHandler.AppendLiteral("");
              str1 = interpolatedStringHandler.ToStringAndClear();
            }
            else
              str1 = zRrvwDu67s9Rm.ShareSettings.FileName;
            string str2 = str1;
            awaiter = zRrvwDu67s9Rm.\u0023\u003DzbF_eohY\u003D(str2, true, CancellationToken.None).GetAwaiter();
            if (!awaiter.IsCompleted)
            {
              this.\u0023\u003Dz4fzyEZ1SsHYa = num = 0;
              this.\u0023\u003DzS68_TbFJ1FlN = awaiter;
              this.\u0023\u003DzvW8JtmzNnvTa.AwaitUnsafeOnCompleted<TaskAwaiter, ChartPanel.\u0023\u003DzTcq_yS5TSFVn96767oyYZ8s\u003D>(ref awaiter, ref this);
              return;
            }
          }
          else
          {
            awaiter = this.\u0023\u003DzS68_TbFJ1FlN;
            this.\u0023\u003DzS68_TbFJ1FlN = new TaskAwaiter();
            this.\u0023\u003Dz4fzyEZ1SsHYa = num = -1;
          }
          awaiter.GetResult();
        }
        catch (Exception ex)
        {
          int num = (int) new MessageBoxBuilder().Text(ex.ToString()).Caption(LocalizedStrings.ShareFile).Owner((DependencyObject) zRrvwDu67s9Rm).Error().Show();
        }
      }
      catch (Exception ex)
      {
        this.\u0023\u003Dz4fzyEZ1SsHYa = -2;
        this.\u0023\u003DzvW8JtmzNnvTa.SetException(ex);
        return;
      }
      this.\u0023\u003Dz4fzyEZ1SsHYa = -2;
      this.\u0023\u003DzvW8JtmzNnvTa.SetResult();
    }

    [DebuggerHidden]
    void IAsyncStateMachine.SetStateMachine(
    #nullable enable
    IAsyncStateMachine _param1) => this.\u0023\u003DzvW8JtmzNnvTa.SetStateMachine(_param1);
  }

  [StructLayout(LayoutKind.Auto)]
  private struct \u0023\u003DzZgO8RIPoYDQT9H_pgQ\u003D\u003D : IAsyncStateMachine
  {
    
    public int \u0023\u003Dz4fzyEZ1SsHYa;
    
    public AsyncTaskMethodBuilder \u0023\u003DzvW8JtmzNnvTa;
    
    public 
    #nullable disable
    ChartPanel \u0023\u003DzRRvwDu67s9Rm;
    
    public bool \u0023\u003Dz5kxxfgM\u003D;
    
    public string \u0023\u003DznVxnqZE\u003D;
    
    public CancellationToken \u0023\u003Dzf32vx1Q\u003D;
    
    private TaskAwaiter \u0023\u003DzS68_TbFJ1FlN;

    void IAsyncStateMachine.MoveNext()
    {
      int z4fzyEz1SsHya = this.\u0023\u003Dz4fzyEZ1SsHYa;
      ChartPanel zRrvwDu67s9Rm = this.\u0023\u003DzRRvwDu67s9Rm;
      try
      {
        TaskAwaiter awaiter;
        int num;
        if (z4fzyEz1SsHya != 0)
        {
          awaiter = zRrvwDu67s9Rm.ShareAsync(this.\u0023\u003Dz5kxxfgM\u003D, this.\u0023\u003DznVxnqZE\u003D, new Func<Stream>(((ChartHelper) zRrvwDu67s9Rm.\u0023\u003DzO72kpz0\u003D).SaveToImage), this.\u0023\u003Dzf32vx1Q\u003D).GetAwaiter();
          if (!awaiter.IsCompleted)
          {
            this.\u0023\u003Dz4fzyEZ1SsHYa = num = 0;
            this.\u0023\u003DzS68_TbFJ1FlN = awaiter;
            this.\u0023\u003DzvW8JtmzNnvTa.AwaitUnsafeOnCompleted<TaskAwaiter, ChartPanel.\u0023\u003DzZgO8RIPoYDQT9H_pgQ\u003D\u003D>(ref awaiter, ref this);
            return;
          }
        }
        else
        {
          awaiter = this.\u0023\u003DzS68_TbFJ1FlN;
          this.\u0023\u003DzS68_TbFJ1FlN = new TaskAwaiter();
          this.\u0023\u003Dz4fzyEZ1SsHYa = num = -1;
        }
        awaiter.GetResult();
      }
      catch (Exception ex)
      {
        this.\u0023\u003Dz4fzyEZ1SsHYa = -2;
        this.\u0023\u003DzvW8JtmzNnvTa.SetException(ex);
        return;
      }
      this.\u0023\u003Dz4fzyEZ1SsHYa = -2;
      this.\u0023\u003DzvW8JtmzNnvTa.SetResult();
    }

    [DebuggerHidden]
    void IAsyncStateMachine.SetStateMachine(
    #nullable enable
    IAsyncStateMachine _param1) => this.\u0023\u003DzvW8JtmzNnvTa.SetStateMachine(_param1);
  }

  private sealed class \u0023\u003Dzfi7\u0024520U3d1pGN5XKVeQxF4\u003D
  {
    public 
    #nullable disable
    ChartPanel \u0023\u003DzRRvwDu67s9Rm;
    public Func<Order, bool> \u0023\u003DzaPd0W_M\u003D;

    internal void \u0023\u003DzjckDB_EnwpKPCaWFi4pW638\u003D()
    {
      this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzO72kpz0\u003D.CancelOrders(this.\u0023\u003DzaPd0W_M\u003D);
    }
  }
}
