// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.Transform.Affine
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using MatterHackers.VectorMath;

namespace MatterHackers.Agg.Transform
{
    internal struct Affine : ITransform
    {
        public static double affine_epsilon = 1E-14;
        public double sx;
        public double shy;
        public double shx;
        public double sy;
        public double tx;
        public double ty;

        public Affine( Affine copyFrom )
        {
            this.sx = copyFrom.sx;
            this.shy = copyFrom.shy;
            this.shx = copyFrom.shx;
            this.sy = copyFrom.sy;
            this.tx = copyFrom.tx;
            this.ty = copyFrom.ty;
        }

        public Affine( double v0, double v1, double v2, double v3, double v4, double v5 )
        {
            this.sx = v0;
            this.shy = v1;
            this.shx = v2;
            this.sy = v3;
            this.tx = v4;
            this.ty = v5;
        }

        public Affine( double[ ] m )
        {
            this.sx = m[ 0 ];
            this.shy = m[ 1 ];
            this.shx = m[ 2 ];
            this.sy = m[ 3 ];
            this.tx = m[ 4 ];
            this.ty = m[ 5 ];
        }

        public static Affine NewIdentity()
        {
            return new Affine() { sx = 1.0, shy = 0.0, shx = 0.0, sy = 1.0, tx = 0.0, ty = 0.0 };
        }

        public static Affine NewRotation( double AngleRadians )
        {
            return new Affine( Math.Cos( AngleRadians ), Math.Sin( AngleRadians ), -Math.Sin( AngleRadians ), Math.Cos( AngleRadians ), 0.0, 0.0 );
        }

        public static Affine NewScaling( double Scale )
        {
            return new Affine( Scale, 0.0, 0.0, Scale, 0.0, 0.0 );
        }

        public static Affine NewScaling( double x, double y )
        {
            return new Affine( x, 0.0, 0.0, y, 0.0, 0.0 );
        }

        public static Affine NewTranslation( double x, double y )
        {
            return new Affine( 1.0, 0.0, 0.0, 1.0, x, y );
        }

        public static Affine NewTranslation( Vector2 offset )
        {
            return new Affine( 1.0, 0.0, 0.0, 1.0, offset.x, offset.y );
        }

        public static Affine NewSkewing( double x, double y )
        {
            return new Affine( 1.0, Math.Tan( y ), Math.Tan( x ), 1.0, 0.0, 0.0 );
        }

        public void identity()
        {
            this.sx = this.sy = 1.0;
            this.shy = this.shx = this.tx = this.ty = 0.0;
        }

        public void translate( double x, double y )
        {
            this.tx += x;
            this.ty += y;
        }

        public void rotate( double AngleRadians )
        {
            double num1 = Math.Cos(AngleRadians);
            double num2 = Math.Sin(AngleRadians);
            double num3 = this.sx * num1 - this.shy * num2;
            double num4 = this.shx * num1 - this.sy * num2;
            double num5 = this.tx * num1 - this.ty * num2;
            this.shy = this.sx * num2 + this.shy * num1;
            this.sy = this.shx * num2 + this.sy * num1;
            this.ty = this.tx * num2 + this.ty * num1;
            this.sx = num3;
            this.shx = num4;
            this.tx = num5;
        }

        public void scale( double x, double y )
        {
            double num1 = x;
            double num2 = y;
            this.sx *= num1;
            this.shx *= num1;
            this.tx *= num1;
            this.shy *= num2;
            this.sy *= num2;
            this.ty *= num2;
        }

        public void scale( double scaleAmount )
        {
            this.sx *= scaleAmount;
            this.shx *= scaleAmount;
            this.tx *= scaleAmount;
            this.shy *= scaleAmount;
            this.sy *= scaleAmount;
            this.ty *= scaleAmount;
        }

        private void multiply( Affine m )
        {
            double num1 = this.sx * m.sx + this.shy * m.shx;
            double num2 = this.shx * m.sx + this.sy * m.shx;
            double num3 = this.tx * m.sx + this.ty * m.shx + m.tx;
            this.shy = this.sx * m.shy + this.shy * m.sy;
            this.sy = this.shx * m.shy + this.sy * m.sy;
            this.ty = this.tx * m.shy + this.ty * m.sy + m.ty;
            this.sx = num1;
            this.shx = num2;
            this.tx = num3;
        }

