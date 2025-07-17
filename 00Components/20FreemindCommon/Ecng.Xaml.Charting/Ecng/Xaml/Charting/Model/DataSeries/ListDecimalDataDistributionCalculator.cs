// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Model.DataSeries.ListDecimalDataDistributionCalculator
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\fx.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace fx.Xaml.Charting
{
    internal sealed class ListDecimalDataDistributionCalculator : BaseDataDistributionCalculator<Decimal>
    {
        private double _currentSpacing = 1.0;
        private const double e = 0.000125;

        public ListDecimalDataDistributionCalculator()
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

        public override void OnAppendXValue( ISeriesColumn<Decimal> xValues, Decimal newXValue, bool acceptsUnsortedData )
        {
            OnAppend( ( IList<Decimal> ) xValues, 1 );
            if ( !acceptsUnsortedData && !IsSortedAscending )
                throw new InvalidOperationException( "Data has been Appended to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks" );
        }

        public override void OnAppendXValues( ISeriesColumn<Decimal> xValues, int countBeforeAppending, IEnumerable<Decimal> newXValues, bool acceptsUnsortedData )
        {
            OnAppend( ( IList<Decimal> ) xValues, ( ( ISeriesColumn ) xValues ).Count - countBeforeAppending );
            if ( !acceptsUnsortedData && !IsSortedAscending )
                throw new InvalidOperationException( "Data has been Appended to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks" );
        }

        public override void OnInsertXValue( ISeriesColumn<Decimal> xValues, int indexWhereInserted, Decimal newXValue, bool acceptsUnsortedData )
        {
            OnInsert( ( IList<Decimal> ) xValues, indexWhereInserted, 1 );
            if ( !acceptsUnsortedData && !IsSortedAscending )
                throw new InvalidOperationException( "Data has been Appended to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks" );
        }

        public override void OnInsertXValues( ISeriesColumn<Decimal> xValues, int indexWhereInserted, int insertedCount, IEnumerable<Decimal> newXValues, bool acceptsUnsortedData )
        {
            OnInsert( ( IList<Decimal> ) xValues, indexWhereInserted, newXValues.Count<Decimal>() );
            if ( !acceptsUnsortedData && !IsSortedAscending )
                throw new InvalidOperationException( "Data has been Appended to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks" );
        }

        public void OnAppend( IList<Decimal> valuesAfterAppend, int appendedCount )
        {
            if ( !IsSortedAscending )
                return;
            int count = valuesAfterAppend.Count;
            int startIndex = count - appendedCount;
            IsSortedAscending = ( appendedCount == 1 || ArrayOperations.IsSortedAscending<Decimal>( valuesAfterAppend, startIndex, appendedCount ) ) & ( count <= 1 || startIndex <= 0 || valuesAfterAppend[ startIndex ] >= valuesAfterAppend[ startIndex - 1 ] );
            if ( !IsEvenlySpaced )
                return;
            double spacing = 1.0;
            double epsilon = _currentSpacing * 0.000125;
            if ( ( appendedCount == 1 ? 1 : ( ArrayOperations.IsEvenlySpaced<Decimal>( valuesAfterAppend, startIndex, appendedCount, epsilon, out spacing ) ? 1 : 0 ) ) == 0 )
                IsEvenlySpaced = false;
            else if ( count > appendedCount && appendedCount >= 2 && Math.Abs( _currentSpacing - spacing ) > epsilon )
                IsEvenlySpaced = false;
            else if ( startIndex > 0 )
            {
                double num1 = ListDecimalDataDistributionCalculator.TxToDouble(valuesAfterAppend[startIndex - 1]);
                double num2 = ListDecimalDataDistributionCalculator.TxToDouble(valuesAfterAppend[startIndex]) - num1;
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

        public void OnInsert( IList<Decimal> valuesAfterInsert, int insertedIndex, int insertedCount )
        {
            if ( !IsSortedAscending )
                return;
            bool flag = insertedCount == 1 || ArrayOperations.IsSortedAscending<Decimal>(valuesAfterInsert, insertedIndex, insertedCount);
            int index = insertedIndex + insertedCount - 1;
            IsSortedAscending = flag && ( insertedIndex == 0 || valuesAfterInsert[ insertedIndex ] >= valuesAfterInsert[ insertedIndex - 1 ] ) && ( index >= valuesAfterInsert.Count - 1 || valuesAfterInsert[ index ] <= valuesAfterInsert[ index + 1 ] );
            if ( !IsEvenlySpaced )
                return;
            double spacing = 1.0;
            double epsilon = _currentSpacing * 0.000125;
            if ( ( insertedCount == 1 ? 1 : ( ArrayOperations.IsEvenlySpaced<Decimal>( valuesAfterInsert, insertedIndex, insertedCount, epsilon, out spacing ) ? 1 : 0 ) ) == 0 )
                IsEvenlySpaced = false;
            else if ( valuesAfterInsert.Count > insertedCount && insertedCount >= 2 && Math.Abs( _currentSpacing - spacing ) > epsilon )
            {
                IsEvenlySpaced = false;
            }
            else
            {
                if ( insertedIndex > 0 )
                {
                    double num1 = ListDecimalDataDistributionCalculator.TxToDouble(valuesAfterInsert[insertedIndex - 1]);
                    double num2 = ListDecimalDataDistributionCalculator.TxToDouble(valuesAfterInsert[insertedIndex]) - num1;
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
                    double num1 = ListDecimalDataDistributionCalculator.TxToDouble(valuesAfterInsert[index]);
                    double num2 = ListDecimalDataDistributionCalculator.TxToDouble(valuesAfterInsert[index + 1]) - num1;
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

        private static double TxToDouble( Decimal xValue )
        {
            return ( double ) xValue;
        }
    }
}
