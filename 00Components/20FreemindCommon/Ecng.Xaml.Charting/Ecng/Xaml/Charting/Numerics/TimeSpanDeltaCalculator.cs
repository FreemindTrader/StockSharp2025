// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Numerics.TimeSpanDeltaCalculator
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using StockSharp.Xaml.Charting.Common.Extensions;

namespace StockSharp.Xaml.Charting.Numerics
{
    internal class TimeSpanDeltaCalculator : TimeSpanDeltaCalculatorBase
    {
        private static TimeSpanDeltaCalculator _instance;

        internal static TimeSpanDeltaCalculator Instance
        {
            get
            {
                return TimeSpanDeltaCalculator._instance ?? ( TimeSpanDeltaCalculator._instance = new TimeSpanDeltaCalculator() );
            }
        }

        protected TimeSpanDeltaCalculator()
        {
        }

        protected override long GetTicks( IComparable value )
        {
            return value.ToTimeSpan().Ticks;
        }
    }
}
