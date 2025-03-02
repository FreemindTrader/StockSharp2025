// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.Commands.IStrategyBinder
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using StockSharp.Algo.Strategies;
using System;

namespace StockSharp.Studio.Controls.Commands
{
    public interface IStrategyBinder
    {
        string BinderTitle { get; }

        object Settings { get; }

        event Action<object> SettingsChanged;

        void Init( Action<Strategy> bind, Action<Strategy> unbind );
    }
}
