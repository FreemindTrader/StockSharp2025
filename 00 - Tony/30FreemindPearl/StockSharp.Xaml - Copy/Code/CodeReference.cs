

using Ecng.Common;
using Ecng.Serialization;
using System.IO;

namespace StockSharp.Xaml.Code
{
    public class CodeReference : IPersistable
    {
        private string _name;
        private string _location;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public string Location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
            }
        }

        public void Load( SettingsStorage storage )
        {
            Name = ( string ) storage.GetValue<string>( "Name",  null );
            Location = ( string ) storage.GetValue<string>( "Location",  null );
            if ( !StringHelper.CompareIgnoreCase( Location, Path.GetFileName( Location ) ) )
                return;
            Location = Path.Combine( Directory.GetCurrentDirectory(), Location );
        }

        public void Save( SettingsStorage storage )
        {
            storage.SetValue<string>( "Name",  Name );
            string path = Location;
            string fileName = Path.GetFileName(path);
            if ( !StringHelper.IsEmpty( fileName ) && File.Exists( fileName ) )
                path = fileName;
            storage.SetValue<string>( "Location",  path );
        }
    }
}
