// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.ExchangeEditorPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Windows.Markup;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Localization;
using StockSharp.Xaml;

#nullable disable
namespace StockSharp.Studio.Controls;

[Display(ResourceType = typeof(LocalizedStrings), Name = "Boards", Description = "ExchangeEditorPanel")]
[Guid("66f50d15-6eaa-4692-ba88-12bee0b6e6e5")]
[Doc("topics/designer/user_interface/boards.html")]
[VectorIcon("Bank")]
public partial class ExchangeEditorPanel : BaseStudioControl, IComponentConnector
{

    public ExchangeEditorPanel()
    {
        this.InitializeComponent();
        this.Panel.Changed += RaiseChangedCommand;
    }

    public override void Save(SettingsStorage storage)
    {
        this.Panel.Save(storage);
        base.Save(storage);
    }

    public override void Load(SettingsStorage storage)
    {
        PersistableHelper.LoadIfNotNull((IPersistable)this.Panel, storage);
        base.Load(storage);
    }


}
