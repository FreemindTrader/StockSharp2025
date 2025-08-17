using Ecng.Collections;
using Ecng.Common;
using LinqToDB.DataProvider;
using System;
using System.Collections.Generic;

namespace Ecng.Data
{    
    public static class DatabaseProviderRegistry
    {
        private static readonly CachedSynchronizedDictionary<Type, Func<IDataProvider>> _providers = new CachedSynchronizedDictionary<Type, Func<IDataProvider>>();

        public static void AddProvider<TProvider>( Func<TProvider> createProvider ) where TProvider : IDataProvider
        {
            AddProvider( typeof( TProvider ), () => createProvider() );
        }

        public static void AddProvider( Type provider, Func<IDataProvider> createProvider )
        {
            if ( ( object )provider == null )
                throw new ArgumentNullException( nameof( provider ) );
            if ( !provider.Is<IDataProvider>() )
                throw new ArgumentException( nameof( provider ) );
            _providers.Add( provider, createProvider );
        }

        public static void RemoveProvider<TProvider>() where TProvider : IDataProvider
        {
            RemoveProvider( typeof( TProvider ) );
        }

        public static void RemoveProvider( Type provider )
        {
            _providers.Remove( provider );
        }

        public static IEnumerable<Type> Providers
        {
            get
            {
                return _providers.CachedKeys;
            }
        }

        public static IDataProvider CreateProvider( Type provider )
        {
            CachedSynchronizedDictionary<Type, Func<IDataProvider>> providers = _providers;
            Type index = provider;
            if ( ( object )index == null )
                throw new ArgumentNullException( nameof( provider ) );
            return providers[index]();
        }
    }
}
