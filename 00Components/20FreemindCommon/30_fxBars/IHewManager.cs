using fx.Definitions;
using fx.Definitions.UndoRedo;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Bars
{
    public interface IHewManager
    {
        UndoRedoArea GetSelectedUndoRedoArea( TimeSpan period );
        bool RestoreElliottWaveToDatabar( fxHistoricBarsRepo bars, TimeSpan period, ref SBar tmpBar );
        bool GetElliottWave( int offerId,
                                    long lowerBound,
                                    long upperBound,
                                    TimeSpan period );
        int GetGannImportance( TimeSpan period, long rawBarTime );

        int GetWaveImportance( TimeSpan period, long rawBarTime );

        WavePointImportance GetWaveImportanceExt( TimeSpan period, long rawBarTime );

        WavePointImportance GetGannImportanceExt( TimeSpan period, long rawBarTime );

        int GetWaveCount( TimeSpan period );
        List<long> GetWaveDatesList( TimeSpan period );

        void RestoreElliottWaves( SBarList bar, TimeSpan period, int waveScenarioNo );
    }
}
