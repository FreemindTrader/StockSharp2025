// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Model.DataSeries.DoubleDataDistributionCalculator
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\fx.Xaml.Charting.dll

using System;
using System.Collections.Generic;
namespace fx.Xaml.Charting
{
    internal class DoubleDataDistributionCalculator : BaseDataDistributionCalculator<double>
    {
        private const double MinVisibleRelativeXDiffError = 0.000125;
        private double _lastXValue;
        private double _firstXDiff;
        private bool   _firstXDiffIsValid;
        private double _minVisibleXDiffError;

        public override void OnAppendXValue( ISeriesColumn<double> xValues, double newXValue, bool acceptsUnsortedData )
        {
            UpdateDataDistributionFlagsWhenAppendedXValue( xValues, newXValue, ( ( ICollection<double> ) xValues ).Count - 1, acceptsUnsortedData );
        }

        public override void OnAppendXValues( ISeriesColumn<double> xValues, int countBeforeAppending, IEnumerable<double> newXValues, bool acceptsUnsortedData )
        {
            double[] newXValues1 = newXValues as double[];

            if ( newXValues1 != null )
            {
                UpdateDataDistributionFlagsWhenAppendedDataArray( ( IList<double> ) xValues, countBeforeAppending, newXValues1, newXValues1.Length, acceptsUnsortedData );
            }
            else
            {
                IList<double> list = newXValues as IList<double>;

                if ( list != null )
                {
                    int count = list.Count;
                    double[] uncheckedList = list.ToUncheckedList<double>();
                    UpdateDataDistributionFlagsWhenAppendedDataArray( ( IList<double> ) xValues, countBeforeAppending, uncheckedList, count, acceptsUnsortedData );
                }
                else
                {
                    IEnumerable<double> doubles = newXValues;
                    int lastIndexAfterAppending = countBeforeAppending;
                    foreach ( double newXValue in doubles )
                    {
                        UpdateDataDistributionFlagsWhenAppendedXValue( xValues, newXValue, lastIndexAfterAppending, acceptsUnsortedData );
                        ++lastIndexAfterAppending;
                    }
                }
            }
        }

        public override void OnInsertXValue( ISeriesColumn<double> xValues2, int indexWhereInserted, double newXValue, bool acceptsUnsortedData )
        {
            if ( !DataIsSortedAscending && !DataIsEvenlySpaced )
                return;

            IList<double> doubleList = (IList<double>) xValues2;
            int count = doubleList.Count;

            if ( indexWhereInserted == 0 )
            {
                if ( count <= 1 )
                    return;

                double xValue      = doubleList[1];
                double num1        = DoubleDataDistributionCalculator.TxToDouble(xValue) - DoubleDataDistributionCalculator.TxToDouble(newXValue);
                _firstXDiff = num1;
                _firstXDiffIsValid = true;

                if ( num1 < 0.0 )
                {
                    DataIsSortedAscending = false;
                    DataIsEvenlySpaced = false;
                    if ( !DataIsSortedAscending && !acceptsUnsortedData )
                        throw new InvalidOperationException( "Data has been Inserted to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks" );
                }
                else
                {
                    if ( !DataIsEvenlySpaced || !_firstXDiffIsValid )
                        return;
                    double num2 = num1 - (DoubleDataDistributionCalculator.TxToDouble(doubleList[2]) - DoubleDataDistributionCalculator.TxToDouble(xValue));
                    if ( num2 < 0.0 )
                        num2 = -num2;
                    if ( num2 <= _minVisibleXDiffError )
                        return;
                    DataIsEvenlySpaced = false;
                }
            }
            else if ( indexWhereInserted == count )
            {
                OnAppendXValue( xValues2, newXValue, acceptsUnsortedData );
            }
            else
            {
                DataIsEvenlySpaced = false;
                if ( !DataIsSortedAscending )
                    return;
                if ( indexWhereInserted > 0 && doubleList[ indexWhereInserted - 1 ] > newXValue )
                {
                    DataIsSortedAscending = false;
                    if ( !DataIsSortedAscending && !acceptsUnsortedData )
                        throw new InvalidOperationException( "Data has been Inserted to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks" );
                }
                if ( indexWhereInserted >= count - 1 || doubleList[ indexWhereInserted + 1 ] >= newXValue )
                    return;
                DataIsSortedAscending = false;
                if ( !DataIsSortedAscending && !acceptsUnsortedData )
                    throw new InvalidOperationException( "Data has been Inserted to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks" );
            }
        }

        public override void OnInsertXValues( ISeriesColumn<double> xValues2, int indexWhereInserted, int insertedCount, IEnumerable<double> newXValues, bool acceptsUnsortedData )
        {
            IList<double> doubleList = (IList<double>) xValues2;
            int count = doubleList.Count;
            if ( indexWhereInserted + insertedCount == count )
                OnAppendXValues( xValues2, count - insertedCount, newXValues, acceptsUnsortedData );
            else if ( indexWhereInserted == 0 )
            {
                if ( count <= 2 )
                    return;
                double num1 = DoubleDataDistributionCalculator.TxToDouble(doubleList[1]) - DoubleDataDistributionCalculator.TxToDouble(doubleList[0]);
                _firstXDiff = num1;
                _firstXDiffIsValid = true;
                _minVisibleXDiffError = _firstXDiff * 0.000125;
                double visibleXdiffError = _minVisibleXDiffError;
                for ( int index = 2 ; index < insertedCount ; ++index )
                {
                    double num2 = DoubleDataDistributionCalculator.TxToDouble(doubleList[index]);
                    double num3 = num2 - _lastXValue;
                    _lastXValue = num2;
                    if ( num3 < 0.0 )
                    {
                        DataIsSortedAscending = false;
                        DataIsEvenlySpaced = false;
                        if ( DataIsSortedAscending || acceptsUnsortedData )
                            break;
                        throw new InvalidOperationException( "Data has been Inserted to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks" );
                    }
                    if ( DataIsEvenlySpaced )
                    {
                        double num4 = num3 - num1;
                        if ( num4 < 0.0 )
                            num4 = -num4;
                        if ( num4 > visibleXdiffError )
                        {
                            DataIsEvenlySpaced = false;
                            break;
                        }
                    }
                }
            }
            else
            {
                DataIsEvenlySpaced = false;
                if ( !DataIsSortedAscending )
                    return;
                double num1 = doubleList[indexWhereInserted - 1];
                int num2 = indexWhereInserted + insertedCount + 1;
                for ( int index = indexWhereInserted ; index < num2 ; ++index )
                {
                    double num3 = doubleList[index];
                    if ( num3 < num1 )
                    {
                        DataIsSortedAscending = false;
                        if ( DataIsSortedAscending || acceptsUnsortedData )
                            break;
                        throw new InvalidOperationException( "Data has been Inserted to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks" );
                    }
                    num1 = num3;
                }
            }
        }

