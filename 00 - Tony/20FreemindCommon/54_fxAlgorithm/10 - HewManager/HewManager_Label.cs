using System;
using fx.Database.Common.DataModel;
using fx.Database.ForexDatabarsDataModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Linq;
using fx.Definitions;
using fx.Database;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Collections;
using fx.Bars;

namespace fx.Algorithm
{
    public partial class HewManager
    {
        public WaveLabelPosition FindCurrentWaveLabelPosition( int waveScenarioNo, TimeSpan responsibleForWhatTimeFrame,
                                                               long rawBarTime,
                                                               ElliottWaveCycle cycle,
                                                               ElliottWaveEnum currentWaveName,
                                                               ref SBar bar,
                                                               bool isOutsideBar )
        {
            var estWaveImportance = -1;

            if( _selectedWaveImportanceDict != null )
            {
                if( _selectedWaveImportanceDict.ContainsKey( rawBarTime ) )
                {
                    estWaveImportance = _selectedWaveImportanceDict[ rawBarTime ].WaveImportance;
                }
            }

            if( bar.IsWavePeak( ) && estWaveImportance >= 5 )
            {
                return WaveLabelPosition.TOP;
            }
            else
            {
                if( bar.IsWaveTrough( ) && estWaveImportance >= 5 )
                {
                    return WaveLabelPosition.BOTTOM;
                }
            }

            var byWaveCount = WaveLabelPosition.UNKNOWN;
            var byTASignal = WaveLabelPosition.UNKNOWN;

            var dbHewPrev = GetPreviousWaveStructure( waveScenarioNo, rawBarTime );
            var dbHewNext = GetNextWaveStructure( waveScenarioNo, rawBarTime );

            if( dbHewPrev != null && dbHewNext != null ) // We are trying to label wave in Betwen
            {
                byWaveCount = FindLabelPositionBetweenWaves( waveScenarioNo, dbHewPrev, dbHewNext, responsibleForWhatTimeFrame, rawBarTime, cycle, currentWaveName, ref bar, isOutsideBar );
            }
            else if( dbHewPrev != null )
            {
                byWaveCount = FindLabelPositionFromPreviousWave( waveScenarioNo, dbHewPrev, responsibleForWhatTimeFrame, rawBarTime, cycle, currentWaveName, ref bar, isOutsideBar );
            }
            else if( dbHewNext != null )
            {
                byWaveCount = FindLabelPositionFromNextWave( waveScenarioNo, dbHewNext, responsibleForWhatTimeFrame, rawBarTime, cycle, currentWaveName, ref bar, isOutsideBar );
            }

            if( bar.IsWavePeak() || bar.IsGannPeak( ) )
            {
                byTASignal = WaveLabelPosition.TOP;
            }
            else
            {
                if( bar.IsGannTrough( ) || bar.IsGannTrough( ) )
                {
                    byTASignal = WaveLabelPosition.BOTTOM;
                }
            }

            if( byWaveCount == byTASignal )
            {
                return byWaveCount;
            }

            if( isOutsideBar )
            {
                if( byWaveCount != WaveLabelPosition.UNKNOWN )
                {
                    return byWaveCount;
                }
            }

            if( byWaveCount == WaveLabelPosition.UNKNOWN && byTASignal != WaveLabelPosition.UNKNOWN )
            {
                return byTASignal;
            }

            return byWaveCount;
        }

        //public int GetWaveImportance( long rawBarTime )
        //{
        //    if ( _selectedWaveImportanceDict != null )
        //    {
        //        if ( _selectedWaveImportanceDict.ContainsKey( rawBarTime ) )
        //        {
        //            return _selectedWaveImportanceDict [ rawBarTime ].WaveImportance;
        //        }
        //    }

        //    return 0;
        //}

        public int GetWaveImportance( TimeSpan period, long rawBarTime )
        {
            var WaveImportanceDict = GetWaveImportanceDictionary( period );

            if( WaveImportanceDict != null )
            {
                if( WaveImportanceDict.ContainsKey( rawBarTime ) )
                {
                    return WaveImportanceDict[ rawBarTime ].WaveImportance;
                }
            }

            return 0;
        }

        public WavePointImportance GetWaveImportanceExt( TimeSpan period, long rawBarTime )
        {
            var WaveImportanceDict = GetWaveImportanceDictionary( period );

            if( WaveImportanceDict != null )
            {
                if( WaveImportanceDict.ContainsKey( rawBarTime ) )
                {
                    return WaveImportanceDict[ rawBarTime ];
                }
            }

            return null;
        }

