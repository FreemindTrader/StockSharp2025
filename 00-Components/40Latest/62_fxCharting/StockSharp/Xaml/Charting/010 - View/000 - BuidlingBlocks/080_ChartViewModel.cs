using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors.Helpers;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Xaml;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Messages;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;


/// <summary>
/// The view model for the chart control.
/// 
/// We can control the following DependencyProperty
/// 
///     1   - Selected theme
///     2   - Add (Area, Candles, Indicators, Orders, OwnTrades) commands
///         - AllowAddXXX - Allow users to add (Area, Candles, Indicators, Orders, OwnTrades) to the chart
///     3   - Show/Hide Overview, legends
///     4   - IsInteracted - Allow users to add orders, indicators, annotations to the chart
///     5   - AddXAxis, AddYAxis, RemoveAxis commands
///     
/// </summary>
public sealed class ChartViewModel : DependencyObject
{
    public static readonly DependencyProperty SelectedThemeProperty         = DependencyProperty.Register(nameof(SelectedTheme),         typeof(string), typeof(ChartViewModel));

    public static readonly DependencyProperty ShowOverviewProperty          = DependencyProperty.Register(nameof(ShowOverview),          typeof(bool), typeof(ChartViewModel), new PropertyMetadata((object)false));

    public static readonly DependencyProperty ShowLegendProperty            = DependencyProperty.Register(nameof(ShowLegend),            typeof(bool), typeof(ChartViewModel));

    public static readonly DependencyProperty IsInteractedProperty          = DependencyProperty.Register(nameof(IsInteracted),          typeof(bool), typeof(ChartViewModel), new PropertyMetadata((object)true, new PropertyChangedCallback(OnIsInteractedCallback)));

    public static readonly DependencyProperty ChartPaneViewModelProperty    = DependencyProperty.Register(nameof(ChartPaneViewModels),   typeof(ObservableCollection<ScichartSurfaceMVVM>), typeof(ChartViewModel));

    public static readonly DependencyProperty ShowHiddenAxesCommandProperty = DependencyProperty.Register(nameof(ShowHiddenAxesCommand), typeof(ICommand), typeof(ChartViewModel));

    public static readonly DependencyProperty AddAreaCommandProperty        = DependencyProperty.Register(nameof(AddAreaCommand),        typeof(ICommand), typeof(ChartViewModel));

    public static readonly DependencyProperty AddCandlesCommandProperty     = DependencyProperty.Register(nameof(AddCandlesCommand),     typeof(ICommand), typeof(ChartViewModel));

    public static readonly DependencyProperty AddIndicatorCommandProperty   = DependencyProperty.Register(nameof(AddIndicatorCommand),   typeof(ICommand), typeof(ChartViewModel));

    public static readonly DependencyProperty AddOrdersCommandProperty      = DependencyProperty.Register(nameof(AddOrdersCommand),      typeof(ICommand), typeof(ChartViewModel));

    public static readonly DependencyProperty AddTradesCommandProperty      = DependencyProperty.Register(nameof(AddTradesCommand),      typeof(ICommand), typeof(ChartViewModel));

    public static readonly DependencyProperty UngroupCommandProperty        = DependencyProperty.Register(nameof(UngroupCommand),        typeof(ICommand), typeof(ChartViewModel));

    public static readonly DependencyProperty AddXAxisCommandProperty       = DependencyProperty.Register(nameof(AddXAxisCommand),       typeof(ICommand), typeof(ChartViewModel));

    public static readonly DependencyProperty AddYAxisCommandProperty       = DependencyProperty.Register(nameof(AddYAxisCommand),       typeof(ICommand), typeof(ChartViewModel));

    public static readonly DependencyProperty RemoveAxisCommandProperty     = DependencyProperty.Register(nameof(RemoveAxisCommand),     typeof(ICommand), typeof(ChartViewModel));

    public static readonly DependencyProperty IndicatorTypesProperty        = DependencyProperty.Register(nameof(IndicatorTypes),        typeof(ObservableCollection<IndicatorType>), typeof(ChartViewModel));

    public static readonly DependencyProperty AllowAddAreaProperty          = DependencyProperty.Register(nameof(AllowAddArea),          typeof(bool), typeof(ChartViewModel), new PropertyMetadata((object)true, new PropertyChangedCallback(OnAllowAddXXXPropertyChanged), new CoerceValueCallback(OnAllowAddXPropertyCoreceValueChanged)));

    public static readonly DependencyProperty AllowAddAxisProperty          = DependencyProperty.Register(nameof(AllowAddAxis),          typeof(bool), typeof(ChartViewModel), new PropertyMetadata((object)true, new PropertyChangedCallback(OnAllowAddXXXPropertyChanged), new CoerceValueCallback(OnAllowAddXPropertyCoreceValueChanged)));

    public static readonly DependencyProperty AllowAddCandlesProperty       = DependencyProperty.Register(nameof(AllowAddCandles),       typeof(bool), typeof(ChartViewModel), new PropertyMetadata((object)true, new PropertyChangedCallback(OnAllowAddXXXPropertyChanged), new CoerceValueCallback(OnAllowAddXPropertyCoreceValueChanged)));

