// Decompiled with JetBrains decompiler
// Type: MatterHackers.VectorMath.Quaternion
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;

namespace MatterHackers.VectorMath
{
    internal struct Quaternion : IEquatable<Quaternion>
    {
        public static readonly Quaternion Identity = new Quaternion(0.0, 0.0, 0.0, 1.0);
        private Vector3 xyz;
        private double w;

        public Quaternion( Vector3 v, double w )
        {
            this.xyz = v;
            this.w = w;
        }

        public Quaternion( double x, double y, double z, double w )
        {
            this = new Quaternion( new Vector3( x, y, z ), w );
        }

        public Vector3 Xyz
        {
            get
            {
                return this.xyz;
            }
            set
            {
                this.xyz = value;
            }
        }

        public double X
        {
            get
            {
                return this.xyz.x;
            }
            set
            {
                this.xyz.x = value;
            }
        }

        public double Y
        {
            get
            {
                return this.xyz.y;
            }
            set
            {
                this.xyz.y = value;
            }
        }

        public double Z
        {
            get
            {
                return this.xyz.z;
            }
            set
            {
                this.xyz.z = value;
            }
        }

        public double W
        {
            get
            {
                return this.w;
            }
            set
            {
                this.w = value;
            }
        }

        public void ToAxisAngle( out Vector3 axis, out double angle )
        {
            Vector4 axisAngle = this.ToAxisAngle();
            axis = axisAngle.Xyz;
            angle = axisAngle.w;
        }

        public Vector4 ToAxisAngle()
        {
            Quaternion quaternion = this;
            if ( quaternion.W > 1.0 )
                quaternion.Normalize();
            Vector4 vector4 = new Vector4();
            vector4.w = 2.0 * Math.Acos( quaternion.W );
            float num = (float) Math.Sqrt(1.0 - quaternion.W * quaternion.W);
            vector4.Xyz = ( double ) num <= 9.99999974737875E-05 ? Vector3.UnitX : quaternion.Xyz / ( double ) num;
            return vector4;
        }

        public double Length
        {
            get
            {
                return Math.Sqrt( this.W * this.W + this.Xyz.LengthSquared );
            }
        }

        public double LengthSquared
        {
            get
            {
                return this.W * this.W + this.Xyz.LengthSquared;
            }
        }

        public void Normalize()
        {
            double num = 1.0 / this.Length;
            this.Xyz *= num;
            this.W *= num;
        }

        public void Conjugate()
        {
            this.Xyz = -this.Xyz;
        }

        public static Quaternion Add( Quaternion left, Quaternion right )
        {
            return new Quaternion( left.Xyz + right.Xyz, left.W + right.W );
        }

        public static void Add( ref Quaternion left, ref Quaternion right, out Quaternion result )
        {
            result = new Quaternion( left.Xyz + right.Xyz, left.W + right.W );
        }

        public static Quaternion Sub( Quaternion left, Quaternion right )
        {
            return new Quaternion( left.Xyz - right.Xyz, left.W - right.W );
        }

        public static void Sub( ref Quaternion left, ref Quaternion right, out Quaternion result )
        {
            result = new Quaternion( left.Xyz - right.Xyz, left.W - right.W );
        }

        [Obsolete( "Use Multiply instead." )]
        public static Quaternion Mult( Quaternion left, Quaternion right )
        {
            return new Quaternion( right.W * left.Xyz + left.W * right.Xyz + Vector3.Cross( left.Xyz, right.Xyz ), left.W * right.W - Vector3.Dot( left.Xyz, right.Xyz ) );
        }

        [Obsolete( "Use Multiply instead." )]
        public static void Mult( ref Quaternion left, ref Quaternion right, out Quaternion result )
        {
            result = new Quaternion( right.W * left.Xyz + left.W * right.Xyz + Vector3.Cross( left.Xyz, right.Xyz ), left.W * right.W - Vector3.Dot( left.Xyz, right.Xyz ) );
        }

        public static Quaternion Multiply( Quaternion left, Quaternion right )
        {
            Quaternion result;
            Quaternion.Multiply( ref left, ref right, out result );
            return result;
        }

        public static void Multiply( ref Quaternion left, ref Quaternion right, out Quaternion result )
        {
            result = new Quaternion( right.W * left.Xyz + left.W * right.Xyz + Vector3.Cross( left.Xyz, right.Xyz ), left.W * right.W - Vector3.Dot( left.Xyz, right.Xyz ) );
        }

        public static void Multiply( ref Quaternion quaternion, double scale, out Quaternion result )
        {
            result = new Quaternion( quaternion.X * scale, quaternion.Y * scale, quaternion.Z * scale, quaternion.W * scale );
        }

        public static Quaternion Multiply( Quaternion quaternion, double scale )
        {
            return new Quaternion( quaternion.X * scale, quaternion.Y * scale, quaternion.Z * scale, quaternion.W * scale );
        }

