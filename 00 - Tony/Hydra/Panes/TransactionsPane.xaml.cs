using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo.Storages;
using StockSharp.Hydra.Controls;
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
    public partial class TransactionsPane : DataPane, IComponentConnector
    {        
        public TransactionsPane()
        {
            InitializeComponent();
            Init( ExportBtn, MainGrid, SelectSecurityBtn, new Func<SecurityId, DateTime?, DateTime?, bool, IEnumerable>( GetTransactions ) );
            SetTitlePrefix( LocalizedStrings.Transactions );
        }

        protected override DataType DataType
        {
            get
            {
                return DataType.Transactions;
            }
        }

        private IEnumerable<ExecutionMessage> GetTransactions(
          SecurityId securityId,
          DateTime? from,
          DateTime? to,
          bool exporting )
        {
            IEnumerable<ExecutionMessage> source = InternalGetTransactions( securityId, from, to );
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

        private IEnumerable<ExecutionMessage> InternalGetTransactions(
          SecurityId securityId,
          DateTime? from,
          DateTime? to )
        {
            IMarketDataStorage<ExecutionMessage> transactionStorage = StorageRegistry.GetTransactionStorage( securityId, Drive, StorageFormat );
            DateTime? nullable = from;
            DateTimeOffset? from1 = nullable.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable.GetValueOrDefault() ) : new DateTimeOffset?();
            nullable = to;
            DateTimeOffset? to1 = nullable.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable.GetValueOrDefault() ) : new DateTimeOffset?();
            return transactionStorage.Load( from1, to1 );
        }

        private void OnDateValueChanged( object sender, EditValueChangedEventArgs e )
        {
            Progress.ClearStatus();
        }

        private void FindClick( object sender, RoutedEventArgs e )
        {
            if ( !ValidateSettings() )
                return;
            FindedTransactions.Messages.Clear();
            Progress.Load( GetAllValues<ExecutionMessage>( new DateTime?(), new DateTime?(), false ), new Action<IEnumerable<ExecutionMessage>>( FindedTransactions.Messages.AddRange ), 10000, null, null );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            FindedTransactions.Load( storage.GetValue<SettingsStorage>( "FindedTransactions", null ) );
            TimeZone.TimeZone = storage.GetValue( "TimeZone", TimeZone.TimeZone );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "FindedTransactions", FindedTransactions.Save() );
            storage.SetValue( "TimeZone", TimeZone.TimeZone );
        }        
    }
}
