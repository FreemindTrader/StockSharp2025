//// Ecng.Xaml.Charting.Visuals.RenderableSeries.FastBandRenderableSeries
//using Ecng.Xaml.Charting.Common.Databinding;
//using Ecng.Xaml.Charting.Common.Extensions;
//using Ecng.Xaml.Charting.Licensing;
//using Ecng.Xaml.Charting.Model.DataSeries;
//using Ecng.Xaml.Charting.Numerics.CoordinateCalculators;
//using Ecng.Xaml.Charting.Rendering.Common;
//using Ecng.Xaml.Charting.Utility;
//using Ecng.Xaml.Charting.Visuals.PointMarkers;
//using Ecng.Xaml.Charting.Visuals.RenderableSeries;
//using StockSharp.Xaml.Licensing.Core;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Windows;
//using System.Windows.Media;

//namespace Ecng.Xaml.Charting.Visuals.RenderableSeries
//{
//    
//    public class FastBandRenderableSeries : BaseRenderableSeries
//    {
//        protected struct Polygon
//        {
//            private readonly Point[] _points;

//            private readonly IBrush2D _brush2D;

//            public IBrush2D Brush => _brush2D;

//            public Point[ ] Points => _points;

//            public Polygon( Point[ ] points, IBrush2D brush2D )
//            {
//                this = default( Polygon );
//                _points = points;
//                _brush2D = brush2D;
//            }
//        }

//        public static readonly DependencyProperty IsDigitalLineProperty = DependencyProperty.Register("IsDigitalLine", typeof(bool), typeof(FastBandRenderableSeries), new PropertyMetadata(false, BaseRenderableSeries.OnInvalidateParentSurface));

//        public static readonly DependencyProperty Series1ColorProperty = DependencyProperty.Register("Series1Color", typeof(Color), typeof(FastBandRenderableSeries), new PropertyMetadata(default(Color), BaseRenderableSeries.OnInvalidateParentSurface));

//        public static readonly DependencyProperty BandUpColorProperty = DependencyProperty.Register("BandUpColor", typeof(Color), typeof(FastBandRenderableSeries), new PropertyMetadata(default(Color), BaseRenderableSeries.OnInvalidateParentSurface));

//        public static readonly DependencyProperty BandDownColorProperty = DependencyProperty.Register("BandDownColor", typeof(Color), typeof(FastBandRenderableSeries), new PropertyMetadata(default(Color), BaseRenderableSeries.OnInvalidateParentSurface));

//        public static readonly DependencyProperty Series0StrokeDashArrayProperty = DependencyProperty.Register("Series0StrokeDashArray", typeof(double[]), typeof(FastBandRenderableSeries), new PropertyMetadata(null, BaseRenderableSeries.OnInvalidateParentSurface));

//        public static readonly DependencyProperty Series1StrokeDashArrayProperty = DependencyProperty.Register("Series1StrokeDashArray", typeof(double[]), typeof(FastBandRenderableSeries), new PropertyMetadata(null, BaseRenderableSeries.OnInvalidateParentSurface));

//        private FrameworkElement _rolloverMarker1Cache;

//        public bool IsDigitalLine
//        {
//            get
//            {
//                return ( bool ) GetValue( IsDigitalLineProperty );
//            }
//            set
//            {
//                SetValue( IsDigitalLineProperty, value );
//            }
//        }

//        [TypeConverter( typeof( StringToDoubleArrayTypeConverter ) )]
//        public double[ ] Series0StrokeDashArray
//        {
//            get
//            {
//                return ( double[ ] ) GetValue( Series0StrokeDashArrayProperty );
//            }
//            set
//            {
//                SetValue( Series0StrokeDashArrayProperty, value );
//            }
//        }

//        [TypeConverter( typeof( StringToDoubleArrayTypeConverter ) )]
//        public double[ ] Series1StrokeDashArray
//        {
//            get
//            {
//                return ( double[ ] ) GetValue( Series1StrokeDashArrayProperty );
//            }
//            set
//            {
//                SetValue( Series1StrokeDashArrayProperty, value );
//            }
//        }

//        public Color Series1Color
//        {
//            get
//            {
//                return ( Color ) GetValue( Series1ColorProperty );
//            }
//            set
//            {
//                SetValue( Series1ColorProperty, value );
//            }
//        }

//        public Color BandDownColor
//        {
//            get
//            {
//                return ( Color ) GetValue( BandDownColorProperty );
//            }
//            set
//            {
//                SetValue( BandDownColorProperty, value );
//            }
//        }

//        public Color BandUpColor
//        {
//            get
//            {
//                return ( Color ) GetValue( BandUpColorProperty );
//            }
//            set
//            {
//                SetValue( BandUpColorProperty, value );
//            }
//        }

//        public FrameworkElement RolloverMarker1
//        {
//            get
//            {
//                return _rolloverMarker1Cache;
//            }
//            private set
//            {
//                _rolloverMarker1Cache = value;
//            }
//        }

//        public FastBandRenderableSeries( )
//        {
//            base.DefaultStyleKey = typeof( FastBandRenderableSeries );
//        }

//        protected override void CreateRolloverMarker( )
//        {
//            base.CreateRolloverMarker();
//            RolloverMarker1 = Ecng.Xaml.Charting.Visuals.RenderableSeries.PointMarker.CreateFromTemplate( base.RolloverMarkerTemplate, this );
//        }

