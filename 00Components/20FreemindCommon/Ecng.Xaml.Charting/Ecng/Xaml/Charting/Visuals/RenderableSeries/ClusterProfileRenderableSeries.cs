// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.RenderableSeries.ClusterProfileRenderableSeries
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using MatterHackers.VectorMath;
using StockSharp.Xaml.Charting.Licensing;
using StockSharp.Xaml.Charting.Model.DataSeries.SegmentDataSeries;
using StockSharp.Xaml.Charting.Numerics.CoordinateCalculators;
using StockSharp.Xaml.Charting.Rendering.Common;
using StockSharp.Xaml.Charting.Utility;
using StockSharp.Xaml.Licensing.Core;

namespace StockSharp.Xaml.Charting.Visuals.RenderableSeries
{
    [UltrachartLicenseProvider( typeof( RenderableSeriesUltrachartLicenseProvider ) )]
    public class ClusterProfileRenderableSeries : TimeframeSegmentRenderableSeries
    {
        public static readonly DependencyProperty LineColorProperty       = DependencyProperty.Register(nameof (LineColor),       typeof (Color), typeof (ClusterProfileRenderableSeries), new PropertyMetadata((object) Colors.DarkGray, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty TextColorProperty       = DependencyProperty.Register(nameof (TextColor),       typeof (Color), typeof (ClusterProfileRenderableSeries), new PropertyMetadata((object) Color.FromRgb((byte) 90, (byte) 90, (byte) 90), new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty ClusterColorProperty    = DependencyProperty.Register(nameof (ClusterColor),    typeof (Color), typeof (ClusterProfileRenderableSeries), new PropertyMetadata((object) Colors.DarkGreen, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty ClusterMaxColorProperty = DependencyProperty.Register(nameof (ClusterMaxColor), typeof (Color), typeof (ClusterProfileRenderableSeries), new PropertyMetadata((object) Colors.LimeGreen, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

        private ClusterProfileRenderableSeries.LocalRenderContext _lastContext;

        public Color LineColor
        {
            get
            {
                return ( Color ) GetValue( ClusterProfileRenderableSeries.LineColorProperty );
            }
            set
            {
                SetValue( ClusterProfileRenderableSeries.LineColorProperty, ( object ) value );
            }
        }

        public Color TextColor
        {
            get
            {
                return ( Color ) GetValue( ClusterProfileRenderableSeries.TextColorProperty );
            }
            set
            {
                SetValue( ClusterProfileRenderableSeries.TextColorProperty, ( object ) value );
            }
        }

        public Color ClusterColor
        {
            get
            {
                return ( Color ) GetValue( ClusterProfileRenderableSeries.ClusterColorProperty );
            }
            set
            {
                SetValue( ClusterProfileRenderableSeries.ClusterColorProperty, ( object ) value );
            }
        }

        public Color ClusterMaxColor
        {
            get
            {
                return ( Color ) GetValue( ClusterProfileRenderableSeries.ClusterMaxColorProperty );
            }
            set
            {
                SetValue( ClusterProfileRenderableSeries.ClusterMaxColorProperty, ( object ) value );
            }
        }

        public ClusterProfileRenderableSeries()
        {
            DefaultStyleKey = ( object ) typeof( ClusterProfileRenderableSeries );
        }

        protected override void OnDrawImpl( IRenderContext2D renderContext, IRenderPassData renderPassData )
        {
            if ( !( DataSeries is TimeframeSegmentDataSeries ) )
                return;
            TimeframeSegmentPointSeries pointSeries = (TimeframeSegmentPointSeries) CurrentRenderPassData.PointSeries;
            TimeframeSegmentWrapper[] segments = pointSeries.Segments;
            double priceStep = pointSeries.PriceStep;
            ClusterProfileRenderableSeries.LocalRenderContext localRenderContext1 = new ClusterProfileRenderableSeries.LocalRenderContext();
            localRenderContext1.XCalc = CurrentRenderPassData.XCoordinateCalculator;
            localRenderContext1.YCalc = CurrentRenderPassData.YCoordinateCalculator;
            localRenderContext1.Timeframe = Timeframe;
            localRenderContext1.ScreenHeight = renderContext.ViewportSize.Height;
            localRenderContext1.ScreenWidth = renderContext.ViewportSize.Width;
            localRenderContext1.RenderContext = renderContext;
            localRenderContext1.DefaultFontColor = TextColor;
            ClusterProfileRenderableSeries.LocalRenderContext localRenderContext2 = localRenderContext1;
            _lastContext = localRenderContext1;
            ClusterProfileRenderableSeries.LocalRenderContext ctx = localRenderContext2;
            ctx.SegmentWidth = Math.Abs( ctx.XCalc.GetCoordinate( 1.0 ) - ctx.XCalc.GetCoordinate( 0.0 ) );
            ctx.PriceLevelHeight = Math.Abs( ctx.YCalc.GetCoordinate( segments[ 0 ].Segment.MinPrice ) - ctx.YCalc.GetCoordinate( segments[ 0 ].Segment.MinPrice + priceStep ) );
            ctx.HalfSegmentWidth = ctx.SegmentWidth / 2.0;
            ctx.HalfPriceLevelHeight = ctx.PriceLevelHeight / 2.0;
            Color mainColor = ClusterColor;
            Color maxColor = ClusterMaxColor;
            Color textColor = TextColor;
            double dataValue1 = ctx.YCalc.GetDataValue(ctx.ScreenHeight + ctx.PriceLevelHeight);
            double dataValue2 = ctx.YCalc.GetDataValue(-ctx.PriceLevelHeight);
            IndexRange visibleRange = pointSeries.VisibleRange;
            if ( segments.Length < 1 )
                return;
            if ( ctx.Timeframe < 1 )
                throw new InvalidOperationException( string.Format( "invalid timeframes. tf1={0}", ( object ) ctx.Timeframe ) );
            if ( dataValue1 > dataValue2 )
                throw new InvalidOperationException( string.Format( "minDrawPrice({0}) > maxDrawPrice({1})", ( object ) dataValue1, ( object ) dataValue2 ) );
            UltrachartDebugLogger.Instance.WriteLine( "ClusterProfile: started render {0} segments. Indexes: {1}-{2}, VisibleRange: {3}-{4}", ( object ) segments.Length, ( object ) segments[ 0 ].Segment.Index, ( object ) segments[ segments.Length - 1 ].Segment.Index, ( object ) visibleRange.Min, ( object ) visibleRange.Max );
            PenManager penManager = new PenManager(renderContext, false, (float) StrokeThickness, Opacity, (double[]) null);
            try
            {
                IPen2D pen = penManager.GetPen(LineColor);
                ctx.BarSeparatorPen = penManager.GetPen( Color.FromArgb( ( byte ) 50, byte.MaxValue, byte.MaxValue, byte.MaxValue ) );
                IEnumerable<TimeframeSegmentWrapper> source1 = ((IEnumerable<TimeframeSegmentWrapper>) segments).Where<TimeframeSegmentWrapper>((Func<TimeframeSegmentWrapper, bool>) (s =>
                {
                    if (s.Segment.Index >= visibleRange.Min)
                        return s.Segment.Index <= visibleRange.Max;
                    return false;
                }));
                if ( !source1.Any<TimeframeSegmentWrapper>() )
                    return;
                ClusterProfileRenderableSeries.LocalRenderContext localRenderContext3 = ctx;
                IEnumerable<TimeframeSegmentWrapper> source2 = source1;
                long num1;
                long num2 = num1 = source2.Max<TimeframeSegmentWrapper>((Func<TimeframeSegmentWrapper, long>) (s => s.Segment.MaxValue));
                localRenderContext3.VisibleMaxVolume = num1;
                long maxValue = num2;
                bool flag = ctx.SegmentWidth >= 3.0;
                foreach ( TimeframeSegmentWrapper timeframeSegmentWrapper in source1 )
                {
                    TimeframeDataSegment segment = timeframeSegmentWrapper.Segment;
                    double coordinate = ctx.XCalc.GetCoordinate(timeframeSegmentWrapper.X);
                    double segmentWidth = ctx.SegmentWidth;
                    double x = coordinate;
                    if ( timeframeSegmentWrapper.Segment.MinPrice <= dataValue2 && segment.MaxPrice >= dataValue1 )
                    {
                        double num3 = ctx.YCalc.GetCoordinate(segment.MaxPrice) - ctx.HalfPriceLevelHeight;
                        double y = ctx.YCalc.GetCoordinate(segment.MinPrice) + ctx.HalfPriceLevelHeight;
                        TimeframeDataSegment.PriceLevel[] data = segment.Values.ToArray<TimeframeDataSegment.PriceLevel>();
                        renderContext.DrawLine( pen, new Point( x, num3 + 1.0 ), new Point( x, y ) );
                        if ( flag )
                        {
                            long localMaxVal = segment.MaxValue;
                            ClusterProfileRenderableSeries.BarIterator bars = new ClusterProfileRenderableSeries.BarIterator(data.Length, maxValue, (Action<ClusterProfileRenderableSeries.BarIterator>) (it =>
                            {
                                TimeframeDataSegment.PriceLevel priceLevel = data[it.Index];
                                it.Coord = priceLevel.Price;
                                it.Value = priceLevel.Value;
                                it.BarBrush = penManager.GetBrush(it.Value == localMaxVal ? maxColor : mainColor);
                            }));
                            bars.FontColor = textColor;
                            double barMaxHeight = ctx.SegmentWidth - 1.0;
                            DrawHistogram( bars, ctx, x + 1.0, barMaxHeight, ClusterProfileRenderableSeries.DrawStartPoint.TopLeft, 0.0f );
                        }
                    }
                }
            }
            finally
            {
                if ( penManager != null )
                    penManager.Dispose();
            }
        }

        private void DrawHistogram( ClusterProfileRenderableSeries.BarIterator bars, ClusterProfileRenderableSeries.LocalRenderContext ctx, double baselineCoord, double barMaxHeight, ClusterProfileRenderableSeries.DrawStartPoint orientation, float minFontSize )
        {
            if ( barMaxHeight <= 1.0 )
                return;
            bool flag1;
            ICoordinateCalculator<double> coordinateCalculator;
            int num1;
            int num2;
            if ( orientation == ClusterProfileRenderableSeries.DrawStartPoint.BottomLeft || orientation == ClusterProfileRenderableSeries.DrawStartPoint.TopRight )
            {
                flag1 = true;
                coordinateCalculator = ctx.XCalc;
                num1 = 1;
                num2 = orientation == ClusterProfileRenderableSeries.DrawStartPoint.BottomLeft ? -1 : 1;
            }
            else
            {
                flag1 = false;
                coordinateCalculator = ctx.YCalc;
                num1 = orientation == ClusterProfileRenderableSeries.DrawStartPoint.TopLeft ? 1 : -1;
                num2 = -1;
            }
            bars.Reset();
            if ( flag1 )
            {
                bool flag2 = (double) minFontSize > 0.0 || _fontCalculator.CanDrawText(new Size(ctx.SegmentWidth, _fontCalculator.MinFontHeight), 1);
                AlignmentY txtAlignY = orientation == ClusterProfileRenderableSeries.DrawStartPoint.BottomLeft ? AlignmentY.Top : AlignmentY.Bottom;
                long maxValue = bars.MaxValue;
                while ( bars.NextBar() )
                {
                    long num3 = bars.Value;
                    double coordinate = coordinateCalculator.GetCoordinate(bars.Coord);
                    double a = baselineCoord;
                    double b1 = coordinate + ctx.SegmentWidth - 1.0;
                    double b2 = a + (double) num2 * (barMaxHeight * (double) num3 / (double) maxValue);
                    if ( coordinate > b1 )
                        MathHelper.Swap( ref coordinate, ref b1 );
                    if ( a > b2 )
                        MathHelper.Swap( ref a, ref b2 );
                    if ( b2 - a >= 0.5 )
                        DrawBar( ctx, new Point( coordinate, a ), new Point( b1, b2 ), bars.BarBrush, false, flag2 ? num3.ToString( ( IFormatProvider ) CultureInfo.InvariantCulture ) : ( string ) null, AlignmentX.Center, txtAlignY, bars.FontColor, minFontSize, barMaxHeight );
                }
            }
            else
            {
                bool flag2 = (double) minFontSize > 0.0 || ctx.PriceLevelHeight >= _fontCalculator.MinFontHeight && barMaxHeight >= _fontCalculator.MinDigitWidth;
                AlignmentX txtAlignX = orientation == ClusterProfileRenderableSeries.DrawStartPoint.TopLeft ? AlignmentX.Left : AlignmentX.Right;
                long maxValue = bars.MaxValue;
                while ( bars.NextBar() )
                {
                    long num3 = bars.Value;
                    double a1 = baselineCoord;
                    double a2 = coordinateCalculator.GetCoordinate(bars.Coord) - (double) num2 * ctx.HalfPriceLevelHeight;
                    double b1 = a1 + (maxValue > 0L ? (double) num1 * (barMaxHeight * (double) num3 / (double) maxValue) : 0.0);
                    double b2 = a2 + (double) num2 * ctx.PriceLevelHeight - (double) num2;
                    if ( a1 > b1 )
                        MathHelper.Swap( ref a1, ref b1 );
                    if ( a2 > b2 )
                        MathHelper.Swap( ref a2, ref b2 );
                    if ( b1 - a1 >= 0.5 )
                        DrawBar( ctx, new Point( a1, a2 ), new Point( b1, b2 ), bars.BarBrush, true, flag2 ? num3.ToString( ( IFormatProvider ) CultureInfo.InvariantCulture ) : ( string ) null, txtAlignX, AlignmentY.Center, bars.FontColor, minFontSize, barMaxHeight );
                }
            }
        }

        private void DrawBar( ClusterProfileRenderableSeries.LocalRenderContext ctx, Point pt1, Point pt2, IBrush2D brush, bool isHorizontalBar, string text, AlignmentX txtAlignX, AlignmentY txtAlignY, Color fontColor, float minFontSize, double barMaxHeight )
        {
            ctx.RenderContext.FillRectangle( brush, pt1, pt2, 0.0 );
            if ( isHorizontalBar )
            {
                if ( pt2.Y - pt1.Y >= 2.0 )
                    ctx.RenderContext.DrawLine( ctx.BarSeparatorPen, pt1, new Point( pt2.X, pt1.Y ) );
            }
            else if ( pt2.X - pt1.X >= 2.0 )
                ctx.RenderContext.DrawLine( ctx.BarSeparatorPen, pt1, new Point( pt1.X, pt2.Y ) );
            if ( text == null )
                return;
            Rect dstBoundingRect = new Rect(pt1, pt2);
            Tuple<float, FontWeight, bool> font = _fontCalculator.GetFont(dstBoundingRect.Size, text.Length, minFontSize);
            if ( font == null || !font.Item3 )
                return;
            ctx.RenderContext.DrawText( text, dstBoundingRect, txtAlignX, txtAlignY, fontColor, font.Item1, _fontCalculator.FontFamily, font.Item2 );
        }

        protected override HitTestInfo HitTestSeriesWithBody( Point rawPoint, HitTestInfo nearestHitPoint, double hitTestRadius )
        {
            TimeframeSegmentDataSeries dataSeries = DataSeries as TimeframeSegmentDataSeries;
            ClusterProfileRenderableSeries.LocalRenderContext lastContext = _lastContext;
            int dataSeriesIndex = nearestHitPoint.DataSeriesIndex;
            if ( dataSeries == null || dataSeries.Count < 1 || ( lastContext == null || dataSeriesIndex < 0 ) || dataSeriesIndex >= dataSeries.Count )
                return HitTestInfo.Empty;
            long visibleMaxVolume = lastContext.VisibleMaxVolume;
            if ( visibleMaxVolume == 0L )
                return HitTestInfo.Empty;
            long volume = nearestHitPoint.Volume;
            double coordinate = lastContext.XCalc.GetCoordinate((double) dataSeriesIndex);
            double x = coordinate + lastContext.SegmentWidth * (double) volume / (double) visibleMaxVolume;
            if ( rawPoint.X < coordinate || rawPoint.X > x )
                return HitTestInfo.Empty;
            nearestHitPoint.HitTestPoint = new Point( x, nearestHitPoint.HitTestPoint.Y );
            return nearestHitPoint;
        }

        private class LocalRenderContext
        {
            public ICoordinateCalculator<double> XCalc;
            public ICoordinateCalculator<double> YCalc;
            public int Timeframe;
            public double ScreenWidth;
            public double ScreenHeight;
            public double SegmentWidth;
            public double HalfSegmentWidth;
            public double PriceLevelHeight;
            public double HalfPriceLevelHeight;
            public Color DefaultFontColor;
            public long VisibleMaxVolume;
            public IRenderContext2D RenderContext;
            public IPen2D BarSeparatorPen;
        }

        private enum DrawStartPoint
        {
            BottomLeft,
            BottomRight,
            TopRight,
            TopLeft,
        }

        private class BarIterator
        {
            private readonly int _count;
            private readonly Action<ClusterProfileRenderableSeries.BarIterator> _onNextBar;
            private int _index;

            public int Index
            {
                get
                {
                    return _index;
                }
            }

            public BarIterator( int count, long maxValue, Action<ClusterProfileRenderableSeries.BarIterator> onNextBar )
            {
                _count = count;
                MaxValue = maxValue;
                _onNextBar = onNextBar;
            }

            public void Reset()
            {
                _index = -1;
            }

            public bool NextBar()
            {
                if ( ++_index >= _count )
                    return false;
                _onNextBar( this );
                return true;
            }

            public double Coord
            {
                get; set;
            }

            public long Value
            {
                get; set;
            }

            public IBrush2D BarBrush
            {
                get; set;
            }

            public Color FontColor
            {
                get; set;
            }

            public long MaxValue
            {
                get;
            }
        }
    }
}
