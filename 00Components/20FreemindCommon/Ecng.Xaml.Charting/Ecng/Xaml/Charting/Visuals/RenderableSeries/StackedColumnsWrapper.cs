// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.StackedColumnsWrapper
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\fx.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
namespace fx.Xaml.Charting
{
    internal class StackedColumnsWrapper : StackedSeriesWrapperBase<IStackedColumnRenderableSeries>, IStackedColumnsWrapper, IStackedSeriesWrapperBase<IStackedColumnRenderableSeries>
    {
        public double DataPointWidth
        {
            get; set;
        }

        public IRange GetXRange( bool isLogarithmicAxis )
        {
            DoubleRange doubleRange = GetCommonXRange().AsDoubleRange();
            if ( !isLogarithmicAxis )
            {
                int count = SeriesCollection[0].DataSeries.Count;
                double num = count > 1 ? doubleRange.Diff / (double) (count - 1) / 2.0 * GetDataPointWidthFraction() : GetDataPointWidthFraction() / 2.0;
                doubleRange.Max += num;
                doubleRange.Min -= num;
            }
            return ( IRange ) doubleRange;
        }

        private IRange GetCommonXRange()
        {
            IRange range = SeriesCollection[0].DataSeries.XRange;
            for ( int index = 1 ; index < SeriesCollection.Count ; ++index )
                range = range.Union( SeriesCollection[ index ].DataSeries.XRange );
            return RangeFactory.NewRange( range.Min, range.Max );
        }

        public double GetDataPointWidthFraction()
        {
            return SeriesCollection[ 0 ].DataPointWidth;
        }

        public override void DrawStackedSeries( IRenderContext2D renderContext )
        {
            if ( ++Counter != SeriesCollection.Count<IStackedColumnRenderableSeries>( ( Func<IStackedColumnRenderableSeries, bool> ) ( x => x.IsVisible ) ) )
                return;
            Counter = 0;
            List<IStackedColumnRenderableSeries> list = SeriesCollection.Where<IStackedColumnRenderableSeries>((Func<IStackedColumnRenderableSeries, bool>) (x => x.IsVisible)).ToList<IStackedColumnRenderableSeries>();
            if ( !list.Any<IStackedColumnRenderableSeries>() )
                return;
            IRenderPassData currentRenderPassData = list[0].CurrentRenderPassData;
            double pointWidthFraction = GetDataPointWidthFraction();
            double count = CalculateCount(currentRenderPassData.PointSeries.XValues);
            DataPointWidth = ( double ) list[ 0 ].GetDatapointWidth( currentRenderPassData.XCoordinateCalculator, currentRenderPassData.PointSeries, count, pointWidthFraction );
            foreach ( IStackedColumnRenderableSeries renderableSeries in list )
                DrawColumns( renderContext, ( StackedColumnRenderableSeries ) renderableSeries );
        }

        private double CalculateCount( IUltraList<double> xValues )
        {
            double num1 = double.MaxValue;
            for ( int index = 1 ; index < xValues.Count ; ++index )
            {
                double num2 = xValues[index] - xValues[index - 1];
                if ( num2 < num1 )
                    num1 = num2;
            }
            return ( xValues[ xValues.Count - 1 ] - xValues[ 0 ] + num1 ) / num1;
        }

