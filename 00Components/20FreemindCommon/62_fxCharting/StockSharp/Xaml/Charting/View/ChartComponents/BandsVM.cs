using Ecng.Collections;
using Ecng.Common;
using Ecng.Xaml;
using SciChart.Charting;
using SciChart.Charting.Model.ChartData;
using SciChart.Charting.Model.ChartSeries;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Visuals.RenderableSeries;
using StockSharp.Localization;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic; using fx.Collections;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

#pragma warning disable CA1416

internal sealed class BandViewModel< T > : UIHigherVM< BandsUI > where T : struct, IComparable
{
    private readonly XyyDataSeries< T, double > _bandData;
    private readonly XyDataSeries< T, double >  _lineOneData;
    private readonly XyDataSeries< T, double >  _lineTwoData;

    private FastLineRenderableSeries            _lineOneRSerie = null;
    private FastLineRenderableSeries            _lineTwoRSerie = null;
    private FastBandRenderableSeries            _wholeBandRSerie;

    
    private ChildVM                             _lineOneViewModel;
    private ChildVM                             _lineTwoViewModel;
    private IComparable                         _lastDrawValueObject;

    public BandViewModel( BandsUI bandElement ) : base( bandElement )
    {
        Type type = typeof( T );
        
        if( type != typeof( DateTime ) && type != typeof( double ) )
        {
            throw new NotSupportedException( "X type " + type.Name + " is not supported" );
        }

        _bandData    = new XyyDataSeries< T, double >( );
        _lineOneData = new XyDataSeries< T, double >( );
        _lineTwoData = new XyDataSeries< T, double >( );
    }

    protected override void Init( )
    {
        base.Init( );
        DrawStyles[ ] lineStyle = new DrawStyles[ 4 ]
                                                                                {
                                                                                    DrawStyles.Line,
                                                                                    DrawStyles.NoGapLine,
                                                                                    DrawStyles.StepLine,
                                                                                    DrawStyles.DashedLine
                                                                                };
        AddStylePropertyChanging( ChartElement.Line1, "Style", lineStyle );
        AddStylePropertyChanging( ChartElement.Line2, "Style", lineStyle );

        string[ ] strArray = new string[ 2 ]
                                            {
                                                "Color",
                                                "AdditionalColor"
                                            };

        ChartViewModel.AddChild( _lineOneViewModel = new ChildVM( ChartElement.Line1, new Func< SeriesInfo, Color >( HigherAlphaColor ), ( s => s.FormattedYValue ), strArray ) );
        ChartViewModel.AddChild( _lineTwoViewModel = new ChildVM( ChartElement.Line2, new Func< SeriesInfo, Color >( LowerAlphaColor ),  ( s => s.FormattedYValue ), strArray ) );

        AddPropertyEvents( ChartElement.Line1 );
        AddPropertyEvents( ChartElement.Line2 );
        
        SetupFastBandSeriesAndBinding( );
        _lineOneRSerie = CreateFastLineSeriesAndBinding( _lineOneRSerie, ChartElement.Line1, _lineOneViewModel );
        _lineTwoRSerie = CreateFastLineSeriesAndBinding( _lineTwoRSerie,  ChartElement.Line2, _lineTwoViewModel );

        SetupAxisMarkerAndBinding( _lineOneRSerie, ChartElement.Line1, "ShowAxisMarker", "Color" );
        SetupAxisMarkerAndBinding( _lineTwoRSerie, ChartElement.Line2, "ShowAxisMarker", "Color" );

        if ( _wholeBandRSerie != null )
        {
            _wholeBandRSerie.DataSeries = _bandData;
        }

        if ( _lineOneRSerie != null )
        {
            _lineOneRSerie.DataSeries = _lineOneData;
        }

        if ( _lineTwoRSerie != null )
        {
            _lineTwoRSerie.DataSeries = _lineTwoData;
        }

        ScichartSurfaceMVVM.AddRenderableSeriesToChartSurface( RootElem, _lineOneRSerie );
        ScichartSurfaceMVVM.AddRenderableSeriesToChartSurface( RootElem, _lineTwoRSerie );
        ScichartSurfaceMVVM.AddRenderableSeriesToChartSurface( RootElem, _wholeBandRSerie );

        SetIncludeSeries( );
    }

