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
using fx.Collections;
using fx.Bars;

namespace fx.Algorithm
{
    public partial class HewManager
    {
        #region 10 - Cycles
        public ElliottWaveCycle FindCurrentWaveCycle( int waveScenarioNo, TimeSpan responsibleForWhatTimeFrame,
                                                      long rawBarTime,
                                                      ElliottWaveCycle cycle,
                                                      ElliottWaveEnum currentWaveName,
                                                      ref SBar bar )
        {
            if( !_smartWaveLabeling )
            {
                return cycle;
            }

            _selectedWaveImportanceDict = GetWaveImportanceDictionary( _currentActiveTimeFrame );

            var output = cycle;
            var dbHewPrev = GetPreviousWaveStructure( waveScenarioNo, rawBarTime );
            var dbHewNext = GetNextWaveStructure( waveScenarioNo, rawBarTime );
            output = harmonicEWave.FindCurrentWaveCycle( waveScenarioNo, rawBarTime, cycle, currentWaveName, ref bar, dbHewPrev, dbHewNext, _selectedWaveImportanceDict, _offerId, responsibleForWhatTimeFrame );

            Messenger.Default.Send( new HewMessage( output ) );

            return output;
        }

        //public ElliottWaveCycle FindCycleFromPreviousWithMultipleWaves( DbElliottWave previousDB,
        //                                                                  TimeSpan responsibleForWhatTimeFrame,
        //                                                                  long rawBarTime,
        //                                                                  ElliottWaveCycle cycle,
        //                                                                  ElliottWaveEnum currentWaveName,
        //                                                                  ref SBar bar )
        //{
        //    var previousHighestWave = previousDB.GetElliottWave().GetFirstHighestWaveInfo( );
        //    var previousHighestWaveCycle = previousHighestWave.HasValue ? previousHighestWave.Value.WaveCycle : ElliottWaveCycle.UNKNOWN;

        //    var estNextWaveImportance = -1;
        //    var estWaveImportance = -1;

        //    switch( previousHighestWave.Value.WaveName )
        //    {
        //        case ElliottWaveEnum.Wave1:
        //        case ElliottWaveEnum.Wave1C:
        //        {
        //        }
        //            break;

        //        case ElliottWaveEnum.Wave2:

        //            break;

        //        case ElliottWaveEnum.Wave3:
        //        case ElliottWaveEnum.Wave3C:
        //            break;

        //        case ElliottWaveEnum.Wave4:
        //            //SmartProcessingWave4( responsibleForWhatTimeFrame, rawBarTime, bar );
        //            break;
        //        case ElliottWaveEnum.Wave5:
        //        case ElliottWaveEnum.Wave5C:

        //            break;

        //        case ElliottWaveEnum.WaveA:

        //            break;
        //        case ElliottWaveEnum.WaveB:

        //            break;
        //        case ElliottWaveEnum.WaveC:

        //            break;
        //        case ElliottWaveEnum.WaveX:

        //            break;
        //        case ElliottWaveEnum.WaveTA:

        //            break;
        //        case ElliottWaveEnum.WaveTB:

        //            break;
        //        case ElliottWaveEnum.WaveTC:

        //            break;
        //        case ElliottWaveEnum.WaveTD:

        //            break;
        //        case ElliottWaveEnum.WaveTE:

        //            break;
        //        case ElliottWaveEnum.WaveEFA:

        //            break;
        //        case ElliottWaveEnum.WaveEFB:

        //            break;
        //        case ElliottWaveEnum.WaveEFC:

        //            break;
        //    }

        //    if( _selectedWaveImportanceDict != null )
        //    {
        //        if( _selectedWaveImportanceDict.ContainsKey( rawBarTime ) )
        //        {
        //            estWaveImportance = _selectedWaveImportanceDict[ rawBarTime ].WaveImportance;
        //        }

        //        if( _selectedWaveImportanceDict.ContainsKey( previousDB.StartDate ) )
        //        {
        //            estNextWaveImportance = _selectedWaveImportanceDict[ previousDB.StartDate ].WaveImportance;
        //        }
        //    }

        //    /*  
        //    *  -----------------------------------------------------------------------------------------------------------------------------
        //    *      Since they are neither of the above, we want to check from the wave Estimation.
        //    *      1) Since they are both of the same wave Degress, the WaveImportance should be either the same or off by one level
        //    *      2) If the current Wave's WaveImportance is of the same as those previous and Next wave, they should be of the same
        //    *         degree.
        //    *  -----------------------------------------------------------------------------------------------------------------------------
        //    */            ElliottWaveCycle output = cycle;
        //    DbSmartWaveCycles estWaveCycle = null;

