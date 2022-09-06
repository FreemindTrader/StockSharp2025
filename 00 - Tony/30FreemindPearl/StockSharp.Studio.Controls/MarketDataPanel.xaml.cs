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
        private readonly MarketDataPanel.ProgressData _progressData = new MarketDataPanel.ProgressData();
        private readonly Tuple<string, object> _storageDataSource = new Tuple<string, object>( LocalizedStrings.Str1405, ( object )LocalizedStrings.Str1405 );
        private readonly SecurityTypes[ ] _defaultSecurityTypes = new SecurityTypes[5] { SecurityTypes.Currency, SecurityTypes.Index, SecurityTypes.Stock, SecurityTypes.Future, SecurityTypes.CryptoCurrency };
        private CancellationTokenSource _downloadCts;
        private readonly MarketDataPanel.SelectableObject[ ] _allCandleTimeFrames;
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
            this.InitializeComponent();
            this.TimeFrames.ItemsSource = ( IEnumerable )( this._allCandleTimeFrames = ( ( IEnumerable<int> )new int[7]
            {
        1,
        5,
        10,
        15,
        30,
        60,
        1440
            } ).Select<int, MarketDataPanel.SelectableObject>( ( Func<int, MarketDataPanel.SelectableObject> )( i =>
          {
              TimeSpan timeSpan = TimeSpan.FromMinutes( ( double )i );
              MarketDataPanel.SelectableObject selectableObject = new MarketDataPanel.SelectableObject( DataType.TimeFrame( timeSpan ), timeSpan );
              if ( timeSpan == TimeSpan.FromMinutes( 5.0 ) )
                  selectableObject.IsSelected = true;
              return selectableObject;
          } ) ).ToArray<MarketDataPanel.SelectableObject>() );
            this.SecuritiesAll.GridChanged += RaiseChangedCommand;
            this.SecuritiesAll.SecurityDoubleClick += ( Action<Security> )( security =>
              {
                  if ( security == null )
                      return;
                  this.SelectSecurities( new Security[1] { security } );
              } );
            this.SecuritiesSelected.GridChanged += RaiseChangedCommand;
            this.SecuritiesSelected.SecurityDoubleClick += ( Action<Security> )( security =>
              {
                  if ( security == null )
                      return;
                  this.UnselectSecurities( new Security[1] { security } );
              } );
            this.SecuritiesAll.SecurityProvider = BaseStudioControl.SecurityProvider;
            this.SelectedDrive = ServicesRegistry.DriveCache.DefaultDrive;
            DateTime today = DateTime.Today;
            this.DateTimeFrom.NullValue = ( object )DateTime.MinValue;
            this.DateTimeFrom.DateTime = today.AddDays( -365.0 );
            this.DateTimeTo.NullValue = ( object )DateTime.MinValue;
            this.DateTimeTo.DateTime = today;
            
            // Tony
            //this.TimeoutSeconds.Value = ( Decimal )MarketDataPanel.DataDownloader.DefaultDataTimeout.TotalSeconds.To<int>();

            this.TimeoutSeconds.Value = 10;
            this.StorageFormat = StorageFormats.Binary;
            this.BusyIndicator.BusyContent = ( object )this._progressData;
            this.MarketDataGrid.LayoutChanged += RaiseChangedCommand;
            this.MarketDataGrid.DataLoading += ( Action )( () => this.BusyIndicator1.IsBusy = true );
            this.MarketDataGrid.DataLoaded += ( Action )( () => this.BusyIndicator1.IsBusy = false );
            this._connector = BaseStudioControl.Connector;
            MarketDataPanel.ExtendedInfoStorage.Deleted += new Action<IExtendedInfoStorageItem>( this.OnExtendedInfoStorageDeleted );
        }

        public IMarketDataDrive SelectedDrive
        {
            get
            {
                return this.DriveCtrl.SelectedDrive;
            }
            set
            {
                this.DriveCtrl.SelectedDrive = value;
            }
        }

        public StorageFormats StorageFormat
        {
            get
            {
                return this.FormatCtrl.SelectedFormat;
            }
            set
            {
                this.FormatCtrl.SelectedFormat = value;
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
            return reason == CloseReason.Shutdown || !this.BusyIndicator.IsBusy ? CloseAction.Close : CloseAction.StayOpen;
        }

        private void ExecutedSelectSecurity( object sender, ExecutedRoutedEventArgs e )
        {
            this.SelectSecurities( this.SecuritiesAll.SelectedSecurities.ToArray<Security>() );
        }

        private void CanExecuteSelectSecurity( object sender, CanExecuteRoutedEventArgs e )
        {
            CanExecuteRoutedEventArgs executeRoutedEventArgs = e;
            SecurityPicker securitiesAll = this.SecuritiesAll;
            int num = securitiesAll != null ? ( securitiesAll.SelectedSecurities.Any<Security>() ? 1 : 0 ) : 0;
            executeRoutedEventArgs.CanExecute = num != 0;
        }

        private void ExecutedUnselectSecurity( object sender, ExecutedRoutedEventArgs e )
        {
            this.UnselectSecurities( this.SecuritiesSelected.SelectedSecurities.ToArray<Security>() );
        }

        private void CanExecuteUnselectSecurity( object sender, CanExecuteRoutedEventArgs e )
        {
            CanExecuteRoutedEventArgs executeRoutedEventArgs = e;
            SecurityPicker securitiesSelected = this.SecuritiesSelected;
            int num = securitiesSelected != null ? ( securitiesSelected.SelectedSecurities.Any<Security>() ? 1 : 0 ) : 0;
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
                int num = ( int )new MessageBoxBuilder().Warning().Owner( ( DependencyObject )marketDataPanel ).Caption( LocalizedStrings.Warning ).Text( LocalizedStrings.OperationCanceled ).Show();
            }
            catch ( Exception ex )
            {
                int num = ( int )new MessageBoxBuilder().Error().Owner( ( DependencyObject )marketDataPanel ).Caption( LocalizedStrings.Str152 ).Text( ex.ToString() ).Show();
            }
        }

        private async Task ExecutedDownloadAsync()
        {
            MarketDataPanel parent = this;
            parent.RaiseChangedCommand();
            DataSourceWindow wnd = new DataSourceWindow();
            wnd.Configure = ( Action )( () => this.ConfigureConnector( ( ICollection<Tuple<string, object>> )wnd.DataSourceItemsSource, false ) );
            parent.FillDataSources( ( ICollection<Tuple<string, object>> )wnd.DataSourceItemsSource, false );
            Security[ ] array1 = parent.SecuritiesSelected.Securities.LookupAll().ToArray<Security>();
            ExchangeBoard board = array1[0].Board;
            if ( array1.Length == 1 || ( Equatable<ExchangeBoard> )board != ( ExchangeBoard )null && ( ( IEnumerable<Security> )array1 ).All<Security>( ( Func<Security, bool> )( s => ( Equatable<ExchangeBoard> )s.Board == board ) ) )
            {
                Guid? secAdapter = ServicesRegistry.SecurityAdapterProvider.TryGetAdapter( array1[0].ToSecurityId( ( SecurityIdGenerator )null, true, false ), ( DataType )null );
                if ( secAdapter.HasValue )
                    wnd.SelectedDataSource = wnd.DataSourceItemsSource.FirstOrDefault<Tuple<string, object>>( ( Func<Tuple<string, object>, bool> )( t =>
                       {
                           IMessageAdapter messageAdapter = t.Item2 as IMessageAdapter;
                           if ( messageAdapter == null )
                               return !secAdapter.HasValue;
                           Guid id = messageAdapter.Id;
                           Guid? nullable = secAdapter;
                           if ( !nullable.HasValue )
                               return false;
                           return id == nullable.GetValueOrDefault();
                       } ) );
            }
            if ( !wnd.ShowModal( ( DependencyObject )parent ) )
                return;
            DateTime dateTime1 = parent.DateTimeFrom.DateTime;
            DateTime dateTime2 = parent.DateTimeTo.DateTime;
            IEnumerable<DataType> dataTypes = ( ( IEnumerable<MarketDataPanel.SelectableObject> )parent._allCandleTimeFrames ).Where<MarketDataPanel.SelectableObject>( ( Func<MarketDataPanel.SelectableObject, bool> )( t => t.IsSelected ) ).Select<MarketDataPanel.SelectableObject, DataType>( ( Func<MarketDataPanel.SelectableObject, DataType> )( t => t.Value ) );
            bool? isChecked = parent.Ticks.IsChecked;
            bool flag = true;
            if ( isChecked.GetValueOrDefault() == flag & isChecked.HasValue )
                dataTypes = dataTypes.Concat<DataType>( ( IEnumerable<DataType> )new DataType[1]
                {
          DataType.Ticks
                } );
            IEnumerable<DataType> array2 = ( IEnumerable<DataType> )dataTypes.ToArray<DataType>();
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
            if ( source.IsEmpty<Subscription>() )
            {
                int num = ( int )new MessageBoxBuilder().Owner( ( DependencyObject )parent ).Text( LocalizedStrings.XamlStr344 ).Show();
            }
            else
            {
                if ( !parent.CheckConnectionState() )
                    return;
                parent._isCancelled = false;
                try
                {
                    parent.BusyIndicator.IsBusy = true;
                    parent.SetProgressSettings( LocalizedStrings.XamlStr189, ( BaseEditStyleSettings )new ProgressBarMarqueeStyleSettings() );
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
            e.CanExecute = this.SecuritiesSelected.Securities.Count > 0;
        }

        private void ExecutedCancel( object sender, ExecutedRoutedEventArgs e )
        {
            this._isCancelled = true;
            this._downloadCts?.Cancel();
        }

        private void CanExecuteCancel( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = !this._isCancelled;
        }

        private void CreateRegular( object sender, RoutedEventArgs e )
        {
            new CreateSecurityCommand( typeof( Security ) ).Process( ( object )this, true );
        }

        private void CreateIndex( object sender, RoutedEventArgs e )
        {
            new CreateSecurityCommand( typeof( IndexSecurity ) ).Process( ( object )this, true );
        }

        private void CreateContinuous( object sender, RoutedEventArgs e )
        {
            new CreateSecurityCommand( typeof( ContinuousSecurity ) ).Process( ( object )this, true );
        }

        private void EditSecurityCommandExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            new EditSecuritiesCommand( ( IEnumerable<Security> )this.SecuritiesAll.SelectedSecurities ).Process( ( object )this, true );
        }

        private void EditSecurityCommandCanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = this.SecuritiesAll?.SelectedSecurity != null;
        }

        private void DeleteSecurityCommandExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            new RemoveSecuritiesCommand( ( IEnumerable<Security> )this.SecuritiesAll.SelectedSecurities.ToArray<Security>() ).Process( ( object )this, true );
        }

        private void DeleteSecurityCommandCanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = this.SecuritiesAll?.SelectedSecurity != null;
        }

        public void SelectSecurities( Security[ ] securities )
        {
            this.SecuritiesSelected.Securities.AddRange( ( IEnumerable<Security> )securities );
            this.SecuritiesAll.ExcludeSecurities.AddRange<Security>( ( IEnumerable<Security> )securities );
            this.RaiseChangedCommand();
        }

        private void UnselectSecurities( Security[ ] securities )
        {
            this.SecuritiesSelected.Securities.RemoveRange( ( IEnumerable<Security> )securities );
            this.SecuritiesAll.ExcludeSecurities.RemoveRange<Security>( ( IEnumerable<Security> )securities );
            this.RaiseChangedCommand();
        }

        private void DownloadSecurities_OnClick( object sender, RoutedEventArgs e )
        {
            SecurityLookupWindow wnd = new SecurityLookupWindow() { ExchangeInfoProvider = BaseStudioControl.ExchangeInfoProvider, ShowDataSourcePanel = true };
            wnd.Configure = ( Action )( () => this.ConfigureConnector( ( ICollection<Tuple<string, object>> )wnd.DataSourceItemsSource, true ) );
            this.FillDataSources( ( ICollection<Tuple<string, object>> )wnd.DataSourceItemsSource, true );
            if ( !wnd.ShowModal( ( DependencyObject )this ) )
                return;
            Tuple<string, object> selectedDataSource = wnd.SelectedDataSource;
            if ( !this.OnCanLookup( selectedDataSource ) )
                return;
            this._isCancelled = false;
            this._numSecuritiesBeforeLookup = BaseStudioControl.SecurityProvider.Count;
            this.BusyIndicator.IsBusy = true;
            this.SetProgressSettings( LocalizedStrings.Str3657, ( BaseEditStyleSettings )new ProgressBarMarqueeStyleSettings() );
            SecurityLookupMessage criteriaMessage = wnd.CriteriaMessage;
            if ( object.Equals( ( object )selectedDataSource, ( object )this._storageDataSource ) )
                this.OnStorageLookup( criteriaMessage );
            else
                this.OnConnectorLookup( criteriaMessage, selectedDataSource );
        }

        private bool OnCanLookup( Tuple<string, object> type )
        {
            if ( object.Equals( ( object )type, ( object )this._storageDataSource ) )
                return true;
            if ( !this.CanLookup )
            {
                int num = ( int )new MessageBoxBuilder().Owner( ( DependencyObject )this ).Warning().Text( LocalizedStrings.LookupSecuritiesNotSupported ).Show();
                return false;
            }
            return this.CheckConnectionState();
        }

        private bool CheckConnectionState()
        {
            if ( this._connector.ConnectionState == ConnectionStates.Connected )
                return true;
            return new MessageBoxBuilder().Owner( ( DependencyObject )this ).Question().Text( "{0} {1}?".Put( ( object )LocalizedStrings.NoActiveConnection, ( object )LocalizedStrings.Connect ) ).YesNo().Show() == MessageBoxResult.Yes;
        }

        private void ConfigureConnector( ICollection<Tuple<string, object>> items, bool addStorageSource )
        {
            if ( this._connector.ConnectionState == ConnectionStates.Connected )
            {
                if ( new MessageBoxBuilder().Owner( ( DependencyObject )this ).Question().Text( "{0} {1}?".Put( ( object )LocalizedStrings.Str1555, ( object )LocalizedStrings.Disconnect ) ).YesNo().Show() != MessageBoxResult.Yes )
                    return;
                this._connector.Disconnect();
            }
            ConfigureConnectorCommand command = new ConfigureConnectorCommand();
            command.Process( ( object )this, true );
            if ( !command.Result )
                return;
            this.FillDataSources( items, addStorageSource );
        }

        private void OnConnectorConnectionError( Exception error )
        {
            this.OnComplete( error.Message, true );
        }

        private bool CanLookup
        {
            get
            {
                IMessageAdapter marketDataAdapter = this._connector.MarketDataAdapter;
                if ( marketDataAdapter == null )
                    return false;
                return marketDataAdapter.IsMessageSupported( MessageTypes.SecurityLookup );
            }
        }

        private void OnConnectorLookup( SecurityLookupMessage filter, Tuple<string, object> dataSource )
        {
            this._securityFilter = filter;
            this._dataSource = dataSource;
            this._connector.LookupSecuritiesResult2 += new Action<SecurityLookupMessage, IEnumerable<Security>, IEnumerable<Security>, Exception>( this.OnLookupConnectorLookupSecuritiesResult );
            if ( this._connector.ConnectionState != ConnectionStates.Connected )
            {
                this._connector.Connected += new Action( this.OnLookupConnectorConnected );
                this._connector.ConnectionError += new Action<Exception>( this.OnConnectorConnectionError );
                this._connector.Connect();
            }
            else
                this.OnLookupConnectorConnected();
        }

        private void OnLookupConnectorConnected()
        {
            SecurityLookupMessage criteria = this._securityFilter.TypedClone<SecurityLookupMessage>();
            criteria.Adapter = ( IMessageAdapter )this._dataSource?.Item2;
            this._connector.LookupSecurities( criteria );
        }

        private void OnLookupConnectorLookupSecuritiesResult(
          SecurityLookupMessage message,
          IEnumerable<Security> securities,
          IEnumerable<Security> newSecurities,
          Exception error )
        {
            this._connector.Connected -= new Action( this.OnLookupConnectorConnected );
            this._connector.ConnectionError -= new Action<Exception>( this.OnConnectorConnectionError );
            this._connector.LookupSecuritiesResult2 -= new Action<SecurityLookupMessage, IEnumerable<Security>, IEnumerable<Security>, Exception>( this.OnLookupConnectorLookupSecuritiesResult );
            this._securityFilter = ( SecurityLookupMessage )null;
            this._dataSource = ( Tuple<string, object> )null;
            this.OnComplete( LocalizedStrings.Str3264Params.Put( ( object )( BaseStudioControl.SecurityProvider.Count - this._numSecuritiesBeforeLookup ) ), false );
        }

        private void DriveCtrl_OnChanged( IMarketDataDrive drive, bool isNew )
        {
            if ( new MessageBoxBuilder().Text( LocalizedStrings.ImportSecurities ).Question().YesNo().Owner( ( DependencyObject )this ).Show() != MessageBoxResult.Yes )
                return;
            SecurityLookupWindow wnd = new SecurityLookupWindow() { CriteriaMessage = new SecurityLookupMessage() { SecurityTypes = this._defaultSecurityTypes } };
            if ( !wnd.ShowModal( ( DependencyObject )this ) )
                return;
            this.OnStorageLookup( wnd.CriteriaMessage );
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
            GuiDispatcher.GlobalDispatcher.AddAction( ( Action )( () =>
               {
                   this.BusyIndicator.IsBusy = false;
                   if ( !message.IsEmpty() )
                   {
                       MessageBoxBuilder messageBoxBuilder = new MessageBoxBuilder().Owner( ( DependencyObject )this ).Text( message );
                       if ( error )
                           messageBoxBuilder.Error();
                       int num = ( int )messageBoxBuilder.Show();
                   }
                   else
                       this.RefreshGrid( this._lastSelectedSecurity );
               } ) );
        }

        private void SetProgressSettings( string title, BaseEditStyleSettings settings )
        {
            this._progressData.Title = title;
            this._progressData.Settings = settings;
        }

        private void SetProgress( double minimum, double maximum, double value )
        {
            this._progressData.Minimum = minimum;
            this._progressData.Value = value;
            this._progressData.Maximum = maximum;
        }

        private void Candles_OnClick( object sender, RoutedEventArgs e )
        {
            this.ShowCandlesPopup();
        }

        private void Candles_OnMouseEnter( object sender, MouseEventArgs e )
        {
            this.ShowCandlesPopup();
        }

        private void ShowCandlesPopup()
        {
            this.TimeFramesPopup.IsOpen = false;
            this.TimeFramesPopup.IsOpen = true;
        }

        private void FillDataSources( ICollection<Tuple<string, object>> items, bool addStorageSource )
        {
            items.Clear();
            if ( addStorageSource )
                items.Add( this._storageDataSource );
            foreach ( IMessageAdapter sortedAdapter in this._connector.Adapter.InnerAdapters.SortedAdapters )
                items.Add( new Tuple<string, object>( sortedAdapter.ToString(), ( object )sortedAdapter ) );
        }

        public override void Load( SettingsStorage storage )
        {
            this._isLoading = true;
            try
            {
                base.Load( storage );
                this.DateTimeFrom.DateTime = storage.GetValue<DateTime>( "DateTimeFrom", this.DateTimeFrom.DateTime );
                this.DateTimeTo.DateTime = storage.GetValue<DateTime>( "DateTimeTo", this.DateTimeTo.DateTime );
                
                // Tony
                //this.TimeoutSeconds.Value = ( Decimal )storage.GetValue<int>( "TimeoutSeconds", ( int )MarketDataPanel.DataDownloader.DefaultDataTimeout.TotalSeconds );
                this.StorageFormat = storage.GetValue<StorageFormats>( "StorageFormat", this.StorageFormat );
                this.SelectedDrive = ServicesRegistry.DriveCache.GetDrive( storage.GetValue<string>( "SelectedDrive", ( string )null ) );
                CheckBox ticks = this.Ticks;
                SettingsStorage settingsStorage = storage;
                bool? isChecked = this.Ticks.IsChecked;
                bool flag = true;
                int num = isChecked.GetValueOrDefault() == flag & isChecked.HasValue ? 1 : 0;
                bool? nullable = new bool?( settingsStorage.GetValue<bool>( "LoadTicks", num != 0 ) );
                ticks.IsChecked = nullable;
                foreach ( TimeSpan timeSpan in storage.GetValue<IEnumerable<TimeSpan>>( "LoadCandles", Enumerable.Empty<TimeSpan>() ) )
                {
                    TimeSpan period = timeSpan;
                    ( ( IEnumerable<MarketDataPanel.SelectableObject> )this._allCandleTimeFrames ).First<MarketDataPanel.SelectableObject>( ( Func<MarketDataPanel.SelectableObject, bool> )( t => t.TimeFrame == period ) ).IsSelected = true;
                }
                IEnumerable<string> source = storage.GetValue<IEnumerable<string>>( "Securities", Enumerable.Empty<string>() );
                ISecurityStorage secStorage = MarketDataPanel.SecurityStorage;
                Func<string, Security> selector = ( Func<string, Security> )( id => secStorage.LookupById( id ) );
                this.SelectSecurities( source.Select<string, Security>( selector ).Where<Security>( ( Func<Security, bool> )( s => s != null ) ).ToArray<Security>() );
                this.FormatCtrl.SelectedFormat = storage.GetValue<StorageFormats>( "StorageFormat", this.FormatCtrl.SelectedFormat );
                storage.TryLoadSettings<SettingsStorage>( "SecuritiesAll", ( Action<SettingsStorage> )( s => this.SecuritiesAll.Load( s ) ) );
                storage.TryLoadSettings<SettingsStorage>( "SecuritiesSelected", ( Action<SettingsStorage> )( s => this.SecuritiesSelected.Load( s ) ) );
                storage.TryLoadSettings<SettingsStorage>( "MarketDataGrid", ( Action<SettingsStorage> )( s => this.MarketDataGrid.Load( s ) ) );
                storage.TryLoadSettings<string>( "ExtendedInfo", ( Action<string> )( s =>
                   {
                       if ( s.IsEmpty() )
                           return;
                       this._extendedStorage = MarketDataPanel.ExtendedInfoStorage.Get( s );
                       this.ApplyExtendedStorage();
                   } ) );
                this._mappingSecurityWindowSettings = storage.GetValue<SettingsStorage>( "SecurityMappingWindow", ( SettingsStorage )null );
            }
            finally
            {
                this._isLoading = false;
            }
            this.RefreshGrid( this.SecuritiesSelected.SelectedSecurity ?? this.SecuritiesAll.SelectedSecurity );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue<DateTime>( "DateTimeFrom", this.DateTimeFrom.DateTime );
            storage.SetValue<DateTime>( "DateTimeTo", this.DateTimeTo.DateTime );
            storage.SetValue<int>( "TimeoutSeconds", this.TimeoutSeconds.Value.To<int>() );
            storage.SetValue<StorageFormats>( "StorageFormat", this.StorageFormat );
            if ( this.SelectedDrive != null )
                storage.SetValue<string>( "SelectedDrive", this.SelectedDrive.Path );
            SettingsStorage settingsStorage = storage;
            bool? isChecked = this.Ticks.IsChecked;
            bool flag = true;
            int num = isChecked.GetValueOrDefault() == flag & isChecked.HasValue ? 1 : 0;
            settingsStorage.SetValue<bool>( "LoadTicks", num != 0 );
            storage.SetValue<TimeSpan[ ]>( "LoadCandles", ( ( IEnumerable<MarketDataPanel.SelectableObject> )this._allCandleTimeFrames ).Where<MarketDataPanel.SelectableObject>( ( Func<MarketDataPanel.SelectableObject, bool> )( t => t.IsSelected ) ).Select<MarketDataPanel.SelectableObject, TimeSpan>( ( Func<MarketDataPanel.SelectableObject, TimeSpan> )( t => t.TimeFrame ) ).ToArray<TimeSpan>() );
            storage.SetValue<string[ ]>( "Securities", this.SecuritiesSelected.Securities.LookupAll().Select<Security, string>( ( Func<Security, string> )( s => s.Id ) ).ToArray<string>() );
            storage.SetValue<StorageFormats>( "StorageFormat", this.FormatCtrl.SelectedFormat );
            storage.SetValue<SettingsStorage>( "SecuritiesAll", this.SecuritiesAll.Save() );
            storage.SetValue<SettingsStorage>( "SecuritiesSelected", this.SecuritiesSelected.Save() );
            storage.SetValue<SettingsStorage>( "MarketDataGrid", this.MarketDataGrid.Save() );
            storage.SetValue<string>( "ExtendedInfo", this._extendedStorage?.StorageName );
            storage.SetValue<SettingsStorage>( "SecurityMappingWindow", this._mappingSecurityWindowSettings );
        }

        private void SettingsChanged()
        {
            if ( this._isLoading )
                return;
            this.RaiseChangedCommand();
            this.RefreshGrid( this._lastSelectedSecurity );
        }

        private void SecurityPicker_OnSecuritySelected( Security security )
        {
            if ( this._isLoading )
                return;
            this.RefreshGrid( security );
        }

        private void RefreshGrid( Security security )
        {
            if ( this.DriveCtrl == null || this.FormatCtrl == null )
                return;
            this._lastSelectedSecurity = security;
            this.MarketDataGrid.BeginMakeEntries( ServicesRegistry.StorageRegistry, security != null ? new SecurityId?( security.ToSecurityId( ( SecurityIdGenerator )null, true, false ) ) : new SecurityId?(), this.StorageFormat, this.SelectedDrive );
        }

        private void Grid_OnSizeChanged( object sender, SizeChangedEventArgs e )
        {
            this.RaiseChangedCommand();
        }

        private void ExtendedInfo_OnClick( object sender, RoutedEventArgs e )
        {
            if ( this.SecuritiesAll.ExtendedInfoStorage != null )
            {
                this.SecuritiesAll.ExtendedInfoStorage = ( IExtendedInfoStorageItem )null;
                this.ExtendedInfo.Content = ( object )LocalizedStrings.ExtendedInfo;
                this._extendedStorage = ( IExtendedInfoStorageItem )null;
            }
            else
            {
                ExtendedInfoStorageWindow wnd = new ExtendedInfoStorageWindow();
                if ( !wnd.ShowModal( ( DependencyObject )this ) )
                    return;
                this._extendedStorage = wnd.SelectedStorage;
                this.ApplyExtendedStorage();
            }
        }

        private void OnExtendedInfoStorageDeleted( IExtendedInfoStorageItem storage )
        {
            ( ( DispatcherObject )this ).GuiAsync( ( Action )( () =>
                  {
                      if ( this.SecuritiesAll.ExtendedInfoStorage != storage )
                          return;
                      this.SecuritiesAll.ExtendedInfoStorage = ( IExtendedInfoStorageItem )null;
                  } ) );
        }

        private void ApplyExtendedStorage()
        {
            this.SecuritiesAll.ExtendedInfoStorage = this._extendedStorage;
            this.ExtendedInfo.Content = ( object )( LocalizedStrings.ExtendedInfo + ": " + this._extendedStorage?.StorageName );
        }

        private void SecurityMappings_OnClick( object sender, RoutedEventArgs e )
        {
            SecurityMappingWindow securityMappingWindow = new SecurityMappingWindow();
            if ( this._mappingSecurityWindowSettings != null )
                securityMappingWindow.Load( this._mappingSecurityWindowSettings );
            foreach ( IMessageAdapter possibleAdapter in ServicesRegistry.AdapterProvider.PossibleAdapters )
                securityMappingWindow.ConnectorsInfo.Add( new ConnectorInfo( possibleAdapter ) );
            securityMappingWindow.Storage = ServicesRegistry.MappingStorage;
            securityMappingWindow.ShowModal( ( DependencyObject )this );
            this._mappingSecurityWindowSettings = securityMappingWindow.Save();
            this.RaiseChangedCommand();
        }

        private void DriveCtrl_OnEditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            this.SettingsChanged();
        }

        private void FormatCtrl_OnEditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            this.SettingsChanged();
        }

    }
}
