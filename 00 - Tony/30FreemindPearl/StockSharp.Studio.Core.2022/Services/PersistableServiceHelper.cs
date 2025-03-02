// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Services.PersistableServiceHelper
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using Ecng.Serialization;
using StockSharp.Algo.Storages;
using StockSharp.Algo.Testing;
using StockSharp.Logging;
using System;

namespace StockSharp.Studio.Core.Services
{
    public static class PersistableServiceHelper
    {
        private const string _studioSessionKey = "StudioSession";
        private const string _autoConnectKey = "AutoConnect";
        private const string _driveCacheKey = "DriveCache";
        private const string _layoutKey = "Layout";
        private const string _ribbonKey = "Ribbon";
        private const string _nextTimeFeedback = "NextTimeFeedback";
        private const string _suggestionsKey = "DoNotShowSuggestionsWindow";
        private const string _alertServiceKey = "AlertService";
        private const string _isFirstRunKey = "IsFirstRun";
        private const string _daysLoadKey = "DaysLoad";
        private const int _defaultDaysLoad = 7;
        private const string _storageFormatKey = "StorageFormat";
        private const StorageFormats _defaultFormat = StorageFormats.Binary;
        private const string _connectorKey = "Connector";
        private const string _connectorWindowKey = "ConnectorWindow";
        private const string _settingsKey = "Settings";

        public static SettingsStorage GetStudioSession( this IPersistableService service )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            return service.GetValue<SettingsStorage>( "StudioSession", null );
        }

        public static void SetStudioSession( this IPersistableService service, SettingsStorage session )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            if ( session == null )
                throw new ArgumentNullException( nameof( session ) );
            service.SetValue( "StudioSession", session );
        }

        public static bool GetAutoConnect( this IPersistableService service )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            return service.GetValue( "AutoConnect", false );
        }

        public static void SetAutoConnect( this IPersistableService service, bool autoConnect )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            service.SetValue( "AutoConnect", autoConnect );
        }

        public static void SetDriveCache( this IPersistableService service, SettingsStorage storage )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            service.SetValue( "DriveCache", storage );
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
            ( ( Action )( () =>
               {
                   T obj = service.GetValue( name, default( T ) );
                   if ( obj == null )
                       return;
                   load( obj );
               } ) ).DoWithLog();
        }

        public static void TryLoadSettings<T>(
          this SettingsStorage storage,
          string name,
          Action<T> load )
          where T : class
        {
            if ( storage == null )
                throw new ArgumentNullException( nameof( storage ) );
            ( ( Action )( () =>
               {
                   T obj = storage.GetValue( name, default( T ) );
                   if ( obj == null )
                       return;
                   load( obj );
               } ) ).DoWithLog();
        }

        public static SettingsStorage GetLayout( this IPersistableService service )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            return service.GetValue<SettingsStorage>( "Layout", null );
        }

        public static void SetLayout( this IPersistableService service, Func<SettingsStorage> getLayout )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            if ( getLayout == null )
                throw new ArgumentNullException( nameof( getLayout ) );
            service.SetDelayValue( "Layout", getLayout );
        }

        public static string GetRibbon( this IPersistableService service )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            return service.GetValue<string>( "Ribbon", null );
        }

        public static void SetRibbon( this IPersistableService service, string ribbon )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            service.SetValue( "Ribbon", ribbon );
        }

        public static DateTime? GetNextTimeFeedback( this IPersistableService service )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            return service.GetValue( "NextTimeFeedback", new DateTime?() );
        }

        public static void SetNextTimeFeedback( this IPersistableService service, DateTime date )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            service.SetValue( "NextTimeFeedback", date );
        }

        public static bool GetDoNotShowQuestionWindow( this IPersistableService service )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            return service.GetValue( "DoNotShowSuggestionsWindow", false );
        }

        public static void SetDoNotShowQuestionWindow( this IPersistableService service, bool doNotShow )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            service.SetValue( "DoNotShowSuggestionsWindow", doNotShow );
        }

        public static SettingsStorage GetAlertService( this IPersistableService service )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            return service.GetValue<SettingsStorage>( "AlertService", null );
        }

        public static void SetAlertService( this IPersistableService service, SettingsStorage value )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            service.SetValue( "AlertService", value );
        }

        public static bool GetIsFirstRun( this IPersistableService service )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            return service.GetValue( "IsFirstRun", true );
        }

        public static void SetIsFirstRun( this IPersistableService service, bool value )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            service.SetValue( "IsFirstRun", value );
        }

        public static TimeSpan GetDaysLoad( this IPersistableService service )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            return TimeSpan.FromDays( service.GetValue( "DaysLoad", 7 ) );
        }

        public static void SetDaysLoad( this IPersistableService service )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            service.SetValue( "DaysLoad", 7 );
        }

        public static StorageFormats GetStorageFormat( this IPersistableService service )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            return service.GetValue( "StorageFormat", StorageFormats.Binary );
        }

        public static void SetStorageFormat( this IPersistableService service )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            service.SetValue( "StorageFormat", StorageFormats.Binary );
        }

        public static SettingsStorage GetConnector( this IPersistableService service )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            return service.GetValue<SettingsStorage>( "Connector", null );
        }

        public static void SetConnector( this IPersistableService service, SettingsStorage value )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            service.SetValue( "Connector", value );
        }

        public static SettingsStorage GetConnectorWindow( this IPersistableService service )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            return service.GetValue<SettingsStorage>( "ConnectorWindow", null );
        }

        public static void SetConnectorWindow( this IPersistableService service, SettingsStorage value )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            service.SetValue( "ConnectorWindow", value );
        }

        public static void SaveRiskManager( this IPersistableService service, SettingsStorage value )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            if ( value == null )
                throw new ArgumentNullException( nameof( value ) );
            service.SetValue( "RiskManager", value );
        }

        public static void SaveEmulatorSettings(
          this IPersistableService service,
          MarketEmulatorSettings settings )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            if ( settings == null )
                throw new ArgumentNullException( nameof( settings ) );
            service.SetValue( "MarketEmulatorSettings", settings.Save() );
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
            return service.GetSettings<T>( "Settings" );
        }

        public static void SetCommonSettings<T>( this IPersistableService service, T settings ) where T : StudioCommonSettings, new()
        {
            service.SetSettings( "Settings", settings );
        }

        public static T GetSettings<T>( this IPersistableService service, string key ) where T : class, IPersistable, new()
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            object obj1 = service.GetValue<object>( key, null );
            if ( obj1 == null )
                return new T();
            T obj2 = obj1 as T;
            if ( obj2 != null )
                return obj2;
            return ( ( SettingsStorage )obj1 ).Load<T>();
        }

        public static void SetSettings<T>( this IPersistableService service, string key, T settings ) where T : class, IPersistable, new()
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            service.SetValue( key, settings.Save() );
        }
    }
}
