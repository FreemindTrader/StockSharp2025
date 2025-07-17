// Decompiled with JetBrains decompiler
// Type: MatterHackers.VectorMath.Matrix4X4
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\fx.Xaml.Charting.dll

using System;

namespace MatterHackers.VectorMath
{
    internal struct Matrix4X4 : IEquatable<Matrix4X4>
    {
        public static Matrix4X4 Identity = new Matrix4X4(Vector4.UnitX, Vector4.UnitY, Vector4.UnitZ, Vector4.UnitW);
        public Vector4 Row0;
        public Vector4 Row1;
        public Vector4 Row2;
        public Vector4 Row3;

        public Matrix4X4( Vector4 row0, Vector4 row1, Vector4 row2, Vector4 row3 )
        {
            Row0 = row0;
            Row1 = row1;
            Row2 = row2;
            Row3 = row3;
        }

        public Matrix4X4( double m00, double m01, double m02, double m03, double m10, double m11, double m12, double m13, double m20, double m21, double m22, double m23, double m30, double m31, double m32, double m33 )
        {
            Row0 = new Vector4( m00, m01, m02, m03 );
            Row1 = new Vector4( m10, m11, m12, m13 );
            Row2 = new Vector4( m20, m21, m22, m23 );
            Row3 = new Vector4( m30, m31, m32, m33 );
        }

        public double Determinant
        {
            get
            {
                return Row0.x * Row1.y * Row2.z * Row3.w - Row0.x * Row1.y * Row2.w * Row3.z + Row0.x * Row1.z * Row2.w * Row3.y - Row0.x * Row1.z * Row2.y * Row3.w + Row0.x * Row1.w * Row2.y * Row3.z - Row0.x * Row1.w * Row2.z * Row3.y - Row0.y * Row1.z * Row2.w * Row3.x + Row0.y * Row1.z * Row2.x * Row3.w - Row0.y * Row1.w * Row2.x * Row3.z + Row0.y * Row1.w * Row2.z * Row3.x - Row0.y * Row1.x * Row2.z * Row3.w + Row0.y * Row1.x * Row2.w * Row3.z + Row0.z * Row1.w * Row2.x * Row3.y - Row0.z * Row1.w * Row2.y * Row3.x + Row0.z * Row1.x * Row2.y * Row3.w - Row0.z * Row1.x * Row2.w * Row3.y + Row0.z * Row1.y * Row2.w * Row3.x - Row0.z * Row1.y * Row2.x * Row3.w - Row0.w * Row1.x * Row2.y * Row3.z + Row0.w * Row1.x * Row2.z * Row3.y - Row0.w * Row1.y * Row2.z * Row3.x + Row0.w * Row1.y * Row2.x * Row3.z - Row0.w * Row1.z * Row2.x * Row3.y + Row0.w * Row1.z * Row2.y * Row3.x;
            }
        }

        public Vector3 Position
        {
            get
            {
                return new Vector3( Row3 );
            }
        }

        public Vector4 Column0
        {
            get
            {
                return new Vector4( Row0.x, Row1.x, Row2.x, Row3.x );
            }
        }

        public Vector4 Column1
        {
            get
            {
                return new Vector4( Row0.y, Row1.y, Row2.y, Row3.y );
            }
        }

        public Vector4 Column2
        {
            get
            {
                return new Vector4( Row0.z, Row1.z, Row2.z, Row3.z );
            }
        }

        public Vector4 Column3
        {
            get
            {
                return new Vector4( Row0.w, Row1.w, Row2.w, Row3.w );
            }
        }

        public double M11
        {
            get
            {
                return Row0.x;
            }
            set
            {
                Row0.x = value;
            }
        }

        public double M12
        {
            get
            {
                return Row0.y;
            }
            set
            {
                Row0.y = value;
            }
        }

        public double M13
        {
            get
            {
                return Row0.z;
            }
            set
            {
                Row0.z = value;
            }
        }

        public double M14
        {
            get
            {
                return Row0.w;
            }
            set
            {
                Row0.w = value;
            }
        }

        public double M21
        {
            get
            {
                return Row1.x;
            }
            set
            {
                Row1.x = value;
            }
        }