        public int GetGannImportance( TimeSpan period, long rawBarTime )
        {
            var WaveImportanceDict = GetGannSwingDictionary( period );

            if( WaveImportanceDict != null )
            {
                if( WaveImportanceDict.ContainsKey( rawBarTime ) )
                {
                    return WaveImportanceDict[ rawBarTime ].WaveImportance;
                }
            }

            return 0;
        }

        public WavePointImportance GetGannImportanceExt( TimeSpan period, long rawBarTime )
        {
            var WaveImportanceDict = GetGannSwingDictionary( period );

            if( WaveImportanceDict != null )
            {
                if( WaveImportanceDict.ContainsKey( rawBarTime ) )
                {
                    return WaveImportanceDict[ rawBarTime ];
                }
            }

            return null;
        }

        protected WaveLabelPosition FindLabelPositionFromNextWave( int waveScenarioNo, DbElliottWave nextWave, TimeSpan responsibleForWhatTimeFrame, long rawBarTime, ElliottWaveCycle cycle, ElliottWaveEnum currentWaveName, ref SBar bar, bool isOutsideBar )
        {
            var output = WaveLabelPosition.UNKNOWN;

            if( nextWave != null ) // We are labeling the latest wave 
            {
                // Finding of the beginning wave should take into consideration of the higher timeframe.                
                long endWaveTime = FindEndWaveOfCurrent( waveScenarioNo, rawBarTime, cycle );

                if( ( endWaveTime ) != -1 )
                {
                    var endWaveInfo = _hews[ endWaveTime ];

                    if( _bars != null )
                    {
                        if( _bars.IsDownTrend( rawBarTime, endWaveInfo.StartDate ) )
                        {
                            output = FindLabelPositionFromNextWaveAtDownTrend( waveScenarioNo, nextWave, rawBarTime, cycle, currentWaveName, ref bar, isOutsideBar );
                        }
                        else if( _bars.IsUpTrend( rawBarTime, endWaveInfo.StartDate ) )
                        {
                            output = FindLabelPositionFromNextWaveAtUpTrend( waveScenarioNo, nextWave, rawBarTime, cycle, currentWaveName, ref bar, isOutsideBar );
                        }
                        else
                        {
                            return WaveLabelPosition.UNKNOWN;
                        }
                    }
                }
            }

            return output;
        }

