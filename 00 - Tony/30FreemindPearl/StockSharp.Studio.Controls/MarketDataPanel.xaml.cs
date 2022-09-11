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
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Core.Commands;
using StockSharp.Studio.Core.Services;
using StockSharp.Xaml;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;

#pragma warning disable CS0168
#pragma warning disable CS1998

namespace StockSharp.Studio.Controls
{
    [DisplayNameLoc( "MarketData" )]
    [DescriptionLoc( "MarketData", false )]
    [Guid( "B5CA7FCD-601E-41C8-8C94-4F57855F64E0" )]
    [VectorIcon( "Storage" )]
    [Doc( "topics/Designer_Repository_of_historical_data.html" )]
    public partial class MarketDataPanel : BaseStudioControl, IStudioCommandScope, IComponentConnector
    {
        private class SelectableObject : NotifiableObject
        {
            private bool _isSelected;

            public SelectableObject( DataType value, TimeSpan timeFrame )
            {
                DataType dataType = value;
                if ( dataType == null )
                {
                    throw new ArgumentNullException( nameof( value ) );
                }

                Value = dataType;
                TimeFrame = timeFrame;
            }

            public bool IsSelected
            {
                get => _isSelected;
                set
                {
                    _isSelected = value;
                    NotifyChanged( nameof( IsSelected ) );
                }
            }

            public TimeSpan TimeFrame { get; }

            public DataType Value { get; }
        }

        private class ProgressData : DevExpress.Mvvm.ViewModelBase
        {
            public double Maximum { get => GetProperty( () => Maximum ); set => SetProperty( () => Maximum, value ); }

            public double Minimum { get => GetProperty( () => Minimum ); set => SetProperty( () => Minimum, value ); }

            public BaseEditStyleSettings Settings
            {
                get => GetProperty( () => Settings );
                set => SetProperty( () => Settings, value );
            }

            public string Title { get => GetProperty( () => Title ); set => SetProperty( () => Title, value ); }

            public double Value { get => GetProperty( () => Value ); set => SetProperty( () => Value, value ); }
        }

        private class DataInfo
        {
            public DataInfo( MarketDataMessage request, IMarketDataDrive drive, StorageFormats format )
            {
                MarketDataMessage marketDataMessage = request;
                if ( marketDataMessage == null )
                {
                    throw new ArgumentNullException( nameof( request ) );
                }

                Request = marketDataMessage;
                IMarketDataDrive marketDataDrive = drive;
                if ( marketDataDrive == null )
                {
                    throw new ArgumentNullException( nameof( drive ) );
                }

                Drive = marketDataDrive;
                Format = format;
                Messages = new List<Message>();
            }

            public IMarketDataDrive Drive { get; }

            public StorageFormats Format { get; }

            public List<Message> Messages { get; }

            public MarketDataMessage Request { get; }
        }

        private class DownloadInfo
        {
            public IMarketDataDrive Drive { get; set; }

            public StorageFormats Format { get; set; }

            public MarketDataMessage[ ] Requests { get; set; }
        }

        public static RoutedCommand SelectSecurityCommand = new RoutedCommand();
        public static RoutedCommand UnselectSecurityCommand = new RoutedCommand();
        public static RoutedCommand CancelCommand = new RoutedCommand();
        public static RoutedCommand DownloadCommand = new RoutedCommand();
        public static RoutedCommand EditSecurityCommand = new RoutedCommand();
        public static RoutedCommand DeleteSecurityCommand = new RoutedCommand();
        private readonly ProgressData _progressData = new ProgressData();
        private readonly Tuple<string, object> _storageDataSource = new Tuple<string, object>( LocalizedStrings.Str1405, LocalizedStrings.Str1405 );
        private readonly SecurityTypes[ ] _defaultSecurityTypes = new SecurityTypes[5] { SecurityTypes.Currency, SecurityTypes.Index, SecurityTypes.Stock, SecurityTypes.Future, SecurityTypes.CryptoCurrency };
        private CancellationTokenSource _downloadCts;
        private readonly SelectableObject[ ] _allCandleTimeFrames;
        private readonly Connector _connector;
        private int _numSecuritiesBeforeLookup;
        private bool _isCancelled;
        private bool _isLoading;
        private SecurityLookupMessage _securityFilter;
        private Tuple<string, object> _dataSource;
        private IExtendedInfoStorageItem _extendedStorage;
        private SettingsStorage _mappingSecurityWindowSettings;
        private Security _lastSelectedSecurity;


