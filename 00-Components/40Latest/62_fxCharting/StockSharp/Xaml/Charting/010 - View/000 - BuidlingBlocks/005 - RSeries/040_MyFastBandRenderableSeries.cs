using DevExpress.Internal.WinApi.Windows.UI.Notifications;
using DevExpress.Xpf.Charts.Native;
using SciChart.Charting.Common.Databinding;
using SciChart.Charting.Common.Extensions;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Visuals.PointMarkers;
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Charting.Visuals.RenderableSeries.DrawingProviders;
using SciChart.Charting.Visuals.RenderableSeries.HitTesters;
using SciChart.Data.Model;
using SciChart.Drawing.Common;
using SciChart.Drawing.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using Color = System.Windows.Media.Color;


namespace StockSharp.Xaml.Charting;

public class MyFastBandRenderableSeries : FastBandRenderableSeries
{
    public MyFastBandRenderableSeries( )
    {
        HitTestProvider = new MyBandSeriesHitTestProvider( this );


        DefaultStyleKey = ( object ) typeof( MyFastBandRenderableSeries );
    }


    [TypeConverter( typeof( StringToDoubleArrayTypeConverter ) )]
    public double[ ] Series0StrokeDashArray
    {
        get
        {
            return ( double[ ] ) GetValue( BaseRenderableSeries.StrokeDashArrayProperty );
        }
        set
        {
            SetValue( BaseRenderableSeries.StrokeDashArrayProperty, ( object ) value );
        }
    }

    [TypeConverter( typeof( StringToDoubleArrayTypeConverter ) )]
    public double[ ] Series1StrokeDashArray
    {
        get
        {
            return ( double[ ] ) GetValue( FastBandRenderableSeries.StrokeDashArrayY1Property );
        }
        set
        {
            SetValue( FastBandRenderableSeries.StrokeDashArrayY1Property, ( object ) value );
        }
    }

    //
    // Summary:
    //     Gets or sets the Stroke of the Y1 line. For the Y0 line, use Stroke
    public Color Series1Color
    {
        get
        {
            return ( Color ) GetValue( FastBandRenderableSeries.StrokeY1Property );
        }
        set
        {
            SetValue( FastBandRenderableSeries.StrokeY1Property, ( object ) value );
        }
    }

    //
    // Summary:
    //     Gets or sets the Color of the shaded area when Y1 is less than Y0
    public Color BandDownColor
    {
        get
        {
            return ( Color ) GetValue( FastBandRenderableSeries.FillProperty );
        }
        set
        {
            SetValue( FastBandRenderableSeries.FillProperty, ( object ) value );
        }
    }

    //
    // Summary:
    //     Gets or sets the Color of the shaded area when Y1 is greater than Y0
    public Color BandUpColor
    {
        get
        {
            return ( Color ) GetValue( FastBandRenderableSeries.FillY1Property );
        }
        set
        {
            SetValue( FastBandRenderableSeries.FillY1Property, ( object ) value );
        }
    }

    //public FrameworkElement RolloverMarker1
    //{
    //    get
    //    {
    //        return RolloverMarkerY1;
    //    }
    //    private set
    //    {
    //        RolloverMarkerY1 = value;
    //    }
    //}

    protected override void InternalDraw( IRenderContext2D rc, IRenderPassData renderPassData )
    {
        base.InternalDraw( rc, renderPassData );


    }

    



    public class MyBandSeriesHitTestProvider : DefaultHitTestProvider<MyFastBandRenderableSeries>
    {
        public MyBandSeriesHitTestProvider( MyFastBandRenderableSeries renderSeries ) : base( renderSeries )
        {
        }

