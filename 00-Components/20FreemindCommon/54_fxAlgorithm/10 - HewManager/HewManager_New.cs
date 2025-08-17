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
using fx.Definitions.UndoRedo;
using fx.Collections;
using fx.Bars;

namespace fx.Algorithm
{
    public partial class HewManager
    {
        #region 00 - Special Waves

        public void AddIntelliWave( int waveScenarioNo,
                                    TimeSpan responsibleForWhatTimeFrame,
                                    long selectedBarTime,
                                    ElliottWaveCycle waveCycle,
                                    ElliottWaveEnum waveNameToBeAdded,
                                    ref SBar bar,
                                    bool isSpecialBar,
                                    bool isOutsideBar )
        {
            var waveCount = WaveCount(waveScenarioNo, selectedBarTime );

            if ( waveCount == 0 )
            {
                if ( isOutsideBar )
                {
                    AddWaveToOutsideBar( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, waveCycle, waveNameToBeAdded, ref bar, isSpecialBar, isOutsideBar );
                }
                else
                {
                    AddSingleWave( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, waveCycle, waveNameToBeAdded, ref bar, isSpecialBar, isOutsideBar );
                }
            }
            else
            {
                if ( waveCount > 0 )
                {
                    if ( bar.IsSpecialBar || isSpecialBar )
                    {
                        IntelliAddWaveToSpecialBar( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, waveCycle, waveNameToBeAdded, ref bar, isOutsideBar );
                    }
                    else
                    {
                        IntelliAddWaveToCurrentBar( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, waveCycle, waveNameToBeAdded, ref bar, isSpecialBar, isOutsideBar );
                    }
                }
            }
        }

        private void AddSingleWave(
                                    int waveScenarioNo,
                                    TimeSpan responsibleForWhatTimeFrame,
                                    long selectedBarTime,
                                    ElliottWaveCycle waveCycle,
                                    ElliottWaveEnum waveNameToBeAdded,
                                    ref SBar bar,
                                    bool isSpecialBar,
                                    bool isOutsideBar )
        {
            switch ( waveNameToBeAdded )
            {
                case ElliottWaveEnum.WaveA:
                    AddWaveA( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, waveCycle, waveNameToBeAdded, ref bar, isOutsideBar );
                    break;

                case ElliottWaveEnum.WaveB:
                    AddWaveB( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, waveCycle, waveNameToBeAdded, ref bar, isOutsideBar );
                    break;

                case ElliottWaveEnum.WaveC:
                    AddWaveC( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, waveCycle, waveNameToBeAdded, ref bar, isOutsideBar );
                    break;

                default:
                    InternalAddSingleWave( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, waveCycle, waveNameToBeAdded, ref bar, isOutsideBar );
                    break;
            }
        }

        public bool AddWaveA( int waveScenarioNo,
                                TimeSpan responsibleForWhatTimeFrame,
                              long selectedBarTime,
                              ElliottWaveCycle waveCycle,
                              ElliottWaveEnum elliottWave,
                              ref SBar bar,
                              bool isOutsideBar )
        {
            var dbHewPrev = GetPreviousWaveStructure(waveScenarioNo, selectedBarTime );

            var inBetweenZigZag = FindImportantWavePointsBetween( responsibleForWhatTimeFrame, dbHewPrev.StartDate, selectedBarTime );

            if ( inBetweenZigZag.Count == 0 )
            {
                if ( dbHewPrev != null )
                {
                    ref var hew = ref dbHewPrev.GetWaveFromScenario( waveScenarioNo );
                    var previousWave = hew.GetWavesAtCycle( waveCycle );

                    if ( previousWave.Count > 0 )
                    {
                        if ( previousWave[ previousWave.Count - 1 ] == ElliottWaveEnum.Wave1 || previousWave[ previousWave.Count - 1 ] == ElliottWaveEnum.Wave1C || previousWave[ previousWave.Count - 1 ] == ElliottWaveEnum.Wave3 || previousWave[ previousWave.Count - 1 ] == ElliottWaveEnum.Wave3C ) // If we are labeling Wave 2 or Wave 4, it needs to automatically lower by one degress
                        {
                            ElliottWaveCycle newWaveCycle = waveCycle - GlobalConstants.OneWaveCycle;

                            if ( newWaveCycle >= ElliottWaveCycle.Miniscule )
                            {
                                InternalAddSingleWave( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, waveCycle, elliottWave, ref bar, isOutsideBar );

                                return true;
                            }
                        }
                    }
                }
            }

            InternalAddSingleWave( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, waveCycle, elliottWave, ref bar, isOutsideBar );

            return true;
        }

        public bool AddWaveB( int waveScenarioNo,
                              TimeSpan responsibleForWhatTimeFrame,
                              long selectedBarTime,
                              ElliottWaveCycle waveCycle,
                              ElliottWaveEnum elliottWave,
                              ref SBar bar,
                              bool isOutsideBar )
        {
            var dbHewPrev = GetPreviousWaveStructure( waveScenarioNo, selectedBarTime );

            var inBetweenZigZag = FindImportantWavePointsBetween( responsibleForWhatTimeFrame, dbHewPrev.StartDate, selectedBarTime );

            if ( inBetweenZigZag.Count == 0 )
            {
                ref var hew = ref dbHewPrev.GetWaveFromScenario( waveScenarioNo );
                var previousWaves = hew.GetAllWaves( );

                foreach ( var previousWave in previousWaves )
                {
                    if ( previousWave.WaveName == ElliottWaveEnum.WaveA ) // If we are labeling Wave 2 or Wave 4, it needs to automatically lower by one degress
                    {
                        ElliottWaveCycle newWaveCycle = previousWave.WaveCycle;

                        InternalAddSingleWave( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, waveCycle, elliottWave, ref bar, isOutsideBar );
                    }
                }
            }

            InternalAddSingleWave( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, waveCycle, elliottWave, ref bar, isOutsideBar );

            return true;
        }

        public bool AddWaveC( int waveScenarioNo,
                                TimeSpan responsibleForWhatTimeFrame,
                              long selectedBarTime,
                              ElliottWaveCycle waveCycle,
                              ElliottWaveEnum elliottWave,
                              ref SBar bar,
                              bool isOutsideBar )
        {
            var allWave = GetAllWavesDescending( waveScenarioNo, selectedBarTime );

            if ( allWave != null )
            {
                var dbHewPrev = GetPreviousWaveStructure( waveScenarioNo, selectedBarTime );

                var nextWave = ElliottWaveEnum.NONE;

                if ( dbHewPrev != null ) // This means we are dealing with the ABC of a larger timeframe
                {
                    var WaveCCycle = waveCycle; // We are adding Wave C of a lower degree.

                    ref var hew = ref dbHewPrev.GetWaveFromScenario( waveScenarioNo );

                    var previousWave = hew.GetWavesAtCycle( WaveCCycle );

                    if ( previousWave.Count == 0 && dbHewPrev.HighestWaveCycle != WaveCCycle )
                    {
                        var higherWave = hew.GetWavesAtCycle( dbHewPrev.HighestWaveCycle );

                        if ( higherWave.Count > 0 && higherWave[ 0 ] == ElliottWaveEnum.WaveB )
                        {
                            WaveCCycle = dbHewPrev.HighestWaveCycle;
                            previousWave = hew.GetWavesAtCycle( WaveCCycle );
                        }
                    }

                    if ( previousWave.Count > 0 && previousWave[ previousWave.Count - 1 ] == ElliottWaveEnum.WaveB )
                    {
                        InternalAddWaveC( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, WaveCCycle, ref bar );
                    }
                }
                else
                {
                    /*  
                        *  -----------------------------------------------------------------------------------------------------------------------------
                        *      If we end up getting here, we could be 
                        *      1) We are initially defining the wave, so there is no previous wave.
                        *      2) We have labeled an afterward wave, so there is also no previous wave.
                        *  -----------------------------------------------------------------------------------------------------------------------------
                    */

                    nextWave = ElliottWaveEnum.WaveC;

                    InternalAddSingleWave( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, waveCycle, nextWave, ref bar, isOutsideBar );
                }
            }

            return true;
        }

        public void InternalAddSingleWave( int waveScenarioNo,
                                            TimeSpan responsibleForWhatTimeFrame,
                                            long rawBarTime,
                                            ElliottWaveCycle cycle,
                                            ElliottWaveEnum currentWaveName,
                                            ref SBar bar,
                                            bool isOutsideBar )
        {
            if ( _hews.ContainsKey( rawBarTime ) )
            {
                throw new InvalidOperationException();
            }

            var posCycle = GetCycleAndLabelPosition( waveScenarioNo, responsibleForWhatTimeFrame, rawBarTime, cycle, currentWaveName, ref bar, isOutsideBar );
        }

        public WaveInfo GetCycleAndLabelPosition( int waveScenarioNo, TimeSpan responsibleForWhatTimeFrame,
                                                      long selectedBarTime,
                                                      ElliottWaveCycle cycle,
                                                      ElliottWaveEnum currentWaveName,
                                                      ref SBar bar,
                                                      bool isOutsideBar )
        {
            var output = new WaveInfo( ElliottWaveCycle.UNKNOWN, ElliottWaveEnum.NONE, WaveLabelPosition.UNKNOWN );
            var dbHewPrev = GetPreviousWaveStructure( waveScenarioNo, selectedBarTime );
            var dbHewNext = GetNextWaveStructure( waveScenarioNo, selectedBarTime );

            if ( dbHewPrev != null && dbHewNext != null ) // We are trying to label wave in Betwen
            {
                var tempOut = harmonicEWave.GetCycleAndLabelPositionBetweenWaves( waveScenarioNo, dbHewPrev, dbHewNext, cycle, currentWaveName );

                if ( tempOut.HasValue )
                {
                    output = tempOut.Value;
                }
            }
            else if ( dbHewPrev != null )
            {
                output = GetCycleAndLabelPositionFromPreviousWave( dbHewPrev, responsibleForWhatTimeFrame, selectedBarTime, cycle, currentWaveName, ref bar, isOutsideBar );
            }
            else if ( dbHewNext != null )
            {
                output = GetCycleAndLabelPositionFromNextWave( dbHewNext, responsibleForWhatTimeFrame, selectedBarTime, cycle, currentWaveName, ref bar );
            }

            return output;
        }

        //public DbElliottWave DeleteLargestWavesFromManagerAndBar( int waveScenarioNo,
        //                                                            long rawBarTime,
        //                                                            ref SBar bar,
        //                                                            TimeSpan period )
        //{
        //    DbElliottWave output = null;

        //    if ( _hews.ContainsKey( rawBarTime ) )
        //    {
        //        output = _hews[ rawBarTime ];

        //        ref var hew = ref output.GetWaveFromScenario( waveScenarioNo );

        //        var largestWave = hew.GetFirstHighestWaveInfo( );

