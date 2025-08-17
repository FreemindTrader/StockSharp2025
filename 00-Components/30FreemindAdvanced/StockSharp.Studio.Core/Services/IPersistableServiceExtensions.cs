using Ecng.Logging;
using Ecng.Serialization;
using StockSharp.Algo.Storages;
using StockSharp.Algo.Testing;
using System;

#nullable disable
namespace StockSharp.Studio.Core.Services;

public static class IPersistableServiceExtensions
{
    private const string _themeKey = "Theme";
    private const string _mainWindowKey = "MainWindow";
    private const string _studioSessionKey = "StudioSession";
    private const string _autoConnectKey = "AutoConnect";
    private const string _driveCacheKey = "DriveCache";
    private const string _layoutKey = "Layout";
    private const string _ribbonKey = "Ribbon";
    private const string _nextTimeFeedback = "NextTimeFeedback";
    private const string _suggestionsKey = "DoNotShowSuggestionsWindow";
    private const string _alertServiceKey = "AlertService";
    private const string _isFirstRunKey = "IsFirstRun";
    private const string _storageFormatKey = "StorageFormat";
    private const StorageFormats _defaultFormat = StorageFormats.Binary;
    private const string _connectorKey = "Connector";
    private const string _connectorWindowKey = "ConnectorWindow";
    private const string _riskManagerKey = "RiskManager";
    private static StudioCommonSettings _commonSettings;
    private const string _settingsKey = "Settings";
    private const string _isRemoteEnabled = "IsRemoteEnabled";
    private const string _lastDir = "LastDir";

    public static string GetTheme(this IPersistableService service, string defaultTheme)
    {
        return service != null ? service.GetValue<string>("Theme", defaultTheme) : throw new ArgumentNullException(nameof(service));
    }

