﻿using Ecng.Collections;
using Ecng.Common;
using Ecng.Xaml;
using SciChart.Charting;
using SciChart.Charting.Model.ChartData;
using SciChart.Charting.Model.ChartSeries;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Numerics;
using SciChart.Charting.Visuals;
using SciChart.Charting.Visuals.PaletteProviders;
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Data.Numerics;
using StockSharp.Localization;
using fx.Charting;
using System;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;

#pragma warning disable CA1416

internal sealed class LineVM< T > : UIHigherVM< LineUI >, IPaletteProvider where T : struct, IComparable
{
    //private ChartSeriesViewModel                        _chartSeriesViewModel;

    private readonly XyzDataSeries< T, double, double > _xyzDataSeries;    
    private ChildVM                                     _childrenChartViewModels;
    private IComparable                                 _lastDrawValueObject;
    private Func< IComparable, Color? >                 _lineColorFunction;
    private IDataSeries                                 _lineData;
    private IRenderableSeries                           _lineSeries;

    public LineVM( LineUI myLineUI ) : base( myLineUI )
    {
        Type type = typeof( T );
        
        if( type != typeof( DateTime ) && type != typeof( double ) )
        {
            throw new NotSupportedException( "X type " + type.Name + " is not supported" );
        }

        if ( myLineUI.FifoCapacity > 0 )
        {
            _xyzDataSeries = new XyzDataSeries< T, double, double >( ) { FifoCapacity = myLineUI.FifoCapacity };
        }
        else
        {
            _xyzDataSeries = new XyzDataSeries<T, double, double>();
        }

        
    }

    protected override void Init( )
    {
        base.Init( );
        AddStylePropertyChanging( ChartElement, "Style", new ChartIndicatorDrawStyles[ 9 ]  {
                                                                                                ChartIndicatorDrawStyles.Line,
                                                                                                ChartIndicatorDrawStyles.NoGapLine,
                                                                                                ChartIndicatorDrawStyles.StepLine,
                                                                                                ChartIndicatorDrawStyles.DashedLine,
                                                                                                ChartIndicatorDrawStyles.Dot,
                                                                                                ChartIndicatorDrawStyles.Histogram,
                                                                                                ChartIndicatorDrawStyles.Bubble,
                                                                                                ChartIndicatorDrawStyles.StackedBar,
                                                                                                ChartIndicatorDrawStyles.Area
                                                                                            } );
        string[ ] strArray = new string[ 2 ]
        {
            "Color",
            "AdditionalColor"
        };

        Func< SeriesInfo, Color > getColorFunc = s =>
        {
            if( ChartElement.Style != ChartIndicatorDrawStyles.StackedBar && ChartElement.Style != ChartIndicatorDrawStyles.Area )
            {
                return ChartElement.Color;
            }

            return ChildVM.GetHigherAlphaColor( ChartElement.Color, ChartElement.AdditionalColor );
        };

        _childrenChartViewModels = new ChildVM( ChartElement, getColorFunc, s => s.FormattedYValue, strArray );

        ChartViewModel.AddChild( _childrenChartViewModels );

        _lineData             = _xyzDataSeries;        
        SetupLineExtraProperties( );

        //_chartSeriesViewModel = new ChartSeriesViewModel( _xyzDataSeries, null );
        //ChartSurfaceViewModel.AddSeriesViewModelsToRoot( RootElem, _chartSeriesViewModel );

    }