        private void UpdateDataDistributionFlagsWhenAppendedXValue( ISeriesColumn<double> xValues, double newXValue, int lastIndexAfterAppending, bool acceptsUnsortedData )
        {
            if ( !DataIsSortedAscending && !DataIsEvenlySpaced )
                return;
            if ( lastIndexAfterAppending > 0 )
            {
                double num1 = DoubleDataDistributionCalculator.TxToDouble(newXValue) - _lastXValue;
                if ( num1 < 0.0 )
                {
                    DataIsSortedAscending = false;
                    DataIsEvenlySpaced = false;
                    if ( DataIsSortedAscending || acceptsUnsortedData )
                        return;
                    throw new InvalidOperationException( "Data has been Appended to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks" );
                }
                if ( DataIsEvenlySpaced )
                {
                    if ( _firstXDiffIsValid )
                    {
                        double num2 = num1 - _firstXDiff;
                        if ( num2 < 0.0 )
                            num2 = -num2;
                        if ( num2 > _minVisibleXDiffError )
                            DataIsEvenlySpaced = false;
                    }
                    else
                    {
                        _firstXDiffIsValid = true;
                        _firstXDiff = num1;
                        _minVisibleXDiffError = num1 * 0.000125;
                    }
                }
            }
            _lastXValue = DoubleDataDistributionCalculator.TxToDouble( newXValue );
        }

        private void UpdateDataDistributionFlagsWhenAppendedDataArray( IList<double> xValues, int countBeforeAppending, double[ ] newXValues, int newXValuesLength, bool acceptsUnsortedData )
        {
            if ( !DataIsSortedAscending && !DataIsEvenlySpaced )
                return;
            int num1 = newXValuesLength;
            double visibleXdiffError = _minVisibleXDiffError;
            if ( countBeforeAppending > 0 && newXValuesLength > 0 )
            {
                double num2 = DoubleDataDistributionCalculator.TxToDouble(newXValues[0]) - _lastXValue;
                if ( DataIsSortedAscending && _lastXValue > DoubleDataDistributionCalculator.TxToDouble( newXValues[ 0 ] ) )
                {
                    DataIsSortedAscending = false;
                    if ( !DataIsSortedAscending && !acceptsUnsortedData )
                        throw new InvalidOperationException( "Data has been Appended to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks" );
                }
                if ( DataIsEvenlySpaced )
                {
                    if ( _firstXDiffIsValid && newXValuesLength > 0 )
                    {
                        double num3 = num2 - _firstXDiff;
                        if ( num3 < 0.0 )
                            num3 = -num3;
                        if ( num3 > _minVisibleXDiffError )
                            DataIsEvenlySpaced = false;
                    }
                    else
                    {
                        _firstXDiff = num2;
                        _firstXDiffIsValid = true;
                        _minVisibleXDiffError = num2 * 0.000125;
                    }
                }
            }
            if ( !false )
            {
                double num2 = DoubleDataDistributionCalculator.TxToDouble(newXValues[0]);
                double num3 = _firstXDiff;
                bool flag = _firstXDiffIsValid;
                for ( int index = 1 ; index < num1 ; ++index )
                {
                    double num4 = DoubleDataDistributionCalculator.TxToDouble(newXValues[index]);
                    double num5 = num4 - num2;
                    if ( num5 < 0.0 )
                    {
                        DataIsSortedAscending = false;
                        DataIsEvenlySpaced = false;
                        if ( !DataIsSortedAscending && !acceptsUnsortedData )
                            throw new InvalidOperationException( "Data has been Appended to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks" );
                        break;
                    }
                    num2 = num4;
                    if ( DataIsEvenlySpaced )
                    {
                        if ( flag )
                        {
                            double num6 = num5 - num3;
                            if ( num6 < 0.0 )
                                num6 = -num6;
                            if ( num6 > visibleXdiffError )
                                DataIsEvenlySpaced = false;
                        }
                        else
                        {
                            num3 = num5;
                            flag = true;
                        }
                    }
                }
                _lastXValue = num2;
                _firstXDiffIsValid = flag;
                _firstXDiff = num3;
            }
            _lastXValue = DoubleDataDistributionCalculator.TxToDouble( newXValues[ num1 - 1 ] );
            if ( _firstXDiffIsValid || num1 <= 1 )
                return;
            _firstXDiff = _lastXValue - DoubleDataDistributionCalculator.TxToDouble( newXValues[ num1 - 2 ] );
            _firstXDiffIsValid = true;
            _minVisibleXDiffError = _firstXDiff * 0.000125;
        }

        private static double TxToDouble( double xValue )
        {
            return xValue;
        }
    }
}
