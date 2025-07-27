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


public sealed class ChartViewModel : DependencyObject
{
    #region Tony Added

    public event EventHandler<AddCandlesEventArgs>  CodingAddCandlesEvent;

    
    public void ExecuteCodingAddCandles(ChartArea chartArea, CandleSeries series)
    {
        CodingAddCandlesEvent?.Invoke(this, new AddCandlesEventArgs(chartArea, series));
    }

    public bool CanExecuteCodingAddCandles()
    {
        return true;
    }
    #endregion Tony Added

    public string SelectedTheme
    {
        get
        {
            return (string)this.GetValue(ChartViewModel.SelectedThemeProperty);
        }
        set
        {
            this.SetValue(ChartViewModel.SelectedThemeProperty, (object)value);
        }
    }

    public bool ShowOverview
    {
        get
        {
            return (bool)this.GetValue(ChartViewModel.ShowOverviewProperty);
        }
        set
        {
            this.SetValue(ChartViewModel.ShowOverviewProperty, (object)value);
        }
    }

    public bool ShowLegend
    {
        get
        {
            return (bool)this.GetValue(ChartViewModel.ShowLegendProperty);
        }
        set
        {
            this.SetValue(ChartViewModel.ShowLegendProperty, (object)value);
        }
    }

    public bool IsInteracted
    {
        get
        {
            return (bool)this.GetValue(ChartViewModel.IsInteractedProperty);
        }
        set
        {
            this.SetValue(ChartViewModel.IsInteractedProperty, (object)value);
        }
    }

    public bool AllowAddArea
    {
        get
        {
            return (bool)this.GetValue(ChartViewModel.AllowAddAreaProperty);
        }
        set
        {
            this.SetValue(ChartViewModel.AllowAddAreaProperty, (object)value);
        }
    }

    public bool AllowAddAxis
    {
        get
        {
            return (bool)this.GetValue(ChartViewModel.AllowAddAxisProperty);
        }
        set
        {
            this.SetValue(ChartViewModel.AllowAddAxisProperty, (object)value);
        }
    }

    public bool AllowAddCandles
    {
        get
        {
            return (bool)this.GetValue(ChartViewModel.AllowAddCandlesProperty);
        }
        set
        {
            this.SetValue(ChartViewModel.AllowAddCandlesProperty, (object)value);
        }
    }

    public bool AllowAddIndicators
    {
        get
        {
            return (bool)this.GetValue(ChartViewModel.AllowAddIndicatorsProperty);
        }
        set
        {
            this.SetValue(ChartViewModel.AllowAddIndicatorsProperty, (object)value);
        }
    }

    public bool AllowAddOrders
    {
        get
        {
            return (bool)this.GetValue(ChartViewModel.AllowAddOrdersProperty);
        }
        set
        {
            this.SetValue(ChartViewModel.AllowAddOrdersProperty, (object)value);
        }
    }

    public bool AllowAddOwnTrades
    {
        get
        {
            return (bool)this.GetValue(ChartViewModel.AllowAddOwnTradesProperty);
        }
        set
        {
            this.SetValue(ChartViewModel.AllowAddOwnTradesProperty, (object)value);
        }
    }

    private static void OnAllowAddXPropertyChanged(DependencyObject _param0, DependencyPropertyChangedEventArgs _param1)
    {
        ( (ChartViewModel)_param0 ).AllowAddXPropertyChanged();
    }

    private void AllowAddXPropertyChanged()
    {
        CommandManager.InvalidateRequerySuggested();
        this.ClosePaneCommand.RaiseCanExecuteChanged();
        Action izKc6jG0gptPuO01M = ChartViewModel.InteractedEvent;
        if ( izKc6jG0gptPuO01M == null )
            return;
        izKc6jG0gptPuO01M();
    }

    public ICommand AddXAxisCommand
    {
        get
        {
            return (ICommand)this.GetValue(ChartViewModel.AddXAxisCommandProperty);
        }
        set
        {
            this.SetValue(ChartViewModel.AddXAxisCommandProperty, (object)value);
        }
    }

