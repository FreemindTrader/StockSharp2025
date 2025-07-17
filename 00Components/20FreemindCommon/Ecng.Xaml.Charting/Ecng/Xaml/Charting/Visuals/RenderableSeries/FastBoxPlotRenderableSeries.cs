// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.RenderableSeries.FastBoxPlotRenderableSeries
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\StockSharp.Xaml.Charting.dll

using System;
using System.Windows;
using System.Windows.Media;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Licensing;
using StockSharp.Xaml.Charting.Model.DataSeries;
using StockSharp.Xaml.Charting.Rendering.Common;
using StockSharp.Xaml.Licensing.Core;

namespace StockSharp.Xaml.Charting.Visuals.RenderableSeries
{
    [UltrachartLicenseProvider( typeof( RenderableSeriesUltrachartLicenseProvider ) )]
    public class FastBoxPlotRenderableSeries : BaseRenderableSeries
    {
        public static readonly DependencyProperty DataPointWidthProperty = DependencyProperty.Register(nameof (DataPointWidth), typeof (double), typeof (FastBoxPlotRenderableSeries), new PropertyMetadata((object) 0.2, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty BodyBrushProperty = DependencyProperty.Register(nameof (BodyBrush), typeof (Brush), typeof (FastBoxPlotRenderableSeries), new PropertyMetadata((object) new SolidColorBrush(Color.FromArgb((byte) 0, (byte) 0, (byte) 0, (byte) 0)), new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        private int _barWidth;

        public FastBoxPlotRenderableSeries()
        {
            DefaultStyleKey = ( object ) typeof( FastBoxPlotRenderableSeries );
        }

        public virtual double DataPointWidth
        {
            get
            {
                return ( double ) GetValue( FastBoxPlotRenderableSeries.DataPointWidthProperty );
            }
            set
            {
                SetValue( FastBoxPlotRenderableSeries.DataPointWidthProperty, ( object ) value );
            }
        }

        public Brush BodyBrush
        {
            get
            {
                return ( Brush ) GetValue( FastBoxPlotRenderableSeries.BodyBrushProperty );
            }
            set
            {
                SetValue( FastBoxPlotRenderableSeries.BodyBrushProperty, ( object ) value );
            }
        }

        protected override HitTestInfo HitTestInternal( Point rawPoint, double hitTestRadius, bool interpolate )
        {
            HitTestInfo nearestHitPoint1 = base.HitTestInternal(rawPoint, hitTestRadius, false);
            HitTestInfo nearestHitPoint2 = HitTestSeriesWithBody(rawPoint, nearestHitPoint1, hitTestRadius);

            double num = CurrentRenderPassData.IsVerticalChart ? Math.Abs(nearestHitPoint2.HitTestPoint.Y - rawPoint.Y) : Math.Abs(nearestHitPoint2.HitTestPoint.X - rawPoint.X);

            if ( !nearestHitPoint2.IsWithinDataBounds )
            {
                bool flag = num < GetSeriesBodyWidth(nearestHitPoint2) / DataPointWidth / 2.0;
                nearestHitPoint2.IsWithinDataBounds = nearestHitPoint2.IsVerticalHit = flag;
            }
            return nearestHitPoint2;
        }

        public override SeriesInfo GetSeriesInfo( HitTestInfo hitTestInfo )
        {
            return ( SeriesInfo ) new BoxPlotSeriesInfo( ( IRenderableSeries ) this, hitTestInfo );
        }

        protected override double GetSeriesBodyWidth( HitTestInfo nearestHitPoint )
        {
            return ( double ) _barWidth;
        }

        protected override double GetSeriesBodyLowerDataBound( HitTestInfo nearestHitPoint )
        {
            return nearestHitPoint.Minimum.ToDouble();
        }

        protected override double GetSeriesBodyUpperDataBound( HitTestInfo nearestHitPoint )
        {
            return nearestHitPoint.Maximum.ToDouble();
        }

        protected override bool GetIsValidForDrawing()
        {
            if ( !base.GetIsValidForDrawing() )
                return false;
            if ( SeriesColor.A == ( byte ) 0 || StrokeThickness <= 0 )
                return BodyBrush != null;
            return true;
        }

        protected override void InternalDraw( IRenderContext2D renderContext, IRenderPassData renderPassData )
        {
            bool isVerticalChart = renderPassData.IsVerticalChart;
            double gradientRotationAngle = 0.0;
            AssertDataPointType<BoxSeriesPoint>( "BoxDataSeries" );
            BoxPointSeries pointSeries = CurrentRenderPassData.PointSeries as BoxPointSeries;
            int count = pointSeries.Count;
            _barWidth = GetDatapointWidth( renderPassData.XCoordinateCalculator, ( IPointSeries ) pointSeries, DataPointWidth );
            using ( IPen2D pen1 = renderContext.CreatePen( SeriesColor, AntiAliasing, ( float ) StrokeThickness, Opacity, ( double[ ] ) null, PenLineCap.Round ) )
            {
                using ( IPen2D pen2 = renderContext.CreatePen( SeriesColor, AntiAliasing, ( float ) ( StrokeThickness + 1 ), Opacity, ( double[ ] ) null, PenLineCap.Round ) )
                {
                    using ( IBrush2D brush = renderContext.CreateBrush( BodyBrush, Opacity, TextureMappingMode.PerPrimitive ) )
                    {
                        ISeriesDrawingHelper seriesDrawingHelper = SeriesDrawingHelpersFactory.GetSeriesDrawingHelper(renderContext, CurrentRenderPassData);
                        for ( int index = 0 ; index < count ; ++index )
                        {
                            GenericPoint2D<BoxSeriesPoint> genericPoint2D = pointSeries[index] as GenericPoint2D<BoxSeriesPoint>;
                            double x1 = genericPoint2D.X;
                            double min = genericPoint2D.YValues.Min;
                            BoxSeriesPoint yvalues1 = genericPoint2D.YValues;
                            double lowerQuartile = yvalues1.LowerQuartile;
                            yvalues1 = genericPoint2D.YValues;
                            double y = yvalues1.Y;
                            BoxSeriesPoint yvalues2 = genericPoint2D.YValues;
                            double upperQuartile = yvalues2.UpperQuartile;
                            yvalues2 = genericPoint2D.YValues;
                            double max = yvalues2.Max;
                            if ( !double.IsNaN( min ) && !double.IsNaN( lowerQuartile ) && ( !double.IsNaN( y ) && !double.IsNaN( upperQuartile ) ) && !double.IsNaN( max ) )
                            {
                                double coordinate1 = renderPassData.XCoordinateCalculator.GetCoordinate(x1);
                                double coordinate2 = renderPassData.YCoordinateCalculator.GetCoordinate(max);
                                double coordinate3 = renderPassData.YCoordinateCalculator.GetCoordinate(upperQuartile);
                                double coordinate4 = renderPassData.YCoordinateCalculator.GetCoordinate(y);
                                double coordinate5 = renderPassData.YCoordinateCalculator.GetCoordinate(lowerQuartile);
                                double coordinate6 = renderPassData.YCoordinateCalculator.GetCoordinate(min);
                                double num = (double) _barWidth * 0.5;
                                double x2 = coordinate1 - num;
                                double x3 = coordinate1 + num;
                                seriesDrawingHelper.DrawLine( TransformPoint( new Point( x2, coordinate2 ), isVerticalChart ), TransformPoint( new Point( x3, coordinate2 ), isVerticalChart ), pen1 );
                                seriesDrawingHelper.DrawLine( TransformPoint( new Point( coordinate1, coordinate2 ), isVerticalChart ), TransformPoint( new Point( coordinate1, coordinate3 ), isVerticalChart ), pen1 );
                                seriesDrawingHelper.DrawLine( TransformPoint( new Point( coordinate1, coordinate5 ), isVerticalChart ), TransformPoint( new Point( coordinate1, coordinate6 ), isVerticalChart ), pen1 );
                                seriesDrawingHelper.DrawLine( TransformPoint( new Point( x2, coordinate6 ), isVerticalChart ), TransformPoint( new Point( x3, coordinate6 ), isVerticalChart ), pen1 );
                                seriesDrawingHelper.DrawBox( TransformPoint( new Point( x2, coordinate5 ), isVerticalChart ), TransformPoint( new Point( x3, coordinate3 ), isVerticalChart ), brush, pen1, gradientRotationAngle );
                                if ( StrokeThickness > 0 )
                                    seriesDrawingHelper.DrawLine( TransformPoint( new Point( x2 + 1.0, coordinate4 ), isVerticalChart ), TransformPoint( new Point( x3, coordinate4 ), isVerticalChart ), pen2 );
                            }
                        }
                    }
                }
            }
        }
    }
}
