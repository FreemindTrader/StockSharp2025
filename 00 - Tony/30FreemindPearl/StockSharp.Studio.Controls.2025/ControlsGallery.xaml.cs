// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.ControlsGallery
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Dialogs;
using DevExpress.Xpf.Ribbon;
using Ecng.Common;
using Ecng.Xaml;
using StockSharp.Localization;
using StockSharp.Studio.Controls.Commands;
using StockSharp.Studio.Core.Commands;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace StockSharp.Studio.Controls
{
    public partial class ControlsGallery : RibbonGalleryBarItem
    {
        public static readonly DependencyProperty ControlTypesProperty = DependencyProperty.Register(nameof (ControlTypes), typeof (IEnumerable<ControlType>), typeof (ControlsGallery));
        

        public IEnumerable<ControlType> ControlTypes
        {
            get
            {
                return ( IEnumerable<ControlType> ) this.GetValue( ControlsGallery.ControlTypesProperty );
            }
            set
            {
                this.SetValue( ControlsGallery.ControlTypesProperty,  value );
            }
        }

        public ControlsGallery()
        {
            this.InitializeComponent();
            if ( this.IsDesignMode() )
                return;
            this.ControlTypes = ControlType.GetComponents();
        }

        private void Gallery_OnItemClick( object sender, GalleryItemEventArgs e )
        {
            ControlType dataContext = e.Item.DataContext as ControlType;
            if ( dataContext == null )
                return;
            OpenWindowCommand command = new OpenWindowCommand(dataContext.Type, true);
            command.SyncProcess( this.GetContext() );
            ValueTuple<IStudioControl, bool> result = command.Result;
            IStudioControl studioControl = result.Item1;
            bool flag = result.Item2;
            if ( !( studioControl != null & flag ) )
                return;
            studioControl.FirstTimeInit();
        }

        private object GetContext()
        {
            IControlsGalleryControl dataContext = this.DataContext as IControlsGalleryControl;
            if ( dataContext == null )
                return this.DataContext;
            return dataContext.State;
        }

        private void SaveLayout_OnItemClick( object sender, ItemClickEventArgs e )
        {
            DXSaveFileDialog dxSaveFileDialog = new DXSaveFileDialog();
            dxSaveFileDialog.Filter = LocalizedStrings.LayoutFilter;
            dxSaveFileDialog.DefaultExt = "json";
            dxSaveFileDialog.RestoreDirectory = true;
            DXSaveFileDialog dlg = dxSaveFileDialog;
            if ( !dlg.TryOpenWithInitialDir( ( DependencyObject ) this, nameof( ControlsGallery ) ) )
                return;
            SaveLayoutCommand command = new SaveLayoutCommand();
            command.SyncProcess( this.GetContext() );
            if ( StringHelper.IsEmpty( command.Layout ) )
                return;
            File.WriteAllText( dlg.FileName, command.Layout );
        }

        private void LoadLayout_OnItemClick( object sender, ItemClickEventArgs e )
        {
            DXOpenFileDialog dxOpenFileDialog = new DXOpenFileDialog();
            dxOpenFileDialog.Filter = LocalizedStrings.LayoutFilter;
            dxOpenFileDialog.CheckFileExists = true;
            dxOpenFileDialog.RestoreDirectory = true;
            DXOpenFileDialog dlg = dxOpenFileDialog;
            if ( !dlg.TryOpenWithInitialDir( ( DependencyObject ) this, nameof( ControlsGallery ) ) )
                return;
            new LoadLayoutCommand( File.ReadAllText( dlg.FileName ) ).SyncProcess( this.GetContext() );
        }

        
    }
}
