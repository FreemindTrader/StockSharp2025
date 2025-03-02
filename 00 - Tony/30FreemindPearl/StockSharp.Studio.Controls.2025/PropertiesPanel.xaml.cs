// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.PropertiesPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.PropertyGrid;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Xaml;
using StockSharp.Localization;
using StockSharp.Studio.Controls.Commands;
using StockSharp.Studio.Core.Commands;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Windows.Data;

namespace StockSharp.Studio.Controls
{
    [Display( Description = "PropertiesPanel", Name = "Properties", ResourceType = typeof( LocalizedStrings ) )]
    [VectorIcon( "Edit" )]
    public partial class PropertiesPanel : BaseStudioControl
    {
        
        public PropertiesPanel()
        {
            this.InitializeComponent();
            if ( Extensions.HideBars )
            {
                this.Register<BindCommand>(  this, true, ( Action<BindCommand> ) ( cmd =>
                {
                    if ( !cmd.CheckControl( ( IStudioControl ) this ) )
                        return;
                    if ( !StringHelper.IsEmpty( cmd.Binder.BinderTitle ) )
                        this.SetBindings( BaseStudioControl.TitleProperty,  cmd.Binder, "BinderTitle", BindingMode.OneWay, ( IValueConverter ) null,  null );
                    
                    cmd.Binder.SettingsChanged += s =>
                    {
                        this.PropertyGrid.SelectedObject = null;
                        this.PropertyGrid.SelectedObject = s;
                    };
                    updateSettings( cmd.Binder.Settings );
                } ), ( Func<BindCommand, bool> ) null );
                this.WhenLoaded( ( Action ) ( () => new RequestBindSource( ( IStudioControl ) this ).SyncProcess(  this ) ) );
            }
            else
            {
                Type prevType = (Type) null;
                this.Register<SelectCommand>(  this, true, ( Action<SelectCommand> ) ( cmd =>
                {
                    if ( cmd.Instance == null && cmd.InstanceType != prevType )
                        return;
                    prevType = cmd.InstanceType;
                    this.WatermarkTextBlock.Visibility = System.Windows.Visibility.Collapsed;
                    if ( this.PropertyGrid.SelectedObject == cmd.Instance )
                        this.PropertyGrid.SelectedObject =  null;
                    this.PropertyGrid.IsReadOnly = !cmd.CanEdit;
                    this.PropertyGrid.SelectedObject = cmd.Instance;
                } ), ( Func<SelectCommand, bool> ) null );
            }

            void updateSettings( object settings )
            {
                this.PropertyGrid.SelectedObject =  null;
                this.PropertyGrid.SelectedObject = settings;
            }
        }

        private void PropertyGrid_CustomExpand( object sender, CustomExpandEventArgs args )
        {
            if ( !args.IsInitializing )
                return;
            args.IsExpanded = true;
        }        
    }
}