    public static readonly DependencyProperty AllowAddIndicatorsProperty    = DependencyProperty.Register(nameof(AllowAddIndicators),    typeof(bool), typeof(ChartViewModel), new PropertyMetadata((object)true, new PropertyChangedCallback(OnAllowAddXXXPropertyChanged), new CoerceValueCallback(OnAllowAddXPropertyCoreceValueChanged)));

    public static readonly DependencyProperty AllowAddOrdersProperty        = DependencyProperty.Register(nameof(AllowAddOrders),        typeof(bool), typeof(ChartViewModel), new PropertyMetadata((object)true, new PropertyChangedCallback(OnAllowAddXXXPropertyChanged), new CoerceValueCallback(OnAllowAddXPropertyCoreceValueChanged)));

    public static readonly DependencyProperty AllowAddOwnTradesProperty     = DependencyProperty.Register(nameof(AllowAddOwnTrades),     typeof(bool), typeof(ChartViewModel), new PropertyMetadata((object)true, new PropertyChangedCallback(OnAllowAddXXXPropertyChanged), new CoerceValueCallback(OnAllowAddXPropertyCoreceValueChanged)));

    public static readonly DependencyProperty MinimumRangeProperty          = DependencyProperty.Register(nameof(MinimumRange),          typeof(int), typeof(ChartViewModel), new PropertyMetadata( (d,e)=> ( ( ChartViewModel ) d )._minimumRange = ( int ) e.NewValue ));

    /// <summary>
    /// What theme is currently selected in the application. 
    /// </summary>
    public string SelectedTheme
    {
        get
        {
            return ( string ) GetValue( SelectedThemeProperty );
        }
        set
        {
            SetValue( SelectedThemeProperty, value );
        }
    }

    public bool ShowOverview
    {
        get
        {
            return ( bool ) GetValue( ShowOverviewProperty );
        }
        set
        {
            SetValue( ShowOverviewProperty, value );
        }
    }

    /// <summary>
    /// Legend is the box on the side of the chart that shows 
    ///     - What point series are selected on the chart
    ///     - what indicators are on the chart and their current values.
    /// </summary>
    public bool ShowLegend
    {
        get
        {
            return ( bool ) GetValue( ShowLegendProperty );
        }
        set
        {
            SetValue( ShowLegendProperty, value );
        }
    }

    /// <summary>
    /// Allow users to add orders, indicators, annotations to the chart
    /// </summary>
    public bool IsInteracted
    {
        get
        {
            return ( bool ) GetValue( IsInteractedProperty );
        }
        set
        {
            SetValue( IsInteractedProperty, value );
        }
    }

    /// <summary>
    /// A property to control if we can add more areas to the chart. Like if we can add more indicator panes to the chart.
    /// </summary>
    public bool AllowAddArea
    {
        get
        {
            return ( bool ) GetValue( AllowAddAreaProperty );
        }
        set
        {
            SetValue( AllowAddAreaProperty, value );
        }
    }

    /// <summary>
    /// Scichart allow adding multiple y-axis to the chart. This property controls if we can add more y-axis to the chart.
    /// </summary>
    public bool AllowAddAxis
    {
        get
        {
            return ( bool ) GetValue( AllowAddAxisProperty );
        }
        set
        {
            SetValue( AllowAddAxisProperty, value );
        }
    }


    /// <summary>
    /// Allow users to add candles to the chart.
    /// </summary>
    public bool AllowAddCandles
    {
        get
        {
            return ( bool ) GetValue( AllowAddCandlesProperty );
        }
        set
        {
            SetValue( AllowAddCandlesProperty, value );
        }
    }

    /// <summary>
    /// Allow users to add indicators to the chart.
    /// </summary>
    public bool AllowAddIndicators
    {
        get
        {
            return ( bool ) GetValue( AllowAddIndicatorsProperty );
        }
        set
        {
            SetValue( AllowAddIndicatorsProperty, value );
        }
    }

    /// <summary>
    /// Allow users to add orders to the chart.
    /// </summary>
    public bool AllowAddOrders
    {
        get
        {
            return ( bool ) GetValue( AllowAddOrdersProperty );
        }
        set
        {
            SetValue( AllowAddOrdersProperty, value );
        }
    }

    /// <summary>
    /// Allow users to add their own trades to the chart.
    /// </summary>
    public bool AllowAddOwnTrades
    {
        get
        {
            return ( bool ) GetValue( AllowAddOwnTradesProperty );
        }
        set
        {
            SetValue( AllowAddOwnTradesProperty, value );
        }
    }

    /// <summary>
    /// Called when any of the AllowAddArea, AllowAddOwnTrades, AllowAddOrders, AllowAddIndicators ....... properties are changed.
    /// </summary>
    /// <param name="dpo"></param>
    /// <param name="e"></param>
    private static void OnAllowAddXXXPropertyChanged( DependencyObject dpo, DependencyPropertyChangedEventArgs e )
    {
        ( ( ChartViewModel ) dpo ).AllowAddXXXPropertyChanged();
    }