        //    if( estNextWaveImportance > -1 && estWaveImportance > -1 )
        //    {
        //        if( estWaveImportance > estNextWaveImportance )
        //        {
        //            output = previousHighestWaveCycle + GlobalConstants.OneWaveCycle;
        //        }
        //        else if( estWaveImportance < estNextWaveImportance )
        //        {
        //            output = previousHighestWaveCycle - GlobalConstants.OneWaveCycle;
        //        }
        //        else if( estWaveImportance == estNextWaveImportance )
        //        {
        //            output = previousHighestWaveCycle;
        //        }
        //        else
        //        {
        //            estWaveCycle = GetWaveCycleEstimation( _offerId, responsibleForWhatTimeFrame, estWaveImportance );
        //            if( estWaveCycle != null )
        //            {
        //                if( estWaveCycle.WaveCycle1 <= previousHighestWaveCycle && estWaveCycle.WaveCycle1 >= cycle )
        //                {
        //                    output = estWaveCycle.WaveCycle1;
        //                }
        //                else
        //                {
        //                    throw new NotImplementedException( );
        //                }
        //            }
        //        }
        //    }

        //    return output;
        //}

        public static DbSmartWaveCycles GetWaveCycleEstimation(
                                                                    int offerId,
                                                                    TimeSpan timeframe,
                                                                    int waveImportance )
        {
            DbSmartWaveCycles output = null;

            string periodString = FinancialHelper.GetPeriodString( timeframe );

            using( var context = new ForexDatabars( ) )
            {
                var found = from b in context.SMARTWAVECYCLES where b.Period == periodString && b.WaveImportance == waveImportance select b;

                if( found.Any( ) )
                {
                    return found.FirstOrDefault( );
                }
                else
                {
                    if( timeframe == TimeSpan.FromMinutes( 1 ) )
                    {
                        if( waveImportance == 89 )
                        {
                        }
                        else if( waveImportance == 55 )
                        {
                        }
                        else if( waveImportance == 34 )
                        {
                        }
                        else if( waveImportance == 21 )
                        {
                        }
                        else if( waveImportance == 13 )
                        {
                        }
                        else if( waveImportance == 8 )
                        {
                        }
                        else if( waveImportance == 5 )
                        {
                        }
                    }
                    else if( timeframe == TimeSpan.FromMinutes( 5 ) )
                    {
                        if( waveImportance == 89 )
                        {
                        }
                        else if( waveImportance == 55 )
                        {
                        }
                        else if( waveImportance == 34 )
                        {
                        }
                        else if( waveImportance == 21 )
                        {
                        }
                        else if( waveImportance == 13 )
                        {
                        }
                        else if( waveImportance == 8 )
                        {
                        }
                        else if( waveImportance == 5 )
                        {
                        }
                    }
                    else if( timeframe == TimeSpan.FromMinutes( 15 ) )
                    {
                        if( waveImportance == 89 )
                        {
                        }
                        else if( waveImportance == 55 )
                        {
                        }
                        else if( waveImportance == 34 )
                        {
                        }
                        else if( waveImportance == 21 )
                        {
                        }
                        else if( waveImportance == 13 )
                        {
                        }
                        else if( waveImportance == 8 )
                        {
                        }
                        else if( waveImportance == 5 )
                        {
                        }
                    }
                    else if( timeframe == TimeSpan.FromMinutes( 60 ) )
                    {
                        if( waveImportance == 89 )
                        {
                        }
                        else if( waveImportance == 55 )
                        {
                        }
                        else if( waveImportance == 34 )
                        {
                        }
                        else if( waveImportance == 21 )
                        {
                        }
                        else if( waveImportance == 13 )
                        {
                        }
                        else if( waveImportance == 8 )
                        {
                        }
                        else if( waveImportance == 5 )
                        {
                        }
                    }
                    else if( timeframe == TimeSpan.FromDays( 1 ) )
                    {
                        if( waveImportance == 89 )
                        {
                            output = new DbSmartWaveCycles( offerId, periodString, waveImportance, ElliottWaveCycle.Intermediate );
                        }
                        else if( waveImportance == 55 )
                        {
                            output = new DbSmartWaveCycles( offerId, periodString, waveImportance, ElliottWaveCycle.Intermediate );
                        }
                        else if( waveImportance == 34 )
                        {
                            output = new DbSmartWaveCycles( offerId, periodString, waveImportance, ElliottWaveCycle.Minor );
                        }
                        else if( waveImportance == 21 )
                        {
                            output = new DbSmartWaveCycles( offerId, periodString, waveImportance, ElliottWaveCycle.Minor );
                        }
                        else if( waveImportance == 13 )
                        {
                            output = new DbSmartWaveCycles( offerId, periodString, waveImportance, ElliottWaveCycle.Minor );
                        }
                        else if( waveImportance == 8 )
                        {
                            output = new DbSmartWaveCycles( offerId, periodString, waveImportance, ElliottWaveCycle.Minute );
                        }
                        else if( waveImportance == 5 )
                        {
                            output = new DbSmartWaveCycles( offerId, periodString, waveImportance, ElliottWaveCycle.Minute );
                        }
                    }
                    else if( timeframe == TimeSpan.FromDays( 7 ) )
                    {
                        if( waveImportance == 89 )
                        {
                            output = new DbSmartWaveCycles( offerId, periodString, waveImportance, ElliottWaveCycle.Supercycle );
                        }
                        else if( waveImportance == 55 )
                        {
                            output = new DbSmartWaveCycles( offerId, periodString, waveImportance, ElliottWaveCycle.Supercycle );
                        }
                        else if( waveImportance == 34 )
                        {
                            output = new DbSmartWaveCycles( offerId, periodString, waveImportance, ElliottWaveCycle.Intermediate );
                        }
                        else if( waveImportance == 21 )
                        {
                            output = new DbSmartWaveCycles( offerId, periodString, waveImportance, ElliottWaveCycle.Intermediate );
                        }
                        else if( waveImportance == 13 )
                        {
                            output = new DbSmartWaveCycles( offerId, periodString, waveImportance, ElliottWaveCycle.Intermediate );
                        }
                        else if( waveImportance == 8 )
                        {
                            output = new DbSmartWaveCycles( offerId, periodString, waveImportance, ElliottWaveCycle.Minor );
                        }
                        else if( waveImportance == 5 )
                        {
                            output = new DbSmartWaveCycles( offerId, periodString, waveImportance, ElliottWaveCycle.Minor );
                        }
                    }
                    else if( timeframe == TimeSpan.FromDays( 30 ) )
                    {
                        if( waveImportance == 89 )
                        {
                            output = new DbSmartWaveCycles( offerId, periodString, waveImportance, ElliottWaveCycle.GrandSupercycle );
                        }
                        else if( waveImportance == 55 )
                        {
                            output = new DbSmartWaveCycles( offerId, periodString, waveImportance, ElliottWaveCycle.GrandSupercycle );
                        }
                        else if( waveImportance == 34 )
                        {
                            output = new DbSmartWaveCycles( offerId, periodString, waveImportance, ElliottWaveCycle.Supercycle );
                        }
                        else if( waveImportance == 21 )
                        {
                            output = new DbSmartWaveCycles( offerId, periodString, waveImportance, ElliottWaveCycle.Supercycle );
                        }
                        else if( waveImportance == 13 )
                        {
                            output = new DbSmartWaveCycles( offerId, periodString, waveImportance, ElliottWaveCycle.Supercycle );
                        }
                        else if( waveImportance == 8 )
                        {
                            output = new DbSmartWaveCycles( offerId, periodString, waveImportance, ElliottWaveCycle.Intermediate );
                        }
                        else if( waveImportance == 5 )
                        {
                            output = new DbSmartWaveCycles( offerId, periodString, waveImportance, ElliottWaveCycle.Intermediate );
                        }
                    }
                }
            }

            return output;
        }

