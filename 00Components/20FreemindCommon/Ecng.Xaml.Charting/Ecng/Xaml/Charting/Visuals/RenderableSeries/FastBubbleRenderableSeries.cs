// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Visuals.RenderableSeries.FastBubbleRenderableSeries
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Ecng.Xaml.Charting.Common;
using Ecng.Xaml.Charting.Common.Extensions;
using Ecng.Xaml.Charting.Licensing;
using Ecng.Xaml.Charting.Model.DataSeries;
using Ecng.Xaml.Charting.Rendering.Common;
using StockSharp.Xaml.Licensing.Core;

namespace Ecng.Xaml.Charting.Visuals.RenderableSeries
{
    [UltrachartLicenseProvider( typeof( RenderableSeriesUltrachartLicenseProvider ) )]
    public class FastBubbleRenderableSeries : BaseRenderableSeries
    {
        public static readonly DependencyProperty BubbleColorProperty = DependencyProperty.Register(nameof (BubbleColor), typeof (Color), typeof (FastBubbleRenderableSeries), new PropertyMetadata((object) new Color(), new PropertyChangedCallback(FastBubbleRenderableSeries.OnPropertyChanged)));
        public static readonly DependencyProperty AutoZRangeProperty = DependencyProperty.Register(nameof (AutoZRange), typeof (bool), typeof (FastBubbleRenderableSeries), new PropertyMetadata((object) true, new PropertyChangedCallback(FastBubbleRenderableSeries.OnPropertyChanged)));
        public static readonly DependencyProperty ZScaleFactorProperty = DependencyProperty.Register(nameof (ZScaleFactor), typeof (double), typeof (FastBubbleRenderableSeries), new PropertyMetadata((object) 1.0, new PropertyChangedCallback(FastBubbleRenderableSeries.OnPropertyChanged)));
        private SmartDisposable<ISprite2D> _cachedBubble;
        private Type _typeOfRendererForCachedBubble;
        private double _maxZValue;
        private const double MaxBubbleSizeInPixels = 300.0;

        public FastBubbleRenderableSeries()
        {
            DefaultStyleKey = ( object ) typeof( FastBubbleRenderableSeries );
        }

        public Color BubbleColor
        {
            get
            {
                return ( Color ) GetValue( FastBubbleRenderableSeries.BubbleColorProperty );
            }
            set
            {
                SetValue( FastBubbleRenderableSeries.BubbleColorProperty, ( object ) value );
            }
        }

        public bool AutoZRange
        {
            get
            {
                return ( bool ) GetValue( FastBubbleRenderableSeries.AutoZRangeProperty );
            }
            set
            {
                SetValue( FastBubbleRenderableSeries.AutoZRangeProperty, ( object ) value );
            }
        }

        public double ZScaleFactor
        {
            get
            {
                return ( double ) GetValue( FastBubbleRenderableSeries.ZScaleFactorProperty );
            }
            set
            {
                SetValue( FastBubbleRenderableSeries.ZScaleFactorProperty, ( object ) value );
            }
        }

        protected override HitTestInfo HitTestInternal( Point rawPoint, double hitTestRadius, bool interpolate )
        {
            HitTestInfo nearestHitPoint = base.HitTestInternal(rawPoint, hitTestRadius, false);
            return HitTestSeriesWithBody( rawPoint, nearestHitPoint, hitTestRadius );
        }

        protected override bool IsBodyHit( Point pt, Rect boundaries, HitTestInfo nearestHitPoint )
        {
            double x = nearestHitPoint.HitTestPoint.X;
            double y = nearestHitPoint.HitTestPoint.Y;
            double bubbleSize = GetBubbleSize(nearestHitPoint.ZValue.ToDouble());
            boundaries = new Rect( x - bubbleSize / 2.0, y - bubbleSize / 2.0, bubbleSize, bubbleSize );
            double num1 = bubbleSize / 2.0;
            double num2 = pt.X - (boundaries.Left + boundaries.Right) / 2.0;
            double num3 = pt.Y - (boundaries.Top + boundaries.Bottom) / 2.0;
            return Math.Sqrt( num2 * num2 + num3 * num3 ) <= num1;
        }

