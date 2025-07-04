using Ecng.Collections;
using Ecng.Common;
using Ecng.Xaml;
using SciChart.Charting;
using SciChart.Charting.ChartModifiers;
using SciChart.Charting.Model.ChartData;
using SciChart.Charting.Model.ChartSeries;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Visuals.RenderableSeries;
using StockSharp.Localization;
using fx.Charting;
using System;
using System.Collections.Generic; using fx.Collections;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

#pragma warning disable CA1416

internal sealed class QuotesVM : UIHigherVM<QuotesUI>, IFastQuotes 
{
    private readonly XyDataSeries<DateTime, double> _askLine;
    private readonly XyDataSeries<DateTime, double> _bidLine;

    private QuoteRenderableSeries _askLineRSerie = null;
    private QuoteRenderableSeries _bidLineRSerie = null;

    private bool _doneInit = false;

    private ChildVM _bidLineVM;
    private ChildVM _askLineVM;
    private IComparable _lastDrawValueObject;

    public QuotesVM( QuotesUI bandElement ) : base( bandElement )
    {        
        
        _askLine  = new XyDataSeries<DateTime, double>( );
        _bidLine  = new XyDataSeries<DateTime, double>( );
    }

    public bool CanUpdateQuotes
    {
        get
        {
            return _doneInit;
        }

        set
        {
            _doneInit = value;
        }
    }

    protected override void Init( )
    {
        base.Init( );
        ChartIndicatorDrawStyles[ ] lineStyle = new ChartIndicatorDrawStyles[ 4 ] {
                                                                                    ChartIndicatorDrawStyles.Line,
                                                                                    ChartIndicatorDrawStyles.NoGapLine,
                                                                                    ChartIndicatorDrawStyles.StepLine,
                                                                                    ChartIndicatorDrawStyles.DashedLine
                                                                                  };
        AddStylePropertyChanging( ChartElement.BidLine, "Style", lineStyle );
        AddStylePropertyChanging( ChartElement.AskLine, "Style", lineStyle );

        string[ ] strArray = new string[ 2 ] { "Color", "AdditionalColor" };

        ChartViewModel.AddChild( _bidLineVM = new ChildVM( ChartElement.BidLine, new Func<SeriesInfo, Color>( HigherAlphaColor ), ( s => s.FormattedYValue ), strArray ) );
        ChartViewModel.AddChild( _askLineVM = new ChildVM( ChartElement.AskLine, new Func<SeriesInfo, Color>( LowerAlphaColor ), ( s => s.FormattedYValue ), strArray ) );

        AddPropertyEvents( ChartElement.BidLine );
        AddPropertyEvents( ChartElement.AskLine );
        
        _askLineRSerie = CreateQuoteRSeriesAndBinding( _askLineRSerie, ChartElement.BidLine, _bidLineVM );
        _bidLineRSerie = CreateQuoteRSeriesAndBinding( _bidLineRSerie, ChartElement.AskLine, _askLineVM );

        ChartElement.BidLine.ShowAxisMarker = true;


        SetupAxisMarkerAndBinding( _askLineRSerie, ChartElement.AskLine, "ShowAxisMarker", "Color" );
        SetupAxisMarkerAndBinding( _bidLineRSerie, ChartElement.BidLine, "ShowAxisMarker", "Color" );        

        if ( _askLineRSerie != null )
        {
            _askLineRSerie.DataSeries = _askLine;
        }

        if ( _bidLineRSerie != null )
        {
            _bidLineRSerie.DataSeries = _bidLine;
        }

        ScichartSurfaceMVVM.AddRenderableSeriesToChartSurface( RootElem, _askLineRSerie );
        ScichartSurfaceMVVM.AddRenderableSeriesToChartSurface( RootElem, _bidLineRSerie );        

        SetIncludeSeries( );        
    }

    private void SetIncludeSeries( )
    {   
        // We won't to only display the two line, so we would disable the other modifier.

        SetIncludeSeries( _askLineRSerie, false );

        SeriesValueModifier.SetIncludeSeries( _askLineRSerie, true );

        SetIncludeSeries( _bidLineRSerie, false );        
    }    

