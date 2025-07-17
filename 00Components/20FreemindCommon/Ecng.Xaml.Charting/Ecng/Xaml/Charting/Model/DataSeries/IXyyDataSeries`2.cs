// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Model.DataSeries.IXyyDataSeries`2
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using StockSharp.Xaml.Charting.Visuals;

namespace StockSharp.Xaml.Charting.Model.DataSeries
{
    public interface IXyyDataSeries<TX, TY> : IDataSeries<TX, TY>, IDataSeries, ISuspendable, IXyyDataSeries where TX : IComparable where TY : IComparable
    {
        IList<TY> Y1Values
        {
            get;
        }

        void Append( TX x, TY y0, TY y1 );

        void Append( IEnumerable<TX> x, IEnumerable<TY> y0, IEnumerable<TY> y1 );

        void Update( TX x, TY y0, TY y1 );

        void Insert( int index, TX x, TY y0, TY y1 );

        void InsertRange( int startIndex, IEnumerable<TX> x, IEnumerable<TY> y0, IEnumerable<TY> y1 );
    }
}