    private void SetIncludeSeries( )
    {
        SetIncludeSeries( _lineOneRSerie, false );
        SetIncludeSeries( _lineTwoRSerie, true );
        SetIncludeSeries( _wholeBandRSerie, ChartElement.Style == DrawStyles.Band );
    }

    private void SetupFastBandSeriesAndBinding( )
    {
        if( _wholeBandRSerie is FastBandRenderableSeries )
        {
            return;
        }

        _wholeBandRSerie = CreateRenderableSeries< FastBandRenderableSeries >( new ChildVM[ 0 ] );

        _wholeBandRSerie.Fill = _wholeBandRSerie.Fill = Colors.Transparent;

        _wholeBandRSerie.SetBindings( BaseRenderableSeries.StrokeProperty,   ChartElement.Line1, "AdditionalColor", BindingMode.TwoWay, null, null );
        _wholeBandRSerie.SetBindings( FastBandRenderableSeries.StrokeY1Property, ChartElement.Line2, "AdditionalColor", BindingMode.TwoWay, null, null );
    }

    private FastLineRenderableSeries CreateFastLineSeriesAndBinding( IRenderableSeries lineSeries, LineUI line, ChildVM viewModel )
    {
        // Tony Fix

        throw new NotImplementedException();

        //var fastLineSeries = lineSeries as FastLineRenderableSeries;

        //if ( fastLineSeries == null )
        //{            
        //    ChildVM[ ] childViewModels = new ChildVM[ 1 ] { viewModel };            

        //    fastLineSeries = CreateRenderableSeries< FastLineRenderableSeries >( childViewModels );            

        //    fastLineSeries.SetBindings( BaseRenderableSeries.StrokeProperty                , line, "Color"          , BindingMode.TwoWay, null, null );
        //    fastLineSeries.SetBindings( BaseRenderableSeries.RolloverMarkerTemplateProperty, line, "DrawTemplate"   , BindingMode.TwoWay, null, null );
        //    fastLineSeries.SetBindings( BaseRenderableSeries.StrokeThicknessProperty       , line, "StrokeThickness", BindingMode.TwoWay, null, null );
        //    fastLineSeries.SetBindings( BaseRenderableSeries.AntiAliasingProperty          , line, "AntiAliasing"   , BindingMode.TwoWay, null, null );
            
        //    var isVisibleProperty   = BaseRenderableSeries.IsVisibleProperty;
        //    var cnvt                = new BackgroundBorderBrushMultiConverter( );
        //    cnvt.Value              = true;

        //    Binding[ ] bindingArray = new Binding[ 3 ]
        //                                                {
        //                                                    new Binding( "IsVisible" )
        //                                                    {
        //                                                        Source =    line
        //                                                    },
        //                                                    new Binding( "IsVisible" )
        //                                                    {
        //                                                        Source =    ChartElement
        //                                                    },
        //                                                    new Binding( "IsVisible" )
        //                                                    {
        //                                                        Source =    ( ( IChartComponent ) ChartElement ).ElementWithXYAxes
        //                                                    }
        //                                                };
        //    fastLineSeries.SetMultiBinding( isVisibleProperty, cnvt, bindingArray );
        //}

        //fastLineSeries.StrokeDashArray = null;
        //fastLineSeries.IsDigitalLine = false;

        //if( line.Style == DrawStyles.DashedLine )
        //{
        //    fastLineSeries.StrokeDashArray = new double[ 2 ] { 5.0, 5.0 };
        //}
        //else
        //{
        //    if( line.Style == DrawStyles.StepLine )
        //    {
        //        fastLineSeries.IsDigitalLine = true;
        //    }            
        //}

        //return fastLineSeries;
    }

