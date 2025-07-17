// Decompiled with JetBrains decompiler
// Type: AGG.Matrix4X4
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using MatterHackers.VectorMath;

namespace AGG
{
    internal class Matrix4X4
    {
        private double[] matrix = new double[16];

        public Matrix4X4()
        {
            this.SetElement( 0, 0, 1.0 );
            this.SetElement( 1, 1, 1.0 );
            this.SetElement( 2, 2, 1.0 );
            this.SetElement( 3, 3, 1.0 );
        }

        public Matrix4X4( Matrix4X4 CopyFrom )
        {
            for ( int index = 0 ; index < 16 ; ++index )
                this.matrix[ index ] = CopyFrom.matrix[ index ];
        }

        public Matrix4X4( double[ ] CopyFrom )
        {
            this.SetElements( CopyFrom );
        }

        public double this[ int index ]
        {
            get
            {
                return this.matrix[ index ];
            }
            set
            {
                this.matrix[ index ] = value;
            }
        }

        public double this[ int row, int column ]
        {
            get
            {
                return this.GetElement( row, column );
            }
            set
            {
                this.SetElement( row, column, value );
            }
        }

        public double GetElement( int Row, int Column )
        {
            return this.matrix[ Row * 4 + Column ];
        }

        public void SetElement( int Row, int Column, double Value )
        {
            this.matrix[ Row * 4 + Column ] = Value;
        }

        public void AddElement( int Row, int Column, double Value )
        {
            this.matrix[ Row * 4 + Column ] += Value;
        }

        public void Identity()
        {
            this.Zero();
            this.SetElement( 0, 0, 1.0 );
            this.SetElement( 1, 1, 1.0 );
            this.SetElement( 2, 2, 1.0 );
            this.SetElement( 3, 3, 1.0 );
        }

        public void Zero()
        {
            for ( int index = 0 ; index < 16 ; ++index )
                this.matrix[ index ] = 0.0;
        }

