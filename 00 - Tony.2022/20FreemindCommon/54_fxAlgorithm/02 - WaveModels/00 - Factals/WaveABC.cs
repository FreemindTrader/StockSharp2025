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

#pragma warning disable 414

namespace fx.Algorithm
{
    public class WaveABC : I3Waves
    {
        private WaveType _waveCType;
        private WaveType _waveAType;
        private HewManager _hews;
        private fxHistoricBarsRepo _bars;

        private WaveModelKey _k = null;

        private IFactal _precedingWaves = null;

        private HewFibGannTargets _precedingWavesRetracement = null;

        private long _wave0Time = -1;
        private long _waveATime = -1;
        private long _waveBTime = -1;
        private long _waveCTime = -1;

        private IWave _waveA = null;
        private IWave _waveB = null;
        private IWave _waveC = null;

        public WaveABC( fxHistoricBarsRepo bars, HewManager hewManager, WaveModelKey k )
        {
            _bars = bars;
            _hews = hewManager;
            _k    = k;
        }

        public void StartAnalysis( ref HewLong hew, ElliottWaveEnum highestWaveName, ElliottWaveCycle highestWaveDegree, IFactal precedingWaves, HewFibGannTargets retracement )
        {
            _precedingWaves = precedingWaves;
            _precedingWavesRetracement = retracement;

            var waveInfos = hew.GetAllWaves();

            foreach ( var waveInfo in waveInfos )
            {
                //AnalyzeWaveInfo( waveInfo );
            }
        }

        public WaveType MainWaveType { get; set; }
        public long BeginTime { get; set; }
        public long EndTime { get; set; }

        public bool HasChildren { get; set; }
        public PooledList<FibLevelInfo> PredictedTargets { get; }


        public WaveType WaveAType
        {
            get { return _waveAType; }
            set
            {
                _waveAType = value;
            }
        }

        
        public WaveType WaveCType
        {
            get { return _waveCType; }
            set
            {
                _waveCType = value;
            }
        }
        

        private WaveType DetectWaveA( long wave0Time, long waveATime )
        {
            WaveType output = WaveType.UNKNOWN;

            return output;
        }

        private WaveType DetectWaveC( long wave0Time, long waveCTime )
        {
            WaveType output = WaveType.UNKNOWN;

            return output;
        }

        public SBar Bar0;

        public SBar BarA;

        public SBar BarB;

        public SBar BarC;

        public long Wave0Time { get; set; }
        public long WaveA1Time { get; set; }
        public long WaveB1Time { get; set; }
        public long WaveC1Time { get; set; }
        public long WaveA2Time { get; set; }
        public long WaveB2Time { get; set; }
        public long WaveC2Time { get; set; }
        public long WaveA3Time { get; set; }
        public long WaveB3Time { get; set; }
        public long WaveC3Time { get; set; }

        public IFactal SubWaveA1 { get; set; }
        public IFactal SubWaveB1 { get; set; }
        public IFactal SubWaveC1 { get; set; }
        public IFactal SubWaveA2 { get; set; }
        public IFactal SubWaveB2 { get; set; }
        public IFactal SubWaveC2 { get; set; }
        public IFactal SubWaveA3 { get; set; }
        public IFactal SubWaveB3 { get; set; }
        public IFactal SubWaveC3 { get; set; }
    }
}
