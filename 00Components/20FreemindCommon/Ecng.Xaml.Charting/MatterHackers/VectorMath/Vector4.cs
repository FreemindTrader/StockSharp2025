// Decompiled with JetBrains decompiler
// Type: MatterHackers.VectorMath.Vector4
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Runtime.InteropServices;

namespace MatterHackers.VectorMath
{
    internal struct Vector4 : IEquatable<Vector4>
    {
        public static Vector4 UnitX = new Vector4(1.0, 0.0, 0.0, 0.0);
        public static Vector4 UnitY = new Vector4(0.0, 1.0, 0.0, 0.0);
        public static Vector4 UnitZ = new Vector4(0.0, 0.0, 1.0, 0.0);
        public static Vector4 UnitW = new Vector4(0.0, 0.0, 0.0, 1.0);
        public static Vector4 Zero = new Vector4(0.0, 0.0, 0.0, 0.0);
        public static readonly Vector4 One = new Vector4(1.0, 1.0, 1.0, 1.0);
        public static readonly int SizeInBytes = Marshal.SizeOf((object) new Vector4());
        public double x;
        public double y;
        public double z;
        public double w;

        public Vector4( double x, double y, double z, double w )
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Vector4( Vector2 v )
        {
            this.x = v.x;
            this.y = v.y;
            this.z = 0.0;
            this.w = 0.0;
        }

        public Vector4( Vector3 v )
        {
            this.x = v.x;
            this.y = v.y;
            this.z = v.z;
            this.w = 0.0;
        }

        public Vector4( Vector3 v, double w )
        {
            this.x = v.x;
            this.y = v.y;
            this.z = v.z;
            this.w = w;
        }

        public Vector4( Vector4 v )
        {
            this.x = v.x;
            this.y = v.y;
            this.z = v.z;
            this.w = v.w;
        }

        public double this[ int index ]
        {
            get
            {
                switch ( index )
                {
                    case 0:
                        return this.x;
                    case 1:
                        return this.y;
                    case 2:
                        return this.z;
                    case 3:
                        return this.w;
                    default:
                        return 0.0;
                }
            }
            set
            {
                switch ( index )
                {
                    case 0:
                        this.x = value;
                        break;
                    case 1:
                        this.y = value;
                        break;
                    case 2:
                        this.z = value;
                        break;
                    case 3:
                        this.w = value;
                        break;
                    default:
                        throw new Exception();
                }
            }
        }

        public double Length
        {
            get
            {
                return Math.Sqrt( this.x * this.x + this.y * this.y + this.z * this.z + this.w * this.w );
            }
        }

        public double LengthFast
        {
            get
            {
                return 1.0 / MathHelper.InverseSqrtFast( this.x * this.x + this.y * this.y + this.z * this.z + this.w * this.w );
            }
        }

        public double LengthSquared
        {
            get
            {
                return this.x * this.x + this.y * this.y + this.z * this.z + this.w * this.w;
            }
        }

        public void Normalize()
        {
            double num = 1.0 / this.Length;
            this.x *= num;
            this.y *= num;
            this.z *= num;
            this.w *= num;
        }

        public void NormalizeFast()
        {
            double num = MathHelper.InverseSqrtFast(this.x * this.x + this.y * this.y + this.z * this.z + this.w * this.w);
            this.x *= num;
            this.y *= num;
            this.z *= num;
            this.w *= num;
        }

        public static Vector4 Add( Vector4 a, Vector4 b )
        {
            Vector4.Add( ref a, ref b, out a );
            return a;
        }

        public static void Add( ref Vector4 a, ref Vector4 b, out Vector4 result )
        {
            result = new Vector4( a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w );
        }

        public static Vector4 Subtract( Vector4 a, Vector4 b )
        {
            Vector4.Subtract( ref a, ref b, out a );
            return a;
        }