        //        var largestValue = largestWave.Value;

        //        DeleteWavesDownSmallerTimeFrame( waveScenarioNo, ref largestValue, rawBarTime.FromLinuxTime(), period );

        //        DeleteWavesUpHigherTimeFrame( waveScenarioNo, ref largestValue, rawBarTime.FromLinuxTime(), period );

        //        bar.RemoveWavesFromDatabar( waveScenarioNo, largestWave.Value.ElliottWave );

        //        if ( _selectedRemovedWavesList != null )
        //        {
        //            var index = _selectedRemovedWavesList.FindIndex( x => x.Equals( rawBarTime ) );

        //            if ( index == -1 )
        //            {
        //                _selectedRemovedWavesList.Add( rawBarTime );
        //            }

        //            if ( hew.Count == 0 )
        //            {
        //                _hews.Remove( rawBarTime );
        //                Messenger.Default.Send( new ToggleCommandMessage( CommandMessages.HasHewFunctions, false ) );
        //            }
        //        }
        //    }

        //    return output;
        //}

        public ElliottWaveCycle SmartAddWaveXtoManagerAndBar( int waveScenarioNo,
                                                                TimeSpan responsibleForWhatTimeFrame,
                                                                long rawBarTime,
                                                                ElliottWaveCycle cycle,
                                                                ElliottWaveEnum currentWaveName,
                                                                ref SBar bar )
        {
            var changedWavedCycle = cycle;
            var tobeSynced        = new WaveInfo( );

            tobeSynced.WaveCycle = cycle;
            tobeSynced.WaveName = currentWaveName;

            DbElliottWave dbHew = null;

            if ( _hews.ContainsKey( rawBarTime ) )
            {
                dbHew = _hews[ rawBarTime ];

                tobeSynced.LabelPosition = dbHew.WaveLabelPosition;

                ref var hew = ref dbHew.GetWaveFromScenario( waveScenarioNo );

                if ( hew.HasElliottWave )
                {
                    PooledList < ElliottWaveEnum > waveAtCycle =  hew.GetWavesAtCycle( cycle ); ;

                    if ( waveAtCycle.Count > 0 )
                    {
                        if ( waveAtCycle[ 0 ] == ElliottWaveEnum.WaveC )
                        {
                            switch ( currentWaveName )
                            {
                                case ElliottWaveEnum.Wave1:
                                {
                                    tobeSynced.WaveName = ElliottWaveEnum.Wave1C;
                                    hew.AddHarmonicElliottWave( cycle, ElliottWaveEnum.Wave1C, dbHew.WaveLabelPosition );
                                }
                                break;

                                case ElliottWaveEnum.Wave3:
                                {
                                    tobeSynced.WaveName = ElliottWaveEnum.Wave3C;
                                    hew.AddHarmonicElliottWave( cycle, ElliottWaveEnum.Wave3C, dbHew.WaveLabelPosition );
                                }
                                break;

                                case ElliottWaveEnum.Wave5:
                                {
                                    tobeSynced.WaveName = ElliottWaveEnum.Wave5C;
                                    hew.AddHarmonicElliottWave( cycle, ElliottWaveEnum.Wave5C, dbHew.WaveLabelPosition );
                                }
                                break;

                                // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                                //
                                // If we are adding a wave on top of Wave C, then it might be wave of higher degree. We need to check the previous wave of higher degree.
                                //
                                // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                                default:
                                {
                                    hew.AddHarmonicElliottWave( cycle, currentWaveName, dbHew.WaveLabelPosition );
                                }
                                break;
                            }
                        }
                        else
                        {
                            hew.AddHarmonicElliottWave( cycle, currentWaveName, dbHew.WaveLabelPosition );
                        }
                    }
                    else
                    {
                        hew.AddHarmonicElliottWave( cycle, currentWaveName, dbHew.WaveLabelPosition );
                    }

                    bar.AddWave( waveScenarioNo, ref hew );

                    bar.WaveDirty = WaveDirtyEnum.Add;

                    _hews[ rawBarTime ].SyncWithBar( ref bar );

                    WavePriceTimeInfo output2 = default;

                    if ( CalculateWavePriceTimeInfo( waveScenarioNo, _bars, FinancialHelper.GetTimeSpanFromString( dbHew.Period ), dbHew.StartDate, currentWaveName, cycle, dbHew.WaveLabelPosition, ref output2 ) )
                    {
                        ref var ptInfo = ref bar.GetPriceTimeFromScenario( waveScenarioNo );
                        ptInfo = output2;
                    }

                    return changedWavedCycle;
                }
            }

            /* -----------------------------------------------------------------------------------------
             * 
             * If we get to the following code, we either don't have any waves at this bar or 
             * We don't have wave at this wave Scenario
             * 
             * -----------------------------------------------------------------------------------------
             */

            // We have to find the cycle first before we find 
            changedWavedCycle = FindCurrentWaveCycle( waveScenarioNo, responsibleForWhatTimeFrame, rawBarTime, cycle, currentWaveName, ref bar );
            var labelPosition = FindCurrentWaveLabelPosition( waveScenarioNo, responsibleForWhatTimeFrame, rawBarTime, changedWavedCycle, currentWaveName, ref bar, _bars.IsOutsideBar( rawBarTime ) );

            if ( labelPosition == WaveLabelPosition.UNKNOWN )
            {
                labelPosition = GetWaveLabelPositionByTASignal( ref bar );
            }

            if ( labelPosition != WaveLabelPosition.UNKNOWN )
            {
                tobeSynced.WaveCycle = changedWavedCycle;
                tobeSynced.WaveName = currentWaveName;
                tobeSynced.LabelPosition = labelPosition;

                var newWave = new HewLong( changedWavedCycle, currentWaveName, labelPosition );

                if ( currentWaveName == ElliottWaveEnum.Wave5C )
                {
                    var lastHigherWave = GetPreviousWaveStructureOfDegree( waveScenarioNo, rawBarTime, changedWavedCycle + GlobalConstants.OneWaveCycle );

                    var inBetweenZigZag = FindImportantWavePointsBetween( responsibleForWhatTimeFrame, lastHigherWave.StartDate, rawBarTime );

                    if ( inBetweenZigZag.Count == 0 )
                    {
                        var lastHigherWaveName = GetPreviousWaveOfDegree( waveScenarioNo, rawBarTime, changedWavedCycle + GlobalConstants.OneWaveCycle );

                        var higerNameOfCurrentWave = harmonicEWave.GetNextWave( lastHigherWaveName );

                        if ( higerNameOfCurrentWave != ElliottWaveEnum.NONE )
                        {
                            changedWavedCycle = changedWavedCycle + GlobalConstants.OneWaveCycle;

                            tobeSynced.WaveName = higerNameOfCurrentWave;
                            tobeSynced.WaveCycle = changedWavedCycle;

                            newWave.AddHarmonicElliottWave( changedWavedCycle, higerNameOfCurrentWave, labelPosition );
                        }
                    }
                }

                bar.AddWave( waveScenarioNo, ref newWave );

                if ( dbHew == null )
                {
                    dbHew = new DbElliottWave( _offerId, _bars, ref bar );
                    _hews.Add( rawBarTime, dbHew );
                }
                else
                {
                    dbHew.SyncWithBar( ref bar );
                }


                if ( _selectedRemovedWavesList != null )
                {
                    var index = _selectedRemovedWavesList.FindIndex( x => x.Equals( rawBarTime ) );

                    if ( index > -1 )
                    {
                        _selectedRemovedWavesList.Remove( rawBarTime );
                    }
                }

                Messenger.Default.Send( new ToggleCommandMessage( CommandMessages.HasHewFunctions, true ) );
            }

            WavePriceTimeInfo output = default;

            if ( CalculateWavePriceTimeInfo( waveScenarioNo, _bars, bar.BarPeriod, rawBarTime, currentWaveName, changedWavedCycle, labelPosition, ref output ) )
            {
                bar.AddPriceTimeInfo( waveScenarioNo, ref output );

                changedWavedCycle = cycle;
            }


            return changedWavedCycle;
        }

