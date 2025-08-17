// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.Level2Panel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using Ecng.Collections;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Xaml;
using StockSharp.Xaml.PropertyGrid;
using DataType = StockSharp.Messages.DataType;

#nullable disable
namespace StockSharp.Studio.Controls;

[Display(ResourceType = typeof(LocalizedStrings), Name = "Level2", Description = "Level2Panel")]
[VectorIcon("Table")]
public partial class Level2Panel : BaseStudioControl, IComponentConnector
{
    private static readonly DataType _dataType = DataType.Level1;
    private readonly SubscriptionManager _subscriptionManager;
    private readonly HashSet<string> _askDefaultColumns = new HashSet<string>()
  {
    "Board",
    "BestAsk.Price",
    "BestAsk.Volume"
  };
    private readonly HashSet<string> _bidDefaultColumns = new HashSet<string>()
  {
    "Board",
    "BestBid.Price",
    "BestBid.Volume"
  };
    private Security[] _securities;
    
    private BuySellSettings Settings => this.BuySellPanel.Settings;

    public Level2Panel()
    {
        this.InitializeComponent();
        this._subscriptionManager = new SubscriptionManager((IStudioControl)this);
        this.BuySellPanel.Host = (IStudioControl)this;
        this.SecurityAsksGrid.LayoutChanged += RaiseChangedCommand;
        this.SecurityAsksGrid.SelectionChanged += new GridSelectionChangedEventHandler(this.SecuritiesCtrlOnSelectionChanged);
        this.SecurityBidsGrid.LayoutChanged += RaiseChangedCommand;
        this.SecurityBidsGrid.SelectionChanged += new GridSelectionChangedEventHandler(this.SecuritiesCtrlOnSelectionChanged);
        this.SecurityPicker.EditValueChanged += new EditValueChangedEventHandler(this.SecurityPickerSecuritySelected);
        this.SecurityPicker.SecurityProvider = BaseStudioControl.SecurityProvider;
        this.SecurityAsksGrid.MarketDataProvider = this.SecurityBidsGrid.MarketDataProvider = BaseStudioControl.MarketDataProvider;
        Level2Panel.SetVisibleColumns((GridControl)this.SecurityAsksGrid, (ISet<string>)this._askDefaultColumns);
        Level2Panel.SetVisibleColumns((GridControl)this.SecurityBidsGrid, (ISet<string>)this._bidDefaultColumns);
    }

    private static void SetVisibleColumns(GridControl grid, ISet<string> columns)
    {
        foreach (GridColumn column in (Collection<GridColumn>)grid.Columns)
            column.Visible = columns.Contains(column.FieldName);
    }

    private void SecurityPickerSecuritySelected(object sender, EditValueChangedEventArgs e)
    {
        Security[] array = BaseStudioControl.SecurityProvider.LookupByCode(this.SecurityPicker.SelectedSecurity.Code).ToArray<Security>();
        Security[] securities = this._securities;
        if (securities != null)
            CollectionHelper.ForEach<Security>((IEnumerable<Security>)securities, (Action<Security>)(s => this._subscriptionManager.RemoveSubscriptions(s, Level2Panel._dataType)));
        ((ICollection<Security>)this.SecurityAsksGrid.Securities).Clear();
        ((ICollection<Security>)this.SecurityBidsGrid.Securities).Clear();
        this._securities = array;
        if (this._securities == null)
            return;
        CollectionHelper.ForEach<Security>((IEnumerable<Security>)this._securities, (Action<Security>)(s => this._subscriptionManager.CreateSubscription(s, Level2Panel._dataType)));
        ((ICollectionEx<Security>)this.SecurityAsksGrid.Securities).AddRange((IEnumerable<Security>)this._securities);
        ((ICollectionEx<Security>)this.SecurityBidsGrid.Securities).AddRange((IEnumerable<Security>)this._securities);
    }

    private void SecuritiesCtrlOnSelectionChanged(object sender, GridSelectionChangedEventArgs e)
    {
        this.Settings.Security = ((SecurityGrid)sender).SelectedSecurity;
    }

    public override void Load(SettingsStorage storage)
    {
        string id = storage.GetValue<string>("SecurityId", (string)null);
        if (id != null)
            this.SecurityPicker.SelectedSecurity = BaseStudioControl.SecurityProvider.LookupById(id);
        PersistableHelper.LoadIfNotNull((IPersistable)this.SecurityAsksGrid, storage, "SecurityAsksGrid");
        PersistableHelper.LoadIfNotNull((IPersistable)this.SecurityBidsGrid, storage, "SecurityBidsGrid");
        PersistableHelper.LoadIfNotNull((IPersistable)this.BuySellPanel, storage, "BuySellSettings");
    }

    public override void Save(SettingsStorage storage)
    {
        storage.SetValue<string>("SecurityId", this.SecurityPicker.SelectedSecurity?.Id);
        storage.SetValue<SettingsStorage>("SecurityAsksGrid", PersistableHelper.Save((IPersistable)this.SecurityAsksGrid));
        storage.SetValue<SettingsStorage>("SecurityBidsGrid", PersistableHelper.Save((IPersistable)this.SecurityBidsGrid));
        storage.SetValue<SettingsStorage>("BuySellSettings", PersistableHelper.Save((IPersistable)this.BuySellPanel));
    }

    public override void Dispose(CloseReason reason)
    {
        this.SecurityAsksGrid.LayoutChanged -= RaiseChangedCommand;
        this.SecurityBidsGrid.LayoutChanged -= RaiseChangedCommand;
        this._subscriptionManager.Dispose();
        base.Dispose(reason);
    }

    
}
