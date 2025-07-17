// Decompiled with JetBrains decompiler
// Type: MatterHackers.VectorMath.Vector2
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Runtime.InteropServices;

namespace MatterHackers.VectorMath
{
    internal struct Vector2 : IEquatable<Vector2>
    {
        public static Vector2 UnitX = new Vector2(1.0, 0.0);
        public static Vector2 UnitY = new Vector2(0.0, 1.0);
        public static Vector2 Zero = new Vector2(0.0, 0.0);
        public static readonly Vector2 One = new Vector2(1.0, 1.0);
        public static readonly int SizeInBytes = Marshal.SizeOf((object) new Vector2());
        public double x;
        public double y;

        public Vector2( double x, double y )
        {
            this.x = x;
            this.y = y;
        }

        public Vector2( Vector3 vector )
        {
            this.x = vector.x;
            this.y = vector.y;
        }

        public double this[ int index ]
        {
            get
            {
                if ( index == 0 )
                    return this.x;
                if ( index == 1 )
                    return this.y;
                return 0.0;
            }
            set
            {
                if ( index != 0 )
                {
                    if ( index != 1 )
                        throw new Exception();
                    this.y = value;
                }
                else
                    this.x = value;
            }
        }

        public double Length
        {
            get
            {
                return Math.Sqrt( this.x * this.x + this.y * this.y );
            }
        }

        public double LengthSquared
        {
            get
            {
                return this.x * this.x + this.y * this.y;
            }
        }

        public void Rotate( double radians )
        {
            this = Vector2.Rotate( this, radians );
        }

        public double GetAngle()
        {
            return Math.Atan2( this.y, this.x );
        }

        public double GetAngle0To2PI()
        {
            return MathHelper.Range0ToTau( this.GetAngle() );
        }

        public Vector2 PerpendicularRight
        {
            get
            {
                return new Vector2( this.y, -this.x );
            }
        }

        public Vector2 PerpendicularLeft
        {
            get
            {
                return new Vector2( -this.y, this.x );
            }
        }

        public Vector2 GetNormal()
        {
            Vector2 vector2 = this;
            vector2.Normalize();
            return vector2;
        }

        public void Normalize()
        {
            double num = 1.0 / this.Length;
            this.x *= num;
            this.y *= num;
        }

        public static Vector2 Add( Vector2 a, Vector2 b )
        {
            Vector2.Add( ref a, ref b, out a );
            return a;
        }

        public static void Add( ref Vector2 a, ref Vector2 b, out Vector2 result )
        {
            result = new Vector2( a.x + b.x, a.y + b.y );
        }

        public static Vector2 Subtract( Vector2 a, Vector2 b )
        {
            Vector2.Subtract( ref a, ref b, out a );
            return a;
        }

        public static void Subtract( ref Vector2 a, ref Vector2 b, out Vector2 result )
        {
            result = new Vector2( a.x - b.x, a.y - b.y );
        }

        public static Vector2 Multiply( Vector2 vector, double scale )
        {
            Vector2.Multiply( ref vector, scale, out vector );
            return vector;
        }

        public static void Multiply( ref Vector2 vector, double scale, out Vector2 result )
        {
            result = new Vector2( vector.x * scale, vector.y * scale );
        }

        public static Vector2 Multiply( Vector2 vector, Vector2 scale )
        {
            Vector2.Multiply( ref vector, ref scale, out vector );
            return vector;
        }

        public static void Multiply( ref Vector2 vector, ref Vector2 scale, out Vector2 result )
        {
            result = new Vector2( vector.x * scale.x, vector.y * scale.y );
        }

        public static Vector2 Divide( Vector2 vector, double scale )
        {
            Vector2.Divide( ref vector, scale, out vector );
            return vector;
        }

        public static void Divide( ref Vector2 vector, double scale, out Vector2 result )
        {
            Vector2.Multiply( ref vector, 1.0 / scale, out result );
        }

        public static Vector2 Divide( Vector2 vector, Vector2 scale )
        {
            Vector2.Divide( ref vector, ref scale, out vector );
            return vector;
        }

