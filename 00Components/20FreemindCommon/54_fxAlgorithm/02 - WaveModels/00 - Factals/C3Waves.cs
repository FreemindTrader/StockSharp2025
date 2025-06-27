using fx.Database;
using fx.Bars;
using fx.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fx.Collections;
using fx.Common;
using fx.Definitions.UndoRedo;
using StockSharp.BusinessEntities;

#pragma warning disable 414

namespace fx.Algorithm
{
    public class C3Waves : I3Waves
    {
        public SBar Bar0;
        public SBar BarA1;
        public SBar BarB1;
        public SBar BarC1;
        public SBar BarX1;
        public SBar BarA2;
        public SBar BarB2;
        public SBar BarC2;
        public SBar BarX2;
        public SBar BarA3;
        public SBar BarB3;
        public SBar BarC3;
        public SBar BarEnd;

        private bool               _hasChildren;
        private HewManager         _hews;
        private fxHistoricBarsRepo _bars;
        private WaveType           _mainWaveType;

        private long               _wave0Time = -1;
        private long               _waveA1Time = -1;
        private long               _waveB1Time = -1;
        private long               _waveC1Time = -1;
        private long               _waveA2Time = -1;
        private long               _waveB2Time = -1;
        private long               _waveC2Time = -1;
        private long               _waveA3Time = -1;
        private long               _waveB3Time = -1;
        private long               _waveC3Time = -1;
        private long               _waveX1Time = -1;
        private long               _waveX2Time = -1;
        private long               _endTime = -1;

        private IFactal            _subWaveA1  = null;
        private IFactal            _subWaveB1  = null;
        private IFactal            _subWaveC1  = null;
        private IFactal            _subWaveA2  = null;
        private IFactal            _subWaveB2  = null;
        private IFactal            _subWaveC2  = null;
        private IFactal            _subWaveA3  = null;
        private IFactal            _subWaveB3  = null;
        private IFactal            _subWaveC3  = null;
        private WaveModelKey       _k            = null;

        public C3Waves( fxHistoricBarsRepo bars, HewManager hewManager, WaveModelKey key )
        {
            _bars = bars;
            _hews = hewManager;
            _k    = key;
        }

        public WaveType MainWaveType
        {
            get { return _mainWaveType; }
            set
            {
                _mainWaveType = value;
            }
        }

        public bool HasChildren
        {
            get => _hasChildren;
            set => _hasChildren = value;
        }

        ElliottWaveCycle _waveCycle;

        public ElliottWaveCycle WaveCycle
        {
            get => _waveCycle;
            set => _waveCycle = value;
        }

        public long BeginTime
        {
            get => _wave0Time;
            set => _wave0Time = value;
        }

        public long Wave0Time
        {
            get => _wave0Time;
            set => _wave0Time = value;
        }


        public long WaveA1Time
        {
            get => _waveA1Time;
            set => _waveA1Time = value;
        }


        public long WaveB1Time
        {
            get => _waveB1Time;
            set => _waveB1Time = value;
        }

        
        public long WaveC1Time
        {
            get => _waveC1Time;
            set => _waveC1Time = value;
        }

        public long WaveA2Time
        {
            get => _waveA2Time;
            set => _waveA2Time = value;
        }


        public long WaveB2Time
        {
            get => _waveB2Time;
            set => _waveB2Time = value;
        }


        public long WaveC2Time
        {
            get => _waveC2Time;
            set => _waveC2Time = value;
        }

        public long WaveA3Time
        {
            get => _waveA3Time;
            set => _waveA3Time = value;
        }


        public long WaveB3Time
        {
            get => _waveB3Time;
            set => _waveB3Time = value;
        }


        public long WaveC3Time
        {
            get => _waveC3Time;
            set => _waveC3Time = value;
        }

        public long EndTime
        {
            get => _endTime;
            set => _endTime = value;
        }

        public IFactal SubWaveA1
        {
            get => _subWaveA1;
            set => _subWaveA1 = value;
        }

