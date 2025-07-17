// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Model.DataSeries.Int16DataDistributionCalculator
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Collections.Generic;
namespace fx.Xaml.Charting
{
    internal class Int16DataDistributionCalculator : BaseDataDistributionCalculator<short>
    {
        private const double MinVisibleRelativeXDiffError = 0.000125;
        private double _lastXValue;
        private double _firstXDiff;
        private bool _firstXDiffIsValid;
        private double _minVisibleXDiffError;

        public override void OnAppendXValue( ISeriesColumn<short> xValues, short newXValue, bool acceptsUnsortedData )
        {
            this.UpdateDataDistributionFlagsWhenAppendedXValue( xValues, newXValue, ( ( ICollection<short> ) xValues ).Count - 1, acceptsUnsortedData );
        }

        public override void OnAppendXValues( ISeriesColumn<short> xValues, int countBeforeAppending, IEnumerable<short> newXValues, bool acceptsUnsortedData )
        {
            short[] newXValues1 = newXValues as short[];
            if ( newXValues1 != null )
            {
                this.UpdateDataDistributionFlagsWhenAppendedDataArray( ( IList<short> ) xValues, countBeforeAppending, newXValues1, newXValues1.Length, acceptsUnsortedData );
            }
            else
            {
                IList<short> list = newXValues as IList<short>;
                if ( list != null )
                {
                    int count = list.Count;
                    short[] uncheckedList = list.ToUncheckedList<short>();
                    this.UpdateDataDistributionFlagsWhenAppendedDataArray( ( IList<short> ) xValues, countBeforeAppending, uncheckedList, count, acceptsUnsortedData );
                }
                else
                {
                    IEnumerable<short> shorts = newXValues;
                    int lastIndexAfterAppending = countBeforeAppending;
                    foreach ( short newXValue in shorts )
                    {
                        this.UpdateDataDistributionFlagsWhenAppendedXValue( xValues, newXValue, lastIndexAfterAppending, acceptsUnsortedData );
                        ++lastIndexAfterAppending;
                    }
                }
            }
        }

        public override void OnInsertXValue( ISeriesColumn<short> xValues2, int indexWhereInserted, short newXValue, bool acceptsUnsortedData )
        {
            if ( !this.DataIsSortedAscending && !this.DataIsEvenlySpaced )
                return;
            IList<short> shortList = (IList<short>) xValues2;
            int count = shortList.Count;
            if ( indexWhereInserted == 0 )
            {
                if ( count <= 1 )
                    return;
                short xValue = shortList[1];
                double num1 = Int16DataDistributionCalculator.TxToDouble(xValue) - Int16DataDistributionCalculator.TxToDouble(newXValue);
                this._firstXDiff = num1;
                this._firstXDiffIsValid = true;
                if ( num1 < 0.0 )
                {
                    this.DataIsSortedAscending = false;
                    this.DataIsEvenlySpaced = false;
                    if ( !this.DataIsSortedAscending && !acceptsUnsortedData )
                        throw new InvalidOperationException( "Data has been Inserted to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks" );
                }
                else
                {
                    if ( !this.DataIsEvenlySpaced || !this._firstXDiffIsValid )
                        return;
                    double num2 = num1 - (Int16DataDistributionCalculator.TxToDouble(shortList[2]) - Int16DataDistributionCalculator.TxToDouble(xValue));
                    if ( num2 < 0.0 )
                        num2 = -num2;
                    if ( num2 <= this._minVisibleXDiffError )
                        return;
                    this.DataIsEvenlySpaced = false;
                }
            }
            else if ( indexWhereInserted == count )
            {
                this.OnAppendXValue( xValues2, newXValue, acceptsUnsortedData );
            }
            else
            {
                this.DataIsEvenlySpaced = false;
                if ( !this.DataIsSortedAscending )
                    return;
                if ( indexWhereInserted > 0 && ( int ) shortList[ indexWhereInserted - 1 ] > ( int ) newXValue )
                {
                    this.DataIsSortedAscending = false;
                    if ( !this.DataIsSortedAscending && !acceptsUnsortedData )
                        throw new InvalidOperationException( "Data has been Inserted to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks" );
                }
                if ( indexWhereInserted >= count - 1 || ( int ) shortList[ indexWhereInserted + 1 ] >= ( int ) newXValue )
                    return;
                this.DataIsSortedAscending = false;
                if ( !this.DataIsSortedAscending && !acceptsUnsortedData )
                    throw new InvalidOperationException( "Data has been Inserted to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks" );
            }
        }

