//using fx.Common;
//using Itenso.TimePeriod;
//using fx.Definitions;
//using StockSharp.BusinessEntities;
//using StockSharp.Messages;
//using System;
//using System.Collections.Generic; using fx.Collections;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace fx.Charting
//{
//    public static class SRlevelsHelper
//    {
//        public static SR2ndType ToSR2ndType( this ElliottWaveEnum waveName )
//        {
//            switch ( waveName )
//            {
//                case ElliottWaveEnum.NONE:
//                    return SR2ndType.NONE;

//                case ElliottWaveEnum.Wave1:
//                case ElliottWaveEnum.Wave1C:
//                case ElliottWaveEnum.Wave3:
//                case ElliottWaveEnum.Wave3C:
//                case ElliottWaveEnum.Wave5:
//                case ElliottWaveEnum.Wave5C:
//                    return SR2ndType.Impulsive;

//                case ElliottWaveEnum.WaveA:
//                case ElliottWaveEnum.WaveB:
//                case ElliottWaveEnum.WaveC:
//                case ElliottWaveEnum.WaveX:
//                case ElliottWaveEnum.Wave2:
//                case ElliottWaveEnum.Wave4:
//                    return SR2ndType.Correction;


//                case ElliottWaveEnum.WaveTA:
//                case ElliottWaveEnum.WaveTB:
//                case ElliottWaveEnum.WaveTC:
//                case ElliottWaveEnum.WaveTD:
//                case ElliottWaveEnum.WaveTE:
//                    return SR2ndType.Triangle;

//                case ElliottWaveEnum.WaveEFA:
//                case ElliottWaveEnum.WaveEFB:
//                case ElliottWaveEnum.WaveEFC:
//                    return SR2ndType.ExpandedFlat;
//            }

//            return SR2ndType.NONE;
//        }

//        public static SR1stType ToSR1stType( this FibonacciType signal )
//        {
//            switch ( signal )
//            {
//                case FibonacciType.WaveCProjection:
//                    return SR1stType.HewEXP;

//                case FibonacciType.Wave2Retracement:
//                    return SR1stType.HewRET;

//                case FibonacciType.Wave3Projection:
//                    return SR1stType.HewEXP;


//                case FibonacciType.Wave3CProjection:
//                    return SR1stType.HewEXP;


//                case FibonacciType.Wave4Retracement:
//                    return SR1stType.HewRET;


//                case FibonacciType.Wave5Projection:
//                    return SR1stType.HewEXP;


//                case FibonacciType.Wave5CProjection:
//                    return SR1stType.HewEXP;


//                case FibonacciType.ABCWaveCProjection:
//                    return SR1stType.HewEXP;


//                case FibonacciType.ABCWaveBRetracement:
//                    return SR1stType.HewRET;

//                case FibonacciType.WaveEFBRetracement:
//                    return SR1stType.HewRET;

//                case FibonacciType.WaveTriBRetracement:
//                    return SR1stType.HewRET;

//                case FibonacciType.WaveTriCProjection:
//                    return SR1stType.HewEXP;

//                case FibonacciType.WaveTriDProjection:
//                    return SR1stType.HewEXP;

//                case FibonacciType.WaveTriEProjection:
//                    return SR1stType.HewEXP;

//                case FibonacciType.FirstXProjection:
//                    return SR1stType.HewEXP;

//                case FibonacciType.SecondXProjection:
//                    return SR1stType.HewEXP;
//            }

//            return SR1stType.Unknown;
//        }
//        public static SR3rdType ToSR3rdType( this FibonacciType signal )
//        {
//            switch ( signal )
//            {
//                case FibonacciType.WaveCProjection:
//                    return SR3rdType.WaveCProjection;

//                case FibonacciType.Wave2Retracement:
//                    return SR3rdType.Wave2Retracement;

//                case FibonacciType.Wave3Projection:
//                    return SR3rdType.Wave3Projection;


//                case FibonacciType.Wave3CProjection:
//                    return SR3rdType.Wave3CProjection;


//                case FibonacciType.Wave4Retracement:
//                    return SR3rdType.Wave4Retracement;


//                case FibonacciType.Wave5Projection:
//                    return SR3rdType.Wave5Projection;


//                case FibonacciType.Wave5CProjection:
//                    return SR3rdType.Wave5CProjection;


