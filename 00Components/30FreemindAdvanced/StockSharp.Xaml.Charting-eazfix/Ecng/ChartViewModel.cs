// Decompiled with JetBrains decompiler
// Type: #=z$rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors.Helpers;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Xaml;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;


public sealed class ChartViewModel : DependencyObject
{
    public string SelectedTheme
    {
        get
        {
            return ( string ) this.GetValue( ChartViewModel.SelectedThemeProperty );
        }
        set
        {
            this.SetValue( ChartViewModel.SelectedThemeProperty, ( object ) value );
        }
    }

    public bool ShowOverview
    {
        get
        {
            return ( bool ) this.GetValue( ChartViewModel.ShowOverviewProperty );
        }
        set
        {
            this.SetValue( ChartViewModel.ShowOverviewProperty, ( object ) value );
        }
    }

    public bool ShowLegend
    {
        get
        {
            return ( bool ) this.GetValue( ChartViewModel.ShowLegendProperty );
        }
        set
        {
            this.SetValue( ChartViewModel.ShowLegendProperty, ( object ) value );
        }
    }

    public bool IsInteracted
    {
        get
        {
            return ( bool ) this.GetValue( ChartViewModel.IsInteractedProperty );
        }
        set
        {
            this.SetValue( ChartViewModel.IsInteractedProperty, ( object ) value );
        }
    }

    public bool AllowAddArea
    {
        get
        {
            return ( bool ) this.GetValue( ChartViewModel.AllowAddAreaProperty );
        }
        set
        {
            this.SetValue( ChartViewModel.AllowAddAreaProperty, ( object ) value );
        }
    }

    public bool AllowAddAxis
    {
        get
        {
            return ( bool ) this.GetValue( ChartViewModel.AllowAddAxisProperty );
        }
        set
        {
            this.SetValue( ChartViewModel.AllowAddAxisProperty, ( object ) value );
        }
    }

    public bool AllowAddCandles
    {
        get
        {
            return ( bool ) this.GetValue( ChartViewModel.AllowAddCandlesProperty );
        }
        set
        {
            this.SetValue( ChartViewModel.AllowAddCandlesProperty, ( object ) value );
        }
    }

    public bool AllowAddIndicators
    {
        get
        {
            return ( bool ) this.GetValue( ChartViewModel.AllowAddIndicatorsProperty );
        }
        set
        {
            this.SetValue( ChartViewModel.AllowAddIndicatorsProperty, ( object ) value );
        }
    }

    public bool AllowAddOrders
    {
        get
        {
            return ( bool ) this.GetValue( ChartViewModel.AllowAddOrdersProperty );
        }
        set
        {
            this.SetValue( ChartViewModel.AllowAddOrdersProperty, ( object ) value );
        }
    }

    public bool AllowAddOwnTrades
    {
        get
        {
            return ( bool ) this.GetValue( ChartViewModel.AllowAddOwnTradesProperty );
        }
        set
        {
            this.SetValue( ChartViewModel.AllowAddOwnTradesProperty, ( object ) value );
        }
    }

    private static void OnAllowAddAreaCallback( DependencyObject _param0, DependencyPropertyChangedEventArgs _param1 )
    {
        ( ( ChartViewModel ) _param0 ).AllowAddXPropertyChanged();
    }

    private void AllowAddXPropertyChanged()
    {
        CommandManager.InvalidateRequerySuggested();
        this.ClosePaneCommand.RaiseCanExecuteChanged();
        Action izKc6jG0gptPuO01M = ChartViewModel.AllowToRemoveEvent;
        if ( izKc6jG0gptPuO01M == null )
            return;
        izKc6jG0gptPuO01M();
    }

    public ICommand AddXAxisCommand
    {
        get
        {
            return ( ICommand ) this.GetValue( ChartViewModel.AddXAxisCommandProperty );
        }
        set
        {
            this.SetValue( ChartViewModel.AddXAxisCommandProperty, ( object ) value );
        }
    }

    public ICommand AddYAxisCommand
    {
        get
        {
            return ( ICommand ) this.GetValue( ChartViewModel.AddYAxisCommandProperty );
        }
        set
        {
            this.SetValue( ChartViewModel.AddYAxisCommandProperty, ( object ) value );
        }
    }