        bool IStudioCommandScope.UseParentScope
        {
            get
            {
                return false;
            }
        }

        bool IStudioCommandScope.RouteToGlobalScope
        {
            get
            {
                return false;
            }
        }

        public bool AutoSearchSecurities { get; set; }

        public MarketDataPanel()
        {
            InitializeComponent();
            TimeFrames.ItemsSource = _allCandleTimeFrames = ( new int[7]
            {
        1,
        5,
        10,
        15,
        30,
        60,
        1440
            } ).Select( i =>
          {
              TimeSpan timeSpan = TimeSpan.FromMinutes( i );
              SelectableObject selectableObject = new SelectableObject( DataType.TimeFrame( timeSpan ), timeSpan );
              if ( timeSpan == TimeSpan.FromMinutes( 5.0 ) )
                  selectableObject.IsSelected = true;
              return selectableObject;
          } ).ToArray();
            SecuritiesAll.GridChanged += RaiseChangedCommand;
            SecuritiesAll.SecurityDoubleClick += security =>
              {
                  if ( security == null )
                      return;
                  SelectSecurities( new Security[1] { security } );
              };
            SecuritiesSelected.GridChanged += RaiseChangedCommand;
            SecuritiesSelected.SecurityDoubleClick += security =>
              {
                  if ( security == null )
                      return;
                  UnselectSecurities( new Security[1] { security } );
              };
            SecuritiesAll.SecurityProvider = SecurityProvider;
            SelectedDrive = ServicesRegistry.DriveCache.DefaultDrive;
            DateTime today = DateTime.Today;
            DateTimeFrom.NullValue = DateTime.MinValue;
            DateTimeFrom.DateTime = today.AddDays( -365.0 );
            DateTimeTo.NullValue = DateTime.MinValue;
            DateTimeTo.DateTime = today;
            
            // Tony
            //this.TimeoutSeconds.Value = ( Decimal )MarketDataPanel.DataDownloader.DefaultDataTimeout.TotalSeconds.To<int>();

            TimeoutSeconds.Value = 10;
            StorageFormat = StorageFormats.Binary;
            BusyIndicator.BusyContent = _progressData;
            MarketDataGrid.LayoutChanged += RaiseChangedCommand;
            MarketDataGrid.DataLoading += () => BusyIndicator1.IsBusy = true;
            MarketDataGrid.DataLoaded += () => BusyIndicator1.IsBusy = false;
            _connector = Connector;
            ExtendedInfoStorage.Deleted += new Action<IExtendedInfoStorageItem>( OnExtendedInfoStorageDeleted );
        }

        public IMarketDataDrive SelectedDrive
        {
            get
            {
                return DriveCtrl.SelectedDrive;
            }
            set
            {
                DriveCtrl.SelectedDrive = value;
            }
        }

        public StorageFormats StorageFormat
        {
            get
            {
                return FormatCtrl.SelectedFormat;
            }
            set
            {
                FormatCtrl.SelectedFormat = value;
            }
        }

        private static IExtendedInfoStorage ExtendedInfoStorage
        {
            get
            {
                return ServicesRegistry.ExtendedInfoStorage;
            }
        }

        private static ISecurityStorage SecurityStorage
        {
            get
            {
                return ServicesRegistry.SecurityStorage;
            }
        }

        public override CloseAction CanClose( CloseReason reason )
        {
            return reason == CloseReason.Shutdown || !BusyIndicator.IsBusy ? CloseAction.Close : CloseAction.StayOpen;
        }

        private void ExecutedSelectSecurity( object sender, ExecutedRoutedEventArgs e )
        {
            SelectSecurities( SecuritiesAll.SelectedSecurities.ToArray() );
        }

