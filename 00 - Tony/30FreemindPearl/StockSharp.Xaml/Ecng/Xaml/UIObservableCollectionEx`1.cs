// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.UIObservableCollectionEx`1
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using Ecng.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Data;

namespace Ecng.Xaml
{
  /// <summary>
  /// </summary>
  public class UIObservableCollectionEx<TItem> : ObservableCollectionEx<TItem>
  {
    /// <summary>
    /// </summary>
    protected override void ProcessCollectionChanged(
      IEnumerable<NotifyCollectionChangedEventHandler> subscribers,
      NotifyCollectionChangedAction action,
      IList<TItem> items,
      int index)
    {
      NotifyCollectionChangedEventArgs e = new NotifyCollectionChangedEventArgs(action, (IList) items, index);
      foreach (NotifyCollectionChangedEventHandler subscriber in subscribers)
      {
        CollectionView target = subscriber.Target as CollectionView;
        if (target != null)
        {
          if (items.Count > 10)
          {
            target.Refresh();
          }
          else
          {
            int index1 = index;
            foreach (TItem obj in (IEnumerable<TItem>) items)
            {
              subscriber((object) this, new NotifyCollectionChangedEventArgs(action, (object) obj, index1));
              if (action == NotifyCollectionChangedAction.Add)
                ++index1;
            }
          }
        }
        else
          subscriber((object) this, e);
      }
    }
  }
}