        private static void IntelInvertC( double[ ] matrixToInvert, double[ ] destMatrix )
        {
            double[] numArray1 = new double[12];
            double[] numArray2 = new double[16];
            for ( int index = 0 ; index < 4 ; ++index )
            {
                numArray2[ index ] = matrixToInvert[ index * 4 ];
                numArray2[ index + 4 ] = matrixToInvert[ index * 4 + 1 ];
                numArray2[ index + 8 ] = matrixToInvert[ index * 4 + 2 ];
                numArray2[ index + 12 ] = matrixToInvert[ index * 4 + 3 ];
            }
            numArray1[ 0 ] = numArray2[ 10 ] * numArray2[ 15 ];
            numArray1[ 1 ] = numArray2[ 11 ] * numArray2[ 14 ];
            numArray1[ 2 ] = numArray2[ 9 ] * numArray2[ 15 ];
            numArray1[ 3 ] = numArray2[ 11 ] * numArray2[ 13 ];
            numArray1[ 4 ] = numArray2[ 9 ] * numArray2[ 14 ];
            numArray1[ 5 ] = numArray2[ 10 ] * numArray2[ 13 ];
            numArray1[ 6 ] = numArray2[ 8 ] * numArray2[ 15 ];
            numArray1[ 7 ] = numArray2[ 11 ] * numArray2[ 12 ];
            numArray1[ 8 ] = numArray2[ 8 ] * numArray2[ 14 ];
            numArray1[ 9 ] = numArray2[ 10 ] * numArray2[ 12 ];
            numArray1[ 10 ] = numArray2[ 8 ] * numArray2[ 13 ];
            numArray1[ 11 ] = numArray2[ 9 ] * numArray2[ 12 ];
            destMatrix[ 0 ] = numArray1[ 0 ] * numArray2[ 5 ] + numArray1[ 3 ] * numArray2[ 6 ] + numArray1[ 4 ] * numArray2[ 7 ];
            destMatrix[ 0 ] -= numArray1[ 1 ] * numArray2[ 5 ] + numArray1[ 2 ] * numArray2[ 6 ] + numArray1[ 5 ] * numArray2[ 7 ];
            destMatrix[ 1 ] = numArray1[ 1 ] * numArray2[ 4 ] + numArray1[ 6 ] * numArray2[ 6 ] + numArray1[ 9 ] * numArray2[ 7 ];
            destMatrix[ 1 ] -= numArray1[ 0 ] * numArray2[ 4 ] + numArray1[ 7 ] * numArray2[ 6 ] + numArray1[ 8 ] * numArray2[ 7 ];
            destMatrix[ 2 ] = numArray1[ 2 ] * numArray2[ 4 ] + numArray1[ 7 ] * numArray2[ 5 ] + numArray1[ 10 ] * numArray2[ 7 ];
            destMatrix[ 2 ] -= numArray1[ 3 ] * numArray2[ 4 ] + numArray1[ 6 ] * numArray2[ 5 ] + numArray1[ 11 ] * numArray2[ 7 ];
            destMatrix[ 3 ] = numArray1[ 5 ] * numArray2[ 4 ] + numArray1[ 8 ] * numArray2[ 5 ] + numArray1[ 11 ] * numArray2[ 6 ];
            destMatrix[ 3 ] -= numArray1[ 4 ] * numArray2[ 4 ] + numArray1[ 9 ] * numArray2[ 5 ] + numArray1[ 10 ] * numArray2[ 6 ];
            destMatrix[ 4 ] = numArray1[ 1 ] * numArray2[ 1 ] + numArray1[ 2 ] * numArray2[ 2 ] + numArray1[ 5 ] * numArray2[ 3 ];
            destMatrix[ 4 ] -= numArray1[ 0 ] * numArray2[ 1 ] + numArray1[ 3 ] * numArray2[ 2 ] + numArray1[ 4 ] * numArray2[ 3 ];
            destMatrix[ 5 ] = numArray1[ 0 ] * numArray2[ 0 ] + numArray1[ 7 ] * numArray2[ 2 ] + numArray1[ 8 ] * numArray2[ 3 ];
            destMatrix[ 5 ] -= numArray1[ 1 ] * numArray2[ 0 ] + numArray1[ 6 ] * numArray2[ 2 ] + numArray1[ 9 ] * numArray2[ 3 ];
            destMatrix[ 6 ] = numArray1[ 3 ] * numArray2[ 0 ] + numArray1[ 6 ] * numArray2[ 1 ] + numArray1[ 11 ] * numArray2[ 3 ];
            destMatrix[ 6 ] -= numArray1[ 2 ] * numArray2[ 0 ] + numArray1[ 7 ] * numArray2[ 1 ] + numArray1[ 10 ] * numArray2[ 3 ];
            destMatrix[ 7 ] = numArray1[ 4 ] * numArray2[ 0 ] + numArray1[ 9 ] * numArray2[ 1 ] + numArray1[ 10 ] * numArray2[ 2 ];
            destMatrix[ 7 ] -= numArray1[ 5 ] * numArray2[ 0 ] + numArray1[ 8 ] * numArray2[ 1 ] + numArray1[ 11 ] * numArray2[ 2 ];
            numArray1[ 0 ] = numArray2[ 2 ] * numArray2[ 7 ];
            numArray1[ 1 ] = numArray2[ 3 ] * numArray2[ 6 ];
            numArray1[ 2 ] = numArray2[ 1 ] * numArray2[ 7 ];
            numArray1[ 3 ] = numArray2[ 3 ] * numArray2[ 5 ];
            numArray1[ 4 ] = numArray2[ 1 ] * numArray2[ 6 ];
            numArray1[ 5 ] = numArray2[ 2 ] * numArray2[ 5 ];
            numArray1[ 6 ] = numArray2[ 0 ] * numArray2[ 7 ];
            numArray1[ 7 ] = numArray2[ 3 ] * numArray2[ 4 ];
            numArray1[ 8 ] = numArray2[ 0 ] * numArray2[ 6 ];
            numArray1[ 9 ] = numArray2[ 2 ] * numArray2[ 4 ];
            numArray1[ 10 ] = numArray2[ 0 ] * numArray2[ 5 ];
            numArray1[ 11 ] = numArray2[ 1 ] * numArray2[ 4 ];
            destMatrix[ 8 ] = numArray1[ 0 ] * numArray2[ 13 ] + numArray1[ 3 ] * numArray2[ 14 ] + numArray1[ 4 ] * numArray2[ 15 ];
            destMatrix[ 8 ] -= numArray1[ 1 ] * numArray2[ 13 ] + numArray1[ 2 ] * numArray2[ 14 ] + numArray1[ 5 ] * numArray2[ 15 ];
            destMatrix[ 9 ] = numArray1[ 1 ] * numArray2[ 12 ] + numArray1[ 6 ] * numArray2[ 14 ] + numArray1[ 9 ] * numArray2[ 15 ];
            destMatrix[ 9 ] -= numArray1[ 0 ] * numArray2[ 12 ] + numArray1[ 7 ] * numArray2[ 14 ] + numArray1[ 8 ] * numArray2[ 15 ];
            destMatrix[ 10 ] = numArray1[ 2 ] * numArray2[ 12 ] + numArray1[ 7 ] * numArray2[ 13 ] + numArray1[ 10 ] * numArray2[ 15 ];
            destMatrix[ 10 ] -= numArray1[ 3 ] * numArray2[ 12 ] + numArray1[ 6 ] * numArray2[ 13 ] + numArray1[ 11 ] * numArray2[ 15 ];
            destMatrix[ 11 ] = numArray1[ 5 ] * numArray2[ 12 ] + numArray1[ 8 ] * numArray2[ 13 ] + numArray1[ 11 ] * numArray2[ 14 ];
            destMatrix[ 11 ] -= numArray1[ 4 ] * numArray2[ 12 ] + numArray1[ 9 ] * numArray2[ 13 ] + numArray1[ 10 ] * numArray2[ 14 ];
            destMatrix[ 12 ] = numArray1[ 2 ] * numArray2[ 10 ] + numArray1[ 5 ] * numArray2[ 11 ] + numArray1[ 1 ] * numArray2[ 9 ];
            destMatrix[ 12 ] -= numArray1[ 4 ] * numArray2[ 11 ] + numArray1[ 0 ] * numArray2[ 9 ] + numArray1[ 3 ] * numArray2[ 10 ];
            destMatrix[ 13 ] = numArray1[ 8 ] * numArray2[ 11 ] + numArray1[ 0 ] * numArray2[ 8 ] + numArray1[ 7 ] * numArray2[ 10 ];
            destMatrix[ 13 ] -= numArray1[ 6 ] * numArray2[ 10 ] + numArray1[ 9 ] * numArray2[ 11 ] + numArray1[ 1 ] * numArray2[ 8 ];
            destMatrix[ 14 ] = numArray1[ 6 ] * numArray2[ 9 ] + numArray1[ 11 ] * numArray2[ 11 ] + numArray1[ 3 ] * numArray2[ 8 ];
            destMatrix[ 14 ] -= numArray1[ 10 ] * numArray2[ 11 ] + numArray1[ 2 ] * numArray2[ 8 ] + numArray1[ 7 ] * numArray2[ 9 ];
            destMatrix[ 15 ] = numArray1[ 10 ] * numArray2[ 10 ] + numArray1[ 4 ] * numArray2[ 8 ] + numArray1[ 9 ] * numArray2[ 9 ];
            destMatrix[ 15 ] -= numArray1[ 8 ] * numArray2[ 9 ] + numArray1[ 11 ] * numArray2[ 10 ] + numArray1[ 5 ] * numArray2[ 8 ];
            double num = 1.0 / (numArray2[0] * destMatrix[0] + numArray2[1] * destMatrix[1] + numArray2[2] * destMatrix[2] + numArray2[3] * destMatrix[3]);
            for ( int index = 0 ; index < 16 ; ++index )
                destMatrix[ index ] *= num;
        }