        private void CanExecuteSelectSecurity( object sender, CanExecuteRoutedEventArgs e )
        {
            CanExecuteRoutedEventArgs executeRoutedEventArgs = e;
            SecurityPicker securitiesAll = SecuritiesAll;
            int num = securitiesAll != null ? ( securitiesAll.SelectedSecurities.Any() ? 1 : 0 ) : 0;
            executeRoutedEventArgs.CanExecute = num != 0;
        }

        private void ExecutedUnselectSecurity( object sender, ExecutedRoutedEventArgs e )
        {
            UnselectSecurities( SecuritiesSelected.SelectedSecurities.ToArray() );
        }

        private void CanExecuteUnselectSecurity( object sender, CanExecuteRoutedEventArgs e )
        {
            CanExecuteRoutedEventArgs executeRoutedEventArgs = e;
            SecurityPicker securitiesSelected = SecuritiesSelected;
            int num = securitiesSelected != null ? ( securitiesSelected.SelectedSecurities.Any() ? 1 : 0 ) : 0;
            executeRoutedEventArgs.CanExecute = num != 0;
        }

        private async void ExecutedDownload( object sender, ExecutedRoutedEventArgs args )
        {
            MarketDataPanel marketDataPanel = this;
            try
            {
                await marketDataPanel.ExecutedDownloadAsync();
            }
            catch ( OperationCanceledException ex )
            {
                int num = ( int )new MessageBoxBuilder().Warning().Owner( marketDataPanel ).Caption( LocalizedStrings.Warning ).Text( LocalizedStrings.OperationCanceled ).Show();
            }
            catch ( Exception ex )
            {
                int num = ( int )new MessageBoxBuilder().Error().Owner( marketDataPanel ).Caption( LocalizedStrings.Str152 ).Text( ex.ToString() ).Show();
            }
        }

        private async Task ExecutedDownloadAsync()
        {
            MarketDataPanel parent = this;
            parent.RaiseChangedCommand();
            DataSourceWindow wnd = new DataSourceWindow();
            wnd.Configure = () => ConfigureConnector( wnd.DataSourceItemsSource, false );
            parent.FillDataSources( wnd.DataSourceItemsSource, false );
            Security[ ] array1 = parent.SecuritiesSelected.Securities.LookupAll().ToArray();
            ExchangeBoard board = array1[0].Board;
            if ( array1.Length == 1 || board != null && array1.All( s => s.Board == board ) )
            {
                Guid? secAdapter = ServicesRegistry.SecurityAdapterProvider.TryGetAdapter( array1[0].ToSecurityId( null, true, false ), null );
                if ( secAdapter.HasValue )
                    wnd.SelectedDataSource = wnd.DataSourceItemsSource.FirstOrDefault( t =>
                       {
                           IMessageAdapter messageAdapter = t.Item2 as IMessageAdapter;
                           if ( messageAdapter == null )
                               return !secAdapter.HasValue;
                           Guid id = messageAdapter.Id;
                           Guid? nullable = secAdapter;
                           if ( !nullable.HasValue )
                               return false;
                           return id == nullable.GetValueOrDefault();
                       } );
            }
            if ( !wnd.ShowModal( parent ) )
                return;
            DateTime dateTime1 = parent.DateTimeFrom.DateTime;
            DateTime dateTime2 = parent.DateTimeTo.DateTime;
            IEnumerable<DataType> dataTypes = parent._allCandleTimeFrames.Where( t => t.IsSelected ).Select( t => t.Value );
            bool? isChecked = parent.Ticks.IsChecked;
            bool flag = true;
            if ( isChecked.GetValueOrDefault() == flag & isChecked.HasValue )
                dataTypes = dataTypes.Concat( new DataType[1]
                {
          DataType.Ticks
                } );
            IEnumerable<DataType> array2 = dataTypes.ToArray();
            IMessageAdapter messageAdapter1 = wnd.SelectedDataSource.Item2 as IMessageAdapter;
            List<Subscription> source = new List<Subscription>();
            foreach ( Security security in array1 )
            {
                foreach ( DataType dataType in array2 )
                {
                    Subscription subscription = new Subscription( dataType, security );
                    subscription.SubscriptionMessage.From = new DateTimeOffset?( ( DateTimeOffset )dateTime1 );
                    subscription.SubscriptionMessage.To = new DateTimeOffset?( ( DateTimeOffset )dateTime2 );
                    subscription.SubscriptionMessage.Adapter = messageAdapter1;
                    source.Add( subscription );
                }
            }
            if ( source.IsEmpty() )
            {
                int num = ( int )new MessageBoxBuilder().Owner( parent ).Text( LocalizedStrings.XamlStr344 ).Show();
            }
            else
            {
                if ( !parent.CheckConnectionState() )
                    return;
                parent._isCancelled = false;
                try
                {
                    parent.BusyIndicator.IsBusy = true;
                    parent.SetProgressSettings( LocalizedStrings.XamlStr189, new ProgressBarMarqueeStyleSettings() );
                    parent._downloadCts = new CancellationTokenSource();
                    
                    // TOny
                    //using ( MarketDataPanel.DataDownloader downloader = new MarketDataPanel.DataDownloader( parent, ( IEnumerable<Subscription> )source, parent._downloadCts.Token ) )
                    //    await downloader.DownloadTask;
                }
                finally
                {
                    parent.BusyIndicator.IsBusy = false;
                    parent.RefreshGrid( parent._lastSelectedSecurity );
                }
            }
        }

