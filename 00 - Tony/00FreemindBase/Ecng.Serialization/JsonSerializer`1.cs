// Decompiled with JetBrains decompiler
// Type: Ecng.Serialization.JsonSerializer`1
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
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ecng.Serialization
{
    public class JsonSerializer<T> : Serializer<T>
    {
        static JsonSerializer()
        {
            typeof( JsonConversions ).EnsureRunClass();
        }

        public bool Indent { get; set; }

        public Encoding Encoding { get; set; } = Encoding.UTF8;

        public bool FillMode { get; set; } = true;

        public bool EnumAsString { get; set; }

        public bool EncryptedAsByteArray { get; set; }

        public int BufferSize { get; set; } = 1024;

        public NullValueHandling NullValueHandling { get; set; }

        public override ISerializer GetSerializer( Type entityType )
        {
            ISerializer serializer = base.GetSerializer( entityType );
            serializer.SetValue<ISerializer, bool>( "Indent", this.Indent );
            serializer.SetValue<ISerializer, Encoding>( "Encoding", this.Encoding );
            serializer.SetValue<ISerializer, bool>( "FillMode", this.FillMode );
            serializer.SetValue<ISerializer, bool>( "EnumAsString", this.EnumAsString );
            serializer.SetValue<ISerializer, bool>( "EncryptedAsByteArray", this.EncryptedAsByteArray );
            serializer.SetValue<ISerializer, int>( "BufferSize", this.BufferSize );
            return serializer;
        }

        public override string FileExtension
        {
            get
            {
                return "json";
            }
        }

        public static JsonSerializer<T> CreateDefault()
        {
            return new JsonSerializer<T>()
            {
                Indent = true,
                EnumAsString = true,
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        private static bool IsJsonPrimitive()
        {
            if ( typeof( T ).IsSerializablePrimitive() )
                return typeof( T ) != typeof( byte[ ] );
            return false;
        }

        public override async ValueTask SerializeAsync( T graph, Stream stream, CancellationToken cancellationToken )
        {
            JsonTextWriter writer;

            bool isPrimitive = JsonSerializer<T>.IsJsonPrimitive();
            JsonTextWriter jsonTextWriter = new JsonTextWriter( ( TextWriter )new StreamWriter( stream, this.Encoding, this.BufferSize, true ) );
            jsonTextWriter.Formatting = this.Indent ? Formatting.Indented : Formatting.None;
            writer = jsonTextWriter;

            if ( isPrimitive )
            {
                await writer.WriteStartArrayAsync( cancellationToken );
            }

            ValueTask valueTask = this.WriteAsync( ( JsonWriter )writer, ( object )graph, cancellationToken );
            //ValueTaskAwaiter awaiter2 = ( ( ValueTask )ref valueTask ).GetAwaiter();

            if ( isPrimitive )
            {
                await writer.WriteEndArrayAsync( cancellationToken );
            }

        }

        public override async ValueTask<T> DeserializeAsync( Stream stream, CancellationToken cancellationToken )
        {
            JsonTextReader reader;

            T result;

            bool isPrimitive = JsonSerializer<T>.IsJsonPrimitive();
            JsonTextReader jsonTextReader = new JsonTextReader( ( TextReader )new StreamReader( stream, this.Encoding, true, this.BufferSize, true ) );
            jsonTextReader.FloatParseHandling = FloatParseHandling.Decimal;
            reader = jsonTextReader;

            if ( isPrimitive )
            {
                if ( !await reader.ReadAsync( cancellationToken ) )
                {
                    result = default( T );

                    return result;
                }
            }

            result = ( T )await this.ReadAsync( ( JsonReader )reader, typeof( T ), cancellationToken );

            if ( isPrimitive )
            {
                await reader.ReadAsync( cancellationToken );
            }

            return result;
        }

        private async ValueTask TryClearDeepLevel( JsonReader reader, SettingsStorage storage, CancellationToken cancellationToken )
        {
            int lvl = storage.DeepLevel;
            if ( lvl == 0 )
                return;
            for ( int i = 1; i <= lvl; ++i )
                await reader.ReadWithCheckAsync( cancellationToken );
            storage.DeepLevel = 0;
        }

        private async ValueTask<object> GetValueFromReaderAsync( JsonReader reader, SettingsStorage storage, string name, Type type, CancellationToken cancellationToken )
        {
            await this.TryClearDeepLevel( reader, storage, cancellationToken );
            await reader.ReadWithCheckAsync( cancellationToken );

            if ( !( ( string )reader.Value ).EqualsIgnoreCase( name ) )
            {
                throw new InvalidOperationException( string.Format( "{0} != {1}", reader.Value, ( object )name ) );
            }

            return ( object )await this.ReadAsync( reader, type, cancellationToken );
        }

        private async ValueTask FillAsync( SettingsStorage storage, JsonReader reader, CancellationToken cancellationToken )
        {
            if ( storage == null )
            {
                throw new ArgumentNullException( nameof( storage ) );
            }

            if ( reader == null )
            {
                throw new ArgumentNullException( nameof( reader ) );
            }

            while ( true )
            {
                await reader.ReadWithCheckAsync( cancellationToken );

                if ( reader.TokenType != JsonToken.EndObject )
                {
                    string propName = ( string )reader.Value;
                    await reader.ReadWithCheckAsync( cancellationToken );
                    SettingsStorage inner;
                    object obj;
                    switch ( reader.TokenType )
                    {
                        case JsonToken.StartObject:
                        inner = new SettingsStorage();
                        await this.FillAsync( inner, reader, cancellationToken );
                        obj = ( object )inner;
                        break;
                        case JsonToken.StartArray:
                        await reader.ReadWithCheckAsync( cancellationToken );
                        List<object> list = new List<object>();
                        while ( reader.TokenType != JsonToken.EndArray )
                        {
                            if ( reader.TokenType == JsonToken.StartObject )
                            {
                                inner = new SettingsStorage();
                                await this.FillAsync( inner, reader, cancellationToken );
                                list.Add( ( object )inner );
                            }
                            else
                                list.Add( reader.Value );
                            await reader.ReadWithCheckAsync( cancellationToken );
                        }
                        obj = ( object )list.ToArray();
                        break;
                        default:
                        obj = reader.Value;
                        break;
                    }
                    storage.Set<object>( propName, obj );
                    propName = ( string )null;
                }
                else
                    break;
            }
        }



        private async ValueTask<object> ReadAsync( JsonReader reader, Type type, CancellationToken cancellationToken )
        {
            JsonSerializer<T> jsonSerializer = this;
            SettingsStorage storage;
            if ( type.IsPersistable() )
            {
                object per;
                if ( jsonSerializer.FillMode )
                {
                    SettingsStorage storage1 = ( SettingsStorage )await jsonSerializer.ReadAsync( reader, typeof( SettingsStorage ), cancellationToken );
                    if ( storage1 == null )
                        return ( object )null;
                    per = type.CreateInstance();
                    IAsyncPersistable asyncPersistable = per as IAsyncPersistable;
                    if ( asyncPersistable != null )
                        await asyncPersistable.LoadAsync( storage1, new CancellationToken() );
                    else
                        ( ( IPersistable )per ).Load( storage1 );
                    return per;
                }
                await reader.ReadWithCheckAsync( cancellationToken );
                if ( reader.TokenType == JsonToken.EndArray || reader.TokenType == JsonToken.Null )
                    return ( object )null;
                per = type.CreateInstance();
                storage = new SettingsStorage( reader, new Func<JsonReader, SettingsStorage, string, Type, CancellationToken, ValueTask<object>>( jsonSerializer.GetValueFromReaderAsync ) );
                IAsyncPersistable asyncPersistable1 = per as IAsyncPersistable;
                if ( asyncPersistable1 != null )
                    await asyncPersistable1.LoadAsync( storage, new CancellationToken() );
                else
                    ( ( IPersistable )per ).Load( storage );
                await jsonSerializer.TryClearDeepLevel( reader, storage, cancellationToken );
                await reader.ReadWithCheckAsync( cancellationToken );
                return per;
            }
            if ( type == typeof( SettingsStorage ) )
            {
                await reader.ReadWithCheckAsync( cancellationToken );
                if ( reader.TokenType == JsonToken.EndArray || reader.TokenType == JsonToken.Null )
                    return ( object )null;
                storage = new SettingsStorage();
                await jsonSerializer.FillAsync( storage, reader, cancellationToken );
                return ( object )storage;
            }
            if ( type.Is<IEnumerable>() && type != typeof( string ) )
            {
                await reader.ReadWithCheckAsync( cancellationToken );
                if ( reader.TokenType == JsonToken.EndArray || reader.TokenType == JsonToken.Null )
                    return ( object )null;
                Type itemType = type.GetItemType();
                List<object> col = new List<object>();
                while ( true )
                {
                    object obj = await jsonSerializer.ReadAsync( reader, itemType, cancellationToken );
                    if ( obj != null || reader.TokenType != JsonToken.EndArray )
                        col.Add( obj );
                    else
                        break;
                }
                Array instance = Array.CreateInstance( itemType, col.Count );
                int num = 0;
                foreach ( object obj in col )
                    instance.SetValue( obj, num++ );
                return type.IsArray ? ( object )instance : ( object )col;
            }
            object obj1;
            if ( type == typeof( DateTime ) )
                obj1 = ( object )await reader.ReadAsDateTimeAsync( cancellationToken );
            else if ( type == typeof( DateTimeOffset ) )
                obj1 = ( object )await reader.ReadAsDateTimeOffsetAsync( cancellationToken );
            else if ( type == typeof( byte[ ] ) )
                obj1 = ( object )await reader.ReadAsBytesAsync( cancellationToken );
            else if ( type == typeof( SecureString ) )
            {
                SecureStringEncryptor secureStringEncryptor = SecureStringEncryptor.Instance;
                byte[ ] source;
                if ( jsonSerializer.EncryptedAsByteArray )
                {
                    source = await reader.ReadAsBytesAsync( cancellationToken );
                }
                else
                {
                    string str = await reader.ReadAsStringAsync( cancellationToken );
                    source = str != null ? str.Base64() : ( byte[ ] )null;
                }
                obj1 = ( object )secureStringEncryptor.Decrypt( source );
                secureStringEncryptor = ( SecureStringEncryptor )null;
            }
            else
            {
                Type adapterType;
                if ( type.TryGetAdapterType( out adapterType ) )
                {
                    obj1 = await jsonSerializer.ReadAsync( reader, adapterType, cancellationToken );
                    IPersistableAdapter persistableAdapter = obj1 as IPersistableAdapter;
                    if ( persistableAdapter != null )
                        obj1 = persistableAdapter.UnderlyingValue;
                }
                else
                    obj1 = ( object )await reader.ReadAsStringAsync( cancellationToken );
            }
            return obj1 != null ? obj1.To( type ) : ( object )null;
        }

        private async ValueTask WriteAsync( JsonWriter writer, object value, CancellationToken cancellationToken )
        {
            IPersistable persistable1 = value as IPersistable;
            

            async Task WriteSettingsStorageAsync( SettingsStorage storage )
            {
                await writer.WriteStartObjectAsync( cancellationToken );
                foreach ( KeyValuePair<string, object> keyValuePair in ( SynchronizedDictionary<string, object> )storage )
                {
                    KeyValuePair<string, object> pair = keyValuePair;

                    if ( pair.Value != null || this.NullValueHandling != NullValueHandling.Ignore)
                    {
                        await writer.WritePropertyNameAsync( pair.Key, cancellationToken );

                        await this.WriteAsync( writer, pair.Value, cancellationToken );                                               
                    }
                }

                await writer.WriteEndObjectAsync( cancellationToken );
            }

            if ( persistable1 != null )
            {
                await WriteSettingsStorageAsync( persistable1.Save() );
            }
            else
            {
                IAsyncPersistable persistable2 = value as IAsyncPersistable;
                if ( persistable2 != null )
                {
                    var settingStorage = await persistable2.SaveAsync( cancellationToken );

                    await WriteSettingsStorageAsync( settingStorage );
                }
                else
                {
                    SettingsStorage storage = value as SettingsStorage;
                    if ( storage != null )
                    {
                        await WriteSettingsStorageAsync( storage );
                    }
                    else
                    {
                        IEnumerable primCol = value as IEnumerable;

                        if ( primCol != null && !( value is string ) )
                        {
                            await writer.WriteStartArrayAsync( cancellationToken );

                            IEnumerator enumerator = primCol.GetEnumerator();

                            while ( enumerator.MoveNext() )
                            {
                                object current = enumerator.Current;
                                await this.WriteAsync( writer, current, cancellationToken );
                            }

                            await writer.WriteEndArrayAsync( cancellationToken );
                        }
                        else
                        {
                            SecureString secret = value as SecureString;

                            if ( secret != null )
                            {
                                byte[ ] numArray = SecureStringEncryptor.Instance.Encrypt( secret );
                                await this.WriteAsync( writer, this.EncryptedAsByteArray ? ( object )numArray : ( numArray != null ? ( object )numArray.Base64() : ( object )null ), cancellationToken );
                            }
                            else
                            {
                                TimeZoneInfo timeZoneInfo = value as TimeZoneInfo;

                                if ( timeZoneInfo != null )
                                {
                                    value = ( object )timeZoneInfo.To<string>();
                                }
                                else if ( value is Enum && this.EnumAsString )
                                {
                                    value = ( object )value.To<string>();
                                }
                                else
                                {
                                    Type type = value as Type;
                                    if ( ( object )type != null )
                                    {
                                        value = ( object )type.GetTypeAsString( false );
                                    }
                                    else
                                    {
                                        Type adapterType;
                                        if ( value != null && value.GetType().TryGetAdapterType( out adapterType ) )
                                        {
                                            IPersistableAdapter instance2 = adapterType.CreateInstance<IPersistableAdapter>();
                                            instance2.UnderlyingValue = value;
                                            await this.WriteAsync( writer, ( object )instance2, cancellationToken );

                                        }
                                    }

                                    await writer.WriteValueAsync( value, cancellationToken );
                                }
                            }
                        }
                    }
                }







                
            }
        }
    }
}

