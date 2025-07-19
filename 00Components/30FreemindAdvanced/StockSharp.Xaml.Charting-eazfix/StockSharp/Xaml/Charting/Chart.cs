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
using SciChart.Data.Model;
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
using System.Xml.Linq;

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
	private sealed class SomeInternalSealedClass98364
	{
		public string[ ] _someStringArray03843;
		public Chart _variableSome3535;

		public void OnAreaYAxisesRemoving()
		{
			CollectionHelper.ForEach<string>( ( IEnumerable<string> ) this._someStringArray03843, new Action<string>( this._variableSome3535.SomeInternalFunction34382084 ) );
		}
	}

	private sealed class SomeInternalSealedClass897634 : Disposable
	{

		private readonly Chart _chart;

		private readonly IChartIndicatorElement _baseViewModel;

		private readonly IIndicator _indicator934;

		public SomeInternalSealedClass897634(
		  Chart _param1,
		  IChartIndicatorElement _param2,
		  IIndicator _param3 )
		{
			this._chart = _param1 ?? throw new ArgumentNullException( "parent" );
			this._baseViewModel = _param2 ?? throw new ArgumentNullException( "element" );
			this._indicator934 = _param3 ?? throw new ArgumentNullException( "indicator" );
			this.Indicator.Reseted += new Action( this.OnResetCallback );
		}

		public IIndicator Indicator => this._indicator934;

		protected virtual void DisposeManaged()
		{
			base.DisposeManaged();
			this.Indicator.Reseted -= new Action( this.OnResetCallback );
		}

		private void OnResetCallback()
		{
			if ( this._chart.DisableIndicatorReset )
				return;
			this._chart.\u0023\u003Dzs2PvqlQSy\u002401UuUfTA\u003D\u003D(this._baseViewModel, this.Indicator);
		}
	}


	private static int staticChartCount;

	private readonly int _instanceCount = ++Chart.staticChartCount;

	private readonly ChartBuilder _chartBuilder = new ChartBuilder();

	private Subscription _subscription;

	private ChartCandleDrawStyles _chartCandleDrawStyles;

	private MarketDataMessage _defaultCandlesSettings = new MarketDataMessage()
	{
		DataType2 = Extensions.TimeFrame( TimeSpan.FromMinutes( 5.0 ) )
	};

	private readonly ChartViewModel _chartSurfaceVM;

	private readonly SynchronizedDictionary<IChartIndicatorElement, Chart.SomeInternalSealedClass897634> _indicatorElementMap = new SynchronizedDictionary<IChartIndicatorElement, Chart.SomeInternalSealedClass897634>();
  
	private readonly SynchronizedDictionary<IChartElement, Subscription> _subscrptionMap = new SynchronizedDictionary<IChartElement, Subscription>();
  
	private ChartAnnotationTypes _annotationType;

	private bool _isAutoScroll = true;

	private bool _crossHairAxisLabels = true;

	private bool _crossHairTooltip;

	private bool _crossHair = true;

	private bool _orderCreationMode;

	private bool _isAutoRange;

	private bool _autoRangeByAnnotations;

	private bool _showPerfStats;

	private readonly Dictionary<(IChartArea, IndicatorMeasures), string> _area2IndicatorMeasuresMap = new Dictionary<(IChartArea, IndicatorMeasures), string>();
  
	private TimeSpan _autoRangeIntervalNoGroup = TimeSpan.FromMilliseconds(200.0);

	private readonly List<IChartArea> \u0023\u003Dza1mnh6ythHbd = new List<IChartArea>();
  
	private ISecurityProvider _securityProvider;;
  
	private bool _disableIndicatorReset;;
  
	private TimeZoneInfo _timeZone = TimeZoneInfo.Local;

	private bool _showNonFormedIndicators;
  
	private Security \u0023\u003DzCeIks\u0024kpJtyCA0n_Hg\u003D\u003D;
  
	private Subscription \u0023\u003DzAFpdFZRw72NT1DPyxQ\u003D\u003D;
  
	private Action<IChartArea> \u0023\u003DzcHtgn6mNhxMM;
  
	private Action \u0023\u003DzKGKj0Lc\u003D;
  
	private PropertyChangedEventHandler PropertyChangedEvent;

	public SciChartGroup \u0023\u003DzxYLKFqWiCEs\u0024;
  
	private bool _someInternalBoolean;

  public Chart()
	{
		Chart.SomeInternalSealedClass98364 ucNpCcsOdkLfs7Ks = new Chart.SomeInternalSealedClass98364();
		ucNpCcsOdkLfs7Ks._variableSome3535 = this;
		this.InitializeComponent();
		this.DataContext = ( object ) ( this._chartSurfaceVM = new ChartViewModel() );
		this.ViewModel.\u0023\u003DzgNd4ReliYq4x( new Action<Order>( this.\u0023\u003DzrMNjBJFuBLP3 ) );
		this.ViewModel.\u0023\u003DzqMcw8k8QHzu3( new Action<ChartArea>( this.\u0023\u003DzYhCvVp5ZsuEOxZPGgrZ6vLQ\u003D) );
		this.AreaAdding += new Action( this.OnAreaAdding );
		this.AddCandles += new Action<ChartArea>( this.OnAddCandles );
		this.RebuildCandles += new Action<IChartElement, Subscription>( this.OnRebuildCandles );
		this.AddIndicator += new Action<ChartArea>( this.OnAddIndicator );
		this.AddOrders += new Action<ChartArea>( this.OnAddOrders );
		this.AddTrades += new Action<ChartArea>( this.OnAddTrades );
		this.RemoveElement += new Action<IChartElement>( this.OnRemoveElement );
		ucNpCcsOdkLfs7Ks._someStringArray03843 = new string[ 7 ]
		{
			  nameof (IsInteracted),
			  nameof (AllowAddArea),
			  nameof (AllowAddAxis),
			  nameof (AllowAddCandles),
			  nameof (AllowAddIndicators),
			  nameof (AllowAddOrders),
			  nameof (AllowAddOwnTrades)
		};
		ChartViewModel.\u0023\u003DztwqF4KBjQLI4w4fkq\u0024UNEzaV82mj( new Action( ucNpCcsOdkLfs7Ks.OnAreaYAxisesRemoving ) );
		if ( IChartExtensions.TryIndicatorProvider == null )
		{
			IndicatorProvider indicatorProvider = new IndicatorProvider();
			indicatorProvider.Init();
			ConfigManager.RegisterService<IIndicatorProvider>( ( IIndicatorProvider ) indicatorProvider );
		}
		if ( IChartExtensions.TryIndicatorPainterProvider != null )
			return;
		ChartIndicatorPainterProvider indicatorPainterProvider = new ChartIndicatorPainterProvider();
		( ( IChartIndicatorPainterProvider ) indicatorPainterProvider ).Init();
		ConfigManager.RegisterService<IChartIndicatorPainterProvider>( ( IChartIndicatorPainterProvider ) indicatorPainterProvider );
	}

	public int GetInstanceCount() => this._instanceCount;


	public ChartCandleDrawStyles CandleDrawStyles
	{
		get
		{
			return this._chartCandleDrawStyles;
		}

		set
		{
			this._chartCandleDrawStyles = value;
		}
	}


	public MarketDataMessage DefaultCandlesSettings
	{
		get => this._defaultCandlesSettings;
		set
		{
			this._defaultCandlesSettings = value ?? throw new ArgumentNullException( nameof( value ) );
		}
	}

	public ChartViewModel ViewModel
	{
    return this._chartSurfaceVM;
	}

	public IEnumerable<IChartArea> Areas => ( IEnumerable<IChartArea> ) this.\u0023\u003Dza1mnh6ythHbd;

  public bool IsAutoScroll
	{
		get => this._isAutoScroll;
		set
		{
			this._isAutoScroll = value;
			NotifyPropertyChangedExHelper.Notify<Chart>( this, nameof( IsAutoScroll ) );
		}
	}

	public bool IsAutoRange
	{
		get => this._isAutoRange;
		set
		{
			this._isAutoRange = value;
			foreach ( IChartArea area in this.Areas )
				area.SetAutoRange( value );
			NotifyPropertyChangedExHelper.Notify<Chart>( this, nameof( IsAutoRange ) );
		}
	}

	public TimeSpan AutoRangeInterval
	{
		get => this._autoRangeIntervalNoGroup;
		set
		{
			this._autoRangeIntervalNoGroup = !( value <= TimeSpan.Zero ) ? value : throw new ArgumentOutOfRangeException( nameof( value ), ( object ) value, LocalizedStrings.InvalidValue );
			NotifyPropertyChangedExHelper.Notify<Chart>( this, nameof( AutoRangeInterval ) );
		}
	}

	public ISecurityProvider SecurityProvider
	{
		get => this._securityProvider;
		set => this._securityProvider = value;
	}

	public bool DisableIndicatorReset
	{
		get => this._disableIndicatorReset;
		set => this._disableIndicatorReset = value;
	}

	public void AddArea( IChartArea area )
	{
		( ( DispatcherObject ) this ).GuiSync( new Action( new Chart.SomeInternalSealedClass08343()

	{
      _chartArea_093 = area,
	  _variableSome3535 = this
	}.\u0023\u003DzTUl6zvo_PmQZ4TJ2ew\u003D\u003D) );
	}

	public void RemoveArea( IChartArea area )
	{
		( ( DispatcherObject ) this ).GuiSync( new Action( new Chart.\u0023\u003DzwNBM8yU0t_ER1Neix6Hn12Q\u003D()


	{
			_variableSome3535 = this,
      _chartArea_093 = area
		}.\u0023\u003DzJiDIwzXuOcws4Nsgtg\u003D\u003D));
	}

	private void \u0023\u003DzqfFsxob3ngDX( object _param1, PropertyChangedEventArgs _param2 )
	{
		ChartArea chartArea = (ChartArea) _param1;
		if ( !( _param2.PropertyName == "Height" ) )
			return;
		chartArea.ViewModel.Height = chartArea.Height;
	}

	public event Action<IChartArea> AreaAdded;

	public event Action<IChartArea> AreaRemoved;

	public void AddElement( IChartArea area, IChartElement element )
	{
		if ( area == null )
		{
			throw new ArgumentNullException( nameof( area ) );
		}

		if ( element == null )
		{
			throw new ArgumentNullException( nameof( element ) );
		}

		Chart.SomeInternalSealedClass3578512 gncgo1okL0HgWiq5JrU = new Chart.SomeInternalSealedClass3578512();
		gncgo1okL0HgWiq5JrU.\u0023\u003Dz_i6sZDg\u003D = element;
		gncgo1okL0HgWiq5JrU._variableSome3535 = this;
		gncgo1okL0HgWiq5JrU._chartArea_093 = area;
		
		( ( DispatcherObject ) this ).GuiSync( new Action( gncgo1okL0HgWiq5JrU.SomeMethod03843) );
	}

	private sealed class SomeInternalSealedClass3578512
	{
		public IChartElement \u0023\u003Dz_i6sZDg\u003D;
    public Chart _variableSome3535;
		public IChartArea _chartArea_093;

    public void SomeMethod03843()
    {
      Chart.SomeInternalSealedClass897634 zZq9Hpf12oRwg;
      if (this.\u0023\u003Dz_i6sZDg\u003D is IChartIndicatorElement zI6sZdg && zI6sZdg.AutoAssignYAxis && this._variableSome3535._indicatorElementMap.TryGetValue(zI6sZdg, ref zZq9Hpf12oRwg) && zZq9Hpf12oRwg.Indicator.Measure != IndicatorMeasures.Price)

	  {
			(IChartArea, IndicatorMeasures) key = (this._chartArea_093, zZq9Hpf12oRwg.Indicator.Measure);
			string str;
			if ( !this._variableSome3535._area2IndicatorMeasuresMap.TryGetValue( key, out str ) )
			{
				str = $"{"Y"}({Guid.NewGuid()})";
				// ISSUE: explicit non-virtual call
				IChartAxis axis = __nonvirtual (this._variableSome3535.CreateAxis());
				axis.Id = str;
				axis.AxisType = ChartAxisType.Numeric;
				( ( ICollection<IChartAxis> ) this._chartArea_093.YAxises).Add( axis );
				this._variableSome3535._area2IndicatorMeasuresMap.Add( key, axis.Id );
			}
			this.\u0023\u003Dz_i6sZDg\u003D.YAxisId = str;
        }

	  ((ICollection<IChartElement>) this._chartArea_093.Elements).Add(this.\u0023\u003Dz_i6sZDg\u003D);
		}
	}

	public void AddElement( IChartArea area, IChartCandleElement element, Subscription subscription )
	{
		if ( area == null )
			throw new ArgumentNullException( nameof( area ) );
		if ( element == null )
			throw new ArgumentNullException( nameof( element ) );
		if ( subscription == null )
			throw new ArgumentNullException( nameof( subscription ) );
		this._subscrptionMap.Add( ( IChartElement ) element, subscription );
		this.AddElement( area, ( IChartElement ) element );
	}

	public void AddElement(
	  IChartArea area,
	  IChartIndicatorElement element,
	  Subscription subscription,
	  IIndicator indicator )
	{
		if ( area == null )
			throw new ArgumentNullException( nameof( area ) );
		if ( element == null )
			throw new ArgumentNullException( nameof( element ) );
		if ( indicator == null )
			throw new ArgumentNullException( nameof( indicator ) );
		if ( subscription != null )
			this._subscrptionMap.Add( ( IChartElement ) element, subscription );
		this._indicatorElementMap.Add( element, new Chart.SomeInternalSealedClass897634( this, element, indicator ) );
		( ( ChartIndicatorElement ) element ).\u0023\u003Dz2Afk71t1OoFdU8tQ4Q\u003D\u003D(this.IndicatorTypes, indicator);
		this.AddElement( area, ( IChartElement ) element );
	}

	public void AddElement( IChartArea area, IChartOrderElement element, Subscription subscription )
	{
		if ( area == null )
			throw new ArgumentNullException( nameof( area ) );
		if ( element == null )
			throw new ArgumentNullException( nameof( element ) );
		if ( subscription == null )
			throw new ArgumentNullException( nameof( subscription ) );
		this._subscrptionMap.Add( ( IChartElement ) element, subscription );
		this.AddElement( area, ( IChartElement ) element );
	}

	public void AddElement( IChartArea area, IChartTradeElement element, Subscription subscription )
	{
		if ( area == null )
			throw new ArgumentNullException( nameof( area ) );
		if ( element == null )
			throw new ArgumentNullException( nameof( element ) );
		if ( subscription == null )
			throw new ArgumentNullException( nameof( subscription ) );
		this._subscrptionMap.Add( ( IChartElement ) element, subscription );
		this.AddElement( area, ( IChartElement ) element );
	}

	void IChart.RemoveElement( IChartArea area, IChartElement element )
	{
		Chart.\u0023\u003Dzm_oWpvJyDZiF55m7JqXuwJc\u003D jyDziF55m7JqXuwJc = new Chart.\u0023\u003Dzm_oWpvJyDZiF55m7JqXuwJc\u003D();
		jyDziF55m7JqXuwJc._chartArea_093 = area;
		jyDziF55m7JqXuwJc.\u0023\u003Dz_i6sZDg\u003D = element;
		if ( jyDziF55m7JqXuwJc._chartArea_093 == null)
      throw new ArgumentNullException( nameof( area ) );
		if ( jyDziF55m7JqXuwJc.\u0023\u003Dz_i6sZDg\u003D == null)
      throw new ArgumentNullException( nameof( element ) );
		Chart.SomeInternalSealedClass897634 zZq9Hpf12oRwg;
		if ( jyDziF55m7JqXuwJc.\u0023\u003Dz_i6sZDg\u003D is IChartIndicatorElement zI6sZdg && this._indicatorElementMap.TryGetValue( zI6sZdg, ref zZq9Hpf12oRwg ))
    {
			zZq9Hpf12oRwg.Dispose();
			this._indicatorElementMap.Remove( zI6sZdg );
		}
	  ( ( DispatcherObject ) this ).GuiSync<bool>( new Func<bool>( jyDziF55m7JqXuwJc.\u0023\u003DzRy89s7w8wbPZi21A_M2JlLqW05mnmhs8PApgiRA\u003D) );
		this._subscrptionMap.Remove( jyDziF55m7JqXuwJc.\u0023\u003Dz_i6sZDg\u003D);
	}

	public IIndicator GetIndicatorElement( IChartIndicatorElement element )
	{
		return CollectionHelper.TryGetValue<IChartIndicatorElement, Chart.SomeInternalSealedClass897634>( ( IDictionary<IChartIndicatorElement, Chart.SomeInternalSealedClass897634> ) this._indicatorElementMap, element )?.Indicator;
	}

	public Subscription TryGetSubscription( IChartElement element )
	{
		return CollectionHelper.TryGetValue<IChartElement, Subscription>( ( IDictionary<IChartElement, Subscription> ) this._subscrptionMap, element);
  }

  private (IChartCandleElement, Subscription) \u0023\u003DzmqXWWh6oQVIEJrU2Pw\u003D\u003D()
  {
    foreach (IChartElement chartElement in this.Areas.SelectMany<IChartArea, IChartElement>(Chart.SomeClass34343383.\u0023\u003Dzv\u002455fO77Mn0JronfvA\u003D\u003D ?? (Chart.SomeClass34343383.\u0023\u003Dzv\u002455fO77Mn0JronfvA\u003D\u003D = new Func<IChartArea, IEnumerable<IChartElement>>(Chart.SomeClass34343383.SomeMethond0343.\u0023\u003DzPJeLPTCgYwdmBIJASFbIA_hFSy_8))))
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
    SynchronizedDictionary<IChartElement, Subscription> zvWhSaOs = this._subscrptionMap;
    IChartElement chartElement = element;
    zvWhSaOs[chartElement] = subscription ?? throw new ArgumentNullException(nameof (subscription));
    ((IChartComponent) element).ResetUI();
  }

  public void CancelOrders(Func<Order, bool> predicate = null)
  {
    ((DispatcherObject) this).GuiSync(new Action(new Chart.\u0023\u003DzFDlMmaXY4v\u0024C2LpqBGOoouE\u003D()
    {
      _variableSome3535 = this,
      m_public_Func_Order_bool_ = predicate
    }.\u0023\u003DzjckDB_EnwpKPCaWFi4pW638\u003D));
  }

  public bool AutoRangeByAnnotations
  {
    get => this._autoRangeByAnnotations;
    set
    {
      if (this._autoRangeByAnnotations == value)
        return;
      this._autoRangeByAnnotations = value;
      NotifyPropertyChangedExHelper.Notify<Chart>(this, nameof (AutoRangeByAnnotations));
    }
  }

  public int MinimumRange
  {
    get => this.ViewModel.MinimumRange;
    set => this.ViewModel.MinimumRange = value;
  }

  public string ChartTheme
  {
    get => this.ViewModel.SelectedTheme;
    set => this.ViewModel.SelectedTheme = value;
  }

  public bool ShowLegend
  {
    get => this.ViewModel.ShowLegend;
    set => this.ViewModel.ShowLegend = value;
  }

  public bool ShowOverview
  {
    get => this.ViewModel.ShowOverview;
    set => this.ViewModel.ShowOverview = value;
  }

  public bool ShowPerfStats
  {
    get => this._showPerfStats;
    set
    {
      if (this._showPerfStats == value)
        return;
      this._showPerfStats = value;
      NotifyPropertyChangedExHelper.Notify<Chart>(this, nameof (ShowPerfStats));
    }
  }

  public bool IsInteracted
  {
    get => this.ViewModel.IsInteracted;
    set => this.ViewModel.IsInteracted = value;
  }

  public bool AllowAddArea
  {
    get => this.ViewModel.AllowAddArea;
    set => this.ViewModel.AllowAddArea = value;
  }

  public bool AllowAddAxis
  {
    get => this.ViewModel.AllowAddAxis;
    set => this.ViewModel.AllowAddAxis = value;
  }

  public bool AllowAddCandles
  {
    get => this.ViewModel.AllowAddCandles;
    set => this.ViewModel.AllowAddCandles = value;
  }

  public bool AllowAddIndicators
  {
    get => this.ViewModel.AllowAddIndicators;
    set => this.ViewModel.AllowAddIndicators = value;
  }

  public bool AllowAddOrders
  {
    get => this.ViewModel.AllowAddOrders;
    set => this.ViewModel.AllowAddOrders = value;
  }

  public bool AllowAddOwnTrades
  {
    get => this.ViewModel.AllowAddOwnTrades;
    set => this.ViewModel.AllowAddOwnTrades = value;
  }

  public bool CrossHair
  {
    get => this._crossHair;
    set
    {
      if (this._crossHair == value)
        return;
      this._crossHair = value;
      NotifyPropertyChangedExHelper.Notify<Chart>(this, nameof (CrossHair));
    }
  }

  public bool CrossHairTooltip
  {
    get => this._crossHairTooltip;
    set
    {
      if (this._crossHairTooltip == value)
        return;
      this._crossHairTooltip = value;
      NotifyPropertyChangedExHelper.Notify<Chart>(this, nameof (CrossHairTooltip));
    }
  }

  public bool CrossHairAxisLabels
  {
    get => this._crossHairAxisLabels;
    set
    {
      if (this._crossHairAxisLabels == value)
        return;
      this._crossHairAxisLabels = value;
      NotifyPropertyChangedExHelper.Notify<Chart>(this, nameof (CrossHairAxisLabels));
    }
  }

  public ChartAnnotationTypes AnnotationType
  {
    get => this._annotationType;
    set
    {
      if (this._annotationType == value)
        return;
      this._annotationType = value;
      NotifyPropertyChangedExHelper.Notify<Chart>(this, nameof (AnnotationType));
    }
  }

  public bool OrderCreationMode
  {
    get => this._orderCreationMode;
    set
    {
      if (this._orderCreationMode == value)
        return;
      this._orderCreationMode = value;
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
    get => this._showNonFormedIndicators;
    set => this._showNonFormedIndicators = value;
  }

  public IList<IndicatorType> IndicatorTypes
  {
    get => (IList<IndicatorType>) this.ViewModel.IndicatorTypes;
  }

  public Security \u0023\u003Dz3OPaBTitYMD\u0024()
  {
    return this.\u0023\u003DzCeIks\u0024kpJtyCA0n_Hg\u003D\u003D;
  }

  private void \u0023\u003DzJaQe\u0024mdWmfnr(Security _param1)
  {
    this.\u0023\u003DzCeIks\u0024kpJtyCA0n_Hg\u003D\u003D = _param1;
  }

  public Subscription \u0023\u003DzgDVjqFRN8sR7()
  {
    return this.\u0023\u003DzAFpdFZRw72NT1DPyxQ\u003D\u003D;
  }

  private void \u0023\u003DzoVkyaxlnrQ61(Subscription _param1)
  {
    this.\u0023\u003DzAFpdFZRw72NT1DPyxQ\u003D\u003D = _param1;
  }

  public IEnumerable<Subscription> Subscriptions
  {
    get => this._subscrptionMap.Values.Distinct<Subscription>();
  }

        /// <summary>The chart area creation event.</summary>
  public event Action AreaAdding
  {
    add
            {
                ViewModel.AreaAddingEvent += value;
            }
            remove
            {
                ViewModel.AreaAddingEvent -= value;
            }
  }

        /// <summary>The chart element creation event.</summary>
        public event Action<ChartArea> AddCandles
        {
            add
            {
                ViewModel.AddCandlesEvent += value;
            }
            remove
            {
                ViewModel.AddCandlesEvent -= value;
            }
        }

  public event Action<ChartArea> AddIndicator
        {
            add
            {
                ViewModel.AddIndicatorEvent += value;
            }
            remove
            {
                ViewModel.AddIndicatorEvent -= value;
            }
        }

  public event Action<ChartArea> AddOrders
{
    add
    {
        ViewModel.AddOrdersEvent += value;
    }
    remove
    {
        ViewModel.AddOrdersEvent -= value;
    }
}

  public event Action<ChartArea> AddTrades
  {
    add => this.ViewModel.AddTradesEvent += value;
    remove
    {
      this.ViewModel.AddTradesEvent -= value;
    }
  }

        /// <summary>The chart element removal event.</summary>
  public event Action<IChartElement> RemoveElement
  {
    add => this.ViewModel.RemoveElementEvent+= (value);
    remove => this.ViewModel.RemoveElementEvent -= (value);
  }

        /// <summary>Rebuild candles event.</summary>
  public event Action<IChartElement, Subscription> RebuildCandles
  {
    add => this.ViewModel.RebuildCandlesEvent += (value);
    remove => this.ViewModel.RebuildCandlesEvent -=(value);
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

  public void \u0023\u003DzTWNYl4Ujpxot(Action<IChartArea> _param1)
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

  public void \u0023\u003DzLw8k9fjIsyhB(Action<IChartArea> _param1)
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

  public void \u0023\u003Dzz6Byf1ItRSMq(Action _param1)
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

  public void \u0023\u003DzhaPIHSD4gMQ9(Action _param1)
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
    obj1._variableSome3535 = this;
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
    return ((DispatcherObject) this).GuiSync<IChartArea>(new Func<IChartArea>(this._chartBuilder.CreateArea));
  }

  public IChartAxis CreateAxis()
  {
    return ((DispatcherObject) this).GuiSync<IChartAxis>(new Func<IChartAxis>(this._chartBuilder.CreateAxis));
  }

  public IChartCandleElement CreateCandleElement()
  {
    return ((DispatcherObject) this).GuiSync<IChartCandleElement>(new Func<IChartCandleElement>(this._chartBuilder.CreateCandleElement));
  }

  public IChartIndicatorElement CreateIndicatorElement()
  {
    return ((DispatcherObject) this).GuiSync<IChartIndicatorElement>(new Func<IChartIndicatorElement>(this._chartBuilder.CreateIndicatorElement));
  }

  public IChartActiveOrdersElement CreateActiveOrdersElement()
  {
    return ((DispatcherObject) this).GuiSync<IChartActiveOrdersElement>(new Func<IChartActiveOrdersElement>(this._chartBuilder.CreateActiveOrdersElement));
  }

  public IChartAnnotationElement CreateAnnotation()
  {
    return ((DispatcherObject) this).GuiSync<IChartAnnotationElement>(new Func<IChartAnnotationElement>(this._chartBuilder.CreateAnnotation));
  }

  public IChartBandElement CreateBandElement()
  {
    return ((DispatcherObject) this).GuiSync<IChartBandElement>(new Func<IChartBandElement>(this._chartBuilder.CreateBandElement));
  }

  public IChartLineElement CreateLineElement()
  {
    return ((DispatcherObject) this).GuiSync<IChartLineElement>(new Func<IChartLineElement>(this._chartBuilder.CreateLineElement));
  }

  public IChartLineElement CreateBubbleElement()
  {
    return ((DispatcherObject) this).GuiSync<IChartLineElement>(new Func<IChartLineElement>(this._chartBuilder.CreateBubbleElement));
  }

  public IChartOrderElement CreateOrderElement()
  {
    return ((DispatcherObject) this).GuiSync<IChartOrderElement>(new Func<IChartOrderElement>(this._chartBuilder.CreateOrderElement));
  }

  public IChartTradeElement CreateTradeElement()
  {
    return ((DispatcherObject) this).GuiSync<IChartTradeElement>(new Func<IChartTradeElement>(this._chartBuilder.CreateTradeElement));
  }

  public void Draw(IChartDrawData data)
  {
    ChartDrawData chartDrawData = data != null ? (ChartDrawData) data : throw new ArgumentNullException(nameof (data));
    foreach (ChartArea chartArea in this.\u0023\u003Dza1mnh6ythHbd)
      chartArea.ViewModel.Draw(chartDrawData);
  }

  public void InvokeCreateOrderEvent(ChartArea _param1, Order _param2)
  {
    Action<ChartArea, Order> zlaBqx5E = this.\u0023\u003DzlaBQx5E\u003D;
    if (zlaBqx5E == null)
      return;
    zlaBqx5E(_param1, _param2);
  }

  public void \u0023\u003DzoSyIfjNKL9Ta(Order _param1, Decimal _param2)
  {
    Action<Order, Decimal> zJiM5nvc = this.\u0023\u003DzJIM5nvc\u003D;
    if (zJiM5nvc == null)
      return;
    zJiM5nvc(_param1, _param2);
  }

  public void \u0023\u003DzrMNjBJFuBLP3(Order _param1)
  {
    Action<Order> zmMdfCucSnZwz = this.CancelActiveOrderEvent ;
    if (zmMdfCucSnZwz == null)
      return;
    zmMdfCucSnZwz(_param1);
  }

  public void InvokeAnnotationCreatedEvent(ChartAnnotation _param1)
  {
    Action<IChartAnnotationElement> z6KsbJRt22Hb = this.\u0023\u003Dz6KSbJ_RT22HB;
    if (z6KsbJRt22Hb == null)
      return;
    z6KsbJRt22Hb((IChartAnnotationElement) _param1);
  }

  public void InvokeAnnotationModifiedEvent(
    ChartAnnotation _param1,
    ChartDrawData.AnnotationData _param2)
  {
    Action<IChartAnnotationElement, ChartDrawData.AnnotationData> zygdSp72uKvhL = this.\u0023\u003DzygdSp72uKVhL;
    if (zygdSp72uKvhL == null)
      return;
    zygdSp72uKvhL((IChartAnnotationElement) _param1, _param2);
  }

  public void \u0023\u003DzXartur54T48t(ChartAnnotation _param1)
  {
    Action<IChartAnnotationElement> z53l3VmDrGxpJ = this.\u0023\u003Dz53l3VMDrGxpJ;
    if (z53l3VmDrGxpJ == null)
      return;
    z53l3VmDrGxpJ((IChartAnnotationElement) _param1);
  }

  public void InvokeAnnotationSelectedEvent(
    ChartAnnotation _param1,
    ChartDrawData.AnnotationData _param2)
  {
    Action<IChartAnnotationElement, ChartDrawData.AnnotationData> zxVqsLo94Ea68 = this.\u0023\u003DzxVqsLO94Ea68;
    if (zxVqsLo94Ea68 == null)
      return;
    zxVqsLo94Ea68((IChartAnnotationElement) _param1, _param2);
  }

  public TimeZoneInfo \u0023\u003DzJp_PZYEzsJcq()
  {
    return this.Areas.Select<IChartArea, IChartAxis>(Chart.SomeClass34343383.\u0023\u003DzUgb1mArEMRFvFA4YrQ\u003D\u003D ?? (Chart.SomeClass34343383.\u0023\u003DzUgb1mArEMRFvFA4YrQ\u003D\u003D = new Func<IChartArea, IChartAxis>(Chart.SomeClass34343383.SomeMethond0343.\u0023\u003DzqN6a1mvRk0j592xRhqiAxD4ES8KX))).LastOrDefault<IChartAxis>(Chart.SomeClass34343383.\u0023\u003DzHbIKsCHhb08DRHHYMg\u003D\u003D ?? (Chart.SomeClass34343383.\u0023\u003DzHbIKsCHhb08DRHHYMg\u003D\u003D = new Func<IChartAxis, bool>(Chart.SomeClass34343383.SomeMethond0343.\u0023\u003DzOCtOMr4FFxWKTTRmm0dMrehqCY9f)))?.TimeZone;
  }

  public void \u0023\u003DzBF\u0024LAMIgiEWk(TimeSpan _param1)
  {
    (IChartCandleElement chartCandleElement, Subscription subscription) = this.\u0023\u003DzmqXWWh6oQVIEJrU2Pw\u003D\u003D();
    if (chartCandleElement == null)
      return;
    object obj = subscription.DataType.Arg;
    if ((obj != null ? (obj.Equals((object) _param1) ? 1 : 0) : 0) != 0)
      return;
    this.OnRebuildCandles((IChartElement) chartCandleElement, new Subscription(Extensions.TimeFrame(_param1), (SecurityMessage) subscription.MarketData));
  }

  private void OnRebuildCandles(
    IChartElement _param1,
    Subscription _param2)
  {
    Chart.\u0023\u003DzOuPSV9MAGW\u0024JG5Cu0tHrBA0\u003D magwJg5Cu0tHrBa0 = new Chart.\u0023\u003DzOuPSV9MAGW\u0024JG5Cu0tHrBA0\u003D();
    magwJg5Cu0tHrBa0._variableSome3535 = this;
    magwJg5Cu0tHrBa0.\u0023\u003Dz_i6sZDg\u003D = _param1 as IChartCandleElement;
    if (magwJg5Cu0tHrBa0.\u0023\u003Dz_i6sZDg\u003D == null)
      return;
    IChartArea chartArea = magwJg5Cu0tHrBa0.\u0023\u003Dz_i6sZDg\u003D.ChartArea;
    magwJg5Cu0tHrBa0.\u0023\u003DzlRZ9MD8\u003D = this.TryGetSubscription((IChartElement) magwJg5Cu0tHrBa0.\u0023\u003Dz_i6sZDg\u003D);
    Dictionary<IChartIndicatorElement, Tuple<IIndicator, IChartArea>> dictionary = ((IEnumerable<KeyValuePair<IChartElement, Subscription>>) this._subscrptionMap).Where<KeyValuePair<IChartElement, Subscription>>(new Func<KeyValuePair<IChartElement, Subscription>, bool>(magwJg5Cu0tHrBa0.\u0023\u003Dz0h9UKK62LR3Vt49xiuMu8R9QgAUI)).Select<KeyValuePair<IChartElement, Subscription>, IChartIndicatorElement>(Chart.SomeClass34343383.\u0023\u003DzW6pqQQ9lqKPZfyTXDw\u003D\u003D ?? (Chart.SomeClass34343383.\u0023\u003DzW6pqQQ9lqKPZfyTXDw\u003D\u003D = new Func<KeyValuePair<IChartElement, Subscription>, IChartIndicatorElement>(Chart.SomeClass34343383.SomeMethond0343.\u0023\u003DzPMkLVMTIVacbdOnMgx5om9R8XfFp42JlLQ\u003D\u003D))).ToDictionary<IChartIndicatorElement, IChartIndicatorElement, Tuple<IIndicator, IChartArea>>(Chart.SomeClass34343383.\u0023\u003DzBCXPyDZYNZgQeoOs4Q\u003D\u003D ?? (Chart.SomeClass34343383.\u0023\u003DzBCXPyDZYNZgQeoOs4Q\u003D\u003D = new Func<IChartIndicatorElement, IChartIndicatorElement>(Chart.SomeClass34343383.SomeMethond0343.\u0023\u003DzuKv4rqmZgBRXOk9cL\u0024V\u0024mJZ_bYxaKSqnjQ\u003D\u003D)), new Func<IChartIndicatorElement, Tuple<IIndicator, IChartArea>>(magwJg5Cu0tHrBa0.\u0023\u003Dzw1Y8PN8Ihy08dhHsTYoW7TH7dWHX));
    this.OnRemoveElement((IChartElement) magwJg5Cu0tHrBa0.\u0023\u003Dz_i6sZDg\u003D);
    CollectionHelper.ForEach<IChartIndicatorElement>((IEnumerable<IChartIndicatorElement>) dictionary.Keys, new Action<IChartIndicatorElement>(this.OnRemoveElement));
    ((IChartComponent) magwJg5Cu0tHrBa0.\u0023\u003Dz_i6sZDg\u003D).ResetUI();
    this.AddElement(chartArea, magwJg5Cu0tHrBa0.\u0023\u003Dz_i6sZDg\u003D, _param2);
    foreach (KeyValuePair<IChartIndicatorElement, Tuple<IIndicator, IChartArea>> keyValuePair in dictionary)
      this.AddElement(keyValuePair.Value.Item2, keyValuePair.Key, _param2, keyValuePair.Value.Item1);
    this.\u0023\u003Dz1jTUhLCjcbWI();
  }

  private void OnRemoveElement(IChartElement _param1)
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
    this._subscrptionMap.Clear();
    CollectionHelper.ForEach<Chart.SomeInternalSealedClass897634>((IEnumerable<Chart.SomeInternalSealedClass897634>) this._indicatorElementMap.Values, Chart.SomeClass34343383.\u0023\u003Dzosc1BoJdGH8Feib98w\u003D\u003D ?? (Chart.SomeClass34343383.\u0023\u003Dzosc1BoJdGH8Feib98w\u003D\u003D = new Action<Chart.SomeInternalSealedClass897634>(Chart.SomeClass34343383.SomeMethond0343.\u0023\u003Dzd9aoFEQWycLNB5pmAWPP_1U\u003D)));
    this._indicatorElementMap.Clear();
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
    storage.SetValue<SettingsStorage[]>("Areas", this.\u0023\u003Dza1mnh6ythHbd.Select<IChartArea, SettingsStorage>(Chart.SomeClass34343383.\u0023\u003DzIUod_Wcpx0QRK1tJ1g\u003D\u003D ?? (Chart.SomeClass34343383.\u0023\u003DzIUod_Wcpx0QRK1tJ1g\u003D\u003D = new Func<IChartArea, SettingsStorage>(Chart.SomeClass34343383.SomeMethond0343.\u0023\u003Dzi3wYC00Rk5VQyB\u00247dTCZE2Q\u003D))).ToArray<SettingsStorage>());
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
      area.ViewModel.Height = storage.GetValue<double>("Height", double.NaN);
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
        zLdfE1FxkiHdr(element, _param2, this.GetIndicatorElement(element));
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
      uyozsiG5aIcl2VmAkuos._variableSome3535 = this;
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

  public static void \u0023\u003Dz370H8OFDsNyA(
    SciChartSurface _param0)
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
    Chart.\u0023\u003Dz370H8OFDsNyA((SciChartSurface) _param1);
  }

  private void \u0023\u003Dzs2PvqlQSy\u002401UuUfTA\u003D\u003D(
    IChartIndicatorElement _param1,
    IIndicator _param2)
  {
    ((IChartComponent) _param1).ResetUI();
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
    if (this._someInternalBoolean)
      return;
    this._someInternalBoolean = true;
    Application.LoadComponent((object) this, new Uri("/StockSharp.Xaml.Charting;V5.0.0;component/chart.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  public Delegate \u0023\u003DzciIj4U627yBM(Type _param1, string _param2)
  {
    return Delegate.CreateDelegate(_param1, (object) this, _param2);
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    if (connectionId == 1)
      this.\u0023\u003DzxYLKFqWiCEs\u0024 = (SciChartGroup) target;
    else
      this._someInternalBoolean = true;
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

  private void OnAreaAdding()
  {
    Chart.\u0023\u003DzCMPTGwQ4dXwYZLzYWDdBciE\u003D q4dXwYzLzYwDdBciE = new Chart.\u0023\u003DzCMPTGwQ4dXwYZLzYWDdBciE\u003D();
    ChartArea area = new ChartArea()
    {
      Title = $"{LocalizedStrings.Panel} {(this.\u0023\u003Dza1mnh6ythHbd.Count + 1).ToString()}"
    };
    q4dXwYzLzYwDdBciE.\u0023\u003DzvZK8J1raIDr8 = this.\u0023\u003DzJp_PZYEzsJcq();
    CollectionHelper.ForEach<IChartAxis>((IEnumerable<IChartAxis>) area.XAxises, new Action<IChartAxis>(q4dXwYzLzYwDdBciE.OnAreaYAxisesRemovingAt));
    this.AddArea((IChartArea) area);
  }

  private void OnAddCandles(ChartArea _param1)
  {
    if (this._subscription == null)
      this._subscription = new Subscription((ISubscriptionMessage) this.DefaultCandlesSettings, (SecurityMessage) null);
    CandleSettingsWindow wnd = new CandleSettingsWindow()
    {
      Subscription = ((Cloneable<Subscription>) this._subscription).Clone()
    };
    if (this.SecurityProvider != null)
      wnd.SecurityProvider = this.SecurityProvider;
    if (!wnd.ShowModal((DependencyObject) this))
      return;
    Subscription subscription = wnd.Subscription;
    this._subscription = subscription;
    ChartCandleElement element = new ChartCandleElement()
    {
      PriceStep = (Decimal?) ((SecurityMessage) subscription?.MarketData)?.PriceStep,
      DrawStyle = this.CandleDrawStyles()
    };
    this.AddElement((IChartArea) _param1, (IChartCandleElement) element, subscription);
  }

  private void OnAddIndicator(ChartArea _param1)
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

  private void OnAddOrders(ChartArea _param1)
  {
    Security security = this.\u0023\u003DqWFLBlYXsZ6MGzXnc3yqMfUBG1YSw2WmShVXKtnoKBjE\u003D(_param1);
    if (security == null)
      return;
    this.AddElement((IChartArea) _param1, (IChartOrderElement) new ChartOrderElement(), new Subscription(DataType.Transactions, security));
  }

  private void OnAddTrades(ChartArea _param1)
  {
    Security security = this.\u0023\u003DqWFLBlYXsZ6MGzXnc3yqMfUBG1YSw2WmShVXKtnoKBjE\u003D(_param1);
    if (security == null)
      return;
    this.AddElement((IChartArea) _param1, (IChartTradeElement) new ChartTradeElement(), new Subscription(DataType.Transactions, security));
  }

  private void SomeInternalFunction34382084(string _param1)
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
  private sealed class SomeClass34343383
  {
    public static readonly Chart.SomeClass34343383 SomeMethond0343 = new Chart.SomeClass34343383();
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
    Action<Chart.SomeInternalSealedClass897634> \u0023\u003Dzosc1BoJdGH8Feib98w\u003D\u003D;
    public static Func<IChartArea, SettingsStorage> \u0023\u003DzIUod_Wcpx0QRK1tJ1g\u003D\u003D;

    public 
    #nullable enable
    IEnumerable<
    #nullable disable
    IChartElement> \u0023\u003DzPJeLPTCgYwdmBIJASFbIA_hFSy_8(IChartArea _param1)
    {
      return (IEnumerable<IChartElement>) _param1.Elements;
    }

    public IChartAxis \u0023\u003DzqN6a1mvRk0j592xRhqiAxD4ES8KX(IChartArea _param1)
    {
      return ((IEnumerable<IChartAxis>) _param1.XAxises).FirstOrDefault<IChartAxis>(Chart.SomeClass34343383.\u0023\u003DzJWBwcSmmMYm95T_4EA\u003D\u003D ?? (Chart.SomeClass34343383.\u0023\u003DzJWBwcSmmMYm95T_4EA\u003D\u003D = new Func<IChartAxis, bool>(Chart.SomeClass34343383.SomeMethond0343.\u0023\u003DzVVB4LH1KsMwAYfJoLw4TOG6pJ6Nk)));
    }

    public bool \u0023\u003DzVVB4LH1KsMwAYfJoLw4TOG6pJ6Nk(IChartAxis _param1)
    {
      return _param1.TimeZone != null;
    }

    public bool \u0023\u003DzOCtOMr4FFxWKTTRmm0dMrehqCY9f(IChartAxis _param1) => _param1 != null;

    public IChartIndicatorElement \u0023\u003DzPMkLVMTIVacbdOnMgx5om9R8XfFp42JlLQ\u003D\u003D(
      KeyValuePair<IChartElement, Subscription> _param1)
    {
      return (IChartIndicatorElement) _param1.Key;
    }

    public 
    #nullable enable
    IChartIndicatorElement \u0023\u003DzuKv4rqmZgBRXOk9cL\u0024V\u0024mJZ_bYxaKSqnjQ\u003D\u003D(
      #nullable disable
      IChartIndicatorElement _param1)
    {
      return _param1;
    }

    public void \u0023\u003Dzd9aoFEQWycLNB5pmAWPP_1U\u003D(Chart.SomeInternalSealedClass897634 _param1)
    {
      _param1.Dispose();
    }

    public SettingsStorage \u0023\u003Dzi3wYC00Rk5VQyB\u00247dTCZE2Q\u003D(IChartArea _param1)
    {
      SettingsStorage settingsStorage = PersistableHelper.Save((IPersistable) _param1);
      settingsStorage.SetValue<double>("Height", ((ChartArea) _param1).ViewModel.Height);
      return settingsStorage;
    }
  }

  private sealed class \u0023\u003DzCMPTGwQ4dXwYZLzYWDdBciE\u003D
  {
    public TimeZoneInfo \u0023\u003DzvZK8J1raIDr8;

    public void OnAreaYAxisesRemovingAt(IChartAxis _param1)
    {
      _param1.TimeZone = this.\u0023\u003DzvZK8J1raIDr8;
    }
  }

  private sealed class \u0023\u003DzFDlMmaXY4v\u0024C2LpqBGOoouE\u003D
  {
    public Chart _variableSome3535;
    public Func<Order, bool> m_public_Func_Order_bool_;

    public void \u0023\u003DzjckDB_EnwpKPCaWFi4pW638\u003D()
    {
      this._variableSome3535.ViewModel.\u0023\u003Dz\u0024DK5seweHzSZIyjEhw\u003D\u003D(this.m_public_Func_Order_bool_);
    }
  }

  

  private sealed class SomeInternalSealedClass08343
  {
    public IChartArea _chartArea_093;
    public Chart _variableSome3535;
    public Action<IChartAxis> _Func_IAxis_bool_0835;

    public void \u0023\u003DzTUl6zvo_PmQZ4TJ2ew\u003D\u003D()
    {
      if (this._chartArea_093.Chart != null)
        throw new ArgumentException("area.Chart != null", "area");
      if (this._chartArea_093 == null || this._variableSome3535.\u0023\u003Dza1mnh6ythHbd.Contains(this._chartArea_093))
        throw new ArgumentException("area2");
      ChartAxisType? xaxisType = this._variableSome3535.\u0023\u003Dza1mnh6ythHbd.FirstOrDefault<IChartArea>()?.XAxisType;
      if (xaxisType.HasValue)
      {
        if (CollectionHelper.IsEmpty<IChartElement>((ICollection<IChartElement>) this._chartArea_093.Elements))
          this._chartArea_093.XAxisType = xaxisType.Value;
        else if (this._chartArea_093.XAxisType != xaxisType.Value)
          throw new InvalidOperationException(LocalizedStrings.InvalidAxisType);
      }
      CollectionHelper.ForEach<IChartAxis>((IEnumerable<IChartAxis>) this._chartArea_093.XAxises, this._Func_IAxis_bool_0835 ?? (this._Func_IAxis_bool_0835 = new Action<IChartAxis>(this.\u0023\u003Dz_CddBmgHU5hqO5sw1g\u003D\u003D)));
      this._chartArea_093.PropertyChanged += new PropertyChangedEventHandler(this._variableSome3535.\u0023\u003DzqfFsxob3ngDX);
      ((INotifyCollection<IChartElement>) this._chartArea_093.Elements).Added += new Action<IChartElement>(this._variableSome3535.\u0023\u003DzVpu_ST0\u003D);
      ((INotifyCollection<IChartElement>) this._chartArea_093.Elements).Removed += new Action<IChartElement>(this._variableSome3535.\u0023\u003DzW\u0024jq94I\u003D);
      this._variableSome3535.\u0023\u003Dza1mnh6ythHbd.Add(this._chartArea_093);
      this._chartArea_093.Chart = (IChart) this._variableSome3535;
      this._variableSome3535.ViewModel.ChartPaneViewModels.Add(((ChartArea) this._chartArea_093).ViewModel);
      CollectionHelper.ForEach<IChartElement>((IEnumerable<IChartElement>) this._chartArea_093.Elements, new Action<IChartElement>(this._variableSome3535.\u0023\u003DzVpu_ST0\u003D));
      Action<IChartArea> zk8cjLwfRrDki = this._variableSome3535.\u0023\u003Dzk8cjLWfRrDKI;
      if (zk8cjLwfRrDki == null)
        return;
      zk8cjLwfRrDki(this._chartArea_093);
    }

    public void \u0023\u003Dz_CddBmgHU5hqO5sw1g\u003D\u003D(IChartAxis _param1)
    {
      // ISSUE: explicit non-virtual call
      _param1.AutoRange = __nonvirtual (this._variableSome3535.IsAutoRange);
    }
  }

  private sealed class \u0023\u003DzOuPSV9MAGW\u0024JG5Cu0tHrBA0\u003D
  {
    public Subscription \u0023\u003DzlRZ9MD8\u003D;
    public IChartCandleElement \u0023\u003Dz_i6sZDg\u003D;
    public Chart _variableSome3535;

    public bool \u0023\u003Dz0h9UKK62LR3Vt49xiuMu8R9QgAUI(
      KeyValuePair<IChartElement, Subscription> _param1)
    {
      return _param1.Value == this.\u0023\u003DzlRZ9MD8\u003D && _param1.Key != this.\u0023\u003Dz_i6sZDg\u003D;
    }

    public Tuple<IIndicator, IChartArea> \u0023\u003Dzw1Y8PN8Ihy08dhHsTYoW7TH7dWHX(
      IChartIndicatorElement _param1)
    {
      return Tuple.Create<IIndicator, IChartArea>(this._variableSome3535.GetIndicatorElement(_param1), _param1.ChartArea);
    }
  }

  

  private sealed class \u0023\u003DzRhtUyozsiG5aICl2VmAKuos\u003D
  {
    public Subscription \u0023\u003DzgZ3Boxc\u003D;
    public Chart _variableSome3535;

    public bool \u0023\u003Dzdtmu9eDzQDfVd61b8w\u003D\u003D(IChartElement _param1)
    {
      // ISSUE: explicit non-virtual call
      return __nonvirtual (this._variableSome3535.TryGetSubscription(_param1)) == this.\u0023\u003DzgZ3Boxc\u003D;
    }
  }

  

  private sealed class \u0023\u003DzbPn0uB_1mAkEZqFF7kGyKzM\u003D
  {
    public Chart _variableSome3535;
    public IEnumerable<IChartElement> \u0023\u003DzDqgUu38\u003D;

    public void \u0023\u003DzD1ojw__0StW8WOcIEQ\u003D\u003D()
    {
      foreach (ChartArea chartArea in this._variableSome3535.\u0023\u003Dza1mnh6ythHbd)
        chartArea.ViewModel.Reset(this.\u0023\u003DzDqgUu38\u003D);
    }
  }

  private sealed class \u0023\u003DziYslZOQka25erb85NfEM3z4\u003D
  {
    public SciChartSurface \u0023\u003Dz6x1I8qQ\u003D;

    public void \u0023\u003Dqtx1KXraU1keT0uiySlEVOOB5PnDLulwyMJjyjX7rsVjruD1DZyrc16lnN0h2\u0024q6Q()
    {
      ((ScichartSurfaceMVVM) this.\u0023\u003Dz6x1I8qQ\u003D.DataContext).SetScichartSurface(this.\u0023\u003Dz6x1I8qQ\u003D);
    }

    public void \u0023\u003DzvrcTIvo4QYO6VIoIYgtMLK0\u003D(
      object _param1,
      DependencyPropertyChangedEventArgs _param2)
    {
      this.\u0023\u003Dqtx1KXraU1keT0uiySlEVOOB5PnDLulwyMJjyjX7rsVjruD1DZyrc16lnN0h2\u0024q6Q();
    }
  }

  private sealed class \u0023\u003Dzm_oWpvJyDZiF55m7JqXuwJc\u003D
  {
    public IChartArea _chartArea_093;
    public IChartElement \u0023\u003Dz_i6sZDg\u003D;

    public bool \u0023\u003DzRy89s7w8wbPZi21A_M2JlLqW05mnmhs8PApgiRA\u003D()
    {
      return ((ICollection<IChartElement>) this._chartArea_093.Elements).Remove(this.\u0023\u003Dz_i6sZDg\u003D);
    }
  }

  private sealed class \u0023\u003DzwNBM8yU0t_ER1Neix6Hn12Q\u003D
  {
    public Chart _variableSome3535;
    public IChartArea _chartArea_093;

    public void \u0023\u003DzJiDIwzXuOcws4Nsgtg\u003D\u003D()
    {
      if (!this._variableSome3535.\u0023\u003Dza1mnh6ythHbd.Remove(this._chartArea_093))
        return;
      this._chartArea_093.PropertyChanged -= new PropertyChangedEventHandler(this._variableSome3535.\u0023\u003DzqfFsxob3ngDX);
      ((INotifyCollection<IChartElement>) this._chartArea_093.Elements).Added -= new Action<IChartElement>(this._variableSome3535.\u0023\u003DzVpu_ST0\u003D);
      ((INotifyCollection<IChartElement>) this._chartArea_093.Elements).Removed -= new Action<IChartElement>(this._variableSome3535.\u0023\u003DzW\u0024jq94I\u003D);
      this._variableSome3535.ViewModel.ChartPaneViewModels.Remove(((ChartArea) this._chartArea_093).ViewModel);
      CollectionHelper.ForEach<IChartElement>((IEnumerable<IChartElement>) this._chartArea_093.Elements, new Action<IChartElement>(this._variableSome3535.\u0023\u003DzW\u0024jq94I\u003D));
      this._chartArea_093.Chart = (IChart) null;
      TypeHelper.DoDispose<IChartArea>(this._chartArea_093);
      Action<IChartArea> z0aBkRs4Mkj0a = this._variableSome3535.\u0023\u003Dz0aBkRs4Mkj0a;
      if (z0aBkRs4Mkj0a != null)
        z0aBkRs4Mkj0a(this._chartArea_093);
      if (!CollectionHelper.IsEmpty<IChartArea>((ICollection<IChartArea>) this._variableSome3535.\u0023\u003Dza1mnh6ythHbd))
        return;
      this._variableSome3535.ViewModel.InitRangeDepProperty();
    }
  }
}
