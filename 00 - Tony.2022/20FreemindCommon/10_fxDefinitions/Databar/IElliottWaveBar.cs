using fx.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{    
    public interface IElliottWaveBar : IBasicBar
    {
        ref HewLong MainElliottWave              { get; }
        ref HewLong GetWave( int waveScenarioNo );

        WavePriceTimeInfo MainPriceTimeInfo      { get; set; }
        WaveDirtyEnum     WaveDirty              { get; set; }
        string            WaveString             { get; }
        bool              HasElliottWave         { get; }
        bool              HasFibRetExpInfo       { get; }
        bool              IsSpecialBar           { get; set; }
        bool              IsSelected             { get; set; }
        bool              SelectionChanged       { get; set; }
        bool              HasBaZi                { get; }

        string            WaveRotationInfoString { get; }
        string            GannSquareInfoString   { get; }
        string            DivergenceInfoString   { get; }
        string            BaZiString             { get; set; }

        double            PeakTroughValue        { get; }

        

        

        bool IsWavePeak();
        bool IsPeak();
        bool IsTrough();
        bool IsExtremum();
        bool IsGannPeak();
        bool IsWaveTrough();
        bool IsGannTrough();

        PooledList<int> GetWaveRotationIndex();
        PooledList<int> GetDivergenceIndex();
        PooledList<int> GetGannSquareIndex();

        PooledList<WaveInfo> GetAllWaves( int waveScenarioNo, bool filterWaves );
        PooledList<WaveInfo> GetAllWaves( bool filterWaves );
        PooledList<WaveInfo> GetAllWaves( bool filterWaves, ElliottWaveCountType waveCountType );
        PooledList<WaveInfo> GetAllWaves( bool filterWaves, ElliottWaveCountType waveCountType, ElliottWaveCycle minimumToShow );
        
        PooledList<WaveInfo> GetTopWaves();
        PooledList<WaveInfo> GetTopWaves( int waveScenarioNo, bool filterWaves );
        PooledList<WaveInfo> GetTopWaves( bool filterWaves );
        PooledList<WaveInfo> GetTopWaves( bool filterWaves, ElliottWaveCountType waveCountType );
        PooledList<WaveInfo> GetTopWaves( bool filterWaves, ElliottWaveCountType waveCountType, ElliottWaveCycle minimumToShow );
        
        PooledList<WaveInfo> GetBottomWaves();
        PooledList<WaveInfo> GetBottomWaves( int waveScenarioNo, bool filterWaves );
        PooledList<WaveInfo> GetBottomWaves( bool filterWaves );
        PooledList<WaveInfo> GetBottomWaves( bool filterWaves, ElliottWaveCountType waveCountType );
        PooledList<WaveInfo> GetBottomWaves( bool filterWaves, ElliottWaveCountType waveCountType, ElliottWaveCycle minimumToShow );


        string GetPriceTimeInfoString( int waveScenarioNo );

        void AddWave( ref HewLong copy );
        void AddWave( int waveScenarioNo, ref HewLong copy );
        void MergeWave( int waveScenarioNo, ref WaveInfo wave );
        void UpdateWave( int waveScenarioNo, ref HewLong wave );
        
        void BranchWaves( int waveScenarioNo );
        void ConfirmWaves( int waveScenarioNo );
        void RemoveWavesFromDatabar( int waveScenarioNo );
        void RemoveWavesFromDatabar( int waveScenarioNo, HewLong wave );
        bool RemoveMatchedWavesFromDatabar( int waveScenarioNo, HewLong wave );

        void MergeHigherTimeFrameWaves( int waveScenarioNo, long waveBit, long extraWaveBit, WaveLabelPosition waveLabelPosition );
        void MergeHigherTimeFrameWaves( int waveScenarioNo, PooledList<WaveInfo> waves, WaveLabelPosition waveLabelPosition );
        void MergeHigherTimeFrameWaves( int waveScenarioNo, ref WaveInfo wave, WaveLabelPosition waveLabelPosition );
        void MergeHigherTimeFrameWaves( int waveScenarioNo, ref WaveInfo wave );
        
        int WaveScenarios { get; }

        void SwapMainWavesWithAlternative( );        
        void SwapMainWavesWithAlternative( HewLong wavesToBeChanged );
        

        bool MatchesWave( ref WaveInfo value );

        

        
        void SwapWavesAndZeroOut( );

        



        ElliottWaveEnum GetWaveCount( ElliottWaveCycle waveCycle );
        

        WaveInfo? GetFirstWave( ElliottWaveCountType waveCountType );        
        WaveInfo? GetLowestCycleWave( );

        

        WaveLabelPosition GetRetracmentInfoPos( );
        WaveLabelPosition GetWaveLabelPosition( );

        WaveLabelPosition GetWaveLabelPosition( ElliottWaveCountType waveCountType );

        
        string GetPivotRelationString();
                       

        

        

        TASignal GetExtremumType( );

        TASignal GetWaveType();
        TASignal GetGannType( );

        

        
    }
}
