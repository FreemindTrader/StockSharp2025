// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.Point2D
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg
{
    internal struct Point2D
    {
        public int x;
        public int y;

        public Point2D( int newX, int newY )
        {
            this.x = newX;
            this.y = newY;
        }

        public override string ToString()
        {
            return string.Format( "X={0};Y={1}", ( object ) this.x, ( object ) this.y );
        }

        public void Set( int inX, int inY )
        {
            this.x = inX;
            this.y = inY;
        }

        public static Point2D operator +( Point2D A, Point2D B )
        {
            return new Point2D() { x = A.x + B.x, y = A.y + B.y };
        }

        public static Point2D operator -( Point2D A, Point2D B )
        {
            return new Point2D() { x = A.x - B.x, y = A.y - B.y };
        }

        public static Point2D operator *( Point2D A, Point2D B )
        {
            return new Point2D() { x = A.x * B.x, y = A.y * B.y };
        }

        public static Point2D operator *( Point2D A, int B )
        {
            return new Point2D() { x = A.x * B, y = A.y * B };
        }

        public static Point2D operator *( int B, Point2D A )
        {
            return new Point2D() { x = A.x * B, y = A.y * B };
        }

        public static Point2D operator /( Point2D A, Point2D B )
        {
            return new Point2D() { x = A.x / B.x, y = A.y / B.y };
        }

        public static Point2D operator /( Point2D A, int B )
        {
            return new Point2D() { x = A.x / B, y = A.y / B };
        }

        public static Point2D operator /( int B, Point2D A )
        {
            return new Point2D() { x = A.x / B, y = A.y / B };
        }

        public bool Equals( Point2D OtherVector )
        {
            return this.x == OtherVector.x && this.y == OtherVector.y;
        }

        public override bool Equals( object obj )
        {
            if ( obj == null )
                return false;
            Point2D point2D = (Point2D) obj;
            if ( ( ValueType ) point2D == null || this.x != point2D.x )
                return false;
            return this.y == point2D.y;
        }

        public override int GetHashCode()
        {
            return new int[ 2 ] { this.x, this.y }.GetHashCode();
        }

        public static bool operator ==( Point2D a, Point2D b )
        {
            return a.Equals( b );
        }

        public static bool operator !=( Point2D a, Point2D b )
        {
            return !a.Equals( b );
        }

        public Point2D GetNormal()
        {
            Point2D point2D = this;
            point2D.Normalize();
            return point2D;
        }

        public Point2D GetPerpendicular()
        {
            return new Point2D( this.y, -this.x );
        }

        public Point2D GetPerpendicularNormal()
        {
            Point2D perpendicular = this.GetPerpendicular();
            perpendicular.Normalize();
            return perpendicular;
        }

        public double GetLength()
        {
            return Math.Sqrt( ( double ) ( this.x * this.x + this.y * this.y ) );
        }

        public double GetLengthSquared()
        {
            return this.Dot( this );
        }

        public static double GetDistanceBetween( Point2D A, Point2D B )
        {
            return Math.Sqrt( Point2D.GetDistanceBetweenSquared( A, B ) );
        }

        public static double GetDistanceBetweenSquared( Point2D A, Point2D B )
        {
            return ( double ) ( ( A.x - B.x ) * ( A.x - B.x ) + ( A.y - B.y ) * ( A.y - B.y ) );
        }

        public double GetSquaredDistanceTo( Point2D Other )
        {
            return ( double ) ( ( this.x - Other.x ) * ( this.x - Other.x ) + ( this.y - Other.y ) * ( this.y - Other.y ) );
        }

        public static double Range0To2PI( double Value )
        {
            if ( Value < 0.0 )
                Value += 2.0 * Math.PI;
            if ( Value >= 2.0 * Math.PI )
                Value -= 2.0 * Math.PI;
            if ( Value < 0.0 || Value > 2.0 * Math.PI )
                throw new Exception( "Value >= 0 && Value <= 2 * PI" );
            return Value;
        }

        public static double GetDeltaAngle( double StartAngle, double EndAngle )
        {
            if ( StartAngle != Point2D.Range0To2PI( StartAngle ) )
                throw new Exception( "StartAngle == Range0To2PI(StartAngle)" );
            if ( EndAngle != Point2D.Range0To2PI( EndAngle ) )
                throw new Exception( "EndAngle   == Range0To2PI(EndAngle)" );
            double num = EndAngle - StartAngle;
            if ( num > Math.PI )
                num -= 2.0 * Math.PI;
            if ( num < -1.0 * Math.PI )
                num += 2.0 * Math.PI;
            return num;
        }

        public double GetAngle0To2PI()
        {
            return Point2D.Range0To2PI( Math.Atan2( ( double ) this.y, ( double ) this.x ) );
        }

        public double GetDeltaAngle( Point2D A )
        {
            return Point2D.GetDeltaAngle( this.GetAngle0To2PI(), A.GetAngle0To2PI() );
        }

        public void Normalize()
        {
            double length = this.GetLength();
            if ( length == 0.0 )
                throw new Exception( "Length != 0.f" );
            if ( length == 0.0 )
                return;
            double num = 1.0 / length;
            this.x = ( int ) ( ( double ) this.x * num + 0.5 );
            this.y = ( int ) ( ( double ) this.y * num + 0.5 );
        }

        public void Normalize( double Length )
        {
            if ( Length == 0.0 )
                throw new Exception( "Length == 0.f" );
            if ( Length == 0.0 )
                return;
            double num = 1.0 / Length;
            this.x = ( int ) ( ( double ) this.x * num + 0.5 );
            this.y = ( int ) ( ( double ) this.y * num + 0.5 );
        }

        public double NormalizeAndReturnLength()
        {
            double length = this.GetLength();
            if ( length != 0.0 )
            {
                double num = 1.0 / length;
                this.x = ( int ) ( ( double ) this.x * num + 0.5 );
                this.y = ( int ) ( ( double ) this.y * num + 0.5 );
            }
            return length;
        }

        public void Zero()
        {
            this.x = 0;
            this.y = 0;
        }

        public void Negate()
        {
            this.x = -this.x;
            this.y = -this.y;
        }

        public double Dot( Point2D B )
        {
            return ( double ) ( this.x * B.x + this.y * B.y );
        }

        public double Cross( Point2D B )
        {
            return ( double ) ( this.x * B.y - this.y * B.x );
        }
    }
}