    public ICommand AddYAxisCommand
    {
        get
        {
            return (ICommand)this.GetValue(ChartViewModel.AddYAxisCommandProperty);
        }
        set
        {
            this.SetValue(ChartViewModel.AddYAxisCommandProperty, (object)value);
        }
    }

    public ICommand RemoveAxisCommand
    {
        get
        {
            return (ICommand)this.GetValue(ChartViewModel.RemoveAxisCommandProperty);
        }
        set
        {
            this.SetValue(ChartViewModel.RemoveAxisCommandProperty, (object)value);
        }
    }

    public ObservableCollection<IndicatorType> IndicatorTypes
    {
        get
        {
            return (ObservableCollection<IndicatorType>)this.GetValue(ChartViewModel.IndicatorTypesProperty);
        }
        set
        {
            this.SetValue(ChartViewModel.IndicatorTypesProperty, (object)value);
        }
    }

    public DelegateCommand<IDrawingSurfaceVM> ClosePaneCommand
    {
        get => this._closePaneCommand;
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

    public static readonly DependencyProperty SelectedThemeProperty = DependencyProperty.Register(nameof(SelectedTheme), typeof(string), typeof(ChartViewModel));

    public static readonly DependencyProperty ShowOverviewProperty = DependencyProperty.Register(nameof(ShowOverview), typeof(bool), typeof(ChartViewModel), new PropertyMetadata((object)false));

    public static readonly DependencyProperty ShowLegendProperty = DependencyProperty.Register(nameof(ShowLegend), typeof(bool), typeof(ChartViewModel));

    public static Action InteractedEvent;

    public static readonly DependencyProperty IsInteractedProperty = DependencyProperty.Register(nameof(IsInteracted), typeof(bool), typeof(ChartViewModel), new PropertyMetadata((object)true, new PropertyChangedCallback(ChartViewModel.OnIsInteractedCallback)));

    private static void OnIsInteractedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ChartViewModel vm = (ChartViewModel)d;
        vm.CoerceValue(ChartViewModel.AllowAddAreaProperty);
        vm.CoerceValue(ChartViewModel.AllowAddAxisProperty);
        vm.CoerceValue(ChartViewModel.AllowAddCandlesProperty);
        vm.CoerceValue(ChartViewModel.AllowAddIndicatorsProperty);
        vm.CoerceValue(ChartViewModel.AllowAddOrdersProperty);
        vm.CoerceValue(ChartViewModel.AllowAddOwnTradesProperty);
        ( (ChartViewModel)d ).AllowAddXPropertyChanged();
    }

    private readonly ICommand CancelActiveOrdersCommand;

    private int _minimumRange;

    public static readonly DependencyProperty ChartPaneViewModelProperty = DependencyProperty.Register(nameof(ChartPaneViewModels), typeof(ObservableCollection<ScichartSurfaceMVVM>), typeof(ChartViewModel));



    public static readonly DependencyProperty ShowHiddenAxesCommandProperty = DependencyProperty.Register(nameof(ShowHiddenAxesCommand), typeof(ICommand), typeof(ChartViewModel));

    public static readonly DependencyProperty AddAreaCommandProperty = DependencyProperty.Register(nameof(AddAreaCommand), typeof(ICommand), typeof(ChartViewModel));

    public static readonly DependencyProperty AddCandlesCommandProperty = DependencyProperty.Register(nameof(AddCandlesCommand), typeof(ICommand), typeof(ChartViewModel));

    public static readonly DependencyProperty AddIndicatorCommandProperty = DependencyProperty.Register(nameof(AddIndicatorCommand), typeof(ICommand), typeof(ChartViewModel));

    public static readonly DependencyProperty AddOrdersCommandProperty = DependencyProperty.Register(nameof(AddOrdersCommand), typeof(ICommand), typeof(ChartViewModel));

    public static readonly DependencyProperty AddTradesCommandProperty = DependencyProperty.Register(nameof(AddTradesCommand), typeof(ICommand), typeof(ChartViewModel));

    public static readonly DependencyProperty UngroupCommandProperty = DependencyProperty.Register(nameof(UngroupCommand), typeof(ICommand), typeof(ChartViewModel));