        public bool SetToInverse( Matrix4X4 OriginalMatrix )
        {
            Matrix4X4.IntelInvertC( OriginalMatrix.matrix, this.matrix );
            return true;
        }

        public bool Invert()
        {
            return this.SetToInverse( new Matrix4X4( this ) );
        }

        private void matrix_swap_mirror( int a, int b )
        {
            double element = this.GetElement(a, b);
            this.SetElement( a, b, this.GetElement( b, a ) );
            this.SetElement( b, a, element );
        }

        public void Transpose3X3()
        {
            this.matrix_swap_mirror( 0, 1 );
            this.matrix_swap_mirror( 0, 2 );
            this.matrix_swap_mirror( 0, 3 );
            this.matrix_swap_mirror( 1, 2 );
            this.matrix_swap_mirror( 1, 3 );
            this.matrix_swap_mirror( 2, 3 );
        }

        public Vector3 Position
        {
            get
            {
                return new Vector3( this.GetElement( 3, 0 ), this.GetElement( 3, 1 ), this.GetElement( 3, 2 ) );
            }
            set
            {
                this.SetElement( 3, 0, value.x );
                this.SetElement( 3, 1, value.y );
                this.SetElement( 3, 2, value.z );
            }
        }

        public void Translate( double tx, double ty, double tz )
        {
            this.Zero();
            for ( int index = 0 ; index < 4 ; ++index )
                this.SetElement( index, index, 1.0 );
            this.SetElement( 3, 0, tx );
            this.SetElement( 3, 1, ty );
            this.SetElement( 3, 2, tz );
        }