        //public ElliottWaveCycle FindCycleFromPreviousOneWave( DbElliottWave previousDB,
        //                                                      TimeSpan responsibleForWhatTimeFrame,
        //                                                      long rawBarTime,
        //                                                      ElliottWaveCycle cycle,
        //                                                      ElliottWaveEnum currentWaveName,
        //                                                      ref SBar bar )
        //{
        //    var previousWave = previousDB.GetElliottWave().GetFirstWave( );

        //    if( previousWave.HasValue )
        //    {
        //        if( previousWave.Value.WaveName.IsNextWave( currentWaveName ) )
        //        {
        //            return previousWave.Value.WaveCycle;
        //        }

        //        else if( previousWave.Value.WaveName.IsNextWaveOneDegreeLowerBeginning( currentWaveName ) )
        //        {
        //            return previousWave.Value.WaveCycle - GlobalConstants.OneWaveCycle;
        //        }
        //        else if( previousWave.Value.WaveName.IsNextWaveTwoDegreesLowerBeginning( currentWaveName ) )
        //        {
        //            return ( previousWave.Value.WaveCycle - GlobalConstants.OneWaveCycle - GlobalConstants.OneWaveCycle );
        //        }
        //        else if( previousWave.Value.WaveName.IsOneDegreeLowerEndingWrt( currentWaveName ) )
        //        {
        //            /* ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //             * 
        //             * Since the 1st Wave is one degree lower, so the current wave should be one degree higher than the 1st Wave
        //             * 
        //             * ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //             */
        //            return previousWave.Value.WaveCycle + GlobalConstants.OneWaveCycle;
        //        }
        //    }

