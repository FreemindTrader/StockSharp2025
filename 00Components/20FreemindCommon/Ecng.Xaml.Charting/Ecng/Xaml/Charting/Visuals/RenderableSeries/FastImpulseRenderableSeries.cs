// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.FastImpulseRenderableSeries
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\fx.Xaml.Charting.dll

using System;
using System.Windows;
using System.Windows.Media;
using StockSharp.Xaml.Licensing.Core;

namespace fx.Xaml.Charting
{
    [UltrachartLicenseProvider( typeof( RenderableSeriesUltrachartLicenseProvider ) )]
    public class FastImpulseRenderableSeries : BaseRenderableSeries
    {
        private IPen2D _linePen;

        public FastImpulseRenderableSeries()
        {
            this.DefaultStyleKey = ( object ) typeof( FastImpulseRenderableSeries );
        }

        public override IRange GetYRange( IRange xRange, bool getPositiveRange )
        {
            IRange yrange = base.GetYRange(xRange, getPositiveRange);
            double zeroLineY = this.ZeroLineY;
            if ( getPositiveRange && zeroLineY <= 0.0 )
                zeroLineY = yrange.Min.ToDouble();
            return RangeFactory.NewRange( ( IComparable ) Math.Min( yrange.Min.ToDouble(), zeroLineY ), ( IComparable ) Math.Max( yrange.Max.ToDouble(), zeroLineY ) );
        }

        protected override HitTestInfo HitTestInternal( Point rawPoint, double hitTestRadius, bool interpolate )
        {
            double consideringPointMarkerSize = this.GetHitTestRadiusConsideringPointMarkerSize(hitTestRadius - (double) this.StrokeThickness / 2.0);
            HitTestInfo hitTestInfo1 = this.NearestHitResult(rawPoint, consideringPointMarkerSize, SearchMode.Nearest, true);
            if ( !hitTestInfo1.IsHit )
            {
                HitTestInfo hitTestInfo2 = this.NearestHitResult(rawPoint, hitTestRadius, SearchMode.Nearest, false);
                if ( !hitTestInfo2.IsHit && hitTestInfo2.DataSeriesIndex != -1 && this.DataSeries.YValues.Count != 0 )
                {
                    double num1 = ((IComparable) this.DataSeries.YValues[hitTestInfo2.DataSeriesIndex]).ToDouble();
                    ICoordinateCalculator<double> ycoordinateCalculator = this.CurrentRenderPassData.YCoordinateCalculator;
                    Point point = this.TransformPoint(rawPoint, this.CurrentRenderPassData.IsVerticalChart);
                    double y = point.Y;
                    double dataValue = ycoordinateCalculator.GetDataValue(y);
                    double num2;
                    if ( !this.CurrentRenderPassData.IsVerticalChart )
                    {
                        point = hitTestInfo2.HitTestPoint;
                        num2 = point.X;
                    }
                    else
                    {
                        point = hitTestInfo2.HitTestPoint;
                        num2 = point.Y;
                    }
                    double num3 = num2;
                    double lowerBound;
                    double upperBound;
                    if ( num1.CompareTo( this.ZeroLineY ) > 0 )
                    {
                        lowerBound = this.ZeroLineY;
                        upperBound = num1;
                    }
                    else
                    {
                        lowerBound = num1;
                        upperBound = this.ZeroLineY;
                    }
                    hitTestInfo2.IsHit = BaseRenderableSeries.CheckIsInBounds( rawPoint.X, num3 - hitTestRadius, num3 + hitTestRadius ) && BaseRenderableSeries.CheckIsInBounds( dataValue, lowerBound, upperBound );
                }
                if ( hitTestInfo2.IsHit )
                    return hitTestInfo2;
            }
            return hitTestInfo1;
        }

        protected override bool GetIsValidForDrawing()
        {
            if ( !base.GetIsValidForDrawing() )
                return false;
            if ( this.SeriesColor.A == ( byte ) 0 || this.StrokeThickness <= 0 )
                return this.PointMarker != null;
            return true;
        }

        protected override void InternalDraw( IRenderContext2D renderContext, IRenderPassData renderPassData )
        {
            bool isVerticalChart = renderPassData.IsVerticalChart;
            IPointSeries pointSeries = this.CurrentRenderPassData.PointSeries;
            int count = pointSeries.Count;
            IPaletteProvider paletteProvider = this.PaletteProvider;
            using ( PenManager penManager = new PenManager( renderContext, this.AntiAliasing, ( float ) this.StrokeThickness, this.Opacity, ( double[ ] ) null ) )
            {
                this._linePen = penManager.GetPen( this.SeriesColor );
                float yzeroCoord = (float) this.GetYZeroCoord();
                ISeriesDrawingHelper seriesDrawingHelper = SeriesDrawingHelpersFactory.GetSeriesDrawingHelper(renderContext, this.CurrentRenderPassData);
                for ( int index = 0 ; index < count ; ++index )
                {
                    IPoint point = pointSeries[index];
                    if ( !double.IsNaN( point.Y ) )
                    {
                        float coordinate1 = (float) renderPassData.XCoordinateCalculator.GetCoordinate(point.X);
                        float coordinate2 = (float) renderPassData.YCoordinateCalculator.GetCoordinate(point.Y);
                        Point pt1 = this.TransformPoint(new Point((double) coordinate1, (double) coordinate2), isVerticalChart);
                        Point pt2 = this.TransformPoint(new Point((double) coordinate1, (double) yzeroCoord), isVerticalChart);
                        if ( paletteProvider != null )
                        {
                            Color? color = paletteProvider.GetColor((IRenderableSeries) this, point.X, point.Y);
                            if ( color.HasValue )
                            {
                                IPen2D pen = penManager.GetPen(color.Value);
                                seriesDrawingHelper.DrawLine( pt1, pt2, pen );
                                continue;
                            }
                        }
                        seriesDrawingHelper.DrawLine( pt1, pt2, this._linePen );
                    }
                }
            }
            IPointMarker pointMarker = this.GetPointMarker();
            if ( pointMarker == null )
                return;
            FastPointsHelper.IteratePoints( SeriesDrawingHelpersFactory.GetPointMarkerPathFactory( renderContext, this.CurrentRenderPassData, pointMarker ), pointSeries, this.CurrentRenderPassData.XCoordinateCalculator, this.CurrentRenderPassData.YCoordinateCalculator );
        }
    }
}
