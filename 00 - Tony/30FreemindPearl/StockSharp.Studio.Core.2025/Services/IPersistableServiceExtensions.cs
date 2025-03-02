// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Services.IPersistableServiceExtensions
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

using Ecng.Logging;
using Ecng.Serialization;
using StockSharp.Algo.Storages;
using StockSharp.Algo.Testing;
using System;

namespace StockSharp.Studio.Core.Services
{
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

        public static string GetTheme( this IPersistableService service, string defaultTheme )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            return service.GetValue<string>( "Theme", defaultTheme );
        }

        public static void SetTheme( this IPersistableService service, string theme )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            service.SetValue( "Theme", ( object ) theme );
        }

        public static SettingsStorage LoadMainWindowSettings(
          this IPersistableService service )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            return service.GetValue<SettingsStorage>( "MainWindow", ( SettingsStorage ) null );
        }

        public static void SaveMainWindowSettings(
          this IPersistableService service,
          SettingsStorage settings )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            service.SetValue( "MainWindow", ( object ) settings );
        }

        public static SettingsStorage GetStudioSession( this IPersistableService service )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            return service.GetValue<SettingsStorage>( "StudioSession", ( SettingsStorage ) null );
        }

        public static void SetStudioSession( this IPersistableService service, SettingsStorage session )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            if ( session == null )
                throw new ArgumentNullException( nameof( session ) );
            service.SetValue( "StudioSession", ( object ) session );
        }

        public static bool GetAutoConnect( this IPersistableService service )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            return service.GetValue<bool>( "AutoConnect", false );
        }

        public static void SetAutoConnect( this IPersistableService service, bool autoConnect )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            service.SetValue( "AutoConnect", ( object ) autoConnect );
        }

        public static void SetDriveCache( this IPersistableService service, SettingsStorage storage )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            service.SetValue( "DriveCache", ( object ) storage );
        }

        public static void TryLoadSettings(
          this IPersistableService service,
          string name,
          Action<SettingsStorage> load )
        {
            service.TryLoadSettings<SettingsStorage>( name, load );
        }

        public static void TryLoadSettings<T>(
          this IPersistableService service,
          string name,
          Action<T> load )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            LoggingHelper.DoWithLog( ( Action ) ( () =>
            {
                T obj = service.GetValue<T>(name, default (T));
                if ( ( object ) obj == null )
                    return;
                load( obj );
            } ) );
        }

        public static void TryLoad(
          this SettingsStorage storage,
          string name,
          Action<SettingsStorage> load )
        {
            storage.TryLoad<SettingsStorage>( name, load );
        }

        public static void TryLoad<T>( this SettingsStorage storage, string name, Action<T> load ) where T : class
        {
            if ( storage == null )
                throw new ArgumentNullException( nameof( storage ) );
            LoggingHelper.DoWithLog( ( Action ) ( () =>
            {
                T obj = (T) storage.GetValue<T>(name,  default (T));
                if ( ( object ) obj == null )
                    return;
                load( obj );
            } ) );
        }

        public static SettingsStorage GetLayout( this IPersistableService service )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            return service.GetValue<SettingsStorage>( "Layout", ( SettingsStorage ) null );
        }

        public static void SetLayout( this IPersistableService service, Func<SettingsStorage> getLayout )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            if ( getLayout == null )
                throw new ArgumentNullException( nameof( getLayout ) );
            service.SetDelayValue( "Layout", ( Func<object> ) getLayout );
        }

        public static string GetRibbon( this IPersistableService service )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            return service.GetValue<string>( "Ribbon", ( string ) null );
        }

        public static void SetRibbon( this IPersistableService service, string ribbon )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            service.SetValue( "Ribbon", ( object ) ribbon );
        }

        public static DateTime? GetNextTimeFeedback( this IPersistableService service )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            return service.GetValue<DateTime?>( "NextTimeFeedback", new DateTime?() );
        }

        public static void SetNextTimeFeedback( this IPersistableService service, DateTime date )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            service.SetValue( "NextTimeFeedback", ( object ) date );
        }

        public static bool GetDoNotShowQuestionWindow( this IPersistableService service )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            return service.GetValue<bool>( "DoNotShowSuggestionsWindow", false );
        }

        public static void SetDoNotShowQuestionWindow( this IPersistableService service, bool doNotShow )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            service.SetValue( "DoNotShowSuggestionsWindow", ( object ) doNotShow );
        }

        public static SettingsStorage GetAlertService( this IPersistableService service )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            return service.GetValue<SettingsStorage>( "AlertService", ( SettingsStorage ) null );
        }

        public static void SetAlertService( this IPersistableService service, SettingsStorage value )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            service.SetValue( "AlertService", ( object ) value );
        }

        public static bool GetIsFirstRun( this IPersistableService service )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            return service.GetValue<bool>( "IsFirstRun", true );
        }

        public static void SetIsFirstRun( this IPersistableService service, bool value )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            service.SetValue( "IsFirstRun", ( object ) value );
        }

        public static StorageFormats GetStorageFormat( this IPersistableService service )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            return service.GetValue<StorageFormats>( "StorageFormat", StorageFormats.Binary );
        }

        public static void SetStorageFormat( this IPersistableService service )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            service.SetValue( "StorageFormat", ( object ) StorageFormats.Binary );
        }

        public static SettingsStorage GetConnector( this IPersistableService service )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            return service.GetValue<SettingsStorage>( "Connector", ( SettingsStorage ) null );
        }

        public static void SetConnector( this IPersistableService service, SettingsStorage value )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            service.SetValue( "Connector", ( object ) value );
        }

        public static SettingsStorage GetConnectorWindow( this IPersistableService service )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            return service.GetValue<SettingsStorage>( "ConnectorWindow", ( SettingsStorage ) null );
        }

        public static void SetConnectorWindow( this IPersistableService service, SettingsStorage value )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            service.SetValue( "ConnectorWindow", ( object ) value );
        }

        public static SettingsStorage GetRiskManager( this IPersistableService service )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            return service.GetValue<SettingsStorage>( "RiskManager", ( SettingsStorage ) null );
        }

        public static void SaveRiskManager( this IPersistableService service, SettingsStorage value )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            if ( value == null )
                throw new ArgumentNullException( nameof( value ) );
            service.SetValue( "RiskManager", ( object ) value );
        }

        public static void SaveEmulatorSettings(
          this IPersistableService service,
          MarketEmulatorSettings settings )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            if ( settings == null )
                throw new ArgumentNullException( nameof( settings ) );
            service.SetValue( "MarketEmulatorSettings", ( object ) PersistableHelper.Save( ( IPersistable ) settings ) );
        }

        public static MarketEmulatorSettings LoadEmulatorSettings(
          this IPersistableService service )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            MarketEmulatorSettings emulatorSettings = new MarketEmulatorSettings();
            service.TryLoadSettings( "MarketEmulatorSettings", new Action<SettingsStorage>( emulatorSettings.Load ) );
            return emulatorSettings;
        }

        public static T GetCommonSettings<T>( this IPersistableService service ) where T : StudioCommonSettings, new()
        {
            if ( IPersistableServiceExtensions._commonSettings == null )
            {
                bool isNew;
                IPersistableServiceExtensions._commonSettings = ( StudioCommonSettings ) service.GetSettings<T>( "Settings", out isNew );
            }
            return ( T ) IPersistableServiceExtensions._commonSettings;
        }

        public static void SetCommonSettings<T>( this IPersistableService service, T settings ) where T : StudioCommonSettings, new()
        {
            service.SetSettings<T>( "Settings", settings );
        }

        public static T GetSettings<T>( this IPersistableService service, string key, out bool isNew ) where T : class, IPersistable, new()
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            isNew = false;
            object obj1 = service.GetValue<object>(key,  null);
            if ( obj1 == null )
            {
                isNew = true;
                return new T();
            }
            T obj2 = obj1 as T;
            if ( ( object ) obj2 != null )
                return obj2;
            return PersistableHelper.Load<T>( ( SettingsStorage ) obj1 );
        }

        public static void SetSettings<T>( this IPersistableService service, string key, T settings ) where T : class, IPersistable, new()
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            service.SetValue( key, ( object ) PersistableHelper.Save( ( IPersistable ) ( object ) settings ) );
        }

        public static bool GetIsRemoteEnabled( this IPersistableService service )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            return service.GetValue<bool>( "IsRemoteEnabled", false );
        }

        public static void SetIsRemoteEnabled( this IPersistableService service, bool value )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            service.SetValue( "IsRemoteEnabled", ( object ) value );
        }

        public static string GetLastDir( this IPersistableService service, string prefix )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            return service.GetValue<string>( "LastDir" + prefix, ( string ) null );
        }

        public static void SetLastDir( this IPersistableService service, string prefix, string value )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            service.SetValue( "LastDir" + prefix, ( object ) value );
        }
    }
}
