// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.PortfoliosPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Grid;
using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;
using StockSharp.Xaml.GridControl;
using DataType = StockSharp.Messages.DataType;

#nullable disable
namespace StockSharp.Studio.Controls;

[Display(ResourceType = typeof(LocalizedStrings), Name = "Portfolios", Description = "PortfoliosPanel")]
[Guid("AC608491-B1FE-4A80-8707-25F3CF263302")]
[VectorIcon("Portfolio")]
[Doc("topics/designer/user_interface/portfolios.html")]
public partial class PortfoliosPanel : BaseStudioControl, IComponentConnector
{
    public static readonly DependencyProperty ShowToolBarProperty = DependencyProperty.Register(nameof(ShowToolBar), typeof(bool), typeof(PortfoliosPanel), new PropertyMetadata((object)true));
    private readonly SubscriptionManager _subscriptionManager;
    
    public bool ShowToolBar
    {
        get => (bool)this.GetValue(PortfoliosPanel.ShowToolBarProperty);
        set => this.SetValue(PortfoliosPanel.ShowToolBarProperty, (object)value);
    }

    public PortfoliosPanel()
    {
        this.InitializeComponent();
        this._subscriptionManager = new SubscriptionManager((IStudioControl)this);
        if (this.IsDesignMode())
            return;
        this.PortfolioGrid.LayoutChanged += RaiseChangedCommand;
        this.PortfolioGrid.SelectionChanged += (GridSelectionChangedEventHandler)((s, e) => this.SelectedPosition.SendSelect<StockSharp.BusinessEntities.Position>((object)this, this.CanEditPosition));
        this.GotFocus += (RoutedEventHandler)((s, e) => this.SelectedPosition.SendSelect<StockSharp.BusinessEntities.Position>((object)this, this.CanEditPosition));
        this.PortfolioGrid.ItemDoubleClick += (Action<object, ItemDoubleClickEventArgs>)((sender, e) =>
        {
            if (this.SelectedPosition == null)
                return;
            new PositionEditCommand(this.SelectedPosition).Process((object)this);
        });
        this.Register<EntityCommand<StockSharp.BusinessEntities.Position>>((object)this, false, new Action<EntityCommand<StockSharp.BusinessEntities.Position>>((this.PortfolioGrid.Positions).TryAddEntity<StockSharp.BusinessEntities.Position>));
        this.Register<ResetedCommand>((object)this, false, (Action<ResetedCommand>)(cmd => ((ICollection<StockSharp.BusinessEntities.Position>)this.PortfolioGrid.Positions).Clear()));
        this.PortfolioGrid.PopupMenu.Items.Add((IBarItem)new BarItemSeparator());
        CommonBarItemCollection items1 = this.PortfolioGrid.PopupMenu.Items;
        BarButtonItem barButtonItem1 = new BarButtonItem();
        barButtonItem1.Glyph = ThemedIconsExtension.GetImage("Remove2");
        barButtonItem1.Content = (object)LocalizedStrings.ClosePosition;
        barButtonItem1.Command = (ICommand)new DelegateCommand(new Action<object>(this.ClosePositionCommand_Executed), new Func<object, bool>(this.ClosePositionCommand_CanExecute));
        barButtonItem1.CommandTarget = (IInputElement)this;
        items1.Add((IBarItem)barButtonItem1);
        CommonBarItemCollection items2 = this.PortfolioGrid.PopupMenu.Items;
        BarButtonItem barButtonItem2 = new BarButtonItem();
        barButtonItem2.Glyph = ThemedIconsExtension.GetImage("Refresh");
        barButtonItem2.Content = (object)LocalizedStrings.RevertPosition;
        barButtonItem2.Command = (ICommand)new DelegateCommand(new Action<object>(this.RevertPositionCommand_Executed), new Func<object, bool>(this.RevertPositionCommand_CanExecute));
        barButtonItem2.CommandTarget = (IInputElement)this;
        items2.Add((IBarItem)barButtonItem2);
        CommonBarItemCollection items3 = this.PortfolioGrid.PopupMenu.Items;
        BarButtonItem barButtonItem3 = new BarButtonItem();
        barButtonItem3.Glyph = ThemedIconsExtension.GetImage("Money");
        barButtonItem3.Content = (object)LocalizedStrings.Withdraw;
        barButtonItem3.Command = (ICommand)new DelegateCommand(new Action<object>(this.WithdrawCommand_Executed), new Func<object, bool>(this.WithdrawCommand_CanExecute));
        barButtonItem3.CommandTarget = (IInputElement)this;
        items3.Add((IBarItem)barButtonItem3);
        this.WhenLoaded((Action)(() => this._subscriptionManager.CreateSubscription(DataType.PositionChanges)));
    }

