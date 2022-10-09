
using fx.Collections;
using fx.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace fx.DefinitionsWnd
{
    public static class fxFibRatioConstants
    {
        public static readonly PooledList< fxFibRatioModel > Wave2RetracementFibLevels         = new PooledList<fxFibRatioModel>();
        public static readonly PooledList< fxFibRatioModel > Wave3CProjectionFibLevels         = new PooledList<fxFibRatioModel>();
        public static readonly PooledList< fxFibRatioModel > Wave4RetracementFibLevels         = new PooledList<fxFibRatioModel>();
        public static readonly PooledList< fxFibRatioModel > Wave5ProjectionFibLevels          = new PooledList<fxFibRatioModel>();
        public static readonly PooledList< fxFibRatioModel > Wave5CProjectionFibLevels         = new PooledList<fxFibRatioModel>();

        public static readonly PooledList< fxFibRatioModel > WaveTriBRetracementFibLevels      = new PooledList<fxFibRatioModel>();
        public static readonly PooledList< fxFibRatioModel > WaveTriCRetracementFibLevels      = new PooledList<fxFibRatioModel>();
        public static readonly PooledList< fxFibRatioModel > WaveTriDRetracementFibLevels      = new PooledList<fxFibRatioModel>();
        public static readonly PooledList< fxFibRatioModel > WaveTriERetracementFibLevels      = new PooledList<fxFibRatioModel>();
        public static readonly PooledList< fxFibRatioModel > FirstXProjectionFibLevels         = new PooledList<fxFibRatioModel>();
        public static readonly PooledList< fxFibRatioModel > SecondXProjectionFibLevels        = new PooledList<fxFibRatioModel>();

        public static readonly PooledList< fxFibRatioModel > ABCWaveBRetracementFibLevels      = new PooledList<fxFibRatioModel>();
        public static readonly PooledList< fxFibRatioModel > ABCWaveCProjectionFibLevels       = new PooledList<fxFibRatioModel>();
        public static readonly PooledList< fxFibRatioModel > WaveEFBRetraceFibLevels           = new PooledList<fxFibRatioModel>();

        public static readonly PooledList< fxFibRatioModel > TonyDiscoveryFibLevels            = new PooledList<fxFibRatioModel>();


        public static readonly PooledList<fxFibRatioModel> Wave3ClassicFibLevels               = new PooledList<fxFibRatioModel>();
        public static readonly PooledList<fxFibRatioModel> Wave3ExtendedFibLevels              = new PooledList<fxFibRatioModel>();
        public static readonly PooledList<fxFibRatioModel> Wave3SuperExtendedFibLevels         = new PooledList<fxFibRatioModel>();

        public static readonly PooledList<fxFibRatioModel> Wave3AllFibLevels                   = new PooledList<fxFibRatioModel>();

        public static readonly PooledList<fxFibRatioModel> Wave3CompressFibLevels              = new PooledList<fxFibRatioModel>();

        static fxFibRatioConstants( )
        {
            int i = 0;

            for ( i = 0; i < GlobalConstants.Wave2RetracementLevels.Length; i++ )
            {
                var lvls = new fxFibRatioModel( GlobalConstants.Wave2RetracementLevels[ i ] / 100,   GetFibColor( GlobalConstants.Wave2RetracementStrength[ i ] ), FibonacciTargetType.Retracement );
                Wave2RetracementFibLevels.Add( lvls );
            }

            for ( i = 0; i < GlobalConstants.Wave2RetracementLevels.Length; i++ )
            {
                var lvls = new fxFibRatioModel( GlobalConstants.Wave2RetracementLevels[i] / 100, GetFibColor( GlobalConstants.Wave2RetracementStrength[i] ), FibonacciTargetType.Retracement );
                Wave2RetracementFibLevels.Add( lvls );
            }

            for ( i = 0; i < Wave3Compressed.CompressWave3.Length; i++ )
            {
                var lvls = new fxFibRatioModel( Wave3Compressed.CompressWave3[i].Value / 100, GetFibColor( Wave3Compressed.CompressWave3[i].FibStrength ), Wave3Compressed.CompressWave3[i].TargetType );
                Wave3CompressFibLevels.Add( lvls );
            }

            for ( i = 0; i < GlobalConstants.Wave3AllProjectionLevels.Length; i++ )
            {
                var lvls = new fxFibRatioModel( GlobalConstants.Wave3AllProjectionLevels[i] / 100, GetFibColor( GlobalConstants.Wave3AllProjectionStrength[i] ), FibonacciTargetType.Wave3All );
                Wave3AllFibLevels.Add( lvls );
            }

            for ( i = 0; i < Wave3Classic.ClassicWave3.Length; i++ )
            {
                var lvls = new fxFibRatioModel( Wave3Classic.ClassicWave3[ i ].Value / 100,    GetFibColor( Wave3Classic.ClassicWave3[i].FibStrength ), Wave3Classic.ClassicWave3[i].TargetType );
                Wave3ClassicFibLevels.Add( lvls );
            }

            for ( i = 0; i < Wave3Extended.ExtendedWave3.Length; i++ )
            {
                var lvls = new fxFibRatioModel( Wave3Extended.ExtendedWave3[i].Value / 100, GetFibColor( Wave3Extended.ExtendedWave3[i].FibStrength ), Wave3Extended.ExtendedWave3[i].TargetType );
                Wave3ExtendedFibLevels.Add( lvls );
            }

            for ( i = 0; i < Wave3SuperExtended.SuperExtendedWave3.Length; i++ )
            {
                var lvls = new fxFibRatioModel( Wave3SuperExtended.SuperExtendedWave3[i].Value / 100, GetFibColor( Wave3SuperExtended.SuperExtendedWave3[i].FibStrength ), Wave3SuperExtended.SuperExtendedWave3[i].TargetType  );
                Wave3SuperExtendedFibLevels.Add( lvls );
            }

            for ( i = 0; i < GlobalConstants.Wave3CProjectionLevels.Length; i++ )
            {
                var lvls = new fxFibRatioModel( GlobalConstants.Wave3CProjectionLevels[ i ] / 100,   GetFibColor( GlobalConstants.Wave3CProjectionStrength[ i ] ), FibonacciTargetType.Retracement );
                Wave3CProjectionFibLevels.Add( lvls );
            }

            for ( i = 0; i < GlobalConstants.Wave4RetracementLevels.Length; i++ )
            {
                var lvls = new fxFibRatioModel( GlobalConstants.Wave4RetracementLevels[ i ] / 100,   GetFibColor( GlobalConstants.Wave4RetracementStrength[ i ] ), FibonacciTargetType.Retracement );
                Wave4RetracementFibLevels.Add( lvls );
            }

            for ( i = 0; i < GlobalConstants.Wave5ProjectionLevels.Length; i++ )
            {
                var lvls = new fxFibRatioModel( GlobalConstants.Wave5ProjectionLevels[ i ] / 100,    GetFibColor( GlobalConstants.Wave5ProjectionStrength[ i ] ), FibonacciTargetType.Wave5 );
                Wave5ProjectionFibLevels.Add( lvls );
            }

            for ( i = 0; i < GlobalConstants.Wave5CProjectionLevels.Length; i++ )
            {
                var lvls = new fxFibRatioModel( GlobalConstants.Wave5CProjectionLevels[ i ] / 100,   GetFibColor( GlobalConstants.Wave5CProjectionStrength[ i ] ), FibonacciTargetType.Wave5 );
                Wave5CProjectionFibLevels.Add( lvls );
            }

            for ( i = 0; i < GlobalConstants.WaveTriBRetracementLevels.Length; i++ )
            {
                var lvls = new fxFibRatioModel( GlobalConstants.WaveTriBRetracementLevels[ i ] / 100,   GetFibColor( GlobalConstants.WaveTriBRetracementStrength[ i ] ), FibonacciTargetType.Retracement );
                WaveTriBRetracementFibLevels.Add( lvls );
            }

            for ( i = 0; i < GlobalConstants.WaveTriCRetracementLevels.Length; i++ )
            {
                var lvls = new fxFibRatioModel( GlobalConstants.WaveTriCRetracementLevels[ i ] / 100,   GetFibColor( GlobalConstants.WaveTriCRetracementStrength[ i ] ), FibonacciTargetType.Retracement );
                WaveTriCRetracementFibLevels.Add( lvls );
            }

            for ( i = 0; i < GlobalConstants.WaveTriDRetracementLevels.Length; i++ )
            {
                var lvls = new fxFibRatioModel( GlobalConstants.WaveTriDRetracementLevels[ i ] / 100,   GetFibColor( GlobalConstants.WaveTriDRetracementStrength[ i ] ), FibonacciTargetType.Retracement );
                WaveTriDRetracementFibLevels.Add( lvls );
            }

            for ( i = 0; i < GlobalConstants.WaveTriERetracementLevels.Length; i++ )
            {
                var lvls = new fxFibRatioModel( GlobalConstants.WaveTriERetracementLevels[ i ] / 100,   GetFibColor( GlobalConstants.WaveTriERetracementStrength[ i ] ), FibonacciTargetType.Retracement );
                WaveTriERetracementFibLevels.Add( lvls );
            }

            for ( i = 0; i < GlobalConstants.FirstXProjectionLevels.Length; i++ )
            {
                var lvls = new fxFibRatioModel( GlobalConstants.FirstXProjectionLevels[ i ] / 100,   GetFibColor( GlobalConstants.FirstXProjectionStrength[ i ] ), FibonacciTargetType.Retracement );
                FirstXProjectionFibLevels.Add( lvls );
            }

            for ( i = 0; i < GlobalConstants.SecondXProjectionLevels.Length; i++ )
            {
                var lvls = new fxFibRatioModel( GlobalConstants.SecondXProjectionLevels[ i ] / 100,  GetFibColor( GlobalConstants.SecondXProjectionStrength[ i ] ), FibonacciTargetType.WaveX );
                SecondXProjectionFibLevels.Add( lvls );
            }

            for ( i = 0; i < GlobalConstants.ABCWaveBRetracementLevels.Length; i++ )
            {
                var lvls = new fxFibRatioModel( GlobalConstants.ABCWaveBRetracementLevels[ i ] / 100,  GetFibColor( GlobalConstants.ABCWaveBRetracementStrength[ i ] ), FibonacciTargetType.Retracement );
                ABCWaveBRetracementFibLevels.Add( lvls );
            }

            for ( i = 0; i < GlobalConstants.ABCWaveCProjectionLevels.Length; i++ )
            {
                var lvls = new fxFibRatioModel( GlobalConstants.ABCWaveCProjectionLevels[ i ] / 100, GetFibColor( GlobalConstants.ABCWaveCProjectionStrength[ i ] ), FibonacciTargetType.WaveC );
                ABCWaveCProjectionFibLevels.Add( lvls );
            }

            for ( i = 0; i < GlobalConstants.WaveEFBRetracementLevels.Length; i++ )
            {
                var lvls = new fxFibRatioModel( GlobalConstants.WaveEFBRetracementLevels[ i ] / 100, GetFibColor( GlobalConstants.WaveEFBRetracementStrength[ i ] ), FibonacciTargetType.InverseRetracement );
                WaveEFBRetraceFibLevels.Add( lvls );
            }


            for ( i = 0; i < GlobalConstants.TonyDiscoveryLevels.Length; i++ )
            {
                var lvls = new fxFibRatioModel( GlobalConstants.TonyDiscoveryLevels[ i ] / 100, GetFibColor( GlobalConstants.TonyDiscoveryLevelsStrength[ i ] ), FibonacciTargetType.Retracement );
                TonyDiscoveryFibLevels.Add( lvls );
            }
        }

        public static readonly SolidColorBrush BaseColor    = new SolidColorBrush( Color.FromArgb( byte.MaxValue, 119, 119, 135 ) );
        public static readonly SolidColorBrush Impt0Color   = new SolidColorBrush( Colors.LightGray );
        public static readonly SolidColorBrush Impt5Color   = new SolidColorBrush( Colors.LightGray );
        public static readonly SolidColorBrush Impt10Color  = new SolidColorBrush( Colors.Blue );
        public static readonly SolidColorBrush Impt20Color  = new SolidColorBrush( Colors.Red );
        public static readonly SolidColorBrush Impt50Color  = new SolidColorBrush( Colors.Red );
        public static readonly SolidColorBrush Impt100Color = new SolidColorBrush( Colors.Red );
        public static readonly SolidColorBrush Impt95Color  = new SolidColorBrush( Colors.Magenta );
        public static readonly SolidColorBrush Impt85Color  = new SolidColorBrush( Colors.Red );
        public static readonly SolidColorBrush Impt75Color  = new SolidColorBrush( Colors.Blue );
        public static readonly SolidColorBrush Impt35Color  = new SolidColorBrush( Colors.Violet );
        public static readonly SolidColorBrush Impt25Color  = new SolidColorBrush( Colors.Plum );


        


        public static SolidColorBrush GetFibColor( int strength )
        {
            if ( strength == 0 )
            {
                return Impt0Color;
            }
            else if ( strength == 5 )
            {
                return Impt5Color;
            }
            else if ( strength == 10 )
            {
                return Impt10Color;
            }
            else if ( strength == 20 )
            {
                return Impt20Color;
            }
            else if ( strength == 25 )
            {
                return Impt25Color;
            }
            else if ( strength == 35 )
            {
                return Impt35Color;
            }
            else if ( strength == 50 )
            {
                return Impt50Color;
            }
            else if ( strength == 75 )
            {
                return Impt75Color;
            }
            else if ( strength == 85 )
            {
                return Impt85Color;
            }
            else if ( strength == 95 )
            {
                return Impt95Color;
            }
            else if ( strength == 100 )
            {
                return Impt100Color;
            }

            return BaseColor;
        }

    }
}


