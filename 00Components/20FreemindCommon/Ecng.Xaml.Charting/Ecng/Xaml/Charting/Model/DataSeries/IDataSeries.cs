// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Model.DataSeries.IDataSeries
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Collections;
namespace fx.Xaml.Charting
{
    public interface IDataSeries : ISuspendable
    {
        event EventHandler<DataSeriesChangedEventArgs> DataSeriesChanged;

        Type XType
        {
            get;
        }

        Type YType
        {
            get;
        }

        ISciChartSurface ParentSurface
        {
            get; set;
        }

        [Obsolete( "IsAttached is obsolete because there is no DataSeriesSet now" )]
        bool IsAttached
        {
            get;
        }

        IRange XRange
        {
            get;
        }

        IRange YRange
        {
            get;
        }

        DataSeriesType DataSeriesType
        {
            get;
        }

        IList XValues
        {
            get;
        }

        IList YValues
        {
            get;
        }

        IComparable LatestYValue
        {
            get;
        }

        string SeriesName
        {
            get; set;
        }

        IComparable YMin
        {
            get;
        }

        [Obsolete( "XRange, YRange, XMin, YMin are all obsolete for performance reasons, Sorry!", true )]
        IComparable YMinPositive
        {
            get;
        }

        IComparable YMax
        {
            get;
        }

        IComparable XMin
        {
            get;
        }

        [Obsolete( "XRange, YRange, XMin, YMin are all obsolete for performance reasons, Sorry!", true )]
        IComparable XMinPositive
        {
            get;
        }

        IComparable XMax
        {
            get;
        }

        bool IsFifo
        {
            get;
        }

        int? FifoCapacity
        {
            get; set;
        }

        bool HasValues
        {
            get;
        }

        int Count
        {
            get;
        }

        bool IsSorted
        {
            get;
        }

        object SyncRoot
        {
            get;
        }

        bool AcceptsUnsortedData
        {
            get; set;
        }

        void Clear();

        IndexRange GetIndicesRange( IRange visibleRange );

        IPointSeries ToPointSeries( ResamplingMode resamplingMode, IndexRange pointRange, int viewportWidth, bool isCategoryAxis, bool? dataIsDisplayedAs2D, IRange visibleXRange, IPointResamplerFactory factory, object pointSeriesArg = null );

        [Obsolete( "ToPointSeries overload has been deprecated, use ToPointSeries instead, and cast to correct type of point series", true )]
        IPointSeries ToPointSeries( IList column, ResamplingMode resamplingMode, IndexRange pointRange, int viewportWidth, bool isCategoryAxis );

        IRange GetWindowedYRange( IRange xRange );

        IRange GetWindowedYRange( IndexRange xIndexRange );

        IRange GetWindowedYRange( IndexRange xIndexRange, bool getPositiveRange );

        IRange GetWindowedYRange( IRange xRange, bool getPositiveRange );

        int FindIndex( IComparable x, SearchMode searchMode = SearchMode.Exact );

        HitTestInfo ToHitTestInfo( int index );

        void InvalidateParentSurface( RangeMode rangeMode );

        int FindClosestPoint( IComparable x, IComparable y, double xyScaleRatio, double maxXDistance );

        int FindClosestLine( IComparable x, IComparable y, double xyScaleRatio, double xRadius, LineDrawMode drawNanAs );

        void OnBeginRenderPass();
    }
}