    private Type GetRenderSeries( )
    {
        switch( ChartElement.Style )
        {
            case ChartIndicatorDrawStyles.Line:
            case ChartIndicatorDrawStyles.NoGapLine:
            case ChartIndicatorDrawStyles.StepLine:
            case ChartIndicatorDrawStyles.DashedLine:
            {
                return typeof( FastLineRenderableSeries );
            }
                
            // Tony: To Draw my wave Importance, I have to use Dot and draw the dot with SpritePointMarker
            case ChartIndicatorDrawStyles.Dot:
            {
                return typeof( XyScatterRenderableSeries );
            }
                
            case ChartIndicatorDrawStyles.Histogram:
            {
                return typeof( FastColumnRenderableSeries );
            }
                
            case ChartIndicatorDrawStyles.Bubble:
            {
                return typeof( FastBubbleRenderableSeries );
            }
                
            case ChartIndicatorDrawStyles.StackedBar:
            {
                return typeof( StackedColumnRenderableSeries );
            }
                
            case ChartIndicatorDrawStyles.Area:
            {
                return typeof( FastMountainRenderableSeries );
            }
                
            default:
            {
                throw new ArgumentOutOfRangeException( );
            }                
        }
    }

    private void CreateRenderSeriesByDrawStyle( )
    {
        if( _lineSeries is BaseRenderableSeries renderSeries )
        {
            BindingOperations.ClearAllBindings( renderSeries );
            ClearAll( );
        }

        BaseRenderableSeries visualSereis;
        
        switch( ChartElement.Style )
        {
            case ChartIndicatorDrawStyles.Line:
            case ChartIndicatorDrawStyles.NoGapLine:
            case ChartIndicatorDrawStyles.StepLine:
            case ChartIndicatorDrawStyles.DashedLine:
            {
                visualSereis = CreateRenderableSeries<FastLineRenderableSeries>( new ChildVM[ 1 ] { _childrenChartViewModels } );
                visualSereis.DrawNaNAs = LineDrawMode.Gaps;
                visualSereis.SetBindings( BaseRenderableSeries.StrokeProperty            , ChartElement, "Color", BindingMode.TwoWay, null, null );
            }
            break;

            case ChartIndicatorDrawStyles.Dot:
            {
                visualSereis = CreateRenderableSeries<XyScatterRenderableSeries>( new ChildVM[ 1 ] { _childrenChartViewModels } );
                visualSereis.SetBindings( BaseRenderableSeries.StrokeProperty            , ChartElement, "Color", BindingMode.TwoWay, null, null );
                visualSereis.SetBindings( BaseRenderableSeries.PointMarkerProperty       , ChartElement, "PointMarker", BindingMode.TwoWay, null, null );
            }
            break;

            case ChartIndicatorDrawStyles.Sprite:
            {
                visualSereis = CreateRenderableSeries<XyScatterRenderableSeries>( new ChildVM[ 1 ] { _childrenChartViewModels } );
                visualSereis.SetBindings( BaseRenderableSeries.StrokeProperty, ChartElement, "Color", BindingMode.TwoWay, null, null );
                visualSereis.SetBindings( BaseRenderableSeries.PointMarkerProperty, ChartElement, "SpritePointMarker", BindingMode.TwoWay, null, null );
            }
            break;

            case ChartIndicatorDrawStyles.Histogram:
            {
                
                visualSereis =  CreateRenderableSeries<FastColumnRenderableSeries>( new ChildVM[ 1 ] { _childrenChartViewModels } );
                visualSereis.SetBindings( BaseColumnRenderableSeries.FillProperty        , ChartElement, "Color", BindingMode.TwoWay, new ColorToBrushConverter( ), null );
                visualSereis.SetBindings( BaseRenderableSeries.StrokeProperty            , ChartElement, "Color", BindingMode.TwoWay, null, null );
            } 
            break;
            case ChartIndicatorDrawStyles.Bubble:
            {                
                visualSereis =  CreateRenderableSeries<FastBubbleRenderableSeries>( new ChildVM[ 1 ] { _childrenChartViewModels } );
                visualSereis.ResamplingMode = ResamplingMode.None;
                visualSereis.SetBindings( FastBubbleRenderableSeries.BubbleColorProperty , ChartElement, "Color", BindingMode.TwoWay, new ColorToBrushConverter( ), null );
                visualSereis.SetBindings( FastBubbleRenderableSeries.AutoZRangeProperty  , ChartElement, "StrokeThickness", BindingMode.TwoWay, new StockThicknessToRangeConverter( ), null );
            }
            break;

            case ChartIndicatorDrawStyles.StackedBar:
            {
                StackedColumnRenderableSeries stackSeris;
                visualSereis = stackSeris = CreateRenderableSeries<StackedColumnRenderableSeries>( new ChildVM[ 1 ] { _childrenChartViewModels } );
                stackSeris.UseUniformWidth = true;
                stackSeris.SetBindings( BaseRenderableSeries.StrokeProperty              , ChartElement, "Color", BindingMode.TwoWay, new ColorToSeriesColorConverter( ), 51 );
                stackSeris.SetBindings( BaseColumnRenderableSeries.FillProperty          , ChartElement, "Color", BindingMode.TwoWay, new ColorToBrushConverter( ), null );
                stackSeris.SetBindings( BaseColumnRenderableSeries.DataPointWidthProperty, ChartElement, "StrokeThickness", BindingMode.TwoWay, new StrokeThickToPointWidthConverter( ), null );
            }
            break;

            case ChartIndicatorDrawStyles.Area:
            {
                FastMountainRenderableSeries mountainSeries;
                visualSereis = mountainSeries = CreateRenderableSeries<FastMountainRenderableSeries>( new ChildVM[ 1 ] { _childrenChartViewModels } );
                mountainSeries.SetBindings( BaseRenderableSeries.StrokeProperty          , ChartElement, "Color", BindingMode.TwoWay, null, null );
                mountainSeries.SetBindings( BaseMountainRenderableSeries.FillProperty    , ChartElement, "AdditionalColor", BindingMode.TwoWay, new ColorToBrushConverter( ), null );
            }
            break;

            default:
                throw new InvalidOperationException( LocalizedStrings.Str2063Params.Put( ChartElement.Style ) );
        }

        visualSereis.SetBindings( BaseRenderableSeries.RolloverMarkerTemplateProperty, ChartElement, "DrawTemplate", BindingMode.TwoWay, null, null );
        visualSereis.PaletteProvider = this;
        
        SetupAxisMarkerAndBinding( visualSereis, ChartElement, "ShowAxisMarker", "Color" );
        _lineSeries = visualSereis;

        if ( _lineData != null )
        {
            _lineSeries.DataSeries = _lineData;
            ScichartSurfaceMVVM.AddRenderableSeriesToChartSurface( RootElem, _lineSeries );
        }

        SetupLineExtraProperties( );

        

        
    }

