// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Numerics.NiceLogScale
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;

namespace Ecng.Xaml.Charting.Numerics
{
    internal class NiceLogScale : NiceDoubleScale
    {
        private readonly double _logBase;

        internal NiceLogScale( double min, double max, double logBase, int minorsPerMajor, uint maxTicks = 10 )
          : base( min, max, minorsPerMajor, maxTicks )
        {
            this._logBase = logBase;
        }

        public override void CalculateDelta()
        {
            double num1 = Math.Log(this._maxDelta, this._logBase);
            double y1 = num1 > 0.0 ? Math.Ceiling(num1) : Math.Floor(num1);
            double num2 = Math.Floor(Math.Log(Math.Abs(this._minDelta), this._logBase));
            double y2 = Math.Max(Math.Abs(Math.Sign(this._minDelta) == -1 ? y1 + num2 : y1 - num2), 1.0);
            double y3 = this.NiceNum(this.NiceNum(y1 - num2, false) / (double) NiceDoubleScale._maxTicks, true);
            this._maxDelta = y3;
            this._minDelta = ( Math.Pow( this._logBase, y3 ) - 1.0 ) / ( double ) this._minorsPerMajor;
            double max = Math.Pow(this._logBase, y1);
            this._niceRange = new DoubleRange( max / Math.Pow( this._logBase, y2 ), max );
        }
    }
}