        public double M22
        {
            get
            {
                return Row1.y;
            }
            set
            {
                Row1.y = value;
            }
        }

        public double M23
        {
            get
            {
                return Row1.z;
            }
            set
            {
                Row1.z = value;
            }
        }

        public double M24
        {
            get
            {
                return Row1.w;
            }
            set
            {
                Row1.w = value;
            }
        }

        public double M31
        {
            get
            {
                return Row2.x;
            }
            set
            {
                Row2.x = value;
            }
        }

        public double M32
        {
            get
            {
                return Row2.y;
            }
            set
            {
                Row2.y = value;
            }
        }

        public double M33
        {
            get
            {
                return Row2.z;
            }
            set
            {
                Row2.z = value;
            }
        }

        public double M34
        {
            get
            {
                return Row2.w;
            }
            set
            {
                Row2.w = value;
            }
        }

        public double M41
        {
            get
            {
                return Row3.x;
            }
            set
            {
                Row3.x = value;
            }
        }

        public double M42
        {
            get
            {
                return Row3.y;
            }
            set
            {
                Row3.y = value;
            }
        }

        public double M43
        {
            get
            {
                return Row3.z;
            }
            set
            {
                Row3.z = value;
            }
        }

        public double M44
        {
            get
            {
                return Row3.w;
            }
            set
            {
                Row3.w = value;
            }
        }

