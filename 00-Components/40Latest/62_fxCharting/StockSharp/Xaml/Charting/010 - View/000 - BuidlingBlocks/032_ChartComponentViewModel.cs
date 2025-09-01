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
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;

namespace StockSharp.Xaml.Charting;


/// <summary>
/// Represents an view model for a chart Component.
/// 
/// In CandleStickVM, the CandleStick is a Component, and that Candlestick UI has 4 different viusal elements
///     1) High
///     2) Low
///     3) Open
///     4) Close 
/// All of them are represented as a ChartComponent.
/// 
/// 
/// For the quote UI, it should be made up of 2 lines. The QuoteUI should be a ChartComponent. And it is made up of 2 Children's
/// 
///     1) Ask Line - Children one, AskLine
///     2) Bid Line - Children two, BidLine
/// 
/// </summary>
public sealed class ChartComponentViewModel : ChartBaseViewModel, IDisposable
{

    // This is the View Model of the Chart Component
    private readonly ObservableCollection<ChartElementViewModel> _chartElementsViewModel = new ObservableCollection<ChartElementViewModel>();

    // This is the View of the Chart Component
    private readonly List<DrawableChartComponentBaseViewModel>   _chartElementsView = new List<DrawableChartComponentBaseViewModel>();

    private readonly ScichartSurfaceMVVM _drawingSurface;

    private readonly IChartComponent     _chartUiComponent;

    private readonly bool                _isCandleElement;

    private ChartCandleElementViewModel  _ChartCandleElementViewModel;

    private bool                         _isDisposed;

    public ChartComponentViewModel( ScichartSurfaceMVVM drawingSurface, IChartComponent component )
    {
        _drawingSurface   = drawingSurface ?? throw new ArgumentNullException( "pane" );
        _chartUiComponent = component ?? throw new ArgumentNullException( "element" );
        _isCandleElement  = component is IChartCandleElement;

        ChartViewModel.InteractedEvent += OnAllowToRemove;
        RootChartComponent.PropertyChanged += OnPropertyChanged;
    }

    public ScichartSurfaceMVVM Pane
    {
        get => _drawingSurface;
    }

    public bool AllowToRemove
    {
        get
        {
            return Pane.AllowElementToBeRemoved( this );
        }
    }


    public IChartComponent RootChartComponent
    {
        get => _chartUiComponent;
    }

    public string Title => RootChartComponent.GetGeneratedTitle();

    public bool IsCandleElement => _isCandleElement;

    public ChartCandleElementViewModel CandlesViewModel
    {
        get => _ChartCandleElementViewModel;
        private set => _ChartCandleElementViewModel = value;
    }

    public IEnumerable<DrawableChartComponentBaseViewModel> Elements
    {
        get
        {
            return _chartElementsView;
        }
    }

    public IEnumerable<ChartElementViewModel> Values
    {
        get
        {
            return _chartElementsViewModel;
        }
    }

    public Color Color
    {
        get
        {
            return _chartElementsView.Count == 1 ? _chartElementsView[0].Element.Color : Colors.Transparent;
        }
    }

    /// <summary>
    /// In the Initialization of the ChartComponet, we need to initialize the child elements of the ChartComponent.
    /// 
    /// eg. The ChartCandleElementViewModel contains High, low, open, close and each of them need to be initialized. 
    /// 
    /// </summary>
    /// <param name="children"></param>
    /// <exception cref="ArgumentException"></exception>
    public void InitializeChildElements( IEnumerable<DrawableChartComponentBaseViewModel> children )
    {
        var childElements = children.ToArray();

        if ( childElements.IsEmpty() )
        {
            throw new ArgumentException( "zero child elements" );
        }
            
        _chartElementsView.AddRange( childElements );

        if ( IsCandleElement && CandlesViewModel == null )
        {
            CandlesViewModel = childElements.OfType<ChartCandleElementViewModel>().First<ChartCandleElementViewModel>();
        }

        if ( _chartElementsView.Count == 1 )
        {
            MapPropertyChangeNotification( _chartElementsView[0].Element, "Color", "Color" );
        }

        childElements.ForEach<DrawableChartComponentBaseViewModel>( p => p.Init( this ) );
    }

    private void OnAllowToRemove()
    {
        NotifyChanged( "AllowToRemove" );
    }

    public void AddChild( ChartElementViewModel childViewModel )
    {
        if ( childViewModel == null )
            throw new ArgumentNullException( "val" );

        if ( _chartElementsViewModel.Contains( childViewModel ) )
            throw new ArgumentException( "val" );

        childViewModel.ChartComponent = this;
        
        _chartElementsViewModel.Add( childViewModel );
    }

    public void ClearChildViewModels()
    {
        foreach ( ChartElementViewModel childViewModel in _chartElementsViewModel )
        {
            childViewModel.Value = null;
        }
    }

