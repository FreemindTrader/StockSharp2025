using DevExpress.Xpf.Core;
using Ecng.Collections;
using Ecng.Xaml;
using SciChart.Charting;
using SciChart.Charting.Common.Helpers;
using SciChart.Charting.Visuals.TradeChart;
using StockSharp.Algo.Candles;
using StockSharp.Xaml;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using MoreLinq;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using System.Collections.Generic; using fx.Collections;
using StockSharp.Algo.Indicators;

public sealed partial class ChartViewModel : DependencyObject
{
    private bool _isInteracted = false;
    private bool _isProgrammable;
    private int _minimumRange;


    public ChartViewModel( )
    {
        ScichartSurfaceViewModels = new ObservableCollection<ScichartSurfaceMVVM>( );
        MinimumRange              = 50;
        ShowOverview              = false;
        ShowLegend                = true;
        IndicatorTypes            = new ObservableCollection<IndicatorType>( );
        AddAreaCommand            = new ActionCommand( ExecuteAddAreaCommand,                   CanExecuteAddAreaCommand      );
        AddCandlesCommand         = new ActionCommand<ChartArea>( ExecuteAddCandlesCommand,     CanExecuteAddCandlesCommand   );
        AddIndicatorCommand       = new ActionCommand<ChartArea>( ExecuteAddIndicatorCommand,   CanExecuteAddIndicatorCommand );
        AddOrdersCommand          = new ActionCommand<ChartArea>( ExecuteAddOrdersCommand,      CanExecuteAddOrdersCommand    );
        AddTradesCommand          = new ActionCommand<ChartArea>( ExecuteAddTradesCommand,      CanExecuteAddTradesCommand    );
        ShowHiddenAxesCommand     = new ActionCommand<ChartArea>( ExecuteShowHiddenAxesCommand, CanExecuteShowHiddenAxes      );
        AddXAxisCommand           = new ActionCommand<ChartArea>( ExecuteAddXAxisCommand,       a => AllowAddAxis             );
        AddYAxisCommand           = new ActionCommand<ChartArea>( ExecuteAddYAxisCommand,       a => AllowAddAxis             );
        RemoveAxisCommand         = new ActionCommand<ChartAxis>( ExecuteRemoveAxisCommand,     CanExecuteRemoveAxisCommand   );
        InitRangeDepProperty(                                                                                                 );
        _closePaneCommand         = new ActionCommand<IChildPane>( ExecuteClosePaneCommand,     a => IsInteracted             );

        CancelActiveOrdersCommand = new ActionCommand<ChartArea>( ExecuteCancelActiveOrders, CanExecuteCancelActiveOrders     );

        if ( this.IsDesignMode( ) )
        {
            return;
        }

        ChangeApplicationTheme( );
        DevExpress.Xpf.Core.ThemeManager.ApplicationThemeChanged += new ThemeChangedRoutedEventHandler( ThemeManager_ApplicationThemeChanged );
    }

    //internal static void OnIsInteractedPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
    //{
    //    ( ( ChartViewModel ) d )._isInteracted = ( bool ) e.NewValue;
    //}

    private static void OnIsInteractedPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
    {
        ChartViewModel vm = (ChartViewModel) d;
        vm.CoerceValue( AllowAddAreaProperty );
        vm.CoerceValue( AllowAddAxisProperty );
        vm.CoerceValue( AllowAddCandlesProperty );
        vm.CoerceValue( AllowAddIndicatorsProperty );
        vm.CoerceValue( AllowAddOrdersProperty );
        vm.CoerceValue( AllowAddOwnTradesProperty );
        ( ( ChartViewModel ) d ).AllowAddXPropertyChanged( );
    }

    internal static void OnIsProgrammablePropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
    {
        ( ( ChartViewModel ) d )._isProgrammable = ( bool ) e.NewValue;
    }

    internal static void OnMinimumRangePropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
    {
        ( ( ChartViewModel ) d )._minimumRange = ( int ) e.NewValue;
    }

    internal void ExecuteAddXAxisCommand( ChartArea a )
    {
        var area = GetRealChartArea( a );
        if ( area == null )
            return;

        area.XAxises.Add
        (
            new ChartAxis( )
            {
                AxisType = ChartAxisType.CategoryDateTime,                
                TimeZone = ( a.XAxises ).LastOrDefault( )?.TimeZone
            }
        );
    }

    private ChartArea GetRealChartArea( ChartArea area )
    {    
        if ( area != null)
            return area;

        ObservableCollection<ScichartSurfaceMVVM> vm = ScichartSurfaceViewModels;
        if ( vm == null )
            return null;

        return vm.FirstOrDefault()?.Area;
    }

    internal void ExecuteAddYAxisCommand( ChartArea a )
    {
        ChartArea area = GetRealChartArea( a );
        if ( area == null )
            return;

        area.YAxises.Add
        (
            new ChartAxis( )
            {
                AxisType = ChartAxisType.Numeric                
            }
        );
    }

    internal void ExecuteRemoveAxisCommand( ChartAxis axis )
    {
        ChartArea area = axis.ChartArea;

        if ( area.XAxises.Contains( axis ) )
        {
            area.XAxises.Remove( axis );
        }

        if ( !area.YAxises.Contains( axis ) )
        {
            return;
        }

        area.YAxises.Remove( axis );
    }

    private bool CanExecuteRemoveAxisCommand( ChartAxis a )
    {
        if ( IsInteracted && ( a?.ChartArea != null ) && !a.IsDefault )
        {
            return AllowAddAxis;
        }

        return false;
    }

