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
        private void PredictWave5ForExtendedWave3( TimeSpan period, long selectedBarTime, ElliottWaveCycle waveDegree )
        {
            if( _wave5Exp == null )
                return;

            PooledList< FibLevelInfo > wave5Targets = null;

            var diff = _bars.Security.PriceStep.Value;

            double highestLowestFib = 0;

            if ( _wave5Exp != null && Bar3C != SBar.EmptySBar)
            {
                if ( Bar3C.IsTrough( ) )
                {
                    wave5Targets = _wave5Exp.GetAllFibLevelsBelow( Bar3C.PeakTroughValue, out highestLowestFib );
                }
                else
                {
                    wave5Targets = _wave5Exp.GetAllFibLevelsAbove( Bar3C.PeakTroughValue, out highestLowestFib );
                }



                if ( _wave5Exp.HasTonyExtensions )
                {
                    foreach ( FibLevelInfo fibLevelInfo in _wave5Exp.TonyFirstLevels.FibLevels )
                    {
                        var index = wave5Targets.FindIndex( x => Math.Abs( x.FibLevel - fibLevelInfo.FibLevel ) < (double)( diff * 4 ) );

                        if ( index > -1 )
                        {
                            var wave5CalcTarget = wave5Targets[ index ];

                            wave5CalcTarget.UpdateAll( fibLevelInfo );                            
                        }
                        else
                        {
                            if ( _wave5Exp.IsUptrend )
                            {
                                if ( fibLevelInfo.FibLevel < highestLowestFib && fibLevelInfo.FibLevel >= Bar3C.PeakTroughValue)
                                {
                                    wave5Targets.Add( fibLevelInfo );
                                }
                            }
                            else
                            {
                                if ( fibLevelInfo.FibLevel > highestLowestFib && fibLevelInfo.FibLevel <= Bar3C.PeakTroughValue )
                                {
                                    wave5Targets.Add( fibLevelInfo );
                                }
                            }
                        }
                    }
                }

                if ( _wave5Exp.HasTonyExtensions2 )
                {
                    foreach ( FibLevelInfo fibLevelInfo in _wave5Exp.TonySecondLevels.FibLevels )
                    {
                        var index = wave5Targets.FindIndex( x => Math.Abs( x.FibLevel - fibLevelInfo.FibLevel ) < (double)( diff * 4 ) );

                        if ( index > -1 )
                        {
                            var wave5CalcTarget = wave5Targets[ index ];

                            wave5CalcTarget.UpdateAll( fibLevelInfo );                            
                        }
                        else
                        {
                            if ( _wave5Exp.IsUptrend )
                            {
                                if ( fibLevelInfo.FibLevel < highestLowestFib && fibLevelInfo.FibLevel >= Bar3C.PeakTroughValue )
                                {
                                    wave5Targets.Add( fibLevelInfo );
                                }
                            }
                            else
                            {
                                if ( fibLevelInfo.FibLevel > highestLowestFib && fibLevelInfo.FibLevel <= Bar3C.PeakTroughValue )
                                {
                                    wave5Targets.Add( fibLevelInfo );
                                }
                            }
                        }
                    }
                }
            }

            if ( _wave3Exp != null )
            {
                var target = _wave3Exp.GetFibLevelsBtw( FibPercentage.Fib_276_4, FibPercentage.Fib_323_6 );

                foreach ( FibLevelInfo fibLevelInfo in target )
                {
                    var index = wave5Targets.FindIndex( x => Math.Abs( x.FibLevel - fibLevelInfo.FibLevel ) < (double)( diff * 4 ) );

                    if ( index > -1 )
                    {
                        var wave5CalcTarget = wave5Targets[ index ];

                        wave5CalcTarget.UpdateAll( fibLevelInfo );                        
                    }
                    else
                    {
                        if ( _wave5Exp.IsUptrend )
                        {
                            if ( fibLevelInfo.FibLevel < highestLowestFib )
                            {
                                wave5Targets.Add( fibLevelInfo );
                            }
                        }
                        else
                        {
                            if ( fibLevelInfo.FibLevel > highestLowestFib )
                            {
                                wave5Targets.Add( fibLevelInfo );
                            }
                        }
                    }
                }

                if ( _wave3Exp.HasTonyExtensions )
                {
                    foreach ( FibLevelInfo fibLevelInfo in _wave3Exp.TonyFirstLevels.FibLevels )
                    {
                        var index = wave5Targets.FindIndex( x => Math.Abs( x.FibLevel - fibLevelInfo.FibLevel ) < (double)( diff * 4 ) );

                        if ( index > -1 )
                        {
                            var wave5CalcTarget = wave5Targets[ index ];

                            wave5CalcTarget.UpdateAll( fibLevelInfo );
                        }
                        else
                        {
                            if ( _wave5Exp.IsUptrend )
                            {
                                if ( fibLevelInfo.FibLevel < highestLowestFib && fibLevelInfo.FibLevel >= Bar3C.PeakTroughValue )
                                {
                                    wave5Targets.Add( fibLevelInfo );
                                }
                            }
                            else
                            {
                                if ( fibLevelInfo.FibLevel > highestLowestFib && fibLevelInfo.FibLevel <= Bar3C.PeakTroughValue )
                                {
                                    wave5Targets.Add( fibLevelInfo );

                                }
                            }
                        }
                    }
                }

                _largerTargets = wave5Targets;
            }
        }

        private void PredictWave4ForExtendedWave3( TimeSpan period, long selectedBarTime, ElliottWaveCycle waveDegree )
        {

        }

    }
}
