using System;
using fx.Definitions;
using SciChart.Charting.Model.DataSeries;
using fx.Collections;
using Ecng.Configuration;
using fx.Bars;

#pragma warning disable 067

namespace fx.Bars
{
    public partial struct SBar
    {
        public ref HewLong GetWaveFromScenario( int waveScenarioNo )
        {
            AdvBarInfo info = null;

            if ( _parent.HasExtraInfo( _barIndex, out info ) )
            {
                ref var hew = ref info.GetWaveFromScenario( waveScenarioNo );

                return ref hew;
            }

            return ref AdvBarInfo.EmptyHew;
        }

        public ref WavePriceTimeInfo GetPriceTimeFromScenario( int waveScenarioNo )
        {
            AdvBarInfo info = null;

            if ( _parent.HasExtraInfo( _barIndex, out info ) )
            {
                ref WavePriceTimeInfo waveInfo = ref info.GetPriceTimeFromScenario( waveScenarioNo );

                return ref waveInfo;
            }

            return ref AdvBarInfo.EmptyFibR;
        }

        

        public void AddWave( int waveScenarioNo, ref HewLong copy )
        {            
            if ( ! _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
            {
                info = _parent.GetOrAllocate( _barIndex );
            }            

            ref var hew = ref info.GetWaveFromScenario( waveScenarioNo );

            hew.AddHarmonicElliottWave( ref copy );

            WaveDirty = WaveDirtyEnum.Add;

            Features.HasElliottWave = info.HasWaves;
        }

        public void AddPriceTimeInfo( int waveScenarioNo, ref WavePriceTimeInfo copy )
        {            
            if ( ! _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
            {
                info = _parent.GetOrAllocate( _barIndex );                
            }            

            ref var waveInfo = ref info.GetPriceTimeFromScenario( waveScenarioNo );

            waveInfo = copy;

            WaveDirty = WaveDirtyEnum.Add;            
        }


        


        public void UpdateWave( int waveScenarioNo, ref HewLong wave )
        {
            if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
            {
                ref var hew = ref info.GetWaveFromScenario( waveScenarioNo );

                hew.CopyFrom( ref wave );

                WaveDirty = WaveDirtyEnum.Change;

                Features.HasElliottWave = info.HasWaves;
            }
        }

        

        public int WaveScenariosCount
        {
            get
            {
                if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
                {
                    return info.WaveScenariosCount;                    
                }

                return 0;
            }
        }

        public PooledList<WaveInfo> GetAllWaves( int waveScenarioNo, bool isFilterAB )
        {
            if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
            {
                ref var hew = ref info.GetWaveFromScenario( waveScenarioNo );

                if ( hew != AdvBarInfo.EmptyHew )
                {
                    var wavesToShow = FinancialHelper.GetMinimumWavesToShow( SymbolEx.Period );

                    return hew.GetAllWaves( wavesToShow );
                }
            }

            return null;
        }

        public PooledList<WaveInfo> GetAllWaves( int waveScenarioNo, bool isFilterAB, ElliottWaveCycle minimumToShow )
        {
            if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
            {
                ref var hew = ref info.GetWaveFromScenario( waveScenarioNo );

                if ( hew != AdvBarInfo.EmptyHew )
                {                    
                    return hew.GetAllWaves( isFilterAB, minimumToShow );
                }
            }

            return null;
        }

        public PooledList<WaveInfo> GetTopWaves( int waveScenarioNo, bool isFilterAB, ElliottWaveCycle minimumToShow )
        {
            if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
            {
                ref var hew = ref info.GetWaveFromScenario( waveScenarioNo );

                if ( hew != AdvBarInfo.EmptyHew )
                {                    
                    return hew.GetTopWaves( isFilterAB, minimumToShow );
                }                
            }            

            return null;
        }

        public PooledList<WaveInfo> GetTopWaves( int waveScenarioNo, bool isFilterAB )
        {
            if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
            {
                ref var hew = ref info.GetWaveFromScenario( waveScenarioNo );

                if ( hew != AdvBarInfo.EmptyHew )
                {
                    var wavesToShow = FinancialHelper.GetMinimumWavesToShow( SymbolEx.Period );

                    return hew.GetTopWaves( isFilterAB, wavesToShow );
                }
            }

            return null;
        }

        public PooledList<WaveInfo> GetBottomWaves( int waveScenarioNo, bool isFilterAB )
        {
            if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
            {
                ref var hew = ref info.GetWaveFromScenario( waveScenarioNo );

                if ( hew != AdvBarInfo.EmptyHew )
                {
                    var wavesToShow = FinancialHelper.GetMinimumWavesToShow( SymbolEx.Period );

                    return hew.GetBottomWaves( isFilterAB, wavesToShow );
                }
            }

            return null;
        }

        public PooledList<WaveInfo> GetBottomWaves( int waveScenarioNo, bool isFilterAB, ElliottWaveCycle minimumToShow )
        {
            if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
            {
                ref var hew = ref info.GetWaveFromScenario( waveScenarioNo );

                if ( hew != AdvBarInfo.EmptyHew )
                {
                    return hew.GetBottomWaves( isFilterAB, minimumToShow );
                }
            }

            return null;
        }



        public void BranchWaves( int waveScenarioNo )
        {
            if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
            {
                ref var hew   = ref info.GetWaveFromScenario( waveScenarioNo );
                ref var branchWave = ref info.GetNextWaveFromScenario( waveScenarioNo );

                if ( hew.Count > 0 )
                {
                    //baseWave.ResetWaves();

                    branchWave.CopyFrom( ref hew );
                }                
            }
        }


        public void ConfirmWaves( int waveScenarioNo )
        {
            if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
            {
                ref var hew   = ref info.GetWaveFromScenario( waveScenarioNo );
                ref var branchWave = ref info.GetNextWaveFromScenario( waveScenarioNo );

                if ( hew.Count > 0 )
                {
                    branchWave.CopyFrom( ref hew );

                    hew.ResetWaves();

                    branchWave.CopyFrom( ref hew );
                }
            }            
        }

        public void RemoveWavesFromDatabar( int waveScenarioNo )
        {
            if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
            {
                ref var hew = ref info.GetWaveFromScenario( waveScenarioNo );

                hew.ResetWaves();

                IsSpecialBar = false;

                WaveDirty = WaveDirtyEnum.DeleteAll;

                Features.HasElliottWave = info.HasWaves;
            }
        }

        
        public void RemoveWavesFromDatabar( int waveScenarioNo, HewLong otherHew )
        {
            if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
            {
                info.RemoveWavesFromDatabar( waveScenarioNo, otherHew );

                WaveDirty = WaveDirtyEnum.DeleteSingle;

                Features.HasElliottWave = info.HasWaves;
            }
        }

        public bool RemoveMatchedWavesFromDatabar( int waveScenarioNo, HewLong otherHew )
        {
            if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
            {
                info.RemoveMatchedWavesFromDatabar( waveScenarioNo, otherHew );

                WaveDirty = WaveDirtyEnum.DeleteSingle;

                Features.HasElliottWave = info.HasWaves;

                return true;
            }

            return false;
        }

        

        public string GetPriceTimeInfoString( int waveScenarioNo )
        {
            if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
            {
                return info.GetPriceTimeInfoString( waveScenarioNo, BarIndex );
            }

            return null;
        }        
    }
}
