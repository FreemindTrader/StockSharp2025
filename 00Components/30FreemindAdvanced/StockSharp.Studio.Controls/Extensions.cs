// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.Extensions
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;
using DevExpress.Xpf.Bars;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Configuration;
using Ecng.Interop;
using Ecng.Logging;
using Ecng.Net;
using Ecng.Serialization;
using Ecng.Xaml;
using Ecng.Xaml.Converters;
using StockSharp.Alerts;
using StockSharp.Algo;
using StockSharp.Algo.Export;
using StockSharp.Algo.Indicators;
using StockSharp.Algo.Strategies.Reporting;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Configuration;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Controls.Commands;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using StockSharp.Studio.Core.Configuration;
using StockSharp.Studio.Core.Services;
using StockSharp.Studio.IPC;
using StockSharp.Studio.WebApi;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.Common;
using StockSharp.Web.DomainModel;
using StockSharp.Xaml;
using StockSharp.Xaml.Charting;
using StockSharp.Xaml.Charting.IndicatorPainters;
using StockSharp.Xaml.PropertyGrid;

#nullable enable
namespace StockSharp.Studio.Controls;

public static class Extensions
{
    public static void RegisterUIServices()
    {
        ConfigManager.RegisterService<IDispatcher>((IDispatcher)GuiDispatcher.GlobalDispatcher);
        ConfigManager.RegisterService<IIndicatorProvider>((IIndicatorProvider)new IndicatorProvider());
        ConfigManager.RegisterService<IChartIndicatorPainterProvider>((IChartIndicatorPainterProvider)new ChartIndicatorPainterProvider());
        ConfigManager.RegisterService<IExcelWorkerProvider>((IExcelWorkerProvider)new DevExpExcelWorkerProvider());
        ConfigManager.RegisterService<IOAuthProvider>((IOAuthProvider)new OAuthProvider());
        // ISSUE: object of a compiler-generated type is created
        ConfigManager.RegisterService<IReportGeneratorProvider>(new ReportGeneratorProvider(new IReportGenerator[4]
                                                                                                                                {
                                                                                                                                new ExcelReportGenerator(ServicesRegistry.ExcelProvider),
                                                                                                                                new XmlReportGenerator(),
                                                                                                                                new CsvReportGenerator(),
                                                                                                                                 new JsonReportGenerator()
                                                                                                                                }));
        ConfigManager.RegisterService<IIndicatorColorProvider>((IIndicatorColorProvider)new IndicatorColorProvider());
        StudioHelper.RegisterCompilerCache();
        IChartExtensions.IndicatorProvider.Init();
        IChartExtensions.IndicatorPainterProvider.Init();
        ConfigManager.RegisterService<ILastDirSelector>((ILastDirSelector)new LastDirSelector((IPersistableService)StudioUserConfig.Instance));
        PropertyGridEx.CustomEditors.Add(typeof(ITelegramChannelEditor), typeof(ClientSocialComboBoxEditor));
    }

    public static
#nullable disable
    BarButtonItem CreateToolControl<T>(this object sender) where T : IStudioControl
    {
        return new ControlType(typeof(T)).CreateToolControl(sender, (Action<Type>)null);
    }

    public static BarButtonItem CreateToolControl(
      this ControlType controlType,
      object sender,
      Action<Type> customHandler)
    {
        if (controlType == null)
            throw new ArgumentNullException(nameof(controlType));
        if (sender == null)
            throw new ArgumentNullException(nameof(sender));
        BarButtonItem barButtonItem = new BarButtonItem();
        barButtonItem.Glyph = controlType.Icon;
        BarButtonItem toolControl = barButtonItem;
        toolControl.SetBinding(BarItem.ContentProperty, (BindingBase)new Binding()
        {
            Source = (object)controlType,
            Path = new PropertyPath("Name", Array.Empty<object>())
        });
        toolControl.SetBinding(FrameworkContentElement.ToolTipProperty, (BindingBase)new Binding()
        {
            Source = (object)controlType,
            Path = new PropertyPath("Description", Array.Empty<object>())
        });
        toolControl.SetBinding(BarItem.LargeGlyphProperty, (BindingBase)new ThemedIconBinding((DrawingImage)controlType.Icon));
        Type type = controlType.Type;
        bool isToolWindow = controlType.IsToolWindow;
        if (customHandler == null)
            toolControl.ItemClick += (ItemClickEventHandler)((s, e) => new OpenWindowCommand(type, isToolWindow).Process(sender));
        else
            toolControl.ItemClick += (ItemClickEventHandler)((s, e) => customHandler(type));
        return toolControl;
    }