    public static readonly DependencyProperty AddXAxisCommandProperty = DependencyProperty.Register(nameof(AddXAxisCommand), typeof(ICommand), typeof(ChartViewModel));

    public static readonly DependencyProperty AddYAxisCommandProperty = DependencyProperty.Register(nameof(AddYAxisCommand), typeof(ICommand), typeof(ChartViewModel));

    public static readonly DependencyProperty RemoveAxisCommandProperty = DependencyProperty.Register(nameof(RemoveAxisCommand), typeof(ICommand), typeof(ChartViewModel));

    public static readonly DependencyProperty IndicatorTypesProperty = DependencyProperty.Register(nameof(IndicatorTypes), typeof(ObservableCollection<IndicatorType>), typeof(ChartViewModel));

    public static readonly DependencyProperty AllowAddAreaProperty = DependencyProperty.Register(nameof(AllowAddArea), typeof(bool), typeof(ChartViewModel), new PropertyMetadata((object)true, new PropertyChangedCallback(ChartViewModel.OnAllowAddXPropertyChanged), new CoerceValueCallback(ChartViewModel.OnAllowAddXPropertyCoreceValueChanged)));

    public static readonly DependencyProperty AllowAddAxisProperty = DependencyProperty.Register(nameof(AllowAddAxis), typeof(bool), typeof(ChartViewModel), new PropertyMetadata((object)true, new PropertyChangedCallback(ChartViewModel.OnAllowAddXPropertyChanged), new CoerceValueCallback(ChartViewModel.OnAllowAddXPropertyCoreceValueChanged)));

    public static readonly DependencyProperty AllowAddCandlesProperty = DependencyProperty.Register(nameof(AllowAddCandles), typeof(bool), typeof(ChartViewModel), new PropertyMetadata((object)true, new PropertyChangedCallback(ChartViewModel.OnAllowAddXPropertyChanged), new CoerceValueCallback(ChartViewModel.OnAllowAddXPropertyCoreceValueChanged)));

    public static readonly DependencyProperty AllowAddIndicatorsProperty = DependencyProperty.Register(nameof(AllowAddIndicators), typeof(bool), typeof(ChartViewModel), new PropertyMetadata((object)true, new PropertyChangedCallback(ChartViewModel.OnAllowAddXPropertyChanged), new CoerceValueCallback(ChartViewModel.OnAllowAddXPropertyCoreceValueChanged)));

    public static readonly DependencyProperty AllowAddOrdersProperty = DependencyProperty.Register(nameof(AllowAddOrders), typeof(bool), typeof(ChartViewModel), new PropertyMetadata((object)true, new PropertyChangedCallback(ChartViewModel.OnAllowAddXPropertyChanged), new CoerceValueCallback(ChartViewModel.OnAllowAddXPropertyCoreceValueChanged)));

    public static readonly DependencyProperty AllowAddOwnTradesProperty = DependencyProperty.Register(nameof(AllowAddOwnTrades), typeof(bool), typeof(ChartViewModel), new PropertyMetadata((object)true, new PropertyChangedCallback(ChartViewModel.OnAllowAddXPropertyChanged), new CoerceValueCallback(ChartViewModel.OnAllowAddXPropertyCoreceValueChanged)));

    public static readonly DependencyProperty MinimumRangeProperty = DependencyProperty.Register(nameof(MinimumRange), typeof(int), typeof(ChartViewModel), new PropertyMetadata(new PropertyChangedCallback(ChartViewModel.SomeClass34343383.SomeMethond0343.OnMinimumRangeCallback)));


    private readonly DelegateCommand<IDrawingSurfaceVM> _closePaneCommand;

