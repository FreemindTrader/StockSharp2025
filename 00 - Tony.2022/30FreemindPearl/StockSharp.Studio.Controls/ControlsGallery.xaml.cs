
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Dialogs;
using DevExpress.Xpf.Ribbon;
using Ecng.Common;
using Ecng.Xaml;
using StockSharp.Localization;
using StockSharp.Studio.Core.Commands;
using StockSharp.Studio.Core.Configuration;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Studio.Controls
{
    public partial class ControlsGallery : RibbonGalleryBarItem, IComponentConnector
    {
        public static readonly DependencyProperty ControlTypesProperty = DependencyProperty.Register( nameof( ControlTypes ), typeof( IEnumerable<ControlType> ), typeof( ControlsGallery ) );
        

        public IEnumerable<ControlType> ControlTypes
        {
            get
            {
                return ( IEnumerable<ControlType> )GetValue( ControlTypesProperty );
            }
            set
            {
                SetValue( ControlTypesProperty, value );
            }
        }

        public ControlsGallery()
        {
            InitializeComponent();
            if ( this.IsDesignMode() )
                return;
            ControlTypes = BaseAppConfig<StudioAppConfig, StudioSection>.Instance.StrategyControls.GetControlTypes();
        }

        private void Gallery_OnItemClick( object sender, GalleryItemEventArgs e )
        {
            ControlType dataContext1 = e.Item.DataContext as ControlType;
            if ( dataContext1 == null )
                return;
            IControlsGalleryControl dataContext2 = DataContext as IControlsGalleryControl;
            if ( dataContext2 == null )
                return;
            OpenWindowCommand command = new OpenWindowCommand( Guid.NewGuid().To<string>(), dataContext1.Type, true );
            command.SyncProcess( dataContext2.State );
            command.Result?.FirstTimeInit();
        }

        private void SaveLayout_OnItemClick( object sender, ItemClickEventArgs e )
        {
            DXSaveFileDialog dxSaveFileDialog = new DXSaveFileDialog();
            dxSaveFileDialog.Filter = LocalizedStrings.Str3584;
            dxSaveFileDialog.DefaultExt = "xml";
            dxSaveFileDialog.RestoreDirectory = true;
            DXSaveFileDialog dlg = dxSaveFileDialog;
            if ( !dlg.ShowModal( this ) )
                return;
            SaveLayoutCommand command = new SaveLayoutCommand();
            command.SyncProcess( ( ( IControlsGalleryControl )DataContext ).State );
            if ( command.Layout.IsEmpty() )
                return;
            File.WriteAllText( dlg.FileName, command.Layout );
        }

        private void LoadLayout_OnItemClick( object sender, ItemClickEventArgs e )
        {
            DXOpenFileDialog dxOpenFileDialog = new DXOpenFileDialog();
            dxOpenFileDialog.Filter = LocalizedStrings.Str3584;
            dxOpenFileDialog.CheckFileExists = true;
            dxOpenFileDialog.RestoreDirectory = true;
            DXOpenFileDialog dlg = dxOpenFileDialog;
            if ( !dlg.ShowModal( this ) )
                return;
            new LoadLayoutCommand( File.ReadAllText( dlg.FileName ) ).SyncProcess( ( ( IControlsGalleryControl )DataContext ).State );
        }

        
    }
}