        private WaveLabelPosition FindLabelPositionFromNextWaveAtDownTrend( int waveScenarioNo, DbElliottWave nextWave, long rawBarTime, ElliottWaveCycle cycle, ElliottWaveEnum currentWaveName, ref SBar bar, bool isOutsideBar )
        {
            var output = WaveLabelPosition.UNKNOWN;

            if( IsWave135( currentWaveName ) )
            {
                return WaveLabelPosition.BOTTOM;
            }
            else if( IsWave24( currentWaveName ) )
            {
                return WaveLabelPosition.TOP;
            }
            else if( IsTriangleWaveABCDE( currentWaveName ) )
            {
                if( currentWaveName == ElliottWaveEnum.WaveTA || currentWaveName == ElliottWaveEnum.WaveTC || currentWaveName == ElliottWaveEnum.WaveTE )
                {
                    return WaveLabelPosition.TOP;
                }
                else if( currentWaveName == ElliottWaveEnum.WaveTB || currentWaveName == ElliottWaveEnum.WaveTD )
                {
                    return WaveLabelPosition.BOTTOM;
                }
            }
            else
            {
                /* --------------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 * Since A,B,C occurs in both impulsive wave and corrective wave, we need to look back to see the beginning wave of current wave.
                 * 
                 * --------------------------------------------------------------------------------------------------------------------------------------------------
                */

                long endWaveTime = FindEndWaveOfCurrentABCX(waveScenarioNo, rawBarTime, cycle );

                if( endWaveTime != -1 )
                {
                    var endWaveInfo = _hews[ endWaveTime ];

                    ref var hew = ref endWaveInfo.GetWaveFromScenario( waveScenarioNo );

                    var highestBeginWave = hew.GetLastHighestWaveDegree( );

                    if( highestBeginWave.HasValue )
                    {
                        if( currentWaveName == ElliottWaveEnum.WaveA || currentWaveName == ElliottWaveEnum.WaveC )
                        {
                            return highestBeginWave.Value.LabelPosition;
                        }
                        else if( currentWaveName == ElliottWaveEnum.WaveB || currentWaveName == ElliottWaveEnum.WaveX )
                        {
                            return highestBeginWave.Value.OppositeLabelDirection( );
                        }
                        else
                        {
                            throw new NotImplementedException( );
                        }
                    }
                }
            }

            return output;

            //var output = WaveLabelPosition.UNKNOWN;

            //if ( IsWave135( currentWaveName ) )
            //{
            //    return WaveLabelPosition.BOTTOM;
            //}
            //else if ( IsWave24( currentWaveName ) )
            //{
            //    return WaveLabelPosition.TOP;
            //}
            //else
            //{
            //    /* --------------------------------------------------------------------------------------------------------------------------------------------------
            //     * 
            //     * Since A,B,C occurs in both impulsive wave and corrective wave, we need to look back to see the beginning wave of current wave.
            //     * 
            //     * --------------------------------------------------------------------------------------------------------------------------------------------------
            //    */

            //    long endWaveTime = FindEndWaveOfCurrent( rawBarTime, cycle );

            //    if ( endWaveTime != -1 )
            //    {
            //        var endWaveInfo = _hews [ endWaveTime ];

            //        var highestEndWave = endWaveInfo.GetElliottWave().GetFirstHighestWaveDegree( );

            //        if ( highestEndWave.HasValue )
            //        {
            //            if ( currentWaveName == ElliottWaveEnum.WaveA || currentWaveName == ElliottWaveEnum.WaveC )
            //            {
            //                return highestEndWave.Value.OppositeLabelDirection( );
            //            }
            //            else if ( currentWaveName == ElliottWaveEnum.WaveB || currentWaveName == ElliottWaveEnum.WaveX )
            //            {
            //                return highestEndWave.Value.LabelPosition;
            //            }
            //            else
            //            {
            //                throw new NotImplementedException( );
            //            }
            //        }

            //    }
            //}

            //return output;
        }

