//using fx.Definitions;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Media;

//namespace fx.Common
//{    
//    public static class fxFibRatioConstants
//    {
//        public static readonly PooledList< fxFibRatioModel > Wave2RetracementFibLevels         = new PooledList<fxFibRatioModel>();
//        public static readonly PooledList< fxFibRatioModel > Wave3ProjectionFibLevels          = new PooledList<fxFibRatioModel>();
//        public static readonly PooledList< fxFibRatioModel > Wave3CProjectionFibLevels         = new PooledList<fxFibRatioModel>();
//        public static readonly PooledList< fxFibRatioModel > Wave4RetracementFibLevels         = new PooledList<fxFibRatioModel>();
//        public static readonly PooledList< fxFibRatioModel > Wave5ProjectionFibLevels          = new PooledList<fxFibRatioModel>();
//        public static readonly PooledList< fxFibRatioModel > Wave5CProjectionFibLevels         = new PooledList<fxFibRatioModel>();

//        public static readonly PooledList< fxFibRatioModel > WaveTriBRetracementFibLevels      = new PooledList<fxFibRatioModel>();
//        public static readonly PooledList< fxFibRatioModel > WaveTriCRetracementFibLevels      = new PooledList<fxFibRatioModel>();
//        public static readonly PooledList< fxFibRatioModel > WaveTriDRetracementFibLevels      = new PooledList<fxFibRatioModel>();
//        public static readonly PooledList< fxFibRatioModel > WaveTriERetracementFibLevels      = new PooledList<fxFibRatioModel>();
//        public static readonly PooledList< fxFibRatioModel > FirstXProjectionFibLevels         = new PooledList<fxFibRatioModel>();
//        public static readonly PooledList< fxFibRatioModel > SecondXProjectionFibLevels        = new PooledList<fxFibRatioModel>();

//        public static readonly PooledList< fxFibRatioModel > ABCWaveBRetracementFibLevels      = new PooledList<fxFibRatioModel>();        
//        public static readonly PooledList< fxFibRatioModel > ABCWaveCProjectionFibLevels       = new PooledList<fxFibRatioModel>();
//        public static readonly PooledList< fxFibRatioModel > WaveEFBRetraceFibLevels           = new PooledList<fxFibRatioModel>();

//        public static readonly PooledList< fxFibRatioModel > TonyDiscoveryFibLevels            = new PooledList<fxFibRatioModel>();


//        static fxFibRatioConstants( )
//        {
//            int i = 0;
//            
//            for ( i = 0; i < GlobalConstants.Wave2RetracementLevels.Length; i++ )
//            {
//                var lvls = new fxFibRatioModel( GlobalConstants.Wave2RetracementLevels[ i ],   GetFibColor( GlobalConstants.Wave2RetracementStrength[ i ] ) );
//                Wave2RetracementFibLevels.Add( lvls );
//            }

//            for ( i = 0; i < GlobalConstants.Wave3ProjectionLevels.Length; i++ )
//            {
//                var lvls = new fxFibRatioModel( GlobalConstants.Wave3ProjectionLevels[ i ],    GetFibColor( GlobalConstants.Wave3ProjectionStrength[ i ] ) );
//                Wave3ProjectionFibLevels.Add( lvls );
//            }

//            for ( i = 0; i < GlobalConstants.Wave3CProjectionLevels.Length; i++ )
//            {
//                var lvls = new fxFibRatioModel( GlobalConstants.Wave3CProjectionLevels[ i ],   GetFibColor( GlobalConstants.Wave3CProjectionStrength[ i ] ) );
//                Wave3CProjectionFibLevels.Add( lvls );
//            }

//            for ( i = 0; i < GlobalConstants.Wave4RetracementLevels.Length; i++ )
//            {
//                var lvls = new fxFibRatioModel( GlobalConstants.Wave4RetracementLevels[ i ],   GetFibColor( GlobalConstants.Wave4RetracementStrength[ i ] ) );
//                Wave4RetracementFibLevels.Add( lvls );
//            }

//            for ( i = 0; i < GlobalConstants.Wave5ProjectionLevels.Length; i++ )
//            {
//                var lvls = new fxFibRatioModel( GlobalConstants.Wave5ProjectionLevels[ i ],    GetFibColor( GlobalConstants.Wave5ProjectionStrength[ i ] ) );
//                Wave5ProjectionFibLevels.Add( lvls );
//            }

//            for ( i = 0; i < GlobalConstants.Wave5CProjectionLevels.Length; i++ )
//            {
//                var lvls = new fxFibRatioModel( GlobalConstants.Wave5CProjectionLevels[ i ],   GetFibColor( GlobalConstants.Wave5CProjectionStrength[ i ] ) );
//                Wave5CProjectionFibLevels.Add( lvls );
//            }

//            for ( i = 0; i < GlobalConstants.WaveTriBRetracementLevels.Length; i++ )
//            {
//                var lvls = new fxFibRatioModel( GlobalConstants.WaveTriBRetracementLevels[ i ],   GetFibColor( GlobalConstants.WaveTriBRetracementStrength[ i ] ) );
//                WaveTriBRetracementFibLevels.Add( lvls );
//            }