        protected override HitTestInfo HitTestInternal( System.Windows.Point rawPoint, double hitTestRadius, bool interpolate )
        {
            var ht = base.HitTestInternal( rawPoint, GetHitTestRadiusConsideringPointMarkerSize( hitTestRadius ), interpolate );

            ht.DataSeriesType = DataSeriesType.Xyy;

            var ds = RenderableSeries.DataSeries;

            if ( interpolate && !ht.IsHit )
            {
                var htData         = GetHitDataValue( rawPoint );
                var lower_X        = ds.XValues.FindIndex( ds.IsSorted, htData.Item1, SearchMode.RoundDown );
                var upper_X        = ds.XValues.FindIndex( ds.IsSorted, htData.Item1, SearchMode.RoundUp );
                var lower_X_Value  = ((IComparable)ds.XValues[lower_X]).ToDouble();
                var upper_X_Value  = ((IComparable)ds.XValues[upper_X]).ToDouble();
                var lower_Y_pt     = new System.Windows.Point(lower_X_Value, ((IComparable)ds.YValues[lower_X]).ToDouble());
                var upper_Y_pt     = new System.Windows.Point(upper_X_Value, ((IComparable)ds.YValues[upper_X]).ToDouble());
                int isYNan         = double.IsNaN(lower_Y_pt.Y) ? 1 : (double.IsNaN(upper_Y_pt.Y) ? 1 : 0);
                var lower_Y1_pt    = new System.Windows.Point(lower_X_Value, ((IComparable) ((IXyyDataSeries)ds).Y1Values[lower_X]).ToDouble());
                var upper_Y1_pt    = new System.Windows.Point(upper_X_Value, ((IComparable) ((IXyyDataSeries)ds).Y1Values[upper_X]).ToDouble());
                int isY1Nan        = double.IsNaN(lower_Y1_pt.Y) ? (true ? 1 : 0) : (double.IsNaN(upper_Y1_pt.Y) ? 1 : 0);
                if ( ( isYNan | isY1Nan ) != 0 )
                    return ht;

                var hitPt = new System.Windows.Point(htData.Item1.ToDouble(), htData.Item2.ToDouble());

                ht.IsHit = !PointUtil.LineSegmentsIntersection2D( new PointUtil.Line( lower_Y_pt, upper_Y_pt ), new PointUtil.Line( lower_Y1_pt, upper_Y1_pt ), out var intersectionPt ) ?
                            PointUtil.IsPointInTriangle( hitPt, lower_Y_pt, lower_Y1_pt, intersectionPt ) || PointUtil.IsPointInTriangle( hitPt, upper_Y_pt, upper_Y1_pt, intersectionPt ) :
                            PointUtil.IsPointInTriangle( hitPt, lower_Y_pt, upper_Y_pt, lower_Y1_pt ) || PointUtil.IsPointInTriangle( hitPt, lower_Y1_pt, upper_Y1_pt, upper_Y_pt );

            }

            return ht;
        }

        protected override HitTestInfo NearestHitResult( System.Windows.Point mouseRawPoint, double hitTestRadiusInPixels, SearchMode searchMode, bool considerYCoordinateForDistanceCalculation )
        {
            var ht = base.NearestHitResult( mouseRawPoint, hitTestRadiusInPixels, searchMode, considerYCoordinateForDistanceCalculation );

            if ( !ht.IsEmpty( ) )
            {
                var isVerticalChart = RenderableSeries.CurrentRenderPassData.IsVerticalChart;
                var yCalc           = RenderableSeries.CurrentRenderPassData.YCoordinateCalculator;
                var x               = isVerticalChart ? ht.HitTestPoint.Y : ht.HitTestPoint.X;
                var dataValue       = ht.Y1Value.ToDouble();
                var coordinate      = yCalc.GetCoordinate(dataValue);
                var logicHt         = DrawingHelper2025.NormalizePoint(new System.Windows.Point(x, coordinate), isVerticalChart);
                ht.Y1HitTestPoint = logicHt;
                ht.IsHit = ht.IsHit || PointUtil.Distance( logicHt, mouseRawPoint ) < hitTestRadiusInPixels;
            }

            return ht;
        }

        protected override HitTestInfo InterpolatePoint( System.Windows.Point rawPoint, HitTestInfo nearest, double hitTestRadius )
        {
            var newNeareast = new HitTestInfo()
            {
                XValue                 = nearest.XValue,
                YValue                 = nearest.Y1Value,
                HitTestPoint           = nearest.Y1HitTestPoint,
                DataSeriesIndex        = nearest.DataSeriesIndex
            };

            var ds                     = RenderableSeries.DataSeries;

            var nearestY               = GetPrevAndNextYValues(nearest.DataSeriesIndex, (i => ((IComparable) ds.YValues[i]).ToDouble()));
            var nearestY_IP            = InterpolatePoint(rawPoint, nearest, hitTestRadius, nearestY, null);
            var newNearestY            = GetPrevAndNextYValues(newNeareast.DataSeriesIndex, (i => ((IComparable) ((IXyyDataSeries) ds ).Y1Values[i]).ToDouble()));
            var newNearestY_IP         = InterpolatePoint(rawPoint, newNeareast, hitTestRadius, newNearestY, null);

            nearestY_IP.Y1Value        = newNearestY_IP.YValue;
            nearestY_IP.Y1HitTestPoint = newNearestY_IP.HitTestPoint;
            nearestY_IP.IsHit          = nearestY_IP.IsHit || newNearestY_IP.IsHit;

            return nearestY_IP;
        }
    }
}