        private WaveLabelPosition FindLabelPositionFromNextWaveAtUpTrend( int waveScenarioNo, DbElliottWave nextWave,
                                                                              long rawBarTime,
                                                                              ElliottWaveCycle cycle,
                                                                              ElliottWaveEnum currentWaveName,
                                                                              ref SBar bar,
                                                                              bool isOutsideBar )
        {
            var output = WaveLabelPosition.UNKNOWN;

            if( IsWave135( currentWaveName ) )
            {
                return WaveLabelPosition.TOP;
            }
            else if( IsWave24( currentWaveName ) )
            {
                return WaveLabelPosition.BOTTOM;
            }
            else if( IsTriangleWaveABCDE( currentWaveName ) )
            {
                if( currentWaveName == ElliottWaveEnum.WaveTA || currentWaveName == ElliottWaveEnum.WaveTC || currentWaveName == ElliottWaveEnum.WaveTE )
                {
                    return WaveLabelPosition.BOTTOM;
                }
                else if( currentWaveName == ElliottWaveEnum.WaveTB || currentWaveName == ElliottWaveEnum.WaveTD )
                {
                    return WaveLabelPosition.TOP;
                }
            }
            else
            {
                /* --------------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 * Since A,B,C occurs in both impulsive wave and corrective wave, we need to look back to see the beginning wave of current wave.
                 * 
                 * --------------------------------------------------------------------------------------------------------------------------------------------------
                */

                long endWaveTime = FindEndWaveOfCurrentABCX( waveScenarioNo,rawBarTime, cycle );

                if( endWaveTime != -1 )
                {
                    var endWaveInfo = _hews[ endWaveTime ];

                    ref var hew = ref endWaveInfo.GetWaveFromScenario( waveScenarioNo );

                    var highestBeginWave = hew.GetLastHighestWaveDegree( );

                    if( highestBeginWave.HasValue )
                    {
                        if( currentWaveName == ElliottWaveEnum.WaveA || currentWaveName == ElliottWaveEnum.WaveC )
                        {
                            return highestBeginWave.Value.LabelPosition;
                        }
                        else if( currentWaveName == ElliottWaveEnum.WaveB || currentWaveName == ElliottWaveEnum.WaveX )
                        {
                            return highestBeginWave.Value.OppositeLabelDirection( );
                        }
                        else
                        {
                            throw new NotImplementedException( );
                        }
                    }
                }
            }

            return output;

            //var output = WaveLabelPosition.UNKNOWN;

            //if ( IsWave135( currentWaveName ) )
            //{
            //    return WaveLabelPosition.TOP;
            //}
            //else if ( IsWave24( currentWaveName ) )
            //{
            //    return WaveLabelPosition.BOTTOM;
            //}
            //else
            //{
            //    /* --------------------------------------------------------------------------------------------------------------------------------------------------
            //     * 
            //     * Since A,B,C occurs in both impulsive wave and corrective wave, we need to look back to see the beginning wave of current wave.
            //     * 
            //     * --------------------------------------------------------------------------------------------------------------------------------------------------
            //    */

            //    var beginWaveTime = FindBeginningWaveOfCurrentABCX( rawBarTime, cycle );

            //    if ( beginWaveTime != -1 )
            //    {
            //        var beginWaveInfo = _hews [ beginWaveTime ];

            //        var highestBeginWave = beginWaveInfo.GetElliottWave().GetLastHighestWaveDegree( );

            //        if ( highestBeginWave.HasValue )
            //        {
            //            if ( currentWaveName == ElliottWaveEnum.WaveA || currentWaveName == ElliottWaveEnum.WaveC )
            //            {
            //                return highestBeginWave.Value.OppositeLabelDirection( );
            //            }
            //            else if ( currentWaveName == ElliottWaveEnum.WaveB || currentWaveName == ElliottWaveEnum.WaveX )
            //            {
            //                return highestBeginWave.Value.LabelPosition;
            //            }
            //            else
            //            {
            //                throw new NotImplementedException( );
            //            }
            //        }

            //    }

            //    long endWaveTime       = FindEndWaveOfCurrent( rawBarTime, cycle );

            //    if ( endWaveTime != -1 )
            //    {
            //        var endWaveInfo    = _hews [ endWaveTime ];

            //        var highestEndWave = endWaveInfo.GetElliottWave().GetFirstHighestWaveDegree( );

            //        if ( highestEndWave.HasValue )
            //        {
            //            if ( currentWaveName == ElliottWaveEnum.WaveA || currentWaveName == ElliottWaveEnum.WaveC )
            //            {
            //                return highestEndWave.Value.LabelPosition;

            //                
            //            }
            //            else if ( currentWaveName == ElliottWaveEnum.WaveB || currentWaveName == ElliottWaveEnum.WaveX )
            //            {
            //                return highestEndWave.Value.OppositeLabelDirection( );
            //            }
            //            else
            //            {
            //                throw new NotImplementedException( );
            //            }
            //        }

            //    }
            //}

            //return output;
        }

        protected WaveLabelPosition FindLabelPositionFromPreviousWave( int waveScenarioNo, DbElliottWave previousWave,
                                                                       TimeSpan responsibleForWhatTimeFrame,
                                                                       long rawBarTime,
                                                                       ElliottWaveCycle cycle,
                                                                       ElliottWaveEnum currentWaveName,
                                                                       ref SBar bar,
                                                                       bool isOutsideBar )
        {
            var output = WaveLabelPosition.UNKNOWN;

            if( previousWave != null ) // We are labeling the latest wave 
            {
                // Finding of the beginning wave should take into consideration of the higher timeframe.                
                long beginWaveTime = FindBeginningWaveTimeOfCurrentCycle( waveScenarioNo, rawBarTime, cycle );

                if( ( beginWaveTime ) != -1 )
                {
                    var beginWaveInfo = _hews[ beginWaveTime ];

                    if( _bars != null )
                    {
                        if( _bars.IsDownTrend( beginWaveInfo.StartDate, rawBarTime ) )
                        {
                            output = FindLabelPositionFromPreviousWaveAtDownTrend( waveScenarioNo, previousWave, rawBarTime, cycle, currentWaveName, ref bar, isOutsideBar );
                        }
                        else if( _bars.IsUpTrend( beginWaveInfo.StartDate, rawBarTime ) )
                        {
                            output = FindLabelPositionFromPreviousWaveAtUpTrend( waveScenarioNo, previousWave, rawBarTime, cycle, currentWaveName, ref bar, isOutsideBar );
                        }
                        else
                        {
                            return WaveLabelPosition.UNKNOWN;
                        }
                    }
                }
            }

            return output;
        }

