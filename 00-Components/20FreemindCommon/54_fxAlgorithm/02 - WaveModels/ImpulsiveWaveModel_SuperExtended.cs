using fx.Bars;
using fx.Collections;
using fx.Common;
using fx.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Algorithm
{
    public partial class ImpulsiveWaveModel : IWaveModel
    {
        private void PredictWave4ForSuperExtendedWave3( int waveScenarioNo, TimeSpan period, long selectedBarTime, ElliottWaveCycle waveDegree )
        {
            if ( _wave4Ret == null )
                return;

            // ![](36FB7D21B48FCA036EA0851CF62D4A17.png;;;0.03383,0.02484)
            // To maintain the bullishness, wave < 4 > should not break wave {IV}            

            var superExtendedWave4Targets       = new PooledList< FibLevelInfo >( );
            double maxWave4FibLevel             = 0;            
            var pips                            = (double) _bars.Security.PriceStep.Value;

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  Since it is Super Extended Wave 3 model, Wave 4 should be as follows.
             *  
             *  Wave 4:         176.4% - 200% - 223.6% of Wave 1.
             *                  23.6%  - 41.4% - 44.1% of Wave 3    
             * ------------------------------------------------------------------------------------------------------------------------------------------- */
            var wave4_RET_236_441   = _wave4Ret.GetAllFibLevelsLE( FibPercentage.Fib_44_1, 5 * pips );
            var wave1_EXP_1764_2236 = _wave3Exp.GetFibLevelsBtw( FibPercentage.Fib_176_4, FibPercentage.Fib_223_6 );

            if ( wave4_RET_236_441 != null )
            {
                if ( _wave4Ret.IsUptrend )
                {
                    // The Whole Trend from beginning is Uptrend. Wave4 is downtrend, so we have to find the lowest
                    maxWave4FibLevel = wave4_RET_236_441.Min( t => t.FibLevel );

                }
                else
                {
                    // The Whole Trend from beginning is Downtrend. Wave4 is uptrend, so we have to find the highest
                    maxWave4FibLevel = wave4_RET_236_441.Max( t => t.FibLevel );
                }

                superExtendedWave4Targets.AddRange( wave4_RET_236_441 );

                superExtendedWave4Targets = superExtendedWave4Targets.OrderBy( x => x.FibLevel ).ToPooledList();
            }

            long wave4smaller = _hews.FindPreviousWave4( waveScenarioNo, period, selectedBarTime, waveDegree - GlobalConstants.OneWaveCycle );
                        
            if ( wave4smaller > -1 )
            {
                ref SBar barSmaller4 = ref _bars.GetBarByTime( wave4smaller );

                if ( barSmaller4 == SBar.EmptySBar ) return;                

                int breakIndex = -1;

                if ( barSmaller4.IsWaveTrough( ) )
                {
                    breakIndex = _bars.GetIndexBreakDown( barSmaller4.BarIndex, _bars.TotalBarCount, barSmaller4.Low );
                }
                else
                {
                    breakIndex = _bars.GetIndexBreakUp( barSmaller4.BarIndex, _bars.TotalBarCount, barSmaller4.High );
                }

                if ( breakIndex > -1 )
                {
                    PooledList< FibLevelInfo > wave4Broken = null;

                    // Now that Wave 4 has been broken, we are going to look at the Fib levels below Wave 4.
                    if ( barSmaller4 != SBar.EmptySBar)
                    {
                        if ( barSmaller4.IsTrough( ) )
                        {
                            wave4Broken = _wave4Ret.GetAllFibLevelsBelow( barSmaller4.PeakTroughValue, out double discard );
                        }
                        else
                        {
                            wave4Broken = _wave4Ret.GetAllFibLevelsAbove( barSmaller4.PeakTroughValue, out double discard );
                        }

                        foreach ( FibLevelInfo fibLevelInfo in wave4Broken )
                        {
                            var index = superExtendedWave4Targets.FindIndex( x => x.Equals( fibLevelInfo ) );

                            if ( index > -1 )
                            {
                                var selectedLevel = superExtendedWave4Targets[ index ];

                                selectedLevel.LikelyScore *= 2;
                            }
                        }
                    }

                    // ![](00F903AC9BCCD58282FC55DDDCF8C3A7.png;;;0.02630,0.02218)
                    foreach ( FibLevelInfo fibLevelInfo in _wave4Ret.TonyFirstLevels.FibLevels )
                    {
                        var index = superExtendedWave4Targets.FindIndex( x => Math.Abs( x.FibLevel - fibLevelInfo.FibLevel ) < (double)( pips * 5 ) );

                        if ( index > -1 )
                        {
                            var selectedLevel = superExtendedWave4Targets[ index ];

                            selectedLevel.UpdateAll( fibLevelInfo );                            
                        }
                        else
                        {
                            if ( ! _wave4Ret.IsUptrend )
                            {
                                if ( fibLevelInfo.FibLevel < maxWave4FibLevel )
                                {
                                    superExtendedWave4Targets.Add( fibLevelInfo );
                                }                                
                            }
                            else
                            {
                                if ( fibLevelInfo.FibLevel > maxWave4FibLevel )
                                {
                                    superExtendedWave4Targets.Add( fibLevelInfo );
                                }
                            }                            
                        }
                    }

                    foreach ( FibLevelInfo fibLevelInfo in _wave4Ret.TonySecondLevels.FibLevels )
                    {
                        var index = superExtendedWave4Targets.FindIndex( x => Math.Abs( x.FibLevel - fibLevelInfo.FibLevel ) < (double)( pips * 5 ) );

                        if ( index > -1 )
                        {
                            var selectedLevel = superExtendedWave4Targets[ index ];

                            selectedLevel.UpdateAll( fibLevelInfo );                            
                        }
                        else
                        {
                            if ( ! _wave4Ret.IsUptrend )
                            {
                                if ( fibLevelInfo.FibLevel < maxWave4FibLevel )
                                {
                                    superExtendedWave4Targets.Add( fibLevelInfo );
                                }
                            }
                            else
                            {
                                if ( fibLevelInfo.FibLevel > maxWave4FibLevel )
                                {
                                    superExtendedWave4Targets.Add( fibLevelInfo );
                                }
                            }
                        }
                    }

                    foreach ( FibLevelInfo fibLevelInfo in _wave4Ret.TonyRetracementLevels.FibLevels )
                    {
                        var index = superExtendedWave4Targets.FindIndex( x => Math.Abs( x.FibLevel - fibLevelInfo.FibLevel ) < (double)( pips * 5 ) );

                        if ( index > -1 )
                        {
                            var selectedLevel = superExtendedWave4Targets[ index ];

                            selectedLevel.UpdateAll( fibLevelInfo );                            
                        }
                        else
                        {
                            if ( ! _wave4Ret.IsUptrend )
                            {
                                if ( fibLevelInfo.FibLevel < maxWave4FibLevel )
                                {
                                    superExtendedWave4Targets.Add( fibLevelInfo );
                                }
                            }
                            else
                            {
                                if ( fibLevelInfo.FibLevel > maxWave4FibLevel )
                                {
                                    superExtendedWave4Targets.Add( fibLevelInfo );
                                }
                            }
                        }
                    }
                }
                else
                {
                    PooledList< FibLevelInfo > notBreakWave4 = null;

                    // To maintain the bullishness, wave < 4 > should not break wave {IV}
                    var nonBreakWave4Ret = _hews.GetRetracementTargets( waveScenarioNo, _bars.Security, period, wave4smaller, selectedBarTime, waveDegree );

                    if ( nonBreakWave4Ret != null )
                    {
                        foreach ( FibLevelInfo fibLevelInfo in nonBreakWave4Ret.RegularRetraceProjectionLevels.FibLevels )
                        {
                            var index = superExtendedWave4Targets.FindIndex( x => Math.Abs( x.FibLevel - fibLevelInfo.FibLevel ) < (double)( pips * 5 ) );

                            if ( index > -1 )
                            {
                                var selectedLevel = superExtendedWave4Targets[ index ];

                                selectedLevel.UpdateAll( fibLevelInfo );                                
                            }
                            else
                            {
                                superExtendedWave4Targets.Add( fibLevelInfo );
                            }
                        }
                    }



                    if ( barSmaller4 != SBar.EmptySBar)
                    {
                        if ( barSmaller4.IsTrough( ) )
                        {
                            notBreakWave4 = _wave4Ret.GetAllFibLevelsAbove( barSmaller4.PeakTroughValue, out double discard );
                        }
                        else
                        {
                            notBreakWave4 = _wave4Ret.GetAllFibLevelsBelow( barSmaller4.PeakTroughValue, out double discard );
                        }

                        foreach ( FibLevelInfo fibLevelInfo in notBreakWave4 )
                        {
                            var index = superExtendedWave4Targets.FindIndex( x => x.Equals( fibLevelInfo ) );

                            if ( index > -1 )
                            {
                                var selectedLevel = superExtendedWave4Targets[ index ];

                                selectedLevel.LikelyScore *= 2;
                            }
                        }
                    }

                    var pt_4b_5 = _hews.GetPoints_Wave4B_Wave5( waveScenarioNo, wave4smaller, waveDegree - GlobalConstants.OneWaveCycle, selectedBarTime );

                    if ( pt_4b_5 != default )
                    {
                        var fib_4b_5 = _hews.GetRetracementTargets( _bars.Security, period, pt_4b_5 );

                        foreach ( FibLevelInfo fibLevelInfo in fib_4b_5.RegularRetraceProjectionLevels.FibLevels )
                        {
                            var index = superExtendedWave4Targets.FindIndex( x => Math.Abs( x.FibLevel - fibLevelInfo.FibLevel ) < (double)( pips * 5 ) );

                            if ( index > -1 )
                            {
                                var selectedLevel = superExtendedWave4Targets[ index ];

                                selectedLevel.UpdateAll( fibLevelInfo );                                
                            }
                            else
                            {
                                if ( ! _wave4Ret.IsUptrend )
                                {
                                    if ( fibLevelInfo.FibLevel < maxWave4FibLevel )
                                    {
                                        superExtendedWave4Targets.Add( fibLevelInfo );
                                    }
                                }
                                else
                                {
                                    if ( fibLevelInfo.FibLevel > maxWave4FibLevel )
                                    {
                                        superExtendedWave4Targets.Add( fibLevelInfo );
                                    }
                                }
                            }
                        }

                        var time4B = pt_4b_5.Item1.Item1.ToLinuxTime( );

                        var pt_4_5 = _hews.GetPoints_LowerDegreeWave4_Wave5(waveScenarioNo, time4B, waveDegree - GlobalConstants.OneWaveCycle * 2, selectedBarTime );

                        if ( pt_4_5 != default )
                        {
                            var pt_4b_5Target = _hews.GetTonyProjectionTarget( _bars.Security, period, pt_4_5 );

                            foreach ( FibLevelInfo fibLevelInfo in pt_4b_5Target.TonyFirstLevels.FibLevels )
                            {
                                var index = superExtendedWave4Targets.FindIndex( x => Math.Abs( x.FibLevel - fibLevelInfo.FibLevel ) < (double)( pips * 5 ) );

                                if ( index > -1 )
                                {
                                    var selectedLevel = superExtendedWave4Targets[ index ];

                                    selectedLevel.UpdateAll( fibLevelInfo );                                    
                                }
                                else
                                {
                                    if ( ! _wave4Ret.IsUptrend )
                                    {
                                        if ( fibLevelInfo.FibLevel < maxWave4FibLevel )
                                        {
                                            superExtendedWave4Targets.Add( fibLevelInfo );
                                        }
                                    }
                                    else
                                    {
                                        if ( fibLevelInfo.FibLevel > maxWave4FibLevel )
                                        {
                                            superExtendedWave4Targets.Add( fibLevelInfo );
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                for ( int i = 0; i < superExtendedWave4Targets.Count; i++ )
                {
                    if ( Bar3C.IsWavePeak() )
                    {
                        if ( _bars.GetIndexBreakDown( Bar3C.BarIndex, _bars.TotalBarCount, superExtendedWave4Targets[ i ].FibLevel ) > -1 )
                        {
                            var fibInfo = superExtendedWave4Targets[ i ];
                            fibInfo.IsBroken = true;
                            superExtendedWave4Targets[ i ] = fibInfo;

                        }
                    }
                    else
                    {
                        if ( _bars.GetIndexBreakUp( Bar3C.BarIndex, _bars.TotalBarCount, superExtendedWave4Targets[ i ].FibLevel ) > -1 )
                        {
                            var fibInfo = superExtendedWave4Targets[ i ];
                            fibInfo.IsBroken = true;
                            superExtendedWave4Targets[ i ] = fibInfo;
                        }
                    }
                }
                
                _largerTargets = superExtendedWave4Targets.OrderByDescending( x => x.OverlappedCount ).ToPooledList();
            }
        }


        private void PredictWave5ForSuperExtendedWave3( TimeSpan period, long selectedBarTime, ElliottWaveCycle waveDegree )
        {

        }

    }
}
