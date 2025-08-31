using SciChart.Charting.Common.Extensions;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Numerics.CoordinateCalculators;
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Charting.Visuals.RenderableSeries.HitTesters;
using SciChart.Core.Utility;
using SciChart.Drawing.Common;
using StockSharp.Messages;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;
using Brush = System.Windows.Media.Brush;
using Color = System.Windows.Media.Color;
using Point = System.Windows.Point;
using Size = System.Windows.Size;


/// <summary>
/// TimeframeSegmentRenderableSeries is used for volume profile data for a certain time period.
/// 
/// I believe we can use this chart to know where there is resistance and support for a given price level.
/// 
/// </summary>
public class TimeframeSegmentRenderableSeries : CustomRenderableSeries
{
    /// <summary>
    /// Since we have defined our own Drawing Provider and HitTest provider, we need to set them up in the constructor
    /// </summary>
    public TimeframeSegmentRenderableSeries()
    {        
        HitTestProvider = new TfsHitTestProvider(this);
    }

    /// <summary>
    /// I am using this metadata class to provide additional information when user clicks on a TimeframeSegmentRenderableSeries
    /// 
    /// The one piece of specific info is CandlePriceLevel.
    /// </summary>
    public class MyMetadata : IPointMetadata
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MyMetadata(CandlePriceLevel level )
        {
            CandlePriceLevel = level;
        }