    public ICommand RemoveAxisCommand
    {
        get
        {
            return ( ICommand ) this.GetValue( ChartViewModel.RemoveAxisCommandProperty );
        }
        set
        {
            this.SetValue( ChartViewModel.RemoveAxisCommandProperty, ( object ) value );
        }
    }

    public ObservableCollection<IndicatorType> IndicatorTypes
    {
        get
        {
            return ( ObservableCollection<IndicatorType> ) this.GetValue( ChartViewModel.RemoveAxisCommandProperty );
        }
        set
        {
            this.SetValue( ChartViewModel.RemoveAxisCommandProperty, ( object ) value );
        }
    }

    public DelegateCommand<IScichartSurfaceVM> ClosePaneCommand
    {
        get => this._closePaneCommand;
    }



    public event Action AreaAddingEvent;

    public event Action< ChartArea > AddCandlesEvent;

    public event Action<ChartArea> AddIndicatorEvent;

    public event Action<ChartArea> AddOrdersEvent;

    public Action<ChartArea> AddTradesEvent;

    public Action<IChartElement> RemoveElementEvent;

    public Action<IChartElement, Subscription> RebuildCandlesEvent;

    public Action<Order> CancelActiveOrderEvent;

    public Action<ChartArea> UngroupEvent;

    public static readonly DependencyProperty SelectedThemeProperty = DependencyProperty.Register(nameof (SelectedTheme), typeof (string), typeof (ChartViewModel));

    public static readonly DependencyProperty ShowOverviewProperty = DependencyProperty.Register(nameof (ShowOverview), typeof (bool), typeof (ChartViewModel), new PropertyMetadata((object) false));

    public static readonly DependencyProperty ShowLegendProperty = DependencyProperty.Register(nameof (ShowLegend), typeof (bool), typeof (ChartViewModel));

    public static Action AllowToRemoveEvent;

    public static readonly DependencyProperty IsInteractedProperty = DependencyProperty.Register(nameof (IsInteracted), typeof (bool), typeof (ChartViewModel), new PropertyMetadata((object) true, new PropertyChangedCallback(ChartViewModel.OnIsInteractedCallback)));

    private static void OnIsInteractedCallback( DependencyObject d, DependencyPropertyChangedEventArgs e )
    {
        ChartViewModel vm = (ChartViewModel) d;
        vm.CoerceValue( ChartViewModel.AllowAddAreaProperty );
        vm.CoerceValue( ChartViewModel.AllowAddAxisProperty );
        vm.CoerceValue( ChartViewModel.AllowAddCandlesProperty );
        vm.CoerceValue( ChartViewModel.AllowAddIndicatorsProperty );
        vm.CoerceValue( ChartViewModel.AllowAddOrdersProperty );
        vm.CoerceValue( ChartViewModel.AllowAddOwnTradesProperty );
        ( ( ChartViewModel ) d ).AllowAddXPropertyChanged();
    }

    private readonly ICommand CancelActiveOrdersCommand;

    private int _minimumRange;

    public static readonly DependencyProperty ChartPaneViewModelProperty = DependencyProperty.Register(nameof (ChartPaneViewModels), typeof (ObservableCollection<ScichartSurfaceMVVM>), typeof (ChartViewModel));

    

    public static readonly DependencyProperty ShowHiddenAxesCommandProperty = DependencyProperty.Register(nameof (ShowHiddenAxesCommand), typeof (ICommand), typeof (ChartViewModel));

    public static readonly DependencyProperty AddAreaCommandProperty = DependencyProperty.Register(nameof (AddAreaCommand), typeof (ICommand), typeof (ChartViewModel));

    public static readonly DependencyProperty AddCandlesCommandProperty = DependencyProperty.Register(nameof (AddCandlesCommand), typeof (ICommand), typeof (ChartViewModel));

    public static readonly DependencyProperty AddIndicatorCommandProperty = DependencyProperty.Register(nameof (AddIndicatorCommand), typeof (ICommand), typeof (ChartViewModel));

    public static readonly DependencyProperty AddOrdersCommandProperty = DependencyProperty.Register(nameof (AddOrdersCommand), typeof (ICommand), typeof (ChartViewModel));

