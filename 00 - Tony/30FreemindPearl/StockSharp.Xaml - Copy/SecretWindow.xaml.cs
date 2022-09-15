using DevExpress.Xpf.Core;
using Ecng.Xaml.DevExp;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class SecretWindow : DXWindow
    {
        public static readonly RoutedCommand OkCommand = new RoutedCommand();
        
        public SecretWindow( )
        {
            InitializeComponent();
        }

        public SecureString Secret
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

        private void OnSecretWindowLoaded( object sender, RoutedEventArgs e )
        {
            ( ( UIElement ) PasswordCtrl ).Focus();
        }

        private void CanExecuteOkCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = Secret != null;
        }

        private void ExecuteOkCommand( object sender, ExecutedRoutedEventArgs e )
        {
            ( ( Window ) this ).Close();
        }
    }
}