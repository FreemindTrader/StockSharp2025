using Ecng.Collections;
using Ecng.Common;
using Ecng.Xaml;
using StockSharp.Charting;
using SciChart.Charting.ChartModifiers;
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
using Ecng.Drawing;
using System.Windows.Data;
using System.Windows.Media;

#pragma warning disable CA1416

internal sealed class QuotesVM : ChartCompentWpfUiDomain<QuotesUI>, IFastQuotes 
{
    private readonly XyDataSeries<DateTime, double> _askLine;
    private readonly XyDataSeries<DateTime, double> _bidLine;

    private ChartSeriesViewModel _askLineRSerieVM = null;
    private ChartSeriesViewModel _bidLineRSerieVM = null;

    private bool _doneInit = false;

    private ChartElementViewModel _bidLineVM;
    private ChartElementViewModel _askLineVM;
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

        // There are 4 types of draw styles for the Quote lines
        DrawStyles[ ] lineStyle = new DrawStyles[ 4 ] {
                                                        DrawStyles.Line,
                                                        DrawStyles.NoGapLine,
                                                        DrawStyles.StepLine,
                                                        DrawStyles.DashedLine
                                                      };

        // Validate that Bid Line and Ask Line have a style that is one of the lineDrawStyle
        ValidateDrawStylePropertyChanging( ChartComponentView.BidLine, "Style", lineStyle );
        ValidateDrawStylePropertyChanging( ChartComponentView.AskLine, "Style", lineStyle );

        string[ ] strArray = new string[ 2 ] { "Color", "AdditionalColor" };

        // The Quote UI is composed of two lines - ask and bid
        ChartComponentUiDomain.AddChild( _bidLineVM = new ChartElementViewModel( ChartComponentView.BidLine, new Func<SeriesInfo, Color>( HigherAlphaColor ), ( s => s.FormattedYValue ), strArray ) );
        ChartComponentUiDomain.AddChild( _askLineVM = new ChartElementViewModel( ChartComponentView.AskLine, new Func<SeriesInfo, Color>( LowerAlphaColor ), ( s => s.FormattedYValue ), strArray ) );

        AddPropertyEvents( ChartComponentView.BidLine );
        AddPropertyEvents( ChartComponentView.AskLine );

        // The following code will create renderableSeries for
        //
        //      - Bid Line
        //      - Ask Line
        //
        CreateQuoteRSeriesAndBinding( _askLineRSerieVM, ChartComponentView.BidLine, _bidLineVM );
        CreateQuoteRSeriesAndBinding( _bidLineRSerieVM, ChartComponentView.AskLine, _askLineVM );

        ChartComponentView.BidLine.ShowAxisMarker = true;


        SetupAxisMarkerAndBinding( _askLineRSerieVM.RenderSeries, ChartComponentView.AskLine, "ShowAxisMarker", "Color" );
        SetupAxisMarkerAndBinding( _bidLineRSerieVM.RenderSeries, ChartComponentView.BidLine, "ShowAxisMarker", "Color" );


        if ( _askLineRSerieVM != null )
        {
            _askLineRSerieVM.DataSeries = _askLine;
        }

        if ( _bidLineRSerieVM != null )
        {
            _bidLineRSerieVM.DataSeries = _bidLine;
        }

        DrawingSurface.AddRenderableSeriesToChartSurface( RootElem, _askLineRSerieVM );
        DrawingSurface.AddRenderableSeriesToChartSurface( RootElem, _bidLineRSerieVM );

