using Ecng.Collections;
using Ecng.Common;
using Ecng.Xaml;
using StockSharp.Charting;
using SciChart.Charting.Model.ChartData;
using SciChart.Charting.Model.ChartSeries;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Numerics;
using SciChart.Charting.Visuals;
using SciChart.Charting.Visuals.PaletteProviders;
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Data.Numerics;
using Ecng.Drawing;
using StockSharp.Xaml.Charting;
using System;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;

#pragma warning disable CA1416

internal sealed class ChartLineElementVM< T > : ChartCompentWpfUiDomain< ChartLineElement >, IPaletteProvider where T : struct, IComparable
{
    //private ChartSeriesViewModel                        _chartSeriesViewModel;

    private readonly XyzDataSeries< T, double, double > _xyzDataSeries;    
    private ChartElementViewModel                                     _childrenChartViewModels;
    private IComparable                                 _lastDrawValueObject;
    private Func< IComparable, Color? >                 _lineColorFunction;
    private IDataSeries                                 _lineData;
    private IRenderableSeries                           _lineSeries;

    public ChartLineElementVM( ChartLineElement myLineUI ) : base( myLineUI )
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
        AddDrawStylePropertyChanging( ChartComponentView, "Style", new DrawStyles[ 9 ]  {
                                                                                                DrawStyles.Line,
                                                                                                DrawStyles.NoGapLine,
                                                                                                DrawStyles.StepLine,
                                                                                                DrawStyles.DashedLine,
                                                                                                DrawStyles.Dot,
                                                                                                DrawStyles.Histogram,
                                                                                                DrawStyles.Bubble,
                                                                                                DrawStyles.StackedBar,
                                                                                                DrawStyles.Area
                                                                                            } );
        string[ ] strArray = new string[ 2 ]
        {
            "Color",
            "AdditionalColor"
        };

        Func< SeriesInfo, Color > getColorFunc = s =>
        {
            if( ChartComponentView.Style != DrawStyles.StackedBar && ChartComponentView.Style != DrawStyles.Area )
            {
                return ChartComponentView.Color;
            }

            return ChartElementViewModel.GetHigherAlphaColor( ChartComponentView.Color, ChartComponentView.AdditionalColor );
        };

        _childrenChartViewModels = new ChartElementViewModel( ChartComponentView, getColorFunc, s => s.FormattedYValue, strArray );

        ChartViewModel.AddChild( _childrenChartViewModels );

        _lineData             = _xyzDataSeries;        
        SetupLineExtraProperties( );

