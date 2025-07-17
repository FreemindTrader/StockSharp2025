// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Visuals.RenderableSeries.TimeframeSegmentRenderableSeries
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Ecng.Xaml.Charting.Common;
using Ecng.Xaml.Charting.Common.Extensions;
using Ecng.Xaml.Charting.Licensing;
using Ecng.Xaml.Charting.Model.DataSeries.SegmentDataSeries;
using Ecng.Xaml.Charting.Numerics.CoordinateCalculators;
using Ecng.Xaml.Charting.Rendering.Common;
using Ecng.Xaml.Charting.Utility;
using StockSharp.Xaml.Licensing.Core;

namespace Ecng.Xaml.Charting.Visuals.RenderableSeries
{
    [UltrachartLicenseProvider( typeof( RenderableSeriesUltrachartLicenseProvider ) )]
    public abstract class TimeframeSegmentRenderableSeries : BaseRenderableSeries
    {
        private static readonly Brush _defaultVerticalVolumeBrush = (Brush) new LinearGradientBrush(Color.FromRgb((byte) 0, (byte) 128, (byte) 0), Color.FromRgb((byte) 0, (byte) 15, (byte) 0), 90.0);
        public static readonly DependencyProperty LocalHorizontalVolumesProperty = DependencyProperty.Register(nameof (LocalHorizontalVolumes), typeof (bool), typeof (TimeframeSegmentRenderableSeries), new PropertyMetadata((object) false, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty ShowHorizontalVolumesProperty = DependencyProperty.Register(nameof (ShowHorizontalVolumes), typeof (bool), typeof (TimeframeSegmentRenderableSeries), new PropertyMetadata((object) true, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty HorizontalVolumeWidthFractionProperty = DependencyProperty.Register(nameof (HorizontalVolumeWidthFraction), typeof (double), typeof (TimeframeSegmentRenderableSeries), new PropertyMetadata((object) 0.15, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface), new CoerceValueCallback(TimeframeSegmentRenderableSeries.CoerceHorizontalVolumeWidthFraction)));
        public static readonly DependencyProperty VolumeBarsBrushProperty = DependencyProperty.Register(nameof (VolumeBarsBrush), typeof (Brush), typeof (TimeframeSegmentRenderableSeries), new PropertyMetadata((object) TimeframeSegmentRenderableSeries._defaultVerticalVolumeBrush, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty VolBarsFontColorProperty = DependencyProperty.Register(nameof (VolBarsFontColor), typeof (Color), typeof (TimeframeSegmentRenderableSeries), new PropertyMetadata((object) Colors.White, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        private Dictionary<double, Tuple<double, long>> _horizVolsWidths = new Dictionary<double, Tuple<double, long>>();
        protected const string _defaultFontFamily = "Tahoma";
        protected TimeframeSegmentRenderableSeries.FontCalculator _fontCalculator;

        public bool LocalHorizontalVolumes
        {
            get
            {
                return ( bool ) GetValue( TimeframeSegmentRenderableSeries.LocalHorizontalVolumesProperty );
            }
            set
            {
                SetValue( TimeframeSegmentRenderableSeries.LocalHorizontalVolumesProperty, ( object ) value );
            }
        }

        public bool ShowHorizontalVolumes
        {
            get
            {
                return ( bool ) GetValue( TimeframeSegmentRenderableSeries.ShowHorizontalVolumesProperty );
            }
            set
            {
                SetValue( TimeframeSegmentRenderableSeries.ShowHorizontalVolumesProperty, ( object ) value );
            }
        }

        public double HorizontalVolumeWidthFraction
        {
            get
            {
                return ( double ) GetValue( TimeframeSegmentRenderableSeries.HorizontalVolumeWidthFractionProperty );
            }
            set
            {
                SetValue( TimeframeSegmentRenderableSeries.HorizontalVolumeWidthFractionProperty, ( object ) value );
            }
        }

        public Brush VolumeBarsBrush
        {
            get
            {
                return ( Brush ) GetValue( TimeframeSegmentRenderableSeries.VolumeBarsBrushProperty );
            }
            set
            {
                SetValue( TimeframeSegmentRenderableSeries.VolumeBarsBrushProperty, ( object ) value );
            }
        }

        public Color VolBarsFontColor
        {
            get
            {
                return ( Color ) GetValue( TimeframeSegmentRenderableSeries.VolBarsFontColorProperty );
            }
            set
            {
                SetValue( TimeframeSegmentRenderableSeries.VolBarsFontColorProperty, ( object ) value );
            }
        }

        public double PriceScale
        {
            get
            {
                return ( ( TimeframeSegmentDataSeries ) DataSeries ).Return<TimeframeSegmentDataSeries, double>( ( Func<TimeframeSegmentDataSeries, double> ) ( ser => ser.PriceStep ), 10.0 );
            }
        }

        public int Timeframe
        {
            get
            {
                return ( ( TimeframeSegmentDataSeries ) DataSeries ).Return<TimeframeSegmentDataSeries, int>( ( Func<TimeframeSegmentDataSeries, int> ) ( ser => ser.Timeframe ), 1 );
            }
        }

        protected internal override bool IsPartOfExtendedFeatures
        {
            get
            {
                return true;
            }
        }

        protected override HitTestInfo HitTestInternal( Point rawPoint, double hitTestRadius, bool interpolate )
        {
            return base.HitTestInternal( rawPoint, hitTestRadius, false );
        }

        private static object CoerceHorizontalVolumeWidthFraction( DependencyObject d, object newVal )
        {
            double num = (double) newVal;
            if ( num < 0.0 )
                return ( object ) 0.0;
            if ( num <= 1.0 )
                return newVal;
            return ( object ) 1.0;
        }

        protected abstract void OnDrawImpl( IRenderContext2D renderContext, IRenderPassData renderPassData );

        protected override void InternalDraw( IRenderContext2D renderContext, IRenderPassData renderPassData )
        {
            if ( renderPassData.IsVerticalChart )
                throw new NotSupportedException( "vertical charts not supported" );
            if ( CurrentRenderPassData.XCoordinateCalculator.HasFlippedCoordinates || CurrentRenderPassData.YCoordinateCalculator.HasFlippedCoordinates )
                throw new NotSupportedException( "flipped axes not supported" );
            if ( CurrentRenderPassData.PointSeries.Count < 1 || !( DataSeries is TimeframeSegmentDataSeries ) )
                return;
            if ( _fontCalculator == null )
            {
                string fontFamily = FontFamily != null ? FontFamily.Source : "Tahoma";
                float maxFontSize = ((float) FontSize).Round(0.5f);
                FontWeight fontWeight = FontWeight;
                _fontCalculator = new TimeframeSegmentRenderableSeries.FontCalculator( renderContext, fontFamily, maxFontSize, fontWeight );
            }
            OnDrawImpl( renderContext, renderPassData );
            if ( ShowHorizontalVolumes )
                DrawHorizontalVolumes( renderContext, renderPassData );
            else
                _horizVolsWidths = new Dictionary<double, Tuple<double, long>>();
        }

        protected void DrawGrid( IRenderContext2D renderContext, ISeriesDrawingHelper drawingHelper, Point pt1, Point pt2, int xCount, int yCount, IPen2D framePen, IPen2D gridPen, IBrush2D fillBrush )
        {
            if ( pt1.X >= pt2.X || pt1.Y >= pt2.Y )
                return;
            double num1 = pt2.X - pt1.X;
            double num2 = pt2.Y - pt1.Y;
            double num3 = (pt2.X - pt1.X) / (double) xCount;
            double num4 = (pt2.Y - pt1.Y) / (double) yCount;
            double num5 = 1.0;
            if ( num1 > num5 && num2 > 1.0 )
            {
                if ( num3 > 2.0 && num4 > 2.0 )
                {
                    for ( int index = 1 ; index < xCount ; ++index )
                    {
                        double x = pt1.X + (double) index * num3;
                        drawingHelper.DrawLine( new Point( x, pt1.Y ), new Point( x, pt2.Y ), gridPen );
                    }
                    for ( int index = 1 ; index < yCount ; ++index )
                    {
                        double y = pt1.Y + (double) index * num4;
                        drawingHelper.DrawLine( new Point( pt1.X, y ), new Point( pt2.X, y ), gridPen );
                    }
                }
                else
                    renderContext.FillRectangle( fillBrush, pt1, pt2, 0.0 );
            }
            drawingHelper.DrawQuad( framePen, pt1, pt2 );
        }

        protected void FillPeriodSegments( List<TimeframeSegmentWrapper> buf, TimeframeSegmentWrapper[ ] arr, int index, int tf )
        {
            buf.Clear();
            Tuple<DateTime, DateTime, int> timeframePeriod = TimeframeSegmentDataSeries.GetTimeframePeriod(arr[index].Segment.Time, tf, (Tuple<DateTime, DateTime, int>) null);
            do
            {
                buf.Add( arr[ index ] );
            }
            while ( index < arr.Length - 1 && !( arr[ ++index ].Segment.Time >= timeframePeriod.Item2 ) );
        }

        private void DrawHorizontalVolumes( IRenderContext2D renderContext, IRenderPassData renderPassData )
        {
            TimeframeSegmentDataSeries series = DataSeries as TimeframeSegmentDataSeries;
            if ( series == null )
                return;
            int num1 = LocalHorizontalVolumes ? 1 : 0;
            TimeframeSegmentPointSeries pointSeries = (TimeframeSegmentPointSeries) CurrentRenderPassData.PointSeries;
            double priceStep = pointSeries.PriceStep;
            TimeframeSegmentWrapper[] segments = pointSeries.Segments;
            ICoordinateCalculator<double> ycoordinateCalculator = CurrentRenderPassData.YCoordinateCalculator;
            Size viewportSize = renderContext.ViewportSize;
            double width = viewportSize.Width;
            viewportSize = renderContext.ViewportSize;
            double height = viewportSize.Height;
            double num2 = Math.Abs(ycoordinateCalculator.GetCoordinate(segments[0].Segment.MinPrice) - ycoordinateCalculator.GetCoordinate(segments[0].Segment.MinPrice + priceStep));
            double num3 = num2 / 2.0;
            double maxDrawPrice = ycoordinateCalculator.GetDataValue(-num2);
            double minDrawPrice = ycoordinateCalculator.GetDataValue(height + num2);
            IEnumerable<double> source = num1 != 0 ? pointSeries.AllPrices.Where<double>((Func<double, bool>) (p =>
            {
                if (p > minDrawPrice)
                    return p < maxDrawPrice;
                return false;
            })) : (IEnumerable<double>) TimeframeSegmentDataSeries.GeneratePrices(minDrawPrice, maxDrawPrice, priceStep);
            KeyValuePair<double, long>[] array = (num1 != 0 ? (IEnumerable<KeyValuePair<double, long>>) source.ToDictionary<double, double, long>((Func<double, double>) (price => price), new Func<double, long>(pointSeries.GetVolumeByPrice)) : (IEnumerable<KeyValuePair<double, long>>) source.ToDictionary<double, double, long>((Func<double, double>) (price => price), (Func<double, long>) (price => series.GetVolumeByPrice(price, priceStep)))).Where<KeyValuePair<double, long>>((Func<KeyValuePair<double, long>, bool>) (kv =>
            {
                if (kv.Key > minDrawPrice && kv.Key < maxDrawPrice)
                    return kv.Value > 0L;
                return false;
            })).ToArray<KeyValuePair<double, long>>();
            Brush volumeBarsBrush = VolumeBarsBrush;
            Color volBarsFontColor = VolBarsFontColor;
            LinearGradientBrush linearGradientBrush = volumeBarsBrush as LinearGradientBrush;
            Dictionary<double, Tuple<double, long>> dictionary = new Dictionary<double, Tuple<double, long>>();
            if ( !( ( IEnumerable<KeyValuePair<double, long>> ) array ).Any<KeyValuePair<double, long>>() )
                return;
            using ( PenManager penManager = new PenManager( renderContext, false, ( float ) StrokeThickness, Opacity, ( double[ ] ) null ) )
            {
                IPen2D pen = penManager.GetPen(linearGradientBrush != null ? linearGradientBrush.GradientStops[0].Color : volBarsFontColor);
                IBrush2D brush = penManager.GetBrush(volumeBarsBrush);
                long num4 = ((IEnumerable<KeyValuePair<double, long>>) array).Max<KeyValuePair<double, long>>((Func<KeyValuePair<double, long>, long>) (kv => kv.Value));
                double volumeWidthFraction = HorizontalVolumeWidthFraction;
                foreach ( KeyValuePair<double, long> keyValuePair in array )
                {
                    double y1 = ycoordinateCalculator.GetCoordinate(keyValuePair.Key) - num3;
                    double y2 = y1 + num2;
                    double x = width * (double) keyValuePair.Value * volumeWidthFraction / (double) num4;
                    Point pt1 = new Point(0.0, y1);
                    Point point = new Point(x, y2);
                    if ( num2 > 1.5 )
                    {
                        renderContext.DrawLine( pen, pt1, new Point( x, y1 ) );
                        renderContext.FillRectangle( brush, new Point( 0.0, y1 + 1.0 ), point, 3.0 * Math.PI / 2.0 );
                    }
                    else
                        renderContext.FillRectangle( brush, pt1, point, 3.0 * Math.PI / 2.0 );
                    dictionary[ keyValuePair.Key ] = Tuple.Create<double, long>( x, keyValuePair.Value );
                    string text = keyValuePair.Value.ToString((IFormatProvider) CultureInfo.InvariantCulture);
                    Rect dstBoundingRect = new Rect(new Point(1.0, y1), point);
                    Tuple<float, FontWeight, bool> font = _fontCalculator.GetFont(dstBoundingRect.Size, (int) keyValuePair.Value.NumDigitsInPositiveNumber(), 9f);
                    if ( font.Item3 )
                        renderContext.DrawText( text, dstBoundingRect, AlignmentX.Left, AlignmentY.Center, volBarsFontColor, font.Item1, _fontCalculator.FontFamily, font.Item2 );
                }
                _horizVolsWidths = dictionary;
            }
        }

        protected override HitTestInfo NearestHitResult( Point mouseRawPoint, double hitTestRadiusInPixels, SearchMode searchMode, bool considerYCoordinateForDistanceCalculation )
        {
            TimeframeSegmentDataSeries dataSeries = DataSeries as TimeframeSegmentDataSeries;
            if ( dataSeries == null || dataSeries.Count < 1 )
                return HitTestInfo.Empty;
            double priceStep = dataSeries.PriceStep;
            ICoordinateCalculator<double> xcoordinateCalculator = CurrentRenderPassData.XCoordinateCalculator;
            ICoordinateCalculator<double> ycoordinateCalculator = CurrentRenderPassData.YCoordinateCalculator;
            int dataValue1 = (int) xcoordinateCalculator.GetDataValue(mouseRawPoint.X);
            double dataValue2 = ycoordinateCalculator.GetDataValue(mouseRawPoint.Y);
            double num1 = dataValue2.NormalizePrice(priceStep);
            double num2 = Math.Abs(xcoordinateCalculator.GetCoordinate(1.0) - xcoordinateCalculator.GetCoordinate(0.0));
            double num3 = Math.Abs(ycoordinateCalculator.GetCoordinate(num1) - ycoordinateCalculator.GetCoordinate(num1 + priceStep));
            double num4 = 1.0;
            if ( num2 < num4 || num3 < 1.0 )
                return HitTestInfo.Empty;
            Tuple<double, long> tuple;
            if ( ShowHorizontalVolumes && _horizVolsWidths.TryGetValue( num1, out tuple ) && mouseRawPoint.X <= tuple.Item1 )
                return new HitTestInfo()
                {
                    DataSeriesName = dataSeries.SeriesName,
                    DataSeriesType = dataSeries.DataSeriesType,
                    YValue = ( IComparable ) num1,
                    Volume = tuple.Item2,
                    IsHit = true,
                    HitTestPoint = new Point( tuple.Item1, ycoordinateCalculator.GetCoordinate( num1 ) )
                };
            if ( dataValue1 < 0 || dataValue1 >= dataSeries.Count )
                return HitTestInfo.Empty;
            TimeframeDataSegment segment = dataSeries.Segments[dataValue1];
            long valueByPrice = segment.GetValueByPrice(dataValue2);
            if ( valueByPrice == 0L )
                return HitTestInfo.Empty;
            HitTestInfo nearestHitPoint = new HitTestInfo()
            {
                DataSeriesName = dataSeries.SeriesName,
                DataSeriesType = dataSeries.DataSeriesType,
                XValue = (IComparable) segment.Time,
                YValue = (IComparable) num1,
                DataSeriesIndex = dataValue1,
                Volume = valueByPrice,
                IsHit = true,
                HitTestPoint = new Point(xcoordinateCalculator.GetCoordinate((double) dataValue1), ycoordinateCalculator.GetCoordinate(num1))
            };
            return HitTestSeriesWithBody( mouseRawPoint, nearestHitPoint, hitTestRadiusInPixels );
        }

        protected override HitTestInfo HitTestSeriesWithBody( Point rawPoint, HitTestInfo nearestHitPoint, double hitTestRadius )
        {
            return nearestHitPoint;
        }

        protected class FontCalculator
        {
            private readonly Dictionary<Tuple<int, int>, TimeframeSegmentRenderableSeries.FontCalculator.FontInfo> _fontInfosDict = new Dictionary<Tuple<int, int>, TimeframeSegmentRenderableSeries.FontCalculator.FontInfo>();
            private readonly Dictionary<float, TimeframeSegmentRenderableSeries.FontCalculator.FontInfo> _fontInfoBySizeDict = new Dictionary<float, TimeframeSegmentRenderableSeries.FontCalculator.FontInfo>();
            private readonly Stopwatch _watch = new Stopwatch();
            private const float MinFontSize = 7f;
            private const float MaxFontSize = 32f;
            private const float FontSizeStep = 0.5f;
            private readonly TimeframeSegmentRenderableSeries.FontCalculator.FontInfo[] _fontInfos;
            private readonly TimeframeSegmentRenderableSeries.FontCalculator.FontInfo _biggestFont;
            private readonly TimeframeSegmentRenderableSeries.FontCalculator.FontInfo _smallestFont;
            private readonly string _fontFamily;

            public string FontFamily
            {
                get
                {
                    return _fontFamily;
                }
            }

            public double MinFontHeight
            {
                get
                {
                    return _smallestFont.SymbolDimensions.Height;
                }
            }

            public double MinDigitWidth
            {
                get
                {
                    return _smallestFont.SymbolDimensions.Width;
                }
            }

            public FontCalculator( IRenderContext2D renderContext, string fontFamily, float maxFontSize, FontWeight weightAtMaxSize )
            {
                _watch.Restart();
                _fontFamily = fontFamily;
                maxFontSize = Math.Min( 32f, maxFontSize ).Round( 0.5f );
                _fontInfos = new TimeframeSegmentRenderableSeries.FontCalculator.FontInfo[ 1 + ( int ) Math.Round( ( ( double ) maxFontSize - 7.0 ) / 0.5 ) ];
                float num1 = 7f;
                while ( ( double ) num1 <= ( double ) maxFontSize )
                {
                    FontWeight fontWeight = (double) num1 <= 8.5 ? FontWeights.ExtraLight : ((double) num1 <= 10.0 ? FontWeights.Light : weightAtMaxSize);
                    Size dimensions = renderContext.DigitMaxSize(num1, _fontFamily, fontWeight);
                    _fontInfos[ ( int ) Math.Round( ( ( double ) num1 - 7.0 ) / 0.5 ) ] = new TimeframeSegmentRenderableSeries.FontCalculator.FontInfo( num1, fontWeight, dimensions );
                    num1 += 0.5f;
                }
                _smallestFont = _fontInfos[ 0 ];
                _biggestFont = _fontInfos[ _fontInfos.Length - 1 ];
                double d1;
                double d2 = d1 = double.MaxValue;
                double a1;
                double a2 = a1 = double.MinValue;
                foreach ( TimeframeSegmentRenderableSeries.FontCalculator.FontInfo fontInfo in _fontInfos )
                {
                    Size symbolDimensions = fontInfo.SymbolDimensions;
                    double width = symbolDimensions.Width;
                    symbolDimensions = fontInfo.SymbolDimensions;
                    double height = symbolDimensions.Height;
                    if ( width < d2 )
                        d2 = width;
                    if ( width > a2 )
                        a2 = width;
                    if ( height < d1 )
                        d1 = height;
                    if ( height > a1 )
                        a1 = height;
                    _fontInfosDict[ Tuple.Create<int, int>( ( int ) Math.Round( width ), ( int ) Math.Round( height ) ) ] = fontInfo;
                    _fontInfoBySizeDict[ fontInfo.FontSize ] = fontInfo;
                }
                int num2 = (int) Math.Floor(d2);
                int num3 = (int) Math.Ceiling(a2);
                int num4 = (int) Math.Floor(d1);
                int num5 = (int) Math.Ceiling(a1);
                int[] array = Enumerable.Range(0, _fontInfos.Length).Reverse<int>().ToArray<int>();
                for ( int w = num2 ; w <= num3 ; ++w )
                {
                    for ( int h = num4 ; h <= num5 ; ++h )
                    {
                        Tuple<int, int> key = Tuple.Create<int, int>(w, h);
                        if ( !_fontInfosDict.ContainsKey( key ) )
                            _fontInfosDict[ key ] = ( ( IEnumerable<int> ) array ).Select<int, TimeframeSegmentRenderableSeries.FontCalculator.FontInfo>( ( Func<int, TimeframeSegmentRenderableSeries.FontCalculator.FontInfo> ) ( i => _fontInfos[ i ] ) ).First<TimeframeSegmentRenderableSeries.FontCalculator.FontInfo>( ( Func<TimeframeSegmentRenderableSeries.FontCalculator.FontInfo, bool> ) ( fi =>
                            {
                                if ( fi.SymbolDimensions.Width <= ( double ) w )
                                    return fi.SymbolDimensions.Height <= ( double ) h;
                                return false;
                            } ) );
                    }
                }
                _watch.Stop();
                UltrachartDebugLogger.Instance.WriteLine( "Initialized font calculator. Font={0}, MaxSize={1}, MaxWeight={2}, dict.Size={3}, initTime={4:F3}ms", ( object ) fontFamily, ( object ) maxFontSize, ( object ) weightAtMaxSize, ( object ) _fontInfosDict.Count, ( object ) _watch.Elapsed.TotalMilliseconds );
            }

            public Tuple<float, FontWeight, bool> GetFont( Size area, int numSymbols, float minFontSize = 0.0f )
            {
                numSymbols = Math.Max( numSymbols, 1 );
                if ( ( double ) minFontSize != 0.0 )
                    minFontSize = Math.Min( Math.Max( minFontSize, 7f ), 32f ).Round( 0.5f );
                try
                {
                    double num1 = area.Width / (double) numSymbols;
                    double num2 = num1;
                    Size symbolDimensions = _smallestFont.SymbolDimensions;
                    double width1 = symbolDimensions.Width;
                    if ( num2 >= width1 )
                    {
                        double height1 = area.Height;
                        symbolDimensions = _smallestFont.SymbolDimensions;
                        double height2 = symbolDimensions.Height;
                        if ( height1 >= height2 )
                        {
                            double num3 = num1;
                            symbolDimensions = _biggestFont.SymbolDimensions;
                            double width2 = symbolDimensions.Width;
                            if ( num3 >= width2 )
                            {
                                double height3 = area.Height;
                                symbolDimensions = _biggestFont.SymbolDimensions;
                                double height4 = symbolDimensions.Height;
                                if ( height3 >= height4 )
                                    return Tuple.Create<float, FontWeight, bool>( _biggestFont.FontSize, _biggestFont.FontWeight, true );
                            }
                            double val1 = num1;
                            symbolDimensions = _biggestFont.SymbolDimensions;
                            double width3 = symbolDimensions.Width;
                            int num4 = (int) Math.Floor(Math.Min(val1, width3));
                            double height5 = area.Height;
                            symbolDimensions = _biggestFont.SymbolDimensions;
                            double height6 = symbolDimensions.Height;
                            int num5 = (int) Math.Floor(Math.Min(height5, height6));
                            TimeframeSegmentRenderableSeries.FontCalculator.FontInfo fontInfo1 = _fontInfosDict[Tuple.Create<int, int>(num4, num5)];
                            if ( ( double ) minFontSize == 0.0 || ( double ) fontInfo1.FontSize >= ( double ) minFontSize )
                                return Tuple.Create<float, FontWeight, bool>( fontInfo1.FontSize, fontInfo1.FontWeight, true );
                            TimeframeSegmentRenderableSeries.FontCalculator.FontInfo fontInfo2 = _fontInfoBySizeDict[minFontSize];
                            return Tuple.Create<float, FontWeight, bool>( fontInfo2.FontSize, fontInfo2.FontWeight, false );
                        }
                    }
                    if ( ( double ) minFontSize == 0.0 )
                        return ( Tuple<float, FontWeight, bool> ) null;
                    TimeframeSegmentRenderableSeries.FontCalculator.FontInfo fontInfo = _fontInfoBySizeDict[minFontSize];
                    return Tuple.Create<float, FontWeight, bool>( fontInfo.FontSize, fontInfo.FontWeight, false );
                }
                catch ( Exception ex )
                {
                    UltrachartDebugLogger.Instance.WriteLine( "GetFont({0}x{1}, {2}, {3:0.##}) error: {4}", ( object ) area.Width, ( object ) area.Height, ( object ) numSymbols, ( object ) minFontSize, ( object ) ex );
                    TimeframeSegmentRenderableSeries.FontCalculator.FontInfo fontInfo = _fontInfoBySizeDict[Math.Max(minFontSize, 7f)];
                    return Tuple.Create<float, FontWeight, bool>( fontInfo.FontSize, fontInfo.FontWeight, false );
                }
            }

            public bool CanDrawText( Size area, int numSymbols )
            {
                return GetFont( area, numSymbols, 0.0f ) != null;
            }

            private class FontInfo
            {
                private readonly float _fontSize;
                private readonly FontWeight _fontWeight;
                private readonly Size _symbolDimensions;

                public float FontSize
                {
                    get
                    {
                        return _fontSize;
                    }
                }

                public FontWeight FontWeight
                {
                    get
                    {
                        return _fontWeight;
                    }
                }

                public Size SymbolDimensions
                {
                    get
                    {
                        return _symbolDimensions;
                    }
                }

                public FontInfo( float size, FontWeight weight, Size dimensions )
                {
                    _fontSize = size.Round( 0.5f );
                    _fontWeight = weight;
                    _symbolDimensions = dimensions;
                }
            }
        }
    }
}
