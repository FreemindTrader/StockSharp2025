// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Numerics.TimeSpanDeltaCalculator
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using Ecng.Xaml.Charting.Common.Extensions;

namespace Ecng.Xaml.Charting.Numerics
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