//        protected override HitTestInfo HitTestInternal( Point rawPoint, double hitTestRadius, bool interpolate )
//        {
//            HitTestInfo result = base.HitTestInternal(rawPoint, GetHitTestRadiusConsideringPointMarkerSize(hitTestRadius), interpolate);
//            result.DataSeriesType = DataSeriesType.Xyy;
//            if ( interpolate && !result.IsHit )
//            {
//                Tuple<IComparable, IComparable> hitDataValue = GetHitDataValue(rawPoint);
//                int index = base.DataSeries.XValues.FindIndex(base.DataSeries.IsSorted, hitDataValue.Item1, SearchMode.RoundDown);
//                int index2 = base.DataSeries.XValues.FindIndex(base.DataSeries.IsSorted, hitDataValue.Item1, SearchMode.RoundUp);
//                double x = ((IComparable)base.DataSeries.XValues[index]).ToDouble();
//                double x2 = ((IComparable)base.DataSeries.XValues[index2]).ToDouble();
//                Point point = new Point(x, ((IComparable)base.DataSeries.YValues[index]).ToDouble());
//                Point point2 = new Point(x2, ((IComparable)base.DataSeries.YValues[index2]).ToDouble());
//                bool flag = double.IsNaN(point.Y) || double.IsNaN(point2.Y);
//                Point point3 = new Point(x, ((IComparable)((IXyyDataSeries)base.DataSeries).Y1Values[index]).ToDouble());
//                Point point4 = new Point(x2, ((IComparable)((IXyyDataSeries)base.DataSeries).Y1Values[index2]).ToDouble());
//                bool flag2 = double.IsNaN(point3.Y) || double.IsNaN(point4.Y);
//                if ( flag | flag2 )
//                {
//                    return result;
//                }
//                Point checkPt = new Point(hitDataValue.Item1.ToDouble(), hitDataValue.Item2.ToDouble());
//                PointUtil.Line firstLine = new PointUtil.Line(point, point2);
//                PointUtil.Line secondLine = new PointUtil.Line(point3, point4);
//                if ( PointUtil.LineSegmentsIntersection2D( firstLine, secondLine, out Point intersectionPoint ) )
//                {
//                    result.IsHit = ( PointUtil.IsPointInTriangle( checkPt, point, point3, intersectionPoint ) || PointUtil.IsPointInTriangle( checkPt, point2, point4, intersectionPoint ) );
//                }
//                else
//                {
//                    result.IsHit = ( PointUtil.IsPointInTriangle( checkPt, point, point2, point3 ) || PointUtil.IsPointInTriangle( checkPt, point3, point4, point2 ) );
//                }
//            }
//            return result;
//        }

//        protected override HitTestInfo NearestHitResult( Point rawPoint, double hitTestRadius, SearchMode searchMode, bool considerYCoordinateForDistanceCalculation )
//        {
//            HitTestInfo result = base.NearestHitResult(rawPoint, hitTestRadius, searchMode, considerYCoordinateForDistanceCalculation);
//            if ( !result.IsEmpty() )
//            {
//                bool isVerticalChart = base.CurrentRenderPassData.IsVerticalChart;
//                ICoordinateCalculator<double> yCoordinateCalculator = base.CurrentRenderPassData.YCoordinateCalculator;
//                Point hitTestPoint;
//                double num;
//                if ( !isVerticalChart )
//                {
//                    hitTestPoint = result.HitTestPoint;
//                    num = hitTestPoint.X;
//                }
//                else
//                {
//                    hitTestPoint = result.HitTestPoint;
//                    num = hitTestPoint.Y;
//                }
//                double x = num;
//                double coordinate = yCoordinateCalculator.GetCoordinate(result.Y1Value.ToDouble());
//                Point point2 = result.Y1HitTestPoint = TransformPoint(new Point(x, coordinate), isVerticalChart);
//                result.IsHit = ( result.IsHit || PointUtil.Distance( point2, rawPoint ) < hitTestRadius );
//            }
//            return result;
//        }

//        protected override HitTestInfo InterpolatePoint( Point rawPoint, HitTestInfo nearestHitResult, double hitTestRadius )
//        {
//            HitTestInfo hitTestInfo = default(HitTestInfo);
//            hitTestInfo.XValue = nearestHitResult.XValue;
//            hitTestInfo.YValue = nearestHitResult.Y1Value;
//            hitTestInfo.HitTestPoint = nearestHitResult.Y1HitTestPoint;
//            hitTestInfo.DataSeriesIndex = nearestHitResult.DataSeriesIndex;
//            HitTestInfo nearestHitResult2 = hitTestInfo;
//            Tuple<double, double> prevAndNextYValues = GetPrevAndNextYValues(nearestHitResult.DataSeriesIndex, (int i) => ((IComparable)base.DataSeries.YValues[i]).ToDouble());
//            HitTestInfo result = InterpolatePoint(rawPoint, nearestHitResult, hitTestRadius, prevAndNextYValues, null);
//            prevAndNextYValues = GetPrevAndNextYValues( nearestHitResult2.DataSeriesIndex, ( int i ) => ( ( IComparable ) ( ( IXyyDataSeries ) base.DataSeries ).Y1Values[ i ] ).ToDouble() );
//            nearestHitResult2 = InterpolatePoint( rawPoint, nearestHitResult2, hitTestRadius, prevAndNextYValues, null );
//            result.Y1Value = nearestHitResult2.YValue;
//            result.Y1HitTestPoint = nearestHitResult2.HitTestPoint;
//            result.IsHit = ( result.IsHit || nearestHitResult2.IsHit );
//            return result;
//        }

//        protected override bool GetIsValidForDrawing( )
//        {
//            int result;
//            if ( base.GetIsValidForDrawing() )
//            {
//                Color color = base.SeriesColor;
//                if ( color.A == 0 || base.StrokeThickness <= 0 )
//                {
//                    color = Series1Color;
//                    if ( color.A == 0 || base.StrokeThickness <= 0 )
//                    {
//                        color = BandDownColor;
//                        if ( color.A == 0 )
//                        {
//                            color = BandUpColor;
//                            if ( color.A == 0 )
//                            {
//                                result = ( ( base.PointMarker != null ) ? 1 : 0 );
//                                goto IL_0069;
//                            }
//                        }
//                    }
//                }
//                result = 1;
//            }
//            else
//            {
//                result = 0;
//            }
//            goto IL_0069;
//        IL_0069:
//            return ( byte ) result != 0;
//        }

