using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Interop;
using Ecng.Localization;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Reflection;

namespace StockSharp.Xaml
{
    public class ConnectorInfo
    {
        private readonly string _name;
        private readonly string _description;
        private readonly string _category;
        private readonly Languages _preferLanguage;
        private readonly Platforms _platform;
        private Type _adapterType;
        private readonly string _storageName;

        public ConnectorInfo( Type adapterType )
        {
            if ( adapterType == ( Type ) null )
            {
                throw new ArgumentNullException( nameof( adapterType ) );
            }
                
            if ( !typeof( IMessageAdapter ).IsAssignableFrom( adapterType ) )
            {
                throw new ArgumentException( nameof( adapterType ) );
            }
                
            AdapterType  = adapterType;
            _name        = adapterType.GetDisplayName( );
            _description = adapterType.GetDescription( );
            _category    = adapterType.GetCategory( LocalizedStrings.Str1559 );
            _storageName = StringHelper.Remove( StringHelper.Remove( adapterType.Namespace, "StockSharp", false ), ".", false );
            _platform    = InteropHelper.GetPlatform( adapterType );

            var language = adapterType.GetAttribute< MessageAdapterCategoryAttribute >( )?.Categories;

            if ( language.HasValue )
            {
                _preferLanguage = StockSharp.Messages.Extensions.GetPreferredLanguage( language.Value );
            }

            
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
        }

        public string Category
        {
            get
            {
                return _category;
            }
        }

        public Languages PreferLanguage
        {
            get
            {
                return _preferLanguage;
            }
        }

        public Platforms Platform
        {
            get
            {
                return _platform;
            }
        }

        public Type AdapterType
        {
            get
            {
                return _adapterType;
            }
            set
            {
                _adapterType = value;
            }
        }

        public string StorageName
        {
            get
            {
                return _storageName;
            }
        }
    }
}
