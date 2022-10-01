using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace StockSharp.Xaml
{
    /// <summary>Information about connection.</summary>
    public class ConnectorInfo
    {

        private readonly string _name;

        private readonly string _description;

        private readonly string _category;

        private readonly int _categoryOrder;

        private readonly string _preferLanguage;

        private readonly Platforms _platform;

        private Type _adapterType;

        private readonly string _storageName;

        private readonly Uri _icon;

        private static readonly Dictionary<string, int> _dictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Xaml.ConnectorInfo" />.
        /// </summary>
        /// <param name="adapter">Adapter.</param>
        public ConnectorInfo( IMessageAdapter adapter ) : this( ( ( object )adapter ).GetType() )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Xaml.ConnectorInfo" />.
        /// </summary>
        /// <param name="adapterType">The type of transaction or market data adapter.</param>
        public ConnectorInfo( Type adapterType )
        {
            if ( adapterType == ( Type )null )
                throw new ArgumentNullException( nameof( adapterType ) );

            if ( !typeof( IMessageAdapter ).IsAssignableFrom( adapterType ) )
                throw new ArgumentException( nameof( adapterType ) );

            this.AdapterType = adapterType;
            this._name = adapterType.GetDisplayName();
            this._description = adapterType.GetDescription();
            this._category = adapterType.GetCategory( LocalizedStrings.Common );
            this._storageName = StringHelper.Remove( StringHelper.Remove( adapterType.Namespace, "StockSharp", false ), ".", false );
            this._platform = TypeHelper.GetPlatform( adapterType );
            this._preferLanguage = StockSharp.Messages.Extensions.GetPreferredLanguage( ( MessageAdapterCategories? )( ( MessageAdapterCategoryAttribute )AttributeHelper.GetAttribute<MessageAdapterCategoryAttribute>( ( ICustomAttributeProvider )adapterType, true ) )?.Categories );
            this._icon = adapterType.GetIconUrl();
            int num;
            this._categoryOrder = StringHelper.IsEmptyOrWhiteSpace( this.Category ) ? 3 : ( ConnectorInfo._dictionary.TryGetValue( this.Category, out num ) ? num : 3 );
        }

        static ConnectorInfo()
        {
            bool isRussian = LocalizedStrings.ActiveLanguage == "ru";
            ConnectorInfo._dictionary = new Dictionary<string, int>()
            {
                [LocalizedStrings.Russia] = isRussian ? 1 : 4,
                [LocalizedStrings.America] = isRussian ? 4 : 1,
                [LocalizedStrings.Cryptocurrency] = 0,
                [LocalizedStrings.Forex] = 2
            };
        }

        /// <summary>The connection name.</summary>
        public string Name
        {
            get
            {
                return this._name;
            }
        }

        /// <summary>The connection description.</summary>
        public string Description
        {
            get
            {
                return this._description;
            }
        }

        /// <summary>The connection description.</summary>
        public string Category
        {
            get
            {
                return this._category;
            }
        }

        /// <summary>Category order.</summary>
        public int CategoryOrder
        {
            get
            {
                return this._categoryOrder;
            }
        }

        /// <summary>The target audience.</summary>
        public string PreferLanguage
        {
            get
            {
                return this._preferLanguage;
            }
        }

        /// <summary>Platform.</summary>
        public Platforms Platform
        {
            get
            {
                return this._platform;
            }
        }

        /// <summary>The type of adapter.</summary>
        public Type AdapterType
        {
            get
            {
                return this._adapterType;
            }
            set
            {
                this._adapterType = value;
            }
        }

        /// <summary>Storage name.</summary>
        public string StorageName
        {
            get
            {
                return this._storageName;
            }
        }

        /// <summary>Icon.</summary>
        public Uri Icon
        {
            get
            {
                return this._icon;
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return this.Name;
        }
    }
}