    public static readonly DependencyProperty AddTradesCommandProperty = DependencyProperty.Register(nameof (AddTradesCommand), typeof (ICommand), typeof (ChartViewModel));

    public static readonly DependencyProperty UngroupCommandProperty = DependencyProperty.Register(nameof (UngroupCommand), typeof (ICommand), typeof (ChartViewModel));

    public static readonly DependencyProperty AddXAxisCommandProperty = DependencyProperty.Register(nameof (AddXAxisCommand), typeof (ICommand), typeof (ChartViewModel));

    public static readonly DependencyProperty AddYAxisCommandProperty = DependencyProperty.Register(nameof (AddYAxisCommand), typeof (ICommand), typeof (ChartViewModel));

    public static readonly DependencyProperty RemoveAxisCommandProperty = DependencyProperty.Register(nameof (RemoveAxisCommand), typeof (ICommand), typeof (ChartViewModel));

    public static readonly DependencyProperty RemoveAxisCommandProperty = DependencyProperty.Register(nameof (IndicatorTypes), typeof (ObservableCollection<IndicatorType>), typeof (ChartViewModel));

    public static readonly DependencyProperty AllowAddAreaProperty = DependencyProperty.Register(nameof (AllowAddArea), typeof (bool), typeof (ChartViewModel), new PropertyMetadata((object) true, new PropertyChangedCallback(ChartViewModel.OnAllowAddAreaCallback), new CoerceValueCallback(ChartViewModel.\u0023\u003DzgQVt7i8Rpksb)));

    public static readonly DependencyProperty AllowAddAxisProperty = DependencyProperty.Register(nameof (AllowAddAxis), typeof (bool), typeof (ChartViewModel), new PropertyMetadata((object) true, new PropertyChangedCallback(ChartViewModel.OnAllowAddAreaCallback), new CoerceValueCallback(ChartViewModel.\u0023\u003DzgQVt7i8Rpksb)));

    public static readonly DependencyProperty AllowAddCandlesProperty = DependencyProperty.Register(nameof (AllowAddCandles), typeof (bool), typeof (ChartViewModel), new PropertyMetadata((object) true, new PropertyChangedCallback(ChartViewModel.OnAllowAddAreaCallback), new CoerceValueCallback(ChartViewModel.\u0023\u003DzgQVt7i8Rpksb)));

    public static readonly DependencyProperty AllowAddIndicatorsProperty = DependencyProperty.Register(nameof (AllowAddIndicators), typeof (bool), typeof (ChartViewModel), new PropertyMetadata((object) true, new PropertyChangedCallback(ChartViewModel.OnAllowAddAreaCallback), new CoerceValueCallback(ChartViewModel.\u0023\u003DzgQVt7i8Rpksb)));

    public static readonly DependencyProperty AllowAddOrdersProperty = DependencyProperty.Register(nameof (AllowAddOrders), typeof (bool), typeof (ChartViewModel), new PropertyMetadata((object) true, new PropertyChangedCallback(ChartViewModel.OnAllowAddAreaCallback), new CoerceValueCallback(ChartViewModel.\u0023\u003DzgQVt7i8Rpksb)));

    public static readonly DependencyProperty AllowAddOwnTradesProperty = DependencyProperty.Register(nameof (AllowAddOwnTrades), typeof (bool), typeof (ChartViewModel), new PropertyMetadata((object) true, new PropertyChangedCallback(ChartViewModel.OnAllowAddAreaCallback), new CoerceValueCallback(ChartViewModel.\u0023\u003DzgQVt7i8Rpksb)));

    public static readonly DependencyProperty MinimumRangeProperty = DependencyProperty.Register(nameof (MinimumRange), typeof (int), typeof (ChartViewModel), new PropertyMetadata(new PropertyChangedCallback(ChartViewModel.SomeClass34343383.SomeMethond0343.OnMinimumRangeCallback)));


    private readonly DelegateCommand<IScichartSurfaceVM> _closePaneCommand;