//                case FibonacciType.ABCWaveCProjection:
//                    return SR3rdType.ABCWaveCProjection;


//                case FibonacciType.ABCWaveBRetracement:
//                    return SR3rdType.ABCWaveBRetracement;

//                case FibonacciType.WaveEFBRetracement:
//                    return SR3rdType.WaveEFBRetracement;

//                case FibonacciType.WaveTriBRetracement:
//                    return SR3rdType.WaveTriBRetracement;

//                case FibonacciType.WaveTriCProjection:
//                    return SR3rdType.WaveTriCProjection;

//                case FibonacciType.WaveTriDProjection:
//                    return SR3rdType.WaveTriDProjection;

//                case FibonacciType.WaveTriEProjection:
//                    return SR3rdType.WaveTriEProjection;

//                case FibonacciType.FirstXProjection:
//                    return SR3rdType.FirstXProjection;

//                case FibonacciType.SecondXProjection:
//                    return SR3rdType.SecondXProjection;
//            }

//            return SR3rdType.NONE;
//        }
//        
//        

//        public static PooledList<SRlevel> GetFibonacciSRLevels( FibonacciType fibCalculationType,
//                                                          (DateTime Time, float Value) startPoint,
//                                                          (DateTime Time, float Value) endPoint,
//                                                          (DateTime Time, float Value) projectionPoint )
//        {
//            var output = new PooledList<SRlevel>( 20 );

//            int i = 0;

//            switch ( fibCalculationType )
//            {
//                case FibonacciType.WaveCProjection:
//                {
//                    for ( i = 0; i < GlobalConstants.ABCWaveCProjectionLevels.Length; i++ )
//                    {
//                        var level    = projectionPoint.Value + ( ( endPoint.Value - startPoint.Value ) * GlobalConstants.ABCWaveCProjectionLevels [ i ] / 100 );
//                        var strength = GlobalConstants.ABCWaveCProjectionStrength[ i ];
//                        var tb       = new TimeBlock( startPoint.Time, endPoint.Time );
//                        var lvl      = new SRlevel( tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.WaveCProjection );

//                        output.Add( lvl );
//                    }
//                }
//                break;

//                case FibonacciType.Wave2Retracement:
//                {
//                    for ( i = 0; i < GlobalConstants.Wave2RetracementLevels.Length; i++ )
//                    {
//                        var level    = endPoint.Value + ( ( startPoint.Value - endPoint.Value ) * GlobalConstants.Wave2RetracementLevels [ i ] / 100 ) ;
//                        var strength = GlobalConstants.Wave2RetracementStrength[ i ];
//                        var tb       = new TimeBlock( startPoint.Time, endPoint.Time );
//                        var lvl      = new SRlevel( tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.Wave2Retracement );

//                        output.Add( lvl );
//                    }
//                }
//                break;

//                case FibonacciType.FirstXProjection:
//                {
//                    for ( i = 0; i < GlobalConstants.FirstXProjectionLevels.Length; i++ )
//                    {
//                        var level    = projectionPoint.Value + ((endPoint.Value - startPoint.Value) * GlobalConstants.FirstXProjectionLevels[i] / 100);
//                        var strength = GlobalConstants.FirstXProjectionStrength[i];
//                        var tb       = new TimeBlock( startPoint.Time, projectionPoint.Time );
//                        var lvl      = new SRlevel( tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.FirstXProjection );

//                        output.Add( lvl );
//                    }
//                }
//                break;

//                case FibonacciType.SecondXProjection:
//                {
//                    for ( i = 0; i < GlobalConstants.SecondXProjectionLevels.Length; i++ )
//                    {
//                        var level    = projectionPoint.Value + ((endPoint.Value - startPoint.Value) * GlobalConstants.SecondXProjectionLevels[i] / 100);
//                        var strength = GlobalConstants.SecondXProjectionStrength[i];
//                        var tb       = new TimeBlock( startPoint.Time, projectionPoint.Time );
//                        var lvl      = new SRlevel( tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.SecondXProjection );

//                        output.Add( lvl );
//                    }
//                }
//                break;