//        protected override void InternalDraw( IRenderContext2D renderContext, IRenderPassData renderPassData )
//        {
//            AssertDataPointType<XyySeriesPoint>( "XyyDataSeries" );
//            using ( IPen2D series1Pen = renderContext.CreatePen( base.SeriesColor, base.AntiAliasing, ( float ) base.StrokeThickness, base.Opacity, null, PenLineCap.Round ) )
//            {
//                using ( IPen2D series2Pen = renderContext.CreatePen( Series1Color, base.AntiAliasing, ( float ) base.StrokeThickness, base.Opacity, null, PenLineCap.Round ) )
//                {
//                    DrawBands( series1Pen, series2Pen, renderContext, renderPassData );
//                }
//            }
//        }

//        private void DrawBands( IPen2D series1Pen, IPen2D series2Pen, IRenderContext2D renderContext, IRenderPassData renderPassData )
//        {
//            using ( IBrush2D brush2D = renderContext.CreateBrush( BandUpColor, base.Opacity, null ) )
//            {
//                using ( IBrush2D brush2D2 = renderContext.CreateBrush( BandDownColor, base.Opacity, null ) )
//                {
//                    XyyPointSeries xyyPointSeries = (XyyPointSeries)base.CurrentRenderPassData.PointSeries;
//                    int count = xyyPointSeries.Count;
//                    ISeriesDrawingHelper seriesDrawingHelper = SeriesDrawingHelpersFactory.GetSeriesDrawingHelper(renderContext, base.CurrentRenderPassData);
//                    IList<Polygon> list = CreateBandPolygons(renderContext, renderPassData, xyyPointSeries.YPoints, brush2D, xyyPointSeries.Y1Points, brush2D2);
//                    foreach ( Polygon item in list )
//                    {
//                        Point[] points = PointUtil.ClipPolygon(item.Points, renderContext.ViewportSize, 0, 0).ToArray();
//                        seriesDrawingHelper.FillPolygon( item.Brush, points );
//                    }
//                    using ( IPen2D pen = renderContext.CreatePen( base.SeriesColor, base.AntiAliasing, ( float ) base.StrokeThickness, base.Opacity, Series0StrokeDashArray, PenLineCap.Round ) )
//                    {
//                        IPathContextFactory linesPathFactory = SeriesDrawingHelpersFactory.GetLinesPathFactory(renderContext, base.CurrentRenderPassData);
//                        FastLinesHelper.IterateLines( linesPathFactory, pen, xyyPointSeries.YPoints, base.CurrentRenderPassData.XCoordinateCalculator, base.CurrentRenderPassData.YCoordinateCalculator, IsDigitalLine, base.DrawNaNAs == LineDrawMode.ClosedLines );
//                    }
//                    using ( IPen2D pen2 = renderContext.CreatePen( Series1Color, base.AntiAliasing, ( float ) base.StrokeThickness, base.Opacity, Series1StrokeDashArray, PenLineCap.Round ) )
//                    {
//                        IPathContextFactory linesPathFactory2 = SeriesDrawingHelpersFactory.GetLinesPathFactory(renderContext, base.CurrentRenderPassData);
//                        FastLinesHelper.IterateLines( linesPathFactory2, pen2, xyyPointSeries.Y1Points, base.CurrentRenderPassData.XCoordinateCalculator, base.CurrentRenderPassData.YCoordinateCalculator, IsDigitalLine, base.DrawNaNAs == LineDrawMode.ClosedLines );
//                    }
//                    IPointMarker pointMarker = GetPointMarker();
//                    if ( pointMarker != null )
//                    {
//                        bool flag = pointMarker.Stroke == base.SeriesColor;
//                        bool flag2 = pointMarker.Fill == base.SeriesColor;
//                        IPen2D pen3 = flag ? series1Pen : null;
//                        IPen2D pen4 = flag ? series2Pen : null;
//                        IBrush2D brush = flag2 ? brush2D : null;
//                        IBrush2D brush2 = flag2 ? brush2D2 : null;
//                        for ( int i = 0; i < count; i++ )
//                        {
//                            GenericPoint2D<XyySeriesPoint> genericPoint2D = xyyPointSeries[i] as GenericPoint2D<XyySeriesPoint>;
//                            double x = genericPoint2D.X;
//                            XyySeriesPoint yValues = genericPoint2D.YValues;
//                            double y = yValues.Y0;
//                            yValues = genericPoint2D.YValues;
//                            double y2 = yValues.Y1;
//                            if ( y == y && y2 == y2 )
//                            {
//                                float num = (float)renderPassData.XCoordinateCalculator.GetCoordinate(x);
//                                float num2 = (float)renderPassData.YCoordinateCalculator.GetCoordinate(y);
//                                float num3 = (float)renderPassData.YCoordinateCalculator.GetCoordinate(y2);
//                                seriesDrawingHelper.DrawPoint( pointMarker, new Point( ( double ) num, ( double ) num2 ), brush, pen3 );
//                                seriesDrawingHelper.DrawPoint( pointMarker, new Point( ( double ) num, ( double ) num3 ), brush2, pen4 );
//                            }
//                        }
//                    }
//                }
//            }
//        }

