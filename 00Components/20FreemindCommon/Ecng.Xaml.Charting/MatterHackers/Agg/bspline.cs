// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.bspline
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal sealed class bspline
    {
        private ArrayPOD<double> m_am = new ArrayPOD<double>(16);
        private int m_max;
        private int m_num;
        private int m_xOffset;
        private int m_yOffset;
        private int m_last_idx;

        public bspline()
        {
            this.m_max = 0;
            this.m_num = 0;
            this.m_xOffset = 0;
            this.m_yOffset = 0;
            this.m_last_idx = -1;
        }

        public bspline( int num )
        {
            this.m_max = 0;
            this.m_num = 0;
            this.m_xOffset = 0;
            this.m_yOffset = 0;
            this.m_last_idx = -1;
            this.init( num );
        }

        public bspline( int num, double[ ] x, double[ ] y )
        {
            this.m_max = 0;
            this.m_num = 0;
            this.m_xOffset = 0;
            this.m_yOffset = 0;
            this.m_last_idx = -1;
            this.init( num, x, y );
        }

        public void init( int max )
        {
            if ( max > 2 && max > this.m_max )
            {
                this.m_am.Resize( max * 3 );
                this.m_max = max;
                this.m_xOffset = this.m_max;
                this.m_yOffset = this.m_max * 2;
            }
            this.m_num = 0;
            this.m_last_idx = -1;
        }

        public void add_point( double x, double y )
        {
            if ( this.m_num >= this.m_max )
                return;
            this.m_am[ this.m_xOffset + this.m_num ] = x;
            this.m_am[ this.m_yOffset + this.m_num ] = y;
            ++this.m_num;
        }

        public void prepare()
        {
            if ( this.m_num > 2 )
            {
                for ( int index = 0 ; index < this.m_num ; ++index )
                    this.m_am[ index ] = 0.0;
                int size = 3 * this.m_num;
                ArrayPOD<double> arrayPod = new ArrayPOD<double>(size);
                for ( int index = 0 ; index < size ; ++index )
                    arrayPod[ index ] = 0.0;
                int num1 = this.m_num;
                int num2 = this.m_num * 2;
                int index1 = this.m_num - 1;
                double num3 = this.m_am[this.m_xOffset + 1] - this.m_am[this.m_xOffset];
                double num4 = (this.m_am[this.m_yOffset + 1] - this.m_am[this.m_yOffset]) / num3;
                for ( int index2 = 1 ; index2 < index1 ; ++index2 )
                {
                    double num5 = num3;
                    num3 = this.m_am[ this.m_xOffset + index2 + 1 ] - this.m_am[ this.m_xOffset + index2 ];
                    double num6 = num4;
                    num4 = ( this.m_am[ this.m_yOffset + index2 + 1 ] - this.m_am[ this.m_yOffset + index2 ] ) / num3;
                    arrayPod[ index2 ] = num3 / ( num3 + num5 );
                    arrayPod[ num1 + index2 ] = 1.0 - arrayPod[ index2 ];
                    arrayPod[ num2 + index2 ] = 6.0 * ( num4 - num6 ) / ( num5 + num3 );
                }
                for ( int index2 = 1 ; index2 < index1 ; ++index2 )
                {
                    double num5 = 1.0 / (arrayPod[num1 + index2] * arrayPod[index2 - 1] + 2.0);
                    arrayPod[ index2 ] *= -num5;
                    arrayPod[ num2 + index2 ] = ( arrayPod[ num2 + index2 ] - arrayPod[ num1 + index2 ] * arrayPod[ num2 + index2 - 1 ] ) * num5;
                }
                this.m_am[ index1 ] = 0.0;
                arrayPod[ index1 - 1 ] = arrayPod[ num2 + index1 - 1 ];
                this.m_am[ index1 - 1 ] = arrayPod[ index1 - 1 ];
                int index3 = index1 - 2;
                int num7 = 0;
                while ( num7 < this.m_num - 2 )
                {
                    arrayPod[ index3 ] = arrayPod[ index3 ] * arrayPod[ index3 + 1 ] + arrayPod[ num2 + index3 ];
                    this.m_am[ index3 ] = arrayPod[ index3 ];
                    ++num7;
                    --index3;
                }
            }
            this.m_last_idx = -1;
        }

        public void init( int num, double[ ] x, double[ ] y )
        {
            if ( num > 2 )
            {
                this.init( num );
                for ( int index = 0 ; index < num ; ++index )
                    this.add_point( x[ index ], y[ index ] );
                this.prepare();
            }
            this.m_last_idx = -1;
        }

        private void bsearch( int n, int xOffset, double x0, out int i )
        {
            int num1 = n - 1;
            i = 0;
            while ( num1 - i > 1 )
            {
                int num2 = i + num1 >> 1;
                if ( x0 < this.m_am[ xOffset + num2 ] )
                    num1 = num2;
                else
                    i = num2;
            }
        }

        private double interpolation( double x, int i )
        {
            int index = i + 1;
            double num1 = this.m_am[this.m_xOffset + i] - this.m_am[this.m_xOffset + index];
            double num2 = x - this.m_am[this.m_xOffset + index];
            double num3 = this.m_am[this.m_xOffset + i] - x;
            double num4 = num1 * num1 / 6.0;
            return ( this.m_am[ index ] * num3 * num3 * num3 + this.m_am[ i ] * num2 * num2 * num2 ) / 6.0 / num1 + ( ( this.m_am[ this.m_yOffset + index ] - this.m_am[ index ] * num4 ) * num3 + ( this.m_am[ this.m_yOffset + i ] - this.m_am[ i ] * num4 ) * num2 ) / num1;
        }

        private double extrapolation_left( double x )
        {
            double num = this.m_am[this.m_xOffset + 1] - this.m_am[this.m_xOffset];
            return ( -num * this.m_am[ 1 ] / 6.0 + ( this.m_am[ this.m_yOffset + 1 ] - this.m_am[ this.m_yOffset ] ) / num ) * ( x - this.m_am[ this.m_xOffset ] ) + this.m_am[ this.m_yOffset ];
        }

        private double extrapolation_right( double x )
        {
            double num = this.m_am[this.m_xOffset + this.m_num - 1] - this.m_am[this.m_xOffset + this.m_num - 2];
            return ( num * this.m_am[ this.m_num - 2 ] / 6.0 + ( this.m_am[ this.m_yOffset + this.m_num - 1 ] - this.m_am[ this.m_yOffset + this.m_num - 2 ] ) / num ) * ( x - this.m_am[ this.m_xOffset + this.m_num - 1 ] ) + this.m_am[ this.m_yOffset + this.m_num - 1 ];
        }

        public double get( double x )
        {
            if ( this.m_num <= 2 )
                return 0.0;
            if ( x < this.m_am[ this.m_xOffset ] )
                return this.extrapolation_left( x );
            if ( x >= this.m_am[ this.m_xOffset + this.m_num - 1 ] )
                return this.extrapolation_right( x );
            int i;
            this.bsearch( this.m_num, this.m_xOffset, x, out i );
            return this.interpolation( x, i );
        }

        public double get_stateful( double x )
        {
            if ( this.m_num <= 2 )
                return 0.0;
            if ( x < this.m_am[ this.m_xOffset ] )
                return this.extrapolation_left( x );
            if ( x >= this.m_am[ this.m_xOffset + this.m_num - 1 ] )
                return this.extrapolation_right( x );
            if ( this.m_last_idx >= 0 )
            {
                if ( x < this.m_am[ this.m_xOffset + this.m_last_idx ] || x > this.m_am[ this.m_xOffset + this.m_last_idx + 1 ] )
                {
                    if ( this.m_last_idx < this.m_num - 2 && x >= this.m_am[ this.m_xOffset + this.m_last_idx + 1 ] && x <= this.m_am[ this.m_xOffset + this.m_last_idx + 2 ] )
                        ++this.m_last_idx;
                    else if ( this.m_last_idx > 0 && x >= this.m_am[ this.m_xOffset + this.m_last_idx - 1 ] && x <= this.m_am[ this.m_xOffset + this.m_last_idx ] )
                        --this.m_last_idx;
                    else
                        this.bsearch( this.m_num, this.m_xOffset, x, out this.m_last_idx );
                }
                return this.interpolation( x, this.m_last_idx );
            }
            this.bsearch( this.m_num, this.m_xOffset, x, out this.m_last_idx );
            return this.interpolation( x, this.m_last_idx );
        }
    }
}