//public override object PointSeriesArg
//{
//    get
//    {
//        return (object) OhlcDrawMode;
//    }
//}

//protected override void OnSeriesColorChanged()
//{
//}

//protected override bool GetIsValidForDrawing()
//{
//    if(!base.GetIsValidForDrawing())
//        return false;
//    if(SeriesColor.A == (byte) 0 || StrokeThickness <= 0)
//        return PointMarker != null;
//    return true;
//}

//protected override HitTestInfo ToHitTestInfoImpl(int nearestDataPointIndex)
//{
//    HitTestInfo hitTestInfoImpl = base.ToHitTestInfoImpl(nearestDataPointIndex);
//    if(hitTestInfoImpl.DataSeriesType != DataSeriesType.Ohlc && hitTestInfoImpl.DataSeriesType != DataSeriesType.Hlc)
//        return hitTestInfoImpl;
//    hitTestInfoImpl.DataSeriesType = DataSeriesType.Xy;
//    switch(OhlcDrawMode)
//    {
//        case OhlcLineDrawMode.Open:
//            hitTestInfoImpl.YValue = hitTestInfoImpl.OpenValue;
//            break;
//        case OhlcLineDrawMode.High:
//            hitTestInfoImpl.YValue = hitTestInfoImpl.HighValue;
//            break;
//        case OhlcLineDrawMode.Low:
//            hitTestInfoImpl.YValue = hitTestInfoImpl.LowValue;
//            break;
//        case OhlcLineDrawMode.Close:
//            hitTestInfoImpl.YValue = hitTestInfoImpl.CloseValue;
//            break;
//    }
//    return hitTestInfoImpl;
//}

//protected override void InternalDraw(IRenderContext2D renderContext, IRenderPassData renderPassData)
//{
//    IPointSeries pointSeries = CurrentRenderPassData.PointSeries;
//    int num = PaletteProvider != null ? 1 : 0;
//    Color lineColor = SeriesColor;
//    IPathContextFactory linesPathFactory = SeriesDrawingHelpersFactory.GetLinesPathFactory(
//        renderContext,
//        CurrentRenderPassData);
//    if(num != 0)
//    {
//        PenManager penManager = new PenManager(
//            renderContext,
//            AntiAliasing,
//            (float) StrokeThickness,
//            Opacity,
//            StrokeDashArray);
//        try
//        {
//            Func<double, double, IPen2D> func = (Func<double, double, IPen2D>) ((x, y) => penManager.GetPen(
//                PaletteProvider.GetColor((IRenderableSeries) this, x, y) ?? lineColor));
//            FastLinesHelper.DrawLines(
//                linesPathFactory,
//                (Func<double, double, IPathColor>) func,
//                pointSeries,
//                CurrentRenderPassData.XCoordinateCalculator,
//                CurrentRenderPassData.YCoordinateCalculator,
//                IsDigitalLine,
//                DrawNaNAs == LineDrawMode.ClosedLines);
//        } finally
//        {
//            if(penManager != null)
//                penManager.Dispose();
//        }
//    } else
//    {
//        using(IPen2D pen = renderContext.CreatePen(
//            SeriesColor,
//            AntiAliasing,
//            (float) StrokeThickness,
//            Opacity,
//            StrokeDashArray,
//            PenLineCap.Round))
//            FastLinesHelper.DrawLines(
//                linesPathFactory,
//                pen,
//                pointSeries,
//                CurrentRenderPassData.XCoordinateCalculator,
//                CurrentRenderPassData.YCoordinateCalculator,
//                IsDigitalLine,
//                DrawNaNAs == LineDrawMode.ClosedLines);
//    }
//    IPointMarker pointMarker = GetPointMarker();
//    if(pointMarker == null)
//        return;
//    FastPointsHelper.DrawPoints(
//        SeriesDrawingHelpersFactory.GetPointMarkerPathFactory(
//            renderContext,
//            CurrentRenderPassData,
//            pointMarker),
//        pointSeries,
//        CurrentRenderPassData.XCoordinateCalculator,
//        CurrentRenderPassData.YCoordinateCalculator);
//}
//}
