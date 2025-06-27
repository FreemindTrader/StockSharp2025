using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.ComponentModel;
using System.Data;
using fx.Definitions.Collections;
using fx.Collections;
using fx.Definitions;
using fx.Bars;
using fx.Common;

namespace fx.Algorithm
{
    /// <summary>
    /// Class contains general helper functionality for financial classes and operation.
    /// </summary>
    public static class harmonicEWave
    {
        public static PooledList<double> CalcFibLevels( FibonacciType fibCalculationType,
                                                              double startPoint,
                                                              double endPoint,
                                                              double projectionPoint )
        {
            PooledList< double > output = new PooledList< double >( 20 );

            int i = 0;

            switch ( fibCalculationType )
            {
                case FibonacciType.WaveCProjection:
                    {
                        for ( i = 0; i < GlobalConstants.Wave3CProjectionLevels.Length; i++ )
                        {
                            output.Add( projectionPoint + ( ( endPoint - startPoint ) * GlobalConstants.Wave3CProjectionLevels[ i ] / 100 ) );
                        }
                    }
                    break;

                case FibonacciType.Wave2Retracement:
                    {
                        for ( i = 0; i < GlobalConstants.Wave2RetracementLevels.Length; i++ )
                        {
                            output.Add( endPoint + ( ( startPoint - endPoint ) * GlobalConstants.Wave2RetracementLevels[ i ] / 100 ) );
                        }
                    }
                    break;

                case FibonacciType.Wave3Projection:
                    {
                        for ( i = 0; i < GlobalConstants.Wave3ProjectionLevels.Length; i++ )
                        {
                            output.Add( projectionPoint + ( ( endPoint - startPoint ) * GlobalConstants.Wave3ProjectionLevels[ i ] / 100 ) );
                        }
                    }
                    break;

                case FibonacciType.Wave3CProjection:
                    {
                        for ( i = 0; i < GlobalConstants.Wave3CProjectionLevels.Length; i++ )
                        {
                            output.Add( projectionPoint + ( ( endPoint - startPoint ) * GlobalConstants.Wave3CProjectionLevels[ i ] / 100 ) );
                        }
                    }
                    break;

                case FibonacciType.Wave4Retracement:
                    {
                        for ( i = 0; i < GlobalConstants.Wave4RetracementLevels.Length; i++ )
                        {
                            output.Add( endPoint + ( ( startPoint - endPoint ) * GlobalConstants.Wave4RetracementLevels[ i ] / 100 ) );
                        }
                    }
                    break;

                case FibonacciType.Wave5Projection:
                    {
                        for ( i = 0; i < GlobalConstants.Wave5ProjectionLevels.Length; i++ )
                        {
                            output.Add( projectionPoint + ( ( endPoint - startPoint ) * GlobalConstants.Wave5ProjectionLevels[ i ] / 100 ) );
                        }
                    }
                    break;

                case FibonacciType.Wave5CProjection:
                    {
                        for ( i = 0; i < GlobalConstants.Wave5CProjectionLevels.Length; i++ )
                        {
                            output.Add( projectionPoint + ( ( endPoint - startPoint ) * GlobalConstants.Wave5CProjectionLevels[ i ] / 100 ) );
                        }
                    }
                    break;

                case FibonacciType.ABCWaveCProjection:
                    {
                        for ( i = 0; i < GlobalConstants.ABCWaveCProjectionLevels.Length; i++ )
                        {
                            output.Add( projectionPoint + ( ( endPoint - startPoint ) * GlobalConstants.ABCWaveCProjectionLevels[ i ] / 100 ) );
                        }
                    }
                    break;

                case FibonacciType.ABCWaveBRetracement:
                    {
                        for ( i = 0; i < GlobalConstants.ABCWaveBRetracementLevels.Length; i++ )
                        {
                            output.Add( endPoint + ( ( startPoint - endPoint ) * GlobalConstants.ABCWaveBRetracementLevels[ i ] / 100 ) );
                        }
                    }
                    break;

                case FibonacciType.WaveEFBRetracement:
                    {
                        for ( i = 0; i < GlobalConstants.WaveEFBRetracementLevels.Length; i++ )
                        {
                            output.Add( endPoint + ( ( startPoint - endPoint ) * GlobalConstants.WaveEFBRetracementLevels[ i ] / 100 ) );
                        }
                    }
                    break;

                case FibonacciType.WaveTriBRetracement:
                    {
                        for ( i = 0; i < GlobalConstants.WaveTriBRetracementLevels.Length; i++ )
                        {
                            output.Add( endPoint + ( ( startPoint - endPoint ) * GlobalConstants.WaveTriBRetracementLevels[ i ] / 100 ) );
                        }
                    }
                    break;

                case FibonacciType.WaveTriCProjection:
                    {
                        for ( i = 0; i < GlobalConstants.WaveTriCRetracementLevels.Length; i++ )
                        {
                            output.Add( endPoint + ( ( startPoint - endPoint ) * GlobalConstants.WaveTriCRetracementLevels[ i ] / 100 ) );
                        }
                    }
                    break;

                case FibonacciType.WaveTriDProjection:
                    {
                        for ( i = 0; i < GlobalConstants.WaveTriDRetracementLevels.Length; i++ )
                        {
                            output.Add( endPoint + ( ( startPoint - endPoint ) * GlobalConstants.WaveTriDRetracementLevels[ i ] / 100 ) );
                        }
                    }
                    break;

                case FibonacciType.WaveTriEProjection:
                    {
                        for ( i = 0; i < GlobalConstants.WaveTriERetracementLevels.Length; i++ )
                        {
                            output.Add( endPoint + ( ( startPoint - endPoint ) * GlobalConstants.WaveTriERetracementLevels[ i ] / 100 ) );
                        }
                    }
                    break;
            }

            return output;
        }


        public static PooledList< ( float, int ) > CalculateAcutalFibonacciLevelsAndStrength(   FibonacciType fibCalculationType,
                                                                                          float startPoint,
                                                                                          float endPoint,
                                                                                          float projectionPoint )
        {
            PooledList< ( float, int ) > output = new PooledList<(float, int) >( 20 );

            int i = 0;

            switch ( fibCalculationType )
            {
                case FibonacciType.WaveCProjection:
                {
                    for ( i = 0; i < GlobalConstants.ABCWaveCProjectionLevels.Length; i++ )
                    {
                        var level = projectionPoint + ( ( endPoint - startPoint ) * GlobalConstants.ABCWaveCProjectionLevels [ i ] / 100 );
                        var strength = GlobalConstants.ABCWaveCProjectionStrength[ i ];

                        output.Add( (level, strength) );
                    }
                }
                break;

                case FibonacciType.Wave2Retracement:
                {
                    for ( i = 0; i < GlobalConstants.Wave2RetracementLevels.Length; i++ )
                    {
                        var level = endPoint + ( ( startPoint - endPoint ) * GlobalConstants.Wave2RetracementLevels [ i ] / 100 ) ;
                        var strength = GlobalConstants.Wave2RetracementStrength[ i ];

                        output.Add( (level, strength) );
                    }
                }
                break;

                case FibonacciType.FirstXProjection:
                {
                    for ( i = 0; i < GlobalConstants.FirstXProjectionLevels.Length; i++ )
                    {
                        var level = projectionPoint + ((endPoint - startPoint) * GlobalConstants.FirstXProjectionLevels[i] / 100);
                        var strength = GlobalConstants.FirstXProjectionStrength[i];

                        output.Add( (level, strength) );
                    }
                }
                break;

                case FibonacciType.SecondXProjection:
                {
                    for ( i = 0; i < GlobalConstants.SecondXProjectionLevels.Length; i++ )
                    {
                        var level = projectionPoint + ((endPoint - startPoint) * GlobalConstants.SecondXProjectionLevels[i] / 100);
                        var strength = GlobalConstants.SecondXProjectionStrength[i];

                        output.Add( (level, strength) );
                    }
                }
                break;

                case FibonacciType.Wave3Projection:
                {
                    for ( i = 0; i < GlobalConstants.Wave3ProjectionLevels.Length; i++ )
                    {
                        var level = projectionPoint + ( ( endPoint - startPoint ) * GlobalConstants.Wave3ProjectionLevels [ i ] / 100 );
                        var strength = GlobalConstants.Wave3ProjectionStrength[ i ];

                        output.Add( (level, strength) );
                    }
                }
                break;

                case FibonacciType.Wave3CProjection:
                {
                    for ( i = 0; i < GlobalConstants.Wave3CProjectionLevels.Length; i++ )
                    {
                        var level = projectionPoint + ( ( endPoint - startPoint ) * GlobalConstants.Wave3CProjectionLevels [ i ] / 100 );
                        var strength = GlobalConstants.Wave3CProjectionStrength[ i ];

                        output.Add( (level, strength) );
                    }
                }
                break;

                case FibonacciType.Wave4Retracement:
                {
                    for ( i = 0; i < GlobalConstants.Wave4RetracementLevels.Length; i++ )
                    {
                        var level = endPoint + ( ( startPoint - endPoint ) * GlobalConstants.Wave4RetracementLevels [ i ] / 100 );
                        var strength = GlobalConstants.Wave4RetracementStrength[ i ];

                        output.Add( (level, strength) );
                    }
                }
                break;

                case FibonacciType.Wave5Projection:
                {
                    for ( i = 0; i < GlobalConstants.Wave5ProjectionLevels.Length; i++ )
                    {
                        var level = projectionPoint + ( ( endPoint - startPoint ) * GlobalConstants.Wave5ProjectionLevels [ i ] / 100 );
                        var strength = GlobalConstants.Wave5ProjectionStrength[ i ];


                        output.Add( (level, strength) );
                    }
                }
                break;

                case FibonacciType.Wave5CProjection:
                {
                    for ( i = 0; i < GlobalConstants.Wave5CProjectionLevels.Length; i++ )
                    {
                        var level = projectionPoint + ( ( endPoint - startPoint ) * GlobalConstants.Wave5CProjectionLevels [ i ] / 100 );
                        var strength = GlobalConstants.Wave5CProjectionStrength[ i ];


                        output.Add( (level, strength) );
                    }
                }
                break;

                case FibonacciType.ABCWaveCProjection:
                {
                    for ( i = 0; i < GlobalConstants.ABCWaveCProjectionLevels.Length; i++ )
                    {
                        var level = projectionPoint + ( ( endPoint - startPoint ) * GlobalConstants.ABCWaveCProjectionLevels [ i ] / 100 );
                        var strength = GlobalConstants.ABCWaveCProjectionStrength[ i ];


                        output.Add( (level, strength) );
                    }
                }
                break;

                case FibonacciType.ABCWaveBRetracement:
                {
                    for ( i = 0; i < GlobalConstants.ABCWaveBRetracementLevels.Length; i++ )
                    {
                        var level = endPoint + ( ( startPoint - endPoint ) * GlobalConstants.ABCWaveBRetracementLevels [ i ] / 100 ) ;
                        var strength = GlobalConstants.ABCWaveBRetracementStrength[ i ];


                        output.Add( (level, strength) );
                    }
                }
                break;

                case FibonacciType.WaveEFBRetracement:
                {
                    for ( i = 0; i < GlobalConstants.WaveEFBRetracementLevels.Length; i++ )
                    {
                        var level = endPoint + ( ( startPoint - endPoint ) * GlobalConstants.WaveEFBRetracementLevels [ i ] / 100 ) ;
                        var strength = GlobalConstants.WaveEFBRetracementStrength[ i ];


                        output.Add( (level, strength) );
                    }
                }
                break;

                case FibonacciType.WaveTriBRetracement:
                {
                    for ( i = 0; i < GlobalConstants.WaveTriBRetracementLevels.Length; i++ )
                    {
                        var level = endPoint + ( ( startPoint - endPoint ) * GlobalConstants.WaveTriBRetracementLevels [ i ] / 100 ) ;
                        var strength = GlobalConstants.WaveTriBRetracementStrength[ i ];


                        output.Add( (level, strength) );
                    }
                }
                break;

                case FibonacciType.WaveTriCProjection:
                {
                    for ( i = 0; i < GlobalConstants.WaveTriCRetracementLevels.Length; i++ )
                    {
                        var level = endPoint + ( ( startPoint - endPoint ) * GlobalConstants.WaveTriCRetracementLevels [ i ] / 100 ) ;
                        var strength = GlobalConstants.WaveTriCRetracementStrength[ i ];


                        output.Add( (level, strength) );
                    }
                }
                break;

                case FibonacciType.WaveTriDProjection:
                {
                    for ( i = 0; i < GlobalConstants.WaveTriDRetracementLevels.Length; i++ )
                    {
                        var level = endPoint + ( ( startPoint - endPoint ) * GlobalConstants.WaveTriDRetracementLevels [ i ] / 100 );
                        var strength = GlobalConstants.WaveTriDRetracementStrength[ i ];


                        output.Add( (level, strength) );
                    }
                }
                break;

                case FibonacciType.WaveTriEProjection:
                {
                    for ( i = 0; i < GlobalConstants.WaveTriERetracementLevels.Length; i++ )
                    {
                        var level = endPoint + ( ( startPoint - endPoint ) * GlobalConstants.WaveTriERetracementLevels [ i ] / 100 );
                        var strength = GlobalConstants.WaveTriERetracementStrength[ i ];


                        output.Add( (level, strength) );
                    }
                }
                break;

                case FibonacciType.TonyProjection:
                {
                    for ( i = 0; i < GlobalConstants.TonyDiscoveryLevels.Length; i++ )
                    {
                        var level = endPoint + ( ( startPoint - endPoint ) * GlobalConstants.TonyDiscoveryLevels [ i ] / 100 );
                        var strength = GlobalConstants.TonyDiscoveryLevelsStrength[ i ];


                        output.Add( (level, strength) );
                    }
                }
                break;

                case FibonacciType.TonyRetracement:
                {
                    for ( i = 0; i < GlobalConstants.Wave2RetracementLevels.Length; i++ )
                    {
                        var level = endPoint + ( ( startPoint - endPoint ) * GlobalConstants.Wave2RetracementLevels [ i ] / 100 ) ;
                        var strength = GlobalConstants.Wave2RetracementStrength[ i ];

                        output.Add( (level, strength) );
                    }
                }
                break;
            }

            return output;
        }


        
        public static PooledList<WaveSRLineResponse> GetSRLinesResponse( FibonacciType fibType, ref SBar bar, PooledList<FibLevelInfo> srLines, TrendDirection direction )
        {
            PooledList< WaveSRLineResponse > output = new PooledList< WaveSRLineResponse >( );

            for ( int i = 0; i < srLines.Count; i++ )
            {
                var fibLevelInfo = srLines[ i ];
                var response = GetBarReactionAtSRLine( ref bar, fibLevelInfo.FibLevel, direction );

                if ( response != SRLineResponseType.NoRelationShip )
                {
                    var responseObj = new WaveSRLineResponse( direction, fibType, response, fibLevelInfo );

                    output.Add( responseObj );
                }
            }

            return output;
        }