    private void SetupLineExtraProperties( )
    {
        if( _lineSeries?.GetType( ) != GetRenderSeries( ) )
        {
            CreateRenderSeriesByDrawStyle( );
        }
        else
        {
            if( !( _lineSeries is FastLineRenderableSeries fastLine ) )
            {
                return;
            }

            fastLine.StrokeDashArray = null;
            fastLine.IsDigitalLine   = false;
            fastLine.DrawNaNAs       = LineDrawMode.Gaps;
            
            if( ChartElement.Style == ChartIndicatorDrawStyles.DashedLine )
            {
                fastLine.StrokeDashArray = new double[ 2 ]
                {
                    5.0,
                    5.0
                };
            }
            else if( ChartElement.Style == ChartIndicatorDrawStyles.StepLine )
            {
                fastLine.IsDigitalLine = true;
            }
            else
            {
                if( ChartElement.Style != ChartIndicatorDrawStyles.NoGapLine )
                {
                    return;
                }
                fastLine.DrawNaNAs = LineDrawMode.ClosedLines;
            }
        }
    }

    protected override void Clear( )
    {
        ScichartSurfaceMVVM.Remove( RootElem );
    }

    protected override void UpdateUi( )
    {
        _xyzDataSeries.Clear( );
        _lastDrawValueObject = default( T );
    }

