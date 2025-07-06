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
using StockSharp.BusinessEntities;
using StockSharp.Algo.Indicators;

public sealed partial class ChartViewModel 
{
    #region Commands
    private readonly ICommand CancelActiveOrdersCommand;
    private readonly ActionCommand< IChildPane > _closePaneCommand;
    #endregion
    #region variables
    #endregion

    #region Events

    public event Action                             AreaAddedEvent;

    public event Action< ChartArea >                AddCandlesEvent;
    public event EventHandler<AddCandlesEventArgs>  CodingAddCandlesEvent;

    public event Action< ChartArea >                AddIndicatorEvent;
    //public event Action<ChartArea>                CodingAddIndicatorEvent;

    public event Action< ChartArea >                AddOrdersEvent;

    public event Action< ChartArea >                AddTradesEvent;

    public event Action< IChartElement >          RemoveElementEvent;
    private Action<IChartElement, CandleSeries>   RebuildCandlesEvent = null;
    private Action<Order>                           CancelActiveOrderEvent = null;
    private static Action                           InteractedEvent = null;
    #endregion

    #region Dependency Properties
    public static readonly DependencyProperty SelectedThemeProperty             = DependencyProperty.Register( nameof( SelectedTheme )      ,       typeof( string ), typeof( ChartViewModel ) );
    public static readonly DependencyProperty ShowOverviewProperty              = DependencyProperty.Register( nameof( ShowOverview )       ,       typeof( bool ), typeof( ChartViewModel ), new PropertyMetadata( false ) );
    public static readonly DependencyProperty ShowLegendProperty                = DependencyProperty.Register( nameof( ShowLegend )         ,       typeof( bool ), typeof( ChartViewModel ) );
    public static readonly DependencyProperty IsInteractedProperty              = DependencyProperty.Register( nameof( IsInteracted )       ,       typeof( bool ), typeof( ChartViewModel ), new PropertyMetadata( new PropertyChangedCallback( OnIsInteractedPropertyChanged ) ) );
    public static readonly DependencyProperty IsProgrammableProperty            = DependencyProperty.Register( nameof( IsProgrammable )     ,       typeof( bool ), typeof( ChartViewModel ), new PropertyMetadata( new PropertyChangedCallback( OnIsProgrammablePropertyChanged ) ) );
    public static readonly DependencyProperty AllowAddAreaProperty              = DependencyProperty.Register(nameof( AllowAddArea ),               typeof (bool), typeof (ChartViewModel), new PropertyMetadata( true, new PropertyChangedCallback(OnAllowAddXPropertyChanged), new CoerceValueCallback(OnAllowAddXPropertyCoreceValueChanged)));
    public static readonly DependencyProperty AllowAddAxisProperty              = DependencyProperty.Register(nameof( AllowAddAxis ),               typeof (bool), typeof (ChartViewModel), new PropertyMetadata( true, new PropertyChangedCallback(OnAllowAddXPropertyChanged), new CoerceValueCallback(OnAllowAddXPropertyCoreceValueChanged)));
    public static readonly DependencyProperty AllowAddCandlesProperty           = DependencyProperty.Register(nameof( AllowAddCandles ),            typeof (bool), typeof (ChartViewModel), new PropertyMetadata( true, new PropertyChangedCallback(OnAllowAddXPropertyChanged), new CoerceValueCallback(OnAllowAddXPropertyCoreceValueChanged)));
    public static readonly DependencyProperty AllowAddIndicatorsProperty        = DependencyProperty.Register(nameof( AllowAddIndicators ),         typeof (bool), typeof (ChartViewModel), new PropertyMetadata( true, new PropertyChangedCallback(OnAllowAddXPropertyChanged), new CoerceValueCallback(OnAllowAddXPropertyCoreceValueChanged)));
    public static readonly DependencyProperty AllowAddOrdersProperty            = DependencyProperty.Register(nameof( AllowAddOrders),              typeof (bool), typeof (ChartViewModel), new PropertyMetadata( true, new PropertyChangedCallback(OnAllowAddXPropertyChanged), new CoerceValueCallback(OnAllowAddXPropertyCoreceValueChanged)));
    public static readonly DependencyProperty AllowAddOwnTradesProperty         = DependencyProperty.Register(nameof( AllowAddOwnTrades),           typeof (bool), typeof (ChartViewModel), new PropertyMetadata( true, new PropertyChangedCallback(OnAllowAddXPropertyChanged), new CoerceValueCallback(OnAllowAddXPropertyCoreceValueChanged)));

