// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.TradesPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Grid;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;
using DataType = StockSharp.Messages.DataType;

#nullable disable
namespace StockSharp.Studio.Controls;

[Display(ResourceType = typeof(LocalizedStrings), Name = "TradesFeed", Description = "TradesPanel")]
[VectorIcon("Deal")]
[Doc("topics/designer/user_interface/components/tick_trades.html")]
public partial class TradesPanel :
  BaseSubscriptionStudioControl,
  IBarManagerControl,
  IComponentConnector
{

    BarManager IBarManagerControl.Bar => this.BarManager;

    protected override DataType DataType { get; } = DataType.Ticks;

    public TradesPanel()
    {
        this.InitializeComponent();
        this.TryHideBar();
        this.TradesGrid.LayoutChanged += RaiseChangedCommand;
        this.TradesGrid.SelectionChanged += (GridSelectionChangedEventHandler)((s, e) => this.RaiseSelectedCommand());
        this.GotFocus += (RoutedEventHandler)((s, e) => this.RaiseSelectedCommand());
        this.Register<ResetedCommand>((object)this, false, (Action<ResetedCommand>)(cmd => ((ICollection<ITickTradeMessage>)this.TradesGrid.Trades).Clear()));
        this.Register<EntityCommand<ITickTradeMessage>>((object)this, false, new Action<EntityCommand<ITickTradeMessage>>((this.TradesGrid.Trades).AddEntity<ITickTradeMessage>));
        this.Register<FirstInitSecuritiesCommand>((object)this, false, (Action<FirstInitSecuritiesCommand>)(cmd => this.AddSecurities(cmd.Securities.Take<Security>(2), (IEnumerable<Security>)this.Securities)));
    }

    public override void Dispose(CloseReason reason)
    {
        this.TradesGrid.LayoutChanged -= RaiseChangedCommand;
        base.Dispose(reason);
    }

    private void RaiseSelectedCommand()
    {
        this.TradesGrid.SelectedTrade.SendSelect<ITickTradeMessage>((object)this);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.SetValue<SettingsStorage>("TradesGrid", PersistableHelper.Save((IPersistable)this.TradesGrid));
        storage.SetValue<string>("BarManager", this.BarManager.SaveDevExpressControl());
        this.SaveSubscriptions(storage);
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        PersistableHelper.LoadIfNotNull((IPersistable)this.TradesGrid, storage, "TradesGrid");
        string settings = storage.GetValue<string>("BarManager", (string)null);
        if (settings != null)
            this.BarManager.LoadDevExpressControl(settings);
        this.LoadSubscriptions(storage);
    }

    private void Filter_OnClick(object sender, RoutedEventArgs e) => this.FilterConfig();


}
