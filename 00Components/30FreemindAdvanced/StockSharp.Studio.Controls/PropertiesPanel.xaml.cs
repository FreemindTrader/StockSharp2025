// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.PropertiesPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Data;
using System.Windows.Markup;
using DevExpress.Xpf.PropertyGrid;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Xaml;
using StockSharp.Localization;
using StockSharp.Studio.Controls.Commands;
using StockSharp.Studio.Core.Commands;

#nullable disable
namespace StockSharp.Studio.Controls;

[Display(ResourceType = typeof(LocalizedStrings), Name = "Properties", Description = "PropertiesPanel")]
[VectorIcon("Edit")]
public partial class PropertiesPanel : BaseStudioControl, IComponentConnector
{

    public PropertiesPanel()
    {
        this.InitializeComponent();
        if (Extensions.HideBars)
        {
            this.Register<BindCommand>(this, true, (Action<BindCommand>)(cmd =>
            {
                if (!cmd.CheckControl((IStudioControl)this))
                    return;
                if (!StringHelper.IsEmpty(cmd.Binder.BinderTitle))
                    this.SetBindings(BaseStudioControl.TitleProperty, cmd.Binder, "BinderTitle", BindingMode.OneWay, (IValueConverter)null, null);

                cmd.Binder.SettingsChanged += s =>
                {
                    this.PropertyGrid.SelectedObject = null;
                    this.PropertyGrid.SelectedObject = s;
                };
                updateSettings(cmd.Binder.Settings);
            }), (Func<BindCommand, bool>)null);
            this.WhenLoaded((Action)(() => new RequestBindSource((IStudioControl)this).SyncProcess(this)));
        }
        else
        {
            Type prevType = (Type)null;
            this.Register<SelectCommand>(this, true, (Action<SelectCommand>)(cmd =>
            {
                if (cmd.Instance == null && cmd.InstanceType != prevType)
                    return;
                prevType = cmd.InstanceType;
                this.WatermarkTextBlock.Visibility = System.Windows.Visibility.Collapsed;
                if (this.PropertyGrid.SelectedObject == cmd.Instance)
                    this.PropertyGrid.SelectedObject = null;
                this.PropertyGrid.IsReadOnly = !cmd.CanEdit;
                this.PropertyGrid.SelectedObject = cmd.Instance;
            }), (Func<SelectCommand, bool>)null);
        }

        void updateSettings(object settings)
        {
            this.PropertyGrid.SelectedObject = (object)null;
            this.PropertyGrid.SelectedObject = settings;
        }
    }

    private void PropertyGrid_CustomExpand(object sender, CustomExpandEventArgs args)
    {
        if (!args.IsInitializing)
            return;
        args.IsExpanded = true;
    }


}