        public void Translate( Vector3 Vect )
        {
            this.Translate( Vect.x, Vect.y, Vect.z );
        }

        public void AddTranslate( double x, double y, double z )
        {
            this.AddTranslate( new Vector3( x, y, z ) );
        }

        public void AddTranslate( Vector3 Vect )
        {
            Matrix4X4 Two = new Matrix4X4();
            Two.Translate( Vect.x, Vect.y, Vect.z );
            this.Multiply( Two );
        }

        public void Scale( float sx, float sy, float sz )
        {
            this.Scale( ( double ) sx, ( double ) sy, ( double ) sz );
        }

        public void Scale( double sx, double sy, double sz )
        {
            this.Zero();
            this.SetElement( 0, 0, sx );
            this.SetElement( 1, 1, sy );
            this.SetElement( 2, 2, sz );
            this.SetElement( 3, 3, 1.0 );
        }

        public void AddRotate( uint Axis, double Theta )
        {
            Matrix4X4 Two = new Matrix4X4();
            Two.Rotate( Axis, Theta );
            this.Multiply( Two );
        }

        public void Rotate( uint Axis, double Theta )
        {
            double num1;
            double num2;
            if ( Theta != 0.0 )
            {
                num1 = Math.Cos( Theta );
                num2 = Math.Sin( Theta );
            }
            else
            {
                num1 = 1.0;
                num2 = 0.0;
            }
            switch ( Axis )
            {
                case 0:
                    this.SetElement( 0, 0, 1.0 );
                    this.SetElement( 0, 1, 0.0 );
                    this.SetElement( 0, 2, 0.0 );
                    this.SetElement( 0, 3, 0.0 );
                    this.SetElement( 1, 0, 0.0 );
                    this.SetElement( 1, 1, num1 );
                    this.SetElement( 1, 2, num2 );
                    this.SetElement( 1, 3, 0.0 );
                    this.SetElement( 2, 0, 0.0 );
                    this.SetElement( 2, 1, -num2 );
                    this.SetElement( 2, 2, num1 );
                    this.SetElement( 2, 3, 0.0 );
                    break;
                case 1:
                    this.SetElement( 0, 0, num1 );
                    this.SetElement( 0, 1, 0.0 );
                    this.SetElement( 0, 2, -num2 );
                    this.SetElement( 0, 3, 0.0 );
                    this.SetElement( 1, 0, 0.0 );
                    this.SetElement( 1, 1, 1.0 );
                    this.SetElement( 1, 2, 0.0 );
                    this.SetElement( 1, 3, 0.0 );
                    this.SetElement( 2, 0, num2 );
                    this.SetElement( 2, 1, 0.0 );
                    this.SetElement( 2, 2, num1 );
                    this.SetElement( 2, 3, 0.0 );
                    break;
                case 2:
                    this.SetElement( 0, 0, num1 );
                    this.SetElement( 0, 1, num2 );
                    this.SetElement( 0, 2, 0.0 );
                    this.SetElement( 0, 3, 0.0 );
                    this.SetElement( 1, 0, -num2 );
                    this.SetElement( 1, 1, num1 );
                    this.SetElement( 1, 2, 0.0 );
                    this.SetElement( 1, 3, 0.0 );
                    this.SetElement( 2, 0, 0.0 );
                    this.SetElement( 2, 1, 0.0 );
                    this.SetElement( 2, 2, 1.0 );
                    this.SetElement( 2, 3, 0.0 );
                    break;
            }
            this.SetElement( 3, 0, 0.0 );
            this.SetElement( 3, 1, 0.0 );
            this.SetElement( 3, 2, 0.0 );
            this.SetElement( 3, 3, 1.0 );
        }

