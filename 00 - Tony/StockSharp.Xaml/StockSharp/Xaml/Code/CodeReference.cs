using Ecng.Common;
using Ecng.Serialization;
using System.Diagnostics;
using System.IO;

namespace StockSharp.Xaml.Code
{
    /// <summary>The link to the .NET build.</summary>
    public class CodeReference : IPersistable
    {

        private string _name;

        private string _location;

        private bool _isValid;

        /// <summary>The build name.</summary>
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        /// <summary>The path to the build.</summary>
        public string Location
        {
            get
            {
                return this._location;
            }
            set
            {
                this._location = value;
                IsValid = ( false );
                if ( StringHelper.IsEmpty( value ) )
                    return;
                try
                {
                    this.IsValid = ( File.Exists( value ) );
                }
                catch
                {
                }
            }
        }

        /// <summary>Is valid.</summary>
        public bool IsValid
        {
            get
            {
                return this._isValid;
            }

            set { this._isValid = value; }
        }

        /// <summary>Load settings.</summary>
        /// <param name="storage">Settings storage.</param>
        public void Load( SettingsStorage storage )
        {
            this.Name = ( string )storage.GetValue<string>( "Name", null );
            this.Location = ( string )storage.GetValue<string>( "Location", null );
            if ( !StringHelper.EqualsIgnoreCase( this.Location, Path.GetFileName( this.Location ) ) )
                return;
            this.Location = Path.Combine( Directory.GetCurrentDirectory(), this.Location );
        }

        /// <summary>Save settings.</summary>
        /// <param name="storage">Settings storage.</param>
        public void Save( SettingsStorage storage )
        {
            storage.SetValue<string>( "Name", this.Name );
            string path = this.Location;
            string fileName = Path.GetFileName( path );
            if ( !StringHelper.IsEmpty( fileName ) && File.Exists( fileName ) )
                path = fileName;
            storage.SetValue<string>( "Location", path );
        }
    }
}
