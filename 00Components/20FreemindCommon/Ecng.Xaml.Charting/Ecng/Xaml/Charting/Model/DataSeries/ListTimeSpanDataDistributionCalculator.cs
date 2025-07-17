// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Model.DataSeries.ListTimeSpanDataDistributionCalculator
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecng.Xaml.Charting
{
    internal sealed class ListTimeSpanDataDistributionCalculator : BaseDataDistributionCalculator<TimeSpan>
    {
        private double _currentSpacing = 1.0;
        private const double e = 0.000125;

        public ListTimeSpanDataDistributionCalculator()
        {
            IsSortedAscending = true;
            IsEvenlySpaced = true;
        }

        public bool IsSortedAscending
        {
            get
            {
                return DataIsSortedAscending;
            }
            set
            {
                DataIsSortedAscending = value;
            }
        }

        public bool IsEvenlySpaced
        {
            get
            {
                return DataIsEvenlySpaced;
            }
            set
            {
                DataIsEvenlySpaced = value;
            }
        }

        internal double CurrentSpacing
        {
            get
            {
                return _currentSpacing;
            }
        }

        public override void OnAppendXValue( ISeriesColumn<TimeSpan> xValues, TimeSpan newXValue, bool acceptsUnsortedData )
        {
            OnAppend( ( IList<TimeSpan> ) xValues, 1 );

            if ( !acceptsUnsortedData && !IsSortedAscending )
                throw new InvalidOperationException( "Data has been Appended to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks" );
        }

        public override void OnAppendXValues( ISeriesColumn<TimeSpan> xValues, int countBeforeAppending, IEnumerable<TimeSpan> newXValues, bool acceptsUnsortedData )
        {
            OnAppend( ( IList<TimeSpan> ) xValues, ( ( ISeriesColumn ) xValues ).Count - countBeforeAppending );
            if ( !acceptsUnsortedData && !IsSortedAscending )
                throw new InvalidOperationException( "Data has been Appended to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks" );
        }

        public override void OnInsertXValue( ISeriesColumn<TimeSpan> xValues, int indexWhereInserted, TimeSpan newXValue, bool acceptsUnsortedData )
        {
            OnInsert( ( IList<TimeSpan> ) xValues, indexWhereInserted, 1 );
            if ( !acceptsUnsortedData && !IsSortedAscending )
                throw new InvalidOperationException( "Data has been Appended to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks" );
        }

        public override void OnInsertXValues( ISeriesColumn<TimeSpan> xValues, int indexWhereInserted, int insertedCount, IEnumerable<TimeSpan> newXValues, bool acceptsUnsortedData )
        {
            OnInsert( ( IList<TimeSpan> ) xValues, indexWhereInserted, newXValues.Count<TimeSpan>() );
            if ( !acceptsUnsortedData && !IsSortedAscending )
                throw new InvalidOperationException( "Data has been Appended to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks" );
        }

        public void OnAppend( IList<TimeSpan> valuesAfterAppend, int appendedCount )
        {
            if ( !IsSortedAscending )
                return;
            int count = valuesAfterAppend.Count;
            int startIndex = count - appendedCount;
            IsSortedAscending = ( appendedCount == 1 || ArrayOperations.IsSortedAscending<TimeSpan>( valuesAfterAppend, startIndex, appendedCount ) ) & ( count <= 1 || startIndex <= 0 || valuesAfterAppend[ startIndex ] >= valuesAfterAppend[ startIndex - 1 ] );
            if ( !IsEvenlySpaced )
                return;
            double spacing = 1.0;
            double epsilon = _currentSpacing * 0.000125;
            if ( ( appendedCount == 1 ? 1 : ( ArrayOperations.IsEvenlySpaced<TimeSpan>( valuesAfterAppend, startIndex, appendedCount, epsilon, out spacing ) ? 1 : 0 ) ) == 0 )
                IsEvenlySpaced = false;
            else if ( count > appendedCount && appendedCount >= 2 && Math.Abs( _currentSpacing - spacing ) > epsilon )
                IsEvenlySpaced = false;
            else if ( startIndex > 0 )
            {
                double num1 = ListTimeSpanDataDistributionCalculator.TxToDouble(valuesAfterAppend[startIndex - 1]);
                double num2 = ListTimeSpanDataDistributionCalculator.TxToDouble(valuesAfterAppend[startIndex]) - num1;
                if ( valuesAfterAppend.Count > 2 )
                {
                    if ( Math.Abs( num2 - _currentSpacing ) > epsilon )
                    {
                        IsEvenlySpaced = false;
                        return;
                    }
                }
                else if ( num2 == 0.0 )
                {
                    IsEvenlySpaced = false;
                    return;
                }
                _currentSpacing = num2;
            }
            else
                _currentSpacing = spacing;
        }

        public void OnInsert( IList<TimeSpan> valuesAfterInsert, int insertedIndex, int insertedCount )
        {
            if ( !IsSortedAscending )
                return;
            bool flag = insertedCount == 1 || ArrayOperations.IsSortedAscending<TimeSpan>(valuesAfterInsert, insertedIndex, insertedCount);
            int index = insertedIndex + insertedCount - 1;
            IsSortedAscending = flag && ( insertedIndex == 0 || valuesAfterInsert[ insertedIndex ] >= valuesAfterInsert[ insertedIndex - 1 ] ) && ( index >= valuesAfterInsert.Count - 1 || valuesAfterInsert[ index ] <= valuesAfterInsert[ index + 1 ] );
            if ( !IsEvenlySpaced )
                return;
            double spacing = 1.0;
            double epsilon = _currentSpacing * 0.000125;
            if ( ( insertedCount == 1 ? 1 : ( ArrayOperations.IsEvenlySpaced<TimeSpan>( valuesAfterInsert, insertedIndex, insertedCount, epsilon, out spacing ) ? 1 : 0 ) ) == 0 )
                IsEvenlySpaced = false;
            else if ( valuesAfterInsert.Count > insertedCount && insertedCount >= 2 && Math.Abs( _currentSpacing - spacing ) > epsilon )
            {
                IsEvenlySpaced = false;
            }
            else
            {
                if ( insertedIndex > 0 )
                {
                    double num1 = ListTimeSpanDataDistributionCalculator.TxToDouble(valuesAfterInsert[insertedIndex - 1]);
                    double num2 = ListTimeSpanDataDistributionCalculator.TxToDouble(valuesAfterInsert[insertedIndex]) - num1;
                    if ( valuesAfterInsert.Count > 2 )
                    {
                        if ( Math.Abs( num2 - _currentSpacing ) > epsilon )
                        {
                            IsEvenlySpaced = false;
                            return;
                        }
                    }
                    else if ( num2 == 0.0 )
                    {
                        IsEvenlySpaced = false;
                        return;
                    }
                    _currentSpacing = num2;
                }
                if ( index < valuesAfterInsert.Count - 1 )
                {
                    double num1 = ListTimeSpanDataDistributionCalculator.TxToDouble(valuesAfterInsert[index]);
                    double num2 = ListTimeSpanDataDistributionCalculator.TxToDouble(valuesAfterInsert[index + 1]) - num1;
                    if ( valuesAfterInsert.Count > 2 )
                    {
                        if ( Math.Abs( num2 - _currentSpacing ) > epsilon )
                        {
                            IsEvenlySpaced = false;
                            return;
                        }
                    }
                    else if ( num2 == 0.0 )
                    {
                        IsEvenlySpaced = false;
                        return;
                    }
                    _currentSpacing = num2;
                }
                else
                    _currentSpacing = spacing;
            }
        }

        public override void Clear()
        {
            base.Clear();
            _currentSpacing = 1.0;
        }

        private static double TxToDouble( TimeSpan xValue )
        {
            return ( double ) xValue.Ticks;
        }
    }
}