        SetIncludeSeries( );        
    }

    private void SetIncludeSeries( )
    {   
        // We won't to only display the two line, so we would disable the other modifier.

        SetIncludeSeries( _askLineRSerieVM.RenderSeries, false );

        BaseRenderableSeries baseSeries = ( BaseRenderableSeries )_askLineRSerieVM.RenderSeries;

        SeriesValueModifier.SetIncludeSeries( baseSeries, true );

        SetIncludeSeries( _bidLineRSerieVM.RenderSeries, false );        
    }    

    private void CreateQuoteRSeriesAndBinding( ChartSeriesViewModel lineSeriesVM, ChartLineElement line, ChartElementViewModel viewModel )
    {
        var quoteSeries = lineSeriesVM.RenderSeries as QuoteRenderableSeries;

        if ( quoteSeries == null )
        {
            ChartElementViewModel[ ] childViewModels = new ChartElementViewModel[ 1 ] { viewModel };

            quoteSeries = CreateRenderableSeries<QuoteRenderableSeries>( childViewModels );

            quoteSeries.SetBindings( BaseRenderableSeries.StrokeProperty, line, "Color", BindingMode.TwoWay, null, null );
            quoteSeries.SetBindings( BaseRenderableSeries.RolloverMarkerTemplateProperty, line, "DrawTemplate",    BindingMode.TwoWay, null, null );
            quoteSeries.SetBindings( BaseRenderableSeries.StrokeThicknessProperty, line, "StrokeThickness", BindingMode.TwoWay, null, null );
            quoteSeries.SetBindings( BaseRenderableSeries.AntiAliasingProperty, line, "AntiAliasing", BindingMode.TwoWay, null, null );

            var isVisibleProperty = BaseRenderableSeries.IsVisibleProperty;
            var cnvt = new BackgroundBorderBrushMultiConverter( );
            cnvt.Value = true;

            Binding[ ] bindingArray = new Binding[ 3 ]
                                                        {
                                                            new Binding( "IsVisible" )
                                                            {
                                                                Source = line
                                                            },
                                                            new Binding( "IsVisible" )
                                                            {
                                                                Source = ChartComponentView
                                                            },
                                                            new Binding( "IsVisible" )
                                                            {
                                                                Source = ( ( IChartComponent ) ChartComponentView ).RootElement
                                                            }
                                                        };
            quoteSeries.SetMultiBinding( isVisibleProperty, cnvt, bindingArray );
        }

        quoteSeries.StrokeDashArray = new double[2] { 1.0, 3.0 };        
    }

    protected override void Clear( )
    {        
        DrawingSurface.Remove( RootElem );
    }

    protected override void UpdateUi( )
    {
        _askLine.Clear( );
        _bidLine.Clear( );        
        _lastDrawValueObject = TimeSpan.Zero;
    }

    public override bool Draw( IEnumerableEx<ChartDrawData.IDrawValue> drawValues )
    {
        return UpdateDataSeries( drawValues.Cast<ChartDrawData.sxTuple<TimeSpan>>( ).ToEx( drawValues.Count ) );
    }

    public bool UpdateDataSeries<TX1>( IEnumerableEx<ChartDrawData.sxTuple<TX1>> drawValues ) where TX1 : struct, IComparable
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

        foreach ( ChartDrawData.sxTuple<TX1> band in drawValues )
        {
            DateTime x = ( DateTime )( ValueType )band.Property( );

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

    protected override void RootElementPropertyChanged( IChartComponent comp, string propName )
    {
        base.RootElementPropertyChanged( comp, propName );

        if ( propName == "Style" )
        {            
            CreateQuoteRSeriesAndBinding( _askLineRSerieVM, ChartComponentView.BidLine, _bidLineVM );
            CreateQuoteRSeriesAndBinding( _bidLineRSerieVM, ChartComponentView.AskLine, _askLineVM );
        }
        if ( !( propName == "Style" ) )
        {
            return;
        }
        SetIncludeSeries( );
    }

    private Color HigherAlphaColor( SeriesInfo seriesInfo_0 )
    {
        return ChartElementViewModel.GetHigherAlphaColor( ChartComponentView.BidLine.Color, ChartComponentView.BidLine.AdditionalColor );
    }

    private Color LowerAlphaColor( SeriesInfo seriesInfo_0 )
    {
        return ChartElementViewModel.GetHigherAlphaColor( ChartComponentView.AskLine.Color, ChartComponentView.AskLine.AdditionalColor );
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