    private bool CanEditPosition { get; set; }

    private StockSharp.BusinessEntities.Position SelectedPosition
    {
        get => this.PortfolioGrid?.SelectedPosition;
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.SetValue<SettingsStorage>("PortfolioGrid", PersistableHelper.Save((IPersistable)this.PortfolioGrid));
        storage.SetValue<string>("BarManager", this.BarManager.SaveDevExpressControl());
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        if (!PersistableHelper.LoadIfNotNull((IPersistable)this.PortfolioGrid, storage, "PortfolioGrid"))
        {
            SettingsStorage settingsStorage = storage.GetValue<SettingsStorage>("PositionsPanel", (SettingsStorage)null);
            if (settingsStorage != null)
                PersistableHelper.LoadIfNotNull((IPersistable)this.PortfolioGrid, settingsStorage, "PortfolioGrid");
        }
        string settings = storage.GetValue<string>("BarManager", (string)null);
        if (settings == null)
            return;
        this.BarManager.LoadDevExpressControl(settings);
    }

    public override void Dispose(CloseReason reason)
    {
        this.PortfolioGrid.LayoutChanged -= RaiseChangedCommand;
        this._subscriptionManager.Dispose();
        base.Dispose(reason);
    }

    private void CreatePortfolio_OnClick(object sender, RoutedEventArgs e)
    {
        new PositionEditCommand((StockSharp.BusinessEntities.Position)new Portfolio()).Process((object)this);
    }

    private void CreatePosition_OnClick(object sender, RoutedEventArgs e)
    {
        new PositionEditCommand(new StockSharp.BusinessEntities.Position()).Process((object)this);
    }

    private void ClosePositionCommand_Executed(object sender)
    {
        new ClosePositionCommand(this.SelectedPosition).Process((object)this);
    }

    private bool ClosePositionCommand_CanExecute(object sender)
    {
        return this.SelectedPosition.CanCloseOrRevert();
    }

    private void RevertPositionCommand_Executed(object sender)
    {
        new RevertPositionCommand(this.SelectedPosition).Process((object)this);
    }

    private bool RevertPositionCommand_CanExecute(object sender)
    {
        return this.SelectedPosition.CanCloseOrRevert();
    }

    private void WithdrawCommand_Executed(object sender)
    {
        StockSharp.BusinessEntities.Position selectedPosition = this.SelectedPosition;
        IMessageAdapter adapter = BaseStudioControl.PortfolioMessageAdapterProvider.TryGetAdapter(ServicesRegistry.AdapterProvider, selectedPosition.Portfolio);
        WithdrawWindow wnd = new WithdrawWindow()
        {
            VolumeStep = selectedPosition.Security.VolumeStep ?? 0.000001M
        };
        if (!wnd.ShowModal((DependencyObject)this))
            return;
        Decimal? volume = wnd.Volume;
        if (!volume.HasValue)
            return;
        OrderCondition orderCondition = StockSharp.Messages.Extensions.CreateOrderCondition(adapter);
        if (!(orderCondition is IWithdrawOrderCondition iwithdrawOrderCondition))
            return;
        iwithdrawOrderCondition.IsWithdraw = true;
        iwithdrawOrderCondition.WithdrawInfo = wnd.WithdrawInfo;
        Order order = new Order();
        order.Portfolio = selectedPosition.Portfolio;
        order.Security = selectedPosition.Security;
        order.Type = new OrderTypes?((OrderTypes)2);
        order.Condition = orderCondition;
        volume = wnd.Volume;
        order.Volume = volume.Value;
        new RegisterOrderCommand(order).Process((object)this);
    }

    private bool WithdrawCommand_CanExecute(object sender)
    {
        StockSharp.BusinessEntities.Position selectedPosition = this.SelectedPosition;
        if (selectedPosition.CanCloseOrRevert())
        {
            Decimal? currentValue = selectedPosition.CurrentValue;
            Decimal num = 0M;
            if (currentValue.GetValueOrDefault() > num & currentValue.HasValue)
            {
                IMessageAdapter adapter = BaseStudioControl.PortfolioMessageAdapterProvider.TryGetAdapter(ServicesRegistry.AdapterProvider, selectedPosition.Portfolio);
                return adapter != null && StockSharp.Messages.Extensions.IsSupportWithdraw(adapter);
            }
        }
        return false;
    }

    
}
