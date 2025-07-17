// Decompiled with JetBrains decompiler
// Type: MatterHackers.VectorMath.AxisAlignedBoundingBox
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;

namespace MatterHackers.VectorMath
{
    internal class AxisAlignedBoundingBox
    {
        private Vector3 readOnlyMinXYZ;
        private Vector3 readOnlyMaxXYZ;
        private double volumeCache;
        private double surfaceAreaCache;

        public AxisAlignedBoundingBox( Vector3 minXYZ, Vector3 maxXYZ )
        {
            if ( maxXYZ.x < minXYZ.x || maxXYZ.y < minXYZ.y || maxXYZ.z < minXYZ.z )
                throw new ArgumentException( "All values of min must be less than all values in max." );
            this.readOnlyMinXYZ = minXYZ;
            this.readOnlyMaxXYZ = maxXYZ;
        }

        public Vector3 Size
        {
            get
            {
                return this.maxXYZ - this.minXYZ;
            }
        }

        public double XSize
        {
            get
            {
                return this.maxXYZ.x - this.minXYZ.x;
            }
        }

        public double YSize
        {
            get
            {
                return this.maxXYZ.y - this.minXYZ.y;
            }
        }

        public double ZSize
        {
            get
            {
                return this.maxXYZ.z - this.minXYZ.z;
            }
        }

        public AxisAlignedBoundingBox NewTransformed( Matrix4X4 transform )
        {
            Vector3[] vecArray = new Vector3[8];
            Vector3[] vector3Array1 = vecArray;
            int index1 = 0;
            Vector3 vector3_1 = this[0];
            double x1 = vector3_1[0];
            vector3_1 = this[ 0 ];
            double y1 = vector3_1[1];
            vector3_1 = this[ 0 ];
            double z1 = vector3_1[2];
            Vector3 vector3_2 = new Vector3(x1, y1, z1);
            vector3Array1[ index1 ] = vector3_2;
            Vector3[] vector3Array2 = vecArray;
            int index2 = 1;
            Vector3 vector3_3 = this[0];
            double x2 = vector3_3[0];
            vector3_3 = this[ 0 ];
            double y2 = vector3_3[1];
            vector3_3 = this[ 1 ];
            double z2 = vector3_3[2];
            Vector3 vector3_4 = new Vector3(x2, y2, z2);
            vector3Array2[ index2 ] = vector3_4;
            Vector3[] vector3Array3 = vecArray;
            int index3 = 2;
            Vector3 vector3_5 = this[0];
            double x3 = vector3_5[0];
            vector3_5 = this[ 1 ];
            double y3 = vector3_5[1];
            vector3_5 = this[ 0 ];
            double z3 = vector3_5[2];
            Vector3 vector3_6 = new Vector3(x3, y3, z3);
            vector3Array3[ index3 ] = vector3_6;
            Vector3[] vector3Array4 = vecArray;
            int index4 = 3;
            Vector3 vector3_7 = this[0];
            double x4 = vector3_7[0];
            vector3_7 = this[ 1 ];
            double y4 = vector3_7[1];
            vector3_7 = this[ 1 ];
            double z4 = vector3_7[2];
            Vector3 vector3_8 = new Vector3(x4, y4, z4);
            vector3Array4[ index4 ] = vector3_8;
            Vector3[] vector3Array5 = vecArray;
            int index5 = 4;
            Vector3 vector3_9 = this[1];
            double x5 = vector3_9[0];
            vector3_9 = this[ 0 ];
            double y5 = vector3_9[1];
            vector3_9 = this[ 0 ];
            double z5 = vector3_9[2];
            Vector3 vector3_10 = new Vector3(x5, y5, z5);
            vector3Array5[ index5 ] = vector3_10;
            Vector3[] vector3Array6 = vecArray;
            int index6 = 5;
            Vector3 vector3_11 = this[1];
            double x6 = vector3_11[0];
            vector3_11 = this[ 0 ];
            double y6 = vector3_11[1];
            vector3_11 = this[ 1 ];
            double z6 = vector3_11[2];
            Vector3 vector3_12 = new Vector3(x6, y6, z6);
            vector3Array6[ index6 ] = vector3_12;
            Vector3[] vector3Array7 = vecArray;
            int index7 = 6;
            Vector3 vector3_13 = this[1];
            double x7 = vector3_13[0];
            vector3_13 = this[ 1 ];
            double y7 = vector3_13[1];
            vector3_13 = this[ 0 ];
            double z7 = vector3_13[2];
            Vector3 vector3_14 = new Vector3(x7, y7, z7);
            vector3Array7[ index7 ] = vector3_14;
            Vector3[] vector3Array8 = vecArray;
            int index8 = 7;
            Vector3 vector3_15 = this[1];
            double x8 = vector3_15[0];
            vector3_15 = this[ 1 ];
            double y8 = vector3_15[1];
            vector3_15 = this[ 1 ];
            double z8 = vector3_15[2];
            Vector3 vector3_16 = new Vector3(x8, y8, z8);
            vector3Array8[ index8 ] = vector3_16;
            Vector3.Transform( vecArray, transform );
            Vector3 minXYZ = new Vector3(double.MaxValue, double.MaxValue, double.MaxValue);
            Vector3 maxXYZ = new Vector3(double.MinValue, double.MinValue, double.MinValue);
            for ( int index9 = 0 ; index9 < 8 ; ++index9 )
            {
                minXYZ.x = Math.Min( minXYZ.x, vecArray[ index9 ].x );
                minXYZ.y = Math.Min( minXYZ.y, vecArray[ index9 ].y );
                minXYZ.z = Math.Min( minXYZ.z, vecArray[ index9 ].z );
                maxXYZ.x = Math.Max( maxXYZ.x, vecArray[ index9 ].x );
                maxXYZ.y = Math.Max( maxXYZ.y, vecArray[ index9 ].y );
                maxXYZ.z = Math.Max( maxXYZ.z, vecArray[ index9 ].z );
            }
            return new AxisAlignedBoundingBox( minXYZ, maxXYZ );
        }

