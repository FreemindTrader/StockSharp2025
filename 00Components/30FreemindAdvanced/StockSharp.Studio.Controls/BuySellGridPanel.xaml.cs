// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.BuySellGridPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Markup;
using Ecng.Collections;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;
using DataType = StockSharp.Messages.DataType;

#nullable disable
namespace StockSharp.Studio.Controls;

[Display(ResourceType = typeof(LocalizedStrings), Name = "BuySell", Description = "BuySellPanel")]
[VectorIcon("Order")]
[Doc("topics/terminal/user_interface/components/buy_sell.html")]
public partial class BuySellGridPanel : BaseStudioControl, IComponentConnector
{
    private readonly SubscriptionManager _subscriptionManager;

    public BuySellGridPanel()
    {
        this.InitializeComponent();
        this._subscriptionManager = new SubscriptionManager((IStudioControl)this);
        Portfolio defaultPortfolio = (Portfolio)null;
        this.Register<ResetedCommand>((object)this, false, (Action<ResetedCommand>)(cmd => this.UnderlyingGrid.ClearPanels()));
        this.Register<EntitiesRemovedCommand<Security>>((object)this, false, (Action<EntitiesRemovedCommand<Security>>)(cmd =>
        {
            bool flag = false;
            foreach (Security entity in cmd.Entities)
            {
                Security security = entity;
                StockSharp.Xaml.BuySellPanel buySellPanel = this.UnderlyingGrid.Panels.FirstOrDefault<StockSharp.Xaml.BuySellPanel>((Func<StockSharp.Xaml.BuySellPanel, bool>)(p => p.Security == security));
                if (buySellPanel != null)
                {
                    buySellPanel.Security = (Security)null;
                    flag = true;
                }
            }
            if (!flag)
                return;
            this.RaiseChangedCommand();
        }));
        this.Register<FirstInitSecuritiesCommand>((object)this, true, (Action<FirstInitSecuritiesCommand>)(cmd =>
        {
            foreach (Security security in cmd.Securities.Take<Security>(4))
                this.UnderlyingGrid.AddPanel(security).Portfolio = defaultPortfolio;
        }));
        this.Register<FirstInitPortfoliosCommand>((object)this, true, (Action<FirstInitPortfoliosCommand>)(cmd =>
        {
            Portfolio portfolio = cmd.Portfolios.First<Portfolio>();
            if (CollectionHelper.IsEmpty<StockSharp.Xaml.BuySellPanel>(this.UnderlyingGrid.Panels))
                defaultPortfolio = portfolio;
            else
                CollectionHelper.ForEach<StockSharp.Xaml.BuySellPanel>(this.UnderlyingGrid.Panels, (Action<StockSharp.Xaml.BuySellPanel>)(p => p.Portfolio = portfolio));
        }));
        this.UnderlyingGrid.SecurityProvider = BaseStudioControl.SecurityProvider;
        this.UnderlyingGrid.MarketDataProvider = BaseStudioControl.MarketDataProvider;
        this.UnderlyingGrid.Portfolios = BaseStudioControl.PortfolioDataSource;
        this.UnderlyingGrid.OrderRegistering += new Action<Security, Portfolio, Sides, Decimal, Decimal>(this.UnderlyingGridOnOrderRegistering);
    }

    public override void Dispose(CloseReason reason)
    {
        this.UnderlyingGrid.OrderRegistering -= new Action<Security, Portfolio, Sides, Decimal, Decimal>(this.UnderlyingGridOnOrderRegistering);
        this._subscriptionManager.Dispose();
        base.Dispose(reason);
    }

    private void UnderlyingGridOnOrderRegistering(
      Security security,
      Portfolio portfolio,
      Sides side,
      Decimal price,
      Decimal volume)
    {
        new RegisterOrderCommand(new Order()
        {
            Security = security,
            Portfolio = portfolio,
            Side = side,
            Price = price,
            Volume = volume
        }).Process((object)this);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.SetValue<SettingsStorage>("UnderlyingGrid", PersistableHelper.Save((IPersistable)this.UnderlyingGrid));
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        PersistableHelper.LoadIfNotNull((IPersistable)this.UnderlyingGrid, storage, "UnderlyingGrid");
    }

    private void UnderlyingGrid_OnPanelAdded(StockSharp.Xaml.BuySellPanel panel)
    {
        if (panel.Security != null)
            this.RequestMarketData(panel.Security);
        panel.SecurityChanged += new Action<Security, Security>(this.PanelOnSecurityChanged);
        panel.PortfolioChanged += new Action<Portfolio, Portfolio>(this.PanelOnPortfolioChanged);
        this.RaiseChangedCommand();
    }

    private void UnderlyingGrid_OnPanelRemoved(StockSharp.Xaml.BuySellPanel panel)
    {
        if (panel.Security != null)
            this.RefuseMarketData(panel.Security);
        panel.SecurityChanged -= new Action<Security, Security>(this.PanelOnSecurityChanged);
        panel.PortfolioChanged -= new Action<Portfolio, Portfolio>(this.PanelOnPortfolioChanged);
        this.RaiseChangedCommand();
    }

    private void PanelOnSecurityChanged(Security oldSecurity, Security newSecurity)
    {
        if (oldSecurity != null)
            this.RefuseMarketData(oldSecurity);
        if (newSecurity != null)
            this.RequestMarketData(newSecurity);
        this.RaiseChangedCommand();
    }

    private void PanelOnPortfolioChanged(Portfolio oldPortfolio, Portfolio newPortfolio)
    {
        this.RaiseChangedCommand();
    }

    private void RequestMarketData(Security security)
    {
        this._subscriptionManager.CreateSubscription(security, DataType.Level1);
        this._subscriptionManager.CreateSubscription(security, DataType.MarketDepth);
    }

    private void RefuseMarketData(Security security)
    {
        this._subscriptionManager.RemoveSubscriptions(security);
    }


}
