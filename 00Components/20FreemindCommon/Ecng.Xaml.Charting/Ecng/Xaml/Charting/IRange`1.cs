// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.IRange`1
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.ComponentModel;

namespace Ecng.Xaml.Charting
{
    public interface IRange<T> : IRange, ICloneable, INotifyPropertyChanged where T : IComparable
    {
        T Min
        {
            get; set;
        }

        T Max
        {
            get; set;
        }

        T Diff
        {
            get;
        }

        IRange<T> GrowBy( double minFraction, double maxFraction );

        IRange<T> SetMinMax( double min, double max );

        IRange<T> Union( IRange<T> range );
    }
}
