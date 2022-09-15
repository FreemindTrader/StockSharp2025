// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.NotifiableObjectGuiWrapper`1
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using Ecng.ComponentModel;
using Ecng.Configuration;
using System.ComponentModel;

namespace Ecng.Xaml
{
  /// <summary>
  ///   <see cref="T:Ecng.ComponentModel.DispatcherNotifiableObject`1" />
  /// </summary>
  public class NotifiableObjectGuiWrapper<T> : DispatcherNotifiableObject<T>
    where T : class, INotifyPropertyChanged
  {
    /// <summary>
    /// </summary>
    /// <param name="value">
    /// </param>
    public NotifiableObjectGuiWrapper(T value)
      : base(ConfigManager.GetService<IDispatcher>(), value)
    {
    }
  }
}