    public override bool Draw( IEnumerableEx< ChartDrawDataEx.IDrawValue > drawValue )
    {
        return UpdateDataSeries( drawValue.Cast< ChartDrawDataEx.sxTuple< T > >( ).ToEx( drawValue.Count ) );
    }

    public bool UpdateDataSeries< TX1 >( IEnumerableEx< ChartDrawDataEx.sxTuple< TX1 > > drawValues ) where TX1 : struct, IComparable
    {
        if( _lineColorFunction != ChartElement.Colorer )
        {
            _lineColorFunction = ChartElement.Colorer;
            
            _lineSeries.Services?.GetService< ISciChartSurface >( )?.InvalidateElement( );
        }

        if( drawValues == null || drawValues.IsEmpty( ) )
        {
            return false;
        }

        int count = drawValues.Count;
        IComparable lastLine = _lastDrawValueObject;
        int index = -1;
        T[ ] myLine = new T[ count ];
        double[ ] lineOne = new double[ count ];
        double[ ] lineTwo = new double[ count ];
        
        foreach( var lineValue in drawValues )
        {
            T x = ( T )( ValueType )lineValue.GetProperty( );
            switch( x.CompareTo( lastLine ) )
            {
                case -1:
                case 0:
                {
                    _xyzDataSeries.Update( x, lineValue.ValueOne( ), lineValue.ValueTwo( ) );
                    --count;
                }
                break;

                default:
                {
                    ++index;
                    myLine[ index ] = x;
                    lineOne[ index ] = lineValue.ValueOne( );
                    lineTwo[ index ] = lineValue.ValueTwo( );
                    lastLine = x;
                }
                break;
            }
            //
        }

        if( count == 0 )
        {
            return false;
        }

        Array.Resize( ref myLine, count );
        Array.Resize( ref lineOne, count );
        Array.Resize( ref lineTwo, count );
        _xyzDataSeries.Append( myLine, lineOne, lineTwo );

        PerformUiAction( ( ) => _xyzDataSeries.InvalidateParentSurface( RangeMode.None, true ), true );

        _lastDrawValueObject = lastLine;
        return true;
    }

    protected override void RootElementPropertyChanged( IElementWithXYAxes interface5_0, string string_0 )
    {
        base.RootElementPropertyChanged( interface5_0, string_0 );
        if( !( string_0 == "Style" ) )
        {
            return;
        }
        SetupLineExtraProperties( );
    }

    public void OnBeginSeriesDraw( IRenderableSeries rSeries )
    {
        //throw new NotImplementedException( );
    }

    //Color? IPaletteProvider.OverrideColor( IRenderableSeries visualSeries,
    //                                       double double_0,
    //                                       double double_1,
    //                                       double double_2 )
    //{
    //    return new Color?( );
    //}

    //Color? IPaletteProvider.OverrideColor( IRenderableSeries visualSeries,
    //                                       double double_0,
    //                                       double double_1,
    //                                       double double_2,
    //                                       double double_3,
    //                                       double double_4 )
    //{
    //    return new Color?( );
    //}

    //Color? IPaletteProvider.GetColor( IRenderableSeries visualSeries,
    //                                  double double_0,
    //                                  double double_1 )
    //{
    //    if( !( visualSeries.DataSeries is XyzDataSeries< T, double, double > dataSeries ) )
    //    {
    //        return new Color?( );
    //    }
    //    int index = ( int )double_0;
    //    if( !( typeof( T ) == typeof( DateTime ) ) )
    //    {
    //        Func< IComparable, Color? > func0 = _lineColorFunction;
    //        if( func0 == null )
    //        {
    //            return new Color?( );
    //        }
    //        return func0( double_0 );
    //    }
    //    Func< IComparable, Color? > func0_1 = _lineColorFunction;
    //    if( func0_1 == null )
    //    {
    //        return new Color?( );
    //    }
    //    return func0_1( ( ( DateTime )( ValueType )dataSeries.XValues[ index ] ).ToDateTimeOffset( TimeZoneInfo.Utc ) );
    //}      
}
