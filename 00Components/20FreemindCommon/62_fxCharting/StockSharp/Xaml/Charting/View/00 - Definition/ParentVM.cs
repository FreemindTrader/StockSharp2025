using Ecng.Collections;
using StockSharp.Xaml;
using fx.Charting;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;

public sealed class ParentVM : BaseVM, IDisposable
{
    private readonly ObservableCollection< ChildVM > _childViewModels = new ObservableCollection< ChildVM >( );
    private readonly UIBaseVM[ ] _childElements;
    private double _minFieldWidth;
    private readonly IScichartSurfaceVM _scichartSurfaceVM;
    private readonly IChartComponent _iRootElement;
    private bool _isDisposed;

    public ParentVM( IScichartSurfaceVM pane, IChartComponent elementXY, IEnumerable< UIBaseVM > childElements )
    {
        if( pane == null )
        {
            throw new ArgumentNullException( "pane" );
        }

        _scichartSurfaceVM = pane;

        if( elementXY == null )
        {
            throw new ArgumentNullException( "element" );
        }

        _iRootElement = elementXY;
        _childElements = childElements.ToArray( );

        if( _childElements.IsEmpty( ) )
        {
            throw new ArgumentException( "zero child elements" );
        }

        if( _childElements.Length == 1 )
        {
            MapPropertyChangeNotification( _childElements[ 0 ].Element, nameof( Color ), nameof( Color ) );
        }

        foreach( UIBaseVM childElement in _childElements )
        {
            childElement.Init( this );
        }
    }

    public IScichartSurfaceVM ScichartSurface
    {
        get
        {
            return _scichartSurfaceVM;
        }
    }

    public IChartComponent ChartElement
    {
        get
        {
            return _iRootElement;
        }
    }

    public IEnumerable< UIBaseVM > Elements
    {
        get
        {
            return _childElements;
        }
    }

    public IEnumerable< ChildVM > Values
    {
        get
        {
            return _childViewModels;
        }
    }

    public Color Color
    {
        get
        {
            if( _childElements.Length == 1 )
            {
                return _childElements[ 0 ].Element.Color;
            }
            return Colors.Transparent;
        }
    }

    public double MinFieldWidth
    {
        get
        {
            return _minFieldWidth;
        }
        private set
        {
            SetField( ref _minFieldWidth, value, nameof( MinFieldWidth ) );
        }
    }

    public void AddChild( ChildVM childViewModel )
    {
        if( childViewModel == null )
        {
            throw new ArgumentNullException( "val" );
        }

        if( _childViewModels.Contains( childViewModel ) )
        {
            throw new ArgumentException( "val" );
        }

        childViewModel.Parent = this;
        _childViewModels.Add( childViewModel );
    }

    public void ClearChildViewModels( )
    {
        foreach( ChildVM childViewModel in _childViewModels )
        {
            childViewModel.Value = null;
        }
    }

    public void UpdateMinFieldWidth( double minFieldWith )
    {
        if( minFieldWith <= MinFieldWidth )
        {
            return;
        }

        MinFieldWidth = minFieldWith;
    }

    public bool DrawChartData( ChartDrawDataEx data )
    {
        return ChartElement.Draw( data );
    }

    public void UpdateChildElements( )
    {
        ChartElement.Reset( );

        foreach( UIBaseVM child in _childElements )
        {
            child.Update( );
        }
    }

    public void UpdateChildElementYAxisMarker( )
    {
        foreach( UIBaseVM child in _childElements )
        {
            child.UpdateYAxisMarker( );
        }
    }

    public void ChildElementPeriodicalAction( )
    {
        foreach( UIBaseVM child in _childElements )
        {
            child.PerformPeriodicalAction( );
        }
    }

    public void ChildElementUpdateAndClear( )
    {
        foreach( UIBaseVM child in _childElements )
        {
            child.GuiUpdateAndClear( );
        }
    }

    public bool IsDisposed
    {
        get
        {
            return _isDisposed;
        }
        private set
        {
            _isDisposed = value;
        }
    }

    public void Dispose( )
    {
        IsDisposed = true;
    }
}
