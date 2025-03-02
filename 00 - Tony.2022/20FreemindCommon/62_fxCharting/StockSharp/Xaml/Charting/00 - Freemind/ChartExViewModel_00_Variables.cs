﻿using DevExpress.Mvvm;
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
using fx.Charting;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wintellect.PowerCollections;
using DevExpress.Mvvm.UI;
using fx.Charting.Definitions;
using System.Windows;
using DevExpress.Xpf.Grid;
using SciChart.Charting.Visuals;
using fx.Common;
using StockSharp.Xaml;



#pragma warning disable 067

namespace fx.Charting
{
    public partial class ChartExViewModel 
    {
        #region Variables

        private readonly SynchronizedDictionary<IndicatorUI, IIndicator> _indicators   = new SynchronizedDictionary<IndicatorUI, IIndicator>( );
        private readonly SynchronizedDictionary<IfxChartElement, object> _uiDatasource = new SynchronizedDictionary<IfxChartElement, object>( );
        private readonly CachedSynchronizedList<IfxChartElement>         _uiList       = new CachedSynchronizedList<IfxChartElement>( );

        private CandleSeries                                             _candleSeries;
        
        private CandlestickUI                                            _candleStickUI;
        
        private ScichartSurfaceMVVM _drawSurface;

        private ChartArea _mainChartArea;



        public IScichartSurfaceVM SurfaceVM
        {
            get
            {
                return _drawSurface;
            }
        }


        private CandleSeries _candleSeriesRebuilt;
        public CandleSeries CandleSeriesRebuilt
        {
            get
            {
                return _candleSeriesRebuilt;
            }

            set
            {
                _candleSeriesRebuilt = value;
            }
        }



        private static int staticChartCount;
        private readonly int _instanceCount = ++staticChartCount;

        private bool _isActive;
        public bool IsActive
        {
            get
            {
                return _isActive;
            }

            set
            {
                _isActive = value;
            }
        }

        public Security SelectedSecurity
        {
            get;
            set;

        }

        #endregion
    }
}
