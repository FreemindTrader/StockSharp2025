// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Numerics.DateTimeDeltaCalculator
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
namespace fx.Xaml.Charting
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
