// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Numerics.NiceDoubleScale
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
namespace fx.Xaml.Charting
{
    internal class NiceDoubleScale : INiceScale
    {
        private double _range = double.NaN;
        protected static uint _maxTicks;
        protected readonly int _minorsPerMajor;
        protected double _minDelta;
        protected double _maxDelta;
        protected DoubleRange _niceRange;
        private double _tickSpacing;

        internal NiceDoubleScale( double min, double max, int minorsPerMajor, uint maxTicks = 10 )
        {
            if ( minorsPerMajor < 1 )
                throw new ArgumentException( "MinorsPerMajor must be greater than or equal to 2" );
            this._minDelta = min;
            this._maxDelta = max;
            this._minorsPerMajor = minorsPerMajor;
            NiceDoubleScale._maxTicks = maxTicks;
        }

        public DoubleAxisDelta TickSpacing
        {
            get
            {
                return new DoubleAxisDelta( this._minDelta, this._maxDelta );
            }
        }

        internal DoubleRange NiceRange
        {
            get
            {
                return this._niceRange;
            }
        }

        public virtual void CalculateDelta()
        {
            uint num = (uint) Math.Max((int) NiceDoubleScale._maxTicks - 1, 1);
            this._range = this.NiceNum( this._maxDelta - this._minDelta, false );
            this._tickSpacing = this.NiceNum( this._range / ( double ) num, true );
            double min = Math.Floor(this._minDelta / this._tickSpacing) * this._tickSpacing;
            double max = Math.Ceiling(this._maxDelta / this._tickSpacing) * this._tickSpacing;
            this._maxDelta = this._tickSpacing;
            this._minDelta = this._tickSpacing / ( double ) this._minorsPerMajor;
            this._niceRange = new DoubleRange( min, max );
        }

        protected virtual double NiceNum( double range, bool round )
        {
            double y = range > 0.0 ? Math.Floor(Math.Log10(range)) : 0.0;
            double num = (range / Math.Pow(10.0, y)).RoundOff(1, MidpointRounding.AwayFromZero);
            return ( !round ? ( num > 1.0 ? ( num > 2.0 ? ( num > 5.0 ? 10.0 : 5.0 ) : 2.0 ) : 1.0 ) : ( num >= 1.5 ? ( num >= 3.0 ? ( num >= 7.0 ? 10.0 : 5.0 ) : 2.0 ) : 1.0 ) ) * Math.Pow( 10.0, y );
        }
    }
}
