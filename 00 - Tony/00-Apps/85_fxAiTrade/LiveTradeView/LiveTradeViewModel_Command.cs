using DevExpress.Mvvm;
using fx.Algorithm;
using fx.Bars;
using fx.Definitions;
using System;
using System.Linq;

namespace FreemindAITrade.ViewModels
{
    public partial class LiveTradeViewModel
    {
        public ElliottWaveCycle AddToExistingWave(
                                                    int waveScenarioNo,
                                                    long selectedBarTime,
                                                    ElliottWaveCycle waveCycle,
                                                    ElliottWaveEnum waveNameToBeAdded )
        {
            var desiredCycle = waveCycle;

            bool doneAdding = false;

            if ( _hews.WaveCount( waveScenarioNo, selectedBarTime ) == 0 ) return desiredCycle;

            var highestDegreeHewPointInfo = _hews.GetWaveOfHigestDegree( waveScenarioNo, selectedBarTime );

            var highestDegreeCycle = highestDegreeHewPointInfo.HasValue ? highestDegreeHewPointInfo.Value.WaveCycle : ElliottWaveCycle.UNKNOWN;

            var lowestDegreeHewPointInfo = _hews.GetWaveOfLowestDegree( waveScenarioNo, selectedBarTime );

            var lowestDegreeCycle = lowestDegreeHewPointInfo.HasValue ? lowestDegreeHewPointInfo.Value.WaveCycle : ElliottWaveCycle.UNKNOWN;

            var oneDegreeHigher = highestDegreeCycle + GlobalConstants.OneWaveCycle;
            var oneDegreeLower = lowestDegreeCycle - GlobalConstants.OneWaveCycle;

            var previousHigherWaveName = _hews.GetPreviousWaveOfDegree( waveScenarioNo, selectedBarTime, oneDegreeHigher );
            var previousLowerWaveName = _hews.GetPreviousWaveOfDegree( waveScenarioNo, selectedBarTime, oneDegreeLower );

            var currentWaveName = highestDegreeHewPointInfo.Value.WaveName;



            ref SBar bar = ref _bars.GetBarByTime( selectedBarTime );

            if ( bar != SBar.EmptySBar )
            {
                if ( _hews.TryUpgradeWaveC( waveScenarioNo, ResponsibleTF, selectedBarTime, waveNameToBeAdded, ref bar ) )
                {
                    return waveCycle;
                }

                if ( ( waveNameToBeAdded == ElliottWaveEnum.WaveEFA || waveNameToBeAdded == ElliottWaveEnum.WaveEFB || waveNameToBeAdded == ElliottWaveEnum.WaveA ) && ( highestDegreeHewPointInfo.Value.WaveName == ElliottWaveEnum.WaveC ) )
                {
                    desiredCycle = oneDegreeHigher;
                    _hews.SmartAddWaveXtoManagerAndBar( waveScenarioNo, ResponsibleTF, selectedBarTime, desiredCycle, waveNameToBeAdded, ref bar );

                    return desiredCycle;
                }

                if ( waveNameToBeAdded == ElliottWaveEnum.WaveEFC && ( ( highestDegreeHewPointInfo.Value.WaveName == ElliottWaveEnum.Wave5 ) || ( highestDegreeHewPointInfo.Value.WaveName == ElliottWaveEnum.Wave5C ) ) )
                {
                    desiredCycle = oneDegreeHigher;
                    _hews.SmartAddWaveXtoManagerAndBar( waveScenarioNo, ResponsibleTF, selectedBarTime, desiredCycle, waveNameToBeAdded, ref bar );

                    return desiredCycle;
                }

                if ( previousHigherWaveName != ElliottWaveEnum.NONE )
                {
                    var previousWaveTime = _hews.GetRawBarTimeOfPreviousWaveOfDegree( waveScenarioNo, selectedBarTime, oneDegreeHigher );

                    if ( previousHigherWaveName.IsNextWave( waveNameToBeAdded ) && _hews.NoHigherWaveBetween( waveScenarioNo, previousWaveTime, selectedBarTime, oneDegreeHigher ) && waveNameToBeAdded.IsValidLowerDegreeWave( currentWaveName ) )
                    {
                        /*
                         * We can see that my code will first check if the higher degree is next wave before checking for the lower degree next wave, so it caused the above bug 
                         * 
                         * In order to solve this problem, I think I will also need to check if the current degree can co-exist with this higher degree.
                         */

                        doneAdding = true;
                        desiredCycle = oneDegreeHigher;

                        _hews.SmartAddWaveXtoManagerAndBar( waveScenarioNo, ResponsibleTF, selectedBarTime, desiredCycle, waveNameToBeAdded, ref bar );
                    }
                }

                if ( previousLowerWaveName != ElliottWaveEnum.NONE && doneAdding == false )
                {
                    var previousWaveTime = _hews.GetRawBarTimeOfPreviousWaveOfDegree( waveScenarioNo, selectedBarTime, oneDegreeLower );

                    if ( previousLowerWaveName.IsNextWave( waveNameToBeAdded ) && _hews.NoHigherWaveBetween( waveScenarioNo, previousWaveTime, selectedBarTime, oneDegreeLower ) )
                    {
                        if ( previousLowerWaveName == ElliottWaveEnum.WaveB )
                        {
                            ProcessAddingWaveC( waveScenarioNo, selectedBarTime, oneDegreeLower );

                            doneAdding = true;
                        }
                        else
                        {
                            doneAdding = true;

                            desiredCycle = oneDegreeLower;

                            _hews.SmartAddWaveXtoManagerAndBar( waveScenarioNo, ResponsibleTF, selectedBarTime, desiredCycle, waveNameToBeAdded, ref bar );
                        }
                    }
                    else if ( waveNameToBeAdded == ElliottWaveEnum.Wave5 || waveNameToBeAdded == ElliottWaveEnum.Wave5C || waveNameToBeAdded == ElliottWaveEnum.WaveC )
                    {
                        doneAdding = true;

                        desiredCycle = oneDegreeLower;

                        _hews.SmartAddWaveXtoManagerAndBar( waveScenarioNo, ResponsibleTF, selectedBarTime, desiredCycle, waveNameToBeAdded, ref bar );
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
                            case ElliottWaveEnum.WaveA:
                            case ElliottWaveEnum.WaveC:
                                {
                                    if ( waveNameToBeAdded == ElliottWaveEnum.Wave5 )
                                    {
                                        desiredCycle = oneDegreeLower;

                                        _hews.SmartAddWaveXtoManagerAndBar( waveScenarioNo, ResponsibleTF, selectedBarTime, desiredCycle, waveNameToBeAdded, ref bar );
                                    }
                                }
                                break;

                            case ElliottWaveEnum.Wave5C:
                                {
                                    if ( waveNameToBeAdded == ElliottWaveEnum.Wave5 )
                                    {
                                        desiredCycle = oneDegreeLower;

                                        _hews.SmartAddWaveXtoManagerAndBar( waveScenarioNo, ResponsibleTF, selectedBarTime, desiredCycle, waveNameToBeAdded, ref bar );
                                    }
                                    else if ( waveNameToBeAdded == ElliottWaveEnum.WaveA || waveNameToBeAdded == ElliottWaveEnum.WaveC )
                                    {
                                        desiredCycle = oneDegreeHigher;

                                        _hews.SmartAddWaveXtoManagerAndBar( waveScenarioNo, ResponsibleTF, selectedBarTime, desiredCycle, waveNameToBeAdded, ref bar );
                                    }
                                }
                                break;


                            // Tony Lam: Need to work on adding wave C to this wave before adding lower degree wave.
                            case ElliottWaveEnum.Wave1:
                                {
                                    if ( waveNameToBeAdded == ElliottWaveEnum.WaveC )
                                    {
                                        _hews.ReplaceWaveInManagerAndBar( waveScenarioNo, ResponsibleTF, selectedBarTime, lowestDegreeCycle, ElliottWaveEnum.Wave1, ElliottWaveEnum.Wave1C, ref bar );
                                    }
                                    else
                                    {
                                        desiredCycle = oneDegreeLower;
                                        _hews.SmartAddWaveXtoManagerAndBar( waveScenarioNo, ResponsibleTF, selectedBarTime, desiredCycle, waveNameToBeAdded, ref bar );
                                    }
                                }
                                break;

                            case ElliottWaveEnum.Wave3:
                                {
                                    if ( waveNameToBeAdded == ElliottWaveEnum.WaveC )
                                    {
                                        _hews.ReplaceWaveInManagerAndBar( waveScenarioNo, ResponsibleTF, selectedBarTime, lowestDegreeCycle, ElliottWaveEnum.Wave3, ElliottWaveEnum.Wave3C, ref bar );
                                    }
                                    else
                                    {
                                        desiredCycle = oneDegreeLower;
                                        _hews.SmartAddWaveXtoManagerAndBar( waveScenarioNo, ResponsibleTF, selectedBarTime, desiredCycle, waveNameToBeAdded, ref bar );
                                    }
                                }
                                break;

                            case ElliottWaveEnum.Wave5:
                                {
                                    if ( waveNameToBeAdded == ElliottWaveEnum.WaveC )
                                    {
                                        _hews.ReplaceWaveInManagerAndBar( waveScenarioNo, ResponsibleTF, selectedBarTime, lowestDegreeCycle, ElliottWaveEnum.Wave5, ElliottWaveEnum.Wave5C, ref bar );
                                    }
                                    else
                                    {
                                        desiredCycle = oneDegreeLower;
                                        _hews.SmartAddWaveXtoManagerAndBar( waveScenarioNo, ResponsibleTF, selectedBarTime, desiredCycle, waveNameToBeAdded, ref bar );
                                    }
                                }
                                break;

                            case ElliottWaveEnum.WaveB:
                            case ElliottWaveEnum.WaveW:
                            case ElliottWaveEnum.WaveX:
                            case ElliottWaveEnum.WaveY:
                            case ElliottWaveEnum.WaveZ:
                                {
                                    if ( waveNameToBeAdded == ElliottWaveEnum.WaveC )
                                    {

                                        desiredCycle = oneDegreeLower;
                                        _hews.SmartAddWaveXtoManagerAndBar( waveScenarioNo, ResponsibleTF, selectedBarTime, desiredCycle, waveNameToBeAdded, ref bar );
                                    }
                                }
                                break;

                            default:
                                Console.Beep();
                                break;
                        }
                    }
                    else
                    {
                        var dbHewPrev = _hews.GetPreviousWaveStructure( waveScenarioNo, selectedBarTime );

                        if ( dbHewPrev == null )
                        {
                            return waveCycle;
                        }

                        if ( _hews.NoHigherWaveBetween( waveScenarioNo, dbHewPrev.StartDate, selectedBarTime, oneDegreeHigher ) && waveNameToBeAdded.IsValidLowerDegreeWave( currentWaveName ) )
                        {
                            desiredCycle = oneDegreeHigher;

                            _hews.SmartAddWaveXtoManagerAndBar( waveScenarioNo, ResponsibleTF, selectedBarTime, desiredCycle, waveNameToBeAdded, ref bar );
                        }
                    }
                }
            }

