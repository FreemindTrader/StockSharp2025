using DevExpress.Xpf.Charts.Native;
using SciChart.Charting.Common.Extensions;
using SciChart.Charting.Numerics.CoordinateCalculators;
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Charting.Visuals.RenderableSeries.HitTesters;
using SciChart.Core.Utility;
using SciChart.Data.Model;
using SciChart.Drawing.Common;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;

#nullable disable
namespace StockSharp.Xaml.Charting;

public sealed class ClusterProfileRenderableSeries : TimeframeSegmentRenderableSeries
{
    public class ClusterProfileHitTestProvider : DefaultHitTestProvider<ClusterProfileRenderableSeries>
    {
        public ClusterProfileHitTestProvider(ClusterProfileRenderableSeries renderSeries) : base(renderSeries)
        {
        }

        protected override HitTestInfo HitTestSeriesWithBody(Point rawPoint, HitTestInfo nearestHitPoint, double hitTestRadius)
        {
            RenderContextDetails lastContext = RenderableSeries._lastContext;
            int dataSeriesIndex = nearestHitPoint.DataSeriesIndex;

            if ( !( RenderableSeries.DataSeries is TimeframeSegmentDataSeries dataSeries ) || dataSeries.Count < 1 || lastContext == null || dataSeriesIndex < 0 || dataSeriesIndex >= dataSeries.Count )
                return HitTestInfo.Empty;

            double coordinate = lastContext.XCalc.GetCoordinate((double)dataSeriesIndex);
            double x = coordinate + lastContext.SegmentWidth;

            if ( rawPoint.X < coordinate || rawPoint.X > x )
                return HitTestInfo.Empty;

            nearestHitPoint.HitTestPoint=( new Point(x, nearestHitPoint.HitTestPoint.Y) );

            return nearestHitPoint;
        }
    }

    private sealed class RenderContextDetails
    {
        public ICoordinateCalculator<double> XCalc;
        public ICoordinateCalculator<double> YCalc;
        public double ScreenWidth;
        public double ScreenHeight;
        public double SegmentWidth;
        public double HalfSegmentWidth;
        public double PriceLevelHeight;
        public double HalfPriceLevelHeight;
        public Color DefaultFontColor;
        public IRenderContext2D RenderContext;
        public IPen2D BarSeparatorPen;
    }

    [StructLayout(LayoutKind.Auto)]
    private struct DrawInfo
    {

        public ClusterProfileRenderableSeries _rSeries;

        public float _minFontSize;

        public RenderContextDetails _rcDetails;

        public bool _isVerticalChart;
    }

    private enum DrawStartPoint
    {
        BottomLeft,
        BottomRight,
        TopRight,
        TopLeft,
    }

    private sealed class ColorDetails
    {
        public IndexRange VisibleRange;
        public PenManager PenManager;
        public Color ClusterMaxColor;
        public Color ClusterColor;
        public Color BuyColor;
        public Color SellColor;

        public bool Method013(
          TimeframeIndexedSegment p)
        {
            return p.Segment.Count >= this.VisibleRange.Min && p.Segment.Count <= this.VisibleRange.Max;
        }
    }

    private sealed class BarIterator( int count, double maxValue, Action<BarIterator> onNextBar)
    {
        private readonly int _count = count;
        private readonly Action<BarIterator> _onNextBar = onNextBar;
        private int _index;
        private double _price;
        private CandlePriceLevel _candlePriceLevel;
        private IBrush2D _clusterColorBrush;
        private IBrush2D _buyColorBrush;
        private IBrush2D _sellColorBrush;
        private Color _textColor;
        private readonly double _maxValue = maxValue;
        private IPen2D _clusterMaxColorPen;

        public int Count => this._index;

        public void Reset() => this._index = -1;

        public bool NextBar()
        {
            if ( ++this._index >= this._count )
                return false;
            this._onNextBar(this);
            return true;
        }