        private void DrawColumns( IRenderContext2D renderContext, StackedColumnRenderableSeries series )
        {
            using ( PenManager penManager = new PenManager( renderContext, series.AntiAliasing, ( float ) series.StrokeThickness, series.Opacity, ( double[ ] ) null ) )
            {
                IRenderPassData currentRenderPassData = series.CurrentRenderPassData;
                string format = "{0:" + series.LabelTextFormatting + "}";
                bool flag = IsOneHundredPercentGroup(series.StackedGroupId);
                if ( flag )
                    format += "%";
                bool isVerticalChart = currentRenderPassData.IsVerticalChart;
                double chartRotationAngle = series.GetChartRotationAngle();
                double spacingInPixels;
                double width = Math.Max(CalculateColumnWidth(series.StackedGroupId, out spacingInPixels), 0.0);
                IBrush2D brush1 = renderContext.CreateBrush(series.FillBrush, series.Opacity, series.FillBrushMappingMode);
                IPen2D pen1 = penManager.GetPen(series.SeriesColor);
                ISeriesDrawingHelper seriesDrawingHelper = SeriesDrawingHelpersFactory.GetSeriesDrawingHelper(renderContext, currentRenderPassData);
                for ( int i = 0 ; i < currentRenderPassData.PointSeries.Count ; i++ )
                {
                    IPoint point1 = currentRenderPassData.PointSeries[i];
                    int groupIndex;
                    int groupCountAtX = GetGroupCountAtX((IStackedColumnRenderableSeries) series, point1.X, out groupIndex);
                    double shiftedColumnCenter = CalculateShiftedColumnCenter(currentRenderPassData.XCoordinateCalculator.GetCoordinate(point1.X), groupIndex, groupCountAtX, width, spacingInPixels);
                    Tuple<double, double> tuple = AccumulateYValueAtX((IStackedColumnRenderableSeries) series, i, true);
                    double coordinate1 = currentRenderPassData.YCoordinateCalculator.GetCoordinate(tuple.Item1);
                    double coordinate2 = currentRenderPassData.YCoordinateCalculator.GetCoordinate(tuple.Item2);
                    Point point2 = DrawingHelper.TransformPoint(new Point(shiftedColumnCenter - width / 2.0, coordinate1), isVerticalChart);
                    Point point3 = DrawingHelper.TransformPoint(new Point(shiftedColumnCenter + width / 2.0, coordinate2), isVerticalChart);
                    IPaletteProvider paletteProvider = series.PaletteProvider;
                    if ( paletteProvider != null )
                    {
                        Color? color = paletteProvider.GetColor((IRenderableSeries) series, point1.X, point1.Y);
                        if ( color.HasValue )
                        {
                            using ( IPen2D pen2 = penManager.GetPen( color.Value ) )
                            {
                                using ( IBrush2D brush2 = renderContext.CreateBrush( color.Value, 1.0, new bool?() ) )
                                    DrawColumn( seriesDrawingHelper, point2, point3, pen2, brush2, chartRotationAngle );
                            }
                        }
                    }
                    else
                        DrawColumn( seriesDrawingHelper, point2, point3, pen1, brush1, chartRotationAngle );
                    if ( width > 0.0 && series.ShowLabel )
                    {
                        double y = point1.Y;
                        if ( flag )
                        {
                            IRange yrangeAtX = GetYRangeAtX((IStackedColumnRenderableSeries) series, (Func<IStackedColumnRenderableSeries, double>) (s => s.CurrentRenderPassData.PointSeries.YValues[i]));
                            y /= ( double ) yrangeAtX.Diff * 100.0;
                        }
                        string text = string.Format(format, (object) y);
                        renderContext.DrawText( text, new Rect( point2, point3 ), AlignmentX.Center, AlignmentY.Center, series.LabelColor, series.LabelFontSize, ( string ) null, new FontWeight() );
                    }
                }
            }
        }

        private void DrawColumn( ISeriesDrawingHelper drawingHelper, Point leftUpper, Point rightBottom, IPen2D stroke, IBrush2D fill, double gradientRotationAngle )
        {
            if ( leftUpper.X.CompareTo( rightBottom.X ) == 0 )
                drawingHelper.DrawLine( leftUpper, rightBottom, stroke );
            else
                drawingHelper.DrawBox( leftUpper, rightBottom, fill, stroke, gradientRotationAngle );
        }

        private double CalculateColumnWidth( string stackedGroupId, out double spacingInPixels )
        {
            int visibleGroupsCount = GetVisibleGroupsCount();
            double spacing = GetSpacing(stackedGroupId);
            double num;
            if ( GetSpacingMode( stackedGroupId ) == SpacingMode.Absolute )
            {
                spacingInPixels = spacing;
                num = ( DataPointWidth - spacing * ( double ) ( visibleGroupsCount - 1 ) ) / ( double ) visibleGroupsCount;
            }
            else
            {
                num = DataPointWidth / ( ( double ) visibleGroupsCount + spacing * ( double ) visibleGroupsCount - spacing );
                spacingInPixels = num * spacing;
            }
            return num;
        }

