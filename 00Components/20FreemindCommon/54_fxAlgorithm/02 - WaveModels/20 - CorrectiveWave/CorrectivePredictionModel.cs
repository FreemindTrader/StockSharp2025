//using fx.Collections;
//using fx.Database;
//using fx.Definitions;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using fx.Bars;
//using fx.Common;

//#pragma warning disable 414

//namespace fx.Algorithm
//{
//    public class CorrectivePredictionModel : IWavePredictionModel
//    {
//        WavePattern _correctionPattern;
//        HewManager _hews;
//        fxHistoricBarsRepo _bars;

//        PooledList< FibLevelInfo > _largerTargets = null;
//        PooledList< FibLevelInfo > _unfoldingTargets = null;
//        private PooledList< FibLevelInfo > _predictedTargets = null;

//        public CorrectivePredictionModel( fxHistoricBarsRepo bars, HewManager hewManager )
//        {
//            _bars = bars;
//            _hews = hewManager;
//        }

//        public PooledList<FibLevelInfo> LargerTargets
//        {
//            get { return _largerTargets; }
//            set
//            {
//                _largerTargets = value;
//            }
//        }

//        public PooledList<FibLevelInfo> UnfoldingTargets
//        {
//            get { return _unfoldingTargets; }
//            set
//            {
//                _unfoldingTargets = value;
//            }
//        }

//        public PooledList<FibLevelInfo> PredictedTargets
//        {
//            get { return _unfoldingTargets; }
//            set
//            {
//                _unfoldingTargets = value;
//            }
//        }

//        
//        public WavePattern CorrectionPattern
//        {
//            get { return _correctionPattern; }
//            set
//            {
//                _correctionPattern = value;
//            }
//        }
//        

//        public void AnalysePastImpulsiveMove( int waveScenarioNo, TimeSpan period, fxHistoricBarsRepo bars, long selectedBarTime, ElliottWaveEnum waveName, ElliottWaveCycle waveDegree )
//        {
//            var postActions = _hews.GetAllWavesOfDegreeAfter(waveScenarioNo, period, selectedBarTime, waveName, waveDegree );

//            _correctionPattern = GetCorrectionPattern( waveScenarioNo, selectedBarTime, postActions );

//            if ( postActions.Count > 0 )
//            {
//                var lastWave = postActions[ postActions.Count - 1 ];

//                var lastTopWave = lastWave.GetWaveFromScenario( waveScenarioNo ).GetFirstHighestWaveInfo( ); 

//                if ( lastTopWave.HasValue )
//                {                    
//                    var lastWaveName   = lastTopWave.Value.WaveName;

//                    WavePredictionModel waveC5Waves = null;

//                    switch ( lastWaveName )
//                    {
//                        case ElliottWaveEnum.WaveB:
//                        {
//                            var waveCTarget = _hews.GetHewFibTargets( waveScenarioNo, SymbolHelper.ToSymbolEx( _bars.Security, period ), lastWave.StartDate, ElliottWaveEnum.WaveB, waveDegree );

//                            if ( waveCTarget != null )
//                            {
//                                var cSubDegree        = waveDegree - GlobalConstants.OneWaveCycle;
//                                var waveCSub          = _hews.GetAllWavesOfDegreeAfter( waveScenarioNo,period, lastWave.StartDate, ElliottWaveEnum.WaveB,  cSubDegree);
//                                var lastWaveInCSub    = waveCSub[ waveCSub.Count - 1 ];
//                                var lastTopWaveInCSub = lastWaveInCSub.GetWaveFromScenario( waveScenarioNo ).GetFirstHighestWaveInfo( );

//                                if ( lastTopWaveInCSub.HasValue )
//                                {
//                                    var lastTopWaveNameInCSub = lastTopWaveInCSub.Value.WaveName;

//                                    switch ( lastTopWaveNameInCSub )
//                                    {
//                                        // ![](AC2A661DBE0D6DDBEAFE608B61FE5936.png;;;0.03797,0.01903)
//                                        // Need to use Wave 2 and Wave 4 to calculate the final destination.
//                                        // In this case we have an extended Wave 3 model.

//                                        case ElliottWaveEnum.Wave2:
//                                        {

//                                        }
//                                        break;

//                                        case ElliottWaveEnum.Wave4:
//                                        {
//                                            waveC5Waves = new WavePredictionModel( bars, _hews );                                            

//                                            waveC5Waves.AnalysePastImpulsiveMove( waveScenarioNo, period, bars, lastWaveInCSub.StartDate, ElliottWaveEnum.Wave4, cSubDegree );
//                                            waveC5Waves.AnalyseCurrentCorrection( waveScenarioNo, period, bars, lastWaveInCSub.StartDate, ElliottWaveEnum.Wave3C, cSubDegree );
//                                        }
//                                        break;
//                                    }

