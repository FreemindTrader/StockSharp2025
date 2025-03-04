using fx.Bars;
using fx.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreemindAITrade.ViewModels
{
    public partial class LiveTradeViewModel
    {
        private void DataBarHistory_MonthlyPivotsUpdateEvent( fxHistoricBarsRepo provider, IList<SRlevel> newLevels )
        {
            if ( !_isNonVisual )
            {
                _chartVM.ClearMonthlyPivots();

                if ( newLevels.Count > 0 )
                {
                    _chartVM.AddMonthlyPivots( SelectedSecurity, newLevels, ref _bars.NullBar );

                }
            }
        }

        private void DataBarHistory_WeeklyPivotsUpdateEvent( fxHistoricBarsRepo provider, IList<SRlevel> newLevels )
        {
            if ( !_isNonVisual )
            {
                _chartVM.ClearWeeklyPivots();

                if ( newLevels.Count > 0 )
                {
                    _chartVM.AddWeeklyPivots( SelectedSecurity, newLevels, ref _bars.NullBar );


                }
            }
        }



        private void DataBarHistory_DailyPivotsUpdateEvent( fxHistoricBarsRepo provider, IList<SRlevel> newLevels )
        {
            if ( !_isNonVisual )
            {
                _chartVM.ClearDailyPivots();

                if ( newLevels.Count > 0 )
                {
                    _chartVM.AddDailyPivots( SelectedSecurity, newLevels, ref _bars.NullBar );
                }
            }
        }
    }
}