        protected override void InternalDraw( IRenderContext2D renderContext, IRenderPassData renderPassData )
        {
            AssertDataPointType<XyzSeriesPoint>( "XyzDataSeries" );
            RecreateCachedBubbles( renderContext );
            Color? paletteColor = new Color?();
            EnumerateBubbles( renderPassData, ( Action<double, double, double, Rect> ) ( ( x, y, z, rect ) =>
            {
                if ( PaletteProvider != null )
                    paletteColor = PaletteProvider.OverrideColor( ( IRenderableSeries ) this, x, y, z );
                if ( paletteColor.HasValue )
                {
                    using ( IPen2D pen = renderContext.CreatePen( paletteColor.Value, AntiAliasing, ( float ) StrokeThickness, Opacity, ( double[ ] ) null, PenLineCap.Round ) )
                    {
                        using ( IBrush2D brush = renderContext.CreateBrush( paletteColor.Value, Opacity, new bool?() ) )
                            renderContext.DrawEllipse( pen, brush, new Point( rect.X + rect.Width * 0.5, rect.Y + rect.Height * 0.5 ), rect.Width, rect.Height );
                    }
                }
                else
                    renderContext.DrawSprites( _cachedBubble.Inner, ( IEnumerable<Rect> ) new Rect[ 1 ]
                    {
            rect
                    } );
            } ) );
        }

        private void RecreateCachedBubbles( IRenderContext2D renderContext2D )
        {
            Type type = renderContext2D.GetType();
            if ( _cachedBubble != null && _typeOfRendererForCachedBubble == type )
                return;
            if ( _cachedBubble != null )
            {
                _cachedBubble.Dispose();
                _cachedBubble = ( SmartDisposable<ISprite2D> ) null;
            }
            IRenderContext2D renderContext2D1 = renderContext2D;
            Ellipse ellipse = new Ellipse();
            ellipse.Width = 300.0;
            ellipse.Height = 300.0;
            ellipse.Fill = ( Brush ) new RadialGradientBrush( new GradientStopCollection()
      {
        new GradientStop()
        {
          Color = Colors.Transparent,
          Offset = 0.0
        },
        new GradientStop()
        {
          Color = BubbleColor,
          Offset = 0.95
        },
        new GradientStop()
        {
          Color = Colors.Transparent,
          Offset = 1.0
        }
      } );
            _cachedBubble = new SmartDisposable<ISprite2D>( renderContext2D1.CreateSprite( ( FrameworkElement ) ellipse ) );
            _typeOfRendererForCachedBubble = type;
        }

        private void EnumerateBubbles( IRenderPassData renderPassData, Action<double, double, double, Rect> callback )
        {
            XyzPointSeries pointSeries = renderPassData.PointSeries as XyzPointSeries;
            int count = pointSeries.Count;
            _maxZValue = 0.0;
            bool isVerticalChart = renderPassData.IsVerticalChart;
            XyzSeriesPoint yvalues;
            if ( AutoZRange )
            {
                for ( int index = 0 ; index < count ; ++index )
                {
                    yvalues = ( pointSeries[ index ] as GenericPoint2D<XyzSeriesPoint> ).YValues;
                    double z = yvalues.Z;
                    if ( z > _maxZValue )
                        _maxZValue = z;
                }
            }
            for ( int index = 0 ; index < count ; ++index )
            {
                GenericPoint2D<XyzSeriesPoint> genericPoint2D = pointSeries[index] as GenericPoint2D<XyzSeriesPoint>;
                double x = genericPoint2D.X;
                yvalues = genericPoint2D.YValues;
                double y = yvalues.Y;
                yvalues = genericPoint2D.YValues;
                double z = yvalues.Z;
                if ( !double.IsNaN( y ) )
                {
                    double bubbleSize = GetBubbleSize(z);
                    Point point1 = TransformPoint((float) (int) renderPassData.XCoordinateCalculator.GetCoordinate(x), (float) (int) renderPassData.YCoordinateCalculator.GetCoordinate(y), isVerticalChart);
                    Point point2 = renderPassData.TransformationStrategy.ReverseTransform(point1);
                    callback( x, y, z, new Rect( point2.X - bubbleSize / 2.0, point2.Y - bubbleSize / 2.0, bubbleSize, bubbleSize ) );
                }
            }
        }

        private double GetBubbleSize( double zValue )
        {
            double num = zValue * ZScaleFactor;
            if ( AutoZRange )
                num = 300.0 * zValue / _maxZValue;
            return num;
        }

        private static void OnPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            FastBubbleRenderableSeries renderableSeries = (FastBubbleRenderableSeries) d;
            renderableSeries._cachedBubble = ( SmartDisposable<ISprite2D> ) null;
            renderableSeries.OnInvalidateParentSurface();
        }
    }
}
