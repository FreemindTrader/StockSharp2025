// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Configuration.BaseUserConfig`1
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using StockSharp.Algo;
using StockSharp.Configuration;
using StockSharp.Logging;
using StockSharp.Messages;
using StockSharp.Studio.Core.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace StockSharp.Studio.Core.Configuration
{
    public abstract class BaseUserConfig<TConfig> : Disposable, IPersistableService
      where TConfig : BaseUserConfig<TConfig>, new()
    {
        private static readonly Lazy<TConfig> _instance = new Lazy<TConfig>( ( Func<TConfig> )( () => new TConfig() ) );
        private readonly CachedSynchronizedDictionary<string, BaseUserConfig<TConfig>.FileSettingsInfo> _fileServices = new CachedSynchronizedDictionary<string, BaseUserConfig<TConfig>.FileSettingsInfo>();
        private readonly CachedSynchronizedDictionary<string, BaseUserConfig<TConfig>.DirectorySettingsInfo> _directoryServices = new CachedSynchronizedDictionary<string, BaseUserConfig<TConfig>.DirectorySettingsInfo>();
        private readonly TimeSpan _period = TimeSpan.FromSeconds( 5.0 );
        private readonly SyncObject _flushingSync = new SyncObject();
        private readonly SyncObject _refCountSync = new SyncObject();
        private readonly BaseUserConfig<TConfig>.FileSettingsInfo _fileSettings;
        private Timer _flushTimer;
        private bool _isFlushing;
        private bool _isDisposing;
        private int _refCount;

        public static TConfig Instance
        {
            get
            {
                return BaseUserConfig<TConfig>._instance.Value;
            }
        }

        public IBasketSecurityProcessorProvider ProcessorProvider { get; } = ( IBasketSecurityProcessorProvider )new BasketSecurityProcessorProvider();

        public bool IsReseting { get; private set; }

        public LogConfig LogConfig { get; }

        protected BaseUserConfig()
        {
            Directory.CreateDirectory( Paths.AppDataPath );
            this._fileSettings = new BaseUserConfig<TConfig>.FileSettingsInfo( Path.Combine( Paths.AppDataPath, "settings.json" ) );
            this.LogConfig = new LogConfig();
            this._flushTimer = new Timer( new TimerCallback( this.OnFlush ), ( object )null, this._period, this._period );
        }

        public bool IsChangesSuspended
        {
            get
            {
                lock ( this._refCountSync )
                    return this._refCount > 0;
            }
        }

        public void SuspendChangesMonitor()
        {
            lock ( this._refCountSync )
                ++this._refCount;
        }

        public void ResumeChangesMonitor()
        {
            lock ( this._refCountSync )
                --this._refCount;
        }

        public void ResetSettings()
        {
            this.IsReseting = true;
            ServicesRegistry.LogManager.Dispose();
            IOHelper.BlockDeleteDir( Paths.AppDataPath, true, 1000, 0 );
        }

        protected override void DisposeManaged()
        {
            this._isDisposing = true;
            if ( !this.IsReseting )
            {
                this.DisposeTimer();
                this.OnSave( true );
            }
            base.DisposeManaged();
        }

        private void OnFlush( object state )
        {
            lock ( this._flushingSync )
            {
                if ( this._isFlushing )
                    return;
                this._isFlushing = true;
            }
            if ( this._isDisposing )
                return;
            try
            {
                this.OnSave( false );
            }
            finally
            {
                lock ( this._flushingSync )
                    this._isFlushing = false;
            }
        }

        private void OnSave( bool force )
        {
            this._fileSettings.Save( force );
            foreach ( BaseUserConfig<TConfig>.FileSettingsInfo cachedValue in this._fileServices.CachedValues )
                cachedValue.Save( force );
            foreach ( BaseUserConfig<TConfig>.DirectorySettingsInfo cachedValue in this._directoryServices.CachedValues )
                cachedValue.Save( force );
        }

        private void DisposeTimer()
        {
            if ( this._flushTimer == null )
                return;
            this._flushTimer.Dispose();
            this._flushTimer = ( Timer )null;
        }

        public INamedPersistableService GetService( string key )
        {
            if ( key == null )
                throw new ArgumentNullException( nameof( key ) );
            return ( INamedPersistableService )this._fileServices.SafeAdd<string, BaseUserConfig<TConfig>.FileSettingsInfo>( key, ( Func<string, BaseUserConfig<TConfig>.FileSettingsInfo> )( k => new BaseUserConfig<TConfig>.FileSettingsInfo( k ) ) );
        }

        public INamedPersistableService GetService( string group, string key )
        {
            if ( group == null )
                throw new ArgumentNullException( nameof( group ) );
            if ( key == null )
                throw new ArgumentNullException( nameof( key ) );
            return this._directoryServices.SafeAdd<string, BaseUserConfig<TConfig>.DirectorySettingsInfo>( group, ( Func<string, BaseUserConfig<TConfig>.DirectorySettingsInfo> )( k => new BaseUserConfig<TConfig>.DirectorySettingsInfo( Path.Combine( Paths.AppDataPath, k ) ) ) ).GetService( key );
        }

        public IEnumerable<INamedPersistableService> GetServices(
          string group )
        {
            if ( group == null )
                throw new ArgumentNullException( nameof( group ) );
            return this._directoryServices.SafeAdd<string, BaseUserConfig<TConfig>.DirectorySettingsInfo>( group, ( Func<string, BaseUserConfig<TConfig>.DirectorySettingsInfo> )( k => new BaseUserConfig<TConfig>.DirectorySettingsInfo( Path.Combine( Paths.AppDataPath, k ) ) ) ).GetServices();
        }

        public void RemoveService( string group, string key )
        {
            if ( group == null )
                throw new ArgumentNullException( nameof( group ) );
            if ( key == null )
                throw new ArgumentNullException( nameof( key ) );
            this._directoryServices.TryGetValue<string, BaseUserConfig<TConfig>.DirectorySettingsInfo>( group ).RemoveService( key );
        }

        public bool ContainsKey( string key )
        {
            return this._fileSettings.ContainsKey( key );
        }

        public TValue GetValue<TValue>( string key, TValue defaultValue = default )
        {
            return this._fileSettings.GetValue<TValue>( key, defaultValue );
        }

        public void SetValue( string key, object value )
        {
            if ( this.IsChangesSuspended )
                return;
            this._fileSettings.SetValue( key, value );
        }

        public void SetDelayValue( string key, Func<object> value )
        {
            if ( value == null )
                throw new ArgumentNullException( nameof( value ) );
            if ( this.IsChangesSuspended )
                return;
            this._fileSettings.SetDelayValue( key, value );
        }

        private sealed class FileSettingsInfo : INamedPersistableService, IPersistableService
        {
            private readonly IDictionary<string, Func<object>> _delayValues = ( IDictionary<string, Func<object>> )new Dictionary<string, Func<object>>();
            private readonly SyncObject _syncRoot = new SyncObject();
            private readonly string _settingsFile;
            private SettingsStorage _values;
            private bool _isChanged;

            private SettingsStorage Values
            {
                get
                {
                    if ( this._values != null )
                        return this._values;
                    ( ( Action )( () =>
                       {
                           if ( !File.Exists( this._settingsFile ) )
                               return;
                           this._values = Do.Invariant<SettingsStorage>( ( Func<SettingsStorage> )( () => this._settingsFile.Deserialize<SettingsStorage>() ) );
                       } ) ).DoWithLog();
                    return this._values ?? ( this._values = new SettingsStorage() );
                }
            }

            public FileSettingsInfo( string settingsFile )
            {
                string str = settingsFile;
                if ( str == null )
                    throw new ArgumentNullException( nameof( settingsFile ) );
                this._settingsFile = str;
            }

            public void Save( bool force )
            {
                ( ( Action )( () =>
                   {
                       lock ( this._syncRoot )
                       {
                           if ( !this._isChanged && !force )
                               return;
                           this._isChanged = false;
                       }
                       this.OnSave();
                   } ) ).DoWithLog();
            }

            private void OnSave()
            {
                IDictionary<string, Func<object>> dictionary;
                lock ( this._syncRoot )
                    dictionary = this._delayValues.Count == 0 ? ( IDictionary<string, Func<object>> )null : this._delayValues.ToDictionary<string, Func<object>>();
                if ( dictionary != null )
                {
                    foreach ( KeyValuePair<string, Func<object>> keyValuePair in ( IEnumerable<KeyValuePair<string, Func<object>>> )dictionary )
                    {
                        KeyValuePair<string, Func<object>> pair = keyValuePair;
                        ( ( Action )( () =>
                           {
                               object obj = pair.Value();
                               lock ( this._syncRoot )
                                   this.Values.Set<object>( pair.Key, obj );
                           } ) ).DoWithLog();
                    }
                }
                SettingsStorage clone = new SettingsStorage();
                lock ( this._syncRoot )
                    clone.AddRange<KeyValuePair<string, object>>( ( IEnumerable<KeyValuePair<string, object>> )this.Values.ToArray<KeyValuePair<string, object>>() );
                ( ( Action )( () =>
                   {
                       byte[ ] bytes = Do.Invariant<byte[ ]>( ( Func<byte[ ]> )( () => clone.Serialize<SettingsStorage>() ) );
                       lock ( this._syncRoot )
                           File.WriteAllBytes( this._settingsFile, bytes );
                   } ) ).DoWithLog();
            }

            public string Name
            {
                get
                {
                    return Path.GetFileName( this._settingsFile );
                }
            }

            public bool ContainsKey( string key )
            {
                lock ( this._syncRoot )
                    return this.Values.ContainsKey( key );
            }

            public TValue GetValue<TValue>( string key, TValue defaultValue = default )
            {
                lock ( this._syncRoot )
                    return this.Values.GetValue<TValue>( key, defaultValue );
            }

            public void SetValue( string key, object value )
            {
                lock ( this._syncRoot )
                {
                    this.Values.Set<object>( key, value );
                    this._isChanged = true;
                }
            }

            public void SetDelayValue( string key, Func<object> value )
            {
                if ( value == null )
                    throw new ArgumentNullException( nameof( value ) );
                lock ( this._syncRoot )
                {
                    this._delayValues[key] = value;
                    this._isChanged = true;
                }
            }
        }

        private sealed class DirectorySettingsInfo
        {
            private readonly CachedSynchronizedDictionary<string, BaseUserConfig<TConfig>.FileSettingsInfo> _persistableServices = new CachedSynchronizedDictionary<string, BaseUserConfig<TConfig>.FileSettingsInfo>();
            private readonly string _path;

            public DirectorySettingsInfo( string path )
            {
                string str = path;
                if ( str == null )
                    throw new ArgumentNullException( nameof( path ) );
                this._path = str;
                Directory.CreateDirectory( this._path );
                foreach ( string file in Directory.GetFiles( this._path, "*.json" ) )
                    this._persistableServices.Add( Path.GetFileName( file ), new BaseUserConfig<TConfig>.FileSettingsInfo( file ) );
            }

            public INamedPersistableService GetService( string key )
            {
                if ( key == null )
                    throw new ArgumentNullException( nameof( key ) );
                return ( INamedPersistableService )this._persistableServices.SafeAdd<string, BaseUserConfig<TConfig>.FileSettingsInfo>( key, ( Func<string, BaseUserConfig<TConfig>.FileSettingsInfo> )( k => new BaseUserConfig<TConfig>.FileSettingsInfo( this.GetPath( k ) ) ) );
            }

            public IEnumerable<INamedPersistableService> GetServices()
            {
                return ( IEnumerable<INamedPersistableService> )this._persistableServices.CachedValues;
            }

            public void RemoveService( string key )
            {
                if ( key == null )
                    throw new ArgumentNullException( nameof( key ) );
                this._persistableServices.Remove( key );
                File.Delete( this.GetPath( key ) );
            }

            public void Save( bool force )
            {
                foreach ( BaseUserConfig<TConfig>.FileSettingsInfo cachedValue in this._persistableServices.CachedValues )
                    cachedValue.Save( force );
            }

            private string GetPath( string key )
            {
                return Path.Combine( this._path, key );
            }
        }
    }
}
