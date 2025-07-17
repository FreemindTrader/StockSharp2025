// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.Axes.DateTimeTickProvider
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Utility;

namespace StockSharp.Xaml.Charting.Visuals.Axes
{
    public class DateTimeTickProvider : TimeSpanTickProviderBase
    {
        protected override double GetTicks( IComparable value )
        {
            return ( double ) value.ToDateTime().Ticks;
        }

        protected override IComparable RoundUp( IComparable current, TimeSpan delta )
        {
            return ( IComparable ) DateUtil.RoundUp( current.ToDateTime(), delta );
        }

        protected override bool IsAdditionValid( IComparable current, TimeSpan delta )
        {
            return current.ToDateTime().IsAdditionValid( delta );
        }

        protected override IComparable AddDelta( IComparable current, TimeSpan delta )
        {
            return ( IComparable ) current.ToDateTime().AddDelta( delta );
        }

        protected override bool IsDivisibleBy( IComparable current, TimeSpan delta )
        {
            return DateUtil.IsDivisibleBy( current.ToDateTime(), delta );
        }
    }
}