    public ChartViewModel()
    {
        this.ChartPaneViewModels = new ObservableCollection<ScichartSurfaceMVVM>();
        this.MinimumRange = 50;
        this.ShowOverview = false;
        this.ShowLegend = true;
        this.IndicatorTypes = new ObservableCollection<IndicatorType>();
        this.AddAreaCommand = (ICommand)new DelegateCommand(new Action<object>(this.ExecuteAddAreaCommand), new Func<object, bool>(this.CanExecuteAddAreaCommand));
        this.AddCandlesCommand = (ICommand)new DelegateCommand<ChartArea>(new Action<ChartArea>(this.ExecuteAddCandlesCommand), new Func<ChartArea, bool>(this.CanExecuteAddAreaCommand));
        this.AddIndicatorCommand = (ICommand)new DelegateCommand<ChartArea>(new Action<ChartArea>(this.ExecuteAddIndicatorCommand), new Func<ChartArea, bool>(this.CanExecuteAddIndicatorCommand));
        this.AddOrdersCommand = (ICommand)new DelegateCommand<ChartArea>(new Action<ChartArea>(this.ExecuteAddOrdersCommand), new Func<ChartArea, bool>(this.CanExecuteAddOrdersCommand));
        this.AddTradesCommand = (ICommand)new DelegateCommand<ChartArea>(new Action<ChartArea>(this.ExecuteAddTradesCommand), new Func<ChartArea, bool>(this.CanExecuteAddTradesCommand));
        this.UngroupCommand = (ICommand)new DelegateCommand<ChartArea>(new Action<ChartArea>(this.ExecuteUngroupCommand), new Func<ChartArea, bool>(this.CanExecuteUngroupCommand));
        this.ShowHiddenAxesCommand = (ICommand)new DelegateCommand<ChartArea>(new Action<ChartArea>(this.ExecuteShowHiddenAxesCommand), new Func<ChartArea, bool>(this.CanExecuteShowHiddenAxes));
        this.AddXAxisCommand = (ICommand)new DelegateCommand<ChartArea>(new Action<ChartArea>(this.ExecuteAddXAxisCommand), new Func<ChartArea, bool>(this.CanExecuteAddXAxisCommand));
        this.AddYAxisCommand = (ICommand)new DelegateCommand<ChartArea>(new Action<ChartArea>(this.ExecuteAddYAxisCommand), new Func<ChartArea, bool>(this.CanExecuteAddYAxisCommand));
        this.RemoveAxisCommand = (ICommand)new DelegateCommand<ChartAxis>(ChartViewModel.SomeClass34343383.ExecuteRemoveAxisCommand ?? ( ChartViewModel.SomeClass34343383.ExecuteRemoveAxisCommand = new Action<ChartAxis>(ChartViewModel.SomeClass34343383.SomeMethond0343.ExecuteRemoveAxisCommand2) ), new Func<ChartAxis, bool>(this.CanExecuteRemoveAxisCommand));
        this.InitRangeDepProperty();
        this._closePaneCommand = new DelegateCommand<IDrawingSurfaceVM>(ChartViewModel.SomeClass34343383.ExecuteClosePaneCommand ?? ( ChartViewModel.SomeClass34343383.ExecuteClosePaneCommand = new Action<IDrawingSurfaceVM>(ChartViewModel.SomeClass34343383.SomeMethond0343.ExecuteClosePaneCommand2) ), new Func<IDrawingSurfaceVM, bool>(this.CanExecuteClosePaneCommand));
        this.CancelActiveOrdersCommand = (ICommand)new DelegateCommand<ChartArea>(new Action<ChartArea>(this.ExecuteCancelActiveOrders), new Func<ChartArea, bool>(this.CanExecuteCancelActiveOrders));
        if ( this.IsDesignMode() )
            return;
        this.ChangeApplicationTheme();
        DevExpress.Xpf.Core.ThemeManager.ApplicationThemeChanged += new ThemeChangedRoutedEventHandler(this.OnApplicationThemeChanged);
    }



