// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.IRange
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.ComponentModel;

namespace Ecng.Xaml.Charting
{
    public interface IRange : ICloneable, INotifyPropertyChanged
    {
        IComparable Min
        {
            get; set;
        }

        IComparable Max
        {
            get; set;
        }

        bool IsDefined
        {
            get;
        }

        IComparable Diff
        {
            get;
        }

        bool IsZero
        {
            get;
        }

        DoubleRange AsDoubleRange();

        IRange GrowBy( double minFraction, double maxFraction );

        IRange SetMinMax( double min, double max );

        IRange SetMinMaxWithLimit( double min, double max, IRange maxRange );

        IRange ClipTo( IRange maximumRange );

        IRange ClipTo( IRange maximumRange, RangeClipMode clipMode );

        IRange Union( IRange range );

        bool IsValueWithinRange( IComparable value );
    }
}
