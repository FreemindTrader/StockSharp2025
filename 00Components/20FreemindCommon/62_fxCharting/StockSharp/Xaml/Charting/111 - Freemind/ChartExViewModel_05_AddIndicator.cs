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
        [Command]
        public void Step01_AddIndicatorArea()
        {
            IndicatorAreaAddedEvent?.Invoke();
        }

        private void Step02_OnIndicatorAreaAddedEventInvoke()
        {
            ChartCount++;

            var indicatorArea = new ChartArea(ChartCount) { Title = "MACD", XAxisType = XAxisType };
            var indicatorSurface = new ScichartSurfaceMVVM(indicatorArea, true);

            indicatorArea.ViewModel = indicatorSurface;

            var timeZoneInfo = GetTimeZone();

            foreach (ChartAxis xAxis in indicatorArea.XAxises)
            {
                xAxis.TimeZone = timeZoneInfo;
            }

            AddArea(indicatorArea);

            if (_indicatorAreas.FindIndex(x => x == indicatorArea) == -1)
            {
                _indicatorAreas.Add(indicatorArea);
            }

            if (_indicatorSurfaces.FindIndex(x => x == indicatorSurface) == -1)
            {
                _indicatorSurfaces.Add(indicatorSurface);
            }

            ChartCount++;

            indicatorArea = new ChartArea(ChartCount) { Title = "RSI", XAxisType = XAxisType };
            indicatorSurface = new ScichartSurfaceMVVM(indicatorArea, true);



            indicatorArea.ViewModel = indicatorSurface;

            timeZoneInfo = GetTimeZone();

            foreach (ChartAxis xAxis in indicatorArea.XAxises)
            {
                xAxis.TimeZone = timeZoneInfo;
            }

            AddArea(indicatorArea);

            if (_indicatorAreas.FindIndex(x => x == indicatorArea) == -1)
            {
                _indicatorAreas.Add(indicatorArea);
            }

            if (_indicatorSurfaces.FindIndex(x => x == indicatorSurface) == -1)
            {
                _indicatorSurfaces.Add(indicatorSurface);
            }
        }


        public void Step05_ExecuteAddIndicatorsProgramatically(ChartArea chartArea, CandleSeries series)
        {
            CodingAddIndicatorsEvent?.Invoke(this, new AddIndicatorEventArgs(chartArea, series));
        }

        private void Step06_OnCodingAddIndicators(object sender, AddIndicatorEventArgs e)
        {
            var array = Elements.OfType<ChartCandleElement>().ToArray();
            var chartUi = e.ChartArea.Elements.OfType<ChartCandleElement>().Concat(array).FirstOrDefault();

            var tonyCandleSeries = GetSeries<CandleSeries>(chartUi);

            IIndicator indicator = null;

            IIndicator sma55indicator = null;

            if (chartUi == null)
            {
                MessageBoxResult canCloseDocument = MessageBoxService.Show(messageBoxText: LocalizedStrings.ChartAreaName, caption: "Add Indicator", button: MessageBoxButton.OK);

                return;
            }

            /* ----------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  Here I wanna draw the SMA 55
             * 
             * 
             * ----------------------------------------------------------------------------------------------------------------------------------------
             */
            var sma55indicatorUI = new ChartIndicatorElement();

            sma55indicatorUI.IndicatorPainter = new Sma55Painter();

            if (_period == TimeSpan.FromMinutes(1))
            {                
                sma55indicator = new SimpleMovingAverage { Length = 55 * 5 };
            }
            else
            {
                sma55indicator = new SimpleMovingAverage { Length = 55 };
            }

            if (sma55indicator != null)
            {
                Step07_AddIndicatorElement( _mainChartArea, sma55indicatorUI, tonyCandleSeries, sma55indicator);
            }

            var indicatorUI = new ChartIndicatorElement();

            indicatorUI.IndicatorPainter = null;

            if (e.ChartArea.Title == "MACD")
            {
                if (_period == TimeSpan.FromMinutes(1))
                {
                    var longEma   = new ExponentialMovingAverage { Length = 200 };
                    var shortEma  = new ExponentialMovingAverage { Length = 100 };
                    var signal    = new ExponentialMovingAverage { Length = 50 };
                    var macdSmall = new MovingAverageConvergenceDivergence(longEma, shortEma);

                    indicator     = new MovingAverageConvergenceDivergenceHistogram(macdSmall, signal);
                }
                else
                {
                    var longEma   = new ExponentialMovingAverage { Length = 40 };
                    var shortEma  = new ExponentialMovingAverage { Length = 20 };
                    var signal    = new ExponentialMovingAverage { Length = 10 };
                    var macdSmall = new MovingAverageConvergenceDivergence(longEma, shortEma);

                    indicator     = new MovingAverageConvergenceDivergenceHistogram(macdSmall, signal);
                }
            }
            else if (e.ChartArea.Title == "RSI")
            {
                indicator = new HewRsiComplex();
                indicatorUI.IndicatorPainter = new HewRsiPainter();
            }


            

            if (indicator != null)
            {
                Step07_AddIndicatorElement(e.ChartArea, indicatorUI, tonyCandleSeries, indicator);
            }
            
        }


        public void Step07_AddIndicatorElement(ChartArea area, ChartIndicatorElement indicatorUI, CandleSeries candleSeries, IIndicator indicator)
        {
            if (area == null)
            {
                throw new ArgumentNullException(nameof(area));
            }

            if (indicatorUI == null)
            {
                throw new ArgumentNullException(nameof(indicatorUI));
            }

            if (candleSeries == null)
            {
                throw new ArgumentNullException(nameof(candleSeries));
            }

            if (indicator == null)
            {
                throw new ArgumentNullException(nameof(indicator));
            }

            _uiDatasource.Add(indicatorUI, candleSeries);

            _indicators.Add(indicatorUI, indicator);

            if (!DisableIndicatorReset)
            {
                indicator.Reseted += () => OnIndicatorReset(indicatorUI, indicator);
            }

            if (StringHelper.IsEmpty(indicatorUI.FullTitle))
            {
                indicatorUI.FullTitle = indicator.ToString();
            }

            indicatorUI.CreateIndicatorPainter(IndicatorTypes, indicator);
            AddElement(area, indicatorUI);
        }
    }
}