    private QuoteRenderableSeries CreateQuoteRSeriesAndBinding( IRenderableSeries lineSeries, LineUI line, ChildVM viewModel )
    {
        // Tony 4:

        throw new NotImplementedException();

        //var fastLineSeries = lineSeries as QuoteRenderableSeries;

        //if ( fastLineSeries == null )
        //{
        //    ChildVM[ ] childViewModels = new ChildVM[ 1 ] { viewModel };

        //    fastLineSeries = CreateRenderableSeries<QuoteRenderableSeries>( childViewModels );

        //    fastLineSeries.SetBindings( BaseRenderableSeries.StrokeProperty,                 line, "Color",           BindingMode.TwoWay, null, null );
        //    //fastLineSeries.SetBindings( BaseRenderableSeries.RolloverMarkerTemplateProperty, line, "DrawTemplate",    BindingMode.TwoWay, null, null );
        //    fastLineSeries.SetBindings( BaseRenderableSeries.StrokeThicknessProperty,        line, "StrokeThickness", BindingMode.TwoWay, null, null );
        //    fastLineSeries.SetBindings( BaseRenderableSeries.AntiAliasingProperty,           line, "AntiAliasing",    BindingMode.TwoWay, null, null );

        //    var isVisibleProperty = BaseRenderableSeries.IsVisibleProperty;
        //    var cnvt = new BackgroundBorderBrushMultiConverter( );
        //    cnvt.Value = true;

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
        //    fastLineSeries.SetMultiBinding( isVisibleProperty, cnvt,  bindingArray );
        //}
        
        //fastLineSeries.StrokeDashArray = new double[ 2 ] { 1.0, 3.0 };


        //return fastLineSeries;
    }

    protected override void Clear( )
    {
        ScichartSurfaceMVVM.Remove( RootElem );
    }

    protected override void UpdateUi( )
    {
        _askLine.Clear( );
        _bidLine.Clear( );        
        _lastDrawValueObject = TimeSpan.Zero;
    }

    public override bool Draw( IEnumerableEx<ChartDrawDataEx.IDrawValue> drawValues )
    {
        return UpdateDataSeries( drawValues.Cast<ChartDrawDataEx.sxTuple<TimeSpan>>( ).ToEx( drawValues.Count ) );
    }

    public bool UpdateDataSeries<TX1>( IEnumerableEx<ChartDrawDataEx.sxTuple<TX1>> drawValues ) where TX1 : struct, IComparable
    {
        if ( drawValues == null )
        {
            return false;
        }

        int count = drawValues.Count;
        IComparable lastBand = _lastDrawValueObject;
        int index = -1;
        DateTime[ ] myBand = new DateTime[ count ];
        double[ ] bandOne = new double[ count ];
        double[ ] bandTwo = new double[ count ];

        foreach ( ChartDrawDataEx.sxTuple<TX1> band in drawValues )
        {
            DateTime x = ( DateTime )( ValueType )band.GetProperty( );

            switch ( x.CompareTo( lastBand ) )
            {
                case -1:
                {
                    //throw new InvalidOperationException( LocalizedStrings.Str2064Params.Put( x, lastBand ) );
                }
                break;

                case 0:
                {
                    _askLine.Update( x, band.ValueOne( ) );
                    _bidLine.Update( x, band.ValueTwo( ) );                    
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

        if ( count == 0 )
        {
            return false;
        }

        Array.Resize( ref myBand, count );
        Array.Resize( ref bandOne, count );
        Array.Resize( ref bandTwo, count );

        _askLine.Append( myBand, bandOne );
        _bidLine.Append( myBand, bandTwo );        

        PerformUiAction( ( ) =>
        {
            _askLine.InvalidateParentSurface( RangeMode.None, true );
            _bidLine.InvalidateParentSurface( RangeMode.None, true );            
        },
                                true
                        );

        _lastDrawValueObject = lastBand;
        return true;
    }

    protected override void RootElementPropertyChanged( IChartComponent elementXY, string propName )
    {
        base.RootElementPropertyChanged( elementXY, propName );

        if ( propName == "Style" )
        {            
            _askLineRSerie = CreateQuoteRSeriesAndBinding( _askLineRSerie, ChartElement.BidLine, _bidLineVM );
            _bidLineRSerie = CreateQuoteRSeriesAndBinding( _bidLineRSerie, ChartElement.AskLine, _askLineVM );
        }
        if ( !( propName == "Style" ) )
        {
            return;
        }
        SetIncludeSeries( );
    }

    private Color HigherAlphaColor( SeriesInfo seriesInfo_0 )
    {
        return ChildVM.GetHigherAlphaColor( ChartElement.BidLine.Color, ChartElement.BidLine.AdditionalColor );
    }

    private Color LowerAlphaColor( SeriesInfo seriesInfo_0 )
    {
        return ChildVM.GetHigherAlphaColor( ChartElement.AskLine.Color, ChartElement.AskLine.AdditionalColor );
    }

    

    public void UpdateQuotes( double bid, double ask )
    {
        if( !_doneInit )
            return;

        if ( _askLine.Count == 0 )
        {
            _askLine.Insert( 0, DateTime.MinValue, ask );
            _askLine.Insert( 1, DateTime.MaxValue, ask );
        }
        else
        {
            _askLine.Update( 0,  ask );
            _askLine.Update( 1,  ask );
        }

        if ( _bidLine.Count == 0 )
        {
            _bidLine.Insert( 0, DateTime.MinValue, bid );
            _bidLine.Insert( 1, DateTime.MaxValue, bid );
        }
        else
        {
            _bidLine.Update( 0, bid );
            _bidLine.Update( 1, bid );

        }        
    }
}
