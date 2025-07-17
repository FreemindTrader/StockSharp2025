// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.FloatRange
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;

namespace StockSharp.Xaml.Charting
{
    public class FloatRange : Range<float>
    {
        public FloatRange()
        {
        }

        public FloatRange( float min, float max )
          : base( min, max )
        {
        }

        public override object Clone()
        {
            return ( object ) new FloatRange( this.Min, this.Max );
        }

        public override float Diff
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
                return ( double ) Math.Abs( this.Max - this.Min ) < double.Epsilon;
            }
        }

        public override DoubleRange AsDoubleRange()
        {
            return new DoubleRange( ( double ) this.Min, ( double ) this.Max );
        }

        public override IRange<float> SetMinMax( double min, double max )
        {
            this.SetMinMaxInternal( ( float ) min, ( float ) max );
            return ( IRange<float> ) this;
        }

        public override IRange<float> SetMinMax( double min, double max, IRange<float> maxRange )
        {
            this.Min = Math.Max( ( float ) min, maxRange.Min );
            this.Max = Math.Min( ( float ) max, maxRange.Max );
            return ( IRange<float> ) this;
        }

        public override IRange<float> GrowBy( double minFraction, double maxFraction )
        {
            float diff = this.Diff;
            if ( ( double ) Math.Abs( diff ) < double.Epsilon )
            {
                this.Max += this.Max * ( float ) maxFraction;
                this.Min -= this.Min * ( float ) minFraction;
                return ( IRange<float> ) this;
            }
            this.Max += diff * ( float ) maxFraction;
            this.Min -= diff * ( float ) minFraction;
            return ( IRange<float> ) this;
        }

        public override IRange<float> ClipTo( IRange<float> maximumRange )
        {
            this.Max = Math.Min( this.Max, maximumRange.Max );
            this.Min = Math.Max( this.Min, maximumRange.Min );
            return ( IRange<float> ) this;
        }
    }
}
