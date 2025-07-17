// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Model.DataSeries.ListUInt64DataDistributionCalculator
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecng.Xaml.Charting.Model.DataSeries
{
    internal sealed class ListUInt64DataDistributionCalculator : BaseDataDistributionCalculator<ulong>
    {
        private double _currentSpacing = 1.0;
        private const double e = 0.000125;

        public ListUInt64DataDistributionCalculator()
        {
            this.IsSortedAscending = true;
            this.IsEvenlySpaced = true;
        }

        public bool IsSortedAscending
        {
            get
            {
                return this.DataIsSortedAscending;
            }
            set
            {
                this.DataIsSortedAscending = value;
            }
        }

        public bool IsEvenlySpaced
        {
            get
            {
                return this.DataIsEvenlySpaced;
            }
            set
            {
                this.DataIsEvenlySpaced = value;
            }
        }

        internal double CurrentSpacing
        {
            get
            {
                return this._currentSpacing;
            }
        }

        public override void OnAppendXValue( ISeriesColumn<ulong> xValues, ulong newXValue, bool acceptsUnsortedData )
        {
            this.OnAppend( ( IList<ulong> ) xValues, 1 );
            if ( !acceptsUnsortedData && !this.IsSortedAscending )
                throw new InvalidOperationException( "Data has been Appended to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks" );
        }

        public override void OnAppendXValues( ISeriesColumn<ulong> xValues, int countBeforeAppending, IEnumerable<ulong> newXValues, bool acceptsUnsortedData )
        {
            this.OnAppend( ( IList<ulong> ) xValues, ( ( ISeriesColumn ) xValues ).Count - countBeforeAppending );
            if ( !acceptsUnsortedData && !this.IsSortedAscending )
                throw new InvalidOperationException( "Data has been Appended to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks" );
        }

        public override void OnInsertXValue( ISeriesColumn<ulong> xValues, int indexWhereInserted, ulong newXValue, bool acceptsUnsortedData )
        {
            this.OnInsert( ( IList<ulong> ) xValues, indexWhereInserted, 1 );
            if ( !acceptsUnsortedData && !this.IsSortedAscending )
                throw new InvalidOperationException( "Data has been Appended to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks" );
        }

        public override void OnInsertXValues( ISeriesColumn<ulong> xValues, int indexWhereInserted, int insertedCount, IEnumerable<ulong> newXValues, bool acceptsUnsortedData )
        {
            this.OnInsert( ( IList<ulong> ) xValues, indexWhereInserted, newXValues.Count<ulong>() );
            if ( !acceptsUnsortedData && !this.IsSortedAscending )
                throw new InvalidOperationException( "Data has been Appended to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks" );
        }

        public void OnAppend( IList<ulong> valuesAfterAppend, int appendedCount )
        {
            if ( !this.IsSortedAscending )
                return;
            int count = valuesAfterAppend.Count;
            int startIndex = count - appendedCount;
            this.IsSortedAscending = ( appendedCount == 1 || ArrayOperations.IsSortedAscending<ulong>( valuesAfterAppend, startIndex, appendedCount ) ) & ( count <= 1 || startIndex <= 0 || valuesAfterAppend[ startIndex ] >= valuesAfterAppend[ startIndex - 1 ] );
            if ( !this.IsEvenlySpaced )
                return;
            double spacing = 1.0;
            double epsilon = this._currentSpacing * 0.000125;
            if ( ( appendedCount == 1 ? 1 : ( ArrayOperations.IsEvenlySpaced<ulong>( valuesAfterAppend, startIndex, appendedCount, epsilon, out spacing ) ? 1 : 0 ) ) == 0 )
                this.IsEvenlySpaced = false;
            else if ( count > appendedCount && appendedCount >= 2 && Math.Abs( this._currentSpacing - spacing ) > epsilon )
                this.IsEvenlySpaced = false;
            else if ( startIndex > 0 )
            {
                double num1 = ListUInt64DataDistributionCalculator.TxToDouble(valuesAfterAppend[startIndex - 1]);
                double num2 = ListUInt64DataDistributionCalculator.TxToDouble(valuesAfterAppend[startIndex]) - num1;
                if ( valuesAfterAppend.Count > 2 )
                {
                    if ( Math.Abs( num2 - this._currentSpacing ) > epsilon )
                    {
                        this.IsEvenlySpaced = false;
                        return;
                    }
                }
                else if ( num2 == 0.0 )
                {
                    this.IsEvenlySpaced = false;
                    return;
                }
                this._currentSpacing = num2;
            }
            else
                this._currentSpacing = spacing;
        }

        public void OnInsert( IList<ulong> valuesAfterInsert, int insertedIndex, int insertedCount )
        {
            if ( !this.IsSortedAscending )
                return;
            bool flag = insertedCount == 1 || ArrayOperations.IsSortedAscending<ulong>(valuesAfterInsert, insertedIndex, insertedCount);
            int index = insertedIndex + insertedCount - 1;
            this.IsSortedAscending = flag && ( insertedIndex == 0 || valuesAfterInsert[ insertedIndex ] >= valuesAfterInsert[ insertedIndex - 1 ] ) && ( index >= valuesAfterInsert.Count - 1 || valuesAfterInsert[ index ] <= valuesAfterInsert[ index + 1 ] );
            if ( !this.IsEvenlySpaced )
                return;
            double spacing = 1.0;
            double epsilon = this._currentSpacing * 0.000125;
            if ( ( insertedCount == 1 ? 1 : ( ArrayOperations.IsEvenlySpaced<ulong>( valuesAfterInsert, insertedIndex, insertedCount, epsilon, out spacing ) ? 1 : 0 ) ) == 0 )
                this.IsEvenlySpaced = false;
            else if ( valuesAfterInsert.Count > insertedCount && insertedCount >= 2 && Math.Abs( this._currentSpacing - spacing ) > epsilon )
            {
                this.IsEvenlySpaced = false;
            }
            else
            {
                if ( insertedIndex > 0 )
                {
                    double num1 = ListUInt64DataDistributionCalculator.TxToDouble(valuesAfterInsert[insertedIndex - 1]);
                    double num2 = ListUInt64DataDistributionCalculator.TxToDouble(valuesAfterInsert[insertedIndex]) - num1;
                    if ( valuesAfterInsert.Count > 2 )
                    {
                        if ( Math.Abs( num2 - this._currentSpacing ) > epsilon )
                        {
                            this.IsEvenlySpaced = false;
                            return;
                        }
                    }
                    else if ( num2 == 0.0 )
                    {
                        this.IsEvenlySpaced = false;
                        return;
                    }
                    this._currentSpacing = num2;
                }
                if ( index < valuesAfterInsert.Count - 1 )
                {
                    double num1 = ListUInt64DataDistributionCalculator.TxToDouble(valuesAfterInsert[index]);
                    double num2 = ListUInt64DataDistributionCalculator.TxToDouble(valuesAfterInsert[index + 1]) - num1;
                    if ( valuesAfterInsert.Count > 2 )
                    {
                        if ( Math.Abs( num2 - this._currentSpacing ) > epsilon )
                        {
                            this.IsEvenlySpaced = false;
                            return;
                        }
                    }
                    else if ( num2 == 0.0 )
                    {
                        this.IsEvenlySpaced = false;
                        return;
                    }
                    this._currentSpacing = num2;
                }
                else
                    this._currentSpacing = spacing;
            }
        }

        public override void Clear()
        {
            base.Clear();
            this._currentSpacing = 1.0;
        }

        private static double TxToDouble( ulong xValue )
        {
            return ( double ) xValue;
        }
    }
}
