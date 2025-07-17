// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.DecimalRange
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;

namespace StockSharp.Xaml.Charting
{
    public class DecimalRange : Range<Decimal>
    {
        public DecimalRange()
        {
        }

        public DecimalRange( Decimal min, Decimal max )
          : base( min, max )
        {
        }

        public override Decimal Diff
        {
            get
            {
                return this.Max - this.Min;
            }
        }

        public override bool IsZero
        {
            get
            {
                return this.Diff == Decimal.Zero;
            }
        }

        public override object Clone()
        {
            return ( object ) new DecimalRange( this.Min, this.Max );
        }

        public override DoubleRange AsDoubleRange()
        {
            return new DoubleRange( ( double ) this.Min, ( double ) this.Max );
        }

        public override IRange<Decimal> SetMinMax( double min, double max )
        {
            this.SetMinMaxInternal( ( Decimal ) min, ( Decimal ) max );
            return ( IRange<Decimal> ) this;
        }

        public override IRange<Decimal> SetMinMax( double min, double max, IRange<Decimal> maxRange )
        {
            throw new NotImplementedException();
        }

        public override IRange<Decimal> GrowBy( double minFraction, double maxFraction )
        {
            Decimal diff = this.Diff;
            if ( diff == new Decimal( 0, 0, 0, false, ( byte ) 1 ) )
            {
                this.Max = this.Max + this.Max * ( Decimal ) maxFraction;
                this.Min = this.Min - this.Min * ( Decimal ) minFraction;
                return ( IRange<Decimal> ) this;
            }
            this.Max = this.Max + diff * ( Decimal ) maxFraction;
            this.Min = this.Min - diff * ( Decimal ) minFraction;
            return ( IRange<Decimal> ) this;
        }

        public override IRange<Decimal> ClipTo( IRange<Decimal> maximumRange )
        {
            this.Max = Math.Min( this.Max, maximumRange.Max );
            this.Min = Math.Max( this.Min, maximumRange.Min );
            return ( IRange<Decimal> ) this;
        }
    }
}
