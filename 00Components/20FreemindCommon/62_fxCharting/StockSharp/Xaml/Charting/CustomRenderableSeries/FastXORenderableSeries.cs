using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Numerics.CoordinateCalculators;
using SciChart.Charting.Visuals.PaletteProviders;
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Drawing.Common;
using StockSharp.Xaml.Charting.Definitions;
using System;
using System.Linq;
using System.Windows;

namespace StockSharp.Xaml.Charting
{
    public class FastXORenderableSeries : FastCandlestickRenderableSeries
    {
        public static readonly DependencyProperty XOBoxSizeProperty = DependencyProperty.Register( nameof( XOBoxSize ), typeof( double ), typeof( FastXORenderableSeries ), new PropertyMetadata( 0.0, new PropertyChangedCallback( OnInvalidateParentSurface ) ) );

        private IPen2D _upWickPen;
        private IPen2D _downWickPen;

        public double XOBoxSize
        {
            get
            {
                return ( double )GetValue( XOBoxSizeProperty );
            }
            set
            {
                SetValue( XOBoxSizeProperty, value );
            }
        }

        public FastXORenderableSeries( )
        {
            DefaultStyleKey = typeof( FastXORenderableSeries );
            SetCurrentValue( FastOhlcRenderableSeries.DataPointWidthProperty, 1.0 );
        }

        protected override void InternalDraw( IRenderContext2D renderContext, IRenderPassData renderPassData )
        {
            AssertPointSeriesType< OhlcPointSeries >( "OhlcDataSeries" );

            using( var penManager = new PenManager( renderContext, AntiAliasing, StrokeThickness, Opacity, null ) )
            {
                _upWickPen   = penManager.GetPen( StrokeUp );
                _downWickPen = penManager.GetPen( StrokeDown );

                renderContext.SetPrimitivesCachingEnabled( true );
                //if( Class775.smethod_0( this.ResamplingMode, renderPassData.PointRange, ( int )renderContext.ViewportSize.Width ) )
                //{
                //    this.method_10( renderContext, renderPassData, ( Interface0 )drawCache );
                //}
                //else
                //{
                //    this.method_11( renderContext, renderPassData, ( Interface0 )drawCache );
                //}
                renderContext.SetPrimitivesCachingEnabled( false );
            }
        }

        //protected override void DrawVanilla( IRenderContext2D renderContext, IRenderPassData renderPassData, IPenManager penManager )
        //{
        //    bool isVerticalChart = renderPassData.IsVerticalChart;
        //    IPointSeries pointSeries = this.CurrentRenderPassData.PointSeries;
        //    double xoBoxSize = this.XOBoxSize;
        //    int count = pointSeries.Count;
        //    if( count == 1 || xoBoxSize <= 0.0 )
        //    {
        //        return;
        //    }

