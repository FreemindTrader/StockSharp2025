using fx.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public interface IHarmonicElliottWave<T> 
    {        
        int Count { get; }        

        T RawWave { get; set; }        

        bool HasElliottWave { get; }

        void ResetWaves( );

        bool AddHarmonicElliottWave( ElliottWaveCycle waveCycle,
                                     ElliottWaveEnum waveName,
                                     WaveLabelPosition waveLabelPosition );

        bool AddHarmonicElliottWave( WaveInfo wave );

        bool AddHarmonicElliottWave( ElliottWaveCycle waveCycle,
                                     PooledList< ElliottWaveEnum > waveNames,
                                     WaveLabelPosition waveLabelPosition );

        bool AddSpecialHarmonicElliottWave( ElliottWaveCycle waveCycle,
                                            ElliottWaveEnum waveName,
                                            WaveLabelPosition waveLabelPosition );

        bool ReplaceWave( 
                          ElliottWaveCycle desiredCycle,
                          ElliottWaveEnum oldElliottWave,
                          ElliottWaveEnum newElliottWave );

        bool ReplaceTopWave( T selectedBarTime,
                             ElliottWaveCycle desiredCycle,
                             ElliottWaveEnum oldElliottWave,
                             ElliottWaveEnum newElliottWave );

        bool ReplaceBottomWave( T selectedBarTime,
                                ElliottWaveCycle desiredCycle,
                                ElliottWaveEnum oldElliottWave,
                                ElliottWaveEnum newElliottWave );

        PooledList< ElliottWaveEnum > GetWavesAtCycle( ElliottWaveCycle waveCycle );

        WaveInfo? GetHewPointInfoAtCycle( ElliottWaveCycle waveCycle );

        bool HasWaveAboveCycle( ElliottWaveCycle waveCycle );

        PooledList< WaveInfo > GetAllWaves( );

        PooledList< WaveInfo > GetAllWaves( ElliottWaveCycle minimumWaveCycle );

        void CycleUpAllWaves( );

        void CycleDownAllWaves( );

        int GetNumOfWaveC( );

        PooledList< WaveInfo > GetTopWaves( );

        PooledList< WaveInfo > GetTopWaves( ElliottWaveCycle minimumWaveCycle );

        PooledList<WaveInfo> GetTopWaves( bool filterAB, ElliottWaveCycle minimumWaveCycle );

        WaveInfo? GetHighestTopWave( );

        WaveInfo? GetLowestTopWave( );

        WaveInfo? GetHighestBottomWave( );

        WaveInfo? GetLowestBottomWave( );

        PooledList< WaveInfo > GetBottomWaves( );

        PooledList< WaveInfo > GetBottomWaves( ElliottWaveCycle minimumWaveCycle );

        PooledList<WaveInfo> GetBottomWaves( bool filterAB, ElliottWaveCycle minimumWaveCycle );

        WaveInfo? GetLastWave( );

        WaveInfo? GetFirstOppositeWave( );

        WaveInfo? GetLowestDegreeWaveInfo( );

        WaveInfo? GetFirstWave( );

        WaveInfo? GetSecondaryWave( );

        WaveInfo? GetLastHighestWaveDegree( );

        WaveInfo? GetFirstHighestWaveInfo( );

        bool HasWave( ElliottWaveCycle waveCycle );

        int GetWaveDegreeCount( );

        bool isValidPreviousWave( ElliottWaveCycle waveCycle,
                                  ElliottWaveEnum previousWave );

        bool IsWave5OrHigherDegree( ElliottWaveCycle cycle );

        bool HasWave0X_OrHigher( ElliottWaveCycle cycle );

        bool HasWave024XOrHigher( ElliottWaveCycle cycle );

        bool HasWave2OfDegree( ElliottWaveCycle cycle );

        bool HasWave1OfDegree( ElliottWaveCycle cycle );

        bool HasWave3OfDegree( ElliottWaveCycle cycle );

        bool HasWaveAOfDegree( ElliottWaveCycle cycle );

        bool IsWave12345OrHigherDegree( ElliottWaveCycle cycle );

        bool HasHigherDegreeWave( ElliottWaveCycle cycle );

        WaveInfo? GetWavePointInfo( ElliottWaveEnum beginningWaveName,
                                        ElliottWaveCycle waveCycle );

        void MergeWave( T waveBit );

        void MergeWavesList( PooledList< WaveInfo > topWaves );

        void MergeWave( WaveInfo wave );

        void MergeInLowerTimeFrameWaves( ref HewLong otherHew, ElliottWaveCycle minimumCycle );
        

        void RemoveWaves( ref HewLong hew );
        

        void CopyFrom( ref HewLong otherWave );
        

        WaveLabelPosition GetLabelPositionFromHew( );
    }
}
