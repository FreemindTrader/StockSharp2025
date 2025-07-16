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


/// <summary>
/// 
/// </summary>
public sealed class ChartCompentViewModel : ChartBaseViewModel, IDisposable
{

    private readonly 
  #nullable disable
  ObservableCollection<ChartElementViewModel> _childViewModels = new ObservableCollection<ChartElementViewModel>();

    private readonly List<DrawableChartElementBaseViewModel> _childElements = new List<DrawableChartElementBaseViewModel>();

    private readonly ScichartSurfaceMVVM _scichartSurfaceVM;

    private readonly IChartComponent _chartElement;

    private readonly bool _isCandleElement;

    private CandlestickUI _candleStickUI;

    private bool _isDisposed;

    public ChartCompentViewModel( ScichartSurfaceMVVM _param1, IChartComponent _param2 )
    {
        this._scichartSurfaceVM = _param1 ?? throw new ArgumentNullException( "pane" );
        this._chartElement = _param2 ?? throw new ArgumentNullException( "element" );
        this._isCandleElement = _param2 is IChartCandleElement;
        ChartViewModel.AllowToRemoveEvent += OnAllowToRemove;
        this.ChartComponent.PropertyChanged += OnPropertyChanged;
    }

    public ScichartSurfaceMVVM Pane
    {
        get => this._scichartSurfaceVM;
    }

    public bool AllowToRemove => this.Pane.AllowElementToBeRemoved( this );

    public IChartComponent ChartComponent
    {
        get => this._chartElement;
    }

    public string Title => this.ChartComponent.GetGeneratedTitle();

    public bool IsCandleElement => this._isCandleElement;

    public CandlestickUI Candles
    {
        get => this._candleStickUI;
        private set => this._candleStickUI = value;
    }

    public IEnumerable<DrawableChartElementBaseViewModel> Elements
    {
        get
        {
            return ( IEnumerable<DrawableChartElementBaseViewModel> ) this._childElements;
        }
    }

    public IEnumerable<ChartElementViewModel> Values
    {
        get
        {
            return ( IEnumerable<ChartElementViewModel> ) this._childViewModels;
        }
    }

    public Color Color
    {
        get
        {
            return this._childElements.Count == 1 ? this._childElements[ 0 ].Element.Color : Colors.Transparent;
        }
    }

    public void InitializeChildElements( IEnumerable<DrawableChartElementBaseViewModel> vms )
    {
        DrawableChartElementBaseViewModel[] childElementArray = vms.ToArray<DrawableChartElementBaseViewModel>();
        if ( CollectionHelper.IsEmpty<DrawableChartElementBaseViewModel>( childElementArray ) )
            throw new ArgumentException( "zero child elements" );

        this._childElements.AddRange( ( IEnumerable<DrawableChartElementBaseViewModel> ) childElementArray );
        if ( this.IsCandleElement && this.Candles == null )
            this.Candles = childElementArray.OfType<CandlestickUI>().First<CandlestickUI>();
        if ( this._childElements.Count == 1 )
            this.MapPropertyChangeNotification( this._childElements[ 0 ].Element, "Color", "Color" );

        CollectionHelper.ForEach<DrawableChartElementBaseViewModel>( ( IEnumerable<DrawableChartElementBaseViewModel> ) childElementArray, new Action<DrawableChartElementBaseViewModel>( this.OnSomethingMethod0934 ) );
    }

    private void OnAllowToRemove()
    {
        this.NotifyChanged( "AllowToRemove" );
    }

    public void AddChild( ChartElementViewModel childViewModel )
    {
        if ( childViewModel == null )
            throw new ArgumentNullException( "val" );
        if ( this._childViewModels.Contains( childViewModel ) )
            throw new ArgumentException( "val" );
        childViewModel.Parent = this;
        this._childViewModels.Add( childViewModel );
    }

    public void ClearChildViewModels()
    {
        foreach ( ChartElementViewModel childViewModel in _childViewModels )
        {
            childViewModel.Value = null;
        }
    }

    public bool Draw( ChartDrawData _param1 )
    {
        return this.ChartComponent.Draw( _param1 );
    }

    public void Reset()
    {
        this.ChartComponent.Reset();

        foreach ( var child in _childElements )
        {
            child.Reset();
        }
    }

    public void UpdateYAxisMarker()
    {
        foreach ( var child in _childElements )
        {
            child.UpdateYAxisMarker();
        }
    }

    public void PerformPeriodicalAction()
    {
        foreach ( var child in _childElements )
        {
            child.PerformPeriodicalAction();
        }
    }

    public void GuiUpdateAndClear()
    {
        foreach ( DrawableChartElementBaseViewModel child in _childElements )
        {
            child.GuiUpdateAndClear();
        }
    }

    public Subscription GetSubscription(
      IChartComponent _param1 )
    {
        return ( ( Chart ) this.Pane.Chart ).TryGetSubscription( ( IChartElement ) _param1 );
    }

    public bool IsDisposed
    {
        get => this._isDisposed;
        private set => this._isDisposed = value;
    }

    public void Dispose()
    {
        // BUG: I believe there is a problem here. Doesn't look like OnAllToRemove should be binded to InteractedEvent
        //      Should probably be linked to AllowToRemoveEvent
        ChartViewModel.InteractedEvent += ( new Action( this.OnAllowToRemove ) );
        this.IsDisposed = true;
    }

    private void OnPropertyChanged(
#nullable enable
      object? _param1,
      PropertyChangedEventArgs _param2 )
    {
        if ( !( _param2.PropertyName == "FullTitle" ) )
            return;
        this.NotifyChanged( "Title" );
    }

    private void OnSomethingMethod0934(
#nullable disable
      DrawableChartElementBaseViewModel _param1 )
    {
        _param1.Init( this );
    }

}