//                                    PooledList< FibLevelInfo > allLevels = null;

//                                    if ( waveC5Waves != null )
//                                    {
//                                        if ( waveC5Waves.LargerTargets.Count > 0 )
//                                        {
//                                            var diff = (double) _bars.Security.PriceStep.Value;
//                                            allLevels = waveC5Waves.LargerTargets;

//                                            waveC5Waves.GetFibExtremum( out FibLevelInfo highestLowestFib );

//                                            foreach ( FibLevelInfo fibLevelInfo in waveCTarget.RegularRetraceProjectionLevels.FibLevels )
//                                            {
//                                                var index = allLevels.FindIndex( x => x.WithinCluster( fibLevelInfo, diff * 4  ) );

//                                                if ( index > -1 )
//                                                {
//                                                    var selectedLevel = allLevels[ index ];

//                                                    selectedLevel.UpdateAll( fibLevelInfo );                                                    
//                                                }
//                                                else
//                                                {
//                                                    if ( waveCTarget.IsUptrend )
//                                                    {
//                                                        if ( fibLevelInfo.FibLevel < highestLowestFib.FibLevel )
//                                                        {
//                                                            if ( waveC5Waves.Bar3C != SBar.EmptySBar )
//                                                            {
//                                                                if ( fibLevelInfo.FibLevel > waveC5Waves.Bar3C.PeakTroughValue )
//                                                                {
//                                                                    allLevels.Add( fibLevelInfo );
//                                                                }
//                                                            }
//                                                            else
//                                                            {
//                                                                allLevels.Add( fibLevelInfo );
//                                                            }                                                            
//                                                        }
//                                                    }
//                                                    else
//                                                    {
//                                                        if ( fibLevelInfo.FibLevel > highestLowestFib.FibLevel )
//                                                        {
//                                                            if ( waveC5Waves.Bar3C != SBar.EmptySBar)
//                                                            {
//                                                                if ( fibLevelInfo.FibLevel < waveC5Waves.Bar3C.PeakTroughValue )
//                                                                {
//                                                                    allLevels.Add( fibLevelInfo );
//                                                                }
//                                                            }
//                                                            else
//                                                            {
//                                                                allLevels.Add( fibLevelInfo );
//                                                            }
//                                                            
//                                                        }
//                                                    }
//                                                }
//                                            }
//                                        }
//                                    }

//                                    _largerTargets = allLevels;
//                                }
//                            }
//                            
//                        }
//                        break;
//                    }

//                }

//            }
//        }

//        private WavePattern GetCorrectionPattern( int waveScenarioNo, long wave0Time, PooledList<DbElliottWave> waves )
//        {
//            var xWaveCount = from n in waves where n.GetWaveFromScenario( waveScenarioNo ).GetFirstHighestWaveInfo( ).HasValue && n.GetWaveFromScenario( waveScenarioNo ).GetFirstHighestWaveInfo( ).Value.WaveName == ElliottWaveEnum.WaveX select n;

//            var xCount = xWaveCount.Count( );

//            if ( xCount > 0 )
//            {
//                int firstXIndex = waves.FindIndex( r => r == xWaveCount.First() );
//                int secondXIndex = -1;

//                if ( xCount == 1 )
//                {                                        
//                    if ( firstXIndex > -1 )
//                    {
//                        var firstABC = waves.Take( firstXIndex - 1 );
//                        //var waveABC1st = new WaveABC( _bars, _hews, wave0Time, firstABC );
//                    }

//                    return WavePattern.DoubleZigZag;
//                }
//                else if ( xCount == 2 )
//                {
//                    if ( firstXIndex != -1 )
//                    {
//                        secondXIndex = waves.FindIndex( firstXIndex + 1, r => r == xWaveCount.Last() );

//                        if ( secondXIndex > -1 )
//                        {
//                            var secondABC = waves.Skip(firstXIndex + 1).Take(secondXIndex - (firstXIndex +1));

//                            //var waveABC2nd = new WaveABC( _bars, _hews, wave0Time, secondABC );
//                        }
//                    }
//                    
//                    return WavePattern.TripleZigZag;
//                }
//            }

//            return WavePattern.UNKNOWN;
//        }

//        public void AnalyseCurrentCorrection( int waveScenarioNo, TimeSpan period, fxHistoricBarsRepo bars, long selectedBarTime, ElliottWaveEnum waveName, ElliottWaveCycle waveDegree )
//        {
//            
//        }

//        public void AnalyseWave5Movement( int waveScenarioNo, TimeSpan period, fxHistoricBarsRepo bars, long selectedBarTime, ElliottWaveEnum waveName, ElliottWaveCycle waveDegree )
//        {

//        }
//    }
//}
