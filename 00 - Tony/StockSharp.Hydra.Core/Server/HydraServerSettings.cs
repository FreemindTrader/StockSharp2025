using Ecng.Common;
using Ecng.Serialization;
using StockSharp.Algo.Storages;
using StockSharp.Localization;
using StockSharp.Server.Core;
using StockSharp.Server.Fix;
using System;
using System.ComponentModel.DataAnnotations;

namespace StockSharp.Hydra.Core.Server
{
    /// <summary>Hydra server settings.</summary>
    [DisplayNameLoc( "Str2211" )]
    public class HydraServerSettings : IPersistable
    {
        private int _maxSecurityCount = 1000;
        private int _candleHistoryMaxDays = 180;
        private int _tickHistoryMaxDays = 5;
        private int _transactionsHistoryMaxDays = 1;
        private int _orderBookHistoryMaxDays;
        private int _orderLogHistoryMaxDays;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Hydra.Core.Server.HydraServerSettings" />.
        /// </summary>
        public HydraServerSettings()
        {
            FixSession marketDataSession     = ServerSettings.MarketDataSession;
            FixSession transactionSession    = ServerSettings.TransactionSession;
            transactionSession.MaxReadBytes  = marketDataSession.MaxReadBytes = 1048576;
            transactionSession.MaxWriteBytes = marketDataSession.MaxWriteBytes = 10485760;
            marketDataSession.Address        = RemoteMarketDataDrive.DefaultAddress;
            marketDataSession.TargetCompId   = RemoteMarketDataDrive.DefaultTargetCompId;
            marketDataSession.IsEnabled      = true;
            transactionSession.IsEnabled     = false;
        }

        /// <summary>Is server mode enabled.</summary>
        [Display( Description = "HydraFixServer", GroupName = "DataServer", Name = "FixServer", Order = 1, ResourceType = typeof( LocalizedStrings ) )]
        public bool IsFixServer { get; set; }

        /// <summary>FIX market data session settings.</summary>
        [Display( Description = "MarketDataSession", GroupName = "DataServer", Name = "FixServer", Order = 2, ResourceType = typeof( LocalizedStrings ) )]
        public FixServerSettings ServerSettings { get; set; } = new FixServerSettings();

        /// <summary>Authorization type.</summary>
        [Display( Description = "Str2216", GroupName = "DataServer", Name = "Authorization", Order = 3, ResourceType = typeof( LocalizedStrings ) )]
        public AuthorizationModes Authorization { get; set; }

        /// <summary>Max securities count per request.</summary>
        [Display( Description = "Str2215", GroupName = "DataServer", Name = "Str2214", Order = 4, ResourceType = typeof( LocalizedStrings ) )]
        public int MaxSecurityCount
        {
            get
            {
                return _maxSecurityCount;
            }
            set
            {
                if ( value < 1 )
                    throw new ArgumentOutOfRangeException( nameof( value ) );
                _maxSecurityCount = value;
            }
        }

        /// <summary>
        /// The maximum number of days available to download the <see cref="P:StockSharp.Messages.DataType.IsCandles" /> data history.
        /// </summary>
        [Display( Description = "CandleMaxDaysDescription", GroupName = "DataServer", Name = "CandleMaxDays", Order = 5, ResourceType = typeof( LocalizedStrings ) )]
        public int CandleHistoryMaxDays
        {
            get
            {
                return _candleHistoryMaxDays;
            }
            set
            {
                if ( value < 0 )
                    throw new ArgumentOutOfRangeException( nameof( value ) );
                _candleHistoryMaxDays = value;
            }
        }

        /// <summary>
        /// The maximum number of days available to download the <see cref="P:StockSharp.Messages.DataType.Ticks" /> data history.
        /// </summary>
        [Display( Description = "TickMaxDaysDescription", GroupName = "DataServer", Name = "TickMaxDays", Order = 6, ResourceType = typeof( LocalizedStrings ) )]
        public int TickHistoryMaxDays
        {
            get
            {
                return _tickHistoryMaxDays;
            }
            set
            {
                if ( value < 0 )
                    throw new ArgumentOutOfRangeException( nameof( value ) );
                _tickHistoryMaxDays = value;
            }
        }

        /// <summary>
        /// The maximum number of days available to download the <see cref="P:StockSharp.Messages.DataType.MarketDepth" /> data history.
        /// </summary>
        [Display( Description = "OrderBookMaxDaysDescription", GroupName = "DataServer", Name = "OrderBookMaxDays", Order = 7, ResourceType = typeof( LocalizedStrings ) )]
        public int OrderBookHistoryMaxDays
        {
            get
            {
                return _orderBookHistoryMaxDays;
            }
            set
            {
                if ( value < 0 )
                    throw new ArgumentOutOfRangeException( nameof( value ) );
                _orderBookHistoryMaxDays = value;
            }
        }

