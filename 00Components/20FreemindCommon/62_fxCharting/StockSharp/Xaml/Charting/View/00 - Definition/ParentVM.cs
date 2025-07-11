//using Ecng.Collections;
//using StockSharp.Xaml;
//using StockSharp.Xaml.Charting;
//using System;
//using System.Collections.Generic; using fx.Collections;
//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.Linq;
//using System.Windows.Media;

//public sealed class ParentVM : ChartBaseViewModel, IDisposable
//{
//    private readonly ObservableCollection< ChildVM > _childViewModels = new ObservableCollection< ChildVM >( );
//    private readonly UIChartBaseViewModel[ ] _childElements;
//    private double _minFieldWidth;
//    private readonly IScichartSurfaceVM _scichartSurfaceVM;
//    private readonly IChartComponent _iRootElement;
//    private bool _isDisposed;

//    public ParentVM( IScichartSurfaceVM pane, IChartComponent elementXY, IEnumerable< UIChartBaseViewModel > childElements )
//    {
//        if( pane == null )
//        {
//            throw new ArgumentNullException( "pane" );
//        }

//        _scichartSurfaceVM = pane;

//        if( elementXY == null )
//        {
//            throw new ArgumentNullException( "element" );
//        }

//        _iRootElement = elementXY;
//        _childElements = childElements.ToArray( );

//        if( _childElements.IsEmpty( ) )
//        {
//            throw new ArgumentException( "zero child elements" );
//        }

//        if( _childElements.Length == 1 )
//        {
//            MapPropertyChangeNotification( _childElements[ 0 ].Element, nameof( Color ), nameof( Color ) );
//        }

//        foreach( UIChartBaseViewModel childElement in _childElements )
//        {
//            childElement.Init( this );
//        }
//    }

//    public IScichartSurfaceVM ScichartSurface
//    {
//        get
//        {
//            return _scichartSurfaceVM;
//        }
//    }

//    public IChartComponent ChartElement
//    {
//        get
//        {
//            return _iRootElement;
//        }
//    }

//    public IEnumerable< UIChartBaseViewModel > Elements
//    {
//        get
//        {
//            return _childElements;
//        }
//    }

//    public IEnumerable< ChildVM > Values
//    {
//        get
//        {
//            return _childViewModels;
//        }
//    }

//    public Color Color
//    {
//        get
//        {
//            if( _childElements.Length == 1 )
//            {
//                return _childElements[ 0 ].Element.Color;
//            }
//            return Colors.Transparent;
//        }
//    }

//    public double MinFieldWidth
//    {
//        get
//        {
//            return _minFieldWidth;
//        }
//        private set
//        {
//            SetField( ref _minFieldWidth, value, nameof( MinFieldWidth ) );
//        }
//    }

//    public void AddChild( ChildVM childViewModel )
//    {
//        if( childViewModel == null )
//        {
//            throw new ArgumentNullException( "val" );
//        }

//        if( _childViewModels.Contains( childViewModel ) )
//        {
//            throw new ArgumentException( "val" );
//        }

//        childViewModel.Parent = this;
//        _childViewModels.Add( childViewModel );
//    }

//    public void ClearChildViewModels( )
//    {
//        foreach( ChildVM childViewModel in _childViewModels )
//        {
//            childViewModel.Value = null;
//        }
//    }

//    public void UpdateMinFieldWidth( double minFieldWith )
//    {
//        if( minFieldWith <= MinFieldWidth )
//        {
//            return;
//        }

//        MinFieldWidth = minFieldWith;
//    }

//    public bool DrawChartData( ChartDrawData data )
//    {
//        return ChartElement.Draw( data );
//    }

//    public void UpdateChildElements( )
//    {
//        ChartElement.Reset( );

//        foreach( UIChartBaseViewModel child in _childElements )
//        {
//            child.Update( );
//        }
//    }

//    public void UpdateChildElementYAxisMarker( )
//    {
//        foreach( UIChartBaseViewModel child in _childElements )
//        {
//            child.UpdateYAxisMarker( );
//        }
//    }

//    public void ChildElementPeriodicalAction( )
//    {
//        foreach( UIChartBaseViewModel child in _childElements )
//        {
//            child.PerformPeriodicalAction( );
//        }
//    }

//    public void ChildElementUpdateAndClear( )
//    {
//        foreach( UIChartBaseViewModel child in _childElements )
//        {
//            child.GuiUpdateAndClear( );
//        }
//    }

//    public bool IsDisposed
//    {
//        get
//        {
//            return _isDisposed;
//        }
//        private set
//        {
//            _isDisposed = value;
//        }
//    }

