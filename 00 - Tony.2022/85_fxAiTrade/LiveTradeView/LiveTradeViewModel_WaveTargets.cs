using DevExpress.Mvvm;
using fx.Algorithm;
using fx.Bars;
using fx.Collections;
using fx.Definitions;
using System;
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

        public override void ClassicWave3Target()
        {

        }

        public override void ExtendedWave3Target()
        {

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
    }
}
