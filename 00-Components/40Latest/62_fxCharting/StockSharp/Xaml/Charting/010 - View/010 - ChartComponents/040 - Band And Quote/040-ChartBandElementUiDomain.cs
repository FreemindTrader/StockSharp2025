using Ecng.Collections;
using Ecng.Common;
using Ecng.Xaml;
using StockSharp.Charting;
using SciChart.Charting.Model.ChartData;
using SciChart.Charting.Model.ChartSeries;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Visuals.RenderableSeries;
using StockSharp.Localization;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic;
using fx.Collections;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Ecng.Drawing;
using Ecng.Xaml.Converters;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;
using System.Collections;
using StockSharp.Algo.Indicators;

#pragma warning disable CA1416

internal sealed class ChartBandElementUiDomain<T> : ChartCompentWpfUiDomain<ChartBandElement> where T : struct, IComparable
{
    private readonly XyyDataSeries< T, double > _bandData;
    private readonly XyDataSeries< T, double >  _lineOneData;
    private readonly XyDataSeries< T, double >  _lineTwoData;

    //
    // Summary:
    //     A viewmodel to a single data-render series pair, used in the new SciChart.Charting.Visuals.SciChartSurface
    //     Mvvm API. For usage, see the SeriesSource property and the Mvvm examples in the
    //     examples suite. You may bind SeriesSource to a collection of SciChart.Charting.Model.ChartSeries.IChartSeriesViewModel
    //     and SciChart will automatically associated the SciChart.Charting.Visuals.RenderableSeries.BaseRenderableSeries
    //     and SciChart.Charting.Model.DataSeries.IDataSeries instances
    //
    // Remarks:
    //     DataSeries are assigned to the RenderableSeries via the SciChart.Charting.Visuals.RenderableSeries.IRenderableSeries.DataSeries
    //     property. Any time a DataSeries is appended to, the parent SciChart.Charting.Visuals.SciChartSurface
    //     will be redrawn
    private ChartSeriesViewModel  _lineOneSeriesVM = null;
    private ChartSeriesViewModel  _lineTwoSeriesVM = null;
    private ChartSeriesViewModel  _wholeBandSeriesVM;


    private ChartElementViewModel _lineOneViewModel;
    private ChartElementViewModel _lineTwoViewModel;
    private IComparable           _lastDrawValueObject;

    public ChartBandElementUiDomain( ChartBandElement bandElement ) : base( bandElement )
    {
        Type type = typeof( T );

        if ( type != typeof( DateTime ) && type != typeof( double ) )
        {
            throw new NotSupportedException( "X type " + type.Name + " is not supported" );
        }

        _bandData    = new XyyDataSeries<T, double>( );
        _lineOneData = new XyDataSeries<T, double>( );
        _lineTwoData = new XyDataSeries<T, double>( );
    }

