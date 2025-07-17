// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.RenderableSeries.BaseColumnRenderableSeries
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\StockSharp.Xaml.Charting.dll

using System;
using System.Windows;
using System.Windows.Media;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Licensing;
using StockSharp.Xaml.Charting.Model.DataSeries;
using StockSharp.Xaml.Charting.Numerics;
using StockSharp.Xaml.Charting.Rendering.Common;
using StockSharp.Xaml.Charting.StrategyManager;
using StockSharp.Xaml.Charting.Utility;
using StockSharp.Xaml.Licensing.Core;

namespace StockSharp.Xaml.Charting.Visuals.RenderableSeries
{
    [UltrachartLicenseProvider( typeof( RenderableSeriesUltrachartLicenseProvider ) )]
    public abstract class BaseColumnRenderableSeries : BaseRenderableSeries
    {
        [Obsolete("We're sorry! FastColumnRenderableSeries.FillColor is obsolete, please use FillBrush instead", true)]
        public static readonly DependencyProperty FillColorProperty = DependencyProperty.Register(nameof (FillColor), typeof (Color), typeof (BaseColumnRenderableSeries), new PropertyMetadata((object) ColorConstants.White, new PropertyChangedCallback(BaseColumnRenderableSeries.OnRenderablePropertyChangedStatic)));
        public static readonly DependencyProperty FillBrushProperty = DependencyProperty.Register(nameof (FillBrush), typeof (Brush), typeof (BaseColumnRenderableSeries), new PropertyMetadata((object) new SolidColorBrush(ColorConstants.White), new PropertyChangedCallback(BaseColumnRenderableSeries.OnRenderablePropertyChangedStatic)));
        public static readonly DependencyProperty FillBrushMappingModeProperty = DependencyProperty.Register(nameof (FillBrushMappingMode), typeof (TextureMappingMode), typeof (BaseColumnRenderableSeries), new PropertyMetadata((object) TextureMappingMode.PerPrimitive, new PropertyChangedCallback(BaseColumnRenderableSeries.OnRenderablePropertyChangedStatic)));
        public static readonly DependencyProperty UniformWidthProperty = DependencyProperty.Register(nameof (UseUniformWidth), typeof (bool), typeof (BaseColumnRenderableSeries), new PropertyMetadata((object) false, new PropertyChangedCallback(BaseColumnRenderableSeries.OnRenderablePropertyChangedStatic)));
        public static readonly DependencyProperty DataPointWidthProperty = DependencyProperty.Register(nameof (DataPointWidth), typeof (double), typeof (BaseColumnRenderableSeries), new PropertyMetadata((object) 0.4, new PropertyChangedCallback(BaseColumnRenderableSeries.OnRenderablePropertyChangedStatic)));
        private double[] _precalculatedPoints;
        protected double _minColumnWidth;

        protected BaseColumnRenderableSeries()
        {
            SetCurrentValue( BaseRenderableSeries.ResamplingModeProperty, ( object ) ResamplingMode.Max );
        }