        public override void OnInsertXValues( ISeriesColumn<short> xValues2, int indexWhereInserted, int insertedCount, IEnumerable<short> newXValues, bool acceptsUnsortedData )
        {
            IList<short> shortList = (IList<short>) xValues2;
            int count = shortList.Count;
            if ( indexWhereInserted + insertedCount == count )
                this.OnAppendXValues( xValues2, count - insertedCount, newXValues, acceptsUnsortedData );
            else if ( indexWhereInserted == 0 )
            {
                if ( count <= 2 )
                    return;
                double num1 = Int16DataDistributionCalculator.TxToDouble(shortList[1]) - Int16DataDistributionCalculator.TxToDouble(shortList[0]);
                this._firstXDiff = num1;
                this._firstXDiffIsValid = true;
                this._minVisibleXDiffError = this._firstXDiff * 0.000125;
                double visibleXdiffError = this._minVisibleXDiffError;
                for ( int index = 2 ; index < insertedCount ; ++index )
                {
                    double num2 = Int16DataDistributionCalculator.TxToDouble(shortList[index]);
                    double num3 = num2 - this._lastXValue;
                    this._lastXValue = num2;
                    if ( num3 < 0.0 )
                    {
                        this.DataIsSortedAscending = false;
                        this.DataIsEvenlySpaced = false;
                        if ( this.DataIsSortedAscending || acceptsUnsortedData )
                            break;
                        throw new InvalidOperationException( "Data has been Inserted to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks" );
                    }
                    if ( this.DataIsEvenlySpaced )
                    {
                        double num4 = num3 - num1;
                        if ( num4 < 0.0 )
                            num4 = -num4;
                        if ( num4 > visibleXdiffError )
                        {
                            this.DataIsEvenlySpaced = false;
                            break;
                        }
                    }
                }
            }
            else
            {
                this.DataIsEvenlySpaced = false;
                if ( !this.DataIsSortedAscending )
                    return;
                short num1 = shortList[indexWhereInserted - 1];
                int num2 = indexWhereInserted + insertedCount + 1;
                for ( int index = indexWhereInserted ; index < num2 ; ++index )
                {
                    short num3 = shortList[index];
                    if ( ( int ) num3 < ( int ) num1 )
                    {
                        this.DataIsSortedAscending = false;
                        if ( this.DataIsSortedAscending || acceptsUnsortedData )
                            break;
                        throw new InvalidOperationException( "Data has been Inserted to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks" );
                    }
                    num1 = num3;
                }
            }
        }

        private void UpdateDataDistributionFlagsWhenAppendedXValue( ISeriesColumn<short> xValues, short newXValue, int lastIndexAfterAppending, bool acceptsUnsortedData )
        {
            if ( !this.DataIsSortedAscending && !this.DataIsEvenlySpaced )
                return;
            if ( lastIndexAfterAppending > 0 )
            {
                double num1 = Int16DataDistributionCalculator.TxToDouble(newXValue) - this._lastXValue;
                if ( num1 < 0.0 )
                {
                    this.DataIsSortedAscending = false;
                    this.DataIsEvenlySpaced = false;
                    if ( this.DataIsSortedAscending || acceptsUnsortedData )
                        return;
                    throw new InvalidOperationException( "Data has been Appended to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks" );
                }
                if ( this.DataIsEvenlySpaced )
                {
                    if ( this._firstXDiffIsValid )
                    {
                        double num2 = num1 - this._firstXDiff;
                        if ( num2 < 0.0 )
                            num2 = -num2;
                        if ( num2 > this._minVisibleXDiffError )
                            this.DataIsEvenlySpaced = false;
                    }
                    else
                    {
                        this._firstXDiffIsValid = true;
                        this._firstXDiff = num1;
                        this._minVisibleXDiffError = num1 * 0.000125;
                    }
                }
            }
            this._lastXValue = Int16DataDistributionCalculator.TxToDouble( newXValue );
        }