    protected override void Init( )
    {
        base.Init( );

        // There are 4 types of draw styles for the band
        var lineDrawStyle = new DrawStyles[ 4 ]
                                        {
                                            DrawStyles.Line,
                                            DrawStyles.NoGapLine,
                                            DrawStyles.StepLine,
                                            DrawStyles.DashedLine
                                        };

        // Validate that Line1 and Line2 have a style that is one of the lineDrawStyle
        ValidateDrawStylePropertyChanging( ChartComponentView.Line1, "Style", lineDrawStyle );
        ValidateDrawStylePropertyChanging( ChartComponentView.Line2, "Style", lineDrawStyle );

        string[ ] strArray = new string[ 2 ]
                                            {
                                                "Color",
                                                "AdditionalColor"
                                            };

        // The band is composed of two lines.
        ChartComponentUiDomain.AddChild( _lineOneViewModel = new ChartElementViewModel( ChartComponentView.Line1, new Func<SeriesInfo, Color>( HigherAlphaColor ), ( s => s.FormattedYValue ), strArray ) );
        ChartComponentUiDomain.AddChild( _lineTwoViewModel = new ChartElementViewModel( ChartComponentView.Line2, new Func<SeriesInfo, Color>( LowerAlphaColor ),  ( s => s.FormattedYValue ), strArray ) );

        AddPropertyEvents( ChartComponentView.Line1 );
        AddPropertyEvents( ChartComponentView.Line2 );

        // The following code will create renderableSeries for
        //      - Whole Band
        //      - Line One
        //      - Line Two
        //
        CreateFastBandSeriesAndSetupBinding( );
        CreateFastLineSeriesAndSetupBinding( _lineOneSeriesVM, ChartComponentView.Line1, _lineOneViewModel );
        CreateFastLineSeriesAndSetupBinding( _lineTwoSeriesVM, ChartComponentView.Line2, _lineTwoViewModel );

        // Setup the X-Axis and Y-Axis marker and the bindings for line 1
        CreateAxisMarkerAndSetupBinding( _lineOneSeriesVM.RenderSeries, ChartComponentView.Line1, "ShowAxisMarker", "Color" );
        // Setup the X-Axis and Y-Axis marker and the bindings for line 2
        CreateAxisMarkerAndSetupBinding( _lineTwoSeriesVM.RenderSeries, ChartComponentView.Line2, "ShowAxisMarker", "Color" );

        if ( _wholeBandSeriesVM != null )
        {
            _wholeBandSeriesVM.DataSeries = _bandData;
        }

        if ( _lineOneSeriesVM != null )
        {
            _lineOneSeriesVM.DataSeries = _lineOneData;
        }

        if ( _lineTwoSeriesVM != null )
        {
            _lineTwoSeriesVM.DataSeries = _lineTwoData;
        }

        
        DrawingSurface.AddRenderableSeriesToChartSurface( RootElem, _lineOneSeriesVM );
        DrawingSurface.AddRenderableSeriesToChartSurface( RootElem, _lineTwoSeriesVM );
        DrawingSurface.AddRenderableSeriesToChartSurface( RootElem, _wholeBandSeriesVM );

        SetIncludeSeries( );
    }

    /// <summary>
    /// LineOne RenderSeries is not included the following modifiers
    ///     RolloverModifier
    ///     CursorModifier
    ///     TooltipModifier
    ///     VerticalSliceModifier
    ///     
    /// While LineTwo and the Band RenderSeries are included in the above modifiers
    /// 
    /// </summary>
    private void SetIncludeSeries( )
    {
        SetIncludeSeries( _lineOneSeriesVM.RenderSeries, false );
        SetIncludeSeries( _lineTwoSeriesVM.RenderSeries, true );
        SetIncludeSeries( _wholeBandSeriesVM.RenderSeries, ChartComponentView.Style == DrawStyles.Band );
    }

    /// <summary>
    /// Setup the band series and binding
    /// 
    /// Notice that we are using the additionalColor for the filled color of the band
    /// 
    /// Only the _wholeBandSeriesVM RenderSeries has a real scichart RenderSeries
    /// </summary>
    private void CreateFastBandSeriesAndSetupBinding( )
    {
        if ( _wholeBandSeriesVM.RenderSeries is MyFastBandRenderableSeries )
        {
            return;
        }

        var rs = CreateRenderableSeries<MyFastBandRenderableSeries>( new ChartElementViewModel[ 0 ] );

        rs.Stroke = rs.StrokeY1 = Colors.Transparent;

        XamlHelper.SetBindings( rs, MyFastBandRenderableSeries.FillY1Property, ChartComponentView.Line1, "AdditionalColor", BindingMode.TwoWay, null, null );
        XamlHelper.SetBindings( rs, MyFastBandRenderableSeries.FillProperty,   ChartComponentView.Line2, "AdditionalColor", BindingMode.TwoWay, null, null );

        _wholeBandSeriesVM.RenderSeries = ( IRenderableSeries ) rs;
    }

