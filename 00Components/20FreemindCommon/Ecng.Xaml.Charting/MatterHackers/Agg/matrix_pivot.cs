// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.matrix_pivot
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg
{
    internal static class matrix_pivot
    {
        private static void swap_arrays_index1( double[ , ] a1, uint a1Index0, double[ , ] a2, uint a2Index0 )
        {
            int length = a1.GetLength(1);
            if ( a2.GetLength( 1 ) != length )
                throw new FormatException( "a1 and a2 must have the same second dimension." );
            for ( int index = 0 ; index < length ; ++index )
            {
                double num = a1[(int) a1Index0, index];
                a1[ ( int ) a1Index0, index ] = a2[ ( int ) a2Index0, index ];
                a2[ ( int ) a2Index0, index ] = num;
            }
        }

        public static int pivot( double[ , ] m, uint row )
        {
            int index1 = (int) row;
            double num1 = -1.0;
            int length = m.GetLength(0);
            for ( int index2 = ( int ) row ; index2 < length ; ++index2 )
            {
                double num2;
                if ( ( num2 = Math.Abs( m[ index2, ( int ) row ] ) ) > num1 && num2 != 0.0 )
                {
                    num1 = num2;
                    index1 = index2;
                }
            }
            if ( m[ index1, ( int ) row ] == 0.0 )
                return -1;
            if ( index1 == ( int ) row )
                return 0;
            matrix_pivot.swap_arrays_index1( m, ( uint ) index1, m, row );
            return index1;
        }
    }
}
