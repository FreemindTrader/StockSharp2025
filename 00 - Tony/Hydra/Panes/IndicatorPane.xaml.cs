using DevExpress.Utils;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using DevExpress.XtraGrid;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
using Ecng.Xaml.Converters;
using Microsoft.CSharp.RuntimeBinder;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Export;
using StockSharp.Algo.Indicators;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Hydra.Controls;
using StockSharp.Hydra.Core;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using StockSharp.Xaml.Charting;
using StockSharp.Xaml.GridControl;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace StockSharp.Hydra.Panes
{
    public partial class IndicatorPane : DataPane, IComponentConnector
    {
        private readonly ThreadSafeObservableCollection<IndicatorValue> _findedValues = new ThreadSafeObservableCollection<IndicatorValue>( new UIObservableCollectionEx<IndicatorValue>() );
        private const string _indicatorSettingsKey = "IndicatorSettings";

        public IndicatorPane()
        {
            InitializeComponent();
            IEnumerable<IndicatorType> source = ChartHelper.GetIndicatorTypes().ExcludeObsolete();
            IndicatorCtrl.ItemsSource = source;
            FindedValues.ItemsSource = _findedValues.Items;
            ExportBtn.SetTypeEnabled( ExportTypes.StockSharp, false );
            CandleSettings.Settings = new CandleSeries()
            {
                CandleType = typeof( TimeFrameCandle ),
                Arg = TimeSpan.FromMinutes( 1.0 )
            };
            Init( ExportBtn, MainGrid, SelectSecurityBtn, new Func<SecurityId, DateTime?, DateTime?, bool, IEnumerable>( GetIndicatorValues ) );
            SetTitlePrefix( LocalizedStrings.Str1981 );
            SelectedIndicator = source.FirstOrDefault();
        }

        protected override bool CanDirectExport
        {
            get
            {
                return false;
            }
        }

        protected override DataType DataType
        {
            get
            {
                return DataType.Create( typeof( IndicatorValue ), SelectedIndicator );
            }
        }

        public override string Title
        {
            get
            {
                return base.Title + " " + SelectedIndicator?.Name;
            }
            set
            {
            }
        }

        private IndicatorType SelectedIndicator
        {
            get
            {
                return ( IndicatorType )IndicatorCtrl?.SelectedItem;
            }
            set
            {
                IndicatorCtrl.SelectedItem = value;
            }
        }

        private BuildTypes BuildType
        {
            get
            {
                return ( BuildTypes )BuildFrom.SelectedIndex;
            }
            set
            {
                BuildFrom.SelectedIndex = ( int )value;
            }
        }

        private IIndicator Indicator
        {
            get
            {
                return ( IIndicator )PropGrid.SelectedObject;
            }
            set
            {
                PropGrid.SelectedObject = value;
            }
        }

        private static Candle CreateCandle( Decimal price )
        {
            TickCandle tickCandle = new TickCandle();
            tickCandle.OpenPrice = price;
            tickCandle.HighPrice = price;
            tickCandle.LowPrice = price;
            tickCandle.ClosePrice = price;
            tickCandle.State = CandleStates.Finished;
            return tickCandle;
        }

        private IMarketDataStorage<TMessage> CombineStorage<TMessage>( Func<SecurityId, IMarketDataStorage> getStorage )
          where TMessage : Message
        {
            BasketMarketDataStorage<TMessage> marketDataStorage = new BasketMarketDataStorage<TMessage>();
            foreach ( Security security in SelectedSecurities.Take( 2 ) )
                marketDataStorage.InnerStorages.Add( getStorage( security.ToSecurityId( null, true, false ) ) );
            return marketDataStorage;
        }

        private static IEnumerable<Tuple<DateTimeOffset, Tuple<Decimal, Decimal>>> Join<TMessage>( IEnumerable<TMessage> messages, Func<TMessage, Decimal?> getPrice )
            where TMessage : Message
        {
            Dictionary<SecurityId, Decimal> dict = new Dictionary<SecurityId, Decimal>();
            return messages.Select(
                ( m =>
                {
                    Decimal? nullable = getPrice( m );

                    if ( nullable.HasValue )
                    {
                    }
                    return m;
                } ) )
                .Where( m => dict.Count > 1 )
                .Select(
                    m =>
                    {
                        Dictionary<SecurityId, Decimal>.KeyCollection keys = dict.Keys;
                        return Tuple.Create(
                            m.GetServerTime(),
                            Tuple.Create( dict[keys.First()], dict[keys.Last()] ) );
                    } );
        }

        private IEnumerable<Tuple<DateTimeOffset, IIndicatorValue>> GetPairIndicatorValues( IIndicator indicator, DateTime? from, DateTime? to )
        {
            switch ( BuildType )
            {
                case BuildTypes.Candles:
                    IMarketDataStorage<CandleMessage> storage1 = CombineStorage<CandleMessage>( s => StorageRegistry.GetCandleMessageStorage( CandleSettings.Settings.CandleType.ToCandleMessageType(), s, CandleSettings.Settings.Arg, Drive, StorageFormat ) );
                    DateTime? nullable1 = from;
                    DateTimeOffset? from1 = nullable1.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable1.GetValueOrDefault() ) : new DateTimeOffset?();
                    nullable1 = to;
                    DateTimeOffset? to1 = nullable1.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable1.GetValueOrDefault() ) : new DateTimeOffset?();
                    return Join( storage1.Load( from1, to1 ), m => new Decimal?( m.ClosePrice ) ).Select( t => Tuple.Create<DateTimeOffset, IIndicatorValue>( t.Item1, new PairIndicatorValue<Decimal>( indicator, t.Item2 ) ) );
                case BuildTypes.Level1:
                    IMarketDataStorage<Level1ChangeMessage> storage2 = CombineStorage<Level1ChangeMessage>( s => StorageRegistry.GetLevel1MessageStorage( s, Drive, StorageFormat ) );
                    DateTime? nullable2 = from;
                    DateTimeOffset? from2 = nullable2.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable2.GetValueOrDefault() ) : new DateTimeOffset?();
                    nullable2 = to;
                    DateTimeOffset? to2 = nullable2.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable2.GetValueOrDefault() ) : new DateTimeOffset?();
                    return Join( storage2.Load( from2, to2 ), m => m.GetLastTradePrice() ).Select( t => Tuple.Create<DateTimeOffset, IIndicatorValue>( t.Item1, new PairIndicatorValue<Decimal>( indicator, t.Item2 ) ) );
                case BuildTypes.Ticks:
                    IMarketDataStorage<ExecutionMessage> storage3 = CombineStorage<ExecutionMessage>( s => StorageRegistry.GetTickMessageStorage( s, Drive, StorageFormat ) );
                    DateTime? nullable3 = from;
                    DateTimeOffset? from3 = nullable3.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable3.GetValueOrDefault() ) : new DateTimeOffset?();
                    nullable3 = to;
                    DateTimeOffset? to3 = nullable3.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable3.GetValueOrDefault() ) : new DateTimeOffset?();
                    return Join( storage3.Load( from3, to3 ), m => m.TradePrice ).Select( t => Tuple.Create<DateTimeOffset, IIndicatorValue>( t.Item1, new PairIndicatorValue<Decimal>( indicator, t.Item2 ) ) );
                case BuildTypes.Depths:
                    IMarketDataStorage<QuoteChangeMessage> storage4 = CombineStorage<QuoteChangeMessage>( s => StorageRegistry.GetQuoteMessageStorage( s, Drive, StorageFormat ) );
                    DateTime? nullable4 = from;
                    DateTimeOffset? from4 = nullable4.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable4.GetValueOrDefault() ) : new DateTimeOffset?();
                    nullable4 = to;
                    DateTimeOffset? to4 = nullable4.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable4.GetValueOrDefault() ) : new DateTimeOffset?();
                    return Join( storage4.Load( from4, to4 ).BuildIfNeed( null ), m => m.GetSpreadMiddle() ).Select( t => Tuple.Create<DateTimeOffset, IIndicatorValue>( t.Item1, new PairIndicatorValue<Decimal>( indicator, t.Item2 ) ) );
                case BuildTypes.OrderLog:
                    IMarketDataStorage<ExecutionMessage> storage5 = CombineStorage<ExecutionMessage>( s => StorageRegistry.GetOrderLogMessageStorage( s, Drive, StorageFormat ) );
                    DateTime? nullable5 = from;
                    DateTimeOffset? from5 = nullable5.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable5.GetValueOrDefault() ) : new DateTimeOffset?();
                    nullable5 = to;
                    DateTimeOffset? to5 = nullable5.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable5.GetValueOrDefault() ) : new DateTimeOffset?();
                    return Join( storage5.Load( from5, to5 ), m => m.TradePrice ).Select( t => Tuple.Create<DateTimeOffset, IIndicatorValue>( t.Item1, new PairIndicatorValue<Decimal>( indicator, t.Item2 ) ) );
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private IEnumerable<Tuple<DateTimeOffset, IIndicatorValue>> GetSingleIndicatorValues( IIndicator indicator, SecurityId securityId, DateTime? from, DateTime? to )
        {
            Security security = GetSecurity( securityId );
            switch ( BuildType )
            {
                case BuildTypes.Candles:
                    IMarketDataStorage<CandleMessage> candleMessageStorage = StorageRegistry.GetCandleMessageStorage( CandleSettings.Settings.CandleType.ToCandleMessageType(), securityId, CandleSettings.Settings.Arg, Drive, StorageFormat );
                    DateTime? nullable1 = from;
                    DateTimeOffset? from1 = nullable1.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable1.GetValueOrDefault() ) : new DateTimeOffset?();
                    nullable1 = to;
                    DateTimeOffset? to1 = nullable1.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable1.GetValueOrDefault() ) : new DateTimeOffset?();
                    return candleMessageStorage.Load( from1, to1 ).Select( c => Tuple.Create<DateTimeOffset, IIndicatorValue>( c.OpenTime, new CandleIndicatorValue( indicator, c.ToCandle( security ) ) ) );
                case BuildTypes.Level1:
                    IMarketDataStorage<Level1ChangeMessage> level1MessageStorage = StorageRegistry.GetLevel1MessageStorage( securityId, Drive, StorageFormat );
                    DateTime? nullable2 = from;
                    DateTimeOffset? from2 = nullable2.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable2.GetValueOrDefault() ) : new DateTimeOffset?();
                    nullable2 = to;
                    DateTimeOffset? to2 = nullable2.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable2.GetValueOrDefault() ) : new DateTimeOffset?();
                    return level1MessageStorage.Load( from2, to2 ).Select( m => Tuple.Create( m.ServerTime, m.GetLastTradePrice() ) ).Where( t => t.Item2.HasValue ).Select( t => Tuple.Create<DateTimeOffset, IIndicatorValue>( t.Item1, new CandleIndicatorValue( indicator, CreateCandle( t.Item2.Value ) ) ) );
                case BuildTypes.Ticks:
                    IMarketDataStorage<ExecutionMessage> tickMessageStorage = StorageRegistry.GetTickMessageStorage( securityId, Drive, StorageFormat );
                    DateTime? nullable3 = from;
                    DateTimeOffset? from3 = nullable3.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable3.GetValueOrDefault() ) : new DateTimeOffset?();
                    nullable3 = to;
                    DateTimeOffset? to3 = nullable3.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable3.GetValueOrDefault() ) : new DateTimeOffset?();
                    return tickMessageStorage.Load( from3, to3 ).Select( m => Tuple.Create( m.ServerTime, m.TradePrice ) ).Where( t => t.Item2.HasValue ).Select( t => Tuple.Create<DateTimeOffset, IIndicatorValue>( t.Item1, new CandleIndicatorValue( indicator, CreateCandle( t.Item2.Value ) ) ) );
                case BuildTypes.Depths:
                    IMarketDataStorage<QuoteChangeMessage> quoteMessageStorage = StorageRegistry.GetQuoteMessageStorage( securityId, Drive, StorageFormat );
                    DateTime? nullable4 = from;
                    DateTimeOffset? from4 = nullable4.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable4.GetValueOrDefault() ) : new DateTimeOffset?();
                    nullable4 = to;
                    DateTimeOffset? to4 = nullable4.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable4.GetValueOrDefault() ) : new DateTimeOffset?();
                    return quoteMessageStorage.Load( from4, to4 ).BuildIfNeed( null ).Select( m => Tuple.Create( m.ServerTime, m.GetSpreadMiddle() ) ).Where( t => t.Item2.HasValue ).Select( t => Tuple.Create<DateTimeOffset, IIndicatorValue>( t.Item1, new CandleIndicatorValue( indicator, CreateCandle( t.Item2.Value ) ) ) );
                case BuildTypes.OrderLog:
                    IMarketDataStorage<ExecutionMessage> logMessageStorage = StorageRegistry.GetOrderLogMessageStorage( securityId, Drive, StorageFormat );
                    DateTime? nullable5 = from;
                    DateTimeOffset? from5 = nullable5.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable5.GetValueOrDefault() ) : new DateTimeOffset?();
                    nullable5 = to;
                    DateTimeOffset? to5 = nullable5.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable5.GetValueOrDefault() ) : new DateTimeOffset?();
                    return logMessageStorage.Load( from5, to5 ).Select( m => Tuple.Create( m.ServerTime, m.TradePrice ) ).Where( t => t.Item2.HasValue ).Select( t => Tuple.Create<DateTimeOffset, IIndicatorValue>( t.Item1, new CandleIndicatorValue( indicator, CreateCandle( t.Item2.Value ) ) ) );
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private IEnumerable<Tuple<DateTimeOffset, IIndicatorValue>> GetLevel1IndicatorValues( IIndicator indicator, SecurityId securityId, DateTime? from, DateTime? to )
        {
            switch ( BuildType )
            {
                case BuildTypes.Candles:
                    IMarketDataStorage<CandleMessage> candleMessageStorage = StorageRegistry.GetCandleMessageStorage( CandleSettings.Settings.CandleType.ToCandleMessageType(), securityId, CandleSettings.Settings.Arg, Drive, StorageFormat );
                    DateTime? nullable1 = from;
                    DateTimeOffset? from1 = nullable1.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable1.GetValueOrDefault() ) : new DateTimeOffset?();
                    nullable1 = to;
                    DateTimeOffset? to1 = nullable1.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable1.GetValueOrDefault() ) : new DateTimeOffset?();
                    return candleMessageStorage.Load( from1, to1 ).Select( m => Tuple.Create<DateTimeOffset, IIndicatorValue>( m.OpenTime, new SingleIndicatorValue<Level1ChangeMessage>( indicator, m.ToLevel1() ) ) );
                case BuildTypes.Level1:
                    IMarketDataStorage<Level1ChangeMessage> level1MessageStorage = StorageRegistry.GetLevel1MessageStorage( securityId, Drive, StorageFormat );
                    DateTime? nullable2 = from;
                    DateTimeOffset? from2 = nullable2.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable2.GetValueOrDefault() ) : new DateTimeOffset?();
                    nullable2 = to;
                    DateTimeOffset? to2 = nullable2.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable2.GetValueOrDefault() ) : new DateTimeOffset?();
                    return level1MessageStorage.Load( from2, to2 ).Select( m => Tuple.Create<DateTimeOffset, IIndicatorValue>( m.ServerTime, new SingleIndicatorValue<Level1ChangeMessage>( indicator, m ) ) );
                case BuildTypes.Ticks:
                    IMarketDataStorage<ExecutionMessage> tickMessageStorage = StorageRegistry.GetTickMessageStorage( securityId, Drive, StorageFormat );
                    DateTime? nullable3 = from;
                    DateTimeOffset? from3 = nullable3.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable3.GetValueOrDefault() ) : new DateTimeOffset?();
                    nullable3 = to;
                    DateTimeOffset? to3 = nullable3.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable3.GetValueOrDefault() ) : new DateTimeOffset?();
                    return tickMessageStorage.Load( from3, to3 ).Select( m => Tuple.Create<DateTimeOffset, IIndicatorValue>( m.ServerTime, new SingleIndicatorValue<Level1ChangeMessage>( indicator, m.ToLevel1() ) ) );
                case BuildTypes.Depths:
                    IMarketDataStorage<QuoteChangeMessage> quoteMessageStorage = StorageRegistry.GetQuoteMessageStorage( securityId, Drive, StorageFormat );
                    DateTime? nullable4 = from;
                    DateTimeOffset? from4 = nullable4.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable4.GetValueOrDefault() ) : new DateTimeOffset?();
                    nullable4 = to;
                    DateTimeOffset? to4 = nullable4.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable4.GetValueOrDefault() ) : new DateTimeOffset?();
                    return quoteMessageStorage.Load( from4, to4 ).BuildIfNeed( null ).Select( m => Tuple.Create<DateTimeOffset, IIndicatorValue>( m.ServerTime, new SingleIndicatorValue<Level1ChangeMessage>( indicator, m.ToLevel1() ) ) );
                case BuildTypes.OrderLog:
                    IMarketDataStorage<ExecutionMessage> logMessageStorage = StorageRegistry.GetOrderLogMessageStorage( securityId, Drive, StorageFormat );
                    DateTime? nullable5 = from;
                    DateTimeOffset? from5 = nullable5.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable5.GetValueOrDefault() ) : new DateTimeOffset?();
                    nullable5 = to;
                    DateTimeOffset? to5 = nullable5.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable5.GetValueOrDefault() ) : new DateTimeOffset?();
                    return logMessageStorage.Load( from5, to5 ).Where( m => m.TradePrice.HasValue ).Select( m => Tuple.Create<DateTimeOffset, IIndicatorValue>( m.ServerTime, new SingleIndicatorValue<Level1ChangeMessage>( indicator, m.ToLevel1() ) ) );
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private IEnumerable<IndicatorValue> GetIndicatorValues( SecurityId securityId, DateTime? from, DateTime? to, bool exporting )
        {
            IEnumerable<IndicatorValue> source = InternalGetIndicatorValues( securityId, from, to );
            TimeZoneInfo tz = TimeZone.TimeZone;
            if ( tz != null )
                source = source.Select( v =>
                {
                    if ( !v.Time.IsDefault() )
                        v.Time = v.Time.Convert( tz );
                    return v;
                } );
            return source;
        }

        private IEnumerable<IndicatorValue> InternalGetIndicatorValues( SecurityId securityId, DateTime? from, DateTime? to )
        {
            IIndicator indicator = Indicator.Clone();
            IEnumerable<IndicatorValue> source = ( IsPairIndicator ? GetPairIndicatorValues( indicator, from, to ) : ( SelectedIndicator.InputValue == typeof( SingleIndicatorValue<Level1ChangeMessage> ) ? GetLevel1IndicatorValues( indicator, securityId, from, to ) : GetSingleIndicatorValues( indicator, securityId, from, to ) ) ).Select( t =>
            {
                t.Item2.IsFinal = true;
                return new IndicatorValue() { Time = t.Item1, Value = indicator.Process( t.Item2 ), SecurityId = securityId };
            } );
            bool? isChecked = OnlyFormed.IsChecked;
            bool flag = true;
            if ( isChecked.GetValueOrDefault() == flag & isChecked.HasValue )
                source = source.Where( v => v.Value.IsFormed );
            return source;
        }

        private bool IsPairIndicator
        {
            get
            {
                return SelectedIndicator.InputValue == typeof( PairIndicatorValue<Decimal> );
            }
        }

        protected override IEnumerable<Security> FilterSecurities(
          IEnumerable<Security> securities )
        {
            if ( !IsPairIndicator )
                return securities;
            return securities.Take( 1 );
        }

        protected override bool ValidateSettings()
        {
            if ( !base.ValidateSettings() )
                return false;
            if ( SelectedIndicator != null )
            {
                if ( !IsPairIndicator || SelectedSecurities.Count() >= 2 )
                    return true;
                int num = ( int )new MessageBoxBuilder().Text( LocalizedStrings.NeedNSecurities.Put( 2 ) ).Warning().Owner( this ).Show();
                return false;
            }
            int num1 = ( int )new MessageBoxBuilder().Text( LocalizedStrings.SelectIndicator ).Warning().Owner( this ).Show();
            return false;
        }

        private void OnDateValueChanged( object sender, EditValueChangedEventArgs e )
        {
            Progress.ClearStatus();
        }

        private void FindClick( object sender, RoutedEventArgs e )
        {
            if ( !ValidateSettings() )
                return;
            _findedValues.Clear();
            Progress.Load( GetAllValues<IndicatorValue>( new DateTime?(), new DateTime?(), false ), new Action<IEnumerable<IndicatorValue>>( _findedValues.AddRange ), 10000, null, null );
        }

        private void IndicatorCtrl_OnEditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            if ( ExportBtn != null )
                TryEnabledExport();
            IndicatorType selectedIndicator = SelectedIndicator;
            Indicator = selectedIndicator != null ? selectedIndicator.Indicator.CreateInstance<IIndicator>() : null;
            UpdateTitle();
            _findedValues.Clear();
            FindedValues.Columns.RemoveRange( FindedValues.Columns.Skip( 1 ).ToArray() );
            List<IIndicator> indicatorList = new List<IIndicator>();
            if ( Indicator != null )
                PlainIndicators( indicatorList, Indicator );
            int num = 0;
            foreach ( IIndicator indicator in indicatorList )
            {
                GridColumnCollection columns = FindedValues.Columns;
                GridColumn gridColumn = new GridColumn();
                gridColumn.Header = indicator.Name;
                gridColumn.Width = ( GridColumnWidth )150.0;
                gridColumn.SortMode = ColumnSortMode.Custom;
                gridColumn.AllowSorting = DefaultBoolean.True;
                gridColumn.Binding = new Binding()
                {
                    Path = new PropertyPath( "ValuesAsDecimal", Array.Empty<object>() ),
                    Converter = new IndexerConverter(),
                    ConverterParameter = num
                };
                columns.Add( gridColumn );
                ++num;
            }
        }

        private static void PlainIndicators( ICollection<IIndicator> plainList, IIndicator indicator )
        {
            if ( indicator == null )
                throw new ArgumentNullException( nameof( indicator ) );
            IComplexIndicator complexIndicator = indicator as IComplexIndicator;
            if ( complexIndicator != null )
            {
                foreach ( IIndicator innerIndicator in complexIndicator.InnerIndicators )
                    PlainIndicators( plainList, innerIndicator );
            }
            else
                plainList.Add( indicator );
        }

        protected override bool IsExportEnabled()
        {
            if ( base.IsExportEnabled() )
                return SelectedIndicator != null;
            return false;
        }

        private void BuildFrom_OnSelectionChanged( object sender, RoutedEventArgs e )
        {
            if ( ExportBtn != null )
                TryEnabledExport();
            if ( CandleSettings == null )
                return;
            CandleSettings.IsEnabled = BuildType == BuildTypes.Candles;
        }

        private void ShowChartClick( object sender, RoutedEventArgs e )
        {
            if ( !ValidateSettings() )
                return;
            foreach ( SecurityId securityId1 in IsPairIndicator ?   ( new SecurityId[1] ) : SelectedSecurityIds )
            {
                ChartPane chartPane1 = new ChartPane();
                MainWindow.Instance.ShowPane( chartPane1 );
                ChartPane chartPane2 = chartPane1;
                SecurityId securityId2 = securityId1;
                string name = SelectedIndicator.Name;
                IEnumerable<IndicatorValue> values = GetValues<IndicatorValue>( securityId1 );
                Type painter1 = SelectedIndicator.Painter;
                IChartIndicatorPainter painter2 = ( object )painter1 != null ? painter1.CreateInstance<IChartIndicatorPainter>() : null;
                TimeZoneInfo timeZone = TimeZone.TimeZone;
                chartPane2.DrawIndicator( securityId2, name, values, painter2, timeZone );
            }
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            FindedValues.Load( storage.GetValue<SettingsStorage>( "FindedValues", null ) );
            BuildType = storage.GetValue( "BuildType", BuildTypes.Candles );
            string indName = storage.GetValue<string>( "SelectedIndicator", null );
            SelectedIndicator = ( ( IEnumerable<IndicatorType> )IndicatorCtrl.ItemsSource ).FirstOrDefault( t => t.Name == indName );
            if ( Indicator != null && storage.ContainsKey( "IndicatorSettings" ) )
                Indicator.Load( storage.GetValue<SettingsStorage>( "IndicatorSettings", null ) );
            if ( storage.ContainsKey( "CandleSettings" ) )
            {
                CandleSeries candleSeries = new CandleSeries();
                candleSeries.Load( storage.GetValue<SettingsStorage>( "CandleSettings", null ) );
                CandleSettings.Settings = candleSeries;
            }
            OnlyFormed.IsChecked = new bool?( storage.GetValue( "OnlyFormed", true ) );
            TimeZone.TimeZone = storage.GetValue( "TimeZone", TimeZone.TimeZone );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "FindedValues", FindedValues.Save() );
            storage.SetValue( "BuildType", BuildType );
            if ( SelectedIndicator != null )
            {
                storage.SetValue( "SelectedIndicator", SelectedIndicator.Name );
                storage.SetValue( "IndicatorSettings", Indicator.Save() );
            }
            storage.SetValue( "CandleSettings", CandleSettings.Settings.Save() );
            storage.SetValue( "OnlyFormed", OnlyFormed.IsChecked );
            storage.SetValue( "TimeZone", TimeZone.TimeZone );
        }


        private enum BuildTypes
        {
            Candles,
            Level1,
            Ticks,
            Depths,
            OrderLog,
        }
    }
}
