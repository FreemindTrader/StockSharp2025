using DevExpress.Xpf.Editors;
using Ecng.Common;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Ecng.Xaml
{
    /// <summary>
    /// </summary>
    /// <summary>SecretEdit</summary>
    public partial class SecretEdit : UserControl, IComponentConnector
    {
        /// <summary>
        /// <see cref="T:System.Windows.DependencyProperty" /> для <see cref="P:Ecng.Xaml.SecretEdit.Secret" />.
        ///     </summary>
        public static readonly DependencyProperty SecretProperty = DependencyProperty.Register( nameof( Secret ), typeof( SecureString ), typeof( SecretEdit ), new PropertyMetadata( ( object )null, ( PropertyChangedCallback )( ( o, args ) => ( ( SecretEdit )o ).SetPassword( ( SecureString )args.NewValue ) ) ) );

        private bool _suspendChanges;

        /// <summary>
        /// </summary>
        public SecretEdit()
        {
            this.InitializeComponent();
        }

        /// <summary>Секрет.</summary>
        public SecureString Secret
        {
            get
            {
                return ( SecureString )this.GetValue( SecretEdit.SecretProperty );
            }
            set
            {
                this.SetValue( SecretEdit.SecretProperty, ( object )value );
            }
        }

        private void BaseEdit_OnEditValueChanged(
          object sender,
          EditValueChangedEventArgs e )
        {
            if ( this.PasswordCtrl.Password == "5mmdfxfo56" )
                return;
            try
            {
                this._suspendChanges = true;
                this.Secret = StringHelper.Secure( this.PasswordCtrl.Password );
            }
            finally
            {
                this._suspendChanges = false;
            }
        }

        private void SetPassword( SecureString secret )
        {
            if ( this._suspendChanges )
                return;
            this.PasswordCtrl.Password = !secret.IsEmpty() ? "5mmdfxfo56" : ( string )null;
        }

    }
}