        public IFactal SubWaveB1
        {
            get => _subWaveB1;
            set => _subWaveB1 = value;
        }

        public IFactal SubWaveC1
        {
            get => _subWaveC1;
            set => _subWaveC1 = value;
        }

        public IFactal SubWaveA2
        {
            get => _subWaveA2;
            set => _subWaveA2 = value;
        }

        public IFactal SubWaveB2
        {
            get => _subWaveB2;
            set => _subWaveB2 = value;
        }

        public IFactal SubWaveC2
        {
            get => _subWaveC2;
            set => _subWaveC2 = value;
        }

        public IFactal SubWaveA3
        {
            get => _subWaveA3;
            set => _subWaveA3 = value;
        }

        public IFactal SubWaveB3
        {
            get => _subWaveB3;
            set => _subWaveB3 = value;
        }

        public IFactal SubWaveC3
        {
            get => _subWaveC3;
            set => _subWaveC3 = value;
        }

        private PooledList< FibLevelInfo > _predictedTargets = null;

        public PooledList<FibLevelInfo> PredictedTargets
        {
            get
            {
                return _predictedTargets;
            }
        }

        public void ProcessFinishedWaves( int waveScenarioNo, long beginTime, long endTime, ElliottWaveCycle cycle, UndoRedoBTreeDictionary<long, DbElliottWave> hews, List<DbElliottWave> dbHews )
        {
            if ( beginTime > -1 )
            {
                _wave0Time = beginTime;
                Bar0 = _bars.GetBarByTime( _wave0Time );
            }

            var childCycle   = cycle      - GlobalConstants.OneWaveCycle;
            var grandChild   = childCycle - GlobalConstants.OneWaveCycle;

            var lastWaveName = ElliottWaveEnum.NONE;
            var lastWaveTime = 1L;
            //var lowerDegWave = ElliottWaveEnum.NONE;

            var inBtw   = hews.Where( x => x.Value.StartDate >= beginTime && x.Value.StartDate <= endTime ).OrderBy( x => x.Value.StartDate );

            foreach ( var pair in inBtw )
            {
                var dbHew    = pair.Value;

                ref var hew  = ref dbHew.GetWaveFromScenario( waveScenarioNo );

                var waveInfo = hew.GetHewPointInfoAtCycle( childCycle );

                if ( waveInfo.HasValue )
                {
                    lastWaveName = waveInfo.Value.WaveName;
                    lastWaveTime = pair.Key;

                    switch ( waveInfo.Value.WaveName )
                    {

                        case ElliottWaveEnum.WaveA:
                        {
                            if ( _waveA1Time == -1 && _waveA2Time == -1 && _waveA3Time == -1 )
                            {
                                _waveA1Time = dbHew.StartDate;
                                BarA1 = _bars.GetBarByTime( _waveA1Time );
                            }
                            else if ( _waveA1Time > -1 && _waveA2Time == -1 && _waveA3Time == -1 )
                            {
                                _waveA2Time = dbHew.StartDate;
                                BarA2 = _bars.GetBarByTime( _waveA2Time );
                            }
                            else if ( _waveA1Time > -1 && _waveA2Time > -1 && _waveA3Time == -1 )
                            {
                                _waveA3Time = dbHew.StartDate;
                                BarA3 = _bars.GetBarByTime( _waveA3Time );
                            }
                        }
                        break;

                        case ElliottWaveEnum.WaveB:
                        {
                            if ( _waveB1Time == -1 && _waveB2Time == -1 && _waveB3Time == -1 )
                            {
                                _waveB1Time = dbHew.StartDate;
                                BarB1 = _bars.GetBarByTime( _waveB1Time );
                            }
                            else if ( _waveB1Time > -1 && _waveB2Time == -1 && _waveB3Time == -1 )
                            {
                                _waveB2Time = dbHew.StartDate;
                                BarB2 = _bars.GetBarByTime( _waveB2Time );
                            }
                            else if ( _waveB1Time > -1 && _waveB2Time > -1 && _waveB3Time == -1 )
                            {
                                _waveB3Time = dbHew.StartDate;
                                BarB3 = _bars.GetBarByTime( _waveB3Time );
                            }
                        }
                        break;

                        case ElliottWaveEnum.WaveC:
                        {
                            if ( _waveC1Time == -1 && _waveC2Time == -1 && _waveC3Time == -1 )
                            {
                                _waveC1Time = dbHew.StartDate;
                                BarC1 = _bars.GetBarByTime( _waveC1Time );
                            }
                            else if ( _waveC1Time > -1 && _waveC2Time == -1 && _waveC3Time == -1 )
                            {
                                _waveC2Time = dbHew.StartDate;
                                BarC2 = _bars.GetBarByTime( _waveC2Time );
                            }
                            else if ( _waveC1Time > -1 && _waveC2Time > -1 && _waveC3Time == -1 )
                            {
                                _waveC3Time = dbHew.StartDate;
                                BarC3 = _bars.GetBarByTime( _waveC3Time );
                            }
                        }
                        break;

                        case ElliottWaveEnum.WaveX:
                        {
                            if ( _waveX1Time == -1 && _waveX2Time == -1 )
                            {
                                _waveX1Time = dbHew.StartDate;
                                BarX1 = _bars.GetBarByTime( _waveX1Time );
                            }
                            else if ( _waveX1Time > -1 && _waveX2Time == -1 )
                            {
                                _waveX2Time = dbHew.StartDate;
                                BarX2 = _bars.GetBarByTime( _waveX2Time );
                            }
                        }
                        break;

                        case ElliottWaveEnum.WaveTA:
                        {

                        }
                        break;

                        case ElliottWaveEnum.WaveTB:
                        {
                        }
                        break;

                        case ElliottWaveEnum.WaveTC:
                        {
                        }
                        break;

                        case ElliottWaveEnum.WaveTD:
                        {

                        }
                        break;

                        case ElliottWaveEnum.WaveTE:
                        {

                        }
                        break;

                        case ElliottWaveEnum.WaveEFA:
                        {

                        }
                        break;

                        case ElliottWaveEnum.WaveEFB:
                        {

                        }
                        break;

                        case ElliottWaveEnum.WaveEFC:
                        {

                        }
                        break;


                        case ElliottWaveEnum.WaveY:
                        {

                        }
                        break;

                        case ElliottWaveEnum.WaveZ:
                        {

                        }
                        break;

                        case ElliottWaveEnum.WaveW:
                        {

                        }
                        break;

                    }
                }
            }
        }

