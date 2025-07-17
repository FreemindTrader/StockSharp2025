// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Visuals.RenderableSeries.FastCandlestickRenderableSeries
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System;
using System.Windows;
using System.Windows.Media;
using Ecng.Xaml.Charting.Common.Extensions;
using Ecng.Xaml.Charting.Licensing;
using Ecng.Xaml.Charting.Model.DataSeries;
using Ecng.Xaml.Charting.Numerics;
using Ecng.Xaml.Charting.Numerics.CoordinateCalculators;
using Ecng.Xaml.Charting.Rendering.Common;
using Ecng.Xaml.Charting.Utility;
using StockSharp.Xaml.Licensing.Core;

namespace Ecng.Xaml.Charting.Visuals.RenderableSeries
{
    [UltrachartLicenseProvider( typeof( RenderableSeriesUltrachartLicenseProvider ) )]
    public class FastCandlestickRenderableSeries : BaseRenderableSeries
    {
        public static readonly DependencyProperty UpWickColorProperty = DependencyProperty.Register(nameof (UpWickColor), typeof (Color), typeof (FastCandlestickRenderableSeries), new PropertyMetadata((object) ColorConstants.White, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty DownWickColorProperty = DependencyProperty.Register(nameof (DownWickColor), typeof (Color), typeof (FastCandlestickRenderableSeries), new PropertyMetadata((object) ColorConstants.SteelBlue, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty UpBodyBrushProperty = DependencyProperty.Register(nameof (UpBodyBrush), typeof (Brush), typeof (FastCandlestickRenderableSeries), new PropertyMetadata((object) new SolidColorBrush(ColorConstants.Transparent), new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty DownBodyBrushProperty = DependencyProperty.Register(nameof (DownBodyBrush), typeof (Brush), typeof (FastCandlestickRenderableSeries), new PropertyMetadata((object) new SolidColorBrush(ColorConstants.SteelBlue), new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty DataPointWidthProperty = DependencyProperty.Register(nameof (DataPointWidth), typeof (double), typeof (FastCandlestickRenderableSeries), new PropertyMetadata((object) 0.8, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        [Obsolete("We're sorry! FastCandlestickRenderableSeries.UpBodyColor is obsolete, please use UpBodyBrush instead", true)]
        public static readonly DependencyProperty UpBodyColorProperty = DependencyProperty.Register(nameof (UpBodyColor), typeof (Color), typeof (FastCandlestickRenderableSeries), new PropertyMetadata((object) ColorConstants.Transparent, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        [Obsolete("We're sorry! FastCandlestickRenderableSeries.DownBodyColor is obsolete, please use DownBodyBrush instead", true)]
        public static readonly DependencyProperty DownBodyColorProperty = DependencyProperty.Register(nameof (DownBodyColor), typeof (Color), typeof (FastCandlestickRenderableSeries), new PropertyMetadata((object) ColorConstants.SteelBlue, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        private IPen2D _upWickPen;
        private IPen2D _downWickPen;
        private IBrush2D _upBodyBrush;
        private IBrush2D _downBodyBrush;
        private int _candleWidth;

        public FastCandlestickRenderableSeries()
        {
            this.DefaultStyleKey = ( object ) typeof( FastCandlestickRenderableSeries );
            this.ResamplingMode = ResamplingMode.Mid;
        }

        public double DataPointWidth
        {
            get
            {
                return ( double ) this.GetValue( FastCandlestickRenderableSeries.DataPointWidthProperty );
            }
            set
            {
                this.SetValue( FastCandlestickRenderableSeries.DataPointWidthProperty, ( object ) value );
            }
        }

        public Color UpWickColor
        {
            get
            {
                return ( Color ) this.GetValue( FastCandlestickRenderableSeries.UpWickColorProperty );
            }
            set
            {
                this.SetValue( FastCandlestickRenderableSeries.UpWickColorProperty, ( object ) value );
            }
        }

        public Color DownWickColor
        {
            get
            {
                return ( Color ) this.GetValue( FastCandlestickRenderableSeries.DownWickColorProperty );
            }
            set
            {
                this.SetValue( FastCandlestickRenderableSeries.DownWickColorProperty, ( object ) value );
            }
        }

        [Obsolete( "We're sorry! FastCandlestickRenderableSeries.UpBodyColor is obsolete, please use UpBodyBrush instead", true )]
        public Color UpBodyColor
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

        public Brush UpBodyBrush
        {
            get
            {
                return ( Brush ) this.GetValue( FastCandlestickRenderableSeries.UpBodyBrushProperty );
            }
            set
            {
                this.SetValue( FastCandlestickRenderableSeries.UpBodyBrushProperty, ( object ) value );
            }
        }

        [Obsolete( "We're sorry! FastCandlestickRenderableSeries.DownBodyColor is obsolete, please use DownBodyBrush instead", true )]
        public Color DownBodyColor
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

        public Brush DownBodyBrush
        {
            get
            {
                return ( Brush ) this.GetValue( FastCandlestickRenderableSeries.DownBodyBrushProperty );
            }
            set
            {
                this.SetValue( FastCandlestickRenderableSeries.DownBodyBrushProperty, ( object ) value );
            }
        }

        protected override void OnResamplingModeChanged()
        {
            this.ResamplingMode = ResamplingMode.Mid;
        }

        protected override HitTestInfo HitTestInternal( Point rawPoint, double hitTestRadius, bool interpolate )
        {
            HitTestInfo nearestHitPoint1 = base.HitTestInternal(rawPoint, hitTestRadius, false);
            HitTestInfo nearestHitPoint2 = this.HitTestSeriesWithBody(rawPoint, nearestHitPoint1, hitTestRadius);
            double num = this.CurrentRenderPassData.IsVerticalChart ? Math.Abs(nearestHitPoint2.HitTestPoint.Y - rawPoint.Y) : Math.Abs(nearestHitPoint2.HitTestPoint.X - rawPoint.X);
            if ( !nearestHitPoint2.IsWithinDataBounds )
            {
                bool flag = num < this.GetSeriesBodyWidth(nearestHitPoint2) / this.DataPointWidth / 2.0;
                nearestHitPoint2.IsWithinDataBounds = nearestHitPoint2.IsVerticalHit = flag;
            }
            return nearestHitPoint2;
        }

        protected override double GetSeriesBodyWidth( HitTestInfo nearestHitPoint )
        {
            return ( double ) this._candleWidth;
        }

        protected override double GetSeriesBodyLowerDataBound( HitTestInfo nearestHitPoint )
        {
            return nearestHitPoint.LowValue.ToDouble();
        }

        protected override double GetSeriesBodyUpperDataBound( HitTestInfo nearestHitPoint )
        {
            return nearestHitPoint.HighValue.ToDouble();
        }

        protected override bool GetIsValidForDrawing()
        {
            if ( !base.GetIsValidForDrawing() )
                return false;
            if ( ( this.UpWickColor.A == ( byte ) 0 || this.StrokeThickness <= 0 ) && ( ( this.DownWickColor.A == ( byte ) 0 || this.StrokeThickness <= 0 ) && this.UpBodyBrush == null ) )
                return this.DownBodyBrush != null;
            return true;
        }

        protected override void InternalDraw( IRenderContext2D renderContext, IRenderPassData renderPassData )
        {
            this.AssertDataPointType<OhlcSeriesPoint>( "OhlcDataSeries" );
            IndexRange pointRange = this.CurrentRenderPassData.PointRange;
            using ( PenManager penManager = new PenManager( renderContext, this.AntiAliasing, ( float ) this.StrokeThickness, this.Opacity, ( double[ ] ) null ) )
            {
                using ( this._upWickPen = penManager.GetPen( this.UpWickColor ) )
                {
                    using ( this._downWickPen = penManager.GetPen( this.DownWickColor ) )
                    {
                        this._upBodyBrush = this.CreateBrush( renderContext, this.UpBodyBrush );
                        this._downBodyBrush = this.CreateBrush( renderContext, this.DownBodyBrush );
                        renderContext.DisposeResourceAfterDraw( ( IDisposable ) this._upBodyBrush );
                        renderContext.DisposeResourceAfterDraw( ( IDisposable ) this._downBodyBrush );
                        renderContext.SetPrimitvesCachingEnabled( true );
                        if ( PointResamplerBase.RequiresReduction( this.ResamplingMode, pointRange, ( int ) renderContext.ViewportSize.Width ) )
                            this.DrawReduced( renderContext, renderPassData, ( IPenManager ) penManager );
                        else
                            this.DrawVanilla( renderContext, renderPassData, ( IPenManager ) penManager );
                        renderContext.SetPrimitvesCachingEnabled( false );
                    }
                }
            }
        }

        private IBrush2D CreateBrush( IRenderContext2D renderContext, Brush wpfBrush )
        {
            SolidColorBrush solidColorBrush = wpfBrush as SolidColorBrush;
            if ( solidColorBrush == null )
                return renderContext.CreateBrush( wpfBrush, this.Opacity, TextureMappingMode.PerPrimitive );
            return renderContext.CreateBrush( solidColorBrush.Color, this.Opacity, new bool?() );
        }

        private void DrawReduced( IRenderContext2D renderContext, IRenderPassData renderPassData, IPenManager penManager )
        {
            bool isVerticalChart = renderPassData.IsVerticalChart;
            IPointSeries pointSeries = this.CurrentRenderPassData.PointSeries;
            int count = pointSeries.Count;
            IPaletteProvider paletteProvider1 = this.PaletteProvider;
            ISeriesDrawingHelper seriesDrawingHelper = SeriesDrawingHelpersFactory.GetSeriesDrawingHelper(renderContext, this.CurrentRenderPassData);
            for ( int index = 0 ; index < count ; ++index )
            {
                GenericPoint2D<OhlcSeriesPoint> genericPoint2D = pointSeries[index] as GenericPoint2D<OhlcSeriesPoint>;
                double coordinate1 = renderPassData.XCoordinateCalculator.GetCoordinate(genericPoint2D.X);
                ICoordinateCalculator<double> ycoordinateCalculator1 = renderPassData.YCoordinateCalculator;
                OhlcSeriesPoint yvalues = genericPoint2D.YValues;
                double high1 = yvalues.High;
                double coordinate2 = ycoordinateCalculator1.GetCoordinate(high1);
                ICoordinateCalculator<double> ycoordinateCalculator2 = renderPassData.YCoordinateCalculator;
                yvalues = genericPoint2D.YValues;
                double low1 = yvalues.Low;
                double coordinate3 = ycoordinateCalculator2.GetCoordinate(low1);
                yvalues = genericPoint2D.YValues;
                double open1 = yvalues.Open;
                yvalues = genericPoint2D.YValues;
                IPen2D pointPen = yvalues.Close >= open1 ? this._upWickPen : this._downWickPen;
                if ( paletteProvider1 != null )
                {
                    IPaletteProvider paletteProvider2 = paletteProvider1;
                    double x = genericPoint2D.X;
                    yvalues = genericPoint2D.YValues;
                    double open2 = yvalues.Open;
                    yvalues = genericPoint2D.YValues;
                    double high2 = yvalues.High;
                    yvalues = genericPoint2D.YValues;
                    double low2 = yvalues.Low;
                    yvalues = genericPoint2D.YValues;
                    double close = yvalues.Close;
                    Color? nullable = paletteProvider2.OverrideColor((IRenderableSeries) this, x, open2, high2, low2, close);
                    if ( nullable.HasValue )
                        pointPen = penManager.GetPen( nullable.Value );
                }
                seriesDrawingHelper.DrawLine( this.TransformPoint( new Point( coordinate1, coordinate2 ), isVerticalChart ), this.TransformPoint( new Point( coordinate1, coordinate3 ), isVerticalChart ), pointPen );
            }
        }

        private void DrawVanilla( IRenderContext2D renderContext, IRenderPassData renderPassData, IPenManager penManager )
        {
            bool isVerticalChart = renderPassData.IsVerticalChart;
            double chartRotationAngle = this.GetChartRotationAngle(renderPassData);
            IPointSeries pointSeries = this.CurrentRenderPassData.PointSeries;
            int count = pointSeries.Count;
            if ( count == 1 )
                return;
            this._candleWidth = this.GetDatapointWidth( renderPassData.XCoordinateCalculator, pointSeries, this.DataPointWidth );
            this._candleWidth = this._candleWidth <= 1 || this._candleWidth % 2 != 0 ? this._candleWidth : this._candleWidth - 1;
            IPaletteProvider paletteProvider = this.PaletteProvider;
            ISeriesDrawingHelper seriesDrawingHelper = SeriesDrawingHelpersFactory.GetSeriesDrawingHelper(renderContext, this.CurrentRenderPassData);
            for ( int index = 0 ; index < count ; ++index )
            {
                GenericPoint2D<OhlcSeriesPoint> genericPoint2D = pointSeries[index] as GenericPoint2D<OhlcSeriesPoint>;
                OhlcSeriesPoint yvalues = genericPoint2D.YValues;
                double open = yvalues.Open;
                yvalues = genericPoint2D.YValues;
                double high = yvalues.High;
                yvalues = genericPoint2D.YValues;
                double close = yvalues.Close;
                yvalues = genericPoint2D.YValues;
                double low = yvalues.Low;
                bool flag = close >= open;
                int intValue1 = renderPassData.XCoordinateCalculator.GetCoordinate(genericPoint2D.X).ClipToIntValue();
                int intValue2 = ((double) intValue1 - (double) this._candleWidth * 0.5).ClipToIntValue();
                int intValue3 = ((double) intValue1 + (double) this._candleWidth * 0.5).ClipToIntValue();
                int intValue4 = renderPassData.YCoordinateCalculator.GetCoordinate(flag ? close : open).ClipToIntValue();
                int intValue5 = renderPassData.YCoordinateCalculator.GetCoordinate(high).ClipToIntValue();
                int intValue6 = renderPassData.YCoordinateCalculator.GetCoordinate(low).ClipToIntValue();
                int intValue7 = renderPassData.YCoordinateCalculator.GetCoordinate(flag ? open : close).ClipToIntValue();
                IPen2D pointPen = flag ? this._upWickPen : this._downWickPen;
                IBrush2D pointFill = flag ? this._upBodyBrush : this._downBodyBrush;
                if ( paletteProvider != null )
                {
                    Color? nullable = paletteProvider.OverrideColor((IRenderableSeries) this, genericPoint2D.X, open, high, low, close);
                    if ( nullable.HasValue )
                    {
                        pointPen = penManager.GetPen( nullable.Value );
                        pointFill = renderContext.CreateBrush( nullable.Value, this.Opacity, new bool?() );
                    }
                }
                seriesDrawingHelper.DrawLine( this.TransformPoint( new Point( ( double ) intValue1, ( double ) intValue5 ), isVerticalChart ), this.TransformPoint( new Point( ( double ) intValue1, ( double ) intValue4 ), isVerticalChart ), pointPen );
                seriesDrawingHelper.DrawLine( this.TransformPoint( new Point( ( double ) intValue1, ( double ) intValue7 ), isVerticalChart ), this.TransformPoint( new Point( ( double ) intValue1, ( double ) intValue6 ), isVerticalChart ), pointPen );
                if ( flag )
                    NumberUtil.Swap( ref intValue4, ref intValue7 );
                seriesDrawingHelper.DrawBox( this.TransformPoint( new Point( ( double ) intValue2, ( double ) intValue4 ), isVerticalChart ), this.TransformPoint( new Point( ( double ) intValue3, ( double ) intValue7 ), isVerticalChart ), pointFill, pointPen, chartRotationAngle );
            }
        }

        protected override void OnDataSeriesDependencyPropertyChanged( IDataSeries oldDataSeries, IDataSeries newDataSeries )
        {
            if ( newDataSeries != null && !( newDataSeries is IOhlcDataSeries ) )
                throw new InvalidOperationException( string.Format( "{0} expects a DataSeries of type {1}. Please ensure the correct data has been bound to the Renderable Series", ( object ) this.GetType().Name, ( object ) typeof( IOhlcDataSeries ) ) );
            base.OnDataSeriesDependencyPropertyChanged( oldDataSeries, newDataSeries );
        }
    }
}
