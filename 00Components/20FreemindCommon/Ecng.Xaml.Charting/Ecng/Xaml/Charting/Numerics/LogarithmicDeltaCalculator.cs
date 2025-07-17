// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Numerics.LogarithmicDeltaCalculator
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace Ecng.Xaml.Charting
{
    internal class LogarithmicDeltaCalculator : NumericDeltaCalculatorBase
    {
        private static NumericDeltaCalculatorBase _instance;

        internal static NumericDeltaCalculatorBase Instance
        {
            get
            {
                return LogarithmicDeltaCalculator._instance ?? ( LogarithmicDeltaCalculator._instance = ( NumericDeltaCalculatorBase ) new LogarithmicDeltaCalculator() );
            }
        }

        protected LogarithmicDeltaCalculator()
        {
            this.LogarithmicBase = 10.0;
        }

        public double LogarithmicBase
        {
            get; set;
        }

        protected override INiceScale GetScale( double min, double max, int minorsPerMajor, uint maxTicks )
        {
            return ( INiceScale ) new NiceLogScale( min, max, this.LogarithmicBase, minorsPerMajor, maxTicks );
        }
    }
}