    public ChartViewModel()
    {
        this.ChartPaneViewModels = new ObservableCollection<ScichartSurfaceMVVM>();
        this.MinimumRange = 50;
        this.ShowOverview = false;
        this.ShowLegend = true;
        this.IndicatorTypes = new ObservableCollection<IndicatorType>();
        this.AddAreaCommand = ( ICommand ) new DelegateCommand( new Action<object>( this.ExecuteAddAreaCommand ), new Func<object, bool>( this.CanExecuteAddAreaCommand ) );
        this.AddCandlesCommand = ( ICommand ) new DelegateCommand<ChartArea>( new Action<ChartArea>( this.ExecuteAddCandlesCommand ), new Func<ChartArea, bool>( this.CanExecuteAddAreaCommand ) );
        this.AddIndicatorCommand = ( ICommand ) new DelegateCommand<ChartArea>( new Action<ChartArea>( this.ExecuteAddIndicatorCommand ), new Func<ChartArea, bool>( this.CanExecuteAddIndicatorCommand ) );
        this.AddOrdersCommand = ( ICommand ) new DelegateCommand<ChartArea>( new Action<ChartArea>( this.ExecuteAddOrdersCommand ), new Func<ChartArea, bool>( this.CanExecuteAddOrdersCommand ) );
        this.AddTradesCommand = ( ICommand ) new DelegateCommand<ChartArea>( new Action<ChartArea>( this.ExecuteAddTradesCommand ), new Func<ChartArea, bool>( this.CanExecuteAddTradesCommand ) );
        this.UngroupCommand = ( ICommand ) new DelegateCommand<ChartArea>( new Action<ChartArea>( this.ExecuteUngroupCommand ), new Func<ChartArea, bool>( this.CanExecuteUngroupCommand ) );
        this.ShowHiddenAxesCommand = ( ICommand ) new DelegateCommand<ChartArea>( new Action<ChartArea>( this.ExecuteShowHiddenAxesCommand ), new Func<ChartArea, bool>( this.CanExecuteShowHiddenAxes ) );
        this.AddXAxisCommand = ( ICommand ) new DelegateCommand<ChartArea>( new Action<ChartArea>( this.ExecuteAddXAxisCommand ), new Func<ChartArea, bool>( this.CanExecuteAddXAxisCommand ) );
        this.AddYAxisCommand = ( ICommand ) new DelegateCommand<ChartArea>( new Action<ChartArea>( this.ExecuteAddYAxisCommand ), new Func<ChartArea, bool>( this.CanExecuteAddYAxisCommand ) );
        this.RemoveAxisCommand = ( ICommand ) new DelegateCommand<ChartAxis>( ChartViewModel.SomeClass34343383.ExecuteRemoveAxisCommand ?? ( ChartViewModel.SomeClass34343383.ExecuteRemoveAxisCommand = new Action<ChartAxis>( ChartViewModel.SomeClass34343383.SomeMethond0343.ExecuteRemoveAxisCommand2 ) ), new Func<ChartAxis, bool>( this.CanExecuteRemoveAxisCommand ) );
        this.InitRangeDepProperty();
        this._closePaneCommand = new DelegateCommand<IScichartSurfaceVM>( ChartViewModel.SomeClass34343383.ExecuteClosePaneCommand ?? ( ChartViewModel.SomeClass34343383.ExecuteClosePaneCommand = new Action<IScichartSurfaceVM>( ChartViewModel.SomeClass34343383.SomeMethond0343.ExecuteClosePaneCommand2 ) ), new Func<IScichartSurfaceVM, bool>( this.CanExecuteClosePaneCommand ) );
        this.CancelActiveOrdersCommand = ( ICommand ) new DelegateCommand<ChartArea>( new Action<ChartArea>( this.ExecuteCancelActiveOrders ), new Func<ChartArea, bool>( this.CanExecuteCancelActiveOrders ) );
        if ( this.IsDesignMode() )
            return;
        this.ChangeApplicationTheme();
        ThemeManager.ApplicationThemeChanged += new ThemeChangedRoutedEventHandler( this.\u0023\u003Dz_JOkHnKu927R4qx\u0024N\u0024sTKxE\u003D);
    }



