// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.PositionsPanel
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
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;
using StockSharp.Xaml.GridControl;
using DataType = StockSharp.Messages.DataType;

#nullable disable
namespace StockSharp.Studio.Controls;

[Display(ResourceType = typeof(LocalizedStrings), Name = "Positions", Description = "PositionsPanel")]
[Guid("D2E3A2B3-3C34-4973-A973-28D0722B1005")]
[VectorIcon("Money")]
public partial class PositionsPanel : BaseStudioControl, IComponentConnector
{
    public static readonly RoutedCommand ClosePositionCommand = new RoutedCommand();
    public static readonly RoutedCommand RevertPositionCommand = new RoutedCommand();
    private readonly SubscriptionManager _subscriptionManager;
    
    public PositionsPanel()
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
        this.WhenLoaded((Action)(() => this._subscriptionManager.CreateSubscription(DataType.PositionChanges)));
        this.PortfolioGrid.PopupMenu.Items.Add((IBarItem)new BarItemSeparator());
        CommonBarItemCollection items1 = this.PortfolioGrid.PopupMenu.Items;
        BarButtonItem barButtonItem1 = new BarButtonItem();
        barButtonItem1.Glyph = ThemedIconsExtension.GetImage("Remove2");
        barButtonItem1.Content = (object)LocalizedStrings.ClosePosition;
        barButtonItem1.Command = (ICommand)PositionsPanel.ClosePositionCommand;
        barButtonItem1.CommandTarget = (IInputElement)this;
        items1.Add((IBarItem)barButtonItem1);
        CommonBarItemCollection items2 = this.PortfolioGrid.PopupMenu.Items;
        BarButtonItem barButtonItem2 = new BarButtonItem();
        barButtonItem2.Glyph = ThemedIconsExtension.GetImage("Refresh");
        barButtonItem2.Content = (object)LocalizedStrings.RevertPosition;
        barButtonItem2.Command = (ICommand)PositionsPanel.RevertPositionCommand;
        barButtonItem2.CommandTarget = (IInputElement)this;
        items2.Add((IBarItem)barButtonItem2);
    }

    private bool CanEditPosition { get; set; }

    private StockSharp.BusinessEntities.Position SelectedPosition
    {
        get => this.PortfolioGrid?.SelectedPosition;
    }

    public override void Save(SettingsStorage settings)
    {
        settings.SetValue<SettingsStorage>("PortfolioGrid", PersistableHelper.Save((IPersistable)this.PortfolioGrid));
    }

    public override void Load(SettingsStorage settings)
    {
        PersistableHelper.LoadIfNotNull((IPersistable)this.PortfolioGrid, settings, "PortfolioGrid");
    }

    public override void Dispose(CloseReason reason)
    {
        this.PortfolioGrid.LayoutChanged -= RaiseChangedCommand;
        this._subscriptionManager.Dispose();
        base.Dispose(reason);
    }

    private void ExecutedClosePositionCommand(object sender, ExecutedRoutedEventArgs e)
    {
        new StockSharp.Studio.Core.Commands.ClosePositionCommand(this.SelectedPosition).Process((object)this);
    }

    private void CanExecuteClosePositionCommand(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = this.SelectedPosition.CanCloseOrRevert();
    }

    private void ExecutedRevertPositionCommand(object sender, ExecutedRoutedEventArgs e)
    {
        new StockSharp.Studio.Core.Commands.RevertPositionCommand(this.SelectedPosition).Process((object)this);
    }

    private void CanExecuteRevertPositionCommand(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = this.SelectedPosition.CanCloseOrRevert();
    }

    
}
