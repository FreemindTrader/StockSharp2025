// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.Commands.BindCommand
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using StockSharp.Studio.Core.Commands;

#nullable disable
namespace StockSharp.Studio.Controls.Commands;

public class BindCommand(IStrategyBinder binder, IStudioControl control) : BaseStudioCommand
{
    public IStrategyBinder Binder { get; } = binder ?? throw new ArgumentNullException(nameof(binder));

    public IStudioControl Control { get; } = control;

    public bool IsInteractive { get; set; }

    public bool CheckControl(IStudioControl control)
    {
        return this.Control == null || this.Control == control;
    }
}