        public double this[ int row, int column ]
        {
            get
            {
                switch ( row )
                {
                    case 0:
                        return Row0[ column ];
                    case 1:
                        return Row1[ column ];
                    case 2:
                        return Row2[ column ];
                    case 3:
                        return Row3[ column ];
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
            set
            {
                switch ( row )
                {
                    case 0:
                        Row0[ column ] = value;
                        break;
                    case 1:
                        Row1[ column ] = value;
                        break;
                    case 2:
                        Row2[ column ] = value;
                        break;
                    case 3:
                        Row3[ column ] = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        public Quaternion GetRotation()
        {
            Quaternion identity = Quaternion.Identity;
            double num1 = M11 + M22 + M33;
            if ( num1 > 0.0 )
            {
                double num2 = Math.Sqrt(num1 + 1.0) * 2.0;
                identity.W = 0.25 * num2;
                identity.X = ( M32 - M23 ) / num2;
                identity.Y = ( M13 - M31 ) / num2;
                identity.Z = ( M21 - M12 ) / num2;
            }
            else if ( M11 > M22 & M11 > M33 )
            {
                double num2 = Math.Sqrt(1.0 + M11 - M22 - M33) * 2.0;
                identity.W = ( M32 - M23 ) / num2;
                identity.X = 0.25 * num2;
                identity.Y = ( M12 + M21 ) / num2;
                identity.Z = ( M13 + M31 ) / num2;
            }
            else if ( M22 > M33 )
            {
                double num2 = Math.Sqrt(1.0 + M22 - M11 - M33) * 2.0;
                identity.W = ( M13 - M31 ) / num2;
                identity.X = ( M12 + M21 ) / num2;
                identity.Y = 0.25 * num2;
                identity.Z = ( M23 + M32 ) / num2;
            }
            else
            {
                double num2 = Math.Sqrt(1.0 + M33 - M11 - M22) * 2.0;
                identity.W = ( M21 - M12 ) / num2;
                identity.X = ( M13 + M31 ) / num2;
                identity.Y = ( M23 + M32 ) / num2;
                identity.Z = 0.25 * num2;
            }
            return identity;
        }

        public void Invert()
        {
            this = Matrix4X4.Invert( this );
        }

        public void Transpose()
        {
            this = Matrix4X4.Transpose( this );
        }

        public static void CreateFromAxisAngle( Vector3 axis, double angle, out Matrix4X4 result )
        {
            double num1 = Math.Cos(-angle);
            double num2 = Math.Sin(-angle);
            double num3 = 1.0 - num1;
            axis.Normalize();
            result = new Matrix4X4( num3 * axis.x * axis.x + num1, num3 * axis.x * axis.y - num2 * axis.z, num3 * axis.x * axis.z + num2 * axis.y, 0.0, num3 * axis.x * axis.y + num2 * axis.z, num3 * axis.y * axis.y + num1, num3 * axis.y * axis.z - num2 * axis.x, 0.0, num3 * axis.x * axis.z - num2 * axis.y, num3 * axis.y * axis.z + num2 * axis.x, num3 * axis.z * axis.z + num1, 0.0, 0.0, 0.0, 0.0, 1.0 );
        }

        public static Matrix4X4 CreateFromAxisAngle( Vector3 axis, double angle )
        {
            Matrix4X4 result;
            Matrix4X4.CreateFromAxisAngle( axis, angle, out result );
            return result;
        }

        public static Matrix4X4 CreateRotation( Vector3 radians )
        {
            return Matrix4X4.CreateRotation( Quaternion.FromEulerAngles( radians ) );
        }

        public static void CreateRotationX( double angle, out Matrix4X4 result )
        {
            double num = Math.Cos(angle);
            double z = Math.Sin(angle);
            result.Row0 = Vector4.UnitX;
            result.Row1 = new Vector4( 0.0, num, z, 0.0 );
            result.Row2 = new Vector4( 0.0, -z, num, 0.0 );
            result.Row3 = Vector4.UnitW;
        }

        public static Matrix4X4 CreateRotationX( double angle )
        {
            Matrix4X4 result;
            Matrix4X4.CreateRotationX( angle, out result );
            return result;
        }

        public static void CreateRotationY( double angle, out Matrix4X4 result )
        {
            double num = Math.Cos(angle);
            double x = Math.Sin(angle);
            result.Row0 = new Vector4( num, 0.0, -x, 0.0 );
            result.Row1 = Vector4.UnitY;
            result.Row2 = new Vector4( x, 0.0, num, 0.0 );
            result.Row3 = Vector4.UnitW;
        }

        public static Matrix4X4 CreateRotationY( double angle )
        {
            Matrix4X4 result;
            Matrix4X4.CreateRotationY( angle, out result );
            return result;
        }

        public static void CreateRotationZ( double angle, out Matrix4X4 result )
        {
            double num = Math.Cos(angle);
            double y = Math.Sin(angle);
            result.Row0 = new Vector4( num, y, 0.0, 0.0 );
            result.Row1 = new Vector4( -y, num, 0.0, 0.0 );
            result.Row2 = Vector4.UnitZ;
            result.Row3 = Vector4.UnitW;
        }

        public static Matrix4X4 CreateRotationZ( double angle )
        {
            Matrix4X4 result;
            Matrix4X4.CreateRotationZ( angle, out result );
            return result;
        }

        public static Matrix4X4 CreateRotation( Vector3 axis, double angle )
        {
            double num1 = Math.Cos(-angle);
            double num2 = Math.Sin(-angle);
            double num3 = 1.0 - num1;
            axis.Normalize();
            Matrix4X4 matrix4X4;
            matrix4X4.Row0 = new Vector4( num3 * axis.x * axis.x + num1, num3 * axis.x * axis.y - num2 * axis.z, num3 * axis.x * axis.z + num2 * axis.y, 0.0 );
            matrix4X4.Row1 = new Vector4( num3 * axis.x * axis.y + num2 * axis.z, num3 * axis.y * axis.y + num1, num3 * axis.y * axis.z - num2 * axis.x, 0.0 );
            matrix4X4.Row2 = new Vector4( num3 * axis.x * axis.z - num2 * axis.y, num3 * axis.y * axis.z + num2 * axis.x, num3 * axis.z * axis.z + num1, 0.0 );
            matrix4X4.Row3 = Vector4.UnitW;
            return matrix4X4;
        }

        public static Matrix4X4 CreateRotation( Quaternion q )
        {
            Vector3 axis;
            double angle;
            q.ToAxisAngle( out axis, out angle );
            return Matrix4X4.CreateRotation( axis, angle );
        }

        public static void CreateTranslation( double x, double y, double z, out Matrix4X4 result )
        {
            result = Matrix4X4.Identity;
            result.Row3 = new Vector4( x, y, z, 1.0 );
        }

        public static void CreateTranslation( ref Vector3 vector, out Matrix4X4 result )
        {
            result = Matrix4X4.Identity;
            result.Row3 = new Vector4( vector.x, vector.y, vector.z, 1.0 );
        }

        public static Matrix4X4 CreateTranslation( double x, double y, double z )
        {
            Matrix4X4 result;
            Matrix4X4.CreateTranslation( x, y, z, out result );
            return result;
        }

        public static Matrix4X4 CreateTranslation( Vector3 vector )
        {
            Matrix4X4 result;
            Matrix4X4.CreateTranslation( vector.x, vector.y, vector.z, out result );
            return result;
        }

        public static void CreateOrthographic( double width, double height, double zNear, double zFar, out Matrix4X4 result )
        {
            Matrix4X4.CreateOrthographicOffCenter( -width / 2.0, width / 2.0, -height / 2.0, height / 2.0, zNear, zFar, out result );
        }

        public static Matrix4X4 CreateOrthographic( double width, double height, double zNear, double zFar )
        {
            Matrix4X4 result;
            Matrix4X4.CreateOrthographicOffCenter( -width / 2.0, width / 2.0, -height / 2.0, height / 2.0, zNear, zFar, out result );
            return result;
        }

        public static void CreateOrthographicOffCenter( double left, double right, double bottom, double top, double zNear, double zFar, out Matrix4X4 result )
        {
            result = new Matrix4X4();
            double num1 = 1.0 / (right - left);
            double num2 = 1.0 / (top - bottom);
            double num3 = 1.0 / (zFar - zNear);
            result.M11 = 2.0 * num1;
            result.M22 = 2.0 * num2;
            result.M33 = -2.0 * num3;
            result.M41 = -( right + left ) * num1;
            result.M42 = -( top + bottom ) * num2;
            result.M43 = -( zFar + zNear ) * num3;
            result.M44 = 1.0;
        }

        public static Matrix4X4 CreateOrthographicOffCenter( double left, double right, double bottom, double top, double zNear, double zFar )
        {
            Matrix4X4 result;
            Matrix4X4.CreateOrthographicOffCenter( left, right, bottom, top, zNear, zFar, out result );
            return result;
        }

        public static void CreatePerspectiveFieldOfView( double fovYRadians, double aspectWidthOverHeight, double zNear, double zFar, out Matrix4X4 result )
        {
            if ( fovYRadians <= 0.0 || fovYRadians > Math.PI )
                throw new ArgumentOutOfRangeException( "fovy" );
            if ( aspectWidthOverHeight <= 0.0 )
                throw new ArgumentOutOfRangeException( "aspect" );
            if ( zNear <= 0.0 )
                throw new ArgumentOutOfRangeException( nameof( zNear ) );
            if ( zFar <= 0.0 )
                throw new ArgumentOutOfRangeException( nameof( zFar ) );
            if ( zNear >= zFar )
                throw new ArgumentOutOfRangeException( nameof( zNear ) );
            double top = zNear * Math.Tan(0.5 * fovYRadians);
            double bottom = -top;
            Matrix4X4.CreatePerspectiveOffCenter( bottom * aspectWidthOverHeight, top * aspectWidthOverHeight, bottom, top, zNear, zFar, out result );
        }

        public static Matrix4X4 CreatePerspectiveFieldOfView( double fovYRadians, double aspectWidthOverHeight, double zNear, double zFar )
        {
            Matrix4X4 result;
            Matrix4X4.CreatePerspectiveFieldOfView( fovYRadians, aspectWidthOverHeight, zNear, zFar, out result );
            return result;
        }

        public static void CreatePerspectiveOffCenter( double left, double right, double bottom, double top, double zNear, double zFar, out Matrix4X4 result )
        {
            if ( zNear <= 0.0 )
                throw new ArgumentOutOfRangeException( nameof( zNear ) );
            if ( zFar <= 0.0 )
                throw new ArgumentOutOfRangeException( nameof( zFar ) );
            if ( zNear >= zFar )
                throw new ArgumentOutOfRangeException( nameof( zNear ) );
            double m00 = 2.0 * zNear / (right - left);
            double m11 = 2.0 * zNear / (top - bottom);
            double m20 = (right + left) / (right - left);
            double m21 = (top + bottom) / (top - bottom);
            double m22 = -(zFar + zNear) / (zFar - zNear);
            double m32 = -(2.0 * zFar * zNear) / (zFar - zNear);
            result = new Matrix4X4( m00, 0.0, 0.0, 0.0, 0.0, m11, 0.0, 0.0, m20, m21, m22, -1.0, 0.0, 0.0, m32, 0.0 );
        }

        public static Matrix4X4 CreatePerspectiveOffCenter( double left, double right, double bottom, double top, double zNear, double zFar )
        {
            Matrix4X4 result;
            Matrix4X4.CreatePerspectiveOffCenter( left, right, bottom, top, zNear, zFar, out result );
            return result;
        }

        public static Matrix4X4 CreateScale( double scale )
        {
            return Matrix4X4.CreateScale( scale, scale, scale );
        }

        public static Matrix4X4 CreateScale( Vector3 scale )
        {
            return Matrix4X4.CreateScale( scale.x, scale.y, scale.z );
        }

        public static Matrix4X4 CreateScale( double x, double y, double z )
        {
            Matrix4X4 matrix4X4;
            matrix4X4.Row0 = Vector4.UnitX * x;
            matrix4X4.Row1 = Vector4.UnitY * y;
            matrix4X4.Row2 = Vector4.UnitZ * z;
            matrix4X4.Row3 = Vector4.UnitW;
            return matrix4X4;
        }

        public static Matrix4X4 LookAt( Vector3 eye, Vector3 target, Vector3 up )
        {
            Vector3 vector3_1 = Vector3.Normalize(eye - target);
            Vector3 right = Vector3.Normalize(Vector3.Cross(up, vector3_1));
            Vector3 vector3_2 = Vector3.Normalize(Vector3.Cross(vector3_1, right));
            Matrix4X4 matrix4X4 = new Matrix4X4(new Vector4(right.x, vector3_2.x, vector3_1.x, 0.0), new Vector4(right.y, vector3_2.y, vector3_1.y, 0.0), new Vector4(right.z, vector3_2.z, vector3_1.z, 0.0), Vector4.UnitW);
            return Matrix4X4.CreateTranslation( -eye ) * matrix4X4;
        }

        public static Matrix4X4 LookAt( double eyeX, double eyeY, double eyeZ, double targetX, double targetY, double targetZ, double upX, double upY, double upZ )
        {
            return Matrix4X4.LookAt( new Vector3( eyeX, eyeY, eyeZ ), new Vector3( targetX, targetY, targetZ ), new Vector3( upX, upY, upZ ) );
        }

        public static Matrix4X4 Frustum( double left, double right, double bottom, double top, double near, double far )
        {
            double num1 = 1.0 / (right - left);
            double num2 = 1.0 / (top - bottom);
            double num3 = 1.0 / (far - near);
            return new Matrix4X4( new Vector4( 2.0 * near * num1, 0.0, 0.0, 0.0 ), new Vector4( 0.0, 2.0 * near * num2, 0.0, 0.0 ), new Vector4( ( right + left ) * num1, ( top + bottom ) * num2, -( far + near ) * num3, -1.0 ), new Vector4( 0.0, 0.0, -2.0 * far * near * num3, 0.0 ) );
        }

        public static Matrix4X4 Perspective( double fovy, double aspect, double near, double far )
        {
            double top = near * Math.Tan(0.5 * fovy);
            double bottom = -top;
            return Matrix4X4.Frustum( bottom * aspect, top * aspect, bottom, top, near, far );
        }

        public static Matrix4X4 Mult( Matrix4X4 left, Matrix4X4 right )
        {
            Matrix4X4 result;
            Matrix4X4.Mult( ref left, ref right, out result );
            return result;
        }

        public static void Mult( ref Matrix4X4 left, ref Matrix4X4 right, out Matrix4X4 result )
        {
            result = new Matrix4X4();
            result.M11 = left.M11 * right.M11 + left.M12 * right.M21 + left.M13 * right.M31 + left.M14 * right.M41;
            result.M12 = left.M11 * right.M12 + left.M12 * right.M22 + left.M13 * right.M32 + left.M14 * right.M42;
            result.M13 = left.M11 * right.M13 + left.M12 * right.M23 + left.M13 * right.M33 + left.M14 * right.M43;
            result.M14 = left.M11 * right.M14 + left.M12 * right.M24 + left.M13 * right.M34 + left.M14 * right.M44;
            result.M21 = left.M21 * right.M11 + left.M22 * right.M21 + left.M23 * right.M31 + left.M24 * right.M41;
            result.M22 = left.M21 * right.M12 + left.M22 * right.M22 + left.M23 * right.M32 + left.M24 * right.M42;
            result.M23 = left.M21 * right.M13 + left.M22 * right.M23 + left.M23 * right.M33 + left.M24 * right.M43;
            result.M24 = left.M21 * right.M14 + left.M22 * right.M24 + left.M23 * right.M34 + left.M24 * right.M44;
            result.M31 = left.M31 * right.M11 + left.M32 * right.M21 + left.M33 * right.M31 + left.M34 * right.M41;
            result.M32 = left.M31 * right.M12 + left.M32 * right.M22 + left.M33 * right.M32 + left.M34 * right.M42;
            result.M33 = left.M31 * right.M13 + left.M32 * right.M23 + left.M33 * right.M33 + left.M34 * right.M43;
            result.M34 = left.M31 * right.M14 + left.M32 * right.M24 + left.M33 * right.M34 + left.M34 * right.M44;
            result.M41 = left.M41 * right.M11 + left.M42 * right.M21 + left.M43 * right.M31 + left.M44 * right.M41;
            result.M42 = left.M41 * right.M12 + left.M42 * right.M22 + left.M43 * right.M32 + left.M44 * right.M42;
            result.M43 = left.M41 * right.M13 + left.M42 * right.M23 + left.M43 * right.M33 + left.M44 * right.M43;
            result.M44 = left.M41 * right.M14 + left.M42 * right.M24 + left.M43 * right.M34 + left.M44 * right.M44;
        }

        public static Matrix4X4 Invert( Matrix4X4 mat )
        {
            int[] numArray1 = new int[4];
            int[] numArray2 = new int[4];
            int[] numArray3 = new int[4]{ -1, -1, -1, -1 };
            double[,] numArray4 = new double[4, 4]
      {
        {
          mat.Row0.x,
          mat.Row0.y,
          mat.Row0.z,
          mat.Row0.w
        },
        {
          mat.Row1.x,
          mat.Row1.y,
          mat.Row1.z,
          mat.Row1.w
        },
        {
          mat.Row2.x,
          mat.Row2.y,
          mat.Row2.z,
          mat.Row2.w
        },
        {
          mat.Row3.x,
          mat.Row3.y,
          mat.Row3.z,
          mat.Row3.w
        }
      };
            int index1 = 0;
            int index2 = 0;
            for ( int index3 = 0 ; index3 < 4 ; ++index3 )
            {
                double num1 = 0.0;
                for ( int index4 = 0 ; index4 < 4 ; ++index4 )
                {
                    if ( numArray3[ index4 ] != 0 )
                    {
                        for ( int index5 = 0 ; index5 < 4 ; ++index5 )
                        {
                            if ( numArray3[ index5 ] == -1 )
                            {
                                double num2 = Math.Abs(numArray4[index4, index5]);
                                if ( num2 > num1 )
                                {
                                    num1 = num2;
                                    index2 = index4;
                                    index1 = index5;
                                }
                            }
                            else if ( numArray3[ index5 ] > 0 )
                                return mat;
                        }
                    }
                }
                ++numArray3[ index1 ];
                if ( index2 != index1 )
                {
                    for ( int index4 = 0 ; index4 < 4 ; ++index4 )
                    {
                        double num2 = numArray4[index2, index4];
                        numArray4[ index2, index4 ] = numArray4[ index1, index4 ];
                        numArray4[ index1, index4 ] = num2;
                    }
                }
                numArray2[ index3 ] = index2;
                numArray1[ index3 ] = index1;
                double num3 = numArray4[index1, index1];
                if ( num3 == 0.0 )
                    throw new InvalidOperationException( "Matrix is singular and cannot be inverted." );
                double num4 = 1.0 / num3;
                numArray4[ index1, index1 ] = 1.0;
                for ( int index4 = 0 ; index4 < 4 ; ++index4 )
                    numArray4[ index1, index4 ] *= num4;
                for ( int index4 = 0 ; index4 < 4 ; ++index4 )
                {
                    if ( index1 != index4 )
                    {
                        double num2 = numArray4[index4, index1];
                        numArray4[ index4, index1 ] = 0.0;
                        for ( int index5 = 0 ; index5 < 4 ; ++index5 )
                            numArray4[ index4, index5 ] -= numArray4[ index1, index5 ] * num2;
                    }
                }
            }
            for ( int index3 = 3 ; index3 >= 0 ; --index3 )
            {
                int index4 = numArray2[index3];
                int index5 = numArray1[index3];
                for ( int index6 = 0 ; index6 < 4 ; ++index6 )
                {
                    double num = numArray4[index6, index4];
                    numArray4[ index6, index4 ] = numArray4[ index6, index5 ];
                    numArray4[ index6, index5 ] = num;
                }
            }
            mat.Row0 = new Vector4( numArray4[ 0, 0 ], numArray4[ 0, 1 ], numArray4[ 0, 2 ], numArray4[ 0, 3 ] );
            mat.Row1 = new Vector4( numArray4[ 1, 0 ], numArray4[ 1, 1 ], numArray4[ 1, 2 ], numArray4[ 1, 3 ] );
            mat.Row2 = new Vector4( numArray4[ 2, 0 ], numArray4[ 2, 1 ], numArray4[ 2, 2 ], numArray4[ 2, 3 ] );
            mat.Row3 = new Vector4( numArray4[ 3, 0 ], numArray4[ 3, 1 ], numArray4[ 3, 2 ], numArray4[ 3, 3 ] );
            return mat;
        }

        public static Matrix4X4 Transpose( Matrix4X4 mat )
        {
            return new Matrix4X4( mat.Column0, mat.Column1, mat.Column2, mat.Column3 );
        }

        public static void Transpose( ref Matrix4X4 mat, out Matrix4X4 result )
        {
            result.Row0 = mat.Column0;
            result.Row1 = mat.Column1;
            result.Row2 = mat.Column2;
            result.Row3 = mat.Column3;
        }

        public static Matrix4X4 operator *( Matrix4X4 left, Matrix4X4 right )
        {
            return Matrix4X4.Mult( left, right );
        }

        public static bool operator ==( Matrix4X4 left, Matrix4X4 right )
        {
            return left.Equals( right );
        }

        public static bool operator !=( Matrix4X4 left, Matrix4X4 right )
        {
            return !left.Equals( right );
        }

        public override string ToString()
        {
            return string.Format( "{0}\n{1}\n{2}\n{3}", Row0, Row1, Row2, Row3 );
        }

        public override int GetHashCode()
        {
            return new Vector4[ 4 ]
            {
        Row0,
        Row1,
        Row2,
        Row3
            }.GetHashCode();
        }

        public override bool Equals( object obj )
        {
            if ( !( obj is Matrix4X4 ) )
                return false;
            return Equals( ( Matrix4X4 ) obj );
        }

        public bool Equals( Matrix4X4 other )
        {
            if ( Row0 == other.Row0 && Row1 == other.Row1 && Row2 == other.Row2 )
                return Row3 == other.Row3;
            return false;
        }

        public double[ ] GetAsDoubleArray()
        {
            return new double[ 16 ]
            {
        Row0[0],
        Row0[1],
        Row0[2],
        Row0[3],
        Row1[0],
        Row1[1],
        Row1[2],
        Row1[3],
        Row2[0],
        Row2[1],
        Row2[2],
        Row2[3],
        Row3[0],
        Row3[1],
        Row3[2],
        Row3[3]
            };
        }
    }
}
