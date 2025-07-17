// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.RenderableSeries.FastOhlcRenderableSeries
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
using StockSharp.Xaml.Charting.Numerics.CoordinateCalculators;
using StockSharp.Xaml.Charting.Rendering.Common;
using StockSharp.Xaml.Licensing.Core;

namespace StockSharp.Xaml.Charting.Visuals.RenderableSeries
{
    [UltrachartLicenseProvider( typeof( RenderableSeriesUltrachartLicenseProvider ) )]
    public class FastOhlcRenderableSeries : BaseRenderableSeries
    {
        public static readonly DependencyProperty UpWickColorProperty = DependencyProperty.Register(nameof (UpWickColor), typeof (Color), typeof (FastOhlcRenderableSeries), new PropertyMetadata((object) ColorConstants.White, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty DownWickColorProperty = DependencyProperty.Register(nameof (DownWickColor), typeof (Color), typeof (FastOhlcRenderableSeries), new PropertyMetadata((object) ColorConstants.SteelBlue, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty DataPointWidthProperty = DependencyProperty.Register(nameof (DataPointWidth), typeof (double), typeof (FastOhlcRenderableSeries), new PropertyMetadata((object) 0.8, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        private int _ohlcWidth;
        protected IPen2D _upWickPen;
        protected IPen2D _downWickPen;

        public FastOhlcRenderableSeries()
        {
            DefaultStyleKey = ( object ) typeof( FastOhlcRenderableSeries );
            ResamplingMode = ResamplingMode.Mid;
        }

        public double DataPointWidth
        {
            get
            {
                return ( double ) GetValue( FastOhlcRenderableSeries.DataPointWidthProperty );
            }
            set
            {
                SetValue( FastOhlcRenderableSeries.DataPointWidthProperty, ( object ) value );
            }
        }

        public Color UpWickColor
        {
            get
            {
                return ( Color ) GetValue( FastOhlcRenderableSeries.UpWickColorProperty );
            }
            set
            {
                SetValue( FastOhlcRenderableSeries.UpWickColorProperty, ( object ) value );
            }
        }

        public Color DownWickColor
        {
            get
            {
                return ( Color ) GetValue( FastOhlcRenderableSeries.DownWickColorProperty );
            }
            set
            {
                SetValue( FastOhlcRenderableSeries.DownWickColorProperty, ( object ) value );
            }
        }

        protected override void OnResamplingModeChanged()
        {
            ResamplingMode = ResamplingMode.Mid;
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

        protected override double GetSeriesBodyWidth( HitTestInfo nearestHitPoint )
        {
            return ( double ) _ohlcWidth;
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
            if ( UpWickColor.A != ( byte ) 0 && StrokeThickness > 0 )
                return true;
            if ( DownWickColor.A != ( byte ) 0 )
                return StrokeThickness > 0;
            return false;
        }

        protected override void InternalDraw( IRenderContext2D renderContext, IRenderPassData renderPassData )
        {
            AssertDataPointType<OhlcSeriesPoint>( "OhlcDataSeries" );
            using ( PenManager penManager = new PenManager( renderContext, AntiAliasing, ( float ) StrokeThickness, Opacity, ( double[ ] ) null ) )
            {
                _upWickPen = penManager.GetPen( UpWickColor );
                _downWickPen = penManager.GetPen( DownWickColor );
                renderContext.SetPrimitvesCachingEnabled( true );
                if ( PointResamplerBase.RequiresReduction( ResamplingMode, renderPassData.PointRange, ( int ) renderContext.ViewportSize.Width ) )
                    DrawReduced( renderContext, renderPassData, ( IPenManager ) penManager );
                else
                    DrawVanilla( renderContext, renderPassData, ( IPenManager ) penManager );
                renderContext.SetPrimitvesCachingEnabled( false );
            }
        }

        private void DrawReduced( IRenderContext2D renderContext, IRenderPassData renderPassData, IPenManager penManager )
        {
            bool isVerticalChart = renderPassData.IsVerticalChart;
            ICoordinateCalculator<double> xcoordinateCalculator = renderPassData.XCoordinateCalculator;
            ICoordinateCalculator<double> ycoordinateCalculator = renderPassData.YCoordinateCalculator;
            IPointSeries pointSeries = CurrentRenderPassData.PointSeries;
            int count = pointSeries.Count;
            IPaletteProvider paletteProvider1 = PaletteProvider;
            ISeriesDrawingHelper seriesDrawingHelper = SeriesDrawingHelpersFactory.GetSeriesDrawingHelper(renderContext, CurrentRenderPassData);
            for ( int index = 0 ; index < count ; ++index )
            {
                GenericPoint2D<OhlcSeriesPoint> genericPoint2D = pointSeries[index] as GenericPoint2D<OhlcSeriesPoint>;
                double coordinate1 = xcoordinateCalculator.GetCoordinate(genericPoint2D.X);
                ICoordinateCalculator<double> coordinateCalculator1 = ycoordinateCalculator;
                OhlcSeriesPoint yvalues = genericPoint2D.YValues;
                double high1 = yvalues.High;
                double coordinate2 = coordinateCalculator1.GetCoordinate(high1);
                ICoordinateCalculator<double> coordinateCalculator2 = ycoordinateCalculator;
                yvalues = genericPoint2D.YValues;
                double low1 = yvalues.Low;
                double coordinate3 = coordinateCalculator2.GetCoordinate(low1);
                yvalues = genericPoint2D.YValues;
                double open1 = yvalues.Open;
                yvalues = genericPoint2D.YValues;
                IPen2D pointPen = yvalues.Close >= open1 ? _upWickPen : _downWickPen;
                Color? nullable = new Color?();
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
                    nullable = paletteProvider2.OverrideColor( ( IRenderableSeries ) this, x, open2, high2, low2, close );
                    if ( nullable.HasValue )
                        pointPen = penManager.GetPen( nullable.Value );
                }
                seriesDrawingHelper.DrawLine( TransformPoint( new Point( coordinate1, coordinate2 ), isVerticalChart ), TransformPoint( new Point( coordinate1, coordinate3 ), isVerticalChart ), pointPen );
                if ( nullable.HasValue )
                    pointPen.Dispose();
            }
        }

        protected virtual void DrawVanilla( IRenderContext2D renderContext, IRenderPassData renderPassData, IPenManager penManager )
        {
            bool isVerticalChart = renderPassData.IsVerticalChart;
            IPointSeries pointSeries = CurrentRenderPassData.PointSeries;
            int count = pointSeries.Count;
            if ( count == 1 )
                return;
            _ohlcWidth = GetDatapointWidth( renderPassData.XCoordinateCalculator, pointSeries, DataPointWidth );
            _ohlcWidth = _ohlcWidth <= 1 || _ohlcWidth % 2 != 0 ? _ohlcWidth : _ohlcWidth - 1;
            IPaletteProvider paletteProvider1 = PaletteProvider;
            ISeriesDrawingHelper seriesDrawingHelper = SeriesDrawingHelpersFactory.GetSeriesDrawingHelper(renderContext, CurrentRenderPassData);
            for ( int index = 0 ; index < count ; ++index )
            {
                GenericPoint2D<OhlcSeriesPoint> genericPoint2D = pointSeries[index] as GenericPoint2D<OhlcSeriesPoint>;
                OhlcSeriesPoint yvalues = genericPoint2D.YValues;
                double open1 = yvalues.Open;
                yvalues = genericPoint2D.YValues;
                double high1 = yvalues.High;
                yvalues = genericPoint2D.YValues;
                double close1 = yvalues.Close;
                yvalues = genericPoint2D.YValues;
                double low1 = yvalues.Low;
                int num = close1 >= open1 ? 1 : 0;
                int intValue1 = renderPassData.XCoordinateCalculator.GetCoordinate(genericPoint2D.X).ClipToIntValue();
                int intValue2 = ((double) intValue1 - (double) _ohlcWidth * 0.5).ClipToIntValue();
                int intValue3 = ((double) intValue1 + (double) _ohlcWidth * 0.5).ClipToIntValue();
                int intValue4 = renderPassData.YCoordinateCalculator.GetCoordinate(open1).ClipToIntValue();
                int intValue5 = renderPassData.YCoordinateCalculator.GetCoordinate(high1).ClipToIntValue();
                int intValue6 = renderPassData.YCoordinateCalculator.GetCoordinate(low1).ClipToIntValue();
                int intValue7 = renderPassData.YCoordinateCalculator.GetCoordinate(close1).ClipToIntValue();
                IPen2D pointPen = num != 0 ? _upWickPen : _downWickPen;
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
                    double close2 = yvalues.Close;
                    Color? nullable = paletteProvider2.OverrideColor((IRenderableSeries) this, x, open2, high2, low2, close2);
                    if ( nullable.HasValue )
                        pointPen = penManager.GetPen( nullable.Value );
                }
                seriesDrawingHelper.DrawLine( TransformPoint( new Point( ( double ) intValue2, ( double ) intValue4 ), isVerticalChart ), TransformPoint( new Point( ( double ) intValue1, ( double ) intValue4 ), isVerticalChart ), pointPen );
                seriesDrawingHelper.DrawLine( TransformPoint( new Point( ( double ) intValue1, ( double ) intValue7 ), isVerticalChart ), TransformPoint( new Point( ( double ) intValue3, ( double ) intValue7 ), isVerticalChart ), pointPen );
                seriesDrawingHelper.DrawLine( TransformPoint( new Point( ( double ) intValue1, ( double ) intValue5 ), isVerticalChart ), TransformPoint( new Point( ( double ) intValue1, ( double ) intValue6 ), isVerticalChart ), pointPen );
            }
        }

        protected override void OnDataSeriesDependencyPropertyChanged( IDataSeries oldDataSeries, IDataSeries newDataSeries )
        {
            if ( newDataSeries != null && !( newDataSeries is IOhlcDataSeries ) )
                throw new InvalidOperationException( string.Format( "{0} expects a DataSeries of type {1}. Please ensure the correct data has been bound to the Renderable Series", ( object ) GetType().Name, ( object ) typeof( IOhlcDataSeries ) ) );
            base.OnDataSeriesDependencyPropertyChanged( oldDataSeries, newDataSeries );
        }
    }
}
