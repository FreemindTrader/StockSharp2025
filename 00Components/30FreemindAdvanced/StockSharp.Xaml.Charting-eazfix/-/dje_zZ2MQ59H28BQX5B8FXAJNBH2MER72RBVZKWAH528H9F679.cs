// Decompiled with JetBrains decompiler
// Type: -.dje_zZ2MQ59H28BQX5B8FXAJNBH2MER72RBVZKWAH528H9F679PZ_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace \u002D;

internal sealed class dje_zZ2MQ59H28BQX5B8FXAJNBH2MER72RBVZKWAH528H9F679PZ_ejd
{
  public static readonly DependencyProperty \u0023\u003Dz8gpJZfsjfhZY = DependencyProperty.RegisterAttached("IsFocusable", typeof (bool), typeof (dje_zZ2MQ59H28BQX5B8FXAJNBH2MER72RBVZKWAH528H9F679PZ_ejd), new PropertyMetadata((object) true, new PropertyChangedCallback(dje_zZ2MQ59H28BQX5B8FXAJNBH2MER72RBVZKWAH528H9F679PZ_ejd.\u0023\u003Dz9rT93zhf3BVL)));

  public static bool GetIsFocusable(DependencyObject _param0)
  {
    return (bool) _param0.GetValue(dje_zZ2MQ59H28BQX5B8FXAJNBH2MER72RBVZKWAH528H9F679PZ_ejd.\u0023\u003Dz8gpJZfsjfhZY);
  }

  public static void SetIsFocusable(DependencyObject _param0, bool _param1)
  {
    _param0.SetValue(dje_zZ2MQ59H28BQX5B8FXAJNBH2MER72RBVZKWAH528H9F679PZ_ejd.\u0023\u003Dz8gpJZfsjfhZY, (object) _param1);
  }

  private static void \u0023\u003Dz9rT93zhf3BVL(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    Control control = _param0 as Control;
    bool newValue = (bool) _param1.NewValue;
    if (control == null)
      return;
    control.Focusable = newValue;
  }
}
