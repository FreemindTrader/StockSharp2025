using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using Ecng.Collections;
using Ecng.Common;
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
using StockSharp.Studio.Controls.Editors;
using StockSharp.Xaml;
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
    public partial class DepthPane : DataPane, IComponentConnector
    {
        private readonly List<QuoteChangeMessage> _loadedDepths = new List<QuoteChangeMessage>();

        public DepthPane()
        {
            InitializeComponent();
            Init( ExportBtn, MainGrid, SelectSecurityBtn, new Func<SecurityId, DateTime?, DateTime?, bool, IEnumerable>( GetDepths ) );
            SetTitlePrefix( LocalizedStrings.MarketDepths );
            IsSingleSecurity = true;
        }

        protected override DataType DataType
        {
            get
            {
                return DataType.MarketDepth;
            }
        }

        private BuildTypes BuildType
        {
            get
            {
                return ( BuildTypes )BuildFrom.SelectedIndex;
            }
        }

        private int Depth
        {
            get
            {
                int result;
                if ( !int.TryParse( DepthCb.Text, out result ) )
                    return 20;
                if ( result <= 0 )
                    return 1;
                return result;
            }
            set
            {
                DepthCb.Text = value.To<string>();
            }
        }

        private void BuildFrom_OnSelectionChanged( object sender, RoutedEventArgs e )
        {
            ExportBtn?.SetTypeEnabled( ExportTypes.SaveBuild, ( uint )BuildType > 0U );
            OrderLogBuilder.IsEnabled = BuildType == BuildTypes.OrderLog;
        }

        private IEnumerable<QuoteChangeMessage> GetDepths(
          SecurityId securityId,
          DateTime? from,
          DateTime? to,
          bool exporting )
        {
            IEnumerable<QuoteChangeMessage> source = InternalGetDepths( securityId, from, to, exporting );
            TimeZoneInfo tz = TimeZone.TimeZone;
            if ( tz != null )
                source = source.Select( m =>
                {
                    if ( !m.ServerTime.IsDefault() )
                        m.ServerTime = m.ServerTime.Convert( tz );
                    if ( !m.LocalTime.IsDefault() )
                        m.LocalTime = m.LocalTime.Convert( tz );
                    return m;
                } );
            return source;
        }

        private IEnumerable<QuoteChangeMessage> InternalGetDepths(
          SecurityId securityId,
          DateTime? from,
          DateTime? to,
          bool exporting )
        {
            int maxDepth = Depth;
            int num = DepthGenerationInterval.EditValue.To<int?>().GetValueOrDefault();
            if ( num > 100000000 )
                num = 100000000;
            TimeSpan interval = TimeSpan.FromMilliseconds( num );
            switch ( BuildFrom.SelectedIndex )
            {
                case 0:
                    IMarketDataStorage<QuoteChangeMessage> quoteMessageStorage = StorageRegistry.GetQuoteMessageStorage( securityId, Drive, StorageFormat );
                    DateTime? nullable1 = from;
                    DateTimeOffset? from1 = nullable1.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable1.GetValueOrDefault() ) : new DateTimeOffset?();
                    DateTime? nullable2 = to;
                    DateTimeOffset? to1 = nullable2.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable2.GetValueOrDefault() ) : new DateTimeOffset?();
                    IEnumerable<QuoteChangeMessage> quoteChangeMessages = quoteMessageStorage.Load( from1, to1 );
                    if ( !exporting )
                        quoteChangeMessages = quoteChangeMessages.BuildIfNeed( null );
                    return quoteChangeMessages.Select( md =>
                    {
                        md.Bids = md.Bids.Take( maxDepth ).ToArray();
                        md.Asks = md.Asks.Take( maxDepth ).ToArray();
                        return md;
                    } ).WhereWithPrevious( ( prev, curr ) => curr.ServerTime - prev.ServerTime >= interval );
                case 1:
                    IMarketDataStorage<ExecutionMessage> logMessageStorage = StorageRegistry.GetOrderLogMessageStorage( securityId, Drive, StorageFormat );
                    DateTime? nullable3 = from;
                    DateTimeOffset? from2 = nullable3.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable3.GetValueOrDefault() ) : new DateTimeOffset?();
                    DateTime? nullable4 = to;
                    DateTimeOffset? to2 = nullable4.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable4.GetValueOrDefault() ) : new DateTimeOffset?();
                    return logMessageStorage.Load( from2, to2 ).ToOrderBooks( OrderLogBuilder.SelectedBuilder.CreateOrderLogMarketDepthBuilder( securityId ), interval, maxDepth ).BuildIfNeed( null );
                case 2:
                    IMarketDataStorage<Level1ChangeMessage> level1MessageStorage = StorageRegistry.GetLevel1MessageStorage( securityId, Drive, StorageFormat );
                    DateTime? nullable5 = from;
                    DateTimeOffset? from3 = nullable5.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable5.GetValueOrDefault() ) : new DateTimeOffset?();
                    DateTime? nullable6 = to;
                    DateTimeOffset? to3 = nullable6.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable6.GetValueOrDefault() ) : new DateTimeOffset?();
                    return level1MessageStorage.Load( from3, to3 ).ToOrderBooks();
                case 3:
                    BasketSecurity basket;
                    IEnumerable<SecurityId> innerSecurityIds = GetInnerSecurityIds( securityId, out basket );
                    BasketMarketDataStorage<QuoteChangeMessage> marketDataStorage = new BasketMarketDataStorage<QuoteChangeMessage>();
                    foreach ( SecurityId securityId1 in innerSecurityIds )
                        marketDataStorage.InnerStorages.Add( StorageRegistry.GetQuoteMessageStorage( securityId1, Drive, StorageFormat ) );
                    BasketMarketDataStorage<QuoteChangeMessage> storage = marketDataStorage;
                    DateTime? nullable7 = from;
                    DateTimeOffset? from4 = nullable7.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable7.GetValueOrDefault() ) : new DateTimeOffset?();
                    DateTime? nullable8 = to;
                    DateTimeOffset? to4 = nullable8.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable8.GetValueOrDefault() ) : new DateTimeOffset?();
                    return storage.Load( from4, to4 ).ToBasket( basket, ProcessorProvider );
                default:
                    throw new InvalidOperationException();
            }
        }

        protected override bool IsFromComposite
        {
            get
            {
                return BuildType == BuildTypes.FromComposites;
            }
        }

        private void FindClick( object sender, RoutedEventArgs e )
        {
            if ( !ValidateSettings() )
                return;
            DepthGrid.Clear();
            DepthGrid.UpdateFormat( SelectedSecurity );
            DepthGrid.MaxDepth = Depth;
            bool isFirstAdded = false;
            _loadedDepths.Clear();
            QuotesSlider.Maximum = 0.0;
            QuotesSlider.Minimum = 0.0;
            QuotesSlider.SmallChange = 1.0;
            QuotesSlider.LargeChange = 5.0;
            DepthDate.Text = string.Empty;
            Progress.Load( GetAllValues<QuoteChangeMessage>( new DateTime?(), new DateTime?(), false ), new Action<IEnumerable<QuoteChangeMessage>>( _loadedDepths.AddRange ), 1000, items =>
            {
                QuotesSlider.Maximum = _loadedDepths.Count - 1;
                if ( isFirstAdded )
                    return;
                DisplayDepth();
                isFirstAdded = true;
            }, null );
        }

        private void QuotesSliderValueChanged( object sender, RoutedPropertyChangedEventArgs<double> e )
        {
            DisplayDepth();
        }

        private void DisplayDepth()
        {
            int index = ( int )QuotesSlider.Value;
            if ( _loadedDepths.Count < index + 1 )
                return;
            QuoteChangeMessage loadedDepth = _loadedDepths[index];
            DepthGrid.UpdateDepth( loadedDepth, null );
            DepthDate.Text = loadedDepth.ServerTime.ToString( "yyyy.MM.dd HH:mm:ss.fff" );
        }

        protected override bool CanDirectExport
        {
            get
            {
                return BuildFrom.SelectedIndex == 0;
            }
        }

        private void SelectSecurityBtn_SecuritySelected()
        {
            ComboBoxEditItem fromComposite = FromComposite;
            Security selectedSecurity = SelectedSecurity;
            int num = selectedSecurity != null ? ( selectedSecurity.IsBasket() ? 1 : 0 ) : 0;
            fromComposite.IsEnabled = num != 0;
        }

        private void ShowChartCandles_Click( object sender, RoutedEventArgs e )
        {
            if ( !ValidateSettings() )
                return;
            foreach ( Security selectedSecurity in SelectedSecurities )
            {
                ChartPane chartPane = new ChartPane();
                MainWindow.Instance.ShowPane( chartPane );
                CandleSeries series = new CandleSeries( typeof( TickCandle ), selectedSecurity, 1 );
                chartPane.DrawCandles( series, GetValues<QuoteChangeMessage>( selectedSecurity.ToSecurityId( null, true, false ) ).ToCandles( series, Level1Fields.SpreadMiddle, null ).ToEntities<CandleMessage, Candle>( series.Security, null ), TimeZone.TimeZone );
            }
        }

        private void ShowChartSpread_Click( object sender, RoutedEventArgs e )
        {
            if ( !ValidateSettings() )
                return;
            foreach ( SecurityId selectedSecurityId in SelectedSecurityIds )
            {
                ChartPane chartPane = new ChartPane();
                MainWindow.Instance.ShowPane( chartPane );
                chartPane.DrawLines( selectedSecurityId, GetValues<QuoteChangeMessage>( selectedSecurityId ), TimeZone.TimeZone );
            }
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            DepthGrid.Load( storage.GetValue<SettingsStorage>( "DepthGrid", null ) );
            Depth = storage.GetValue( "Depth", 0 );
            if ( storage.ContainsKey( "DepthGenerationInterval" ) )
                DepthGenerationInterval.EditValue = storage.GetValue( "DepthGenerationInterval", 0 );
            BuildFrom.SelectedIndex = storage.GetValue( "BuildFrom", 0 );
            TimeZone.TimeZone = storage.GetValue( "TimeZone", TimeZone.TimeZone );
            OrderLogBuilderComboBox orderLogBuilder = OrderLogBuilder;
            Type selectedBuilder = storage.GetValue<Type>( "OrderLogBuilder", null );
            if ( ( object )selectedBuilder == null )
                selectedBuilder = OrderLogBuilder.SelectedBuilder;
            orderLogBuilder.SelectedBuilder = selectedBuilder;
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "DepthGrid", DepthGrid.Save() );
            storage.SetValue( "Depth", Depth );
            if ( DepthGenerationInterval.EditValue != null )
                storage.SetValue( "DepthGenerationInterval", DepthGenerationInterval.EditValue.To<int>() );
            storage.SetValue( "BuildFrom", BuildFrom.SelectedIndex );
            storage.SetValue( "TimeZone", TimeZone.TimeZone );
            storage.SetValue( "OrderLogBuilder", OrderLogBuilder.SelectedBuilder.GetTypeName( false ) );
        }

        

        private enum BuildTypes
        {
            DoNot,
            OrderLog,
            Level1,
            FromComposites,
        }
    }
}