//                case FibonacciType.Wave3Projection:
//                {
//                    for ( i = 0; i < GlobalConstants.Wave3ProjectionLevels.Length; i++ )
//                    {
//                        var level    = projectionPoint.Value + ( ( endPoint.Value - startPoint.Value ) * GlobalConstants.Wave3ProjectionLevels [ i ] / 100 );
//                        var strength = GlobalConstants.Wave3ProjectionStrength[ i ];
//                        var tb       = new TimeBlock( startPoint.Time, projectionPoint.Time );
//                        var lvl      = new SRlevel( tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.Wave3Projection );

//                        output.Add( lvl );
//                    }
//                }
//                break;

//                case FibonacciType.Wave3CProjection:
//                {
//                    for ( i = 0; i < GlobalConstants.Wave3CProjectionLevels.Length; i++ )
//                    {
//                        var level    = projectionPoint.Value + ( ( endPoint.Value- startPoint.Value ) * GlobalConstants.Wave3CProjectionLevels [ i ] / 100 );
//                        var strength = GlobalConstants.Wave3CProjectionStrength[ i ];
//                        var tb       = new TimeBlock( startPoint.Time, projectionPoint.Time );
//                        var lvl      = new SRlevel( tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.Wave3CProjection );

//                        output.Add( lvl );
//                    }
//                }
//                break;

//                case FibonacciType.Wave4Retracement:
//                {
//                    for ( i = 0; i < GlobalConstants.Wave4RetracementLevels.Length; i++ )
//                    {
//                        var level    = endPoint.Value + ( ( startPoint.Value - endPoint.Value ) * GlobalConstants.Wave4RetracementLevels [ i ] / 100 );
//                        var strength = GlobalConstants.Wave4RetracementStrength[ i ];
//                        var tb       = new TimeBlock( startPoint.Time, endPoint.Time );
//                        var lvl      = new SRlevel( tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.Wave4Retracement );

//                        output.Add( lvl );
//                    }
//                }
//                break;

//                case FibonacciType.Wave5Projection:
//                {
//                    for ( i = 0; i < GlobalConstants.Wave5ProjectionLevels.Length; i++ )
//                    {
//                        var level    = projectionPoint.Value + ( ( endPoint.Value - startPoint.Value ) * GlobalConstants.Wave5ProjectionLevels [ i ] / 100 );
//                        var strength = GlobalConstants.Wave5ProjectionStrength[ i ];
//                        var tb       = new TimeBlock( startPoint.Time, projectionPoint.Time );
//                        var lvl      = new SRlevel( tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.Wave5Projection );

//                        output.Add( lvl );
//                    }
//                }
//                break;

//                case FibonacciType.Wave5CProjection:
//                {
//                    for ( i = 0; i < GlobalConstants.Wave5CProjectionLevels.Length; i++ )
//                    {
//                        var level    = projectionPoint.Value + ( ( endPoint.Value - startPoint.Value ) * GlobalConstants.Wave5CProjectionLevels [ i ] / 100 );
//                        var strength = GlobalConstants.Wave5CProjectionStrength[ i ];
//                        var tb       = new TimeBlock( startPoint.Time, projectionPoint.Time );
//                        var lvl      = new SRlevel( tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.Wave5CProjection );

//                        output.Add( lvl );
//                    }
//                }
//                break;

//                case FibonacciType.ABCWaveCProjection:
//                {
//                    for ( i = 0; i < GlobalConstants.ABCWaveCProjectionLevels.Length; i++ )
//                    {
//                        var level    = projectionPoint.Value + ( ( endPoint.Value - startPoint.Value ) * GlobalConstants.ABCWaveCProjectionLevels [ i ] / 100 );
//                        var strength = GlobalConstants.ABCWaveCProjectionStrength[ i ];
//                        var tb       = new TimeBlock( startPoint.Time, projectionPoint.Time );
//                        var lvl      = new SRlevel( tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.ABCWaveCProjection );

//                        output.Add( lvl );
//                    }
//                }
//                break;

//                case FibonacciType.ABCWaveBRetracement:
//                {
//                    for ( i = 0; i < GlobalConstants.ABCWaveBRetracementLevels.Length; i++ )
//                    {
//                        var level    = endPoint.Value + ( ( startPoint.Value - endPoint.Value ) * GlobalConstants.ABCWaveBRetracementLevels [ i ] / 100 ) ;
//                        var strength = GlobalConstants.ABCWaveBRetracementStrength[ i ];
//                        var tb       = new TimeBlock( startPoint.Time, endPoint.Time );
//                        var lvl      = new SRlevel( tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.ABCWaveBRetracement );

