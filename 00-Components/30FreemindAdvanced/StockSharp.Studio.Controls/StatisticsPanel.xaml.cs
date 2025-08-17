// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.StatisticsPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Algo.Statistics;
using StockSharp.Algo.Strategies;
using StockSharp.Localization;
using StockSharp.Studio.Controls.Commands;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;

#nullable disable
namespace StockSharp.Studio.Controls;

[Display(ResourceType = typeof(LocalizedStrings), Name = "Statistics", Description = "StatisticsPanel")]
[VectorIcon("Clipboard2")]
[Doc("topics/designer/user_interface/components/statistics.html")]
public partial class StatisticsPanel : BaseStudioControl, IComponentConnector
{
    
    public StatisticsPanel()
    {
        this.InitializeComponent();
        this.Register<BindCommand>((object)this, true, (Action<BindCommand>)(cmd =>
        {
            if (!cmd.CheckControl((IStudioControl)this))
                return;
            cmd.Binder.Init((Action<Strategy>)(s => this.StatisticsGrid.StatisticManager = s.StatisticManager), (Action<Strategy>)(s => this.StatisticsGrid.StatisticManager = (IStatisticManager)null));
        }));
        this.Register<ResetedCommand>((object)this, true, (Action<ResetedCommand>)(cmd => this.StatisticsGrid.Reset()));
        this.WhenLoaded((Action)(() => new RequestBindSource((IStudioControl)this).SyncProcess((object)this)));
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.SetValue<SettingsStorage>("StatisticsGrid", PersistableHelper.Save((IPersistable)this.StatisticsGrid));
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        PersistableHelper.LoadIfNotNull((IPersistable)this.StatisticsGrid, storage, "StatisticsGrid");
    }

    
}
