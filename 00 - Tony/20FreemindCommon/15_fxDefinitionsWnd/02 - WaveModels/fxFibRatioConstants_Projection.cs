
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
        //public static readonly PooledList< fxFibRatioModel > Wave2RetracementFibLevels         = new PooledList<fxFibRatioModel>();
        //public static readonly PooledList< fxFibRatioModel > Wave3CProjectionFibLevels         = new PooledList<fxFibRatioModel>();
        //public static readonly PooledList< fxFibRatioModel > Wave4RetracementFibLevels         = new PooledList<fxFibRatioModel>();
        //public static readonly PooledList< fxFibRatioModel > Wave5ProjectionFibLevels          = new PooledList<fxFibRatioModel>();
        //public static readonly PooledList< fxFibRatioModel > Wave5CProjectionFibLevels         = new PooledList<fxFibRatioModel>();

        //public static readonly PooledList< fxFibRatioModel > WaveTriBRetracementFibLevels      = new PooledList<fxFibRatioModel>();
        //public static readonly PooledList< fxFibRatioModel > WaveTriCRetracementFibLevels      = new PooledList<fxFibRatioModel>();
        //public static readonly PooledList< fxFibRatioModel > WaveTriDRetracementFibLevels      = new PooledList<fxFibRatioModel>();
        //public static readonly PooledList< fxFibRatioModel > WaveTriERetracementFibLevels      = new PooledList<fxFibRatioModel>();
        //public static readonly PooledList< fxFibRatioModel > FirstXProjectionFibLevels         = new PooledList<fxFibRatioModel>();
        //public static readonly PooledList< fxFibRatioModel > SecondXProjectionFibLevels        = new PooledList<fxFibRatioModel>();

        //public static readonly PooledList< fxFibRatioModel > ABCWaveBRetracementFibLevels      = new PooledList<fxFibRatioModel>();
        //public static readonly PooledList< fxFibRatioModel > ABCWaveCProjectionFibLevels       = new PooledList<fxFibRatioModel>();
        //public static readonly PooledList< fxFibRatioModel > WaveEFBRetraceFibLevels           = new PooledList<fxFibRatioModel>();

        //public static readonly PooledList< fxFibRatioModel > TonyDiscoveryFibLevels            = new PooledList<fxFibRatioModel>();


        //public static readonly PooledList<fxFibRatioModel> Wave3ClassicFibLevels               = new PooledList<fxFibRatioModel>();
        //public static readonly PooledList<fxFibRatioModel> Wave3ExtendedFibLevels              = new PooledList<fxFibRatioModel>();
        //public static readonly PooledList<fxFibRatioModel> Wave3SuperExtendedFibLevels         = new PooledList<fxFibRatioModel>();

        //public static readonly PooledList<fxFibRatioModel> Wave3AllFibLevels                   = new PooledList<fxFibRatioModel>();

        //public static readonly PooledList<fxFibRatioModel> Wave3CompressFibLevels              = new PooledList<fxFibRatioModel>();

        static fxFibRatioConstants( )
        {
            //int i = 0;

            //for ( i = 0; i < WaveFibConstants.Wave2RetracementLevels.Length; i++ )
            //{
            //    var lvls = new fxFibRatioModel( WaveFibConstants.Wave2RetracementLevels[ i ].FibValue/100, GetFibColor( WaveFibConstants.Wave2RetracementLevels[ i ].FibStrength ), WaveFibConstants.Wave2RetracementLevels[i].FibTargetType );
            //    Wave2RetracementFibLevels.Add( lvls );
            //}
            
            //for ( i = 0; i < WaveFibConstants.CompressWave3.Length; i++ )
            //{
            //    var lvls = new fxFibRatioModel( WaveFibConstants.CompressWave3[i].FibValue / 100, GetFibColor( WaveFibConstants.CompressWave3[i].FibStrength ), WaveFibConstants.CompressWave3[i].FibTargetType );
            //    Wave3CompressFibLevels.Add( lvls );
            //}

            //for ( i = 0; i < WaveFibConstants.AllWave3.Length; i++ )
            //{
            //    var lvls = new fxFibRatioModel( WaveFibConstants.AllWave3[i].FibValue / 100, GetFibColor( WaveFibConstants.AllWave3[i].FibStrength ), WaveFibConstants.AllWave3[i].FibTargetType );
            //    Wave3AllFibLevels.Add( lvls );
            //}

            //for ( i = 0; i < WaveFibConstants.ClassicWave3.Length; i++ )
            //{
            //    var lvls = new fxFibRatioModel( WaveFibConstants.ClassicWave3[ i ].FibValue / 100,    GetFibColor( WaveFibConstants.ClassicWave3[i].FibStrength ), WaveFibConstants.ClassicWave3[i].FibTargetType );
            //    Wave3ClassicFibLevels.Add( lvls );
            //}

            //for ( i = 0; i < WaveFibConstants.ExtendedWave3.Length; i++ )
            //{
            //    var lvls = new fxFibRatioModel( WaveFibConstants.ExtendedWave3[i].FibValue / 100, GetFibColor( WaveFibConstants.ExtendedWave3[i].FibStrength ), WaveFibConstants.ExtendedWave3[i].FibTargetType );
            //    Wave3ExtendedFibLevels.Add( lvls );
            //}

            //for ( i = 0; i < WaveFibConstants.SuperExtendedWave3.Length; i++ )
            //{
            //    var lvls = new fxFibRatioModel( WaveFibConstants.SuperExtendedWave3[i].FibValue / 100, GetFibColor( WaveFibConstants.SuperExtendedWave3[i].FibStrength ), WaveFibConstants.SuperExtendedWave3[i].FibTargetType  );
            //    Wave3SuperExtendedFibLevels.Add( lvls );
            //}

            //for ( i = 0; i < WaveFibConstants.Wave3CProjectionLevels.Length; i++ )
            //{
            //    var lvls = new fxFibRatioModel( WaveFibConstants.Wave3CProjectionLevels[ i ].FibValue / 100,   GetFibColor( WaveFibConstants.Wave3CProjectionLevels[ i ].FibStrength ), WaveFibConstants.Wave3CProjectionLevels[i].FibTargetType );
            //    Wave3CProjectionFibLevels.Add( lvls );
            //}

            //for ( i = 0; i < WaveFibConstants.Wave4RetracementLevels.Length; i++ )
            //{
            //    var lvls = new fxFibRatioModel( WaveFibConstants.Wave4RetracementLevels[ i ].FibValue / 100,   GetFibColor( WaveFibConstants.Wave4RetracementLevels[ i ].FibStrength ), WaveFibConstants.Wave4RetracementLevels[i].FibTargetType );
            //    Wave4RetracementFibLevels.Add( lvls );
            //}

            //for ( i = 0; i < WaveFibConstants.Wave5ProjectionFibLevels.Length; i++ )
            //{
            //    var lvls = new fxFibRatioModel( WaveFibConstants.Wave5ProjectionFibLevels[ i ].FibValue / 100,    GetFibColor( WaveFibConstants.Wave5ProjectionFibLevels[ i ].FibStrength ), WaveFibConstants.Wave5ProjectionFibLevels[i].FibTargetType );
            //    Wave5ProjectionFibLevels.Add( lvls );
            //}

            //for ( i = 0; i < WaveFibConstants.Wave5CProjectionFibLevelType.Length; i++ )
            //{
            //    var lvls = new fxFibRatioModel( WaveFibConstants.Wave5CProjectionFibLevelType[ i ].FibValue / 100,   GetFibColor( WaveFibConstants.Wave5CProjectionFibLevelType[ i ].FibStrength ), WaveFibConstants.Wave5CProjectionFibLevelType[i].FibTargetType );
            //    Wave5CProjectionFibLevels.Add( lvls );
            //}

            //for ( i = 0; i < WaveFibConstants.WaveTriBRetracementLevels.Length; i++ )
            //{
            //    var lvls = new fxFibRatioModel( WaveFibConstants.WaveTriBRetracementLevels[ i ].FibValue / 100,   GetFibColor( WaveFibConstants.WaveTriBRetracementLevels[ i ].FibStrength ), WaveFibConstants.WaveTriBRetracementLevels[i].FibTargetType );
            //    WaveTriBRetracementFibLevels.Add( lvls );
            //}

            //for ( i = 0; i < WaveFibConstants.WaveTriCRetracementLevels.Length; i++ )
            //{
            //    var lvls = new fxFibRatioModel( WaveFibConstants.WaveTriCRetracementLevels[ i ].FibValue / 100,   GetFibColor( WaveFibConstants.WaveTriCRetracementLevels[ i ].FibStrength ), WaveFibConstants.WaveTriCRetracementLevels[i].FibTargetType );
            //    WaveTriCRetracementFibLevels.Add( lvls );
            //}

            //for ( i = 0; i < WaveFibConstants.WaveTriDRetracementLevels.Length; i++ )
            //{
            //    var lvls = new fxFibRatioModel( WaveFibConstants.WaveTriDRetracementLevels[ i ].FibValue / 100,   GetFibColor( WaveFibConstants.WaveTriDRetracementLevels[ i ].FibStrength ), WaveFibConstants.WaveTriDRetracementLevels[i].FibTargetType );
            //    WaveTriDRetracementFibLevels.Add( lvls );
            //}

            //for ( i = 0; i < WaveFibConstants.WaveTriERetracementLevels.Length; i++ )
            //{
            //    var lvls = new fxFibRatioModel( WaveFibConstants.WaveTriERetracementLevels[ i ].FibValue / 100,   GetFibColor( WaveFibConstants.WaveTriERetracementLevels[ i ].FibStrength ), WaveFibConstants.WaveTriERetracementLevels[i].FibTargetType );
            //    WaveTriERetracementFibLevels.Add( lvls );
            //}

            //for ( i = 0; i < WaveFibConstants.FirstXProjectionLevels.Length; i++ )
            //{
            //    var lvls = new fxFibRatioModel( WaveFibConstants.FirstXProjectionLevels[ i ].FibValue / 100,   GetFibColor( WaveFibConstants.FirstXProjectionLevels[i].FibStrength ), WaveFibConstants.FirstXProjectionLevels[i].FibTargetType );
            //    FirstXProjectionFibLevels.Add( lvls );
            //}

            //for ( i = 0; i < WaveFibConstants.FirstXProjectionLevels.Length; i++ )
            //{
            //    var lvls = new fxFibRatioModel( WaveFibConstants.FirstXProjectionLevels[i].FibValue / 100, GetFibColor( WaveFibConstants.FirstXProjectionLevels[i].FibStrength ), WaveFibConstants.FirstXProjectionLevels[i].FibTargetType );
            //    SecondXProjectionFibLevels.Add( lvls );
            //}

            //for ( i = 0; i < WaveFibConstants.ABCWaveBRetracementLevels.Length; i++ )
            //{
            //    var lvls = new fxFibRatioModel( WaveFibConstants.ABCWaveBRetracementLevels[ i ].FibValue / 100,  GetFibColor( WaveFibConstants.ABCWaveBRetracementLevels[ i ].FibStrength ), WaveFibConstants.ABCWaveBRetracementLevels[i].FibTargetType );
            //    ABCWaveBRetracementFibLevels.Add( lvls );
            //}

            //for ( i = 0; i < WaveFibConstants.ABCWaveCProjectionFibLevels.Length; i++ )
            //{
            //    var lvls = new fxFibRatioModel( WaveFibConstants.ABCWaveCProjectionFibLevels[ i ].FibValue / 100, GetFibColor( WaveFibConstants.ABCWaveCProjectionFibLevels[i].FibStrength ), WaveFibConstants.ABCWaveCProjectionFibLevels[i].FibTargetType );
            //    ABCWaveCProjectionFibLevels.Add( lvls );
            //}

            //for ( i = 0; i < WaveFibConstants.WaveEFBRetracementLevels.Length; i++ )
            //{
            //    var lvls = new fxFibRatioModel( WaveFibConstants.WaveEFBRetracementLevels[ i ].FibValue / 100, GetFibColor( WaveFibConstants.WaveEFBRetracementLevels[ i ].FibStrength ), WaveFibConstants.WaveEFBRetracementLevels[i].FibTargetType );
            //    WaveEFBRetraceFibLevels.Add( lvls );
            //}


            //for ( i = 0; i < WaveFibConstants.TonyDiscoveryLevels.Length; i++ )
            //{
            //    var lvls = new fxFibRatioModel( WaveFibConstants.TonyDiscoveryLevels[ i ].FibValue / 100, GetFibColor( WaveFibConstants.TonyDiscoveryLevels[ i ].FibStrength ), WaveFibConstants.TonyDiscoveryLevels[i].FibTargetType );
            //    TonyDiscoveryFibLevels.Add( lvls );
            //}
        }

        

    }
}