        public static bool GetClosestSRLine( FibonacciType fibType, ref SBar bar, PooledList<FibLevelInfo> srLines, TrendDirection direction, out WaveSRLineResponse output )
        {
            output = default;

            var responses = GetSRLinesResponse( fibType, ref bar, srLines, direction );

            if ( responses.Count > 0 )
            {
                var minDiff = double.MaxValue;

                double checkingValue = -1;

                if ( direction == TrendDirection.Uptrend )
                {
                    checkingValue = bar.High;
                }
                else if ( direction == TrendDirection.DownTrend )
                {
                    checkingValue = bar.Low;
                }

                foreach ( var response in responses )
                {
                    var diff = Math.Abs( response.SRLineValue - checkingValue );

                    if ( diff < minDiff )
                    {
                        minDiff = diff;

                        output = response;
                    }
                }

                return true;
            }
            

            return false;
        }


        public static bool isRetracement( this FibonacciType fibType )
        {
            if ( fibType == FibonacciType.Wave2Retracement || 
                 fibType == FibonacciType.Wave4Retracement ||
                 fibType == FibonacciType.WaveTriBRetracement ||
                 fibType == FibonacciType.ABCWaveBRetracement ||
                 fibType == FibonacciType.WaveEFBRetracement )
            {
                return true;
            }

            return false;
        }

        public static bool isProjection( this FibonacciType fibType )
        {
            if ( fibType == FibonacciType.WaveCProjection ||
                 fibType == FibonacciType.Wave3Projection ||
                 fibType == FibonacciType.Wave3CProjection ||
                 fibType == FibonacciType.Wave5Projection ||
                 fibType == FibonacciType.Wave5CProjection ||
                 fibType == FibonacciType.ABCWaveCProjection ||
                 fibType == FibonacciType.WaveTriCProjection ||
                 fibType == FibonacciType.WaveTriDProjection ||
                 fibType == FibonacciType.WaveTriEProjection ||
                 fibType == FibonacciType.FirstXProjection ||
                 fibType == FibonacciType.SecondXProjection )                
            {
                return true;
            }

            return false;
        }


        public static SRLineResponseType GetBarReactionAtSRLine( ref SBar bar, float srLine, TrendDirection direction )
        {
            var candleRange = bar.WholeCandle;

            if ( direction == TrendDirection.Uptrend )
            {
                if ( candleRange.Contains( srLine ) || candleRange.WithUpperRange( srLine ) )
                {
                    var upperShadow = bar.UpperShadow;

                    if ( upperShadow.ExactTouch( srLine ) )
                    {
                        return SRLineResponseType.ExactTouch;
                    }
                    else if ( upperShadow.AlmostTouch( srLine ) )
                    {
                        return SRLineResponseType.AlmostTouch;                        
                    }
                    else if ( upperShadow.Contains( srLine ) )
                    {
                        return SRLineResponseType.PenetratedAndClosedBelow;                        
                    }
                    else
                    {
                        var realBody = bar.RealBody;

                        if ( realBody.HighestPointExactTouch( srLine ) )
                        {
                            return SRLineResponseType.ExactTouch;                            
                        }
                        else if ( realBody.HighestPointAlmostTouch( srLine ) )
                        {
                            return SRLineResponseType.AlmostTouch;
                        }
                        else if ( ( bar.Open < srLine ) && ( bar.Close > srLine ) )
                        {
                            return SRLineResponseType.BrokenAndClosedAbove;                            
                        }
                        else if ( ( bar.Open > srLine ) && ( bar.Close < srLine ) )
                        {
                            return SRLineResponseType.TestedAndFailedBelow;                            
                        }
                    }
                }
            }
            else if ( direction == TrendDirection.DownTrend )
            {
                if ( candleRange.Contains( srLine ) || candleRange.WithLowerRange( srLine ) )
                {
                    var lowerShadow = bar.LowerShadow;

                    if ( lowerShadow.ExactTouch( srLine ) )
                    {
                        return SRLineResponseType.ExactTouch;
                    }
                    else if ( lowerShadow.AlmostTouch( srLine ) )
                    {
                        return SRLineResponseType.AlmostTouch;
                    }
                    else if ( lowerShadow.Contains( srLine ) )
                    {
                        return SRLineResponseType.PenetratedAndClosedAbove;
                    }
                    else
                    {
                        var realBody = bar.RealBody;

                        if ( realBody.LowestPointExactTouch( srLine ) )
                        {
                            return SRLineResponseType.ExactTouch;
                        }
                        else if ( realBody.LowestPointAlmostTouch( srLine ) )
                        {
                            return SRLineResponseType.AlmostTouch;
                        }
                        else if ( ( bar.Open > srLine ) && ( bar.Close < srLine ) )
                        {
                            return SRLineResponseType.BrokenAndClosedBelow;
                        }
                        else if ( ( bar.Open < srLine ) && ( bar.Close > srLine ) )
                        {
                            return SRLineResponseType.BrokenAndRecoverAbove;
                        }
                    }
                }
            }

            return SRLineResponseType.NoRelationShip;
        }

        public static bool IsNextWaveOneDegreeLowerBeginning( this ElliottWaveEnum lastWave, ElliottWaveEnum currentWave )
        {
            if ( lastWave == ElliottWaveEnum.NONE || currentWave == ElliottWaveEnum.NONE )
            {
                return false;
            }

            if ( currentWave != ElliottWaveEnum.WaveA && currentWave != ElliottWaveEnum.Wave1 && currentWave != ElliottWaveEnum.WaveEFA && currentWave != ElliottWaveEnum.WaveTA )
            {
                return false;
            }

            switch ( lastWave )
            {
                /* ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *              = C, that means we are done with an implusive move and starting a corrective wave of lower degree
                 *                   that means we are We are done with a corrective wave and starting a corrective wave of lower degree
                 *              = 1
                 *              = 1C, that means we are beginning Wave 2
                 *              = 3,
                 *              = 3C, that means we are beginning Wave 4
                 *              = 5，
                 *              = 5C，Whether it is going to be impulsive or corrective, it all begins with Wave a 
                 * 
                 * ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                 */


                case ElliottWaveEnum.WaveC:
                case ElliottWaveEnum.Wave1:
                case ElliottWaveEnum.Wave1C:
                case ElliottWaveEnum.Wave3:
                case ElliottWaveEnum.Wave3C:
                case ElliottWaveEnum.Wave5:
                case ElliottWaveEnum.Wave5C:
                case ElliottWaveEnum.WaveTA:
                case ElliottWaveEnum.WaveTB:
                case ElliottWaveEnum.WaveTC:
                case ElliottWaveEnum.WaveTD:
                case ElliottWaveEnum.WaveTE:
                case ElliottWaveEnum.WaveEFA:
                case ElliottWaveEnum.WaveEFC:

                    {
                        if ( currentWave == ElliottWaveEnum.WaveA )
                        {
                            return true;
                        }
                    }
                    break;

                /* ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 * last Wave    = A, that means we are beginning Wave B, which is corrective wave, so the wave of lower degree is Wave A or EFA
                 * 
                 * ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                 */
                case ElliottWaveEnum.WaveA:
                    {
                        if ( currentWave == ElliottWaveEnum.WaveA || currentWave == ElliottWaveEnum.WaveEFA )
                        {
                            return true;
                        }
                    }
                    break;


                case ElliottWaveEnum.Wave2:
                case ElliottWaveEnum.Wave4:
                case ElliottWaveEnum.WaveX:
                case ElliottWaveEnum.WaveB:
                case ElliottWaveEnum.WaveEFB:
                {
                    if ( currentWave == ElliottWaveEnum.Wave1 )
                    {
                        return true;
                    }
                }
                break;

                /* ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 * last Wave    = W, that means we are beginning Wave X, which is corrective wave, so the wave of lower degree is Wave A 
                 * 
                 * ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                 */
                case ElliottWaveEnum.WaveY:
                case ElliottWaveEnum.WaveW:
                case ElliottWaveEnum.WaveZ:
                {
                    if ( currentWave == ElliottWaveEnum.WaveA )
                    {
                        return true;
                    }
                }
                break;

                default:
                    {
                        throw new NotImplementedException( );
                    }
            }

            return false;
        }