    public static readonly DependencyProperty MinimumRangeProperty              = DependencyProperty.Register( nameof( MinimumRange )       ,       typeof( int ), typeof( ChartViewModel ), new PropertyMetadata( new PropertyChangedCallback( OnMinimumRangePropertyChanged ) ) );
    public static readonly DependencyProperty ScichartSurfaceViewModelsProperty = DependencyProperty.Register( nameof( ScichartSurfaceViewModels ), typeof( ObservableCollection<ScichartSurfaceMVVM> ), typeof( ChartViewModel ) );
    public static readonly DependencyProperty ShowHiddenAxesCommandProperty     = DependencyProperty.Register( nameof(ShowHiddenAxesCommand),       typeof (ICommand), typeof (ChartViewModel));
    public static readonly DependencyProperty AddAreaCommandProperty            = DependencyProperty.Register( nameof( AddAreaCommand )     ,       typeof( ICommand ), typeof( ChartViewModel ) );
    public static readonly DependencyProperty AddCandlesCommandProperty         = DependencyProperty.Register( nameof( AddCandlesCommand )  ,       typeof( ICommand ), typeof( ChartViewModel ) );
    public static readonly DependencyProperty AddIndicatorCommandProperty       = DependencyProperty.Register( nameof( AddIndicatorCommand ),       typeof( ICommand ), typeof( ChartViewModel ) );
    public static readonly DependencyProperty AddOrdersCommandProperty          = DependencyProperty.Register( nameof( AddOrdersCommand )   ,       typeof( ICommand ), typeof( ChartViewModel ) );
    public static readonly DependencyProperty AddTradesCommandProperty          = DependencyProperty.Register( nameof( AddTradesCommand )   ,       typeof( ICommand ), typeof( ChartViewModel ) );
    public static readonly DependencyProperty AddXAxisCommandProperty           = DependencyProperty.Register( nameof( AddXAxisCommand )    ,       typeof( ICommand ), typeof( ChartViewModel ) );
    public static readonly DependencyProperty AddYAxisCommandProperty           = DependencyProperty.Register( nameof( AddYAxisCommand )    ,       typeof( ICommand ), typeof( ChartViewModel ) );
    public static readonly DependencyProperty RemoveAxisCommandProperty         = DependencyProperty.Register( nameof( RemoveAxisCommand )  ,       typeof( ICommand ), typeof( ChartViewModel ) );
    public static readonly DependencyProperty IndicatorTypesProperty            = DependencyProperty.Register( nameof( IndicatorTypes )     ,       typeof( ObservableCollection< IndicatorType > ), typeof( ChartViewModel ) );

    private static void OnAllowAddXPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
    {
        ( ( ChartViewModel ) d ).AllowAddXPropertyChanged( );
    }

    private static object OnAllowAddXPropertyCoreceValueChanged( DependencyObject d, object e )
    {
        bool coerceValue = ( bool ) e;
        var vm = ( ( ChartViewModel ) d );

        bool result = !vm.IsInteracted ? false : coerceValue;

        return result;
    }

    private void AllowAddXPropertyChanged( )
    {
        CommandManager.InvalidateRequerySuggested( );
        ClosePaneCommand.RaiseCanExecuteChanged( );
        Action myEvent = InteractedEvent;
        if ( myEvent == null )
            return;
        myEvent( );
    }

    #endregion
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

    public bool IsInteracted
    {
        get
        {
            return _isInteracted;
        }
        set
        {
            SetValue( IsInteractedProperty, value );
        }
    }

    public bool IsProgrammable
    {
        get
        {
            return _isProgrammable;
        }
        set
        {
            SetValue( IsProgrammableProperty, value );
        }
    }




    public int MinimumRange
    {
        get
        {
            return _minimumRange;
        }
        set
        {
            SetValue( MinimumRangeProperty, value );
        }
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
}

