// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.OptionDeskButton
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.Windows.Markup;
using System.Windows.Media;
using DevExpress.Xpf.Core;
using Ecng.Serialization;
using StockSharp.Studio.Controls.Commands;
using StockSharp.Studio.Core.Commands;

#nullable disable
namespace StockSharp.Studio.Controls;

public partial class OptionDeskButton :
  SimpleButton,
  IStudioControl,
  IPersistable,
  IDisposable,
  IComponentConnector
{


    public OptionDeskButton() => this.InitializeComponent();

    protected override void OnClick()
    {
        new OpenWindowCommand(typeof(OptionDeskPanel), false).Process((object)this);
        base.OnClick();
    }

    void IPersistable.Load(SettingsStorage storage)
    {
    }

    void IPersistable.Save(SettingsStorage storage)
    {
    }

    void IDisposable.Dispose() => GC.SuppressFinalize((object)this);

    void IStudioControl.FirstTimeInit()
    {
    }

    void IStudioControl.SendCommand(IStudioCommand command)
    {
    }

    string IStudioControl.Title
    {
        get => (string)this.ToolTip;
        set
        {
        }
    }

    ImageSource IStudioControl.Icon => (ImageSource)null;

    string IStudioControl.DocUrl => (string)null;

    public string Key { get; set; }

    bool IStudioControl.SaveWithLayout => true;

    bool IStudioControl.IsTitleEditable => false;


}
