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
    public partial class OrderLogPane : DataPane, IComponentConnector
    {
        public OrderLogPane()
        {
            InitializeComponent();
            FindedOrderLog.HideColumns( ExecutionTypes.OrderLog );
            Init( ExportBtn, MainGrid, SelectSecurityBtn, new Func<SecurityId, DateTime?, DateTime?, bool, IEnumerable>( GetOrderLog ) );
            SetTitlePrefix( LocalizedStrings.OrderLog );
        }

        protected override DataType DataType
        {
            get
            {
                return DataType.OrderLog;
            }
        }

        private IEnumerable<ExecutionMessage> GetOrderLog(
          SecurityId securityId,
          DateTime? from,
          DateTime? to,
          bool exporting )
        {
            IEnumerable<ExecutionMessage> source = InternalGetOrderLog( securityId, from, to );
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

        private IEnumerable<ExecutionMessage> InternalGetOrderLog(
          SecurityId securityId,
          DateTime? from,
          DateTime? to )
        {
            IMarketDataStorage<ExecutionMessage> logMessageStorage = StorageRegistry.GetOrderLogMessageStorage( securityId, Drive, StorageFormat );
            DateTime? nullable = from;
            DateTimeOffset? from1 = nullable.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable.GetValueOrDefault() ) : new DateTimeOffset?();
            nullable = to;
            DateTimeOffset? to1 = nullable.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable.GetValueOrDefault() ) : new DateTimeOffset?();
            IEnumerable<ExecutionMessage> source = logMessageStorage.Load( from1, to1 );
            bool? isChecked = IsNonSystem.IsChecked;
            bool flag1 = false;
            if ( isChecked.GetValueOrDefault() == flag1 & isChecked.HasValue )
                source = source.Where( o =>
                {
                    bool? isSystem = o.IsSystem;
                    bool flag2 = false;
                    return !( isSystem.GetValueOrDefault() == flag2 & isSystem.HasValue );
                } );
            return source;
        }

        private void FindClick( object sender, RoutedEventArgs e )
        {
            if ( !ValidateSettings() )
                return;
            FindedOrderLog.Messages.Clear();
            Progress.Load( GetAllValues<ExecutionMessage>( new DateTime?(), new DateTime?(), false ), new Action<IEnumerable<ExecutionMessage>>( FindedOrderLog.Messages.AddRange ), 10000, null, null );
        }

        private void OnDateValueChanged( object sender, EditValueChangedEventArgs e )
        {
            Progress.ClearStatus();
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
                chartPane.DrawCandles( series, GetValues<ExecutionMessage>( selectedSecurity.ToSecurityId( null, true, false ) ).ToTicks().ToCandles( series, null ).ToEntities<CandleMessage, Candle>( series.Security, null ), TimeZone.TimeZone );
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
                chartPane.DrawLines( selectedSecurityId, GetValues<ExecutionMessage>( selectedSecurityId ).ToOrderBooks( OrderLogBuilder.SelectedBuilder.CreateOrderLogMarketDepthBuilder( selectedSecurityId ), new TimeSpan(), int.MaxValue ).BuildIfNeed( null ), TimeZone.TimeZone );
            }
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            FindedOrderLog.Load( storage.GetValue<SettingsStorage>( "FindedOrderLog", null ) );
            IsNonSystem.IsChecked = new bool?( storage.GetValue( "IsNonSystem", false ) );
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
            storage.SetValue( "FindedOrderLog", FindedOrderLog.Save() );
            SettingsStorage settingsStorage = storage;
            bool? isChecked = IsNonSystem.IsChecked;
            bool flag = true;
            int num = isChecked.GetValueOrDefault() == flag & isChecked.HasValue ? 1 : 0;
            settingsStorage.SetValue( "IsNonSystem", num != 0 );
            storage.SetValue( "TimeZone", TimeZone.TimeZone );
            storage.SetValue( "OrderLogBuilder", OrderLogBuilder.SelectedBuilder.GetTypeName( false ) );
        }

        
    }
}