        private void CanExecuteDownload( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = SecuritiesSelected.Securities.Count > 0;
        }

        private void ExecutedCancel( object sender, ExecutedRoutedEventArgs e )
        {
            _isCancelled = true;
            _downloadCts?.Cancel();
        }

        private void CanExecuteCancel( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = !_isCancelled;
        }

        private void CreateRegular( object sender, RoutedEventArgs e )
        {
            new CreateSecurityCommand( typeof( Security ) ).Process( this, true );
        }

        private void CreateIndex( object sender, RoutedEventArgs e )
        {
            new CreateSecurityCommand( typeof( IndexSecurity ) ).Process( this, true );
        }

        private void CreateContinuous( object sender, RoutedEventArgs e )
        {
            new CreateSecurityCommand( typeof( ContinuousSecurity ) ).Process( this, true );
        }

        private void EditSecurityCommandExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            new EditSecuritiesCommand( SecuritiesAll.SelectedSecurities ).Process( this, true );
        }

        private void EditSecurityCommandCanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = SecuritiesAll?.SelectedSecurity != null;
        }

        private void DeleteSecurityCommandExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            new RemoveSecuritiesCommand( SecuritiesAll.SelectedSecurities.ToArray() ).Process( this, true );
        }

        private void DeleteSecurityCommandCanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = SecuritiesAll?.SelectedSecurity != null;
        }

        public void SelectSecurities( Security[ ] securities )
        {
            SecuritiesSelected.Securities.AddRange( securities );
            SecuritiesAll.ExcludeSecurities.AddRange( securities );
            RaiseChangedCommand();
        }

        private void UnselectSecurities( Security[ ] securities )
        {
            SecuritiesSelected.Securities.RemoveRange( securities );
            SecuritiesAll.ExcludeSecurities.RemoveRange( securities );
            RaiseChangedCommand();
        }

        private void DownloadSecurities_OnClick( object sender, RoutedEventArgs e )
        {
            SecurityLookupWindow wnd = new SecurityLookupWindow() { ExchangeInfoProvider = ExchangeInfoProvider, ShowDataSourcePanel = true };
            wnd.Configure = () => ConfigureConnector( wnd.DataSourceItemsSource, true );
            FillDataSources( wnd.DataSourceItemsSource, true );
            if ( !wnd.ShowModal( this ) )
                return;
            Tuple<string, object> selectedDataSource = wnd.SelectedDataSource;
            if ( !OnCanLookup( selectedDataSource ) )
                return;
            _isCancelled = false;
            _numSecuritiesBeforeLookup = SecurityProvider.Count;
            BusyIndicator.IsBusy = true;
            SetProgressSettings( LocalizedStrings.Str3657, new ProgressBarMarqueeStyleSettings() );
            SecurityLookupMessage criteriaMessage = wnd.CriteriaMessage;
            if ( Equals( selectedDataSource, _storageDataSource ) )
                OnStorageLookup( criteriaMessage );
            else
                OnConnectorLookup( criteriaMessage, selectedDataSource );
        }

        private bool OnCanLookup( Tuple<string, object> type )
        {
            if ( Equals( type, _storageDataSource ) )
                return true;
            if ( !CanLookup )
            {
                int num = ( int )new MessageBoxBuilder().Owner( this ).Warning().Text( LocalizedStrings.LookupSecuritiesNotSupported ).Show();
                return false;
            }
            return CheckConnectionState();
        }

        private bool CheckConnectionState()
        {
            if ( _connector.ConnectionState == ConnectionStates.Connected )
                return true;
            return new MessageBoxBuilder().Owner( this ).Question().Text( "{0} {1}?".Put( LocalizedStrings.NoActiveConnection, LocalizedStrings.Connect ) ).YesNo().Show() == MessageBoxResult.Yes;
        }

        private void ConfigureConnector( ICollection<Tuple<string, object>> items, bool addStorageSource )
        {
            if ( _connector.ConnectionState == ConnectionStates.Connected )
            {
                if ( new MessageBoxBuilder().Owner( this ).Question().Text( "{0} {1}?".Put( LocalizedStrings.Str1555, LocalizedStrings.Disconnect ) ).YesNo().Show() != MessageBoxResult.Yes )
                    return;
                _connector.Disconnect();
            }
            ConfigureConnectorCommand command = new ConfigureConnectorCommand();
            command.Process( this, true );
            if ( !command.Result )
                return;
            FillDataSources( items, addStorageSource );
        }

        private void OnConnectorConnectionError( Exception error )
        {
            OnComplete( error.Message, true );
        }

        private bool CanLookup
        {
            get
            {
                IMessageAdapter marketDataAdapter = _connector.MarketDataAdapter;
                if ( marketDataAdapter == null )
                    return false;
                return marketDataAdapter.IsMessageSupported( MessageTypes.SecurityLookup );
            }
        }

        private void OnConnectorLookup( SecurityLookupMessage filter, Tuple<string, object> dataSource )
        {
            _securityFilter = filter;
            _dataSource = dataSource;
            _connector.LookupSecuritiesResult2 += new Action<SecurityLookupMessage, IEnumerable<Security>, IEnumerable<Security>, Exception>( OnLookupConnectorLookupSecuritiesResult );
            if ( _connector.ConnectionState != ConnectionStates.Connected )
            {
                _connector.Connected += new Action( OnLookupConnectorConnected );
                _connector.ConnectionError += new Action<Exception>( OnConnectorConnectionError );
                _connector.Connect();
            }
            else
                OnLookupConnectorConnected();
        }

        private void OnLookupConnectorConnected()
        {
            SecurityLookupMessage criteria = _securityFilter.TypedClone();
            criteria.Adapter = ( IMessageAdapter )_dataSource?.Item2;
            _connector.LookupSecurities( criteria );
        }

        private void OnLookupConnectorLookupSecuritiesResult(
          SecurityLookupMessage message,
          IEnumerable<Security> securities,
          IEnumerable<Security> newSecurities,
          Exception error )
        {
            _connector.Connected -= new Action( OnLookupConnectorConnected );
            _connector.ConnectionError -= new Action<Exception>( OnConnectorConnectionError );
            _connector.LookupSecuritiesResult2 -= new Action<SecurityLookupMessage, IEnumerable<Security>, IEnumerable<Security>, Exception>( OnLookupConnectorLookupSecuritiesResult );
            _securityFilter = null;
            _dataSource = null;
            OnComplete( LocalizedStrings.Str3264Params.Put( SecurityProvider.Count - _numSecuritiesBeforeLookup ), false );
        }

        private void DriveCtrl_OnChanged( IMarketDataDrive drive, bool isNew )
        {
            if ( new MessageBoxBuilder().Text( LocalizedStrings.ImportSecurities ).Question().YesNo().Owner( this ).Show() != MessageBoxResult.Yes )
                return;
            SecurityLookupWindow wnd = new SecurityLookupWindow() { CriteriaMessage = new SecurityLookupMessage() { SecurityTypes = _defaultSecurityTypes } };
            if ( !wnd.ShowModal( this ) )
                return;
            OnStorageLookup( wnd.CriteriaMessage );
        }

        private void OnStorageLookup( SecurityLookupMessage filter )
        {
            //            this._isCancelled = false;
            //            this.BusyIndicator.IsBusy = true;
            //            this.SetProgressSettings( LocalizedStrings.Str3657, ( BaseEditStyleSettings )new ProgressBarStyleSettings() );
            //            IMarketDataDrive drive = this.SelectedDrive;
            //            Task.Run<int>( ( Func<int> )( () =>
            //               {
            //               try
            //               {
            //                   int count = 0;
            //                   ISecurityStorage securityStorage = MarketDataPanel.SecurityStorage;
            //                   IExchangeInfoProvider exchangeInfoProvider = BaseStudioControl.ExchangeInfoProvider;
            //                   drive.LookupSecurities( filter, ( ISecurityProvider )securityStorage, ( Action<SecurityMessage> )( msg =>
            //              {
            //                  securityStorage.Save( msg.ToSecurity( exchangeInfoProvider ), false );
            //                  BaseStudioControl.Connector.SendOutMessage( ( Message )msg );
            //                  ++count;
            //              } ), ( Func<bool> )( () => this._isCancelled ), ( Action<int, int> )( ( i, c ) =>
            //           {
            //                // ISSUE: variable of a compiler-generated type
            //                MarketDataPanel.\u003C\u003Ec__DisplayClass70_0 cDisplayClass700 = this;
            //           int c1 = c;
            //           int i1 = i;
            //                // ISSUE: reference to a compiler-generated field
            //                ( ( DispatcherObject )this ).GuiAsync( ( Action )( () => cDisplayClass700.\u003C\u003E4__this.SetProgress( 0.0, ( double )c1, ( double )i1 ) ));
            //               }) );
            //            return count;
            //        }
            //        catch (SystemException ex)
            //        {
            //          ex.LogError((string) null);
            //          throw new InvalidOperationException( LocalizedStrings.ServerUnavailable);
            //    }
            //})).ContinueWith( ( Action<Task<int>> )( t =>
            //{
            //Exception innerException = t.Exception?.InnerException;
            //if ( innerException != null )
            //    innerException.LogError( ( string )null );
            //string message = innerException?.Message;
            //if ( message == null )
            //    message = LocalizedStrings.Str3264Params.Put( ( object )t.Result );
            //this.OnComplete( message, innerException != null );
            //} ) );
        }

        private void OnComplete( string message, bool error )
        {
            GuiDispatcher.GlobalDispatcher.AddAction( () =>
               {
                   BusyIndicator.IsBusy = false;
                   if ( !message.IsEmpty() )
                   {
                       MessageBoxBuilder messageBoxBuilder = new MessageBoxBuilder().Owner( this ).Text( message );
                       if ( error )
                           messageBoxBuilder.Error();
                       int num = ( int )messageBoxBuilder.Show();
                   }
                   else
                       RefreshGrid( _lastSelectedSecurity );
               } );
        }

        private void SetProgressSettings( string title, BaseEditStyleSettings settings )
        {
            _progressData.Title = title;
            _progressData.Settings = settings;
        }

        private void SetProgress( double minimum, double maximum, double value )
        {
            _progressData.Minimum = minimum;
            _progressData.Value = value;
            _progressData.Maximum = maximum;
        }

        private void Candles_OnClick( object sender, RoutedEventArgs e )
        {
            ShowCandlesPopup();
        }

        private void Candles_OnMouseEnter( object sender, MouseEventArgs e )
        {
            ShowCandlesPopup();
        }

        private void ShowCandlesPopup()
        {
            TimeFramesPopup.IsOpen = false;
            TimeFramesPopup.IsOpen = true;
        }

        private void FillDataSources( ICollection<Tuple<string, object>> items, bool addStorageSource )
        {
            items.Clear();
            if ( addStorageSource )
                items.Add( _storageDataSource );
            foreach ( IMessageAdapter sortedAdapter in _connector.Adapter.InnerAdapters.SortedAdapters )
                items.Add( new Tuple<string, object>( sortedAdapter.ToString(), sortedAdapter ) );
        }

        public override void Load( SettingsStorage storage )
        {
            _isLoading = true;
            try
            {
                base.Load( storage );
                DateTimeFrom.DateTime = storage.GetValue( "DateTimeFrom", DateTimeFrom.DateTime );
                DateTimeTo.DateTime = storage.GetValue( "DateTimeTo", DateTimeTo.DateTime );
                
                // Tony
                //this.TimeoutSeconds.Value = ( Decimal )storage.GetValue<int>( "TimeoutSeconds", ( int )MarketDataPanel.DataDownloader.DefaultDataTimeout.TotalSeconds );
                StorageFormat = storage.GetValue( "StorageFormat", StorageFormat );
                SelectedDrive = ServicesRegistry.DriveCache.GetDrive( storage.GetValue<string>( "SelectedDrive", null ) );
                CheckBox ticks = Ticks;
                SettingsStorage settingsStorage = storage;
                bool? isChecked = Ticks.IsChecked;
                bool flag = true;
                int num = isChecked.GetValueOrDefault() == flag & isChecked.HasValue ? 1 : 0;
                bool? nullable = new bool?( settingsStorage.GetValue( "LoadTicks", num != 0 ) );
                ticks.IsChecked = nullable;
                foreach ( TimeSpan timeSpan in storage.GetValue( "LoadCandles", Enumerable.Empty<TimeSpan>() ) )
                {
                    TimeSpan period = timeSpan;
                    _allCandleTimeFrames.First( t => t.TimeFrame == period ).IsSelected = true;
                }
                IEnumerable<string> source = storage.GetValue( "Securities", Enumerable.Empty<string>() );
                ISecurityStorage secStorage = SecurityStorage;
                Func<string, Security> selector = id => secStorage.LookupById( id );
                SelectSecurities( source.Select( selector ).Where( s => s != null ).ToArray() );
                FormatCtrl.SelectedFormat = storage.GetValue( "StorageFormat", FormatCtrl.SelectedFormat );
                storage.TryLoadSettings<SettingsStorage>( "SecuritiesAll", s => SecuritiesAll.Load( s ) );
                storage.TryLoadSettings<SettingsStorage>( "SecuritiesSelected", s => SecuritiesSelected.Load( s ) );
                storage.TryLoadSettings<SettingsStorage>( "MarketDataGrid", s => MarketDataGrid.Load( s ) );
                storage.TryLoadSettings<string>( "ExtendedInfo", s =>
                   {
                       if ( s.IsEmpty() )
                           return;
                       _extendedStorage = ExtendedInfoStorage.Get( s );
                       ApplyExtendedStorage();
                   } );
                _mappingSecurityWindowSettings = storage.GetValue<SettingsStorage>( "SecurityMappingWindow", null );
            }
            finally
            {
                _isLoading = false;
            }
            RefreshGrid( SecuritiesSelected.SelectedSecurity ?? SecuritiesAll.SelectedSecurity );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "DateTimeFrom", DateTimeFrom.DateTime );
            storage.SetValue( "DateTimeTo", DateTimeTo.DateTime );
            storage.SetValue( "TimeoutSeconds", TimeoutSeconds.Value.To<int>() );
            storage.SetValue( "StorageFormat", StorageFormat );
            if ( SelectedDrive != null )
                storage.SetValue( "SelectedDrive", SelectedDrive.Path );
            SettingsStorage settingsStorage = storage;
            bool? isChecked = Ticks.IsChecked;
            bool flag = true;
            int num = isChecked.GetValueOrDefault() == flag & isChecked.HasValue ? 1 : 0;
            settingsStorage.SetValue( "LoadTicks", num != 0 );
            storage.SetValue( "LoadCandles", _allCandleTimeFrames.Where( t => t.IsSelected ).Select( t => t.TimeFrame ).ToArray() );
            storage.SetValue( "Securities", SecuritiesSelected.Securities.LookupAll().Select( s => s.Id ).ToArray() );
            storage.SetValue( "StorageFormat", FormatCtrl.SelectedFormat );
            storage.SetValue( "SecuritiesAll", SecuritiesAll.Save() );
            storage.SetValue( "SecuritiesSelected", SecuritiesSelected.Save() );
            storage.SetValue( "MarketDataGrid", MarketDataGrid.Save() );
            storage.SetValue( "ExtendedInfo", _extendedStorage?.StorageName );
            storage.SetValue( "SecurityMappingWindow", _mappingSecurityWindowSettings );
        }

        private void SettingsChanged()
        {
            if ( _isLoading )
                return;
            RaiseChangedCommand();
            RefreshGrid( _lastSelectedSecurity );
        }

        private void SecurityPicker_OnSecuritySelected( Security security )
        {
            if ( _isLoading )
                return;
            RefreshGrid( security );
        }

        private void RefreshGrid( Security security )
        {
            if ( DriveCtrl == null || FormatCtrl == null )
                return;
            _lastSelectedSecurity = security;
            MarketDataGrid.BeginMakeEntries( ServicesRegistry.StorageRegistry, security != null ? new SecurityId?( security.ToSecurityId( null, true, false ) ) : new SecurityId?(), StorageFormat, SelectedDrive );
        }

        private void Grid_OnSizeChanged( object sender, SizeChangedEventArgs e )
        {
            RaiseChangedCommand();
        }

        private void ExtendedInfo_OnClick( object sender, RoutedEventArgs e )
        {
            if ( SecuritiesAll.ExtendedInfoStorage != null )
            {
                SecuritiesAll.ExtendedInfoStorage = null;
                ExtendedInfo.Content = LocalizedStrings.ExtendedInfo;
                _extendedStorage = null;
            }
            else
            {
                ExtendedInfoStorageWindow wnd = new ExtendedInfoStorageWindow();
                if ( !wnd.ShowModal( this ) )
                    return;
                _extendedStorage = wnd.SelectedStorage;
                ApplyExtendedStorage();
            }
        }

        private void OnExtendedInfoStorageDeleted( IExtendedInfoStorageItem storage )
        {
            this.GuiAsync( () =>
               {
                   if ( SecuritiesAll.ExtendedInfoStorage != storage )
                       return;
                   SecuritiesAll.ExtendedInfoStorage = null;
               } );
        }

        private void ApplyExtendedStorage()
        {
            SecuritiesAll.ExtendedInfoStorage = _extendedStorage;
            ExtendedInfo.Content = LocalizedStrings.ExtendedInfo + ": " + _extendedStorage?.StorageName;
        }

        private void SecurityMappings_OnClick( object sender, RoutedEventArgs e )
        {
            SecurityMappingWindow securityMappingWindow = new SecurityMappingWindow();
            if ( _mappingSecurityWindowSettings != null )
                securityMappingWindow.Load( _mappingSecurityWindowSettings );
            foreach ( IMessageAdapter possibleAdapter in ServicesRegistry.AdapterProvider.PossibleAdapters )
                securityMappingWindow.ConnectorsInfo.Add( new ConnectorInfo( possibleAdapter ) );
            securityMappingWindow.Storage = ServicesRegistry.MappingStorage;
            securityMappingWindow.ShowModal( this );
            _mappingSecurityWindowSettings = securityMappingWindow.Save();
            RaiseChangedCommand();
        }

        private void DriveCtrl_OnEditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            SettingsChanged();
        }

        private void FormatCtrl_OnEditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            SettingsChanged();
        }

    }
}
