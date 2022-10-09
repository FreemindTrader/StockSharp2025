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

        public void ShrinkWave3Target()
        {
            ShowSmallTradingEvent = true;

            _selectedViewModel.ShrinkWave3Target();

            _selectedViewModel.ChartVM.CheckAndShowFibonacci();

            _selectedViewModel.Refresh();            
        }

        public void ExtendedWave3Target()
        {
            ShowSmallTradingEvent = true;

            _selectedViewModel.ExtendedWave3Target();

            _selectedViewModel.ChartVM.CheckAndShowFibonacci();

            _selectedViewModel.Refresh();            
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