        private int GetVisibleGroupsCount()
        {
            return SeriesGroups.Count<Tuple<string, List<IStackedColumnRenderableSeries>>>( ( Func<Tuple<string, List<IStackedColumnRenderableSeries>>, bool> ) ( group => group.Item2.Any<IStackedColumnRenderableSeries>( ( Func<IStackedColumnRenderableSeries, bool> ) ( x => x.IsVisible ) ) ) );
        }

        private double CalculateShiftedColumnCenter( double xValue, int groupIndex, int count, double width, double spacing )
        {
            return xValue - width * ( double ) count / 2.0 - spacing * ( double ) ( count - 1 ) / 2.0 + ( double ) groupIndex * ( spacing + width ) + 0.5 * width;
        }

        public Tuple<double, double> GetSeriesVerticalBounds( IStackedColumnRenderableSeries series, int indexInDataSeries )
        {
            Tuple<double, double> tuple = AccumulateYValueAtX(series, indexInDataSeries, false);
            return new Tuple<double, double>( tuple.Item2, tuple.Item1 );
        }

        public double GetSeriesBodyWidth( IStackedColumnRenderableSeries series, int dataSeriesIndex )
        {
            double spacingInPixels;
            return CalculateColumnWidth( series.StackedGroupId, out spacingInPixels );
        }

        internal int GetGroupCountAtX( IStackedColumnRenderableSeries series, double xValue, out int groupIndex )
        {
            groupIndex = -1;
            int num = 0;
            foreach ( Tuple<string, List<IStackedColumnRenderableSeries>> seriesGroup in SeriesGroups )
            {
                List<IStackedColumnRenderableSeries> list = seriesGroup.Item2.Where<IStackedColumnRenderableSeries>((Func<IStackedColumnRenderableSeries, bool>) (x => x.IsVisible)).ToList<IStackedColumnRenderableSeries>();
                foreach ( IRenderableSeries renderableSeries in list )
                {
                    IPointSeries pointSeries = renderableSeries.CurrentRenderPassData.PointSeries;
                    if ( pointSeries != null )
                    {
                        int index = pointSeries.XValues.FindIndex<double>(true, (IComparable) xValue, SearchMode.Exact);
                        if ( index != -1 && !NumberUtil.IsNaN( pointSeries.YValues[ index ] ) )
                        {
                            ++num;
                            break;
                        }
                    }
                }
                if ( list.Contains( series ) )
                    groupIndex = num - 1;
            }
            return num;
        }

        public SpacingMode GetSpacingMode( string groupId )
        {
            return GetStackedSeriesFromSameGroup( groupId )[ 0 ].SpacingMode;
        }

        public double GetSpacing( string groupId )
        {
            return GetStackedSeriesFromSameGroup( groupId )[ 0 ].Spacing;
        }

        public override HitTestInfo ShiftHitTestInfo( Point rawPoint, HitTestInfo nearestHitResult, double hitTestRadius, IStackedColumnRenderableSeries series )
        {
            nearestHitResult = base.ShiftHitTestInfo( rawPoint, nearestHitResult, hitTestRadius, series );
            if ( !nearestHitResult.IsEmpty() )
            {
                bool isVerticalChart = series.CurrentRenderPassData.IsVerticalChart;
                double spacingInPixels;
                double columnWidth = CalculateColumnWidth(series.StackedGroupId, out spacingInPixels);
                int groupIndex;
                int groupCountAtX = GetGroupCountAtX(series, nearestHitResult.XValue.ToDouble(), out groupIndex);
                Point point1 = DrawingHelper.TransformPoint(nearestHitResult.HitTestPoint, isVerticalChart);
                double y = point1.Y;
                double shiftedColumnCenter = CalculateShiftedColumnCenter(point1.X, groupIndex, groupCountAtX, columnWidth, spacingInPixels);
                Point point2 = new Point(shiftedColumnCenter, y);
                nearestHitResult.HitTestPoint = DrawingHelper.TransformPoint( point2, isVerticalChart );
                nearestHitResult.IsHit = PointUtil.Distance( point2, rawPoint ) < hitTestRadius;
                double num = isVerticalChart ? Math.Abs(shiftedColumnCenter - rawPoint.Y) : Math.Abs(shiftedColumnCenter - rawPoint.X);
                nearestHitResult.IsWithinDataBounds = nearestHitResult.IsVerticalHit = num < columnWidth / 2.0;
            }
            return nearestHitResult;
        }
    }
}
