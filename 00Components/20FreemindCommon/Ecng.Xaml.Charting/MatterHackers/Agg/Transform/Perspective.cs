// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.Transform.Perspective
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg.Transform
{
    internal sealed class Perspective : ITransform
    {
        public static double affine_epsilon = 1E-14;
        public double sx;
        public double shy;
        public double w0;
        public double shx;
        public double sy;
        public double w1;
        public double tx;
        public double ty;
        public double w2;

        public Perspective()
        {
            this.sx = 1.0;
            this.shy = 0.0;
            this.w0 = 0.0;
            this.shx = 0.0;
            this.sy = 1.0;
            this.w1 = 0.0;
            this.tx = 0.0;
            this.ty = 0.0;
            this.w2 = 1.0;
        }

        public Perspective( double v0, double v1, double v2, double v3, double v4, double v5, double v6, double v7, double v8 )
        {
            this.sx = v0;
            this.shy = v1;
            this.w0 = v2;
            this.shx = v3;
            this.sy = v4;
            this.w1 = v5;
            this.tx = v6;
            this.ty = v7;
            this.w2 = v8;
        }

        public Perspective( double[ ] m )
        {
            this.sx = m[ 0 ];
            this.shy = m[ 1 ];
            this.w0 = m[ 2 ];
            this.shx = m[ 3 ];
            this.sy = m[ 4 ];
            this.w1 = m[ 5 ];
            this.tx = m[ 6 ];
            this.ty = m[ 7 ];
            this.w2 = m[ 8 ];
        }

        public Perspective( Affine a )
        {
            this.sx = a.sx;
            this.shy = a.shy;
            this.w0 = 0.0;
            this.shx = a.shx;
            this.sy = a.sy;
            this.w1 = 0.0;
            this.tx = a.tx;
            this.ty = a.ty;
            this.w2 = 1.0;
        }

        public Perspective( Perspective a )
        {
            this.sx = a.sx;
            this.shy = a.shy;
            this.w0 = a.w0;
            this.shx = a.shx;
            this.sy = a.sy;
            this.w1 = a.w1;
            this.tx = a.tx;
            this.ty = a.ty;
            this.w2 = a.w2;
        }

        public Perspective( double x1, double y1, double x2, double y2, double[ ] quad )
        {
            this.rect_to_quad( x1, y1, x2, y2, quad );
        }

        public Perspective( double[ ] quad, double x1, double y1, double x2, double y2 )
        {
            this.quad_to_rect( quad, x1, y1, x2, y2 );
        }

        public Perspective( double[ ] src, double[ ] dst )
        {
            this.quad_to_quad( src, dst );
        }

        public void Set( Perspective Other )
        {
            this.sx = Other.sx;
            this.shy = Other.shy;
            this.w0 = Other.w0;
            this.shx = Other.shx;
            this.sy = Other.sy;
            this.w1 = Other.w1;
            this.tx = Other.tx;
            this.ty = Other.ty;
            this.w2 = Other.w2;
        }

        public bool quad_to_quad( double[ ] qs, double[ ] qd )
        {
            Perspective a = new Perspective();
            if ( !this.quad_to_square( qs ) || !a.square_to_quad( qd ) )
                return false;
            this.multiply( a );
            return true;
        }

        public bool rect_to_quad( double x1, double y1, double x2, double y2, double[ ] q )
        {
            double[] qs = new double[8];
            qs[ 0 ] = qs[ 6 ] = x1;
            qs[ 2 ] = qs[ 4 ] = x2;
            qs[ 1 ] = qs[ 3 ] = y1;
            qs[ 5 ] = qs[ 7 ] = y2;
            return this.quad_to_quad( qs, q );
        }

        public bool quad_to_rect( double[ ] q, double x1, double y1, double x2, double y2 )
        {
            double[] qd = new double[8];
            qd[ 0 ] = qd[ 6 ] = x1;
            qd[ 2 ] = qd[ 4 ] = x2;
            qd[ 1 ] = qd[ 3 ] = y1;
            qd[ 5 ] = qd[ 7 ] = y2;
            return this.quad_to_quad( q, qd );
        }

        public bool square_to_quad( double[ ] q )
        {
            double num1 = q[0] - q[2] + q[4] - q[6];
            double num2 = q[1] - q[3] + q[5] - q[7];
            if ( num1 == 0.0 && num2 == 0.0 )
            {
                this.sx = q[ 2 ] - q[ 0 ];
                this.shy = q[ 3 ] - q[ 1 ];
                this.w0 = 0.0;
                this.shx = q[ 4 ] - q[ 2 ];
                this.sy = q[ 5 ] - q[ 3 ];
                this.w1 = 0.0;
                this.tx = q[ 0 ];
                this.ty = q[ 1 ];
                this.w2 = 1.0;
            }
            else
            {
                double num3 = q[2] - q[4];
                double num4 = q[3] - q[5];
                double num5 = q[6] - q[4];
                double num6 = q[7] - q[5];
                double num7 = num3 * num6 - num5 * num4;
                if ( num7 == 0.0 )
                {
                    this.sx = this.shy = this.w0 = this.shx = this.sy = this.w1 = this.tx = this.ty = this.w2 = 0.0;
                    return false;
                }
                double num8 = (num1 * num6 - num2 * num5) / num7;
                double num9 = (num2 * num3 - num1 * num4) / num7;
                this.sx = q[ 2 ] - q[ 0 ] + num8 * q[ 2 ];
                this.shy = q[ 3 ] - q[ 1 ] + num8 * q[ 3 ];
                this.w0 = num8;
                this.shx = q[ 6 ] - q[ 0 ] + num9 * q[ 6 ];
                this.sy = q[ 7 ] - q[ 1 ] + num9 * q[ 7 ];
                this.w1 = num9;
                this.tx = q[ 0 ];
                this.ty = q[ 1 ];
                this.w2 = 1.0;
            }
            return true;
        }

        public bool quad_to_square( double[ ] q )
        {
            if ( !this.square_to_quad( q ) )
                return false;
            this.invert();
            return true;
        }

        public Perspective from_affine( Affine a )
        {
            this.sx = a.sx;
            this.shy = a.shy;
            this.w0 = 0.0;
            this.shx = a.shx;
            this.sy = a.sy;
            this.w1 = 0.0;
            this.tx = a.tx;
            this.ty = a.ty;
            this.w2 = 1.0;
            return this;
        }

        public Perspective reset()
        {
            this.sx = 1.0;
            this.shy = 0.0;
            this.w0 = 0.0;
            this.shx = 0.0;
            this.sy = 1.0;
            this.w1 = 0.0;
            this.tx = 0.0;
            this.ty = 0.0;
            this.w2 = 1.0;
            return this;
        }

        public bool invert()
        {
            double num1 = this.sy * this.w2 - this.w1 * this.ty;
            double num2 = this.w0 * this.ty - this.shy * this.w2;
            double num3 = this.shy * this.w1 - this.w0 * this.sy;
            double num4 = this.sx * num1 + this.shx * num2 + this.tx * num3;
            if ( num4 == 0.0 )
            {
                this.sx = this.shy = this.w0 = this.shx = this.sy = this.w1 = this.tx = this.ty = this.w2 = 0.0;
                return false;
            }
            double num5 = 1.0 / num4;
            Perspective perspective = new Perspective(this);
            this.sx = num5 * num1;
            this.shy = num5 * num2;
            this.w0 = num5 * num3;
            this.shx = num5 * ( perspective.w1 * perspective.tx - perspective.shx * perspective.w2 );
            this.sy = num5 * ( perspective.sx * perspective.w2 - perspective.w0 * perspective.tx );
            this.w1 = num5 * ( perspective.w0 * perspective.shx - perspective.sx * perspective.w1 );
            this.tx = num5 * ( perspective.shx * perspective.ty - perspective.sy * perspective.tx );
            this.ty = num5 * ( perspective.shy * perspective.tx - perspective.sx * perspective.ty );
            this.w2 = num5 * ( perspective.sx * perspective.sy - perspective.shy * perspective.shx );
            return true;
        }

        public Perspective translate( double x, double y )
        {
            this.tx += x;
            this.ty += y;
            return this;
        }

        public Perspective rotate( double a )
        {
            this.multiply( Affine.NewRotation( a ) );
            return this;
        }

        public Perspective scale( double s )
        {
            this.multiply( Affine.NewScaling( s ) );
            return this;
        }

        public Perspective scale( double x, double y )
        {
            this.multiply( Affine.NewScaling( x, y ) );
            return this;
        }

        public Perspective multiply( Perspective a )
        {
            Perspective perspective = new Perspective(this);
            this.sx = a.sx * perspective.sx + a.shx * perspective.shy + a.tx * perspective.w0;
            this.shx = a.sx * perspective.shx + a.shx * perspective.sy + a.tx * perspective.w1;
            this.tx = a.sx * perspective.tx + a.shx * perspective.ty + a.tx * perspective.w2;
            this.shy = a.shy * perspective.sx + a.sy * perspective.shy + a.ty * perspective.w0;
            this.sy = a.shy * perspective.shx + a.sy * perspective.sy + a.ty * perspective.w1;
            this.ty = a.shy * perspective.tx + a.sy * perspective.ty + a.ty * perspective.w2;
            this.w0 = a.w0 * perspective.sx + a.w1 * perspective.shy + a.w2 * perspective.w0;
            this.w1 = a.w0 * perspective.shx + a.w1 * perspective.sy + a.w2 * perspective.w1;
            this.w2 = a.w0 * perspective.tx + a.w1 * perspective.ty + a.w2 * perspective.w2;
            return this;
        }

        public Perspective multiply( Affine a )
        {
            Perspective perspective = new Perspective(this);
            this.sx = a.sx * perspective.sx + a.shx * perspective.shy + a.tx * perspective.w0;
            this.shx = a.sx * perspective.shx + a.shx * perspective.sy + a.tx * perspective.w1;
            this.tx = a.sx * perspective.tx + a.shx * perspective.ty + a.tx * perspective.w2;
            this.shy = a.shy * perspective.sx + a.sy * perspective.shy + a.ty * perspective.w0;
            this.sy = a.shy * perspective.shx + a.sy * perspective.sy + a.ty * perspective.w1;
            this.ty = a.shy * perspective.tx + a.sy * perspective.ty + a.ty * perspective.w2;
            return this;
        }

        public Perspective premultiply( Perspective b )
        {
            Perspective perspective = new Perspective(this);
            this.sx = perspective.sx * b.sx + perspective.shx * b.shy + perspective.tx * b.w0;
            this.shx = perspective.sx * b.shx + perspective.shx * b.sy + perspective.tx * b.w1;
            this.tx = perspective.sx * b.tx + perspective.shx * b.ty + perspective.tx * b.w2;
            this.shy = perspective.shy * b.sx + perspective.sy * b.shy + perspective.ty * b.w0;
            this.sy = perspective.shy * b.shx + perspective.sy * b.sy + perspective.ty * b.w1;
            this.ty = perspective.shy * b.tx + perspective.sy * b.ty + perspective.ty * b.w2;
            this.w0 = perspective.w0 * b.sx + perspective.w1 * b.shy + perspective.w2 * b.w0;
            this.w1 = perspective.w0 * b.shx + perspective.w1 * b.sy + perspective.w2 * b.w1;
            this.w2 = perspective.w0 * b.tx + perspective.w1 * b.ty + perspective.w2 * b.w2;
            return this;
        }

        public Perspective premultiply( Affine b )
        {
            Perspective perspective = new Perspective(this);
            this.sx = perspective.sx * b.sx + perspective.shx * b.shy;
            this.shx = perspective.sx * b.shx + perspective.shx * b.sy;
            this.tx = perspective.sx * b.tx + perspective.shx * b.ty + perspective.tx;
            this.shy = perspective.shy * b.sx + perspective.sy * b.shy;
            this.sy = perspective.shy * b.shx + perspective.sy * b.sy;
            this.ty = perspective.shy * b.tx + perspective.sy * b.ty + perspective.ty;
            this.w0 = perspective.w0 * b.sx + perspective.w1 * b.shy;
            this.w1 = perspective.w0 * b.shx + perspective.w1 * b.sy;
            this.w2 = perspective.w0 * b.tx + perspective.w1 * b.ty + perspective.w2;
            return this;
        }

        public Perspective multiply_inv( Perspective m )
        {
            Perspective a = m;
            a.invert();
            return this.multiply( a );
        }

        public Perspective trans_perspectivemultiply_inv( Affine m )
        {
            Affine a = m;
            a.invert();
            return this.multiply( a );
        }

        public Perspective premultiply_inv( Perspective m )
        {
            Perspective perspective = m;
            perspective.invert();
            this.Set( perspective.multiply( this ) );
            return this;
        }

        public Perspective premultiply_inv( Affine m )
        {
            Perspective perspective = new Perspective(m);
            perspective.invert();
            this.Set( perspective.multiply( this ) );
            return this;
        }

        public void store_to( double[ ] m )
        {
            m[ 0 ] = this.sx;
            m[ 1 ] = this.shy;
            m[ 2 ] = this.w0;
            m[ 3 ] = this.shx;
            m[ 4 ] = this.sy;
            m[ 5 ] = this.w1;
            m[ 6 ] = this.tx;
            m[ 7 ] = this.ty;
            m[ 8 ] = this.w2;
        }

        public Perspective load_from( double[ ] m )
        {
            this.sx = m[ 0 ];
            this.shy = m[ 1 ];
            this.w0 = m[ 2 ];
            this.shx = m[ 3 ];
            this.sy = m[ 4 ];
            this.w1 = m[ 5 ];
            this.tx = m[ 6 ];
            this.ty = m[ 7 ];
            this.w2 = m[ 8 ];
            return this;
        }

        public static Perspective operator *( Perspective a, Perspective b )
        {
            Perspective perspective = a;
            perspective.multiply( b );
            return perspective;
        }

        public static Perspective operator *( Perspective a, Affine b )
        {
            Perspective perspective = a;
            perspective.multiply( b );
            return perspective;
        }

        public static Perspective operator /( Perspective a, Perspective b )
        {
            Perspective perspective = a;
            perspective.multiply_inv( b );
            return perspective;
        }

        public static Perspective operator ~( Perspective b )
        {
            Perspective perspective = b;
            perspective.invert();
            return perspective;
        }

        public static bool operator ==( Perspective a, Perspective b )
        {
            return a.is_equal( b, Perspective.affine_epsilon );
        }

        public static bool operator !=( Perspective a, Perspective b )
        {
            return !a.is_equal( b, Perspective.affine_epsilon );
        }

        public override bool Equals( object obj )
        {
            return base.Equals( obj );
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public void transform( ref double px, ref double py )
        {
            double num1 = px;
            double num2 = py;
            double num3 = 1.0 / (num1 * this.w0 + num2 * this.w1 + this.w2);
            px = num3 * ( num1 * this.sx + num2 * this.shx + this.tx );
            py = num3 * ( num1 * this.shy + num2 * this.sy + this.ty );
        }

        public void transform_affine( ref double x, ref double y )
        {
            double num = x;
            x = num * this.sx + y * this.shx + this.tx;
            y = num * this.shy + y * this.sy + this.ty;
        }

        public void transform_2x2( ref double x, ref double y )
        {
            double num = x;
            x = num * this.sx + y * this.shx;
            y = num * this.shy + y * this.sy;
        }

        public void inverse_transform( ref double x, ref double y )
        {
            Perspective perspective = new Perspective(this);
            if ( !perspective.invert() )
                return;
            perspective.transform( ref x, ref y );
        }

        public double determinant()
        {
            return this.sx * ( this.sy * this.w2 - this.ty * this.w1 ) + this.shx * ( this.ty * this.w0 - this.shy * this.w2 ) + this.tx * ( this.shy * this.w1 - this.sy * this.w0 );
        }

        public double determinant_reciprocal()
        {
            return 1.0 / this.determinant();
        }

        public bool is_valid()
        {
            return this.is_valid( Perspective.affine_epsilon );
        }

        public bool is_valid( double epsilon )
        {
            if ( Math.Abs( this.sx ) > epsilon && Math.Abs( this.sy ) > epsilon )
                return Math.Abs( this.w2 ) > epsilon;
            return false;
        }

        public bool is_identity()
        {
            return this.is_identity( Perspective.affine_epsilon );
        }

        public bool is_identity( double epsilon )
        {
            if ( agg_basics.is_equal_eps( this.sx, 1.0, epsilon ) && agg_basics.is_equal_eps( this.shy, 0.0, epsilon ) && ( agg_basics.is_equal_eps( this.w0, 0.0, epsilon ) && agg_basics.is_equal_eps( this.shx, 0.0, epsilon ) ) && ( agg_basics.is_equal_eps( this.sy, 1.0, epsilon ) && agg_basics.is_equal_eps( this.w1, 0.0, epsilon ) && ( agg_basics.is_equal_eps( this.tx, 0.0, epsilon ) && agg_basics.is_equal_eps( this.ty, 0.0, epsilon ) ) ) )
                return agg_basics.is_equal_eps( this.w2, 1.0, epsilon );
            return false;
        }

        public bool is_equal( Perspective m )
        {
            return this.is_equal( m, Perspective.affine_epsilon );
        }

        public bool is_equal( Perspective m, double epsilon )
        {
            if ( agg_basics.is_equal_eps( this.sx, m.sx, epsilon ) && agg_basics.is_equal_eps( this.shy, m.shy, epsilon ) && ( agg_basics.is_equal_eps( this.w0, m.w0, epsilon ) && agg_basics.is_equal_eps( this.shx, m.shx, epsilon ) ) && ( agg_basics.is_equal_eps( this.sy, m.sy, epsilon ) && agg_basics.is_equal_eps( this.w1, m.w1, epsilon ) && ( agg_basics.is_equal_eps( this.tx, m.tx, epsilon ) && agg_basics.is_equal_eps( this.ty, m.ty, epsilon ) ) ) )
                return agg_basics.is_equal_eps( this.w2, m.w2, epsilon );
            return false;
        }

        public double scale()
        {
            double num1 = 0.707106781 * this.sx + 0.707106781 * this.shx;
            double num2 = 0.707106781 * this.shy + 0.707106781 * this.sy;
            return Math.Sqrt( num1 * num1 + num2 * num2 );
        }

        public double rotation()
        {
            double px1 = 0.0;
            double py1 = 0.0;
            double px2 = 1.0;
            double py2 = 0.0;
            this.transform( ref px1, ref py1 );
            this.transform( ref px2, ref py2 );
            return Math.Atan2( py2 - py1, px2 - px1 );
        }

        public void translation( out double dx, out double dy )
        {
            dx = this.tx;
            dy = this.ty;
        }

        public void scaling( out double x, out double y )
        {
            double px1 = 0.0;
            double py1 = 0.0;
            double px2 = 1.0;
            double py2 = 1.0;
            Perspective perspective = new Perspective(this) * Affine.NewRotation(-this.rotation());
            perspective.transform( ref px1, ref py1 );
            perspective.transform( ref px2, ref py2 );
            x = px2 - px1;
            y = py2 - py1;
        }

        public void scaling_abs( out double x, out double y )
        {
            x = Math.Sqrt( this.sx * this.sx + this.shx * this.shx );
            y = Math.Sqrt( this.shy * this.shy + this.sy * this.sy );
        }

        public Perspective.iterator_x begin( double x, double y, double step )
        {
            return new Perspective.iterator_x( x, y, step, this );
        }

        internal sealed class iterator_x
        {
            private double den;
            private double den_step;
            private double nom_x;
            private double nom_x_step;
            private double nom_y;
            private double nom_y_step;
            public double x;
            public double y;

            public iterator_x()
            {
            }

            public iterator_x( double px, double py, double step, Perspective m )
            {
                this.den = px * m.w0 + py * m.w1 + m.w2;
                this.den_step = m.w0 * step;
                this.nom_x = px * m.sx + py * m.shx + m.tx;
                this.nom_x_step = step * m.sx;
                this.nom_y = px * m.shy + py * m.sy + m.ty;
                this.nom_y_step = step * m.shy;
                this.x = this.nom_x / this.den;
                this.y = this.nom_y / this.den;
            }

            public static Perspective.iterator_x operator ++( Perspective.iterator_x a )
            {
                a.den += a.den_step;
                a.nom_x += a.nom_x_step;
                a.nom_y += a.nom_y_step;
                double num = 1.0 / a.den;
                a.x = a.nom_x * num;
                a.y = a.nom_y * num;
                return a;
            }
        }
    }
}
