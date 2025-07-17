// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Numerics.NiceLongScale
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using StockSharp.Xaml.Charting.Common.Extensions;

namespace StockSharp.Xaml.Charting.Numerics
{
    internal class NiceLongScale
    {
        private static uint _maxTicks;
        private readonly long _min;
        private readonly long _max;
        private readonly int _minorsPerMajor;
        private long _tickSpacing;
        private Tuple<long, long> _niceRange;

        internal NiceLongScale( long min, long max, int minorsPerMajor, uint maxTicks = 10 )
        {
            if ( minorsPerMajor < 1 )
                throw new ArgumentException( "MinorsPerMajor must be greater than or equal to 2" );
            this._min = min;
            this._max = max;
            this._minorsPerMajor = minorsPerMajor;
            NiceLongScale._maxTicks = maxTicks;
            this.Calculate();
        }

        public Tuple<long, long> TickSpacing
        {
            get
            {
                return new Tuple<long, long>( this._tickSpacing / ( long ) this._minorsPerMajor, this._tickSpacing );
            }
        }

        internal Tuple<long, long> NiceRange
        {
            get
            {
                return this._niceRange;
            }
        }

        private void Calculate()
        {
            long range = this.NiceNum(this._max - this._min, false) / (long) (uint) Math.Max((int) NiceLongScale._maxTicks - 1, 1);
            this._tickSpacing = range > 0L ? this.NiceNum( range, true ) : 1L;
            this._niceRange = Tuple.Create<long, long>( this._min / this._tickSpacing * this._tickSpacing, this._max / this._tickSpacing * this._tickSpacing );
        }

        private long NiceNum( long range, bool round )
        {
            double y = Math.Floor(Math.Log10((double) range));
            double num = ((double) range / Math.Pow(10.0, y)).RoundOff(1, MidpointRounding.AwayFromZero);
            return ( !round ? ( num > 1.0 ? ( num > 2.0 ? ( num > 5.0 ? 10L : 5L ) : 2L ) : 1L ) : ( num >= 1.5 ? ( num >= 3.0 ? ( num >= 7.0 ? 10L : 5L ) : 2L ) : 1L ) ) * ( long ) Math.Pow( 10.0, y );
        }
    }
}
