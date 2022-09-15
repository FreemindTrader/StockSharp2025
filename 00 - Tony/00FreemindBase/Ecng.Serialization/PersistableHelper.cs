using Ecng.Collections;
using Ecng.Common;
using Ecng.Reflection;
using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Ecng.Serialization
{
    public static class PersistableHelper
    {
        private static readonly CachedSynchronizedDictionary<Type, Type> _adapterTypes = new CachedSynchronizedDictionary<Type, Type>();
        private const string _typeKey = "type";
        private const string _valueKey = "value";

        private static Type ValidateAdapterType( Type adapterType )
        {
            if ( ( object )adapterType == null )
                throw new ArgumentNullException( nameof( adapterType ) );
            if ( !adapterType.IsPersistable() )
                throw new ArgumentException( nameof( adapterType ) );
            if ( !adapterType.Is<IPersistableAdapter>() )
                throw new ArgumentException( nameof( adapterType ) );
            return adapterType;
        }

        public static void RegisterAdapterType( this Type type, Type adapterType )
        {
            PersistableHelper._adapterTypes.Add( type, PersistableHelper.ValidateAdapterType( adapterType ) );
        }

        public static bool RemoveAdapterType( this Type type )
        {
            return PersistableHelper._adapterTypes.Remove( type );
        }

        public static bool TryGetAdapterType( this Type type, out Type adapterType )
        {
            return PersistableHelper._adapterTypes.TryGetValue( type, out adapterType );
        }

        public static bool IsPersistable( this Type type )
        {
            if ( !type.Is<IPersistable>() )
                return type.Is<IAsyncPersistable>();
            return true;
        }

        public static T LoadEntire<T>( this SettingsStorage storage ) where T : IPersistable
        {
            if ( storage == null )
                throw new ArgumentNullException( nameof( storage ) );
            T instance = storage.GetValue<Type>( "type", ( Type )null ).CreateInstance<T>();
            instance.Load( storage.GetValue<SettingsStorage>( "settings", ( SettingsStorage )null ) );
            return instance;
        }

        public static SettingsStorage SaveEntire( this IPersistable persistable, bool isAssemblyQualifiedName )
        {
            if ( persistable == null )
                throw new ArgumentNullException( nameof( persistable ) );
            return new SettingsStorage().Set<string>( "type", persistable.GetType().GetTypeAsString( isAssemblyQualifiedName ) ).Set<SettingsStorage>( "settings", persistable.Save() );
        }

        public static T Clone<T>( this T obj ) where T : IPersistable
        {
            if ( obj.IsNull<T>() )
                return default( T );
            T instance = obj.GetType().CreateInstance<T>();
            instance.Load( obj.Save() );
            return instance;
        }

        public static async ValueTask<T> CloneAsync<T>( this T obj, CancellationToken cancellationToken = default( CancellationToken ) ) where T : IAsyncPersistable
        {
            T clone;            

            if ( obj.IsNull<T>() )
            {
                clone = default( T );
            }
            else
            {
                clone = obj.GetType().CreateInstance<T>();

                ValueTask<SettingsStorage> valueTask = ( ( T )obj ).SaveAsync( cancellationToken );

                await valueTask;                
            }


            return clone;
        }

        public static void Apply<T>( this T obj, T clone ) where T : IPersistable
        {
            obj.Load( clone.Save() );
        }

        public static async ValueTask ApplyAsync<T>( this T obj, T clone, CancellationToken cancellationToken = default( CancellationToken ) ) where T : IAsyncPersistable
        {
            ValueTask<SettingsStorage> valueTask = ( ( T )clone ).SaveAsync( cancellationToken );
            await obj.LoadAsync( await valueTask, cancellationToken );
        }

        public static async ValueTask<SettingsStorage> SaveAsync( this IAsyncPersistable persistable, CancellationToken cancellationToken = default( CancellationToken ) )
        {
            SettingsStorage storage;

            if ( persistable == null )
            {
                throw new ArgumentNullException( nameof( persistable ) );
            }

            storage = new SettingsStorage();
            await persistable.SaveAsync( storage, cancellationToken );

            return storage;
        }

        public static async ValueTask<IAsyncPersistable> LoadAsync( this SettingsStorage storage, Type type, CancellationToken cancellationToken = default( CancellationToken ) )
        {
            IAsyncPersistable store;            

            if ( storage == null )
            {
                throw new ArgumentNullException( nameof( storage ) );
            }
                
            store = type.CreateInstance<IAsyncPersistable>();
            await store.LoadAsync( storage, cancellationToken );
            
            return store;            
        }

        public static async ValueTask<T> LoadAsync<T>( this SettingsStorage storage, CancellationToken cancellationToken = default( CancellationToken ) ) where T : IAsyncPersistable, new()
        {
            var result = await storage.LoadAsync( typeof( T ), cancellationToken );

            return (T) result;
        }

        public static SettingsStorage Save( this IPersistable persistable )
        {
            if ( persistable == null )
                throw new ArgumentNullException( nameof( persistable ) );
            SettingsStorage storage = new SettingsStorage();
            persistable.Save( storage );
            return storage;
        }

        public static IPersistable Load( this SettingsStorage storage, Type type )
        {
            if ( storage == null )
                throw new ArgumentNullException( nameof( storage ) );
            IPersistable instance = type.CreateInstance<IPersistable>();
            instance.Load( storage );
            return instance;
        }

        public static T Load<T>( this SettingsStorage storage ) where T : IPersistable
        {
            return ( T )storage.Load( typeof( T ) );
        }

        public static void ForceLoad<T>( this T t, SettingsStorage storage ) where T : IPersistable
        {
            t.Load( storage );
        }

        public static void SetValue(
          this SettingsStorage storage,
          string name,
          IPersistable persistable )
        {
            if ( storage == null )
                throw new ArgumentNullException( nameof( storage ) );
            storage.SetValue<SettingsStorage>( name, persistable.Save() );
        }

        public static void LoadFromString<TSerializer>( this IPersistable persistable, string value ) where TSerializer : ISerializer<SettingsStorage>, new()
        {
            if ( persistable == null )
                throw new ArgumentNullException( nameof( persistable ) );
            persistable.Load( value.LoadFromString<TSerializer>() );
        }

        public static SettingsStorage LoadFromString<TSerializer>( this string value ) where TSerializer : ISerializer<SettingsStorage>, new()
        {
            if ( value == null )
                throw new ArgumentNullException( nameof( value ) );
            return CultureInfo.InvariantCulture.DoInCulture<SettingsStorage>( ( Func<SettingsStorage> )( () => new TSerializer().Deserialize<SettingsStorage>( value.UTF8() ) ) );
        }

        public static string SaveToString<TSerializer>( this IPersistable persistable ) where TSerializer : ISerializer<SettingsStorage>, new()
        {
            if ( persistable == null )
                throw new ArgumentNullException( nameof( persistable ) );
            return persistable.Save().SaveToString<TSerializer>();
        }

        public static string SaveToString<TSerializer>( this SettingsStorage settings ) where TSerializer : ISerializer<SettingsStorage>, new()
        {
            if ( settings == null )
                throw new ArgumentNullException( nameof( settings ) );
            return CultureInfo.InvariantCulture.DoInCulture<string>( ( Func<string> )( () => new TSerializer().Serialize( ( object )settings ).UTF8() ) );
        }

        public static bool IsSerializablePrimitive( this Type type )
        {
            if ( !type.IsPrimitive() )
                return type == typeof( Uri );
            return true;
        }

        public static SettingsStorage ToStorage( this IRefTuple tuple )
        {
            if ( tuple == null )
                throw new ArgumentNullException( nameof( tuple ) );
            SettingsStorage settingsStorage = new SettingsStorage();
            int num = 0;
            foreach ( object obj in tuple.Values )
                settingsStorage.Set<object>( RefTuple.GetName( num++ ), obj );
            return settingsStorage;
        }

        public static RefPair<T1, T2> ToRefPair<T1, T2>( this SettingsStorage storage )
        {
            if ( storage == null )
                throw new ArgumentNullException( nameof( storage ) );
            return new RefPair<T1, T2>()
            {
                First = storage.GetValue<T1>( "First", default( T1 ) ),
                Second = storage.GetValue<T2>( "Second", default( T2 ) )
            };
        }

        public static RefTriple<T1, T2, T3> ToRefTriple<T1, T2, T3>(
          this SettingsStorage storage )
        {
            if ( storage == null )
                throw new ArgumentNullException( nameof( storage ) );
            RefTriple<T1, T2, T3> refTriple = new RefTriple<T1, T2, T3>();
            refTriple.First = storage.GetValue<T1>( "First", default( T1 ) );
            refTriple.Second = storage.GetValue<T2>( "Second", default( T2 ) );
            refTriple.Third = storage.GetValue<T3>( "Third", default( T3 ) );
            return refTriple;
        }

        public static RefQuadruple<T1, T2, T3, T4> ToRefQuadruple<T1, T2, T3, T4>(
          this SettingsStorage storage )
        {
            if ( storage == null )
                throw new ArgumentNullException( nameof( storage ) );
            RefQuadruple<T1, T2, T3, T4> refQuadruple = new RefQuadruple<T1, T2, T3, T4>();
            refQuadruple.First = storage.GetValue<T1>( "First", default( T1 ) );
            refQuadruple.Second = storage.GetValue<T2>( "Second", default( T2 ) );
            refQuadruple.Third = storage.GetValue<T3>( "Third", default( T3 ) );
            refQuadruple.Fourth = storage.GetValue<T4>( "Fourth", default( T4 ) );
            return refQuadruple;
        }

        public static RefFive<T1, T2, T3, T4, T5> ToRefFive<T1, T2, T3, T4, T5>(
          this SettingsStorage storage )
        {
            if ( storage == null )
                throw new ArgumentNullException( nameof( storage ) );
            RefFive<T1, T2, T3, T4, T5> refFive = new RefFive<T1, T2, T3, T4, T5>();
            refFive.First = storage.GetValue<T1>( "First", default( T1 ) );
            refFive.Second = storage.GetValue<T2>( "Second", default( T2 ) );
            refFive.Third = storage.GetValue<T3>( "Third", default( T3 ) );
            refFive.Fourth = storage.GetValue<T4>( "Fourth", default( T4 ) );
            refFive.Fifth = storage.GetValue<T5>( "Fifth", default( T5 ) );
            return refFive;
        }

        public static MemberInfo ToMember( this SettingsStorage storage )
        {
            return storage.ToMember<MemberInfo>();
        }

        public static T ToMember<T>( this SettingsStorage storage ) where T : MemberInfo
        {
            if ( storage == null )
                throw new ArgumentNullException( nameof( storage ) );
            Type type = storage.GetValue<Type>( "type", ( Type )null );
            string str = storage.GetValue<string>( "value", storage.GetValue<string>( "name", string.Empty ) );
            if ( !str.IsEmpty() )
                return type.GetMember<T>( str );
            return type.To<T>();
        }

        public static SettingsStorage ToStorage<T>(
          this T member,
          bool isAssemblyQualifiedName = false )
          where T : MemberInfo
        {
            if ( ( object )member == null )
                throw new ArgumentNullException( nameof( member ) );
            SettingsStorage settingsStorage1 = new SettingsStorage();
            SettingsStorage settingsStorage2 = settingsStorage1;
            Type type = ( object )member as Type;
            if ( ( object )type == null )
                type = member.ReflectedType;
            string typeAsString = type.GetTypeAsString( isAssemblyQualifiedName );
            settingsStorage2.Set<string>( "type", typeAsString );
            if ( member.ReflectedType != ( Type )null )
                settingsStorage1.Set<string>( "value", member.Name );
            return settingsStorage1;
        }

        public static bool LoadIfNotNull( this IPersistable persistable, SettingsStorage storage )
        {
            if ( storage == null )
                return false;
            persistable.Load( storage );
            return true;
        }

        public static SettingsStorage ToStorage(
          this object value,
          bool isAssemblyQualifiedName = false )
        {
            return new SettingsStorage().Set<string>( "type", value.CheckOnNull<object>( nameof( value ) ).GetType().GetTypeAsString( isAssemblyQualifiedName ) ).Set<string>( nameof( value ), value.To<string>() );
        }

        public static object FromStorage( this SettingsStorage storage )
        {
            if ( storage == null )
                throw new ArgumentNullException( nameof( storage ) );
            object universalTime = storage.GetValue<string>( "value", ( string )null ).To( storage.GetValue<Type>( "type", ( Type )null ) );
            if ( universalTime is DateTime )
                universalTime = ( object )( ( DateTime )universalTime ).ToUniversalTime();
            return universalTime;
        }
    }
}