        public void ProcessOngoingHews( int waveScenarioNo, long beginTime, long endTime, ElliottWaveCycle cycle, UndoRedoBTreeDictionary<long, DbElliottWave> hews, List<DbElliottWave> dbHews )
        {
            if ( beginTime > -1 )
            {
                _wave0Time = beginTime;
                Bar0 = _bars.GetBarByTime( _wave0Time );
            }

            var childCycle   = cycle      - GlobalConstants.OneWaveCycle;
            var grandChild   = childCycle - GlobalConstants.OneWaveCycle;

            var lastWaveName = ElliottWaveEnum.NONE;
            var lastWaveTime = 1L;
            var lowerDegWave = ElliottWaveEnum.NONE;

            var inBtw   = hews.Where( x => x.Value.StartDate >= _k.RawBeginTime ).OrderBy( x => x.Value.StartDate );

            foreach ( var pair in inBtw )
            {
                var dbHew    = pair.Value;

                ref var hew  = ref dbHew.GetWaveFromScenario( waveScenarioNo );

                var waveInfo = hew.GetHewPointInfoAtCycle( childCycle );

                if ( waveInfo.HasValue )
                {
                    lastWaveName = waveInfo.Value.WaveName;
                    lastWaveTime = pair.Key;

                    switch ( waveInfo.Value.WaveName )
                    {

                        case ElliottWaveEnum.WaveA:
                        {
                            if ( _waveA1Time == -1 && _waveA2Time == -1 && _waveA3Time == -1  )
                            {
                                _waveA1Time = dbHew.StartDate;
                                BarA1 = _bars.GetBarByTime( _waveA1Time );
                            }
                            else if ( _waveA1Time > -1 && _waveA2Time == -1 && _waveA3Time == -1 )
                            {
                                _waveA2Time = dbHew.StartDate;
                                BarA2 = _bars.GetBarByTime( _waveA2Time );
                            }
                            else if ( _waveA1Time > -1 && _waveA2Time > -1 && _waveA3Time == -1 )
                            {
                                _waveA3Time = dbHew.StartDate;
                                BarA3 = _bars.GetBarByTime( _waveA3Time );
                            }
                        }
                        break;

                        case ElliottWaveEnum.WaveB:
                        {
                            if ( _waveB1Time == -1 && _waveB2Time == -1 && _waveB3Time == -1 )
                            {
                                _waveB1Time = dbHew.StartDate;
                                BarB1 = _bars.GetBarByTime( _waveB1Time );
                            }
                            else if ( _waveB1Time > -1 && _waveB2Time == -1 && _waveB3Time == -1 )
                            {
                                _waveB2Time = dbHew.StartDate;
                                BarB2 = _bars.GetBarByTime( _waveB2Time );
                            }
                            else if ( _waveB1Time > -1 && _waveB2Time > -1 && _waveB3Time == -1 )
                            {
                                _waveB3Time = dbHew.StartDate;
                                BarB3 = _bars.GetBarByTime( _waveB3Time );
                            }
                        }
                        break;

                        case ElliottWaveEnum.WaveC:
                        {
                            if ( _waveC1Time == -1 && _waveC2Time == -1 && _waveC3Time == -1 )
                            {
                                _waveC1Time = dbHew.StartDate;
                                BarC1 = _bars.GetBarByTime( _waveC1Time );
                            }
                            else if ( _waveC1Time > -1 && _waveC2Time == -1 && _waveC3Time == -1 )
                            {
                                _waveC2Time = dbHew.StartDate;
                                BarC2 = _bars.GetBarByTime( _waveC2Time );
                            }
                            else if ( _waveC1Time > -1 && _waveC2Time > -1 && _waveC3Time == -1 )
                            {
                                _waveC3Time = dbHew.StartDate;
                                BarC3 = _bars.GetBarByTime( _waveC3Time );
                            }
                        }
                        break;

                        case ElliottWaveEnum.WaveX:
                        {
                            if ( _waveX1Time == -1 && _waveX2Time == -1 )
                            {
                                _waveX1Time = dbHew.StartDate;
                                BarX1 = _bars.GetBarByTime( _waveX1Time );
                            }
                            else if ( _waveX1Time > -1 && _waveX2Time == -1 )
                            {
                                _waveX2Time = dbHew.StartDate;
                                BarX2 = _bars.GetBarByTime( _waveX2Time );
                            }
                        }
                        break;

                        case ElliottWaveEnum.WaveTA:
                        {

                        }
                        break;

                        case ElliottWaveEnum.WaveTB:
                        {
                        }
                        break;

                        case ElliottWaveEnum.WaveTC:
                        {
                        }
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

                        
                        case ElliottWaveEnum.WaveY:

                            break;
                        case ElliottWaveEnum.WaveZ:

                            break;
                        case ElliottWaveEnum.WaveW:

                            break;

                    }
                }
                else
                {
                    var grandchildrenWI = hew.GetHewPointInfoAtCycle( grandChild );

                    if ( grandchildrenWI.HasValue )
                    {
                        lowerDegWave = grandchildrenWI.Value.WaveName;

                        if ( lastWaveName.IsNextWaveOneDegreeLowerBeginning( lowerDegWave ) )
                        {
                            var grandChildrendbHews = hews.Where( x => x.Value.StartDate >= dbHew.StartDate ).OrderBy( x => x.Value.StartDate );

                            var aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _bars.Security );
                            
                            var correctionDoneWave = GetCorrectionDoneWave( grandChildrendbHews, grandChild, waveScenarioNo );

                            var waveKey = new WaveModelKey( _k.Security, _k.Period, waveScenarioNo, lastWaveTime, correctionDoneWave == null ? -1 : correctionDoneWave.StartDate, grandChild );

                            C3Waves c3Waves = null;

                            if ( aa.GetOrCreate3Waves( waveKey, _bars, _hews, out c3Waves ) )
                            {
                                SetCurrentSubWave( lastWaveName, c3Waves );

                                var listHews = grandChildrendbHews.Select( x => x.Value ).ToList();

                                if ( correctionDoneWave != null )
                                {
                                    c3Waves.ProcessFinishedWaves( waveScenarioNo, lastWaveTime, correctionDoneWave.StartDate, childCycle, hews, listHews );
                                }
                                else
                                {

                                    c3Waves.ProcessOngoingHews( waveScenarioNo, lastWaveTime, -1, childCycle, hews, listHews );
                                }
                                //
                            }
                            
                        }
                    }
                }
            }
        }