        public static ElliottWaveEnum GetNextWave( ElliottWaveEnum previousWave )
        {
            ElliottWaveEnum output = ElliottWaveEnum.NONE;

            switch ( previousWave )
            {
                // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                //
                // When the last Wave is 1 or 1C, that means we are beginning Wave 2, which is corrective wave, the lower Wave A, B, C will be of lower degree, so the only next wave is wave 2
                //
                // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                case ElliottWaveEnum.Wave1:
                case ElliottWaveEnum.Wave1C:
                    output = ElliottWaveEnum.Wave2;
                    break;

                // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                //
                // When the last Wave is 2, that means we are beginning Wave 3, which is impulsive wave, the A, B, C waves will be of the same degree
                //
                // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                case ElliottWaveEnum.Wave2:
                    output = ElliottWaveEnum.Wave3;
                    break;

                // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                //
                // When the last Wave is 3 or 3C, that means we are beginning Wave 4, which is corrective wave, the lower Wave A, B, C will be of lower degree, so the only next wave is wave 4
                //
                // -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                case ElliottWaveEnum.Wave3:
                case ElliottWaveEnum.Wave3C:
                    output = ElliottWaveEnum.Wave4;
                    break;

                // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                //
                // When the last Wave is 4, that means we are beginning Wave 5, which is impulsive wave, the A, B, C waves will be of the same degree
                //
                // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                case ElliottWaveEnum.Wave4:
                    output = ElliottWaveEnum.Wave5;
                    break;

                case ElliottWaveEnum.Wave5:
                case ElliottWaveEnum.Wave5C:
                    output = ElliottWaveEnum.WaveA;
                    break;

                case ElliottWaveEnum.WaveA:
                    output = ElliottWaveEnum.WaveB;
                    break;

                case ElliottWaveEnum.WaveB:
                    output = ElliottWaveEnum.WaveC;
                    break;

                case ElliottWaveEnum.WaveC:
                    output = ElliottWaveEnum.NONE;
                    break;

                case ElliottWaveEnum.WaveX:
                    output = ElliottWaveEnum.WaveA;
                    break;

                case ElliottWaveEnum.WaveW:
                    output = ElliottWaveEnum.WaveX;
                    break;

                case ElliottWaveEnum.WaveY:
                    output = ElliottWaveEnum.WaveX;
                    break;
                
                case ElliottWaveEnum.WaveZ:
                    output = ElliottWaveEnum.NONE;
                    break;


                case ElliottWaveEnum.WaveEFA:
                    output = ElliottWaveEnum.WaveEFB;
                    break;

                case ElliottWaveEnum.WaveEFB:
                    output = ElliottWaveEnum.WaveEFC;
                    break;

                default:
                    throw new NotImplementedException( );
            }

            return output;
        }

        public static FibonacciType GetProjectionTypeOfWaveB( this ElliottWaveEnum containingWave )
        {
            FibonacciType output = FibonacciType.NONE;
            switch ( containingWave )
            {                
                case ElliottWaveEnum.Wave1:
                    output = FibonacciType.WaveCProjection;
                    break;
                
                case ElliottWaveEnum.Wave3:
                    output = FibonacciType.Wave3CProjection;
                    break;
                
                case ElliottWaveEnum.Wave5:
                    output = FibonacciType.Wave5CProjection;
                    break;
                
                case ElliottWaveEnum.WaveTA:                    
                case ElliottWaveEnum.WaveTB:
                case ElliottWaveEnum.WaveTC:
                case ElliottWaveEnum.WaveTD:
                case ElliottWaveEnum.WaveTE:
                    output = FibonacciType.WaveCProjection;
                    break;

                case ElliottWaveEnum.WaveEFA:                    
                case ElliottWaveEnum.WaveEFB:
                    output = FibonacciType.WaveCProjection;
                    break;
                
                case ElliottWaveEnum.WaveA:
                case ElliottWaveEnum.WaveB:                
                    output = FibonacciType.WaveCProjection;
                    break;
                case ElliottWaveEnum.WaveX:
                case ElliottWaveEnum.WaveY:
                case ElliottWaveEnum.WaveZ:
                    output = FibonacciType.FirstXProjection;
                    break;
                
            }

            return output;
        }


        

        public static bool IsNextWave( this ElliottWaveEnum previousWave, ElliottWaveEnum nextWave )
        {
            if ( previousWave == ElliottWaveEnum.NONE || nextWave == ElliottWaveEnum.NONE )
            {
                return false;
            }


            switch ( previousWave )
            {
                // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                //
                // When the last Wave is 1 or 1C, that means we are beginning Wave 2, which is corrective wave, the lower Wave A, B, C will be of lower degree, so the only next wave is wave 2
                //
                // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                case ElliottWaveEnum.Wave1:
                case ElliottWaveEnum.Wave1C:
                {
                    if ( nextWave == ElliottWaveEnum.Wave2 )
                    {
                        return true;
                    }
                }
                break;

                // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                //
                // When the last Wave is 2, that means we are beginning Wave 3, which is impulsive wave, the A, B, C waves will be of the same degree
                //
                // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                case ElliottWaveEnum.Wave2:
                {
                    if ( nextWave == ElliottWaveEnum.WaveA || nextWave == ElliottWaveEnum.Wave3 || nextWave == ElliottWaveEnum.Wave3C )
                    {
                        return true;
                    }
                }
                break;

                // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                //
                // When the last Wave is 3 or 3C, that means we are beginning Wave 4, which is corrective wave, the lower Wave A, B, C will be of lower degree, so the only next wave is wave 4
                //
                // -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                case ElliottWaveEnum.Wave3:
                case ElliottWaveEnum.Wave3C:
                {
                    if ( nextWave == ElliottWaveEnum.Wave4 )
                    {
                        return true;
                    }
                }
                break;

                // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                //
                // When the last Wave is 4, that means we are beginning Wave 5, which is impulsive wave, the A, B, C waves will be of the same degree
                //
                // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                case ElliottWaveEnum.Wave4:
                {
                    if ( nextWave == ElliottWaveEnum.WaveA || nextWave == ElliottWaveEnum.Wave5 || nextWave == ElliottWaveEnum.Wave5C )
                    {
                        return true;
                    }
                }
                break;

                case ElliottWaveEnum.Wave5:
                case ElliottWaveEnum.Wave5C:
                {
                    if ( nextWave == ElliottWaveEnum.WaveA || nextWave == ElliottWaveEnum.Wave1 || nextWave == ElliottWaveEnum.Wave1C )
                    {
                        return true;
                    }
                } 
                break;

                case ElliottWaveEnum.WaveA:
                {
                    if ( nextWave == ElliottWaveEnum.WaveB )
                    {
                        return true;
                    }
                }
                break;

                case ElliottWaveEnum.WaveB:
                {
                    if ( nextWave == ElliottWaveEnum.WaveC || nextWave == ElliottWaveEnum.Wave1C || nextWave == ElliottWaveEnum.Wave3C || nextWave == ElliottWaveEnum.Wave5C )
                    {
                        return true;
                    }
                }
                break;

                case ElliottWaveEnum.WaveC:
                {
                    if ( ( nextWave == ElliottWaveEnum.Wave1 ) || ( nextWave == ElliottWaveEnum.WaveX ) || ( nextWave == ElliottWaveEnum.WaveA ) )
                    {
                        return true;
                    }
                }
                break;

                case ElliottWaveEnum.WaveX:
                {
                    if ( ( nextWave == ElliottWaveEnum.WaveA ) || ( nextWave == ElliottWaveEnum.WaveY ) || ( nextWave == ElliottWaveEnum.WaveZ ) )
                    {
                        return true;
                    }
                }
                break;

                case ElliottWaveEnum.WaveW:
                case ElliottWaveEnum.WaveY:
                {
                    if ( nextWave == ElliottWaveEnum.WaveX )
                    {
                        return true;
                    }
                }
                break;
                
                case ElliottWaveEnum.WaveZ:
                {
                    if ( nextWave == ElliottWaveEnum.Wave1 )
                    {
                        return true;
                    }
                }
                break;

                case ElliottWaveEnum.WaveTE:
                    if ( nextWave == ElliottWaveEnum.WaveA )
                    {
                        return true;
                    }
                    break;

                case ElliottWaveEnum.WaveTD:
                    if ( nextWave == ElliottWaveEnum.WaveTE )
                    {
                        return true;
                    }
                    break;

                case ElliottWaveEnum.WaveTC:
                    if ( nextWave == ElliottWaveEnum.WaveTD )
                    {
                        return true;
                    }
                    break;

                case ElliottWaveEnum.WaveTB:
                    if ( nextWave == ElliottWaveEnum.WaveTC )
                    {
                        return true;
                    }
                    break;

                case ElliottWaveEnum.WaveTA:
                    if ( nextWave == ElliottWaveEnum.WaveTB )
                    {
                        return true;
                    }
                    break;

                case ElliottWaveEnum.WaveEFA:
                    if ( nextWave == ElliottWaveEnum.WaveEFB )
                    {
                        return true;
                    }
                    break;

                case ElliottWaveEnum.WaveEFB:
                    if ( nextWave == ElliottWaveEnum.WaveEFC )
                    {
                        return true;
                    }
                    break;

                case ElliottWaveEnum.WaveEFC:
                    if (nextWave == ElliottWaveEnum.WaveA)
                    {
                        return true;
                    }
                    break;
                    

                default:
                    throw new NotImplementedException( );
            }

            return false;
        }

        public static bool IsValidLowerDegreeWave( this ElliottWaveEnum higherDegreeWave, ElliottWaveEnum lowerDegreeWave )
        {
            if ( higherDegreeWave == ElliottWaveEnum.NONE || lowerDegreeWave == ElliottWaveEnum.NONE )
            {
                return false;
            }

            switch ( higherDegreeWave )
            {
                // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                //
                // When the higer degress Wave is A, that will mean the lower degree wave is at Wave 5 or Wave 5c
                //
                // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                case ElliottWaveEnum.WaveA:
                case ElliottWaveEnum.WaveC:
                case ElliottWaveEnum.Wave1:
                case ElliottWaveEnum.Wave1C:
                case ElliottWaveEnum.Wave3:
                case ElliottWaveEnum.Wave3C:
                case ElliottWaveEnum.Wave5:
                case ElliottWaveEnum.Wave5C:
                    {
                        if ( lowerDegreeWave == ElliottWaveEnum.Wave5 || lowerDegreeWave == ElliottWaveEnum.Wave5C )
                        {
                            return true;
                        }
                    }
                    break;

                case ElliottWaveEnum.WaveEFA:
                case ElliottWaveEnum.WaveEFB:
                case ElliottWaveEnum.WaveEFC:
                case ElliottWaveEnum.WaveTA:
                case ElliottWaveEnum.WaveTB:
                case ElliottWaveEnum.WaveTC:
                case ElliottWaveEnum.WaveTD:
                case ElliottWaveEnum.WaveTE:
                case ElliottWaveEnum.Wave2:
                    {
                        if ( ( lowerDegreeWave == ElliottWaveEnum.WaveC ) || ( lowerDegreeWave == ElliottWaveEnum.WaveEFC ) || ( lowerDegreeWave == ElliottWaveEnum.WaveTE ) )
                        {
                            return true;
                        }
                    }
                    break;

                // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                //
                // When the last Wave is B, that will mean the lower degree wave is at WaveC
                //
                // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                case ElliottWaveEnum.WaveB:
                case ElliottWaveEnum.Wave4:
                case ElliottWaveEnum.WaveX:
                case ElliottWaveEnum.WaveY:
                case ElliottWaveEnum.WaveZ:
                {
                    if ( lowerDegreeWave == ElliottWaveEnum.WaveC || lowerDegreeWave == ElliottWaveEnum.WaveTE || lowerDegreeWave == ElliottWaveEnum.WaveEFC || lowerDegreeWave == ElliottWaveEnum.WaveY || lowerDegreeWave == ElliottWaveEnum.WaveZ )
                    {
                        return true;
                    }
                }
                break;





                default:
                    {
                        throw new NotImplementedException( );
                    }
            }

            return false;
        }