        public void Rotate( Vector3 Axis, double AngleRadians )
        {
            Axis.Normalize();
            double num1 = Math.Cos(AngleRadians);
            double num2 = Math.Sin(AngleRadians);
            double num3 = 1.0 - num1;
            this.matrix[ 0 ] = num3 * Axis.x * Axis.x + num1;
            this.matrix[ 4 ] = num3 * Axis.x * Axis.y - num2 * Axis.z;
            this.matrix[ 8 ] = num3 * Axis.x * Axis.z + num2 * Axis.y;
            this.matrix[ 12 ] = 0.0;
            this.matrix[ 1 ] = num3 * Axis.x * Axis.y + num2 * Axis.z;
            this.matrix[ 5 ] = num3 * Axis.y * Axis.y + num1;
            this.matrix[ 9 ] = num3 * Axis.y * Axis.z - num2 * Axis.x;
            this.matrix[ 13 ] = 0.0;
            this.matrix[ 2 ] = num3 * Axis.x * Axis.z - num2 * Axis.y;
            this.matrix[ 6 ] = num3 * Axis.y * Axis.z + num2 * Axis.x;
            this.matrix[ 10 ] = num3 * Axis.z * Axis.z + num1;
            this.matrix[ 14 ] = 0.0;
            this.matrix[ 3 ] = 0.0;
            this.matrix[ 7 ] = 0.0;
            this.matrix[ 11 ] = 0.0;
            this.matrix[ 15 ] = 1.0;
        }

        public bool Equals( Matrix4X4 OtherMatrix, double ErrorRange )
        {
            for ( int Row = 0 ; Row < 4 ; ++Row )
            {
                for ( int Column = 0 ; Column < 4 ; ++Column )
                {
                    if ( this.GetElement( Row, Column ) < OtherMatrix.GetElement( Row, Column ) - ErrorRange || this.GetElement( Row, Column ) > OtherMatrix.GetElement( Row, Column ) + ErrorRange )
                        return false;
                }
            }
            return true;
        }

        public void PrepareMatrix( double Tx, double Ty, double Tz, double Rx, double Ry, double Rz, double Sx, double Sy, double Sz )
        {
            bool flag = false;
            if ( Sx != 1.0 || Sy != 1.0 || Sz != 1.0 )
            {
                if ( flag )
                {
                    Matrix4X4 Two = new Matrix4X4();
                    Two.Scale( Sx, Sy, Sz );
                    this.Multiply( Two );
                }
                else
                {
                    this.Scale( Sx, Sy, Sz );
                    flag = true;
                }
            }
            if ( Rx != 0.0 )
            {
                if ( flag )
                {
                    Matrix4X4 Two = new Matrix4X4();
                    Two.Rotate( 0U, Rx );
                    this.Multiply( Two );
                }
                else
                {
                    this.Rotate( 0U, Rx );
                    flag = true;
                }
            }
            if ( Ry != 0.0 )
            {
                if ( flag )
                {
                    Matrix4X4 Two = new Matrix4X4();
                    Two.Rotate( 1U, Ry );
                    this.Multiply( Two );
                }
                else
                {
                    this.Rotate( 1U, Ry );
                    flag = true;
                }
            }
            if ( Rz != 0.0 )
            {
                if ( flag )
                {
                    Matrix4X4 Two = new Matrix4X4();
                    Two.Rotate( 2U, Rz );
                    this.Multiply( Two );
                }
                else
                {
                    this.Rotate( 2U, Rz );
                    flag = true;
                }
            }
            if ( Tx == 0.0 && Ty == 0.0 && Tz == 0.0 )
                return;
            if ( flag )
            {
                Matrix4X4 Two = new Matrix4X4();
                Two.Translate( Tx, Ty, Tz );
                this.Multiply( Two );
            }
            else
            {
                this.Translate( Tx, Ty, Tz );
                flag = true;
            }
            if ( flag )
                return;
            this.Identity();
        }

