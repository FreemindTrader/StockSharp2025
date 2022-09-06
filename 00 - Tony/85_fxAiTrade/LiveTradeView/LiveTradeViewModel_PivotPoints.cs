using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Docking;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Configuration;
using Ecng.Serialization;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Candles.Compression;
using StockSharp.Algo.Indicators;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using fx.Definitions;
using fx.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Ecng.ComponentModel;
using fx.Algorithm;
using fx.Bars;

namespace FreemindAITrade.ViewModels
{
    public partial class LiveTradeViewModel 
    {
        private void DataBarHistory_MonthlyPivotsUpdateEvent( fxHistoricBarsRepo provider, IList<SRlevel> newLevels )
        {
            if ( ! _isNonVisual )
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
                _chartVM.ClearDailyPivots( );

                if ( newLevels.Count > 0 )
                {
                    _chartVM.AddDailyPivots( SelectedSecurity, newLevels, ref _bars.NullBar );
                }
            }
        }        
    }
}