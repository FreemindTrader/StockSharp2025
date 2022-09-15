// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.UIDispatcher
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using Ecng.ComponentModel;
using System;
using System.Diagnostics;
using System.Windows.Threading;

namespace Ecng.Xaml
{
  /// <summary>
  /// </summary>
  public class UIDispatcher : IDispatcher
  {
    
    private readonly Dispatcher \u0023\u003DzKM9i\u0024aQ\u003D;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ecng.Xaml.UIDispatcher" />.
    /// </summary>
    /// <param name="dispatcher">
    ///   <see cref="T:System.Windows.Threading.Dispatcher" />
    /// </param>
    public UIDispatcher(Dispatcher dispatcher)
    {
      Dispatcher dispatcher1 = dispatcher;
      if (dispatcher1 == null)
        throw new ArgumentNullException(nameof(2127278630));
      this.\u0023\u003DzKM9i\u0024aQ\u003D = dispatcher1;
    }

    bool IDispatcher.\u0023\u003Dz0EztFcHaE9LtsypphGecHgQDCsS6()
    {
      return this.\u0023\u003DzKM9i\u0024aQ\u003D.CheckAccess();
    }

    void IDispatcher.\u0023\u003DzBIUAQgeeuVoldv0lZLut9xg\u003D(Action _param1)
    {
      this.\u0023\u003DzKM9i\u0024aQ\u003D.Invoke(_param1);
    }

    void IDispatcher.\u0023\u003Dz14fNg9rfyduZuGwlUNGhzn8\u003D(Action _param1)
    {
      this.\u0023\u003DzKM9i\u0024aQ\u003D.InvokeAsync(_param1);
    }
  }
}