        public static Quaternion Conjugate( Quaternion q )
        {
            return new Quaternion( -q.Xyz, q.W );
        }

        public static void Conjugate( ref Quaternion q, out Quaternion result )
        {
            result = new Quaternion( -q.Xyz, q.W );
        }

        public static Quaternion Invert( Quaternion q )
        {
            Quaternion result;
            Quaternion.Invert( ref q, out result );
            return result;
        }

        public static void Invert( ref Quaternion q, out Quaternion result )
        {
            double lengthSquared = q.LengthSquared;
            if ( lengthSquared != 0.0 )
            {
                double num = 1.0 / lengthSquared;
                result = new Quaternion( q.Xyz * -num, q.W * num );
            }
            else
                result = q;
        }

        public static Quaternion Normalize( Quaternion q )
        {
            Quaternion result;
            Quaternion.Normalize( ref q, out result );
            return result;
        }

        public static void Normalize( ref Quaternion q, out Quaternion result )
        {
            double num = 1.0 / q.Length;
            result = new Quaternion( q.Xyz * num, q.W * num );
        }

        public static Quaternion FromEulerAngles( Vector3 rotation )
        {
            Quaternion quaternion1 = Quaternion.FromAxisAngle(Vector3.UnitX, rotation.x);
            Quaternion quaternion2 = Quaternion.FromAxisAngle(Vector3.UnitY, rotation.y);
            return Quaternion.FromAxisAngle( Vector3.UnitZ, rotation.z ) * quaternion2 * quaternion1;
        }

        public static Quaternion FromAxisAngle( Vector3 axis, double angle )
        {
            if ( axis.LengthSquared == 0.0 )
                return Quaternion.Identity;
            Quaternion identity = Quaternion.Identity;
            angle *= 0.5;
            axis.Normalize();
            identity.Xyz = axis * Math.Sin( angle );
            identity.W = Math.Cos( angle );
            return Quaternion.Normalize( identity );
        }

        public static Quaternion Slerp( Quaternion q1, Quaternion q2, double blend )
        {
            if ( q1.LengthSquared == 0.0 )
            {
                if ( q2.LengthSquared == 0.0 )
                    return Quaternion.Identity;
                return q2;
            }
            if ( q2.LengthSquared == 0.0 )
                return q1;
            double d = q1.W * q2.W + Vector3.Dot(q1.Xyz, q2.Xyz);
            if ( d >= 1.0 || d <= -1.0 )
                return q1;
            if ( d < 0.0 )
            {
                q2.Xyz = -q2.Xyz;
                q2.W = -q2.W;
                d = -d;
            }
            double num1;
            double num2;
            if ( d < 0.990000009536743 )
            {
                double a = Math.Acos(d);
                double num3 = 1.0 / Math.Sin(a);
                num1 = Math.Sin( a * ( 1.0 - blend ) ) * num3;
                num2 = Math.Sin( a * blend ) * num3;
            }
            else
            {
                num1 = 1.0 - blend;
                num2 = blend;
            }
            Quaternion q = new Quaternion(num1 * q1.Xyz + num2 * q2.Xyz, num1 * q1.W + num2 * q2.W);
            if ( q.LengthSquared > 0.0 )
                return Quaternion.Normalize( q );
            return Quaternion.Identity;
        }

        public static Quaternion operator +( Quaternion left, Quaternion right )
        {
            left.Xyz += right.Xyz;
            left.W += right.W;
            return left;
        }

        public static Quaternion operator -( Quaternion left, Quaternion right )
        {
            left.Xyz -= right.Xyz;
            left.W -= right.W;
            return left;
        }

        public static Quaternion operator *( Quaternion left, Quaternion right )
        {
            Quaternion.Multiply( ref left, ref right, out left );
            return left;
        }

        public static Quaternion operator *( Quaternion quaternion, double scale )
        {
            Quaternion.Multiply( ref quaternion, scale, out quaternion );
            return quaternion;
        }

        public static Quaternion operator *( double scale, Quaternion quaternion )
        {
            return new Quaternion( quaternion.X * scale, quaternion.Y * scale, quaternion.Z * scale, quaternion.W * scale );
        }

        public static bool operator ==( Quaternion left, Quaternion right )
        {
            return left.Equals( right );
        }

        public static bool operator !=( Quaternion left, Quaternion right )
        {
            return !left.Equals( right );
        }

        public override string ToString()
        {
            return string.Format( "V: {0}, W: {1}", ( object ) this.Xyz, ( object ) this.W );
        }

        public override bool Equals( object other )
        {
            if ( !( other is Quaternion ) )
                return false;
            return this == ( Quaternion ) other;
        }

        public override int GetHashCode()
        {
            return new double[ 4 ] { this.Xyz.x, this.Xyz.y, this.Xyz.z, this.W }.GetHashCode();
        }

        public bool Equals( Quaternion other )
        {
            if ( this.Xyz == other.Xyz )
                return this.W == other.W;
            return false;
        }
    }
}
