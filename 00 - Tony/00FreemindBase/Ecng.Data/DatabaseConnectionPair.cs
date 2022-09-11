using Ecng.ComponentModel;
using Ecng.Serialization;
using System;
using System.Linq;

namespace Ecng.Data
{
    public class DatabaseConnectionPair : NotifiableObject, IPersistable
    {
        private Type _provider = DatabaseProviderRegistry.Providers.FirstOrDefault();
        private string _connectionString;

        public Type Provider
        {
            get
            {
                return _provider;
            }
            set
            {
                _provider = value;
                UpdateTitle();
            }
        }

        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                _connectionString = value;
                UpdateTitle();
            }
        }

        public string Title
        {
            get
            {
                return "(" + Provider.Name + ") " + ConnectionString;
            }
        }

        private void UpdateTitle()
        {
            NotifyChanged( "Title" );
        }

        public override string ToString()
        {
            return Title;
        }

        void IPersistable.Load( SettingsStorage storage )
        {
            Provider = storage.GetValue<Type>( "Provider", null );
            ConnectionString = storage.GetValue<string>( "ConnectionString", null );
        }

        void IPersistable.Save( SettingsStorage storage )
        {
            storage.Set( "Provider", Provider ).Set( "ConnectionString", ConnectionString );
        }
    }
}