        public bool SmartAddEndingWave5CtoManagerAndBar( int waveScenarioNo,
                                                            TimeSpan responsibleForWhatTimeFrame,
                                                            long rawBarTime,
                                                            ElliottWaveCycle cycle,
                                                            ElliottWaveEnum waveNameToBeAdded,
                                                            WaveLabelPosition labelPosition,
                                                            ref SBar bar )
        {
            if ( ( waveNameToBeAdded != ElliottWaveEnum.Wave5C ) && ( waveNameToBeAdded != ElliottWaveEnum.Wave5 ) )
            {
                return false;
            }

            ElliottWaveCycle changedWavedCycle = cycle;

            WaveInfo tobeSynced = new WaveInfo( );

            tobeSynced.WaveCycle = cycle;
            tobeSynced.WaveName = waveNameToBeAdded;

            if ( _hews.ContainsKey( rawBarTime ) )
            {
                var dbHew = _hews[ rawBarTime ];

                tobeSynced.LabelPosition = dbHew.WaveLabelPosition;

                ref var hew = ref dbHew.GetWaveFromScenario( waveScenarioNo );

                var waveAtCycle = hew.GetWavesAtCycle( cycle );

                if ( waveAtCycle.Count > 0 )
                {
                    if ( waveAtCycle[ 0 ] == ElliottWaveEnum.WaveC )
                    {
                        switch ( waveNameToBeAdded )
                        {
                            case ElliottWaveEnum.Wave1:
                            {
                                tobeSynced.WaveName = ElliottWaveEnum.Wave1C;
                                hew.AddHarmonicElliottWave( cycle, ElliottWaveEnum.Wave1C, dbHew.WaveLabelPosition );
                            }
                            break;

                            case ElliottWaveEnum.Wave3:
                            {
                                tobeSynced.WaveName = ElliottWaveEnum.Wave3C;
                                hew.AddHarmonicElliottWave( cycle, ElliottWaveEnum.Wave3C, dbHew.WaveLabelPosition );
                            }
                            break;

                            case ElliottWaveEnum.Wave5:
                            {
                                tobeSynced.WaveName = ElliottWaveEnum.Wave5C;
                                hew.AddHarmonicElliottWave( cycle, ElliottWaveEnum.Wave5C, dbHew.WaveLabelPosition );
                            }
                            break;

                            // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                            //
                            // If we are adding a wave on top of Wave C, then it might be wave of higher degree. We need to check the previous wave of higher degree.
                            //
                            // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                            default:
                            {
                                hew.AddHarmonicElliottWave( cycle, waveNameToBeAdded, dbHew.WaveLabelPosition );
                            }
                            break;
                        }
                    }
                    else
                    {
                        hew.AddHarmonicElliottWave( cycle, waveNameToBeAdded, dbHew.WaveLabelPosition );
                    }
                }
                else
                {
                    hew.AddHarmonicElliottWave( cycle, waveNameToBeAdded, dbHew.WaveLabelPosition );
                }

                //AddWavesDownSmallerTimeFrame( waveScenarioNo, _offerId, tobeSynced, rawBarTime.FromLinuxTime( ), responsibleForWhatTimeFrame );
                //AddWaveUpHigherTimeFrame( waveScenarioNo, _offerId, tobeSynced, rawBarTime.FromLinuxTime( ), responsibleForWhatTimeFrame );

                bar.AddWave( waveScenarioNo, ref hew );

                _hews[ rawBarTime ] = dbHew;
            }
            else
            {
                if ( labelPosition != WaveLabelPosition.UNKNOWN )
                {
                    tobeSynced.WaveCycle = changedWavedCycle;
                    tobeSynced.WaveName = waveNameToBeAdded;
                    tobeSynced.LabelPosition = labelPosition;

                    var newWave = new HewLong( changedWavedCycle, waveNameToBeAdded, labelPosition );

                    while ( ( waveNameToBeAdded == ElliottWaveEnum.Wave5C ) || ( waveNameToBeAdded == ElliottWaveEnum.Wave5 ) )
                    {
                        var lastHigherWave = GetPreviousWaveStructureOfDegree( waveScenarioNo, rawBarTime, changedWavedCycle + GlobalConstants.OneWaveCycle );

                        if ( lastHigherWave != null )
                        {
                            var lastHigherWaveName = GetPreviousWaveOfDegree( waveScenarioNo, rawBarTime, changedWavedCycle + GlobalConstants.OneWaveCycle );

                            waveNameToBeAdded = harmonicEWave.GetNextWave( lastHigherWaveName );

                            if ( waveNameToBeAdded != ElliottWaveEnum.NONE )
                            {
                                changedWavedCycle = changedWavedCycle + GlobalConstants.OneWaveCycle;

                                newWave.AddHarmonicElliottWave( changedWavedCycle, waveNameToBeAdded, labelPosition );
                            }
                        }
                    }

                    var waves = newWave.GetAllWaves( );

                    //foreach( var wave in waves )
                    //{
                    //    AddWavesDownSmallerTimeFrame( waveScenarioNo, _offerId, wave, rawBarTime.FromLinuxTime( ), responsibleForWhatTimeFrame );
                    //    AddWaveUpHigherTimeFrame( waveScenarioNo, _offerId, wave, rawBarTime.FromLinuxTime( ), responsibleForWhatTimeFrame );
                    //}

                    bar.AddWave( waveScenarioNo, ref newWave );

                    var dbHewNew = new DbElliottWave( _offerId, _bars, ref bar );

                    _hews.Add( rawBarTime, dbHewNew );

                    if ( _selectedRemovedWavesList != null )
                    {
                        var index = _selectedRemovedWavesList.FindIndex( x => x.Equals( rawBarTime ) );

                        if ( index > -1 )
                        {
                            _selectedRemovedWavesList.Remove( rawBarTime );
                        }
                    }

                    Messenger.Default.Send( new ToggleCommandMessage( CommandMessages.HasHewFunctions, true ) );
                }
            }

            return true;
        }

        public WaveLabelPosition DumpAddWaveXtoManagerAndBar( int waveScenarioNo,
                                                                TimeSpan responsibleForWhatTimeFrame,
                                                                long rawBarTime,
                                                                ElliottWaveCycle cycle,
                                                                WaveLabelPosition labelPosition,
                                                                ElliottWaveEnum currentWaveName
                                                            )
        {
            bool succeed = false;

            ref SBar bar = ref _bars.GetBarByTime( rawBarTime );

            if ( _hews.ContainsKey( rawBarTime ) )
            {
                var dbHew = _hews[ rawBarTime ];

                ref var hew = ref dbHew.GetWaveFromScenario( waveScenarioNo );

                hew.AddHarmonicElliottWave( cycle, currentWaveName, labelPosition );

                bar.AddWave( waveScenarioNo, ref hew );

                _hews[ rawBarTime ] = dbHew;
            }
            else
            {
                if ( labelPosition != WaveLabelPosition.UNKNOWN )
                {
                    var newWave = new HewLong( cycle, currentWaveName, labelPosition );
                    bar.AddWave( waveScenarioNo, ref newWave );
                    var dbHewNew = new DbElliottWave( _offerId, _bars, ref bar );

                    _hews.Add( rawBarTime, dbHewNew );

                    if ( _selectedRemovedWavesList != null )
                    {
                        var index = _selectedRemovedWavesList.FindIndex( x => x.Equals( rawBarTime ) );

                        if ( index > -1 )
                        {
                            _selectedRemovedWavesList.Remove( rawBarTime );
                        }
                    }

                    WavePriceTimeInfo output = default;

                    if ( currentWaveName == ElliottWaveEnum.Wave2 )
                    {
                        if ( succeed = GetPriceTimeInfo_Wave0_Wave1C_Wave2_HigherTF( waveScenarioNo, _bars, responsibleForWhatTimeFrame, rawBarTime, cycle, labelPosition, ref output ) == false )
                        {
                            succeed = GetPriceTimeInfo_Wave0_Wave1C_Wave2_LowerTF( waveScenarioNo, _bars, responsibleForWhatTimeFrame, rawBarTime, cycle, labelPosition, ref output );
                        }

                    }
                    else if ( currentWaveName == ElliottWaveEnum.Wave4 )
                    {
                        if ( succeed = GetPriceTimeInfo_Wave2_Wave3C_Wave4_HigherTF( waveScenarioNo, _bars, responsibleForWhatTimeFrame, rawBarTime, cycle, labelPosition, ref output ) == false )
                        {
                            succeed = GetPriceTimeInfo_Wave2_Wave3C_Wave4_LowerTF( waveScenarioNo, _bars, responsibleForWhatTimeFrame, rawBarTime, cycle, labelPosition, ref output );
                        }
                    }
                    else if ( currentWaveName == ElliottWaveEnum.WaveB )
                    {
                        if ( succeed = GetPriceTimeInfo_Wave024X_WaveA_WaveB_HigherTF( waveScenarioNo, _bars, responsibleForWhatTimeFrame, rawBarTime, cycle, labelPosition, ref output ) == false )
                        {
                            succeed = GetPriceTimeInfo_Wave024X_WaveA_WaveB_HigherTF( waveScenarioNo, _bars, responsibleForWhatTimeFrame, rawBarTime, cycle, labelPosition, ref output );
                        }
                    }

                    if ( succeed )
                    {
                        bar.MainPriceTimeInfo = output;
                    }

                    Messenger.Default.Send( new ToggleCommandMessage( CommandMessages.HasHewFunctions, false ) );
                }
            }

            return labelPosition;
        }

        public void AddMultipleWavesToManager( int waveScenarioNo, TimeSpan responsibleForWhatTimeFrame,
                                               long rawBarTime,
                                               ElliottWaveCycle cycle,
                                               ElliottWaveEnum waveNameToBeAdded,
                                               ref SBar bar,
                                               bool isOutsideBar )
        {
            var labelPosition = WaveLabelPosition.UNKNOWN;

            DbElliottWave dbHew = null;

            if ( _hews.ContainsKey( rawBarTime ) )
            {
                dbHew = _hews[ rawBarTime ];

                labelPosition = WaveLabelPosition.BOTH;

                if ( DidUpgradeWaveC( waveScenarioNo, responsibleForWhatTimeFrame, dbHew, rawBarTime, cycle, waveNameToBeAdded, ref bar ) )
                {
                    return;
                }
                else
                {
                    var labelPosCycle = GetCycleAndLabelPosition( waveScenarioNo, responsibleForWhatTimeFrame, rawBarTime, cycle, waveNameToBeAdded, ref bar, isOutsideBar );

                    var hew = new HewLong( dbHew.HarmonicElliottWaveBit );

                    hew.AddSpecialHarmonicElliottWave( cycle, waveNameToBeAdded, labelPosition );

                    bar.AddWave( waveScenarioNo, ref hew );

                    dbHew.SyncWithBar( ref bar );

                    dbHew.WaveLabelPosition = WaveLabelPosition.BOTH;
                }
            }
            else
            {
                labelPosition = FindCurrentWaveLabelPosition( waveScenarioNo, responsibleForWhatTimeFrame, rawBarTime, cycle, waveNameToBeAdded, ref bar, isOutsideBar );

                var hew = new HewLong( cycle, waveNameToBeAdded, labelPosition );

                bar.AddWave( waveScenarioNo, ref hew );

                dbHew = new DbElliottWave( _offerId, _bars, ref bar );

                bar.IsSpecialBar = true;
            }

            _hews[ rawBarTime ] = dbHew;
        }

        private void AddWaveToOutsideBar(
                                            int waveScenarioNo,
                                            TimeSpan responsibleForWhatTimeFrame,
                                            long rawBarTime,
                                            ElliottWaveCycle cycle,
                                            ElliottWaveEnum waveNameToBeAdded,
                                            ref SBar bar,
                                            bool isSpecialBar,
                                            bool isOutsideBar )
        {
            var labelPosition = WaveLabelPosition.UNKNOWN;

            DbElliottWave dbHew = null;

            if ( _hews.ContainsKey( rawBarTime ) )
            {
                throw new InvalidProgramException();
            }

            labelPosition = FindCurrentWaveLabelPosition( waveScenarioNo, responsibleForWhatTimeFrame, rawBarTime, cycle, waveNameToBeAdded, ref bar, isOutsideBar );

            var hew = new HewLong( cycle, waveNameToBeAdded, labelPosition );

            bar.AddWave( waveScenarioNo, ref hew );

            dbHew = new DbElliottWave( _offerId, _bars, ref bar );

            bar.IsSpecialBar = isSpecialBar;

            _hews[ rawBarTime ] = dbHew;
        }

        private void IntelliAddWaveToSpecialBar(
                                                    int waveScenarioNo,
                                                    TimeSpan responsibleForWhatTimeFrame,
                                                    long rawBarTime,
                                                    ElliottWaveCycle cycle,
                                                    ElliottWaveEnum waveNameToBeAdded,
                                                    ref SBar bar,
                                                    bool isOutsideBar )
        {
            if ( !_hews.ContainsKey( rawBarTime ) )
            {
                if ( isOutsideBar )
                {
                    AddWaveToOutsideBar( waveScenarioNo, responsibleForWhatTimeFrame, rawBarTime, cycle, waveNameToBeAdded, ref bar, true, isOutsideBar );
                }
                else
                {
                    AddSingleWave( waveScenarioNo, responsibleForWhatTimeFrame, rawBarTime, cycle, waveNameToBeAdded, ref bar, true, isOutsideBar );
                }

                return;
            }

            var dbHew = _hews[ rawBarTime ];
            ref var hew = ref dbHew.GetWaveFromScenario(waveScenarioNo );

            if ( hew.Count == 1 )
            {
                AddWaveToSpecialBarOrUpgrade( waveScenarioNo, responsibleForWhatTimeFrame, rawBarTime, cycle, waveNameToBeAdded, dbHew, hew, ref bar, true, isOutsideBar );
            }
            else if ( hew.Count > 1 )
            {
                ComplexAddWaveToSpecialBar( waveScenarioNo, responsibleForWhatTimeFrame, rawBarTime, cycle, waveNameToBeAdded, dbHew, hew, ref bar, true, isOutsideBar );
            }
        }