    public bool Draw( ChartDrawData data )
    {
        return RootChartComponent.Draw( data );
    }

    public void Reset()
    {
        RootChartComponent.Reset();

        foreach ( var child in _chartElementsView )
        {
            child.Reset();
        }
    }

    public void UpdateYAxisMarker()
    {
        foreach ( var child in _chartElementsView )
        {
            child.UpdateYAxisMarker();
        }
    }

    public void PerformPeriodicalAction()
    {
        foreach ( var child in _chartElementsView )
        {
            child.PerformPeriodicalAction();
        }
    }

    public void GuiUpdateAndClear()
    {
        foreach ( var child in _chartElementsView )
        {
            child.GuiUpdateAndClear();
        }
    }

    internal Subscription GetSubscription( IChartComponent _param1 )
    {
        throw new NotImplementedException();

        // BUG: Wait till Chart Code has been upgrade to uncomment the following code.

        // return ( ( Chart ) Pane.Chart ).TryGetSubscription( ( IChartElement ) _param1 );
    }

    public bool IsDisposed
    {
        get => _isDisposed;
        private set => _isDisposed = value;
    }

    public void Dispose()
    {
        // BUG: I believe there is a problem here. Doesn't look like OnAllToRemove should be binded to InteractedEvent
        //      Should probably be linked to AllowToRemoveEvent
        ChartViewModel.InteractedEvent -= OnAllowToRemove;
        IsDisposed = true;
    }

    private void OnPropertyChanged( object? _param1, PropertyChangedEventArgs _param2 )
    {
        if ( _param2.PropertyName != "FullTitle" )
            return;

        NotifyChanged( "Title" );
    }
}



//using Ecng.Collections;
//using StockSharp.Xaml;
//using StockSharp.Xaml.Charting;
//using System;
//using System.Collections.Generic; using fx.Collections;
//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.Linq;
//using System.Windows.Media;

//public sealed class ChartComponentViewModel : ChartBaseViewModel, IDisposable
//{
//    private readonly ObservableCollection< ChartElementViewModel > _childViewModels = new ObservableCollection< ChartElementViewModel >( );
//    private readonly DrawableChartComponentBaseViewModel[ ] _componentsCache;
//    private double _minFieldWidth;
//    private readonly IDrawingSurfaceVM _scichartSurfaceVM;
//    private readonly IChartComponent _iRootElement;
//    private bool _isDisposed;

//    public ChartComponentViewModel( IDrawingSurfaceVM pane, IChartComponent elementXY, IEnumerable< DrawableChartComponentBaseViewModel > childElements )
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
//        _componentsCache = childElements.ToArray( );

//        if( _componentsCache.IsEmpty( ) )
//        {
//            throw new ArgumentException( "zero child elements" );
//        }

//        if( _componentsCache.Length == 1 )
//        {
//            MapPropertyChangeNotification( _componentsCache[ 0 ].Element, nameof( Color ), nameof( Color ) );
//        }

//        foreach( DrawableChartComponentBaseViewModel childElement in _componentsCache )
//        {
//            childElement.Init( this );
//        }
//    }

//    public IDrawingSurfaceVM ScichartSurface
//    {
//        get
//        {
//            return _scichartSurfaceVM;
//        }
//    }

//    public IChartComponent ChartComponent
//    {
//        get
//        {
//            return _iRootElement;
//        }
//    }

//    public IEnumerable< DrawableChartComponentBaseViewModel > Elements
//    {
//        get
//        {
//            return _componentsCache;
//        }
//    }

//    public IEnumerable< ChartElementViewModel > Values
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
//            if( _componentsCache.Length == 1 )
//            {
//                return _componentsCache[ 0 ].Element.Color;
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

//    public void AddChild( ChartElementViewModel childViewModel )
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
//        foreach( ChartElementViewModel childViewModel in _childViewModels )
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
//        return ChartComponent.Draw( data );
//    }

//    public void UpdateChildElements( )
//    {
//        ChartComponent.Reset( );

//        foreach( DrawableChartComponentBaseViewModel child in _componentsCache )
//        {
//            child.Update( );
//        }
//    }

//    public void UpdateChildElementYAxisMarker( )
//    {
//        foreach( DrawableChartComponentBaseViewModel child in _componentsCache )
//        {
//            child.UpdateYAxisMarker( );
//        }
//    }

//    public void ChildElementPeriodicalAction( )
//    {
//        foreach( DrawableChartComponentBaseViewModel child in _componentsCache )
//        {
//            child.PerformPeriodicalAction( );
//        }
//    }

//    public void ChildElementUpdateAndClear( )
//    {
//        foreach( DrawableChartComponentBaseViewModel child in _componentsCache )
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

