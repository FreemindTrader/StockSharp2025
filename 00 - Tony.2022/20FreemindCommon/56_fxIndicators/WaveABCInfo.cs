using fx.Algorithm;
using fx.Definitions;
using System;
using fx.Collections;
using System.Text;
using fx.Bars;

namespace fx.Indicators
{
    public class WaveABCInfo
    {        
        public WaveABCInfo( TrendDirection dir, ref SBar startbar, ref SBar barA, ref SBar barB, ref SBar barC, WaveSRLineResponse responseA, WaveSRLineResponse responseB, WaveSRLineResponse responseC, bool deepWaveB, double wave3cTo3a )
        {
            _dir             = dir;
            _startBar        = startbar;
            _barA            = barA;
            _barB            = barB;
            _barC            = barC;
            _responseA       = responseA;
            _responseB       = responseB;
            _responseC       = responseC;
            _deepWaveB       = deepWaveB;
            _wave3cTo3aRatio = wave3cTo3a;
        }


        bool _deepWaveB;
        private TrendDirection _dir;

        private SBar _startBar;
        private SBar _barA;
        private SBar _barB;
        private SBar _barC;

        private WaveSRLineResponse _responseA;
        private WaveSRLineResponse _responseB;
        private WaveSRLineResponse _responseC;

        double _wave3cTo3aRatio = 0d;


        public double CToA
        {
            get
            {

                return _wave3cTo3aRatio;
            }
        }

        
        public bool DeepWaveB
        {
            get { return _deepWaveB; }
            set
            {
                _deepWaveB = value;
            }
        }
        

        public WaveSRLineResponse ResponseA
        {
            get
            {
                return _responseA;
            }
        }

        public SBar BarA
        {
            get
            {
                return _barA;
            }
        }

        public SBar BarB
        {
            get
            {
                return _barB;
            }
        }

        public SBar BarC
        {
            get
            {
                return _barC;
            }
        }

        

        public WaveSRLineResponse ResponseB
        {
            get
            {
                return _responseB;
            }
        }

        

        public WaveSRLineResponse ResponseC
        {
            get
            {
                return _responseC;
            }
        }


        public override string ToString( )
        {
            string output = "";

            if ( _dir == TrendDirection.Uptrend )
            {
                output =  "[" + GlobalConstants.UpTrend + "] " + _startBar.BarIndex + GlobalConstants.UpTrendArrow + _barA.BarIndex + GlobalConstants.UpTrendRetracement + _barB.BarIndex + GlobalConstants.UpTrendArrow + _barC.BarIndex;
            }
            else
            {
                output = "[" + GlobalConstants.DownTrend + "] " + _startBar.BarIndex + GlobalConstants.DownTrendRetracement + _barA.BarIndex + GlobalConstants.DownTrendArrow + _barB.BarIndex + GlobalConstants.DownTrendRetracement + _barC.BarIndex;
            }

            return output;
        }

    }
}