        //    var previousHighestWave = previousDB.GetElliottWave().GetFirstHighestWaveInfo( );
        //    var previousHighestWaveCycle = previousHighestWave.HasValue ? previousHighestWave.Value.WaveCycle : ElliottWaveCycle.UNKNOWN;

        //    var estNextWaveImportance = -1;
        //    var estWaveImportance = -1;

        //    if( _selectedWaveImportanceDict != null )
        //    {
        //        if( _selectedWaveImportanceDict.ContainsKey( rawBarTime ) )
        //        {
        //            estWaveImportance = _selectedWaveImportanceDict[ rawBarTime ].WaveImportance;
        //        }

        //        if( _selectedWaveImportanceDict.ContainsKey( previousDB.StartDate ) )
        //        {
        //            estNextWaveImportance = _selectedWaveImportanceDict[ previousDB.StartDate ].WaveImportance;
        //        }
        //    }

        //    /*  
        //    *  -----------------------------------------------------------------------------------------------------------------------------
        //    *      Since they are neither of the above, we want to check from the wave Estimation.
        //    *      1) Since they are both of the same wave Degress, the WaveImportance should be either the same or off by one level
        //    *      2) If the current Wave's WaveImportance is of the same as those previous and Next wave, they should be of the same
        //    *         degree.
        //    *  -----------------------------------------------------------------------------------------------------------------------------
        //    */            ElliottWaveCycle output = cycle;
        //    DbSmartWaveCycles estWaveCycle = null;

        //    if( estNextWaveImportance > -1 && estWaveImportance > -1 )
        //    {
        //        if( estWaveImportance > estNextWaveImportance )
        //        {
        //            output = previousHighestWaveCycle + GlobalConstants.OneWaveCycle;
        //        }
        //        else if( estWaveImportance < estNextWaveImportance )
        //        {
        //            output = previousHighestWaveCycle - GlobalConstants.OneWaveCycle;
        //        }
        //        else if( estWaveImportance == estNextWaveImportance )
        //        {
        //            output = previousHighestWaveCycle;
        //        }
        //        else
        //        {
        //            estWaveCycle = GetWaveCycleEstimation( _offerId, responsibleForWhatTimeFrame, estWaveImportance );
        //            if( estWaveCycle != null )
        //            {
        //                if( estWaveCycle.WaveCycle1 <= previousHighestWaveCycle && estWaveCycle.WaveCycle1 >= cycle )
        //                {
        //                    output = estWaveCycle.WaveCycle1;
        //                }
        //                else
        //                {
        //                    throw new NotImplementedException( );
        //                }
        //            }
        //        }
        //    }

        //    return output;
        //}

        //public bool RelocateWaveAndSyncDown( int waveScenarioNo,
        //                                        TimeSpan responsibleForWhatTimeFrame,
        //                                     long firstBarTime,
        //                                     long secondBarTime,
        //                                     ref SBar firstBar,
        //                                     ref SBar secondBar )
        //{
        //    var periodString = FinancialHelper.GetPeriodId( responsibleForWhatTimeFrame );

        //    var currentWaveDict = GetElliottWavesDictionary( responsibleForWhatTimeFrame );

        //    var currentUndoRedo = GetSelectedUndoRedoArea( responsibleForWhatTimeFrame );

        //    DbElliottWave foundWave = null;

        //    using( currentUndoRedo.Start( "RelocateWaveAndSyncDown" ) )
        //    {
        //        if( currentWaveDict.ContainsKey( firstBarTime ) )
        //        {
        //            foundWave = currentWaveDict[ firstBarTime ];

        //            firstBar.DetachWaveFromDatabar( );

        //            foundWave.ChangeOwningBar( ref secondBar );

        //            //secondBar.GetElliottWave() = foundWave.GetElliottWave();

        //            currentWaveDict.Remove( firstBarTime );

        //            currentWaveDict.Add( secondBarTime, foundWave );
        //        }
        //        else if( currentWaveDict.ContainsKey( secondBarTime ) )
        //        {
        //            foundWave = currentWaveDict[ secondBarTime ];

        //            secondBar.DetachWaveFromDatabar( );

        //            foundWave.ChangeOwningBar( ref firstBar );

        //            currentWaveDict.Remove( secondBarTime );

        //            currentWaveDict.Add( firstBarTime, foundWave );
        //        }
        //        else
        //        {
        //            return false;
        //        }

        //        currentUndoRedo.Commit( );
        //    }

        //    // After relocating the wave info to another bar.
        //    // We need to update the database as well as update Wave of higher and lower timeframe.

