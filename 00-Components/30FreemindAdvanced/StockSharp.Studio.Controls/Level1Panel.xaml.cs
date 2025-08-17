// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.Level1Panel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Markup;
using DevExpress.Xpf.Bars;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;
using DataType = StockSharp.Messages.DataType;

#nullable disable
namespace StockSharp.Studio.Controls;

[Display(ResourceType = typeof(LocalizedStrings), Name = "Level1", Description = "Level1Panel")]
[VectorIcon("Table")]
[Doc("topics/terminal/user_interface/components/level_1.html")]
public partial class Level1Panel :
  BaseSubscriptionStudioControl,
  IBarManagerControl,
  IComponentConnector
{

    BarManager IBarManagerControl.Bar => this.BarManager;

    protected override DataType DataType { get; } = DataType.Level1;

    public Level1Panel()
    {
        this.InitializeComponent();
        this.TryHideBar();
        this.Level1Grid.LayoutChanged += RaiseChangedCommand;
        this.Register<ResetedCommand>((object)this, false, (Action<ResetedCommand>)(cmd => ((ICollection<Level1ChangeMessage>)this.Level1Grid.Messages).Clear()));
        this.Register<EntityCommand<Level1ChangeMessage>>((object)this, false, new Action<EntityCommand<Level1ChangeMessage>>((this.Level1Grid.Messages).AddEntity<Level1ChangeMessage>));
    }

    public override void Dispose(CloseReason reason)
    {
        this.Level1Grid.LayoutChanged -= RaiseChangedCommand;
        base.Dispose(reason);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.SetValue<SettingsStorage>("Level1Grid", PersistableHelper.Save((IPersistable)this.Level1Grid));
        storage.SetValue<string>("BarManager", this.BarManager.SaveDevExpressControl());
        this.SaveSubscriptions(storage);
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        PersistableHelper.LoadIfNotNull((IPersistable)this.Level1Grid, storage, "Level1Grid");
        string settings = storage.GetValue<string>("BarManager", (string)null);
        if (settings != null)
            this.BarManager.LoadDevExpressControl(settings);
        this.LoadSubscriptions(storage);
    }

    private void Filter_OnClick(object sender, RoutedEventArgs e) => this.FilterConfig();


}
