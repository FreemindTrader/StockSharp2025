// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Model.DataSeries.IXyDataSeries`2
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Collections.Generic;
namespace fx.Xaml.Charting
{
    public interface IXyDataSeries<TX, TY> : IDataSeries<TX, TY>, IDataSeries, ISuspendable, IXyDataSeries where TX : IComparable where TY : IComparable
    {
        void Append( TX x, TY y );

        void Append( IEnumerable<TX> x, IEnumerable<TY> y );

        void Update( TX x, TY y );

        void Insert( int index, TX x, TY y );

        void InsertRange( int startIndex, IEnumerable<TX> x, IEnumerable<TY> y );
    }
}