    private void AllowAddXXXPropertyChanged()
    {
        //
        // Summary:
        //     Forces the System.Windows.Input.CommandManager to raise the System.Windows.Input.CommandManager.RequerySuggested
        //     event.
        CommandManager.InvalidateRequerySuggested();
        ClosePaneCommand.RaiseCanExecuteChanged();

        InteractedEvent?.Invoke();
    }

    /// <summary>
    /// Add a new X axis to the chart area.
    /// </summary>
    public ICommand AddXAxisCommand
    {
        get
        {
            return ( ICommand ) GetValue( AddXAxisCommandProperty );
        }
        set
        {
            SetValue( AddXAxisCommandProperty, value );
        }
    }

    /// <summary>
    /// Add a new Y axis to the chart area.
    /// </summary>
    public ICommand AddYAxisCommand
    {
        get
        {
            return ( ICommand ) GetValue( AddYAxisCommandProperty );
        }
        set
        {
            SetValue( AddYAxisCommandProperty, value );
        }
    }


    /// <summary>
    /// Remove an axis from the chart area.
    /// </summary>
    public ICommand RemoveAxisCommand
    {
        get
        {
            return ( ICommand ) GetValue( RemoveAxisCommandProperty );
        }
        set
        {
            SetValue( RemoveAxisCommandProperty, value );
        }
    }


    /// <summary>
    /// The list of available indicators types.
    /// </summary>
    public ObservableCollection<IndicatorType> IndicatorTypes
    {
        get
        {
            return ( ObservableCollection<IndicatorType> ) GetValue( IndicatorTypesProperty );
        }
        set
        {
            SetValue( IndicatorTypesProperty, value );
        }
    }

    public DelegateCommand<IDrawingSurfaceVM> ClosePaneCommand
    {
        get => _closePaneCommand;
    }



    public event Action AreaAddingEvent;

    public event Action<ChartArea> AddCandlesEvent;

    public event Action<ChartArea> AddIndicatorEvent;

    public event Action<ChartArea> AddOrdersEvent;

    public Action<ChartArea> AddTradesEvent;

    public Action<IChartElement> RemoveElementEvent;

    public Action<IChartElement, Subscription> RebuildCandlesEvent;

    public Action<Order> CancelActiveOrderEvent;

    public Action<ChartArea> UngroupEvent;

    public static Action InteractedEvent;

    
    private static void OnIsInteractedCallback( DependencyObject d, DependencyPropertyChangedEventArgs e )
    {
        ChartViewModel vm = (ChartViewModel)d;
        vm.CoerceValue( AllowAddAreaProperty );
        vm.CoerceValue( AllowAddAxisProperty );
        vm.CoerceValue( AllowAddCandlesProperty );
        vm.CoerceValue( AllowAddIndicatorsProperty );
        vm.CoerceValue( AllowAddOrdersProperty );
        vm.CoerceValue( AllowAddOwnTradesProperty );
        ( ( ChartViewModel ) d ).AllowAddXXXPropertyChanged();
    }

    private readonly ICommand CancelActiveOrdersCommand;

    private int _minimumRange;

    

    private readonly DelegateCommand<IDrawingSurfaceVM> _closePaneCommand;

    public ChartViewModel()
    {
        ChartPaneViewModels   = new ObservableCollection<ScichartSurfaceMVVM>();
        MinimumRange          = 50;
        ShowOverview          = false;
        ShowLegend            = true;
        IndicatorTypes        = new ObservableCollection<IndicatorType>();
        AddAreaCommand        = ( ICommand ) new DelegateCommand(            ExecuteAddAreaCommand,        CanExecuteAddAreaCommand );
        AddCandlesCommand     = ( ICommand ) new DelegateCommand<ChartArea>( ExecuteAddCandlesCommand,     CanExecuteAddAreaCommand );
        AddIndicatorCommand   = ( ICommand ) new DelegateCommand<ChartArea>( ExecuteAddIndicatorCommand,   CanExecuteAddIndicatorCommand );
        AddOrdersCommand      = ( ICommand ) new DelegateCommand<ChartArea>( ExecuteAddOrdersCommand,      CanExecuteAddOrdersCommand );
        AddTradesCommand      = ( ICommand ) new DelegateCommand<ChartArea>( ExecuteAddTradesCommand,      CanExecuteAddTradesCommand );
        UngroupCommand        = ( ICommand ) new DelegateCommand<ChartArea>( ExecuteUngroupCommand,        CanExecuteUngroupCommand );
        ShowHiddenAxesCommand = ( ICommand ) new DelegateCommand<ChartArea>( ExecuteShowHiddenAxesCommand, CanExecuteShowHiddenAxes );
        AddXAxisCommand       = ( ICommand ) new DelegateCommand<ChartArea>( ExecuteAddXAxisCommand,       CanExecuteAddXAxisCommand );
        AddYAxisCommand       = ( ICommand ) new DelegateCommand<ChartArea>( ExecuteAddYAxisCommand,       CanExecuteAddYAxisCommand );
        RemoveAxisCommand     = ( ICommand ) new DelegateCommand<ChartAxis>( a =>
                                                                                {

                                                                                    IChartArea area = a.ChartArea;

                                                                                    if ( area.XAxises.Contains( a ) )
                                                                                    {
                                                                                        area.XAxises.Remove( a );
                                                                                    }

                                                                                    if ( area.YAxises.Contains( a ) )
                                                                                    {
                                                                                        area.YAxises.Remove( a );
                                                                                    }
                                                                                }, CanExecuteRemoveAxisCommand );

        InitRangeDepProperty();
        _closePaneCommand = new DelegateCommand<IDrawingSurfaceVM>( s =>
                                                                                {

                                                                                    var chart = ( (ScichartSurfaceMVVM)s ).Chart;
                                                                                    var area = chart.Areas.FirstOrDefault( p => ( (ChartArea)p ).ViewModel == s );
                                                                                    if ( area == null )
                                                                                        return;
                                                                                    chart.RemoveArea( area );
                                                                                }, CanExecuteClosePaneCommand );

        CancelActiveOrdersCommand = ( ICommand ) new DelegateCommand<ChartArea>( ExecuteCancelActiveOrders, CanExecuteCancelActiveOrders );

        if ( this.IsDesignMode() )
            return;

        ChangeApplicationTheme();

        DevExpress.Xpf.Core.ThemeManager.ApplicationThemeChanged += new ThemeChangedRoutedEventHandler( OnApplicationThemeChanged );
    }



