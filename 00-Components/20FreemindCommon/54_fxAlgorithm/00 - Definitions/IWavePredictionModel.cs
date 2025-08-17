using fx.Bars;
using fx.Collections;
using fx.Common;
using fx.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Algorithm
{
    public interface IWavePredictionModel
    {        
        //void AnalysePastImpulsiveMove( int waveScenarioNo, TimeSpan period, fxHistoricBarsRepo bars, long selectedBarTime, ElliottWaveEnum waveName, ElliottWaveCycle waveDegree );
        //void AnalyseWave5Movement( int waveScenarioNo, TimeSpan period, fxHistoricBarsRepo bars, long selectedBarTime, ElliottWaveEnum waveName, ElliottWaveCycle waveDegree );

        //void AnalyseCurrentCorrection( int waveScenarioNo, TimeSpan period, fxHistoricBarsRepo bars, long selectedBarTime, ElliottWaveEnum waveName, ElliottWaveCycle waveDegree );

        PooledList<FibLevelInfo> LargerTargets { get; set; }
        PooledList<FibLevelInfo> UnfoldingTargets { get; set; }
    }
}
