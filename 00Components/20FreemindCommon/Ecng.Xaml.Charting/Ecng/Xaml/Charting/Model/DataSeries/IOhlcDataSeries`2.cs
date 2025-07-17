// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Model.DataSeries.IOhlcDataSeries`2
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using StockSharp.Xaml.Charting.Visuals;

namespace StockSharp.Xaml.Charting.Model.DataSeries
{
    public interface IOhlcDataSeries<TX, TY> : IDataSeries<TX, TY>, IDataSeries, ISuspendable, IOhlcDataSeries where TX : IComparable where TY : IComparable
    {
        IList<TY> OpenValues
        {
            get;
        }

        IList<TY> HighValues
        {
            get;
        }

        IList<TY> LowValues
        {
            get;
        }

        IList<TY> CloseValues
        {
            get;
        }

        void Append( TX x, TY open, TY high, TY low, TY close );

        void Append( IEnumerable<TX> x, IEnumerable<TY> open, IEnumerable<TY> high, IEnumerable<TY> low, IEnumerable<TY> close );

        void Update( TX x, TY open, TY high, TY low, TY close );

        void Insert( int index, TX x, TY open, TY high, TY low, TY close );

        void InsertRange( int startIndex, IEnumerable<TX> x, IEnumerable<TY> open, IEnumerable<TY> high, IEnumerable<TY> low, IEnumerable<TY> close );
    }
}
