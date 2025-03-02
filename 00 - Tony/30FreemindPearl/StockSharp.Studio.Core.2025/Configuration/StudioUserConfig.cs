// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Configuration.StudioUserConfig
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.Logging;
using Ecng.Serialization;
using StockSharp.Algo;
using StockSharp.Configuration;
using StockSharp.Messages;
using StockSharp.Studio.Core.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace StockSharp.Studio.Core.Configuration
{
    public class StudioUserConfig : Disposable, IPersistableService
    {
        private static readonly Lazy<StudioUserConfig> _instance = new Lazy<StudioUserConfig>((Func<StudioUserConfig>) (() => new StudioUserConfig()));
        private readonly CachedSynchronizedDictionary<string, StudioUserConfig.FileSettingsInfo> _fileServices;
        private readonly CachedSynchronizedDictionary<string, StudioUserConfig.DirectorySettingsInfo> _directoryServices;
        private readonly TimeSpan _period;
        private readonly SyncObject _flushingSync;
        private readonly StudioUserConfig.FileSettingsInfo _fileSettings;
        private Timer _flushTimer;
        private bool _isFlushing;
        private bool _isDisposing;
        private readonly SyncObject _refCountSync;
        private int _refCount;

        public static StudioUserConfig Instance
        {
            get
            {
                return StudioUserConfig._instance.Value;
            }
        }

        public IBasketSecurityProcessorProvider ProcessorProvider { get; }

        public bool IsReseting { get; private set; }

        public LogConfig LogConfig { get; }

        protected StudioUserConfig()
        {            
            Directory.CreateDirectory( Paths.AppDataPath );
            this._fileSettings = new StudioUserConfig.FileSettingsInfo( Path.Combine( Paths.AppDataPath, "settings.json" ) );
            this.LogConfig = new LogConfig();
            this._flushTimer = new Timer( new TimerCallback( this.OnFlush ), ( object ) null, this._period, this._period );
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
            ( ( Disposable ) ServicesRegistry.LogManager ).Dispose();
            for ( int index = 0; index < 5; ++index )
            {
                try
                {
                    IOHelper.BlockDeleteDir( Paths.AppDataPath, true, 1000, 0 );
                    break;
                }
                catch
                {
                    ThreadingHelper.Sleep( TimeSpan.FromSeconds( 2.0 ) );
                }
            }
        }

        protected virtual void DisposeManaged()
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
            foreach ( StudioUserConfig.FileSettingsInfo cachedValue in this._fileServices.CachedValues )
                cachedValue.Save( force );
            foreach ( StudioUserConfig.DirectorySettingsInfo cachedValue in this._directoryServices.CachedValues )
                cachedValue.Save( force );
        }

        private void DisposeTimer()
        {
            if ( this._flushTimer == null )
                return;
            this._flushTimer.Dispose();
            this._flushTimer = ( Timer ) null;
        }

        public INamedPersistableService GetService( string group, string key )
        {
            if ( group == null )
                throw new ArgumentNullException( nameof( group ) );
            if ( key == null )
                throw new ArgumentNullException( nameof( key ) );
            return ( ( StudioUserConfig.DirectorySettingsInfo ) CollectionHelper.SafeAdd<string, StudioUserConfig.DirectorySettingsInfo>(  this._directoryServices,  group,  ( k => new StudioUserConfig.DirectorySettingsInfo( Path.Combine( Paths.AppDataPath, k ) ) ) ) ).GetService( key );
        }

        public IEnumerable<INamedPersistableService> GetServices(
          string group )
        {
            if ( group == null )
                throw new ArgumentNullException( nameof( group ) );
            return ( ( StudioUserConfig.DirectorySettingsInfo ) CollectionHelper.SafeAdd<string, StudioUserConfig.DirectorySettingsInfo>(  this._directoryServices,  group,  ( k => new StudioUserConfig.DirectorySettingsInfo( Path.Combine( Paths.AppDataPath, k ) ) ) ) ).GetServices();
        }

        public void RemoveService( string group, string key )
        {
            if ( group == null )
                throw new ArgumentNullException( nameof( group ) );
            if ( key == null )
                throw new ArgumentNullException( nameof( key ) );
            _directoryServices[ group ].RemoveService( key );
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

        private class FileSettingsInfo : INamedPersistableService, IPersistableService
        {
            private readonly IDictionary<string, Func<object>> _delayValues;
            private readonly SyncObject _syncRoot;
            private readonly string _settingsFile;
            private SettingsStorage _values;
            private bool _isChanged;

            public FileSettingsInfo( string settingsFile )
            {
                string str = settingsFile;
                if ( str == null )
                    throw new ArgumentNullException( nameof( settingsFile ) );
                this._settingsFile = str;
                
            }

            private SettingsStorage Values
            {
                get
                {
                    if ( this._values != null )
                        return this._values;
                    LoggingHelper.DoWithLog( ( Action ) ( () =>
                    {
                        if ( !File.Exists( this._settingsFile ) )
                            return;
                        this._values = this._settingsFile.DeserializeInvariant();
                    } ) );
                    return this._values ?? ( this._values = new SettingsStorage() );
                }
            }

            public void Save( bool force )
            {
                LoggingHelper.DoWithLog( ( Action ) ( () =>
                {
                    lock ( this._syncRoot )
                    {
                        if ( !this._isChanged && !force )
                            return;
                        this._isChanged = false;
                    }
                    IDictionary<string, Func<object>> dictionary;
                    lock ( this._syncRoot )
                        dictionary = this._delayValues.Count == 0 ? ( IDictionary<string, Func<object>> ) null : ( IDictionary<string, Func<object>> ) CollectionHelper.ToDictionary<string, Func<object>>( this._delayValues );
                    if ( dictionary != null )
                    {
                        foreach ( KeyValuePair<string, Func<object>> keyValuePair in ( IEnumerable<KeyValuePair<string, Func<object>>> ) dictionary )
                        {
                            try
                            {
                                object obj = keyValuePair.Value();
                                lock ( this._syncRoot )
                                    this.Values.Set<object>( keyValuePair.Key,  obj );
                            }
                            catch ( Exception ex )
                            {
                                LoggingHelper.LogError( ex, ( string ) null );
                            }
                        }
                    }
                    SettingsStorage clone = new SettingsStorage();
                    lock ( this._syncRoot )
                    {
                        ICollection<KeyValuePair<string, object>> keyValuePairs = (ICollection<KeyValuePair<string, object>>) clone;
                        SettingsStorage values = this.Values;
                        int index = 0;
                        KeyValuePair<string, object>[] items = new KeyValuePair<string, object>[((SynchronizedDictionary<string, object>) values).Count];
                        using ( IEnumerator<KeyValuePair<string, object>> enumerator = ( ( SynchronizedDictionary<string, object> ) values ).GetEnumerator() )
                        {
                            while ( enumerator.MoveNext() )
                            {
                                KeyValuePair<string, object> current = enumerator.Current;
                                items [index] = current;
                                ++index;
                            }
                        }
                        
                        keyValuePairs.AddRange( items );
                    }
                    LoggingHelper.DoWithLog( ( Action ) ( () =>
            {
                byte[] bytes = clone.SerializeInvariant(true);
                lock ( this._syncRoot )
                    File.WriteAllBytes( this._settingsFile, bytes );
            } ) );
                } ) );
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
                    return ( ( SynchronizedDictionary<string, object> ) this.Values ).ContainsKey( key );
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
                    this.Values.Set<object>( key,  value );
                    this._isChanged = true;
                }
            }

            public void SetDelayValue( string key, Func<object> value )
            {
                if ( value == null )
                    throw new ArgumentNullException( nameof( value ) );
                lock ( this._syncRoot )
                {
                    this._delayValues [key] = value;
                    this._isChanged = true;
                }
            }
        }

        private class DirectorySettingsInfo
        {
            private readonly CachedSynchronizedDictionary<string, StudioUserConfig.FileSettingsInfo> _persistableServices = new CachedSynchronizedDictionary<string, StudioUserConfig.FileSettingsInfo>();
            private readonly string _path;

            public DirectorySettingsInfo( string path )
            {
                string str = path;
                if ( str == null )
                    throw new ArgumentNullException( nameof( path ) );
                this._path = str;
                Directory.CreateDirectory( this._path );
                foreach ( string file in Directory.GetFiles( this._path, "*.json" ) )
                    ( ( SynchronizedDictionary<string, StudioUserConfig.FileSettingsInfo> ) this._persistableServices ).Add( Path.GetFileName( file ), new StudioUserConfig.FileSettingsInfo( file ) );
            }

            public INamedPersistableService GetService( string key )
            {
                if ( key == null )
                    throw new ArgumentNullException( nameof( key ) );
                return ( INamedPersistableService ) CollectionHelper.SafeAdd<string, StudioUserConfig.FileSettingsInfo>(  this._persistableServices,  key,  ( k => new StudioUserConfig.FileSettingsInfo( this.GetPath( k ) ) ) );
            }

            public IEnumerable<INamedPersistableService> GetServices()
            {
                return ( IEnumerable<INamedPersistableService> ) this._persistableServices.CachedValues;
            }

            public void RemoveService( string key )
            {
                if ( key == null )
                    throw new ArgumentNullException( nameof( key ) );
                ( ( SynchronizedDictionary<string, StudioUserConfig.FileSettingsInfo> ) this._persistableServices ).Remove( key );
                File.Delete( this.GetPath( key ) );
            }

            public void Save( bool force )
            {
                foreach ( StudioUserConfig.FileSettingsInfo cachedValue in this._persistableServices.CachedValues )
                    cachedValue.Save( force );
            }

            private string GetPath( string key )
            {
                return Path.Combine( this._path, key );
            }
        }
    }
}