//        protected virtual IList<Polygon> CreateBandPolygons( IRenderContext2D renderContext, IRenderPassData renderPassData, IPointSeries yPointSeries, IBrush2D yBrush, IPointSeries y1PointSeries, IBrush2D y1Brush )
//        {
//            Guard.NotNull( yPointSeries, "yPointSeries" );
//            Guard.NotNull( y1PointSeries, "y1PointSeries" );
//            Guard.ArrayLengthsSame( yPointSeries.Count, "yPointSeries", y1PointSeries.Count, "y1PointSeries" );
//            List<Polygon> list = new List<Polygon>();
//            int count = yPointSeries.Count;
//            List<Point> list2 = new List<Point>(32);
//            int index = 0;
//            index = SkipNans( yPointSeries, y1PointSeries, index, count );
//            for ( int i = index + 1; i < count; i++ )
//            {
//                double x = yPointSeries[i - 1].X;
//                double y = yPointSeries[i - 1].Y;
//                double y2 = y1PointSeries[i - 1].Y;
//                double x2 = yPointSeries[i].X;
//                double y3 = yPointSeries[i].Y;
//                double y4 = y1PointSeries[i].Y;
//                if ( y3.IsNaN() || y4.IsNaN() )
//                {
//                    StartPolygon( index, i, yPointSeries, renderPassData, list2 );
//                    FinishPolygon( index, i, y1PointSeries, renderPassData, list2 );
//                    list.Add( new Polygon( list2.ToArray(), ( y > y2 ) ? yBrush : y1Brush ) );
//                    i++;
//                    i = SkipNans( yPointSeries, y1PointSeries, i, count );
//                    if ( i >= count )
//                    {
//                        break;
//                    }
//                    index = i;
//                    list2 = new List<Point>( 32 );
//                }
//                else
//                {
//                    PointUtil.Line firstLine = new PointUtil.Line(x, y, x2, y3);
//                    PointUtil.Line secondLine = new PointUtil.Line(x, y2, x2, y4);
//                    if ( PointUtil.LineSegmentsIntersection2D( firstLine, secondLine, out Point intersectionPoint ) )
//                    {
//                        ICoordinateCalculator<double> xCoordinateCalculator = renderPassData.XCoordinateCalculator;
//                        ICoordinateCalculator<double> yCoordinateCalculator = renderPassData.YCoordinateCalculator;
//                        bool isVerticalChart = renderPassData.IsVerticalChart;
//                        float num = (float)xCoordinateCalculator.GetCoordinate(x2);
//                        float num2 = (float)xCoordinateCalculator.GetCoordinate(x);
//                        double x3 = xCoordinateCalculator.IsCategoryAxisCalculator ? ((double)num2 + (double)(num - num2) * (intersectionPoint.X - x)) : xCoordinateCalculator.GetCoordinate(intersectionPoint.X);
//                        double coordinate = yCoordinateCalculator.GetCoordinate(intersectionPoint.Y);
//                        Point point = new Point(x3, coordinate);
//                        StartPolygon( index, i, yPointSeries, renderPassData, list2 );
//                        if ( IsDigitalLine )
//                        {
//                            list2.Add( TransformPoint( new Point( ( double ) num, ( double ) ( float ) yCoordinateCalculator.GetCoordinate( y ) ), isVerticalChart ) );
//                            list2.Add( TransformPoint( new Point( ( double ) num, ( double ) ( float ) yCoordinateCalculator.GetCoordinate( y2 ) ), isVerticalChart ) );
//                        }
//                        else
//                        {
//                            list2.Add( TransformPoint( point, isVerticalChart ) );
//                        }
//                        FinishPolygon( index, i, y1PointSeries, renderPassData, list2 );
//                        list.Add( new Polygon( list2.ToArray(), ( y > y2 ) ? yBrush : y1Brush ) );
//                        index = i;
//                        list2 = new List<Point>( 32 );
//                        if ( !IsDigitalLine )
//                        {
//                            list2.Add( TransformPoint( point, isVerticalChart ) );
//                        }
//                    }
//                }
//            }
//            if ( count > 0 )
//            {
//                IPoint point2 = yPointSeries[count - 1];
//                IPoint point3 = y1PointSeries[count - 1];
//                if ( !point2.Y.IsNaN() && !point3.Y.IsNaN() )
//                {
//                    StartPolygon( index, count, yPointSeries, renderPassData, list2 );
//                    FinishPolygon( index, count, y1PointSeries, renderPassData, list2 );
//                    IBrush2D brush2D = (point2.Y > point3.Y) ? yBrush : y1Brush;
//                    list.Add( new Polygon( list2.ToArray(), brush2D ) );
//                }
//            }
//            return list;
//        }

//        private void StartPolygon( int startIndex, int endIndex, IPointSeries y0PointSeries, IRenderPassData renderPassData, List<Point> polygonPoints )
//        {
//            for ( int i = startIndex; i < endIndex; i++ )
//            {
//                AddPointToPolygon( y0PointSeries[ i ], polygonPoints, false, renderPassData );
//            }
//        }

//        private void FinishPolygon( int startIndex, int endIndex, IPointSeries y1PointSeries, IRenderPassData renderPassData, List<Point> polygonPoints )
//        {
//            for ( int num = endIndex - 1; num >= startIndex; num-- )
//            {
//                AddPointToPolygon( y1PointSeries[ num ], polygonPoints, true, renderPassData );
//            }
//            polygonPoints.Add( polygonPoints[ 0 ] );
//        }

//        private void AddPointToPolygon( IPoint point, List<Point> polygonPoints, bool needToInvert, IRenderPassData renderPassData )
//        {
//            ICoordinateCalculator<double> xCoordinateCalculator = renderPassData.XCoordinateCalculator;
//            ICoordinateCalculator<double> yCoordinateCalculator = renderPassData.YCoordinateCalculator;
//            bool isVerticalChart = renderPassData.IsVerticalChart;
//            float num = (float)yCoordinateCalculator.GetCoordinate(point.Y);
//            float num2 = (float)xCoordinateCalculator.GetCoordinate(point.X);
//            Point item = TransformPoint(new Point((double)num2, (double)num), isVerticalChart);
//            if ( IsDigitalLine && polygonPoints.Count > 0 )
//            {
//                Point point2 = polygonPoints[polygonPoints.Count - 1];
//                polygonPoints.Add( ( needToInvert ^ isVerticalChart ) ? new Point( point2.X, item.Y ) : new Point( item.X, point2.Y ) );
//            }
//            polygonPoints.Add( item );
//        }

//        private static int SkipNans( IPointSeries yPoints, IPointSeries y1Points, int index, int pointCount )
//        {
//            while ( index < pointCount && ( double.IsNaN( yPoints[ index ].Y ) || double.IsNaN( y1Points[ index ].Y ) ) )
//            {
//                index++;
//            }
//            return index;
//        }

//        protected override void OnDataSeriesDependencyPropertyChanged( IDataSeries oldDataSeries, IDataSeries newDataSeries )
//        {
//            if ( newDataSeries != null && !( newDataSeries is IXyyDataSeries ) )
//            {
//                throw new InvalidOperationException( $"{GetType().Name} expects a DataSeries of type {typeof( IXyyDataSeries )}. Please ensure the correct data has been bound to the Renderable Series" );
//            }
//            base.OnDataSeriesDependencyPropertyChanged( oldDataSeries, newDataSeries );
//        }
//    }
//}

// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Visuals.RenderableSeries.FastBandRenderableSeries
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Ecng.Xaml.Charting.Common.Databinding;
using Ecng.Xaml.Charting.Common.Extensions;
using Ecng.Xaml.Charting.Licensing;
using Ecng.Xaml.Charting.Model.DataSeries;
using Ecng.Xaml.Charting.Numerics.CoordinateCalculators;
using Ecng.Xaml.Charting.Rendering.Common;
using Ecng.Xaml.Charting.Utility;
using Ecng.Xaml.Charting.Visuals.PointMarkers;
using StockSharp.Xaml.Licensing.Core;

namespace Ecng.Xaml.Charting.Visuals.RenderableSeries
{
    [UltrachartLicenseProvider( typeof( RenderableSeriesUltrachartLicenseProvider ) )]
    public class FastBandRenderableSeries : BaseRenderableSeries
    {
        public static readonly DependencyProperty IsDigitalLineProperty = DependencyProperty.Register(nameof (IsDigitalLine), typeof (bool), typeof (FastBandRenderableSeries), new PropertyMetadata((object) false, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty Series1ColorProperty = DependencyProperty.Register(nameof (Series1Color), typeof (Color), typeof (FastBandRenderableSeries), new PropertyMetadata((object) new Color(), new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty BandUpColorProperty = DependencyProperty.Register(nameof (BandUpColor), typeof (Color), typeof (FastBandRenderableSeries), new PropertyMetadata((object) new Color(), new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty BandDownColorProperty = DependencyProperty.Register(nameof (BandDownColor), typeof (Color), typeof (FastBandRenderableSeries), new PropertyMetadata((object) new Color(), new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty Series0StrokeDashArrayProperty = DependencyProperty.Register(nameof (Series0StrokeDashArray), typeof (double[]), typeof (FastBandRenderableSeries), new PropertyMetadata((object) null, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty Series1StrokeDashArrayProperty = DependencyProperty.Register(nameof (Series1StrokeDashArray), typeof (double[]), typeof (FastBandRenderableSeries), new PropertyMetadata((object) null, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        private FrameworkElement _rolloverMarker1Cache;

        public FastBandRenderableSeries()
        {
            this.DefaultStyleKey = typeof( FastBandRenderableSeries );
        }

        public bool IsDigitalLine
        {
            get
            {
                return ( bool ) this.GetValue( FastBandRenderableSeries.IsDigitalLineProperty );
            }
            set
            {
                this.SetValue( FastBandRenderableSeries.IsDigitalLineProperty, value );
            }
        }

        [TypeConverter( typeof( StringToDoubleArrayTypeConverter ) )]
        public double[ ] Series0StrokeDashArray
        {
            get
            {
                return ( double[ ] ) this.GetValue( FastBandRenderableSeries.Series0StrokeDashArrayProperty );
            }
            set
            {
                this.SetValue( FastBandRenderableSeries.Series0StrokeDashArrayProperty, value );
            }
        }

        [TypeConverter( typeof( StringToDoubleArrayTypeConverter ) )]
        public double[ ] Series1StrokeDashArray
        {
            get
            {
                return ( double[ ] ) this.GetValue( FastBandRenderableSeries.Series1StrokeDashArrayProperty );
            }
            set
            {
                this.SetValue( FastBandRenderableSeries.Series1StrokeDashArrayProperty, value );
            }
        }

        public Color Series1Color
        {
            get
            {
                return ( Color ) this.GetValue( FastBandRenderableSeries.Series1ColorProperty );
            }
            set
            {
                this.SetValue( FastBandRenderableSeries.Series1ColorProperty, value );
            }
        }

        public Color BandDownColor
        {
            get
            {
                return ( Color ) this.GetValue( FastBandRenderableSeries.BandDownColorProperty );
            }
            set
            {
                this.SetValue( FastBandRenderableSeries.BandDownColorProperty, value );
            }
        }

        public Color BandUpColor
        {
            get
            {
                return ( Color ) this.GetValue( FastBandRenderableSeries.BandUpColorProperty );
            }
            set
            {
                this.SetValue( FastBandRenderableSeries.BandUpColorProperty, value );
            }
        }

        public FrameworkElement RolloverMarker1
        {
            get
            {
                return this._rolloverMarker1Cache;
            }
            private set
            {
                this._rolloverMarker1Cache = value;
            }
        }

        protected override void CreateRolloverMarker()
        {
            base.CreateRolloverMarker();
            RolloverMarker1 = Ecng.Xaml.Charting.Visuals.RenderableSeries.PointMarker.CreateFromTemplate( base.RolloverMarkerTemplate, this );
        }

        protected override HitTestInfo HitTestInternal( Point rawPoint, double hitTestRadius, bool interpolate )
        {
            HitTestInfo hitTestInfo = base.HitTestInternal(rawPoint, this.GetHitTestRadiusConsideringPointMarkerSize(hitTestRadius), interpolate);
            hitTestInfo.DataSeriesType = DataSeriesType.Xyy;
            if ( interpolate && !hitTestInfo.IsHit )
            {
                Tuple<IComparable, IComparable> hitDataValue = this.GetHitDataValue(rawPoint);
                int index1 = this.DataSeries.XValues.FindIndex(this.DataSeries.IsSorted, hitDataValue.Item1, SearchMode.RoundDown);
                int index2 = this.DataSeries.XValues.FindIndex(this.DataSeries.IsSorted, hitDataValue.Item1, SearchMode.RoundUp);
                double x1 = ((IComparable) this.DataSeries.XValues[index1]).ToDouble();
                double x2 = ((IComparable) this.DataSeries.XValues[index2]).ToDouble();
                Point point1 = new Point(x1, ((IComparable) this.DataSeries.YValues[index1]).ToDouble());
                Point point2 = new Point(x2, ((IComparable) this.DataSeries.YValues[index2]).ToDouble());
                int num1 = double.IsNaN(point1.Y) ? 1 : (double.IsNaN(point2.Y) ? 1 : 0);
                Point point3 = new Point(x1, ((IComparable) ((IXyyDataSeries) this.DataSeries).Y1Values[index1]).ToDouble());
                Point point4 = new Point(x2, ((IComparable) ((IXyyDataSeries) this.DataSeries).Y1Values[index2]).ToDouble());
                int num2 = double.IsNaN(point3.Y) ? (true ? 1 : 0) : (double.IsNaN(point4.Y) ? 1 : 0);
                if ( ( num1 | num2 ) != 0 )
                    return hitTestInfo;
                Point checkPt = new Point(hitDataValue.Item1.ToDouble(), hitDataValue.Item2.ToDouble());
                Point intersectionPoint;
                hitTestInfo.IsHit = !PointUtil.LineSegmentsIntersection2D( new PointUtil.Line( point1, point2 ), new PointUtil.Line( point3, point4 ), out intersectionPoint ) ? PointUtil.IsPointInTriangle( checkPt, point1, point2, point3 ) || PointUtil.IsPointInTriangle( checkPt, point3, point4, point2 ) : PointUtil.IsPointInTriangle( checkPt, point1, point3, intersectionPoint ) || PointUtil.IsPointInTriangle( checkPt, point2, point4, intersectionPoint );
            }
            return hitTestInfo;
        }

        protected override HitTestInfo NearestHitResult( Point rawPoint, double hitTestRadius, SearchMode searchMode, bool considerYCoordinateForDistanceCalculation )
        {
            HitTestInfo hitTestInfo = base.NearestHitResult(rawPoint, hitTestRadius, searchMode, considerYCoordinateForDistanceCalculation);
            if ( !hitTestInfo.IsEmpty() )
            {
                bool isVerticalChart = this.CurrentRenderPassData.IsVerticalChart;
                ICoordinateCalculator<double> ycoordinateCalculator = this.CurrentRenderPassData.YCoordinateCalculator;
                double x = isVerticalChart ? hitTestInfo.HitTestPoint.Y : hitTestInfo.HitTestPoint.X;
                double dataValue = hitTestInfo.Y1Value.ToDouble();
                double coordinate = ycoordinateCalculator.GetCoordinate(dataValue);
                Point point1 = this.TransformPoint(new Point(x, coordinate), isVerticalChart);
                hitTestInfo.Y1HitTestPoint = point1;
                hitTestInfo.IsHit = hitTestInfo.IsHit || PointUtil.Distance( point1, rawPoint ) < hitTestRadius;
            }
            return hitTestInfo;
        }

        protected override HitTestInfo InterpolatePoint( Point rawPoint, HitTestInfo nearestHitResult, double hitTestRadius )
        {
            HitTestInfo nearestHitResult1 = new HitTestInfo()
            {
                XValue = nearestHitResult.XValue,
                YValue = nearestHitResult.Y1Value,
                HitTestPoint = nearestHitResult.Y1HitTestPoint,
                DataSeriesIndex = nearestHitResult.DataSeriesIndex
            };
            Tuple<double, double> prevAndNextYvalues1 = this.GetPrevAndNextYValues(nearestHitResult.DataSeriesIndex, (Func<int, double>) (i => ((IComparable) this.DataSeries.YValues[i]).ToDouble()));
            HitTestInfo hitTestInfo1 = this.InterpolatePoint(rawPoint, nearestHitResult, hitTestRadius, prevAndNextYvalues1, (Tuple<double, double>) null);
            Tuple<double, double> prevAndNextYvalues2 = this.GetPrevAndNextYValues(nearestHitResult1.DataSeriesIndex, (Func<int, double>) (i => ((IComparable) ((IXyyDataSeries) this.DataSeries).Y1Values[i]).ToDouble()));
            HitTestInfo hitTestInfo2 = this.InterpolatePoint(rawPoint, nearestHitResult1, hitTestRadius, prevAndNextYvalues2, (Tuple<double, double>) null);
            hitTestInfo1.Y1Value = hitTestInfo2.YValue;
            hitTestInfo1.Y1HitTestPoint = hitTestInfo2.HitTestPoint;
            hitTestInfo1.IsHit = hitTestInfo1.IsHit || hitTestInfo2.IsHit;
            return hitTestInfo1;
        }

        protected override bool GetIsValidForDrawing()
        {
            if ( !base.GetIsValidForDrawing() )
                return false;
            if ( ( this.SeriesColor.A == ( byte ) 0 || this.StrokeThickness <= 0 ) && ( this.Series1Color.A == ( byte ) 0 || this.StrokeThickness <= 0 ) && ( this.BandDownColor.A == ( byte ) 0 && this.BandUpColor.A == ( byte ) 0 ) )
                return this.PointMarker != null;
            return true;
        }

        protected override void InternalDraw( IRenderContext2D renderContext, IRenderPassData renderPassData )
        {
            this.AssertDataPointType<XyySeriesPoint>( "XyyDataSeries" );
            using ( IPen2D pen1 = renderContext.CreatePen( this.SeriesColor, this.AntiAliasing, ( float ) this.StrokeThickness, this.Opacity, ( double[ ] ) null, PenLineCap.Round ) )
            {
                using ( IPen2D pen2 = renderContext.CreatePen( this.Series1Color, this.AntiAliasing, ( float ) this.StrokeThickness, this.Opacity, ( double[ ] ) null, PenLineCap.Round ) )
                    this.DrawBands( pen1, pen2, renderContext, renderPassData );
            }
        }

        private void DrawBands( IPen2D series1Pen, IPen2D series2Pen, IRenderContext2D renderContext, IRenderPassData renderPassData )
        {
            using ( IBrush2D brush2D = renderContext.CreateBrush( this.BandUpColor, base.Opacity, null ) )
            {
                using ( IBrush2D brush2D2 = renderContext.CreateBrush( this.BandDownColor, base.Opacity, null ) )
                {
                    XyyPointSeries xyyPointSeries = (XyyPointSeries)base.CurrentRenderPassData.PointSeries;
                    int count = xyyPointSeries.Count;
                    ISeriesDrawingHelper seriesDrawingHelper = SeriesDrawingHelpersFactory.GetSeriesDrawingHelper(renderContext, base.CurrentRenderPassData);

                    foreach ( FastBandRenderableSeries.Polygon polygon in this.CreateBandPolygons( renderContext, renderPassData, xyyPointSeries.YPoints, brush2D, xyyPointSeries.Y1Points, brush2D2 ) )
                    {
                        Point[] points = PointUtil.ClipPolygon(polygon.Points, renderContext.ViewportSize, 0, 0).ToArray<Point>();
                        seriesDrawingHelper.FillPolygon( polygon.Brush, points );
                    }

                    using ( IPen2D pen2D = renderContext.CreatePen( base.SeriesColor, base.AntiAliasing, ( float ) base.StrokeThickness, base.Opacity, this.Series0StrokeDashArray, PenLineCap.Round ) )
                    {
                        FastLinesHelper.IterateLines( SeriesDrawingHelpersFactory.GetLinesPathFactory( renderContext, base.CurrentRenderPassData ), pen2D, xyyPointSeries.YPoints, base.CurrentRenderPassData.XCoordinateCalculator, base.CurrentRenderPassData.YCoordinateCalculator, this.IsDigitalLine, base.DrawNaNAs == LineDrawMode.ClosedLines );
                    }

                    using ( IPen2D pen2D2 = renderContext.CreatePen( this.Series1Color, base.AntiAliasing, ( float ) base.StrokeThickness, base.Opacity, this.Series1StrokeDashArray, PenLineCap.Round ) )
                    {
                        FastLinesHelper.IterateLines( SeriesDrawingHelpersFactory.GetLinesPathFactory( renderContext, base.CurrentRenderPassData ), pen2D2, xyyPointSeries.Y1Points, base.CurrentRenderPassData.XCoordinateCalculator, base.CurrentRenderPassData.YCoordinateCalculator, this.IsDigitalLine, base.DrawNaNAs == LineDrawMode.ClosedLines );
                    }

                    IPointMarker pointMarker = base.GetPointMarker();
                    if ( pointMarker != null )
                    {
                        bool flag       = pointMarker.Stroke == base.SeriesColor;
                        bool flag2      = pointMarker.Fill == base.SeriesColor;
                        IPen2D pen      = flag ? series1Pen : null;
                        IPen2D pen2     = flag ? series2Pen : null;
                        IBrush2D brush  = flag2 ? brush2D : null;
                        IBrush2D brush2 = flag2 ? brush2D2 : null;

                        for ( int i = 0 ; i < count ; i++ )
                        {
                            GenericPoint2D<XyySeriesPoint> genericPoint2D = xyyPointSeries[i] as GenericPoint2D<XyySeriesPoint>;

                            double x   = genericPoint2D.X;
                            double y   = genericPoint2D.YValues.Y0;
                            double y2  = genericPoint2D.YValues.Y1;

                            float num  = (float)renderPassData.XCoordinateCalculator.GetCoordinate(x);
                            float num2 = (float)renderPassData.YCoordinateCalculator.GetCoordinate(y);
                            float num3 = (float)renderPassData.YCoordinateCalculator.GetCoordinate(y2);

                            seriesDrawingHelper.DrawPoint( pointMarker, new Point( ( double ) num, ( double ) num2 ), brush, pen );
                            seriesDrawingHelper.DrawPoint( pointMarker, new Point( ( double ) num, ( double ) num3 ), brush2, pen2 );
                        }
                    }
                }
            }
        }

        protected virtual IList<FastBandRenderableSeries.Polygon> CreateBandPolygons( IRenderContext2D renderContext, IRenderPassData renderPassData, IPointSeries yPointSeries, IBrush2D yBrush, IPointSeries y1PointSeries, IBrush2D y1Brush )
        {
            Guard.NotNull( yPointSeries, nameof( yPointSeries ) );
            Guard.NotNull( y1PointSeries, nameof( y1PointSeries ) );
            Guard.ArrayLengthsSame( yPointSeries.Count, nameof( yPointSeries ), y1PointSeries.Count, nameof( y1PointSeries ) );
            List<FastBandRenderableSeries.Polygon> polygonList = new List<FastBandRenderableSeries.Polygon>();
            int count = yPointSeries.Count;
            List<Point> polygonPoints = new List<Point>(32);
            int index1 = 0;
            int startIndex = FastBandRenderableSeries.SkipNans(yPointSeries, y1PointSeries, index1, count);
            for ( int endIndex = startIndex + 1 ; endIndex < count ; ++endIndex )
            {
                double x1 = yPointSeries[endIndex - 1].X;
                double y1 = yPointSeries[endIndex - 1].Y;
                double y2 = y1PointSeries[endIndex - 1].Y;
                double x2 = yPointSeries[endIndex].X;
                double y3 = yPointSeries[endIndex].Y;
                double y4 = y1PointSeries[endIndex].Y;

                if ( ( y3.IsNaN() ? 1 : ( y4.IsNaN() ? 1 : 0 ) ) != 0 )
                {
                    this.StartPolygon( startIndex, endIndex, yPointSeries, renderPassData, polygonPoints );
                    this.FinishPolygon( startIndex, endIndex, y1PointSeries, renderPassData, polygonPoints );
                    polygonList.Add( new FastBandRenderableSeries.Polygon( polygonPoints.ToArray(), y1 > y2 ? yBrush : y1Brush ) );
                    int index2 = endIndex + 1;
                    endIndex = FastBandRenderableSeries.SkipNans( yPointSeries, y1PointSeries, index2, count );
                    if ( endIndex < count )
                    {
                        startIndex = endIndex;
                        polygonPoints = new List<Point>( 32 );
                    }
                    else
                        break;
                }
                else
                {
                    Point intersectionPoint;
                    if ( PointUtil.LineSegmentsIntersection2D( new PointUtil.Line( x1, y1, x2, y3 ), new PointUtil.Line( x1, y2, x2, y4 ), out intersectionPoint ) )
                    {
                        ICoordinateCalculator<double> xcoordinateCalculator = renderPassData.XCoordinateCalculator;
                        ICoordinateCalculator<double> ycoordinateCalculator = renderPassData.YCoordinateCalculator;
                        bool isVerticalChart = renderPassData.IsVerticalChart;
                        float coordinate1 = (float) xcoordinateCalculator.GetCoordinate(x2);
                        float coordinate2 = (float) xcoordinateCalculator.GetCoordinate(x1);
                        Point point = new Point(xcoordinateCalculator.IsCategoryAxisCalculator ? (double) coordinate2 + ((double) coordinate1 - (double) coordinate2) * (intersectionPoint.X - x1) : xcoordinateCalculator.GetCoordinate(intersectionPoint.X), ycoordinateCalculator.GetCoordinate(intersectionPoint.Y));
                        this.StartPolygon( startIndex, endIndex, yPointSeries, renderPassData, polygonPoints );
                        if ( this.IsDigitalLine )
                        {
                            polygonPoints.Add( this.TransformPoint( new Point( ( double ) coordinate1, ycoordinateCalculator.GetCoordinate( y1 ) ), isVerticalChart ) );
                            polygonPoints.Add( this.TransformPoint( new Point( ( double ) coordinate1, ycoordinateCalculator.GetCoordinate( y2 ) ), isVerticalChart ) );
                        }
                        else
                            polygonPoints.Add( this.TransformPoint( point, isVerticalChart ) );
                        this.FinishPolygon( startIndex, endIndex, y1PointSeries, renderPassData, polygonPoints );
                        polygonList.Add( new FastBandRenderableSeries.Polygon( polygonPoints.ToArray(), y1 > y2 ? yBrush : y1Brush ) );
                        startIndex = endIndex;
                        polygonPoints = new List<Point>( 32 );
                        if ( !this.IsDigitalLine )
                            polygonPoints.Add( this.TransformPoint( point, isVerticalChart ) );
                    }
                }
            }
            if ( count > 0 )
            {
                IPoint point1 = yPointSeries[count - 1];
                IPoint point2 = y1PointSeries[count - 1];
                if ( !point1.Y.IsNaN() && !point2.Y.IsNaN() )
                {
                    this.StartPolygon( startIndex, count, yPointSeries, renderPassData, polygonPoints );
                    this.FinishPolygon( startIndex, count, y1PointSeries, renderPassData, polygonPoints );
                    IBrush2D brush2D = point1.Y > point2.Y ? yBrush : y1Brush;
                    polygonList.Add( new FastBandRenderableSeries.Polygon( polygonPoints.ToArray(), brush2D ) );
                }
            }
            return ( IList<FastBandRenderableSeries.Polygon> ) polygonList;
        }

        private void StartPolygon( int startIndex, int endIndex, IPointSeries y0PointSeries, IRenderPassData renderPassData, List<Point> polygonPoints )
        {
            for ( int index = startIndex ; index < endIndex ; ++index )
                this.AddPointToPolygon( y0PointSeries[ index ], polygonPoints, false, renderPassData );
        }

        private void FinishPolygon( int startIndex, int endIndex, IPointSeries y1PointSeries, IRenderPassData renderPassData, List<Point> polygonPoints )
        {
            for ( int index = endIndex - 1 ; index >= startIndex ; --index )
                this.AddPointToPolygon( y1PointSeries[ index ], polygonPoints, true, renderPassData );
            polygonPoints.Add( polygonPoints[ 0 ] );
        }

        private void AddPointToPolygon( IPoint point, List<Point> polygonPoints, bool needToInvert, IRenderPassData renderPassData )
        {
            ICoordinateCalculator<double> xcoordinateCalculator = renderPassData.XCoordinateCalculator;
            ICoordinateCalculator<double> ycoordinateCalculator = renderPassData.YCoordinateCalculator;
            bool isVerticalChart = renderPassData.IsVerticalChart;
            double y = point.Y;
            float coordinate = (float) ycoordinateCalculator.GetCoordinate(y);
            double x = point.X;
            Point point1 = this.TransformPoint(new Point(xcoordinateCalculator.GetCoordinate(x), (double) coordinate), isVerticalChart);
            if ( this.IsDigitalLine && polygonPoints.Count > 0 )
            {
                Point polygonPoint = polygonPoints[polygonPoints.Count - 1];
                bool flag = needToInvert ^ isVerticalChart;
                polygonPoints.Add( flag ? new Point( polygonPoint.X, point1.Y ) : new Point( point1.X, polygonPoint.Y ) );
            }
            polygonPoints.Add( point1 );
        }

        private static int SkipNans( IPointSeries yPoints, IPointSeries y1Points, int index, int pointCount )
        {
            while ( index < pointCount && ( double.IsNaN( yPoints[ index ].Y ) || double.IsNaN( y1Points[ index ].Y ) ) )
                ++index;
            return index;
        }

        protected override void OnDataSeriesDependencyPropertyChanged( IDataSeries oldDataSeries, IDataSeries newDataSeries )
        {
            if ( newDataSeries != null && !( newDataSeries is IXyyDataSeries ) )
                throw new InvalidOperationException( string.Format( "{0} expects a DataSeries of type {1}. Please ensure the correct data has been bound to the Renderable Series", this.GetType().Name, typeof( IXyyDataSeries ) ) );
            base.OnDataSeriesDependencyPropertyChanged( oldDataSeries, newDataSeries );
        }

        protected struct Polygon
        {
            private readonly Point[] _points;
            private readonly IBrush2D _brush2D;

            public Polygon( Point[ ] points, IBrush2D brush2D )
            {
                this = new FastBandRenderableSeries.Polygon();
                this._points = points;
                this._brush2D = brush2D;
            }

            public IBrush2D Brush
            {
                get
                {
                    return this._brush2D;
                }
            }

            public Point[ ] Points
            {
                get
                {
                    return this._points;
                }
            }
        }
    }
}