    public void \u0023\u003Dz\u0024DK5seweHzSZIyjEhw\u003D\u003D(Func<Order, bool> _param1)
  {
    ChartViewModel.\u0023\u003DzymgCCsreH3nwJBFwtEPKXQw\u003D ccsreH3nwJbFwtEpkxQw = new ChartViewModel.\u0023\u003DzymgCCsreH3nwJBFwtEPKXQw\u003D();
ccsreH3nwJbFwtEpkxQw.\u0023\u003DzXCEqv64\u003D = this.CancelActiveOrderEvent ;
if (_param1 == null )
    _param1 = ChartViewModel.SomeClass34343383.\u0023\u003DzwCieN8nlFS3aCeRPgg\u003D\u003D ?? (ChartViewModel.SomeClass34343383.\u0023\u003DzwCieN8nlFS3aCeRPgg\u003D\u003D = new Func<Order, bool>(ChartViewModel.SomeClass34343383.SomeMethond0343.\u0023\u003DzO_BIxNwmDn6VDgqOIx_JE6RwPKwP));
    CollectionHelper.ForEach<Order>((IEnumerable<Order>) CollectionHelper.ToSet<Order>(this.ChartPaneViewModels.SelectMany<ScichartSurfaceMVVM, Order>(ChartViewModel.SomeClass34343383.\u0023\u003DzKeRpc1y1\u0024C\u0024IWjYsyQ\u003D\u003D ?? (ChartViewModel.SomeClass34343383.\u0023\u003DzKeRpc1y1\u0024C\u0024IWjYsyQ\u003D\u003D = new Func<ScichartSurfaceMVVM, IEnumerable<Order>>(ChartViewModel.SomeClass34343383.SomeMethond0343.\u0023\u003DzrhkYnMPbNPr3HVPmAT1zSUTwkFvh))).Where<Order>(_param1)), new Action<Order>(ccsreH3nwJbFwtEpkxQw.\u0023\u003Dz69cIxDaENs3AtcYRgfOovNI\u003D));
  }

private void ChangeApplicationTheme() => this.SelectedTheme = ChartHelper.CurrChartTheme();









private static object \u0023\u003DzgQVt7i8Rpksb( DependencyObject _param0, object _param1 )
  {
    return ( object ) ( bool ) ( !( ( ChartViewModel ) _param0 ).IsInteracted ? 0 : ( ( bool ) _param1 ? 1 : 0 ) );
}

public int MinimumRange
{
    get => this._minimumRange;
    set
    {
        this.SetValue( ChartViewModel.MinimumRangeProperty, ( object ) value );
    }
}

public ObservableCollection<ScichartSurfaceMVVM> ChartPaneViewModels
{
    get
    {
        return ( ObservableCollection<ScichartSurfaceMVVM> ) this.GetValue( ChartViewModel.ChartPaneViewModelProperty );
    }
    set
    {
        this.SetValue( ChartViewModel.ChartPaneViewModelProperty, ( object ) value );
    }
}

public ICommand \u0023\u003Dzd4glwaTNkyYeey63BQ\u003D\u003D()
  {
    return this.CancelActiveOrdersCommand;
  }

