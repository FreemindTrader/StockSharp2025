using DevExpress.Xpf.Core;
using Ecng.Xaml.DevExp;
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
    public partial class ChangePasswordWindow : DXWindow
    {
        public Action Process;

        public ChangePasswordWindow()
        {
            InitializeComponent();
        }

        private void ChangePassword_Click( object sender, RoutedEventArgs e )
        {
            ( ( UIElement ) this.ChangePassword ).IsEnabled = false;

            Process?.Invoke( );            
        }

        public void UpdateResult( string result )
        {
            this.Result.Text = result;
            ( ( UIElement ) this.ChangePassword ).IsEnabled = true;
        }

        public SecureString CurrentPassword
        {
            get
            {
                return this.CurrentPasswordCtrl.Secret;
            }
        }

        public SecureString NewPassword
        {
            get
            {
                return this.NewPasswordCtrl.Secret;
            }
        }
    }
}
