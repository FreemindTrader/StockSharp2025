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
using fx.Charting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using fx.Definitions;
using fx.Common;
using Ecng.ComponentModel;
using fx.Database;
using fx.Database.Common.DataModel;
using fx.Database.ForexDatabarsDataModel;
using fx.Algorithm;
using System.Windows.Media;
using StockSharp.Xaml;
using fx.Indicators;
using System.ComponentModel;
using StockSharp.Studio.Core.Configuration;
using Ecng.Xaml;
using StockSharp.Algo.Testing;
using StockSharp.Studio.Core;
using Disruptor;
using fx.Collections;

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

        CandleSeries _settingSeries = new CandleSeries( )
        {
            CandleType = typeof(TimeFrameCandle ),
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
        private bool _doneDrawing         = false;
        private CandleSeries _drawSeries = null;
        
        private IEnumerable<Candle> _drawCandles;
        private bool _isDrawingCandles    = false;
        private readonly SubscriptionControlManager _subscriptionManager; // = new SubscriptionControlManager();
        private readonly PooledDictionary<CandleSeries, CandleSeriesData> _candles = new PooledDictionary<CandleSeries, CandleSeriesData>();
        private readonly PooledDictionary<IndicatorUI, IndicatorPair> _indicators = new PooledDictionary<IndicatorUI, IndicatorPair>();
        private readonly PooledDictionary<CandleSeries, Tuple<IndicatorUI, IndicatorPair>[]> _indicatorsBySeries = new PooledDictionary<CandleSeries, Tuple<IndicatorUI, IndicatorPair>[]>();
        public StorageFormats Format { get; set; } = StorageFormats.Binary;

        private CandleManager _candleManager;
        private CandlestickUI _candleUI;
        private DateTimeOffset _lastBarTime         = DateTimeOffset.MinValue;

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
