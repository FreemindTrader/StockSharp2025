// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.Transform.Bilinear
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace MatterHackers.Agg.Transform
{
    internal sealed class Bilinear : ITransform
    {
        private double[,] m_mtx = new double[4, 2];
        private bool m_valid;

        public Bilinear()
        {
            this.m_valid = false;
        }

        public Bilinear( double[ ] src, double[ ] dst )
        {
            this.quad_to_quad( src, dst );
        }

        public Bilinear( double x1, double y1, double x2, double y2, double[ ] quad )
        {
            this.rect_to_quad( x1, y1, x2, y2, quad );
        }

        public Bilinear( double[ ] quad, double x1, double y1, double x2, double y2 )
        {
            this.quad_to_rect( quad, x1, y1, x2, y2 );
        }

        public void quad_to_quad( double[ ] src, double[ ] dst )
        {
            double[,] left = new double[4, 4];
            double[,] right = new double[4, 2];
            for ( uint index = 0 ; index < 4U ; ++index )
            {
                uint num1 = index * 2U;
                uint num2 = num1 + 1U;
                left[ ( int ) index, 0 ] = 1.0;
                left[ ( int ) index, 1 ] = src[ ( int ) num1 ] * src[ ( int ) num2 ];
                left[ ( int ) index, 2 ] = src[ ( int ) num1 ];
                left[ ( int ) index, 3 ] = src[ ( int ) num2 ];
                right[ ( int ) index, 0 ] = dst[ ( int ) num1 ];
                right[ ( int ) index, 1 ] = dst[ ( int ) num2 ];
            }
            this.m_valid = simul_eq.solve( left, right, this.m_mtx );
        }

        public void rect_to_quad( double x1, double y1, double x2, double y2, double[ ] quad )
        {
            double[] src = new double[8];
            src[ 0 ] = src[ 6 ] = x1;
            src[ 2 ] = src[ 4 ] = x2;
            src[ 1 ] = src[ 3 ] = y1;
            src[ 5 ] = src[ 7 ] = y2;
            this.quad_to_quad( src, quad );
        }

        public void quad_to_rect( double[ ] quad, double x1, double y1, double x2, double y2 )
        {
            double[] dst = new double[8];
            dst[ 0 ] = dst[ 6 ] = x1;
            dst[ 2 ] = dst[ 4 ] = x2;
            dst[ 1 ] = dst[ 3 ] = y1;
            dst[ 5 ] = dst[ 7 ] = y2;
            this.quad_to_quad( quad, dst );
        }

        public bool is_valid()
        {
            return this.m_valid;
        }

        public void transform( ref double x, ref double y )
        {
            double num1 = x;
            double num2 = y;
            double num3 = num1 * num2;
            x = this.m_mtx[ 0, 0 ] + this.m_mtx[ 1, 0 ] * num3 + this.m_mtx[ 2, 0 ] * num1 + this.m_mtx[ 3, 0 ] * num2;
            y = this.m_mtx[ 0, 1 ] + this.m_mtx[ 1, 1 ] * num3 + this.m_mtx[ 2, 1 ] * num1 + this.m_mtx[ 3, 1 ] * num2;
        }

        public Bilinear.iterator_x begin( double x, double y, double step )
        {
            return new Bilinear.iterator_x( x, y, step, this.m_mtx );
        }

        internal sealed class iterator_x
        {
            private double inc_x;
            private double inc_y;
            public double x;
            public double y;

            public iterator_x()
            {
            }

            public iterator_x( double tx, double ty, double step, double[ , ] m )
            {
                this.inc_x = m[ 1, 0 ] * step * ty + m[ 2, 0 ] * step;
                this.inc_y = m[ 1, 1 ] * step * ty + m[ 2, 1 ] * step;
                this.x = m[ 0, 0 ] + m[ 1, 0 ] * tx * ty + m[ 2, 0 ] * tx + m[ 3, 0 ] * ty;
                this.y = m[ 0, 1 ] + m[ 1, 1 ] * tx * ty + m[ 2, 1 ] * tx + m[ 3, 1 ] * ty;
            }

            public static Bilinear.iterator_x operator ++( Bilinear.iterator_x a )
            {
                a.x += a.inc_x;
                a.y += a.inc_y;
                return a;
            }
        }
    }
}