        private WaveLabelPosition FindLabelPositionFromPreviousWaveAtDownTrend( int waveScenarioNo, DbElliottWave previousWave,
                                                                                long rawBarTime,
                                                                                ElliottWaveCycle cycle,
                                                                                ElliottWaveEnum currentWaveName,
                                                                                ref SBar bar,
                                                                                bool isOutsideBar )
        {
            var output = WaveLabelPosition.UNKNOWN;

            if( IsWave135( currentWaveName ) )
            {
                return WaveLabelPosition.BOTTOM;
            }
            else if( IsWave24( currentWaveName ) )
            {
                return WaveLabelPosition.TOP;
            }
            else if( IsTriangleWaveABCDE( currentWaveName ) )
            {
                if( currentWaveName == ElliottWaveEnum.WaveTA || currentWaveName == ElliottWaveEnum.WaveTC || currentWaveName == ElliottWaveEnum.WaveTE )
                {
                    return WaveLabelPosition.TOP;
                }
                else if( currentWaveName == ElliottWaveEnum.WaveTB || currentWaveName == ElliottWaveEnum.WaveTD )
                {
                    return WaveLabelPosition.BOTTOM;
                }
            }
            else
            {
                /* --------------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 * Since A,B,C occurs in both impulsive wave and corrective wave, we need to look back to see the beginning wave of current wave.
                 * 
                 * --------------------------------------------------------------------------------------------------------------------------------------------------
                */

                long beginWaveTime = FindBeginningWaveOfCurrentABCX(waveScenarioNo, rawBarTime, cycle );

                if( beginWaveTime != -1 )
                {
                    var beginWaveInfo = _hews[ beginWaveTime ];

                    ref var hew = ref beginWaveInfo.GetWaveFromScenario( waveScenarioNo );

                    var highestBeginWave = hew.GetLastHighestWaveDegree( );

                    if( highestBeginWave.HasValue )
                    {
                        if( currentWaveName == ElliottWaveEnum.WaveA || currentWaveName == ElliottWaveEnum.WaveC )
                        {
                            return highestBeginWave.Value.OppositeLabelDirection( );
                        }
                        else if( currentWaveName == ElliottWaveEnum.WaveB || currentWaveName == ElliottWaveEnum.WaveX )
                        {
                            return highestBeginWave.Value.LabelPosition;
                        }
                        else
                        {
                            throw new NotImplementedException( );
                        }
                    }
                }
            }

            return output;
        }

        private WaveLabelPosition FindLabelPositionFromPreviousWaveAtUpTrend( int waveScenarioNo, DbElliottWave previousWave,
                                                                              long rawBarTime,
                                                                              ElliottWaveCycle cycle,
                                                                              ElliottWaveEnum currentWaveName,
                                                                              ref SBar bar,
                                                                              bool isOutsideBar )
        {
            var output = WaveLabelPosition.UNKNOWN;

            if( IsWave135( currentWaveName ) )
            {
                return WaveLabelPosition.TOP;
            }
            else if( IsWave24( currentWaveName ) )
            {
                return WaveLabelPosition.BOTTOM;
            }
            else if( IsTriangleWaveABCDE( currentWaveName ) )
            {
                ref var hew = ref previousWave.GetWaveFromScenario( waveScenarioNo );
                var waveInfo = hew.GetFirstHighestWaveInfo( );

                if( ( waveInfo.Value.WaveName == ElliottWaveEnum.WaveTA || waveInfo.Value.WaveName == ElliottWaveEnum.WaveTC || waveInfo.Value.WaveName == ElliottWaveEnum.WaveTE ) && ( ( currentWaveName == ElliottWaveEnum.WaveTB ) || ( currentWaveName == ElliottWaveEnum.WaveTD ) ) )
                {
                    return ( waveInfo.Value.LabelPosition == WaveLabelPosition.TOP ) ? WaveLabelPosition.BOTTOM : WaveLabelPosition.TOP;
                }
                else if( ( waveInfo.Value.WaveName == ElliottWaveEnum.WaveTB || waveInfo.Value.WaveName == ElliottWaveEnum.WaveTD ) && ( currentWaveName == ElliottWaveEnum.WaveTA || currentWaveName == ElliottWaveEnum.WaveTC || currentWaveName == ElliottWaveEnum.WaveTE ) )
                {
                    return ( waveInfo.Value.LabelPosition == WaveLabelPosition.TOP ) ? WaveLabelPosition.BOTTOM : WaveLabelPosition.TOP;
                }
            }
            else
            {
                /* --------------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 * Since A,B,C occurs in both impulsive wave and corrective wave, we need to look back to see the beginning wave of current wave.
                 * 
                 * --------------------------------------------------------------------------------------------------------------------------------------------------
                */

                long beginWaveTime = FindBeginningWaveOfCurrentABCX(waveScenarioNo, rawBarTime, cycle );

                if( beginWaveTime != -1 )
                {
                    var beginWaveInfo = _hews[ beginWaveTime ];

                    ref var hew = ref beginWaveInfo.GetWaveFromScenario( waveScenarioNo );

                    var highestBeginWave = hew.GetLastHighestWaveDegree( );

                    if( highestBeginWave.HasValue )
                    {
                        if( currentWaveName == ElliottWaveEnum.WaveA || currentWaveName == ElliottWaveEnum.WaveC )
                        {
                            return highestBeginWave.Value.OppositeLabelDirection( );
                        }
                        else if( currentWaveName == ElliottWaveEnum.WaveB || currentWaveName == ElliottWaveEnum.WaveX )
                        {
                            return highestBeginWave.Value.LabelPosition;
                        }
                        else
                        {
                            throw new NotImplementedException( );
                        }
                    }
                }
            }

            return output;
        }