            Messenger.Default.Send( new HewMessage( desiredCycle ) );

            return desiredCycle;
        }

        public void ProcessAddingWaveC( int waveScenarioNo, long selectedBarTime, ElliottWaveCycle waveCycle )
        {
            var nextWave = ElliottWaveEnum.WaveC;

            ref SBar bar = ref _bars.GetBarByTime( selectedBarTime );

            if ( bar == SBar.EmptySBar ) return;

            var beginWaveHewInfo = _hews.FindBeginningWaveOfThisWaveC( waveScenarioNo, selectedBarTime, waveCycle );

            if ( !beginWaveHewInfo.HasValue )
            {
                _hews.SmartAddWaveXtoManagerAndBar( waveScenarioNo, ResponsibleTF, selectedBarTime, waveCycle, nextWave, ref bar );

                _chartVM.Refresh();

                //this.Refresh( );

                return;
            }


            var beginningWave = beginWaveHewInfo.Value.WaveName;


            if ( beginningWave != ElliottWaveEnum.NONE )
            {
                switch ( beginningWave )
                {
                    case ElliottWaveEnum.Wave2:
                        {
                            if ( beginWaveHewInfo.Value.WaveCycle == waveCycle )
                            {
                                nextWave = ElliottWaveEnum.Wave3C;

                                _hews.SmartAddWaveXtoManagerAndBar( WaveScenarioNumber, ResponsibleTF, selectedBarTime, waveCycle, nextWave, ref bar );

                                _chartVM.Refresh();

                                return;
                            }
                        }
                        break;


                    case ElliottWaveEnum.Wave4:
                        {
                            if ( beginWaveHewInfo.Value.WaveCycle == waveCycle )
                            {
                                nextWave = ElliottWaveEnum.Wave5C;

                                _hews.SmartAddEndingWave5CtoManagerAndBar( WaveScenarioNumber, ResponsibleTF, selectedBarTime, waveCycle, nextWave, beginWaveHewInfo.Value.OppositeLabelDirection(), ref bar );

                                _chartVM.Refresh();

                                return;
                            }
                        }
                        break;


                    case ElliottWaveEnum.WaveB:
                        {

                        }
                        break;


                    default:
                        {

                        }
                        break;
                }

                _hews.SmartAddWaveXtoManagerAndBar( WaveScenarioNumber, ResponsibleTF, selectedBarTime, waveCycle, nextWave, ref bar );

                _chartVM.Refresh();

                return;
            }
        }

