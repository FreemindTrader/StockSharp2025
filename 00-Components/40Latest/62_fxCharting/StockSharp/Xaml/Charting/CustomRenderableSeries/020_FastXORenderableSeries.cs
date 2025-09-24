using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Numerics.CoordinateCalculators;
using SciChart.Charting.Visuals.PaletteProviders;
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Charting2D.Interop;
using SciChart.Data.Model;
using SciChart.Data.Numerics.PointResamplers;
using SciChart.Drawing.Common;
using StockSharp.Xaml.Charting.Definitions;
using System;
using System.Linq;
using System.Windows;

namespace StockSharp.Xaml.Charting;

/// <summary>
/// The following FastXORenderableSeries class indeed is a Point and Figure Chart implementation.
/// </summary>
public class FastXORenderableSeries : MyFastCandlestickRenderableSeries
{
    /// <summary>
    /// The dependency property for <see cref="XOBoxSize"/> property. This defines the size of the X and O to be drawn.
    /// </summary>
    public static readonly DependencyProperty XOBoxSizeProperty = DependencyProperty.Register(nameof(XOBoxSize), typeof(double), typeof(FastXORenderableSeries), new PropertyMetadata(0.0, new PropertyChangedCallback(OnInvalidateParentSurface)));


    public double XOBoxSize
    {
        get
        {
            return ( double ) GetValue( XOBoxSizeProperty );
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


    protected override void DrawVanilla( IRenderContext2D rc, IRenderPassData renderPassData, IPenManager penManager )
    {
        bool isVerticalChart = renderPassData.IsVerticalChart;

        OhlcPointSeries ohlc = renderPassData.PointSeries as OhlcPointSeries;

        if ( ohlc.Count == 1 || XOBoxSize <= 0.0 )
        {
            return;
        }

        var xCalc      = renderPassData.XCoordinateCalculator;
        var yCalc      = renderPassData.YCoordinateCalculator;

        // Returns number of pixels of a single bar in associated SciChart.Charting.Visuals.RenderableSeries.IRenderableSeries.
        var boxWidth   = DataPointWidthProvider.GetDataPointWidth();

        // The number of pixels to draw one UNIT of X or O
        var boxHeight  = Math.Abs(yCalc.GetCoordinate(XOBoxSize) - yCalc.GetCoordinate(0.0));

        var isSmall    = (double)boxWidth < 3.0 || boxHeight < 3.0;

        var drawHelper = SeriesDrawingHelpersFactory.GetSeriesDrawingHelper(rc, this.CurrentRenderPassData);
        var upBrush    = rc.CreateBrush(this.StrokeUp, this.Opacity, null);
        var downBrush  = rc.CreateBrush(this.StrokeDown, this.Opacity, null);

        for ( int index = 0; index < ohlc.Count; ++index )
        {
            var high     = ohlc.HighValues[index];
            var low      = ohlc.LowValues[index];
            var open     = ohlc.OpenValues[index];
            var close    = ohlc.CloseValues[index];
            var xValue   = ohlc.XValues[index];
            var yValue   = ohlc.YValues[index];

            var highXO   = NumberUtil.RoundUp(high, XOBoxSize);
            var lowXO    = NumberUtil.RoundDown(low, XOBoxSize);
            var isRising = close >= open;

            int Xi       = xCalc.GetCoordinate(xValue).ClipToIntValue();
            int minXi    = ((double)Xi - (double)boxWidth * 0.5).ClipToIntValue();
            int maxXi    = ((double)Xi + (double)boxWidth * 0.5).ClipToIntValue();
            int YHighXo  = yCalc.GetCoordinate(highXO).ClipToIntValue();
            int YLowXo   = yCalc.GetCoordinate(lowXO).ClipToIntValue();
            var wickPen  = isRising ? this._upWickPen : this._downWickPen;
            var brush    = isRising ? upBrush : downBrush;

            Maybe.Do<IPaletteProviderSS>( XxxPaletteProvider,
                                                                ( Action<IPaletteProviderSS> ) ( pp =>
                                                                {
                                                                    var orColor = pp.OverrideColor((IRenderableSeries)this, xValue, open, high, low, close);
                                                                    if ( !orColor.HasValue )
                                                                    {
                                                                        return;
                                                                    }

                                                                    wickPen = penManager.GetPen( orColor.Value, null );
                                                                    brush = rc.CreateBrush( orColor.Value, this.Opacity, new bool?( ) );
                                                                } ) );
            int maxY = Math.Max(YLowXo, YHighXo);
            int minY = Math.Min(YLowXo, YHighXo);

            if ( isSmall )
            {
                rc.FillRectangle( brush, DrawingHelper2025.NormalizePoint( new Point( ( double ) minXi, ( double ) YLowXo ), isVerticalChart ), DrawingHelper2025.NormalizePoint( new Point( ( double ) maxXi, ( double ) YHighXo ), isVerticalChart ), 0.0 );
            }
            else if ( isRising )
            {
                double topY = (double)maxY;

                while ( topY > ( double ) minY )
                {
                    double bottomY = Math.Max(topY - boxHeight, (double)minY);

                    if ( topY - bottomY >= 3.0 )
                    {
                        // The following draws a X on the chart
                        drawHelper.DrawLine( DrawingHelper2025.NormalizePoint( new Point( ( double ) minXi, topY ), isVerticalChart ), DrawingHelper2025.NormalizePoint( new Point( ( double ) maxXi, bottomY ), isVerticalChart ), wickPen );

                        drawHelper.DrawLine( DrawingHelper2025.NormalizePoint( new Point( ( double ) maxXi, topY ), isVerticalChart ), DrawingHelper2025.NormalizePoint( new Point( ( double ) minXi, bottomY ), isVerticalChart ), wickPen );
                    }

                    topY -= boxHeight;
                }
            }
            else
            {
                double bottomY = (double)minY;

                while ( bottomY < ( double ) maxY )
                {
                    double boxH = Math.Min(bottomY + boxHeight, (double)maxY) - bottomY;

                    if ( boxH >= 3.0 )
                    {
                        // The following draws a O on the chart
                        rc.DrawEllipse( wickPen, ( IBrush2D ) null, new Point( ( double ) Xi, bottomY + boxH / 2.0 ), ( double ) boxWidth, boxH );
                    }

                    bottomY += boxHeight;
                }
            }
        }
    }
}