  public ICommand ShowHiddenAxesCommand
{
    get
    {
        return ( ICommand ) this.GetValue( ChartViewModel.ShowHiddenAxesCommandProperty );
    }
    set
    {
        this.SetValue( ChartViewModel.ShowHiddenAxesCommandProperty, ( object ) value );
    }
}

public ICommand AddAreaCommand
{
    get
    {
        return ( ICommand ) this.GetValue( ChartViewModel.AddAreaCommandProperty );
    }
    set
    {
        this.SetValue( ChartViewModel.AddAreaCommandProperty, ( object ) value );
    }
}

public ICommand AddCandlesCommand
{
    get
    {
        return ( ICommand ) this.GetValue( ChartViewModel.AddCandlesCommandProperty );
    }
    set
    {
        this.SetValue( ChartViewModel.AddCandlesCommandProperty, ( object ) value );
    }
}

public ICommand AddIndicatorCommand
{
    get
    {
        return ( ICommand ) this.GetValue( ChartViewModel.AddIndicatorCommandProperty );
    }
    set
    {
        this.SetValue( ChartViewModel.AddIndicatorCommandProperty, ( object ) value );
    }
}

public ICommand AddOrdersCommand
{
    get
    {
        return ( ICommand ) this.GetValue( ChartViewModel.AddOrdersCommandProperty );
    }
    set
    {
        this.SetValue( ChartViewModel.AddOrdersCommandProperty, ( object ) value );
    }
}

public ICommand AddTradesCommand
{
    get
    {
        return ( ICommand ) this.GetValue( ChartViewModel.AddTradesCommandProperty );
    }
    set
    {
        this.SetValue( ChartViewModel.AddTradesCommandProperty, ( object ) value );
    }
}

public ICommand UngroupCommand
{
    get
    {
        return ( ICommand ) this.GetValue( ChartViewModel.UngroupCommandProperty );
    }
    set
    {
        this.SetValue( ChartViewModel.UngroupCommandProperty, ( object ) value );
    }
}

public void \u0023\u003Dzld7tWxZuooQ2UzOmtQ\u003D\u003D(
    IChartElement _param1,
    Subscription _param2)
  {
    Action<IChartElement, Subscription> zu6zyxyyj5lOwEolg = this.RebuildCandlesEvent;
if ( zu6zyxyyj5lOwEolg == null )
    return;
zu6zyxyyj5lOwEolg( _param1, _param2 );
  }

  

public void InitRangeDepProperty()
{
    \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR.InitRangeDepProperty( ( object ) this );
}

public void \u0023\u003DzzXq5ccDMuPZc( IChartElement _param1 )
  {
    Action<IChartElement> zeBeQvx4 = this.RemoveElementEvent;
    if ( zeBeQvx4 == null )
        return;
    zeBeQvx4( _param1 );
}

private ChartArea GetRealChartArea( ChartArea _param1 )
{
    ChartArea chartArea = _param1;
    if ( chartArea != null )
        return chartArea;
    ObservableCollection<ScichartSurfaceMVVM> chartPaneViewModels = this.ChartPaneViewModels;
    if ( chartPaneViewModels == null )
        return ( ChartArea ) null;
    return chartPaneViewModels.FirstOrDefault<ScichartSurfaceMVVM>()?.Area;
}

private void ExecuteAddAreaCommand( object _param1 )
{
    Action zJ36T8eJdWvlL = this.AreaAddingEvent;
    if ( zJ36T8eJdWvlL == null )
        return;
    zJ36T8eJdWvlL();
}

private bool CanExecuteAddAreaCommand( object _param1 ) => this.AllowAddArea;

private void ExecuteAddCandlesCommand( ChartArea _param1 )
{
    AddCandlesEvent?.Invoke( this.GetRealChartArea( _param1 ) );
}

private bool CanExecuteAddAreaCommand( ChartArea _param1 )
{
    return this.AllowAddCandles && this.GetRealChartArea( _param1 ) != null;
}

private void ExecuteAddIndicatorCommand( ChartArea _param1 )
{

    AddIndicatorEvent?.Invoke( this.GetRealChartArea( _param1 ) );
}

private bool CanExecuteAddIndicatorCommand( ChartArea _param1 )
{
    return this.AllowAddIndicators && this.GetRealChartArea( _param1 ) != null;
}

private void ExecuteAddOrdersCommand( ChartArea _param1 )
{
    AddOrdersEvent?.Invoke( this.GetRealChartArea( _param1 ) );
}

private bool CanExecuteAddOrdersCommand( ChartArea _param1 )
{
    return this.AllowAddOrders && this.GetRealChartArea( _param1 ) != null;
}

private void ExecuteAddTradesCommand( ChartArea _param1 )
{

    AddTradesEvent?.Invoke( this.GetRealChartArea( _param1 ) );
}

private bool CanExecuteAddTradesCommand( ChartArea _param1 )
{
    return this.AllowAddOwnTrades && this.GetRealChartArea( _param1 ) != null;
}

private void ExecuteUngroupCommand( ChartArea _param1 )
{

    UngroupEvent?.Invoke( this.GetRealChartArea( _param1 ) );
}

private bool CanExecuteUngroupCommand( ChartArea _param1 )
{
    return this.GetRealChartArea( _param1 ) != null;
}

private void ExecuteShowHiddenAxesCommand( ChartArea _param1 )
{
    if ( Equatable<ChartArea>.op_Inequality( ( Equatable<ChartArea> ) _param1, ( ChartArea ) null ) )
        _param1.ViewModel.ShowHiddenAxesCommand.TryExecute( ( object ) null );
    else
        CollectionHelper.ForEach<ScichartSurfaceMVVM>( ( IEnumerable<ScichartSurfaceMVVM> ) this.ChartPaneViewModels, ChartViewModel.SomeClass34343383.\u0023\u003Dz4K9Ew\u00245ncgrgb99V4w\u003D\u003D ?? ( ChartViewModel.SomeClass34343383.\u0023\u003Dz4K9Ew\u00245ncgrgb99V4w\u003D\u003D = new Action<ScichartSurfaceMVVM>( ChartViewModel.SomeClass34343383.SomeMethond0343.\u0023\u003Dz0sbMZcoPgn7DQ3i\u0024I2emR\u00244\u003D) ));
}

private bool CanExecuteShowHiddenAxes( ChartArea _param1 ) => this.IsInteracted;

private void ExecuteAddXAxisCommand( ChartArea _param1 )
{
    _param1 = this.GetRealChartArea( _param1 );
    if ( _param1 == null )
        return;
    // ISSUE: explicit non-virtual call
    ( ( ICollection<IChartAxis> ) __nonvirtual( _param1.XAxises ) ).Add( ( IChartAxis ) new ChartAxis()
    {
        AxisType = ChartAxisType.CategoryDateTime,
        TimeZone = ( ( IEnumerable<IChartAxis> ) _param1.XAxises ).LastOrDefault<IChartAxis>()?.TimeZone
    } );
}

private bool CanExecuteAddXAxisCommand( ChartArea _param1 )
{
    return this.AllowAddAxis;
}

private void ExecuteAddYAxisCommand( ChartArea _param1 )
{
    ChartArea chartArea = this.GetRealChartArea(_param1);
    if ( chartArea == null )
        return;
    // ISSUE: explicit non-virtual call
    ( ( ICollection<IChartAxis> ) __nonvirtual( chartArea.YAxises ) ).Add( ( IChartAxis ) new ChartAxis()
    {
        AxisType = ChartAxisType.Numeric
    } );
}

private bool CanExecuteAddYAxisCommand( ChartArea _param1 )
{
    return this.AllowAddAxis && this.GetRealChartArea( _param1 ) != null;
}

private bool CanExecuteRemoveAxisCommand( ChartAxis _param1 )
{
    return this.IsInteracted && _param1?.ChartArea != null && !CompareHelper.IsDefault<ChartAxis>( _param1 ) && this.AllowAddAxis;
}

private bool CanExecuteClosePaneCommand(
  IScichartSurfaceVM _param1 )
{
    return this.AllowAddArea;
}

private void ExecuteCancelActiveOrders( ChartArea _param1 )
{
    this.\u0023\u003Dz\u0024DK5seweHzSZIyjEhw\u003D\u003D( ( Func<Order, bool> ) null );
}

private bool CanExecuteCancelActiveOrders( ChartArea _param1 )
{
    return this.IsInteracted;
}

private void \u0023\u003Dz_JOkHnKu927R4qx\u0024N\u0024sTKxE\u003D(
    DependencyObject _param1,
    ThemeChangedRoutedEventArgs _param2)
  {
    this.ChangeApplicationTheme();
  }

