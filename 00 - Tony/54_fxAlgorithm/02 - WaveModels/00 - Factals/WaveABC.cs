using fx.Database;
using fx.Bars;
using fx.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable 414

namespace fx.Algorithm
{
    public class WaveABC
    {
        WaveType _waveCType;
        WaveType _waveAType;
        HewManager _hews;
        fxHistoricBarsRepo _bars;

        long _wave0Time = -1;
        long _waveATime = -1;
        long _waveBTime = -1;
        long _waveCTime = -1;

        IWave _waveA = null;
        IWave _waveB = null;
        IWave _waveC = null;

        public WaveABC( fxHistoricBarsRepo bars, HewManager hewManager )
        {
            _bars = bars;
            _hews = hewManager;
        }

        


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
    }
}