//                        output.Add( lvl );
//                    }
//                }
//                break;

//                case FibonacciType.WaveEFBRetracement:
//                {
//                    for ( i = 0; i < GlobalConstants.WaveEFBRetracementLevels.Length; i++ )
//                    {
//                        var level = endPoint.Value + ( ( startPoint.Value - endPoint.Value ) * GlobalConstants.WaveEFBRetracementLevels [ i ] / 100 ) ;
//                        var strength = GlobalConstants.WaveEFBRetracementStrength[ i ];
//                        var tb       = new TimeBlock( startPoint.Time, endPoint.Time );
//                        var lvl      = new SRlevel( tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.WaveEFBRetracement );

//                        output.Add( lvl );
//                    }
//                }
//                break;

//                case FibonacciType.WaveTriBRetracement:
//                {
//                    for ( i = 0; i < GlobalConstants.WaveTriBRetracementLevels.Length; i++ )
//                    {
//                        var level = endPoint.Value + ( ( startPoint.Value - endPoint.Value ) * GlobalConstants.WaveTriBRetracementLevels [ i ] / 100 ) ;
//                        var strength = GlobalConstants.WaveTriBRetracementStrength[ i ];
//                        var tb       = new TimeBlock( startPoint.Time, endPoint.Time );
//                        var lvl      = new SRlevel( tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.WaveTriBRetracement );

//                        output.Add( lvl );
//                    }
//                }
//                break;

//                case FibonacciType.WaveTriCProjection:
//                {
//                    for ( i = 0; i < GlobalConstants.WaveTriCRetracementLevels.Length; i++ )
//                    {
//                        var level = endPoint.Value + ( ( startPoint.Value - endPoint.Value ) * GlobalConstants.WaveTriCRetracementLevels [ i ] / 100 ) ;
//                        var strength = GlobalConstants.WaveTriCRetracementStrength[ i ];
//                        var tb       = new TimeBlock( startPoint.Time, endPoint.Time );
//                        var lvl      = new SRlevel( tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.WaveTriCProjection );

//                        output.Add( lvl );
//                    }
//                }
//                break;

//                case FibonacciType.WaveTriDProjection:
//                {
//                    for ( i = 0; i < GlobalConstants.WaveTriDRetracementLevels.Length; i++ )
//                    {
//                        var level = endPoint.Value + ( ( startPoint.Value - endPoint.Value ) * GlobalConstants.WaveTriDRetracementLevels [ i ] / 100 );
//                        var strength = GlobalConstants.WaveTriDRetracementStrength[ i ];
//                        var tb       = new TimeBlock( startPoint.Time, endPoint.Time );
//                        var lvl      = new SRlevel( tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.WaveTriDProjection );

//                        output.Add( lvl );
//                    }
//                }
//                break;

//                case FibonacciType.WaveTriEProjection:
//                {
//                    for ( i = 0; i < GlobalConstants.WaveTriERetracementLevels.Length; i++ )
//                    {
//                        var level = endPoint.Value + ( ( startPoint.Value - endPoint.Value ) * GlobalConstants.WaveTriERetracementLevels [ i ] / 100 );
//                        var strength = GlobalConstants.WaveTriERetracementStrength[ i ];
//                        var tb       = new TimeBlock( startPoint.Time, endPoint.Time );
//                        var lvl      = new SRlevel( tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.WaveTriEProjection );

//                        output.Add( lvl );
//                    }
//                }
//                break;

//                case FibonacciType.TonyProjection:
//                {
//                    for ( i = 0; i < GlobalConstants.TonyDiscoveryLevels.Length; i++ )
//                    {
//                        var level = endPoint.Value + ( ( startPoint.Value - endPoint.Value ) * GlobalConstants.TonyDiscoveryLevels [ i ] / 100 );
//                        var strength = GlobalConstants.TonyDiscoveryLevelsStrength[ i ];

//                        var tb       = new TimeBlock( startPoint.Time, endPoint.Time );
//                        var lvl      = new SRlevel( tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.TonyProjection );

//                        output.Add( lvl );
//                    }
//                }
//                break;
//            }

//            return output;
//        }
//    }
//}


