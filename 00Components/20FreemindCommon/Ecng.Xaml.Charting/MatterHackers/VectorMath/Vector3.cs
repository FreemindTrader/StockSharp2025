// Decompiled with JetBrains decompiler
// Type: MatterHackers.VectorMath.Vector3
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Runtime.InteropServices;

namespace MatterHackers.VectorMath
{
    internal struct Vector3 : IEquatable<Vector3>
    {
        public static readonly Vector3 UnitX = new Vector3(1.0, 0.0, 0.0);
        public static readonly Vector3 UnitY = new Vector3(0.0, 1.0, 0.0);
        public static readonly Vector3 UnitZ = new Vector3(0.0, 0.0, 1.0);
        public static readonly Vector3 Zero = new Vector3(0.0, 0.0, 0.0);
        public static readonly Vector3 One = new Vector3(1.0, 1.0, 1.0);
        public static readonly Vector3 PositiveInfinity = new Vector3(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity);
        public static readonly Vector3 NegativeInfinity = new Vector3(double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity);
        public static readonly int SizeInBytes = Marshal.SizeOf((object) new Vector3());
        public double x;
        public double y;
        public double z;

        public Vector3( double x, double y, double z )
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3( Vector2 v, double z = 0.0 )
        {
            this.x = v.x;
            this.y = v.y;
            this.z = z;
        }

        public Vector3( Vector3 v )
        {
            this.x = v.x;
            this.y = v.y;
            this.z = v.z;
        }

