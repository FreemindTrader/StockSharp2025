using System;
using System.Collections.Generic;

namespace StockSharp.Xaml.Charting.Model.DataSeries
{
    public interface IDataDistributionCalculator<TX> where TX : IComparable
    {
        bool DataIsSortedAscending
        {
            get;
        }

        bool DataIsEvenlySpaced
        {
            get;
        }

        void OnAppendXValue( ISeriesColumn<TX> xValues, TX newXValue, bool acceptsUnsortedData );

        void OnAppendXValues( ISeriesColumn<TX> xValues, int countBeforeAppending, IEnumerable<TX> newXValues, bool acceptsUnsortedData );

        void OnInsertXValue( ISeriesColumn<TX> xValues, int indexWhereInserted, TX newXValue, bool acceptsUnsortedData );

        void OnInsertXValues( ISeriesColumn<TX> xValues, int indexWhereInserted, int insertedCount, IEnumerable<TX> newXValues, bool acceptsUnsortedData );

        void UpdateDataDistributionFlagsWhenRemovedXValues();

        void Clear();
    }
}
