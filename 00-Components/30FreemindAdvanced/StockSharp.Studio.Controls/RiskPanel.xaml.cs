// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.RiskPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Algo;
using StockSharp.Localization;

#nullable disable
namespace StockSharp.Studio.Controls;

[Display(ResourceType = typeof(LocalizedStrings), Name = "Risks", Description = "RiskSettings")]
[Guid("CBF49990-CC8B-49BE-8FD8-713CE6F98297")]
[VectorIcon("Dices")]
[Doc("topics/designer/user_interface/risk_management.html")]
public partial class RiskPanel : BaseStudioControl, IComponentConnector
{
    
    public RiskPanel()
    {
        this.InitializeComponent();
        this.RiskPanelCtrl.Rules = ServicesRegistry.RiskManager.Rules;
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.SetValue<SettingsStorage>("RiskPanelCtrl", PersistableHelper.Save((IPersistable)this.RiskPanelCtrl));
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        PersistableHelper.LoadIfNotNull((IPersistable)this.RiskPanelCtrl, storage, "RiskPanelCtrl");
    }

    private void RiskPanelCtrl_OnLayoutChanged() => this.RaiseChangedCommand();

    
}
