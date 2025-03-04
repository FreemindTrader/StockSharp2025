using Ecng.Collections;
using Ecng.ComponentModel;
using Ecng.Configuration;
using Ecng.Serialization;
using StockSharp.Localization;
using StockSharp.Server.Core;
using StockSharp.Studio.Controls;
using StockSharp.Xaml;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Hydra.Panes
{
    [Guid( "97430589-8202-4182-933D-AF386AA1FD17" )]
    [DisplayNameLoc( "Users" )]
    [VectorIcon( "Profile" )]
    public partial class UsersPane : BaseStudioControl, IComponentConnector
    {
        private readonly PermissionCredentialsStorage _credentialsStorage;
        
        public UsersPane()
        {
            InitializeComponent();
            _credentialsStorage = ConfigManager.GetService<PermissionCredentialsStorage>();
            CredentialsPanel.Credentials.AddRange( _credentialsStorage.Credentials.Select( c => c.Clone() ) );
        }

        private void CredentialsPanel_Saving()
        {
            _credentialsStorage.Credentials = CredentialsPanel.Credentials.Select( c => c.Clone() ).ToArray();
            _credentialsStorage.SaveCredentials();
            MainWindow.Instance.LoadCredentials();
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "CredentialsPanel", CredentialsPanel.Save() );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            if ( !storage.Contains( "CredentialsPanel" ) )
                return;
            CredentialsPanel.Load( storage.GetValue<SettingsStorage>( "CredentialsPanel", null ) );
        }        
    }
}
