using DevExpress.Mvvm;
using fx.Algorithm;
using fx.Bars;
using fx.Collections;
using fx.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace FreemindAITrade.ViewModels
{
    public partial class LiveTradeViewModel
    {
        public override void AnalysisWave()
        {
            long selectedBarTime = _chartVM.SelectedCandleBarTime;

            if ( selectedBarTime > -1 )
            {
                ref SBar bar = ref _bars.GetBarByTime( selectedBarTime );

                if ( bar != SBar.EmptySBar )
                {
                    _freemindIndicator.DoInitialZigZagAnalysis( bar.Index );
                }
            }
        }

        public override void ShrinkWave3Target()
        {
            if ( null == _hews )
            {
                var aa = SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( SelectedSecurity );

                if ( aa != null )
                {
                    _hews = ( HewManager )aa.HewManager;
                }
                else
                {
                    return;
                }
            }

            _hews.ShrinkWave3Target( );

            Refresh( );
        }

        public override void ExtendedWave3Target()
        {
            if ( null == _hews )
            {
                var aa = SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( SelectedSecurity );

                if ( aa != null )
                {
                    _hews = ( HewManager )aa.HewManager;
                }
                else
                {
                    return;
                }
            }

            _hews.ExtendedWave3Target();

            Refresh();
        }

        public override void SuperExtendedWave3Target()
        {

        }

        public override void WaveCTarget()
        {

        }
        public override void Wave4Target()
        {

        }

        

        private DictionarySlim< long, HewLong> _lockedWaves = new DictionarySlim< long, HewLong>( );

        public override void AnalysisLockedFibs()
        {
            long selectedBarTime = _chartVM.SelectedCandleBarTime;

            if ( selectedBarTime > -1 )
            {
                ref SBar bar = ref _bars.GetBarByTime( selectedBarTime );

                if ( bar != SBar.EmptySBar )
                {
                    if ( bar.HasElliottWave )
                    {
                        ref var hew = ref bar.GetWaveFromScenario( _waveScenarioNumber );

                        if ( hew == AdvBarInfo.EmptyHew )
                        {
                            return;
                        }

                        _lockedWaves.GetOrAddValueRef( bar.LinuxTime ) = hew;

                        var waves = hew.GetHighestDegreeWaves( );

                        foreach ( var wave in waves )
                        {
                            var waveName = wave.WaveName;
                            var wDegree = wave.WaveCycle;

                            switch ( waveName )
                            {
                                
                                case ElliottWaveEnum.Wave1:                                
                                case ElliottWaveEnum.Wave1C:
                                
                                break;

                                case ElliottWaveEnum.Wave2:
                                {
                                    _hews.AnalyzeWaveTargetsFromWave2( _waveScenarioNumber, ResponsibleTF, _bars, selectedBarTime );
                                }
                                break;

                                case ElliottWaveEnum.Wave3:                                
                                case ElliottWaveEnum.Wave3C:
                                {

                                }
                                break;

                                case ElliottWaveEnum.Wave4:
                                {

                                }
                                break;
                                
                                case ElliottWaveEnum.Wave5:                                
                                case ElliottWaveEnum.Wave5C:
                                {

                                }
                                break;
                                
                                case ElliottWaveEnum.WaveTA:
                                {

                                }
                                break;

                                case ElliottWaveEnum.WaveTB:
                                {

                                }
                                break;

                                case ElliottWaveEnum.WaveTC:
                                {

                                }
                                break;

                                case ElliottWaveEnum.WaveTD:
                                
                                break;
                                case ElliottWaveEnum.WaveTE:
                                
                                break;
                                case ElliottWaveEnum.WaveEFA:
                                
                                break;
                                case ElliottWaveEnum.WaveEFB:
                                
                                break;
                                case ElliottWaveEnum.WaveEFC:
                                
                                break;
                                case ElliottWaveEnum.WaveA:
                                
                                break;
                                case ElliottWaveEnum.WaveB:
                                
                                break;
                                case ElliottWaveEnum.WaveC:
                                
                                break;
                                case ElliottWaveEnum.WaveX:
                                
                                break;
                                case ElliottWaveEnum.WaveY:
                                
                                break;
                                case ElliottWaveEnum.WaveZ:
                                
                                break;
                                case ElliottWaveEnum.WaveW:
                                
                                break;
                                case ElliottWaveEnum.Wave1B:
                                
                                break;
                                case ElliottWaveEnum.Wave3B:
                                
                                break;
                                case ElliottWaveEnum.Wave5B:
                                
                                break;                                
                            }                            
                        }
                    }
                }
            }
        }
    }
}
