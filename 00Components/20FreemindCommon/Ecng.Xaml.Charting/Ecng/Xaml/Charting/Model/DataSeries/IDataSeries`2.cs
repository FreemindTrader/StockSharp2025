// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Model.DataSeries.IDataSeries`2
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
namespace Ecng.Xaml.Charting
{
    public interface IDataSeries<TX, TY> : IDataSeries, ISuspendable where TX : IComparable where TY : IComparable
    {
        IList<TX> XValues
        {
            get;
        }

        IList<TY> YValues
        {
            get;
        }

        void Append( TX x, params TY[ ] yValues );

        void Append( IEnumerable<TX> x, params IEnumerable<TY>[ ] yValues );

        void Remove( TX x );

        void RemoveAt( int index );

        void RemoveRange( int startIndex, int count );

        IDataSeries<TX, TY> Clone();

        TY GetYMinAt( int index, TY existingYMin );

        TY GetYMaxAt( int index, TY existingYMax );
    }
}
