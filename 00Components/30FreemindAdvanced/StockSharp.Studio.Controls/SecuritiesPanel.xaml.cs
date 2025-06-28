// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.SecuritiesPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using DevExpress.Xpf.Bars;
using Ecng.Collections;
using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Controls.Commands;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;
using DataType = StockSharp.Messages.DataType;

#nullable disable
namespace StockSharp.Studio.Controls;

[Display(ResourceType = typeof(LocalizedStrings), Name = "Securities", Description = "Securities")]
[VectorIcon("Certificate")]
[Doc("topics/terminal/user_interface/components/instruments.html")]
public partial class SecuritiesPanel : BaseStudioControl, IComponentConnector
{
    private static readonly DataType _dataType = DataType.Level1;
    private readonly SubscriptionManager _subscriptionManager;
    private static readonly string[] _defaultColumns = new string[14]
    {
    "LastTick.Price",
    "LastTick.Volume",
    "LastTick.ServerTime",
    "BestBid.Price",
    "BestBid.Volume",
    "BestAsk.Price",
    "BestAsk.Volume",
    "OpenPrice",
    "HighPrice",
    "LowPrice",
    "ClosePrice",
    "Volume",
    "Turnover",
    "ExpiryDate"
    };
    
    public SecuritiesPanel()
    {
        this.InitializeComponent();
        this._subscriptionManager = new SubscriptionManager((IStudioControl)this);
        CollectionHelper.ForEach<string>((IEnumerable<string>)SecuritiesPanel._defaultColumns, (Action<string>)(c => this.SecurityPicker.SetColumnVisibility(c, Visibility.Visible)));
        this.SecurityPicker.GridChanged += RaiseChangedCommand;
        this.Register<BindCommand>((object)this, true, (Action<BindCommand>)(cmd =>
        {
            if (!cmd.CheckControl((IStudioControl)this))
                return;
            this.SecurityPicker.PriceChartDataProvider = StudioServicesRegistry.PriceChartDataProvider;
        }));
        this.Register<EntitiesRemovedCommand<Security>>((object)this, false, (Action<EntitiesRemovedCommand<Security>>)(cmd =>
        {
            bool flag = false;
            foreach (Security entity in cmd.Entities)
            {
                if (this.SecurityPicker.Securities.Remove(entity))
                {
                    this._subscriptionManager.RemoveSubscriptions(entity, SecuritiesPanel._dataType);
                    flag = true;
                }
            }
            if (!flag)
                return;
            this.RaiseChangedCommand();
        }));
        this.Register<FirstInitSecuritiesCommand>((object)this, true, (Action<FirstInitSecuritiesCommand>)(cmd =>
        {
            this.SecurityPicker.Securities.AddRange(cmd.Securities);
            CollectionHelper.ForEach<Security>(cmd.Securities, (Action<Security>)(s => this._subscriptionManager.CreateSubscription(s, SecuritiesPanel._dataType)));
            this.RaiseChangedCommand();
        }));
        this.Register<EntityCommand<Level1ChangeMessage>>((object)this, false, (Action<EntityCommand<Level1ChangeMessage>>)(cmd =>
        {
            Dictionary<Security, Level1ChangeMessage> changes = new Dictionary<Security, Level1ChangeMessage>();
            Security key = BaseStudioControl.SecurityProvider.LookupById(cmd.Entity.SecurityId);
            if (key != null)
            {
                Level1ChangeMessage level1ChangeMessage;
                if (changes.TryGetValue(key, out level1ChangeMessage))
                {
                    foreach (KeyValuePair<Level1Fields, object> change in (IEnumerable<KeyValuePair<Level1Fields, object>>)((BaseChangeMessage<Level1ChangeMessage, Level1Fields>)cmd.Entity).Changes)
                        ((BaseChangeMessage<Level1ChangeMessage, Level1Fields>)level1ChangeMessage).Changes[change.Key] = change.Value;
                }
                else
                    changes.Add(key, cmd.Entity);
            }
            this.SecurityPicker.ProcessLevel1((IDictionary<Security, Level1ChangeMessage>)changes);
        }));
        this.WhenLoaded((Action)(() => new RequestBindSource((IStudioControl)this).SyncProcess((object)this)));
    }

    private void RaiseSelectedCommand()
    {
        this.SecurityPicker.SelectedSecurity.SendSelect<Security>((object)this);
    }

    public override void Dispose(CloseReason reason)
    {
        this._subscriptionManager.Dispose();
        base.Dispose(reason);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.SetValue<SettingsStorage>("SecurityPicker", PersistableHelper.Save((IPersistable)this.SecurityPicker));
        storage.SetValue<string[]>("Securities", this.SecurityPicker.Securities.LookupAll().Select<Security, string>((Func<Security, string>)(s => s.Id)).ToArray<string>());
        storage.SetValue<string>("BarManager", this.BarManager.SaveDevExpressControl());
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        PersistableHelper.LoadIfNotNull((IPersistable)this.SecurityPicker, storage, "SecurityPicker");
        this.SecurityPicker.Securities.Clear();
        foreach (string id in storage.GetValue<string[]>("Securities", Array.Empty<string>()))
        {
            Security security = BaseStudioControl.SecurityProvider.LookupById(id);
            if (security != null)
            {
                this.SecurityPicker.Securities.Add(security);
                this._subscriptionManager.CreateSubscription(security, SecuritiesPanel._dataType);
            }
        }
        string settings = storage.GetValue<string>("BarManager", (string)null);
        if (settings == null)
            return;
        this.BarManager.LoadDevExpressControl(settings);
    }

    private void SecurityPicker_SecuritySelected(Security security)
    {
        this.RaiseSelectedCommand();
        this.RemoveSecurities.IsEnabled = this.SecurityPicker.SelectedSecurity != null;
    }

    private void AddSecurities_OnClick(object sender, ItemClickEventArgs e)
    {
        IEnumerable<Security> securities = this.SecurityPicker.Securities.LookupAll();
        SecuritiesWindowEx wnd = new SecuritiesWindowEx()
        {
            SecurityProvider = BaseStudioControl.SecurityProvider,
            SelectedSecurities = securities
        };
        if (!wnd.ShowModal((DependencyObject)this))
            return;
        Security[] array1 = securities.Except<Security>(wnd.SelectedSecurities).ToArray<Security>();
        Security[] array2 = wnd.SelectedSecurities.Except<Security>(securities).ToArray<Security>();
        if (array1.Length == 0 && array2.Length == 0)
            return;
        this.SecurityPicker.Securities.RemoveRange((IEnumerable<Security>)array1);
        this.SecurityPicker.Securities.AddRange((IEnumerable<Security>)array2);
        CollectionHelper.ForEach<Security>((IEnumerable<Security>)array1, (Action<Security>)(s => this._subscriptionManager.RemoveSubscriptions(s, SecuritiesPanel._dataType)));
        CollectionHelper.ForEach<Security>((IEnumerable<Security>)array2, (Action<Security>)(s => this._subscriptionManager.CreateSubscription(s, SecuritiesPanel._dataType)));
        this.RaiseChangedCommand();
    }

    private void RemoveSecurities_OnClick(object sender, ItemClickEventArgs e)
    {
        Security[] array = this.SecurityPicker.SelectedSecurities.ToArray<Security>();
        foreach (Security security in array)
            this._subscriptionManager.RemoveSubscriptions(security, SecuritiesPanel._dataType);
        this.SecurityPicker.Securities.RemoveRange((IEnumerable<Security>)array);
        this.RaiseChangedCommand();
    }

    
}
