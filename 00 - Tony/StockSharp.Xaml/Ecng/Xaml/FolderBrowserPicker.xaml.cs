using DevExpress.Xpf.Core;
using DevExpress.Xpf.Dialogs;
using Ecng.Common;
using Microsoft.Win32;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace Ecng.Xaml
{
    public partial class FolderBrowserPicker : UserControl, IComponentConnector
    {
        /// <summary>
        /// <see cref="T:System.Windows.DependencyProperty" /> для <see cref="P:Ecng.Xaml.FolderBrowserPicker.Folder" />.
        ///     </summary>
        public static readonly DependencyProperty FolderProperty = DependencyProperty.Register( nameof( Folder ), typeof( string ), typeof( FolderBrowserPicker ), new PropertyMetadata( ( object )null ) );

        /// <summary>
        /// Создать <see cref="T:Ecng.Xaml.FolderBrowserPicker" />.
        /// </summary>
        public FolderBrowserPicker()
        {
            this.InitializeComponent();
        }

        /// <summary>Директория.</summary>
        public string Folder
        {
            get
            {
                return ( string )this.GetValue( FolderBrowserPicker.FolderProperty );
            }
            set
            {
                this.SetValue( FolderBrowserPicker.FolderProperty, ( object )value );
            }
        }

        /// <summary>
        /// Событие изменения <see cref="P:Ecng.Xaml.FolderBrowserPicker.Folder" />.
        /// </summary>
        public event Action<string> FolderChanged;

        private void OpenFolder_Click( object _param1, RoutedEventArgs _param2 )
        {
            DXFolderBrowserDialog folderBrowserDialog = new DXFolderBrowserDialog();
            if ( !StringHelper.IsEmpty( this.FolderPath.Text ) )
                folderBrowserDialog.SelectedPath = ( this.FolderPath.Text );
            if ( !( ( CommonDialog )folderBrowserDialog ).ShowModal( ( DependencyObject )_param1 ) )
                return;
            this.FolderPath.Text = folderBrowserDialog.SelectedPath;
            Action<string> z8bphLv4 = this.FolderChanged;
            if ( z8bphLv4 == null )
                return;
            z8bphLv4( folderBrowserDialog.SelectedPath );
        }

        private void OnFolderPathTextChanged( object _param1, TextChangedEventArgs _param2 )
        {
            Action<string> z8bphLv4 = this.FolderChanged;
            if ( z8bphLv4 == null )
                return;
            z8bphLv4( this.FolderPath.Text );
        }

    }
}
