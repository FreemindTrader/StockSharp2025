using Ecng.ComponentModel;
using Ecng.Serialization;
using System;
using System.Linq;

namespace Ecng.Data
{
    public class DatabaseConnectionPair : NotifiableObject, IPersistable
    {
        private string _provider;
        private string _connectionString;

        public string Provider
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
                return "(" + Provider + ") " + ConnectionString;
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
            Provider = storage.GetValue<string>( "Provider", null );
            ConnectionString = storage.GetValue<string>( "ConnectionString", null );
        }

        void IPersistable.Save( SettingsStorage storage )
        {
            storage.Set( "Provider", Provider ).Set( "ConnectionString", ConnectionString );
        }
    }
}
