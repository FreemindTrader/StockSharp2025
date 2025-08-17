// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.MarketDataPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;
using DevExpress.Internal;
using DevExpress.Xpf.Editors;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Logging;
using Ecng.Serialization;
using Ecng.Xaml;
using Nito.AsyncEx;
using StockSharp.Algo;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Core.Commands;
using StockSharp.Studio.Core.Services;
using StockSharp.Xaml;
using StockSharp.Xaml.GridControl;



#nullable enable
namespace StockSharp.Studio.Controls;

[Display(ResourceType = typeof(LocalizedStrings), Name = "MarketData", Description = "MarketData")]
[Guid("B5CA7FCD-601E-41C8-8C94-4F57855F64E0")]
[VectorIcon("Storage")]
[Doc("topics/designer/market_data_storage.html")]
public partial class MarketDataPanel : BaseStudioControl, IStudioCommandScope, IComponentConnector
{
    public static readonly
#nullable disable
    RoutedCommand SelectSecurityCommand = new RoutedCommand();
    public static readonly RoutedCommand UnselectSecurityCommand = new RoutedCommand();
    public static readonly RoutedCommand CancelCommand = new RoutedCommand();
    public static readonly RoutedCommand DownloadCommand = new RoutedCommand();
    public static readonly RoutedCommand EditSecurityCommand = new RoutedCommand();
    public static readonly RoutedCommand DeleteSecurityCommand = new RoutedCommand();
    private readonly MarketDataPanel.ProgressData _progressData;
    private readonly Tuple<string, object> _storageDataSource;
    private CancellationTokenSource _downloadCts;
    private readonly SecurityTypes[] _defaultSecurityTypes;
    private readonly MarketDataPanel.SelectableObject[] _allCandleTimeFrames;
    private readonly Connector _connector;
    private int _numSecuritiesBeforeLookup;
    private bool _isCancelled;
    private bool _isLoading;
    private SecurityLookupMessage _securityFilter;
    private Tuple<string, object> _dataSource;
    private IExtendedInfoStorageItem _extendedStorage;
    private SettingsStorage _mappingSecurityWindowSettings;
    private Security _lastSelectedSecurity;


    bool IStudioCommandScope.UseParentScope => false;

    bool IStudioCommandScope.RouteToGlobalScope => false;

    public MarketDataPanel()
    {
        // ISSUE: unable to decompile the method.
    }

    public IMarketDataDrive SelectedDrive
    {
        get => this.DriveCtrl.SelectedDrive;
        set => this.DriveCtrl.SelectedDrive = value;
    }

    public StorageFormats StorageFormat
    {
        get => this.FormatCtrl.SelectedFormat;
        set => this.FormatCtrl.SelectedFormat = value;
    }

    private static IExtendedInfoStorage ExtendedInfoStorage => ServicesRegistry.ExtendedInfoStorage;

    private static ISecurityStorage SecurityStorage => ServicesRegistry.SecurityStorage;

    public override CloseAction CanClose(CloseReason reason)
    {
        return reason == CloseReason.Shutdown || !this.BusyIndicator.IsBusy ? CloseAction.Close : CloseAction.StayOpen;
    }

    private void ExecutedSelectSecurity(object sender, ExecutedRoutedEventArgs e)
    {
        IList<Security> selectedSecurities = this.SecuritiesAll.SelectedSecurities;
        int index = 0;
        Security[] securities = new Security[selectedSecurities.Count];
        foreach (Security security in (IEnumerable<Security>)selectedSecurities)
        {
            securities[index] = security;
            ++index;
        }
        this.SelectSecurities(securities);
    }

    private void CanExecuteSelectSecurity(object sender, CanExecuteRoutedEventArgs e)
    {
        CanExecuteRoutedEventArgs executeRoutedEventArgs = e;
        SecurityPicker securitiesAll = this.SecuritiesAll;
        int num = securitiesAll != null ? (securitiesAll.SelectedSecurities.Any<Security>() ? 1 : 0) : 0;
        executeRoutedEventArgs.CanExecute = num != 0;
    }

    private void ExecutedUnselectSecurity(object sender, ExecutedRoutedEventArgs e)
    {
        IList<Security> selectedSecurities = this.SecuritiesSelected.SelectedSecurities;
        int index = 0;
        Security[] securities = new Security[selectedSecurities.Count];
        foreach (Security security in (IEnumerable<Security>)selectedSecurities)
        {
            securities[index] = security;
            ++index;
        }
        this.UnselectSecurities(securities);
    }

    private void CanExecuteUnselectSecurity(object sender, CanExecuteRoutedEventArgs e)
    {
        CanExecuteRoutedEventArgs executeRoutedEventArgs = e;
        SecurityPicker securitiesSelected = this.SecuritiesSelected;
        int num = securitiesSelected != null ? (securitiesSelected.SelectedSecurities.Any<Security>() ? 1 : 0) : 0;
        executeRoutedEventArgs.CanExecute = num != 0;
    }

