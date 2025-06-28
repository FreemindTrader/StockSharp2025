// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.OrderLogPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Grid;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;
using DataType = StockSharp.Messages.DataType;

#nullable disable
namespace StockSharp.Studio.Controls;

[Display(ResourceType = typeof(LocalizedStrings), Name = "OrderLog", Description = "OrderLog")]
[VectorIcon("Order")]
[Doc("topics/terminal/user_interface/components/order_log.html")]
public partial class OrderLogPanel :
  BaseSubscriptionStudioControl,
  IBarManagerControl,
  IComponentConnector
{
    
    BarManager IBarManagerControl.Bar => this.BarManager;

    protected override DataType DataType { get; } = DataType.OrderLog;

    public OrderLogPanel()
    {
        this.InitializeComponent();
        this.TryHideBar();
        this.OrderLogGrid.LayoutChanged += RaiseChangedCommand;
        this.OrderLogGrid.SelectionChanged += (GridSelectionChangedEventHandler)((s, e) => this.RaiseSelectedCommand());
        this.GotFocus += (RoutedEventHandler)((s, e) => this.RaiseSelectedCommand());
        this.Register<ResetedCommand>((object)this, false, (Action<ResetedCommand>)(cmd => ((ICollection<IOrderLogMessage>)this.OrderLogGrid.LogItems).Clear()));
        this.Register<EntityCommand<IOrderLogMessage>>((object)this, false, new Action<EntityCommand<IOrderLogMessage>>((this.OrderLogGrid.LogItems).AddEntity<IOrderLogMessage>));
    }

    private void RaiseSelectedCommand()
    {
        this.OrderLogGrid.SelectedLogItem.SendSelect<IOrderLogMessage>((object)this);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.SetValue<SettingsStorage>("OrderLogGrid", PersistableHelper.Save((IPersistable)this.OrderLogGrid));
        storage.SetValue<string>("BarManager", this.BarManager.SaveDevExpressControl());
        this.SaveSubscriptions(storage);
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        PersistableHelper.LoadIfNotNull((IPersistable)this.OrderLogGrid, storage, "OrderLogGrid");
        string settings = storage.GetValue<string>("BarManager", (string)null);
        if (settings != null)
            this.BarManager.LoadDevExpressControl(settings);
        this.LoadSubscriptions(storage);
    }

    public override void Dispose(CloseReason reason)
    {
        this.OrderLogGrid.LayoutChanged -= RaiseChangedCommand;
        base.Dispose(reason);
    }

    private void Filter_OnClick(object sender, RoutedEventArgs e) => this.FilterConfig();

    
}