        [Obsolete( "We're sorry! FastColumnRenderableSeries.FillColor is obsolete, please use FillBrush instead", true )]
        public Color FillColor
        {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public Brush FillBrush
        {
            get
            {
                return ( Brush ) GetValue( BaseColumnRenderableSeries.FillBrushProperty );
            }
            set
            {
                SetValue( BaseColumnRenderableSeries.FillBrushProperty, ( object ) value );
            }
        }

        public TextureMappingMode FillBrushMappingMode
        {
            get
            {
                return ( TextureMappingMode ) GetValue( BaseColumnRenderableSeries.FillBrushMappingModeProperty );
            }
            set
            {
                SetValue( BaseColumnRenderableSeries.FillBrushMappingModeProperty, ( object ) value );
            }
        }

        public virtual double DataPointWidth
        {
            get
            {
                return ( double ) GetValue( BaseColumnRenderableSeries.DataPointWidthProperty );
            }
            set
            {
                SetValue( BaseColumnRenderableSeries.DataPointWidthProperty, ( object ) value );
            }
        }

        public bool UseUniformWidth
        {
            get
            {
                return ( bool ) GetValue( BaseColumnRenderableSeries.UniformWidthProperty );
            }
            set
            {
                SetValue( BaseColumnRenderableSeries.UniformWidthProperty, ( object ) value );
            }
        }

        public new double ZeroLineY
        {
            get
            {
                return ( double ) GetValue( BaseRenderableSeries.ZeroLineYProperty );
            }
            set
            {
                SetValue( BaseRenderableSeries.ZeroLineYProperty, ( object ) value );
            }
        }

        public override HitTestInfo HitTest( Point rawPoint, double hitTestRadius, bool interpolate = false )
        {
            HitTestInfo hitTestInfo = HitTestInfo.Empty;
            if ( IsVisible && CurrentRenderPassData != null )
            {
                HitTestInfo nearestHitPoint = base.HitTest(rawPoint, hitTestRadius, interpolate);
                ITransformationStrategy transformationStrategy = CurrentRenderPassData.TransformationStrategy;
                rawPoint = transformationStrategy.Transform( rawPoint );
                nearestHitPoint.HitTestPoint = transformationStrategy.Transform( nearestHitPoint.HitTestPoint );
                nearestHitPoint.Y1HitTestPoint = transformationStrategy.Transform( nearestHitPoint.Y1HitTestPoint );
                hitTestInfo = HitTestSeriesWithBody( rawPoint, nearestHitPoint, hitTestRadius );
                hitTestInfo.HitTestPoint = transformationStrategy.ReverseTransform( hitTestInfo.HitTestPoint );
                hitTestInfo.Y1HitTestPoint = transformationStrategy.ReverseTransform( hitTestInfo.Y1HitTestPoint );
            }
            return hitTestInfo;
        }

        protected override HitTestInfo HitTestInternal( Point rawPoint, double hitTestRadius, bool interpolate )
        {
            HitTestInfo nearestHitPoint = HitTestInfo.Empty;
            if ( IsVisible )
                nearestHitPoint = NearestHitResult( rawPoint, GetHitTestRadiusConsideringPointMarkerSize( hitTestRadius ), SearchMode.Nearest, false );
            double num = CurrentRenderPassData.IsVerticalChart ? Math.Abs(nearestHitPoint.HitTestPoint.Y - rawPoint.Y) : Math.Abs(nearestHitPoint.HitTestPoint.X - rawPoint.X);
            if ( !nearestHitPoint.IsWithinDataBounds )
            {
                bool flag = num < GetSeriesBodyWidth(nearestHitPoint) / DataPointWidth / 2.0;
                nearestHitPoint.IsWithinDataBounds = nearestHitPoint.IsVerticalHit = flag;
            }
            return nearestHitPoint;
        }

        protected override double GetSeriesBodyWidth( HitTestInfo nearestHitPoint )
        {
            double dataPointXCoord = CurrentRenderPassData.IsVerticalChart ? nearestHitPoint.HitTestPoint.Y : nearestHitPoint.HitTestPoint.X;
            return ( double ) GetColumnWidth( nearestHitPoint.DataSeriesIndex, dataPointXCoord );
        }

        private int GetColumnWidth( int dataPointIndex, double dataPointXCoord )
        {
            double num = _minColumnWidth;
            int index = dataPointIndex - 1;
            if ( index > 0 && !UseUniformWidth && !double.IsNaN( CurrentRenderPassData.YCoordinateCalculator.GetCoordinate( ( ( IComparable ) DataSeries.YValues[ index ] ).ToDouble() ) ) )
            {
                double coordinate = CurrentRenderPassData.XCoordinateCalculator.GetCoordinate((XAxis.IsCategoryAxis ? (IComparable) index : (IComparable) DataSeries.XValues[index]).ToDouble());
                num = Math.Abs( ( dataPointXCoord - coordinate ) * DataPointWidth );
            }
            return ( int ) num;
        }

        protected override void InternalDraw( IRenderContext2D renderContext, IRenderPassData renderPassData )
        {
            IPointSeries pointSeries = CurrentRenderPassData.PointSeries;
            int count = pointSeries.Count;
            double columnWidth = GetColumnWidth(pointSeries, renderPassData);
            int yzeroCoord = (int) GetYZeroCoord();
            using ( PenManager penManager = new PenManager( renderContext, AntiAliasing, ( float ) StrokeThickness, Opacity, ( double[ ] ) null ) )
            {
                IBrush2D brush = CreateBrush(renderContext, FillBrush, Opacity, FillBrushMappingMode);
                renderContext.DisposeResourceAfterDraw( ( IDisposable ) brush );
                renderContext.SetPrimitvesCachingEnabled( true );
                if ( columnWidth > 1.0 )
                {
                    DrawAsColumns( renderContext, pointSeries, count, yzeroCoord, renderPassData, columnWidth, brush, PaletteProvider, ( IPenManager ) penManager );
                }
                else
                {
                    if ( StrokeThickness <= 0 || SeriesColor.A == ( byte ) 0 )
                        return;
                    DrawAsLines( renderContext, pointSeries, count, yzeroCoord, renderPassData, PaletteProvider, ( IPenManager ) penManager );
                }
            }
        }

        private IBrush2D CreateBrush( IRenderContext2D renderContext, Brush fillBrush, double opacity, TextureMappingMode fillBrushMappingMode )
        {
            if ( FillBrush is SolidColorBrush )
                return renderContext.CreateBrush( ( fillBrush as SolidColorBrush ).Color, opacity, new bool?() );
            return renderContext.CreateBrush( fillBrush, opacity, fillBrushMappingMode );
        }

        protected virtual double GetColumnWidth( IPointSeries points, IRenderPassData renderPassData )
        {
            return ( double ) GetDatapointWidth( renderPassData.XCoordinateCalculator, points, DataPointWidth );
        }

        protected virtual double GetNonUniformColumnWidth( IPointSeries points, IRenderPassData renderPassData, double prevCoord, double xCenter, int pointsIndex )
        {
            double num = double.NaN;
            if ( double.IsNaN( prevCoord ) )
            {
                int index = pointsIndex + 1;
                if ( index < points.Count && !double.IsNaN( points[ index ].Y ) )
                    num = ( renderPassData.XCoordinateCalculator.GetCoordinate( points[ index ].X ) - xCenter ) * DataPointWidth;
            }
            else
                num = ( xCenter - prevCoord ) * DataPointWidth;
            return num;
        }

        private void DrawAsColumns( IRenderContext2D renderContext, IPointSeries points, int setCount, int zeroY, IRenderPassData renderPassData, double columnWidthPixels, IBrush2D fillBrush, IPaletteProvider paletteProvider, IPenManager penManager )
        {
            IPen2D pen1 = penManager.GetPen(SeriesColor);
            bool isVerticalChart = renderPassData.IsVerticalChart;
            double chartRotationAngle = GetChartRotationAngle(renderPassData);
            ISeriesDrawingHelper seriesDrawingHelper = SeriesDrawingHelpersFactory.GetSeriesDrawingHelper(renderContext, CurrentRenderPassData);
            int length = points.Count * 5;
            int num1 = 0;
            if ( _precalculatedPoints == null || _precalculatedPoints.Length != length )
                _precalculatedPoints = new double[ length ];
            IPen2D pointPen = pen1;
            IBrush2D pointFill = fillBrush;
            _minColumnWidth = columnWidthPixels;
            double yBottom;
            for ( int dataPointIndex = 0 ; dataPointIndex < setCount ; ++dataPointIndex )
            {
                double xCenter;
                double yTop;
                if ( GetColumnCenterTopAndBottom( dataPointIndex, renderPassData, zeroY, out xCenter, out yTop, out yBottom ) )
                {
                    double num2 = _minColumnWidth * 0.5;
                    if ( xCenter + num2 <= ( double ) int.MaxValue && yTop <= ( double ) int.MaxValue )
                    {
                        double[] precalculatedPoints1 = _precalculatedPoints;
                        int index1 = num1;
                        int num3 = index1 + 1;
                        double num4 = (double) dataPointIndex;
                        precalculatedPoints1[ index1 ] = num4;
                        double[] precalculatedPoints2 = _precalculatedPoints;
                        int index2 = num3;
                        int num5 = index2 + 1;
                        double num6 = xCenter;
                        precalculatedPoints2[ index2 ] = num6;
                        double[] precalculatedPoints3 = _precalculatedPoints;
                        int index3 = num5;
                        int num7 = index3 + 1;
                        double num8 = yTop;
                        precalculatedPoints3[ index3 ] = num8;
                        double[] precalculatedPoints4 = _precalculatedPoints;
                        int index4 = num7;
                        int num9 = index4 + 1;
                        double num10 = yBottom;
                        precalculatedPoints4[ index4 ] = num10;
                        double[] precalculatedPoints5 = _precalculatedPoints;
                        int index5 = num9;
                        num1 = index5 + 1;
                        double num11 = num2;
                        precalculatedPoints5[ index5 ] = num11;
                    }
                }
            }
            double num12 = _minColumnWidth / 2.0;
            int num13 = 0;
            while ( num13 < num1 )
            {
                double[] precalculatedPoints1 = _precalculatedPoints;
                int index1 = num13;
                int num2 = index1 + 1;
                int index2 = (int) precalculatedPoints1[index1];
                double[] precalculatedPoints2 = _precalculatedPoints;
                int index3 = num2;
                int num3 = index3 + 1;
                double num4 = precalculatedPoints2[index3];
                double[] precalculatedPoints3 = _precalculatedPoints;
                int index4 = num3;
                int num5 = index4 + 1;
                double num6 = precalculatedPoints3[index4];
                double[] precalculatedPoints4 = _precalculatedPoints;
                int index5 = num5;
                int num7 = index5 + 1;
                yBottom = precalculatedPoints4[ index5 ];
                int num8 = num7;
                num13 = num8 + 1;
                int index6 = num8;
                if ( !UseUniformWidth )
                    num12 = _precalculatedPoints[ index6 ];
                double num9 = num4 + num12;
                double x1 = num4 - num12;
                double x2 = num9;
                double y1 = num6;
                double y2 = yBottom;
                Point pt1 = TransformPoint(new Point(x1, y1), isVerticalChart);
                Point pt2 = TransformPoint(new Point(x2, y2), isVerticalChart);
                if ( paletteProvider != null )
                {
                    IPoint point = points[index2];
                    Color? color = paletteProvider.GetColor((IRenderableSeries) this, point.X, point.Y);
                    if ( color.HasValue )
                    {
                        IPen2D pen2 = penManager.GetPen(color.Value);
                        using ( IBrush2D brush = renderContext.CreateBrush( color.Value, 1.0, new bool?() ) )
                        {
                            seriesDrawingHelper.DrawBox( pt1, pt2, brush, pen2, chartRotationAngle );
                            continue;
                        }
                    }
                }
                seriesDrawingHelper.DrawBox( pt1, pt2, pointFill, pointPen, chartRotationAngle );
            }
        }

        protected virtual bool GetColumnCenterTopAndBottom( int dataPointIndex, IRenderPassData renderPassData, int zeroY, out double xCenter, out double yTop, out double yBottom )
        {
            xCenter = 0.0;
            yTop = 0.0;
            yBottom = 0.0;
            IPoint point = renderPassData.PointSeries[dataPointIndex];
            int num = double.IsNaN(point.Y) ? 0 : (Math.Abs(point.Y - ZeroLineY) >= double.Epsilon ? 1 : 0);
            if ( num == 0 )
                return num != 0;
            xCenter = renderPassData.XCoordinateCalculator.GetCoordinate( point.X );
            yTop = renderPassData.YCoordinateCalculator.GetCoordinate( point.Y );
            yBottom = ( double ) zeroY;
            if ( yTop >= yBottom )
                return num != 0;
            NumberUtil.Swap( ref yBottom, ref yTop );
            return num != 0;
        }

        private void DrawAsLines( IRenderContext2D renderContext, IPointSeries points, int setCount, int zeroY, IRenderPassData renderPassData, IPaletteProvider paletteProvider, IPenManager penManager )
        {
            IPen2D pen1 = penManager.GetPen(SeriesColor);
            _minColumnWidth = ( double ) pen1.StrokeThickness;
            bool isVerticalChart = renderPassData.IsVerticalChart;
            IPen2D pointPen = pen1;
            ISeriesDrawingHelper seriesDrawingHelper = SeriesDrawingHelpersFactory.GetSeriesDrawingHelper(renderContext, CurrentRenderPassData);
            for ( int dataPointIndex = 0 ; dataPointIndex < setCount ; ++dataPointIndex )
            {
                IPoint point = points[dataPointIndex];
                double xCenter;
                double yTop;
                double yBottom;
                if ( GetColumnCenterTopAndBottom( dataPointIndex, renderPassData, zeroY, out xCenter, out yTop, out yBottom ) )
                {
                    int num1 = (int) xCenter;
                    int num2 = (int) yTop;
                    int num3 = (int) yBottom;
                    Point pt1 = TransformPoint(new Point((double) num1, (double) num2), isVerticalChart);
                    Point pt2 = TransformPoint(new Point((double) num1, (double) num3), isVerticalChart);
                    if ( paletteProvider != null )
                    {
                        Color? color = paletteProvider.GetColor((IRenderableSeries) this, point.X, point.Y);
                        if ( color.HasValue )
                        {
                            using ( IPen2D pen2 = penManager.GetPen( color.Value ) )
                            {
                                seriesDrawingHelper.DrawLine( pt1, pt2, pen2 );
                                continue;
                            }
                        }
                    }
                    seriesDrawingHelper.DrawLine( pt1, pt2, pointPen );
                }
            }
        }

        private static void OnRenderablePropertyChangedStatic( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            BaseRenderableSeries.OnInvalidateParentSurface( d, e );
        }
    }
}
