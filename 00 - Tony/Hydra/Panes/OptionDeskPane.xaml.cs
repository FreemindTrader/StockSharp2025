using DevExpress.Xpf.Core;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Editors;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Hydra.Controls;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Xaml;
using StockSharp.Xaml.Charting;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace StockSharp.Hydra.Panes
{
    [DisplayNameLoc( "OptionDesk" )]
    [VectorIcon( "Table" )]
    public partial class OptionDeskPane : DataPane, IComponentConnector
    {
        public static readonly RoutedCommand AddSecurityCommand = new RoutedCommand();
        public static readonly RoutedCommand RemoveSecurityCommand = new RoutedCommand();
        private readonly List<Level1ChangeMessage> _data = new List<Level1ChangeMessage>();
        private readonly BasketMarketDataStorage<Level1ChangeMessage> _storage = new BasketMarketDataStorage<Level1ChangeMessage>();
        private readonly ICollection<LineData<double>> _putBidSmile;
        private readonly ICollection<LineData<double>> _putAskSmile;
        private readonly ICollection<LineData<double>> _putLastSmile;
        private readonly ICollection<LineData<double>> _callBidSmile;
        private readonly ICollection<LineData<double>> _callAskSmile;
        private readonly ICollection<LineData<double>> _callLastSmile;
        private readonly Level1DataProvider _dataProvider;
        private readonly OptionDeskModel _model;
        private bool _isLoading;

        private static IEntityRegistry EntityRegistry
        {
            get
            {
                return ServicesRegistry.EntityRegistry;
            }
        }

        protected override DataType DataType
        {
            get
            {
                return DataType.Level1;
            }
        }

        protected override bool CanDirectExport
        {
            get
            {
                return false;
            }
        }

        private bool UseBlackModel
        {
            get
            {
                bool? isChecked = BlackModelCtrl.IsChecked;
                bool flag = true;
                return isChecked.GetValueOrDefault() == flag & isChecked.HasValue;
            }
            set
            {
                BlackModelCtrl.IsChecked = new bool?( value );
            }
        }

        public OptionDeskPane()
        {
            InitializeComponent();
            Init( ExportBtn, MainGrid, SelectSecurityBtn, new Func<SecurityId, DateTime?, DateTime?, bool, IEnumerable>( GetChanges ) );
            SetTitlePrefix( LocalizedStrings.OptionDesk );
            IsSingleSecurity = true;
            SelectSecurityBtn.InitTitle( LocalizedStrings.UnderlyingAsset + "..." );
            _putBidSmile = SmileChart.CreateSmile( "Put (B)", Colors.DarkRed, ChartIndicatorDrawStyles.Line, new Guid() );
            _putAskSmile = SmileChart.CreateSmile( "Put (A)", Colors.Red, ChartIndicatorDrawStyles.Line, new Guid() );
            _putLastSmile = SmileChart.CreateSmile( "Put (L)", Colors.OrangeRed, ChartIndicatorDrawStyles.Line, new Guid() );
            _callBidSmile = SmileChart.CreateSmile( "Call (B)", Colors.GreenYellow, ChartIndicatorDrawStyles.Line, new Guid() );
            _callAskSmile = SmileChart.CreateSmile( "Call (A)", Colors.DarkGreen, ChartIndicatorDrawStyles.Line, new Guid() );
            _callLastSmile = SmileChart.CreateSmile( "Call (L)", Colors.DarkOliveGreen, ChartIndicatorDrawStyles.Line, new Guid() );
            _dataProvider = new Level1DataProvider();
            _model = new OptionDeskModel()
            {
                MarketDataProvider = _dataProvider,
                ExchangeInfoProvider = ServicesRegistry.ExchangeInfoProvider
            };
            Desk.Model = _model;
            Level1Fields[ ] level1FieldsArray = new Level1Fields[6] { Level1Fields.ImpliedVolatility, Level1Fields.Delta, Level1Fields.Gamma, Level1Fields.Vega, Level1Fields.Theta, Level1Fields.Rho };
            Level1FieldsCtrl.SetItemsSource( level1FieldsArray, null, null );
            EvaluatedGreeks = level1FieldsArray;
        }

        private void FindClick( object sender, RoutedEventArgs e )
        {
            if ( !ValidateSettings() )
                return;
            _data.Clear();
            bool isFirstAdded = false;
            Progress.Load( GetAllValues<Level1ChangeMessage>( new DateTime?(), new DateTime?(), false ), new Action<IEnumerable<Level1ChangeMessage>>( _data.AddRange ), 5000, items =>
            {
                TimeSlider.Maximum = _data.Count - 1;
                if ( isFirstAdded )
                    return;
                DisplayChanges();
                isFirstAdded = true;
            }, null );
            TimeSlider.Maximum = 0.0;
            TimeSlider.Minimum = 0.0;
            TimeSlider.SmallChange = 1.0;
            TimeSlider.LargeChange = 5.0;
        }

        private void TimeSliderValueChanged( object sender, RoutedPropertyChangedEventArgs<double> e )
        {
            DisplayChanges();
        }

        private void Clear()
        {
            _model.Clear();
            ClearSmiles();
        }

        private void ClearSmiles()
        {
            _putBidSmile.Clear();
            _putAskSmile.Clear();
            _putLastSmile.Clear();
            _callBidSmile.Clear();
            _callAskSmile.Clear();
            _callLastSmile.Clear();
        }

        private void SelectSecurityBtn_SecuritySelected()
        {
            if ( _isLoading )
                return;
            Clear();
            if ( SelectedSecurity != null )
            {
                _model.UnderlyingAsset = SelectedSecurity;
                IStorageSecurityList securities = EntityRegistry.Securities;
                Progress.Load( securities.Where( s => s.UnderlyingSecurityId == SelectedSecurity.Id ), ss => ss.ForEach( new Action<Security>( _model.Add ) ), ( ( ISecurityProvider )securities ).Count, null, LocalizedStrings.Str2834 );
            }
            else
                _model.UnderlyingAsset = null;
        }

        private void Level1FieldsCtrl_OnEditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            if ( _isLoading )
                return;
            _model.EvaluateFields.Clear();
            _model.EvaluateFields.AddRange( EvaluatedGreeks );
        }

        private void BlackModelCtrl_OnEditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            if ( _isLoading )
                return;
            _model.UseBlackModel = UseBlackModel;
        }

        protected override void ExportBtnOnExportStarted()
        {
            if ( !GetChanges( SelectedSecurity.ToSecurityId( null, true, false ), From, To, true ).Any() )
            {
                Progress.DoesntExist();
            }
            else
            {
                StorageFormats storageFormat = StorageFormat;
                object path = ExportBtn.GetPath( SelectedSecurity, "options", DataType, From, To, Drive, ref storageFormat );
                if ( path == null )
                    return;
                Level1DataProvider dataProvider = new Level1DataProvider();
                OptionDeskModel model = new OptionDeskModel() { MarketDataProvider = dataProvider, ExchangeInfoProvider = ServicesRegistry.ExchangeInfoProvider, UnderlyingAsset = _model.UnderlyingAsset };
                foreach ( Security option in _model.Options )
                    model.Add( option );
                Progress.Start( SelectedSecurities.ToArray(), DataType.Level1, ( s1, f, t ) => GetChanges( s1, f, t, true ).SelectMany( l1Msg =>
                {
                    dataProvider.Process( l1Msg );
                    model.Refresh( new DateTimeOffset?( l1Msg.ServerTime ), new Decimal?() );
                    return model.Rows.SelectMany( r => ( new Level1ChangeMessage[2] { r.Call?.ToLevel1(), r.Put?.ToLevel1() } ).Where( l => l != null ) ).Select( l =>
            {
                l.ServerTime = l1Msg.ServerTime;
                return l;
            } ).ToArray();
                } ), int.MaxValue, path, From, To, false, null, StorageFormat );
            }
        }

        private IEnumerable<Level1Fields> EvaluatedGreeks
        {
            get
            {
                return Level1FieldsCtrl.SelectedFields;
            }
            set
            {
                Level1FieldsCtrl.SelectedFields = value;
            }
        }

        public override void Load( SettingsStorage storage )
        {
            try
            {
                _isLoading = true;
                Clear();
                base.Load( storage );
                Desk.Load( storage.GetValue<SettingsStorage>( "Desk", null ) );
                EvaluatedGreeks = storage.GetValue( "EvaluatedGreeks", Enumerable.Empty<Level1Fields>() );
                UseBlackModel = storage.GetValue( "UseBlackModel", UseBlackModel );
                TimeZone.TimeZone = storage.GetValue( "TimeZone", TimeZone.TimeZone );
                IEntityRegistry registry = EntityRegistry;
                Security[ ] array = storage.GetValue( "Options", Enumerable.Empty<string>() ).Select( id => registry.Securities.LookupById( id ) ).ToArray();
                _model.UseBlackModel = UseBlackModel;
                _model.UnderlyingAsset = SelectedSecurity;
                _model.EvaluateFields.Clear();
                _model.EvaluateFields.AddRange( EvaluatedGreeks );
                Action<Security> action = new Action<Security>( _model.Add );
                array.ForEach( action );
                UpdateTitle();
            }
            finally
            {
                _isLoading = false;
            }
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "Desk", Desk.Save() );
            storage.SetValue( "EvaluatedGreeks", EvaluatedGreeks.ToArray() );
            storage.SetValue( "Options", _model.Options.Select( p => p.Id ).ToArray() );
            storage.SetValue( "UseBlackModel", UseBlackModel );
            storage.SetValue( "TimeZone", TimeZone.TimeZone );
        }

        private IEnumerable<Level1ChangeMessage> GetChanges(
          SecurityId securityId,
          DateTime? from,
          DateTime? to,
          bool exporting )
        {
            IEnumerable<Level1ChangeMessage> source = InternalGetChanges( from, to );
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

        private IEnumerable<Level1ChangeMessage> InternalGetChanges(
          DateTime? from,
          DateTime? to )
        {
            IMarketDataDrive drive = Drive;
            StorageFormats storageFormat = StorageFormat;
            _storage.InnerStorages.Clear();
            _storage.InnerStorages.Add( StorageRegistry.GetLevel1MessageStorage( _model.UnderlyingAsset.ToSecurityId( null, true, false ), drive, storageFormat ) );
            foreach ( Security option in _model.Options )
                _storage.InnerStorages.Add( StorageRegistry.GetLevel1MessageStorage( option.ToSecurityId( null, true, false ), drive, storageFormat ) );
            BasketMarketDataStorage<Level1ChangeMessage> storage = _storage;
            DateTime? nullable1 = from;
            DateTimeOffset? from1 = nullable1.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable1.GetValueOrDefault() ) : new DateTimeOffset?();
            DateTime? nullable2 = to;
            DateTimeOffset? to1 = nullable2.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable2.GetValueOrDefault() ) : new DateTimeOffset?();
            return storage.Load( from1, to1 );
        }

        private void DisplayChanges()
        {
            int index = ( int )TimeSlider.Value;
            if ( _data.Count < index + 1 )
                return;
            Desk.BeginEndUpdate( () =>
            {
                _dataProvider.Clear();
                DateTimeOffset? currentTime = new DateTimeOffset?();
                for ( int index1 = 0; index1 < index; ++index1 )
                {
                    Level1ChangeMessage msg = _data[index1];
                    currentTime = new DateTimeOffset?( msg.ServerTime );
                    _dataProvider.Process( msg );
                }
                _model.Refresh( currentTime, new Decimal?() );
            } );
            ClearSmiles();
            foreach ( OptionDeskRow row in _model.Rows )
            {
                Decimal? strike = row.Strike;
                if ( strike.HasValue )
                {
                    TryAddSmileItem( _callBidSmile, strike.Value, row.Call?.ImpliedVolatilityBestBid );
                    TryAddSmileItem( _callAskSmile, strike.Value, row.Call?.ImpliedVolatilityBestAsk );
                    TryAddSmileItem( _callLastSmile, strike.Value, row.Call?.ImpliedVolatilityLastTrade );
                    TryAddSmileItem( _putBidSmile, strike.Value, row.Put?.ImpliedVolatilityBestBid );
                    TryAddSmileItem( _putAskSmile, strike.Value, row.Put?.ImpliedVolatilityBestAsk );
                    TryAddSmileItem( _putLastSmile, strike.Value, row.Put?.ImpliedVolatilityLastTrade );
                }
            }
            CurrentDate.Text = _data[index].ServerTime.ToString( "yyyy.MM.dd HH:mm:ss.fff" );
        }

        private static void TryAddSmileItem(
          ICollection<LineData<double>> smile,
          Decimal strike,
          Decimal? iv )
        {
            if ( !iv.HasValue )
                return;
            smile.Add( new LineData<double>()
            {
                X = ( double )strike,
                Y = iv.Value
            } );
        }

        private void AddSecurityCommand_OnCanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = SelectedSecurity != null;
        }

        private void AddSecurityCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            SecurityPickerWindow wnd = new SecurityPickerWindow() { SecurityProvider = SecurityProvider };
            wnd.ExcludeSecurities.Add( TraderHelper.AllSecurity );
            wnd.ExcludeSecurities.AddRange( _model.Options );
            if ( !wnd.ShowModal( this ) )
                return;
            wnd.SelectedSecurities.ForEach( new Action<Security>( _model.Add ) );
        }

        private void RemoveSecurityCommand_OnCanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = Desk?.SelectedItem != null;
        }

        private void RemoveSecurityCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            List<Security> securityList = new List<Security>();
            foreach ( OptionDeskRow optionDeskRow in Desk.SelectedItems.OfType<OptionDeskRow>().ToArray() )
            {
                if ( optionDeskRow.Call != null )
                    securityList.Add( optionDeskRow.Call.Option );
                if ( optionDeskRow.Put != null )
                    securityList.Add( optionDeskRow.Put.Option );
            }
            securityList.ForEach( new Action<Security>( _model.Remove ) );
        }

        

        private sealed class Level1DataProvider : IMarketDataProvider
        {
            private readonly Dictionary<SecurityId, Dictionary<Level1Fields, object>> _securities = new Dictionary<SecurityId, Dictionary<Level1Fields, object>>();

            event Action<Security, IEnumerable<KeyValuePair<Level1Fields, object>>, DateTimeOffset, DateTimeOffset> IMarketDataProvider.ValuesChanged
            {
                add
                {
                }
                remove
                {
                }
            }

            public void Process( Level1ChangeMessage msg )
            {
                if ( msg == null )
                    throw new ArgumentNullException( nameof( msg ) );
                Dictionary<Level1Fields, object> dictionary = _securities.SafeAdd( msg.SecurityId, k => new Dictionary<Level1Fields, object>() );
                foreach ( KeyValuePair<Level1Fields, object> change in ( IEnumerable<KeyValuePair<Level1Fields, object>> )msg.Changes )
                    dictionary[change.Key] = change.Value;
            }

            public void Clear()
            {
                _securities.Clear();
            }

            MarketDepth IMarketDataProvider.GetMarketDepth( Security security )
            {
                throw new NotSupportedException();
            }

            object IMarketDataProvider.GetSecurityValue(
              Security security,
              Level1Fields field )
            {
                Dictionary<Level1Fields, object> dict = _securities.TryGetValue( security.ToSecurityId( null, true, false ) );
                if ( dict == null )
                    return null;
                return dict.TryGetValue( field );
            }

            IEnumerable<Level1Fields> IMarketDataProvider.GetLevel1Fields(
              Security security )
            {
                Dictionary<Level1Fields, object> dictionary = _securities.TryGetValue( security.ToSecurityId( null, true, false ) );
                if ( dictionary == null )
                    return Enumerable.Empty<Level1Fields>();
                return dictionary.Keys.ToArray();
            }

            event Action<Trade> IMarketDataProvider.NewTrade
            {
                add
                {
                    throw new NotSupportedException();
                }
                remove
                {
                    throw new NotSupportedException();
                }
            }

            event Action<Security> IMarketDataProvider.NewSecurity
            {
                add
                {
                    throw new NotSupportedException();
                }
                remove
                {
                    throw new NotSupportedException();
                }
            }

            event Action<Security> IMarketDataProvider.SecurityChanged
            {
                add
                {
                    throw new NotSupportedException();
                }
                remove
                {
                    throw new NotSupportedException();
                }
            }

            event Action<MarketDepth> IMarketDataProvider.NewMarketDepth
            {
                add
                {
                    throw new NotSupportedException();
                }
                remove
                {
                    throw new NotSupportedException();
                }
            }

            event Action<MarketDepth> IMarketDataProvider.MarketDepthChanged
            {
                add
                {
                    throw new NotSupportedException();
                }
                remove
                {
                    throw new NotSupportedException();
                }
            }

            event Action<MarketDepth> IMarketDataProvider.FilteredMarketDepthChanged
            {
                add
                {
                    throw new NotSupportedException();
                }
                remove
                {
                    throw new NotSupportedException();
                }
            }

            event Action<OrderLogItem> IMarketDataProvider.NewOrderLogItem
            {
                add
                {
                    throw new NotSupportedException();
                }
                remove
                {
                    throw new NotSupportedException();
                }
            }

            event Action<News> IMarketDataProvider.NewNews
            {
                add
                {
                    throw new NotSupportedException();
                }
                remove
                {
                    throw new NotSupportedException();
                }
            }

            event Action<News> IMarketDataProvider.NewsChanged
            {
                add
                {
                    throw new NotSupportedException();
                }
                remove
                {
                    throw new NotSupportedException();
                }
            }

            event Action<SecurityLookupMessage, IEnumerable<Security>, Exception> IMarketDataProvider.LookupSecuritiesResult
            {
                add
                {
                    throw new NotSupportedException();
                }
                remove
                {
                    throw new NotSupportedException();
                }
            }

            event Action<SecurityLookupMessage, IEnumerable<Security>, IEnumerable<Security>, Exception> IMarketDataProvider.LookupSecuritiesResult2
            {
                add
                {
                    throw new NotSupportedException();
                }
                remove
                {
                    throw new NotSupportedException();
                }
            }

            event Action<BoardLookupMessage, IEnumerable<ExchangeBoard>, Exception> IMarketDataProvider.LookupBoardsResult
            {
                add
                {
                    throw new NotSupportedException();
                }
                remove
                {
                    throw new NotSupportedException();
                }
            }

            event Action<BoardLookupMessage, IEnumerable<ExchangeBoard>, IEnumerable<ExchangeBoard>, Exception> IMarketDataProvider.LookupBoardsResult2
            {
                add
                {
                    throw new NotSupportedException();
                }
                remove
                {
                    throw new NotSupportedException();
                }
            }

            event Action<TimeFrameLookupMessage, IEnumerable<TimeSpan>, Exception> IMarketDataProvider.LookupTimeFramesResult
            {
                add
                {
                    throw new NotSupportedException();
                }
                remove
                {
                    throw new NotSupportedException();
                }
            }

            event Action<TimeFrameLookupMessage, IEnumerable<TimeSpan>, IEnumerable<TimeSpan>, Exception> IMarketDataProvider.LookupTimeFramesResult2
            {
                add
                {
                    throw new NotSupportedException();
                }
                remove
                {
                    throw new NotSupportedException();
                }
            }

            event Action<Security, MarketDataMessage> IMarketDataProvider.MarketDataSubscriptionSucceeded
            {
                add
                {
                    throw new NotSupportedException();
                }
                remove
                {
                    throw new NotSupportedException();
                }
            }

            event Action<Security, MarketDataMessage, Exception> IMarketDataProvider.MarketDataSubscriptionFailed
            {
                add
                {
                    throw new NotSupportedException();
                }
                remove
                {
                    throw new NotSupportedException();
                }
            }

            event Action<Security, MarketDataMessage, SubscriptionResponseMessage> IMarketDataProvider.MarketDataSubscriptionFailed2
            {
                add
                {
                    throw new NotSupportedException();
                }
                remove
                {
                    throw new NotSupportedException();
                }
            }

            event Action<Security, MarketDataMessage> IMarketDataProvider.MarketDataUnSubscriptionSucceeded
            {
                add
                {
                    throw new NotSupportedException();
                }
                remove
                {
                    throw new NotSupportedException();
                }
            }

            event Action<Security, MarketDataMessage, Exception> IMarketDataProvider.MarketDataUnSubscriptionFailed
            {
                add
                {
                    throw new NotSupportedException();
                }
                remove
                {
                    throw new NotSupportedException();
                }
            }

            event Action<Security, MarketDataMessage, SubscriptionResponseMessage> IMarketDataProvider.MarketDataUnSubscriptionFailed2
            {
                add
                {
                    throw new NotSupportedException();
                }
                remove
                {
                    throw new NotSupportedException();
                }
            }

            event Action<Security, SubscriptionFinishedMessage> IMarketDataProvider.MarketDataSubscriptionFinished
            {
                add
                {
                    throw new NotSupportedException();
                }
                remove
                {
                    throw new NotSupportedException();
                }
            }

            event Action<Security, MarketDataMessage, Exception> IMarketDataProvider.MarketDataUnexpectedCancelled
            {
                add
                {
                    throw new NotSupportedException();
                }
                remove
                {
                    throw new NotSupportedException();
                }
            }

            event Action<Security, MarketDataMessage> IMarketDataProvider.MarketDataSubscriptionOnline
            {
                add
                {
                    throw new NotSupportedException();
                }
                remove
                {
                    throw new NotSupportedException();
                }
            }

            MarketDepth IMarketDataProvider.GetFilteredMarketDepth(
              Security security )
            {
                throw new NotSupportedException();
            }
        }
    }
}
