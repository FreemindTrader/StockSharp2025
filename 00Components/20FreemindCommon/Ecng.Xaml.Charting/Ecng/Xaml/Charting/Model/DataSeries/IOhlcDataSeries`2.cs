// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Model.DataSeries.IOhlcDataSeries`2
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
namespace Ecng.Xaml.Charting
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