        public void PrepareMatrix( Vector3 pTranslateVector, Vector3 pRotateVector, Vector3 pScaleVector )
        {
            this.PrepareMatrix( pTranslateVector.x, pTranslateVector.y, pTranslateVector.z, pRotateVector.x, pRotateVector.y, pRotateVector.z, pScaleVector.x, pScaleVector.y, pScaleVector.z );
        }

        public void PrepareMatrixFromPositionAndDirection( Vector3 Position, Vector3 Direction )
        {
            this.Translate( Position );
            Vector3 vector3_1 = Direction;
            vector3_1.Normalize();
            Vector3 vector3_2 = new Vector3(1.0, 0.0, 0.0);
            double num = Math.Cos(0.174532930056254);
            if ( Vector3.Dot( vector3_1, vector3_2 ) > num )
                vector3_2 = new Vector3( 0.0, 1.0, 0.0 );
            Vector3 right = Vector3.Cross(vector3_2, vector3_1);
            right.Normalize();
            Vector3 vector3_3 = Vector3.Cross(vector3_1, right);
            for ( int Column = 0 ; Column < 3 ; ++Column )
            {
                this.SetElement( 0, Column, vector3_3[ Column ] );
                this.SetElement( 1, Column, vector3_1[ Column ] );
                this.SetElement( 2, Column, right[ Column ] );
            }
            this.SetElement( 0, 3, 0.0 );
            this.SetElement( 1, 3, 0.0 );
            this.SetElement( 2, 3, 0.0 );
        }

        public void PrepareInvMatrix( double Tx, double Ty, double Tz, double Rx, double Ry, double Rz, double Sx, double Sy, double Sz )
        {
            Matrix4X4 Two1 = new Matrix4X4();
            Matrix4X4 Two2 = new Matrix4X4();
            Matrix4X4 Two3 = new Matrix4X4();
            Matrix4X4 Two4 = new Matrix4X4();
            Matrix4X4 One1 = new Matrix4X4();
            Matrix4X4 One2 = new Matrix4X4();
            Matrix4X4 One3 = new Matrix4X4();
            Matrix4X4 One4 = new Matrix4X4();
            Two1.Scale( Sx, Sy, Sz );
            Two2.Rotate( 0U, Rx );
            Two3.Rotate( 1U, Ry );
            Two4.Rotate( 2U, Rz );
            One1.Translate( Tx, Ty, Tz );
            One2.Multiply( One1, Two4 );
            One3.Multiply( One2, Two3 );
            One4.Multiply( One3, Two2 );
            this.Multiply( One4, Two1 );
        }

        public void PrepareInvMatrix( Vector3 pTranslateVector, Vector3 pRotateVector, Vector3 pScaleVector )
        {
            this.PrepareInvMatrix( pTranslateVector.x, pTranslateVector.y, pTranslateVector.z, pRotateVector.x, pRotateVector.y, pRotateVector.z, pScaleVector.x, pScaleVector.y, pScaleVector.z );
        }

        public void TransformVector( double[ ] pChanged )
        {
            double[] numArray = (double[]) pChanged.Clone();
            pChanged[ 0 ] = this.GetElement( 0, 0 ) * numArray[ 0 ] + this.GetElement( 0, 1 ) * numArray[ 1 ] + this.GetElement( 0, 2 ) * numArray[ 2 ] + this.GetElement( 0, 3 ) * numArray[ 3 ];
            pChanged[ 1 ] = this.GetElement( 1, 0 ) * numArray[ 0 ] + this.GetElement( 1, 1 ) * numArray[ 1 ] + this.GetElement( 1, 2 ) * numArray[ 2 ] + this.GetElement( 1, 3 ) * numArray[ 3 ];
            pChanged[ 2 ] = this.GetElement( 2, 0 ) * numArray[ 0 ] + this.GetElement( 2, 1 ) * numArray[ 1 ] + this.GetElement( 2, 2 ) * numArray[ 2 ] + this.GetElement( 2, 3 ) * numArray[ 3 ];
            pChanged[ 3 ] = this.GetElement( 3, 0 ) * numArray[ 0 ] + this.GetElement( 3, 1 ) * numArray[ 1 ] + this.GetElement( 3, 2 ) * numArray[ 2 ] + this.GetElement( 3, 3 ) * numArray[ 3 ];
        }