        private void UpdateDataDistributionFlagsWhenAppendedDataArray( IList<short> xValues, int countBeforeAppending, short[ ] newXValues, int newXValuesLength, bool acceptsUnsortedData )
        {
            if ( !this.DataIsSortedAscending && !this.DataIsEvenlySpaced )
                return;
            int num1 = newXValuesLength;
            double visibleXdiffError = this._minVisibleXDiffError;
            if ( countBeforeAppending > 0 && newXValuesLength > 0 )
            {
                double num2 = Int16DataDistributionCalculator.TxToDouble(newXValues[0]) - this._lastXValue;
                if ( this.DataIsSortedAscending && this._lastXValue > Int16DataDistributionCalculator.TxToDouble( newXValues[ 0 ] ) )
                {
                    this.DataIsSortedAscending = false;
                    if ( !this.DataIsSortedAscending && !acceptsUnsortedData )
                        throw new InvalidOperationException( "Data has been Appended to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks" );
                }
                if ( this.DataIsEvenlySpaced )
                {
                    if ( this._firstXDiffIsValid && newXValuesLength > 0 )
                    {
                        double num3 = num2 - this._firstXDiff;
                        if ( num3 < 0.0 )
                            num3 = -num3;
                        if ( num3 > this._minVisibleXDiffError )
                            this.DataIsEvenlySpaced = false;
                    }
                    else
                    {
                        this._firstXDiff = num2;
                        this._firstXDiffIsValid = true;
                        this._minVisibleXDiffError = num2 * 0.000125;
                    }
                }
            }
            if ( !false )
            {
                double num2 = Int16DataDistributionCalculator.TxToDouble(newXValues[0]);
                double num3 = this._firstXDiff;
                bool flag = this._firstXDiffIsValid;
                for ( int index = 1 ; index < num1 ; ++index )
                {
                    double num4 = Int16DataDistributionCalculator.TxToDouble(newXValues[index]);
                    double num5 = num4 - num2;
                    if ( num5 < 0.0 )
                    {
                        this.DataIsSortedAscending = false;
                        this.DataIsEvenlySpaced = false;
                        if ( !this.DataIsSortedAscending && !acceptsUnsortedData )
                            throw new InvalidOperationException( "Data has been Appended to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks" );
                        break;
                    }
                    num2 = num4;
                    if ( this.DataIsEvenlySpaced )
                    {
                        if ( flag )
                        {
                            double num6 = num5 - num3;
                            if ( num6 < 0.0 )
                                num6 = -num6;
                            if ( num6 > visibleXdiffError )
                                this.DataIsEvenlySpaced = false;
                        }
                        else
                        {
                            num3 = num5;
                            flag = true;
                        }
                    }
                }
                this._lastXValue = num2;
                this._firstXDiffIsValid = flag;
                this._firstXDiff = num3;
            }
            this._lastXValue = Int16DataDistributionCalculator.TxToDouble( newXValues[ num1 - 1 ] );
            if ( this._firstXDiffIsValid || num1 <= 1 )
                return;
            this._firstXDiff = this._lastXValue - Int16DataDistributionCalculator.TxToDouble( newXValues[ num1 - 2 ] );
            this._firstXDiffIsValid = true;
            this._minVisibleXDiffError = this._firstXDiff * 0.000125;
        }

        private static double TxToDouble( short xValue )
        {
            return ( double ) xValue;
        }
    }
}