        public static void Subtract( ref Vector4 a, ref Vector4 b, out Vector4 result )
        {
            result = new Vector4( a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w );
        }

        public static Vector4 Multiply( Vector4 vector, double scale )
        {
            Vector4.Multiply( ref vector, scale, out vector );
            return vector;
        }

        public static void Multiply( ref Vector4 vector, double scale, out Vector4 result )
        {
            result = new Vector4( vector.x * scale, vector.y * scale, vector.z * scale, vector.w * scale );
        }

        public static Vector4 Multiply( Vector4 vector, Vector4 scale )
        {
            Vector4.Multiply( ref vector, ref scale, out vector );
            return vector;
        }

        public static void Multiply( ref Vector4 vector, ref Vector4 scale, out Vector4 result )
        {
            result = new Vector4( vector.x * scale.x, vector.y * scale.y, vector.z * scale.z, vector.w * scale.w );
        }

        public static Vector4 Divide( Vector4 vector, double scale )
        {
            Vector4.Divide( ref vector, scale, out vector );
            return vector;
        }

        public static void Divide( ref Vector4 vector, double scale, out Vector4 result )
        {
            Vector4.Multiply( ref vector, 1.0 / scale, out result );
        }

        public static Vector4 Divide( Vector4 vector, Vector4 scale )
        {
            Vector4.Divide( ref vector, ref scale, out vector );
            return vector;
        }

        public static void Divide( ref Vector4 vector, ref Vector4 scale, out Vector4 result )
        {
            result = new Vector4( vector.x / scale.x, vector.y / scale.y, vector.z / scale.z, vector.w / scale.w );
        }

        public static Vector4 Min( Vector4 a, Vector4 b )
        {
            a.x = a.x < b.x ? a.x : b.x;
            a.y = a.y < b.y ? a.y : b.y;
            a.z = a.z < b.z ? a.z : b.z;
            a.w = a.w < b.w ? a.w : b.w;
            return a;
        }

        public static void Min( ref Vector4 a, ref Vector4 b, out Vector4 result )
        {
            result.x = a.x < b.x ? a.x : b.x;
            result.y = a.y < b.y ? a.y : b.y;
            result.z = a.z < b.z ? a.z : b.z;
            result.w = a.w < b.w ? a.w : b.w;
        }

        public static Vector4 Max( Vector4 a, Vector4 b )
        {
            a.x = a.x > b.x ? a.x : b.x;
            a.y = a.y > b.y ? a.y : b.y;
            a.z = a.z > b.z ? a.z : b.z;
            a.w = a.w > b.w ? a.w : b.w;
            return a;
        }

        public static void Max( ref Vector4 a, ref Vector4 b, out Vector4 result )
        {
            result.x = a.x > b.x ? a.x : b.x;
            result.y = a.y > b.y ? a.y : b.y;
            result.z = a.z > b.z ? a.z : b.z;
            result.w = a.w > b.w ? a.w : b.w;
        }

        public static Vector4 Clamp( Vector4 vec, Vector4 min, Vector4 max )
        {
            vec.x = vec.x < min.x ? min.x : ( vec.x > max.x ? max.x : vec.x );
            vec.y = vec.y < min.y ? min.y : ( vec.y > max.y ? max.y : vec.y );
            vec.z = vec.x < min.z ? min.z : ( vec.z > max.z ? max.z : vec.z );
            vec.w = vec.y < min.w ? min.w : ( vec.w > max.w ? max.w : vec.w );
            return vec;
        }

        public static void Clamp( ref Vector4 vec, ref Vector4 min, ref Vector4 max, out Vector4 result )
        {
            result.x = vec.x < min.x ? min.x : ( vec.x > max.x ? max.x : vec.x );
            result.y = vec.y < min.y ? min.y : ( vec.y > max.y ? max.y : vec.y );
            result.z = vec.x < min.z ? min.z : ( vec.z > max.z ? max.z : vec.z );
            result.w = vec.y < min.w ? min.w : ( vec.w > max.w ? max.w : vec.w );
        }

