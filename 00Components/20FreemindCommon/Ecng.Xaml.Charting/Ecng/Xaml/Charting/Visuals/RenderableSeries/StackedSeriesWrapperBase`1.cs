// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.RenderableSeries.StackedSeriesWrapperBase`1
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Model.DataSeries;
using StockSharp.Xaml.Charting.Rendering.Common;
using StockSharp.Xaml.Charting.Utility;

namespace StockSharp.Xaml.Charting.Visuals.RenderableSeries
{
    internal abstract class StackedSeriesWrapperBase<T> : IStackedSeriesWrapperBase<T> where T : IStackedRenderableSeries
    {
        protected readonly List<Tuple<string, List<T>>> SeriesGroups;
        protected readonly List<T> SeriesCollection;
        protected int Counter;

        protected StackedSeriesWrapperBase()
        {
            this.SeriesCollection = new List<T>();
            this.SeriesGroups = new List<Tuple<string, List<T>>>();
        }

        public int GetStackedSeriesCount()
        {
            return this.SeriesCollection.Count;
        }

        public DoubleRange CalculateYRange( T series, IndexRange xIndexRange )
        {
            double zeroLineY1 = series.ZeroLineY;
            double zeroLineY2 = series.ZeroLineY;
            int num1 = Math.Max(xIndexRange.Min, 0);
            int num2 = Math.Min(xIndexRange.Max, series.DataSeries.XValues.Count - 1);
            for ( int index = num1 ; index <= num2 ; ++index )
            {
                Tuple<double, double> tuple = this.AccumulateYValueAtX(series, index, false);
                if ( tuple.Item1 > 0.0 && tuple.Item1 > zeroLineY1 )
                    zeroLineY1 = tuple.Item1;
                if ( tuple.Item1 < 0.0 && tuple.Item1 < zeroLineY2 )
                    zeroLineY2 = tuple.Item1;
            }
            return RangeFactory.NewRange( ( IComparable ) zeroLineY2, ( IComparable ) zeroLineY1 ).AsDoubleRange();
        }

        public Tuple<double, double> AccumulateYValueAtX( T series, int index, bool isResampledSeries = false )
        {
            double num1 = series.ZeroLineY;
            double num2 = series.ZeroLineY;
            double num3 = 0.0;
            double num4 = 0.0;
            IList<T> seriesFromSameGroup = this.GetStackedSeriesFromSameGroup(series.StackedGroupId);
            bool flag = this.IsOneHundredPercentGroup(series.StackedGroupId);
            foreach ( T obj in seriesFromSameGroup.Where<T>( ( Func<T, bool> ) ( x => x.IsVisible ) ) )
            {
                double num5 = isResampledSeries ? obj.CurrentRenderPassData.PointSeries.YValues[index].ToDouble() : ((IComparable) obj.DataSeries.YValues[index]).ToDouble();
                if ( !NumberUtil.IsNaN( num5 ) )
                {
                    if ( num5 >= 0.0 )
                        num3 += num5;
                    else if ( num5 < 0.0 )
                        num4 += num5;
                    if ( ( object ) obj == ( object ) series )
                    {
                        num1 = num5 >= 0.0 ? num3 : num4;
                        num1 += series.ZeroLineY;
                        num2 = num1 - num5;
                        if ( !flag )
                            break;
                    }
                }
            }
            if ( flag )
            {
                double num5 = num3 - num4;
                num1 = num1 * 100.0 / num5;
                num2 = num2 * 100.0 / num5;
            }
            return new Tuple<double, double>( num1, num2 );
        }

        public bool IsOneHundredPercentGroup( string groupId )
        {
            return this.GetStackedSeriesFromSameGroup( groupId )[ 0 ].IsOneHundredPercent;
        }

        public IRange GetYRangeAtX( T series, Func<T, double> getYValue )
        {
            double num1 = 0.0;
            double num2 = 0.0;
            foreach ( T obj in this.GetStackedSeriesFromSameGroup( series.StackedGroupId ).Where<T>( ( Func<T, bool> ) ( x =>
            {
                if ( x.IsVisible )
                    return x.DataSeries != null;
                return false;
            } ) ) )
            {
                double num3 = getYValue(obj);
                if ( !NumberUtil.IsNaN( num3 ) )
                {
                    if ( num3 > 0.0 )
                        num1 += num3;
                    else
                        num2 += num3;
                }
            }
            return RangeFactory.NewRange( ( IComparable ) num2, ( IComparable ) num1 );
        }

        public abstract void DrawStackedSeries( IRenderContext2D renderContext );

