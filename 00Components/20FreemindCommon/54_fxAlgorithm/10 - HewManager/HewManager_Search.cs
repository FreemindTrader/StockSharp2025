using System;
using fx.Database.Common.DataModel;
using fx.Database.ForexDatabarsDataModel;
using DevExpress.Mvvm;
using System.Linq;
using fx.Definitions.UndoRedo;
using fx.Definitions;
using fx.Database;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using fx.Definitions.Collections;
using fx.Collections;
using fx.Bars;

namespace fx.Algorithm
{
    public partial class HewManager
    {
        public DbElliottWave GetPreviousWaveStructure( int waveScenarioNo, long rawBarTime )
        {
            if ( _hews.Count > 0 )
            {
                var previousWaves = _hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.HasElliottWave )
                    {
                        return wave.Value;
                    }
                }                              
            }

            return null;
        }

        public DbElliottWave GetNextWaveStructure( int waveScenarioNo, long rawBarTime )
        {
            if ( _hews.Count > 0 )
            {
                var nextWaves = _hews.GetElementsLeftOf( rawBarTime );

                foreach ( var wave in nextWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.HasElliottWave )
                    {
                        return wave.Value;
                    }
                }

                //return ( _hews.GetLeftElement( rawBarTime ) );                
            }

            return null;
        }

        public ElliottWaveEnum GetLastWaveOfDegree( int waveScenarioNo, ElliottWaveCycle currentWaveDegree )
        {
            if ( _hews.Count > 0 )
            {
                foreach ( var wave in _hews )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    PooledList< ElliottWaveEnum > lastWave = hew.GetWavesAtCycle( currentWaveDegree );

                    if ( lastWave.Count > 0 )
                    {
                        return lastWave[ lastWave.Count - 1 ];
                    }
                }
            }

            return ElliottWaveEnum.NONE;
        }

        public ElliottWaveEnum GetPreviousWaveOfDegree( int waveScenarioNo, TimeSpan period, long rawBarTime, ElliottWaveCycle currentWaveDegree )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                var previousWaves = hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    PooledList< ElliottWaveEnum > lastWave = hew.GetWavesAtCycle( currentWaveDegree );

                    if ( lastWave.Count > 0 )
                    {
                        return lastWave[ lastWave.Count - 1 ];
                    }
                }
            }

            return ElliottWaveEnum.NONE;
        }

        public ElliottWaveEnum GetPreviousWaveOfDegree( int waveScenarioNo, long rawBarTime, ElliottWaveCycle currentWaveDegree )
        {
            if ( _hews.Count > 0 )
            {
                var previousWaves = _hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    PooledList< ElliottWaveEnum > lastWave = hew.GetWavesAtCycle( currentWaveDegree );

                    if ( lastWave.Count > 0 )
                    {
                        return lastWave[ lastWave.Count - 1 ];
                    }
                }
            }

            return ElliottWaveEnum.NONE;
        }

        public WavePointInfo? GetPreviousWavePointInfoOfDegreeOrHigher( int waveScenarioNo, long rawBarTime, ElliottWaveCycle currentWaveDegree )
        {
            if ( _hews.Count > 0 )
            {
                var previousWaves = _hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    var lastWave = hew.GetHewPointInfoAtCycleOrHigherLast( currentWaveDegree );

                    if ( lastWave != null )
                    {
                        return new WavePointInfo( wave.Key, lastWave.Value );
                    }
                }
            }

            return null;
        }

        public WavePointInfo? GetPreviousWavePointInfoOfDegreeOrHigher( int waveScenarioNo, TimeSpan period, long rawBarTime, ElliottWaveCycle currentWaveDegree )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                var previousWaves = hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    var lastWave = hew.GetHewPointInfoAtCycleOrHigherLast( currentWaveDegree );

                    if ( lastWave != null )
                    {
                        return new WavePointInfo( wave.Key, lastWave.Value );
                    }
                }
            }

            return null;
        }

        public WavePointInfo? GetNextWavePointInfoOfDegreeOrHigher( int waveScenarioNo, TimeSpan period, long rawBarTime, ElliottWaveCycle currentWaveDegree )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                var nextWaves = hews.GetElementsLeftOf( rawBarTime );

                foreach ( var wave in nextWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    var lastWave = hew.GetHewPointInfoAtCycleOrHigherFirst( currentWaveDegree );

                    if ( lastWave != null )
                    {
                        return new WavePointInfo( wave.Key, lastWave.Value );
                    }
                }
            }

            return null;
        }

        public WavePointInfo? GetNextWavePointInfoOfDegreeOrHigher( int waveScenarioNo, long rawBarTime, ElliottWaveCycle currentWaveDegree )
        {
            if ( _hews.Count > 0 )
            {
                var nextWaves = _hews.GetElementsLeftOf( rawBarTime );

                foreach ( var wave in nextWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    var lastWave = hew.GetHewPointInfoAtCycleOrHigherFirst( currentWaveDegree );

                    if ( lastWave != null )
                    {
                        return new WavePointInfo( wave.Key, lastWave.Value );
                    }
                }
            }

            return null;
        }

        public DbElliottWave GetPreviousWaveStructureOfDegree( int waveScenarioNo, TimeSpan period, long rawBarTime, ElliottWaveCycle currentWaveDegree )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                var previousWaves = hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    PooledList< ElliottWaveEnum > lastWave = hew.GetWavesAtCycle( currentWaveDegree );

                    if ( lastWave.Count > 0 )
                    {
                        return wave.Value;
                    }
                }
            }

            return null;
        }

        public DbElliottWave GetPreviousWaveStructureOfDegree( int waveScenarioNo, long rawBarTime, ElliottWaveCycle currentWaveDegree )
        {
            if ( _hews.Count > 0 )
            {
                var previousWaves = _hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    PooledList< ElliottWaveEnum > lastWave = hew.GetWavesAtCycle( currentWaveDegree );

                    if ( lastWave.Count > 0 )
                    {
                        return wave.Value;
                    }
                }
            }

            return null;
        }

        public long GetRawBarTimeOfPreviousWave( int waveScenarioNo, long rawBarTime )
        {
            var dbHewPrev = GetPreviousWaveStructure( waveScenarioNo, rawBarTime );

            if ( dbHewPrev != null )
            {
                return dbHewPrev.StartDate;
            }

            return -1;
        }

        public long GetRawBarTimeOfPreviousWaveOfDegree( int waveScenarioNo, long rawBarTime,
                                                         ElliottWaveCycle currentWaveDegree )
        {
            if ( _hews.Count > 0 )
            {
                var previousWaves = _hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.GetWavesAtCycle( currentWaveDegree ).Count > 0 )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public WaveLabelPosition GetPreviousWaveLabelPosition( int waveScenarioNo, long rawBarTime, ElliottWaveCycle currentWaveDegree )
        {
            if ( _hews.Count > 0 )
            {
                var previousWaves = _hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    var lastWave = hew.GetWavesAtCycle( currentWaveDegree );

                    if ( lastWave.Count > 0 )
                    {
                        return hew.GetWaveLabelPosition();
                    }
                }
            }

            return WaveLabelPosition.UNKNOWN;
        }

        public DbElliottWave GetNextWaveOfSameDegreeExcept24( int waveScenarioNo, long rawBarTime, ElliottWaveEnum currentWave, ElliottWaveCycle currentWaveDegree )
        {
            ElliottWaveEnum nextWave = ElliottWaveEnum.NONE;

            switch ( currentWave )
            {
                case ElliottWaveEnum.Wave2:
                    nextWave = ElliottWaveEnum.Wave3;
                    break;

                case ElliottWaveEnum.Wave4:
                    nextWave = ElliottWaveEnum.Wave5;
                    break;
            }

            if ( currentWave == ElliottWaveEnum.WaveB )
            {
                nextWave = WaveBOfWhat( waveScenarioNo, rawBarTime, currentWaveDegree );
            }

            if ( _hews.Count > 0 )
            {
                var nextWaves = _hews.GetElementsLeftOf( rawBarTime );

                foreach ( var wave in nextWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    var lastWave = hew.GetHewPointInfoAtCycle( currentWaveDegree );

                    if ( lastWave.HasValue )
                    {
                        if ( nextWave == ElliottWaveEnum.NONE )
                        {
                            if ( lastWave.Value.WaveCycle == currentWaveDegree )
                            {
                                return wave.Value;
                            }
                        }
                        else if ( lastWave.Value.WaveName.SameWaveName( nextWave ) )
                        {
                            return wave.Value;
                        }
                    }
                }
            }

            return null;
        }

        public DbElliottWave GetNextWaveOfSameDegreeExcept24( int waveScenarioNo, TimeSpan period, long rawBarTime, ElliottWaveEnum currentWave, ElliottWaveCycle currentWaveDegree )
        {
            var hews = GetElliottWavesDictionary( period );

            ElliottWaveEnum nextWave = ElliottWaveEnum.NONE;

            switch ( currentWave )
            {
                case ElliottWaveEnum.Wave2:
                    nextWave = ElliottWaveEnum.Wave3;
                    break;

                case ElliottWaveEnum.Wave4:
                    nextWave = ElliottWaveEnum.Wave5;
                    break;
            }

            if ( currentWave == ElliottWaveEnum.WaveB )
            {
                nextWave = WaveBOfWhat( waveScenarioNo, rawBarTime, currentWaveDegree );
            }

            if ( hews.Count > 0 )
            {
                var nextWaves = hews.GetElementsLeftOf( rawBarTime );

                foreach ( var wave in nextWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    var lastWave = hew.GetHewPointInfoAtCycle( currentWaveDegree );

                    if ( lastWave.HasValue )
                    {
                        if ( nextWave == ElliottWaveEnum.NONE )
                        {
                            if ( lastWave.Value.WaveCycle == currentWaveDegree )
                            {
                                return wave.Value;
                            }
                        }
                        else if ( lastWave.Value.WaveName.SameWaveName( nextWave ) )
                        {
                            return wave.Value;
                        }
                    }
                }
            }

            return null;
        }

        public long GetRawBarTimeOfNextWave( int waveScenarioNo, long rawBarTime )
        {
            var dbHewNext = GetNextWaveStructure( waveScenarioNo, rawBarTime );

            if ( dbHewNext != null )
            {
                return dbHewNext.StartDate;
            }

            return -1;
        }

        public long GetRawBarTimeOfNextWaveOfDegree( int waveScenarioNo, long rawBarTime,
                                                     ElliottWaveCycle currentWaveDegree )
        {
            if ( _hews.Count > 0 )
            {
                var nextWaves = _hews.GetElementsLeftOf( rawBarTime );

                foreach ( var wave in nextWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.GetWavesAtCycle( currentWaveDegree ).Count > 0 )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public WaveLabelPosition GetNextWaveLabelPosition( int waveScenarioNo, long rawBarTime,
                                                           ElliottWaveCycle waveCycle )
        {
            if ( _hews.Count > 0 )
            {
                var nextWaves = _hews.GetElementsLeftOf( rawBarTime );

                foreach ( var wave in nextWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.GetWavesAtCycle( waveCycle ).Count > 0 )
                    {
                        return hew.GetWaveLabelPosition();
                    }
                }
            }

            return WaveLabelPosition.UNKNOWN;
        }

        public WaveInfo? FindBeginningWaveOfThisWaveC( int waveScenarioNo, long rawBarTime, ElliottWaveCycle waveCycle )
        {
            if ( _hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo, rawBarTime ) )
                {
                    var specialWave = _hews[ rawBarTime ];

                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.IsWave12345OrHigherDegree_ScanBackward( waveCycle ) )
                    {
                        var waveInfo = hew.GetLastHighestWaveDegree( );

                        if ( waveInfo.HasValue )
                        {
                            return waveInfo;
                        }
                    }
                }

                var previousWaves = _hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew2 = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew2.IsWave12345OrHigherDegree( waveCycle ) )
                    {
                        var waveInfo = hew2.GetLastHighestWaveDegree( );

                        if ( waveInfo.HasValue )
                        {
                            return waveInfo;
                        }
                    }
                }
            }

            return null;
        }

        public DbElliottWave FindBeginningDbElliottWaveOfThisWaveC( int waveScenarioNo, long rawBarTime, ElliottWaveCycle waveCycle )
        {
            if ( _hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  rawBarTime ) )
                {
                    var specialWave = _hews[ rawBarTime ];

                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.IsWave12345OrHigherDegree_ScanBackward( waveCycle ) )
                    {
                        var waveInfo = hew.GetLastHighestWaveDegree( );

                        if ( waveInfo.HasValue )
                        {
                            return specialWave;
                        }
                    }
                }

                var previousWaves = _hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.IsWave12345OrHigherDegree( waveCycle ) )
                    {
                        var waveInfo = hew.GetLastHighestWaveDegree( );

                        if ( waveInfo.HasValue )
                        {
                            return wave.Value;
                        }
                    }
                }
            }

            return null;
        }

        public DbElliottWave FindBeginningWave( int waveScenarioNo, TimeSpan period, long rawBarTime, ElliottWaveCycle waveCycle )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo, rawBarTime ) )
                {
                    var specialWave = _hews[ rawBarTime ];

                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.IsWave12345OrHigherDegree_ScanBackward( waveCycle ) )
                    {
                        var waveInfo = hew.GetLastHighestWaveDegree( );

                        if ( waveInfo.HasValue )
                        {
                            return specialWave;
                        }
                    }
                }

                var previousWaves = hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.IsWave12345OrHigherDegree( waveCycle ) )
                    {
                        var waveInfo = hew.GetLastHighestWaveDegree( );

                        if ( waveInfo.HasValue )
                        {
                            return wave.Value;
                        }
                    }
                }
            }

            return null;
        }
        public long FindBeginningWaveTimeOfCurrentCycle( int waveScenarioNo, long rawBarTime, ElliottWaveCycle cycle )
        {
            if ( _hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo, rawBarTime ) )
                {
                    var specialWave = _hews[ rawBarTime ];

                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.IsWave5OrHigherDegree_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = _hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.IsWave5OrHigherDegree( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindBeginningWaveTimeOfCurrentCycle( int waveScenarioNo, TimeSpan period, long rawBarTime, ElliottWaveCycle cycle )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo, period, rawBarTime ) )
                {
                    var specialWave = hews[ rawBarTime ];

                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.IsWave5OrHigherDegree_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.IsWave5OrHigherDegree( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindPreviousWave2( int waveScenarioNo, long rawBarTime, ElliottWaveCycle cycle )
        {
            if ( _hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo, rawBarTime ) )
                {
                    var specialWave = _hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.HasWave2OfDegree_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = _hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.HasWave2OfDegree( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindPreviousWave2( int waveScenarioNo, TimeSpan period, long rawBarTime, ElliottWaveCycle cycle )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo, period, rawBarTime ) )
                {
                    var specialWave = hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.HasWave2OfDegree_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.HasWave2OfDegree( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindPreviousWaveA( int waveScenarioNo, long rawBarTime, ElliottWaveCycle cycle )
        {
            if ( _hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo, rawBarTime ) )
                {
                    var specialWave = _hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.HasWaveAOfDegree_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = _hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.HasWaveAOfDegree( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindPreviousWaveA( int waveScenarioNo, TimeSpan period, long rawBarTime, ElliottWaveCycle cycle )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  period, rawBarTime ) )
                {
                    var specialWave = hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.HasWaveAOfDegree_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.HasWaveAOfDegree( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindPreviousWaveB( int waveScenarioNo, long rawBarTime,
                                       ElliottWaveCycle cycle )
        {
            if ( _hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  rawBarTime ) )
                {
                    var specialWave = _hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.HasWaveBOfDegree_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = _hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.HasWaveBOfDegree( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindPreviousWaveB( int waveScenarioNo, TimeSpan period,
                                        long rawBarTime,
                                        ElliottWaveCycle cycle )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  period, rawBarTime ) )
                {
                    var specialWave = hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.HasWaveBOfDegree_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.HasWaveBOfDegree( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindPreviousWaveC( int waveScenarioNo, long rawBarTime,
                                       ElliottWaveCycle cycle )
        {
            if ( _hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  rawBarTime ) )
                {
                    var specialWave = _hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.HasWaveCOfDegree_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = _hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.HasWaveCOfDegree( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindPreviousWaveC( int waveScenarioNo, TimeSpan period, long rawBarTime, ElliottWaveCycle cycle )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  period, rawBarTime ) )
                {
                    var specialWave = hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.HasWaveCOfDegree_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.HasWaveCOfDegree( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindPreviousWaveW( int waveScenarioNo, long rawBarTime, ElliottWaveCycle cycle )
        {
            if ( _hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  rawBarTime ) )
                {
                    var specialWave = _hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.HasWaveWOfDegree_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = _hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.HasWaveWOfDegree( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindPreviousWaveW( int waveScenarioNo, TimeSpan period, long rawBarTime, ElliottWaveCycle cycle )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  period, rawBarTime ) )
                {
                    var specialWave = hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.HasWaveYOfDegree_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.HasWaveWOfDegree( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindPreviousWaveY( int waveScenarioNo, long rawBarTime, ElliottWaveCycle cycle )
        {
            if ( _hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  rawBarTime ) )
                {
                    var specialWave = _hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.HasWaveYOfDegree_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = _hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.HasWaveYOfDegree( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindPreviousWaveY( int waveScenarioNo, TimeSpan period, long rawBarTime, ElliottWaveCycle cycle )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  period, rawBarTime ) )
                {
                    var specialWave = hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.HasWaveWOfDegree_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.HasWaveYOfDegree( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindPreviousWaveZ( int waveScenarioNo, long rawBarTime, ElliottWaveCycle cycle )
        {
            if ( _hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  rawBarTime ) )
                {
                    var specialWave = _hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.HasWaveZOfDegree_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = _hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.HasWaveZOfDegree( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindPreviousWaveZ( int waveScenarioNo, TimeSpan period, long rawBarTime, ElliottWaveCycle cycle )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  period, rawBarTime ) )
                {
                    var specialWave = hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.HasWaveZOfDegree_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.HasWaveZOfDegree( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindPreviousWaveWY( int waveScenarioNo, long rawBarTime, ElliottWaveCycle cycle )
        {
            if ( _hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  rawBarTime ) )
                {
                    var specialWave = _hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.HasWaveWYOfDegree_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = _hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.HasWaveWYOfDegree( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindPreviousWaveWY( int waveScenarioNo, TimeSpan period, long rawBarTime, ElliottWaveCycle cycle )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  period, rawBarTime ) )
                {
                    var specialWave = hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.HasWaveWYOfDegree_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.HasWaveWYOfDegree( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindPreviousWave024X( int waveScenarioNo, long rawBarTime, ElliottWaveCycle cycle )
        {
            if ( _hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  rawBarTime ) )
                {
                    var specialWave = _hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.HasWave024X_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = _hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.HasWave024XOrHigher( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindPreviousWave024X( int waveScenarioNo, TimeSpan period, long rawBarTime, ElliottWaveCycle cycle )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  period, rawBarTime ) )
                {
                    var specialWave = hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.HasWave024X_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.HasWave024XOrHigher( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindPreviousWave4( int waveScenarioNo, long rawBarTime, ElliottWaveCycle cycle )
        {
            if ( _hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  rawBarTime ) )
                {
                    var specialWave = _hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.HasWave4_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = _hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.HasWave4OfDegree( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindPreviousWave4( int waveScenarioNo, TimeSpan period, long rawBarTime, ElliottWaveCycle cycle )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  period, rawBarTime ) )
                {
                    var specialWave = hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.HasWave4_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.HasWave4OfDegree( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindPreviousWave0X( int waveScenarioNo, long rawBarTime,
                                        ElliottWaveCycle cycle )
        {
            if ( _hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  rawBarTime ) )
                {
                    var specialWave = _hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.HasWave0X_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = _hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.HasWave0X_OrHigher( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindBeginOfCurrentTrend( int waveScenarioNo, TimeSpan period, long rawBarTime, ElliottWaveCycle cycle )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  rawBarTime ) )
                {
                    var specialWave = hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.IsAnyWaveOfOneHigherDegree_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.IsAnyWaveOfOneHigherDegree( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }


        public long FindPreviousWave0X( int waveScenarioNo, TimeSpan period, long rawBarTime, ElliottWaveCycle cycle )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  rawBarTime ) )
                {
                    var specialWave = hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.HasWave0X_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.HasWave0X_OrHigher( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindPreviousWaveEFA( int waveScenarioNo, long rawBarTime,
                                         ElliottWaveCycle cycle )
        {
            if ( _hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  rawBarTime ) )
                {
                    var specialWave = _hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.HasWaveEFA_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = _hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.HasWaveEFA( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindPreviousWaveEFA( int waveScenarioNo, TimeSpan period, long rawBarTime, ElliottWaveCycle cycle )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo, rawBarTime ) )
                {
                    var specialWave = hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.HasWaveEFA_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.HasWaveEFA( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindPreviousWave1( int waveScenarioNo, long rawBarTime,
                                       ElliottWaveCycle cycle )
        {
            if ( _hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  rawBarTime ) )
                {
                    var specialWave = _hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.HasWave1OfDegree_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = _hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.HasWave1OfDegree( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindPreviousWave1( int waveScenarioNo, TimeSpan period, long rawBarTime, ElliottWaveCycle cycle )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  period, rawBarTime ) )
                {
                    var specialWave = hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.HasWave1OfDegree_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.HasWave1OfDegree( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindPreviousWave3( int waveScenarioNo, long rawBarTime,
                                        ElliottWaveCycle cycle )
        {
            if ( _hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  rawBarTime ) )
                {
                    var specialWave = _hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.HasWave3OfDegree_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = _hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.HasWave3OfDegree( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindPreviousWave3( int waveScenarioNo, TimeSpan period, long rawBarTime,
                                        ElliottWaveCycle cycle )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  period, rawBarTime ) )
                {
                    var specialWave = hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.HasWave3OfDegree_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.HasWave3OfDegree( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindPreviousWave3Btw( int waveScenarioNo, TimeSpan period, long startTime, long endTime, ElliottWaveCycle cycle )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  period, endTime ) )
                {
                    var specialWave = hews[ endTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.HasWave3OfDegree_ScanBackward( cycle ) )
                    {
                        return endTime;
                    }
                }

                var previousWaves = hews.GetElementsRightOf( endTime );

                foreach ( var wave in previousWaves )
                {
                    if ( wave.Key > startTime )
                    {
                        ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                        if ( hew.HasWave3OfDegree( cycle ) )
                        {
                            return wave.Key;
                        }
                    }
                }
            }

            return -1;
        }

        public long FindPreviousWaveCBtw( int waveScenarioNo, TimeSpan period, long startTime, long endTime, ElliottWaveCycle cycle )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  period, endTime ) )
                {
                    var specialWave = hews[ endTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.HasWaveCOfDegree_ScanBackward( cycle ) )
                    {
                        return endTime;
                    }
                }

                var previousWaves = hews.GetElementsRightOf( endTime );

                foreach ( var wave in previousWaves )
                {
                    if ( wave.Key > startTime )
                    {
                        ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                        if ( hew.HasWaveCOfDegree( cycle ) )
                        {
                            return wave.Key;
                        }
                    }
                }
            }

            return -1;
        }

        public long FindPreviousWaveBBtw( int waveScenarioNo, TimeSpan period, long startTime, long endTime, ElliottWaveCycle cycle )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  period, endTime ) )
                {
                    var specialWave = hews[ endTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.HasWaveBOfDegree_ScanBackward( cycle ) )
                    {
                        return endTime;
                    }
                }

                var previousWaves = hews.GetElementsRightOf( endTime );

                foreach ( var wave in previousWaves )
                {
                    if ( wave.Key > startTime )
                    {
                        ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                        if ( hew.HasWaveBOfDegree( cycle ) )
                        {
                            return wave.Key;
                        }
                    }
                }
            }

            return -1;
        }

        public long FindPreviousWaveABtw( int waveScenarioNo, TimeSpan period, long startTime, long endTime, ElliottWaveCycle cycle )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  period, endTime ) )
                {
                    var specialWave = hews[ endTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.HasWaveAOfDegree_ScanBackward( cycle ) )
                    {
                        return endTime;
                    }
                }

                var previousWaves = hews.GetElementsRightOf( endTime );

                foreach ( var wave in previousWaves )
                {
                    if ( wave.Key > startTime )
                    {
                        ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                        if ( hew.HasWaveAOfDegree( cycle ) )
                        {
                            return wave.Key;
                        }
                    }
                }
            }

            return -1;
        }

        public long FindPreviousWave2Btw( int waveScenarioNo, TimeSpan period, long startTime, long endTime, ElliottWaveCycle cycle )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  period, endTime ) )
                {
                    var specialWave = hews[ endTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.HasWave2OfDegree_ScanBackward( cycle ) )
                    {
                        return endTime;
                    }
                }

                var previousWaves = hews.GetElementsRightOf( endTime );

                foreach ( var wave in previousWaves )
                {
                    if ( wave.Key > startTime )
                    {
                        ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                        if ( hew.HasWave2OfDegree( cycle ) )
                        {
                            return wave.Key;
                        }
                    }
                }
            }

            return -1;
        }

        public long FindPreviousWave1Btw( int waveScenarioNo, TimeSpan period, long startTime, long endTime, ElliottWaveCycle cycle )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  period, endTime ) )
                {
                    var specialWave = hews[ endTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.HasWave1OfDegree_ScanBackward( cycle ) )
                    {
                        return endTime;
                    }
                }

                var previousWaves = hews.GetElementsRightOf( endTime );

                foreach ( var wave in previousWaves )
                {
                    if ( wave.Key > startTime )
                    {
                        ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                        if ( hew.HasWave1OfDegree( cycle ) )
                        {
                            return wave.Key;
                        }
                    }
                }
            }

            return -1;
        }

        public long FindBeginningWaveOfWaveY( int waveScenarioNo, long rawBarTime, ElliottWaveCycle cycle )
        {
            return ( FindBeginningWaveOfWaveY( waveScenarioNo, _currentActiveTimeFrame, rawBarTime, cycle ) );
        }

        public long FindBeginningWaveOfWaveY( int waveScenarioNo, TimeSpan period, long rawBarTime, ElliottWaveCycle cycle )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo, rawBarTime ) )
                {
                    var specialWave = hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.IsWaveX_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.IsWaveX( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindBeginningWaveOfWaveW( int waveScenarioNo, long rawBarTime, ElliottWaveCycle cycle )
        {
            return ( FindBeginningWaveOfWaveY( waveScenarioNo, _currentActiveTimeFrame, rawBarTime, cycle ) );
        }

        public long FindBeginningWaveOfWaveW( int waveScenarioNo, TimeSpan period, long rawBarTime, ElliottWaveCycle cycle )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo, rawBarTime ) )
                {
                    var specialWave = hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.IsAnyWaveOfOneHigherDegree_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.IsAnyWaveOfOneHigherDegree( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindBeginningWaveOfCurrentABCX( int waveScenarioNo, long rawBarTime, ElliottWaveCycle cycle )
        {
            if ( _hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  rawBarTime ) )
                {
                    var specialWave = _hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.IsWave12345OrHigherDegree_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = _hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.IsWave12345OrHigherDegree( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindEndWaveOfCurrentABCX( int waveScenarioNo, long rawBarTime, ElliottWaveCycle cycle )
        {
            if ( _hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  rawBarTime ) )
                {
                    var specialWave = _hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.IsWave12345OrHigherDegree( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var nextWaves = _hews.GetElementsLeftOf( rawBarTime );

                foreach ( var wave in nextWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.IsWave12345OrHigherDegree( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindBeginningWaveOfCurrentExpandedFlat( int waveScenarioNo, long rawBarTime, ElliottWaveCycle cycle )
        {
            if ( _hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  rawBarTime ) )
                {
                    var specialWave = _hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.IsBeginningWaveOfExpandedFlat_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = _hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.IsBeginningWaveOfExpandedFlat( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindBeginningWaveOfCurrentExpandedFlat( int waveScenarioNo, TimeSpan period, long rawBarTime, ElliottWaveCycle cycle )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo, rawBarTime ) )
                {
                    var specialWave = hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.IsBeginningWaveOfExpandedFlat_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.IsBeginningWaveOfExpandedFlat( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindBeginningWaveOfCurrentTriangle( int waveScenarioNo, long rawBarTime,
                                                            ElliottWaveCycle cycle )
        {
            if ( _hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  rawBarTime ) )
                {
                    var specialWave = _hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.IsBeginningWaveOfExpandedFlat_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = _hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.IsBeginningWaveOfTriangle( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindBeginningWaveOfCurrentTriangle( int waveScenarioNo, TimeSpan period, long rawBarTime, ElliottWaveCycle cycle )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo, rawBarTime ) )
                {
                    var specialWave = hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.IsBeginningWaveOfExpandedFlat_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.IsBeginningWaveOfTriangle( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindWaveAOfTriangle( int waveScenarioNo, long rawBarTime, ElliottWaveCycle cycle )
        {
            if ( _hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  rawBarTime ) )
                {
                    var specialWave = _hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.IsWaveAOfTriangle_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = _hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.IsWaveAOfTriangle( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindWaveAOfTriangle( int waveScenarioNo, TimeSpan period, long rawBarTime, ElliottWaveCycle cycle )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo, rawBarTime ) )
                {
                    var specialWave = hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.IsWaveAOfTriangle_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.IsWaveAOfTriangle( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindWaveBOfTriangle( int waveScenarioNo, TimeSpan period, long rawBarTime, ElliottWaveCycle cycle )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo, rawBarTime ) )
                {
                    var specialWave = hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.IsWaveBOfTriangle_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.IsWaveBOfTriangle( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindWaveCOfTriangle( int waveScenarioNo, TimeSpan period, long rawBarTime, ElliottWaveCycle cycle )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo, rawBarTime ) )
                {
                    var specialWave = hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.IsWaveCOfTriangle_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.IsWaveCOfTriangle( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindWaveBOfTriangle( int waveScenarioNo, long rawBarTime, ElliottWaveCycle cycle )
        {
            if ( _hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  rawBarTime ) )
                {
                    var specialWave = _hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.IsWaveBOfTriangle_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = _hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.IsWaveBOfTriangle( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindWaveCOfTriangle( int waveScenarioNo, long rawBarTime, ElliottWaveCycle cycle )
        {
            if ( _hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  rawBarTime ) )
                {
                    var specialWave = _hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.IsWaveCOfTriangle_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = _hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.IsWaveCOfTriangle( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindWaveDOfTriangle( int waveScenarioNo, long rawBarTime, ElliottWaveCycle cycle )
        {
            if ( _hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  rawBarTime ) )
                {
                    var specialWave = _hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.IsWaveDOfTriangle_ScanBackward( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var previousWaves = _hews.GetElementsRightOf( rawBarTime );

                foreach ( var wave in previousWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.IsWaveDOfTriangle( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long FindEndWaveOfCurrent( int waveScenarioNo, long rawBarTime,
                                          ElliottWaveCycle cycle )
        {
            if ( _hews.Count > 0 )
            {
                if ( IsSpecialBar( waveScenarioNo,  rawBarTime ) )
                {
                    var specialWave = _hews[ rawBarTime ];
                    ref var hew = ref specialWave.GetWaveFromScenario( waveScenarioNo );
                    if ( hew.IsWave5OrHigherDegree( cycle ) )
                    {
                        return rawBarTime;
                    }
                }

                var nextWaves = _hews.GetElementsLeftOf( rawBarTime );

                foreach ( var wave in nextWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.IsWave5OrHigherDegree( cycle ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return -1;
        }

        public long GetNextZigZag( TimeSpan responsibleForWhatTimeFrame,
                                   long startDate )
        {
            if ( _selectedWaveImportanceDict.Count > 0 )
            {
                var ascendingDict = _selectedWaveImportanceDict.OrderBy( hew => hew.Key );

                foreach ( var wave in ascendingDict )
                {
                    long iteratingBarTime = wave.Key;

                    if ( ( iteratingBarTime > startDate ) )
                    {
                        return wave.Key;
                    }
                }
            }

            return ( -1 );
        }

        

        //public bool NoHigherWaveBetween( TimeSpan period, long previousWaveTime,
        //                                 long selectedBarTime,
        //                                 ElliottWaveCycle waveCycle )
        //{
        //    var hews = GetElliottWavesDictionary( period );

        //    if ( hews.Count > 0 )
        //    {
        //        var matches = hews.GetElementsLeftOf( previousWaveTime ).TakeWhile( x => x.Key < selectedBarTime );

        //        //var ascendingDict = _hews.OrderBy( hew => hew.Key );

        //        //var matches = ascendingDict.Where( d => d.Key > previousWaveTime && d.Key < selectedBarTime );

        //        foreach ( var wave in matches )
        //        {
        //            HewLong waveStructure = hew;

        //            if ( waveStructure.HasWaveAboveCycle( waveCycle ) )
        //            {
        //                return false;
        //            }
        //        }
        //    }

        //    return true;
        //}

        public bool NoEqualOrHigherWaveBetween( int waveScenarioNo, long previousWaveTime,
                                         long selectedBarTime,
                                         ElliottWaveCycle waveCycle )
        {
            if ( _hews.Count > 0 )
            {
                var matches = _hews.GetElementsLeftOf( previousWaveTime ).TakeWhile( x => x.Key < selectedBarTime );

                //var ascendingDict = _hews.OrderBy( hew => hew.Key );

                //var matches = ascendingDict.Where( d => d.Key > previousWaveTime && d.Key < selectedBarTime );

                foreach ( var wave in matches )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew.HasWaveEqualOrAboveCycle( waveCycle ) )
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool NoHigherWaveBetween( int waveScenarioNo, TimeSpan period,
                                            long previousWaveTime,
                                            long selectedBarTime,
                                            ElliottWaveCycle waveCycle )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                var matches = hews.GetElementsLeftOf( previousWaveTime ).TakeWhile( x => x.Key < selectedBarTime );
                
                foreach ( var wave in matches )
                {
                    ref HewLong waveStructure = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( waveStructure.HasWaveAboveCycle( waveCycle ) )
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool NoHigherWaveBetween( int waveScenarioNo, long previousWaveTime,
                                         long selectedBarTime,
                                         ElliottWaveCycle waveCycle )
        {
            if ( _hews.Count > 0 )
            {
                var matches = _hews.GetElementsLeftOf( previousWaveTime ).TakeWhile( x => x.Key < selectedBarTime );

                foreach ( var wave in matches )
                {
                    ref HewLong waveStructure = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( waveStructure.HasWaveAboveCycle( waveCycle ) )
                    {
                        return false;
                    }
                }
            }

            return true;
        }



        public void ResetWaveImportance()
        {
            _01minElliottWave = new BTreeDictionary<long, WavePointImportance>();
            _04minElliottWave = new BTreeDictionary<long, WavePointImportance>();
            _05minElliottWave = new BTreeDictionary<long, WavePointImportance>();
            _15minElliottWave = new BTreeDictionary<long, WavePointImportance>();
            _01HourElliottWave = new BTreeDictionary<long, WavePointImportance>();
            _dailyElliottWave = new BTreeDictionary<long, WavePointImportance>();
            _weeklyElliottWave = new BTreeDictionary<long, WavePointImportance>();
            _monthlyElliottWave = new BTreeDictionary<long, WavePointImportance>();
        }

        /*public bool FindOutWhereAreWeInWave( string symbol )
        {
            var output = new PooledDictionary< long, WaveInfo >( );

            var alreadyExist = new PooledDictionary< ElliottWaveCycle, long >( );

            var monthlyWave = GetElliottWavesDictionary( TimeSpan.FromDays( 30 ) );

            var tempWaveInfo = new WaveInfo( );

            long barTime = -1;

            if ( monthlyWave.Count > 0 )
            {
                var matches = monthlyWave.Take( 1 );

                foreach ( var wave in matches )
                {
                    HewLong waveStructure = hew;

                    var highestInfo = waveStructure.GetFirstHighestWaveInfo( );

                    if ( highestInfo.HasValue )
                    {
                        barTime = wave.Key;
                        tempWaveInfo = highestInfo.Value;

                        break;
                    }
                }
            }

            var beginTime2  = barTime.FromLinuxTime( );

            var endTime2    = beginTime2.AddMonths( 1 );

            var bars = GetDatabarsRepository( TimeSpan.FromDays( 1 ) );

            ref SBar bar       =ref bars.GetBarWithWaveInRange( ref tempWaveInfo, beginTime2, endTime2 );


            if ( bar != SBar.EmptySBar )
            {
                var barTime2 = bar.LinuxTime;

                output.Add( barTime2, tempWaveInfo );

                alreadyExist.Add( tempWaveInfo.WaveCycle, barTime2 );

                ElliottWaveCycle oneCycleLower = ElliottWaveCycle.MAX;

                TimeSpan timeFrame = TimeSpan.FromDays( 1 );

                UndoRedoBTreeDictionary< long, DbElliottWave > hews = GetElliottWavesDictionary( timeFrame );

                oneCycleLower = tempWaveInfo.WaveCycle;

                oneCycleLower -= GlobalConstants.OneWaveCycle;

                do
                {
                    var previousWaves = hews.TakeWhile( x => x.Key > barTime2 ).OrderByDescending( x => x.Key );

                    if ( previousWaves.Count() > 0 )
                    {
                        bool found = false;

                        foreach ( var wave in previousWaves )
                        {
                            long iteratingBarTime = wave.Key;

                            if ( ( iteratingBarTime > barTime2 ) )
                            {
                                var hew = hew;

                                var lastWave = hew.GetFirstHighestWaveInfo( );

                                if ( lastWave.HasValue && lastWave.Value.WaveCycle >= oneCycleLower )
                                {
                                    found = true;

                                    if ( !alreadyExist.ContainsKey( lastWave.Value.WaveCycle ) )
                                    {
                                        alreadyExist.Add( lastWave.Value.WaveCycle, iteratingBarTime );
                                        output.Add( iteratingBarTime, lastWave.Value );
                                    }
                                    else
                                    {
                                        var existingTime = alreadyExist[ lastWave.Value.WaveCycle ];

                                        if ( iteratingBarTime > existingTime )
                                        {
                                            output.Remove( existingTime );

                                            if ( !output.ContainsKey( iteratingBarTime ) )
                                            {
                                                output.Add( iteratingBarTime, lastWave.Value );

                                                alreadyExist[ lastWave.Value.WaveCycle ] = iteratingBarTime;
                                            }
                                        }
                                    }


                                    bar = ref bars.GetBarByTime( iteratingBarTime );

                                    

                                    break;
                                }
                            }
                        }

                        if ( !found )
                        {
                            oneCycleLower -= GlobalConstants.OneWaveCycle;
                        }
                    }
                    else
                    {
                        var oldTimeFrame = timeFrame;

                        var aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

                        if ( aa != null )
                        {
                            timeFrame = aa.GetOneTimeSpanLower( timeFrame );
                        }
                        else
                        {
                            return false;
                        }

                        if ( timeFrame != TimeSpan.Zero )
                        {
                            hews = GetElliottWavesDictionary( timeFrame );

                            bars = GetDatabarsRepository( timeFrame );

                            bar = ref bars.GetBarWithWaveInRange( ref tempWaveInfo, bar.BarTime, bar.BarTime + oldTimeFrame );
                        }
                    }
                }
                while ( timeFrame != TimeSpan.Zero );
            }


            return true;
        }*/
    }
}
