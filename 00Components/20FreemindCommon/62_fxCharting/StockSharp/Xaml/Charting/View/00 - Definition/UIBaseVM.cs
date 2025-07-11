using Ecng.Collections;
using Ecng.Xaml;
using SciChart.Charting.Visuals.Annotations;
using SciChart.Charting.Visuals.RenderableSeries;
using StockSharp.Xaml;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;
using Ecng.Xaml.Converters;

#pragma warning disable CA1416

public abstract class UIChartBaseViewModel : ChartBaseViewModel
{
    private readonly PooledDictionary< IRenderableSeries, AxisMarkerAnnotation > _renderseries2AxisMarker = new PooledDictionary< IRenderableSeries, AxisMarkerAnnotation >( );
    private ParentVM _parentChartViewModel;    

    protected ParentVM ChartViewModel
    {
        get
        {
            return _parentChartViewModel;
        }

        set
        {
            _parentChartViewModel = value;
        }
    }

    public abstract IDrawableChartElement Element
    {
        get;
    }

    public IChartComponent RootElem
    {
        get
        {
            return ChartViewModel.ChartElement;
        }
    }

    protected IScichartSurfaceVM ScichartSurfaceMVVM
    {
        get
        {
            return ChartViewModel.Pane;
        }
    }    

    protected bool IsDisposed( )
    {
        return ChartViewModel.IsDisposed;
    }

    private static Dispatcher GetDispatcher( )
    {
        return GuiDispatcher.GlobalDispatcher.Dispatcher;
    }

    protected static bool IsUiThread( )
    {
        return GetDispatcher( ).CheckAccess( );
    }

    protected abstract void UpdateUi( );

    protected virtual void Init( )
    {
    }

    protected abstract void Clear( );

    public abstract bool Draw( IEnumerableEx< ChartDrawData.IDrawValue > data );
    //public abstract bool Draw<T>( IEnumerableEx< ChartDrawData.IDrawValue<T> > data );

    public void Reset( )
    {
        UpdateUi( );
        PerformUiAction( new Action( ResetY1Annotation ), true );
    }

    public virtual void GuiUpdateAndClear( )
    {
        PerformUiAction( new Action( UpdateAndClear ), true );
    }

    protected virtual void RootElementPropertyChanging( IChartComponent interface5_0, string string_0, object obj )
    {
    }

    protected virtual void RootElementPropertyChanged( IChartComponent interface5_0, string string_0 )
    {
    }

    public void Init( ParentVM parentVM )
    {
        if( ChartViewModel != null )
        {
            throw new InvalidOperationException( "parent was already addded" );
        }
        ChartViewModel = ( parentVM );
        Init( );
        Reset( );
    }

    public virtual void UpdateYAxisMarker( )
    {
        foreach( KeyValuePair< IRenderableSeries, AxisMarkerAnnotation > p in _renderseries2AxisMarker )
        {
            p.Value.Y1 = p.Key.DataSeries?.LatestYValue;
        }
    }

    public virtual void PerformPeriodicalAction( )
    {
    }

    protected void ClearAll( )
    {
        foreach( KeyValuePair< IRenderableSeries, AxisMarkerAnnotation > p in _renderseries2AxisMarker )
        {
            p.Value.IsHidden = true;
            BindingOperations.ClearAllBindings( p.Value );
        }
        ScichartSurfaceMVVM.RemoveAnnotation( RootElem, null );
        _renderseries2AxisMarker.Clear( );
    }

    protected void SetupAxisMarkerAndBinding( IRenderableSeries renderSeries,
                                              IChartComponent element,
                                              string showAxisMakerStr,
                                              string colorStr )
    {
        var axisMarker        = new AxisMarkerAnnotation( );
        axisMarker.FontSize   = 11.0;
        axisMarker.Foreground = Brushes.White;
        
        _renderseries2AxisMarker[ renderSeries ] = axisMarker;
        ScichartSurfaceMVVM.AddAxisMakerAnnotation( RootElem, axisMarker, axisMarker );

        axisMarker.SetBindings( AnnotationBase.XAxisIdProperty, element, "XAxisId", BindingMode.TwoWay, null, null );
        axisMarker.SetBindings( AnnotationBase.YAxisIdProperty, element, "YAxisId", BindingMode.TwoWay, null, null );

        //AxisMarkerAnnotation markerAnnotation3 = axisMarker;
        var isHiddenProperty = AnnotationBase.IsHiddenProperty;
        var converter        = new BoolAnyConverter( );
        converter.Value      = false;

        Binding[ ] bindingArray = new Binding[ 2 ]
        {
            new Binding( showAxisMakerStr ) { Source = element },
            new Binding( )
                                            {
                                                Source = renderSeries,
                                                Path = new PropertyPath( BaseRenderableSeries.IsVisibleProperty )
                                            }
        };

        axisMarker.SetMultiBinding( isHiddenProperty, converter, bindingArray );

        if( colorStr != null )
        {
            axisMarker.SetBindings( Control.BackgroundProperty, element, colorStr, BindingMode.TwoWay, new ColorToBrushConverter( ), null );
            axisMarker.SetBindings( Control.BorderBrushProperty, element, colorStr, BindingMode.TwoWay, new ColorToBrushConverter( ), null );
        }
        else
        {
            axisMarker.Background = axisMarker.BorderBrush = Brushes.Gray;
        }
    }

    protected void PerformUIAction2( Action toBeDone, bool checkDisposed )
    {
        ExecuteAction( new Action< Action >( ( GetDispatcher( ) ).GuiAsync ), toBeDone, checkDisposed );
    }

    protected void PerformUiAction( Action toBeDone, bool checkDisposed )
    {
        ExecuteAction( new Action< Action >( ( GetDispatcher( ) ).GuiSync ), toBeDone, checkDisposed );
    }

    private void ExecuteAction( Action< Action > UIAction, Action tobeDone, bool checkDisposed )
    {
        var checkDisposedAction = new Action( ( ) =>
        {
            if( IsDisposed( ) )
            {
                return;
            }

            tobeDone( );
        } );

        Action action = checkDisposed ? checkDisposedAction : tobeDone;

        if( IsUiThread( ) )
        {
            action( );
        }
        else
        {
            UIAction( action );
        }
    }

    private void ResetY1Annotation( )
    {
        foreach( KeyValuePair< IRenderableSeries, AxisMarkerAnnotation > pair in _renderseries2AxisMarker )
        {
            pair.Value.Y1 = null;
        }
    }

    private void UpdateAndClear( )
    {
        Reset( );
        Clear( );
    }
}