//    public void Dispose( )
//    {
//        IsDisposed = true;
//    }
//}

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
public sealed class ParentVM : ChartBaseViewModel, IDisposable
{

    private readonly 
  #nullable disable
  ObservableCollection<ChildVM> _childViewModels = new ObservableCollection<ChildVM>();

    private readonly List<UIChartBaseViewModel> _childElements = new List<UIChartBaseViewModel>();

    private readonly ScichartSurfaceMVVM _scichartSurfaceVM;

    private readonly IChartComponent _chartElement;

    private readonly bool _isCandleElement;

    private ChartCandleElement _candleStickUI;

    private bool _isDisposed;

    public ParentVM( ScichartSurfaceMVVM _param1, IChartComponent _param2 )
    {
        this._scichartSurfaceVM = _param1 ?? throw new ArgumentNullException( "pane" );
        this._chartElement = _param2 ?? throw new ArgumentNullException( "element" );
        this._isCandleElement = _param2 is IChartCandleElement;
        ChartViewModel.AddInteractedEvent( OnAllowToRemove ) ;
        this.ChartElement.PropertyChanged += OnPropertyChanged;
    }

    public ScichartSurfaceMVVM Pane
    {
        get => this._scichartSurfaceVM;
    }

    public bool AllowToRemove
    {
        get
        {
            // BUG: 
            throw new NotImplementedException();
            // return this.Pane.AllowElementToBeRemoved(this);
        }
    }
        

    public IChartComponent ChartElement
    {
        get => this._chartElement;
    }

    public string Title => this.ChartElement.GetGeneratedTitle();

    public bool IsCandleElement => this._isCandleElement;

    public ChartCandleElement Candles
    {
        get => this._candleStickUI;
        private set => this._candleStickUI = value;
    }

    public IEnumerable<UIChartBaseViewModel> Elements
    {
        get
        {
            return ( IEnumerable<UIChartBaseViewModel> ) this._childElements;
        }
    }

    public IEnumerable<ChildVM> Values
    {
        get
        {
            return ( IEnumerable<ChildVM> ) this._childViewModels;
        }
    }

    public Color Color
    {
        get
        {
            return this._childElements.Count == 1 ? this._childElements[ 0 ].Element.Color : Colors.Transparent;
        }
    }

    public void InitializeChildElements( IEnumerable<UIChartBaseViewModel> vms )
    {
        UIChartBaseViewModel[] childElementArray = vms.ToArray<UIChartBaseViewModel>();
        if ( CollectionHelper.IsEmpty<UIChartBaseViewModel>( childElementArray ) )
            throw new ArgumentException( "zero child elements" );

        this._childElements.AddRange( ( IEnumerable<UIChartBaseViewModel> ) childElementArray );
        if ( this.IsCandleElement && this.Candles == null )
            this.Candles = childElementArray.OfType<ChartCandleElement>().First<ChartCandleElement>();
        if ( this._childElements.Count == 1 )
            this.MapPropertyChangeNotification( this._childElements[ 0 ].Element, "Color", "Color" );

        CollectionHelper.ForEach<UIChartBaseViewModel>( ( IEnumerable<UIChartBaseViewModel> ) childElementArray, new Action<UIChartBaseViewModel>( this.OnSomethingMethod0934 ) );
    }

    private void OnAllowToRemove()
    {
        this.NotifyChanged( "AllowToRemove" );
    }

    public void AddChild( ChildVM childViewModel )
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
        foreach ( ChildVM childViewModel in _childViewModels )
        {
            childViewModel.Value = null;
        }
    }

    public bool Draw( ChartDrawData _param1 )
    {
        return this.ChartElement.Draw( _param1 );
    }

    public void Reset()
    {
        this.ChartElement.Reset();

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
        foreach ( UIChartBaseViewModel child in _childElements )
        {
            child.GuiUpdateAndClear();
        }
    }

    internal Subscription GetSubscription( IChartComponent _param1 )
    {
        throw new NotImplementedException();

        // BUG: Wait till Chart Code has been upgrade to uncomment the following code.

        // return ( ( Chart ) this.Pane.Chart ).TryGetSubscription( ( IChartElement ) _param1 );
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
        ChartViewModel.RemoveInteractedEvent( new Action( this.OnAllowToRemove ) );
        this.IsDisposed = true;
    }

    private void OnPropertyChanged( object? _param1, PropertyChangedEventArgs _param2 )
    {
        if ( !( _param2.PropertyName == "FullTitle" ) )
            return;
        this.NotifyChanged( "Title" );
    }

    private void OnSomethingMethod0934( UIChartBaseViewModel _param1 )
    {
        _param1.Init( this );
    }

}