    internal void ExecuteClosePaneCommand( IChildPane c )
    {
        var areas     = ( ( ScichartSurfaceMVVM )c ).Chart.ChartAreas;
        var chartArea = areas.FirstOrDefault( a => a.ChartSurfaceViewModel == c );

        if ( chartArea == null )
        {
            return;
        }

        areas.Remove( chartArea );
    }

    public ObservableCollection<ScichartSurfaceMVVM> ScichartSurfaceViewModels
    {
        get
        {
            return ( ObservableCollection<ScichartSurfaceMVVM> ) GetValue( ScichartSurfaceViewModelsProperty );
        }
        set
        {
            SetValue( ScichartSurfaceViewModelsProperty, value );
        }
    }

    private void ChangeApplicationTheme( )
    {
        SelectedTheme = ApplicationThemeHelper.ApplicationThemeName.ToChartTheme( );
    }



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

    internal void RaiseRebuildCandlesEvent( IChartElement chartUI, CandleSeries candleSeries )
    {
        Action<IChartElement, CandleSeries> rebuildEvent = RebuildCandlesEvent;
        if ( rebuildEvent == null )
            return;
        rebuildEvent( chartUI, candleSeries );
    }

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

    public ActionCommand<IChildPane> ClosePaneCommand
    {
        get
        {
            return _closePaneCommand;
        }
    }

    public void InitRangeDepProperty( )
    {
        VisbleRangeDp.InitRangeDepProperty( this );
    }

    public void InvokeRemoveElementEvent( IChartElement element )
    {
        RemoveElementEvent?.Invoke( element );
    }

    public void ExecuteAddAreaCommand( )
    {
        AreaAddedEvent?.Invoke( );
    }

    public bool CanExecuteAddAreaCommand( )
    {
        return AllowAddArea;
    }

    private void ExecuteAddCandlesCommand( ChartArea area )
    {
        Action<ChartArea> myEvent = AddCandlesEvent;
        if ( myEvent == null )
            return;

        myEvent( GetRealChartArea( area ) );        
    }


    private bool CanExecuteAddCandlesCommand( ChartArea chartArea )
    {
        return AllowAddCandles;
    }

    private void ExecuteAddIndicatorCommand( ChartArea area )
    {
        Action<ChartArea> myEvent = AddIndicatorEvent;
        if ( myEvent == null )
            return;

        myEvent( GetRealChartArea( area ) );        
    }

    private bool CanExecuteAddIndicatorCommand( ChartArea chartArea )
    {
        return AllowAddIndicators;
    }

    private void ExecuteAddOrdersCommand( ChartArea area )
    {
        Action<ChartArea> myEvent = AddOrdersEvent;
        if ( myEvent == null )
            return;

        myEvent( GetRealChartArea( area ) );        
    }

    private bool CanExecuteAddOrdersCommand( ChartArea chartArea )
    {
        return AllowAddOrders;
    }

    private void ExecuteAddTradesCommand( ChartArea area )
    {
        Action<ChartArea> myEvent = AddTradesEvent;
        if ( myEvent == null )
            return;

        myEvent( GetRealChartArea( area ) );
    }

    private bool CanExecuteAddTradesCommand( ChartArea chartArea )
    {
        return AllowAddOwnTrades;
    }

    private void ThemeManager_ApplicationThemeChanged( DependencyObject d, ThemeChangedRoutedEventArgs e )
    {
        ChangeApplicationTheme( );
    }

    private void ExecuteShowHiddenAxesCommand( ChartArea area )
    {
        if ( area != null )
        {
            area.ChartSurfaceViewModel.ShowHiddenAxesCommand.Execute( null );
        }
        else
        {
            Ecng.Collections.CollectionHelper.ForEach( ScichartSurfaceViewModels, p => p.Area.ChartSurfaceViewModel.ShowHiddenAxesCommand.Execute( null ) );
            
        }

    }

    private bool CanExecuteShowHiddenAxes( ChartArea _param1 )
    {
        return IsInteracted;
    }

    private void ExecuteCancelActiveOrders( ChartArea _param1 )
    {
        OnExecuteCancelActiveOrders( null );
    }

    public void OnExecuteCancelActiveOrders( Func<Order, bool> cancelOrdersFunc )
    {        
        if( cancelOrdersFunc == null )
        {
            cancelOrdersFunc = ( p => true );
        }

        IEnumerable<Order> selectActiverOrders( ScichartSurfaceMVVM s )
        {
            return s.GetActiveOrders( o =>
                                    {
                                        if( o.State != OrderStates.Active )
                                            return o.State == OrderStates.Pending;
                                        return true;
                                    } );
        }

        var some = ScichartSurfaceViewModels.SelectMany( selectActiverOrders).Where( cancelOrdersFunc);
        var ordersSet = Enumerable.ToHashSet( some);

        foreach ( var order in ordersSet )
        {
            var cancelOrder = CancelActiveOrderEvent;
            if ( cancelOrder == null )
                return;
            cancelOrder( order );            
        }
    }

    private bool CanExecuteCancelActiveOrders( ChartArea _param1 )
    {
        return IsInteracted;
    }    
    #region Tony
    public void ExecuteCodingAddCandles( ChartArea chartArea, CandleSeries series )
    {
        CodingAddCandlesEvent?.Invoke( this, new AddCandlesEventArgs( chartArea, series ) );
    }

    public bool CanExecuteCodingAddCandles( )
    {
        return IsProgrammable;
    }
    #endregion

}
