// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.FolderBrowserEditor
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using DevExpress.Xpf.Dialogs;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using Ecng.Common;
using Ecng.ComponentModel;
using Microsoft.Win32;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Markup;

using StockSharp.Localization;
using System.Windows.Controls;

namespace Ecng.Xaml
{
    internal sealed class FolderPathValidationRule : ValidationRule
    {
        public override System.Windows.Controls.ValidationResult Validate( object _param1, CultureInfo _param2 )
        {
            string str = ( string )_param1;
            if ( !str.IsEmpty() && !Directory.Exists( str ) )
                return new System.Windows.Controls.ValidationResult( false, ( object )LocalizedStrings.InvalidFolderPath );
            return System.Windows.Controls.ValidationResult.ValidResult;
        }
    }

    /// <summary>
    /// </summary>
    /// <summary>FolderBrowserEditor</summary>
    public partial class FolderBrowserEditor : ButtonEditSettings, IFolderBrowserEditor, IComponentConnector
    {


        /// <summary>
        /// </summary>
        public FolderBrowserEditor()
        {

            this.InitializeComponent();
        }

        /// <inheritdoc />
        protected override void AssignToEditCore( IBaseEdit edit )
        {
            ButtonEdit buttonEdit = edit as ButtonEdit;
            if ( buttonEdit != null )
                ValidationHelper.SetBaseEdit( ( BaseEditSettings )this, ( BaseEdit )buttonEdit );
            base.AssignToEditCore( edit );
        }

        private void OnClearButtonClicked( object o, RoutedEventArgs e )
        {
            BaseEdit ownerEdit = BaseEdit.GetOwnerEdit( ( DependencyObject )o );
            if ( ownerEdit == null || ownerEdit.IsReadOnly )
                return;
            DXFolderBrowserDialog folderBrowserDialog = new DXFolderBrowserDialog();
            string editValue = ( string )ownerEdit.EditValue;
            if ( !StringHelper.IsEmpty( editValue ) )
                folderBrowserDialog.SelectedPath = ( editValue );
            if ( !( ( CommonDialog )folderBrowserDialog ).ShowModal( ( DependencyObject )o ) )
                return;
            ownerEdit.EditValue = ( ( object )folderBrowserDialog.SelectedPath );
        }

        private void OnOpenBtnClicked( object o, RoutedEventArgs e )
        {
            BaseEdit ownerEdit = BaseEdit.GetOwnerEdit( ( DependencyObject )o );
            if ( ownerEdit == null || ownerEdit.IsReadOnly )
                return;
            ownerEdit.EditValue = ( ( object )null );
        }

        
    }
}
