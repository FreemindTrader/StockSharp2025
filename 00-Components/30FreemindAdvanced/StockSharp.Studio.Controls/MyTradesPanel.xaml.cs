// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.MyTradesPanel
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

[Display(ResourceType = typeof(LocalizedStrings), Name = "Trades", Description = "MyTradesTable")]
[VectorIcon("Deal")]
[Doc("topics/designer/user_interface/components/trades.html")]
public partial class MyTradesPanel : BaseStudioControl, IComponentConnector
{
    private readonly SubscriptionManager _subscriptionManager;
    
    public MyTradesPanel()
    {
        this.InitializeComponent();
        this._subscriptionManager = new SubscriptionManager((IStudioControl)this);
        this.TradesGrid.LayoutChanged += RaiseChangedCommand;
        this.TradesGrid.SelectionChanged += (GridSelectionChangedEventHandler)((s, e) => this.TradesGrid.SelectedTrade.SendSelect<MyTrade>((object)this));
        this.GotFocus += (RoutedEventHandler)((s, e) => this.TradesGrid.SelectedTrade.SendSelect<MyTrade>((object)this));
        HashSet<long> tradeIds = new HashSet<long>();
        this.Register<EntityCommand<MyTrade>>((object)this, false, (Action<EntityCommand<MyTrade>>)(command =>
        {
            var trade = command.Entity.Trade;
            if (trade.Id.HasValue)
            {
                if (tradeIds.Contains(trade.Id.Value))
                    return;
                tradeIds.Add(trade.Id.Value);
            }
          ((IList<MyTrade>)this.TradesGrid.Trades).TryAddEntities<MyTrade>(command);
        }));
        this.Register<ResetedCommand>((object)this, false, (Action<ResetedCommand>)(cmd =>
        {
            tradeIds.Clear();
            ((ICollection<MyTrade>)this.TradesGrid.Trades).Clear();
        }));
        this.WhenLoaded((Action)(() => this._subscriptionManager.CreateSubscription(DataType.Transactions)));
    }

    public override void Save(SettingsStorage settings)
    {
        base.Save(settings);
        settings.SetValue<SettingsStorage>("TradesGrid", PersistableHelper.Save((IPersistable)this.TradesGrid));
    }

    public override void Load(SettingsStorage settings)
    {
        base.Load(settings);
        PersistableHelper.LoadIfNotNull((IPersistable)this.TradesGrid, settings, "TradesGrid");
    }

    public override void Dispose(CloseReason reason)
    {
        this.TradesGrid.LayoutChanged -= RaiseChangedCommand;
        this._subscriptionManager.Dispose();
        base.Dispose(reason);
    }

    
}
