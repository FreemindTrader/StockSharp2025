using DevExpress.Internal.WinApi.Windows.UI.Notifications;
using DevExpress.Utils;
using DevExpress.Xpf.Charts.Native;
using DevExpress.Xpf.Charts.Surface;
using SciChart.Charting.Common.Databinding;
using SciChart.Charting.Common.Extensions;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Numerics.CoordinateCalculators;
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

    //protected override void InternalDraw( IRenderContext2D rc, IRenderPassData renderPassData )
    //{        
    //    using ( IPen2D pen1 = rc.CreatePen( this.Stroke, this.AntiAliasing, ( float ) this.StrokeThickness, this.Opacity, ( double[ ] ) null, PenLineCap.Round ) )
    //    {
    //        using ( IPen2D pen2 = rc.CreatePen( this.Series1Color, this.AntiAliasing, ( float ) this.StrokeThickness, this.Opacity, ( double[ ] ) null, PenLineCap.Round ) )
    //            this.DrawBands( pen1, pen2, rc, renderPassData );
    //    }
    //}

    //private void DrawBands( IPen2D series1Pen, IPen2D series2Pen, IRenderContext2D renderContext, IRenderPassData renderPassData )
    //{
    //    using ( IBrush2D brush2D = renderContext.CreateBrush( this.BandUpColor, base.Opacity, null ) )
    //    {
    //        using ( IBrush2D brush2D2 = renderContext.CreateBrush( this.BandDownColor, base.Opacity, null ) )
    //        {
    //            XyyPointSeries xyyPointSeries = (XyyPointSeries)base.CurrentRenderPassData.PointSeries;
    //            int count = xyyPointSeries.Count;
    //            ISeriesDrawingHelper seriesDrawingHelper = SeriesDrawingHelpersFactory.GetSeriesDrawingHelper(renderContext, base.CurrentRenderPassData);

    //            foreach ( FastBandRenderableSeries.Polygon polygon in this.CreateBandPolygons( renderContext, renderPassData, xyyPointSeries.YPoints, brush2D, xyyPointSeries.Y1Points, brush2D2 ) )
    //            {
    //                Point[] points = PointUtil.ClipPolygon(polygon.Points, renderContext.ViewportSize, 0, 0).ToArray<Point>();
    //                seriesDrawingHelper.FillPolygon( polygon.Brush, points );
    //            }

    //            using ( IPen2D pen2D = renderContext.CreatePen( base.SeriesColor, base.AntiAliasing, ( float ) base.StrokeThickness, base.Opacity, this.Series0StrokeDashArray, PenLineCap.Round ) )
    //            {
    //                FastLinesHelper.IterateLines( SeriesDrawingHelpersFactory.GetLinesPathFactory( renderContext, base.CurrentRenderPassData ), pen2D, xyyPointSeries.YPoints, base.CurrentRenderPassData.XCoordinateCalculator, base.CurrentRenderPassData.YCoordinateCalculator, this.IsDigitalLine, base.DrawNaNAs == LineDrawMode.ClosedLines );
    //            }

    //            using ( IPen2D pen2D2 = renderContext.CreatePen( this.Series1Color, base.AntiAliasing, ( float ) base.StrokeThickness, base.Opacity, this.Series1StrokeDashArray, PenLineCap.Round ) )
    //            {
    //                FastLinesHelper.IterateLines( SeriesDrawingHelpersFactory.GetLinesPathFactory( renderContext, base.CurrentRenderPassData ), pen2D2, xyyPointSeries.Y1Points, base.CurrentRenderPassData.XCoordinateCalculator, base.CurrentRenderPassData.YCoordinateCalculator, this.IsDigitalLine, base.DrawNaNAs == LineDrawMode.ClosedLines );
    //            }

    //            IPointMarker pointMarker = base.GetPointMarker();
    //            if ( pointMarker != null )
    //            {
    //                bool flag       = pointMarker.Stroke == base.SeriesColor;
    //                bool flag2      = pointMarker.Fill == base.SeriesColor;
    //                IPen2D pen      = flag ? series1Pen : null;
    //                IPen2D pen2     = flag ? series2Pen : null;
    //                IBrush2D brush  = flag2 ? brush2D : null;
    //                IBrush2D brush2 = flag2 ? brush2D2 : null;

    //                for ( int i = 0; i < count; i++ )
    //                {
    //                    GenericPoint2D<XyySeriesPoint> genericPoint2D = xyyPointSeries[i] as GenericPoint2D<XyySeriesPoint>;

    //                    double x   = genericPoint2D.X;
    //                    double y   = genericPoint2D.YValues.Y0;
    //                    double y2  = genericPoint2D.YValues.Y1;

    //                    float num  = (float)renderPassData.XCoordinateCalculator.GetCoordinate(x);
    //                    float num2 = (float)renderPassData.YCoordinateCalculator.GetCoordinate(y);
    //                    float num3 = (float)renderPassData.YCoordinateCalculator.GetCoordinate(y2);

    //                    seriesDrawingHelper.DrawPoint( pointMarker, new Point( ( double ) num, ( double ) num2 ), brush, pen );
    //                    seriesDrawingHelper.DrawPoint( pointMarker, new Point( ( double ) num, ( double ) num3 ), brush2, pen2 );
    //                }
    //            }
    //        }
    //    }
    //}

    //protected virtual IList<FastBandRenderableSeries.Polygon> CreateBandPolygons( IRenderContext2D renderContext, IRenderPassData renderPassData, IPointSeries yPointSeries, IBrush2D yBrush, IPointSeries y1PointSeries, IBrush2D y1Brush )
    //{
    //    Guard.NotNull( yPointSeries, nameof( yPointSeries ) );
    //    Guard.NotNull( y1PointSeries, nameof( y1PointSeries ) );
    //    Guard.ArrayLengthsSame( yPointSeries.Count, nameof( yPointSeries ), y1PointSeries.Count, nameof( y1PointSeries ) );
    //    List<FastBandRenderableSeries.Polygon> polygonList = new List<FastBandRenderableSeries.Polygon>();
    //    int count = yPointSeries.Count;
    //    List<Point> polygonPoints = new List<Point>(32);
    //    int index1 = 0;
    //    int startIndex = FastBandRenderableSeries.SkipNans(yPointSeries, y1PointSeries, index1, count);
    //    for ( int endIndex = startIndex + 1; endIndex < count; ++endIndex )
    //    {
    //        double x1 = yPointSeries[endIndex - 1].X;
    //        double y1 = yPointSeries[endIndex - 1].Y;
    //        double y2 = y1PointSeries[endIndex - 1].Y;
    //        double x2 = yPointSeries[endIndex].X;
    //        double y3 = yPointSeries[endIndex].Y;
    //        double y4 = y1PointSeries[endIndex].Y;

    //        if ( ( y3.IsNaN( ) ? 1 : ( y4.IsNaN( ) ? 1 : 0 ) ) != 0 )
    //        {
    //            this.StartPolygon( startIndex, endIndex, yPointSeries, renderPassData, polygonPoints );
    //            this.FinishPolygon( startIndex, endIndex, y1PointSeries, renderPassData, polygonPoints );
    //            polygonList.Add( new FastBandRenderableSeries.Polygon( polygonPoints.ToArray( ), y1 > y2 ? yBrush : y1Brush ) );
    //            int index2 = endIndex + 1;
    //            endIndex = FastBandRenderableSeries.SkipNans( yPointSeries, y1PointSeries, index2, count );
    //            if ( endIndex < count )
    //            {
    //                startIndex = endIndex;
    //                polygonPoints = new List<Point>( 32 );
    //            }
    //            else
    //                break;
    //        }
    //        else
    //        {
    //            Point intersectionPoint;
    //            if ( PointUtil.LineSegmentsIntersection2D( new PointUtil.Line( x1, y1, x2, y3 ), new PointUtil.Line( x1, y2, x2, y4 ), out intersectionPoint ) )
    //            {
    //                ICoordinateCalculator<double> xcoordinateCalculator = renderPassData.XCoordinateCalculator;
    //                ICoordinateCalculator<double> ycoordinateCalculator = renderPassData.YCoordinateCalculator;
    //                bool isVerticalChart = renderPassData.IsVerticalChart;
    //                float coordinate1 = (float) xcoordinateCalculator.GetCoordinate(x2);
    //                float coordinate2 = (float) xcoordinateCalculator.GetCoordinate(x1);
    //                Point point = new Point(xcoordinateCalculator.IsCategoryAxisCalculator ? (double) coordinate2 + ((double) coordinate1 - (double) coordinate2) * (intersectionPoint.X - x1) : xcoordinateCalculator.GetCoordinate(intersectionPoint.X), ycoordinateCalculator.GetCoordinate(intersectionPoint.Y));
    //                this.StartPolygon( startIndex, endIndex, yPointSeries, renderPassData, polygonPoints );
    //                if ( this.IsDigitalLine )
    //                {
    //                    polygonPoints.Add( this.TransformPoint( new Point( ( double ) coordinate1, ycoordinateCalculator.GetCoordinate( y1 ) ), isVerticalChart ) );
    //                    polygonPoints.Add( this.TransformPoint( new Point( ( double ) coordinate1, ycoordinateCalculator.GetCoordinate( y2 ) ), isVerticalChart ) );
    //                }
    //                else
    //                    polygonPoints.Add( this.TransformPoint( point, isVerticalChart ) );
    //                this.FinishPolygon( startIndex, endIndex, y1PointSeries, renderPassData, polygonPoints );
    //                polygonList.Add( new FastBandRenderableSeries.Polygon( polygonPoints.ToArray( ), y1 > y2 ? yBrush : y1Brush ) );
    //                startIndex = endIndex;
    //                polygonPoints = new List<Point>( 32 );
    //                if ( !this.IsDigitalLine )
    //                    polygonPoints.Add( this.TransformPoint( point, isVerticalChart ) );
    //            }
    //        }
    //    }
    //    if ( count > 0 )
    //    {
    //        IPoint point1 = yPointSeries[count - 1];
    //        IPoint point2 = y1PointSeries[count - 1];
    //        if ( !point1.Y.IsNaN( ) && !point2.Y.IsNaN( ) )
    //        {
    //            this.StartPolygon( startIndex, count, yPointSeries, renderPassData, polygonPoints );
    //            this.FinishPolygon( startIndex, count, y1PointSeries, renderPassData, polygonPoints );
    //            IBrush2D brush2D = point1.Y > point2.Y ? yBrush : y1Brush;
    //            polygonList.Add( new FastBandRenderableSeries.Polygon( polygonPoints.ToArray( ), brush2D ) );
    //        }
    //    }
    //    return ( IList<FastBandRenderableSeries.Polygon> ) polygonList;
    //}


    protected override bool GetIsValidForDrawing( )
    {
        if ( !base.GetIsValidForDrawing( ) )
            return false;

        return Stroke.A        != 0 && StrokeThickness > 0 || 
               Series1Color.A  != 0 && StrokeThickness > 0 || 
               BandDownColor.A != 0 || 
               BandUpColor.A   != 0 || 
               PointMarker     != null;
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
