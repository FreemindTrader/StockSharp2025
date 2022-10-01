
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Dialogs;
using Ecng.Common;
using Microsoft.Win32;
using StockSharp.Localization;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Markup;

namespace Ecng.Xaml
{
    internal sealed class IsActiveValidationRule : ValidationRule
    {
        private bool _isActive = true;

        public bool IsActive
        {
            get
            {
                return this._isActive;
            }
            set
            {
                this._isActive = value;
            }
        }

        public override ValidationResult Validate( object d, CultureInfo e )
        {
            string str = ( string )d;
            if ( this.IsActive && !str.IsEmpty() && !File.Exists( str ) )
                return new ValidationResult( false, LocalizedStrings.InvalidFilePath );
            return ValidationResult.ValidResult;
        }
    }

    /// <summary>Визуальный редактор для выбора файла.</summary>
    /// <summary>FileBrowserPicker</summary>
    public partial class FileBrowserPicker : UserControl, IComponentConnector
    {
        /// <summary>
        /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:Ecng.Xaml.FileBrowserPicker.DefaultExt" />.
        ///     </summary>
        public static readonly DependencyProperty DefaultExtProperty = DependencyProperty.Register( nameof( DefaultExt ), typeof( string ), typeof( FileBrowserPicker ), new PropertyMetadata( ( object )null ) );
        /// <summary>
        /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:Ecng.Xaml.FileBrowserPicker.Filter" />.
        ///     </summary>
        public static readonly DependencyProperty FilterProperty = DependencyProperty.Register( nameof( Filter ), typeof( string ), typeof( FileBrowserPicker ), new PropertyMetadata( ( object )null ) );
        /// <summary>
        /// <see cref="T:System.Windows.DependencyProperty" /> для <see cref="P:Ecng.Xaml.FileBrowserPicker.File" />.
        ///     </summary>
        public static readonly DependencyProperty FileProperty = DependencyProperty.Register( nameof( File ), typeof( string ), typeof( FileBrowserPicker ), new PropertyMetadata( ( object )null ) );
        /// <summary>
        /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:Ecng.Xaml.FileBrowserPicker.IsSaving" />.
        ///     </summary>
        public static readonly DependencyProperty IsSavingProperty = DependencyProperty.Register( nameof( IsSaving ), typeof( bool ), typeof( FileBrowserPicker ), new PropertyMetadata( OnSavingPropertyChanged ) );

        public static void OnSavingPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( IsActiveValidationRule )BindingOperations.GetBinding( ( DependencyObject )( ( FileBrowserPicker )d ).FilePath, TextBox.TextProperty ).ValidationRules[0] ).IsActive = !( bool ) e.NewValue;
        }

        /// <summary>
        /// Создать <see cref="T:Ecng.Xaml.FileBrowserPicker" />.
        /// </summary>
        public FileBrowserPicker()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// </summary>
        public string DefaultExt
        {
            get
            {
                return ( string )this.GetValue( FileBrowserPicker.DefaultExtProperty );
            }
            set
            {
                this.SetValue( FileBrowserPicker.DefaultExtProperty, ( object )value );
            }
        }

        /// <summary>
        /// </summary>
        public string Filter
        {
            get
            {
                return ( string )this.GetValue( FileBrowserPicker.FilterProperty );
            }
            set
            {
                this.SetValue( FileBrowserPicker.FilterProperty, ( object )value );
            }
        }

        /// <summary>Директория.</summary>
        public string File
        {
            get
            {
                return ( string )this.GetValue( FileBrowserPicker.FileProperty );
            }
            set
            {
                this.SetValue( FileBrowserPicker.FileProperty, ( object )value );
            }
        }

        /// <summary>
        /// </summary>
        public bool IsSaving
        {
            get
            {
                return ( bool )this.GetValue( FileBrowserPicker.IsSavingProperty );
            }
            set
            {
                this.SetValue( FileBrowserPicker.IsSavingProperty, ( object )value );
            }
        }

        /// <summary>
        /// Событие изменения <see cref="P:Ecng.Xaml.FileBrowserPicker.File" />.
        /// </summary>
        public event Action<string> FileChanged;

        private void OnOpenFileBtnClicked( object d, RoutedEventArgs e )
        {
            DXFileDialog dxSaveFileDialog;
            if ( !this.IsSaving )
            {
                DXOpenFileDialog openfile = new DXOpenFileDialog();
                ( ( DXFileDialog )openfile ).CheckFileExists = ( true );
                dxSaveFileDialog = openfile;
            }
            else
                dxSaveFileDialog = new DXSaveFileDialog();
            DXFileDialog dxFileDialog = ( DXFileDialog )dxSaveFileDialog;
            dxFileDialog.RestoreDirectory = ( true );
            if ( !StringHelper.IsEmpty( this.Filter ) )
                dxFileDialog.Filter = ( this.Filter );
            if ( !StringHelper.IsEmpty( this.DefaultExt ) )
                dxFileDialog.DefaultExt = ( this.DefaultExt );
            if ( !StringHelper.IsEmpty( this.File ) )
                dxFileDialog.FileName = ( this.File );
            if ( !( ( CommonDialog )dxFileDialog ).ShowModal( ( DependencyObject )d ) )
                return;
            this.File = dxFileDialog.FileName;
            Action<string> zYmRgzHu = this.FileChanged;
            if ( zYmRgzHu == null )
                return;
            zYmRgzHu( dxFileDialog.FileName );
        }

        private void FilePath_OnTextChanged( object d, TextChangedEventArgs e )
        {
            Action<string> zYmRgzHu = this.FileChanged;
            if ( zYmRgzHu == null )
                return;
            zYmRgzHu( this.FilePath.Text );
        }



        [Serializable]
        private sealed class Lamdba0003
        {
            public static readonly FileBrowserPicker.Lamdba0003 _this = new FileBrowserPicker.Lamdba0003();

            internal void OnSavingPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
            {
                
            }
        }
    }
}