    public static void SetTheme(this IPersistableService service, string theme)
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service));
        service.SetValue("Theme", (object)theme);
    }

    public static SettingsStorage LoadMainWindowSettings(this IPersistableService service)
    {
        return service != null ? service.GetValue<SettingsStorage>("MainWindow") : throw new ArgumentNullException(nameof(service));
    }

    public static void SaveMainWindowSettings(
      this IPersistableService service,
      SettingsStorage settings)
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service));
        service.SetValue("MainWindow", (object)settings);
    }

    public static SettingsStorage GetStudioSession(this IPersistableService service)
    {
        return service != null ? service.GetValue<SettingsStorage>("StudioSession") : throw new ArgumentNullException(nameof(service));
    }

    public static void SetStudioSession(this IPersistableService service, SettingsStorage session)
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service));
        if (session == null)
            throw new ArgumentNullException(nameof(session));
        service.SetValue("StudioSession", (object)session);
    }

    public static bool GetAutoConnect(this IPersistableService service)
    {
        return service != null ? service.GetValue<bool>("AutoConnect") : throw new ArgumentNullException(nameof(service));
    }

    public static void SetAutoConnect(this IPersistableService service, bool autoConnect)
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service));
        service.SetValue("AutoConnect", (object)autoConnect);
    }

    public static void SetDriveCache(this IPersistableService service, SettingsStorage storage)
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service));
        service.SetValue("DriveCache", (object)storage);
    }

    public static void TryLoadSettings(
      this IPersistableService service,
      string name,
      Action<SettingsStorage> load)
    {
        service.TryLoadSettings<SettingsStorage>(name, load);
    }

    public static void TryLoadSettings<T>(
      this IPersistableService service,
      string name,
      Action<T> load)
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service));
        LoggingHelper.DoWithLog((Action)(() =>
        {
            T obj = service.GetValue<T>(name);
            if ((object)obj == null)
                return;
            load(obj);
        }));
    }

    public static void TryLoad(
      this SettingsStorage storage,
      string name,
      Action<SettingsStorage> load)
    {
        storage.TryLoad<SettingsStorage>(name, load);
    }

    public static void TryLoad<T>(this SettingsStorage storage, string name, Action<T> load) where T : class
    {
        if (storage == null)
            throw new ArgumentNullException(nameof(storage));
        LoggingHelper.DoWithLog((Action)(() =>
        {
            T obj = storage.GetValue<T>(name, default(T));
            if ((object)obj == null)
                return;
            load(obj);
        }));
    }

    public static SettingsStorage GetLayout(this IPersistableService service)
    {
        return service != null ? service.GetValue<SettingsStorage>("Layout") : throw new ArgumentNullException(nameof(service));
    }

    public static void SetLayout(this IPersistableService service, Func<SettingsStorage> getLayout)
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service));
        if (getLayout == null)
            throw new ArgumentNullException(nameof(getLayout));
        service.SetDelayValue("Layout", (Func<object>)getLayout);
    }

    public static string GetRibbon(this IPersistableService service)
    {
        return service != null ? service.GetValue<string>("Ribbon") : throw new ArgumentNullException(nameof(service));
    }

    public static void SetRibbon(this IPersistableService service, string ribbon)
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service));
        service.SetValue("Ribbon", (object)ribbon);
    }

    public static DateTime? GetNextTimeFeedback(this IPersistableService service)
    {
        return service != null ? service.GetValue<DateTime?>("NextTimeFeedback") : throw new ArgumentNullException(nameof(service));
    }

    public static void SetNextTimeFeedback(this IPersistableService service, DateTime date)
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service));
        service.SetValue("NextTimeFeedback", (object)date);
    }

    public static bool GetDoNotShowQuestionWindow(this IPersistableService service)
    {
        return service != null ? service.GetValue<bool>("DoNotShowSuggestionsWindow") : throw new ArgumentNullException(nameof(service));
    }

    public static void SetDoNotShowQuestionWindow(this IPersistableService service, bool doNotShow)
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service));
        service.SetValue("DoNotShowSuggestionsWindow", (object)doNotShow);
    }

    public static SettingsStorage GetAlertService(this IPersistableService service)
    {
        return service != null ? service.GetValue<SettingsStorage>("AlertService") : throw new ArgumentNullException(nameof(service));
    }

    public static void SetAlertService(this IPersistableService service, SettingsStorage value)
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service));
        service.SetValue("AlertService", (object)value);
    }

    public static bool GetIsFirstRun(this IPersistableService service)
    {
        return service != null ? service.GetValue<bool>("IsFirstRun", true) : throw new ArgumentNullException(nameof(service));
    }

    public static void SetIsFirstRun(this IPersistableService service, bool value)
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service));
        service.SetValue("IsFirstRun", (object)value);
    }

    public static StorageFormats GetStorageFormat(this IPersistableService service)
    {
        return service != null ? service.GetValue<StorageFormats>("StorageFormat") : throw new ArgumentNullException(nameof(service));
    }

    public static void SetStorageFormat(this IPersistableService service)
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service));
        service.SetValue("StorageFormat", (object)StorageFormats.Binary);
    }

    public static SettingsStorage GetConnector(this IPersistableService service)
    {
        return service != null ? service.GetValue<SettingsStorage>("Connector") : throw new ArgumentNullException(nameof(service));
    }

    public static void SetConnector(this IPersistableService service, SettingsStorage value)
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service));
        service.SetValue("Connector", (object)value);
    }

    public static SettingsStorage GetConnectorWindow(this IPersistableService service)
    {
        return service != null ? service.GetValue<SettingsStorage>("ConnectorWindow") : throw new ArgumentNullException(nameof(service));
    }

    public static void SetConnectorWindow(this IPersistableService service, SettingsStorage value)
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service));
        service.SetValue("ConnectorWindow", (object)value);
    }

    public static SettingsStorage GetRiskManager(this IPersistableService service)
    {
        return service != null ? service.GetValue<SettingsStorage>("RiskManager") : throw new ArgumentNullException(nameof(service));
    }

    public static void SaveRiskManager(this IPersistableService service, SettingsStorage value)
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service));
        if (value == null)
            throw new ArgumentNullException(nameof(value));
        service.SetValue("RiskManager", (object)value);
    }

    public static void SaveEmulatorSettings(
      this IPersistableService service,
      MarketEmulatorSettings settings)
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service));
        if (settings == null)
            throw new ArgumentNullException(nameof(settings));
        service.SetValue("MarketEmulatorSettings", (object)PersistableHelper.Save((IPersistable)settings));
    }

    public static MarketEmulatorSettings LoadEmulatorSettings(this IPersistableService service)
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service));
        MarketEmulatorSettings emulatorSettings = new MarketEmulatorSettings();
        service.TryLoadSettings("MarketEmulatorSettings", new Action<SettingsStorage>(emulatorSettings.Load));
        return emulatorSettings;
    }

    public static T GetCommonSettings<T>(this IPersistableService service) where T : StudioCommonSettings, new()
    {
        if (IPersistableServiceExtensions._commonSettings == null)
            IPersistableServiceExtensions._commonSettings = (StudioCommonSettings)service.GetSettings<T>("Settings", out bool _);
        return (T)IPersistableServiceExtensions._commonSettings;
    }

    public static void SetCommonSettings<T>(this IPersistableService service, T settings) where T : StudioCommonSettings, new()
    {
        service.SetSettings<T>("Settings", settings);
    }

    public static T GetSettings<T>(this IPersistableService service, string key, out bool isNew) where T : class, IPersistable, new()
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service));
        isNew = false;
        object obj1 = service.GetValue<object>(key);
        if (obj1 == null)
        {
            isNew = true;
            return new T();
        }
        return obj1 is T obj2 ? obj2 : PersistableHelper.Load<T>((SettingsStorage)obj1);
    }

    public static void SetSettings<T>(this IPersistableService service, string key, T settings) where T : class, IPersistable, new()
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service));
        service.SetValue(key, (object)PersistableHelper.Save((IPersistable)settings));
    }

    public static bool GetIsRemoteEnabled(this IPersistableService service)
    {
        return service != null ? service.GetValue<bool>("IsRemoteEnabled") : throw new ArgumentNullException(nameof(service));
    }

    public static void SetIsRemoteEnabled(this IPersistableService service, bool value)
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service));
        service.SetValue("IsRemoteEnabled", (object)value);
    }

    public static string GetLastDir(this IPersistableService service, string prefix)
    {
        return service != null ? service.GetValue<string>("LastDir" + prefix) : throw new ArgumentNullException(nameof(service));
    }

    public static void SetLastDir(this IPersistableService service, string prefix, string value)
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service));
        service.SetValue("LastDir" + prefix, (object)value);
    }
}