        public WaveLabelPosition GetLabelPositionsAtTrend( TimeSpan responsibleForWhatTimeFrame,
                                                           long currentBarTime,
                                                           ElliottWaveEnum currentWaveName,
                                                           ElliottWaveCycle cycle,
                                                           long previousWaveBarTime,
                                                           long nextWaveBarTime )
        {
            var output = WaveLabelPosition.UNKNOWN;

            if( _bars != null )
            {
                //if ( _bars.IsOutsideBar( previousWaveBarTime, nextWaveBarTime ) )
                //{
                //    output = GetLabelPositionsForOutsideBar( responsibleForWhatTimeFrame, currentBarTime, currentWaveName, cycle, previousWaveBarTime, nextWaveBarTime );
                //}
                //else 

                if( _bars.IsDownTrend( previousWaveBarTime, nextWaveBarTime ) )
                {
                    output = GetLabelPositionsAtDownTrend( currentWaveName );
                }
                else if( _bars.IsUpTrend( previousWaveBarTime, nextWaveBarTime ) )
                {
                    output = GetLabelPositionsAtUpTrend( currentWaveName );
                }
            }

            return output;
        }

        public WaveLabelPosition GetLabelPositionsForOutsideBar( int waveScenarioNo, TimeSpan responsibleForWhatTimeFrame,
                                                                 long currentBarTime,
                                                                 ElliottWaveEnum currentWaveName,
                                                                 ElliottWaveCycle cycle,
                                                                 long previousWaveBarTime,
                                                                 long outsideBarTime )
        {
            var output = WaveLabelPosition.UNKNOWN;

            var beginWaveTime = FindBeginningWaveTimeOfCurrentCycle(waveScenarioNo, currentBarTime, cycle );

            if( beginWaveTime > -1 )
            {
                if( _bars.IsDownTrend( beginWaveTime, currentBarTime ) )
                {
                    output = GetLabelPositionsAtDownTrend( currentWaveName );
                }
                else if( _bars.IsUpTrend( beginWaveTime, currentBarTime ) )
                {
                    output = GetLabelPositionsAtUpTrend( currentWaveName );
                }
            }

            return output;
        }

        public WaveLabelPosition GetLabelPositionsAtUpTrend( ElliottWaveEnum currentWaveName )
        {
            if( IsMainTrend( currentWaveName ) )
            {
                return WaveLabelPosition.TOP;
            }
            else
            {
                return WaveLabelPosition.BOTTOM;
            }
        }

        public WaveLabelPosition GetLabelPositionsAtDownTrend( ElliottWaveEnum currentWaveName )
        {
            if( IsMainTrend( currentWaveName ) )
            {
                return WaveLabelPosition.BOTTOM;
            }
            else
            {
                return WaveLabelPosition.TOP;
            }
        }

        public bool IsMainTrend( ElliottWaveEnum currentWaveName )
        {
            if( currentWaveName == ElliottWaveEnum.Wave1 || currentWaveName == ElliottWaveEnum.Wave1C || currentWaveName == ElliottWaveEnum.Wave3 || currentWaveName == ElliottWaveEnum.Wave3C || currentWaveName == ElliottWaveEnum.Wave5 || currentWaveName == ElliottWaveEnum.Wave5C || currentWaveName == ElliottWaveEnum.WaveA || currentWaveName == ElliottWaveEnum.WaveC )
            {
                return true;
            }

            return false;
        }

