// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.FileBrowserEditor
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
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

namespace Ecng.Xaml
{
    /// <summary>
    /// </summary>
    /// <summary>FileBrowserEditor</summary>
    public partial class FileBrowserEditor : ButtonEditSettings, IFileBrowserEditor, IComponentConnector
    {

        private string _defaultExt;

        private string _filter;

        private bool _isSaving;


        /// <summary>
        /// </summary>
        public FileBrowserEditor()
        {

            this.InitializeComponent();
        }

        /// <summary>
        /// </summary>
        public string DefaultExt
        {
            get
            {
                return this._defaultExt;
            }
            set
            {
                this._defaultExt = value;
            }
        }

        /// <summary>
        /// </summary>
        public string Filter
        {
            get
            {
                return this._filter;
            }
            set
            {
                this._filter = value;
            }
        }

        /// <summary>
        /// </summary>
        public bool IsSaving
        {
            get
            {
                return this._isSaving;
            }
            set
            {
                this._isSaving = value;
            }
        }

        /// <inheritdoc />
        protected override void AssignToEditCore( IBaseEdit edit )
        {
            ButtonEdit buttonEdit = edit as ButtonEdit;
            if ( buttonEdit != null )
                ValidationHelper.SetBaseEdit( ( BaseEditSettings )this, ( BaseEdit )buttonEdit );
            base.AssignToEditCore( edit );
        }

        private void OnClearButtonClicked(
          object _param1,
          RoutedEventArgs _param2 )
        {
            BaseEdit ownerEdit = BaseEdit.GetOwnerEdit( ( DependencyObject )_param1 );
            if ( ownerEdit == null || ownerEdit.IsReadOnly )
                return;
            DXSaveFileDialog dxSaveFileDialog;
            if ( !this.IsSaving )
            {
                DXOpenFileDialog dxOpenFileDialog = new DXOpenFileDialog();
                ( ( DXFileDialog )dxOpenFileDialog ).CheckFileExists = ( true );
                dxSaveFileDialog = ( DXSaveFileDialog )dxOpenFileDialog;
            }
            else
                dxSaveFileDialog = new DXSaveFileDialog();
            DXFileDialog dxFileDialog = ( DXFileDialog )dxSaveFileDialog;
            dxFileDialog.RestoreDirectory = ( true );
            if ( !StringHelper.IsEmpty( this.Filter ) )
                dxFileDialog.Filter = ( this.Filter );
            if ( !StringHelper.IsEmpty( this.DefaultExt ) )
                dxFileDialog.DefaultExt = ( this.DefaultExt );
            string editValue = ( string )ownerEdit.EditValue;
            if ( !StringHelper.IsEmpty( editValue ) )
                dxFileDialog.FileName = ( editValue );
            if ( !( ( CommonDialog )dxFileDialog ).ShowModal( ( DependencyObject )_param1 ) )
                return;
            ownerEdit.EditValue = ( ( object )dxFileDialog.FileName );
        }

        private void OnOpenBtnClicked(
          object _param1,
          RoutedEventArgs _param2 )
        {
            BaseEdit ownerEdit = BaseEdit.GetOwnerEdit( ( DependencyObject )_param1 );
            if ( ownerEdit == null || ownerEdit.IsReadOnly )
                return;
            ownerEdit.EditValue = ( ( object )null );
        }

        
    }
}
