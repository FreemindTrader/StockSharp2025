// Decompiled with JetBrains decompiler
// Type: Ecng.Serialization.SettingsStorage
// Assembly: Ecng.Serialization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71D99F6F-5E8A-42DB-B327-361BEEF69266
// Assembly location: T:\00-FreemindTrader\packages\ecng.serialization\1.0.127\lib\netstandard2.0\Ecng.Serialization.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.Reflection;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace Ecng.Serialization
{
    public class SettingsStorage : SynchronizedDictionary<string, object>
    {
        private readonly JsonReader _reader;
        private readonly Func<JsonReader, SettingsStorage, string, Type, CancellationToken, ValueTask<object>> _readJson;

        public SettingsStorage()
          : base( ( IEqualityComparer<string> )StringComparer.InvariantCultureIgnoreCase )
        {
        }

        internal SettingsStorage(
          JsonReader reader,
          Func<JsonReader, SettingsStorage, string, Type, CancellationToken, ValueTask<object>> readJson )
          : this()
        {
            JsonReader jsonReader = reader;
            if ( jsonReader == null )
                throw new ArgumentNullException( nameof( reader ) );
            this._reader = jsonReader;
            Func<JsonReader, SettingsStorage, string, Type, CancellationToken, ValueTask<object>> func = readJson;
            if ( func == null )
                throw new ArgumentNullException( nameof( readJson ) );
            this._readJson = func;
        }

        public IEnumerable<string> Names
        {
            get
            {
                return ( IEnumerable<string> )this.SyncGet<SettingsStorage, string[ ]>( ( Func<SettingsStorage, string[ ]> )( d => d.Keys.ToArray<string>() ) );
            }
        }

        public SettingsStorage Set<T>( string name, T value )
        {
            this.SetValue<T>( name, value );
            return this;
        }

        private void EnsureIsNotReader()
        {
            if ( this._reader != null )
                throw new InvalidOperationException( "_reader != null" );
        }

        public void SetValue<T>( string name, T value )
        {
            if ( name.IsEmpty() )
                throw new ArgumentNullException( nameof( name ) );
            this.EnsureIsNotReader();
            this[name] = ( object )value;
        }

        public bool Contains( string name )
        {
            if ( name.IsEmpty() )
                throw new ArgumentNullException( nameof( name ) );
            return this.ContainsKey( name );
        }

        public override bool ContainsKey( string key )
        {
            this.EnsureIsNotReader();
            return base.ContainsKey( key );
        }

        internal int DeepLevel { get; set; }

        public T GetValue<T>( string name, T defaultValue = default(T) )
        {
            var myValue = this.GetValue( typeof( T ), name, ( object )defaultValue );
            
            return ( T )myValue.Result;
        }

        public async Task<object> GetValue( Type type, string name, object defaultValue = null )
        {
            if ( ( object )type == null )
            {
                throw new ArgumentNullException( nameof( type ) );
            }
                
            if ( name.IsEmpty() )
            {
                throw new ArgumentNullException( nameof( name ) );
            }
                
            if ( this._reader != null )
            {
                var result = await this.GetValueFromReaderAsync( type, name, new CancellationToken() );
                return ( result ?? defaultValue );
            }
            object obj;
            if ( !this.TryGetValue( name, out obj ) )
                return defaultValue;
            SettingsStorage storage1 = obj as SettingsStorage;
            if ( storage1 != null )
            {
                if ( type.IsAssignableFrom( typeof( SettingsStorage ) ) )
                    return obj;
                object instance = Activator.CreateInstance( type );
                IAsyncPersistable asyncPersistable = instance as IAsyncPersistable;
                if ( asyncPersistable != null )
                {
                    asyncPersistable.LoadAsync( storage1, new CancellationToken() ).Wait();
                }
                else
                {
                    IPersistable persistable = instance as IPersistable;
                    if ( persistable == null )
                        throw new ArgumentOutOfRangeException( type.To<string>() );
                    persistable.Load( storage1 );
                }
                return instance;
            }
            if ( type.IsCollection() && type.GetItemType().IsPersistable() )
            {
                if ( obj == null )
                    return ( object )null;
                Type elemType = type.GetItemType();
                object[ ] array1 = ( ( IEnumerable )obj ).Cast<SettingsStorage>().Select<SettingsStorage, object>( ( Func<SettingsStorage, object> )( storage =>
                {
                    if ( storage == null )
                        return ( object )null;
                    object instance = Activator.CreateInstance( elemType );
                    IAsyncPersistable asyncPersistable = instance as IAsyncPersistable;
                    if ( asyncPersistable != null )
                        asyncPersistable.LoadAsync( storage, new CancellationToken() ).Wait();
                    else
                        ( ( IPersistable )instance ).Load( storage );
                    return instance;
                } ) ).ToArray<object>();
                Array array2 = elemType.CreateArray( array1.Length );
                array1.CopyTo( array2, 0 );
                return array2.To( type );
            }
            if ( type == typeof( SecureString ) )
            {
                string str = obj as string;
                if ( str != null )
                    obj = ( object )SecureStringEncryptor.Instance.Decrypt( str.Base64() );
            }
            return obj.To( type );
        }

        public T TryGet<T>( string name, T defaultValue = default(T) )
        {
            return ( T )this.TryGet( typeof( T ), name, ( object )defaultValue );
        }

        public object TryGet( Type type, string name, object defaultValue = null )
        {
            return this.GetValue( type, name, defaultValue );
        }

        public async ValueTask<T> GetValueAsync<T>( string name, T defaultValue = default(T), CancellationToken cancellationToken = default( CancellationToken ) )
        {
            var result = await this.GetValueAsync( typeof( T ), name, ( T )defaultValue, cancellationToken );
            
            return( (T) result );
        }

        public async ValueTask<object> GetValueAsync(
          Type type,
          string name,
          object defaultValue = null,
          CancellationToken cancellationToken = default( CancellationToken ) )
        {
            if ( this._reader == null )
                return this.GetValue( type, name, defaultValue );
            return await this.GetValueFromReaderAsync( type, name, cancellationToken ) ?? defaultValue;
        }

        private async ValueTask<object> GetValueFromReaderAsync(
          Type type,
          string name,
          CancellationToken cancellationToken )
        {
            SettingsStorage settingsStorage = this;
            return ( object )await settingsStorage._readJson( settingsStorage._reader, settingsStorage, name, type, cancellationToken );
        }
    }
}
