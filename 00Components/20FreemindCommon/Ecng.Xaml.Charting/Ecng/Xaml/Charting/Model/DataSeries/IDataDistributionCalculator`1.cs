// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Model.DataSeries.IDataDistributionCalculator`1
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

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
