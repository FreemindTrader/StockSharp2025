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
        private static readonly Lazy<TConfig> _instance = new Lazy<TConfig>( () => new TConfig() );
        private readonly CachedSynchronizedDictionary<string, FileSettingsInfo> _fileServices = new CachedSynchronizedDictionary<string, FileSettingsInfo>();
        private readonly CachedSynchronizedDictionary<string, DirectorySettingsInfo> _directoryServices = new CachedSynchronizedDictionary<string, DirectorySettingsInfo>();
        private readonly TimeSpan _period = TimeSpan.FromSeconds( 5.0 );
        private readonly SyncObject _flushingSync = new SyncObject();
        private readonly SyncObject _refCountSync = new SyncObject();
        private readonly FileSettingsInfo _fileSettings;
        private Timer _flushTimer;
        private bool _isFlushing;
        private bool _isDisposing;
        private int _refCount;

        public static TConfig Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        public IBasketSecurityProcessorProvider ProcessorProvider { get; } = new BasketSecurityProcessorProvider();

        public bool IsReseting { get; private set; }

        public LogConfig LogConfig { get; }

        protected BaseUserConfig()
        {
            Directory.CreateDirectory( Paths.AppDataPath );
            _fileSettings = new FileSettingsInfo( Path.Combine( Paths.AppDataPath, "settings.json" ) );
            LogConfig = new LogConfig();
            _flushTimer = new Timer( new TimerCallback( OnFlush ), null, _period, _period );
        }

        public bool IsChangesSuspended
        {
            get
            {
                lock ( _refCountSync )
                    return _refCount > 0;
            }
        }

        public void SuspendChangesMonitor()
        {
            lock ( _refCountSync )
                ++_refCount;
        }

        public void ResumeChangesMonitor()
        {
            lock ( _refCountSync )
                --_refCount;
        }

        public void ResetSettings()
        {
            IsReseting = true;
            ServicesRegistry.LogManager.Dispose();
            IOHelper.BlockDeleteDir( Paths.AppDataPath, true, 1000, 0 );
        }

        protected override void DisposeManaged()
        {
            _isDisposing = true;
            if ( !IsReseting )
            {
                DisposeTimer();
                OnSave( true );
            }
            base.DisposeManaged();
        }

        private void OnFlush( object state )
        {
            lock ( _flushingSync )
            {
                if ( _isFlushing )
                    return;
                _isFlushing = true;
            }
            if ( _isDisposing )
                return;
            try
            {
                OnSave( false );
            }
            finally
            {
                lock ( _flushingSync )
                    _isFlushing = false;
            }
        }

        private void OnSave( bool force )
        {
            _fileSettings.Save( force );
            foreach ( FileSettingsInfo cachedValue in _fileServices.CachedValues )
                cachedValue.Save( force );
            foreach ( DirectorySettingsInfo cachedValue in _directoryServices.CachedValues )
                cachedValue.Save( force );
        }

        private void DisposeTimer()
        {
            if ( _flushTimer == null )
                return;
            _flushTimer.Dispose();
            _flushTimer = null;
        }

        public INamedPersistableService GetService( string key )
        {
            if ( key == null )
                throw new ArgumentNullException( nameof( key ) );
            return _fileServices.SafeAdd( key, k => new FileSettingsInfo( k ) );
        }

        public INamedPersistableService GetService( string group, string key )
        {
            if ( group == null )
                throw new ArgumentNullException( nameof( group ) );
            if ( key == null )
                throw new ArgumentNullException( nameof( key ) );
            return _directoryServices.SafeAdd( group, k => new DirectorySettingsInfo( Path.Combine( Paths.AppDataPath, k ) ) ).GetService( key );
        }

        public IEnumerable<INamedPersistableService> GetServices(
          string group )
        {
            if ( group == null )
                throw new ArgumentNullException( nameof( group ) );
            return _directoryServices.SafeAdd( group, k => new DirectorySettingsInfo( Path.Combine( Paths.AppDataPath, k ) ) ).GetServices();
        }

        public void RemoveService( string group, string key )
        {
            if ( group == null )
                throw new ArgumentNullException( nameof( group ) );
            if ( key == null )
                throw new ArgumentNullException( nameof( key ) );
            _directoryServices.TryGetValue( group ).RemoveService( key );
        }

        public bool ContainsKey( string key )
        {
            return _fileSettings.ContainsKey( key );
        }

        public TValue GetValue<TValue>( string key, TValue defaultValue = default )
        {
            return _fileSettings.GetValue( key, defaultValue );
        }

        public void SetValue( string key, object value )
        {
            if ( IsChangesSuspended )
                return;
            _fileSettings.SetValue( key, value );
        }

        public void SetDelayValue( string key, Func<object> value )
        {
            if ( value == null )
                throw new ArgumentNullException( nameof( value ) );
            if ( IsChangesSuspended )
                return;
            _fileSettings.SetDelayValue( key, value );
        }

        private sealed class FileSettingsInfo : INamedPersistableService, IPersistableService
        {
            private readonly IDictionary<string, Func<object>> _delayValues = new Dictionary<string, Func<object>>();
            private readonly SyncObject _syncRoot = new SyncObject();
            private readonly string _settingsFile;
            private SettingsStorage _values;
            private bool _isChanged;

            private SettingsStorage Values
            {
                get
                {
                    if ( _values != null )
                        return _values;
                    ( ( Action )( () =>
                       {
                           if ( !File.Exists( _settingsFile ) )
                               return;
                           _values = Do.Invariant( () => _settingsFile.Deserialize<SettingsStorage>() );
                       } ) ).DoWithLog();
                    return _values ?? ( _values = new SettingsStorage() );
                }
            }

            public FileSettingsInfo( string settingsFile )
            {
                string str = settingsFile;
                if ( str == null )
                    throw new ArgumentNullException( nameof( settingsFile ) );
                _settingsFile = str;
            }

            public void Save( bool force )
            {
                ( ( Action )( () =>
                   {
                       lock ( _syncRoot )
                       {
                           if ( !_isChanged && !force )
                               return;
                           _isChanged = false;
                       }
                       OnSave();
                   } ) ).DoWithLog();
            }

            private void OnSave()
            {
                IDictionary<string, Func<object>> dictionary;
                lock ( _syncRoot )
                    dictionary = _delayValues.Count == 0 ? null : _delayValues.ToDictionary();
                if ( dictionary != null )
                {
                    foreach ( KeyValuePair<string, Func<object>> keyValuePair in ( IEnumerable<KeyValuePair<string, Func<object>>> )dictionary )
                    {
                        KeyValuePair<string, Func<object>> pair = keyValuePair;
                        ( ( Action )( () =>
                           {
                               object obj = pair.Value();
                               lock ( _syncRoot )
                                   Values.Set( pair.Key, obj );
                           } ) ).DoWithLog();
                    }
                }
                SettingsStorage clone = new SettingsStorage();
                lock ( _syncRoot )
                    clone.AddRange( Values.ToArray() );
                ( ( Action )( () =>
                   {
                       byte[ ] bytes = Do.Invariant( () => clone.Serialize() );
                       lock ( _syncRoot )
                           File.WriteAllBytes( _settingsFile, bytes );
                   } ) ).DoWithLog();
            }

            public string Name
            {
                get
                {
                    return Path.GetFileName( _settingsFile );
                }
            }

            public bool ContainsKey( string key )
            {
                lock ( _syncRoot )
                    return Values.ContainsKey( key );
            }

            public TValue GetValue<TValue>( string key, TValue defaultValue = default )
            {
                lock ( _syncRoot )
                    return Values.GetValue( key, defaultValue );
            }

            public void SetValue( string key, object value )
            {
                lock ( _syncRoot )
                {
                    Values.Set( key, value );
                    _isChanged = true;
                }
            }

            public void SetDelayValue( string key, Func<object> value )
            {
                if ( value == null )
                    throw new ArgumentNullException( nameof( value ) );
                lock ( _syncRoot )
                {
                    _delayValues[key] = value;
                    _isChanged = true;
                }
            }
        }

        private sealed class DirectorySettingsInfo
        {
            private readonly CachedSynchronizedDictionary<string, FileSettingsInfo> _persistableServices = new CachedSynchronizedDictionary<string, FileSettingsInfo>();
            private readonly string _path;

            public DirectorySettingsInfo( string path )
            {
                string str = path;
                if ( str == null )
                    throw new ArgumentNullException( nameof( path ) );
                _path = str;
                Directory.CreateDirectory( _path );
                foreach ( string file in Directory.GetFiles( _path, "*.json" ) )
                    _persistableServices.Add( Path.GetFileName( file ), new FileSettingsInfo( file ) );
            }

            public INamedPersistableService GetService( string key )
            {
                if ( key == null )
                    throw new ArgumentNullException( nameof( key ) );
                return _persistableServices.SafeAdd( key, k => new FileSettingsInfo( GetPath( k ) ) );
            }

            public IEnumerable<INamedPersistableService> GetServices()
            {
                return _persistableServices.CachedValues;
            }

            public void RemoveService( string key )
            {
                if ( key == null )
                    throw new ArgumentNullException( nameof( key ) );
                _persistableServices.Remove( key );
                File.Delete( GetPath( key ) );
            }

            public void Save( bool force )
            {
                foreach ( FileSettingsInfo cachedValue in _persistableServices.CachedValues )
                    cachedValue.Save( force );
            }

            private string GetPath( string key )
            {
                return Path.Combine( _path, key );
            }
        }
    }
}
