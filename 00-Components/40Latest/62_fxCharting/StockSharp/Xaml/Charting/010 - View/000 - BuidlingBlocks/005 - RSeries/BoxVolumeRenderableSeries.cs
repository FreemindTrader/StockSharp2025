using Ecng.ComponentModel;
using SciChart.Charting.Numerics.CoordinateCalculators;
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Core.Utility;
using SciChart.Data.Model;
using SciChart.Drawing.Common;
using StockSharp.Algo.Indicators;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace StockSharp.Xaml.Charting;

public sealed class BoxVolumeRenderableSeries : TimeframeSegmentRenderableSeries
{
    private new sealed class SomeSealClass3Dz7qOdp
    {
        public static readonly BoxVolumeRenderableSeries.SomeSealClass3Dz7qOdp _instance01 = new BoxVolumeRenderableSeries.SomeSealClass3Dz7qOdp();
        public static Func<TimeframeIndexedSegment, TimeframeDataSegment> _instance02;
        public static Func<TimeframeIndexedSegment, TimeframeDataSegment> _instance03;

        internal TimeframeDataSegment Method01(
          TimeframeIndexedSegment p)
        {
            return p.Segment;
        }

        internal TimeframeDataSegment Method02(
          TimeframeIndexedSegment _param1)
        {
            return _param1.Segment;
        }
    }