        //    ICoordinateCalculator< double > xcoordinateCalculator = renderPassData.XCoordinateCalculator;
        //    ICoordinateCalculator< double > ycoordinateCalculator = renderPassData.YCoordinateCalculator;
        //    int datapointWidth = this.GetDatapointWidth( xcoordinateCalculator, pointSeries, this.DataPointWidth );
        //    double num1 = Math.Abs( ycoordinateCalculator.GetCoordinate( xoBoxSize ) - ycoordinateCalculator.GetCoordinate( 0.0 ) );
        //    bool flag1 = ( double )datapointWidth < 3.0 || num1 < 3.0;
        //    IPaletteProvider paletteProvider1 = this.PaletteProvider;
        //    ISeriesDrawingHelper seriesDrawingHelper = SeriesDrawingHelpersFactory.GetSeriesDrawingHelper( renderContext, this.CurrentRenderPassData );
        //    IBrush2D brush1 = renderContext.CreateBrush( this.UpWickColor, this.Opacity, new bool?( ) );
        //    IBrush2D brush2 = renderContext.CreateBrush( this.DownWickColor, this.Opacity, new bool?( ) );
        //    for( int index = 0; index < count; ++index )
        //    {
        //        GenericPoint2D< OhlcSeriesPoint > ohlcPoint = ( GenericPoint2D< OhlcSeriesPoint > )pointSeries[ index ];
        //        OhlcSeriesPoint yvalues1 = ohlcPoint.YValues;
        //        double dataValue1 = NumberUtil.RoundUp( yvalues1.High, xoBoxSize );
        //        yvalues1 = ohlcPoint.YValues;
        //        double dataValue2 = NumberUtil.RoundDown( yvalues1.Low, xoBoxSize );
        //        yvalues1 = ohlcPoint.YValues;
        //        double close1 = yvalues1.Close;
        //        yvalues1 = ohlcPoint.YValues;
        //        double open1 = yvalues1.Open;
        //        bool flag2 = close1 >= open1;
        //        int intValue1 = renderPassData.XCoordinateCalculator.GetCoordinate( ohlcPoint.X ).ClipToIntValue( );
        //        int intValue2 = ( ( double )intValue1 - ( double )datapointWidth * 0.5 ).ClipToIntValue( );
        //        int intValue3 = ( ( double )intValue1 + ( double )datapointWidth * 0.5 ).ClipToIntValue( );
        //        int intValue4 = ycoordinateCalculator.GetCoordinate( dataValue1 ).ClipToIntValue( );
        //        int intValue5 = ycoordinateCalculator.GetCoordinate( dataValue2 ).ClipToIntValue( );
        //        IPen2D wickPen = flag2 ? this._upWickPen : this._downWickPen;
        //        IBrush2D brush = flag2 ? brush1 : brush2;
        //        paletteProvider1.Do< IPaletteProvider >( ( Action< IPaletteProvider > )( pp =>
        //        {
        //            IPaletteProvider paletteProvider2 = pp;
        //            double x = ohlcPoint.X;
        //            OhlcSeriesPoint yvalues = ohlcPoint.YValues;
        //            double open = yvalues.Open;
        //            yvalues = ohlcPoint.YValues;
        //            double high = yvalues.High;
        //            yvalues = ohlcPoint.YValues;
        //            double low = yvalues.Low;
        //            yvalues = ohlcPoint.YValues;
        //            double close = yvalues.Close;
        //            Color? nullable = paletteProvider2.OverrideColor( ( IRenderableSeries )this, x, open, high, low, close );
        //            if( !nullable.HasValue )
        //            {
        //                return;
        //            }

        //            wickPen = penManager.GetPen( nullable.Value );
        //            brush = renderContext.CreateBrush( nullable.Value, this.Opacity, new bool?( ) );
        //        } ) );
        //        int num2 = Math.Max( intValue5, intValue4 );
        //        int num3 = Math.Min( intValue5, intValue4 );
        //        if( flag1 )
        //        {
        //            renderContext.FillRectangle( brush, this.TransformPoint( new Point( ( double )intValue2, ( double )intValue5 ), isVerticalChart ), this.TransformPoint( new Point( ( double )intValue3, ( double )intValue4 ), isVerticalChart ), 0.0 );
        //        }
        //        else if( flag2 )
        //        {
        //            double y1 = ( double )num2;
        //            while( y1 > ( double )num3 )
        //            {
        //                double y2 = Math.Max( y1 - num1, ( double )num3 );
        //                if( y1 - y2 >= 3.0 )
        //                {
        //                    seriesDrawingHelper.DrawLine( this.TransformPoint( new Point( ( double )intValue2, y1 ), isVerticalChart ), this.TransformPoint( new Point( ( double )intValue3, y2 ), isVerticalChart ), wickPen );
        //                    seriesDrawingHelper.DrawLine( this.TransformPoint( new Point( ( double )intValue3, y1 ), isVerticalChart ), this.TransformPoint( new Point( ( double )intValue2, y2 ), isVerticalChart ), wickPen );
        //                }
        //                y1 -= num1;
        //            }
        //        }
        //        else
        //        {
        //            double num4 = ( double )num3;
        //            while( num4 < ( double )num2 )
        //            {
        //                double height = Math.Min( num4 + num1, ( double )num2 ) - num4;
        //                if( height >= 3.0 )
        //                {
        //                    renderContext.DrawEllipse( wickPen, ( IBrush2D )null, new Point( ( double )intValue1, num4 + height / 2.0 ), ( double )datapointWidth, height );
        //                }

        //                num4 += num1;
        //            }
        //        }
        //    }
        //}
    }
}
