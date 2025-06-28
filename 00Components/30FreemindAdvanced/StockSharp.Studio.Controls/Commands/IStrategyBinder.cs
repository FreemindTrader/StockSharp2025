// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.Commands.IStrategyBinder
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using StockSharp.Algo.Strategies;

#nullable disable
namespace StockSharp.Studio.Controls.Commands;

public interface IStrategyBinder
{
    string BinderTitle { get; }

    object Settings { get; }

    event Action<object> SettingsChanged;

    void Init(Action<Strategy> bind, Action<Strategy> unbind);
}