    public void InternalExecuteCancelActiveOrders( Func<Order, bool> myFunc )
    {        
        if ( myFunc == null )
            myFunc = o => true;

        CollectionHelper.ForEach( CollectionHelper.ToSet( ChartPaneViewModels.SelectMany( s => s.GetActiveOrders( o => o.State == OrderStates.Active || o.State == OrderStates.Pending ) ).Where( myFunc ) ), 
            o => 
            {
                Action<Order> s = CancelActiveOrderEvent;
                if ( s == null )
                    return;
                CancelActiveOrderEvent?.Invoke( o );
            } 
        );
    }    

    private void ChangeApplicationTheme() => SelectedTheme = ChartHelper2025.CurrChartTheme();

    private static object OnAllowAddXPropertyCoreceValueChanged( DependencyObject d, object e )
    {
        bool coerceValue = (bool)e;
        var vm = ( (ChartViewModel)d );

        bool result = !vm.IsInteracted ? false : coerceValue;

        return result;
    }

    public int MinimumRange
    {
        get => _minimumRange;
        set
        {
            SetValue( MinimumRangeProperty, value );
        }
    }

    public ObservableCollection<ScichartSurfaceMVVM> ChartPaneViewModels
    {
        get
        {
            return ( ObservableCollection<ScichartSurfaceMVVM> ) GetValue( ChartPaneViewModelProperty );
        }
        set
        {
            SetValue( ChartPaneViewModelProperty, value );
        }
    }

    public ICommand GetCancelActiveOrdersCommand()
    {
        return CancelActiveOrdersCommand;
    }

    public ICommand ShowHiddenAxesCommand
    {
        get
        {
            return ( ICommand ) GetValue( ShowHiddenAxesCommandProperty );
        }
        set
        {
            SetValue( ShowHiddenAxesCommandProperty, value );
        }
    }


    /// <summary>
    /// AddAreaCommand is a DelegateCommand of the following functions
    /// 
    ///     1) ExecuteAddAreaCommand
    ///     2) CanExecuteAddAreaCommand
    ///     
    /// </summary>
    public ICommand AddAreaCommand
    {
        get
        {
            return ( ICommand ) GetValue( AddAreaCommandProperty );
        }
        set
        {
            SetValue( AddAreaCommandProperty, value );
        }
    }

    /// <summary>
    /// AddCandlesCommand is a DelegateCommand of the following functions
    /// 
    ///     1) ExecuteAddCandlesCommand - This will invoke the AddCandlesEvent 
    ///          
    ///     2) CanExecuteAddCandlesCommand
    ///     
    /// </summary>
    public ICommand AddCandlesCommand
    {
        get
        {
            return ( ICommand ) GetValue( AddCandlesCommandProperty );
        }
        set
        {
            SetValue( AddCandlesCommandProperty, value );
        }
    }

    /// <summary>
    /// AddCandlesCommand is a DelegateCommand of the following functions
    /// 
    ///     1) ExecuteAddIndicatorCommand - This will invoke the AddIndicatorEvent 
    ///     2) CanExecuteAddIndicatorCommand
    ///     
    /// </summary>
    public ICommand AddIndicatorCommand
    {
        get
        {
            return ( ICommand ) GetValue( AddIndicatorCommandProperty );
        }
        set
        {
            SetValue( AddIndicatorCommandProperty, value );
        }
    }

    /// <summary>
    /// AddCandlesCommand is a DelegateCommand of the following functions
    /// 
    ///     1) ExecuteAddOrdersCommand - This will invoke the AddOrdersEvent
    ///     2) CanExecuteAddOrdersCommand
    ///     
    /// </summary>
    public ICommand AddOrdersCommand
    {
        get
        {
            return ( ICommand ) GetValue( AddOrdersCommandProperty );
        }
        set
        {
            SetValue( AddOrdersCommandProperty, value );
        }
    }