    /// <summary>
    /// Create a new MyFastLineRenderableSeries and bind to the ChartLineElement LineUI properties
    /// 
    /// The line's visibility property is multi-bound to the following three properties
    /// 
    /// 
    /// </summary>
    /// <param name="lineSeriesVM"></param>
    /// <param name="lineUI"></param>
    /// <param name="viewModel"></param>
    private void CreateFastLineSeriesAndSetupBinding( ChartSeriesViewModel lineSeriesVM, ChartLineElement lineUI, ChartElementViewModel viewModel )
    {
        if ( !( lineSeriesVM.RenderSeries is MyFastLineRenderableSeries fastLine ) )
        {
            var viewModelArray = new ChartElementViewModel[1] { viewModel };
            
            fastLine = CreateRenderableSeries<MyFastLineRenderableSeries>( viewModelArray );
            lineSeriesVM.RenderSeries = ( IRenderableSeries ) fastLine;
            XamlHelper.SetBindings( fastLine, BaseRenderableSeries.StrokeProperty,                 lineUI, "Color",           BindingMode.TwoWay, ( IValueConverter ) null,  null );
            XamlHelper.SetBindings( fastLine, BaseRenderableSeries.RolloverMarkerTemplateProperty, lineUI, "DrawTemplate",    BindingMode.TwoWay, ( IValueConverter ) null,  null );
            XamlHelper.SetBindings( fastLine, BaseRenderableSeries.StrokeThicknessProperty,        lineUI, "StrokeThickness", BindingMode.TwoWay, ( IValueConverter ) null,  null );
            XamlHelper.SetBindings( fastLine, BaseRenderableSeries.AntiAliasingProperty,           lineUI, "AntiAliasing",    BindingMode.TwoWay, ( IValueConverter ) null,  null );
            
            var isVisibleProperty  = BaseRenderableSeries.IsVisibleProperty;
            var boolAllConverter   = new BoolAllConverter();
            boolAllConverter.Value = true;
            var bindingArray       = new Binding[3]
                                    {
                                        new Binding("IsVisible") { Source = lineUI },                           // This specific line
                                        new Binding("IsVisible") { Source = ChartComponentView },               // The chartBand Component
                                        new Binding("IsVisible") { Source = ChartComponentView.RootElement }    // Its parent element if existed
                                    };
            XamlHelper.SetMultiBinding( fastLine, isVisibleProperty, ( IMultiValueConverter ) boolAllConverter, bindingArray );
        }

        fastLine.StrokeDashArray = null;
        fastLine.IsDigitalLine   = false;
        
        if ( lineUI.Style == DrawStyles.DashedLine )
        {
            fastLine.StrokeDashArray = new double[2] { 5.0, 5.0 };
        }
        else
        {
            if ( lineUI.Style != DrawStyles.StepLine )
                return;

            // Only DrawStyles.StepLine is digital line
            fastLine.IsDigitalLine = true;
        }        
    }

    protected override void Clear( )
    {
        DrawingSurface.RemoveChartComponent( RootElem );
    }

    protected override void UpdateUi( )
    {
        _lineOneData.Clear( );
        _lineTwoData.Clear( );
        _bandData.Clear( );
        _lastDrawValueObject = default( T );
    }

    public override bool Draw( IEnumerableEx<ChartDrawData.IDrawValue> _param1 )
    {
        return Draw<T>( CollectionHelper.ToEx < ChartDrawData.sxTuple< T >> ( ( ( IEnumerable ) _param1 ).Cast < ChartDrawData.sxTuple< T >> (), ( ( IEnumerableEx ) _param1 ).Count ));
    }