        public static void Divide( ref Vector2 vector, ref Vector2 scale, out Vector2 result )
        {
            result = new Vector2( vector.x / scale.x, vector.y / scale.y );
        }

        public static Vector2 Min( Vector2 a, Vector2 b )
        {
            a.x = a.x < b.x ? a.x : b.x;
            a.y = a.y < b.y ? a.y : b.y;
            return a;
        }

        public static void Min( ref Vector2 a, ref Vector2 b, out Vector2 result )
        {
            result.x = a.x < b.x ? a.x : b.x;
            result.y = a.y < b.y ? a.y : b.y;
        }

        public static Vector2 Max( Vector2 a, Vector2 b )
        {
            a.x = a.x > b.x ? a.x : b.x;
            a.y = a.y > b.y ? a.y : b.y;
            return a;
        }

        public static void Max( ref Vector2 a, ref Vector2 b, out Vector2 result )
        {
            result.x = a.x > b.x ? a.x : b.x;
            result.y = a.y > b.y ? a.y : b.y;
        }

        public static Vector2 Clamp( Vector2 vec, Vector2 min, Vector2 max )
        {
            vec.x = vec.x < min.x ? min.x : ( vec.x > max.x ? max.x : vec.x );
            vec.y = vec.y < min.y ? min.y : ( vec.y > max.y ? max.y : vec.y );
            return vec;
        }

        public static void Clamp( ref Vector2 vec, ref Vector2 min, ref Vector2 max, out Vector2 result )
        {
            result.x = vec.x < min.x ? min.x : ( vec.x > max.x ? max.x : vec.x );
            result.y = vec.y < min.y ? min.y : ( vec.y > max.y ? max.y : vec.y );
        }

        public static Vector2 Normalize( Vector2 vec )
        {
            double num = 1.0 / vec.Length;
            vec.x *= num;
            vec.y *= num;
            return vec;
        }

        public static void Normalize( ref Vector2 vec, out Vector2 result )
        {
            double num = 1.0 / vec.Length;
            result.x = vec.x * num;
            result.y = vec.y * num;
        }

        public static Vector2 NormalizeFast( Vector2 vec )
        {
            double num = MathHelper.InverseSqrtFast(vec.x * vec.x + vec.y * vec.y);
            vec.x *= num;
            vec.y *= num;
            return vec;
        }

        public static void NormalizeFast( ref Vector2 vec, out Vector2 result )
        {
            double num = MathHelper.InverseSqrtFast(vec.x * vec.x + vec.y * vec.y);
            result.x = vec.x * num;
            result.y = vec.y * num;
        }

        public static double Dot( Vector2 left, Vector2 right )
        {
            return left.x * right.x + left.y * right.y;
        }

        public static void Dot( ref Vector2 left, ref Vector2 right, out double result )
        {
            result = left.x * right.x + left.y * right.y;
        }

        public static double Cross( Vector2 left, Vector2 right )
        {
            return left.x * right.y - left.y * right.x;
        }

        public static Vector2 Rotate( Vector2 toRotate, double radians )
        {
            Vector2 output;
            Vector2.Rotate( ref toRotate, radians, out output );
            return output;
        }

        public static void Rotate( ref Vector2 input, double radians, out Vector2 output )
        {
            double num1 = Math.Cos(radians);
            double num2 = Math.Sin(radians);
            output.x = input.x * num1 - input.y * num2;
            output.y = input.y * num1 + input.x * num2;
        }

        public static Vector2 Lerp( Vector2 a, Vector2 b, double blend )
        {
            a.x = blend * ( b.x - a.x ) + a.x;
            a.y = blend * ( b.y - a.y ) + a.y;
            return a;
        }

        public static void Lerp( ref Vector2 a, ref Vector2 b, double blend, out Vector2 result )
        {
            result.x = blend * ( b.x - a.x ) + a.x;
            result.y = blend * ( b.y - a.y ) + a.y;
        }

        public static Vector2 BaryCentric( Vector2 a, Vector2 b, Vector2 c, double u, double v )
        {
            return a + u * ( b - a ) + v * ( c - a );
        }

