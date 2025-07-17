// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.agg_basics
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg
{
    internal static class agg_basics
    {
        public static void memcpy( byte[ ] dest, int destIndex, byte[ ] source, int sourceIndex, int Count )
        {
            for ( int index = 0 ; index < Count ; ++index )
                dest[ destIndex + index ] = source[ sourceIndex + index ];
        }

        public static void memcpy( int[ ] dest, int destIndex, int[ ] source, int sourceIndex, int Count )
        {
            for ( int index = 0 ; index < Count ; ++index )
                dest[ destIndex + index ] = source[ sourceIndex + index ];
        }

        public static void memcpy( float[ ] dest, int destIndex, float[ ] source, int sourceIndex, int count )
        {
            for ( int index = 0 ; index < count ; ++index )
                dest[ destIndex++ ] = source[ sourceIndex++ ];
        }

        public static void memmove( byte[ ] dest, int destIndex, byte[ ] source, int sourceIndex, int Count )
        {
            if ( source == dest && destIndex >= sourceIndex )
                throw new Exception( "this code needs to be tested" );
            agg_basics.memcpy( dest, destIndex, source, sourceIndex, Count );
        }

        public static void memmove( int[ ] dest, int destIndex, int[ ] source, int sourceIndex, int Count )
        {
            if ( source == dest && destIndex >= sourceIndex )
                throw new Exception( "this code needs to be tested" );
            agg_basics.memcpy( dest, destIndex, source, sourceIndex, Count );
        }

        public static void memmove( float[ ] dest, int destIndex, float[ ] source, int sourceIndex, int Count )
        {
            if ( source == dest && destIndex >= sourceIndex )
                throw new Exception( "this code needs to be tested" );
            agg_basics.memcpy( dest, destIndex, source, sourceIndex, Count );
        }

        public static void memset( int[ ] dest, int destIndex, int Val, int Count )
        {
            for ( int index = 0 ; index < Count ; ++index )
                dest[ destIndex + index ] = Val;
        }

        public static void memset( byte[ ] dest, int destIndex, byte ByteVal, int Count )
        {
            for ( int index = 0 ; index < Count ; ++index )
                dest[ destIndex + index ] = ByteVal;
        }

        public static void MemClear( int[ ] dest, int destIndex, int Count )
        {
            for ( int index = 0 ; index < Count ; ++index )
                dest[ destIndex + index ] = 0;
        }

        public static void MemClear( byte[ ] dest, int destIndex, int Count )
        {
            for ( int index = 0 ; index < Count ; ++index )
                dest[ destIndex + index ] = ( byte ) 0;
        }

        public static bool is_equal_eps( double v1, double v2, double epsilon )
        {
            return Math.Abs( v1 - v2 ) <= epsilon;
        }

        public static double deg2rad( double deg )
        {
            return deg * Math.PI / 180.0;
        }

        public static double rad2deg( double rad )
        {
            return rad * 180.0 / Math.PI;
        }

        public static int iround( double v )
        {
            return v < 0.0 ? ( int ) ( v - 0.5 ) : ( int ) ( v + 0.5 );
        }

        public static int iround( double v, int saturationLimit )
        {
            if ( v < ( double ) -saturationLimit )
                return -saturationLimit;
            if ( v > ( double ) saturationLimit )
                return saturationLimit;
            return agg_basics.iround( v );
        }

        public static int uround( double v )
        {
            return ( int ) ( uint ) ( v + 0.5 );
        }

        public static int ufloor( double v )
        {
            return ( int ) ( uint ) v;
        }

        public static int uceil( double v )
        {
            return ( int ) ( uint ) Math.Ceiling( v );
        }

        public enum filling_rule_e
        {
            fill_non_zero,
            fill_even_odd,
        }

        public enum poly_subpixel_scale_e
        {
            poly_subpixel_shift = 8,
            poly_subpixel_mask = 255, // 0x000000FF
            poly_subpixel_scale = 256, // 0x00000100
        }
    }
}