        public Vector3( Vector4 v )
        {
            this.x = v.x;
            this.y = v.y;
            this.z = v.z;
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
                    default:
                        throw new Exception();
                }
            }
        }

        public double Length
        {
            get
            {
                return Math.Sqrt( this.x * this.x + this.y * this.y + this.z * this.z );
            }
        }

        public double LengthFast
        {
            get
            {
                return 1.0 / MathHelper.InverseSqrtFast( this.x * this.x + this.y * this.y + this.z * this.z );
            }
        }

        public double LengthSquared
        {
            get
            {
                return this.x * this.x + this.y * this.y + this.z * this.z;
            }
        }

        public Vector3 GetNormal()
        {
            Vector3 vector3 = this;
            vector3.Normalize();
            return vector3;
        }

        public void Normalize()
        {
            double num = 1.0 / this.Length;
            this.x *= num;
            this.y *= num;
            this.z *= num;
        }

        public void NormalizeFast()
        {
            double num = MathHelper.InverseSqrtFast(this.x * this.x + this.y * this.y + this.z * this.z);
            this.x *= num;
            this.y *= num;
            this.z *= num;
        }

        public double[ ] ToArray()
        {
            return new double[ 3 ] { this.x, this.y, this.z };
        }

        public static Vector3 Add( Vector3 a, Vector3 b )
        {
            Vector3.Add( ref a, ref b, out a );
            return a;
        }

        public static void Add( ref Vector3 a, ref Vector3 b, out Vector3 result )
        {
            result = new Vector3( a.x + b.x, a.y + b.y, a.z + b.z );
        }

        public static Vector3 Subtract( Vector3 a, Vector3 b )
        {
            Vector3.Subtract( ref a, ref b, out a );
            return a;
        }

        public static void Subtract( ref Vector3 a, ref Vector3 b, out Vector3 result )
        {
            result = new Vector3( a.x - b.x, a.y - b.y, a.z - b.z );
        }

        public static Vector3 Multiply( Vector3 vector, double scale )
        {
            Vector3.Multiply( ref vector, scale, out vector );
            return vector;
        }

        public static void Multiply( ref Vector3 vector, double scale, out Vector3 result )
        {
            result = new Vector3( vector.x * scale, vector.y * scale, vector.z * scale );
        }

        public static Vector3 Multiply( Vector3 vector, Vector3 scale )
        {
            Vector3.Multiply( ref vector, ref scale, out vector );
            return vector;
        }

        public static void Multiply( ref Vector3 vector, ref Vector3 scale, out Vector3 result )
        {
            result = new Vector3( vector.x * scale.x, vector.y * scale.y, vector.z * scale.z );
        }

        public static Vector3 Divide( Vector3 vector, double scale )
        {
            Vector3.Divide( ref vector, scale, out vector );
            return vector;
        }

        public static void Divide( ref Vector3 vector, double scale, out Vector3 result )
        {
            Vector3.Multiply( ref vector, 1.0 / scale, out result );
        }

        public static Vector3 Divide( Vector3 vector, Vector3 scale )
        {
            Vector3.Divide( ref vector, ref scale, out vector );
            return vector;
        }

        public static void Divide( ref Vector3 vector, ref Vector3 scale, out Vector3 result )
        {
            result = new Vector3( vector.x / scale.x, vector.y / scale.y, vector.z / scale.z );
        }

        public static Vector3 ComponentMin( Vector3 a, Vector3 b )
        {
            a.x = a.x < b.x ? a.x : b.x;
            a.y = a.y < b.y ? a.y : b.y;
            a.z = a.z < b.z ? a.z : b.z;
            return a;
        }

        public static void ComponentMin( ref Vector3 a, ref Vector3 b, out Vector3 result )
        {
            result.x = a.x < b.x ? a.x : b.x;
            result.y = a.y < b.y ? a.y : b.y;
            result.z = a.z < b.z ? a.z : b.z;
        }

        public static Vector3 ComponentMax( Vector3 a, Vector3 b )
        {
            a.x = a.x > b.x ? a.x : b.x;
            a.y = a.y > b.y ? a.y : b.y;
            a.z = a.z > b.z ? a.z : b.z;
            return a;
        }

        public static void ComponentMax( ref Vector3 a, ref Vector3 b, out Vector3 result )
        {
            result.x = a.x > b.x ? a.x : b.x;
            result.y = a.y > b.y ? a.y : b.y;
            result.z = a.z > b.z ? a.z : b.z;
        }

        public static Vector3 Min( Vector3 left, Vector3 right )
        {
            if ( left.LengthSquared >= right.LengthSquared )
                return right;
            return left;
        }

        public static Vector3 Max( Vector3 left, Vector3 right )
        {
            if ( left.LengthSquared < right.LengthSquared )
                return right;
            return left;
        }

        public static Vector3 Clamp( Vector3 vec, Vector3 min, Vector3 max )
        {
            vec.x = vec.x < min.x ? min.x : ( vec.x > max.x ? max.x : vec.x );
            vec.y = vec.y < min.y ? min.y : ( vec.y > max.y ? max.y : vec.y );
            vec.z = vec.z < min.z ? min.z : ( vec.z > max.z ? max.z : vec.z );
            return vec;
        }

        public static void Clamp( ref Vector3 vec, ref Vector3 min, ref Vector3 max, out Vector3 result )
        {
            result.x = vec.x < min.x ? min.x : ( vec.x > max.x ? max.x : vec.x );
            result.y = vec.y < min.y ? min.y : ( vec.y > max.y ? max.y : vec.y );
            result.z = vec.z < min.z ? min.z : ( vec.z > max.z ? max.z : vec.z );
        }

        public static Vector3 Normalize( Vector3 vec )
        {
            double num = 1.0 / vec.Length;
            vec.x *= num;
            vec.y *= num;
            vec.z *= num;
            return vec;
        }

        public static void Normalize( ref Vector3 vec, out Vector3 result )
        {
            double num = 1.0 / vec.Length;
            result.x = vec.x * num;
            result.y = vec.y * num;
            result.z = vec.z * num;
        }

        public static Vector3 NormalizeFast( Vector3 vec )
        {
            double num = MathHelper.InverseSqrtFast(vec.x * vec.x + vec.y * vec.y + vec.z * vec.z);
            vec.x *= num;
            vec.y *= num;
            vec.z *= num;
            return vec;
        }

        public static void NormalizeFast( ref Vector3 vec, out Vector3 result )
        {
            double num = MathHelper.InverseSqrtFast(vec.x * vec.x + vec.y * vec.y + vec.z * vec.z);
            result.x = vec.x * num;
            result.y = vec.y * num;
            result.z = vec.z * num;
        }

        public static double Dot( Vector3 left, Vector3 right )
        {
            return left.x * right.x + left.y * right.y + left.z * right.z;
        }

        public static void Dot( ref Vector3 left, ref Vector3 right, out double result )
        {
            result = left.x * right.x + left.y * right.y + left.z * right.z;
        }

        public static Vector3 Cross( Vector3 left, Vector3 right )
        {
            Vector3 result;
            Vector3.Cross( ref left, ref right, out result );
            return result;
        }

        public static void Cross( ref Vector3 left, ref Vector3 right, out Vector3 result )
        {
            result = new Vector3( left.y * right.z - left.z * right.y, left.z * right.x - left.x * right.z, left.x * right.y - left.y * right.x );
        }

        public static Vector3 Lerp( Vector3 a, Vector3 b, double blend )
        {
            a.x = blend * ( b.x - a.x ) + a.x;
            a.y = blend * ( b.y - a.y ) + a.y;
            a.z = blend * ( b.z - a.z ) + a.z;
            return a;
        }

        public static void Lerp( ref Vector3 a, ref Vector3 b, double blend, out Vector3 result )
        {
            result.x = blend * ( b.x - a.x ) + a.x;
            result.y = blend * ( b.y - a.y ) + a.y;
            result.z = blend * ( b.z - a.z ) + a.z;
        }

        public static Vector3 BaryCentric( Vector3 a, Vector3 b, Vector3 c, double u, double v )
        {
            return a + u * ( b - a ) + v * ( c - a );
        }

        public static void BaryCentric( ref Vector3 a, ref Vector3 b, ref Vector3 c, double u, double v, out Vector3 result )
        {
            result = a;
            Vector3 result1 = b;
            Vector3.Subtract( ref result1, ref a, out result1 );
            Vector3.Multiply( ref result1, u, out result1 );
            Vector3.Add( ref result, ref result1, out result );
            Vector3 result2 = c;
            Vector3.Subtract( ref result2, ref a, out result2 );
            Vector3.Multiply( ref result2, v, out result2 );
            Vector3.Add( ref result, ref result2, out result );
        }

        public static Vector3 TransformVector( Vector3 vec, Matrix4X4 mat )
        {
            return new Vector3( Vector3.Dot( vec, new Vector3( mat.Column0 ) ), Vector3.Dot( vec, new Vector3( mat.Column1 ) ), Vector3.Dot( vec, new Vector3( mat.Column2 ) ) );
        }

        public static void TransformVector( ref Vector3 vec, ref Matrix4X4 mat, out Vector3 result )
        {
            result.x = vec.x * mat.Row0.x + vec.y * mat.Row1.x + vec.z * mat.Row2.x;
            result.y = vec.x * mat.Row0.y + vec.y * mat.Row1.y + vec.z * mat.Row2.y;
            result.z = vec.x * mat.Row0.z + vec.y * mat.Row1.z + vec.z * mat.Row2.z;
        }

        public static Vector3 TransformNormal( Vector3 norm, Matrix4X4 mat )
        {
            mat.Invert();
            return Vector3.TransformNormalInverse( norm, mat );
        }

        public static void TransformNormal( ref Vector3 norm, ref Matrix4X4 mat, out Vector3 result )
        {
            Matrix4X4 invMat = Matrix4X4.Invert(mat);
            Vector3.TransformNormalInverse( ref norm, ref invMat, out result );
        }

        public static Vector3 TransformNormalInverse( Vector3 norm, Matrix4X4 invMat )
        {
            return new Vector3( Vector3.Dot( norm, new Vector3( invMat.Row0 ) ), Vector3.Dot( norm, new Vector3( invMat.Row1 ) ), Vector3.Dot( norm, new Vector3( invMat.Row2 ) ) );
        }

        public static void TransformNormalInverse( ref Vector3 norm, ref Matrix4X4 invMat, out Vector3 result )
        {
            result.x = norm.x * invMat.Row0.x + norm.y * invMat.Row0.y + norm.z * invMat.Row0.z;
            result.y = norm.x * invMat.Row1.x + norm.y * invMat.Row1.y + norm.z * invMat.Row1.z;
            result.z = norm.x * invMat.Row2.x + norm.y * invMat.Row2.y + norm.z * invMat.Row2.z;
        }

        public static Vector3 TransformPosition( Vector3 pos, Matrix4X4 mat )
        {
            return new Vector3( Vector3.Dot( pos, new Vector3( mat.Column0 ) ) + mat.Row3.x, Vector3.Dot( pos, new Vector3( mat.Column1 ) ) + mat.Row3.y, Vector3.Dot( pos, new Vector3( mat.Column2 ) ) + mat.Row3.z );
        }

        public static void TransformPosition( ref Vector3 pos, ref Matrix4X4 mat, out Vector3 result )
        {
            result.x = pos.x * mat.Row0.x + pos.y * mat.Row1.x + pos.z * mat.Row2.x + mat.Row3.x;
            result.y = pos.x * mat.Row0.y + pos.y * mat.Row1.y + pos.z * mat.Row2.y + mat.Row3.y;
            result.z = pos.x * mat.Row0.z + pos.y * mat.Row1.z + pos.z * mat.Row2.z + mat.Row3.z;
        }

        public static void Transform( Vector3[ ] vecArray, Matrix4X4 mat )
        {
            for ( int index = 0 ; index < vecArray.Length ; ++index )
                vecArray[ index ] = Vector3.Transform( vecArray[ index ], mat );
        }

        public static Vector3 Transform( Vector3 vec, Matrix4X4 mat )
        {
            Vector3 result;
            Vector3.Transform( ref vec, ref mat, out result );
            return result;
        }

        public static void Transform( ref Vector3 vec, ref Matrix4X4 mat, out Vector3 result )
        {
            Vector4 result1 = new Vector4(vec.x, vec.y, vec.z, 1.0);
            Vector4.Transform( ref result1, ref mat, out result1 );
            result = result1.Xyz;
        }

        public static Vector3 Transform( Vector3 vec, Quaternion quat )
        {
            Vector3 result;
            Vector3.Transform( ref vec, ref quat, out result );
            return result;
        }

        public static void Transform( ref Vector3 vec, ref Quaternion quat, out Vector3 result )
        {
            Vector3 xyz = quat.Xyz;
            Vector3 result1;
            Vector3.Cross( ref xyz, ref vec, out result1 );
            Vector3 result2;
            Vector3.Multiply( ref vec, quat.W, out result2 );
            Vector3.Add( ref result1, ref result2, out result1 );
            Vector3.Cross( ref xyz, ref result1, out result1 );
            Vector3.Multiply( ref result1, 2.0, out result1 );
            Vector3.Add( ref vec, ref result1, out result );
        }

        public static void Transform( Vector3[ ] vecArray, Quaternion rotationQuaternion )
        {
            for ( int index = 0 ; index < vecArray.Length ; ++index )
                vecArray[ index ] = Vector3.Transform( vecArray[ index ], rotationQuaternion );
        }

        public static Vector3 TransformPerspective( Vector3 vec, Matrix4X4 mat )
        {
            Vector3 result;
            Vector3.TransformPerspective( ref vec, ref mat, out result );
            return result;
        }

        public static void TransformPerspective( ref Vector3 vec, ref Matrix4X4 mat, out Vector3 result )
        {
            Vector4 result1 = new Vector4(vec);
            Vector4.Transform( ref result1, ref mat, out result1 );
            result.x = result1.x / result1.w;
            result.y = result1.y / result1.w;
            result.z = result1.z / result1.w;
        }

        public static double CalculateAngle( Vector3 first, Vector3 second )
        {
            return Math.Acos( Vector3.Dot( first, second ) / ( first.Length * second.Length ) );
        }

        public static void CalculateAngle( ref Vector3 first, ref Vector3 second, out double result )
        {
            double result1;
            Vector3.Dot( ref first, ref second, out result1 );
            result = Math.Acos( result1 / ( first.Length * second.Length ) );
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

        public static Vector3 operator +( Vector3 left, Vector3 right )
        {
            left.x += right.x;
            left.y += right.y;
            left.z += right.z;
            return left;
        }

        public static Vector3 operator -( Vector3 left, Vector3 right )
        {
            left.x -= right.x;
            left.y -= right.y;
            left.z -= right.z;
            return left;
        }

        public static Vector3 operator -( Vector3 vec )
        {
            vec.x = -vec.x;
            vec.y = -vec.y;
            vec.z = -vec.z;
            return vec;
        }

        public static Vector3 operator *( Vector3 vecA, Vector3 vecB )
        {
            vecA.x *= vecB.x;
            vecA.y *= vecB.y;
            vecA.z *= vecB.z;
            return vecA;
        }

        public static Vector3 operator *( Vector3 vec, double scale )
        {
            vec.x *= scale;
            vec.y *= scale;
            vec.z *= scale;
            return vec;
        }

        public static Vector3 operator *( double scale, Vector3 vec )
        {
            vec.x *= scale;
            vec.y *= scale;
            vec.z *= scale;
            return vec;
        }

        public static Vector3 operator /( double numerator, Vector3 vec )
        {
            return new Vector3( numerator / vec.x, numerator / vec.y, numerator / vec.z );
        }

        public static Vector3 operator /( Vector3 vec, double scale )
        {
            double num = 1.0 / scale;
            vec.x *= num;
            vec.y *= num;
            vec.z *= num;
            return vec;
        }

        public static bool operator ==( Vector3 left, Vector3 right )
        {
            return left.Equals( right );
        }

        public static bool operator !=( Vector3 left, Vector3 right )
        {
            return !left.Equals( right );
        }

        public override string ToString()
        {
            return string.Format( "[{0}, {1}, {2}]", ( object ) this.x, ( object ) this.y, ( object ) this.z );
        }

        public override int GetHashCode()
        {
            return new double[ 3 ] { this.x, this.y, this.z }.GetHashCode();
        }

        public override bool Equals( object obj )
        {
            if ( !( obj is Vector3 ) )
                return false;
            return this.Equals( ( Vector3 ) obj );
        }

        public bool Equals( Vector3 OtherVector, double ErrorValue )
        {
            return this.x < OtherVector.x + ErrorValue && this.x > OtherVector.x - ErrorValue && ( this.y < OtherVector.y + ErrorValue && this.y > OtherVector.y - ErrorValue ) && ( this.z < OtherVector.z + ErrorValue && this.z > OtherVector.z - ErrorValue );
        }

        public bool Equals( Vector3 other )
        {
            if ( this.x == other.x && this.y == other.y )
                return this.z == other.z;
            return false;
        }

        public static double ComponentMax( Vector3 vector3 )
        {
            return Math.Max( vector3.x, Math.Max( vector3.y, vector3.z ) );
        }

        public static double ComponentMin( Vector3 vector3 )
        {
            return Math.Min( vector3.x, Math.Min( vector3.y, vector3.z ) );
        }
    }
}