        public double Price
        {
            get
            {
                return this._price;
            }

            set
            {
                this._price = value;
            }
        }


        public CandlePriceLevel Value
        {
            get => this._candlePriceLevel;
            set => this._candlePriceLevel = value;
        }

        public IBrush2D ClusterColorBrush
        {
            get
            {
                return this._clusterColorBrush;
            }

            set
            {
                this._clusterColorBrush = value;
            }
        }



        public IBrush2D BuyColorBrush
        {
            get
            {
                return this._buyColorBrush;
            }

            set
            {
                this._buyColorBrush = value;
            }
        }



        public IBrush2D SellColorBrush
        {
            get
            {
                return this._sellColorBrush;
            }

            set
            {
                this._sellColorBrush = value;
            }
        }

        public Color TextColor
        {
            get
            {
                return this._textColor;
            }

            set
            {
                this._textColor = value;
            }
        }

        public double MaxValue => this._maxValue;

        public IPen2D ClusterMaxColorPen
        {
            get
            {
                return this._clusterMaxColorPen;
            }

            set
            {
                this._clusterMaxColorPen = value;
            }

        }
    }

    

    public static readonly DependencyProperty SeparatorLineColorProperty = DependencyProperty.Register(nameof(SeparatorLineColor), typeof(Color), typeof(ClusterProfileRenderableSeries), new PropertyMetadata((object)Color.FromArgb((byte)50, byte.MaxValue, byte.MaxValue, byte.MaxValue), new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

    public static readonly DependencyProperty LineColorProperty = DependencyProperty.Register(nameof(LineColor), typeof(Color), typeof(ClusterProfileRenderableSeries), new PropertyMetadata((object)Colors.DarkGray, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

    public static readonly DependencyProperty TextColorProperty = DependencyProperty.Register(nameof(TextColor), typeof(Color), typeof(ClusterProfileRenderableSeries), new PropertyMetadata((object)Color.FromRgb((byte)90, (byte)90, (byte)90), new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

    public static readonly DependencyProperty ClusterColorProperty = DependencyProperty.Register(nameof(ClusterColor), typeof(Color), typeof(ClusterProfileRenderableSeries), new PropertyMetadata((object)Colors.DarkGreen, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

    public static readonly DependencyProperty ClusterMaxColorProperty = DependencyProperty.Register(nameof(ClusterMaxColor), typeof(Color), typeof(ClusterProfileRenderableSeries), new PropertyMetadata((object)Colors.LimeGreen, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

    private RenderContextDetails _lastContext;

    public ClusterProfileRenderableSeries()
    {
        this.DefaultStyleKey = (object)typeof(ClusterProfileRenderableSeries);
    }

    public Color SeparatorLineColor
    {
        get
        {
            return (Color)this.GetValue(SeparatorLineColorProperty);
        }
        set
        {
            this.SetValue(SeparatorLineColorProperty, (object)value);
        }
    }

    public Color LineColor
    {
        get
        {
            return (Color)this.GetValue(LineColorProperty);
        }
        set
        {
            this.SetValue(LineColorProperty, (object)value);
        }
    }

    public Color TextColor
    {
        get
        {
            return (Color)this.GetValue(TextColorProperty);
        }
        set
        {
            this.SetValue(TextColorProperty, (object)value);
        }
    }

    public Color ClusterColor
    {
        get
        {
            return (Color)this.GetValue(ClusterColorProperty);
        }
        set
        {
            this.SetValue(ClusterColorProperty, (object)value);
        }
    }

    public Color ClusterMaxColor
    {
        get
        {
            return (Color)this.GetValue(ClusterMaxColorProperty);
        }
        set
        {
            this.SetValue(ClusterMaxColorProperty, (object)value);
        }
    }

    protected override void OnDrawImpl(IRenderContext2D renderContext, IRenderPassData renderPassData)
    {
        ColorDetails ci = new ColorDetails();
        if ( !( this.DataSeries is TimeframeSegmentDataSeries ) )
            return;

        var pointSeries           = (TimeframeSegmentPointSeries)this.CurrentRenderPassData.PointSeries;
        var segments              = pointSeries.Segments;
        var priceStep             = pointSeries.PriceStep;

        var rc                    = new RenderContextDetails();
        rc.XCalc                  = this.CurrentRenderPassData.XCoordinateCalculator;
        rc.YCalc                  = this.CurrentRenderPassData.YCoordinateCalculator;
        rc.ScreenHeight           = renderContext.ViewportSize.Height;
        rc.ScreenWidth            = renderContext.ViewportSize.Width;
        rc.RenderContext          = renderContext;
        rc.DefaultFontColor       = this.TextColor;

        this._lastContext         = rc;

        rc.SegmentWidth           = Math.Abs(rc.XCalc.GetCoordinate(1.0) - rc.XCalc.GetCoordinate(0.0));
        rc.PriceLevelHeight       = Math.Abs(rc.YCalc.GetCoordinate(segments[0].Segment.LowPrice) - rc.YCalc.GetCoordinate(segments[0].Segment.LowPrice + priceStep));
        rc.HalfSegmentWidth       = rc.SegmentWidth / 2.0;
        rc.HalfPriceLevelHeight   = rc.PriceLevelHeight / 2.0;
        ci.ClusterColor           = this.ClusterColor;
        ci.ClusterMaxColor        = this.ClusterMaxColor;

        Color textColor           = this.TextColor;
        double minDrawPrice       = rc.YCalc.GetDataValue(rc.ScreenHeight + rc.PriceLevelHeight);
        double maxDrawPrice       = rc.YCalc.GetDataValue(-rc.PriceLevelHeight);
        ci.VisibleRange           = pointSeries.VisibleRange;
        ci.BuyColor               = this.BuyColor;
        ci.SellColor              = this.SellColor;

        if ( segments.Length < 1 )
            return;

        if ( minDrawPrice > maxDrawPrice )
            throw new InvalidOperationException($"minDrawPrice({minDrawPrice}) > maxDrawPrice({maxDrawPrice})");
                       
        SciChartDebugLogger.Instance.WriteLine("ClusterProfile: started render {0} segments. Indexes: {1}-{2}, VisibleRange: {3}-{4}", segments.Length, segments[0].Segment.Count, segments[segments.Length - 1].Segment.Count, ci.VisibleRange.Min, ci.VisibleRange.Max);

        var selectedSegment = segments.Where(p => p.Segment.Count >= pointSeries.VisibleRange.Min && p.Segment.Count <= pointSeries.VisibleRange.Max).ToArray();

        if ( !TemplateTypeHelper.IsNotEmpty(selectedSegment) )
            return;

        ci.PenManager = new PenManager(renderContext, false, (float)this.StrokeThickness, this.Opacity);
        try
        {
            var lineColorPen   = ci.PenManager.GetPen(this.LineColor);
            rc.BarSeparatorPen = ci.PenManager.GetPen(this.SeparatorLineColor);            
            var newStroke      = this.StrokeThickness + 2;
            var upPen          = ci.PenManager.GetPen(this.UpColor, new float?((float)newStroke));
            var downPen        = ci.PenManager.GetPen(this.DownColor, new float?((float)newStroke));

            foreach ( TimeframeIndexedSegment wrapper in selectedSegment )
            {
                var tfds  = wrapper.Segment;
                var xCoor = rc.XCalc.GetCoordinate(wrapper.X);                
                
                if ( wrapper.Segment.LowPrice <= maxDrawPrice && tfds.HighPrice >= minDrawPrice )
                {
                    double maxPriceMinus = rc.YCalc.GetCoordinate(tfds.HighPrice) - rc.HalfPriceLevelHeight;
                    double minPricePlus = rc.YCalc.GetCoordinate(tfds.LowPrice) + rc.HalfPriceLevelHeight;
                    ( var priceLevelsArrary, var volume ) = tfds.GetCPLVFromPriceStep(priceStep);

                    renderContext.DrawLine(lineColorPen, new Point(xCoor, maxPriceMinus + 1.0), new Point(xCoor, minPricePlus));
                    
                    if ( rc.SegmentWidth >= 3.0 )
                    {
                        BarIterator bar = new BarIterator(priceLevelsArrary.Length, (double)volume, p =>
                        {
                            var pLvl             = priceLevelsArrary[(int)p.Count];
                            p.Price              = pLvl.Key;
                            p.Value              = pLvl.Value;
                            
                            var lvlValue         = pLvl.Value;                            
                            p.ClusterMaxColorPen = lvlValue.TotalVolume == volume ? ci.PenManager.GetPen(ci.ClusterMaxColor) : (IPen2D)null;
                            p.ClusterColorBrush  = ci.PenManager.GetBrush(ci.ClusterColor);
                            p.BuyColorBrush      = ci.PenManager.GetBrush(ci.BuyColor) ;
                            p.SellColorBrush     = ci.PenManager.GetBrush(ci.SellColor) ;
                        });

                        bar.TextColor = textColor;                        
                        this.DrawTimeframeSegments(tfds, bar, rc, rc.SegmentWidth + 1.0, rc.SegmentWidth - 1.0, DrawStartPoint.TopLeft, 0.0f, (float)newStroke, upPen, downPen);
                    }
                }
            }
        }
        finally
        {
            if ( ci.PenManager != null )
                ci.PenManager.Dispose();
        }
    }
    
    /// <summary>
    /// Draws a histogram for the given timeframe data segment.
    /// </summary>
    /// <param name="tfds"></param>
    /// <param name="bar"></param>
    /// <param name="rcDetails"></param>
    /// <param name="baselineCoord"></param>
    /// <param name="barMaxHeight"></param>
    /// <param name="drawStartPoint"></param>
    /// <param name="minFontSize"></param>
    /// <param name="shift"></param>
    /// <param name="risingPen"></param>
    /// <param name="fallingPen"></param>
    private void DrawTimeframeSegments(TimeframeDataSegment tfds, BarIterator bar, RenderContextDetails rcDetails, double baselineCoord, double barMaxHeight, DrawStartPoint drawStartPoint, float minFontSize, float shift, IPen2D risingPen, IPen2D fallingPen)
    {
        DrawInfo di;
        di._rSeries     = this;
        di._minFontSize = minFontSize;
        di._rcDetails   = rcDetails;

        if ( barMaxHeight <= 1.0 )
            return;

        double barMaxH = Math.Max(1.0, barMaxHeight - 5.0);
        ICoordinateCalculator<double> calc;
        int horizontalSign;
        int verticalSign;

        if ( drawStartPoint == DrawStartPoint.BottomLeft || drawStartPoint == DrawStartPoint.TopRight )
        {
            di._isVerticalChart = true;
            calc                = di._rcDetails.XCalc;
            horizontalSign      = 1;
            verticalSign        = drawStartPoint == DrawStartPoint.BottomLeft ? -1 : 1;
        }
        else
        {
            di._isVerticalChart = false;
            calc                = di._rcDetails.YCalc;
            horizontalSign      = drawStartPoint == DrawStartPoint.TopLeft ? 1 : -1;
            verticalSign        = -1;
        }
        bar.Reset();

        if ( di._isVerticalChart )
        {
            bool hasValidFont = (double)di._minFontSize > 0.0 || this._fontCalculator.HasFont(new Size(di._rcDetails.SegmentWidth, this._fontCalculator.SmallestFontSDHeight()), 1);
            AlignmentY alignmentY = drawStartPoint == DrawStartPoint.BottomLeft ? AlignmentY.Top : AlignmentY.Bottom;
            double maxValue = bar.MaxValue;

            while ( bar.NextBar() )
            {
                var priceLvl = bar.Value;
                var startPoint = calc.GetCoordinate(bar.Price);
                var startValue = baselineCoord;
                var endPoint = startPoint + di._rcDetails.SegmentWidth - 1.0;
                var endValue = startValue + (double)verticalSign * ( barMaxH * (double)( (CandlePriceLevel)priceLvl ).TotalVolume / maxValue );

                if ( startPoint > endPoint )
                    NumberUtil.Swap(ref startPoint, ref endPoint);

                if ( startValue > endValue )
                    NumberUtil.Swap(ref startValue, ref endValue);

                if ( endValue - startValue >= 0.5 )
                {
                    Point startPt = new Point(startPoint, startValue);
                    Point endPt = new Point(endPoint, endValue);

                    this.DrawBar(startPt, endPt, bar, ref di);

                    if ( bar.ClusterMaxColorPen != null )
                        di._rcDetails.RenderContext.DrawQuad(bar.ClusterMaxColorPen, startPt, endPt);

                    if ( hasValidFont )
                    {
                        double startValue2 = startValue + 2.0;
                        this.DrawVolumeText(bar, priceLvl, new Point(startPoint, startValue2), new Point(endPoint, startValue + (double)verticalSign * barMaxH), AlignmentX.Center, alignmentY, ref di);
                    }
                }
            }
        }
        else
        {
            bool hasValidFont = (double)di._minFontSize > 0.0 || di._rcDetails.PriceLevelHeight >= this._fontCalculator.SmallestFontSDHeight() && barMaxH >= this._fontCalculator.SmallestFontSDWidth();
            AlignmentX alignmentX = drawStartPoint == (DrawStartPoint)3 ? AlignmentX.Left : AlignmentX.Right;
            double barMaxValue = bar.MaxValue;

            if ( tfds.OpenPrice.HasValue )
            {
                var openPrice  = tfds.OpenPrice;
                var closePrice = tfds.ClosePrice;
                var drawingPen = openPrice.GetValueOrDefault() <= closePrice & openPrice.HasValue ? risingPen : fallingPen;
                var minPriceY  = di._rcDetails.YCalc.GetCoordinate(tfds.LowPrice);
                var maxPriceY  = di._rcDetails.YCalc.GetCoordinate(tfds.HighPrice);

                di._rcDetails.RenderContext.DrawLine(drawingPen, new Point(baselineCoord, minPriceY), new Point(baselineCoord, maxPriceY));

                baselineCoord += (double)shift;
            }

            while ( bar.NextBar() )
            {
                var candlePriceLevel = bar.Value;
                var startValue       = baselineCoord;
                var startPoint       = calc.GetCoordinate(bar.Price) - (double)verticalSign * di._rcDetails.HalfPriceLevelHeight;
                var endValue         = startValue + ( barMaxValue > 0.0 ? (double)horizontalSign * ( barMaxH * (double)( (CandlePriceLevel)candlePriceLevel ).TotalVolume / barMaxValue ) : 0.0 );
                var endPoint         = startPoint + (double)verticalSign * di._rcDetails.PriceLevelHeight - (double)verticalSign;

                if ( startValue > endValue )
                    NumberUtil.Swap(ref startValue, ref endValue);

                if ( startPoint > endPoint )
                    NumberUtil.Swap(ref startPoint, ref endPoint);

                if ( endValue - startValue >= 0.5 )
                {
                    Point startPt = new Point(startValue, startPoint);
                    Point endPt = new Point(endValue, endPoint);
                    this.DrawBar(startPt, endPt, bar, ref di);

                    if ( bar.ClusterMaxColorPen != null )
                        di._rcDetails.RenderContext.DrawQuad(bar.ClusterMaxColorPen, startPt, endPt);

                    if ( hasValidFont )
                    {
                        double num18 = startValue + 2.0;
                        this.DrawVolumeText(bar, candlePriceLevel, new Point(num18, startPoint), new Point(num18 + ( barMaxValue > 0.0 ? (double)horizontalSign * barMaxH : 0.0 ), endPoint), alignmentX, AlignmentY.Center, ref di);
                    }
                }
            }
        }
    }




    /// <summary>
    /// The following function will draw the Total Volume text for the given bar.
    /// </summary>
    /// <param name="bar"></param>
    /// <param name="pl"></param>
    /// <param name="pt1"></param>
    /// <param name="pt2"></param>
    /// <param name="alignX"></param>
    /// <param name="alignY"></param>
    /// <param name="di"></param>
    private void DrawVolumeText(BarIterator bar, CandlePriceLevel pl, Point pt1, Point pt2, AlignmentX alignX, AlignmentY alignY, ref DrawInfo di)
    {
        string volString = ( (CandlePriceLevel)pl ).TotalVolume.ToString();

        Rect rect = new Rect(pt1, pt2);

        (float, FontWeight, bool) tuple = this._fontCalculator.GetFont(rect.Size, volString.Length, di._minFontSize);

        float fontSize = tuple.Item1;
        FontWeight fontWeight = tuple.Item2;

        if ( !tuple.Item3 )
            return;

        var myff = new System.Windows.Media.FontFamily(_fontCalculator.FontFamily);
        di._rcDetails.RenderContext.DrawText(rect, bar.TextColor, fontSize, volString, myff, bar.ClusterMaxColorPen == null ? FontWeights.Normal : fontWeight, FontStyles.Normal);
    }

    /// <summary>
    /// This function will draw a specific bar. The bar will have two portions, one represented by the buy volume and the other by the sell volume.
    /// </summary>
    /// <param name="pt1"></param>
    /// <param name="pt2"></param>
    /// <param name="bar"></param>
    /// <param name="di"></param>
    private void DrawBar(Point pt1, Point pt2, BarIterator bar, ref DrawInfo di)
    {
        var priceLvl = bar.Value;
        var totalVolume = ( (CandlePriceLevel)priceLvl ).TotalVolume;
        if ( totalVolume == 0M )
            return;
        
        var buyVolume  = priceLvl.BuyVolume;        
        var sellVolume = priceLvl.SellVolume;

        if ( !this.DrawSeparateVolumes || buyVolume == 0M && sellVolume == 0M )
        {
            di._rcDetails.RenderContext.FillRectangle(bar.ClusterColorBrush, pt1, pt2, 0.0);
        }
        else
        {
            var buyPercentage = (double)( buyVolume / totalVolume );
            var sellPercentage = (double)( sellVolume / totalVolume );
            
            if ( di._isVerticalChart )
            {
                var yLength    = pt2.Y - pt1.Y;
                var buyLength  = yLength * buyPercentage;
                var sellLength = yLength * sellPercentage;
                var drawPt     = pt2.Y - buyLength;

                if ( buyLength > 0.0 )
                    di._rcDetails.RenderContext.FillRectangle(bar.BuyColorBrush, new Point(pt1.X, drawPt), sellLength > 0.0 ? pt2 : new Point(pt2.X, drawPt), 0.0);

                if ( sellLength <= 0.0 )
                    return;

                di._rcDetails.RenderContext.FillRectangle(bar.SellColorBrush, pt1, new Point(pt2.X, drawPt), 0.0);
            }
            else
            {
                var xLength    = pt2.X - pt1.X;
                var buyLength  = xLength * buyPercentage;
                var sellLength = xLength * sellPercentage;
                var drawPt     = pt1.X + buyLength;
                
                if ( buyLength > 0.0 )
                    di._rcDetails.RenderContext.FillRectangle(bar.BuyColorBrush, pt1, sellLength > 0.0 ? new Point(drawPt, pt2.Y) : pt2, 0.0);
                
                if ( sellLength <= 0.0 )
                    return;
                
                di._rcDetails.RenderContext.FillRectangle(bar.SellColorBrush, new Point(drawPt, pt1.Y), pt2, 0.0);
            }
        }
    }












}