        public virtual HitTestInfo ShiftHitTestInfo( Point rawPoint, HitTestInfo nearestHitResult, double hitTestRadius, T series )
        {
            if ( series.DataSeries != null && series.CurrentRenderPassData != null && !nearestHitResult.IsEmpty() )
            {
                bool isVerticalChart = series.CurrentRenderPassData.IsVerticalChart;
                Point point1 = DrawingHelper.TransformPoint(nearestHitResult.HitTestPoint, isVerticalChart);
                double x1 = point1.X;
                double y = point1.Y;
                bool flag = this.IsOneHundredPercentGroup(series.StackedGroupId);
                if ( this.GetStackedSeriesFromSameGroup( series.StackedGroupId ).Count<T>( ( Func<T, bool> ) ( x => x.IsVisible ) ) > 1 | flag )
                {
                    int index = nearestHitResult.DataSeriesIndex;
                    Tuple<double, double> tuple = this.AccumulateYValueAtX(series, index, false);
                    y = series.CurrentRenderPassData.YCoordinateCalculator.GetCoordinate( tuple.Item1 );
                    if ( flag )
                    {
                        IRange yrangeAtX = this.GetYRangeAtX(series, (Func<T, double>) (s => ((IComparable) s.DataSeries.YValues[index]).ToDouble()));
                        nearestHitResult.Persentage = ( double ) nearestHitResult.YValue / ( double ) yrangeAtX.Diff * 100.0;
                        nearestHitResult.DataSeriesType = DataSeriesType.OneHundredPercentStackedXy;
                    }
                    nearestHitResult.Y1Value = ( IComparable ) tuple.Item1;
                }
                Point point2 = new Point(x1, y);
                nearestHitResult.HitTestPoint = DrawingHelper.TransformPoint( point2, isVerticalChart );
            }
            return nearestHitResult;
        }

        public void AddSeries( T series )
        {
            if ( this.SeriesCollection.Contains( series ) )
                return;
            this.SeriesCollection.Add( series );
            string stackedGroupId = series.StackedGroupId;
            this.AddSeriesToGroup( series, stackedGroupId );
        }

        private void AddSeriesToGroup( T series, string groupId )
        {
            if ( this.ContainsGroup( groupId ) )
                this.GetStackedSeriesFromSameGroup( series.StackedGroupId ).Add( series );
            else
                this.SeriesGroups.Add( new Tuple<string, List<T>>( groupId, new List<T>()
        {
          series
        } ) );
        }

        private bool ContainsGroup( string groupId )
        {
            return this.SeriesGroups.Any<Tuple<string, List<T>>>( ( Func<Tuple<string, List<T>>, bool> ) ( group => group.Item1 == groupId ) );
        }

        public void RemoveSeries( T series )
        {
            if ( !this.SeriesCollection.Contains( series ) )
                return;
            this.SeriesCollection.Remove( series );
            string stackedGroupId = series.StackedGroupId;
            this.RemoveSeriesFromGroup( series, stackedGroupId );
        }

        private void RemoveSeriesFromGroup( T series, string groupId )
        {
            if ( !this.ContainsGroup( groupId ) )
                return;
            int indexOfGroup = this.FindIndexOfGroup(groupId);
            this.SeriesGroups[ indexOfGroup ].Item2.Remove( series );
            if ( this.SeriesGroups[ indexOfGroup ].Item2.Count != 0 )
                return;
            this.SeriesGroups.RemoveAt( indexOfGroup );
        }

        public void MoveSeriesToAnotherGroup( T rSeries, string oldGroupId, string newGroupId )
        {
            if ( !this.ContainsGroup( oldGroupId ) )
                return;
            this.RemoveSeriesFromGroup( rSeries, oldGroupId );
            this.AddSeriesToGroup( rSeries, newGroupId );
        }

        public IList<T> GetStackedSeriesFromSameGroup( string groupId )
        {
            return ( IList<T> ) this.SeriesGroups[ this.FindIndexOfGroup( groupId ) ].Item2;
        }

        protected int FindIndexOfGroup( string groupId )
        {
            int num = -1;
            foreach ( Tuple<string, List<T>> seriesGroup in this.SeriesGroups )
            {
                ++num;
                if ( seriesGroup.Item1 == groupId )
                    break;
            }
            return num;
        }

        internal List<T> StackedSeriesCollection
        {
            get
            {
                return this.SeriesCollection;
            }
        }

        internal List<Tuple<string, List<T>>> StackedSeriesGroups
        {
            get
            {
                return this.SeriesGroups;
            }
        }

        internal int SeriesToDrawCounter
        {
            get
            {
                return this.Counter;
            }
        }
    }
}