    public void InternalExecuteCancelActiveOrders(Func<Order, bool> _param1)
    {
        ChartViewModel.SomeInternalSealedClassXQw ccsreH3nwJbFwtEpkxQw = new ChartViewModel.SomeInternalSealedClassXQw();
        ccsreH3nwJbFwtEpkxQw._order_action = this.CancelActiveOrderEvent;
        if ( _param1 == null )
            _param1 = ChartViewModel.SomeClass34343383._fuction_order_bool_093 ?? ( ChartViewModel.SomeClass34343383._fuction_order_bool_093 = new Func<Order, bool>(ChartViewModel.SomeClass34343383.SomeMethond0343.CanCancelActiveOrders) );
        CollectionHelper.ForEach<Order>((IEnumerable<Order>)CollectionHelper.ToSet<Order>(this.ChartPaneViewModels.SelectMany<ScichartSurfaceMVVM, Order>(ChartViewModel.SomeClass34343383._fuction_order_bool_333 ?? ( ChartViewModel.SomeClass34343383._fuction_order_bool_333 = new Func<ScichartSurfaceMVVM, IEnumerable<Order>>(ChartViewModel.SomeClass34343383.SomeMethond0343.selectActiverOrders) )).Where<Order>(_param1)), new Action<Order>(ccsreH3nwJbFwtEpkxQw.Method034));
    }

    private void ChangeApplicationTheme() => this.SelectedTheme = ChartHelper2025.CurrChartTheme();









    private static object OnAllowAddXPropertyCoreceValueChanged(DependencyObject d, object e)
    {
        bool coerceValue = (bool)e;
        var vm = ( (ChartViewModel)d );

        bool result = !vm.IsInteracted ? false : coerceValue;

        return result;
    }

    public int MinimumRange
    {
        get => this._minimumRange;
        set
        {
            this.SetValue(ChartViewModel.MinimumRangeProperty, (object)value);
        }
    }

    public ObservableCollection<ScichartSurfaceMVVM> ChartPaneViewModels
    {
        get
        {
            return (ObservableCollection<ScichartSurfaceMVVM>)this.GetValue(ChartViewModel.ChartPaneViewModelProperty);
        }
        set
        {
            this.SetValue(ChartViewModel.ChartPaneViewModelProperty, (object)value);
        }
    }

    public ICommand GetCancelActiveOrdersCommand()
    {
        return this.CancelActiveOrdersCommand;
    }

    public ICommand ShowHiddenAxesCommand
    {
        get
        {
            return (ICommand)this.GetValue(ChartViewModel.ShowHiddenAxesCommandProperty);
        }
        set
        {
            this.SetValue(ChartViewModel.ShowHiddenAxesCommandProperty, (object)value);
        }
    }

    public ICommand AddAreaCommand
    {
        get
        {
            return (ICommand)this.GetValue(ChartViewModel.AddAreaCommandProperty);
        }
        set
        {
            this.SetValue(ChartViewModel.AddAreaCommandProperty, (object)value);
        }
    }

    public ICommand AddCandlesCommand
    {
        get
        {
            return (ICommand)this.GetValue(ChartViewModel.AddCandlesCommandProperty);
        }
        set
        {
            this.SetValue(ChartViewModel.AddCandlesCommandProperty, (object)value);
        }
    }

    public ICommand AddIndicatorCommand
    {
        get
        {
            return (ICommand)this.GetValue(ChartViewModel.AddIndicatorCommandProperty);
        }
        set
        {
            this.SetValue(ChartViewModel.AddIndicatorCommandProperty, (object)value);
        }
    }

    public ICommand AddOrdersCommand
    {
        get
        {
            return (ICommand)this.GetValue(ChartViewModel.AddOrdersCommandProperty);
        }
        set
        {
            this.SetValue(ChartViewModel.AddOrdersCommandProperty, (object)value);
        }
    }

    public ICommand AddTradesCommand
    {
        get
        {
            return (ICommand)this.GetValue(ChartViewModel.AddTradesCommandProperty);
        }
        set
        {
            this.SetValue(ChartViewModel.AddTradesCommandProperty, (object)value);
        }
    }

    public ICommand UngroupCommand
    {
        get
        {
            return (ICommand)this.GetValue(ChartViewModel.UngroupCommandProperty);
        }
        set
        {
            this.SetValue(ChartViewModel.UngroupCommandProperty, (object)value);
        }
    }

    public void InvokeRebuildCandlesEvent(IChartElement _param1, Subscription _param2)
    {
        Action<IChartElement, Subscription> myEvent = this.RebuildCandlesEvent;
        if ( myEvent == null )
            return;
        myEvent(_param1, _param2);
    }



