// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.FastMountainRenderableSeries
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System;
using System.Windows;
using System.Windows.Media;
using StockSharp.Xaml.Licensing.Core;

namespace Ecng.Xaml.Charting
{
    [UltrachartLicenseProvider( typeof( RenderableSeriesUltrachartLicenseProvider ) )]
    public class FastMountainRenderableSeries : BaseMountainRenderableSeries
    {
        public FastMountainRenderableSeries()
        {
            this.DefaultStyleKey = ( object ) typeof( FastMountainRenderableSeries );
        }

        protected override bool GetIsValidForDrawing()
        {
            if ( !base.GetIsValidForDrawing() )
                return false;
            if ( this.SeriesColor.A == ( byte ) 0 || this.StrokeThickness <= 0 )
                return this.AreaBrush != null;
            return true;
        }

        protected override void InternalDraw( IRenderContext2D renderContext, IRenderPassData renderPassData )
        {
            double chartRotationAngle = this.GetChartRotationAngle(renderPassData);
            float yzeroCoord = (float) this.GetYZeroCoord();
            IPointSeries pointSeries = this.CurrentRenderPassData.PointSeries;
            Color lineColor = this.SeriesColor;
            IBrush2D areaBrush = renderContext.CreateBrush(this.AreaBrush, this.Opacity, TextureMappingMode.PerPrimitive);
            try
            {
                using ( IPen2D pen = renderContext.CreatePen( this.SeriesColor, this.AntiAliasing, ( float ) this.StrokeThickness, this.Opacity, ( double[ ] ) null, PenLineCap.Round ) )
                {
                    IPathContextFactory linesPathFactory = SeriesDrawingHelpersFactory.GetLinesPathFactory(renderContext, this.CurrentRenderPassData);
                    IPathContextFactory mountainAreaPathFactory = SeriesDrawingHelpersFactory.GetMountainAreaPathFactory(renderContext, this.CurrentRenderPassData, yzeroCoord, chartRotationAngle);
                    if ( this.PaletteProvider != null )
                    {
                        PenManager penManager = new PenManager(renderContext, this.AntiAliasing, (float) this.StrokeThickness, this.Opacity, (double[]) null);
                        try
                        {
                            Func<double, double, IPen2D> func1 = (Func<double, double, IPen2D>) ((x, y) => penManager.GetPen(this.PaletteProvider.GetColor((IRenderableSeries) this, x, y) ?? lineColor));
                            Func<double, double, IBrush2D> func2 = (Func<double, double, IBrush2D>) ((x, y) =>
                            {
                                Color? color = this.PaletteProvider.GetColor((IRenderableSeries) this, x, y);
                                if (!color.HasValue)
                                    return areaBrush;
                                return penManager.GetBrush(color.Value);
                            });
                            FastLinesHelper.IterateLines( mountainAreaPathFactory, ( Func<double, double, IPathColor> ) func2, pointSeries, this.CurrentRenderPassData.XCoordinateCalculator, this.CurrentRenderPassData.YCoordinateCalculator, this.IsDigitalLine, false );
                            FastLinesHelper.IterateLines( linesPathFactory, ( Func<double, double, IPathColor> ) func1, pointSeries, this.CurrentRenderPassData.XCoordinateCalculator, this.CurrentRenderPassData.YCoordinateCalculator, this.IsDigitalLine, this.DrawNaNAs == LineDrawMode.ClosedLines );
                        }
                        finally
                        {
                            if ( penManager != null )
                                penManager.Dispose();
                        }
                    }
                    else
                    {
                        FastLinesHelper.IterateLines( mountainAreaPathFactory, areaBrush, pointSeries, this.CurrentRenderPassData.XCoordinateCalculator, this.CurrentRenderPassData.YCoordinateCalculator, this.IsDigitalLine, false );
                        FastLinesHelper.IterateLines( linesPathFactory, pen, pointSeries, this.CurrentRenderPassData.XCoordinateCalculator, this.CurrentRenderPassData.YCoordinateCalculator, this.IsDigitalLine, false );
                    }
                    IPointMarker pointMarker = this.GetPointMarker();
                    if ( pointMarker == null )
                        return;
                    FastPointsHelper.IteratePoints( SeriesDrawingHelpersFactory.GetPointMarkerPathFactory( renderContext, this.CurrentRenderPassData, pointMarker ), pointSeries, this.CurrentRenderPassData.XCoordinateCalculator, this.CurrentRenderPassData.YCoordinateCalculator );
                }
            }
            finally
            {
                if ( areaBrush != null )
                    areaBrush.Dispose();
            }
        }

        protected override bool IsHitTest( Point rawPoint, HitTestInfo nearestHitResult, double hitTestRadius, Point previousDataPoint, Point nextDataPoint )
        {
            bool flag = base.IsHitTest(rawPoint, nearestHitResult, hitTestRadius, previousDataPoint, nextDataPoint);
            Tuple<IComparable, IComparable> hitDataValue = this.GetHitDataValue(rawPoint);
            if ( !flag && hitDataValue.Item1.ToDouble() >= previousDataPoint.X && ( hitDataValue.Item1.ToDouble() <= nextDataPoint.X && !double.IsNaN( nearestHitResult.YValue.ToDouble() ) ) )
            {
                Point intersectionPoint;
                PointUtil.LineIntersection2D( new PointUtil.Line( previousDataPoint, nextDataPoint ), new PointUtil.Line( new Point( hitDataValue.Item1.ToDouble(), this.ZeroLineY ), new Point( hitDataValue.Item1.ToDouble(), Math.Max( previousDataPoint.Y, nextDataPoint.Y ) ) ), out intersectionPoint );
                flag = hitDataValue.Item2.ToDouble().CompareTo( this.ZeroLineY ) >= 0 && hitDataValue.Item2.ToDouble().CompareTo( intersectionPoint.Y ) <= 0;
            }
            return flag;
        }
    }
}