        public void invert()
        {
            double num1 = this.determinant_reciprocal();
            double num2 = this.sy * num1;
            this.sy = this.sx * num1;
            this.shy = -this.shy * num1;
            this.shx = -this.shx * num1;
            double num3 = -this.tx * num2 - this.ty * this.shx;
            this.ty = -this.tx * this.shy - this.ty * this.sy;
            this.sx = num2;
            this.tx = num3;
        }

        public static Affine operator *( Affine a, Affine b )
        {
            Affine affine = new Affine(a);
            affine.multiply( b );
            return affine;
        }

        public static Affine operator +( Affine a, Vector2 b )
        {
            Affine affine = new Affine(a);
            affine.tx += b.x;
            affine.ty += b.y;
            return affine;
        }

        public void transform( ref double x, ref double y )
        {
            double num = x;
            x = num * this.sx + y * this.shx + this.tx;
            y = num * this.shy + y * this.sy + this.ty;
        }

        public void transform( ref Vector2 pointToTransform )
        {
            this.transform( ref pointToTransform.x, ref pointToTransform.y );
        }

        public void transform( ref RectangleDouble rectToTransform )
        {
            this.transform( ref rectToTransform.Left, ref rectToTransform.Bottom );
            this.transform( ref rectToTransform.Right, ref rectToTransform.Top );
        }

        public void inverse_transform( ref double x, ref double y )
        {
            double num1 = this.determinant_reciprocal();
            double num2 = (x - this.tx) * num1;
            double num3 = (y - this.ty) * num1;
            x = num2 * this.sy - num3 * this.shx;
            y = num3 * this.sx - num2 * this.shy;
        }

        public void inverse_transform( ref Vector2 pointToTransform )
        {
            this.inverse_transform( ref pointToTransform.x, ref pointToTransform.y );
        }

        private double determinant_reciprocal()
        {
            return 1.0 / ( this.sx * this.sy - this.shy * this.shx );
        }

        public double GetScale()
        {
            double num1 = 0.707106781 * this.sx + 0.707106781 * this.shx;
            double num2 = 0.707106781 * this.shy + 0.707106781 * this.sy;
            return Math.Sqrt( num1 * num1 + num2 * num2 );
        }

        public bool is_valid( double epsilon )
        {
            if ( Math.Abs( this.sx ) > epsilon )
                return Math.Abs( this.sy ) > epsilon;
            return false;
        }

        public bool is_identity()
        {
            return this.is_identity( Affine.affine_epsilon );
        }

        public bool is_identity( double epsilon )
        {
            if ( agg_basics.is_equal_eps( this.sx, 1.0, epsilon ) && agg_basics.is_equal_eps( this.shy, 0.0, epsilon ) && ( agg_basics.is_equal_eps( this.shx, 0.0, epsilon ) && agg_basics.is_equal_eps( this.sy, 1.0, epsilon ) ) && agg_basics.is_equal_eps( this.tx, 0.0, epsilon ) )
                return agg_basics.is_equal_eps( this.ty, 0.0, epsilon );
            return false;
        }

        public bool is_equal( Affine m, double epsilon )
        {
            if ( agg_basics.is_equal_eps( this.sx, m.sx, epsilon ) && agg_basics.is_equal_eps( this.shy, m.shy, epsilon ) && ( agg_basics.is_equal_eps( this.shx, m.shx, epsilon ) && agg_basics.is_equal_eps( this.sy, m.sy, epsilon ) ) && agg_basics.is_equal_eps( this.tx, m.tx, epsilon ) )
                return agg_basics.is_equal_eps( this.ty, m.ty, epsilon );
            return false;
        }

        public double rotation()
        {
            double x1 = 0.0;
            double y1 = 0.0;
            double x2 = 1.0;
            double y2 = 0.0;
            this.transform( ref x1, ref y1 );
            this.transform( ref x2, ref y2 );
            return Math.Atan2( y2 - y1, x2 - x1 );
        }

        public void translation( out double dx, out double dy )
        {
            dx = this.tx;
            dy = this.ty;
        }

        public void scaling( out double x, out double y )
        {
            double x1 = 0.0;
            double y1 = 0.0;
            double x2 = 1.0;
            double y2 = 1.0;
            Affine affine = new Affine(this) * Affine.NewRotation(-this.rotation());
            affine.transform( ref x1, ref y1 );
            affine.transform( ref x2, ref y2 );
            x = x2 - x1;
            y = y2 - y1;
        }

        public void scaling_abs( out double x, out double y )
        {
            x = Math.Sqrt( this.sx * this.sx + this.shx * this.shx );
            y = Math.Sqrt( this.shy * this.shy + this.sy * this.sy );
        }
    }
}