        public bool IsWave135( ElliottWaveEnum currentWaveName )
        {
            if( currentWaveName == ElliottWaveEnum.Wave1 || currentWaveName == ElliottWaveEnum.Wave1C || currentWaveName == ElliottWaveEnum.Wave3 || currentWaveName == ElliottWaveEnum.Wave3C || currentWaveName == ElliottWaveEnum.Wave5 || currentWaveName == ElliottWaveEnum.Wave5C )
            {
                return true;
            }

            return false;
        }

        public bool IsWave24( ElliottWaveEnum currentWaveName )
        {
            if( currentWaveName == ElliottWaveEnum.Wave2 || currentWaveName == ElliottWaveEnum.Wave4 )
            {
                return true;
            }

            return false;
        }

        public bool IsTriangleWaveABCDE( ElliottWaveEnum currentWaveName )
        {
            if( currentWaveName == ElliottWaveEnum.WaveTA || currentWaveName == ElliottWaveEnum.WaveTB || currentWaveName == ElliottWaveEnum.WaveTC || currentWaveName == ElliottWaveEnum.WaveTD || currentWaveName == ElliottWaveEnum.WaveTE )
            {
                return true;
            }

            return false;
        }

        public bool IsCounterTrend( ElliottWaveEnum currentWaveName )
        {
            if( currentWaveName == ElliottWaveEnum.Wave2 || currentWaveName == ElliottWaveEnum.Wave4 || currentWaveName == ElliottWaveEnum.WaveB || currentWaveName == ElliottWaveEnum.WaveX )
            {
                return true;
            }

            return false;
        }

        public WaveLabelPosition FindLabelPositionBetweenWaves(int waveScenarioNo , DbElliottWave previousWave,
                                                                DbElliottWave nextWave,
                                                                TimeSpan responsibleForWhatTimeFrame,
                                                                long rawBarTime,
                                                                ElliottWaveCycle cycle,
                                                                ElliottWaveEnum currentWaveName,
                                                                ref SBar bar,
                                                                bool isOutsideBar )
        {
            if( previousWave == null || nextWave == null )
            {
                throw new NotImplementedException( );
            }

            var outputFromPrevious = WaveLabelPosition.UNKNOWN;
            var outputFromNext = WaveLabelPosition.UNKNOWN;

            long beginWaveTime = FindBeginningWaveTimeOfCurrentCycle( waveScenarioNo, rawBarTime, cycle );

            if( ( beginWaveTime ) != -1 )
            {
                outputFromPrevious = FindLabelPositionFromPreviousWave( waveScenarioNo, previousWave, responsibleForWhatTimeFrame, rawBarTime, cycle, currentWaveName, ref bar, isOutsideBar );
            }

            long endWaveTime = FindEndWaveOfCurrent( waveScenarioNo,rawBarTime, cycle );

            if( ( endWaveTime ) != -1 )
            {
                outputFromNext = FindLabelPositionFromNextWave( waveScenarioNo, previousWave, responsibleForWhatTimeFrame, rawBarTime, cycle, currentWaveName, ref bar, isOutsideBar );
            }

            if( outputFromNext == outputFromPrevious )
            {
                return outputFromNext;
            }

            var byTASignal = WaveLabelPosition.UNKNOWN;

            if( bar.IsWavePeak( ) || bar.IsGannPeak( ) )
            {
                byTASignal = WaveLabelPosition.TOP;
            }
            else if( bar.IsGannTrough( ) || bar.IsWavePeak( ) )
            {
                byTASignal = WaveLabelPosition.BOTTOM;
            }

            if( outputFromPrevious != WaveLabelPosition.UNKNOWN && outputFromNext == WaveLabelPosition.UNKNOWN )
            {
                if( outputFromPrevious == byTASignal || byTASignal == WaveLabelPosition.UNKNOWN )
                {
                    return outputFromPrevious;
                }
            }
            else if( outputFromPrevious == WaveLabelPosition.UNKNOWN && outputFromNext != WaveLabelPosition.UNKNOWN )
            {
                if( outputFromNext == byTASignal || byTASignal == WaveLabelPosition.UNKNOWN )
                {
                    return outputFromNext;
                }
            }

            return ( outputFromPrevious );
        }
    }
}