        public static Vector4 Normalize( Vector4 vec )
        {
            double num = 1.0 / vec.Length;
            vec.x *= num;
            vec.y *= num;
            vec.z *= num;
            vec.w *= num;
            return vec;
        }

        public static void Normalize( ref Vector4 vec, out Vector4 result )
        {
            double num = 1.0 / vec.Length;
            result.x = vec.x * num;
            result.y = vec.y * num;
            result.z = vec.z * num;
            result.w = vec.w * num;
        }

        public static Vector4 NormalizeFast( Vector4 vec )
        {
            double num = MathHelper.InverseSqrtFast(vec.x * vec.x + vec.y * vec.y + vec.z * vec.z + vec.w * vec.w);
            vec.x *= num;
            vec.y *= num;
            vec.z *= num;
            vec.w *= num;
            return vec;
        }

        public static void NormalizeFast( ref Vector4 vec, out Vector4 result )
        {
            double num = MathHelper.InverseSqrtFast(vec.x * vec.x + vec.y * vec.y + vec.z * vec.z + vec.w * vec.w);
            result.x = vec.x * num;
            result.y = vec.y * num;
            result.z = vec.z * num;
            result.w = vec.w * num;
        }

        public static double Dot( Vector4 left, Vector4 right )
        {
            return left.x * right.x + left.y * right.y + left.z * right.z + left.w * right.w;
        }

        public static void Dot( ref Vector4 left, ref Vector4 right, out double result )
        {
            result = left.x * right.x + left.y * right.y + left.z * right.z + left.w * right.w;
        }

        public static Vector4 Lerp( Vector4 a, Vector4 b, double blend )
        {
            a.x = blend * ( b.x - a.x ) + a.x;
            a.y = blend * ( b.y - a.y ) + a.y;
            a.z = blend * ( b.z - a.z ) + a.z;
            a.w = blend * ( b.w - a.w ) + a.w;
            return a;
        }

        public static void Lerp( ref Vector4 a, ref Vector4 b, double blend, out Vector4 result )
        {
            result.x = blend * ( b.x - a.x ) + a.x;
            result.y = blend * ( b.y - a.y ) + a.y;
            result.z = blend * ( b.z - a.z ) + a.z;
            result.w = blend * ( b.w - a.w ) + a.w;
        }

        public static Vector4 BaryCentric( Vector4 a, Vector4 b, Vector4 c, double u, double v )
        {
            return a + u * ( b - a ) + v * ( c - a );
        }

        public static void BaryCentric( ref Vector4 a, ref Vector4 b, ref Vector4 c, double u, double v, out Vector4 result )
        {
            result = a;
            Vector4 result1 = b;
            Vector4.Subtract( ref result1, ref a, out result1 );
            Vector4.Multiply( ref result1, u, out result1 );
            Vector4.Add( ref result, ref result1, out result );
            Vector4 result2 = c;
            Vector4.Subtract( ref result2, ref a, out result2 );
            Vector4.Multiply( ref result2, v, out result2 );
            Vector4.Add( ref result, ref result2, out result );
        }

        public static Vector4 Transform( Vector4 vec, Matrix4X4 mat )
        {
            Vector4 result;
            Vector4.Transform( ref vec, ref mat, out result );
            return result;
        }

        public static void Transform( ref Vector4 vec, ref Matrix4X4 mat, out Vector4 result )
        {
            result = new Vector4( vec.x * mat.Row0.x + vec.y * mat.Row1.x + vec.z * mat.Row2.x + vec.w * mat.Row3.x, vec.x * mat.Row0.y + vec.y * mat.Row1.y + vec.z * mat.Row2.y + vec.w * mat.Row3.y, vec.x * mat.Row0.z + vec.y * mat.Row1.z + vec.z * mat.Row2.z + vec.w * mat.Row3.z, vec.x * mat.Row0.w + vec.y * mat.Row1.w + vec.z * mat.Row2.w + vec.w * mat.Row3.w );
        }