    /// <summary>
    /// AddCandlesCommand is a DelegateCommand of the following functions
    /// 
    ///     1) ExecuteAddTradesCommand - This will invoke the AddTradesEvent
    ///     2) CanExecuteAddTradesCommand
    ///     
    /// </summary>
    public ICommand AddTradesCommand
    {
        get
        {
            return ( ICommand ) GetValue( AddTradesCommandProperty );
        }
        set
        {
            SetValue( AddTradesCommandProperty, value );
        }
    }

    /// <summary>
    /// AddCandlesCommand is a DelegateCommand of the following functions
    /// 
    ///     1) ExecuteUngroupCommand - This will invoke the UngroupEvent
    ///     2) CanExecuteUngroupCommand
    ///     
    /// </summary>
    public ICommand UngroupCommand
    {
        get
        {
            return ( ICommand ) GetValue( UngroupCommandProperty );
        }
        set
        {
            SetValue( UngroupCommandProperty, value );
        }
    }

    public void InvokeRebuildCandlesEvent( IChartElement chartElement, Subscription subscription )
    {        
        RebuildCandlesEvent?.Invoke( chartElement, subscription );
    }



    public void InitRangeDepProperty()
    {
        VisibleRangeDpo.InitRangeDepProperty( this );
    }

    public void InvokeRemoveElementEvent( IChartElement chartElement )
    {        
        RemoveElementEvent?.Invoke( chartElement );
    }

    private ChartArea GetRealChartArea( ChartArea area )
    {        
        if ( area != null )
            return area;

        ObservableCollection<ScichartSurfaceMVVM> drawingSurfaceVM = ChartPaneViewModels;

        if ( drawingSurfaceVM == null )
            return null;
        
        return drawingSurfaceVM.FirstOrDefault<ScichartSurfaceMVVM>()?.Area;
    }

    private void ExecuteAddAreaCommand( object area )
    {        
        AreaAddingEvent?.Invoke();
    }

    private bool CanExecuteAddAreaCommand( object area ) => AllowAddArea;

    private void ExecuteAddCandlesCommand( ChartArea area )
    {
        AddCandlesEvent?.Invoke( GetRealChartArea( area ) );
    }

    private bool CanExecuteAddAreaCommand( ChartArea area )
    {
        return AllowAddCandles && GetRealChartArea( area ) != null;
    }

    private void ExecuteAddIndicatorCommand( ChartArea area )
    {
        AddIndicatorEvent?.Invoke( GetRealChartArea( area ) );
    }

    private bool CanExecuteAddIndicatorCommand( ChartArea area )
    {
        return AllowAddIndicators && GetRealChartArea( area ) != null;
    }

    private void ExecuteAddOrdersCommand( ChartArea area )
    {
        AddOrdersEvent?.Invoke( GetRealChartArea( area ) );
    }

    private bool CanExecuteAddOrdersCommand( ChartArea area )
    {
        return AllowAddOrders && GetRealChartArea( area ) != null;
    }

    private void ExecuteAddTradesCommand( ChartArea area )
    {

        AddTradesEvent?.Invoke( GetRealChartArea( area ) );
    }

    private bool CanExecuteAddTradesCommand( ChartArea area )
    {
        return AllowAddOwnTrades && GetRealChartArea( area ) != null;
    }

    private void ExecuteUngroupCommand( ChartArea area )
    {
        UngroupEvent?.Invoke( GetRealChartArea( area ) );
    }

    private bool CanExecuteUngroupCommand( ChartArea area )
    {
        return GetRealChartArea( area ) != null;
    }

    private void ExecuteShowHiddenAxesCommand( ChartArea area )
    {
        if ( area != null )
        {
            area.ViewModel.ShowHiddenAxesCommand.Execute( null );
        }
        else
            CollectionHelper.ForEach<ScichartSurfaceMVVM>( ( IEnumerable<ScichartSurfaceMVVM> ) ChartPaneViewModels, p => p.Area.ViewModel.ShowHiddenAxesCommand.Execute( null ) );
    }

    private bool CanExecuteShowHiddenAxes( ChartArea area ) => IsInteracted;

    private void ExecuteAddXAxisCommand( ChartArea a )
    {
        var area = GetRealChartArea(a);
        if ( area == null )
            return;

        area.XAxises.Add
        (
            new ChartAxis()
            {
                AxisType = ChartAxisType.CategoryDateTime,
                TimeZone = ( a.XAxises ).LastOrDefault()?.TimeZone
            }
        );
    }

    private bool CanExecuteAddXAxisCommand( ChartArea area )
    {
        return AllowAddAxis;
    }

    private void ExecuteAddYAxisCommand( ChartArea a )
    {
        ChartArea area = GetRealChartArea(a);
        if ( area == null )
            return;

        area.YAxises.Add
        (
            new ChartAxis()
            {
                AxisType = ChartAxisType.Numeric
            }
        );
    }

    private bool CanExecuteAddYAxisCommand( ChartArea area )
    {
        return AllowAddAxis && GetRealChartArea( area ) != null;
    }

    private bool CanExecuteRemoveAxisCommand( ChartAxis area )
    {
        return IsInteracted && area?.ChartArea != null && !CompareHelper.IsDefault<ChartAxis>( area ) && AllowAddAxis;
    }