    public void InitRangeDepProperty()
    {
        VisibleRangeDpo.InitRangeDepProperty((object)this);
    }

    public void InvokeRemoveElementEvent(IChartElement _param1)
    {
        Action<IChartElement> zeBeQvx4 = this.RemoveElementEvent;
        if ( zeBeQvx4 == null )
            return;
        zeBeQvx4(_param1);
    }

    private ChartArea GetRealChartArea(ChartArea _param1)
    {
        ChartArea chartArea = _param1;
        if ( chartArea != null )
            return chartArea;
        ObservableCollection<ScichartSurfaceMVVM> chartPaneViewModels = this.ChartPaneViewModels;
        if ( chartPaneViewModels == null )
            return (ChartArea)null;
        return chartPaneViewModels.FirstOrDefault<ScichartSurfaceMVVM>()?.Area;
    }

    private void ExecuteAddAreaCommand(object _param1)
    {
        Action zJ36T8eJdWvlL = this.AreaAddingEvent;
        if ( zJ36T8eJdWvlL == null )
            return;
        zJ36T8eJdWvlL();
    }

    private bool CanExecuteAddAreaCommand(object _param1) => this.AllowAddArea;

    private void ExecuteAddCandlesCommand(ChartArea _param1)
    {
        AddCandlesEvent?.Invoke(this.GetRealChartArea(_param1));
    }

    private bool CanExecuteAddAreaCommand(ChartArea _param1)
    {
        return this.AllowAddCandles && this.GetRealChartArea(_param1) != null;
    }

    private void ExecuteAddIndicatorCommand(ChartArea _param1)
    {

        AddIndicatorEvent?.Invoke(this.GetRealChartArea(_param1));
    }

    private bool CanExecuteAddIndicatorCommand(ChartArea _param1)
    {
        return this.AllowAddIndicators && this.GetRealChartArea(_param1) != null;
    }

    private void ExecuteAddOrdersCommand(ChartArea _param1)
    {
        AddOrdersEvent?.Invoke(this.GetRealChartArea(_param1));
    }

    private bool CanExecuteAddOrdersCommand(ChartArea _param1)
    {
        return this.AllowAddOrders && this.GetRealChartArea(_param1) != null;
    }

    private void ExecuteAddTradesCommand(ChartArea _param1)
    {

        AddTradesEvent?.Invoke(this.GetRealChartArea(_param1));
    }

    private bool CanExecuteAddTradesCommand(ChartArea _param1)
    {
        return this.AllowAddOwnTrades && this.GetRealChartArea(_param1) != null;
    }

    private void ExecuteUngroupCommand(ChartArea _param1)
    {

        UngroupEvent?.Invoke(this.GetRealChartArea(_param1));
    }

    private bool CanExecuteUngroupCommand(ChartArea _param1)
    {
        return this.GetRealChartArea(_param1) != null;
    }

    private void ExecuteShowHiddenAxesCommand(ChartArea area)
    {
        if ( area != null )
        {
            area.ViewModel.ShowHiddenAxesCommand.Execute(null);
        }
        else
            CollectionHelper.ForEach<ScichartSurfaceMVVM>((IEnumerable<ScichartSurfaceMVVM>)this.ChartPaneViewModels, p => p.Area.ViewModel.ShowHiddenAxesCommand.Execute(null));
    }

    private bool CanExecuteShowHiddenAxes(ChartArea _param1) => this.IsInteracted;

    private void ExecuteAddXAxisCommand(ChartArea a)
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

    private bool CanExecuteAddXAxisCommand(ChartArea _param1)
    {
        return this.AllowAddAxis;
    }

    private void ExecuteAddYAxisCommand(ChartArea a)
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

    private bool CanExecuteAddYAxisCommand(ChartArea _param1)
    {
        return this.AllowAddAxis && this.GetRealChartArea(_param1) != null;
    }

    private bool CanExecuteRemoveAxisCommand(ChartAxis _param1)
    {
        return this.IsInteracted && _param1?.ChartArea != null && !CompareHelper.IsDefault<ChartAxis>(_param1) && this.AllowAddAxis;
    }

