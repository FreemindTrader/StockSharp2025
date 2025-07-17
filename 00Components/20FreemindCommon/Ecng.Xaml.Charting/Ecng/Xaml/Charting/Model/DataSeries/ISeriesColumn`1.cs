// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Model.DataSeries.ISeriesColumn`1
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Collections;
using System.Collections.Generic;

namespace Ecng.Xaml.Charting.Model.DataSeries
{
    public interface ISeriesColumn<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, ISeriesColumn
    {
        T GetMinimum();

        T GetMaximum();

        void AddRange( IEnumerable<T> values );

        void InsertRange( int startIndex, IEnumerable<T> values );

        void RemoveRange( int startIndex, int count );
    }
}