  [Serializable]
private sealed class SomeClass34343383
{
    public static readonly ChartViewModel.SomeClass34343383 SomeMethond0343 = new ChartViewModel.SomeClass34343383();
    public static Action<ScichartSurfaceMVVM> \u0023\u003Dz4K9Ew\u00245ncgrgb99V4w\u003D\u003D;
    public static Action<ChartAxis> ExecuteRemoveAxisCommand;
    public static Action<IScichartSurfaceVM> ExecuteClosePaneCommand;
    public static Func<Order, bool> \u0023\u003DzwCieN8nlFS3aCeRPgg\u003D\u003D;
    public static Func<Order, bool> \u0023\u003DzU9srAoETJDIIA3EbGw\u003D\u003D;
    public static Func<ScichartSurfaceMVVM,
#nullable enable
    IEnumerable<
#nullable disable
    Order>> \u0023\u003DzKeRpc1y1\u0024C\u0024IWjYsyQ\u003D\u003D;

    public void \u0023\u003Dz0sbMZcoPgn7DQ3i\u0024I2emR\u00244\u003D(
      ScichartSurfaceMVVM _param1)
    {
      _param1.Area.ViewModel.ShowHiddenAxesCommand.TryExecute((object) null);
    }

public void ExecuteRemoveAxisCommand2( ChartAxis _param1 )
{
    IChartArea chartArea = _param1.ChartArea;
    if ( ( ( ICollection<IChartAxis> ) chartArea.XAxises ).Contains( ( IChartAxis ) _param1 ) )
        ( ( ICollection<IChartAxis> ) chartArea.XAxises ).Remove( ( IChartAxis ) _param1 );
    if ( !( ( ICollection<IChartAxis> ) chartArea.YAxises ).Contains( ( IChartAxis ) _param1 ) )
        return;
    ( ( ICollection<IChartAxis> ) chartArea.YAxises ).Remove( ( IChartAxis ) _param1 );
}

public void ExecuteClosePaneCommand2(
  IScichartSurfaceVM _param1 )
{
    ChartViewModel.\u0023\u003DzNCeF9LrrNtIjst\u0024ouVg4RRo\u003D lrrNtIjstOuVg4Rro = new ChartViewModel.\u0023\u003DzNCeF9LrrNtIjst\u0024ouVg4RRo\u003D();
      lrrNtIjstOuVg4Rro.\u0023\u003DzsWV8_ck\u003D = _param1;
      IChart chart = ((ScichartSurfaceMVVM) lrrNtIjstOuVg4Rro.\u0023\u003DzsWV8_ck\u003D).Chart;
      IChartArea area = chart.Areas.FirstOrDefault<IChartArea>(new Func<IChartArea, bool>(lrrNtIjstOuVg4Rro.\u0023\u003DzHDJpZroCOKM644oB\u0024A\u003D\u003D));
      if (area == null)
        return;
      chart.RemoveArea(area);
    }