//            for ( i = 0; i < GlobalConstants.WaveTriCRetracementLevels.Length; i++ )
//            {
//                var lvls = new fxFibRatioModel( GlobalConstants.WaveTriCRetracementLevels[ i ],   GetFibColor( GlobalConstants.WaveTriCRetracementStrength[ i ] ) );
//                WaveTriCRetracementFibLevels.Add( lvls );
//            }

//            for ( i = 0; i < GlobalConstants.WaveTriDRetracementLevels.Length; i++ )
//            {
//                var lvls = new fxFibRatioModel( GlobalConstants.WaveTriDRetracementLevels[ i ],   GetFibColor( GlobalConstants.WaveTriDRetracementStrength[ i ] ) );
//                WaveTriDRetracementFibLevels.Add( lvls );
//            }

//            for ( i = 0; i < GlobalConstants.WaveTriERetracementLevels.Length; i++ )
//            {
//                var lvls = new fxFibRatioModel( GlobalConstants.WaveTriERetracementLevels[ i ],   GetFibColor( GlobalConstants.WaveTriERetracementStrength[ i ] ) );
//                WaveTriERetracementFibLevels.Add( lvls );
//            }

//            for ( i = 0; i < GlobalConstants.FirstXProjectionLevels.Length; i++ )
//            {
//                var lvls = new fxFibRatioModel( GlobalConstants.FirstXProjectionLevels[ i ],   GetFibColor( GlobalConstants.FirstXProjectionStrength[ i ] ) );
//                FirstXProjectionFibLevels.Add( lvls );
//            }

//            for ( i = 0; i < GlobalConstants.SecondXProjectionLevels.Length; i++ )
//            {
//                var lvls = new fxFibRatioModel( GlobalConstants.SecondXProjectionLevels[ i ],  GetFibColor( GlobalConstants.SecondXProjectionStrength[ i ] ) );
//                SecondXProjectionFibLevels.Add( lvls );
//            }

//            for ( i = 0; i < GlobalConstants.ABCWaveBRetracementLevels.Length; i++ )
//            {
//                var lvls = new fxFibRatioModel( GlobalConstants.ABCWaveBRetracementLevels[ i ],  GetFibColor( GlobalConstants.ABCWaveBRetracementStrength[ i ] ) );
//                ABCWaveBRetracementFibLevels.Add( lvls );
//            }

//            for ( i = 0; i < GlobalConstants.ABCWaveCProjectionLevels.Length; i++ )
//            {
//                var lvls = new fxFibRatioModel( GlobalConstants.ABCWaveCProjectionLevels[ i ], GetFibColor( GlobalConstants.ABCWaveCProjectionStrength[ i ] ) );
//                ABCWaveCProjectionFibLevels.Add( lvls );
//            }

//            for ( i = 0; i < GlobalConstants.WaveEFBRetracementLevels.Length; i++ )
//            {
//                var lvls = new fxFibRatioModel( GlobalConstants.WaveEFBRetracementLevels[ i ], GetFibColor( GlobalConstants.WaveEFBRetracementStrength[ i ] ) );
//                WaveEFBRetraceFibLevels.Add( lvls );
//            }


//            for ( i = 0; i < GlobalConstants.TonyDiscoveryLevels.Length; i++ )
//            {
//                var lvls = new fxFibRatioModel( GlobalConstants.TonyDiscoveryLevels[ i ], GetFibColor( GlobalConstants.TonyDiscoveryLevelsStrength[ i ] ) );
//                TonyDiscoveryFibLevels.Add( lvls );
//            }
//        }

//        public static readonly SolidColorBrush BaseColor    = new SolidColorBrush( Color.FromArgb( byte.MaxValue, 119, 119, 135 ) );
//        public static readonly SolidColorBrush Impt0Color   = new SolidColorBrush( Colors.Brown );
//        public static readonly SolidColorBrush Impt5Color   = new SolidColorBrush( Colors.SteelBlue );
//        public static readonly SolidColorBrush Impt10Color  = new SolidColorBrush( Colors.Blue );
//        public static readonly SolidColorBrush Impt20Color  = new SolidColorBrush( Colors.Red );
//        public static readonly SolidColorBrush Impt50Color  = new SolidColorBrush( Colors.Red );
//        public static readonly SolidColorBrush Impt100Color = new SolidColorBrush( Colors.Red );


//        public static SolidColorBrush GetFibColor( int strength )
//        {
//            if ( strength == 0 )
//            {
//                return Impt0Color;
//            }
//            else if ( strength == 5 )
//            {
//                return Impt5Color;
//            }
//            else if ( strength == 10 )
//            {
//                return Impt10Color;
//            }
//            else if ( strength == 20 )
//            {
//                return Impt20Color;
//            }
//            else if ( strength == 50 )
//            {
//                return Impt50Color;
//            }
//            else if ( strength == 100 )
//            {
//                return Impt100Color;
//            }

//            return BaseColor;
//        }

//    }
//}
