// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Common.Extensions.DecimalExtensions
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;

namespace fx.Xaml.Charting
{
    internal static class DecimalExtensions
    {
        internal static Decimal RoundOff( this Decimal d )
        {
            return Decimal.Round( d, 0 );
        }

        internal static Decimal RoundOff( this Decimal d, MidpointRounding mode )
        {
            return d.RoundOff( 0, mode );
        }

        internal static Decimal RoundOff( this Decimal d, int decimals, MidpointRounding mode )
        {
            if ( mode == MidpointRounding.ToEven )
                return Decimal.Round( d, decimals );
            Decimal num1 = Convert.ToDecimal(Math.Pow(10.0, (double) decimals));
            int num2 = Math.Sign(d);
            return Decimal.Truncate( d * num1 + new Decimal( 5, 0, 0, false, ( byte ) 1 ) * ( Decimal ) num2 ) / num1;
        }

        internal static DateTime ToDateTime( this Decimal d )
        {
            return new DateTime( ( long ) d.RoundOff( MidpointRounding.AwayFromZero ) );
        }
    }
}
