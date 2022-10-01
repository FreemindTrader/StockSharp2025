// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.DelegateCommand
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using System;

namespace Ecng.Xaml
{
  /// <summary>
  /// </summary>
  public class DelegateCommand : DelegateCommand<object>
  {
    /// <summary>
    /// </summary>
    public DelegateCommand(Action<object> execute, Predicate<object> canExecute = null)
      : base(execute, canExecute)
    {
    }
  }
}