        public static string PositionIndicator( this StructureLabelEnum labelEnum )
        {
            string output = "";

            if ( labelEnum.HasFlag( StructureLabelEnum.F3 ) )
            {
                output += ":F3";
            }

            if ( labelEnum.HasFlag( StructureLabelEnum.c3 ) )
            {
                if( !string.IsNullOrEmpty( output ) )
                    output += "/";

                output += ":c3";
            }

            if ( labelEnum.HasFlag( StructureLabelEnum.x_c3 ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += "/";

                output += "x:c3";
            }

            if ( labelEnum.HasFlag( StructureLabelEnum.sL3 ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += "/";

                output += ":sL3";
            }

            if ( labelEnum.HasFlag( StructureLabelEnum.L3 ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += "/";

                output += ":L3";
            }

            if ( labelEnum.HasFlag( StructureLabelEnum._5 ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += "/";

                output += ":5";
            }

            if ( labelEnum.HasFlag( StructureLabelEnum.s5 ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += "/";

                output += ":s5";
            }


            if ( labelEnum.HasFlag( StructureLabelEnum.L5 ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += "/";

                output += ":L5";
            }

            return output;
        }

        public static bool IsNextWaveTwoDegreesLowerBeginning( this ElliottWaveEnum lastWave, ElliottWaveEnum currentWave )
        {
            if ( lastWave == ElliottWaveEnum.NONE || currentWave == ElliottWaveEnum.NONE )
            {
                return false;
            }

            if ( currentWave != ElliottWaveEnum.WaveA && currentWave != ElliottWaveEnum.Wave1 )
            {
                return false;
            }

            switch ( lastWave )
            {
                /* ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 * last Wave    = A, that means we are beginning Wave B, which is corrective wave, so the wave of lower degree is Wave A
                 *              = C, that means we are done with an implusive move and starting a corrective wave of lower degree
                 *                   that means we are We are done with a corrective wave and starting a corrective wave of lower degree
                 *              = 1
                 *              = 1C, that means we are beginning Wave 2
                 *              = 3,
                 *              = 3C, that means we are beginning Wave 4
                 *              = 5，
                 *              = 5C，Whether it is going to be impulsive or corrective, it all begins with Wave a 
                 * 
                 * ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                 */

                case ElliottWaveEnum.WaveA:
                case ElliottWaveEnum.WaveC:
                case ElliottWaveEnum.Wave1:
                case ElliottWaveEnum.Wave1C:
                case ElliottWaveEnum.Wave3:
                case ElliottWaveEnum.Wave3C:
                case ElliottWaveEnum.Wave5:
                case ElliottWaveEnum.Wave5C:
                case ElliottWaveEnum.WaveTA:
                case ElliottWaveEnum.WaveTB:
                case ElliottWaveEnum.WaveTC:
                case ElliottWaveEnum.WaveTD:
                case ElliottWaveEnum.WaveTE:
                case ElliottWaveEnum.WaveEFA:
                case ElliottWaveEnum.WaveEFB:
                    {
                        if ( currentWave == ElliottWaveEnum.Wave1 )
                        {
                            return true;
                        }
                    }
                    break;

                /* ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 * last Wave    = EFC, that means this is the end of a higher degree Wave B or Wave 2
                 * 
                 *         B
                 *        EFC
                 *        
                 *  So in a way, we are beginning Wave c, which should be 1,2,3,4,5 ( this is the same degree as the EFC wave )
                 *  So for Wave 1, Which is Wave ABC ( this will be one degree lower than EFC )
                 *  Subdivide into lower degree ( 2 degrees lower ) for WAVE A ( which is 1,2,3,4,5 ) again.
                 * ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                 */
                case ElliottWaveEnum.WaveEFC:
                    {
                        if ( currentWave == ElliottWaveEnum.Wave1 )
                        {
                            return true;
                        }
                    }
                    break;


                /* ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 * last Wave    = 2, that means we are beginning Wave A of Wave 3, Which is an impulsive move ( 1 - 2 - 3 - 4 - 5 )
                 *              =       so for wave 1 ( which will be ABC ) for the second degree.
                 *              
                 *              = X, that means we are in the first part double Three or Triple Three
                 *              = Y, if all depends on where we are in, if we are in double three, Y will be the end and if we are in the triple three, we will be expecting wave X
                 * 
                 * ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                 */
                case ElliottWaveEnum.Wave2:
                case ElliottWaveEnum.Wave4:
                case ElliottWaveEnum.WaveX:                
                case ElliottWaveEnum.WaveB:
                {
                    if ( currentWave == ElliottWaveEnum.WaveA )
                    {
                        return true;
                    }
                }
                break;

                /* ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 * last Wave    = W, next wave will be wave X which should be (ABC）so lower degree for Wave A is 1-2-3-4-5 or A-B-C
                 * 
                 * ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                 */
                case ElliottWaveEnum.WaveW:
                {
                    if ( currentWave == ElliottWaveEnum.WaveA )
                    {
                        return true;
                    }
                }
                break;

                /* ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 * last Wave    = Y, if all depends on where we are in, if we are in double three, Y will be the end and if we are in the triple three, we will be expecting wave X
                 *                   1) If double three, we will be resuming the main trend
                 *              
                 * ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                 */
                case ElliottWaveEnum.WaveY:
                {
                    // If we are expecting X, which is ABC, so lower degree for Wave A is 1-2-3-4-5 or A-B-C
                    if ( ( currentWave == ElliottWaveEnum.WaveA ) || ( currentWave == ElliottWaveEnum.Wave1 ) )
                    {
                        return true;
                    }

                    // If we are expecting resuming of the trend, which should be an impulsive move, so we will have 1-2-3-4-5 and second degree of wave 1 is ABC
                    // The following code will not be executed, I am only putting code here to clarify the thought process.
                    if ( currentWave == ElliottWaveEnum.WaveA )
                    {
                        return true;
                    }
                }
                break;

                case ElliottWaveEnum.WaveZ:
                {
                    // After ending of Wave Z, trend will resume. Since Wave Z should have a higher degree Wave, we should not get here.
                    // The following code will not be executed, I am only putting code here to clarify the thought process.
                    if ( currentWave == ElliottWaveEnum.WaveA )
                    {
                        return true;
                    }
                }
                break;

                default:
                {
                    throw new NotImplementedException( );
                }
            }

            return false;
        }


        public static bool IsOneDegreeLowerEndingWrt( this ElliottWaveEnum firstWave, ElliottWaveEnum secondWave )
        {
            if ( secondWave == ElliottWaveEnum.NONE || firstWave == ElliottWaveEnum.NONE )
            {
                return false;
            }

            if ( firstWave != ElliottWaveEnum.WaveB && firstWave != ElliottWaveEnum.Wave4 )
            {
                return false;
            }

            switch ( secondWave )
            {
                /* ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 * second Wave  = A or C, that means we should see 1-2-3-4, or 1-2-3-4-a-b-5 of one degree lower
                 *                        (A)              (c)
                 *                1-2-3-4- 5        A - B - C
                 * ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                 */

                case ElliottWaveEnum.WaveA:
                case ElliottWaveEnum.WaveC:
                    if ( firstWave == ElliottWaveEnum.Wave4 || firstWave == ElliottWaveEnum.WaveB )
                    {
                        return true;
                    }
                    break;

                /* ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 * second Wave  =   1, 1C, 3, 3C, 5, 5C
                 * 
                 *                  For impulsive wave, AB1C, AB3C, AB5C, all those waves belong to the same degree
                 *                  So if it is one degree lower, that will be ONE DEGREE LOWER wave 4 inside the same Degree Wave C
                 * 
                 * ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                 */
                case ElliottWaveEnum.Wave1:
                case ElliottWaveEnum.Wave1C:
                case ElliottWaveEnum.Wave3:
                case ElliottWaveEnum.Wave3C:
                case ElliottWaveEnum.Wave5:
                case ElliottWaveEnum.Wave5C:
                {
                    if ( firstWave == ElliottWaveEnum.Wave4 )
                    {
                        return true;
                    }
                }
                break;


                /* ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 * second Wave  = 2,4,X,B
                 * 
                 *                  For Corrective wave, AB1C, AB3C, AB5C, all those waves belong to the same degree
                 *                  So if it is one degree lower, that will be ONE DEGREE LOWER wave 4 inside the same Degree Wave C
                 * 
                 * 
                 * * ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                 */
                
                case ElliottWaveEnum.Wave2:
                case ElliottWaveEnum.Wave4:
                case ElliottWaveEnum.WaveB:
                {
                    if ( firstWave == ElliottWaveEnum.WaveB )
                    {
                        return true;
                    }
                }
                break;

                /* ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                * 
                * second Wave  = A or C, that means we should see 1-2-3-4, or 1-2-3-4-a-b-5 of one degree lower
                *                        (W)              (X)           (Y)
                *                 A - B - C        A - B - C     A - B - C
                * ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                */
                case ElliottWaveEnum.WaveW:
                case ElliottWaveEnum.WaveX:
                case ElliottWaveEnum.WaveY:
                case ElliottWaveEnum.WaveZ:
                {
                    if ( firstWave == ElliottWaveEnum.WaveB )
                    {
                        return true;
                    }
                }
                break;


                case ElliottWaveEnum.WaveEFA:
                {
                    if ( ( firstWave == ElliottWaveEnum.WaveA ) || ( firstWave == ElliottWaveEnum.Wave1 ) || ( firstWave == ElliottWaveEnum.Wave3 ) || ( firstWave == ElliottWaveEnum.Wave1C ) || ( firstWave == ElliottWaveEnum.Wave3C ) )
                    {
                        return true;
                    }
                }
                break;

                default:
                {
                    throw new NotImplementedException( );
                }
            }
            return false;
        }

        public static ElliottWaveEnum GetLowerDegreeWaveName( ElliottWaveEnum higherDegreeWave )
        {
            switch ( higherDegreeWave )
            {
                // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                //
                // When the higer degress Wave is A, that will mean the lower degree wave is at Wave 5 or Wave 5c
                //
                // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                case ElliottWaveEnum.WaveA:
                case ElliottWaveEnum.WaveC:
                case ElliottWaveEnum.Wave1:
                case ElliottWaveEnum.Wave1C:
                case ElliottWaveEnum.Wave3:
                case ElliottWaveEnum.Wave3C:
                case ElliottWaveEnum.Wave5:
                case ElliottWaveEnum.Wave5C:
                {
                    return ElliottWaveEnum.Wave5C;
                }


                // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                //
                // When the last Wave is B, that will mean the lower degree wave is at WaveC
                //
                // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                case ElliottWaveEnum.WaveB:
                case ElliottWaveEnum.Wave2:
                case ElliottWaveEnum.Wave4:
                case ElliottWaveEnum.WaveW:
                case ElliottWaveEnum.WaveX:
                case ElliottWaveEnum.WaveY:
                case ElliottWaveEnum.WaveZ:
                {
                    return ElliottWaveEnum.WaveC;
                }

                default:
                {
                    throw new NotImplementedException( );
                }
            }
        }

        public static bool IsValidWaves( ElliottWaveEnum startWaveName, ElliottWaveEnum endWaveName, ElliottWaveEnum? projectionWave )
        {
            if ( projectionWave.HasValue )
            {
                switch ( projectionWave ) // We we are doing projection, the only possiblities are.
                {
                    case ElliottWaveEnum.Wave2: // i) Projection of Wave 1 length from the base of Wave 2
                        {
                            if ( endWaveName == ElliottWaveEnum.Wave1 )
                                return true;
                        }
                        break;

                    case ElliottWaveEnum.Wave4: // ii) Projection of Wave 1-3 length from the base of Wave 4
                        {
                            if ( endWaveName == ElliottWaveEnum.Wave3 )
                                return true;
                        }
                        break;

                    case ElliottWaveEnum.WaveB: // iii) Projection of Wave A length from the base of Wave B
                        {
                            if ( endWaveName == ElliottWaveEnum.WaveA )
                                return true;
                        }
                        break;
                }
            }
            else
            {
                switch ( endWaveName )
                {
                    case ElliottWaveEnum.Wave1: // i) Projection of Wave 1 length from the base of Wave 2
                    case ElliottWaveEnum.Wave1C: // i) Projection of Wave 1 length from the base of Wave 2
                        {
                            if ( startWaveName == ElliottWaveEnum.Wave5 )
                                return true;
                        }
                        break;

                    case ElliottWaveEnum.Wave3: // ii) Projection of Wave 1-3 length from the base of Wave 4
                    case ElliottWaveEnum.Wave3C: // ii) Projection of Wave 1-3 length from the base of Wave 4
                        {
                            if ( startWaveName == ElliottWaveEnum.Wave2 )
                                return true;
                        }
                        break;

                    case ElliottWaveEnum.Wave5: // iii) Projection of Wave A length from the base of Wave B
                    case ElliottWaveEnum.Wave5C: // iii) Projection of Wave A length from the base of Wave B
                        {
                            if ( startWaveName == ElliottWaveEnum.Wave5 || startWaveName == ElliottWaveEnum.WaveC )
                                return true;
                        }
                        break;

                    case ElliottWaveEnum.WaveA:
                        {
                            return true;
                        }
                }
            }


            return false;
        }

        
        

        public static bool AnyKindOfWaveC( ElliottWaveEnum waveName )
        {
            if ( waveName == ElliottWaveEnum.Wave1C || waveName == ElliottWaveEnum.Wave3C || waveName == ElliottWaveEnum.Wave5C || waveName == ElliottWaveEnum.WaveC )
                return true;

            return false;
        }

        public static bool IsInBetweenWave( ElliottWaveEnum beginWave, ElliottWaveEnum currentWave, ElliottWaveEnum endWave )
        {
            if ( ( beginWave >= ElliottWaveEnum.Wave1 ) && ( beginWave <= ElliottWaveEnum.Wave5C ) && ( endWave >= ElliottWaveEnum.Wave1 ) && ( endWave <= ElliottWaveEnum.Wave5C ) )
            {
                if ( ( ( currentWave > beginWave ) && ( currentWave < endWave ) ) || ( currentWave == ElliottWaveEnum.WaveA ) || ( currentWave == ElliottWaveEnum.WaveB ) || ( currentWave == ElliottWaveEnum.WaveC ) )
                {
                    return true;
                }
            }

            if ( ( beginWave == ElliottWaveEnum.WaveA ) && ( currentWave == ElliottWaveEnum.WaveB )   )
            {
                if ( AnyKindOfWaveC( endWave ) )
                {
                    return true;
                }
                
            }

            if ( ( beginWave == ElliottWaveEnum.WaveW ) && ( currentWave == ElliottWaveEnum.WaveB ) )
            {
                if ( AnyKindOfWaveC( endWave ) )
                {
                    return true;
                }

            }

            if ( beginWave == ElliottWaveEnum.WaveB ) 
            {                
                if ( currentWave == ElliottWaveEnum.WaveC )
                {
                    if ( endWave == ElliottWaveEnum.WaveX  )
                    {
                        return true;
                    }

                    if ( endWave == ElliottWaveEnum.Wave2 )
                    {
                        return true;
                    }

                    if ( endWave == ElliottWaveEnum.Wave4 )
                    {
                        return true;
                    }
                }

                if ( currentWave == ElliottWaveEnum.Wave1C || currentWave == ElliottWaveEnum.Wave1 )
                {                    
                    if ( endWave == ElliottWaveEnum.Wave2 )
                    {
                        return true;
                    }                    
                }

                if ( currentWave == ElliottWaveEnum.Wave3C || currentWave == ElliottWaveEnum.Wave3 )
                {
                    if ( endWave == ElliottWaveEnum.Wave4 )
                    {
                        return true;
                    }
                }                
            }

            if ( ( beginWave == ElliottWaveEnum.WaveW ) && ( currentWave == ElliottWaveEnum.WaveX ) && ( endWave == ElliottWaveEnum.WaveY ) )
            {
                return true;
            }

            if ( ( beginWave == ElliottWaveEnum.WaveY ) && ( currentWave == ElliottWaveEnum.WaveX ) && ( endWave == ElliottWaveEnum.WaveZ ) )
            {
                return true;
            }

            if ( ( beginWave == ElliottWaveEnum.WaveC ) && ( currentWave == ElliottWaveEnum.WaveX ) && ( endWave == ElliottWaveEnum.WaveA )  )
            {
                return true;
            }

            if ( ( beginWave == ElliottWaveEnum.WaveTE ) )
            {
                if ( endWave == ElliottWaveEnum.WaveC || endWave == ElliottWaveEnum.Wave1C )
                {
                    if ( ( currentWave == ElliottWaveEnum.WaveA ) || ( currentWave == ElliottWaveEnum.WaveB ) || ( currentWave == ElliottWaveEnum.WaveC ) )
                    {
                        return true;
                    }
                }

            }

            if ( ( beginWave >= ElliottWaveEnum.WaveTA ) && ( endWave <= ElliottWaveEnum.WaveTE ) )
            {
                if ( ( currentWave >= ElliottWaveEnum.WaveTA ) && ( currentWave <= ElliottWaveEnum.WaveTE ) )
                {
                    return true;
                }
            }

            if ( ( beginWave == ElliottWaveEnum.WaveEFA ) && ( endWave == ElliottWaveEnum.WaveEFC ) && ( currentWave == ElliottWaveEnum.WaveEFB ) )
            {
                return true;
            }

            if ( ( beginWave == ElliottWaveEnum.WaveEFC ) )
            {
                if ( endWave == ElliottWaveEnum.WaveC || endWave == ElliottWaveEnum.Wave1C )
                {
                    if ( ( currentWave == ElliottWaveEnum.WaveA ) || ( currentWave == ElliottWaveEnum.WaveB ) || ( currentWave == ElliottWaveEnum.WaveC ) )
                    {
                        return true;
                    }
                }

            }

            return false;
        }


        public static WaveInfo? GetCycleAndLabelPositionBetweenWaves( int waveScenarioNo, IElliottWave previousWave,
                                                                       IElliottWave nextWave,
                                                                       ElliottWaveCycle cycle,
                                                                       ElliottWaveEnum currentWaveName
                                                                )
        {            
            if ( previousWave == null || nextWave == null )
            {
                return null;
            }

            var posCycle = new WaveInfo( ElliottWaveCycle.UNKNOWN, ElliottWaveEnum.NONE, WaveLabelPosition.UNKNOWN );

            ElliottWaveCycle output        = cycle;

            ref var hewPrev            = ref previousWave.GetWaveFromScenario( 1 );
            ref var hewNext                = ref nextWave.GetWaveFromScenario( 1 );

            var previousHighestWave        = hewPrev.GetLastHighestWaveDegree( );
            var nextHighestWave            = hewNext.GetFirstHighestWaveInfo( );

            var previousHighestWaveName    = previousHighestWave.HasValue ? previousHighestWave.Value.WaveName  : ElliottWaveEnum.NONE;
            var previousHighestWaveCycle   = previousHighestWave.HasValue ? previousHighestWave.Value.WaveCycle : ElliottWaveCycle.UNKNOWN;            
            var nextHighestWaveName        = nextHighestWave.HasValue     ? nextHighestWave.Value.WaveName      : ElliottWaveEnum.NONE;
            var nextHighestWaveCycle       = nextHighestWave.HasValue     ? nextHighestWave.Value.WaveCycle     : ElliottWaveCycle.UNKNOWN;
            

            int previousWaveCount          = hewPrev.GetWaveDegreeCount( );
            int nextWaveCount              = hewNext.GetWaveDegreeCount( );

            


            if ( previousHighestWaveCycle == nextHighestWaveCycle )
            {
                /*  
                 *  -----------------------------------------------------------------------------------------------------------------------------
                 *      So we are between two waves of the same degrees, there are 2 scenarios
                 *      1) Our current wave lable is a label of the same degree.
                 *      2) We are beginning to label a lower degree wave.
                 *  -----------------------------------------------------------------------------------------------------------------------------
                */

                if ( IsNextWave( previousHighestWaveName, nextHighestWaveName ) )
                {
                    output = previousHighestWaveCycle - GlobalConstants.OneWaveCycle;
                }
                else if ( previousHighestWaveName.IsNextWaveOneDegreeLowerBeginning( currentWaveName ) )
                {
                    output = previousHighestWaveCycle - GlobalConstants.OneWaveCycle;
                }
                else if ( IsInBetweenWave( previousHighestWaveName, currentWaveName, nextHighestWaveName ) )
                {
                    output = previousHighestWaveCycle;
                }
                else if ( IsNextWave( currentWaveName, nextHighestWaveName ) )
                {
                    output = nextHighestWaveCycle;
                }
                else if ( previousHighestWaveName.IsNextWaveTwoDegreesLowerBeginning( currentWaveName ) )
                {
                    output = previousHighestWaveCycle - GlobalConstants.OneWaveCycle - GlobalConstants.OneWaveCycle;
                }
                else
                {

                }
            }
            else if ( previousHighestWaveCycle > nextHighestWaveCycle )
            {
                if ( ( previousHighestWaveCycle - nextHighestWaveCycle ) == GlobalConstants.OneWaveCycle )
                {
                    /*  
                     *  --------------------------------------------------------------------------------------------------------------------------------------------------
                     *      1) Since previous Wave is one degree higher than next Wave, we want to see what wave that we have on the previous wave of the same degree 
                     *          a) If there is wave at the same degree, we can see if the current wave is in between waves of the lower degree.
                     *          b) If there isn't wave at that degree                     
                     *  --------------------------------------------------------------------------------------------------------------------------------------------------
                    */
                    ref var hew = ref previousWave.GetWaveFromScenario( waveScenarioNo );

                    var previousWaveLowerDegreeName = hew.GetWavesAtCycle( nextHighestWaveCycle );

                    if ( previousWaveLowerDegreeName.Count == 0 )
                    {
                        // Since it has a higher degree wave, the underlying wave should be the ending wave of either an impulsive wave or a corrective wave.
                        previousWaveLowerDegreeName.Add( GetLowerDegreeWaveName( previousHighestWaveName ) );
                    }

                    if ( previousWaveLowerDegreeName.Count > 0 && IsInBetweenWave( previousWaveLowerDegreeName[0], currentWaveName, nextHighestWaveName ) )
                    {
                        output = nextHighestWaveCycle;
                    }
                    else if ( IsNextWave( currentWaveName, nextHighestWaveName ) )
                    {
                        output = nextHighestWaveCycle;
                    }
                    else if ( previousWaveLowerDegreeName.Count > 0 && IsNextWave( previousWaveLowerDegreeName[0], nextHighestWaveName ) )
                    {
                        output = nextHighestWaveCycle - GlobalConstants.OneWaveCycle;
                    }                    
                    else if ( previousHighestWaveName.IsNextWaveOneDegreeLowerBeginning( currentWaveName ) )
                    {
                        output = previousHighestWaveCycle - GlobalConstants.OneWaveCycle;
                    }
                    else if ( previousHighestWaveName.IsNextWaveTwoDegreesLowerBeginning( currentWaveName ) )
                    {
                        output = previousHighestWaveCycle - GlobalConstants.OneWaveCycle - GlobalConstants.OneWaveCycle;
                    }
                    else
                    {

                    }                    
                }
                else if ( ( previousHighestWaveCycle - nextHighestWaveCycle ) > GlobalConstants.OneWaveCycle )
                {
                    /*  
                     *  --------------------------------------------------------------------------------------------------------------------------------------------------
                     *      1) Since previous Wave is Several degree higher than next Wave, we want to look up wave degree based on our database result.                     
                     *  --------------------------------------------------------------------------------------------------------------------------------------------------
                    */


                    //estWaveCycle = GetWaveCycleEstimation( responsibleForWhatTimeFrame, estWaveImportance );

                    //if ( estWaveCycle != null )
                    //{
                    //    if ( estWaveCycle.WaveCycle1 <= previousHighestWaveCycle && estWaveCycle.WaveCycle1 >= nextHighestWaveCycle )
                    //    {
                    //        output = estWaveCycle.WaveCycle1;
                    //    }
                    //}
                }
            }
            else if ( previousHighestWaveCycle < nextHighestWaveCycle )
            {
                int waveDegreeDiff = nextHighestWaveCycle - previousHighestWaveCycle;

                if ( waveDegreeDiff == GlobalConstants.OneWaveCycle )
                {
                    /*  
                     *  --------------------------------------------------------------------------------------------------------------------------------------------------
                     *      1) Since previous Wave is one degree higher than next Wave, we want to see what wave that we have on the previous wave of the same degree 
                     *          a) If there is wave at the same degree, we can see if the current wave is in between waves of the lower degree.
                     *          b) If there isn't wave at that degree                     
                     *  --------------------------------------------------------------------------------------------------------------------------------------------------
                    */
                    ref var hew = ref nextWave.GetWaveFromScenario( waveScenarioNo );


                    var nextWaveLowerDegreeName = hew.GetWavesAtCycle( previousHighestWaveCycle );

                    if ( nextWaveLowerDegreeName.Count == 0 )
                    {
                        nextWaveLowerDegreeName.Add( GetLowerDegreeWaveName( nextHighestWaveName ) );
                    }

                    if ( nextWaveLowerDegreeName.Count > 0 && IsInBetweenWave( previousHighestWaveName, currentWaveName, nextWaveLowerDegreeName[ 0 ] ) )
                    {
                        output = previousHighestWaveCycle;
                    }
                    else if ( nextWaveLowerDegreeName.Count > 0 && IsNextWave( previousHighestWaveName, nextWaveLowerDegreeName[0] ) )
                    {
                        output = previousHighestWaveCycle - GlobalConstants.OneWaveCycle;
                    }
                    else if ( IsNextWave( previousHighestWaveName, currentWaveName ) )
                    {
                        output = previousHighestWaveCycle;
                    }
                    else if ( previousHighestWaveName.IsNextWaveOneDegreeLowerBeginning( currentWaveName ) )
                    {
                        output = previousHighestWaveCycle - GlobalConstants.OneWaveCycle;
                    }
                    else if ( previousHighestWaveName.IsNextWaveTwoDegreesLowerBeginning( currentWaveName ) )
                    {
                        output = previousHighestWaveCycle - GlobalConstants.OneWaveCycle - GlobalConstants.OneWaveCycle;
                    }


                }
                else if ( waveDegreeDiff > GlobalConstants.OneWaveCycle )
                {
                    if ( IsNextWave( previousHighestWaveName, currentWaveName ) )
                    {
                        output = previousHighestWaveCycle;
                    }
                    else
                    {
                        /*  
                         *  --------------------------------------------------------------------------------------------------------------------------------------------------
                         *      1) Since previous Wave is Several degree higher than next Wave, we want to look up wave degree based on our database result.                     
                         *  --------------------------------------------------------------------------------------------------------------------------------------------------
                        */

                        //estWaveCycle = GetWaveCycleEstimation( responsibleForWhatTimeFrame, estWaveImportance );
                        //if ( estWaveCycle != null )
                        //{
                        //    if ( estWaveCycle.WaveCycle1 <= previousHighestWaveCycle && estWaveCycle.WaveCycle1 >= nextHighestWaveCycle )
                        //    {
                        //        output = estWaveCycle.WaveCycle1;
                        //    }
                        //}
                    }
                }
            }


            return posCycle;            
        }

        public static ElliottWaveCycle FindCycleBetweenWaves( int waveScenarioNo,
                                                                BTreeDictionary<long, WavePointImportance> waveImportanceDict,
                                                                IElliottWave previousWave,
                                                                IElliottWave nextWave,
                                                                TimeSpan responsibleForWhatTimeFrame,
                                                                long rawBarTime,
                                                                ElliottWaveCycle cycle,
                                                                ElliottWaveEnum currentWaveName,
                                                                ref SBar bar )
        {
            if ( previousWave == null || nextWave == null )
            {
                return cycle;
            }


            ElliottWaveCycle output = cycle;

            //ISmartWaveCycles estWaveCycle = null;

            ref var hewPrev       = ref previousWave.GetWaveFromScenario( waveScenarioNo );
            ref var hewNext           = ref nextWave.GetWaveFromScenario( waveScenarioNo );

            var previousHighestWave       = hewPrev.GetLastHighestWaveDegree( );
            var nextHighestWave           = hewNext.GetFirstHighestWaveInfo( );

            var previousHighestWaveName   = previousHighestWave.HasValue ? previousHighestWave.Value.WaveName : ElliottWaveEnum.NONE;
            var previousHighestWaveCycle  = previousHighestWave.HasValue ? previousHighestWave.Value.WaveCycle : ElliottWaveCycle.UNKNOWN;
            var nextHighestWaveName       = nextHighestWave.HasValue ? nextHighestWave.Value.WaveName : ElliottWaveEnum.NONE;
            var nextHighestWaveCycle      = nextHighestWave.HasValue ? nextHighestWave.Value.WaveCycle : ElliottWaveCycle.UNKNOWN;

            int previousWaveCount         = hewPrev.GetWaveDegreeCount( );
            int nextWaveCount             = hewNext.GetWaveDegreeCount( );

            var estPreviousWaveImportance = -1;
            var estNextWaveImportance     = -1;
            var estWaveImportance         = -1;

            if ( waveImportanceDict != null )
            {
                if ( waveImportanceDict.ContainsKey( rawBarTime ) )
                {
                    estWaveImportance = waveImportanceDict [ rawBarTime ].WaveImportance;
                }

                if ( waveImportanceDict.ContainsKey( previousWave.StartDate ) )
                {
                    estPreviousWaveImportance = waveImportanceDict [ previousWave.StartDate ].WaveImportance;
                }

                if ( waveImportanceDict.ContainsKey( nextWave.StartDate ) )
                {
                    estNextWaveImportance = waveImportanceDict [ nextWave.StartDate ].WaveImportance;
                }
            }


            if ( previousHighestWaveCycle == nextHighestWaveCycle )
            {
                /*  
                 *  -----------------------------------------------------------------------------------------------------------------------------
                 *      So we are between two waves of the same degrees, there are 2 scenarios
                 *      1) Our current wave lable is a label of the same degree.
                 *      2) We are beginning to label a lower degree wave.
                 *  -----------------------------------------------------------------------------------------------------------------------------
                */

                if ( IsNextWave( previousHighestWaveName, nextHighestWaveName ) )
                {
                    if ( IsAbcForImpulsiveWave( previousHighestWaveName, currentWaveName, nextHighestWaveName ) )
                    {
                        output = previousHighestWaveCycle;
                    }
                    else
                        output = previousHighestWaveCycle - GlobalConstants.OneWaveCycle;
                }
                else if ( IsInBetweenWave( previousHighestWaveName, currentWaveName, nextHighestWaveName ) )
                {
                    output = previousHighestWaveCycle;
                }
                else if ( IsNextWave( previousHighestWaveName, currentWaveName ) )
                {
                    output = previousHighestWaveCycle;
                }
                else
                {
                    /*  
                     *  -----------------------------------------------------------------------------------------------------------------------------
                     *      Since they are neither of the above, we want to check from the wave Estimation.
                     *      1) Since they are both of the same wave Degress, the WaveImportance should be either the same or off by one level
                     *      2) If the current Wave's WaveImportance is of the same as those previous and Next wave, they should be of the same
                     *         degree.
                     *  -----------------------------------------------------------------------------------------------------------------------------
                    */
                    if ( estPreviousWaveImportance > -1 && estNextWaveImportance > -1 && estWaveImportance > -1 )
                    {
                        var minImportance = Math.Min( estPreviousWaveImportance, estNextWaveImportance );
                        var maxImportance = Math.Max( estPreviousWaveImportance, estNextWaveImportance );

                        if ( estWaveImportance > maxImportance )
                        {
                            output = previousHighestWaveCycle + GlobalConstants.OneWaveCycle;
                        }
                        else if ( estWaveImportance < minImportance )
                        {
                            output = previousHighestWaveCycle - GlobalConstants.OneWaveCycle;
                        }
                        else if ( estWaveImportance == minImportance )
                        {
                            output = previousHighestWaveCycle;
                        }
                    }
                }
            }
            else if ( previousHighestWaveCycle > nextHighestWaveCycle )
            {
                int waveDegreeDiff = previousHighestWaveCycle - nextHighestWaveCycle;

                if ( waveDegreeDiff == GlobalConstants.OneWaveCycle )
                {
                    /*  
                     *  --------------------------------------------------------------------------------------------------------------------------------------------------
                     *      1) Since previous Wave is one degree higher than next Wave, we want to see what wave that we have on the previous wave of the same degree 
                     *          a) If there is wave at the same degree, we can see if the current wave is in between waves of the lower degree.
                     *          b) If there isn't wave at that degree                     
                     *  --------------------------------------------------------------------------------------------------------------------------------------------------
                    */
                    ref var hew = ref previousWave.GetWaveFromScenario( waveScenarioNo );

                    var previousWaveLowerDegree = hew.GetWavesAtCycle( nextHighestWaveCycle );

                    if ( previousWaveLowerDegree.Count > 0 )
                    {
                        if ( IsInBetweenWave( previousWaveLowerDegree [ 0 ], currentWaveName, nextHighestWaveName ) )
                        {
                            output = nextHighestWaveCycle;
                        }
                        else if ( IsNextWave( previousWaveLowerDegree [ 0 ], nextHighestWaveName ) )
                        {
                            output = nextHighestWaveCycle - GlobalConstants.OneWaveCycle;
                        }
                        else
                        {
                            /*  
                             *  -----------------------------------------------------------------------------------------------------------------------------
                             *      Since they are neither of the above, we want to check from the wave Estimation.
                             *      1) Since the previous wave is one degree higher than the next wave 
                             *      2) So we can compare out estWaveImportance with previous one and the next one and see which one it is closed too.
                             *  -----------------------------------------------------------------------------------------------------------------------------
                            */
                            if ( estPreviousWaveImportance > -1 && estNextWaveImportance > -1 && estWaveImportance > -1 )
                            {
                                var minImportance = Math.Min( estPreviousWaveImportance, estNextWaveImportance );
                                var maxImportance = Math.Max( estPreviousWaveImportance, estNextWaveImportance );

                                if ( estWaveImportance > maxImportance )
                                {
                                    output = previousHighestWaveCycle + GlobalConstants.OneWaveCycle;
                                }
                                else if ( estWaveImportance < minImportance )
                                {
                                    output = previousHighestWaveCycle - GlobalConstants.OneWaveCycle;
                                }
                                else if ( estWaveImportance == estPreviousWaveImportance )
                                {
                                    output = previousHighestWaveCycle;
                                }
                                else if ( estWaveImportance == estNextWaveImportance )
                                {
                                    output = nextHighestWaveCycle;
                                }
                            }
                        }
                    }
                    else
                    {
                        if ( IsNextWave(previousHighestWaveName, currentWaveName) )
                        {
                            output = previousHighestWaveCycle;
                        }
                        else if ( IsNextWave( currentWaveName, nextHighestWaveName ) )
                        {
                            output = nextHighestWaveCycle;
                        }
                        else if ( previousHighestWaveName.IsNextWaveOneDegreeLowerBeginning( currentWaveName ) )
                        {
                            output = previousHighestWaveCycle - GlobalConstants.OneWaveCycle;
                        }
                        else if ( previousHighestWaveName.IsNextWaveTwoDegreesLowerBeginning( currentWaveName ) )
                        {
                            output = previousHighestWaveCycle - GlobalConstants.OneWaveCycle - GlobalConstants.OneWaveCycle;
                        }
                        else
                        {
                            /*  
                                    *  -----------------------------------------------------------------------------------------------------------------------------
                                    *      Since they are neither of the above, we want to check from the wave Estimation.
                                    *      1) Since the previous wave is one degree higher than the next wave 
                                    *      2) So we can compare out estWaveImportance with previous one and the next one and see which one it is closed too.
                                    *  -----------------------------------------------------------------------------------------------------------------------------
                                */
                            if ( estPreviousWaveImportance > -1 && estNextWaveImportance > -1 && estWaveImportance > -1 )
                            {
                                var minImportance = Math.Min( estPreviousWaveImportance, estNextWaveImportance );
                                var maxImportance = Math.Max( estPreviousWaveImportance, estNextWaveImportance );

                                if ( estWaveImportance > maxImportance )
                                {
                                    output = previousHighestWaveCycle + GlobalConstants.OneWaveCycle;
                                }
                                else if ( estWaveImportance < minImportance )
                                {
                                    output = nextHighestWaveCycle - GlobalConstants.OneWaveCycle;
                                }
                                else if ( estWaveImportance == estPreviousWaveImportance )
                                {
                                    output = previousHighestWaveCycle;
                                }
                                else if ( estWaveImportance == estNextWaveImportance )
                                {
                                    output = nextHighestWaveCycle;
                                }
                            }
                        }
                    }
                }
                else if ( waveDegreeDiff > GlobalConstants.OneWaveCycle )
                {
                    /*  
                     *  --------------------------------------------------------------------------------------------------------------------------------------------------
                     *      1) Since previous Wave is Several degree higher than next Wave, we want to look up wave degree based on our database result.                     
                     *  --------------------------------------------------------------------------------------------------------------------------------------------------
                    */

                    output = previousHighestWaveCycle;
                }
            }
            else if ( previousHighestWaveCycle < nextHighestWaveCycle )
            {
                int waveDegreeDiff = nextHighestWaveCycle - previousHighestWaveCycle;

                if ( waveDegreeDiff == GlobalConstants.OneWaveCycle )
                {
                    /*  
                     *  --------------------------------------------------------------------------------------------------------------------------------------------------
                     *      1) Since previous Wave is one degree LOWER than next Wave, we want to see what wave that we have on the previous wave of the same degree 
                     *          a) If there is wave at the same degree, we can see if the current wave is in between waves of the lower degree.
                     *          b) If there isn't wave at that degree                     
                     *  --------------------------------------------------------------------------------------------------------------------------------------------------
                    */
                  
                    var nextWaveLowerDegreeName = hewNext.GetWavesAtCycle( previousHighestWaveCycle );

                    if ( nextWaveLowerDegreeName.Count > 0 )
                    {
                        if ( IsInBetweenWave( previousHighestWaveName, currentWaveName, nextWaveLowerDegreeName [ 0 ] ) )
                        {
                            output = previousHighestWaveCycle;
                        }
                        else if ( IsNextWave( previousHighestWaveName, nextWaveLowerDegreeName [ 0 ] ) )
                        {
                            output = previousHighestWaveCycle - GlobalConstants.OneWaveCycle;
                        }
                        else
                        {
                            /*  
                             *  -----------------------------------------------------------------------------------------------------------------------------
                             *      Since they are neither of the above, we want to check from the wave Estimation.
                             *      1) Since the previous wave is one degree higher than the next wave 
                             *      2) So we can compare out estWaveImportance with previous one and the next one and see which one it is closed too.
                             *  -----------------------------------------------------------------------------------------------------------------------------
                            */
                            if ( estPreviousWaveImportance > -1 && estNextWaveImportance > -1 && estWaveImportance > -1 )
                            {
                                var minImportance = Math.Min( estPreviousWaveImportance, estNextWaveImportance );
                                var maxImportance = Math.Max( estPreviousWaveImportance, estNextWaveImportance );

                                if ( estWaveImportance > maxImportance )
                                {
                                    output = nextHighestWaveCycle + GlobalConstants.OneWaveCycle;
                                }
                                else if ( estWaveImportance < minImportance )
                                {
                                    output = previousHighestWaveCycle - GlobalConstants.OneWaveCycle;
                                }
                                else if ( estWaveImportance == estPreviousWaveImportance )
                                {
                                    output = previousHighestWaveCycle;
                                }
                                else if ( estWaveImportance == estNextWaveImportance )
                                {
                                    output = nextHighestWaveCycle;
                                }
                            }
                        }
                    }
                    else
                    {
                        if ( IsNextWave( previousHighestWaveName, currentWaveName ) )
                        {
                            output = previousHighestWaveCycle;
                        }
                        else if (IsNextWave(currentWaveName, nextHighestWaveName))
                        {
                            output = nextHighestWaveCycle;
                        }
                        else if ( previousHighestWaveName.IsNextWaveOneDegreeLowerBeginning( currentWaveName ) )
                        {
                            output = previousHighestWaveCycle - GlobalConstants.OneWaveCycle;
                        }
                        else if ( previousHighestWaveName.IsNextWaveTwoDegreesLowerBeginning( currentWaveName ) )
                        {
                            output = previousHighestWaveCycle - GlobalConstants.OneWaveCycle - GlobalConstants.OneWaveCycle;
                        }
                        else
                        {
                            /*  
                                    *  -----------------------------------------------------------------------------------------------------------------------------
                                    *      Since they are neither of the above, we want to check from the wave Estimation.
                                    *      1) Since the previous wave is one degree higher than the next wave 
                                    *      2) So we can compare out estWaveImportance with previous one and the next one and see which one it is closed too.
                                    *  -----------------------------------------------------------------------------------------------------------------------------
                                */
                            if ( estPreviousWaveImportance > -1 && estNextWaveImportance > -1 && estWaveImportance > -1 )
                            {
                                var minImportance = Math.Min( estPreviousWaveImportance, estNextWaveImportance );
                                var maxImportance = Math.Max( estPreviousWaveImportance, estNextWaveImportance );

                                if ( estWaveImportance > maxImportance )
                                {
                                    output = nextHighestWaveCycle + GlobalConstants.OneWaveCycle;
                                }
                                else if ( estWaveImportance < minImportance )
                                {
                                    output = previousHighestWaveCycle - GlobalConstants.OneWaveCycle;
                                }
                                else if ( estWaveImportance == estPreviousWaveImportance )
                                {
                                    output = previousHighestWaveCycle;
                                }
                                else if ( estWaveImportance == estNextWaveImportance )
                                {
                                    output = nextHighestWaveCycle;
                                }
                            }
                        }
                    }
                }
                else if ( waveDegreeDiff > GlobalConstants.OneWaveCycle )
                {
                    output = previousHighestWaveCycle;
                }
            }


            return output;
        }

        public static ElliottWaveCycle FindCycleFromNextWave(  int waveScenarioNo,
                                                                BTreeDictionary<long, WavePointImportance> waveImportanceDict,
                                                                IElliottWave nextWaveDB,
                                                                TimeSpan responsibleForWhatTimeFrame,
                                                                long rawBarTime,
                                                                ElliottWaveCycle cycle,
                                                                ElliottWaveEnum currentWaveName,
                                                                ref SBar bar 
                                                            )
        {
            ref var hew = ref nextWaveDB.GetWaveFromScenario( waveScenarioNo );

            var allwaves = hew.GetAllWaves( );

            foreach ( var nextWave in allwaves )
            {
                if ( IsNextWave( currentWaveName, nextWave.WaveName ) )
                {
                    return nextWave.WaveCycle;
                }
                else if ( currentWaveName.IsNextWaveOneDegreeLowerBeginning( nextWave.WaveName ) )
                {
                    return nextWave.WaveCycle - GlobalConstants.OneWaveCycle;
                }
                else if ( currentWaveName.IsNextWaveTwoDegreesLowerBeginning( nextWave.WaveName ) )
                {
                    return ( nextWave.WaveCycle - GlobalConstants.OneWaveCycle - GlobalConstants.OneWaveCycle );
                }
                else if ( currentWaveName.IsOneDegreeLowerEndingWrt( nextWave.WaveName ) )
                {
                    return nextWave.WaveCycle - GlobalConstants.OneWaveCycle;
                }
            }

            ref var hew2 = ref nextWaveDB.GetWaveFromScenario( waveScenarioNo );

            var nextHighestWave            = hew2.GetFirstHighestWaveInfo( );
            var nextHighestWaveCycle       = nextHighestWave.HasValue ? nextHighestWave.Value.WaveCycle : ElliottWaveCycle.UNKNOWN;

            var estNextWaveImportance      = -1;
            var estWaveImportance          = -1;

            if ( waveImportanceDict != null )
            {
                if ( waveImportanceDict.ContainsKey( rawBarTime ) )
                {
                    estWaveImportance = waveImportanceDict [ rawBarTime ].WaveImportance;
                }

                if ( waveImportanceDict.ContainsKey( nextWaveDB.StartDate ) )
                {
                    estNextWaveImportance = waveImportanceDict [ nextWaveDB.StartDate ].WaveImportance;
                }
            }



            /*  
            *  -----------------------------------------------------------------------------------------------------------------------------
            *      Since they are neither of the above, we want to check from the wave Estimation.
            *      1) Since they are both of the same wave Degress, the WaveImportance should be either the same or off by one level
            *      2) If the current Wave's WaveImportance is of the same as those previous and Next wave, they should be of the same
            *         degree.
            *  -----------------------------------------------------------------------------------------------------------------------------
            */
            ElliottWaveCycle output        = cycle;
            //ISmartWaveCycles estWaveCycle = null;

            if ( estNextWaveImportance > -1 && estWaveImportance > -1 )
            {
                if ( estWaveImportance > estNextWaveImportance )
                {
                    output = nextHighestWaveCycle + GlobalConstants.OneWaveCycle;
                }
                else if ( estWaveImportance < estNextWaveImportance )
                {
                    output = nextHighestWaveCycle - GlobalConstants.OneWaveCycle;
                }
                else if ( estWaveImportance == estNextWaveImportance )
                {
                    output = nextHighestWaveCycle;
                }
                else
                {
                    output = nextHighestWaveCycle;
                }
            }


            return output;
        }


        public static ElliottWaveCycle FindCycleFromPreviousWave( int waveScenarioNo, 
                                                                    BTreeDictionary<long, WavePointImportance> waveImportanceDict,
                                                                    IElliottWave previousDB,
                                                                    TimeSpan responsibleForWhatTimeFrame,
                                                                    long rawBarTime,
                                                                    ElliottWaveCycle cycle,
                                                                    ElliottWaveEnum currentWaveName,
                                                                    ref SBar bar )
        {
            ref var hew = ref previousDB.GetWaveFromScenario( waveScenarioNo );
            var allwaves = hew.GetAllWaves( );

            foreach ( var previousWave in allwaves )
            {
                if ( IsNextWave( previousWave.WaveName, currentWaveName ) )
                {
                    return previousWave.WaveCycle;
                }
            }

            foreach ( var previousWave in allwaves )
            {
                if ( previousWave.WaveName.IsNextWaveOneDegreeLowerBeginning( currentWaveName ) )
                {
                    return previousWave.WaveCycle - GlobalConstants.OneWaveCycle;
                }
                else if ( previousWave.WaveName.IsNextWaveTwoDegreesLowerBeginning( currentWaveName ) )
                {
                    return ( previousWave.WaveCycle - GlobalConstants.OneWaveCycle - GlobalConstants.OneWaveCycle );
                }
                else if ( previousWave.WaveName.IsOneDegreeLowerEndingWrt( currentWaveName ) )
                {
                    return previousWave.WaveCycle + GlobalConstants.OneWaveCycle;
                }
            }

            
            var previousHighestWave = hew.GetFirstHighestWaveInfo( );
            var previousHighestWaveCycle = previousHighestWave.HasValue ? previousHighestWave.Value.WaveCycle : ElliottWaveCycle.UNKNOWN;

            var estNextWaveImportance = -1;
            var estWaveImportance = -1;

            if ( waveImportanceDict != null )
            {
                if ( waveImportanceDict.ContainsKey( rawBarTime ) )
                {
                    estWaveImportance = waveImportanceDict [ rawBarTime ].WaveImportance;
                }

                if ( waveImportanceDict.ContainsKey( previousDB.StartDate ) )
                {
                    estNextWaveImportance = waveImportanceDict [ previousDB.StartDate ].WaveImportance;
                }
            }



            /*  
            *  -----------------------------------------------------------------------------------------------------------------------------
            *      Since they are neither of the above, we want to check from the wave Estimation.
            *      1) Since they are both of the same wave Degress, the WaveImportance should be either the same or off by one level
            *      2) If the current Wave's WaveImportance is of the same as those previous and Next wave, they should be of the same
            *         degree.
            *  -----------------------------------------------------------------------------------------------------------------------------
            */
            ElliottWaveCycle output = cycle;
            //ISmartWaveCycles estWaveCycle = null;

            if ( estNextWaveImportance > -1 && estWaveImportance > -1 )
            {
                if ( estWaveImportance > estNextWaveImportance )
                {
                    output = previousHighestWaveCycle + GlobalConstants.OneWaveCycle;
                }
                else if ( estWaveImportance < estNextWaveImportance )
                {
                    output = previousHighestWaveCycle - GlobalConstants.OneWaveCycle;
                }
                else if ( estWaveImportance == estNextWaveImportance )
                {
                    output = previousHighestWaveCycle;
                }
                else
                {
                    output = previousHighestWaveCycle;
                }
            }


            return output;
        }



        public static bool IsAbcForImpulsiveWave( ElliottWaveEnum beginWave,
                                     ElliottWaveEnum currentWave,
                                     ElliottWaveEnum endWave )
        {
            if ( ( currentWave != ElliottWaveEnum.WaveA ) && ( currentWave != ElliottWaveEnum.WaveB ) && ( currentWave != ElliottWaveEnum.WaveC ) )
            {
                return false;
            }

            switch ( endWave )
            {
                case ElliottWaveEnum.Wave3:
                case ElliottWaveEnum.Wave3C:

                    if ( beginWave == ElliottWaveEnum.Wave2 ) return true;
                    break;

                case ElliottWaveEnum.Wave5:
                case ElliottWaveEnum.Wave5C:

                    if ( beginWave == ElliottWaveEnum.Wave4 ) return true;
                    break;

                case ElliottWaveEnum.Wave1:
                case ElliottWaveEnum.Wave1C:
                    if ( beginWave == ElliottWaveEnum.Wave5 || beginWave == ElliottWaveEnum.Wave5C ) return true;
                    break;
            }
            return false;
        }


        public static ElliottWaveCycle GetWaveCycleEstimation( int offerId,
                                                                    TimeSpan timeframe,
                                                                    int waveImportance )
        {
            ElliottWaveCycle output = ElliottWaveCycle.GrandSupercycle;


            if ( timeframe == TimeSpan.FromMinutes( 1 ) )
            {
                if ( waveImportance == 89 )
                {
                }
                else if ( waveImportance == 55 )
                {
                }
                else if ( waveImportance == 34 )
                {
                }
                else if ( waveImportance == 21 )
                {
                }
                else if ( waveImportance == 13 )
                {
                }
                else if ( waveImportance == 8 )
                {
                }
                else if ( waveImportance == 5 )
                {
                }
            }
            else if ( timeframe == TimeSpan.FromMinutes( 5 ) )
            {
                if ( waveImportance == 89 )
                {
                }
                else if ( waveImportance == 55 )
                {
                }
                else if ( waveImportance == 34 )
                {
                }
                else if ( waveImportance == 21 )
                {
                }
                else if ( waveImportance == 13 )
                {
                }
                else if ( waveImportance == 8 )
                {
                }
                else if ( waveImportance == 5 )
                {
                }
            }
            else if ( timeframe == TimeSpan.FromMinutes( 15 ) )
            {
                if ( waveImportance == 89 )
                {
                }
                else if ( waveImportance == 55 )
                {
                }
                else if ( waveImportance == 34 )
                {
                }
                else if ( waveImportance == 21 )
                {
                }
                else if ( waveImportance == 13 )
                {
                }
                else if ( waveImportance == 8 )
                {
                }
                else if ( waveImportance == 5 )
                {
                }
            }
            else if ( timeframe == TimeSpan.FromMinutes( 60 ) )
            {
                if ( waveImportance == 89 )
                {
                }
                else if ( waveImportance == 55 )
                {
                }
                else if ( waveImportance == 34 )
                {
                }
                else if ( waveImportance == 21 )
                {
                }
                else if ( waveImportance == 13 )
                {
                }
                else if ( waveImportance == 8 )
                {
                }
                else if ( waveImportance == 5 )
                {
                }
            }
            else if ( timeframe == TimeSpan.FromDays( 1 ) )
            {
                if ( waveImportance == 89 )
                {
                    output = ElliottWaveCycle.Intermediate;
                }
                else if ( waveImportance == 55 )
                {
                    output = ElliottWaveCycle.Intermediate;
                }
                else if ( waveImportance == 34 )
                {
                    output = ElliottWaveCycle.Minor; 
                }
                else if ( waveImportance == 21 )
                {
                    output = ElliottWaveCycle.Minor;
                }
                else if ( waveImportance == 13 )
                {
                    output = ElliottWaveCycle.Minor;
                }
                else if ( waveImportance == 8 )
                {
                    output = ElliottWaveCycle.Minute;
                }
                else if ( waveImportance == 5 )
                {
                    output = ElliottWaveCycle.Minute;
                }
            }
            else if ( timeframe == TimeSpan.FromDays( 7 ) )
            {
                if ( waveImportance == 89 )
                {
                    output = ElliottWaveCycle.Supercycle;
                }
                else if ( waveImportance == 55 )
                {
                    output = ElliottWaveCycle.Supercycle;
                }
                else if ( waveImportance == 34 )
                {
                    output = ElliottWaveCycle.Intermediate;
                }
                else if ( waveImportance == 21 )
                {
                    output = ElliottWaveCycle.Intermediate;
                }
                else if ( waveImportance == 13 )
                {
                    output = ElliottWaveCycle.Intermediate;
                }
                else if ( waveImportance == 8 )
                {
                    output = ElliottWaveCycle.Minor;
                }
                else if ( waveImportance == 5 )
                {
                    output = ElliottWaveCycle.Minor;
                }
            }
            else if ( timeframe == TimeSpan.FromDays( 30 ) )
            {
                if ( waveImportance == 89 )
                {
                    output = ElliottWaveCycle.GrandSupercycle;
                }
                else if ( waveImportance == 55 )
                {
                    output = ElliottWaveCycle.GrandSupercycle;
                }
                else if ( waveImportance == 34 )
                {
                    output = ElliottWaveCycle.Supercycle;
                }
                else if ( waveImportance == 21 )
                {
                    output = ElliottWaveCycle.Supercycle;
                }
                else if ( waveImportance == 13 )
                {
                    output = ElliottWaveCycle.Supercycle;
                }
                else if ( waveImportance == 8 )
                {
                    output = ElliottWaveCycle.Intermediate;
                }
                else if ( waveImportance == 5 )
                {
                    output = ElliottWaveCycle.Intermediate;
                }
            }
        

            return output;
        }

        public static ElliottWaveCycle FindCurrentWaveCycle( int waveScenarioNo, long rawBarTime, ElliottWaveCycle cycle, ElliottWaveEnum currentWaveName, ref SBar bar, IElliottWave previousWave, IElliottWave nextWave, BTreeDictionary<long, WavePointImportance> waveImportanceDictionary, int offerId, TimeSpan responsibleForWhatTimeFrame )
        {
            ElliottWaveCycle output = ElliottWaveCycle.UNKNOWN;

            if ( previousWave != null && nextWave != null ) // We are trying to label wave in Betwen 2 wave labels
            {
                output = FindCycleBetweenWaves( waveScenarioNo, waveImportanceDictionary, previousWave, nextWave, responsibleForWhatTimeFrame, rawBarTime, cycle, currentWaveName, ref  bar );
            }
            else if ( previousWave != null )
            {
                output = FindCycleFromPreviousWave( waveScenarioNo, waveImportanceDictionary, previousWave, responsibleForWhatTimeFrame, rawBarTime, cycle, currentWaveName, ref bar );
            }
            else if ( nextWave != null )
            {
                output = FindCycleFromNextWave( waveScenarioNo, waveImportanceDictionary, nextWave, responsibleForWhatTimeFrame, rawBarTime, cycle, currentWaveName, ref bar );
            }
            else
            {
                var waveImpt = -1;
                if ( waveImportanceDictionary != null )
                {
                    if ( waveImportanceDictionary.ContainsKey( rawBarTime ) )
                    {
                        waveImpt = waveImportanceDictionary [ rawBarTime ].WaveImportance;
                    }

                    if ( waveImpt > -1 )
                    {
                        output = GetWaveCycleEstimation( offerId, responsibleForWhatTimeFrame, waveImportanceDictionary [ rawBarTime ].WaveImportance );                        
                    }
                }
            }

            if ( output == ElliottWaveCycle.UNKNOWN )
            {
                output = cycle;
            }

            return output;
        }


    }

}
    


