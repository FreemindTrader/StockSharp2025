using fx.Algorithm;
using fx.Definitions;
using StockSharp.BusinessEntities;
using StockSharp.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreemindAITrade.ViewModels
{
    public partial class TradeStationViewModel : BaseLogReceiver, IMutltiTimeFrameSessionDataRepo
    {
        public void AnalyzeWaveTarget()
        {
            _selectedViewModel.AnalyzeWaveTarget();
        }

        public void ClassicWave3Target()
        {
            ShowSmallTradingEvent = true;

            _selectedViewModel.ClassicWave3Target();
        }

        public void ExtendedWave3Target()
        {
            ShowSmallTradingEvent = true;

            _selectedViewModel.ExtendedWave3Target();
        }

        public void SuperExtendedWave3Target()
        {
            ShowSmallTradingEvent = true;

            _selectedViewModel.SuperExtendedWave3Target();
        }

        public void WaveCTarget()
        {
            ShowSmallTradingEvent = true;

            _selectedViewModel.WaveCTarget();
        }

        public void Wave4Target()
        {
            ShowSmallTradingEvent = true;

            _selectedViewModel.Wave4Target();
        }

    }
}
