// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.FastLineRenderableSeries
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\fx.Xaml.Charting.dll

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using StockSharp.Xaml.Licensing.Core;

namespace fx.Xaml.Charting
{
    [UltrachartLicenseProvider( typeof( RenderableSeriesUltrachartLicenseProvider ) )]
    public class FastLineRenderableSeries : BaseRenderableSeries
    {
        public static readonly DependencyProperty IsDigitalLineProperty = DependencyProperty.Register(nameof (IsDigitalLine), typeof (bool), typeof (FastLineRenderableSeries), new PropertyMetadata((object) false, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty StrokeDashArrayProperty = DependencyProperty.Register(nameof (StrokeDashArray), typeof (double[]), typeof (FastLineRenderableSeries), new PropertyMetadata((object) null, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty OhlcDrawModeProperty = DependencyProperty.Register(nameof (OhlcDrawMode), typeof (OhlcLineDrawMode), typeof (FastLineRenderableSeries), new PropertyMetadata((object) OhlcLineDrawMode.Close, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

        public FastLineRenderableSeries()
        {
            this.DefaultStyleKey = ( object ) typeof( FastLineRenderableSeries );
        }

        public bool IsDigitalLine
        {
            get
            {
                return ( bool ) this.GetValue( FastLineRenderableSeries.IsDigitalLineProperty );
            }
            set
            {
                this.SetValue( FastLineRenderableSeries.IsDigitalLineProperty, ( object ) value );
            }
        }

        [TypeConverter( typeof( StringToDoubleArrayTypeConverter ) )]
        public double[ ] StrokeDashArray
        {
            get
            {
                return ( double[ ] ) this.GetValue( FastLineRenderableSeries.StrokeDashArrayProperty );
            }
            set
            {
                this.SetValue( FastLineRenderableSeries.StrokeDashArrayProperty, ( object ) value );
            }
        }

        public OhlcLineDrawMode OhlcDrawMode
        {
            get
            {
                return ( OhlcLineDrawMode ) this.GetValue( FastLineRenderableSeries.OhlcDrawModeProperty );
            }
            set
            {
                this.SetValue( FastLineRenderableSeries.OhlcDrawModeProperty, ( object ) value );
            }
        }

        public override object PointSeriesArg
        {
            get
            {
                return ( object ) this.OhlcDrawMode;
            }
        }

        protected override void OnSeriesColorChanged()
        {
        }

        protected override bool GetIsValidForDrawing()
        {
            if ( !base.GetIsValidForDrawing() )
                return false;
            if ( this.SeriesColor.A == ( byte ) 0 || this.StrokeThickness <= 0 )
                return this.PointMarker != null;
            return true;
        }

        protected override HitTestInfo ToHitTestInfoImpl( int nearestDataPointIndex )
        {
            HitTestInfo hitTestInfoImpl = base.ToHitTestInfoImpl(nearestDataPointIndex);
            if ( hitTestInfoImpl.DataSeriesType != DataSeriesType.Ohlc && hitTestInfoImpl.DataSeriesType != DataSeriesType.Hlc )
                return hitTestInfoImpl;
            hitTestInfoImpl.DataSeriesType = DataSeriesType.Xy;
            switch ( this.OhlcDrawMode )
            {
                case OhlcLineDrawMode.Open:
                    hitTestInfoImpl.YValue = hitTestInfoImpl.OpenValue;
                    break;
                case OhlcLineDrawMode.High:
                    hitTestInfoImpl.YValue = hitTestInfoImpl.HighValue;
                    break;
                case OhlcLineDrawMode.Low:
                    hitTestInfoImpl.YValue = hitTestInfoImpl.LowValue;
                    break;
                case OhlcLineDrawMode.Close:
                    hitTestInfoImpl.YValue = hitTestInfoImpl.CloseValue;
                    break;
            }
            return hitTestInfoImpl;
        }

        protected override void InternalDraw( IRenderContext2D renderContext, IRenderPassData renderPassData )
        {
            IPointSeries pointSeries = this.CurrentRenderPassData.PointSeries;
            int num = this.PaletteProvider != null ? 1 : 0;
            Color lineColor = this.SeriesColor;
            IPathContextFactory linesPathFactory = SeriesDrawingHelpersFactory.GetLinesPathFactory(renderContext, this.CurrentRenderPassData);
            if ( num != 0 )
            {
                PenManager penManager = new PenManager(renderContext, this.AntiAliasing, (float) this.StrokeThickness, this.Opacity, this.StrokeDashArray);
                try
                {
                    Func<double, double, IPen2D> func = (Func<double, double, IPen2D>) ((x, y) => penManager.GetPen(this.PaletteProvider.GetColor((IRenderableSeries) this, x, y) ?? lineColor));
                    FastLinesHelper.IterateLines( linesPathFactory, ( Func<double, double, IPathColor> ) func, pointSeries, this.CurrentRenderPassData.XCoordinateCalculator, this.CurrentRenderPassData.YCoordinateCalculator, this.IsDigitalLine, this.DrawNaNAs == LineDrawMode.ClosedLines );
                }
                finally
                {
                    if ( penManager != null )
                        penManager.Dispose();
                }
            }
            else
            {
                using ( IPen2D pen = renderContext.CreatePen( this.SeriesColor, this.AntiAliasing, ( float ) this.StrokeThickness, this.Opacity, this.StrokeDashArray, PenLineCap.Round ) )
                    FastLinesHelper.IterateLines( linesPathFactory, pen, pointSeries, this.CurrentRenderPassData.XCoordinateCalculator, this.CurrentRenderPassData.YCoordinateCalculator, this.IsDigitalLine, this.DrawNaNAs == LineDrawMode.ClosedLines );
            }
            IPointMarker pointMarker = this.GetPointMarker();
            if ( pointMarker == null )
                return;
            FastPointsHelper.IteratePoints( SeriesDrawingHelpersFactory.GetPointMarkerPathFactory( renderContext, this.CurrentRenderPassData, pointMarker ), pointSeries, this.CurrentRenderPassData.XCoordinateCalculator, this.CurrentRenderPassData.YCoordinateCalculator );
        }
    }
}
