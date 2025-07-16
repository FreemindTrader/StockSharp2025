using System;
using System.Collections.Generic;

namespace StockSharp.Xaml.Charting.Model.DataSeries
{
    public abstract class BaseDataDistributionCalculator<TX> : IDataDistributionCalculator<TX> where TX : IComparable
    {
        internal const string DataInsertedUnsortedWarning = "Data has been Inserted to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks";
        internal const string DataAppendedUnsortedWarning = "Data has been Appended to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks";
        private const string PostAmble = "Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks";

        public bool DataIsSortedAscending
        {
            get; protected set;
        }

        public bool DataIsEvenlySpaced
        {
            get; protected set;
        }

        public void UpdateDataDistributionFlagsWhenRemovedXValues()
        {
            this.DataIsEvenlySpaced = false;
        }

        public abstract void OnAppendXValue( ISeriesColumn<TX> xValues, TX newXValue, bool acceptsUnsortedData );

        public abstract void OnAppendXValues( ISeriesColumn<TX> xValues, int countBeforeAppending, IEnumerable<TX> newXValues, bool acceptsUnsortedData );

        public abstract void OnInsertXValues( ISeriesColumn<TX> xValues, int indexWhereInserted, int insertedCount, IEnumerable<TX> newXValues, bool acceptsUnsortedData );

        public abstract void OnInsertXValue( ISeriesColumn<TX> xValues, int indexWhereInserted, TX newXValue, bool acceptsUnsortedData );

        public virtual void Clear()
        {
            this.DataIsSortedAscending = true;
            this.DataIsEvenlySpaced = true;
        }
    }
}