        private bool AddWaveToSpecialBarOrUpgrade( int waveScenarioNo, TimeSpan responsibleForWhatTimeFrame,
                                                   long selectedBarTime,
                                                   ElliottWaveCycle waveCycle,
                                                   ElliottWaveEnum waveNameToBeAdded,
                                                   DbElliottWave dbHew,
                                                   HewLong hew,
                                                   ref SBar bar,
                                                   bool isSpecialBar,
                                                   bool isOutsideBar )
        {
            if ( hew.Count == 0 )
            {
                return false;
            }

            var startingWaveInfo = hew.GetFirstWave( );

            if ( !startingWaveInfo.HasValue )
            {
                return false;
            }

            var desiredWaveCycle = startingWaveInfo.Value.WaveCycle;

            var oneDegreeHigher = startingWaveInfo.Value.WaveCycle + GlobalConstants.OneWaveCycle;
            var oneDegreeLower = startingWaveInfo.Value.WaveCycle - GlobalConstants.OneWaveCycle;

            var prevStructOneDegreeHigher = GetPreviousWaveStructureOfDegree( waveScenarioNo,selectedBarTime, oneDegreeHigher );
            var prevStructOneDegreeLower = GetPreviousWaveStructureOfDegree( waveScenarioNo,selectedBarTime, oneDegreeLower );
            var prevStructOfSameDegree = GetPreviousWaveStructureOfDegree( waveScenarioNo,selectedBarTime, startingWaveInfo.Value.WaveCycle );

            PooledList<ElliottWaveEnum> prevWaveOneDegreeHigher = null;
            PooledList<ElliottWaveEnum> prevWaveOneDegreeLower = null;

            if ( prevStructOneDegreeHigher != null )
            {
                ref var hew2 = ref prevStructOneDegreeHigher.GetWaveFromScenario(waveScenarioNo );
                prevWaveOneDegreeHigher = hew2.GetWavesAtCycle( oneDegreeHigher );
            }

            if ( prevWaveOneDegreeLower != null )
            {
                ref var hew2 = ref prevStructOneDegreeLower.GetWaveFromScenario(waveScenarioNo );
                prevWaveOneDegreeLower = hew2.GetWavesAtCycle( oneDegreeLower );
            }

            if ( hew.Count == 1 )
            {
                /// <image url="$(SolutionDir)\..\..\30 - CommonImages\AddingWaveC.jpg"/>
                ElliottWaveEnum output = ElliottWaveEnum.NONE;

                if ( ( output = ShouldUpgradeWaveC( waveScenarioNo, selectedBarTime, waveCycle, waveNameToBeAdded, hew, ref bar, isSpecialBar, isOutsideBar ) ) != ElliottWaveEnum.NONE )
                {
                }
                // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                //
                //  Check if the One degree Higher wave Can be added. 
                //
                // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                else if ( prevWaveOneDegreeHigher != null && prevWaveOneDegreeLower != null && prevWaveOneDegreeHigher.Count > 0 && prevWaveOneDegreeLower[ 0 ].IsNextWave( waveNameToBeAdded ) && NoHigherWaveBetween( waveScenarioNo, prevStructOneDegreeHigher.StartDate, selectedBarTime, oneDegreeHigher ) && harmonicEWave.IsValidLowerDegreeWave( waveNameToBeAdded, startingWaveInfo.Value.WaveName ) )
                {
                    if ( waveNameToBeAdded == ElliottWaveEnum.Wave2 || waveNameToBeAdded == ElliottWaveEnum.Wave4 || waveNameToBeAdded == ElliottWaveEnum.WaveB )
                    {
                        desiredWaveCycle = oneDegreeHigher;

                        hew.AddSpecialHarmonicElliottWave( desiredWaveCycle, waveNameToBeAdded, startingWaveInfo.Value.LabelPosition );
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                //
                //  Check if the input wave is the next wave of the ONE DEGREE LOWER
                //
                // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                else if ( prevWaveOneDegreeHigher != null && prevWaveOneDegreeLower != null && prevWaveOneDegreeLower.Count > 0 && startingWaveInfo.Value.WaveName.IsValidLowerDegreeWave( waveNameToBeAdded ) && prevWaveOneDegreeLower[ 0 ].IsNextWave( waveNameToBeAdded ) && NoHigherWaveBetween( waveScenarioNo, prevStructOneDegreeLower.StartDate, selectedBarTime, oneDegreeLower ) )
                {
                    desiredWaveCycle = oneDegreeLower;

                    hew.AddSpecialHarmonicElliottWave( desiredWaveCycle, waveNameToBeAdded, startingWaveInfo.Value.LabelPosition );
                }
                else if ( startingWaveInfo.Value.WaveName.IsNextWaveOneDegreeLowerBeginning( waveNameToBeAdded ) )
                {
                    var waveLabelPosition = WaveLabelPosition.UNKNOWN;

                    if ( startingWaveInfo.Value.LabelPosition == WaveLabelPosition.TOP )
                    {
                        waveLabelPosition = WaveLabelPosition.BOTTOM;
                    }
                    else if ( startingWaveInfo.Value.LabelPosition == WaveLabelPosition.BOTTOM )
                    {
                        waveLabelPosition = WaveLabelPosition.TOP;
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }

                    desiredWaveCycle = oneDegreeLower;

                    hew.AddSpecialHarmonicElliottWave( desiredWaveCycle, waveNameToBeAdded, waveLabelPosition );
                }

                // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                //
                //  Check if the input wave is the next wave of the same degree
                //
                // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                else if ( startingWaveInfo.Value.WaveName.IsNextWave( waveNameToBeAdded ) )
                {
                    var waveLabelPosition = WaveLabelPosition.UNKNOWN;

                    if ( startingWaveInfo.Value.LabelPosition == WaveLabelPosition.TOP )
                    {
                        waveLabelPosition = WaveLabelPosition.BOTTOM;
                    }
                    else if ( startingWaveInfo.Value.LabelPosition == WaveLabelPosition.BOTTOM )
                    {
                        waveLabelPosition = WaveLabelPosition.TOP;
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }

                    desiredWaveCycle = startingWaveInfo.Value.WaveCycle;

                    hew.AddSpecialHarmonicElliottWave( desiredWaveCycle, waveNameToBeAdded, waveLabelPosition );
                }
                else if ( startingWaveInfo.Value.WaveName.IsNextWaveTwoDegreesLowerBeginning( waveNameToBeAdded ) )
                {
                    var waveLabelPosition = WaveLabelPosition.UNKNOWN;

                    if ( startingWaveInfo.Value.LabelPosition == WaveLabelPosition.TOP )
                    {
                        waveLabelPosition = WaveLabelPosition.BOTTOM;
                    }
                    else if ( startingWaveInfo.Value.LabelPosition == WaveLabelPosition.BOTTOM )
                    {
                        waveLabelPosition = WaveLabelPosition.TOP;
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }

                    desiredWaveCycle = oneDegreeLower - GlobalConstants.OneWaveCycle;

                    hew.AddSpecialHarmonicElliottWave( desiredWaveCycle, waveNameToBeAdded, waveLabelPosition );
                }

                ref var hew2 = ref dbHew.GetWaveFromScenario( waveScenarioNo );
                hew2.CopyFrom( ref hew );

                dbHew.WaveLabelPosition = WaveLabelPosition.BOTH;

                bar.AddWave( waveScenarioNo, ref hew );

                bar.IsSpecialBar = true;

                _hews[ selectedBarTime ] = dbHew;

                Messenger.Default.Send( new HewMessage( desiredWaveCycle ) );
            }

            return true;
        }

        private bool ComplexAddWaveToSpecialBar( int waveScenarioNo, TimeSpan responsibleForWhatTimeFrame,
                                                   long selectedBarTime,
                                                   ElliottWaveCycle waveCycle,
                                                   ElliottWaveEnum waveNameToBeAdded,
                                                   DbElliottWave dbHew,
                                                   HewLong hew,
                                                   ref SBar bar,
                                                   bool isSpecialBar,
                                                   bool isOutsideBar )
        {
            if ( hew.Count == 0 )
            {
                return false;
            }

            var startingWaveInfo = hew.GetFirstWave( );

            if ( !startingWaveInfo.HasValue )
            {
                return false;
            }

            if ( hew.Count > 1 )
            {
                if ( startingWaveInfo.Value.LabelPosition == WaveLabelPosition.TOP )
                {
                    ProcessTopStartingWave( waveScenarioNo, selectedBarTime, waveCycle, waveNameToBeAdded, dbHew, hew, ref bar );
                }
                else if ( startingWaveInfo.Value.LabelPosition == WaveLabelPosition.BOTTOM )
                {
                    ProcessBottomStartingWave( waveScenarioNo, selectedBarTime, waveCycle, waveNameToBeAdded, dbHew, hew, ref bar );
                }
            }

            return true;
        }

        private bool ProcessTopStartingWave( int waveScenarioNo, long selectedBarTime,
                                                ElliottWaveCycle waveCycle,
                                                ElliottWaveEnum waveNameToBeAdded,
                                                DbElliottWave dbHew,
                                                HewLong hew,
                                                ref SBar bar )
        {
            if ( hew.Count <= 1 )
            {
                return false;
            }

            bool doneAdding               = false;

            var startingWaveInfo          = hew.GetFirstWave( );

            var highestTopWave            = hew.GetHighestTopWave( );

            var highestDegreeCycle        = highestTopWave.HasValue ? highestTopWave.Value.WaveCycle : ElliottWaveCycle.UNKNOWN;

            var lowestTopWave             = hew.GetLowestTopWave( );

            var lowestDegreeCycle         = lowestTopWave.HasValue ? lowestTopWave.Value.WaveCycle : ElliottWaveCycle.UNKNOWN;

            var oneDegreeHigher           = highestDegreeCycle + GlobalConstants.OneWaveCycle;
            var oneDegreeLower            = lowestDegreeCycle - GlobalConstants.OneWaveCycle;

            var prevStructOneDegreeHigher = GetPreviousWaveStructureOfDegree( waveScenarioNo,selectedBarTime, oneDegreeHigher );
            var prevStructOneDegreeLower  = GetPreviousWaveStructureOfDegree( waveScenarioNo,selectedBarTime, oneDegreeLower );
            var prevStructOfSameDegree    = GetPreviousWaveStructureOfDegree( waveScenarioNo,selectedBarTime, startingWaveInfo.Value.WaveCycle );

            PooledList<ElliottWaveEnum> prevWaveOneDegreeHigher = null;
            PooledList<ElliottWaveEnum> prevWaveOneDegreeLower = null;

            if ( prevStructOneDegreeHigher != null )
            {
                ref var hew2 = ref prevStructOneDegreeHigher.GetWaveFromScenario(waveScenarioNo );
                prevWaveOneDegreeHigher = hew2.GetWavesAtCycle( oneDegreeHigher );
            }

            if ( prevWaveOneDegreeLower != null )
            {
                ref var hew2 = ref prevStructOneDegreeLower.GetWaveFromScenario(waveScenarioNo );
                prevWaveOneDegreeLower = hew2.GetWavesAtCycle( oneDegreeLower );
            }

            var allTopWaves = hew.GetTopWaves( );

            var allBottomWaves = hew.GetBottomWaves( );

            if ( prevWaveOneDegreeHigher != null && prevWaveOneDegreeHigher.Count > 0 && prevWaveOneDegreeHigher[ 0 ].IsNextWave( waveNameToBeAdded ) && NoHigherWaveBetween( waveScenarioNo, prevStructOneDegreeHigher.StartDate, selectedBarTime, oneDegreeHigher ) )
            {
                if ( waveNameToBeAdded == ElliottWaveEnum.Wave2 || waveNameToBeAdded == ElliottWaveEnum.Wave4 || waveNameToBeAdded == ElliottWaveEnum.WaveB )
                {
                    var index = allTopWaves.FindIndex( x => x.WaveName == waveNameToBeAdded && x.WaveCycle == oneDegreeHigher );

                    if ( index < 0 )
                    {
                        hew.AddSpecialHarmonicElliottWave( oneDegreeHigher, waveNameToBeAdded, startingWaveInfo.Value.LabelPosition );

                        doneAdding = true;
                    }
                }
            }
            else if ( prevWaveOneDegreeLower != null && prevWaveOneDegreeLower.Count > 0 && startingWaveInfo.Value.WaveName.IsValidLowerDegreeWave( waveNameToBeAdded ) && prevWaveOneDegreeLower[ 0 ].IsNextWave( waveNameToBeAdded ) && NoHigherWaveBetween( waveScenarioNo, prevStructOneDegreeLower.StartDate, selectedBarTime, oneDegreeLower ) )
            {
                var index = allTopWaves.FindIndex( x => x.WaveName == waveNameToBeAdded && x.WaveCycle == oneDegreeLower );

                if ( index < 0 )
                {
                    hew.AddSpecialHarmonicElliottWave( oneDegreeLower, waveNameToBeAdded, startingWaveInfo.Value.LabelPosition );

                    doneAdding = true;
                }
            }
            else
            {
                if ( allBottomWaves.Count == 0 )
                {
                    foreach ( var topWave in allTopWaves )
                    {
                        if ( topWave.WaveName.IsNextWave( waveNameToBeAdded ) )
                        {
                            var waveLabelPosition = WaveLabelPosition.UNKNOWN;

                            if ( topWave.LabelPosition == WaveLabelPosition.TOP )
                            {
                                waveLabelPosition = WaveLabelPosition.BOTTOM;
                            }
                            else if ( topWave.LabelPosition == WaveLabelPosition.BOTTOM )
                            {
                                waveLabelPosition = WaveLabelPosition.TOP;
                            }
                            else
                            {
                                throw new NotImplementedException();
                            }

                            var index = allTopWaves.FindIndex( x => x.WaveName == waveNameToBeAdded && x.WaveCycle == topWave.WaveCycle );

                            if ( index < 0 )
                            {
                                hew.AddSpecialHarmonicElliottWave( topWave.WaveCycle, waveNameToBeAdded, waveLabelPosition );

                                doneAdding = true;

                                break;
                            }
                        }
                    }
                }
            }

            if ( doneAdding )
            {
                bar.AddWave( waveScenarioNo, ref hew );
                bar.IsSpecialBar = true;

                dbHew.SyncWithBar( ref bar );
                dbHew.WaveLabelPosition = WaveLabelPosition.BOTH;

                _hews[ selectedBarTime ] = dbHew;
            }
            else
            {
                allBottomWaves.Reverse();

                var highestPreviousWave = hew.GetHighestTopWave( );
                var lowestPreviousWave = hew.GetLowestTopWave( );

                foreach ( var bottomWave in allBottomWaves )
                {
                    if ( bottomWave.WaveName == ElliottWaveEnum.WaveC )
                    {
                        if ( waveNameToBeAdded == ElliottWaveEnum.Wave1 || waveNameToBeAdded == ElliottWaveEnum.Wave3 || waveNameToBeAdded == ElliottWaveEnum.Wave5 )
                        {
                            switch ( waveNameToBeAdded )
                            {
                                case ElliottWaveEnum.Wave1:
                                {
                                    if ( CanUpgradeTo1C( waveScenarioNo, selectedBarTime, bottomWave.WaveCycle, bottomWave.WaveName, waveNameToBeAdded, true ) )
                                    {
                                        hew.ReplaceBottomWave( selectedBarTime, bottomWave.WaveCycle, bottomWave.WaveName, ElliottWaveEnum.Wave1C );
                                        bar.AddWave( waveScenarioNo, ref hew );
                                        bar.IsSpecialBar = true;

                                        dbHew.SyncWithBar( ref bar );
                                        dbHew.WaveLabelPosition = WaveLabelPosition.BOTH;

                                        _hews[ selectedBarTime ] = dbHew;
                                        return true;
                                    }
                                }
                                break;

                                case ElliottWaveEnum.Wave3:
                                {
                                    if ( CanUpgradeTo3C( waveScenarioNo, selectedBarTime, bottomWave.WaveCycle, bottomWave.WaveName, waveNameToBeAdded, true ) )
                                    {
                                        hew.ReplaceBottomWave( selectedBarTime, bottomWave.WaveCycle, bottomWave.WaveName, ElliottWaveEnum.Wave3C );
                                        bar.AddWave( waveScenarioNo, ref hew );
                                        bar.IsSpecialBar = true;

                                        dbHew.SyncWithBar( ref bar );
                                        dbHew.WaveLabelPosition = WaveLabelPosition.BOTH;

                                        _hews[ selectedBarTime ] = dbHew;
                                        return true;
                                    }
                                }

                                break;

                                case ElliottWaveEnum.Wave5:
                                {
                                    if ( CanUpgradeTo5C( waveScenarioNo, selectedBarTime, bottomWave.WaveCycle, bottomWave.WaveName, waveNameToBeAdded, true ) )
                                    {
                                        hew.ReplaceBottomWave( selectedBarTime, bottomWave.WaveCycle, bottomWave.WaveName, ElliottWaveEnum.Wave5C );
                                        bar.AddWave( waveScenarioNo, ref hew );
                                        bar.IsSpecialBar = true;

                                        dbHew.SyncWithBar( ref bar );
                                        dbHew.WaveLabelPosition = WaveLabelPosition.BOTH;

                                        _hews[ selectedBarTime ] = dbHew;
                                        return true;
                                    }
                                }

                                break;
                            }
                        }
                    }
                    else if ( bottomWave.WaveName.IsNextWave( waveNameToBeAdded ) )
                    {
                        var newWaveName = waveNameToBeAdded;

                        if ( waveNameToBeAdded == ElliottWaveEnum.WaveC )
                        {
                        }

                        var index = allBottomWaves.FindIndex( x => x.WaveName == newWaveName && x.WaveCycle == bottomWave.WaveCycle );

                        if ( index < 0 )
                        {
                            hew.AddSpecialHarmonicElliottWave( bottomWave.WaveCycle, newWaveName, bottomWave.LabelPosition );

                            doneAdding = true;

                            break;
                        }
                    }
                }
            }

            if ( doneAdding )
            {
                bar.AddWave( waveScenarioNo, ref hew );
                bar.IsSpecialBar = true;

                dbHew.SyncWithBar( ref bar );
                dbHew.WaveLabelPosition = WaveLabelPosition.BOTH;

                _hews[ selectedBarTime ] = dbHew;
            }

            return true;
        }

        public bool TryUpgradeWaveC(
                                        int waveScenarioNo,
                                        TimeSpan responsibleForWhaTimeSpan,
                                        long selectedBarTime,
                                        ElliottWaveEnum waveNameToBeAdded,
                                        ref SBar bar )
        {
            if ( _hews.ContainsKey( selectedBarTime ) )
            {
                var dbHew = _hews[ selectedBarTime ];

                ref var hew = ref dbHew.GetWaveFromScenario( waveScenarioNo );

                if ( hew.HasElliottWave )
                {
                    var allWaves = hew.GetAllWaves( ).OrderBy( l => l.WaveCycle ).ThenBy( l => l.WaveName ).ToPooledList();

                    foreach ( var eachWave in allWaves )
                    {
                        if ( eachWave.WaveName == ElliottWaveEnum.WaveC )
                        {
                            if ( waveNameToBeAdded == ElliottWaveEnum.Wave1 || waveNameToBeAdded == ElliottWaveEnum.Wave3 || waveNameToBeAdded == ElliottWaveEnum.Wave5 )
                            {
                                switch ( waveNameToBeAdded )
                                {
                                    case ElliottWaveEnum.Wave1:

                                        if ( CanUpgradeTo1C( waveScenarioNo, selectedBarTime, eachWave.WaveCycle, eachWave.WaveName, waveNameToBeAdded, true ) )
                                        {
                                            ReplaceWaveInManagerAndBar( waveScenarioNo, responsibleForWhaTimeSpan, selectedBarTime, eachWave.WaveCycle, eachWave.WaveName, ElliottWaveEnum.Wave1C, ref bar );

                                            return true;
                                        }
                                        break;

                                    case ElliottWaveEnum.Wave3:
                                        if ( CanUpgradeTo3C( waveScenarioNo, selectedBarTime, eachWave.WaveCycle, eachWave.WaveName, waveNameToBeAdded, true ) )
                                        {
                                            ReplaceWaveInManagerAndBar( waveScenarioNo, responsibleForWhaTimeSpan, selectedBarTime, eachWave.WaveCycle, eachWave.WaveName, ElliottWaveEnum.Wave3C, ref bar );

                                            return true;
                                        }
                                        break;

                                    case ElliottWaveEnum.Wave5:
                                        if ( CanUpgradeTo5C( waveScenarioNo, selectedBarTime, eachWave.WaveCycle, eachWave.WaveName, waveNameToBeAdded, true ) )
                                        {
                                            ReplaceWaveInManagerAndBar( waveScenarioNo, responsibleForWhaTimeSpan, selectedBarTime, eachWave.WaveCycle, eachWave.WaveName, ElliottWaveEnum.Wave5C, ref bar );

                                            return true;
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        private bool ProcessBottomStartingWave( int waveScenarioNo, long selectedBarTime,
                                                ElliottWaveCycle waveCycle,
                                                ElliottWaveEnum waveNameToBeAdded,
                                                DbElliottWave dbHew,
                                                HewLong hew,
                                                ref SBar bar )
        {
            if ( hew.Count <= 1 )
            {
                return false;
            }

            bool doneAdding = false;

            var startingWaveInfo = hew.GetFirstWave( );

            var highestBottomWave = hew.GetHighestBottomWave( );

            var highestDegreeCycle = highestBottomWave.HasValue ? highestBottomWave.Value.WaveCycle : ElliottWaveCycle.UNKNOWN;

            var lowestBottomWave = hew.GetLowestBottomWave( );

            var lowestDegreeCycle = lowestBottomWave.HasValue ? lowestBottomWave.Value.WaveCycle : ElliottWaveCycle.UNKNOWN;

            var oneDegreeHigher = highestDegreeCycle + GlobalConstants.OneWaveCycle;
            var oneDegreeLower = lowestDegreeCycle - GlobalConstants.OneWaveCycle;

            var prevStructOneDegreeHigher = GetPreviousWaveStructureOfDegree(waveScenarioNo, selectedBarTime, oneDegreeHigher );
            var prevStructOneDegreeLower = GetPreviousWaveStructureOfDegree( waveScenarioNo,selectedBarTime, oneDegreeLower );
            var prevStructOfSameDegree = GetPreviousWaveStructureOfDegree( waveScenarioNo,selectedBarTime, startingWaveInfo.Value.WaveCycle );
            PooledList<ElliottWaveEnum> prevWaveOneDegreeHigher = null;
            PooledList<ElliottWaveEnum> prevWaveOneDegreeLower = null;

            if ( prevStructOneDegreeHigher != null )
            {
                ref var hew2 = ref prevStructOneDegreeHigher.GetWaveFromScenario(waveScenarioNo );
                prevWaveOneDegreeHigher = hew2.GetWavesAtCycle( oneDegreeHigher );
            }

            if ( prevWaveOneDegreeLower != null )
            {
                ref var hew2 = ref prevStructOneDegreeLower.GetWaveFromScenario(waveScenarioNo );
                prevWaveOneDegreeLower = hew2.GetWavesAtCycle( oneDegreeLower );
            }

            var allTopWaves = hew.GetTopWaves( );

            var allBottomWaves = hew.GetBottomWaves( );

            if ( prevWaveOneDegreeHigher != null && prevWaveOneDegreeHigher.Count > 0 && prevWaveOneDegreeHigher[ 0 ].IsNextWave( waveNameToBeAdded ) && NoHigherWaveBetween( waveScenarioNo, prevStructOneDegreeHigher.StartDate, selectedBarTime, oneDegreeHigher ) )
            {
                if ( waveNameToBeAdded == ElliottWaveEnum.Wave2 || waveNameToBeAdded == ElliottWaveEnum.Wave4 || waveNameToBeAdded == ElliottWaveEnum.WaveB )
                {
                    var index = allBottomWaves.FindIndex( x => x.WaveName == waveNameToBeAdded && x.WaveCycle == oneDegreeHigher );

                    if ( index < 0 )
                    {
                        hew.AddSpecialHarmonicElliottWave( oneDegreeHigher, waveNameToBeAdded, startingWaveInfo.Value.LabelPosition );

                        doneAdding = true;
                    }
                }
            }
            else if ( prevWaveOneDegreeLower != null && prevWaveOneDegreeLower.Count > 0 && startingWaveInfo.Value.WaveName.IsValidLowerDegreeWave( waveNameToBeAdded ) && prevWaveOneDegreeLower[ 0 ].IsNextWave( waveNameToBeAdded ) && NoHigherWaveBetween( waveScenarioNo, prevStructOneDegreeLower.StartDate, selectedBarTime, oneDegreeLower ) )
            {
                var index = allBottomWaves.FindIndex( x => x.WaveName == waveNameToBeAdded && x.WaveCycle == oneDegreeLower );

                if ( index < 0 )
                {
                    hew.AddSpecialHarmonicElliottWave( oneDegreeLower, waveNameToBeAdded, startingWaveInfo.Value.LabelPosition );

                    doneAdding = true;
                }
            }
            else
            {
                if ( allTopWaves.Count == 0 )
                {
                    foreach ( var bottomWave in allBottomWaves )
                    {
                        if ( bottomWave.WaveName.IsNextWave( waveNameToBeAdded ) )
                        {
                            var waveLabelPosition = WaveLabelPosition.UNKNOWN;

                            if ( bottomWave.LabelPosition == WaveLabelPosition.TOP )
                            {
                                waveLabelPosition = WaveLabelPosition.BOTTOM;
                            }
                            else if ( bottomWave.LabelPosition == WaveLabelPosition.BOTTOM )
                            {
                                waveLabelPosition = WaveLabelPosition.TOP;
                            }
                            else
                            {
                                throw new NotImplementedException();
                            }

                            var index = allBottomWaves.FindIndex( x => x.WaveName == waveNameToBeAdded && x.WaveCycle == bottomWave.WaveCycle );

                            if ( index < 0 )
                            {
                                hew.AddSpecialHarmonicElliottWave( bottomWave.WaveCycle, waveNameToBeAdded, waveLabelPosition );

                                doneAdding = true;

                                break;
                            }
                        }
                    }
                }
            }

            if ( doneAdding )
            {
                bar.AddWave( waveScenarioNo, ref hew );
                bar.IsSpecialBar = true;

                dbHew.SyncWithBar( ref bar );
                dbHew.WaveLabelPosition = WaveLabelPosition.BOTH;

                _hews[ selectedBarTime ] = dbHew;
            }
            else
            {
                allTopWaves.Reverse();

                var highestPreviousWave = hew.GetHighestBottomWave( );
                var lowestPreviousWave = hew.GetLowestBottomWave( );

                foreach ( var topWave in allTopWaves )
                {
                    if ( topWave.WaveName == ElliottWaveEnum.WaveC )
                    {
                        if ( waveNameToBeAdded == ElliottWaveEnum.Wave1 || waveNameToBeAdded == ElliottWaveEnum.Wave3 || waveNameToBeAdded == ElliottWaveEnum.Wave5 )
                        {
                            bool didUpgrade = false;

                            switch ( waveNameToBeAdded )
                            {
                                case ElliottWaveEnum.Wave1:
                                    if ( CanUpgradeTo1C( waveScenarioNo, selectedBarTime, topWave.WaveCycle, topWave.WaveName, waveNameToBeAdded, true ) )
                                    {
                                        hew.ReplaceBottomWave( selectedBarTime, topWave.WaveCycle, topWave.WaveName, ElliottWaveEnum.Wave1C );
                                        didUpgrade = true;
                                    }
                                    break;

                                case ElliottWaveEnum.Wave3:
                                    if ( CanUpgradeTo3C( waveScenarioNo, selectedBarTime, topWave.WaveCycle, topWave.WaveName, waveNameToBeAdded, true ) )
                                    {
                                        hew.ReplaceBottomWave( selectedBarTime, topWave.WaveCycle, topWave.WaveName, ElliottWaveEnum.Wave3C );
                                        didUpgrade = true;
                                    }
                                    break;

                                case ElliottWaveEnum.Wave5:
                                    if ( CanUpgradeTo5C( waveScenarioNo, selectedBarTime, topWave.WaveCycle, topWave.WaveName, waveNameToBeAdded, true ) )
                                    {
                                        hew.ReplaceBottomWave( selectedBarTime, topWave.WaveCycle, topWave.WaveName, ElliottWaveEnum.Wave5C );
                                        didUpgrade = true;
                                    }
                                    break;
                            }

                            if ( didUpgrade )
                            {
                                bar.AddWave( waveScenarioNo, ref hew );
                                bar.IsSpecialBar = true;

                                dbHew.SyncWithBar( ref bar );
                                dbHew.WaveLabelPosition = WaveLabelPosition.BOTH;

                                _hews[ selectedBarTime ] = dbHew;
                                return true;
                            }
                        }
                    }
                    else if ( topWave.WaveName.IsNextWave( waveNameToBeAdded ) )
                    {
                        var newWaveName = waveNameToBeAdded;

                        if ( waveNameToBeAdded == ElliottWaveEnum.WaveC )
                        {
                        }

                        var index = allTopWaves.FindIndex( x => x.WaveName == newWaveName && x.WaveCycle == topWave.WaveCycle );

                        if ( index < 0 )
                        {
                            hew.AddSpecialHarmonicElliottWave( topWave.WaveCycle, newWaveName, topWave.LabelPosition );

                            doneAdding = true;

                            break;
                        }
                    }
                }
            }

            if ( doneAdding )
            {
                bar.AddWave( waveScenarioNo, ref hew );

                bar.IsSpecialBar = true;

                dbHew.SyncWithBar( ref bar );
                dbHew.WaveLabelPosition = WaveLabelPosition.BOTH;



                _hews[ selectedBarTime ] = dbHew;
            }

            return true;
        }

        public bool DidUpgradeWaveC( int waveScenarioNo, TimeSpan responsibleForWhatTimeFrame,
                                     DbElliottWave dbHew,
                                     long rawBarTime,
                                     ElliottWaveCycle cycle,
                                     ElliottWaveEnum waveNameToBeAdded,
                                     ref SBar bar )
        {
            ref var hew = ref dbHew.GetWaveFromScenario( waveScenarioNo );

            var allWaves = hew.GetAllWaves( );

            foreach ( var wave in allWaves )
            {
                var existingWaveCycle = wave.WaveCycle;
                var existingWaveName = wave.WaveName;

                if ( existingWaveCycle == cycle )
                {
                    if ( existingWaveName == ElliottWaveEnum.WaveC )
                    {
                        if ( waveNameToBeAdded == ElliottWaveEnum.Wave1 || waveNameToBeAdded == ElliottWaveEnum.Wave3 || waveNameToBeAdded == ElliottWaveEnum.Wave5 )
                        {
                            var newWaveName = ElliottWaveEnum.NONE;

                            switch ( waveNameToBeAdded )
                            {
                                case ElliottWaveEnum.Wave1:
                                    newWaveName = ElliottWaveEnum.Wave1C;
                                    break;

                                case ElliottWaveEnum.Wave3:
                                    newWaveName = ElliottWaveEnum.Wave3C;
                                    break;

                                case ElliottWaveEnum.Wave5:
                                    newWaveName = ElliottWaveEnum.Wave5C;
                                    break;

                                default:
                                    continue;
                            }

                            ReplaceWaveInManagerAndBar( waveScenarioNo, responsibleForWhatTimeFrame, rawBarTime, cycle, existingWaveName, newWaveName, ref bar );

                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private ElliottWaveEnum ShouldUpgradeWaveC( int waveScenarioNo, long selectedBarTime, ElliottWaveCycle waveCycle, ElliottWaveEnum waveNameToBeAdded, HewLong hew, ref SBar bar, bool isSpecialBar, bool isOutsideBar )
        {
            var allWaves = hew.GetAllWaves( );

            if ( allWaves.Count == 0 )
            {
                return ElliottWaveEnum.NONE;
            }

            if ( ( waveNameToBeAdded == ElliottWaveEnum.Wave1 ) || ( waveNameToBeAdded == ElliottWaveEnum.Wave3 ) || ( waveNameToBeAdded == ElliottWaveEnum.Wave5 ) )
            {
                foreach ( var allWave in allWaves )
                {
                    if ( allWave.WaveName == ElliottWaveEnum.WaveC )
                    {
                        switch ( waveNameToBeAdded )
                        {
                            case ElliottWaveEnum.Wave1:
                                if ( CanUpgradeTo1C( waveScenarioNo, selectedBarTime, allWave.WaveCycle, allWave.WaveName, waveNameToBeAdded, isSpecialBar ) )
                                {
                                    return ElliottWaveEnum.Wave1C;
                                }
                                break;

                            case ElliottWaveEnum.Wave3:
                                if ( CanUpgradeTo3C( waveScenarioNo, selectedBarTime, allWave.WaveCycle, allWave.WaveName, waveNameToBeAdded, isSpecialBar ) )
                                {
                                    return ElliottWaveEnum.Wave1C;
                                }
                                break;

                            case ElliottWaveEnum.Wave5:
                                if ( CanUpgradeTo5C( waveScenarioNo, selectedBarTime, allWave.WaveCycle, allWave.WaveName, waveNameToBeAdded, isSpecialBar ) )
                                {
                                    return ElliottWaveEnum.Wave1C;
                                }
                                break;
                        }
                    }
                }
            }

            return ElliottWaveEnum.NONE;
        }

        public bool CanUpgradeTo1C( int waveScenarioNo, long selectedBarTime, ElliottWaveCycle currentWaveDegree, ElliottWaveEnum currentWaveName, ElliottWaveEnum waveNameTobeAdded, bool isSpecialBar )
        {
            if ( currentWaveName != ElliottWaveEnum.WaveC && waveNameTobeAdded != ElliottWaveEnum.Wave1 )
            {
                throw new InvalidProgramException();
            }

            if ( isSpecialBar )
            {
                return SpecialBarCanUpgradeTo1C( waveScenarioNo, selectedBarTime, currentWaveDegree, currentWaveName, waveNameTobeAdded );
            }

            var beginningWaveRawTime = FindBeginningWaveOfCurrentABCX(waveScenarioNo, selectedBarTime, currentWaveDegree );

            if ( beginningWaveRawTime != -1 )
            {
                var beginWaveHewInfoure = _hews[ beginningWaveRawTime ];

                ref var hew = ref beginWaveHewInfoure.GetWaveFromScenario( waveScenarioNo );

                if ( hew.HasHigherDegreeWave( currentWaveDegree ) )
                {
                    return true;
                }
            }

            return false;
        }

        private bool SpecialBarCanUpgradeTo1C( int waveScenarioNo, long selectedBarTime, ElliottWaveCycle currentWaveDegree, ElliottWaveEnum currentWaveName, ElliottWaveEnum waveNameTobeAdded )
        {
            bool found = false;

            if ( _hews.ContainsKey( selectedBarTime ) )
            {
                var currentWave = _hews[ selectedBarTime ];

                ref var hew = ref currentWave.GetWaveFromScenario( waveScenarioNo );

                var allWavesDesc = hew.GetAllWaves( );
                allWavesDesc.Reverse();

                var listIterator = allWavesDesc.GetEnumerator( );

                if ( listIterator.MoveNext() )                                  // Ignore the Wave C
                {
                    if ( listIterator.MoveNext() )                              // This should normally be wave B
                    {
                        var lastWaveName = listIterator.Current.WaveName;
                        var lastWaveCycle = listIterator.Current.WaveCycle;

                        while ( lastWaveName == ElliottWaveEnum.WaveA || lastWaveName == ElliottWaveEnum.WaveB && ( lastWaveCycle == currentWaveDegree ) )
                        {
                            if ( listIterator.MoveNext() )
                            {
                                lastWaveName = listIterator.Current.WaveName;
                                lastWaveCycle = listIterator.Current.WaveCycle;
                            }
                            else
                            {
                                break;
                            }
                        }

                        if ( ( lastWaveCycle - currentWaveDegree ) >= GlobalConstants.OneWaveCycle )
                        {
                            found = true;
                        }
                    }
                }

                if ( found )
                {
                    return true;
                }
                else
                {
                    return CanUpgradeTo1C( waveScenarioNo, selectedBarTime, currentWaveDegree, currentWaveName, waveNameTobeAdded, false );
                }
            }

            return false;
        }

        public bool CanUpgradeTo3C( int waveScenarioNo, long selectedBarTime, ElliottWaveCycle currentWaveDegree, ElliottWaveEnum currentWaveName, ElliottWaveEnum waveNameTobeAdded, bool isSpecialBar )
        {
            if ( currentWaveName != ElliottWaveEnum.WaveC && waveNameTobeAdded != ElliottWaveEnum.Wave3 )
            {
                throw new InvalidProgramException();
            }

            if ( isSpecialBar )
            {
                return SpecialBarCanUpgradeTo3C( waveScenarioNo, selectedBarTime, currentWaveDegree, currentWaveName, waveNameTobeAdded );
            }

            var beginningWaveRawTime = FindBeginningWaveOfCurrentABCX( waveScenarioNo, selectedBarTime, currentWaveDegree );

            if ( beginningWaveRawTime != -1 )
            {
                var beginWaveHewInfoure = _hews[ beginningWaveRawTime ];

                ref var hew = ref beginWaveHewInfoure.GetWaveFromScenario( waveScenarioNo );

                var nextHigherWaveName = hew.GetWavesAtCycle( currentWaveDegree );

                if ( nextHigherWaveName.Count > 0 && nextHigherWaveName[ 0 ] == ElliottWaveEnum.Wave2 )
                {
                    return true;
                }
            }

            return false;
        }

        private bool SpecialBarCanUpgradeTo3C( int waveScenarioNo, long selectedBarTime, ElliottWaveCycle currentWaveDegree, ElliottWaveEnum currentWaveName, ElliottWaveEnum waveNameTobeAdded )
        {
            bool found = false;

            if ( _hews.ContainsKey( selectedBarTime ) )
            {
                var currentWave = _hews[ selectedBarTime ];

                ref var hew = ref currentWave.GetWaveFromScenario( waveScenarioNo );

                var allWavesDesc = hew.GetAllWaves( );
                allWavesDesc.Reverse();

                var listIterator = allWavesDesc.GetEnumerator( );

                if ( listIterator.MoveNext() )                                  // Ignore the Wave C
                {
                    if ( listIterator.MoveNext() )                              // This should normally be wave B
                    {
                        var lastWaveName = listIterator.Current.WaveName;
                        var lastWaveCycle = listIterator.Current.WaveCycle;

                        while ( lastWaveName == ElliottWaveEnum.WaveA || lastWaveName == ElliottWaveEnum.WaveB && ( lastWaveCycle == currentWaveDegree ) )
                        {
                            if ( listIterator.MoveNext() )
                            {
                                lastWaveName = listIterator.Current.WaveName;
                                lastWaveCycle = listIterator.Current.WaveCycle;
                            }
                            else
                            {
                                break;
                            }
                        }

                        if ( lastWaveName == ElliottWaveEnum.Wave2 && ( lastWaveCycle == currentWaveDegree ) )
                        {
                            found = true;
                        }
                    }
                }

                if ( found )
                {
                    return true;
                }
                else
                {
                    return CanUpgradeTo3C( waveScenarioNo, selectedBarTime, currentWaveDegree, currentWaveName, waveNameTobeAdded, false );
                }
            }

            return false;
        }

        public bool CanUpgradeTo5C( int waveScenarioNo, long selectedBarTime, ElliottWaveCycle currentWaveDegree, ElliottWaveEnum currentWaveName, ElliottWaveEnum waveNameTobeAdded, bool isSpecialBar )
        {
            if ( currentWaveName != ElliottWaveEnum.WaveC && waveNameTobeAdded != ElliottWaveEnum.Wave5 )
            {
                throw new InvalidProgramException();
            }

            if ( isSpecialBar )
            {
                return SpecialBarCanUpgradeTo5C( waveScenarioNo, selectedBarTime, currentWaveDegree, currentWaveName, waveNameTobeAdded );
            }

            var beginningWaveRawTime = FindBeginningWaveOfCurrentABCX( waveScenarioNo,selectedBarTime, currentWaveDegree );

            if ( beginningWaveRawTime != -1 )
            {
                var beginWaveHewInfoure = _hews[ beginningWaveRawTime ];

                ref var hew = ref beginWaveHewInfoure.GetWaveFromScenario( waveScenarioNo );

                var nextHigherWaveName = hew.GetWavesAtCycle( currentWaveDegree );

                if ( nextHigherWaveName.Count > 0 && nextHigherWaveName[ 0 ] == ElliottWaveEnum.Wave4 )
                {
                    return true;
                }
            }

            return false;
        }

        private bool SpecialBarCanUpgradeTo5C( int waveScenarioNo, long selectedBarTime, ElliottWaveCycle currentWaveDegree, ElliottWaveEnum currentWaveName, ElliottWaveEnum waveNameTobeAdded )
        {
            bool found = false;

            if ( _hews.ContainsKey( selectedBarTime ) )
            {
                var currentWave = _hews[ selectedBarTime ];

                ref var hew = ref currentWave.GetWaveFromScenario( waveScenarioNo );

                var allWavesDesc = hew.GetAllWaves( );
                allWavesDesc.Reverse();

                var listIterator = allWavesDesc.GetEnumerator( );

                if ( listIterator.MoveNext() )                                  // Ignore the Wave C
                {
                    if ( listIterator.MoveNext() )                              // This should normally be wave B
                    {
                        var lastWaveName = listIterator.Current.WaveName;
                        var lastWaveCycle = listIterator.Current.WaveCycle;

                        while ( lastWaveName == ElliottWaveEnum.WaveA || lastWaveName == ElliottWaveEnum.WaveB && ( lastWaveCycle == currentWaveDegree ) )
                        {
                            if ( listIterator.MoveNext() )
                            {
                                lastWaveName = listIterator.Current.WaveName;
                                lastWaveCycle = listIterator.Current.WaveCycle;
                            }
                            else
                            {
                                break;
                            }
                        }

                        if ( lastWaveName == ElliottWaveEnum.Wave4 && ( lastWaveCycle == currentWaveDegree ) )
                        {
                            found = true;
                        }
                    }
                }

                if ( found )
                {
                    return true;
                }
                else
                {
                    return CanUpgradeTo5C( waveScenarioNo, selectedBarTime, currentWaveDegree, currentWaveName, waveNameTobeAdded, false );
                }
            }

            return false;
        }

        private bool IntelliAddWaveToCurrentBar(
                                                    int waveScenarioNo,
                                                    TimeSpan responsibleForWhatTimeFrame,
                                                    long selectedBarTime,
                                                    ElliottWaveCycle waveCycle,
                                                    ElliottWaveEnum waveNameToBeAdded,
                                                    ref SBar bar,
                                                    bool isSpecialBar,
                                                    bool isOutsideBar )
        {
            var allWaves = GetAllWavesDescending( waveScenarioNo,selectedBarTime );

            if ( allWaves.Count == 0 )
            {
                return false;
            }

            bool doneAdding = false;

            var highestDegreeHewPointInfo = GetWaveOfHigestDegree( waveScenarioNo,selectedBarTime );
            var highestDegreeCycle = highestDegreeHewPointInfo.HasValue ? highestDegreeHewPointInfo.Value.WaveCycle : ElliottWaveCycle.UNKNOWN;

            var lowestDegreeHewPointInfo = GetWaveOfLowestDegree( waveScenarioNo,selectedBarTime );
            var lowestDegreeCycle = lowestDegreeHewPointInfo.HasValue ? lowestDegreeHewPointInfo.Value.WaveCycle : ElliottWaveCycle.UNKNOWN;

            var oneDegreeHigher = highestDegreeCycle + GlobalConstants.OneWaveCycle;
            var oneDegreeLower = lowestDegreeCycle - GlobalConstants.OneWaveCycle;

            var previousWaveHigherDegreeWaveName = GetPreviousWaveOfDegree( waveScenarioNo,selectedBarTime, oneDegreeHigher );
            var previousWaveLowerDegreeWaveName = GetPreviousWaveOfDegree( waveScenarioNo, selectedBarTime, oneDegreeLower );

            var currentWaveName = GetWaveOfDegree( waveScenarioNo,selectedBarTime, waveCycle );

            if ( allWaves.Count == 1 && allWaves[ 0 ].WaveName == ElliottWaveEnum.WaveC )
            {
                if ( waveNameToBeAdded == ElliottWaveEnum.Wave1 || waveNameToBeAdded == ElliottWaveEnum.Wave3 || waveNameToBeAdded == ElliottWaveEnum.Wave5 )
                {
                    var newWaveName = ElliottWaveEnum.NONE;

                    switch ( waveNameToBeAdded )
                    {
                        case ElliottWaveEnum.Wave1:
                            newWaveName = ElliottWaveEnum.Wave1C;
                            break;

                        case ElliottWaveEnum.Wave3:
                            newWaveName = ElliottWaveEnum.Wave3C;
                            break;

                        case ElliottWaveEnum.Wave5:
                            newWaveName = ElliottWaveEnum.Wave5C;
                            break;
                    }

                    ReplaceWaveInManagerAndBar( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, waveCycle, waveNameToBeAdded, newWaveName, ref bar );

                    return true;
                }
            }

            if ( previousWaveHigherDegreeWaveName != ElliottWaveEnum.NONE )
            {
                var previousWaveTime = GetRawBarTimeOfPreviousWaveOfDegree( waveScenarioNo,selectedBarTime, oneDegreeHigher );

                if ( previousWaveHigherDegreeWaveName.IsNextWave( waveNameToBeAdded ) && NoHigherWaveBetween( waveScenarioNo, previousWaveTime, selectedBarTime, oneDegreeHigher ) )
                {
                    /*
                     * We can see that my code will first check if the higher degree is next wave before checking for the lower degree next wave, so it caused the above bug 
                     * 
                     * In order to solve this problem, I think I will also need to check if the current degree can co-exist with this higher degree.
                     */

                    if ( waveNameToBeAdded.IsValidLowerDegreeWave( currentWaveName ) )
                    {
                        SmartAddWaveXtoManagerAndBar( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, oneDegreeHigher, waveNameToBeAdded, ref bar );
                    }
                }
            }

            if ( previousWaveLowerDegreeWaveName != ElliottWaveEnum.NONE && doneAdding == false )
            {
                var previousWaveTime = GetRawBarTimeOfPreviousWaveOfDegree( waveScenarioNo,selectedBarTime, oneDegreeLower );

                if ( previousWaveLowerDegreeWaveName.IsNextWave( waveNameToBeAdded ) && NoHigherWaveBetween( waveScenarioNo, previousWaveTime, selectedBarTime, oneDegreeLower ) )
                {
                    if ( previousWaveLowerDegreeWaveName == ElliottWaveEnum.WaveB )
                    {
                        AddWaveC( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, oneDegreeLower, waveNameToBeAdded, ref bar, isOutsideBar );
                    }
                    else
                    {
                        SmartAddWaveXtoManagerAndBar( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, oneDegreeLower, waveNameToBeAdded, ref bar );
                    }

                    doneAdding = true;
                }
                else if ( waveNameToBeAdded == ElliottWaveEnum.Wave5 || waveNameToBeAdded == ElliottWaveEnum.Wave5C || waveNameToBeAdded == ElliottWaveEnum.WaveC )
                {
                    SmartAddWaveXtoManagerAndBar( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, oneDegreeLower, waveNameToBeAdded, ref bar );
                    doneAdding = true;
                }
            }

            if ( doneAdding == false )
            {
                if ( waveNameToBeAdded == ElliottWaveEnum.Wave5 || waveNameToBeAdded == ElliottWaveEnum.Wave5C || waveNameToBeAdded == ElliottWaveEnum.WaveC )
                {
                    var lowestDegreeName = lowestDegreeHewPointInfo.HasValue ? lowestDegreeHewPointInfo.Value.WaveName : ElliottWaveEnum.NONE;

                    switch ( lowestDegreeName )
                    {
                        case ElliottWaveEnum.Wave1C:
                        case ElliottWaveEnum.Wave3C:
                        case ElliottWaveEnum.Wave5C:
                        case ElliottWaveEnum.WaveA:
                        case ElliottWaveEnum.WaveC:

                            if ( waveNameToBeAdded == ElliottWaveEnum.Wave5 )
                            {
                                SmartAddWaveXtoManagerAndBar( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, oneDegreeLower, waveNameToBeAdded, ref bar );
                            }

                            break;

                        // Tony Lam: Need to work on adding wave C to this wave before adding lower degree wave.
                        case ElliottWaveEnum.Wave1:
                            if ( waveNameToBeAdded == ElliottWaveEnum.WaveC )
                            {
                                ReplaceWaveInManagerAndBar( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, lowestDegreeCycle, ElliottWaveEnum.Wave1, ElliottWaveEnum.Wave1C, ref bar );
                            }
                            else
                            {
                                SmartAddWaveXtoManagerAndBar( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, oneDegreeLower, waveNameToBeAdded, ref bar );
                            }
                            break;

                        case ElliottWaveEnum.Wave3:
                            if ( waveNameToBeAdded == ElliottWaveEnum.WaveC )
                            {
                                ReplaceWaveInManagerAndBar( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, lowestDegreeCycle, ElliottWaveEnum.Wave3, ElliottWaveEnum.Wave3C, ref bar );
                            }
                            else
                            {
                                SmartAddWaveXtoManagerAndBar( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, oneDegreeLower, waveNameToBeAdded, ref bar );
                            }
                            break;

                        case ElliottWaveEnum.Wave5:
                            if ( waveNameToBeAdded == ElliottWaveEnum.WaveC )
                            {
                                ReplaceWaveInManagerAndBar( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, lowestDegreeCycle, ElliottWaveEnum.Wave5, ElliottWaveEnum.Wave5C, ref bar );
                            }
                            else
                            {
                                SmartAddWaveXtoManagerAndBar( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, oneDegreeLower, waveNameToBeAdded, ref bar );
                            }

                            break;

                        case ElliottWaveEnum.WaveB:
                        case ElliottWaveEnum.WaveX:
                            if ( waveNameToBeAdded == ElliottWaveEnum.WaveC )
                            {
                                SmartAddWaveXtoManagerAndBar( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, oneDegreeLower, waveNameToBeAdded, ref bar );
                            }
                            break;

                        default:
                            Console.Beep();
                            break;
                    }
                }
            }

            return true;
        }

        private WaveInfo GetNormalWavePositionAndCycle( TimeSpan responsibleForWhatTimeFrame,
                                                            long selectedBarTime,
                                                            ElliottWaveCycle waveCycle,
                                                            ElliottWaveEnum waveName,
                                                            ref SBar bar,
                                                            bool isOutsideBar )
        {
            throw new NotImplementedException();
        }

        public UndoRedoArea GetSelectedUndoRedoArea( TimeSpan period )
        {
            if ( period == TimeSpan.FromSeconds( 1 ) )
            {
                return _01SecondUndoArea;
            }
            else if ( period == TimeSpan.FromMinutes( 1 ) )
            {
                return _01minsUndoArea;
            }
            else if ( period == TimeSpan.FromMinutes( 4 ) )
            {
                return _04minsUndoArea;
            }
            else if ( period == TimeSpan.FromMinutes( 5 ) )
            {
                return _05minsUndoArea;
            }
            else if ( period == TimeSpan.FromMinutes( 15 ) )
            {
                return _15minsUndoArea;
            }
            else if ( period == TimeSpan.FromMinutes( 30 ) )
            {
                return _30minsUndoArea;
            }
            else if ( period == TimeSpan.FromHours( 1 ) )
            {
                return _01HourUndoArea;
            }
            else if ( period == TimeSpan.FromHours( 2 ) )
            {
                return _02HourUndoArea;
            }
            else if ( period == TimeSpan.FromHours( 3 ) )
            {
                return _03HourUndoArea;
            }
            else if ( period == TimeSpan.FromHours( 4 ) )
            {
                return _04HourUndoArea;
            }
            else if ( period == TimeSpan.FromHours( 6 ) )
            {
                return _06HourUndoArea;
            }
            else if ( period == TimeSpan.FromHours( 8 ) )
            {
                return _08HourUndoArea;
            }
            else if ( period == TimeSpan.FromDays( 1 ) )
            {
                return _dailyUndoArea;
            }
            else if ( period == TimeSpan.FromDays( 7 ) )
            {
                return _weeklyUndoArea;
            }
            else if ( period == TimeSpan.FromDays( 30 ) )
            {
                return _monthlyUndoArea;
            }

            return null;
        }
        #endregion
    }
}
