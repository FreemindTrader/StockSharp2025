// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.simul_eq
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Runtime.InteropServices;

namespace MatterHackers.Agg
{
    [StructLayout( LayoutKind.Sequential, Size = 1 )]
    internal struct simul_eq
    {
        public static bool solve( double[ , ] left, double[ , ] right, double[ , ] result )
        {
            if ( left.GetLength( 0 ) != 4 || right.GetLength( 0 ) != 4 || ( left.GetLength( 1 ) != 4 || result.GetLength( 0 ) != 4 ) || ( right.GetLength( 1 ) != 2 || result.GetLength( 1 ) != 2 ) )
                throw new FormatException( "left right and result must all be the same size." );
            int length1 = right.GetLength(0);
            int length2 = right.GetLength(1);
            double[,] m = new double[length1, length1 + length2];
            for ( int index1 = 0 ; index1 < length1 ; ++index1 )
            {
                for ( int index2 = 0 ; index2 < length1 ; ++index2 )
                    m[ index1, index2 ] = left[ index1, index2 ];
                for ( int index2 = 0 ; index2 < length2 ; ++index2 )
                    m[ index1, length1 + index2 ] = right[ index1, index2 ];
            }
            for ( int index1 = 0 ; index1 < length1 ; ++index1 )
            {
                if ( matrix_pivot.pivot( m, ( uint ) index1 ) < 0 )
                    return false;
                double num1 = m[index1, index1];
                for ( int index2 = index1 ; index2 < length1 + length2 ; ++index2 )
                    m[ index1, index2 ] /= num1;
                for ( int index2 = index1 + 1 ; index2 < length1 ; ++index2 )
                {
                    double num2 = m[index2, index1];
                    for ( int index3 = index1 ; index3 < length1 + length2 ; ++index3 )
                        m[ index2, index3 ] -= num2 * m[ index1, index3 ];
                }
            }
            for ( int index1 = 0 ; index1 < length2 ; ++index1 )
            {
                for ( int index2 = length1 - 1 ; index2 >= 0 ; --index2 )
                {
                    result[ index2, index1 ] = m[ index2, length1 + index1 ];
                    for ( int index3 = index2 + 1 ; index3 < length1 ; ++index3 )
                        result[ index2, index1 ] -= m[ index2, index3 ] * result[ index3, index1 ];
                }
            }
            return true;
        }
    }
}