    private async void ExecutedDownload(object sender, ExecutedRoutedEventArgs args)
    {
        MarketDataPanel owner = this;
        try
        {
            await owner.ExecutedDownloadAsync();
        }
        catch (OperationCanceledException ex)
        {
            int num = (int)new MessageBoxBuilder().Warning().Owner((DependencyObject)owner).Caption(LocalizedStrings.Warning).Text(LocalizedStrings.OperationCanceled).Show();
        }
        catch (Exception ex)
        {
            int num = (int)new MessageBoxBuilder().Error().Owner((DependencyObject)owner).Caption(LocalizedStrings.Error).Text(ex.ToString()).Show();
        }
    }

    private async Task ExecutedDownloadAsync()
    {
        MarketDataPanel parent = this;
        parent.RaiseChangedCommand();
        DataSourceWindow wnd = new DataSourceWindow();
        wnd.Configure = (Action)(() => this.ConfigureConnector((ICollection<Tuple<string, object>>)wnd.DataSourceItemsSource, false));
        parent.FillDataSources((ICollection<Tuple<string, object>>)wnd.DataSourceItemsSource, false);
        Security[] array1 = parent.SecuritiesSelected.Securities.LookupAll().ToArray<Security>();
        ExchangeBoard board = array1[0].Board;
        if ((array1.Length == 1 || board != null) && (array1).All<Security>(s => s.Board == board))
        {
            Guid? secAdapter = ServicesRegistry.SecurityAdapterProvider.TryGetAdapter(array1[0].ToSecurityId((SecurityIdGenerator)null, true, false), (StockSharp.Messages.DataType)null);
            if (secAdapter.HasValue)
                wnd.SelectedDataSource = wnd.DataSourceItemsSource.FirstOrDefault<Tuple<string, object>>((Func<Tuple<string, object>, bool>)(t =>
                {
                    IMessageAdapter messageAdapter = t.Item2 as IMessageAdapter;
                    if (messageAdapter == null)
                        return !secAdapter.HasValue;
                    Guid id = ((ILogSource)messageAdapter).Id;
                    Guid? nullable = secAdapter;
                    if (!nullable.HasValue)
                        return false;
                    return id == nullable.GetValueOrDefault();
                }));
        }
        if (!wnd.ShowModal((DependencyObject)parent))
            return;
        DateTime dateTime1 = parent.DateTimeFrom.DateTime;
        DateTime dateTime2 = TimeHelper.EndOfDay(parent.DateTimeTo.DateTime);
        IEnumerable<StockSharp.Messages.DataType> array2 = (IEnumerable<StockSharp.Messages.DataType>)((IEnumerable<MarketDataPanel.SelectableObject>)parent._allCandleTimeFrames).Where<MarketDataPanel.SelectableObject>((Func<MarketDataPanel.SelectableObject, bool>)(t => t.IsSelected)).Select<MarketDataPanel.SelectableObject, StockSharp.Messages.DataType>((Func<MarketDataPanel.SelectableObject, StockSharp.Messages.DataType>)(t => t.Value)).ToArray<StockSharp.Messages.DataType>();
        IMessageAdapter messageAdapter1 = wnd.SelectedDataSource.Item2 as IMessageAdapter;
        List<Subscription> subscriptionList = new List<Subscription>();
        foreach (Security security in array1)
        {
            foreach (StockSharp.Messages.DataType dataType in array2)
            {
                Subscription subscription = new Subscription(dataType, security);
                MarketDataMessage subscriptionMessage = (MarketDataMessage)subscription.SubscriptionMessage;
                subscriptionMessage.From = new DateTimeOffset?((DateTimeOffset)dateTime1);
                subscriptionMessage.To = new DateTimeOffset?((DateTimeOffset)dateTime2);
                subscriptionMessage.BuildMode = MarketDataBuildModes.Load;
                subscriptionMessage.Adapter = messageAdapter1;
                subscriptionList.Add(subscription);
            }
        }
        if (CollectionHelper.IsEmpty<Subscription>(subscriptionList))
        {
            int num = (int)new MessageBoxBuilder().Owner((DependencyObject)parent).Text(LocalizedStrings.NoDataTypeSelected).Show();
        }
        else
        {
            if (!parent.CheckConnectionState())
                return;
            parent._isCancelled = false;
            try
            {
                parent.BusyIndicator.IsBusy = true;
                parent.SetProgressSettings(LocalizedStrings.LoadingDataWait, (BaseEditStyleSettings)new ProgressBarMarqueeStyleSettings());
                parent._downloadCts = new CancellationTokenSource();
                using (MarketDataPanel.DataDownloader downloader = new MarketDataPanel.DataDownloader(parent, (IEnumerable<Subscription>)subscriptionList, parent._downloadCts.Token))
                    await downloader.DownloadTask;
            }
            finally
            {
                parent.BusyIndicator.IsBusy = false;
                parent.RefreshGrid(parent._lastSelectedSecurity);
            }
        }
    }

    private void CanExecuteDownload(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = this.SecuritiesSelected.Securities.Count > 0;
    }

    private void ExecutedCancel(object sender, ExecutedRoutedEventArgs e)
    {
        this._isCancelled = true;
        this._downloadCts?.Cancel();
    }

    private void CanExecuteCancel(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = !this._isCancelled;
    }

    private void CreateRegular(object sender, RoutedEventArgs e)
    {
        new CreateSecurityCommand(typeof(Security)).Process((object)this, true);
    }

