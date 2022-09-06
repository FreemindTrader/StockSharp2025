using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
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
    public partial class NewsPane : DataPane, IComponentConnector
    {
        private readonly IEnumerable<Security> _selectedSecurities = new Security[1] { TraderHelper.NewsSecurity };

        public NewsPane()
        {
            InitializeComponent();
            Init( ExportBtn, MainGrid, null, new Func<SecurityId, DateTime?, DateTime?, bool, IEnumerable>( GetNews ) );
        }

        protected override DataType DataType
        {
            get
            {
                return DataType.News;
            }
        }

        public override string Title
        {
            get
            {
                return LocalizedStrings.News;
            }
            set
            {
            }
        }

        protected override IEnumerable<Security> SelectedSecurities
        {
            get
            {
                return _selectedSecurities;
            }
        }

        private void OnDateValueChanged( object sender, EditValueChangedEventArgs e )
        {
            Progress.ClearStatus();
        }

        private IEnumerable<NewsMessage> GetNews(
          SecurityId securityId,
          DateTime? from,
          DateTime? to,
          bool exporting )
        {
            IEnumerable<NewsMessage> source = InternalGetNews( from, to );
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

        private IEnumerable<NewsMessage> InternalGetNews(
          DateTime? from,
          DateTime? to )
        {
            IMarketDataStorage<NewsMessage> newsMessageStorage = ServicesRegistry.StorageRegistry.GetNewsMessageStorage( Drive, StorageFormat );
            DateTime? nullable = from;
            DateTimeOffset? from1 = nullable.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable.GetValueOrDefault() ) : new DateTimeOffset?();
            nullable = to;
            DateTimeOffset? to1 = nullable.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable.GetValueOrDefault() ) : new DateTimeOffset?();
            return newsMessageStorage.Load( from1, to1 );
        }

        private void Find_OnClick( object sender, RoutedEventArgs e )
        {
            IListEx<NewsMessage> messages = NewsPanel.NewsGrid.Messages;
            messages.Clear();
            Progress.Load( GetAllValues<NewsMessage>( new DateTime?(), new DateTime?(), false ), new Action<IEnumerable<NewsMessage>>( messages.AddRange ), 1000, null, null );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            SettingsStorage storage1 = storage.GetValue<SettingsStorage>( "NewsPanel", null );
            if ( storage1 != null )
                NewsPanel.Load( storage1 );
            TimeZone.TimeZone = storage.GetValue( "TimeZone", TimeZone.TimeZone );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "NewsPanel", NewsPanel.Save() );
            storage.SetValue( "TimeZone", TimeZone.TimeZone );
        }

        
    }
}