        public void TransformVector( ref Vector3 Changed )
        {
            Vector3 Original = Changed;
            this.TransformVector( out Changed, Original );
        }

        public void TransformVector( out Vector3 Changed, Vector3 Original )
        {
            this.TransformVector3X3( out Changed, Original );
            Changed.x += this.GetElement( 3, 0 );
            Changed.y += this.GetElement( 3, 1 );
            Changed.z += this.GetElement( 3, 2 );
        }

        public void TransformVector3X3( ref Vector3 Changed )
        {
            Vector3 Original = new Vector3(Changed);
            this.TransformVector3X3( out Changed, Original );
        }

        public void TransformVector3X3( out Vector3 Changed, Vector3 Original )
        {
            Changed.x = this.GetElement( 0, 0 ) * Original.x + this.GetElement( 1, 0 ) * Original.y + this.GetElement( 2, 0 ) * Original.z;
            Changed.y = this.GetElement( 0, 1 ) * Original.x + this.GetElement( 1, 1 ) * Original.y + this.GetElement( 2, 1 ) * Original.z;
            Changed.z = this.GetElement( 0, 2 ) * Original.x + this.GetElement( 1, 2 ) * Original.y + this.GetElement( 2, 2 ) * Original.z;
        }

        public void TransformVector3X3( Vector3[ ] vertsToTransform )
        {
            for ( int index = 0 ; index < vertsToTransform.Length ; ++index )
                this.TransformVector3X3( ref vertsToTransform[ index ] );
        }

        public uint ValidateMatrix()
        {
            return this.GetElement( 3, 0 ) == 0.0 && this.GetElement( 3, 1 ) == 0.0 && ( this.GetElement( 3, 2 ) == 0.0 && this.GetElement( 3, 3 ) == 1.0 ) ? 1U : 0U;
        }

        public static Matrix4X4 operator *( Matrix4X4 A, Matrix4X4 B )
        {
            Matrix4X4 matrix4X4 = new Matrix4X4(A);
            matrix4X4.Multiply( B );
            return matrix4X4;
        }

        public void Multiply( Matrix4X4 Two )
        {
            this.Multiply( new Matrix4X4( this ), Two );
        }

        public void Multiply( Matrix4X4 One, Matrix4X4 Two )
        {
            if ( this == One || this == Two )
                throw new FormatException( "Neither of the input parameters can be the same Matrix as this." );
            for ( int Row = 0 ; Row < 4 ; ++Row )
            {
                for ( int Column = 0 ; Column < 4 ; ++Column )
                {
                    this.SetElement( Row, Column, 0.0 );
                    for ( int index = 0 ; index < 4 ; ++index )
                        this.AddElement( Row, Column, One.GetElement( Row, index ) * Two.GetElement( index, Column ) );
                }
            }
        }

        public void GetXAxisVector( Vector3 result )
        {
            result.x = this.GetElement( 0, 0 );
            result.y = this.GetElement( 0, 1 );
            result.z = this.GetElement( 0, 2 );
        }

        public void GetYAxisVector( Vector3 result )
        {
            result.x = this.GetElement( 1, 0 );
            result.y = this.GetElement( 1, 1 );
            result.z = this.GetElement( 1, 2 );
        }

        public void GetZAxisVector( Vector3 result )
        {
            result.x = this.GetElement( 2, 0 );
            result.y = this.GetElement( 2, 1 );
            result.z = this.GetElement( 2, 2 );
        }

        public void GetTranslation( Vector3 result )
        {
            result.x = this.GetElement( 3, 0 );
            result.y = this.GetElement( 3, 1 );
            result.z = this.GetElement( 3, 2 );
        }

        public void SetElements( Matrix4X4 CopyFrom )
        {
            this.SetElements( CopyFrom.GetElements() );
        }

        public void SetElements( double[ ] pElements )
        {
            for ( int index = 0 ; index < 16 ; ++index )
                this.matrix[ index ] = pElements[ index ];
        }