    private bool CanExecuteClosePaneCommand( IDrawingSurfaceVM area )
    {
        return AllowAddArea;
    }

    private void ExecuteCancelActiveOrders( ChartArea area )
    {
        InternalExecuteCancelActiveOrders( ( Func<Order, bool> ) null );
    }

    private bool CanExecuteCancelActiveOrders( ChartArea area )
    {
        return IsInteracted;
    }

    private void OnApplicationThemeChanged( DependencyObject dpo, ThemeChangedRoutedEventArgs e )
    {
        ChangeApplicationTheme();
    }

    

    #region Tony Added

    public event EventHandler<AddCandlesEventArgs>  CodingAddCandlesEvent;


    public void ExecuteCodingAddCandles( ChartArea chartArea, Subscription series )
    {
        CodingAddCandlesEvent?.Invoke( this, new AddCandlesEventArgs( chartArea, series ) );
    }

    public bool CanExecuteCodingAddCandles()
    {
        return true;
    }
    #endregion Tony Added

}


//using DevExpress.Xpf.Core;
//using Ecng.Collections;
//using Ecng.Xaml;
//using StockSharp.Charting;
//using SciChart.Charting.Common.Helpers;
//using SciChart.Charting.Visuals.TradeChart;
//using StockSharp.Algo.Candles;
//using StockSharp.Xaml;
//using StockSharp.Xaml.Charting;
//using System;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Windows;
//using System.Windows.Input;
//using MoreLinq;
//using StockSharp.BusinessEntities;
//using StockSharp.Messages;
//using System.Collections.Generic; 
//using fx.Collections;
//using StockSharp.Algo.Indicators;
//using StockSharp.Charting;

//public sealed partial class ChartViewModel : DependencyObject
//{
//    private bool _isInteracted = false;
//    private bool _isProgrammable;
//    private int _minimumRange;

//    private static int staticChartCount;

//    public int _instanceCount = ++staticChartCount;
//    public int InstanceCount
//    {
//        get
//        {
//            return _instanceCount;
//        }
//    }

//    public ChartViewModel( )
//    {
//        ScichartSurfaceViewModels = new ObservableCollection<ScichartSurfaceMVVM>( );
//        MinimumRange              = 50;
//        ShowOverview              = false;
//        ShowLegend                = true;
//        IndicatorTypes            = new ObservableCollection<IndicatorType>( );
//        AddAreaCommand            = new ActionCommand( ExecuteAddAreaCommand,                   CanExecuteAddAreaCommand      );
//        AddCandlesCommand         = new ActionCommand<ChartArea>( ExecuteAddCandlesCommand,     CanExecuteAddCandlesCommand   );
//        AddIndicatorCommand       = new ActionCommand<ChartArea>( ExecuteAddIndicatorCommand,   CanExecuteAddIndicatorCommand );
//        AddOrdersCommand          = new ActionCommand<ChartArea>( ExecuteAddOrdersCommand,      CanExecuteAddOrdersCommand    );
//        AddTradesCommand          = new ActionCommand<ChartArea>( ExecuteAddTradesCommand,      CanExecuteAddTradesCommand    );
//        ShowHiddenAxesCommand     = new ActionCommand<ChartArea>( ExecuteShowHiddenAxesCommand, CanExecuteShowHiddenAxes      );
//        AddXAxisCommand           = new ActionCommand<ChartArea>( ExecuteAddXAxisCommand,       a => AllowAddAxis             );
//        AddYAxisCommand           = new ActionCommand<ChartArea>( ExecuteAddYAxisCommand,       a => AllowAddAxis             );
//        RemoveAxisCommand         = new ActionCommand<ChartAxis>( ExecuteRemoveAxisCommand,     CanExecuteRemoveAxisCommand   );
//        InitRangeDepProperty(                                                                                                 );
//        _closePaneCommand         = new ActionCommand<IChildPane>( ExecuteClosePaneCommand,     a => IsInteracted             );

//        CancelActiveOrdersCommand = new ActionCommand<ChartArea>( ExecuteCancelActiveOrders, CanExecuteCancelActiveOrders     );

//        if ( IsDesignMode( ) )
//        {
//            return;
//        }

//        ChangeApplicationTheme( );
//        DevExpress.Xpf.Core.ThemeManager.ApplicationThemeChanged += new ThemeChangedRoutedEventHandler( ThemeManager_ApplicationThemeChanged );
//    }

//    //internal static void OnIsInteractedPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
//    //{
//    //    ( ( ChartViewModel ) d )._isInteracted = ( bool ) e.NewValue;
//    //}

//    private static void OnIsInteractedPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
//    {
//        ChartViewModel vm = (ChartViewModel) d;
//        vm.CoerceValue( AllowAddAreaProperty );
//        vm.CoerceValue( AllowAddAxisProperty );
//        vm.CoerceValue( AllowAddCandlesProperty );
//        vm.CoerceValue( AllowAddIndicatorsProperty );
//        vm.CoerceValue( AllowAddOrdersProperty );
//        vm.CoerceValue( AllowAddOwnTradesProperty );
//        ( ( ChartViewModel ) d ).AllowAddXPropertyChanged( );
//    }

