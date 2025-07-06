using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using Ecng.Common;
using Ecng.Collections;
using Ecng.ComponentModel;
using Ecng.Configuration;
using Ecng.Serialization;
using MoreLinq;
using SciChart.Charting.Visuals.TradeChart;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic;
using fx.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.Mvvm.UI;
using StockSharp.Xaml.Charting.Definitions;
using System.Windows;
using DevExpress.Xpf.Grid;
using SciChart.Charting.Visuals;
using fx.Common;
using StockSharp.Xaml;
using StockSharp.Messages;
using fx.Indicators;
using StockSharp.Xaml.Charting.IndicatorPainters;


#pragma warning disable 067

namespace StockSharp.Xaml.Charting
{
    public partial class ChartExViewModel
    {
        /* -------------------------------------------------------------------------------------------------------------------------------------------
         * 
         *  The following is DevExpress's implementation of Commands which is more cleaner.
         *  
         *  Normal Coding
         *          
         *      public DelegateCommand AddAreaCommand { get; private set; }
         *      
         *      AddAreaCommand = new DelegateCommand ( ExecuteAddAreaCommand, CanExecuteAddAreaCommand );

                public void ExecuteAddAreaCommand( )
                {
                    AreaAddedEvent?.Invoke( );
                }

                public bool CanExecuteAddAreaCommand( )
                {
                    return IsInteracted;
                }       
         * 
         * ------------------------------------------------------------------------------------------------------------------------------------------- */

        [Command]
        public void Step01_AddChartArea()
        {
            AreaAddedEvent?.Invoke();
        }


        private void Step02_OnChartAreaAddedEventInvoke()
        {
            _mainChartArea = new ChartArea() { Title = LocalizedStrings.Panel + " " + (ChartAreas.Count + 1), XAxisType = XAxisType };
            _drawSurface = new ScichartSurfaceMVVM(_mainChartArea, true);

            _mainChartArea.ChartSurfaceViewModel = _drawSurface;

            var timeZoneInfo = GetTimeZone();

            foreach (ChartAxis xAxis in _mainChartArea.XAxises)
            {
                xAxis.TimeZone = timeZoneInfo;
            }

            AddArea(_mainChartArea);
        }

        public void Step05_ExecuteAddCandlesProgramatically(ChartArea chartArea, CandleSeries series)
        {
            CodingAddCandlesEvent?.Invoke(this, new AddCandlesEventArgs(chartArea, series));
        }

        public void Step05_ExecuteAddCandlesProgramatically(ChartArea chartArea, CandleSeries series, int fifoCapcity)
        {
            CodingAddCandlesEvent?.Invoke(this, new AddCandlesEventArgs(chartArea, series, fifoCapcity));
        }

        private void Step06_OnCodingAddCandles(object sender, AddCandlesEventArgs e)
        {
            if (e.UseFifo)
            {
                _candleStickUI = new ChartCandleElement() { FifoCapacity = e.FifoCapcity };
            }
            else
            {
                _candleStickUI = new ChartCandleElement();
            }


            _candleStickUI.ShowAxisMarker = false;

            _candleSeries = e.CandleSerie.Clone();
            _period = (TimeSpan)_candleSeries.Arg;
            _candleSeries.Security = null;

            AddElement(e.ChartArea, _candleStickUI, e.CandleSerie);

            e.CandleSerie.PropertyChanged += new PropertyChangedEventHandler(OnCandleSeriesPropertyChanged);
        }

    }
}