        public Vector3 minXYZ
        {
            get
            {
                return this.readOnlyMinXYZ;
            }
        }

        public Vector3 maxXYZ
        {
            get
            {
                return this.readOnlyMaxXYZ;
            }
        }

        public Vector3 Center
        {
            get
            {
                return ( this.minXYZ + this.maxXYZ ) / 2.0;
            }
        }

        public static double GetIntersectCost()
        {
            return 132.0;
        }

        public Vector3 GetCenter()
        {
            return ( this.minXYZ + this.maxXYZ ) * 0.5;
        }

        public double GetCenterX()
        {
            return ( this.minXYZ.x + this.maxXYZ.x ) * 0.5;
        }

        public double GetVolume()
        {
            if ( this.volumeCache == 0.0 )
                this.volumeCache = ( this.maxXYZ.x - this.minXYZ.x ) * ( this.maxXYZ.y - this.minXYZ.y ) * ( this.maxXYZ.z - this.minXYZ.z );
            return this.volumeCache;
        }

        public double GetSurfaceArea()
        {
            if ( this.surfaceAreaCache == 0.0 )
                this.surfaceAreaCache = ( this.maxXYZ.x - this.minXYZ.x ) * ( this.maxXYZ.z - this.minXYZ.z ) * 2.0 + ( this.maxXYZ.y - this.minXYZ.y ) * ( this.maxXYZ.z - this.minXYZ.z ) * 2.0 + ( this.maxXYZ.x - this.minXYZ.x ) * ( this.maxXYZ.y - this.minXYZ.y ) * 2.0;
            return this.surfaceAreaCache;
        }

        public Vector3 this[ int index ]
        {
            get
            {
                if ( index == 0 )
                    return this.minXYZ;
                if ( index == 1 )
                    return this.maxXYZ;
                throw new IndexOutOfRangeException();
            }
        }

        public static AxisAlignedBoundingBox operator +( AxisAlignedBoundingBox A, AxisAlignedBoundingBox B )
        {
            return new AxisAlignedBoundingBox( new Vector3() { x = Math.Min( A.minXYZ.x, B.minXYZ.x ), y = Math.Min( A.minXYZ.y, B.minXYZ.y ), z = Math.Min( A.minXYZ.z, B.minXYZ.z ) }, new Vector3() { x = Math.Max( A.maxXYZ.x, B.maxXYZ.x ), y = Math.Max( A.maxXYZ.y, B.maxXYZ.y ), z = Math.Max( A.maxXYZ.z, B.maxXYZ.z ) } );
        }

        public static AxisAlignedBoundingBox Union( AxisAlignedBoundingBox boundsA, AxisAlignedBoundingBox boundsB )
        {
            Vector3 zero1 = Vector3.Zero;
            zero1.x = Math.Min( boundsA.minXYZ.x, boundsB.minXYZ.x );
            zero1.y = Math.Min( boundsA.minXYZ.y, boundsB.minXYZ.y );
            zero1.z = Math.Min( boundsA.minXYZ.z, boundsB.minXYZ.z );
            Vector3 zero2 = Vector3.Zero;
            zero2.x = Math.Max( boundsA.maxXYZ.x, boundsB.maxXYZ.x );
            zero2.y = Math.Max( boundsA.maxXYZ.y, boundsB.maxXYZ.y );
            zero2.z = Math.Max( boundsA.maxXYZ.z, boundsB.maxXYZ.z );
            return new AxisAlignedBoundingBox( zero1, zero2 );
        }

        public static AxisAlignedBoundingBox Intersection( AxisAlignedBoundingBox boundsA, AxisAlignedBoundingBox boundsB )
        {
            Vector3 zero1 = Vector3.Zero;
            zero1.x = Math.Max( boundsA.minXYZ.x, boundsB.minXYZ.x );
            zero1.y = Math.Max( boundsA.minXYZ.y, boundsB.minXYZ.y );
            zero1.z = Math.Max( boundsA.minXYZ.z, boundsB.minXYZ.z );
            Vector3 zero2 = Vector3.Zero;
            zero2.x = Math.Min( boundsA.maxXYZ.x, boundsB.maxXYZ.x );
            zero2.y = Math.Min( boundsA.maxXYZ.y, boundsB.maxXYZ.y );
            zero2.z = Math.Min( boundsA.maxXYZ.z, boundsB.maxXYZ.z );
            return new AxisAlignedBoundingBox( zero1, zero2 );
        }

        public static AxisAlignedBoundingBox Union( AxisAlignedBoundingBox bounds, Vector3 vertex )
        {
            Vector3 zero1 = Vector3.Zero;
            zero1.x = Math.Min( bounds.minXYZ.x, vertex.x );
            zero1.y = Math.Min( bounds.minXYZ.y, vertex.y );
            zero1.z = Math.Min( bounds.minXYZ.z, vertex.z );
            Vector3 zero2 = Vector3.Zero;
            zero2.x = Math.Max( bounds.maxXYZ.x, vertex.x );
            zero2.y = Math.Max( bounds.maxXYZ.y, vertex.y );
            zero2.z = Math.Max( bounds.maxXYZ.z, vertex.z );
            return new AxisAlignedBoundingBox( zero1, zero2 );
        }
    }
}