        //    if( responsibleForWhatTimeFrame > TimeSpan.FromMinutes( 1 ) && foundWave != null )
        //    {
        //        TimeSpan lowerTimeSpan = TimeSpan.MinValue;

        //        var aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

        //        if ( aa != null )
        //        {
        //            lowerTimeSpan = aa.GetOneTimeSpanLower( responsibleForWhatTimeFrame );
        //        }
        //        else
        //        {
        //            return false;
        //        }                

        //        if( lowerTimeSpan != TimeSpan.Zero )
        //        {
        //            var lowerTimespanBarRepo = GetDatabarsRepository( lowerTimeSpan );

        //            if( foundWave.WaveLabelPosition == WaveLabelPosition.TOP )
        //            {
        //                ref SBar firstBarLowerTF = ref lowerTimespanBarRepo.GetHighestBarOfTheRange( firstBarTime.FromLinuxTime(), responsibleForWhatTimeFrame ); 
        //                ref SBar secondBarLowerTF = ref lowerTimespanBarRepo.GetHighestBarOfTheRange( secondBarTime.FromLinuxTime(), responsibleForWhatTimeFrame );

        //                if(  firstBarLowerTF != SBar.EmptySBar && secondBarLowerTF != SBar.EmptySBar )
        //                {                            
        //                    return ( RelocateWaveAndSyncDown( isMainCount, lowerTimeSpan, firstBarLowerTF.LinuxTime, secondBarLowerTF.LinuxTime, ref firstBarLowerTF, ref secondBarLowerTF ) );
        //                }
        //            }
        //            else if( foundWave.WaveLabelPosition == WaveLabelPosition.BOTTOM )
        //            {
        //                ref SBar firstBarLowerTF = ref lowerTimespanBarRepo.GetLowestBarOfTheRange( firstBarTime.FromLinuxTime(), responsibleForWhatTimeFrame );
        //                ref SBar secondBarLowerTF = ref lowerTimespanBarRepo.GetLowestBarOfTheRange( secondBarTime.FromLinuxTime(), responsibleForWhatTimeFrame );

        //                if ( firstBarLowerTF != SBar.EmptySBar && secondBarLowerTF != SBar.EmptySBar )                            
        //                {                            
        //                    return ( RelocateWaveAndSyncDown( isMainCount, lowerTimeSpan, firstBarLowerTF.LinuxTime, secondBarLowerTF.LinuxTime, ref firstBarLowerTF, ref secondBarLowerTF ) );
        //                }
        //            }
        //        }
        //    }

        //    return true;
        //}