        /// <summary>
        /// The maximum number of days available to download the <see cref="P:StockSharp.Messages.DataType.OrderLog" /> data history.
        /// </summary>
        [Display( Description = "OrderLogMaxDaysDescription", GroupName = "DataServer", Name = "OrderLogMaxDays", Order = 8, ResourceType = typeof( LocalizedStrings ) )]
        public int OrderLogHistoryMaxDays
        {
            get
            {
                return _orderLogHistoryMaxDays;
            }
            set
            {
                if ( value < 0 )
                    throw new ArgumentOutOfRangeException( nameof( value ) );
                _orderLogHistoryMaxDays = value;
            }
        }

        /// <summary>
        /// The maximum number of days available to download the <see cref="P:StockSharp.Messages.DataType.Transactions" /> data history.
        /// </summary>
        [Display( Description = "TransactionsMaxDaysDescription", GroupName = "DataServer", Name = "TransactionsMaxDays", Order = 9, ResourceType = typeof( LocalizedStrings ) )]
        public int TransactionsHistoryMaxDays
        {
            get
            {
                return _transactionsHistoryMaxDays;
            }
            set
            {
                if ( value < 0 )
                    throw new ArgumentOutOfRangeException( nameof( value ) );
                _transactionsHistoryMaxDays = value;
            }
        }

        /// <summary>Is trading simulator enabled.</summary>
        [Display( Description = "Str1209", GroupName = "DataServer", Name = "Str1209", Order = 12, ResourceType = typeof( LocalizedStrings ) )]
        public bool SimulatorEnabled { get; set; }

        /// <summary>Translates on client only mapped securities.</summary>
        [Display( Description = "OnlyMappedSecurities", GroupName = "DataServer", Name = "SecurityMapping", Order = 13, ResourceType = typeof( LocalizedStrings ) )]
        public bool OnlyMappedSecurities { get; set; }

        /// <summary>Load settings.</summary>
        /// <param name="storage">Settings storage.</param>
        public void Load( SettingsStorage storage )
        {
            IsFixServer = storage.GetValue( "IsFixServer", false );
            Authorization = storage.GetValue( "Authorization", AuthorizationModes.Anonymous );
            if ( storage.ContainsKey( "ServerSettings" ) )
            {
                ServerSettings.ForceLoad( storage.GetValue<SettingsStorage>( "ServerSettings", null ) );
            }
            else
            {
                SettingsStorage storage1 = storage.GetValue<SettingsStorage>( "FixMarketDataSession", null );
                if ( storage1 != null )
                    ServerSettings.MarketDataSession.Load( storage1 );
            }
            MaxSecurityCount = storage.GetValue( "MaxSecurityCount", MaxSecurityCount );
            CandleHistoryMaxDays = storage.GetValue( "CandleHistoryMaxDays", CandleHistoryMaxDays );
            TickHistoryMaxDays = storage.GetValue( "TickHistoryMaxDays", TickHistoryMaxDays );
            OrderBookHistoryMaxDays = storage.GetValue( "OrderBookHistoryMaxDays", OrderBookHistoryMaxDays );
            OrderLogHistoryMaxDays = storage.GetValue( "OrderLogHistoryMaxDays", OrderLogHistoryMaxDays );
            TransactionsHistoryMaxDays = storage.GetValue( "TransactionsHistoryMaxDays", TransactionsHistoryMaxDays );
            SimulatorEnabled = storage.GetValue( "SimulatorEnabled", SimulatorEnabled );
            OnlyMappedSecurities = storage.GetValue( "OnlyMappedSecurities", OnlyMappedSecurities );
        }

        /// <summary>Save settings.</summary>
        /// <param name="storage">Settings storage.</param>
        public void Save( SettingsStorage storage )
        {
            storage.SetValue( "IsFixServer", IsFixServer );
            storage.SetValue( "Authorization", Authorization.To<string>() );
            storage.SetValue( "ServerSettings", ServerSettings.Save() );
            storage.SetValue( "MaxSecurityCount", MaxSecurityCount );
            storage.SetValue( "CandleHistoryMaxDays", CandleHistoryMaxDays );
            storage.SetValue( "TickHistoryMaxDays", TickHistoryMaxDays );
            storage.SetValue( "OrderBookHistoryMaxDays", OrderBookHistoryMaxDays );
            storage.SetValue( "OrderLogHistoryMaxDays", OrderLogHistoryMaxDays );
            storage.SetValue( "TransactionsHistoryMaxDays", TransactionsHistoryMaxDays );
            storage.SetValue( "SimulatorEnabled", SimulatorEnabled );
            storage.SetValue( "OnlyMappedSecurities", OnlyMappedSecurities );
        }
    }
}