    public bool \u0023\u003DzO_BIxNwmDn6VDgqOIx_JE6RwPKwP(Order _param1) => true;

    public 
    #nullable enable
    IEnumerable<
    #nullable disable
    Order> \u0023\u003DzrhkYnMPbNPr3HVPmAT1zSUTwkFvh(
      ScichartSurfaceMVVM _param1)
    {
      return _param1.GetActiveOrders(ChartViewModel.SomeClass34343383.\u0023\u003DzU9srAoETJDIIA3EbGw\u003D\u003D ?? (ChartViewModel.SomeClass34343383.\u0023\u003DzU9srAoETJDIIA3EbGw\u003D\u003D = new Func<Order, bool>(ChartViewModel.SomeClass34343383.SomeMethond0343.\u0023\u003Dz6ysbq7QSBMgTXxXd1pDg18rT_80t)));
    }

    public bool \u0023\u003Dz6ysbq7QSBMgTXxXd1pDg18rT_80t(Order _param1)
    {
      return _param1.State == 1 || _param1.State == 4;
    }

    public void OnMinimumRangeCallback(
      DependencyObject _param1,
      DependencyPropertyChangedEventArgs _param2)
    {
      ((ChartViewModel) _param1)._minimumRange = (int) _param2.NewValue;
    }
  }

  private sealed class \u0023\u003DzNCeF9LrrNtIjst\u0024ouVg4RRo\u003D
  {
    public IScichartSurfaceVM \u0023\u003DzsWV8_ck\u003D;

    public bool \u0023\u003DzHDJpZroCOKM644oB\u0024A\u003D\u003D(IChartArea _param1)
    {
      return ((ChartArea) _param1).ViewModel == this.\u0023\u003DzsWV8_ck\u003D;
    }
  }

  private sealed class \u0023\u003DzymgCCsreH3nwJBFwtEPKXQw\u003D
  {
    public Action<Order> \u0023\u003DzXCEqv64\u003D;

    public void \u0023\u003Dz69cIxDaENs3AtcYRgfOovNI\u003D(Order _param1)
    {
      Action<Order> zXcEqv64 = this.\u0023\u003DzXCEqv64\u003D;
      if (zXcEqv64 == null)
        return;
      zXcEqv64(_param1);
    }
  }
}
