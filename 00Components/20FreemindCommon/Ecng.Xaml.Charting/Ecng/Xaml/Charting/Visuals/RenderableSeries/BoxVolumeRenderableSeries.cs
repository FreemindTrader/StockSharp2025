// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Visuals.RenderableSeries.BoxVolumeRenderableSeries
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Ecng.Xaml.Charting.Licensing;
using Ecng.Xaml.Charting.Model.DataSeries.SegmentDataSeries;
using Ecng.Xaml.Charting.Numerics.CoordinateCalculators;
using Ecng.Xaml.Charting.Rendering.Common;
using Ecng.Xaml.Charting.Utility;
using StockSharp.Xaml.Licensing.Core;

namespace Ecng.Xaml.Charting.Visuals.RenderableSeries
{
    [UltrachartLicenseProvider( typeof( RenderableSeriesUltrachartLicenseProvider ) )]
    public class BoxVolumeRenderableSeries : TimeframeSegmentRenderableSeries
    {
        public static readonly DependencyProperty Timeframe2Property = DependencyProperty.Register(nameof (Timeframe2), typeof (int), typeof (BoxVolumeRenderableSeries), new PropertyMetadata((object) 5, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface), new CoerceValueCallback(BoxVolumeRenderableSeries.CoerceHigherTimeframe)));
        public static readonly DependencyProperty Timeframe3Property = DependencyProperty.Register(nameof (Timeframe3), typeof (int), typeof (BoxVolumeRenderableSeries), new PropertyMetadata((object) 15, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface), new CoerceValueCallback(BoxVolumeRenderableSeries.CoerceHigherTimeframe)));
        public static readonly DependencyProperty Timeframe2ColorProperty = DependencyProperty.Register(nameof (Timeframe2Color), typeof (Color), typeof (BoxVolumeRenderableSeries), new PropertyMetadata((object) Color.FromRgb((byte) 36, (byte) 36, (byte) 36), new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty Timeframe2FrameColorProperty = DependencyProperty.Register(nameof (Timeframe2FrameColor), typeof (Color), typeof (BoxVolumeRenderableSeries), new PropertyMetadata((object) Color.FromRgb(byte.MaxValue, (byte) 102, (byte) 0), new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty Timeframe3ColorProperty = DependencyProperty.Register(nameof (Timeframe3Color), typeof (Color), typeof (BoxVolumeRenderableSeries), new PropertyMetadata((object) Color.FromRgb((byte) 0, (byte) 55, (byte) 24), new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty CellFontColorProperty = DependencyProperty.Register(nameof (CellFontColor), typeof (Color), typeof (BoxVolumeRenderableSeries), new PropertyMetadata((object) Color.FromRgb((byte) 90, (byte) 90, (byte) 90), new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty HighVolColorProperty = DependencyProperty.Register(nameof (HighVolColor), typeof (Color), typeof (BoxVolumeRenderableSeries), new PropertyMetadata((object) Colors.LawnGreen, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

        public int Timeframe2
        {
            get
            {
                return ( int ) this.GetValue( BoxVolumeRenderableSeries.Timeframe2Property );
            }
            set
            {
                this.SetValue( BoxVolumeRenderableSeries.Timeframe2Property, ( object ) value );
            }
        }

        public Color Timeframe2Color
        {
            get
            {
                return ( Color ) this.GetValue( BoxVolumeRenderableSeries.Timeframe2ColorProperty );
            }
            set
            {
                this.SetValue( BoxVolumeRenderableSeries.Timeframe2ColorProperty, ( object ) value );
            }
        }

        public Color Timeframe2FrameColor
        {
            get
            {
                return ( Color ) this.GetValue( BoxVolumeRenderableSeries.Timeframe2FrameColorProperty );
            }
            set
            {
                this.SetValue( BoxVolumeRenderableSeries.Timeframe2FrameColorProperty, ( object ) value );
            }
        }

        public int Timeframe3
        {
            get
            {
                return ( int ) this.GetValue( BoxVolumeRenderableSeries.Timeframe3Property );
            }
            set
            {
                this.SetValue( BoxVolumeRenderableSeries.Timeframe3Property, ( object ) value );
            }
        }

        public Color Timeframe3Color
        {
            get
            {
                return ( Color ) this.GetValue( BoxVolumeRenderableSeries.Timeframe3ColorProperty );
            }
            set
            {
                this.SetValue( BoxVolumeRenderableSeries.Timeframe3ColorProperty, ( object ) value );
            }
        }

        public Color CellFontColor
        {
            get
            {
                return ( Color ) this.GetValue( BoxVolumeRenderableSeries.CellFontColorProperty );
            }
            set
            {
                this.SetValue( BoxVolumeRenderableSeries.CellFontColorProperty, ( object ) value );
            }
        }

        public Color HighVolColor
        {
            get
            {
                return ( Color ) this.GetValue( BoxVolumeRenderableSeries.HighVolColorProperty );
            }
            set
            {
                this.SetValue( BoxVolumeRenderableSeries.HighVolColorProperty, ( object ) value );
            }
        }

        public BoxVolumeRenderableSeries()
        {
            this.DefaultStyleKey = ( object ) typeof( BoxVolumeRenderableSeries );
        }

        protected override void OnDataSeriesDependencyPropertyChanged()
        {
            base.OnDataSeriesDependencyPropertyChanged();
            this.CoerceValue( BoxVolumeRenderableSeries.Timeframe2Property );
            this.CoerceValue( BoxVolumeRenderableSeries.Timeframe3Property );
        }

        private static object CoerceHigherTimeframe( DependencyObject d, object newVal )
        {
            BoxVolumeRenderableSeries renderableSeries = (BoxVolumeRenderableSeries) d;
            int num = (int) newVal;
            if ( num < renderableSeries.Timeframe || num % renderableSeries.Timeframe != 0 )
                return ( object ) renderableSeries.Timeframe;
            return newVal;
        }

        public override IndexRange GetExtendedXRange( IndexRange range )
        {
            int timeframe = this.Timeframe;
            if ( timeframe < 1 )
                return range;
            int num = Math.Max(this.Timeframe2, this.Timeframe3) / timeframe;
            return new IndexRange( range.Min - num + 1, range.Max + num - 1 );
        }

        protected override void OnDrawImpl( IRenderContext2D renderContext, IRenderPassData renderPassData )
        {
            if ( !( this.DataSeries is TimeframeSegmentDataSeries ) )
                return;
            ICoordinateCalculator<double> xcoordinateCalculator = this.CurrentRenderPassData.XCoordinateCalculator;
            ICoordinateCalculator<double> ycoordinateCalculator = this.CurrentRenderPassData.YCoordinateCalculator;
            int timeframe = this.Timeframe;
            int timeframe2 = this.Timeframe2;
            int timeframe3 = this.Timeframe3;
            Color cellFontColor = this.CellFontColor;
            Color highVolColor = this.HighVolColor;
            double height1 = renderContext.ViewportSize.Height;
            TimeframeSegmentPointSeries pointSeries = (TimeframeSegmentPointSeries) this.CurrentRenderPassData.PointSeries;
            TimeframeSegmentWrapper[] segments = pointSeries.Segments;
            int length = segments.Length;
            double priceStep = pointSeries.PriceStep;
            IndexRange visibleRange = pointSeries.VisibleRange;
            if ( timeframe < 1 || timeframe2 < timeframe || ( timeframe3 < timeframe || timeframe2 % timeframe != 0 ) || timeframe3 % timeframe != 0 )
                throw new InvalidOperationException( string.Format( "invalid timeframes. tf1={0}, tf2={1}, tf3={2}", ( object ) timeframe, ( object ) timeframe2, ( object ) timeframe3 ) );
            UltrachartDebugLogger.Instance.WriteLine( "BoxVolume: started render {0} segments. Indexes: {1}-{2}, VisibleRange: {3}-{4}", ( object ) segments.Length, ( object ) segments[ 0 ].Segment.Index, ( object ) segments[ segments.Length - 1 ].Segment.Index, ( object ) visibleRange.Min, ( object ) visibleRange.Max );
            double width = Math.Abs(xcoordinateCalculator.GetCoordinate(1.0) - xcoordinateCalculator.GetCoordinate(0.0));
            double height2 = Math.Abs(ycoordinateCalculator.GetCoordinate(segments[0].Segment.MinPrice) - ycoordinateCalculator.GetCoordinate(segments[0].Segment.MinPrice + priceStep));
            double num1 = height2 / 2.0;
            double num2 = width / 2.0;
            double maxDrawPrice = ycoordinateCalculator.GetDataValue(-height2);
            double minDrawPrice = ycoordinateCalculator.GetDataValue(height1 + height2);
            List<TimeframeSegmentWrapper> timeframeSegmentWrapperList = new List<TimeframeSegmentWrapper>(Math.Max(timeframe2, timeframe3));
            if ( minDrawPrice > maxDrawPrice )
                throw new InvalidOperationException( string.Format( "minDrawPrice({0}) > maxDrawPrice({1})", ( object ) minDrawPrice, ( object ) maxDrawPrice ) );
            using ( PenManager penManager = new PenManager( renderContext, false, ( float ) this.StrokeThickness, this.Opacity, ( double[ ] ) null ) )
            {
                IPen2D pen1 = penManager.GetPen(this.Timeframe3Color);
                IPen2D pen2 = penManager.GetPen(this.Timeframe2Color);
                Color color = this.Timeframe2Color;
                IBrush2D brush1 = penManager.GetBrush(Color.FromArgb((byte) ((uint) color.A / 2U), color.R, color.G, color.B));
                color = this.Timeframe3Color;
                IBrush2D brush2 = penManager.GetBrush(Color.FromArgb((byte) ((uint) color.A / 2U), color.R, color.G, color.B));
                IPen2D pen3 = penManager.GetPen(this.Timeframe2FrameColor);
                ISeriesDrawingHelper seriesDrawingHelper = SeriesDrawingHelpersFactory.GetSeriesDrawingHelper(renderContext, this.CurrentRenderPassData);
                int tf1 = timeframe3;
                int num3;
                for ( int index = 0 ; index < length ; index = num3 + 1 )
                {
                    this.FillPeriodSegments( timeframeSegmentWrapperList, segments, index, tf1 );
                    num3 = index + ( timeframeSegmentWrapperList.Count - 1 );
                    double minPrice;
                    double maxPrice;
                    int numCellsY;
                    TimeframeDataSegment.MinMax( timeframeSegmentWrapperList.Select<TimeframeSegmentWrapper, TimeframeDataSegment>( ( Func<TimeframeSegmentWrapper, TimeframeDataSegment> ) ( w => w.Segment ) ), out minPrice, out maxPrice, out numCellsY );
                    double x1 = xcoordinateCalculator.GetCoordinate(timeframeSegmentWrapperList[0].X) - num2;
                    double x2 = xcoordinateCalculator.GetCoordinate(timeframeSegmentWrapperList[timeframeSegmentWrapperList.Count - 1].X) + num2;
                    double y1 = ycoordinateCalculator.GetCoordinate(maxPrice) - num1;
                    double y2 = ycoordinateCalculator.GetCoordinate(minPrice) + num1;
                    this.DrawGrid( renderContext, seriesDrawingHelper, new Point( x1, y1 ), new Point( x2, y2 ), timeframeSegmentWrapperList.Count, numCellsY, pen1, pen1, brush2 );
                }
                int tf2 = timeframe2;
                int num4;
                for ( int index = 0 ; index < length ; index = num4 + 1 )
                {
                    this.FillPeriodSegments( timeframeSegmentWrapperList, segments, index, tf2 );
                    num4 = index + ( timeframeSegmentWrapperList.Count - 1 );
                    double minPrice;
                    double maxPrice;
                    int numCellsY;
                    TimeframeDataSegment.MinMax( timeframeSegmentWrapperList.Select<TimeframeSegmentWrapper, TimeframeDataSegment>( ( Func<TimeframeSegmentWrapper, TimeframeDataSegment> ) ( w => w.Segment ) ), out minPrice, out maxPrice, out numCellsY );
                    double x1 = xcoordinateCalculator.GetCoordinate(timeframeSegmentWrapperList[0].X) - num2;
                    double x2 = xcoordinateCalculator.GetCoordinate(timeframeSegmentWrapperList[timeframeSegmentWrapperList.Count - 1].X) + num2;
                    double y1 = ycoordinateCalculator.GetCoordinate(maxPrice) - num1;
                    double y2 = ycoordinateCalculator.GetCoordinate(minPrice) + num1;
                    this.DrawGrid( renderContext, seriesDrawingHelper, new Point( x1, y1 ), new Point( x2, y2 ), timeframeSegmentWrapperList.Count, numCellsY, pen3, pen2, brush1 );
                }
                int numSymbols = ((IEnumerable<TimeframeSegmentWrapper>) segments).Max<TimeframeSegmentWrapper>((Func<TimeframeSegmentWrapper, int>) (s => s.Segment.MaxDigits));
                Size area = new Size(width, height2);
                bool flag = this._fontCalculator.CanDrawText(area, numSymbols);
                IBrush2D brush3 = penManager.GetBrush(cellFontColor);
                IBrush2D brush4 = penManager.GetBrush(highVolColor);
                if ( width < 2.0 || height2 < 2.0 )
                    return;
                for ( int index = 0 ; index < length ; ++index )
                {
                    TimeframeSegmentWrapper timeframeSegmentWrapper = segments[index];
                    double x = xcoordinateCalculator.GetCoordinate(timeframeSegmentWrapper.X) - num2;
                    foreach ( TimeframeDataSegment.PriceLevel priceLevel in timeframeSegmentWrapper.Segment.Values.Where<TimeframeDataSegment.PriceLevel>( ( Func<TimeframeDataSegment.PriceLevel, bool> ) ( v =>
                    {
                        if ( v != null && v.Price > minDrawPrice )
                            return v.Price < maxDrawPrice;
                        return false;
                    } ) ) )
                    {
                        double coordinate = ycoordinateCalculator.GetCoordinate(priceLevel.Price);
                        Rect dstBoundingRect = new Rect(x, coordinate - num1, width, height2);
                        Rect rect = new Rect(x + 1.0, coordinate - num1 + 1.0, width - 2.0, height2 - 2.0);
                        if ( priceLevel.Value > 0L )
                        {
                            if ( flag )
                            {
                                string text = priceLevel.Value.ToString((IFormatProvider) CultureInfo.InvariantCulture);
                                Tuple<float, FontWeight, bool> font = this._fontCalculator.GetFont(area, priceLevel.Digits, 0.0f);
                                Color foreColor = priceLevel.Value == timeframeSegmentWrapper.Segment.MaxValue ? highVolColor : cellFontColor;
                                renderContext.DrawText( text, dstBoundingRect, AlignmentX.Center, AlignmentY.Center, foreColor, font.Item1, this._fontCalculator.FontFamily, font.Item2 );
                            }
                            else
                                renderContext.FillRectangle( priceLevel.Value == timeframeSegmentWrapper.Segment.MaxValue ? brush4 : brush3, rect.TopLeft, rect.BottomRight, 0.0 );
                        }
                    }
                }
            }
        }
    }
}