        public DbElliottWave GetCorrectionDoneWave( IOrderedEnumerable<KeyValuePair<long, DbElliottWave>> dbHews, ElliottWaveCycle cycle, int waveScenarioNo )
        {
            foreach ( var hewPair in dbHews )
            {
                var dbHew = hewPair.Value;

                ref var hew  = ref dbHew.GetWaveFromScenario( waveScenarioNo );

                if ( hew.IsAnyWaveOfOneHigherDegree( cycle ) )
                {
                    return dbHew;
                }
            }
            return null;
        }

        public void SetCurrentSubWave( ElliottWaveEnum lastWaveName, IFactal subWave )
        {

            switch ( lastWaveName )
            {
                case ElliottWaveEnum.WaveA:
                {
                    
                }
                break;

                case ElliottWaveEnum.WaveB:
                {
                    
                }
                break;

                case ElliottWaveEnum.WaveC:
                {

                }
                break;

                case ElliottWaveEnum.WaveTA:
                {

                }
                break;

                case ElliottWaveEnum.WaveTB:
                {
                }
                break;

                case ElliottWaveEnum.WaveTC:
                {
                }
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

                case ElliottWaveEnum.WaveX:

                    break;
                case ElliottWaveEnum.WaveY:

                    break;
                case ElliottWaveEnum.WaveZ:

                    break;
                case ElliottWaveEnum.WaveW:

                    break;


            }
        }