    private bool CanExecuteClosePaneCommand(
      IDrawingSurfaceVM _param1)
    {
        return this.AllowAddArea;
    }

    private void ExecuteCancelActiveOrders(ChartArea _param1)
    {
        this.InternalExecuteCancelActiveOrders((Func<Order, bool>)null);
    }

    private bool CanExecuteCancelActiveOrders(ChartArea _param1)
    {
        return this.IsInteracted;
    }

    private void OnApplicationThemeChanged(
        DependencyObject _param1,
        ThemeChangedRoutedEventArgs _param2)
    {
        this.ChangeApplicationTheme();
    }

    [Serializable]
    private sealed class SomeClass34343383
    {
        public static readonly ChartViewModel.SomeClass34343383 SomeMethond0343 = new ChartViewModel.SomeClass34343383();
        public static Action<ScichartSurfaceMVVM> _action_scichartsufaceMvvm_093;
        public static Action<ChartAxis> ExecuteRemoveAxisCommand;
        public static Action<IDrawingSurfaceVM> ExecuteClosePaneCommand;
        public static Func<Order, bool> _fuction_order_bool_093;
        public static Func<Order, bool> _fuction_order_bool_087;
        public static Func<ScichartSurfaceMVVM,
#nullable enable
        IEnumerable<
#nullable disable
        Order>> _fuction_order_bool_333;

        public void Method083(
          ScichartSurfaceMVVM _param1)
        {
            _param1.Area.ViewModel.ShowHiddenAxesCommand.TryExecute((object)null);
        }

        public void ExecuteRemoveAxisCommand2(ChartAxis _param1)
        {
            IChartArea chartArea = _param1.ChartArea;
            if ( ( (ICollection<IChartAxis>)chartArea.XAxises ).Contains((IChartAxis)_param1) )
                ( (ICollection<IChartAxis>)chartArea.XAxises ).Remove((IChartAxis)_param1);
            if ( !( (ICollection<IChartAxis>)chartArea.YAxises ).Contains((IChartAxis)_param1) )
                return;
            ( (ICollection<IChartAxis>)chartArea.YAxises ).Remove((IChartAxis)_param1);
        }

        public void ExecuteClosePaneCommand2(
          IDrawingSurfaceVM _param1)
        {
            ChartViewModel.SomeInternalSealedClass4RRo lrrNtIjstOuVg4Rro = new ChartViewModel.SomeInternalSealedClass4RRo();
            lrrNtIjstOuVg4Rro._IDrawingSurfaceVM = _param1;
            IChart chart = ( (ScichartSurfaceMVVM)lrrNtIjstOuVg4Rro._IDrawingSurfaceVM ).Chart;
            IChartArea area = chart.Areas.FirstOrDefault<IChartArea>(new Func<IChartArea, bool>(lrrNtIjstOuVg4Rro.Method01));
            if ( area == null )
                return;
            chart.RemoveArea(area);
        }

        public bool CanCancelActiveOrders(Order o) => true;

        public IEnumerable<Order> selectActiverOrders(
          ScichartSurfaceMVVM _param1)
        {
            return _param1.GetActiveOrders(o => o.State == OrderStates.Active || o.State == OrderStates.Pending);
        }


        public void OnMinimumRangeCallback(
          DependencyObject _param1,
          DependencyPropertyChangedEventArgs _param2)
        {
            ( (ChartViewModel)_param1 )._minimumRange = (int)_param2.NewValue;
        }
    }

    private sealed class SomeInternalSealedClass4RRo
    {
        public IDrawingSurfaceVM _IDrawingSurfaceVM;

        public bool Method01(IChartArea _param1)
        {
            return ( (ChartArea)_param1 ).ViewModel == this._IDrawingSurfaceVM;
        }
    }

    private sealed class SomeInternalSealedClassXQw
    {
        public Action<Order> _order_action;

        public void Method034(Order _param1)
        {
            Action<Order> zXcEqv64 = this._order_action;
            if ( zXcEqv64 == null )
                return;
            zXcEqv64(_param1);
        }
    }
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

//        if ( this.IsDesignMode( ) )
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
//        Action<IChartElement> myRemoveAction = this.RemoveElementEvent;
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