    private static async Task<Client> TryLogin(this Window owner, CancellationToken token)
    {
        if (owner == null)
            throw new ArgumentNullException(nameof(owner));
        ICredentialsProvider credProvider = ConfigurationServicesRegistry.CredentialsProvider;
        ServerCredentials credentials = (ServerCredentials)null;
        bool autoLogon = true;
        bool flag1 = LoggingHelper.DoWithLog<bool>((Func<bool>)(() => !credProvider.TryLoad(out credentials)));
        Client currentAsync;
        while (true)
        {
            if (flag1)
            {
                (ServerCredentials serverCredentials, bool flag2) = await CredentialsWindow.TryShow((DependencyObject)owner, credentials);
                if (serverCredentials != null)
                {
                    credentials = serverCredentials;
                    autoLogon = flag2;
                    credProvider.Save(credentials, autoLogon);
                }
                else
                    break;
            }
            try
            {
                currentAsync = await WebApiServicesRegistry.GetService<IClientService>().GetCurrentAsync(cancellationToken: token);
                goto label_11;
            }
            catch (Exception ex)
            {
                int num = (int)new MessageBoxBuilder().Text(ex.Message).Error().Owner(owner).Show();
                LoggingHelper.LogError(ex, (string)null);
                flag1 = true;
            }
        }
        owner.Close();
        return (Client)null;
    label_11:
        string accessToken = currentAsync.GetAccessToken();
        if (!StringHelper.IsEmpty(accessToken))
        {
            credentials.Token = StringHelper.Secure(accessToken);
            credentials.Password = (SecureString)null;
            LoggingHelper.DoWithLog((Action)(() => credProvider.Save(credentials, autoLogon)));
        }
        return currentAsync;
    }

    public static ValueTask SubscribeNews(this IWebSocketService socket, Window owner, CancellationToken cancellationToken = default(CancellationToken))
    {
        if (socket == null)
            throw new ArgumentNullException(nameof(socket));
        if (owner == null)
            throw new ArgumentNullException(nameof(owner));

        StockSharp.BusinessEntities.Subscription subscription = new StockSharp.BusinessEntities.Subscription(StockSharp.Messages.DataType.News);

        return socket.SubscribeAsync<NewsRequest, NewsResponse>(new NewsRequest(), (Func<NewsResponse, CancellationToken, ValueTask>)((r, t) =>
        {
            string text = r.Text;
            new EntityCommand<News>(subscription, new News()
            {
                Headline = text,
                ExpiryDate = new DateTimeOffset?(DateTimeOffset.UtcNow.AddDays(1.0)),
                Story = text,
                Id = Converter.To<string>((object)r.EntityId)
            }).Process((object)owner);
            return new ValueTask();
        }), cancellationToken);
    }

    public static void ProcessLogInCommand(this Window owner, CancellationToken token)
    {
        if (WebApiServicesRegistry.Offline)
            new LoggedInCommand(WebApiHelper.Profile).Process((object)owner);
        else
            owner.TryLogin(token).ContinueWith((Action<Task<Client>>)(t =>
            {
                if (t.IsFaulted)
                {
                    LoggingHelper.LogError((Exception)t.Exception, (string)null);
                }
                else
                {
                    Client result = t.Result;
                    if (result == null)
                    {
                        owner.Close();
                    }
                    else
                    {
                        WebApiHelper.Profile = result;
                        new LoggedInCommand(result).Process((object)owner);
                    }
                }
            }), token);
    }

    private static StudioUserConfig Config => StudioUserConfig.Instance;

    public static bool TryResetSettings(this Window owner, Action postAction = null)
    {
        if (owner == null)
            throw new ArgumentNullException(nameof(owner));
        if (new MessageBoxBuilder().Text(StringHelper.Put(LocalizedStrings.SettingsWillBeResetContinue, new object[1]
        {
      (object) Paths.AppName
        })).Warning().Owner(owner).YesNo().Show() != MessageBoxResult.Yes)
            return false;
        if (postAction != null)
            postAction();
        AlertServicesRegistry.TryNotificationService?.Dispose();
        AlertServicesRegistry.TryProcessingService?.Dispose();
        Extensions.Config.ResetSettings();
        owner.Restart();
        return true;
    }

    public static bool InitStockSharpConnections(this Connector connector, Window owner) => false;

