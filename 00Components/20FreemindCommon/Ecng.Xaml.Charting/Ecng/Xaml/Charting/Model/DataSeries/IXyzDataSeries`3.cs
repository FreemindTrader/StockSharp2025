// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Model.DataSeries.IXyzDataSeries`3
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
namespace Ecng.Xaml.Charting
{
    public interface IXyzDataSeries<TX, TY, TZ> : IDataSeries<TX, TY>, IDataSeries, ISuspendable, IXyzDataSeries where TX : IComparable where TY : IComparable where TZ : IComparable
    {
        IList<TZ> ZValues
        {
            get;
        }

        void Append( TX x, TY y, TZ z );

        void Append( IEnumerable<TX> x, IEnumerable<TY> y, IEnumerable<TZ> z );

        void Update( TX x, TY y, TZ z );

        void Insert( int index, TX x, TY y, TZ z );

        void InsertRange( int startIndex, IEnumerable<TX> x, IEnumerable<TY> y, IEnumerable<TZ> z );
    }
}