        public static void BaryCentric( ref Vector2 a, ref Vector2 b, ref Vector2 c, double u, double v, out Vector2 result )
        {
            result = a;
            Vector2 result1 = b;
            Vector2.Subtract( ref result1, ref a, out result1 );
            Vector2.Multiply( ref result1, u, out result1 );
            Vector2.Add( ref result, ref result1, out result );
            Vector2 result2 = c;
            Vector2.Subtract( ref result2, ref a, out result2 );
            Vector2.Multiply( ref result2, v, out result2 );
            Vector2.Add( ref result, ref result2, out result );
        }

        public static Vector2 Transform( Vector2 vec, Quaternion quat )
        {
            Vector2 result;
            Vector2.Transform( ref vec, ref quat, out result );
            return result;
        }

        public static void Transform( ref Vector2 vec, ref Quaternion quat, out Vector2 result )
        {
            Quaternion result1 = new Quaternion(vec.x, vec.y, 0.0, 0.0);
            Quaternion result2;
            Quaternion.Invert( ref quat, out result2 );
            Quaternion result3;
            Quaternion.Multiply( ref quat, ref result1, out result3 );
            Quaternion.Multiply( ref result3, ref result2, out result1 );
            result = new Vector2( result1.X, result1.Y );
        }

        public static Vector2 ComponentMin( Vector2 a, Vector2 b )
        {
            a.x = a.x < b.x ? a.x : b.x;
            a.y = a.y < b.y ? a.y : b.y;
            return a;
        }

        public static void ComponentMin( ref Vector2 a, ref Vector2 b, out Vector2 result )
        {
            result.x = a.x < b.x ? a.x : b.x;
            result.y = a.y < b.y ? a.y : b.y;
        }

        public static Vector2 ComponentMax( Vector2 a, Vector2 b )
        {
            a.x = a.x > b.x ? a.x : b.x;
            a.y = a.y > b.y ? a.y : b.y;
            return a;
        }

        public static void ComponentMax( ref Vector2 a, ref Vector2 b, out Vector2 result )
        {
            result.x = a.x > b.x ? a.x : b.x;
            result.y = a.y > b.y ? a.y : b.y;
        }

        public static Vector2 operator +( Vector2 left, Vector2 right )
        {
            left.x += right.x;
            left.y += right.y;
            return left;
        }

        public static Vector2 operator -( Vector2 left, Vector2 right )
        {
            left.x -= right.x;
            left.y -= right.y;
            return left;
        }

        public static Vector2 operator -( Vector2 vec )
        {
            vec.x = -vec.x;
            vec.y = -vec.y;
            return vec;
        }

        public static Vector2 operator *( Vector2 vec, double f )
        {
            vec.x *= f;
            vec.y *= f;
            return vec;
        }

        public static Vector2 operator *( double f, Vector2 vec )
        {
            vec.x *= f;
            vec.y *= f;
            return vec;
        }

        public static Vector2 operator /( Vector2 vec, double f )
        {
            double num = 1.0 / f;
            vec.x *= num;
            vec.y *= num;
            return vec;
        }

        public static Vector2 operator /( double f, Vector2 vec )
        {
            vec.x = f / vec.x;
            vec.y = f / vec.y;
            return vec;
        }

        public static bool operator ==( Vector2 left, Vector2 right )
        {
            return left.Equals( right );
        }

        public static bool operator !=( Vector2 left, Vector2 right )
        {
            return !left.Equals( right );
        }

        public override string ToString()
        {
            return string.Format( "({0}, {1})", ( object ) this.x, ( object ) this.y );
        }

        public override int GetHashCode()
        {
            return new double[ 2 ] { this.x, this.y }.GetHashCode();
        }

        public override bool Equals( object obj )
        {
            if ( !( obj is Vector2 ) )
                return false;
            return this.Equals( ( Vector2 ) obj );
        }

        public bool Equals( Vector2 other )
        {
            if ( this.x == other.x )
                return this.y == other.y;
            return false;
        }

        public bool Equals( Vector2 other, double errorRange )
        {
            return this.x < other.x + errorRange && this.x > other.x - errorRange && ( this.y < other.y + errorRange && this.y > other.y - errorRange );
        }
    }
}