    public static void TryAddEntity<TEntity>(this IList<Position> entity, EntityCommand<TEntity> cmd) where TEntity : Position
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        if (cmd == null)
            throw new ArgumentNullException(nameof(cmd));
        if ((object)cmd.Entity == null)
            return;
        CollectionHelper.TryAdd<Position>((ICollection<Position>)entity, (Position)cmd.Entity);
    }

    public static void AddEntity<TEntity>(this IList<TEntity> entity, EntityCommand<TEntity> cmd) where TEntity : class
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        if (cmd == null)
            throw new ArgumentNullException(nameof(cmd));
        if ((object)cmd.Entity == null)
            return;
        entity.Add(cmd.Entity);
    }

    public static void TryAddEntities<TEntity>(
      this IList<TEntity> entities,
      EntityCommand<TEntity> cmd)
      where TEntity : class
    {
        if (entities == null)
            throw new ArgumentNullException(nameof(entities));
        if (cmd == null)
            throw new ArgumentNullException(nameof(cmd));
        if ((object)cmd.Entity == null)
            return;
        CollectionHelper.TryAdd<TEntity>((ICollection<TEntity>)entities, cmd.Entity);
    }

    public static bool CanCloseOrRevert(this Position position)
    {
        switch (position)
        {
            case null:
            case Portfolio _:
                return false;
            default:
                Decimal? currentValue = position.CurrentValue;
                Decimal num = 0M;
                return !(currentValue.GetValueOrDefault() == num & currentValue.HasValue);
        }
    }

    public static void ShowCommonSettings<TSettings>(
      this Window owner,
      Action<TSettings> applyChanges = null)
      where TSettings : StudioCommonSettings, new()
    {
        if (owner == null)
            throw new ArgumentNullException(nameof(owner));
        StudioUserConfig instance = StudioUserConfig.Instance;
        TSettings commonSettings = instance.GetCommonSettings<TSettings>();
        commonSettings.IsDark = ThemeExtensions.IsCurrDark();
        if (!commonSettings.ShowSettingsWindow<TSettings>((DependencyObject)owner))
            return;
        instance.SetCommonSettings<TSettings>(commonSettings);
        if (applyChanges != null)
            applyChanges(commonSettings);
        if (ThemeExtensions.IsCurrDark() != commonSettings.IsDark)
            ThemeExtensions.Invert();
        commonSettings.UpdateGlobalTimeZone();
        commonSettings.UpdateErrorsDialogs();
    }

    public static void UpdateGlobalTimeZone(this StudioCommonSettings settings)
    {
        TimeConverter.GlobalTimeZone = settings != null ? settings.TimeZone : throw new ArgumentNullException(nameof(settings));
    }

    public static void UpdateErrorsDialogs(this StudioCommonSettings settings)
    {
        ((BaseApplication)Application.Current).ShowExceptions = settings != null ? settings.ErrorsDialogs : throw new ArgumentNullException(nameof(settings));
    }

    public static async Task InitAlerts(CancellationToken cancellationToken)
    {
        try
        {
            int maxQueue1 = ConfigManager.TryGet<int?>("alertsMaxProcess", new int?()) ?? 10;
            int maxQueue2 = ConfigManager.TryGet<int?>("alertsMaxNotify", new int?()) ?? 3;
            ConfigManager.RegisterService<IAlertProcessingService>((IAlertProcessingService)new AlertProcessingService(maxQueue1));
            string appDataPath = Paths.AppDataPath;
            WebApiAlertNotificationService externalProvider = new WebApiAlertNotificationService();
            AlertNotificationService notificationService = new AlertNotificationService(maxQueue2, appDataPath, (IAlertNotificationService)externalProvider);
            ConfigManager.RegisterService<IAlertNotificationService>((IAlertNotificationService)notificationService);
            ConfigManager.RegisterService<IDesktopPopupService>((IDesktopPopupService)notificationService);
            ServicesRegistry.LogManager.Sources.Add((ILogSource)AlertServicesRegistry.ProcessingService);
            ServicesRegistry.LogManager.Sources.Add((ILogSource)AlertServicesRegistry.NotificationService);
            IAlertProcessingService alertService = AlertServicesRegistry.ProcessingService;
            SettingsStorage settings = StudioUserConfig.Instance.GetAlertService();
            if (settings != null)
                Do.Invariant((Action)(() => ((IPersistable)alertService).Load(settings)));
            alertService.Registered += new Action<AlertSchema>(AlertsChanged);
            alertService.UnRegistered += new Action<AlertSchema>(AlertsChanged);
            if (WebApiServicesRegistry.Offline)
                return;
            await WebApiServicesRegistry.GetService<IClientSocialService>().RefreshAsync(cancellationToken);

            void AlertsChanged(AlertSchema schema)
            {
                StudioUserConfig.Instance.SetAlertService(PersistableHelper.Save((IPersistable)alertService));
            }
        }
        catch (Exception ex)
        {
            LoggingHelper.LogError(ex, (string)null);
        }
    }

    public static bool TryConnect(this Connector connector, Window owner)
    {
        if (connector == null)
            throw new ArgumentNullException(nameof(connector));
        if (owner == null)
            throw new ArgumentNullException(nameof(owner));
        IInnerAdapterList innerAdapters = connector.Adapter.InnerAdapters;
        if (CollectionHelper.IsEmpty<IMessageAdapter>((ICollection<IMessageAdapter>)innerAdapters))
        {
            int num = (int)new MessageBoxBuilder().Owner(owner).Text(LocalizedStrings.AtLeastOneConnectionMustBe).Warning().Show();
            if (!connector.ConfigureConnector(owner))
                return false;
        }
        if (CollectionHelper.IsEmpty<IMessageAdapter>(innerAdapters.SortedAdapters))
        {
            int num = (int)new MessageBoxBuilder().Owner(owner).Text(LocalizedStrings.AtLeastOneConnectionActive).Warning().Show();
            if (!connector.ConfigureConnector(owner))
                return false;
        }
        connector.Connect();
        return true;
    }

    public static bool ConfigureConnector(this Connector connector, Window owner)
    {
        if (connector == null)
            throw new ArgumentNullException(nameof(connector));
        if (owner == null)
            throw new ArgumentNullException(nameof(owner));
        StudioUserConfig instance = StudioUserConfig.Instance;
        bool autoConnect = instance.GetAutoConnect();
        SettingsStorage connectorWindow = instance.GetConnectorWindow();
        int num = connector.Adapter.Configure(owner, ref autoConnect, ref connectorWindow) ? 1 : 0;
        instance.SetConnectorWindow(connectorWindow);
        if (num == 0)
            return false;
        instance.SetConnector(PersistableHelper.Save((IPersistable)connector));
        instance.SetAutoConnect(autoConnect);
        return true;
    }

    public static StudioChannel CreateChannel(
      this LogManager logManager,
      Func<StudioMessage, CancellationToken, ValueTask<StudioMessage>> handler,
      DependencyObject owner)
    {
        if (logManager == null)
            throw new ArgumentNullException(nameof(logManager));
        if (owner == null)
            throw new ArgumentNullException(nameof(owner));
        return new StudioChannel(WebApiHelper.ProductId, Paths.CreateSerializer<SettingsStorage>(true), handler, (ILogSource)logManager.Application, new Func<bool>(receiver));

        bool receiver()
        {
            return ((DispatcherObject)owner).GuiSync<bool>((Func<bool>)(() =>
            {
                if (new MessageBoxBuilder().Owner(owner).Text(LocalizedStrings.InstallerNotStarted).Warning().YesNo().Show() != MessageBoxResult.Yes)
                    return false;
                string installedPath = Paths.TryGetInstalledPath(16L /*0x10*/);
                if (StringHelper.IsEmpty(installedPath))
                {
                    int num = (int)new MessageBoxBuilder().Owner(owner).Text(StringHelper.Put(LocalizedStrings.AppNotFound, new object[1]
              {
            (object) LocalizedStrings.Installer
                })).Warning().Show();
                    return false;
                }
                return Process.Start(new ProcessStartInfo()
                {
                    UseShellExecute = true,
                    FileName = "StockSharp.Installer.UI.exe",
                    WorkingDirectory = installedPath
                }) != null;
            }));
        }
    }

    public static bool HideBars { get; set; }

    public static void TryHideBar(this IBarManagerControl ctrl)
    {
        if (ctrl == null)
            throw new ArgumentNullException(nameof(ctrl));
        if (!Extensions.HideBars)
            return;
        ctrl.Bar.Visibility = Visibility.Collapsed;
    }

    public static void InvertThemeAndSave()
    {
        ThemeExtensions.Invert();
        StudioUserConfig instance = StudioUserConfig.Instance;
        StudioCommonSettings commonSettings = instance.GetCommonSettings<StudioCommonSettings>();
        commonSettings.IsDark = ThemeExtensions.IsCurrDark();
        instance.SetCommonSettings<StudioCommonSettings>(commonSettings);
    }
}