        public void ProcessdbHews( int waveScenarioNo, long beginTime, long endTime, ElliottWaveCycle cycle, UndoRedoBTreeDictionary<long, DbElliottWave> hews, List<DbElliottWave> dbHews )
        {
            if ( beginTime > -1 )
            {
                _wave0Time = beginTime;
                Bar0 = _bars.GetBarByTime( _wave0Time );
            }

            if ( endTime > -1 )
            {
                _endTime = endTime;
                BarEnd = _bars.GetBarByTime( _endTime );
            }



            var childCycle = cycle - GlobalConstants.OneWaveCycle;

            Bar0 = _bars.GetBarByTime( beginTime );

            var inBtw   = hews.Where( x => x.Value.StartDate >= _k.RawBeginTime && x.Value.StartDate <= _k.RawEndTime ).OrderBy( x => x.Value.StartDate );

            foreach ( var pair in inBtw )
            {
                var dbHew    = pair.Value;

                ref var hew  = ref dbHew.GetWaveFromScenario( waveScenarioNo );

                var waveInfo = hew.GetHewPointInfoAtCycle( childCycle );

                if ( waveInfo.HasValue )
                {
                    switch ( waveInfo.Value.WaveName )
                    {
                        case ElliottWaveEnum.WaveA:
                        {
                            if ( _waveA1Time == -1 && _waveA2Time == -1 && _waveA3Time == -1 )
                            {
                                _waveA1Time = dbHew.StartDate;
                                BarA1 = _bars.GetBarByTime( _waveA1Time );
                            }
                            else if ( _waveA1Time > -1 && _waveA2Time == -1 && _waveA3Time == -1 )
                            {
                                _waveA2Time = dbHew.StartDate;
                                BarA2 = _bars.GetBarByTime( _waveA2Time );
                            }
                            else if ( _waveA1Time > -1 && _waveA2Time > -1 && _waveA3Time == -1 )
                            {
                                _waveA3Time = dbHew.StartDate;
                                BarA3 = _bars.GetBarByTime( _waveA3Time );
                            }
                        }
                        break;

                        case ElliottWaveEnum.WaveB:
                        {
                            if ( _waveB1Time == -1 && _waveB2Time == -1 && _waveB3Time == -1 )
                            {
                                _waveB1Time = dbHew.StartDate;
                                BarB1 = _bars.GetBarByTime( _waveB1Time );
                            }
                            else if ( _waveB1Time > -1 && _waveB2Time == -1 && _waveB3Time == -1 )
                            {
                                _waveB2Time = dbHew.StartDate;
                                BarB2 = _bars.GetBarByTime( _waveB2Time );
                            }
                            else if ( _waveB1Time > -1 && _waveB2Time > -1 && _waveB3Time == -1 )
                            {
                                _waveB3Time = dbHew.StartDate;
                                BarB3 = _bars.GetBarByTime( _waveB3Time );
                            }
                        }
                        break;

                        case ElliottWaveEnum.Wave3:
                        case ElliottWaveEnum.WaveC:
                        {
                            if ( _waveC1Time == -1 && _waveC2Time == -1 && _waveC3Time == -1 )
                            {
                                _waveC1Time = dbHew.StartDate;
                                BarC1 = _bars.GetBarByTime( _waveC1Time );
                            }
                            else if ( _waveC1Time > -1 && _waveC2Time == -1 && _waveC3Time == -1 )
                            {
                                _waveC2Time = dbHew.StartDate;
                                BarC2 = _bars.GetBarByTime( _waveC2Time );
                            }
                            else if ( _waveC1Time > -1 && _waveC2Time > -1 && _waveC3Time == -1 )
                            {
                                _waveC3Time = dbHew.StartDate;
                                BarC3 = _bars.GetBarByTime( _waveC3Time );
                            }
                        }
                        break;

                        case ElliottWaveEnum.WaveX:
                        {
                            
                        }
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

                        case ElliottWaveEnum.WaveY:

                            break;
                        case ElliottWaveEnum.WaveZ:

                            break;
                        case ElliottWaveEnum.WaveW:

                            break;

                    }
                }
            }

            var symbolex = SymbolHelper.ToSymbolEx( _bars.Security.ToSecurityId(), _bars.Period.Value );

            //if ( _wave1CTime > -1 )
            //{
            //    _wave2Ret = _hews.GetHewFibTargets( waveScenarioNo, symbolex, _bars.Period.Value, _wave1CTime, ElliottWaveEnum.Wave1, childCycle );

            //    if ( _wave2Ret != null && Bar2 != SBar.EmptySBar )
            //    {
            //        if ( _wave2Ret.GetClosestFibLevel( Bar2.PeakTroughValue, out FibLevelInfo wave2Retracement ) )
            //        {
            //            Wave2Ret = wave2Retracement.FibPrecentage;
            //        }
            //    }
            //}

            //if ( _wave2Time > -1 )
            //{
            //    _wave3Exp = _hews.GetHewFibTargets( waveScenarioNo, symbolex, _bars.Period.Value, _wave2Time, ElliottWaveEnum.Wave2, childCycle );

            //    if ( _wave3Exp != null )
            //    {
            //        if ( Bar3A != SBar.EmptySBar )
            //        {
            //            if ( _wave3Exp.GetClosestFibLevel( Bar3A.PeakTroughValue, out FibLevelInfo wave3AtoWave1 ) )
            //            {
            //                Wave3aProj = wave3AtoWave1.FibPrecentage;
            //            }
            //        }

            //        if ( Bar3B != SBar.EmptySBar )
            //        {
            //            if ( _wave3Exp.GetClosestFibLevel( Bar3B.PeakTroughValue, out FibLevelInfo wave3btowave1 ) )
            //            {
            //                Wave3btoWave1 = wave3btowave1.FibPrecentage;
            //            }
            //        }

            //        if ( Bar3C != SBar.EmptySBar )
            //        {
            //            if ( _wave3Exp.GetClosestFibLevel( Bar3C.PeakTroughValue, out FibLevelInfo wave3CtoWave1 ) )
            //            {
            //                Wave3cProj = wave3CtoWave1.FibPrecentage;
            //            }
            //        }
            //    }
            //}

            //if ( _wave3ATime > -1 )
            //{
            //    _wave3BRet = _hews.GetHewFibTargets( waveScenarioNo, symbolex, _bars.Period.Value, _wave3ATime, ElliottWaveEnum.WaveA, childCycle );

            //    if ( _wave3BRet != null && Bar3B != SBar.EmptySBar )
            //    {
            //        if ( _wave3BRet.GetClosestFibLevel( Bar3B.PeakTroughValue, out FibLevelInfo wave3BRetValue ) )
            //        {
            //            Wave3bRet = wave3BRetValue.FibPrecentage;
            //        }

            //        if ( _wave3Exp != null )
            //        {
            //            if ( _wave3Exp.GetClosestFibLevel( Bar3B.PeakTroughValue, out FibLevelInfo wave3btoWave1Value ) )
            //            {
            //                Wave3btoWave1 = wave3btoWave1Value.FibPrecentage;
            //            }
            //        }
            //    }
            //}


            //if ( _wave3CTime > -1 )
            //{
            //    _wave4Ret = _hews.GetHewFibTargets( waveScenarioNo, symbolex, _bars.Period.Value, _wave3CTime, ElliottWaveEnum.Wave3, childCycle );

            //    if ( _wave4Ret != null && Bar4 != SBar.EmptySBar )
            //    {
            //        if ( _wave4Ret.GetClosestFibLevel( Bar4.PeakTroughValue, out FibLevelInfo wave4RetValue ) )
            //        {
            //            Wave4Ret = wave4RetValue.FibPrecentage;
            //        }

            //        if ( _wave3Exp != null )
            //        {
            //            if ( _wave3Exp.GetClosestFibLevel( Bar4.PeakTroughValue, out FibLevelInfo wave4toWave1Value ) )
            //            {
            //                Wave4toWave1 = wave4toWave1Value.FibPrecentage;
            //            }
            //        }
            //    }
            //}

            //if ( _wave4Time > -1 )
            //{
            //    _wave5Exp = _hews.GetHewFibTargets( waveScenarioNo, symbolex, _bars.Period.Value, _wave4Time, ElliottWaveEnum.Wave4, childCycle );

            //    if ( _wave5Exp != null )
            //    {
            //        if ( Bar5A != SBar.EmptySBar )
            //        {
            //            if ( _wave5Exp.GetClosestFibLevel( Bar5A.PeakTroughValue, out FibLevelInfo wave5AExp ) )
            //            {
            //                Wave5AProj = wave5AExp.FibPrecentage;
            //            }
            //        }

            //        if ( Bar5C != SBar.EmptySBar )
            //        {
            //            if ( _wave5Exp.GetClosestFibLevel( Bar5C.PeakTroughValue, out FibLevelInfo wave5CExp ) )
            //            {
            //                Wave5CProj = wave5CExp.FibPrecentage;
            //            }
            //        }
            //    }

            //    if ( _wave3Exp != null )
            //    {
            //        if ( _wave3Exp.GetClosestFibLevel( Bar5C.PeakTroughValue, out FibLevelInfo wave5CtoWave1 ) )
            //        {
            //            Wave5toWave1 = wave5CtoWave1.FibPrecentage;
            //        }
            //    }
            //}


        }
    }
}

