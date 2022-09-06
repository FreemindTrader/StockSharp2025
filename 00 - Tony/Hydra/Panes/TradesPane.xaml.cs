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
using StockSharp.Messages;
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
    public partial class TradesPane : DataPane, IComponentConnector
    {        
        public TradesPane()
        {
            InitializeComponent();
            FindedTrades.HideColumns( ExecutionTypes.Tick );
            Init( ExportBtn, MainGrid, SelectSecurityBtn, new Func<SecurityId, DateTime?, DateTime?, bool, IEnumerable>( GetTrades ) );
            SetTitlePrefix( LocalizedStrings.Str985 );
        }

        protected override DataType DataType
        {
            get
            {
                return DataType.Ticks;
            }
        }

        private BuildTypes BuildType
        {
            get
            {
                return ( BuildTypes )BuildFrom.SelectedIndex;
            }
        }

        private void BuildFrom_OnSelectionChanged( object sender, RoutedEventArgs e )
        {
            ExportBtn?.SetTypeEnabled( ExportTypes.SaveBuild, ( uint )BuildType > 0U );
        }

        private IEnumerable<ExecutionMessage> GetTrades(
          SecurityId securityId,
          DateTime? from,
          DateTime? to,
          bool exporting )
        {
            IEnumerable<ExecutionMessage> source = InternalGetTrades( securityId, from, to );
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

        private IEnumerable<ExecutionMessage> InternalGetTrades(
          SecurityId securityId,
          DateTime? from,
          DateTime? to )
        {
            switch ( BuildType )
            {
                case BuildTypes.DoNot:
                    IMarketDataStorage<ExecutionMessage> tickMessageStorage = StorageRegistry.GetTickMessageStorage( securityId, Drive, StorageFormat );
                    DateTime? nullable1 = from;
                    DateTimeOffset? from1 = nullable1.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable1.GetValueOrDefault() ) : new DateTimeOffset?();
                    nullable1 = to;
                    DateTimeOffset? to1 = nullable1.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable1.GetValueOrDefault() ) : new DateTimeOffset?();
                    IEnumerable<ExecutionMessage> source = tickMessageStorage.Load( from1, to1 );
                    bool? isChecked1 = IsNonSystem.IsChecked;
                    bool flag1 = false;
                    if ( isChecked1.GetValueOrDefault() == flag1 & isChecked1.HasValue )
                        source = source.Where( t =>
                        {
                            bool? isSystem = t.IsSystem;
                            bool flag2 = false;
                            return !( isSystem.GetValueOrDefault() == flag2 & isSystem.HasValue );
                        } );
                    return source;
                case BuildTypes.OrderLog:
                    IMarketDataStorage<ExecutionMessage> logMessageStorage = StorageRegistry.GetOrderLogMessageStorage( securityId, Drive, StorageFormat );
                    DateTime? nullable2 = from;
                    DateTimeOffset? from2 = nullable2.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable2.GetValueOrDefault() ) : new DateTimeOffset?();
                    nullable2 = to;
                    DateTimeOffset? to2 = nullable2.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable2.GetValueOrDefault() ) : new DateTimeOffset?();
                    IEnumerable<ExecutionMessage> executionMessages = logMessageStorage.Load( from2, to2 );
                    bool? isChecked2 = IsNonSystem.IsChecked;
                    bool flag3 = false;
                    if ( isChecked2.GetValueOrDefault() == flag3 & isChecked2.HasValue )
                        executionMessages = executionMessages.Where( i =>
                        {
                            bool? isSystem = i.IsSystem;
                            bool flag2 = false;
                            return !( isSystem.GetValueOrDefault() == flag2 & isSystem.HasValue );
                        } );
                    return executionMessages.ToTicks();
                case BuildTypes.Level1:
                    IMarketDataStorage<Level1ChangeMessage> level1MessageStorage = StorageRegistry.GetLevel1MessageStorage( securityId, Drive, StorageFormat );
                    DateTime? nullable3 = from;
                    DateTimeOffset? from3 = nullable3.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable3.GetValueOrDefault() ) : new DateTimeOffset?();
                    nullable3 = to;
                    DateTimeOffset? to3 = nullable3.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable3.GetValueOrDefault() ) : new DateTimeOffset?();
                    return level1MessageStorage.Load( from3, to3 ).ToTicks();
                case BuildTypes.FromComposites:
                    BasketMarketDataStorage<ExecutionMessage> marketDataStorage = new BasketMarketDataStorage<ExecutionMessage>();
                    BasketSecurity basket;
                    foreach ( SecurityId innerSecurityId in GetInnerSecurityIds( securityId, out basket ) )
                        marketDataStorage.InnerStorages.Add( StorageRegistry.GetTickMessageStorage( innerSecurityId, Drive, StorageFormat ) );
                    BasketMarketDataStorage<ExecutionMessage> storage = marketDataStorage;
                    DateTime? nullable4 = from;
                    DateTimeOffset? from4 = nullable4.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable4.GetValueOrDefault() ) : new DateTimeOffset?();
                    DateTime? nullable5 = to;
                    DateTimeOffset? to4 = nullable5.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable5.GetValueOrDefault() ) : new DateTimeOffset?();
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
            FindedTrades.Messages.Clear();
            Progress.Load( GetAllValues<ExecutionMessage>( new DateTime?(), new DateTime?(), false ), new Action<IEnumerable<ExecutionMessage>>( FindedTrades.Messages.AddRange ), 10000, null, null );
        }

        protected override bool CanDirectExport
        {
            get
            {
                return BuildFrom.SelectedIndex == 0;
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

        private void ShowChartClick( object sender, RoutedEventArgs e )
        {
            if ( !ValidateSettings() )
                return;
            foreach ( Security selectedSecurity in SelectedSecurities )
            {
                ChartPane chartPane = new ChartPane();
                MainWindow.Instance.ShowPane( chartPane );
                CandleSeries series = new CandleSeries( typeof( TickCandle ), selectedSecurity, 1 );
                chartPane.DrawCandles( series, GetValues<ExecutionMessage>( selectedSecurity.ToSecurityId( null, true, false ) ).ToCandles( series, null ).ToEntities<CandleMessage, Candle>( series.Security, null ), TimeZone.TimeZone );
            }
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            FindedTrades.Load( storage.GetValue<SettingsStorage>( "FindedTrades", null ) );
            BuildFrom.SelectedIndex = storage.GetValue( "BuildFrom", 0 );
            IsNonSystem.IsChecked = new bool?( storage.GetValue( "IsNonSystem", false ) );
            TimeZone.TimeZone = storage.GetValue( "TimeZone", TimeZone.TimeZone );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "FindedTrades", FindedTrades.Save() );
            storage.SetValue( "BuildFrom", BuildFrom.SelectedIndex );
            SettingsStorage settingsStorage = storage;
            bool? isChecked = IsNonSystem.IsChecked;
            bool flag = true;
            int num = isChecked.GetValueOrDefault() == flag & isChecked.HasValue ? 1 : 0;
            settingsStorage.SetValue( "IsNonSystem", num != 0 );
            storage.SetValue( "TimeZone", TimeZone.TimeZone );
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
