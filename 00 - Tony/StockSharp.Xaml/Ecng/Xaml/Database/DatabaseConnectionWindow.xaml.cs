using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Data;
using LinqToDB;
using StockSharp.Localization;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace Ecng.Xaml.Database
{
    /// <summary>Window for create and edit database connection.</summary>
    /// <summary>DatabaseConnectionWindow</summary>
    public partial class DatabaseConnectionWindow : ThemedWindow, IComponentConnector
    {

        private DatabaseConnectionWindow.DBConnectionNotifiableObject _dbConnections;


        /// <summary>
        /// Initializes a new instance of the <see cref="T:Ecng.Xaml.Database.DatabaseConnectionWindow" />.
        /// </summary>
        public DatabaseConnectionWindow()
        {
            this.InitializeComponent();
            this.Pair = new DatabaseConnectionPair();
        }

        /// <summary>
        /// Show <see cref="T:Ecng.Xaml.Database.DatabaseConnectionComboBox" />.
        /// </summary>
        public bool ShowComboBox
        {
            get
            {
                return ( ( UIElement )this.ConnectionStrings ).IsVisible;
            }
            set
            {
                ( ( UIElement )this.ConnectionStrings ).Visibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private DatabaseConnectionPair GetDatabaseConnectionPair()
        {
            if ( this._dbConnections == null )
                return null;

            var dbPair = new DatabaseConnectionPair();
            dbPair.Provider = this._dbConnections.Provider;
            dbPair.ConnectionString = this._dbConnections.ConnectionString;
            return dbPair;
        }

        /// <summary>Connection info.</summary>
        public DatabaseConnectionPair Pair
        {
            get
            {
                var pair = this.GetDatabaseConnectionPair();
                if ( pair == null )
                    return null;
                
                if ( DatabaseHelper.Cache != null )
                {
                    pair = DatabaseHelper.Cache.GetOrAddCache( pair );
                }
                    
                return pair;
            }
            set
            {
                if ( value == null )
                {
                    this._dbConnections = null;
                }
                else
                {
                    this._dbConnections = new DatabaseConnectionWindow.DBConnectionNotifiableObject()
                    {
                        Provider = value.Provider
                    };
                    if ( !StringHelper.IsEmpty( value.ConnectionString ) )
                        this._dbConnections.ConnectionString = value.ConnectionString;
                }
                this.SettingsGrid.SelectedObject = ( ( object )this._dbConnections );
                ( ( UIElement )this.TestBtn).IsEnabled = value != null;
            }
        }

        private void OnEditValueChanged( object _param1, EditValueChangedEventArgs _param2 )
        {
            this.Pair = ( DatabaseConnectionPair )_param2.NewValue;
            TestBtn.IsEnabled = _param2.NewValue != null;
        }

        private void OnVerifyButtonClicked( object _param1, RoutedEventArgs _param2 )
        {
            OkBtn.IsEnabled = this.GetDatabaseConnectionPair().Verify( ( DependencyObject )this, true );
        }



        [Display( Description = "StringDescription", Name = "Settings", ResourceType = typeof( LocalizedStrings ) )]
        private sealed class DBConnectionNotifiableObject : NotifiableObject
        {

            private readonly DbConnectionStringBuilder _builder;

            private string _provider;

            public DBConnectionNotifiableObject()
            {

            }

            private T GetBuilderString<T>( string _param1 )
            {
                if ( !this._builder.Keys.Cast<string>().Contains<string>( _param1 ) )
                    return default( T );
                try
                {
                    return Converter.To<T>( this._builder[_param1] );
                }
                catch ( InvalidCastException ex )
                {
                    return default( T );
                }
            }

            [ItemsSource( typeof( DatabaseConnectionWindow.DBConnectionNotifiableObject.DbSettingsItemSource ) )]
            [Display( Description = "ProviderSettings", GroupName = "Common", Name = "Provider", Order = 0, ResourceType = typeof( LocalizedStrings ) )]
            public string Provider
            {
                get
                {
                    return this._provider;
                }
                set
                {
                    this._provider = value;
                    this.NotifyChanged( nameof( Provider ) );
                }
            }

            [Display( Description = "ServerDescription", GroupName = "Common", Name = "Str3416", Order = 1, ResourceType = typeof( LocalizedStrings ) )]
            public string Server
            {
                get
                {
                    return this.GetBuilderString<string>( nameof( Server ) );
                }
                set
                {
                    this._builder["Server"] = ( object )value;
                    this.NotifyChanged( nameof( Server ) );
                }
            }

            [Display( Description = "DatabaseDescription", GroupName = "Common", Name = "Database", Order = 2, ResourceType = typeof( LocalizedStrings ) )]
            public string Database
            {
                get
                {
                    return this.GetBuilderString<string>( nameof( Database ) );
                }
                set
                {
                    this._builder["Database"] = ( object )value;
                    this.NotifyChanged( nameof( Database ) );
                }
            }

            [Display( Description = "LoginDescription", GroupName = "Common", Name = "Login", Order = 3, ResourceType = typeof( LocalizedStrings ) )]
            public string UserName
            {
                get
                {
                    return this.GetBuilderString<string>( nameof( UserName ) );
                }
                set
                {
                    this._builder["UserName"] = ( object )value;
                    this.NotifyChanged( nameof( UserName ) );
                }
            }

            [Display( Description = "PasswordDescription", GroupName = "Common", Name = "Password", Order = 4, ResourceType = typeof( LocalizedStrings ) )]
            public SecureString Password
            {
                get
                {
                    return StringHelper.Secure( this.GetBuilderString<string>( nameof( Password ) ) );
                }
                set
                {
                    this._builder["Password"] = ( object )StringHelper.UnSecure( value );
                    this.NotifyChanged( nameof( Password ) );
                }
            }

            [Display( Description = "SecurityDescription", GroupName = "Common", Name = "IntegratedSecurity", Order = 5, ResourceType = typeof( LocalizedStrings ) )]
            public bool IntegratedSecurity
            {
                get
                {
                    return this.GetBuilderString<bool>( nameof( IntegratedSecurity ) );
                }
                set
                {
                    if ( value )
                        this._builder["IntegratedSecurity"] = ( object )true;
                    else
                        this._builder.Remove( "IntegratedSecurity" );

                    this.NotifyChanged( nameof( IntegratedSecurity ) );
                }
            }

            [Display( Description = "ConnectionStringDescription", GroupName = "Common", Name = "NewConnectionString", Order = 6, ResourceType = typeof( LocalizedStrings ) )]
            public string ConnectionString
            {
                get
                {
                    return this._builder.ConnectionString;
                }
                set
                {
                    this._builder.ConnectionString = value;
                    this.NotifyChanged( nameof( Provider ) );
                    this.NotifyChanged( nameof( Server ) );
                    this.NotifyChanged( nameof( Database ) );
                    this.NotifyChanged( nameof( UserName ) );
                    this.NotifyChanged( nameof( IntegratedSecurity ) );
                }
            }

            private sealed class DbSettingsItemSource : ItemsSourceBase<string>
            {

                private static readonly string[ ] _settingString = ( ( IEnumerable<FieldInfo> )typeof( ProviderName ).GetFields( BindingFlags.Static | BindingFlags.Public ) ).Select<FieldInfo, object>( new Func<FieldInfo, object>( DatabaseConnectionWindow.DBConnectionNotifiableObject.DbSettingsItemSource.Lamdba0003._this.LDM04 ) ).OfType<string>().ToArray<string>();

                public DbSettingsItemSource()
                {

                }

                protected override IEnumerable<string> GetValues()
                {
                    return ( IEnumerable<string> )DatabaseConnectionWindow.DBConnectionNotifiableObject.DbSettingsItemSource._settingString;
                }

                [Serializable]
                private sealed class Lamdba0003
                {
                    public static readonly DatabaseConnectionWindow.DBConnectionNotifiableObject.DbSettingsItemSource.Lamdba0003 _this = new DatabaseConnectionWindow.DBConnectionNotifiableObject.DbSettingsItemSource.Lamdba0003();

                    internal object LDM04( FieldInfo _param1 )
                    {
                        return _param1.GetValue( ( object )null );
                    }
                }
            }
        }
    }
}
