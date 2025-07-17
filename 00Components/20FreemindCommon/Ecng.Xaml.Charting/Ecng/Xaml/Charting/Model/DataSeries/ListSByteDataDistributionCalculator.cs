// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Model.DataSeries.ListSByteDataDistributionCalculator
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\fx.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace fx.Xaml.Charting
{
    internal sealed class ListSByteDataDistributionCalculator : BaseDataDistributionCalculator<sbyte>
    {
        private double _currentSpacing = 1.0;
        private const double e = 0.000125;

        public ListSByteDataDistributionCalculator()
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

        public override void OnAppendXValue( ISeriesColumn<sbyte> xValues, sbyte newXValue, bool acceptsUnsortedData )
        {
            OnAppend( ( IList<sbyte> ) xValues, 1 );
            if ( !acceptsUnsortedData && !IsSortedAscending )
                throw new InvalidOperationException( "Data has been Appended to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks" );
        }

        public override void OnAppendXValues( ISeriesColumn<sbyte> xValues, int countBeforeAppending, IEnumerable<sbyte> newXValues, bool acceptsUnsortedData )
        {
            OnAppend( ( IList<sbyte> ) xValues, ( ( ISeriesColumn ) xValues ).Count - countBeforeAppending );
            if ( !acceptsUnsortedData && !IsSortedAscending )
                throw new InvalidOperationException( "Data has been Appended to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks" );
        }

        public override void OnInsertXValue( ISeriesColumn<sbyte> xValues, int indexWhereInserted, sbyte newXValue, bool acceptsUnsortedData )
        {
            OnInsert( ( IList<sbyte> ) xValues, indexWhereInserted, 1 );
            if ( !acceptsUnsortedData && !IsSortedAscending )
                throw new InvalidOperationException( "Data has been Appended to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks" );
        }

        public override void OnInsertXValues( ISeriesColumn<sbyte> xValues, int indexWhereInserted, int insertedCount, IEnumerable<sbyte> newXValues, bool acceptsUnsortedData )
        {
            OnInsert( ( IList<sbyte> ) xValues, indexWhereInserted, newXValues.Count<sbyte>() );
            if ( !acceptsUnsortedData && !IsSortedAscending )
                throw new InvalidOperationException( "Data has been Appended to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks" );
        }

        public void OnAppend( IList<sbyte> valuesAfterAppend, int appendedCount )
        {
            if ( !IsSortedAscending )
                return;
            int count = valuesAfterAppend.Count;
            int startIndex = count - appendedCount;
            IsSortedAscending = ( appendedCount == 1 || ArrayOperations.IsSortedAscending<sbyte>( valuesAfterAppend, startIndex, appendedCount ) ) & ( count <= 1 || startIndex <= 0 || ( int ) valuesAfterAppend[ startIndex ] >= ( int ) valuesAfterAppend[ startIndex - 1 ] );
            if ( !IsEvenlySpaced )
                return;
            double spacing = 1.0;
            double epsilon = _currentSpacing * 0.000125;
            if ( ( appendedCount == 1 ? 1 : ( ArrayOperations.IsEvenlySpaced<sbyte>( valuesAfterAppend, startIndex, appendedCount, epsilon, out spacing ) ? 1 : 0 ) ) == 0 )
                IsEvenlySpaced = false;
            else if ( count > appendedCount && appendedCount >= 2 && Math.Abs( _currentSpacing - spacing ) > epsilon )
                IsEvenlySpaced = false;
            else if ( startIndex > 0 )
            {
                double num1 = ListSByteDataDistributionCalculator.TxToDouble(valuesAfterAppend[startIndex - 1]);
                double num2 = ListSByteDataDistributionCalculator.TxToDouble(valuesAfterAppend[startIndex]) - num1;
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

        public void OnInsert( IList<sbyte> valuesAfterInsert, int insertedIndex, int insertedCount )
        {
            if ( !IsSortedAscending )
                return;
            bool flag = insertedCount == 1 || ArrayOperations.IsSortedAscending<sbyte>(valuesAfterInsert, insertedIndex, insertedCount);
            int index = insertedIndex + insertedCount - 1;
            IsSortedAscending = flag && ( insertedIndex == 0 || ( int ) valuesAfterInsert[ insertedIndex ] >= ( int ) valuesAfterInsert[ insertedIndex - 1 ] ) && ( index >= valuesAfterInsert.Count - 1 || ( int ) valuesAfterInsert[ index ] <= ( int ) valuesAfterInsert[ index + 1 ] );
            if ( !IsEvenlySpaced )
                return;
            double spacing = 1.0;
            double epsilon = _currentSpacing * 0.000125;
            if ( ( insertedCount == 1 ? 1 : ( ArrayOperations.IsEvenlySpaced<sbyte>( valuesAfterInsert, insertedIndex, insertedCount, epsilon, out spacing ) ? 1 : 0 ) ) == 0 )
                IsEvenlySpaced = false;
            else if ( valuesAfterInsert.Count > insertedCount && insertedCount >= 2 && Math.Abs( _currentSpacing - spacing ) > epsilon )
            {
                IsEvenlySpaced = false;
            }
            else
            {
                if ( insertedIndex > 0 )
                {
                    double num1 = ListSByteDataDistributionCalculator.TxToDouble(valuesAfterInsert[insertedIndex - 1]);
                    double num2 = ListSByteDataDistributionCalculator.TxToDouble(valuesAfterInsert[insertedIndex]) - num1;
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
                    double num1 = ListSByteDataDistributionCalculator.TxToDouble(valuesAfterInsert[index]);
                    double num2 = ListSByteDataDistributionCalculator.TxToDouble(valuesAfterInsert[index + 1]) - num1;
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

        private static double TxToDouble( sbyte xValue )
        {
            return ( double ) xValue;
        }
    }
}