        public bool IsSelected { get; set; }
        public CandlePriceLevel CandlePriceLevel { get; set; }
    }

    //public class TFSegmentDrawingProvider : CustomSeriesDrawingProvider
    //{
    //    public TFSegmentDrawingProvider(TimeframeSegmentRenderableSeries renderableSeries) : base(renderableSeries)
    //    {
    //    }

    //    public override void OnDraw(IRenderContext2D renderContext, IRenderPassData renderPassData)
    //    {
    //        base.OnDraw(renderContext, renderPassData);


    //    }
    //}

    /// <summary>
    /// 
    //  TimeFrameSegment HitTest Provider to provide specific information when user clicks on or near the TimeframeSegmentRenderableSeries
    //
    // Type parameters:
    //                      TimeframeSegmentRenderableSeries:
    /// </summary>
    public class TfsHitTestProvider : DefaultHitTestProvider<TimeframeSegmentRenderableSeries>
    {
        public TfsHitTestProvider(TimeframeSegmentRenderableSeries renderSeries) : base(renderSeries)
        {
        }


        protected override HitTestInfo HitTestInternal(Point rawPoint, double hitTestRadius, bool interpolate)        
        {
            return base.HitTestInternal(rawPoint, hitTestRadius, false);
        }


        //
        // Summary:
        //     Called by SciChart.Charting.Visuals.RenderableSeries.HitTesters.DefaultHitTestProvider`1.HitTest(System.Windows.Point,System.Boolean)
        //     to get the nearest (non-interpolated) SciChart.Charting.Visuals.RenderableSeries.HitTestInfo
        //     to the mouse point
        //
        // Parameters:
        //   mouseRawPoint:
        //     The mouse point
        //
        //   hitTestRadiusInPixels:
        //     The radius (in pixels) to use when determining if the mouseRawPoint is over a
        //     data-point
        //
        //   searchMode:
        //     The search mode.
        //
        //   considerYCoordinateForDistanceCalculation:
        //     if set to true then perform a true euclidean distance to find the nearest hit
        //     result.
        //
        // Returns:
        //     The SciChart.Charting.Visuals.RenderableSeries.HitTestInfo result
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     hitTestRadiusInPixels is NaN
        //
        //   T:System.NotImplementedException:
        protected override HitTestInfo NearestHitResult( Point mouseRawPoint, double hitTestRadiusInPixels, SearchMode searchMode, bool considerYCoordinateForDistanceCalculation)
        {
            if ( !( RenderableSeries.DataSeries is TimeframeSegmentDataSeries dataSeries ) || dataSeries.Count < 1 )
                return HitTestInfo.Empty;

            var tfs       = RenderableSeries as TimeframeSegmentRenderableSeries;

            var priceStep = (double)( tfs.PriceStep ?? 0.000001M );

            var xCalc     = RenderableSeries.CurrentRenderPassData.XCoordinateCalculator;
            var yCalc     = RenderableSeries.CurrentRenderPassData.YCoordinateCalculator;

            var index     = (int)xCalc.GetDataValue(mouseRawPoint.X);
            var yValue    = yCalc.GetDataValue(mouseRawPoint.Y);
            var yRounded  = yValue.NormalizePrice(priceStep);

            // Transforms a data value into a pixel coordinate. The following is one Unit Pixel equivalent
            var xUnit     = Math.Abs(xCalc.GetCoordinate(1.0) - xCalc.GetCoordinate(0.0));
            var yUnit     = Math.Abs(yCalc.GetCoordinate(yRounded) - yCalc.GetCoordinate(yRounded + priceStep));

            if ( xUnit < 1.0 || yUnit < 1.0 )
                return HitTestInfo.Empty;
            
            
            if ( tfs.ShowHorizontalVolumes && tfs._candlePriceLevelMap.TryGetValue(yRounded, out var doubleCandlePriceLevels) && mouseRawPoint.X <= doubleCandlePriceLevels.Item1 )
            {
                var ht2            = new HitTestInfo();

                ht2.DataSeriesName = dataSeries.SeriesName;
                ht2.DataSeriesType = dataSeries.DataSeriesType;
                ht2.YValue         = (IComparable)yRounded;
                ht2.Metadata       = new MyMetadata(  doubleCandlePriceLevels.Item2 );
                ht2.IsHit          = true;
                ht2.HitTestPoint   = new Point(doubleCandlePriceLevels.Item1, yCalc.GetCoordinate(yRounded));
                return ht2;
            }

            if ( index < 0 || index >= dataSeries.Count )
                return HitTestInfo.Empty;

            TimeframeDataSegment segment = dataSeries.Segments[index];
            
            var candlePriceLevel = segment.GetCPLFromPriceStep(yValue, priceStep);

            if ( candlePriceLevel.TotalVolume == 0M )
                return HitTestInfo.Empty;

            HitTestInfo ht     = new HitTestInfo();
            ht.DataSeriesName  = dataSeries.SeriesName;
            ht.DataSeriesType  = dataSeries.DataSeriesType;
            ht.XValue          = (IComparable) segment.Time;
            ht.YValue          = (IComparable)yRounded;
            ht.DataSeriesIndex = index;
            ht.Metadata        = new MyMetadata(candlePriceLevel);            
            ht.IsHit           = true;
            ht.HitTestPoint    = new Point(xCalc.GetCoordinate((double)index), yCalc.GetCoordinate(yRounded));
            ht.OpenValue       = (IComparable) segment.OpenPrice;
            ht.HighValue       = (IComparable) segment.HighPrice;
            ht.LowValue        = (IComparable) segment.LowPrice;
            ht.CloseValue      = (IComparable) segment.ClosePrice;
            
            return HitTestSeriesWithBody(mouseRawPoint, ht, hitTestRadiusInPixels);
        }

        protected override HitTestInfo HitTestSeriesWithBody(Point _param1, HitTestInfo ht, double _param3)
        {
            return ht;
        }        
    }
    
    /// <summary>
    /// Given a font Family and minimum size and font weight, Font Builder will build a cached of Font with increment of 0.5f
    /// </summary>
    protected sealed class FontCalculator
    {
        private readonly FontInfo[] _fontInfos;
        private readonly FontInfo _biggestFont;
        private readonly FontInfo _smallestFont;
        private readonly string _fontFamily;
        private readonly Dictionary<Tuple<int, int>, FontInfo> _fontInfosDict = new Dictionary<Tuple<int, int>, FontInfo>();
        private readonly Dictionary<float, FontInfo> _fontInfoBySizeDict = new Dictionary<float, FontInfo>();
        private readonly Stopwatch _watch = new Stopwatch();

        /// <summary>
        /// This FontCalculator constructor will build a cache of fonts with increment of 0.5f from 7f to minFontSize.
        /// 
        /// Different font family will have different pixel size for digits [0,1,2,3,4,5,6,7,8,9]
        /// </summary>
        /// <param name="renderContext"></param>
        /// <param name="fontFamily"></param>
        /// <param name="minFontSize"></param>
        /// <param name="fontWeight"></param>
        public FontCalculator(IRenderContext2D renderContext, string fontFamily, float minFontSize, FontWeight fontWeight)
        {
            _watch.Restart();

            _fontFamily = fontFamily;
            minFontSize = Math.Min(32f, minFontSize).Round(0.5f);
            _fontInfos  = new FontInfo[1 + (int)Math.Round(( (double)minFontSize - 7.0 ) / 0.5)];
            
            for ( float i = 7f; (double)i <= (double)minFontSize; i += 0.5f )
            {
                var fWeight = (double)i <= 8.5 ? FontWeights.ExtraLight : ( (double)i <= 10.0 ? FontWeights.Light : fontWeight );

                // Different font family will have different pixel size for digits [0,1,2,3,4,5,6,7,8,9]
                var maxDigitSize = renderContext.DigitMaxSize(i, _fontFamily, fWeight);

                _fontInfos[(int)Math.Round(( (double)i - 7.0 ) / 0.5)] = new FontInfo(i, fWeight, maxDigitSize);
            }

            _smallestFont = _fontInfos[0];            
            _biggestFont  = _fontInfos[_fontInfos.Length - 1];

            var minHeight = double.MaxValue;
            var minWidth  =  double.MaxValue;
            
            var maxHeight = double.MinValue;
            var maxWidth  = double.MinValue;
            
            foreach ( FontInfo fi in _fontInfos )
            {                
                var width = fi.SymbolDimensions.Width;                
                var height = fi.SymbolDimensions.Height;

                if ( width < minWidth )
                    minWidth = width;

                if ( width > maxWidth )
                    maxWidth = width;

                if ( height < minHeight )
                    minHeight = height;

                if ( height > maxHeight )
                    maxHeight = height;

                _fontInfosDict[Tuple.Create<int, int>((int)Math.Round(width), (int)Math.Round(height))] = fi;
                _fontInfoBySizeDict[fi.FontSize] = fi;
            }

            int minWidthFloor    = (int)Math.Floor(minWidth);
            int maxWidthCeiling  = (int)Math.Ceiling(maxWidth);
            int minHeightFloor   = (int)Math.Floor(minHeight);
            int maxHeightCeiling = (int)Math.Ceiling(maxHeight);
            var reversedFontInfo = Enumerable.Range(0, _fontInfos.Length).Reverse<int>().ToArray<int>();
            

            for ( var w = minWidthFloor; w <= maxWidthCeiling; ++w )
            {                
                for ( var h = minHeightFloor; h <= maxHeightCeiling; ++h )
                {
                    var wh = Tuple.Create(w, h);

                    if ( !_fontInfosDict.ContainsKey(wh) )
                    {
                        _fontInfosDict[wh] = reversedFontInfo.Select(p => _fontInfos[p]).First(p => p.SymbolDimensions.Width <= w && p.SymbolDimensions.Height <= h);
                    }
                        
                }
            }
            _watch.Stop();

            SciChartDebugLogger.Instance.WriteLine("Initialized font calculator. Font={0}, MaxSize={1}, MaxWeight={2}, dict.Size={3}, initTime={4:F3}ms", fontFamily, minFontSize, fontWeight, _fontInfosDict.Count, _watch.Elapsed.TotalMilliseconds);
        }

        public string FontFamily => _fontFamily;

        public double SmallestFontSDHeight()
        {
            return _smallestFont.SymbolDimensions.Height;
        }

        public double SmallestFontSDWidth()
        {
            return _smallestFont.SymbolDimensions.Width;
        }

        /// <summary>
        /// GetFont will return a tuple of (fontSize, fontWeight, isBiggestFont) based on the drawable size, count of digits and prefer size.
        /// </summary>
        /// <param name="drawableSize"></param>
        /// <param name="count"></param>
        /// <param name="preferSize"></param>
        /// <returns>
        ///     float - best Font Size
        ///     FontWeight - best desired Font Weight
        ///     bool - has enough space for drawing
        /// </returns>
        public (float, FontWeight, bool) GetFont(Size drawableSize, int count, float preferSize )
        {
            count = Math.Max(count, 1);

            if ( (double)preferSize != 0.0 )
            {
                // Prefer size should not be less than 7f and not more than 32f
                preferSize = Math.Min(Math.Max(preferSize, 7f), 32f).Round(0.5f);
            }
                
            try
            {
                double digitAvgWidth = drawableSize.Width / (double)count;                
                                
                // available view port width is larger than the smallest font
                if ( digitAvgWidth >= _smallestFont.SymbolDimensions.Width )
                {
                    // available view port height is larger than the smallest font height
                    if ( drawableSize.Height >= _smallestFont.SymbolDimensions.Height )
                    {
                        // if available view port width is larger than the biggest font width
                        if ( digitAvgWidth >= _biggestFont.SymbolDimensions.Width )
                        {
                            // if available view port height is larger than the biggest font height
                            if ( drawableSize.Height >= _biggestFont.SymbolDimensions.Height )
                            {
                                // we will return the biggest font
                                return (_biggestFont.FontSize, _biggestFont.FontWeight, true);
                            }
                                
                        }
                                                                        
                        int digitMinWidth =  (int)Math.Floor(Math.Min(digitAvgWidth, _biggestFont.SymbolDimensions.Width));                                                
                        int digitMinHeight = (int)Math.Floor(Math.Min(drawableSize.Height, _biggestFont.SymbolDimensions.Height));

                        FontInfo fi = _fontInfosDict[Tuple.Create(digitMinWidth, digitMinHeight)];

                        if ( (double)preferSize == 0.0 || (double)fi.FontSize >= (double)preferSize )
                            return (fi.FontSize, fi.FontWeight, true);

                        if ( !_fontInfoBySizeDict.TryGetValue(preferSize, out fi) )
                            fi = _smallestFont;

                        return (fi.FontSize, fi.FontWeight, false);
                    }
                }
                if ( (double)preferSize == 0.0 )
                    return default;

                FontInfo fontInfo;
                if ( !_fontInfoBySizeDict.TryGetValue(preferSize, out fontInfo) )
                    fontInfo = _smallestFont;

                return (fontInfo.FontSize, fontInfo.FontWeight, false);
            }
            catch ( Exception ex )
            {                
                SciChartDebugLogger.Instance.WriteLine("GetFont({0}x{1}, {2}, {3:0.##}) error: {4}", drawableSize.Width, drawableSize.Height, count, preferSize, ex);
                
                FontInfo fi7f = _fontInfoBySizeDict[Math.Max(preferSize, 7f)];
                
                return (fi7f.FontSize, fi7f.FontWeight, false);
            }
        }

        public bool HasFont(Size drawableSize, int count)
        {
            (float, FontWeight, bool) tuple = GetFont(drawableSize, count, 0.0f);
            return (double)tuple.Item1 != 0.0 || tuple.Item2 != new FontWeight() || tuple.Item3;
        }

        private FontInfo GetFontInfo( int p)
        {
            return _fontInfos[p];
        }

                
        public sealed class FontInfo( float fontSize, FontWeight fontWeight, Size dimension)
        {
            private readonly float _fontSize        = fontSize.Round(0.5f);
            private readonly FontWeight _fontWeight = fontWeight;
            private readonly Size _symbolDimensions = dimension;

            public float FontSize => _fontSize;

            public FontWeight FontWeight => _fontWeight;

            public Size SymbolDimensions => _symbolDimensions;
        }
    }

    private static readonly Brush _defaultVerticalVolumeBrush                        = (Brush)new LinearGradientBrush(Color.FromRgb((byte)0, (byte)128 /*0x80*/, (byte)0), Color.FromRgb((byte)0, (byte)15, (byte)0), 90.0);

    private Dictionary<double, Tuple<double, CandlePriceLevel>> _candlePriceLevelMap = new Dictionary<double, Tuple<double, CandlePriceLevel>>();

    public static readonly DependencyProperty PriceStepProperty                      = DependencyProperty.Register(nameof(PriceStep), typeof(Decimal?), typeof(TimeframeSegmentRenderableSeries), new PropertyMetadata(0.000001M, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

    public static readonly DependencyProperty LocalHorizontalVolumesProperty         = DependencyProperty.Register(nameof(LocalHorizontalVolumes), typeof(bool), typeof(TimeframeSegmentRenderableSeries), new PropertyMetadata(false, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

    public static readonly DependencyProperty ShowHorizontalVolumesProperty          = DependencyProperty.Register(nameof(ShowHorizontalVolumes), typeof(bool), typeof(TimeframeSegmentRenderableSeries), new PropertyMetadata(true, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

    public static readonly DependencyProperty HorizontalVolumeWidthFractionProperty  = DependencyProperty.Register(nameof(HorizontalVolumeWidthFraction), typeof(double), typeof(TimeframeSegmentRenderableSeries), new PropertyMetadata(0.15, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface), new CoerceValueCallback(CoerceHorizontalVolumeWidthFraction)));

    public static readonly DependencyProperty VolumeBarsBrushProperty                = DependencyProperty.Register(nameof(VolumeBarsBrush), typeof(Brush), typeof(TimeframeSegmentRenderableSeries), new PropertyMetadata(_defaultVerticalVolumeBrush, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

    public static readonly DependencyProperty VolBarsFontColorProperty               = DependencyProperty.Register(nameof(VolBarsFontColor), typeof(Color), typeof(TimeframeSegmentRenderableSeries), new PropertyMetadata(Colors.White, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

    public static readonly DependencyProperty DrawSeparateVolumesProperty            = DependencyProperty.Register("DrawSeparateVolumes", typeof(bool), typeof(ClusterProfileRenderableSeries), new PropertyMetadata(true, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

    public static readonly DependencyProperty BuyColorProperty                       = DependencyProperty.Register("BuyColor", typeof(Color), typeof(ClusterProfileRenderableSeries), new PropertyMetadata(Colors.Green, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

    public static readonly DependencyProperty SellColorProperty                      = DependencyProperty.Register("SellColor", typeof(Color), typeof(ClusterProfileRenderableSeries), new PropertyMetadata(Colors.Red, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

    public static readonly DependencyProperty UpColorProperty                        = DependencyProperty.Register("UpColor", typeof(Color), typeof(ClusterProfileRenderableSeries), new PropertyMetadata(Colors.DarkGreen, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

    public static readonly DependencyProperty DownColorProperty                      = DependencyProperty.Register("DownColor", typeof(Color), typeof(ClusterProfileRenderableSeries), new PropertyMetadata(Colors.DarkGreen, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

    protected FontCalculator _fontCalculator;


    /// <summary>
    /// 
    /// </summary>
    public bool LocalHorizontalVolumes
    {
        get
        {
            return (bool)GetValue(LocalHorizontalVolumesProperty);
        }
        set
        {
            SetValue(LocalHorizontalVolumesProperty, (object)value);
        }
    }

    /// <summary>
    /// ShowHorizontalVolumes indicates whether to draw horizontal volumes or not.
    /// </summary>
    public bool ShowHorizontalVolumes
    {
        get
        {
            return (bool)GetValue(ShowHorizontalVolumesProperty);
        }
        set
        {
            SetValue(ShowHorizontalVolumesProperty, (object)value);
        }
    }

    public double HorizontalVolumeWidthFraction
    {
        get
        {
            return (double)GetValue(HorizontalVolumeWidthFractionProperty);
        }
        set
        {
            SetValue(HorizontalVolumeWidthFractionProperty, (object)value);
        }
    }

    public Brush VolumeBarsBrush
    {
        get
        {
            return (Brush)GetValue(VolumeBarsBrushProperty);
        }
        set
        {
            SetValue(VolumeBarsBrushProperty, (object)value);
        }
    }

    public Color VolBarsFontColor
    {
        get
        {
            return (Color)GetValue(VolBarsFontColorProperty);
        }
        set
        {
            SetValue(VolBarsFontColorProperty, (object)value);
        }
    }

    public bool DrawSeparateVolumes
    {
        get
        {
            return (bool)GetValue(DrawSeparateVolumesProperty);
        }

        set
        {
            SetValue(DrawSeparateVolumesProperty, value);
        }
    }


    public Color BuyColor
    {
        get
        {
            return (Color)GetValue(BuyColorProperty);
        }

        set
        {
            SetValue(BuyColorProperty, value);
        }

    }

    public Color SellColor
    {
        get
        {
            return (Color)GetValue(SellColorProperty);
        }

        set
        {
            SetValue(SellColorProperty, value);
        }
    }

    public Color UpColor
    {
        get
        {
            return (Color)GetValue(UpColorProperty);
        }

        set
        {
            SetValue(UpColorProperty, value);
        }

    }



    public Color DownColor
    {
        get
        {
            return (Color)GetValue(DownColorProperty);
        }

        set
        {
            SetValue(DownColorProperty, value);
        }

    }






    public Decimal? PriceStep
    {
        get
        {
            return (Decimal?)GetValue(PriceStepProperty);
        }
        set
        {
            SetValue(PriceStepProperty, (object)value);
        }
    }

    protected TimeSpan? Timeframe
    {
        get
        {
            return ( (ITimeframe)DataSeries )?.Timeframe;
        }
    }



    

    private static object CoerceHorizontalVolumeWidthFraction(DependencyObject dpo, object w)
    {
        double widthFraction = (double)w;
        if ( widthFraction < 0.0 )
            return (object)0.0;

        return widthFraction <= 1.0 ? w : (object)1.0;
    }

    protected virtual void OnDrawImpl(IRenderContext2D _param1, IRenderPassData _param2)
    {

    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if ( e.Property != Control.FontFamilyProperty && e.Property != Control.FontSizeProperty && e.Property != Control.FontWeightProperty )
            return;
        _fontCalculator = null;
        OnInvalidateParentSurface();
    }

    //
    // Summary:
    //     Draws the series using the SciChart.Drawing.Common.IRenderContext2D and the SciChart.Charting.Visuals.RenderableSeries.IRenderPassData
    //     passed in
    //
    //     From version 6 onward, this defers to the SciChart.Charting.Visuals.RenderableSeries.BaseRenderableSeries.DrawingProviders
    //     class. If you wish to have custom drawing, you must implement a drawing provider
    //
    //
    // Parameters:
    //   renderContext:
    //     The render context. This is a graphics object which has methods to draw lines,
    //     quads and polygons to the screen
    //
    //   renderPassData:
    //     The render pass data. Contains a resampled SciChart.Data.Model.IPointSeries,
    //     the SciChart.Data.Model.IndexRange of points on the screen and the current YAxis
    //     and XAxis SciChart.Charting.Numerics.CoordinateCalculators.ICoordinateCalculator`1
    //     to convert data-points to screen points
    protected override void Draw(IRenderContext2D renderContext, IRenderPassData renderPassData)
    {
        if ( renderPassData.IsVerticalChart )
            throw new NotSupportedException("vertical charts not supported");

        if ( CurrentRenderPassData.XCoordinateCalculator.HasFlippedCoordinates || CurrentRenderPassData.YCoordinateCalculator.HasFlippedCoordinates )
            throw new NotSupportedException("flipped axes not supported");

        if ( CurrentRenderPassData.PointSeries.Count < 1 || !( DataSeries is TimeframeSegmentDataSeries ) )
            return;

        if ( _fontCalculator == null )
        {
            string ff = FontFamily != null ? FontFamily.Source : "Tahoma";
            float fsRounded = ( (float)FontSize ).Round(0.5f);            
            _fontCalculator = new FontCalculator(renderContext, ff, fsRounded, FontWeight);
        }

        OnDrawImpl(renderContext, renderPassData);
        
        if ( ShowHorizontalVolumes )
            DrawHorizontalVolumes(renderContext, renderPassData);
        else
            _candlePriceLevelMap = new Dictionary<double, Tuple<double, CandlePriceLevel>>();
    }

    /// <summary>
    /// This function will draw a grid using two points that define the rectangle area, the number of horizontal and vertical lines, and the pens for the frame and grid lines.
    /// </summary>
    /// <param name="renderContext"></param>
    /// <param name="drawingHelper"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <param name="xCount"></param>
    /// <param name="yCount"></param>
    /// <param name="framePen"></param>
    /// <param name="gridPen"></param>
    /// <param name="fillBrush"></param>
    protected static void DrawGrid(IRenderContext2D renderContext, ISeriesDrawingHelperSS drawingHelper, Point begin, Point end, int xCount, int yCount, IPen2D framePen, IPen2D gridPen, IBrush2D fillBrush)
    {
        if ( begin.X >= end.X || begin.Y >= end.Y )
            return;

        double width = end.X - begin.X;
        double height = end.Y - begin.Y;
        
        double widthDPI = ( end.X - begin.X ) / (double)xCount;
        double heightDPI = ( end.Y - begin.Y ) / (double)yCount;

        if ( width > 1.0 && height > 1.0 )
        {
            if ( widthDPI > 2.0 && heightDPI > 2.0 )
            {
                for ( int index = 1; index < xCount; ++index )
                {
                    double verticalX = begin.X + (double)index * widthDPI;
                    drawingHelper.DrawLine(new Point(verticalX, begin.Y), new Point(verticalX, end.Y), gridPen);
                }

                for ( int index = 1; index < yCount; ++index )
                {
                    double horizontalY = begin.Y + (double)index * heightDPI;
                    drawingHelper.DrawLine(new Point(begin.X, horizontalY), new Point(end.X, horizontalY), gridPen);
                }
            }
            else
                renderContext.FillRectangle(fillBrush, begin, end, 0.0);
        }
        drawingHelper.DrawQuad(framePen, begin, end);
    }

    
    protected static void FillPeriodSegments(List<TimeframeIndexedSegment> wrapperList, TimeframeIndexedSegment[] wriapper, int index, TimeSpan timeFrame)
    {
        wrapperList.Clear();
        Tuple<DateTime, DateTime, long> timeframePeriod = TimeframeSegmentDataSeries.GetTimeframePeriod(wriapper[index].Segment.Time, timeFrame);
        do
        {
            wrapperList.Add(wriapper[index]);
        }
        while ( index < wriapper.Length - 1 && !( wriapper[++index].Segment.Time >= timeframePeriod.Item2 ) );
    }


    /// <summary>
    /// Draw horizontal volume bar for each price level in the given timeframe segment.
    /// </summary>
    /// <param name="renderContext"></param>
    /// <param name="renderPassData"></param>
    private void DrawHorizontalVolumes( IRenderContext2D renderContext, IRenderPassData renderPassData)
    {
        var series = DataSeries as TimeframeSegmentDataSeries;
        if ( series == null )
            return;

        var tfps                 = (TimeframeSegmentPointSeries) CurrentRenderPassData.PointSeries;
        
        var ps                   = tfps.PriceStep;

        var yCalc                = CurrentRenderPassData.YCoordinateCalculator;
        var width                = renderContext.ViewportSize.Width;
        var height               = renderContext.ViewportSize.Height;
        var psDPI                = Math.Abs(yCalc.GetCoordinate(tfps.Segments[0].Segment.LowPrice) - yCalc.GetCoordinate(tfps.Segments[0].Segment.LowPrice + tfps.PriceStep));
        var psDPIHalf            = psDPI / 2.0;

        var maxDrawPrice         = yCalc.GetDataValue(-psDPI);
        var minDrawPrice         = yCalc.GetDataValue(height + psDPI);

        var requiredPrices       = LocalHorizontalVolumes ? tfps.AllPrices.Where( p => p > minDrawPrice && p < maxDrawPrice) : (IEnumerable<double>)TimeframeSegmentDataSeries.GeneratePrices(minDrawPrice, maxDrawPrice, ps);

        var priceLevelWithVolume = ( LocalHorizontalVolumes ? requiredPrices.ToDictionary( p => p, tfps.GetCandlePriceLevelFromPrice ) : requiredPrices.ToDictionary( p => p, y => series.GetVolumeByPrice(y, ps) ) ).Where( p =>
        {
            if ( p.Key <= minDrawPrice || p.Key >= maxDrawPrice )
                return false;
            
            return p.Value.TotalVolume > 0M;
        }).ToArray();

        if ( !Enumerable.Any<KeyValuePair<double, CandlePriceLevel>>(priceLevelWithVolume) )
            return;

        var volumeBarsBrush     = VolumeBarsBrush;
        var volBarsFontColor    = VolBarsFontColor;
        var linearGradientBrush = volumeBarsBrush as LinearGradientBrush;
                
        using ( PenManager pm = new PenManager(renderContext, false, (float)StrokeThickness, Opacity) )
        {
            var newCandlePriceLevelMap = new Dictionary<double, Tuple<double, CandlePriceLevel>>();

            var drawColor              = pm.GetPen(linearGradientBrush != null ? linearGradientBrush.GradientStops[0].Color : volBarsFontColor);
            var volumeBrush            = pm.GetBrush(volumeBarsBrush);
            var buyBrush               = pm.GetBrush(BuyColor);
            var sellBrush              = pm.GetBrush(SellColor);

            double maxVolume           = (double) priceLevelWithVolume.Max( p =>  p.Value.TotalVolume );            
            double itemCount           = width * HorizontalVolumeWidthFraction;

            foreach ( (double price, CandlePriceLevel pl) in priceLevelWithVolume )
            {
                double yCoor_bottom      = yCalc.GetCoordinate(price) - psDPIHalf;
                double yCoor_top         = yCoor_bottom + psDPI;
                Decimal totalVolume      = ( (CandlePriceLevel)pl ).TotalVolume;
                double volumenPercentage = itemCount * (double)totalVolume / maxVolume;
                Point beginPt            = new Point(0.0, yCoor_bottom);
                Point endPt              = new Point(volumenPercentage, yCoor_top);
                double pi1_5             = 3.0 * Math.PI / 2.0;
                
                if ( psDPI > 1.5 )
                {
                    renderContext.DrawLine(drawColor, beginPt, new Point(volumenPercentage, yCoor_bottom));

                    if ( DrawSeparateVolumes && ( pl.BuyVolume > 0M || pl.SellVolume > 0M ) )
                    {
                        double buyVolPercentage = volumenPercentage * (double)( pl.BuyVolume / totalVolume );
                        if ( pl.BuyVolume > 0M )
                        {
                            renderContext.FillRectangle(buyBrush, new Point(0.0, yCoor_bottom + 1.0), pl.SellVolume > 0M ? new Point(buyVolPercentage, yCoor_top) : endPt, pi1_5);
                        }
                            
                        if ( pl.SellVolume > 0M )
                        {
                            renderContext.FillRectangle(sellBrush, new Point(buyVolPercentage, yCoor_bottom + 1.0), endPt, pi1_5);
                        }
                            
                    }
                    else
                    {
                        renderContext.FillRectangle(volumeBrush, new Point(0.0, yCoor_bottom + 1.0), endPt, pi1_5);
                    }
                        
                }
                else
                {
                    renderContext.FillRectangle(volumeBrush, beginPt, endPt, pi1_5);
                }
                    
                newCandlePriceLevelMap[price] = Tuple.Create<double, CandlePriceLevel>(volumenPercentage, pl);
                var volString                 = totalVolume.ToString();
                
                var labelRectangle            = new Rect(new Point(2, yCoor_bottom), new Point(itemCount + 2, yCoor_top));
                var labelSize                 = _fontCalculator.GetFont(labelRectangle.Size, totalVolume.IncreaseEffectiveScale(), 9f);                                
                
                if ( labelSize.Item3 )
                {
                    var myff = new System.Windows.Media.FontFamily(_fontCalculator.FontFamily);
                    renderContext.DrawText(labelRectangle, volBarsFontColor, labelSize.Item1, volString, myff, labelSize.Item2, FontStyles.Normal );
                }                    
            }
            _candlePriceLevelMap = newCandlePriceLevelMap;
        }
    }
}


//using SciChart.Charting.Visuals.RenderableSeries;
//using SciChart.Core.Utility;
//using SciChart.Drawing.Common;
//using StockSharp.Messages;
//using StockSharp.Xaml.Charting.ATony;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Media;
//using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;

//namespace StockSharp.Xaml.Charting;

//#nullable disable
//public abstract class TimeframeSegmentRenderableSeries : BaseRenderableSeries
//{
//    protected sealed class FontCalculator
//    {
//        private sealed class FontInfo(float size, FontWeight weight, Size dim)
//        {
//            private readonly float _fontSize = NumberUtil.Round(size, 0.5f);
//            private readonly FontWeight _fontWeight = weight;
//            private readonly Size _symbolDimensions = dim;

//            public float FontSize => _fontSize;

//            public FontWeight FontWeight => _fontWeight;

//            public Size SymbolDimensions => _symbolDimensions;
//        }

//        private readonly FontInfo[] _fontInfos;
//        private readonly FontInfo _biggestFont;
//        private readonly FontInfo _smallestFont;
//        private readonly string _fontFamily;
//        private readonly Dictionary<Tuple<int, int>, FontInfo> _fontInfosDict = new Dictionary<Tuple<int, int>, FontInfo>(
//            );
//        private readonly Dictionary<float, FontInfo> _fontInfoBySizeDict = new Dictionary<float, FontInfo>();
//        private readonly Stopwatch _watch = new Stopwatch();

//        public FontCalculator(IRenderContext2D renderContext, string fontFamily, float maxFontSize, FontWeight weightAtMaxSize)
//        {
//            _watch.Restart();
//            _fontFamily = fontFamily;
//            maxFontSize = Math.Min(32f, maxFontSize).Round(0.5f);
//            _fontInfos = new FontInfo[1 + (int)Math.Round(( (double)maxFontSize - 7.0 ) / 0.5)];

//            for ( float num = 7f; (double)num <= (double)maxFontSize; num += 0.5f )
//            {
//                FontWeight fontWeight = (double)num <= 8.5
//                    ? FontWeights.ExtraLight
//                    : ( (double)num <= 10.0 ? FontWeights.Light : weightAtMaxSize );
//                Size size = renderContext.DigitMaxSize(num, _fontFamily, fontWeight);
//                _fontInfos[(int)Math.Round(( (double)num - 7.0 ) / 0.5)] = new FontInfo(num, fontWeight, size);
//            }
//            _smallestFont = _fontInfos[0];
//            var zEdGaOlhSe7Xw = _fontInfos;
//            _biggestFont = _fontInfos[_fontInfos.Length - 1];
//            double min_height;
//            double min_width = min_height = double.MaxValue;
//            double max_height;
//            double max_width = max_height = double.MinValue;

//            foreach ( var fontInfo in _fontInfos )
//            {
//                Size fs = fontInfo.SymbolDimensions;
//                double width = fs.Width;
//                fs = fontInfo.SymbolDimensions;
//                double height = fs.Height;

//                if ( width < min_width )
//                    min_width = width;
//                if ( width > max_width )
//                    max_width = width;

//                if ( height < min_height )
//                    min_height = height;
//                if ( height > max_height )
//                    max_height = height;

//                _fontInfosDict[Tuple.Create<int, int>((int)Math.Round(width), (int)Math.Round(height))] = fontInfo;
//                _fontInfoBySizeDict[fontInfo.FontSize] = fontInfo;
//            }            

//            int[] reversedFontInfos = Enumerable.Range(0, _fontInfos.Length).Reverse<int>().ToArray<int>();            
            
//            for ( var i = (int)Math.Floor(min_width); i <= (int)Math.Ceiling(max_width); ++i )
//            {                
//                for ( var j = (int)Math.Floor(min_height); j <= (int)Math.Ceiling(max_height); ++j )
//                {
//                    var key = Tuple.Create<int, int>( i, j );

//                    if(!_fontInfosDict.ContainsKey(key))
//                    {
//                        _fontInfosDict[key] = reversedFontInfos.Select(p => _fontInfos[p]).First(s => s.SymbolDimensions.Width <= i && s.SymbolDimensions.Height <= j);
//                    }
                        
                    
//                }
//            }
//            _watch.Stop();
//            SciChartDebugLogger.Instance.WriteLine("Initialized font calculator. Font={0}, MaxSize={1}, MaxWeight={2}, dict.Size={3}, initTime={4:F3}ms", fontFamily, maxFontSize, weightAtMaxSize, _fontInfosDict.Count, _watch.Elapsed.TotalMilliseconds);
//        }

//        public string FontFamily => _fontFamily;

//        public double SFSymbolDimensionsHeight()
//        {
//            return _smallestFont.SymbolDimensions.Height;
//        }

//        public double SFSymbolDimensionsWidth()
//        {
//            return _smallestFont.SymbolDimensions.Width;
//        }

//        public (float, FontWeight, bool) GetFont(Size fontSize, int weight, float _param3)
//        {
//            weight = Math.Max(weight, 1);
//            if ( (double)_param3 != 0.0 )
//                _param3 = Math.Min(Math.Max(_param3, 7f), 32f).Round(0.5f);
//            try
//            {
//                double num1 = fontSize.Width / (double)weight;
//                double num2 = num1;
//                Size size = _smallestFont.SymbolDimensions;
//                double width1 = size.Width;
//                if ( num2 >= width1 )
//                {
//                    double height1 = fontSize.Height;
//                    size = _smallestFont.SymbolDimensions;
//                    double height2 = size.Height;
//                    if ( height1 >= height2 )
//                    {
//                        double num3 = num1;
//                        size = _biggestFont.SymbolDimensions;
//                        double width2 = size.Width;
//                        if ( num3 >= width2 )
//                        {
//                            double height3 = fontSize.Height;
//                            size = _biggestFont.SymbolDimensions;
//                            double height4 = size.Height;
//                            if ( height3 >= height4 )
//                                return (_biggestFont.FontSize, _biggestFont.FontWeight, true);
//                        }
//                        double val1 = num1;
//                        size = _biggestFont.SymbolDimensions;
//                        double width3 = size.Width;
//                        int num4 = (int)Math.Floor(Math.Min(val1, width3));
//                        double height5 = fontSize.Height;
//                        size = _biggestFont.SymbolDimensions;
//                        double height6 = size.Height;
//                        int num5 = (int)Math.Floor(Math.Min(height5, height6));
//                        FontInfo tmaYvnm94O29pOcja = _fontInfosDict[
//                            Tuple.Create<int, int>(num4, num5)];
//                        if ( (double)_param3 == 0.0 || (double)tmaYvnm94O29pOcja.FontSize >= (double)_param3 )
//                            return (tmaYvnm94O29pOcja.FontSize, tmaYvnm94O29pOcja.FontWeight, true);
//                        if ( !_fontInfoBySizeDict.TryGetValue(_param3, out tmaYvnm94O29pOcja) )
//                            tmaYvnm94O29pOcja = _smallestFont;
//                        return (tmaYvnm94O29pOcja.FontSize, tmaYvnm94O29pOcja.FontWeight, false);
//                    }
//                }
//                if ( (double)_param3 == 0.0 )
//                    return default;
                
//                if ( !_fontInfoBySizeDict.TryGetValue(_param3, out var fi) )
//                    fi = _smallestFont;
//                return (fi.FontSize, fi.FontWeight, false);
//            }
//            catch ( Exception ex )
//            {
//                SciChartDebugLogger.Instance .WriteLine( "GetFont({0}x{1}, {2}, {3:0.##}) error: {4}", fontSize.Width, fontSize.Height, weight, _param3, ex );
//                FontInfo standardFI = _fontInfoBySizeDict[ Math.Max(_param3, 7f)];

//                return (standardFI.FontSize, standardFI.FontWeight, false);
//            }
//        }

//        public bool HasFont(Size fontSize, int weight)
//        {
//            (float, FontWeight, bool) tuple = GetFont(fontSize, weight, 0.0f);
//            return (double)tuple.Item1 != 0.0 || tuple.Item2 != new FontWeight() || tuple.Item3;
//        }

//        private FontInfo OnLineOnePropertyChanged(int p)
//        {
//            return _fontInfos[p];
//        }

//        private sealed class SealClass0133
//        {
//            public int _someInteger1234;
//            public FontCalculator.SealClass0132 _SealClass0132Variable0033;

//            public bool OnChartAreaElementsRemovingAt(FontInfo p)
//            {
//                return p.SymbolDimensions.Width <= (double)_SealClass0132Variable0033._instanceVariable0032 && p.SymbolDimensions.Height <= (double)_someInteger1234;
//            }
//        }

//        private sealed class SealClass0132
//        {
//            public int _instanceVariable0032;
//        }
//    }


//    private static readonly Brush _defaultVerticalVolumeBrush = (Brush)new LinearGradientBrush(Color.FromRgb((byte)0, (byte)128 /*0x80*/, (byte)0), Color.FromRgb((byte)0, (byte)15, (byte)0), 90.0);

//    private Dictionary<double, Tuple<double, CandlePriceLevel>> _candlePriceLevelMap = new Dictionary<double, Tuple<double, CandlePriceLevel>>();

//    public static readonly DependencyProperty PriceStepProperty = DependencyProperty.Register(nameof(PriceStep), typeof(Decimal?), typeof(TimeframeSegmentRenderableSeries), new PropertyMetadata(0.000001M, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

//    public static readonly DependencyProperty LocalHorizontalVolumesProperty = DependencyProperty.Register(nameof(LocalHorizontalVolumes), typeof(bool), typeof(TimeframeSegmentRenderableSeries), new PropertyMetadata(false, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

//    public static readonly DependencyProperty ShowHorizontalVolumesProperty = DependencyProperty.Register(nameof(ShowHorizontalVolumes), typeof(bool), typeof(TimeframeSegmentRenderableSeries), new PropertyMetadata(true, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

//    public static readonly DependencyProperty HorizontalVolumeWidthFractionProperty = DependencyProperty.Register(nameof(HorizontalVolumeWidthFraction), typeof(double), typeof(TimeframeSegmentRenderableSeries), new PropertyMetadata(0.15, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface), new CoerceValueCallback(CoerceHorizontalVolumeWidthFraction)));

//    public static readonly DependencyProperty VolumeBarsBrushProperty = DependencyProperty.Register(nameof(VolumeBarsBrush), typeof(Brush), typeof(TimeframeSegmentRenderableSeries), new PropertyMetadata(_defaultVerticalVolumeBrush, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

//    public static readonly DependencyProperty VolBarsFontColorProperty = DependencyProperty.Register(nameof(VolBarsFontColor), typeof(Color), typeof(TimeframeSegmentRenderableSeries), new PropertyMetadata(Colors.White, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

//    public static readonly DependencyProperty DrawSeparateVolumesProperty = DependencyProperty.Register("DrawSeparateVolumes", typeof(bool), typeof(ClusterProfileRenderableSeries), new PropertyMetadata(true, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

//    public static readonly DependencyProperty BuyColorProperty = DependencyProperty.Register("BuyColor", typeof(Color), typeof(ClusterProfileRenderableSeries), new PropertyMetadata(Colors.Green, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

//    public static readonly DependencyProperty SellColorProperty = DependencyProperty.Register("SellColor", typeof(Color), typeof(ClusterProfileRenderableSeries), new PropertyMetadata(Colors.Red, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

//    public static readonly DependencyProperty UpColorProperty = DependencyProperty.Register("UpColor", typeof(Color), typeof(ClusterProfileRenderableSeries), new PropertyMetadata(Colors.DarkGreen, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

//    public static readonly DependencyProperty DownColorProperty = DependencyProperty.Register("DownColor", typeof(Color), typeof(ClusterProfileRenderableSeries), new PropertyMetadata(Colors.DarkGreen, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

//    protected FontCalculator _fontCalculator;

//    public bool LocalHorizontalVolumes
//    {
//        get
//        {
//            return (bool)GetValue(LocalHorizontalVolumesProperty);
//        }
//        set
//        {
//            SetValue(LocalHorizontalVolumesProperty, value);
//        }
//    }

//    public bool ShowHorizontalVolumes
//    {
//        get
//        {
//            return (bool)GetValue(ShowHorizontalVolumesProperty);
//        }
//        set
//        {
//            SetValue(ShowHorizontalVolumesProperty, value);
//        }
//    }

//    public double HorizontalVolumeWidthFraction
//    {
//        get
//        {
//            return (double)GetValue(HorizontalVolumeWidthFractionProperty);
//        }
//        set
//        {
//            SetValue(HorizontalVolumeWidthFractionProperty, value);
//        }
//    }

//    public Brush VolumeBarsBrush
//    {
//        get
//        {
//            return (Brush)GetValue(VolumeBarsBrushProperty);
//        }
//        set
//        {
//            SetValue(VolumeBarsBrushProperty, value);
//        }
//    }

//    public Color VolBarsFontColor
//    {
//        get
//        {
//            return (Color)GetValue(VolBarsFontColorProperty);
//        }
//        set
//        {
//            SetValue(VolBarsFontColorProperty, value);
//        }
//    }

//    public bool DrawSeparateVolumes
//    {
//        get
//        {
//            return (bool)GetValue(DrawSeparateVolumesProperty);
//        }

//        set
//        {
//            SetValue(DrawSeparateVolumesProperty, value);
//        }
//    }


//    public Color BuyColor
//    {
//        get
//        {
//            return (Color)GetValue(BuyColorProperty);
//        }

//        set
//        {
//            SetValue(BuyColorProperty, value);
//        }

//    }

//    public Color SellColor
//    {
//        get
//        {
//            return (Color)GetValue(SellColorProperty);
//        }

//        set
//        {
//            SetValue(SellColorProperty, value);
//        }
//    }

//    public Color UpColor
//    {
//        get
//        {
//            return (Color)GetValue(UpColorProperty);
//        }

//        set
//        {
//            SetValue(UpColorProperty, value);
//        }

//    }



//    public Color DownColor
//    {
//        get
//        {
//            return (Color)GetValue(DownColorProperty);
//        }

//        set
//        {
//            SetValue(DownColorProperty, value);
//        }

//    }

//    public Decimal? PriceStep
//    {
//        get
//        {
//            return (Decimal?)GetValue(PriceStepProperty);
//        }
//        set
//        {
//            SetValue(PriceStepProperty, value);
//        }
//    }

//    protected TimeSpan? Timeframe
//    {
//        get
//        {
//            return ( (ITimeframe)DataSeries )?.Timeframe;
//        }
//    }

//    //protected override HitTestInfo HitTestInternal(
//    //    Point _param1,
//    //    double _param2,
//    //    bool _param3)
//    //{
//    //    return base.HitTestInternal(_param1, _param2, false);
//    //}

//    private static object CoerceHorizontalVolumeWidthFraction(DependencyObject d, object changedObj)
//    {
//        double num = (double)changedObj;
//        if ( num< 0.0 )
//            return 0.0;
//        return num <= 1.0 ? changedObj : 1.0;
//    }

//    protected abstract void InternalDrawingRoutine(IRenderContext2D _param1, IRenderPassData _param2);

//    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs _param1)
//    {
//        base.OnPropertyChanged(_param1);
//        if ( _param1.Property != Control.FontFamilyProperty && _param1.Property != Control.FontSizeProperty && _param1.Property != Control.FontWeightProperty )
//            return;
//        _fontCalculator = (FontCalculator)null;
//        OnInvalidateParentSurface();
//    }

    

//    protected override void InternalDraw(IRenderContext2D renderContext, IRenderPassData renderPassData)
//    {
//        if ( renderPassData.IsVerticalChart )
//        {
//            throw new NotSupportedException("vertical charts not supported");
//        }
              
//        if ( CurrentRenderPassData.XCoordinateCalculator.HasFlippedCoordinates || CurrentRenderPassData.YCoordinateCalculator.HasFlippedCoordinates)
//      throw new NotSupportedException("flipped axes not supported");
//        if ( CurrentRenderPassData.PointSeries.Count < 1 || !( DataSeries is TimeframeSegmentDataSeries ))
//      return;
//        if ( _fontCalculator == null )
//        {
//            string str = FontFamily != null ? FontFamily.Source : "Tahoma";
//            float num = ( (float)FontSize ).Round(0.5f);
//            FontWeight fontWeight = FontWeight;
//            _fontCalculator = new FontCalculator(renderContext, str, num, fontWeight);
//        }
//        InternalDrawingRoutine(renderContext, renderPassData);
//        if ( ShowHorizontalVolumes )
//            DrawHorizontalVolumes(renderContext, renderPassData);
//    else
//            _candlePriceLevelMap = new Dictionary<double, Tuple<double, CandlePriceLevel>>();
//    }

//    protected static void DrawGrid(
//      IRenderContext2D _param0,
//                ISeriesDrawingHelper _param1,
//      Point _param2,
//      Point _param3,
//      int _param4,
//      int _param5,
//                IPen2D _param6,
//                IPen2D _param7,
//      IBrush2D _param8)
//    {
//        if ( _param2.X >= _param3.X || _param2.Y >= _param3.Y )
//            return;
//        double num1 = _param3.X - _param2.X;
//        double num2 = _param3.Y - _param2.Y;
//        double num3 = ( _param3.X - _param2.X ) / (double)_param4;
//        double num4 = ( _param3.Y - _param2.Y ) / (double)_param5;
//        if ( num1 > 1.0 && num2 > 1.0 )
//        {
//            if ( num3 > 2.0 && num4 > 2.0 )
//            {
//                for ( int index = 1; index < _param4; ++index )
//                {
//                    double num5 = _param2.X + (double)index * num3;
//                    _param1.DrawLine(new Point(num5, _param2.Y), new Point(num5, _param3.Y), _param7);
//                }
//                for ( int index = 1; index < _param5; ++index )
//                {
//                    double num6 = _param2.Y + (double)index * num4;
//                    _param1.DrawLine(new Point(_param2.X, num6), new Point(_param3.X, num6), _param7);
//                }
//            }
//            else
//                _param0.FillRectangle(_param8, _param2, _param3, 0.0);
//        }
//        _param1.DrawQuad(_param6, _param2, _param3);
//    }

//    protected static void FillPeriodSegments(
//      List<TimeframeSegmentWrapper> _param0,
//                TimeframeSegmentWrapper[] _param1,
//      int _param2,
//      TimeSpan _param3)
//    {
//        _param0.Clear();
//        Tuple<DateTime, DateTime, long> timeframePeriod = TimeframeSegmentDataSeries.GetTimeframePeriod(_param1[_param2].Segment.Time, _param3);
//        do
//        {
//            _param0.Add(_param1[_param2]);
//        }
//        while ( _param2 < _param1.Length - 1 && !( _param1[++_param2].Segment.Time >= timeframePeriod.Item2));
//    }

//    private void DrawHorizontalVolumes(
//            IRenderContext2D _param1,
//      IRenderPassData _param2)
//  {
//    SomeInternalSealedClassTwzt9h _localInstance123 = new SomeInternalSealedClassTwzt9h();
//IDataSeries dataSeries = DataSeries;
//    _localInstance123._tfds01 = dataSeries as TimeframeSegmentDataSeries;
//if (_localInstance123._tfds01 == null)
//      return;
//int num1 = LocalHorizontalVolumes ? 1 : 0;
//    TimeframeSegmentPointSeries pointSeries = (TimeframeSegmentPointSeries) CurrentRenderPassData.PointSeries;
//    _localInstance123._doubleInstance009 = pointSeries.PriceStep;
//    TimeframeSegmentWrapper[] j9sJkRf4wMmhD3hBArray = pointSeries.Segments;
//    ICoordinateCalculator<<double> ycoordinateCalculator = CurrentRenderPassData.YCoordinateCalculator;
//    double width = _param1.ViewportSize.Width;
//    double height = _param1.ViewportSize.Height;
//    double num2 = Math.Abs(ycoordinateCalculator.GetCoordinate( j9sJkRf4wMmhD3hBArray[0].Segment.MinPrice) - ycoordinateCalculator.GetCoordinate(j9sJkRf4wMmhD3hBArray[0].Segment.MinPrice + _localInstance123._doubleInstance009));
//    double num3 = num2 / 2.0;
//        _localInstance123._double02 = ycoordinateCalculator.GetDataValue(-num2);
//    _localInstance123._double01 = ycoordinateCalculator.GetDataValue(height + num2);
//    IEnumerable<double> source = num1 != 0 ? pointSeries.AllPrices.Where<double>(new Func<double, bool>(_localInstance123.Method01)) : (IEnumerable<double>) TimeframeSegmentDataSeries.GeneratePrices(_localInstance123._double01, _localInstance123._double02, _localInstance123._doubleInstance009);
//    KeyValuePair<double, CandlePriceLevel>[] array = (num1 != 0 ? (IEnumerable<KeyValuePair<double, CandlePriceLevel>>) source.ToDictionary<double, double, CandlePriceLevel>(SomeClass34343383._instance02 ?? (SomeClass34343383._instance02 = new Func<double, double>(SomeClass34343383.SomeMethond0343.Method04)), new Func<double, CandlePriceLevel>(pointSeries.GetCandlePriceLevelFromPrice)) : (IEnumerable<KeyValuePair<double, CandlePriceLevel>>) source.ToDictionary<double, double, CandlePriceLevel>(SomeClass34343383._instance03 ?? (SomeClass34343383._instance03 = new Func<double, double>(SomeClass34343383.SomeMethond0343.Method05)), new Func<double, CandlePriceLevel>(_localInstance123.Method02))).Where<KeyValuePair<double, CandlePriceLevel>>(new Func<KeyValuePair<double, CandlePriceLevel>, bool>(_localInstance123.Method03)).ToArray<KeyValuePair<double, CandlePriceLevel>>();
//    Brush volumeBarsBrush = VolumeBarsBrush;
//    Color volBarsFontColor = VolBarsFontColor;
//    LinearGradientBrush linearGradientBrush = volumeBarsBrush as LinearGradientBrush;
//    Dictionary<double, Tuple<double, CandlePriceLevel>> dictionary = new Dictionary<double, Tuple<double, CandlePriceLevel>>();
//    if (!TemplateTypeHelper.IsNotEmpty<KeyValuePair<double, CandlePriceLevel>>(array))
//      return;
//    using (PenManager penManager = new PenManager(_param1, false, (float) StrokeThickness, Opacity))
//    {
//      IPen2D rhwYsZxA33iRu6Id7J = penManager.GetPen(linearGradientBrush != null ? linearGradientBrush.GradientStops[0].Color : volBarsFontColor);
//      IBrush2D xrgcdFbSdWgN9GcT8_1 = penManager.GetBrush(volumeBarsBrush);
//      IBrush2D xrgcdFbSdWgN9GcT8_2 = penManager.GetBrush(BuyColor);
//      IBrush2D xrgcdFbSdWgN9GcT8_3 = penManager.GetBrush(SellColor);
//      double num4 = (double) ((IEnumerable<KeyValuePair<double, CandlePriceLevel>>) array).Max<KeyValuePair<double, CandlePriceLevel>>(SomeClass34343383._instance04 ?? (SomeClass34343383._instance04 = new Func<KeyValuePair<double, CandlePriceLevel>, Decimal>(SomeClass34343383.SomeMethond0343.Method06)));
//      double volumeWidthFraction = HorizontalVolumeWidthFraction;
//      double num5 = width * volumeWidthFraction;
//      foreach ((double key, CandlePriceLevel candlePriceLevel) in array)
//      {
//        double num6 = ycoordinateCalculator.GetCoordinate(key) - num3;
//        double num7 = num6 + num2;
//        Decimal totalVolume = ((CandlePriceLevel) ref candlePriceLevel).TotalVolume;
//        double num8 = num5 * (double) totalVolume / num4;
//        Point point1 = new Point(0.0, num6);
//        Point point2 = new Point(num8, num7);
//        double num9 = 3.0 * Math.PI / 2.0;
//        if (num2 > 1.5)
//        {
//          _param1.DrawLine(rhwYsZxA33iRu6Id7J, point1, new Point(num8, num6));
//          if (DrawSeparateVolumes && (((CandlePriceLevel) ref candlePriceLevel).BuyVolume > 0M || ((CandlePriceLevel) ref candlePriceLevel).SellVolume > 0M))
//          {
//            double num10 = num8 * (double) (((CandlePriceLevel) ref candlePriceLevel).BuyVolume / totalVolume);
//            if (((CandlePriceLevel) ref candlePriceLevel).BuyVolume > 0M)
//              _param1.FillRectangle(xrgcdFbSdWgN9GcT8_2, new Point(0.0, num6 + 1.0), ((CandlePriceLevel) ref candlePriceLevel).SellVolume > 0M ? new Point(num10, num7) : point2, num9);
//            if (((CandlePriceLevel) ref candlePriceLevel).SellVolume > 0M)
//              _param1.FillRectangle(xrgcdFbSdWgN9GcT8_3, new Point(num10, num6 + 1.0), point2, num9);
//          }
//          else
//            _param1.FillRectangle(xrgcdFbSdWgN9GcT8_1, new Point(0.0, num6 + 1.0), point2, num9);
//        }
//        else
//          _param1.FillRectangle(xrgcdFbSdWgN9GcT8_1, point1, point2, num9);
//        dictionary[key] = Tuple.Create<double, CandlePriceLevel>(num8, candlePriceLevel);
//        string str = totalVolume.ToString();
//        int num11 = 2;
//        Rect rect = new Rect(new Point((double) num11, num6), new Point(num5 + (double) num11, num7));
//        (float, FontWeight, bool) tuple = _fontCalculator.GetFont(rect.Size, totalVolume.GetDecimalLength(), 9f);
//        float num12 = tuple.Item1;
//        FontWeight fontWeight = tuple.Item2;
//        if (tuple.Item3)
//          _param1.DrawText(str, rect, AlignmentX.Left, AlignmentY.Center, volBarsFontColor, num12, _fontCalculator.FontFamily, fontWeight);
//      }
//      _candlePriceLevelMap = dictionary;
//    }
//  }

//  protected override HitTestInfo NearestHitResult(
//    Point _param1,
//    double _param2,
//    SearchMode _param3,
//    bool _param4)
//  {
//    if (!(DataSeries is TimeframeSegmentDataSeries dataSeries) || dataSeries.Count < 1)
//      return HitTestInfo.Empty;
//    double num1 = (double) (PriceStep ?? 0.000001M);
//    ICoordinateCalculator<<double> xkzemsMs5tGkouk5w1 = CurrentRenderPassData.XCoordinateCalculator;
//    ICoordinateCalculator<<double> xkzemsMs5tGkouk5w2 = CurrentRenderPassData.YCoordinateCalculator;
//    int index = (int) xkzemsMs5tGkouk5w1.GetDataValue(_param1.X);
//    double num2 = xkzemsMs5tGkouk5w2.GetDataValue(_param1.Y);
//    double key = num2.NormalizePrice(num1);
//    double num3 = Math.Abs(xkzemsMs5tGkouk5w1.GetCoordinate(1.0) - xkzemsMs5tGkouk5w1.GetCoordinate(0.0));
//    double num4 = Math.Abs(xkzemsMs5tGkouk5w2.GetCoordinate(key) - xkzemsMs5tGkouk5w2.GetCoordinate(key + num1));
//    if (num3 < 1.0 || num4 < 1.0)
//      return HitTestInfo.Empty;
//    Tuple<double, CandlePriceLevel> tuple;
//    if (ShowHorizontalVolumes && _candlePriceLevelMap.TryGetValue(key, out tuple) && _param1.X <= tuple.Item1)
//    {
//      HitTestInfo zldchDrVsrVyHh6WyiGy = new HitTestInfo();
//      zldchDrVsrVyHh6WyiGy.DataSeriesName(dataSeries.SeriesName);
//      zldchDrVsrVyHh6WyiGy.DataSeriesType(dataSeries.DataSeriesType);
//      zldchDrVsrVyHh6WyiGy.YValue((IComparable) key);
//      zldchDrVsrVyHh6WyiGy.Volume(tuple.Item2);
//      zldchDrVsrVyHh6WyiGy.IsHit(true);
//      zldchDrVsrVyHh6WyiGy.HitTestPoint(new Point(tuple.Item1, xkzemsMs5tGkouk5w2.GetCoordinate(key)));
//      return zldchDrVsrVyHh6WyiGy;
//    }
//    if (index < 0 || index >= dataSeries.Count)
//      return HitTestInfo.Empty;
//    TimeframeDataSegment segment = dataSeries.Segments[index];
//    CandlePriceLevel candlePriceLevel = segment.GetCandlePriceLevel(num2, num1);
//    if (((CandlePriceLevel) ref candlePriceLevel).TotalVolume == 0M)
//      return HitTestInfo.Empty;
//    HitTestInfo zldchDrVsrVyHh6WyiGy1 = new HitTestInfo();
//    zldchDrVsrVyHh6WyiGy1.DataSeriesName(dataSeries.SeriesName);
//    zldchDrVsrVyHh6WyiGy1.DataSeriesType(dataSeries.DataSeriesType);
//    zldchDrVsrVyHh6WyiGy1.XValue((IComparable) segment.Time);
//    zldchDrVsrVyHh6WyiGy1.YValue((IComparable) key);
//    zldchDrVsrVyHh6WyiGy1.DataSeriesIndex(index);
//    zldchDrVsrVyHh6WyiGy1.Volume(candlePriceLevel);
//    zldchDrVsrVyHh6WyiGy1.IsHit(true);
//    zldchDrVsrVyHh6WyiGy1.HitTestPoint(new Point(xkzemsMs5tGkouk5w1.GetCoordinate((double) index), xkzemsMs5tGkouk5w2.GetCoordinate(key)));
//    zldchDrVsrVyHh6WyiGy1.OpenValue((IComparable) segment.FirstPrice);
//    zldchDrVsrVyHh6WyiGy1.HighValue((IComparable) segment.MaxPrice);
//    zldchDrVsrVyHh6WyiGy1.LowValue((IComparable) segment.MinPrice);
//    zldchDrVsrVyHh6WyiGy1.CloseValue((IComparable) segment.LastPrice);
//    HitTestInfo zldchDrVsrVyHh6WyiGy2 = zldchDrVsrVyHh6WyiGy1;
//    return HitTestSeriesWithBody(_param1, zldchDrVsrVyHh6WyiGy2, _param2);
//  }

//  protected override HitTestInfo HitTestSeriesWithBody(
//    Point _param1,
//    HitTestInfo _param2,
//    double _param3)
//  {
//    return _param2;
//  }

  

//  [Serializable]
//  private new sealed class SomeClass34343383
//  {
//    public static readonly SomeClass34343383 SomeMethond0343 = new SomeClass34343383();
//    public static Func<double, double> _instance02;
//    public static Func<double, double> _instance03;
//    public static Func<KeyValuePair<double, CandlePriceLevel>, Decimal> _instance04;

//    public double Method04(double _param1)
//    {
//      return _param1;
//    }

//    public double Method05(double _param1)
//    {
//      return _param1;
//    }

//    public Decimal Method06(
//      KeyValuePair<double, CandlePriceLevel> _param1)
//    {
//      CandlePriceLevel candlePriceLevel = _param1.Value;
//      return ((CandlePriceLevel) ref candlePriceLevel).TotalVolume;
//    }
//  }

//  private sealed class SomeInternalSealedClassTwzt9h
//  {
//    public double _double01;
//    public double _double02;
//    public TimeframeSegmentDataSeries _tfds01;
//    public double _doubleInstance009;

//    public bool Method01(double _param1)
//    {
//      return _param1 > _double01 && _param1 < _double02;
//    }

//    public CandlePriceLevel Method02(double _param1)
//    {
//      return _tfds01.GetVolumeByPrice(_param1, _doubleInstance009);
//    }

//    public bool Method03(
//      KeyValuePair<double, CandlePriceLevel> _param1)
//    {
//      if (_param1.Key <= _double01 || _param1.Key >= _double02)
//        return false;
//      CandlePriceLevel candlePriceLevel = _param1.Value;
//      return ((CandlePriceLevel) ref candlePriceLevel).TotalVolume > 0M;
//    }
//  }
//}



////using SciChart.Charting.Visuals.RenderableSeries;
////using StockSharp.Messages;
////using System;
////using System.Collections.Generic;
////using System.Diagnostics;
////using System.Linq;
////using System.Windows;
////using System.Windows.Controls;
////using System.Windows.Media;
////using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;

////namespace StockSharp.Xaml.Charting;
////#nullable disable
////public abstract class TimeframeSegmentRenderableSeries : BaseRenderableSeries
////{

////    private static readonly Brush _defaultVerticalVolumeBrush = (Brush)new LinearGradientBrush(Color.FromRgb((byte)0, (byte)128 /*0x80*/, (byte)0), Color.FromRgb((byte)0, (byte)15, (byte)0), 90.0);

////    private Dictionary<double, Tuple<double, CandlePriceLevel>> _candlePriceLevelMap = new Dictionary<double, Tuple<double, CandlePriceLevel>>();

////    public static readonly DependencyProperty PriceStepProperty = DependencyProperty.Register(nameof(PriceStep), typeof(Decimal?), typeof(TimeframeSegmentRenderableSeries), new PropertyMetadata((object)0.000001M, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

////    public static readonly DependencyProperty LocalHorizontalVolumesProperty = DependencyProperty.Register(nameof(LocalHorizontalVolumes), typeof(bool), typeof(TimeframeSegmentRenderableSeries), new PropertyMetadata((object)false, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

////    public static readonly DependencyProperty ShowHorizontalVolumesProperty = DependencyProperty.Register(nameof(ShowHorizontalVolumes), typeof(bool), typeof(TimeframeSegmentRenderableSeries), new PropertyMetadata((object)true, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

////    public static readonly DependencyProperty HorizontalVolumeWidthFractionProperty = DependencyProperty.Register(nameof(HorizontalVolumeWidthFraction), typeof(double), typeof(TimeframeSegmentRenderableSeries), new PropertyMetadata((object)0.15, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface), new CoerceValueCallback(CoerceHorizontalVolumeWidthFraction)));

////    public static readonly DependencyProperty VolumeBarsBrushProperty = DependencyProperty.Register(nameof(VolumeBarsBrush), typeof(Brush), typeof(TimeframeSegmentRenderableSeries), new PropertyMetadata((object)_defaultVerticalVolumeBrush, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

////    public static readonly DependencyProperty VolBarsFontColorProperty = DependencyProperty.Register(nameof(VolBarsFontColor), typeof(Color), typeof(TimeframeSegmentRenderableSeries), new PropertyMetadata((object)Colors.White, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

////    public static readonly DependencyProperty DrawSeparateVolumesProperty = DependencyProperty.Register("DrawSeparateVolumes", typeof(bool), typeof(ClusterProfileRenderableSeries), new PropertyMetadata((object)true, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

////    public static readonly DependencyProperty BuyColorProperty = DependencyProperty.Register("BuyColor", typeof(Color), typeof(ClusterProfileRenderableSeries), new PropertyMetadata((object)Colors.Green, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

////    public static readonly DependencyProperty SellColorProperty = DependencyProperty.Register("SellColor", typeof(Color), typeof(ClusterProfileRenderableSeries), new PropertyMetadata((object)Colors.Red, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

////    public static readonly DependencyProperty UpColorProperty = DependencyProperty.Register("UpColor", typeof(Color), typeof(ClusterProfileRenderableSeries), new PropertyMetadata((object)Colors.DarkGreen, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

////    public static readonly DependencyProperty DownColorProperty = DependencyProperty.Register("DownColor", typeof(Color), typeof(ClusterProfileRenderableSeries), new PropertyMetadata((object)Colors.DarkGreen, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

////    protected FontCalculator _fontCalculator;

////    public bool LocalHorizontalVolumes
////    {
////        get
////        {
////            return (bool)GetValue(LocalHorizontalVolumesProperty);
////        }
////        set
////        {
////            SetValue(LocalHorizontalVolumesProperty, (object)value);
////        }
////    }

////    public bool ShowHorizontalVolumes
////    {
////        get
////        {
////            return (bool)GetValue(ShowHorizontalVolumesProperty);
////        }
////        set
////        {
////            SetValue(ShowHorizontalVolumesProperty, (object)value);
////        }
////    }

////    public double HorizontalVolumeWidthFraction
////    {
////        get
////        {
////            return (double)GetValue(HorizontalVolumeWidthFractionProperty);
////        }
////        set
////        {
////            SetValue(HorizontalVolumeWidthFractionProperty, (object)value);
////        }
////    }

////    public Brush VolumeBarsBrush
////    {
////        get
////        {
////            return (Brush)GetValue(VolumeBarsBrushProperty);
////        }
////        set
////        {
////            SetValue(VolumeBarsBrushProperty, (object)value);
////        }
////    }

////    public Color VolBarsFontColor
////    {
////        get
////        {
////            return (Color)GetValue(VolBarsFontColorProperty);
////        }
////        set
////        {
////            SetValue(VolBarsFontColorProperty, (object)value);
////        }
////    }

////    public bool DrawSeparateVolumes
////    {
////        get
////        {
////            return (bool)GetValue(DrawSeparateVolumesProperty);
////        }
////        set
////        {
////            SetValue(DrawSeparateVolumesProperty, (object)value);
////        }
////    }

////    public Color BuyColor
////    {
////        get
////        {
////            return (Color)GetValue(BuyColorProperty);
////        }
////        set
////        {
////            SetValue(BuyColorProperty, (object)value);
////        }
////    }





////    public Color SellColor
////    {
////        get
////        {
////            return (Color)GetValue(SellColorProperty);
////        }

////        set
////        {
////            SetValue(SellColorProperty, (object)value);
////        }
////    }



////    public Color UpColor
////    {
////        get
////        {
////            return (Color)GetValue(UpColorProperty);
////        }

////        set
////        {
////            SetValue(UpColorProperty, (object)value);
////        }
////    }



////    public Color DownColor
////    {
////        get
////        {
////            return (Color)GetValue(DownColorProperty);
////        }
////        set
////        {
////            SetValue(DownColorProperty, (object)value);
////        }
////    }

////    public Decimal? PriceStep
////    {
////        get
////        {
////            return (Decimal?)GetValue(PriceStepProperty);
////        }
////        set
////        {
////            SetValue(PriceStepProperty, (object)value);
////        }
////    }

////    protected TimeSpan? Timeframe
////    {
////        get
////        {
////            return ( (ITimeframe)DataSeries )?.Timeframe;
////        }
////    }

////    protected override HitTestInfo HitTestInternal(
////      Point _param1,
////      double _param2,
////      bool _param3)
////    {
////        return base.HitTestInternal(_param1, _param2, false);
////    }

////    private static object CoerceHorizontalVolumeWidthFraction(
////      DependencyObject _param0,
////      object _param1)
////  {
////    double num = (double)_param1;
////    if (num< 0.0)
////      return 0.0;
////    return num <= 1.0 ? _param1 : 1.0;
////  }

////protected abstract void InternalDrawingRoutine(
////  IRenderContext2D _param1,
////  IRenderPassData _param2);

////protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs _param1)
////{
////    base.OnPropertyChanged(_param1);
////    if ( _param1.Property != Control.FontFamilyProperty && _param1.Property != Control.FontSizeProperty && _param1.Property != Control.FontWeightProperty )
////        return;
////    _fontCalculator = (FontCalculator)null;
////    OnInvalidateParentSurface();
////}

////protected override void InternalDraw(
////  IRenderContext2D _param1,
////  IRenderPassData _param2)
////{
////    if ( _param2.IsVerticalChart)
////      throw new NotSupportedException("vertical charts not supported");
////    if ( CurrentRenderPassData.XCoordinateCalculator.HasFlippedCoordinates || CurrentRenderPassData.YCoordinateCalculator.HasFlippedCoordinates)
////      throw new NotSupportedException("flipped axes not supported");
////    if ( CurrentRenderPassData.PointSeries.Count < 1 || !( DataSeries is TimeframeSegmentDataSeries ))
////      return;
////    if ( _fontCalculator == null )
////    {
////        string str = FontFamily != null ? FontFamily.Source : "Tahoma";
////        float num = ( (float)FontSize ).Round(0.5f);
////        FontWeight fontWeight = FontWeight;
////        _fontCalculator = new FontCalculator(_param1, str, num, fontWeight);
////    }
////    InternalDrawingRoutine(_param1, _param2);
////    if ( ShowHorizontalVolumes )
////        DrawHorizontalVolumes(_param1, _param2);
////    else
////        _candlePriceLevelMap = new Dictionary<double, Tuple<double, CandlePriceLevel>>();
////}

////protected static void DrawGrid(
////  IRenderContext2D _param0,
////    ISeriesDrawingHelper _param1,
////  Point _param2,
////  Point _param3,
////  int _param4,
////  int _param5,
////    IPen2D _param6,
////    IPen2D _param7,
////  IBrush2D _param8)
////{
////    if ( _param2.X >= _param3.X || _param2.Y >= _param3.Y )
////        return;
////    double num1 = _param3.X - _param2.X;
////    double num2 = _param3.Y - _param2.Y;
////    double num3 = ( _param3.X - _param2.X ) / (double)_param4;
////    double num4 = ( _param3.Y - _param2.Y ) / (double)_param5;
////    if ( num1 > 1.0 && num2 > 1.0 )
////    {
////        if ( num3 > 2.0 && num4 > 2.0 )
////        {
////            for ( int index = 1; index < _param4; ++index )
////            {
////                double num5 = _param2.X + (double)index * num3;
////                _param1.DrawLine(new Point(num5, _param2.Y), new Point(num5, _param3.Y), _param7);
////            }
////            for ( int index = 1; index < _param5; ++index )
////            {
////                double num6 = _param2.Y + (double)index * num4;
////                _param1.DrawLine(new Point(_param2.X, num6), new Point(_param3.X, num6), _param7);
////            }
////        }
////        else
////            _param0.FillRectangle(_param8, _param2, _param3, 0.0);
////    }
////    _param1.DrawQuad(_param6, _param2, _param3);
////}

////protected static void FillPeriodSegments(
////  List<TimeframeSegmentWrapper> _param0,
////    TimeframeSegmentWrapper[] _param1,
////  int _param2,
////  TimeSpan _param3)
////{
////    _param0.Clear();
////    Tuple<DateTime, DateTime, long> timeframePeriod = TimeframeSegmentDataSeries.GetTimeframePeriod(_param1[_param2].Segment.Time, _param3);
////    do
////    {
////        _param0.Add(_param1[_param2]);
////    }
////    while ( _param2 < _param1.Length - 1 && !( _param1[++_param2].Segment.Time >= timeframePeriod.Item2));
////}

////private void DrawHorizontalVolumes(
////    IRenderContext2D _param1,
////  IRenderPassData _param2)
////  {
////    SomeInternalSealedClassTwzt9h _localInstance123 = new SomeInternalSealedClassTwzt9h();
////IDataSeries dataSeries = DataSeries;
////_localInstance123._tfds01 = dataSeries as TimeframeSegmentDataSeries;
////if (_localInstance123._tfds01 == null)
////      return;
////int num1 = LocalHorizontalVolumes ? 1 : 0;
////    TimeframeSegmentPointSeries pointSeries = (TimeframeSegmentPointSeries) CurrentRenderPassData.PointSeries;
////    _localInstance123._doubleInstance009 = pointSeries.PriceStep;
////    TimeframeSegmentWrapper[] j9sJkRf4wMmhD3hBArray = pointSeries.Segments;
////    ICoordinateCalculator<<double> ycoordinateCalculator = CurrentRenderPassData.YCoordinateCalculator;
////    double width = _param1.ViewportSize.Width;
////    double height = _param1.ViewportSize.Height;
////    double num2 = Math.Abs(ycoordinateCalculator.GetCoordinate(j9sJkRf4wMmhD3hBArray[0].Segment.MinPrice) - ycoordinateCalculator.GetCoordinate(j9sJkRf4wMmhD3hBArray[0].Segment.MinPrice + _localInstance123._doubleInstance009));
////    double num3 = num2 / 2.0;
////    _localInstance123._double02 = ycoordinateCalculator.GetDataValue(-num2);
////    _localInstance123._double01 = ycoordinateCalculator.GetDataValue(height + num2);
////    IEnumerable<double> source = num1 != 0 ? pointSeries.AllPrices.Where<double>(new Func<double, bool>(_localInstance123.Method01)) : (IEnumerable<double>) TimeframeSegmentDataSeries.GeneratePrices(_localInstance123._double01, _localInstance123._double02, _localInstance123._doubleInstance009);
////    KeyValuePair<double, CandlePriceLevel>[] array = (num1 != 0 ? (IEnumerable<KeyValuePair<double, CandlePriceLevel>>) source.ToDictionary<double, double, CandlePriceLevel>(SomeClass34343383._instance02 ?? (SomeClass34343383._instance02 = new Func<double, double>(SomeClass34343383.SomeMethond0343.Method04)), new Func<double, CandlePriceLevel>(pointSeries.GetCandlePriceLevelFromPrice)) : (IEnumerable<KeyValuePair<double, CandlePriceLevel>>) source.ToDictionary<double, double, CandlePriceLevel>(SomeClass34343383._instance03 ?? (SomeClass34343383._instance03 = new Func<double, double>(SomeClass34343383.SomeMethond0343.Method05)), new Func<double, CandlePriceLevel>(_localInstance123.Method02))).Where<KeyValuePair<double, CandlePriceLevel>>(new Func<KeyValuePair<double, CandlePriceLevel>, bool>(_localInstance123.Method03)).ToArray<KeyValuePair<double, CandlePriceLevel>>();
////    Brush volumeBarsBrush = VolumeBarsBrush;
////    Color volBarsFontColor = VolBarsFontColor;
////    LinearGradientBrush linearGradientBrush = volumeBarsBrush as LinearGradientBrush;
////    Dictionary<double, Tuple<double, CandlePriceLevel>> dictionary = new Dictionary<double, Tuple<double, CandlePriceLevel>>();
////    if (!TemplateTypeHelper.IsNotEmpty<KeyValuePair<double, CandlePriceLevel>>(array))
////      return;
////    using (PenManager penManager = new PenManager(_param1, false, (float) StrokeThickness, Opacity))
////    {
////      IPen2D rhwYsZxA33iRu6Id7J = penManager.GetPen(linearGradientBrush != null ? linearGradientBrush.GradientStops[0].Color : volBarsFontColor);
////      IBrush2D xrgcdFbSdWgN9GcT8_1 = penManager.GetBrush(volumeBarsBrush);
////      IBrush2D xrgcdFbSdWgN9GcT8_2 = penManager.GetBrush(BuyColor);
////      IBrush2D xrgcdFbSdWgN9GcT8_3 = penManager.GetBrush(SellColor);
////      double num4 = (double) ((IEnumerable<KeyValuePair<double, CandlePriceLevel>>) array).Max<KeyValuePair<double, CandlePriceLevel>>(SomeClass34343383._instance04 ?? (SomeClass34343383._instance04 = new Func<KeyValuePair<double, CandlePriceLevel>, Decimal>(SomeClass34343383.SomeMethond0343.Method06)));
////      double volumeWidthFraction = HorizontalVolumeWidthFraction;
////      double num5 = width * volumeWidthFraction;
////      foreach ((double key, CandlePriceLevel candlePriceLevel) in array)
////      {
////        double num6 = ycoordinateCalculator.GetCoordinate(key) - num3;
////        double num7 = num6 + num2;
////        Decimal totalVolume = ((CandlePriceLevel) ref candlePriceLevel).TotalVolume;
////        double num8 = num5 * (double) totalVolume / num4;
////        Point point1 = new Point(0.0, num6);
////        Point point2 = new Point(num8, num7);
////        double num9 = 3.0 * Math.PI / 2.0;
////        if (num2 > 1.5)
////        {
////          _param1.DrawLine(rhwYsZxA33iRu6Id7J, point1, new Point(num8, num6));
////          if (DrawSeparateVolumes && (((CandlePriceLevel) ref candlePriceLevel).BuyVolume > 0M || ((CandlePriceLevel) ref candlePriceLevel).SellVolume > 0M))
////          {
////            double num10 = num8 * (double) (((CandlePriceLevel) ref candlePriceLevel).BuyVolume / totalVolume);
////            if (((CandlePriceLevel) ref candlePriceLevel).BuyVolume > 0M)
////              _param1.FillRectangle(xrgcdFbSdWgN9GcT8_2, new Point(0.0, num6 + 1.0), ((CandlePriceLevel) ref candlePriceLevel).SellVolume > 0M ? new Point(num10, num7) : point2, num9);
////            if (((CandlePriceLevel) ref candlePriceLevel).SellVolume > 0M)
////              _param1.FillRectangle(xrgcdFbSdWgN9GcT8_3, new Point(num10, num6 + 1.0), point2, num9);
////          }
////          else
////            _param1.FillRectangle(xrgcdFbSdWgN9GcT8_1, new Point(0.0, num6 + 1.0), point2, num9);
////        }
////        else
////          _param1.FillRectangle(xrgcdFbSdWgN9GcT8_1, point1, point2, num9);
////        dictionary[key] = Tuple.Create<double, CandlePriceLevel>(num8, candlePriceLevel);
////        string str = totalVolume.ToString();
////        int num11 = 2;
////        Rect rect = new Rect(new Point((double) num11, num6), new Point(num5 + (double) num11, num7));
////        (float, FontWeight, bool) tuple = _fontCalculator.GetFont(rect.Size, totalVolume.GetDecimalLength(), 9f);
////        float num12 = tuple.Item1;
////        FontWeight fontWeight = tuple.Item2;
////        if (tuple.Item3)
////          _param1.DrawText(str, rect, AlignmentX.Left, AlignmentY.Center, volBarsFontColor, num12, _fontCalculator.FontFamily, fontWeight);
////      }
////      _candlePriceLevelMap = dictionary;
////    }
////  }

////  protected override HitTestInfo NearestHitResult(
////    Point _param1,
////    double _param2,
////    SearchMode _param3,
////    bool _param4)
////  {
////    if (!(DataSeries is TimeframeSegmentDataSeries dataSeries) || dataSeries.Count < 1)
////      return HitTestInfo.Empty;
////    double num1 = (double) (PriceStep ?? 0.000001M);
////    ICoordinateCalculator<<double> xkzemsMs5tGkouk5w1 = CurrentRenderPassData.XCoordinateCalculator;
////    ICoordinateCalculator<<double> xkzemsMs5tGkouk5w2 = CurrentRenderPassData.YCoordinateCalculator;
////    int index = (int) xkzemsMs5tGkouk5w1.GetDataValue(_param1.X);
////    double num2 = xkzemsMs5tGkouk5w2.GetDataValue(_param1.Y);
////    double key = num2.NormalizePrice(num1);
////    double num3 = Math.Abs(xkzemsMs5tGkouk5w1.GetCoordinate(1.0) - xkzemsMs5tGkouk5w1.GetCoordinate(0.0));
////    double num4 = Math.Abs(xkzemsMs5tGkouk5w2.GetCoordinate(key) - xkzemsMs5tGkouk5w2.GetCoordinate(key + num1));
////    if (num3 < 1.0 || num4 < 1.0)
////      return HitTestInfo.Empty;
////    Tuple<double, CandlePriceLevel> tuple;
////    if (ShowHorizontalVolumes && _candlePriceLevelMap.TryGetValue(key, out tuple) && _param1.X <= tuple.Item1)
////    {
////      HitTestInfo zldchDrVsrVyHh6WyiGy = new HitTestInfo();
////      zldchDrVsrVyHh6WyiGy.DataSeriesName(dataSeries.SeriesName);
////      zldchDrVsrVyHh6WyiGy.DataSeriesType(dataSeries.DataSeriesType);
////      zldchDrVsrVyHh6WyiGy.YValue((IComparable) key);
////      zldchDrVsrVyHh6WyiGy.Volume(tuple.Item2);
////      zldchDrVsrVyHh6WyiGy.IsHit(true);
////      zldchDrVsrVyHh6WyiGy.HitTestPoint(new Point(tuple.Item1, xkzemsMs5tGkouk5w2.GetCoordinate(key)));
////      return zldchDrVsrVyHh6WyiGy;
////    }
////    if (index < 0 || index >= dataSeries.Count)
////      return HitTestInfo.Empty;
////    TimeframeDataSegment segment = dataSeries.Segments[index];
////    CandlePriceLevel candlePriceLevel = segment.GetCandlePriceLevel(num2, num1);
////    if (((CandlePriceLevel) ref candlePriceLevel).TotalVolume == 0M)
////      return HitTestInfo.Empty;
////    HitTestInfo zldchDrVsrVyHh6WyiGy1 = new HitTestInfo();
////    zldchDrVsrVyHh6WyiGy1.DataSeriesName(dataSeries.SeriesName);
////    zldchDrVsrVyHh6WyiGy1.DataSeriesType(dataSeries.DataSeriesType);
////    zldchDrVsrVyHh6WyiGy1.XValue((IComparable) segment.Time);
////    zldchDrVsrVyHh6WyiGy1.YValue((IComparable) key);
////    zldchDrVsrVyHh6WyiGy1.DataSeriesIndex(index);
////    zldchDrVsrVyHh6WyiGy1.Volume(candlePriceLevel);
////    zldchDrVsrVyHh6WyiGy1.IsHit(true);
////    zldchDrVsrVyHh6WyiGy1.HitTestPoint(new Point(xkzemsMs5tGkouk5w1.GetCoordinate((double) index), xkzemsMs5tGkouk5w2.GetCoordinate(key)));
////    zldchDrVsrVyHh6WyiGy1.OpenValue((IComparable) segment.FirstPrice);
////    zldchDrVsrVyHh6WyiGy1.HighValue((IComparable) segment.MaxPrice);
////    zldchDrVsrVyHh6WyiGy1.LowValue((IComparable) segment.MinPrice);
////    zldchDrVsrVyHh6WyiGy1.CloseValue((IComparable) segment.LastPrice);
////    HitTestInfo zldchDrVsrVyHh6WyiGy2 = zldchDrVsrVyHh6WyiGy1;
////    return HitTestSeriesWithBody(_param1, zldchDrVsrVyHh6WyiGy2, _param2);
////  }

////  protected override HitTestInfo HitTestSeriesWithBody(
////    Point _param1,
////    HitTestInfo _param2,
////    double _param3)
////  {
////    return _param2;
////  }

////  protected sealed class FontCalculator
////  {
////    private readonly FontInfo[] _fontInfos;
////    private readonly FontInfo _biggestFont;
////    private readonly FontInfo _smallestFont;
////    private readonly string _fontFamily;
////    private readonly Dictionary<Tuple<int, int>, FontInfo> _fontInfosDict = new Dictionary<Tuple<int, int>, FontInfo>();
////    private readonly Dictionary<float, FontInfo> _fontInfoBySizeDict = new Dictionary<float, FontInfo>();
////    private readonly Stopwatch _watch = new Stopwatch();

////    public FontCalculator(
////      IRenderContext2D _param1,
////      string _param2,
////      float _param3,
////      FontWeight _param4)
////    {
////      _watch.Restart();
////      _fontFamily = _param2;
////      _param3 = Math.Min(32f, _param3).Round(0.5f);
////      _fontInfos = new FontInfo[1 + (int) Math.Round(((double) _param3 - 7.0) / 0.5)];
////      for (float num = 7f; (double) num <= (double) _param3; num += 0.5f)
////      {
////        FontWeight fontWeight = (double) num <= 8.5 ? FontWeights.ExtraLight : ((double) num <= 10.0 ? FontWeights.Light : _param4);
////        Size size = _param1.DigitMaxSize(num, _fontFamily, fontWeight);
////        _fontInfos[(int) Math.Round(((double) num - 7.0) / 0.5)] = new FontInfo(num, fontWeight, size);
////      }
////      _smallestFont = _fontInfos[0];
////      FontInfo[] zEdGaOlhSe7Xw = _fontInfos;
////      _biggestFont = zEdGaOlhSe7Xw[zEdGaOlhSe7Xw.Length - 1];
////      double d1;
////      double d2 = d1 = double.MaxValue;
////      double a1;
////      double a2 = a1 = double.MinValue;
////      foreach (FontInfo zvzdRt5Y in _fontInfos)
////      {
////        Size size = zvzdRt5Y.SymbolDimensions;
////        double width = size.Width;
////        size = zvzdRt5Y.SymbolDimensions;
////        double height = size.Height;
////        if (width < d2)
////          d2 = width;
////        if (width > a2)
////          a2 = width;
////        if (height < d1)
////          d1 = height;
////        if (height > a1)
////          a1 = height;
////        _fontInfosDict[Tuple.Create<int, int>((int) Math.Round(width), (int) Math.Round(height))] = zvzdRt5Y;
////        _fontInfoBySizeDict[zvzdRt5Y.FontSize] = zvzdRt5Y;
////      }
////      int num1 = (int) Math.Floor(d2);
////      int num2 = (int) Math.Ceiling(a2);
////      int num3 = (int) Math.Floor(d1);
////      int num4 = (int) Math.Ceiling(a1);
////      int[] array = Enumerable.Range(0, _fontInfos.Length).Reverse<int>().ToArray<int>();
////      FontCalculator.SomeInternalSealedClass5vcJ qws5VcJaSLcUcBty = new FontCalculator.SomeInternalSealedClass5vcJ();
////      for (qws5VcJaSLcUcBty._instance05 = num1; qws5VcJaSLcUcBty._instance05 <= num2; ++qws5VcJaSLcUcBty._instance05)
////      {
////        FontCalculator.SomeInternalSealedClassDzAsDB eiGfnlxs70MiqCaxA = new FontCalculator.SomeInternalSealedClassDzAsDB();
////        eiGfnlxs70MiqCaxA._instance07 = qws5VcJaSLcUcBty;
////        for (eiGfnlxs70MiqCaxA._instance06 = num3; eiGfnlxs70MiqCaxA._instance06 <= num4; ++eiGfnlxs70MiqCaxA._instance06)
////        {
////          Tuple<int, int> key = Tuple.Create<int, int>(eiGfnlxs70MiqCaxA._instance07._instance05, eiGfnlxs70MiqCaxA._instance06);
////          if (!_fontInfosDict.ContainsKey(key))
////            _fontInfosDict[key] = ((IEnumerable<int>) array).Select<int, FontInfo>(new Func<int, FontInfo>(OnLineOnePropertyChanged)).First<FontInfo>(new Func<FontInfo, bool>(eiGfnlxs70MiqCaxA.OnChartAreaElementsRemovingAt));
////        }
////      }
////      _watch.Stop();
////      SciChartDebugLogger.Instance.WriteLine("Initialized font calculator. Font={0}, MaxSize={1}, MaxWeight={2}, dict.Size={3}, initTime={4:F3}ms", new object[5]
////      {
////        _param2,
////        _param3,
////        _param4,
////        _fontInfosDict.Count,
////        _watch.Elapsed.TotalMilliseconds
////      });
////    }

////    public string FontFamily => _fontFamily;

////    public double SmallestFontSDHeight()
////    {
////      return _smallestFont.SymbolDimensions.Height;
////    }

////    public double SmallestFontSDWidth()
////    {
////      return _smallestFont.SymbolDimensions.Width;
////    }

////    public (float, FontWeight, bool) GetFont(
////      Size _param1,
////      int _param2,
////      float _param3)
////    {
////      _param2 = Math.Max(_param2, 1);
////      if ((double) _param3 != 0.0)
////        _param3 = Math.Min(Math.Max(_param3, 7f), 32f).Round(0.5f);
////      try
////      {
////        double num1 = _param1.Width / (double) _param2;
////        double num2 = num1;
////        Size size = _smallestFont.SymbolDimensions;
////        double width1 = size.Width;
////        if (num2 >= width1)
////        {
////          double height1 = _param1.Height;
////          size = _smallestFont.SymbolDimensions;
////          double height2 = size.Height;
////          if (height1 >= height2)
////          {
////            double num3 = num1;
////            size = _biggestFont.SymbolDimensions;
////            double width2 = size.Width;
////            if (num3 >= width2)
////            {
////              double height3 = _param1.Height;
////              size = _biggestFont.SymbolDimensions;
////              double height4 = size.Height;
////              if (height3 >= height4)
////                return (_biggestFont.FontSize, _biggestFont.FontWeight, true);
////            }
////            double val1 = num1;
////            size = _biggestFont.SymbolDimensions;
////            double width3 = size.Width;
////            int num4 = (int) Math.Floor(Math.Min(val1, width3));
////            double height5 = _param1.Height;
////            size = _biggestFont.SymbolDimensions;
////            double height6 = size.Height;
////            int num5 = (int) Math.Floor(Math.Min(height5, height6));
////            FontInfo tmaYvnm94O29pOcja = _fontInfosDict[Tuple.Create<int, int>(num4, num5)];
////            if ((double) _param3 == 0.0 || (double) tmaYvnm94O29pOcja.FontSize >= (double) _param3)
////              return (tmaYvnm94O29pOcja.FontSize, tmaYvnm94O29pOcja.FontWeight, true);
////            if (!_fontInfoBySizeDict.TryGetValue(_param3, out tmaYvnm94O29pOcja))
////              tmaYvnm94O29pOcja = _smallestFont;
////            return (tmaYvnm94O29pOcja.FontSize, tmaYvnm94O29pOcja.FontWeight, false);
////          }
////        }
////        if ((double) _param3 == 0.0)
////          return ();
////        FontInfo tmaYvnm94O29pOcja1;
////        if (!_fontInfoBySizeDict.TryGetValue(_param3, out tmaYvnm94O29pOcja1))
////          tmaYvnm94O29pOcja1 = _smallestFont;
////        return (tmaYvnm94O29pOcja1.FontSize, tmaYvnm94O29pOcja1.FontWeight, false);
////      }
////      catch (Exception ex)
////      {
////        SciChartDebugLogger.Instance.WriteLine("GetFont({0}x{1}, {2}, {3:0.##}) error: {4}", new object[5]
////        {
////          _param1.Width,
////          _param1.Height,
////          _param2,
////          _param3,
////          ex
////        });
////        FontInfo zvzdRt5Y = _fontInfoBySizeDict[Math.Max(_param3, 7f)];
////        return (zvzdRt5Y.FontSize, zvzdRt5Y.FontWeight, false);
////      }
////    }

////    public bool HasFont(Size _param1, int _param2)
////    {
////      (float, FontWeight, bool) tuple = GetFont(_param1, _param2, 0.0f);
////      return (double) tuple.Item1 != 0.0 || tuple.Item2 != new FontWeight() || tuple.Item3;
////    }

////    private FontInfo OnLineOnePropertyChanged(
////      int _param1)
////    {
////      return _fontInfos[_param1];
////    }

////    private sealed class SomeInternalSealedClassDzAsDB
////    {
////      public int _instance06;
////      public FontCalculator.SomeInternalSealedClass5vcJ _instance07;

////      public bool OnChartAreaElementsRemovingAt(
////        FontInfo _param1)
////      {
////        return _param1.SymbolDimensions.Width <= (double) _instance07._instance05 && _param1.SymbolDimensions.Height <= (double) _instance06;
////      }
////    }

////    private sealed class SomeInternalSealedClass5vcJ
////    {
////      public int _instance05;
////    }

////    private sealed class FontInfo(
////      float _param1,
////      FontWeight _param2,
////      Size _param3)
////    {
////      private readonly float _fontSize = _param1.Round(0.5f);
////      private readonly FontWeight _fontWeight = _param2;
////      private readonly Size _symbolDimensions = _param3;

////      public float FontSize => _fontSize;

////      public FontWeight FontWeight => _fontWeight;

////      public Size SymbolDimensions => _symbolDimensions;
////    }
////  }

////  [Serializable]
////  private new sealed class SomeClass34343383
////  {
////    public static readonly SomeClass34343383 SomeMethond0343 = new SomeClass34343383();
////    public static Func<double, double> _instance02;
////    public static Func<double, double> _instance03;
////    public static Func<KeyValuePair<double, CandlePriceLevel>, Decimal> _instance04;

////    public double Method04(double _param1)
////    {
////      return _param1;
////    }

////    public double Method05(double _param1)
////    {
////      return _param1;
////    }

////    public Decimal Method06(
////      KeyValuePair<double, CandlePriceLevel> _param1)
////    {
////      CandlePriceLevel candlePriceLevel = _param1.Value;
////      return ((CandlePriceLevel) ref candlePriceLevel).TotalVolume;
////    }
////  }

////  private sealed class SomeInternalSealedClassTwzt9h
////  {
////    public double _double01;
////    public double _double02;
////    public TimeframeSegmentDataSeries _tfds01;
////    public double _doubleInstance009;

////    public bool Method01(double _param1)
////    {
////      return _param1 > _double01 && _param1 < _double02;
////    }

////    public CandlePriceLevel Method02(double _param1)
////    {
////      return _tfds01.GetVolumeByPrice(_param1, _doubleInstance009);
////    }

////    public bool Method03(
////      KeyValuePair<double, CandlePriceLevel> _param1)
////    {
////      if (_param1.Key <= _double01 || _param1.Key >= _double02)
////        return false;
////      CandlePriceLevel candlePriceLevel = _param1.Value;
////      return ((CandlePriceLevel) ref candlePriceLevel).TotalVolume > 0M;
////    }
////  }
////}
