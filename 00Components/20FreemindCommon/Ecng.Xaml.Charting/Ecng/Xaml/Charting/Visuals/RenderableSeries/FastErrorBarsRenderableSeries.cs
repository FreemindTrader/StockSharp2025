using System;
using System.Windows;
using System.Windows.Media;
using Ecng.Xaml.Charting.Common.Extensions;
using Ecng.Xaml.Charting.Licensing;
using Ecng.Xaml.Charting.Model.DataSeries;
using Ecng.Xaml.Charting.Rendering.Common;
using StockSharp.Xaml.Licensing.Core;

namespace Ecng.Xaml.Charting.Visuals.RenderableSeries
{
    [UltrachartLicenseProvider( typeof( RenderableSeriesUltrachartLicenseProvider ) )]
    public class FastErrorBarsRenderableSeries : BaseRenderableSeries
    {
        private int _errorBarWidth;

        public readonly static DependencyProperty DataPointWidthProperty;

        public virtual double DataPointWidth
        {
            get
            {
                return ( double ) base.GetValue( FastErrorBarsRenderableSeries.DataPointWidthProperty );
            }
            set
            {
                base.SetValue( FastErrorBarsRenderableSeries.DataPointWidthProperty, value );
            }
        }

        static FastErrorBarsRenderableSeries()
        {
            FastErrorBarsRenderableSeries.DataPointWidthProperty = DependencyProperty.Register( "DataPointWidth", typeof( double ), typeof( FastErrorBarsRenderableSeries ), new PropertyMetadata( ( object ) 0.2, new PropertyChangedCallback( BaseRenderableSeries.OnInvalidateParentSurface ) ) );
        }

        public FastErrorBarsRenderableSeries()
        {
            base.DefaultStyleKey = typeof( FastErrorBarsRenderableSeries );
        }

        protected override bool GetIsValidForDrawing()
        {
            if ( !base.GetIsValidForDrawing() || base.SeriesColor.A == 0 )
            {
                return false;
            }
            return base.StrokeThickness > 0;
        }

        protected override double GetSeriesBodyLowerDataBound( HitTestInfo nearestHitPoint )
        {
            return nearestHitPoint.ErrorLow.ToDouble();
        }

        protected override double GetSeriesBodyUpperDataBound( HitTestInfo nearestHitPoint )
        {
            return nearestHitPoint.ErrorHigh.ToDouble();
        }

        protected override double GetSeriesBodyWidth( HitTestInfo nearestHitPoint )
        {
            return ( double ) this._errorBarWidth;
        }

        protected override HitTestInfo HitTestInternal( Point rawPoint, double hitTestRadius, bool interpolate )
        {
            Point hitTestPoint;
            double num;
            HitTestInfo hitTestInfo = base.HitTestInternal(rawPoint, base.GetHitTestRadiusConsideringPointMarkerSize(hitTestRadius), false);
            hitTestInfo = this.HitTestSeriesWithBody( rawPoint, hitTestInfo, hitTestRadius );
            if ( base.CurrentRenderPassData.IsVerticalChart )
            {
                hitTestPoint = hitTestInfo.HitTestPoint;
                num = Math.Abs( hitTestPoint.Y - rawPoint.Y );
            }
            else
            {
                hitTestPoint = hitTestInfo.HitTestPoint;
                num = Math.Abs( hitTestPoint.X - rawPoint.X );
            }
            double num1 = num;
            if ( !hitTestInfo.IsWithinDataBounds )
            {
                bool seriesBodyWidth = num1 < this.GetSeriesBodyWidth(hitTestInfo) / this.DataPointWidth / 2;
                bool flag = seriesBodyWidth;
                bool flag1 = flag;
                hitTestInfo.IsVerticalHit = flag;
                hitTestInfo.IsWithinDataBounds = flag1;
            }
            return hitTestInfo;
        }

        protected override void InternalDraw( IRenderContext2D renderContext, IRenderPassData renderPassData )
        {
            base.AssertDataPointType<HlcSeriesPoint>( "ErrorDataSeries" );
            this._errorBarWidth = base.GetDatapointWidth( renderPassData.XCoordinateCalculator, base.CurrentRenderPassData.PointSeries, this.DataPointWidth );
            bool isVerticalChart = renderPassData.IsVerticalChart;
            HlcPointSeries pointSeries = base.CurrentRenderPassData.PointSeries as HlcPointSeries;
            int count = pointSeries.Count;
            int datapointWidth = base.GetDatapointWidth(renderPassData.XCoordinateCalculator, pointSeries, this.DataPointWidth);
            using ( IPen2D pen2D = renderContext.CreatePen( base.SeriesColor, base.AntiAliasing, ( float ) base.StrokeThickness, base.Opacity, null, PenLineCap.Round ) )
            {
                ISeriesDrawingHelper seriesDrawingHelper = SeriesDrawingHelpersFactory.GetSeriesDrawingHelper(renderContext, base.CurrentRenderPassData);
                for ( int i = 0 ; i < count ; i++ )
                {
                    GenericPoint2D<HlcSeriesPoint> item = pointSeries[i] as GenericPoint2D<HlcSeriesPoint>;
                    double x = item.X;
                    double yErrorHigh = item.YValues.YErrorHigh;
                    double yErrorLow = item.YValues.YErrorLow;
                    if ( !double.IsNaN( yErrorHigh ) && !double.IsNaN( yErrorLow ) )
                    {
                        float coordinate = (float)renderPassData.XCoordinateCalculator.GetCoordinate(x);
                        float single = (float)renderPassData.YCoordinateCalculator.GetCoordinate(yErrorHigh);
                        float coordinate1 = (float)renderPassData.YCoordinateCalculator.GetCoordinate(yErrorLow);
                        int num = (int)((double)datapointWidth * 0.5);
                        float single1 = coordinate - (float)num;
                        float single2 = coordinate + (float)num;
                        seriesDrawingHelper.DrawLine( base.TransformPoint( coordinate, single, isVerticalChart ), base.TransformPoint( coordinate, coordinate1, isVerticalChart ), pen2D );
                        seriesDrawingHelper.DrawLine( base.TransformPoint( single1, single, isVerticalChart ), base.TransformPoint( single2, single, isVerticalChart ), pen2D );
                        seriesDrawingHelper.DrawLine( base.TransformPoint( single1, coordinate1, isVerticalChart ), base.TransformPoint( single2, coordinate1, isVerticalChart ), pen2D );
                    }
                }
            }
        }
    }
}