        public static Vector4 Transform( Vector4 vec, Quaternion quat )
        {
            Vector4 result;
            Vector4.Transform( ref vec, ref quat, out result );
            return result;
        }

        public static void Transform( ref Vector4 vec, ref Quaternion quat, out Vector4 result )
        {
            Quaternion result1 = new Quaternion(vec.x, vec.y, vec.z, vec.w);
            Quaternion result2;
            Quaternion.Invert( ref quat, out result2 );
            Quaternion result3;
            Quaternion.Multiply( ref quat, ref result1, out result3 );
            Quaternion.Multiply( ref result3, ref result2, out result1 );
            result = new Vector4( result1.X, result1.Y, result1.Z, result1.W );
        }

        public Vector2 Xy
        {
            get
            {
                return new Vector2( this.x, this.y );
            }
            set
            {
                this.x = value.x;
                this.y = value.y;
            }
        }

        public Vector3 Xyz
        {
            get
            {
                return new Vector3( this.x, this.y, this.z );
            }
            set
            {
                this.x = value.x;
                this.y = value.y;
                this.z = value.z;
            }
        }

        public static Vector4 operator +( Vector4 left, Vector4 right )
        {
            left.x += right.x;
            left.y += right.y;
            left.z += right.z;
            left.w += right.w;
            return left;
        }

        public static Vector4 operator -( Vector4 left, Vector4 right )
        {
            left.x -= right.x;
            left.y -= right.y;
            left.z -= right.z;
            left.w -= right.w;
            return left;
        }

        public static Vector4 operator -( Vector4 vec )
        {
            vec.x = -vec.x;
            vec.y = -vec.y;
            vec.z = -vec.z;
            vec.w = -vec.w;
            return vec;
        }

        public static Vector4 operator *( Vector4 vec, double scale )
        {
            vec.x *= scale;
            vec.y *= scale;
            vec.z *= scale;
            vec.w *= scale;
            return vec;
        }

        public static Vector4 operator *( double scale, Vector4 vec )
        {
            vec.x *= scale;
            vec.y *= scale;
            vec.z *= scale;
            vec.w *= scale;
            return vec;
        }

        public static Vector4 operator /( Vector4 vec, double scale )
        {
            double num = 1.0 / scale;
            vec.x *= num;
            vec.y *= num;
            vec.z *= num;
            vec.w *= num;
            return vec;
        }

        public static bool operator ==( Vector4 left, Vector4 right )
        {
            return left.Equals( right );
        }

        public static bool operator !=( Vector4 left, Vector4 right )
        {
            return !left.Equals( right );
        }

        public static unsafe explicit operator double*( Vector4 v )
        {
            return &v.x;
        }

        public static unsafe explicit operator IntPtr( Vector4 v )
        {
            return ( IntPtr ) ( ( void* ) &v.x );
        }

        public override string ToString()
        {
            return string.Format( "{0}, {1}, {2}, {3}", ( object ) this.x, ( object ) this.y, ( object ) this.z, ( object ) this.w );
        }

        public string ToString( string format = "" )
        {
            return this.x.ToString( format ) + ", " + this.y.ToString( format ) + ", " + this.z.ToString( format ) + ", " + this.w.ToString( format );
        }

        public override int GetHashCode()
        {
            return new { x = this.x, y = this.y, z = this.z, w = this.w }.GetHashCode();
        }

        public override bool Equals( object obj )
        {
            if ( !( obj is Vector4 ) )
                return false;
            return this.Equals( ( Vector4 ) obj );
        }

        public bool Equals( Vector4 other )
        {
            if ( this.x == other.x && this.y == other.y && this.z == other.z )
                return this.w == other.w;
            return false;
        }
    }
}
