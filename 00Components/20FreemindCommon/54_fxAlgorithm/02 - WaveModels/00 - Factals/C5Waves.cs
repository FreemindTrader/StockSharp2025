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
    public partial class C5Waves : I5Waves
    {
        public SBar Bar0;
        public SBar Bar1A;
        public SBar Bar1B;
        public SBar Bar1C;
        public SBar Bar2;
        public SBar Bar3A;
        public SBar Bar3B;
        public SBar Bar3C;
        public SBar Bar4;
        public SBar Bar5A;
        public SBar Bar5B;
        public SBar Bar5C;

        private HewFibGannTargets  _wave0Ret      = null;
        private HewFibGannTargets  _wave2Ret      = null;
        private HewFibGannTargets  _wave3BRet     = null;
        private HewFibGannTargets  _wave3Exp      = null;
        private HewFibGannTargets  _wave3CExp      = null;
        private HewFibGannTargets  _wave4Ret      = null;
        private HewFibGannTargets  _wave5Exp      = null;
        private HewFibGannTargets  _waveCExp      = null;

        private HewManager         _hews          = null;
        private fxHistoricBarsRepo _bars          = null;

        private bool               _isComplete    = false;
        private bool               _hasChildren   = false;
        private WaveType           _mainWaveType  = WaveType.UNKNOWN;

        private long               _wave0Time     = -1;
        private long               _wave1ATime    = -1;
        private long               _wave1BTime    = -1;
        private long               _wave1CTime    = -1;
        private long               _wave2Time     = -1;
        private long               _wave3ATime    = -1;
        private long               _wave3BTime    = -1;
        private long               _wave3CTime    = -1;
        private long               _wave4Time     = -1;
        private long               _wave5ATime    = -1;
        private long               _wave5BTime    = -1;
        private long               _wave5CTime    = -1;

        private WaveModelKey       _k             = null;

        private IFactal            _subWave1A     = null;
        private IFactal            _subWave1B     = null;
        private IFactal            _subWave1C     = null;
        private IFactal            _subWave2      = null;
        private IFactal            _subWave3A     = null;
        private IFactal            _subWave3B     = null;
        private IFactal            _subWave3C     = null;
        private IFactal            _subWave4      = null;
        private IFactal            _subWave5A     = null;
        private IFactal            _subWave5B     = null;
        private IFactal            _subWave5C     = null;



        public C5Waves( fxHistoricBarsRepo bars, HewManager hewManager, WaveModelKey key )
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

        
        public bool IsComplete
        {
            get => _isComplete;
            set => _isComplete = value;
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


        public long Wave1ATime
        {
            get => _wave1ATime;
            set => _wave1ATime = value;
        }

        public long Wave1BTime
        {
            get => _wave1BTime;
            set => _wave1BTime = value;
        }

        public long Wave1CTime
        {
            get => _wave1CTime;
            set => _wave1CTime = value;
        }


        public long Wave2Time
        {
            get => _wave2Time;
            set => _wave2Time = value;
        }


        public long Wave3ATime
        {
            get => _wave3ATime;
            set => _wave3ATime = value;
        }

        public long Wave3BTime
        {
            get => _wave3BTime;
            set => _wave3BTime = value;
        }

        public long Wave3CTime
        {
            get => _wave3CTime;
            set => _wave3CTime = value;
        }

        public long Wave4Time
        {
            get => _wave4Time;
            set => _wave4Time = value;
        }

        public long Wave5ATime
        {
            get => _wave5ATime;
            set => _wave5ATime = value;
        }

        public long Wave5BTime
        {
            get => _wave5BTime;
            set => _wave5BTime = value;
        }

        public long Wave5CTime
        {
            get => _wave5CTime;
            set => _wave5CTime = value;
        }

        public long EndTime
        {
            get => _wave5CTime;
            set => _wave5CTime = value;
        }

        public IFactal SubWave1A
        {
            get => _subWave1A;
            set => _subWave1A = value;
        }

        public IFactal SubWave1B
        {
            get => _subWave1B;
            set => _subWave1B = value;
        }

        public IFactal SubWave1C
        {
            get => _subWave1C;
            set => _subWave1C = value;
        }

        public IFactal SubWave2
        {
            get => _subWave2;
            set => _subWave2 = value;
        }

        public IFactal SubWave3A
        {
            get => _subWave3A;
            set => _subWave3A = value;
        }

        public IFactal SubWave3B
        {
            get => _subWave3B;
            set => _subWave3B = value;
        }

        public IFactal SubWave3C
        {
            get => _subWave3C;
            set => _subWave3C = value;
        }

        public IFactal SubWave4
        {
            get => _subWave4;
            set => _subWave4 = value;
        }

        public IFactal SubWave5A
        {
            get => _subWave5A;
            set => _subWave5A = value;
        }

        public IFactal SubWave5B
        {
            get => _subWave5B;
            set => _subWave5B = value;
        }

        public IFactal SubWave5C
        {
            get => _subWave5C;
            set => _subWave5C = value;
        }

        public FibPercentage Wave2Ret       { get; set; }
        public FibPercentage Wave3aProj     { get; set; }
        public FibPercentage Wave3cProj     { get; set; }
        public FibPercentage Wave3CtoWave3A { get; set; }

        public FibPercentage Wave2toWave1   { get; set; }

        public FibPercentage Wave3bRet      { get; set; }
        public FibPercentage Wave3btoWave1  { get; set; }

        public FibPercentage Wave4Ret       { get; set; }
        public FibPercentage Wave4toWave1   { get; set; }

        public FibPercentage Wave5AProj     { get; set; }
        public FibPercentage Wave5CProj     { get; set; }
        public FibPercentage Wave5toWave1   { get; set; }

        public Wave3Type Wave3Type          { get; set; }

        private PooledList< FibLevelInfo > _predictedTargets = null;

        public PooledList<FibLevelInfo> PredictedTargets
        {
            get
            {
                return _predictedTargets;
            }
        }

        public void ProcessFinishedWaves()
        {
            var hews = _hews.GetElliottWavesDictionary( _k.Period );
            ProcessFinishedWaves( _k.WaveScenarioNo, _k.RawBeginTime, _k.RawEndTime, _k.WaveCycle, hews, null );
        }

        public void ProcessFinishedWaves( int waveScenarioNo, long beginTime, long endTime, ElliottWaveCycle cycle, UndoRedoBTreeDictionary<long, DbElliottWave> hews, List<DbElliottWave> dbHews )
        {
            if ( beginTime > -1 )
            {
                _wave0Time = beginTime;
                Bar0 = _bars.GetBarByTime( _wave0Time );
            }

            if ( endTime > -1 )
            {
                _wave5CTime = endTime;
                Bar5C = _bars.GetBarByTime( _wave5CTime );
            }
            
            

            var childCycle = cycle - GlobalConstants.OneWaveCycle;

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
                        case ElliottWaveEnum.Wave1:
                        case ElliottWaveEnum.Wave1C:
                        {
                            _wave1CTime = dbHew.StartDate;
                            Bar1C       = _bars.GetBarByTime( _wave1CTime );
                        }
                        break;

                        case ElliottWaveEnum.Wave2:
                        {
                            _wave2Time  = dbHew.StartDate;
                            Bar2        = _bars.GetBarByTime( _wave2Time );
                        }
                        break;

                        case ElliottWaveEnum.Wave3:
                        case ElliottWaveEnum.Wave3C:
                        {
                            _wave3CTime = dbHew.StartDate;
                            Bar3C       = _bars.GetBarByTime( _wave3CTime );
                        }    
                        break;
                        
                        case ElliottWaveEnum.Wave4:
                        {
                            _wave4Time  = dbHew.StartDate;
                            Bar4        = _bars.GetBarByTime( _wave4Time );
                        }    
                        break;
                        
                        case ElliottWaveEnum.Wave5:
                        case ElliottWaveEnum.Wave5C:
                        {
                            _wave5CTime = dbHew.StartDate;
                            Bar5C       = _bars.GetBarByTime( _wave5CTime );
                        }    
                        break;

                        case ElliottWaveEnum.WaveA:
                        {
                            if ( _wave1CTime == -1 && _wave2Time == -1 && _wave3CTime == -1 && _wave4Time == -1  )
                            {
                                _wave1ATime = dbHew.StartDate;
                                Bar1A       = _bars.GetBarByTime( _wave1ATime );
                            }
                            else if ( _wave1CTime > -1 && _wave2Time > -1 && _wave3CTime == -1 && _wave4Time == -1 )
                            {
                                _wave3ATime = dbHew.StartDate;
                                Bar3A       = _bars.GetBarByTime( _wave3ATime );
                            }
                            else if ( _wave1CTime > -1 && _wave2Time > -1 && _wave3CTime > -1 && _wave4Time > -1 )
                            {
                                _wave5ATime = dbHew.StartDate;
                                Bar5A       = _bars.GetBarByTime( _wave5ATime );
                            }
                        }
                        break;

                        case ElliottWaveEnum.WaveB:
                        {
                            if ( _wave1CTime == -1 && _wave2Time == -1 && _wave3CTime == -1 && _wave4Time == -1  )
                            {
                                _wave1BTime = dbHew.StartDate;
                                Bar1B = _bars.GetBarByTime( _wave1BTime );
                            }
                            else if ( _wave1CTime > -1 && _wave2Time > -1 && _wave3CTime == -1 && _wave4Time == -1  )
                            {
                                _wave3BTime = dbHew.StartDate;
                                Bar3B = _bars.GetBarByTime( _wave3BTime );
                            }
                            else if ( _wave1CTime > -1 && _wave2Time > -1 && _wave3CTime > -1 && _wave4Time > -1 && _wave5ATime > -1 )
                            {
                                _wave5BTime = dbHew.StartDate;
                                Bar5B = _bars.GetBarByTime( _wave5BTime );
                            }
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
            }

            var symbolex = SymbolHelper.ToSymbolEx( _bars.Security.ToSecurityId(), _bars.Period.Value );

            if ( _wave1CTime > -1 )
            {
                _wave2Ret = _hews.GetHewFibTargets( waveScenarioNo, symbolex, _bars.Period.Value, _wave1CTime, ElliottWaveEnum.Wave1, childCycle );

                if ( _wave2Ret != null && Bar2 != SBar.EmptySBar )
                {
                    if ( _wave2Ret.GetClosestFibLevel( Bar2.PeakTroughValue, out FibLevelInfo wave2Retracement ) )
                    {
                        Wave2Ret = wave2Retracement.FibPrecentage;
                    }
                }
            }

            if ( _wave2Time > -1 )
            {
                _wave3Exp = _hews.GetHewFibTargets( waveScenarioNo, symbolex, _bars.Period.Value, _wave2Time, ElliottWaveEnum.Wave2, childCycle );

                if ( _wave3Exp != null )
                {
                    if ( Bar3A != SBar.EmptySBar )
                    {
                        if ( _wave3Exp.GetClosestFibLevel( Bar3A.PeakTroughValue, out FibLevelInfo wave3AtoWave1 ) )
                        {
                            Wave3aProj = wave3AtoWave1.FibPrecentage;
                        }
                    }

                    if ( Bar3B != SBar.EmptySBar )
                    {
                        if ( _wave3Exp.GetClosestFibLevel( Bar3B.PeakTroughValue, out FibLevelInfo wave3btowave1 ) )
                        {
                            Wave3btoWave1 = wave3btowave1.FibPrecentage;
                        }
                    }

                    if ( Bar3C != SBar.EmptySBar )
                    {
                        if ( _wave3Exp.GetClosestFibLevel( Bar3C.PeakTroughValue, out FibLevelInfo wave3CtoWave1 ) )
                        {
                            Wave3cProj = wave3CtoWave1.FibPrecentage;
                        }
                    }
                }
            }

            if ( _wave3ATime > -1 )
            {
                _wave3BRet = _hews.GetHewFibTargets( waveScenarioNo, symbolex, _bars.Period.Value, _wave3ATime, ElliottWaveEnum.WaveA, childCycle );

                if ( _wave3BRet != null && Bar3B != SBar.EmptySBar )
                {
                    if ( _wave3BRet.GetClosestFibLevel( Bar3B.PeakTroughValue, out FibLevelInfo wave3BRetValue ) )
                    {
                        Wave3bRet = wave3BRetValue.FibPrecentage;
                    }

                    if ( _wave3Exp != null )
                    {
                        if ( _wave3Exp.GetClosestFibLevel( Bar3B.PeakTroughValue, out FibLevelInfo wave3btoWave1Value ) )
                        {
                            Wave3btoWave1 = wave3btoWave1Value.FibPrecentage;
                        }
                    }
                }
            }


            if ( _wave3CTime > -1 )
            {
                _wave4Ret = _hews.GetHewFibTargets( waveScenarioNo, symbolex, _bars.Period.Value, _wave3CTime, ElliottWaveEnum.Wave3, childCycle );

                if ( _wave4Ret != null && Bar4 != SBar.EmptySBar )
                {
                    if ( _wave4Ret.GetClosestFibLevel( Bar4.PeakTroughValue, out FibLevelInfo wave4RetValue ) )
                    {
                        Wave4Ret = wave4RetValue.FibPrecentage;
                    }

                    if ( _wave3Exp != null )
                    {
                        if ( _wave3Exp.GetClosestFibLevel( Bar4.PeakTroughValue, out FibLevelInfo wave4toWave1Value ) )
                        {
                            Wave4toWave1 = wave4toWave1Value.FibPrecentage;
                        }
                    }
                }
            }

            if ( _wave3ATime > -1 && _wave3BTime > -1 && _wave3CTime > -1 )
            {
                _wave3CExp = _hews.GetHewFibTargets( waveScenarioNo, symbolex, _bars.Period.Value, _wave3BTime, ElliottWaveEnum.WaveB, childCycle );

                if ( _wave3CExp != null && Bar3C != SBar.EmptySBar )
                {
                    if ( _wave3CExp.GetClosestFibLevel( Bar3C.PeakTroughValue, out FibLevelInfo wave3CtoWave3a ) )
                    {
                        Wave3CtoWave3A = wave3CtoWave3a.FibPrecentage;
                    }                    
                }
            }

            if ( _wave4Time > -1 )
            {
                _wave5Exp = _hews.GetHewFibTargets( waveScenarioNo, symbolex, _bars.Period.Value, _wave4Time, ElliottWaveEnum.Wave4, childCycle );

                if ( _wave5Exp != null )
                {
                    if ( Bar5A != SBar.EmptySBar )
                    {
                        if ( _wave5Exp.GetClosestFibLevel( Bar5A.PeakTroughValue, out FibLevelInfo wave5AExp ) )
                        {
                            Wave5AProj = wave5AExp.FibPrecentage;
                        }
                    }

                    if ( Bar5C != SBar.EmptySBar )
                    {
                        if ( _wave5Exp.GetClosestFibLevel( Bar5C.PeakTroughValue, out FibLevelInfo wave5CExp ) )
                        {
                            Wave5CProj = wave5CExp.FibPrecentage;
                        }
                    }
                }

                if ( _wave3Exp != null )
                {
                    if ( _wave3Exp.GetClosestFibLevel( Bar5C.PeakTroughValue, out FibLevelInfo wave5CtoWave1 ) )
                    {
                        Wave5toWave1 = wave5CtoWave1.FibPrecentage;
                    }
                }
            }


        }

        public void ProcessOngoingHews( int waveScenarioNo, long beginTime, long endTime, ElliottWaveCycle cycle, UndoRedoBTreeDictionary<long, DbElliottWave> hews, List<DbElliottWave> dbHews )
        {
            if ( beginTime > -1 )
            {
                _wave0Time  = beginTime;
                Bar0        = _bars.GetBarByTime( _wave0Time );
            }

            if ( endTime > -1 )
            {
                _wave5CTime = endTime;
                Bar5C       = _bars.GetBarByTime( _wave5CTime );
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
                        case ElliottWaveEnum.Wave1:
                        case ElliottWaveEnum.Wave1C:
                        {
                            _wave1CTime = dbHew.StartDate;
                            Bar1C       = _bars.GetBarByTime( _wave1CTime );
                        }
                        break;

                        case ElliottWaveEnum.Wave2:
                        {
                            _wave2Time = dbHew.StartDate;
                            Bar2       = _bars.GetBarByTime( _wave2Time );
                        }
                        break;

                        case ElliottWaveEnum.Wave3:
                        case ElliottWaveEnum.Wave3C:
                        {
                            _wave3CTime = dbHew.StartDate;
                            Bar3C       = _bars.GetBarByTime( _wave3CTime );
                        }
                        break;

                        case ElliottWaveEnum.Wave4:
                        {
                            _wave4Time = dbHew.StartDate;
                            Bar4       = _bars.GetBarByTime( _wave4Time );
                        }
                        break;

                        case ElliottWaveEnum.Wave5:
                        case ElliottWaveEnum.Wave5C:
                        {
                            _wave5CTime = dbHew.StartDate;
                            Bar5C       = _bars.GetBarByTime( _wave5CTime );
                        }
                        break;

                        case ElliottWaveEnum.WaveA:
                        {
                            if ( _wave1CTime == -1 && _wave2Time == -1 && _wave3CTime == -1 && _wave4Time == -1 )
                            {
                                _wave1ATime = dbHew.StartDate;
                                Bar1A       = _bars.GetBarByTime( _wave1ATime );
                            }
                            else if ( _wave1CTime > -1 && _wave2Time > -1 && _wave3CTime == -1 && _wave4Time == -1 )
                            {
                                _wave3ATime = dbHew.StartDate;
                                Bar3A       = _bars.GetBarByTime( _wave3ATime );
                            }
                            else if ( _wave1CTime > -1 && _wave2Time > -1 && _wave3CTime > -1 && _wave4Time > -1 )
                            {
                                _wave5ATime = dbHew.StartDate;
                                Bar5A       = _bars.GetBarByTime( _wave5ATime );
                            }
                        }
                        break;

                        case ElliottWaveEnum.WaveB:
                        {
                            if ( _wave1CTime == -1 && _wave2Time == -1 && _wave3CTime == -1 && _wave4Time == -1 )
                            {
                                _wave1BTime = dbHew.StartDate;
                                Bar1B       = _bars.GetBarByTime( _wave1BTime );
                            }
                            else if ( _wave1CTime > -1 && _wave2Time > -1 && _wave3CTime == -1 && _wave4Time == -1 )
                            {
                                _wave3BTime = dbHew.StartDate;
                                Bar3B       = _bars.GetBarByTime( _wave3BTime );
                            }
                            else if ( _wave1CTime > -1 && _wave2Time > -1 && _wave3CTime > -1 && _wave4Time > -1 && _wave5ATime > -1 )
                            {
                                _wave5BTime = dbHew.StartDate;
                                Bar5B       = _bars.GetBarByTime( _wave5BTime );
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

                        case ElliottWaveEnum.WaveX:
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
                else
                {
                    var grandchildrenWI = hew.GetHewPointInfoAtCycle( grandChild );

                    if ( grandchildrenWI.HasValue )
                    {
                        lowerDegWave = grandchildrenWI.Value.WaveName;

                        if ( lastWaveName.IsNextWaveOneDegreeLowerBeginning( lowerDegWave ) )
                        {
                            var grandChildrendbHews = hews.Where( x => x.Value.StartDate >= dbHew.StartDate ).OrderBy( x => x.Value.StartDate );
                            
                            long grandChildEndedTime = -1;

                            foreach ( var hewPair in grandChildrendbHews )
                            {
                                var myHew = hewPair.Value;

                                ref var hew1st = ref myHew.GetWaveFromScenario( waveScenarioNo );

                                if ( hew1st.HasHigherDegreeWave( grandChild ) )
                                {
                                    grandChildEndedTime = hewPair.Key;
                                    break;
                                }                                
                            }
                                                        
                            if ( grandChildEndedTime > -1 )
                            {
                                grandChildrendbHews = hews.Where( x => x.Value.StartDate >= dbHew.StartDate && x.Value.StartDate <= grandChildEndedTime ).OrderBy( x => x.Value.StartDate );
                            }
                            

                            var aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _bars.Security );

                            if ( is5Waves( grandChildrendbHews, grandChild, waveScenarioNo ) )
                            {                                
                                var lowerDegWave5 = Get5thWave( grandChildrendbHews, grandChild, waveScenarioNo );

                                var waveKey       = new WaveModelKey( _k.Security, _k.Period, waveScenarioNo, lastWaveTime, lowerDegWave5 == null ? -1 : lowerDegWave5.StartDate, grandChild );
                                
                                C5Waves c5waves   = null;
                                
                                if ( aa.GetOrCreate5Waves( waveKey, _bars, _hews, out c5waves ) )
                                {
                                    SetCurrentSubWave( lastWaveName, c5waves );

                                    var listHews = grandChildrendbHews.Select( x => x.Value ).ToList();

                                    if ( lowerDegWave5 != null )
                                    {
                                        c5waves.ProcessFinishedWaves( waveScenarioNo, lastWaveTime, lowerDegWave5.StartDate, childCycle, hews, listHews );
                                    }
                                    else
                                    {
                                        
                                        c5waves.ProcessOngoingHews( waveScenarioNo, lastWaveTime, -1, childCycle, hews, listHews );
                                    }
                                    //
                                }
                            }
                            else
                            {
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

            var symbolex = SymbolHelper.ToSymbolEx( _bars.Security.ToSecurityId(), _bars.Period.Value );

            if ( _wave1CTime > -1 )
            {
                _wave2Ret = _hews.GetHewFibTargets( waveScenarioNo, symbolex, _bars.Period.Value, _wave1CTime, ElliottWaveEnum.Wave1, childCycle );

                if ( _wave2Ret != null && Bar2 != SBar.EmptySBar )
                {
                    if ( _wave2Ret.GetClosestFibLevel( Bar2.PeakTroughValue, out FibLevelInfo wave2Retracement ) )
                    {
                        Wave2Ret = wave2Retracement.FibPrecentage;
                    }
                }
            }

            if ( _wave2Time > -1 )
            {
                _wave3Exp = _hews.GetHewFibTargets( waveScenarioNo, symbolex, _bars.Period.Value, _wave2Time, ElliottWaveEnum.Wave2, childCycle );

                if ( _wave3Exp != null )
                {
                    if ( Bar3A != SBar.EmptySBar )
                    {
                        if ( _wave3Exp.GetClosestFibLevel( Bar3A.PeakTroughValue, out FibLevelInfo wave3AtoWave1 ) )
                        {
                            Wave3aProj = wave3AtoWave1.FibPrecentage;
                        }
                    }

                    if ( Bar3B != SBar.EmptySBar )
                    {
                        if ( _wave3Exp.GetClosestFibLevel( Bar3B.PeakTroughValue, out FibLevelInfo wave3btowave1 ) )
                        {
                            Wave3btoWave1 = wave3btowave1.FibPrecentage;
                        }
                    }

                    if ( Bar3C != SBar.EmptySBar )
                    {
                        if ( _wave3Exp.GetClosestFibLevel( Bar3C.PeakTroughValue, out FibLevelInfo wave3CtoWave1 ) )
                        {
                            Wave3cProj = wave3CtoWave1.FibPrecentage;
                        }
                    }
                }
            }

            if ( _wave3ATime > -1 )
            {
                _wave3BRet = _hews.GetHewFibTargets( waveScenarioNo, symbolex, _bars.Period.Value, _wave3ATime, ElliottWaveEnum.WaveA, childCycle );

                if ( _wave3BRet != null && Bar3B != SBar.EmptySBar )
                {
                    if ( _wave3BRet.GetClosestFibLevel( Bar3B.PeakTroughValue, out FibLevelInfo wave3BRetValue ) )
                    {
                        Wave3bRet = wave3BRetValue.FibPrecentage;
                    }

                    if ( _wave3Exp != null )
                    {
                        if ( _wave3Exp.GetClosestFibLevel( Bar3B.PeakTroughValue, out FibLevelInfo wave3btoWave1Value ) )
                        {
                            Wave3btoWave1 = wave3btoWave1Value.FibPrecentage;
                        }
                    }
                }
            }


            if ( _wave3CTime > -1 )
            {
                _wave4Ret = _hews.GetHewFibTargets( waveScenarioNo, symbolex, _bars.Period.Value, _wave3CTime, ElliottWaveEnum.Wave3, childCycle );

                if ( _wave4Ret != null && Bar4 != SBar.EmptySBar )
                {
                    if ( _wave4Ret.GetClosestFibLevel( Bar4.PeakTroughValue, out FibLevelInfo wave4RetValue ) )
                    {
                        Wave4Ret = wave4RetValue.FibPrecentage;
                    }

                    if ( _wave3Exp != null )
                    {
                        if ( _wave3Exp.GetClosestFibLevel( Bar4.PeakTroughValue, out FibLevelInfo wave4toWave1Value ) )
                        {
                            Wave4toWave1 = wave4toWave1Value.FibPrecentage;
                        }
                    }
                }
            }

            if ( _wave4Time > -1 )
            {
                _wave5Exp = _hews.GetHewFibTargets( waveScenarioNo, symbolex, _bars.Period.Value, _wave4Time, ElliottWaveEnum.Wave4, childCycle );

                if ( _wave5Exp != null )
                {
                    if ( Bar5A != SBar.EmptySBar )
                    {
                        if ( _wave5Exp.GetClosestFibLevel( Bar5A.PeakTroughValue, out FibLevelInfo wave5AExp ) )
                        {
                            Wave5AProj = wave5AExp.FibPrecentage;
                        }
                    }

                    if ( Bar5C != SBar.EmptySBar )
                    {
                        if ( _wave5Exp.GetClosestFibLevel( Bar5C.PeakTroughValue, out FibLevelInfo wave5CExp ) )
                        {
                            Wave5CProj = wave5CExp.FibPrecentage;
                        }
                    }
                }

                if ( _wave3Exp != null )
                {
                    if ( _wave3Exp.GetClosestFibLevel( Bar5C.PeakTroughValue, out FibLevelInfo wave5CtoWave1 ) )
                    {
                        Wave5toWave1 = wave5CtoWave1.FibPrecentage;
                    }
                }
            }
        }

        public bool is5Waves( IOrderedEnumerable< KeyValuePair<long, DbElliottWave >> dbHews, ElliottWaveCycle cycle, int waveScenarioNo )
        {
            foreach ( var hewPair in dbHews )
            {
                var dbHew = hewPair.Value;

                ref var hew  = ref dbHew.GetWaveFromScenario( waveScenarioNo );

                var waveInfo = hew.GetHewPointInfoAtCycle( cycle );

                if ( waveInfo.HasValue )
                {
                    switch ( waveInfo.Value.WaveName )
                    {
                        case ElliottWaveEnum.Wave1:
                        case ElliottWaveEnum.Wave1C:
                        case ElliottWaveEnum.Wave2:
                        case ElliottWaveEnum.Wave3:
                        case ElliottWaveEnum.Wave3C:
                        case ElliottWaveEnum.Wave5:
                        case ElliottWaveEnum.Wave5C:
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public DbElliottWave Get5thWave( IOrderedEnumerable<KeyValuePair<long, DbElliottWave>> dbHews, ElliottWaveCycle cycle, int waveScenarioNo )
        {
            foreach ( var hewPair in dbHews )
            {
                var dbHew = hewPair.Value;

                ref var hew  = ref dbHew.GetWaveFromScenario( waveScenarioNo );

                if ( hew.IsWave5OrHigherDegree( cycle ) )
                {
                    return dbHew;
                }
            }
            return null;
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
                case ElliottWaveEnum.Wave1:
                case ElliottWaveEnum.Wave1C:
                {
                    _subWave2 = subWave;
                }
                break;    

                case ElliottWaveEnum.Wave2:
                {
                    _subWave3A = subWave;
                }
                break;

                case ElliottWaveEnum.Wave3:
                case ElliottWaveEnum.Wave3C:
                {
                    _subWave4 = subWave;
                }
                break;

                case ElliottWaveEnum.Wave4:
                {
                    _subWave5A = subWave;
                }
                break;



                case ElliottWaveEnum.WaveA:
                {
                    if ( _wave1CTime == -1 && _wave2Time == -1 && _wave3CTime == -1 && _wave4Time == -1 )
                    {
                        _subWave1B = subWave;
                    }
                    else if ( _wave1CTime > -1 && _wave2Time > -1 && _wave3CTime == -1 && _wave4Time == -1 )
                    {
                        _subWave3B = subWave;
                    }
                    else if ( _wave1CTime > -1 && _wave2Time > -1 && _wave3CTime > -1 && _wave4Time > -1 )
                    {
                        _subWave5B = subWave;
                    }
                }
                break;

                case ElliottWaveEnum.WaveB:
                {
                    if ( _wave1CTime == -1 && _wave2Time == -1 && _wave3CTime == -1 && _wave4Time == -1 )
                    {
                        _subWave1C = subWave;
                    }
                    else if ( _wave1CTime > -1 && _wave2Time > -1 && _wave3CTime == -1 && _wave4Time == -1 )
                    {
                        _subWave3C = subWave;
                    }
                    else if ( _wave1CTime > -1 && _wave2Time > -1 && _wave3CTime > -1 && _wave4Time > -1 && _wave5ATime > -1 )
                    {
                        _subWave5C = subWave;
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

    }

}

