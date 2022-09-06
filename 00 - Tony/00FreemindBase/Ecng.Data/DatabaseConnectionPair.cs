using Ecng.ComponentModel;
using Ecng.Serialization;
using System;
using System.Linq;

namespace Ecng.Data
{
    public class DatabaseConnectionPair : NotifiableObject, IPersistable
    {
        private Type _provider = DatabaseProviderRegistry.Providers.FirstOrDefault<Type>();
        private string _connectionString;

        public Type Provider
        {
            get
            {
                return this._provider;
            }
            set
            {
                this._provider = value;
                this.UpdateTitle();
            }
        }

        public string ConnectionString
        {
            get
            {
                return this._connectionString;
            }
            set
            {
                this._connectionString = value;
                this.UpdateTitle();
            }
        }

        public string Title
        {
            get
            {
                return "(" + this.Provider.Name + ") " + this.ConnectionString;
            }
        }

        private void UpdateTitle()
        {
            this.NotifyChanged( "Title" );
        }

        public override string ToString()
        {
            return this.Title;
        }

        void IPersistable.Load( SettingsStorage storage )
        {
            this.Provider = storage.GetValue<Type>( "Provider", ( Type )null );
            this.ConnectionString = storage.GetValue<string>( "ConnectionString", ( string )null );
        }

        void IPersistable.Save( SettingsStorage storage )
        {
            storage.Set<Type>( "Provider", this.Provider ).Set<string>( "ConnectionString", this.ConnectionString );
        }
    }
}
