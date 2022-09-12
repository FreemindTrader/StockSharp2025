
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using Ecng.Common;
using Ecng.Localization;
using StockSharp.Localization;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class CredentialsWindow : DXWindow    
    {
        private bool _isLoggedIn;

        public CredentialsWindow()
        {
            InitializeComponent();

            if ( LocalizedStrings.ActiveLanguage != Languages.Russian )
                return;

            var linkString = ( ( string ) ( ( BaseEdit ) RegisterLink ).EditValue );

            RegisterLink.EditValue = linkString.Replace( "stocksharp.com", "stocksharp.ru" ).To< Uri >( ) ;

            linkString = ( ( string ) ( ( BaseEdit ) ForgotLink ).EditValue );

            ForgotLink.EditValue = linkString.Replace( "stocksharp.com", "stocksharp.ru" ).To<Uri>( );
            
        }

        public string Email
        {
            get
            {
                return EmailCtrl.Text;
            }
            set
            {
                EmailCtrl.Text = value;
            }
        }

        public SecureString Password
        {
            get
            {
                return PasswordCtrl.Password.To<SecureString>(  );
            }
            set
            {
                PasswordCtrl.Password = value.To<string>();
            }
        }

        public bool AutoLogon
        {
            get
            {
                bool? isChecked = this.AutoLogonCtrl.IsChecked;

                if ( !isChecked.GetValueOrDefault( ) )
                    return false;

                return isChecked.HasValue;
            }
            set
            {
                AutoLogonCtrl.IsChecked = value;
            }
        }

        public bool IsLoggedIn
        {
            get
            {
                return _isLoggedIn;
            }
            set
            {
                _isLoggedIn = value;
                EmailCtrl.IsReadOnly = ( value );
                PasswordCtrl.IsEnabled = !value;
                Ok.Content = value ? LocalizedStrings.Str1457 : LocalizedStrings.Str1458;
            }
        }

        private void PasswordCtrl_EditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            TryEnableOk();
        }

        private void TryEnableOk( )
        {
            Ok.IsEnabled = !Email.IsEmpty( ) && !Password.IsEmpty( );
        }

        private void Ok_Click( object sender, RoutedEventArgs e )
        {
            DialogResult = true;
        }

        private void EmailCtrl_EditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            TryEnableOk( );
        }

        private void HyperlinkEdit_MouseDoubleClick( object sender, System.Windows.Input.MouseButtonEventArgs e )
        {
            BaseApplication.EditProxySettings( this );
        }
    }
}
