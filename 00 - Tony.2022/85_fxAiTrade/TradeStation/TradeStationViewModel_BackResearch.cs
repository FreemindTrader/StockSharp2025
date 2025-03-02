using fx.Algorithm;
using StockSharp.Logging;
using System;
using System.Linq;

namespace FreemindAITrade.ViewModels
{
    public partial class TradeStationViewModel : BaseLogReceiver, IMutltiTimeFrameSessionDataRepo
    {
        public virtual bool ResearchPastTA { get; set; }
        protected void OnResearchPastTAChanging( bool newBool )
        {
            _selectedViewModel.ChartVM.ShowSmallTradingEvent( newBool );

            _selectedViewModel.ChartVM.ResearchPastTA( newBool );

            _selectedViewModel.Refresh();
        }
    }
}