        //_chartSeriesViewModel = new ChartSeriesViewModel( _xyzDataSeries, null );
        //ViewModel.AddSeriesViewModelsToRoot( RootElem, _chartSeriesViewModel );

    }

    private Type GetRenderSeries( )
    {
        switch( ChartComponentView.Style )
        {
            case DrawStyles.Line:
            case DrawStyles.NoGapLine:
            case DrawStyles.StepLine:
            case DrawStyles.DashedLine:
            {
                return typeof( FastLineRenderableSeries );
            }
                
            // Tony: To Draw my wave Importance, I have to use Dot and draw the dot with SpritePointMarker
            case DrawStyles.Dot:
            {
                return typeof( XyScatterRenderableSeries );
            }
                
            case DrawStyles.Histogram:
            {
                return typeof( FastColumnRenderableSeries );
            }
                
            case DrawStyles.Bubble:
            {
                return typeof( FastBubbleRenderableSeries );
            }
                
            case DrawStyles.StackedBar:
            {
                return typeof( StackedColumnRenderableSeries );
            }
                
            case DrawStyles.Area:
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

        BaseRenderableSeries visualSereis = null;
        
        switch( ChartComponentView.Style )
        {
            case DrawStyles.Line:
            case DrawStyles.NoGapLine:
            case DrawStyles.StepLine:
            case DrawStyles.DashedLine:
            {
                visualSereis = CreateRenderableSeries<FastLineRenderableSeries>( new ChartElementViewModel[ 1 ] { _childrenChartViewModels } );
                visualSereis.DrawNaNAs = LineDrawMode.Gaps;
                visualSereis.SetBindings( BaseRenderableSeries.StrokeProperty            , ChartComponentView, "Color", BindingMode.TwoWay, null, null );
            }
            break;

            case DrawStyles.Dot:
            {
                visualSereis = CreateRenderableSeries<XyScatterRenderableSeries>( new ChartElementViewModel[ 1 ] { _childrenChartViewModels } );
                visualSereis.SetBindings( BaseRenderableSeries.StrokeProperty            , ChartComponentView, "Color", BindingMode.TwoWay, null, null );
                visualSereis.SetBindings( BaseRenderableSeries.PointMarkerProperty       , ChartComponentView, "PointMarker", BindingMode.TwoWay, null, null );
            }
            break;

            case DrawStyles.Sprite:
            {
                visualSereis = CreateRenderableSeries<XyScatterRenderableSeries>( new ChartElementViewModel[ 1 ] { _childrenChartViewModels } );
                visualSereis.SetBindings( BaseRenderableSeries.StrokeProperty, ChartComponentView, "Color", BindingMode.TwoWay, null, null );
                visualSereis.SetBindings( BaseRenderableSeries.PointMarkerProperty, ChartComponentView, "SpritePointMarker", BindingMode.TwoWay, null, null );
            }
            break;

            case DrawStyles.Histogram:
            {
                
                visualSereis =  CreateRenderableSeries<FastColumnRenderableSeries>( new ChartElementViewModel[ 1 ] { _childrenChartViewModels } );
                visualSereis.SetBindings( BaseColumnRenderableSeries.FillProperty        , ChartComponentView, "Color", BindingMode.TwoWay, new ColorToBrushConverter( ), null );
                visualSereis.SetBindings( BaseRenderableSeries.StrokeProperty            , ChartComponentView, "Color", BindingMode.TwoWay, null, null );
            } 
            break;
            case DrawStyles.Bubble:
            {                
                visualSereis =  CreateRenderableSeries<FastBubbleRenderableSeries>( new ChartElementViewModel[ 1 ] { _childrenChartViewModels } );
                visualSereis.ResamplingMode = ResamplingMode.None;
                visualSereis.SetBindings( FastBubbleRenderableSeries.BubbleColorProperty , ChartComponentView, "Color", BindingMode.TwoWay, new ColorToBrushConverter( ), null );
                visualSereis.SetBindings( FastBubbleRenderableSeries.AutoZRangeProperty  , ChartComponentView, "StrokeThickness", BindingMode.TwoWay, new StockThicknessToRangeConverter( ), null );
            }
            break;

            case DrawStyles.StackedBar:
            {
                StackedColumnRenderableSeries stackSeris;
                visualSereis = stackSeris = CreateRenderableSeries<StackedColumnRenderableSeries>( new ChartElementViewModel[ 1 ] { _childrenChartViewModels } );
                stackSeris.UseUniformWidth = true;
                stackSeris.SetBindings( BaseRenderableSeries.StrokeProperty              , ChartComponentView, "Color", BindingMode.TwoWay, new ColorToSeriesColorConverter( ), 51 );
                stackSeris.SetBindings( BaseColumnRenderableSeries.FillProperty          , ChartComponentView, "Color", BindingMode.TwoWay, new ColorToBrushConverter( ), null );
                stackSeris.SetBindings( BaseColumnRenderableSeries.DataPointWidthProperty, ChartComponentView, "StrokeThickness", BindingMode.TwoWay, new StrokeThickToPointWidthConverter( ), null );
            }
            break;

            case DrawStyles.Area:
            {
                FastMountainRenderableSeries mountainSeries;
                visualSereis = mountainSeries = CreateRenderableSeries<FastMountainRenderableSeries>( new ChartElementViewModel[ 1 ] { _childrenChartViewModels } );
                mountainSeries.SetBindings( BaseRenderableSeries.StrokeProperty          , ChartComponentView, "Color", BindingMode.TwoWay, null, null );
                mountainSeries.SetBindings( BaseMountainRenderableSeries.FillProperty    , ChartComponentView, "AdditionalColor", BindingMode.TwoWay, new ColorToBrushConverter( ), null );
            }
            break;

            default:
            //throw new InvalidOperationException( LocalizedStrings.Str2063Params.Put( ChartComponent.Style ) );
            break;
        }

        visualSereis.SetBindings( BaseRenderableSeries.RolloverMarkerTemplateProperty, ChartComponentView, "DrawTemplate", BindingMode.TwoWay, null, null );
        visualSereis.PaletteProvider = this;
        
        SetupAxisMarkerAndBinding( visualSereis, ChartComponentView, "ShowAxisMarker", "Color" );
        _lineSeries = visualSereis;

        if ( _lineData != null )
        {
            _lineSeries.DataSeries = _lineData;

            //DrawingSurface.AddRenderableSeriesToChartSurface( RootElem, _lineSeries );
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
            
            if( ChartComponentView.Style == DrawStyles.DashedLine )
            {
                fastLine.StrokeDashArray = new double[ 2 ]
                {
                    5.0,
                    5.0
                };
            }
            else if( ChartComponentView.Style == DrawStyles.StepLine )
            {
                fastLine.IsDigitalLine = true;
            }
            else
            {
                if( ChartComponentView.Style != DrawStyles.NoGapLine )
                {
                    return;
                }
                fastLine.DrawNaNAs = LineDrawMode.ClosedLines;
            }
        }
    }

    protected override void Clear( )
    {
        //DrawingSurface.Remove( RootElem );
    }

    protected override void UpdateUi( )
    {
        _xyzDataSeries.Clear( );
        _lastDrawValueObject = default( T );
    }

    public override bool Draw( IEnumerableEx< ChartDrawData.IDrawValue > drawValue )
    {
        return UpdateDataSeries( drawValue.Cast< ChartDrawData.sxTuple< T > >( ).ToEx( drawValue.Count ) );
    }

    public bool UpdateDataSeries< TX1 >( IEnumerableEx< ChartDrawData.sxTuple< TX1 > > drawValues ) where TX1 : struct, IComparable
    {
        if( _lineColorFunction != ChartComponentView.WinColorer )
        {
            _lineColorFunction = ChartComponentView.WinColorer;
            
            _lineSeries.Services?.GetService< ISciChartSurface >( )?.InvalidateElement( );
        }

        if( drawValues == null || Ecng.Collections.CollectionHelper.IsEmpty( drawValues ) )
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
            T x = ( T )( ValueType )lineValue.Property( );
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

    protected override void RootElementPropertyChanged( IChartComponent interface5_0, string string_0 )
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
