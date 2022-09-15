// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.ThreadSafeObservableCollection`1
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using Ecng.Collections;
using Ecng.ComponentModel;
using Ecng.Configuration;

namespace Ecng.Xaml
{
  /// <summary>
  /// </summary>
  public class ThreadSafeObservableCollection<TItem> : DispatcherObservableCollection<TItem>
  {
    /// <summary>
    /// </summary>
    public ThreadSafeObservableCollection(IListEx<TItem> items)
      : this(ConfigManager.GetService<IDispatcher>() ?? (IDispatcher) GuiDispatcher.GlobalDispatcher, items)
    {
    }

    /// <summary>
    /// </summary>
    public ThreadSafeObservableCollection(IDispatcher dispatcher, IListEx<TItem> items)
      : base(dispatcher, items)
    {
    }
  }
}
