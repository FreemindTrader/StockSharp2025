// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.TimeZoneComboBox
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Ecng.Xaml
{
  /// <summary>
  /// </summary>
  public class TimeZoneComboBox : ComboBox
  {
    /// <summary>
    /// </summary>
    public static readonly DependencyProperty SelectedTimeZoneProperty = DependencyProperty.Register(nameof(2127278669), typeof (TimeZoneInfo), typeof (TimeZoneComboBox), (PropertyMetadata) new UIPropertyMetadata((object) TimeZoneInfo.Utc, new PropertyChangedCallback((object) TimeZoneComboBox.SomeShit.ShitMethod02, __methodptr(\u0023\u003DzPPOT3JdaBe1nDG7jMmVBBvY\u003D))));

    /// <summary>
    /// </summary>
    public TimeZoneComboBox()
    {
      this.ItemsSource = (IEnumerable) TimeZoneInfo.GetSystemTimeZones();
    }

    /// <summary>
    /// </summary>
    public TimeZoneInfo SelectedTimeZone
    {
      get
      {
        return (TimeZoneInfo) this.GetValue(TimeZoneComboBox.SelectedTimeZoneProperty);
      }
      set
      {
        this.SetValue(TimeZoneComboBox.SelectedTimeZoneProperty, (object) value);
      }
    }

    /// <inheritdoc />
    protected override void OnSelectionChanged(SelectionChangedEventArgs e)
    {
      this.SelectedTimeZone = (TimeZoneInfo) this.SelectedItem;
      base.OnSelectionChanged(e);
    }

    [Serializable]
    private sealed class SomeShit
    {
      public static readonly TimeZoneComboBox.SomeShit ShitMethod02 = new TimeZoneComboBox.SomeShit();

      internal void \u0023\u003DzPPOT3JdaBe1nDG7jMmVBBvY\u003D(
        DependencyObject _param1,
        DependencyPropertyChangedEventArgs _param2)
      {
        ((Selector) _param1).SelectedItem = ((DependencyPropertyChangedEventArgs) ref _param2).get_NewValue();
      }
    }
  }
}