    public bool Draw<TX1>( IEnumerableEx<ChartDrawData.sxTuple<TX1>> drawData ) where TX1 : struct, IComparable
    {
        if ( drawData == null )
            return false;

        var count    = drawData.Count;
        var lastBand = _lastDrawValueObject;
        int index    = -1;
        var myBand   = new T[count];
        var bandOne  = new double[count];
        var bandTwo  = new double[count];

        foreach ( var band in drawData )
        {
            T property = (T) (ValueType) band.Property();

            switch ( property.CompareTo( lastBand ) )
            {
                case -1:
                    throw new InvalidOperationException( StringHelper.Put( LocalizedStrings.CannotChangeCandleValue, property, lastBand ));

                case 0:
                    _lineOneData.Update(property, band.ValueOne( ));
                    _lineTwoData.Update(property, band.ValueTwo( ));
                    _bandData.Update(property, band.ValueOne( ), band.ValueTwo( ));
                    --count;
                    break;
                default:
                    ++index;
                    myBand[index] = property;
                    bandOne[index] = band.ValueOne( );
                    bandTwo[index] = band.ValueTwo( );
                    break;
            }
            lastBand = ( IComparable ) property;
        }
        if ( count == 0 )
            return false;
        Array.Resize( ref myBand, count );
        Array.Resize( ref bandOne, count );
        Array.Resize( ref bandTwo, count );

        _lineOneData.Append( myBand, bandOne);
        _lineTwoData.Append( myBand, bandTwo);
        _bandData.Append( myBand, bandOne, bandTwo);

        // The following is not in the SS code.
        PerformUiActionSync( ( ) =>
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

    public bool UpdateDataSeries<TX1>( IEnumerableEx<ChartDrawData.sxTuple<TX1>> drawValues ) where TX1 : struct, IComparable
    {
        if ( drawValues == null )
        {
            return false;
        }

        int count = drawValues.Count;
        IComparable lastBand = _lastDrawValueObject;
        int index = -1;
        T[ ] myBand = new T[ count ];
        double[ ] bandOne = new double[ count ];
        double[ ] bandTwo = new double[ count ];

        foreach ( ChartDrawData.sxTuple<TX1> band in drawValues )
        {
            T x = ( T )( ValueType )band.Property( );

            switch ( x.CompareTo( lastBand ) )
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
                        myBand[index] = x;
                        bandOne[index] = band.ValueOne( );
                        bandTwo[index] = band.ValueTwo( );
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

        _lineOneData.Append( myBand, bandOne );
        _lineTwoData.Append( myBand, bandTwo );
        _bandData.Append( myBand, bandOne, bandTwo );

        PerformUiActionSync( ( ) =>
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

    protected override void RootElementPropertyChanged( IChartComponent comp, string propName )
    {
        base.RootElementPropertyChanged( comp, propName );

        if ( propName == "Style" )
        {
            // Here we create the band and setup its binding
            CreateFastBandSeriesAndSetupBinding( );

            // Here we recreate the two lines and setup their binding
            CreateFastLineSeriesAndSetupBinding( _lineOneSeriesVM, ChartComponentView.Line1, _lineOneViewModel );
            CreateFastLineSeriesAndSetupBinding( _lineTwoSeriesVM, ChartComponentView.Line2, _lineTwoViewModel );
        }

        if ( propName != "Style" )
        {
            return;
        }
        
        SetIncludeSeries( );
    }

    /// <summary>
    /// Get from two colors' RGBA which has the higher A value
    /// </summary>
    /// <param name="sInfo"></param>
    /// <returns></returns>
    private Color HigherAlphaColor( SeriesInfo sInfo )
    {
        return ChartElementViewModel.GetHigherAlphaColor( ChartComponentView.Line1.Color, ChartComponentView.Line1.AdditionalColor );
    }

    /// <summary>
    /// Get from two colors' RGBA which has the Lower A value
    /// </summary>
    /// <param name="sInfo"></param>
    /// <returns></returns>
    private Color LowerAlphaColor( SeriesInfo sInfo )
    {
        return ChartElementViewModel.GetHigherAlphaColor( ChartComponentView.Line2.Color, ChartComponentView.Line2.AdditionalColor );
    }

}
