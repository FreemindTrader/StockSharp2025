// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Model.DataSeries.IHlcDataSeries`2
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using Ecng.Xaml.Charting.Visuals;

namespace Ecng.Xaml.Charting.Model.DataSeries
{
    public interface IHlcDataSeries<TX, TY> : IDataSeries<TX, TY>, IDataSeries, ISuspendable, IHlcDataSeries where TX : IComparable where TY : IComparable
    {
        IList<TY> HighValues
        {
            get;
        }

        IList<TY> LowValues
        {
            get;
        }
    }
}