        public void SetElements( double A00_00, double A00_01, double A00_02, double A00_03, double A01_00, double A01_01, double A01_02, double A01_03, double A02_00, double A02_01, double A02_02, double A02_03, double A03_00, double A03_01, double A03_02, double A03_03 )
        {
            int num1 = 0;
            double[] matrix1 = this.matrix;
            int index1 = num1;
            int num2 = index1 + 1;
            double num3 = A00_00;
            matrix1[ index1 ] = num3;
            double[] matrix2 = this.matrix;
            int index2 = num2;
            int num4 = index2 + 1;
            double num5 = A00_01;
            matrix2[ index2 ] = num5;
            double[] matrix3 = this.matrix;
            int index3 = num4;
            int num6 = index3 + 1;
            double num7 = A00_02;
            matrix3[ index3 ] = num7;
            double[] matrix4 = this.matrix;
            int index4 = num6;
            int num8 = index4 + 1;
            double num9 = A00_03;
            matrix4[ index4 ] = num9;
            double[] matrix5 = this.matrix;
            int index5 = num8;
            int num10 = index5 + 1;
            double num11 = A01_00;
            matrix5[ index5 ] = num11;
            double[] matrix6 = this.matrix;
            int index6 = num10;
            int num12 = index6 + 1;
            double num13 = A01_01;
            matrix6[ index6 ] = num13;
            double[] matrix7 = this.matrix;
            int index7 = num12;
            int num14 = index7 + 1;
            double num15 = A01_02;
            matrix7[ index7 ] = num15;
            double[] matrix8 = this.matrix;
            int index8 = num14;
            int num16 = index8 + 1;
            double num17 = A01_03;
            matrix8[ index8 ] = num17;
            double[] matrix9 = this.matrix;
            int index9 = num16;
            int num18 = index9 + 1;
            double num19 = A02_00;
            matrix9[ index9 ] = num19;
            double[] matrix10 = this.matrix;
            int index10 = num18;
            int num20 = index10 + 1;
            double num21 = A02_01;
            matrix10[ index10 ] = num21;
            double[] matrix11 = this.matrix;
            int index11 = num20;
            int num22 = index11 + 1;
            double num23 = A02_02;
            matrix11[ index11 ] = num23;
            double[] matrix12 = this.matrix;
            int index12 = num22;
            int num24 = index12 + 1;
            double num25 = A02_03;
            matrix12[ index12 ] = num25;
            double[] matrix13 = this.matrix;
            int index13 = num24;
            int num26 = index13 + 1;
            double num27 = A03_00;
            matrix13[ index13 ] = num27;
            double[] matrix14 = this.matrix;
            int index14 = num26;
            int num28 = index14 + 1;
            double num29 = A03_01;
            matrix14[ index14 ] = num29;
            double[] matrix15 = this.matrix;
            int index15 = num28;
            int num30 = index15 + 1;
            double num31 = A03_02;
            matrix15[ index15 ] = num31;
            double[] matrix16 = this.matrix;
            int index16 = num30;
            int num32 = index16 + 1;
            double num33 = A03_03;
            matrix16[ index16 ] = num33;
        }

        public void SetElements( float A00_00, float A00_01, float A00_02, float A00_03, float A01_00, float A01_01, float A01_02, float A01_03, float A02_00, float A02_01, float A02_02, float A02_03, float A03_00, float A03_01, float A03_02, float A03_03 )
        {
            this.SetElements( ( double ) A00_00, ( double ) A00_01, ( double ) A00_02, ( double ) A00_03, ( double ) A01_00, ( double ) A01_01, ( double ) A01_02, ( double ) A01_03, ( double ) A02_00, ( double ) A02_01, ( double ) A02_02, ( double ) A02_03, ( double ) A03_00, ( double ) A03_01, ( double ) A03_02, ( double ) A03_03 );
        }

        public double[ ] GetElements()
        {
            return this.matrix;
        }

        public override string ToString()
        {
            string str = "Matrix4x4 (";
            for ( int index = 0 ; index < 16 ; ++index )
                str = str + this[ index ].ToString() + ", ";
            return str + ")";
        }
    }
}
