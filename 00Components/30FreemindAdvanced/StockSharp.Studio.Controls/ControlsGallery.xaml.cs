// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.ControlsGallery
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Markup;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Dialogs;
using DevExpress.Xpf.Ribbon;
using Ecng.Common;
using Ecng.Xaml;
using StockSharp.Localization;
using StockSharp.Studio.Controls.Commands;
using StockSharp.Studio.Core.Commands;

#nullable disable
namespace StockSharp.Studio.Controls;

public partial class ControlsGallery : RibbonGalleryBarItem, IComponentConnector
{
    public static readonly DependencyProperty ControlTypesProperty = DependencyProperty.Register(nameof(ControlTypes), typeof(IEnumerable<ControlType>), typeof(ControlsGallery));


    public IEnumerable<ControlType> ControlTypes
    {
        get => (IEnumerable<ControlType>)this.GetValue(ControlsGallery.ControlTypesProperty);
        set => this.SetValue(ControlsGallery.ControlTypesProperty, (object)value);
    }

    public ControlsGallery()
    {
        this.InitializeComponent();
        if (this.IsDesignMode())
            return;
        this.ControlTypes = ControlType.GetComponents();
    }

    private void Gallery_OnItemClick(object sender, GalleryItemEventArgs e)
    {
        if (!(e.Item.DataContext is ControlType dataContext))
            return;
        OpenWindowCommand command = new OpenWindowCommand(dataContext.Type, true);
        command.SyncProcess(this.GetContext());
        (IStudioControl ctrl, bool isNew) = command.Result;
        if (!(ctrl != null & isNew))
            return;
        ctrl.FirstTimeInit();
    }

    private object GetContext()
    {
        return !(this.DataContext is IControlsGalleryControl dataContext) ? this.DataContext : dataContext.State;
    }

    private void SaveLayout_OnItemClick(object sender, ItemClickEventArgs e)
    {
        DXSaveFileDialog dxSaveFileDialog = new DXSaveFileDialog();
        dxSaveFileDialog.Filter = LocalizedStrings.LayoutFilter;
        dxSaveFileDialog.DefaultExt = "json";
        dxSaveFileDialog.RestoreDirectory = true;
        DXSaveFileDialog dlg = dxSaveFileDialog;
        if (!dlg.TryOpenWithInitialDir((DependencyObject)this, nameof(ControlsGallery)))
            return;
        SaveLayoutCommand command = new SaveLayoutCommand();
        command.SyncProcess(this.GetContext());
        if (StringHelper.IsEmpty(command.Layout))
            return;
        File.WriteAllText(dlg.FileName, command.Layout);
    }

    private void LoadLayout_OnItemClick(object sender, ItemClickEventArgs e)
    {
        DXOpenFileDialog dxOpenFileDialog = new DXOpenFileDialog();
        dxOpenFileDialog.Filter = LocalizedStrings.LayoutFilter;
        dxOpenFileDialog.CheckFileExists = true;
        dxOpenFileDialog.RestoreDirectory = true;
        DXOpenFileDialog dlg = dxOpenFileDialog;
        if (!dlg.TryOpenWithInitialDir((DependencyObject)this, nameof(ControlsGallery)))
            return;
        new LoadLayoutCommand(File.ReadAllText(dlg.FileName)).SyncProcess(this.GetContext());
    }


}