        public override void AddWaveAToChart( int waveScenarioNo, ElliottWaveCycle waveCycle, ElliottWaveEnum elliottWave )
        {
            var selectedBarTime = _chartVM.SelectedCandleBarTime;

            ref SBar bar = ref _bars.GetBarByTime( selectedBarTime );

            if ( bar != SBar.EmptySBar )
            {
                if ( _chartVM.IsSpecialBar || _hews.IsSpecialBar( waveScenarioNo, selectedBarTime ) || bar.IsSpecialBar )
                {
                    _hews?.AddIntelliWave( waveScenarioNo, ResponsibleTF, selectedBarTime, waveCycle, elliottWave, ref bar, true, _bars.IsOutsideBar( selectedBarTime ) );
                }
                else
                {
                    var allWave = _hews.GetAllWavesDescending( waveScenarioNo, selectedBarTime );

                    if ( allWave != null && allWave.Count > 0 )
                    {
                        AddToExistingWave( waveScenarioNo, selectedBarTime, waveCycle, elliottWave );
                    }
                    else
                    {
                        _hews?.SmartAddWaveXtoManagerAndBar( waveScenarioNo, ResponsibleTF, selectedBarTime, waveCycle, elliottWave, ref bar );
                    }
                }
            }

            Refresh();
        }
    }
}