        public bool RelocateWaveAndSyncUp( int waveScenarioNo,
                                            TimeSpan responsibleForWhatTimeFrame,
                                            long firstBarTime,
                                            long secondBarTime,
                                            ref SBar firstBar,
                                            ref SBar secondBar )
        {
            if( firstBarTime < 0 || secondBarTime < 0 || SBar.EmptySBar == firstBar || SBar.EmptySBar == secondBar )
            {
                return false;
            }

            var periodString = FinancialHelper.GetPeriodId( responsibleForWhatTimeFrame );

            var hews = GetElliottWavesDictionary( responsibleForWhatTimeFrame );

            DbElliottWave foundWave = null;

            TimeSpan higherTimeSpan = TimeSpan.MinValue;

            var aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

            if ( aa != null )
            {
                higherTimeSpan = aa.GetOneTimeSpanHigher( responsibleForWhatTimeFrame );
            }
            else
            {
                return false;
            }            

            var periodStringHigherTF = FinancialHelper.GetPeriodId( higherTimeSpan );

            var bars = GetDatabarsRepository( higherTimeSpan );

            DateTime firstBarDT = firstBarTime.FromLinuxTime( );

            DateTime secondBarDt = secondBarTime.FromLinuxTime( );

            ref SBar bar1stHTF = ref bars.GetBarContainingTime( firstBarDT, higherTimeSpan );

            ref SBar bar2ndHTF = ref bars.GetBarContainingTime( secondBarDt, higherTimeSpan );            

            if( bar1stHTF != SBar.EmptySBar || bar2ndHTF != SBar.EmptySBar )
            {
                return false;
            }
            

            var currentUndoRedo = GetSelectedUndoRedoArea( responsibleForWhatTimeFrame );

            using( currentUndoRedo.Start( "RelocateWaveAndSyncUp" ) )
            {
                if( hews.ContainsKey( firstBarTime ) )
                {
                    foundWave = hews[ firstBarTime ];

                    // We are moving waves from FirstBar to secondBar 
                    if( bar1stHTF != bar2ndHTF )
                    {
                        ref var waves = ref firstBar.GetWaveFromScenario( waveScenarioNo );
                        var hewsHTF       = GetElliottWavesDictionary( higherTimeSpan );
                        var higherTFRemoveList = GetSelectedRemovedWavesList( higherTimeSpan );

                        bar1stHTF.RemoveWavesFromDatabar( waveScenarioNo, waves );
                        bar2ndHTF.MergeWaves( waveScenarioNo, ref waves );

                        if( hewsHTF.ContainsKey( bar1stHTF.LinuxTime ) )
                        {
                            var higherWaves = hewsHTF[ bar1stHTF.LinuxTime ];

                            ref var hew = ref higherWaves.GetWaveFromScenario( waveScenarioNo ); 

                            if( hew.Count == 0 )
                            {
                                hewsHTF.Remove( bar1stHTF.LinuxTime );

                                if( higherTFRemoveList != null )
                                {
                                    var hfTime = bar1stHTF.LinuxTime;
                                    var index = higherTFRemoveList.FindIndex( x => x.Equals( hfTime ) );

                                    if( index == -1 )
                                    {
                                        _selectedRemovedWavesList.Add( bar1stHTF.LinuxTime );
                                    }
                                }
                            }
                        }
                    }
                }
                else if( hews.ContainsKey( secondBarTime ) )
                {
                    foundWave = hews[ secondBarTime ];

                    // We are moving waves from SecondBar to FirstBar
                    if( bar1stHTF != bar2ndHTF )
                    {
                        ref var waves          = ref secondBar.GetWaveFromScenario( waveScenarioNo );                        
                        var hewsHTF       = GetElliottWavesDictionary( higherTimeSpan );
                        var higherTFRemoveList = GetSelectedRemovedWavesList( higherTimeSpan );

                        bar1stHTF.MergeWaves( waveScenarioNo, ref waves );
                        bar2ndHTF.RemoveWavesFromDatabar( waveScenarioNo, waves );

                        if( hewsHTF.ContainsKey( bar2ndHTF.LinuxTime ) )
                        {
                            var higherWaves = hewsHTF[ bar2ndHTF.LinuxTime ];

                            ref var hew = ref higherWaves.GetWaveFromScenario( waveScenarioNo );

                            if ( hew.Count == 0 )
                            {
                                hewsHTF.Remove( bar2ndHTF.LinuxTime );

                                if( higherTFRemoveList != null )
                                {
                                    var hfTime = bar2ndHTF.LinuxTime;

                                    var index = higherTFRemoveList.FindIndex( x => x.Equals( hfTime ) );

                                    if( index == -1 )
                                    {
                                        _selectedRemovedWavesList.Add( bar2ndHTF.LinuxTime );
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    return false;
                }

                currentUndoRedo.Commit( );
            }

            return false;
        }

        #endregion

        public bool RestoreElliottWaveToDatabar(
                                                    fxHistoricBarsRepo bars,
                                                    TimeSpan period,
                                                    ref SBar tmpBar )
        {
            var hews = GetElliottWavesDictionary( period );

            long searchbarTime = -1;

            if( period == TimeSpan.FromTicks( 1 ) )
            {
                var barTimeToMin = tmpBar.BarTime.Truncate( TimeSpan.FromMinutes( 1 ) );
                searchbarTime = barTimeToMin.ToLinuxTime( );
            }
            else
            {
                searchbarTime = tmpBar.LinuxTime;
            }

            var waveTimeTaken = new PooledDictionary< long, bool >( );

            if ( hews.ContainsKey( searchbarTime ) )
            {                
                if ( waveTimeTaken.ContainsKey( searchbarTime ) )
                {
                    return false;
                }

                waveTimeTaken.Add( searchbarTime, true );

                var dbHew = hews[ searchbarTime ];

                tmpBar.Features.HasElliottWave = true;

                dbHew.AttachOwningBar( bars, ref tmpBar );

                ref var hew = ref dbHew.GetWaveFromScenario( 1 ); 

                var firstWave = hew.GetFirstHighestWaveInfo( );

                if( firstWave.HasValue )
                {
                    WavePriceTimeInfo output1 = default;                    

                    if( CalculateWavePriceTimeInfo( 1, bars, period, dbHew.StartDate, firstWave.Value.WaveName, firstWave.Value.WaveCycle, firstWave.Value.LabelPosition, ref output1 ) )
                    {
                        tmpBar.MainPriceTimeInfo = output1;
                    }                    
                }

                hew = ref dbHew.GetWaveFromScenario( 2 );
                firstWave = hew.GetFirstHighestWaveInfo();

                if ( firstWave.HasValue )
                {
                    WavePriceTimeInfo output1 = default;

                    if ( CalculateWavePriceTimeInfo( 2, bars, period, dbHew.StartDate, firstWave.Value.WaveName, firstWave.Value.WaveCycle, firstWave.Value.LabelPosition, ref output1 ) )
                    {
                        tmpBar.Atl01PriceTimeInfo = output1;
                    }
                }

                hew = ref dbHew.GetWaveFromScenario( 3 );
                firstWave = hew.GetFirstHighestWaveInfo();

                if ( firstWave.HasValue )
                {
                    WavePriceTimeInfo output1 = default;

                    if ( CalculateWavePriceTimeInfo( 3, bars, period, dbHew.StartDate, firstWave.Value.WaveName, firstWave.Value.WaveCycle, firstWave.Value.LabelPosition, ref output1 ) )
                    {
                        tmpBar.Atl02PriceTimeInfo = output1;
                    }
                }

                hew = ref dbHew.GetWaveFromScenario( 4 );
                firstWave = hew.GetFirstHighestWaveInfo();

                if ( firstWave.HasValue )
                {
                    WavePriceTimeInfo output1 = default;

                    if ( CalculateWavePriceTimeInfo( 4, bars, period, dbHew.StartDate, firstWave.Value.WaveName, firstWave.Value.WaveCycle, firstWave.Value.LabelPosition, ref output1 ) )
                    {
                        tmpBar.Atl03PriceTimeInfo = output1;
                    }
                }
            }

            return false;
        }

        private bool CalculateWavePriceTimeInfo( int waveScenarioNo, fxHistoricBarsRepo bars, TimeSpan period, long waveTime, ElliottWaveEnum waveName, ElliottWaveCycle waveCycle, WaveLabelPosition waveLabelPos, ref WavePriceTimeInfo output )
        {
            bool succeed = false;
            switch ( waveName )
            {
                case ElliottWaveEnum.Wave1:
                case ElliottWaveEnum.Wave1C:
                {
                    succeed = GetPriceTimeInfo_Wave0_Wave1C( waveScenarioNo, bars, period, waveTime, waveCycle, waveLabelPos, ref output );
                }
                break;

                case ElliottWaveEnum.Wave2:
                {
                    if ( ( succeed = GetPriceTimeInfo_Wave0_Wave1C_Wave2_HigherTF( waveScenarioNo, bars, period, waveTime, waveCycle, waveLabelPos, ref output ) ) == false )
                    {
                        succeed = GetPriceTimeInfo_Wave0_Wave1C_Wave2_LowerTF( waveScenarioNo, bars, period, waveTime, waveCycle, waveLabelPos, ref output );
                    }                    
                }
                break;

                case ElliottWaveEnum.Wave3:
                case ElliottWaveEnum.Wave3C:
                {
                    succeed = GetPriceTimeInfo_Wave0_Wave3C( waveScenarioNo, bars, period, waveTime, waveCycle, waveLabelPos, ref output );
                }
                break;

                case ElliottWaveEnum.Wave4:
                {
                    if ( ( succeed = GetPriceTimeInfo_Wave2_Wave3C_Wave4_HigherTF( waveScenarioNo, bars, period, waveTime, waveCycle, waveLabelPos, ref output ) ) == false )
                    {
                        succeed = GetPriceTimeInfo_Wave2_Wave3C_Wave4_LowerTF( waveScenarioNo, bars, period, waveTime, waveCycle, waveLabelPos, ref output );
                    }

                    
                }
                break;

                case ElliottWaveEnum.Wave5:
                case ElliottWaveEnum.Wave5C:

                    break;
                case ElliottWaveEnum.WaveTA:

                    break;
                case ElliottWaveEnum.WaveTB:

                    break;
                case ElliottWaveEnum.WaveTC:

                    break;
                case ElliottWaveEnum.WaveTD:

                    break;
                case ElliottWaveEnum.WaveTE:

                    break;
                case ElliottWaveEnum.WaveEFA:

                    break;
                case ElliottWaveEnum.WaveEFB:

                    break;
                case ElliottWaveEnum.WaveEFC:

                    break;
                case ElliottWaveEnum.WaveA:
                {
                    succeed = GetPriceTimeInfo_Wave024X_WaveA( waveScenarioNo, bars, period, waveTime, waveCycle, waveLabelPos, ref output );
                }

                    break;
                case ElliottWaveEnum.WaveB:
                {                    
                    if( ( succeed = GetPriceTimeInfo_Wave024X_WaveA_WaveB_HigherTF( waveScenarioNo, bars, period, waveTime, waveCycle, waveLabelPos, ref output ) ) == false )
                    {
                        succeed = GetPriceTimeInfo_Wave024X_WaveA_WaveB_LowerTF( waveScenarioNo, bars, period, waveTime, waveCycle, waveLabelPos, ref output );
                    }
                }
                break;

                case ElliottWaveEnum.WaveC:
                {
                    succeed = GetPriceTimeInfo_Wave024X_WaveC( waveScenarioNo, bars, period, waveTime, waveCycle, waveLabelPos, ref output );
                }
                break;

                case ElliottWaveEnum.WaveX:

                    break;
            }

            return succeed;
        }

        // Only Wave A and Wave C contains Wave 5
        public WaveInfo? GetContainingWaveOfThisWave5( int waveScenarioNo, long bartimeWave5, ElliottWaveCycle wave5Cycle )
        {
            if( _hews.Count > 0 )
            {
                foreach( var wave in _hews )
                {
                    long iteratingBarTime = wave.Key;

                    if( bartimeWave5 > iteratingBarTime )
                    {
                        ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                        if ( hew.HasHigherDegreeWave( wave5Cycle ) )
                        {
                            var allWaves = hew.GetAllWaves( wave5Cycle + GlobalConstants.OneWaveCycle );

                            foreach( var oneWave in allWaves )
                            {
                                var waveName = oneWave.WaveName;
                                var waveCycle = oneWave.WaveCycle;
                                var waveLabel = oneWave.LabelPosition;

                                switch( waveName )
                                {
                                    case ElliottWaveEnum.Wave2:
                                    case ElliottWaveEnum.Wave4:
                                        if( ( waveCycle - wave5Cycle ) == GlobalConstants.OneWaveCycle )
                                        {
                                            return new WaveInfo( waveCycle, ElliottWaveEnum.WaveA, ( waveLabel == WaveLabelPosition.TOP ? WaveLabelPosition.BOTTOM : WaveLabelPosition.TOP ) );
                                        }
                                        break;

                                    case ElliottWaveEnum.WaveB:
                                        if( ( waveCycle - wave5Cycle ) == GlobalConstants.OneWaveCycle )
                                        {
                                            return new WaveInfo( waveCycle, ElliottWaveEnum.WaveC, ( waveLabel == WaveLabelPosition.TOP ? WaveLabelPosition.BOTTOM : WaveLabelPosition.TOP ) );
                                        }
                                        break;

                                    case ElliottWaveEnum.Wave1:
                                    case ElliottWaveEnum.Wave1C:
                                        if( ( waveCycle - wave5Cycle ) == 2 * GlobalConstants.OneWaveCycle )
                                        {
                                            return new WaveInfo( wave5Cycle + GlobalConstants.OneWaveCycle, ElliottWaveEnum.WaveA, ( waveLabel == WaveLabelPosition.TOP ? WaveLabelPosition.BOTTOM : WaveLabelPosition.TOP ) );
                                        }
                                        break;

                                    case ElliottWaveEnum.Wave3:
                                    case ElliottWaveEnum.Wave3C:
                                        if( ( waveCycle - wave5Cycle ) == 2 * GlobalConstants.OneWaveCycle )
                                        {
                                            return new WaveInfo( wave5Cycle + GlobalConstants.OneWaveCycle, ElliottWaveEnum.WaveA, ( waveLabel == WaveLabelPosition.TOP ? WaveLabelPosition.BOTTOM : WaveLabelPosition.TOP ) );
                                        }
                                        break;

                                    case ElliottWaveEnum.Wave5:
                                    case ElliottWaveEnum.Wave5C:
                                        if( ( waveCycle - wave5Cycle ) == GlobalConstants.OneWaveCycle )
                                        {
                                            return new WaveInfo( waveCycle, ElliottWaveEnum.WaveA, ( waveLabel == WaveLabelPosition.TOP ? WaveLabelPosition.BOTTOM : WaveLabelPosition.TOP ) );
                                        }
                                        break;

                                    case ElliottWaveEnum.WaveA:
                                        if( ( waveCycle - wave5Cycle ) == GlobalConstants.OneWaveCycle )
                                        {
                                            return new WaveInfo( waveCycle, ElliottWaveEnum.WaveC, ( waveLabel == WaveLabelPosition.TOP ? WaveLabelPosition.BOTTOM : WaveLabelPosition.TOP ) );
                                        }
                                        break;

                                    case ElliottWaveEnum.WaveC:
                                    case ElliottWaveEnum.WaveX:
                                        if( ( waveCycle - wave5Cycle ) == GlobalConstants.OneWaveCycle )
                                        {
                                            return new WaveInfo( waveCycle, ElliottWaveEnum.WaveA, ( waveLabel == WaveLabelPosition.TOP ? WaveLabelPosition.BOTTOM : WaveLabelPosition.TOP ) );
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }
    }
}
