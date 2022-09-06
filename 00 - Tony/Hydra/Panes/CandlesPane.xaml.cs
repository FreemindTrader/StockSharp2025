using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Configuration;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Candles.Compression;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Hydra.Controls;
using StockSharp.Hydra.Core;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using StockSharp.Xaml;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace StockSharp.Hydra.Panes
{
    public partial class CandlesPane : DataPane, IComponentConnector
    {

        public CandlesPane()
        {
            InitializeComponent();
            CandleSettings.Settings = new CandleSeries()
            {
                CandleType = typeof( TimeFrameCandle ),
                Arg = TimeSpan.FromMinutes( 1.0 )
            };
            Init( ExportBtn, MainGrid, SelectSecurityBtn, new Func<SecurityId, DateTime?, DateTime?, bool, IEnumerable>( GetCandles ) );
            SetTitlePrefix( LocalizedStrings.Candles );
        }

        protected override DataType DataType
        {
            get
            {
                return CandleSettings.Settings.ToDataType();
            }
        }

        public CandleSeries CandleSeries
        {
            get
            {
                return CandleSettings.Settings;
            }
            set
            {
                if ( value == null )
                    return;
                CandleSettings.Settings = new CandleSeries()
                {
                    CandleType = value.CandleType,
                    Security = CandleSeries?.Security,
                    Arg = value.Arg
                };
            }
        }

        private void BuildFrom_OnSelectionChanged( object sender, RoutedEventArgs e )
        {
            BuildTypes buildType = BuildType;
            if ( BuildFromExtra != null )
            {
                BuildFromExtra.IsEnabled = buildType == BuildTypes.OrderLog || buildType == BuildTypes.Depths || buildType == BuildTypes.Level1;
                if ( BuildFromExtra.IsEnabled )
                {
                    LastTradeCbItem.IsEnabled = buildType != BuildTypes.Depths;
                    if ( !LastTradeCbItem.IsEnabled && LastTradeCbItem.IsSelected )
                        BuildFromExtra.SelectedIndex = 3;
                }
            }
            if ( VolumeProfile != null && BuildFromExtra != null )
                VolumeProfile.IsEnabled = buildType == BuildTypes.Ticks || buildType == BuildTypes.OrderLog || ( buildType == BuildTypes.Level1 || buildType == BuildTypes.Depths ) || BuildFromExtra.IsEnabled;
            ExportBtn?.SetTypeEnabled( ExportTypes.SaveBuild, ( uint )BuildType > 0U );
        }

        private BuildTypes BuildType
        {
            get
            {
                return ( BuildTypes )BuildFrom.SelectedIndex;
            }
        }

        private CandleSeries CreateCandleSeries( Security security )
        {
            CandleSeries candleSeries = new CandleSeries( CandleSeries.CandleType, security, CandleSeries.Arg );
            bool? isChecked1 = VolumeProfile.IsChecked;
            bool flag1 = true;
            candleSeries.IsCalcVolumeProfile = isChecked1.GetValueOrDefault() == flag1 & isChecked1.HasValue;
            bool? isChecked2 = IsRegularTradingHours.IsChecked;
            bool flag2 = true;
            candleSeries.IsRegularTradingHours = isChecked2.GetValueOrDefault() == flag2 & isChecked2.HasValue;
            return candleSeries;
        }

        private static Level1Fields Convert( ExtraBuildTypes type )
        {
            switch ( type )
            {
                case ExtraBuildTypes.Bid:
                    return Level1Fields.BestBidPrice;
                case ExtraBuildTypes.Ask:
                    return Level1Fields.BestAskPrice;
                case ExtraBuildTypes.Middle:
                    return Level1Fields.SpreadMiddle;
                default:
                    throw new ArgumentOutOfRangeException( nameof( type ), type, null );
            }
        }

        private IEnumerable<CandleMessage> GetCandles(
          SecurityId securityId,
          DateTime? from,
          DateTime? to,
          bool exporting )
        {
            IEnumerable<CandleMessage> source = InternalGetCandles( securityId, from, to );
            TimeZoneInfo tz = TimeZone.TimeZone;
            if ( tz != null )
                source = source.Select( c =>
                {
                    if ( !c.OpenTime.IsDefault() )
                        c.OpenTime = c.OpenTime.Convert( tz );
                    if ( !c.HighTime.IsDefault() )
                        c.HighTime = c.HighTime.Convert( tz );
                    if ( !c.LowTime.IsDefault() )
                        c.LowTime = c.LowTime.Convert( tz );
                    if ( !c.CloseTime.IsDefault() )
                        c.CloseTime = c.CloseTime.Convert( tz );
                    return c;
                } );
            return source;
        }

        private IEnumerable<CandleMessage> InternalGetCandles(
          SecurityId securityId,
          DateTime? from,
          DateTime? to )
        {
            MarketDataMessage marketDataMessage = CreateCandleSeries( TraderHelper.AllSecurity ).ToMarketDataMessage( true, new DateTimeOffset?(), new DateTimeOffset?(), new long?(), true );
            marketDataMessage.SecurityId = securityId;
            Type messageType = marketDataMessage.DataType2.MessageType;
            BuildTypes buildType = BuildType;
            ExtraBuildTypes selectedIndex = ( ExtraBuildTypes )BuildFromExtra.SelectedIndex;
            CandleBuilderProvider service = ConfigManager.GetService<CandleBuilderProvider>();
            switch ( buildType )
            {
                case BuildTypes.DoNot:
                    IMarketDataStorage<CandleMessage> candleMessageStorage = StorageRegistry.GetCandleMessageStorage( messageType, securityId, marketDataMessage.DataType2.Arg, Drive, StorageFormat );
                    DateTime? nullable1 = from;
                    DateTimeOffset? from1 = nullable1.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable1.GetValueOrDefault() ) : new DateTimeOffset?();
                    nullable1 = to;
                    DateTimeOffset? to1 = nullable1.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable1.GetValueOrDefault() ) : new DateTimeOffset?();
                    return candleMessageStorage.Load( from1, to1 );
                case BuildTypes.Ticks:
                    IMarketDataStorage<ExecutionMessage> tickMessageStorage = StorageRegistry.GetTickMessageStorage( securityId, Drive, StorageFormat );
                    DateTime? nullable2 = from;
                    DateTimeOffset? from2 = nullable2.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable2.GetValueOrDefault() ) : new DateTimeOffset?();
                    nullable2 = to;
                    DateTimeOffset? to2 = nullable2.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable2.GetValueOrDefault() ) : new DateTimeOffset?();
                    return tickMessageStorage.Load( from2, to2 ).ToCandles( marketDataMessage, service );
                case BuildTypes.OrderLog:
                    switch ( selectedIndex )
                    {
                        case ExtraBuildTypes.Last:
                            IMarketDataStorage<ExecutionMessage> logMessageStorage1 = StorageRegistry.GetOrderLogMessageStorage( securityId, Drive, StorageFormat );
                            DateTime? nullable3 = from;
                            DateTimeOffset? from3 = nullable3.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable3.GetValueOrDefault() ) : new DateTimeOffset?();
                            nullable3 = to;
                            DateTimeOffset? to3 = nullable3.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable3.GetValueOrDefault() ) : new DateTimeOffset?();
                            return logMessageStorage1.Load( from3, to3 ).ToTicks().ToCandles( marketDataMessage, service );
                        case ExtraBuildTypes.Bid:
                        case ExtraBuildTypes.Ask:
                        case ExtraBuildTypes.Middle:
                            IMarketDataStorage<ExecutionMessage> logMessageStorage2 = StorageRegistry.GetOrderLogMessageStorage( securityId, Drive, StorageFormat );
                            DateTime? nullable4 = from;
                            DateTimeOffset? from4 = nullable4.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable4.GetValueOrDefault() ) : new DateTimeOffset?();
                            nullable4 = to;
                            DateTimeOffset? to4 = nullable4.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable4.GetValueOrDefault() ) : new DateTimeOffset?();
                            return logMessageStorage2.Load( from4, to4 ).ToOrderBooks( new OrderLogMarketDepthBuilder( securityId ), new TimeSpan(), int.MaxValue ).BuildIfNeed( null ).ToCandles( marketDataMessage, Convert( selectedIndex ), service );
                        default:
                            throw new InvalidOperationException( LocalizedStrings.Str2874Params.Put( selectedIndex ) );
                    }
                case BuildTypes.Depths:
                    IMarketDataStorage<QuoteChangeMessage> quoteMessageStorage = StorageRegistry.GetQuoteMessageStorage( securityId, Drive, StorageFormat );
                    DateTime? nullable5 = from;
                    DateTimeOffset? from5 = nullable5.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable5.GetValueOrDefault() ) : new DateTimeOffset?();
                    nullable5 = to;
                    DateTimeOffset? to5 = nullable5.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable5.GetValueOrDefault() ) : new DateTimeOffset?();
                    return quoteMessageStorage.Load( from5, to5 ).BuildIfNeed( null ).ToCandles( marketDataMessage, Convert( selectedIndex ), service );
                case BuildTypes.SmallerTimeFrame:
                    IMarketDataStorage<CandleMessage> buildableStorage = service.GetCandleMessageBuildableStorage( StorageRegistry, securityId, marketDataMessage.GetTimeFrame(), Drive, StorageFormat );
                    DateTime? nullable6 = from;
                    DateTimeOffset? from6 = nullable6.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable6.GetValueOrDefault() ) : new DateTimeOffset?();
                    nullable6 = to;
                    DateTimeOffset? to6 = nullable6.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable6.GetValueOrDefault() ) : new DateTimeOffset?();
                    return buildableStorage.Load( from6, to6 );
                case BuildTypes.Level1:
                    switch ( selectedIndex )
                    {
                        case ExtraBuildTypes.Last:
                            IMarketDataStorage<Level1ChangeMessage> level1MessageStorage1 = StorageRegistry.GetLevel1MessageStorage( securityId, Drive, StorageFormat );
                            DateTime? nullable7 = from;
                            DateTimeOffset? from7 = nullable7.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable7.GetValueOrDefault() ) : new DateTimeOffset?();
                            nullable7 = to;
                            DateTimeOffset? to7 = nullable7.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable7.GetValueOrDefault() ) : new DateTimeOffset?();
                            return level1MessageStorage1.Load( from7, to7 ).ToTicks().ToCandles( marketDataMessage, service );
                        case ExtraBuildTypes.Bid:
                        case ExtraBuildTypes.Ask:
                        case ExtraBuildTypes.Middle:
                            IMarketDataStorage<Level1ChangeMessage> level1MessageStorage2 = StorageRegistry.GetLevel1MessageStorage( securityId, Drive, StorageFormat );
                            DateTime? nullable8 = from;
                            DateTimeOffset? from8 = nullable8.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable8.GetValueOrDefault() ) : new DateTimeOffset?();
                            nullable8 = to;
                            DateTimeOffset? to8 = nullable8.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable8.GetValueOrDefault() ) : new DateTimeOffset?();
                            return level1MessageStorage2.Load( from8, to8 ).ToOrderBooks().ToCandles( marketDataMessage, Convert( selectedIndex ), service );
                        default:
                            throw new InvalidOperationException( LocalizedStrings.Str2874Params.Put( selectedIndex ) );
                    }
                case BuildTypes.FromComposites:
                    BasketSecurity basket;
                    IEnumerable<SecurityId> innerSecurityIds = GetInnerSecurityIds( securityId, out basket );
                    BasketMarketDataStorage<CandleMessage> marketDataStorage = new BasketMarketDataStorage<CandleMessage>();
                    foreach ( SecurityId securityId1 in innerSecurityIds )
                        marketDataStorage.InnerStorages.Add( StorageRegistry.GetCandleMessageStorage( messageType, securityId1, marketDataMessage.DataType2.Arg, Drive, StorageFormat ) );
                    BasketMarketDataStorage<CandleMessage> storage = marketDataStorage;
                    DateTime? nullable9 = from;
                    DateTimeOffset? from9 = nullable9.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable9.GetValueOrDefault() ) : new DateTimeOffset?();
                    nullable9 = to;
                    DateTimeOffset? to9 = nullable9.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable9.GetValueOrDefault() ) : new DateTimeOffset?();
                    return storage.Load( from9, to9 ).ToBasket( basket, ProcessorProvider );
                default:
                    throw new InvalidOperationException( LocalizedStrings.Str2874Params.Put( buildType ) );
            }
        }

        protected override bool CanDirectExport
        {
            get
            {
                return BuildFrom.SelectedIndex == 0;
            }
        }

        protected override bool IsFromComposite
        {
            get
            {
                return BuildType == BuildTypes.FromComposites;
            }
        }

        protected override bool ValidateSettings()
        {
            if ( !base.ValidateSettings() )
                return false;
            CandleSeries candleSeries = CreateCandleSeries( SelectedSecurity );
            object obj = candleSeries.Arg;
            if ( !( obj is TimeSpan ) || !( ( TimeSpan )obj <= TimeSpan.Zero ) )
            {
                Unit unit = obj as Unit;
                if ( ( object )unit == null || !( unit <= ( Unit )0 ) )
                {
                    PnFArg pnFarg = obj as PnFArg;
                    if ( pnFarg == null || !( pnFarg.BoxSize <= ( Unit )0 ) && pnFarg.ReversalAmount > 0 )
                    {
                        if ( BuildType != BuildTypes.SmallerTimeFrame || !( candleSeries.CandleType != typeof( TimeFrameCandle ) ) )
                            return true;
                        int num = ( int )new MessageBoxBuilder().Text( LocalizedStrings.CannotBuildFromSmallerTimeFrame.Put( candleSeries.CandleType.GetDisplayName( null ) ) ).Error().Owner( this ).Show();
                        return false;
                    }
                }
            }
            int num1 = ( int )new MessageBoxBuilder().Text( LocalizedStrings.InvalidArgumentValue ).Error().Owner( this ).Show();
            return false;
        }

        private void FindClick( object sender, RoutedEventArgs e )
        {
            if ( !ValidateSettings() )
                return;
            BuildedCandles.Messages.Clear();
            Progress.Load( GetAllValues<CandleMessage>( new DateTime?(), new DateTime?(), false ), new Action<IEnumerable<CandleMessage>>( BuildedCandles.Messages.AddRange ), 100000, null, null );
        }

        private void ShowChartClick( object sender, RoutedEventArgs e )
        {
            if ( !ValidateSettings() )
                return;
            foreach ( Security selectedSecurity in SelectedSecurities )
            {
                ChartPane chartPane = new ChartPane();
                MainWindow.Instance.ShowPane( chartPane );
                CandleSeries candleSeries = CreateCandleSeries( selectedSecurity );
                chartPane.DrawCandles( candleSeries, GetValues<CandleMessage>( selectedSecurity.ToSecurityId( null, true, false ) ).ToCandles<Candle>( selectedSecurity ), TimeZone.TimeZone );
            }
        }

        private void OnDateValueChanged( object sender, EditValueChangedEventArgs e )
        {
            Progress.ClearStatus();
        }

        private void SelectSecurityBtn_SecuritySelected()
        {
            ComboBoxEditItem fromComposite = FromComposite;
            Security selectedSecurity = SelectedSecurity;
            int num = selectedSecurity != null ? ( selectedSecurity.IsBasket() ? 1 : 0 ) : 0;
            fromComposite.IsEnabled = num != 0;
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            BuildedCandles.Load( storage.GetValue<SettingsStorage>( "BuildedCandles", null ) );
            BuildFrom.SelectedIndex = storage.GetValue( "BuildFrom", 0 );
            BuildFromExtra.SelectedIndex = storage.GetValue( "BuildFromExtra", 0 );
            CandleSettings.Settings = storage.GetValue<SettingsStorage>( "CandleSettings", null ).Load<CandleSeries>();
            VolumeProfile.IsChecked = storage.GetValue( "VolumeProfile", new bool?() );
            IsRegularTradingHours.IsChecked = new bool?( storage.GetValue( "IsRegularTradingHours", false ) );
            TimeZone.TimeZone = storage.GetValue( "TimeZone", TimeZone.TimeZone );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "BuildedCandles", BuildedCandles.Save() );
            storage.SetValue( "BuildFrom", BuildFrom.SelectedIndex );
            storage.SetValue( "BuildFromExtra", BuildFromExtra.SelectedIndex );
            storage.SetValue( "CandleSettings", CandleSettings.Settings.Save() );
            storage.SetValue( "VolumeProfile", VolumeProfile.IsChecked );
            SettingsStorage settingsStorage = storage;
            bool? isChecked = IsRegularTradingHours.IsChecked;
            bool flag = true;
            int num = isChecked.GetValueOrDefault() == flag & isChecked.HasValue ? 1 : 0;
            settingsStorage.SetValue( "IsRegularTradingHours", num != 0 );
            storage.SetValue( "TimeZone", TimeZone.TimeZone );
        }

        

        private enum ExtraBuildTypes
        {
            Last,
            Bid,
            Ask,
            Middle,
        }

        private enum BuildTypes
        {
            DoNot,
            Ticks,
            OrderLog,
            Depths,
            SmallerTimeFrame,
            Level1,
            FromComposites,
        }
    }
}
