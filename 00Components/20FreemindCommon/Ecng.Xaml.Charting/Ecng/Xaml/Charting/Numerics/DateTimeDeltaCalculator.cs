// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Numerics.DateTimeDeltaCalculator
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using Ecng.Xaml.Charting.Common.Extensions;

namespace Ecng.Xaml.Charting.Numerics
{
    internal class DateTimeDeltaCalculator : TimeSpanDeltaCalculatorBase
    {
        private static DateTimeDeltaCalculator _instance;

        internal static DateTimeDeltaCalculator Instance
        {
            get
            {
                return DateTimeDeltaCalculator._instance ?? ( DateTimeDeltaCalculator._instance = new DateTimeDeltaCalculator() );
            }
        }

        protected DateTimeDeltaCalculator()
        {
        }

        protected override long GetTicks( IComparable value )
        {
            return value.ToDateTime().Ticks;
        }
    }
}