//    internal static void OnIsProgrammablePropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
//    {
//        ( ( ChartViewModel ) d )._isProgrammable = ( bool ) e.NewValue;
//    }

//    internal static void OnMinimumRangePropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
//    {
//        ( ( ChartViewModel ) d )._minimumRange = ( int ) e.NewValue;
//    }

//    internal void ExecuteAddXAxisCommand( ChartArea a )
//    {
//        var area = GetRealChartArea( a );
//        if ( area == null )
//            return;

//        area.XAxises.Add
//        (
//            new ChartAxis( )
//            {
//                AxisType = ChartAxisType.CategoryDateTime,                
//                TimeZone = ( a.XAxises ).LastOrDefault( )?.TimeZone
//            }
//        );
//    }

//    private ChartArea GetRealChartArea( ChartArea area )
//    {    
//        if ( area != null)
//            return area;

//        ObservableCollection<ScichartSurfaceMVVM> vm = ScichartSurfaceViewModels;
//        if ( vm == null )
//            return null;

//        return vm.FirstOrDefault()?.Area;
//    }

//    internal void ExecuteAddYAxisCommand( ChartArea a )
//    {
//        ChartArea area = GetRealChartArea( a );
//        if ( area == null )
//            return;

//        area.YAxises.Add
//        (
//            new ChartAxis( )
//            {
//                AxisType = ChartAxisType.Numeric                
//            }
//        );
//    }

//    internal void ExecuteRemoveAxisCommand( ChartAxis axis )
//    {
//        var area = axis.ChartArea;

//        if ( area.XAxises.Contains( axis ) )
//        {
//            area.XAxises.Remove( axis );
//        }

//        if ( !area.YAxises.Contains( axis ) )
//        {
//            return;
//        }

//        area.YAxises.Remove( axis );
//    }

//    private bool CanExecuteRemoveAxisCommand( ChartAxis a )
//    {
//        if ( IsInteracted && ( a?.ChartArea != null ) /*&& !a.IsDefault*/ )
//        {
//            return AllowAddAxis;
//        }

//        return false;
//    }

//    internal void ExecuteClosePaneCommand( IChildPane c )
//    {
//        //var areas     = ( ( ScichartSurfaceMVVM )c ).Chart.ChartAreas;
//        //var chartArea = areas.FirstOrDefault( a => a.ViewModel == c );

//        //if ( chartArea == null )
//        //{
//        //    return;
//        //}

//        //areas.Remove( chartArea );
//    }

//    public ObservableCollection<ScichartSurfaceMVVM> ScichartSurfaceViewModels
//    {
//        get
//        {
//            return ( ObservableCollection<ScichartSurfaceMVVM> ) GetValue( ScichartSurfaceViewModelsProperty );
//        }
//        set
//        {
//            SetValue( ScichartSurfaceViewModelsProperty, value );
//        }
//    }

//    private void ChangeApplicationTheme( )
//    {
//        SelectedTheme = ApplicationThemeHelper.ApplicationThemeName.ToChartTheme( );
//    }



//    public ICommand AddAreaCommand
//    {
//        get
//        {
//            return ( ICommand ) GetValue( AddAreaCommandProperty );
//        }
//        set
//        {
//            SetValue( AddAreaCommandProperty, value );
//        }
//    }

//    public ICommand AddCandlesCommand
//    {
//        get
//        {
//            return ( ICommand ) GetValue( AddCandlesCommandProperty );
//        }
//        set
//        {
//            SetValue( AddCandlesCommandProperty, value );
//        }
//    }

//    public ICommand AddIndicatorCommand
//    {
//        get
//        {
//            return ( ICommand ) GetValue( AddIndicatorCommandProperty );
//        }
//        set
//        {
//            SetValue( AddIndicatorCommandProperty, value );
//        }
//    }

//    public ICommand AddOrdersCommand
//    {
//        get
//        {
//            return ( ICommand ) GetValue( AddOrdersCommandProperty );
//        }
//        set
//        {
//            SetValue( AddOrdersCommandProperty, value );
//        }
//    }

//    public ICommand AddTradesCommand
//    {
//        get
//        {
//            return ( ICommand ) GetValue( AddTradesCommandProperty );
//        }
//        set
//        {
//            SetValue( AddTradesCommandProperty, value );
//        }
//    }

//    internal void RaiseRebuildCandlesEvent( StockSharp.Charting.IChartElement chartUI, CandleSeries candleSeries )
//    {
//        var rebuildEvent = RebuildCandlesEvent;
//        if ( rebuildEvent == null )
//            return;
//        rebuildEvent( chartUI, candleSeries );
//    }

//    public ICommand AddXAxisCommand
//    {
//        get
//        {
//            return ( ICommand ) GetValue( AddXAxisCommandProperty );
//        }
//        set
//        {
//            SetValue( AddXAxisCommandProperty, value );
//        }
//    }

//    public ICommand AddYAxisCommand
//    {
//        get
//        {
//            return ( ICommand ) GetValue( AddYAxisCommandProperty );
//        }
//        set
//        {
//            SetValue( AddYAxisCommandProperty, value );
//        }
//    }