    public static readonly DependencyProperty Timeframe2Property = DependencyProperty.Register(nameof(Timeframe2), typeof(TimeSpan?), typeof(BoxVolumeRenderableSeries), new PropertyMetadata((object)TimeSpan.FromMinutes(5.0), new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface), new CoerceValueCallback(BoxVolumeRenderableSeries.CoerceHigherTimeframe)));

    public static readonly DependencyProperty Timeframe3Property = DependencyProperty.Register(nameof(Timeframe3), typeof(TimeSpan?), typeof(BoxVolumeRenderableSeries), new PropertyMetadata((object)TimeSpan.FromMinutes(15.0), new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface), new CoerceValueCallback(BoxVolumeRenderableSeries.CoerceHigherTimeframe)));

    public static readonly DependencyProperty Timeframe2ColorProperty = DependencyProperty.Register(nameof(Timeframe2Color), typeof(Color), typeof(BoxVolumeRenderableSeries), new PropertyMetadata((object)Color.FromRgb((byte)36, (byte)36, (byte)36), new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

    public static readonly DependencyProperty Timeframe2FrameColorProperty = DependencyProperty.Register(nameof(Timeframe2FrameColor), typeof(Color), typeof(BoxVolumeRenderableSeries), new PropertyMetadata((object)Color.FromRgb(byte.MaxValue, (byte)102, (byte)0), new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

    public static readonly DependencyProperty Timeframe3ColorProperty = DependencyProperty.Register(nameof(Timeframe3Color), typeof(Color), typeof(BoxVolumeRenderableSeries), new PropertyMetadata((object)Color.FromRgb((byte)0, (byte)55, (byte)24), new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

    public static readonly DependencyProperty CellFontColorProperty = DependencyProperty.Register(nameof(CellFontColor), typeof(Color), typeof(BoxVolumeRenderableSeries), new PropertyMetadata((object)Color.FromRgb((byte)90, (byte)90, (byte)90), new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

    public static readonly DependencyProperty HighVolColorProperty = DependencyProperty.Register(nameof(HighVolColor), typeof(Color), typeof(BoxVolumeRenderableSeries), new PropertyMetadata((object)Colors.LawnGreen, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

    public static readonly DependencyProperty HighVolBackgroundProperty = DependencyProperty.Register(nameof(HighVolBackground), typeof(Color), typeof(BoxVolumeRenderableSeries), new PropertyMetadata((object)Colors.LightBlue, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

    public BoxVolumeRenderableSeries()
    {
        this.DefaultStyleKey = (object)typeof(BoxVolumeRenderableSeries);
    }

    public TimeSpan? Timeframe2
    {
        get
        {
            return (TimeSpan?)this.GetValue(BoxVolumeRenderableSeries.Timeframe2Property);
        }
        set
        {
            this.SetValue(BoxVolumeRenderableSeries.Timeframe2Property, (object)value);
        }
    }

    public Color Timeframe2Color
    {
        get
        {
            return (Color)this.GetValue(BoxVolumeRenderableSeries.Timeframe2ColorProperty);
        }
        set
        {
            this.SetValue(BoxVolumeRenderableSeries.Timeframe2ColorProperty, (object)value);
        }
    }

    public Color Timeframe2FrameColor
    {
        get
        {
            return (Color)this.GetValue(BoxVolumeRenderableSeries.Timeframe2FrameColorProperty);
        }
        set
        {
            this.SetValue(BoxVolumeRenderableSeries.Timeframe2FrameColorProperty, (object)value);
        }
    }

    public TimeSpan? Timeframe3
    {
        get
        {
            return (TimeSpan?)this.GetValue(BoxVolumeRenderableSeries.Timeframe3Property);
        }
        set
        {
            this.SetValue(BoxVolumeRenderableSeries.Timeframe3Property, (object)value);
        }
    }

    public Color Timeframe3Color
    {
        get
        {
            return (Color)this.GetValue(BoxVolumeRenderableSeries.Timeframe3ColorProperty);
        }
        set
        {
            this.SetValue(BoxVolumeRenderableSeries.Timeframe3ColorProperty, (object)value);
        }
    }

    public Color CellFontColor
    {
        get
        {
            return (Color)this.GetValue(BoxVolumeRenderableSeries.CellFontColorProperty);
        }
        set
        {
            this.SetValue(BoxVolumeRenderableSeries.CellFontColorProperty, (object)value);
        }
    }

    public Color HighVolColor
    {
        get
        {
            return (Color)this.GetValue(BoxVolumeRenderableSeries.HighVolColorProperty);
        }
        set
        {
            this.SetValue(BoxVolumeRenderableSeries.HighVolColorProperty, (object)value);
        }
    }

    public Color HighVolBackground
    {
        get
        {
            return (Color)this.GetValue(BoxVolumeRenderableSeries.HighVolBackgroundProperty);
        }
        set
        {
            this.SetValue(BoxVolumeRenderableSeries.HighVolBackgroundProperty, (object)value);
        }
    }

    protected override void OnDataSeriesDependencyPropertyChanged()
    {
        base.OnDataSeriesDependencyPropertyChanged();
        this.CoerceValue(BoxVolumeRenderableSeries.Timeframe2Property);
        this.CoerceValue(BoxVolumeRenderableSeries.Timeframe3Property);
    }

    private static object CoerceHigherTimeframe(DependencyObject d, object newVal)
    {
        BoxVolumeRenderableSeries box = (BoxVolumeRenderableSeries)d;
        TimeSpan? period = (TimeSpan?)newVal;
        if ( !period.HasValue )
            return newVal;

        if ( !box.Timeframe.HasValue )
            return null;

        if ( !( period.Value < box.Timeframe.Value ) )
        {
            if ( period.Value.Ticks % box.Timeframe.Value.Ticks == 0L )
                return newVal;
        }

        return (object)box.Timeframe;
    }

    public IndexRange GetExtendedXRange(IndexRange range)
    {
        TimeSpan? tf = this.Timeframe;
        if ( !tf.HasValue )
            return range;

        long tf2Ticks = Timeframe2.HasValue ? Timeframe2.GetValueOrDefault().Ticks : 0L;
        long tf3Ticks = Timeframe3.HasValue ? Timeframe3.GetValueOrDefault().Ticks : 0L;
        long maxTicks = Math.Max(tf2Ticks, tf3Ticks);
        int multiplier = (int)( maxTicks / tf.Value.Ticks );

        return new IndexRange(range.Min - multiplier + 1, range.Max + multiplier - 1);
    }

    protected override void OnDrawImpl(IRenderContext2D rc, IRenderPassData data)
    {
        
        if ( !( this.DataSeries is TimeframeSegmentDataSeries ) )
            return;
        var xCalc = this.CurrentRenderPassData.XCoordinateCalculator;
        var yCalc = this.CurrentRenderPassData.YCoordinateCalculator;
        //TimeSpan? nullable1 = this.Timeframe;
        //TimeSpan? nullable2 = nullable1;
        //TimeSpan zero1 = TimeSpan.Zero;
        //TimeSpan? nullable3 = ( nullable2.HasValue ? ( nullable2.GetValueOrDefault() > zero1 ? 1 : 0 ) : 0 ) != 0 ? this.Timeframe2 : new TimeSpan?();
        //TimeSpan? nullable4 = nullable1;
        //TimeSpan zero2 = TimeSpan.Zero;
        //TimeSpan? nullable5;
        //if ( ( nullable4.HasValue ? ( nullable4.GetValueOrDefault() > zero2 ? 1 : 0 ) : 0 ) == 0 )
        //{
        //    nullable4 = new TimeSpan?();
        //    nullable5 = nullable4;
        //}
        //else
        //    nullable5 = this.Timeframe3;
        //TimeSpan? nullable6 = nullable5;

        TimeSpan? tfHasValue = null;
        TimeSpan? tfNoValue = null;

        if ( Timeframe.HasValue && ( Timeframe.GetValueOrDefault() > TimeSpan.Zero ) )
        {
            tfHasValue = Timeframe2;
        }

        if ( !Timeframe.HasValue || ( Timeframe.GetValueOrDefault() == TimeSpan.Zero ) )
        {
            tfNoValue = Timeframe3;
        }

        var cellFontColor     = this.CellFontColor;
        var highVolColor      = this.HighVolColor;
        var highVolBackground = this.HighVolBackground;
        var height            = rc.ViewportSize.Height;
        var pointSeries       = (TimeframeSegmentPointSeries)this.CurrentRenderPassData.PointSeries;
        var segments          = pointSeries.Segments;
        var length            = segments.Length;
        var ps                = pointSeries.PriceStep;
        var visibleRange      = pointSeries.VisibleRange;

        if ( length == 0 )
            return;

        var timeframe  = this.Timeframe;
        var timeframe2 = this.Timeframe2;
        var timeframe3 = this.Timeframe3;


        if ( !Timeframe.HasValue       ||
              Timeframe2 < Timeframe   ||
              ( Timeframe3 < Timeframe || Timeframe2.Value.Ticks % Timeframe.Value.Ticks != 0 || Timeframe3.Value.Ticks % Timeframe.Value.Ticks != 0 )

            )
        {
            throw new InvalidOperationException(string.Format("invalid timeframes. tf1={0}, tf2={1}, tf3={2}", (object)timeframe, (object)timeframe2, (object)timeframe3));
        }

        SciChartDebugLogger.Instance.WriteLine("BoxVolume: started render {0} segments. Indexes: {1}-{2}, VisibleRange: {3}-{4}", (object)segments.Length, (object)segments[0].Segment.X, (object)segments[segments.Length - 1].Segment.X, (object)visibleRange.Min, (object)visibleRange.Max);

        var xUnit                      = Math.Abs(xCalc.GetCoordinate(1.0) - xCalc.GetCoordinate(0.0));
        var yUnit                      = Math.Abs(yCalc.GetCoordinate(segments[0].Segment.LowPrice) - yCalc.GetCoordinate(segments[0].Segment.LowPrice + ps));
        var yHalfUnit                  = yUnit / 2.0;
        var xHalfUnit                  = xUnit / 2.0;

        var maxDrawPrice               = yCalc.GetDataValue(-yUnit);
        var minDrawPrice               = yCalc.GetDataValue(height + yUnit);
        long? tfHasValueTicks          = tfHasValue?.Ticks;
        long? tfTicks                  = Timeframe?.Ticks;
        long? timeSpanTicks            = tfHasValueTicks.HasValue & tfTicks.HasValue ? new long?(tfHasValueTicks.GetValueOrDefault() / tfTicks.GetValueOrDefault()) : new long?();
        long HasValueTimeSpanTicksMin1 = timeSpanTicks ?? 1L;

        long? tfNoValueTicks           = tfNoValue?.Ticks;

        long NoValueTimeSpanTicksMin1  = ( timeSpanTicks.HasValue & tfTicks.HasValue ? new long?(timeSpanTicks.GetValueOrDefault() / tfTicks.GetValueOrDefault()) : new long?() ) ?? 1L;
        var maxTimeSpanTicks           = new List<TimeframeIndexedSegment>((int)Math.Max(HasValueTimeSpanTicksMin1, NoValueTimeSpanTicksMin1));

        if ( minDrawPrice > maxDrawPrice )
            throw new InvalidOperationException($"minDrawPrice({minDrawPrice}) > maxDrawPrice({maxDrawPrice})");

        using ( PenManager pm = new PenManager(rc, false, (float)this.StrokeThickness, this.Opacity) )
        {
            var tf3Pen   = pm.GetPen(this.Timeframe3Color);
            var tf2Pen   = pm.GetPen(this.Timeframe2Color);
            var tf2Brush = pm.GetBrush(Color.FromArgb((byte)( (uint)Timeframe2Color.A / 2U ), Timeframe2Color.R, Timeframe2Color.G, Timeframe2Color.B));
            var tf3Brush = pm.GetBrush(Color.FromArgb((byte)( (uint)Timeframe3Color.A / 2U ), Timeframe3Color.R, Timeframe3Color.G, Timeframe3Color.B));
            var tf2fPen  = pm.GetPen(this.Timeframe2FrameColor);
            var helper   = SeriesDrawingHelpersFactory.GetSeriesDrawingHelper(rc, this.CurrentRenderPassData);

            if ( tfNoValue.HasValue )
            {
                int tfsCount;
                for ( int index = 0; index < length; index = tfsCount + 1 )
                {
                    FillPeriodSegments(maxTimeSpanTicks, segments, index, tfNoValue.Value);
                    tfsCount = index + ( maxTimeSpanTicks.Count - 1 );
                    (double minPrice, double maxPrice) = TimeframeDataSegment.MinMax(maxTimeSpanTicks.Select(p => p.Segment));

                    var startX   = xCalc.GetCoordinate(maxTimeSpanTicks[0].X) - xHalfUnit;
                    var endXCoor = maxTimeSpanTicks[maxTimeSpanTicks.Count - 1].X;
                    var endX     = xCalc.GetCoordinate(endXCoor) + xHalfUnit;
                    var startY   = yCalc.GetCoordinate(maxPrice) - yHalfUnit;
                    var endY     = yCalc.GetCoordinate(minPrice) + yHalfUnit;
                    var yCount   = 1 + (int)Math.Round(( maxPrice - minPrice ) / ps);

                    DrawGrid(rc, (ISeriesDrawingHelperSS)helper, new Point(startX, startY), new Point(endX, endY), maxTimeSpanTicks.Count, yCount, tf3Pen, tf3Pen, tf3Brush);
                }
            }
            if ( tfHasValue.HasValue )
            {
                int tfsCount;
                for ( int index = 0; index < length; index = tfsCount + 1 )
                {
                    FillPeriodSegments(maxTimeSpanTicks, segments, index, tfHasValue.Value);
                    
                    tfsCount = index + ( maxTimeSpanTicks.Count - 1 );
                    
                    (double minPrice, double maxPrice) = TimeframeDataSegment.MinMax(maxTimeSpanTicks.Select<TimeframeIndexedSegment, TimeframeDataSegment>(p => p.Segment));

                    var startX   = xCalc.GetCoordinate(maxTimeSpanTicks[0].X) - xHalfUnit;
                    var endXCoor = maxTimeSpanTicks[maxTimeSpanTicks.Count - 1].X;
                    var endX     = xCalc.GetCoordinate(endXCoor) + xHalfUnit;
                    var startY   = yCalc.GetCoordinate(maxPrice) - yHalfUnit;
                    var endY     = yCalc.GetCoordinate(minPrice) + yHalfUnit;
                    var yCount   = 1 + (int)Math.Round(( maxPrice - minPrice ) / ps);

                    DrawGrid(rc, (ISeriesDrawingHelperSS)helper, new Point(startX, startY), new Point(endX, endY), maxTimeSpanTicks.Count, yCount, tf3Pen, tf3Pen, tf3Brush);
                }
            }

            var size             = new Size(xUnit, yUnit);
            var fontColorBrush   = pm.GetBrush(cellFontColor);
            pm.GetBrush(highVolColor);
            var volBgBrush       = pm.GetBrush(highVolBackground);
            var upColorPen       = pm.GetPen(this.UpColor, new float?((float)( this.StrokeThickness + 2 )));
            var downColorPen     = pm.GetPen(this.DownColor, new float?((float)( this.StrokeThickness + 2 )));
            var lastPrice        = new double?();
            var lastYPriceCenter = 0.0;

            if ( xUnit >= 2.0 && yUnit >= 2.0 )
            {
                for ( int index = 0; index<length; ++index )
                {
                    var indexSegment = segments[index];
                    var xCoorCenter = xCalc.GetCoordinate(indexSegment.X) - xHalfUnit;
                    (KeyValuePair<double, CandlePriceLevel>[] priceLevels, Decimal myVolume) = indexSegment.Segment.GetCPLVFromPriceStep(ps);
                    var hasValidFont     = this._fontCalculator.HasFont(size, myVolume.IncreaseEffectiveScale());
                    
                    foreach ( KeyValuePair<double, CandlePriceLevel> keyValuePair in priceLevels.Where(p => p.Key > minDrawPrice && p.Key< maxDrawPrice) )
                    {
                        var price        = keyValuePair.Key;
                        var yPriceCenter = yCalc.GetCoordinate(keyValuePair.Key) - yHalfUnit;
                        price            = lastPrice.GetValueOrDefault();

                        if ( !lastPrice.HasValue )
                        {
                            price        = yPriceCenter;
                            lastPrice    = new double?(yPriceCenter);
                        }

                        lastYPriceCenter = yPriceCenter;

                        var begin        = new Rect(xCoorCenter, yPriceCenter, xUnit, yUnit);
                        var end          = new Rect(xCoorCenter + 1.0, yPriceCenter + 1.0, xUnit - 2.0, yUnit - 2.0);
                        var totalVolume  = ( (CandlePriceLevel)keyValuePair.Value ).TotalVolume;

                        if ( totalVolume > 0M )
                        {
                            bool isSameVolume = totalVolume == myVolume;

                            if ( hasValidFont )
                            {
                                if ( isSameVolume )
                                    rc.FillRectangle(volBgBrush, end.TopLeft, end.BottomRight, 0.0);

                                string volString = totalVolume.ToString();
                                (float, FontWeight, bool) tuple = this._fontCalculator.GetFont(size, totalVolume.IncreaseEffectiveScale(), 0.0f);
                                
                                var fontSize   = tuple.Item1;
                                var fontWeight = tuple.Item2;
                                
                                if ( tuple.Item3 )
                                {
                                    Color drawColor = isSameVolume ? highVolColor : cellFontColor;

                                    var myff = new System.Windows.Media.FontFamily(_fontCalculator.FontFamily);
                                    
                                    rc.DrawText(begin, drawColor, fontSize, volString, myff, fontWeight, FontStyles.Normal);
                                }
                            }
                            else
                                rc.FillRectangle(isSameVolume ? volBgBrush : fontColorBrush, end.TopLeft, end.BottomRight, 0.0);
                        }
                    }

                    if ( lastPrice.HasValue && indexSegment.Segment.OpenPrice.HasValue )
                    {
                        double? firstPrice = indexSegment.Segment.OpenPrice;
                        
                        IPen2D drawPen = firstPrice.GetValueOrDefault() <= indexSegment.Segment.ClosePrice & firstPrice.HasValue ? upColorPen : downColorPen;
                        rc.DrawLine(drawPen, new Point(xCoorCenter, lastPrice.Value), new Point(xCoorCenter, lastYPriceCenter + yUnit));
                    }
                }
            }
            else
            {
                if ( tfHasValue.HasValue || tfNoValue.HasValue )
                    return;

                for ( int index = 0; index < length; ++index )
                {
                    TimeframeIndexedSegment indexSegment = segments[index];
                    double num29 = xCalc.GetCoordinate(indexSegment.X) - xHalfUnit;

                    if ( indexSegment.Segment.LowPrice <= maxDrawPrice && indexSegment.Segment.HighPrice >= minDrawPrice )
                    {
                        double minPrice = xCalc.GetCoordinate(indexSegment.Segment.LowPrice);
                        double maxPrice = yCalc.GetCoordinate(indexSegment.Segment.HighPrice);
                        Rect rect = new Rect(num29, maxPrice, Math.Max(xUnit, 1.0), Math.Max(Math.Abs(maxPrice - minPrice), 1.0));
                        rc.FillRectangle(fontColorBrush, rect.TopLeft, rect.BottomRight, 0.0);
                    }
                }
            }
        }
    }   
}