    private void CreateIndex(object sender, RoutedEventArgs e)
    {
        new CreateSecurityCommand(typeof(IndexSecurity)).Process((object)this, true);
    }

    private void CreateContinuous(object sender, RoutedEventArgs e)
    {
        new CreateSecurityCommand(typeof(ContinuousSecurity)).Process((object)this, true);
    }

    private void EditSecurityCommandExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        new EditSecuritiesCommand((IEnumerable<Security>)this.SecuritiesAll.SelectedSecurities).Process((object)this, true);
    }

    private void EditSecurityCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = this.SecuritiesAll?.SelectedSecurity != null;
    }

    private void DeleteSecurityCommandExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        IList<Security> selectedSecurities = this.SecuritiesAll.SelectedSecurities;
        int index = 0;
        Security[] items = new Security[selectedSecurities.Count];
        foreach (Security security in (IEnumerable<Security>)selectedSecurities)
        {
            items[index] = security;
            ++index;
        }

        new RemoveEntitiesCommand<Security>(items).Process(this, true);
    }

    private void DeleteSecurityCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = this.SecuritiesAll?.SelectedSecurity != null;
    }

    public void SelectSecurities(Security[] securities)
    {
        this.SecuritiesSelected.Securities.AddRange((IEnumerable<Security>)securities);
        CollectionHelper.AddRange<Security>((ICollection<Security>)this.SecuritiesAll.ExcludeSecurities, (IEnumerable<Security>)securities);
        this.RaiseChangedCommand();
    }

    private void UnselectSecurities(Security[] securities)
    {
        this.SecuritiesSelected.Securities.RemoveRange((IEnumerable<Security>)securities);
        CollectionHelper.RemoveRange<Security>((ICollection<Security>)this.SecuritiesAll.ExcludeSecurities, (IEnumerable<Security>)securities);
        this.RaiseChangedCommand();
    }

    private void DownloadSecurities_OnClick(object sender, RoutedEventArgs e)
    {
        SecurityLookupWindow wnd = new SecurityLookupWindow()
        {
            ExchangeInfoProvider = BaseStudioControl.ExchangeInfoProvider,
            ShowDataSourcePanel = true
        };
        wnd.Configure = (Action)(() => this.ConfigureConnector((ICollection<Tuple<string, object>>)wnd.DataSourceItemsSource, true));
        this.FillDataSources((ICollection<Tuple<string, object>>)wnd.DataSourceItemsSource, true);
        if (!wnd.ShowModal((DependencyObject)this))
            return;
        Tuple<string, object> selectedDataSource = wnd.SelectedDataSource;
        if (!this.OnCanLookup(selectedDataSource))
            return;
        this._isCancelled = false;
        this._numSecuritiesBeforeLookup = BaseStudioControl.SecurityProvider.Count;
        this.BusyIndicator.IsBusy = true;
        this.SetProgressSettings(LocalizedStrings.Search, (BaseEditStyleSettings)new ProgressBarMarqueeStyleSettings());
        SecurityLookupMessage criteriaMessage = wnd.CriteriaMessage;
        if (object.Equals((object)selectedDataSource, (object)this._storageDataSource))
            this.OnStorageLookup(criteriaMessage);
        else
            this.OnConnectorLookup(criteriaMessage, selectedDataSource);
    }

    private bool OnCanLookup(Tuple<string, object> type)
    {
        if (object.Equals((object)type, (object)this._storageDataSource))
            return true;
        if ((type.Item2 is IMessageAdapter imessageAdapter ? (StockSharp.Messages.Extensions.IsMessageSupported(imessageAdapter, (MessageTypes)21) ? 1 : 0) : 0) == 0)
        {
            int num = (int)new MessageBoxBuilder().Owner((DependencyObject)this).Warning().Text(LocalizedStrings.LookupSecuritiesNotSupported).Show();
            return false;
        }
        return this.CheckConnectionState();
    }

    private bool CheckConnectionState()
    {
        if (this._connector.ConnectionState == ConnectionStates.Connected)
            return true;
        return new MessageBoxBuilder().Owner((DependencyObject)this).Question().Text(StringHelper.Put("{0} {1}?", new object[2]
        {
      (object) LocalizedStrings.NoActiveConnection,
      (object) LocalizedStrings.Connect
        })).YesNo().Show() == MessageBoxResult.Yes;
    }

    private void ConfigureConnector(ICollection<Tuple<string, object>> items, bool addStorageSource)
    {
        if (this._connector.ConnectionState == ConnectionStates.Connected)
        {
            if (new MessageBoxBuilder().Owner((DependencyObject)this).Question().Text(StringHelper.Put("{0} {1}?", new object[2]
            {
        (object) LocalizedStrings.CannotEditStartedConnections,
        (object) LocalizedStrings.Disconnect
            })).YesNo().Show() != MessageBoxResult.Yes)
                return;
            this._connector.Disconnect();
        }
        ConfigureConnectorCommand command = new ConfigureConnectorCommand();
        command.Process((object)this, true);
        if (!command.Result)
            return;
        this.FillDataSources(items, addStorageSource);
    }

    private void OnConnectorConnectionError(Exception error) => this.OnComplete(error.Message, true);

    private void OnConnectorLookup(SecurityLookupMessage filter, Tuple<string, object> dataSource)
    {
        this._securityFilter = filter;
        this._dataSource = dataSource;
        this._connector.LookupSecuritiesResult += new Action<SecurityLookupMessage, IEnumerable<Security>, Exception>(this.OnLookupConnectorLookupSecuritiesResult);
        if (this._connector.ConnectionState != ConnectionStates.Connected)
        {
            this._connector.Connected += new Action(this.OnLookupConnectorConnected);
            this._connector.ConnectionError += new Action<Exception>(this.OnConnectorConnectionError);
            this._connector.Connect();
        }
        else
            this.OnLookupConnectorConnected();
    }

    private void OnLookupConnectorConnected()
    {
        SecurityLookupMessage subscriptionMessage = CloneHelper.TypedClone<SecurityLookupMessage>(this._securityFilter);
        ((Message)subscriptionMessage).Adapter = (IMessageAdapter)this._dataSource?.Item2;
        this._connector.Subscribe(new Subscription((ISubscriptionMessage)subscriptionMessage));
    }

    private void OnLookupConnectorLookupSecuritiesResult(
      SecurityLookupMessage message,
      IEnumerable<Security> newSecurities,
      Exception error)
    {
        this._connector.Connected -= new Action(this.OnLookupConnectorConnected);
        this._connector.ConnectionError -= new Action<Exception>(this.OnConnectorConnectionError);
        this._connector.LookupSecuritiesResult -= new Action<SecurityLookupMessage, IEnumerable<Security>, Exception>(this.OnLookupConnectorLookupSecuritiesResult);
        this._securityFilter = (SecurityLookupMessage)null;
        this._dataSource = (Tuple<string, object>)null;
        this.OnComplete(StringHelper.Put(LocalizedStrings.NSecAdded, new object[1]
        {
      (object) (BaseStudioControl.SecurityProvider.Count - this._numSecuritiesBeforeLookup)
        }), false);
    }

    private void DriveCtrl_OnChanged(IMarketDataDrive drive, bool isNew)
    {
        if (new MessageBoxBuilder().Text(LocalizedStrings.ImportSecurities).Question().YesNo().Owner((DependencyObject)this).Show() != MessageBoxResult.Yes)
            return;
        SecurityLookupWindow wnd = new SecurityLookupWindow()
        {
            CriteriaMessage = new SecurityLookupMessage()
            {
                SecurityTypes = this._defaultSecurityTypes
            }
        };
        if (!wnd.ShowModal((DependencyObject)this))
            return;
        this.OnStorageLookup(wnd.CriteriaMessage);
    }

    private void OnStorageLookup(SecurityLookupMessage filter)
    {
        this._isCancelled = false;
        this.BusyIndicator.IsBusy = true;
        this.SetProgressSettings(LocalizedStrings.Search, (BaseEditStyleSettings)new ProgressBarStyleSettings());
        IMarketDataDrive drive = this.SelectedDrive;
        Task.Run<int>((Func<int>)(() =>
        {
            try
            {
                int count = 0;
                ISecurityStorage securityStorage = MarketDataPanel.SecurityStorage;
                IExchangeInfoProvider exchangeInfoProvider = BaseStudioControl.ExchangeInfoProvider;
                drive.LookupSecurities(filter, (ISecurityProvider)securityStorage, (Action<SecurityMessage>)(msg =>
            {
                securityStorage.Save(msg.ToSecurity(exchangeInfoProvider), false);
                BaseStudioControl.Connector.SendOutMessage((Message)msg);
                ++count;
            }), (Func<bool>)(() => this._isCancelled), (Action<int, int>)((i, c) => ((DispatcherObject)this).GuiAsync((Action)(() => this.SetProgress(0.0, (double)c, (double)i)))));
                return count;
            }
            catch (SystemException ex)
            {
                LoggingHelper.LogError((Exception)ex, (string)null);
                throw new InvalidOperationException(LocalizedStrings.ServerUnavailable);
            }
        })).ContinueWith((Action<Task<int>>)(t =>
        {
            Exception innerException = t.Exception?.InnerException;
            if (innerException != null)
                LoggingHelper.LogError(innerException, (string)null);
            string message = innerException?.Message;
            if (message == null)
                message = StringHelper.Put(LocalizedStrings.NSecAdded, new object[1]
            {
          (object) t.Result
            });
            this.OnComplete(message, innerException != null);
        }));
    }

    private void OnComplete(string message, bool error)
    {
        GuiDispatcher.GlobalDispatcher.AddAction((Action)(() =>
        {
            this.BusyIndicator.IsBusy = false;
            if (!StringHelper.IsEmpty(message))
            {
                MessageBoxBuilder messageBoxBuilder = new MessageBoxBuilder().Owner((DependencyObject)this).Text(message);
                if (error)
                    messageBoxBuilder.Error();
                int num = (int)messageBoxBuilder.Show();
            }
            else
                this.RefreshGrid(this._lastSelectedSecurity);
        }));
    }

    private void SetProgressSettings(string title, BaseEditStyleSettings settings)
    {
        this._progressData.Title = title;
        this._progressData.Settings = settings;
    }

    private void SetProgress(double minimum, double maximum, double value)
    {
        this._progressData.Minimum = minimum;
        this._progressData.Value = value;
        this._progressData.Maximum = maximum;
    }

    private void Candles_OnClick(object sender, RoutedEventArgs e) => this.ShowCandlesPopup();

    private void Candles_OnMouseEnter(object sender, MouseEventArgs e) => this.ShowCandlesPopup();

    private void ShowCandlesPopup()
    {
        this.Candles.IsPopupOpen = false;
        this.Candles.IsPopupOpen = true;
    }

    private void FillDataSources(ICollection<Tuple<string, object>> items, bool addStorageSource)
    {
        items.Clear();
        if (addStorageSource)
            items.Add(this._storageDataSource);
        foreach (IMessageAdapter sortedAdapter in this._connector.Adapter.InnerAdapters.SortedAdapters)
            items.Add(new Tuple<string, object>(sortedAdapter.ToString(), (object)sortedAdapter));
    }

    public override void Load(SettingsStorage storage)
    {
        this._isLoading = true;
        try
        {
            base.Load(storage);
            this.DateTimeFrom.DateTime = storage.GetValue<DateTime>("DateTimeFrom", this.DateTimeFrom.DateTime);
            this.DateTimeTo.DateTime = storage.GetValue<DateTime>("DateTimeTo", this.DateTimeTo.DateTime);
            this.TimeoutSeconds.Value = (Decimal)storage.GetValue<int>("TimeoutSeconds", (int)MarketDataPanel.DataDownloader.DefaultDataTimeout.TotalSeconds);
            this.StorageFormat = storage.GetValue<StorageFormats>("StorageFormat", this.StorageFormat);
            this.SelectedDrive = ServicesRegistry.DriveCache.GetDrive(storage.GetValue<string>("SelectedDrive", (string)null));
            CollectionHelper.ForEach<MarketDataPanel.SelectableObject>((IEnumerable<MarketDataPanel.SelectableObject>)this._allCandleTimeFrames, (Action<MarketDataPanel.SelectableObject>)(t => t.IsSelected = false));
            foreach (TimeSpan timeSpan in storage.GetValue<IEnumerable<TimeSpan>>("LoadCandles", Enumerable.Empty<TimeSpan>()))
            {
                TimeSpan period = timeSpan;
                ((IEnumerable<MarketDataPanel.SelectableObject>)this._allCandleTimeFrames).First<MarketDataPanel.SelectableObject>((Func<MarketDataPanel.SelectableObject, bool>)(t => t.TimeFrame == period)).IsSelected = true;
            }
            IEnumerable<string> source = storage.GetValue<IEnumerable<string>>("Securities", Enumerable.Empty<string>());
            ISecurityStorage secStorage = MarketDataPanel.SecurityStorage;
            Func<string, Security> selector = (Func<string, Security>)(id => secStorage.LookupById(id));
            this.SelectSecurities(CollectionHelper.WhereNotNull<Security>(source.Select<string, Security>(selector)).ToArray<Security>());
            this.FormatCtrl.SelectedFormat = storage.GetValue<StorageFormats>("StorageFormat", this.FormatCtrl.SelectedFormat);
            storage.TryLoad("SecuritiesAll", new Action<SettingsStorage>(this.SecuritiesAll.Load));
            storage.TryLoad("SecuritiesSelected", new Action<SettingsStorage>(this.SecuritiesSelected.Load));
            storage.TryLoad("MarketDataGrid", new Action<SettingsStorage>(((BaseGridControl)this.MarketDataGrid).Load));
            storage.TryLoad<string>("ExtendedInfo", (Action<string>)(s =>
            {
                if (StringHelper.IsEmpty(s))
                    return;
                this._extendedStorage = MarketDataPanel.ExtendedInfoStorage.Get(s);
                this.ApplyExtendedStorage();
            }));
            this._mappingSecurityWindowSettings = storage.GetValue<SettingsStorage>("SecurityMappingWindow", (SettingsStorage)null);
        }
        finally
        {
            this._isLoading = false;
        }
        this.RefreshGrid(this.SecuritiesSelected.SelectedSecurity ?? this.SecuritiesAll.SelectedSecurity);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.SetValue<DateTime>("DateTimeFrom", this.DateTimeFrom.DateTime);
        storage.SetValue<DateTime>("DateTimeTo", this.DateTimeTo.DateTime);
        storage.SetValue<int>("TimeoutSeconds", Converter.To<int>((object)this.TimeoutSeconds.Value));
        storage.SetValue<StorageFormats>("StorageFormat", this.StorageFormat);
        if (this.SelectedDrive != null)
            storage.SetValue<string>("SelectedDrive", this.SelectedDrive.Path);
        storage.SetValue<TimeSpan[]>("LoadCandles", ((IEnumerable<MarketDataPanel.SelectableObject>)this._allCandleTimeFrames).Where<MarketDataPanel.SelectableObject>((Func<MarketDataPanel.SelectableObject, bool>)(t => t.IsSelected)).Select<MarketDataPanel.SelectableObject, TimeSpan>((Func<MarketDataPanel.SelectableObject, TimeSpan>)(t => t.TimeFrame)).ToArray<TimeSpan>());
        storage.SetValue<string[]>("Securities", this.SecuritiesSelected.Securities.LookupAll().Select<Security, string>((Func<Security, string>)(s => s.Id)).ToArray<string>());
        storage.SetValue<StorageFormats>("StorageFormat", this.FormatCtrl.SelectedFormat);
        storage.SetValue<SettingsStorage>("SecuritiesAll", PersistableHelper.Save((IPersistable)this.SecuritiesAll));
        storage.SetValue<SettingsStorage>("SecuritiesSelected", PersistableHelper.Save((IPersistable)this.SecuritiesSelected));
        storage.SetValue<SettingsStorage>("MarketDataGrid", PersistableHelper.Save((IPersistable)this.MarketDataGrid));
        storage.SetValue<string>("ExtendedInfo", this._extendedStorage?.StorageName);
        storage.SetValue<SettingsStorage>("SecurityMappingWindow", this._mappingSecurityWindowSettings);
    }

    private void SettingsChanged()
    {
        if (this._isLoading)
            return;
        this.RaiseChangedCommand();
        this.RefreshGrid(this._lastSelectedSecurity);
    }

    private void SecurityPicker_OnSecuritySelected(Security security)
    {
        if (this._isLoading)
            return;
        this.RefreshGrid(security);
    }

    private void RefreshGrid(Security security)
    {
        if (this.DriveCtrl == null || this.FormatCtrl == null)
            return;
        this._lastSelectedSecurity = security;
        this.MarketDataGrid.BeginMakeEntries(ServicesRegistry.StorageRegistry, security != null ? new SecurityId?(security.ToSecurityId()) : new SecurityId?(), this.StorageFormat, this.SelectedDrive);
    }

    private void Grid_OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        this.RaiseChangedCommand();
    }

    private void ExtendedInfo_OnClick(object sender, RoutedEventArgs e)
    {
        if (this.SecuritiesAll.ExtendedInfoStorage != null)
        {
            this.SecuritiesAll.ExtendedInfoStorage = (IExtendedInfoStorageItem)null;
            this.ExtendedInfo.Content = (object)LocalizedStrings.ExtendedInfo;
            this._extendedStorage = (IExtendedInfoStorageItem)null;
        }
        else
        {
            ExtendedInfoStorageWindow wnd = new ExtendedInfoStorageWindow();
            if (!wnd.ShowModal((DependencyObject)this))
                return;
            this._extendedStorage = wnd.SelectedStorage;
            this.ApplyExtendedStorage();
        }
    }

    private void OnExtendedInfoStorageDeleted(IExtendedInfoStorageItem storage)
    {
        ((DispatcherObject)this).GuiAsync((Action)(() =>
        {
            if (this.SecuritiesAll.ExtendedInfoStorage != storage)
                return;
            this.SecuritiesAll.ExtendedInfoStorage = (IExtendedInfoStorageItem)null;
        }));
    }

    private void ApplyExtendedStorage()
    {
        this.SecuritiesAll.ExtendedInfoStorage = this._extendedStorage;
        this.ExtendedInfo.Content = (object)$"{LocalizedStrings.ExtendedInfo}: {this._extendedStorage?.StorageName}";
    }

    private void SecurityMappings_OnClick(object sender, RoutedEventArgs e)
    {
        SecurityMappingWindow wnd = new SecurityMappingWindow();
        if (this._mappingSecurityWindowSettings != null)
            wnd.Load(this._mappingSecurityWindowSettings);
        foreach (IMessageAdapter possibleAdapter in ServicesRegistry.AdapterProvider.PossibleAdapters)
            wnd.ConnectorsInfo.Add(new ConnectorInfo(possibleAdapter));
        wnd.Storage = ServicesRegistry.MappingStorage;
        wnd.ShowModal((DependencyObject)this);
        this._mappingSecurityWindowSettings = PersistableHelper.Save((IPersistable)wnd);
        this.RaiseChangedCommand();
    }

    private void DriveCtrl_OnEditValueChanged(object sender, EditValueChangedEventArgs e)
    {
        this.SettingsChanged();
    }

    private void FormatCtrl_OnEditValueChanged(object sender, EditValueChangedEventArgs e)
    {
        this.SettingsChanged();
    }

    private void DateTime_EditValueChanged(object sender, EditValueChangedEventArgs e)
    {
        this.RaiseChangedCommand();
    }



    private class SelectableObject : NotifiableObject
    {
        private readonly MarketDataPanel _parent;
        private bool _isSelected;

        public SelectableObject(MarketDataPanel parent, StockSharp.Messages.DataType value, TimeSpan timeFrame)
        {
            if (parent == null)
                throw new ArgumentNullException(nameof(parent));
            this._parent = parent;

            if (value == null)
                throw new ArgumentNullException(nameof(value));
            this.Value = value;
            this.TimeFrame = timeFrame;
        }

        public StockSharp.Messages.DataType Value { get; }

        public TimeSpan TimeFrame { get; }

        public bool IsSelected
        {
            get => this._isSelected;
            set
            {
                this._isSelected = value;
                this.NotifyChanged(nameof(IsSelected));
                this._parent.RaiseChangedCommand();
            }
        }
    }

    private class ProgressData : DevExpress.Mvvm.ViewModelBase
    {
        public double Minimum
        {
            get => this.GetProperty<double>((Expression<Func<double>>)(() => this.Minimum));
            set => this.SetProperty<double>((Expression<Func<double>>)(() => this.Minimum), value);
        }

        public double Maximum
        {
            get => this.GetProperty<double>((Expression<Func<double>>)(() => this.Maximum));
            set => this.SetProperty<double>((Expression<Func<double>>)(() => this.Maximum), value);
        }

        public double Value
        {
            get => this.GetProperty<double>((Expression<Func<double>>)(() => this.Value));
            set => this.SetProperty<double>((Expression<Func<double>>)(() => this.Value), value);
        }

        public BaseEditStyleSettings Settings
        {
            get
            {
                return this.GetProperty<BaseEditStyleSettings>((Expression<Func<BaseEditStyleSettings>>)(() => this.Settings));
            }
            set
            {
                this.SetProperty<BaseEditStyleSettings>((Expression<Func<BaseEditStyleSettings>>)(() => this.Settings), value);
            }
        }

        public string Title
        {
            get => this.GetProperty<string>((Expression<Func<string>>)(() => this.Title));
            set => this.SetProperty<string>((Expression<Func<string>>)(() => this.Title), value);
        }
    }

    private class DataDownloader : Disposable
    {
        public static readonly TimeSpan DefaultDataTimeout = TimeSpan.FromSeconds(60.0);
        private static readonly TimeSpan _unsubscribeTimeout = TimeSpan.FromSeconds(5.0);
        private static readonly TimeSpan _storageDelay = TimeSpan.FromSeconds(11.0);
        private readonly AsyncManualResetEvent _connected = new AsyncManualResetEvent();
        private readonly MarketDataPanel _parent;
        private readonly CancellationTokenSource _cts;
        private readonly TaskCompletionSource<object> _downloadTcs = new TaskCompletionSource<object>();
        private readonly TaskCompletionSource<object> _downloadWithCancelTcs = new TaskCompletionSource<object>();
        private readonly MarketDataPanel.DataDownloader.DataInfo[] _infos;
        private readonly TimeSpan _dataTimeout;
        private DateTime _lastReceiveTime;
        private bool _isDisposing;

        private Connector Connector => this._parent._connector;

        private StorageFormats Format { get; }

        private IMarketDataDrive Drive { get; }

        public Task DownloadTask => (Task)this._downloadWithCancelTcs.Task;

        public DataDownloader(MarketDataPanel parent, IEnumerable<Subscription> subscriptions, CancellationToken token)
        {
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));
            _dataTimeout = TimeSpan.FromSeconds((double)Converter.To<int>((object)this._parent.TimeoutSeconds.Value));
            this.Format = parent.StorageFormat;
            this.Drive = parent.SelectedDrive;
            this.Connector.SubscriptionReceived += OnNewMessage;
            this.Connector.SubscriptionStopped += OnMarketDataSubscriptionStopped;
            this.Connector.SubscriptionFailed += OnSubscriptionFailed;
            this.Connector.Connected += OnConnected;
            this.Connector.ConnectionError += OnConnectorConnectionError;
            this.UpdateLastRecieveTime();
            this._cts = CancellationTokenSource.CreateLinkedTokenSource(token);
            this._infos = subscriptions.Select(sub => new MarketDataPanel.DataDownloader.DataInfo(this, sub)).ToArray();
            Task.Run(new Func<Task>(this.DownloadWithCancellationAsync), this._cts.Token);
            Task.Run(new Func<Task>(this.HandleTimeoutAsync), this._cts.Token);
        }

        protected override void DisposeManaged()
        {
            this._isDisposing = true;
            this._cts.Cancel();
            this.Connector.SubscriptionReceived -= new Action<Subscription, object>(this.OnNewMessage);
            this.Connector.SubscriptionStopped -= new Action<Subscription, Exception>(this.OnMarketDataSubscriptionStopped);
            this.Connector.SubscriptionFailed -= new Action<Subscription, Exception, bool>(this.OnSubscriptionFailed);
            this.Connector.Connected -= new Action(this.OnConnected);
            this.Connector.ConnectionError -= new Action<Exception>(this.OnConnectorConnectionError);
            base.DisposeManaged();
        }

        private void UpdateLastRecieveTime() => this._lastReceiveTime = DateTime.UtcNow;

        private void OnConnected()
        {
            this.UpdateLastRecieveTime();
            this._connected.Set();
        }

        private async Task HandleTimeoutAsync()
        {
            try
            {
                while (true)
                {
                    TimeSpan timeSpan = this._lastReceiveTime + this._dataTimeout - DateTime.UtcNow;
                    if (timeSpan > TimeSpan.Zero)
                        await AsyncHelper.Delay(timeSpan, this._cts.Token);
                    else
                        break;
                }
                LogManager logManager = ServicesRegistry.LogManager;
                if (logManager != null)
                    LoggingHelper.AddErrorLog(logManager.Application, "DataDownloader: operation time out", Array.Empty<object>());
                this._downloadTcs.TrySetException((Exception)new InvalidOperationException(LocalizedStrings.TimeOut));
                this._cts.Cancel();
            }
            catch (OperationCanceledException ex)
            {
            }
            catch (Exception ex)
            {
                LoggingHelper.LogError(ex, (string)null);
            }
        }

        private async Task DownloadWithCancellationAsync()
        {
            try
            {
                this._cts.Token.ThrowIfCancellationRequested();
                await this.DownloadAsync();
                this._downloadTcs.TrySetResult(null);
            }
            catch (OperationCanceledException ex)
            {
                this._downloadTcs.TrySetCanceled();
            }
            catch (Exception ex)
            {
                LoggingHelper.LogError(ex, (string)null);
                this._downloadTcs.TrySetException(ex);
            }
            try
            {
                await this.UnsubscribeAsync();
            }
            catch (TaskCanceledException ex)
            {
            }
            catch (Exception ex)
            {
                LoggingHelper.LogError(ex, (string)null);
            }
            finally
            {
                TaskCompletionSourceExtensions.TryCompleteFromCompletedTask<object, object>(this._downloadWithCancelTcs, this._downloadTcs.Task);
            }
        }

        private async Task DownloadAsync()
        {
            if (this.Connector.ConnectionState != ConnectionStates.Connected)
            {
                Task task = _connected.WaitAsync(_cts.Token);
                this.Connector.Connect();
                await task;
            }

            foreach (var info in _infos)
            {
                this.Connector.Subscribe(info.Subscription);
                info.IsSubscribed = true;
            }

            throw new NotImplementedException();

            //await TaskExtensions.WhenAll( _infos.Select(new Func<MarketDataPanel.DataDownloader.DataInfo, Task>(dataDownloader.\u003CDownloadAsync\u003Eb__29_0)));

            //_cts.Token.ThrowIfCancellationRequested();

            //if (Connector.Adapter.StorageSettings.Drive == Drive)
            //{
            //    await AsyncHelper.Delay(MarketDataPanel.DataDownloader._storageDelay, _cts.Token);
            //}
            //else
            //{
            //    _infos.ForEach(i => i.Flush());
            //}

        }

        private async Task UnsubscribeAsync()
        {
            if (this._isDisposing)
                return;
            var activeSubs = _infos.Where((i => i.IsSubscribed && !i.SubscriptionIsDone.IsSet)).ToArray();

            foreach (MarketDataPanel.DataDownloader.DataInfo dataInfo in activeSubs)
                this.Connector.UnSubscribe(dataInfo.Subscription);

            CancellationTokenSource timeout = new CancellationTokenSource(MarketDataPanel.DataDownloader._unsubscribeTimeout);

            await Task.WhenAll(activeSubs.Select(i => i.SubscriptionIsDone.WaitAsync(timeout.Token)));
        }

        private void OnConnectorConnectionError(Exception error)
        {
            this._downloadTcs.TrySetException(error);
            this._cts.Cancel();
        }

        private void OnNewMessage(Subscription subscription, object arg)
        {
            this.UpdateLastRecieveTime();
            if (!(arg is Message message))
                return;
            this._infos.FirstOrDefault((i => i.Subscription == subscription))?.AddMessage(message);
        }

        private void OnSubscriptionFailed(Subscription subscription, Exception error, bool isSubscribe)
        {
            this.OnSubscriptionDone(subscription, error);
        }

        private void OnMarketDataSubscriptionStopped(Subscription subscription, Exception error)
        {
            this.OnSubscriptionDone(subscription, error);
        }

        private void OnSubscriptionDone(Subscription subscription, Exception error)
        {
            MarketDataPanel.DataDownloader.DataInfo dataInfo = ((IEnumerable<MarketDataPanel.DataDownloader.DataInfo>)this._infos).FirstOrDefault<MarketDataPanel.DataDownloader.DataInfo>((Func<MarketDataPanel.DataDownloader.DataInfo, bool>)(i => i.Subscription == subscription));
            if (dataInfo == null)
                return;
            dataInfo.SubscriptionIsDone.Set();
            if (error != null)
                return;
            this.UpdateLastRecieveTime();
        }

        private class DataInfo(MarketDataPanel.DataDownloader parent, Subscription subscription)
        {
            private readonly SynchronizedList<Message> _messages = new SynchronizedList<Message>();
            private readonly MarketDataPanel.DataDownloader _parent = parent ?? throw new ArgumentNullException(nameof(parent));

            public Subscription Subscription { get; } = subscription ?? throw new ArgumentNullException(nameof(subscription));

            public bool IsSubscribed { get; set; }

            public AsyncManualResetEvent SubscriptionIsDone { get; } = new AsyncManualResetEvent();

            public void AddMessage(Message message)
            {
                this._messages.Add(message);
                if (this._messages.Count <= 1000)
                    return;

                this.Flush();
            }

            public void Flush()
            {
                Message[] data = this._messages.CopyAndClear();

                if (data.Length == 0)
                    return;

                ServicesRegistry.StorageRegistry.GetStorage(this.Subscription.SecurityId.Value, this.Subscription.DataType, this._parent.Drive, this._parent.Format).Save((IEnumerable<Message>)data);
            }
        }
    }
}