    protected override void Clear( )
    {
        ScichartSurfaceMVVM.Remove( RootElem );
    }

    protected override void UpdateUi( )
    {
        _lineOneData.Clear( );
        _lineTwoData.Clear( );
        _bandData.Clear( );
        _lastDrawValueObject = default( T );
    }

    public override bool Draw( IEnumerableEx< ChartDrawData.IDrawValue > ienumerableEx_0 )
    {
        return UpdateDataSeries( ienumerableEx_0.Cast< ChartDrawData.sxTuple< T > >( ).ToEx( ienumerableEx_0.Count ) );
    }

    public bool UpdateDataSeries< TX1 >( IEnumerableEx< ChartDrawData.sxTuple< TX1 > > drawValues ) where TX1 : struct, IComparable
    {
        if( drawValues == null )
        {
            return false;
        }

        int count = drawValues.Count;
        IComparable lastBand = _lastDrawValueObject;
        int index = -1;
        T[ ] myBand = new T[ count ];
        double[ ] bandOne = new double[ count ];
        double[ ] bandTwo = new double[ count ];

        foreach( ChartDrawData.sxTuple< TX1 > band in drawValues )
        {
            T x = ( T )( ValueType )band.GetProperty( );
            
            switch( x.CompareTo( lastBand ) )
            {
                case -1:
                {
                    //throw new InvalidOperationException( LocalizedStrings.Str2064Params.Put( x, lastBand ) );
                }
                break;
                    
                case 0:
                {
                    _lineOneData.Update( x, band.ValueOne( ) );
                    _lineTwoData.Update( x, band.ValueTwo( ) );
                    _bandData.Update( x, band.ValueOne( ), band.ValueTwo( ) );
                    --count;
                }
                break;

                default:
                {
                    ++index;
                    myBand[ index ] = x;
                    bandOne[ index ] = band.ValueOne( );
                    bandTwo[ index ] = band.ValueTwo( );
                }
                break;
            }
            lastBand = x;
        }

        if( count == 0 )
        {
            return false;
        }

        Array.Resize( ref myBand, count );
        Array.Resize( ref bandOne, count );
        Array.Resize( ref bandTwo, count );

        _lineOneData.Append( myBand, bandOne );
        _lineTwoData.Append( myBand, bandTwo );
        _bandData.Append( myBand, bandOne, bandTwo );

        PerformUiAction( ( ) =>
                                {
                                    _lineOneData.InvalidateParentSurface( RangeMode.None, true );
                                    _lineTwoData.InvalidateParentSurface( RangeMode.None, true );
                                    _bandData.InvalidateParentSurface( RangeMode.None, true );
                                },
                                true
                        );

        _lastDrawValueObject = lastBand;
        return true;
    }

    protected override void RootElementPropertyChanged( IChartComponent elementXY, string propName )
    {
        base.RootElementPropertyChanged( elementXY, propName );

        if( propName == "Style" )
        {
            SetupFastBandSeriesAndBinding( );
            _lineOneRSerie = CreateFastLineSeriesAndBinding( _lineOneRSerie, ChartElement.Line1, _lineOneViewModel );
            _lineTwoRSerie = CreateFastLineSeriesAndBinding( _lineTwoRSerie, ChartElement.Line2, _lineTwoViewModel );
        }
        if( !( propName == "Style" ) )
        {
            return;
        }
        SetIncludeSeries( );
    }

    private Color HigherAlphaColor( SeriesInfo seriesInfo_0 )
    {
        return ChildVM.GetHigherAlphaColor( ChartElement.Line1.Color, ChartElement.Line1.AdditionalColor );
    }

    private Color LowerAlphaColor( SeriesInfo seriesInfo_0 )
    {
        return ChildVM.GetHigherAlphaColor( ChartElement.Line2.Color, ChartElement.Line2.AdditionalColor );
    }   
}
