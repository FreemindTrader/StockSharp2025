//using fx.Database;
//using fx.Definitions;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace fx.Algorithm
//{
//    public class FactalABC
//    {
//        HewManager _hews;
//        fxHistoricBarsRepo _bars;

//        long _wave0Time = -1;
//        long _waveATime = -1;
//        long _waveBTime = -1;
//        long _waveCTime = -1;

//        IWave _waveA = null;
//        IWave _waveB = null;
//        IWave _waveC = null;

//        public FactalABC( fxHistoricBarsRepo bars, HewManager hewManager )
//        {
//            _bars    = bars;
//            _hews = hewManager;
//        }

//        public FactalABC( fxHistoricBarsRepo bars, HewManager hewManager, long wave0, IEnumerable< DbElliottWave > waveABC )
//        {
//            _bars    = bars;
//            _hews = hewManager;
//            _wave0Time  = wave0;

//            foreach ( DbElliottWave wave in waveABC )
//            {
//                if ( wave.ElliottWave.HasElliottWave )
//                {
//                    var topWave = wave.ElliottWave.GetFirstHighestWaveInfo( );

//                    if ( topWave.HasValue )
//                    {
//                        var waveName = topWave.Value.WaveName;

//                        if ( waveName == ElliottWaveEnum.WaveA )
//                        {
//                            _waveATime = wave.StartDate;
//                        }
//                        else if ( waveName == ElliottWaveEnum.WaveB )
//                        {
//                            _waveBTime = wave.StartDate;
//                        }
//                        else if ( waveName == ElliottWaveEnum.WaveC )
//                        {
//                            _waveCTime = wave.StartDate;
//                        }
//                    }
//                }
//            }  
//            
//            if ( _wave0Time > 0 )
//            {
//                Bar0 = bars.GetBarByTime( _wave0Time );
//            }

//            if ( _waveATime > 0 )
//            {
//                BarA = bars.GetBarByTime( _waveATime );
//            }

//            if ( _waveBTime > 0 )
//            {
//                BarB = bars.GetBarByTime( _waveBTime );
//            }

//            if ( _waveCTime > 0 )
//            {
//                BarC = bars.GetBarByTime( _waveCTime );
//            }
//        }

//        public SBar Bar0 { get; set; }
//        public SBar BarA { get; set; }
//        public SBar BarB { get; set; }
//        public SBar BarC { get; set; }        
//    }
//}
