// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Numerics.NumericDeltaCalculatorBase
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using Ecng.Xaml.Charting.Common.Extensions;
using Ecng.Xaml.Charting.Utility;

namespace Ecng.Xaml.Charting.Numerics
{
    internal abstract class NumericDeltaCalculatorBase : IDeltaCalculator
    {
        private const uint DefaultTicksCount = 10;

        public IAxisDelta<double> GetDeltaFromRange( double min, double max, int minorsPerMajor, uint maxTicks = 10 )
        {
            Guard.ArgumentIsRealNumber( min );
            Guard.ArgumentIsRealNumber( max );
            INiceScale scale = this.GetScale(min, max, minorsPerMajor, maxTicks);
            scale.CalculateDelta();
            return ( IAxisDelta<double> ) scale.TickSpacing;
        }

        protected virtual INiceScale GetScale( double min, double max, int minorsPerMajor, uint maxTicks )
        {
            return ( INiceScale ) new NiceDoubleScale( min, max, minorsPerMajor, maxTicks );
        }

        IAxisDelta IDeltaCalculator.GetDeltaFromRange( IComparable min, IComparable max, int minorsPerMajor, uint maxTicks )
        {
            return ( IAxisDelta ) this.GetDeltaFromRange( min.ToDouble(), max.ToDouble(), minorsPerMajor, maxTicks );
        }
    }
}
