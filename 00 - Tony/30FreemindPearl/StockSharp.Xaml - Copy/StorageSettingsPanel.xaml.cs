using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using Ecng.Xaml.DevExp;
using Ookii.Dialogs.Wpf;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using XamlHelper = Ecng.Xaml.XamlHelper;

namespace StockSharp.Xaml
{
    /// <summary>
    /// Interaction logic for StorageSettingsPanel.xaml
    /// </summary>
    public partial class StorageSettingsPanel : UserControl,  IPersistable
    {
        public static readonly DependencyProperty IsCredentialsEnabledProperty = DependencyProperty.Register(nameof (IsCredentialsEnabled), typeof (bool), typeof (StorageSettingsPanel), (PropertyMetadata) new FrameworkPropertyMetadata((object) true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        private readonly PropertyChangeNotifier _notifier;
                
        public StorageSettingsPanel( )
        {
            InitializeComponent();
            Address = "net.tcp://localhost:8000";
            _notifier = new PropertyChangeNotifier( ( DependencyObject ) AddressCtrl, ( DependencyProperty ) EndPointEditor.EndPointProperty );
            _notifier.ValueChanged += ( new Action( OnValueChanged ) );
        }

        public bool IsCredentialsEnabled
        {
            get
            {
                return ( bool ) GetValue( StorageSettingsPanel.IsCredentialsEnabledProperty );
            }
            set
            {
                SetValue( StorageSettingsPanel.IsCredentialsEnabledProperty, ( object ) value );
            }
        }

        public event Action RemotePathChanged;

        public bool IsLocal
        {
            get
            {
                bool? isChecked = IsLocalCtrl.IsChecked;
                if ( !isChecked.GetValueOrDefault() )
                {
                    return false;
                }

                return isChecked.HasValue;
            }
            set
            {
                ( value ? ( ToggleButton ) IsLocalCtrl : ( ToggleButton ) IsRemoteCtrl ).IsChecked = new bool?( true );
            }
        }

        public string Path
        {
            get
            {
                return PathCtrl.Text;
            }
            set
            {
                PathCtrl.Text =  value;
            }
        }

        public string Address
        {
            get
            {
                EndPoint endPoint = AddressCtrl.EndPoint;

                if ( endPoint == null )
                {
                    return ( string ) null;
                }

                return ( string ) endPoint.To<string>( );
            }
            set
            {
                AddressCtrl.EndPoint = value.To<EndPoint>( );
            }
        }

        public string Login
        {
            get
            {
                return LoginCtrl.Text;
            }
            set
            {
                LoginCtrl.Text =  value;
            }
        }

        public SecureString Password
        {
            get
            {
                return PasswordCtrl.Secret;
            }
            set
            {
                PasswordCtrl.Secret =  value;
            }
        }

        private void BrowseButtonClicked( object sender, RoutedEventArgs e )
        {
            VistaFolderBrowserDialog folderBrowserDialog = new VistaFolderBrowserDialog();

            if ( !folderBrowserDialog.ShowModal( this ) )
            {
                return;
            } 
            
            PathCtrl.Text = folderBrowserDialog.SelectedPath;
        }

        private void OnValueChanged( )
        {
            Action action0 = RemotePathChanged;
            if ( action0 == null )
            {
                return;
            }

            action0();
        }

        void IPersistable.Load( SettingsStorage storage )
        {
            IsLocal = ( bool ) storage.GetValue<bool>( "IsLocal",  false );
            Path = ( string ) storage.GetValue<string>( "Path",  null );
            Address = ( string ) storage.GetValue<string>( "Address",  null );
            Login = ( string ) storage.GetValue<string>( "Login",  null );
            Password = ( SecureString ) storage.GetValue<SecureString>( "Password",  null );
        }

        void IPersistable.Save( SettingsStorage storage )
        {
            storage.SetValue<bool>( "IsLocal",  IsLocal );
            storage.SetValue<string>( "Path",  Path );
            storage.SetValue<string>( "Address",  Address );
            storage.SetValue<string>( "Login",  Login );
            storage.SetValue<SecureString>( "Password",  Password );
        }

        
    }
}
