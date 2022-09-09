using fx.Charting;
using fx.Collections;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Storages;
using StockSharp.Algo.Testing;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

#pragma warning disable CS0618

namespace FreemindAITrade.ViewModels
{
    public partial class BackTesterViewModel
    {
        DateTime _startDate = new DateTime( 2020, 10, 01 );
        public DateTime StartDate
        {
            get { return _startDate; }
            set { SetValue( ref _startDate, value ); }
        }

        DateTime _endDate = DateTime.UtcNow;
        public DateTime EndDate
        {
            get { return _endDate; }
            set { SetValue( ref _endDate, value ); }
        }

        CandleSeries _settingSeries = new CandleSeries()
        {
            CandleType = typeof( TimeFrameCandle ),
            Arg = TimeSpan.FromMinutes( 5 )
        };

        public CandleSeries SeriesSetting
        {
            get { return _settingSeries; }
            set { SetValue( ref _settingSeries, value ); }
        }

        private fxHistoricEmulationConnector _connector;

        private IStorageRegistry _storageRegistry;

        private CandleSeries _candlesSeries;



        //private IMarketDataStorage _candleStorage = null;        
        private bool _loadingSettings = false;
        private bool _doneDrawing = false;
        private CandleSeries _drawSeries = null;

        private IEnumerable<Candle> _drawCandles;
        private bool _isDrawingCandles = false;
        private readonly SubscriptionControlManager _subscriptionManager; // = new SubscriptionControlManager();
        private readonly PooledDictionary<CandleSeries, CandleSeriesData> _candles = new PooledDictionary<CandleSeries, CandleSeriesData>();
        private readonly PooledDictionary<IndicatorUI, IndicatorPair> _indicators = new PooledDictionary<IndicatorUI, IndicatorPair>();
        private readonly PooledDictionary<CandleSeries, Tuple<IndicatorUI, IndicatorPair>[ ]> _indicatorsBySeries = new PooledDictionary<CandleSeries, Tuple<IndicatorUI, IndicatorPair>[ ]>();
        public StorageFormats Format { get; set; } = StorageFormats.Binary;

        private CandleManager _candleManager;
        private CandlestickUI _candleUI;
        private DateTimeOffset _lastBarTime = DateTimeOffset.MinValue;

        //private bool _hasSubscribed = false;




        private IMarketDataDrive _drive;
        public IMarketDataDrive Drive
        {
            get
            {
                if ( _drive == null )
                {
                    _drive = _storageRegistry.DefaultDrive;
                }

                return _drive;
            }
            set
            {
                _drive = value;
            }
        }

        private IStudioCommandService _commandService;
    }
}
