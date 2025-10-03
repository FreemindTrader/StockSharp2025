using Ecng.Collections;
using Ecng.Xaml;
using SciChart.Charting.Visuals.Annotations;
using SciChart.Charting.Visuals.RenderableSeries;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;
using Ecng.Xaml.Converters;
using SciChart.Charting.Model.ChartSeries;

#pragma warning disable CA1416

namespace StockSharp.Xaml.Charting;

/// <summary>
/// This class mainly take care of the drawing of the Chart Componenets
/// 
/// It defines the abstract drawing methods for the Chart Elements or Chart components
/// 
///     1) UpdateUi
///     2) Init
///     3) Draw
///     4) PerformPeriodicalAction
///     
/// It provides the basic abstract functions for the dervied class to implement.
/// </summary>
public abstract class ChartElementUiDomain : ChartPropertiesViewModel
{
    private readonly Dictionary< IRenderableSeries, AxisMarkerAnnotation > _renderseries2AxisMarker = new Dictionary< IRenderableSeries, AxisMarkerAnnotation >( );
    private ChartComponentUiDomain _chartComponentViewModel;    

    protected ChartComponentUiDomain ChartComponentUiDomain
    {
        get
        {
            return _chartComponentViewModel;
        }

        set
        {
            _chartComponentViewModel = value;
        }
    }

    public abstract IChartElementUiDomain Element
    {
        get;
    }

    public IChartComponent RootElem
    {
        get
        {
            return ChartComponentUiDomain.RootChartComponent;
        }
    }


    /// <summary>
    /// This is the Scichart Drawing Surface
    /// </summary>
    protected ScichartSurfaceMVVM DrawingSurface
    {
        get
        {
            return ChartComponentUiDomain.Pane;
        }
    }    

    protected bool IsDisposed( )
    {
        return ChartComponentUiDomain.IsDisposed;
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

    /// <summary>
    /// Abstract method that the inherited class need to implement for drawing of the chart component.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
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

    public void Init( ChartComponentUiDomain parentVM )
    {
        if( ChartComponentUiDomain != null )
        {
            throw new InvalidOperationException( "parent was already addded" );
        }

        ChartComponentUiDomain =  parentVM;
        Init( );
        Reset( );
    }

    public virtual void UpdateYAxisMarker( )
    {
        foreach( var p in _renderseries2AxisMarker )
        {
            p.Value.Y1 = p.Key.DataSeries?.LatestYValue;
        }
    }

    public virtual void PerformPeriodicalAction( )
    {
    }

    /// <summary>
    /// This function will remove 
    ///     - all the axis markers from the chart
    ///     - all the annotation on the chart
    /// </summary>
    protected void ClearAll( )
    {
        //
        // Summary:
        //     The AxisMarkerAnnotation provides an axis label which is data-bound to its Y-value.
        //     Used to place a marker on the Y-Axis it can give feedback about the latest value
        //     of a series, or important points in a series.
        foreach ( var p in _renderseries2AxisMarker )
        {
            p.Value.IsHidden = true;

            //
            // Summary:
            //     Removes all bindings, including bindings of type System.Windows.Data.Binding,
            //     System.Windows.Data.MultiBinding, and System.Windows.Data.PriorityBinding, from
            //     the specified System.Windows.DependencyObject.
            //
            // Parameters:
            //   target:
            //     The object from which to remove bindings.
            //
            // Exceptions:
            //   T:System.ArgumentNullException:
            //     If target is null.
            BindingOperations.ClearAllBindings( p.Value );
        }

        DrawingSurface.RemoveAnnotation( RootElem, null );
        
        _renderseries2AxisMarker.Clear( );
    }


    /// <summary>
    /// For the XY Axis, we need to add the labels.
    /// 
    ///     - the color of the Axis font is white
    /// </summary>
    /// <param name="rs"></param>
    /// <param name="chartComponent"></param>
    /// <param name="showAxisMakerStr"></param>
    /// <param name="colorStr"></param>
    protected void SetupAxisMarkerAndBinding( IRenderableSeries rs,
                                              IChartComponent chartComponent,
                                              string showAxisMakerStr,
                                              string colorStr )
    {
        var axisMarker        = new AxisMarkerAnnotation( );
        axisMarker.FontSize   = 11.0;
        axisMarker.Foreground = Brushes.White;
        
        _renderseries2AxisMarker[ rs ] = axisMarker;
        DrawingSurface.AddAxisMakerAnnotation( RootElem, axisMarker, axisMarker );

        axisMarker.SetBindings( AnnotationBase.XAxisIdProperty, chartComponent, "XAxisId", BindingMode.TwoWay, null, null );
        axisMarker.SetBindings( AnnotationBase.YAxisIdProperty, chartComponent, "YAxisId", BindingMode.TwoWay, null, null );

        //AxisMarkerAnnotation markerAnnotation3 = axisMarker;
        var isHiddenProperty = AnnotationBase.IsHiddenProperty;
        var converter        = new BoolAnyConverter( );
        converter.Value      = false;

        Binding[ ] bindingArray = new Binding[ 2 ]
        {
            new Binding( showAxisMakerStr ) { Source = chartComponent },
            new Binding( )
                                            {
                                                Source = rs,
                                                Path = new PropertyPath( BaseRenderableSeries.IsVisibleProperty )
                                            }
        };

        // Axis Marker is visible depends on if the ChartComponent's ShowAxisMarker is true and the RenderableSeries's IsVisible is true
        axisMarker.SetMultiBinding( isHiddenProperty, converter, bindingArray );

        // Both the background and broder of the axis will follow the color of the ChartComponent
        if ( colorStr != null )
        {
            axisMarker.SetBindings( Control.BackgroundProperty,  chartComponent, colorStr, BindingMode.TwoWay, new ColorToBrushConverter( ), null );
            axisMarker.SetBindings( Control.BorderBrushProperty, chartComponent, colorStr, BindingMode.TwoWay, new ColorToBrushConverter( ), null );
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
        foreach( var pair in _renderseries2AxisMarker )
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