//    public ICommand RemoveAxisCommand
//    {
//        get
//        {
//            return ( ICommand ) GetValue( RemoveAxisCommandProperty );
//        }
//        set
//        {
//            SetValue( RemoveAxisCommandProperty, value );
//        }
//    }

//    public ObservableCollection<IndicatorType> IndicatorTypes
//    {
//        get
//        {
//            return ( ObservableCollection<IndicatorType> ) GetValue( IndicatorTypesProperty );
//        }
//        set
//        {
//            SetValue( IndicatorTypesProperty, value );
//        }
//    }

//    public ActionCommand<IChildPane> ClosePaneCommand
//    {
//        get
//        {
//            return _closePaneCommand;
//        }
//    }

//    public void OnRemoveElementEvent( IChartElement component )
//    {
//        Action<IChartElement> myRemoveAction = RemoveElementEvent;
//        if ( myRemoveAction == null )
//            return;
//        myRemoveAction( component );
//    }

//    public void InitRangeDepProperty( )
//    {
//        VisibleRangeDpo.InitRangeDepProperty( this );
//    }

//    public void InvokeRemoveElementEvent( StockSharp.Charting.IChartElement element )
//    {
//        RemoveElementEvent?.Invoke( element );
//    }

//    public void ExecuteAddAreaCommand( )
//    {
//        AreaAddedEvent?.Invoke( );
//    }

//    public bool CanExecuteAddAreaCommand( )
//    {
//        return AllowAddArea;
//    }

//    private void ExecuteAddCandlesCommand( ChartArea area )
//    {
//        Action<ChartArea> myEvent = AddCandlesEvent;
//        if ( myEvent == null )
//            return;

//        myEvent( GetRealChartArea( area ) );        
//    }


//    private bool CanExecuteAddCandlesCommand( ChartArea chartArea )
//    {
//        return AllowAddCandles;
//    }

//    private void ExecuteAddIndicatorCommand( ChartArea area )
//    {
//        Action<ChartArea> myEvent = AddIndicatorEvent;
//        if ( myEvent == null )
//            return;

//        myEvent( GetRealChartArea( area ) );        
//    }

//    private bool CanExecuteAddIndicatorCommand( ChartArea chartArea )
//    {
//        return AllowAddIndicators;
//    }

//    private void ExecuteAddOrdersCommand( ChartArea area )
//    {
//        Action<ChartArea> myEvent = AddOrdersEvent;
//        if ( myEvent == null )
//            return;

//        myEvent( GetRealChartArea( area ) );        
//    }

//    private bool CanExecuteAddOrdersCommand( ChartArea chartArea )
//    {
//        return AllowAddOrders;
//    }

//    private void ExecuteAddTradesCommand( ChartArea area )
//    {
//        Action<ChartArea> myEvent = AddTradesEvent;
//        if ( myEvent == null )
//            return;

//        myEvent( GetRealChartArea( area ) );
//    }

//    private bool CanExecuteAddTradesCommand( ChartArea chartArea )
//    {
//        return AllowAddOwnTrades;
//    }

//    private void ThemeManager_ApplicationThemeChanged( DependencyObject d, ThemeChangedRoutedEventArgs e )
//    {
//        ChangeApplicationTheme( );
//    }

//    private void ExecuteShowHiddenAxesCommand( ChartArea area )
//    {
//        if ( area != null )
//        {
//            area.ViewModel.ShowHiddenAxesCommand.Execute( null );
//        }
//        else
//        {
//            Ecng.Collections.CollectionHelper.ForEach( ScichartSurfaceViewModels, p => p.Area.ViewModel.ShowHiddenAxesCommand.Execute( null ) );

//        }

//    }

//    private bool CanExecuteShowHiddenAxes( ChartArea _param1 )
//    {
//        return IsInteracted;
//    }

//    private void ExecuteCancelActiveOrders( ChartArea _param1 )
//    {
//        OnExecuteCancelActiveOrders( null );
//    }

//    public void OnExecuteCancelActiveOrders( Func<Order, bool> cancelOrdersFunc )
//    {        
//        if( cancelOrdersFunc == null )
//        {
//            cancelOrdersFunc = ( p => true );
//        }

//        IEnumerable<Order> selectActiverOrders( ScichartSurfaceMVVM s )
//        {
//            return s.GetActiveOrders( o =>
//                                    {
//                                        if( o.State != OrderStates.Active )
//                                            return o.State == OrderStates.Pending;
//                                        return true;
//                                    } );
//        }

//        var some = ScichartSurfaceViewModels.SelectMany( selectActiverOrders).Where( cancelOrdersFunc);
//        var ordersSet = Enumerable.ToHashSet( some);

//        foreach ( var order in ordersSet )
//        {
//            var cancelOrder = CancelActiveOrderEvent;
//            if ( cancelOrder == null )
//                return;
//            cancelOrder( order );            
//        }
//    }

//    private bool CanExecuteCancelActiveOrders( ChartArea _param1 )
//    {
//        return IsInteracted;
//    }    
//    
//}
