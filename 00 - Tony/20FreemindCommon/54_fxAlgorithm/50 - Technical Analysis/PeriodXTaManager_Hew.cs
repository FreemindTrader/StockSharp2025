using fx.Collections;
using DevExpress.Mvvm;

using fx.Common;
using fx.Definitions;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace fx.Algorithm
{
    public partial class PeriodXTaManager
    {
        private ThreadSafeDictionary< ImpulsiveWaveKey, ImpulsiveWave > _impulsiveWavesInCorrection = new ThreadSafeDictionary< ImpulsiveWaveKey, ImpulsiveWave >( );
        private ThreadSafeDictionary< ImpulsiveWaveKey, ImpulsiveWave >   _impulsiveWavesInTrend = new ThreadSafeDictionary< ImpulsiveWaveKey, ImpulsiveWave >( );

        public void AddImplusiveWaves( WaveType waveType, PooledList< ImpulsiveWave > impulsiveWaves )
        {
            if( waveType == WaveType.Impulsive5Waves )
            {
                foreach( ImpulsiveWave impulsiveWave in impulsiveWaves )
                {
                    _impulsiveWavesInTrend.Add( impulsiveWave.WaveKey, impulsiveWave );
                }
            }
            else if( waveType == WaveType.Correction )
            {
                foreach( ImpulsiveWave impulsiveWave in impulsiveWaves )
                {
                    _impulsiveWavesInCorrection.Add( impulsiveWave.WaveKey, impulsiveWave );
                }
            }
        }

        public bool IsImpulsiveWave( DateTime barTime, out RangeEx< DateTime > timeBoundary )
        {
            timeBoundary = null;

            var timeBlocks = _impulsiveWavesInCorrection.Keys;

            foreach( var timeBlock in timeBlocks )
            {
                if( timeBlock.WaveRange.Contains( barTime ) )
                {
                    timeBoundary = timeBlock.WaveRange;
                    return true;
                }
            }

            return false;
        }
    }
}